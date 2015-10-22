using System;
using NDsl.Back.Api.Def;

namespace NDsl.Front.Api
{
    public interface DefBase
    {
        IDisposable Define();
        void Ref();
    }

    public interface DefBase<out TModel, out TId> : Artefact<TModel>, DefBase
        where TModel : IDefModel<TId>
        where TId : IDefId
    {
    }
}