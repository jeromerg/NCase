using System;
using JetBrains.Annotations;

namespace NDsl.All
{
    public class TypeCannotBeContributorException : Exception
    {
        public TypeCannotBeContributorException([NotNull] Type type)
            : base(string.Format("Type {0} cannot be a contributor: it must be an interface or an abstract or non-sealed class", type.FullName))
        {
        }
    }
}