using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public class Case : Artefact<ICaseModel>, ICase, ICaseModel
    {
        [NotNull] private readonly IEnumerable<INode> mFactNodes;

        public Case([NotNull] IEnumerable<INode> factNodes, [NotNull] IServices<ICaseModel> services)
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