namespace SRPG
{
    using System;

    [Flags]
    public enum UnlockTargets
    {
        Shop = 1,
        Cave = 2,
        Tour = 4,
        Tower = 8,
        Arena = 0x10,
        ShopTabi = 0x20,
        ShopKimagure = 0x40,
        ShopMonozuki = 0x80,
        MultiPlay = 0x100,
        UnitAwaking = 0x200,
        UnitEvolution = 0x400,
        EnhanceEquip = 0x800,
        EnhanceAbility = 0x1000,
        Artifact = 0x2000,
        ShopAwakePiece = 0x4000,
        LimitedShop = 0x8000,
        MultiVS = 0x10000,
        Ordeal = 0x20000,
        Gallery = 0x40000,
        ConceptCard = 0x80000,
        KeyQuest = 0x100000,
        EventShop = 0x200000,
        TowerQuest = 0x400000,
        RankMatch = 0x800000
    }
}

