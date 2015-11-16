using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.TokenStream;

namespace NDsl.Back.Imp.Common
{
    public class TokenStream : ITokenStream
    {
        private readonly Queue<IToken> mTokens = new Queue<IToken>();
        private TokenStreamMode mMode;

        [NotNull] public IEnumerable<IToken> Tokens
        {
            get
            {
                while (mTokens.Count > 0)
                {
                    if (mMode != TokenStreamMode.Read)
                        throw new InvalidRecPlayStateException("Cannot read token as TokenStream is currently not in reading mode");

                    yield return mTokens.Dequeue();
                }
            }
        }

        public TokenStreamMode Mode
        {
            get { return mMode; }
        }

        public void SetReadMode(bool isReadMode)
        {
            if(mMode == TokenStreamMode.Write)
                throw new InvalidRecPlayStateException("Cannot set read mode as TokenStream is currently in write mode");

            mMode = isReadMode ? TokenStreamMode.Write : TokenStreamMode.None;
        }

        public void SetWriteMode(bool isWriteMode)
        {
            if(mMode == TokenStreamMode.Read)
                throw new InvalidRecPlayStateException("Cannot set write mode as TokenStream is currently in read mode");

            mMode = isWriteMode ? TokenStreamMode.Write : TokenStreamMode.None;
        }

        public void Append(IToken token)
        {
            if (mMode != TokenStreamMode.Write)
                throw new InvalidRecPlayStateException("Cannot append token as TokenStream is currently not in writing mode");

            if (token == null) throw new ArgumentNullException("token");

            mTokens.Enqueue(token);
        }
    }
}