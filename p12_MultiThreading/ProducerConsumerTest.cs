using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace p12_MultiThreading
{
    public static class ProducerConsumerUsingTest
    {
        public static void Start()
        {
            using (ProducerConsumerTest<string> q = new ProducerConsumerTest<string>())
            {
                q.EnqueueTask("Let's begin");
                for (int i = 0; i < 20; i++)
                {
                    if (q.counter >= ProducerConsumerTest<string>.MembersToPass)
                    {
                        q._WaitHandle.WaitOne();
                    }
                    q.EnqueueTask($"Task #{i}");
                }
                q.EnqueueTask("Consumer thread finished work");
                Console.WriteLine("Producer thread finished work");
            }
        }
    }
    
    public class ProducerConsumerTest<T> : IDisposable where T: class
    {
        public const int MembersToPass = 15;
        public EventWaitHandle _WaitHandle = new AutoResetEvent(false);//public handle - pass only const members to queue
        private EventWaitHandle _wh = new AutoResetEvent(false);
        private Task _workerTask;
        private Queue<T> _tasksQueue = new Queue<T>();
        private static object _locker = new object();
        public int counter; 

        public ProducerConsumerTest()
        {
            _workerTask = Task.Factory.StartNew(Work);
        }

        public void EnqueueTask(T task)
        {
            lock (_locker)
            {
                _tasksQueue.Enqueue(task);
                Interlocked.Increment(ref counter);
            }
            _wh.Set();
        }

        private void Work()
        {
            while (true)
            {
                T task = null;
                lock (_locker)
                {
                    if (_tasksQueue.Any())
                    {
                        task = _tasksQueue.Dequeue();
                        _WaitHandle.Set();
                        if (task == null) //last task
                        {
                            return;
                        }
                    }
                }

                if (task != null)
                {
                    Console.WriteLine($"Doing work on task {task.ToString()}");
                    Thread.Sleep(500); // imitation of work
                }
                else
                {
                    _wh.WaitOne(); //wait when producer thread add some task
                }
            }
        }

        public void Dispose()
        {
            _tasksQueue.Enqueue(null); // null task to stop work
            _workerTask.Wait(); // wait while work will finish
            _wh.Close();
        }
    }
}