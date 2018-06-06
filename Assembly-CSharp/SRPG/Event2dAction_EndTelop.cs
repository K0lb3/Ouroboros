// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndTelop
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("会話/テロップ閉じる(2D)", "表示されているテロップを閉じます", 5592405, 4473992)]
  public class Event2dAction_EndTelop : EventAction
  {
    public override void OnActivate()
    {
      for (int index = EventTelopBubble.Instances.Count - 1; index >= 0; --index)
        EventTelopBubble.Instances[index].Close();
      this.ActivateNext();
    }
  }
}
