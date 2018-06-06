// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SE
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/SE再生", "SEを再生します", 4478293, 4491400)]
  public class EventAction_SE : EventAction
  {
    public string m_CueName;
    public float m_Delay;
    public float m_Wait;
    private bool m_bPlay;

    public override void OnActivate()
    {
      if ((double) this.m_Delay > 0.0)
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.m_CueName, 0.0f);
      this.m_bPlay = true;
      if ((double) this.m_Wait > 0.0)
        return;
      this.ActivateNext();
    }

    public override void Update()
    {
      this.m_Delay -= Time.get_deltaTime();
      if (this.m_bPlay)
      {
        this.m_Wait -= Time.get_deltaTime();
        if ((double) this.m_Wait > 0.0)
          return;
        this.ActivateNext();
      }
      else
      {
        if ((double) this.m_Delay >= 0.0)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.m_CueName, 0.0f);
        this.m_bPlay = true;
        if ((double) this.m_Wait > 0.0)
          return;
        this.ActivateNext();
      }
    }
  }
}
