using System;
using JetBrains.Annotations;
using NDsl.All.Def;

namespace NDsl.Back.Api.Def
{
    public abstract class DefId : IDefId
    {
        private static int sAnonymousDefCounter;

        [NotNull] private readonly string mName;

        protected DefId()
        {
            mName = "anonymous_def" + sAnonymousDefCounter;
            sAnonymousDefCounter++;
        }

        protected DefId([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            mName = name;
        }

        public abstract string DefTypeName { get; }

        public virtual string Name
        {
            get { return mName; }
        }
    }
}