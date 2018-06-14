// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.PlayerRoomIndexingExtensions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace ExitGames.UtilityScripts
{
  public static class PlayerRoomIndexingExtensions
  {
    public static int GetRoomIndex(this PhotonPlayer player)
    {
      if (!Object.op_Equality((Object) PlayerRoomIndexing.instance, (Object) null))
        return PlayerRoomIndexing.instance.GetRoomIndex(player);
      Debug.LogError((object) "Missing PlayerRoomIndexing Component in Scene");
      return -1;
    }
  }
}
