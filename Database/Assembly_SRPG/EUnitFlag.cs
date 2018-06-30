namespace SRPG
{
    using System;

    public enum EUnitFlag
    {
        Entried = 1,
        Moved = 2,
        Action = 4,
        Searched = 8,
        Defended = 0x10,
        SideAttack = 0x20,
        BackAttack = 0x40,
        Escaped = 0x80,
        Sneaking = 0x100,
        Paralysed = 0x200,
        ForceMoved = 0x400,
        ForceEntried = 0x800,
        ForceAuto = 0x1000,
        EntryDead = 0x2000,
        FirstAction = 0x4000,
        DisableFirstVoice = 0x8000,
        DamagedActionStart = 0x10000,
        TriggeredAutoSkills = 0x20000,
        DisableUnitChange = 0x40000,
        UnitChanged = 0x80000,
        UnitWithdraw = 0x100000,
        CreatedBreakObj = 0x200000,
        Reinforcement = 0x400000,
        ToDying = 0x800000,
        OtherTeam = 0x1000000,
        IsHelp = 0x2000000
    }
}

