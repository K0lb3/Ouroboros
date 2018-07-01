// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAddChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqAddChatBlackList : WebAPI
  {
    public ReqAddChatBlackList(string target_uid, Network.ResponseCallback response)
    {
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      this.name = "chat/blacklist/add";
      stringBuilder.Append("\"target_uid\":\"" + target_uid + "\"");
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
