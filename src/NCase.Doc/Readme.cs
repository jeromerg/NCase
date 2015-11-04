using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Ui;
using NDsl.Front.Api;
using NUnit.Framework;
using NUtil.Doc;

namespace NCaseFramework.doc
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Readme
    {
        private readonly DocUtil mDocUtil = new DocUtil("mDocUtil");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            mDocUtil.UpdateDocAssociatedToThisFile();
        }

        #region inner types
        public enum Architecture { x86, x64, arm }
        public struct Size
        {
            int Horizontal {get;set;}
            int Vertical {get;set;}

            public Size(int horizontal, int vertical)
                : this()
            {
                Horizontal = horizontal;
                Vertical = vertical;
            }
        }

        public enum Os
        {
            Ios7,
            Ios8,
            Android5,
            Android6,
            WindowsMobile8,
            WindowsMobile10,
            Windows7,
            OsX
        }

        public enum Browser
        {
            Chrome,
            Safari,
            Firefox,
            Dolphin
        }

        public interface IHardware
        {
            Architecture Architecture { get; set; }
            int RamInGb { get; set; }
            int HardDriveInGb { get; set; }
            Size ScreenResolution { get; set; }
        }
        
        public interface ISoftware
        {
            Os Os { get; set; }
            Browser Browser { get; set; }
            bool IsFacebookInstalled { get; set; }
            bool IsTwitterInstalled { get; set; }
        }

        public interface IUser
        {
            string UserName { get; set; }
            string Password { get; set; }
            int Age { get; set; }
        }

        private Environment GetHardwareAndSoftwareEnvironment(IHardware hw, ISoftware sw)
        {
            return new Environment();
        }

        internal class Environment
        {
            public SignInPage GetSignInPage()
            {
                return new SignInPage();
            }
        }

        internal class SignInPage
        {
            public void FillInForm(IUser user)
            {
            }
        }
        #endregion

        [Test]
        public void Demo()
        {
            IBuilder builder = NCase.NewBuilder();

            var sw = builder.NewContributor<ISoftware>("sw");

            //# AllCombinations
            var swSet = builder.NewDefinition<AllCombinations>("swSet");
            using (swSet.Define())
            {
                sw.Os = Os.Ios7;
                sw.Os = Os.Ios8;
                sw.Os = Os.Android5;
                sw.Os = Os.Android6;
                sw.Os = Os.WindowsMobile8;
                sw.Os = Os.WindowsMobile10;
                sw.Os = Os.OsX;
                sw.Os = Os.Windows7;

                sw.Browser = Browser.Chrome;
                sw.Browser = Browser.Firefox;
                sw.Browser = Browser.Safari;
                sw.Browser = Browser.Dolphin;

                sw.IsFacebookInstalled = false;
                sw.IsFacebookInstalled = true;
            }
            //#

            var hw = builder.NewContributor<IHardware>("hw");

            //# PairwiseCombinations
            var hwSet = builder.NewDefinition<PairwiseCombinations>("hwSet");
            using (hwSet.Define())
            {
                hw.Architecture = Architecture.arm;
                hw.Architecture = Architecture.x64;
                hw.Architecture = Architecture.x86;

                hw.HardDriveInGb = 10;
                hw.HardDriveInGb = 20;
                hw.HardDriveInGb = 50;

                hw.RamInGb = 1;
                hw.RamInGb = 2;
                hw.RamInGb = 5;

                hw.ScreenResolution = new Size(480, 320);
                hw.ScreenResolution = new Size(320, 480);
                hw.ScreenResolution = new Size(960, 640);
                hw.ScreenResolution = new Size(640, 960);
                hw.ScreenResolution = new Size(1136, 640);
                hw.ScreenResolution = new Size(640, 1136);
            }
            //#

            var user = builder.NewContributor<IUser>("user");

            //# Tree
            var userSet = builder.NewDefinition<Tree>("userSet");
            using (userSet.Define())
            {
                user.UserName = "Richard";
                    user.Password = "SomePass678;";
                        user.Age = 24;
                        user.Age = 36;
                user.UserName = "*+#&%$!$";
                    user.Password = "tooeasy";
                        user.Age = -1;
                        user.Age = 00;
            }
            //#

            //# Combine
            var allSet = builder.NewDefinition<AllCombinations>("allSet");
            using (allSet.Define())
            {
                hwSet.Ref();
                swSet.Ref();
                userSet.Ref();
            }
            //#

            //# Visualize_Def
            mDocUtil.BeginRecordConsole("Visualize_Def_Console");
            Console.WriteLine(userSet.PrintDefinition(isFileInfo: true));
            mDocUtil.StopRecordConsole();
            //#

            //# Visualize_Table
            mDocUtil.BeginRecordConsole("Visualize_Table_Console");
            Console.WriteLine(userSet.PrintCasesAsTable());
            mDocUtil.StopRecordConsole();
            //#

            //# Replay
            mDocUtil.BeginRecordConsole("Replay_Console");
            allSet.Cases().Replay().ActAndAssert(ctx =>
            {
                Environment env = GetHardwareAndSoftwareEnvironment(hw, sw);
                SignInPage signInPage = env.GetSignInPage();
                signInPage.FillInForm(user);
            });
            mDocUtil.StopRecordConsole();
            //#

            Console.WriteLine("swSet.Cases().Count() : {0}", swSet.Cases().Count());
            Console.WriteLine("hwSet.Cases().Count() : {0}", hwSet.Cases().Count());
            Console.WriteLine("userSet.Cases().Count() : {0}", userSet.Cases().Count());
            Console.WriteLine("all.Cases().Count() : {0}", allSet.Cases().Count());
        }
    }

}