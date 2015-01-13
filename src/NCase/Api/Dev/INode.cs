using System.Collections.Generic;
using NCase.Util.Quality;

namespace NCase.Api.Dev
{
    public interface INode<out TKey>
        where TKey : ITarget
    {
        [NotNull]
        List<INode<ITarget>> Children { get; }

        [NotNull]
        TKey TargetKey { get; }
        
        void Replay();
    }
}