namespace SRPG
{
    using System;

    public enum QuestMultiTypes : short
    {
        NOT_MULTI = 0,
        RAID = 1,
        VERSUS = 2,
        EVENT_TOP = 100,
        RAID_EVENT = 0x65,
        VERSUS_EVENT = 0x66,
        TOWER_EVENT = 0x67
    }
}

