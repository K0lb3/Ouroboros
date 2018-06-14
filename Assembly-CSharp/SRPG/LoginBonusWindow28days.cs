// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindow28days
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(13, "Taked", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(12, "Last Day", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "Load Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "Take Bonus", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(16, "Message Closed", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(14, "詳細表示", FlowNode.PinTypes.Output, 4)]
  public class LoginBonusWindow28days : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemList;
    public GameObject[] PositionList;
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public GameObject BonusParticleEffect;
    public GameObject LastItem;
    public GameObject TodayItem;
    public GameObject TommorowItem;
    public RectTransform TodayBadge;
    public RectTransform TommorowBadge;
    public string CheckName;
    public string[] DisabledFirstDayNames;
    public string TableID;
    public List<Toggle> WeakToggle;
    public Text GainLastItemMessage;
    public Text PopupMessage;
    public GameObject RemainingCounter;
    public Text RemainingCount;
    public Transform PreviewParent;
    public RawImage PreviewImage;
    public Camera PreviewCamera;
    public float PreviewCameraDistance;
    public bool IsConfigWindow;
    public string DebugNotifyUnitID;
    public LoginBonusWindow Message;
    public float MessageDelay;
    private UnitData mCurrentUnit;
    private UnitPreview mCurrentPreview;
    private RenderTexture mPreviewUnitRT;
    private LoginBonusWindow mMessageWindow;
    private List<ListItemEvents> mItems;
    private int mCurrentWeak;
    private string[] mNotifyUnits;

    public LoginBonusWindow28days()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null))
        ((Component) this.Item_Normal).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Taken, (UnityEngine.Object) null))
        ((Component) this.Item_Taken).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null))
        ((Component) this.TodayBadge).get_gameObject().SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null))
        return;
      ((Component) this.TommorowBadge).get_gameObject().SetActive(false);
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
      this.mCurrentPreview = (UnitPreview) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewUnitRT, (UnityEngine.Object) null))
      {
        RenderTexture.ReleaseTemporary(this.mPreviewUnitRT);
        this.mPreviewUnitRT = (RenderTexture) null;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMessageWindow, (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mMessageWindow).get_gameObject());
      this.mMessageWindow = (LoginBonusWindow) null;
    }

    private void DisableFirstDayHiddenOject(GameObject parent)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) parent, (UnityEngine.Object) null) || this.DisabledFirstDayNames == null)
        return;
      for (int index = 0; index < this.DisabledFirstDayNames.Length; ++index)
      {
        string disabledFirstDayName = this.DisabledFirstDayNames[index];
        if (!string.IsNullOrEmpty(disabledFirstDayName))
        {
          Transform child = parent.get_transform().FindChild(disabledFirstDayName);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
            ((Component) child).get_gameObject().SetActive(false);
        }
      }
    }

    private string GetPopupMessage(ItemData item)
    {
      if (item == null)
        return (string) null;
      return LocalizedText.Get("sys.LOGBO_POPUP_MESSAGE", (object) item.Param.name, (object) item.Num);
    }

    private string GetGainLastItemMessage(ItemData item)
    {
      if (item == null)
        return (string) null;
      return LocalizedText.Get("sys.LOGBO_GAIN_LASTITEM", new object[1]{ (object) item.Param.name });
    }

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Json_LoginBonus[] jsonLoginBonusArray;
      if (this.IsConfigWindow)
      {
        jsonLoginBonusArray = instance.Player.LoginBonus28days.bonuses;
        this.mLoginBonusCount = instance.Player.LoginBonus28days.count;
        this.mNotifyUnits = instance.Player.LoginBonus28days.bonus_units;
      }
      else
      {
        jsonLoginBonusArray = instance.Player.FindLoginBonuses(this.TableID);
        this.mLoginBonusCount = instance.Player.LoginCountWithType(this.TableID);
        this.mNotifyUnits = instance.Player.GetLoginBonuseUnitIDs(this.TableID);
      }
      ItemData data1 = (ItemData) null;
      ItemData data2 = (ItemData) null;
      ItemData data3 = (ItemData) null;
      if (this.DebugItems != null && this.DebugItems.Length > 0)
      {
        jsonLoginBonusArray = this.DebugItems;
        this.mLoginBonusCount = this.DebugBonusCount;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainingCount, (UnityEngine.Object) null))
        this.RemainingCount.set_text(Math.Max(28 - this.mLoginBonusCount, 0).ToString());
      this.mCurrentWeak = Math.Max(this.mLoginBonusCount - 1, 0) / 7;
      if (jsonLoginBonusArray != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Item_Normal, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemList, (UnityEngine.Object) null))
      {
        for (int index1 = 0; index1 < jsonLoginBonusArray.Length; ++index1)
        {
          string key = jsonLoginBonusArray[index1].iname;
          int num1 = jsonLoginBonusArray[index1].num;
          if (string.IsNullOrEmpty(key) && jsonLoginBonusArray[index1].coin > 0)
          {
            key = "$COIN";
            num1 = jsonLoginBonusArray[index1].coin;
          }
          if (!string.IsNullOrEmpty(key))
          {
            ItemParam itemParam = instance.GetItemParam(key);
            if (itemParam != null)
            {
              LoginBonusData loginBonusData = new LoginBonusData();
              loginBonusData.DayNum = index1 + 1;
              if (index1 == this.mLoginBonusCount - 1)
              {
                data2 = (ItemData) loginBonusData;
                if (loginBonusData != null && loginBonusData.Param != null)
                {
                  if (loginBonusData.ItemType == EItemType.Ticket)
                    AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.SummonTicket, (long) loginBonusData.Num, "Login Bonus", loginBonusData.ItemID);
                  else
                    AnalyticsManager.TrackNonPremiumCurrencyObtain(AnalyticsManager.NonPremiumCurrencyType.Item, (long) loginBonusData.Num, "Login Bonus", loginBonusData.ItemID);
                }
                if (string.IsNullOrEmpty(key) && jsonLoginBonusArray[index1].coin > 0)
                  AnalyticsManager.TrackFreePremiumCurrencyObtain((long) jsonLoginBonusArray[index1].coin, "Login Bonus");
              }
              else if (index1 == this.mLoginBonusCount)
                data3 = (ItemData) loginBonusData;
              if (loginBonusData.Setup(0L, itemParam, num1))
              {
                int num2 = this.mLoginBonusCount - (!this.IsConfigWindow ? 1 : 0);
                ListItemEvents listItemEvents = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>(index1 >= num2 ? (M0) this.Item_Normal : (M0) this.Item_Taken);
                listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
                DataSource.Bind<ItemData>(((Component) listItemEvents).get_gameObject(), (ItemData) loginBonusData);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null) && index1 == this.mLoginBonusCount - 1)
                {
                  ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents).get_transform(), false);
                  this.TodayBadge.set_anchoredPosition(Vector2.get_zero());
                  ((Component) this.TodayBadge).get_gameObject().SetActive(true);
                }
                else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowBadge, (UnityEngine.Object) null) && index1 == this.mLoginBonusCount)
                {
                  ((Transform) this.TommorowBadge).SetParent(((Component) listItemEvents).get_transform(), false);
                  this.TommorowBadge.set_anchoredPosition(Vector2.get_zero());
                  ((Component) this.TommorowBadge).get_gameObject().SetActive(true);
                }
                if (index1 < this.mLoginBonusCount - 1)
                {
                  Transform child = ((Component) listItemEvents).get_transform().FindChild(this.CheckName);
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
                  {
                    Animator component = (Animator) ((Component) child).GetComponent<Animator>();
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
                      ((Behaviour) component).set_enabled(false);
                  }
                }
                Transform transform = this.ItemList.get_transform();
                int index2 = index1 % 7;
                if (this.PositionList != null && this.PositionList.Length > index2 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PositionList[index2], (UnityEngine.Object) null))
                  transform = this.PositionList[index2].get_transform();
                if (index1 == 0)
                  this.DisableFirstDayHiddenOject(((Component) listItemEvents).get_gameObject());
                int num3 = index1 / 7;
                ((Component) listItemEvents).get_transform().SetParent(transform, false);
                ((Component) listItemEvents).get_gameObject().SetActive(num3 == this.mCurrentWeak);
                this.mItems.Add(listItemEvents);
              }
            }
          }
        }
        data1 = DataSource.FindDataOfClass<ItemData>(((Component) this.mItems[this.mItems.Count - 1]).get_gameObject(), (ItemData) null);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PopupMessage, (UnityEngine.Object) null))
        this.PopupMessage.set_text(this.GetPopupMessage(data2));
      bool flag = instance.Player.IsLastLoginBonus(this.TableID);
      if (jsonLoginBonusArray != null && this.mLoginBonusCount == jsonLoginBonusArray.Length)
        flag = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RemainingCounter, (UnityEngine.Object) null))
        this.RemainingCounter.SetActive(!flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<ItemData>(this.TodayItem.get_gameObject(), data2);
        GameParameter.UpdateAll(this.TodayItem);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<ItemData>(this.TommorowItem.get_gameObject(), data3);
        GameParameter.UpdateAll(this.TommorowItem);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LastItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<ItemData>(this.LastItem, data1);
        GameParameter.UpdateAll(this.LastItem);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GainLastItemMessage, (UnityEngine.Object) null))
        this.GainLastItemMessage.set_text(this.GetGainLastItemMessage(data1));
      if (flag)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
      if (this.WeakToggle != null)
      {
        for (int index1 = 0; index1 < this.WeakToggle.Count; ++index1)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          LoginBonusWindow28days.\u003CStart\u003Ec__AnonStorey344 startCAnonStorey344 = new LoginBonusWindow28days.\u003CStart\u003Ec__AnonStorey344();
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey344.\u003C\u003Ef__this = this;
          int index2 = index1 * 7 + 6;
          if (index2 < this.mItems.Count)
          {
            ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) this.mItems[index2]).get_gameObject(), (ItemData) null);
            DataSource.Bind<ItemData>(((Component) this.WeakToggle[index1]).get_gameObject(), dataOfClass);
          }
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey344.index = index1;
          this.WeakToggle[index1].set_isOn(index1 == this.mCurrentWeak);
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.WeakToggle[index1].onValueChanged).AddListener(new UnityAction<bool>((object) startCAnonStorey344, __methodptr(\u003C\u003Em__39B)));
        }
      }
      string unit_iname = (string) null;
      if (this.mNotifyUnits != null && this.mNotifyUnits.Length > 0)
        unit_iname = this.mNotifyUnits[new Random().Next() % this.mNotifyUnits.Length];
      if (!string.IsNullOrEmpty(this.DebugNotifyUnitID))
        unit_iname = this.DebugNotifyUnitID;
      if (!string.IsNullOrEmpty(unit_iname))
      {
        this.mCurrentUnit = new UnitData();
        this.mCurrentUnit.Setup(unit_iname, 0, 0, 0, (string) null, 1, EElement.None);
        GameObject gameObject = new GameObject("Preview", new System.Type[1]{ typeof (UnitPreview) });
        this.mCurrentPreview = (UnitPreview) gameObject.GetComponent<UnitPreview>();
        this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
        this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
        gameObject.get_transform().SetParent(this.PreviewParent, false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreviewCamera, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreviewImage, (UnityEngine.Object) null))
        {
          int num = Mathf.FloorToInt((float) Screen.get_height() * 0.8f);
          this.mPreviewUnitRT = RenderTexture.GetTemporary(num, num, 16, (RenderTextureFormat) 7);
          this.PreviewCamera.set_targetTexture(this.mPreviewUnitRT);
          this.PreviewImage.set_texture((Texture) this.mPreviewUnitRT);
        }
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerCH, true);
      }
      this.StartCoroutine(this.WaitLoadAsync());
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CWaitLoadAsync\u003Ec__IteratorFF() { \u003C\u003Ef__this = this };
    }

    public void Activated(int pinID)
    {
      if (pinID != 11)
        return;
      this.FlipTodaysItem();
    }

    private void FlipTodaysItem()
    {
      if (this.mLoginBonusCount <= 0 || this.mItems.Count < this.mLoginBonusCount)
        return;
      int index = Math.Max(this.mLoginBonusCount - 1, 0);
      ListItemEvents mItem = this.mItems[index];
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusParticleEffect, (UnityEngine.Object) null))
        UIUtility.SpawnParticle(this.BonusParticleEffect, ((Component) mItem).get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) mItem).get_gameObject(), (ItemData) null);
      ListItemEvents listItemEvents = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) this.Item_Taken);
      DataSource.Bind<ItemData>(((Component) listItemEvents).get_gameObject(), dataOfClass);
      ((Component) listItemEvents).get_transform().SetParent(((Component) mItem).get_transform().get_parent(), false);
      ((Component) listItemEvents).get_transform().SetSiblingIndex(((Component) mItem).get_transform().GetSiblingIndex());
      listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TodayBadge, (UnityEngine.Object) null))
        ((Transform) this.TodayBadge).SetParent(((Component) listItemEvents).get_transform(), false);
      UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) mItem).get_gameObject());
      ((Component) listItemEvents).get_gameObject().SetActive(true);
      Transform child = ((Component) listItemEvents).get_transform().FindChild(this.CheckName);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
      {
        Animator component = (Animator) ((Component) child).GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          ((Behaviour) component).set_enabled(true);
      }
      if (index == 0)
        this.DisableFirstDayHiddenOject(((Component) listItemEvents).get_gameObject());
      this.mItems[index] = listItemEvents;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Message, (UnityEngine.Object) null))
        this.StartCoroutine(this.DelayPopupMessage());
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 13);
    }

    private void OnWeakSelect(GameObject go)
    {
      this.mCurrentWeak = this.WeakToggle.FindIndex((Predicate<Toggle>) (p => UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) p).get_gameObject(), (UnityEngine.Object) go)));
      if (this.WeakToggle != null)
      {
        for (int index = 0; index < this.WeakToggle.Count; ++index)
          this.WeakToggle[index].set_isOn(index == this.mCurrentWeak);
      }
      int index1 = 0;
      int num = 0;
      while (index1 < this.mItems.Count)
      {
        ListItemEvents mItem = this.mItems[index1];
        ((Component) mItem).get_gameObject().SetActive(num == this.mCurrentWeak);
        Transform child = ((Component) mItem).get_transform().FindChild(this.CheckName);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
        {
          Animator component = (Animator) ((Component) child).GetComponent<Animator>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            ((Behaviour) component).set_enabled(false);
        }
        ++index1;
        num = index1 / 7;
      }
    }

    private void OnItemSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedItemID = dataOfClass.Param.iname;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 14);
    }

    [DebuggerHidden]
    private IEnumerator DelayPopupMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CDelayPopupMessage\u003Ec__Iterator100() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator WaitForDestroy()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CWaitForDestroy\u003Ec__Iterator101() { \u003C\u003Ef__this = this };
    }
  }
}
