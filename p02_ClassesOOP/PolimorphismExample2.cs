using System;

namespace p02_ClassesOOP
{
    public static class PolimorphismExample2
    {
        internal static void Show()
        {
            A3 a = new A3();
            a.Foo();
            //B3 b = new A3(); compile error
            A3 b2 = new B3();
            A3 c = new C3();
            // B3 b3 = new C3(); compile error
            D3 d = new D3();
            E3 e3 = new E3();
        }
    }

    class A3
    {
        public virtual void Foo() => Console.WriteLine("A");
    }

    class B3 : A3
    {
        public override void Foo() => Console.WriteLine("B");
    }
    
    class C3 : A3
    {
        public new void Foo() => Console.WriteLine("C");
    }
    
    class D3 : A3
    {
        public void Foo() => Console.WriteLine("D");
    }
    
    class E3 : B3
    {
        public new void Foo() => Console.WriteLine("E");
    }
}