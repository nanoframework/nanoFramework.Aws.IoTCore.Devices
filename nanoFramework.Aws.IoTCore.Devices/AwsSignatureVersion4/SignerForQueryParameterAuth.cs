
//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace nanoFramework.Aws.SignatureVersion4
{
    /// <summary>
    /// AWS Signature Version 4 signer for signing requests
    /// using query string parameters.
    /// </summary>
    public class SignerForQueryParameterAuth : SignerBase
    {
        /// <summary>
        /// Computes an AWS Signature Version 4 authorization for a request, suitable for embedding 
        /// in query parameters.
        /// </summary>
        /// <param name="headers">
        /// The request headers; 'Host' and 'X-Amz-Date' will be added to this set.
        /// </param>
        /// <param name="queryParameters">
        /// Any query parameters that will be added to the endpoint. The parameters 
        /// should be specified in canonical format.
        /// </param>
        /// <param name="bodyHash">
        /// Precomputed SHA256 hash of the request body content; this value should also
        /// be set as the header 'X-Amz-Content-SHA256' for non-streaming uploads.
        /// </param>
        /// <param name="awsAccessKey">
        /// The user's AWS Access Key.
        /// </param>
        /// <param name="awsSecretKey">
        /// The user's AWS Secret Key.
        /// </param>
        /// <returns>
        /// The string expressing the Signature V4 components to add to query parameters.
        /// </returns>
        public string ComputeSignature(IDictionary headers,
                                       string queryParameters,
                                       string bodyHash,
                                       string awsAccessKey,
                                       string awsSecretKey)
        {
            // first get the date and time for the subsequent request, and convert to ISO8601 format (without '-' and ':')
            // for use in signature generation
            var requestDateTime = DateTime.UtcNow;
            var dateTimeStamp = requestDateTime.ToString(ISO8601BasicFormat);

            // extract the host portion of the endpoint to include in the signature calculation,
            // unless already set
            if (!headers.Contains("Host"))
            {
                var hostHeader = EndpointUri.Host;
                hostHeader += ":" + EndpointUri.Port; // FIXME: should use //if (!EndpointUri.IsDefaultPort)
                headers.Add("Host", hostHeader);
            }

            var dateStamp = requestDateTime.ToString(DateStringFormat);
            var scope = $"{dateStamp}/{Region}/{Service}/{TERMINATOR}";

            // canonicalized headers need to be expressed in the query
            // parameters processed in the signature
            var canonicalizedHeaderNames = CanonicalizeHeaderNames(headers);
            var canonicalizedHeaders = CanonicalizeHeaders(headers);

            // reform the query parameters to (a) add the parameters required for
            // Signature V4 and (b) canonicalize the set before they go into the
            // signature calculation. Note that this assumes parameter names and 
            // values added outside this routine are already url encoded
            var paramDictionary = new Hashtable();
            if (!string.IsNullOrEmpty(queryParameters))
            {
                var qparam = queryParameters.Split('&');
                foreach (string p in qparam)
                {
                    var items = p.Split('=');
                    if (items.Length == 1)
                    {
                        paramDictionary.Add(items[0], null);
                    }
                    else
                    {
                        paramDictionary.Add(items[0], items[1]);
                    }
                }
            }

            // add the fixed authorization params required by Signature V4
            paramDictionary.Add(X_Amz_Algorithm, HttpUtility.UrlEncode($"{SCHEME}-{ALGORITHM}"));
            paramDictionary.Add(X_Amz_Credential, HttpUtility.UrlEncode($"{awsAccessKey}/{scope}"));
            paramDictionary.Add(X_Amz_SignedHeaders, HttpUtility.UrlEncode(canonicalizedHeaderNames));

            // x-amz-date is now added as a query parameter, not a header, but still needs to be in ISO8601 basic form
            paramDictionary.Add(X_Amz_Date, HttpUtility.UrlEncode(dateTimeStamp));

            // build the expanded canonical query parameter string that will go into the
            // signature computation
            var sb = new StringBuilder();
            var paramKeys = new ArrayList();
            foreach (DictionaryEntry kvp in paramDictionary)
            {
                paramKeys.Add(kvp.Key);
            }
            paramKeys.Sort(StringComparer.Ordinal);
            foreach (var p in paramKeys)
            {
                if (sb.Length > 0)
                    sb.Append("&");
                sb.Append($"{p}={paramDictionary[p]}");
            }
            var canonicalizedQueryParameters = sb.ToString();

            // express all the header and query parameter data as a canonical request string
            var canonicalRequest = CanonicalizeRequest(EndpointUri,
                                                       HttpMethod,
                                                       canonicalizedQueryParameters,
                                                       canonicalizedHeaderNames,
                                                       canonicalizedHeaders,
                                                       bodyHash);
            //Debug.WriteLine($"\nDEBUG-CanonicalRequest:\n{canonicalRequest}");

            byte[] canonicalRequestHashBytes
                = CanonicalRequestHashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(canonicalRequest));

            // construct the string to be signed
            var stringToSign = new StringBuilder();

            stringToSign.Append($"{SCHEME}-{ALGORITHM}\n{dateTimeStamp}\n{scope}\n");
            stringToSign.Append(ToHexString(canonicalRequestHashBytes, true));

            //Debug.WriteLine($"\nDEBUG-StringToSign:\n{stringToSign}");

            // compute the multi-stage signing key
            var kha = new HMACSHA256(DeriveSigningKey(awsSecretKey, Region, dateStamp, Service));

            // compute the final signature for the request, place into the result and return to the 
            // user to be embedded in the request as needed
            var signature = kha.ComputeHash(Encoding.UTF8.GetBytes(stringToSign.ToString()));
            var signatureString = ToHexString(signature, true);
            //Debug.WriteLine($"\nDEBUG-Signature:\n{signatureString}");

            // form up the authorization parameters for the caller to place in the query string
            var authString = new StringBuilder();
            var authParams = new string[]
                {
                    X_Amz_Algorithm,
                    X_Amz_Credential,
                    X_Amz_Date,
                    X_Amz_SignedHeaders
                };

            foreach (var p in authParams)
            {
                if (authString.Length > 0)
                    authString.Append("&");
                authString.Append($"{p}={paramDictionary[p]}");
            }

            authString.Append($"&{X_Amz_Signature}={signatureString}");

            var authorization = authString.ToString();
            //Debug.WriteLine($"\nDEBUG-Authorization:\n{authorization}");

            return authorization;
        }
    }
}
