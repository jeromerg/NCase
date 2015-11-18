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

        [NotNull] public abstract string TypeName { get; }

        [NotNull] 
        public virtual string Name
        {
            get { return mName; }
        }

        public override string ToString()
        {
            return string.Format("Definition '{0}' of type '{1}'", Name, TypeName);
        }
    }
}