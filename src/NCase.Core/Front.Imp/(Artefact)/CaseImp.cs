using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.Case;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class CaseImp : ArtefactImp<ICaseModel>, Case, ICaseModel
    {
        [NotNull] private readonly List<INode> mFactNodes;

        public class Factory : ICaseFactory
        {
            [NotNull] private readonly IServiceSet<ICaseModel> mServices;

            public Factory([NotNull] IServiceSet<ICaseModel> services)
            {
                if (services == null) throw new ArgumentNullException("services");
                mServices = services;
            }

            public Case Create(List<INode> factNodes)
            {
                return new CaseImp(factNodes, mServices);
            }
        }

        public CaseImp([NotNull] List<INode> factNodes, [NotNull] IServiceSet<ICaseModel> services)
            : base(services)
        {
            if (factNodes == null) throw new ArgumentNullException("factNodes");
            mFactNodes = factNodes;
        }

        public override ICaseModel Model
        {
            get { return this; }
        }

        #region ICaseModel

        [NotNull] public List<INode> FactNodes
        {
            get { return mFactNodes; }
        }

        #endregion
    }
}