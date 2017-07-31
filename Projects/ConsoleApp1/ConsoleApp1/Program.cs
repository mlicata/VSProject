using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "C:\\Users\\malicata\\Documents\\Visual Studio 2017\\Projects\\ConsoleApp1\\ConsoleApp1\\DateBook2.xlsx";

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = 208000;
            int[] DoW = new int[7];
            float[] TTR = new float[7];


            Console.WriteLine("start loop");
            for (int i = 2; i <= rowCount; i++)
            {
                if (xlRange.Cells[i, 3] != null && xlRange.Cells[i, 3].Value2 != null)
                {
                    DateTime d = new DateTime();
                    d = xlRange.Cells[i, 2].Value;
                    float cellTTR = float.Parse(xlRange.Cells[i, 4].Value.ToString());

                    switch (d.DayOfWeek.ToString().Trim().ToLower())
                    {
                        case "sunday":
                            DoW[0] += 1;
                            TTR[0] += cellTTR;
                            break;
                        case "monday":
                            DoW[1] += 1;
                            TTR[1] += cellTTR;
                            break;
                        case "tuesday":
                            DoW[2] += 1;
                            TTR[2] += cellTTR;
                            break;
                        case "wednesday":
                            DoW[3] += 1;
                            TTR[3] += cellTTR;
                            break;
                        case "thursday":
                            DoW[4] += 1;
                            TTR[4] += cellTTR;
                            break;
                        case "friday":
                            DoW[5] += 1;
                            TTR[5] += cellTTR;
                            break;
                        case "saturday":
                            DoW[6] += 1;
                            TTR[6] += cellTTR;
                            break;
                    }
                }
                if(i%1000 == 0)
                    Console.WriteLine(i + " loops completed");
            }
            Console.WriteLine("data loaded");

            for (int i = 0; i < DoW.Length; i++)
            {
                xlRange.Cells[i+3, 9].Value = DoW[i];
                xlRange.Cells[i+3, 10].Value = (TTR[i] / DoW[i]);
            }

            Console.WriteLine("data written");

            xlRange.Cells[3, 8].Value = "Sunday";
            xlRange.Cells[4, 8].Value = "Monday";
            xlRange.Cells[5, 8].Value = "Tuesday";
            xlRange.Cells[6, 8].Value = "Wednesday";
            xlRange.Cells[7, 8].Value = "Thursday";
            xlRange.Cells[8, 8].Value = "Friday";
            xlRange.Cells[9, 8].Value = "Saturday";

            //xlWorkbook.Save();
            xlWorkbook.Close();
            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
