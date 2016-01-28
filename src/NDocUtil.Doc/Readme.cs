using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NDocUtilLibrary;
using NUnit.Framework;

namespace NUtil.Doc
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Readme
    {
        [NotNull]
        private readonly NDocUtil docu = new NDocUtil();

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            //# SaveSnippetsAsRaw
            docu.SaveSnippetsAsRaw();
            //#

            //# SaveSnippetsAsRaw2
            docu.SaveSnippetsAsRaw(path:"anotherPath", fileExtension:".txt");
            //#

            //# SaveSnippetsAsHtml
            docu.SaveSnippetsAsHtml();
            //#

            //# SaveSnippetsAsHtml2
            docu.SaveSnippetsAsHtml(htmlSnippetDecorator: "{0}", path:"anotherPath", fileExtension:".htm");
            //#

            //# SaveSnippetsAsImage
            docu.SaveSnippetsAsImage(ImageFormat.Png);
            //#

            //# SaveSnippetsAsImage2
            docu.SaveSnippetsAsImage(ImageFormat.Png, path:"anotherPath");
            //#

            //# SaveSnippetsAsImage3
            docu.SaveSnippetsAsImage(ImageFormat.Emf, path:"pathToPowerpointFile");
            //#

            docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void MyCodeAndConsoleSnippets()
        {
            docu.BeginRecordConsole("MY_CONSOLE_SNIPPET");

            //# MY_CODE_SNIPPET
            var someDate = new DateTime(2011, 11, 11, 11, 11, 11);
            Console.WriteLine(someDate.ToString("o"));
            //#

            docu.StopRecordConsole();

            const string dummy = @"
                //# SNIPPET
                at least one character here!
                //#
                ";
        }
    }

}