// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusSeason
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusSeason : WebAPI
  {
    public ReqVersusSeason(Network.ResponseCallback response)
    {
      this.name = "vs/towermatch/season";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Response
    {
      public int unreadmail;
    }
  }
}
