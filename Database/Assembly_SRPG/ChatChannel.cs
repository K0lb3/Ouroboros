// Decompiled with JetBrains decompiler
// Type: SRPG.ChatChannel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class ChatChannel
  {
    public ChatChannelParam[] channels;

    public void Deserialize(JSON_ChatChannel json)
    {
      if (json == null || json.channels == null)
        return;
      this.channels = new ChatChannelParam[json.channels.Length];
      ChatChannelMasterParam[] chatChannelMaster = MonoSingleton<GameManager>.Instance.GetChatChannelMaster();
      for (int index = 0; index < json.channels.Length; ++index)
      {
        this.channels[index] = json.channels[index];
        if (chatChannelMaster.Length >= this.channels[index].id)
        {
          this.channels[index].category_id = (int) chatChannelMaster[this.channels[index].id - 1].category_id;
          this.channels[index].name = chatChannelMaster[this.channels[index].id - 1].name;
        }
      }
    }
  }
}
