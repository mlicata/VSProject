using System;
using System.Collections.Generic;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = new int[] { 3, 9, 7, 12, 11 };
            int[] arr2 = new int[] { 1, 8, 5, 2, 15 };
            int[] arrFinal = new int[10];
            int[] arrFinal2 = new int[10];

            /*
            Console.WriteLine("\n printing array 1");
            PrintArray(arr1);
            Console.WriteLine("\n printing array 2");
            PrintArray(arr2);

            Console.WriteLine("sorting with option 1");
            arrFinal = Option1(arr1, arr2);
            Console.WriteLine("printing option 1 array");
            PrintArray(arrFinal);
            */
            Console.WriteLine("sorting with option 2");
            arrFinal2 = Option2(arr1, arr2);
            Console.WriteLine("printing final array");
            PrintArray(arrFinal2);

            Console.ReadKey();
        }

        private static void PrintArray(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }

        public static int[] Option1(int[] arr1, int[] arr2)
        {
            int[] returnArray = new int[arr1.Length + arr2.Length];
            for(var i = 0; i < arr1.Length; i++)
            {
                returnArray[i] = arr1[i];
            }

            for (var i = 0; i < arr2.Length; i++)
            {
                returnArray[i + arr1.Length] = arr2[i];
            }
            Array.Sort(returnArray);
            return returnArray;
        }

        public static int[] Option2(int[] arr1, int[] arr2)
        {
            int[] returnArray = new int[arr1.Length + arr2.Length];
            for (var i = 0; i < arr1.Length; i++)
            {
                returnArray[i] = arr1[i];
            }

            for (var i = 0; i < arr2.Length; i++)
            {
                returnArray[i + arr1.Length] = arr2[i];
            }

            Array.Sort(returnArray);

            int temp;
            for (var i = 0; i < returnArray.Length; i++)
            {
                for (var j = 1; j < returnArray.Length; j++)
                {
                    if (returnArray[j+1] < returnArray[j])
                    {
                        temp = returnArray[j];
                        returnArray[j+1] = returnArray[j];
                        returnArray[j] = temp;
                    }
                }
            }

            return returnArray;
        }

    }
}