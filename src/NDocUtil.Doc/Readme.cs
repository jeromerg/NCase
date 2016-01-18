using System;
using System.Drawing.Imaging;
using NDocUtil;
using NUnit.Framework;

namespace NUtil.Doc
{
    [TestFixture]
    public class MyDocumentation
    {
        private readonly DocUtil docu = new DocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
            // -- OR --
            docu.SaveSnippetsAsImage(ImageFormat.Emf);
            // -- OR --
            docu.SaveSnippets();
        }

        [Test]
        public void PairwiseGenerator()
        {
            //# MY_CODE_SNIPPET
            var now = DateTime.Now;
            //#

            docu.BeginRecordConsole("MY_CONSOLE_SNIPPET");
            Console.WriteLine("This line will be exported into the console snippet");
            docu.StopRecordConsole();
        }
    }

}