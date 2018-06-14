// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelAutoAssign
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ChatChannelAutoAssign
  {
    public int channel;

    public void Deserialize(JSON_ChatChannelAutoAssign json)
    {
      if (json == null)
        return;
      this.channel = json.channel;
    }
  }
}
