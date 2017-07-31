using System.IO;
using System.Collections;

namespace Grades
{
    internal interface IGradeTracker : IEnumerable
    {
        void AddGrade(float grade);
        GradeStatistics ComputeStatistics();
        void WriteGrades(TextWriter dest);

        string Name { get; set; }

    }
}