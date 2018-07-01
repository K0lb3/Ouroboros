// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_DialogAsync
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("会話/表示 (非同期)", "会話の文章を表示します。", 5592456, 5592490)]
  public class EventAction_DialogAsync : EventAction_Dialog
  {
    public override void OnActivate()
    {
      base.OnActivate();
      this.ActivateNext();
    }

    protected override void OnFinish()
    {
    }
  }
}
