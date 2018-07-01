// Decompiled with JetBrains decompiler
// Type: SRPG.TutorialFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Flags]
  public enum TutorialFlags
  {
    GamePlay = 1,
    Magic = 2,
    Attack_1 = 4,
    Updown = 8,
    Evolution = 16, // 0x00000010
    Equip = 32, // 0x00000020
    UnitShard = 64, // 0x00000040
    Ability = 128, // 0x00000080
    Attack_2 = 256, // 0x00000100
    Move = 512, // 0x00000200
    Organize = 1024, // 0x00000400
    AutoBattle = 2048, // 0x00000800
    Mission = 4096, // 0x00001000
    ConceptCard_1 = 8192, // 0x00002000
    ConceptCard_2 = 16384, // 0x00004000
    ConceptCard_3 = 32768, // 0x00008000
    Tobira_1 = 65536, // 0x00010000
    Tobira_2 = 131072, // 0x00020000
  }
}
