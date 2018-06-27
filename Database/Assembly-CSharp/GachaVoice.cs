// Decompiled with JetBrains decompiler
// Type: GachaVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class GachaVoice : MonoBehaviour
{
  public string DirectCharName;
  public int Excites;
  public string Play1CueName;
  public string Play2CueName;
  public string[] Play3Cuename;
  private int excites;
  private string mCharName;
  private string mCueName;
  private MySound.Voice mVoice;

  public GachaVoice()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    if (string.IsNullOrEmpty(this.DirectCharName))
      this.DirectCharName = "uroboros";
    this.mVoice = new MySound.Voice(this.DirectCharName);
    this.excites = 0;
    this.mCharName = this.DirectCharName;
  }

  public void Play1()
  {
    this.SetupCueName(this.Play1CueName);
    this.Play();
  }

  public void Play2()
  {
    this.SetupCueName(this.Play2CueName);
    this.Play();
  }

  public void Play3()
  {
    this.excites = this.Excites <= 0 || this.Excites >= 4 ? 0 : this.Excites - 1;
    this.SetupCueName(this.Play3Cuename[this.excites]);
    this.Play();
  }

  private void Play()
  {
    if (this.mVoice == null)
      return;
    this.mVoice.Play(this.mCueName, 0.0f, false);
  }

  public void Stop()
  {
    if (this.mVoice == null)
      return;
    this.mVoice.StopAll(0.0f);
  }

  public void Discard()
  {
    if (this.mVoice != null)
      this.mVoice.Cleanup();
    this.mVoice = (MySound.Voice) null;
    this.mCharName = (string) null;
  }

  public bool SetupCueName(string cuename)
  {
    if (string.IsNullOrEmpty(cuename) || string.IsNullOrEmpty(this.mCharName))
      return false;
    this.mCueName = MySound.Voice.ReplaceCharNameOfCueName(cuename, this.mCharName);
    return true;
  }
}
