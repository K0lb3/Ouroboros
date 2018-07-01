// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusFreematchStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusFreematchStatus : WebAPI
  {
    public ReqVersusFreematchStatus(Network.ResponseCallback response)
    {
      this.name = "vs/freematch/status";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class EnableTimeSchedule
    {
      public long expired;
      public long next;
    }

    public class Response
    {
      public ReqVersusFreematchStatus.EnableTimeSchedule enabletime;
    }
  }
}
