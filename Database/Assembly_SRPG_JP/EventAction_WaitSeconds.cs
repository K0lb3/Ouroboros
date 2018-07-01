// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_WaitSeconds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("待機/秒数を指定", "指定した時間の間スクリプトの実行を停止します。", 5592405, 4473992)]
  public class EventAction_WaitSeconds : EventAction
  {
    public float WaitSeconds = 1f;
    private float mTimer;

    public override void OnActivate()
    {
      this.mTimer = this.WaitSeconds;
    }

    public override void Update()
    {
      this.mTimer -= Time.get_deltaTime();
      if ((double) this.mTimer > 0.0)
        return;
      this.ActivateNext();
    }

    public override void GoToEndState()
    {
      this.mTimer = 0.0f;
    }

    public override void SkipImmediate()
    {
      this.mTimer = 0.0f;
    }
  }
}
