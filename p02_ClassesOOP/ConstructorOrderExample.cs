using System;

namespace p02_ClassesOOP
{
    public static class ConstructorOrderExample
    {
        public static void Show()
        {
            A1 a = new B1();
        }
    }

    public class A1
    {
        public A1()
        {
            Console.WriteLine("ctor A called");
            Console.WriteLine($"B value = {B1.value}");
        }
    }

    public class B1 : A1
    {
        public B1()
        {
            Console.WriteLine("Constructor B called");
        }

        static B1()
        {
            value = 0;
            Console.WriteLine("Static constructor B called");
        }

        public static int value = 1;
    }
}