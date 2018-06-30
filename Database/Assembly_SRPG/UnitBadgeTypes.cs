namespace SRPG
{
    using System;

    [Flags]
    public enum UnitBadgeTypes
    {
        EnableEquipment = 1,
        EnableAwaking = 2,
        EnableRarityUp = 4,
        EnableJobRankUp = 8,
        EnableClassChange = 0x10
    }
}

