using System;
using JetBrains.Annotations;

namespace NDsl.All
{
    public class NoParameterlessConstructorException : Exception
    {
        public NoParameterlessConstructorException([NotNull] Type type)
            : base(string.Format("No parameterless constructor for type {0}", type.FullName))
        {
        }
    }
}