using System.Collections.Generic;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public class CaseImp : Artefact<ICaseApi>, ICase, ICaseApi
    {
        private readonly IEnumerable<INode> mFactNodes;

        public CaseImp(IEnumerable<INode> factNodes, ITools tools)
            : base(tools)
        {
            mFactNodes = factNodes;
        }

        public override ICaseApi Api
        {
            get { return this; }
        }

        public IEnumerable<INode> FactNodes
        {
            get { return mFactNodes; }
        }
    }
}