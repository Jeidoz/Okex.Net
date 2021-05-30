using System;
using System.IO;
using System.Linq;
using CustomOkexClient.Objects.Config;
using CustomOkexClient.RestObjects.Common;
using Newtonsoft.Json;

namespace CustomOkexClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var appSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.demo.json");
            var appSettingsJson = File.ReadAllText(appSettingsFilePath);
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(appSettingsJson);
            
            var okexClient = new CustomOkexClient(appSettings.OkexApiCredentials, true);
            var instruments = okexClient.GetInstrumentsWithOpenContracts(InstrumentType.FUTURES);
            foreach (var tradeInstrument in instruments)
            {
                Console.WriteLine(
                    $"ID: {tradeInstrument.Id} — {tradeInstrument.SettlementAndMarginCurrency} — {tradeInstrument.ContractMul} — {tradeInstrument.Status.ToString()}");
            }
        }
    }
}