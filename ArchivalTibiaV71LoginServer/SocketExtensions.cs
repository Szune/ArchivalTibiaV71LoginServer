using System.Net;
using System.Net.Sockets;

namespace ArchivalTibiaV71LoginServer
{
    public static class SocketExtensions
    {
        public static void Start(this Socket connection, int port)
        {
            var endpoint = new IPEndPoint(IPAddress.Any, port);
            connection.Bind(endpoint);
            connection.Listen(0);
        }
        
        public static void Sorry(this Socket connection, string message)
        {
            var builder = new PacketBuilder(Send.Sorry);
            builder.AddString(message);
            builder.Send(connection);
        }
        
        /// <summary>
        /// Message of the day has to be followed by either a "sorry" or the character list, otherwise the client gets stuck.
        /// </summary>
        public static void MotD(this Socket connection, ushort id, string message)
        {
            var builder = new PacketBuilder(Send.MotD);
            builder.AddString($"+{id}\n{message}");
            builder.Send(connection);
        }
    }
}