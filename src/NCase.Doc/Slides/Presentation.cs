using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Imaging;
using JetBrains.Annotations;
using NDocUtil;
using NUnit.Framework;

namespace NCaseFramework.Doc.Slides
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public class Presentation
    {
        [NotNull] private readonly DocUtil docu = new DocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.SaveSnippetsAsImage(ImageFormat.Wmf);
        }

        //# CodeSnippet
        public interface ITodo
        {
            string Title { get; set; }
            DateTime DueDate { get; set; }
            bool IsDone { get; set; }
        }
        //#

        [Test]
        public void OutputTest()
        {
            docu.BeginRecordConsole("ConsoleSnippet");
            Console.WriteLine("This is a line written into the Console");
            docu.StopRecordConsole();
        }

    }
}