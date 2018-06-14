// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Detectors.SpeedHackDetector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;

namespace CodeStage.AntiCheat.Detectors
{
  [AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Speed Hack Detector")]
  public class SpeedHackDetector : ActDetectorBase
  {
    [Tooltip("Time (in seconds) between detector checks.")]
    public float interval = 1f;
    [Tooltip("Maximum false positives count allowed before registering speed hack.")]
    public byte maxFalsePositives = 3;
    [Tooltip("Amount of sequential successful checks before clearing internal false positives counter.\nSet 0 to disable Cool Down feature.")]
    public int coolDown = 30;
    internal const string COMPONENT_NAME = "Speed Hack Detector";
    internal const string FINAL_LOG_PREFIX = "[ACTk] Speed Hack Detector: ";
    private const long TICKS_PER_SECOND = 10000000;
    private const int THRESHOLD = 5000000;
    private static int instancesInScene;
    private byte currentFalsePositives;
    private int currentCooldownShots;
    private long ticksOnStart;
    private long vulnerableTicksOnStart;
    private long prevTicks;
    private long prevIntervalTicks;

    private SpeedHackDetector()
    {
    }

    public static void StartDetection()
    {
      if (Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) null))
        SpeedHackDetector.Instance.StartDetectionInternal((UnityAction) null, SpeedHackDetector.Instance.interval, SpeedHackDetector.Instance.maxFalsePositives, SpeedHackDetector.Instance.coolDown);
      else
        Debug.LogError((object) "[ACTk] Speed Hack Detector: can't be started since it doesn't exists in scene or not yet initialized!");
    }

    public static void StartDetection(UnityAction callback)
    {
      SpeedHackDetector.StartDetection(callback, SpeedHackDetector.GetOrCreateInstance.interval);
    }

    public static void StartDetection(UnityAction callback, float interval)
    {
      SpeedHackDetector.StartDetection(callback, interval, SpeedHackDetector.GetOrCreateInstance.maxFalsePositives);
    }

    public static void StartDetection(UnityAction callback, float interval, byte maxFalsePositives)
    {
      SpeedHackDetector.StartDetection(callback, interval, maxFalsePositives, SpeedHackDetector.GetOrCreateInstance.coolDown);
    }

    public static void StartDetection(UnityAction callback, float interval, byte maxFalsePositives, int coolDown)
    {
      SpeedHackDetector.GetOrCreateInstance.StartDetectionInternal(callback, interval, maxFalsePositives, coolDown);
    }

    public static void StopDetection()
    {
      if (!Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) null))
        return;
      SpeedHackDetector.Instance.StopDetectionInternal();
    }

    public static void Dispose()
    {
      if (!Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) null))
        return;
      SpeedHackDetector.Instance.DisposeInternal();
    }

    public static SpeedHackDetector Instance { get; private set; }

    private static SpeedHackDetector GetOrCreateInstance
    {
      get
      {
        if (Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) null))
          return SpeedHackDetector.Instance;
        if (Object.op_Equality((Object) ActDetectorBase.detectorsContainer, (Object) null))
          ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
        SpeedHackDetector.Instance = (SpeedHackDetector) ActDetectorBase.detectorsContainer.AddComponent<SpeedHackDetector>();
        return SpeedHackDetector.Instance;
      }
    }

    private void Awake()
    {
      ++SpeedHackDetector.instancesInScene;
      if (!this.Init((ActDetectorBase) SpeedHackDetector.Instance, "Speed Hack Detector"))
        return;
      SpeedHackDetector.Instance = this;
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      --SpeedHackDetector.instancesInScene;
    }

    private void OnLevelWasLoaded(int index)
    {
      if (SpeedHackDetector.instancesInScene < 2)
      {
        if (this.keepAlive)
          return;
        this.DisposeInternal();
      }
      else
      {
        if (this.keepAlive || !Object.op_Inequality((Object) SpeedHackDetector.Instance, (Object) this))
          return;
        this.DisposeInternal();
      }
    }

    private void OnApplicationPause(bool pause)
    {
      if (pause)
        return;
      this.ResetStartTicks();
    }

    private void Update()
    {
      if (!this.isRunning)
        return;
      long ticks = DateTime.UtcNow.Ticks;
      long num1 = ticks - this.prevTicks;
      if (num1 < 0L || num1 > 10000000L)
      {
        this.ResetStartTicks();
      }
      else
      {
        this.prevTicks = ticks;
        long num2 = (long) ((double) this.interval * 10000000.0);
        if (ticks - this.prevIntervalTicks < num2)
          return;
        if ((double) Mathf.Abs((float) ((long) Environment.TickCount * 10000L - this.vulnerableTicksOnStart - (ticks - this.ticksOnStart))) > 5000000.0)
        {
          ++this.currentFalsePositives;
          if ((int) this.currentFalsePositives > (int) this.maxFalsePositives)
          {
            this.OnCheatingDetected();
          }
          else
          {
            this.currentCooldownShots = 0;
            this.ResetStartTicks();
          }
        }
        else if ((int) this.currentFalsePositives > 0 && this.coolDown > 0)
        {
          ++this.currentCooldownShots;
          if (this.currentCooldownShots >= this.coolDown)
            this.currentFalsePositives = (byte) 0;
        }
        this.prevIntervalTicks = ticks;
      }
    }

    private void StartDetectionInternal(UnityAction callback, float checkInterval, byte falsePositives, int shotsTillCooldown)
    {
      if (this.isRunning)
        Debug.LogWarning((object) "[ACTk] Speed Hack Detector: already running!", (Object) this);
      else if (!((Behaviour) this).get_enabled())
      {
        Debug.LogWarning((object) "[ACTk] Speed Hack Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", (Object) this);
      }
      else
      {
        if (callback != null && this.detectionEventHasListener)
          Debug.LogWarning((object) "[ACTk] Speed Hack Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", (Object) this);
        if (callback == null && !this.detectionEventHasListener)
        {
          Debug.LogWarning((object) "[ACTk] Speed Hack Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", (Object) this);
          ((Behaviour) this).set_enabled(false);
        }
        else
        {
          this.detectionAction = callback;
          this.interval = checkInterval;
          this.maxFalsePositives = falsePositives;
          this.coolDown = shotsTillCooldown;
          this.ResetStartTicks();
          this.currentFalsePositives = (byte) 0;
          this.currentCooldownShots = 0;
          this.started = true;
          this.isRunning = true;
        }
      }
    }

    protected override void StartDetectionAutomatically()
    {
      this.StartDetectionInternal((UnityAction) null, this.interval, this.maxFalsePositives, this.coolDown);
    }

    protected override void PauseDetector()
    {
      this.isRunning = false;
    }

    protected override void ResumeDetector()
    {
      if (this.detectionAction == null && !this.detectionEventHasListener)
        return;
      this.isRunning = true;
    }

    protected override void StopDetectionInternal()
    {
      if (!this.started)
        return;
      this.detectionAction = (UnityAction) null;
      this.started = false;
      this.isRunning = false;
    }

    protected override void DisposeInternal()
    {
      base.DisposeInternal();
      if (!Object.op_Equality((Object) SpeedHackDetector.Instance, (Object) this))
        return;
      SpeedHackDetector.Instance = (SpeedHackDetector) null;
    }

    private void ResetStartTicks()
    {
      this.ticksOnStart = DateTime.UtcNow.Ticks;
      this.vulnerableTicksOnStart = (long) Environment.TickCount * 10000L;
      this.prevTicks = this.ticksOnStart;
      this.prevIntervalTicks = this.ticksOnStart;
    }
  }
}
