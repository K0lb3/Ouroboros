// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemComposit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqItemComposit : WebAPI
  {
    public ReqItemComposit(string iname, bool is_cmn, Network.ResponseCallback response)
    {
      this.name = "item/gousei";
      int num = !is_cmn ? 0 : 1;
      this.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\",\"is_cmn\":" + (object) num);
      this.callback = response;
    }
  }
}
