// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEquipmentSlotEvents
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [RequireComponent(typeof (Animator))]
  public class UnitEquipmentSlotEvents : ListItemEvents
  {
    [HelpBox("スロットの状態にあわせてこの数値を切り替えます。スロットが空=0、装備は持ってる=1、レベル足りない=2、装備してる=3")]
    public string StateIntName = "state";

    public UnitEquipmentSlotEvents.SlotStateTypes StateType
    {
      set
      {
        Animator component = (Animator) ((Component) this).GetComponent<Animator>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.SetInteger(this.StateIntName, (int) value);
        component.Update(1f);
      }
    }

    public enum SlotStateTypes
    {
      Empty,
      HasEquipment,
      NeedMoreLevel,
      Equipped,
      EnableCraft,
      EnableCraftNeedMoreLevel,
      None,
      EnableCommon,
      EnableCommonSoul,
      EnableCommonSoulNeedMoreLevel,
    }
  }
}
