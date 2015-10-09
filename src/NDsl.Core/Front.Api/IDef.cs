using System;
using NDsl.Back.Api.Def;

namespace NDsl.Front.Api
{
    public interface IDef<out TModel, out TId> : IArtefact<TModel>
        where TModel : IDefModel<TId>
        where TId : IDefId
    {
        IDisposable Define();
        void Ref();
    }
}