using System;
using NDsl.Back.Api.Def;

namespace NDsl.Front.Api
{
    public interface IDef
    {
        IDisposable Define();
        void Ref();
    }

    public interface IDef<out TModel, out TId> : IArtefact<TModel>, IDef
        where TModel : IDefModel<TId>
        where TId : IDefId
    {
    }
}