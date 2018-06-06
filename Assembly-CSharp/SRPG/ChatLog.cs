// Decompiled with JetBrains decompiler
// Type: SRPG.ChatLog
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class ChatLog
  {
    public List<ChatLogParam> messages = new List<ChatLogParam>();

    public void Deserialize(JSON_ChatLog json)
    {
      if (json == null)
        return;
      this.messages.Clear();
      this.messages = new List<ChatLogParam>(30);
      if (json.messages == null)
        return;
      this.messages.AddRange((IEnumerable<ChatLogParam>) json.messages);
    }
  }
}
