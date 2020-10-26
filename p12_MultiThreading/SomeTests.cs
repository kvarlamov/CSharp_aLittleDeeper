using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace p12_MultiThreading
{
    public class SomeTests
    {
        private static Dictionary<int, long> _dic = new Dictionary<int, long>();
        public static ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        public static void Start()
        {
            Console.WriteLine("Старт");
            Parallel.For(0, 10, DoWork);
            foreach (var value in _dic.OrderBy(v => v.Key))
            {
                Console.WriteLine(value.Key + " : " + value.Value);
            }
        }
        private static void DoWork(int id)
        {
            Console.WriteLine("Поток с id: " + id);
            for (int j = 0; j < 10; j++)
            {
                try
                {
                    rwl.EnterUpgradeableReadLock();
                    var result = _dic.ContainsKey(j) ? _dic[j] * 10 : j*10;
                    try
                    {
                        rwl.EnterWriteLock();
                        _dic[j] = result;
                    }
                    finally
                    {
                        rwl.ExitWriteLock();
                    }
                }
                finally
                {
                    rwl.ExitUpgradeableReadLock();
                }
            }
        }
    }
}