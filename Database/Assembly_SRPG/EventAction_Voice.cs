// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Voice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/ボイス再生", "ボイスを再生します。", 4478293, 4491400)]
  internal class EventAction_Voice : EventAction
  {
    public string m_VoiceName;
    [StringIsActorID]
    public bool m_Async;
    [HideInInspector]
    public float m_Delay;
    private MySound.Voice m_Voice;
    private bool m_Play;

    public override void OnActivate()
    {
      if (this.m_Async)
      {
        this.ActivateNext(true);
        if ((double) this.m_Delay > 0.0)
          return;
        this.m_Play = this.PlayVoice();
      }
      else
        this.m_Play = this.PlayVoice();
    }

    public override void Update()
    {
      if (this.m_Play && this.m_Voice != null)
      {
        if (this.m_Voice.IsPlaying)
          return;
        if (!this.m_Async)
          this.ActivateNext();
        else
          this.enabled = false;
      }
      else
      {
        if (!this.m_Async)
          return;
        this.m_Delay -= Time.get_deltaTime();
        if ((double) this.m_Delay >= 0.0 || (this.m_Play = this.PlayVoice()))
          return;
        this.enabled = false;
      }
    }

    private bool PlayVoice()
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(this.m_VoiceName))
      {
        string[] strArray = this.m_VoiceName.Split('.');
        if (strArray.Length == 2)
        {
          string sheetName = strArray[0];
          string cueID = strArray[1];
          this.m_Voice = new MySound.Voice(sheetName, (string) null, (string) null, false);
          this.m_Voice.Play(cueID, 0.0f, false);
          flag = true;
        }
      }
      return flag;
    }
  }
}
