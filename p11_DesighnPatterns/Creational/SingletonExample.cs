using System;

namespace p11_DesighnPatterns
{
    public static class SingletonExample
    {
        public static void TestSingleton()
        {
            Logger.Instance.Log("it's works");
            Logger.Instance.Log("create just one instance");
        }
    }

    class Logger
    {
        private static object _locker = new object();
        private static volatile Logger _instance;

        private Logger()
        {
            Console.WriteLine("Logger created");
        }

        public static Logger Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (_locker)
                {
                    return _instance ?? (_instance = new Logger());
                }
            }
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}