namespace SRPG
{
    using System;

    public enum QuestTypes : byte
    {
        Story = 0,
        Multi = 1,
        Arena = 2,
        Tutorial = 3,
        Free = 4,
        Event = 5,
        Character = 6,
        Tower = 7,
        VersusFree = 8,
        VersusRank = 9,
        Gps = 10,
        Extra = 11,
        MultiTower = 12,
        Beginner = 13,
        MultiGps = 14,
        Ordeal = 15,
        RankMatch = 0x10,
        None = 0x7f
    }
}

