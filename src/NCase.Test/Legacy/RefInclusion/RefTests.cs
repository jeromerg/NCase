using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace NCaseFramework.Test.RefInclusion
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class RefTests
    {
        public interface IMyTestvalues
        {
            string A { get; set; }
            string B { get; set; }
            string C { get; set; }
            string D { get; set; }
        }

        // TODO: Ref before definition
    }
}