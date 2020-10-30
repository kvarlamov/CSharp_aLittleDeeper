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

        public void AddCustomer(Customer customer)
        {
            if (_counter == 0)
            {
                Interlocked.Increment(ref _counter);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("WriteToDataBase");
            }
            Thread.Sleep(TimeSpan.FromTicks(100));
            //Add customer to data source   
        }
    }
}