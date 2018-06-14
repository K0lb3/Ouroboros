// Decompiled with JetBrains decompiler
// Type: SRPG.ReqHikkoshiExec
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
