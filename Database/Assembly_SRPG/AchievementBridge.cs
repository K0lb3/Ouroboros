// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementBridge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
