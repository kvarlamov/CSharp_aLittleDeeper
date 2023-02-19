using System.Threading.Channels;

namespace p13_Channels;

public class Producer
{
    private readonly ChannelWriter<Envelope> _writer;

    public Producer(ChannelWriter<Envelope> writer)
    {
        _writer = writer;
    }

    public async ValueTask Write()
    {
        while (true)
        {
            Console.WriteLine("Insert message");
            var msg = Console.ReadLine();
            
            await _writer.WriteAsync(new Envelope(msg));

            Thread.Sleep(500);
        }
    }
}