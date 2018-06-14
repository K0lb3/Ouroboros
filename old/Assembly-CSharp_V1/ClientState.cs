// Decompiled with JetBrains decompiler
// Type: ClientState
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
