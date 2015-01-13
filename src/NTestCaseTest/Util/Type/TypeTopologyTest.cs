using System;
using System.Collections.Generic;
using NTestCase.Util.Typ;
using NUnit.Framework;

namespace NTestCaseTest.Base.Helper
{
    [TestFixture]
    public class TypeTopologyTests
    {
        public class O {}
        public class A {}
        public class B : A {}

        public class X { }
        public class Y : X { }
        public class Z : Y { }

        // ReSharper disable InconsistentNaming
        public class IC1 { }
        public interface IC2 { }
        public class C : IC1, IC2 { }
        // ReSharper restore InconsistentNaming

        [Test]
        public void TestO()
        {
            var topo = new TypeTopology(typeof (O));
            Type result = topo.ResolveBestUnambiguousTargetType(new HashSet<Type> {typeof (O)});

            Assert.AreEqual(typeof(O), result);
        }

        [Test, ExpectedException(typeof(TargetTypeNotResolvedException))]
        public void TestO_A()
        {
            var topo = new TypeTopology(typeof (O));
            Type result = topo.ResolveBestUnambiguousTargetType(new HashSet<Type> {typeof (A)});

            Assert.AreEqual(null, result);
        }

        [Test, ExpectedException(typeof(TargetTypeNotResolvedException))]
        public void TestA_B()
        {
            var topo = new TypeTopology(typeof (A));
            Type result = topo.ResolveBestUnambiguousTargetType(new HashSet<Type> {typeof (B)});

            Assert.AreEqual(null, result);
        }

        [Test]
        public void TestB_A()
        {
            var topo = new TypeTopology(typeof (B));
            Type result = topo.ResolveBestUnambiguousTargetType(new HashSet<Type> {typeof (A)});

            Assert.AreEqual(typeof(A), result);
        }

        [Test]
        public void TestZ_X_Y()
        {
            var topo = new TypeTopology(typeof (Z));
            Type result = topo.ResolveBestUnambiguousTargetType(new HashSet<Type> {typeof (Y), typeof (X)});

            Assert.AreEqual(typeof(Y), result);
        }


        [Test, ExpectedException(typeof(TargetTypeNotResolvedException))]
        public void TestC_IC1_IC2()
        {
            var topo = new TypeTopology(typeof (C));
            Type result = topo.ResolveBestUnambiguousTargetType(new HashSet<Type> {typeof (IC1), typeof (IC2)});

            Assert.AreEqual(null, result);
        }

        [Test]
        public void TestC_IC1_IC2_C()
        {
            var topo = new TypeTopology(typeof (C));
            Type result = topo.ResolveBestUnambiguousTargetType(new HashSet<Type> {typeof (IC1), typeof (IC2), typeof (C)});

            Assert.AreEqual(typeof(C), result);
        }


    }
}