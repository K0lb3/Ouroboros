// Decompiled with JetBrains decompiler
// Type: SRPG.ReqItemComposit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
