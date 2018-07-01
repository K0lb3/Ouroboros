// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusCustomWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class LoginBonusCustomWindow : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemList;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public LoginBonusVIPBadge VIPBadge;
    public string CheckName;
    private List<ListItemEvents> mItems;
    private ItemData mCurrentItem;
    public GameObject ItemInfo;
    public GameObject ItemBg;
    public GameObject PieceInfo;
    public GameObject PieceBg;

    public LoginBonusCustomWindow()
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

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Json_LoginBonus[] jsonLoginBonusArray = instance.Player.LoginBonus;
      this.mLoginBonusCount = instance.Player.LoginBonusCount;
      if (this.DebugItems != null && this.DebugItems.Length > 0)
      {
        jsonLoginBonusArray = this.DebugItems;
        this.mLoginBonusCount = this.DebugBonusCount;
      }
      if (jsonLoginBonusArray != null && Object.op_Inequality((Object) this.Item_Normal, (Object) null) && Object.op_Inequality((Object) this.ItemList, (Object) null))
      {
        Transform transform = this.ItemList.get_transform();
        for (int index = 0; index < jsonLoginBonusArray.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          LoginBonusCustomWindow.\u003CStart\u003Ec__AnonStorey358 startCAnonStorey358 = new LoginBonusCustomWindow.\u003CStart\u003Ec__AnonStorey358();
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey358.\u003C\u003Ef__this = this;
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
              if (loginBonusData.Setup(0L, itemParam, num))
              {
                ListItemEvents listItemEvents = index >= this.mLoginBonusCount - 1 ? this.Item_Normal : this.Item_Taken;
                // ISSUE: reference to a compiler-generated field
                startCAnonStorey358.item = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) listItemEvents);
                // ISSUE: reference to a compiler-generated field
                DataSource.Bind<ItemData>(((Component) startCAnonStorey358.item).get_gameObject(), (ItemData) loginBonusData);
                if (Object.op_Equality((Object) listItemEvents, (Object) this.Item_Normal) && Object.op_Inequality((Object) this.VIPBadge, (Object) null) && (jsonLoginBonusArray[index].vip != null && jsonLoginBonusArray[index].vip.lv > 0))
                {
                  LoginBonusVIPBadge loginBonusVipBadge = (LoginBonusVIPBadge) Object.Instantiate<LoginBonusVIPBadge>((M0) this.VIPBadge);
                  if (Object.op_Inequality((Object) loginBonusVipBadge.VIPRank, (Object) null))
                    loginBonusVipBadge.VIPRank.set_text(jsonLoginBonusArray[index].vip.lv.ToString());
                  // ISSUE: reference to a compiler-generated field
                  ((Component) loginBonusVipBadge).get_transform().SetParent(((Component) startCAnonStorey358.item).get_transform(), false);
                  ((RectTransform) ((Component) loginBonusVipBadge).get_transform()).set_anchoredPosition(Vector2.get_zero());
                  ((Component) loginBonusVipBadge).get_gameObject().SetActive(true);
                }
                if (Object.op_Inequality((Object) this.TodayBadge, (Object) null) && index == this.mLoginBonusCount - 1)
                {
                  // ISSUE: reference to a compiler-generated field
                  ((Transform) this.TodayBadge).SetParent(((Component) startCAnonStorey358.item).get_transform(), false);
                  this.TodayBadge.set_anchoredPosition(Vector2.get_zero());
                  ((Component) this.TodayBadge).get_gameObject().SetActive(true);
                }
                else if (Object.op_Inequality((Object) this.TommorowBadge, (Object) null) && index == this.mLoginBonusCount)
                {
                  // ISSUE: reference to a compiler-generated field
                  ((Transform) this.TommorowBadge).SetParent(((Component) startCAnonStorey358.item).get_transform(), false);
                  this.TommorowBadge.set_anchoredPosition(Vector2.get_zero());
                  ((Component) this.TommorowBadge).get_gameObject().SetActive(true);
                }
                if (index < this.mLoginBonusCount - 1)
                {
                  // ISSUE: reference to a compiler-generated field
                  Transform child = ((Component) startCAnonStorey358.item).get_transform().FindChild(this.CheckName);
                  if (Object.op_Inequality((Object) child, (Object) null))
                  {
                    Animator component = (Animator) ((Component) child).GetComponent<Animator>();
                    if (Object.op_Inequality((Object) component, (Object) null))
                      ((Behaviour) component).set_enabled(false);
                  }
                }
                // ISSUE: reference to a compiler-generated field
                Button component1 = (Button) ((Component) ((Component) startCAnonStorey358.item).get_transform()).GetComponent<Button>();
                if (Object.op_Inequality((Object) component1, (Object) null))
                {
                  // ISSUE: method pointer
                  ((UnityEvent) component1.get_onClick()).AddListener(new UnityAction((object) startCAnonStorey358, __methodptr(\u003C\u003Em__359)));
                }
                // ISSUE: reference to a compiler-generated field
                ((Component) startCAnonStorey358.item).get_transform().SetParent(transform, false);
                // ISSUE: reference to a compiler-generated field
                ((Component) startCAnonStorey358.item).get_gameObject().SetActive(true);
                // ISSUE: reference to a compiler-generated field
                this.mItems.Add(startCAnonStorey358.item);
              }
            }
          }
        }
      }
      this.FlipTodaysItem();
      this.StartCoroutine(this.WaitLoadAsync());
    }

    private void ShowDetailWindow(ListItemEvents item)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) item).get_gameObject(), (ItemData) null);
      if (dataOfClass == null)
        return;
      string empty = string.Empty;
      string EventName;
      if (string.IsNullOrEmpty(dataOfClass.Param.Flavor))
      {
        DataSource.Bind<ItemData>(this.PieceInfo, dataOfClass);
        GameParameter.UpdateAll(this.PieceInfo);
        EventName = "OPEN_PIECE_DETAIL";
        CanvasGroup component = (CanvasGroup) this.PieceBg.GetComponent<CanvasGroup>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.set_interactable(true);
          component.set_blocksRaycasts(true);
        }
      }
      else
      {
        DataSource.Bind<ItemData>(this.ItemInfo, dataOfClass);
        GameParameter.UpdateAll(this.ItemInfo);
        EventName = "OPEN_ITEM_DETAIL";
        CanvasGroup component = (CanvasGroup) this.ItemBg.GetComponent<CanvasGroup>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.set_interactable(true);
          component.set_blocksRaycasts(true);
        }
      }
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, EventName);
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      LoginBonusCustomWindow.\u003CWaitLoadAsync\u003Ec__Iterator11F asyncCIterator11F = new LoginBonusCustomWindow.\u003CWaitLoadAsync\u003Ec__Iterator11F();
      return (IEnumerator) asyncCIterator11F;
    }

    public void Activated(int pinID)
    {
    }

    private void FlipTodaysItem()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      LoginBonusCustomWindow.\u003CFlipTodaysItem\u003Ec__AnonStorey359 itemCAnonStorey359 = new LoginBonusCustomWindow.\u003CFlipTodaysItem\u003Ec__AnonStorey359();
      // ISSUE: reference to a compiler-generated field
      itemCAnonStorey359.\u003C\u003Ef__this = this;
      if (this.mLoginBonusCount < 0 || this.mItems.Count < this.mLoginBonusCount)
        return;
      int index = this.mLoginBonusCount - 1;
      ListItemEvents mItem = this.mItems[index];
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) mItem).get_gameObject(), (ItemData) null);
      // ISSUE: reference to a compiler-generated field
      itemCAnonStorey359.newItem = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.Item_Taken);
      // ISSUE: reference to a compiler-generated field
      DataSource.Bind<ItemData>(((Component) itemCAnonStorey359.newItem).get_gameObject(), dataOfClass);
      // ISSUE: reference to a compiler-generated field
      ((Component) itemCAnonStorey359.newItem).get_transform().SetParent(((Component) mItem).get_transform().get_parent(), false);
      // ISSUE: reference to a compiler-generated field
      ((Component) itemCAnonStorey359.newItem).get_transform().SetSiblingIndex(((Component) mItem).get_transform().GetSiblingIndex());
      // ISSUE: reference to a compiler-generated field
      Button component = (Button) ((Component) ((Component) itemCAnonStorey359.newItem).get_transform()).GetComponent<Button>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) itemCAnonStorey359, __methodptr(\u003C\u003Em__35A)));
      }
      if (Object.op_Inequality((Object) this.TodayBadge, (Object) null))
      {
        // ISSUE: reference to a compiler-generated field
        ((Transform) this.TodayBadge).SetParent(((Component) itemCAnonStorey359.newItem).get_transform(), false);
      }
      Object.Destroy((Object) ((Component) mItem).get_gameObject());
      // ISSUE: reference to a compiler-generated field
      ((Component) itemCAnonStorey359.newItem).get_gameObject().SetActive(true);
      // ISSUE: reference to a compiler-generated field
      this.mItems[index] = itemCAnonStorey359.newItem;
    }
  }
}
