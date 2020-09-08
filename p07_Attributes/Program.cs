using System;
using System.Reflection;

namespace p07_Attributes
{
    class Program
    {
        static void Main(string[] args)
        {
            var isAttributeDefined = typeof(MyClass).IsDefined(typeof(LastChangeAttribute));
            var name = (typeof(MyClass).GetCustomAttribute(typeof(LastChangeAttribute)) as LastChangeAttribute)
                .GetName;
            Console.WriteLine(isAttributeDefined);
            Console.WriteLine(name);
        }
    }

    [LastChange("John Doe", "2000-10-10", 1234)]
    public class MyClass
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    sealed class LastChangeAttribute : Attribute
    {
        private readonly string _name;
        private readonly DateTime _time;
        private readonly int _number;
        
        public LastChangeAttribute(string name, string time, int number)
        {
            _name = name;
            _time = DateTime.Parse(time);
            _number = number;
        }

        public string GetName => _name;
        public string GetTime => _time.ToString();
        public int GetNumber => _number;
    }
}