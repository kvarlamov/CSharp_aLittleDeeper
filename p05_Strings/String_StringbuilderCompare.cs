using System;
using System.Diagnostics;
using System.Text;

namespace p05_Strings
{
    public static class String_StringbuilderCompare
    {
        public static void StringCreator()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string text = "";
            for (int i = 1; i < 100000; i++)
            {
                text += i.ToString();
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        public static void StringBuilderCreator()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 1; i < 100000; i++)
            {
                stringBuilder.Append(i);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}