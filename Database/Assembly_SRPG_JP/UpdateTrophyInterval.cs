// Decompiled with JetBrains decompiler
// Type: SRPG.UpdateTrophyInterval
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UpdateTrophyInterval
  {
    private const float NOW_TROPHY_INTERVAL_TIME = 0.0f;
    private const float UPDATE_TROPHY_INTERVAL_TIME = 5f;
    private const float SYNC_TROPHY_INTERVAL_TIME = 0.1f;
    private float updat_torphy_interval;

    public void SetSyncNow()
    {
      this.updat_torphy_interval = 0.0f;
    }

    public void SetSyncInterval()
    {
      this.updat_torphy_interval = Mathf.Max(this.updat_torphy_interval, 0.1f);
    }

    public void SetUpdateInterval()
    {
      this.updat_torphy_interval = Mathf.Max(this.updat_torphy_interval, 5f);
    }

    public bool PlayCheckUpdate()
    {
      if (0.0 >= (double) this.updat_torphy_interval)
        return true;
      this.updat_torphy_interval -= Time.get_unscaledDeltaTime();
      return false;
    }
  }
}
