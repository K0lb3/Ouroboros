// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MediaPlayerController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine.Events;

namespace SRPG
{
  [FlowNode.Pin(1050, "OnLoadFailed", FlowNode.PinTypes.Output, 1050)]
  [FlowNode.Pin(1000, "OnReady", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1010, "OnFirstFrameReady", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1020, "OnBufferingStart", FlowNode.PinTypes.Output, 1020)]
  [FlowNode.Pin(1021, "OnBufferingEnd", FlowNode.PinTypes.Output, 1021)]
  [FlowNode.Pin(1022, "OnBufferingTimeout", FlowNode.PinTypes.Output, 1022)]
  [FlowNode.Pin(1030, "OnEnd", FlowNode.PinTypes.Output, 1030)]
  [FlowNode.Pin(1040, "OnError", FlowNode.PinTypes.Output, 1040)]
  [FlowNode.NodeType("AVProVideo/MediaPlayerController")]
  [FlowNode.Pin(10, "Play", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "Pause", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(30, "Stop", FlowNode.PinTypes.Input, 30)]
  [FlowNode.Pin(40, "Skip", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(50, "Unload", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(60, "Reload", FlowNode.PinTypes.Input, 60)]
  public class FlowNode_MediaPlayerController : FlowNodePersistent
  {
    public const int PIN_ID_PLAY = 10;
    public const int PIN_ID_PAUSE = 20;
    public const int PIN_ID_STOP = 30;
    public const int PIN_ID_SKIP = 40;
    public const int PIN_ID_UNLOAD = 50;
    public const int PIN_ID_RELOAD = 60;
    public const int PIN_ID_ON_READY = 1000;
    public const int PIN_ID_ON_FIRST_FRAME_READY = 1010;
    public const int PIN_ID_ON_BUFFERING_START = 1020;
    public const int PIN_ID_ON_BUFFERING_END = 1021;
    public const int PIN_ID_ON_BUFFERING_TIMEOUT = 1022;
    public const int PIN_ID_ON_END = 1030;
    public const int PIN_ID_ON_ERROR = 1040;
    public const int PIN_ID_ON_LOAD_FAILED = 1050;
    public MediaPlayerWrapper m_MediaPlayerWrapper;
    public bool m_EnableBufferingTimeout;
    public float m_BufferingGraceTime;

    private void Start()
    {
      // ISSUE: method pointer
      this.m_MediaPlayerWrapper.Events.RemoveListener(new UnityAction<MediaPlayerWrapper.Event.Type>((object) this, __methodptr(OnMediaPlayerEvent)));
      // ISSUE: method pointer
      this.m_MediaPlayerWrapper.Events.AddListener(new UnityAction<MediaPlayerWrapper.Event.Type>((object) this, __methodptr(OnMediaPlayerEvent)));
      this.m_MediaPlayerWrapper.EnableBufferingTimeout = this.m_EnableBufferingTimeout;
      this.m_MediaPlayerWrapper.BufferingGraceTime = this.m_BufferingGraceTime;
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.m_MediaPlayerWrapper.Play();
          break;
        case 20:
          this.m_MediaPlayerWrapper.Pause();
          break;
        case 30:
          this.m_MediaPlayerWrapper.Stop();
          break;
        case 40:
          this.m_MediaPlayerWrapper.SkipToEnd();
          break;
        case 50:
          this.m_MediaPlayerWrapper.Unload();
          break;
        case 60:
          this.m_MediaPlayerWrapper.Reload(false);
          break;
      }
    }

    public void Load(string url)
    {
      this.m_MediaPlayerWrapper.LoadFromURL(url, false);
    }

    public void SetVolume(float value)
    {
      this.m_MediaPlayerWrapper.SetVolume(value);
    }

    private void OnMediaPlayerEvent(MediaPlayerWrapper.Event.Type eventType)
    {
      switch (eventType)
      {
        case MediaPlayerWrapper.Event.Type.ReadyToPlay:
          this.OnReady();
          break;
        case MediaPlayerWrapper.Event.Type.FirstFrameReady:
          this.OnFirstFrameReady();
          break;
        case MediaPlayerWrapper.Event.Type.FinishedPlaying:
          this.OnFinished();
          break;
        case MediaPlayerWrapper.Event.Type.Error:
          this.OnError();
          break;
        case MediaPlayerWrapper.Event.Type.LoadFailed:
          this.OnLoadFailed();
          break;
        case MediaPlayerWrapper.Event.Type.BufferingStart:
          this.OnBufferingStart();
          break;
        case MediaPlayerWrapper.Event.Type.BufferingEnd:
          this.OnBufferingEnd();
          break;
        case MediaPlayerWrapper.Event.Type.BufferingTimeout:
          this.OnBufferingTimeout();
          break;
      }
    }

    private void OnReady()
    {
      this.ActivateOutputLinks(1000);
    }

    private void OnFirstFrameReady()
    {
      this.ActivateOutputLinks(1010);
    }

    private void OnFinished()
    {
      this.ActivateOutputLinks(1030);
    }

    private void OnBufferingStart()
    {
      this.ActivateOutputLinks(1020);
    }

    private void OnBufferingEnd()
    {
      this.ActivateOutputLinks(1021);
    }

    private void OnBufferingTimeout()
    {
      this.ActivateOutputLinks(1022);
    }

    private void OnError()
    {
      this.ActivateOutputLinks(1040);
    }

    private void OnLoadFailed()
    {
      this.ActivateOutputLinks(1050);
    }
  }
}
