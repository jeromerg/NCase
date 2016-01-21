using System;
using NDocUtilLibrary;
using NUnit.Framework;

namespace NUtil.Doc
{
    [TestFixture]
    public class Readme
    {
        private readonly NDocUtil docu = new NDocUtil();

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            //# SaveSnippetsAsRaw
            docu.SaveSnippetsAsRaw();
            //#

            //# SaveSnippetsAsRaw2
            docu.SaveSnippetsAsRaw(fileExtension:".txt");
            //#

            //# SaveSnippetsAsHtml
            docu.SaveSnippetsAsHtml();
            //#

            //# SaveSnippetsAsHtml2
            docu.SaveSnippetsAsHtml(htmlSnippetDecorator: "{0}", fileExtension:".html");
            //#

            //# SaveSnippetsAsImage
            docu.SaveSnippetsAsImage(ImageFormat.Png);
            //#

            //# SaveSnippetsAsImage2
            docu.SaveSnippetsAsImage(ImageFormat.Emf);
            //#

            docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void PairwiseGenerator()
        {
            docu.BeginRecordConsole("MY_CONSOLE_SNIPPET");

            //# MY_CODE_SNIPPET
            var someDate = new DateTime(2011, 11, 11, 11, 11, 11);
            Console.WriteLine(someDate.ToString("o"));
            //#

            docu.StopRecordConsole();
        }
    }

}