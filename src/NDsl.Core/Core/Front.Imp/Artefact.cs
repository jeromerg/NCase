using System;
using JetBrains.Annotations;
using NDsl.All;
using NDsl.Front.Ui;

namespace NDsl.Front.Imp
{
    public abstract class Artefact : IArtefact, IArtefactApi
    {
        public IArtefactApi Api
        {
            get { return this; }
        }

        public abstract IToolBox<IArtefactApi> Toolbox
        {
            get { throw new NotImplementedException(); }
        }
    }
}