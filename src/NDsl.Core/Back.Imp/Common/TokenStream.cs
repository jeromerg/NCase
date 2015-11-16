using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Common
{
    public class TokenStream : ITokenStream
    {
        private readonly Queue<IToken> mTokens = new Queue<IToken>();
        private RecorderMode mMode;

        [NotNull] public IEnumerable<IToken> Tokens
        {
            get
            {
                while (mTokens.Count > 0)
                {
                    if (mMode != RecorderMode.Read)
                        throw new InvalidRecPlayStateException("Cannot read token as TokenStream is currently not in reading mode");

                    yield return mTokens.Dequeue();
                }
            }
        }

        public RecorderMode Mode
        {
            get { return mMode; }
        }

        public IDisposable SetReadMode()
        {
            return new DisposableWithCallbacks(() => SetReadMode(true),() => SetReadMode(false));
        }

        public void SetReadMode(bool isReadMode)
        {
            if (mMode == RecorderMode.Write)
                throw new InvalidRecPlayStateException("Cannot set read mode as TokenStream is currently in write mode");

            mMode = isReadMode ? RecorderMode.Read : RecorderMode.None;
        }


        public IDisposable SetWriteMode()
        {
            return new DisposableWithCallbacks(()=>SetWriteMode(true),()=>SetWriteMode(false));
        }

        public void SetWriteMode(bool isWriteMode)
        {
            if (mMode == RecorderMode.Read)
                throw new InvalidRecPlayStateException("Cannot set write mode as TokenStream is currently in read mode");

            mMode = isWriteMode ? RecorderMode.Write : RecorderMode.None;
        }

        public void Append(IToken token)
        {
            if (mMode != RecorderMode.Write)
                throw new InvalidRecPlayStateException("Cannot append token as TokenStream is currently not in writing mode");

            if (token == null) throw new ArgumentNullException("token");

            mTokens.Enqueue(token);
        }
    }
}