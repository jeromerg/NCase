using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api
{
    public abstract class DefId : IDefId
    {
        [NotNull] private readonly string mName;

        protected DefId([NotNull] string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            mName = name;
        }

        public virtual string Name
        {
            get { return mName; }
        }
    }
}