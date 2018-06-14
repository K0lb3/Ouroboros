// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_DialogAsync
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
