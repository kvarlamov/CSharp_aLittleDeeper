using System;
using System.IO;
using System.Runtime;
using Microsoft.Extensions.Configuration;

namespace p10_DotNetDbTools
{
    public static class Helper
    {
        public static string GetConnectionString()
        {
            // var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json");
            // var configuration = builder.Build();
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",true,true).Build();
            return configuration["ConnectionStrings:practiceDb"];
        }
    }
}