// Decompiled with JetBrains decompiler
// Type: SRPG.ReqFriendBlockApply
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqFriendBlockApply : WebAPI
  {
    public ReqFriendBlockApply(string[] _friends, string[] _blocks, Network.ResponseCallback _response)
    {
      this.name = "friend/multi/req";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"friends\":[");
      if (_friends != null && _friends.Length > 0)
      {
        for (int index = 0; index < _friends.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("\"");
          stringBuilder.Append(_friends[index]);
          stringBuilder.Append("\"");
        }
      }
      stringBuilder.Append("]");
      stringBuilder.Append(",");
      stringBuilder.Append("\"blocks\":[");
      if (_blocks != null && _blocks.Length > 0)
      {
        for (int index = 0; index < _blocks.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(",");
          stringBuilder.Append("\"");
          stringBuilder.Append(_blocks[index]);
          stringBuilder.Append("\"");
        }
      }
      stringBuilder.Append("]");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = _response;
    }
  }
}
