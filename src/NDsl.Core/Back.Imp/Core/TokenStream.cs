using System;
using System.Collections.Generic;
using NDsl.Back.Api.Core;
using NVisitor.Common.Quality;

namespace NDsl.Back.Imp.Core
{
    public class TokenStream : ITokenReaderWriter
    {
        private readonly Queue<IToken> mTokens = new Queue<IToken>();

        [NotNull] public IEnumerable<IToken> Tokens
        {
            get
            {
                while (mTokens.Count > 0)
                    yield return mTokens.Dequeue();
            }
        }

        public void Append(IToken token)
        {
            if (token == null) throw new ArgumentNullException("token");

            mTokens.Enqueue(token);
        }
    }
}