using System.Collections.Generic;
using NTestCase.Util.Quality;

namespace NTestCase.Api.Dev
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