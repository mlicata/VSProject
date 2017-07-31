using System;

namespace Hello
{
    class Program
    {
        static void Main(string[] args)
        {
            UserData user = new UserData();
            Console.WriteLine("your name");
            user.name = Console.ReadLine();
            Console.WriteLine("Hello, " + user.name);
            
            Console.WriteLine("How many hours of sleep did you get last night?");
            user.sleepHours = float.Parse(Console.ReadLine());

            if (user.sleepHours >= 8)
            {
                Console.WriteLine("You are well rested!");
            }
            else
            {
                Console.WriteLine("You need more sleep!");
            }
        }
    }
}
