// Decompiled with JetBrains decompiler
// Type: SRPG.ElementDropdown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
