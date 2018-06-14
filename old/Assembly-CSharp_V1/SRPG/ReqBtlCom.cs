// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlCom
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlCom : WebAPI
  {
    public ReqBtlCom(Network.ResponseCallback response, bool refresh = false)
    {
      this.name = "btl/com";
      if (refresh)
        this.body = WebAPI.GetRequestString("\"event\":1");
      else
        this.body = WebAPI.GetRequestString((string) null);
      this.callback = response;
    }
  }
}
