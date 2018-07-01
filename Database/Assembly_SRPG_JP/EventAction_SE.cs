// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/SE再生", "SEを再生します", 4478293, 4491400)]
  public class EventAction_SE : EventAction
  {
    public string m_CueName;
    public bool m_Async;
    public float m_Delay;
    [HideInInspector]
    public float m_Wait;
    private bool m_bPlay;

    public override void OnActivate()
    {
      if (this.m_Async)
      {
        if ((double) this.m_Delay <= 0.0)
        {
          MonoSingleton<MySound>.Instance.PlaySEOneShot(this.m_CueName, 0.0f);
          this.m_bPlay = true;
          this.ActivateNext();
        }
        else
          this.ActivateNext(true);
      }
      else
      {
        if ((double) this.m_Delay > 0.0)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.m_CueName, 0.0f);
        this.m_bPlay = true;
        if ((double) this.m_Wait > 0.0)
          return;
        this.ActivateNext();
      }
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
        if (this.m_Async)
        {
          this.enabled = false;
        }
        else
        {
          if ((double) this.m_Wait > 0.0)
            return;
          this.ActivateNext();
        }
      }
    }
  }
}
