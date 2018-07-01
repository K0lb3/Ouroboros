// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComOpen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqBtlComOpen : WebAPI
  {
    public ReqBtlComOpen(string iname, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "btl/com/open";
      stringBuilder.Append("\"areaid\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
