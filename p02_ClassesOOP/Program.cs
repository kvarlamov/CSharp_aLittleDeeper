using System;
using System.Collections.Generic;

namespace p02_ClassesOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            PolimorphismExample1.Show();
            ConstructorOrderExample.Show();
            PolimorphismExample2.Show();
            PolimorphismExample3.Show();
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
    }

    public interface IFigure
    {
        List<Point> Points { get; }
        double GetPerimetr();
    }

    public abstract class Figure : IFigure
    {
        protected List<Point> _points;
        public List<Point> Points => _points;

        public virtual double GetPerimetr()
        {
            return 0;
        }
    }

    public class Round : Figure
    {
        public double Radius { get; set; }
        public Point Centr => Points[0];
        public override double GetPerimetr() => Radius * 2 * Math.PI;

        public Round(double x, double y, double radius)
        {
            Radius = radius; 
            Points.Add(new Point{X= x, Y = y});
        }
    }

}