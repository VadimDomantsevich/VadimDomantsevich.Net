using CommandLine;
using Lab1.CommandLineArguments;
using Lab1.Models;
using Lab1.Services;
using NLog;
using System.Collections.Generic;

namespace Lab1
{
    class Program
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            const string json = "JSON";
            const string excel = "EXCEL";
            List<Student> students = new List<Student>();
            string inputFile = string.Empty;
            string outputFile = string.Empty;
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       inputFile = o.InputFile;
                       outputFile = o.OutputFile;
                       if (!string.IsNullOrEmpty(inputFile))
                       {
                           CSVReader reader = new CSVReader();
                           students = reader.Read(inputFile);
                           switch (o.FileType.ToUpper())
                           {
                               case json:
                                   {
                                       JsonWriter jsonWriter = new JsonWriter();
                                       jsonWriter.Write(students, $"{outputFile}.json");
                                       break;
                                   }
                               case excel:
                                   {
                                       ExcelWriter excelWriter = new ExcelWriter();
                                       excelWriter.Write(students, $"{outputFile}.xls");
                                       break;
                                   }
                               default:
                                   {
                                       logger.Error("Wrong file format");
                                       break;
                                   }
                           }
                       }
                       else
                           logger.Error("Wrong input file");
                   });
        }
    }
}
