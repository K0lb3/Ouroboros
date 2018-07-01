// Decompiled with JetBrains decompiler
// Type: SRPG.ReqChatMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqChatMessage : WebAPI
  {
    public ReqChatMessage(long start_id, int channel, int limit, long exclude_id, bool isMultiPush, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/message";
      stringBuilder.Append("\"start_id\":" + start_id.ToString() + ",");
      stringBuilder.Append("\"channel\":" + channel.ToString() + ",");
      stringBuilder.Append("\"limit\":" + limit.ToString() + ",");
      stringBuilder.Append("\"exclude_id\":" + exclude_id.ToString());
      if (isMultiPush)
        stringBuilder.Append(",\"is_multi_push\":1");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
