using System;

namespace p02_ClassesOOP
{
    public static class PolimorphismExample3
    {
        internal static void Show()
        {
            A4 a4 = new B4();
            A4 a41 = new C4();
            Console.WriteLine(a4.Calc());
            Console.WriteLine(a41.Calc());
            Console.WriteLine(a4.Calc() + a41.Calc());
        }
    }

    class A4
    {
        public virtual int Calc() => 10 * Gen();
        protected int Gen() => 10;
    }
    
    class B4 : A4
    {
        public override int Calc() => 20 * Gen();
        protected int Gen() => 20;
    }
    class C4 : B4
    {
        public override int Calc() => 30 * Gen();
        protected int Gen() => base.Gen();
    }
}