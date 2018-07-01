// Decompiled with JetBrains decompiler
// Type: SRPG.LimitedShopBuyWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(102, "アイテム更新", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(101, "アイテム選択セット", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(100, "アイテム選択単品", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(104, "商品の期限切れ警告表示", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(103, "武具選択単品", FlowNode.PinTypes.Output, 103)]
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

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void OnDestroy()
    {
      GlobalVars.TimeOutShopItems = (List<ShopItem>) null;
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      LimitedShopData limitedShopData = MonoSingleton<GameManager>.Instance.Player.GetLimitedShopData();
      DebugUtility.Assert(limitedShopData != null, "ショップ情報が存在しない");
      this.ShopName.set_text(GlobalVars.LimitedShopItem.shops.info.title);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Updated, (UnityEngine.Object) null))
        this.Updated.SetActive(GlobalVars.LimitedShopItem.btn_update);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Update, (UnityEngine.Object) null))
        this.Update.SetActive(GlobalVars.LimitedShopItem.btn_update);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Lineup, (UnityEngine.Object) null))
        this.Lineup.SetActive(GlobalVars.LimitedShopItem.btn_update);
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].get_gameObject().SetActive(false);
      int count = limitedShopData.items.Count;
      DateTime serverTime = TimeManager.ServerTime;
      List<LimitedShopItem> nearbyTimeout = new List<LimitedShopItem>();
      for (int index = 0; index < count; ++index)
      {
        LimitedShopItem data1 = limitedShopData.items[index];
        if (data1.end != 0L)
        {
          DateTime dateTime = TimeManager.FromUnixTime(data1.end);
          if (!(serverTime >= dateTime))
          {
            if ((dateTime - serverTime).TotalHours < 1.0)
              nearbyTimeout.Add(data1);
          }
          else
            continue;
        }
        if (index >= this.mBuyItems.Count)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
          gameObject.get_transform().SetParent((Transform) this.ItemLayoutParent, false);
          this.mBuyItems.Add(gameObject);
        }
        GameObject mBuyItem = this.mBuyItems[index];
        LimitedShopBuyList componentInChildren = (LimitedShopBuyList) mBuyItem.GetComponentInChildren<LimitedShopBuyList>();
        componentInChildren.limitedShopItem = data1;
        DataSource.Bind<LimitedShopItem>(mBuyItem, data1);
        componentInChildren.amount.SetActive(!data1.IsSet);
        if (data1.IsArtifact)
        {
          ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(data1.iname);
          DataSource.Bind<ArtifactParam>(mBuyItem, artifactParam);
        }
        else if (data1.IsConceptCard)
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(data1.iname);
          componentInChildren.SetupConceptCard(cardDataForDisplay);
        }
        else if (data1.IsItem || data1.IsSet)
        {
          ItemData data2 = new ItemData();
          data2.Setup(0L, data1.iname, data1.num);
          DataSource.Bind<ItemData>(mBuyItem, data2);
          DataSource.Bind<ItemParam>(mBuyItem, MonoSingleton<GameManager>.Instance.GetItemParam(data1.iname));
        }
        else
          DebugUtility.LogError(string.Format("不明な商品タイプが設定されています (shopitem.iname({0}) => {1})", (object) data1.iname, (object) data1.ShopItemType));
        ListItemEvents component1 = (ListItemEvents) mBuyItem.GetComponent<ListItemEvents>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          component1.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Button component2 = (Button) mBuyItem.GetComponent<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          ((Selectable) component2).set_interactable(!data1.is_soldout);
        mBuyItem.SetActive(true);
      }
      this.ShowAndSaveTimeOutItem(nearbyTimeout);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void ShowAndSaveTimeOutItem(List<LimitedShopItem> nearbyTimeout)
    {
      GlobalVars.TimeOutShopItems = new List<ShopItem>();
      if (nearbyTimeout.Count <= 0)
        return;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT))
      {
        ShopTimeOutItemInfoArray outItemInfoArray = (ShopTimeOutItemInfoArray) JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, string.Empty));
        ShopTimeOutItemInfo[] shopTimeOutItemInfoArray = outItemInfoArray.Infos != null ? ((IEnumerable<ShopTimeOutItemInfo>) outItemInfoArray.Infos).Where<ShopTimeOutItemInfo>((Func<ShopTimeOutItemInfo, bool>) (t => t.ShopId == GlobalVars.LimitedShopItem.shops.gname)).ToArray<ShopTimeOutItemInfo>() : new ShopTimeOutItemInfo[0];
        List<LimitedShopItem> source1 = new List<LimitedShopItem>();
        List<LimitedShopItem> source2 = new List<LimitedShopItem>();
        bool flag = false;
        using (List<LimitedShopItem>.Enumerator enumerator = nearbyTimeout.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            LimitedShopItem item = enumerator.Current;
            ShopTimeOutItemInfo shopTimeOutItemInfo = ((IEnumerable<ShopTimeOutItemInfo>) shopTimeOutItemInfoArray).FirstOrDefault<ShopTimeOutItemInfo>((Func<ShopTimeOutItemInfo, bool>) (t => t.ItemId == item.id));
            if (shopTimeOutItemInfo != null)
            {
              if (item.end > shopTimeOutItemInfo.End)
              {
                source2.Add(item);
                shopTimeOutItemInfo.End = item.end;
                flag = true;
              }
            }
            else
            {
              source2.Add(item);
              source1.Add(item);
              flag = true;
            }
          }
        }
        if (flag)
        {
          JSON_ShopListArray.Shops shop = GlobalVars.LimitedShopItem.shops;
          IEnumerable<ShopTimeOutItemInfo> shopTimeOutItemInfos = source1.Select<LimitedShopItem, ShopTimeOutItemInfo>((Func<LimitedShopItem, ShopTimeOutItemInfo>) (target => new ShopTimeOutItemInfo(shop.gname, target.id, target.end)));
          if (outItemInfoArray.Infos != null)
            shopTimeOutItemInfos = shopTimeOutItemInfos.Concat<ShopTimeOutItemInfo>((IEnumerable<ShopTimeOutItemInfo>) outItemInfoArray.Infos);
          string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray(shopTimeOutItemInfos.ToArray<ShopTimeOutItemInfo>()));
          PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, json, false);
        }
        GlobalVars.TimeOutShopItems = source2.Cast<ShopItem>().ToList<ShopItem>();
        if (source2.Count <= 0)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
      else
      {
        JSON_ShopListArray.Shops shop = GlobalVars.LimitedShopItem.shops;
        string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray(nearbyTimeout.Select<LimitedShopItem, ShopTimeOutItemInfo>((Func<LimitedShopItem, ShopTimeOutItemInfo>) (item => new ShopTimeOutItemInfo(shop.gname, item.id, item.end))).ToArray<ShopTimeOutItemInfo>()));
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_LIMITEDSHOP_ITEM_TIMEOUT, json, false);
        GlobalVars.TimeOutShopItems = nearbyTimeout.Cast<ShopItem>().ToList<ShopItem>();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
    }

    private void OnSelect(GameObject go)
    {
      LimitedShopBuyList component = (LimitedShopBuyList) this.mBuyItems[this.mBuyItems.FindIndex((Predicate<GameObject>) (p => UnityEngine.Object.op_Equality((UnityEngine.Object) p, (UnityEngine.Object) go)))].GetComponent<LimitedShopBuyList>();
      GlobalVars.ShopBuyIndex = component.limitedShopItem.id;
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
