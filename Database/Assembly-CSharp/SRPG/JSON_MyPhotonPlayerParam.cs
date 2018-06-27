// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MyPhotonPlayerParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [Serializable]
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
    public int mtChallengeFloor;
    public int mtClearedFloor;

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
        if (this.units[index].unitJson != null)
        {
          UnitData unitData = new UnitData();
          unitData.Deserialize(this.units[index].unitJson);
          this.units[index].unit = unitData;
        }
      }
    }

    public string Serialize()
    {
      string str = "{" + "\"playerID\":" + (object) this.playerID + ",\"playerIndex\":" + (object) this.playerIndex + ",\"playerName\":\"" + JsonEscape.Escape(this.playerName) + "\"" + ",\"playerLevel\":" + (object) this.playerLevel + ",\"FUID\":\"" + JsonEscape.Escape(this.FUID) + "\"" + ",\"UID\":\"" + JsonEscape.Escape(this.UID) + "\"" + ",\"state\":" + (object) this.state + ",\"leaderID\":" + (object) this.leaderID + ",\"totalAtk\":" + (object) this.totalAtk + ",\"rankpoint\":" + (object) this.rankpoint + ",\"mtChallengeFloor\":" + (object) this.mtChallengeFloor + ",\"mtClearedFloor\":" + (object) this.mtClearedFloor + ",\"award\":\"" + JsonEscape.Escape(this.award) + "\"" + ",\"units\":[";
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

    public void CreateJsonUnitData()
    {
      if (this.units == null)
        return;
      for (int index = 0; index < this.units.Length; ++index)
      {
        if (this.units[index].unit != null)
        {
          string str = this.units[index].unit.Serialize();
          this.units[index].unitJson = (Json_Unit) JsonUtility.FromJson<Json_Unit>(str);
        }
      }
    }

    public static JSON_MyPhotonPlayerParam Create(int playerID = 0, int playerIndex = 0)
    {
      JSON_MyPhotonPlayerParam photonPlayerParam = new JSON_MyPhotonPlayerParam();
      if (photonPlayerParam == null)
        return (JSON_MyPhotonPlayerParam) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) PunMonoSingleton<MyPhoton>.Instance, (UnityEngine.Object) null))
        return (JSON_MyPhotonPlayerParam) null;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      photonPlayerParam.playerID = playerID;
      photonPlayerParam.playerIndex = playerIndex;
      photonPlayerParam.playerName = player.Name;
      photonPlayerParam.playerLevel = player.Lv;
      photonPlayerParam.FUID = player.FUID;
      photonPlayerParam.UID = MonoSingleton<GameManager>.Instance.DeviceId;
      photonPlayerParam.award = player.SelectedAward;
      PlayerPartyTypes partyType = PlayerPartyTypes.Multiplay;
      switch (GlobalVars.SelectedMultiPlayRoomType)
      {
        case JSON_MyPhotonRoomParam.EType.RAID:
          partyType = PlayerPartyTypes.Multiplay;
          break;
        case JSON_MyPhotonRoomParam.EType.VERSUS:
          partyType = PlayerPartyTypes.Versus;
          break;
        case JSON_MyPhotonRoomParam.EType.TOWER:
          partyType = PlayerPartyTypes.MultiTower;
          break;
      }
      QuestParam quest = (QuestParam) null;
      PartyData party;
      int length;
      List<PartyEditData> teams;
      if (partyType == PlayerPartyTypes.Versus || partyType == PlayerPartyTypes.MultiTower)
      {
        party = player.Partys[(int) partyType];
        length = party.MAX_UNIT;
        int lastSelectionIndex;
        teams = PartyUtility.LoadTeamPresets(partyType, out lastSelectionIndex);
      }
      else
      {
        party = player.Partys[(int) partyType];
        length = party.MAX_UNIT;
        if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID))
        {
          quest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
          if (quest != null)
            length = (int) quest.unitNum;
        }
        int lastSelectionIndex;
        teams = PartyUtility.LoadTeamPresets(partyType, out lastSelectionIndex);
      }
      UnitData[] unitDataArray = new UnitData[length];
      if (teams != null && teams.Count > 0)
      {
        PartyUtility.ResetToDefaultTeamIfNeeded(partyType, quest, teams);
        UnitData[] units = teams[0].Units;
        for (int index = 0; index < unitDataArray.Length && index < units.Length; ++index)
          unitDataArray[index] = units[index];
      }
      else
      {
        for (int index = 0; index < unitDataArray.Length; ++index)
        {
          long unitUniqueId = party.GetUnitUniqueID(index);
          unitDataArray[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unitUniqueId);
        }
      }
      int num1 = 0;
      int num2 = 0;
      bool flag = GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER;
      photonPlayerParam.leaderID = party.LeaderIndex;
      List<JSON_MyPhotonPlayerParam.UnitDataElem> unitDataElemList = new List<JSON_MyPhotonPlayerParam.UnitDataElem>();
      for (int index = 0; index < unitDataArray.Length; ++index)
      {
        UnitData unitData = unitDataArray[index];
        if (unitData != null)
        {
          unitDataElemList.Add(new JSON_MyPhotonPlayerParam.UnitDataElem()
          {
            slotID = num1++,
            place = !flag ? player.GetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + (object) index) : -1,
            sub = index < party.MAX_MAINMEMBER || party.MAX_SUBMEMBER <= 0 ? 0 : 1,
            unit = unitData
          });
          num2 = num2 + (int) unitData.Status.param.atk + (int) unitData.Status.param.mag;
        }
      }
      photonPlayerParam.units = unitDataElemList.ToArray();
      photonPlayerParam.totalAtk = num2;
      photonPlayerParam.rankpoint = player.VERSUS_POINT;
      photonPlayerParam.mtChallengeFloor = MonoSingleton<GameManager>.Instance.GetMTChallengeFloor();
      photonPlayerParam.mtClearedFloor = MonoSingleton<GameManager>.Instance.GetMTClearedMaxFloor();
      return photonPlayerParam;
    }

    public void UpdateMultiTowerPlacement(bool isDefault)
    {
      if (GlobalVars.SelectedMultiPlayRoomType != JSON_MyPhotonRoomParam.EType.TOWER)
        return;
      if (isDefault)
      {
        for (int index = 0; index < this.units.Length; ++index)
        {
          if (this.units[index] != null)
          {
            this.units[index].place = (this.playerIndex - 1) * 2 + index;
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTITW_ID_KEY + (object) index, this.units[index].place, true);
          }
        }
      }
      else
      {
        for (int index = 0; index < this.units.Length; ++index)
        {
          if (this.units[index] != null)
            this.units[index].place = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.MULTITW_ID_KEY + (object) index, 0);
        }
      }
    }

    public enum EState
    {
      NOP,
      READY,
      START,
      START_CONFIRM,
      EDIT,
      FLOOR_SELECT,
      NUM,
    }

    [Serializable]
    public class UnitDataElem
    {
      public int slotID;
      public int place;
      public int sub;
      public Json_Unit unitJson;
      public UnitData unit;

      public string Serialize()
      {
        string str = "{" + "\"slotID\":" + (object) this.slotID + ",\"place\":" + (object) this.place + ",\"sub\":" + (object) this.sub;
        if (this.unit != null)
          str = str + ",\"unitJson\":" + this.unit.Serialize2();
        else if (this.unitJson != null)
        {
          this.unit = new UnitData();
          this.unit.Deserialize(this.unitJson);
          str = str + ",\"unitJson\":" + this.unit.Serialize2();
        }
        return str + "}";
      }
    }
  }
}
