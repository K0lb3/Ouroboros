// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComCont
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
