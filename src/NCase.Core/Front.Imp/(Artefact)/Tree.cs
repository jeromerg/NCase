using JetBrains.Annotations;
using NCase.Back.Api.Tree;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;

namespace NCase.Front.Imp
{
    public class Tree : SetDef<TreeId, ITreeApi>, ITree, ITreeApi
    {
        public Tree([NotNull] string defName, [NotNull] IBook book, [NotNull] ITools tools)
            : base(new TreeId(defName), book, tools)
        {
        }

        public override ITreeApi Api
        {
            get { return this; }
        }
    }
}