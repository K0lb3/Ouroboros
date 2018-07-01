// Decompiled with JetBrains decompiler
// Type: SRPG.ReqRankMatchMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqRankMatchMake : WebAPI
  {
    public ReqRankMatchMake(Network.ResponseCallback response = null)
    {
      this.name = "vs/rankmatch/make";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    public class Response
    {
      public string token;
      public string owner_name;
    }
  }
}
