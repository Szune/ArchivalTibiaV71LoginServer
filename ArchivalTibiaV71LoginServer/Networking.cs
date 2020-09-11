using System;
using System.Net.Sockets;

namespace ArchivalTibiaV71LoginServer
{
    public static class Networking
    {
        public static void Start()
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Start(7171);
            while (true)
            {
                try
                {
                    Console.WriteLine("[Waiting for account logins]");
                    AcceptReceiveLoop(socket);
                }
                catch(Exception ex)
                {
                    // accept new socket
                    Console.WriteLine($"Error during main loop: {ex}");
                }
            }
        }

        private static void AcceptReceiveLoop(Socket socket)
        {
            var connection = socket.Accept();
            var buffer = new byte[1024 * 1]; 
            var received = connection.Receive(buffer);
            if (received < 1) 
                return;
            HandleMessage(buffer, connection);
            connection.Dispose(); // you only get one shot
        }

        private static void HandleMessage(ReadOnlySpan<byte> buffer, Socket connection)
        {
            // could probably use memory<T> or span<T> here for better performance
            var reader = new PacketReader(buffer);
            reader.Skip(2); // skipping length, for now, stop being lazy at some point
            var packetId = (Receive) reader.ReadU8();
            if (packetId != Receive.AccountLogin)
                return;
            HandleAccountLogin(connection, reader);
        }

        private static void HandleAccountLogin(Socket connection, PacketReader reader)
        {
            // account login packet
            Console.WriteLine("[Received account login packet]");
            var os = reader.ReadU16();
            var client = reader.ReadU16();
            var datVersion = reader.ReadU32();
            var sprVersion = reader.ReadU32();
            var picVersion = reader.ReadU32();
            // Console.WriteLine(
            //     $">Client information:\nVersion: {client}\nOS: {os}\n.spr version: {sprVersion}\n.dat version: {datVersion}\n.pic version: {picVersion}");
            var accountNumber = reader.ReadU32();
            var password = reader.ReadString();
            var account = Cache.GetAccount(accountNumber);
            if (account == null || account.Password != password)
            {
                Console.WriteLine($" -- Login failed with account number {accountNumber}");
                connection.Sorry("Wrong account number or password.");
            }
            else
            {
                Console.WriteLine($" > Account number {accountNumber} logged in");
                var motd = Cache.GetMotD();
                if(motd != null)
                    connection.MotD((ushort)motd.Id, motd.Message);
                SendCharacterList(connection, account);
            }
        }

        private static void SendCharacterList(Socket connection, Account account)
        {
            var builder = new PacketBuilder(Send.CharacterList);
            if (account.Characters != null)
            {
                builder.AddU8((byte) account.Characters.Length);
                for (int i = 0; i < account.Characters.Length; i++)
                {
                    var world = Cache.GetWorld(account.Characters[i].World);
                    if (world == null)
                    {
                        Console.WriteLine($"Character '{account.Characters[i].Name}' has invalid world id: {account.Characters[i].World}");
                        return;
                    }
                    builder.AddString(account.Characters[i].Name);
                    builder.AddString(world.Name);
                    var ip = world.IpAddress.GetAddressBytes();
                    builder.AddBytes(ip);
                    builder.AddU16((ushort) world.Port);
                }

            }

            builder.AddU16(account.PremiumDays);
            builder.Send(connection);
        }
    }
}