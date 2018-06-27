// Decompiled with JetBrains decompiler
// Type: SRPG.BattleInventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleInventory : MonoBehaviour
  {
    public BattleInventory.SelectEvent OnSelectItem;
    public RectTransform ListParent;
    public ListItemEvents ItemTemplate;
    public ListItemEvents EmptySlotTemplate;
    public bool DisplayEmptySlots;
    private List<ListItemEvents> mItems;
    public ItemData[] mInventory;

    public BattleInventory()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
      {
        ((Component) this.ItemTemplate).get_gameObject().SetActive(false);
        if (Object.op_Equality((Object) this.ListParent, (Object) null))
          this.ListParent = ((Component) this.ItemTemplate).get_transform().get_parent() as RectTransform;
      }
      if (Object.op_Inequality((Object) this.EmptySlotTemplate, (Object) null))
        ((Component) this.EmptySlotTemplate).get_gameObject().SetActive(false);
      this.Refresh();
    }

    public void Refresh()
    {
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null) || Object.op_Equality((Object) this.ListParent, (Object) null))
        return;
      this.mInventory = SceneBattle.Instance.Battle.mInventory;
      for (int index1 = 0; index1 < this.mInventory.Length; ++index1)
      {
        if (this.DisplayEmptySlots)
        {
          ListItemEvents listItemEvents1 = !Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) || !SceneBattle.Instance.Battle.IsMultiPlay ? (!Object.op_Inequality((Object) this.EmptySlotTemplate, (Object) null) || this.mInventory[index1] != null && this.mInventory[index1].Param != null ? this.ItemTemplate : this.EmptySlotTemplate) : this.EmptySlotTemplate;
          ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
          ((Component) listItemEvents2).get_gameObject().SetActive(true);
          ((Component) listItemEvents2).get_transform().SetParent((Transform) this.ListParent, false);
          this.mItems.Add(listItemEvents2);
          if (!Object.op_Equality((Object) listItemEvents1, (Object) this.EmptySlotTemplate))
          {
            DataSource.Bind<ItemData>(((Component) listItemEvents2).get_gameObject(), this.mInventory[index1]);
            bool flag = false;
            if (this.mInventory[index1] != null && this.mInventory[index1].Param != null && this.mInventory[index1].Num > 0)
            {
              Unit currentUnit = SceneBattle.Instance.Battle.CurrentUnit;
              if (currentUnit != null)
                flag = currentUnit.CheckEnableUseSkill(this.mInventory[index1].Skill, false);
            }
            Selectable[] componentsInChildren = (Selectable[]) ((Component) listItemEvents2).get_gameObject().GetComponentsInChildren<Selectable>(true);
            if (componentsInChildren != null)
            {
              for (int index2 = componentsInChildren.Length - 1; index2 >= 0; --index2)
                componentsInChildren[index2].set_interactable(flag);
            }
            listItemEvents2.OnSelect = (ListItemEvents.ListItemEvent) (go =>
            {
              ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
              if (dataOfClass == null || dataOfClass.Param == null || this.OnSelectItem == null)
                return;
              this.OnSelectItem(dataOfClass);
            });
          }
        }
      }
    }

    public delegate void SelectEvent(ItemData item);
  }
}
