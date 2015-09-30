using System.Diagnostics.CodeAnalysis;

namespace NDsl.Front.Ui
{
    public interface IOp
    {
    }

    /// <summary>Operation base interface</summary>
    /// <typeparam name="TArtefact">The artefact the operation applies to</typeparam>
    /// <typeparam name="TResult">The result of the operation</typeparam>
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public interface IOp<in TArtefact, TResult> : IOp
        where TArtefact : IArtefact
    {
    }
}