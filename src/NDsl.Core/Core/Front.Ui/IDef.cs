using System;
using JetBrains.Annotations;

namespace NDsl.Front.Ui
{
    public interface IDef : IArtefact
    {
        [NotNull] new IDefApi Api { get; }
        IDisposable Define();
        void Ref();
    }
}