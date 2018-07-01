// Decompiled with JetBrains decompiler
// Type: SRPG.ReqSendChatStampWorld
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
