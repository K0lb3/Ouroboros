// Decompiled with JetBrains decompiler
// Type: ExitGames.UtilityScripts.PlayerRoomIndexingExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
