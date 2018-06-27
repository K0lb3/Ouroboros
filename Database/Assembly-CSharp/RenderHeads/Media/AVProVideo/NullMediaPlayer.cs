// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.NullMediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  public sealed class NullMediaPlayer : BaseMediaPlayer
  {
    private float _playbackRate = 1f;
    private int _Width = 256;
    private int _height = 256;
    private const float FrameRate = 10f;
    private bool _isPlaying;
    private bool _isPaused;
    private float _currentTime;
    private float _volume;
    private bool _bLoop;
    private Texture2D _texture;
    private Texture2D _texture_AVPro;
    private Texture2D _texture_AVPro1;
    private float _fakeFlipTime;
    private int _frameCount;

    public override string GetVersion()
    {
      return "0.0.0";
    }

    public override bool OpenVideoFromFile(string path, long offset, string httpHeaderJson)
    {
      this._texture_AVPro = (Texture2D) Resources.Load("AVPro");
      this._texture_AVPro1 = (Texture2D) Resources.Load("AVPro1");
      if (Object.op_Implicit((Object) this._texture_AVPro))
      {
        this._Width = ((Texture) this._texture_AVPro).get_width();
        this._height = ((Texture) this._texture_AVPro).get_height();
      }
      this._texture = this._texture_AVPro;
      this._fakeFlipTime = 0.0f;
      this._frameCount = 0;
      return true;
    }

    public override void CloseVideo()
    {
      this._frameCount = 0;
      Resources.UnloadAsset((Object) this._texture_AVPro);
      Resources.UnloadAsset((Object) this._texture_AVPro1);
    }

    public override void SetLooping(bool bLooping)
    {
      this._bLoop = bLooping;
    }

    public override bool IsLooping()
    {
      return this._bLoop;
    }

    public override bool HasMetaData()
    {
      return true;
    }

    public override bool CanPlay()
    {
      return true;
    }

    public override bool HasAudio()
    {
      return false;
    }

    public override bool HasVideo()
    {
      return false;
    }

    public override void Play()
    {
      this._isPlaying = true;
      this._isPaused = false;
      this._fakeFlipTime = 0.0f;
    }

    public override void Pause()
    {
      this._isPlaying = false;
      this._isPaused = true;
    }

    public override void Stop()
    {
      this._isPlaying = false;
      this._isPaused = false;
    }

    public override bool IsSeeking()
    {
      return false;
    }

    public override bool IsPlaying()
    {
      return this._isPlaying;
    }

    public override bool IsPaused()
    {
      return this._isPaused;
    }

    public override bool IsFinished()
    {
      if (this._isPlaying)
        return (double) this._currentTime >= (double) this.GetDurationMs();
      return false;
    }

    public override bool IsBuffering()
    {
      return false;
    }

    public override float GetDurationMs()
    {
      return 10000f;
    }

    public override int GetVideoWidth()
    {
      return this._Width;
    }

    public override int GetVideoHeight()
    {
      return this._height;
    }

    public override float GetVideoDisplayRate()
    {
      return 10f;
    }

    public override Texture GetTexture(int index)
    {
      return (Texture) this._texture;
    }

    public override int GetTextureFrameCount()
    {
      return this._frameCount;
    }

    public override bool RequiresVerticalFlip()
    {
      return false;
    }

    public override void Rewind()
    {
      this.Seek(0.0f);
    }

    public override void Seek(float timeMs)
    {
      this._currentTime = timeMs;
    }

    public override void SeekFast(float timeMs)
    {
      this._currentTime = timeMs;
    }

    public override float GetCurrentTimeMs()
    {
      return this._currentTime;
    }

    public override void SetPlaybackRate(float rate)
    {
      this._playbackRate = rate;
    }

    public override float GetPlaybackRate()
    {
      return this._playbackRate;
    }

    public override float GetBufferingProgress()
    {
      return 0.0f;
    }

    public override void MuteAudio(bool bMuted)
    {
    }

    public override bool IsMuted()
    {
      return true;
    }

    public override void SetVolume(float volume)
    {
      this._volume = volume;
    }

    public override float GetVolume()
    {
      return this._volume;
    }

    public override int GetAudioTrackCount()
    {
      return 0;
    }

    public override int GetCurrentAudioTrack()
    {
      return 0;
    }

    public override void SetAudioTrack(int index)
    {
    }

    public override int GetVideoTrackCount()
    {
      return 0;
    }

    public override int GetCurrentVideoTrack()
    {
      return 0;
    }

    public override string GetCurrentAudioTrackId()
    {
      return string.Empty;
    }

    public override int GetCurrentAudioTrackBitrate()
    {
      return 0;
    }

    public override void SetVideoTrack(int index)
    {
    }

    public override string GetCurrentVideoTrackId()
    {
      return string.Empty;
    }

    public override int GetCurrentVideoTrackBitrate()
    {
      return 0;
    }

    public override float GetVideoFrameRate()
    {
      return 0.0f;
    }

    public override void Update()
    {
      this.UpdateSubtitles();
      if (!this._isPlaying)
        return;
      this._currentTime += Time.get_deltaTime() * 1000f;
      if ((double) this._currentTime >= (double) this.GetDurationMs())
      {
        this._currentTime = this.GetDurationMs();
        if (this._bLoop)
          this.Rewind();
      }
      this._fakeFlipTime += Time.get_deltaTime();
      if ((double) this._fakeFlipTime < 0.1)
        return;
      this._fakeFlipTime = 0.0f;
      this._texture = !Object.op_Equality((Object) this._texture, (Object) this._texture_AVPro) ? this._texture_AVPro : this._texture_AVPro1;
      ++this._frameCount;
    }

    public override void Render()
    {
    }

    public override void Dispose()
    {
    }
  }
}
