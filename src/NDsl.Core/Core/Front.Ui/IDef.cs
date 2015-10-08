using System;
using JetBrains.Annotations;

namespace NDsl.Front.Ui
{
    public interface IDef<out TApi> : IArtefact<TApi>
    {
        IDisposable Define();
        void Ref();
    }
}