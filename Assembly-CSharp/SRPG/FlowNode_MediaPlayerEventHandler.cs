// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_MediaPlayerEventHandler
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(12, "OnEnd", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(14, "OnError", FlowNode.PinTypes.Output, 14)]
  [FlowNode.Pin(13, "OnResize", FlowNode.PinTypes.Output, 13)]
  [FlowNode.NodeType("EzMovieTexture/MediaPlayerEventHandler", 58751)]
  [FlowNode.Pin(10, "OnReady", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "OnFirstFrameReady", FlowNode.PinTypes.Output, 11)]
  public class FlowNode_MediaPlayerEventHandler : FlowNodePersistent
  {
    public const int PIN_ID_ON_READY = 10;
    public const int PIN_ID_ON_FIRST_FRAME_READY = 11;
    public const int PIN_ID_ON_END = 12;
    public const int PIN_ID_ON_RESIZE = 13;
    public const int PIN_ID_ON_ERROR = 14;
    public MediaPlayerCtrl m_srcVideo;

    private void Start()
    {
      this.m_srcVideo.OnReady += new MediaPlayerCtrl.VideoReady(this.OnReady);
      this.m_srcVideo.OnVideoFirstFrameReady += new MediaPlayerCtrl.VideoFirstFrameReady(this.OnFirstFrameReady);
      this.m_srcVideo.OnVideoError += new MediaPlayerCtrl.VideoError(this.OnError);
      this.m_srcVideo.OnEnd += new MediaPlayerCtrl.VideoEnd(this.OnEnd);
      this.m_srcVideo.OnResize += new MediaPlayerCtrl.VideoResize(this.OnResize);
    }

    public void Skip()
    {
      this.m_srcVideo.OnEnd();
    }

    private void OnReady()
    {
      this.ActivateOutputLinks(10);
    }

    private void OnFirstFrameReady()
    {
      this.ActivateOutputLinks(11);
    }

    private void OnEnd()
    {
      this.ActivateOutputLinks(12);
    }

    private void OnResize()
    {
      this.ActivateOutputLinks(13);
    }

    private void OnError(MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCodeExtra)
    {
      this.ActivateOutputLinks(14);
    }

    public override void OnActivate(int pinID)
    {
    }
  }
}
