// Decompiled with JetBrains decompiler
// Type: MediaPlayerEvent
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class MediaPlayerEvent : MonoBehaviour
{
  public MediaPlayerCtrl m_srcVideo;

  public MediaPlayerEvent()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.m_srcVideo.OnReady += new MediaPlayerCtrl.VideoReady(this.OnReady);
    this.m_srcVideo.OnVideoFirstFrameReady += new MediaPlayerCtrl.VideoFirstFrameReady(this.OnFirstFrameReady);
    this.m_srcVideo.OnVideoError += new MediaPlayerCtrl.VideoError(this.OnError);
    this.m_srcVideo.OnEnd += new MediaPlayerCtrl.VideoEnd(this.OnEnd);
    this.m_srcVideo.OnResize += new MediaPlayerCtrl.VideoResize(this.OnResize);
  }

  private void Update()
  {
  }

  private void OnReady()
  {
    Debug.Log((object) nameof (OnReady));
  }

  private void OnFirstFrameReady()
  {
    Debug.Log((object) nameof (OnFirstFrameReady));
  }

  private void OnEnd()
  {
    Debug.Log((object) nameof (OnEnd));
  }

  private void OnResize()
  {
    Debug.Log((object) nameof (OnResize));
  }

  private void OnError(MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCodeExtra)
  {
    Debug.Log((object) nameof (OnError));
  }
}
