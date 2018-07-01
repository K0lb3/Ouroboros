// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
