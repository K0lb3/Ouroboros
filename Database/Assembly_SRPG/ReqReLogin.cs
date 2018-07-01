// Decompiled with JetBrains decompiler
// Type: SRPG.ReqReLogin
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqReLogin : WebAPI
  {
    public ReqReLogin(Network.ResponseCallback response)
    {
      this.name = "login/span";
      this.body = string.Empty;
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
