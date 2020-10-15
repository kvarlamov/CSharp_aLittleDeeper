using System;

namespace p11_DesighnPatterns
{
    public static class SingletonExample
    {
        public static void TestSingleton()
        {
            Logger.GetInstance().Log("it's works");
            Logger.GetInstance().Log("create just one instance");
        }
    }

    class Logger
    {
        private static Logger _instance = null;

        private Logger()
        {
            Console.WriteLine("Logger created");
        }

        public static Logger GetInstance()
        {
            return _instance ??= new Logger();
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}