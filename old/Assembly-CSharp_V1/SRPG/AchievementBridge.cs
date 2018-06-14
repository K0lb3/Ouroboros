// Decompiled with JetBrains decompiler
// Type: SRPG.AchievementBridge
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
        GameCenterManager.ShowAchievement();
      else
        GameCenterManager.ReAuth();
    }
  }
}
