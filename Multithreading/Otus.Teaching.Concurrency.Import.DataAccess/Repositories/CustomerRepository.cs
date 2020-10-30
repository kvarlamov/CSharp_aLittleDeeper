using System;
using System.Threading;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using Otus.Teaching.Concurrency.Import.Handler.Repositories;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Repositories
{
    public class CustomerRepository
        : ICustomerRepository
    {
        static object _locker = new object();
        private EventWaitHandle _wh = new AutoResetEvent(true);
        private int _counter;

        public void SignalThatWriteToDbStart()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("WriteToDataBase");
        }

        public void AddCustomer(Customer customer)
        {
            //while (true)
            //{
            //    if (Monitor.TryEnter(_locker, 31))
            //    {
            //        try
            //        {
            //            Thread.Sleep(TimeSpan.FromTicks(100));
            //            break;
            //        }
            //        finally
            //        {
            //            Monitor.Exit(_locker);
            //        }
            //    }
            //}

            Thread.Sleep(TimeSpan.FromTicks(100));
            //Add customer to data source   
        }
    }
}