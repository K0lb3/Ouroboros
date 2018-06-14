// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_WaitTap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/待機", "待機します。", 5592405, 4473992)]
  public class Event2dAction_WaitTap : EventAction
  {
    [HideInInspector]
    public float WaitSeconds = 1f;
    public bool tapWaiting;
    private float mTimer;
    private bool waitFrame;

    public override void OnActivate()
    {
      this.waitFrame = false;
      if (this.tapWaiting)
        return;
      this.mTimer = this.WaitSeconds;
    }

    public override void Update()
    {
      if (!this.waitFrame)
      {
        this.waitFrame = true;
      }
      else
      {
        if (this.tapWaiting)
          return;
        this.mTimer -= Time.get_deltaTime();
        if ((double) this.mTimer > 0.0)
          return;
        this.ActivateNext();
      }
    }

    public override bool Forward()
    {
      if (!this.waitFrame || !this.tapWaiting)
        return false;
      this.ActivateNext();
      return true;
    }
  }
}
