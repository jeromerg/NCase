using System;

namespace NCaseFramework.Back.Api.Combinations
{
    public class CompilationException : Exception
    {
        public CompilationException(string msg)
            : base(msg)
        {
        }
    }
}