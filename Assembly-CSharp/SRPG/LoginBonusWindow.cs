// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Load Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(12, "Last Day", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(11, "Take Bonus", FlowNode.PinTypes.Input, 0)]
  public class LoginBonusWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemList;
    public GameObject[] PositionList;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public GameObject BonusParticleEffect;
    public GameObject TodayItem;
    public GameObject TommorowItem;
    public Text Today;
    public Text Tommorow;
    public GameObject TommorowRow;
    public GameObject VIPBonusRow;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public LoginBonusVIPBadge VIPBadge;
    public string CheckName;
    public string[] DisabledFirstDayNames;
    public string TableID;
    public string TodayTextID;
    public string TommorowTextID1;
    public string TommorowTextID2;
    public string LastDayTextID;
    private List<ListItemEvents> mItems;

    public LoginBonusWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.Item_Normal, (Object) null))
        ((Component) this.Item_Normal).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.Item_Taken, (Object) null))
        ((Component) this.Item_Taken).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.VIPBadge, (Object) null))
        ((Component) this.VIPBadge).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.TodayBadge, (Object) null))
        ((Component) this.TodayBadge).get_gameObject().SetActive(false);
      if (!Object.op_Inequality((Object) this.TommorowBadge, (Object) null))
        return;
      ((Component) this.TommorowBadge).get_gameObject().SetActive(false);
    }

    private string MakeTodayText(ItemData todaysBonusItem)
    {
      return LocalizedText.Get(string.IsNullOrEmpty(this.TodayTextID) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TODAY" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TODAY") : this.TodayTextID, (object) todaysBonusItem.Param.name, (object) todaysBonusItem.Num, (object) this.mLoginBonusCount);
    }

    private string MakeTomorrowText(ItemData todaysBonusItem, ItemData tomorrowBonusItem)
    {
      return LocalizedText.Get(todaysBonusItem == null || todaysBonusItem.Param != tomorrowBonusItem.Param ? (string.IsNullOrEmpty(this.TommorowTextID1) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TOMMOROW" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW") : this.TommorowTextID1) : (string.IsNullOrEmpty(this.TommorowTextID2) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_TOMMOROW2" : "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW2") : this.TommorowTextID2), new object[1]{ (object) tomorrowBonusItem.Param.name });
    }

    private string MakeLastText()
    {
      return LocalizedText.Get(string.IsNullOrEmpty(this.LastDayTextID) ? (string.IsNullOrEmpty(this.TableID) ? "sys.LOGBO_LAST" : "sys.LOGBO_" + this.TableID.ToUpper() + "_LAST") : this.LastDayTextID);
    }

    private void DisableFirstDayHiddenOject(GameObject parent)
    {
      if (Object.op_Equality((Object) parent, (Object) null) || this.DisabledFirstDayNames == null)
        return;
      for (int index = 0; index < this.DisabledFirstDayNames.Length; ++index)
      {
        string disabledFirstDayName = this.DisabledFirstDayNames[index];
        if (!string.IsNullOrEmpty(disabledFirstDayName))
        {
          Transform child = parent.get_transform().FindChild(disabledFirstDayName);
          if (Object.op_Inequality((Object) child, (Object) null))
            ((Component) child).get_gameObject().SetActive(false);
        }
      }
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Json_LoginBonus[] jsonLoginBonusArray = instance.Player.FindLoginBonuses(this.TableID);
      this.mLoginBonusCount = instance.Player.LoginCountWithType(this.TableID);
      ItemData itemData1 = (ItemData) null;
      ItemData itemData2 = (ItemData) null;
      bool flag1 = false;
      if (this.DebugItems != null && this.DebugItems.Length > 0)
      {
        jsonLoginBonusArray = this.DebugItems;
        this.mLoginBonusCount = this.DebugBonusCount;
      }
      if (jsonLoginBonusArray != null)
      {
        for (int index = 0; index < jsonLoginBonusArray.Length; ++index)
        {
          string key = jsonLoginBonusArray[index].iname;
          int num = jsonLoginBonusArray[index].num;
          if (string.IsNullOrEmpty(key) && jsonLoginBonusArray[index].coin > 0)
          {
            key = "$COIN";
            num = jsonLoginBonusArray[index].coin;
          }
          if (!string.IsNullOrEmpty(key))
          {
            ItemParam itemParam = instance.GetItemParam(key);
            if (itemParam != null)
            {
              LoginBonusData loginBonusData = new LoginBonusData();
              loginBonusData.DayNum = index + 1;
              if (index == this.mLoginBonusCount - 1)
              {
                itemData1 = (ItemData) loginBonusData;
                if (jsonLoginBonusArray[index].vip != null && jsonLoginBonusArray[index].vip.lv > 0)
                  flag1 = true;
                if (loginBonusData != null && loginBonusData.Param != null)
                {
                  if (loginBonusData.Param.type == EItemType.Ticket)
                    AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.SummonTicket, AnalyticsManager.CurrencySubType.FREE, (long) loginBonusData.Num, "Login Bonus", new Dictionary<string, object>()
                    {
                      {
                        "ticket_id",
                        (object) loginBonusData.ItemID
                      }
                    });
                  else
                    AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.Item, AnalyticsManager.CurrencySubType.FREE, (long) loginBonusData.Num, "Login Bonus", new Dictionary<string, object>()
                    {
                      {
                        "item_id",
                        (object) loginBonusData.ItemID
                      }
                    });
                }
                if (string.IsNullOrEmpty(key) && jsonLoginBonusArray[index].coin > 0)
                  AnalyticsManager.TrackCurrencyObtain(AnalyticsManager.CurrencyType.Gem, AnalyticsManager.CurrencySubType.FREE, (long) jsonLoginBonusArray[index].coin, "Login Bonus", (Dictionary<string, object>) null);
              }
              else if (index == this.mLoginBonusCount)
                itemData2 = (ItemData) loginBonusData;
              if (loginBonusData.Setup(0L, itemParam, num))
              {
                ListItemEvents listItemEvents1 = index >= this.mLoginBonusCount - 1 ? this.Item_Normal : this.Item_Taken;
                if (!Object.op_Equality((Object) listItemEvents1, (Object) null) && !Object.op_Equality((Object) this.ItemList, (Object) null))
                {
                  ListItemEvents listItemEvents2 = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents1);
                  DataSource.Bind<ItemData>(((Component) listItemEvents2).get_gameObject(), (ItemData) loginBonusData);
                  if (Object.op_Equality((Object) listItemEvents1, (Object) this.Item_Normal) && Object.op_Inequality((Object) this.VIPBadge, (Object) null) && (jsonLoginBonusArray[index].vip != null && jsonLoginBonusArray[index].vip.lv > 0))
                  {
                    LoginBonusVIPBadge loginBonusVipBadge = (LoginBonusVIPBadge) Object.Instantiate<LoginBonusVIPBadge>((M0) this.VIPBadge);
                    if (Object.op_Inequality((Object) loginBonusVipBadge.VIPRank, (Object) null))
                      loginBonusVipBadge.VIPRank.set_text(jsonLoginBonusArray[index].vip.lv.ToString());
                    ((Component) loginBonusVipBadge).get_transform().SetParent(((Component) listItemEvents2).get_transform(), false);
                    ((RectTransform) ((Component) loginBonusVipBadge).get_transform()).set_anchoredPosition(Vector2.get_zero());
                    ((Component) loginBonusVipBadge).get_gameObject().SetActive(true);
                  }
                  if (Object.op_Inequality((Object) this.TodayBadge, (Object) null) && index == this.mLoginBonusCount - 1)
                  {
                    ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents2).get_transform(), false);
                    this.TodayBadge.set_anchoredPosition(Vector2.get_zero());
                    ((Component) this.TodayBadge).get_gameObject().SetActive(true);
                  }
                  else if (Object.op_Inequality((Object) this.TommorowBadge, (Object) null) && index == this.mLoginBonusCount)
                  {
                    ((Transform) this.TommorowBadge).SetParent(((Component) listItemEvents2).get_transform(), false);
                    this.TommorowBadge.set_anchoredPosition(Vector2.get_zero());
                    ((Component) this.TommorowBadge).get_gameObject().SetActive(true);
                  }
                  if (index < this.mLoginBonusCount - 1)
                  {
                    Transform child = ((Component) listItemEvents2).get_transform().FindChild(this.CheckName);
                    if (Object.op_Inequality((Object) child, (Object) null))
                    {
                      Animator component = (Animator) ((Component) child).GetComponent<Animator>();
                      if (Object.op_Inequality((Object) component, (Object) null))
                        ((Behaviour) component).set_enabled(false);
                    }
                  }
                  Transform transform = this.ItemList.get_transform();
                  if (this.PositionList != null && this.PositionList.Length > index && Object.op_Inequality((Object) this.PositionList[index], (Object) null))
                    transform = this.PositionList[index].get_transform();
                  if (index == 0)
                    this.DisableFirstDayHiddenOject(((Component) listItemEvents2).get_gameObject());
                  ((Component) listItemEvents2).get_transform().SetParent(transform, false);
                  ((Component) listItemEvents2).get_gameObject().SetActive(true);
                  this.mItems.Add(listItemEvents2);
                }
              }
            }
          }
        }
      }
      bool flag2 = instance.Player.IsLastLoginBonus(this.TableID);
      if (jsonLoginBonusArray != null && this.mLoginBonusCount == jsonLoginBonusArray.Length)
        flag2 = true;
      if (Object.op_Inequality((Object) this.Today, (Object) null) && itemData1 != null && itemData1.Param != null)
        this.Today.set_text(this.MakeTodayText(itemData1));
      if (Object.op_Inequality((Object) this.TodayItem, (Object) null))
      {
        DataSource.Bind<ItemData>(this.TodayItem.get_gameObject(), itemData1);
        GameParameter.UpdateAll(this.TodayItem);
      }
      if (Object.op_Inequality((Object) this.TommorowItem, (Object) null))
      {
        DataSource.Bind<ItemData>(this.TommorowItem.get_gameObject(), itemData2);
        GameParameter.UpdateAll(this.TommorowItem);
      }
      if (Object.op_Inequality((Object) this.Tommorow, (Object) null) && !flag2 && (itemData2 != null && itemData2.Param != null))
        this.Tommorow.set_text(this.MakeTomorrowText(itemData1, itemData2));
      else if (Object.op_Inequality((Object) this.TommorowRow, (Object) null))
        this.Tommorow.set_text(this.MakeLastText());
      if (Object.op_Inequality((Object) this.VIPBonusRow, (Object) null))
        this.VIPBonusRow.SetActive(flag1);
      if (flag2)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
      this.StartCoroutine(this.WaitLoadAsync());
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow.\u003CWaitLoadAsync\u003Ec__IteratorBB() { \u003C\u003Ef__this = this };
    }

    public void Activated(int pinID)
    {
      if (pinID != 11)
        return;
      this.FlipTodaysItem();
    }

    private void FlipTodaysItem()
    {
      if (this.mLoginBonusCount < 0 || this.mItems.Count < this.mLoginBonusCount)
        return;
      int index = this.mLoginBonusCount - 1;
      ListItemEvents mItem = this.mItems[index];
      if (Object.op_Inequality((Object) this.BonusParticleEffect, (Object) null))
        UIUtility.SpawnParticle(this.BonusParticleEffect, ((Component) mItem).get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) mItem).get_gameObject(), (ItemData) null);
      ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.Item_Taken);
      DataSource.Bind<ItemData>(((Component) listItemEvents).get_gameObject(), dataOfClass);
      ((Component) listItemEvents).get_transform().SetParent(((Component) mItem).get_transform().get_parent(), false);
      ((Component) listItemEvents).get_transform().SetSiblingIndex(((Component) mItem).get_transform().GetSiblingIndex());
      if (Object.op_Inequality((Object) this.TodayBadge, (Object) null))
        ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents).get_transform(), false);
      Object.Destroy((Object) ((Component) mItem).get_gameObject());
      ((Component) listItemEvents).get_gameObject().SetActive(true);
      Transform child = ((Component) listItemEvents).get_transform().FindChild(this.CheckName);
      if (Object.op_Inequality((Object) child, (Object) null))
      {
        Animator component = (Animator) ((Component) child).GetComponent<Animator>();
        if (Object.op_Inequality((Object) component, (Object) null))
          ((Behaviour) component).set_enabled(true);
      }
      if (index == 0)
        this.DisableFirstDayHiddenOject(((Component) listItemEvents).get_gameObject());
      this.mItems[index] = listItemEvents;
    }
  }
}
