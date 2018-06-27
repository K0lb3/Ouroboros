// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.BaseMediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  public abstract class BaseMediaPlayer : IMediaPlayer, IMediaSubtitles, IMediaControl, IMediaInfo, IMediaProducer, IDisposable
  {
    protected string _playerDescription = string.Empty;
    protected FilterMode _defaultTextureFilterMode = (FilterMode) 1;
    protected TextureWrapMode _defaultTextureWrapMode = (TextureWrapMode) 1;
    protected int _defaultTextureAnisoLevel = 1;
    protected ErrorCode _lastError;
    protected List<Subtitle> _subtitles;
    protected Subtitle _currentSubtitle;

    public abstract string GetVersion();

    public abstract bool OpenVideoFromFile(string path, long offset, string httpHeaderJson);

    public virtual bool OpenVideoFromBuffer(byte[] buffer)
    {
      return false;
    }

    public abstract void CloseVideo();

    public abstract void SetLooping(bool bLooping);

    public abstract bool IsLooping();

    public abstract bool HasMetaData();

    public abstract bool CanPlay();

    public abstract void Play();

    public abstract void Pause();

    public abstract void Stop();

    public abstract void Rewind();

    public abstract void Seek(float timeMs);

    public abstract void SeekFast(float timeMs);

    public abstract float GetCurrentTimeMs();

    public abstract float GetPlaybackRate();

    public abstract void SetPlaybackRate(float rate);

    public abstract float GetDurationMs();

    public abstract int GetVideoWidth();

    public abstract int GetVideoHeight();

    public abstract float GetVideoDisplayRate();

    public abstract bool HasAudio();

    public abstract bool HasVideo();

    public abstract bool IsSeeking();

    public abstract bool IsPlaying();

    public abstract bool IsPaused();

    public abstract bool IsFinished();

    public abstract bool IsBuffering();

    public virtual int GetTextureCount()
    {
      return 1;
    }

    public abstract Texture GetTexture(int index = 0);

    public abstract int GetTextureFrameCount();

    public virtual long GetTextureTimeStamp()
    {
      return long.MinValue;
    }

    public abstract bool RequiresVerticalFlip();

    public virtual float[] GetTextureTransform()
    {
      return new float[6]{ 1f, 0.0f, 0.0f, 1f, 0.0f, 0.0f };
    }

    public abstract void MuteAudio(bool bMuted);

    public abstract bool IsMuted();

    public abstract void SetVolume(float volume);

    public virtual void SetBalance(float balance)
    {
    }

    public abstract float GetVolume();

    public virtual float GetBalance()
    {
      return 0.0f;
    }

    public abstract int GetAudioTrackCount();

    public abstract int GetCurrentAudioTrack();

    public abstract void SetAudioTrack(int index);

    public abstract string GetCurrentAudioTrackId();

    public abstract int GetCurrentAudioTrackBitrate();

    public virtual int GetNumAudioChannels()
    {
      return -1;
    }

    public abstract int GetVideoTrackCount();

    public abstract int GetCurrentVideoTrack();

    public abstract void SetVideoTrack(int index);

    public abstract string GetCurrentVideoTrackId();

    public abstract int GetCurrentVideoTrackBitrate();

    public abstract float GetVideoFrameRate();

    public abstract float GetBufferingProgress();

    public abstract void Update();

    public abstract void Render();

    public abstract void Dispose();

    public ErrorCode GetLastError()
    {
      return this._lastError;
    }

    public string GetPlayerDescription()
    {
      return this._playerDescription;
    }

    public virtual bool PlayerSupportsLinearColorSpace()
    {
      return false;
    }

    public virtual int GetBufferedTimeRangeCount()
    {
      return 0;
    }

    public virtual bool GetBufferedTimeRange(int index, ref float startTimeMs, ref float endTimeMs)
    {
      return false;
    }

    public void SetTextureProperties(FilterMode filterMode = 1, TextureWrapMode wrapMode = 1, int anisoLevel = 0)
    {
      this._defaultTextureFilterMode = filterMode;
      this._defaultTextureWrapMode = wrapMode;
      this._defaultTextureAnisoLevel = anisoLevel;
      this.ApplyTextureProperties(this.GetTexture(0));
    }

    protected virtual void ApplyTextureProperties(Texture texture)
    {
      if (!Object.op_Inequality((Object) texture, (Object) null))
        return;
      texture.set_filterMode(this._defaultTextureFilterMode);
      texture.set_wrapMode(this._defaultTextureWrapMode);
      texture.set_anisoLevel(this._defaultTextureAnisoLevel);
    }

    public virtual void GrabAudio(float[] buffer, int floatCount, int channelCount)
    {
    }

    public virtual bool IsPlaybackStalled()
    {
      return false;
    }

    public bool LoadSubtitlesSRT(string data)
    {
      if (string.IsNullOrEmpty(data))
      {
        this._subtitles = (List<Subtitle>) null;
        this._currentSubtitle = (Subtitle) null;
      }
      else
        this._subtitles = Helper.LoadSubtitlesSRT(data);
      return this._subtitles != null;
    }

    public virtual void UpdateSubtitles()
    {
      if (this._subtitles == null)
        return;
      float currentTimeMs = this.GetCurrentTimeMs();
      int num = 0;
      if (this._currentSubtitle != null && !this._currentSubtitle.IsTime(currentTimeMs))
      {
        if ((double) currentTimeMs > (double) this._currentSubtitle.timeEndMs)
          num = this._currentSubtitle.index + 1;
        this._currentSubtitle = (Subtitle) null;
      }
      if (this._currentSubtitle != null)
        return;
      for (int index = num; index < this._subtitles.Count; ++index)
      {
        if (this._subtitles[index].IsTime(currentTimeMs))
        {
          this._currentSubtitle = this._subtitles[index];
          break;
        }
      }
    }

    public virtual int GetSubtitleIndex()
    {
      int num = -1;
      if (this._currentSubtitle != null)
        num = this._currentSubtitle.index;
      return num;
    }

    public virtual string GetSubtitleText()
    {
      string str = string.Empty;
      if (this._currentSubtitle != null)
        str = this._currentSubtitle.text;
      return str;
    }

    public virtual void OnEnable()
    {
    }
  }
}
