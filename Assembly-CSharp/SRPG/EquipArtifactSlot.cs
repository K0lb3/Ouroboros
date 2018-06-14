// Decompiled with JetBrains decompiler
// Type: SRPG.EquipArtifactSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EquipArtifactSlot : GenericSlot
  {
    public SRPG_Button Lock;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.SelectButton, (Object) null))
        this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
      if (!Object.op_Inequality((Object) this.Lock, (Object) null))
        return;
      this.Lock.AddListener(new SRPG_Button.ButtonClickEvent(this.OnLockClick));
    }

    private void OnButtonClick(SRPG_Button button)
    {
      if (this.OnSelect == null || !((Selectable) button).get_interactable())
        return;
      this.OnSelect((GenericSlot) this, ((Selectable) button).get_interactable());
    }

    private void OnLockClick(SRPG_Button button)
    {
      if (this.OnSelect == null)
        return;
      this.OnSelect((GenericSlot) this, false);
    }
  }
}
