using System;
using NUnit.Framework;

namespace NDsl.test
{
    [TestFixture]
    public class StackTraceTests
    {
        [Test]
        public void PrintUrl()
        {
            // remark: just used, to perform test in IDE
            Console.WriteLine(@"C:\temp\aeff.txt");
            Console.WriteLine(@"file://C:/Temp/aef.txt");
            Console.WriteLine(@"file://D:/src/test/NCase/src/NDslTest/StackTraceTests.cs");
        }

        [Test]
        public void PrintUsualStrackTraceTest()
        {
            // remark: just used, to perform test in IDE
            try
            {
                throw new ApplicationException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}