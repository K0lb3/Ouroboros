// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendList : WebAPI
  {
    public ReqFriendList(bool is_follow, Network.ResponseCallback response)
    {
      this.name = "friend";
      this.body = WebAPI.GetRequestString((string) null);
      if (is_follow)
        this.body = WebAPI.GetRequestString("\"is_follower\":" + (object) 1);
      this.callback = response;
    }
  }
}
