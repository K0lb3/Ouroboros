// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetName
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
