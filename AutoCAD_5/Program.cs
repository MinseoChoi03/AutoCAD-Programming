using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutoCAD_5
{
    class Program
    {
        static void Main(string[] args)
        {
            DivideByZeroExceptionExercise();
            IndexOutOfBoundsExceptionExercise();
            string contents = FileNotFoundExceptionExcercise(@"C:\temp\test2.txt");
            Console.WriteLine(contents);
            Console.ReadKey();
        }

        private static string FileNotFoundExceptionExcercise(string filename)
        {
            FileStream stream = null;
            StreamReader file = null;
            string contents = "";

            try
            {
                stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                file = new StreamReader(stream);
                contents = file.ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("The file is not available: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered: " + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }

                if (file != null)
                {
                    file.Dispose();
                }
            }
            return contents;
        }


        private static void DivideByZeroExceptionExercise()
        {
            try
            {
                int i = 0;
                int result = 12 / i;
                Console.WriteLine("The result is : " + result.ToString());
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("You are dividing by zero: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered: " + ex.Message);
            }
        }

        private static void IndexOutOfBoundsExceptionExercise()
        {
            try
            {
                int[] numbers = new int[3];

                numbers[0] = 2;
                numbers[1] = 9;
                numbers[2] = 5;
                numbers[3] = 7;

                foreach (int i in numbers)
                {
                    Console.WriteLine("The current number is: " + i);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Array overloaded: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered: " + ex.Message);
            }
            Console.ReadKey();
        }
    }
}
