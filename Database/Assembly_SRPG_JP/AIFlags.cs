// Decompiled with JetBrains decompiler
// Type: SRPG.AIFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
    UseOldSort = 1024, // 0x00000400
  }
}
