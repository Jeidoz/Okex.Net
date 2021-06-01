using System.IO;
using CustomCexWrapper.Objects.Config;
using CustomCexWrapper.RestObjects.Common;
using Newtonsoft.Json;

namespace CustomCexWrapper
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var appSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.demo.json");
            var appSettingsJson = File.ReadAllText(appSettingsFilePath);
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(appSettingsJson);

            var okexClient = new CustomOkexClient(appSettings.OkexApiCredentials, true);
            var order = okexClient.FuturesPlaceOrderByMarket("TRX-USDT-210611", CustomOrderSide.Buy, 1m);
        }
    }
}