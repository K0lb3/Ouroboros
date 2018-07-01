// Decompiled with JetBrains decompiler
// Type: SRPG.InventorySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class InventorySlot : MonoBehaviour
  {
    public static InventorySlot Active;
    public Animator StateAnimator;
    public string AnimatorBoolName;
    public GameObject Empty;
    public GameObject NonEmpty;
    public GameObject[] HideIfEmpty;
    public int Index;
    public SRPG_Button Button;

    public InventorySlot()
    {
      base.\u002Ector();
    }

    public void SetItem(ItemData item)
    {
      DataSource.Bind<ItemData>(((Component) this).get_gameObject(), item);
      bool flag = item == null;
      if (Object.op_Inequality((Object) this.Empty, (Object) null))
        this.Empty.SetActive(flag);
      if (Object.op_Inequality((Object) this.NonEmpty, (Object) null))
        this.NonEmpty.SetActive(!flag);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void Select()
    {
      InventorySlot.Active = this;
    }

    public void Update()
    {
      if (Object.op_Inequality((Object) this.StateAnimator, (Object) null) && !string.IsNullOrEmpty(this.AnimatorBoolName))
        this.StateAnimator.SetBool(this.AnimatorBoolName, Object.op_Equality((Object) InventorySlot.Active, (Object) this));
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      bool flag = false;
      if (0 <= this.Index && this.Index < player.Inventory.Length)
        flag = player.Inventory[this.Index] != null && player.Inventory[this.Index].Param != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.HideIfEmpty[index], (Object) null))
          this.HideIfEmpty[index].SetActive(flag);
      }
    }
  }
}
