using System;
using JetBrains.Annotations;
using NCaseFramework.Front.Api.Fact;
using NCaseFramework.Front.Ui;
using NDsl.Back.Api.Common;
using NDsl.Back.Api.Record;
using NDsl.Back.Api.Util;
using NDsl.Front.Imp;

namespace NCaseFramework.Front.Imp
{
    public class FactImp : ArtefactImp<IFactModel>, Fact, IFactModel
    {
        [NotNull] private readonly INode mFactNode;
        [NotNull] private readonly IRecorder mRecorder;

        public class Factory : IFactFactory
        {
            [NotNull] private readonly IServiceSet<IFactModel> mServices;

            public Factory([NotNull] IServiceSet<IFactModel> services)
            {
                if (services == null) throw new ArgumentNullException("services");
                mServices = services;
            }

            public Fact Create([NotNull] INode fact, [NotNull] IRecorder recorder)
            {
                return new FactImp(fact, recorder, mServices);
            }
        }

        public FactImp([NotNull] INode factNode, [NotNull] IRecorder recorder, [NotNull] IServiceSet<IFactModel> services)
            : base(services)
        {
            if (factNode == null) throw new ArgumentNullException("factNode");
            if (recorder == null) throw new ArgumentNullException("recorder");
            mFactNode = factNode;
            mRecorder = recorder;
        }

        [NotNull] 
        public override IFactModel Model
        {
            get { return this; }
        }

        #region IFactModel

        [NotNull] 
        public INode FactNode
        {
            get { return mFactNode; }
        }

        [NotNull] 
        public IRecorder Recorder
        {
            get { return mRecorder; }
        }

        #endregion
    }
}