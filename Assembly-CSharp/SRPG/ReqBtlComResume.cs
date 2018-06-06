// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComResume
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlComResume : WebAPI
  {
    public ReqBtlComResume(long btlid, Network.ResponseCallback response)
    {
      this.name = "btl/com/resume";
      this.body = "\"btlid\":" + (object) btlid;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
