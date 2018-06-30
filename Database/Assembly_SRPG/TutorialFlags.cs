namespace SRPG
{
    using System;

    [Flags]
    public enum TutorialFlags
    {
        GamePlay = 1,
        Magic = 2,
        Attack_1 = 4,
        Updown = 8,
        Evolution = 0x10,
        Equip = 0x20,
        UnitShard = 0x40,
        Ability = 0x80,
        Attack_2 = 0x100,
        Move = 0x200,
        Organize = 0x400,
        AutoBattle = 0x800,
        Mission = 0x1000,
        ConceptCard_1 = 0x2000,
        ConceptCard_2 = 0x4000,
        ConceptCard_3 = 0x8000,
        Tobira_1 = 0x10000,
        Tobira_2 = 0x20000
    }
}

