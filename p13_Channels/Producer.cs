using System.Threading.Channels;

namespace p13_Channels;

public class Producer<T> where T : class
{
    private readonly ChannelWriter<T> _writer;

    public Producer(ChannelWriter<T> writer)
    {
        _writer = writer;
    }

    public async ValueTask Write()
    {
        while (true)
        {
            Console.WriteLine("Insert message");
            var msg = Console.ReadLine();
            if (typeof(T) != typeof(string))
                throw new NotImplementedException();
            
            if (msg is T converted) 
                await _writer.WriteAsync(converted);

            Task.Delay(500);
        }
    }
}