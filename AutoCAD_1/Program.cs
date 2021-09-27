using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCAD_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Values();
            //Person_read();
            //SingleDimArrayExercise();
            //MultiDimArrayExercise();
        }
        void Values()
        {
            sbyte sb = -128;//min
            sbyte sb2 = 127; //max 

            short sh = -32768; //min
            short sh2 = 32767; //max

            int i1 = -2147483648; //min
            int i2 = 2147483647; //max

            byte bt = 0; //min
            byte br2 = 255; //max

            ushort ush = 0; //min
            ushort ush1 = 6553; //max

            float f = 4.6f;

            double dbNum = 3.5;
            double dbNum2 = 3D; //D 써도 되고 안 써도 되고

            decimal myMoney = 100.5M; //M안 쓰면 double형 인식

            decimal totalAmout = myMoney * 10;

        }
        void Person_read()
        {
            Person p = new Person();
            p.FirstName = "Jen";
            p.LastName = "Kennedy";
            p.Age = 28;

            Person p2 = new Person("Choi", "Minseo", 19);

            string info = p.DisplayPersonInfo();
            string info2 = p2.DisplayPersonInfo();

            Console.WriteLine(info);
            Console.WriteLine(info2);
        }
        void SingleDimArrayExercise()
        {
            //int[] num = new int[] { 2, 3, 5, 7, 8};
            //int[] num = new int[5];

            //for(int i = 0; i < num.Length; i++)
            //{
            //    num[i] = i;
            //    Console.WriteLine(num[i]);
            //}

            //string[] cars = new string[] { "mustang", "chevy", "cadillac" };
            string[] cars = new string[4];

            cars[0] = "toyota";
            cars[1] = "honda";
            cars[2] = "BMW";
            cars[3] = "porsche";

            for (int i = 0; i < cars.Length; i++)
            {
                Console.WriteLine(cars[i]);
            }
        }
        void MultiDimArrayExercise()
        {
            //int[,] array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            //int[,] array2D = new int[3, 2];
            //array2D[0, 0] = 1;
            //array2D[0, 1] = 2;
            //array2D[1, 0] = 3;
            //array2D[1, 1] = 4;
            //array2D[2, 0] = 5;
            //array2D[2, 1] = 6;

            //foreach(int i in array2D)
            //{
            //    Console.WriteLine("Value is {0}", i);
            //}
            string[,] cars2D = new string[3, 2] { { "japan", "toyota" }, { "korea", "hyundai" }, { "us", "ford" } };
            foreach (string s in cars2D)
            {
                Console.WriteLine("Value is {0}", s);
            }
        }
    }
}
