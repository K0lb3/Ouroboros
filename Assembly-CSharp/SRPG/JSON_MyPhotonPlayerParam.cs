// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MyPhotonPlayerParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class JSON_MyPhotonPlayerParam
  {
    public string playerName = string.Empty;
    public string FUID = string.Empty;
    public string UID = string.Empty;
    public string award = string.Empty;
    public int playerID;
    public int playerIndex;
    public int playerLevel;
    public int totalAtk;
    public int rankpoint;
    public int state;
    public JSON_MyPhotonPlayerParam.UnitDataElem[] units;
    public int leaderID;

    public static JSON_MyPhotonPlayerParam Parse(string json)
    {
      if (string.IsNullOrEmpty(json))
        return new JSON_MyPhotonPlayerParam();
      JSON_MyPhotonPlayerParam jsonObject = JSONParser.parseJSONObject<JSON_MyPhotonPlayerParam>(json);
      jsonObject.SetupUnits();
      return jsonObject;
    }

    public void SetupUnits()
    {
      if (this.units == null)
        return;
      for (int index = 0; index < this.units.Length; ++index)
      {
        UnitData unitData = new UnitData();
        unitData.Deserialize(this.units[index].unitJson);
        this.units[index].unit = unitData;
      }
    }

    public string Serialize()
    {
      string str = "{" + "\"playerID\":" + (object) this.playerID + ",\"playerIndex\":" + (object) this.playerIndex + ",\"playerName\":\"" + JsonEscape.Escape(this.playerName) + "\"" + ",\"playerLevel\":" + (object) this.playerLevel + ",\"FUID\":\"" + JsonEscape.Escape(this.FUID) + "\"" + ",\"UID\":\"" + JsonEscape.Escape(this.UID) + "\"" + ",\"state\":" + (object) this.state + ",\"leaderID\":" + (object) this.leaderID + ",\"totalAtk\":" + (object) this.totalAtk + ",\"rankpoint\":" + (object) this.rankpoint + ",\"award\":\"" + JsonEscape.Escape(this.award) + "\"" + ",\"units\":[";
      if (this.units != null)
      {
        for (int index = 0; index < this.units.Length; ++index)
          str = str + (index != 0 ? "," : string.Empty) + this.units[index].Serialize();
      }
      return str + "]" + "}";
    }

    public SupportData CreateSupportData()
    {
      if (this.units == null || this.units.Length <= 0)
        return (SupportData) null;
      if (this.units[0] == null || this.units[0].unit == null)
        return (SupportData) null;
      SupportData supportData = new SupportData();
      supportData.FUID = this.FUID;
      supportData.Unit = this.units[0].unit;
      supportData.PlayerName = this.playerName;
      supportData.PlayerLevel = this.playerLevel;
      supportData.UnitID = supportData.Unit.UnitID;
      supportData.UnitLevel = supportData.Unit.Lv;
      supportData.UnitRarity = supportData.Unit.Rarity;
      supportData.JobID = supportData.Unit.CurrentJob.JobID;
      supportData.LeaderSkillLevel = UnitParam.GetLeaderSkillLevel(supportData.UnitRarity, supportData.Unit.AwakeLv);
      return supportData;
    }

    public static JSON_MyPhotonPlayerParam Create(int playerID = 0, int playerIndex = 0)
    {
      JSON_MyPhotonPlayerParam photonPlayerParam = new JSON_MyPhotonPlayerParam();
      if (photonPlayerParam == null)
        return (JSON_MyPhotonPlayerParam) null;
      if (Object.op_Equality((Object) PunMonoSingleton<MyPhoton>.Instance, (Object) null))
        return (JSON_MyPhotonPlayerParam) null;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      photonPlayerParam.playerID = playerID;
      photonPlayerParam.playerIndex = playerIndex;
      photonPlayerParam.playerName = player.Name;
      photonPlayerParam.playerLevel = player.Lv;
      photonPlayerParam.FUID = player.FUID;
      photonPlayerParam.UID = MonoSingleton<GameManager>.Instance.DeviceId;
      photonPlayerParam.award = player.SelectedAward;
      PartyData partyData = GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.VERSUS ? player.Partys[2] : player.Partys[7];
      int num = 0;
      photonPlayerParam.leaderID = partyData.LeaderIndex;
      List<JSON_MyPhotonPlayerParam.UnitDataElem> unitDataElemList = new List<JSON_MyPhotonPlayerParam.UnitDataElem>();
      for (int index = 0; index < partyData.MAX_UNIT; ++index)
      {
        long unitUniqueId = partyData.GetUnitUniqueID(index);
        UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(unitUniqueId);
        if (unitDataByUniqueId != null)
        {
          unitDataElemList.Add(new JSON_MyPhotonPlayerParam.UnitDataElem()
          {
            slotID = index,
            place = player.GetVersusPlacement(PlayerData.VERSUS_ID_KEY + (object) index),
            unit = unitDataByUniqueId
          });
          num = num + (int) unitDataByUniqueId.Status.param.atk + (int) unitDataByUniqueId.Status.param.mag;
        }
      }
      photonPlayerParam.units = unitDataElemList.ToArray();
      photonPlayerParam.totalAtk = num;
      photonPlayerParam.rankpoint = player.VERSUS_POINT;
      return photonPlayerParam;
    }

    public enum EState
    {
      NOP,
      READY,
      START,
      START_CONFIRM,
      EDIT,
      NUM,
    }

    public class UnitDataElem
    {
      public int slotID;
      public int place;
      public Json_Unit unitJson;
      public UnitData unit;

      public string Serialize()
      {
        string str = "{" + "\"slotID\":" + (object) this.slotID + ",\"place\":" + (object) this.place;
        if (this.unit != null)
          str = str + ",\"unitJson\":" + this.unit.Serialize();
        return str + "}";
      }
    }
  }
}
