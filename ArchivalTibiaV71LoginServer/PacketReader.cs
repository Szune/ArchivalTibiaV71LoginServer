using System;
using System.Text;

namespace ArchivalTibiaV71LoginServer
{
    public ref struct PacketReader
    {
        private readonly ReadOnlySpan<byte> _bytes;
        private int _pos;

        public PacketReader(ReadOnlySpan<byte> bytes)
        {
            _bytes = bytes;
            _pos = 0;
        }

        public ushort ReadU16()
        {
            ushort value = (ushort) (_bytes[_pos + 0] + (_bytes[_pos + 1] << 8));
            _pos += 2;
            return value;
        }

        public uint ReadU32()
        {
            uint value = (uint) _bytes[_pos + 0] + ((uint) _bytes[_pos + 1] << 8) + ((uint) _bytes[_pos + 2] << 16) +
                         ((uint) _bytes[_pos + 3] << 24);
            _pos += 4;
            return value;
        }

        public byte ReadU8()
        {
            byte value = _bytes[_pos];
            _pos += 1;
            return value;
        }

        private string ReadPackedAsciiString(int length)
        {
            // rewrite for better performance
            var value = Encoding.ASCII.GetString(_bytes.Slice(_pos, length));
            _pos += length;
            return value;
        }

        public string ReadString()
        {
            var length = ReadU16();
            return ReadPackedAsciiString(length);
        }

        public void SkipHeader()
        {
            Skip(3);
        }

        public void Skip(int bytes)
        {
            _pos += bytes;
        }
    }
}