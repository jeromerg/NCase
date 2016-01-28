using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Util
{
    public class UserStackFrameUtil : IUserStackFrameUtil
    {
        /// <summary>Generated classes by Castle are located in following module </summary>
        private const string IN_MEMORY_MODULE_NAME = "<In Memory Module>";

        [NotNull, ItemNotNull] private readonly HashSet<string> mExcludedModuleNames = new HashSet<string>();
        [NotNull, ItemNotNull] private readonly HashSet<Assembly> mExcludedAssemblies = new HashSet<Assembly>();
        [NotNull, ItemNotNull] private readonly HashSet<string> mExcludedNamespaces = new HashSet<string>();

        public UserStackFrameUtil(
            [ItemNotNull] IEnumerable<string> excludedModuleNames = null,
            [ItemNotNull] IEnumerable<Assembly> excludedAssemblies = null,
            [ItemNotNull] IEnumerable<string> excludedNamespaces = null)
        {
            mExcludedModuleNames.Add(IN_MEMORY_MODULE_NAME);

            if(excludedModuleNames != null)
            {
                foreach (var excludedModuleName in excludedModuleNames)
                    mExcludedModuleNames.Add(excludedModuleName);
            }

            ExcludeAssemblyAndReferenced(Assembly.GetExecutingAssembly());

            if(excludedAssemblies != null)
            {
                foreach (Assembly excludedAssembly in excludedAssemblies)
                    // ReSharper disable once AssignNullToNotNullAttribute
                    ExcludeAssemblyAndReferenced(excludedAssembly);
            }

            if(excludedNamespaces != null)
            {
                foreach (var excludedNamespace in excludedNamespaces)
                    mExcludedNamespaces.Add(excludedNamespace);
            }
        }

        public StackFrame GetUserStackFrame()
        {
            StackFrame[] stackFrames = new StackTrace(true).GetFrames();

            if (stackFrames == null)
                return null;

            foreach (StackFrame stackFrame in stackFrames)
            {
                // ReSharper disable once PossibleNullReferenceException
                MethodBase method = stackFrame.GetMethod();
                if (method == null)
                    return null;

                if (mExcludedModuleNames.Contains(method.Module.FullyQualifiedName))
                    continue;

                if (mExcludedAssemblies.Contains(method.Module.Assembly))
                    continue;

                // ReSharper disable once PossibleNullReferenceException
                if(mExcludedNamespaces.Contains(method.DeclaringType.Namespace))
                    continue;

                return stackFrame;
            }

            return null;
        }

        private void ExcludeAssemblyAndReferenced([NotNull] Assembly assembly)
        {
            IEnumerable<Assembly> assemblyAndReferenced = GetAssemblyAndReferenced(assembly);
            foreach (Assembly a in assemblyAndReferenced)
                mExcludedAssemblies.Add(a);
        }

        [NotNull, ItemNotNull]
        private IEnumerable<Assembly> GetAssemblyAndReferenced([NotNull] Assembly assembly)
        {
            yield return assembly;

            // ReSharper disable once AssignNullToNotNullAttribute
            foreach (Assembly ass in assembly.GetReferencedAssemblies().Select(assemblyName => Assembly.Load(assemblyName)))
                yield return ass;
        }
    }
}