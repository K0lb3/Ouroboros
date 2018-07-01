// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLoginPack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqLoginPack : WebAPI
  {
    public ReqLoginPack(Network.ResponseCallback response, bool relogin = false)
    {
      this.name = "login/param";
      this.body = "\"relogin\":";
      this.body += (string) (object) (!relogin ? 0 : 1);
      this.body += ",\"req_uid\":1";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
