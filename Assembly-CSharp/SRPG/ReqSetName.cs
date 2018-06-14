// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqSetName : WebAPI
  {
    public ReqSetName(string username, Network.ResponseCallback response)
    {
      username = WebAPI.EscapeString(username);
      this.name = "setname";
      this.body = WebAPI.GetRequestString("\"name\":\"" + username + "\"");
      this.callback = response;
    }
  }
}
