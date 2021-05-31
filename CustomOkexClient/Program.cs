using System;
using System.IO;
using CustomCexWrapper.Objects.Config;
using Newtonsoft.Json;

namespace CustomCexWrapper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var appSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var appSettingsJson = File.ReadAllText(appSettingsFilePath);
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(appSettingsJson);
            
            var okexClient = new CustomOkexClient(appSettings.OkexApiCredentials, false);
            
            Console.WriteLine("\tFutures Order Books");
            var orderBooks = okexClient.GetFuturesUsdtOrderBooks();
            foreach (var entry in orderBooks)
            {
                Console.WriteLine($"\t{entry.Key}");
                Console.WriteLine(entry.Value);
            }
        }
    }
}