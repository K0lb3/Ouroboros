// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_DestroyActor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/削除", "指定のアクターを削除します。", 6702148, 11158596)]
  public class EventAction_DestroyActor : EventAction
  {
    [StringIsActorList]
    public string ActorID;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if (Object.op_Inequality((Object) actor, (Object) null))
      {
        TacticsUnitController component = (TacticsUnitController) actor.GetComponent<TacticsUnitController>();
        if (Object.op_Inequality((Object) component, (Object) null))
          Object.Destroy((Object) ((Component) component).get_gameObject());
      }
      this.ActivateNext();
    }
  }
}
