using System;
using System.Xml.Linq;

namespace Otus.Teaching.Concurrency.Import.Core.Loaders
{
    public class DataLoader
        : IDataLoader
    {
        private readonly string _filepath;

        public DataLoader(string filepath)
        {
            _filepath = filepath;
        }        

        public XDocument LoadData()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Loading data...");            
            XDocument doc = XDocument.Load(_filepath);
            Console.WriteLine("Data loaded!");
            return doc;
        }
    }
}