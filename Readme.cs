using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NCaseFramework.Front.Ui;
using NDocUtilLibrary;
using NDsl.Front.Ui;
using NUnit.Framework;

namespace NCaseFramework.Doc
{
    // remark: must remain here, in order to redirect NCase calls to the doc-specific implementation
    using Shared;
    
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class Readme
    {
        // ReSharper disable once InconsistentNaming
        [NotNull] private readonly NDocUtil docu = new NDocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            //docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void DemoTest()
        {
            
        }

   }

}