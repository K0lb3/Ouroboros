// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleWeaponFrame
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleWeaponFrame : AnimEvent
  {
    public ToggleWeaponFrame.SHOW_TYPE Primary;
    public ToggleWeaponFrame.SHOW_TYPE Secondary;

    public override void OnStart(GameObject go)
    {
      UnitController componentInParent = (UnitController) go.GetComponentInParent<UnitController>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
      {
        bool visible = this.Primary != ToggleWeaponFrame.SHOW_TYPE.HIDDEN;
        componentInParent.SetPrimaryEquipmentsVisible(visible);
      }
      if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
        return;
      bool visible1 = this.Secondary != ToggleWeaponFrame.SHOW_TYPE.HIDDEN;
      componentInParent.SetSecondaryEquipmentsVisible(visible1);
    }

    public override void OnEnd(GameObject go)
    {
      UnitController componentInParent = (UnitController) go.GetComponentInParent<UnitController>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
        return;
      if (this.Primary != ToggleWeaponFrame.SHOW_TYPE.KEEP)
      {
        bool visible = this.Primary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN;
        componentInParent.SetPrimaryEquipmentsVisible(visible);
      }
      if (this.Secondary == ToggleWeaponFrame.SHOW_TYPE.KEEP)
        return;
      bool visible1 = this.Secondary == ToggleWeaponFrame.SHOW_TYPE.HIDDEN;
      componentInParent.SetSecondaryEquipmentsVisible(visible1);
    }

    public enum SHOW_TYPE
    {
      KEEP,
      HIDDEN,
      VISIBLE,
    }
  }
}
