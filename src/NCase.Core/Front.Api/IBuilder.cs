

using JetBrains.Annotations;

namespace NCase.Front.Api
{
    public interface IBuilder : IArtefact<IBuilder>
    {
        [NotNull]
        TDef CreateDef<TDef>([NotNull] string name) where TDef : IDef<TDef>;

        [NotNull]
        T CreateContributor<T>([NotNull] string name);
    }
}