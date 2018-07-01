// Decompiled with JetBrains decompiler
// Type: SRPG.ReqChatChannelList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqChatChannelList : WebAPI
  {
    public ReqChatChannelList(int start_id, int limit, int exclude_id, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/channel";
      stringBuilder.Append("\"start_id\":" + start_id.ToString() + ",");
      stringBuilder.Append("\"limit\":" + limit.ToString() + ",");
      stringBuilder.Append("\"exclude_id\":" + exclude_id.ToString());
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public ReqChatChannelList(int[] channel_ids, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/channel";
      stringBuilder.Append("\"channel_ids\":[");
      for (int index = 0; index < channel_ids.Length; ++index)
      {
        stringBuilder.Append(channel_ids[index]);
        if (index != channel_ids.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
