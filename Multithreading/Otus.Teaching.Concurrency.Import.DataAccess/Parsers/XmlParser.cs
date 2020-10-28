using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.DataGenerator.Dto;
using Otus.Teaching.Concurrency.Import.Handler.Entities;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Parsers
{
    public class XmlParser
        : IDataParser<List<Customer>>
    {
        public IEnumerable<XElement> Items { get; set; }
        public List<Customer> Parse()
        {
            List<Customer> customers = new List<Customer>();
            XmlSerializer serializer = new XmlSerializer(typeof(Customer));
            foreach (var item in Items)
            {
                customers.Add((Customer)serializer.Deserialize(item.CreateReader()));
            }
            
            return customers.OrderBy(c => c.Id).ToList();
        }
    }
}