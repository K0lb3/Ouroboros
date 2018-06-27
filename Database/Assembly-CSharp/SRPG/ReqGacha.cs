// Decompiled with JetBrains decompiler
// Type: SRPG.ReqGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqGacha : WebAPI
  {
    public ReqGacha(Network.ResponseCallback response)
    {
      this.name = "gacha";
      this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
