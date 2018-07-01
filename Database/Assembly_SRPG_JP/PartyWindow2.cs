// Decompiled with JetBrains decompiler
// Type: SRPG.PartyWindow2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
  [FlowNode.Pin(4, "開く", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "戻る", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(100, "ユニット選択", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(101, "サポートユニット選択", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(110, "ユニット解除", FlowNode.PinTypes.Input, 110)]
  [FlowNode.Pin(111, "サポートユニット解除", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(119, "ユニットリストウィンド開いた", FlowNode.PinTypes.Input, 119)]
  [FlowNode.Pin(120, "ユニットリストウィンド閉じ始めた", FlowNode.PinTypes.Input, 120)]
  [FlowNode.Pin(130, "強制的に更新", FlowNode.PinTypes.Input, 130)]
  [FlowNode.Pin(140, "強制的に更新(チームのリロードなし)", FlowNode.PinTypes.Input, 140)]
  [FlowNode.Pin(200, "フレンドがサポートに設定された", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(210, "フレンド以外がサポートが設定された", FlowNode.PinTypes.Output, 210)]
  [FlowNode.Pin(220, "サポートが解除された", FlowNode.PinTypes.Output, 220)]
  [FlowNode.Pin(7, "AP回復アイテム", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(6, "画面アンロック", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(5, "画面ロック", FlowNode.PinTypes.Output, 5)]
  [FlowNode.Pin(1, "進む", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(50, "クエスト更新", FlowNode.PinTypes.Input, 50)]
  [FlowNode.Pin(41, "おまかせ編成完了", FlowNode.PinTypes.Input, 41)]
  [FlowNode.Pin(40, "チーム名変更完了", FlowNode.PinTypes.Input, 40)]
  [FlowNode.Pin(21, "アイテム選択完了", FlowNode.PinTypes.Output, 21)]
  [FlowNode.Pin(20, "アイテム選択開始", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(11, "ユニット選択完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "ユニット選択開始", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(8, "マルチタワー用進む", FlowNode.PinTypes.Output, 8)]
  public class PartyWindow2 : MonoBehaviour, IFlowInterface, ISortableList
  {
    public int MaxRaidNum;
    public int DefaultRaidNum;
    public PartyWindow2.EditPartyTypes PartyType;
    [Space(10f)]
    public GenericSlot UnitSlotTemplate;
    public GenericSlot NpcSlotTemplate;
    public Transform MainMemberHolder;
    public Transform SubMemberHolder;
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
    public FixedScrollablePulldown TeamPulldown;
    public Toggle[] TeamTabs;
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
    public bool EnableTeamAssign;
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
    public RectTransform UnitListHilit;
    public string UnitListHilitParent;
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
    [SerializeField]
    private QuestCampaignCreate QuestCampaigns;
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
    protected List<UnitData> mGuestUnit;
    private List<PartySlotData> mSlotData;
    private PartySlotData mSupportSlotData;
    private int mCurrentTeamIndex;
    private int mMaxTeamCount;
    protected PartyWindow2.EditPartyTypes mCurrentPartyType;
    private List<SupportData> mSupports;
    protected SupportData mCurrentSupport;
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
    protected int mLockedPartySlots;
    protected bool mSupportLocked;
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
    public string SceneNameHome;
    private GameObject mMultiErrorMsg;
    private Transform mTrHomeHeader;
    private bool mUnitSlotSelected;

    public PartyWindow2()
    {
      base.\u002Ector();
    }

    public PartyWindow2.EditPartyTypes CurrentPartyType
    {
      get
      {
        return this.mCurrentPartyType;
      }
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

    public List<PartyEditData> Teams
    {
      get
      {
        return this.mTeams;
      }
    }

    private void OpenQuestDetail()
    {
      if (this.mCurrentQuest == null || this.mReqQuestDetail == null || (!this.mReqQuestDetail.isDone || !UnityEngine.Object.op_Inequality(this.mReqQuestDetail.asset, (UnityEngine.Object) null)))
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate(this.mReqQuestDetail.asset) as GameObject;
      DataSource.Bind<QuestParam>(gameObject, this.mCurrentQuest);
      if (this.mGuestUnit != null && this.mGuestUnit.Count > 0 && this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character)
        DataSource.Bind<UnitData>(gameObject, this.mGuestUnit[0]);
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
          Scene sceneByName = SceneManager.GetSceneByName(this.SceneNameHome);
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
      return (IEnumerator) new PartyWindow2.\u003CStart\u003Ec__Iterator12A()
      {
        \u003C\u003Ef__this = this
      };
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMultiErrorMsg, (UnityEngine.Object) null))
      {
        UIUtility.PopCanvas();
        this.mMultiErrorMsg = (GameObject) null;
      }
      GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
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

    private void ChangeEnabledArrowButtons(int index, int max)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextButton, (UnityEngine.Object) null))
        ((Selectable) this.NextButton).set_interactable(index < max - 1);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PrevButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.PrevButton).set_interactable(index > 0);
    }

    private void ChangeEnabledTeamTabs(int index)
    {
      if (this.TeamTabs == null)
        return;
      for (int index1 = 0; index1 < this.TeamTabs.Length; ++index1)
        this.TeamTabs[index1].set_isOn(index1 == index);
    }

    private void OnNextTeamChange()
    {
      if (!this.OnTeamChangeImpl(this.mCurrentTeamIndex + 1) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
        return;
      this.TeamPulldown.Selection = this.mCurrentTeamIndex;
    }

    private void OnPrevTeamChange()
    {
      if (!this.OnTeamChangeImpl(this.mCurrentTeamIndex - 1) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
        return;
      this.TeamPulldown.Selection = this.mCurrentTeamIndex;
    }

    private bool OnTeamChangeImpl(int index)
    {
      if (this.mCurrentTeamIndex == index || index >= this.mMaxTeamCount || index < 0)
        return false;
      this.ChangeEnabledArrowButtons(index, this.mMaxTeamCount);
      this.ChangeEnabledTeamTabs(index);
      this.mCurrentTeamIndex = index;
      this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
      this.AssignUnits(this.mCurrentParty);
      for (int slotIndex = 0; slotIndex < this.mCurrentParty.PartyData.MAX_UNIT; ++slotIndex)
        this.SetPartyUnit(slotIndex, this.mCurrentParty.Units[slotIndex]);
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Ordeal)
      {
        this.mCurrentSupport = this.mSupports[this.mCurrentTeamIndex];
        this.SetSupport(this.mCurrentSupport);
        if (this.mCurrentSupport != null)
        {
          if (this.mCurrentSupport.IsFriend())
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
          else
            FlowNode_GameObject.ActivateOutputLinks((Component) this, 210);
        }
        else
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 220);
      }
      this.OnPartyMemberChange();
      this.SaveTeamPresets();
      return true;
    }

    private void OnTeamChange(int index)
    {
      this.OnTeamChangeImpl(index);
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
      ItemData currentItem = this.mCurrentItems[this.mSelectedSlotIndex];
      this.ItemList.ClearItems();
      if (currentItem != null || this.AlwaysShowRemoveItem)
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
        if (currentItem != null)
        {
          int itemID = this.mOwnItems.FindIndex((Predicate<ItemData>) (p => p.Param == currentItem.Param)) + 1;
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
      for (int i = 0; i < this.ChosenItemBadges.Length; ++i)
      {
        if (this.mCurrentItems[i] != null)
        {
          RectTransform rectTransform = this.ItemList.FindItem(this.mOwnItems.FindIndex((Predicate<ItemData>) (p => p.Param == this.mCurrentItems[i].Param)) + 1);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) rectTransform, (UnityEngine.Object) null))
          {
            ((Transform) this.ChosenItemBadges[i]).SetParent((Transform) rectTransform, false);
            ((Component) this.ChosenItemBadges[i]).get_gameObject().SetActive(true);
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
          if (this.mCurrentParty.Units[mainmemberStart] == null && mainmemberStart < this.UnitSlots.Length && (mainmemberStart < this.mSlotData.Count && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[mainmemberStart], (UnityEngine.Object) null)) && ((this.mLockedPartySlots & 1 << mainmemberStart) == 0 && this.mSlotData[mainmemberStart].Type == SRPG.PartySlotType.Free))
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
        if (this.mCurrentParty.Units[submemberStart] == null && submemberStart < this.UnitSlots.Length && (submemberStart < this.mSlotData.Count && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[submemberStart], (UnityEngine.Object) null)) && ((this.mLockedPartySlots & 1 << submemberStart) == 0 && this.mSlotData[submemberStart].Type == SRPG.PartySlotType.Free))
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
          if (this.mGuestUnit != null && this.mGuestUnit.Count > 0 && this.mGuestUnit[0].LeaderSkill != null)
            data = this.mGuestUnit[0].LeaderSkill.SkillParam;
        }
        else if (this.mCurrentParty.Units[0] != null)
        {
          if (this.mCurrentParty.Units[0].LeaderSkill != null)
            data = this.mCurrentParty.Units[0].LeaderSkill.SkillParam;
        }
        else if (this.mSlotData[0].Type == SRPG.PartySlotType.Npc || this.mSlotData[0].Type == SRPG.PartySlotType.NpcHero)
        {
          UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.mSlotData[0].UnitName);
          if (unitParam != null && unitParam.leader_skills != null && unitParam.leader_skills.Length >= 4)
          {
            string leaderSkill = unitParam.leader_skills[4];
            if (!string.IsNullOrEmpty(leaderSkill))
              data = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(leaderSkill);
          }
        }
        else if (this.EnableHeroSolo && this.mGuestUnit != null && (this.mGuestUnit.Count > 0 && this.mGuestUnit[0].LeaderSkill != null))
          data = this.mGuestUnit[0].LeaderSkill.SkillParam;
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
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Ordeal)
      {
        this.mSupports[this.mCurrentTeamIndex] = support;
      }
      else
      {
        for (int index = 0; index < this.mSupports.Count; ++index)
          this.mSupports[index] = support;
      }
      this.mCurrentSupport = support;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        return;
      this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
      this.FriendSlot.SetSlotData<SupportData>(support);
      if (support == null)
        this.FriendSlot.SetSlotData<UnitData>((UnitData) null);
      else
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
        int num = Math.Min(item.Num, item.Param.invcap);
        ItemData data = new ItemData();
        data.Setup(item.UniqueID, item.Param, num);
        this.mCurrentItems[slotIndex] = data;
        this.ItemSlots[slotIndex].SetSlotData<ItemData>(data);
        this.OnSlotChange(((Component) this.ItemSlots[slotIndex]).get_gameObject());
      }
    }

    private void SetPartyUnit(int slotIndex, UnitData unit)
    {
      if (slotIndex < 0 || slotIndex >= this.mSlotData.Count || !this.IsSettableSlot(this.mSlotData[slotIndex]))
        return;
      if (unit == null)
      {
        int num1 = slotIndex >= this.mCurrentParty.PartyData.MAX_MAINMEMBER ? this.mCurrentParty.PartyData.MAX_UNIT : this.mCurrentParty.PartyData.MAX_MAINMEMBER;
        for (int index = slotIndex; index < num1; ++index)
        {
          if (this.mSlotData[index].Type == SRPG.PartySlotType.Free)
          {
            int num2 = 1;
            while (index + num2 < num1 && this.mSlotData[index + num2].Type != SRPG.PartySlotType.Free)
              ++num2;
            if (index + num2 < num1)
            {
              this.mCurrentParty.Units[index] = this.mCurrentParty.Units[index + num2];
              this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
              this.UnitSlots[index].SetSlotData<UnitData>(this.mCurrentParty.Units[index]);
              this.mCurrentParty.Units[index + num2] = (UnitData) null;
              this.UnitSlots[index + num2].SetSlotData<QuestParam>(this.mCurrentQuest);
              this.UnitSlots[index + num2].SetSlotData<UnitData>((UnitData) null);
            }
            else
            {
              this.mCurrentParty.Units[index] = (UnitData) null;
              this.UnitSlots[index].SetSlotData<QuestParam>(this.mCurrentQuest);
              this.UnitSlots[index].SetSlotData<UnitData>((UnitData) null);
            }
          }
        }
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

    private void SetPartyUnitForce(int slotIndex, UnitData unit)
    {
      if (slotIndex < 0 || slotIndex >= this.mSlotData.Count || !this.IsSettableSlot(this.mSlotData[slotIndex]))
        return;
      this.mCurrentParty.Units[slotIndex] = unit;
      this.UnitSlots[slotIndex].SetSlotData<QuestParam>(this.mCurrentQuest);
      this.UnitSlots[slotIndex].SetSlotData<UnitData>(unit);
      this.RefreshSankaStates();
      this.OnSlotChange(((Component) this.UnitSlots[slotIndex]).get_gameObject());
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
      return (IEnumerator) new PartyWindow2.\u003CPopulateItemList\u003Ec__Iterator12B()
      {
        \u003C\u003Ef__this = this
      };
    }

    protected virtual void RecalcTotalAttack()
    {
      int num = PartyUtility.CalcTotalAttack(this.mCurrentParty, this.mLockedPartySlots, this.mOwnUnits, !this.mSupportLocked ? this.mCurrentSupport : (SupportData) null, this.mGuestUnit);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TotalAtk, (UnityEngine.Object) null))
        return;
      this.TotalAtk.set_text(num.ToString());
    }

    private bool IsMultiTowerPartySlot(int index)
    {
      return this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MultiTower && (index == 0 || index == 1 || index == 2);
    }

    private void OnUnitSlotClick(GenericSlot slot, bool interactable)
    {
      if (!this.mInitialized || this.mIsSaving)
        return;
      this.Refresh(true);
      int index = Array.IndexOf<GenericSlot>(this.UnitSlots, slot);
      if (0 <= index && index < this.mCurrentParty.PartyData.MAX_UNIT)
      {
        this.mUnitSlotSelected = true;
        this.mSelectedSlotIndex = index;
        this.UnitList_Show();
      }
      else
      {
        if (!this.IsMultiTowerPartySlot(index))
          return;
        this.mUnitSlotSelected = true;
        this.mSelectedSlotIndex = index;
        this.UnitList_Show();
      }
    }

    private void OnSupportUnitSlotClick(GenericSlot slot, bool interactable)
    {
      if (!this.mInitialized || this.mIsSaving)
        return;
      this.Refresh(true);
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) slot))
        return;
      this.mUnitSlotSelected = true;
      this.SupportList_Show();
    }

    private UnitData[] RefreshUnits(UnitData[] units)
    {
      List<UnitData> source = new List<UnitData>((IEnumerable<UnitData>) this.mOwnUnits);
      List<UnitData> unitDataList1 = new List<UnitData>();
      bool flag1 = this.mCurrentParty.Units[this.mSelectedSlotIndex] == null;
      bool flag2 = PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest);
      if (this.UseQuestInfo && (this.mCurrentQuest.type == QuestTypes.Event || this.mCurrentQuest.type == QuestTypes.Beginner || this.PartyType == PartyWindow2.EditPartyTypes.Character))
      {
        string[] strArray = this.mCurrentQuest.questParty == null ? this.mCurrentQuest.units.GetList() : ((IEnumerable<PartySlotTypeUnitPair>) this.mCurrentQuest.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot =>
        {
          if (slot.Type != SRPG.PartySlotType.ForcedHero)
            return slot.Type == SRPG.PartySlotType.Forced;
          return true;
        })).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
        if (strArray != null)
        {
          for (int index = 0; index < strArray.Length; ++index)
          {
            string chQuestHeroId = strArray[index];
            UnitData unitData = source.FirstOrDefault<UnitData>((Func<UnitData, bool>) (u => u.UnitParam.iname == chQuestHeroId));
            if (unitData != null)
              source.Remove(unitData);
          }
        }
      }
      int num = 0;
      for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart <= this.mCurrentParty.PartyData.MAINMEMBER_END; ++mainmemberStart)
      {
        if (this.mCurrentParty.Units[mainmemberStart] != null)
          ++num;
      }
      for (int i = 0; i < this.mCurrentParty.PartyData.MAX_UNIT; ++i)
      {
        if (this.mCurrentParty.Units[i] != null && (flag2 || !this.mCurrentParty.Units[i].UnitParam.IsHero()) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (i != 0 || !flag1) || num > 1))
        {
          if (this.UseQuestInfo)
          {
            string empty = string.Empty;
            if (!this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units[i], ref empty))
              continue;
          }
          UnitData unitData = source.Find((Predicate<UnitData>) (v => v.UniqueID == this.mCurrentParty.Units[i].UniqueID));
          if (unitData != null)
            unitDataList1.Add(unitData);
          int index = source.FindIndex((Predicate<UnitData>) (v => v.UniqueID == this.mCurrentParty.Units[i].UniqueID));
          if (index >= 0)
            source.RemoveAt(index);
        }
      }
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag3 = UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.CurrentState == MyPhoton.MyState.ROOM;
      List<UnitData> unitDataList2 = new List<UnitData>();
      for (int index = 0; index < source.Count; ++index)
      {
        if ((flag2 || !source[index].UnitParam.IsHero()) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (source[index] != this.mCurrentParty.Units[0] || !flag1) || num > 1))
        {
          if (flag3)
          {
            MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              if (source[index].CalcLevel() < myPhotonRoomParam.unitlv)
                continue;
            }
          }
          unitDataList2.Add(source[index]);
        }
      }
      unitDataList1.AddRange((IEnumerable<UnitData>) unitDataList2);
      return unitDataList1.ToArray();
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
      this.LockWindow(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mRaidSettings, (UnityEngine.Object) null))
      {
        this.mRaidSettings.Close();
        this.mRaidSettings = (RaidSettingsWindow) null;
      }
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
      Network.RequestAPI((WebAPI) new ReqBtlComRaid(this.mCurrentQuest.iname, this.mNumRaids, new Network.ResponseCallback(this.RecvRaidResult), 0), false);
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
        if (jsonObject.body.cards != null)
        {
          for (int index = 0; index < jsonObject.body.cards.Length; ++index)
          {
            GlobalVars.IsDirtyConceptCardData.Set(true);
            if (jsonObject.body.cards[index].IsGetUnit)
              FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(jsonObject.body.cards[index].iname));
          }
        }
        if (jsonObject.body.btlinfos != null)
        {
          for (int index1 = 0; index1 < jsonObject.body.btlinfos.Length; ++index1)
          {
            BattleCore.Json_BtlInfo btlinfo = jsonObject.body.btlinfos[index1];
            int length = btlinfo.drops == null ? 0 : btlinfo.drops.Length;
            RaidQuestResult raidQuestResult = new RaidQuestResult();
            raidQuestResult.index = index1;
            raidQuestResult.pexp = this.mCurrentQuest.pexp;
            raidQuestResult.uexp = this.mCurrentQuest.uexp;
            raidQuestResult.gold = this.mCurrentQuest.gold;
            raidQuestResult.drops = new QuestResult.DropItemData[length];
            if (btlinfo.drops != null)
            {
              for (int index2 = 0; index2 < btlinfo.drops.Length; ++index2)
              {
                ItemParam itemParam = (ItemParam) null;
                ConceptCardParam conceptCardParam = (ConceptCardParam) null;
                if (btlinfo.drops[index2].dropItemType != EBattleRewardType.None)
                {
                  if (btlinfo.drops[index2].dropItemType == EBattleRewardType.Item)
                    itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(btlinfo.drops[index2].iname);
                  else if (btlinfo.drops[index2].dropItemType == EBattleRewardType.ConceptCard)
                    conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(btlinfo.drops[index2].iname);
                  else
                    DebugUtility.LogError(string.Format("不明なドロップ品が登録されています。iname:{0} (itype:{1})", (object) btlinfo.drops[index2].iname, (object) btlinfo.drops[index2].itype));
                  if (itemParam != null)
                  {
                    raidQuestResult.drops[index2] = new QuestResult.DropItemData();
                    raidQuestResult.drops[index2].SetupDropItemData(EBattleRewardType.Item, (long) index2, btlinfo.drops[index2].iname, btlinfo.drops[index2].num);
                  }
                  else if (conceptCardParam != null)
                  {
                    GlobalVars.IsDirtyConceptCardData.Set(true);
                    raidQuestResult.drops[index2] = new QuestResult.DropItemData();
                    raidQuestResult.drops[index2].SetupDropItemData(EBattleRewardType.ConceptCard, (long) index2, btlinfo.drops[index2].iname, btlinfo.drops[index2].num);
                  }
                }
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
      return (IEnumerator) new PartyWindow2.\u003CShowRaidResultAsync\u003Ec__Iterator12C()
      {
        \u003C\u003Ef__this = this
      };
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
            FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            int num = (int) fixParam.EliteResetMax - (int) this.mCurrentQuest.dailyReset;
            int coin = 0;
            if (fixParam.EliteResetCosts != null)
              coin = (int) this.mCurrentQuest.dailyReset >= fixParam.EliteResetCosts.Length ? (int) fixParam.EliteResetCosts[fixParam.EliteResetCosts.Length - 1] : (int) fixParam.EliteResetCosts[(int) this.mCurrentQuest.dailyReset];
            string msg = string.Format(LocalizedText.Get("sys.QUEST_CHALLENGE_RESET"), (object) coin, (object) num);
            UIUtility.ConfirmBox(msg, (string) null, (UIUtility.DialogResultEvent) (g =>
            {
              if (MonoSingleton<GameManager>.Instance.Player.Coin < coin)
              {
                msg = LocalizedText.Get("sys.OUTOFCOIN");
                UIUtility.SystemMessage((string) null, msg, (UIUtility.DialogResultEvent) (gob => {}), (GameObject) null, false, -1);
              }
              else
                Network.RequestAPI((WebAPI) new ReqBtlComReset(this.mCurrentQuest.iname, new Network.ResponseCallback(this.OnResetChallenge)), false);
            }), (UIUtility.DialogResultEvent) (g => {}), (GameObject) null, false, -1);
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
                UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), new UIUtility.DialogResultEvent(this.JumpToBefore), (GameObject) null, false, -1);
                return;
              }
              if (this.mCurrentQuest.IsKeyQuest && !this.mCurrentQuest.IsKeyUnlock(-1L))
              {
                UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
                return;
              }
              if (MonoSingleton<GameManager>.Instance.Player.Stamina < this.mCurrentQuest.RequiredApWithPlayerLv(MonoSingleton<GameManager>.Instance.Player.Lv, true))
              {
                MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true, this);
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
            int availableMainMemberSlots = this.AvailableMainMemberSlots;
            List<string> stringList = new List<string>();
            bool flag1 = false;
            for (int index = 0; index < availableMainMemberSlots; ++index)
            {
              if (this.mCurrentParty.Units.Length > index)
              {
                if (this.mCurrentParty.Units[index] != null || this.mSlotData[index].Type == SRPG.PartySlotType.Npc || (this.mSlotData[index].Type == SRPG.PartySlotType.NpcHero || this.mSlotData[index].Type == SRPG.PartySlotType.ForcedHero))
                  ++numMainUnits;
                if ((this.mLockedPartySlots & 1 << index) == 0)
                {
                  ++num1;
                  if (this.mCurrentParty.Units[index] != null && !this.mCurrentQuest.IsUnitAllowed(this.mCurrentParty.Units[index]))
                    ++num2;
                }
              }
            }
            if (this.EnableHeroSolo && this.mGuestUnit != null && this.mGuestUnit.Count > 0)
            {
              int count = this.mGuestUnit.Count;
              if (numMainUnits < 1 && count == 1)
                flag1 = true;
              numMainUnits += count;
              num1 += count;
            }
            string[] force_units = (string[]) null;
            if (this.mCurrentQuest.questParty != null)
              force_units = ((IEnumerable<PartySlotTypeUnitPair>) this.mCurrentQuest.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot =>
              {
                if (slot.Type != SRPG.PartySlotType.ForcedHero)
                  return slot.Type == SRPG.PartySlotType.Forced;
                return true;
              })).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
            else
              force_units = this.mCurrentQuest.units.GetList();
            if (force_units != null)
            {
              for (int i = 0; i < force_units.Length; ++i)
              {
                if (0 > MonoSingleton<GameManager>.Instance.Player.Units.FindIndex((Predicate<UnitData>) (u => u.UnitParam.iname == force_units[i])))
                {
                  UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(force_units[i]);
                  stringList.Add(unitParam.name);
                }
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
            if (!this.CheckMember(numMainUnits))
              return;
            string empty1 = string.Empty;
            if (!this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.mCurrentParty.Units, ref empty1) && (!this.EnableHeroSolo || !this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.mGuestUnit, ref empty1)))
            {
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty1), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
              return;
            }
            if (this.mCurrentSupport != null && this.mCurrentSupport.Unit != null && !this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentSupport.Unit, ref empty1))
            {
              UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get(empty1), (UIUtility.DialogResultEvent) (dialog => {}), (GameObject) null, false, -1);
              return;
            }
            if (this.mCurrentQuest.IsCharacterQuest())
            {
              List<UnitData> unitDataList = new List<UnitData>((IEnumerable<UnitData>) this.mCurrentParty.Units);
              unitDataList.Add(this.mGuestUnit[0]);
              for (int index1 = 0; index1 < unitDataList.Count; ++index1)
              {
                UnitData unitData = unitDataList[index1];
                if (unitData != null && unitData.UnitID == this.mGuestUnit[0].UnitID)
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
            if (!flag1 && numMainUnits < num1 && this.PartyType != PartyWindow2.EditPartyTypes.MultiTower)
            {
              if (this.PartyType == PartyWindow2.EditPartyTypes.RankMatch)
              {
                UIUtility.SystemMessage(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL_INVALID"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
                return;
              }
              UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL"), (UIUtility.DialogResultEvent) (dialog => this.PostForwardPressed()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1, (string) null, (string) null);
              return;
            }
          }
          this.PostForwardPressed();
        }
      }
      else if (this.PartyType == PartyWindow2.EditPartyTypes.MultiTower)
        this.SaveAndActivatePin(8);
      else if (this.PartyType == PartyWindow2.EditPartyTypes.Ordeal)
      {
        GlobalVars.OrdealParties = this.mTeams;
        GlobalVars.OrdealSupports = this.mSupports;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
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
      if (this.mCurrentQuest != null && !this.mCurrentQuest.IsMultiTower)
      {
        string empty = string.Empty;
        if (!this.mCurrentQuest.IsEntryQuestCondition((IEnumerable<UnitData>) this.mCurrentParty.Units, ref empty))
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
        this.SaveTeamPresets();
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(this.mCurrentPartyType.ToPlayerPartyType());
        List<UnitData> unitDataList = new List<UnitData>();
        for (int mainmemberStart = this.mCurrentParty.PartyData.MAINMEMBER_START; mainmemberStart < this.mCurrentParty.PartyData.MAX_MAINMEMBER; ++mainmemberStart)
        {
          PartySlotData partySlotData = this.mSlotData[mainmemberStart];
          if (partySlotData.Type != SRPG.PartySlotType.Npc && partySlotData.Type != SRPG.PartySlotType.NpcHero)
            unitDataList.Add(this.mCurrentParty.Units[mainmemberStart]);
        }
        for (int count = unitDataList.Count; count < this.mCurrentParty.PartyData.MAX_MAINMEMBER; ++count)
          unitDataList.Add((UnitData) null);
        for (int submemberStart = this.mCurrentParty.PartyData.SUBMEMBER_START; submemberStart <= this.mCurrentParty.PartyData.SUBMEMBER_END; ++submemberStart)
        {
          PartySlotData partySlotData = this.mSlotData[submemberStart];
          if (partySlotData.Type != SRPG.PartySlotType.Npc && partySlotData.Type != SRPG.PartySlotType.NpcHero)
            unitDataList.Add(this.mCurrentParty.Units[submemberStart]);
        }
        for (int count = unitDataList.Count; count < this.mCurrentParty.PartyData.MAX_UNIT; ++count)
          unitDataList.Add((UnitData) null);
        for (int index = 0; index < unitDataList.Count; ++index)
        {
          long uniqueid = unitDataList[index] == null ? 0L : unitDataList[index].UniqueID;
          partyOfType.SetUnitUniqueID(index, uniqueid);
        }
        Network.RequestAPI((WebAPI) new ReqParty(new Network.ResponseCallback(this.SavePartyCallback), false, false, false), false);
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

    private void JumpToBefore(GameObject dialog)
    {
      BackHandler.Invoke();
    }

    [DebuggerHidden]
    private IEnumerator WaitForSave()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PartyWindow2.\u003CWaitForSave\u003Ec__Iterator12D()
      {
        \u003C\u003Ef__this = this
      };
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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestCampaigns, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestCampaigns.GetQuestCampaignList, (UnityEngine.Object) null))
        this.QuestCampaigns.GetQuestCampaignList.RefreshIcons();
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    protected void RefreshNoneQuestInfo(bool keepTeam)
    {
      this.mIsHeloOnly = false;
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
      int index1 = 0;
      for (int index2 = 0; index2 < this.UnitSlots.Length && index2 < this.mCurrentParty.Units.Length && index2 < this.mSlotData.Count; ++index2)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index2], (UnityEngine.Object) null))
        {
          this.UnitSlots[index2].SetSlotData<QuestParam>(this.mCurrentQuest);
          PartySlotData partySlotData = this.mSlotData[index2];
          if (partySlotData.Type == SRPG.PartySlotType.ForcedHero)
          {
            if (this.mGuestUnit != null && index1 < this.mGuestUnit.Count)
              this.UnitSlots[index2].SetSlotData<UnitData>(PartyUtility.FindUnit(this.mGuestUnit[index1], this.mOwnUnits));
            ++index1;
          }
          else if (partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero)
          {
            UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(partySlotData.UnitName);
            this.UnitSlots[index2].SetSlotData<UnitParam>(unitParam);
          }
          else
            this.UnitSlots[index2].SetSlotData<UnitData>(PartyUtility.FindUnit(this.mCurrentParty.Units[index2], this.mOwnUnits));
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
      {
        if (this.mSupportSlotData != null && this.mSupportSlotData.Type != SRPG.PartySlotType.Free)
          this.mCurrentSupport = (SupportData) null;
        this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
        this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
        this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport == null ? (UnitData) null : this.mCurrentSupport.Unit);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestInfo, (UnityEngine.Object) null))
        this.QuestInfo.SetActive(false);
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

    public void RefreshArenaDefUnits()
    {
      if (this.mCurrentParty == null)
      {
        this.mOwnUnits = new List<UnitData>((IEnumerable<UnitData>) ArenaDefenceUnits.mArenaDefUnits);
        for (int index = 0; index < this.mOwnUnits.Count; ++index)
          this.mOwnUnits[index].TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
      }
      else
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        this.mOwnUnits = new List<UnitData>((IEnumerable<UnitData>) ArenaDefenceUnits.mArenaDefUnits);
        int num = 3;
        for (int index1 = 0; index1 < this.mOwnUnits.Count; ++index1)
        {
          for (int index2 = 0; index2 < num; ++index2)
          {
            if (this.mCurrentParty.Units[index2] != null && this.mOwnUnits[index1].UnitID == this.mCurrentParty.Units[index2].UnitID)
            {
              this.mOwnUnits[index1].Setup(player.Units[index1]);
              this.mOwnUnits[index1].TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
              this.mOwnUnits[index1].SetJob(PlayerPartyTypes.ArenaDef);
            }
          }
        }
      }
    }

    public void Refresh(bool keepTeam = false)
    {
      if (!this.UseQuestInfo)
      {
        this.RefreshNoneQuestInfo(keepTeam);
      }
      else
      {
        this.mIsHeloOnly = this.IsHeroSoloParty();
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
          if (playerPartyType == PlayerPartyTypes.ArenaDef)
          {
            ArenaDefenceUnits.CompleteLoading();
            this.RefreshArenaDefUnits();
          }
          else
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
        int index1 = 0;
        for (int index2 = 0; index2 < this.UnitSlots.Length && index2 < this.mCurrentParty.Units.Length && index2 < this.mSlotData.Count; ++index2)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index2], (UnityEngine.Object) null))
          {
            this.UnitSlots[index2].SetSlotData<QuestParam>(this.mCurrentQuest);
            PartySlotData partySlotData = this.mSlotData[index2];
            if (partySlotData.Type == SRPG.PartySlotType.ForcedHero)
            {
              if (this.mGuestUnit != null && index1 < this.mGuestUnit.Count)
                this.UnitSlots[index2].SetSlotData<UnitData>(PartyUtility.FindUnit(this.mGuestUnit[index1], this.mOwnUnits));
              ++index1;
            }
            else if (partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero)
            {
              UnitParam unitParam = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(partySlotData.UnitName);
              this.UnitSlots[index2].SetSlotData<UnitParam>(unitParam);
            }
            else
              this.UnitSlots[index2].SetSlotData<UnitData>(PartyUtility.FindUnit(this.mCurrentParty.Units[index2], this.mOwnUnits));
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        {
          if (this.mSupportSlotData != null && this.mSupportSlotData.Type != SRPG.PartySlotType.Free)
            this.mCurrentSupport = (SupportData) null;
          this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
          this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
          this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport == null ? (UnitData) null : this.mCurrentSupport.Unit);
        }
        if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestUnitCond, (UnityEngine.Object) null) && (this.mGuestUnit != null && this.mGuestUnit.Count > 0))
        {
          DataSource.Bind<UnitData>(this.QuestUnitCond, this.mGuestUnit[0]);
          GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.QUEST_UNIT_ENTRYCONDITION);
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
        if (this.GetPartyCondType() == PartyCondType.Forced || this.IsFixedParty())
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
          bool flag = this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character || (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Tower) || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Ordeal;
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
          {
            if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event && this.ContainsNotFree())
              ((Component) this.RenameButton).get_gameObject().SetActive(false);
            else
              ((Component) this.RenameButton).get_gameObject().SetActive(flag);
          }
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

    private void CreateSlots()
    {
      GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
      GameUtility.DestroyGameObject((Component) this.FriendSlot);
      this.mSlotData.Clear();
      List<PartySlotData> partySlotDataList1 = new List<PartySlotData>();
      List<PartySlotData> partySlotDataList2 = new List<PartySlotData>();
      PartySlotData slotData = (PartySlotData) null;
      if (this.mCurrentQuest == null)
      {
        partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
        partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
        partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
        partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
        slotData = new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Support, false);
        partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub1, false));
        partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub2, false));
      }
      else if (this.mCurrentQuest.questParty != null)
      {
        QuestPartyParam questParty = this.mCurrentQuest.questParty;
        partySlotDataList1.Add(new PartySlotData(questParty.type_1, questParty.unit_1, PartySlotIndex.Main1, false));
        partySlotDataList1.Add(new PartySlotData(questParty.type_2, questParty.unit_2, PartySlotIndex.Main2, false));
        partySlotDataList1.Add(new PartySlotData(questParty.type_3, questParty.unit_3, PartySlotIndex.Main3, false));
        partySlotDataList1.Add(new PartySlotData(questParty.type_4, questParty.unit_4, PartySlotIndex.Main4, false));
        slotData = new PartySlotData(questParty.support_type, (string) null, PartySlotIndex.Support, false);
        partySlotDataList2.Add(new PartySlotData(questParty.subtype_1, questParty.subunit_1, PartySlotIndex.Sub1, false));
        partySlotDataList2.Add(new PartySlotData(questParty.subtype_2, questParty.subunit_2, PartySlotIndex.Sub2, false));
      }
      else
      {
        string unitName1 = this.mCurrentQuest.units == null || this.mCurrentQuest.units.Length <= 0 ? (string) null : this.mCurrentQuest.units.GetList()[0];
        if (this.GetPartyCondType() != PartyCondType.Forced)
        {
          if (this.mCurrentQuest.type == QuestTypes.Tower)
          {
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main5, false));
            slotData = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub1, false));
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub2, false));
          }
          else if (this.mCurrentQuest.type == QuestTypes.Ordeal)
          {
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
            slotData = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
          }
          else if (this.mCurrentQuest.type == QuestTypes.VersusFree || this.mCurrentQuest.type == QuestTypes.RankMatch || this.mCurrentQuest.type == QuestTypes.VersusRank)
          {
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main4, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main5, false));
          }
          else if (this.mCurrentQuest.type == QuestTypes.Arena)
          {
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main4, false));
            slotData = new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Support, false);
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub1, false));
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub2, false));
          }
          else if (this.mCurrentQuest.type == QuestTypes.Multi)
          {
            MyPhoton.MyRoom currentRoom = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
            JSON_MyPhotonRoomParam myPhotonRoomParam = currentRoom == null || string.IsNullOrEmpty(currentRoom.json) ? (JSON_MyPhotonRoomParam) null : JSON_MyPhotonRoomParam.Parse(currentRoom.json);
            if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.RAID && myPhotonRoomParam != null)
            {
              int unitSlotNum = myPhotonRoomParam.GetUnitSlotNum();
              for (int index = 0; index < 4; ++index)
              {
                if (index < unitSlotNum)
                  partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, (PartySlotIndex) index, false));
                else
                  partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, (PartySlotIndex) index, true));
              }
            }
            slotData = new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Support, true);
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub1, true));
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub2, true));
          }
          else
          {
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main1, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main2, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main3, false));
            if (string.IsNullOrEmpty(unitName1))
              partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Main4, false));
            else
              partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main4, false));
            slotData = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub1, false));
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Sub2, false));
          }
        }
        else
        {
          QuestCondParam cond = this.GetPartyCondition();
          if (cond.unit.Length > 1)
          {
            int index = 0;
            Func<string> func = (Func<string>) (() =>
            {
              if (index >= cond.unit.Length)
                return (string) null;
              string[] unit = cond.unit;
              int num;
              index = (num = index) + 1;
              int index = num;
              return unit[index];
            });
            string unitName2 = func();
            partySlotDataList1.Add(new PartySlotData(unitName2 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName2, PartySlotIndex.Main1, false));
            string unitName3 = func();
            partySlotDataList1.Add(new PartySlotData(unitName3 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName3, PartySlotIndex.Main2, false));
            string unitName4 = func();
            partySlotDataList1.Add(new PartySlotData(unitName4 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName4, PartySlotIndex.Main3, false));
            string str = func();
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main4, false));
            slotData = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
            string unitName5 = func();
            partySlotDataList2.Add(new PartySlotData(unitName5 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName5, PartySlotIndex.Sub1, false));
            string unitName6 = func();
            partySlotDataList2.Add(new PartySlotData(unitName6 != null ? SRPG.PartySlotType.Forced : SRPG.PartySlotType.Locked, unitName6, PartySlotIndex.Sub2, false));
          }
          else
          {
            if (cond.unit[0] == unitName1)
              partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.ForcedHero, unitName1, PartySlotIndex.Main1, false));
            else
              partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Forced, cond.unit[0], PartySlotIndex.Main1, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main2, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main3, false));
            partySlotDataList1.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Main4, false));
            slotData = new PartySlotData(SRPG.PartySlotType.Free, (string) null, PartySlotIndex.Support, false);
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub1, false));
            partySlotDataList2.Add(new PartySlotData(SRPG.PartySlotType.Locked, (string) null, PartySlotIndex.Sub2, false));
          }
        }
      }
      List<GenericSlot> genericSlotList = new List<GenericSlot>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MainMemberHolder, (UnityEngine.Object) null) && partySlotDataList1.Count > 0)
      {
        using (List<PartySlotData>.Enumerator enumerator = partySlotDataList1.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            PartySlotData current = enumerator.Current;
            GenericSlot genericSlot = current.Type == SRPG.PartySlotType.Npc || current.Type == SRPG.PartySlotType.NpcHero ? this.CreateSlotObject(current, this.NpcSlotTemplate, this.MainMemberHolder) : this.CreateSlotObject(current, this.UnitSlotTemplate, this.MainMemberHolder);
            genericSlotList.Add(genericSlot);
          }
        }
        if (slotData != null)
          this.FriendSlot = this.CreateSlotObject(slotData, this.UnitSlotTemplate, this.MainMemberHolder);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SubMemberHolder, (UnityEngine.Object) null) && partySlotDataList2.Count > 0)
      {
        using (List<PartySlotData>.Enumerator enumerator = partySlotDataList2.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            PartySlotData current = enumerator.Current;
            GenericSlot genericSlot = current.Type == SRPG.PartySlotType.Npc || current.Type == SRPG.PartySlotType.NpcHero ? this.CreateSlotObject(current, this.NpcSlotTemplate, this.SubMemberHolder) : this.CreateSlotObject(current, this.UnitSlotTemplate, this.SubMemberHolder);
            genericSlotList.Add(genericSlot);
          }
        }
      }
      this.mSlotData.AddRange((IEnumerable<PartySlotData>) partySlotDataList1);
      this.mSlotData.AddRange((IEnumerable<PartySlotData>) partySlotDataList2);
      this.mSupportSlotData = slotData;
      this.UnitSlots = genericSlotList.ToArray();
      for (int index = 0; index < this.UnitSlots.Length && index < this.mSlotData.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitSlots[index], (UnityEngine.Object) null) && this.mSlotData[index].Type == SRPG.PartySlotType.Free)
          this.UnitSlots[index].OnSelect = new GenericSlot.SelectEvent(this.OnUnitSlotClick);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FriendSlot, (UnityEngine.Object) null))
        return;
      if (this.mSupportSlotData != null && this.mSupportSlotData.Type == SRPG.PartySlotType.Free)
        this.FriendSlot.OnSelect = new GenericSlot.SelectEvent(this.OnSupportUnitSlotClick);
      if (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tower)
        return;
      TowerFloorParam towerFloor = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
      if (towerFloor == null)
        return;
      ((Component) this.FriendSlot).get_gameObject().SetActive(towerFloor.can_help);
      ((Component) this.SupportSkill).get_gameObject().SetActive(towerFloor.can_help);
    }

    private GenericSlot CreateSlotObject(PartySlotData slotData, GenericSlot template, Transform parent)
    {
      GenericSlot component = (GenericSlot) ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) ((Component) template).get_gameObject())).GetComponent<GenericSlot>();
      ((Component) component).get_transform().SetParent(parent, false);
      ((Component) component).get_gameObject().SetActive(true);
      component.SetSlotData<PartySlotData>(slotData);
      return component;
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
        for (int index = 0; index < this.mCurrentParty.PartyData.MAX_UNIT; ++index)
        {
          if (index >= 3)
            this.mLockedPartySlots |= 1 << index;
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MultiTower)
      {
        this.mSupportLocked = true;
        this.mItemsLocked = true;
        for (int index = 0; index < this.UnitSlots.Length; ++index)
          ((Selectable) this.UnitSlots[index].SelectButton).set_interactable(true);
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Versus)
      {
        if (MonoSingleton<GameManager>.Instance.GetVSMode(-1L) == VS_MODE.THREE_ON_THREE)
        {
          for (int vswaitmemberStart = this.mCurrentParty.PartyData.VSWAITMEMBER_START; vswaitmemberStart < this.mCurrentParty.PartyData.VSWAITMEMBER_END; ++vswaitmemberStart)
            this.mLockedPartySlots |= 1 << vswaitmemberStart;
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.RankMatch)
      {
        if (MonoSingleton<GameManager>.Instance.GetVSMode(-1L) == VS_MODE.THREE_ON_THREE)
        {
          for (int vswaitmemberStart = this.mCurrentParty.PartyData.VSWAITMEMBER_START; vswaitmemberStart < this.mCurrentParty.PartyData.VSWAITMEMBER_END; ++vswaitmemberStart)
            this.mLockedPartySlots |= 1 << vswaitmemberStart;
        }
      }
      else if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Normal || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Character || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Event)
      {
        this.mSupportLocked = false;
        this.mItemsLocked = false;
        for (int index = 0; index < this.mSlotData.Count; ++index)
        {
          PartySlotData partySlotData = this.mSlotData[index];
          if (partySlotData.Type == SRPG.PartySlotType.Locked || partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero)
            this.mLockedPartySlots |= 1 << index;
        }
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
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Arena || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.ArenaDef || this.mCurrentPartyType == PartyWindow2.EditPartyTypes.MP || (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Event) && this.ContainsNpcOrForced() || (this.GetPartyCondType() == PartyCondType.Forced || this.IsFixedParty()))
        return;
      PartyUtility.SaveTeamPresets(this.mCurrentPartyType, this.mCurrentTeamIndex, this.mTeams, this.ContainsNotFree());
    }

    private void LoadTeam()
    {
      if (this.mCurrentPartyType == PartyWindow2.EditPartyTypes.Auto)
        throw new InvalidPartyTypeException();
      this.mGuestUnit.Clear();
      this.mMaxTeamCount = this.mCurrentPartyType.GetMaxTeamCount();
      PlayerPartyTypes playerPartyType = this.mCurrentPartyType.ToPlayerPartyType();
      this.mTeams.Clear();
      if (this.GetPartyCondType() == PartyCondType.Forced || this.IsFixedParty())
      {
        PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
        UnitData[] src = new UnitData[this.mSlotData.Count];
        for (int index = 0; index < this.mSlotData.Count; ++index)
        {
          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.mSlotData[index].UnitName);
          if (this.mSlotData[index].Type == SRPG.PartySlotType.ForcedHero)
          {
            this.mGuestUnit.Add(unitDataByUnitId);
            src[index] = (UnitData) null;
          }
          else
            src[index] = this.mSlotData[index].Type == SRPG.PartySlotType.Npc || this.mSlotData[index].Type == SRPG.PartySlotType.NpcHero || this.mSlotData[index].Type == SRPG.PartySlotType.Locked && !this.mSlotData[index].IsSettable ? (UnitData) null : unitDataByUnitId;
        }
        PartyEditData partyEditData = new PartyEditData(string.Empty, partyOfType);
        partyEditData.SetUnitsForce(src);
        this.mTeams.Add(partyEditData);
        this.mCurrentParty = this.mTeams[0];
        this.mCurrentTeamIndex = 0;
      }
      else
      {
        if (this.mCurrentPartyType != PartyWindow2.EditPartyTypes.Arena && this.mCurrentPartyType != PartyWindow2.EditPartyTypes.ArenaDef && this.mCurrentPartyType != PartyWindow2.EditPartyTypes.MP)
        {
          bool containsNotFree = this.ContainsNotFree();
          int lastSelectionIndex;
          List<PartyEditData> partyEditDataList = PartyUtility.LoadTeamPresets(this.mCurrentPartyType, out lastSelectionIndex, containsNotFree);
          if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Event && containsNotFree)
            this.mMaxTeamCount = 1;
          if (partyEditDataList != null)
          {
            this.mTeams = partyEditDataList;
            this.mCurrentTeamIndex = lastSelectionIndex;
            if (this.mCurrentTeamIndex < 0 || this.mMaxTeamCount <= this.mCurrentTeamIndex)
              this.mCurrentTeamIndex = 0;
          }
        }
        if (this.mTeams.Count > this.mMaxTeamCount)
          this.mTeams = this.mTeams.Take<PartyEditData>(this.mMaxTeamCount).ToList<PartyEditData>();
        else if (this.mTeams.Count < this.mMaxTeamCount)
        {
          PartyData partyOfType = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(playerPartyType);
          if (playerPartyType == PlayerPartyTypes.Ordeal)
          {
            for (int count = this.mTeams.Count; count < this.mMaxTeamCount; ++count)
              this.mTeams.Add(new PartyEditData(PartyUtility.CreateOrdealPartyNameFromIndex(count), partyOfType));
          }
          else
          {
            for (int count = this.mTeams.Count; count < this.mMaxTeamCount; ++count)
              this.mTeams.Add(new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(count), partyOfType));
          }
        }
        if (this.mCurrentTeamIndex < 0 || this.mCurrentTeamIndex >= this.mTeams.Count)
          this.mCurrentTeamIndex = 0;
        if (this.EnableTeamAssign && (GlobalVars.SelectedTeamIndex >= 0 || GlobalVars.SelectedTeamIndex < this.mMaxTeamCount))
        {
          this.mCurrentTeamIndex = GlobalVars.SelectedTeamIndex;
          this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
        }
        else
          this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
        this.AssignUnits(this.mCurrentParty);
      }
      this.ValidateSupport(this.mMaxTeamCount);
      this.mCurrentSupport = this.mSupports[this.mCurrentTeamIndex];
      this.ResetTeamPulldown(this.mTeams, this.mMaxTeamCount, this.mCurrentQuest);
      this.ChangeEnabledArrowButtons(this.mCurrentTeamIndex, this.mMaxTeamCount);
      this.ChangeEnabledTeamTabs(this.mCurrentTeamIndex);
    }

    private void AssignUnits(PartyEditData partyEditData)
    {
      UnitData[] unitDataArray = new UnitData[this.mSlotData.Count];
      UnitData[] array1 = ((IEnumerable<UnitData>) partyEditData.Units).ToArray<UnitData>();
      string[] array2 = this.mSlotData.Where<PartySlotData>((Func<PartySlotData, bool>) (slot =>
      {
        if (slot.Type != SRPG.PartySlotType.Forced)
          return slot.Type == SRPG.PartySlotType.ForcedHero;
        return true;
      })).Select<PartySlotData, string>((Func<PartySlotData, string>) (slot => slot.UnitName)).ToArray<string>();
      int num1 = 0;
      for (int index = 0; index < unitDataArray.Length && index < this.mSlotData.Count; ++index)
      {
        PartySlotData partySlotData = this.mSlotData[index];
        if (partySlotData.Type == SRPG.PartySlotType.ForcedHero)
        {
          UnitData unitDataByUnitId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(partySlotData.UnitName);
          if (unitDataByUnitId != null)
            this.mGuestUnit.Add(unitDataByUnitId);
          unitDataArray[index] = (UnitData) null;
        }
        else if (partySlotData.Type == SRPG.PartySlotType.Forced)
          unitDataArray[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(partySlotData.UnitName);
        else if (partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero || partySlotData.Type == SRPG.PartySlotType.Locked && !partySlotData.IsSettable)
        {
          unitDataArray[index] = (UnitData) null;
        }
        else
        {
          int num2 = index >= this.mCurrentParty.PartyData.MAX_MAINMEMBER ? this.mCurrentParty.PartyData.MAX_UNIT : this.mCurrentParty.PartyData.MAX_MAINMEMBER;
          while (num1 < num2 && num1 < array1.Length)
          {
            UnitData unitData = array1[num1++];
            if (unitData != null && !((IEnumerable<string>) array2).Contains<string>(unitData.UnitID))
            {
              unitDataArray[index] = unitData;
              break;
            }
          }
        }
      }
      for (int index = 0; index < partyEditData.Units.Length && index < unitDataArray.Length; ++index)
        partyEditData.Units[index] = unitDataArray[index];
      this.ValidateTeam(partyEditData);
    }

    private bool IsFixedParty()
    {
      return !this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Free));
    }

    private bool ContainsNotFree()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type != SRPG.PartySlotType.Free));
    }

    private bool ContainsNpcOrForcedOrForcedHero()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot =>
      {
        if (slot.Type != SRPG.PartySlotType.Npc && slot.Type != SRPG.PartySlotType.NpcHero && slot.Type != SRPG.PartySlotType.Forced)
          return slot.Type == SRPG.PartySlotType.ForcedHero;
        return true;
      }));
    }

    private bool ContainsNpcOrForced()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot =>
      {
        if (slot.Type != SRPG.PartySlotType.Npc && slot.Type != SRPG.PartySlotType.NpcHero)
          return slot.Type == SRPG.PartySlotType.Forced;
        return true;
      }));
    }

    private bool ContainsNpcOrForcedOrLocked()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot =>
      {
        if (slot.Type != SRPG.PartySlotType.Npc && slot.Type != SRPG.PartySlotType.NpcHero && slot.Type != SRPG.PartySlotType.Forced)
          return slot.Type == SRPG.PartySlotType.Locked;
        return true;
      }));
    }

    private bool ContainsNpcOrForcedHero()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot =>
      {
        if (slot.Type != SRPG.PartySlotType.Npc && slot.Type != SRPG.PartySlotType.NpcHero)
          return slot.Type == SRPG.PartySlotType.ForcedHero;
        return true;
      }));
    }

    private bool ContainsNpc()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot =>
      {
        if (slot.Type != SRPG.PartySlotType.Npc)
          return slot.Type == SRPG.PartySlotType.NpcHero;
        return true;
      }));
    }

    private bool ContainsForcedHero()
    {
      return this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero));
    }

    private bool IsSettableSlot(PartySlotData slotData)
    {
      if (slotData.Type == SRPG.PartySlotType.Free)
        return true;
      if (slotData.Type == SRPG.PartySlotType.Locked)
        return slotData.IsSettable;
      return false;
    }

    private bool IsHeroSoloParty()
    {
      bool flag1 = this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero));
      bool flag2 = !this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Free));
      bool flag3 = !this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.Forced));
      bool flag4 = !this.mSlotData.Any<PartySlotData>((Func<PartySlotData, bool>) (slot =>
      {
        if (slot.Type != SRPG.PartySlotType.Npc)
          return slot.Type == SRPG.PartySlotType.NpcHero;
        return true;
      }));
      if (flag1 && flag2 && flag3)
        return flag4;
      return false;
    }

    private void ValidateTeam(PartyEditData partyEditData)
    {
      bool flag = false;
      string[] array1 = this.mSlotData.Where<PartySlotData>((Func<PartySlotData, bool>) (slot =>
      {
        if (slot.Type != SRPG.PartySlotType.Forced)
          return slot.Type == SRPG.PartySlotType.ForcedHero;
        return true;
      })).Select<PartySlotData, string>((Func<PartySlotData, string>) (slot => slot.UnitName)).ToArray<string>();
      if ((this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Ordeal) && (this.mSlotData[0].Type == SRPG.PartySlotType.Free && partyEditData.Units[0] == null))
        flag |= PartyUtility.AutoSetLeaderUnit(this.mCurrentQuest, partyEditData, array1, MonoSingleton<GameManager>.Instance.Player.Units, this.mSlotData);
      for (int index = 0; index < partyEditData.Units.Length && index < this.mSlotData.Count; ++index)
      {
        PartySlotData partySlotData = this.mSlotData[index];
        if (partySlotData.Type == SRPG.PartySlotType.Npc || partySlotData.Type == SRPG.PartySlotType.NpcHero || partySlotData.Type == SRPG.PartySlotType.ForcedHero || partySlotData.Type == SRPG.PartySlotType.Locked && !partySlotData.IsSettable)
          partyEditData.Units[index] = (UnitData) null;
        else if (partySlotData.Type == SRPG.PartySlotType.Forced)
          partyEditData.Units[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(partySlotData.UnitName);
      }
      if (this.mCurrentQuest != null)
      {
        if (this.mCurrentQuest.type == QuestTypes.Event || this.mCurrentQuest.type == QuestTypes.Gps || this.mCurrentQuest.type == QuestTypes.Beginner)
          flag |= PartyUtility.SetUnitIfEmptyParty(this.mCurrentQuest, this.mTeams, array1, this.mSlotData);
        else if (PartyUtility.IsHeloQuest(this.mCurrentQuest))
        {
          string[] array2 = this.mSlotData.Where<PartySlotData>((Func<PartySlotData, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero)).Select<PartySlotData, string>((Func<PartySlotData, string>) (slot => slot.UnitName)).ToArray<string>();
          for (int index = 0; index < this.mTeams.Count; ++index)
            flag |= PartyUtility.PartyUnitsRemoveHelo(this.mTeams[index], array2);
          if (!this.mIsHeloOnly)
            flag |= PartyUtility.SetUnitIfEmptyParty(this.mCurrentQuest, this.mTeams, array1, this.mSlotData);
        }
      }
      if (!flag)
        return;
      this.SaveTeamPresets();
    }

    private void ValidateSupport(int maxTeamCount)
    {
      if (this.mSupports.Count < maxTeamCount)
      {
        for (int count = this.mSupports.Count; count < maxTeamCount; ++count)
          this.mSupports.Add((SupportData) null);
      }
      else
      {
        if (this.mSupports.Count <= maxTeamCount)
          return;
        this.mSupports.Take<SupportData>(maxTeamCount);
      }
    }

    private PartyCondType GetPartyCondType()
    {
      if (this.mCurrentQuest != null)
      {
        if (this.mCurrentQuest.type == QuestTypes.Character && this.mCurrentQuest.EntryConditionCh != null)
          return this.mCurrentQuest.EntryConditionCh.party_type;
        if (this.mCurrentQuest.EntryCondition != null)
          return this.mCurrentQuest.EntryCondition.party_type;
      }
      return PartyCondType.None;
    }

    private QuestCondParam GetPartyCondition()
    {
      if (this.mCurrentQuest == null)
        return (QuestCondParam) null;
      if (this.mCurrentQuest.type == QuestTypes.Character)
        return this.mCurrentQuest.EntryConditionCh;
      return this.mCurrentQuest.EntryCondition;
    }

    private void ResetTeamPulldown(List<PartyEditData> teams, int maxTeams, QuestParam currentQuest)
    {
      if (this.GetPartyCondType() == PartyCondType.Forced || this.mIsHeloOnly || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TeamPulldown, (UnityEngine.Object) null))
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
        if (!player.Units[index2].UnitParam.IsHero())
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
        return this.mCurrentParty.PartyData.MAX_MAINMEMBER;
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
        case 50:
          this.RefreshQuest();
          break;
        case 130:
          this.Refresh(false);
          break;
        case 140:
          this.Refresh(true);
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

    private void RefreshUnitList()
    {
      List<UnitData> unitDataList = new List<UnitData>((IEnumerable<UnitData>) this.mOwnUnits);
      this.UnitList.ClearItems();
      bool selectedSlotIsEmpty = this.mCurrentParty.Units[this.mSelectedSlotIndex] == null;
      if ((this.mSelectedSlotIndex > 0 || this.mIsHeloOnly) && (!selectedSlotIsEmpty || this.AlwaysShowRemoveUnit))
        this.UnitList.AddItem(0);
      bool heroesAvailable = PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest);
      if (this.UseQuestInfo && (this.mCurrentQuest.type == QuestTypes.Event || this.mCurrentQuest.type == QuestTypes.Beginner || this.PartyType == PartyWindow2.EditPartyTypes.Character))
      {
        string[] strArray = this.mCurrentQuest.questParty == null ? this.mCurrentQuest.units.GetList() : ((IEnumerable<PartySlotTypeUnitPair>) this.mCurrentQuest.questParty.GetMainSubSlots()).Where<PartySlotTypeUnitPair>((Func<PartySlotTypeUnitPair, bool>) (slot => slot.Type == SRPG.PartySlotType.ForcedHero)).Select<PartySlotTypeUnitPair, string>((Func<PartySlotTypeUnitPair, string>) (slot => slot.Unit)).ToArray<string>();
        if (strArray != null)
        {
          for (int index = 0; index < strArray.Length; ++index)
          {
            string chQuestHeroId = strArray[index];
            UnitData unitData = unitDataList.FirstOrDefault<UnitData>((Func<UnitData, bool>) (u => u.UnitParam.iname == chQuestHeroId));
            if (unitData != null)
              unitDataList.Remove(unitData);
          }
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
        if (this.mCurrentParty.Units[index] != null && (heroesAvailable || !this.mCurrentParty.Units[index].UnitParam.IsHero()) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (index != 0 || !selectedSlotIsEmpty) || numMainMembers > 1))
        {
          if (this.UseQuestInfo)
          {
            string empty = string.Empty;
            if (!this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units[index], ref empty))
              continue;
          }
          this.UnitList.AddItem(this.mOwnUnits.IndexOf(PartyUtility.FindUnit(this.mCurrentParty.Units[index], this.mOwnUnits)) + 1);
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
        if ((heroesAvailable || !allUnits[index].UnitParam.IsHero()) && (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex || this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END || (allUnits[index] != this.mCurrentParty.Units[0] || !selectedSlotIsEmpty) || numMainMembers > 1))
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

    public void Reopen(bool farceRefresh = false)
    {
      if (farceRefresh || this.mCurrentQuest != null && this.mCurrentQuest.iname != GlobalVars.SelectedQuestID)
      {
        this.RefreshQuest();
        this.CreateSlots();
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
        MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(true, this);
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
      this.mMultiRaidNum = num;
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
      int num = 0;
      for (int index = 0; index < this.mSlotData.Count; ++index)
      {
        PartySlotData partySlotData = this.mSlotData[index];
        if (partySlotData.Type == SRPG.PartySlotType.Free || partySlotData.Type == SRPG.PartySlotType.Forced || (partySlotData.Type == SRPG.PartySlotType.ForcedHero || partySlotData.Type == SRPG.PartySlotType.Npc) || partySlotData.Type == SRPG.PartySlotType.NpcHero)
        {
          num = index;
          break;
        }
      }
      for (int slotIndex = 0; slotIndex < this.mCurrentParty.PartyData.MAX_UNIT; ++slotIndex)
      {
        if (slotIndex != num)
          this.SetPartyUnitForce(slotIndex, (UnitData) null);
      }
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

    public void OnTeamTabChange()
    {
      this.OnTeamChangeImpl((FlowNode_ButtonEvent.currentValue as SerializeValueList).GetInt("tab_index"));
    }

    public void GoToUnitList()
    {
      if (this.PartyType != PartyWindow2.EditPartyTypes.MultiTower)
        return;
      int towerMultiPartyIndex = GlobalVars.SelectedTowerMultiPartyIndex;
      if (!this.IsMultiTowerPartySlot(towerMultiPartyIndex))
        return;
      this.mSelectedSlotIndex = towerMultiPartyIndex;
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
      HashSet<string> source = kyouseiUnits != null ? new HashSet<string>(kyouseiUnits) : new HashSet<string>();
      if (this.mCurrentQuest.type == QuestTypes.Ordeal)
      {
        for (int index = 0; index < this.mMaxTeamCount; ++index)
        {
          if (index != this.mCurrentTeamIndex)
          {
            foreach (UnitData unit in this.mTeams[index].Units)
            {
              if (unit != null)
                source.Add(unit.UnitID);
            }
          }
        }
      }
      using (List<UnitData>.Enumerator enumerator = allUnits.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          UnitData unit = enumerator.Current;
          string str = source.FirstOrDefault<string>((Func<string, bool>) (iname => iname == unit.UnitParam.iname));
          if (str != null)
            source.Remove(str);
          else if ((isHeroAvailable || !unit.UnitParam.IsHero()) && this.mCurrentQuest.IsEntryQuestCondition(unit))
          {
            if (targetElement == EElement.None)
            {
              unitDataList1.Add(unit);
            }
            else
            {
              switch (unit.Element)
              {
                case EElement.Fire:
                  unitDataList2.Add(unit);
                  continue;
                case EElement.Water:
                  unitDataList3.Add(unit);
                  continue;
                case EElement.Wind:
                  unitDataList4.Add(unit);
                  continue;
                case EElement.Thunder:
                  unitDataList5.Add(unit);
                  continue;
                case EElement.Shine:
                  unitDataList6.Add(unit);
                  continue;
                case EElement.Dark:
                  unitDataList7.Add(unit);
                  continue;
                default:
                  continue;
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
      this.BreakupTeamImpl();
      List<string> removeTarget = new List<string>();
      using (List<PartySlotData>.Enumerator enumerator = this.mSlotData.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          PartySlotData current = enumerator.Current;
          if (current.Type == SRPG.PartySlotType.Forced || current.Type == SRPG.PartySlotType.ForcedHero)
            removeTarget.Add(current.UnitName);
        }
      }
      List<List<UnitData>> unitDataListList = this.SeparateUnitByElement(MonoSingleton<GameManager>.Instance.Player.Units.Where<UnitData>((Func<UnitData, bool>) (unit => !removeTarget.Contains(unit.UnitID))).ToList<UnitData>(), (IEnumerable<string>) this.mCurrentQuest.units.GetList(), targetElement, PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest));
      List<Comparison<UnitData>> targetComparators = new List<Comparison<UnitData>>()
      {
        new Comparison<UnitData>(PartyWindow2.CompareTo_Total),
        new Comparison<UnitData>(PartyWindow2.CompareTo_HP),
        new Comparison<UnitData>(PartyWindow2.CompareTo_Attack),
        new Comparison<UnitData>(PartyWindow2.CompareTo_Defense),
        new Comparison<UnitData>(PartyWindow2.CompareTo_Magic),
        new Comparison<UnitData>(PartyWindow2.CompareTo_Mind),
        new Comparison<UnitData>(PartyWindow2.CompareTo_Speed),
        new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeSlash),
        new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeStab),
        new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeBlow),
        new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeShot),
        new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeMagic),
        new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeNone)
      };
      int comparatorOrder = PartyUtility.RecommendTypeToComparatorOrder(targetType);
      if (comparatorOrder >= 0 && comparatorOrder < targetComparators.Count)
      {
        Comparison<UnitData> comparison = targetComparators[comparatorOrder];
        targetComparators.RemoveAt(comparatorOrder);
        targetComparators.Insert(0, comparison);
      }
      using (List<List<UnitData>>.Enumerator enumerator1 = unitDataListList.GetEnumerator())
      {
        while (enumerator1.MoveNext())
          enumerator1.Current.Sort((Comparison<UnitData>) ((x, y) =>
          {
            using (List<Comparison<UnitData>>.Enumerator enumerator = targetComparators.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                int num = enumerator.Current(x, y);
                if (num != 0)
                  return num;
              }
            }
            return UnitData.CompareTo_Iname(x, y);
          }));
      }
      int maxUnit = this.mCurrentParty.PartyData.MAX_UNIT;
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
      int num1 = 0;
      for (int slotIndex = 0; slotIndex < partyData.MAX_UNIT && slotIndex < this.mSlotData.Count && num1 < unitDataList.Count; ++slotIndex)
      {
        if (this.IsSettableSlot(this.mSlotData[slotIndex]))
          this.SetPartyUnit(slotIndex, unitDataList[num1++]);
      }
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
      this.mUnitListWindow.AddData("data_party", (object) this.mTeams.ToArray());
      this.mUnitListWindow.AddData("data_party_index", (object) this.mCurrentTeamIndex);
      this.mUnitListWindow.AddData("data_quest", (object) this.mCurrentQuest);
      this.mUnitListWindow.AddData("data_slot", (object) this.mSelectedSlotIndex);
      this.mUnitListWindow.AddData("data_heroOnly", (object) this.mIsHeloOnly);
      if (this.mCurrentQuest != null && this.mCurrentQuest.type == QuestTypes.Tower)
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
      int slotIndex1;
      if (selectedSlotIndex < this.mCurrentParty.PartyData.MAX_MAINMEMBER)
      {
        int num = selectedSlotIndex;
        for (int index = selectedSlotIndex; index >= 0; --index)
        {
          if (this.IsSettableSlot(this.mSlotData[index]) && this.mCurrentParty.Units[index] == null)
            num = index;
        }
        slotIndex1 = num;
      }
      else
      {
        int num = selectedSlotIndex;
        for (int index = selectedSlotIndex; index >= this.mCurrentParty.PartyData.MAX_MAINMEMBER; --index)
        {
          if (this.IsSettableSlot(this.mSlotData[index]) && this.mCurrentParty.Units[index] == null)
            num = index;
        }
        slotIndex1 = num;
      }
      UnitData unit2 = (UnitData) null;
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
        unit2 = currentValue.GetDataSource<UnitData>("_self");
      if (unit2 != null && unit2 != unit1)
      {
        int slotIndex2 = this.mCurrentParty.IndexOf(unit2);
        if (slotIndex2 >= 0 && slotIndex1 != slotIndex2)
        {
          this.SetPartyUnit(slotIndex1, unit2);
          this.SetPartyUnit(slotIndex2, unit1);
        }
        else
          this.SetPartyUnit(slotIndex1, unit2);
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
      this.mUnitListWindow.AddData("data_support", (object) this.mSupports.ToArray());
      this.mUnitListWindow.AddData("data_party_index", (object) this.mCurrentTeamIndex);
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
      if (MonoSingleton<GameManager>.Instance.Player.Gold < this.CalculateTotalSupportCost(this.mSelectedSupport))
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

    private int CalculateTotalSupportCost(SupportData support)
    {
      if (this.mCurrentPartyType != PartyWindow2.EditPartyTypes.Ordeal)
        return support.GetCost();
      int cost = support.GetCost();
      for (int index = 0; index < this.mSupports.Count; ++index)
      {
        if (index != this.mCurrentTeamIndex && this.mSupports[index] != null)
          cost += this.mSupports[index].GetCost();
      }
      return cost;
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
      Ordeal,
      RankMatch,
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
      public Json_BtlRewardConceptCard[] cards;
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
