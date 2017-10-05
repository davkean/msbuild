// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Build.Evaluation
{
    /// <summary>
    /// Splits an expression into fragments at semicolons, except where the
    /// semicolons are in a macro or separator expression.
    /// Fragments are trimmed and empty fragments discarded.
    /// </summary>
    /// <remarks>
    /// These complex cases prevent us from doing a simple split on ';':
    ///  (1) Macro expression: @(foo->'xxx;xxx')
    ///  (2) Separator expression: @(foo, 'xxx;xxx')
    ///  (3) Combination: @(foo->'xxx;xxx', 'xxx;xxx')
    ///  We must not split on semicolons in macro or separator expressions like these.
    /// </remarks>
    internal struct SemicolonTokenizer : IEnumerable<StringSegment>
    {
        private readonly StringSegment _expression;

        public SemicolonTokenizer(StringSegment expression)
        {
            _expression = expression;
        }

        public Enumerator GetEnumerator() => new Enumerator(_expression);

        IEnumerator<StringSegment> IEnumerable<StringSegment>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        internal struct Enumerator : IEnumerator<StringSegment>
        {
            private readonly StringSegment _expression;
            private StringSegment _current;
            private int _index;

            public Enumerator(StringSegment expression)
            {
                _expression = expression;
                _index = 0;
                _current = default(StringSegment);
            }

            public StringSegment Current
            {
                get { return _current; }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                throw new System.NotImplementedException();
            }

            public void Reset()
            {
                _current = default(StringSegment);
                _index = 0;
            }
        }
    }
}
