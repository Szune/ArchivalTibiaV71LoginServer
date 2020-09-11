﻿namespace ArchivalTibiaV71LoginServer
{
    public enum Receive : byte
    {
        AccountLogin = 0x01,
    }

    public enum Send : byte
    {
        CharacterList = 0x64,
        Sorry = 0xA,
        MotD = 0x14,
    }
}