using System;
using JetBrains.Annotations;

namespace NDsl.Back.Api.Core
{
    public abstract class DefId : IDefId
    {
        [NotNull] private readonly string mName;
        private string mDefTypeName;

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