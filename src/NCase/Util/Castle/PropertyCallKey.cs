using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace NCase.Util.Castle
{
    public class PropertyCallKey
    {
        private readonly string mPropertyName;
        private readonly object[] mIndexParameters;

        public PropertyCallKey(IInvocation invocation, PropertyInfo propertyInfo)
        {
            mPropertyName = propertyInfo.Name;
            mIndexParameters = invocation.Arguments.Take(propertyInfo.GetIndexParameters().Length).ToArray();
        }

        protected bool Equals(PropertyCallKey other)
        {
            return string.Equals(mPropertyName, other.mPropertyName) && Equals(mIndexParameters, other.mIndexParameters);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PropertyCallKey) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((mPropertyName != null ? mPropertyName.GetHashCode() : 0)*397) ^ (mIndexParameters != null ? mIndexParameters.GetHashCode() : 0);
            }
        }
    }
}
