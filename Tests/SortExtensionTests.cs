//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.TestFramework;
using System;
using System.Collections;

namespace nanoFramework.Aws.SignatureVersion4.Tests
{
    [TestClass]
    public class SortExtensionTests
    {
        [TestMethod]
        public void check_ArrayList_Sort_Ordinal_when_order_is_canonical_already()
        {
            var list = new ArrayList();
            list.Add("aTest");
            list.Add("Atest2");
            list.Add("btest");
            list.Add("ctest");

            list.Sort(StringComparer.Ordinal);

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

            

            // Expect "atest;atest2;btest;ctest"
        }

        [TestMethod]
        public void check_check_ArrayList_Sort_Ordinal_when_order_is_not_sorted()
        {
            var list = new ArrayList();
            list.Add("aTest");
            list.Add("ctest");
            list.Add("btest");
            list.Add("Atest2");

            list.Sort(StringComparer.Ordinal);

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

            // Expect "atest;atest2;btest;ctest"
        }


        [TestMethod]
        public void check_ArrayList_Sort_OrdinalIgnoreCase_when_order_is_canonical_already()
        {
            var list = new ArrayList();
            list.Add("aTest");
            list.Add("Atest2");
            list.Add("btest");
            list.Add("ctest");

            list.Sort(StringComparer.OrdinalIgnoreCase);

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }



            // Expect "atest;atest2;btest;ctest"
        }

        [TestMethod]
        public void check_check_ArrayList_Sort_OrdinalIgnoreCase_when_order_is_not_sorted()
        {
            var list = new ArrayList();
            list.Add("aTest");
            list.Add("ctest");
            list.Add("btest");
            list.Add("Atest2");

            list.Sort(StringComparer.OrdinalIgnoreCase);

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }

            // Expect "atest;atest2;btest;ctest"
        }

    }
}
