using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ArchivalTibiaV71LoginServer
{
    public static class Cache
    {
        private static Random Random { get; set; } = new Random();
        private static Dictionary<uint, Account> Accounts { get; set; } = new Dictionary<uint, Account>();
        private static Dictionary<int, World> Worlds { get; set; } = new Dictionary<int, World>();

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };

        private static MotD MotD { get; set; }

        public static void Load()
        {
            Worlds = TryLoadFile<List<World>>("worlds.json").ToDictionary(w => w.Id);
            Accounts = TryLoadFile<List<Account>>("accounts.json").ToDictionary(a => a.AccountNumber);
            MotD = TryLoadFile<MotD>("motd.json");
        }


        private static T TryLoadFile<T>(string fileName)
        {
            try
            {
                var json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<T>(json, JsonOptions);
            }
            catch (Exception ex)
            {
                File.AppendAllText("errors.log",
                    $"[{DateTimeOffset.UtcNow.ToString()}] Failed to load '{fileName}': {ex.Message}\n");
                Environment.Exit(-1);
                return default;
            }
        }

        public static Account GetAccount(uint accountNumber)
        {
            if (Accounts.TryGetValue(accountNumber, out var acc))
                return acc;
            return null;
        }

        public static World GetWorld(int world)
        {
            if (Worlds.TryGetValue(world, out var w))
                return w;
            return null;
        }

        public static MotD GetMotD()
        {
            if (MotD == null)
                return null;
            if (MotD.Id != -1)
                return MotD;
            return new MotD
            {
                Id = Random.Next(1, 2000),
                Message = MotD.Message
            };
        }
    }
}