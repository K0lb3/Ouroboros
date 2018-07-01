// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSendChatStampWorld
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqSendChatStampWorld : WebAPI
  {
    public ReqSendChatStampWorld(int channel, int stamp_id, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/send/stamp";
      stringBuilder.Append("\"channel\":" + channel.ToString() + ",");
      stringBuilder.Append("\"stamp_id\":" + stamp_id.ToString());
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
