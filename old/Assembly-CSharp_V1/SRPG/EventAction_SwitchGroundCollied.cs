// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SwitchGroundCollied
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/地面判定切り替え", "指定のアクターの地面判定を切り替えます。", 5592405, 4473992)]
  public class EventAction_SwitchGroundCollied : EventAction
  {
    public bool GroundSnap = true;
    [StringIsActorList]
    public string ActorID;

    public override void OnActivate()
    {
      TacticsUnitController byUniqueName = TacticsUnitController.FindByUniqueName(this.ActorID);
      if (Object.op_Inequality((Object) byUniqueName, (Object) null))
        byUniqueName.CollideGround = this.GroundSnap;
      this.ActivateNext();
    }
  }
}
