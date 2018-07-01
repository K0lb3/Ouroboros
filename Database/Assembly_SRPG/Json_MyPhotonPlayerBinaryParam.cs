// Decompiled with JetBrains decompiler
// Type: SRPG.Json_MyPhotonPlayerBinaryParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class Json_MyPhotonPlayerBinaryParam
  {
    public string playerName = string.Empty;
    public string FUID = string.Empty;
    public string UID = string.Empty;
    public string award = string.Empty;
    public int state = 1;
    public int playerID;
    public int playerIndex;
    public int playerLevel;
    public int totalAtk;
    public int rankpoint;
    public Json_MyPhotonPlayerBinaryParam.UnitDataElem[] units;
    public int leaderID;

    public void Set(JSON_MyPhotonPlayerParam param)
    {
      this.playerID = param.playerID;
      this.playerIndex = param.playerIndex;
      this.playerName = param.playerName;
      this.playerLevel = param.playerLevel;
      this.FUID = param.FUID;
      this.UID = param.UID;
      this.totalAtk = param.totalAtk;
      this.rankpoint = param.rankpoint;
      this.award = param.award;
      this.state = param.state;
      if (param.units == null)
        return;
      this.units = new Json_MyPhotonPlayerBinaryParam.UnitDataElem[param.units.Length];
      for (int index = 0; index < param.units.Length; ++index)
      {
        this.units[index] = new Json_MyPhotonPlayerBinaryParam.UnitDataElem();
        this.units[index].slotID = param.units[index].slotID;
        this.units[index].place = param.units[index].place;
        this.units[index].unitJson = param.units[index].unitJson;
      }
    }

    public static bool IsEqual(Json_MyPhotonPlayerBinaryParam data0, Json_MyPhotonPlayerBinaryParam data1)
    {
      bool flag = true & data0.playerID == data1.playerID & data0.playerIndex == data1.playerIndex & data0.playerName == data1.playerName & data0.playerLevel == data1.playerLevel & data0.FUID == data1.FUID & data0.UID == data1.UID & data0.totalAtk == data1.totalAtk & data0.rankpoint == data1.rankpoint & data0.award == data1.award & data0.state == data1.state;
      if (data0.units != null && data1.units != null && data0.units.Length == data1.units.Length)
      {
        for (int index = 0; index < data0.units.Length; ++index)
          flag = flag & data0.units[index].slotID == data1.units[index].slotID & data0.units[index].place == data1.units[index].place & JsonUtility.ToJson((object) data0.units[index].unitJson).Equals(JsonUtility.ToJson((object) data1.units[index].unitJson));
      }
      return flag;
    }

    public class UnitDataElem
    {
      public int slotID;
      public int place;
      public Json_Unit unitJson;
    }
  }
}
