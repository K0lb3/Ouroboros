// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBundleParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBundleParam : WebAPI
  {
    public ReqBundleParam(Network.ResponseCallback response)
    {
      this.name = "bundle";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
