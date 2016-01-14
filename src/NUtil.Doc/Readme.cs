using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NDocUtil;
using NUnit.Framework;

namespace NCaseFramework.Doc
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class Readme
    {
        // ReSharper disable once InconsistentNaming
        [NotNull] private readonly DocUtil docu = new DocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void NCaseTree()
        {
            //# NCaseTree
            //#

            docu.BeginRecordConsole("Visualize_Def_Console");
            docu.StopRecordConsole();
        }

    }

}