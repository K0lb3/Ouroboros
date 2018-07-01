// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerRecover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTowerRecover : WebAPI
  {
    public ReqTowerRecover(string qid, int coin, int round, byte floor, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "tower/recover";
      stringBuilder.Append("\"qid\":\"");
      stringBuilder.Append(qid);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"coin\":");
      stringBuilder.Append(coin);
      stringBuilder.Append(",");
      stringBuilder.Append("\"round\":");
      stringBuilder.Append(round);
      stringBuilder.Append(",");
      stringBuilder.Append("\"floor\":");
      stringBuilder.Append(floor);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
