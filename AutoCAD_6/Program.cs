using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCAD_6
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example 1 - using the fields default values
            //--------------------------------------------------
            Person person1 = new Person();
            person1.DisplayPersonInfo();

            //Example 2 - using the fields
            // --------------------------------------------------
            Person person2 = new Person("Joy", "Frazer", 32);
            person2.DisplayPersonInfo();
            Console.WriteLine("My Type of work is: " + person2.DoWork());
        }
    }
}
