using System;
using NVisitor.Common.Quality;

namespace NDsl.Back.Api.Core
{
    public class DisposableWithCallbacks : IDisposable
    {
        private readonly Action mOnEnd;

        public DisposableWithCallbacks([NotNull] Action onBegin, [NotNull] Action onEnd)
        {
            if (onBegin == null) throw new ArgumentNullException("onBegin");
            if (onEnd == null) throw new ArgumentNullException("onEnd");
            mOnEnd = onEnd;
            onBegin();
        }

        public void Dispose()
        {
            mOnEnd();
        }
    }
}