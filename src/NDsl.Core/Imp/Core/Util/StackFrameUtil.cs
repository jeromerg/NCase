using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NDsl.Api.Dev.Core.Util;

namespace NDsl.Imp.Core.Util
{
    public class StackFrameUtil : IStackFrameUtil
    {
        private const string IN_MEMORY_MODULE_NAME = "<In Memory Module>";
        private readonly List<Assembly> mExcludedAssemblies = new List<Assembly>();

        /// <param name="assembliesToIgnore">
        /// the assemblies to filter out as they don't belong to user code. 
        /// Their dependencies will be ignored either
        /// </param>
        public StackFrameUtil(params Assembly[] assembliesToIgnore)
        {
            // In any case remove the assembly executing this code and its dependencies
            mExcludedAssemblies.AddRange(GetAssemblyAndReferenced(Assembly.GetExecutingAssembly()));

            foreach (var assembly in assembliesToIgnore)
                mExcludedAssemblies.AddRange(GetAssemblyAndReferenced(assembly));
        }

        public StackFrame GetUserStackFrame()
        {
            StackFrame[] stackFrames = new StackTrace(true).GetFrames();

            if (stackFrames == null)
                return null;

            foreach (var stackFrame in stackFrames)
            {                
                MethodBase method = stackFrame.GetMethod();
                if (method == null)
                    return null;

                if (method.Module.FullyQualifiedName == IN_MEMORY_MODULE_NAME)
                    continue;

                if (mExcludedAssemblies.Any(excludedAssembly => excludedAssembly == method.Module.Assembly)) 
                    continue;
                
                return stackFrame;
            }

            return null;
        }

        private IEnumerable<Assembly> GetAssemblyAndReferenced(Assembly assembly)
        {
            yield return assembly;
            foreach (var ass in assembly.GetReferencedAssemblies().Select(n => Assembly.Load(n)))
                yield return ass;
        }
    }
}
