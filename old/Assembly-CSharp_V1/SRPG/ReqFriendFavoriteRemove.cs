// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendFavoriteRemove
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendFavoriteRemove : WebAPI
  {
    public ReqFriendFavoriteRemove(string fuid, Network.ResponseCallback response)
    {
      this.name = "friend/favorite/remove";
      this.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
      this.callback = response;
    }
  }
}
