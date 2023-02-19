using System.Threading.Channels;

namespace p13_Channels;

public class Consumer
{
    private readonly ChannelReader<Envelope> _reader;

    public Consumer(ChannelReader<Envelope> reader)
    {
        _reader = reader;
    }

    public async ValueTask Read()
    {
        while (true)
        {
            await foreach (var data in _reader.ReadAllAsync())
            {
                Console.WriteLine($"your message: {data.Data}");
            }
        }
    }
}