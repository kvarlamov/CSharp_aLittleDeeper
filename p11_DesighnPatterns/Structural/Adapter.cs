using System;

namespace p11_DesighnPatterns.Structural
{
    public class Adapter
    {
        public static void Test()
        {
            LocalRepo r = new LocalRepo();
            IRepository irep = new RepoAdapter(r);
            ProxyEx.PrintData(irep);
        }
    }

    public sealed class LocalRepo
    {
        public string Data { get; set; }
        public string GetData()
        {
            return this.Data;
        }
    }

    public class RepoAdapter : IRepository
    {
        private readonly LocalRepo _repo;

        public RepoAdapter(LocalRepo repo)
        {
            _repo = repo;
        }

        public string GetData(string data)
        {
            _repo.Data = data;
            return _repo.GetData();
        }
    }
}