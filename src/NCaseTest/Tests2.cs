using System.Diagnostics;
using System.Reflection;
using NDsl.Impl.Core;
using NDsl.Impl.Core.Util;
using NDsl.Util;
using NUnit.Framework;
using NVisitor.Common.Quality;

namespace NCaseTest
{
    [TestFixture]
    public class Tests2
    {
        public class A
        {
            private string mName;

            public string Name
            {
                get { return mName; }
                set { mName = value; }
            }

            public string this[int index]
            {
                get { return "" + index; }
                set { }
            }
        }

        [Test]
        public void Test()
        {
            var type = typeof (A);
            var methodInfo = type.GetMethod("set_Item");

        }

        [Test]
        public void Test2()
        {
            //GetUserStackFrame();
            new StackFrameUtil().GetUserStackFrame();
        }

        [CanBeNull]
        public static StackFrame GetOuterStackFrame()
        {
            for (int i = 1; ; i++)
            {
                var stackFrame = new StackFrame(i, true);
                MethodBase method = stackFrame.GetMethod();
                if (method == null)
                    return null;

            }
        }
    }
}
