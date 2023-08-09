//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.TestFramework;
using System;
using System.Collections;
using nanoFramework.Aws.SignatureVersion4;

namespace nanoFramework.Aws.SignatureVersion4.Tests
{
    // https://github.com/inspiration/uniface-aws-sigv4
    // https://github.com/FantasticFiasco/aws-signature-version-4
    // https://stackoverflow.com/questions/28966075/creating-a-hmac-signature-for-aws-rest-query-in-c-sharp
    [TestClass]
    public class SignatureVersion4Tests
    {
        [TestMethod]
        public void check_CanonicalizeHeaderNames_when_order_is_canonical_already() // using an already ordered list
        {
            //// Given that: SigV4 seems to require a header that is canonicalized
            //var headers = new Hashtable();
            //headers.Add("aTest", "atestval");
            //headers.Add("Atest2", "Atest2val");
            //headers.Add("btest", "btestval");
            //headers.Add("ctest", "ctestval");

            //Console.WriteLine(SignerBase.CanonicalizeHeaderNames(headers));

            // Expect "atest;atest2;btest;ctest"
        }

        [TestMethod]
        public void check_CanonicalizeHeaderNames_when_order_is_not_sorted()
        {
            //var headers = new Hashtable();
            //headers.Add("aTest", "atestval");
            //headers.Add("ctest", "ctestval");
            //headers.Add("btest", "btestval");
            //headers.Add("Atest2", "Atest2val");

            //Console.WriteLine(CanonicalizeHeaderNames(headers));

            // Expect "atest;atest2;btest;ctest"
        }


        [TestMethod]
        public void check_CanonicalizeHeaders_when_order_is_not_sorted()
        {
        //var headers = new Hashtable();
        //headers.Add("aTest", "atestval");
        //headers.Add("ctest", "ctestval");
        //headers.Add("btest", "btestval");
        //headers.Add("Atest2", "Atest2val");
        //Console.WriteLine(CanonicalizeHeaders(headers));

        // Expect "atest:atestval\natest2:Atest2val\nbtest:btestval\nctest:ctestval\n"
        }

    }
}
