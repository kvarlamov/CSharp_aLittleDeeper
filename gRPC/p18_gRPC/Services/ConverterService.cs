using Grpc.Core;

namespace p18_gRPC.Services;

public class ConverterService : Converter.ConverterBase
{
    // key - source currency
    private readonly Dictionary<KeyValuePair<Currency, Currency>, Func<double, double>> converters = new()
    {
        {new KeyValuePair<Currency, Currency>(Currency.Rub, Currency.Usd), srcVal => srcVal / 85},
        {new KeyValuePair<Currency, Currency>(Currency.Rub, Currency.Euro), srcVal => srcVal / 75},
        {new KeyValuePair<Currency, Currency>(Currency.Usd, Currency.Rub), srcVal => srcVal * 75},
        {new KeyValuePair<Currency, Currency>(Currency.Euro, Currency.Rub), srcVal => srcVal * 85},
    };

    public override Task<ConvertReply> Convert(ConvertRequest request, ServerCallContext context)
    {
        if (request.SourceCurrency == Currency.Unknown || request.CurrencyToConvert == Currency.Unknown)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Некорректно указана валюта"));
        
        var kvp = new KeyValuePair<Currency, Currency>(request.SourceCurrency, request.CurrencyToConvert);
        
        if (!converters.ContainsKey(kvp))
            throw new RpcException(new Status(StatusCode.Unimplemented, "Данный вид конвертации не поддерживается"));

        return Task.FromResult(new ConvertReply()
        {
            SourceCurrency = request.SourceCurrency,
            CurrencyToConvert = request.CurrencyToConvert,
            SourceValue = request.SourceValue,
            ConvertedValue = converters[kvp](request.SourceValue)
        });
    }
}