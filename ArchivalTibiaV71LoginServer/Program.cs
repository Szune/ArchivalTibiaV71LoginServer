using System;

namespace ArchivalTibiaV71LoginServer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ArchivalTibiaV71LoginServer";
            Console.WriteLine(".-----------------------------------.");
            Console.WriteLine("| Archival Tibia 7.1 Login Server: Online |");
            Console.WriteLine("'-----------------------------------'");
            Cache.Load();
            Networking.Start();
        }
    }
}
