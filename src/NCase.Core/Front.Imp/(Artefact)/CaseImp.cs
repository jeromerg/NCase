using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Api.Fact;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class CaseImp : ArtefactImp<ICaseModel>, Case, ICaseModel
    {
        [NotNull] private readonly List<INode> mFactNodes;
        private readonly IFactFactory mFactFactory;

        public class Factory : ICaseFactory
        {
            [NotNull] private readonly IServiceSet<ICaseModel> mServices;
            private readonly IFactFactory mFactFactory;

            public Factory([NotNull] IServiceSet<ICaseModel> services, [NotNull] IFactFactory factFactory)
            {
                if (services == null) throw new ArgumentNullException("services");
                if (factFactory == null) throw new ArgumentNullException("factFactory");
                mServices = services;
                mFactFactory = factFactory;
            }

            public Case Create(List<INode> factNodes)
            {
                return new CaseImp(factNodes, mServices, mFactFactory);
            }
        }

        public CaseImp([NotNull] List<INode> factNodes,
                       [NotNull] IServiceSet<ICaseModel> services,
                       [NotNull] IFactFactory factFactory)
            : base(services)
        {
            if (factNodes == null) throw new ArgumentNullException("factNodes");
            if (factFactory == null) throw new ArgumentNullException("factFactory");
            mFactNodes = factNodes;
            mFactFactory = factFactory;
        }

        public override ICaseModel Model
        {
            get { return this; }
        }

        public IEnumerable<Fact> Facts
        {
            get { return mFactNodes.Select(factNode => mFactFactory.Create(factNode)); }
        }

        #region ICaseModel

        [NotNull] public List<INode> FactNodes
        {
            get { return mFactNodes; }
        }

        #endregion
    }
}