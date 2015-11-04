﻿using System;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.Fact;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class FactImp : ArtefactImp<IFactModel>, Fact, IFactModel
    {
        private readonly INode mFactNode;

        public class Factory : IFactFactory
        {
            [NotNull] private readonly IServiceSet<IFactModel> mServices;

            public Factory([NotNull] IServiceSet<IFactModel> services)
            {
                if (services == null) throw new ArgumentNullException("services");
                mServices = services;
            }

            public Fact Create(INode fact)
            {
                return new FactImp(fact, mServices);
            }
        }

        public FactImp([NotNull] INode factNode, [NotNull] IServiceSet<IFactModel> services)
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