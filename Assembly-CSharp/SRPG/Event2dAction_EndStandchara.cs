// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_EndStandchara
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
