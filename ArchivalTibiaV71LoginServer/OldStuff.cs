namespace ArchivalTibiaV71LoginServer
{
    public class OldStuff
    {

            // do
            // {
                // var buffer = new byte[1024 * 1];
                // received = connection.Receive(buffer);
                // if (received > 0)
                // {
                //     if(!HandleMessage(buffer, connection))
                //         connection.Dispose();
                // }
            // } while (received > 0);
            
        // /// <summary>
        // /// Only valid before attempting to login a character
        // /// </summary>
        // public enum DialogPacket : byte
        // {
        //     Info = 0x08,
        //     Error = 0x0A,
        //     /// <summary>
        //     /// Message of the day
        //     /// </summary>
        //     Motd = 0x14,
        // }
        // private static void SendCharacterList(Socket connection)
        // {
        //     var characters = new List<(string name, string world, IPAddress ip, int port)>
        //     {
        //         ("erki", "erkiworld", IPAddress.Parse("127.0.0.1"), 7172),
        //         ("gm erki", "erkiworld", IPAddress.Parse("127.0.0.1"), 7172)
        //     };
        //     const string msg = "welcome to welcome!";
        //     var msgBytes = PackString(msg);
        //     var msgLen = msgBytes.Length;
        //     //var bytes = new byte[] { 0x05, 0x00, 0x0A, 0x02, 0x00, (byte)'F', (byte)'U' };
        //     var packetLen = 1 + 2 + msgLen + 1; // packet type, string length, string bytes, character list dialog
        //     var bytes = new byte[4 * 1024];
        //     bytes[0] = (byte) ((uint) packetLen & 0xFF); // packet length
        //     bytes[1] = (byte) (((uint) packetLen >> 8) & 0xFF); // packet length
        //     bytes[2] = 0x14; // message of the day message
        //     Array.Copy(msgBytes, 0, bytes, 3, msgLen);
        //     bytes[3 + msgLen] = 0x64; // character list dialog
        //     bytes[3 + msgLen + 1] = (byte) characters.Count;
        //     var cPos = 0;
        //     for (int i = 0; i < characters.Count; i++)
        //     {
        //         var name = PackString(characters[i].name);
        //         Array.Copy(name, 0, bytes, 3 + msgLen + 2 + cPos, name.Length);
        //         var world = PackString(characters[i].world);
        //         Array.Copy(world, 0, bytes, 3 + msgLen + 2 + cPos + name.Length, world.Length);
        //         var ip = characters[i].ip.GetAddressBytes();
        //         Array.Copy(ip, 0, bytes, 3 + msgLen + 2 + cPos + name.Length + world.Length + 0, ip.Length);
        //         bytes[3 + msgLen + 2 + cPos + name.Length + world.Length + 4] =
        //             (byte) ((uint) characters[i].port & 0xFF);
        //         bytes[3 + msgLen + 2 + cPos + name.Length + world.Length + 5] =
        //             (byte) (((uint) characters[i].port >> 8) & 0xFF);
        //         cPos += name.Length + world.Length + 6;
        //     }
        //
        //     bytes[3 + msgLen + 2 + cPos + 0] = 0x01; // prem
        //     bytes[3 + msgLen + 2 + cPos + 1] = 0x00;
        //     var buffer = new byte[3 + msgLen + 2 + cPos + 2];
        //     Array.Copy(bytes, 0, buffer, 0, buffer.Length);
        //     buffer[0] = (byte) ((uint) (buffer.Length - 2) & 0xFF); // packet length
        //     buffer[1] = (byte) (((uint) (buffer.Length - 2) >> 8) & 0xFF); // packet length
        //     Send(connection, buffer);
        // }
        //
        //
        // private static byte[] PackString(string s)
        // {
        //     var sBytes = Encoding.ASCII.GetBytes(s);
        //     var sLen = sBytes.Length;
        //     var bytes = new byte[2 + sLen];
        //     bytes[0] = (byte) ((uint) sLen & 0xFF); // string length
        //     bytes[1] = (byte) (((uint) sLen >> 8) & 0xFF); // string length
        //     Array.Copy(sBytes, 0, bytes, 2, sLen);
        //     return bytes;
        // }
        //
        // private static void SendDialog(Socket connection, string msg, DialogPacket dialog)
        // {
        //     var msgBytes = Encoding.ASCII.GetBytes(msg);
        //     var msgLen = msgBytes.Length;
        //     //var bytes = new byte[] { 0x05, 0x00, 0x0A, 0x02, 0x00, (byte)'F', (byte)'U' };
        //     var packetLen = 1 + 2 + msgLen; // packet type, string length, string bytes
        //     var bytes = new byte[2 + packetLen];
        //     bytes[0] = (byte) ((uint) packetLen & 0xFF);
        //     bytes[1] = (byte) (((uint) packetLen >> 8) & 0xFF);
        //     bytes[2] = (byte) dialog;
        //     bytes[3] = (byte) ((uint) msgLen & 0xFF);
        //     bytes[4] = (byte) (((uint) msgLen >> 8) & 0xFF);
        //     Array.Copy(msgBytes, 0, bytes, 5, msgLen);
        //     Send(connection, bytes);
        // }
        //
        //
        // static void Send(Socket connection, byte[] bytes)
        // {
        //     Console.WriteLine($"Sending {bytes.ToHexString()}");
        //     connection.Send(bytes);
        // }
    }
}