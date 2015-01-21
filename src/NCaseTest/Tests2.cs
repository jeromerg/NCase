﻿using System;
using System.Diagnostics;
using System.Reflection;
using NCase.Util;
using NCase.Util.Quality;
using NUnit.Framework;

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
            //GetOuterStackFrame();
            StackFrameUtil.GetOuterStackFrame();
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