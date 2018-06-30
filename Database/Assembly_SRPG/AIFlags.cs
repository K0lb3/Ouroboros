namespace SRPG
{
    using System;

    public enum AIFlags
    {
        Positioning = 1,
        Sneaking = 2,
        DisableMove = 4,
        DisableAction = 8,
        DisableSkill = 0x10,
        DisableAvoid = 0x20,
        CastSkillFriendlyFire = 0x40,
        DisableJewelAttack = 0x80,
        SelfBuffOnly = 0x100,
        DisableTargetPriority = 0x200,
        UseOldSort = 0x400
    }
}

