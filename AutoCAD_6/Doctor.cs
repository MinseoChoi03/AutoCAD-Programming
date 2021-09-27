using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCAD_6
{
    class Doctor : Person
    {
        public string Specialty { get; set; }

        public Doctor() { }

        public Doctor(string firstName, string lastName, int age, string specialty)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Specialty = specialty;
        }


        // Override the method
        public string DoWork()
        {
            // Do some specific work here for the Doctor
            DoWorkPrivately();

            // Return the type of work
            return "";
        }

        private void DoWorkPrivately()
        {
            Console.WriteLine("I prescribe medication.");
            Console.WriteLine("I perform surgery.");
        }

        // Overload the method
        public string DoWork(int noOfTimes)
        {
            for (int i = 1; i <= noOfTimes; i++)
            {
                Console.WriteLine("I perform operation {0} times a day.", i);
            }

            return "And I'm tired.";
        }
    }
}
