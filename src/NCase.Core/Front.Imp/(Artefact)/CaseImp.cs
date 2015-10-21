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
    public class CaseImp : Artefact<ICaseModel>, Case, ICaseModel
    {
        [NotNull] private readonly IEnumerable<INode> mFactNodes;

        public class Factory : ICaseFactory
        {
            [NotNull] private readonly IServices<ICaseModel> mServices;

            public Factory([NotNull] IServices<ICaseModel> services)
            {
                if (services == null) throw new ArgumentNullException("services");
                mServices = services;
            }

            public Case Create(IEnumerable<INode> factNodes)
            {
                return new CaseImp(factNodes, mServices);
            }
        }

        public CaseImp([NotNull] IEnumerable<INode> factNodes, [NotNull] IServices<ICaseModel> services)
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

        [NotNull] public IEnumerable<INode> FactNodes
        {
            get { return mFactNodes; }
        }

        #endregion
    }
}