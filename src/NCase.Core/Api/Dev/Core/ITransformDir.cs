using NCase.Api.Pub;

namespace NCase.Api.Dev.Core
{
    public interface ITransformDir<TArtefact, out TResult, in TTransform>
        where TArtefact : IArtefact
        where TTransform : ITransform<TArtefact, TResult>
    {
        TResult Transform(TTransform transform);
    }
}