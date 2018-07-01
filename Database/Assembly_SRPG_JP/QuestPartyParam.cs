// Decompiled with JetBrains decompiler
// Type: SRPG.QuestPartyParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class QuestPartyParam
  {
    public string iname;
    public PartySlotType type_1;
    public PartySlotType type_2;
    public PartySlotType type_3;
    public PartySlotType type_4;
    public PartySlotType support_type;
    public PartySlotType subtype_1;
    public PartySlotType subtype_2;
    public string unit_1;
    public string unit_2;
    public string unit_3;
    public string unit_4;
    public string subunit_1;
    public string subunit_2;
    public int l_npc_rare;

    public bool Deserialize(JSON_QuestPartyParam json)
    {
      this.iname = json.iname;
      this.type_1 = (PartySlotType) json.type_1;
      this.type_2 = (PartySlotType) json.type_2;
      this.type_3 = (PartySlotType) json.type_3;
      this.type_4 = (PartySlotType) json.type_4;
      this.support_type = (PartySlotType) json.support_type;
      this.subtype_1 = (PartySlotType) json.subtype_1;
      this.subtype_2 = (PartySlotType) json.subtype_2;
      this.unit_1 = json.unit_1;
      this.unit_2 = json.unit_2;
      this.unit_3 = json.unit_3;
      this.unit_4 = json.unit_4;
      this.subunit_1 = json.subunit_1;
      this.subunit_2 = json.subunit_2;
      this.l_npc_rare = json.l_npc_rare;
      return true;
    }

    public PartySlotTypeUnitPair[] GetMainSlots()
    {
      return new PartySlotTypeUnitPair[4]
      {
        new PartySlotTypeUnitPair()
        {
          Type = this.type_1,
          Unit = this.unit_1
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.type_2,
          Unit = this.unit_2
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.type_3,
          Unit = this.unit_3
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.type_4,
          Unit = this.unit_4
        }
      };
    }

    public PartySlotTypeUnitPair[] GetSubSlots()
    {
      return new PartySlotTypeUnitPair[2]
      {
        new PartySlotTypeUnitPair()
        {
          Type = this.subtype_1,
          Unit = this.subunit_1
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.subtype_2,
          Unit = this.subunit_2
        }
      };
    }

    public PartySlotTypeUnitPair[] GetMainSubSlots()
    {
      return new PartySlotTypeUnitPair[6]
      {
        new PartySlotTypeUnitPair()
        {
          Type = this.type_1,
          Unit = this.unit_1
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.type_2,
          Unit = this.unit_2
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.type_3,
          Unit = this.unit_3
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.type_4,
          Unit = this.unit_4
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.subtype_1,
          Unit = this.subunit_1
        },
        new PartySlotTypeUnitPair()
        {
          Type = this.subtype_2,
          Unit = this.subunit_2
        }
      };
    }

    public PartySlotTypeUnitPair[] GetSupportSlots()
    {
      return new PartySlotTypeUnitPair[1]
      {
        new PartySlotTypeUnitPair()
        {
          Type = this.support_type,
          Unit = (string) null
        }
      };
    }

    public string GetNpcLeaderUnitIname()
    {
      switch (this.type_1)
      {
        case PartySlotType.Npc:
        case PartySlotType.NpcHero:
          return this.unit_1;
        default:
          return (string) null;
      }
    }
  }
}
