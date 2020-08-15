using System;
using System.Linq;

namespace p01_Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p1 = new Point(1, 2);
            Point p2 = new Point(3, 2);
            var p3 = p1 + p2;
            Console.WriteLine(p3);
            string str = "ABC";
            Console.WriteLine(str.RevertString());

            Console.ReadKey();
        }
    }
    public struct Point
    {
        public double X, Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        
        public static Point operator +(Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y);
        public override string ToString()
        {
            return $"[{X};{Y}]";
        }
    }

    public static class StringExtencion
    {
        public static string RevertString(this string text) => new string(text.ToCharArray().Reverse().ToArray());
    }
}