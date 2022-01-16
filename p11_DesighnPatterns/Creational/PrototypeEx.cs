using System;

namespace p11_DesighnPatterns
{
    public class PrototypeEx
    {
        public static void TestPrototype()
        {
            Person p = new Person()
            {
                FirstName = "John",
                LastName = "Daw",
                Age = 33
            };

            var p2 = p.Copy();
        }
        
    }

    public class Person : IPrototype<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }


        public Person Copy()
        {
            return new Person()
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = Age
            };
        }
    }

    public interface IPrototype<T>
    {
        T Copy();
    }
}