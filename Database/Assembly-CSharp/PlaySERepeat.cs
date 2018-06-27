// Decompiled with JetBrains decompiler
// Type: PlaySERepeat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class PlaySERepeat : MonoBehaviour
{
  private string mCueID;
  private float RepeatWait;
  private int mPlayCount;
  private float mRepeatWait;
  private bool mStop;
  private float mStopSec;
  private float mFadeOutSec;
  private bool mUseCustomPlay;
  private MySound.CueSheetHandle.ELoopFlag mCustomLoopFlag;
  private float mCustomDelaySec;
  private MySound.CueSheetHandle mHandle;
  private MySound.PlayHandle mPlayHandle;

  public PlaySERepeat()
  {
    base.\u002Ector();
  }

  public void Setup(string sheetName, string cueID, MySound.EType type, int repeatCount, float repeatWait, float stopSec, float fadeOutSec, bool useCustom, MySound.CueSheetHandle.ELoopFlag customLoopFlag, float customDelaySec)
  {
    this.mCueID = cueID;
    this.mPlayCount = repeatCount + 1;
    this.RepeatWait = repeatWait;
    this.mStopSec = stopSec;
    this.mStop = (double) stopSec > 0.0;
    this.mFadeOutSec = fadeOutSec;
    this.mUseCustomPlay = useCustom;
    this.mCustomLoopFlag = customLoopFlag;
    this.mCustomDelaySec = customDelaySec;
    this.mHandle = MySound.CueSheetHandle.Create(sheetName, type, true, true, false, false);
    this.mHandle.CreateDefaultOneShotSource();
  }

  private void Update()
  {
    if (this.mStop)
    {
      this.mStopSec -= Time.get_unscaledDeltaTime();
      if ((double) this.mStopSec <= 0.0)
      {
        if (this.mPlayHandle != null)
        {
          this.mPlayHandle.Stop(this.mFadeOutSec);
          this.mPlayHandle = (MySound.PlayHandle) null;
        }
        if (this.mHandle != null)
        {
          this.mHandle.StopDefaultAll(this.mFadeOutSec);
          this.mHandle = (MySound.CueSheetHandle) null;
        }
        Object.Destroy((Object) ((Component) this).get_gameObject());
        return;
      }
    }
    if (this.mPlayCount <= 0)
      return;
    this.mRepeatWait -= Time.get_unscaledDeltaTime();
    if ((double) this.mRepeatWait > 0.0)
      return;
    if (this.mUseCustomPlay)
      this.mPlayHandle = this.mHandle.Play(this.mCueID, this.mCustomLoopFlag, false, this.mCustomDelaySec);
    else
      this.mHandle.PlayDefaultOneShot(this.mCueID, false, 0.0f, false);
    this.mRepeatWait = this.RepeatWait;
    --this.mPlayCount;
    if (this.mStop || this.mPlayCount > 0)
      return;
    Object.Destroy((Object) ((Component) this).get_gameObject());
  }

  private void OnDestroy()
  {
    if (this.mPlayHandle != null)
    {
      this.mPlayHandle.Stop(this.mFadeOutSec);
      this.mPlayHandle = (MySound.PlayHandle) null;
    }
    if (this.mHandle == null)
      return;
    this.mHandle.StopDefaultAll(this.mFadeOutSec);
    this.mHandle = (MySound.CueSheetHandle) null;
  }
}
