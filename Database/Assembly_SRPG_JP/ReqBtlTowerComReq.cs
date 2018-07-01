// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlTowerComReq
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqBtlTowerComReq : WebAPI
  {
    public ReqBtlTowerComReq(string qid, string fid, PartyData partyIndex, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "tower/btl/req";
      stringBuilder.Append("\"qid\":\"");
      stringBuilder.Append(qid);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"fid\":\"");
      stringBuilder.Append(fid);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"fuid\":\"");
      stringBuilder.Append(GlobalVars.SelectedFriendID);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
