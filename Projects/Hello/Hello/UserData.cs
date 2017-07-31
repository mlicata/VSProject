using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello
{
    public class UserData
    {
        public string name;
        public float sleepHours;

        /*
        public class GradeStatistics
        {
            public GradeStatistics()
            {
                HighestGrade = 0;
                LowestGrade = float.MaxValue;
            }
            public float AverageGrade;
            public float HighestGrade;
            public float LowestGrade;

            public int HighestGradeCnt = 1;
            public int LowestGradeCnt = 1;
        }
        */

        public void BubbleSort()
        {
            int[] a = { 3, 2, 5, 4, 1 };
            int t;
            Console.WriteLine("The Array is : ");
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
            for (int j = 0; j <= a.Length - 2; j++)
            {
                for (int i = 0; i <= a.Length - 2; i++)
                {
                    if (a[i] > a[i + 1])
                    {
                        t = a[i + 1];
                        a[i + 1] = a[i];
                        a[i] = t;
                    }
                }
            }
            Console.WriteLine("The Sorted Array :");
            foreach (int aray in a)
                Console.Write(aray + " ");
            Console.ReadLine();
        }
    }
}
