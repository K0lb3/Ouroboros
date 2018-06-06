// Decompiled with JetBrains decompiler
// Type: CustomSound
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

[AddComponentMenu("Audio/Custom Sound")]
public class CustomSound : MonoBehaviour
{
  public CustomSound.EType type;
  public string cueID;
  public bool PlayOnAwake;
  public bool LoopFlag;
  public float StopSec;
  private bool mPlayAutomatic;
  private MySound.PlayHandle mPlayHandle;

  public CustomSound()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
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
    if (this.type == CustomSound.EType.SE)
    {
      if (this.LoopFlag)
        this.mPlayHandle = MonoSingleton<MySound>.Instance.PlaySELoop(this.cueID, 0.0f);
      else
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0.0f);
    }
    else
    {
      if (this.type != CustomSound.EType.JINGLE)
        return;
      MonoSingleton<MySound>.Instance.PlayJingle(this.cueID, 0.0f, (string) null);
    }
  }

  public void Stop()
  {
    if (this.mPlayHandle == null)
      return;
    this.mPlayHandle.Stop(this.StopSec);
    this.mPlayHandle = (MySound.PlayHandle) null;
  }

  public enum EType
  {
    SE,
    JINGLE,
  }
}
