//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace nanoFramework.Aws.SignatureVersion4
{
    /// <summary>
    /// Common methods and properties for all AWS Signature Version 4 signer variants
    /// </summary>
    public abstract class SignerBase
    {
        // SHA256 hash of an empty request body
        internal const string EMPTY_BODY_SHA256 = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";

        internal const string SCHEME = "AWS4";
        internal const string ALGORITHM = "HMAC-SHA256";
        internal const string TERMINATOR = "aws4_request";

        // format strings for the date/time and date stamps required during signing
        internal const string ISO8601BasicFormat = "yyyyMMddTHHmmssZ";
        internal const string DateStringFormat = "yyyyMMdd";

        // some common x-amz-* parameters
        internal const string X_Amz_Algorithm = "X-Amz-Algorithm";
        internal const string X_Amz_Credential = "X-Amz-Credential";
        internal const string X_Amz_SignedHeaders = "X-Amz-SignedHeaders";
        internal const string X_Amz_Date = "X-Amz-Date";
        internal const string X_Amz_Signature = "X-Amz-Signature";
        internal const string X_Amz_Expires = "X-Amz-Expires";
        internal const string X_Amz_Content_SHA256 = "X-Amz-Content-SHA256";
        internal const string X_Amz_Decoded_Content_Length = "X-Amz-Decoded-Content-Length";
        internal const string X_Amz_Meta_UUID = "X-Amz-Meta-UUID";

        // request canonicalization requires multiple whitespace compression
        internal static readonly Regex CompressWhitespaceRegex = new Regex("\\s+");

        // algorithm used to hash the canonical request that is supplied to
        // the signature computation
        internal static readonly SHA256 CanonicalRequestHashAlgorithm = SHA256.Create();

        /// <summary>
        /// The service endpoint, including the path to any resource.
        /// </summary>
        public Uri EndpointUri { get; set; }

        /// <summary>
        /// The HTTP verb for the request, e.g. GET.
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// The signing name of the service, e.g. 's3'.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// The system name of the AWS region associated with the endpoint, e.g. us-east-1.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Returns the canonical collection of header names that will be included in
        /// the signature. For AWS Signature Version 4, all header names must be included in the process 
        /// in sorted canonicalized order.
        /// </summary>
        /// <param name="headers">
        /// The set of header names and values that will be sent with the request
        /// </param>
        /// <returns>
        /// The set of header names canonicalized to a flattened, ;-delimited string
        /// </returns>
        protected string CanonicalizeHeaderNames(IDictionary headers)
        {
            var headersToSign = new ArrayList();
            foreach (DictionaryEntry kvp in headers)
            {
                headersToSign.Add(kvp.Key);
            }
            headersToSign.Sort(StringComparer.OrdinalIgnoreCase);

            var sb = new StringBuilder();
            foreach (var header in headersToSign)
            {
                if (sb.Length > 0)
                    sb.Append(";");
                sb.Append(header.ToString().ToLower());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Computes the canonical headers with values for the request. 
        /// For AWS Signature Version 4, all headers must be included in the signing process.
        /// </summary>
        /// <param name="headers">The set of headers to be encoded</param>
        /// <returns>Canonicalized string of headers with values</returns>
        protected virtual string CanonicalizeHeaders(IDictionary headers)
        {
            if (headers == null || headers.Count == 0)
                return string.Empty;

            // step1: sort the headers using lower-case format; we create a new
            // map to ensure we can do a subsequent key lookup using a lower-case
            // key regardless of how 'headers' was created.

            var headerKeys = new ArrayList();
            foreach (DictionaryEntry kvp in headers)
            {
                headerKeys.Add(kvp.Key);
            }
            headerKeys.Sort(StringComparer.OrdinalIgnoreCase);

            // step2: form the canonical header:value entries in sorted order. 
            // Multiple white spaces in the values should be compressed to a single 
            // space.
            var sb = new StringBuilder();
            foreach (var p in headerKeys)
            {
                sb.Append($"{p.ToString().ToLower()}={CompressWhitespaceRegex.Replace(headers[p].ToString().Trim(), " ")}\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the canonical request string to go into the signer process; this 
        /// consists of several canonical sub-parts.
        /// </summary>
        /// <param name="endpointUri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="queryParameters"></param>
        /// <param name="canonicalizedHeaderNames">
        /// The set of header names to be included in the signature, formatted as a flattened, ;-delimited string
        /// </param>
        /// <param name="canonicalizedHeaders">
        /// </param>
        /// <param name="bodyHash">
        /// Precomputed SHA256 hash of the request body content. For chunked encoding this
        /// should be the fixed string ''.
        /// </param>
        /// <returns>String representing the canonicalized request for signing</returns>
        protected string CanonicalizeRequest(Uri endpointUri,
                                             string httpMethod,
                                             string queryParameters,
                                             string canonicalizedHeaderNames,
                                             string canonicalizedHeaders,
                                             string bodyHash)
        {
            var canonicalRequest = new StringBuilder();

            canonicalRequest.Append($"{httpMethod}\n");
            canonicalRequest.Append($"{CanonicalResourcePath(endpointUri)}\n");
            canonicalRequest.Append($"{queryParameters}\n");

            canonicalRequest.Append($"{canonicalizedHeaders}\n");
            canonicalRequest.Append($"{canonicalizedHeaderNames}\n");

            canonicalRequest.Append(bodyHash);

            return canonicalRequest.ToString();
        }

        /// <summary>
        /// Returns the canonicalized resource path for the service endpoint
        /// </summary>
        /// <param name="endpointUri">Endpoint to the service/resource</param>
        /// <returns>Canonicalized resource path for the endpoint</returns>
        protected string CanonicalResourcePath(Uri endpointUri)
        {
            if (string.IsNullOrEmpty(endpointUri.AbsolutePath))
                return "/";

            // encode the path per RFC3986
            return HttpUtility.UrlEncode(endpointUri.AbsolutePath); // TODO: check if hash ('#') is allowed/supported as per original implementation.
        }

        /// <summary>
        /// Compute and return the multi-stage signing key for the request.
        /// </summary>
        /// <param name="awsSecretAccessKey">The clear-text AWS secret key</param>
        /// <param name="region">The region in which the service request will be processed</param>
        /// <param name="date">Date of the request, in yyyyMMdd format</param>
        /// <param name="service">The name of the service being called by the request</param>
        /// <returns>Computed signing key</returns>
        protected byte[] DeriveSigningKey(string awsSecretAccessKey, string region, string date, string service)
        {
            const string ksecretPrefix = SCHEME;

            string ksecret = (ksecretPrefix + awsSecretAccessKey);

            byte[] hashDate = ComputeKeyedHash(Encoding.UTF8.GetBytes(ksecret), Encoding.UTF8.GetBytes(date));
            byte[] hashRegion = ComputeKeyedHash(hashDate, Encoding.UTF8.GetBytes(region));
            byte[] hashService = ComputeKeyedHash(hashRegion, Encoding.UTF8.GetBytes(service));
            return ComputeKeyedHash(hashService, Encoding.UTF8.GetBytes(TERMINATOR));
        }

        /// <summary>
        /// Compute and return the hash of a data blob using the specified algorithm
        /// and key
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="data">Data blob</param>
        /// <returns>Hash of the data</returns>
        protected byte[] ComputeKeyedHash(byte[] key, byte[] data)
        {
            var kha = new HMACSHA256(key);
            return kha.ComputeHash(data);
        }

        /// <summary>
        /// Helper to format a byte array into string
        /// </summary>
        /// <param name="data">The data blob to process</param>
        /// <param name="lowercase">If true, returns hex digits in lower case form</param>
        /// <returns>String version of the data</returns>
        public static string ToHexString(byte[] data, bool lowercase)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString(lowercase ? "x2" : "X2"));
            }
            return sb.ToString();
        }
    }
}