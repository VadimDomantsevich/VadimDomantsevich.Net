using Lab1.Interfaces;
using Lab1.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace Lab1.Services
{
    public class ExcelWriter : IWriter
    {
        const string worksheetName = "Sheet 1";
        const string averageGroupMark = "Average group mark:";
        const string name = "Name";
        const string surname = "Surname";
        const string patronymic = "Panronymic";
        const string mark = "Average mark";
        public void Write(IEnumerable<Student> students, string fileName)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(worksheetName);
                List<StudentToWrite> studentsToWrite = new List<StudentToWrite>();
                foreach (Student student in students)
                {
                    var studentToWrite = new StudentToWrite
                    {
                        Name = student.Name,
                        Surname = student.Surname,
                        Patronymic = student.Patronymic,
                        AverageMark = student.GetAverageMark(),
                    };
                    studentsToWrite.Add(studentToWrite);
                }
                //Add some text to cell A1
                worksheet.Cells["A1"].Value = name;
                worksheet.Cells["B1"].Value = surname;
                worksheet.Cells["C1"].Value = patronymic;
                worksheet.Cells["D1"].Value = mark;
                worksheet.Cells["A2"].LoadFromCollection(studentsToWrite);
                worksheet.Cells["F1"].Value = averageGroupMark;
                worksheet.Cells["F2"].Value = Student.GetAverageGroupMark(students);

                //Save your file
                FileInfo fi = new FileInfo(fileName);
                excelPackage.SaveAs(fi);
            }
        }
    }
}
