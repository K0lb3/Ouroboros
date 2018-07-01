// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ResetAttachment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ResetAttachment : AnimEvent
  {
    public bool IsNoResetPrimaryHand;
    public bool IsNoResetSecondaryHand;

    public override void OnStart(GameObject go)
    {
      UnitController componentInParent = (UnitController) go.GetComponentInParent<UnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      if (!this.IsNoResetPrimaryHand)
        componentInParent.ResetAttachmentLists(UnitController.EquipmentType.PRIMARY);
      if (this.IsNoResetSecondaryHand)
        return;
      componentInParent.ResetAttachmentLists(UnitController.EquipmentType.SECONDARY);
    }
  }
}
