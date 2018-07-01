// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendReq : WebAPI
  {
    public ReqFriendReq(string fuid, Network.ResponseCallback response)
    {
      this.name = "friend/req";
      this.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
      this.callback = response;
    }
  }
}
