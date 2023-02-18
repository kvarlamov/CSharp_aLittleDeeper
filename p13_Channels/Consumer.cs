using System.Threading.Channels;

namespace p13_Channels;

public class Consumer<T> where T: class
{
    private readonly ChannelReader<T> _reader;

    public Consumer(ChannelReader<T> reader)
    {
        _reader = reader;
    }

    public async ValueTask Read()
    {
        try
        {
            while (true)
            {
                await foreach (var msg in _reader.ReadAllAsync())
                {
                    if (typeof(T) != typeof(string))
                        throw new NotImplementedException();
            
                    Console.WriteLine($"your message: {msg}");
                }
            }
        }
        catch (ChannelClosedException)
        {
            Console.WriteLine("Channel was closed");
        }
    }
}