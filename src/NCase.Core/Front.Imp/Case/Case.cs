using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NCase.Front.Api.Case;
using NCase.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCase.Front.Imp.Case
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