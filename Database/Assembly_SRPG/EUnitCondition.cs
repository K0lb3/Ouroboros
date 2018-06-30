namespace SRPG
{
    using System;

    public enum EUnitCondition : long
    {
        Poison = 1L,
        Paralysed = 2L,
        Stun = 4L,
        Sleep = 8L,
        Charm = 0x10L,
        Stone = 0x20L,
        Blindness = 0x40L,
        DisableSkill = 0x80L,
        DisableMove = 0x100L,
        DisableAttack = 0x200L,
        Zombie = 0x400L,
        DeathSentence = 0x800L,
        Berserk = 0x1000L,
        DisableKnockback = 0x2000L,
        DisableBuff = 0x4000L,
        DisableDebuff = 0x8000L,
        Stop = 0x10000L,
        Fast = 0x20000L,
        Slow = 0x40000L,
        AutoHeal = 0x80000L,
        Donsoku = 0x100000L,
        Rage = 0x200000L,
        GoodSleep = 0x400000L,
        AutoJewel = 0x800000L,
        DisableHeal = 0x1000000L,
        DisableSingleAttack = 0x2000000L,
        DisableAreaAttack = 0x4000000L,
        DisableDecCT = 0x8000000L,
        DisableIncCT = 0x10000000L,
        DisableEsaFire = 0x20000000L,
        DisableEsaWater = 0x40000000L,
        DisableEsaWind = 0x80000000L,
        DisableEsaThunder = 0x100000000L,
        DisableEsaShine = 0x200000000L,
        DisableEsaDark = 0x400000000L,
        DisableMaxDamageHp = 0x800000000L,
        DisableMaxDamageMp = 0x1000000000L
    }
}

