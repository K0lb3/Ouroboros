// Decompiled with JetBrains decompiler
// Type: SRPG.SkillFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public enum SkillFlags
  {
    EnableRankUp = 1,
    EnableChangeRange = 2,
    PierceAttack = 4,
    SelfTargetSelect = 8,
    ExecuteCutin = 16, // 0x00000010
    ExecuteInBattle = 32, // 0x00000020
    EnableHeightRangeBonus = 64, // 0x00000040
    EnableHeightParamAdjust = 128, // 0x00000080
    EnableUnitLockTarget = 256, // 0x00000100
    CastBreak = 512, // 0x00000200
    JewelAttack = 1024, // 0x00000400
    ForceHit = 2048, // 0x00000800
    Suicide = 4096, // 0x00001000
    SubActuate = 8192, // 0x00002000
    FixedDamage = 16384, // 0x00004000
    ForceUnitLock = 32768, // 0x00008000
    AllDamageReaction = 65536, // 0x00010000
    ShieldReset = 131072, // 0x00020000
    IgnoreElement = 262144, // 0x00040000
    PrevApply = 524288, // 0x00080000
    JudgeHpOver = 1048576, // 0x00100000
    MhmDamage = 2097152, // 0x00200000
    AcSelf = 4194304, // 0x00400000
    AcReset = 8388608, // 0x00800000
    HitTargetNumDiv = 16777216, // 0x01000000
    NoChargeCalcCT = 33554432, // 0x02000000
    JumpBreak = 67108864, // 0x04000000
  }
}
