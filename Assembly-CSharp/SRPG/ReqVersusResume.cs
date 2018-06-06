// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersusResume
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqVersusResume : WebAPI
  {
    public ReqVersusResume(long btlID, Network.ResponseCallback response)
    {
      this.name = "vs/resume";
      this.body = string.Empty;
      ReqVersusResume reqVersusResume = this;
      reqVersusResume.body = reqVersusResume.body + "\"btlid\":" + (object) btlID;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Quest
    {
      public string iname;
    }

    public class Response
    {
      public ReqVersusResume.Quest quest;
      public int btlid;
      public string app_id;
      public string token;
      public string type;
      public Json_BtlInfo_Multi btlinfo;
    }
  }
}
