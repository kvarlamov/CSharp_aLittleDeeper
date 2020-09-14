using System;
using System.Collections.Generic;

namespace p09_Delegates
{
    public class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public static class DelegateTasting
    {
        public static void TestDelegates()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee{Name = "Jonh Smit", Age = 43},
                new Employee{Name = "Jonh Smit2", Age = 33},
                new Employee{Name = "Jonh Smit3", Age = 21},
                new Employee{Name = "Jonh Smit4", Age = 19},
                new Employee{Name = "Jonh Smit5", Age = 53},
                new Employee{Name = "Jonh Smit6", Age = 40},
                new Employee{Name = "Jonh Smit7", Age = 41},
                new Employee{Name = "Jonh Smit8", Age = 60},
                new Employee{Name = "Jonh Smit9", Age = 36}
            };
            
            List<string> names = new List<string>
            {
                "SomeString",
                "SomeValue",
                "AnotherValue",
                "LongestValueEver",
                "Short"
            };

            var maxEmployeeByAge = employees.FindMaxElement((x, y) => x.Age > y.Age);
            Console.WriteLine($"Max employee by age is {maxEmployeeByAge.Name}, age = {maxEmployeeByAge.Age}");
            var maxStringByLength = names.FindMaxElement((x, y) => x.Length > y.Length);
            Console.WriteLine($"Longest string is {maxStringByLength}");
        }
    }

    public static class ListExtension
    {
        public static T FindMaxElement<T>(this List<T> list, Func<T, T, bool> func)
        {
            T max = list[0];
            for (int i = 0; i < list.Count-1; i++)
            {
                max = func(max, list[i + 1]) ? max : list[i + 1];
            }
            return max;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            DelegateTasting.TestDelegates();
        }
    }
}