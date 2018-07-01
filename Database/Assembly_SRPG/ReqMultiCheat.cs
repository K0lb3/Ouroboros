// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMultiCheat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqMultiCheat : WebAPI
  {
    public ReqMultiCheat(int type, string val, Network.ResponseCallback response)
    {
      this.name = "btl/multi/cheat";
      this.body = string.Empty;
      ReqMultiCheat reqMultiCheat1 = this;
      reqMultiCheat1.body = reqMultiCheat1.body + "\"type\":" + (object) type + ",";
      ReqMultiCheat reqMultiCheat2 = this;
      reqMultiCheat2.body = reqMultiCheat2.body + "\"val\":\"" + JsonEscape.Escape(val) + "\"";
      this.body = WebAPI.GetRequestString(this.body);
      this.callback = response;
    }
  }
}
