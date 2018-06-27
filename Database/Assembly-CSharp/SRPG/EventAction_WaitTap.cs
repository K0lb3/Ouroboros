// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_WaitTap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/待機/秒数(タップ)を指定", "指定した時間の間スクリプトの実行を停止します。", 5592405, 4473992)]
  public class EventAction_WaitTap : EventAction
  {
    [HideInInspector]
    public float WaitSeconds = 1f;
    public bool waitTap;
    private float mTimer;

    public override void OnActivate()
    {
      this.mTimer = this.WaitSeconds;
    }

    public override void Update()
    {
      if (this.waitTap)
        return;
      this.mTimer -= Time.get_deltaTime();
      if ((double) this.mTimer > 0.0)
        return;
      this.ActivateNext();
    }

    public override void SkipImmediate()
    {
      this.mTimer = 0.0f;
    }

    public override bool Forward()
    {
      if (!this.waitTap)
        return false;
      this.ActivateNext();
      return true;
    }
  }
}
