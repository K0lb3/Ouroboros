// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiRank
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiRank : WebAPI
  {
    public ReqMultiRank(string iname, Network.ResponseCallback response)
    {
      this.name = "btl/usedunit";
      this.body = "\"iname\":\"" + JsonEscape.Escape(iname) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Json_MultiRankParam
    {
      public string unit_iname;
      public string job_iname;
    }

    public class Json_MultiRank
    {
      public ReqMultiRank.Json_MultiRankParam[] ranking;
      public int isReady;
    }
  }
}
