using CommandLine;
using Lab1.CommandLineArguments;
using Lab1.Models;
using Lab1.Services;
using NLog;
using System.Collections.Generic;

namespace Lab1
{
    public class Program
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        const string json = "json";
        const string excel = "xls";
        static void Main(string[] args)
        {
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
                           switch (o.FileType.ToLower())
                           {
                               case json:
                                   {
                                       JsonWriter jsonWriter = new JsonWriter();
                                       jsonWriter.Write(students, $"{outputFile}.{json}");
                                       break;
                                   }
                               case excel:
                                   {
                                       ExcelWriter excelWriter = new ExcelWriter();
                                       excelWriter.Write(students, $"{outputFile}.{excel}");
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
