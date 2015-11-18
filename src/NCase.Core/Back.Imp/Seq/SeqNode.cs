using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Back.Api.Seq;
using NDsl.All.Def;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Def;
using NDsl.Back.Api.Util;

namespace NCaseFramework.Back.Imp.Seq
{
    public class SeqNode : ISeqNode
    {
        [NotNull] private readonly SequenceId mId;
        [NotNull] private readonly List<INode> mChildren = new List<INode>();
        [NotNull] private readonly CodeLocation mCodeLocation;

        public SeqNode([NotNull] CodeLocation codeLocation, [NotNull] SequenceId id)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (id == null) throw new ArgumentNullException("id");

            mCodeLocation = codeLocation;
            mId = id;
        }

        [NotNull] 
        IDefId IDefNode.Id
        {
            get { return mId; }
        }

        [NotNull] 
        public SequenceId Id
        {
            get { return mId; }
        }

        [NotNull] 
        public IEnumerable<INode> Children
        {
            get { return mChildren; }
        }

        [NotNull] 
        public CodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public void AddChild([NotNull] INode child)
        {
            mChildren.Add(child);
        }
    }
}