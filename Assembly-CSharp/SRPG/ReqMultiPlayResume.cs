// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiPlayResume
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiPlayResume : WebAPI
  {
    public ReqMultiPlayResume(long btlID, Network.ResponseCallback response)
    {
      this.name = "btl/multi/resume";
      this.body = string.Empty;
      ReqMultiPlayResume reqMultiPlayResume = this;
      reqMultiPlayResume.body = reqMultiPlayResume.body + "\"btlid\":" + (object) btlID;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }

    public class Quest
    {
      public string iname;
    }

    public class Response
    {
      public ReqMultiPlayResume.Quest quest;
      public int btlid;
      public string app_id;
      public string token;
      public Json_BtlInfo_Multi btlinfo;
    }
  }
}
