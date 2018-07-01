// Decompiled with JetBrains decompiler
// Type: SRPG.ReqChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqChatBlackList : WebAPI
  {
    public ReqChatBlackList(int start_id, int limit, int exclude_id, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/blacklist";
      stringBuilder.Append("\"start_id\":" + start_id.ToString() + ",");
      stringBuilder.Append("\"limit\":" + limit.ToString() + ",");
      stringBuilder.Append("\"exclude_id\":" + exclude_id.ToString());
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }

    public ReqChatBlackList(int offset, int limit, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/blacklist";
      stringBuilder.Append("\"offset\":" + offset.ToString() + ",");
      stringBuilder.Append("\"limit\":" + limit.ToString());
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
