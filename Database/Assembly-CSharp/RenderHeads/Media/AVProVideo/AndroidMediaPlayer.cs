// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.AndroidMediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  public class AndroidMediaPlayer : BaseMediaPlayer
  {
    private static string s_Version = "Plug-in not yet initialised";
    protected int m_iPlayerIndex = -1;
    protected static AndroidJavaObject s_ActivityContext;
    protected static bool s_bInitialised;
    private static IntPtr _nativeFunction_RenderEvent;
    protected AndroidJavaObject m_Video;
    private Texture2D m_Texture;
    private int m_TextureHandle;
    private bool m_UseFastOesPath;
    private float m_DurationMs;
    private int m_Width;
    private int m_Height;

    public AndroidMediaPlayer(bool useFastOesPath, bool showPosterFrame)
    {
      this.m_Video = new AndroidJavaObject("com.RenderHeads.AVProVideo.AVProMobileVideo", new object[0]);
      if (this.m_Video == null)
        return;
      this.m_Video.Call("Initialise", new object[1]
      {
        (object) AndroidMediaPlayer.s_ActivityContext
      });
      this.m_iPlayerIndex = (int) this.m_Video.Call<int>("GetPlayerIndex", new object[0]);
      this.SetOptions(useFastOesPath, showPosterFrame);
      AndroidMediaPlayer.IssuePluginEvent(AndroidMediaPlayer.Native.AVPPluginEvent.PlayerSetup, this.m_iPlayerIndex);
    }

    public static void InitialisePlatform()
    {
      if (AndroidMediaPlayer.s_ActivityContext == null)
      {
        AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        if (androidJavaClass != null)
          AndroidMediaPlayer.s_ActivityContext = (AndroidJavaObject) ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity");
      }
      if (AndroidMediaPlayer.s_bInitialised)
        return;
      AndroidMediaPlayer.s_bInitialised = true;
      AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.RenderHeads.AVProVideo.AVProMobileVideo", new object[0]);
      if (androidJavaObject == null)
        return;
      AndroidMediaPlayer.s_Version = (string) androidJavaObject.CallStatic<string>("GetPluginVersion", new object[0]);
      AndroidMediaPlayer._nativeFunction_RenderEvent = AndroidMediaPlayer.Native.GetRenderEventFunc();
    }

    private static void IssuePluginEvent(AndroidMediaPlayer.Native.AVPPluginEvent type, int param)
    {
      int num = 1566228480 | (int) type << 8;
      switch (type)
      {
        case AndroidMediaPlayer.Native.AVPPluginEvent.PlayerSetup:
        case AndroidMediaPlayer.Native.AVPPluginEvent.PlayerUpdate:
        case AndroidMediaPlayer.Native.AVPPluginEvent.PlayerDestroy:
          num |= param & (int) byte.MaxValue;
          break;
      }
      GL.IssuePluginEvent(AndroidMediaPlayer._nativeFunction_RenderEvent, num);
    }

    public void SetOptions(bool useFastOesPath, bool showPosterFrame)
    {
      this.m_UseFastOesPath = useFastOesPath;
      if (this.m_Video == null)
        return;
      this.m_Video.Call("SetPlayerOptions", new object[2]
      {
        (object) this.m_UseFastOesPath,
        (object) showPosterFrame
      });
    }

    public override string GetVersion()
    {
      return AndroidMediaPlayer.s_Version;
    }

    public override bool OpenVideoFromFile(string path, long offset, string httpHeaderJson)
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (OpenVideoFromFile), new object[3]
        {
          (object) path,
          (object) offset,
          (object) httpHeaderJson
        });
      return flag;
    }

    public override void CloseVideo()
    {
      if (Object.op_Inequality((Object) this.m_Texture, (Object) null))
      {
        Object.Destroy((Object) this.m_Texture);
        this.m_Texture = (Texture2D) null;
      }
      this.m_TextureHandle = 0;
      this.m_DurationMs = 0.0f;
      this.m_Width = 0;
      this.m_Height = 0;
      this._lastError = ErrorCode.None;
      this.m_Video.Call(nameof (CloseVideo), new object[0]);
    }

    public override void SetLooping(bool bLooping)
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (SetLooping), new object[1]
      {
        (object) bLooping
      });
    }

    public override bool IsLooping()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (IsLooping), new object[0]);
      return flag;
    }

    public override bool HasVideo()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (HasVideo), new object[0]);
      return flag;
    }

    public override bool HasAudio()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (HasAudio), new object[0]);
      return flag;
    }

    public override bool HasMetaData()
    {
      bool flag = false;
      if ((double) this.m_DurationMs > 0.0)
      {
        flag = true;
        if (this.HasVideo())
          flag = this.m_Width > 0 && this.m_Height > 0;
      }
      return flag;
    }

    public override bool CanPlay()
    {
      return AndroidMediaPlayer.Native._CanPlay(this.m_iPlayerIndex);
    }

    public override void Play()
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (Play), new object[0]);
    }

    public override void Pause()
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (Pause), new object[0]);
    }

    public override void Stop()
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call("Pause", new object[0]);
    }

    public override void Rewind()
    {
      this.Seek(0.0f);
    }

    public override void Seek(float timeMs)
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (Seek), new object[1]
      {
        (object) Mathf.FloorToInt(timeMs)
      });
    }

    public override void SeekFast(float timeMs)
    {
      this.Seek(timeMs);
    }

    public override float GetCurrentTimeMs()
    {
      float num = 0.0f;
      if (this.m_Video != null)
        num = (float) this.m_Video.Call<long>(nameof (GetCurrentTimeMs), new object[0]);
      return num;
    }

    public override void SetPlaybackRate(float rate)
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (SetPlaybackRate), new object[1]
      {
        (object) rate
      });
    }

    public override float GetPlaybackRate()
    {
      float num = 0.0f;
      if (this.m_Video != null)
        num = (float) this.m_Video.Call<float>(nameof (GetPlaybackRate), new object[0]);
      return num;
    }

    public override float GetDurationMs()
    {
      return this.m_DurationMs;
    }

    public override int GetVideoWidth()
    {
      return this.m_Width;
    }

    public override int GetVideoHeight()
    {
      return this.m_Height;
    }

    public override float GetVideoFrameRate()
    {
      float num = 0.0f;
      if (this.m_Video != null)
        num = (float) this.m_Video.Call<float>("GetSourceVideoFrameRate", new object[0]);
      return num;
    }

    public override float GetBufferingProgress()
    {
      float num = 0.0f;
      if (this.m_Video != null)
        num = (float) (this.m_Video.Call<float>("GetBufferingProgressPercent", new object[0]) * 0.00999999977648258);
      return num;
    }

    public override float GetVideoDisplayRate()
    {
      return AndroidMediaPlayer.Native._GetVideoDisplayRate(this.m_iPlayerIndex);
    }

    public override bool IsSeeking()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (IsSeeking), new object[0]);
      return flag;
    }

    public override bool IsPlaying()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (IsPlaying), new object[0]);
      return flag;
    }

    public override bool IsPaused()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (IsPaused), new object[0]);
      return flag;
    }

    public override bool IsFinished()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (IsFinished), new object[0]);
      return flag;
    }

    public override bool IsBuffering()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (IsBuffering), new object[0]);
      return flag;
    }

    public override Texture GetTexture(int index)
    {
      Texture texture = (Texture) null;
      if (this.GetTextureFrameCount() > 0)
        texture = (Texture) this.m_Texture;
      return texture;
    }

    public override int GetTextureFrameCount()
    {
      return AndroidMediaPlayer.Native._GetFrameCount(this.m_iPlayerIndex);
    }

    public override bool RequiresVerticalFlip()
    {
      return false;
    }

    public override void MuteAudio(bool bMuted)
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (MuteAudio), new object[1]
      {
        (object) bMuted
      });
    }

    public override bool IsMuted()
    {
      bool flag = false;
      if (this.m_Video != null)
        flag = (bool) this.m_Video.Call<bool>(nameof (IsMuted), new object[0]);
      return flag;
    }

    public override void SetVolume(float volume)
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (SetVolume), new object[1]
      {
        (object) volume
      });
    }

    public override float GetVolume()
    {
      float num = 0.0f;
      if (this.m_Video != null)
        num = (float) this.m_Video.Call<float>(nameof (GetVolume), new object[0]);
      return num;
    }

    public override void SetBalance(float balance)
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call("SetAudioPan", new object[1]
      {
        (object) balance
      });
    }

    public override float GetBalance()
    {
      float num = 0.0f;
      if (this.m_Video != null)
        num = (float) this.m_Video.Call<float>("GetAudioPan", new object[0]);
      return num;
    }

    public override int GetAudioTrackCount()
    {
      int num = 0;
      if (this.m_Video != null)
        num = (int) this.m_Video.Call<int>("GetNumberAudioTracks", new object[0]);
      return num;
    }

    public override int GetCurrentAudioTrack()
    {
      int num = 0;
      if (this.m_Video != null)
        num = (int) this.m_Video.Call<int>("GetCurrentAudioTrackIndex", new object[0]);
      return num;
    }

    public override void SetAudioTrack(int index)
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (SetAudioTrack), new object[1]
      {
        (object) index
      });
    }

    public override string GetCurrentAudioTrackId()
    {
      string str = string.Empty;
      if (this.m_Video != null)
        str = (string) this.m_Video.Call<string>(nameof (GetCurrentAudioTrackId), new object[0]);
      return str;
    }

    public override int GetCurrentAudioTrackBitrate()
    {
      int num = 0;
      if (this.m_Video != null)
        num = (int) this.m_Video.Call<int>("GetCurrentAudioTrackIndex", new object[0]);
      return num;
    }

    public override int GetVideoTrackCount()
    {
      int num = 0;
      if (this.m_Video != null)
        num = (int) this.m_Video.Call<int>("GetNumberVideoTracks", new object[0]);
      return num;
    }

    public override int GetCurrentVideoTrack()
    {
      int num = 0;
      if (this.m_Video != null)
        num = (int) this.m_Video.Call<int>("GetCurrentVideoTrackIndex", new object[0]);
      return num;
    }

    public override void SetVideoTrack(int index)
    {
      if (this.m_Video == null)
        return;
      this.m_Video.Call(nameof (SetVideoTrack), new object[1]
      {
        (object) index
      });
    }

    public override string GetCurrentVideoTrackId()
    {
      string str = string.Empty;
      if (this.m_Video != null)
        str = (string) this.m_Video.Call<string>(nameof (GetCurrentVideoTrackId), new object[0]);
      return str;
    }

    public override int GetCurrentVideoTrackBitrate()
    {
      int num = 0;
      if (this.m_Video != null)
        num = (int) this.m_Video.Call<int>(nameof (GetCurrentVideoTrackBitrate), new object[0]);
      return num;
    }

    public override long GetTextureTimeStamp()
    {
      long num = long.MinValue;
      if (this.m_Video != null)
        num = (long) this.m_Video.Call<long>(nameof (GetTextureTimeStamp), new object[0]);
      return num;
    }

    public override void Render()
    {
      if (this.m_Video == null)
        return;
      if (this.m_UseFastOesPath)
        GL.InvalidateState();
      AndroidMediaPlayer.IssuePluginEvent(AndroidMediaPlayer.Native.AVPPluginEvent.PlayerUpdate, this.m_iPlayerIndex);
      if (this.m_UseFastOesPath)
        GL.InvalidateState();
      int num1 = -1;
      int num2 = -1;
      if (Object.op_Inequality((Object) this.m_Texture, (Object) null))
      {
        num1 = AndroidMediaPlayer.Native._GetWidth(this.m_iPlayerIndex);
        num2 = AndroidMediaPlayer.Native._GetHeight(this.m_iPlayerIndex);
        if (num1 != this.m_Width || num2 != this.m_Height)
        {
          this.m_Texture = (Texture2D) null;
          this.m_TextureHandle = 0;
        }
      }
      int textureHandle = AndroidMediaPlayer.Native._GetTextureHandle(this.m_iPlayerIndex);
      if (textureHandle <= 0 || textureHandle == this.m_TextureHandle)
        return;
      if (num1 == -1 || num2 == -1)
      {
        num1 = AndroidMediaPlayer.Native._GetWidth(this.m_iPlayerIndex);
        num2 = AndroidMediaPlayer.Native._GetHeight(this.m_iPlayerIndex);
      }
      if (Mathf.Max(num1, num2) > SystemInfo.get_maxTextureSize())
      {
        this.m_Width = num1;
        this.m_Height = num2;
        this.m_TextureHandle = textureHandle;
        Debug.LogError((object) "[AVProVideo] Video dimensions larger than maxTextureSize");
      }
      else
      {
        if (num1 <= 0 || num2 <= 0)
          return;
        this.m_Width = num1;
        this.m_Height = num2;
        this.m_TextureHandle = textureHandle;
        this._playerDescription = "MediaPlayer";
        this.m_Texture = Texture2D.CreateExternalTexture(this.m_Width, this.m_Height, (TextureFormat) 4, false, false, new IntPtr(textureHandle));
        if (!Object.op_Inequality((Object) this.m_Texture, (Object) null))
          return;
        this.ApplyTextureProperties((Texture) this.m_Texture);
      }
    }

    protected override void ApplyTextureProperties(Texture texture)
    {
      if (this.m_UseFastOesPath)
        return;
      base.ApplyTextureProperties(texture);
    }

    public override void OnEnable()
    {
      base.OnEnable();
      int textureHandle = AndroidMediaPlayer.Native._GetTextureHandle(this.m_iPlayerIndex);
      if (!Object.op_Inequality((Object) this.m_Texture, (Object) null) || textureHandle <= 0 || !(((Texture) this.m_Texture).GetNativeTexturePtr() == IntPtr.Zero))
        return;
      this.m_Texture.UpdateExternalTexture(new IntPtr(textureHandle));
    }

    public override void Update()
    {
      if (this.m_Video != null)
        this._lastError = (ErrorCode) AndroidMediaPlayer.Native._GetLastErrorCode(this.m_iPlayerIndex);
      this.UpdateSubtitles();
      if (!Mathf.Approximately(this.m_DurationMs, 0.0f))
        return;
      this.m_DurationMs = (float) AndroidMediaPlayer.Native._GetDuration(this.m_iPlayerIndex);
    }

    public override bool PlayerSupportsLinearColorSpace()
    {
      return false;
    }

    public override void Dispose()
    {
      AndroidMediaPlayer.IssuePluginEvent(AndroidMediaPlayer.Native.AVPPluginEvent.PlayerDestroy, this.m_iPlayerIndex);
      if (this.m_Video != null)
      {
        this.m_Video.Call("SetDeinitialiseFlagged", new object[0]);
        this.m_Video.Dispose();
        this.m_Video = (AndroidJavaObject) null;
      }
      if (!Object.op_Inequality((Object) this.m_Texture, (Object) null))
        return;
      Object.Destroy((Object) this.m_Texture);
      this.m_Texture = (Texture2D) null;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Native
    {
      [DllImport("AVProLocal")]
      public static extern IntPtr GetRenderEventFunc();

      [DllImport("AVProLocal")]
      public static extern int _GetWidth(int iPlayerIndex);

      [DllImport("AVProLocal")]
      public static extern int _GetHeight(int iPlayerIndex);

      [DllImport("AVProLocal")]
      public static extern int _GetTextureHandle(int iPlayerIndex);

      [DllImport("AVProLocal")]
      public static extern long _GetDuration(int iPlayerIndex);

      [DllImport("AVProLocal")]
      public static extern int _GetLastErrorCode(int iPlayerIndex);

      [DllImport("AVProLocal")]
      public static extern int _GetFrameCount(int iPlayerIndex);

      [DllImport("AVProLocal")]
      public static extern float _GetVideoDisplayRate(int iPlayerIndex);

      [DllImport("AVProLocal")]
      public static extern bool _CanPlay(int iPlayerIndex);

      public enum AVPPluginEvent
      {
        Nop,
        PlayerSetup,
        PlayerUpdate,
        PlayerDestroy,
      }
    }
  }
}
