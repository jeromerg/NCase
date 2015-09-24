

using JetBrains.Annotations;

namespace NCase.Front.Api
{
    public interface IBuilder
    {
        [NotNull]
        TDef CreateDef<TDef>([NotNull] string name) where TDef : IDef;

        [NotNull]
        T CreateContributor<T>([NotNull] string name);
    }
}