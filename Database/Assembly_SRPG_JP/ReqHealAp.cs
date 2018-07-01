// Decompiled with JetBrains decompiler
// Type: SRPG.ReqHealAp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqHealAp : WebAPI
  {
    public ReqHealAp(long iid, int num, Network.ResponseCallback response)
    {
      this.name = "item/addstm";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"iid\" : ");
      stringBuilder.Append(iid);
      stringBuilder.Append(",");
      stringBuilder.Append("\"num\" : ");
      stringBuilder.Append(num);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
