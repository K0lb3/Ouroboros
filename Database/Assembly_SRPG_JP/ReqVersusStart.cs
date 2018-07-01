// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusStart : WebAPI
  {
    public ReqVersusStart(Network.ResponseCallback response)
    {
      this.name = "vs/start";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
    }

    public class JSON_VersusMap
    {
      public string free;
      public string tower;
      public string friend;
    }

    public class Response
    {
      public string app_id;
      public ReqVersusStart.JSON_VersusMap maps;
    }
  }
}
