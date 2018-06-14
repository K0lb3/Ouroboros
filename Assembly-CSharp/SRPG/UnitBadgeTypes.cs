// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBadgeTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Flags]
  public enum UnitBadgeTypes
  {
    EnableEquipment = 1,
    EnableAwaking = 2,
    EnableRarityUp = 4,
    EnableJobRankUp = 8,
    EnableClassChange = 16, // 0x00000010
  }
}
