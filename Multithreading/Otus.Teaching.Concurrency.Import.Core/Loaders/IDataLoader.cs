using System.Xml.Linq;

namespace Otus.Teaching.Concurrency.Import.Core.Loaders
{
    public interface IDataLoader
    {
        XDocument LoadData();
    }
}