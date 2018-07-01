// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannelAutoAssign
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
