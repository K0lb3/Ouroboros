// Decompiled with JetBrains decompiler
// Type: SRPG.UnlockTargets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Flags]
  public enum UnlockTargets
  {
    Shop = 1,
    Cave = 2,
    Tour = 4,
    Tower = 8,
    Arena = 16, // 0x00000010
    ShopTabi = 32, // 0x00000020
    ShopKimagure = 64, // 0x00000040
    ShopMonozuki = 128, // 0x00000080
    MultiPlay = 256, // 0x00000100
    UnitAwaking = 512, // 0x00000200
    UnitEvolution = 1024, // 0x00000400
    EnhanceEquip = 2048, // 0x00000800
    EnhanceAbility = 4096, // 0x00001000
    Artifact = 8192, // 0x00002000
    ShopAwakePiece = 16384, // 0x00004000
    LimitedShop = 32768, // 0x00008000
    MultiVS = 65536, // 0x00010000
    Ordeal = 131072, // 0x00020000
    Gallery = 262144, // 0x00040000
    ConceptCard = 524288, // 0x00080000
    KeyQuest = 1048576, // 0x00100000
    EventShop = 2097152, // 0x00200000
    TowerQuest = 4194304, // 0x00400000
    RankMatch = 8388608, // 0x00800000
  }
}
