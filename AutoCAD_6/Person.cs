using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCAD_6
{
    class Person
    {
        // Declare the Fields
        private string _firstName = "DefaultFirstName";
        private string _lastName = "DefaultLastName";
        private int _age = 20;

        // Declare the Properties
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value.Length <= 20 && value != null)
                {
                    _firstName = value;
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        // Constructors
        public Person() { }

        public Person(string firstName, string lastName, int age)
        {
            _firstName = firstName;
            _lastName = lastName;
            _age = age;
        }

        // Methods
        public string DoWork()
        {
            // do some other work here
            DoWorkPrivately();
            return "I do general labour work";
        }
        private void DoWorkPrivately()
        {
            Console.WriteLine("I do gardening.");
            Console.WriteLine("I clean the house.");
            Console.WriteLine("I walk the dog.");
        }

        public void DisplayPersonInfo()
        {
            Console.WriteLine("Your Fullname is: {0} {1} and your age is {2}", _firstName, _lastName, _age);
        }
    }
}
