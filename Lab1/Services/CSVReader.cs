using CsvHelper;
using CsvHelper.TypeConversion;
using Lab1.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Lab1.Services
{
    public class CSVReader : Interfaces.IReader
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        const int indexOfSubjects = 3;
        public List<Student> Read(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = new List<Student>();
            int index;

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                if (csv.Context.Record.Length != csv.Context.HeaderRecord.Length)
                {
                    logger.Error($"Student's {csv.Context.Record[0]} count of subjects didn't match the count of marks");
                    throw new InvalidDataException("Wrong number of parametres");
                }
                index = indexOfSubjects;
                List<Subject> listOfSubjects = new List<Subject>();
                while (index < csv.Context.Record.Length)
                {
                    try
                    {
                        var subject = new Subject
                        {
                            Name = csv.Context.HeaderRecord[index],
                            Mark = csv.GetField<int>(index),
                        };
                        index++;
                        listOfSubjects.Add(subject);
                    }
                    catch (ArgumentException)
                    {
                        logger.Error($"Student's {csv.Context.Record[0]} mark of {csv.Context.HeaderRecord[index]} isn't valid");
                        throw;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        logger.Error($"Student's {csv.Context.Record[0]} count of subjects didn't match the count of marks");
                        throw;
                    }
                    catch (TypeConverterException e)
                    {
                        logger.Error(e.Message);
                        throw;
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        throw;
                    }
                }
                var record = new Student
                {
                    Surname = csv.Context.Record[0],
                    Name = csv.Context.Record[1],
                    Patronymic = csv.Context.Record[2],
                    Subjects = listOfSubjects,
                };
                records.Add(record);
            }
            return records;
        }
    }
}
