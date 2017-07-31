using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grades
{
    public class GradeBook : GradeTracker
    {
        public GradeBook()
        {
            _name = "Empty";
            grades = new List<float>();
        }
        public override GradeStatistics ComputeStatistics()
        {
            GradeStatistics stats = new GradeStatistics();
            float sum = 0;
            foreach (float grade in grades)
            {
                if (grade == stats.HighestGrade)
                {
                    stats.HighestGradeCnt++;
                }
                else
                {
                    stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
                }

                if (grade == stats.LowestGrade)
                {
                    stats.LowestGradeCnt++;
                }
                else
                {
                    stats.LowestGrade = Math.Min(grade, stats.LowestGrade);
                }
                sum += grade;
            }
            stats.AverageGrade = sum / grades.Count;
            return stats;
        }

        public override void WriteGrades(TextWriter dest)
        {
            for (int i = 0; i < grades.Count; i++)
            {
                dest.WriteLine(grades[i]);
            }
        }

        public override void AddGrade(float grade)
        {
            grades.Add(grade);
        }

        public override IEnumerator GetEnumerator()
        {
            return grades.GetEnumerator();
        }

        protected List<float> grades;
        public static float MinimumGrade = 0;
        public static float MaximumGrade = 100;

    }
}
