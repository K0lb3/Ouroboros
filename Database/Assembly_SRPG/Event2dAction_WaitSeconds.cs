// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_WaitSeconds
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("待機/秒数を指定(2D)", "指定した時間の間スクリプトの実行を停止します。", 5592405, 4473992)]
  public class Event2dAction_WaitSeconds : EventAction
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
  }
}
