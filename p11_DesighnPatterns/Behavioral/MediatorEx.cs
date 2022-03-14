using System;

namespace p11_DesighnPatterns.Behavioral
{
    public class MediatorEx
    {
        public static void Start()
        {
            var reader = new LogFileReader();
            var saver = new LogSaver();
            new Mediator(reader, saver);
            reader.ReadLog();
        }
    }
    
    public class LogFileReader
    {
        public event Action<string> LogReaded;
        public void ReadLog()
        {
            Console.WriteLine("log was read");
            var log = "log1";
            LogReaded?.Invoke(log);
        }
    }

    public class LogSaver
    {
        public void SaveLogEntry(LogEntry entry)
        {
            Console.WriteLine($"Log with name {entry.Name} saved");
        }
    }

    public class LogEntry
    {
        public LogEntry(string log)
        {
            Name = log;
        }
        public string Name { get; set; }
    }

    public class Mediator
    {
        private readonly LogFileReader _fileReader;
        private readonly LogSaver _saver;

        public Mediator(LogFileReader fileReader, LogSaver saver)
        {
            _fileReader = fileReader;
            _saver = saver;
            _fileReader.LogReaded += ImportLog;
        }
        public void ImportLog(string log)
        {
            var entry = new LogEntry(log);
            _saver.SaveLogEntry(entry);
        }
    }
}