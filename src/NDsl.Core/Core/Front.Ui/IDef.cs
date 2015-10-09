using System;
using JetBrains.Annotations;
using NDsl.Back.Api.Core;

namespace NDsl.Front.Ui
{
    public interface IDef<out TModel, out TId> : IArtefact<TModel>
        where TModel : IDefModel<TId>
        where TId : IDefId
    {
        IDisposable Define();
        void Ref();
    }
}