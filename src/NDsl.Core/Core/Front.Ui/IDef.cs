using System;
using JetBrains.Annotations;

namespace NDsl.Front.Ui
{
    public interface IDef<out TApi> : IArtefact<TApi>
        where TApi : IDefApi
    {
        IDisposable Define();
        void Ref();
    }
}