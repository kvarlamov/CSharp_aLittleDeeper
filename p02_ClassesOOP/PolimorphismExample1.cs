using System;

namespace p02_ClassesOOP
{
    public static class PolimorphismExample1
    {
        public static void Show()
        {
            A a = new A(), b = new B(), c = new C();
            I ia = new A(), ib = new B(), ic = new C();
            C c3 = new C();
            Console.WriteLine($"{a.X},{b.X}, {c.X}, {ia.X}, {ib.X}, {ic.X}");
            Console.WriteLine(c3.X);
        }
    }
    public interface I
    {
        int X { get; }
    }

    public class A : I
    {
        public virtual int X => 0;
    }

    public class B : A
    {
        public override int X => 1;
    }

    public class C : B, I
    {
        public int X => 2;
    }
}