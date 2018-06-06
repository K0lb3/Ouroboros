// Decompiled with JetBrains decompiler
// Type: EasyMovieTexture
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;

public class EasyMovieTexture : Singleton<EasyMovieTexture>
{
  private const string PREFAB_PATH = "UI/FullScreenMovie";
  private EasyMovieTexture.OnFinished onFinished;

  public EasyMovieTexture.PlayResult PlayFullScreenMovie(string url, string prefabPath, EasyMovieTexture.ControlMode controlMode, EasyMovieTexture.OnFinished callback)
  {
    GameObject gameObject = AssetManager.Load<GameObject>(!string.IsNullOrEmpty(prefabPath) ? prefabPath : "UI/FullScreenMovie");
    MediaPlayerCtrl ctrl = (MediaPlayerCtrl) null;
    this.onFinished = callback;
    if (Object.op_Inequality((Object) gameObject, (Object) null))
      ctrl = (MediaPlayerCtrl) ((GameObject) Object.Instantiate<GameObject>((M0) gameObject)).GetComponent<MediaPlayerCtrl>();
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      ctrl.OnReady += new MediaPlayerCtrl.VideoReady(this.OnReady);
      ctrl.OnVideoFirstFrameReady += new MediaPlayerCtrl.VideoFirstFrameReady(this.OnFirstFrameReady);
      ctrl.OnVideoError += new MediaPlayerCtrl.VideoError(this.OnError);
      ctrl.OnEnd += new MediaPlayerCtrl.VideoEnd(this.OnEnd);
      ctrl.OnResize += new MediaPlayerCtrl.VideoResize(this.OnResize);
    }
    ctrl.Load(url);
    return new EasyMovieTexture.PlayResult(ctrl);
  }

  public EasyMovieTexture.PlayResult PlayFullScreenMovie(string url, EasyMovieTexture.ControlMode controlMode, EasyMovieTexture.OnFinished callback)
  {
    GameObject gameObject = AssetManager.Load<GameObject>("UI/FullScreenMovie");
    MediaPlayerCtrl ctrl = (MediaPlayerCtrl) null;
    this.onFinished = callback;
    if (Object.op_Inequality((Object) gameObject, (Object) null))
      ctrl = (MediaPlayerCtrl) ((GameObject) Object.Instantiate<GameObject>((M0) gameObject)).GetComponent<MediaPlayerCtrl>();
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      ctrl.OnReady += new MediaPlayerCtrl.VideoReady(this.OnReady);
      ctrl.OnVideoFirstFrameReady += new MediaPlayerCtrl.VideoFirstFrameReady(this.OnFirstFrameReady);
      ctrl.OnVideoError += new MediaPlayerCtrl.VideoError(this.OnError);
      ctrl.OnEnd += new MediaPlayerCtrl.VideoEnd(this.OnEnd);
      ctrl.OnResize += new MediaPlayerCtrl.VideoResize(this.OnResize);
    }
    ctrl.Load(url);
    return new EasyMovieTexture.PlayResult(ctrl);
  }

  private void OnReady()
  {
  }

  private void OnFirstFrameReady()
  {
  }

  private void OnEnd()
  {
    if (this.onFinished != null)
      ;
  }

  private void OnResize()
  {
  }

  private void OnError(MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCodeExtra)
  {
    if (this.onFinished != null)
      ;
  }

  public enum ControlMode
  {
    Full,
    Skip,
    CancelOnInput,
    Hidden,
  }

  public class PlayResult
  {
    public bool isSuccess;
    public bool isError;
    public bool isEnd;
    private MediaPlayerCtrl mediaPlayerCtrl;

    public PlayResult(MediaPlayerCtrl ctrl)
    {
      ctrl.OnEnd += new MediaPlayerCtrl.VideoEnd(this.OnEnd);
      ctrl.OnVideoError += new MediaPlayerCtrl.VideoError(this.OnError);
      this.mediaPlayerCtrl = ctrl;
    }

    private void OnEnd()
    {
      FadeController.Instance.FadeTo(Color.get_black(), 0.3f, 0);
      this.isEnd = true;
    }

    private void OnError(MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCode, MediaPlayerCtrl.MEDIAPLAYER_ERROR errorCodeExtra)
    {
      FadeController.Instance.FadeTo(Color.get_black(), 0.3f, 0);
      this.isError = true;
    }

    public void ForceSkip()
    {
      if (!Object.op_Inequality((Object) this.mediaPlayerCtrl, (Object) null) || this.isEnd)
        return;
      this.mediaPlayerCtrl.OnEnd();
    }
  }

  public delegate void OnFinished();
}
