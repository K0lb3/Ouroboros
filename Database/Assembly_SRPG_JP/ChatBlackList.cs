// Decompiled with JetBrains decompiler
// Type: SRPG.ChatBlackList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
