// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ResetEquipment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ResetEquipment : AnimEvent
  {
    public bool IsNoResetPrimaryHand;
    public bool IsNoResetSecondaryHand;

    public override void OnStart(GameObject go)
    {
      UnitController componentInParent = (UnitController) go.GetComponentInParent<UnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      if (!this.IsNoResetPrimaryHand)
        componentInParent.ResetEquipmentLists(UnitController.EquipmentType.PRIMARY);
      if (this.IsNoResetSecondaryHand)
        return;
      componentInParent.ResetEquipmentLists(UnitController.EquipmentType.SECONDARY);
    }
  }
}
