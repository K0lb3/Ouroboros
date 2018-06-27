// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCheckVersion2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqCheckVersion2 : WebAPI
  {
    public ReqCheckVersion2(string ver, Network.ResponseCallback response)
    {
      this.name = "chkver2";
      this.body = "{\"ver\":\"";
      this.body += ver;
      this.body += "\"}";
      this.callback = response;
    }
  }
}
