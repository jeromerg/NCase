using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Back.Api.Seq;
using NDsl.Back.Api.Core;

namespace NCase.Back.Imp.Seq
{
    public class SeqNode : ISeqNode
    {
        [NotNull] private readonly SeqId mId;
        [NotNull] private readonly List<INode> mChildren = new List<INode>();
        private readonly CodeLocation mCodeLocation;

        public SeqNode([NotNull] CodeLocation codeLocation, [NotNull] SeqId id)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            mCodeLocation = codeLocation;
            mId = id;
        }

        public SeqId Id
        {
            get { return mId; }
        }

        public IDefId DefId
        {
            get { return mId; }
        }

        public IEnumerable<INode> Children
        {
            get { return mChildren; }
        }

        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public void AddChild(INode child)
        {
            mChildren.Add(child);
        }
    }
}