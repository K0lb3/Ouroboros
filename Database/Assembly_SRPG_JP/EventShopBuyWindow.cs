// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBuyWindow
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
  [FlowNode.Pin(104, "商品の期限切れ警告表示", FlowNode.PinTypes.Output, 104)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "アイテム選択単品", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "アイテム選択セット", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "アイテム更新", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "武具選択単品", FlowNode.PinTypes.Output, 103)]
  public class EventShopBuyWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public GameObject ItemTemplate;
    public Button BtnUpdated;
    public GameObject ObjUpdateBtn;
    public GameObject ObjUpdateTime;
    public GameObject ObjLineup;
    public GameObject ObjItemNumLimit;
    public Text TxtPossessionCoinNum;
    public GameObject ImgEventCoinType;
    public Text ShopName;
    public GameObject CoinPeriodBanner;
    public Text CoinPeriodText;
    public List<GameObject> mBuyItems;

    public EventShopBuyWindow()
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
      bool btnUpdate = GlobalVars.EventShopItem.btn_update;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ObjUpdateBtn, (UnityEngine.Object) null))
        this.ObjUpdateBtn.SetActive(btnUpdate);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ObjUpdateTime, (UnityEngine.Object) null))
        this.ObjUpdateTime.SetActive(btnUpdate);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ObjLineup, (UnityEngine.Object) null))
        this.ObjLineup.SetActive(btnUpdate);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ObjItemNumLimit, (UnityEngine.Object) null))
        this.ObjItemNumLimit.SetActive(!btnUpdate);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShopName, (UnityEngine.Object) null))
        this.ShopName.set_text(GlobalVars.EventShopItem.shops.info.title);
      bool flag = GlobalVars.EventShopItem.shops.gname.Split('-')[0] == "EventSummon2";
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CoinPeriodBanner, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CoinPeriodText, (UnityEngine.Object) null))
        return;
      if (flag && GlobalVars.NewSummonCoinInfo != null)
      {
        DateTime localTime = GameUtility.UnixtimeToLocalTime(GlobalVars.NewSummonCoinInfo.Period);
        this.CoinPeriodText.set_text(LocalizedText.Get("sys.SUMMON_COIN_PERIOD_TEXT", (object) localTime.ToString("yyyy/M/dd"), (object) localTime.ToString("H:mm")));
        this.CoinPeriodBanner.SetActive(true);
      }
      else
      {
        this.CoinPeriodText.set_text((string) null);
        this.CoinPeriodBanner.SetActive(false);
      }
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
      GlobalVars.TimeOutShopItems = (List<ShopItem>) null;
    }

    public void Activated(int pinID)
    {
      this.Refresh();
    }

    private void Refresh()
    {
      MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        return;
      this.SetPossessionCoinParam();
      EventShopData eventShopData = MonoSingleton<GameManager>.Instance.Player.GetEventShopData();
      DebugUtility.Assert(eventShopData != null, "ショップ情報が存在しない");
      for (int index = 0; index < this.mBuyItems.Count; ++index)
        this.mBuyItems[index].get_gameObject().SetActive(false);
      int count = eventShopData.items.Count;
      DateTime serverTime = TimeManager.ServerTime;
      List<EventShopItem> nearbyTimeout = new List<EventShopItem>();
      for (int index = 0; index < count; ++index)
      {
        EventShopItem data1 = eventShopData.items[index];
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
        EventShopBuyList componentInChildren = (EventShopBuyList) mBuyItem.GetComponentInChildren<EventShopBuyList>();
        componentInChildren.eventShopItem = data1;
        DataSource.Bind<EventShopItem>(mBuyItem, data1);
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

    private void ShowAndSaveTimeOutItem(List<EventShopItem> nearbyTimeout)
    {
      GlobalVars.TimeOutShopItems = new List<ShopItem>();
      if (nearbyTimeout.Count <= 0)
        return;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT))
      {
        ShopTimeOutItemInfoArray outItemInfoArray = (ShopTimeOutItemInfoArray) JsonUtility.FromJson<ShopTimeOutItemInfoArray>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, string.Empty));
        ShopTimeOutItemInfo[] shopTimeOutItemInfoArray = outItemInfoArray.Infos != null ? ((IEnumerable<ShopTimeOutItemInfo>) outItemInfoArray.Infos).Where<ShopTimeOutItemInfo>((Func<ShopTimeOutItemInfo, bool>) (t => t.ShopId == GlobalVars.EventShopItem.shops.gname)).ToArray<ShopTimeOutItemInfo>() : new ShopTimeOutItemInfo[0];
        List<EventShopItem> source1 = new List<EventShopItem>();
        List<EventShopItem> source2 = new List<EventShopItem>();
        bool flag = false;
        using (List<EventShopItem>.Enumerator enumerator = nearbyTimeout.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            EventShopItem item = enumerator.Current;
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
          JSON_ShopListArray.Shops shop = GlobalVars.EventShopItem.shops;
          IEnumerable<ShopTimeOutItemInfo> shopTimeOutItemInfos = source1.Select<EventShopItem, ShopTimeOutItemInfo>((Func<EventShopItem, ShopTimeOutItemInfo>) (target => new ShopTimeOutItemInfo(shop.gname, target.id, target.end)));
          if (outItemInfoArray.Infos != null)
            shopTimeOutItemInfos = shopTimeOutItemInfos.Concat<ShopTimeOutItemInfo>((IEnumerable<ShopTimeOutItemInfo>) outItemInfoArray.Infos);
          string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray(shopTimeOutItemInfos.ToArray<ShopTimeOutItemInfo>()));
          PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, json, false);
        }
        GlobalVars.TimeOutShopItems = source2.Cast<ShopItem>().ToList<ShopItem>();
        if (source2.Count <= 0)
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
      else
      {
        JSON_ShopListArray.Shops shop = GlobalVars.EventShopItem.shops;
        string json = JsonUtility.ToJson((object) new ShopTimeOutItemInfoArray(nearbyTimeout.Select<EventShopItem, ShopTimeOutItemInfo>((Func<EventShopItem, ShopTimeOutItemInfo>) (item => new ShopTimeOutItemInfo(shop.gname, item.id, item.end))).ToArray<ShopTimeOutItemInfo>()));
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.WARNED_EVENTSHOP_ITEM_TIMEOUT, json, false);
        GlobalVars.TimeOutShopItems = nearbyTimeout.Cast<ShopItem>().ToList<ShopItem>();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
    }

    private void OnSelect(GameObject go)
    {
      EventShopBuyList component = (EventShopBuyList) this.mBuyItems[this.mBuyItems.FindIndex((Predicate<GameObject>) (p => UnityEngine.Object.op_Equality((UnityEngine.Object) p, (UnityEngine.Object) go)))].GetComponent<EventShopBuyList>();
      GlobalVars.ShopBuyIndex = component.eventShopItem.id;
      if (component.eventShopItem.IsArtifact)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
      else if (!component.eventShopItem.IsSet)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void OnItemUpdated()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    private void SetPossessionCoinParam()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ImgEventCoinType, (UnityEngine.Object) null))
        DataSource.Bind<ItemParam>(this.ImgEventCoinType, MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.EventShopItem.shop_cost_iname));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TxtPossessionCoinNum, (UnityEngine.Object) null))
        return;
      DataSource.Bind<EventCoinData>(((Component) this.TxtPossessionCoinNum).get_gameObject(), MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals(GlobalVars.EventShopItem.shop_cost_iname))));
    }
  }
}
