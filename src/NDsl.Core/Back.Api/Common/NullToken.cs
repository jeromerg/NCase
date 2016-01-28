using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Api.Common
{
    /// <summary>Null Token object, following Null Object Pattern</summary>
    public class NullToken : IToken
    {
        [NotNull]
        public static readonly NullToken Instance = new NullToken(CodeLocation.Unknown);

        [NotNull] private readonly CodeLocation mCodeLocation;

        public NullToken([NotNull] CodeLocation codeLocation)
        {
            mCodeLocation = codeLocation;
        }

        [NotNull] public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        [NotNull, ItemNotNull] public IEnumerable<IToken> Children
        {
            get { return Enumerable.Empty<IToken>(); }
        }
    }
}