//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.TestFramework;
using System;
using System.Collections;

namespace nanoFramework.Aws.SignatureVersion4.Tests
{
    // https://github.com/inspiration/uniface-aws-sigv4
    // https://github.com/FantasticFiasco/aws-signature-version-4
    // https://stackoverflow.com/questions/28966075/creating-a-hmac-signature-for-aws-rest-query-in-c-sharp
    [TestClass]
    public class SignatureVersion4Tests
    {

        [TestMethod]
        public void check_SignerForQueryParameters_ComputeSignature_Get_iotGateway_generated_correctly()
        {
            // See: https://docs.aws.amazon.com/AmazonS3/latest/API/sigv4-query-string-auth.html
            // https://github.com/aws/aws-sdk-java-v2/blob/master/core/auth/src/test/java/software/amazon/awssdk/auth/signer/Aws4SignerTest.java
            // https://github.com/aws/aws-sdk-net/blob/master/sdk/test/Services/Signer/UnitTests/Generated/Endpoints/SignerEndpointProviderTests.cs

            var v4signer = new SignerForQueryParameterAuth
            {
                EndpointUri = new System.Uri("https://test.us-east-1.amazonaws.com"),
                HttpMethod = "GET",
                Service = "iotdevicegateway",
                Region = "us-east-1"
            };

            Console.WriteLine(v4signer.ComputeSignature(new Hashtable(), "", "", "fakeAccessKey", "fakeSecret"));

            //http://demo.us-east-1.amazonaws.com
            //?X-Amz-Algorithm=AWS4-HMAC-SHA256
            //&X-Amz-Credential=<your-access-key-id>/20130721/us-east-1/iotdevicegateway/aws4_request
            //&X-Amz-Date=20130721T201207Z
            //&X-Amz-SignedHeaders=host
            //&X-Amz-Signature=<signature-value>

            // replace '<your-access-key-id>' with "fakeAccessKey"
            // replace '<signature-value>'
            // replace '/' with '%2F' in url
            // ignore or inject datetime?
        }

        [TestMethod]
        public void check_SignerForQueryParameters_ComputeSignature_Post_S3_generated_correctly()
        {
            // See: https://docs.aws.amazon.com/AmazonS3/latest/API/sigv4-query-string-auth.html


            var v4signer = new SignerForQueryParameterAuth
            {
                EndpointUri = new System.Uri("https://test.us-east-1.amazonaws.com"),
                HttpMethod = "POST",
                Service = "s3",
                Region = "us-east-1"
            };

            // FIXME: would fail!
            var sigparams = new Hashtable
            {
                { "X-Amz-Expires", "86400" }
            };

            Console.WriteLine(v4signer.ComputeSignature(sigparams, "", "", "fakeAccessKey", "fakeSecret"));

            //http://demo.us-east-1.amazonaws.com
            //?X-Amz-Algorithm=AWS4-HMAC-SHA256
            //&X-Amz-Credential=<your-access-key-id>/20130721/us-east-1/s3/aws4_request
            //&X-Amz-Date=20130721T201207Z
            //&X-Amz-Expires=86400
            //&X-Amz-SignedHeaders=host
            //&X-Amz-Signature=<signature-value>

            // replace '<your-access-key-id>' with "fakeAccessKey"
            // replace '<signature-value>'
            // replace '/' with '%2F' in url
            // ignore or inject datetime?
        }

    }
}
