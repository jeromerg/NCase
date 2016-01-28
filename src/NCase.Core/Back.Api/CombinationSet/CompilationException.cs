using System;

namespace NCaseFramework.Back.Api.CombinationSet
{
    public class CompilationException : Exception
    {
        public CompilationException(string msg)
            : base(msg)
        {
        }
    }
}