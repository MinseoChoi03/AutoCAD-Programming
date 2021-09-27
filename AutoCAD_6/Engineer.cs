using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCAD_6
{
    class Engineer : Person
    {
        public string Degree { get; set; }

        public Engineer() { }

        public Engineer(string firstName, string lastName, int age, string degree)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Degree = degree;
        }

        // Override the method_Polymorphism
        public void DoWork()
        {
            DoWorkPrivately();
        }

        private void DoWorkPrivately()
        {
            Console.WriteLine("I create engineering design");
            Console.WriteLine("I oversee construction projects");
        }
    }
}
