using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Book;
using NDsl.Back.Api.Common;

namespace NDsl.Back.Imp.Common
{
    public class TokenStream : IBook
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