// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComCont
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqBtlComCont : WebAPI
  {
    public ReqBtlComCont(long btlid, BattleCore.Record record, Network.ResponseCallback response, bool multi, bool isMT)
    {
      if (isMT)
        this.name = "btl/multi/tower/cont";
      else
        this.name = !multi ? "btl/com/cont" : "btl/multi/cont";
      if (record != null)
      {
        this.body = "\"btlid\":" + (object) btlid + ",";
        if (!string.IsNullOrEmpty(WebAPI.GetBtlEndParamString(record, multi)))
          this.body += WebAPI.GetBtlEndParamString(record, multi);
        this.body = WebAPI.GetRequestString(this.body);
      }
      else
        this.body = WebAPI.GetRequestString("\"btlid\":\"" + (object) btlid + "\"");
      this.callback = response;
    }
  }
}
