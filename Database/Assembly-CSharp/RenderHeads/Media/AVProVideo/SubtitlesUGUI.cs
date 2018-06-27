// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.SubtitlesUGUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RenderHeads.Media.AVProVideo
{
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Subtitles uGUI", 201)]
  public class SubtitlesUGUI : MonoBehaviour
  {
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    [SerializeField]
    private Text _text;

    public SubtitlesUGUI()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.ChangeMediaPlayer(this._mediaPlayer);
    }

    private void OnDestroy()
    {
      this.ChangeMediaPlayer((MediaPlayer) null);
    }

    public void ChangeMediaPlayer(MediaPlayer newPlayer)
    {
      if (Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
      {
        // ISSUE: method pointer
        this._mediaPlayer.Events.RemoveListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
        this._mediaPlayer = (MediaPlayer) null;
      }
      this._mediaPlayer = newPlayer;
      if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) null))
        return;
      // ISSUE: method pointer
      this._mediaPlayer.Events.AddListener(new UnityAction<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>((object) this, __methodptr(OnMediaPlayerEvent)));
    }

    private void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
      if (et != MediaPlayerEvent.EventType.SubtitleChange)
        return;
      this._text.set_text(this._mediaPlayer.Subtitles.GetSubtitleText().Replace("<font color=", "<color=").Replace("</font>", "</color>").Replace("<u>", string.Empty).Replace("</u>", string.Empty));
    }
  }
}
