// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTowerReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTowerReset : WebAPI
  {
    public ReqTowerReset(string qid, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"qid\":\"");
      stringBuilder.Append(qid);
      stringBuilder.Append("\"");
      this.name = "tower/reset";
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
