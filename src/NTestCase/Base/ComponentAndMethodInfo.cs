using System.Reflection;
using NTestCase.Api.Dev;

namespace NTestCase.Base
{
    public class ComponentAndMethodInfo : ITarget
    {
        private readonly object mComponent;
        private readonly MethodInfo mTarget;

        public ComponentAndMethodInfo(object component, MethodInfo target)
        {
            mComponent = component;
            mTarget = target;
        }

        #region Equals and GetHashCode
        protected bool Equals(ComponentAndMethodInfo other)
        {
            return Equals(mComponent, other.mComponent) && Equals(mTarget, other.mTarget);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ComponentAndMethodInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((mComponent != null ? mComponent.GetHashCode() : 0)*397) ^ (mTarget != null ? mTarget.GetHashCode() : 0);
            }
        }
        #endregion

    }
}