// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Games.Stats.Stats_LoadPlayerStatsResultObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Com.Google.Android.Gms.Common.Api;
using Google.Developers;
using System;

namespace Com.Google.Android.Gms.Games.Stats
{
  public class Stats_LoadPlayerStatsResultObject : JavaObjWrapper, Result, Stats_LoadPlayerStatsResult
  {
    private const string CLASS_NAME = "com/google/android/gms/games/stats/Stats$LoadPlayerStatsResult";

    public Stats_LoadPlayerStatsResultObject(IntPtr ptr)
      : base(ptr)
    {
    }

    public PlayerStats getPlayerStats()
    {
      return (PlayerStats) new PlayerStatsObject(this.InvokeCall<IntPtr>(nameof (getPlayerStats), "()Lcom/google/android/gms/games/stats/PlayerStats;"));
    }

    public Status getStatus()
    {
      return new Status(this.InvokeCall<IntPtr>(nameof (getStatus), "()Lcom/google/android/gms/common/api/Status;"));
    }
  }
}
