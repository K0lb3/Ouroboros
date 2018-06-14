// Decompiled with JetBrains decompiler
// Type: SRPG.AIFlags
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public enum AIFlags
  {
    Positioning = 1,
    Sneaking = 2,
    DisableMove = 4,
    DisableAction = 8,
    DisableSkill = 16, // 0x00000010
    DisableAvoid = 32, // 0x00000020
    CastSkillFriendlyFire = 64, // 0x00000040
    DisableJewelAttack = 128, // 0x00000080
    SelfBuffOnly = 256, // 0x00000100
    DisableTargetPriority = 512, // 0x00000200
  }
}
