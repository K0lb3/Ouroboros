// Decompiled with JetBrains decompiler
// Type: TypedLobbyInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public class TypedLobbyInfo : TypedLobby
{
  public int PlayerCount;
  public int RoomCount;

  public override string ToString()
  {
    return string.Format("TypedLobbyInfo '{0}'[{1}] rooms: {2} players: {3}", new object[4]
    {
      (object) this.Name,
      (object) this.Type,
      (object) this.RoomCount,
      (object) this.PlayerCount
    });
  }
}
