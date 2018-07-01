// Decompiled with JetBrains decompiler
// Type: SRPG.ReqPlayNew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ReqPlayNew : WebAPI
  {
    public ReqPlayNew(bool isDebug, Network.ResponseCallback response)
    {
      this.name = "playnew";
      this.body = string.Empty;
      string str = string.Empty;
      if (isDebug)
        str = "\"debug\":1,";
      this.body += WebAPI.GetRequestString(str + "\"permanent_id\":\"" + MonoSingleton<GameManager>.Instance.UdId + "\"");
      this.callback = response;
    }
  }
}
