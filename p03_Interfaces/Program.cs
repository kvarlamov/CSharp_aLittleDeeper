using System;
using System.Collections.Generic;

namespace p03_Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            LinkedList<string> mylist = new LinkedList<string>();
            mylist.Add("1");
            mylist.Add("2");
            mylist.Add("3");
            mylist.Add("4");
            mylist.Add("5");
            foreach (var s in mylist)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine(mylist.Contains("3"));
            Console.WriteLine(mylist.Contains("10"));

            mylist.Remove("4");
            Console.WriteLine(mylist.Contains("4"));
            mylist.Clear();
            
            Console.ReadKey();
        }
    }
}