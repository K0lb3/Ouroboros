// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFgGAuth
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqFgGAuth : WebAPI
  {
    public ReqFgGAuth(Network.ResponseCallback response)
    {
      this.name = "achieve/auth";
      this.callback = response;
    }

    public enum eAuthStatus
    {
      None,
      Disable,
      NotSynchronized,
      Synchronized,
    }
  }
}
