using System;
using NVisitor.Common.Quality;

namespace NDsl.Util
{
    public class Disposable : IDisposable
    {
        private readonly Action mOnDisposeAction;

        public Disposable([NotNull] Action onDisposeAction)
        {
            if (onDisposeAction == null) throw new ArgumentNullException("onDisposeAction");

            mOnDisposeAction = onDisposeAction;
        }

        public void Dispose()
        {
            mOnDisposeAction();
        }
    }
}
