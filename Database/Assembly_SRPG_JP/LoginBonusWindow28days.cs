// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusWindow28days
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
  [FlowNode.Pin(16, "Message Closed", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(12, "Last Day", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(13, "Taked", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(14, "詳細表示（アイテム）", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(15, "詳細表示（真理念装）", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(10, "Load Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "Take Bonus", FlowNode.PinTypes.Input, 0)]
  public class LoginBonusWindow28days : MonoBehaviour, IFlowInterface
  {
    public GameObject ItemList;
    public GameObject[] PositionList;
    [HeaderBar("▼アイコン表示用オブジェクト")]
    public ListItemEvents Item_Normal;
    public ListItemEvents Item_Taken;
    public Json_LoginBonus[] DebugItems;
    public int DebugBonusCount;
    private int mLoginBonusCount;
    public GameObject BonusParticleEffect;
    [HeaderBar("▼演出時のアイコン表示用オブジェクト")]
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
    [HeaderBar("▼3Dモデル表示用")]
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

    private string GetPopupMessage(GiftRecieveItemData item)
    {
      if (item == null)
        return (string) null;
      return LocalizedText.Get("sys.LOGBO_POPUP_MESSAGE", (object) item.name, (object) item.num);
    }

    private string GetGainLastItemMessage(GiftRecieveItemData item)
    {
      if (item == null)
        return (string) null;
      return LocalizedText.Get("sys.LOGBO_GAIN_LASTITEM", new object[1]
      {
        (object) item.name
      });
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
      GiftRecieveItemData data1 = (GiftRecieveItemData) null;
      GiftRecieveItemData data2 = (GiftRecieveItemData) null;
      GiftRecieveItemData data3 = (GiftRecieveItemData) null;
      List<GiftRecieveItemData> giftRecieveItemDataList = new List<GiftRecieveItemData>();
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
          GiftRecieveItemData data4 = new GiftRecieveItemData();
          giftRecieveItemDataList.Add(data4);
          string str = jsonLoginBonusArray[index1].iname;
          int num1 = jsonLoginBonusArray[index1].num;
          if (string.IsNullOrEmpty(str) && jsonLoginBonusArray[index1].coin > 0)
          {
            str = "$COIN";
            num1 = jsonLoginBonusArray[index1].coin;
          }
          if (!string.IsNullOrEmpty(str))
          {
            ItemParam itemParam = instance.MasterParam.GetItemParam(str, false);
            if (itemParam != null)
            {
              data4.Set(str, GiftTypes.Item, itemParam.rare, num1);
              data4.name = itemParam.name;
            }
            ConceptCardParam conceptCardParam = instance.MasterParam.GetConceptCardParam(str);
            if (conceptCardParam != null)
            {
              data4.Set(str, GiftTypes.ConceptCard, conceptCardParam.rare, num1);
              data4.name = conceptCardParam.name;
            }
            if (itemParam == null && conceptCardParam == null)
              DebugUtility.LogError(string.Format("不明な識別子が報酬として設定されています。itemID => {0}", (object) str));
            int num2 = this.mLoginBonusCount - (!this.IsConfigWindow ? 1 : 0);
            ListItemEvents listItemEvents = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>(index1 >= num2 ? (M0) this.Item_Normal : (M0) this.Item_Taken);
            listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).get_gameObject(), data4);
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
            if (num3 == this.mCurrentWeak)
              ((GiftRecieveItem) ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>()).UpdateValue();
            this.mItems.Add(listItemEvents);
          }
        }
        int index = this.mLoginBonusCount - 1;
        int mLoginBonusCount = this.mLoginBonusCount;
        if (index >= 0 && index < jsonLoginBonusArray.Length)
          data2 = giftRecieveItemDataList[index];
        if (mLoginBonusCount >= 0 && mLoginBonusCount < jsonLoginBonusArray.Length)
          data3 = giftRecieveItemDataList[mLoginBonusCount];
        data1 = DataSource.FindDataOfClass<GiftRecieveItemData>(((Component) this.mItems[this.mItems.Count - 1]).get_gameObject(), (GiftRecieveItemData) null);
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
        DataSource.Bind<GiftRecieveItemData>(this.TodayItem.get_gameObject(), data2);
        ((GiftRecieveItem) this.TodayItem.get_gameObject().GetComponentInChildren<GiftRecieveItem>()).UpdateValue();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TommorowItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<GiftRecieveItemData>(this.TommorowItem.get_gameObject(), data3);
        ((GiftRecieveItem) this.TommorowItem.get_gameObject().GetComponentInChildren<GiftRecieveItem>()).UpdateValue();
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LastItem, (UnityEngine.Object) null))
      {
        DataSource.Bind<GiftRecieveItemData>(this.LastItem.get_gameObject(), data1);
        ((GiftRecieveItem) this.LastItem.get_gameObject().GetComponentInChildren<GiftRecieveItem>()).UpdateValue();
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
          LoginBonusWindow28days.\u003CStart\u003Ec__AnonStorey35A startCAnonStorey35A = new LoginBonusWindow28days.\u003CStart\u003Ec__AnonStorey35A();
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey35A.\u003C\u003Ef__this = this;
          int index2 = index1 * 7 + 6;
          if (index2 < this.mItems.Count)
          {
            ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) this.mItems[index2]).get_gameObject(), (ItemData) null);
            DataSource.Bind<ItemData>(((Component) this.WeakToggle[index1]).get_gameObject(), dataOfClass);
          }
          // ISSUE: reference to a compiler-generated field
          startCAnonStorey35A.index = index1;
          this.WeakToggle[index1].set_isOn(index1 == this.mCurrentWeak);
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.WeakToggle[index1].onValueChanged).AddListener(new UnityAction<bool>((object) startCAnonStorey35A, __methodptr(\u003C\u003Em__35B)));
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
        this.mCurrentUnit.Setup(unit_iname, 0, 0, 0, (string) null, 1, EElement.None, 0);
        GameObject gameObject = new GameObject("Preview", new System.Type[1]
        {
          typeof (UnitPreview)
        });
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
      return (IEnumerator) new LoginBonusWindow28days.\u003CWaitLoadAsync\u003Ec__Iterator121()
      {
        \u003C\u003Ef__this = this
      };
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
      GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(((Component) mItem).get_gameObject(), (GiftRecieveItemData) null);
      ListItemEvents listItemEvents = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) this.Item_Taken);
      DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).get_gameObject(), dataOfClass);
      ((Component) listItemEvents).get_transform().SetParent(((Component) mItem).get_transform().get_parent(), false);
      ((Component) listItemEvents).get_transform().SetSiblingIndex(((Component) mItem).get_transform().GetSiblingIndex());
      listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
      GiftRecieveItem componentInChildren = (GiftRecieveItem) ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        componentInChildren.UpdateValue();
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
      GiftRecieveItemData dataOfClass = DataSource.FindDataOfClass<GiftRecieveItemData>(go, (GiftRecieveItemData) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.type == GiftTypes.Item)
      {
        GlobalVars.SelectedItemID = dataOfClass.iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 14);
      }
      else if (dataOfClass.type == GiftTypes.ConceptCard)
      {
        ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(dataOfClass.iname);
        GlobalVars.SelectedConceptCardData.Set(cardDataForDisplay);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 15);
      }
      else
        DebugUtility.LogError(string.Format("不明な種類のログインボーナスが設定されています。{0} => {1}個", (object) dataOfClass.iname, (object) dataOfClass.num));
    }

    [DebuggerHidden]
    private IEnumerator DelayPopupMessage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CDelayPopupMessage\u003Ec__Iterator122()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator WaitForDestroy()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new LoginBonusWindow28days.\u003CWaitForDestroy\u003Ec__Iterator123()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
