// Decompiled with JetBrains decompiler
// Type: SRPG.TimeManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Manager/Time")]
  public class TimeManager : MonoSingleton<TimeManager>
  {
    private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static long UTC2LOCAL = 32400;
    public static readonly int DEFAULT_FRAME_RATE = 30;
    public static readonly float FPS = 1f / (float) TimeManager.DEFAULT_FRAME_RATE;
    public static readonly string ISO_8601_FORMAT = "yyyy-MM-ddTHH:mm:ss.fffZ";
    private float mTimeScale = 1f;
    private float[] mTimeScales = new float[3]{ 1f, 1f, 1f };
    private float mDeltaTime;
    private float mFixedDeltaTime;
    private float mUnscaledDeltaTime;
    private float mUnscaledFixedDeltaTime;
    private float mReqTimeScale;
    private float mHitStop;

    public static int FrameRate
    {
      get
      {
        return Application.get_targetFrameRate();
      }
    }

    public static float RealTimeSinceStartup
    {
      get
      {
        return Time.get_realtimeSinceStartup();
      }
    }

    public static float DeltaTime
    {
      get
      {
        return MonoSingleton<TimeManager>.Instance.mDeltaTime;
      }
    }

    public static float UnscaledDeltaTime
    {
      get
      {
        return MonoSingleton<TimeManager>.Instance.mUnscaledDeltaTime;
      }
    }

    public static float FixedDeltaTime
    {
      get
      {
        return MonoSingleton<TimeManager>.Instance.mFixedDeltaTime;
      }
    }

    public static float UnscaledFixedDeltaTime
    {
      get
      {
        return MonoSingleton<TimeManager>.Instance.mUnscaledFixedDeltaTime;
      }
    }

    public static float TimeScale
    {
      get
      {
        return MonoSingleton<TimeManager>.Instance.mTimeScale;
      }
      set
      {
        MonoSingleton<TimeManager>.Instance.mReqTimeScale = value;
      }
    }

    protected override void Initialize()
    {
      if (!GameUtility.IsDebugBuild)
        Application.set_targetFrameRate(TimeManager.DEFAULT_FRAME_RATE);
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }

    private void Update()
    {
      if ((double) this.mReqTimeScale != 0.0)
      {
        this.mTimeScale = this.mReqTimeScale;
        this.mReqTimeScale = 0.0f;
      }
      this.mFixedDeltaTime = this.mUnscaledFixedDeltaTime = TimeManager.FPS;
      this.mFixedDeltaTime *= this.mTimeScale;
      this.mDeltaTime = this.mUnscaledDeltaTime = Time.get_deltaTime();
      this.mDeltaTime *= this.mTimeScale;
      if ((double) this.mHitStop > 0.0)
      {
        this.mHitStop -= Time.get_unscaledDeltaTime() * this.mTimeScales[0];
        if ((double) this.mHitStop <= 0.0)
          TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.HitStop, 1f);
      }
      float mTimeScale = this.mTimeScales[0];
      for (int index = 1; index < this.mTimeScales.Length; ++index)
        mTimeScale *= this.mTimeScales[index];
      Time.set_timeScale(mTimeScale);
    }

    public static void StartHitSlow(float rate, float sec)
    {
      TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.HitStop, rate);
      MonoSingleton<TimeManager>.Instance.mHitStop = sec;
    }

    public static void SetTimeScale(TimeManager.TimeScaleGroups group, float value)
    {
      MonoSingleton<TimeManager>.Instance.mTimeScales[(int) group] = value;
    }

    public static long Now()
    {
      return TimeManager.FromDateTime(DateTime.UtcNow);
    }

    public static DateTime FromUnixTime(long unixtime)
    {
      return TimeManager.UNIX_EPOCH.AddSeconds((double) (unixtime + TimeManager.UTC2LOCAL));
    }

    public static long FromDateTime(DateTime datetime)
    {
      return (long) (datetime - TimeManager.UNIX_EPOCH).TotalSeconds - TimeManager.UTC2LOCAL;
    }

    public static string ToMinSecString(long time)
    {
      return (time / 60L).ToString().PadLeft(2, '0') + ":" + (time % 60L).ToString().PadLeft(2, '0');
    }

    public static void HitStop(float sec)
    {
      MonoSingleton<TimeManager>.Instance.mHitStop = sec;
    }

    public static DateTime ServerTime
    {
      get
      {
        return TimeManager.FromUnixTime(Network.GetServerTime());
      }
    }

    public static long GetUnixSec(DateTime targetTime)
    {
      targetTime = targetTime.ToUniversalTime();
      return (long) (targetTime - TimeManager.UNIX_EPOCH).TotalSeconds;
    }

    public enum TimeScaleGroups
    {
      Game,
      HitStop,
      Debug,
    }
  }
}
