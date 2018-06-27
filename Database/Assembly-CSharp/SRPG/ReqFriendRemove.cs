// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendRemove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendRemove : WebAPI
  {
    public ReqFriendRemove(string[] fuids, Network.ResponseCallback response)
    {
      this.name = "friend/remove";
      this.body = "\"fuids\":[";
      for (int index = 0; index < fuids.Length; ++index)
      {
        ReqFriendRemove reqFriendRemove = this;
        reqFriendRemove.body = reqFriendRemove.body + "\"" + fuids[index] + "\"";
        if (index != fuids.Length - 1)
          this.body += ",";
      }
      this.body += "]";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
