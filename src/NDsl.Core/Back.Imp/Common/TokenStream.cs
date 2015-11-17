using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Ex;
using NDsl.Back.Api.Record;

namespace NDsl.Back.Imp.Common
{
    // TODO: Mix between Recorder and TokenStream and inheritance are questionable!
    public class TokenStream : Recorder, ITokenStream
    {
        private readonly Queue<IToken> mTokens = new Queue<IToken>();

        [NotNull] public IEnumerable<IToken> Tokens
        {
            get
            {
                while (mTokens.Count > 0)
                {
                    if (Mode != RecorderMode.Read)
                        throw new InvalidRecPlayStateException("Cannot read token as TokenStream is currently not in reading mode");

                    yield return mTokens.Dequeue();
                }
            }
        }

        public void Append(IToken token)
        {
            if (Mode != RecorderMode.Write)
                throw new InvalidRecPlayStateException("Cannot append token as TokenStream is currently not in writing mode");

            if (token == null) throw new ArgumentNullException("token");

            mTokens.Enqueue(token);
        }
    }
}