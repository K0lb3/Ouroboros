// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndStandchara
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("立ち絵/立ち絵消去(2D)", "表示されている立ち絵を消します", 5592405, 4473992)]
  public class Event2dAction_EndStandchara : EventAction
  {
    public string CharaID;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.CharaID))
      {
        for (int index = EventStandChara.Instances.Count - 1; index >= 0; --index)
          EventStandChara.Instances[index].Close(0.5f);
      }
      else
      {
        EventStandChara eventStandChara = EventStandChara.Find(this.CharaID);
        if (Object.op_Inequality((Object) eventStandChara, (Object) null))
          eventStandChara.Close(0.5f);
      }
      this.ActivateNext();
    }

    public override void Update()
    {
    }
  }
}
