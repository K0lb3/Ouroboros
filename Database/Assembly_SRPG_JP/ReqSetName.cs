// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSetName
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
