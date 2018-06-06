// Decompiled with JetBrains decompiler
// Type: SRPG.SoundTrigger
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [DisallowMultipleComponent]
  public class SoundTrigger : MonoBehaviour
  {
    [FlexibleArray]
    public string[] VoiceNames;
    private MySound.Voice[] mVoices;

    public SoundTrigger()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mVoices = new MySound.Voice[this.VoiceNames.Length];
      for (int index = 0; index < this.VoiceNames.Length; ++index)
      {
        if (!string.IsNullOrEmpty(this.VoiceNames[index]))
          this.mVoices[index] = new MySound.Voice(this.VoiceNames[index], (string) null, (string) null);
      }
    }

    private void OnDestroy()
    {
      for (int index = 0; index < this.mVoices.Length; ++index)
      {
        if (this.mVoices[index] != null)
        {
          this.mVoices[index].StopAll(1f);
          this.mVoices[index].Cleanup();
          this.mVoices[index] = (MySound.Voice) null;
        }
      }
    }

    public void PlayVoice(string cueID)
    {
      int length = cueID.IndexOf('.');
      if (length <= 0)
        return;
      string str = cueID.Substring(0, length);
      string cueID1 = cueID.Substring(length + 1);
      if (string.IsNullOrEmpty(cueID1))
        return;
      for (int index = 0; index < this.VoiceNames.Length; ++index)
      {
        if (this.VoiceNames[index] == str && this.mVoices[index] != null)
        {
          this.mVoices[index].Play(cueID1, 0.0f);
          break;
        }
      }
    }

    public void PlaySE(string cueID)
    {
      MonoSingleton<MySound>.Instance.PlaySEOneShot(cueID, 0.0f);
    }

    public void PlayJingle(string cueID)
    {
      MonoSingleton<MySound>.Instance.PlayJingle(cueID, 0.0f, (string) null);
    }
  }
}
