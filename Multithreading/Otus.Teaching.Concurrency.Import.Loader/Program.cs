using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Otus.Teaching.Concurrency.Import.Core.Loaders;
using Otus.Teaching.Concurrency.Import.DataAccess.Parsers;
using Otus.Teaching.Concurrency.Import.DataAccess.Repositories;
using Otus.Teaching.Concurrency.Import.DataGenerator.Generators;
using Otus.Teaching.Concurrency.Import.Handler.Entities;


namespace Otus.Teaching.Concurrency.Import.Loader
{
    class Program
    {
        private static string _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "customers.xml");
        private static readonly int _numberOfNodes = 500000;
        private static XDocument _doc;
        private static readonly int _numOfThreads = 4;
        private static readonly int _step = _numberOfNodes / _numOfThreads;
        private static bool _isFirstCustomer = true;
        private static int _currentMax;
        private static EventWaitHandle wh = new AutoResetEvent(false);
        private static CustomerRepository _repository = new CustomerRepository();
        
        static void Main(string[] args)
        {
            Stopwatch sw  = new Stopwatch();
            sw.Start();
            //TODO: move all hardcode to config!!!
            if (args != null && args.Length == 1)
            {
                _dataFilePath = args[0];
                Process.Start(@"D:\IT\Programming\C#\CSharp_aLittleDeeper\Multithreading\Otus.Teaching.Concurrency.Import.DataGenerator.App\bin\Debug\netcoreapp3.1\Otus.Teaching.Concurrency.Import.DataGenerator.App.exe", "customers");
            }
            else
            {
                Console.WriteLine($"Loader started with process Id {Process.GetCurrentProcess().Id}...");
                GenerateCustomersDataFile();
            }

            var loader = new DataLoader(@"D:\IT\Programming\C#\CSharp_aLittleDeeper\Multithreading\Otus.Teaching.Concurrency.Import.Loader\bin\Debug\netcoreapp3.1\customers.xml");

            _doc = loader.LoadData();
            SelectNodesSingleThread();
            //TODO: set number of threads in config - now 4

            //int leftBorder = 0;
            //Thread[] threads = new Thread[_numOfThreads];
            //for (byte i = 0; i < _numOfThreads; i++)
            //{
            //    threads[i] = new Thread(new ParameterizedThreadStart(SelectNodesFromXmlAndSendToParser));
            //    threads[i].Start(leftBorder);
            //    leftBorder += _step;
            //}

            //for (int i = 0; i < _numOfThreads; i++)
            //{
            //    threads[i].Join();
            //}



            sw.Stop();
            Console.WriteLine($"\n***Всего потрачено секунд: {sw.ElapsedMilliseconds / 1000},{sw.ElapsedMilliseconds % 1000}");

            Console.ReadKey();
        }

        static void GenerateCustomersDataFile()
        {
            XmlGenerator xmlGenerator = new XmlGenerator(_dataFilePath, _numberOfNodes);
            xmlGenerator.Generate();
        }

        static void SelectNodesSingleThread()
        {
            List<XElement> nodes = _doc.Descendants("Customer").ToList();
            XmlParser parser = new XmlParser { Items = nodes };
            FeedDatabaseSingleThread(parser.Parse());
        }

        static void FeedDatabaseSingleThread(List<Customer> customers)
        {
            customers.ForEach(c => _repository.AddCustomer(c));
        }

        static void SelectNodesFromXmlAndSendToParser(object leftBorder)
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/linq/linq-xml-overview
            List<XElement> nodes = new List<XElement>();
            if (_numberOfNodes - (int) leftBorder - _step < _step)
            {
                nodes = _doc.Descendants("Customer")
                    .Where(el => (int)el.Element("Id") >= (int)leftBorder + 1).ToList();
            }
            else
            {
                nodes = _doc.Descendants("Customer")
                    .Where(el => (int)el.Element("Id") >= (int)leftBorder + 1 &&
                                 (int)el.Element("Id") <= (int)leftBorder + _step).ToList();
            }
            
            XmlParser parser = new XmlParser { Items = nodes};
            FeedDatabase(parser.Parse());
            wh.Set();
        }

        static void FeedDatabase(List<Customer> customers)
        {
            while (true)
            {
                if (customers[0].Id == 1)
                {
                    customers.ForEach(c => _repository.AddCustomer(c));
                    //Thread.Sleep(300);//Imitation of lont time work
                    _currentMax = customers.Last().Id;
                    return;
                }

                if (customers[0].Id - 1 == _currentMax)
                {
                    customers.ForEach(c => _repository.AddCustomer(c));
                    //Thread.Sleep(300);//Imitation of lont time work
                    _currentMax = customers.Last().Id;
                    return;
                }
                
                wh.WaitOne();
            }
        }
    }
}