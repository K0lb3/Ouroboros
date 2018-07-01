// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleTargetDisp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleTargetDisp : AnimEvent
  {
    private TacticsUnitController getTarget(TacticsUnitController tuc)
    {
      if (Object.op_Equality((Object) tuc, (Object) null))
        return (TacticsUnitController) null;
      List<TacticsUnitController> targetTucLists = tuc.GetTargetTucLists();
      if (targetTucLists == null || targetTucLists.Count == 0)
        return (TacticsUnitController) null;
      return targetTucLists[0];
    }

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = (TacticsUnitController) go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      TacticsUnitController target = this.getTarget(componentInParent);
      if (!Object.op_Implicit((Object) target))
        return;
      target.SetVisible(false);
    }

    public override void OnEnd(GameObject go)
    {
      TacticsUnitController componentInParent = (TacticsUnitController) go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      TacticsUnitController target = this.getTarget(componentInParent);
      if (!Object.op_Implicit((Object) target))
        return;
      target.SetVisible(true);
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      instance.OnGimmickUpdate();
    }
  }
}
