// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFgGAuth
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
