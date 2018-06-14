// Decompiled with JetBrains decompiler
// Type: SRPG.ItemListWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/リスト/アイテム")]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "詳細表示", FlowNode.PinTypes.Output, 100)]
  public class ItemListWindow : SRPG_ListBase, IFlowInterface
  {
    private static List<EItemType> UsedItemTypes = new List<EItemType>() { EItemType.ExpUpPlayer, EItemType.ExpUpUnit, EItemType.ExpUpSkill, EItemType.ExpUpEquip, EItemType.ExpUpArtifact, EItemType.GoldConvert, EItemType.Ticket, EItemType.Used, EItemType.ApHeal };
    public GameObject ItemTemplate;
    public Toggle ToggleShowAll;
    public Toggle ToggleShowUsed;
    public Toggle ToggleShowEquip;
    public Toggle ToggleShowUnitPierce;
    public Toggle ToggleShowItemPierce;
    public Toggle ToggleShowMaterial;
    private ItemData SelectItem;

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.ToggleShowAll, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowAll.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowAll)));
      }
      if (Object.op_Inequality((Object) this.ToggleShowUsed, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowUsed.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowUsed)));
      }
      if (Object.op_Inequality((Object) this.ToggleShowEquip, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowEquip.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowEquip)));
      }
      if (Object.op_Inequality((Object) this.ToggleShowUnitPierce, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowUnitPierce.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowUnitPierce)));
      }
      if (Object.op_Inequality((Object) this.ToggleShowItemPierce, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ToggleShowItemPierce.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowItemPierce)));
      }
      if (!Object.op_Inequality((Object) this.ToggleShowMaterial, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<bool>) this.ToggleShowMaterial.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnShowMaterial)));
    }

    protected override void Start()
    {
      base.Start();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
      Transform transform = ((Component) this).get_transform();
      for (int index = 0; index < items.Count; ++index)
      {
        ItemData data = items[index];
        if (data.Num != 0 && data.Param.CheckCanShowInList())
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent(transform, false);
          DataSource.Bind<ItemData>(gameObject, data);
          ListItemEvents component = (ListItemEvents) gameObject.GetComponent<ListItemEvents>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
            this.AddItem(component);
          }
          gameObject.SetActive(true);
        }
      }
    }

    public void Activated(int pinID)
    {
    }

    private void OnShowAll(bool isActive)
    {
      if (!isActive)
        return;
      foreach (ListItemEvents listItemEvents in this.Items)
      {
        if (DataSource.FindDataOfClass<ItemData>(((Component) listItemEvents).get_gameObject(), (ItemData) null) != null)
          ((Component) listItemEvents).get_gameObject().SetActive(true);
      }
    }

    private void OnShowUsed(bool isActive)
    {
      if (!isActive)
        return;
      foreach (ListItemEvents listItemEvents in this.Items)
      {
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) listItemEvents).get_gameObject(), (ItemData) null);
        if (dataOfClass != null)
          ((Component) listItemEvents).get_gameObject().SetActive(ItemListWindow.UsedItemTypes.Contains(dataOfClass.ItemType));
      }
    }

    private void OnShowEquip(bool isActive)
    {
      if (!isActive)
        return;
      foreach (ListItemEvents listItemEvents in this.Items)
      {
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) listItemEvents).get_gameObject(), (ItemData) null);
        if (dataOfClass != null)
          ((Component) listItemEvents).get_gameObject().SetActive(dataOfClass.ItemType == EItemType.Equip);
      }
    }

    private void OnShowUnitPierce(bool isActive)
    {
      if (!isActive)
        return;
      foreach (ListItemEvents listItemEvents in this.Items)
      {
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) listItemEvents).get_gameObject(), (ItemData) null);
        if (dataOfClass != null)
          ((Component) listItemEvents).get_gameObject().SetActive(dataOfClass.ItemType == EItemType.UnitPiece);
      }
    }

    private void OnShowItemPierce(bool isActive)
    {
      if (!isActive)
        return;
      foreach (ListItemEvents listItemEvents in this.Items)
      {
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) listItemEvents).get_gameObject(), (ItemData) null);
        if (dataOfClass != null)
          ((Component) listItemEvents).get_gameObject().SetActive(dataOfClass.ItemType == EItemType.ItemPiece || dataOfClass.ItemType == EItemType.ItemPiecePiece || dataOfClass.ItemType == EItemType.ArtifactPiece);
      }
    }

    private void OnShowMaterial(bool isActive)
    {
      foreach (ListItemEvents listItemEvents in this.Items)
      {
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) listItemEvents).get_gameObject(), (ItemData) null);
        if (dataOfClass != null)
          ((Component) listItemEvents).get_gameObject().SetActive(dataOfClass.ItemType == EItemType.Material);
      }
    }

    private void OnSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedItemID = dataOfClass.Param.iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
