// Decompiled with JetBrains decompiler
// Type: SRPG.ChatBlackList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ChatBlackList
  {
    public ChatBlackListParam[] lists;
    public int total;

    public void Deserialize(JSON_ChatBlackList json)
    {
      if (json == null)
        return;
      this.lists = (ChatBlackListParam[]) null;
      if (json.blacklist != null)
      {
        this.lists = new ChatBlackListParam[json.blacklist.Length];
        for (int index = 0; index < json.blacklist.Length; ++index)
          this.lists[index] = json.blacklist[index];
      }
      else
        this.lists = new ChatBlackListParam[0];
      this.total = json.total;
    }
  }
}
