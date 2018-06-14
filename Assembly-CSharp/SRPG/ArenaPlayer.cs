// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Text;

namespace SRPG
{
  public class ArenaPlayer
  {
    public UnitData[] Unit = new UnitData[3];
    public DateTime battle_at = DateTime.MinValue;
    public string result;
    public string FUID;
    public string PlayerName;
    public int PlayerLevel;
    public int ArenaRank;
    public int TotalAtk;
    public string SelectAward;

    public int BattleCount()
    {
      return 10;
    }

    public int WinCount()
    {
      return 5;
    }

    public void Serialize(StringBuilder sb)
    {
      sb.Append("{\"result\":\"");
      sb.Append(this.result);
      sb.Append("\",\"fuid\":\"");
      sb.Append(this.FUID);
      sb.Append("\",\"lv\":");
      sb.Append(this.PlayerLevel);
      sb.Append(",\"rank\":");
      sb.Append(this.ArenaRank);
      sb.Append(",\"units\":[");
      int num = 0;
      for (int index = 0; index < this.Unit.Length; ++index)
      {
        if (this.Unit[index] != null)
        {
          if (num > 0)
            sb.Append(',');
          sb.Append(this.Unit[index].Serialize());
          ++num;
        }
      }
      sb.Append("]}");
    }

    public void Deserialize(Json_ArenaPlayer json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.FUID = json.fuid;
      this.PlayerName = json.name;
      this.PlayerLevel = json.lv;
      this.ArenaRank = json.rank;
      this.TotalAtk = 0;
      this.result = json.result;
      this.battle_at = GameUtility.UnixtimeToLocalTime(json.btl_at);
      this.SelectAward = json.award;
      if (json.units == null)
        return;
      for (int index = 0; index < json.units.Length && index < this.Unit.Length; ++index)
      {
        UnitData unitData = new UnitData();
        if (json.units[index] == null)
        {
          DebugUtility.LogWarning("Unit is NULL");
        }
        else
        {
          unitData.Deserialize(json.units[index]);
          unitData.SetJob(PlayerPartyTypes.ArenaDef);
          this.Unit[index] = unitData;
          this.TotalAtk += (int) unitData.Status.param.atk;
          this.TotalAtk += (int) unitData.Status.param.mag;
        }
      }
    }
  }
}
