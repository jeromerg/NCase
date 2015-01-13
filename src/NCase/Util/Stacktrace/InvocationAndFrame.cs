using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using NTestCase.Util.Quality;

namespace NTestCase.Util.Stacktrace
{
    public class InvocationAndFrame
    {
        private static readonly Assembly[] sExcludedAssemblies;
        private readonly IInvocation mInvocation;
        [CanBeNull]
        private readonly StackFrame mStackFrame;

        static InvocationAndFrame()
        {
            IEnumerable<Assembly> referencedAssemblies = Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(assemblyName => Assembly.Load(assemblyName));
            
            var excludedAssemblies = new List<Assembly>();            
            excludedAssemblies.Add(Assembly.GetExecutingAssembly());
            excludedAssemblies.AddRange(referencedAssemblies);
            sExcludedAssemblies = excludedAssemblies.ToArray();
        }

        public InvocationAndFrame(IInvocation invocation)
        {
            mInvocation = invocation;
            mStackFrame = GetFirstStackframeOutsideNTestCase();
        }

        public IInvocation Invocation
        {
            get { return mInvocation; }
        }

        public StackFrame StackFrame
        {
            get { return mStackFrame; }
        }

        [CanBeNull]
        private StackFrame GetFirstStackframeOutsideNTestCase()
        {
            for (int i = 1; ; i++)
            {
                var stackFrame = new StackFrame(i, true);
                MethodBase method = stackFrame.GetMethod();
                if (method == null)
                    break;

                if (sExcludedAssemblies.All(excludedAssembly => excludedAssembly != method.Module.Assembly))
                    return stackFrame;
            }

            return null;
        }
    }
}
