using System.Net;

namespace ArchivalTibiaV71LoginServer
{
    public class World
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }

        private IPAddress _ipAddress;
        public IPAddress IpAddress => _ipAddress ??= IPAddress.Parse(Ip);
    }
}