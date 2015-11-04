using System;
using System.Diagnostics.CodeAnalysis;
using NDsl.Back.Api.Def;
using NDsl.Front.Api;

namespace NDsl.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface DefBase
    {
        IDisposable Define();
        void Ref();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface DefBase<out TModel, out TId> : Artefact<TModel>, DefBase
        where TModel : IDefModel<TId>
        where TId : IDefId
    {
    }
}