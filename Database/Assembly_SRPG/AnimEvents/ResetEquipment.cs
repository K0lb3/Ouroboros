// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ResetEquipment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
