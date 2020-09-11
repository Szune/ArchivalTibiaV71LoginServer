namespace ArchivalTibiaV71LoginServer
{
    public class Account
    {
        public uint AccountNumber { get; set; }
        public string Password { get; set; } // add encryption for storage pls
        public Character[] Characters { get; set; }
        public ushort PremiumDays { get; set; }
    }
}