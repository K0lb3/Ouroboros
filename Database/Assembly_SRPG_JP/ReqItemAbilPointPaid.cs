// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemAbilPointPaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqItemAbilPointPaid : WebAPI
  {
    public ReqItemAbilPointPaid(Network.ResponseCallback response)
    {
      this.name = "item/addappaid";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }

    public ReqItemAbilPointPaid(int value, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "item/addappaid";
      stringBuilder.Append("\"val\" : ");
      stringBuilder.Append(value);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
