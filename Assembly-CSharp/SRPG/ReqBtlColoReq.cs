// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlColoReq
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqBtlColoReq : WebAPI
  {
    public ReqBtlColoReq(string questID, string fuid, ArenaPlayer ap, Network.ResponseCallback response, int partyIndex)
    {
      this.name = "btl/colo/req";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      if (partyIndex >= 0)
      {
        stringBuilder.Append("\"partyid\":");
        stringBuilder.Append(partyIndex);
        stringBuilder.Append(",");
      }
      stringBuilder.Append("\"btlparam\":{},");
      stringBuilder.Append("\"fuid\":\"");
      stringBuilder.Append(fuid);
      stringBuilder.Append("\"");
      stringBuilder.Append(",");
      stringBuilder.Append("\"opp_rank\":");
      stringBuilder.Append(ap.ArenaRank);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
