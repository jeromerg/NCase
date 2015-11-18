using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Api.Fact;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class CaseImp : ArtefactImp<ICaseModel>, Case, ICaseModel
    {
        [NotNull] private readonly IFactFactory mFactFactory;
        [NotNull] private readonly List<INode> mFactNodes;
        [NotNull] private readonly IRecorder mRecorder;

        public class Factory : ICaseFactory
        {
            [NotNull] private readonly IServiceSet<ICaseModel> mServices;
            [NotNull] private readonly IFactFactory mFactFactory;

            public Factory([NotNull] IServiceSet<ICaseModel> services, [NotNull] IFactFactory factFactory)
            {
                mServices = services;
                mFactFactory = factFactory;
            }

            public Case Create([NotNull] List<INode> factNodes, [NotNull] IRecorder recorder)
            {
                return new CaseImp(factNodes, recorder, mFactFactory, mServices);
            }
        }

        public CaseImp([NotNull] List<INode> factNodes,
                       [NotNull] IRecorder recorder,
                       [NotNull] IFactFactory factFactory,
                       [NotNull] IServiceSet<ICaseModel> services)
            : base(services)
        {
            if (factNodes == null) throw new ArgumentNullException("factNodes");
            if (recorder == null) throw new ArgumentNullException("recorder");
            if (factFactory == null) throw new ArgumentNullException("factFactory");
            mFactNodes = factNodes;
            mFactFactory = factFactory;
            mRecorder = recorder;
        }

        [NotNull] 
        public override ICaseModel Model
        {
            get { return this; }
        }

        [NotNull, ItemNotNull] 
        public IEnumerable<Fact> Facts
        {
            get { return mFactNodes.Select(factNode => mFactFactory.Create(factNode, mRecorder)); }
        }

        #region ICaseModel

        [NotNull] public List<INode> FactNodes
        {
            get { return mFactNodes; }
        }

        #endregion
    }
}