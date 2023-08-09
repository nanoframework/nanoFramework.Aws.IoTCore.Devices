//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace System.Collections
{
    /// <summary>
    /// Extension methods used for canonicalization.
    /// </summary>
    public static class nfArrayListExtensions
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


    /// <summary>
    /// String Comparers
    /// </summary>
    public abstract class StringComparer : IComparer
    {
        private static readonly StringComparer _ordinal = new OrdinalComparer(false);
        private static readonly StringComparer _ordinalIgnoreCase = new OrdinalComparer(true);

        /// <summary>
        /// Ordinal Comparer
        /// </summary>
        public static StringComparer Ordinal
        {
            get
            {
                return _ordinal;
            }
        }

        /// <summary>
        /// Ordinal Comparer (ignoring case)
        /// </summary>
        public static StringComparer OrdinalIgnoreCase
        {
            get
            {
                return _ordinalIgnoreCase;
            }
        }

        /// <summary>
        /// Compares two objects
        /// </summary>
        /// <param name="x">Object x</param>
        /// <param name="y">Object y</param>
        /// <returns>The result</returns>
        /// <exception cref="ArgumentException"></exception>
        public int Compare(object x, object y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            string sa = x as string;
            if (sa != null)
            {
                string sb = y as string;
                if (sb != null)
                {
                    return Compare(sa, sb);
                }
            }

            IComparable ia = x as IComparable;
            if (ia != null)
            {
                return ia.CompareTo(y);
            }

            throw new ArgumentException("Argument_ImplementIComparable");
        }

        /// <summary>
        /// Compares two strings
        /// </summary>
        /// <param name="x">String x</param>
        /// <param name="y">String y</param>
        /// <returns>The result.</returns>
        public abstract int Compare(string x, string y);
    }

    internal sealed class OrdinalComparer : StringComparer
    {
        private bool _ignoreCase;

        internal OrdinalComparer(bool ignoreCase)
        {
            _ignoreCase = ignoreCase;
        }

        public override int Compare(string x, string y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (_ignoreCase)
            {
                return string.Compare(x.ToLower(), y.ToLower());
            }

            return string.Compare(x, y);
        }

    }

}
