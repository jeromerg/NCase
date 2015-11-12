using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using JetBrains.Annotations;

namespace NDsl.Back.Api.RecPlay
{
    public class PropertyCallKey
    {
        [NotNull] private readonly object mOwningProxy;
        [NotNull] private readonly string mPropertyName;
        [NotNull] private readonly object[] mIndexParameters;

        public PropertyCallKey([NotNull] IInvocation invocation, [NotNull] PropertyInfo propertyInfo)
        {
            if (invocation == null)
                throw new ArgumentNullException("invocation");

            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            if (invocation.Proxy == null)
                throw new ArgumentException("invocation.Proxy is null");

            mOwningProxy = invocation.Proxy;
            mPropertyName = propertyInfo.Name;
            mIndexParameters = invocation.Arguments.Take(propertyInfo.GetIndexParameters().Length).ToArray();
        }

        [NotNull] public object OwningProxy
        {
            get { return mOwningProxy; }
        }

        public string PropertyName
        {
            get { return mPropertyName; }
        }

        public object[] IndexParameters
        {
            get { return mIndexParameters; }
        }

        public override string ToString()
        {
            return this.PrintMember();
        }

        #region Equals and GetHashCode

        private bool Equals(PropertyCallKey other)
        {
            bool equal = Equals(mOwningProxy, other.mOwningProxy);
            if (!equal)
                return false;

            equal = string.Equals(mPropertyName, other.mPropertyName);
            if (!equal)
                return false;

            if (mIndexParameters.Length != other.mIndexParameters.Length)
                return false;

            for (int i = 0; i < mIndexParameters.Length; i++)
            {
                if (!Equals(mIndexParameters[i], other.mIndexParameters[i]))
                    return false;
            }
            return true;
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
                int i = (mPropertyName.GetHashCode()*397) ^ (1 + mIndexParameters.Length);
                return mIndexParameters.Where(p => p != null).Aggregate(i, (agg, p) => agg ^ p.GetHashCode());
            }
        }

        #endregion
    }
}