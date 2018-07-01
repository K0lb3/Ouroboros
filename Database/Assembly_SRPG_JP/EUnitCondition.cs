// Decompiled with JetBrains decompiler
// Type: SRPG.EUnitCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public enum EUnitCondition : long
  {
    Poison = 1,
    Paralysed = 2,
    Stun = 4,
    Sleep = 8,
    Charm = 16, // 0x0000000000000010
    Stone = 32, // 0x0000000000000020
    Blindness = 64, // 0x0000000000000040
    DisableSkill = 128, // 0x0000000000000080
    DisableMove = 256, // 0x0000000000000100
    DisableAttack = 512, // 0x0000000000000200
    Zombie = 1024, // 0x0000000000000400
    DeathSentence = 2048, // 0x0000000000000800
    Berserk = 4096, // 0x0000000000001000
    DisableKnockback = 8192, // 0x0000000000002000
    DisableBuff = 16384, // 0x0000000000004000
    DisableDebuff = 32768, // 0x0000000000008000
    Stop = 65536, // 0x0000000000010000
    Fast = 131072, // 0x0000000000020000
    Slow = 262144, // 0x0000000000040000
    AutoHeal = 524288, // 0x0000000000080000
    Donsoku = 1048576, // 0x0000000000100000
    Rage = 2097152, // 0x0000000000200000
    GoodSleep = 4194304, // 0x0000000000400000
    AutoJewel = 8388608, // 0x0000000000800000
    DisableHeal = 16777216, // 0x0000000001000000
    DisableSingleAttack = 33554432, // 0x0000000002000000
    DisableAreaAttack = 67108864, // 0x0000000004000000
    DisableDecCT = 134217728, // 0x0000000008000000
    DisableIncCT = 268435456, // 0x0000000010000000
    DisableEsaFire = 536870912, // 0x0000000020000000
    DisableEsaWater = 1073741824, // 0x0000000040000000
    DisableEsaWind = 2147483648, // 0x0000000080000000
    DisableEsaThunder = 4294967296, // 0x0000000100000000
    DisableEsaShine = 8589934592, // 0x0000000200000000
    DisableEsaDark = 17179869184, // 0x0000000400000000
    DisableMaxDamageHp = 34359738368, // 0x0000000800000000
    DisableMaxDamageMp = 68719476736, // 0x0000001000000000
  }
}
