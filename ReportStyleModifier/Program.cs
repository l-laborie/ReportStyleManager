using System;
using CommandLine;
using CommandLine.Text;
using System.IO;

namespace ReportStyleModifier
{
    class Program
    {
        class Options
        {
            [Option('p', "path", Required = true,
                HelpText = "The path to the report .rdl file.")]
            public string ReportPath { get; set; }

            [Option('d', "dataset-name", Required = true,
                HelpText = "Name of the dataset present into the report and contains styles.")]
            public string DatasetName { get; set; }

            [Option('b', "backup", DefaultValue = false,
                HelpText = "Backup the current file in *.rdl.timestamp.bkp file")]
            public bool Backup { get; set; }

            [ParserState]
            public IParserState LastParserState { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this,
                  (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
        }

        static void Main(string[] args)
        {
            Options options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                if (!File.Exists(options.ReportPath))
                {
                    Console.Error.WriteLine("The report given {0} doesn't exist.",
                        options.ReportPath);
                    Environment.Exit(1);
                }

                if (options.Backup)
                {
                    string backupPath = null;
                    do
                    {
                        string backupSuffix = string.Format(
                            ".{0:yyyyMMdd-HHmmss}.bkp",
                            DateTime.Now);
                        backupPath = options.ReportPath + backupSuffix;
                    }
                    while (File.Exists(backupPath));

                    File.Copy(options.ReportPath, backupPath);

                    ReportModifier modifier = new ReportModifier(options.ReportPath);
                    try
                    {
                        modifier.SetManagedStyle(options.DatasetName);
                    }
                    catch (ReportModifierException error)
                    {
                        Console.Error.WriteLine(error.Message);
                        Environment.Exit(1);
                    }
                    catch (DescriptionException error)
                    {
                        Console.Error.WriteLine(error.Message);
                        Environment.Exit(1);
                    }

                    Console.WriteLine("Update of styles for report {0} Done!");
                }
            }
        }
    }
}
