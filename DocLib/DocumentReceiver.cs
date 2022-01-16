using System;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

namespace DocLib
{
    public class DocumentReceiver
    {
        private string _path;
        private double _interval;
        private DateTime _startTime;
        static object locker = new object();
        private Timer _timer;
        private FileSystemWatcher _watcher;

        public event Action DocumentsReady;
        public event Action TimedOut;

        private void Stop()
        {
            DocumentsReady?.Invoke();
            Unsubscribe();
        }

        private void StopByTime()
        {
            TimedOut?.Invoke();
            Unsubscribe();
        }

        public void Start(string path, double waitingInterval)
        {            
            _path = path;
            _interval = waitingInterval;
            _startTime = DateTime.Now;
            if (Directory.Exists(path))
                Directory.Delete(path);
            Directory.CreateDirectory(path);

            _watcher = new FileSystemWatcher(path);
            _watcher.Changed += OnChanged;
            _watcher.EnableRaisingEvents = true;
            _timer = new Timer(1) {Enabled = true};
            _timer.Start();
            _timer.Elapsed += OnTimedOut;
        }

        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (Directory.GetFiles(_path).Length >= 3)
            {
                Stop();
            }
        }

        public void OnTimedOut(object sender, ElapsedEventArgs e)
        {
            lock (locker)
            {
                if ((e.SignalTime - _startTime).Seconds > _interval)
                {
                    StopByTime();
                }
            }
        }

        public void Unsubscribe()
        {
            _timer.Elapsed -= OnTimedOut;
            _watcher.Changed -= OnChanged;
            _timer.Dispose();
            _watcher.Dispose();
        }
    }
}