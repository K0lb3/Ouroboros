// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.MediaPlayerEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine.Events;

namespace RenderHeads.Media.AVProVideo
{
  [Serializable]
  public class MediaPlayerEvent : UnityEvent<MediaPlayer, MediaPlayerEvent.EventType, ErrorCode>
  {
    public MediaPlayerEvent()
    {
      base.\u002Ector();
    }

    public enum EventType
    {
      MetaDataReady,
      ReadyToPlay,
      Started,
      FirstFrameReady,
      FinishedPlaying,
      Closing,
      Error,
      SubtitleChange,
      Stalled,
      Unstalled,
    }
  }
}
