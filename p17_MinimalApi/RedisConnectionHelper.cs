using StackExchange.Redis;

namespace p17_MinimalApi;

public static class RedisConnectionHelper
{
    private static Lazy<ConnectionMultiplexer> _lazyConnection;

    static RedisConnectionHelper()
    {
        _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(Program.Config["Cache"]!));
    }

    public static ConnectionMultiplexer Connection => _lazyConnection.Value;
}