// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAddChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
