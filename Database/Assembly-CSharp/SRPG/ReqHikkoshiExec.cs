// Decompiled with JetBrains decompiler
// Type: SRPG.ReqHikkoshiExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqHikkoshiExec : WebAPI
  {
    public ReqHikkoshiExec(string token, Network.ResponseCallback response)
    {
      this.name = "hikkoshi/exec";
      this.body = WebAPI.GetRequestString("\"token\":\"" + token + "\"");
      this.callback = response;
    }
  }
}
