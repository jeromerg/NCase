using System;
using System.IO;
using System.Linq;
using Antlr4.StringTemplate;
using JetBrains.Annotations;
using LibGit2Sharp;

namespace NGitVersion
{
    public static class NGitVersion
    {
        private const string MODEL_VAR = @"m";

        // directories are relative to output project directory ${ProjectDir}\bin
        private const string TEMPLATE_DIR = @"..\..\Templates\";
        private const string OUTPUT_DIR = @"..\..\Generated\";
        private const string MAIN_TEMPLATE_NAME = @"MainTemplate";

        public static void Main()
        {
            var model = new Model.Model(new Repository(GetGitRoot()));

            Directory.GetFiles(TEMPLATE_DIR, "*.stg")
                     .Select(Path.GetFullPath)
                     .ToList()
                // ReSharper disable once AssignNullToNotNullAttribute
                     .ForEach(templateFile => ProcessTemplate(templateFile, model));
        }

        private static void ProcessTemplate([NotNull] string templateFile, Model.Model model)
        {
            if (templateFile == null) throw new ArgumentNullException("templateFile");

            Template template = new TemplateGroupFile(templateFile)
                .GetInstanceOf(MAIN_TEMPLATE_NAME);

            if (template == null)
                throw new ArgumentException(string.Format("Template {0} not found", MAIN_TEMPLATE_NAME));

            template.Add(MODEL_VAR, model);

            File.WriteAllText(BuildTargetFileName(templateFile), template.Render());
        }

        private static string GetGitRoot()
        {
            const string gitDir = ".git";
            string hierarchy = @".\";
            while (true)
            {
                bool exists = Directory.Exists(hierarchy + gitDir);
                if (exists)
                    return hierarchy;

                hierarchy = hierarchy + @"..\";

                if (!Directory.Exists(hierarchy))
                    throw new ApplicationException("No .git folder found in the current path hierarchy");
            }
        }

        [NotNull]
        private static string BuildTargetFileName([NotNull] string templateFile)
        {
            string outputFileName = Path.GetFileNameWithoutExtension(templateFile);
            return OUTPUT_DIR + outputFileName;
        }
    }
}