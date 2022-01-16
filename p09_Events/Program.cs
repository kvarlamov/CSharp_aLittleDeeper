using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DocLib;
using Timer = System.Timers.Timer;

namespace p09_Events
{
    class Program
    {
        private const string PATH = @"TargetFolder";
        private const double INTERVAL = 15;
        private static AutoResetEvent waitHadnle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var ev = new DocumentReceiver();

            ev.DocumentsReady += () =>
            {
                Console.WriteLine("All documents in folder");
                waitHadnle.Set();
            };

            ev.TimedOut += () =>
            {
                Console.WriteLine("Time is over");
                waitHadnle.Set();
            };

            ev.Start(PATH, INTERVAL);

            waitHadnle.WaitOne();
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}