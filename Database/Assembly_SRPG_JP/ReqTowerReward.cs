// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
