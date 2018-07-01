// Decompiled with JetBrains decompiler
// Type: SRPG.ElementDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ElementDropdown : Pulldown
  {
    [SerializeField]
    private Image ElementIcon;

    protected override void UpdateSelection()
    {
      if (!Object.op_Inequality((Object) this.ElementIcon, (Object) null))
        return;
      ElementDropdownItem currentSelection = this.GetCurrentSelection() as ElementDropdownItem;
      if (!Object.op_Inequality((Object) currentSelection, (Object) null))
        return;
      this.ElementIcon.set_sprite(currentSelection.IconImage.get_sprite());
    }

    protected override PulldownItem SetupPulldownItem(GameObject itemObject)
    {
      ElementDropdownItem elementDropdownItem = (ElementDropdownItem) itemObject.GetComponent<ElementDropdownItem>();
      if (Object.op_Equality((Object) elementDropdownItem, (Object) null))
        elementDropdownItem = (ElementDropdownItem) itemObject.AddComponent<ElementDropdownItem>();
      return (PulldownItem) elementDropdownItem;
    }

    public PulldownItem AddItem(string label, Sprite sprite, int value)
    {
      PulldownItem pulldownItem = this.AddItem(label, value);
      ElementDropdownItem elementDropdownItem = pulldownItem as ElementDropdownItem;
      if (Object.op_Inequality((Object) elementDropdownItem, (Object) null))
        elementDropdownItem.IconImage.set_sprite(sprite);
      return pulldownItem;
    }
  }
}
