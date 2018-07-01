// Decompiled with JetBrains decompiler
// Type: SRPG.ReqCheckVersion2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
