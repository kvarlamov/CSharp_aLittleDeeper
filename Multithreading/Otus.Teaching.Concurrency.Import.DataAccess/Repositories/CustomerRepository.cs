using System;
using Otus.Teaching.Concurrency.Import.Handler.Entities;
using Otus.Teaching.Concurrency.Import.Handler.Repositories;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Repositories
{
    public class CustomerRepository
        : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            Console.WriteLine(customer.Id);
            //Add customer to data source   
        }
    }
}