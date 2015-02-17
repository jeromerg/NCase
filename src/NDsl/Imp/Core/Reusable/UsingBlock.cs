using System;
using NDsl.Api.Core;
using NDsl.Imp.Core.Token;
using NVisitor.Common.Quality;

namespace NDsl.Imp.Core.Semantic
{
    public class SemanticalBlockDisposable<T> : IDisposable
    {
        [NotNull]
        private readonly IAstRoot mAstRoot;
        private readonly T mParent;

        public SemanticalBlockDisposable([NotNull] IAstRoot astRoot, [NotNull] T parent)
        {
            if (astRoot == null) throw new ArgumentNullException("astRoot");
            if (parent == null) throw new ArgumentNullException("parent");

            mAstRoot = astRoot;
            mParent = parent;

            mAstRoot.AddChild(new BeginToken<T>(mParent));
        }

        public void Dispose()
        {
            mAstRoot.AddChild(new EndToken<T>(mParent));
        }
    }
}
