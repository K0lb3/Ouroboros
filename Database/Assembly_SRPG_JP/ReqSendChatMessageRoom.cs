// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSendChatMessageRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqSendChatMessageRoom : WebAPI
  {
    public ReqSendChatMessageRoom(string room_token, string message, string[] uids, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/room/send";
      stringBuilder.Append("\"room_token\":\"" + room_token + "\",");
      stringBuilder.Append("\"message\":\"" + message + "\",");
      stringBuilder.Append("\"uids\":[");
      for (int index = 0; index < uids.Length; ++index)
      {
        stringBuilder.Append("\"" + uids[index] + "\"");
        if (index != uids.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
