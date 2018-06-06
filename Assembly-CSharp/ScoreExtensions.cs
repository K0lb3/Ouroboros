// Decompiled with JetBrains decompiler
// Type: ScoreExtensions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using ExitGames.Client.Photon;
using System.Collections.Generic;

public static class ScoreExtensions
{
  public static void SetScore(this PhotonPlayer player, int newScore)
  {
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet.set_Item((object) "score", (object) newScore);
    player.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
  }

  public static void AddScore(this PhotonPlayer player, int scoreToAddToCurrent)
  {
    int num = player.GetScore() + scoreToAddToCurrent;
    Hashtable propertiesToSet = new Hashtable();
    propertiesToSet.set_Item((object) "score", (object) num);
    player.SetCustomProperties(propertiesToSet, (Hashtable) null, false);
  }

  public static int GetScore(this PhotonPlayer player)
  {
    object obj;
    if (((Dictionary<object, object>) player.CustomProperties).TryGetValue((object) "score", out obj))
      return (int) obj;
    return 0;
  }
}
