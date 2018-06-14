// Decompiled with JetBrains decompiler
// Type: CustomSound2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

[AddComponentMenu("Audio/Custom Sound2")]
public class CustomSound2 : MonoBehaviour
{
  public string CueSheet;
  public string CueName;
  public bool PlayOnAwake;
  public bool LoopFlag;
  public float StopSec;
  private bool mPlayAutomatic;
  private MySound.PlayHandle mPlayHandle;

  public CustomSound2()
  {
    base.\u002Ector();
  }

  private void OnEnable()
  {
    this.mPlayAutomatic = this.PlayOnAwake;
  }

  private void OnDisable()
  {
    this.mPlayAutomatic = false;
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
    this.Stop();
    if (this.LoopFlag)
    {
      MySound.EType type = MySound.EType.SE;
      if (!string.IsNullOrEmpty(this.CueName))
      {
        if (this.CueName.StartsWith("BGM_"))
          type = MySound.EType.BGM;
        else if (this.CueName.StartsWith("JIN_"))
          type = MySound.EType.JINGLE;
        else if (this.CueName.StartsWith("VO_"))
          type = MySound.EType.VOICE;
      }
      this.mPlayHandle = MonoSingleton<MySound>.Instance.PlayLoop(this.CueSheet, this.CueName, type, 0.0f);
    }
    else
      MonoSingleton<MySound>.Instance.PlayOneShot(this.CueSheet, this.CueName, MySound.EType.SE, 0.0f);
  }

  public void Stop()
  {
    if (this.mPlayHandle == null)
      return;
    this.mPlayHandle.Stop(this.StopSec);
    this.mPlayHandle = (MySound.PlayHandle) null;
  }
}
