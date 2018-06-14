// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.IMediaInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace RenderHeads.Media.AVProVideo
{
  public interface IMediaInfo
  {
    float GetDurationMs();

    int GetVideoWidth();

    int GetVideoHeight();

    float GetVideoFrameRate();

    float GetVideoDisplayRate();

    bool HasVideo();

    bool HasAudio();

    int GetAudioTrackCount();

    string GetCurrentAudioTrackId();

    int GetCurrentAudioTrackBitrate();

    int GetVideoTrackCount();

    string GetCurrentVideoTrackId();

    int GetCurrentVideoTrackBitrate();

    string GetPlayerDescription();

    bool PlayerSupportsLinearColorSpace();

    bool IsPlaybackStalled();

    float[] GetTextureTransform();
  }
}
