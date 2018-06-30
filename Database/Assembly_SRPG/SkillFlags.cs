namespace SRPG
{
    using System;

    public enum SkillFlags
    {
        EnableRankUp = 1,
        EnableChangeRange = 2,
        PierceAttack = 4,
        SelfTargetSelect = 8,
        ExecuteCutin = 0x10,
        ExecuteInBattle = 0x20,
        EnableHeightRangeBonus = 0x40,
        EnableHeightParamAdjust = 0x80,
        EnableUnitLockTarget = 0x100,
        CastBreak = 0x200,
        JewelAttack = 0x400,
        ForceHit = 0x800,
        Suicide = 0x1000,
        SubActuate = 0x2000,
        FixedDamage = 0x4000,
        ForceUnitLock = 0x8000,
        AllDamageReaction = 0x10000,
        ShieldReset = 0x20000,
        IgnoreElement = 0x40000,
        PrevApply = 0x80000,
        JudgeHpOver = 0x100000,
        MhmDamage = 0x200000,
        AcSelf = 0x400000,
        AcReset = 0x800000,
        HitTargetNumDiv = 0x1000000,
        NoChargeCalcCT = 0x2000000,
        JumpBreak = 0x4000000
    }
}

