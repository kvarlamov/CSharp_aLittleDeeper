using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace p06_Reflection
{
    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        public List<Person> Friends { get; set; } = new List<Person>();
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person{LastName = "Иванов", FirstName = "Иван", Age = 25};
            var pSer = p.Serialize();
            Console.WriteLine(pSer);
            var newP = SerializerExtension.DeSerialize<Person>(pSer);
            Console.WriteLine("Deserealized object:\n" +
                              $"LastName = {newP.LastName},\n FirstName = {newP.FirstName},\n Age = {newP.Age}");
            Console.ReadKey();
        }
    }

    public static class SerializerExtension
    {
        public static string Serialize(this object o) => string.Join(";", o.GetType().GetProperties()
            .Select(p => $"{p.Name}={p.GetValue(o)}"));

        public static T DeSerialize<T>(string obj) where T: new()
        {
            T result = new T();
            string[] properties = obj.Split(';');
            foreach (var prop in properties)
            {
                string key = string.Concat(prop.TakeWhile(ch => ch != '='));
                int index = prop.IndexOf('=');
                string value = prop.Substring(index + 1);
                var property = typeof(T).GetProperty(key);
                try
                {
                    property?.SetValue(result, Convert.ChangeType(value, property.PropertyType),null);
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
            
            return result;
        }
    }
}