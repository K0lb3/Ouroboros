// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendReject
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendReject : WebAPI
  {
    public ReqFriendReject(string[] fuids, Network.ResponseCallback response)
    {
      this.name = "friend/reject";
      this.body = "\"fuids\":[";
      for (int index = 0; index < fuids.Length; ++index)
      {
        ReqFriendReject reqFriendReject = this;
        reqFriendReject.body = reqFriendReject.body + "\"" + fuids[index] + "\"";
        if (index != fuids.Length - 1)
          this.body += ",";
      }
      this.body += "]";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
