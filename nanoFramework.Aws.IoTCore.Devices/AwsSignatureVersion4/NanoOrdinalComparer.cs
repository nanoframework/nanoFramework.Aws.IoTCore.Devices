//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace System.Collections
{

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
