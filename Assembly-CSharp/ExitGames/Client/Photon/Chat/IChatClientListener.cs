// Decompiled with JetBrains decompiler
// Type: ExitGames.Client.Photon.Chat.IChatClientListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace ExitGames.Client.Photon.Chat
{
  public interface IChatClientListener
  {
    void DebugReturn(DebugLevel level, string message);

    void OnDisconnected();

    void OnConnected();

    void OnChatStateChange(ChatState state);

    void OnGetMessages(string channelName, string[] senders, object[] messages);

    void OnPrivateMessage(string sender, object message, string channelName);

    void OnSubscribed(string[] channels, bool[] results);

    void OnUnsubscribed(string[] channels);

    void OnStatusUpdate(string user, int status, bool gotMessage, object message);
  }
}
