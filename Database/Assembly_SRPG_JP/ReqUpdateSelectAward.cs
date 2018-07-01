// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUpdateSelectAward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqUpdateSelectAward : WebAPI
  {
    public ReqUpdateSelectAward(string iname, Network.ResponseCallback response)
    {
      this.name = "award/select";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"selected_award\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
