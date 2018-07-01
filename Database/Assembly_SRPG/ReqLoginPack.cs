// Decompiled with JetBrains decompiler
// Type: SRPG.ReqLoginPack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqLoginPack : WebAPI
  {
    public ReqLoginPack(Network.ResponseCallback response, bool relogin = false)
    {
      this.name = "login/param";
      this.body = "\"relogin\":";
      this.body += (string) (object) (!relogin ? 0 : 1);
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
