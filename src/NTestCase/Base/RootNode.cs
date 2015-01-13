using NTestCase.Api.Dev;

namespace NTestCase.Base
{
    public class RootNode : NodeBase<RootNode.RootNodeTarget>
    {
        public class RootNodeTarget : ITarget
        {
            // without explicit override, the Equals implementation redirect to ReferenceEquals
            // as a result, two RootNodes have unequal TargetKey and are considered as different target
            // by the algorithms
        }

        public RootNode() : base(new RootNodeTarget())
        {
        }

        public override void Replay()
        {
            // do nothing
        }
    }
}