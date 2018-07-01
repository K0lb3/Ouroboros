// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Yuremono
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/揺れもの切替", "アクターの揺れもの状態を切り替えます。", 6702148, 11158596)]
  public class EventAction_Yuremono : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    public bool EnableYuremono;

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      if (Object.op_Inequality((Object) byUniqueName, (Object) null))
      {
        foreach (Behaviour componentsInChild in (YuremonoInstance[]) ((Component) byUniqueName).get_gameObject().GetComponentsInChildren<YuremonoInstance>())
          componentsInChild.set_enabled(this.EnableYuremono);
      }
      this.ActivateNext();
    }
  }
}
