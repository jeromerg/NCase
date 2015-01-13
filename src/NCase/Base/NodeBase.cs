using System.Collections.Generic;
using NTestCase.Api.Dev;

namespace NTestCase.Base
{
    public abstract class NodeBase<TKey> : INode<TKey> 
        where TKey : ITarget
    {
        private readonly List<INode<ITarget>> mChildren = new List<INode<ITarget>>();
        private readonly TKey mTargetKey;

        protected NodeBase(TKey targetKey)
        {
            mTargetKey = targetKey;
        }

        public List<INode<ITarget>> Children
        {
            get { return mChildren; }
        }

        public TKey TargetKey
        {
            get { return mTargetKey; }
        }

        public abstract void Replay();
    }
}