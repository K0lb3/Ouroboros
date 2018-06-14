// Decompiled with JetBrains decompiler
// Type: EnterRoomParams
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
