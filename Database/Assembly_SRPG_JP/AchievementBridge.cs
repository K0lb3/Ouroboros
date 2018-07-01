// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementBridge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class AchievementBridge : MonoBehaviour
  {
    public AchievementBridge()
    {
      base.\u002Ector();
    }

    public void OnClick()
    {
      if (GameCenterManager.IsAuth())
      {
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        if (Object.op_Inequality((Object) instanceDirect, (Object) null))
          instanceDirect.Player.UpdateAchievementTrophyStates();
        GameCenterManager.ShowAchievement();
      }
      else
        GameCenterManager.ReAuth();
    }
  }
}
