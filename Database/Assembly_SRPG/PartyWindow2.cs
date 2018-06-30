namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [Pin(4, "開く", 1, 2), Pin(3, "戻る", 1, 1), Pin(100, "ユニット選択", 0, 100), Pin(0x65, "サポートユニット選択", 0, 0x65), Pin(110, "ユニット解除", 0, 110), Pin(0x6f, "サポートユニット解除", 0, 0x6f), Pin(0x77, "ユニットリストウィンド開いた", 0, 0x77), Pin(120, "ユニットリストウィンド閉じ始めた", 0, 120), Pin(130, "強制的に更新", 0, 130), Pin(140, "強制的に更新(チームのリロードなし)", 0, 140), Pin(200, "フレンドがサポートに設定された", 1, 200), Pin(210, "フレンド以外がサポートが設定された", 1, 210), Pin(220, "サポートが解除された", 1, 220), Pin(7, "AP回復アイテム", 1, 7), Pin(6, "画面アンロック", 1, 6), Pin(5, "画面ロック", 1, 5), Pin(1, "進む", 1, 4), Pin(50, "クエスト更新", 0, 50), Pin(0x29, "おまかせ編成完了", 0, 0x29), Pin(40, "チーム名変更完了", 0, 40), Pin(0x15, "アイテム選択完了", 1, 0x15), Pin(20, "アイテム選択開始", 1, 20), Pin(11, "ユニット選択完了", 1, 11), Pin(10, "ユニット選択開始", 1, 10), Pin(8, "マルチタワー用進む", 1, 8)]
    public class PartyWindow2 : MonoBehaviour, IFlowInterface, ISortableList
    {
        public int MaxRaidNum;
        public int DefaultRaidNum;
        public EditPartyTypes PartyType;
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
        [StringIsResourcePath(typeof(GameObject))]
        public string QuestDetail;
        [StringIsResourcePath(typeof(GameObject))]
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
        [StringIsResourcePath(typeof(RaidResultWindow))]
        public string RaidResultPrefab;
        public GameObject RaidSettingsTemplate;
        [Space(10f)]
        public Toggle ToggleDirectineCut;
        [Space(10f), SerializeField]
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
        protected EditPartyTypes mCurrentPartyType;
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
        private Callback mOnPartySaveSuccess;
        private Callback mOnPartySaveFail;
        private RaidResultWindow mRaidResultWindow;
        private RaidResult mRaidResult;
        private LoadRequest mReqRaidResultWindow;
        private LoadRequest mReqQuestDetail;
        private string[] mUnitFilter;
        private bool mReverse;
        private SRPG_ToggleButton[] mItemFilterToggles;
        private ItemFilterTypes mItemFilter;
        protected GameObject[] mSankaFukaIcons;
        private RaidSettingsWindow mRaidSettings;
        private int mMultiRaidNum;
        private bool mInitialized;
        private bool mIsHeloOnly;
        [Space(10f)]
        public SRPG_Button ButtonMapEffectQuest;
        [StringIsResourcePath(typeof(GameObject))]
        public string PrefabMapEffectQuest;
        private LoadRequest mReqMapEffectQuest;
        public string SceneNameHome;
        private GameObject mMultiErrorMsg;
        private Transform mTrHomeHeader;
        private bool mUnitSlotSelected;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, bool> <>f__am$cache83;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, string> <>f__am$cache84;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache85;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache86;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache87;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, bool> <>f__am$cache88;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, string> <>f__am$cache89;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache8A;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache8B;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache8C;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache8D;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache8E;
        [CompilerGenerated]
        private static Callback <>f__am$cache8F;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache90;
        [CompilerGenerated]
        private static Func<PartySlotData, string> <>f__am$cache91;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache92;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache93;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache94;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache95;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache96;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache97;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache98;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache99;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache9A;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache9B;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache9C;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache9D;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cache9E;
        [CompilerGenerated]
        private static Func<PartySlotData, string> <>f__am$cache9F;
        [CompilerGenerated]
        private static Func<PartySlotData, bool> <>f__am$cacheA0;
        [CompilerGenerated]
        private static Func<PartySlotData, string> <>f__am$cacheA1;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, bool> <>f__am$cacheA2;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, string> <>f__am$cacheA3;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cacheA4;

        public PartyWindow2()
        {
            PlayerPartyTypes[] typesArray1;
            this.MaxRaidNum = 20;
            this.DefaultRaidNum = 10;
            this.PartyType = 1;
            this.TeamTabs = new Toggle[0];
            this.ShowQuestInfo = 1;
            this.UseQuestInfo = 1;
            this.ShowRaidInfo = 1;
            this.ShowForwardButton = 1;
            this.ShowBackButton = 1;
            this.SankaFukaOpacity = 0.5f;
            this.ChosenUnitBadges = new RectTransform[0];
            this.ChosenItemBadges = new RectTransform[0];
            typesArray1 = new PlayerPartyTypes[] { 4 };
            this.SaveJobs = typesArray1;
            this.EnableHeroSolo = 1;
            this.UNIT_LIST_PATH = "UI/UnitPickerWindowEx";
            this.UNITLIST_WINDOW_PATH = "UI/UnitListWindow";
            this.mGuestUnit = new List<UnitData>();
            this.mSlotData = new List<PartySlotData>();
            this.mSupports = new List<SupportData>();
            this.mCurrentItems = new ItemData[5];
            this.mUnitPoolA = new List<RectTransform>();
            this.mUnitPoolB = new List<RectTransform>();
            this.mItemPoolA = new List<RectTransform>();
            this.mItemPoolB = new List<RectTransform>();
            this.mSupportPoolA = new List<RectTransform>();
            this.mSupportPoolB = new List<RectTransform>();
            this.mTeams = new List<PartyEditData>();
            this.mMultiRaidNum = -1;
            this.SceneNameHome = "Home";
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <AssignUnits>m__399(PartySlotData slot)
        {
            return ((slot.Type == 2) ? 1 : (slot.Type == 3));
        }

        [CompilerGenerated]
        private static string <AssignUnits>m__39A(PartySlotData slot)
        {
            return slot.UnitName;
        }

        [CompilerGenerated]
        private static void <CheckMember>m__394(GameObject dialog)
        {
        }

        [CompilerGenerated]
        private static void <CheckMember>m__395(GameObject dialog)
        {
        }

        [CompilerGenerated]
        private static bool <ContainsForcedHero>m__3A2(PartySlotData slot)
        {
            return (slot.Type == 3);
        }

        [CompilerGenerated]
        private static bool <ContainsNotFree>m__39C(PartySlotData slot)
        {
            return ((slot.Type == 0) == 0);
        }

        [CompilerGenerated]
        private static bool <ContainsNpc>m__3A1(PartySlotData slot)
        {
            return ((slot.Type == 4) ? 1 : (slot.Type == 5));
        }

        [CompilerGenerated]
        private static bool <ContainsNpcOrForced>m__39E(PartySlotData slot)
        {
            return (((slot.Type == 4) || (slot.Type == 5)) ? 1 : (slot.Type == 2));
        }

        [CompilerGenerated]
        private static bool <ContainsNpcOrForcedHero>m__3A0(PartySlotData slot)
        {
            return (((slot.Type == 4) || (slot.Type == 5)) ? 1 : (slot.Type == 3));
        }

        [CompilerGenerated]
        private static bool <ContainsNpcOrForcedOrForcedHero>m__39D(PartySlotData slot)
        {
            return ((((slot.Type == 4) || (slot.Type == 5)) || (slot.Type == 2)) ? 1 : (slot.Type == 3));
        }

        [CompilerGenerated]
        private static bool <ContainsNpcOrForcedOrLocked>m__39F(PartySlotData slot)
        {
            return ((((slot.Type == 4) || (slot.Type == 5)) || (slot.Type == 2)) ? 1 : (slot.Type == 1));
        }

        [CompilerGenerated]
        private static bool <IsFixedParty>m__39B(PartySlotData slot)
        {
            return (slot.Type == 0);
        }

        [CompilerGenerated]
        private static bool <IsHeroSoloParty>m__3A3(PartySlotData slot)
        {
            return (slot.Type == 3);
        }

        [CompilerGenerated]
        private static bool <IsHeroSoloParty>m__3A4(PartySlotData slot)
        {
            return (slot.Type == 0);
        }

        [CompilerGenerated]
        private static bool <IsHeroSoloParty>m__3A5(PartySlotData slot)
        {
            return (slot.Type == 2);
        }

        [CompilerGenerated]
        private static bool <IsHeroSoloParty>m__3A6(PartySlotData slot)
        {
            return ((slot.Type == 4) ? 1 : (slot.Type == 5));
        }

        [CompilerGenerated]
        private static void <OnForwardOrBackButtonClick>m__385(GameObject g)
        {
        }

        [CompilerGenerated]
        private static void <OnForwardOrBackButtonClick>m__387(GameObject g)
        {
        }

        [CompilerGenerated]
        private void <OnForwardOrBackButtonClick>m__388(GameObject dialog)
        {
            this.mMultiErrorMsg = null;
            return;
        }

        [CompilerGenerated]
        private static bool <OnForwardOrBackButtonClick>m__38A(PartySlotTypeUnitPair slot)
        {
            return ((slot.Type == 3) ? 1 : (slot.Type == 2));
        }

        [CompilerGenerated]
        private static string <OnForwardOrBackButtonClick>m__38B(PartySlotTypeUnitPair slot)
        {
            return slot.Unit;
        }

        [CompilerGenerated]
        private static void <OnForwardOrBackButtonClick>m__38D(GameObject dialog)
        {
        }

        [CompilerGenerated]
        private static void <OnForwardOrBackButtonClick>m__38E(GameObject dialog)
        {
        }

        [CompilerGenerated]
        private static void <OnForwardOrBackButtonClick>m__38F(GameObject dialog)
        {
        }

        [CompilerGenerated]
        private void <OnRaidAccept>m__384()
        {
            this.StartRaid();
            return;
        }

        [CompilerGenerated]
        private static void <OnRaidClick>m__383(GameObject go)
        {
        }

        [CompilerGenerated]
        private void <PrepareRaid>m__3AE(GameObject go)
        {
            this.OpenQuestDetail();
            return;
        }

        [CompilerGenerated]
        private static void <PrepareRaid>m__3AF(GameObject go)
        {
        }

        [CompilerGenerated]
        private static bool <RefreshUnitList>m__3AB(PartySlotTypeUnitPair slot)
        {
            return (slot.Type == 3);
        }

        [CompilerGenerated]
        private static string <RefreshUnitList>m__3AC(PartySlotTypeUnitPair slot)
        {
            return slot.Unit;
        }

        [CompilerGenerated]
        private static bool <RefreshUnits>m__37E(PartySlotTypeUnitPair slot)
        {
            return ((slot.Type == 3) ? 1 : (slot.Type == 2));
        }

        [CompilerGenerated]
        private static string <RefreshUnits>m__37F(PartySlotTypeUnitPair slot)
        {
            return slot.Unit;
        }

        [CompilerGenerated]
        private static void <SaveAndActivatePin>m__397()
        {
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.ILLEGAL_PARTY"), null, null, 0, -1);
            return;
        }

        [CompilerGenerated]
        private static bool <ValidateTeam>m__3A7(PartySlotData slot)
        {
            return ((slot.Type == 2) ? 1 : (slot.Type == 3));
        }

        [CompilerGenerated]
        private static string <ValidateTeam>m__3A8(PartySlotData slot)
        {
            return slot.UnitName;
        }

        [CompilerGenerated]
        private static bool <ValidateTeam>m__3A9(PartySlotData slot)
        {
            return (slot.Type == 3);
        }

        [CompilerGenerated]
        private static string <ValidateTeam>m__3AA(PartySlotData slot)
        {
            return slot.UnitName;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 40)
            {
                goto Label_0035;
            }
            if (num == 0x29)
            {
                goto Label_007C;
            }
            if (num == 50)
            {
                goto Label_00B0;
            }
            if (num == 130)
            {
                goto Label_00BB;
            }
            if (num == 140)
            {
                goto Label_00C7;
            }
            goto Label_00D3;
        Label_0035:
            if (string.IsNullOrEmpty(GlobalVars.TeamName) != null)
            {
                goto Label_00DF;
            }
            this.mCurrentParty.Name = GlobalVars.TeamName;
            this.SaveTeamPresets();
            this.ResetTeamPulldown(this.mTeams, SRPG_Extensions.GetMaxTeamCount(this.mCurrentPartyType), this.mCurrentQuest);
            goto Label_00DF;
        Label_007C:
            if (GlobalVars.RecommendTeamSettingValue == null)
            {
                goto Label_00DF;
            }
            this.SaveRecommendedTeamSetting(GlobalVars.RecommendTeamSettingValue);
            this.OrganizeRecommendedParty(GlobalVars.RecommendTeamSettingValue.recommendedType, GlobalVars.RecommendTeamSettingValue.recommendedElement);
            goto Label_00DF;
        Label_00B0:
            this.RefreshQuest();
            goto Label_00DF;
        Label_00BB:
            this.Refresh(0);
            goto Label_00DF;
        Label_00C7:
            this.Refresh(1);
            goto Label_00DF;
        Label_00D3:
            this.UnitList_Activated(pinID);
        Label_00DF:
            return;
        }

        private void AssignUnits(PartyEditData partyEditData)
        {
            UnitData[] dataArray;
            UnitData[] dataArray2;
            string[] strArray;
            int num;
            int num2;
            PartySlotData data;
            UnitData data2;
            int num3;
            UnitData data3;
            int num4;
            dataArray = new UnitData[this.mSlotData.Count];
            dataArray2 = Enumerable.ToArray<UnitData>(partyEditData.Units);
            if (<>f__am$cache90 != null)
            {
                goto Label_003B;
            }
            <>f__am$cache90 = new Func<PartySlotData, bool>(PartyWindow2.<AssignUnits>m__399);
        Label_003B:
            if (<>f__am$cache91 != null)
            {
                goto Label_005D;
            }
            <>f__am$cache91 = new Func<PartySlotData, string>(PartyWindow2.<AssignUnits>m__39A);
        Label_005D:
            strArray = Enumerable.ToArray<string>(Enumerable.Select<PartySlotData, string>(Enumerable.Where<PartySlotData>(this.mSlotData, <>f__am$cache90), <>f__am$cache91));
            num = 0;
            num2 = 0;
            goto Label_01BB;
        Label_0077:
            data = this.mSlotData[num2];
            if (data.Type != 3)
            {
                goto Label_00C9;
            }
            data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(data.UnitName);
            if (data2 == null)
            {
                goto Label_00BF;
            }
            this.mGuestUnit.Add(data2);
        Label_00BF:
            dataArray[num2] = null;
            goto Label_01B5;
        Label_00C9:
            if (data.Type != 2)
            {
                goto Label_00F5;
            }
            dataArray[num2] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(data.UnitName);
            goto Label_01B5;
        Label_00F5:
            if (data.Type == 4)
            {
                goto Label_0128;
            }
            if (data.Type == 5)
            {
                goto Label_0128;
            }
            if (data.Type != 1)
            {
                goto Label_0132;
            }
            if (data.IsSettable != null)
            {
                goto Label_0132;
            }
        Label_0128:
            dataArray[num2] = null;
            goto Label_01B5;
        Label_0132:
            if (num2 >= this.mCurrentParty.PartyData.MAX_MAINMEMBER)
            {
                goto Label_0160;
            }
            num3 = this.mCurrentParty.PartyData.MAX_MAINMEMBER;
            goto Label_0172;
        Label_0160:
            num3 = this.mCurrentParty.PartyData.MAX_UNIT;
        Label_0172:
            goto Label_01A4;
        Label_0177:
            data3 = dataArray2[num++];
            if (data3 == null)
            {
                goto Label_01A4;
            }
            if (Enumerable.Contains<string>(strArray, data3.UnitID) != null)
            {
                goto Label_01A4;
            }
            dataArray[num2] = data3;
            goto Label_01B5;
        Label_01A4:
            if (num >= num3)
            {
                goto Label_01B5;
            }
            if (num < ((int) dataArray2.Length))
            {
                goto Label_0177;
            }
        Label_01B5:
            num2 += 1;
        Label_01BB:
            if (num2 >= ((int) dataArray.Length))
            {
                goto Label_01D7;
            }
            if (num2 < this.mSlotData.Count)
            {
                goto Label_0077;
            }
        Label_01D7:
            num4 = 0;
            goto Label_01F2;
        Label_01DF:
            partyEditData.Units[num4] = dataArray[num4];
            num4 += 1;
        Label_01F2:
            if (num4 >= ((int) partyEditData.Units.Length))
            {
                goto Label_020B;
            }
            if (num4 < ((int) dataArray.Length))
            {
                goto Label_01DF;
            }
        Label_020B:
            this.ValidateTeam(partyEditData);
            return;
        }

        private void AttachAndEnable(Transform go, Transform parent, string subPath)
        {
            Transform transform;
            if (string.IsNullOrEmpty(subPath) != null)
            {
                goto Label_0022;
            }
            transform = parent.FindChild(subPath);
            if ((transform != null) == null)
            {
                goto Label_0022;
            }
            parent = transform;
        Label_0022:
            go.SetParent(parent, 0);
            go.get_gameObject().SetActive(1);
            return;
        }

        public void BreakupTeam()
        {
            this.BreakupTeamImpl();
            this.OnPartyMemberChange();
            this.SaveTeamPresets();
            return;
        }

        private void BreakupTeamImpl()
        {
            int num;
            int num2;
            PartySlotData data;
            int num3;
            num = 0;
            num2 = 0;
            goto Label_005C;
        Label_0009:
            data = this.mSlotData[num2];
            if (data.Type == null)
            {
                goto Label_0051;
            }
            if (data.Type == 2)
            {
                goto Label_0051;
            }
            if (data.Type == 3)
            {
                goto Label_0051;
            }
            if (data.Type == 4)
            {
                goto Label_0051;
            }
            if (data.Type != 5)
            {
                goto Label_0058;
            }
        Label_0051:
            num = num2;
            goto Label_006D;
        Label_0058:
            num2 += 1;
        Label_005C:
            if (num2 < this.mSlotData.Count)
            {
                goto Label_0009;
            }
        Label_006D:
            num3 = 0;
            goto Label_0087;
        Label_0074:
            if (num3 == num)
            {
                goto Label_0083;
            }
            this.SetPartyUnitForce(num3, null);
        Label_0083:
            num3 += 1;
        Label_0087:
            if (num3 < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_0074;
            }
            this.SetSupport(null);
            return;
        }

        private int CalculateTotalSupportCost(SupportData support)
        {
            int num;
            int num2;
            if (this.mCurrentPartyType != 10)
            {
                goto Label_0063;
            }
            num = support.GetCost();
            num2 = 0;
            goto Label_0050;
        Label_001B:
            if (num2 == this.mCurrentTeamIndex)
            {
                goto Label_004C;
            }
            if (this.mSupports[num2] == null)
            {
                goto Label_004C;
            }
            num += this.mSupports[num2].GetCost();
        Label_004C:
            num2 += 1;
        Label_0050:
            if (num2 < this.mSupports.Count)
            {
                goto Label_001B;
            }
            return num;
        Label_0063:
            return support.GetCost();
        }

        private void ChangeEnabledArrowButtons(int index, int max)
        {
            if ((this.NextButton != null) == null)
            {
                goto Label_0022;
            }
            this.NextButton.set_interactable(index < (max - 1));
        Label_0022:
            if ((this.PrevButton != null) == null)
            {
                goto Label_0042;
            }
            this.PrevButton.set_interactable(index > 0);
        Label_0042:
            return;
        }

        private void ChangeEnabledTeamTabs(int index)
        {
            int num;
            if (this.TeamTabs != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_0028;
        Label_0013:
            this.TeamTabs[num].set_isOn(num == index);
            num += 1;
        Label_0028:
            if (num < ((int) this.TeamTabs.Length))
            {
                goto Label_0013;
            }
            return;
        }

        protected virtual unsafe bool CheckMember(int numMainUnits)
        {
            string str;
            if (numMainUnits > 0)
            {
                goto Label_003A;
            }
            if (<>f__am$cache8D != null)
            {
                goto Label_002A;
            }
            <>f__am$cache8D = new UIUtility.DialogResultEvent(PartyWindow2.<CheckMember>m__394);
        Label_002A:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), <>f__am$cache8D, null, 0, -1);
            return 0;
        Label_003A:
            if (this.mCurrentQuest == null)
            {
                goto Label_00A7;
            }
            if (this.mCurrentQuest.IsMultiTower != null)
            {
                goto Label_00A7;
            }
            str = string.Empty;
            if (this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units, &str) != null)
            {
                goto Label_00A7;
            }
            if (<>f__am$cache8E != null)
            {
                goto Label_0097;
            }
            <>f__am$cache8E = new UIUtility.DialogResultEvent(PartyWindow2.<CheckMember>m__395);
        Label_0097:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get(str), <>f__am$cache8E, null, 0, -1);
            return 0;
        Label_00A7:
            return 1;
        }

        private static int CompareTo_Attack(UnitData unit1, UnitData unit2)
        {
            return (unit2.Status.param.atk - unit1.Status.param.atk);
        }

        private static int CompareTo_AttackType(UnitData unit1, UnitData unit2, AttackDetailTypes type)
        {
            AttackDetailTypes types;
            AttackDetailTypes types2;
            types = unit1.GetAttackSkill().AttackDetailType;
            types2 = unit2.GetAttackSkill().AttackDetailType;
            if (types != type)
            {
                goto Label_0028;
            }
            if (types2 != type)
            {
                goto Label_0028;
            }
            return 0;
        Label_0028:
            if (types != type)
            {
                goto Label_0038;
            }
            if (types2 == type)
            {
                goto Label_0038;
            }
            return -1;
        Label_0038:
            if (types == type)
            {
                goto Label_0048;
            }
            if (types2 != type)
            {
                goto Label_0048;
            }
            return 1;
        Label_0048:
            return 0;
        }

        private static int CompareTo_AttackTypeBlow(UnitData unit1, UnitData unit2)
        {
            return CompareTo_AttackType(unit1, unit2, 3);
        }

        private static int CompareTo_AttackTypeMagic(UnitData unit1, UnitData unit2)
        {
            return CompareTo_AttackType(unit1, unit2, 5);
        }

        private static int CompareTo_AttackTypeNone(UnitData unit1, UnitData unit2)
        {
            return CompareTo_AttackType(unit1, unit2, 0);
        }

        private static int CompareTo_AttackTypeShot(UnitData unit1, UnitData unit2)
        {
            return CompareTo_AttackType(unit1, unit2, 4);
        }

        private static int CompareTo_AttackTypeSlash(UnitData unit1, UnitData unit2)
        {
            return CompareTo_AttackType(unit1, unit2, 1);
        }

        private static int CompareTo_AttackTypeStab(UnitData unit1, UnitData unit2)
        {
            return CompareTo_AttackType(unit1, unit2, 2);
        }

        private static int CompareTo_Defense(UnitData unit1, UnitData unit2)
        {
            return (unit2.Status.param.def - unit1.Status.param.def);
        }

        private static int CompareTo_HP(UnitData unit1, UnitData unit2)
        {
            return (unit2.Status.param.hp - unit1.Status.param.hp);
        }

        private static int CompareTo_Magic(UnitData unit1, UnitData unit2)
        {
            return (unit2.Status.param.mag - unit1.Status.param.mag);
        }

        private static int CompareTo_Mind(UnitData unit1, UnitData unit2)
        {
            return (unit2.Status.param.mnd - unit1.Status.param.mnd);
        }

        private static int CompareTo_Speed(UnitData unit1, UnitData unit2)
        {
            return (unit2.Status.param.spd - unit1.Status.param.spd);
        }

        private static int CompareTo_Total(UnitData unit1, UnitData unit2)
        {
            int num;
            int num2;
            num = 0;
            num += unit1.Status.param.atk;
            num += unit1.Status.param.def;
            num += unit1.Status.param.mag;
            num += unit1.Status.param.mnd;
            num += unit1.Status.param.spd;
            num += unit1.Status.param.dex;
            num += unit1.Status.param.cri;
            num += unit1.Status.param.luk;
            num2 = 0;
            num2 += unit2.Status.param.atk;
            num2 += unit2.Status.param.def;
            num2 += unit2.Status.param.mag;
            num2 += unit2.Status.param.mnd;
            num2 += unit2.Status.param.spd;
            num2 += unit2.Status.param.dex;
            num2 += unit2.Status.param.cri;
            num2 += unit2.Status.param.luk;
            return (num2 - num);
        }

        private bool ContainsForcedHero()
        {
            if (<>f__am$cache99 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache99 = new Func<PartySlotData, bool>(PartyWindow2.<ContainsForcedHero>m__3A2);
        Label_001E:
            return Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache99);
        }

        private bool ContainsNotFree()
        {
            if (<>f__am$cache93 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache93 = new Func<PartySlotData, bool>(PartyWindow2.<ContainsNotFree>m__39C);
        Label_001E:
            return Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache93);
        }

        private bool ContainsNpc()
        {
            if (<>f__am$cache98 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache98 = new Func<PartySlotData, bool>(PartyWindow2.<ContainsNpc>m__3A1);
        Label_001E:
            return Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache98);
        }

        private bool ContainsNpcOrForced()
        {
            if (<>f__am$cache95 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache95 = new Func<PartySlotData, bool>(PartyWindow2.<ContainsNpcOrForced>m__39E);
        Label_001E:
            return Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache95);
        }

        private bool ContainsNpcOrForcedHero()
        {
            if (<>f__am$cache97 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache97 = new Func<PartySlotData, bool>(PartyWindow2.<ContainsNpcOrForcedHero>m__3A0);
        Label_001E:
            return Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache97);
        }

        private bool ContainsNpcOrForcedOrForcedHero()
        {
            if (<>f__am$cache94 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache94 = new Func<PartySlotData, bool>(PartyWindow2.<ContainsNpcOrForcedOrForcedHero>m__39D);
        Label_001E:
            return Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache94);
        }

        private bool ContainsNpcOrForcedOrLocked()
        {
            if (<>f__am$cache96 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache96 = new Func<PartySlotData, bool>(PartyWindow2.<ContainsNpcOrForcedOrLocked>m__39F);
        Label_001E:
            return Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache96);
        }

        private ListItemEvents CreateItemListItem()
        {
            ListItemEvents events;
            events = Object.Instantiate<ListItemEvents>(this.ItemListItem);
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            return events;
        }

        private GenericSlot CreateSlotObject(PartySlotData slotData, GenericSlot template, Transform parent)
        {
            GenericSlot slot;
            slot = Object.Instantiate<GameObject>(template.get_gameObject()).GetComponent<GenericSlot>();
            slot.get_transform().SetParent(parent, 0);
            slot.get_gameObject().SetActive(1);
            slot.SetSlotData<PartySlotData>(slotData);
            return slot;
        }

        private unsafe void CreateSlots()
        {
            List<PartySlotData> list;
            List<PartySlotData> list2;
            PartySlotData data;
            QuestPartyParam param;
            string str;
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param2;
            int num;
            int num2;
            Func<string> func;
            string str2;
            List<GenericSlot> list3;
            PartySlotData data2;
            List<PartySlotData>.Enumerator enumerator;
            GenericSlot slot;
            PartySlotData data3;
            List<PartySlotData>.Enumerator enumerator2;
            GenericSlot slot2;
            int num3;
            TowerFloorParam param3;
            <CreateSlots>c__AnonStorey373 storey;
            <CreateSlots>c__AnonStorey372 storey2;
            GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
            GameUtility.DestroyGameObject(this.FriendSlot);
            this.mSlotData.Clear();
            list = new List<PartySlotData>();
            list2 = new List<PartySlotData>();
            data = null;
            if (this.mCurrentQuest != null)
            {
                goto Label_00A3;
            }
            list.Add(new PartySlotData(0, null, 0, 0));
            list.Add(new PartySlotData(0, null, 1, 0));
            list.Add(new PartySlotData(0, null, 2, 0));
            list.Add(new PartySlotData(0, null, 3, 0));
            data = new PartySlotData(1, null, 7, 0);
            list2.Add(new PartySlotData(0, null, 5, 0));
            list2.Add(new PartySlotData(0, null, 6, 0));
            goto Label_06D1;
        Label_00A3:
            if (this.mCurrentQuest.questParty == null)
            {
                goto Label_0169;
            }
            param = this.mCurrentQuest.questParty;
            list.Add(new PartySlotData(param.type_1, param.unit_1, 0, 0));
            list.Add(new PartySlotData(param.type_2, param.unit_2, 1, 0));
            list.Add(new PartySlotData(param.type_3, param.unit_3, 2, 0));
            list.Add(new PartySlotData(param.type_4, param.unit_4, 3, 0));
            data = new PartySlotData(param.support_type, null, 7, 0);
            list2.Add(new PartySlotData(param.subtype_1, param.subunit_1, 5, 0));
            list2.Add(new PartySlotData(param.subtype_2, param.subunit_2, 6, 0));
            goto Label_06D1;
        Label_0169:
            str = ((this.mCurrentQuest.units == null) || (this.mCurrentQuest.units.Length <= 0)) ? null : this.mCurrentQuest.units.GetList()[0];
            if (this.GetPartyCondType() == 2)
            {
                goto Label_04FC;
            }
            if (this.mCurrentQuest.type != 7)
            {
                goto Label_023E;
            }
            list.Add(new PartySlotData(0, null, 0, 0));
            list.Add(new PartySlotData(0, null, 1, 0));
            list.Add(new PartySlotData(0, null, 2, 0));
            list.Add(new PartySlotData(0, null, 3, 0));
            list.Add(new PartySlotData(0, null, 4, 0));
            data = new PartySlotData(0, null, 7, 0);
            list2.Add(new PartySlotData(0, null, 5, 0));
            list2.Add(new PartySlotData(0, null, 6, 0));
            goto Label_04F7;
        Label_023E:
            if (this.mCurrentQuest.type != 15)
            {
                goto Label_029B;
            }
            list.Add(new PartySlotData(0, null, 0, 0));
            list.Add(new PartySlotData(0, null, 1, 0));
            list.Add(new PartySlotData(0, null, 2, 0));
            list.Add(new PartySlotData(0, null, 3, 0));
            data = new PartySlotData(0, null, 7, 0);
            goto Label_04F7;
        Label_029B:
            if (((this.mCurrentQuest.type != 8) && (this.mCurrentQuest.type != 0x10)) && (this.mCurrentQuest.type != 9))
            {
                goto Label_0320;
            }
            list.Add(new PartySlotData(0, null, 0, 0));
            list.Add(new PartySlotData(0, null, 1, 0));
            list.Add(new PartySlotData(0, null, 2, 0));
            list.Add(new PartySlotData(1, null, 3, 0));
            list.Add(new PartySlotData(1, null, 4, 0));
            goto Label_04F7;
        Label_0320:
            if (this.mCurrentQuest.type != 2)
            {
                goto Label_039A;
            }
            list.Add(new PartySlotData(0, null, 0, 0));
            list.Add(new PartySlotData(0, null, 1, 0));
            list.Add(new PartySlotData(0, null, 2, 0));
            list.Add(new PartySlotData(1, null, 3, 0));
            data = new PartySlotData(1, null, 7, 0);
            list2.Add(new PartySlotData(1, null, 5, 0));
            list2.Add(new PartySlotData(1, null, 6, 0));
            goto Label_04F7;
        Label_039A:
            if (this.mCurrentQuest.type != 1)
            {
                goto Label_0472;
            }
            room = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
            param2 = ((room != null) && (string.IsNullOrEmpty(room.json) == null)) ? JSON_MyPhotonRoomParam.Parse(room.json) : null;
            if ((GlobalVars.SelectedMultiPlayRoomType != null) || (param2 == null))
            {
                goto Label_0445;
            }
            num = param2.GetUnitSlotNum();
            num2 = 0;
            goto Label_043D;
        Label_0409:
            if (num2 >= num)
            {
                goto Label_0427;
            }
            list.Add(new PartySlotData(0, null, num2, 0));
            goto Label_0437;
        Label_0427:
            list.Add(new PartySlotData(1, null, num2, 1));
        Label_0437:
            num2 += 1;
        Label_043D:
            if (num2 < 4)
            {
                goto Label_0409;
            }
        Label_0445:
            data = new PartySlotData(1, null, 7, 1);
            list2.Add(new PartySlotData(1, null, 5, 1));
            list2.Add(new PartySlotData(1, null, 6, 1));
            goto Label_04F7;
        Label_0472:
            list.Add(new PartySlotData(0, null, 0, 0));
            list.Add(new PartySlotData(0, null, 1, 0));
            list.Add(new PartySlotData(0, null, 2, 0));
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_04BF;
            }
            list.Add(new PartySlotData(0, null, 3, 0));
            goto Label_04CF;
        Label_04BF:
            list.Add(new PartySlotData(3, str, 3, 0));
        Label_04CF:
            data = new PartySlotData(0, null, 7, 0);
            list2.Add(new PartySlotData(0, null, 5, 0));
            list2.Add(new PartySlotData(0, null, 6, 0));
        Label_04F7:
            goto Label_06D1;
        Label_04FC:
            storey = new <CreateSlots>c__AnonStorey373();
            storey.cond = this.GetPartyCondition();
            if (((int) storey.cond.unit.Length) <= 1)
            {
                goto Label_0631;
            }
            storey2 = new <CreateSlots>c__AnonStorey372();
            storey2.<>f__ref$883 = storey;
            storey2.index = 0;
            func = new Func<string>(storey2.<>m__398);
            str2 = func();
            list.Add(new PartySlotData((str2 != null) ? 2 : 1, str2, 0, 0));
            str2 = func();
            list.Add(new PartySlotData((str2 != null) ? 2 : 1, str2, 1, 0));
            str2 = func();
            list.Add(new PartySlotData((str2 != null) ? 2 : 1, str2, 2, 0));
            str2 = func();
            list.Add(new PartySlotData(3, str, 3, 0));
            data = new PartySlotData(0, null, 7, 0);
            str2 = func();
            list2.Add(new PartySlotData((str2 != null) ? 2 : 1, str2, 5, 0));
            str2 = func();
            list2.Add(new PartySlotData((str2 != null) ? 2 : 1, str2, 6, 0));
            goto Label_06D1;
        Label_0631:
            if ((storey.cond.unit[0] == str) == null)
            {
                goto Label_0660;
            }
            list.Add(new PartySlotData(3, str, 0, 0));
            goto Label_067C;
        Label_0660:
            list.Add(new PartySlotData(2, storey.cond.unit[0], 0, 0));
        Label_067C:
            list.Add(new PartySlotData(1, null, 1, 0));
            list.Add(new PartySlotData(1, null, 2, 0));
            list.Add(new PartySlotData(1, null, 3, 0));
            data = new PartySlotData(0, null, 7, 0);
            list2.Add(new PartySlotData(1, null, 5, 0));
            list2.Add(new PartySlotData(1, null, 6, 0));
        Label_06D1:
            list3 = new List<GenericSlot>();
            if ((this.MainMemberHolder != null) == null)
            {
                goto Label_079C;
            }
            if (list.Count <= 0)
            {
                goto Label_079C;
            }
            enumerator = list.GetEnumerator();
        Label_06FD:
            try
            {
                goto Label_075F;
            Label_0702:
                data2 = &enumerator.Current;
                if (data2.Type == 4)
                {
                    goto Label_0725;
                }
                if (data2.Type != 5)
                {
                    goto Label_0740;
                }
            Label_0725:
                slot = this.CreateSlotObject(data2, this.NpcSlotTemplate, this.MainMemberHolder);
                goto Label_0756;
            Label_0740:
                slot = this.CreateSlotObject(data2, this.UnitSlotTemplate, this.MainMemberHolder);
            Label_0756:
                list3.Add(slot);
            Label_075F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0702;
                }
                goto Label_077D;
            }
            finally
            {
            Label_0770:
                ((List<PartySlotData>.Enumerator) enumerator).Dispose();
            }
        Label_077D:
            if (data == null)
            {
                goto Label_079C;
            }
            this.FriendSlot = this.CreateSlotObject(data, this.UnitSlotTemplate, this.MainMemberHolder);
        Label_079C:
            if ((this.SubMemberHolder != null) == null)
            {
                goto Label_0841;
            }
            if (list2.Count <= 0)
            {
                goto Label_0841;
            }
            enumerator2 = list2.GetEnumerator();
        Label_07C1:
            try
            {
                goto Label_0823;
            Label_07C6:
                data3 = &enumerator2.Current;
                if (data3.Type == 4)
                {
                    goto Label_07E9;
                }
                if (data3.Type != 5)
                {
                    goto Label_0804;
                }
            Label_07E9:
                slot2 = this.CreateSlotObject(data3, this.NpcSlotTemplate, this.SubMemberHolder);
                goto Label_081A;
            Label_0804:
                slot2 = this.CreateSlotObject(data3, this.UnitSlotTemplate, this.SubMemberHolder);
            Label_081A:
                list3.Add(slot2);
            Label_0823:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_07C6;
                }
                goto Label_0841;
            }
            finally
            {
            Label_0834:
                ((List<PartySlotData>.Enumerator) enumerator2).Dispose();
            }
        Label_0841:
            this.mSlotData.AddRange(list);
            this.mSlotData.AddRange(list2);
            this.mSupportSlotData = data;
            this.UnitSlots = list3.ToArray();
            num3 = 0;
            goto Label_08C0;
        Label_0875:
            if ((this.UnitSlots[num3] != null) == null)
            {
                goto Label_08BA;
            }
            if (this.mSlotData[num3].Type != null)
            {
                goto Label_08BA;
            }
            this.UnitSlots[num3].OnSelect = new GenericSlot.SelectEvent(this.OnUnitSlotClick);
        Label_08BA:
            num3 += 1;
        Label_08C0:
            if (num3 >= ((int) this.UnitSlots.Length))
            {
                goto Label_08E1;
            }
            if (num3 < this.mSlotData.Count)
            {
                goto Label_0875;
            }
        Label_08E1:
            if ((this.FriendSlot != null) == null)
            {
                goto Label_098C;
            }
            if (this.mSupportSlotData == null)
            {
                goto Label_0924;
            }
            if (this.mSupportSlotData.Type != null)
            {
                goto Label_0924;
            }
            this.FriendSlot.OnSelect = new GenericSlot.SelectEvent(this.OnSupportUnitSlotClick);
        Label_0924:
            if (this.mCurrentQuest == null)
            {
                goto Label_098C;
            }
            if (this.mCurrentQuest.type != 7)
            {
                goto Label_098C;
            }
            param3 = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
            if (param3 == null)
            {
                goto Label_098C;
            }
            this.FriendSlot.get_gameObject().SetActive(param3.can_help);
            this.SupportSkill.get_gameObject().SetActive(param3.can_help);
        Label_098C:
            return;
        }

        private unsafe T GetComponents<T>(GameObject root, string targetName, bool includeInactive) where T: Component
        {
            T[] localArray;
            T local;
            T[] localArray2;
            int num;
            localArray2 = root.GetComponentsInChildren<T>(includeInactive);
            num = 0;
            goto Label_0037;
        Label_0011:
            local = localArray2[num];
            if ((&local.get_name() == targetName) == null)
            {
                goto Label_0033;
            }
            return local;
        Label_0033:
            num += 1;
        Label_0037:
            if (num < ((int) localArray2.Length))
            {
                goto Label_0011;
            }
            return (T) null;
        }

        private UnitData[] GetDefaultTeam()
        {
            UnitData[] dataArray;
            PlayerData data;
            int num;
            int num2;
            dataArray = new UnitData[this.mCurrentParty.PartyData.MAX_UNIT];
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            num2 = 0;
            goto Label_005C;
        Label_002A:
            if (data.Units[num2].UnitParam.IsHero() != null)
            {
                goto Label_0058;
            }
            dataArray[num] = data.Units[num2];
            num += 1;
        Label_0058:
            num2 += 1;
        Label_005C:
            if (num2 >= data.Units.Count)
            {
                goto Label_0076;
            }
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_002A;
            }
        Label_0076:
            return dataArray;
        }

        private QuestCondParam GetPartyCondition()
        {
            if (this.mCurrentQuest == null)
            {
                goto Label_0034;
            }
            if (this.mCurrentQuest.type != 6)
            {
                goto Label_0028;
            }
            return this.mCurrentQuest.EntryConditionCh;
        Label_0028:
            return this.mCurrentQuest.EntryCondition;
        Label_0034:
            return null;
        }

        private PartyCondType GetPartyCondType()
        {
            if (this.mCurrentQuest == null)
            {
                goto Label_005E;
            }
            if (this.mCurrentQuest.type != 6)
            {
                goto Label_003D;
            }
            if (this.mCurrentQuest.EntryConditionCh == null)
            {
                goto Label_003D;
            }
            return this.mCurrentQuest.EntryConditionCh.party_type;
        Label_003D:
            if (this.mCurrentQuest.EntryCondition == null)
            {
                goto Label_005E;
            }
            return this.mCurrentQuest.EntryCondition.party_type;
        Label_005E:
            return 0;
        }

        public void GoToUnitList()
        {
            int num;
            if (this.PartyType != 9)
            {
                goto Label_002C;
            }
            num = GlobalVars.SelectedTowerMultiPartyIndex;
            if (this.IsMultiTowerPartySlot(num) == null)
            {
                goto Label_002C;
            }
            this.mSelectedSlotIndex = num;
            this.UnitList_Show();
        Label_002C:
            return;
        }

        private void HardQuestDropPiecesUpdate()
        {
            GameObject obj2;
            obj2 = GameObjectID.FindGameObject("WorldMapQuestList");
            if ((obj2 != null) == null)
            {
                goto Label_001D;
            }
            GameParameter.UpdateAll(obj2);
        Label_001D:
            return;
        }

        private bool IsFixedParty()
        {
            if (<>f__am$cache92 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache92 = new Func<PartySlotData, bool>(PartyWindow2.<IsFixedParty>m__39B);
        Label_001E:
            return (Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache92) == 0);
        }

        private bool IsHeroSoloParty()
        {
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            if (<>f__am$cache9A != null)
            {
                goto Label_001E;
            }
            <>f__am$cache9A = new Func<PartySlotData, bool>(PartyWindow2.<IsHeroSoloParty>m__3A3);
        Label_001E:
            flag = Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache9A);
            if (<>f__am$cache9B != null)
            {
                goto Label_0047;
            }
            <>f__am$cache9B = new Func<PartySlotData, bool>(PartyWindow2.<IsHeroSoloParty>m__3A4);
        Label_0047:
            flag2 = Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache9B) == 0;
            if (<>f__am$cache9C != null)
            {
                goto Label_0073;
            }
            <>f__am$cache9C = new Func<PartySlotData, bool>(PartyWindow2.<IsHeroSoloParty>m__3A5);
        Label_0073:
            flag3 = Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache9C) == 0;
            if (<>f__am$cache9D != null)
            {
                goto Label_009F;
            }
            <>f__am$cache9D = new Func<PartySlotData, bool>(PartyWindow2.<IsHeroSoloParty>m__3A6);
        Label_009F:
            flag4 = Enumerable.Any<PartySlotData>(this.mSlotData, <>f__am$cache9D) == 0;
            return ((((flag == null) || (flag2 == null)) || (flag3 == null)) ? 0 : flag4);
        }

        private bool IsMultiTowerPartySlot(int index)
        {
            if (this.mCurrentPartyType != 9)
            {
                goto Label_0023;
            }
            if (index == null)
            {
                goto Label_0021;
            }
            if (index == 1)
            {
                goto Label_0021;
            }
            if (index != 2)
            {
                goto Label_0023;
            }
        Label_0021:
            return 1;
        Label_0023:
            return 0;
        }

        private bool IsSettableSlot(PartySlotData slotData)
        {
            return ((slotData.Type == null) ? 1 : ((slotData.Type != 1) ? 0 : slotData.IsSettable));
        }

        private void JumpToBefore(GameObject dialog)
        {
            BackHandler.Invoke();
            return;
        }

        protected virtual void LoadInventory()
        {
            PlayerData data;
            int num;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_0025;
        Label_0012:
            this.SetItemSlot(num, data.Inventory[num]);
            num += 1;
        Label_0025:
            if (num >= ((int) this.mCurrentItems.Length))
            {
                goto Label_0041;
            }
            if (num < ((int) data.Inventory.Length))
            {
                goto Label_0012;
            }
        Label_0041:
            this.OnItemSlotsChange();
            return;
        }

        public void LoadRecommendedTeamSetting()
        {
            GlobalVars.RecommendTeamSetting setting;
            string str;
            Exception exception;
            setting = null;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY) == null)
            {
                goto Label_0039;
            }
            str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY, string.Empty);
        Label_0021:
            try
            {
                setting = JsonUtility.FromJson<GlobalVars.RecommendTeamSetting>(str);
                goto Label_0039;
            }
            catch (Exception exception1)
            {
            Label_002D:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0039;
            }
        Label_0039:
            GlobalVars.RecommendTeamSettingValue = setting;
            return;
        }

        private unsafe void LoadTeam()
        {
            PlayerPartyTypes types;
            PartyData data;
            UnitData[] dataArray;
            int num;
            UnitData data2;
            PartyEditData data3;
            bool flag;
            int num2;
            List<PartyEditData> list;
            PartyData data4;
            int num3;
            int num4;
            if (this.mCurrentPartyType != null)
            {
                goto Label_0011;
            }
            throw new InvalidPartyTypeException();
        Label_0011:
            this.mGuestUnit.Clear();
            this.mMaxTeamCount = SRPG_Extensions.GetMaxTeamCount(this.mCurrentPartyType);
            types = SRPG_Extensions.ToPlayerPartyType(this.mCurrentPartyType);
            this.mTeams.Clear();
            if (this.GetPartyCondType() == 2)
            {
                goto Label_005B;
            }
            if (this.IsFixedParty() == null)
            {
                goto Label_0191;
            }
        Label_005B:
            data = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(types);
            dataArray = new UnitData[this.mSlotData.Count];
            num = 0;
            goto Label_0140;
        Label_0084:
            data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.mSlotData[num].UnitName);
            if (this.mSlotData[num].Type != 3)
            {
                goto Label_00D3;
            }
            this.mGuestUnit.Add(data2);
            dataArray[num] = null;
            goto Label_013C;
        Label_00D3:
            if (this.mSlotData[num].Type == 4)
            {
                goto Label_012E;
            }
            if (this.mSlotData[num].Type == 5)
            {
                goto Label_012E;
            }
            if (this.mSlotData[num].Type != 1)
            {
                goto Label_0137;
            }
            if (this.mSlotData[num].IsSettable != null)
            {
                goto Label_0137;
            }
        Label_012E:
            dataArray[num] = null;
            goto Label_013C;
        Label_0137:
            dataArray[num] = data2;
        Label_013C:
            num += 1;
        Label_0140:
            if (num < this.mSlotData.Count)
            {
                goto Label_0084;
            }
            data3 = new PartyEditData(string.Empty, data);
            data3.SetUnitsForce(dataArray);
            this.mTeams.Add(data3);
            this.mCurrentParty = this.mTeams[0];
            this.mCurrentTeamIndex = 0;
            goto Label_03B4;
        Label_0191:
            if (this.mCurrentPartyType == 4)
            {
                goto Label_0233;
            }
            if (this.mCurrentPartyType == 5)
            {
                goto Label_0233;
            }
            if (this.mCurrentPartyType == 3)
            {
                goto Label_0233;
            }
            flag = this.ContainsNotFree();
            list = PartyUtility.LoadTeamPresets(this.mCurrentPartyType, &num2, flag);
            if (this.mCurrentQuest == null)
            {
                goto Label_01F8;
            }
            if (this.mCurrentQuest.type != 5)
            {
                goto Label_01F8;
            }
            if (flag == null)
            {
                goto Label_01F8;
            }
            this.mMaxTeamCount = 1;
        Label_01F8:
            if (list == null)
            {
                goto Label_0233;
            }
            this.mTeams = list;
            this.mCurrentTeamIndex = num2;
            if (this.mCurrentTeamIndex < 0)
            {
                goto Label_022C;
            }
            if (this.mMaxTeamCount > this.mCurrentTeamIndex)
            {
                goto Label_0233;
            }
        Label_022C:
            this.mCurrentTeamIndex = 0;
        Label_0233:
            if (this.mTeams.Count <= this.mMaxTeamCount)
            {
                goto Label_026A;
            }
            this.mTeams = Enumerable.ToList<PartyEditData>(Enumerable.Take<PartyEditData>(this.mTeams, this.mMaxTeamCount));
            goto Label_031B;
        Label_026A:
            if (this.mTeams.Count >= this.mMaxTeamCount)
            {
                goto Label_031B;
            }
            data4 = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(types);
            if (types != 9)
            {
                goto Label_02DD;
            }
            num3 = this.mTeams.Count;
            goto Label_02CB;
        Label_02AC:
            this.mTeams.Add(new PartyEditData(PartyUtility.CreateOrdealPartyNameFromIndex(num3), data4));
            num3 += 1;
        Label_02CB:
            if (num3 < this.mMaxTeamCount)
            {
                goto Label_02AC;
            }
            goto Label_031B;
        Label_02DD:
            num4 = this.mTeams.Count;
            goto Label_030E;
        Label_02EF:
            this.mTeams.Add(new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(num4), data4));
            num4 += 1;
        Label_030E:
            if (num4 < this.mMaxTeamCount)
            {
                goto Label_02EF;
            }
        Label_031B:
            if (this.mCurrentTeamIndex < 0)
            {
                goto Label_033D;
            }
            if (this.mCurrentTeamIndex < this.mTeams.Count)
            {
                goto Label_0344;
            }
        Label_033D:
            this.mCurrentTeamIndex = 0;
        Label_0344:
            if (this.EnableTeamAssign == null)
            {
                goto Label_0391;
            }
            if (GlobalVars.SelectedTeamIndex >= 0)
            {
                goto Label_036A;
            }
            if (GlobalVars.SelectedTeamIndex >= this.mMaxTeamCount)
            {
                goto Label_0391;
            }
        Label_036A:
            this.mCurrentTeamIndex = GlobalVars.SelectedTeamIndex;
            this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
            goto Label_03A8;
        Label_0391:
            this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
        Label_03A8:
            this.AssignUnits(this.mCurrentParty);
        Label_03B4:
            this.ValidateSupport(this.mMaxTeamCount);
            this.mCurrentSupport = this.mSupports[this.mCurrentTeamIndex];
            this.ResetTeamPulldown(this.mTeams, this.mMaxTeamCount, this.mCurrentQuest);
            this.ChangeEnabledArrowButtons(this.mCurrentTeamIndex, this.mMaxTeamCount);
            this.ChangeEnabledTeamTabs(this.mCurrentTeamIndex);
            return;
        }

        private void LockSlots()
        {
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            VS_MODE vs_mode;
            int num6;
            VS_MODE vs_mode2;
            int num7;
            int num8;
            PartySlotData data;
            int num9;
            int num10;
            int num11;
            this.mLockedPartySlots = 0;
            this.mSupportLocked = 0;
            this.mItemsLocked = 0;
            if (this.mCurrentPartyType != 3)
            {
                goto Label_01C3;
            }
            this.mSupportLocked = 1;
            this.mItemsLocked = 1;
            if (GameUtility.GetCurrentScene() != 4)
            {
                goto Label_0405;
            }
            room = PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom();
        Label_005D:
            param = ((room != null) && (string.IsNullOrEmpty(room.json) == null)) ? JSON_MyPhotonRoomParam.Parse(room.json) : null;
            if (GlobalVars.SelectedMultiPlayRoomType != null)
            {
                goto Label_0123;
            }
            if (param == null)
            {
                goto Label_0405;
            }
            num = param.GetUnitSlotNum();
            num2 = 0;
            goto Label_0107;
        Label_008E:
            if (num2 < num)
            {
                goto Label_00AD;
            }
            this.mLockedPartySlots |= 1 << ((num2 & 0x1f) & 0x1f);
        Label_00AD:
            if (num2 >= ((int) this.UnitSlots.Length))
            {
                goto Label_0101;
            }
            if ((this.UnitSlots[num2] != null) == null)
            {
                goto Label_0101;
            }
            if ((this.UnitSlots[num2].SelectButton != null) == null)
            {
                goto Label_0101;
            }
            this.UnitSlots[num2].SelectButton.set_interactable(num2 < num);
        Label_0101:
            num2 += 1;
        Label_0107:
            if (num2 < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_008E;
            }
            goto Label_01BE;
        Label_0123:
            num3 = this.mCurrentParty.PartyData.SUBMEMBER_START;
            goto Label_01A7;
        Label_013A:
            this.mLockedPartySlots |= 1 << ((num3 & 0x1f) & 0x1f);
            if (num3 >= ((int) this.UnitSlots.Length))
            {
                goto Label_01A1;
            }
            if ((this.UnitSlots[num3] != null) == null)
            {
                goto Label_01A1;
            }
            if ((this.UnitSlots[num3].SelectButton != null) == null)
            {
                goto Label_01A1;
            }
            this.UnitSlots[num3].SelectButton.set_interactable(0);
        Label_01A1:
            num3 += 1;
        Label_01A7:
            if (num3 < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_013A;
            }
        Label_01BE:
            goto Label_0405;
        Label_01C3:
            if (this.mCurrentPartyType == 4)
            {
                goto Label_01DB;
            }
            if (this.mCurrentPartyType != 5)
            {
                goto Label_0232;
            }
        Label_01DB:
            this.mSupportLocked = 1;
            this.mItemsLocked = 1;
            num4 = 0;
            goto Label_0216;
        Label_01F1:
            if (num4 < 3)
            {
                goto Label_0210;
            }
            this.mLockedPartySlots |= 1 << ((num4 & 0x1f) & 0x1f);
        Label_0210:
            num4 += 1;
        Label_0216:
            if (num4 < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_01F1;
            }
            goto Label_0405;
        Label_0232:
            if (this.mCurrentPartyType != 9)
            {
                goto Label_0283;
            }
            this.mSupportLocked = 1;
            this.mItemsLocked = 1;
            num5 = 0;
            goto Label_026F;
        Label_0255:
            this.UnitSlots[num5].SelectButton.set_interactable(1);
            num5 += 1;
        Label_026F:
            if (num5 < ((int) this.UnitSlots.Length))
            {
                goto Label_0255;
            }
            goto Label_0405;
        Label_0283:
            if (this.mCurrentPartyType != 8)
            {
                goto Label_02F4;
            }
            if (MonoSingleton<GameManager>.Instance.GetVSMode(-1L) != null)
            {
                goto Label_0405;
            }
            num6 = this.mCurrentParty.PartyData.VSWAITMEMBER_START;
            goto Label_02D8;
        Label_02BB:
            this.mLockedPartySlots |= 1 << ((num6 & 0x1f) & 0x1f);
            num6 += 1;
        Label_02D8:
            if (num6 < this.mCurrentParty.PartyData.VSWAITMEMBER_END)
            {
                goto Label_02BB;
            }
            goto Label_0405;
        Label_02F4:
            if (this.mCurrentPartyType != 11)
            {
                goto Label_0366;
            }
            if (MonoSingleton<GameManager>.Instance.GetVSMode(-1L) != null)
            {
                goto Label_0405;
            }
            num7 = this.mCurrentParty.PartyData.VSWAITMEMBER_START;
            goto Label_034A;
        Label_032D:
            this.mLockedPartySlots |= 1 << ((num7 & 0x1f) & 0x1f);
            num7 += 1;
        Label_034A:
            if (num7 < this.mCurrentParty.PartyData.VSWAITMEMBER_END)
            {
                goto Label_032D;
            }
            goto Label_0405;
        Label_0366:
            if (this.mCurrentPartyType == 1)
            {
                goto Label_038A;
            }
            if (this.mCurrentPartyType == 6)
            {
                goto Label_038A;
            }
            if (this.mCurrentPartyType != 2)
            {
                goto Label_0405;
            }
        Label_038A:
            this.mSupportLocked = 0;
            this.mItemsLocked = 0;
            num8 = 0;
            goto Label_03F3;
        Label_03A0:
            data = this.mSlotData[num8];
            if (data.Type == 1)
            {
                goto Label_03D6;
            }
            if (data.Type == 4)
            {
                goto Label_03D6;
            }
            if (data.Type != 5)
            {
                goto Label_03ED;
            }
        Label_03D6:
            this.mLockedPartySlots |= 1 << ((num8 & 0x1f) & 0x1f);
        Label_03ED:
            num8 += 1;
        Label_03F3:
            if (num8 < this.mSlotData.Count)
            {
                goto Label_03A0;
            }
        Label_0405:
            if (this.mCurrentQuest == null)
            {
                goto Label_047A;
            }
            if (this.mCurrentQuest.type != 3)
            {
                goto Label_047A;
            }
            this.mItemsLocked = 1;
            this.mSupportLocked = 1;
            num9 = this.mCurrentParty.PartyData.SUBMEMBER_START;
            goto Label_0463;
        Label_0446:
            this.mLockedPartySlots |= 1 << ((num9 & 0x1f) & 0x1f);
            num9 += 1;
        Label_0463:
            if (num9 <= this.mCurrentParty.PartyData.SUBMEMBER_END)
            {
                goto Label_0446;
            }
        Label_047A:
            if (this.mCurrentQuest == null)
            {
                goto Label_049C;
            }
            if (this.mCurrentQuest.UseSupportUnit != null)
            {
                goto Label_049C;
            }
            this.mSupportLocked = 1;
        Label_049C:
            if ((this.NoItemText != null) == null)
            {
                goto Label_04BE;
            }
            this.NoItemText.SetActive(this.mItemsLocked);
        Label_04BE:
            num10 = 0;
            goto Label_0502;
        Label_04C6:
            if ((this.UnitSlots[num10] != null) == null)
            {
                goto Label_04FC;
            }
            this.UnitSlots[num10].SetLocked(((this.mLockedPartySlots & (1 << (num10 & 0x1f))) == 0) == 0);
        Label_04FC:
            num10 += 1;
        Label_0502:
            if (num10 < ((int) this.UnitSlots.Length))
            {
                goto Label_04C6;
            }
            if ((this.FriendSlot != null) == null)
            {
                goto Label_0533;
            }
            this.FriendSlot.SetLocked(this.mSupportLocked);
        Label_0533:
            num11 = 0;
            goto Label_0569;
        Label_053B:
            if ((this.ItemSlots[num11] != null) == null)
            {
                goto Label_0563;
            }
            this.ItemSlots[num11].SetLocked(this.mItemsLocked);
        Label_0563:
            num11 += 1;
        Label_0569:
            if (num11 < ((int) this.ItemSlots.Length))
            {
                goto Label_053B;
            }
            return;
        }

        private void LockWindow(bool y)
        {
            if (y == null)
            {
                goto Label_0012;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 5);
            goto Label_0019;
        Label_0012:
            FlowNode_GameObject.ActivateOutputLinks(this, 6);
        Label_0019:
            return;
        }

        private void MoveToOrigin(GameObject go)
        {
            RectTransform transform;
            transform = go.GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_001E;
            }
            transform.set_anchoredPosition(Vector2.get_zero());
        Label_001E:
            return;
        }

        public void NextTeam()
        {
            this.OnNextTeamChange();
            return;
        }

        public void OnAutoBattleSetting(string name, ActionCall.EventType eventType, SerializeValueList list)
        {
            bool flag;
            Toggle toggle;
            Toggle toggle2;
            Toggle toggle3;
            ActionCall.EventType type;
            bool flag2;
            flag = 0;
            if (this.mCurrentQuest == null)
            {
                goto Label_0032;
            }
            flag = (this.mCurrentQuest.FirstAutoPlayProhibit == null) ? 0 : ((this.mCurrentQuest.state == 2) == 0);
        Label_0032:
            if ((name == "PROHIBIT") == null)
            {
                goto Label_0071;
            }
            type = eventType;
            if (type == 1)
            {
                goto Label_005A;
            }
            if (type == 5)
            {
                goto Label_005A;
            }
            goto Label_006C;
        Label_005A:
            list.SetActive("item", flag);
        Label_006C:
            goto Label_0225;
        Label_0071:
            if ((name == "SETTING") == null)
            {
                goto Label_0225;
            }
            toggle = list.GetUIToggle("btn_auto");
            toggle2 = list.GetUIToggle("btn_treasure");
            toggle3 = list.GetUIToggle("btn_skill");
            type = eventType;
            switch ((type - 1))
            {
                case 0:
                    goto Label_00CA;

                case 1:
                    goto Label_01AD;

                case 2:
                    goto Label_0225;

                case 3:
                    goto Label_0225;

                case 4:
                    goto Label_00CA;
            }
            goto Label_0225;
        Label_00CA:
            list.SetActive("item", flag);
            if (flag == null)
            {
                goto Label_0134;
            }
            if ((toggle != null) == null)
            {
                goto Label_0109;
            }
            flag2 = 0;
            toggle.set_interactable(flag2);
            toggle.set_isOn(flag2);
            list.SetActive("off", 1);
        Label_0109:
            if ((toggle2 != null) == null)
            {
                goto Label_011C;
            }
            toggle2.set_isOn(0);
        Label_011C:
            if ((toggle3 != null) == null)
            {
                goto Label_0225;
            }
            toggle3.set_isOn(0);
            goto Label_01A8;
        Label_0134:
            if ((toggle != null) == null)
            {
                goto Label_0157;
            }
            toggle.set_isOn(GameUtility.Config_UseAutoPlay.Value);
            toggle.set_interactable(1);
        Label_0157:
            if ((toggle2 != null) == null)
            {
                goto Label_0173;
            }
            toggle2.set_isOn(GameUtility.Config_AutoMode_Treasure.Value);
        Label_0173:
            if ((toggle3 != null) == null)
            {
                goto Label_018F;
            }
            toggle3.set_isOn(GameUtility.Config_AutoMode_DisableSkill.Value);
        Label_018F:
            list.SetActive("off", GameUtility.Config_UseAutoPlay.Value == 0);
        Label_01A8:
            goto Label_0225;
        Label_01AD:
            if (flag != null)
            {
                goto Label_0225;
            }
            if ((toggle != null) == null)
            {
                goto Label_01CF;
            }
            GameUtility.Config_UseAutoPlay.Value = toggle.get_isOn();
        Label_01CF:
            if ((toggle2 != null) == null)
            {
                goto Label_01EB;
            }
            GameUtility.Config_AutoMode_Treasure.Value = toggle2.get_isOn();
        Label_01EB:
            if ((toggle3 != null) == null)
            {
                goto Label_0207;
            }
            GameUtility.Config_AutoMode_DisableSkill.Value = toggle3.get_isOn();
        Label_0207:
            list.SetActive("off", GameUtility.Config_UseAutoPlay.Value == 0);
        Label_0225:
            return;
        }

        private void OnCloseItemListClick(SRPG_Button button)
        {
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x15);
            return;
        }

        private void OnDestroy()
        {
            GameManager local2;
            GameManager local1;
            if ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null)
            {
                goto Label_005C;
            }
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnStaminaChange = (GameManager.StaminaChangeEvent) Delegate.Remove(local1.OnStaminaChange, new GameManager.StaminaChangeEvent(this.OnStaminaChange));
            local2 = MonoSingleton<GameManager>.Instance;
            local2.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Remove(local2.OnSceneChange, new GameManager.SceneChangeEvent(this.OnHomeMenuChange));
        Label_005C:
            GameUtility.DestroyGameObject(this.UnitListHilit);
            GameUtility.DestroyGameObject(this.ItemListHilit);
            GameUtility.DestroyGameObjects<RectTransform>(this.ChosenUnitBadges);
            GameUtility.DestroyGameObjects<RectTransform>(this.ChosenItemBadges);
            if ((this.ChosenSupportBadge != null) == null)
            {
                goto Label_00B0;
            }
            this.ChosenSupportBadge.get_transform().SetParent(base.get_transform(), 0);
        Label_00B0:
            if ((this.ItemRemoveItem != null) == null)
            {
                goto Label_00D8;
            }
            this.ItemRemoveItem.get_transform().SetParent(base.get_transform(), 0);
        Label_00D8:
            GameUtility.DestroyGameObjects<RectTransform>(this.mItemPoolA);
            GameUtility.DestroyGameObjects<RectTransform>(this.mItemPoolB);
            GameUtility.DestroyGameObjects<RectTransform>(this.mUnitPoolA);
            GameUtility.DestroyGameObjects<RectTransform>(this.mUnitPoolB);
            GameUtility.DestroyGameObjects<RectTransform>(this.mSupportPoolA);
            GameUtility.DestroyGameObjects<RectTransform>(this.mSupportPoolB);
            GameUtility.DestroyGameObject(this.mRaidSettings);
            this.UnitList_Remove();
            if ((this.mMultiErrorMsg != null) == null)
            {
                goto Label_0148;
            }
            UIUtility.PopCanvas();
            this.mMultiErrorMsg = null;
        Label_0148:
            GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
            return;
        }

        private void OnDisable()
        {
            UnitJobDropdown.OnJobChange = (UnitJobDropdown.JobChangeEvent) Delegate.Remove(UnitJobDropdown.OnJobChange, new UnitJobDropdown.JobChangeEvent(this.OnUnitJobChange));
            return;
        }

        private void OnEnable()
        {
            UnitJobDropdown.OnJobChange = (UnitJobDropdown.JobChangeEvent) Delegate.Combine(UnitJobDropdown.OnJobChange, new UnitJobDropdown.JobChangeEvent(this.OnUnitJobChange));
            return;
        }

        private unsafe void OnForwardOrBackButtonClick(SRPG_Button button)
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            string str;
            FixParam param;
            int num;
            int num2;
            int num3;
            MyPhoton photon;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param2;
            QuestParam param3;
            int num4;
            bool flag;
            int num5;
            UnitData data;
            bool flag2;
            int num6;
            int num7;
            int num8;
            int num9;
            List<string> list;
            bool flag3;
            int num10;
            int num11;
            PartySlotTypeUnitPair[] pairArray;
            UnitParam param4;
            string str2;
            string str3;
            List<UnitData> list2;
            int num12;
            UnitData data2;
            string str4;
            List<QuestClearUnlockUnitDataParam> list3;
            int num13;
            QuestClearUnlockUnitDataParam param5;
            <OnForwardOrBackButtonClick>c__AnonStorey36E storeye;
            <OnForwardOrBackButtonClick>c__AnonStorey36F storeyf;
            <OnForwardOrBackButtonClick>c__AnonStorey370 storey;
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((button == this.ForwardButton) == null)
            {
                goto Label_0A10;
            }
            if (this.mCurrentQuest == null)
            {
                goto Label_01BF;
            }
            if (this.mCurrentQuest.CheckEnableChallange() != null)
            {
                goto Label_01BF;
            }
            if (this.mCurrentQuest.CheckEnableReset() != null)
            {
                goto Label_00A7;
            }
            str = string.Empty;
            if (this.mCurrentQuest.dayReset <= 0)
            {
                goto Label_006F;
            }
            str = LocalizedText.Get("sys.QUEST_CHALLENGE_NO_RESET");
            goto Label_007A;
        Label_006F:
            str = LocalizedText.Get("sys.QUEST_SPAN_CHALLENGE_NO_RESET");
        Label_007A:
            if (<>f__am$cache86 != null)
            {
                goto Label_0094;
            }
            <>f__am$cache86 = new UIUtility.DialogResultEvent(PartyWindow2.<OnForwardOrBackButtonClick>m__385);
        Label_0094:
            UIUtility.NegativeSystemMessage(null, str, <>f__am$cache86, null, 0, -1);
            goto Label_01BE;
        Label_00A7:
            storeye = new <OnForwardOrBackButtonClick>c__AnonStorey36E();
            storeye.<>f__this = this;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            num = param.EliteResetMax - this.mCurrentQuest.dailyReset;
            storeye.coin = 0;
            if (param.EliteResetCosts == null)
            {
                goto Label_015B;
            }
            if (this.mCurrentQuest.dailyReset >= ((int) param.EliteResetCosts.Length))
            {
                goto Label_0135;
            }
            storeye.coin = *(&(param.EliteResetCosts[this.mCurrentQuest.dailyReset]));
            goto Label_015B;
        Label_0135:
            storeye.coin = *(&(param.EliteResetCosts[((int) param.EliteResetCosts.Length) - 1]));
        Label_015B:
            storeye.msg = string.Format(LocalizedText.Get("sys.QUEST_CHALLENGE_RESET"), (int) storeye.coin, (int) num);
            if (<>f__am$cache87 != null)
            {
                goto Label_01B0;
            }
            <>f__am$cache87 = new UIUtility.DialogResultEvent(PartyWindow2.<OnForwardOrBackButtonClick>m__387);
        Label_01B0:
            UIUtility.ConfirmBox(storeye.msg, null, new UIUtility.DialogResultEvent(storeye.<>m__386), <>f__am$cache87, null, 0, -1);
        Label_01BE:
            return;
        Label_01BF:
            if (this.mCurrentQuest == null)
            {
                goto Label_03AD;
            }
            if (this.mCurrentQuest.IsMulti != null)
            {
                goto Label_02BF;
            }
            if (this.mCurrentQuest.IsDateUnlock(-1L) != null)
            {
                goto Label_0237;
            }
            if (this.mCurrentQuest.IsBeginner == null)
            {
                goto Label_0216;
            }
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.BEGINNER_QUEST_OUT_OF_DATE"), null, null, 0, -1);
            goto Label_0236;
        Label_0216:
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), new UIUtility.DialogResultEvent(this.JumpToBefore), null, 0, -1);
        Label_0236:
            return;
        Label_0237:
            if (this.mCurrentQuest.IsKeyQuest == null)
            {
                goto Label_0278;
            }
            if (this.mCurrentQuest.IsKeyUnlock(-1L) != null)
            {
                goto Label_0278;
            }
            UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_AVAILABLE_CAUTION"), null, null, 0, -1);
            return;
        Label_0278:
            num2 = MonoSingleton<GameManager>.Instance.Player.Lv;
            num3 = this.mCurrentQuest.RequiredApWithPlayerLv(num2, 1);
            if (MonoSingleton<GameManager>.Instance.Player.Stamina >= num3)
            {
                goto Label_03AD;
            }
            MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(1, this);
            return;
            goto Label_03AD;
        Label_02BF:
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon != null) == null)
            {
                goto Label_03AD;
            }
            if (photon.CurrentState != 4)
            {
                goto Label_03AD;
            }
            room = photon.GetCurrentRoom();
            if (room == null)
            {
                goto Label_03AD;
            }
            param2 = JSON_MyPhotonRoomParam.Parse(room.json);
            param3 = MonoSingleton<GameManager>.Instance.FindQuest(param2.iname);
            num4 = param2.unitlv;
            flag = 1;
            if (param3 == null)
            {
                goto Label_03AD;
            }
            if (num4 <= 0)
            {
                goto Label_03AD;
            }
            num5 = 0;
            goto Label_0364;
        Label_0334:
            data = this.mCurrentParty.Units[num5];
            if (data == null)
            {
                goto Label_035E;
            }
            flag &= (data.CalcLevel() < num4) == 0;
        Label_035E:
            num5 += 1;
        Label_0364:
            if (num5 < param3.unitNum)
            {
                goto Label_0334;
            }
            if (flag != null)
            {
                goto Label_03AD;
            }
            this.mMultiErrorMsg = UIUtility.SystemMessage(LocalizedText.Get("sys.TITLE"), LocalizedText.Get("sys.PARTYEDITOR_ULV"), new UIUtility.DialogResultEvent(this.<OnForwardOrBackButtonClick>m__388), null, 0, -1);
            return;
        Label_03AD:
            if (this.mCurrentQuest == null)
            {
                goto Label_0A05;
            }
            storeyf = new <OnForwardOrBackButtonClick>c__AnonStorey36F();
            storeyf.<>f__this = this;
            if (this.mCurrentQuest.IsQuestDrops == null)
            {
                goto Label_043C;
            }
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_043C;
            }
            flag2 = QuestDropParam.Instance.IsChangedQuestDrops(this.mCurrentQuest);
            GlobalVars.SetDropTableGeneratedTime();
            if (flag2 == null)
            {
                goto Label_043C;
            }
            this.HardQuestDropPiecesUpdate();
            if (QuestDropParam.Instance.IsWarningPopupDisable != null)
            {
                goto Label_043C;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), new UIUtility.DialogResultEvent(storeyf.<>m__389), null, 0, -1);
            return;
        Label_043C:
            num6 = 0;
            num7 = 0;
            num8 = 0;
            num9 = this.AvailableMainMemberSlots;
            list = new List<string>();
            flag3 = 0;
            num10 = 0;
            goto Label_052F;
        Label_045F:
            if (((int) this.mCurrentParty.Units.Length) > num10)
            {
                goto Label_0478;
            }
            goto Label_0529;
        Label_0478:
            if (this.mCurrentParty.Units[num10] != null)
            {
                goto Label_04D3;
            }
            if (this.mSlotData[num10].Type == 4)
            {
                goto Label_04D3;
            }
            if (this.mSlotData[num10].Type == 5)
            {
                goto Label_04D3;
            }
            if (this.mSlotData[num10].Type != 3)
            {
                goto Label_04D9;
            }
        Label_04D3:
            num6 += 1;
        Label_04D9:
            if ((this.mLockedPartySlots & (1 << (num10 & 0x1f))) != null)
            {
                goto Label_0529;
            }
            num7 += 1;
            if (this.mCurrentParty.Units[num10] == null)
            {
                goto Label_0529;
            }
            if (this.mCurrentQuest.IsUnitAllowed(this.mCurrentParty.Units[num10]) != null)
            {
                goto Label_0529;
            }
            num8 += 1;
        Label_0529:
            num10 += 1;
        Label_052F:
            if (num10 < num9)
            {
                goto Label_045F;
            }
            if (this.EnableHeroSolo == null)
            {
                goto Label_058D;
            }
            if (this.mGuestUnit == null)
            {
                goto Label_058D;
            }
            if (this.mGuestUnit.Count <= 0)
            {
                goto Label_058D;
            }
            num11 = this.mGuestUnit.Count;
            if (num6 >= 1)
            {
                goto Label_057F;
            }
            if (num11 != 1)
            {
                goto Label_057F;
            }
            flag3 = 1;
        Label_057F:
            num6 += num11;
            num7 += num11;
        Label_058D:
            storeyf.force_units = null;
            if (this.mCurrentQuest.questParty == null)
            {
                goto Label_060E;
            }
            pairArray = this.mCurrentQuest.questParty.GetMainSubSlots();
            if (<>f__am$cache88 != null)
            {
                goto Label_05D3;
            }
            <>f__am$cache88 = new Func<PartySlotTypeUnitPair, bool>(PartyWindow2.<OnForwardOrBackButtonClick>m__38A);
        Label_05D3:
            if (<>f__am$cache89 != null)
            {
                goto Label_05F5;
            }
            <>f__am$cache89 = new Func<PartySlotTypeUnitPair, string>(PartyWindow2.<OnForwardOrBackButtonClick>m__38B);
        Label_05F5:
            storeyf.force_units = Enumerable.ToArray<string>(Enumerable.Select<PartySlotTypeUnitPair, string>(Enumerable.Where<PartySlotTypeUnitPair>(pairArray, <>f__am$cache88), <>f__am$cache89));
            goto Label_0625;
        Label_060E:
            storeyf.force_units = this.mCurrentQuest.units.GetList();
        Label_0625:
            if (storeyf.force_units == null)
            {
                goto Label_06C8;
            }
            storey = new <OnForwardOrBackButtonClick>c__AnonStorey370();
            storey.<>f__ref$879 = storeyf;
            storey.i = 0;
            goto Label_06B3;
        Label_064E:
            if (0 <= MonoSingleton<GameManager>.Instance.Player.Units.FindIndex(new Predicate<UnitData>(storey.<>m__38C)))
            {
                goto Label_06A3;
            }
            param4 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(storeyf.force_units[storey.i]);
            list.Add(param4.name);
        Label_06A3:
            storey.i += 1;
        Label_06B3:
            if (storey.i < ((int) storeyf.force_units.Length))
            {
                goto Label_064E;
            }
        Label_06C8:
            if (1 > list.Count)
            {
                goto Label_0725;
            }
            str2 = string.Join(",", list.ToArray());
            objArray1 = new object[] { str2 };
            if (<>f__am$cache8A != null)
            {
                goto Label_0716;
            }
            <>f__am$cache8A = new UIUtility.DialogResultEvent(PartyWindow2.<OnForwardOrBackButtonClick>m__38D);
        Label_0716:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.PARTYEDITOR_NOHERO", objArray1), <>f__am$cache8A, null, 0, -1);
            return;
        Label_0725:
            if (this.CheckMember(num6) != null)
            {
                goto Label_0733;
            }
            return;
        Label_0733:
            str3 = string.Empty;
            if (this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units, &str3) != null)
            {
                goto Label_07A9;
            }
            if (this.EnableHeroSolo == null)
            {
                goto Label_077A;
            }
            if (this.mCurrentQuest.IsEntryQuestCondition(this.mGuestUnit, &str3) != null)
            {
                goto Label_07A9;
            }
        Label_077A:
            if (<>f__am$cache8B != null)
            {
                goto Label_079A;
            }
            <>f__am$cache8B = new UIUtility.DialogResultEvent(PartyWindow2.<OnForwardOrBackButtonClick>m__38E);
        Label_079A:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get(str3), <>f__am$cache8B, null, 0, -1);
            return;
        Label_07A9:
            if (this.mCurrentSupport == null)
            {
                goto Label_0810;
            }
            if (this.mCurrentSupport.Unit == null)
            {
                goto Label_0810;
            }
            if (this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentSupport.Unit, &str3) != null)
            {
                goto Label_0810;
            }
            if (<>f__am$cache8C != null)
            {
                goto Label_0801;
            }
            <>f__am$cache8C = new UIUtility.DialogResultEvent(PartyWindow2.<OnForwardOrBackButtonClick>m__38F);
        Label_0801:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get(str3), <>f__am$cache8C, null, 0, -1);
            return;
        Label_0810:
            if (this.mCurrentQuest.IsCharacterQuest() == null)
            {
                goto Label_0972;
            }
            list2 = new List<UnitData>(this.mCurrentParty.Units);
            list2.Add(this.mGuestUnit[0]);
            num12 = 0;
            goto Label_0964;
        Label_084D:
            data2 = list2[num12];
            if (data2 == null)
            {
                goto Label_095E;
            }
            if ((data2.UnitID == this.mGuestUnit[0].UnitID) == null)
            {
                goto Label_095E;
            }
            str4 = string.Empty;
            list3 = data2.SkillUnlocks;
            num13 = 0;
            goto Label_0915;
        Label_0899:
            param5 = list3[num13];
            if (param5 == null)
            {
                goto Label_090F;
            }
            if (param5.add != null)
            {
                goto Label_090F;
            }
            if (param5.qids == null)
            {
                goto Label_090F;
            }
            if (Array.FindIndex<string>(param5.qids, new Predicate<string>(storeyf.<>m__390)) == -1)
            {
                goto Label_090F;
            }
            objArray2 = new object[] { param5.GetUnlockTypeText(), param5.GetRewriteName() };
            str4 = str4 + LocalizedText.Get("sys.UNITLIST_REWRITE_TARGET", objArray2);
        Label_090F:
            num13 += 1;
        Label_0915:
            if (num13 < list3.Count)
            {
                goto Label_0899;
            }
            if (string.IsNullOrEmpty(str4) != null)
            {
                goto Label_095E;
            }
            objArray3 = new object[] { str4 };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.UNITLIST_DATA_REWRITE", objArray3), new UIUtility.DialogResultEvent(storeyf.<>m__391), null, null, 0, -1, null, null);
            return;
        Label_095E:
            num12 += 1;
        Label_0964:
            if (num12 < list2.Count)
            {
                goto Label_084D;
            }
        Label_0972:
            if (num8 <= 0)
            {
                goto Label_099E;
            }
            UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_SANKAFUKA"), new UIUtility.DialogResultEvent(storeyf.<>m__392), null, null, 0, -1, null, null);
            return;
        Label_099E:
            if (flag3 != null)
            {
                goto Label_0A05;
            }
            if (num6 >= num7)
            {
                goto Label_0A05;
            }
            if (this.PartyType == 9)
            {
                goto Label_0A05;
            }
            if (this.PartyType != 11)
            {
                goto Label_09E1;
            }
            UIUtility.SystemMessage(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL_INVALID"), null, null, 0, -1);
            goto Label_0A04;
        Label_09E1:
            UIUtility.ConfirmBox(LocalizedText.Get("sys.PARTYEDITOR_PARTYNOTFULL"), new UIUtility.DialogResultEvent(storeyf.<>m__393), null, null, 0, -1, null, null);
        Label_0A04:
            return;
        Label_0A05:
            this.PostForwardPressed();
            goto Label_0A5B;
        Label_0A10:
            if (this.PartyType != 9)
            {
                goto Label_0A25;
            }
            this.SaveAndActivatePin(8);
            return;
        Label_0A25:
            if (this.PartyType != 10)
            {
                goto Label_0A54;
            }
            GlobalVars.OrdealParties = this.mTeams;
            GlobalVars.OrdealSupports = this.mSupports;
            FlowNode_GameObject.ActivateOutputLinks(this, 3);
            goto Label_0A5B;
        Label_0A54:
            this.SaveAndActivatePin(3);
        Label_0A5B:
            return;
        }

        private RectTransform OnGetItemListItem(int id, int old, RectTransform current)
        {
            RectTransform transform;
            ListItemEvents events;
            int num;
            int num2;
            <OnGetItemListItem>c__AnonStorey369 storey;
            storey = new <OnGetItemListItem>c__AnonStorey369();
            storey.id = id;
            storey.<>f__this = this;
            transform = null;
            if (storey.id != null)
            {
                goto Label_0049;
            }
            if ((this.ItemRemoveItem != null) == null)
            {
                goto Label_0047;
            }
            return (this.ItemRemoveItem.get_transform() as RectTransform);
        Label_0047:
            return null;
        Label_0049:
            if ((this.ItemListItem == null) == null)
            {
                goto Label_005C;
            }
            return null;
        Label_005C:
            if (old > 0)
            {
                goto Label_0117;
            }
            if ((this.ItemRemoveItem != null) == null)
            {
                goto Label_00A0;
            }
            if ((this.ItemRemoveItem.get_transform() == current) == null)
            {
                goto Label_00A0;
            }
            this.ItemRemoveItem.get_transform().SetParent(UIUtility.Pool, 0);
        Label_00A0:
            if (this.mItemPoolA.Count > 0)
            {
                goto Label_00D5;
            }
            transform = this.CreateItemListItem().get_transform() as RectTransform;
            this.mItemPoolB.Add(transform);
            goto Label_0112;
        Label_00D5:
            transform = this.mItemPoolA[this.mItemPoolA.Count - 1];
            this.mItemPoolA.RemoveAt(this.mItemPoolA.Count - 1);
            this.mItemPoolB.Add(transform);
        Label_0112:
            goto Label_0119;
        Label_0117:
            transform = current;
        Label_0119:
            DataSource.Bind<ItemData>(transform.get_gameObject(), this.mOwnItems[storey.id - 1]);
            transform.get_gameObject().SetActive(1);
            GameParameter.UpdateAll(transform.get_gameObject());
            num = Array.FindIndex<ItemData>(this.mCurrentItems, new Predicate<ItemData>(storey.<>m__37B));
            if ((this.ItemListHilit != null) == null)
            {
                goto Label_01E0;
            }
            if (this.mSelectedSlotIndex != num)
            {
                goto Label_019D;
            }
            this.AttachAndEnable(this.ItemListHilit, transform, this.ItemListHilitParent);
            goto Label_01E0;
        Label_019D:
            if ((this.ItemListHilit.get_parent() == transform.FindChild(this.ItemListHilitParent)) == null)
            {
                goto Label_01E0;
            }
            this.ItemListHilit.SetParent(UIUtility.Pool, 0);
            this.ItemListHilit.get_gameObject().SetActive(0);
        Label_01E0:
            if (num < 0)
            {
                goto Label_0221;
            }
            if ((this.ChosenItemBadges[num] != null) == null)
            {
                goto Label_0290;
            }
            this.ChosenItemBadges[num].SetParent(transform, 0);
            this.ChosenItemBadges[num].get_gameObject().SetActive(1);
            goto Label_0290;
        Label_0221:
            num2 = 0;
            goto Label_0282;
        Label_0228:
            if ((this.ChosenItemBadges[num2] != null) == null)
            {
                goto Label_027E;
            }
            if ((this.ChosenItemBadges[num2].get_parent() == transform) == null)
            {
                goto Label_027E;
            }
            this.ChosenItemBadges[num2].SetParent(UIUtility.Pool, 0);
            this.ChosenItemBadges[num2].get_gameObject().SetActive(0);
            goto Label_0290;
        Label_027E:
            num2 += 1;
        Label_0282:
            if (num2 < ((int) this.ChosenItemBadges.Length))
            {
                goto Label_0228;
            }
        Label_0290:
            return transform;
        }

        private bool OnHomeMenuChange()
        {
            if (this.IsPartyDirty == null)
            {
                goto Label_0034;
            }
            if (this.mIsSaving != null)
            {
                goto Label_001E;
            }
            this.SaveParty(null, null);
        Label_001E:
            MonoSingleton<GameManager>.Instance.RegisterImportantJob(base.StartCoroutine(this.WaitForSave()));
        Label_0034:
            this.SaveInventory();
            return 1;
        }

        private void OnItemFilterChange(SRPG_Button button)
        {
            ItemFilterTypes types;
            int num;
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            types = 0;
            if ((button == this.ItemFilter_Offense) == null)
            {
                goto Label_0026;
            }
            types = 1;
            goto Label_0039;
        Label_0026:
            if ((button == this.ItemFilter_Support) == null)
            {
                goto Label_0039;
            }
            types = 2;
        Label_0039:
            if (this.mItemFilter != types)
            {
                goto Label_0046;
            }
            return;
        Label_0046:
            this.mItemFilter = types;
            num = 0;
            goto Label_007C;
        Label_0054:
            if ((this.mItemFilterToggles[num] != null) == null)
            {
                goto Label_0078;
            }
            this.mItemFilterToggles[num].IsOn = num == types;
        Label_0078:
            num += 1;
        Label_007C:
            if (num < ((int) this.mItemFilterToggles.Length))
            {
                goto Label_0054;
            }
            this.RefreshItemList();
            return;
        }

        private void OnItemRemoveSelect(SRPG_Button button)
        {
            this.SetItemSlot(this.mSelectedSlotIndex, null);
            this.OnItemSlotsChange();
            this.SaveInventory();
            FlowNode_GameObject.ActivateOutputLinks(this, 0x15);
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            int num;
            ItemData data;
            ItemData data2;
            int num2;
            int num3;
            num = this.mSelectedSlotIndex;
            data = this.mCurrentItems[this.mSelectedSlotIndex];
            goto Label_001E;
        Label_001A:
            num -= 1;
        Label_001E:
            if (0 >= num)
            {
                goto Label_0034;
            }
            if (this.mCurrentItems[num - 1] == null)
            {
                goto Label_001A;
            }
        Label_0034:
            data2 = DataSource.FindDataOfClass<ItemData>(go, null);
            if (data2 == null)
            {
                goto Label_00C1;
            }
            if (data2 == data)
            {
                goto Label_00C1;
            }
            num2 = -1;
            num3 = 0;
            goto Label_0088;
        Label_0053:
            if (this.mCurrentItems[num3] == null)
            {
                goto Label_0082;
            }
            if (this.mCurrentItems[num3].Param != data2.Param)
            {
                goto Label_0082;
            }
            num2 = num3;
            goto Label_0097;
        Label_0082:
            num3 += 1;
        Label_0088:
            if (num3 < ((int) this.mCurrentItems.Length))
            {
                goto Label_0053;
            }
        Label_0097:
            if (num2 < 0)
            {
                goto Label_00B9;
            }
            if (data == null)
            {
                goto Label_00C1;
            }
            this.SetItemSlot(num, data2);
            this.SetItemSlot(num2, data);
            goto Label_00C1;
        Label_00B9:
            this.SetItemSlot(num, data2);
        Label_00C1:
            this.OnItemSlotsChange();
            this.SaveInventory();
            FlowNode_GameObject.ActivateOutputLinks(this, 0x15);
            return;
        }

        private void OnItemSlotClick(GenericSlot slot, bool interactable)
        {
            int num;
            if (interactable == null)
            {
                goto Label_0011;
            }
            if (this.mInitialized != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            num = Array.IndexOf<GenericSlot>(this.ItemSlots, slot);
            if (num < 0)
            {
                goto Label_002D;
            }
            if (5 > num)
            {
                goto Label_002E;
            }
        Label_002D:
            return;
        Label_002E:
            this.mSelectedSlotIndex = num;
            this.RefreshItemList();
            return;
        }

        protected virtual void OnItemSlotsChange()
        {
            int num;
            if ((this.AddItemOverlay != null) == null)
            {
                goto Label_009E;
            }
            this.AddItemOverlay.SetActive(0);
            num = 0;
            goto Label_0090;
        Label_0024:
            if (this.mCurrentItems[num] != null)
            {
                goto Label_008C;
            }
            if (num >= ((int) this.ItemSlots.Length))
            {
                goto Label_008C;
            }
            if ((this.ItemSlots[num] != null) == null)
            {
                goto Label_008C;
            }
            if (this.mItemsLocked != null)
            {
                goto Label_008C;
            }
            this.AddItemOverlay.get_transform().SetParent(this.ItemSlots[num].get_transform(), 0);
            this.AddItemOverlay.SetActive(1);
            goto Label_009E;
        Label_008C:
            num += 1;
        Label_0090:
            if (num < ((int) this.mCurrentItems.Length))
            {
                goto Label_0024;
            }
        Label_009E:
            return;
        }

        private void OnNextTeamChange()
        {
            if (this.OnTeamChangeImpl(this.mCurrentTeamIndex + 1) == null)
            {
                goto Label_0035;
            }
            if ((this.TeamPulldown != null) == null)
            {
                goto Label_0035;
            }
            this.TeamPulldown.Selection = this.mCurrentTeamIndex;
        Label_0035:
            return;
        }

        private void OnPartyMemberChange()
        {
            int num;
            int num2;
            this.RefreshSankaStates();
            this.RecalcTotalAttack();
            this.UpdateLeaderSkills();
            if ((this.AddMainUnitOverlay != null) == null)
            {
                goto Label_00FA;
            }
            this.AddMainUnitOverlay.SetActive(0);
            num = this.mCurrentParty.PartyData.MAINMEMBER_START;
            goto Label_00E4;
        Label_0045:
            if (this.mCurrentParty.Units[num] != null)
            {
                goto Label_00E0;
            }
            if (num >= ((int) this.UnitSlots.Length))
            {
                goto Label_00E0;
            }
            if (num >= this.mSlotData.Count)
            {
                goto Label_00E0;
            }
            if ((this.UnitSlots[num] != null) == null)
            {
                goto Label_00E0;
            }
            if ((this.mLockedPartySlots & (1 << (num & 0x1f))) != null)
            {
                goto Label_00E0;
            }
            if (this.mSlotData[num].Type != null)
            {
                goto Label_00E0;
            }
            this.AddMainUnitOverlay.get_transform().SetParent(this.UnitSlots[num].get_transform(), 0);
            this.AddMainUnitOverlay.SetActive(1);
            goto Label_00FA;
        Label_00E0:
            num += 1;
        Label_00E4:
            if (num <= this.mCurrentParty.PartyData.MAINMEMBER_END)
            {
                goto Label_0045;
            }
        Label_00FA:
            if ((this.AddSubUnitOverlay != null) == null)
            {
                goto Label_01E2;
            }
            this.AddSubUnitOverlay.SetActive(0);
            num2 = this.mCurrentParty.PartyData.SUBMEMBER_START;
            goto Label_01CC;
        Label_012D:
            if (this.mCurrentParty.Units[num2] != null)
            {
                goto Label_01C8;
            }
            if (num2 >= ((int) this.UnitSlots.Length))
            {
                goto Label_01C8;
            }
            if (num2 >= this.mSlotData.Count)
            {
                goto Label_01C8;
            }
            if ((this.UnitSlots[num2] != null) == null)
            {
                goto Label_01C8;
            }
            if ((this.mLockedPartySlots & (1 << (num2 & 0x1f))) != null)
            {
                goto Label_01C8;
            }
            if (this.mSlotData[num2].Type != null)
            {
                goto Label_01C8;
            }
            this.AddSubUnitOverlay.get_transform().SetParent(this.UnitSlots[num2].get_transform(), 0);
            this.AddSubUnitOverlay.SetActive(1);
            goto Label_01E2;
        Label_01C8:
            num2 += 1;
        Label_01CC:
            if (num2 <= this.mCurrentParty.PartyData.SUBMEMBER_END)
            {
                goto Label_012D;
            }
        Label_01E2:
            return;
        }

        private void OnPrevTeamChange()
        {
            if (this.OnTeamChangeImpl(this.mCurrentTeamIndex - 1) == null)
            {
                goto Label_0035;
            }
            if ((this.TeamPulldown != null) == null)
            {
                goto Label_0035;
            }
            this.TeamPulldown.Selection = this.mCurrentTeamIndex;
        Label_0035:
            return;
        }

        private void OnRaidAccept(GameObject go)
        {
            if (this.mNumRaids > 0)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.LockWindow(1);
            if ((this.mRaidSettings != null) == null)
            {
                goto Label_0037;
            }
            this.mRaidSettings.Close();
            this.mRaidSettings = null;
        Label_0037:
            if ((this.mRaidResultWindow == null) == null)
            {
                goto Label_0064;
            }
            if (this.mReqRaidResultWindow != null)
            {
                goto Label_0064;
            }
            this.mReqRaidResultWindow = AssetManager.LoadAsync<RaidResultWindow>(this.RaidResultPrefab);
        Label_0064:
            if (this.IsPartyDirty == null)
            {
                goto Label_0083;
            }
            this.SaveParty(new Callback(this.<OnRaidAccept>m__384), null);
            return;
        Label_0083:
            this.StartRaid();
            return;
        }

        private void OnRaidClick(SRPG_Button button)
        {
            object[] objArray1;
            int num;
            ItemParam param;
            string str;
            if (this.mCurrentQuest != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.mCurrentQuest.GetChallangeLimit() <= 0) || (this.mCurrentQuest.GetChallangeLimit() > this.mCurrentQuest.GetChallangeCount()))
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            if (MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.mCurrentQuest.ticket) > 0)
            {
                goto Label_00C0;
            }
            param = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket);
            objArray1 = new object[] { (param == null) ? null : param.name };
            str = LocalizedText.Get("sys.NO_RAID_TICKET", objArray1);
            if (<>f__am$cache85 != null)
            {
                goto Label_00B1;
            }
            <>f__am$cache85 = new UIUtility.DialogResultEvent(PartyWindow2.<OnRaidClick>m__383);
        Label_00B1:
            UIUtility.NegativeSystemMessage(null, str, <>f__am$cache85, null, 0, -1);
            return;
        Label_00C0:
            if ((button == this.Raid) == null)
            {
                goto Label_00E8;
            }
            this.PrepareRaid(1, button.IsInteractable() == 0, 0);
            goto Label_00EE;
        Label_00E8:
            this.ShowRaidSettings();
        Label_00EE:
            return;
        }

        private unsafe void OnResetChallenge(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ReqBtlComResetResponse> response;
            Exception exception;
            string str;
            Network.EErrCode code;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_0022;
            }
            code = Network.ErrCode;
            FlowNode_Network.Failed();
            return;
        Label_0022:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ReqBtlComResetResponse>>(&www.text);
            if (response.body != null)
            {
                goto Label_0040;
            }
            FlowNode_Network.Failed();
            return;
        Label_0040:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                if (MonoSingleton<GameManager>.Instance.Deserialize(response.body.quests) != null)
                {
                    goto Label_0079;
                }
                FlowNode_Network.Failed();
                goto Label_00B2;
            Label_0079:
                goto Label_0094;
            }
            catch (Exception exception1)
            {
            Label_007E:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_00B2;
            }
        Label_0094:
            Network.RemoveAPI();
            this.RefreshRaidButtons();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
        Label_00B2:
            return;
        }

        private void OnSlotChange(GameObject go)
        {
            Animator animator;
            if ((go != null) == null)
            {
                goto Label_003B;
            }
            if (string.IsNullOrEmpty(this.SlotChangeTrigger) != null)
            {
                goto Label_003B;
            }
            animator = go.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_003B;
            }
            animator.SetTrigger(this.SlotChangeTrigger);
        Label_003B:
            return;
        }

        private void OnStaminaChange()
        {
            if ((this == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.RefreshRaidButtons();
            return;
        }

        private void OnSupportUnitSlotClick(GenericSlot slot, bool interactable)
        {
            if (this.mInitialized == null)
            {
                goto Label_0016;
            }
            if (this.mIsSaving == null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            this.Refresh(1);
            if ((this.FriendSlot == slot) == null)
            {
                goto Label_003C;
            }
            this.mUnitSlotSelected = 1;
            this.SupportList_Show();
        Label_003C:
            return;
        }

        private void OnTeamChange(int index)
        {
            this.OnTeamChangeImpl(index);
            return;
        }

        private bool OnTeamChangeImpl(int index)
        {
            int num;
            if (this.mCurrentTeamIndex != index)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            if (index >= this.mMaxTeamCount)
            {
                goto Label_0021;
            }
            if (index >= 0)
            {
                goto Label_0023;
            }
        Label_0021:
            return 0;
        Label_0023:
            this.ChangeEnabledArrowButtons(index, this.mMaxTeamCount);
            this.ChangeEnabledTeamTabs(index);
            this.mCurrentTeamIndex = index;
            this.mCurrentParty = this.mTeams[this.mCurrentTeamIndex];
            this.AssignUnits(this.mCurrentParty);
            num = 0;
            goto Label_0080;
        Label_0068:
            this.SetPartyUnit(num, this.mCurrentParty.Units[num]);
            num += 1;
        Label_0080:
            if (num < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_0068;
            }
            if (this.mCurrentPartyType != 10)
            {
                goto Label_010C;
            }
            this.mCurrentSupport = this.mSupports[this.mCurrentTeamIndex];
            this.SetSupport(this.mCurrentSupport);
            if (this.mCurrentSupport == null)
            {
                goto Label_0101;
            }
            if (this.mCurrentSupport.IsFriend() == null)
            {
                goto Label_00F1;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
            goto Label_00FC;
        Label_00F1:
            FlowNode_GameObject.ActivateOutputLinks(this, 210);
        Label_00FC:
            goto Label_010C;
        Label_0101:
            FlowNode_GameObject.ActivateOutputLinks(this, 220);
        Label_010C:
            this.OnPartyMemberChange();
            this.SaveTeamPresets();
            return 1;
        }

        public void OnTeamTabChange()
        {
            SerializeValueList list;
            int num;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            num = list.GetInt("tab_index");
            this.OnTeamChangeImpl(num);
            return;
        }

        private void OnUnitJobChange(long unitUniqueID)
        {
            this.Refresh(1);
            return;
        }

        private void OnUnitSlotClick(GenericSlot slot, bool interactable)
        {
            int num;
            if (this.mInitialized == null)
            {
                goto Label_0016;
            }
            if (this.mIsSaving == null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            this.Refresh(1);
            num = Array.IndexOf<GenericSlot>(this.UnitSlots, slot);
            if (0 > num)
            {
                goto Label_005D;
            }
            if (num >= this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_005D;
            }
            this.mUnitSlotSelected = 1;
            this.mSelectedSlotIndex = num;
            this.UnitList_Show();
            return;
        Label_005D:
            if (this.IsMultiTowerPartySlot(num) == null)
            {
                goto Label_007E;
            }
            this.mUnitSlotSelected = 1;
            this.mSelectedSlotIndex = num;
            this.UnitList_Show();
            return;
        Label_007E:
            return;
        }

        private void OpenMapEffectQuest()
        {
            Transform transform;
            GameObject obj2;
            MapEffectQuest quest;
            if (this.mCurrentQuest == null)
            {
                goto Label_003C;
            }
            if (this.mReqMapEffectQuest == null)
            {
                goto Label_003C;
            }
            if (this.mReqMapEffectQuest.isDone == null)
            {
                goto Label_003C;
            }
            if ((this.mReqMapEffectQuest.asset == null) == null)
            {
                goto Label_003D;
            }
        Label_003C:
            return;
        Label_003D:
            transform = this.TrHomeHeader;
            if (transform != null)
            {
                goto Label_0056;
            }
            transform = base.get_transform();
        Label_0056:
            obj2 = MapEffectQuest.CreateInstance(this.mReqMapEffectQuest.asset as GameObject, transform);
            if (obj2 != null)
            {
                goto Label_0079;
            }
            return;
        Label_0079:
            DataSource.Bind<QuestParam>(obj2, this.mCurrentQuest);
            obj2.SetActive(1);
            quest = obj2.GetComponent<MapEffectQuest>();
            if (quest == null)
            {
                goto Label_00A4;
            }
            quest.Setup();
        Label_00A4:
            return;
        }

        private void OpenQuestDetail()
        {
            GameObject obj2;
            QuestCampaignData[] dataArray;
            if (((this.mCurrentQuest == null) || (this.mReqQuestDetail == null)) || ((this.mReqQuestDetail.isDone == null) || ((this.mReqQuestDetail.asset != null) == null)))
            {
                goto Label_00C5;
            }
            obj2 = Object.Instantiate(this.mReqQuestDetail.asset) as GameObject;
            DataSource.Bind<QuestParam>(obj2, this.mCurrentQuest);
            if (((this.mGuestUnit == null) || (this.mGuestUnit.Count <= 0)) || (this.mCurrentPartyType != 6))
            {
                goto Label_0098;
            }
            DataSource.Bind<UnitData>(obj2, this.mGuestUnit[0]);
        Label_0098:
            dataArray = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
            DataSource.Bind<QuestCampaignData[]>(obj2, (((int) dataArray.Length) != null) ? dataArray : null);
            obj2.SetActive(1);
        Label_00C5:
            return;
        }

        private unsafe void OrganizeRecommendedParty(GlobalVars.RecommendType targetType, EElement targetElement)
        {
            List<List<UnitData>> list;
            PartySlotData data;
            List<PartySlotData>.Enumerator enumerator;
            List<UnitData> list2;
            int num;
            Comparison<UnitData> comparison;
            List<UnitData> list3;
            List<List<UnitData>>.Enumerator enumerator2;
            int num2;
            List<UnitData> list4;
            List<UnitData> list5;
            List<List<UnitData>>.Enumerator enumerator3;
            List<int> list6;
            int num3;
            int num4;
            IEnumerator<int> enumerator4;
            PartyData data2;
            int num5;
            int num6;
            <OrganizeRecommendedParty>c__AnonStorey377 storey;
            <OrganizeRecommendedParty>c__AnonStorey376 storey2;
            List<Comparison<UnitData>> list7;
            storey = new <OrganizeRecommendedParty>c__AnonStorey377();
            this.BreakupTeamImpl();
            storey2 = new <OrganizeRecommendedParty>c__AnonStorey376();
            storey2.removeTarget = new List<string>();
            enumerator = this.mSlotData.GetEnumerator();
        Label_002C:
            try
            {
                goto Label_0063;
            Label_0031:
                data = &enumerator.Current;
                if (data.Type == 2)
                {
                    goto Label_0051;
                }
                if (data.Type != 3)
                {
                    goto Label_0063;
                }
            Label_0051:
                storey2.removeTarget.Add(data.UnitName);
            Label_0063:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0031;
                }
                goto Label_0080;
            }
            finally
            {
            Label_0074:
                ((List<PartySlotData>.Enumerator) enumerator).Dispose();
            }
        Label_0080:
            list2 = Enumerable.ToList<UnitData>(Enumerable.Where<UnitData>(MonoSingleton<GameManager>.Instance.Player.Units, new Func<UnitData, bool>(storey2.<>m__3B1)));
            list = this.SeparateUnitByElement(list2, this.mCurrentQuest.units.GetList(), targetElement, PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest));
            list7 = new List<Comparison<UnitData>>();
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_Total));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_HP));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_Attack));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_Defense));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_Magic));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_Mind));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_Speed));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeSlash));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeStab));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeBlow));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeShot));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeMagic));
            list7.Add(new Comparison<UnitData>(PartyWindow2.CompareTo_AttackTypeNone));
            storey.targetComparators = list7;
            num = PartyUtility.RecommendTypeToComparatorOrder(targetType);
            if (num < 0)
            {
                goto Label_0228;
            }
            if (num >= storey.targetComparators.Count)
            {
                goto Label_0228;
            }
            comparison = storey.targetComparators[num];
            storey.targetComparators.RemoveAt(num);
            storey.targetComparators.Insert(0, comparison);
        Label_0228:
            enumerator2 = list.GetEnumerator();
        Label_0230:
            try
            {
                goto Label_0252;
            Label_0235:
                list3 = &enumerator2.Current;
                list3.Sort(new Comparison<UnitData>(storey.<>m__3B2));
            Label_0252:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0235;
                }
                goto Label_0270;
            }
            finally
            {
            Label_0263:
                ((List<List<UnitData>>.Enumerator) enumerator2).Dispose();
            }
        Label_0270:
            num2 = this.mCurrentParty.PartyData.MAX_UNIT;
            list4 = new List<UnitData>();
            enumerator3 = list.GetEnumerator();
        Label_0291:
            try
            {
                goto Label_033D;
            Label_0296:
                list5 = &enumerator3.Current;
                if (num2 > 0)
                {
                    goto Label_02AC;
                }
                goto Label_0349;
            Label_02AC:
                list6 = new List<int>();
                num3 = 0;
                goto Label_02EC;
            Label_02BB:
                list4.Add(list5[num3]);
                list6.Add(num3);
                if ((num2 -= 1) > 0)
                {
                    goto Label_02E6;
                }
                goto Label_02FA;
            Label_02E6:
                num3 += 1;
            Label_02EC:
                if (num3 < list5.Count)
                {
                    goto Label_02BB;
                }
            Label_02FA:
                enumerator4 = Enumerable.Reverse<int>(list6).GetEnumerator();
            Label_0308:
                try
                {
                    goto Label_031F;
                Label_030D:
                    num4 = enumerator4.Current;
                    list5.RemoveAt(num4);
                Label_031F:
                    if (enumerator4.MoveNext() != null)
                    {
                        goto Label_030D;
                    }
                    goto Label_033D;
                }
                finally
                {
                Label_0330:
                    if (enumerator4 != null)
                    {
                        goto Label_0335;
                    }
                Label_0335:
                    enumerator4.Dispose();
                }
            Label_033D:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0296;
                }
            Label_0349:
                goto Label_035B;
            }
            finally
            {
            Label_034E:
                ((List<List<UnitData>>.Enumerator) enumerator3).Dispose();
            }
        Label_035B:
            data2 = this.mCurrentParty.PartyData;
            num5 = 0;
            num6 = 0;
            goto Label_03A7;
        Label_0373:
            if (this.IsSettableSlot(this.mSlotData[num6]) == null)
            {
                goto Label_03A1;
            }
            this.SetPartyUnit(num6, list4[num5++]);
        Label_03A1:
            num6 += 1;
        Label_03A7:
            if (num6 >= data2.MAX_UNIT)
            {
                goto Label_03D5;
            }
            if (num6 >= this.mSlotData.Count)
            {
                goto Label_03D5;
            }
            if (num5 < list4.Count)
            {
                goto Label_0373;
            }
        Label_03D5:
            this.OnPartyMemberChange();
            this.SaveTeamPresets();
            return;
        }

        [DebuggerHidden]
        private IEnumerator PopulateItemList()
        {
            <PopulateItemList>c__Iterator12B iteratorb;
            iteratorb = new <PopulateItemList>c__Iterator12B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        protected virtual void PostForwardPressed()
        {
            GlobalEvent.Invoke("DISABLE_MAINMENU_TOP_COMMAND", null);
            GlobalVars.SelectedSupport.Set(this.mCurrentSupport);
            GlobalVars.SelectedFriendID = (this.mCurrentSupport == null) ? null : this.mCurrentSupport.FUID;
            if (this.PartyType != 9)
            {
                goto Label_0055;
            }
            this.SaveAndActivatePin(8);
            goto Label_005C;
        Label_0055:
            this.SaveAndActivatePin(1);
        Label_005C:
            return;
        }

        private bool PrepareRaid(int num, bool validateOnly, bool skipConfirm)
        {
            object[] objArray2;
            object[] objArray1;
            PlayerData data;
            int num2;
            ItemData data2;
            int num3;
            int num4;
            bool flag;
            ItemParam param;
            string str;
            string str2;
            data = MonoSingleton<GameManager>.Instance.Player;
            num2 = this.mCurrentQuest.RequiredApWithPlayerLv(data.Lv, 1);
            data2 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
            num3 = (data2 == null) ? 0 : data2.Num;
            this.mNumRaids = num;
            if (this.mCurrentQuest.GetChallangeLimit() <= 0)
            {
                goto Label_008C;
            }
            this.mNumRaids = Mathf.Min(this.mNumRaids, this.mCurrentQuest.GetChallangeLimit() - this.mCurrentQuest.GetChallangeCount());
        Label_008C:
            num4 = num2 * this.mNumRaids;
            if (data.Stamina >= num4)
            {
                goto Label_00B1;
            }
            MonoSingleton<GameManager>.Instance.StartBuyStaminaSequence(1, this);
            return 0;
        Label_00B1:
            if ((this.mCurrentQuest.IsQuestDrops == null) || ((QuestDropParam.Instance != null) == null))
            {
                goto Label_0126;
            }
            flag = QuestDropParam.Instance.IsChangedQuestDrops(this.mCurrentQuest);
            GlobalVars.SetDropTableGeneratedTime();
            if (flag == null)
            {
                goto Label_0126;
            }
            this.HardQuestDropPiecesUpdate();
            if (QuestDropParam.Instance.IsWarningPopupDisable != null)
            {
                goto Label_0126;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), new UIUtility.DialogResultEvent(this.<PrepareRaid>m__3AE), null, 0, -1);
            return 0;
        Label_0126:
            param = (data2 == null) ? MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket) : data2.Param;
            if (num3 >= this.mNumRaids)
            {
                goto Label_01A1;
            }
            objArray1 = new object[] { param.name };
            str = LocalizedText.Get("sys.NO_RAID_TICKET", objArray1);
            if (<>f__am$cacheA4 != null)
            {
                goto Label_0191;
            }
            <>f__am$cacheA4 = new UIUtility.DialogResultEvent(PartyWindow2.<PrepareRaid>m__3AF);
        Label_0191:
            UIUtility.NegativeSystemMessage(null, str, <>f__am$cacheA4, null, 0, -1);
            return 0;
        Label_01A1:
            this.mMultiRaidNum = num;
            if (validateOnly == null)
            {
                goto Label_01B0;
            }
            return 0;
        Label_01B0:
            if (skipConfirm == null)
            {
                goto Label_01BF;
            }
            this.OnRaidAccept(null);
            return 1;
        Label_01BF:
            objArray2 = new object[] { (int) this.mNumRaids, (int) num4, param.name };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_RAID", objArray2), null, new UIUtility.DialogResultEvent(this.OnRaidAccept), null, null, 0, -1);
            return 1;
        }

        public void PrevTeam()
        {
            this.OnPrevTeamChange();
            return;
        }

        public void RaidSettingsAccepted(RaidSettingsWindow window)
        {
            this.PrepareRaid(window.Count, 0, 1);
            return;
        }

        protected virtual unsafe void RecalcTotalAttack()
        {
            int num;
            num = PartyUtility.CalcTotalAttack(this.mCurrentParty, this.mLockedPartySlots, this.mOwnUnits, (this.mSupportLocked == null) ? this.mCurrentSupport : null, this.mGuestUnit);
            if ((this.TotalAtk != null) == null)
            {
                goto Label_0058;
            }
            this.TotalAtk.set_text(&num.ToString());
        Label_0058:
            return;
        }

        private unsafe void RecvRaidResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_ReqBtlComRaidResponse> response;
            GameManager manager;
            PlayerData data;
            int num;
            int num2;
            int num3;
            long num4;
            UnitData data2;
            UnitData data3;
            int num5;
            UnitData data4;
            UnitData data5;
            int num6;
            long num7;
            UnitData data6;
            UnitData data7;
            Exception exception;
            int num8;
            ConceptCardData data8;
            int num9;
            BattleCore.Json_BtlInfo info;
            int num10;
            RaidQuestResult result;
            int num11;
            ItemParam param;
            ConceptCardParam param2;
            int num12;
            int num13;
            UnitData.CharacterQuestParam param3;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0017;
            }
            code = Network.ErrCode;
            FlowNode_Network.Failed();
            return;
        Label_0017:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ReqBtlComRaidResponse>>(&www.text);
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            num = data.Exp;
            num2 = data.Lv;
            if (this.mRaidResult != null)
            {
                goto Label_0061;
            }
            this.mRaidResult = new RaidResult(SRPG_Extensions.ToPlayerPartyType(this.mCurrentPartyType));
        Label_0061:
            this.mRaidResult.quest = this.mCurrentQuest;
            this.mRaidResult.members.Clear();
            this.mRaidResult.results.Clear();
            num3 = this.mCurrentParty.PartyData.MAINMEMBER_START;
            goto Label_0128;
        Label_00A9:
            num4 = (this.mCurrentParty.Units[num3] == null) ? 0L : this.mCurrentParty.Units[num3].UniqueID;
            if (num4 > 0L)
            {
                goto Label_00E6;
            }
            goto Label_0122;
        Label_00E6:
            data2 = data.FindUnitDataByUniqueID(num4);
            if (data2 != null)
            {
                goto Label_00FC;
            }
            goto Label_0122;
        Label_00FC:
            data3 = new UnitData();
            data3.Setup(data2);
            data2 = data3;
            this.mRaidResult.members.Add(data2);
        Label_0122:
            num3 += 1;
        Label_0128:
            if (num3 <= this.mCurrentParty.PartyData.MAINMEMBER_END)
            {
                goto Label_00A9;
            }
            if (this.mCurrentQuest.units.IsNotNull() == null)
            {
                goto Label_01CA;
            }
            num5 = 0;
            goto Label_01B3;
        Label_015C:
            data4 = manager.Player.FindUnitDataByUnitID(this.mCurrentQuest.units.Get(num5));
            if (data4 != null)
            {
                goto Label_0187;
            }
            goto Label_01AD;
        Label_0187:
            data5 = new UnitData();
            data5.Setup(data4);
            data4 = data5;
            this.mRaidResult.members.Add(data4);
        Label_01AD:
            num5 += 1;
        Label_01B3:
            if (num5 < this.mCurrentQuest.units.Length)
            {
                goto Label_015C;
            }
        Label_01CA:
            num6 = this.mCurrentParty.PartyData.SUBMEMBER_START;
            goto Label_0260;
        Label_01E1:
            num7 = (this.mCurrentParty.Units[num6] == null) ? 0L : this.mCurrentParty.Units[num6].UniqueID;
            if (num7 > 0L)
            {
                goto Label_021E;
            }
            goto Label_025A;
        Label_021E:
            data6 = data.FindUnitDataByUniqueID(num7);
            if (data6 != null)
            {
                goto Label_0234;
            }
            goto Label_025A;
        Label_0234:
            data7 = new UnitData();
            data7.Setup(data6);
            data6 = data7;
            this.mRaidResult.members.Add(data6);
        Label_025A:
            num6 += 1;
        Label_0260:
            if (num6 <= this.mCurrentParty.PartyData.SUBMEMBER_END)
            {
                goto Label_01E1;
            }
        Label_0277:
            try
            {
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.items);
                manager.Deserialize(response.body.units);
                goto Label_02C7;
            }
            catch (Exception exception1)
            {
            Label_02AF:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_0719;
            }
        Label_02C7:
            Network.RemoveAPI();
            if (response.body.cards == null)
            {
                goto Label_0342;
            }
            num8 = 0;
            goto Label_032E;
        Label_02E4:
            GlobalVars.IsDirtyConceptCardData.Set(1);
            if (response.body.cards[num8].IsGetUnit == null)
            {
                goto Label_0328;
            }
            FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(response.body.cards[num8].iname));
        Label_0328:
            num8 += 1;
        Label_032E:
            if (num8 < ((int) response.body.cards.Length))
            {
                goto Label_02E4;
            }
        Label_0342:
            if (response.body.btlinfos == null)
            {
                goto Label_059C;
            }
            num9 = 0;
            goto Label_0588;
        Label_035A:
            info = response.body.btlinfos[num9];
            num10 = (info.drops == null) ? 0 : ((int) info.drops.Length);
            result = new RaidQuestResult();
            result.index = num9;
            result.pexp = this.mCurrentQuest.pexp;
            result.uexp = this.mCurrentQuest.uexp;
            result.gold = this.mCurrentQuest.gold;
            result.drops = new QuestResult.DropItemData[num10];
            if (info.drops == null)
            {
                goto Label_055E;
            }
            num11 = 0;
            goto Label_054E;
        Label_03EF:
            param = null;
            param2 = null;
            if (info.drops[num11].dropItemType != null)
            {
                goto Label_040E;
            }
            goto Label_0548;
        Label_040E:
            if (info.drops[num11].dropItemType != 2)
            {
                goto Label_0443;
            }
            param = MonoSingleton<GameManager>.Instance.GetItemParam(info.drops[num11].iname);
            goto Label_04AA;
        Label_0443:
            if (info.drops[num11].dropItemType != 3)
            {
                goto Label_047D;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(info.drops[num11].iname);
            goto Label_04AA;
        Label_047D:
            DebugUtility.LogError(string.Format("不明なドロップ品が登録されています。iname:{0} (itype:{1})", info.drops[num11].iname, info.drops[num11].itype));
        Label_04AA:
            if (param == null)
            {
                goto Label_04F6;
            }
            result.drops[num11] = new QuestResult.DropItemData();
            result.drops[num11].SetupDropItemData(2, (long) num11, info.drops[num11].iname, info.drops[num11].num);
            goto Label_0548;
        Label_04F6:
            if (param2 == null)
            {
                goto Label_0548;
            }
            GlobalVars.IsDirtyConceptCardData.Set(1);
            result.drops[num11] = new QuestResult.DropItemData();
            result.drops[num11].SetupDropItemData(3, (long) num11, info.drops[num11].iname, info.drops[num11].num);
        Label_0548:
            num11 += 1;
        Label_054E:
            if (num11 < ((int) info.drops.Length))
            {
                goto Label_03EF;
            }
        Label_055E:
            this.mRaidResult.campaignIds = info.campaigns;
            this.mRaidResult.results.Add(result);
            num9 += 1;
        Label_0588:
            if (num9 < ((int) response.body.btlinfos.Length))
            {
                goto Label_035A;
            }
        Label_059C:
            if (data.Lv <= num2)
            {
                goto Label_05B8;
            }
            data.OnPlayerLevelChange(data.Lv - num2);
        Label_05B8:
            num12 = 0;
            goto Label_05D8;
        Label_05C0:
            data.OnQuestWin(this.mCurrentQuest.iname, null);
            num12 += 1;
        Label_05D8:
            if (num12 < ((int) response.body.btlinfos.Length))
            {
                goto Label_05C0;
            }
            this.mRaidResult.pexp = data.Exp - num;
            this.mRaidResult.uexp = this.mCurrentQuest.uexp * this.mNumRaids;
            this.mRaidResult.gold = this.mCurrentQuest.gold * this.mNumRaids;
            data.OnGoldChange(this.mRaidResult.gold);
            this.mRaidResult.chquest = new QuestParam[this.mRaidResult.members.Count];
            num13 = 0;
            goto Label_06AD;
        Label_0672:
            param3 = this.mRaidResult.members[num13].GetCurrentCharaEpisodeData();
            if (param3 == null)
            {
                goto Label_06A7;
            }
            this.mRaidResult.chquest[num13] = param3.Param;
        Label_06A7:
            num13 += 1;
        Label_06AD:
            if (num13 < this.mRaidResult.members.Count)
            {
                goto Label_0672;
            }
            GlobalVars.RaidResult = this.mRaidResult;
            GlobalVars.PlayerExpOld.Set(num);
            GlobalVars.PlayerExpNew.Set(data.Exp);
            GlobalVars.PlayerLevelChanged.Set((data.Lv == num2) == 0);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(-1);
            base.StartCoroutine(this.ShowRaidResultAsync());
        Label_0719:
            return;
        }

        public void Refresh(bool keepTeam)
        {
            UnitData local1;
            int num;
            RectTransform transform;
            PlayerData data;
            PlayerPartyTypes types;
            int num2;
            UnitData data2;
            int num3;
            int num4;
            int num5;
            PartySlotData data3;
            UnitParam param;
            BackHandler handler;
            bool flag;
            if (this.UseQuestInfo != null)
            {
                goto Label_0013;
            }
            this.RefreshNoneQuestInfo(keepTeam);
            return;
        Label_0013:
            this.mIsHeloOnly = this.IsHeroSoloParty();
            if ((this.QuestInfo != null) == null)
            {
                goto Label_0117;
            }
            if (this.ShowQuestInfo == null)
            {
                goto Label_0068;
            }
            DataSource.Bind<QuestParam>(this.QuestInfo, this.mCurrentQuest);
            GameParameter.UpdateAll(this.QuestInfo);
            this.QuestInfo.SetActive(1);
            goto Label_0074;
        Label_0068:
            this.QuestInfo.SetActive(0);
        Label_0074:
            if ((this.Prefab_SankaFuka != null) == null)
            {
                goto Label_0117;
            }
            num = 0;
            goto Label_00FB;
        Label_008C:
            if (((this.mSankaFukaIcons[num] == null) == null) || ((this.UnitSlots[num] != null) == null))
            {
                goto Label_00F7;
            }
            this.mSankaFukaIcons[num] = Object.Instantiate<GameObject>(this.Prefab_SankaFuka);
            transform = this.mSankaFukaIcons[num].get_transform() as RectTransform;
            transform.set_anchoredPosition(Vector2.get_zero());
            transform.SetParent(this.UnitSlots[num].get_transform(), 0);
        Label_00F7:
            num += 1;
        Label_00FB:
            if ((num < ((int) this.mSankaFukaIcons.Length)) && (num < ((int) this.UnitSlots.Length)))
            {
                goto Label_008C;
            }
        Label_0117:
            if (this.PartyType != null)
            {
                goto Label_016B;
            }
            this.mCurrentPartyType = 0;
            if (this.mCurrentQuest != null)
            {
                goto Label_0143;
            }
            DebugUtility.LogError("Quest not selected");
            goto Label_0154;
        Label_0143:
            this.mCurrentPartyType = PartyUtility.GetEditPartyTypes(this.mCurrentQuest);
        Label_0154:
            if (this.mCurrentPartyType != null)
            {
                goto Label_0177;
            }
            this.mCurrentPartyType = 1;
            goto Label_0177;
        Label_016B:
            this.mCurrentPartyType = this.PartyType;
        Label_0177:
            data = MonoSingleton<GameManager>.Instance.Player;
            types = SRPG_Extensions.ToPlayerPartyType(this.mCurrentPartyType);
            if (Array.IndexOf<PlayerPartyTypes>(this.SaveJobs, types) < 0)
            {
                goto Label_0231;
            }
            if (types != 4)
            {
                goto Label_01B7;
            }
            ArenaDefenceUnits.CompleteLoading();
            this.RefreshArenaDefUnits();
            goto Label_022C;
        Label_01B7:
            this.mOwnUnits = new List<UnitData>(data.Units.Count);
            num2 = 0;
            goto Label_021A;
        Label_01D5:
            data2 = new UnitData();
            data2.Setup(data.Units[num2]);
            data2.TempFlags |= 3;
            data2.SetJob(types);
            this.mOwnUnits.Add(data2);
            num2 += 1;
        Label_021A:
            if (num2 < data.Units.Count)
            {
                goto Label_01D5;
            }
        Label_022C:
            goto Label_027C;
        Label_0231:
            this.mOwnUnits = new List<UnitData>(data.Units);
            num3 = 0;
            goto Label_026A;
        Label_024A:
            local1 = this.mOwnUnits[num3];
            local1.TempFlags |= 2;
            num3 += 1;
        Label_026A:
            if (num3 < this.mOwnUnits.Count)
            {
                goto Label_024A;
            }
        Label_027C:
            DataSource.Bind<PlayerPartyTypes>(base.get_gameObject(), types);
            if (keepTeam != null)
            {
                goto Label_0294;
            }
            this.LoadTeam();
        Label_0294:
            num4 = 0;
            num5 = 0;
            goto Label_03A5;
        Label_029F:
            if ((this.UnitSlots[num5] != null) == null)
            {
                goto Label_039F;
            }
            this.UnitSlots[num5].SetSlotData<QuestParam>(this.mCurrentQuest);
            data3 = this.mSlotData[num5];
            if (data3.Type != 3)
            {
                goto Label_0331;
            }
            if ((this.mGuestUnit == null) || (num4 >= this.mGuestUnit.Count))
            {
                goto Label_0326;
            }
            this.UnitSlots[num5].SetSlotData<UnitData>(PartyUtility.FindUnit(this.mGuestUnit[num4], this.mOwnUnits));
        Label_0326:
            num4 += 1;
            goto Label_039F;
        Label_0331:
            if ((data3.Type != 4) && (data3.Type != 5))
            {
                goto Label_0378;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(data3.UnitName);
            this.UnitSlots[num5].SetSlotData<UnitParam>(param);
            goto Label_039F;
        Label_0378:
            this.UnitSlots[num5].SetSlotData<UnitData>(PartyUtility.FindUnit(this.mCurrentParty.Units[num5], this.mOwnUnits));
        Label_039F:
            num5 += 1;
        Label_03A5:
            if (((num5 < ((int) this.UnitSlots.Length)) && (num5 < ((int) this.mCurrentParty.Units.Length))) && (num5 < this.mSlotData.Count))
            {
                goto Label_029F;
            }
            if ((this.FriendSlot != null) == null)
            {
                goto Label_0456;
            }
            if ((this.mSupportSlotData == null) || (this.mSupportSlotData.Type == null))
            {
                goto Label_040D;
            }
            this.mCurrentSupport = null;
        Label_040D:
            this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
            this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
            this.FriendSlot.SetSlotData<UnitData>((this.mCurrentSupport == null) ? null : this.mCurrentSupport.Unit);
        Label_0456:
            if (((this.mCurrentPartyType != 6) || ((this.QuestUnitCond != null) == null)) || ((this.mGuestUnit == null) || (this.mGuestUnit.Count <= 0)))
            {
                goto Label_04B0;
            }
            DataSource.Bind<UnitData>(this.QuestUnitCond, this.mGuestUnit[0]);
            GameParameter.UpdateValuesOfType(0x1a5);
        Label_04B0:
            if ((this.ForwardButton != null) == null)
            {
                goto Label_0501;
            }
            this.ForwardButton.get_gameObject().SetActive(this.ShowForwardButton);
            handler = this.ForwardButton.GetComponent<BackHandler>();
            if ((handler != null) == null)
            {
                goto Label_0501;
            }
            handler.set_enabled(this.ShowBackButton == 0);
        Label_0501:
            if ((this.BackButton != null) == null)
            {
                goto Label_0528;
            }
            this.BackButton.get_gameObject().SetActive(this.ShowBackButton);
        Label_0528:
            if ((this.GetPartyCondType() != 2) && (this.IsFixedParty() == null))
            {
                goto Label_064A;
            }
            if ((this.RecommendTeamButton != null) == null)
            {
                goto Label_056D;
            }
            this.RecommendTeamButton.get_gameObject().SetActive(1);
            this.RecommendTeamButton.set_interactable(0);
        Label_056D:
            if ((this.BreakupButton != null) == null)
            {
                goto Label_059B;
            }
            this.BreakupButton.get_gameObject().SetActive(1);
            this.BreakupButton.set_interactable(0);
        Label_059B:
            if ((this.RenameButton != null) == null)
            {
                goto Label_05BD;
            }
            this.RenameButton.get_gameObject().SetActive(0);
        Label_05BD:
            if ((this.PrevButton != null) == null)
            {
                goto Label_05DF;
            }
            this.PrevButton.get_gameObject().SetActive(0);
        Label_05DF:
            if ((this.NextButton != null) == null)
            {
                goto Label_0601;
            }
            this.NextButton.get_gameObject().SetActive(0);
        Label_0601:
            if ((this.TeamPulldown != null) == null)
            {
                goto Label_0623;
            }
            this.TeamPulldown.get_gameObject().SetActive(0);
        Label_0623:
            if ((this.TextFixParty != null) == null)
            {
                goto Label_07F4;
            }
            this.TextFixParty.get_gameObject().SetActive(1);
            goto Label_07F4;
        Label_064A:
            flag = (((this.mCurrentPartyType == 1) || (this.mCurrentPartyType == 6)) || ((this.mCurrentPartyType == 2) || (this.mCurrentPartyType == 7))) ? 1 : (this.mCurrentPartyType == 10);
            if ((this.RecommendTeamButton != null) == null)
            {
                goto Label_06B8;
            }
            this.RecommendTeamButton.get_gameObject().SetActive(flag);
            this.RecommendTeamButton.set_interactable(1);
        Label_06B8:
            if ((this.BreakupButton != null) == null)
            {
                goto Label_06E7;
            }
            this.BreakupButton.get_gameObject().SetActive(flag);
            this.BreakupButton.set_interactable(1);
        Label_06E7:
            if ((this.RenameButton != null) == null)
            {
                goto Label_0737;
            }
            if (this.mCurrentPartyType != 2)
            {
                goto Label_0725;
            }
            if (this.ContainsNotFree() == null)
            {
                goto Label_0725;
            }
            this.RenameButton.get_gameObject().SetActive(0);
            goto Label_0737;
        Label_0725:
            this.RenameButton.get_gameObject().SetActive(flag);
        Label_0737:
            if ((this.PrevButton != null) == null)
            {
                goto Label_075A;
            }
            this.PrevButton.get_gameObject().SetActive(flag);
        Label_075A:
            if ((this.NextButton != null) == null)
            {
                goto Label_077D;
            }
            this.NextButton.get_gameObject().SetActive(flag);
        Label_077D:
            if ((this.RecentTeamButton != null) == null)
            {
                goto Label_07D2;
            }
            if (this.mCurrentQuest == null)
            {
                goto Label_07C0;
            }
            if (this.mCurrentQuest.type != 3)
            {
                goto Label_07C0;
            }
            this.RecentTeamButton.get_gameObject().SetActive(0);
            goto Label_07D2;
        Label_07C0:
            this.RecentTeamButton.get_gameObject().SetActive(flag);
        Label_07D2:
            if ((this.TextFixParty != null) == null)
            {
                goto Label_07F4;
            }
            this.TextFixParty.get_gameObject().SetActive(0);
        Label_07F4:
            this.ToggleRaidInfo();
            this.RefreshRaidTicketNum();
            this.RefreshRaidButtons();
            this.LockSlots();
            this.OnPartyMemberChange();
            this.LoadInventory();
            return;
        }

        public void RefreshArenaDefUnits()
        {
            UnitData local2;
            UnitData local1;
            int num;
            PlayerData data;
            int num2;
            int num3;
            int num4;
            if (this.mCurrentParty != null)
            {
                goto Label_0055;
            }
            this.mOwnUnits = new List<UnitData>(ArenaDefenceUnits.mArenaDefUnits);
            num = 0;
            goto Label_003F;
        Label_0022:
            local1 = this.mOwnUnits[num];
            local1.TempFlags |= 3;
            num += 1;
        Label_003F:
            if (num < this.mOwnUnits.Count)
            {
                goto Label_0022;
            }
            goto Label_012D;
        Label_0055:
            data = MonoSingleton<GameManager>.Instance.Player;
            this.mOwnUnits = new List<UnitData>(ArenaDefenceUnits.mArenaDefUnits);
            num2 = 3;
            num3 = 0;
            goto Label_011C;
        Label_0079:
            num4 = 0;
            goto Label_0110;
        Label_0081:
            if (this.mCurrentParty.Units[num4] == null)
            {
                goto Label_010A;
            }
            if ((this.mOwnUnits[num3].UnitID == this.mCurrentParty.Units[num4].UnitID) == null)
            {
                goto Label_010A;
            }
            this.mOwnUnits[num3].Setup(data.Units[num3]);
            local2 = this.mOwnUnits[num3];
            local2.TempFlags |= 3;
            this.mOwnUnits[num3].SetJob(4);
        Label_010A:
            num4 += 1;
        Label_0110:
            if (num4 < num2)
            {
                goto Label_0081;
            }
            num3 += 1;
        Label_011C:
            if (num3 < this.mOwnUnits.Count)
            {
                goto Label_0079;
            }
        Label_012D:
            return;
        }

        private void RefreshItemList()
        {
            List<ItemData> list;
            int num;
            PlayerData data;
            int num2;
            ItemData data2;
            int num3;
            int num4;
            int num5;
            int num6;
            RectTransform transform;
            int num7;
            int num8;
            RectTransform transform2;
            <RefreshItemList>c__AnonStorey36A storeya;
            ItemFilterTypes types;
            <RefreshItemList>c__AnonStorey36B storeyb;
            storeya = new <RefreshItemList>c__AnonStorey36A();
            storeya.<>f__this = this;
            storeya.currentItem = this.mCurrentItems[this.mSelectedSlotIndex];
            this.ItemList.ClearItems();
            if (storeya.currentItem != null)
            {
                goto Label_0045;
            }
            if (this.AlwaysShowRemoveItem == null)
            {
                goto Label_0051;
            }
        Label_0045:
            this.ItemList.AddItem(0);
        Label_0051:
            list = new List<ItemData>(this.mOwnItems);
            num = 0;
            goto Label_0095;
        Label_0064:
            if (list[num].ItemType != null)
            {
                goto Label_0086;
            }
            if (list[num].Skill != null)
            {
                goto Label_0091;
            }
        Label_0086:
            list.RemoveAt(num--);
        Label_0091:
            num += 1;
        Label_0095:
            if (num < list.Count)
            {
                goto Label_0064;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            num2 = 0;
            goto Label_00FC;
        Label_00B3:
            if (this.mCurrentItems[num2] == null)
            {
                goto Label_00F8;
            }
            data2 = data.FindItemDataByItemParam(this.mCurrentItems[num2].Param);
            this.ItemList.AddItem(this.mOwnItems.IndexOf(data2) + 1);
            list.Remove(data2);
        Label_00F8:
            num2 += 1;
        Label_00FC:
            if (num2 < ((int) this.mCurrentItems.Length))
            {
                goto Label_00B3;
            }
            switch (this.mItemFilter)
            {
                case 0:
                    goto Label_012A;

                case 1:
                    goto Label_018E;

                case 2:
                    goto Label_026D;
            }
            goto Label_037D;
        Label_012A:
            num3 = 0;
            goto Label_017C;
        Label_0132:
            if (list[num3].ItemType != null)
            {
                goto Label_0176;
            }
            if (list[num3].Skill == null)
            {
                goto Label_0176;
            }
            this.ItemList.AddItem(this.mOwnItems.IndexOf(list[num3]) + 1);
        Label_0176:
            num3 += 1;
        Label_017C:
            if (num3 < list.Count)
            {
                goto Label_0132;
            }
            goto Label_037D;
        Label_018E:
            num4 = 0;
            goto Label_025B;
        Label_0196:
            if (list[num4].ItemType != null)
            {
                goto Label_0255;
            }
            if (list[num4].Skill == null)
            {
                goto Label_0255;
            }
            if (list[num4].Skill.EffectType == 2)
            {
                goto Label_0235;
            }
            if (list[num4].Skill.EffectType == 6)
            {
                goto Label_0235;
            }
            if (list[num4].Skill.EffectType == 11)
            {
                goto Label_0235;
            }
            if (list[num4].Skill.EffectType == 20)
            {
                goto Label_0235;
            }
            if (list[num4].Skill.EffectType != 0x1c)
            {
                goto Label_0255;
            }
        Label_0235:
            this.ItemList.AddItem(this.mOwnItems.IndexOf(list[num4]) + 1);
        Label_0255:
            num4 += 1;
        Label_025B:
            if (num4 < list.Count)
            {
                goto Label_0196;
            }
            goto Label_037D;
        Label_026D:
            num5 = 0;
            goto Label_036B;
        Label_0275:
            if (list[num5].ItemType != null)
            {
                goto Label_0365;
            }
            if (list[num5].Skill == null)
            {
                goto Label_0365;
            }
            if (list[num5].Skill.EffectType == 4)
            {
                goto Label_0345;
            }
            if (list[num5].Skill.EffectType == 0x13)
            {
                goto Label_0345;
            }
            if (list[num5].Skill.EffectType == 5)
            {
                goto Label_0345;
            }
            if (list[num5].Skill.EffectType == 12)
            {
                goto Label_0345;
            }
            if (list[num5].Skill.EffectType == 13)
            {
                goto Label_0345;
            }
            if (list[num5].Skill.EffectType == 7)
            {
                goto Label_0345;
            }
            if (list[num5].Skill.EffectType != 15)
            {
                goto Label_0365;
            }
        Label_0345:
            this.ItemList.AddItem(this.mOwnItems.IndexOf(list[num5]) + 1);
        Label_0365:
            num5 += 1;
        Label_036B:
            if (num5 < list.Count)
            {
                goto Label_0275;
            }
        Label_037D:
            this.ItemList.Refresh(1);
            if ((this.ItemListHilit != null) == null)
            {
                goto Label_041D;
            }
            this.ItemListHilit.get_gameObject().SetActive(0);
            this.ItemListHilit.SetParent(base.get_transform(), 0);
            if (storeya.currentItem == null)
            {
                goto Label_041D;
            }
            num6 = this.mOwnItems.FindIndex(new Predicate<ItemData>(storeya.<>m__37C)) + 1;
            if (num6 <= 0)
            {
                goto Label_041D;
            }
            transform = this.ItemList.FindItem(num6);
            if ((transform != null) == null)
            {
                goto Label_041D;
            }
            this.AttachAndEnable(this.ItemListHilit, transform, this.ItemListHilitParent);
        Label_041D:
            num7 = 0;
            goto Label_0467;
        Label_0425:
            if ((this.ChosenItemBadges[num7] != null) == null)
            {
                goto Label_0461;
            }
            this.ChosenItemBadges[num7].SetParent(UIUtility.Pool, 0);
            this.ChosenItemBadges[num7].get_gameObject().SetActive(0);
        Label_0461:
            num7 += 1;
        Label_0467:
            if (num7 < ((int) this.ChosenItemBadges.Length))
            {
                goto Label_0425;
            }
            storeyb = new <RefreshItemList>c__AnonStorey36B();
            storeyb.<>f__this = this;
            storeyb.i = 0;
            goto Label_051C;
        Label_0492:
            if (this.mCurrentItems[storeyb.i] == null)
            {
                goto Label_050C;
            }
            num8 = this.mOwnItems.FindIndex(new Predicate<ItemData>(storeyb.<>m__37D)) + 1;
            transform2 = this.ItemList.FindItem(num8);
            if ((transform2 != null) == null)
            {
                goto Label_050C;
            }
            this.ChosenItemBadges[storeyb.i].SetParent(transform2, 0);
            this.ChosenItemBadges[storeyb.i].get_gameObject().SetActive(1);
        Label_050C:
            storeyb.i += 1;
        Label_051C:
            if (storeyb.i < ((int) this.ChosenItemBadges.Length))
            {
                goto Label_0492;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 20);
            return;
        }

        protected void RefreshNoneQuestInfo(bool keepTeam)
        {
            UnitData local1;
            int num;
            RectTransform transform;
            PlayerData data;
            PlayerPartyTypes types;
            int num2;
            UnitData data2;
            int num3;
            int num4;
            int num5;
            PartySlotData data3;
            UnitParam param;
            BackHandler handler;
            bool flag;
            this.mIsHeloOnly = 0;
            if ((this.Prefab_SankaFuka != null) == null)
            {
                goto Label_00AA;
            }
            num = 1;
            goto Label_008E;
        Label_001F:
            if (((this.mSankaFukaIcons[num] == null) == null) || ((this.UnitSlots[num] != null) == null))
            {
                goto Label_008A;
            }
            this.mSankaFukaIcons[num] = Object.Instantiate<GameObject>(this.Prefab_SankaFuka);
            transform = this.mSankaFukaIcons[num].get_transform() as RectTransform;
            transform.set_anchoredPosition(Vector2.get_zero());
            transform.SetParent(this.UnitSlots[num].get_transform(), 0);
        Label_008A:
            num += 1;
        Label_008E:
            if ((num < ((int) this.mSankaFukaIcons.Length)) && (num < ((int) this.UnitSlots.Length)))
            {
                goto Label_001F;
            }
        Label_00AA:
            this.mCurrentPartyType = this.PartyType;
            data = MonoSingleton<GameManager>.Instance.Player;
            types = SRPG_Extensions.ToPlayerPartyType(this.mCurrentPartyType);
            if (Array.IndexOf<PlayerPartyTypes>(this.SaveJobs, types) < 0)
            {
                goto Label_0159;
            }
            this.mOwnUnits = new List<UnitData>(data.Units.Count);
            num2 = 0;
            goto Label_0142;
        Label_00FD:
            data2 = new UnitData();
            data2.Setup(data.Units[num2]);
            data2.TempFlags |= 3;
            data2.SetJob(types);
            this.mOwnUnits.Add(data2);
            num2 += 1;
        Label_0142:
            if (num2 < data.Units.Count)
            {
                goto Label_00FD;
            }
            goto Label_01A4;
        Label_0159:
            this.mOwnUnits = new List<UnitData>(data.Units);
            num3 = 0;
            goto Label_0192;
        Label_0172:
            local1 = this.mOwnUnits[num3];
            local1.TempFlags |= 2;
            num3 += 1;
        Label_0192:
            if (num3 < this.mOwnUnits.Count)
            {
                goto Label_0172;
            }
        Label_01A4:
            DataSource.Bind<PlayerPartyTypes>(base.get_gameObject(), types);
            if (keepTeam != null)
            {
                goto Label_01BC;
            }
            this.LoadTeam();
        Label_01BC:
            num4 = 0;
            num5 = 0;
            goto Label_02CD;
        Label_01C7:
            if ((this.UnitSlots[num5] != null) == null)
            {
                goto Label_02C7;
            }
            this.UnitSlots[num5].SetSlotData<QuestParam>(this.mCurrentQuest);
            data3 = this.mSlotData[num5];
            if (data3.Type != 3)
            {
                goto Label_0259;
            }
            if ((this.mGuestUnit == null) || (num4 >= this.mGuestUnit.Count))
            {
                goto Label_024E;
            }
            this.UnitSlots[num5].SetSlotData<UnitData>(PartyUtility.FindUnit(this.mGuestUnit[num4], this.mOwnUnits));
        Label_024E:
            num4 += 1;
            goto Label_02C7;
        Label_0259:
            if ((data3.Type != 4) && (data3.Type != 5))
            {
                goto Label_02A0;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(data3.UnitName);
            this.UnitSlots[num5].SetSlotData<UnitParam>(param);
            goto Label_02C7;
        Label_02A0:
            this.UnitSlots[num5].SetSlotData<UnitData>(PartyUtility.FindUnit(this.mCurrentParty.Units[num5], this.mOwnUnits));
        Label_02C7:
            num5 += 1;
        Label_02CD:
            if (((num5 < ((int) this.UnitSlots.Length)) && (num5 < ((int) this.mCurrentParty.Units.Length))) && (num5 < this.mSlotData.Count))
            {
                goto Label_01C7;
            }
            if ((this.FriendSlot != null) == null)
            {
                goto Label_037E;
            }
            if ((this.mSupportSlotData == null) || (this.mSupportSlotData.Type == null))
            {
                goto Label_0335;
            }
            this.mCurrentSupport = null;
        Label_0335:
            this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
            this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
            this.FriendSlot.SetSlotData<UnitData>((this.mCurrentSupport == null) ? null : this.mCurrentSupport.Unit);
        Label_037E:
            if ((this.QuestInfo != null) == null)
            {
                goto Label_039B;
            }
            this.QuestInfo.SetActive(0);
        Label_039B:
            if ((this.ForwardButton != null) == null)
            {
                goto Label_03EC;
            }
            this.ForwardButton.get_gameObject().SetActive(this.ShowForwardButton);
            handler = this.ForwardButton.GetComponent<BackHandler>();
            if ((handler != null) == null)
            {
                goto Label_03EC;
            }
            handler.set_enabled(this.ShowBackButton == 0);
        Label_03EC:
            if ((this.BackButton != null) == null)
            {
                goto Label_0413;
            }
            this.BackButton.get_gameObject().SetActive(this.ShowBackButton);
        Label_0413:
            flag = (((this.mCurrentPartyType == 1) || (this.mCurrentPartyType == 6)) || (this.mCurrentPartyType == 2)) ? 1 : (this.mCurrentPartyType == 7);
            if ((this.RecommendTeamButton != null) == null)
            {
                goto Label_0473;
            }
            this.RecommendTeamButton.get_gameObject().SetActive(1);
            this.RecommendTeamButton.set_interactable(0);
        Label_0473:
            if ((this.BreakupButton != null) == null)
            {
                goto Label_04A1;
            }
            this.BreakupButton.get_gameObject().SetActive(1);
            this.BreakupButton.set_interactable(0);
        Label_04A1:
            if ((this.RenameButton != null) == null)
            {
                goto Label_04C4;
            }
            this.RenameButton.get_gameObject().SetActive(flag);
        Label_04C4:
            if ((this.PrevButton != null) == null)
            {
                goto Label_04E7;
            }
            this.PrevButton.get_gameObject().SetActive(flag);
        Label_04E7:
            if ((this.NextButton != null) == null)
            {
                goto Label_050A;
            }
            this.NextButton.get_gameObject().SetActive(flag);
        Label_050A:
            if ((this.RecentTeamButton != null) == null)
            {
                goto Label_055F;
            }
            if (this.mCurrentQuest == null)
            {
                goto Label_054D;
            }
            if (this.mCurrentQuest.type != 3)
            {
                goto Label_054D;
            }
            this.RecentTeamButton.get_gameObject().SetActive(0);
            goto Label_055F;
        Label_054D:
            this.RecentTeamButton.get_gameObject().SetActive(flag);
        Label_055F:
            if ((this.TextFixParty != null) == null)
            {
                goto Label_0581;
            }
            this.TextFixParty.get_gameObject().SetActive(0);
        Label_0581:
            if ((this.BattleSettingButton != null) == null)
            {
                goto Label_05A3;
            }
            this.BattleSettingButton.get_gameObject().SetActive(0);
        Label_05A3:
            if ((this.HelpButton != null) == null)
            {
                goto Label_05C5;
            }
            this.HelpButton.get_gameObject().SetActive(0);
        Label_05C5:
            if ((this.Filter != null) == null)
            {
                goto Label_05E2;
            }
            this.Filter.SetActive(1);
        Label_05E2:
            this.ToggleRaidInfo();
            this.RefreshRaidTicketNum();
            this.RefreshRaidButtons();
            this.LockSlots();
            this.OnPartyMemberChange();
            this.LoadInventory();
            return;
        }

        protected void RefreshQuest()
        {
            QuestParam param;
            QuestCampaignData[] dataArray;
            if (this.UseQuestInfo != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            param = this.mCurrentQuest;
            this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            if (this.mCurrentQuest == param)
            {
                goto Label_003B;
            }
            this.mMultiRaidNum = -1;
        Label_003B:
            DataSource.Bind<QuestParam>(base.get_gameObject(), this.mCurrentQuest);
            dataArray = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(this.mCurrentQuest);
            DataSource.Bind<QuestCampaignData[]>(base.get_gameObject(), (((int) dataArray.Length) != null) ? dataArray : null);
            if ((this.QuestCampaigns != null) == null)
            {
                goto Label_00AE;
            }
            if ((this.QuestCampaigns.GetQuestCampaignList != null) == null)
            {
                goto Label_00AE;
            }
            this.QuestCampaigns.GetQuestCampaignList.RefreshIcons();
        Label_00AE:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void RefreshRaidButtons()
        {
            object[] objArray1;
            int[] numArray1;
            PlayerData data;
            int num;
            ItemData data2;
            ItemParam param;
            int num2;
            int num3;
            int num4;
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            data2 = null;
            param = null;
            if (this.mCurrentQuest == null)
            {
                goto Label_00B9;
            }
            num = this.mCurrentQuest.RequiredApWithPlayerLv(data.Lv, 1);
            data2 = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
            if (data2 == null)
            {
                goto Label_005C;
            }
            param = data2.Param;
            goto Label_0087;
        Label_005C:
            if (string.IsNullOrEmpty(this.mCurrentQuest.ticket) != null)
            {
                goto Label_0087;
            }
            param = MonoSingleton<GameManager>.Instance.GetItemParam(this.mCurrentQuest.ticket);
        Label_0087:
            if ((this.RaidInfo != null) == null)
            {
                goto Label_00B9;
            }
            DataSource.Bind<ItemParam>(this.RaidInfo.get_gameObject(), param);
            GameParameter.UpdateAll(this.RaidInfo.get_gameObject());
        Label_00B9:
            num2 = 0;
            if (data2 == null)
            {
                goto Label_00CA;
            }
            num2 = data2.Num;
        Label_00CA:
            if (this.mCurrentQuest == null)
            {
                goto Label_01E7;
            }
            num3 = this.MaxRaidNum;
            num4 = this.MaxRaidNum;
            if (num <= 0)
            {
                goto Label_0101;
            }
            num3 = Mathf.Min(data.Stamina / num, this.MaxRaidNum);
        Label_0101:
            num3 = Mathf.Min(num2, num3);
            if (this.mCurrentQuest.GetChallangeLimit() <= 0)
            {
                goto Label_013D;
            }
            num4 = Mathf.Min(num4, this.mCurrentQuest.GetChallangeLimit() - this.mCurrentQuest.GetChallangeCount());
        Label_013D:
            if ((this.RaidN != null) == null)
            {
                goto Label_0221;
            }
            if (this.mMultiRaidNum >= 0)
            {
                goto Label_0171;
            }
            this.mMultiRaidNum = Mathf.Min(this.MaxRaidNum, this.DefaultRaidNum);
        Label_0171:
            numArray1 = new int[] { this.mMultiRaidNum, num3, num4 };
            this.mMultiRaidNum = Mathf.Min(numArray1);
            objArray1 = new object[] { (int) Mathf.Max(this.mMultiRaidNum, 1) };
            this.RaidNCount.set_text(LocalizedText.Get("sys.RAIDNUM", objArray1));
            this.RaidN.set_interactable((num4 < 1) ? 0 : ((num2 < 1) == 0));
            goto Label_0221;
        Label_01E7:
            if ((this.Raid != null) == null)
            {
                goto Label_0204;
            }
            this.Raid.set_interactable(0);
        Label_0204:
            if ((this.RaidN != null) == null)
            {
                goto Label_0221;
            }
            this.RaidN.set_interactable(0);
        Label_0221:
            return;
        }

        private unsafe void RefreshRaidTicketNum()
        {
            int num;
            ItemData data;
            if ((this.RaidTicketNum != null) == null)
            {
                goto Label_006D;
            }
            num = 0;
            if (this.mCurrentQuest == null)
            {
                goto Label_005B;
            }
            if (string.IsNullOrEmpty(this.mCurrentQuest.ticket) != null)
            {
                goto Label_005B;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(this.mCurrentQuest.ticket);
            if (data == null)
            {
                goto Label_005B;
            }
            num = data.Num;
        Label_005B:
            this.RaidTicketNum.set_text(&num.ToString());
        Label_006D:
            return;
        }

        private void RefreshSankaStates()
        {
            int num;
            int num2;
            bool flag;
            if (this.mCurrentQuest != null)
            {
                goto Label_006B;
            }
            num = 0;
            goto Label_005C;
        Label_0012:
            if ((this.mSankaFukaIcons[num] != null) == null)
            {
                goto Label_0033;
            }
            this.mSankaFukaIcons[num].SetActive(0);
        Label_0033:
            if ((this.UnitSlots[num] != null) == null)
            {
                goto Label_0058;
            }
            this.UnitSlots[num].SetMainColor(Color.get_white());
        Label_0058:
            num += 1;
        Label_005C:
            if (num < ((int) this.mSankaFukaIcons.Length))
            {
                goto Label_0012;
            }
            return;
        Label_006B:
            num2 = 0;
            goto Label_0138;
        Label_0072:
            if ((this.mSankaFukaIcons[num2] != null) == null)
            {
                goto Label_0134;
            }
            flag = 1;
            if (((int) this.mCurrentParty.Units.Length) > num2)
            {
                goto Label_009F;
            }
            goto Label_0146;
        Label_009F:
            if (this.mCurrentParty.Units[num2] == null)
            {
                goto Label_00CA;
            }
            flag = this.mCurrentQuest.IsUnitAllowed(this.mCurrentParty.Units[num2]);
        Label_00CA:
            if ((this.UnitSlots[num2] != null) == null)
            {
                goto Label_0123;
            }
            if (flag == null)
            {
                goto Label_00FA;
            }
            this.UnitSlots[num2].SetMainColor(Color.get_white());
            goto Label_0123;
        Label_00FA:
            this.UnitSlots[num2].SetMainColor(new Color(this.SankaFukaOpacity, this.SankaFukaOpacity, this.SankaFukaOpacity, 1f));
        Label_0123:
            this.mSankaFukaIcons[num2].SetActive(flag == 0);
        Label_0134:
            num2 += 1;
        Label_0138:
            if (num2 < ((int) this.mSankaFukaIcons.Length))
            {
                goto Label_0072;
            }
        Label_0146:
            return;
        }

        private unsafe void RefreshUnitList()
        {
            List<UnitData> list;
            bool flag;
            bool flag2;
            string[] strArray;
            PartySlotTypeUnitPair[] pairArray;
            int num;
            UnitData data;
            int num2;
            int num3;
            int num4;
            string str;
            int num5;
            UnitData data2;
            int num6;
            RectTransform transform;
            <RefreshUnitList>c__AnonStorey374 storey;
            list = new List<UnitData>(this.mOwnUnits);
            this.UnitList.ClearItems();
            flag = this.mCurrentParty.Units[this.mSelectedSlotIndex] == null;
            if (((this.mSelectedSlotIndex <= 0) && (this.mIsHeloOnly == null)) || ((flag != null) && (this.AlwaysShowRemoveUnit == null)))
            {
                goto Label_0061;
            }
            this.UnitList.AddItem(0);
        Label_0061:
            flag2 = PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest);
            if ((this.UseQuestInfo == null) || (((this.mCurrentQuest.type != 5) && (this.mCurrentQuest.type != 13)) && (this.PartyType != 6)))
            {
                goto Label_0188;
            }
            strArray = null;
            if (this.mCurrentQuest.questParty == null)
            {
                goto Label_0122;
            }
            if (<>f__am$cacheA2 != null)
            {
                goto Label_00EB;
            }
            <>f__am$cacheA2 = new Func<PartySlotTypeUnitPair, bool>(PartyWindow2.<RefreshUnitList>m__3AB);
        Label_00EB:
            if (<>f__am$cacheA3 != null)
            {
                goto Label_010D;
            }
            <>f__am$cacheA3 = new Func<PartySlotTypeUnitPair, string>(PartyWindow2.<RefreshUnitList>m__3AC);
        Label_010D:
            strArray = Enumerable.ToArray<string>(Enumerable.Select<PartySlotTypeUnitPair, string>(Enumerable.Where<PartySlotTypeUnitPair>(this.mCurrentQuest.questParty.GetMainSubSlots(), <>f__am$cacheA2), <>f__am$cacheA3));
            goto Label_0133;
        Label_0122:
            strArray = this.mCurrentQuest.units.GetList();
        Label_0133:
            if (strArray == null)
            {
                goto Label_0188;
            }
            num = 0;
            goto Label_017E;
        Label_0141:
            storey = new <RefreshUnitList>c__AnonStorey374();
            storey.chQuestHeroId = strArray[num];
            data = Enumerable.FirstOrDefault<UnitData>(list, new Func<UnitData, bool>(storey.<>m__3AD));
            if (data == null)
            {
                goto Label_0178;
            }
            list.Remove(data);
        Label_0178:
            num += 1;
        Label_017E:
            if (num < ((int) strArray.Length))
            {
                goto Label_0141;
            }
        Label_0188:
            num2 = 0;
            num3 = this.mCurrentParty.PartyData.MAINMEMBER_START;
            goto Label_01C1;
        Label_01A2:
            if (this.mCurrentParty.Units[num3] == null)
            {
                goto Label_01BB;
            }
            num2 += 1;
        Label_01BB:
            num3 += 1;
        Label_01C1:
            if (num3 <= this.mCurrentParty.PartyData.MAINMEMBER_END)
            {
                goto Label_01A2;
            }
            num4 = 0;
            goto Label_02EE;
        Label_01E0:
            if (this.mCurrentParty.Units[num4] == null)
            {
                goto Label_02E8;
            }
            if ((flag2 != null) || (this.mCurrentParty.Units[num4].UnitParam.IsHero() == null))
            {
                goto Label_021B;
            }
            goto Label_02E8;
        Label_021B:
            if ((((this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex) || (this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END)) || ((num4 != null) || (flag == null))) || (num2 > 1))
            {
                goto Label_026B;
            }
            goto Label_02E8;
        Label_026B:
            if (this.UseQuestInfo == null)
            {
                goto Label_02A2;
            }
            str = string.Empty;
            if (this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units[num4], &str) != null)
            {
                goto Label_02A2;
            }
            goto Label_02E8;
        Label_02A2:
            this.UnitList.AddItem(this.mOwnUnits.IndexOf(PartyUtility.FindUnit(this.mCurrentParty.Units[num4], this.mOwnUnits)) + 1);
            list.Remove(this.mCurrentParty.Units[num4]);
        Label_02E8:
            num4 += 1;
        Label_02EE:
            if (num4 < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_01E0;
            }
            num5 = list.Count;
            UnitListV2.FilterUnits(list, null, this.mUnitFilter);
            if (this.mReverse == null)
            {
                goto Label_032B;
            }
            list.Reverse();
        Label_032B:
            this.RegistPartyMember(list, flag2, flag, num2);
            this.UnitList.Refresh(1);
            this.UnitList.ForceUpdateItems();
            if ((this.UnitListHilit != null) == null)
            {
                goto Label_03E5;
            }
            this.UnitListHilit.get_gameObject().SetActive(0);
            this.UnitListHilit.SetParent(base.get_transform(), 0);
            data2 = this.mCurrentParty.Units[this.mSelectedSlotIndex];
            if (data2 == null)
            {
                goto Label_03E5;
            }
            num6 = this.mOwnUnits.IndexOf(data2) + 1;
            if (num6 <= 0)
            {
                goto Label_03E5;
            }
            transform = this.UnitList.FindItem(num6);
            if ((transform != null) == null)
            {
                goto Label_03E5;
            }
            this.AttachAndEnable(this.UnitListHilit, transform, this.UnitListHilitParent);
        Label_03E5:
            if ((this.NoMatchingUnit != null) == null)
            {
                goto Label_041D;
            }
            this.NoMatchingUnit.SetActive((num5 <= 0) ? 0 : ((this.UnitList.NumItems > 0) == 0));
        Label_041D:
            return;
        }

        private unsafe UnitData[] RefreshUnits(UnitData[] units)
        {
            List<UnitData> list;
            List<UnitData> list2;
            bool flag;
            bool flag2;
            string[] strArray;
            PartySlotTypeUnitPair[] pairArray;
            int num;
            UnitData data;
            int num2;
            int num3;
            string str;
            UnitData data2;
            int num4;
            MyPhoton photon;
            bool flag3;
            List<UnitData> list3;
            int num5;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            <RefreshUnits>c__AnonStorey36C storeyc;
            <RefreshUnits>c__AnonStorey36D storeyd;
            list = new List<UnitData>(this.mOwnUnits);
            list2 = new List<UnitData>();
            flag = this.mCurrentParty.Units[this.mSelectedSlotIndex] == null;
            flag2 = PartyUtility.IsHeroesAvailable(this.mCurrentPartyType, this.mCurrentQuest);
            if ((this.UseQuestInfo == null) || (((this.mCurrentQuest.type != 5) && (this.mCurrentQuest.type != 13)) && (this.PartyType != 6)))
            {
                goto Label_0155;
            }
            strArray = null;
            if (this.mCurrentQuest.questParty == null)
            {
                goto Label_00EB;
            }
            if (<>f__am$cache83 != null)
            {
                goto Label_00B3;
            }
            <>f__am$cache83 = new Func<PartySlotTypeUnitPair, bool>(PartyWindow2.<RefreshUnits>m__37E);
        Label_00B3:
            if (<>f__am$cache84 != null)
            {
                goto Label_00D5;
            }
            <>f__am$cache84 = new Func<PartySlotTypeUnitPair, string>(PartyWindow2.<RefreshUnits>m__37F);
        Label_00D5:
            strArray = Enumerable.ToArray<string>(Enumerable.Select<PartySlotTypeUnitPair, string>(Enumerable.Where<PartySlotTypeUnitPair>(this.mCurrentQuest.questParty.GetMainSubSlots(), <>f__am$cache83), <>f__am$cache84));
            goto Label_00FD;
        Label_00EB:
            strArray = this.mCurrentQuest.units.GetList();
        Label_00FD:
            if (strArray == null)
            {
                goto Label_0155;
            }
            num = 0;
            goto Label_014A;
        Label_010C:
            storeyc = new <RefreshUnits>c__AnonStorey36C();
            storeyc.chQuestHeroId = strArray[num];
            data = Enumerable.FirstOrDefault<UnitData>(list, new Func<UnitData, bool>(storeyc.<>m__380));
            if (data == null)
            {
                goto Label_0144;
            }
            list.Remove(data);
        Label_0144:
            num += 1;
        Label_014A:
            if (num < ((int) strArray.Length))
            {
                goto Label_010C;
            }
        Label_0155:
            num2 = 0;
            num3 = this.mCurrentParty.PartyData.MAINMEMBER_START;
            goto Label_018E;
        Label_016F:
            if (this.mCurrentParty.Units[num3] == null)
            {
                goto Label_0188;
            }
            num2 += 1;
        Label_0188:
            num3 += 1;
        Label_018E:
            if (num3 <= this.mCurrentParty.PartyData.MAINMEMBER_END)
            {
                goto Label_016F;
            }
            storeyd = new <RefreshUnits>c__AnonStorey36D();
            storeyd.<>f__this = this;
            storeyd.i = 0;
            goto Label_02F0;
        Label_01C1:
            if (this.mCurrentParty.Units[storeyd.i] == null)
            {
                goto Label_02E0;
            }
            if ((flag2 != null) || (this.mCurrentParty.Units[storeyd.i].UnitParam.IsHero() == null))
            {
                goto Label_0206;
            }
            goto Label_02E0;
        Label_0206:
            if ((((this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex) || (this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END)) || ((storeyd.i != null) || (flag == null))) || (num2 > 1))
            {
                goto Label_025B;
            }
            goto Label_02E0;
        Label_025B:
            if (this.UseQuestInfo == null)
            {
                goto Label_0297;
            }
            str = string.Empty;
            if (this.mCurrentQuest.IsEntryQuestCondition(this.mCurrentParty.Units[storeyd.i], &str) != null)
            {
                goto Label_0297;
            }
            goto Label_02E0;
        Label_0297:
            data2 = list.Find(new Predicate<UnitData>(storeyd.<>m__381));
            if (data2 == null)
            {
                goto Label_02BB;
            }
            list2.Add(data2);
        Label_02BB:
            num4 = list.FindIndex(new Predicate<UnitData>(storeyd.<>m__382));
            if (num4 < 0)
            {
                goto Label_02E0;
            }
            list.RemoveAt(num4);
        Label_02E0:
            storeyd.i += 1;
        Label_02F0:
            if (storeyd.i < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_01C1;
            }
            photon = PunMonoSingleton<MyPhoton>.Instance;
            flag3 = ((photon != null) == null) ? 0 : (photon.CurrentState == 4);
            list3 = new List<UnitData>();
            num5 = 0;
            goto Label_041B;
        Label_033E:
            if (flag2 != null)
            {
                goto Label_0360;
            }
            if (list[num5].UnitParam.IsHero() == null)
            {
                goto Label_0360;
            }
            goto Label_0415;
        Label_0360:
            if (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex)
            {
                goto Label_03C3;
            }
            if (this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END)
            {
                goto Label_03C3;
            }
            if (list[num5] != this.mCurrentParty.Units[0])
            {
                goto Label_03C3;
            }
            if (flag == null)
            {
                goto Label_03C3;
            }
            if (num2 > 1)
            {
                goto Label_03C3;
            }
            goto Label_0415;
        Label_03C3:
            if (flag3 == null)
            {
                goto Label_0406;
            }
            room = photon.GetCurrentRoom();
            if (room == null)
            {
                goto Label_0406;
            }
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            if (list[num5].CalcLevel() >= param.unitlv)
            {
                goto Label_0406;
            }
            goto Label_0415;
        Label_0406:
            list3.Add(list[num5]);
        Label_0415:
            num5 += 1;
        Label_041B:
            if (num5 < list.Count)
            {
                goto Label_033E;
            }
            list2.AddRange(list3);
            return list2.ToArray();
        }

        protected virtual void RegistPartyMember(List<UnitData> allUnits, bool heroesAvailable, bool selectedSlotIsEmpty, int numMainMembers)
        {
            MyPhoton photon;
            bool flag;
            int num;
            MyPhoton.MyRoom room;
            JSON_MyPhotonRoomParam param;
            int num2;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            flag = ((photon != null) == null) ? 0 : (photon.CurrentState == 4);
            num = 0;
            goto Label_010D;
        Label_0026:
            if (heroesAvailable != null)
            {
                goto Label_0047;
            }
            if (allUnits[num].UnitParam.IsHero() == null)
            {
                goto Label_0047;
            }
            goto Label_0109;
        Label_0047:
            if (this.mCurrentParty.PartyData.SUBMEMBER_START > this.mSelectedSlotIndex)
            {
                goto Label_00A9;
            }
            if (this.mSelectedSlotIndex > this.mCurrentParty.PartyData.SUBMEMBER_END)
            {
                goto Label_00A9;
            }
            if (allUnits[num] != this.mCurrentParty.Units[0])
            {
                goto Label_00A9;
            }
            if (selectedSlotIsEmpty == null)
            {
                goto Label_00A9;
            }
            if (numMainMembers > 1)
            {
                goto Label_00A9;
            }
            goto Label_0109;
        Label_00A9:
            if (flag == null)
            {
                goto Label_00E6;
            }
            room = photon.GetCurrentRoom();
            if (room == null)
            {
                goto Label_00E6;
            }
            param = JSON_MyPhotonRoomParam.Parse(room.json);
            if (allUnits[num].CalcLevel() >= param.unitlv)
            {
                goto Label_00E6;
            }
            goto Label_0109;
        Label_00E6:
            num2 = this.mOwnUnits.IndexOf(allUnits[num]);
            this.UnitList.AddItem(num2 + 1);
        Label_0109:
            num += 1;
        Label_010D:
            if (num < allUnits.Count)
            {
                goto Label_0026;
            }
            return;
        }

        public void Reopen(bool farceRefresh)
        {
            if (farceRefresh != null)
            {
                goto Label_002B;
            }
            if (this.mCurrentQuest == null)
            {
                goto Label_003E;
            }
            if ((this.mCurrentQuest.iname != GlobalVars.SelectedQuestID) == null)
            {
                goto Label_003E;
            }
        Label_002B:
            this.RefreshQuest();
            this.CreateSlots();
            this.Refresh(0);
        Label_003E:
            this.GoToUnitList();
            FlowNode_GameObject.ActivateOutputLinks(this, 4);
            return;
        }

        public void ResetTeamName()
        {
            GlobalVars.TeamName = string.Empty;
            return;
        }

        private void ResetTeamPulldown(List<PartyEditData> teams, int maxTeams, QuestParam currentQuest)
        {
            int num;
            if (this.GetPartyCondType() == 2)
            {
                goto Label_0017;
            }
            if (this.mIsHeloOnly == null)
            {
                goto Label_0018;
            }
        Label_0017:
            return;
        Label_0018:
            if ((this.TeamPulldown != null) == null)
            {
                goto Label_00B5;
            }
            if (maxTeams > 1)
            {
                goto Label_0046;
            }
            this.TeamPulldown.get_gameObject().SetActive(0);
            goto Label_00B5;
        Label_0046:
            this.TeamPulldown.ResetAllItems();
            num = 0;
            goto Label_0076;
        Label_0058:
            this.TeamPulldown.SetItem(teams[num].Name, num, num);
            num += 1;
        Label_0076:
            if (num >= teams.Count)
            {
                goto Label_0093;
            }
            if (num < this.TeamPulldown.ItemCount)
            {
                goto Label_0058;
            }
        Label_0093:
            this.TeamPulldown.Selection = this.mCurrentTeamIndex;
            this.TeamPulldown.get_gameObject().SetActive(1);
        Label_00B5:
            return;
        }

        protected void SaveAndActivatePin(int pinID)
        {
            <SaveAndActivatePin>c__AnonStorey371 storey;
            storey = new <SaveAndActivatePin>c__AnonStorey371();
            storey.pinID = pinID;
            storey.<>f__this = this;
            if (this.mInitialized != null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            this.SaveInventory();
            if (this.IsPartyDirty != null)
            {
                goto Label_003E;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, storey.pinID);
            return;
        Label_003E:
            GlobalVars.SelectedSupport.Set(this.mCurrentSupport);
            if (<>f__am$cache8F != null)
            {
                goto Label_0073;
            }
            <>f__am$cache8F = new Callback(PartyWindow2.<SaveAndActivatePin>m__397);
        Label_0073:
            this.SaveParty(new Callback(storey.<>m__396), <>f__am$cache8F);
            return;
        }

        private void SaveInventory()
        {
            PlayerData data;
            int num;
            ItemData data2;
            if (this.mInitialized == null)
            {
                goto Label_0016;
            }
            if (this.InventoryDirty != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_0066;
        Label_0029:
            data2 = null;
            if (num >= ((int) this.mCurrentItems.Length))
            {
                goto Label_005A;
            }
            if (this.mCurrentItems[num] == null)
            {
                goto Label_005A;
            }
            data2 = data.FindItemDataByItemParam(this.mCurrentItems[num].Param);
        Label_005A:
            data.SetInventory(num, data2);
            num += 1;
        Label_0066:
            if (num < 5)
            {
                goto Label_0029;
            }
            data.SaveInventory();
            return;
        }

        private void SaveParty(Callback cbSuccess, Callback cbError)
        {
            PlayerData data;
            PlayerPartyTypes types;
            PartyData data2;
            List<UnitData> list;
            int num;
            PartySlotData data3;
            int num2;
            int num3;
            PartySlotData data4;
            int num4;
            int num5;
            long num6;
            bool flag;
            this.LockWindow(1);
            this.mIsSaving = 1;
            this.mOnPartySaveSuccess = cbSuccess;
            this.mOnPartySaveFail = cbError;
            if (this.IsPartyDirty != null)
            {
                goto Label_003E;
            }
            if (this.mOnPartySaveSuccess == null)
            {
                goto Label_003D;
            }
            this.mOnPartySaveSuccess();
        Label_003D:
            return;
        Label_003E:
            this.SaveTeamPresets();
            data = MonoSingleton<GameManager>.Instance.Player;
            types = SRPG_Extensions.ToPlayerPartyType(this.mCurrentPartyType);
            data2 = data.FindPartyOfType(types);
            list = new List<UnitData>();
            num = this.mCurrentParty.PartyData.MAINMEMBER_START;
            goto Label_00C8;
        Label_0080:
            data3 = this.mSlotData[num];
            if (data3.Type == 4)
            {
                goto Label_00C2;
            }
            if (data3.Type != 5)
            {
                goto Label_00AE;
            }
            goto Label_00C2;
        Label_00AE:
            list.Add(this.mCurrentParty.Units[num]);
        Label_00C2:
            num += 1;
        Label_00C8:
            if (num < this.mCurrentParty.PartyData.MAX_MAINMEMBER)
            {
                goto Label_0080;
            }
            num2 = list.Count;
            goto Label_00F9;
        Label_00EC:
            list.Add(null);
            num2 += 1;
        Label_00F9:
            if (num2 < this.mCurrentParty.PartyData.MAX_MAINMEMBER)
            {
                goto Label_00EC;
            }
            num3 = this.mCurrentParty.PartyData.SUBMEMBER_START;
            goto Label_016F;
        Label_0127:
            data4 = this.mSlotData[num3];
            if (data4.Type == 4)
            {
                goto Label_0169;
            }
            if (data4.Type != 5)
            {
                goto Label_0155;
            }
            goto Label_0169;
        Label_0155:
            list.Add(this.mCurrentParty.Units[num3]);
        Label_0169:
            num3 += 1;
        Label_016F:
            if (num3 <= this.mCurrentParty.PartyData.SUBMEMBER_END)
            {
                goto Label_0127;
            }
            num4 = list.Count;
            goto Label_01A0;
        Label_0193:
            list.Add(null);
            num4 += 1;
        Label_01A0:
            if (num4 < this.mCurrentParty.PartyData.MAX_UNIT)
            {
                goto Label_0193;
            }
            num5 = 0;
            goto Label_01F2;
        Label_01BF:
            num6 = (list[num5] == null) ? 0L : list[num5].UniqueID;
            data2.SetUnitUniqueID(num5, num6);
            num5 += 1;
        Label_01F2:
            if (num5 < list.Count)
            {
                goto Label_01BF;
            }
            flag = 0;
            Network.RequestAPI(new ReqParty(new Network.ResponseCallback(this.SavePartyCallback), 0, flag, 0), 0);
            return;
        }

        private unsafe void SavePartyCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            GameManager manager;
            if (Network.IsError == null)
            {
                goto Label_0071;
            }
            if (Network.ErrCode != 0x708)
            {
                goto Label_0028;
            }
            Network.RemoveAPI();
            Network.ResetError();
            goto Label_004C;
        Label_0028:
            if (Network.ErrCode != 0x709)
            {
                goto Label_0046;
            }
            Network.RemoveAPI();
            Network.ResetError();
            goto Label_004C;
        Label_0046:
            FlowNode_Network.Retry();
            return;
        Label_004C:
            this.LockWindow(0);
            this.mIsSaving = 0;
            if (this.mOnPartySaveFail == null)
            {
                goto Label_0070;
            }
            this.mOnPartySaveFail();
        Label_0070:
            return;
        Label_0071:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            manager = MonoSingleton<GameManager>.Instance;
        Label_0084:
            try
            {
                if (response.body != null)
                {
                    goto Label_0095;
                }
                throw new InvalidJSONException();
            Label_0095:
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.parties);
                goto Label_00CC;
            }
            catch (Exception)
            {
            Label_00BC:
                FlowNode_Network.Retry();
                goto Label_00F5;
            }
        Label_00CC:
            Network.RemoveAPI();
            this.LockWindow(0);
            this.mIsSaving = 0;
            if (this.mOnPartySaveSuccess == null)
            {
                goto Label_00F5;
            }
            this.mOnPartySaveSuccess();
        Label_00F5:
            return;
        }

        private void SaveRecommendedTeamSetting(GlobalVars.RecommendTeamSetting setting)
        {
            string str;
            str = JsonUtility.ToJson(setting);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.RECOMMENDED_TEAM_SETTING_KEY, str, 1);
            return;
        }

        private void SaveTeamPresets()
        {
            if (this.mCurrentPartyType == 4)
            {
                goto Label_0018;
            }
            if (this.mCurrentPartyType != 5)
            {
                goto Label_0019;
            }
        Label_0018:
            return;
        Label_0019:
            if (this.mCurrentPartyType != 3)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            if (this.mCurrentQuest == null)
            {
                goto Label_0047;
            }
            if (this.mCurrentQuest.type != 5)
            {
                goto Label_0047;
            }
            goto Label_0053;
        Label_0047:
            if (this.ContainsNpcOrForced() == null)
            {
                goto Label_0053;
            }
            return;
        Label_0053:
            if (this.GetPartyCondType() == 2)
            {
                goto Label_006A;
            }
            if (this.IsFixedParty() == null)
            {
                goto Label_006B;
            }
        Label_006A:
            return;
        Label_006B:
            PartyUtility.SaveTeamPresets(this.mCurrentPartyType, this.mCurrentTeamIndex, this.mTeams, this.ContainsNotFree());
            return;
        }

        private unsafe List<List<UnitData>> SeparateUnitByElement(List<UnitData> allUnits, IEnumerable<string> kyouseiUnits, EElement targetElement, bool isHeroAvailable)
        {
            List<UnitData> list;
            List<UnitData> list2;
            List<UnitData> list3;
            List<UnitData> list4;
            List<UnitData> list5;
            List<UnitData> list6;
            List<UnitData> list7;
            HashSet<string> set;
            int num;
            UnitData data;
            UnitData[] dataArray;
            int num2;
            List<UnitData>.Enumerator enumerator;
            string str;
            List<List<UnitData>> list8;
            int num3;
            List<UnitData> list9;
            <SeparateUnitByElement>c__AnonStorey375 storey;
            EElement element;
            List<List<UnitData>> list10;
            list = new List<UnitData>();
            list2 = new List<UnitData>();
            list3 = new List<UnitData>();
            list4 = new List<UnitData>();
            list5 = new List<UnitData>();
            list6 = new List<UnitData>();
            list7 = new List<UnitData>();
            set = (kyouseiUnits != null) ? new HashSet<string>(kyouseiUnits) : new HashSet<string>();
            if (this.mCurrentQuest.type != 15)
            {
                goto Label_00CE;
            }
            num = 0;
            goto Label_00C1;
        Label_005F:
            if (num != this.mCurrentTeamIndex)
            {
                goto Label_0071;
            }
            goto Label_00BB;
        Label_0071:
            dataArray = this.mTeams[num].Units;
            num2 = 0;
            goto Label_00B0;
        Label_008D:
            data = dataArray[num2];
            if (data == null)
            {
                goto Label_00AA;
            }
            set.Add(data.UnitID);
        Label_00AA:
            num2 += 1;
        Label_00B0:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_008D;
            }
        Label_00BB:
            num += 1;
        Label_00C1:
            if (num < this.mMaxTeamCount)
            {
                goto Label_005F;
            }
        Label_00CE:
            storey = new <SeparateUnitByElement>c__AnonStorey375();
            enumerator = allUnits.GetEnumerator();
        Label_00DD:
            try
            {
                goto Label_0215;
            Label_00E2:
                storey.unit = &enumerator.Current;
                str = Enumerable.FirstOrDefault<string>(set, new Func<string, bool>(storey.<>m__3B0));
                if (str == null)
                {
                    goto Label_011C;
                }
                set.Remove(str);
                goto Label_0215;
            Label_011C:
                if (isHeroAvailable != null)
                {
                    goto Label_013E;
                }
                if (storey.unit.UnitParam.IsHero() == null)
                {
                    goto Label_013E;
                }
                goto Label_0215;
            Label_013E:
                if (this.mCurrentQuest.IsEntryQuestCondition(storey.unit) != null)
                {
                    goto Label_015A;
                }
                goto Label_0215;
            Label_015A:
                if (targetElement != null)
                {
                    goto Label_0172;
                }
                list.Add(storey.unit);
                goto Label_0215;
            Label_0172:
                switch ((storey.unit.Element - 1))
                {
                    case 0:
                        goto Label_01A6;

                    case 1:
                        goto Label_01B8;

                    case 2:
                        goto Label_01CA;

                    case 3:
                        goto Label_01DC;

                    case 4:
                        goto Label_01EF;

                    case 5:
                        goto Label_0202;
                }
                goto Label_0215;
            Label_01A6:
                list2.Add(storey.unit);
                goto Label_0215;
            Label_01B8:
                list3.Add(storey.unit);
                goto Label_0215;
            Label_01CA:
                list4.Add(storey.unit);
                goto Label_0215;
            Label_01DC:
                list5.Add(storey.unit);
                goto Label_0215;
            Label_01EF:
                list6.Add(storey.unit);
                goto Label_0215;
            Label_0202:
                list7.Add(storey.unit);
            Label_0215:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00E2;
                }
                goto Label_0233;
            }
            finally
            {
            Label_0226:
                ((List<UnitData>.Enumerator) enumerator).Dispose();
            }
        Label_0233:
            if (targetElement != null)
            {
                goto Label_0251;
            }
            list10 = new List<List<UnitData>>();
            list10.Add(list);
            list8 = list10;
            goto Label_02C8;
        Label_0251:
            list10 = new List<List<UnitData>>();
            list10.Add(list2);
            list10.Add(list3);
            list10.Add(list4);
            list10.Add(list5);
            list10.Add(list6);
            list10.Add(list7);
            list8 = list10;
            num3 = targetElement - 1;
            if (num3 < 0)
            {
                goto Label_02C8;
            }
            if (num3 >= list8.Count)
            {
                goto Label_02C8;
            }
            list9 = list8[num3];
            list8.RemoveAt(num3);
            list8.Insert(0, list9);
        Label_02C8:
            return list8;
        }

        protected virtual void SetItemSlot(int slotIndex, ItemData item)
        {
            int num;
            int num2;
            int num3;
            ItemData data;
            if (item != null)
            {
                goto Label_0069;
            }
            num = slotIndex;
            goto Label_0038;
        Label_000D:
            this.mCurrentItems[num] = this.mCurrentItems[num + 1];
            this.ItemSlots[num].SetSlotData<ItemData>(this.mCurrentItems[num]);
            num += 1;
        Label_0038:
            if (num < 4)
            {
                goto Label_000D;
            }
            num2 = ((int) this.mCurrentItems.Length) - 1;
            this.mCurrentItems[num2] = null;
            this.ItemSlots[num2].SetSlotData<ItemData>(this.mCurrentItems[num2]);
            return;
        Label_0069:
            num3 = Math.Min(item.Num, item.Param.invcap);
            data = new ItemData();
            data.Setup(item.UniqueID, item.Param, num3);
            this.mCurrentItems[slotIndex] = data;
            this.ItemSlots[slotIndex].SetSlotData<ItemData>(data);
            this.OnSlotChange(this.ItemSlots[slotIndex].get_gameObject());
            return;
        }

        private void SetPartyUnit(int slotIndex, UnitData unit)
        {
            int num;
            int num2;
            int num3;
            if (slotIndex < 0)
            {
                goto Label_002F;
            }
            if (slotIndex >= this.mSlotData.Count)
            {
                goto Label_002F;
            }
            if (this.IsSettableSlot(this.mSlotData[slotIndex]) != null)
            {
                goto Label_0030;
            }
        Label_002F:
            return;
        Label_0030:
            if (unit != null)
            {
                goto Label_0188;
            }
            if (slotIndex >= this.mCurrentParty.PartyData.MAX_MAINMEMBER)
            {
                goto Label_0062;
            }
            num = this.mCurrentParty.PartyData.MAX_MAINMEMBER;
            goto Label_0073;
        Label_0062:
            num = this.mCurrentParty.PartyData.MAX_UNIT;
        Label_0073:
            num2 = slotIndex;
            goto Label_0180;
        Label_007A:
            if (this.mSlotData[num2].Type == null)
            {
                goto Label_0095;
            }
            goto Label_017C;
        Label_0095:
            num3 = 1;
            goto Label_00A0;
        Label_009C:
            num3 += 1;
        Label_00A0:
            if ((num2 + num3) >= num)
            {
                goto Label_00C1;
            }
            if (this.mSlotData[num2 + num3].Type != null)
            {
                goto Label_009C;
            }
        Label_00C1:
            if ((num2 + num3) >= num)
            {
                goto Label_014D;
            }
            this.mCurrentParty.Units[num2] = this.mCurrentParty.Units[num2 + num3];
            this.UnitSlots[num2].SetSlotData<QuestParam>(this.mCurrentQuest);
            this.UnitSlots[num2].SetSlotData<UnitData>(this.mCurrentParty.Units[num2]);
            this.mCurrentParty.Units[num2 + num3] = null;
            this.UnitSlots[num2 + num3].SetSlotData<QuestParam>(this.mCurrentQuest);
            this.UnitSlots[num2 + num3].SetSlotData<UnitData>(null);
            goto Label_017C;
        Label_014D:
            this.mCurrentParty.Units[num2] = null;
            this.UnitSlots[num2].SetSlotData<QuestParam>(this.mCurrentQuest);
            this.UnitSlots[num2].SetSlotData<UnitData>(null);
        Label_017C:
            num2 += 1;
        Label_0180:
            if (num2 < num)
            {
                goto Label_007A;
            }
            return;
        Label_0188:
            this.mCurrentParty.Units[slotIndex] = unit;
            this.UnitSlots[slotIndex].SetSlotData<QuestParam>(this.mCurrentQuest);
            this.UnitSlots[slotIndex].SetSlotData<UnitData>(unit);
            this.RefreshSankaStates();
            this.OnSlotChange(this.UnitSlots[slotIndex].get_gameObject());
            return;
        }

        private void SetPartyUnitForce(int slotIndex, UnitData unit)
        {
            if (slotIndex < 0)
            {
                goto Label_002F;
            }
            if (slotIndex >= this.mSlotData.Count)
            {
                goto Label_002F;
            }
            if (this.IsSettableSlot(this.mSlotData[slotIndex]) != null)
            {
                goto Label_0030;
            }
        Label_002F:
            return;
        Label_0030:
            this.mCurrentParty.Units[slotIndex] = unit;
            this.UnitSlots[slotIndex].SetSlotData<QuestParam>(this.mCurrentQuest);
            this.UnitSlots[slotIndex].SetSlotData<UnitData>(unit);
            this.RefreshSankaStates();
            this.OnSlotChange(this.UnitSlots[slotIndex].get_gameObject());
            return;
        }

        public void SetSortMethod(string method, bool ascending, string[] filters)
        {
            GameUtility.UnitSortModes modes;
            modes = 0;
        Label_0002:
            try
            {
                if (string.IsNullOrEmpty(method) != null)
                {
                    goto Label_0024;
                }
                modes = (int) Enum.Parse(typeof(GameUtility.UnitSortModes), method, 1);
            Label_0024:
                goto Label_0049;
            }
            catch (Exception)
            {
            Label_0029:
                if (GameUtility.IsDebugBuild == null)
                {
                    goto Label_0044;
                }
                DebugUtility.LogError("Unknown sort mode: " + method);
            Label_0044:
                goto Label_0049;
            }
        Label_0049:
            if ((this.AscendingIcon != null) == null)
            {
                goto Label_0066;
            }
            this.AscendingIcon.SetActive(ascending);
        Label_0066:
            if ((this.DescendingIcon != null) == null)
            {
                goto Label_0086;
            }
            this.DescendingIcon.SetActive(ascending == 0);
        Label_0086:
            if (modes != null)
            {
                goto Label_0092;
            }
            ascending = ascending == 0;
        Label_0092:
            if ((this.SortModeCaption != null) == null)
            {
                goto Label_00CD;
            }
            this.SortModeCaption.set_text(LocalizedText.Get("sys.SORT_" + ((GameUtility.UnitSortModes) modes).ToString().ToUpper()));
        Label_00CD:
            this.mReverse = ascending;
            this.mUnitFilter = filters;
            if (this.mUnitSlotSelected == null)
            {
                goto Label_00EC;
            }
            this.RefreshUnitList();
        Label_00EC:
            return;
        }

        private void SetSupport(SupportData support)
        {
            int num;
            if (this.mCurrentPartyType != 10)
            {
                goto Label_0024;
            }
            this.mSupports[this.mCurrentTeamIndex] = support;
            goto Label_004D;
        Label_0024:
            num = 0;
            goto Label_003C;
        Label_002B:
            this.mSupports[num] = support;
            num += 1;
        Label_003C:
            if (num < this.mSupports.Count)
            {
                goto Label_002B;
            }
        Label_004D:
            this.mCurrentSupport = support;
            if ((this.FriendSlot != null) == null)
            {
                goto Label_00BB;
            }
            this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
            this.FriendSlot.SetSlotData<SupportData>(support);
            if (support != null)
            {
                goto Label_0099;
            }
            this.FriendSlot.SetSlotData<UnitData>(null);
            goto Label_00AA;
        Label_0099:
            this.FriendSlot.SetSlotData<UnitData>(support.Unit);
        Label_00AA:
            this.OnSlotChange(this.FriendSlot.get_gameObject());
        Label_00BB:
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowRaidResultAsync()
        {
            <ShowRaidResultAsync>c__Iterator12C iteratorc;
            iteratorc = new <ShowRaidResultAsync>c__Iterator12C();
            iteratorc.<>f__this = this;
            return iteratorc;
        }

        private void ShowRaidSettings()
        {
            GameObject obj2;
            if ((this.RaidSettingsTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.mRaidSettings == null) == null)
            {
                goto Label_006F;
            }
            obj2 = Object.Instantiate<GameObject>(this.RaidSettingsTemplate);
            this.mRaidSettings = obj2.GetComponent<RaidSettingsWindow>();
            this.mRaidSettings.OnAccept = new RaidSettingsWindow.RaidSettingsEvent(this.RaidSettingsAccepted);
            this.mRaidSettings.Setup(this.mCurrentQuest, this.mMultiRaidNum, this.MaxRaidNum);
        Label_006F:
            return;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator12A iteratora;
            iteratora = new <Start>c__Iterator12A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        private void StartRaid()
        {
            int num;
            num = 0;
            goto Label_0025;
        Label_0007:
            MonoSingleton<GameManager>.Instance.Player.IncrementQuestChallangeNumDaily(this.mCurrentQuest.iname);
            num += 1;
        Label_0025:
            if (num < this.mNumRaids)
            {
                goto Label_0007;
            }
            Network.RequestAPI(new ReqBtlComRaid(this.mCurrentQuest.iname, this.mNumRaids, new Network.ResponseCallback(this.RecvRaidResult), 0), 0);
            return;
        }

        private void SupportList_Show()
        {
            if ((this.mUnitListWindow == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mUnitListWindow.Enabled(1);
            this.LockWindow(1);
            this.mUnitListWindow.AddData("data_support", this.mSupports.ToArray());
            this.mUnitListWindow.AddData("data_party_index", (int) this.mCurrentTeamIndex);
            this.mUnitListWindow.AddData("data_quest", this.mCurrentQuest);
            ButtonEvent.Invoke("UNITLIST_BTN_SUPPORT_OPEN", null);
            ButtonEvent.Lock("PartyWindow");
            return;
        }

        private static void ToggleBlockRaycasts(Component component, bool block)
        {
            CanvasGroup group;
            group = component.GetComponent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_001A;
            }
            group.set_blocksRaycasts(block);
        Label_001A:
            return;
        }

        private void ToggleRaidInfo()
        {
            bool flag;
            if ((this.RaidInfo != null) == null)
            {
                goto Label_005F;
            }
            flag = 0;
            if (this.mCurrentQuest == null)
            {
                goto Label_0046;
            }
            if (string.IsNullOrEmpty(this.mCurrentQuest.ticket) != null)
            {
                goto Label_0046;
            }
            if (this.mCurrentQuest.state != 2)
            {
                goto Label_0046;
            }
            flag = 1;
        Label_0046:
            if (this.ShowRaidInfo != null)
            {
                goto Label_0053;
            }
            flag = 0;
        Label_0053:
            this.RaidInfo.SetActive(flag);
        Label_005F:
            return;
        }

        private void UnitList_Activated(int pinID)
        {
            SRPG_Button button;
            int num;
            num = pinID;
            if (num == 100)
            {
                goto Label_0037;
            }
            if (num == 0x65)
            {
                goto Label_0042;
            }
            if (num == 110)
            {
                goto Label_004D;
            }
            if (num == 0x6f)
            {
                goto Label_0058;
            }
            if (num == 0x77)
            {
                goto Label_0063;
            }
            if (num == 120)
            {
                goto Label_0079;
            }
            goto Label_00A3;
        Label_0037:
            this.UnitList_OnSelect();
            goto Label_00A3;
        Label_0042:
            this.UnitList_OnSelectSupport();
            goto Label_00A3;
        Label_004D:
            this.UnitList_OnRemove();
            goto Label_00A3;
        Label_0058:
            this.UnitList_OnRemoveSupport();
            goto Label_00A3;
        Label_0063:
            this.LockWindow(0);
            ButtonEvent.ResetLock("PartyWindow");
            goto Label_00A3;
        Label_0079:
            button = FlowNode_ButtonEvent.currentValue as SRPG_Button;
            if ((button == null) == null)
            {
                goto Label_0097;
            }
            button = this.BackButton;
        Label_0097:
            this.UnitList_OnClosing(button);
        Label_00A3:
            return;
        }

        private void UnitList_Create()
        {
            GameObject obj2;
            GameObject obj3;
            CanvasStack stack;
            CanvasStack stack2;
            if (string.IsNullOrEmpty(this.UNITLIST_WINDOW_PATH) != null)
            {
                goto Label_007C;
            }
            obj2 = AssetManager.Load<GameObject>(this.UNITLIST_WINDOW_PATH);
            if ((obj2 != null) == null)
            {
                goto Label_007C;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_007C;
            }
            stack = obj3.GetComponent<CanvasStack>();
            if ((stack != null) == null)
            {
                goto Label_0064;
            }
            stack2 = base.GetComponent<CanvasStack>();
            stack.Priority = stack2.Priority + 10;
        Label_0064:
            this.mUnitListWindow = obj3.GetComponent<UnitListWindow>();
            this.mUnitListWindow.Enabled(0);
        Label_007C:
            return;
        }

        private void UnitList_OnAcceptSupport(GameObject go)
        {
            int num;
            string str;
            num = this.CalculateTotalSupportCost(this.mSelectedSupport);
            if (MonoSingleton<GameManager>.Instance.Player.Gold >= num)
            {
                goto Label_003A;
            }
            str = LocalizedText.Get("sys.SUPPORT_NOGOLD");
            UIUtility.NegativeSystemMessage(null, str, null, null, 0, -1);
            return;
        Label_003A:
            this.SetSupport(this.mSelectedSupport);
            this.OnPartyMemberChange();
            ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", this.ForwardButton);
            if (this.mSelectedSupport == null)
            {
                goto Label_0097;
            }
            if (this.mSelectedSupport.IsFriend() == null)
            {
                goto Label_0087;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 200);
            goto Label_0092;
        Label_0087:
            FlowNode_GameObject.ActivateOutputLinks(this, 210);
        Label_0092:
            goto Label_00A2;
        Label_0097:
            FlowNode_GameObject.ActivateOutputLinks(this, 220);
        Label_00A2:
            return;
        }

        private void UnitList_OnClosing(SRPG_Button button)
        {
            UnitListRootWindow.ListData data;
            if ((this.mUnitListWindow != null) == null)
            {
                goto Label_005D;
            }
            if (this.mUnitListWindow.IsEnabled() != null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            this.mUnitListWindow.ClearData();
            data = this.mUnitListWindow.rootWindow.GetListData("unitlist");
            if (data == null)
            {
                goto Label_0051;
            }
            data.selectUniqueID = 0L;
        Label_0051:
            this.mUnitListWindow.Enabled(0);
        Label_005D:
            if (this.PartyType != 9)
            {
                goto Label_007D;
            }
            if ((button != null) == null)
            {
                goto Label_007D;
            }
            this.OnForwardOrBackButtonClick(button);
        Label_007D:
            return;
        }

        private void UnitList_OnRemove()
        {
            if ((this.mUnitListWindow == null) != null)
            {
                goto Label_0021;
            }
            if (this.mUnitListWindow.IsEnabled() != null)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            this.SetPartyUnit(this.mSelectedSlotIndex, null);
            this.OnPartyMemberChange();
            this.SaveTeamPresets();
            ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", this.ForwardButton);
            return;
        }

        private void UnitList_OnRemoveSupport()
        {
            if ((this.mUnitListWindow == null) != null)
            {
                goto Label_0021;
            }
            if (this.mUnitListWindow.IsEnabled() != null)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            this.SetSupport(null);
            if ((this.FriendSlot != null) == null)
            {
                goto Label_0057;
            }
            this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
            this.FriendSlot.SetSlotData<UnitData>(null);
        Label_0057:
            this.OnPartyMemberChange();
            this.SaveTeamPresets();
            ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", null);
            FlowNode_GameObject.ActivateOutputLinks(this, 220);
            return;
        }

        private void UnitList_OnSelect()
        {
            int num;
            UnitData data;
            int num2;
            int num3;
            int num4;
            int num5;
            UnitData data2;
            SerializeValueList list;
            int num6;
            if ((this.mUnitListWindow == null) != null)
            {
                goto Label_0021;
            }
            if (this.mUnitListWindow.IsEnabled() != null)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            num = this.mSelectedSlotIndex;
            data = this.mCurrentParty.Units[this.mSelectedSlotIndex];
            if (num >= this.mCurrentParty.PartyData.MAX_MAINMEMBER)
            {
                goto Label_0098;
            }
            num2 = num;
            num3 = num;
            goto Label_008A;
        Label_005B:
            if (this.IsSettableSlot(this.mSlotData[num3]) == null)
            {
                goto Label_0086;
            }
            if (this.mCurrentParty.Units[num3] != null)
            {
                goto Label_0086;
            }
            num2 = num3;
        Label_0086:
            num3 -= 1;
        Label_008A:
            if (num3 >= 0)
            {
                goto Label_005B;
            }
            num = num2;
            goto Label_00F2;
        Label_0098:
            num4 = num;
            num5 = num;
            goto Label_00D8;
        Label_00A3:
            if (this.IsSettableSlot(this.mSlotData[num5]) == null)
            {
                goto Label_00D2;
            }
            if (this.mCurrentParty.Units[num5] != null)
            {
                goto Label_00D2;
            }
            num4 = num5;
        Label_00D2:
            num5 -= 1;
        Label_00D8:
            if (num5 >= this.mCurrentParty.PartyData.MAX_MAINMEMBER)
            {
                goto Label_00A3;
            }
            num = num4;
        Label_00F2:
            data2 = null;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_0116;
            }
            data2 = list.GetDataSource<UnitData>("_self");
        Label_0116:
            if (data2 == null)
            {
                goto Label_0164;
            }
            if (data2 == data)
            {
                goto Label_0164;
            }
            num6 = this.mCurrentParty.IndexOf(data2);
            if (num6 < 0)
            {
                goto Label_015B;
            }
            if (num == num6)
            {
                goto Label_015B;
            }
            this.SetPartyUnit(num, data2);
            this.SetPartyUnit(num6, data);
            goto Label_0164;
        Label_015B:
            this.SetPartyUnit(num, data2);
        Label_0164:
            this.OnPartyMemberChange();
            this.SaveTeamPresets();
            ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", this.ForwardButton);
            return;
        }

        private void UnitList_OnSelectSupport()
        {
            object[] objArray1;
            SerializeValueList list;
            string str;
            string str2;
            if ((this.mUnitListWindow == null) != null)
            {
                goto Label_0021;
            }
            if (this.mUnitListWindow.IsEnabled() != null)
            {
                goto Label_0022;
            }
        Label_0021:
            return;
        Label_0022:
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_0049;
            }
            this.mSelectedSupport = list.GetDataSource<SupportData>("_self");
            goto Label_0050;
        Label_0049:
            this.mSelectedSupport = null;
        Label_0050:
            if (this.mCurrentSupport != this.mSelectedSupport)
            {
                goto Label_0072;
            }
            ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", this.ForwardButton);
            return;
        Label_0072:
            if (this.mSelectedSupport.GetCost() <= 0)
            {
                goto Label_008E;
            }
            str = "sys.SUPPORT_CONFIRM2";
            goto Label_0094;
        Label_008E:
            str = "sys.SUPPORT_CONFIRM1";
        Label_0094:
            objArray1 = new object[] { this.mSelectedSupport.PlayerName, (int) this.mSelectedSupport.GetCost() };
            UIUtility.ConfirmBox(LocalizedText.Get(str, objArray1), null, new UIUtility.DialogResultEvent(this.UnitList_OnAcceptSupport), null, null, 0, -1);
            return;
        }

        private void UnitList_Remove()
        {
            if ((this.mUnitListWindow != null) == null)
            {
                goto Label_003E;
            }
            if ((this.mUnitListWindow.get_gameObject() != null) == null)
            {
                goto Label_0037;
            }
            GameUtility.DestroyGameObject(this.mUnitListWindow.get_gameObject());
        Label_0037:
            this.mUnitListWindow = null;
        Label_003E:
            return;
        }

        private void UnitList_Show()
        {
            if ((this.mUnitListWindow == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mUnitListWindow.Enabled(1);
            this.LockWindow(1);
            this.mUnitListWindow.AddData("data_units", this.RefreshUnits(this.mOwnUnits.ToArray()));
            this.mUnitListWindow.AddData("data_party", this.mTeams.ToArray());
            this.mUnitListWindow.AddData("data_party_index", (int) this.mCurrentTeamIndex);
            this.mUnitListWindow.AddData("data_quest", this.mCurrentQuest);
            this.mUnitListWindow.AddData("data_slot", (int) this.mSelectedSlotIndex);
            this.mUnitListWindow.AddData("data_heroOnly", (bool) this.mIsHeloOnly);
            if (this.mCurrentQuest == null)
            {
                goto Label_00F4;
            }
            if (this.mCurrentQuest.type != 7)
            {
                goto Label_00F4;
            }
            ButtonEvent.Invoke("UNITLIST_BTN_TWPARTY_OPEN", null);
            goto Label_00FF;
        Label_00F4:
            ButtonEvent.Invoke("UNITLIST_BTN_PARTY_OPEN", null);
        Label_00FF:
            ButtonEvent.Lock("PartyWindow");
            return;
        }

        private void UpdateLeaderSkills()
        {
            SkillParam param;
            UnitParam param2;
            string str;
            SkillParam param3;
            if ((this.LeaderSkill != null) == null)
            {
                goto Label_01AA;
            }
            param = null;
            if (this.mIsHeloOnly == null)
            {
                goto Label_006C;
            }
            if (this.mGuestUnit == null)
            {
                goto Label_019E;
            }
            if (this.mGuestUnit.Count <= 0)
            {
                goto Label_019E;
            }
            if (this.mGuestUnit[0].LeaderSkill == null)
            {
                goto Label_019E;
            }
            param = this.mGuestUnit[0].LeaderSkill.SkillParam;
            goto Label_019E;
        Label_006C:
            if (this.mCurrentParty.Units[0] == null)
            {
                goto Label_00B2;
            }
            if (this.mCurrentParty.Units[0].LeaderSkill == null)
            {
                goto Label_019E;
            }
            param = this.mCurrentParty.Units[0].LeaderSkill.SkillParam;
            goto Label_019E;
        Label_00B2:
            if (this.mSlotData[0].Type == 4)
            {
                goto Label_00E0;
            }
            if (this.mSlotData[0].Type != 5)
            {
                goto Label_014A;
            }
        Label_00E0:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.mSlotData[0].UnitName);
            if (param2 == null)
            {
                goto Label_019E;
            }
            if (param2.leader_skills == null)
            {
                goto Label_019E;
            }
            if (((int) param2.leader_skills.Length) < 4)
            {
                goto Label_019E;
            }
            str = param2.leader_skills[4];
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_019E;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(str);
            goto Label_019E;
        Label_014A:
            if (this.EnableHeroSolo == null)
            {
                goto Label_019E;
            }
            if (this.mGuestUnit == null)
            {
                goto Label_019E;
            }
            if (this.mGuestUnit.Count <= 0)
            {
                goto Label_019E;
            }
            if (this.mGuestUnit[0].LeaderSkill == null)
            {
                goto Label_019E;
            }
            param = this.mGuestUnit[0].LeaderSkill.SkillParam;
        Label_019E:
            this.LeaderSkill.SetSlotData<SkillParam>(param);
        Label_01AA:
            if ((this.SupportSkill != null) == null)
            {
                goto Label_01FF;
            }
            param3 = null;
            if (this.mCurrentSupport == null)
            {
                goto Label_01F3;
            }
            if (this.mCurrentSupport.Unit.LeaderSkill == null)
            {
                goto Label_01F3;
            }
            param3 = this.mCurrentSupport.Unit.LeaderSkill.SkillParam;
        Label_01F3:
            this.SupportSkill.SetSlotData<SkillParam>(param3);
        Label_01FF:
            return;
        }

        private void ValidateSupport(int maxTeamCount)
        {
            int num;
            if (this.mSupports.Count >= maxTeamCount)
            {
                goto Label_003E;
            }
            num = this.mSupports.Count;
            goto Label_0032;
        Label_0022:
            this.mSupports.Add(null);
            num += 1;
        Label_0032:
            if (num < maxTeamCount)
            {
                goto Label_0022;
            }
            goto Label_005C;
        Label_003E:
            if (this.mSupports.Count <= maxTeamCount)
            {
                goto Label_005C;
            }
            Enumerable.Take<SupportData>(this.mSupports, maxTeamCount);
        Label_005C:
            return;
        }

        private void ValidateTeam(PartyEditData partyEditData)
        {
            bool flag;
            string[] strArray;
            int num;
            PartySlotData data;
            string[] strArray2;
            int num2;
            flag = 0;
            if (<>f__am$cache9E != null)
            {
                goto Label_0020;
            }
            <>f__am$cache9E = new Func<PartySlotData, bool>(PartyWindow2.<ValidateTeam>m__3A7);
        Label_0020:
            if (<>f__am$cache9F != null)
            {
                goto Label_0042;
            }
            <>f__am$cache9F = new Func<PartySlotData, string>(PartyWindow2.<ValidateTeam>m__3A8);
        Label_0042:
            strArray = Enumerable.ToArray<string>(Enumerable.Select<PartySlotData, string>(Enumerable.Where<PartySlotData>(this.mSlotData, <>f__am$cache9E), <>f__am$cache9F));
            if (this.mCurrentQuest == null)
            {
                goto Label_006F;
            }
            if (this.mCurrentQuest.type == 15)
            {
                goto Label_00B7;
            }
        Label_006F:
            if (this.mSlotData[0].Type != null)
            {
                goto Label_00B7;
            }
            if (partyEditData.Units[0] != null)
            {
                goto Label_00B7;
            }
            flag |= PartyUtility.AutoSetLeaderUnit(this.mCurrentQuest, partyEditData, strArray, MonoSingleton<GameManager>.Instance.Player.Units, this.mSlotData);
        Label_00B7:
            num = 0;
            goto Label_0141;
        Label_00BE:
            data = this.mSlotData[num];
            if (data.Type == 4)
            {
                goto Label_0106;
            }
            if (data.Type == 5)
            {
                goto Label_0106;
            }
            if (data.Type == 3)
            {
                goto Label_0106;
            }
            if (data.Type != 1)
            {
                goto Label_0114;
            }
            if (data.IsSettable != null)
            {
                goto Label_0114;
            }
        Label_0106:
            partyEditData.Units[num] = null;
            goto Label_013D;
        Label_0114:
            if (data.Type != 2)
            {
                goto Label_013D;
            }
            partyEditData.Units[num] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(data.UnitName);
        Label_013D:
            num += 1;
        Label_0141:
            if (num >= ((int) partyEditData.Units.Length))
            {
                goto Label_0160;
            }
            if (num < this.mSlotData.Count)
            {
                goto Label_00BE;
            }
        Label_0160:
            if (this.mCurrentQuest == null)
            {
                goto Label_027E;
            }
            if (this.mCurrentQuest.type == 5)
            {
                goto Label_01A0;
            }
            if (this.mCurrentQuest.type == 10)
            {
                goto Label_01A0;
            }
            if (this.mCurrentQuest.type != 13)
            {
                goto Label_01C0;
            }
        Label_01A0:
            flag |= PartyUtility.SetUnitIfEmptyParty(this.mCurrentQuest, this.mTeams, strArray, this.mSlotData);
            goto Label_027E;
        Label_01C0:
            if (PartyUtility.IsHeloQuest(this.mCurrentQuest) == null)
            {
                goto Label_027E;
            }
            if (<>f__am$cacheA0 != null)
            {
                goto Label_01EE;
            }
            <>f__am$cacheA0 = new Func<PartySlotData, bool>(PartyWindow2.<ValidateTeam>m__3A9);
        Label_01EE:
            if (<>f__am$cacheA1 != null)
            {
                goto Label_0210;
            }
            <>f__am$cacheA1 = new Func<PartySlotData, string>(PartyWindow2.<ValidateTeam>m__3AA);
        Label_0210:
            strArray2 = Enumerable.ToArray<string>(Enumerable.Select<PartySlotData, string>(Enumerable.Where<PartySlotData>(this.mSlotData, <>f__am$cacheA0), <>f__am$cacheA1));
            num2 = 0;
            goto Label_0246;
        Label_0229:
            flag |= PartyUtility.PartyUnitsRemoveHelo(this.mTeams[num2], strArray2);
            num2 += 1;
        Label_0246:
            if (num2 < this.mTeams.Count)
            {
                goto Label_0229;
            }
            if (this.mIsHeloOnly != null)
            {
                goto Label_027E;
            }
            flag |= PartyUtility.SetUnitIfEmptyParty(this.mCurrentQuest, this.mTeams, strArray, this.mSlotData);
        Label_027E:
            if (flag == null)
            {
                goto Label_028A;
            }
            this.SaveTeamPresets();
        Label_028A:
            return;
        }

        [DebuggerHidden]
        private IEnumerator WaitForSave()
        {
            <WaitForSave>c__Iterator12D iteratord;
            iteratord = new <WaitForSave>c__Iterator12D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        public EditPartyTypes CurrentPartyType
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

        private Transform TrHomeHeader
        {
            get
            {
                Scene scene;
                GameObject[] objArray;
                GameObject obj2;
                GameObject[] objArray2;
                int num;
                HomeWindow window;
                if ((this.mTrHomeHeader == null) == null)
                {
                    goto Label_0081;
                }
                scene = SceneManager.GetSceneByName(this.SceneNameHome);
                if (&scene.IsValid() == null)
                {
                    goto Label_0081;
                }
                objArray = &scene.GetRootGameObjects();
                if (objArray == null)
                {
                    goto Label_0081;
                }
                objArray2 = objArray;
                num = 0;
                goto Label_0077;
            Label_0041:
                obj2 = objArray2[num];
                window = obj2.GetComponent<HomeWindow>();
                if (window != null)
                {
                    goto Label_005F;
                }
                goto Label_0071;
            Label_005F:
                this.mTrHomeHeader = window.get_transform();
                goto Label_0081;
            Label_0071:
                num += 1;
            Label_0077:
                if (num < ((int) objArray2.Length))
                {
                    goto Label_0041;
                }
            Label_0081:
                return this.mTrHomeHeader;
            }
        }

        private bool IsPartyDirty
        {
            get
            {
                PlayerData data;
                PlayerPartyTypes types;
                PartyData data2;
                int num;
                long num2;
                data = MonoSingleton<GameManager>.Instance.Player;
                types = SRPG_Extensions.ToPlayerPartyType(this.mCurrentPartyType);
                data2 = data.FindPartyOfType(types);
                num = 0;
                goto Label_0067;
            Label_0026:
                num2 = (this.mCurrentParty.Units[num] == null) ? 0L : this.mCurrentParty.Units[num].UniqueID;
                if (data2.GetUnitUniqueID(num) == num2)
                {
                    goto Label_0063;
                }
                return 1;
            Label_0063:
                num += 1;
            Label_0067:
                if (num < data2.MAX_UNIT)
                {
                    goto Label_0026;
                }
                return 0;
            }
        }

        private bool InventoryDirty
        {
            get
            {
                PlayerData data;
                int num;
                ItemData data2;
                data = MonoSingleton<GameManager>.Instance.Player;
                num = 0;
                goto Label_0057;
            Label_0012:
                data2 = null;
                if (num >= ((int) this.mCurrentItems.Length))
                {
                    goto Label_0043;
                }
                if (this.mCurrentItems[num] == null)
                {
                    goto Label_0043;
                }
                data2 = data.FindItemDataByItemParam(this.mCurrentItems[num].Param);
            Label_0043:
                if (data.Inventory[num] == data2)
                {
                    goto Label_0053;
                }
                return 1;
            Label_0053:
                num += 1;
            Label_0057:
                if (num < 5)
                {
                    goto Label_0012;
                }
                return 0;
            }
        }

        private bool TeamsAvailable
        {
            get
            {
                return ((this.mCurrentPartyType == 1) ? 1 : (this.mCurrentPartyType == 2));
            }
        }

        protected virtual int AvailableMainMemberSlots
        {
            get
            {
                return this.mCurrentParty.PartyData.MAX_MAINMEMBER;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateSlots>c__AnonStorey372
        {
            internal int index;
            internal PartyWindow2.<CreateSlots>c__AnonStorey373 <>f__ref$883;

            public <CreateSlots>c__AnonStorey372()
            {
                base..ctor();
                return;
            }

            internal string <>m__398()
            {
                int num;
                if (this.index >= ((int) this.<>f__ref$883.cond.unit.Length))
                {
                    goto Label_0040;
                }
                return this.<>f__ref$883.cond.unit[this.index++];
            Label_0040:
                return null;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateSlots>c__AnonStorey373
        {
            internal QuestCondParam cond;

            public <CreateSlots>c__AnonStorey373()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <OnForwardOrBackButtonClick>c__AnonStorey36E
        {
            internal int coin;
            internal string msg;
            internal PartyWindow2 <>f__this;
            private static UIUtility.DialogResultEvent <>f__am$cache3;

            public <OnForwardOrBackButtonClick>c__AnonStorey36E()
            {
                base..ctor();
                return;
            }

            internal void <>m__386(GameObject g)
            {
                if (MonoSingleton<GameManager>.Instance.Player.Coin >= this.coin)
                {
                    goto Label_005C;
                }
                this.msg = LocalizedText.Get("sys.OUTOFCOIN");
                if (<>f__am$cache3 != null)
                {
                    goto Label_0049;
                }
                <>f__am$cache3 = new UIUtility.DialogResultEvent(PartyWindow2.<OnForwardOrBackButtonClick>c__AnonStorey36E.<>m__3B5);
            Label_0049:
                UIUtility.SystemMessage(null, this.msg, <>f__am$cache3, null, 0, -1);
                goto Label_0088;
            Label_005C:
                Network.RequestAPI(new ReqBtlComReset(this.<>f__this.mCurrentQuest.iname, new Network.ResponseCallback(this.<>f__this.OnResetChallenge)), 0);
            Label_0088:
                return;
            }

            private static void <>m__3B5(GameObject gob)
            {
            }
        }

        [CompilerGenerated]
        private sealed class <OnForwardOrBackButtonClick>c__AnonStorey36F
        {
            internal string[] force_units;
            internal PartyWindow2 <>f__this;

            public <OnForwardOrBackButtonClick>c__AnonStorey36F()
            {
                base..ctor();
                return;
            }

            internal void <>m__389(GameObject dialog)
            {
                this.<>f__this.OpenQuestDetail();
                return;
            }

            internal bool <>m__390(string p)
            {
                return (p == this.<>f__this.mCurrentQuest.iname);
            }

            internal void <>m__391(GameObject dialog)
            {
                this.<>f__this.PostForwardPressed();
                return;
            }

            internal void <>m__392(GameObject dialog)
            {
                this.<>f__this.PostForwardPressed();
                return;
            }

            internal void <>m__393(GameObject dialog)
            {
                this.<>f__this.PostForwardPressed();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <OnForwardOrBackButtonClick>c__AnonStorey370
        {
            internal int i;
            internal PartyWindow2.<OnForwardOrBackButtonClick>c__AnonStorey36F <>f__ref$879;

            public <OnForwardOrBackButtonClick>c__AnonStorey370()
            {
                base..ctor();
                return;
            }

            internal bool <>m__38C(UnitData u)
            {
                return (u.UnitParam.iname == this.<>f__ref$879.force_units[this.i]);
            }
        }

        [CompilerGenerated]
        private sealed class <OnGetItemListItem>c__AnonStorey369
        {
            internal int id;
            internal PartyWindow2 <>f__this;

            public <OnGetItemListItem>c__AnonStorey369()
            {
                base..ctor();
                return;
            }

            internal bool <>m__37B(ItemData p)
            {
                return ((p == null) ? 0 : (p.Param == this.<>f__this.mOwnItems[this.id - 1].Param));
            }
        }

        [CompilerGenerated]
        private sealed class <OrganizeRecommendedParty>c__AnonStorey376
        {
            internal List<string> removeTarget;

            public <OrganizeRecommendedParty>c__AnonStorey376()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3B1(UnitData unit)
            {
                return (this.removeTarget.Contains(unit.UnitID) == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <OrganizeRecommendedParty>c__AnonStorey377
        {
            internal List<Comparison<UnitData>> targetComparators;

            public <OrganizeRecommendedParty>c__AnonStorey377()
            {
                base..ctor();
                return;
            }

            internal unsafe int <>m__3B2(UnitData x, UnitData y)
            {
                Comparison<UnitData> comparison;
                List<Comparison<UnitData>>.Enumerator enumerator;
                int num;
                int num2;
                enumerator = this.targetComparators.GetEnumerator();
            Label_000C:
                try
                {
                    goto Label_002F;
                Label_0011:
                    comparison = &enumerator.Current;
                    num = comparison(x, y);
                    if (num == null)
                    {
                        goto Label_002F;
                    }
                    num2 = num;
                    goto Label_0054;
                Label_002F:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0011;
                    }
                    goto Label_004C;
                }
                finally
                {
                Label_0040:
                    ((List<Comparison<UnitData>>.Enumerator) enumerator).Dispose();
                }
            Label_004C:
                return UnitData.CompareTo_Iname(x, y);
            Label_0054:
                return num2;
            }
        }

        [CompilerGenerated]
        private sealed class <PopulateItemList>c__Iterator12B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal RectTransform <pool>__0;
            internal Stopwatch <sw>__1;
            internal int <i>__2;
            internal ListItemEvents <newItem>__3;
            internal int $PC;
            internal object $current;
            internal PartyWindow2 <>f__this;

            public <PopulateItemList>c__Iterator12B()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0110;
                }
                goto Label_014B;
            Label_0021:
                if ((this.<>f__this.ItemList == null) != null)
                {
                    goto Label_014B;
                }
                if ((this.<>f__this.ItemListItem == null) == null)
                {
                    goto Label_0052;
                }
                goto Label_014B;
            Label_0052:
                this.<pool>__0 = UIUtility.Pool;
                this.<sw>__1 = new Stopwatch();
                this.<sw>__1.Start();
                this.<i>__2 = this.<>f__this.mItemPoolA.Count;
                goto Label_0129;
            Label_008E:
                this.<newItem>__3 = this.<>f__this.CreateItemListItem();
                this.<>f__this.mItemPoolA.Add(this.<newItem>__3.GetComponent<RectTransform>());
                this.<newItem>__3.get_transform().SetParent(this.<pool>__0, 0);
                this.<sw>__1.Stop();
                if (this.<sw>__1.ElapsedTicks < 0x4e20L)
                {
                    goto Label_0110;
                }
                this.<sw>__1.Reset();
                this.$current = null;
                this.$PC = 1;
                goto Label_014D;
            Label_0110:
                this.<sw>__1.Start();
                this.<i>__2 += 1;
            Label_0129:
                if (this.<i>__2 < this.<>f__this.ItemList.NumVisibleItems)
                {
                    goto Label_008E;
                }
                this.$PC = -1;
            Label_014B:
                return 0;
            Label_014D:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshItemList>c__AnonStorey36A
        {
            internal ItemData currentItem;
            internal PartyWindow2 <>f__this;

            public <RefreshItemList>c__AnonStorey36A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__37C(ItemData p)
            {
                return (p.Param == this.currentItem.Param);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshItemList>c__AnonStorey36B
        {
            internal int i;
            internal PartyWindow2 <>f__this;

            public <RefreshItemList>c__AnonStorey36B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__37D(ItemData p)
            {
                return (p.Param == this.<>f__this.mCurrentItems[this.i].Param);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUnitList>c__AnonStorey374
        {
            internal string chQuestHeroId;

            public <RefreshUnitList>c__AnonStorey374()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3AD(UnitData u)
            {
                return (u.UnitParam.iname == this.chQuestHeroId);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUnits>c__AnonStorey36C
        {
            internal string chQuestHeroId;

            public <RefreshUnits>c__AnonStorey36C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__380(UnitData u)
            {
                return (u.UnitParam.iname == this.chQuestHeroId);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUnits>c__AnonStorey36D
        {
            internal int i;
            internal PartyWindow2 <>f__this;

            public <RefreshUnits>c__AnonStorey36D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__381(UnitData v)
            {
                return (v.UniqueID == this.<>f__this.mCurrentParty.Units[this.i].UniqueID);
            }

            internal bool <>m__382(UnitData v)
            {
                return (v.UniqueID == this.<>f__this.mCurrentParty.Units[this.i].UniqueID);
            }
        }

        [CompilerGenerated]
        private sealed class <SaveAndActivatePin>c__AnonStorey371
        {
            internal int pinID;
            internal PartyWindow2 <>f__this;

            public <SaveAndActivatePin>c__AnonStorey371()
            {
                base..ctor();
                return;
            }

            internal void <>m__396()
            {
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, this.pinID);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <SeparateUnitByElement>c__AnonStorey375
        {
            internal UnitData unit;

            public <SeparateUnitByElement>c__AnonStorey375()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3B0(string iname)
            {
                return (iname == this.unit.UnitParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowRaidResultAsync>c__Iterator12C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal RaidResultWindow <window>__0;
            internal int $PC;
            internal object $current;
            internal PartyWindow2 <>f__this;

            public <ShowRaidResultAsync>c__Iterator12C()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_0070;

                    case 2:
                        goto Label_00EA;

                    case 3:
                        goto Label_016E;
                }
                goto Label_0175;
            Label_0029:
                if (this.<>f__this.mReqRaidResultWindow == null)
                {
                    goto Label_009C;
                }
                if (this.<>f__this.mReqRaidResultWindow.isDone != null)
                {
                    goto Label_0070;
                }
                this.$current = this.<>f__this.mReqRaidResultWindow.StartCoroutine();
                this.$PC = 1;
                goto Label_0177;
            Label_0070:
                this.<>f__this.mRaidResultWindow = this.<>f__this.mReqRaidResultWindow.asset as RaidResultWindow;
                this.<>f__this.mReqRaidResultWindow = null;
            Label_009C:
                if ((this.<>f__this.mRaidResultWindow == null) == null)
                {
                    goto Label_00C1;
                }
                DebugUtility.LogError("mRaidResultWindow == null");
                goto Label_0175;
            Label_00C1:
                this.<window>__0 = Object.Instantiate<RaidResultWindow>(this.<>f__this.mRaidResultWindow);
            Label_00D7:
                this.$current = null;
                this.$PC = 2;
                goto Label_0177;
            Label_00EA:
                if ((this.<window>__0 != null) != null)
                {
                    goto Label_00D7;
                }
                GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), null);
                GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
                GlobalEvent.Invoke(((PredefinedGlobalEvents) 6).ToString(), null);
                this.<>f__this.RefreshRaidTicketNum();
                this.<>f__this.RefreshRaidButtons();
                this.<>f__this.RecalcTotalAttack();
                this.<>f__this.LockWindow(0);
                this.$current = null;
                this.$PC = 3;
                goto Label_0177;
            Label_016E:
                this.$PC = -1;
            Label_0175:
                return 0;
            Label_0177:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__Iterator12A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal PlayerData <player>__0;
            internal int <i>__1;
            internal int <i>__2;
            internal int <i>__3;
            internal RectTransform <dir_cut>__4;
            internal int <i>__5;
            internal PartyData <PartyData>__6;
            internal int $PC;
            internal object $current;
            internal PartyWindow2 <>f__this;

            public <Start>c__Iterator12A()
            {
                base..ctor();
                return;
            }

            internal void <>m__3B3(SRPG_Button button)
            {
                this.<>f__this.OpenQuestDetail();
                return;
            }

            internal void <>m__3B4(SRPG_Button button)
            {
                this.<>f__this.OpenMapEffectQuest();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                GameManager local2;
                SRPG_ToggleButton[] buttonArray1;
                GameManager local1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0976;
                }
                goto Label_0994;
            Label_0021:
                this.<>f__this.RefreshQuest();
                this.<player>__0 = MonoSingleton<GameManager>.Instance.Player;
                local1 = MonoSingleton<GameManager>.Instance;
                local1.OnStaminaChange = (GameManager.StaminaChangeEvent) Delegate.Combine(local1.OnStaminaChange, new GameManager.StaminaChangeEvent(this.<>f__this.OnStaminaChange));
                if ((this.<>f__this.Prefab_SankaFuka != null) == null)
                {
                    goto Label_00AD;
                }
                if (this.<>f__this.Prefab_SankaFuka.get_gameObject().get_activeInHierarchy() == null)
                {
                    goto Label_00AD;
                }
                this.<>f__this.Prefab_SankaFuka.get_gameObject().SetActive(0);
            Label_00AD:
                if ((this.<>f__this.UnitSlotTemplate != null) == null)
                {
                    goto Label_00D9;
                }
                this.<>f__this.UnitSlotTemplate.get_gameObject().SetActive(0);
            Label_00D9:
                if ((this.<>f__this.NpcSlotTemplate != null) == null)
                {
                    goto Label_0105;
                }
                this.<>f__this.NpcSlotTemplate.get_gameObject().SetActive(0);
            Label_0105:
                if ((this.<>f__this.QuestInfoButton != null) == null)
                {
                    goto Label_01C6;
                }
                this.<>f__this.QuestInfoButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>m__3B3));
                if (this.<>f__this.mCurrentQuest != null)
                {
                    goto Label_014C;
                }
                goto Label_01C6;
            Label_014C:
                if (this.<>f__this.mCurrentQuest.IsMulti == null)
                {
                    goto Label_0196;
                }
                if (string.IsNullOrEmpty(this.<>f__this.QuestDetailMulti) != null)
                {
                    goto Label_01C6;
                }
                this.<>f__this.mReqQuestDetail = AssetManager.LoadAsync<GameObject>(this.<>f__this.QuestDetailMulti);
                goto Label_01C6;
            Label_0196:
                if (string.IsNullOrEmpty(this.<>f__this.QuestDetail) != null)
                {
                    goto Label_01C6;
                }
                this.<>f__this.mReqQuestDetail = AssetManager.LoadAsync<GameObject>(this.<>f__this.QuestDetail);
            Label_01C6:
                if ((this.<>f__this.ButtonMapEffectQuest != null) == null)
                {
                    goto Label_0238;
                }
                this.<>f__this.ButtonMapEffectQuest.AddListener(new SRPG_Button.ButtonClickEvent(this.<>m__3B4));
                if (this.<>f__this.mCurrentQuest == null)
                {
                    goto Label_0238;
                }
                if (string.IsNullOrEmpty(this.<>f__this.PrefabMapEffectQuest) != null)
                {
                    goto Label_0238;
                }
                this.<>f__this.mReqMapEffectQuest = AssetManager.LoadAsync<GameObject>(this.<>f__this.PrefabMapEffectQuest);
            Label_0238:
                if ((this.<>f__this.CloseItemList != null) == null)
                {
                    goto Label_026F;
                }
                this.<>f__this.CloseItemList.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnCloseItemListClick));
            Label_026F:
                if ((this.<>f__this.Raid != null) == null)
                {
                    goto Label_02A6;
                }
                this.<>f__this.Raid.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnRaidClick));
            Label_02A6:
                if ((this.<>f__this.RaidN != null) == null)
                {
                    goto Label_02DD;
                }
                this.<>f__this.RaidN.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnRaidClick));
            Label_02DD:
                if ((this.<>f__this.UnitListHilit != null) == null)
                {
                    goto Label_0324;
                }
                this.<>f__this.MoveToOrigin(this.<>f__this.UnitListHilit.get_gameObject());
                this.<>f__this.UnitListHilit.get_gameObject().SetActive(0);
            Label_0324:
                if ((this.<>f__this.ItemListHilit != null) == null)
                {
                    goto Label_036B;
                }
                this.<>f__this.MoveToOrigin(this.<>f__this.ItemListHilit.get_gameObject());
                this.<>f__this.ItemListHilit.get_gameObject().SetActive(0);
            Label_036B:
                if ((this.<>f__this.ItemListItem != null) == null)
                {
                    goto Label_0397;
                }
                this.<>f__this.ItemListItem.get_gameObject().SetActive(0);
            Label_0397:
                if ((this.<>f__this.ItemRemoveItem != null) == null)
                {
                    goto Label_03E4;
                }
                this.<>f__this.ItemRemoveItem.get_gameObject().SetActive(0);
                this.<>f__this.ItemRemoveItem.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnItemRemoveSelect));
            Label_03E4:
                if ((this.<>f__this.AddMainUnitOverlay != null) == null)
                {
                    goto Label_0421;
                }
                this.<>f__this.AddMainUnitOverlay.SetActive(0);
                this.<>f__this.MoveToOrigin(this.<>f__this.AddMainUnitOverlay);
            Label_0421:
                if ((this.<>f__this.AddSubUnitOverlay != null) == null)
                {
                    goto Label_045E;
                }
                this.<>f__this.AddSubUnitOverlay.SetActive(0);
                this.<>f__this.MoveToOrigin(this.<>f__this.AddSubUnitOverlay);
            Label_045E:
                if ((this.<>f__this.AddItemOverlay != null) == null)
                {
                    goto Label_049B;
                }
                this.<>f__this.AddItemOverlay.SetActive(0);
                this.<>f__this.MoveToOrigin(this.<>f__this.AddItemOverlay);
            Label_049B:
                this.<i>__1 = 0;
                goto Label_0511;
            Label_04A7:
                if ((this.<>f__this.ChosenUnitBadges[this.<i>__1] != null) == null)
                {
                    goto Label_0503;
                }
                this.<>f__this.MoveToOrigin(this.<>f__this.ChosenUnitBadges[this.<i>__1].get_gameObject());
                this.<>f__this.ChosenUnitBadges[this.<i>__1].get_gameObject().SetActive(0);
            Label_0503:
                this.<i>__1 += 1;
            Label_0511:
                if (this.<i>__1 < ((int) this.<>f__this.ChosenUnitBadges.Length))
                {
                    goto Label_04A7;
                }
                this.<i>__2 = 0;
                goto Label_059F;
            Label_0535:
                if ((this.<>f__this.ChosenItemBadges[this.<i>__2] != null) == null)
                {
                    goto Label_0591;
                }
                this.<>f__this.MoveToOrigin(this.<>f__this.ChosenItemBadges[this.<i>__2].get_gameObject());
                this.<>f__this.ChosenItemBadges[this.<i>__2].get_gameObject().SetActive(0);
            Label_0591:
                this.<i>__2 += 1;
            Label_059F:
                if (this.<i>__2 < ((int) this.<>f__this.ChosenItemBadges.Length))
                {
                    goto Label_0535;
                }
                if ((this.<>f__this.ChosenSupportBadge != null) == null)
                {
                    goto Label_05F8;
                }
                this.<>f__this.ChosenSupportBadge.get_gameObject().SetActive(0);
                this.<>f__this.ChosenSupportBadge.set_anchoredPosition(Vector2.get_zero());
            Label_05F8:
                this.<>f__this.UnitList_Create();
                buttonArray1 = new SRPG_ToggleButton[] { this.<>f__this.ItemFilter_All, this.<>f__this.ItemFilter_Offense, this.<>f__this.ItemFilter_Support };
                this.<>f__this.mItemFilterToggles = buttonArray1;
                this.<i>__3 = 0;
                goto Label_06BD;
            Label_064A:
                if ((this.<>f__this.mItemFilterToggles[this.<i>__3] != null) == null)
                {
                    goto Label_06AF;
                }
                this.<>f__this.mItemFilterToggles[this.<i>__3].AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnItemFilterChange));
                this.<>f__this.mItemFilterToggles[this.<i>__3].IsOn = this.<i>__3 == 0;
            Label_06AF:
                this.<i>__3 += 1;
            Label_06BD:
                if (this.<i>__3 < ((int) this.<>f__this.mItemFilterToggles.Length))
                {
                    goto Label_064A;
                }
                if ((this.<>f__this.ItemList != null) == null)
                {
                    goto Label_070C;
                }
                this.<>f__this.ItemList.OnGetItemObject = new VirtualList.GetItemObjectEvent(this.<>f__this.OnGetItemListItem);
            Label_070C:
                if ((this.<>f__this.ForwardButton != null) == null)
                {
                    goto Label_0743;
                }
                this.<>f__this.ForwardButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnForwardOrBackButtonClick));
            Label_0743:
                if ((this.<>f__this.BackButton != null) == null)
                {
                    goto Label_077A;
                }
                this.<>f__this.BackButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnForwardOrBackButtonClick));
            Label_077A:
                if ((this.<>f__this.ToggleDirectineCut == null) == null)
                {
                    goto Label_07D9;
                }
                this.<dir_cut>__4 = this.<>f__this.GetComponents<RectTransform>(this.<>f__this.get_gameObject(), "dir_cut", 1);
                if ((this.<dir_cut>__4 != null) == null)
                {
                    goto Label_07D9;
                }
                this.<>f__this.ToggleDirectineCut = this.<dir_cut>__4.GetComponentInChildren<Toggle>();
            Label_07D9:
                if ((this.<>f__this.ToggleDirectineCut != null) == null)
                {
                    goto Label_0809;
                }
                GameUtility.SetToggle(this.<>f__this.ToggleDirectineCut, GameUtility.Config_DirectionCut.Value);
            Label_0809:
                this.<i>__5 = 0;
                goto Label_0868;
            Label_0815:
                if ((this.<>f__this.ItemSlots[this.<i>__5] != null) == null)
                {
                    goto Label_085A;
                }
                this.<>f__this.ItemSlots[this.<i>__5].OnSelect = new GenericSlot.SelectEvent(this.<>f__this.OnItemSlotClick);
            Label_085A:
                this.<i>__5 += 1;
            Label_0868:
                if (this.<i>__5 < ((int) this.<>f__this.ItemSlots.Length))
                {
                    goto Label_0815;
                }
                if ((this.<>f__this.TeamPulldown != null) == null)
                {
                    goto Label_08B7;
                }
                this.<>f__this.TeamPulldown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.<>f__this.OnTeamChange);
            Label_08B7:
                local2 = MonoSingleton<GameManager>.Instance;
                local2.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Combine(local2.OnSceneChange, new GameManager.SceneChangeEvent(this.<>f__this.OnHomeMenuChange));
                this.<>f__this.mOwnItems = new List<ItemData>(this.<player>__0.Items);
                this.<PartyData>__6 = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(GlobalVars.SelectedPartyIndex.Get());
                this.<>f__this.mSankaFukaIcons = new GameObject[this.<PartyData>__6.MAX_UNIT];
                this.<>f__this.CreateSlots();
                this.<>f__this.Refresh(0);
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.PopulateItemList());
                this.$PC = 1;
                goto Label_0996;
            Label_0976:
                this.<>f__this.GoToUnitList();
                this.<>f__this.mInitialized = 1;
                this.$PC = -1;
            Label_0994:
                return 0;
            Label_0996:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <WaitForSave>c__Iterator12D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal PartyWindow2 <>f__this;

            public <WaitForSave>c__Iterator12D()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0039;
                }
                goto Label_0050;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_0052;
            Label_0039:
                if (this.<>f__this.mIsSaving != null)
                {
                    goto Label_0026;
                }
                this.$PC = -1;
            Label_0050:
                return 0;
            Label_0052:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        private delegate void Callback();

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
            RankMatch
        }

        private enum ItemFilterTypes
        {
            All,
            Offense,
            Support
        }

        public class JSON_ReqBtlComRaidResponse
        {
            public Json_PlayerData player;
            public Json_Item[] items;
            public Json_Unit[] units;
            public Json_BtlRewardConceptCard[] cards;
            public BattleCore.Json_BtlInfo[] btlinfos;

            public JSON_ReqBtlComRaidResponse()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_ReqBtlComResetResponse
        {
            public Json_PlayerData player;
            public JSON_QuestProgress[] quests;

            public JSON_ReqBtlComResetResponse()
            {
                base..ctor();
                return;
            }
        }

        public enum PartySlotType
        {
            Main0,
            Main1,
            Sub0
        }
    }
}

