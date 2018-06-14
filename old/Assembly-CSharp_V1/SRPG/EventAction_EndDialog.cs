// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_EndDialog
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("会話/閉じる", "表示されている吹き出しを閉じます", 5592456, 5592490)]
  public class EventAction_EndDialog : EventAction
  {
    [StringIsActorID]
    public string ActorID;

    public override void OnActivate()
    {
      for (int index = EventDialogBubble.Instances.Count - 1; index >= 0; --index)
      {
        if (string.IsNullOrEmpty(this.ActorID) || EventDialogBubble.Instances[index].BubbleID == this.ActorID)
          EventDialogBubble.Instances[index].Close();
      }
      this.ActivateNext();
    }
  }
}
