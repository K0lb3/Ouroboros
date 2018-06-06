// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBtlComReq
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqBtlComReq : WebAPI
  {
    public ReqBtlComReq(string iname, string fuid, Network.ResponseCallback response, bool multi, int partyIndex, bool isHost = false, int plid = 0, int seat = 0)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = !multi ? "btl/com/req" : "btl/multi/req";
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append(iname);
      stringBuilder.Append("\",");
      if (partyIndex >= 0)
      {
        stringBuilder.Append("\"partyid\":");
        stringBuilder.Append(partyIndex);
        stringBuilder.Append(",");
      }
      if (multi)
      {
        stringBuilder.Append("\"token\":\"");
        stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
        stringBuilder.Append("\",");
        stringBuilder.Append("\"host\":\"");
        stringBuilder.Append(!isHost ? "0" : "1");
        stringBuilder.Append("\",");
        stringBuilder.Append("\"plid\":\"");
        stringBuilder.Append(plid);
        stringBuilder.Append("\",");
        stringBuilder.Append("\"seat\":\"");
        stringBuilder.Append(seat);
        stringBuilder.Append("\",");
      }
      stringBuilder.Append("\"btlparam\":{\"help\":{\"fuid\":\"");
      stringBuilder.Append(fuid);
      stringBuilder.Append("\"}}");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
