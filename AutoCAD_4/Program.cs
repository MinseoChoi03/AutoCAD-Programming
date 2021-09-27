using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCAD_4
{
    class Program
    {
        static void Main(string[] args)
        {
            ForLoopExercise();
            //ForEachLoopExercise();
            //WhileLoopExercise();
            //DoWhileLoopExercise();
        }

        private static void ForLoopExercise()
        {
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine("The value of i is : " + i.ToString());
            }
            Console.ReadKey();
        }

        private static void ForEachLoopExercise()
        {
            myCircle mc = new myCircle();
            mc.Id = 123;
            mc.Color = "red";
            mc.Radius = 1;

            myCircle mc2 = new myCircle();
            mc2.Id = 345;
            mc2.Color = "blue";
            mc2.Radius = 2;

            myCircle mc3 = new myCircle();
            mc3.Id = 678;
            mc3.Color = "green";
            mc3.Radius = 3;

            List<myCircle> mcs = new List<myCircle>();
            mcs.Add(mc);
            mcs.Add(mc2);
            mcs.Add(mc3);

            foreach (myCircle cir in mcs)
            {
                Console.WriteLine("\n Properties of my circles");
                Console.WriteLine("ID : " + cir.Id.ToString());
                Console.WriteLine("Color : " + cir.Color);
                Console.WriteLine("Radius : " + cir.Radius.ToString());
            }
        }
        class myCircle
        {
            public int Id { get; set; }
            public int Radius { get; set; }
            public string Color { get; set; }
        }

        private static void WhileLoopExercise()
        {
            int ctr = 1;
            while (ctr <= 10)
            {
                Console.WriteLine("Value of counter is now : " + ctr.ToString());
                ctr += 1;
            }
        }

        private static void DoWhileLoopExercise()
        {
            int ctr = 0;
            do
            {
                Console.WriteLine("The value of conter is now : " + ctr.ToString());
                ctr += 1;
            }
            while (ctr < 10);
            Console.ReadKey();
        }
    }
}
