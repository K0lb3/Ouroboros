// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBadgeTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
