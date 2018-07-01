// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendFind
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendFind : WebAPI
  {
    public ReqFriendFind(string fuid, Network.ResponseCallback response)
    {
      fuid = WebAPI.EscapeString(fuid);
      this.name = "friend/find";
      this.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
      this.callback = response;
    }
  }
}
