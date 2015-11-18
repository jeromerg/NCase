using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NCaseFramework.Front.Ui;
using NCaseFramework.NunitAdapter.Front.Ui;
using NDsl.Front.Ui;
using NUnit.Framework;
using NUtil.Doc;
using NUtil.Text;

namespace NCaseFramework.doc
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public class Readme
    {
        private readonly DocUtil docu = new DocUtil("docu", @"c:\dev\NCase");

        [TestFixtureTearDown]
        public void UpdateMarkdownFile()
        {
            docu.UpdateDocAssociatedToThisFile();
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

            public override string ToString()
            {
                return string.Format("({0}, {1})", Horizontal, Vertical);
            }
        }

        public enum Os
        {
            Ios8,
            Android6,
            WindowsMobile10,
        }

        public enum Browser
        {
            Chrome,
            Safari,
            Firefox,
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

        public interface IPatient
        {
            int Age { get; set; }
            Sex Sex { get; set; }
            bool HasPenicillinAllergy { get; set; }
        }

        public enum Sex
        {
            Male,
            Female
        }

        public class Pharmacy
        {
            public bool CanPrescribePenicillin(IPatient patient)
            {
                return true;
            }
        }

        [Test]
        public void ShortExample()
        {
            //# ShortExample
            // ARRANGE
            var builder = NCase.NewBuilder();            
            var p = builder.NewContributor<IPatient>("patient");
            var allPatients = builder.NewDefinition<AllCombinations>("swSet");

            using (allPatients.Define())
            {
                p.Age = 10;
                p.Age = 30;
                p.Age = 60;

                p.Sex = Sex.Female;
                p.Sex = Sex.Male;

                p.HasPenicillinAllergy = true;
            }

            allPatients.Cases().Replay().ActAndAssert(ctx =>
            {
                // ACT
                var pharmacy = new Pharmacy();
                bool canPrescribe = pharmacy.CanPrescribePenicillin(p);

                // ASSERT
                Assert.IsTrue(canPrescribe);
            });
            //#
        }

        [Test]
        public void Demo()
        {
            var builder = NCase.NewBuilder();

            var sw = builder.NewContributor<ISoftware>("sw");

            //# AllCombinations
            var swSet = builder.NewDefinition<AllCombinations>("swSet");
            using (swSet.Define())
            {
                sw.Os = Os.Ios8;
                sw.Os = Os.Android6;
                sw.Os = Os.WindowsMobile10;

                sw.Browser = Browser.Chrome;
                sw.Browser = Browser.Firefox;
                sw.Browser = Browser.Safari;

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

            {
                //# Visualize_Def
                docu.BeginRecordConsole("Visualize_Def_Console");
                string def = userSet.PrintDefinition(isFileInfo: true);

                Console.WriteLine(def);
                docu.StopRecordConsole();
                //#                
            }
            //# Visualize_Table
            docu.BeginRecordConsole("Visualize_Table_Console");
            string table = userSet.PrintCasesAsTable();

            Console.WriteLine(table);
            docu.StopRecordConsole();
            //#

            //# Visualize_Case
            docu.BeginRecordConsole("Visualize_Case_Console");
            string cas = userSet.Cases().First().Print();

            Console.WriteLine(cas);
            docu.StopRecordConsole();
            //#

            //# Iterate
            foreach (Case userCase in userSet.Cases())
                Console.WriteLine(userCase.Print());
            //#

            //# Replay
            foreach (Case userCase in userSet.Cases().Replay())
                Console.WriteLine(user.UserName);
            //#

            docu.BeginRecordConsole("ActAndAssert_Console", s => s.Lines().Skip(1).Reverse().Skip(20).Reverse().Concat(new[] {"(...)"}).JoinLines());
            try
            {
                //# ActAndAssert
                allSet.Cases().Replay().ActAndAssert(ctx =>
                {
                    Environment env = GetHardwareAndSoftwareEnvironment(hw, sw);
                    SignInPage signInPage = env.GetSignInPage();
                    signInPage.FillInForm(user);
                    if (ctx.TestCaseIndex >= 1) throw new OperationCanceledException(); //docu
                });
                //#
            }
            catch (OperationCanceledException)
            {
            }
            docu.StopRecordConsole();

            try { 
            //# ActAndAssert_ExpectException
            allSet.Cases().Replay().ActAndAssert(ctx =>
            {
                ctx.ExceptionAssert = ExceptionAssert.IsOfType<ApplicationException>();

                if (ctx.TestCaseIndex >= 1) throw new OperationCanceledException(); //docu
                throw new ApplicationException("this exception is expected");
            });
            //#
            }
            catch (OperationCanceledException)
            {
            }

            Console.WriteLine("swSet.Cases().Count() : {0}", swSet.Cases().Count());
            Console.WriteLine("hwSet.Cases().Count() : {0}", hwSet.Cases().Count());
            Console.WriteLine("userSet.Cases().Count() : {0}", userSet.Cases().Count());
            Console.WriteLine("all.Cases().Count() : {0}", allSet.Cases().Count());
        }
    }

}