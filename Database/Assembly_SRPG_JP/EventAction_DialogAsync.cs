// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_DialogAsync
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
