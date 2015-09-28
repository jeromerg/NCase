using System.Diagnostics.CodeAnalysis;

namespace NCase.Front.Api
{
    /// <summary>
    /// Describes the artefact produced during the build and processing, which can be manipulated by the user
    /// </summary>
    /// <typeparam name="TArtefact"></typeparam>
    [SuppressMessage("ReSharper", "TypeParameterCanBeVariant")]
    public interface IArtefact<TArtefact>
    {
        TResult Perform<TOp, TResult>(TOp operation) where TOp : IOp<TArtefact, TResult>;
    }
}