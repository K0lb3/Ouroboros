// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_ChangeEmotion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/表情切替", "指定したキャラの表情を切り替えます。", 5592405, 4473992)]
  public class Event2dAction_ChangeEmotion : EventAction
  {
    public string CharaID;
    public string Emotion;

    public override void PreStart()
    {
    }

    public override void OnActivate()
    {
      if (!string.IsNullOrEmpty(this.CharaID) && !string.IsNullOrEmpty(this.Emotion))
      {
        EventStandCharaController2 instances = EventStandCharaController2.FindInstances(this.CharaID);
        instances.Emotion = this.Emotion;
        instances.UpdateEmotion(this.Emotion);
      }
      this.ActivateNext();
    }
  }
}
