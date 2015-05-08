using System;
using System.Collections.Generic;
using NCase.Api;
using NCase.Api.Nod;
using NCase.Api.Vis;
using NDsl.Api.Core;
using NDsl.Api.Core.Util;
using NVisitor.Common.Quality;

namespace NCase.Imp.Nod
{
    public class TreeCaseSetNode : ICaseSetNode
    {
        [NotNull] private readonly List<INode> mChildren = new List<INode>();
        [NotNull] private readonly TreeCaseSet mCaseSet;
        [NotNull] private readonly ICodeLocation mCodeLocation;
        [NotNull] private readonly ITreeCaseSetInsertChildDirector mInsertChildDirector;

        public TreeCaseSetNode(
            [NotNull] TreeCaseSet caseSet, 
            [NotNull] ICodeLocation codeLocation,
            [NotNull] ITreeCaseSetInsertChildDirector insertChildDirector)
        {
            if (caseSet == null) throw new ArgumentNullException("caseSet");
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");
            if (insertChildDirector == null) throw new ArgumentNullException("insertChildDirector");

            mCaseSet = caseSet;
            mCodeLocation = codeLocation;
            mInsertChildDirector = insertChildDirector;
        }

        public IEnumerable<INode> Children
        {
            get { return mChildren; }
        }

        public ICodeLocation CodeLocation
        {
            get { return mCodeLocation; }
        }

        public void AddChild(INode child)
        {
            mChildren.Add(child);
        }

        public ICaseSet CaseSet
        {
            get { return mCaseSet; }
        }

        public void InsertChild(INode newNode)
        {
            mInsertChildDirector.InitializeRoot(this);
            mInsertChildDirector.Visit(newNode);
        }

    }
}