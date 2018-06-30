namespace SRPG
{
    using System;

    public enum GiftTypes : long
    {
        Item = 1L,
        Gold = 2L,
        Coin = 4L,
        ArenaCoin = 8L,
        MultiCoin = 0x10L,
        KakeraCoin = 0x20L,
        Artifact = 0x40L,
        Unit = 0x80L,
        SelectUnitItem = 0x100L,
        SelectItem = 0x200L,
        SelectArtifactItem = 0x400L,
        Award = 0x800L,
        ConceptCard = 0x1000L,
        SelectConceptCardItem = 0x2000L,
        SelectSummonTickets = 0x2700L,
        IgnoreReceiveAll = 0x2780L
    }
}

