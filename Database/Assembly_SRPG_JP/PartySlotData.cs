// Decompiled with JetBrains decompiler
// Type: SRPG.PartySlotData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class PartySlotData
  {
    public PartySlotType Type;
    public PartySlotIndex Index;
    public string UnitName;
    public bool IsSettable;

    public PartySlotData(PartySlotType type, string unitName, PartySlotIndex index, bool isSettable = false)
    {
      this.Type = type;
      this.Index = index;
      this.UnitName = unitName;
      this.IsSettable = isSettable;
    }

    public override string ToString()
    {
      string str = nameof (PartySlotData) + "\n" + "    枠 : ";
      switch (this.Type)
      {
        case PartySlotType.Free:
          str += "自由";
          break;
        case PartySlotType.Locked:
          str += "出撃不可";
          break;
        case PartySlotType.Forced:
          str += "強制出撃";
          break;
        case PartySlotType.ForcedHero:
          str += "強制出撃(主人公)";
          break;
        case PartySlotType.Npc:
          str += "NPC";
          break;
        case PartySlotType.NpcHero:
          str += "NPC(主人公)";
          break;
      }
      return str + "\n" + "    要素 : " + Enum.GetName(typeof (PartySlotIndex), (object) this.Index) + "\n" + "    ユニット名 : " + this.UnitName;
    }
  }
}
