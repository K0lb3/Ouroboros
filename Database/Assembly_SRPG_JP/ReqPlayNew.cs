// Decompiled with JetBrains decompiler
// Type: SRPG.ReqPlayNew
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
