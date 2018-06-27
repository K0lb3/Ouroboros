// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_QuitDisable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("強制終了/禁止(2D)", "強制終了を禁止します", 5592405, 4473992)]
  public class Event2dAction_QuitDisable : EventAction
  {
    public override void OnActivate()
    {
      EventQuit eventQuit = EventQuit.Find();
      if (Object.op_Equality((Object) null, (Object) eventQuit))
      {
        this.ActivateNext();
      }
      else
      {
        ((Component) eventQuit).get_gameObject().SetActive(false);
        this.ActivateNext();
      }
    }
  }
}
