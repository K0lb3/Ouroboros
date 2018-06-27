// Decompiled with JetBrains decompiler
// Type: CustomSound3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Custom Sound3")]
public class CustomSound3 : MonoBehaviour
{
  public string sheetName;
  public string cueID;
  public CustomSound3.EPlayFunction PlayFunction;
  public MySound.EType CueSheetHandlePlayCategory;
  public MySound.CueSheetHandle.ELoopFlag CueSheetHandlePlayLoopType;
  public bool PlayOnEnable;
  private bool mPlayAutomatic;
  public bool StopOnPlay;
  public bool StopOnDisable;
  public float StopSec;
  public float DelayPlaySec;
  private List<CustomSound3.PlayHandles> mHandles;

  public CustomSound3()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    this.mPlayAutomatic = this.PlayOnEnable;
  }

  private void OnDisable()
  {
    this.mPlayAutomatic = false;
    if (!this.StopOnDisable)
      return;
    this.Stop();
  }

  private void Update()
  {
    if (!this.mPlayAutomatic)
      return;
    this.Play();
    this.mPlayAutomatic = false;
  }

  public void Play()
  {
    if (this.StopOnPlay)
      this.Stop();
    if (this.mHandles == null)
      return;
    CustomSound3.PlayHandles playHandles = new CustomSound3.PlayHandles();
    if (playHandles == null)
      return;
    if (this.PlayFunction == CustomSound3.EPlayFunction.PlayBGM)
      MonoSingleton<MySound>.Instance.PlayBGM(this.cueID, this.sheetName, false);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.PlayBGMDelay)
      MonoSingleton<MySound>.Instance.PlayBGM(this.cueID, this.DelayPlaySec, this.sheetName);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.PlayJingle)
      MonoSingleton<MySound>.Instance.PlayJingle(this.cueID, this.DelayPlaySec, this.sheetName);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.VoicePlay)
    {
      playHandles.mVoice = new MySound.Voice(this.sheetName, (string) null, (string) null, false);
      if (playHandles.mVoice != null)
        playHandles.mVoice.Play(this.cueID, this.DelayPlaySec, false);
    }
    else if (this.PlayFunction == CustomSound3.EPlayFunction.PlaySEOneShot)
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, this.DelayPlaySec);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.PlaySELoop)
      playHandles.mPlayHandle = MonoSingleton<MySound>.Instance.PlaySELoop(this.cueID, this.DelayPlaySec);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.CueSheetHandlePlayOneShot)
      MonoSingleton<MySound>.Instance.PlayOneShot(this.sheetName, this.cueID, this.CueSheetHandlePlayCategory, this.DelayPlaySec);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.CueSheetHandlePlayLoop)
      playHandles.mPlayHandle = MonoSingleton<MySound>.Instance.PlayLoop(this.sheetName, this.cueID, this.CueSheetHandlePlayCategory, this.DelayPlaySec);
    else if (this.PlayFunction == CustomSound3.EPlayFunction.CueSheetHandlePlay)
    {
      playHandles.mCueSheetHandle = MySound.CueSheetHandle.Create(this.sheetName, this.CueSheetHandlePlayCategory, true, true, false, false);
      if (playHandles.mCueSheetHandle != null)
        playHandles.mPlayHandle = playHandles.mCueSheetHandle.Play(this.cueID, this.CueSheetHandlePlayLoopType, true, this.DelayPlaySec);
    }
    if (!playHandles.IsValid)
      return;
    this.mHandles.Add(playHandles);
  }

  public void Stop()
  {
    if (this.PlayFunction == CustomSound3.EPlayFunction.PlayBGM || this.PlayFunction == CustomSound3.EPlayFunction.PlayBGMDelay)
      MonoSingleton<MySound>.Instance.StopBGM(this.StopSec);
    if (this.mHandles == null)
      return;
    using (List<CustomSound3.PlayHandles>.Enumerator enumerator = this.mHandles.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        CustomSound3.PlayHandles current = enumerator.Current;
        if (current != null)
        {
          if (current.mPlayHandle != null)
          {
            current.mPlayHandle.Stop(this.StopSec);
            current.mPlayHandle = (MySound.PlayHandle) null;
          }
          if (current.mCueSheetHandle != null)
          {
            current.mCueSheetHandle.StopDefaultAll(this.StopSec);
            current.mCueSheetHandle = (MySound.CueSheetHandle) null;
          }
          if (current.mVoice != null)
          {
            current.mVoice.StopAll(this.StopSec);
            current.mVoice = (MySound.Voice) null;
          }
        }
      }
    }
    this.mHandles.Clear();
  }

  public enum EPlayFunction
  {
    CueSheetHandlePlay,
    CueSheetHandlePlayOneShot,
    CueSheetHandlePlayLoop,
    VoicePlay,
    PlaySEOneShot,
    PlaySELoop,
    PlayJingle,
    PlayBGM,
    PlayBGMDelay,
  }

  private class PlayHandles
  {
    public MySound.PlayHandle mPlayHandle;
    public MySound.CueSheetHandle mCueSheetHandle;
    public MySound.Voice mVoice;

    public bool IsValid
    {
      get
      {
        if (this.mPlayHandle == null && this.mCueSheetHandle == null)
          return this.mVoice != null;
        return true;
      }
    }
  }
}
