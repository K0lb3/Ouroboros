// Decompiled with JetBrains decompiler
// Type: SRPG.PartyWindow2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(21, "アイテム選択完了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(111, "サポートユニット解除", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(220, "サポートが解除された", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(40, "チーム名変更完了", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(110, "ユニット解除", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(101, "サポートユニット選択", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "ユニット選択", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(8, "マルチタワー用進む", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(41, "おまかせ編成完了", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(1, "進む", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(5, "画面ロック", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(3, "戻る", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(4, "開く", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(10, "ユニット選択開始", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "ユニット選択完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(200, "フレンドがサポートに設定された", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(210, "フレンド以外がサポートが設定された", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(20, "アイテム選択開始", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(120, "ユニットリストウィンド閉じ始めた", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(119, "ユニットリストウィンド開いた", FlowNode.PinTypes.Input, 119)]
  [FlowNode.Pin(7, "AP回復アイテム", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(6, "画面アンロック", FlowNode.PinTypes.Output, 6)]
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
    public GameObject[] StoryNormalObjects;
    public GameObject[] HeloOnlyObjects;
    [Space(10f)]
    public GameObject AddMainUnitOverlay;
    public GameObject AddSubUnitOverlay;
    public GameObject AddItemOverlay;
    [Space(10f)]
    public GenericSlot[] ItemSlots;
    [Space(10f)]
    public FixedScrollablePulldown TeamPulldown;
    [Space(10f)]
    public Text TotalAtk;
    public GenericSlot LeaderSkill;
    public GenericSlot SupportSkill;
    public GameObject QuestInfo;
    public SRPG_Button QuestInfoButton;
    [StringIsResourcePath(typeof (GameObject))]
    public string QuestDetail;
    [StringIsResourcePath(typeof (GameObject))]
    public string QuestDetailMulti;
    public bool ShowQuestInfo;
    public bool UseQuestInfo;
    public bool ShowRaidInfo;
    public SRPG_Button ForwardButton;
    public bool ShowForwardButton;
    public SRPG_Button BackButton;
    public bool ShowBackButton;
    public GameObject NoItemText;
    public GameObject Prefab_SankaFuka;
    public float SankaFukaOpacity;
    public SRPG_Button RecommendTeamButton;
    public SRPG_Button BreakupButton;
    public SRPG_Button RenameButton;
    public SRPG_Button PrevButton;
    public SRPG_Button NextButton;
    public SRPG_Button RecentTeamButton;
    public Text TextFixParty;
    [Space(10f)]
    public RectTransform[] ChosenUnitBadges;
    public RectTransform[] ChosenItemBadges;
    public RectTransform ChosenSupportBadge;
    [Space(10f)]
    public RectTransform MainRect;
    public VirtualList UnitList;
    public ListItemEvents UnitListItem;
    public SRPG_Button UnitRemoveItem;
    public RectTransform UnitListHilit;
    public string UnitListHilitParent;
    public SRPG_Button CloseUnitList;
    public Pulldown UnitListFilter;
    public GameObject NoMatchingUnit;
    public bool AlwaysShowRemoveUnit;
    public Text SortModeCaption;
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
    public Text RaidTicketNum;
    public SRPG_Button Raid;
    public SRPG_Button RaidN;
    public Text RaidNCount;
    [StringIsResourcePath(typeof (RaidResultWindow))]
    public string RaidResultPrefab;
    public GameObject RaidSettingsTemplate;
    [Space(10f)]
    public Toggle ToggleDirectineCut;
    [Space(10f)]
    public QuestCampaignList QuestCampaigns;
    [Space(10f)]
    public GameObject QuestUnitCond;
    public PlayerPartyTypes[] SaveJobs;
    [Space(10f)]
    public bool EnableHeroSolo;
    [Space(10f)]
    public SRPG_Button BattleSettingButton;
    public SRPG_Button HelpButton;
    public GameObject Filter;
    [Space(10f)]
    public string UNIT_LIST_PATH;
    [Space(10f)]
    public string UNITLIST_WINDOW_PATH;
    private UnitListWindow mUnitListWindow;
    protected List<UnitData> mOwnUnits;
    private List<ItemData> mOwnItems;
    protected QuestParam mCurrentQuest;
    protected PartyEditData mCurrentParty;
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
    private List<PartyEditData> mTeams;
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
    private string[] mUnitFilter;
    private bool mReverse;
    private SRPG_ToggleButton[] mItemFilterToggles;
    private PartyWindow2.ItemFilterTypes mItemFilter;
    protected GameObject[] mSankaFukaIcons;
    private RaidSettingsWindow mRaidSettings;
    private int mMultiRaidNum;
    private bool mInitialized;
    private bool mIsHeloOnly;
    [Space(10f)]
    public SRPG_Button ButtonMapEffectQuest;
    [StringIsResourcePath(typeof (GameObject))]
    public string PrefabMapEffectQuest;
    private LoadRequest mReqMapEffectQuest;
    private GameObject mMultiErrorMsg;
    private Transform mTrHomeHeader;
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

    private void OpenQuestDetail()
    {
      if (this.mCurrentQuest == null || this.mReqQuestDetail == null || (!this.mReqQuestDetail.isDone || !UnityEngine.Object.op_Inequality(this.mReqQuestDetail.asset, (UnityEngine.Object) null)))
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate(this.mReqQuestDetail.asset) as GameObject;
      DataSource.Bind<QuestParam>(gameObject, this.mCurrentQuest);
      if (this.mGuestUnit != null && this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character)
        DataSource.Bind<UnitData>(gameObject, this.mGuestUnit);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
      DataSource.Bind<QuestCampaignData[]>(gameObject, questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      gameObject.SetActive(true);
    }

    private Transform TrHomeHeader
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTrHomeHeader, (UnityEngine.Object) null))
        {
          Scene sceneByName = SceneManager.GetSceneByName(GameUtility.SceneNameHome());
          if (((Scene) @sceneByName).IsValid())
          {
            GameObject[] rootGameObjects = ((Scene) @sceneByName).GetRootGameObjects();
            if (rootGameObjects != null)
            {
              foreach (GameObject gameObject in rootGameObjects)
              {
                HomeWindow component = (HomeWindow) gameObject.GetComponent<HomeWindow>();
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
                {
                  this.mTrHomeHeader = ((Component) component).get_transform();
                  break;
                }
              }
            }
          }
        }
        return this.mTrHomeHeader;
      }
    }

    private void OpenMapEffectQuest()
    {
      if (this.mCurrentQuest == null || this.mReqMapEffectQuest == null || (!this.mReqMapEffectQuest.isDone || UnityEngine.Object.op_Equality(this.mReqMapEffectQuest.asset, (UnityEngine.Object) null)))
        return;
      Transform parent = this.TrHomeHeader;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) parent))
        parent = ((Component) this).get_transform();
      GameObject instance = MapEffectQuest.CreateInstance(this.mReqMapEffectQuest.asset as GameObject, parent);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return;
      DataSource.Bind<QuestParam>(instance, this.mCurrentQuest);
      instance.SetActive(true);
      MapEffectQuest component = (MapEffectQuest) instance.GetComponent<MapEffectQuest>();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) component))
        return;
      component.Setup();
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CStart\u003Ec__Iterator107() { \u003C\u003Ef__this = this };
    }

    protected virtual void SetFriendSlot()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        return;
      this.FriendSlot.OnSelect = new GenericSlot.SelectEvent(this.OnUnitSlotClick);
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<GameManager>.GetInstanceDirect(), (UnityEngine.Object) null))
      {
        MonoSingleton<GameManager>.Instance.OnStaminaChange -= new GameManager.StaminaChangeEvent(this.OnStaminaChange);
        MonoSingleton<GameManager>.Instance.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnHomeMenuChange);
      }
      GameUtility.DestroyGameObject((Component) this.UnitListHilit);
      GameUtility.DestroyGameObject((Component) this.ItemListHilit);
      GameUtility.DestroyGameObjects<RectTransform>(this.ChosenUnitBadges);
      GameUtility.DestroyGameObjects<RectTransform>(this.ChosenItemBadges);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenSupportBadge, (UnityEngine.Object) null))
        ((Component) this.ChosenSupportBadge).get_transform().SetParent(((Component) this).get_transform(), false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitRemoveItem, (UnityEngine.Object) null))
        ((Component) this.UnitRemoveItem).get_transform().SetParent(((Component) this).get_transform(), false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemRemoveItem, (UnityEngine.Object) null))
        ((Component) this.ItemRemoveItem).get_transform().SetParent(((Component) this).get_transform(), false);
      GameUtility.DestroyGameObjects<RectTransform>(this.mItemPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mItemPoolB);
      GameUtility.DestroyGameObjects<RectTransform>(this.mUnitPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mUnitPoolB);
      GameUtility.DestroyGameObjects<RectTransform>(this.mSupportPoolA);
      GameUtility.DestroyGameObjects<RectTransform>(this.mSupportPoolB);
      GameUtility.DestroyGameObject((Component) this.mRaidSettings);
      this.UnitList_Remove();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMultiErrorMsg, (UnityEngine.Object) null))
        return;
      UIUtility.PopCanvas();
      this.mMultiErrorMsg = (GameObject) null;
    }

    private void OnCloseItemListClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void AttachAndEnable(Transform go, Transform parent, string subPath)
    {
      if (!string.IsNullOrEmpty(subPath))
      {
        Transform child = parent.FindChild(subPath);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
          parent = child;
      }
      go.SetParent(parent, false);
      ((Component) go).get_gameObject().SetActive(true);
    }

    private void MoveToOrigin(GameObject go)
    {
      RectTransform component = (RectTransform) go.GetComponent<RectTransform>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.set_anchoredPosition(Vector2.get_zero());
    }

    private void ChangeEnabledTeamButtons(int index, int max)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        ((Selectable) this.NextButton).set_interactable(index < max - 1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.PrevButton).set_interactable(index > 0);
    }

    private void OnNextTeamChange()
    {
      if (!this.OnTeamChangeImpl(this.mCurrentTeamIndex + 1))
        return;
      this.TeamPulldown.Selection = this.mCurrentTeamIndex;
    }

    private void OnPrevTeamChange()
    {
      if (!this.OnTeamChangeImpl(this.mCurrentTeamIndex - 1))
        return;
      this.TeamPulldown.Selection = this.mCurrentTeamIndex;
    }

    private bool OnTeamChangeImpl(int index)
    {
      if (this.mCurrentTeamIndex == index || index >= this.mTeams.Count || index < 0)
        return false;
      this.ChangeEnabledTeamButtons(index, this.mTeams.Count);
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
      if (this.EnableHeroSolo && PartyUtility.IsSoloStoryOrEventParty(this.mCurrentQuest))
        PartyUtility.KyouseiUnitPartyEdit(this.mCurrentQuest, this.mTeams[index]);
      this.mCurrentTeamIndex = index;
      this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
      for (int slotIndex = 0; slotIndex < this.mCurrentParty.PartyData.MAX_UNIT; ++slotIndex)
        this.SetPartyUnit(slotIndex, this.mTeams[this.mCurrentTeamIndex].Units[slotIndex]);
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      return true;
    }

    private void OnTeamChange(int index)
    {
      this.OnTeamChangeImpl(index);
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
            if (index >= 0 && index < this.ChosenUnitBadges.Length && !UnityEngine.Object.op_Equality((UnityEngine.Object) this.ChosenUnitBadges[index], (UnityEngine.Object) null))
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Transform) this.ChosenUnitBadges[index]).get_parent(), (UnityEngine.Object) rectTransform))
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenUnitBadges[index], (UnityEngine.Object) null) && (num & 1 << index) == 0)
        {
          ((Transform) this.ChosenUnitBadges[index]).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ChosenUnitBadges[index]).get_gameObject().SetActive(false);
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenUnitBadges[0], (UnityEngine.Object) null))
        return;
      ((Component) this.ChosenUnitBadges[0]).get_gameObject().SetActive(!this.mIsHeloOnly);
    }

    private RectTransform OnGetItemListItem(int id, int old, RectTransform current)
    {
      if (id == 0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemRemoveItem, (UnityEngine.Object) null))
          return ((Component) this.ItemRemoveItem).get_transform() as RectTransform;
        return (RectTransform) null;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ItemListItem, (UnityEngine.Object) null))
        return (RectTransform) null;
      RectTransform rectTransform;
      if (old <= 0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemRemoveItem, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) this.ItemRemoveItem).get_transform(), (UnityEngine.Object) current))
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemListHilit, (UnityEngine.Object) null))
      {
        if (this.mSelectedSlotIndex == index1)
          this.AttachAndEnable((Transform) this.ItemListHilit, (Transform) rectTransform, this.ItemListHilitParent);
        else if (UnityEngine.Object.op_Equality((UnityEngine.Object) ((Transform) this.ItemListHilit).get_parent(), (UnityEngine.Object) ((Transform) rectTransform).FindChild(this.ItemListHilitParent)))
        {
          ((Transform) this.ItemListHilit).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ItemListHilit).get_gameObject().SetActive(false);
        }
      }
      if (index1 >= 0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenItemBadges[index1], (UnityEngine.Object) null))
        {
          ((Transform) this.ChosenItemBadges[index1]).SetParent((Transform) rectTransform, false);
          ((Component) this.ChosenItemBadges[index1]).get_gameObject().SetActive(true);
        }
      }
      else
      {
        for (int index2 = 0; index2 < this.ChosenItemBadges.Length; ++index2)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenItemBadges[index2], (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) ((Transform) this.ChosenItemBadges[index2]).get_parent(), (UnityEngine.Object) rectTransform))
          {
            ((Transform) this.ChosenItemBadges[index2]).SetParent((Transform) UIUtility.Pool, false);
            ((Component) this.ChosenItemBadges[index2]).get_gameObject().SetActive(false);
            break;
          }
        }
      }
      return rectTransform;
    }

    private ListItemEvents CreateItemListItem()
    {
      ListItemEvents listItemEvents = (ListItemEvents) UnityEngine.Object.Instantiate<ListItemEvents>((M0) this.ItemListItem);
      listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
      return listItemEvents;
    }

    private void RefreshItemList()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003CRefreshItemList\u003Ec__AnonStorey352 listCAnonStorey352 = new PartyWindow2.\u003CRefreshItemList\u003Ec__AnonStorey352();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey352.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey352.currentItem = this.mCurrentItems[this.mSelectedSlotIndex];
      this.ItemList.ClearItems();
      // ISSUE: reference to a compiler-generated field
      if (listCAnonStorey352.currentItem != null || this.AlwaysShowRemoveItem)
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
            if (itemDataList[index].ItemType == EItemType.Used && itemDataList[index].Skill != null && (itemDataList[index].Skill.EffectType == SkillEffectTypes.Attack || itemDataList[index].Skill.EffectType == SkillEffectTypes.Debuff || (itemDataList[index].Skill.EffectType == SkillEffectTypes.FailCondition || itemDataList[index].Skill.EffectType == SkillEffectTypes.RateDamage) || itemDataList[index].Skill.EffectType == SkillEffectTypes.RateDamageCurrent))
              this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataList[index]) + 1);
          }
          break;
        case PartyWindow2.ItemFilterTypes.Support:
          for (int index = 0; index < itemDataList.Count; ++index)
          {
            if (itemDataList[index].ItemType == EItemType.Used && itemDataList[index].Skill != null && (itemDataList[index].Skill.EffectType == SkillEffectTypes.Heal || itemDataList[index].Skill.EffectType == SkillEffectTypes.RateHeal || (itemDataList[index].Skill.EffectType == SkillEffectTypes.Buff || itemDataList[index].Skill.EffectType == SkillEffectTypes.CureCondition) || (itemDataList[index].Skill.EffectType == SkillEffectTypes.DisableCondition || itemDataList[index].Skill.EffectType == SkillEffectTypes.Revive || itemDataList[index].Skill.EffectType == SkillEffectTypes.GemsIncDec)))
              this.ItemList.AddItem(this.mOwnItems.IndexOf(itemDataList[index]) + 1);
          }
          break;
      }
      this.ItemList.Refresh(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemListHilit, (UnityEngine.Object) null))
      {
        ((Component) this.ItemListHilit).get_gameObject().SetActive(false);
        ((Transform) this.ItemListHilit).SetParent(((Component) this).get_transform(), false);
        // ISSUE: reference to a compiler-generated field
        if (listCAnonStorey352.currentItem != null)
        {
          // ISSUE: reference to a compiler-generated method
          int itemID = this.mOwnItems.FindIndex(new Predicate<ItemData>(listCAnonStorey352.\u003C\u003Em__3B9)) + 1;
          if (itemID > 0)
          {
            RectTransform rectTransform = this.ItemList.FindItem(itemID);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
              this.AttachAndEnable((Transform) this.ItemListHilit, (Transform) rectTransform, this.ItemListHilitParent);
          }
        }
      }
      for (int index = 0; index < this.ChosenItemBadges.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ChosenItemBadges[index], (UnityEngine.Object) null))
        {
          ((Transform) this.ChosenItemBadges[index]).SetParent((Transform) UIUtility.Pool, false);
          ((Component) this.ChosenItemBadges[index]).get_gameObject().SetActive(false);
        }
      }
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003CRefreshItemList\u003Ec__AnonStorey353 listCAnonStorey353 = new PartyWindow2.\u003CRefreshItemList\u003Ec__AnonStorey353();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey353.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (listCAnonStorey353.i = 0; listCAnonStorey353.i < this.ChosenItemBadges.Length; ++listCAnonStorey353.i)
      {
        // ISSUE: reference to a compiler-generated field
        if (this.mCurrentItems[listCAnonStorey353.i] != null)
        {
          // ISSUE: reference to a compiler-generated method
          RectTransform rectTransform = this.ItemList.FindItem(this.mOwnItems.FindIndex(new Predicate<ItemData>(listCAnonStorey353.\u003C\u003Em__3BA)) + 1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
          {
            // ISSUE: reference to a compiler-generated field
            ((Transform) this.ChosenItemBadges[listCAnonStorey353.i]).SetParent((Transform) rectTransform, false);
            // ISSUE: reference to a compiler-generated field
            ((Component) this.ChosenItemBadges[listCAnonStorey353.i]).get_gameObject().SetActive(true);
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
      this.SaveInventory();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void OnSupportRemoveSelect(SRPG_Button button)
    {
      this.SetSupport((SupportData) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
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
          AnalyticsManager.TrackNonPremiumCurrencyUse(AnalyticsManager.NonPremiumCurrencyType.Zeni, (long) this.mSelectedSupport.GetCost(), "Hire Mercenary", (string) null);
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
      this.SaveInventory();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 21);
    }

    private void OnPartyMemberChange()
    {
      this.RefreshSankaStates();
      this.RecalcTotalAttack();
      this.UpdateLeaderSkills();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddMainUnitOverlay, (UnityEngine.Object) null))
      {
        this.AddMainUnitOverlay.SetActive(false);
        for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
        {
          if (this.mCurrentParty.Units[mainmemberStart] == null && mainmemberStart < this.UnitSlots.Length && (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[mainmemberStart], (UnityEngine.Object) null) && (this.mLockedPartySlots & 1 << mainmemberStart) == 0))
          {
            this.AddMainUnitOverlay.get_transform().SetParent(((Component) this.UnitSlots[mainmemberStart]).get_transform(), false);
            this.AddMainUnitOverlay.SetActive(true);
            break;
          }
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddSubUnitOverlay, (UnityEngine.Object) null))
        return;
      this.AddSubUnitOverlay.SetActive(false);
      for (int submemberStart = this.mCurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.mCurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
      {
        if (this.mCurrentParty.Units[submemberStart] == null && submemberStart < this.UnitSlots.Length && (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[submemberStart], (UnityEngine.Object) null) && (this.mLockedPartySlots & 1 << submemberStart) == 0))
        {
          this.AddSubUnitOverlay.get_transform().SetParent(((Component) this.UnitSlots[submemberStart]).get_transform(), false);
          this.AddSubUnitOverlay.SetActive(true);
          break;
        }
      }
    }

    protected virtual void OnItemSlotsChange()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddItemOverlay, (UnityEngine.Object) null))
        return;
      this.AddItemOverlay.SetActive(false);
      for (int index = 0; index < this.mCurrentItems.Length; ++index)
      {
        if (this.mCurrentItems[index] == null && index < this.ItemSlots.Length && (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlots[index], (UnityEngine.Object) null) && !this.mItemsLocked))
        {
          this.AddItemOverlay.get_transform().SetParent(((Component) this.ItemSlots[index]).get_transform(), false);
          this.AddItemOverlay.SetActive(true);
          break;
        }
      }
    }

    private void UpdateLeaderSkills()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LeaderSkill, (UnityEngine.Object) null))
      {
        SkillParam data = (SkillParam) null;
        if (this.mIsHeloOnly)
        {
          if (this.mGuestUnit != null && this.mGuestUnit.LeaderSkill != null)
            data = this.mGuestUnit.LeaderSkill.SkillParam;
        }
        else if (this.mCurrentParty.Units[0] != null)
        {
          if (this.mCurrentParty.Units[0].LeaderSkill != null)
            data = this.mCurrentParty.Units[0].LeaderSkill.SkillParam;
        }
        else if (this.EnableHeroSolo && this.mGuestUnit != null && this.mGuestUnit.LeaderSkill != null)
          data = this.mGuestUnit.LeaderSkill.SkillParam;
        this.LeaderSkill.SetSlotData<SkillParam>(data);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SupportSkill, (UnityEngine.Object) null))
        return;
      SkillParam data1 = (SkillParam) null;
      if (this.mCurrentSupport != null && this.mCurrentSupport.Unit.LeaderSkill != null)
        data1 = this.mCurrentSupport.Unit.LeaderSkill.SkillParam;
      this.SupportSkill.SetSlotData<SkillParam>(data1);
    }

    private void OnSlotChange(GameObject go)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) go, (UnityEngine.Object) null) || string.IsNullOrEmpty(this.SlotChangeTrigger))
        return;
      Animator component = (Animator) go.GetComponent<Animator>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.SetTrigger(this.SlotChangeTrigger);
    }

    private void SetSupport(SupportData support)
    {
      this.mCurrentSupport = support;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        return;
      this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
      this.FriendSlot.SetSlotData<SupportData>(support);
      if (support != null)
        this.FriendSlot.SetSlotData<UnitData>(support.Unit);
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
    private IEnumerator PopulateItemList()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CPopulateItemList\u003Ec__Iterator108() { \u003C\u003Ef__this = this };
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalAtk, (UnityEngine.Object) null))
        return;
      this.TotalAtk.set_text(num.ToString());
    }

    private bool IsMultiTowerPartySlot(int index)
    {
      return this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MultiTower && (index == 0 || index == 1 || index == 2);
    }

    protected void OnUnitSlotClick(GenericSlot slot, bool interactable)
    {
      if (!this.mInitialized || this.mIsSaving)
        return;
      this.Refresh(true);
      if (this.GetPartyCondType() != PartyCondType.Forced)
      {
        int index = Array.IndexOf<GenericSlot>(this.UnitSlots, slot);
        if (0 <= index && index < this.mCurrentParty.PartyData.MAX_UNIT)
        {
          this.mUnitSlotSelected = true;
          this.mSelectedSlotIndex = index;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitRemoveItem, (UnityEngine.Object) null))
            ((Selectable) this.UnitRemoveItem).set_interactable(index > 0 || this.mIsHeloOnly);
          this.UnitList_Show();
          return;
        }
        if (this.IsMultiTowerPartySlot(index))
        {
          this.mUnitSlotSelected = true;
          this.mSelectedSlotIndex = index;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitRemoveItem, (UnityEngine.Object) null))
            ((Selectable) this.UnitRemoveItem).set_interactable(index > 0 || this.mIsHeloOnly);
          this.UnitList_Show();
          return;
        }
      }
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) slot))
        return;
      this.mUnitSlotSelected = true;
      this.SupportList_Show();
    }

    private UnitData[] RefreshUnits(UnitData[] units)
    {
      List<UnitData> unitDataList1 = new List<UnitData>((IEnumerable<UnitData>) this.mOwnUnits);
      List<UnitData> unitDataList2 = new List<UnitData>();
      bool flag1 = this.mCurrentParty.Units[this.mSelectedSlotIndex] == null;
      bool flag2 = PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest);
      if (this.UseQuestInfo && (this.mCurrentQuest.type == QuestTypes.Event || this.mCurrentQuest.type == QuestTypes.Beginner || this.PartyType == PartyWindow2.EditPartyTypes.Character) && (this.mCurrentQuest.units.IsNotNull() && this.mCurrentQuest.units.Length > 0))
      {
        for (int index = 0; index < this.mCurrentQuest.units.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: reference to a compiler-generated method
          UnitData unitData = Array.Find<UnitData>(unitDataList1.ToArray(), new Predicate<UnitData>(new PartyWindow2.\u003CRefreshUnits\u003Ec__AnonStorey354() { chQuestHeroId = this.mCurrentQuest.units.Get(index) }.\u003C\u003Em__3BB));
          if (unitData != null)
            unitDataList1.Remove(unitData);
        }
      }
      int num = 0;
      for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
      {
        if (this.mCurrentParty.Units[mainmemberStart] != null)
          ++num;
      }
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003CRefreshUnits\u003Ec__AnonStorey355 unitsCAnonStorey355 = new PartyWindow2.\u003CRefreshUnits\u003Ec__AnonStorey355();
      // ISSUE: reference to a compiler-generated field
      unitsCAnonStorey355.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      for (unitsCAnonStorey355.i = 0; unitsCAnonStorey355.i < this.mCurrentParty.PartyData.MAX_UNIT; ++unitsCAnonStorey355.i)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (this.mCurrentParty.Units[unitsCAnonStorey355.i] != null && (flag2 || (int) this.mCurrentParty.Units[unitsCAnonStorey355.i].UnitParam.hero == 0) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (unitsCAnonStorey355.i != 0 || !flag1) || num > 1))
        {
          if (this.UseQuestInfo)
          {
            string empty = string.Empty;
            // ISSUE: reference to a compiler-generated field
            if (!this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units[unitsCAnonStorey355.i], ref empty))
              continue;
          }
          // ISSUE: reference to a compiler-generated method
          UnitData unitData = unitDataList1.Find(new Predicate<UnitData>(unitsCAnonStorey355.\u003C\u003Em__3BC));
          if (unitData != null)
            unitDataList2.Add(unitData);
          // ISSUE: reference to a compiler-generated method
          int index = unitDataList1.FindIndex(new Predicate<UnitData>(unitsCAnonStorey355.\u003C\u003Em__3BD));
          if (index >= 0)
            unitDataList1.RemoveAt(index);
        }
      }
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag3 = UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentState == MyPhoton.MyState.ROOM;
      List<UnitData> unitDataList3 = new List<UnitData>();
      for (int index = 0; index < unitDataList1.Count; ++index)
      {
        if ((flag2 || (int) unitDataList1[index].UnitParam.hero == 0) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (unitDataList1[index] != this.mCurrentParty.Units[0] || !flag1) || num > 1))
        {
          if (flag3)
          {
            MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              if (unitDataList1[index].CalcLevel() < myPhotonRoomParam.unitlv)
                continue;
            }
          }
          unitDataList3.Add(unitDataList1[index]);
        }
      }
      unitDataList2.AddRange((IEnumerable<UnitData>) unitDataList3);
      return unitDataList2.ToArray();
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
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.Raid))
        this.PrepareRaid(1, !((Selectable) button).IsInteractable(), false);
      else
        this.ShowRaidSettings();
    }

    private void OnRaidAccept(GameObject go)
    {
      if (this.mNumRaids <= 0)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRaidSettings, (UnityEngine.Object) null))
      {
        this.mRaidSettings.Close();
        this.mRaidSettings = (RaidSettingsWindow) null;
      }
      this.LockWindow(true);
      CriticalSection.Enter(CriticalSections.Network);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRaidResultWindow, (UnityEngine.Object) null) && this.mReqRaidResultWindow == null)
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
        if (this.mCurrentQuest.units.IsNotNull())
        {
          for (int index = 0; index < this.mCurrentQuest.units.Length; ++index)
          {
            UnitData unitDataByUnitId = instance.Player.FindUnitDataByUnitID(this.mCurrentQuest.units.Get(index));
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
            raidQuestResult.pexp = this.mCurrentQuest.pexp;
            raidQuestResult.uexp = this.mCurrentQuest.uexp;
            raidQuestResult.gold = this.mCurrentQuest.gold;
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
          player.OnQuestWin(this.mCurrentQuest.iname, (BattleCore.Record) null);
        this.mRaidResult.pexp = player.Exp - exp;
        this.mRaidResult.uexp = this.mCurrentQuest.uexp * this.mNumRaids;
        this.mRaidResult.gold = this.mCurrentQuest.gold * this.mNumRaids;
        player.OnGoldChange(this.mRaidResult.gold);
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
      return (IEnumerator) new PartyWindow2.\u003CShowRaidResultAsync\u003Ec__Iterator109() { \u003C\u003Ef__this = this };
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

    private void HardQuestDropPiecesUpdate()
    {
      GameObject gameObject = GameObjectID.FindGameObject("WorldMapQuestList");
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      GameParameter.UpdateAll(gameObject);
    }

    private void OnForwardOrBackButtonClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.ForwardButton))
      {
        if (this.mCurrentQuest != null && !this.mCurrentQuest.CheckEnableChallange())
        {
          if (!this.mCurrentQuest.CheckEnableReset())
          {
            string empty = string.Empty;
            UIUtility.NegativeSystemMessage((string) null, this.mCurrentQuest.dayReset <= 0 ? LocalizedText.Get("sys.QUEST_SPAN_CHALLENGE_NO_RESET") : LocalizedText.Get("sys.QUEST_CHALLENGE_NO_RESET"), (UIUtility.DialogResultEvent) (g => {}), (GameObject) null, false, -1);
          }
          else
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            PartyWindow2.\u003COnForwardOrBackButtonClick\u003Ec__AnonStorey356 clickCAnonStorey356 = new PartyWindow2.\u003COnForwardOrBackButtonClick\u003Ec__AnonStorey356();
            // ISSUE: reference to a compiler-generated field
            clickCAnonStorey356.\u003C\u003Ef__this = this;
            FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            int num = (int) fixParam.EliteResetMax - (int) this.mCurrentQuest.dailyReset;
            // ISSUE: reference to a compiler-generated field
            clickCAnonStorey356.coin = 0;
            if (fixParam.EliteResetCosts != null)
            {
              // ISSUE: reference to a compiler-generated field
              clickCAnonStorey356.coin = (int) this.mCurrentQuest.dailyReset >= fixParam.EliteResetCosts.Length ? (int) fixParam.EliteResetCosts[fixParam.EliteResetCosts.Length - 1] : (int) fixParam.EliteResetCosts[(int) this.mCurrentQuest.dailyReset];
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            clickCAnonStorey356.msg = string.Format(LocalizedText.Get("sys.QUEST_CHALLENGE_RESET"), (object) clickCAnonStorey356.coin, (object) num);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            UIUtility.ConfirmBox(clickCAnonStorey356.msg, (string) null, new UIUtility.DialogResultEvent(clickCAnonStorey356.\u003C\u003Em__3C1), (UIUtility.DialogResultEvent) (g => {}), (GameObject) null, false, -1);
          }
        }
        else
        {
          if (this.mCurrentQuest != null)
          {
            if (!this.mCurrentQuest.IsMulti)
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
              if (MonoSingleton<GameManager>.Instance.Player.Stamina < this.mCurrentQuest.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv, true))
              {
                MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true);
                return;
              }
            }
            else
            {
              MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentState == MyPhoton.MyState.ROOM)
              {
                MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
                if (currentRoom != null)
                {
                  JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
                  QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(myPhotonRoomParam.iname);
                  int unitlv = myPhotonRoomParam.unitlv;
                  bool flag = true;
                  if (quest != null && unitlv > 0)
                  {
                    for (int index = 0; index < (int) quest.unitNum; ++index)
                    {
                      UnitData unit = this.mCurrentParty.Units[index];
                      if (unit != null)
                        flag &= unit.CalcLevel() >= unitlv;
                    }
                    if (!flag)
                    {
                      this.mMultiErrorMsg = UIUtility.SystemMessage(LocalizedText.Get("sys.TITLE"), LocalizedText.Get("sys.PARTYEDITOR_ULV"), (UIUtility.DialogResultEvent) (dialog => this.mMultiErrorMsg = (GameObject) null), (GameObject) null, false, -1);
                      return;
                    }
                  }
                }
              }
            }
          }
          if (this.mCurrentQuest != null)
          {
            if (this.mCurrentQuest.IsQuestDrops && UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
            {
              bool flag = QuestDropParam.Instance.IsChangedQuestDrops(this.mCurrentQuest);
              GlobalVars.SetDropTableGeneratedTime();
              if (flag)
              {
                this.HardQuestDropPiecesUpdate();
                if (!QuestDropParam.Instance.IsWarningPopupDisable)
                {
                  UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), (UIUtility.DialogResultEvent) (dialog => this.OpenQuestDetail()), (GameObject) null, false, -1);
                  return;
                }
              }
            }
            int numMainUnits = 0;
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int availableMainMemberSlots = this.AvailableMainMemberSlots;
            List<string> stringList = new List<string>();
            bool flag1 = false;
            for (int index = 0; index < availableMainMemberSlots; ++index)
            {
              if (this.mCurrentParty.Units.Length > index)
              {
                if (this.mCurrentParty.Units[index] != null)
                {
                  ++numMainUnits;
                  ++num3;
                }
                if ((this.mLockedPartySlots & 1 << index) == 0)
                {
                  ++num1;
                  if (this.mCurrentParty.Units[index] != null && !this.mCurrentQuest.IsUnitAllowed(this.mCurrentParty.Units[index]))
                    ++num2;
                }
              }
            }
            if (this.EnableHeroSolo && this.mGuestUnit != null)
            {
              if (numMainUnits < 1)
                flag1 = true;
              ++numMainUnits;
            }
            if (this.mCurrentQuest.units.IsNotNull())
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              PartyWindow2.\u003COnForwardOrBackButtonClick\u003Ec__AnonStorey357 clickCAnonStorey357 = new PartyWindow2.\u003COnForwardOrBackButtonClick\u003Ec__AnonStorey357();
              // ISSUE: reference to a compiler-generated field
              clickCAnonStorey357.\u003C\u003Ef__this = this;
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              for (clickCAnonStorey357.i = 0; clickCAnonStorey357.i < this.mCurrentQuest.units.Length; ++clickCAnonStorey357.i)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated method
                if (this.mCurrentParty.Units[clickCAnonStorey357.i] != null && Array.FindIndex<UnitData>(MonoSingleton<GameManager>.Instance.Player.Units.ToArray(), new Predicate<UnitData>(clickCAnonStorey357.\u003C\u003Em__3C5)) == -1)
                {
                  // ISSUE: reference to a compiler-generated field
                  UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.mCurrentQuest.units.Get(clickCAnonStorey357.i));
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
                  goto label_61;
              }
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty1), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
              return;
            }
label_61:
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
                    }), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
                    return;
                  }
                }
              }
            }
            if (num2 > 0)
            {
              UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_SANKAFUKA"), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
              return;
            }
            if (!flag1 && num3 < num1 && this.PartyType != PartyWindow2.EditPartyTypes.MultiTower)
            {
              UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL"), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
              return;
            }
          }
          this.PostForwardPressed();
        }
      }
      else if (this.PartyType == PartyWindow2.EditPartyTypes.MultiTower)
        this.SaveAndActivatePin(8);
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
      if (this.mCurrentQuest != null && !this.mCurrentQuest.IsMultiTower)
      {
        string empty = string.Empty;
        if (!this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units, ref empty))
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
          return false;
        }
      }
      return true;
    }

    protected virtual void PostForwardPressed()
    {
      GlobalEvent.Invoke("DISABLE_MAINMENU_TOP_COMMAND", (object) null);
      GlobalVars.SelectedSupport.Set(this.mCurrentSupport);
      GlobalVars.SelectedFriendID = this.mCurrentSupport == null ? (string) null : this.mCurrentSupport.FUID;
      if (this.PartyType == PartyWindow2.EditPartyTypes.MultiTower)
        this.SaveAndActivatePin(8);
      else
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
        if (this.EnableHeroSolo && this.mCurrentQuest != null && (this.mCurrentQuest.units.IsNotNull() && this.mCurrentQuest.EntryCondition != null) && (this.mCurrentQuest.EntryCondition.unit != null && this.mCurrentQuest.units.Get(0) == this.mCurrentQuest.EntryCondition.unit[0]))
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
      return (IEnumerator) new PartyWindow2.\u003CWaitForSave\u003Ec__Iterator10A() { \u003C\u003Ef__this = this };
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
      if (!this.UseQuestInfo)
        return;
      QuestParam mCurrentQuest = this.mCurrentQuest;
      this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
      if (this.mCurrentQuest != mCurrentQuest)
        this.mMultiRaidNum = -1;
      DataSource.Bind<QuestParam>(((Component) this).get_gameObject(), this.mCurrentQuest);
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
      DataSource.Bind<QuestCampaignData[]>(((Component) this).get_gameObject(), questCampaigns.Length != 0 ? questCampaigns : (QuestCampaignData[]) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestCampaigns, (UnityEngine.Object) null))
        this.QuestCampaigns.RefreshIcons();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    protected void RefreshNoneQuestInfo(bool keepTeam)
    {
      this.mIsHeloOnly = false;
      if (this.StoryNormalObjects != null)
      {
        for (int index = 0; index < this.StoryNormalObjects.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StoryNormalObjects[index], (UnityEngine.Object) null))
            this.StoryNormalObjects[index].SetActive(!this.mIsHeloOnly);
        }
      }
      if (this.HeloOnlyObjects != null)
      {
        for (int index = 0; index < this.HeloOnlyObjects.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HeloOnlyObjects[index], (UnityEngine.Object) null))
            this.HeloOnlyObjects[index].SetActive(this.mIsHeloOnly);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GuestUnitSlot, (UnityEngine.Object) null))
      {
        if (this.mIsHeloOnly)
          ((Component) this.GuestUnitSlot).get_transform().SetSiblingIndex(0);
        else
          ((Component) this.GuestUnitSlot).get_transform().SetSiblingIndex(4);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_SankaFuka, (UnityEngine.Object) null))
      {
        for (int index = 1; index < this.mSankaFukaIcons.Length && index < this.UnitSlots.Length; ++index)
        {
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSankaFukaIcons[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
          {
            this.mSankaFukaIcons[index] = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.Prefab_SankaFuka);
            RectTransform transform = this.mSankaFukaIcons[index].get_transform() as RectTransform;
            transform.set_anchoredPosition(Vector2.get_zero());
            ((Transform) transform).SetParent(((Component) this.UnitSlots[index]).get_transform(), false);
          }
        }
      }
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
        this.LoadTeam();
      for (int index = 0; index < this.UnitSlots.Length && index < this.mCurrentParty.PartyData.MAX_UNIT; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null) && index < this.mCurrentParty.Units.Length)
        {
          this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
          this.UnitSlots[index].SetSlotData<UnitData>(this.FindUnit(this.mCurrentParty.Units[index]));
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GuestUnitSlot, (UnityEngine.Object) null))
        ((Component) this.GuestUnitSlot).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestInfo, (UnityEngine.Object) null))
        this.QuestInfo.SetActive(false);
      int availableMainMemberSlots = this.AvailableMainMemberSlots;
      for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[mainmemberStart], (UnityEngine.Object) null))
        {
          int num = mainmemberStart - this.mCurrentParty.PartyData.MAINMEMBER_START;
          ((Component) this.UnitSlots[mainmemberStart]).get_gameObject().SetActive(num < availableMainMemberSlots);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ForwardButton, (UnityEngine.Object) null))
      {
        ((Component) this.ForwardButton).get_gameObject().SetActive(this.ShowForwardButton);
        BackHandler component = (BackHandler) ((Component) this.ForwardButton).GetComponent<BackHandler>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          ((Behaviour) component).set_enabled(!this.ShowBackButton);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
        ((Component) this.BackButton).get_gameObject().SetActive(this.ShowBackButton);
      bool flag = this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Tower;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecommendTeamButton, (UnityEngine.Object) null))
      {
        ((Component) this.RecommendTeamButton).get_gameObject().SetActive(true);
        ((Selectable) this.RecommendTeamButton).set_interactable(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BreakupButton, (UnityEngine.Object) null))
      {
        ((Component) this.BreakupButton).get_gameObject().SetActive(true);
        ((Selectable) this.BreakupButton).set_interactable(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RenameButton, (UnityEngine.Object) null))
        ((Component) this.RenameButton).get_gameObject().SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
        ((Component) this.PrevButton).get_gameObject().SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        ((Component) this.NextButton).get_gameObject().SetActive(flag);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecentTeamButton, (UnityEngine.Object) null))
      {
        if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tutorial)
          ((Component) this.RecentTeamButton).get_gameObject().SetActive(false);
        else
          ((Component) this.RecentTeamButton).get_gameObject().SetActive(flag);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextFixParty, (UnityEngine.Object) null))
        ((Component) this.TextFixParty).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BattleSettingButton, (UnityEngine.Object) null))
        ((Component) this.BattleSettingButton).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HelpButton, (UnityEngine.Object) null))
        ((Component) this.HelpButton).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filter, (UnityEngine.Object) null))
        this.Filter.SetActive(true);
      this.ToggleRaidInfo();
      this.RefreshRaidTicketNum();
      this.RefreshRaidButtons();
      this.LockSlots();
      this.OnPartyMemberChange();
      this.LoadInventory();
    }

    public void Refresh(bool keepTeam = false)
    {
      if (!this.UseQuestInfo)
      {
        this.RefreshNoneQuestInfo(keepTeam);
      }
      else
      {
        this.mIsHeloOnly = PartyUtility.IsSoloStoryParty(this.mCurrentQuest);
        if (this.StoryNormalObjects != null)
        {
          for (int index = 0; index < this.StoryNormalObjects.Length; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StoryNormalObjects[index], (UnityEngine.Object) null))
              this.StoryNormalObjects[index].SetActive(!this.mIsHeloOnly);
          }
        }
        if (this.HeloOnlyObjects != null)
        {
          for (int index = 0; index < this.HeloOnlyObjects.Length; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.HeloOnlyObjects[index], (UnityEngine.Object) null))
              this.HeloOnlyObjects[index].SetActive(this.mIsHeloOnly);
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GuestUnitSlot, (UnityEngine.Object) null))
        {
          if (this.mIsHeloOnly)
            ((Component) this.GuestUnitSlot).get_transform().SetSiblingIndex(0);
          else
            ((Component) this.GuestUnitSlot).get_transform().SetSiblingIndex(4);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestInfo, (UnityEngine.Object) null))
        {
          if (this.ShowQuestInfo)
          {
            DataSource.Bind<QuestParam>(this.QuestInfo, this.mCurrentQuest);
            GameParameter.UpdateAll(this.QuestInfo);
            this.QuestInfo.SetActive(true);
          }
          else
            this.QuestInfo.SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_SankaFuka, (UnityEngine.Object) null))
          {
            for (int index = 0; index < this.mSankaFukaIcons.Length && index < this.UnitSlots.Length; ++index)
            {
              if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSankaFukaIcons[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
              {
                this.mSankaFukaIcons[index] = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.Prefab_SankaFuka);
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
            this.mCurrentPartyType = PartyUtility.GetEditPartyTypes(this.mCurrentQuest);
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
          this.LoadTeam();
        for (int index = 0; index < this.UnitSlots.Length && index < this.mCurrentParty.PartyData.MAX_UNIT; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null) && index < this.mCurrentParty.Units.Length)
          {
            this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
            this.UnitSlots[index].SetSlotData<UnitData>(this.FindUnit(this.mCurrentParty.Units[index]));
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        {
          this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
          this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport == null ? (UnitData) null : this.mCurrentSupport.Unit);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GuestUnitSlot, (UnityEngine.Object) null))
        {
          if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentQuest != null && this.mCurrentQuest.UseFixEditor)
          {
            UnitData unitData = (UnitData) null;
            if (this.mCurrentQuest != null && (this.mCurrentQuest.type == QuestTypes.Story || this.mCurrentQuest.type == QuestTypes.Free || (this.mCurrentQuest.type == QuestTypes.Extra || this.mCurrentQuest.type == QuestTypes.Tutorial) || (this.mCurrentQuest.type == QuestTypes.Character || this.mCurrentQuest.type == QuestTypes.Event || this.mCurrentQuest.type == QuestTypes.Beginner)) && (this.mCurrentQuest.units.IsNotNull() && this.mCurrentQuest.units.Length > 0))
              unitData = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.mCurrentQuest.units.Get(0));
            this.mGuestUnit = this.FindUnit(unitData);
            this.GuestUnitSlot.SetSlotData<UnitData>(unitData);
            ((Component) this.GuestUnitSlot).get_gameObject().SetActive(true);
          }
          else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character)
          {
            UnitData unitData = (UnitData) null;
            if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Character && (this.mCurrentQuest.units.IsNotNull() && this.mCurrentQuest.units.Length > 0))
              unitData = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.mCurrentQuest.units.Get(0));
            this.mGuestUnit = this.FindUnit(unitData);
            this.GuestUnitSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
            this.GuestUnitSlot.SetSlotData<UnitData>(unitData);
            ((Component) this.GuestUnitSlot).get_gameObject().SetActive(true);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestUnitCond, (UnityEngine.Object) null) && this.mGuestUnit != null)
            {
              DataSource.Bind<UnitData>(this.QuestUnitCond, this.mGuestUnit);
              GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.QUEST_UNIT_ENTRYCONDITION);
            }
          }
          else
            ((Component) this.GuestUnitSlot).get_gameObject().SetActive(false);
        }
        int availableMainMemberSlots = this.AvailableMainMemberSlots;
        for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[mainmemberStart], (UnityEngine.Object) null))
          {
            int num = mainmemberStart - this.mCurrentParty.PartyData.MAINMEMBER_START;
            ((Component) this.UnitSlots[mainmemberStart]).get_gameObject().SetActive(num < availableMainMemberSlots);
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ForwardButton, (UnityEngine.Object) null))
        {
          ((Component) this.ForwardButton).get_gameObject().SetActive(this.ShowForwardButton);
          BackHandler component = (BackHandler) ((Component) this.ForwardButton).GetComponent<BackHandler>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            ((Behaviour) component).set_enabled(!this.ShowBackButton);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BackButton, (UnityEngine.Object) null))
          ((Component) this.BackButton).get_gameObject().SetActive(this.ShowBackButton);
        if (this.GetPartyCondType() == PartyCondType.Forced)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecommendTeamButton, (UnityEngine.Object) null))
          {
            ((Component) this.RecommendTeamButton).get_gameObject().SetActive(true);
            ((Selectable) this.RecommendTeamButton).set_interactable(false);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BreakupButton, (UnityEngine.Object) null))
          {
            ((Component) this.BreakupButton).get_gameObject().SetActive(true);
            ((Selectable) this.BreakupButton).set_interactable(false);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RenameButton, (UnityEngine.Object) null))
            ((Component) this.RenameButton).get_gameObject().SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
            ((Component) this.PrevButton).get_gameObject().SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
            ((Component) this.NextButton).get_gameObject().SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
            ((Component) this.TeamPulldown).get_gameObject().SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextFixParty, (UnityEngine.Object) null))
            ((Component) this.TextFixParty).get_gameObject().SetActive(true);
        }
        else
        {
          bool flag = this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Tower;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecommendTeamButton, (UnityEngine.Object) null))
          {
            ((Component) this.RecommendTeamButton).get_gameObject().SetActive(flag);
            ((Selectable) this.RecommendTeamButton).set_interactable(true);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BreakupButton, (UnityEngine.Object) null))
          {
            ((Component) this.BreakupButton).get_gameObject().SetActive(flag);
            ((Selectable) this.BreakupButton).set_interactable(true);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RenameButton, (UnityEngine.Object) null))
            ((Component) this.RenameButton).get_gameObject().SetActive(flag);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
            ((Component) this.PrevButton).get_gameObject().SetActive(flag);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
            ((Component) this.NextButton).get_gameObject().SetActive(flag);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RecentTeamButton, (UnityEngine.Object) null))
          {
            if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tutorial)
              ((Component) this.RecentTeamButton).get_gameObject().SetActive(false);
            else
              ((Component) this.RecentTeamButton).get_gameObject().SetActive(flag);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TextFixParty, (UnityEngine.Object) null))
            ((Component) this.TextFixParty).get_gameObject().SetActive(false);
        }
        this.ToggleRaidInfo();
        this.RefreshRaidTicketNum();
        this.RefreshRaidButtons();
        this.LockSlots();
        this.OnPartyMemberChange();
        this.LoadInventory();
      }
    }

    private void RefreshSankaStates()
    {
      if (this.mCurrentQuest == null)
      {
        for (int index = 0; index < this.mSankaFukaIcons.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSankaFukaIcons[index], (UnityEngine.Object) null))
            this.mSankaFukaIcons[index].SetActive(false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
            this.UnitSlots[index].SetMainColor(Color.get_white());
        }
      }
      else
      {
        for (int index = 0; index < this.mSankaFukaIcons.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSankaFukaIcons[index], (UnityEngine.Object) null))
          {
            bool flag = true;
            if (this.mCurrentParty.Units.Length <= index)
              break;
            if (this.mCurrentParty.Units[index] != null)
              flag = this.mCurrentQuest.IsUnitAllowed(this.mCurrentParty.Units[index]);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidInfo, (UnityEngine.Object) null))
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidTicketNum, (UnityEngine.Object) null))
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
                if (index < this.UnitSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index].SelectButton, (UnityEngine.Object) null))
                  ((Selectable) this.UnitSlots[index].SelectButton).set_interactable(index < unitSlotNum);
              }
            }
          }
          else
          {
            for (int submemberStart = this.mCurrentParty.PartyData.SUBMEMBER_START; submemberStart < this.mCurrentParty.PartyData.MAX_UNIT; ++submemberStart)
            {
              this.mLockedPartySlots |= 1 << submemberStart;
              if (submemberStart < this.UnitSlots.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[submemberStart], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[submemberStart].SelectButton, (UnityEngine.Object) null))
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
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MultiTower)
      {
        this.mSupportLocked = true;
        this.mItemsLocked = true;
        for (int index = 0; index < this.UnitSlots.Length; ++index)
          ((Selectable) this.UnitSlots[index].SelectButton).set_interactable(true);
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoItemText, (UnityEngine.Object) null))
        this.NoItemText.SetActive(this.mItemsLocked);
      for (int index = 0; index < this.UnitSlots.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null))
          this.UnitSlots[index].SetLocked((this.mLockedPartySlots & 1 << index) != 0);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        this.FriendSlot.SetLocked(this.mSupportLocked);
      for (int index = 0; index < this.ItemSlots.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemSlots[index], (UnityEngine.Object) null))
          this.ItemSlots[index].SetLocked(this.mItemsLocked);
      }
    }

    protected virtual void LoadInventory()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int slotIndex = 0; slotIndex < this.mCurrentItems.Length && slotIndex < player.Inventory.Length; ++slotIndex)
        this.SetItemSlot(slotIndex, player.Inventory[slotIndex]);
      this.OnItemSlotsChange();
    }

    public void LoadRecommendedTeamSetting()
    {
      GlobalVars.RecommendTeamSetting recommendTeamSetting = (GlobalVars.RecommendTeamSetting) null;
      if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY))
      {
        string str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY, string.Empty);
        try
        {
          recommendTeamSetting = (GlobalVars.RecommendTeamSetting) JsonUtility.FromJson<GlobalVars.RecommendTeamSetting>(str);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
      GlobalVars.RecommendTeamSettingValue = recommendTeamSetting;
    }

    private void SaveRecommendedTeamSetting(GlobalVars.RecommendTeamSetting setting)
    {
      string json = JsonUtility.ToJson((object) setting);
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY, json, true);
    }

    private void SaveTeamPresets()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Arena || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.ArenaDef || this.GetPartyCondType() == PartyCondType.Forced)
        return;
      PartyUtility.SaveTeamPresets(this.mCurrentPartyType, this.mCurrentTeamIndex, this.mTeams);
    }

    private void LoadTeam()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Auto)
        throw new InvalidPartyTypeException();
      int maxTeamCount = this.mCurrentPartyType.GetMaxTeamCount();
      PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
      this.mTeams.Clear();
      if (this.GetPartyCondType() == PartyCondType.Forced)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PartyWindow2.\u003CLoadTeam\u003Ec__AnonStorey359 teamCAnonStorey359 = new PartyWindow2.\u003CLoadTeam\u003Ec__AnonStorey359();
        // ISSUE: reference to a compiler-generated field
        teamCAnonStorey359.cond = this.mCurrentQuest.EntryCondition == null ? this.mCurrentQuest.EntryConditionCh : this.mCurrentQuest.EntryCondition;
        string[] list = this.mCurrentQuest.units.GetList();
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
        UnitData[] src = new UnitData[partyOfType.MAX_UNIT];
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PartyWindow2.\u003CLoadTeam\u003Ec__AnonStorey35A teamCAnonStorey35A = new PartyWindow2.\u003CLoadTeam\u003Ec__AnonStorey35A();
        // ISSUE: reference to a compiler-generated field
        teamCAnonStorey35A.\u003C\u003Ef__ref\u0024857 = teamCAnonStorey359;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (teamCAnonStorey35A.i = 0; teamCAnonStorey35A.i < src.Length && teamCAnonStorey35A.i < teamCAnonStorey359.cond.unit.Length; ++teamCAnonStorey35A.i)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          src[teamCAnonStorey35A.i] = list == null || teamCAnonStorey359.cond == null || Array.FindIndex<string>(list, new Predicate<string>(teamCAnonStorey35A.\u003C\u003Em__3D1)) == -1 ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(teamCAnonStorey359.cond.unit[teamCAnonStorey35A.i]) : (UnitData) null;
        }
        this.mTeams.Add(new PartyEditData(src, string.Empty, partyOfType));
        this.mCurrentParty = this.mTeams[0];
        this.mCurrentTeamIndex = 0;
      }
      else
      {
        if (this.mCurrentPartyType != PartyWindow2.EditPartyTypes.Arena && this.mCurrentPartyType != PartyWindow2.EditPartyTypes.ArenaDef)
        {
          int lastSelectionIndex;
          List<PartyEditData> partyEditDataList = PartyUtility.LoadTeamPresets(this.mCurrentPartyType, out lastSelectionIndex);
          if (partyEditDataList != null)
          {
            this.mTeams = partyEditDataList;
            this.mCurrentTeamIndex = lastSelectionIndex;
            if (this.mCurrentTeamIndex < 0 || maxTeamCount <= this.mCurrentTeamIndex)
              this.mCurrentTeamIndex = 0;
          }
        }
        this.ValidateTeam(maxTeamCount, playerPartyType);
      }
      this.ResetTeamPulldown(this.mTeams, maxTeamCount, this.mCurrentQuest);
      this.ChangeEnabledTeamButtons(this.mCurrentTeamIndex, this.mTeams.Count);
    }

    private void ValidateTeam(int maxTeamCount, PlayerPartyTypes partyType)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (this.mTeams.Count > maxTeamCount)
        this.mTeams = this.mTeams.Take<PartyEditData>(maxTeamCount).ToList<PartyEditData>();
      else if (this.mTeams.Count < maxTeamCount)
      {
        PartyData partyOfType = player.FindPartyOfType(partyType);
        for (int count = this.mTeams.Count; count < maxTeamCount; ++count)
          this.mTeams.Add(new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(count), partyOfType));
      }
      if (maxTeamCount <= 1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PartyWindow2.\u003CValidateTeam\u003Ec__AnonStorey35B teamCAnonStorey35B = new PartyWindow2.\u003CValidateTeam\u003Ec__AnonStorey35B();
        // ISSUE: reference to a compiler-generated field
        teamCAnonStorey35B.edit = this.mTeams[0];
        if (partyType == PlayerPartyTypes.Character && this.mCurrentQuest != null && this.mCurrentQuest.units.Length > 0)
        {
          QuestCondParam entryCondition = this.mCurrentQuest.EntryCondition;
          string[] list = this.mCurrentQuest.units.GetList();
          bool flag = entryCondition != null && entryCondition.unit != null && entryCondition.unit.Length == 1;
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          PartyWindow2.\u003CValidateTeam\u003Ec__AnonStorey35C teamCAnonStorey35C = new PartyWindow2.\u003CValidateTeam\u003Ec__AnonStorey35C();
          // ISSUE: reference to a compiler-generated field
          teamCAnonStorey35C.\u003C\u003Ef__ref\u0024859 = teamCAnonStorey35B;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          for (teamCAnonStorey35C.i = 0; teamCAnonStorey35C.i < teamCAnonStorey35B.edit.Units.Length; ++teamCAnonStorey35C.i)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if (teamCAnonStorey35B.edit.Units[teamCAnonStorey35C.i] != null)
            {
              // ISSUE: reference to a compiler-generated method
              if (Array.FindIndex<string>(list, new Predicate<string>(teamCAnonStorey35C.\u003C\u003Em__3D2)) != -1)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                teamCAnonStorey35B.edit.Units[teamCAnonStorey35C.i] = (UnitData) null;
              }
              else if (this.EnableHeroSolo && flag)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                teamCAnonStorey35B.edit.Units[teamCAnonStorey35C.i] = (UnitData) null;
              }
            }
          }
          if (this.EnableHeroSolo && !flag)
          {
            // ISSUE: reference to a compiler-generated field
            PartyUtility.AutoSetLeaderUnit(this.mCurrentQuest, teamCAnonStorey35B.edit, list, MonoSingleton<GameManager>.Instance.Player.Units);
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          teamCAnonStorey35B.edit.SetUnits(teamCAnonStorey35B.edit.Units);
        }
        this.mCurrentParty = this.mTeams[0];
        this.mCurrentTeamIndex = 0;
      }
      else
      {
        PartyUtility.ResetToDefaultTeamIfNeeded(partyType, this.mCurrentQuest, this.mTeams);
        string[] list = this.mCurrentQuest.units.GetList();
        bool flag1 = false;
        if (this.EnableHeroSolo && this.mCurrentQuest != null)
        {
          if (PartyUtility.IsSoloStoryOrEventParty(this.mCurrentQuest))
            flag1 = PartyUtility.KyouseiUnitPartyEdit(this.mCurrentQuest, this.mTeams[this.mCurrentTeamIndex]);
          else if (this.mCurrentQuest.type == QuestTypes.Event || this.mCurrentQuest.type == QuestTypes.Gps || this.mCurrentQuest.type == QuestTypes.Beginner)
          {
            bool flag2 = PartyUtility.SetUnitIfEmptyParty(this.mCurrentQuest, this.mTeams, list);
            flag1 = flag1 || flag2;
          }
          else if (PartyUtility.IsHeloQuest(this.mCurrentQuest))
          {
            for (int index = 0; index < this.mTeams.Count; ++index)
            {
              bool flag2 = PartyUtility.PartyUnitsRemoveHelo(this.mTeams[index], list);
              flag1 = flag1 || flag2;
            }
            if (!this.mIsHeloOnly)
            {
              bool flag2 = PartyUtility.SetUnitIfEmptyParty(this.mCurrentQuest, this.mTeams, list);
              flag1 = flag1 || flag2;
            }
          }
        }
        this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
        if (!flag1)
          return;
        this.SaveTeamPresets();
      }
    }

    private PartyCondType GetPartyCondType()
    {
      if (this.mCurrentQuest != null)
      {
        if (this.mCurrentQuest.EntryCondition != null)
          return this.mCurrentQuest.EntryCondition.party_type;
        if (this.mCurrentQuest.EntryConditionCh != null)
          return this.mCurrentQuest.EntryConditionCh.party_type;
      }
      return PartyCondType.None;
    }

    private void ResetTeamPulldown(List<PartyEditData> teams, int maxTeams, QuestParam currentQuest)
    {
      if (this.GetPartyCondType() == PartyCondType.Forced || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
        return;
      if (maxTeams <= 1)
      {
        ((Component) this.TeamPulldown).get_gameObject().SetActive(false);
      }
      else
      {
        this.TeamPulldown.ResetAllItems();
        for (int index = 0; index < teams.Count && index < this.TeamPulldown.ItemCount; ++index)
          this.TeamPulldown.SetItem(teams[index].Name, index, index);
        this.TeamPulldown.Selection = this.mCurrentTeamIndex;
        ((Component) this.TeamPulldown).get_gameObject().SetActive(true);
      }
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidInfo, (UnityEngine.Object) null))
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
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidN, (UnityEngine.Object) null))
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Raid, (UnityEngine.Object) null))
          ((Selectable) this.Raid).set_interactable(false);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RaidN, (UnityEngine.Object) null))
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

    protected virtual int AvailableMainMemberSlots
    {
      get
      {
        return this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Arena || (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.ArenaDef || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character) || (this.mCurrentQuest == null ? 0 : (this.mCurrentQuest.UseFixEditor ? 1 : 0)) != 0 ? 3 : 4;
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 40:
          if (string.IsNullOrEmpty(GlobalVars.TeamName))
            break;
          this.mCurrentParty.Name = GlobalVars.TeamName;
          this.SaveTeamPresets();
          this.ResetTeamPulldown(this.mTeams, this.mCurrentPartyType.GetMaxTeamCount(), this.mCurrentQuest);
          break;
        case 41:
          if (GlobalVars.RecommendTeamSettingValue == null)
            break;
          this.SaveRecommendedTeamSetting(GlobalVars.RecommendTeamSettingValue);
          this.OrganizeRecommendedParty(GlobalVars.RecommendTeamSettingValue.recommendedType, GlobalVars.RecommendTeamSettingValue.recommendedElement);
          break;
        default:
          this.UnitList_Activated(pinID);
          break;
      }
    }

    private void OnStaminaChange()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this, (UnityEngine.Object) null))
        return;
      this.RefreshRaidButtons();
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
      if ((this.mSelectedSlotIndex > 0 || this.mIsHeloOnly) && (!selectedSlotIsEmpty || this.AlwaysShowRemoveUnit))
        this.UnitList.AddItem(0);
      bool heroesAvailable = PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest);
      if (this.UseQuestInfo && (this.mCurrentQuest.type == QuestTypes.Event || this.mCurrentQuest.type == QuestTypes.Beginner || this.PartyType == PartyWindow2.EditPartyTypes.Character) && (this.mCurrentQuest.units.IsNotNull() && this.mCurrentQuest.units.Length > 0))
      {
        for (int index = 0; index < this.mCurrentQuest.units.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: reference to a compiler-generated method
          UnitData unitData = Array.Find<UnitData>(unitDataList.ToArray(), new Predicate<UnitData>(new PartyWindow2.\u003CRefreshUnitList\u003Ec__AnonStorey35D() { chQuestHeroId = this.mCurrentQuest.units.Get(index) }.\u003C\u003Em__3D3));
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
          if (this.UseQuestInfo)
          {
            string empty = string.Empty;
            if (!this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units[index], ref empty))
              continue;
          }
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(this.FindUnit(this.mCurrentParty.Units[index])) + 1);
          unitDataList.Remove(this.mCurrentParty.Units[index]);
        }
      }
      int count = unitDataList.Count;
      UnitListV2.FilterUnits(unitDataList, (List<int>) null, this.mUnitFilter);
      if (this.mReverse)
        unitDataList.Reverse();
      this.RegistPartyMember(unitDataList, heroesAvailable, selectedSlotIsEmpty, numMainMembers);
      this.UnitList.Refresh(true);
      this.UnitList.ForceUpdateItems();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitListHilit, (UnityEngine.Object) null))
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
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
              this.AttachAndEnable((Transform) this.UnitListHilit, (Transform) rectTransform, this.UnitListHilitParent);
          }
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NoMatchingUnit, (UnityEngine.Object) null))
        return;
      this.NoMatchingUnit.SetActive(count > 0 && this.UnitList.NumItems <= 0);
    }

    protected virtual void RegistPartyMember(List<UnitData> allUnits, bool heroesAvailable, bool selectedSlotIsEmpty, int numMainMembers)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag = UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentState == MyPhoton.MyState.ROOM;
      for (int index = 0; index < allUnits.Count; ++index)
      {
        if ((heroesAvailable || (int) allUnits[index].UnitParam.hero == 0) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (allUnits[index] != this.mCurrentParty.Units[0] || !selectedSlotIsEmpty) || numMainMembers > 1))
        {
          if (flag)
          {
            MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              if (allUnits[index].CalcLevel() < myPhotonRoomParam.unitlv)
                continue;
            }
          }
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(allUnits[index]) + 1);
        }
      }
    }

    private void OnItemFilterChange(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      PartyWindow2.ItemFilterTypes itemFilterTypes = PartyWindow2.ItemFilterTypes.All;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.ItemFilter_Offense))
        itemFilterTypes = PartyWindow2.ItemFilterTypes.Offense;
      else if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) this.ItemFilter_Support))
        itemFilterTypes = PartyWindow2.ItemFilterTypes.Support;
      if (this.mItemFilter == itemFilterTypes)
        return;
      this.mItemFilter = itemFilterTypes;
      for (int index = 0; index < this.mItemFilterToggles.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mItemFilterToggles[index], (UnityEngine.Object) null))
          this.mItemFilterToggles[index].IsOn = (PartyWindow2.ItemFilterTypes) index == itemFilterTypes;
      }
      this.RefreshItemList();
    }

    private static void ToggleBlockRaycasts(Component component, bool block)
    {
      CanvasGroup component1 = (CanvasGroup) component.GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
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
      this.GoToUnitList();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 4);
    }

    private void ShowRaidSettings()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.RaidSettingsTemplate, (UnityEngine.Object) null) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRaidSettings, (UnityEngine.Object) null))
        return;
      this.mRaidSettings = (RaidSettingsWindow) ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.RaidSettingsTemplate)).GetComponent<RaidSettingsWindow>();
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
      if (this.mCurrentQuest.IsQuestDrops && UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
      {
        bool flag = QuestDropParam.Instance.IsChangedQuestDrops(this.mCurrentQuest);
        GlobalVars.SetDropTableGeneratedTime();
        if (flag)
        {
          this.HardQuestDropPiecesUpdate();
          if (!QuestDropParam.Instance.IsWarningPopupDisable)
          {
            UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), (UIUtility.DialogResultEvent) (go => this.OpenQuestDetail()), (GameObject) null, false, -1);
            return false;
          }
        }
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AscendingIcon, (UnityEngine.Object) null))
        this.AscendingIcon.SetActive(ascending);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescendingIcon, (UnityEngine.Object) null))
        this.DescendingIcon.SetActive(!ascending);
      if (unitSortModes == GameUtility.UnitSortModes.Time)
        ascending = !ascending;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SortModeCaption, (UnityEngine.Object) null))
        this.SortModeCaption.set_text(LocalizedText.Get("sys.SORT_" + unitSortModes.ToString().ToUpper()));
      this.mReverse = ascending;
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

    private T GetComponents<T>(GameObject root, string targetName, bool includeInactive) where T : Component
    {
      foreach (T componentsInChild in root.GetComponentsInChildren<T>(includeInactive))
      {
        if (componentsInChild.get_name() == targetName)
          return componentsInChild;
      }
      return (T) null;
    }

    public void ResetTeamName()
    {
      GlobalVars.TeamName = string.Empty;
    }

    public void BreakupTeam()
    {
      this.BreakupTeamImpl();
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
    }

    private void BreakupTeamImpl()
    {
      int num = !this.mIsHeloOnly ? 1 : 0;
      for (int slotIndex = this.mCurrentParty.PartyData.MAX_UNIT - 1; slotIndex >= num; --slotIndex)
        this.SetPartyUnit(slotIndex, (UnitData) null);
      this.SetSupport((SupportData) null);
    }

    public void PrevTeam()
    {
      this.OnPrevTeamChange();
    }

    public void NextTeam()
    {
      this.OnNextTeamChange();
    }

    public void GoToUnitList()
    {
      if (this.PartyType != PartyWindow2.EditPartyTypes.MultiTower)
        return;
      int towerMultiPartyIndex = GlobalVars.SelectedTowerMultiPartyIndex;
      if (!this.IsMultiTowerPartySlot(towerMultiPartyIndex))
        return;
      this.mSelectedSlotIndex = towerMultiPartyIndex;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitRemoveItem, (UnityEngine.Object) null))
        ((Selectable) this.UnitRemoveItem).set_interactable(towerMultiPartyIndex > 0 || this.mIsHeloOnly);
      this.UnitList_Show();
    }

    private List<List<UnitData>> SeparateUnitByElement(List<UnitData> allUnits, IEnumerable<string> kyouseiUnits, EElement targetElement, bool isHeroAvailable)
    {
      List<UnitData> unitDataList1 = new List<UnitData>();
      List<UnitData> unitDataList2 = new List<UnitData>();
      List<UnitData> unitDataList3 = new List<UnitData>();
      List<UnitData> unitDataList4 = new List<UnitData>();
      List<UnitData> unitDataList5 = new List<UnitData>();
      List<UnitData> unitDataList6 = new List<UnitData>();
      List<UnitData> unitDataList7 = new List<UnitData>();
      List<string> stringList = kyouseiUnits != null ? kyouseiUnits.ToList<string>() : new List<string>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003CSeparateUnitByElement\u003Ec__AnonStorey35E elementCAnonStorey35E = new PartyWindow2.\u003CSeparateUnitByElement\u003Ec__AnonStorey35E();
      using (List<UnitData>.Enumerator enumerator = allUnits.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          elementCAnonStorey35E.unit = enumerator.Current;
          // ISSUE: reference to a compiler-generated method
          int index = stringList.FindIndex(new Predicate<string>(elementCAnonStorey35E.\u003C\u003Em__3D6));
          if (index != -1)
          {
            stringList.RemoveAt(index);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            if ((isHeroAvailable || (int) elementCAnonStorey35E.unit.UnitParam.hero == 0) && this.mCurrentQuest.IsEntryQuestCondition(elementCAnonStorey35E.unit))
            {
              if (targetElement == EElement.None)
              {
                // ISSUE: reference to a compiler-generated field
                unitDataList1.Add(elementCAnonStorey35E.unit);
              }
              else
              {
                // ISSUE: reference to a compiler-generated field
                switch (elementCAnonStorey35E.unit.Element)
                {
                  case EElement.Fire:
                    // ISSUE: reference to a compiler-generated field
                    unitDataList2.Add(elementCAnonStorey35E.unit);
                    continue;
                  case EElement.Water:
                    // ISSUE: reference to a compiler-generated field
                    unitDataList3.Add(elementCAnonStorey35E.unit);
                    continue;
                  case EElement.Wind:
                    // ISSUE: reference to a compiler-generated field
                    unitDataList4.Add(elementCAnonStorey35E.unit);
                    continue;
                  case EElement.Thunder:
                    // ISSUE: reference to a compiler-generated field
                    unitDataList5.Add(elementCAnonStorey35E.unit);
                    continue;
                  case EElement.Shine:
                    // ISSUE: reference to a compiler-generated field
                    unitDataList6.Add(elementCAnonStorey35E.unit);
                    continue;
                  case EElement.Dark:
                    // ISSUE: reference to a compiler-generated field
                    unitDataList7.Add(elementCAnonStorey35E.unit);
                    continue;
                  default:
                    continue;
                }
              }
            }
          }
        }
      }
      List<List<UnitData>> unitDataListList;
      if (targetElement == EElement.None)
      {
        unitDataListList = new List<List<UnitData>>()
        {
          unitDataList1
        };
      }
      else
      {
        unitDataListList = new List<List<UnitData>>()
        {
          unitDataList2,
          unitDataList3,
          unitDataList4,
          unitDataList5,
          unitDataList6,
          unitDataList7
        };
        int index = (int) (targetElement - 1);
        if (index >= 0 && index < unitDataListList.Count)
        {
          List<UnitData> unitDataList8 = unitDataListList[index];
          unitDataListList.RemoveAt(index);
          unitDataListList.Insert(0, unitDataList8);
        }
      }
      return unitDataListList;
    }

    private void OrganizeRecommendedParty(GlobalVars.RecommendType targetType, EElement targetElement)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PartyWindow2.\u003COrganizeRecommendedParty\u003Ec__AnonStorey35F partyCAnonStorey35F = new PartyWindow2.\u003COrganizeRecommendedParty\u003Ec__AnonStorey35F();
      this.BreakupTeamImpl();
      bool flag = PartyUtility.IsHeloQuest(this.mCurrentQuest);
      int maxUnit = this.mCurrentParty.PartyData.MAX_UNIT;
      if (flag)
        --maxUnit;
      List<List<UnitData>> unitDataListList = this.SeparateUnitByElement(MonoSingleton<GameManager>.Instance.Player.Units, (IEnumerable<string>) this.mCurrentQuest.units.GetList(), targetElement, PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest));
      List<Comparison<UnitData>> comparisonList = new List<Comparison<UnitData>>() { new Comparison<UnitData>(PartyWindow2.CompareTo_Total), new Comparison<UnitData>(PartyWindow2.CompareTo_HP), new Comparison<UnitData>(PartyWindow2.CompareTo_Attack), new Comparison<UnitData>(PartyWindow2.CompareTo_Defense), new Comparison<UnitData>(PartyWindow2.CompareTo_Magic), new Comparison<UnitData>(PartyWindow2.CompareTo_Mind), new Comparison<UnitData>(PartyWindow2.CompareTo_Speed), new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeSlash), new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeStab), new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeBlow), new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeShot), new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeMagic), new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeNone) };
      // ISSUE: reference to a compiler-generated field
      partyCAnonStorey35F.targetComparators = comparisonList;
      int comparatorOrder = PartyUtility.RecommendTypeToComparatorOrder(targetType);
      // ISSUE: reference to a compiler-generated field
      if (comparatorOrder >= 0 && comparatorOrder < partyCAnonStorey35F.targetComparators.Count)
      {
        // ISSUE: reference to a compiler-generated field
        Comparison<UnitData> targetComparator = partyCAnonStorey35F.targetComparators[comparatorOrder];
        // ISSUE: reference to a compiler-generated field
        partyCAnonStorey35F.targetComparators.RemoveAt(comparatorOrder);
        // ISSUE: reference to a compiler-generated field
        partyCAnonStorey35F.targetComparators.Insert(0, targetComparator);
      }
      using (List<List<UnitData>>.Enumerator enumerator = unitDataListList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated method
          enumerator.Current.Sort(new Comparison<UnitData>(partyCAnonStorey35F.\u003C\u003Em__3D7));
        }
      }
      List<UnitData> unitDataList = new List<UnitData>();
      using (List<List<UnitData>>.Enumerator enumerator = unitDataListList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          List<UnitData> current = enumerator.Current;
          if (maxUnit > 0)
          {
            List<int> source = new List<int>();
            for (int index = 0; index < current.Count; ++index)
            {
              unitDataList.Add(current[index]);
              source.Add(index);
              if (--maxUnit <= 0)
                break;
            }
            foreach (int index in source.Reverse<int>())
              current.RemoveAt(index);
          }
          else
            break;
        }
      }
      PartyData partyData = this.mCurrentParty.PartyData;
      if (this.mIsHeloOnly || flag)
      {
        int val1 = partyData.MAX_MAINMEMBER - 1;
        for (int index = 0; index < partyData.MAX_MAINMEMBER - 1 && index < unitDataList.Count; ++index)
          this.SetPartyUnit(index + partyData.MAINMEMBER_START, unitDataList[index]);
        unitDataList.RemoveRange(0, Math.Min(val1, unitDataList.Count));
      }
      else
      {
        int maxMainmember = partyData.MAX_MAINMEMBER;
        for (int index = 0; index < maxMainmember && index < unitDataList.Count; ++index)
          this.SetPartyUnit(index + partyData.MAINMEMBER_START, unitDataList[index]);
        unitDataList.RemoveRange(0, Math.Min(maxMainmember, unitDataList.Count));
      }
      for (int index = 0; index < partyData.MAX_SUBMEMBER && index < unitDataList.Count; ++index)
        this.SetPartyUnit(index + partyData.SUBMEMBER_START, unitDataList[index]);
      unitDataList.RemoveRange(0, Math.Min(partyData.MAX_SUBMEMBER, unitDataList.Count));
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
    }

    private static int CompareTo_Total(UnitData unit1, UnitData unit2)
    {
      int num = 0 + (int) unit1.Status.param.atk + (int) unit1.Status.param.def + (int) unit1.Status.param.mag + (int) unit1.Status.param.mnd + (int) unit1.Status.param.spd + (int) unit1.Status.param.dex + (int) unit1.Status.param.cri + (int) unit1.Status.param.luk;
      return 0 + (int) unit2.Status.param.atk + (int) unit2.Status.param.def + (int) unit2.Status.param.mag + (int) unit2.Status.param.mnd + (int) unit2.Status.param.spd + (int) unit2.Status.param.dex + (int) unit2.Status.param.cri + (int) unit2.Status.param.luk - num;
    }

    private static int CompareTo_Attack(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.atk - (int) unit1.Status.param.atk;
    }

    private static int CompareTo_Magic(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.mag - (int) unit1.Status.param.mag;
    }

    private static int CompareTo_Defense(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.def - (int) unit1.Status.param.def;
    }

    private static int CompareTo_Mind(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.mnd - (int) unit1.Status.param.mnd;
    }

    private static int CompareTo_Speed(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.spd - (int) unit1.Status.param.spd;
    }

    private static int CompareTo_HP(UnitData unit1, UnitData unit2)
    {
      return (int) unit2.Status.param.hp - (int) unit1.Status.param.hp;
    }

    private static int CompareTo_AttackType(UnitData unit1, UnitData unit2, AttackDetailTypes type)
    {
      AttackDetailTypes attackDetailType1 = unit1.GetAttackSkill().AttackDetailType;
      AttackDetailTypes attackDetailType2 = unit2.GetAttackSkill().AttackDetailType;
      if (attackDetailType1 == type && attackDetailType2 == type)
        return 0;
      if (attackDetailType1 == type && attackDetailType2 != type)
        return -1;
      return attackDetailType1 != type && attackDetailType2 == type ? 1 : 0;
    }

    private static int CompareTo_AttackTypeSlash(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Slash);
    }

    private static int CompareTo_AttackTypeStab(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Stab);
    }

    private static int CompareTo_AttackTypeBlow(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Blow);
    }

    private static int CompareTo_AttackTypeShot(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Shot);
    }

    private static int CompareTo_AttackTypeMagic(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.Magic);
    }

    private static int CompareTo_AttackTypeNone(UnitData unit1, UnitData unit2)
    {
      return PartyWindow2.CompareTo_AttackType(unit1, unit2, AttackDetailTypes.None);
    }

    public void OnAutoBattleSetting(string name, ActionCall.EventType eventType, SerializeValueList list)
    {
      bool sw = false;
      if (this.mCurrentQuest != null)
        sw = this.mCurrentQuest.FirstAutoPlayProhibit && this.mCurrentQuest.state != QuestStates.Cleared;
      if (name == "PROHIBIT")
      {
        switch (eventType)
        {
          case ActionCall.EventType.START:
          case ActionCall.EventType.OPEN:
            list.SetActive("item", sw);
            break;
        }
      }
      else
      {
        if (!(name == "SETTING"))
          return;
        Toggle uiToggle1 = list.GetUIToggle("btn_auto");
        Toggle uiToggle2 = list.GetUIToggle("btn_treasure");
        Toggle uiToggle3 = list.GetUIToggle("btn_skill");
        switch (eventType)
        {
          case ActionCall.EventType.START:
          case ActionCall.EventType.OPEN:
            list.SetActive("item", sw);
            if (sw)
            {
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle1, (UnityEngine.Object) null))
              {
                Toggle toggle = uiToggle1;
                bool flag = false;
                ((Selectable) uiToggle1).set_interactable(flag);
                int num = flag ? 1 : 0;
                toggle.set_isOn(num != 0);
                list.SetActive("off", true);
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle2, (UnityEngine.Object) null))
                uiToggle2.set_isOn(false);
              if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle3, (UnityEngine.Object) null))
                break;
              uiToggle3.set_isOn(false);
              break;
            }
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle1, (UnityEngine.Object) null))
            {
              uiToggle1.set_isOn(GameUtility.Config_UseAutoPlay.Value);
              ((Selectable) uiToggle1).set_interactable(true);
            }
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle2, (UnityEngine.Object) null))
              uiToggle2.set_isOn(GameUtility.Config_AutoMode_Treasure.Value);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle3, (UnityEngine.Object) null))
              uiToggle3.set_isOn(GameUtility.Config_AutoMode_DisableSkill.Value);
            list.SetActive("off", !GameUtility.Config_UseAutoPlay.Value);
            break;
          case ActionCall.EventType.UPDATE:
            if (sw)
              break;
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle1, (UnityEngine.Object) null))
              GameUtility.Config_UseAutoPlay.Value = uiToggle1.get_isOn();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle2, (UnityEngine.Object) null))
              GameUtility.Config_AutoMode_Treasure.Value = uiToggle2.get_isOn();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle3, (UnityEngine.Object) null))
              GameUtility.Config_AutoMode_DisableSkill.Value = uiToggle3.get_isOn();
            list.SetActive("off", !GameUtility.Config_UseAutoPlay.Value);
            break;
        }
      }
    }

    private void UnitList_Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.UnitList_OnSelect();
          break;
        case 101:
          this.UnitList_OnSelectSupport();
          break;
        case 110:
          this.UnitList_OnRemove();
          break;
        case 111:
          this.UnitList_OnRemoveSupport();
          break;
        case 119:
          this.LockWindow(false);
          ButtonEvent.ResetLock("PartyWindow");
          break;
        case 120:
          SRPG_Button button = FlowNode_ButtonEvent.currentValue as SRPG_Button;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) button, (UnityEngine.Object) null))
            button = this.BackButton;
          this.UnitList_OnClosing(button);
          break;
      }
    }

    private void UnitList_Create()
    {
      if (string.IsNullOrEmpty(this.UNITLIST_WINDOW_PATH))
        return;
      GameObject gameObject1 = AssetManager.Load<GameObject>(this.UNITLIST_WINDOW_PATH);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      CanvasStack component1 = (CanvasStack) gameObject2.GetComponent<CanvasStack>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
      {
        CanvasStack component2 = (CanvasStack) ((Component) this).GetComponent<CanvasStack>();
        component1.Priority = component2.Priority + 10;
      }
      this.mUnitListWindow = (UnitListWindow) gameObject2.GetComponent<UnitListWindow>();
      this.mUnitListWindow.Enabled(false);
    }

    private void UnitList_Remove()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) this.mUnitListWindow).get_gameObject(), (UnityEngine.Object) null))
        GameUtility.DestroyGameObject(((Component) this.mUnitListWindow).get_gameObject());
      this.mUnitListWindow = (UnitListWindow) null;
    }

    private void UnitList_Show()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null))
        return;
      this.mUnitListWindow.Enabled(true);
      this.LockWindow(true);
      this.mUnitListWindow.AddData("data_units", (object) this.RefreshUnits(this.mOwnUnits.ToArray()));
      this.mUnitListWindow.AddData("data_party", (object) this.mCurrentParty);
      this.mUnitListWindow.AddData("data_quest", (object) this.mCurrentQuest);
      this.mUnitListWindow.AddData("data_slot", (object) this.mSelectedSlotIndex);
      this.mUnitListWindow.AddData("data_heroOnly", (object) this.mIsHeloOnly);
      if (this.mCurrentQuest != null && (this.mCurrentQuest.type == QuestTypes.Tower || this.mCurrentQuest.type == QuestTypes.MultiTower))
        ButtonEvent.Invoke("UNITLIST_BTN_TWPARTY_OPEN", (object) null);
      else
        ButtonEvent.Invoke("UNITLIST_BTN_PARTY_OPEN", (object) null);
      ButtonEvent.Lock("PartyWindow");
    }

    private void UnitList_OnSelect()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      int selectedSlotIndex = this.mSelectedSlotIndex;
      UnitData unit1 = this.mCurrentParty.Units[this.mSelectedSlotIndex];
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
      UnitData unit2 = (UnitData) null;
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
        unit2 = currentValue.GetDataSource<UnitData>("_self");
      if (unit2 != null && unit2 != unit1)
      {
        int slotIndex = this.mCurrentParty.IndexOf(unit2);
        if (slotIndex >= 0 && selectedSlotIndex != slotIndex)
        {
          this.SetPartyUnit(selectedSlotIndex, unit2);
          this.SetPartyUnit(slotIndex, unit1);
        }
        else
          this.SetPartyUnit(selectedSlotIndex, unit2);
      }
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) this.ForwardButton);
    }

    private void UnitList_OnRemove()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      this.SetPartyUnit(this.mSelectedSlotIndex, (UnitData) null);
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) this.ForwardButton);
    }

    private void UnitList_OnClosing(SRPG_Button button)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null))
      {
        if (!this.mUnitListWindow.IsEnabled())
          return;
        this.mUnitListWindow.ClearData();
        UnitListRootWindow.ListData listData = this.mUnitListWindow.rootWindow.GetListData("unitlist");
        if (listData != null)
          listData.selectUniqueID = 0L;
        this.mUnitListWindow.Enabled(false);
      }
      if (this.PartyType != PartyWindow2.EditPartyTypes.MultiTower || !UnityEngine.Object.op_Inequality((UnityEngine.Object) button, (UnityEngine.Object) null))
        return;
      this.OnForwardOrBackButtonClick(button);
    }

    private void SupportList_Show()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null))
        return;
      this.mUnitListWindow.Enabled(true);
      this.LockWindow(true);
      this.mUnitListWindow.AddData("data_support", (object) this.mCurrentSupport);
      this.mUnitListWindow.AddData("data_quest", (object) this.mCurrentQuest);
      ButtonEvent.Invoke("UNITLIST_BTN_SUPPORT_OPEN", (object) null);
      ButtonEvent.Lock("PartyWindow");
    }

    private void UnitList_OnSelectSupport()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      this.mSelectedSupport = currentValue == null ? (SupportData) null : currentValue.GetDataSource<SupportData>("_self");
      if (this.mCurrentSupport == this.mSelectedSupport)
        ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) this.ForwardButton);
      else
        UIUtility.ConfirmBox(LocalizedText.Get(this.mSelectedSupport.GetCost() <= 0 ? "sys.SUPPORT_CONFIRM1" : "sys.SUPPORT_CONFIRM2", (object) this.mSelectedSupport.PlayerName, (object) this.mSelectedSupport.GetCost()), (string) null, new UIUtility.DialogResultEvent(this.UnitList_OnAcceptSupport), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
    }

    private void UnitList_OnAcceptSupport(GameObject go)
    {
      if (MonoSingleton<GameManager>.Instance.Player.Gold < this.mSelectedSupport.GetCost())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.SUPPORT_NOGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.SetSupport(this.mSelectedSupport);
        this.OnPartyMemberChange();
        ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) this.ForwardButton);
        if (this.mSelectedSupport != null)
        {
          if (this.mSelectedSupport.IsFriend())
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 210);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 220);
      }
    }

    private void UnitList_OnRemoveSupport()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitListWindow, (UnityEngine.Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      this.SetSupport((SupportData) null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
      {
        this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
        this.FriendSlot.SetSlotData<UnitData>((UnitData) null);
      }
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) null);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 220);
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
      MultiTower,
    }

    public enum PartySlotType
    {
      Main0,
      Main1,
      Sub0,
    }

    private enum ItemFilterTypes
    {
      All,
      Offense,
      Support,
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

    private delegate void Callback();
  }
}
