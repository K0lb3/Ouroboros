// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlColoRanking
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlColoRanking : WebAPI
  {
    public ReqBtlColoRanking(ReqBtlColoRanking.RankingTypes type, Network.ResponseCallback response)
    {
      this.name = "btl/colo/ranking/" + type.ToString();
      this.body = WebAPI.GetRequestString(WebAPI.GetStringBuilder().ToString());
      this.callback = response;
    }

    public enum RankingTypes
    {
      world,
      friend,
    }
  }
}
