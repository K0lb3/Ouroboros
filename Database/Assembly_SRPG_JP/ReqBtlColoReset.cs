// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlColoReset
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlColoReset : WebAPI
  {
    public ReqBtlColoReset(ColoResetTypes reset, Network.ResponseCallback response)
    {
      this.name = "btl/colo/reset/" + reset.ToString();
      this.body = WebAPI.GetRequestString(WebAPI.GetStringBuilder().ToString());
      this.callback = response;
    }
  }
}
