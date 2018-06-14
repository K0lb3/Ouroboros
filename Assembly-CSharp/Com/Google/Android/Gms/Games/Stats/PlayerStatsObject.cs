// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Games.Stats.PlayerStatsObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Google.Developers;
using System;

namespace Com.Google.Android.Gms.Games.Stats
{
  public class PlayerStatsObject : JavaObjWrapper, PlayerStats
  {
    private const string CLASS_NAME = "com/google/android/gms/games/stats/PlayerStats";

    public PlayerStatsObject(IntPtr ptr)
      : base(ptr)
    {
    }

    public static float UNSET_VALUE
    {
      get
      {
        return JavaObjWrapper.GetStaticFloatField("com/google/android/gms/games/stats/PlayerStats", nameof (UNSET_VALUE));
      }
    }

    public static int CONTENTS_FILE_DESCRIPTOR
    {
      get
      {
        return JavaObjWrapper.GetStaticIntField("com/google/android/gms/games/stats/PlayerStats", nameof (CONTENTS_FILE_DESCRIPTOR));
      }
    }

    public static int PARCELABLE_WRITE_RETURN_VALUE
    {
      get
      {
        return JavaObjWrapper.GetStaticIntField("com/google/android/gms/games/stats/PlayerStats", nameof (PARCELABLE_WRITE_RETURN_VALUE));
      }
    }

    public float getAverageSessionLength()
    {
      return this.InvokeCall<float>(nameof (getAverageSessionLength), "()F");
    }

    public int getDaysSinceLastPlayed()
    {
      return this.InvokeCall<int>(nameof (getDaysSinceLastPlayed), "()I");
    }

    public int getNumberOfPurchases()
    {
      return this.InvokeCall<int>(nameof (getNumberOfPurchases), "()I");
    }

    public int getNumberOfSessions()
    {
      return this.InvokeCall<int>(nameof (getNumberOfSessions), "()I");
    }

    public float getSessionPercentile()
    {
      return this.InvokeCall<float>(nameof (getSessionPercentile), "()F");
    }

    public float getSpendPercentile()
    {
      return this.InvokeCall<float>(nameof (getSpendPercentile), "()F");
    }
  }
}
