using JetBrains.Annotations;

namespace NDsl.Back.Api.Util
{
    public interface ICodeLocationPrinter
    {
        [NotNull] string Print([NotNull] CodeLocation codeLocation);
    }
}