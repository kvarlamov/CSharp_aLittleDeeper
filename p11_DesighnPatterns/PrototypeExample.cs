using System;

namespace p11_DesighnPatterns
{
    public static class PrototypeExample
    {
        public static void TestPrototype()
        {
            
        }
    }

    class Circle : ICloneable
    {
        private int _radius;
        private string _color;

        public Circle(int radius, string color)
        {
            _radius = radius;
            _color = color;
        }

        public object Clone()
        {
            return new Circle(_radius, _color);
        }
    }
}