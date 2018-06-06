// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusStart
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusStart : WebAPI
  {
    public ReqVersusStart(VERSUS_TYPE type, Network.ResponseCallback response)
    {
      this.name = "vs/start";
      this.body = string.Empty;
      ReqVersusStart reqVersusStart = this;
      reqVersusStart.body = reqVersusStart.body + "\"type\":\"" + type.ToString().ToLower() + "\"";
      this.body = WebAPI.GetRequestString(this.body);
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
