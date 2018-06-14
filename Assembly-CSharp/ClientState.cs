// Decompiled with JetBrains decompiler
// Type: ClientState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public enum ClientState
{
  Uninitialized,
  PeerCreated,
  Queued,
  Authenticated,
  JoinedLobby,
  DisconnectingFromMasterserver,
  ConnectingToGameserver,
  ConnectedToGameserver,
  Joining,
  Joined,
  Leaving,
  DisconnectingFromGameserver,
  ConnectingToMasterserver,
  QueuedComingFromGameserver,
  Disconnecting,
  Disconnected,
  ConnectedToMaster,
  ConnectingToNameServer,
  ConnectedToNameServer,
  DisconnectingFromNameServer,
  Authenticating,
}
