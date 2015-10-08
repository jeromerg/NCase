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
    public class Case : Artefact<ICaseApi>, ICase, ICaseApi
    {
        [NotNull] private readonly IEnumerable<INode> mFactNodes;

        public class Factory : ICaseFactory
        {
            [NotNull] private readonly IToolBox<ICaseApi> mToolBox;

            public Factory([NotNull] IToolBox <ICaseApi> toolBox)
            {
                if (toolBox == null) throw new ArgumentNullException("toolBox");
                mToolBox = toolBox;
            }

            public ICase Create(IEnumerable<INode> factNodes)
            {
                return new Case(factNodes, mToolBox);
            }
        }

        public Case([NotNull] IEnumerable<INode> factNodes, [NotNull] IToolBox<ICaseApi> toolBox)
            : base(toolBox)
        {
            if (factNodes == null) throw new ArgumentNullException("factNodes");
            mFactNodes = factNodes;
        }

        public override ICaseApi Api
        {
            get { return this; }
        }

        [NotNull] public IEnumerable<INode> FactNodes
        {
            get { return mFactNodes; }
        }
    }
}