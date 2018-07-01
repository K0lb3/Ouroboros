// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlColoHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlColoHistory : WebAPI
  {
    public ReqBtlColoHistory(Network.ResponseCallback response)
    {
      this.name = "btl/colo/history/";
      this.body = WebAPI.GetRequestString(WebAPI.GetStringBuilder().ToString());
      this.callback = response;
    }
  }
}
