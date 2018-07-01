// Decompiled with JetBrains decompiler
// Type: SRPG.SkillFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
  }
}
