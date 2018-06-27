// Decompiled with JetBrains decompiler
// Type: SRPG.PartyData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

namespace SRPG
{
  public class PartyData
  {
    private int mMAX_UNIT;
    private int mMAX_MAINMEMBER;
    private int mMAX_SUBMEMBER;
    private int mMAINMEMBER_START;
    private int mMAINMEMBER_END;
    private int mSUBMEMBER_START;
    private int mSUBMEMBER_END;
    private string mName;
    private long[] mUniqueIDs;
    private int mLeaderIndex;

    public PartyData(PlayerPartyTypes type)
    {
      switch (type)
      {
        case PlayerPartyTypes.Tower:
          this.mMAX_UNIT = 7;
          this.mMAX_MAINMEMBER = 5;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = 0 + this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
        case PlayerPartyTypes.Versus:
          this.mMAX_UNIT = 5;
          this.mMAX_MAINMEMBER = 5;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = 0 + this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
        case PlayerPartyTypes.MultiTower:
          this.mMAX_UNIT = 3;
          this.mMAX_MAINMEMBER = 2;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = 0 + this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
        default:
          this.mMAX_UNIT = 6;
          this.mMAX_MAINMEMBER = 4;
          this.mMAX_SUBMEMBER = this.MAX_UNIT - this.MAX_MAINMEMBER;
          this.mMAINMEMBER_START = 0;
          this.mMAINMEMBER_END = 0 + this.MAX_MAINMEMBER - 1;
          this.mSUBMEMBER_START = this.MAX_MAINMEMBER;
          this.mSUBMEMBER_END = this.SUBMEMBER_START + this.MAX_SUBMEMBER - 1;
          break;
      }
      this.mUniqueIDs = new long[this.MAX_UNIT];
    }

    public int MAX_UNIT
    {
      get
      {
        return this.mMAX_UNIT;
      }
    }

    public int MAX_MAINMEMBER
    {
      get
      {
        return this.mMAX_MAINMEMBER;
      }
    }

    public int MAX_SUBMEMBER
    {
      get
      {
        return this.mMAX_SUBMEMBER;
      }
    }

    public int MAINMEMBER_START
    {
      get
      {
        return this.mMAINMEMBER_START;
      }
    }

    public int MAINMEMBER_END
    {
      get
      {
        return this.mMAINMEMBER_END;
      }
    }

    public int SUBMEMBER_START
    {
      get
      {
        return this.mSUBMEMBER_START;
      }
    }

    public int SUBMEMBER_END
    {
      get
      {
        return this.mSUBMEMBER_END;
      }
    }

    public string Name
    {
      get
      {
        return this.mName;
      }
      set
      {
        this.mName = value;
      }
    }

    public int Num
    {
      get
      {
        int num = 0;
        for (int index = 0; index < this.mUniqueIDs.Length; ++index)
        {
          if (this.mUniqueIDs[index] != 0L)
            ++num;
        }
        return num;
      }
    }

    public int LeaderIndex
    {
      get
      {
        return this.mLeaderIndex;
      }
    }

    public PlayerPartyTypes PartyType { set; get; }

    public bool Selected { set; get; }

    public bool IsDefense { set; get; }

    public static PlayerPartyTypes GetPartyTypeFromString(string ptype)
    {
      string key = ptype;
      if (key != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (PartyData.\u003C\u003Ef__switch\u0024map10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          PartyData.\u003C\u003Ef__switch\u0024map10 = new Dictionary<string, int>(9)
          {
            {
              "norm",
              0
            },
            {
              "ev",
              1
            },
            {
              "mul",
              2
            },
            {
              "col",
              3
            },
            {
              "coldef",
              4
            },
            {
              "chara",
              5
            },
            {
              "tower",
              6
            },
            {
              "vs",
              7
            },
            {
              "multi_tw",
              8
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (PartyData.\u003C\u003Ef__switch\u0024map10.TryGetValue(key, out num))
        {
          switch (num)
          {
            case 0:
              return PlayerPartyTypes.Normal;
            case 1:
              return PlayerPartyTypes.Event;
            case 2:
              return PlayerPartyTypes.Multiplay;
            case 3:
              return PlayerPartyTypes.Arena;
            case 4:
              return PlayerPartyTypes.ArenaDef;
            case 5:
              return PlayerPartyTypes.Character;
            case 6:
              return PlayerPartyTypes.Tower;
            case 7:
              return PlayerPartyTypes.Versus;
            case 8:
              return PlayerPartyTypes.MultiTower;
          }
        }
      }
      return PlayerPartyTypes.Normal;
    }

    public static string GetStringFromPartyType(PlayerPartyTypes type)
    {
      switch (type)
      {
        case PlayerPartyTypes.Normal:
          return "norm";
        case PlayerPartyTypes.Event:
          return "ev";
        case PlayerPartyTypes.Multiplay:
          return "mul";
        case PlayerPartyTypes.Arena:
          return "col";
        case PlayerPartyTypes.ArenaDef:
          return "coldef";
        case PlayerPartyTypes.Character:
          return "chara";
        case PlayerPartyTypes.Tower:
          return "tower";
        case PlayerPartyTypes.Versus:
          return "vs";
        case PlayerPartyTypes.MultiTower:
          return "multi_tw";
        default:
          return "norm";
      }
    }

    public void Deserialize(Json_Party json)
    {
      this.Reset();
      if (json == null)
        throw new InvalidCastException();
      this.mLeaderIndex = 0;
      for (int index = 0; index < json.units.Length; ++index)
        this.mUniqueIDs[index] = json.units[index];
      this.Selected = json.flg_sel != 0;
      this.IsDefense = json.flg_seldef != 0;
    }

    public void Reset()
    {
      Array.Clear((Array) this.mUniqueIDs, 0, this.mUniqueIDs.Length);
    }

    public void SetParty(PartyData org)
    {
      if (org == null)
        return;
      this.Reset();
      for (int index = 0; index < this.MAX_UNIT; ++index)
        this.mUniqueIDs[index] = org.GetUnitUniqueID(index);
    }

    public void SetUnitUniqueID(int index, long uniqueid)
    {
      if (index < 0 || this.MAX_UNIT <= index)
        return;
      this.mUniqueIDs[index] = uniqueid;
    }

    public long GetUnitUniqueID(int index)
    {
      if (index < 0 || this.MAX_UNIT <= index)
        return 0;
      return this.mUniqueIDs[index];
    }

    public bool IsPartyUnit(long uniqueid)
    {
      return this.FindPartyUnit(uniqueid) != -1;
    }

    public int FindPartyUnit(long uniqueid)
    {
      for (int index = 0; index < this.MAX_UNIT; ++index)
      {
        if (this.mUniqueIDs[index] == uniqueid)
          return index;
      }
      return -1;
    }

    public bool IsSub(long uniqueid)
    {
      return this.FindPartyUnit(uniqueid) >= this.MAX_MAINMEMBER;
    }

    public bool IsSub(UnitData unit)
    {
      return this.IsSub(unit.UniqueID);
    }
  }
}
