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


            //Inheritance
            // --------------------------------------------------
            Doctor doctor1 = new Doctor("Jeff", "Daniels", 38, "Oncology");
            Console.WriteLine("\nI am a Doctor and my specialty is: {0}", doctor1.Specialty);

            doctor1.DisplayPersonInfo();
            doctor1.DoWork();

            string work = doctor1.DoWork(5);
            Console.WriteLine("\n{0}", work);

            //Inheritance
            // --------------------------------------------------
            Engineer engineer1 = new Engineer();
            engineer1.FirstName = "Jimmy";
            engineer1.LastName = "Chan";
            engineer1.Age = 25;
            engineer1.Degree = "Structural Engineering";

            Console.WriteLine("\nI am an Engineer and my degree is: {0}", engineer1.Degree);
            engineer1.DisplayPersonInfo();
            engineer1.DoWork();
        }
    }
}
