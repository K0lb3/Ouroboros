// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.AudioOutput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;

namespace RenderHeads.Media.AVProVideo
{
  [RequireComponent(typeof (AudioSource))]
  [HelpURL("http://renderheads.com/product/avpro-video/")]
  [AddComponentMenu("AVPro Video/Audio Output", 400)]
  public class AudioOutput : MonoBehaviour
  {
    public AudioOutput.AudioOutputMode _audioOutputMode;
    [SerializeField]
    private MediaPlayer _mediaPlayer;
    private AudioSource _audioSource;
    [HideInInspector]
    public int _channelMask;

    public AudioOutput()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this._audioSource = (AudioSource) ((Component) this).GetComponent<AudioSource>();
    }

    private void Start()
    {
      this.ChangeMediaPlayer(this._mediaPlayer);
    }

    private void OnDestroy()
    {
      this.ChangeMediaPlayer((MediaPlayer) null);
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this._mediaPlayer, (Object) null) || this._mediaPlayer.Control == null || !this._mediaPlayer.Control.IsPlaying())
        return;
      AudioOutput.ApplyAudioSettings(this._mediaPlayer, this._audioSource);
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
      switch (et)
      {
        case MediaPlayerEvent.EventType.Started:
          AudioOutput.ApplyAudioSettings(this._mediaPlayer, this._audioSource);
          this._audioSource.Play();
          break;
        case MediaPlayerEvent.EventType.Closing:
          this._audioSource.Stop();
          break;
      }
    }

    private static void ApplyAudioSettings(MediaPlayer player, AudioSource audioSource)
    {
      if (!Object.op_Inequality((Object) player, (Object) null) || player.Control == null)
        return;
      float volume = player.Control.GetVolume();
      bool flag = player.Control.IsMuted();
      float playbackRate = player.Control.GetPlaybackRate();
      audioSource.set_volume(volume);
      audioSource.set_mute(flag);
      audioSource.set_pitch(playbackRate);
    }

    public enum AudioOutputMode
    {
      Single,
      Multiple,
    }
  }
}
