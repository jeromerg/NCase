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
    public class AllCombinations_Tree_Tests
    {
        public interface IMyTestvalues
        {
            string A { get; set; }
            string B { get; set; }
            string C { get; set; }
            string D { get; set; }
        }

        [Test]
        public void Tree_AllCombinations_Test()
        {
            
        }

        [Test]
        public void AllCombinations_In_Tree_Test()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var o = caseBuilder.NewContributor<IMyTestvalues>("o");

            var all = caseBuilder.NewDefinition<AllCombinations>("all");
            using (all.Define())
            {
                o.C = "c1";

                o.D = "d1";
                o.D = "d2";
            }

            var tree = caseBuilder.NewDefinition<Tree>("age_set");
            using (tree.Define())
            {
                o.A = "a1";
                    o.B = "b1";
                    o.B = "b2";
                        all.Ref();
                o.A = "a2";
                    o.B = "b3";
            }

            var e = tree.Cases().Replay().GetEnumerator();

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", o.A);
            Assert.AreEqual("b1", o.B);
            Assert.Throws<InvalidRecPlayStateException>(() => { string s = o.C; });
            Assert.Throws<InvalidRecPlayStateException>(() => { string s = o.D; });
        }
    }
}