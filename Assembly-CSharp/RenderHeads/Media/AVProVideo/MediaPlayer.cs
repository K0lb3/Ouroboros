// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.MediaPlayer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
  [AddComponentMenu("AVPro Video/Media Player", -100)]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  public class MediaPlayer : MonoBehaviour
  {
    private const int s_GuiDepth = -1000;
    private const float s_GuiScale = 1.5f;
    private const int s_GuiStartWidth = 10;
    private const int s_GuiWidth = 180;
    public MediaPlayer.FileLocation m_VideoLocation;
    public string m_VideoPath;
    public bool m_AutoOpen;
    public bool m_AutoStart;
    public bool m_Loop;
    [Range(0.0f, 1f)]
    public float m_Volume;
    [SerializeField]
    [Range(-1f, 1f)]
    private float m_Balance;
    public bool m_Muted;
    [SerializeField]
    [Range(-4f, 4f)]
    public float m_PlaybackRate;
    [SerializeField]
    private bool m_DebugGui;
    [SerializeField]
    private bool m_DebugGuiControls;
    [SerializeField]
    private bool m_Persistent;
    public StereoPacking m_StereoPacking;
    public AlphaPacking m_AlphaPacking;
    public bool m_DisplayDebugStereoColorTint;
    public FilterMode m_FilterMode;
    public TextureWrapMode m_WrapMode;
    [Range(0.0f, 16f)]
    public int m_AnisoLevel;
    [SerializeField]
    private bool m_LoadSubtitles;
    [SerializeField]
    private MediaPlayer.FileLocation m_SubtitleLocation;
    private MediaPlayer.FileLocation m_queueSubtitleLocation;
    [SerializeField]
    private string m_SubtitlePath;
    private string m_queueSubtitlePath;
    private Coroutine m_loadSubtitlesRoutine;
    [SerializeField]
    private MediaPlayerEvent m_events;
    private IMediaControl m_Control;
    private IMediaProducer m_Texture;
    private IMediaInfo m_Info;
    private IMediaPlayer m_Player;
    private IMediaSubtitles m_Subtitles;
    private IDisposable m_Dispose;
    private bool m_VideoOpened;
    private bool m_AutoStartTriggered;
    private bool m_WasPlayingOnPause;
    private Coroutine _renderingCoroutine;
    private static bool s_GlobalStartup;
    private bool m_EventFired_ReadyToPlay;
    private bool m_EventFired_Started;
    private bool m_EventFired_FirstFrameReady;
    private bool m_EventFired_FinishedPlaying;
    private bool m_EventFired_MetaDataReady;
    private int m_previousSubtitleIndex;
    private bool m_isPlaybackStalled;
    [SerializeField]
    private MediaPlayer.OptionsWindows _optionsWindows;
    [SerializeField]
    private MediaPlayer.OptionsMacOSX _optionsMacOSX;
    [SerializeField]
    private MediaPlayer.OptionsIOS _optionsIOS;
    [SerializeField]
    private MediaPlayer.OptionsTVOS _optionsTVOS;
    [SerializeField]
    private MediaPlayer.OptionsAndroid _optionsAndroid;
    [SerializeField]
    private MediaPlayer.OptionsWindowsPhone _optionsWindowsPhone;
    [SerializeField]
    private MediaPlayer.OptionsWindowsUWP _optionsWindowsUWP;
    [SerializeField]
    private MediaPlayer.OptionsWebGL _optionsWebGL;

    public MediaPlayer()
    {
      base.\u002Ector();
    }

    public bool DisplayDebugGUI
    {
      get
      {
        return this.m_DebugGui;
      }
      set
      {
        this.m_DebugGui = value;
      }
    }

    public bool DisplayDebugGUIControls
    {
      get
      {
        return this.m_DebugGuiControls;
      }
      set
      {
        this.m_DebugGuiControls = value;
      }
    }

    public bool Persistent
    {
      get
      {
        return this.m_Persistent;
      }
      set
      {
        this.m_Persistent = value;
      }
    }

    public IMediaInfo Info
    {
      get
      {
        return this.m_Info;
      }
    }

    public IMediaControl Control
    {
      get
      {
        return this.m_Control;
      }
    }

    public IMediaPlayer Player
    {
      get
      {
        return this.m_Player;
      }
    }

    public virtual IMediaProducer TextureProducer
    {
      get
      {
        return this.m_Texture;
      }
    }

    public virtual IMediaSubtitles Subtitles
    {
      get
      {
        return this.m_Subtitles;
      }
    }

    public MediaPlayerEvent Events
    {
      get
      {
        if (this.m_events == null)
          this.m_events = new MediaPlayerEvent();
        return this.m_events;
      }
    }

    public bool VideoOpened
    {
      get
      {
        return this.m_VideoOpened;
      }
    }

    public MediaPlayer.OptionsWindows PlatformOptionsWindows
    {
      get
      {
        return this._optionsWindows;
      }
    }

    public MediaPlayer.OptionsMacOSX PlatformOptionsMacOSX
    {
      get
      {
        return this._optionsMacOSX;
      }
    }

    public MediaPlayer.OptionsIOS PlatformOptionsIOS
    {
      get
      {
        return this._optionsIOS;
      }
    }

    public MediaPlayer.OptionsTVOS PlatformOptionsTVOS
    {
      get
      {
        return this._optionsTVOS;
      }
    }

    public MediaPlayer.OptionsAndroid PlatformOptionsAndroid
    {
      get
      {
        return this._optionsAndroid;
      }
    }

    public MediaPlayer.OptionsWindowsPhone PlatformOptionsWindowsPhone
    {
      get
      {
        return this._optionsWindowsPhone;
      }
    }

    public MediaPlayer.OptionsWindowsUWP PlatformOptionsWindowsUWP
    {
      get
      {
        return this._optionsWindowsUWP;
      }
    }

    public MediaPlayer.OptionsWebGL PlatformOptionsWebGL
    {
      get
      {
        return this._optionsWebGL;
      }
    }

    private void Awake()
    {
      if (!this.m_Persistent)
        return;
      Object.DontDestroyOnLoad((Object) ((Component) this).get_gameObject());
    }

    protected void Initialise()
    {
      BaseMediaPlayer platformMediaPlayer = this.CreatePlatformMediaPlayer();
      if (platformMediaPlayer == null)
        return;
      this.m_Control = (IMediaControl) platformMediaPlayer;
      this.m_Texture = (IMediaProducer) platformMediaPlayer;
      this.m_Info = (IMediaInfo) platformMediaPlayer;
      this.m_Player = (IMediaPlayer) platformMediaPlayer;
      this.m_Subtitles = (IMediaSubtitles) platformMediaPlayer;
      this.m_Dispose = (IDisposable) platformMediaPlayer;
      if (MediaPlayer.s_GlobalStartup)
        return;
      MediaPlayer.s_GlobalStartup = true;
    }

    private void Start()
    {
      if (this.m_Control == null)
        this.Initialise();
      if (this.m_Control == null)
        return;
      if (this.m_AutoOpen)
      {
        this.OpenVideoFromFile();
        if (this.m_LoadSubtitles && this.m_Subtitles != null && !string.IsNullOrEmpty(this.m_SubtitlePath))
          this.EnableSubtitles(this.m_SubtitleLocation, this.m_SubtitlePath);
      }
      this.StartRenderCoroutine();
    }

    public bool OpenVideoFromFile(MediaPlayer.FileLocation location, string path, bool autoPlay = true)
    {
      this.m_VideoLocation = location;
      this.m_VideoPath = path;
      this.m_AutoStart = autoPlay;
      if (this.m_Control == null)
        this.Initialise();
      return this.OpenVideoFromFile();
    }

    public bool OpenVideoFromBuffer(byte[] buffer, bool autoPlay = true)
    {
      this.m_VideoLocation = MediaPlayer.FileLocation.AbsolutePathOrURL;
      this.m_VideoPath = nameof (buffer);
      this.m_AutoStart = autoPlay;
      if (this.m_Control == null)
        this.Initialise();
      return this.OpenVideoFromBufferInternal(buffer);
    }

    public bool SubtitlesEnabled
    {
      get
      {
        return this.m_LoadSubtitles;
      }
    }

    public string SubtitlePath
    {
      get
      {
        return this.m_SubtitlePath;
      }
    }

    public MediaPlayer.FileLocation SubtitleLocation
    {
      get
      {
        return this.m_SubtitleLocation;
      }
    }

    public bool EnableSubtitles(MediaPlayer.FileLocation fileLocation, string filePath)
    {
      bool flag1 = false;
      if (this.m_Subtitles != null)
      {
        if (!string.IsNullOrEmpty(filePath))
        {
          string platformFilePath = this.GetPlatformFilePath(MediaPlayer.GetPlatform(), ref filePath, ref fileLocation);
          bool flag2 = true;
          if (platformFilePath.Contains("://"))
            flag2 = false;
          if (false && !File.Exists(platformFilePath))
          {
            Debug.LogError((object) ("[AVProVideo] Subtitle file not found: " + platformFilePath), (Object) this);
          }
          else
          {
            this.m_previousSubtitleIndex = -1;
            try
            {
              if (platformFilePath.Contains("://"))
              {
                if (this.m_loadSubtitlesRoutine != null)
                {
                  this.StopCoroutine(this.m_loadSubtitlesRoutine);
                  this.m_loadSubtitlesRoutine = (Coroutine) null;
                }
                this.m_loadSubtitlesRoutine = this.StartCoroutine(this.LoadSubtitlesCoroutine(platformFilePath, fileLocation, filePath));
              }
              else if (this.m_Subtitles.LoadSubtitlesSRT(File.ReadAllText(platformFilePath)))
              {
                this.m_SubtitleLocation = fileLocation;
                this.m_SubtitlePath = filePath;
                this.m_LoadSubtitles = false;
                flag1 = true;
              }
              else
                Debug.LogError((object) ("[AVProVideo] Failed to load subtitles" + platformFilePath), (Object) this);
            }
            catch (Exception ex)
            {
              Debug.LogError((object) ("[AVProVideo] Failed to load subtitles " + platformFilePath), (Object) this);
              Debug.LogException(ex, (Object) this);
            }
          }
        }
        else
          Debug.LogError((object) "[AVProVideo] No subtitle file path specified", (Object) this);
      }
      else
      {
        this.m_queueSubtitleLocation = fileLocation;
        this.m_queueSubtitlePath = filePath;
      }
      return flag1;
    }

    [DebuggerHidden]
    private IEnumerator LoadSubtitlesCoroutine(string url, MediaPlayer.FileLocation fileLocation, string filePath)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MediaPlayer.\u003CLoadSubtitlesCoroutine\u003Ec__Iterator0() { url = url, fileLocation = fileLocation, filePath = filePath, \u003C\u0024\u003Eurl = url, \u003C\u0024\u003EfileLocation = fileLocation, \u003C\u0024\u003EfilePath = filePath, \u003C\u003Ef__this = this };
    }

    public void DisableSubtitles()
    {
      if (this.m_loadSubtitlesRoutine != null)
      {
        this.StopCoroutine(this.m_loadSubtitlesRoutine);
        this.m_loadSubtitlesRoutine = (Coroutine) null;
      }
      if (this.m_Subtitles != null)
      {
        this.m_previousSubtitleIndex = -1;
        this.m_LoadSubtitles = false;
        this.m_Subtitles.LoadSubtitlesSRT(string.Empty);
      }
      else
        this.m_queueSubtitlePath = string.Empty;
    }

    private bool OpenVideoFromBufferInternal(byte[] buffer)
    {
      bool flag = false;
      if (this.m_Control != null)
      {
        this.CloseVideo();
        this.m_VideoOpened = true;
        this.m_AutoStartTriggered = !this.m_AutoStart;
        this.m_EventFired_MetaDataReady = false;
        this.m_EventFired_ReadyToPlay = false;
        this.m_EventFired_Started = false;
        this.m_EventFired_FirstFrameReady = false;
        this.m_previousSubtitleIndex = -1;
        if (!this.m_Control.OpenVideoFromBuffer(buffer))
        {
          Debug.LogError((object) "[AVProVideo] Failed to open buffer", (Object) this);
        }
        else
        {
          this.SetPlaybackOptions();
          flag = true;
          this.StartRenderCoroutine();
        }
      }
      return flag;
    }

    private bool OpenVideoFromFile()
    {
      bool flag1 = false;
      if (this.m_Control != null)
      {
        this.CloseVideo();
        this.m_VideoOpened = true;
        this.m_AutoStartTriggered = !this.m_AutoStart;
        this.m_EventFired_MetaDataReady = false;
        this.m_EventFired_ReadyToPlay = false;
        this.m_EventFired_Started = false;
        this.m_EventFired_FirstFrameReady = false;
        this.m_previousSubtitleIndex = -1;
        long platformFileOffset = this.GetPlatformFileOffset();
        string platformFilePath = this.GetPlatformFilePath(MediaPlayer.GetPlatform(), ref this.m_VideoPath, ref this.m_VideoLocation);
        if (!string.IsNullOrEmpty(this.m_VideoPath))
        {
          string httpHeaderJson = (string) null;
          bool flag2 = true;
          if (platformFilePath.Contains("://"))
          {
            flag2 = false;
            httpHeaderJson = this.GetPlatformHttpHeaderJson();
          }
          if (false && !File.Exists(platformFilePath))
            Debug.LogError((object) ("[AVProVideo] File not found: " + platformFilePath), (Object) this);
          else if (!this.m_Control.OpenVideoFromFile(platformFilePath, platformFileOffset, httpHeaderJson))
          {
            Debug.LogError((object) ("[AVProVideo] Failed to open " + platformFilePath), (Object) this);
          }
          else
          {
            this.SetPlaybackOptions();
            flag1 = true;
            this.StartRenderCoroutine();
          }
        }
        else
          Debug.LogError((object) "[AVProVideo] No file path specified", (Object) this);
      }
      return flag1;
    }

    private void SetPlaybackOptions()
    {
      if (this.m_Control == null)
        return;
      this.m_Control.SetLooping(this.m_Loop);
      this.m_Control.SetVolume(this.m_Volume);
      this.m_Control.SetBalance(this.m_Balance);
      this.m_Control.SetPlaybackRate(this.m_PlaybackRate);
      this.m_Control.MuteAudio(this.m_Muted);
      this.m_Control.SetTextureProperties(this.m_FilterMode, this.m_WrapMode, this.m_AnisoLevel);
    }

    public void CloseVideo()
    {
      if (this.m_Control != null)
      {
        if (this.m_events != null)
          this.m_events.Invoke(this, MediaPlayerEvent.EventType.Closing, ErrorCode.None);
        this.m_AutoStartTriggered = false;
        this.m_VideoOpened = false;
        this.m_EventFired_ReadyToPlay = false;
        this.m_EventFired_Started = false;
        this.m_EventFired_MetaDataReady = false;
        this.m_EventFired_FirstFrameReady = false;
        if (this.m_loadSubtitlesRoutine != null)
        {
          this.StopCoroutine(this.m_loadSubtitlesRoutine);
          this.m_loadSubtitlesRoutine = (Coroutine) null;
        }
        this.m_previousSubtitleIndex = -1;
        this.m_isPlaybackStalled = false;
        this.m_Control.CloseVideo();
      }
      this.StopRenderCoroutine();
    }

    public void Play()
    {
      if (this.m_Control != null && this.m_Control.CanPlay())
      {
        this.m_Control.Play();
        this.m_EventFired_ReadyToPlay = true;
      }
      else
        this.m_AutoStart = true;
    }

    public void Pause()
    {
      if (this.m_Control == null || !this.m_Control.IsPlaying())
        return;
      this.m_Control.Pause();
    }

    public void Stop()
    {
      if (this.m_Control == null)
        return;
      this.m_Control.Stop();
    }

    public void Rewind(bool pause)
    {
      if (this.m_Control == null)
        return;
      if (pause)
        this.Pause();
      this.m_Control.Rewind();
    }

    private void Update()
    {
      if (this.m_Control == null)
        return;
      if (this.m_VideoOpened && this.m_AutoStart && (!this.m_AutoStartTriggered && this.m_Control.CanPlay()))
      {
        this.m_AutoStartTriggered = true;
        this.Play();
      }
      if (this._renderingCoroutine == null && this.m_Control.CanPlay())
        this.StartRenderCoroutine();
      if (this.m_Subtitles != null && !string.IsNullOrEmpty(this.m_queueSubtitlePath))
      {
        this.EnableSubtitles(this.m_queueSubtitleLocation, this.m_queueSubtitlePath);
        this.m_queueSubtitlePath = string.Empty;
      }
      this.m_Player.Update();
      this.UpdateErrors();
      this.UpdateEvents();
    }

    private void OnEnable()
    {
      if (this.m_Control != null && this.m_WasPlayingOnPause)
      {
        this.m_AutoStart = true;
        this.m_AutoStartTriggered = false;
        this.m_WasPlayingOnPause = false;
      }
      if (this.m_Player != null)
        this.m_Player.OnEnable();
      this.StartRenderCoroutine();
    }

    private void OnDisable()
    {
      if (this.m_Control != null && this.m_Control.IsPlaying())
      {
        this.m_WasPlayingOnPause = true;
        this.Pause();
      }
      this.StopRenderCoroutine();
    }

    private void OnDestroy()
    {
      this.CloseVideo();
      if (this.m_Dispose != null)
      {
        this.m_Dispose.Dispose();
        this.m_Dispose = (IDisposable) null;
      }
      this.m_Control = (IMediaControl) null;
      this.m_Texture = (IMediaProducer) null;
      this.m_Info = (IMediaInfo) null;
      this.m_Player = (IMediaPlayer) null;
    }

    private void OnApplicationQuit()
    {
      if (!MediaPlayer.s_GlobalStartup)
        return;
      MediaPlayer[] objectsOfTypeAll = (MediaPlayer[]) Resources.FindObjectsOfTypeAll<MediaPlayer>();
      if (objectsOfTypeAll != null && objectsOfTypeAll.Length > 0)
      {
        for (int index = 0; index < objectsOfTypeAll.Length; ++index)
          objectsOfTypeAll[index].CloseVideo();
      }
      MediaPlayer.s_GlobalStartup = false;
    }

    private void StartRenderCoroutine()
    {
      if (this._renderingCoroutine != null)
        return;
      this._renderingCoroutine = this.StartCoroutine(this.FinalRenderCapture());
    }

    private void StopRenderCoroutine()
    {
      if (this._renderingCoroutine == null)
        return;
      this.StopCoroutine(this._renderingCoroutine);
      this._renderingCoroutine = (Coroutine) null;
    }

    [DebuggerHidden]
    private IEnumerator FinalRenderCapture()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MediaPlayer.\u003CFinalRenderCapture\u003Ec__Iterator1() { \u003C\u003Ef__this = this };
    }

    public static Platform GetPlatform()
    {
      return Platform.Android;
    }

    public MediaPlayer.PlatformOptions GetCurrentPlatformOptions()
    {
      return (MediaPlayer.PlatformOptions) this._optionsAndroid;
    }

    public static string GetPath(MediaPlayer.FileLocation location)
    {
      string str = string.Empty;
      switch (location)
      {
        case MediaPlayer.FileLocation.RelativeToProjectFolder:
          str = Path.GetFullPath(Path.Combine(Application.get_dataPath(), "..")).Replace('\\', '/');
          break;
        case MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder:
          str = Application.get_streamingAssetsPath();
          break;
        case MediaPlayer.FileLocation.RelativeToDataFolder:
          str = Application.get_dataPath();
          break;
        case MediaPlayer.FileLocation.RelativeToPeristentDataFolder:
          str = Application.get_persistentDataPath();
          break;
      }
      return str;
    }

    public static string GetFilePath(string path, MediaPlayer.FileLocation location)
    {
      string str = string.Empty;
      if (!string.IsNullOrEmpty(path))
      {
        switch (location)
        {
          case MediaPlayer.FileLocation.AbsolutePathOrURL:
            str = path;
            break;
          case MediaPlayer.FileLocation.RelativeToProjectFolder:
          case MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder:
          case MediaPlayer.FileLocation.RelativeToDataFolder:
          case MediaPlayer.FileLocation.RelativeToPeristentDataFolder:
            str = Path.Combine(MediaPlayer.GetPath(location), path);
            break;
        }
      }
      return str;
    }

    private long GetPlatformFileOffset()
    {
      return (long) this._optionsAndroid.fileOffset;
    }

    private string GetPlatformHttpHeaderJson()
    {
      string str = this._optionsAndroid.httpHeaderJson;
      if (!string.IsNullOrEmpty(str))
        str = str.Trim();
      return str;
    }

    private string GetPlatformFilePath(Platform platform, ref string filePath, ref MediaPlayer.FileLocation fileLocation)
    {
      string empty = string.Empty;
      if (platform != Platform.Unknown)
      {
        MediaPlayer.PlatformOptions currentPlatformOptions = this.GetCurrentPlatformOptions();
        if (currentPlatformOptions != null && currentPlatformOptions.overridePath)
        {
          filePath = currentPlatformOptions.path;
          fileLocation = currentPlatformOptions.pathLocation;
        }
      }
      return MediaPlayer.GetFilePath(filePath, fileLocation);
    }

    public virtual BaseMediaPlayer CreatePlatformMediaPlayer()
    {
      AndroidMediaPlayer.InitialisePlatform();
      BaseMediaPlayer baseMediaPlayer = (BaseMediaPlayer) new AndroidMediaPlayer(this._optionsAndroid.useFastOesPath, this._optionsAndroid.showPosterFrame);
      if (baseMediaPlayer == null)
      {
        Debug.LogError((object) string.Format("[AVProVideo] Not supported on this platform {0} {1} {2} {3}.  Using null media player!", new object[4]
        {
          (object) Application.get_platform(),
          (object) SystemInfo.get_deviceModel(),
          (object) SystemInfo.get_processorType(),
          (object) SystemInfo.get_operatingSystem()
        }));
        baseMediaPlayer = (BaseMediaPlayer) new NullMediaPlayer();
      }
      return baseMediaPlayer;
    }

    private bool ForceWaitForNewFrame(int lastFrameCount, float timeoutMs)
    {
      bool flag = false;
      DateTime now = DateTime.Now;
      int num = 0;
      while (this.Control != null && (DateTime.Now - now).TotalMilliseconds < (double) timeoutMs)
      {
        this.m_Player.Update();
        if (lastFrameCount != this.TextureProducer.GetTextureFrameCount())
        {
          flag = true;
          break;
        }
        ++num;
      }
      this.m_Player.Render();
      return flag;
    }

    private void UpdateErrors()
    {
      ErrorCode lastError = this.m_Control.GetLastError();
      if (lastError == ErrorCode.None)
        return;
      Debug.LogError((object) ("[AVProVideo] Error: " + Helper.GetErrorMessage(lastError)));
      if (this.m_events == null)
        return;
      this.m_events.Invoke(this, MediaPlayerEvent.EventType.Error, lastError);
    }

    private void UpdateEvents()
    {
      if (this.m_events == null || this.m_Control == null)
        return;
      this.m_EventFired_FinishedPlaying = this.FireEventIfPossible(MediaPlayerEvent.EventType.FinishedPlaying, this.m_EventFired_FinishedPlaying);
      if (this.m_EventFired_Started && this.m_Control != null && !this.m_Control.IsPlaying())
        this.m_EventFired_Started = false;
      if (this.m_EventFired_FinishedPlaying && this.m_Control != null && (this.m_Control.IsPlaying() && !this.m_Control.IsFinished()))
        this.m_EventFired_FinishedPlaying = false;
      this.m_EventFired_MetaDataReady = this.FireEventIfPossible(MediaPlayerEvent.EventType.MetaDataReady, this.m_EventFired_MetaDataReady);
      this.m_EventFired_ReadyToPlay = this.FireEventIfPossible(MediaPlayerEvent.EventType.ReadyToPlay, this.m_EventFired_ReadyToPlay);
      this.m_EventFired_Started = this.FireEventIfPossible(MediaPlayerEvent.EventType.Started, this.m_EventFired_Started);
      this.m_EventFired_FirstFrameReady = this.FireEventIfPossible(MediaPlayerEvent.EventType.FirstFrameReady, this.m_EventFired_FirstFrameReady);
      if (this.FireEventIfPossible(MediaPlayerEvent.EventType.SubtitleChange, false))
        this.m_previousSubtitleIndex = this.m_Subtitles.GetSubtitleIndex();
      bool flag = this.m_Info.IsPlaybackStalled();
      if (flag == this.m_isPlaybackStalled)
        return;
      this.m_isPlaybackStalled = flag;
      this.FireEventIfPossible(!this.m_isPlaybackStalled ? MediaPlayerEvent.EventType.Unstalled : MediaPlayerEvent.EventType.Stalled, false);
    }

    private bool FireEventIfPossible(MediaPlayerEvent.EventType eventType, bool hasFired)
    {
      if (this.CanFireEvent(eventType, hasFired))
      {
        hasFired = true;
        this.m_events.Invoke(this, eventType, ErrorCode.None);
      }
      return hasFired;
    }

    private bool CanFireEvent(MediaPlayerEvent.EventType et, bool hasFired)
    {
      bool flag = false;
      if (this.m_events != null && this.m_Control != null && !hasFired)
      {
        switch (et)
        {
          case MediaPlayerEvent.EventType.MetaDataReady:
            flag = this.m_Control.HasMetaData();
            break;
          case MediaPlayerEvent.EventType.ReadyToPlay:
            flag = !this.m_Control.IsPlaying() && this.m_Control.CanPlay() && !this.m_AutoStart;
            break;
          case MediaPlayerEvent.EventType.Started:
            flag = this.m_Control.IsPlaying();
            break;
          case MediaPlayerEvent.EventType.FirstFrameReady:
            flag = this.m_Texture != null && this.m_Control.CanPlay() && this.m_Texture.GetTextureFrameCount() > 0;
            break;
          case MediaPlayerEvent.EventType.FinishedPlaying:
            flag = !this.m_Control.IsLooping() && this.m_Control.CanPlay() && this.m_Control.IsFinished();
            break;
          case MediaPlayerEvent.EventType.SubtitleChange:
            flag = this.m_previousSubtitleIndex != this.m_Subtitles.GetSubtitleIndex();
            break;
          case MediaPlayerEvent.EventType.Stalled:
            flag = this.m_Info.IsPlaybackStalled();
            break;
          case MediaPlayerEvent.EventType.Unstalled:
            flag = !this.m_Info.IsPlaybackStalled();
            break;
        }
      }
      return flag;
    }

    private void OnApplicationFocus(bool focusStatus)
    {
      if (!focusStatus || this.m_Control == null || !this.m_WasPlayingOnPause)
        return;
      this.m_WasPlayingOnPause = false;
      this.m_Control.Play();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
      if (pauseStatus)
      {
        if (this.m_Control == null || !this.m_Control.IsPlaying())
          return;
        this.m_WasPlayingOnPause = true;
        this.m_Control.Pause();
      }
      else
        this.OnApplicationFocus(true);
    }

    public Texture2D ExtractFrame(Texture2D target, float timeSeconds = -1f, bool accurateSeek = true, int timeoutMs = 1000)
    {
      Texture2D texture2D = target;
      Texture frame = this.ExtractFrame(timeSeconds, accurateSeek, timeoutMs);
      if (Object.op_Inequality((Object) frame, (Object) null))
        texture2D = Helper.GetReadableTexture(frame, this.TextureProducer.RequiresVerticalFlip(), Helper.GetOrientation(this.Info.GetTextureTransform()), target);
      return texture2D;
    }

    private Texture ExtractFrame(float timeSeconds = -1f, bool accurateSeek = true, int timeoutMs = 1000)
    {
      Texture texture = (Texture) null;
      if (this.m_Control != null)
      {
        if ((double) timeSeconds >= 0.0)
        {
          this.Pause();
          float timeMs = timeSeconds * 1000f;
          if (Object.op_Inequality((Object) this.TextureProducer.GetTexture(0), (Object) null) && (double) this.m_Control.GetCurrentTimeMs() == (double) timeMs)
          {
            texture = this.TextureProducer.GetTexture(0);
          }
          else
          {
            int textureFrameCount = this.TextureProducer.GetTextureFrameCount();
            if (accurateSeek)
              this.m_Control.Seek(timeMs);
            else
              this.m_Control.SeekFast(timeMs);
            this.ForceWaitForNewFrame(textureFrameCount, (float) timeoutMs);
            texture = this.TextureProducer.GetTexture(0);
          }
        }
        else
          texture = this.TextureProducer.GetTexture(0);
      }
      return texture;
    }

    public void SetGuiPositionFromVideoIndex(int index)
    {
    }

    public void SetDebugGuiEnabled(bool bEnabled)
    {
      this.m_DebugGui = bEnabled;
    }

    [Serializable]
    public class Setup
    {
      public bool displayDebugGUI;
      public bool persistent;
    }

    public enum FileLocation
    {
      AbsolutePathOrURL,
      RelativeToProjectFolder,
      RelativeToStreamingAssetsFolder,
      RelativeToDataFolder,
      RelativeToPeristentDataFolder,
    }

    [Serializable]
    public class PlatformOptions
    {
      public MediaPlayer.FileLocation pathLocation = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
      public bool overridePath;
      public string path;

      public virtual bool IsModified()
      {
        return this.overridePath;
      }
    }

    [Serializable]
    public class OptionsWindows : MediaPlayer.PlatformOptions
    {
      public bool useHardwareDecoding = true;
      public bool forceAudioResample = true;
      public int desiredAudioChannels = 2;
      public string forceAudioOutputDeviceName = string.Empty;
      public Windows.VideoApi videoApi;
      public bool useUnityAudio;
      public bool useTextureMips;

      public override bool IsModified()
      {
        if (!base.IsModified() && this.useHardwareDecoding && (!this.useTextureMips && !this.useUnityAudio) && (this.videoApi == Windows.VideoApi.MediaFoundation && this.forceAudioResample))
          return this.desiredAudioChannels != 2;
        return true;
      }
    }

    [Serializable]
    public class OptionsMacOSX : MediaPlayer.PlatformOptions
    {
      [Multiline]
      public string httpHeaderJson;

      public override bool IsModified()
      {
        if (!base.IsModified())
          return !string.IsNullOrEmpty(this.httpHeaderJson);
        return true;
      }
    }

    [Serializable]
    public class OptionsIOS : MediaPlayer.PlatformOptions
    {
      public bool useYpCbCr420Textures;
      [Multiline]
      public string httpHeaderJson;

      public override bool IsModified()
      {
        if (!base.IsModified() && string.IsNullOrEmpty(this.httpHeaderJson))
          return this.useYpCbCr420Textures;
        return true;
      }
    }

    [Serializable]
    public class OptionsTVOS : MediaPlayer.PlatformOptions
    {
      public bool useYpCbCr420Textures;
      [Multiline]
      public string httpHeaderJson;

      public override bool IsModified()
      {
        if (!base.IsModified() && string.IsNullOrEmpty(this.httpHeaderJson))
          return this.useYpCbCr420Textures;
        return true;
      }
    }

    [Serializable]
    public class OptionsAndroid : MediaPlayer.PlatformOptions
    {
      public bool useFastOesPath;
      public bool showPosterFrame;
      [Multiline]
      public string httpHeaderJson;
      [SerializeField]
      public int fileOffset;

      public override bool IsModified()
      {
        if (!base.IsModified() && this.fileOffset == 0 && (!this.useFastOesPath && !this.showPosterFrame))
          return !string.IsNullOrEmpty(this.httpHeaderJson);
        return true;
      }
    }

    [Serializable]
    public class OptionsWindowsPhone : MediaPlayer.PlatformOptions
    {
      public bool useHardwareDecoding = true;
      public bool forceAudioResample = true;
      public int desiredAudioChannels = 2;
      public bool useUnityAudio;
      public bool useTextureMips;

      public override bool IsModified()
      {
        if (!base.IsModified() && this.useHardwareDecoding && (!this.useTextureMips && !this.useUnityAudio) && this.forceAudioResample)
          return this.desiredAudioChannels != 2;
        return true;
      }
    }

    [Serializable]
    public class OptionsWindowsUWP : MediaPlayer.PlatformOptions
    {
      public bool useHardwareDecoding = true;
      public bool forceAudioResample = true;
      public int desiredAudioChannels = 2;
      public bool useUnityAudio;
      public bool useTextureMips;

      public override bool IsModified()
      {
        if (!base.IsModified() && this.useHardwareDecoding && (!this.useTextureMips && !this.useUnityAudio) && this.forceAudioResample)
          return this.desiredAudioChannels != 2;
        return true;
      }
    }

    [Serializable]
    public class OptionsWebGL : MediaPlayer.PlatformOptions
    {
    }
  }
}
