// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTowerReward : WebAPI
  {
    public ReqTowerReward(short mid, short nid, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "expedition/reward";
      stringBuilder.Append("\"mid\":");
      stringBuilder.Append(mid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"nid\":");
      stringBuilder.Append(nid);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
