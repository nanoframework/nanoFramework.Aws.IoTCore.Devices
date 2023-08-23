//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace nanoFramework.Aws.SignatureVersion4
{
    /// <summary>
    /// AWS Signature Version 4 signer for signing requests
    /// using an 'Authorization' header.
    /// </summary>
    public class SignerForAuthorizationHeader : SignerBase
    {
        /// <summary>
        /// Computes an Version 4 signature for a request, ready for inclusion as an 
        /// 'Authorization' header.
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
        /// The computed authorization string for the request. This value needs to be set as the 
        /// header 'Authorization' on the subsequent HTTP request.
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

            // update the headers with required 'x-amz-date' and 'host' values
            headers.Add(X_Amz_Date, dateTimeStamp);

            var hostHeader = EndpointUri.Host;
            hostHeader += ":" + EndpointUri.Port; // FIXME: should use //if (!EndpointUri.IsDefaultPort)
            headers.Add("Host", hostHeader);

            // canonicalize the headers; we need the set of header names as well as the
            // names and values to go into the signature process
            var canonicalizedHeaderNames = CanonicalizeHeaderNames(headers);
            var canonicalizedHeaders = CanonicalizeHeaders(headers);

            // if any query string parameters have been supplied, canonicalize them
            // (note this sample assumes any required url encoding has been done already)
            var canonicalizedQueryParameters = string.Empty;
            if (!string.IsNullOrEmpty(queryParameters))
            {
                var paramDictionary = new Hashtable();

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

                canonicalizedQueryParameters = sb.ToString();
            }

            // canonicalize the various components of the request
            var canonicalRequest = CanonicalizeRequest(EndpointUri,
                                                       HttpMethod,
                                                       canonicalizedQueryParameters,
                                                       canonicalizedHeaderNames,
                                                       canonicalizedHeaders,
                                                       bodyHash);
            Debug.WriteLine($"\nDEBUG-CanonicalRequest:\n{canonicalRequest}");

            // generate a hash of the canonical request, to go into signature computation
            var canonicalRequestHashBytes
                = CanonicalRequestHashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(canonicalRequest));

            // construct the string to be signed
            var stringToSign = new StringBuilder();

            var dateStamp = requestDateTime.ToString(DateStringFormat);
            var scope = $"{dateStamp}/{Region}/{Service}/{TERMINATOR}";

            stringToSign.Append($"{SCHEME}-{ALGORITHM}\n{dateTimeStamp}\n{scope}\n");
            stringToSign.Append(ToHexString(canonicalRequestHashBytes, true));

            Debug.WriteLine($"\nDEBUG-StringToSign:\n{stringToSign}");

            // compute the signing key
            var kha = new HMACSHA256(DeriveSigningKey(awsSecretKey, Region, dateStamp, Service));

            // compute the AWS4 signature and return it
            var signature = kha.ComputeHash(Encoding.UTF8.GetBytes(stringToSign.ToString()));
            var signatureString = ToHexString(signature, true);
            Debug.WriteLine($"\nDEBUG-Signature:\n{signatureString}");

            var authString = new StringBuilder();
            authString.Append($"{SCHEME}-{ALGORITHM} ");
            authString.Append($"Credential={awsAccessKey}/{scope}, ");
            authString.Append($"SignedHeaders={canonicalizedHeaderNames}, ");
            authString.Append($"Signature={signatureString}");

            var authorization = authString.ToString();
            Debug.WriteLine($"\nDEBUG-Authorization:\n{authorization}");

            return authorization;
        }
    }
}
