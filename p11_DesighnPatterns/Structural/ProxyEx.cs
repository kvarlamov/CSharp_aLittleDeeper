using System;

namespace p11_DesighnPatterns.Structural
{
    public class ProxyEx
    {
        public static void Test()
        {
            IRepository r = new Repo();
            RepoProxy repoProxy = new RepoProxy(r);
            PrintData(repoProxy);
        }

        public static void PrintData(IRepository repository)
        {
            Console.WriteLine(repository.GetData("test"));
        }
    }

    public class RepoProxy : IRepository
    {
        private readonly IRepository _repo;

        public RepoProxy(IRepository repo)
        {
            _repo = repo;
        }

        public string GetData(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("data shouldn't be empty");
            return _repo.GetData(data);
        }
    }

    public class Repo : IRepository
    {
        public string GetData(string data)
        {
            return data.ToUpper();
        }
    }

    public interface IRepository
    {
        string GetData(string data);
    }
}