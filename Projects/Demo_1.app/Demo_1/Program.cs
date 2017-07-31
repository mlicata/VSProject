using System;

class RecExercise1
{
    static void Main()
    {
        Console.Write("\n\n Recursion : Find the sum of the first n natural numbers :\n");
        Console.Write("--------------------------------------------------------\n");
        int n = 0;
        int totalIndex = 0;

        try
        {
            n = InputNumber();
        }
        catch (NotFiniteNumberException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        SumNumbers(totalIndex, n);
    }

    public static void SumNumbers(int sumNumber, int index)
    {
        Console.WriteLine("Current Total:" + sumNumber + "\t InputtedValue:" + index);
        if (index > 0)
        {
            sumNumber += index;
            index--;
            SumNumbers(sumNumber, index);
        }
    }

    public static int InputNumber()
    {
        int tmp;
        string str;
        bool cont = true;
        do
        {
            Console.WriteLine("How many numbers to print : ");
            str = Console.ReadLine();
         
            if (int.TryParse(str, out tmp))
            {
                return tmp;//cont = false;
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine("Not a real number, please try again : ");
            }
        }
        while (cont);
        return tmp;
    }
}