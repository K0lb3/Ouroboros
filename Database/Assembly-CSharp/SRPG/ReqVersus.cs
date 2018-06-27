// Decompiled with JetBrains decompiler
// Type: SRPG.ReqVersus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqVersus : WebAPI
  {
    public ReqVersus(string iname, int plid, int seat, string uid, Network.ResponseCallback response, VERSUS_TYPE type)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "vs/" + type.ToString().ToLower() + "match/req";
      stringBuilder.Append("\"iname\":\"");
      stringBuilder.Append("QE_VS_TEST_00");
      stringBuilder.Append("\",");
      stringBuilder.Append("\"token\":\"");
      stringBuilder.Append(JsonEscape.Escape(GlobalVars.SelectedMultiPlayRoomName));
      stringBuilder.Append("\",");
      stringBuilder.Append("\"plid\":\"");
      stringBuilder.Append(plid);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"seat\":\"");
      stringBuilder.Append(seat);
      stringBuilder.Append("\",");
      stringBuilder.Append("\"uid\":\"");
      stringBuilder.Append(uid);
      stringBuilder.Append("\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
