// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendFavoriteAdd
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendFavoriteAdd : WebAPI
  {
    public ReqFriendFavoriteAdd(string fuid, Network.ResponseCallback response)
    {
      this.name = "friend/favorite/add";
      this.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
      this.callback = response;
    }
  }
}
