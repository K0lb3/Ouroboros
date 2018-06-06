// Decompiled with JetBrains decompiler
// Type: SRPG.SkillFlags
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
  }
}
