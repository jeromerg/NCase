using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NCaseFramework.Front.Ui;
using NDsl.Front.Api;
using NDsl.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Test
{
    [TestFixture]
    public class CombinationsTests
    {
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public interface IContrib
        {
            string A { get; set; }
            string B { get; set; }
            string C { get; set; }
            string D { get; set; }
            string E { get; set; }
        }

        [Test]
        public void UnionTest()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var c = caseBuilder.NewContributor<IContrib>("c");
            var set = caseBuilder.NewCombinationSet("set");

            using (set.Define())
            {
                c.A = "a1";
                c.A = "a2";
            }

            IEnumerator<Case> e = set.Cases().Replay().GetEnumerator();

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a2", c.A);

            Assert.AreEqual(false, e.MoveNext());
        }

        [Test]
        public void ProductTest()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var c = caseBuilder.NewContributor<IContrib>("c");
            var set = caseBuilder.NewCombinationSet("set");

            using (set.Define())
            {
                c.A = "a1";

                c.B = "b1";
            }

            IEnumerator<Case> e = set.Cases().Replay().GetEnumerator();

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b1", c.B);

            Assert.AreEqual(false, e.MoveNext());
        }

        [Test]
        public void ProductTest2()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var c = caseBuilder.NewContributor<IContrib>("c");
            var set = caseBuilder.NewCombinationSet("set");

            using (set.Define())
            {
                c.A = "a1";
                c.A = "a2";

                c.B = "b1";
                c.B = "b2";
            }

            IEnumerator<Case> e = set.Cases().Replay().GetEnumerator();

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b1", c.B);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b2", c.B);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a2", c.A);
            Assert.AreEqual("b1", c.B);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a2", c.A);
            Assert.AreEqual("b2", c.B);

            Assert.AreEqual(false, e.MoveNext());
        }

        [Test]
        public void TreeTest()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var c = caseBuilder.NewContributor<IContrib>("c");
            var set = caseBuilder.NewCombinationSet("set");

            using (set.Define())
            {
                c.A = "a1"; 
                    c.B = "b1";
                c.A = "a2";
                    c.B = "b2";
            }

            IEnumerator<Case> e = set.Cases().Replay().GetEnumerator();

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b1", c.B);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a2", c.A);
            Assert.AreEqual("b2", c.B);

            Assert.AreEqual(false, e.MoveNext());
        }

        [Test]
        public void TreeTest2()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var c = caseBuilder.NewContributor<IContrib>("c");
            var set = caseBuilder.NewCombinationSet("set");

            using (set.Define())
            {
                c.A = "a1";
                {
                    c.B = "b1";
                }
                c.A = "a2";
                {
                    c.B = "b2";
                }
            }

            IEnumerator<Case> e = set.Cases().Replay().GetEnumerator();

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b1", c.B);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a2", c.A);
            Assert.AreEqual("b2", c.B);

            Assert.AreEqual(false, e.MoveNext());
        }

        [Test]
        public void TreeAndProductTest()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var c = caseBuilder.NewContributor<IContrib>("c");
            var set = caseBuilder.NewCombinationSet("set");

            using (var o = set.Define())
            {
                c.A = "a1";
                {
                    c.B = "b1";
                    c.B = "b2";
                }

                c.C = "c1";
                {
                    c.D = "d1";
                    c.D = "d2";
                }
            }

            IEnumerator<Case> e = set.Cases().Replay().GetEnumerator();

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b1", c.B);
            Assert.AreEqual("c1", c.C);
            Assert.AreEqual("d1", c.D);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b1", c.B);
            Assert.AreEqual("c1", c.C);
            Assert.AreEqual("d2", c.D);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b2", c.B);
            Assert.AreEqual("c1", c.C);
            Assert.AreEqual("d1", c.D);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b2", c.B);
            Assert.AreEqual("c1", c.C);
            Assert.AreEqual("d2", c.D);

            Assert.AreEqual(false, e.MoveNext());
        }

        [Test]
        public void ComplexTreeAndProductTest()
        {
            CaseBuilder caseBuilder = NCase.NewBuilder();
            var c = caseBuilder.NewContributor<IContrib>("c");
            var set = caseBuilder.NewCombinationSet("set");

            using (var d = set.Define())
            {
                c.A = "a1";
                {
                    d.Branch();
                    {
                        c.D = "d1";
                        {
                            c.B = "b1";
                            {
                                c.C = "c1";
                            }
                            c.B = "b2";
                            {
                                c.C = "c2";
                                c.C = "c3";
                            }
                        }

                        c.E = "e1";
                        c.E = "e2";
                    }
                    d.Branch();
                    {
                        c.B = "b3";
                        {
                            c.C = "c4";
                            c.C = "c5";

                            c.D = "d2";
                            c.D = "d3";
                        }

                        c.E = "e3";
                        c.E = "e4";
                    }
                }
            }

            IEnumerator<Case> e = set.Cases().Replay().GetEnumerator();

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("d1", c.D);
            Assert.AreEqual("b1", c.B);
            Assert.AreEqual("c1", c.C);
            Assert.AreEqual("e1", c.E);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("d1", c.D);
            Assert.AreEqual("b1", c.B);
            Assert.AreEqual("c1", c.C);
            Assert.AreEqual("e2", c.E);


            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("d1", c.D);
            Assert.AreEqual("b2", c.B);
            Assert.AreEqual("c2", c.C);
            Assert.AreEqual("e1", c.E);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("d1", c.D);
            Assert.AreEqual("b2", c.B);
            Assert.AreEqual("c2", c.C);
            Assert.AreEqual("e2", c.E);


            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("d1", c.D);
            Assert.AreEqual("b2", c.B);
            Assert.AreEqual("c3", c.C);
            Assert.AreEqual("e1", c.E);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("d1", c.D);
            Assert.AreEqual("b2", c.B);
            Assert.AreEqual("c3", c.C);
            Assert.AreEqual("e2", c.E);

            //-- next branch

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b3", c.B);
            Assert.AreEqual("c4", c.C);
            Assert.AreEqual("d2", c.D);
            Assert.AreEqual("e3", c.E);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b3", c.B);
            Assert.AreEqual("c4", c.C);
            Assert.AreEqual("d2", c.D);
            Assert.AreEqual("e4", c.E);


            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b3", c.B);
            Assert.AreEqual("c4", c.C);
            Assert.AreEqual("d3", c.D);
            Assert.AreEqual("e3", c.E);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b3", c.B);
            Assert.AreEqual("c4", c.C);
            Assert.AreEqual("d3", c.D);
            Assert.AreEqual("e4", c.E);


            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b3", c.B);
            Assert.AreEqual("c5", c.C);
            Assert.AreEqual("d2", c.D);
            Assert.AreEqual("e3", c.E);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b3", c.B);
            Assert.AreEqual("c5", c.C);
            Assert.AreEqual("d2", c.D);
            Assert.AreEqual("e4", c.E);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b3", c.B);
            Assert.AreEqual("c5", c.C);
            Assert.AreEqual("d3", c.D);
            Assert.AreEqual("e3", c.E);

            Assert.AreEqual(true, e.MoveNext());
            Assert.AreEqual("a1", c.A);
            Assert.AreEqual("b3", c.B);
            Assert.AreEqual("c5", c.C);
            Assert.AreEqual("d3", c.D);
            Assert.AreEqual("e4", c.E);

            Assert.AreEqual(false, e.MoveNext());
        }
    }
}