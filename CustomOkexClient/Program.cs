using System;
using System.IO;
using CustomOkexClient.Objects.Config;
using Newtonsoft.Json;

namespace CustomOkexClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var appSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var appSettingsJson = File.ReadAllText(appSettingsFilePath);
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(appSettingsJson);
            
            var okexClient = new CustomOkexClient(appSettings.OkexApiCredentials);
            var deposits = okexClient.GetCexPendingDeposits();
            foreach (var deposit in deposits)
            {
                Console.WriteLine($"{deposit.Tx}\t{deposit.AssetName}\t{deposit.Amount}");
            }
        }
    }
}