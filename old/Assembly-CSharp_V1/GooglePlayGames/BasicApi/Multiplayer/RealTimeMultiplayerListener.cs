// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace GooglePlayGames.BasicApi.Multiplayer
{
  public interface RealTimeMultiplayerListener
  {
    void OnRoomSetupProgress(float percent);

    void OnRoomConnected(bool success);

    void OnLeftRoom();

    void OnParticipantLeft(Participant participant);

    void OnPeersConnected(string[] participantIds);

    void OnPeersDisconnected(string[] participantIds);

    void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data);
  }
}
