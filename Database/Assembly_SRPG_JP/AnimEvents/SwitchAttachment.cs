// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.SwitchAttachment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class SwitchAttachment : AnimEvent
  {
    public SwitchAttachment.eSwitchTarget SwitchPrimaryHand;
    public SwitchAttachment.eSwitchTarget SwitchSecondaryHand;

    public override void OnStart(GameObject go)
    {
      UnitController componentInParent = (UnitController) go.GetComponentInParent<UnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      if (this.SwitchPrimaryHand != SwitchAttachment.eSwitchTarget.NO_CHANGE)
        componentInParent.SwitchAttachmentLists(UnitController.EquipmentType.PRIMARY, (int) this.SwitchPrimaryHand);
      if (this.SwitchSecondaryHand == SwitchAttachment.eSwitchTarget.NO_CHANGE)
        return;
      componentInParent.SwitchAttachmentLists(UnitController.EquipmentType.SECONDARY, (int) this.SwitchSecondaryHand);
    }

    public enum eSwitchTarget
    {
      NO_CHANGE,
      Element_0,
      Element_1,
      Element_2,
      Element_3,
      Element_4,
      Element_5,
      Element_6,
      Element_7,
      Element_8,
      Element_9,
    }
  }
}
