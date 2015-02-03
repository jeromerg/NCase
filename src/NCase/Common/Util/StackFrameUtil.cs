using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NCase.Common.Quality;

namespace NCase.Common.Util
{
    public static class StackFrameUtil 
    {
        private const string IN_MEMORY_MODULE_NAME = "<In Memory Module>";
        private static readonly Assembly[] sExcludedAssemblies;

        static StackFrameUtil()
        {
            IEnumerable<Assembly> referencedAssemblies = Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Select(assemblyName => Assembly.Load(assemblyName));
            
            var excludedAssemblies = new List<Assembly>();            
            excludedAssemblies.Add(Assembly.GetExecutingAssembly());
            excludedAssemblies.AddRange(referencedAssemblies);
            sExcludedAssemblies = excludedAssemblies.ToArray();
        }

        [CanBeNull]
        public static StackFrame GetOuterStackFrame()
        {
            var stackFrames = new StackTrace(true).GetFrames();

            if (stackFrames == null)
                return null;

            foreach (var stackFrame in stackFrames)
            {                
                MethodBase method = stackFrame.GetMethod();
                if (method == null)
                    return null;

                if (method.Module.FullyQualifiedName == IN_MEMORY_MODULE_NAME)
                    continue;

                if (sExcludedAssemblies.Any(excludedAssembly => excludedAssembly == method.Module.Assembly)) 
                    continue;
                
                return stackFrame;
            }

            return null;
        }
    }
}
