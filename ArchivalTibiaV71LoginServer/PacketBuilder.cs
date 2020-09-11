using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ArchivalTibiaV71LoginServer
{
    public class PacketBuilder
    {
        private readonly byte[] _buffer = new byte[1024 * 4];
        private int _pos = 2;


        public PacketBuilder(Send id)
        {
            AddPacketId(id);
        }

        public void AddU16(ushort value)
        {
            // low order byte
            _buffer[_pos + 0] = (byte)(value & 0xFF);
            // high order byte
            _buffer[_pos + 1] = (byte)((value >> 8) & 0xFF);
            _pos += 2;
        }

        public void AddString(string s)
        {
            var sBytes = Encoding.ASCII.GetBytes(s);
            var sLen = sBytes.Length;
            AddU16((ushort)sLen);
            Array.Copy(sBytes, 0, _buffer, _pos, sLen);
            _pos += sLen;
        }

        public void Reset()
        {
            _pos = 2;
        }

        public void Send(Socket connection)
        {
            var packetSize = _pos - 2;
            _buffer[0] = (byte)(packetSize & 0xFF);
            _buffer[1] = (byte)((packetSize >> 8) & 0xFF);
            var sent = connection.Send(_buffer, 0, _pos, SocketFlags.None);
            #if DEBUG
            Console.Write("[");
            Console.Write(Enum.IsDefined(typeof(Send), _buffer[2]) ? ((Send)_buffer[2]).ToString() : $"0x{_buffer[2]:X2}");
            Console.WriteLine($"] Sent {_buffer.AsSpan().Slice(0, sent).ToHexString()}");
            #endif
            Reset();
        }

        public void AddU8(byte value)
        {
            _buffer[_pos] = value;
            _pos += 1;
        }

        public void AddPacketId(Send packet)
        {
            _buffer[_pos] = (byte)packet;
            _pos += 1;
        }

        public void AddBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                AddU8(bytes[i]);
            }
        }
    }
}