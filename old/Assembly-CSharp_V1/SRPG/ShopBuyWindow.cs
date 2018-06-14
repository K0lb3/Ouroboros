// Decompiled with JetBrains decompiler
// Type: SRPG.ShopBuyWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "アイテム更新", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "アイテム選択", FlowNode.PinTypes.Output, 100)]
  public class ShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Button BtnUpdated;
    public Text TxtTitle;
    public Text TxtUpdateTime;
    public Text TxtUpdated;
    public GameObject TitleObj;
    public List<GameObject> mBuyItems;

    public ShopBuyWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (Object.op_Inequality((Object) this.BtnUpdated, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnUpdated.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnItemUpdated)));
      }
      if (Object.op_Inequality((Object) this.TxtTitle, (Object) null))
        this.TxtTitle.set_text(LocalizedText.Get("sys.SHOP_BUY_TITLE"));
      if (Object.op_Inequality((Object) this.TxtUpdateTime, (Object) null))
        this.TxtUpdateTime.set_text(LocalizedText.Get("sys.UPDATE_TIME"));
      if (Object.op_Inequality((Object) this.TxtUpdated, (Object) null))
        this.TxtUpdated.set_text(LocalizedText.Get("sys.CMD_UPDATED"));
      switch (GlobalVars.ShopType)
      {
        case EShopType.Normal:
        case EShopType.Tabi:
        case EShopType.Kimagure:
          this.TitleObj.SetActive(true);
          break;
        default:
          this.TitleObj.SetActive(false);
          break;
      }
    }

    private void Start()
    {
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Refresh()
    {
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      ShopData shopData = player.GetShopData(GlobalVars.ShopType);
      DebugUtility.Assert(shopData != null, "ショップ情報が存在しない");
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].get_gameObject().SetActive(false);
      int count = shopData.items.Count;
      for (int index = 0; index < count; ++index)
      {
        ShopItem data = shopData.items[index];
        if (index >= this.mBuyItems.Count)
        {
          GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent((Transform) this.ItemLayoutParent, false);
          this.mBuyItems.Add(gameObject);
        }
        GameObject mBuyItem = this.mBuyItems[index];
        DataSource.Bind<ShopItem>(mBuyItem, data);
        ItemData itemDataByItemId = player.FindItemDataByItemID(data.iname);
        DataSource.Bind<ItemData>(mBuyItem, itemDataByItemId);
        DataSource.Bind<ItemParam>(mBuyItem, MonoSingleton<GameManager>.Instance.GetItemParam(data.iname));
        ListItemEvents component1 = (ListItemEvents) mBuyItem.GetComponent<ListItemEvents>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Button component2 = (Button) mBuyItem.GetComponent<Button>();
        if (Object.op_Inequality((Object) component2, (Object) null))
          ((Selectable) component2).set_interactable(!data.is_soldout);
        mBuyItem.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnSelect(GameObject go)
    {
      GlobalVars.ShopBuyIndex = this.mBuyItems.FindIndex((Predicate<GameObject>) (p => Object.op_Equality((Object) p, (Object) go)));
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnItemUpdated()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
