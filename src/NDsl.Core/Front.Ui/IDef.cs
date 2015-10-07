using System;
using NDsl.Back.Api.Core;

namespace NDsl.Front.Ui
{
    /// <summary>Definition that can be defined and referenced in a NDsl workspace</summary>
    public interface IDef<TDefId, out TApi> : IArtefact<TApi>
        where TApi : IDefApi<TDefId>
        where TDefId : IDefId
    {
        IDisposable Define();
        void Ref();
    }
}