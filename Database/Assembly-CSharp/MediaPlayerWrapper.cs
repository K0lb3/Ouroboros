// Decompiled with JetBrains decompiler
// Type: MediaPlayerWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using RenderHeads.Media.AVProVideo;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class MediaPlayerWrapper : MonoBehaviour
{
  [SerializeField]
  private MediaPlayer m_MediaPlayer;
  private bool m_EnableBufferingTimeout;
  private float m_BufferingGraceTime;
  private DateTime m_BufferingStartTime;
  private bool m_IsBuffering;
  private bool m_IsInitialized;
  private MediaPlayerWrapper.Event m_CallBack;
  private Coroutine m_LoadCoroutine;

  public MediaPlayerWrapper()
  {
    base.\u002Ector();
  }

  public MediaPlayerWrapper.Event Events
  {
    get
    {
      if (this.m_CallBack == null)
        this.m_CallBack = new MediaPlayerWrapper.Event();
      return this.m_CallBack;
    }
  }

  public bool isAutoPlay
  {
    get
    {
      return this.m_MediaPlayer.m_AutoStart;
    }
    set
    {
      this.m_MediaPlayer.m_AutoStart = value;
    }
  }

  public bool EnableBufferingTimeout
  {
    get
    {
      return this.m_EnableBufferingTimeout;
    }
    set
    {
      this.m_EnableBufferingTimeout = value;
    }
  }

  public float BufferingGraceTime
  {
    get
    {
      return this.m_BufferingGraceTime;
    }
    set
    {
      this.m_BufferingGraceTime = value;
    }
  }

  private void Start()
  {
    // ISSUE: method pointer
    this.m_MediaPlayer.Events.RemoveListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, RenderHeads.Media.AVProVideo.ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
    // ISSUE: method pointer
    this.m_MediaPlayer.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, RenderHeads.Media.AVProVideo.ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
    this.m_IsInitialized = true;
  }

  public void LoadFromURL(string url, bool autoPlay)
  {
    if (this.m_LoadCoroutine != null)
      this.StopCoroutine(this.m_LoadCoroutine);
    this.m_LoadCoroutine = this.StartCoroutine(this.LoadInternal(url, autoPlay));
  }

  [DebuggerHidden]
  private IEnumerator LoadInternal(string url, bool autoPlay)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new MediaPlayerWrapper.\u003CLoadInternal\u003Ec__Iterator50()
    {
      url = url,
      autoPlay = autoPlay,
      \u003C\u0024\u003Eurl = url,
      \u003C\u0024\u003EautoPlay = autoPlay,
      \u003C\u003Ef__this = this
    };
  }

  public void Play()
  {
    this.m_MediaPlayer.Control.Play();
  }

  public void Stop()
  {
    this.m_MediaPlayer.Control.Stop();
  }

  public void Pause()
  {
    this.m_MediaPlayer.Control.Pause();
  }

  public void Unload()
  {
    this.m_MediaPlayer.Control.CloseVideo();
  }

  public void SkipToEnd()
  {
    this.Seek01(1f);
  }

  public void BackToStart()
  {
    this.Seek01(0.0f);
  }

  public void Reload(bool autoStart = false)
  {
    float seek01 = this.GetSeek01();
    this.m_MediaPlayer.CloseVideo();
    this.LoadFromURL(this.m_MediaPlayer.m_VideoPath, autoStart);
    this.Seek01(seek01);
  }

  public void Seek01(float value)
  {
    if ((double) value > 0.0)
      this.m_MediaPlayer.Control.Seek(this.m_MediaPlayer.Info.GetDurationMs() * value);
    else
      this.m_MediaPlayer.Control.Seek(0.0f);
  }

  public float GetSeek01()
  {
    return this.m_MediaPlayer.Control.GetCurrentTimeMs() / this.m_MediaPlayer.Info.GetDurationMs();
  }

  public void SetVolume(float value)
  {
    this.m_MediaPlayer.m_Volume = value;
  }

  public float GetVolume()
  {
    return this.m_MediaPlayer.m_Volume;
  }

  public float GetBufferingProgress()
  {
    return this.m_MediaPlayer.Control.GetBufferingProgress();
  }

  public int GetBufferedTimeRangeCount()
  {
    return this.m_MediaPlayer.Control.GetBufferedTimeRangeCount();
  }

  private void Notify(MediaPlayerWrapper.Event.Type eventType)
  {
    this.Events.Invoke(eventType);
  }

  private void OnMediaPlayerEvent(MediaPlayer mediaPlayer, MediaPlayerEvent.EventType eventType, RenderHeads.Media.AVProVideo.ErrorCode errorCode)
  {
    switch (eventType)
    {
      case MediaPlayerEvent.EventType.MetaDataReady:
        this.Notify(MediaPlayerWrapper.Event.Type.MetaDataReady);
        break;
      case MediaPlayerEvent.EventType.ReadyToPlay:
        this.Notify(MediaPlayerWrapper.Event.Type.ReadyToPlay);
        break;
      case MediaPlayerEvent.EventType.Started:
        this.Notify(MediaPlayerWrapper.Event.Type.Started);
        break;
      case MediaPlayerEvent.EventType.FirstFrameReady:
        this.Notify(MediaPlayerWrapper.Event.Type.FirstFrameReady);
        break;
      case MediaPlayerEvent.EventType.FinishedPlaying:
        this.Notify(MediaPlayerWrapper.Event.Type.FinishedPlaying);
        break;
      case MediaPlayerEvent.EventType.Closing:
        this.Notify(MediaPlayerWrapper.Event.Type.Closing);
        break;
      case MediaPlayerEvent.EventType.Error:
        this.Notify(MediaPlayerWrapper.Event.Type.Error);
        break;
      case MediaPlayerEvent.EventType.SubtitleChange:
        this.Notify(MediaPlayerWrapper.Event.Type.SubtitleChange);
        break;
      case MediaPlayerEvent.EventType.Stalled:
        this.Notify(MediaPlayerWrapper.Event.Type.Stalled);
        break;
      case MediaPlayerEvent.EventType.Unstalled:
        this.Notify(MediaPlayerWrapper.Event.Type.Unstalled);
        break;
    }
  }

  private void Update()
  {
    if (!this.m_IsBuffering && this.m_MediaPlayer.Control.IsBuffering())
    {
      this.m_BufferingStartTime = DateTime.Now;
      this.Notify(MediaPlayerWrapper.Event.Type.BufferingStart);
    }
    else if (this.m_IsBuffering && !this.m_MediaPlayer.Control.IsBuffering())
      this.Notify(MediaPlayerWrapper.Event.Type.BufferingEnd);
    else if (this.m_IsBuffering && this.m_EnableBufferingTimeout && (DateTime.Now - this.m_BufferingStartTime).TotalSeconds > (double) this.m_BufferingGraceTime)
      this.Notify(MediaPlayerWrapper.Event.Type.BufferingTimeout);
    this.UpdateValues();
  }

  private void UpdateValues()
  {
    this.m_IsBuffering = this.m_MediaPlayer.Control.IsBuffering();
  }

  public class Event : UnityEvent<MediaPlayerWrapper.Event.Type>
  {
    public Event()
    {
      base.\u002Ector();
    }

    public enum Type
    {
      MetaDataReady,
      ReadyToPlay,
      Started,
      FirstFrameReady,
      FinishedPlaying,
      Closing,
      Error,
      SubtitleChange,
      Stalled,
      Unstalled,
      LoadFailed,
      BufferingStart,
      BufferingEnd,
      BufferingTimeout,
    }
  }

  public delegate void OnMediaPlayerWrapperEvent(MediaPlayerWrapper.Event.Type eventType);
}
