using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dahuyag_Assessment_1
{
    public class Person : IComparable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public int CompareTo(Person? other)
        {
            // 0 if same name and age
            // -1 if not same age
            // 1 if not same name
            if (Age == other.Age && Name == other.Name) return 0;
            if (Age != other.Age) return -1;
            return 1;
        }
    }

    /*public class NameComparer : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x.Name == y.Name) return 0;
            if (x.Name.ToLowerInvariant() == y.Name.ToLowerInvariant()) return -1;
            return 1;
        }
    }*/
}
