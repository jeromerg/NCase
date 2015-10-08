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
    public class Fact : Artefact<IFactApi>, IFact, IFactApi
    {
        private readonly INode mFactNode;

        public class Factory : IFactFactory
        {
            [NotNull]
            private readonly IToolBox<IFactApi> mToolBox;

            public Factory([NotNull] IToolBox<IFactApi> toolBox)
            {
                if (toolBox == null) throw new ArgumentNullException("toolBox");
                mToolBox = toolBox;
            }

            public IFact Create(INode fact)
            {
                return new Fact(fact, mToolBox);
            }
        }

        public Fact([NotNull] INode factNode, [NotNull] IToolBox<IFactApi> toolBox)
            : base(toolBox)
        {
            mFactNode = factNode;
        }

        public override IFactApi Api
        {
            get { return this; }
        }

        public INode FactNode
        {
            get { return mFactNode; }
        }

    }
}