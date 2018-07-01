// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTowerRank : WebAPI
  {
    public ReqTowerRank(string qid, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "tower/ranking";
      stringBuilder.Append("\"qid\":\"");
      stringBuilder.Append(qid);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public class JSON_TowerRankParam
    {
      public string fuid;
      public string name;
      public int lv;
      public int rank;
      public int score;
      public string uid;
      public Json_Unit unit;
      public string selected_award;
    }

    public class JSON_TowerRankResponse
    {
      public ReqTowerRank.JSON_TowerRankParam[] speed;
      public ReqTowerRank.JSON_TowerRankParam[] technical;
      public JSON_ReqTowerResuponse.Json_RankStatus rank;
    }
  }
}
