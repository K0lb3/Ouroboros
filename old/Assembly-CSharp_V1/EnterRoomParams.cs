// Decompiled with JetBrains decompiler
// Type: EnterRoomParams
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;

internal class EnterRoomParams
{
  public bool OnGameServer = true;
  public string RoomName;
  public RoomOptions RoomOptions;
  public TypedLobby Lobby;
  public Hashtable PlayerProperties;
  public bool CreateIfNotExists;
  public bool RejoinOnly;
  public string[] ExpectedUsers;
}
