// Decompiled with JetBrains decompiler
// Type: TurnExtensions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;

public static class TurnExtensions
{
  public static readonly string TurnPropKey = "Turn";
  public static readonly string TurnStartPropKey = "TStart";
  public static readonly string FinishedTurnPropKey = "FToA";

  public static void SetTurn(this Room room, int turn, bool setStartTime = false)
  {
    if (room == null || room.CustomProperties == null)
      return;
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet.set_Item((object) TurnExtensions.TurnPropKey, (object) turn);
    if (setStartTime)
      propertiesToSet.set_Item((object) TurnExtensions.TurnStartPropKey, (object) PhotonNetwork.ServerTimestamp);
    room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
  }

  public static int GetTurn(this RoomInfo room)
  {
    if (room == null || room.CustomProperties == null || !((Dictionary<object, object>) room.CustomProperties).ContainsKey((object) TurnExtensions.TurnPropKey))
      return 0;
    return (int) room.CustomProperties.get_Item((object) TurnExtensions.TurnPropKey);
  }

  public static int GetTurnStart(this RoomInfo room)
  {
    if (room == null || room.CustomProperties == null || !((Dictionary<object, object>) room.CustomProperties).ContainsKey((object) TurnExtensions.TurnStartPropKey))
      return 0;
    return (int) room.CustomProperties.get_Item((object) TurnExtensions.TurnStartPropKey);
  }

  public static int GetFinishedTurn(this PhotonPlayer player)
  {
    Room room = PhotonNetwork.room;
    if (room == null || room.CustomProperties == null || !((Dictionary<object, object>) room.CustomProperties).ContainsKey((object) TurnExtensions.TurnPropKey))
      return 0;
    string str = TurnExtensions.FinishedTurnPropKey + (object) player.ID;
    return (int) room.CustomProperties.get_Item((object) str);
  }

  public static void SetFinishedTurn(this PhotonPlayer player, int turn)
  {
    Room room = PhotonNetwork.room;
    if (room == null || room.CustomProperties == null)
      return;
    string str = TurnExtensions.FinishedTurnPropKey + (object) player.ID;
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet.set_Item((object) str, (object) turn);
    room.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
  }
}
