// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTutUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTutUpdate : WebAPI
  {
    public ReqTutUpdate(long flags, Network.ResponseCallback response)
    {
      this.name = "tut/update";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"tut\":");
      stringBuilder.Append(flags);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
