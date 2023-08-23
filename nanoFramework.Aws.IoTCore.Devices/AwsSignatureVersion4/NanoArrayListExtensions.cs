//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace System.Collections
{
    /// <summary>
    /// Extension methods used for canonicalization.
    /// </summary>
    public static class NanoArrayListExtensions
    {
        /// <summary>
        /// Sort an ArrayList
        /// </summary>
        /// <param name="items">Items in the ArrayList</param>
        /// <param name="comparer">The Comparer</param>
        public static void Sort(this ArrayList items, IComparer comparer)
        {
            int i;
            int j;
            object tmpItem;

            for (i = 0; i <= items.Count -1; i++)
            {
                tmpItem = items[i];
                j = i;

                while ((j > 0) && (comparer.Compare(items[j - 1], tmpItem) > 0))
                {
                    items[j] = items[j - 1];
                    j--;
                }

                items[j] = tmpItem;
            }
        }
    }

}
