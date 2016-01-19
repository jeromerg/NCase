using System;
using System.Drawing.Imaging;
using NDocUtilLibrary;
using NUnit.Framework;

namespace NUtil.Doc
{
    [TestFixture]
    public class MyDocumentation
    {
        private readonly NDocUtil docu = new NDocUtil("docu");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
        }

        [Test]
        public void PairwiseGenerator()
        {
            //# MY_CODE_SNIPPET
            var now = DateTime.Now; // this line will be included
            var this_Row_Will_Be_Hidden = "as it contains the regex tag 'docu'";
            //#

            docu.BeginRecordConsole("MY_CONSOLE_SNIPPET");
            Console.WriteLine("This line will be exported into the console snippet");
            docu.StopRecordConsole();
        }
    }

}