// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "更新あり：イベントショップが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(104, "マルチショップが選択された", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(0, "指定イベントショップの商品を開く", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(103, "アリーナショップが選択された", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(102, "更新なし：イベントショップが選択された", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(1, "所持コイン更新", FlowNode.PinTypes.Input, 1)]
  public class EventShopList : SRPG_ListBase, IFlowInterface
  {
    [Description("リストアイテムとして使用するゲームオブジェクト")]
    public GameObject ItemTemplate;
    public GameObject ArenaShopTemplate;
    public GameObject MultiShopTemplate;

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        switch (GlobalVars.SelectionCoinListType)
        {
          case GlobalVars.CoinListSelectionType.EventShop:
            if (Object.op_Inequality((Object) GlobalVars.SelectionEventShop, (Object) null))
            {
              GlobalVars.EventShopItem = GlobalVars.SelectionEventShop;
              GlobalVars.ShopType = EShopType.Event;
              if (GlobalVars.EventShopItem.btn_update)
              {
                FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
                break;
              }
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
              break;
            }
            break;
          case GlobalVars.CoinListSelectionType.ArenaShop:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
            break;
          case GlobalVars.CoinListSelectionType.MultiShop:
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
            break;
        }
      }
      if (pinID != 1)
        return;
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    protected override void Start()
    {
      if (Object.op_Implicit((Object) this.ItemTemplate))
        this.ItemTemplate.SetActive(false);
      if (Object.op_Implicit((Object) this.ArenaShopTemplate))
        this.ArenaShopTemplate.SetActive(false);
      if (Object.op_Implicit((Object) this.MultiShopTemplate))
        this.MultiShopTemplate.SetActive(false);
      base.Start();
    }

    public void DestroyItems()
    {
      if (GlobalVars.EventShopListItems.Count <= 0)
        return;
      for (int index = GlobalVars.EventShopListItems.Count - 1; index >= 0; --index)
      {
        if (Object.op_Inequality((Object) GlobalVars.EventShopListItems[index], (Object) null))
        {
          ((ListItemEvents) ((Component) GlobalVars.EventShopListItems[index]).GetComponent<ListItemEvents>()).OnSelect = (ListItemEvents.ListItemEvent) null;
          Object.Destroy((Object) GlobalVars.EventShopListItems[index]);
        }
      }
      GlobalVars.EventShopListItems.Clear();
    }

    private void OnSelectItem(GameObject go)
    {
      EventShopListItem component = (EventShopListItem) go.GetComponent<EventShopListItem>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      GlobalVars.EventShopItem = component;
      GlobalVars.ShopType = EShopType.Event;
      if (GlobalVars.EventShopItem.btn_update)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    public void AddEventShopList(JSON_ShopListArray.Shops[] shops)
    {
      for (int index = 0; index < shops.Length; ++index)
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
        EventShopListItem component = (EventShopListItem) gameObject.GetComponent<EventShopListItem>();
        component.SetShopList(shops[index]);
        gameObject.get_transform().SetParent(((Component) this).get_transform());
        gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
        ((ListItemEvents) gameObject.GetComponent<ListItemEvents>()).OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
        gameObject.SetActive(true);
        GlobalVars.EventShopListItems.Add(component);
      }
    }

    private void OnSelectArenaItem(GameObject go)
    {
      GlobalVars.ShopType = EShopType.Arena;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }

    public void AddArenaShopList()
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ArenaShopTemplate);
      gameObject.get_transform().SetParent(((Component) this).get_transform());
      gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
      ((ListItemEvents) gameObject.GetComponent<ListItemEvents>()).OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectArenaItem);
      gameObject.SetActive(true);
    }

    private void OnSelectMultiItem(GameObject go)
    {
      GlobalVars.ShopType = EShopType.Multi;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
    }

    public void AddMultiShopList()
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.MultiShopTemplate);
      gameObject.get_transform().SetParent(((Component) this).get_transform());
      gameObject.get_transform().set_localScale(this.ItemTemplate.get_transform().get_localScale());
      ((ListItemEvents) gameObject.GetComponent<ListItemEvents>()).OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectMultiItem);
      gameObject.SetActive(true);
    }
  }
}
