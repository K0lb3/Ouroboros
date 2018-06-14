// Decompiled with JetBrains decompiler
// Type: SRPG.PartyWindow2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(6, "画面アンロック", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(7, "AP回復アイテム", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(5, "画面ロック", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(3, "戻る", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(20, "アイテム選択開始", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(11, "ユニット選択完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "ユニット選択開始", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(4, "開く", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "進む", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(31, "サポート選択完了", FlowNode.PinTypes.Output, 31)]
  [FlowNode.Pin(30, "サポート選択開始", FlowNode.PinTypes.Output, 30)]
  [FlowNode.Pin(21, "アイテム選択完了", FlowNode.PinTypes.Output, 21)]
  public class PartyWindow2 : MonoBehaviour, IFlowInterface, ISortableList
  {
    public int MaxRaidNum;
    public int DefaultRaidNum;
    public PartyWindow2.EditPartyTypes PartyType;
    [Space(10f)]
    public GenericSlot[] UnitSlots;
    public GenericSlot GuestUnitSlot;
    public GenericSlot FriendSlot;
    public string SlotChangeTrigger;
    [Space(10f)]
    public GameObject AddMainUnitOverlay;
    public GameObject AddSubUnitOverlay;
    public GameObject AddItemOverlay;
    [Space(10f)]
    public GenericSlot[] ItemSlots;
    [Space(10f)]
    public Pulldown TeamPulldown;
    [Space(10f)]
    public UnityEngine.UI.Text TotalAtk;
    public GenericSlot LeaderSkill;
    public GenericSlot SupportSkill;
    public GameObject QuestInfo;
    public SRPG_Button QuestInfoButton;
    [StringIsResourcePath(typeof (GameObject))]
    public string QuestDetail;
    [StringIsResourcePath(typeof (GameObject))]
    public string QuestDetailMulti;
    public bool ShowQuestInfo;
    public bool ShowRaidInfo;
    public SRPG_Button ForwardButton;
    public bool ShowForwardButton;
    public SRPG_Button BackButton;
    public bool ShowBackButton;
    public GameObject NoItemText;
    public GameObject Prefab_SankaFuka;
    public float SankaFukaOpacity;
    [Space(10f)]
    public RectTransform[] ChosenUnitBadges;
    public RectTransform[] ChosenItemBadges;
    public RectTransform ChosenSupportBadge;
    [Space(10f)]
    public VirtualList UnitList;
    public ListItemEvents UnitListItem;
    public SRPG_Button UnitRemoveItem;
    public RectTransform UnitListHilit;
    public string UnitListHilitParent;
    public SRPG_Button CloseUnitList;
    public Pulldown UnitListFilter;
    public GameObject NoMatchingUnit;
    public bool AlwaysShowRemoveUnit;
    public UnityEngine.UI.Text SortModeCaption;
    public GameObject AscendingIcon;
    public GameObject DescendingIcon;
    [Space(10f)]
    public VirtualList ItemList;
    public ListItemEvents ItemListItem;
    public SRPG_Button ItemRemoveItem;
    public RectTransform ItemListHilit;
    public string ItemListHilitParent;
    public SRPG_Button CloseItemList;
    public SRPG_ToggleButton ItemFilter_All;
    public SRPG_ToggleButton ItemFilter_Offense;
    public SRPG_ToggleButton ItemFilter_Support;
    public bool AlwaysShowRemoveItem;
    [Space(10f)]
    public VirtualList SupportList;
    public ListItemEvents SupportListItem;
    public SRPG_Button SupportRemoveItem;
    public RectTransform SupportListHilit;
    public string SupportListHilitParent;
    public SRPG_Button CloseSupportList;
    public Pulldown SupportListFilter;
    [Space(10f)]
    public GameObject RaidInfo;
    public UnityEngine.UI.Text RaidTicketNum;
    public SRPG_Button Raid;
    public SRPG_Button RaidN;
    public UnityEngine.UI.Text RaidNCount;
    [StringIsResourcePath(typeof (RaidResultWindow))]
    public string RaidResultPrefab;
    public GameObject RaidSettingsTemplate;
    [Space(10f)]
    public Toggle ToggleAutoPlay;
    public Toggle ToggleTreasure;
    public Toggle ToggleNotSkill;
    [Space(10f)]
    public QuestCampaignList QuestCampaigns;
    [Space(10f)]
    public GameObject QuestUnitCond;
    public PlayerPartyTypes[] SaveJobs;
    [Space(10f)]
    public bool EnableHeroSolo;
    [Space(10f)]
    protected List<UnitData> mOwnUnits;
    private int[] mUnitSortValues;
    private List<ItemData> mOwnItems;
    private List<SupportData> mSupports;
    protected QuestParam mCurrentQuest;
    protected PartyWindow2.PartyEditData mCurrentParty;
    private UnitData mGuestUnit;
    private int mCurrentTeamIndex;
    protected PartyWindow2.EditPartyTypes mCurrentPartyType;
    private SupportData mCurrentSupport;
    private SupportData mSelectedSupport;
    protected ItemData[] mCurrentItems;
    private List<RectTransform> mUnitPoolA;
    private List<RectTransform> mUnitPoolB;
    private List<RectTransform> mItemPoolA;
    private List<RectTransform> mItemPoolB;
    private List<RectTransform> mSupportPoolA;
    private List<RectTransform> mSupportPoolB;
    protected int mSelectedSlotIndex;
    private List<PartyWindow2.PartyEditData> mTeams;
    private int mLockedPartySlots;
    private bool mSupportLocked;
    private bool mItemsLocked;
    private int mNumRaids;
    private bool mIsSaving;
    private PartyWindow2.Callback mOnPartySaveSuccess;
    private PartyWindow2.Callback mOnPartySaveFail;
    private RaidResultWindow mRaidResultWindow;
    private RaidResult mRaidResult;
    private LoadRequest mReqRaidResultWindow;
    private LoadRequest mReqQuestDetail;
    private GameUtility.UnitSortModes mUnitSortMode;
    private string[] mUnitFilter;
    private bool mReverse;
    private SRPG_ToggleButton[] mItemFilterToggles;
    private PartyWindow2.ItemFilterTypes mItemFilter;
    protected GameObject[] mSankaFukaIcons;
    private RaidSettingsWindow mRaidSettings;
    private int mMultiRaidNum;
    private bool mInitialized;
    private bool mUnitSlotSelected;

    public PartyWindow2()
    {
      base.\u002Ector();
    }

    public RaidSettingsWindow RaidSettings
    {
      get
      {
        return this.mRaidSettings;
      }
    }

    public int MultiRaidNum
    {
      get
      {
        return this.mMultiRaidNum;
      }
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CStart\u003Ec__IteratorC3() { \u003C\u003Ef__this = this };
    }

    protected virtual void SetFriendSlot()
    {
      if (!Object.op_Inequality((Object) this.FriendSlot, (Object) null))
        return;
      this.FriendSlot.OnSelect = new GenericSlot.SelectEvent(this.OnUnitSlotClick);
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
      {
        MonoSingleton<GameManager>.Instance.OnStaminaChange -= new GameManager.StaminaChangeEvent(this.OnStaminaChange);
        MonoSingleton<GameManager>.Instance.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnHomeMenuChange);
      }
      GameUtility.DestroyGameObject((Component) this.UnitListHilit);
      GameUtility.DestroyGameObject((Component) this.ItemListHilit);
      GameUtility.DestroyGameObject((Component) this.SupportListHilit);
      GameUtility.DestroyGameObjects<RectTransform>(this.ChosenUnitBadges);
      GameUtility.DestroyGameObjects<RectTransform>(this.ChosenItemBadges);
      if (Object.op_Inequality((Object) this.ChosenSupportBadge, (Object) null))
        ((Component) this.ChosenSupportBadge).get_transform().SetParent(((Component) this).get_transform(), false);
      if (Object.op_Inequality((Object) this.UnitRemoveItem, (Object) null))
        ((Component) this.UnitRemoveItem).get_transform().SetParent(((Component) this).get_transform(), false);
      if (Object.op_Inequality((Object) this.ItemRemoveItem, (Object) null))
        ((Component) this.ItemRemoveItem).get_transform().SetParent(((Component) this).get_transform(), false);
      GameUtility.DestroyGameObjects<RectTransform>(this.mItemPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mItemPoolB);
      GameUtility.DestroyGameObjects<RectTransform>(this.mUnitPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mUnitPoolB);
      GameUtility.DestroyGameObjects<RectTransform>(this.mSupportPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mSupportPoolB);
      GameUtility.DestroyGameObject((Component) this.mRaidSettings);
    }

    private void OnCloseUnitListClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void OnCloseItemListClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void OnCloseSupportListClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 31);
    }

    private void AttachAndEnable(Transform go, Transform parent, string subPath)
    {
      if (!string.IsNullOrEmpty(subPath))
      {
        Transform child = parent.FindChild(subPath);
        if (Object.op_Inequality((Object) child, (Object) null))
          parent = child;
      }
      go.SetParent(parent, false);
      ((Component) go).get_gameObject().SetActive(true);
    }

    private void MoveToOrigin(GameObject go)
    {
      RectTransform component = (RectTransform) go.GetComponent<RectTransform>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.set_anchoredPosition(Vector2.get_zero());
    }

    private void OnTeamChange(int index)
    {
      if (this.mCurrentTeamIndex == index)
        return;
      if (this.mCurrentQuest != null && this.mCurrentQuest.UseFixEditor)
      {
        int currentTeamIndex = this.mCurrentTeamIndex;
        if (this.mGuestUnit != null)
        {
          if (this.mTeams[currentTeamIndex].Units[0] == null)
            this.mTeams[currentTeamIndex].Units[0] = this.mGuestUnit;
          if (this.mTeams[index].Units[0] == this.mGuestUnit)
            this.mTeams[index].Units[0] = (UnitData) null;
        }
      }
      if (this.EnableHeroSolo && this.IsSoloEventParty(this.mCurrentQuest))
        this.KyouseiUnitPartyEdit(this.mCurrentQuest, this.mTeams[index]);
      this.mCurrentTeamIndex = index;
      this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
      for (int slotIndex = 0; slotIndex < this.mCurrentParty.PartyData.MAX_UNIT; ++slotIndex)
        this.SetPartyUnit(slotIndex, this.mTeams[this.mCurrentTeamIndex].Units[slotIndex]);
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
    }

    private void OnPostUnitListUpdate()
    {
      int num = 0;
      for (int index = 0; index < this.mCurrentParty.PartyData.MAX_UNIT; ++index)
      {
        UnitData unit = this.FindUnit(this.mCurrentParty.Units[index]);
        if (unit != null)
        {
          int itemID = this.mOwnUnits.IndexOf(unit) + 1;
          if (itemID > 0)
          {
            RectTransform rectTransform = this.UnitList.FindItem(itemID);
            if (index >= 0 && index < this.ChosenUnitBadges.Length && !Object.op_Equality((Object) this.ChosenUnitBadges[index], (Object) null))
            {
              if (Object.op_Inequality((Object) ((Transform) this.ChosenUnitBadges[index]).get_parent(), (Object) rectTransform))
              {
                ((Transform) this.ChosenUnitBadges[index]).SetParent((Transform) rectTransform, false);
                ((Component) this.ChosenUnitBadges[index]).get_gameObject().SetActive(true);
              }
              num |= 1 << index;
            }
          }
        }
      }
      for (int index = 0; index < this.ChosenUnitBadges.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.ChosenUnitBadges[index], (Object) null) && (num & 1 << index) == 0)
        {
          ((Transform) this.ChosenUnitBadges[index]).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ChosenUnitBadges[index]).get_gameObject().SetActive(false);
        }
      }
    }

    protected virtual RectTransform OnGetUnitListItem(int id, int old, RectTransform current)
    {
      if (id == 0)
      {
        if (Object.op_Equality((Object) this.UnitRemoveItem, (Object) null))
          return (RectTransform) null;
        if (Object.op_Inequality((Object) current, (Object) null) && Object.op_Inequality((Object) ((Component) this.UnitRemoveItem).GetComponent<RectTransform>(), (Object) current))
        {
          this.mUnitPoolB.Remove(current);
          this.mUnitPoolA.Add(current);
          ((Transform) current).SetParent((Transform) UIUtility.Pool, false);
        }
        return ((Component) this.UnitRemoveItem).get_transform() as RectTransform;
      }
      if (Object.op_Equality((Object) this.UnitListItem, (Object) null))
        return (RectTransform) null;
      RectTransform rectTransform;
      if (old <= 0)
      {
        if (Object.op_Inequality((Object) this.UnitRemoveItem, (Object) null) && Object.op_Equality((Object) ((Component) this.UnitRemoveItem).get_transform(), (Object) current))
          ((Component) this.UnitRemoveItem).get_transform().SetParent((Transform) UIUtility.Pool, false);
        if (this.mUnitPoolA.Count <= 0)
        {
          rectTransform = ((Component) this.CreateUnitListItem()).get_transform() as RectTransform;
          this.mUnitPoolB.Add(rectTransform);
        }
        else
        {
          rectTransform = this.mUnitPoolA[this.mUnitPoolA.Count - 1];
          this.mUnitPoolA.RemoveAt(this.mUnitPoolA.Count - 1);
          this.mUnitPoolB.Add(rectTransform);
        }
      }
      else
        rectTransform = current;
      UnitIcon component1 = (UnitIcon) ((Component) rectTransform).GetComponent<UnitIcon>();
      if (Object.op_Inequality((Object) component1, (Object) null))
      {
        if (this.mUnitSortValues != null)
          component1.SetSortValue(this.mUnitSortMode, this.mUnitSortValues[id - 1]);
        else
          component1.ClearSortValue();
      }
      DataSource.Bind<UnitData>(((Component) rectTransform).get_gameObject(), this.mOwnUnits[id - 1]);
      GameParameter.UpdateAll(((Component) rectTransform).get_gameObject());
      int num = this.mCurrentParty.IndexOf(this.mOwnUnits[id - 1]);
      if (Object.op_Inequality((Object) this.UnitListHilit, (Object) null))
      {
        if (num == this.mSelectedSlotIndex)
          this.AttachAndEnable((Transform) this.UnitListHilit, (Transform) rectTransform, this.UnitListHilitParent);
        else if (Object.op_Equality((Object) ((Transform) this.UnitListHilit).get_parent(), (Object) ((Transform) rectTransform).FindChild(this.UnitListHilitParent)))
        {
          ((Transform) this.UnitListHilit).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.UnitListHilit).get_gameObject().SetActive(false);
        }
      }
      if (this.mCurrentQuest != null)
      {
        CanvasGroup component2 = (CanvasGroup) ((Component) rectTransform).GetComponent<CanvasGroup>();
        if (Object.op_Inequality((Object) component2, (Object) null))
        {
          bool flag1 = this.mCurrentQuest.IsUnitAllowed(this.mOwnUnits[id - 1]) || num >= 0;
          string error = (string) null;
          bool flag2 = flag1 & this.mCurrentQuest.IsEntryQuestCondition(this.mOwnUnits[id - 1], ref error);
          component2.set_alpha(!flag2 ? this.SankaFukaOpacity : 1f);
          component2.set_interactable(flag2);
        }
      }
      return rectTransform;
    }

    private RectTransform OnGetSupportListItem(int id, int old, RectTransform current)
    {
      RectTransform rectTransform1 = (RectTransform) null;
      if (this.mSupports == null)
        return rectTransform1;
      if (id == 0)
      {
        if (Object.op_Inequality((Object) current, (Object) null))
        {
          this.mSupportPoolB.Remove(current);
          this.mSupportPoolA.Add(current);
          ((Transform) current).SetParent((Transform) UIUtility.Pool, false);
        }
        if (Object.op_Inequality((Object) this.SupportRemoveItem, (Object) null))
          return ((Component) this.SupportRemoveItem).get_transform() as RectTransform;
        return (RectTransform) null;
      }
      if (Object.op_Equality((Object) this.SupportListItem, (Object) null))
        return (RectTransform) null;
      RectTransform rectTransform2;
      if (old <= 0)
      {
        if (old == -1 && Object.op_Inequality((Object) this.SupportRemoveItem, (Object) null) && Object.op_Equality((Object) ((Component) this.SupportRemoveItem).get_transform(), (Object) current))
          ((Component) this.SupportRemoveItem).get_transform().SetParent((Transform) UIUtility.Pool, false);
        if (this.mSupportPoolA.Count <= 0)
        {
          rectTransform2 = ((Component) this.CreateSupportListItem()).get_transform() as RectTransform;
          this.mSupportPoolB.Add(rectTransform2);
        }
        else
        {
          rectTransform2 = this.mSupportPoolA[this.mSupportPoolA.Count - 1];
          this.mSupportPoolA.RemoveAt(this.mSupportPoolA.Count - 1);
          this.mSupportPoolB.Add(rectTransform2);
        }
      }
      else
        rectTransform2 = current;
      DataSource.Bind<SupportData>(((Component) rectTransform2).get_gameObject(), this.mSupports[id - 1]);
      DataSource.Bind<UnitData>(((Component) rectTransform2).get_gameObject(), this.mSupports[id - 1].Unit);
      ((Component) rectTransform2).get_gameObject().SetActive(true);
      GameParameter.UpdateAll(((Component) rectTransform2).get_gameObject());
      if (Object.op_Inequality((Object) this.SupportListHilit, (Object) null))
      {
        if (this.mSupports[id - 1] == this.mCurrentSupport)
          this.AttachAndEnable((Transform) this.SupportListHilit, (Transform) rectTransform2, this.SupportListHilitParent);
        else if (Object.op_Equality((Object) ((Transform) this.SupportListHilit).get_parent(), (Object) rectTransform2))
        {
          ((Transform) this.SupportListHilit).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.SupportListHilit).get_gameObject().SetActive(false);
        }
      }
      if (Object.op_Inequality((Object) this.ChosenSupportBadge, (Object) null))
      {
        if (this.mSupports[id - 1] == this.mCurrentSupport)
        {
          ((Component) this.ChosenSupportBadge).get_transform().SetParent((Transform) rectTransform2, false);
          ((Component) this.ChosenSupportBadge).get_gameObject().SetActive(true);
        }
        else if (Object.op_Equality((Object) ((Component) this.ChosenSupportBadge).get_transform().get_parent(), (Object) rectTransform2))
        {
          ((Component) this.ChosenSupportBadge).get_transform().SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ChosenSupportBadge).get_gameObject().SetActive(false);
        }
      }
      if (this.mCurrentQuest != null)
      {
        CanvasGroup component = (CanvasGroup) ((Component) rectTransform2).GetComponent<CanvasGroup>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          UnitData unit = this.mSupports[id - 1].Unit;
          bool flag1 = this.mCurrentQuest.IsUnitAllowed(unit);
          string error = (string) null;
          bool flag2 = flag1 & this.mCurrentQuest.IsEntryQuestCondition(unit, ref error);
          component.set_alpha(!flag2 ? this.SankaFukaOpacity : 1f);
          component.set_interactable(flag2);
        }
      }
      return rectTransform2;
    }

    private RectTransform OnGetItemListItem(int id, int old, RectTransform current)
    {
      if (id == 0)
      {
        if (Object.op_Inequality((Object) current, (Object) null))
        {
          this.mItemPoolB.Remove(current);
          this.mItemPoolA.Add(current);
          ((Transform) current).SetParent((Transform) UIUtility.Pool, false);
        }
        if (Object.op_Inequality((Object) this.ItemRemoveItem, (Object) null))
          return ((Component) this.ItemRemoveItem).get_transform() as RectTransform;
        return (RectTransform) null;
      }
      if (Object.op_Equality((Object) this.ItemListItem, (Object) null))
        return (RectTransform) null;
      RectTransform rectTransform;
      if (old <= 0)
      {
        if (Object.op_Inequality((Object) this.ItemRemoveItem, (Object) null) && Object.op_Equality((Object) ((Component) this.ItemRemoveItem).get_transform(), (Object) current))
          ((Component) this.ItemRemoveItem).get_transform().SetParent((Transform) UIUtility.Pool, false);
        if (this.mItemPoolA.Count <= 0)
        {
          rectTransform = ((Component) this.CreateItemListItem()).get_transform() as RectTransform;
          this.mItemPoolB.Add(rectTransform);
        }
        else
        {
          rectTransform = this.mItemPoolA[this.mItemPoolA.Count - 1];
          this.mItemPoolA.RemoveAt(this.mItemPoolA.Count - 1);
          this.mItemPoolB.Add(rectTransform);
        }
      }
      else
        rectTransform = current;
      DataSource.Bind<ItemData>(((Component) rectTransform).get_gameObject(), this.mOwnItems[id - 1]);
      ((Component) rectTransform).get_gameObject().SetActive(true);
      GameParameter.UpdateAll(((Component) rectTransform).get_gameObject());
      int index1 = Array.FindIndex<ItemData>(this.mCurrentItems, (Predicate<ItemData>) (p =>
      {
        if (p != null)
          return p.Param == this.mOwnItems[id - 1].Param;
        return false;
      }));
      if (Object.op_Inequality((Object) this.ItemListHilit, (Object) null))
      {
        if (this.mSelectedSlotIndex == index1)
          this.AttachAndEnable((Transform) this.ItemListHilit, (Transform) rectTransform, this.ItemListHilitParent);
        else if (Object.op_Equality((Object) ((Transform) this.ItemListHilit).get_parent(), (Object) ((Transform) rectTransform).FindChild(this.ItemListHilitParent)))
        {
          ((Transform) this.ItemListHilit).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ItemListHilit).get_gameObject().SetActive(false);
        }
      }
      if (index1 >= 0)
      {
        if (Object.op_Inequality((Object) this.ChosenItemBadges[index1], (Object) null))
        {
          ((Transform) this.ChosenItemBadges[index1]).SetParent((Transform) rectTransform, false);
          ((Component) this.ChosenItemBadges[index1]).get_gameObject().SetActive(true);
        }
      }
      else
      {
        for (int index2 = 0; index2 < this.ChosenItemBadges.Length; ++index2)
        {
          if (Object.op_Inequality((Object) this.ChosenItemBadges[index2], (Object) null) && Object.op_Equality((Object) ((Transform) this.ChosenItemBadges[index2]).get_parent(), (Object) rectTransform))
          {
            ((Transform) this.ChosenItemBadges[index2]).SetParent((Transform) UIUtility.Pool, false);
            ((Component) this.ChosenItemBadges[index2]).get_gameObject().SetActive(false);
            break;
          }
        }
      }
      return rectTransform;
    }

    private ListItemEvents CreateSupportListItem()
    {
      ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.SupportListItem);
      listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnSupportSelect);
      return listItemEvents;
    }

    private ListItemEvents CreateItemListItem()
    {
      ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ItemListItem);
      listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
      return listItemEvents;
    }

    private ListItemEvents CreateUnitListItem()
    {
      ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.UnitListItem);
      listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnUnitItemSelect);
      return listItemEvents;
    }

    private void OnUnitRemoveSelect(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      this.SetPartyUnit(this.mSelectedSlotIndex, (UnitData) null);
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void RefreshItemList()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003CRefreshItemList\u003Ec__AnonStorey25A listCAnonStorey25A = new PartyWindow2.\u003CRefreshItemList\u003Ec__AnonStorey25A();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey25A.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey25A.currentItem = this.mCurrentItems[this.mSelectedSlotIndex];
      this.ItemList.ClearItems();
      // ISSUE: reference to a compiler-generated field
      if (listCAnonStorey25A.currentItem != null || this.AlwaysShowRemoveItem)
        this.ItemList.AddItem(0);
      List<ItemData> itemDataList = new List<ItemData>((IEnumerable<ItemData>) this.mOwnItems);
      for (int index = 0; index < itemDataList.Count; ++index)
      {
        if (itemDataList[index].ItemType != EItemType.Used || itemDataList[index].Skill == null)
          itemDataList.RemoveAt(index--);
      }
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < this.mCurrentItems.Length; ++index)
      {
        if (this.mCurrentItems[index] != null)
        {
          ItemData itemDataByItemParam = player.FindItemDataByItemParam(this.mCurrentItems[index].Param);
          this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataByItemParam) + 1);
          itemDataList.Remove(itemDataByItemParam);
        }
      }
      switch (this.mItemFilter)
      {
        case PartyWindow2.ItemFilterTypes.All:
          for (int index = 0; index < itemDataList.Count; ++index)
          {
            if (itemDataList[index].ItemType == EItemType.Used && itemDataList[index].Skill != null)
              this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataList[index]) + 1);
          }
          break;
        case PartyWindow2.ItemFilterTypes.Offense:
          for (int index = 0; index < itemDataList.Count; ++index)
          {
            if (itemDataList[index].ItemType == EItemType.Used && itemDataList[index].Skill != null && (itemDataList[index].Skill.EffectType == SkillEffectTypes.Attack || itemDataList[index].Skill.EffectType == SkillEffectTypes.Debuff || (itemDataList[index].Skill.EffectType == SkillEffectTypes.FailCondition || itemDataList[index].Skill.EffectType == SkillEffectTypes.RateDamage)))
              this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataList[index]) + 1);
          }
          break;
        case PartyWindow2.ItemFilterTypes.Support:
          for (int index = 0; index < itemDataList.Count; ++index)
          {
            if (itemDataList[index].ItemType == EItemType.Used && itemDataList[index].Skill != null && (itemDataList[index].Skill.EffectType == SkillEffectTypes.Heal || itemDataList[index].Skill.EffectType == SkillEffectTypes.RateHeal || (itemDataList[index].Skill.EffectType == SkillEffectTypes.Buff || itemDataList[index].Skill.EffectType == SkillEffectTypes.CureCondition) || (itemDataList[index].Skill.EffectType == SkillEffectTypes.Revive || itemDataList[index].Skill.EffectType == SkillEffectTypes.GemsIncDec)))
              this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataList[index]) + 1);
          }
          break;
      }
      this.ItemList.Refresh(true);
      if (Object.op_Inequality((Object) this.ItemListHilit, (Object) null))
      {
        ((Component) this.ItemListHilit).get_gameObject().SetActive(false);
        ((Transform) this.ItemListHilit).SetParent(((Component) this).get_transform(), false);
        // ISSUE: reference to a compiler-generated field
        if (listCAnonStorey25A.currentItem != null)
        {
          // ISSUE: reference to a compiler-generated method
          int itemID = this.mOwnItems.FindIndex(new Predicate<ItemData>(listCAnonStorey25A.\u003C\u003Em__2AA)) + 1;
          if (itemID > 0)
          {
            RectTransform rectTransform = this.ItemList.FindItem(itemID);
            if (Object.op_Inequality((Object) rectTransform, (Object) null))
              this.AttachAndEnable((Transform) this.ItemListHilit, (Transform) rectTransform, this.ItemListHilitParent);
          }
        }
      }
      for (int index = 0; index < this.ChosenItemBadges.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.ChosenItemBadges[index], (Object) null))
        {
          ((Transform) this.ChosenItemBadges[index]).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ChosenItemBadges[index]).get_gameObject().SetActive(false);
        }
      }
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003CRefreshItemList\u003Ec__AnonStorey25B listCAnonStorey25B = new PartyWindow2.\u003CRefreshItemList\u003Ec__AnonStorey25B();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey25B.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (listCAnonStorey25B.i = 0; listCAnonStorey25B.i < this.ChosenItemBadges.Length; ++listCAnonStorey25B.i)
      {
        // ISSUE: reference to a compiler-generated field
        if (this.mCurrentItems[listCAnonStorey25B.i] != null)
        {
          // ISSUE: reference to a compiler-generated method
          RectTransform rectTransform = this.ItemList.FindItem(this.mOwnItems.FindIndex(new Predicate<ItemData>(listCAnonStorey25B.\u003C\u003Em__2AB)) + 1);
          if (Object.op_Inequality((Object) rectTransform, (Object) null))
          {
            // ISSUE: reference to a compiler-generated field
            ((Transform) this.ChosenItemBadges[listCAnonStorey25B.i]).SetParent((Transform) rectTransform, false);
            // ISSUE: reference to a compiler-generated field
            ((Component) this.ChosenItemBadges[listCAnonStorey25B.i]).get_gameObject().SetActive(true);
          }
        }
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
    }

    private void OnItemSlotClick(GenericSlot slot, bool interactable)
    {
      if (!interactable || !this.mInitialized)
        return;
      int num = Array.IndexOf<GenericSlot>(this.ItemSlots, slot);
      if (num < 0 || 5 <= num)
        return;
      this.mSelectedSlotIndex = num;
      this.RefreshItemList();
    }

    private void OnItemRemoveSelect(SRPG_Button button)
    {
      this.SetItemSlot(this.mSelectedSlotIndex, (ItemData) null);
      this.OnItemSlotsChange();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void OnSupportRemoveSelect(SRPG_Button button)
    {
      this.SetSupport((SupportData) null);
      if (Object.op_Inequality((Object) this.FriendSlot, (Object) null))
      {
        this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
        this.FriendSlot.SetSlotData<UnitData>((UnitData) null);
      }
      this.OnPartyMemberChange();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 31);
    }

    private void OnSupportSelect(GameObject go)
    {
      this.mSelectedSupport = DataSource.FindDataOfClass<SupportData>(go, (SupportData) null);
      if (this.mCurrentSupport == this.mSelectedSupport)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 31);
      else
        UIUtility.ConfirmBox(LocalizedText.Get(this.mSelectedSupport.GetCost() <= 0 ? "sys.SUPPORT_CONFIRM1" : "sys.SUPPORT_CONFIRM2", (object) this.mSelectedSupport.PlayerName, (object) this.mSelectedSupport.GetCost()), (string) null, new UIUtility.DialogResultEvent(this.OnAcceptSupport), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
    }

    private void OnAcceptSupport(GameObject go)
    {
      if (MonoSingleton<GameManager>.Instance.Player.Gold < this.mSelectedSupport.GetCost())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.SUPPORT_NOGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.SetSupport(this.mSelectedSupport);
        if (this.mSelectedSupport.GetCost() > 0)
          AnalyticsManager.TrackCurrencyUse(AnalyticsManager.CurrencyType.Zeni, AnalyticsManager.CurrencySubType.FREE, (long) this.mSelectedSupport.GetCost(), "Hire Mercenary", (Dictionary<string, object>) null);
        this.OnPartyMemberChange();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 31);
      }
    }

    private void OnItemSelect(GameObject go)
    {
      int selectedSlotIndex = this.mSelectedSlotIndex;
      ItemData mCurrentItem = this.mCurrentItems[this.mSelectedSlotIndex];
      while (0 < selectedSlotIndex && this.mCurrentItems[selectedSlotIndex - 1] == null)
        --selectedSlotIndex;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass != null && dataOfClass != mCurrentItem)
      {
        int slotIndex = -1;
        for (int index = 0; index < this.mCurrentItems.Length; ++index)
        {
          if (this.mCurrentItems[index] != null && this.mCurrentItems[index].Param == dataOfClass.Param)
          {
            slotIndex = index;
            break;
          }
        }
        if (slotIndex >= 0)
        {
          if (mCurrentItem != null)
          {
            this.SetItemSlot(selectedSlotIndex, dataOfClass);
            this.SetItemSlot(slotIndex, mCurrentItem);
          }
        }
        else
          this.SetItemSlot(selectedSlotIndex, dataOfClass);
      }
      this.OnItemSlotsChange();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void OnUnitItemSelect(GameObject go)
    {
      int selectedSlotIndex = this.mSelectedSlotIndex;
      UnitData unit = this.mCurrentParty.Units[this.mSelectedSlotIndex];
      if (selectedSlotIndex < this.mCurrentParty.PartyData.MAX_MAINMEMBER)
      {
        while (0 < selectedSlotIndex && this.mCurrentParty.Units[selectedSlotIndex - 1] == null)
          --selectedSlotIndex;
      }
      else
      {
        while (this.mCurrentParty.PartyData.MAX_MAINMEMBER < selectedSlotIndex && this.mCurrentParty.Units[selectedSlotIndex - 1] == null)
          --selectedSlotIndex;
      }
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(go, (UnitData) null);
      if (dataOfClass != null && dataOfClass != unit)
      {
        int slotIndex = this.mCurrentParty.IndexOf(dataOfClass);
        if (slotIndex >= 0 && selectedSlotIndex != slotIndex)
        {
          this.SetPartyUnit(selectedSlotIndex, dataOfClass);
          this.SetPartyUnit(slotIndex, unit);
        }
        else
          this.SetPartyUnit(selectedSlotIndex, dataOfClass);
      }
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }

    private void OnPartyMemberChange()
    {
      this.RefreshSankaStates();
      this.RecalcTotalAttack();
      this.UpdateLeaderSkills();
      if (Object.op_Inequality((Object) this.AddMainUnitOverlay, (Object) null))
      {
        this.AddMainUnitOverlay.SetActive(false);
        for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
        {
          if (this.mCurrentParty.Units[mainmemberStart] == null && mainmemberStart < this.UnitSlots.Length && (Object.op_Inequality((Object) this.UnitSlots[mainmemberStart], (Object) null) && (this.mLockedPartySlots & 1 << mainmemberStart) == 0))
          {
            this.AddMainUnitOverlay.get_transform().SetParent(((Component) this.UnitSlots[mainmemberStart]).get_transform(), false);
            this.AddMainUnitOverlay.SetActive(true);
            break;
          }
        }
      }
      if (!Object.op_Inequality((Object) this.AddSubUnitOverlay, (Object) null))
        return;
      this.AddSubUnitOverlay.SetActive(false);
      for (int submemberStart = this.mCurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.mCurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
      {
        if (this.mCurrentParty.Units[submemberStart] == null && submemberStart < this.UnitSlots.Length && (Object.op_Inequality((Object) this.UnitSlots[submemberStart], (Object) null) && (this.mLockedPartySlots & 1 << submemberStart) == 0))
        {
          this.AddSubUnitOverlay.get_transform().SetParent(((Component) this.UnitSlots[submemberStart]).get_transform(), false);
          this.AddSubUnitOverlay.SetActive(true);
          break;
        }
      }
    }

    protected virtual void OnItemSlotsChange()
    {
      if (!Object.op_Inequality((Object) this.AddItemOverlay, (Object) null))
        return;
      this.AddItemOverlay.SetActive(false);
      for (int index = 0; index < this.mCurrentItems.Length; ++index)
      {
        if (this.mCurrentItems[index] == null && index < this.ItemSlots.Length && (Object.op_Inequality((Object) this.ItemSlots[index], (Object) null) && !this.mItemsLocked))
        {
          this.AddItemOverlay.get_transform().SetParent(((Component) this.ItemSlots[index]).get_transform(), false);
          this.AddItemOverlay.SetActive(true);
          break;
        }
      }
    }

    private void UpdateLeaderSkills()
    {
      if (Object.op_Inequality((Object) this.LeaderSkill, (Object) null))
      {
        SkillParam data = (SkillParam) null;
        if (this.mCurrentParty.Units[0] != null)
        {
          if (this.mCurrentParty.Units[0].LeaderSkill != null)
            data = this.mCurrentParty.Units[0].LeaderSkill.SkillParam;
        }
        else if (this.EnableHeroSolo && this.mGuestUnit != null && this.mGuestUnit.LeaderSkill != null)
          data = this.mGuestUnit.LeaderSkill.SkillParam;
        this.LeaderSkill.SetSlotData<SkillParam>(data);
      }
      if (!Object.op_Inequality((Object) this.SupportSkill, (Object) null))
        return;
      SkillParam data1 = (SkillParam) null;
      if (this.mCurrentSupport != null && this.mCurrentSupport.Unit.LeaderSkill != null)
        data1 = this.mCurrentSupport.Unit.LeaderSkill.SkillParam;
      this.SupportSkill.SetSlotData<SkillParam>(data1);
    }

    private void OnSlotChange(GameObject go)
    {
      if (!Object.op_Inequality((Object) go, (Object) null) || string.IsNullOrEmpty(this.SlotChangeTrigger))
        return;
      Animator component = (Animator) go.GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetTrigger(this.SlotChangeTrigger);
    }

    private void SetSupport(SupportData support)
    {
      this.mCurrentSupport = support;
      if (!Object.op_Inequality((Object) this.FriendSlot, (Object) null))
        return;
      this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
      this.FriendSlot.SetSlotData<SupportData>(support);
      this.OnSlotChange(((Component) this.FriendSlot).get_gameObject());
    }

    protected virtual void SetItemSlot(int slotIndex, ItemData item)
    {
      if (item == null)
      {
        for (int index = slotIndex; index < 4; ++index)
        {
          this.mCurrentItems[index] = this.mCurrentItems[index + 1];
          this.ItemSlots[index].SetSlotData<ItemData>(this.mCurrentItems[index]);
        }
        int index1 = this.mCurrentItems.Length - 1;
        this.mCurrentItems[index1] = (ItemData) null;
        this.ItemSlots[index1].SetSlotData<ItemData>(this.mCurrentItems[index1]);
      }
      else
      {
        int num = Math.Min(item.Num, (int) item.Param.invcap);
        ItemData data = new ItemData();
        data.Setup(item.UniqueID, item.Param, num);
        this.mCurrentItems[slotIndex] = data;
        this.ItemSlots[slotIndex].SetSlotData<ItemData>(data);
        this.OnSlotChange(((Component) this.ItemSlots[slotIndex]).get_gameObject());
      }
    }

    private void SetPartyUnit(int slotIndex, UnitData unit)
    {
      if (unit == null)
      {
        int num = slotIndex >= this.mCurrentParty.PartyData.MAX_MAINMEMBER ? this.mCurrentParty.PartyData.MAX_UNIT : this.mCurrentParty.PartyData.MAX_MAINMEMBER;
        for (int index = slotIndex; index < num - 1; ++index)
        {
          this.mCurrentParty.Units[index] = this.mCurrentParty.Units[index + 1];
          this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
          this.UnitSlots[index].SetSlotData<UnitData>(this.mCurrentParty.Units[index]);
        }
        this.mCurrentParty.Units[num - 1] = (UnitData) null;
        this.UnitSlots[num - 1].SetSlotData<QuestParam>(this.mCurrentQuest);
        this.UnitSlots[num - 1].SetSlotData<UnitData>((UnitData) null);
      }
      else
      {
        this.mCurrentParty.Units[slotIndex] = unit;
        this.UnitSlots[slotIndex].SetSlotData<QuestParam>(this.mCurrentQuest);
        this.UnitSlots[slotIndex].SetSlotData<UnitData>(unit);
        this.RefreshSankaStates();
        this.OnSlotChange(((Component) this.UnitSlots[slotIndex]).get_gameObject());
      }
    }

    private bool OnHomeMenuChange()
    {
      if (this.IsPartyDirty)
      {
        if (!this.mIsSaving)
          this.SaveParty((PartyWindow2.Callback) null, (PartyWindow2.Callback) null);
        MonoSingleton<GameManager>.Instance.RegisterImportantJob(this.StartCoroutine(this.WaitForSave()));
      }
      this.SaveInventory();
      return true;
    }

    [DebuggerHidden]
    private IEnumerator PopulateUnitList()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CPopulateUnitList\u003Ec__IteratorC4() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator PopulateItemList()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CPopulateItemList\u003Ec__IteratorC5() { \u003C\u003Ef__this = this };
    }

    private void RecalcTotalAttack()
    {
      int num = 0;
      for (int index = 0; index < this.mCurrentParty.Units.Length; ++index)
      {
        UnitData unit = this.FindUnit(this.mCurrentParty.Units[index]);
        if (unit != null && (this.mLockedPartySlots & 1 << index) == 0)
          num = num + (int) unit.Status.param.atk + (int) unit.Status.param.mag;
      }
      if (this.mCurrentSupport != null && this.mCurrentSupport.Unit != null && !this.mSupportLocked)
        num = num + (int) this.mCurrentSupport.Unit.Status.param.atk + (int) this.mCurrentSupport.Unit.Status.param.mag;
      if (this.mGuestUnit != null)
        num = num + (int) this.mGuestUnit.Status.param.atk + (int) this.mGuestUnit.Status.param.mag;
      if (!Object.op_Inequality((Object) this.TotalAtk, (Object) null))
        return;
      this.TotalAtk.set_text(num.ToString());
    }

    protected void OnUnitSlotClick(GenericSlot slot, bool interactable)
    {
      if (Object.op_Equality((Object) this.UnitList, (Object) null) || !this.mInitialized || this.mIsSaving)
        return;
      this.mUnitSlotSelected = true;
      int num = Array.IndexOf<GenericSlot>(this.UnitSlots, slot);
      if (0 <= num && num < this.mCurrentParty.PartyData.MAX_UNIT)
      {
        this.mSelectedSlotIndex = num;
        if (Object.op_Inequality((Object) this.UnitRemoveItem, (Object) null))
          ((Selectable) this.UnitRemoveItem).set_interactable(num > 0);
        this.RefreshUnitList();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
      else
      {
        if (!Object.op_Equality((Object) this.FriendSlot, (Object) slot) || !Object.op_Inequality((Object) this.SupportList, (Object) null))
          return;
        if (this.mSupports == null)
          this.getSupporterList();
        else
          this.entrySupporterList();
      }
    }

    private void getSupporterList()
    {
      Network.RequestAPI((WebAPI) new ReqSupporter((Network.ResponseCallback) (www =>
      {
        if (FlowNode_Network.HasCommonError(www))
          return;
        if (Network.IsError)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          WebAPI.JSON_BodyResponse<PartyWindow2.Json_ReqSupporterResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<PartyWindow2.Json_ReqSupporterResponse>>(www.text);
          if (jsonObject.body == null)
          {
            FlowNode_Network.Retry();
          }
          else
          {
            try
            {
              MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.supports);
            }
            catch (Exception ex)
            {
              DebugUtility.LogException(ex);
              FlowNode_Network.Failed();
              return;
            }
            Network.RemoveAPI();
            this.mSupports = new List<SupportData>((IEnumerable<SupportData>) MonoSingleton<GameManager>.Instance.Player.Supports);
            this.entrySupporterList();
          }
        }
      })), false);
    }

    private void entrySupporterList()
    {
      List<SupportData> supportDataList1 = new List<SupportData>((IEnumerable<SupportData>) this.mSupports);
      this.SupportList.ClearItems();
      if (this.AlwaysShowRemoveUnit || this.mCurrentSupport != null)
        this.SupportList.AddItem(0);
      if (this.mCurrentSupport != null)
      {
        this.SupportList.AddItem(this.mSupports.IndexOf(this.mCurrentSupport) + 1);
        supportDataList1.Remove(this.mCurrentSupport);
      }
      int num;
      for (int index1 = 0; index1 < supportDataList1.Count; index1 = num + 1)
      {
        this.SupportList.AddItem(this.mSupports.IndexOf(supportDataList1[index1]) + 1);
        List<SupportData> supportDataList2 = supportDataList1;
        int index2 = index1;
        num = index2 - 1;
        supportDataList2.RemoveAt(index2);
      }
      this.SupportList.Refresh(true);
      this.SupportList.ForceUpdateItems();
      if (Object.op_Inequality((Object) this.SupportListHilit, (Object) null))
      {
        ((Component) this.SupportListHilit).get_gameObject().SetActive(false);
        ((Transform) this.SupportListHilit).SetParent(((Component) this).get_transform(), false);
        if (this.mCurrentSupport != null)
        {
          int itemID = this.mSupports.IndexOf(this.mCurrentSupport) + 1;
          if (itemID > 0)
          {
            RectTransform rectTransform = this.SupportList.FindItem(itemID);
            if (Object.op_Inequality((Object) rectTransform, (Object) null))
              this.AttachAndEnable((Transform) this.SupportListHilit, (Transform) rectTransform, this.SupportListHilitParent);
          }
        }
      }
      if (Object.op_Inequality((Object) this.ChosenSupportBadge, (Object) null))
      {
        ((Transform) this.ChosenSupportBadge).SetParent((Transform) UIUtility.Pool, false);
        if (this.mCurrentSupport != null)
        {
          RectTransform rectTransform = this.SupportList.FindItem(this.mSupports.IndexOf(this.mCurrentSupport) + 1);
          if (Object.op_Inequality((Object) rectTransform, (Object) null))
          {
            ((Transform) this.ChosenSupportBadge).SetParent((Transform) rectTransform, false);
            ((Component) this.ChosenSupportBadge).get_gameObject().SetActive(true);
          }
        }
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 30);
    }

    private bool IsPartyDirty
    {
      get
      {
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(this.mCurrentPartyType.ToPlayerPartyType());
        for (int index = 0; index < partyOfType.MAX_UNIT; ++index)
        {
          long num = this.mCurrentParty.Units[index] == null ? 0L : this.mCurrentParty.Units[index].UniqueID;
          if (partyOfType.GetUnitUniqueID(index) != num)
            return true;
        }
        return false;
      }
    }

    private void OnRaidClick(SRPG_Button button)
    {
      if (this.mCurrentQuest == null || this.mCurrentQuest.GetChallangeLimit() > 0 && this.mCurrentQuest.GetChallangeLimit() <= this.mCurrentQuest.GetChallangeCount())
        return;
      if (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentQuest.ticket) <= 0)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket);
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.NO_RAID_TICKET", new object[1]
        {
          itemParam == null ? (object) (string) null : (object) itemParam.name
        }), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
      }
      else if (Object.op_Equality((Object) button, (Object) this.Raid))
        this.PrepareRaid(1, !((Selectable) button).IsInteractable(), false);
      else
        this.ShowRaidSettings();
    }

    private void OnRaidAccept(GameObject go)
    {
      if (this.mNumRaids <= 0)
        return;
      if (Object.op_Inequality((Object) this.mRaidSettings, (Object) null))
      {
        this.mRaidSettings.Close();
        this.mRaidSettings = (RaidSettingsWindow) null;
      }
      this.LockWindow(true);
      CriticalSection.Enter(CriticalSections.Network);
      if (Object.op_Equality((Object) this.mRaidResultWindow, (Object) null) && this.mReqRaidResultWindow == null)
        this.mReqRaidResultWindow = AssetManager.LoadAsync<RaidResultWindow>(this.RaidResultPrefab);
      if (this.IsPartyDirty)
        this.SaveParty((PartyWindow2.Callback) (() => this.StartRaid()), (PartyWindow2.Callback) null);
      else
        this.StartRaid();
    }

    private void StartRaid()
    {
      for (int index = 0; index < this.mNumRaids; ++index)
        MonoSingleton<GameManager>.Instance.Player.IncrementQuestChallangeNumDaily(this.mCurrentQuest.iname);
      if (Network.Mode == Network.EConnectMode.Online)
      {
        Network.RequestAPI((WebAPI) new ReqBtlComRaid(this.mCurrentQuest.iname, this.mNumRaids, new Network.ResponseCallback(this.RecvRaidResult), 0), false);
      }
      else
      {
        this.LockWindow(false);
        CriticalSection.Leave(CriticalSections.Network);
      }
    }

    private void RecvRaidResult(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        FlowNode_Network.Failed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComRaidResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComRaidResponse>>(www.text);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        PlayerData player = instance.Player;
        int exp = player.Exp;
        int lv = player.Lv;
        if (this.mRaidResult == null)
          this.mRaidResult = new RaidResult(this.mCurrentPartyType.ToPlayerPartyType());
        this.mRaidResult.quest = this.mCurrentQuest;
        this.mRaidResult.members.Clear();
        this.mRaidResult.results.Clear();
        for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
        {
          long iid = this.mCurrentParty.Units[mainmemberStart] == null ? 0L : this.mCurrentParty.Units[mainmemberStart].UniqueID;
          if (iid > 0L)
          {
            UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(iid);
            if (unitDataByUniqueId != null)
            {
              UnitData unitData = new UnitData();
              unitData.Setup(unitDataByUniqueId);
              this.mRaidResult.members.Add(unitData);
            }
          }
        }
        if (this.mCurrentQuest.units != null)
        {
          for (int index = 0; index < this.mCurrentQuest.units.Length; ++index)
          {
            UnitData unitDataByUnitId = instance.Player.FindUnitDataByUnitID(this.mCurrentQuest.units[index]);
            if (unitDataByUnitId != null)
            {
              UnitData unitData = new UnitData();
              unitData.Setup(unitDataByUnitId);
              this.mRaidResult.members.Add(unitData);
            }
          }
        }
        for (int submemberStart = this.mCurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.mCurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
        {
          long iid = this.mCurrentParty.Units[submemberStart] == null ? 0L : this.mCurrentParty.Units[submemberStart].UniqueID;
          if (iid > 0L)
          {
            UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(iid);
            if (unitDataByUniqueId != null)
            {
              UnitData unitData = new UnitData();
              unitData.Setup(unitDataByUniqueId);
              this.mRaidResult.members.Add(unitData);
            }
          }
        }
        try
        {
          instance.Deserialize(jsonObject.body.player);
          instance.Deserialize(jsonObject.body.items);
          instance.Deserialize(jsonObject.body.units);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        if (jsonObject.body.btlinfos != null)
        {
          for (int index1 = 0; index1 < jsonObject.body.btlinfos.Length; ++index1)
          {
            BattleCore.Json_BtlInfo btlinfo = jsonObject.body.btlinfos[index1];
            RaidQuestResult raidQuestResult = new RaidQuestResult();
            raidQuestResult.index = index1;
            raidQuestResult.pexp = (int) this.mCurrentQuest.pexp;
            raidQuestResult.uexp = (int) this.mCurrentQuest.uexp;
            raidQuestResult.gold = (int) this.mCurrentQuest.gold;
            raidQuestResult.drops = new ItemData[btlinfo.drops.Length];
            for (int index2 = 0; index2 < btlinfo.drops.Length; ++index2)
            {
              if (!string.IsNullOrEmpty(btlinfo.drops[index2].iname))
              {
                raidQuestResult.drops[index2] = new ItemData();
                raidQuestResult.drops[index2].Setup((long) index2, btlinfo.drops[index2].iname, btlinfo.drops[index2].num);
              }
            }
            this.mRaidResult.campaignIds = btlinfo.campaigns;
            this.mRaidResult.results.Add(raidQuestResult);
          }
        }
        if (player.Lv > lv)
          player.OnPlayerLevelChange(player.Lv - lv);
        for (int index = 0; index < jsonObject.body.btlinfos.Length; ++index)
          player.OnQuestWin(this.mCurrentQuest.iname);
        this.mRaidResult.pexp = player.Exp - exp;
        this.mRaidResult.uexp = (int) this.mCurrentQuest.uexp * this.mNumRaids;
        this.mRaidResult.gold = (int) this.mCurrentQuest.gold * this.mNumRaids;
        this.mRaidResult.chquest = new QuestParam[this.mRaidResult.members.Count];
        for (int index = 0; index < this.mRaidResult.members.Count; ++index)
        {
          UnitData.CharacterQuestParam charaEpisodeData = this.mRaidResult.members[index].GetCurrentCharaEpisodeData();
          if (charaEpisodeData != null)
            this.mRaidResult.chquest[index] = charaEpisodeData.Param;
        }
        GlobalVars.RaidResult = this.mRaidResult;
        GlobalVars.PlayerExpOld.Set(exp);
        GlobalVars.PlayerExpNew.Set(player.Exp);
        GlobalVars.PlayerLevelChanged.Set(player.Lv != lv);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.All);
        this.StartCoroutine(this.ShowRaidResultAsync());
      }
    }

    [DebuggerHidden]
    private IEnumerator ShowRaidResultAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CShowRaidResultAsync\u003Ec__IteratorC6() { \u003C\u003Ef__this = this };
    }

    private void OnResetChallenge(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        FlowNode_Network.Failed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComResetResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<PartyWindow2.JSON_ReqBtlComResetResponse>>(www.text);
        if (jsonObject.body == null)
        {
          FlowNode_Network.Failed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            if (!MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.quests))
            {
              FlowNode_Network.Failed();
              return;
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          Network.RemoveAPI();
          this.RefreshRaidButtons();
          GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
        }
      }
    }

    private void OnForwardOrBackButtonClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      if (Object.op_Equality((Object) button, (Object) this.ForwardButton))
      {
        if (this.mCurrentQuest != null && !this.mCurrentQuest.CheckEnableChallange())
        {
          if (!this.mCurrentQuest.CheckEnableReset())
          {
            string empty = string.Empty;
            UIUtility.NegativeSystemMessage((string) null, !(bool) this.mCurrentQuest.isDailyReset ? LocalizedText.Get("sys.QUEST_SPAN_CHALLENGE_NO_RESET") : LocalizedText.Get("sys.QUEST_CHALLENGE_NO_RESET"), (UIUtility.DialogResultEvent) (g => {}), (GameObject) null, false, -1);
          }
          else
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            PartyWindow2.\u003COnForwardOrBackButtonClick\u003Ec__AnonStorey25C clickCAnonStorey25C = new PartyWindow2.\u003COnForwardOrBackButtonClick\u003Ec__AnonStorey25C();
            // ISSUE: reference to a compiler-generated field
            clickCAnonStorey25C.\u003C\u003Ef__this = this;
            FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            int num = (int) fixParam.EliteResetMax - (int) this.mCurrentQuest.dailyReset;
            // ISSUE: reference to a compiler-generated field
            clickCAnonStorey25C.coin = 0;
            if (fixParam.EliteResetCosts != null)
            {
              // ISSUE: reference to a compiler-generated field
              clickCAnonStorey25C.coin = (int) this.mCurrentQuest.dailyReset >= fixParam.EliteResetCosts.Length ? (int) fixParam.EliteResetCosts[fixParam.EliteResetCosts.Length - 1] : (int) fixParam.EliteResetCosts[(int) this.mCurrentQuest.dailyReset];
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            clickCAnonStorey25C.msg = string.Format(LocalizedText.Get("sys.QUEST_CHALLENGE_RESET"), (object) clickCAnonStorey25C.coin, (object) num);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            UIUtility.ConfirmBox(clickCAnonStorey25C.msg, (string) null, new UIUtility.DialogResultEvent(clickCAnonStorey25C.\u003C\u003Em__2B0), (UIUtility.DialogResultEvent) (g => {}), (GameObject) null, false, -1);
          }
        }
        else
        {
          if (this.mCurrentQuest != null)
          {
            if (!this.mCurrentQuest.IsDateUnlock(-1L))
            {
              if (this.mCurrentQuest.IsBeginner)
              {
                UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.BEGINNER_QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
                return;
              }
              UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
              return;
            }
            if (this.mCurrentQuest.IsKeyQuest && !this.mCurrentQuest.IsKeyUnlock(-1L))
            {
              UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
              return;
            }
            if (!this.mCurrentQuest.IsMulti && MonoSingleton<GameManager>.Instance.Player.Stamina < this.mCurrentQuest.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv, true))
            {
              MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true);
              return;
            }
          }
          if (this.mCurrentQuest != null)
          {
            int numMainUnits = 0;
            int num1 = 0;
            int num2 = 0;
            int availableMainMemberSlots = this.AvailableMainMemberSlots;
            List<string> stringList = new List<string>();
            bool flag = false;
            for (int index = 0; index < availableMainMemberSlots; ++index)
            {
              if (this.mCurrentParty.Units[index] != null)
                ++numMainUnits;
              if ((this.mLockedPartySlots & 1 << index) == 0)
              {
                ++num1;
                if (this.mCurrentParty.Units[index] != null && !this.mCurrentQuest.IsUnitAllowed(this.mCurrentParty.Units[index]))
                  ++num2;
              }
            }
            if (this.EnableHeroSolo && this.mGuestUnit != null)
            {
              if (numMainUnits < 1)
                flag = true;
              ++numMainUnits;
            }
            if (this.mCurrentQuest.units != null)
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              PartyWindow2.\u003COnForwardOrBackButtonClick\u003Ec__AnonStorey25D clickCAnonStorey25D = new PartyWindow2.\u003COnForwardOrBackButtonClick\u003Ec__AnonStorey25D();
              // ISSUE: reference to a compiler-generated field
              clickCAnonStorey25D.\u003C\u003Ef__this = this;
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              for (clickCAnonStorey25D.i = 0; clickCAnonStorey25D.i < this.mCurrentQuest.units.Length; ++clickCAnonStorey25D.i)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated method
                if (this.mCurrentParty.Units[clickCAnonStorey25D.i] != null && Array.FindIndex<UnitData>(MonoSingleton<GameManager>.Instance.Player.Units.ToArray(), new Predicate<UnitData>(clickCAnonStorey25D.\u003C\u003Em__2B2)) == -1)
                {
                  // ISSUE: reference to a compiler-generated field
                  UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.mCurrentQuest.units[clickCAnonStorey25D.i]);
                  stringList.Add(unitParam.name);
                }
              }
              if (1 <= stringList.Count)
              {
                UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_NOHERO", new object[1]
                {
                  (object) string.Join(",", stringList.ToArray())
                }), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
                return;
              }
            }
            if (!this.CheckMember(numMainUnits))
              return;
            string empty1 = string.Empty;
            if (!this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units, ref empty1))
            {
              if (this.EnableHeroSolo)
              {
                if (this.mCurrentQuest.IsEntryQuestCondition(new UnitData[1]{ this.mGuestUnit }, ref empty1))
                  goto label_45;
              }
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty1), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
              return;
            }
label_45:
            if (this.mCurrentSupport != null && this.mCurrentSupport.Unit != null && !this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentSupport.Unit, ref empty1))
            {
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty1), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
              return;
            }
            if (this.mCurrentQuest.IsCharacterQuest())
            {
              List<UnitData> unitDataList = new List<UnitData>((IEnumerable<UnitData>) this.mCurrentParty.Units);
              unitDataList.Add(this.mGuestUnit);
              for (int index1 = 0; index1 < unitDataList.Count; ++index1)
              {
                UnitData unitData = unitDataList[index1];
                if (unitData != null && unitData.UnitID == this.mGuestUnit.UnitID)
                {
                  string empty2 = string.Empty;
                  List<QuestClearUnlockUnitDataParam> skillUnlocks = unitData.SkillUnlocks;
                  for (int index2 = 0; index2 < skillUnlocks.Count; ++index2)
                  {
                    QuestClearUnlockUnitDataParam unlockUnitDataParam = skillUnlocks[index2];
                    if (unlockUnitDataParam != null && !unlockUnitDataParam.add && (unlockUnitDataParam.qids != null && Array.FindIndex<string>(unlockUnitDataParam.qids, (Predicate<string>) (p => p == this.mCurrentQuest.iname)) != -1))
                      empty2 += LocalizedText.Get("sys.UNITLIST_REWRITE_TARGET", (object) unlockUnitDataParam.GetUnlockTypeText(), (object) unlockUnitDataParam.GetRewriteName());
                  }
                  if (!string.IsNullOrEmpty(empty2))
                  {
                    UIUtility.ConfirmBox(LocalizedText.Get("sys.UNITLIST_DATA_REWRITE", new object[1]
                    {
                      (object) empty2
                    }), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
                    return;
                  }
                }
              }
            }
            if (num2 > 0)
            {
              UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_SANKAFUKA"), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
              return;
            }
            if (!flag && numMainUnits < num1)
            {
              UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL"), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
              return;
            }
          }
          this.PostForwardPressed();
        }
      }
      else
        this.SaveAndActivatePin(3);
    }

    protected virtual bool CheckMember(int numMainUnits)
    {
      if (numMainUnits <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
        return false;
      }
      string empty = string.Empty;
      if (this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units, ref empty))
        return true;
      UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
      return false;
    }

    protected virtual void PostForwardPressed()
    {
      GlobalEvent.Invoke("DISABLE_MAINMENU_TOP_COMMAND", (object) null);
      GlobalVars.SelectedFriendID = this.mCurrentSupport == null ? (string) null : this.mCurrentSupport.FUID;
      this.SaveAndActivatePin(1);
    }

    protected void SaveAndActivatePin(int pinID)
    {
      if (!this.mInitialized)
        return;
      this.SaveInventory();
      if (!this.EnableHeroSolo && this.mCurrentQuest != null && (this.mCurrentQuest.UseFixEditor && this.mGuestUnit != null) && this.mTeams[this.mCurrentTeamIndex].Units[0] == null)
        this.mTeams[this.mCurrentTeamIndex].Units[0] = this.mGuestUnit;
      if (!this.IsPartyDirty)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID);
      }
      else
      {
        GlobalVars.SelectedSupport.Set(this.mCurrentSupport);
        this.SaveParty((PartyWindow2.Callback) (() => FlowNode_GameObject.ActivateOutputLinks((Component) this, pinID)), (PartyWindow2.Callback) (() => UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.ILLEGAL_PARTY"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1)));
      }
    }

    private void SaveParty(PartyWindow2.Callback cbSuccess, PartyWindow2.Callback cbError)
    {
      if (this.mIsSaving)
        return;
      this.LockWindow(true);
      this.mIsSaving = true;
      this.mOnPartySaveSuccess = cbSuccess;
      this.mOnPartySaveFail = cbError;
      if (!this.IsPartyDirty)
      {
        if (this.mOnPartySaveSuccess == null)
          return;
        this.mOnPartySaveSuccess();
      }
      else
      {
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(this.mCurrentPartyType.ToPlayerPartyType());
        for (int index = 0; index < this.mCurrentParty.PartyData.MAX_UNIT; ++index)
        {
          long uniqueid = this.mCurrentParty.Units[index] == null ? 0L : this.mCurrentParty.Units[index].UniqueID;
          partyOfType.SetUnitUniqueID(index, uniqueid);
        }
        bool ignoreEmpty = true;
        if (this.EnableHeroSolo && this.mCurrentQuest != null && (this.mCurrentQuest.units != null && this.mCurrentQuest.EntryCondition != null) && (this.mCurrentQuest.EntryCondition.unit != null && this.mCurrentQuest.units[0] == this.mCurrentQuest.EntryCondition.unit[0]))
          ignoreEmpty = false;
        Network.RequestAPI((WebAPI) new ReqParty(new Network.ResponseCallback(this.SavePartyCallback), false, ignoreEmpty), false);
      }
    }

    private void SaveInventory()
    {
      if (!this.mInitialized || !this.InventoryDirty)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < 5; ++index)
      {
        ItemData itemData = (ItemData) null;
        if (index < this.mCurrentItems.Length && this.mCurrentItems[index] != null)
          itemData = player.FindItemDataByItemParam(this.mCurrentItems[index].Param);
        player.SetInventory(index, itemData);
      }
      player.SaveInventory();
    }

    private bool InventoryDirty
    {
      get
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        for (int index = 0; index < 5; ++index)
        {
          ItemData itemData = (ItemData) null;
          if (index < this.mCurrentItems.Length && this.mCurrentItems[index] != null)
            itemData = player.FindItemDataByItemParam(this.mCurrentItems[index].Param);
          if (player.Inventory[index] != itemData)
            return true;
        }
        return false;
      }
    }

    [DebuggerHidden]
    private IEnumerator WaitForSave()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CWaitForSave\u003Ec__IteratorC7() { \u003C\u003Ef__this = this };
    }

    private void SavePartyCallback(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoUnitParty:
            Network.RemoveAPI();
            Network.ResetError();
            break;
          case Network.EErrCode.IllegalParty:
            Network.RemoveAPI();
            Network.ResetError();
            break;
          default:
            FlowNode_Network.Retry();
            return;
        }
        this.LockWindow(false);
        this.mIsSaving = false;
        if (this.mOnPartySaveFail == null)
          return;
        this.mOnPartySaveFail();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        try
        {
          if (jsonObject.body == null)
            throw new InvalidJSONException();
          instance.Deserialize(jsonObject.body.player);
          instance.Deserialize(jsonObject.body.parties);
        }
        catch (Exception ex)
        {
          FlowNode_Network.Retry();
          return;
        }
        Network.RemoveAPI();
        this.LockWindow(false);
        this.mIsSaving = false;
        if (this.mOnPartySaveSuccess == null)
          return;
        this.mOnPartySaveSuccess();
      }
    }

    protected void RefreshQuest()
    {
      QuestParam mCurrentQuest = this.mCurrentQuest;
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (this.mCurrentQuest != mCurrentQuest)
        this.mMultiRaidNum = -1;
      DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), this.mCurrentQuest);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
      DataSource.Bind<QuestCampaignData[]>(((Component) this).get_gameObject(), questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      if (Object.op_Inequality((Object) this.QuestCampaigns, (Object) null))
        this.QuestCampaigns.RefreshIcons();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    protected void Refresh(bool keepTeam = false)
    {
      if (Object.op_Inequality((Object) this.QuestInfo, (Object) null))
      {
        if (this.ShowQuestInfo)
        {
          DataSource.Bind<QuestParam>(this.QuestInfo, this.mCurrentQuest);
          GameParameter.UpdateAll(this.QuestInfo);
          this.QuestInfo.SetActive(true);
        }
        else
          this.QuestInfo.SetActive(false);
        if (Object.op_Inequality((Object) this.Prefab_SankaFuka, (Object) null))
        {
          for (int index = 0; index < this.mSankaFukaIcons.Length && index < this.UnitSlots.Length; ++index)
          {
            if (Object.op_Equality((Object) this.mSankaFukaIcons[index], (Object) null) && Object.op_Inequality((Object) this.UnitSlots[index], (Object) null))
            {
              this.mSankaFukaIcons[index] = (GameObject) Object.Instantiate<GameObject>((M0) this.Prefab_SankaFuka);
              RectTransform transform = this.mSankaFukaIcons[index].get_transform() as RectTransform;
              transform.set_anchoredPosition(Vector2.get_zero());
              ((Transform) transform).SetParent(((Component) this.UnitSlots[index]).get_transform(), false);
            }
          }
        }
      }
      if (this.PartyType == PartyWindow2.EditPartyTypes.Auto)
      {
        this.mCurrentPartyType = PartyWindow2.EditPartyTypes.Auto;
        if (this.mCurrentQuest == null)
          DebugUtility.LogError("Quest not selected");
        else
          this.mCurrentPartyType = this.GetEditPartyTypes();
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Auto)
          this.mCurrentPartyType = PartyWindow2.EditPartyTypes.Normal;
      }
      else
        this.mCurrentPartyType = this.PartyType;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
      if (Array.IndexOf<PlayerPartyTypes>(this.SaveJobs, playerPartyType) >= 0)
      {
        this.mOwnUnits = new List<UnitData>(player.Units.Count);
        for (int index = 0; index < player.Units.Count; ++index)
        {
          UnitData unitData = new UnitData();
          unitData.Setup(player.Units[index]);
          unitData.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
          unitData.SetJob(playerPartyType);
          this.mOwnUnits.Add(unitData);
        }
      }
      else
      {
        this.mOwnUnits = new List<UnitData>((IEnumerable<UnitData>) player.Units);
        for (int index = 0; index < this.mOwnUnits.Count; ++index)
          this.mOwnUnits[index].TempFlags |= UnitData.TemporaryFlags.AllowJobChange;
      }
      DataSource.Bind<PlayerPartyTypes>(((Component) this).get_gameObject(), playerPartyType);
      if (!keepTeam)
        this.LoadTeamPresets();
      for (int index = 0; index < this.UnitSlots.Length && index < this.mCurrentParty.PartyData.MAX_UNIT; ++index)
      {
        if (Object.op_Inequality((Object) this.UnitSlots[index], (Object) null) && index < this.mCurrentParty.Units.Length)
        {
          this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
          this.UnitSlots[index].SetSlotData<UnitData>(this.FindUnit(this.mCurrentParty.Units[index]));
        }
      }
      if (Object.op_Inequality((Object) this.FriendSlot, (Object) null))
      {
        this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
        this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport == null ? (UnitData) null : this.mCurrentSupport.Unit);
      }
      if (Object.op_Inequality((Object) this.GuestUnitSlot, (Object) null))
      {
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentQuest != null && this.mCurrentQuest.UseFixEditor)
        {
          UnitData unitData = (UnitData) null;
          if (this.mCurrentQuest != null && (this.mCurrentQuest.type == QuestTypes.Story || this.mCurrentQuest.type == QuestTypes.Free || (this.mCurrentQuest.type == QuestTypes.Tutorial || this.mCurrentQuest.type == QuestTypes.Character) || this.mCurrentQuest.type == QuestTypes.Event) && (this.mCurrentQuest.units != null && this.mCurrentQuest.units.Length > 0))
            unitData = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.mCurrentQuest.units[0]);
          this.mGuestUnit = this.FindUnit(unitData);
          this.GuestUnitSlot.SetSlotData<UnitData>(unitData);
          ((Component) this.GuestUnitSlot).get_gameObject().SetActive(true);
        }
        else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character)
        {
          UnitData unitData = (UnitData) null;
          if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Character && (this.mCurrentQuest.units != null && this.mCurrentQuest.units.Length > 0))
            unitData = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.mCurrentQuest.units[0]);
          this.mGuestUnit = this.FindUnit(unitData);
          this.GuestUnitSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
          this.GuestUnitSlot.SetSlotData<UnitData>(unitData);
          ((Component) this.GuestUnitSlot).get_gameObject().SetActive(true);
          if (Object.op_Inequality((Object) this.QuestUnitCond, (Object) null) && this.mGuestUnit != null)
          {
            DataSource.Bind<UnitData>(this.QuestUnitCond, this.mGuestUnit);
            GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.QUEST_IS_UNIT_ENTRYCONDITION);
          }
        }
        else
          ((Component) this.GuestUnitSlot).get_gameObject().SetActive(false);
      }
      int availableMainMemberSlots = this.AvailableMainMemberSlots;
      for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
      {
        if (Object.op_Inequality((Object) this.UnitSlots[mainmemberStart], (Object) null))
        {
          int num = mainmemberStart - this.mCurrentParty.PartyData.MAINMEMBER_START;
          ((Component) this.UnitSlots[mainmemberStart]).get_gameObject().SetActive(num < availableMainMemberSlots);
        }
      }
      if (Object.op_Inequality((Object) this.ForwardButton, (Object) null))
      {
        ((Component) this.ForwardButton).get_gameObject().SetActive(this.ShowForwardButton);
        BackHandler component = (BackHandler) ((Component) this.ForwardButton).GetComponent<BackHandler>();
        if (Object.op_Inequality((Object) component, (Object) null))
          ((Behaviour) component).set_enabled(!this.ShowBackButton);
      }
      if (Object.op_Inequality((Object) this.BackButton, (Object) null))
        ((Component) this.BackButton).get_gameObject().SetActive(this.ShowBackButton);
      this.ToggleRaidInfo();
      this.RefreshRaidTicketNum();
      this.RefreshRaidButtons();
      this.LockSlots();
      this.OnPartyMemberChange();
      this.LoadInventory();
    }

    protected virtual PartyWindow2.EditPartyTypes GetEditPartyTypes()
    {
      if (this.mCurrentQuest.type == QuestTypes.Multi)
        return PartyWindow2.EditPartyTypes.MP;
      if (this.mCurrentQuest.type == QuestTypes.Event)
        return PartyWindow2.EditPartyTypes.Event;
      if (this.mCurrentQuest.type == QuestTypes.Character)
        return PartyWindow2.EditPartyTypes.Character;
      return this.mCurrentQuest.type == QuestTypes.VersusFree || this.mCurrentQuest.type == QuestTypes.VersusRank ? PartyWindow2.EditPartyTypes.Versus : PartyWindow2.EditPartyTypes.Auto;
    }

    private void RefreshSankaStates()
    {
      if (this.mCurrentQuest == null)
      {
        for (int index = 0; index < this.mSankaFukaIcons.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.mSankaFukaIcons[index], (Object) null))
            this.mSankaFukaIcons[index].SetActive(false);
          if (Object.op_Inequality((Object) this.UnitSlots[index], (Object) null))
            this.UnitSlots[index].SetMainColor(Color.get_white());
        }
      }
      else
      {
        for (int index = 0; index < this.mSankaFukaIcons.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.mSankaFukaIcons[index], (Object) null))
          {
            bool flag = true;
            if (this.mCurrentParty.Units[index] != null)
              flag = this.mCurrentQuest.IsUnitAllowed(this.mCurrentParty.Units[index]);
            if (Object.op_Inequality((Object) this.UnitSlots[index], (Object) null))
            {
              if (flag)
                this.UnitSlots[index].SetMainColor(Color.get_white());
              else
                this.UnitSlots[index].SetMainColor(new Color(this.SankaFukaOpacity, this.SankaFukaOpacity, this.SankaFukaOpacity, 1f));
            }
            this.mSankaFukaIcons[index].SetActive(!flag);
          }
        }
      }
    }

    private void ToggleRaidInfo()
    {
      if (!Object.op_Inequality((Object) this.RaidInfo, (Object) null))
        return;
      bool flag = false;
      if (this.mCurrentQuest != null && !string.IsNullOrEmpty(this.mCurrentQuest.ticket) && this.mCurrentQuest.state == QuestStates.Cleared)
        flag = true;
      if (!this.ShowRaidInfo)
        flag = false;
      this.RaidInfo.SetActive(flag);
    }

    private void RefreshRaidTicketNum()
    {
      if (!Object.op_Inequality((Object) this.RaidTicketNum, (Object) null))
        return;
      int num = 0;
      if (this.mCurrentQuest != null && !string.IsNullOrEmpty(this.mCurrentQuest.ticket))
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
        if (itemDataByItemId != null)
          num = itemDataByItemId.Num;
      }
      this.RaidTicketNum.set_text(num.ToString());
    }

    private void LockSlots()
    {
      this.mLockedPartySlots = 0;
      this.mSupportLocked = false;
      this.mItemsLocked = false;
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MP)
      {
        this.mSupportLocked = true;
        this.mItemsLocked = true;
        if (GameUtility.GetCurrentScene() == GameUtility.EScene.HOME_MULTI)
        {
          MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
          JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom == null || string.IsNullOrEmpty(currentRoom.json) ? (JSON_MyPhotonRoomParam) null : JSON_MyPhotonRoomParam.Parse(currentRoom.json);
          if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID)
          {
            if (myPhotonRoomParam != null)
            {
              int unitSlotNum = myPhotonRoomParam.GetUnitSlotNum();
              for (int index = 0; index < this.mCurrentParty.PartyData.MAX_UNIT; ++index)
              {
                if (index >= unitSlotNum)
                  this.mLockedPartySlots |= 1 << index;
                if (index < this.UnitSlots.Length && Object.op_Inequality((Object) this.UnitSlots[index], (Object) null) && Object.op_Inequality((Object) this.UnitSlots[index].SelectButton, (Object) null))
                  ((Selectable) this.UnitSlots[index].SelectButton).set_interactable(index < unitSlotNum);
              }
            }
          }
          else
          {
            for (int submemberStart = this.mCurrentParty.PartyData.SUBMEMBER_START; submemberStart < this.mCurrentParty.PartyData.MAX_UNIT; ++submemberStart)
            {
              this.mLockedPartySlots |= 1 << submemberStart;
              if (submemberStart < this.UnitSlots.Length && Object.op_Inequality((Object) this.UnitSlots[submemberStart], (Object) null) && Object.op_Inequality((Object) this.UnitSlots[submemberStart].SelectButton, (Object) null))
                ((Selectable) this.UnitSlots[submemberStart].SelectButton).set_interactable(false);
            }
          }
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Arena || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.ArenaDef)
      {
        this.mSupportLocked = true;
        this.mItemsLocked = true;
        for (int submemberStart = this.mCurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.mCurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
          this.mLockedPartySlots |= 1 << submemberStart;
      }
      if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tutorial)
      {
        this.mItemsLocked = true;
        this.mSupportLocked = true;
        for (int submemberStart = this.mCurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.mCurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
          this.mLockedPartySlots |= 1 << submemberStart;
      }
      if (this.mCurrentQuest != null && !this.mCurrentQuest.UseSupportUnit)
        this.mSupportLocked = true;
      if (Object.op_Inequality((Object) this.NoItemText, (Object) null))
        this.NoItemText.SetActive(this.mItemsLocked);
      for (int index = 0; index < this.UnitSlots.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.UnitSlots[index], (Object) null))
          this.UnitSlots[index].SetLocked((this.mLockedPartySlots & 1 << index) != 0);
      }
      if (Object.op_Inequality((Object) this.FriendSlot, (Object) null))
        this.FriendSlot.SetLocked(this.mSupportLocked);
      for (int index = 0; index < this.ItemSlots.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.ItemSlots[index], (Object) null))
          this.ItemSlots[index].SetLocked(this.mItemsLocked);
      }
    }

    private string CurrentTeamID
    {
      get
      {
        return "TEAM_" + this.mCurrentPartyType.ToString().ToUpper();
      }
    }

    protected virtual void LoadInventory()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int slotIndex = 0; slotIndex < this.mCurrentItems.Length && slotIndex < player.Inventory.Length; ++slotIndex)
        this.SetItemSlot(slotIndex, player.Inventory[slotIndex]);
      this.OnItemSlotsChange();
    }

    private void SaveTeamPresets()
    {
      if (this.mTeams.Count <= 1)
        return;
      StringBuilder stringBuilder = new StringBuilder(1024);
      stringBuilder.Append("{\"n\":");
      stringBuilder.Append(this.mCurrentTeamIndex);
      stringBuilder.Append(",\"t\":[");
      for (int index1 = 0; index1 < this.mTeams.Count; ++index1)
      {
        if (index1 > 0)
          stringBuilder.Append(",");
        stringBuilder.Append("{\"u\":[");
        for (int index2 = 0; index2 < this.mCurrentParty.PartyData.MAX_UNIT; ++index2)
        {
          long num = 0;
          if (index2 > 0)
            stringBuilder.Append(',');
          if (this.mTeams[index1].Units[index2] != null)
            num = this.mTeams[index1].Units[index2].UniqueID;
          stringBuilder.Append(num.ToString());
        }
        stringBuilder.Append("]}");
      }
      stringBuilder.Append("]}");
      DebugUtility.Log(stringBuilder.ToString());
      PlayerPrefs.SetString(this.CurrentTeamID, stringBuilder.ToString());
      PlayerPrefs.Save();
    }

    protected void LoadTeamPresets()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Auto)
        throw new InvalidPartyTypeException();
      int maxTeamCount = this.mCurrentPartyType.GetMaxTeamCount();
      this.mTeams = new List<PartyWindow2.PartyEditData>(maxTeamCount);
      if (maxTeamCount <= 1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PartyWindow2.\u003CLoadTeamPresets\u003Ec__AnonStorey25F presetsCAnonStorey25F = new PartyWindow2.\u003CLoadTeamPresets\u003Ec__AnonStorey25F();
        PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
        // ISSUE: reference to a compiler-generated field
        presetsCAnonStorey25F.edit = new PartyWindow2.PartyEditData(partyOfType);
        if (playerPartyType == PlayerPartyTypes.Character && this.mCurrentQuest != null && this.mCurrentQuest.units.Length > 0)
        {
          QuestCondParam entryCondition = this.mCurrentQuest.EntryCondition;
          bool flag = entryCondition != null && entryCondition.unit != null && entryCondition.unit.Length == 1;
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          PartyWindow2.\u003CLoadTeamPresets\u003Ec__AnonStorey260 presetsCAnonStorey260 = new PartyWindow2.\u003CLoadTeamPresets\u003Ec__AnonStorey260();
          // ISSUE: reference to a compiler-generated field
          presetsCAnonStorey260.\u003C\u003Ef__ref\u0024607 = presetsCAnonStorey25F;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          for (presetsCAnonStorey260.i = 0; presetsCAnonStorey260.i < presetsCAnonStorey25F.edit.Units.Length; ++presetsCAnonStorey260.i)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (presetsCAnonStorey25F.edit.Units[presetsCAnonStorey260.i] != null)
            {
              // ISSUE: reference to a compiler-generated method
              if (Array.FindIndex<string>(this.mCurrentQuest.units, new Predicate<string>(presetsCAnonStorey260.\u003C\u003Em__2BE)) != -1)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                presetsCAnonStorey25F.edit.Units[presetsCAnonStorey260.i] = (UnitData) null;
              }
              else if (this.EnableHeroSolo && flag)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                presetsCAnonStorey25F.edit.Units[presetsCAnonStorey260.i] = (UnitData) null;
              }
            }
          }
          if (this.EnableHeroSolo && !flag)
          {
            // ISSUE: reference to a compiler-generated field
            this.AutoSetLeaderUnit(presetsCAnonStorey25F.edit, this.mCurrentQuest.units, MonoSingleton<GameManager>.Instance.Player.Units);
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          presetsCAnonStorey25F.edit.SetUnits(presetsCAnonStorey25F.edit.Units);
        }
        // ISSUE: reference to a compiler-generated field
        this.mCurrentParty = presetsCAnonStorey25F.edit;
        // ISSUE: reference to a compiler-generated field
        this.mTeams.Add(presetsCAnonStorey25F.edit);
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
        string currentTeamId = this.CurrentTeamID;
        PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[(int) playerPartyType];
        UnitData[] src1 = new UnitData[party.MAX_UNIT];
        if (PlayerPrefs.HasKey(currentTeamId))
        {
          string str = PlayerPrefs.GetString(currentTeamId);
          DebugUtility.Log(str);
          try
          {
            PartyWindow2.JSON_TeamSettings jsonObject = JSONParser.parseJSONObject<PartyWindow2.JSON_TeamSettings>(str);
            for (int index1 = 0; index1 < jsonObject.t.Length; ++index1)
            {
              PartyWindow2.JSON_Team jsonTeam = jsonObject.t[index1];
              for (int index2 = 0; index2 < party.MAX_UNIT; ++index2)
              {
                if (index2 >= jsonTeam.u.Length)
                {
                  src1[index2] = (UnitData) null;
                }
                else
                {
                  UnitData unitData = jsonTeam.u[index2] == 0L ? (UnitData) null : player.FindUnitDataByUniqueID(jsonTeam.u[index2]);
                  src1[index2] = unitData;
                }
              }
              this.mTeams.Add(new PartyWindow2.PartyEditData(src1, party));
            }
            this.mCurrentTeamIndex = jsonObject.n;
            if (this.mCurrentTeamIndex >= 0)
            {
              if (maxTeamCount > this.mCurrentTeamIndex)
                goto label_31;
            }
            this.mCurrentTeamIndex = 0;
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
label_31:
        PartyData partyOfType1 = player.FindPartyOfType(playerPartyType);
        for (int count = this.mTeams.Count; count < maxTeamCount; ++count)
          this.mTeams.Add(new PartyWindow2.PartyEditData(partyOfType1));
        UnitData[] src2 = (UnitData[]) null;
        for (int index1 = 0; index1 < this.mTeams.Count; ++index1)
        {
          if (!this.IsTeamLegal(this.mTeams[index1]))
          {
            if (src2 == null)
            {
              PartyData partyOfType2 = player.FindPartyOfType(playerPartyType);
              src2 = new UnitData[partyOfType2.MAX_UNIT];
              for (int index2 = 0; index2 < src2.Length; ++index2)
              {
                long unitUniqueId = partyOfType2.GetUnitUniqueID(index2);
                if (unitUniqueId != 0L)
                {
                  UnitData unitDataByUniqueId = player.FindUnitDataByUniqueID(unitUniqueId);
                  if (unitDataByUniqueId != null)
                    src2[index2] = unitDataByUniqueId;
                }
              }
            }
            this.mTeams[index1].SetUnits(src2);
          }
          if (this.mCurrentQuest != null && this.mCurrentQuest.UseFixEditor)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            PartyWindow2.\u003CLoadTeamPresets\u003Ec__AnonStorey261 presetsCAnonStorey261 = new PartyWindow2.\u003CLoadTeamPresets\u003Ec__AnonStorey261();
            // ISSUE: reference to a compiler-generated field
            presetsCAnonStorey261.edit = this.mTeams[index1];
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            PartyWindow2.\u003CLoadTeamPresets\u003Ec__AnonStorey262 presetsCAnonStorey262 = new PartyWindow2.\u003CLoadTeamPresets\u003Ec__AnonStorey262();
            // ISSUE: reference to a compiler-generated field
            presetsCAnonStorey262.\u003C\u003Ef__ref\u0024609 = presetsCAnonStorey261;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            for (presetsCAnonStorey262.j = 0; presetsCAnonStorey262.j < presetsCAnonStorey261.edit.Units.Length; ++presetsCAnonStorey262.j)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated method
              if (presetsCAnonStorey261.edit.Units[presetsCAnonStorey262.j] != null && Array.FindIndex<string>(this.mCurrentQuest.units, new Predicate<string>(presetsCAnonStorey262.\u003C\u003Em__2BF)) != -1)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                presetsCAnonStorey261.edit.Units[presetsCAnonStorey262.j] = (UnitData) null;
              }
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            presetsCAnonStorey261.edit.SetUnits(presetsCAnonStorey261.edit.Units);
            // ISSUE: reference to a compiler-generated field
            this.mTeams[index1] = presetsCAnonStorey261.edit;
          }
        }
        bool flag = false;
        if (this.EnableHeroSolo && this.mCurrentQuest != null)
        {
          if (this.IsSoloEventParty(this.mCurrentQuest))
            flag = this.KyouseiUnitPartyEdit(this.mCurrentQuest, this.mTeams[this.mCurrentTeamIndex]);
          else if (this.mCurrentQuest.type == QuestTypes.Event)
          {
            List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
            for (int index = 0; index < this.mTeams.Count; ++index)
              flag = this.AutoSetLeaderUnit(this.mTeams[index], this.mCurrentQuest.units, units);
          }
        }
        this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
        if (flag)
          this.SaveTeamPresets();
      }
      if (!Object.op_Inequality((Object) this.TeamPulldown, (Object) null))
        return;
      if (maxTeamCount <= 1)
      {
        ((Component) this.TeamPulldown).get_gameObject().SetActive(false);
      }
      else
      {
        this.TeamPulldown.ClearItems();
        string format = LocalizedText.Get("sys.TEAMNAME");
        for (int index = 0; index < maxTeamCount; ++index)
          this.TeamPulldown.AddItem(string.Format(format, (object) (index + 1)), index);
        this.TeamPulldown.Selection = this.mCurrentTeamIndex;
        ((Component) this.TeamPulldown).get_gameObject().SetActive(true);
      }
      if (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tutorial)
        return;
      this.TeamPulldown.set_interactable(false);
    }

    private bool IsTeamLegal(PartyWindow2.PartyEditData team)
    {
      if (team.Units[0] == null)
        return false;
      for (int index1 = 0; index1 < team.Units.Length; ++index1)
      {
        if (team.Units[index1] != null)
        {
          for (int index2 = index1 + 1; index2 < team.Units.Length; ++index2)
          {
            if (team.Units[index1] == team.Units[index2])
              return false;
          }
        }
      }
      if (!this.HeroesAvailable)
      {
        for (int index = 0; index < team.Units.Length; ++index)
        {
          if (team.Units[index] != null && (int) team.Units[index].UnitParam.hero != 0)
            return false;
        }
      }
      return true;
    }

    private void RefreshRaidButtons()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = 0;
      ItemData itemData = (ItemData) null;
      ItemParam data = (ItemParam) null;
      if (this.mCurrentQuest != null)
      {
        num1 = this.mCurrentQuest.RequiredApWithPlayerLv(player.Lv, true);
        itemData = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
        if (itemData != null)
          data = itemData.Param;
        else if (!string.IsNullOrEmpty(this.mCurrentQuest.ticket))
          data = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket);
        if (Object.op_Inequality((Object) this.RaidInfo, (Object) null))
        {
          DataSource.Bind<ItemParam>(this.RaidInfo.get_gameObject(), data);
          GameParameter.UpdateAll(this.RaidInfo.get_gameObject());
        }
      }
      int num2 = 0;
      if (itemData != null)
        num2 = itemData.Num;
      if (this.mCurrentQuest != null)
      {
        int num3 = this.MaxRaidNum;
        int num4 = this.MaxRaidNum;
        if (num1 > 0)
          num3 = Mathf.Min(player.Stamina / num1, this.MaxRaidNum);
        int num5 = Mathf.Min(num2, num3);
        if (this.mCurrentQuest.GetChallangeLimit() > 0)
          num4 = Mathf.Min(num4, this.mCurrentQuest.GetChallangeLimit() - this.mCurrentQuest.GetChallangeCount());
        if (!Object.op_Inequality((Object) this.RaidN, (Object) null))
          return;
        if (this.mMultiRaidNum < 0)
          this.mMultiRaidNum = Mathf.Min(this.MaxRaidNum, this.DefaultRaidNum);
        this.mMultiRaidNum = Mathf.Min(new int[3]
        {
          this.mMultiRaidNum,
          num5,
          num4
        });
        this.RaidNCount.set_text(LocalizedText.Get("sys.RAIDNUM", new object[1]
        {
          (object) Mathf.Max(this.mMultiRaidNum, 1)
        }));
        ((Selectable) this.RaidN).set_interactable(num4 >= 1 && num2 >= 1);
      }
      else
      {
        if (Object.op_Inequality((Object) this.Raid, (Object) null))
          ((Selectable) this.Raid).set_interactable(false);
        if (!Object.op_Inequality((Object) this.RaidN, (Object) null))
          return;
        ((Selectable) this.RaidN).set_interactable(false);
      }
    }

    private UnitData[] GetDefaultTeam()
    {
      UnitData[] unitDataArray = new UnitData[this.mCurrentParty.PartyData.MAX_UNIT];
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int index1 = 0;
      for (int index2 = 0; index2 < player.Units.Count && index2 < unitDataArray.Length; ++index2)
      {
        if ((int) player.Units[index2].UnitParam.hero == 0)
        {
          unitDataArray[index1] = player.Units[index2];
          ++index1;
        }
      }
      return unitDataArray;
    }

    private bool TeamsAvailable
    {
      get
      {
        if (this.mCurrentPartyType != PartyWindow2.EditPartyTypes.Normal)
          return this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event;
        return true;
      }
    }

    private bool HeroesAvailable
    {
      get
      {
        if (this.mCurrentPartyType != PartyWindow2.EditPartyTypes.Normal)
          return true;
        if (this.mCurrentQuest != null)
          return this.mCurrentQuest.UseFixEditor;
        return false;
      }
    }

    protected virtual int AvailableMainMemberSlots
    {
      get
      {
        return this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Arena || (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.ArenaDef || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character) || (this.mCurrentQuest == null ? 0 : (this.mCurrentQuest.UseFixEditor ? 1 : 0)) != 0 ? 3 : 4;
      }
    }

    public void Activated(int pinID)
    {
    }

    private void OnStaminaChange()
    {
      if (Object.op_Equality((Object) this, (Object) null))
        return;
      this.RefreshRaidButtons();
    }

    private void OnUnitSortModeChange(int index)
    {
      this.mUnitSortMode = GameSettings.Instance.UnitSort_Modes[index].Mode;
      this.RefreshUnitList();
    }

    private UnitData FindUnit(long uniqueID)
    {
      for (int index = this.mOwnUnits.Count - 1; index >= 0; --index)
      {
        if (this.mOwnUnits[index].UniqueID == uniqueID)
          return this.mOwnUnits[index];
      }
      return (UnitData) null;
    }

    private UnitData FindUnit(UnitData source)
    {
      if (source != null)
        return this.FindUnit(source.UniqueID);
      return (UnitData) null;
    }

    private void RefreshUnitList()
    {
      List<UnitData> unitDataList = new List<UnitData>((IEnumerable<UnitData>) this.mOwnUnits);
      this.UnitList.ClearItems();
      bool selectedSlotIsEmpty = this.mCurrentParty.Units[this.mSelectedSlotIndex] == null;
      if (this.mSelectedSlotIndex > 0 && (!selectedSlotIsEmpty || this.AlwaysShowRemoveUnit))
        this.UnitList.AddItem(0);
      bool heroesAvailable = this.HeroesAvailable;
      if (this.mUnitSortMode != GameUtility.UnitSortModes.Time)
        GameUtility.SortUnits(unitDataList, this.mUnitSortMode, false, out this.mUnitSortValues, false);
      else
        this.mUnitSortValues = (int[]) null;
      if ((this.mCurrentQuest.type == QuestTypes.Event || this.PartyType == PartyWindow2.EditPartyTypes.Character) && (this.mCurrentQuest.units != null && this.mCurrentQuest.units.Length > 0))
      {
        for (int index = 0; index < this.mCurrentQuest.units.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: reference to a compiler-generated method
          UnitData unitData = Array.Find<UnitData>(unitDataList.ToArray(), new Predicate<UnitData>(new PartyWindow2.\u003CRefreshUnitList\u003Ec__AnonStorey263() { chQuestHeroId = this.mCurrentQuest.units[index] }.\u003C\u003Em__2C0));
          if (unitData != null)
            unitDataList.Remove(unitData);
        }
      }
      int numMainMembers = 0;
      for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
      {
        if (this.mCurrentParty.Units[mainmemberStart] != null)
          ++numMainMembers;
      }
      for (int index = 0; index < this.mCurrentParty.PartyData.MAX_UNIT; ++index)
      {
        if (this.mCurrentParty.Units[index] != null && (heroesAvailable || (int) this.mCurrentParty.Units[index].UnitParam.hero == 0) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (index != 0 || !selectedSlotIsEmpty) || numMainMembers > 1))
        {
          string empty = string.Empty;
          if (this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units[index], ref empty))
          {
            this.UnitList.AddItem(this.mOwnUnits.IndexOf(this.FindUnit(this.mCurrentParty.Units[index])) + 1);
            unitDataList.Remove(this.mCurrentParty.Units[index]);
          }
        }
      }
      int count = unitDataList.Count;
      UnitListV2.FilterUnits(unitDataList, (List<int>) null, this.mUnitFilter);
      if (this.mReverse)
        unitDataList.Reverse();
      this.RegistPartyMember(unitDataList, heroesAvailable, selectedSlotIsEmpty, numMainMembers);
      this.UnitList.Refresh(true);
      this.UnitList.ForceUpdateItems();
      if (Object.op_Inequality((Object) this.UnitListHilit, (Object) null))
      {
        ((Component) this.UnitListHilit).get_gameObject().SetActive(false);
        ((Transform) this.UnitListHilit).SetParent(((Component) this).get_transform(), false);
        UnitData unit = this.mCurrentParty.Units[this.mSelectedSlotIndex];
        if (unit != null)
        {
          int itemID = this.mOwnUnits.IndexOf(unit) + 1;
          if (itemID > 0)
          {
            RectTransform rectTransform = this.UnitList.FindItem(itemID);
            if (Object.op_Inequality((Object) rectTransform, (Object) null))
              this.AttachAndEnable((Transform) this.UnitListHilit, (Transform) rectTransform, this.UnitListHilitParent);
          }
        }
      }
      if (!Object.op_Inequality((Object) this.NoMatchingUnit, (Object) null))
        return;
      this.NoMatchingUnit.SetActive(count > 0 && this.UnitList.NumItems <= 0);
    }

    protected virtual void RegistPartyMember(List<UnitData> allUnits, bool heroesAvailable, bool selectedSlotIsEmpty, int numMainMembers)
    {
      for (int index = 0; index < allUnits.Count; ++index)
      {
        if ((heroesAvailable || (int) allUnits[index].UnitParam.hero == 0) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (allUnits[index] != this.mCurrentParty.Units[0] || !selectedSlotIsEmpty) || numMainMembers > 1))
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
      }
    }

    private void OnItemFilterChange(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      PartyWindow2.ItemFilterTypes itemFilterTypes = PartyWindow2.ItemFilterTypes.All;
      if (Object.op_Equality((Object) button, (Object) this.ItemFilter_Offense))
        itemFilterTypes = PartyWindow2.ItemFilterTypes.Offense;
      else if (Object.op_Equality((Object) button, (Object) this.ItemFilter_Support))
        itemFilterTypes = PartyWindow2.ItemFilterTypes.Support;
      if (this.mItemFilter == itemFilterTypes)
        return;
      this.mItemFilter = itemFilterTypes;
      for (int index = 0; index < this.mItemFilterToggles.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mItemFilterToggles[index], (Object) null))
          this.mItemFilterToggles[index].IsOn = (PartyWindow2.ItemFilterTypes) index == itemFilterTypes;
      }
      this.RefreshItemList();
    }

    private static void ToggleBlockRaycasts(Component component, bool block)
    {
      CanvasGroup component1 = (CanvasGroup) component.GetComponent<CanvasGroup>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      component1.set_blocksRaycasts(block);
    }

    private void LockWindow(bool y)
    {
      if (y)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 5);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 6);
    }

    public void Reopen()
    {
      if (this.mCurrentQuest != null && this.mCurrentQuest.iname != GlobalVars.SelectedQuestID)
      {
        this.RefreshQuest();
        this.Refresh(false);
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
    }

    private void ShowRaidSettings()
    {
      if (Object.op_Equality((Object) this.RaidSettingsTemplate, (Object) null) || !Object.op_Equality((Object) this.mRaidSettings, (Object) null))
        return;
      this.mRaidSettings = (RaidSettingsWindow) ((GameObject) Object.Instantiate<GameObject>((M0) this.RaidSettingsTemplate)).GetComponent<RaidSettingsWindow>();
      this.mRaidSettings.OnAccept = new RaidSettingsWindow.RaidSettingsEvent(this.RaidSettingsAccepted);
      this.mRaidSettings.Setup(this.mCurrentQuest, this.mMultiRaidNum, this.MaxRaidNum);
    }

    private bool PrepareRaid(int num, bool validateOnly, bool skipConfirm = false)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int num1 = this.mCurrentQuest.RequiredApWithPlayerLv(player.Lv, true);
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
      int num2 = itemDataByItemId == null ? 0 : itemDataByItemId.Num;
      this.mNumRaids = num;
      if (this.mCurrentQuest.GetChallangeLimit() > 0)
        this.mNumRaids = Mathf.Min(this.mNumRaids, this.mCurrentQuest.GetChallangeLimit() - this.mCurrentQuest.GetChallangeCount());
      int num3 = num1 * this.mNumRaids;
      if (player.Stamina < num3)
      {
        MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true);
        return false;
      }
      ItemParam itemParam = itemDataByItemId == null ? MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket) : itemDataByItemId.Param;
      if (num2 < this.mNumRaids)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.NO_RAID_TICKET", new object[1]
        {
          (object) itemParam.name
        }), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
        return false;
      }
      if (validateOnly)
        return false;
      if (skipConfirm)
      {
        this.OnRaidAccept((GameObject) null);
        return true;
      }
      UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RAID", (object) this.mNumRaids, (object) num3, (object) itemParam.name), (string) null, new UIUtility.DialogResultEvent(this.OnRaidAccept), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      return true;
    }

    public void RaidSettingsAccepted(RaidSettingsWindow window)
    {
      this.mMultiRaidNum = window.Count;
      this.PrepareRaid(window.Count, false, true);
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      GameUtility.UnitSortModes unitSortModes = GameUtility.UnitSortModes.Time;
      try
      {
        if (!string.IsNullOrEmpty(method))
          unitSortModes = (GameUtility.UnitSortModes) Enum.Parse(typeof (GameUtility.UnitSortModes), method, true);
      }
      catch (Exception ex)
      {
        if (GameUtility.IsDebugBuild)
          DebugUtility.LogError("Unknown sort mode: " + method);
      }
      if (Object.op_Inequality((Object) this.AscendingIcon, (Object) null))
        this.AscendingIcon.SetActive(ascending);
      if (Object.op_Inequality((Object) this.DescendingIcon, (Object) null))
        this.DescendingIcon.SetActive(!ascending);
      if (unitSortModes == GameUtility.UnitSortModes.Time)
        ascending = !ascending;
      if (Object.op_Inequality((Object) this.SortModeCaption, (Object) null))
        this.SortModeCaption.set_text(LocalizedText.Get("sys.SORT_" + unitSortModes.ToString().ToUpper()));
      this.mReverse = ascending;
      this.mUnitSortMode = unitSortModes;
      this.mUnitFilter = filters;
      if (!this.mUnitSlotSelected)
        return;
      this.RefreshUnitList();
    }

    private void OnEnable()
    {
      UnitJobDropdown.OnJobChange += new UnitJobDropdown.JobChangeEvent(this.OnUnitJobChange);
    }

    private void OnDisable()
    {
      UnitJobDropdown.OnJobChange -= new UnitJobDropdown.JobChangeEvent(this.OnUnitJobChange);
    }

    private void OnUnitJobChange(long unitUniqueID)
    {
      this.Refresh(true);
    }

    private bool IsSoloEventParty(QuestParam quest)
    {
      QuestCondParam entryCondition = quest.EntryCondition;
      if (quest.units != null && entryCondition != null && (entryCondition.unit != null && quest.UseFixEditor))
        return quest.type == QuestTypes.Event;
      return false;
    }

    private bool KyouseiUnitPartyEdit(QuestParam quest, PartyWindow2.PartyEditData edit)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003CKyouseiUnitPartyEdit\u003Ec__AnonStorey264 editCAnonStorey264 = new PartyWindow2.\u003CKyouseiUnitPartyEdit\u003Ec__AnonStorey264();
      // ISSUE: reference to a compiler-generated field
      editCAnonStorey264.edit = edit;
      bool flag = false;
      QuestCondParam entryCondition = quest.EntryCondition;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003CKyouseiUnitPartyEdit\u003Ec__AnonStorey265 editCAnonStorey265 = new PartyWindow2.\u003CKyouseiUnitPartyEdit\u003Ec__AnonStorey265();
      // ISSUE: reference to a compiler-generated field
      editCAnonStorey265.\u003C\u003Ef__ref\u0024612 = editCAnonStorey264;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (editCAnonStorey265.i = 0; editCAnonStorey265.i < editCAnonStorey264.edit.Units.Length; ++editCAnonStorey265.i)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (editCAnonStorey264.edit.Units[editCAnonStorey265.i] != null)
        {
          // ISSUE: reference to a compiler-generated method
          if (Array.FindIndex<string>(quest.units, new Predicate<string>(editCAnonStorey265.\u003C\u003Em__2C2)) != -1)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            editCAnonStorey264.edit.Units[editCAnonStorey265.i] = (UnitData) null;
            flag = true;
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            if (Array.FindIndex<string>(entryCondition.unit, new Predicate<string>(editCAnonStorey265.\u003C\u003Em__2C3)) == -1)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              editCAnonStorey264.edit.Units[editCAnonStorey265.i] = (UnitData) null;
              flag = true;
            }
          }
        }
      }
      return flag;
    }

    private bool AutoSetLeaderUnit(PartyWindow2.PartyEditData party, string[] kyouseiUnitIds, List<UnitData> units)
    {
      if (party.Units[0] != null)
        return false;
      if (kyouseiUnitIds == null && units != null && units.Count >= 1)
      {
        party.Units[0] = units[0];
        return true;
      }
      for (int index = 0; index < units.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PartyWindow2.\u003CAutoSetLeaderUnit\u003Ec__AnonStorey266 unitCAnonStorey266 = new PartyWindow2.\u003CAutoSetLeaderUnit\u003Ec__AnonStorey266();
        // ISSUE: reference to a compiler-generated field
        unitCAnonStorey266.u = units[index];
        // ISSUE: reference to a compiler-generated method
        if (-1 == Array.FindIndex<string>(kyouseiUnitIds, new Predicate<string>(unitCAnonStorey266.\u003C\u003Em__2C4)))
        {
          // ISSUE: reference to a compiler-generated field
          party.Units[0] = unitCAnonStorey266.u;
          break;
        }
      }
      return party.Units[0] != null;
    }

    public enum EditPartyTypes
    {
      Auto,
      Normal,
      Event,
      MP,
      Arena,
      ArenaDef,
      Character,
      Tower,
      Versus,
    }

    private enum ItemFilterTypes
    {
      All,
      Offense,
      Support,
    }

    protected class Json_ReqSupporterResponse
    {
      public Json_Support[] supports;
    }

    public class JSON_ReqBtlComRaidResponse
    {
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public BattleCore.Json_BtlInfo[] btlinfos;
    }

    public class JSON_ReqBtlComResetResponse
    {
      public Json_PlayerData player;
      public JSON_QuestProgress[] quests;
    }

    protected class PartyEditData
    {
      public UnitData[] Units;
      public PartyData PartyData;

      public PartyEditData(UnitData[] src, PartyData party)
      {
        this.Units = new UnitData[party.MAX_UNIT];
        this.PartyData = party;
        this.SetUnits(src);
      }

      public PartyEditData(PartyData party)
      {
        this.Units = new UnitData[party.MAX_UNIT];
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        this.PartyData = party;
        for (int index = 0; index < party.MAX_UNIT; ++index)
        {
          long unitUniqueId = party.GetUnitUniqueID(index);
          if (unitUniqueId > 0L)
            this.Units[index] = player.FindUnitDataByUniqueID(unitUniqueId);
        }
      }

      public PartyEditData(JSON_MyPhotonPlayerParam src)
      {
        for (int index = 0; index < src.units.Length; ++index)
        {
          JSON_MyPhotonPlayerParam.UnitDataElem unit = src.units[index];
          if (0 <= unit.slotID && unit.slotID < this.PartyData.MAX_UNIT)
            this.Units[index] = unit.unit;
        }
      }

      public int IndexOf(UnitData unit)
      {
        for (int index = 0; index < this.Units.Length; ++index)
        {
          if (this.Units[index] != null && this.Units[index].UniqueID == unit.UniqueID)
            return index;
        }
        return -1;
      }

      public void SetUnits(UnitData[] src)
      {
        for (int index = 0; index < src.Length && index < this.Units.Length; ++index)
          this.Units[index] = src[index];
        for (int mainmemberStart = this.PartyData.MAINMEMBER_START; mainmemberStart < this.PartyData.MAINMEMBER_START + this.PartyData.MAX_MAINMEMBER; ++mainmemberStart)
        {
          if (this.Units[mainmemberStart] == null)
          {
            for (int index = mainmemberStart + 1; index < this.PartyData.MAINMEMBER_START + this.PartyData.MAX_MAINMEMBER; ++index)
            {
              if (this.Units[index] != null)
                this.Units[mainmemberStart++] = this.Units[index];
            }
            while (mainmemberStart < this.PartyData.MAINMEMBER_START + this.PartyData.MAX_MAINMEMBER)
              this.Units[mainmemberStart++] = (UnitData) null;
          }
        }
        for (int submemberStart = this.PartyData.SUBMEMBER_START; submemberStart < this.PartyData.SUBMEMBER_START + this.PartyData.MAX_SUBMEMBER; ++submemberStart)
        {
          if (this.Units[submemberStart] == null)
          {
            for (int index = submemberStart + 1; index < this.PartyData.SUBMEMBER_START + this.PartyData.MAX_SUBMEMBER; ++index)
            {
              if (this.Units[index] != null)
                this.Units[submemberStart++] = this.Units[index];
            }
            while (submemberStart < this.PartyData.SUBMEMBER_START + this.PartyData.MAX_SUBMEMBER)
              this.Units[submemberStart++] = (UnitData) null;
          }
        }
      }
    }

    private class JSON_Team
    {
      public long[] u;
    }

    private class JSON_TeamSettings
    {
      public int n;
      public PartyWindow2.JSON_Team[] t;
    }

    private delegate void Callback();
  }
}
