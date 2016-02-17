using System;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace NDsl.test
{
    public interface IA { }
    public class A
    {
        
    }

    [TestFixture]
    public class ProxyTests
    {
        [Test]
        public void Test()
        {
            var proxyGenerator = new ProxyGenerator();
            proxyGenerator.CreateInterfaceProxyWithoutTarget()
        }
    }
}