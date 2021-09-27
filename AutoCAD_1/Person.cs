using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCAD_1
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Person() { }

        public Person(string fname, string lname, int age)
        {
            FirstName = fname;
            LastName = lname;
            Age = age;
        }

        public string DisplayPersonInfo()
        {
            string fullInfo = "Fullname : " + FirstName + " " + LastName + " \nAnd your age is " + Age + "\n";

            return fullInfo;
        }
    }
}
