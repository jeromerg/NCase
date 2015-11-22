using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NCaseFramework.Front.Ui;
using NCaseFramework.Test.Util;
using NDsl.Back.Api.Ex;
using NDsl.Front.Api;
using NDsl.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Test
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