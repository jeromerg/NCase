using JetBrains.Annotations;

namespace NDsl.Front.Api
{
    public interface IBuilderFactory
    {
        [NotNull] 
        CaseBuilder Create();
    }
}