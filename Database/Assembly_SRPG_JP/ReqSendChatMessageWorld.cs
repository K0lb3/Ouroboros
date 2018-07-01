// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSendChatMessageWorld
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqSendChatMessageWorld : WebAPI
  {
    public ReqSendChatMessageWorld(int channel, string message, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/send";
      stringBuilder.Append("\"channel\":" + channel.ToString() + ",");
      stringBuilder.Append("\"message\":\"" + message + "\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
