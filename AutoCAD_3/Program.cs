using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCAD_3
{
    class Program
    {
        static void Main(string[] args)
        {
            IfExercise();
            //IfElseExercise();
            //IfElseIfExercise();
            //SwitchExercise();
        }
        private static int GetRandomNum(int min, int max)
        {
            Random rd = new Random();
            return rd.Next(min, max);
        }
        private static void IfExercise()
        {
            int ctr = 10;
            int num = GetRandomNum(9, 15);

            if (num > ctr)
                Console.WriteLine("The number {0} is greater than the counter.", num);

            Console.ReadKey();
        }

        private static void IfElseExercise()
        {
            int ctr = 10;
            int num = GetRandomNum(7, 15);

            if (num > ctr)
                Console.WriteLine("The number {0} is greater than the counter.", num);
            else
                Console.WriteLine("Ther numbr {0} is less than the counter.", num);

            Console.ReadKey();
        }

        private static void IfElseIfExercise()
        {
            int ctr = 10;
            int num = GetRandomNum(9, 11);

            if (num > ctr)
                Console.WriteLine("The number {0} is greater than the counter.", num);
            else if (num == ctr)
                Console.WriteLine("The number {0} is equal counter.", num);
            else
                Console.WriteLine("Ther numbr {0} is less than the counter.", num);

            Console.ReadKey();
        }

        private static void SwitchExercise()
        {
            string fruit = "";
            Console.WriteLine("\n Enter your fruit preference (x to exit) : ");
            fruit = Console.ReadLine().ToLower();

            while (fruit != "x")
            {
                switch (fruit)
                {
                    case "apple":
                        Console.WriteLine("Apple is your first choice.");
                        break;

                    case "orange":
                        Console.WriteLine("You preferred Orange.");
                        break;

                    case "mango":
                        Console.WriteLine("You preferred Orange.");
                        break;

                    default:
                        Console.WriteLine("You are having banana today.");
                        break;
                }
                Console.WriteLine("\n Enter your fruit preference (x to exit) : ");
                fruit = Console.ReadLine().ToLower();
            }
            Console.WriteLine("Bye!");
            Console.ReadKey();
        }
    }
}
