// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.BasicApi.Multiplayer.RealTimeMultiplayerListener
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
