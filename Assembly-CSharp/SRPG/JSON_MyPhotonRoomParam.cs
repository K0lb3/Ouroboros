// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_MyPhotonRoomParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class JSON_MyPhotonRoomParam
  {
    public static readonly int LINE_PARAM_ENCODE_KEY = 6789;
    public string creatorName = string.Empty;
    public int creatorLV = 1;
    public string creatorFUID = string.Empty;
    public string comment = string.Empty;
    public string passCode = string.Empty;
    public string iname = string.Empty;
    public int type;
    public int isLINE;
    public int started;
    public int roomid;
    public int audience;
    public int audienceNum;
    public int unitlv;
    public int challegedMTFloor;
    public JSON_MyPhotonPlayerParam[] players;

    public static string GetMyCreatorFUID()
    {
      if (Network.Mode == Network.EConnectMode.Offline)
        return MonoSingleton<GameManager>.Instance.UdId;
      return MonoSingleton<GameManager>.Instance.Player.FUID;
    }

    public JSON_MyPhotonPlayerParam GetOwner()
    {
      if (this.players == null)
        return (JSON_MyPhotonPlayerParam) null;
      JSON_MyPhotonPlayerParam photonPlayerParam = (JSON_MyPhotonPlayerParam) null;
      foreach (JSON_MyPhotonPlayerParam player in this.players)
      {
        if (player.playerIndex > 0 && (photonPlayerParam == null || player.playerID < photonPlayerParam.playerID))
          photonPlayerParam = player;
      }
      return photonPlayerParam;
    }

    public string GetOwnerName()
    {
      JSON_MyPhotonPlayerParam owner = this.GetOwner();
      if (owner == null)
        return this.creatorName;
      return owner.playerName;
    }

    public int GetOwnerLV()
    {
      JSON_MyPhotonPlayerParam owner = this.GetOwner();
      if (owner == null)
        return this.creatorLV;
      return owner.playerLevel;
    }

    public static JSON_MyPhotonRoomParam Parse(string json)
    {
      if (json == null || json.Length <= 0)
        return new JSON_MyPhotonRoomParam();
      return JSONParser.parseJSONObject<JSON_MyPhotonRoomParam>(json);
    }

    public string Serialize()
    {
      string str = "{" + "\"creatorName\":\"" + JsonEscape.Escape(this.creatorName) + "\"" + ",\"creatorLV\":" + (object) this.creatorLV + ",\"creatorFUID\":\"" + JsonEscape.Escape(this.creatorFUID) + "\"" + ",\"comment\":\"" + JsonEscape.Escape(this.comment) + "\"" + ",\"passCode\":\"" + JsonEscape.Escape(this.passCode) + "\"" + ",\"iname\":\"" + JsonEscape.Escape(this.iname) + "\"" + ",\"type\":" + (object) this.type + ",\"isLINE\":" + (object) this.isLINE + ",\"started\":" + (object) this.started + ",\"roomid\":" + (object) this.roomid + ",\"audience\":" + (object) this.audience + ",\"audienceNum\":" + (object) this.audienceNum + ",\"unitlv\":" + (object) this.unitlv + ",\"challegedMTFloor\":" + (object) GlobalVars.SelectedMultiTowerFloor + ",\"players\":[";
      if (this.players != null)
      {
        for (int index = 0; index < this.players.Length; ++index)
          str = str + (index > 0 ? "," : string.Empty) + this.players[index].Serialize();
      }
      return str + "]" + "}";
    }

    public static int GetTotalUnitNum(QuestParam param)
    {
      if (param == null)
        return 0;
      return (int) param.unitNum * (int) param.playerNum;
    }

    public int GetUnitSlotNum()
    {
      return this.GetUnitSlotNum(PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex);
    }

    public int GetUnitSlotNum(int playerIndex)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(this.iname);
      if (quest == null)
        return 0;
      return (int) quest.unitNum;
    }

    public enum EType
    {
      RAID,
      VERSUS,
      TOWER,
      NUM,
    }
  }
}
