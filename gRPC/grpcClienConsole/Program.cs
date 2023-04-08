// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using grpcClienConsole;

Console.WriteLine("Insert source currency, currency to convert, value separated by comma");
var str = Console.ReadLine();

var input = str.Split(',');

Currency sourceCurrency = Enum.Parse<Currency>(input[0]);
Currency destCurrency = Enum.Parse<Currency>(input[1]);
double val = double.Parse(input[2]);

// создаем канал для обмена сообщениями с сервером
// параметр - адрес сервера gRPC
using var channel = GrpcChannel.ForAddress("http://localhost:5051");

// создаем клиент
var client = new Converter.ConverterClient(channel);

ConvertRequest request = new ConvertRequest()
{
    SourceCurrency = sourceCurrency,
    CurrencyToConvert = destCurrency,
    SourceValue = val
};

var response = await client.ConvertAsync(request);

Console.WriteLine($"{input[2]} {sourceCurrency} = {response.ConvertedValue} {destCurrency}");
Console.ReadKey();
