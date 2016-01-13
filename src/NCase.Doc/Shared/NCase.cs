using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Autofac;
using JetBrains.Annotations;
using NCaseFramework.Front;
using NCaseFramework.Front.Api;
using NDsl;
using NDsl.Back.Api.Util;
using NDsl.Back.Imp.Util;
using NDsl.Front.Api;

namespace NCaseFramework.Doc.Shared
{
    /// <summary>
    ///     Alternative implementation of NCase static entry point, used to produce reproducible documentation:
    ///     - CodeLocation is overriden, in order to always generate the same path, independently from the real location where
    ///     code is located
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    public static class NCase
    {
        [NotNull]
        public static CaseBuilder NewBuilder([NotNull, CallerFilePath] string filePath = "")
        {
            var cb = new ContainerBuilder();

            // ReSharper disable once AssignNullToNotNullAttribute
            IEnumerable<Assembly> referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(assemblyName => Assembly.Load(assemblyName));
            var userUserStackFrameUtil = new UserStackFrameUtil(excludedAssemblies:referencedAssemblies);
            cb.RegisterModule(new NDslCoreModule(userUserStackFrameUtil));
            cb.RegisterModule<NDslRecPlayModule>();
            cb.RegisterModule<NCaseCoreModule>();
            cb.RegisterModule<NCaseInterfaceRecPlayModule>();
            cb.RegisterModule<NCaseSeqModule>();
            cb.RegisterModule<NCaseTreeModule>();
            cb.RegisterModule<NCaseCombinationsModule>();
            cb.RegisterModule<NCaseProdModule>();
            cb.RegisterModule<NCasePairwiseModule>();

            cb.RegisterGeneric(typeof (ServiceSet<>)).As(typeof (IServiceSet<>));

            // OVERRIDE
            string originalDirectoryName = Path.GetDirectoryName(filePath);

            cb.RegisterInstance(new DocCodeLocationPrinter(originalDirectoryName, DocConstants.DocSourceFolder))
              .As<ICodeLocationPrinter>();

            IContainer container = cb.Build();
            return container.Resolve<IBuilderFactory>().Create();
        }
    }

    public class DocCodeLocationPrinter : ICodeLocationPrinter
    {
        [NotNull] private readonly string mOriginalFolder;
        [NotNull] private readonly string mDisplayedDemoFolder;

        public DocCodeLocationPrinter([NotNull] string originalFolder, [NotNull] string displayedDemoFolder)
        {
            mOriginalFolder = originalFolder;
            mDisplayedDemoFolder = displayedDemoFolder;
        }

        public string Print([NotNull] CodeLocation codeLocation)
        {
            if (codeLocation == null) throw new ArgumentNullException("codeLocation");

            // ReSharper disable once PossibleNullReferenceException
            string filePath = codeLocation.FileName != null
                                  ? codeLocation.FileName.Replace(mOriginalFolder, mDisplayedDemoFolder)
                                  : "unknown file";

            return string.Format("{0}: line {1}",
                                 filePath,
                                 codeLocation.Line.HasValue ? codeLocation.Line.Value.ToString() : "??");
        }
    }
}