using JetBrains.Annotations;
using NDsl.Front.Api;

namespace NCase.Front.Api
{
    public interface IBuilder // TODO JRG: IArtefact<IBuilder>
    {
        [NotNull]
        TDef CreateDef<TDef>([NotNull] string name) where TDef : IDef<TDef>;

        [NotNull]
        T CreateContributor<T>([NotNull] string name);
    }
}