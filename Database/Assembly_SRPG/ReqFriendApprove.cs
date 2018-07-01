// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendApprove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFriendApprove : WebAPI
  {
    public ReqFriendApprove(string fuid, Network.ResponseCallback response)
    {
      fuid = WebAPI.EscapeString(fuid);
      this.name = "friend/approve";
      this.body = WebAPI.GetRequestString("\"fuid\":\"" + fuid + "\"");
      this.callback = response;
    }
  }
}
