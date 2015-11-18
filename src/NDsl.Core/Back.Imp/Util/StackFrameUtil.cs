using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using NDsl.Back.Api.Util;

namespace NDsl.Back.Imp.Util
{
    public class StackFrameUtil : IStackFrameUtil
    {
        private const string IN_MEMORY_MODULE_NAME = "<In Memory Module>";
        [NotNull] private readonly List<Assembly> mExcludedAssemblies = new List<Assembly>();

        /// <param name="assembliesToIgnore">
        ///     the assemblies to filter out as they don't belong to user code.
        ///     Their dependencies will be ignored either
        /// </param>
        public StackFrameUtil([NotNull, ItemNotNull] params Assembly[] assembliesToIgnore)
        {
            if (assembliesToIgnore == null) throw new ArgumentNullException("assembliesToIgnore");

            // In any case remove the assembly executing this code and its dependencies
            mExcludedAssemblies.AddRange(GetAssemblyAndReferenced(Assembly.GetExecutingAssembly()));

            foreach (Assembly assembly in assembliesToIgnore)
                mExcludedAssemblies.AddRange(GetAssemblyAndReferenced(assembly));
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

                if (method.Module.FullyQualifiedName == IN_MEMORY_MODULE_NAME)
                    continue;

                if (mExcludedAssemblies.Any(excludedAssembly => excludedAssembly == method.Module.Assembly))
                    continue;

                return stackFrame;
            }

            return null;
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