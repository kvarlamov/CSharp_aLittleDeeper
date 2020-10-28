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
            //Console.WriteLine("Loading data...");
            //Thread.Sleep(2000);
            //Console.WriteLine("Loaded data...");
            //Console.WriteLine("\n**Start parsing");

            XDocument doc = XDocument.Load(_filepath);
            return doc;
        }
    }
}