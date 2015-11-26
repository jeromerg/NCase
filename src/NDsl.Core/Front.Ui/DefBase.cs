using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NDsl.All.Def;
using NDsl.Front.Api;

namespace NDsl.Front.Ui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface DefBase<out TDefiner>
        where TDefiner : Definer
    {
        [NotNull]
        TDefiner Define();

        void Ref();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public interface DefBase<out TModel, out TId, out TDefiner> : Artefact<TModel>, DefBase<TDefiner>
        where TModel : IDefModel<TId>
        where TId : IDefId
        where TDefiner : Definer
    {
    }
}