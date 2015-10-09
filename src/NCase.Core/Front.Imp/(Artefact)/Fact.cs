using System;
using JetBrains.Annotations;
using NCase.Front.Api;
using NCase.Front.Ui;
using NDsl.All;
using NDsl.Back.Api.Core;
using NDsl.Front.Imp;

namespace NCase.Front.Imp
{
    public class Fact : Artefact<IFactModel>, IFact, IFactModel
    {
        private readonly INode mFactNode;

        public class Factory : IFactFactory
        {
            [NotNull] private readonly IServices<IFactModel> mServices;

            public Factory([NotNull] IServices<IFactModel> services)
            {
                if (services == null) throw new ArgumentNullException("services");
                mServices = services;
            }

            public IFact Create(INode fact)
            {
                return new Fact(fact, mServices);
            }
        }

        public Fact([NotNull] INode factNode, [NotNull] IServices<IFactModel> services)
            : base(services)
        {
            mFactNode = factNode;
        }

        public override IFactModel Model
        {
            get { return this; }
        }

        #region IFactModel

        public INode FactNode
        {
            get { return mFactNode; }
        }

        #endregion
    }
}