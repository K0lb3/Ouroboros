// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(103, "武具選択単品", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "アイテム選択単品", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "アイテム選択セット", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "アイテム更新", FlowNode.PinTypes.Output, 102)]
  public class LimitedShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Button BtnUpdated;
    public Text TxtTitle;
    public Text TxtUpdateTime;
    public Text TxtUpdated;
    public GameObject Updated;
    public GameObject Update;
    public Text ShopName;
    public List<GameObject> mBuyItems;
    public GameObject Lineup;

    public LimitedShopBuyWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BtnUpdated, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BtnUpdated.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnItemUpdated)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtTitle, (UnityEngine.Object) null))
        this.TxtTitle.set_text(LocalizedText.Get("sys.SHOP_BUY_TITLE"));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtUpdateTime, (UnityEngine.Object) null))
        this.TxtUpdateTime.set_text(LocalizedText.Get("sys.UPDATE_TIME"));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtUpdated, (UnityEngine.Object) null))
        return;
      this.TxtUpdated.set_text(LocalizedText.Get("sys.CMD_UPDATED"));
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
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      LimitedShopData limitedShopData = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
      DebugUtility.Assert(limitedShopData != null, "ショップ情報が存在しない");
      this.ShopName.set_text(LocalizedText.Get(GlobalVars.LimitedShopItem.shops.info.title));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Updated, (UnityEngine.Object) null))
        this.Updated.SetActive(GlobalVars.LimitedShopItem.btn_update);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Update, (UnityEngine.Object) null))
        this.Update.SetActive(GlobalVars.LimitedShopItem.btn_update);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Lineup, (UnityEngine.Object) null))
        this.Lineup.SetActive(GlobalVars.LimitedShopItem.btn_update);
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].get_gameObject().SetActive(false);
      int count = limitedShopData.items.Count;
      for (int index = 0; index < count; ++index)
      {
        LimitedShopItem data1 = limitedShopData.items[index];
        if (index >= this.mBuyItems.Count)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent((Transform) this.ItemLayoutParent, false);
          this.mBuyItems.Add(gameObject);
        }
        GameObject mBuyItem = this.mBuyItems[index];
        LimitedShopBuyList componentInChildren = (LimitedShopBuyList) mBuyItem.GetComponentInChildren<LimitedShopBuyList>();
        componentInChildren.limitedShopItem = data1;
        componentInChildren.amount.SetActive(!data1.IsSet);
        DataSource.Bind<LimitedShopItem>(mBuyItem, data1);
        if (data1.IsArtifact)
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data1.iname);
          DataSource.Bind<ArtifactParam>(mBuyItem, artifactParam);
        }
        else
        {
          ItemData data2 = new ItemData();
          data2.Setup(0L, data1.iname, data1.num);
          DataSource.Bind<ItemData>(mBuyItem, data2);
          DataSource.Bind<ItemParam>(mBuyItem, MonoSingleton<GameManager>.Instance.GetItemParam(data1.iname));
        }
        ListItemEvents component1 = (ListItemEvents) mBuyItem.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Button component2 = (Button) mBuyItem.GetComponent<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          ((Selectable) component2).set_interactable(!data1.is_soldout);
        mBuyItem.SetActive(true);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnSelect(GameObject go)
    {
      GlobalVars.ShopBuyIndex = this.mBuyItems.FindIndex((Predicate<GameObject>) (p => UnityEngine.Object.op_Equality((UnityEngine.Object) p, (UnityEngine.Object) go)));
      LimitedShopBuyList component = (LimitedShopBuyList) this.mBuyItems[GlobalVars.ShopBuyIndex].GetComponent<LimitedShopBuyList>();
      if (component.limitedShopItem.IsArtifact)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      else if (!component.limitedShopItem.IsSet)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnItemUpdated()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }
  }
}
