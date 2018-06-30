namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [Pin(200, "UnitVoiceOff(CleanUp)", 0, 200), Pin(110, "ViewerMode ON", 0, 110), Pin(500, "TIPS表示", 1, 500), Pin(0x191, "ユニット詳細表示更新", 0, 0x191), Pin(0x12e, "真理開眼遷移チェック", 0, 0x12e), Pin(100, "閉じる", 0, 100), Pin(0x12d, "真理開眼開放", 0, 0x12d), Pin(1, "初期化完了", 1, 1), Pin(0x6f, "ViewerMode OFF", 0, 0x6f), Pin(210, "UnitVoiceOff Output", 1, 210), Pin(0xc9, "UnitVoiceOff", 0, 0xc9)]
    public class UnitEnhanceV3 : MonoBehaviour, IFlowInterface
    {
        public const int UNLOCK_TOBIRA = 0x12d;
        public const int CHECK_TOBIRA = 0x12e;
        public const int REFRESH_UNIT_DETAIL = 0x191;
        public const int SHOW_TOBIRA_TIPS = 500;
        private const float ExpAnimSpan = 1f;
        public static UnitEnhanceV3 Instance;
        private List<ItemData> mTmpItems;
        [Space(10f)]
        public bool FastStart;
        public GameObject EquipSlotEffect;
        public GameObject JobChangeButtonEffect;
        public GameObject ParamUpEffect;
        public GameObject ParamDownEffect;
        public Color32 DefaultParamColor;
        public Color32 ParamUpColor;
        public Color32 ParamDownColor;
        public string AbilityRankUpTrigger;
        public GameObject AbilityRankUpEffect;
        public GameObject JobRankUpEffect;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_JobRankUp;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_JobUnlock;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_JobCC;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_NewAbilityList;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_NewSkillList;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_NewSkillList2;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_ReturnItems;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_LeveUp;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_Kakusei;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_Evolution;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_JobChange;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_JobMaster;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_Unlock_Tobira;
        private string PREFAB_PATH_TOBIRA_OPEN;
        private string PREFAB_PATH_TOBIRA_LEVELUP;
        private string PREFAB_PATH_TOBIRA_LEVEL_MAX;
        private GameObject mTobiraLevelupMaxEffectObject;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_UnitPieceWindow;
        [StringIsResourcePath(typeof(GameObject))]
        public string Prefab_ProfileWindow;
        public Tooltip Prefab_LockedJobTooltip;
        public Tooltip Prefab_ParamTooltip;
        [SerializeField]
        private string PREFAB_PATH_CHARA_QUEST_LIST_WINDOW;
        [SerializeField]
        private string PREFAB_PATH_UNITDATA_UNLOCK_POPUP;
        [SerializeField]
        private string PREFAB_PATH_EVOLUTION_WINDOW;
        [SerializeField]
        private string PREFAB_PATH_UNLOCK_TOBIRA_WINDOW;
        [SerializeField]
        private string PREFAB_PATH_ARTIFACT_WINDOW;
        [SerializeField]
        private string PREFAB_PATH_MODEL_VIEWER;
        public SRPG_Button CharaQuestButton;
        public SRPG_Button SkinButton;
        [StringIsResourcePath(typeof(GameObject))]
        public string SkinSelectTemplate;
        [Space(10f)]
        public bool ShowNoStockExpPotions;
        [Space(10f)]
        public RectTransform ActiveJobIndicator;
        public SRPG_ToggleButton JobChangeButton;
        public Text JobRank;
        [Space(10f)]
        public SRPG_ToggleButton Tab_Equipments;
        public SRPG_ToggleButton Tab_Kyoka;
        public SRPG_ToggleButton Tab_AbilityList;
        public SRPG_ToggleButton Tab_AbilitySlot;
        public SRPG_Button UnitKakuseiButton;
        public SRPG_Button UnitEvolutionButton;
        public Button UnitUnlockTobiraButton;
        public Button UnitGoTobiraButton;
        public string UnitKakuseiButtonHilitBool;
        public string UnitEvolutionButtonHilitBool;
        public string UnitTobiraButtonHilitBool;
        public string JobRankUpButtonHilitBool;
        public string AllEquipButtonHilitBool;
        [Space(10f)]
        public RectTransform TabPageParent;
        public UnitEnhancePanel Prefab_Equipments;
        public UnitEnhancePanel Prefab_Kyoka;
        public UnitEnhancePanel Prefab_AbilityList;
        public UnitEnhancePanel Prefab_AbilitySlots;
        public UnitAbilityPicker Prefab_AbilityPicker;
        public GameObject Prefab_IkkatsuEquip;
        [Space(10f)]
        public SRPG_Button NextUnitButton;
        public SRPG_Button PrevUnitButton;
        [Space(10f)]
        public Text Status_HP;
        public Text Status_MP;
        public Text Status_MPIni;
        public Text Status_Atk;
        public Text Status_Def;
        public Text Status_Mag;
        public Text Status_Mnd;
        public Text Status_Rec;
        public Text Status_Dex;
        public Text Status_Spd;
        public Text Status_Cri;
        public Text Status_Luk;
        public Text Status_Mov;
        public Text Status_Jmp;
        public Text Param_Renkei;
        [Space(10f)]
        public Vector3 PreviewWindowDir;
        [SerializeField, Space(10f)]
        private Animator UnitinfoViewAnimator;
        public GameObject UnitInfo;
        public GameObject UnitParamInfo;
        public GameObject JobInfo;
        public GenericSlot LeaderSkillInfo;
        public SRPG_Button LeaderSkillDetailButton;
        public GameObject Prefab_LeaderSkillDetail;
        public GameObject UnitExpInfo;
        public GameObject UnitRarityInfo;
        public SliderAnimator UnitEXPSlider;
        public Text UnitLevel;
        public Color32 UnitLevelColor;
        public Color32 CappedUnitLevelColor;
        public GameObject UnitLevelCapInfo;
        public Text UnitExp;
        public Text UnitExpMax;
        public Text UnitExpNext;
        [Space(10f)]
        public string BGAnimatorID;
        public string BGAnimatorPlayTrigger;
        public string BGUnitImageID;
        [SerializeField]
        private string BGTobiraImageID;
        [SerializeField]
        private string BGTobiraEffectImageID;
        public float BGUnitImageFadeTime;
        public string ExpOverflowWarning;
        public Tooltip Prefab_LockedArtifactSlotTooltip;
        [SerializeField]
        private Transform mUnitModelViewerParent;
        private UnitModelViewer mUnitModelViewer;
        public CanvasGroup LeftGroup;
        public CanvasGroup RightGroup;
        public Toggle Favorite;
        private bool mStarted;
        private Dictionary<int, int[]> mJobSetDatas;
        [SerializeField]
        private JobIconScrollListController mJobIconScrollListController;
        private List<UnitInventoryJobIcon> mUnitJobIconSetList;
        private ScrollClamped_JobIcons mScrollClampedJobIcons;
        private Canvas mOverlayCanvas;
        private Animator mBGAnimator;
        private RawImage mBGUnitImage;
        private Animator mBGTobiraAnimator;
        private Animator mBGTobiraEffectAnimator;
        private long mCurrentUnitID;
        private long mCurrentJobUniqueID;
        private long mIsSetJobUniqueID;
        private UnitData mCurrentUnit;
        private float mLastSyncTime;
        private static readonly string CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH;
        private UnitEnhancePanel mEquipmentPanel;
        private UnitEnhancePanel mKyokaPanel;
        private UnitEnhancePanel mAbilityListPanel;
        private UnitEnhancePanel mAbilitySlotPanel;
        private UnitAbilityPicker mAbilityPicker;
        private UnitEvolutionWindow mEvolutionWindow;
        private UnitUnlockTobiraWindow mUnlockTobiraWindow;
        private GameObject mSkinSelectWindow;
        private JobParam mUnlockJobParam;
        private bool mEquipmentPanelDirty;
        private bool mAbilityListDirty;
        private bool mAbilitySlotDirty;
        private Tooltip mJobUnlockTooltip;
        private int mJobUnlockTooltipIndex;
        private Tooltip mParamTooltip;
        private GameObject mParamTooltipTarget;
        private Tooltip mEquipArtifactUnlockTooltip;
        private int mSelectedEquipmentSlot;
        private UnitPreview mCurrentPreview;
        private List<UnitPreview> mPreviewControllers;
        private List<string> mPreviewJobNames;
        private int mSelectedJobIndex;
        private Dictionary<eTabButtons, SRPG_ToggleButton> mTabButtons;
        private UnitEnhancePanel[] mTabPages;
        private eTabButtons mActiveTabIndex;
        private bool mReloading;
        private Text[] mStatusParamSlots;
        private bool mCloseRequested;
        private float mBGUnitImgAlphaStart;
        private float mBGUnitImgAlphaEnd;
        private float mBGUnitImgFadeTime;
        private float mBGUnitImgFadeTimeMax;
        private UnitEquipmentWindow mEquipmentWindow;
        private UnitKakeraWindow mKakeraWindow;
        private UnitCharacterQuestWindow mCharacterQuestWindow;
        private UnitTobiraInventory mTobiraInventoryWindow;
        private float mExpStart;
        private float mExpEnd;
        private float mExpAnimTime;
        private float mLeftTime;
        private DeferredJob mDefferedCallFunc;
        private float mDefferedCallDelay;
        private long[] mOriginalAbilities;
        private List<long> mRankedUpAbilities;
        private Dictionary<string, int> mUsedExpItems;
        private List<long> mDirtyUnits;
        public CloseEvent OnUserClose;
        private UnitUnlockWindow mUnitUnlockWindow;
        private UnitProfileWindow mUnitProfileWindow;
        private GameObject mLeaderSkillDetail;
        private MySound.Voice mUnitVoice;
        private GameObject mArtifactSelector;
        private UnitJobRankUpConfirm mIkkatsuEquipWindow;
        private UnitData.CharacterQuestUnlockProgress mChQuestProg;
        private string mCurrentUnitUnlocks;
        private long mStartSelectUnitUniqueID;
        [Space(10f)]
        public bool HoldUseItemEnable;
        public bool HoldUseItemLvUpStop;
        private ScrollRect mKyoukaItemScroll;
        private ExpItemTouchController mCurrentKyoukaItemHold;
        private List<UnitData> SortedUnits;
        public static readonly string UNIT_EXPMAX_UI_PATH;
        private List<GameObject> mExpItemGameObjects;
        private bool IsBulk;
        private bool mIsCommon;
        private UnitData mCacheUnitData;
        private List<long> m_UnitList;
        private bool IsStopPlayLeftVoice;
        private static readonly int PLAY_LEFTVOICE_SPAN;
        [Space(10f)]
        public SRPG_Button ButtonMapEffectJob;
        [StringIsResourcePath(typeof(GameObject))]
        public string PrefabMapEffectJob;
        private LoadRequest mReqMapEffectJob;
        public string SceneNameHome;
        public string m_VO_TobiraUnlock;
        public string mVO_TobiraOpenCueID;
        public string mVO_TobiraEnhanceCueID;
        public string mVO_TobiraMaxCueID;
        private Transform mTrHomeHeader;
        private bool mSceneChanging;
        private bool mIsJobLvUpAllEquip;
        private bool mIsJobLvMaxAllEquip;
        private BaseStatus mCurrentStatus;
        private int mCurrentRenkei;
        private long mCurrentEquipConceptCardId;
        [NonSerialized]
        public bool MuteVoice;
        private GameObject ClickItem;
        private bool mSendingKyokaRequest;
        private bool mKeepWindowLocked;
        private bool mJobChangeRequestSent;
        private string mNextJobID;
        private string mPrevJobID;
        private bool mJobRankUpRequestSent;
        private static string[] UnitHourVoices;
        private bool mDesiredPreviewVisibility;
        private bool mUpdatePreviewVisibility;
        private bool mAppPausing;
        private UnitAbilityListItemEvents.ListItemTouchController mCurrentAbilityRankUpHold;
        private bool IsFirstPlay;
        private bool mAbilityRankUpRequestSent;
        private LoadRequest mProfileWindowLoadRequest;
        private int mCacheAwakeLv;
        private bool mKakuseiRequestSent;
        private bool mEvolutionRequestSent;
        private bool mUnlockTobiraRequestSent;
        [CompilerGenerated]
        private static Predicate<EquipData> <>f__am$cacheEB;
        [CompilerGenerated]
        private static Predicate<EquipData> <>f__am$cacheEC;
        [CompilerGenerated]
        private static Comparison<int> <>f__am$cacheED;

        static UnitEnhanceV3()
        {
            string[] textArray1;
            Instance = null;
            CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH = "UI/ConceptCardSelect";
            UNIT_EXPMAX_UI_PATH = "UI/UnitLevelupWindow";
            PLAY_LEFTVOICE_SPAN = 60;
            textArray1 = new string[] { 
                "chara_0007", "chara_0007", "chara_0007", "chara_0007", "chara_0002", "chara_0002", "chara_0002", "chara_0002", "chara_0003", "chara_0003", "chara_0003", "chara_0003", "chara_0004", "chara_0004", "chara_0004", "chara_0004",
                "chara_0005", "chara_0005", "chara_0005", "chara_0005", "chara_0006", "chara_0006", "chara_0006", "chara_0006"
            };
            UnitHourVoices = textArray1;
            return;
        }

        public UnitEnhanceV3()
        {
            this.mTmpItems = new List<ItemData>();
            this.FastStart = 1;
            this.DefaultParamColor = Color.get_white();
            this.ParamUpColor = Color.get_green();
            this.ParamDownColor = Color.get_red();
            this.AbilityRankUpTrigger = "on";
            this.PREFAB_PATH_TOBIRA_OPEN = "UI/TobiraOpenEffect";
            this.PREFAB_PATH_TOBIRA_LEVELUP = "UI/TobiraEnhanceEffect";
            this.PREFAB_PATH_TOBIRA_LEVEL_MAX = "UI/TobiraEnhanceEffectMax";
            this.PREFAB_PATH_CHARA_QUEST_LIST_WINDOW = "UI/UnitCharacterQuestWindow";
            this.PREFAB_PATH_UNITDATA_UNLOCK_POPUP = "UI/QuestClearUnlockPopup";
            this.PREFAB_PATH_EVOLUTION_WINDOW = "UI/UnitEvolutionWindow3";
            this.PREFAB_PATH_UNLOCK_TOBIRA_WINDOW = "UI/TobiraOpenCheckWindow";
            this.PREFAB_PATH_ARTIFACT_WINDOW = "UI/ArtiSelect";
            this.PREFAB_PATH_MODEL_VIEWER = "UI/UnitModelViewer";
            this.ShowNoStockExpPotions = 1;
            this.PreviewWindowDir = new Vector3(0f, 0.5f, -1f);
            this.UnitLevelColor = new Color32(0xff, 0xff, 0xff, 0xff);
            this.CappedUnitLevelColor = new Color32(0xff, 0, 0, 0xff);
            this.BGAnimatorPlayTrigger = "play";
            this.BGTobiraImageID = "OPEN_BG_CANVAS";
            this.BGTobiraEffectImageID = "OPEN_BG_EFFECT";
            this.mJobSetDatas = new Dictionary<int, int[]>();
            this.mPreviewControllers = new List<UnitPreview>();
            this.mPreviewJobNames = new List<string>();
            this.mOriginalAbilities = new long[5];
            this.mRankedUpAbilities = new List<long>();
            this.mUsedExpItems = new Dictionary<string, int>();
            this.mDirtyUnits = new List<long>();
            this.mStartSelectUnitUniqueID = -1L;
            this.mExpItemGameObjects = new List<GameObject>();
            this.m_UnitList = new List<long>();
            this.SceneNameHome = "Home";
            this.mCacheAwakeLv = 1;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator _OpenProfileWindow()
        {
            <_OpenProfileWindow>c__Iterator158 iterator;
            iterator = new <_OpenProfileWindow>c__Iterator158();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator _TobiraUIActive(bool is_active, bool is_immediate)
        {
            <_TobiraUIActive>c__Iterator15C iteratorc;
            iteratorc = new <_TobiraUIActive>c__Iterator15C();
            iteratorc.is_immediate = is_immediate;
            iteratorc.is_active = is_active;
            iteratorc.<$>is_immediate = is_immediate;
            iteratorc.<$>is_active = is_active;
            iteratorc.<>f__this = this;
            return iteratorc;
        }

        [CompilerGenerated]
        private void <OnEquipCardSlotSelect>m__44A(GameObject go)
        {
            this.OnCloseEquipConceptCardWindow();
            return;
        }

        [CompilerGenerated]
        private static bool <OnJobRankUpClick>m__44C(EquipData eq)
        {
            return (eq.IsEquiped() == 0);
        }

        [CompilerGenerated]
        private void <OnJobRankUpClick>m__44D(GameObject go)
        {
            this.StartJobRankUp(-1, 0, 0);
            return;
        }

        [CompilerGenerated]
        private static int <RefreshArtifactSlots>m__453(int x, int y)
        {
            return (x - y);
        }

        [CompilerGenerated]
        private bool <RefreshJobIcon>m__44F(JobData job)
        {
            return (job.UniqueID == this.mCurrentJobUniqueID);
        }

        [CompilerGenerated]
        private bool <RefreshUnitShiftButton>m__454(UnitData unit)
        {
            return (unit.UniqueID == this.mCurrentUnit.UniqueID);
        }

        [CompilerGenerated]
        private static bool <UpdateJobRankUpButtonState>m__44E(EquipData eq)
        {
            return (eq.IsEquiped() == 0);
        }

        [DebuggerHidden]
        private IEnumerator AbilityRankUpSkillUnlockEffect()
        {
            <AbilityRankUpSkillUnlockEffect>c__Iterator156 iterator;
            iterator = new <AbilityRankUpSkillUnlockEffect>c__Iterator156();
            iterator.<>f__this = this;
            return iterator;
        }

        private void AcceptChangeSkinByEquipConceptCard(GameObject obj)
        {
            ArtifactParam[] paramArray;
            paramArray = this.mCurrentUnit.GetEnableConceptCardSkins(-1);
            if (((int) paramArray.Length) > 0)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            this.OnSkinSelect(paramArray[0]);
            this.OnSkinDecideAll(paramArray[0]);
            return;
        }

        public void Activated(int pinID)
        {
            GameObject obj2;
            GameObject obj3;
            Canvas canvas;
            int num;
            num = pinID;
            if (num == 110)
            {
                goto Label_006D;
            }
            if (num == 0x6f)
            {
                goto Label_01B2;
            }
            if (num == 200)
            {
                goto Label_0235;
            }
            if (num == 0xc9)
            {
                goto Label_0235;
            }
            if (num == 0x12d)
            {
                goto Label_024B;
            }
            if (num == 0x12e)
            {
                goto Label_0256;
            }
            if (num == 100)
            {
                goto Label_0056;
            }
            if (num == 0x191)
            {
                goto Label_0263;
            }
            goto Label_02CC;
        Label_0056:
            this.mCloseRequested = 1;
            base.GetComponent<WindowController>().Close();
            goto Label_02CC;
        Label_006D:
            this.UpdateMainUIAnimator(0);
            if ((this.LeftGroup != null) == null)
            {
                goto Label_0091;
            }
            this.LeftGroup.set_blocksRaycasts(0);
        Label_0091:
            if ((this.RightGroup != null) == null)
            {
                goto Label_00AE;
            }
            this.RightGroup.set_blocksRaycasts(0);
        Label_00AE:
            if ((this.mUnitModelViewer == null) == null)
            {
                goto Label_0138;
            }
            if ((this.mUnitModelViewerParent != null) == null)
            {
                goto Label_0138;
            }
            if (string.IsNullOrEmpty(this.PREFAB_PATH_MODEL_VIEWER) != null)
            {
                goto Label_0138;
            }
            obj3 = Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.PREFAB_PATH_MODEL_VIEWER));
            this.mUnitModelViewer = obj3.GetComponent<UnitModelViewer>();
            this.mUnitModelViewer.get_transform().SetParent(base.get_transform(), 0);
            this.mUnitModelViewer.get_transform().SetParent(this.mUnitModelViewerParent, 0);
            this.mUnitModelViewer.Initalize();
        Label_0138:
            if ((this.mUnitModelViewer != null) == null)
            {
                goto Label_02CC;
            }
            this.ExecQueuedKyokaRequest(null);
            this.StopUnitVoice();
            this.mUnitModelViewer.OnChangeJobSlot = new UnitModelViewer.ChangeJobSlotEvent(this.ChangeJobSlot);
            this.mUnitModelViewer.OnSkinSelect = new UnitModelViewer.SkinSelectEvent(this.OnSkinSelectOpen);
            this.mUnitModelViewer.OnPlayReaction = new SRPG.UnitModelViewer.PlayReaction(this.PlayReaction);
            this.mUnitModelViewer.get_gameObject().SetActive(1);
            goto Label_02CC;
        Label_01B2:
            if ((this.mUnitModelViewer != null) == null)
            {
                goto Label_01DF;
            }
            this.mUnitModelViewer.ResetTouchControlArea();
            this.mUnitModelViewer.get_gameObject().SetActive(0);
        Label_01DF:
            this.UpdateMainUIAnimator(1);
            if ((this.LeftGroup != null) == null)
            {
                goto Label_0203;
            }
            this.LeftGroup.set_blocksRaycasts(1);
        Label_0203:
            if ((this.RightGroup != null) == null)
            {
                goto Label_0220;
            }
            this.RightGroup.set_blocksRaycasts(1);
        Label_0220:
            this.Refresh(this.mCurrentUnitID, 0L, 0, 0);
            goto Label_02CC;
        Label_0235:
            this.StopUnitVoice();
            FlowNode_GameObject.ActivateOutputLinks(this, 210);
            goto Label_02CC;
        Label_024B:
            this.OpenUnlockTobiraWindow();
            goto Label_02CC;
        Label_0256:
            this.TobiraUIActive(1, 0);
            goto Label_02CC;
        Label_0263:
            canvas = base.GetComponent<Canvas>();
            if ((canvas != null) == null)
            {
                goto Label_02CC;
            }
            if (canvas.get_enabled() == null)
            {
                goto Label_02CC;
            }
            if (this.mCurrentUnitID <= 0L)
            {
                goto Label_02CC;
            }
            this.RefreshEquipments(-1);
            this.UpdateJobRankUpButtonState();
            this.UpdateJobChangeButtonState();
            this.UpdateUnitKakuseiButtonState();
            this.UpdateUnitEvolutionButtonState(1);
            this.UpdateUnitTobiraButtonState();
            this.RefreshAbilitySlotButtonState();
            this.RefreshAbilityList();
            this.RefreshAbilitySlots(0);
        Label_02CC:
            return;
        }

        private void AddParamTooltip(GameObject go)
        {
            UIEventListener listener;
            if ((go != null) == null)
            {
                goto Label_0057;
            }
            listener = SRPG_Extensions.RequireComponent<UIEventListener>(go);
            if (listener.onMove != null)
            {
                goto Label_0035;
            }
            listener.onPointerEnter = new UIEventListener.PointerEvent(this.ShowParamTooltip);
            goto Label_0057;
        Label_0035:
            listener.onPointerEnter = (UIEventListener.PointerEvent) Delegate.Combine(listener.onPointerEnter, new UIEventListener.PointerEvent(this.ShowParamTooltip));
        Label_0057:
            return;
        }

        private unsafe void AnimateExp()
        {
            MasterParam param;
            int num;
            float num2;
            float num3;
            float num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            float num11;
            int num12;
            int num13;
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num = this.mCurrentUnit.GetLevelCap(0);
            num2 = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / 1f);
            num3 = Time.get_unscaledDeltaTime();
            this.mExpAnimTime += num3;
            num4 = Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / 1f));
            num5 = Mathf.FloorToInt(num2);
            num6 = Mathf.FloorToInt(num4);
            num7 = param.CalcUnitLevel(num5, num);
            num8 = param.CalcUnitLevel(num6, num);
            if (num7 == num8)
            {
                goto Label_009E;
            }
        Label_009E:
            num9 = param.GetUnitLevelExp(num8);
            num10 = param.GetUnitLevelExp(Mathf.Min(num8 + 1, num)) - num9;
            if ((this.UnitLevel != null) == null)
            {
                goto Label_00E0;
            }
            this.UnitLevel.set_text(&num8.ToString());
        Label_00E0:
            if ((this.UnitExp != null) == null)
            {
                goto Label_010A;
            }
            num12 = num6 - num9;
            this.UnitExp.set_text(&num12.ToString());
        Label_010A:
            if ((this.UnitExpMax != null) == null)
            {
                goto Label_012D;
            }
            this.UnitExpMax.set_text(&num10.ToString());
        Label_012D:
            if ((this.UnitExpNext != null) == null)
            {
                goto Label_0160;
            }
            this.UnitExpNext.set_text(&Mathf.FloorToInt((float) (num10 - (num6 - num9))).ToString());
        Label_0160:
            if ((this.UnitEXPSlider != null) == null)
            {
                goto Label_01A3;
            }
            if (num10 <= 0)
            {
                goto Label_018A;
            }
            num11 = (num4 - ((float) num9)) / ((float) num10);
            goto Label_0191;
        Label_018A:
            num11 = 1f;
        Label_0191:
            this.UnitEXPSlider.AnimateValue(num11, 0f);
        Label_01A3:
            if (this.mExpAnimTime < 1f)
            {
                goto Label_01D0;
            }
            this.mExpStart = this.mExpEnd;
            this.mExpAnimTime = 0f;
            this.RefreshLevelCap();
        Label_01D0:
            return;
        }

        private void AnimateGainExp(int desiredExp)
        {
            if (this.mExpStart >= this.mExpEnd)
            {
                goto Label_0044;
            }
            if (this.mExpAnimTime >= 1f)
            {
                goto Label_0044;
            }
            this.mExpStart = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / 1f);
        Label_0044:
            this.mExpAnimTime = 0f;
            this.mExpEnd = (float) desiredExp;
            return;
        }

        private void AttachOverlay(GameObject go)
        {
            this.CreateOverlay();
            go.get_transform().SetParent(this.mOverlayCanvas.get_transform(), 0);
            return;
        }

        public void BeginStatusChangeCheck()
        {
            this.mCurrentStatus = new BaseStatus(this.mCurrentUnit.Status);
            this.mCurrentRenkei = this.mCurrentUnit.GetCombination();
            this.mCurrentEquipConceptCardId = (this.mCurrentUnit.ConceptCard != null) ? this.mCurrentUnit.ConceptCard.UniqueID : 0L;
            return;
        }

        private void CancelChangeSkinByEquipConceptCard(GameObject obj)
        {
        }

        private void ChangeJobSlot(int index, GameObject target)
        {
            JobSetParam param;
            Tooltip tooltip;
            JobParam param2;
            if (this.mCurrentUnit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (index >= 0)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            if (this.mCurrentUnit.JobIndex == index)
            {
                goto Label_01FD;
            }
            if (this.mCurrentUnit.CheckJobUnlockable(index) != null)
            {
                goto Label_00FB;
            }
            if ((this.Prefab_LockedJobTooltip == null) == null)
            {
                goto Label_0048;
            }
            return;
        Label_0048:
            param = this.mCurrentUnit.GetJobSetParam(index);
            if (param != null)
            {
                goto Label_005C;
            }
            return;
        Label_005C:
            if ((this.mJobUnlockTooltip != null) == null)
            {
                goto Label_008C;
            }
            this.mJobUnlockTooltip.Close();
            this.mJobUnlockTooltip = null;
            if (this.mJobUnlockTooltipIndex != index)
            {
                goto Label_008C;
            }
            return;
        Label_008C:
            if ((target != null) == null)
            {
                goto Label_00B7;
            }
            Tooltip.SetTooltipPosition(target.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
        Label_00B7:
            tooltip = Object.Instantiate<Tooltip>(this.Prefab_LockedJobTooltip);
            DataSource.Bind<JobSetParam>(tooltip.get_gameObject(), param);
            param2 = MonoSingleton<GameManager>.Instance.GetJobParam(param.job);
            DataSource.Bind<JobParam>(tooltip.get_gameObject(), param2);
            this.mJobUnlockTooltip = tooltip;
            this.mJobUnlockTooltipIndex = index;
            return;
        Label_00FB:
            this.ExecQueuedKyokaRequest(null);
            this.mCurrentUnit.SetJobIndex(index);
            this.RefreshBasicParameters(0);
            this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
            this.SetActivePreview(this.mCurrentUnit.CurrentJob.JobID);
            this.mSelectedJobIndex = index;
            this.RefreshJobInfo();
            this.RefreshEquipments(-1);
            this.UpdateJobChangeButtonState();
            this.UpdateJobRankUpButtonState();
            this.RefreshAbilitySlotButtonState();
            if (this.mCurrentUnit.CurrentJob.Rank != null)
            {
                goto Label_01A4;
            }
            if (this.mActiveTabIndex != 3)
            {
                goto Label_01AB;
            }
            this.OnTabChange(this.Tab_Equipments);
            goto Label_01AB;
        Label_01A4:
            this.RefreshAbilitySlots(0);
        Label_01AB:
            this.FadeUnitImage(0f, 0f, 0f);
            base.StartCoroutine(this.RefreshUnitImage());
            this.FadeUnitImage(0f, 1f, this.BGUnitImageFadeTime);
            GlobalVars.SelectedJobUniqueID.Set(this.mCurrentUnit.CurrentJob.UniqueID);
        Label_01FD:
            if ((target == null) == null)
            {
                goto Label_023B;
            }
            this.ScrollClampedJobIcons.Focus(this.mJobIconScrollListController.Items[index].job_icon.get_gameObject(), 0, 0, 0f);
            goto Label_025D;
        Label_023B:
            this.ScrollClampedJobIcons.Focus(target.get_transform().get_parent().get_gameObject(), 0, 0, 0f);
        Label_025D:
            this.UpdateJobSlotStates(1);
            return;
        }

        public unsafe bool CheckEquipArtifactSlot(int slot, JobData job, UnitData unit)
        {
            int num;
            FixParam param;
            if (job == null)
            {
                goto Label_000C;
            }
            if (unit != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            num = unit.AwakeLv;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (param == null)
            {
                goto Label_0044;
            }
            if (param.EquipArtifactSlotUnlock == null)
            {
                goto Label_0044;
            }
            if (((int) param.EquipArtifactSlotUnlock.Length) > 0)
            {
                goto Label_0046;
            }
        Label_0044:
            return 0;
        Label_0046:
            if (((int) param.EquipArtifactSlotUnlock.Length) >= slot)
            {
                goto Label_0056;
            }
            return 0;
        Label_0056:
            return ((*(&(param.EquipArtifactSlotUnlock[slot])) > num) == 0);
        }

        private unsafe void CheckPlayBackUnlock()
        {
            UnitData.UnitPlaybackVoiceData data;
            bool flag;
            UnitData.Json_PlaybackVoiceData data2;
            string str;
            string str2;
            int num;
            int num2;
            string str3;
            Exception exception;
            long num3;
            long num4;
            long num5;
        Label_0000:
            try
            {
                data = this.mCurrentUnit.GetUnitPlaybackVoiceData();
                flag = 0;
                data2 = null;
                if (PlayerPrefsUtility.HasKey(&this.mCurrentUnit.UniqueID.ToString()) != null)
                {
                    goto Label_003B;
                }
                data2 = new UnitData.Json_PlaybackVoiceData();
                flag = 1;
                goto Label_006F;
            Label_003B:
                data2 = JsonUtility.FromJson<UnitData.Json_PlaybackVoiceData>(PlayerPrefsUtility.GetString(&this.mCurrentUnit.UniqueID.ToString(), string.Empty));
                if (data2 != null)
                {
                    goto Label_006F;
                }
                data2 = new UnitData.Json_PlaybackVoiceData();
                flag = 1;
            Label_006F:
                if (data2.playback_voice_unlocked > 0)
                {
                    goto Label_00BC;
                }
                if (this.mCurrentUnit.CheckUnlockPlaybackVoice() == null)
                {
                    goto Label_00BC;
                }
                NotifyList.Push(string.Format(LocalizedText.Get("sys.NOTIFY_UNLOCK_PLAYBACK_FUNCTION"), this.mCurrentUnit.UnitParam.name));
                data2.playback_voice_unlocked = 1;
                flag = 1;
            Label_00BC:
                num = 0;
                num2 = 0;
                goto Label_013A;
            Label_00C7:
                if (data.VoiceCueList[num2].is_new != null)
                {
                    goto Label_00E3;
                }
                goto Label_0134;
            Label_00E3:
                if (data2.cue_names.Contains(&data.VoiceCueList[num2].cueInfo.name) != null)
                {
                    goto Label_0134;
                }
                num += 1;
                data2.cue_names.Add(&data.VoiceCueList[num2].cueInfo.name);
                flag = 1;
            Label_0134:
                num2 += 1;
            Label_013A:
                if (num2 < data.VoiceCueList.Count)
                {
                    goto Label_00C7;
                }
                if (flag == null)
                {
                    goto Label_01AA;
                }
                if (num <= 0)
                {
                    goto Label_0189;
                }
                NotifyList.Push(string.Format(LocalizedText.Get("sys.NOTIFY_UNLOCK_PLAYBACK_UNITVOICE2"), this.mCurrentUnit.UnitParam.name, (int) num));
            Label_0189:
                PlayerPrefsUtility.SetString(&this.mCurrentUnit.UniqueID.ToString(), JsonUtility.ToJson(data2), 0);
            Label_01AA:
                goto Label_01BD;
            }
            catch (Exception exception1)
            {
            Label_01AF:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_01BD;
            }
        Label_01BD:
            return;
        }

        private bool CheckTargetRankAllEquip(int rank)
        {
            bool flag;
            string[] strArray;
            EquipData[] dataArray;
            int num;
            flag = 0;
            strArray = this.mCurrentUnit.Jobs[this.mSelectedJobIndex].GetRankupItems(rank);
            dataArray = new EquipData[6];
            if (strArray == null)
            {
                goto Label_0071;
            }
            if (((int) strArray.Length) <= 0)
            {
                goto Label_0071;
            }
            num = 0;
            goto Label_0050;
        Label_0038:
            dataArray[num] = new EquipData();
            dataArray[num].Setup(strArray[num]);
            num += 1;
        Label_0050:
            if (num < ((int) dataArray.Length))
            {
                goto Label_0038;
            }
            flag = MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.mCurrentUnit, dataArray, null);
        Label_0071:
            return flag;
        }

        private int ClampJobIconIndex(int index)
        {
            if (index < 0)
            {
                goto Label_001B;
            }
            index = index % this.mJobSetDatas.Count;
            goto Label_0050;
        Label_001B:
            index = Mathf.Abs(index);
            index = index % this.mJobSetDatas.Count;
            index = this.mJobSetDatas.Count - index;
            index = index % this.mJobSetDatas.Count;
        Label_0050:
            return index;
        }

        public void ClearDirtyUnits()
        {
            this.mDirtyUnits.Clear();
            return;
        }

        private void CreateOverlay()
        {
            if ((this.mOverlayCanvas == null) == null)
            {
                goto Label_001E;
            }
            this.mOverlayCanvas = UIUtility.PushCanvas(0, -1);
        Label_001E:
            return;
        }

        private void CreateTabPage(eTabButtons tab)
        {
            if (tab != null)
            {
                goto Label_0027;
            }
            this.mEquipmentPanel = this.InitTabPage(tab, this.Prefab_Equipments, 1);
            this.InitEquipmentPanel(this.mEquipmentPanel);
            return;
        Label_0027:
            if (tab != 1)
            {
                goto Label_004F;
            }
            this.mKyokaPanel = this.InitTabPage(tab, this.Prefab_Kyoka, 0);
            this.InitKyokaPanel(this.mKyokaPanel);
            return;
        Label_004F:
            if (tab != 2)
            {
                goto Label_00BF;
            }
            this.mAbilityListPanel = this.InitTabPage(tab, this.Prefab_AbilityList, 0);
            this.mAbilityListPanel.AbilityList.OnAbilityRankUp = new UnitAbilityList.AbilityEvent(this.OnAbilityRankUpSet);
            this.mAbilityListPanel.AbilityList.OnRankUpBtnPress = new UnitAbilityList.AbilityEvent(this.OnRankUpButtonPressHold);
            this.mAbilityListPanel.AbilityList.OnAbilityRankUpExec = new UnitAbilityList.AbilityEvent(this.OnAbilityRankUpExec);
            return;
        Label_00BF:
            if (tab != 3)
            {
                goto Label_0113;
            }
            this.mAbilitySlotPanel = this.InitTabPage(tab, this.Prefab_AbilitySlots, 0);
            this.mAbilitySlotPanel.AbilitySlots.OnAbilityRankUp = new UnitAbilityList.AbilityEvent(this.OnAbilityRankUp);
            this.mAbilitySlotPanel.AbilitySlots.OnSlotSelect = new UnitAbilityList.AbilitySlotEvent(this.OnAbilitySlotSelect);
            return;
        Label_0113:
            return;
        }

        private void DestroyOverlay()
        {
            if ((this.mOverlayCanvas != null) == null)
            {
                goto Label_0021;
            }
            Object.Destroy(this.mOverlayCanvas.get_gameObject());
        Label_0021:
            return;
        }

        [DebuggerHidden]
        private IEnumerator EvolutionButtonClickSync()
        {
            <EvolutionButtonClickSync>c__Iterator15A iteratora;
            iteratora = new <EvolutionButtonClickSync>c__Iterator15A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        private bool ExecQueuedKyokaRequest(DeferredJob func)
        {
            DeferredJob job;
            if (this.mDefferedCallFunc == null)
            {
                goto Label_0050;
            }
            if ((this.mDefferedCallFunc != func) == null)
            {
                goto Label_0050;
            }
            base.GetComponent<WindowController>().SetCollision(0);
            job = this.mDefferedCallFunc;
            this.mDefferedCallFunc = null;
            this.mSendingKyokaRequest = 1;
            this.mDefferedCallDelay = 0f;
            job();
            return 1;
        Label_0050:
            return this.mSendingKyokaRequest;
        }

        private void FadeUnitImage(float alphaStart, float alphaEnd, float duration)
        {
            this.mBGUnitImgAlphaStart = alphaStart;
            this.mBGUnitImgAlphaEnd = alphaEnd;
            this.mBGUnitImgFadeTime = 0f;
            this.mBGUnitImgFadeTimeMax = duration;
            if (duration > 0f)
            {
                goto Label_0037;
            }
            this.SetUnitImageAlpha(this.mBGUnitImgAlphaEnd);
        Label_0037:
            return;
        }

        private void FinishKyokaRequest()
        {
            this.mSendingKyokaRequest = 0;
            this.RefreshSortedUnits();
            this.UpdateJobRankUpButtonState();
            if (this.mKeepWindowLocked != null)
            {
                goto Label_003A;
            }
            if (base.GetComponent<WindowController>().IsOpened == null)
            {
                goto Label_003A;
            }
            base.GetComponent<WindowController>().SetCollision(1);
        Label_003A:
            this.CheckPlayBackUnlock();
            return;
        }

        public long[] GetDirtyUnits()
        {
            long[] numArray;
            return this.mDirtyUnits.ToArray();
        }

        public unsafe int GetUnlockAritfactSlot()
        {
            int num;
            int num2;
            FixParam param;
            int num3;
            num = 0;
            num2 = this.mCurrentUnit.AwakeLv;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (param == null)
            {
                goto Label_0076;
            }
            if (param.EquipArtifactSlotUnlock == null)
            {
                goto Label_0076;
            }
            if (((int) param.EquipArtifactSlotUnlock.Length) <= 0)
            {
                goto Label_0076;
            }
            num3 = 0;
            goto Label_0068;
        Label_0044:
            if (num2 < *(&(param.EquipArtifactSlotUnlock[num3])))
            {
                goto Label_0064;
            }
            num += 1;
        Label_0064:
            num3 += 1;
        Label_0068:
            if (num3 < ((int) param.EquipArtifactSlotUnlock.Length))
            {
                goto Label_0044;
            }
        Label_0076:
            return Mathf.Max(num, 1);
        }

        private ArtifactData[] GetViewArtifact()
        {
            List<ArtifactData> list;
            list = new List<ArtifactData>();
            list.Add(DataSource.FindDataOfClass<ArtifactData>(this.mEquipmentPanel.ArtifactSlot.get_gameObject(), null));
            list.Add(DataSource.FindDataOfClass<ArtifactData>(this.mEquipmentPanel.ArtifactSlot2.get_gameObject(), null));
            list.Add(DataSource.FindDataOfClass<ArtifactData>(this.mEquipmentPanel.ArtifactSlot3.get_gameObject(), null));
            return list.ToArray();
        }

        private void InitEquipmentPanel(UnitEnhancePanel panel)
        {
            int num;
            num = 0;
            goto Label_0037;
        Label_0007:
            if ((panel.EquipmentSlots[num] != null) == null)
            {
                goto Label_0033;
            }
            panel.EquipmentSlots[num].OnSelect = new ListItemEvents.ListItemEvent(this.OnEquipmentSlotSelect);
        Label_0033:
            num += 1;
        Label_0037:
            if (num < ((int) panel.EquipmentSlots.Length))
            {
                goto Label_0007;
            }
            if ((panel.JobRankUpButton != null) == null)
            {
                goto Label_006D;
            }
            panel.JobRankUpButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
        Label_006D:
            if ((panel.JobUnlockButton != null) == null)
            {
                goto Label_0095;
            }
            panel.JobUnlockButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
        Label_0095:
            if ((panel.AllEquipButton != null) == null)
            {
                goto Label_00BD;
            }
            panel.AllEquipButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
        Label_00BD:
            if ((panel.ArtifactSlot != null) == null)
            {
                goto Label_00E5;
            }
            panel.ArtifactSlot.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
        Label_00E5:
            if ((panel.ArtifactSlot2 != null) == null)
            {
                goto Label_010D;
            }
            panel.ArtifactSlot2.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
        Label_010D:
            if ((panel.ArtifactSlot3 != null) == null)
            {
                goto Label_0135;
            }
            panel.ArtifactSlot3.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
        Label_0135:
            if ((panel.JobRankupAllIn != null) == null)
            {
                goto Label_015D;
            }
            panel.JobRankupAllIn.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
        Label_015D:
            if ((panel.mConceptCardSlot != null) == null)
            {
                goto Label_018F;
            }
            panel.mConceptCardSlot.SelectButton.get_onClick().AddListener(new UnityAction(this, this.OnEquipCardSlotSelect));
        Label_018F:
            return;
        }

        private void InitKyokaPanel(UnitEnhancePanel panel)
        {
            RectTransform transform;
            ListItemEvents events;
            SRPG_Button button;
            PlayerData data;
            int num;
            ItemData data2;
            ItemData data3;
            ListItemEvents events2;
            ExpItemTouchController controller;
            transform = panel.ExpItemList;
            events = panel.ExpItemTemplate;
            button = panel.UnitLevelupButton;
            if ((transform == null) != null)
            {
                goto Label_0039;
            }
            if ((events == null) != null)
            {
                goto Label_0039;
            }
            if ((button == null) == null)
            {
                goto Label_003A;
            }
        Label_0039:
            return;
        Label_003A:
            data = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_0194;
        Label_004D:
            data2 = data.Items[num];
            if (data2 == null)
            {
                goto Label_018E;
            }
            if (data2.Param == null)
            {
                goto Label_018E;
            }
            if (data2.Param.type != 6)
            {
                goto Label_018E;
            }
            if (this.ShowNoStockExpPotions == null)
            {
                goto Label_018E;
            }
            if (data2.Num > 0)
            {
                goto Label_009E;
            }
            goto Label_018E;
        Label_009E:
            data3 = new ItemData();
            data3.Setup(data2.UniqueID, data2.Param, data2.NumNonCap);
            events2 = Object.Instantiate<ListItemEvents>(events);
            if (this.HoldUseItemEnable == null)
            {
                goto Label_012E;
            }
            events2.get_gameObject().AddComponent<ExpItemTouchController>();
            controller = events2.get_gameObject().GetComponent<ExpItemTouchController>();
            controller.OnPointerDownFunc = new ExpItemTouchController.DelegateOnPointerDown(this.OnExpItemButtonDown);
            controller.OnPointerUpFunc = new ExpItemTouchController.DelegateOnPointerDown(this.OnExpItemButtonUp);
            controller.UseItemFunc = new ExpItemTouchController.DelegateUseItem(this.OnExpItemHoldUse);
            goto Label_0141;
        Label_012E:
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnExpItemClick);
        Label_0141:
            this.mExpItemGameObjects.Add(events2.get_gameObject());
            DataSource.Bind<ItemData>(events2.get_gameObject(), data3);
            events2.get_gameObject().SetActive(1);
            events2.get_transform().SetParent(transform.get_transform(), 0);
            this.mTmpItems.Add(data3);
        Label_018E:
            num += 1;
        Label_0194:
            if (num < data.Items.Count)
            {
                goto Label_004D;
            }
            button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnExpMaxOpen));
            return;
        }

        private UnitEnhancePanel InitTabPage(int pageIndex, UnitEnhancePanel prefab, bool visible)
        {
            Canvas canvas;
            if ((prefab == null) == null)
            {
                goto Label_000E;
            }
            return null;
        Label_000E:
            this.mTabPages[pageIndex] = Object.Instantiate<UnitEnhancePanel>(prefab);
            this.mTabPages[pageIndex].get_transform().SetParent(this.TabPageParent, 0);
            canvas = this.mTabPages[pageIndex].GetComponent<Canvas>();
            if ((canvas != null) == null)
            {
                goto Label_0056;
            }
            canvas.set_enabled(visible);
        Label_0056:
            return this.mTabPages[pageIndex];
        }

        private unsafe void InvokeUserClose()
        {
            long num;
            if (this.mStartSelectUnitUniqueID <= 0L)
            {
                goto Label_0051;
            }
            num = -1L;
            if (this.mStartSelectUnitUniqueID == this.mCurrentUnitID)
            {
                goto Label_002E;
            }
            num = this.mCurrentUnitID;
            this.SetUnitDirty();
        Label_002E:
            FlowNode_Variable.Set("LAST_SELECTED_UNITID", (num <= 0L) ? string.Empty : &num.ToString());
        Label_0051:
            this.mStartSelectUnitUniqueID = -1L;
            if (this.OnUserClose == null)
            {
                goto Label_006F;
            }
            this.OnUserClose();
        Label_006F:
            return;
        }

        private bool IsCanPlayLeftVoice()
        {
            if (this.IsStopPlayLeftVoice == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if ((this.mUnitModelViewer != null) == null)
            {
                goto Label_0035;
            }
            if (this.mUnitModelViewer.get_gameObject().get_activeSelf() == null)
            {
                goto Label_0035;
            }
            return 0;
        Label_0035:
            if ((this.mTobiraInventoryWindow != null) == null)
            {
                goto Label_005D;
            }
            if (this.mTobiraInventoryWindow.get_gameObject().get_activeSelf() == null)
            {
                goto Label_005D;
            }
            return 0;
        Label_005D:
            return 1;
        }

        [DebuggerHidden]
        private IEnumerator LoadAllUnitImage()
        {
            <LoadAllUnitImage>c__Iterator153 iterator;
            iterator = new <LoadAllUnitImage>c__Iterator153();
            iterator.<>f__this = this;
            return iterator;
        }

        private void OnAbilityRankUp(AbilityData abilityData, GameObject itemGO)
        {
            int num;
            Animator animator;
            List<string> list;
            List<SkillParam> list2;
            GameManager manager;
            int num2;
            SkillParam param;
            if (this.ExecQueuedKyokaRequest(new DeferredJob(this.SubmitAbilityRankUpRequest)) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if (MonoSingleton<GameManager>.Instance.Player.CheckRankUpAbility(abilityData) != null)
            {
                goto Label_002E;
            }
            return;
        Label_002E:
            this.mChQuestProg = null;
            if (this.mCurrentUnit.IsOpenCharacterQuest() == null)
            {
                goto Label_0056;
            }
            this.mChQuestProg = this.mCurrentUnit.SaveUnlockProgress();
        Label_0056:
            this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
            if (MonoSingleton<GameManager>.Instance.Player.RankUpAbility(abilityData) != null)
            {
                goto Label_007D;
            }
            return;
        Label_007D:
            num = abilityData.Rank;
            MonoSingleton<GameManager>.Instance.NotifyAbilityRankUpCountChanged();
            MonoSingleton<GameManager>.Instance.Player.OnAbilityPowerUp(this.mCurrentUnit.UnitID, abilityData.AbilityID, num, 0);
            if ((itemGO != null) == null)
            {
                goto Label_0121;
            }
            if (string.IsNullOrEmpty(this.AbilityRankUpTrigger) != null)
            {
                goto Label_00EB;
            }
            animator = itemGO.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00EB;
            }
            animator.SetTrigger(this.AbilityRankUpTrigger);
        Label_00EB:
            if ((this.AbilityRankUpEffect != null) == null)
            {
                goto Label_0121;
            }
            UIUtility.SpawnParticle(this.AbilityRankUpEffect, itemGO.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
        Label_0121:
            this.mAbilityPicker.ListBody.UpdateItem(abilityData);
            if ((this.mAbilityListPanel != null) == null)
            {
                goto Label_0154;
            }
            this.mAbilityListPanel.AbilityList.UpdateItem(abilityData);
        Label_0154:
            if ((this.mAbilitySlotPanel != null) == null)
            {
                goto Label_0176;
            }
            this.mAbilitySlotPanel.AbilitySlots.UpdateItem(abilityData);
        Label_0176:
            this.mRankedUpAbilities.Add(abilityData.UniqueID);
            this.QueueKyokaRequest(new DeferredJob(this.SubmitAbilityRankUpRequest), 0f);
            this.ExecQueuedKyokaRequest(null);
            this.PlayUnitVoice("chara_0017");
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), null);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
            list = abilityData.GetLearningSkillList(num);
            if (list == null)
            {
                goto Label_0259;
            }
            if (list.Count <= 0)
            {
                goto Label_0259;
            }
            list2 = new List<SkillParam>(list.Count);
            manager = MonoSingleton<GameManager>.Instance;
            num2 = 0;
            goto Label_0232;
        Label_0208:
            try
            {
                param = manager.GetSkillParam(list[num2]);
                list2.Add(param);
                goto Label_022C;
            }
            catch (Exception)
            {
            Label_0226:
                goto Label_022C;
            }
        Label_022C:
            num2 += 1;
        Label_0232:
            if (num2 < list.Count)
            {
                goto Label_0208;
            }
            this.mKeepWindowLocked = 1;
            base.StartCoroutine(this.PostAbilityRankUp(list2));
            goto Label_0279;
        Label_0259:
            base.GetComponent<WindowController>().SetCollision(0);
            this.mKeepWindowLocked = 1;
            base.StartCoroutine(this.AbilityRankUpSkillUnlockEffect());
        Label_0279:
            return;
        }

        private void OnAbilityRankUpCountPreReset(int unused)
        {
            if (this.ExecQueuedKyokaRequest(null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            return;
        }

        private void OnAbilityRankUpExec(AbilityData abilityData, GameObject go)
        {
            AbilityData data;
            List<string> list;
            List<SkillParam> list2;
            GameManager manager;
            int num;
            SkillParam param;
            if (abilityData != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.mRankedUpAbilities == null)
            {
                goto Label_0023;
            }
            if (this.mRankedUpAbilities.Count > 0)
            {
                goto Label_0024;
            }
        Label_0023:
            return;
        Label_0024:
            this.QueueKyokaRequest(new DeferredJob(this.SubmitAbilityRankUpRequest), 0f);
            this.ExecQueuedKyokaRequest(null);
            this.IsFirstPlay = 0;
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), null);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
            this.mAbilitySlotDirty = 1;
            list = this.mCurrentUnit.GetAbilityData(abilityData.UniqueID).GetLearningSkillList2(abilityData.Rank);
            if (list == null)
            {
                goto Label_010E;
            }
            if (list.Count <= 0)
            {
                goto Label_010E;
            }
            list2 = new List<SkillParam>(list.Count);
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            goto Label_00E7;
        Label_00BE:
            try
            {
                param = manager.GetSkillParam(list[num]);
                list2.Add(param);
                goto Label_00E1;
            }
            catch (Exception)
            {
            Label_00DB:
                goto Label_00E1;
            }
        Label_00E1:
            num += 1;
        Label_00E7:
            if (num < list.Count)
            {
                goto Label_00BE;
            }
            this.mKeepWindowLocked = 1;
            base.StartCoroutine(this.PostAbilityRankUp(list2));
            goto Label_012E;
        Label_010E:
            base.GetComponent<WindowController>().SetCollision(0);
            this.mKeepWindowLocked = 1;
            base.StartCoroutine(this.AbilityRankUpSkillUnlockEffect());
        Label_012E:
            return;
        }

        private void OnAbilityRankUpSet(AbilityData abilityData, GameObject itemGO)
        {
            GameManager manager;
            int num;
            Animator animator;
            if (this.ExecQueuedKyokaRequest(new DeferredJob(this.SubmitAbilityRankUpRequest)) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.Player.CheckRankUpAbility(abilityData) != null)
            {
                goto Label_0030;
            }
            return;
        Label_0030:
            this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
            if (manager.Player.RankUpAbility(abilityData) != null)
            {
                goto Label_0053;
            }
            return;
        Label_0053:
            num = abilityData.Rank;
            MonoSingleton<GameManager>.Instance.NotifyAbilityRankUpCountChanged();
            MonoSingleton<GameManager>.Instance.Player.OnAbilityPowerUp(this.mCurrentUnit.UnitID, abilityData.AbilityID, num, 0);
            if ((itemGO != null) == null)
            {
                goto Label_00F7;
            }
            if (string.IsNullOrEmpty(this.AbilityRankUpTrigger) != null)
            {
                goto Label_00C1;
            }
            animator = itemGO.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00C1;
            }
            animator.SetTrigger(this.AbilityRankUpTrigger);
        Label_00C1:
            if ((this.AbilityRankUpEffect != null) == null)
            {
                goto Label_00F7;
            }
            UIUtility.SpawnParticle(this.AbilityRankUpEffect, itemGO.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
        Label_00F7:
            this.mAbilityPicker.ListBody.UpdateItem(abilityData);
            if ((this.mAbilityListPanel != null) == null)
            {
                goto Label_012A;
            }
            this.mAbilityListPanel.AbilityList.UpdateItem(abilityData);
        Label_012A:
            if ((this.mAbilitySlotPanel != null) == null)
            {
                goto Label_014C;
            }
            this.mAbilitySlotPanel.AbilitySlots.UpdateItem(abilityData);
        Label_014C:
            this.mRankedUpAbilities.Add(abilityData.UniqueID);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0013", 0f);
            if (this.IsFirstPlay != null)
            {
                goto Label_018E;
            }
            this.IsFirstPlay = 1;
            this.PlayUnitVoice("chara_0017");
        Label_018E:
            return;
        }

        private void OnAbilitySlotSelect(int slotIndex)
        {
            if ((this.mAbilityPicker == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.ExecQueuedKyokaRequest(new DeferredJob(this.Req_UpdateEquippedAbility));
            this.mAbilityPicker.UnitData = this.mCurrentUnit;
            this.mAbilityPicker.AbilitySlot = slotIndex;
            this.mAbilityPicker.Refresh();
            this.mAbilityPicker.GetComponent<WindowController>().Open();
            return;
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus != null)
            {
                goto Label_000D;
            }
            this.OnApplicationPause(1);
        Label_000D:
            return;
        }

        private void OnApplicationPause(bool pausing)
        {
            if (pausing == null)
            {
                goto Label_001C;
            }
            this.mAppPausing = 1;
            this.ExecQueuedKyokaRequest(null);
            this.mAppPausing = 0;
        Label_001C:
            return;
        }

        private void OnArtifactClick(GenericSlot slot, bool interactable)
        {
            ArtifactData[] dataArray1;
            bool flag;
            GameObject obj2;
            ArtifactWindow window;
            ArtifactData data;
            long num;
            long num2;
            int num3;
            ArtifactData data2;
            ArtifactTypes types;
            if (interactable != null)
            {
                goto Label_000E;
            }
            this.ShowLockEquipArtifactTooltip(slot);
            return;
        Label_000E:
            if (string.IsNullOrEmpty(this.PREFAB_PATH_ARTIFACT_WINDOW) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            this.ExecQueuedKyokaRequest(null);
            flag = 0;
            if ((this.mArtifactSelector == null) == null)
            {
                goto Label_0057;
            }
            obj2 = AssetManager.Load<GameObject>(this.PREFAB_PATH_ARTIFACT_WINDOW);
            this.mArtifactSelector = Object.Instantiate<GameObject>(obj2);
            goto Label_0065;
        Label_0057:
            this.mArtifactSelector.SetActive(1);
            flag = 1;
        Label_0065:
            window = this.mArtifactSelector.GetComponent<ArtifactWindow>();
            if ((window == null) == null)
            {
                goto Label_007E;
            }
            return;
        Label_007E:
            if ((slot != null) == null)
            {
                goto Label_00EB;
            }
            if ((slot == this.mEquipmentPanel.ArtifactSlot) == null)
            {
                goto Label_00AC;
            }
            window.SelectArtifactSlot = 1;
            goto Label_00EB;
        Label_00AC:
            if ((slot == this.mEquipmentPanel.ArtifactSlot2) == null)
            {
                goto Label_00CE;
            }
            window.SelectArtifactSlot = 2;
            goto Label_00EB;
        Label_00CE:
            if ((slot == this.mEquipmentPanel.ArtifactSlot3) == null)
            {
                goto Label_00EB;
            }
            window.SelectArtifactSlot = 3;
        Label_00EB:
            window.OnEquip = new ArtifactWindow.EquipEvent(this.OnArtifactSelect);
            window.SetOwnerUnit(this.mCurrentUnit, this.mSelectedJobIndex);
            if ((window.ArtifactList != null) == null)
            {
                goto Label_0293;
            }
            if (window.ArtifactList.TestOwner == this.mCurrentUnit)
            {
                goto Label_0149;
            }
            window.ArtifactList.TestOwner = this.mCurrentUnit;
            flag = 1;
        Label_0149:
            data = DataSource.FindDataOfClass<ArtifactData>(slot.get_gameObject(), null);
            num = 0L;
            if (this.mCurrentUnit.CurrentJob.Artifacts == null)
            {
                goto Label_0201;
            }
            num2 = (data == null) ? 0L : data.UniqueID;
            num3 = 0;
            goto Label_01E8;
        Label_0196:
            if ((this.mCurrentUnit.CurrentJob.Artifacts[num3] == null) || (this.mCurrentUnit.CurrentJob.Artifacts[num3] != num2))
            {
                goto Label_01E2;
            }
            num = this.mCurrentUnit.CurrentJob.Artifacts[num3];
            goto Label_0201;
        Label_01E2:
            num3 += 1;
        Label_01E8:
            if (num3 < ((int) this.mCurrentUnit.CurrentJob.Artifacts.Length))
            {
                goto Label_0196;
            }
        Label_0201:
            data2 = null;
            if (num == null)
            {
                goto Label_021E;
            }
            data2 = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(num);
        Label_021E:
            if (data2 == null)
            {
                goto Label_0242;
            }
            dataArray1 = new ArtifactData[] { data2 };
            window.ArtifactList.SetSelection(dataArray1, 1, 1);
            goto Label_0255;
        Label_0242:
            window.ArtifactList.SetSelection(new ArtifactData[0], 1, 1);
        Label_0255:
            types = (data == null) ? 0 : data.ArtifactParam.type;
            window.ArtifactList.FiltersPriority = this.SetEquipArtifactFilters(data, types);
            if (flag == null)
            {
                goto Label_0293;
            }
            window.ArtifactList.Refresh();
        Label_0293:
            return;
        }

        private unsafe void OnArtifactSelect(ArtifactData artifact, ArtifactTypes type)
        {
            PlayerData data;
            UnitData data2;
            JobData data3;
            int num;
            int num2;
            List<ArtifactData> list;
            List<ArtifactData> list2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            <OnArtifactSelect>c__AnonStorey3BD storeybd;
            <OnArtifactSelect>c__AnonStorey3BE storeybe;
            storeybd = new <OnArtifactSelect>c__AnonStorey3BD();
            this.BeginStatusChangeCheck();
            if (artifact == null)
            {
                goto Label_0071;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = null;
            data3 = null;
            if (data.FindOwner(artifact, &data2, &data3) == null)
            {
                goto Label_0071;
            }
            num = 0;
            goto Label_0063;
        Label_0039:
            if (data3.Artifacts[num] != artifact.UniqueID)
            {
                goto Label_005F;
            }
            data3.SetEquipArtifact(num, null);
            goto Label_0071;
        Label_005F:
            num += 1;
        Label_0063:
            if (num < ((int) data3.Artifacts.Length))
            {
                goto Label_0039;
            }
        Label_0071:
            storeybd.view_artifact_datas = this.GetViewArtifact();
            num2 = JobData.GetArtifactSlotIndex(type);
            list = new List<ArtifactData>(this.CurrentUnit.CurrentJob.ArtifactDatas);
            if (list.Count == ((int) storeybd.view_artifact_datas.Length))
            {
                goto Label_00BD;
            }
            DebugUtility.LogError("画面上の武具データと実際の武具データがあっていません。");
            return;
        Label_00BD:
            storeybe = new <OnArtifactSelect>c__AnonStorey3BE();
            storeybe.<>f__ref$957 = storeybd;
            storeybe.i = 0;
            goto Label_0127;
        Label_00DA:
            if (storeybd.view_artifact_datas[storeybe.i] != null)
            {
                goto Label_00F3;
            }
            goto Label_0117;
        Label_00F3:
            if (list.Find(new Predicate<ArtifactData>(storeybe.<>m__44B)) != null)
            {
                goto Label_0117;
            }
            DebugUtility.LogError("画面上の武具データと実際の武具データがあっていません。");
            return;
        Label_0117:
            storeybe.i += 1;
        Label_0127:
            if (storeybe.i < ((int) storeybd.view_artifact_datas.Length))
            {
                goto Label_00DA;
            }
            storeybd.view_artifact_datas[num2] = artifact;
            list2 = new List<ArtifactData>();
            num3 = 0;
            goto Label_0170;
        Label_0156:
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(num3, null);
            num3 += 1;
        Label_0170:
            if (num3 < ((int) this.mCurrentUnit.CurrentJob.ArtifactDatas.Length))
            {
                goto Label_0156;
            }
            num4 = 0;
            goto Label_0213;
        Label_0191:
            if (storeybd.view_artifact_datas[num4] != null)
            {
                goto Label_01A5;
            }
            goto Label_020D;
        Label_01A5:
            if (storeybd.view_artifact_datas[num4].ArtifactParam.type != 3)
            {
                goto Label_01D5;
            }
            list2.Add(storeybd.view_artifact_datas[num4]);
            goto Label_020D;
        Label_01D5:
            num5 = JobData.GetArtifactSlotIndex(storeybd.view_artifact_datas[num4].ArtifactParam.type);
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(num5, storeybd.view_artifact_datas[num4]);
        Label_020D:
            num4 += 1;
        Label_0213:
            if (num4 < ((int) storeybd.view_artifact_datas.Length))
            {
                goto Label_0191;
            }
            num6 = 0;
            goto Label_0296;
        Label_022B:
            num7 = 0;
            goto Label_0277;
        Label_0233:
            if (this.mCurrentUnit.CurrentJob.ArtifactDatas[num7] == null)
            {
                goto Label_0250;
            }
            goto Label_0271;
        Label_0250:
            this.mCurrentUnit.CurrentJob.SetEquipArtifact(num7, list2[num6]);
            goto Label_0290;
        Label_0271:
            num7 += 1;
        Label_0277:
            if (num7 < ((int) this.mCurrentUnit.CurrentJob.ArtifactDatas.Length))
            {
                goto Label_0233;
            }
        Label_0290:
            num6 += 1;
        Label_0296:
            if (num6 < list2.Count)
            {
                goto Label_022B;
            }
            this.mCurrentUnit.UpdateArtifact(this.mCurrentUnit.JobIndex, 1, 0);
            if (Network.Mode != null)
            {
                goto Label_030D;
            }
            Network.RequestAPI(new ReqArtifactSet(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.Artifacts, new Network.ResponseCallback(this.OnArtifactSetResult)), 0);
            goto Label_0313;
        Label_030D:
            this.ShowArtifactSetResult();
        Label_0313:
            this.SetUnitDirty();
            return;
        }

        private unsafe void OnArtifactSetResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0016;
            }
            code = Network.ErrCode;
            FlowNode_Network.Retry();
            return;
        Label_0016:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0045;
            }
            FlowNode_Network.Retry();
            return;
        Label_0045:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.artifacts, 0);
                MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
                goto Label_00AF;
            }
            catch (Exception exception1)
            {
            Label_0099:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_00C6;
            }
        Label_00AF:
            Network.RemoveAPI();
            this.RefreshSortedUnits();
            this.RebuildUnitData();
            this.ShowArtifactSetResult();
        Label_00C6:
            return;
        }

        private void OnCharacterQuestOpen(SRPG_Button button)
        {
            this.OnCharacterQuestOpen(button, 0);
            return;
        }

        private void OnCharacterQuestOpen(SRPG_Button button, bool isRestore)
        {
            GameObject obj2;
            GameObject obj3;
            UnitCharacterQuestWindow window;
            if (string.IsNullOrEmpty(this.PREFAB_PATH_CHARA_QUEST_LIST_WINDOW) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            obj2 = AssetManager.Load<GameObject>(this.PREFAB_PATH_CHARA_QUEST_LIST_WINDOW);
            if ((obj2 == null) == null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            window = Object.Instantiate<GameObject>(obj2).GetComponent<UnitCharacterQuestWindow>();
            if ((window == null) == null)
            {
                goto Label_0045;
            }
            return;
        Label_0045:
            window.IsRestore = isRestore;
            GlobalVars.SelectedUnitUniqueID.Set(this.mCurrentUnit.UniqueID);
            GlobalVars.PreBattleUnitUniqueID.Set(this.mCurrentUnit.UniqueID);
            this.mCharacterQuestWindow = window;
            this.mCharacterQuestWindow.CurrentUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
            base.GetComponent<WindowController>().SetCollision(0);
            this.mCharacterQuestWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnCharacterQuestSelectCancel);
            WindowController.OpenIfAvailable(this.mCharacterQuestWindow);
            return;
        }

        public void OnCharacterQuestRestore()
        {
            this.OnCharacterQuestOpen(this.CharaQuestButton, 1);
            return;
        }

        private void OnCharacterQuestSelectCancel(GameObject go, bool visible)
        {
            WindowController controller;
            if (visible == null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            base.GetComponent<WindowController>().SetCollision(1);
            if ((this.mCharacterQuestWindow == null) != null)
            {
                goto Label_002A;
            }
            if (visible == null)
            {
                goto Label_002B;
            }
        Label_002A:
            return;
        Label_002B:
            this.mCharacterQuestWindow.GetComponent<WindowController>().OnWindowStateChange = null;
            Object.Destroy(this.mCharacterQuestWindow.get_gameObject());
            this.mCharacterQuestWindow = null;
            return;
        }

        private void OnCloseEquipConceptCardWindow()
        {
            object[] objArray1;
            ArtifactParam[] paramArray;
            string str;
            this.SpawnStatusChangeEffects();
            if (this.mCurrentUnit.ConceptCard != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (this.mCurrentUnit.ConceptCard.UniqueID != this.mCurrentEquipConceptCardId)
            {
                goto Label_0038;
            }
            return;
        Label_0038:
            if (((int) this.mCurrentUnit.GetEnableConceptCardSkins(-1).Length) <= 0)
            {
                goto Label_009B;
            }
            objArray1 = new object[] { this.mCurrentUnit.ConceptCard.Param.name };
            UIUtility.ConfirmBox(LocalizedText.Get("sys.CONCEPT_CARD_CONFIRM_CHANGE_SKIN", objArray1), new UIUtility.DialogResultEvent(this.AcceptChangeSkinByEquipConceptCard), new UIUtility.DialogResultEvent(this.CancelChangeSkinByEquipConceptCard), null, 0, -1, null, null);
        Label_009B:
            return;
        }

        private void OnCommonEquipResult(WWWResult www)
        {
            this.OnEquipResult(www, 1);
            return;
        }

        private void OnDestroy()
        {
            GameManager manager;
            this.DestroyOverlay();
            if ((this.mArtifactSelector != null) == null)
            {
                goto Label_002E;
            }
            Object.Destroy(this.mArtifactSelector.get_gameObject());
            this.mArtifactSelector = null;
        Label_002E:
            if ((this.mParamTooltip != null) == null)
            {
                goto Label_0056;
            }
            Object.Destroy(this.mParamTooltip.get_gameObject());
            this.mParamTooltip = null;
        Label_0056:
            if ((this.mUnitProfileWindow != null) == null)
            {
                goto Label_007E;
            }
            Object.Destroy(this.mUnitProfileWindow.get_gameObject());
            this.mUnitProfileWindow = null;
        Label_007E:
            if ((this.mUnitUnlockWindow != null) == null)
            {
                goto Label_00A6;
            }
            Object.Destroy(this.mUnitUnlockWindow.get_gameObject());
            this.mUnitUnlockWindow = null;
        Label_00A6:
            if ((this.mJobUnlockTooltip != null) == null)
            {
                goto Label_00CE;
            }
            Object.Destroy(this.mJobUnlockTooltip.get_gameObject());
            this.mJobUnlockTooltip = null;
        Label_00CE:
            if ((this.mLeaderSkillDetail != null) == null)
            {
                goto Label_00EF;
            }
            Object.Destroy(this.mLeaderSkillDetail.get_gameObject());
        Label_00EF:
            if ((this.mEvolutionWindow != null) == null)
            {
                goto Label_0110;
            }
            Object.Destroy(this.mEvolutionWindow.get_gameObject());
        Label_0110:
            if ((this.mUnlockTobiraWindow != null) == null)
            {
                goto Label_0131;
            }
            Object.Destroy(this.mUnlockTobiraWindow.get_gameObject());
        Label_0131:
            if ((this.mSkinSelectWindow != null) == null)
            {
                goto Label_0152;
            }
            Object.Destroy(this.mSkinSelectWindow.get_gameObject());
        Label_0152:
            if ((this.mAbilityPicker != null) == null)
            {
                goto Label_0173;
            }
            Object.Destroy(this.mAbilityPicker.get_gameObject());
        Label_0173:
            if ((this.mCharacterQuestWindow != null) == null)
            {
                goto Label_019B;
            }
            Object.Destroy(this.mCharacterQuestWindow.get_gameObject());
            this.mCharacterQuestWindow = null;
        Label_019B:
            if ((this.mIkkatsuEquipWindow != null) == null)
            {
                goto Label_01C3;
            }
            Object.Destroy(this.mIkkatsuEquipWindow.get_gameObject());
            this.mIkkatsuEquipWindow = null;
        Label_01C3:
            if ((this.mEquipArtifactUnlockTooltip != null) == null)
            {
                goto Label_01EB;
            }
            Object.Destroy(this.mEquipArtifactUnlockTooltip.get_gameObject());
            this.mEquipArtifactUnlockTooltip = null;
        Label_01EB:
            if ((this.mTobiraLevelupMaxEffectObject != null) == null)
            {
                goto Label_020E;
            }
            Object.Destroy(this.mTobiraLevelupMaxEffectObject);
            this.mTobiraLevelupMaxEffectObject = null;
        Label_020E:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager != null) == null)
            {
                goto Label_0264;
            }
            manager.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Remove(manager.OnSceneChange, new GameManager.SceneChangeEvent(this.OnSceneCHange));
            manager.OnAbilityRankUpCountPreReset = (GameManager.RankUpCountChangeEvent) Delegate.Remove(manager.OnAbilityRankUpCountPreReset, new GameManager.RankUpCountChangeEvent(this.OnAbilityRankUpCountPreReset));
        Label_0264:
            GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
            if (this.mUnitVoice == null)
            {
                goto Label_029C;
            }
            this.mUnitVoice.StopAll(1f);
            this.mUnitVoice.Cleanup();
            this.mUnitVoice = null;
        Label_029C:
            return;
        }

        private void OnDisable()
        {
            if ((Instance == this) == null)
            {
                goto Label_0016;
            }
            Instance = null;
        Label_0016:
            return;
        }

        private void OnEnable()
        {
            if ((Instance == null) == null)
            {
                goto Label_0016;
            }
            Instance = this;
        Label_0016:
            return;
        }

        public void OnEnhanceTobiraRestore()
        {
            this.TobiraUIActive(1, 1);
            return;
        }

        private void OnEquip(bool is_common)
        {
            JobSetParam param;
            param = this.mCurrentUnit.GetJobSetParam(this.mSelectedJobIndex);
            if (param != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            this.EquipmentWindow.GetComponent<WindowController>().OnWindowStateChange = null;
            WindowController.CloseIfAvailable(this.EquipmentWindow);
            if (Network.Mode != null)
            {
                goto Label_00AF;
            }
            if (is_common == null)
            {
                goto Label_007A;
            }
            Network.RequestAPI(new ReqJobEquipV2(this.mCurrentUnit.UniqueID, param.iname, (long) this.mSelectedEquipmentSlot, is_common, new Network.ResponseCallback(this.OnCommonEquipResult)), 0);
            goto Label_00AA;
        Label_007A:
            Network.RequestAPI(new ReqJobEquipV2(this.mCurrentUnit.UniqueID, param.iname, (long) this.mSelectedEquipmentSlot, is_common, new Network.ResponseCallback(this.OnEquipResult)), 0);
        Label_00AA:
            goto Label_00BC;
        Label_00AF:
            base.StartCoroutine(this.PostEquip());
        Label_00BC:
            this.SetUnitDirty();
            return;
        }

        private void OnEquipAll(bool AllIn)
        {
            GameObject obj2;
            UnitJobRankUpConfirm confirm;
            int num;
            obj2 = Object.Instantiate<GameObject>(this.Prefab_IkkatsuEquip);
            if ((obj2 == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            DataSource.Bind<UnitData>(obj2, this.mCurrentUnit);
            confirm = obj2.GetComponent<UnitJobRankUpConfirm>();
            if ((confirm == null) == null)
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            confirm.IsAllIn = AllIn;
            this.IsBulk = AllIn;
            if (this.mCurrentUnit == null)
            {
                goto Label_0062;
            }
            if (this.mCurrentUnit.CurrentJob != null)
            {
                goto Label_0063;
            }
        Label_0062:
            return;
        Label_0063:
            if (this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) <= this.mCurrentUnit.CurrentJob.Rank)
            {
                goto Label_00A7;
            }
            confirm.OnAllEquipAccept = new UnitJobRankUpConfirm.OnAccept(this.OnJobRankUpEquipAllAccept);
            goto Label_00B9;
        Label_00A7:
            confirm.OnAllEquipAccept = new UnitJobRankUpConfirm.OnAccept(this.OnEquipAllAccept);
        Label_00B9:
            confirm.SetCommonFlag = new UnitJobRankUpConfirm.SetFlag(this.SetIsCommon);
            return;
        }

        private void OnEquipAllAccept(int target_rank, bool can_jobmaster, bool can_jobmax)
        {
            JobSetParam param;
            int num;
            if (Network.Mode != null)
            {
                goto Label_010B;
            }
            base.GetComponent<WindowController>().SetCollision(0);
            param = this.mCurrentUnit.GetJobSetParam(this.mSelectedJobIndex);
            num = 0;
            if (can_jobmaster == null)
            {
                goto Label_0037;
            }
            num = 1;
            goto Label_009A;
        Label_0037:
            if (((target_rank <= 0) || (this.mCurrentUnit.GetJobRankCap() == JobParam.MAX_JOB_RANK)) || (this.mCurrentUnit.GetJobRankCap() != target_rank))
            {
                goto Label_009A;
            }
            if (this.IsBulk == null)
            {
                goto Label_0082;
            }
            num = (can_jobmax == null) ? 0 : 1;
            goto Label_009A;
        Label_0082:
            if (this.mCurrentUnit.CurrentJob.Rank != target_rank)
            {
                goto Label_009A;
            }
            num = 1;
        Label_009A:
            this.SetCacheUnitData(this.mCurrentUnit, this.mSelectedJobIndex);
            if (param == null)
            {
                goto Label_00FE;
            }
            Network.RequestAPI(new ReqJobRankupAll(this.mCurrentUnit.UniqueID, param.iname, this.mIsCommon, this.mCurrentUnit.CurrentJob.Rank, target_rank, num, new Network.ResponseCallback(this.OnEquipAllResult)), 0);
            this.SetIsCommon(0);
            goto Label_010B;
        Label_00FE:
            base.StartCoroutine(this.PostEquip());
        Label_010B:
            this.SetUnitDirty();
            return;
        }

        private unsafe void OnEquipAllResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            UnitData data;
            EquipData[] dataArray;
            int num;
            int num2;
            GameManager manager;
            Exception exception;
            UnitData data2;
            int num3;
            EquipData[] dataArray2;
            int num4;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0047;
            }
            code = Network.ErrCode;
            if (code == 0x9c4)
            {
                goto Label_002E;
            }
            if (code == 0x9c5)
            {
                goto Label_0034;
            }
            goto Label_0041;
        Label_002E:
            FlowNode_Network.Failed();
            return;
        Label_0034:
            FlowNode_Network.Failed();
            this.mJobRankUpRequestSent = 1;
            return;
        Label_0041:
            FlowNode_Network.Retry();
            return;
        Label_0047:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
            dataArray = data.GetRankupEquips(this.mCurrentUnit.JobIndex);
            num = 0;
            num2 = 0;
            goto Label_00B4;
        Label_009C:
            if (dataArray[num2].IsEquiped() == null)
            {
                goto Label_00AE;
            }
            num += 1;
        Label_00AE:
            num2 += 1;
        Label_00B4:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_009C;
            }
            if (data.CurrentJob.Rank <= 1)
            {
                goto Label_00E1;
            }
            num += (data.CurrentJob.Rank - 1) * 6;
        Label_00E1:
            try
            {
                manager = MonoSingleton<GameManager>.Instance;
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.units);
                manager.Deserialize(response.body.items);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                goto Label_0146;
            }
            catch (Exception exception1)
            {
            Label_012E:
                exception = exception1;
                Debug.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_0249;
            }
        Label_0146:
            Network.RemoveAPI();
            this.SetNotifyReleaseClassChangeJob();
            this.UpdateTrophy_OnJobLevelChange();
            this.RefreshSortedUnits();
            data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
            num3 = 0;
            dataArray2 = data2.GetRankupEquips(this.mCurrentUnit.JobIndex);
            num4 = 0;
            goto Label_01B3;
        Label_0198:
            if (dataArray2[num4].IsEquiped() == null)
            {
                goto Label_01AD;
            }
            num3 += 1;
        Label_01AD:
            num4 += 1;
        Label_01B3:
            if (num4 < ((int) dataArray2.Length))
            {
                goto Label_0198;
            }
            if (data2.CurrentJob.Rank <= 1)
            {
                goto Label_01E5;
            }
            num3 += (data2.CurrentJob.Rank - 1) * 6;
        Label_01E5:
            MonoSingleton<GameManager>.Instance.Player.OnSoubiSet(this.mCurrentUnit.UnitID, num3 - num);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), null);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
            if (this.mIsJobLvMaxAllEquip == null)
            {
                goto Label_0242;
            }
            base.StartCoroutine(this.PostEquip());
            goto Label_0249;
        Label_0242:
            this.mJobRankUpRequestSent = 1;
        Label_0249:
            return;
        }

        private void OnEquipCardSlotSelect()
        {
            DestroyEventListener local1;
            GameObject obj2;
            GameObject obj3;
            ConceptCardEquipWindow window;
            DestroyEventListener listener;
            obj2 = AssetManager.Load<GameObject>(CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH);
            if ((obj2 == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            obj3 = Object.Instantiate<GameObject>(obj2);
            window = obj3.GetComponent<ConceptCardEquipWindow>();
            if ((window != null) == null)
            {
                goto Label_006D;
            }
            window.Init(this.mCurrentUnit);
            local1 = obj3.AddComponent<DestroyEventListener>();
            local1.Listeners = (DestroyEventListener.DestroyEvent) Delegate.Combine(local1.Listeners, new DestroyEventListener.DestroyEvent(this.<OnEquipCardSlotSelect>m__44A));
            this.BeginStatusChangeCheck();
        Label_006D:
            return;
        }

        private void OnEquipCommon()
        {
            this.OnEquip(1);
            return;
        }

        public void OnEquipConceptCardSelect()
        {
            UnitData data;
            if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID) == null)
            {
                goto Label_0027;
            }
            if (this.mCurrentUnit != null)
            {
                goto Label_0028;
            }
        Label_0027:
            return;
        Label_0028:
            this.RebuildUnitData();
            this.SetUnitDirty();
            this.RefreshBasicParameters(0);
            this.RefreshSortedUnits();
            this.RefreshUnitShiftButton();
            this.SkinButton.get_gameObject().SetActive(this.mCurrentUnit.IsSkinUnlocked());
            this.ShowSkinSetResult();
            this.FadeUnitImage(0f, 0f, 0f);
            base.StartCoroutine(this.RefreshUnitImage());
            this.FadeUnitImage(0f, 1f, 1f);
            return;
        }

        private void OnEquipmentSlotSelect(GameObject go)
        {
            UnitEquipmentSlotEvents events;
            int num;
            events = go.GetComponent<UnitEquipmentSlotEvents>();
            num = Array.IndexOf<UnitEquipmentSlotEvents>(this.mEquipmentPanel.EquipmentSlots, events);
            GlobalVars.SelectedEquipmentSlot.Set(num);
            this.OpenEquipmentSlot(num);
            return;
        }

        private void OnEquipmentWindowCancel(GameObject go, bool visible)
        {
            if (visible != null)
            {
                goto Label_002A;
            }
            this.EquipmentWindow.GetComponent<WindowController>().OnWindowStateChange = null;
            this.RefreshEquipments(-1);
            base.GetComponent<WindowController>().SetCollision(1);
        Label_002A:
            return;
        }

        private void OnEquipNoCommon()
        {
            this.OnEquip(0);
            return;
        }

        private void OnEquipResult(WWWResult www)
        {
            this.OnEquipResult(www, 0);
            return;
        }

        private unsafe void OnEquipResult(WWWResult www, bool is_common)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            GameManager manager;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0050;
            }
            switch ((Network.ErrCode - 0x9c4))
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_0033;

                case 2:
                    goto Label_0033;
            }
            goto Label_004A;
        Label_002D:
            FlowNode_Network.Failed();
            return;
        Label_0033:
            Network.RemoveAPI();
            Network.ResetError();
            base.GetComponent<WindowController>().SetCollision(1);
            return;
        Label_004A:
            FlowNode_Network.Retry();
            return;
        Label_0050:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
        Label_006E:
            try
            {
                manager = MonoSingleton<GameManager>.Instance;
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.units);
                manager.Deserialize(response.body.items);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                goto Label_00CD;
            }
            catch (Exception exception1)
            {
            Label_00B7:
                exception = exception1;
                Debug.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_00F6;
            }
        Label_00CD:
            Network.RemoveAPI();
            this.RefreshSortedUnits();
            if (is_common == null)
            {
                goto Label_00E9;
            }
            this.EquipmentWindow.CommonEquiped();
        Label_00E9:
            base.StartCoroutine(this.PostEquip());
        Label_00F6:
            return;
        }

        private void OnEvolutionButtonClick(SRPG_Button button)
        {
            int num;
            int num2;
            num = this.mCurrentUnit.Rarity;
            if (this.mCurrentUnit.GetRarityCap() > num)
            {
                goto Label_0039;
            }
            UIUtility.NegativeSystemMessage(string.Empty, LocalizedText.Get("sys.DISABLE_EVOLUTION"), null, null, 0, -1);
            return;
        Label_0039:
            this.ExecQueuedKyokaRequest(null);
            base.StartCoroutine(this.EvolutionButtonClickSync());
            return;
        }

        private void OnEvolutionCancel(GameObject go, bool visible)
        {
            if (visible != null)
            {
                goto Label_001E;
            }
            go.GetComponent<WindowController>().OnWindowStateChange = null;
            base.GetComponent<WindowController>().SetCollision(1);
        Label_001E:
            return;
        }

        public void OnEvolutionRestore()
        {
            this.OnEvolutionButtonClick(this.UnitEvolutionButton);
            return;
        }

        private void OnEvolutionStart(GameObject go, bool visible)
        {
            if (visible != null)
            {
                goto Label_001F;
            }
            go.GetComponent<WindowController>().OnWindowStateChange = null;
            base.StartCoroutine(this.PostUnitEvolution());
        Label_001F:
            return;
        }

        private void OnEvolutionWindowClose()
        {
            base.GetComponent<WindowController>().SetCollision(1);
            this.mKeepWindowLocked = 0;
            return;
        }

        private void OnExpItemButtonDown(ExpItemTouchController controller)
        {
            this.mCurrentKyoukaItemHold = controller;
            return;
        }

        private void OnExpItemButtonUp(ExpItemTouchController controller)
        {
            if ((this.mCurrentKyoukaItemHold != null) == null)
            {
                goto Label_0035;
            }
            if ((this.mKyoukaItemScroll != null) == null)
            {
                goto Label_0035;
            }
            this.mKyoukaItemScroll.set_enabled(1);
            this.mKyoukaItemScroll = null;
        Label_0035:
            return;
        }

        private void OnExpItemClick(GameObject go)
        {
            ItemData data;
            Button button;
            GameManager manager;
            int num;
            int num2;
            int num3;
            CustomSound2 sound;
            int num4;
            int num5;
            Button button2;
            int num6;
            Dictionary<string, int> dictionary;
            string str;
            int num7;
            if (this.mSceneChanging == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.ExecQueuedKyokaRequest(new DeferredJob(this.SubmitUnitKyoka)) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            data = DataSource.FindDataOfClass<ItemData>(go, null);
            if (data == null)
            {
                goto Label_003E;
            }
            if (data.Num > 0)
            {
                goto Label_0078;
            }
        Label_003E:
            button = go.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0077;
            }
            if (button.get_interactable() == null)
            {
                goto Label_0077;
            }
            button.set_interactable(0);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0015", 0f);
        Label_0077:
            return;
        Label_0078:
            if (this.mCurrentUnit.CheckGainExp() != null)
            {
                goto Label_00CC;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.LEVEL_CAPPED"), null, null, 0, -1);
            if (this.HoldUseItemEnable == null)
            {
                goto Label_00CB;
            }
            if ((this.mCurrentKyoukaItemHold != null) == null)
            {
                goto Label_00CB;
            }
            this.mCurrentKyoukaItemHold.StatusReset();
            this.mCurrentKyoukaItemHold = null;
        Label_00CB:
            return;
        Label_00CC:
            if ((this.ClickItem == null) == null)
            {
                goto Label_0199;
            }
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            num = manager.Player.Lv;
            num2 = this.mCurrentUnit.GetLevelCap(0);
            num3 = 0;
            if (num >= num2)
            {
                goto Label_012B;
            }
            num3 = (manager.MasterParam.GetUnitLevelExp(num + 1) - 1) - this.mCurrentUnit.Exp;
            goto Label_0146;
        Label_012B:
            num3 = manager.MasterParam.GetUnitLevelExp(num2) - this.mCurrentUnit.Exp;
        Label_0146:
            if (num3 >= data.Param.value)
            {
                goto Label_0199;
            }
            this.ClickItem = go;
            this.mCurrentKyoukaItemHold.StatusReset();
            UIUtility.ConfirmBox(LocalizedText.Get(this.ExpOverflowWarning), new UIUtility.DialogResultEvent(this.OnExpOverflowOk), new UIUtility.DialogResultEvent(this.OnExpOverflowNo), null, 1, -1, null, null);
            return;
        Label_0199:
            sound = go.GetComponent<CustomSound2>();
            if ((sound != null) == null)
            {
                goto Label_01B5;
            }
            sound.Play();
        Label_01B5:
            if ((this.ClickItem != null) == null)
            {
                goto Label_01CD;
            }
            this.ClickItem = null;
        Label_01CD:
            this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
            if (this.mUsedExpItems.ContainsKey(data.Param.iname) == null)
            {
                goto Label_0229;
            }
            num7 = dictionary[str];
            (dictionary = this.mUsedExpItems)[str = data.Param.iname] = num7 + 1;
            goto Label_0240;
        Label_0229:
            this.mUsedExpItems.Add(data.Param.iname, 1);
        Label_0240:
            num4 = this.mCurrentUnit.Lv;
            num5 = this.mCurrentUnit.Exp;
            this.BeginStatusChangeCheck();
            if (MonoSingleton<GameManager>.Instance.Player.UseExpPotion(this.mCurrentUnit, data) != null)
            {
                goto Label_027C;
            }
            return;
        Label_027C:
            if (data.Num > 0)
            {
                goto Label_02C5;
            }
            button2 = go.GetComponent<Button>();
            if ((button2 != null) == null)
            {
                goto Label_02C5;
            }
            if (button2.get_interactable() == null)
            {
                goto Label_02C5;
            }
            button2.set_interactable(0);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0015", 0f);
        Label_02C5:
            num6 = this.mCurrentUnit.Exp - num5;
            if (num6 > 0)
            {
                goto Label_02EB;
            }
            this.SpawnAddExpEffect(num6, data.Param);
        Label_02EB:
            this.AnimateGainExp(this.mCurrentUnit.Exp);
            this.QueueKyokaRequest(new DeferredJob(this.SubmitUnitKyoka), 0f);
            GameParameter.UpdateAll(go);
            if (this.mCurrentUnit.Lv == num4)
            {
                goto Label_03A8;
            }
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
            this.mKeepWindowLocked = 1;
            this.ExecQueuedKyokaRequest(null);
            if (this.HoldUseItemEnable == null)
            {
                goto Label_0399;
            }
            if (this.HoldUseItemLvUpStop == null)
            {
                goto Label_0399;
            }
            if ((this.mCurrentKyoukaItemHold != null) == null)
            {
                goto Label_0399;
            }
            this.mCurrentKyoukaItemHold.StatusReset();
            this.mCurrentKyoukaItemHold = null;
        Label_0399:
            base.StartCoroutine(this.PostUnitLevelUp(num4));
        Label_03A8:
            return;
        }

        private void OnExpItemHoldUse(GameObject listItem)
        {
            this.OnExpItemClick(listItem);
            if ((this.mCurrentKyoukaItemHold != null) == null)
            {
                goto Label_0057;
            }
            if ((this.mKyoukaItemScroll == null) == null)
            {
                goto Label_0057;
            }
            this.mKyoukaItemScroll = this.mKyokaPanel.GetComponentInChildren<ScrollRect>();
            if ((this.mKyoukaItemScroll != null) == null)
            {
                goto Label_0057;
            }
            this.mKyoukaItemScroll.set_enabled(0);
        Label_0057:
            return;
        }

        private void OnExpMaxOpen(SRPG_Button button)
        {
            GameObject obj2;
            GameObject obj3;
            UnitLevelUpWindow window;
            if (this.mCurrentUnit.CheckGainExp() != null)
            {
                goto Label_0026;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.LEVEL_CAPPED"), null, null, 0, -1);
            return;
        Label_0026:
            obj2 = AssetManager.Load<GameObject>(UNIT_EXPMAX_UI_PATH);
            if ((obj2 != null) == null)
            {
                goto Label_008E;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_008E;
            }
            window = obj3.GetComponentInChildren<UnitLevelUpWindow>();
            if ((window != null) == null)
            {
                goto Label_0075;
            }
            window.OnDecideEvent = new UnitLevelUpWindow.OnUnitLevelupEvent(this.OnUnitBulkLevelUp);
        Label_0075:
            DataSource.Bind<UnitData>(obj3, this.mCurrentUnit);
            DataSource.Bind<UnitEnhanceV3>(obj3, this);
            GameParameter.UpdateAll(obj3);
        Label_008E:
            return;
        }

        private void OnExpOverflowNo(GameObject dialog)
        {
            ExpItemTouchController controller;
            if ((this.ClickItem != null) == null)
            {
                goto Label_002F;
            }
            controller = this.ClickItem.GetComponent<ExpItemTouchController>();
            if ((controller != null) == null)
            {
                goto Label_002F;
            }
            controller.StatusReset();
        Label_002F:
            this.ClickItem = null;
            if ((this.mKyoukaItemScroll != null) == null)
            {
                goto Label_005A;
            }
            this.mKyoukaItemScroll.set_enabled(1);
            this.mKyoukaItemScroll = null;
        Label_005A:
            return;
        }

        private void OnExpOverflowOk(GameObject dialog)
        {
            this.OnExpItemClick(this.ClickItem);
            return;
        }

        private void OnJobChangeAccept(GameObject go)
        {
            JobParam param;
            string[] strArray;
            int[] numArray;
            bool flag;
            bool flag2;
            GameManager manager;
            int num;
            ItemParam param2;
            ItemData data;
            int num2;
            param = this.mCurrentUnit.CurrentJob.Param;
            strArray = param.GetJobChangeItems(this.mCurrentUnit.CurrentJob.Rank);
            numArray = param.GetJobChangeItemNums(this.mCurrentUnit.CurrentJob.Rank);
            flag = 1;
            flag2 = 1;
            manager = MonoSingleton<GameManager>.Instance;
            if (strArray == null)
            {
                goto Label_00C3;
            }
            if (numArray == null)
            {
                goto Label_00C3;
            }
            num = 0;
            goto Label_00B9;
        Label_005F:
            if (string.IsNullOrEmpty(strArray[num]) == null)
            {
                goto Label_0072;
            }
            goto Label_00B3;
        Label_0072:
            param2 = manager.GetItemParam(strArray[num]);
            if (param2 != null)
            {
                goto Label_008B;
            }
            goto Label_00B3;
        Label_008B:
            if (flag == null)
            {
                goto Label_00B3;
            }
            if (manager.Player.FindItemDataByItemParam(param2).Num >= numArray[num])
            {
                goto Label_00B3;
            }
            flag = 0;
        Label_00B3:
            num += 1;
        Label_00B9:
            if (num < ((int) strArray.Length))
            {
                goto Label_005F;
            }
        Label_00C3:
            num2 = param.GetJobChangeCost(this.mCurrentUnit.CurrentJob.Rank);
            if (num2 <= 0)
            {
                goto Label_00F9;
            }
            if (manager.Player.Gold >= num2)
            {
                goto Label_00F9;
            }
            flag2 = 0;
        Label_00F9:
            if (flag != null)
            {
                goto Label_011C;
            }
            if (flag2 != null)
            {
                goto Label_011C;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.UE_JOBCHANGE_NOITEMGOLD"), null, null, 0, -1);
            return;
        Label_011C:
            if (flag != null)
            {
                goto Label_0138;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.UE_JOBCHANGE_NOITEM"), null, null, 0, -1);
            return;
        Label_0138:
            if (flag2 != null)
            {
                goto Label_0155;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.UE_JOBCHANGE_NOGOLD"), null, null, 0, -1);
            return;
        Label_0155:
            this.SpawnJobChangeButtonEffect();
            this.mKeepWindowLocked = 1;
            base.GetComponent<WindowController>().SetCollision(0);
            this.mJobChangeRequestSent = 0;
            if (Network.Mode != null)
            {
                goto Label_01B6;
            }
            this.RequestAPI(new ReqUnitJob(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, new Network.ResponseCallback(this.OnJobChangeResult)));
            goto Label_01F9;
        Label_01B6:
            MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID).SetJobIndex(this.mCurrentUnit.JobIndex);
            this.mJobChangeRequestSent = 1;
            manager.Player.OnJobChange(this.mCurrentUnit.UnitID);
        Label_01F9:
            this.SetUnitDirty();
            base.StartCoroutine(this.PostJobChange());
            return;
        }

        private void OnJobChangeClick(SRPG_Button button)
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            JobParam param;
            StringBuilder builder;
            string[] strArray;
            int[] numArray;
            GameManager manager;
            int num;
            ItemParam param2;
            int num2;
            int num3;
            int num4;
            this.ExecQueuedKyokaRequest(null);
            if (this.mCurrentUnit.CurrentJob.Rank > 0)
            {
                goto Label_0034;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.UE_JOB_NOT_UNLOCKED"), null, null, 0, -1);
            return;
        Label_0034:
            if (this.mCurrentUnit.CurrentJob.UniqueID != this.mCurrentJobUniqueID)
            {
                goto Label_0050;
            }
            return;
        Label_0050:
            if (button.IsInteractable() != null)
            {
                goto Label_005C;
            }
            return;
        Label_005C:
            param = this.mCurrentUnit.CurrentJob.Param;
            builder = GameUtility.GetStringBuilder();
            objArray1 = new object[] { param.name };
            builder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_CONFIRM", objArray1));
            builder.Append(10);
            strArray = param.GetJobChangeItems(this.mCurrentUnit.CurrentJob.Rank);
            numArray = param.GetJobChangeItemNums(this.mCurrentUnit.CurrentJob.Rank);
            manager = MonoSingleton<GameManager>.Instance;
            if (strArray == null)
            {
                goto Label_015B;
            }
            if (numArray == null)
            {
                goto Label_015B;
            }
            num = 0;
            goto Label_0151;
        Label_00E5:
            if (string.IsNullOrEmpty(strArray[num]) == null)
            {
                goto Label_00F8;
            }
            goto Label_014B;
        Label_00F8:
            param2 = manager.GetItemParam(strArray[num]);
            if (param2 != null)
            {
                goto Label_0111;
            }
            goto Label_014B;
        Label_0111:
            num2 = numArray[num];
            objArray2 = new object[] { param2.name, (int) num2 };
            builder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_REQITEM", objArray2));
            builder.Append(10);
        Label_014B:
            num += 1;
        Label_0151:
            if (num < ((int) strArray.Length))
            {
                goto Label_00E5;
            }
        Label_015B:
            num3 = param.GetJobChangeCost(this.mCurrentUnit.CurrentJob.Rank);
            if (num3 <= 0)
            {
                goto Label_019C;
            }
            objArray3 = new object[] { (int) num3 };
            builder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_REQGOLD", objArray3));
        Label_019C:
            this.mPrevJobID = null;
            num4 = 0;
            goto Label_01ED;
        Label_01AB:
            if (this.mCurrentUnit.Jobs[num4].UniqueID != this.mCurrentJobUniqueID)
            {
                goto Label_01E7;
            }
            this.mPrevJobID = this.mCurrentUnit.Jobs[num4].Param.iname;
        Label_01E7:
            num4 += 1;
        Label_01ED:
            if (num4 < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_01AB;
            }
            this.mNextJobID = this.mCurrentUnit.CurrentJob.Param.iname;
            UIUtility.ConfirmBox(builder.ToString(), new UIUtility.DialogResultEvent(this.OnJobChangeAccept), null, null, 0, -1, null, null);
            return;
        }

        private unsafe void OnJobChangeResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            GameManager manager;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0039;
            }
            switch ((Network.ErrCode - 0x8fc))
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_002D;

                case 2:
                    goto Label_002D;
            }
            goto Label_0033;
        Label_002D:
            FlowNode_Network.Failed();
            return;
        Label_0033:
            FlowNode_Network.Retry();
            return;
        Label_0039:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0046:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                Network.RemoveAPI();
                goto Label_00B0;
            }
            catch (Exception exception1)
            {
            Label_009A:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_00D9;
            }
        Label_00B0:
            this.mJobChangeRequestSent = 1;
            this.RefreshSortedUnits();
            MonoSingleton<GameManager>.Instance.Player.OnJobChange(this.mCurrentUnit.UnitID);
        Label_00D9:
            return;
        }

        private void OnJobRankUpClick(SRPG_Button button)
        {
            JobData data;
            int num;
            string str;
            string str2;
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (<>f__am$cacheEB != null)
            {
                goto Label_002F;
            }
            <>f__am$cacheEB = new Predicate<EquipData>(UnitEnhanceV3.<OnJobRankUpClick>m__44C);
        Label_002F:
            if (Array.FindIndex<EquipData>(this.mCurrentUnit.CurrentEquips, <>f__am$cacheEB) == -1)
            {
                goto Label_0057;
            }
            this.OnEquipAll(button == this.mEquipmentPanel.JobRankupAllIn);
            return;
        Label_0057:
            if (this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) > this.mCurrentUnit.CurrentJob.Rank)
            {
                goto Label_0083;
            }
            return;
        Label_0083:
            if ((button == this.mEquipmentPanel.JobRankupAllIn) == null)
            {
                goto Label_00A1;
            }
            this.OnEquipAll(1);
            return;
        Label_00A1:
            if (this.mCurrentUnit.JobIndex < this.mCurrentUnit.NumJobsAvailable)
            {
                goto Label_0144;
            }
            data = this.mCurrentUnit.GetBaseJob(this.mCurrentUnit.CurrentJob.JobID);
            if (Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, data) >= 0)
            {
                goto Label_00F2;
            }
            return;
        Label_00F2:
            UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.CONFIRM_CLASSCHANGE"), (data == null) ? string.Empty : data.Name, this.mCurrentUnit.CurrentJob.Name), new UIUtility.DialogResultEvent(this.<OnJobRankUpClick>m__44D), null, null, 0, -1, null, null);
            return;
        Label_0144:
            this.StartJobRankUp(-1, 0, 0);
            return;
        }

        private void OnJobRankUpEquipAllAccept(int target_rank, bool can_jobmaster, bool can_jobmax)
        {
            this.mIsJobLvUpAllEquip = 1;
            this.StartJobRankUp(target_rank, can_jobmaster, can_jobmax);
            return;
        }

        private unsafe void OnJobRankUpResult(WWWResult www)
        {
            ArtifactParam param;
            UnitData data;
            EquipData[] dataArray;
            int num;
            int num2;
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            UnitData data2;
            int num3;
            EquipData[] dataArray2;
            int num4;
            UnitData data3;
            ArtifactParam param2;
            string str;
            string str2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0047;
            }
            code = Network.ErrCode;
            if (code == 0xa8c)
            {
                goto Label_002E;
            }
            if (code == 0xa8d)
            {
                goto Label_0034;
            }
            goto Label_0041;
        Label_002E:
            FlowNode_Network.Failed();
            return;
        Label_0034:
            FlowNode_Network.Failed();
            this.mJobRankUpRequestSent = 1;
            return;
        Label_0041:
            FlowNode_Network.Retry();
            return;
        Label_0047:
            param = this.mCurrentUnit.GetSelectedSkin(-1);
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
            dataArray = data.GetRankupEquips(this.mCurrentUnit.JobIndex);
            num = 0;
            num2 = 0;
            goto Label_00A3;
        Label_008B:
            if (dataArray[num2].IsEquiped() == null)
            {
                goto Label_009D;
            }
            num += 1;
        Label_009D:
            num2 += 1;
        Label_00A3:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_008B;
            }
            if (data.CurrentJob.Rank <= 1)
            {
                goto Label_00D0;
            }
            num += (data.CurrentJob.Rank - 1) * 6;
        Label_00D0:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_00DE:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                Network.RemoveAPI();
                goto Label_014D;
            }
            catch (Exception exception1)
            {
            Label_0135:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_029C;
            }
        Label_014D:
            this.SetNotifyReleaseClassChangeJob();
            this.UpdateTrophy_OnJobLevelChange();
            data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
            num3 = 0;
            dataArray2 = data2.GetRankupEquips(this.mCurrentUnit.JobIndex);
            num4 = 0;
            goto Label_01AF;
        Label_0194:
            if (dataArray2[num4].IsEquiped() == null)
            {
                goto Label_01A9;
            }
            num3 += 1;
        Label_01A9:
            num4 += 1;
        Label_01AF:
            if (num4 < ((int) dataArray2.Length))
            {
                goto Label_0194;
            }
            if (data2.CurrentJob.Rank <= 1)
            {
                goto Label_01E1;
            }
            num3 += (data2.CurrentJob.Rank - 1) * 6;
        Label_01E1:
            MonoSingleton<GameManager>.Instance.Player.OnSoubiSet(this.mCurrentUnit.UnitID, num3 - num);
            this.RefreshSortedUnits();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), null);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
            this.mJobRankUpRequestSent = 1;
            data3 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
            if (data3 == null)
            {
                goto Label_029C;
            }
            param2 = data3.GetSelectedSkin(-1);
            str = (param == null) ? null : param.iname;
            str2 = (param2 == null) ? null : param2.iname;
            if ((str != str2) == null)
            {
                goto Label_029C;
            }
            this.ShowSkinParamChange(param2, 1);
        Label_029C:
            return;
        }

        public void OnJobSlotClick(GameObject target)
        {
            this.ChangeJobSlot(int.Parse(target.get_name()), target);
            return;
        }

        private void OnKakuseiAccept(GameObject go, bool visible)
        {
            if (visible != null)
            {
                goto Label_0024;
            }
            this.KakeraWindow.GetComponent<WindowController>().OnWindowStateChange = null;
            base.StartCoroutine(this.PostUnitKakusei());
        Label_0024:
            return;
        }

        private void OnKakuseiButtonClick(SRPG_Button button)
        {
            this.OpenKakeraWindow();
            return;
        }

        private void OnKakuseiCancel(GameObject go, bool visible)
        {
            if (visible != null)
            {
                goto Label_001E;
            }
            go.GetComponent<WindowController>().OnWindowStateChange = null;
            base.GetComponent<WindowController>().SetCollision(1);
        Label_001E:
            return;
        }

        private void OnRankUpButtonPressHold(AbilityData abilityData, GameObject gobj)
        {
            UnitAbilityListItemEvents events;
            if ((gobj != null) == null)
            {
                goto Label_002B;
            }
            events = gobj.GetComponent<UnitAbilityListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_002B;
            }
            this.mCurrentAbilityRankUpHold = events.ItemTouchController;
        Label_002B:
            return;
        }

        private bool OnSceneCHange()
        {
            if (this.mSceneChanging != null)
            {
                goto Label_0028;
            }
            this.mSceneChanging = 1;
            MonoSingleton<GameManager>.Instance.RegisterImportantJob(base.StartCoroutine(this.OnSceneChangeAsync()));
        Label_0028:
            return 1;
        }

        [DebuggerHidden]
        private IEnumerator OnSceneChangeAsync()
        {
            <OnSceneChangeAsync>c__Iterator14B iteratorb;
            iteratorb = new <OnSceneChangeAsync>c__Iterator14B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        private void OnShiftUnit(SRPG_Button button)
        {
            int num;
            int num2;
            int num3;
            UnitData data;
            long num4;
            num = 0;
            if (button.get_interactable() != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            if ((button == this.NextUnitButton) == null)
            {
                goto Label_0026;
            }
            num = 1;
            goto Label_0028;
        Label_0026:
            num = -1;
        Label_0028:
            num2 = this.Units.Count;
            if (num2 != null)
            {
                goto Label_003B;
            }
            return;
        Label_003B:
            num3 = ((this.CurrentUnitIndex + num) + num2) % num2;
            data = this.Units[num3];
            num4 = data.UniqueID;
            base.GetComponent<WindowController>().SetCollision(0);
            this.mKeepWindowLocked = 1;
            this.ExecQueuedKyokaRequest(null);
            base.StartCoroutine(this.ShiftUnitAsync(num4));
            return;
        }

        private void OnSkinDecide(ArtifactParam artifact)
        {
            this.ShowSkinParamChange(artifact, 0);
            return;
        }

        private void OnSkinDecideAll(ArtifactParam artifact)
        {
            this.UpdataSkinParamChange(artifact, 1);
            return;
        }

        private void OnSkinRemoved()
        {
            this.UpdataSkinParamChange(null, 1);
            return;
        }

        private void OnSkinRemovedAll()
        {
            this.UpdataSkinParamChange(null, 1);
            return;
        }

        private void OnSkinSelect(ArtifactParam artifact)
        {
            this.ShowSkinParamChange(artifact, 1);
            return;
        }

        private void OnSkinSelectOpen(SRPG_Button button)
        {
            if (this.SkinSelectTemplate == null)
            {
                goto Label_0016;
            }
            if (this.mCurrentUnit != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            base.StartCoroutine(this.OnSkinSelectOpenAsync(0));
            return;
        }

        [DebuggerHidden]
        private IEnumerator OnSkinSelectOpenAsync(bool isView)
        {
            <OnSkinSelectOpenAsync>c__Iterator161 iterator;
            iterator = new <OnSkinSelectOpenAsync>c__Iterator161();
            iterator.isView = isView;
            iterator.<$>isView = isView;
            iterator.<>f__this = this;
            return iterator;
        }

        private void OnSkinSelectOpenForViewer(SRPG_Button button)
        {
            if (this.SkinSelectTemplate == null)
            {
                goto Label_0016;
            }
            if (this.mCurrentUnit != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            base.StartCoroutine(this.OnSkinSelectOpenAsync(1));
            return;
        }

        private unsafe void OnSkinSetResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            base.GetComponent<WindowController>().SetCollision(1);
            if (Network.IsError == null)
            {
                goto Label_0022;
            }
            code = Network.ErrCode;
            FlowNode_Network.Retry();
            return;
        Label_0022:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0051;
            }
            FlowNode_Network.Retry();
            return;
        Label_0051:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                goto Label_00A1;
            }
            catch (Exception exception1)
            {
            Label_008B:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_00B2;
            }
        Label_00A1:
            Network.RemoveAPI();
            this.RefreshSortedUnits();
            this.SetUnitDirty();
        Label_00B2:
            return;
        }

        private void OnSkinWindowClose()
        {
            base.GetComponent<WindowController>().SetCollision(1);
            this.mKeepWindowLocked = 0;
            return;
        }

        private void OnSlotAbilitySelect(AbilityData ability, GameObject itemGO)
        {
            int num;
            num = this.mAbilityPicker.AbilitySlot;
            this.mAbilityPicker.GetComponent<WindowController>().Close();
            this.BeginStatusChangeCheck();
            this.mCurrentUnit.SetEquipAbility(this.mCurrentUnit.JobIndex, num, (ability == null) ? 0L : ability.UniqueID);
            MonoSingleton<GameManager>.Instance.Player.OnChangeAbilitySet(this.mCurrentUnit.UnitID);
            this.SpawnStatusChangeEffects();
            this.RefreshBasicParameters(0);
            this.RefreshAbilitySlots(0);
            this.QueueKyokaRequest(new DeferredJob(this.Req_UpdateEquippedAbility), 0f);
            return;
        }

        private unsafe void OnSubmitAbilityRankUpResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002C;
            }
            if (Network.ErrCode == 0x898)
            {
                goto Label_0020;
            }
            goto Label_0026;
        Label_0020:
            FlowNode_Network.Back();
            return;
        Label_0026:
            FlowNode_Network.Retry();
            return;
        Label_002C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0039:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                Network.RemoveAPI();
                goto Label_008E;
            }
            catch (Exception exception1)
            {
            Label_0078:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_00A6;
            }
        Label_008E:
            this.FinishKyokaRequest();
            this.mAbilityRankUpRequestSent = 1;
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
        Label_00A6:
            return;
        }

        private void OnTabChange(SRPG_Button button)
        {
            UnitEnhancePanel panel;
            if (this.TabChange(button) != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            panel = this.mTabPages[this.mActiveTabIndex];
            this.ExecQueuedKyokaRequest(null);
            if ((panel == this.mEquipmentPanel) == null)
            {
                goto Label_0051;
            }
            this.PlayUnitVoice("chara_0012");
            if (this.mEquipmentPanelDirty == null)
            {
                goto Label_0051;
            }
            this.RefreshEquipments(-1);
        Label_0051:
            if ((panel == this.mAbilityListPanel) == null)
            {
                goto Label_007E;
            }
            this.PlayUnitVoice("chara_0014");
            if (this.mAbilityListDirty == null)
            {
                goto Label_007E;
            }
            this.RefreshAbilityList();
        Label_007E:
            if ((panel == this.mAbilitySlotPanel) == null)
            {
                goto Label_00AC;
            }
            this.PlayUnitVoice("chara_0015");
            if (this.mAbilitySlotDirty == null)
            {
                goto Label_00AC;
            }
            this.RefreshAbilitySlots(0);
        Label_00AC:
            if ((panel == this.mKyokaPanel) == null)
            {
                goto Label_00C8;
            }
            this.PlayUnitVoice("chara_0016");
        Label_00C8:
            this.mLeftTime = Time.get_realtimeSinceStartup();
            return;
        }

        private unsafe void OnUnitAwake2(int awake_lv)
        {
            string str;
            string str2;
            if (this.IsKyokaRequestQueued == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mCurrentUnit.CheckUnitAwaking(awake_lv) != null)
            {
                goto Label_0028;
            }
            DebugUtility.LogWarning("[UnitEnhanceV3]OnUnitAwake2=>Not Unit Awake!");
            return;
        Label_0028:
            this.KakeraWindow.GetComponent<WindowController>().Close();
            this.KakeraWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnKakuseiAccept);
            this.mKakuseiRequestSent = 0;
            MonoSingleton<GameManager>.Instance.Player.OnLimitBreak(this.mCurrentUnit.UnitID, awake_lv);
            str = string.Empty;
            str2 = string.Empty;
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(&str, &str2);
            this.mCacheAwakeLv = awake_lv;
            this.RequestAPI(new ReqUnitAwake(this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.OnUnitKakuseiResult), str, str2, this.mCurrentUnit.AwakeLv + awake_lv));
            this.SetUnitDirty();
            return;
        }

        private unsafe void OnUnitBulkLevelUp(Dictionary<string, int> data)
        {
            int num;
            Dictionary<string, int>.Enumerator enumerator;
            ItemData data2;
            int num2;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator2;
            <OnUnitBulkLevelUp>c__AnonStorey3BC storeybc;
            Dictionary<string, int> dictionary;
            string str;
            int num3;
            if (this.mSceneChanging == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (data == null)
            {
                goto Label_001E;
            }
            if (data.Count > 0)
            {
                goto Label_001F;
            }
        Label_001E:
            return;
        Label_001F:
            this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
            num = this.mCurrentUnit.Lv;
            this.BeginStatusChangeCheck();
            storeybc = new <OnUnitBulkLevelUp>c__AnonStorey3BC();
            enumerator = data.GetEnumerator();
        Label_0050:
            try
            {
                goto Label_00BF;
            Label_0055:
                storeybc.p = &enumerator.Current;
                data2 = this.mTmpItems.Find(new Predicate<ItemData>(storeybc.<>m__449));
                if (data2 == null)
                {
                    goto Label_00BF;
                }
                num2 = 0;
                goto Label_00AD;
            Label_0089:
                if (MonoSingleton<GameManager>.Instance.Player.UseExpPotion(this.mCurrentUnit, data2) != null)
                {
                    goto Label_00A9;
                }
                goto Label_0204;
            Label_00A9:
                num2 += 1;
            Label_00AD:
                if (num2 < &storeybc.p.Value)
                {
                    goto Label_0089;
                }
            Label_00BF:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0055;
                }
                goto Label_00DC;
            }
            finally
            {
            Label_00D0:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_00DC:
            enumerator2 = data.GetEnumerator();
        Label_00E4:
            try
            {
                goto Label_0154;
            Label_00E9:
                pair = &enumerator2.Current;
                if (this.mUsedExpItems.ContainsKey(&pair.Key) == null)
                {
                    goto Label_013B;
                }
                num3 = dictionary[str];
                (dictionary = this.mUsedExpItems)[str = &pair.Key] = num3 + &pair.Value;
                goto Label_0154;
            Label_013B:
                this.mUsedExpItems.Add(&pair.Key, &pair.Value);
            Label_0154:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00E9;
                }
                goto Label_0172;
            }
            finally
            {
            Label_0165:
                ((Dictionary<string, int>.Enumerator) enumerator2).Dispose();
            }
        Label_0172:
            this.AnimateGainExp(this.mCurrentUnit.Exp);
            this.QueueKyokaRequest(new DeferredJob(this.SubmitUnitKyoka), 0f);
            GameParameter.UpdateAll(this.mKyokaPanel.get_gameObject());
            this.RefreshExpItemsButtonState();
            if (this.mCurrentUnit.Lv == num)
            {
                goto Label_0204;
            }
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
            this.mKeepWindowLocked = 1;
            this.ExecQueuedKyokaRequest(null);
            base.StartCoroutine(this.PostUnitLevelUp(num));
        Label_0204:
            return;
        }

        private unsafe void OnUnitEvolutionResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002C;
            }
            if (Network.ErrCode == 0x7d0)
            {
                goto Label_0020;
            }
            goto Label_0026;
        Label_0020:
            FlowNode_Network.Failed();
            return;
        Label_0026:
            FlowNode_Network.Retry();
            return;
        Label_002C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0039:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                goto Label_009E;
            }
            catch (Exception exception1)
            {
            Label_0088:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_013B;
            }
        Label_009E:
            Network.RemoveAPI();
            this.mEvolutionRequestSent = 1;
            this.RebuildUnitData();
            this.RefreshSortedUnits();
            this.RefreshJobInfo();
            this.RefreshJobIcons(0);
            this.RefreshEquipments(-1);
            this.RefreshBasicParameters(0);
            this.UpdateJobRankUpButtonState();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), null);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
            this.CheckPlayBackUnlock();
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(2);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
        Label_013B:
            return;
        }

        private unsafe void OnUnitEvolve()
        {
            string str;
            string str2;
            GameManager manager;
            if (this.IsKyokaRequestQueued == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mCurrentUnit.CheckUnitRarityUp() != null)
            {
                goto Label_0027;
            }
            Debug.LogError("なんかエラーだす");
            return;
        Label_0027:
            this.mEvolutionWindow.GetComponent<WindowController>().Close();
            this.mEvolutionWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnEvolutionStart);
            this.mEvolutionRequestSent = 0;
            if (Network.Mode != null)
            {
                goto Label_00C0;
            }
            MonoSingleton<GameManager>.Instance.Player.OnEvolutionChange(this.mCurrentUnit.UnitID, this.mCurrentUnit.Rarity);
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(&str, &str2);
            this.RequestAPI(new ReqUnitRare(this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.OnUnitEvolutionResult), str, str2));
            goto Label_00EE;
        Label_00C0:
            this.mEvolutionRequestSent = 1;
            MonoSingleton<GameManager>.GetInstanceDirect().Player.OnEvolutionChange(this.mCurrentUnit.UnitID, this.mCurrentUnit.Rarity);
        Label_00EE:
            this.SetUnitDirty();
            return;
        }

        private unsafe void OnUnitKakusei()
        {
            string str;
            string str2;
            GameManager manager;
            if (this.IsKyokaRequestQueued == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mCurrentUnit.CheckUnitAwaking() != null)
            {
                goto Label_0027;
            }
            Debug.LogError("なんかエラーだす");
            return;
        Label_0027:
            this.KakeraWindow.GetComponent<WindowController>().Close();
            this.KakeraWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnKakuseiAccept);
            this.mKakuseiRequestSent = 0;
            if (Network.Mode != null)
            {
                goto Label_00B6;
            }
            MonoSingleton<GameManager>.Instance.Player.OnLimitBreak(this.mCurrentUnit.UnitID, 1);
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(&str, &str2);
            this.RequestAPI(new ReqUnitPlus(this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.OnUnitKakuseiResult), str, str2));
            goto Label_00DA;
        Label_00B6:
            this.mKakuseiRequestSent = 1;
            MonoSingleton<GameManager>.GetInstanceDirect().Player.OnLimitBreak(this.mCurrentUnit.UnitID, 1);
        Label_00DA:
            this.SetUnitDirty();
            return;
        }

        private unsafe void OnUnitKakuseiResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002C;
            }
            if (Network.ErrCode == 0x834)
            {
                goto Label_0020;
            }
            goto Label_0026;
        Label_0020:
            FlowNode_Network.Failed();
            return;
        Label_0026:
            FlowNode_Network.Retry();
            return;
        Label_002C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0039:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                goto Label_009E;
            }
            catch (Exception exception1)
            {
            Label_0088:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_00BB;
            }
        Label_009E:
            Network.RemoveAPI();
            this.RefreshSortedUnits();
            this.mKakuseiRequestSent = 1;
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
        Label_00BB:
            return;
        }

        private unsafe void OnUnitKyokaResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002C;
            }
            if (Network.ErrCode == 0x76c)
            {
                goto Label_0020;
            }
            goto Label_0026;
        Label_0020:
            FlowNode_Network.Failed();
            return;
        Label_0026:
            FlowNode_Network.Retry();
            return;
        Label_002C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            if (response.body != null)
            {
                goto Label_004A;
            }
            FlowNode_Network.Failed();
            return;
        Label_004A:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                goto Label_00AF;
            }
            catch (Exception exception1)
            {
            Label_0099:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_00BA;
            }
        Label_00AF:
            Network.RemoveAPI();
            this.FinishKyokaRequest();
        Label_00BA:
            return;
        }

        private void OnUnitUnlockTobira()
        {
            base.StartCoroutine(this.UnitUnlockTobira());
            return;
        }

        private unsafe void OnUnitUnlockTobiraResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002C;
            }
            if (Network.ErrCode == 0x7d0)
            {
                goto Label_0020;
            }
            goto Label_0026;
        Label_0020:
            FlowNode_Network.Failed();
            return;
        Label_0026:
            FlowNode_Network.Retry();
            return;
        Label_002C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0039:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                goto Label_009E;
            }
            catch (Exception exception1)
            {
            Label_0088:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_0154;
            }
        Label_009E:
            Network.RemoveAPI();
            this.mUnlockTobiraRequestSent = 1;
            this.RebuildUnitData();
            this.RefreshSortedUnits();
            this.RefreshJobInfo();
            this.RefreshJobIcons(0);
            this.RefreshEquipments(-1);
            this.RefreshBasicParameters(0);
            this.UpdateJobRankUpButtonState();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), null);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
            this.CheckPlayBackUnlock();
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(2);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
            MonoSingleton<GameManager>.Instance.Player.OnUnlockTobiraTrophy(GlobalVars.SelectedUnitUniqueID);
        Label_0154:
            return;
        }

        public void OnUnlockTobiraRestore()
        {
            this.OpenUnlockTobiraWindow();
            return;
        }

        private void OnWindowStateChange(GameObject go, bool visible)
        {
            if (visible != null)
            {
                goto Label_0082;
            }
            if (this.mCloseRequested == null)
            {
                goto Label_0082;
            }
            this.mCloseRequested = 0;
            this.FadeUnitImage(0f, 0f, 0f);
            this.SetPreviewVisible(0);
            if ((UnitManagementWindow.Instance.UnitModelPreviewBase != null) == null)
            {
                goto Label_005E;
            }
            UnitManagementWindow.Instance.UnitModelPreviewBase.get_gameObject().SetActive(0);
        Label_005E:
            if (this.ExecQueuedKyokaRequest(null) == null)
            {
                goto Label_007C;
            }
            base.StartCoroutine(this.WaitForKyokaRequestAndInvokeUserClose());
            goto Label_0082;
        Label_007C:
            this.InvokeUserClose();
        Label_0082:
            return;
        }

        private void OpenCharacterQuestPopupInternal()
        {
            MonoSingleton<GameManager>.Instance.AddCharacterQuestPopup(this.mCurrentUnit);
            MonoSingleton<GameManager>.Instance.ShowCharacterQuestPopup(GameSettings.Instance.CharacterQuest_Unlock);
            return;
        }

        [DebuggerHidden]
        private IEnumerator OpenCharacterQuestPopupInternalAsync()
        {
            <OpenCharacterQuestPopupInternalAsync>c__Iterator160 iterator;
            iterator = new <OpenCharacterQuestPopupInternalAsync>c__Iterator160();
            iterator.<>f__this = this;
            return iterator;
        }

        public void OpenEquipmentSlot(int slotIndex)
        {
            if (slotIndex >= 0)
            {
                goto Label_0008;
            }
            return;
        Label_0008:
            this.mSelectedEquipmentSlot = slotIndex;
            base.GetComponent<WindowController>().SetCollision(0);
            this.EquipmentWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnEquipmentWindowCancel);
            WindowController.OpenIfAvailable(this.EquipmentWindow);
            if ((this.EquipmentWindow.SubWindow != null) == null)
            {
                goto Label_0069;
            }
            this.EquipmentWindow.SubWindow.SetActive(0);
        Label_0069:
            this.EquipmentWindow.Refresh(this.mCurrentUnit, slotIndex);
            return;
        }

        public void OpenKakeraWindow()
        {
            int num;
            int num2;
            int num3;
            MasterParam param;
            int num4;
            JobSetParam param2;
            JobSetParam[] paramArray;
            bool flag;
            int num5;
            int num6;
            string str;
            int num7;
            int num8;
            if ((this.KakeraWindow == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.ExecQueuedKyokaRequest(null);
            this.mUnlockJobParam = null;
            if (this.mCurrentUnit.Jobs == null)
            {
                goto Label_01F1;
            }
            num = this.mCurrentUnit.Rarity;
            num2 = this.mCurrentUnit.AwakeLv;
            num3 = this.mCurrentUnit.GetAwakeLevelCap();
            if (num2 >= num3)
            {
                goto Label_01F1;
            }
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
            num4 = 0;
            goto Label_01DD;
        Label_006F:
            if (this.mCurrentUnit.Jobs[num4].IsActivated == null)
            {
                goto Label_008C;
            }
            goto Label_01D7;
        Label_008C:
            param2 = this.mCurrentUnit.GetJobSetParam(num4);
            if (param2 == null)
            {
                goto Label_01D7;
            }
            if (num >= param2.lock_rarity)
            {
                goto Label_00B4;
            }
            goto Label_01D7;
        Label_00B4:
            paramArray = param.GetClassChangeJobSetParam(this.mCurrentUnit.UnitParam.iname);
            if (paramArray == null)
            {
                goto Label_0128;
            }
            if (((int) paramArray.Length) <= 0)
            {
                goto Label_0128;
            }
            flag = 0;
            num5 = 0;
            goto Label_0111;
        Label_00E8:
            if ((paramArray[num5].iname == param2.iname) == null)
            {
                goto Label_010B;
            }
            flag = 1;
            goto Label_011C;
        Label_010B:
            num5 += 1;
        Label_0111:
            if (num5 < ((int) paramArray.Length))
            {
                goto Label_00E8;
            }
        Label_011C:
            if (flag == null)
            {
                goto Label_0128;
            }
            goto Label_01D7;
        Label_0128:
            if (param2.lock_jobs == null)
            {
                goto Label_01A5;
            }
            num6 = 0;
            goto Label_0195;
        Label_013C:
            if (param2.lock_jobs[num6] != null)
            {
                goto Label_0150;
            }
            goto Label_018F;
        Label_0150:
            str = param2.lock_jobs[num6].iname;
            num7 = param2.lock_jobs[num6].lv;
            num8 = this.mCurrentUnit.GetJobLevelByJobID(str);
            if (num7 <= num8)
            {
                goto Label_018F;
            }
        Label_018F:
            num6 += 1;
        Label_0195:
            if (num6 < ((int) param2.lock_jobs.Length))
            {
                goto Label_013C;
            }
        Label_01A5:
            if ((num2 + 1) == param2.lock_awakelv)
            {
                goto Label_01B9;
            }
            goto Label_01D7;
        Label_01B9:
            this.mUnlockJobParam = this.mCurrentUnit.Jobs[num4].Param;
            goto Label_01F1;
        Label_01D7:
            num4 += 1;
        Label_01DD:
            if (num4 < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_006F;
            }
        Label_01F1:
            WindowController.OpenIfAvailable(this.KakeraWindow);
            this.KakeraWindow.Refresh(this.mCurrentUnit, this.mUnlockJobParam);
            base.GetComponent<WindowController>().SetCollision(0);
            this.KakeraWindow.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnKakuseiCancel);
            return;
        }

        private void OpenLeaderSkillDetail(SRPG_Button button)
        {
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.LeaderSkillDetailButton != null) == null)
            {
                goto Label_0061;
            }
            if ((this.Prefab_LeaderSkillDetail != null) == null)
            {
                goto Label_0061;
            }
            if ((this.mLeaderSkillDetail == null) == null)
            {
                goto Label_0061;
            }
            this.mLeaderSkillDetail = Object.Instantiate<GameObject>(this.Prefab_LeaderSkillDetail);
            DataSource.Bind<UnitData>(this.mLeaderSkillDetail, this.mCurrentUnit);
        Label_0061:
            return;
        }

        private void OpenMapEffectJob()
        {
            Transform transform;
            GameObject obj2;
            MapEffectJob job;
            if (this.mCurrentUnit == null)
            {
                goto Label_003C;
            }
            if (this.mReqMapEffectJob == null)
            {
                goto Label_003C;
            }
            if (this.mReqMapEffectJob.isDone == null)
            {
                goto Label_003C;
            }
            if ((this.mReqMapEffectJob.asset == null) == null)
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
            obj2 = MapEffectQuest.CreateInstance(this.mReqMapEffectJob.asset as GameObject, transform);
            if (obj2 != null)
            {
                goto Label_0079;
            }
            return;
        Label_0079:
            DataSource.Bind<JobParam>(obj2, this.mCurrentUnit.CurrentJob.Param);
            obj2.SetActive(1);
            job = obj2.GetComponent<MapEffectJob>();
            if (job == null)
            {
                goto Label_00AE;
            }
            job.Setup();
        Label_00AE:
            return;
        }

        public void OpenProfile()
        {
            if ((this.mUnitProfileWindow == null) == null)
            {
                goto Label_0029;
            }
            if (this.mProfileWindowLoadRequest != null)
            {
                goto Label_0029;
            }
            base.StartCoroutine(this._OpenProfileWindow());
        Label_0029:
            return;
        }

        private void OpenUnlockTobiraWindow()
        {
            GameObject obj2;
            GameObject obj3;
            if (this.mCurrentUnit.CanUnlockTobira() != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if (string.IsNullOrEmpty(this.PREFAB_PATH_UNLOCK_TOBIRA_WINDOW) == null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            obj2 = AssetManager.Load<GameObject>(this.PREFAB_PATH_UNLOCK_TOBIRA_WINDOW);
            if ((obj2 != null) == null)
            {
                goto Label_0064;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            this.mUnlockTobiraWindow = obj3.GetComponent<UnitUnlockTobiraWindow>();
            this.mUnlockTobiraWindow.OnCallback = new UnitUnlockTobiraWindow.CallbackEvent(this.OnUnitUnlockTobira);
        Label_0064:
            return;
        }

        private void PlayReaction()
        {
            if ((this.mCurrentPreview == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mCurrentPreview.PlayAction = 1;
            return;
        }

        [DebuggerHidden]
        public IEnumerator PlayTobiraLevelupEffect(TobiraData tobira)
        {
            <PlayTobiraLevelupEffect>c__Iterator164 iterator;
            iterator = new <PlayTobiraLevelupEffect>c__Iterator164();
            iterator.tobira = tobira;
            iterator.<$>tobira = tobira;
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator PlayTobiraLevelupMaxEffect(AbilityData newAbilityData, AbilityData oldAbilityData, SkillData newLeaderSkillData)
        {
            <PlayTobiraLevelupMaxEffect>c__Iterator165 iterator;
            iterator = new <PlayTobiraLevelupMaxEffect>c__Iterator165();
            iterator.newAbilityData = newAbilityData;
            iterator.oldAbilityData = oldAbilityData;
            iterator.newLeaderSkillData = newLeaderSkillData;
            iterator.<$>newAbilityData = newAbilityData;
            iterator.<$>oldAbilityData = oldAbilityData;
            iterator.<$>newLeaderSkillData = newLeaderSkillData;
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        public IEnumerator PlayTobiraOpenEffect(TobiraParam.Category selected_tobira_category)
        {
            <PlayTobiraOpenEffect>c__Iterator163 iterator;
            iterator = new <PlayTobiraOpenEffect>c__Iterator163();
            iterator.selected_tobira_category = selected_tobira_category;
            iterator.<$>selected_tobira_category = selected_tobira_category;
            iterator.<>f__this = this;
            return iterator;
        }

        private void PlayUnitVoice(string name)
        {
            if (this.MuteVoice == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mCurrentUnit != null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if (this.mUnitVoice != null)
            {
                goto Label_002E;
            }
            DebugUtility.LogError("UnitVoiceが存在しません");
            return;
        Label_002E:
            this.mUnitVoice.Play(name, 0f, 1);
            this.mLeftTime = Time.get_realtimeSinceStartup();
            return;
        }

        [DebuggerHidden]
        private IEnumerator PostAbilityRankUp(List<SkillParam> newSkills)
        {
            <PostAbilityRankUp>c__Iterator157 iterator;
            iterator = new <PostAbilityRankUp>c__Iterator157();
            iterator.newSkills = newSkills;
            iterator.<$>newSkills = newSkills;
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator PostEquip()
        {
            <PostEquip>c__Iterator14C iteratorc;
            iteratorc = new <PostEquip>c__Iterator14C();
            iteratorc.<>f__this = this;
            return iteratorc;
        }

        [DebuggerHidden]
        private IEnumerator PostJobChange()
        {
            <PostJobChange>c__Iterator150 iterator;
            iterator = new <PostJobChange>c__Iterator150();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator PostJobMaster()
        {
            <PostJobMaster>c__Iterator14D iteratord;
            iteratord = new <PostJobMaster>c__Iterator14D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        [DebuggerHidden]
        private IEnumerator PostJobRankUp(int target_rank, bool can_jobmaster, bool can_jobmax)
        {
            <PostJobRankUp>c__Iterator151 iterator;
            iterator = new <PostJobRankUp>c__Iterator151();
            iterator.target_rank = target_rank;
            iterator.can_jobmaster = can_jobmaster;
            iterator.can_jobmax = can_jobmax;
            iterator.<$>target_rank = target_rank;
            iterator.<$>can_jobmaster = can_jobmaster;
            iterator.<$>can_jobmax = can_jobmax;
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator PostUnitEvolution()
        {
            <PostUnitEvolution>c__Iterator15B iteratorb;
            iteratorb = new <PostUnitEvolution>c__Iterator15B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        [DebuggerHidden]
        private IEnumerator PostUnitKakusei()
        {
            <PostUnitKakusei>c__Iterator159 iterator;
            iterator = new <PostUnitKakusei>c__Iterator159();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator PostUnitLevelUp(int prevLv)
        {
            <PostUnitLevelUp>c__Iterator14E iteratore;
            iteratore = new <PostUnitLevelUp>c__Iterator14E();
            iteratore.prevLv = prevLv;
            iteratore.<$>prevLv = prevLv;
            iteratore.<>f__this = this;
            return iteratore;
        }

        [DebuggerHidden]
        private IEnumerator PostUnitUnlockTobira()
        {
            <PostUnitUnlockTobira>c__Iterator15E iteratore;
            iteratore = new <PostUnitUnlockTobira>c__Iterator15E();
            iteratore.<>f__this = this;
            return iteratore;
        }

        private void QueueKyokaRequest(DeferredJob func, float delay)
        {
            this.mDefferedCallFunc = func;
            this.mDefferedCallDelay = delay;
            return;
        }

        private void RebuildUnitData()
        {
            UnitData data;
            string str;
            int num;
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID);
            this.mSelectedJobIndex = data.JobIndex;
            this.mLastSyncTime = data.LastSyncTime;
            str = (this.mCurrentUnit == null) ? null : this.mCurrentUnit.CurrentJob.JobID;
            this.mCurrentUnit = new UnitData();
            this.mCurrentUnit.Setup(data);
            if ((this.mEquipmentPanel.mEquipConceptCardIcon != null) == null)
            {
                goto Label_0098;
            }
            this.mEquipmentPanel.mEquipConceptCardIcon.Setup(this.mCurrentUnit.ConceptCard);
        Label_0098:
            base.StartCoroutine(this.LoadAllUnitImage());
            this.mIsSetJobUniqueID = this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex].UniqueID;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0141;
            }
            num = 0;
            goto Label_012E;
        Label_00D9:
            if ((this.mCurrentUnit.Jobs[num].JobID == str) == null)
            {
                goto Label_012A;
            }
            this.mCurrentUnit.SetJobIndex(num);
            this.mSelectedJobIndex = num;
            GlobalVars.SelectedJobUniqueID.Set(this.mCurrentUnit.Jobs[num].UniqueID);
            goto Label_0141;
        Label_012A:
            num += 1;
        Label_012E:
            if (num < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_00D9;
            }
        Label_0141:
            return;
        }

        public void Refresh(long uniqueID, long jobUniqueID, bool immediate, bool is_job_icon_hide)
        {
            long num;
            if (this.mReloading == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = this.mCurrentUnitID;
            this.mCurrentUnitID = uniqueID;
            this.mCurrentUnit = null;
            if (this.mStartSelectUnitUniqueID >= 0L)
            {
                goto Label_0035;
            }
            this.mStartSelectUnitUniqueID = uniqueID;
        Label_0035:
            this.RefreshSortedUnits();
            GlobalVars.SelectedUnitUniqueID.Set(uniqueID);
            GlobalVars.PreBattleUnitUniqueID.Set(uniqueID);
            this.RebuildUnitData();
            this.RefreshUnitShiftButton();
            if (num == uniqueID)
            {
                goto Label_006A;
            }
            this.SetUpUnitVoice();
        Label_006A:
            this.mSelectedJobIndex = this.mCurrentUnit.JobIndex;
            this.CharaQuestButton.get_gameObject().SetActive(this.mCurrentUnit.IsOpenCharacterQuest());
            this.SkinButton.get_gameObject().SetActive(this.mCurrentUnit.IsSkinUnlocked());
            this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
            this.mCurrentJobUniqueID = this.mCurrentUnit.CurrentJob.UniqueID;
            GlobalVars.SelectedJobUniqueID.Set(this.mCurrentJobUniqueID);
            if ((this.Favorite != null) == null)
            {
                goto Label_011E;
            }
            this.Favorite.set_isOn(this.mCurrentUnit.IsFavorite);
        Label_011E:
            this.CheckPlayBackUnlock();
            this.mReloading = 1;
            base.StartCoroutine(this.RefreshAsync(immediate, is_job_icon_hide));
            return;
        }

        private void RefreshAbilityList()
        {
            if (this.mActiveTabIndex == 2)
            {
                goto Label_0014;
            }
            this.mAbilityListDirty = 1;
            return;
        Label_0014:
            this.mAbilityListDirty = 0;
            if ((this.mAbilityListPanel == null) != null)
            {
                goto Label_0042;
            }
            if ((this.mAbilityListPanel.AbilityList == null) == null)
            {
                goto Label_0043;
            }
        Label_0042:
            return;
        Label_0043:
            this.mAbilityListPanel.AbilityList.Unit = this.mCurrentUnit;
            this.mAbilityListPanel.AbilityList.DisplayAll();
            return;
        }

        private void RefreshAbilitySlotButtonState()
        {
            if ((this.Tab_AbilitySlot != null) == null)
            {
                goto Label_002F;
            }
            this.Tab_AbilitySlot.set_interactable(this.mCurrentUnit.CurrentJob.Rank > 0);
        Label_002F:
            return;
        }

        private void RefreshAbilitySlots(bool resetScrollPos)
        {
            if (this.mActiveTabIndex == 3)
            {
                goto Label_0014;
            }
            this.mAbilitySlotDirty = 1;
            return;
        Label_0014:
            this.mAbilitySlotDirty = 0;
            if ((this.mAbilitySlotPanel == null) != null)
            {
                goto Label_0042;
            }
            if ((this.mAbilitySlotPanel.AbilitySlots == null) == null)
            {
                goto Label_0043;
            }
        Label_0042:
            return;
        Label_0043:
            this.mAbilitySlotPanel.AbilitySlots.Unit = this.mCurrentUnit;
            this.mAbilitySlotPanel.AbilitySlots.DisplaySlots();
            if (resetScrollPos == null)
            {
                goto Label_007F;
            }
            this.mAbilitySlotPanel.AbilitySlots.ResetScrollPos();
        Label_007F:
            return;
        }

        public void RefreshArtifactSlot(GenericSlot slot, List<ArtifactData> artifacts_sort_list, int index)
        {
            JobData data;
            ArtifactData data2;
            data = this.mCurrentUnit.CurrentJob;
            if ((this.mEquipmentPanel != null) == null)
            {
                goto Label_008D;
            }
            if ((slot != null) == null)
            {
                goto Label_008D;
            }
            if (this.mCurrentUnit.CurrentJob.Rank <= 0)
            {
                goto Label_007F;
            }
            data2 = null;
            if (artifacts_sort_list.Count <= index)
            {
                goto Label_005A;
            }
            data2 = artifacts_sort_list[index];
            goto Label_005C;
        Label_005A:
            data2 = null;
        Label_005C:
            slot.SetLocked(this.CheckEquipArtifactSlot(index, data, this.mCurrentUnit) == 0);
            slot.SetSlotData<ArtifactData>(data2);
            goto Label_008D;
        Label_007F:
            slot.SetLocked(1);
            slot.SetSlotData<ArtifactData>(null);
        Label_008D:
            return;
        }

        public void RefreshArtifactSlots()
        {
            long[] numArray;
            int num;
            JobData data;
            int num2;
            PlayerData data2;
            List<ArtifactData> list;
            int num3;
            ArtifactData data3;
            Dictionary<int, List<ArtifactData>> dictionary;
            int num4;
            int num5;
            List<int> list2;
            int num6;
            numArray = new long[3];
            num = 0;
            data = this.mCurrentUnit.CurrentJob;
            num2 = 0;
            goto Label_003C;
        Label_001C:
            if (data.Artifacts[num2] == null)
            {
                goto Label_0038;
            }
            numArray[num] = data.Artifacts[num2];
            num += 1;
        Label_0038:
            num2 += 1;
        Label_003C:
            if (num2 < ((int) data.Artifacts.Length))
            {
                goto Label_001C;
            }
            data2 = MonoSingleton<GameManager>.Instance.Player;
            list = new List<ArtifactData>();
            num3 = 0;
            goto Label_009B;
        Label_0065:
            if (numArray[num3] != null)
            {
                goto Label_0073;
            }
            goto Label_0095;
        Label_0073:
            data3 = data2.FindArtifactByUniqueID(numArray[num3]);
            if (data3 != null)
            {
                goto Label_008C;
            }
            goto Label_0095;
        Label_008C:
            list.Add(data3);
        Label_0095:
            num3 += 1;
        Label_009B:
            if (num3 < ((int) numArray.Length))
            {
                goto Label_0065;
            }
            dictionary = new Dictionary<int, List<ArtifactData>>();
            num4 = 0;
            goto Label_0102;
        Label_00B4:
            num5 = list[num4].ArtifactParam.type;
            if (dictionary.ContainsKey(num5) != null)
            {
                goto Label_00E5;
            }
            dictionary[num5] = new List<ArtifactData>();
        Label_00E5:
            dictionary[num5].Add(list[num4]);
            num4 += 1;
        Label_0102:
            if (num4 < list.Count)
            {
                goto Label_00B4;
            }
            list.Clear();
            list2 = new List<int>(dictionary.Keys);
            if (<>f__am$cacheED != null)
            {
                goto Label_013F;
            }
            <>f__am$cacheED = new Comparison<int>(UnitEnhanceV3.<RefreshArtifactSlots>m__453);
        Label_013F:
            list2.Sort(<>f__am$cacheED);
            num6 = 0;
            goto Label_016E;
        Label_0151:
            list.AddRange(dictionary[list2[num6]]);
            num6 += 1;
        Label_016E:
            if (num6 < list2.Count)
            {
                goto Label_0151;
            }
            this.RefreshArtifactSlot(this.mEquipmentPanel.ArtifactSlot, list, 0);
            this.RefreshArtifactSlot(this.mEquipmentPanel.ArtifactSlot2, list, 1);
            this.RefreshArtifactSlot(this.mEquipmentPanel.ArtifactSlot3, list, 2);
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshAsync(bool immediate, bool is_job_icon_hide)
        {
            <RefreshAsync>c__Iterator152 iterator;
            iterator = new <RefreshAsync>c__Iterator152();
            iterator.immediate = immediate;
            iterator.is_job_icon_hide = is_job_icon_hide;
            iterator.<$>immediate = immediate;
            iterator.<$>is_job_icon_hide = is_job_icon_hide;
            iterator.<>f__this = this;
            return iterator;
        }

        private unsafe void RefreshBasicParameters(bool bDisableJobMaster)
        {
            int num;
            int num2;
            int num3;
            BaseStatus status;
            int num4;
            BaseStatus status2;
            int num5;
            int num6;
            int num7;
            int num8;
            OInt num9;
            int num10;
            OInt num11;
            int num12;
            DataSource.Bind<UnitData>(this.UnitInfo, this.mCurrentUnit);
            DataSource.Bind<UnitData>(this.UnitParamInfo, this.mCurrentUnit);
            num = -1;
            num2 = 0;
            goto Label_0053;
        Label_002B:
            if (this.mCurrentUnit.Jobs[num2].UniqueID != this.mCurrentJobUniqueID)
            {
                goto Label_004F;
            }
            num = num2;
            goto Label_0066;
        Label_004F:
            num2 += 1;
        Label_0053:
            if (num2 < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_002B;
            }
        Label_0066:
            num3 = Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, this.mCurrentUnit.CurrentJob);
            if (num < 0)
            {
                goto Label_022A;
            }
            if (num3 < 0)
            {
                goto Label_022A;
            }
            if (num == num3)
            {
                goto Label_022A;
            }
            this.mCurrentUnit.SetJobIndex(num);
            status = new BaseStatus(this.mCurrentUnit.Status);
            if (bDisableJobMaster == null)
            {
                goto Label_00E5;
            }
            this.mCurrentUnit.CalcStatus(this.mCurrentUnit.Lv, num, &status, num3);
            status.CopyTo(this.mCurrentUnit.Status);
        Label_00E5:
            num4 = this.mCurrentUnit.GetCombination();
            this.mCurrentUnit.SetJobIndex(num3);
            status2 = new BaseStatus(this.mCurrentUnit.Status);
            if (bDisableJobMaster == null)
            {
                goto Label_0142;
            }
            this.mCurrentUnit.CalcStatus(this.mCurrentUnit.Lv, num3, &status2, num3);
            status2.CopyTo(this.mCurrentUnit.Status);
        Label_0142:
            num5 = this.mCurrentUnit.GetCombination();
            num6 = 0;
            goto Label_01D4;
        Label_0157:
            if ((this.mStatusParamSlots[num6] != null) == null)
            {
                goto Label_01CE;
            }
            this.SetParamColor(this.mStatusParamSlots[num6], status2.param[num6] - status.param[num6]);
            num9 = this.mCurrentUnit.Status.param[num6];
            this.mStatusParamSlots[num6].set_text(&num9.ToString());
        Label_01CE:
            num6 += 1;
        Label_01D4:
            if (num6 < StatusParam.MAX_STATUS)
            {
                goto Label_0157;
            }
            if ((this.Param_Renkei != null) == null)
            {
                goto Label_02D3;
            }
            num7 = num5 - num4;
            this.SetParamColor(this.Param_Renkei, num7);
            this.Param_Renkei.set_text(&this.mCurrentUnit.GetCombination().ToString());
            goto Label_02D3;
        Label_022A:
            num8 = 0;
            goto Label_028A;
        Label_0232:
            if ((this.mStatusParamSlots[num8] != null) == null)
            {
                goto Label_0284;
            }
            this.SetParamColor(this.mStatusParamSlots[num8], 0);
            num11 = this.mCurrentUnit.Status.param[num8];
            this.mStatusParamSlots[num8].set_text(&num11.ToString());
        Label_0284:
            num8 += 1;
        Label_028A:
            if (num8 < StatusParam.MAX_STATUS)
            {
                goto Label_0232;
            }
            if ((this.Param_Renkei != null) == null)
            {
                goto Label_02D3;
            }
            this.SetParamColor(this.Param_Renkei, 0);
            this.Param_Renkei.set_text(&this.mCurrentUnit.GetCombination().ToString());
        Label_02D3:
            GameParameter.UpdateAll(this.UnitInfo);
            GameParameter.UpdateAll(this.UnitParamInfo);
            if ((this.LeaderSkillInfo != null) == null)
            {
                goto Label_0310;
            }
            this.LeaderSkillInfo.SetSlotData<SkillData>(this.mCurrentUnit.LeaderSkill);
        Label_0310:
            return;
        }

        private void RefreshEquipments(int slot)
        {
            EquipData[] dataArray;
            UnitEquipmentSlotEvents[] eventsArray;
            GameManager manager;
            PlayerData data;
            int num;
            int num2;
            int num3;
            ItemParam param;
            UnitEquipmentSlotEvents.SlotStateTypes types;
            bool flag;
            bool flag2;
            JobData data2;
            ItemParam param2;
            ItemData data3;
            ItemData data4;
            NeedEquipItemList list;
            if (this.mActiveTabIndex == null)
            {
                goto Label_0013;
            }
            this.mEquipmentPanelDirty = 1;
            return;
        Label_0013:
            this.mEquipmentPanelDirty = 0;
            if (this.mCurrentUnit != null)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            dataArray = this.mCurrentUnit.GetRankupEquips(this.mCurrentUnit.JobIndex);
            eventsArray = this.mEquipmentPanel.EquipmentSlots;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            num = 0;
            num2 = 0;
            num3 = 0;
            goto Label_0287;
        Label_0064:
            if ((eventsArray[num3] == null) != null)
            {
                goto Label_0281;
            }
            if ((slot == -1) || (num3 == slot))
            {
                goto Label_0087;
            }
            goto Label_0281;
        Label_0087:
            if (dataArray[num3].IsValid() != null)
            {
                goto Label_00A9;
            }
            eventsArray[num3].get_gameObject().SetActive(0);
            goto Label_0281;
        Label_00A9:
            eventsArray[num3].get_gameObject().SetActive(1);
            param = dataArray[num3].ItemParam;
            DataSource.Bind<ItemParam>(eventsArray[num3].get_gameObject(), param);
            DataSource.Bind<EquipData>(eventsArray[num3].get_gameObject(), dataArray[num3]);
            GameParameter.UpdateAll(eventsArray[num3].get_gameObject());
            flag = 1;
            flag2 = 1;
            if (this.mCurrentUnit.CurrentJob.Rank != null)
            {
                goto Label_017A;
            }
            param2 = manager.MasterParam.GetCommonEquip(param, 1);
            data3 = (param2 == null) ? null : manager.Player.FindItemDataByItemID(param2.iname);
            data4 = data.FindItemDataByItemID(param.iname);
            if (data4 == null)
            {
                goto Label_0166;
            }
            flag = num < data4.Num;
        Label_0166:
            if (data3 == null)
            {
                goto Label_017A;
            }
            flag2 = num2 < data3.Num;
        Label_017A:
            if (dataArray[num3].IsEquiped() == null)
            {
                goto Label_0190;
            }
            types = 3;
            goto Label_0276;
        Label_0190:
            if (data.HasItem(param.iname) == null)
            {
                goto Label_01D6;
            }
            if (flag == null)
            {
                goto Label_01D6;
            }
            num += 1;
            if (param.equipLv <= this.mCurrentUnit.Lv)
            {
                goto Label_01CE;
            }
            types = 2;
            goto Label_01D1;
        Label_01CE:
            types = 1;
        Label_01D1:
            goto Label_0276;
        Label_01D6:
            list = new NeedEquipItemList();
            if (data.CheckEnableCreateItem(param, 1, 1, list) == null)
            {
                goto Label_0215;
            }
            if (param.equipLv <= this.mCurrentUnit.Lv)
            {
                goto Label_020D;
            }
            types = 5;
            goto Label_0210;
        Label_020D:
            types = 4;
        Label_0210:
            goto Label_0276;
        Label_0215:
            if (list.IsEnoughCommon() == null)
            {
                goto Label_0273;
            }
            if (flag2 == null)
            {
                goto Label_0273;
            }
            if (param.equipLv <= this.mCurrentUnit.Lv)
            {
                goto Label_0248;
            }
            types = 9;
            goto Label_026E;
        Label_0248:
            if (this.mCurrentUnit.CurrentJob.Rank != null)
            {
                goto Label_026B;
            }
            types = 8;
            num2 += 1;
            goto Label_026E;
        Label_026B:
            types = 7;
        Label_026E:
            goto Label_0276;
        Label_0273:
            types = 0;
        Label_0276:
            eventsArray[num3].StateType = types;
        Label_0281:
            num3 += 1;
        Label_0287:
            if (num3 < ((int) eventsArray.Length))
            {
                goto Label_0064;
            }
            this.RefreshArtifactSlots();
            return;
        }

        private unsafe void RefreshEXPImmediate()
        {
            int num;
            int num2;
            int num3;
            float num4;
            int num5;
            if ((this.UnitEXPSlider == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = this.mCurrentUnit.Exp;
            num2 = this.mCurrentUnit.GetExp();
            num3 = num2 + this.mCurrentUnit.GetNextExp();
            this.mExpStart = this.mExpEnd = (float) num;
            this.RefreshLevelCap();
            if ((this.UnitExp != null) == null)
            {
                goto Label_0072;
            }
            this.UnitExp.set_text(&num.ToString());
        Label_0072:
            if ((this.UnitExpMax != null) == null)
            {
                goto Label_0095;
            }
            this.UnitExpMax.set_text(&num3.ToString());
        Label_0095:
            if ((this.UnitExpNext != null) == null)
            {
                goto Label_00C5;
            }
            this.UnitExpNext.set_text(&this.mCurrentUnit.GetNextExp().ToString());
        Label_00C5:
            if (num3 > 0)
            {
                goto Label_00D0;
            }
            num = 0;
            num3 = 1;
        Label_00D0:
            this.UnitEXPSlider.AnimateValue(Mathf.Clamp01(((float) num2) / ((float) num3)), 0f);
            return;
        }

        private void RefreshExpInfo()
        {
            if ((this.UnitExpInfo != null) == null)
            {
                goto Label_001C;
            }
            GameParameter.UpdateAll(this.UnitExpInfo);
        Label_001C:
            return;
        }

        private void RefreshExpItemsButtonState()
        {
            int num;
            GameObject obj2;
            ItemData data;
            Button button;
            if (this.mExpItemGameObjects == null)
            {
                goto Label_001C;
            }
            if (this.mExpItemGameObjects.Count >= 0)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            num = 0;
            goto Label_0085;
        Label_0024:
            obj2 = this.mExpItemGameObjects[num];
            if ((obj2 != null) == null)
            {
                goto Label_0081;
            }
            data = DataSource.FindDataOfClass<ItemData>(obj2, null);
            if (data != null)
            {
                goto Label_0050;
            }
            goto Label_0081;
        Label_0050:
            if (data.Num > 0)
            {
                goto Label_0081;
            }
            button = obj2.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0081;
            }
            if (button.get_interactable() == null)
            {
                goto Label_0081;
            }
            button.set_interactable(0);
        Label_0081:
            num += 1;
        Label_0085:
            if (num < this.mExpItemGameObjects.Count)
            {
                goto Label_0024;
            }
            return;
        }

        public void RefreshJobIcon(GameObject target, int job_index)
        {
            int num;
            int num2;
            bool flag;
            if (this.mCurrentUnit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = this.ClampJobIconIndex(job_index);
            flag = Array.FindIndex<JobData>(this.mCurrentUnit.Jobs, new Predicate<JobData>(this.<RefreshJobIcon>m__44F)) == num;
            target.GetComponent<UnitInventoryJobIcon>().SetParam(this.mCurrentUnit, this.mJobSetDatas[num][0], this.mJobSetDatas[num][1], flag, 1);
            this.UpdateJobSlotStates(1);
            return;
        }

        private unsafe void RefreshJobIcons(bool is_hide)
        {
            int[] numArray3;
            int[] numArray1;
            int num;
            int num2;
            int num3;
            int num4;
            int[] numArray;
            int num5;
            JobSetParam param;
            int[] numArray2;
            int num6;
            int num7;
            int num8;
            int num9;
            List<GameObject> list;
            int num10;
            <RefreshJobIcons>c__AnonStorey3C1 storeyc;
            <RefreshJobIcons>c__AnonStorey3BF storeybf;
            <RefreshJobIcons>c__AnonStorey3C0 storeyc2;
            <RefreshJobIcons>c__AnonStorey3C2 storeyc3;
            storeyc = new <RefreshJobIcons>c__AnonStorey3C1();
            storeyc.<>f__this = this;
            this.mJobIconScrollListController.m_ScrollMode = 1;
            if (this.mCurrentUnit != null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            num = 0;
            goto Label_004D;
        Label_002E:
            this.mJobIconScrollListController.Items[num].job_icon.ResetParam();
            num += 1;
        Label_004D:
            if (num < this.mJobIconScrollListController.Items.Count)
            {
                goto Label_002E;
            }
            storeyc.cc_jobset_array = MonoSingleton<GameManager>.Instance.GetClassChangeJobSetParam(this.mCurrentUnit.UnitID);
            if (storeyc.cc_jobset_array != null)
            {
                goto Label_0098;
            }
            storeyc.cc_jobset_array = new JobSetParam[0];
        Label_0098:
            this.mJobSetDatas.Clear();
            num2 = 0;
            storeybf = new <RefreshJobIcons>c__AnonStorey3BF();
            storeybf.<>f__this = this;
            storeybf.i = 0;
            goto Label_024A;
        Label_00C1:
            num3 = Array.FindIndex<JobSetParam>(storeyc.cc_jobset_array, new Predicate<JobSetParam>(storeybf.<>m__450));
            if (num3 < 0)
            {
                goto Label_015F;
            }
            storeyc2 = new <RefreshJobIcons>c__AnonStorey3C0();
            storeyc2.base_jobset_param = MonoSingleton<GameManager>.Instance.GetJobSetParam(storeyc.cc_jobset_array[num3].jobchange);
            if (Array.FindIndex<JobData>(this.mCurrentUnit.Jobs, new Predicate<JobData>(storeyc2.<>m__451)) < 0)
            {
                goto Label_0132;
            }
            goto Label_023A;
        Label_0132:
            numArray1 = new int[] { storeybf.i, -1 };
            numArray = numArray1;
            this.mJobSetDatas.Add(num2, numArray);
            num2 += 1;
            goto Label_023A;
        Label_015F:
            num5 = -1;
            storeyc3 = new <RefreshJobIcons>c__AnonStorey3C2();
            storeyc3.<>f__ref$961 = storeyc;
            storeyc3.j = 0;
            goto Label_01FC;
        Label_017F:
            if ((MonoSingleton<GameManager>.Instance.GetJobSetParam(storeyc.cc_jobset_array[storeyc3.j].jobchange).job == this.mCurrentUnit.Jobs[storeybf.i].JobID) == null)
            {
                goto Label_01EC;
            }
            num5 = Array.FindIndex<JobData>(this.mCurrentUnit.Jobs, new Predicate<JobData>(storeyc3.<>m__452));
            goto Label_0211;
        Label_01EC:
            storeyc3.j += 1;
        Label_01FC:
            if (storeyc3.j < ((int) storeyc.cc_jobset_array.Length))
            {
                goto Label_017F;
            }
        Label_0211:
            numArray3 = new int[] { storeybf.i, num5 };
            numArray2 = numArray3;
            this.mJobSetDatas.Add(num2, numArray2);
            num2 += 1;
        Label_023A:
            storeybf.i += 1;
        Label_024A:
            if (storeybf.i < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_00C1;
            }
            num6 = 0;
            goto Label_0293;
        Label_026B:
            this.mJobIconScrollListController.Items[num6].job_icon.get_gameObject().SetActive(0);
            num6 += 1;
        Label_0293:
            if (num6 < this.mJobIconScrollListController.Items.Count)
            {
                goto Label_026B;
            }
            if (this.mJobSetDatas.Count > 2)
            {
                goto Label_0326;
            }
            num7 = 0;
            goto Label_030F;
        Label_02C3:
            this.mJobIconScrollListController.Items[num7].job_icon.get_gameObject().SetActive(1);
            this.RefreshJobIcon(this.mJobIconScrollListController.Items[num7].job_icon.get_gameObject(), num7);
            num7 += 1;
        Label_030F:
            if (num7 < this.mJobSetDatas.Count)
            {
                goto Label_02C3;
            }
            goto Label_03B4;
        Label_0326:
            num8 = 0;
            goto Label_039D;
        Label_032E:
            this.mJobIconScrollListController.Items[num8].job_icon.get_gameObject().SetActive(1);
            num9 = int.Parse(this.mJobIconScrollListController.Items[num8].job_icon.get_name());
            this.RefreshJobIcon(this.mJobIconScrollListController.Items[num8].job_icon.get_gameObject(), num9);
            num8 += 1;
        Label_039D:
            if (num8 < this.mJobIconScrollListController.Items.Count)
            {
                goto Label_032E;
            }
        Label_03B4:
            this.mJobIconScrollListController.Repotision();
            list = new List<GameObject>();
            num10 = 0;
            goto Label_04B1;
        Label_03CE:
            if ((this.mJobIconScrollListController.Items[num10].job_icon.BaseJobIconButton.get_name() == &this.mSelectedJobIndex.ToString()) == null)
            {
                goto Label_0427;
            }
            list.Add(this.mJobIconScrollListController.Items[num10].job_icon.get_gameObject());
        Label_0427:
            if ((this.mJobIconScrollListController.Items[num10].job_icon.CcJobButton.get_gameObject().get_activeSelf() == null) || ((this.mJobIconScrollListController.Items[num10].job_icon.CcJobButton.get_name() == &this.mSelectedJobIndex.ToString()) == null))
            {
                goto Label_04AB;
            }
            list.Add(this.mJobIconScrollListController.Items[num10].job_icon.get_gameObject());
        Label_04AB:
            num10 += 1;
        Label_04B1:
            if (num10 < this.mJobIconScrollListController.Items.Count)
            {
                goto Label_03CE;
            }
            this.ScrollClampedJobIcons.Focus(list, 1, is_hide, 0f);
            this.mJobIconScrollListController.Step();
            this.mJobIconScrollListController.m_ScrollMode = (this.mJobSetDatas.Count > 2) ? 1 : 0;
            this.UpdateJobSlotStates(1);
            return;
        }

        private void RefreshJobInfo()
        {
            if ((this.JobInfo == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            DataSource.Bind<UnitData>(this.JobInfo, this.mCurrentUnit);
            GameParameter.UpdateAll(this.JobInfo);
            return;
        }

        private unsafe void RefreshLevelCap()
        {
            int num;
            int num2;
            int num3;
            num = MonoSingleton<GameManager>.Instance.Player.Lv;
            num2 = this.mCurrentUnit.GetLevelCap(0);
            if ((this.UnitLevel != null) == null)
            {
                goto Label_008F;
            }
            this.UnitLevel.set_text(&this.mCurrentUnit.Lv.ToString());
        Label_0074:
            this.UnitLevel.set_color(((this.mCurrentUnit.Lv < num2) && (this.mCurrentUnit.Lv < num)) ? this.UnitLevelColor : this.CappedUnitLevelColor);
        Label_008F:
            if ((this.UnitLevelCapInfo != null) == null)
            {
                goto Label_00BC;
            }
            this.UnitLevelCapInfo.SetActive((this.mCurrentUnit.Lv < num) == 0);
        Label_00BC:
            return;
        }

        public void RefreshReturningJobState(long jobUniqueID)
        {
            int num;
            long num2;
            if (jobUniqueID == null)
            {
                goto Label_0064;
            }
            if (this.mIsSetJobUniqueID == null)
            {
                goto Label_0064;
            }
            num = 0;
            goto Label_0051;
        Label_0018:
            if (this.mCurrentUnit.Jobs[num].UniqueID != jobUniqueID)
            {
                goto Label_004D;
            }
            if (this.mIsSetJobUniqueID == jobUniqueID)
            {
                goto Label_004D;
            }
            this.ChangeJobSlot(num, null);
            this.RefreshJobIcons(0);
        Label_004D:
            num += 1;
        Label_0051:
            if (num < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_0018;
            }
        Label_0064:
            this.mSelectedJobIndex = this.mCurrentUnit.JobIndex;
            return;
        }

        private void RefreshSortedUnits()
        {
            PlayerData data;
            int num;
            UnitData data2;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (this.SortedUnits != null)
            {
                goto Label_0026;
            }
            this.SortedUnits = new List<UnitData>();
            goto Label_0031;
        Label_0026:
            this.SortedUnits.Clear();
        Label_0031:
            num = 0;
            goto Label_0061;
        Label_0038:
            data2 = data.GetUnitData(this.m_UnitList[num]);
            if (data2 == null)
            {
                goto Label_005D;
            }
            this.SortedUnits.Add(data2);
        Label_005D:
            num += 1;
        Label_0061:
            if (num < this.m_UnitList.Count)
            {
                goto Label_0038;
            }
            return;
        }

        public unsafe void RefreshTobiraBgAnimation(TobiraData tobira, bool is_immediate)
        {
            string str;
            string str2;
            bool flag;
            string str3;
            bool flag2;
            AnimatorStateInfo info;
            if ((this.mBGTobiraAnimator == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            str = (is_immediate == null) ? "open" : "opened";
            str2 = (is_immediate == null) ? "close" : "closed";
            flag = (tobira == null) ? 1 : (tobira.IsUnlocked == 0);
            str3 = (flag == null) ? str : str2;
            flag2 = 0;
            info = this.mBGTobiraAnimator.GetCurrentAnimatorStateInfo(0);
            if (flag == null)
            {
                goto Label_009D;
            }
            flag2 = (&info.IsName("close") != null) ? 1 : &info.IsName("closed");
            goto Label_00BF;
        Label_009D:
            flag2 = (&info.IsName("open") != null) ? 1 : &info.IsName("opened");
        Label_00BF:
            if (flag2 != null)
            {
                goto Label_00D2;
            }
            this.mBGTobiraAnimator.Play(str3);
        Label_00D2:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshUnitImage()
        {
            <RefreshUnitImage>c__Iterator154 iterator;
            iterator = new <RefreshUnitImage>c__Iterator154();
            iterator.<>f__this = this;
            return iterator;
        }

        private void RefreshUnitShiftButton()
        {
            int num;
            bool flag;
            num = this.SortedUnits.Count;
            if (this.mCurrentUnit == null)
            {
                goto Label_0037;
            }
            if (this.SortedUnits.Find(new Predicate<UnitData>(this.<RefreshUnitShiftButton>m__454)) != null)
            {
                goto Label_0037;
            }
            num += 1;
        Label_0037:
            flag = (num < 2) == 0;
            this.PrevUnitButton.set_interactable(flag);
            this.NextUnitButton.set_interactable(flag);
            return;
        }

        private void ReloadPreviewModels()
        {
            Type[] typeArray2;
            Type[] typeArray1;
            int num;
            UnitPreview preview;
            GameObject obj2;
            GameObject obj3;
            if (this.mCurrentUnit == null)
            {
                goto Label_0020;
            }
            if ((UnitManagementWindow.Instance.UnitModelPreviewParent == null) == null)
            {
                goto Label_0021;
            }
        Label_0020:
            return;
        Label_0021:
            GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
            this.mPreviewControllers.Clear();
            this.mPreviewJobNames.Clear();
            this.mCurrentPreview = null;
            num = 0;
            goto Label_0113;
        Label_0050:
            preview = null;
            if (this.mCurrentUnit.Jobs[num] == null)
            {
                goto Label_00E6;
            }
            if (this.mCurrentUnit.Jobs[num].Param == null)
            {
                goto Label_00E6;
            }
            typeArray1 = new Type[] { typeof(UnitPreview) };
            obj2 = new GameObject("Preview", typeArray1);
            preview = obj2.GetComponent<UnitPreview>();
            preview.DefaultLayer = GameUtility.LayerHidden;
            preview.SetupUnit(this.mCurrentUnit, num);
            obj2.get_transform().SetParent(UnitManagementWindow.Instance.UnitModelPreviewParent, 0);
            if (num != this.mCurrentUnit.JobIndex)
            {
                goto Label_00E6;
            }
            this.mCurrentPreview = preview;
        Label_00E6:
            this.mPreviewControllers.Add(preview);
            this.mPreviewJobNames.Add(this.mCurrentUnit.Jobs[num].JobID);
            num += 1;
        Label_0113:
            if (num < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_0050;
            }
            if ((this.mCurrentPreview == null) == null)
            {
                goto Label_01AA;
            }
            typeArray2 = new Type[] { typeof(UnitPreview) };
            obj3 = new GameObject("Preview", typeArray2);
            this.mCurrentPreview = obj3.GetComponent<UnitPreview>();
            this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
            this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
            obj3.get_transform().SetParent(UnitManagementWindow.Instance.UnitModelPreviewParent, 0);
            this.mPreviewControllers.Add(this.mCurrentPreview);
        Label_01AA:
            return;
        }

        private void Req_UpdateEquippedAbility()
        {
            bool flag;
            int num;
            long num2;
            flag = 0;
            num = 0;
            goto Label_0033;
        Label_0009:
            if (this.mCurrentUnit.CurrentJob.AbilitySlots[num] == this.mOriginalAbilities[num])
            {
                goto Label_002F;
            }
            flag = 1;
            goto Label_004B;
        Label_002F:
            num += 1;
        Label_0033:
            if (num < ((int) this.mCurrentUnit.CurrentJob.AbilitySlots.Length))
            {
                goto Label_0009;
            }
        Label_004B:
            if (flag != null)
            {
                goto Label_0058;
            }
            this.FinishKyokaRequest();
            return;
        Label_0058:
            num2 = this.mCurrentUnit.CurrentJob.UniqueID;
            this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
            this.RequestAPI(new ReqJobAbility(num2, this.mCurrentUnit.CurrentJob.AbilitySlots, new Network.ResponseCallback(this.Res_UpdateEquippedAbility)));
            this.SetUnitDirty();
            return;
        }

        private void RequestAPI(WebAPI api)
        {
            if (this.mAppPausing == null)
            {
                goto Label_0017;
            }
            Network.RequestAPIImmediate(api, 1);
            goto Label_001E;
        Label_0017:
            Network.RequestAPI(api, 0);
        Label_001E:
            return;
        }

        private unsafe void Res_UpdateEquippedAbility(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0039;
            }
            switch ((Network.ErrCode - 0x960))
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_002D;

                case 2:
                    goto Label_002D;
            }
            goto Label_0033;
        Label_002D:
            FlowNode_Network.Failed();
            return;
        Label_0033:
            FlowNode_Network.Retry();
            return;
        Label_0039:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0046:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                this.mLastSyncTime = Time.get_realtimeSinceStartup();
                goto Label_0081;
            }
            catch (Exception exception1)
            {
            Label_006B:
                exception = exception1;
                Debug.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_008C;
            }
        Label_0081:
            Network.RemoveAPI();
            this.FinishKyokaRequest();
        Label_008C:
            return;
        }

        private void SetActivePreview(int index)
        {
            UnitPreview preview;
            preview = this.mPreviewControllers[index];
            if ((preview == this.mCurrentPreview) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            GameUtility.SetLayer(this.mCurrentPreview, GameUtility.LayerHidden, 1);
            GameUtility.SetLayer(preview, GameUtility.LayerCH, 1);
            this.mCurrentPreview = preview;
            return;
        }

        private void SetActivePreview(string jobID)
        {
            int num;
            num = this.mPreviewJobNames.IndexOf(jobID);
            if (num >= 0)
            {
                goto Label_0016;
            }
            num = 0;
        Label_0016:
            this.SetActivePreview(num);
            return;
        }

        private void SetCacheUnitData(UnitData original, int selectedJobIndex)
        {
            this.mCacheUnitData = new UnitData();
            this.mCacheUnitData.Setup(original);
            this.mCacheUnitData.SetJobIndex(selectedJobIndex);
            return;
        }

        private string[] SetEquipArtifactFilters(ArtifactData data, ArtifactTypes select_slot_artifact_type)
        {
            List<string> list;
            int num;
            JobData data2;
            ArtifactData[] dataArray;
            int num2;
            int num3;
            ArtifactData data3;
            list = new List<string>();
            num = 0;
            goto Label_0027;
        Label_000D:
            list.Add("RARE:" + ((int) num));
            num += 1;
        Label_0027:
            if (num < RarityParam.MAX_RARITY)
            {
                goto Label_000D;
            }
            data2 = this.mCurrentUnit.CurrentJob;
            dataArray = data2.ArtifactDatas;
            if (dataArray == null)
            {
                goto Label_00B3;
            }
            num2 = 0;
            goto Label_00A9;
        Label_0053:
            if (dataArray[num2] != null)
            {
                goto Label_0061;
            }
            goto Label_00A3;
        Label_0061:
            if (data == null)
            {
                goto Label_0085;
            }
            if (dataArray[num2].UniqueID == data.UniqueID)
            {
                goto Label_00A3;
            }
        Label_0085:
            list.Add("SAME:" + dataArray[num2].ArtifactParam.iname);
        Label_00A3:
            num2 += 1;
        Label_00A9:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_0053;
            }
        Label_00B3:
            num3 = 0;
            goto Label_012E;
        Label_00BB:
            data3 = data2.ArtifactDatas[num3];
            if (data3 == null)
            {
                goto Label_00DF;
            }
            if (data3.ArtifactParam.type != 3)
            {
                goto Label_00FD;
            }
        Label_00DF:
            list.Add("TYPE:" + ((int) (num3 + 1)));
            goto Label_0128;
        Label_00FD:
            if (data3.ArtifactParam.type != select_slot_artifact_type)
            {
                goto Label_0128;
            }
            list.Add("TYPE:" + ((int) (num3 + 1)));
        Label_0128:
            num3 += 1;
        Label_012E:
            if (num3 < ((int) data2.ArtifactDatas.Length))
            {
                goto Label_00BB;
            }
            return list.ToArray();
        }

        public void SetIsCommon(bool is_common)
        {
            this.mIsCommon = is_common;
            return;
        }

        private void SetNotifyReleaseClassChangeJob()
        {
            UnitData data;
            int num;
            bool flag;
            bool flag2;
            <SetNotifyReleaseClassChangeJob>c__AnonStorey3C3 storeyc;
            <SetNotifyReleaseClassChangeJob>c__AnonStorey3C4 storeyc2;
            storeyc = new <SetNotifyReleaseClassChangeJob>c__AnonStorey3C3();
            if (this.mCacheUnitData != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCacheUnitData.UniqueID);
            if (data != null)
            {
                goto Label_0035;
            }
            return;
        Label_0035:
            storeyc.cc_jobset_array = MonoSingleton<GameManager>.Instance.GetClassChangeJobSetParam(data.UnitID);
            if ((storeyc.cc_jobset_array != null) && (((int) storeyc.cc_jobset_array.Length) > 0))
            {
                goto Label_0068;
            }
            return;
        Label_0068:
            storeyc2 = new <SetNotifyReleaseClassChangeJob>c__AnonStorey3C4();
            storeyc2.<>f__ref$963 = storeyc;
            storeyc2.i = 0;
            goto Label_0153;
        Label_0085:
            num = Array.FindIndex<JobData>(data.Jobs, new Predicate<JobData>(storeyc2.<>m__455));
            if (num >= 0)
            {
                goto Label_00AA;
            }
            goto Label_0143;
        Label_00AA:
            flag = (data.Jobs[num].Rank != null) ? 0 : ((data.CheckJobUnlock(num, 0) != null) ? 1 : data.CheckJobRankUpAllEquip(num, 1));
            flag2 = (this.mCacheUnitData.Jobs[num].Rank != null) ? 0 : ((this.mCacheUnitData.CheckJobUnlock(num, 0) != null) ? 1 : this.mCacheUnitData.CheckJobRankUpAllEquip(num, 1));
            if (flag == null)
            {
                goto Label_0143;
            }
            if (flag == flag2)
            {
                goto Label_0143;
            }
            NotifyList.Push(string.Format(LocalizedText.Get("sys.UNITLIST_NOTIFY_UNLOCK_CC_JOB"), data.Jobs[num].Name));
        Label_0143:
            storeyc2.i += 1;
        Label_0153:
            if (storeyc2.i < ((int) storeyc.cc_jobset_array.Length))
            {
                goto Label_0085;
            }
            return;
        }

        private void SetParamColor(Graphic g, int delta)
        {
            if (delta >= 0)
            {
                goto Label_001D;
            }
            g.set_color(this.ParamDownColor);
            goto Label_004B;
        Label_001D:
            if (delta <= 0)
            {
                goto Label_003A;
            }
            g.set_color(this.ParamUpColor);
            goto Label_004B;
        Label_003A:
            g.set_color(this.DefaultParamColor);
        Label_004B:
            return;
        }

        private void SetPreviewVisible(bool visible)
        {
            if ((this.mCurrentPreview != null) == null)
            {
                goto Label_0084;
            }
            this.mDesiredPreviewVisibility = visible;
            if (visible != null)
            {
                goto Label_0034;
            }
            GameUtility.SetLayer(this.mCurrentPreview, GameUtility.LayerHidden, 1);
            goto Label_003B;
        Label_0034:
            this.mUpdatePreviewVisibility = 1;
        Label_003B:
            if ((UnitManagementWindow.Instance.UnitModelPreviewBase != null) == null)
            {
                goto Label_0084;
            }
            if (UnitManagementWindow.Instance.UnitModelPreviewBase.get_gameObject().get_activeSelf() != null)
            {
                goto Label_0084;
            }
            if (visible == null)
            {
                goto Label_0084;
            }
            UnitManagementWindow.Instance.UnitModelPreviewBase.get_gameObject().SetActive(1);
        Label_0084:
            return;
        }

        private void SetUnitDirty()
        {
            if (this.mDirtyUnits.Contains(this.mCurrentUnitID) != null)
            {
                goto Label_0027;
            }
            this.mDirtyUnits.Add(this.mCurrentUnitID);
        Label_0027:
            this.mLeftTime = Time.get_realtimeSinceStartup();
            return;
        }

        private unsafe void SetUnitImageAlpha(float alpha)
        {
            Color color;
            if ((this.mBGUnitImage == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            color = this.mBGUnitImage.get_color();
            &color.a = alpha;
            this.mBGUnitImage.set_color(color);
            return;
        }

        private void SetUpUnitVoice()
        {
            string str;
            string str2;
            string str3;
            if (this.mUnitVoice == null)
            {
                goto Label_002D;
            }
            this.mUnitVoice.StopAll(1f);
            this.mUnitVoice.Cleanup();
            this.mUnitVoice = null;
        Label_002D:
            str = this.mCurrentUnit.GetUnitSkinVoiceSheetName(-1);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0050;
            }
            DebugUtility.LogError("UnitDataにボイス設定が存在しません");
            return;
        Label_0050:
            str2 = "VO_" + str;
            str3 = this.mCurrentUnit.GetUnitSkinVoiceCueName(-1) + "_";
            this.mUnitVoice = new MySound.Voice(str2, str, str3, 0);
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShiftUnitAsync(long unitUniqueID)
        {
            <ShiftUnitAsync>c__Iterator15F iteratorf;
            iteratorf = new <ShiftUnitAsync>c__Iterator15F();
            iteratorf.unitUniqueID = unitUniqueID;
            iteratorf.<$>unitUniqueID = unitUniqueID;
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        private void ShowArtifactSetResult()
        {
            this.SpawnStatusChangeEffects();
            this.RebuildUnitData();
            this.ReloadPreviewModels();
            this.SetPreviewVisible(1);
            this.RefreshArtifactSlots();
            this.RefreshAbilitySlots(0);
            this.RefreshBasicParameters(0);
            return;
        }

        private unsafe void ShowLockEquipArtifactTooltip(GenericSlot slot)
        {
            object[] objArray1;
            int num;
            int num2;
            bool flag;
            int num3;
            FixParam param;
            if ((slot == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            num = 0;
            num2 = 0;
            goto Label_0042;
        Label_0016:
            if (this.mCurrentUnit.Jobs[num2].UniqueID != GlobalVars.SelectedJobUniqueID)
            {
                goto Label_003E;
            }
            num = num2;
            goto Label_0055;
        Label_003E:
            num2 += 1;
        Label_0042:
            if (num2 < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_0016;
            }
        Label_0055:
            flag = this.mCurrentUnit.Jobs[num].Rank > 0;
            if (flag == null)
            {
                goto Label_0088;
            }
            if ((slot == this.mEquipmentPanel.ArtifactSlot) == null)
            {
                goto Label_0088;
            }
            return;
        Label_0088:
            num3 = 0;
            if ((slot == this.mEquipmentPanel.ArtifactSlot2) == null)
            {
                goto Label_00A7;
            }
            num3 = 1;
            goto Label_00BF;
        Label_00A7:
            if ((slot == this.mEquipmentPanel.ArtifactSlot3) == null)
            {
                goto Label_00BF;
            }
            num3 = 2;
        Label_00BF:
            if ((this.Prefab_LockedArtifactSlotTooltip == null) == null)
            {
                goto Label_00D1;
            }
            return;
        Label_00D1:
            if ((this.mEquipArtifactUnlockTooltip != null) == null)
            {
                goto Label_00F5;
            }
            this.mEquipArtifactUnlockTooltip.Close();
            this.mEquipArtifactUnlockTooltip = null;
            return;
        Label_00F5:
            Tooltip.SetTooltipPosition(this.mEquipmentPanel.ArtifactSlot2.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
            if ((this.mEquipArtifactUnlockTooltip == null) == null)
            {
                goto Label_0145;
            }
            this.mEquipArtifactUnlockTooltip = Object.Instantiate<Tooltip>(this.Prefab_LockedArtifactSlotTooltip);
            goto Label_0150;
        Label_0145:
            this.mEquipArtifactUnlockTooltip.ResetPosition();
        Label_0150:
            if ((this.mEquipArtifactUnlockTooltip.TooltipText != null) == null)
            {
                goto Label_01F3;
            }
            if (flag == null)
            {
                goto Label_01D9;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (param == null)
            {
                goto Label_01F3;
            }
            if (param.EquipArtifactSlotUnlock == null)
            {
                goto Label_01F3;
            }
            if (((int) param.EquipArtifactSlotUnlock.Length) <= 0)
            {
                goto Label_01F3;
            }
            objArray1 = new object[] { &(param.EquipArtifactSlotUnlock[num3]).ToString() };
            this.mEquipArtifactUnlockTooltip.TooltipText.set_text(LocalizedText.Get("sys.EQUIP_ARTIFACT_SLOT_TOOLTIP", objArray1));
            goto Label_01F3;
        Label_01D9:
            this.mEquipArtifactUnlockTooltip.TooltipText.set_text(LocalizedText.Get("sys.TOOLTIP_ARIFACT_UNLOCK"));
        Label_01F3:
            return;
        }

        private unsafe void ShowParamTooltip(PointerEventData eventData)
        {
            GameObject obj2;
            RectTransform transform;
            RaycastResult result;
            if ((this.Prefab_ParamTooltip == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            obj2 = &eventData.get_pointerCurrentRaycast().get_gameObject();
            if ((obj2 == null) != null)
            {
                goto Label_003E;
            }
            if ((obj2 == this.mParamTooltipTarget) == null)
            {
                goto Label_003F;
            }
        Label_003E:
            return;
        Label_003F:
            this.mParamTooltipTarget = obj2;
            Tooltip.SetTooltipPosition(obj2.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f));
            if ((this.mParamTooltip == null) == null)
            {
                goto Label_0089;
            }
            this.mParamTooltip = Object.Instantiate<Tooltip>(this.Prefab_ParamTooltip);
            goto Label_0094;
        Label_0089:
            this.mParamTooltip.ResetPosition();
        Label_0094:
            if ((this.mParamTooltip.TooltipText != null) == null)
            {
                goto Label_00D4;
            }
            this.mParamTooltip.TooltipText.set_text(LocalizedText.Get("sys.UE_HELP_" + obj2.get_name().ToUpper()));
        Label_00D4:
            return;
        }

        private void ShowSkinParamChange(ArtifactParam artifact, bool isAll)
        {
            int num;
            if (isAll == null)
            {
                goto Label_0048;
            }
            num = 0;
            goto Label_0030;
        Label_000D:
            this.mCurrentUnit.SetJobSkin((artifact == null) ? null : artifact.iname, num, 1);
            num += 1;
        Label_0030:
            if (num < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_000D;
            }
            goto Label_0071;
        Label_0048:
            this.mCurrentUnit.SetJobSkin((artifact == null) ? null : artifact.iname, this.mCurrentUnit.JobIndex, 1);
        Label_0071:
            this.ShowSkinSetResult();
            this.FadeUnitImage(0f, 0f, 0f);
            base.StartCoroutine(this.RefreshUnitImage());
            this.FadeUnitImage(0f, 1f, 1f);
            return;
        }

        private void ShowSkinSetResult()
        {
            this.ReloadPreviewModels();
            this.SetPreviewVisible(1);
            this.SetUpUnitVoice();
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowUnlockSkillEffect()
        {
            <ShowUnlockSkillEffect>c__Iterator162 iterator;
            iterator = new <ShowUnlockSkillEffect>c__Iterator162();
            iterator.<>f__this = this;
            return iterator;
        }

        private void SpawnAddExpEffect(int delta, ItemParam item)
        {
        }

        private void SpawnEquipEffect(int slot)
        {
            if ((this.EquipSlotEffect == null) != null)
            {
                goto Label_0043;
            }
            if (slot < 0)
            {
                goto Label_0043;
            }
            if (slot >= ((int) this.mEquipmentPanel.EquipmentSlots.Length))
            {
                goto Label_0043;
            }
            if ((this.mEquipmentPanel.EquipmentSlots[slot] == null) == null)
            {
                goto Label_0044;
            }
        Label_0043:
            return;
        Label_0044:
            UIUtility.SpawnParticle(this.EquipSlotEffect, this.mEquipmentPanel.EquipmentSlots[slot].get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
            return;
        }

        private void SpawnJobChangeButtonEffect()
        {
            if ((this.JobChangeButton == null) != null)
            {
                goto Label_0022;
            }
            if ((this.JobChangeButton == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            UIUtility.SpawnParticle(this.JobChangeButtonEffect, this.JobChangeButton.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
            return;
        }

        private void SpawnParamDeltaEffect(GameObject go, int delta)
        {
            GameObject obj2;
            StringBuilder builder;
            GameObject obj3;
            Text text;
            if (delta != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            obj2 = null;
            builder = GameUtility.GetStringBuilder();
            if (delta <= 0)
            {
                goto Label_002B;
            }
            obj2 = this.ParamUpEffect;
            builder.Append(0x2b);
            goto Label_0039;
        Label_002B:
            if (delta >= 0)
            {
                goto Label_0039;
            }
            obj2 = this.ParamDownEffect;
        Label_0039:
            if ((obj2 == null) == null)
            {
                goto Label_0046;
            }
            return;
        Label_0046:
            obj3 = Object.Instantiate<GameObject>(obj2);
            obj3.get_transform().SetParent(go.get_transform(), 0);
            (obj3.get_transform() as RectTransform).set_anchoredPosition(Vector2.get_zero());
            builder.Append(delta);
            text = obj3.GetComponentInChildren<Text>();
            if ((text != null) == null)
            {
                goto Label_009B;
            }
            text.set_text(builder.ToString());
        Label_009B:
            SRPG_Extensions.RequireComponent<DestructTimer>(obj3);
            return;
        }

        public void SpawnStatusChangeEffects()
        {
            BaseStatus status;
            int num;
            int num2;
            status = this.mCurrentUnit.Status;
            num2 = 0;
            goto Label_0077;
        Label_0013:
            if ((this.mStatusParamSlots[num2] == null) == null)
            {
                goto Label_002B;
            }
            goto Label_0073;
        Label_002B:
            num = status.param[num2] - this.mCurrentStatus.param[num2];
            if (num != null)
            {
                goto Label_005F;
            }
            goto Label_0073;
        Label_005F:
            this.SpawnParamDeltaEffect(this.mStatusParamSlots[num2].get_gameObject(), num);
        Label_0073:
            num2 += 1;
        Label_0077:
            if (num2 < StatusParam.MAX_STATUS)
            {
                goto Label_0013;
            }
            if ((this.Param_Renkei != null) == null)
            {
                goto Label_00B8;
            }
            num = this.mCurrentUnit.GetCombination() - this.mCurrentRenkei;
            this.SpawnParamDeltaEffect(this.Param_Renkei.get_gameObject(), num);
        Label_00B8:
            return;
        }

        [DebuggerHidden]
        private IEnumerator Start()
        {
            <Start>c__Iterator14A iteratora;
            iteratora = new <Start>c__Iterator14A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        private void StartJobRankUp(int target_rank, bool can_jobmaster, bool can_jobmax)
        {
            base.GetComponent<WindowController>().SetCollision(0);
            CriticalSection.Enter(2);
            this.SetCacheUnitData(this.mCurrentUnit, this.mSelectedJobIndex);
            base.StartCoroutine(this.PostJobRankUp(target_rank, can_jobmaster, can_jobmax));
            return;
        }

        private void StopUnitVoice()
        {
            string str;
            str = this.mCurrentUnit.GetUnitSkinVoiceSheetName(-1);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0023;
            }
            DebugUtility.LogError("UnitDataにボイス設定が存在しません");
            return;
        Label_0023:
            MySound.Voice.StopAll(str, 0f, 1);
            return;
        }

        private unsafe void SubmitAbilityRankUpRequest()
        {
            Dictionary<long, int> dictionary;
            int num;
            long num2;
            string str;
            string str2;
            Dictionary<long, int> dictionary2;
            long num3;
            int num4;
            this.mKeepWindowLocked = 0;
            dictionary = new Dictionary<long, int>();
            num = 0;
            goto Label_005A;
        Label_0014:
            num2 = this.mRankedUpAbilities[num];
            if (dictionary.ContainsKey(num2) == null)
            {
                goto Label_004E;
            }
            num4 = dictionary2[num3];
            (dictionary2 = dictionary)[num3 = num2] = num4 + 1;
            goto Label_0056;
        Label_004E:
            dictionary[num2] = 1;
        Label_0056:
            num += 1;
        Label_005A:
            if (num < this.mRankedUpAbilities.Count)
            {
                goto Label_0014;
            }
            this.mRankedUpAbilities.Clear();
            this.mAbilityRankUpRequestSent = 0;
            MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(&str, &str2);
            this.RequestAPI(new ReqAbilityRankUp(dictionary, new Network.ResponseCallback(this.OnSubmitAbilityRankUpResult), str, str2));
            this.SetUnitDirty();
            return;
        }

        private void SubmitUnitKyoka()
        {
            if (Network.Mode != null)
            {
                goto Label_0032;
            }
            Network.RequestAPI(new ReqUnitExpAdd(this.mCurrentUnitID, this.mUsedExpItems, new Network.ResponseCallback(this.OnUnitKyokaResult)), 0);
            goto Label_0038;
        Label_0032:
            this.FinishKyokaRequest();
        Label_0038:
            this.mUsedExpItems.Clear();
            this.SetUnitDirty();
            return;
        }

        private Coroutine SyncKyokaRequest()
        {
            return base.StartCoroutine(this.WaitForKyokaRequestAsync(1));
        }

        private unsafe bool TabChange(SRPG_Button button)
        {
            bool flag;
            eTabButtons buttons;
            Dictionary<eTabButtons, SRPG_ToggleButton>.KeyCollection.Enumerator enumerator;
            Canvas canvas;
            if (button.IsInteractable() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            flag = 0;
            enumerator = this.mTabButtons.Keys.GetEnumerator();
        Label_0020:
            try
            {
                goto Label_00E9;
            Label_0025:
                buttons = &enumerator.Current;
                if (this.mTabButtons.ContainsKey(buttons) != null)
                {
                    goto Label_0043;
                }
                goto Label_00E9;
            Label_0043:
                if ((this.mTabButtons[buttons] == null) == null)
                {
                    goto Label_005F;
                }
                goto Label_00E9;
            Label_005F:
                flag = this.mTabButtons[buttons] == button;
                if (flag == null)
                {
                    goto Label_00A3;
                }
                this.mActiveTabIndex = buttons;
                if ((this.mTabPages[this.mActiveTabIndex] == null) == null)
                {
                    goto Label_00A3;
                }
                this.CreateTabPage(this.mActiveTabIndex);
            Label_00A3:
                this.mTabButtons[buttons].IsOn = flag;
                if ((this.mTabPages[buttons] != null) == null)
                {
                    goto Label_00E9;
                }
                canvas = this.mTabPages[buttons].GetComponent<Canvas>();
                if ((canvas != null) == null)
                {
                    goto Label_00E9;
                }
                canvas.set_enabled(flag);
            Label_00E9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0025;
                }
                goto Label_0106;
            }
            finally
            {
            Label_00FA:
                ((Dictionary<eTabButtons, SRPG_ToggleButton>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_0106:
            return 1;
        }

        public void TobiraUIActive(bool is_active, bool is_immediate)
        {
            if (this.mCurrentUnit.IsUnlockTobira != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if ((this.UnitinfoViewAnimator == null) == null)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            base.StartCoroutine(this._TobiraUIActive(is_active, is_immediate));
            return;
        }

        [DebuggerHidden]
        private IEnumerator UnitUnlockTobira()
        {
            <UnitUnlockTobira>c__Iterator15D iteratord;
            iteratord = new <UnitUnlockTobira>c__Iterator15D();
            iteratord.<>f__this = this;
            return iteratord;
        }

        private void UpdataSkinParamChange(ArtifactParam artifact, bool isAll)
        {
            Dictionary<long, string> dictionary;
            int num;
            if (Network.Mode != null)
            {
                goto Label_0113;
            }
            dictionary = new Dictionary<long, string>();
            if (isAll == null)
            {
                goto Label_0090;
            }
            num = 0;
            goto Label_0078;
        Label_001D:
            if ((this.mCurrentUnit.Jobs[num] == null) || (this.mCurrentUnit.Jobs[num].IsActivated == null))
            {
                goto Label_0074;
            }
            dictionary[this.mCurrentUnit.Jobs[num].UniqueID] = (artifact == null) ? string.Empty : artifact.iname;
        Label_0074:
            num += 1;
        Label_0078:
            if (num < ((int) this.mCurrentUnit.Jobs.Length))
            {
                goto Label_001D;
            }
            goto Label_00EF;
        Label_0090:
            if (this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex] == null)
            {
                goto Label_00EF;
            }
            dictionary[this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex].UniqueID] = ((artifact == null) || (artifact.iname == null)) ? string.Empty : artifact.iname;
        Label_00EF:
            base.GetComponent<WindowController>().SetCollision(0);
            Network.RequestAPI(new ReqSkinSet(dictionary, new Network.ResponseCallback(this.OnSkinSetResult)), 0);
        Label_0113:
            return;
        }

        private void Update()
        {
            UnitData data;
            long num;
            float num2;
            if (base.GetComponent<WindowController>().IsOpened == null)
            {
                goto Label_006F;
            }
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID);
            if (data == null)
            {
                goto Label_006F;
            }
            if (data.LastSyncTime <= this.mLastSyncTime)
            {
                goto Label_006F;
            }
            num = GlobalVars.SelectedJobUniqueID.Get();
            this.Refresh(this.mCurrentUnitID, data.Jobs[this.mSelectedJobIndex].UniqueID, 1, 1);
            this.RefreshReturningJobState(num);
        Label_006F:
            if (this.mUpdatePreviewVisibility == null)
            {
                goto Label_00BE;
            }
            if (this.mDesiredPreviewVisibility == null)
            {
                goto Label_00BE;
            }
            if ((this.mCurrentPreview != null) == null)
            {
                goto Label_00BE;
            }
            if (this.mCurrentPreview.IsLoading != null)
            {
                goto Label_00BE;
            }
            GameUtility.SetLayer(this.mCurrentPreview, GameUtility.LayerCH, 1);
            this.mUpdatePreviewVisibility = 0;
        Label_00BE:
            if (this.mBGUnitImgFadeTime >= this.mBGUnitImgFadeTimeMax)
            {
                goto Label_013E;
            }
            if ((this.mBGUnitImage != null) == null)
            {
                goto Label_013E;
            }
            this.mBGUnitImgFadeTime += Time.get_unscaledDeltaTime();
            num2 = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
            this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num2));
            if (num2 < 1f)
            {
                goto Label_013E;
            }
            this.mBGUnitImgFadeTime = 0f;
            this.mBGUnitImgFadeTimeMax = 0f;
        Label_013E:
            if (this.mExpStart >= this.mExpEnd)
            {
                goto Label_0155;
            }
            this.AnimateExp();
        Label_0155:
            if (this.mDefferedCallDelay <= 0f)
            {
                goto Label_018F;
            }
            this.mDefferedCallDelay -= Time.get_unscaledDeltaTime();
            if (this.mDefferedCallDelay > 0f)
            {
                goto Label_018F;
            }
            this.ExecQueuedKyokaRequest(null);
        Label_018F:
            if ((this.mCurrentKyoukaItemHold != null) == null)
            {
                goto Label_01F8;
            }
            if (this.mKeepWindowLocked != null)
            {
                goto Label_01F8;
            }
            if (this.mCurrentKyoukaItemHold.Holding == null)
            {
                goto Label_01D0;
            }
            this.mCurrentKyoukaItemHold.UpdateTimer(Time.get_unscaledDeltaTime());
            goto Label_01F8;
        Label_01D0:
            this.mCurrentKyoukaItemHold = null;
            if ((this.mKyoukaItemScroll != null) == null)
            {
                goto Label_01F8;
            }
            this.mKyoukaItemScroll.set_scrollSensitivity(1f);
        Label_01F8:
            if ((this.mCurrentAbilityRankUpHold != null) == null)
            {
                goto Label_0235;
            }
            if (this.mCurrentAbilityRankUpHold.Holding == null)
            {
                goto Label_022E;
            }
            this.mCurrentAbilityRankUpHold.UpdatePress(Time.get_unscaledDeltaTime());
            goto Label_0235;
        Label_022E:
            this.mCurrentAbilityRankUpHold = null;
        Label_0235:
            if ((Time.get_realtimeSinceStartup() - this.mLeftTime) <= ((float) PLAY_LEFTVOICE_SPAN))
            {
                goto Label_0262;
            }
            if (this.IsCanPlayLeftVoice() == null)
            {
                goto Label_0262;
            }
            this.PlayUnitVoice("chara_0008");
        Label_0262:
            return;
        }

        private void UpdateJobChangeButtonState()
        {
            int num;
            Transform transform;
            if ((this.JobChangeButton != null) == null)
            {
                goto Label_00AD;
            }
            this.JobChangeButton.IsOn = this.mCurrentUnit.CurrentJob.UniqueID == this.mCurrentJobUniqueID;
            this.JobChangeButton.UpdateButtonState();
            if (this.JobChangeButton.get_transform().get_childCount() <= 0)
            {
                goto Label_00AD;
            }
            num = 0;
            goto Label_0097;
        Label_005C:
            transform = this.JobChangeButton.get_transform().GetChild(num);
            if ((transform != null) == null)
            {
                goto Label_0093;
            }
            transform.get_gameObject().SetActive(this.JobChangeButton.IsOn == 0);
        Label_0093:
            num += 1;
        Label_0097:
            if (num < this.JobChangeButton.get_transform().get_childCount())
            {
                goto Label_005C;
            }
        Label_00AD:
            return;
        }

        private void UpdateJobRankUpButtonState()
        {
            bool flag;
            bool flag2;
            int num;
            bool flag3;
            Animator animator;
            Animator animator2;
            bool flag4;
            bool flag5;
            NeedEquipItemList list;
            bool flag6;
            Animator animator3;
            Canvas canvas;
            if (this.mCurrentUnit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            flag = this.mCurrentUnit.CurrentJob.Rank == 0;
            flag2 = 0;
            if (flag == null)
            {
                goto Label_0045;
            }
            flag2 = this.mCurrentUnit.CheckJobUnlock(this.mCurrentUnit.JobIndex, 1);
            goto Label_005D;
        Label_0045:
            flag2 = this.mCurrentUnit.CheckJobRankUpAllEquip(this.mCurrentUnit.JobIndex, 1);
        Label_005D:
            flag3 = (this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) > this.mCurrentUnit.CurrentJob.Rank) == 0;
            if ((this.mEquipmentPanel.JobRankUpButton != null) == null)
            {
                goto Label_012D;
            }
            this.mEquipmentPanel.JobRankUpButton.get_gameObject().SetActive((flag != null) ? 0 : (flag3 == 0));
            this.mEquipmentPanel.JobRankUpButton.set_interactable(flag2);
            this.mEquipmentPanel.JobRankUpButton.UpdateButtonState();
            if (string.IsNullOrEmpty(this.JobRankUpButtonHilitBool) != null)
            {
                goto Label_012D;
            }
            animator = this.mEquipmentPanel.JobRankUpButton.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_012D;
            }
            animator.SetBool(this.JobRankUpButtonHilitBool, flag2);
            animator.Update(0f);
        Label_012D:
            if ((this.mEquipmentPanel.JobUnlockButton != null) == null)
            {
                goto Label_01CF;
            }
            this.mEquipmentPanel.JobUnlockButton.get_gameObject().SetActive((flag == null) ? 0 : (flag3 == 0));
            this.mEquipmentPanel.JobUnlockButton.set_interactable(flag2);
            this.mEquipmentPanel.JobUnlockButton.UpdateButtonState();
            if (string.IsNullOrEmpty(this.JobRankUpButtonHilitBool) != null)
            {
                goto Label_01CF;
            }
            animator2 = this.mEquipmentPanel.JobUnlockButton.GetComponent<Animator>();
            if ((animator2 != null) == null)
            {
                goto Label_01CF;
            }
            animator2.SetBool(this.JobRankUpButtonHilitBool, flag2);
            animator2.Update(0f);
        Label_01CF:
            this.mIsJobLvMaxAllEquip = 0;
            flag4 = 0;
            if ((this.mEquipmentPanel.AllEquipButton != null) == null)
            {
                goto Label_030E;
            }
            if (<>f__am$cacheEC != null)
            {
                goto Label_0213;
            }
            <>f__am$cacheEC = new Predicate<EquipData>(UnitEnhanceV3.<UpdateJobRankUpButtonState>m__44E);
        Label_0213:
            flag5 = -1 == Array.FindIndex<EquipData>(this.mCurrentUnit.CurrentEquips, <>f__am$cacheEC);
            list = new NeedEquipItemList();
            flag6 = MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.mCurrentUnit, this.mCurrentUnit.CurrentEquips, list);
            if ((flag3 != null) && (flag5 == null))
            {
                goto Label_0274;
            }
            this.mEquipmentPanel.AllEquipButton.get_gameObject().SetActive(0);
            goto Label_02B5;
        Label_0274:
            this.mIsJobLvMaxAllEquip = 1;
            flag4 = 1;
            this.mEquipmentPanel.AllEquipButton.get_gameObject().SetActive(1);
            this.mEquipmentPanel.AllEquipButton.set_interactable((flag6 != null) ? 1 : list.IsEnoughCommon());
        Label_02B5:
            this.mEquipmentPanel.AllEquipButton.UpdateButtonState();
            if (string.IsNullOrEmpty(this.AllEquipButtonHilitBool) != null)
            {
                goto Label_030E;
            }
            animator3 = this.mEquipmentPanel.AllEquipButton.GetComponent<Animator>();
            if ((animator3 != null) == null)
            {
                goto Label_030E;
            }
            animator3.SetBool(this.AllEquipButtonHilitBool, flag2);
            animator3.Update(0f);
        Label_030E:
            if ((this.mEquipmentPanel.JobRankupAllIn != null) == null)
            {
                goto Label_037D;
            }
            this.mEquipmentPanel.JobRankupAllIn.get_gameObject().SetActive((flag != null) ? 0 : (flag3 == 0));
            this.mEquipmentPanel.JobRankupAllIn.set_interactable(this.mCurrentUnit.CheckJobRankUpAllEquip(this.mCurrentUnit.JobIndex, 0));
            this.mEquipmentPanel.JobRankupAllIn.UpdateButtonState();
        Label_037D:
            if ((this.mEquipmentPanel.JobRankCapCaution != null) == null)
            {
                goto Label_03B1;
            }
            this.mEquipmentPanel.JobRankCapCaution.SetActive((flag3 == null) ? 0 : (flag4 == 0));
        Label_03B1:
            canvas = this.mEquipmentPanel.GetComponent<Canvas>();
            if ((canvas != null) == null)
            {
                goto Label_03E7;
            }
            if (canvas.get_enabled() == null)
            {
                goto Label_03E7;
            }
            canvas.set_enabled(0);
            canvas.set_enabled(1);
        Label_03E7:
            return;
        }

        private unsafe void UpdateJobSlotStates(bool immediate)
        {
            UnitInventoryJobIcon[] iconArray;
            int num;
            bool flag;
            int num2;
            iconArray = this.ScrollClampedJobIcons.GetComponentsInChildren<UnitInventoryJobIcon>();
            num = 0;
            goto Label_0046;
        Label_0013:
            flag = iconArray[num].BaseJobIconButton.get_name() == &this.mCurrentUnit.JobIndex.ToString();
            iconArray[num].SetSelectIconAnim(flag);
            num += 1;
        Label_0046:
            if (num < ((int) iconArray.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private void UpdateMainUIAnimator(bool visible)
        {
            WindowController controller;
            Animator animator;
            controller = base.GetComponent<WindowController>();
            if (((controller != null) == null) || (string.IsNullOrEmpty(controller.StateInt) != null))
            {
                goto Label_004F;
            }
            animator = base.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_004F;
            }
            animator.SetInteger(controller.StateInt, (visible == null) ? 0 : 1);
        Label_004F:
            return;
        }

        private void UpdateTrophy_OnJobLevelChange()
        {
            UnitData data;
            JobData data2;
            int num;
            int num2;
            if (this.mCacheUnitData != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("mCacheUnitDataがnullです。リクエストを投げる前に「SetCacheUnitData」を使用してユニットデータをキャッシュしてください。");
            return;
        Label_0016:
            if (this.mCacheUnitData.CurrentJob.Rank >= 1)
            {
                goto Label_0034;
            }
            this.mCacheUnitData = null;
            return;
        Label_0034:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCacheUnitData.UniqueID);
            if (data == null)
            {
                goto Label_00D0;
            }
            data2 = data.GetJobData(this.mCacheUnitData.JobIndex);
            if (data2.UniqueID != this.mCacheUnitData.CurrentJob.UniqueID)
            {
                goto Label_00D0;
            }
            num = data2.Rank - this.mCacheUnitData.CurrentJob.Rank;
            if (num <= 0)
            {
                goto Label_00D0;
            }
            num2 = num;
            MonoSingleton<GameManager>.Instance.Player.OnJobLevelChange(data.UnitParam.iname, data2.Param.iname, data2.Rank, 0, num2);
        Label_00D0:
            this.mCacheUnitData = null;
            return;
        }

        private void UpdateUnitEvolutionButtonState(bool immediate)
        {
            int num;
            int num2;
            Animator animator;
            if ((this.UnitEvolutionButton != null) == null)
            {
                goto Label_00A4;
            }
            this.UnitEvolutionButton.get_gameObject().SetActive(1);
            num = this.mCurrentUnit.Rarity;
            num2 = this.mCurrentUnit.GetRarityCap();
            this.UnitEvolutionButton.set_interactable(num < num2);
            if (immediate == null)
            {
                goto Label_005A;
            }
            this.UnitEvolutionButton.UpdateButtonState();
        Label_005A:
            if (string.IsNullOrEmpty(this.UnitEvolutionButtonHilitBool) != null)
            {
                goto Label_00A4;
            }
            animator = this.UnitEvolutionButton.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_00A4;
            }
            animator.SetBool(this.UnitEvolutionButtonHilitBool, this.mCurrentUnit.CheckUnitRarityUp());
            animator.Update(0f);
        Label_00A4:
            return;
        }

        private void UpdateUnitKakuseiButtonState()
        {
            int num;
            int num2;
            Animator animator;
            if ((this.UnitKakuseiButton != null) == null)
            {
                goto Label_0082;
            }
            num = this.mCurrentUnit.AwakeLv;
            num2 = this.mCurrentUnit.GetAwakeLevelCap();
            this.UnitKakuseiButton.set_interactable(num < num2);
            if (string.IsNullOrEmpty(this.UnitKakuseiButtonHilitBool) != null)
            {
                goto Label_0082;
            }
            animator = this.UnitKakuseiButton.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0082;
            }
            animator.SetBool(this.UnitKakuseiButtonHilitBool, this.mCurrentUnit.CheckUnitAwaking());
            animator.Update(0f);
        Label_0082:
            return;
        }

        private unsafe void UpdateUnitTobiraButtonState()
        {
            bool flag;
            List<UnitData.TobiraConditioError> list;
            bool flag2;
            Animator animator;
            List<ConditionsResult> list2;
            bool flag3;
            int num;
            this.UnitUnlockTobiraButton.get_gameObject().SetActive(0);
            this.UnitGoTobiraButton.get_gameObject().SetActive(0);
            if (this.mCurrentUnit.CanUnlockTobira() != null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            flag = (this.mCurrentUnit.Rarity < this.mCurrentUnit.GetRarityCap()) == 0;
            list = null;
            flag2 = (this.mCurrentUnit.CanUnlockTobira() == null) ? 0 : this.mCurrentUnit.MeetsTobiraConditions(0, &list);
            if (this.mCurrentUnit.IsUnlockTobira == null)
            {
                goto Label_00C2;
            }
            if ((this.UnitGoTobiraButton != null) == null)
            {
                goto Label_00A5;
            }
            this.UnitGoTobiraButton.get_gameObject().SetActive(1);
        Label_00A5:
            this.UnitEvolutionButton.get_gameObject().SetActive(0);
            FlowNode_GameObject.ActivateOutputLinks(this, 500);
            return;
        Label_00C2:
            if (flag != null)
            {
                goto Label_00C9;
            }
            return;
        Label_00C9:
            this.UnitEvolutionButton.get_gameObject().SetActive(0);
            this.UnitUnlockTobiraButton.get_gameObject().SetActive(1);
            if (string.IsNullOrEmpty(this.UnitTobiraButtonHilitBool) == null)
            {
                goto Label_00FC;
            }
            return;
        Label_00FC:
            animator = this.UnitUnlockTobiraButton.GetComponent<Animator>();
            if ((animator == null) == null)
            {
                goto Label_0115;
            }
            return;
        Label_0115:
            if (flag2 == null)
            {
                goto Label_0188;
            }
            list2 = TobiraUtility.TobiraRecipeCheck(this.mCurrentUnit, 0, 0);
            flag3 = 1;
            num = 0;
            goto Label_0156;
        Label_0135:
            if (list2[num].isClear != null)
            {
                goto Label_0150;
            }
            flag3 = 0;
            goto Label_0164;
        Label_0150:
            num += 1;
        Label_0156:
            if (num < list2.Count)
            {
                goto Label_0135;
            }
        Label_0164:
            animator.SetBool(this.UnitTobiraButtonHilitBool, flag3);
            animator.Update(0f);
            FlowNode_GameObject.ActivateOutputLinks(this, 500);
        Label_0188:
            return;
        }

        [DebuggerHidden]
        private IEnumerator WaitForKyokaRequestAndInvokeUserClose()
        {
            <WaitForKyokaRequestAndInvokeUserClose>c__Iterator155 iterator;
            iterator = new <WaitForKyokaRequestAndInvokeUserClose>c__Iterator155();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator WaitForKyokaRequestAsync(bool unlockWindow)
        {
            <WaitForKyokaRequestAsync>c__Iterator14F iteratorf;
            iteratorf = new <WaitForKyokaRequestAsync>c__Iterator14F();
            iteratorf.unlockWindow = unlockWindow;
            iteratorf.<$>unlockWindow = unlockWindow;
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        public UnitData CurrentUnit
        {
            get
            {
                return this.mCurrentUnit;
            }
        }

        public List<ItemData> TmpExpItems
        {
            get
            {
                return this.mTmpItems;
            }
        }

        private List<UnitInventoryJobIcon> UnitJobIconSetList
        {
            get
            {
                int num;
                if (this.mUnitJobIconSetList != null)
                {
                    goto Label_0058;
                }
                this.mUnitJobIconSetList = new List<UnitInventoryJobIcon>();
                num = 0;
                goto Label_0042;
            Label_001D:
                this.mUnitJobIconSetList.Add(this.mJobIconScrollListController.Items[num].job_icon);
                num += 1;
            Label_0042:
                if (num < this.mJobIconScrollListController.Items.Count)
                {
                    goto Label_001D;
                }
            Label_0058:
                return this.mUnitJobIconSetList;
            }
        }

        private ScrollClamped_JobIcons ScrollClampedJobIcons
        {
            get
            {
                if ((this.mScrollClampedJobIcons == null) == null)
                {
                    goto Label_0039;
                }
                this.mScrollClampedJobIcons = this.mJobIconScrollListController.GetComponent<ScrollClamped_JobIcons>();
                this.mScrollClampedJobIcons.OnFrameOutItem = new ScrollClamped_JobIcons.FrameOutItem(this.RefreshJobIcon);
            Label_0039:
                return this.mScrollClampedJobIcons;
            }
        }

        public long IsSetJobUniqueID
        {
            get
            {
                return this.mIsSetJobUniqueID;
            }
        }

        public List<long> UnitList
        {
            get
            {
                return this.m_UnitList;
            }
            set
            {
                this.m_UnitList = value;
                return;
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

        private bool IsKyokaRequestQueued
        {
            get
            {
                return ((this.mDefferedCallFunc == null) == 0);
            }
        }

        private bool IsUnitImageFading
        {
            get
            {
                return (this.mBGUnitImgFadeTime < this.mBGUnitImgFadeTimeMax);
            }
        }

        public UnitEquipmentWindow EquipmentWindow
        {
            get
            {
                GameObject obj2;
                GameObject obj3;
                UnitEquipmentWindow window;
                if ((this.mEquipmentWindow == null) == null)
                {
                    goto Label_0068;
                }
                if ((UnitManagementWindow.Instance != null) == null)
                {
                    goto Label_0068;
                }
                obj2 = AssetManager.Load<GameObject>(UnitManagementWindow.Instance.PATH_UNIT_EQUIPMENT_WINDOW);
                if ((obj2 != null) == null)
                {
                    goto Label_0068;
                }
                window = Object.Instantiate<GameObject>(obj2).GetComponent<UnitEquipmentWindow>();
                window.get_transform().SetParent(UnitManagementWindow.Instance.get_transform(), 0);
                this.EquipmentWindow = window;
            Label_0068:
                return this.mEquipmentWindow;
            }
            set
            {
                this.mEquipmentWindow = value;
                if ((this.mEquipmentWindow != null) == null)
                {
                    goto Label_005D;
                }
                this.mEquipmentWindow.OnEquip = new UnitEquipmentWindow.EquipEvent(this.OnEquipNoCommon);
                this.mEquipmentWindow.OnCommonEquip = new UnitEquipmentWindow.EquipEvent(this.OnEquipCommon);
                this.mEquipmentWindow.OnReload = new UnitEquipmentWindow.EquipReloadEvent(this.UpdateJobRankUpButtonState);
            Label_005D:
                return;
            }
        }

        public UnitKakeraWindow KakeraWindow
        {
            get
            {
                GameObject obj2;
                GameObject obj3;
                UnitKakeraWindow window;
                if ((this.mKakeraWindow == null) == null)
                {
                    goto Label_0068;
                }
                if ((UnitManagementWindow.Instance != null) == null)
                {
                    goto Label_0068;
                }
                obj2 = AssetManager.Load<GameObject>(UnitManagementWindow.Instance.PATH_UNIT_KAKERA_WINDOW);
                if ((obj2 != null) == null)
                {
                    goto Label_0068;
                }
                window = Object.Instantiate<GameObject>(obj2).GetComponent<UnitKakeraWindow>();
                window.get_transform().SetParent(UnitManagementWindow.Instance.get_transform(), 0);
                this.KakeraWindow = window;
            Label_0068:
                return this.mKakeraWindow;
            }
            set
            {
                this.mKakeraWindow = value;
                if ((this.mKakeraWindow != null) == null)
                {
                    goto Label_002F;
                }
                this.mKakeraWindow.OnAwakeAccept = new UnitKakeraWindow.AwakeEvent(this.OnUnitAwake2);
            Label_002F:
                return;
            }
        }

        public UnitTobiraInventory TobiraInventoryWindow
        {
            get
            {
                GameObject obj2;
                GameObject obj3;
                UnitTobiraInventory inventory;
                if ((this.mTobiraInventoryWindow == null) == null)
                {
                    goto Label_0068;
                }
                if ((UnitManagementWindow.Instance != null) == null)
                {
                    goto Label_0068;
                }
                obj2 = AssetManager.Load<GameObject>(UnitManagementWindow.Instance.PATH_TOBIRA_INVENTORY_WINDOW);
                if ((obj2 != null) == null)
                {
                    goto Label_0068;
                }
                inventory = Object.Instantiate<GameObject>(obj2).GetComponent<UnitTobiraInventory>();
                inventory.get_transform().SetParent(UnitManagementWindow.Instance.get_transform(), 0);
                this.TobiraInventoryWindow = inventory;
            Label_0068:
                return this.mTobiraInventoryWindow;
            }
            set
            {
                this.mTobiraInventoryWindow = value;
                if ((this.mTobiraInventoryWindow != null) == null)
                {
                    goto Label_0018;
                }
            Label_0018:
                return;
            }
        }

        private int CurrentUnitIndex
        {
            get
            {
                PlayerData data;
                UnitData data2;
                int num;
                data2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID);
                return this.Units.IndexOf(data2);
            }
        }

        private List<UnitData> Units
        {
            get
            {
                return this.SortedUnits;
            }
        }

        public bool IsLoading
        {
            get
            {
                return this.mReloading;
            }
        }

        public bool HasStarted
        {
            get
            {
                return this.mStarted;
            }
        }

        [CompilerGenerated]
        private sealed class <_OpenProfileWindow>c__Iterator158 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal bool <cancel>__1;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <_OpenProfileWindow>c__Iterator158()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0183;

                    case 2:
                        goto Label_01C4;
                }
                goto Label_01CB;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.Prefab_ProfileWindow) != null)
                {
                    goto Label_01C4;
                }
                GlobalVars.UnlockUnitID = this.<>f__this.mCurrentUnit.UnitID;
                this.<req>__0 = AssetManager.LoadAsync<UnitProfileWindow>(this.<>f__this.Prefab_ProfileWindow);
                this.<>f__this.mProfileWindowLoadRequest = this.<req>__0;
                this.<cancel>__1 = 0;
                if (this.<req>__0 == null)
                {
                    goto Label_00B9;
                }
                goto Label_00A9;
            Label_008D:
                if (this.<>f__this.mCloseRequested == null)
                {
                    goto Label_00A9;
                }
                this.<cancel>__1 = 1;
                goto Label_00B9;
            Label_00A9:
                if (this.<req>__0.isDone == null)
                {
                    goto Label_008D;
                }
            Label_00B9:
                if (this.<cancel>__1 != null)
                {
                    goto Label_00F5;
                }
                if (this.<>f__this.mSceneChanging != null)
                {
                    goto Label_00F5;
                }
                if (this.<req>__0 == null)
                {
                    goto Label_00F5;
                }
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_0106;
                }
            Label_00F5:
                this.<>f__this.mProfileWindowLoadRequest = null;
                goto Label_01CB;
            Label_0106:
                if (this.<>f__this.mUnitVoice == null)
                {
                    goto Label_012B;
                }
                this.<>f__this.mUnitVoice.StopAll(1f);
            Label_012B:
                this.<>f__this.mUnitProfileWindow = Object.Instantiate(this.<req>__0.asset) as UnitProfileWindow;
                DataSource.Bind<UnitData>(this.<>f__this.mUnitProfileWindow.get_gameObject(), this.<>f__this.mCurrentUnit);
                goto Label_0183;
            Label_0170:
                this.$current = null;
                this.$PC = 1;
                goto Label_01CD;
            Label_0183:
                if ((this.<>f__this.mUnitProfileWindow != null) != null)
                {
                    goto Label_0170;
                }
                this.<>f__this.mUnitProfileWindow = null;
                this.<>f__this.mProfileWindowLoadRequest = null;
                this.$current = null;
                this.$PC = 2;
                goto Label_01CD;
            Label_01C4:
                this.$PC = -1;
            Label_01CB:
                return 0;
            Label_01CD:
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
        private sealed class <_TobiraUIActive>c__Iterator15C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <MIN_WAIT_TIME>__0;
            internal float <elapsed_time>__1;
            internal Animator <fade_anim>__2;
            internal AnimatorStateInfo <anim_state>__3;
            internal bool is_immediate;
            internal bool is_active;
            internal Dictionary<UnitEnhanceV3.eTabButtons, SRPG_ToggleButton>.KeyCollection.Enumerator <$s_1170>__4;
            internal UnitEnhanceV3.eTabButtons <key>__5;
            internal Animator <tab_anim>__6;
            internal string <anim_name>__7;
            internal bool <is_restore>__8;
            internal float <need_wait_time>__9;
            internal int $PC;
            internal object $current;
            internal bool <$>is_immediate;
            internal bool <$>is_active;
            internal UnitEnhanceV3 <>f__this;

            public <_TobiraUIActive>c__Iterator15C()
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

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_003D;

                    case 1:
                        goto Label_00D2;

                    case 2:
                        goto Label_0120;

                    case 3:
                        goto Label_014D;

                    case 4:
                        goto Label_0280;

                    case 5:
                        goto Label_03B7;

                    case 6:
                        goto Label_048B;

                    case 7:
                        goto Label_04BA;

                    case 8:
                        goto Label_04F6;
                }
                goto Label_0523;
            Label_003D:
                this.<MIN_WAIT_TIME>__0 = 0.4f;
                this.<elapsed_time>__1 = 0f;
                this.<>f__this.GetComponent<WindowController>().SetCollision(0);
                this.<>f__this.StopUnitVoice();
                this.<fade_anim>__2 = GameObjectID.FindGameObject<Animator>("UNIT_LIST_FADE");
                this.<anim_state>__3 = this.<fade_anim>__2.GetCurrentAnimatorStateInfo(0);
                if (this.is_immediate != null)
                {
                    goto Label_014D;
                }
                this.<elapsed_time>__1 += Time.get_unscaledDeltaTime();
                this.<fade_anim>__2.SetBool("fade", 1);
                this.$current = null;
                this.$PC = 1;
                goto Label_0525;
            Label_00D2:
                this.<anim_state>__3 = this.<fade_anim>__2.GetCurrentAnimatorStateInfo(0);
                goto Label_0120;
            Label_00E9:
                this.<elapsed_time>__1 += Time.get_unscaledDeltaTime();
                this.<anim_state>__3 = this.<fade_anim>__2.GetCurrentAnimatorStateInfo(0);
                this.$current = null;
                this.$PC = 2;
                goto Label_0525;
            Label_0120:
                if (&this.<anim_state>__3.get_normalizedTime() < 1f)
                {
                    goto Label_00E9;
                }
                goto Label_014D;
            Label_013A:
                this.$current = null;
                this.$PC = 3;
                goto Label_0525;
            Label_014D:
                if (this.<>f__this.ExecQueuedKyokaRequest(null) != null)
                {
                    goto Label_013A;
                }
                if (this.is_active == null)
                {
                    goto Label_0280;
                }
                if (this.<>f__this.mTabButtons == null)
                {
                    goto Label_025B;
                }
                this.<$s_1170>__4 = this.<>f__this.mTabButtons.Keys.GetEnumerator();
            Label_0194:
                try
                {
                    goto Label_0235;
                Label_0199:
                    this.<key>__5 = &this.<$s_1170>__4.Current;
                    this.<tab_anim>__6 = this.<>f__this.mTabButtons[this.<key>__5].GetComponent<Animator>();
                    if ((this.<tab_anim>__6 == null) == null)
                    {
                        goto Label_01E1;
                    }
                    goto Label_0235;
                Label_01E1:
                    this.<tab_anim>__6.Play("Normal");
                    this.<tab_anim>__6.SetBool("Normal", 0);
                    this.<tab_anim>__6.SetBool("Highlighted", 0);
                    this.<tab_anim>__6.SetBool("Pressed", 0);
                    this.<tab_anim>__6.SetBool("Disable", 0);
                Label_0235:
                    if (&this.<$s_1170>__4.MoveNext() != null)
                    {
                        goto Label_0199;
                    }
                    goto Label_025B;
                }
                finally
                {
                Label_024A:
                    ((Dictionary<UnitEnhanceV3.eTabButtons, SRPG_ToggleButton>.KeyCollection.Enumerator) this.<$s_1170>__4).Dispose();
                }
            Label_025B:
                this.<elapsed_time>__1 += Time.get_unscaledDeltaTime();
                this.$current = null;
                this.$PC = 4;
                goto Label_0525;
            Label_0280:
                if (this.is_active == null)
                {
                    goto Label_02BD;
                }
                this.<>f__this.MuteVoice = 1;
                this.<>f__this.Refresh(this.<>f__this.mCurrentUnitID, 0L, 1, 1);
                this.<>f__this.MuteVoice = 0;
            Label_02BD:
                this.<anim_name>__7 = (this.is_active == null) ? "unit" : "tobira";
                this.<>f__this.UnitinfoViewAnimator.Play(this.<anim_name>__7);
                this.<>f__this.TobiraInventoryWindow.get_gameObject().SetActive(this.is_active);
                if ((this.<>f__this.mBGTobiraAnimator != null) == null)
                {
                    goto Label_0387;
                }
                if ((this.<>f__this.mBGAnimator != null) == null)
                {
                    goto Label_0387;
                }
                this.<>f__this.mBGAnimator.get_transform().get_parent().get_gameObject().SetActive(this.is_active == 0);
                this.<>f__this.mBGTobiraAnimator.get_transform().get_parent().get_gameObject().SetActive(this.is_active);
            Label_0387:
                if (this.is_immediate != null)
                {
                    goto Label_03B7;
                }
                this.<elapsed_time>__1 += Time.get_unscaledDeltaTime();
                this.$current = null;
                this.$PC = 5;
                goto Label_0525;
            Label_03B7:
                if (this.is_active == null)
                {
                    goto Label_03E9;
                }
                this.<is_restore>__8 = this.is_immediate;
                this.<>f__this.TobiraInventoryWindow.Init(this.<is_restore>__8);
                goto Label_041B;
            Label_03E9:
                this.<>f__this.MuteVoice = 1;
                this.<>f__this.Refresh(this.<>f__this.mCurrentUnitID, 0L, 1, 1);
                this.<>f__this.MuteVoice = 0;
            Label_041B:
                UnitManagementWindow.Instance.ChangeUnitPreviewPos(this.is_active == 0);
                if (this.is_immediate != null)
                {
                    goto Label_048B;
                }
                this.<elapsed_time>__1 += Time.get_unscaledDeltaTime();
                this.<need_wait_time>__9 = this.<MIN_WAIT_TIME>__0 - this.<elapsed_time>__1;
                if (this.<need_wait_time>__9 <= 0f)
                {
                    goto Label_048B;
                }
                this.$current = new WaitForSeconds(this.<need_wait_time>__9);
                this.$PC = 6;
                goto Label_0525;
            Label_048B:
                if (this.is_immediate != null)
                {
                    goto Label_050B;
                }
                this.<fade_anim>__2.SetBool("fade", 0);
                this.$current = null;
                this.$PC = 7;
                goto Label_0525;
            Label_04BA:
                this.<anim_state>__3 = this.<fade_anim>__2.GetCurrentAnimatorStateInfo(0);
                goto Label_04F6;
            Label_04D1:
                this.<anim_state>__3 = this.<fade_anim>__2.GetCurrentAnimatorStateInfo(0);
                this.$current = null;
                this.$PC = 8;
                goto Label_0525;
            Label_04F6:
                if (&this.<anim_state>__3.get_normalizedTime() < 1f)
                {
                    goto Label_04D1;
                }
            Label_050B:
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.$PC = -1;
            Label_0523:
                return 0;
            Label_0525:
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
        private sealed class <AbilityRankUpSkillUnlockEffect>c__Iterator156 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <AbilityRankUpSkillUnlockEffect>c__Iterator156()
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
                goto Label_008F;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_0091;
            Label_0039:
                if (this.<>f__this.mAbilityRankUpRequestSent == null)
                {
                    goto Label_0026;
                }
                this.<>f__this.StartCoroutine(this.<>f__this.ShowUnlockSkillEffect());
                this.<>f__this.RebuildUnitData();
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.<>f__this.mKeepWindowLocked = 0;
                this.$PC = -1;
            Label_008F:
                return 0;
            Label_0091:
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
        private sealed class <EvolutionButtonClickSync>c__Iterator15A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <request>__0;
            internal GameObject <window_obj>__1;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <EvolutionButtonClickSync>c__Iterator15A()
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
                        goto Label_0025;

                    case 1:
                        goto Label_00B1;

                    case 2:
                        goto Label_0188;
                }
                goto Label_018F;
            Label_0025:
                this.<>f__this.GetComponent<WindowController>().SetCollision(0);
                this.<>f__this.mKeepWindowLocked = 1;
                if (string.IsNullOrEmpty(this.<>f__this.PREFAB_PATH_EVOLUTION_WINDOW) == null)
                {
                    goto Label_0079;
                }
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.<>f__this.mKeepWindowLocked = 0;
                goto Label_018F;
            Label_0079:
                this.<request>__0 = AssetManager.LoadAsync(this.<>f__this.PREFAB_PATH_EVOLUTION_WINDOW);
                goto Label_00B1;
            Label_0094:
                this.$current = this.<request>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0191;
            Label_00B1:
                if (this.<request>__0.isDone == null)
                {
                    goto Label_0094;
                }
                if ((this.<request>__0.asset != null) == null)
                {
                    goto Label_0175;
                }
                this.<window_obj>__1 = Object.Instantiate(this.<request>__0.asset) as GameObject;
                this.<>f__this.mEvolutionWindow = this.<window_obj>__1.GetComponent<UnitEvolutionWindow>();
                this.<>f__this.mEvolutionWindow.OnEvolve = new UnitEvolutionWindow.UnitEvolveEvent(this.<>f__this.OnUnitEvolve);
                this.<>f__this.mEvolutionWindow.OnEvolveClose = new UnitEvolutionWindow.EvolveCloseEvent(this.<>f__this.OnEvolutionWindowClose);
                this.<>f__this.mEvolutionWindow.Unit = this.<>f__this.mCurrentUnit;
                this.<>f__this.mEvolutionWindow.Refresh2();
            Label_0175:
                this.$current = null;
                this.$PC = 2;
                goto Label_0191;
            Label_0188:
                this.$PC = -1;
            Label_018F:
                return 0;
            Label_0191:
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
        private sealed class <LoadAllUnitImage>c__Iterator153 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <LoadAllUnitImage>c__Iterator153()
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
                        goto Label_00E3;
                }
                goto Label_00EA;
            Label_0021:
                if ((this.<>f__this.mBGUnitImage != null) == null)
                {
                    goto Label_00D0;
                }
                if (this.<>f__this.mCurrentUnit.Jobs == null)
                {
                    goto Label_00D0;
                }
                this.<i>__0 = 0;
                goto Label_00B3;
            Label_0058:
                AssetManager.LoadAsync<Texture2D>(AssetPath.UnitSkinImage2(this.<>f__this.mCurrentUnit.UnitParam, this.<>f__this.mCurrentUnit.GetSelectedSkin(this.<i>__0), this.<>f__this.mCurrentUnit.Jobs[this.<i>__0].JobID));
                this.<i>__0 += 1;
            Label_00B3:
                if (this.<i>__0 < ((int) this.<>f__this.mCurrentUnit.Jobs.Length))
                {
                    goto Label_0058;
                }
            Label_00D0:
                this.$current = null;
                this.$PC = 1;
                goto Label_00EC;
            Label_00E3:
                this.$PC = -1;
            Label_00EA:
                return 0;
            Label_00EC:
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
        private sealed class <OnArtifactSelect>c__AnonStorey3BD
        {
            internal ArtifactData[] view_artifact_datas;

            public <OnArtifactSelect>c__AnonStorey3BD()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <OnArtifactSelect>c__AnonStorey3BE
        {
            internal int i;
            internal UnitEnhanceV3.<OnArtifactSelect>c__AnonStorey3BD <>f__ref$957;

            public <OnArtifactSelect>c__AnonStorey3BE()
            {
                base..ctor();
                return;
            }

            internal bool <>m__44B(ArtifactData x)
            {
                return ((x == null) ? 0 : (x.UniqueID == this.<>f__ref$957.view_artifact_datas[this.i].UniqueID));
            }
        }

        [CompilerGenerated]
        private sealed class <OnSceneChangeAsync>c__Iterator14B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <OnSceneChangeAsync>c__Iterator14B()
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
                        goto Label_0025;

                    case 1:
                        goto Label_003D;

                    case 2:
                        goto Label_006B;
                }
                goto Label_0072;
            Label_0025:
                goto Label_003D;
            Label_002A:
                this.$current = null;
                this.$PC = 1;
                goto Label_0074;
            Label_003D:
                if (this.<>f__this.ExecQueuedKyokaRequest(null) != null)
                {
                    goto Label_002A;
                }
                if (Network.IsConnecting != null)
                {
                    goto Label_002A;
                }
                this.$current = null;
                this.$PC = 2;
                goto Label_0074;
            Label_006B:
                this.$PC = -1;
            Label_0072:
                return 0;
            Label_0074:
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
        private sealed class <OnSkinSelectOpenAsync>c__Iterator161 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal UnitSkinSelectWindow <window>__1;
            internal bool isView;
            internal int $PC;
            internal object $current;
            internal bool <$>isView;
            internal UnitEnhanceV3 <>f__this;

            public <OnSkinSelectOpenAsync>c__Iterator161()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0090;

                    case 2:
                        goto Label_020B;
                }
                goto Label_0212;
            Label_0025:
                this.<>f__this.GetComponent<WindowController>().SetCollision(0);
                this.<>f__this.mKeepWindowLocked = 1;
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.SkinSelectTemplate);
                if (this.<req>__0 == null)
                {
                    goto Label_0090;
                }
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0090;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0214;
            Label_0090:
                if (this.<req>__0 == null)
                {
                    goto Label_00B1;
                }
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_00DD;
                }
            Label_00B1:
                Debug.Log("SkinSelectTemplate: Load Template Faild");
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.<>f__this.mKeepWindowLocked = 0;
                goto Label_0212;
            Label_00DD:
                this.<>f__this.mSkinSelectWindow = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<window>__1 = this.<>f__this.mSkinSelectWindow.GetComponentInChildren<UnitSkinSelectWindow>();
                if ((this.<window>__1 != null) == null)
                {
                    goto Label_01DD;
                }
                this.<window>__1.OnSkinSelect = new UnitSkinSelectWindow.SkinSelectEvent(this.<>f__this.OnSkinSelect);
                this.<window>__1.OnSkinDecide = new UnitSkinSelectWindow.SkinDecideEvent(this.<>f__this.OnSkinDecide);
                this.<window>__1.OnSkinDecideAll = new UnitSkinSelectWindow.SkinDecideEvent(this.<>f__this.OnSkinDecideAll);
                this.<window>__1.OnSkinRemove = new UnitSkinSelectWindow.SkinRemoveEvent(this.<>f__this.OnSkinRemoved);
                this.<window>__1.OnSkinRemoveAll = new UnitSkinSelectWindow.SkinRemoveEvent(this.<>f__this.OnSkinRemovedAll);
                this.<window>__1.OnSkinClose = new UnitSkinSelectWindow.SkinCloseEvent(this.<>f__this.OnSkinWindowClose);
                this.<window>__1.IsViewOnly = this.isView;
            Label_01DD:
                DataSource.Bind<UnitData>(this.<>f__this.mSkinSelectWindow, this.<>f__this.mCurrentUnit);
                this.$current = null;
                this.$PC = 2;
                goto Label_0214;
            Label_020B:
                this.$PC = -1;
            Label_0212:
                return 0;
            Label_0214:
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
        private sealed class <OnUnitBulkLevelUp>c__AnonStorey3BC
        {
            internal KeyValuePair<string, int> p;

            public <OnUnitBulkLevelUp>c__AnonStorey3BC()
            {
                base..ctor();
                return;
            }

            internal unsafe bool <>m__449(ItemData tmp)
            {
                return (tmp.ItemID == &this.p.Key);
            }
        }

        [CompilerGenerated]
        private sealed class <OpenCharacterQuestPopupInternalAsync>c__Iterator160 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <OpenCharacterQuestPopupInternalAsync>c__Iterator160()
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
                        goto Label_005C;
                }
                goto Label_0063;
            Label_0021:
                MonoSingleton<GameManager>.Instance.AddCharacterQuestPopup(this.<>f__this.mCurrentUnit);
                this.$current = MonoSingleton<GameManager>.Instance.ShowCharacterQuestPopupAsync(GameSettings.Instance.CharacterQuest_Unlock);
                this.$PC = 1;
                goto Label_0065;
            Label_005C:
                this.$PC = -1;
            Label_0063:
                return 0;
            Label_0065:
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
        private sealed class <PlayTobiraLevelupEffect>c__Iterator164 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <effect_request>__0;
            internal TobiraData tobira;
            internal int <_tobiraLv>__1;
            internal bool <_isMaxTobiraLv>__2;
            internal GameObject <effect_obj>__3;
            internal UnitJobMasterWindow <window>__4;
            internal Animator <anim>__5;
            internal string <voice_cueid>__6;
            internal List<AbilityData> <newAbilities>__7;
            internal List<AbilityData> <oldAbilities>__8;
            internal AbilityData <newAbilityData>__9;
            internal AbilityData <oldAbilityData>__10;
            internal SkillData <newLeaderSkillData>__11;
            internal int $PC;
            internal object $current;
            internal TobiraData <$>tobira;
            internal UnitEnhanceV3 <>f__this;

            public <PlayTobiraLevelupEffect>c__Iterator164()
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

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0031;

                    case 1:
                        goto Label_00EA;

                    case 2:
                        goto Label_0248;

                    case 3:
                        goto Label_0287;

                    case 4:
                        goto Label_029F;

                    case 5:
                        goto Label_0412;
                }
                goto Label_0419;
            Label_0031:
                this.<>f__this.RebuildUnitData();
                this.<>f__this.RefreshSortedUnits();
                this.<effect_request>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.PREFAB_PATH_TOBIRA_LEVELUP);
                this.<_tobiraLv>__1 = this.<>f__this.mCurrentUnit.GetTobiraData(this.tobira.Param.TobiraCategory).Lv;
                this.<_isMaxTobiraLv>__2 = (this.<_tobiraLv>__1 < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.TobiraLvCap) == 0;
                if ((this.<effect_request>__0 == null) || (this.<effect_request>__0.isDone != null))
                {
                    goto Label_00EA;
                }
                this.$current = this.<effect_request>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_041B;
            Label_00EA:
                this.<effect_obj>__3 = null;
                if (this.<effect_request>__0 == null)
                {
                    goto Label_01B2;
                }
                this.<effect_obj>__3 = Object.Instantiate(this.<effect_request>__0.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<effect_obj>__3);
                this.<window>__4 = this.<effect_obj>__3.GetComponentInChildren<UnitJobMasterWindow>();
                if ((this.<window>__4 != null) == null)
                {
                    goto Label_0170;
                }
                this.<window>__4.Refresh(this.<>f__this.mCurrentStatus, this.<>f__this.mCurrentUnit.Status);
            Label_0170:
                this.<anim>__5 = this.<effect_obj>__3.GetComponent<Animator>();
                if ((this.<anim>__5 != null) == null)
                {
                    goto Label_01B2;
                }
                this.<anim>__5.SetInteger("state", this.tobira.Param.TobiraCategory);
            Label_01B2:
                this.<>f__this.PlayReaction();
                this.<voice_cueid>__6 = (this.<_isMaxTobiraLv>__2 == null) ? this.<>f__this.mVO_TobiraEnhanceCueID : this.<>f__this.mVO_TobiraMaxCueID;
                this.<>f__this.PlayUnitVoice(this.<voice_cueid>__6);
                if ((this.<>f__this.mBGTobiraEffectAnimator != null) == null)
                {
                    goto Label_0235;
                }
                this.<>f__this.mBGTobiraEffectAnimator.SetInteger("state", this.tobira.Param.TobiraCategory);
            Label_0235:
                this.$current = null;
                this.$PC = 2;
                goto Label_041B;
            Label_0248:
                if ((this.<>f__this.mBGTobiraEffectAnimator != null) == null)
                {
                    goto Label_0274;
                }
                this.<>f__this.mBGTobiraEffectAnimator.SetInteger("state", 0);
            Label_0274:
                this.$current = null;
                this.$PC = 3;
                goto Label_041B;
            Label_0287:
                goto Label_029F;
            Label_028C:
                this.$current = null;
                this.$PC = 4;
                goto Label_041B;
            Label_029F:
                if ((this.<effect_obj>__3 != null) != null)
                {
                    goto Label_028C;
                }
                this.<>f__this.DestroyOverlay();
                this.<>f__this.SpawnStatusChangeEffects();
                this.<>f__this.RefreshBasicParameters(0);
                this.<>f__this.RefreshEXPImmediate();
                this.<>f__this.RefreshExpInfo();
                this.<>f__this.RefreshLevelCap();
                if (this.<_isMaxTobiraLv>__2 == null)
                {
                    goto Label_0412;
                }
                this.<newAbilities>__7 = new List<AbilityData>();
                this.<oldAbilities>__8 = new List<AbilityData>();
                TobiraUtility.TryCraeteAbilityData(this.tobira.Param, this.tobira.Lv, this.<newAbilities>__7, this.<oldAbilities>__8, 1);
                this.<newAbilityData>__9 = (this.<newAbilities>__7.Count <= 0) ? null : this.<newAbilities>__7[0];
                this.<oldAbilityData>__10 = (this.<oldAbilities>__8.Count <= 0) ? null : this.<oldAbilities>__8[0];
                this.<newLeaderSkillData>__11 = null;
                TobiraUtility.TryCraeteLeaderSkill(this.tobira.Param, this.tobira.Lv, &this.<newLeaderSkillData>__11, 1);
                if (this.<newAbilityData>__9 != null)
                {
                    goto Label_03D8;
                }
                if (this.<oldAbilityData>__10 != null)
                {
                    goto Label_03D8;
                }
                if (this.<newLeaderSkillData>__11 == null)
                {
                    goto Label_0412;
                }
            Label_03D8:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.PlayTobiraLevelupMaxEffect(this.<newAbilityData>__9, this.<oldAbilityData>__10, this.<newLeaderSkillData>__11));
                this.$PC = 5;
                goto Label_041B;
            Label_0412:
                this.$PC = -1;
            Label_0419:
                return 0;
            Label_041B:
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
        private sealed class <PlayTobiraLevelupMaxEffect>c__Iterator165 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <effect_request>__0;
            internal UnitTobiraLearnAbilityPopup <popup>__1;
            internal AbilityData newAbilityData;
            internal AbilityParam <newAbilityParam>__2;
            internal AbilityData oldAbilityData;
            internal AbilityParam <oldAbilityParam>__3;
            internal SkillData newLeaderSkillData;
            internal SkillParam <newLeaderSkillParam>__4;
            internal int $PC;
            internal object $current;
            internal AbilityData <$>newAbilityData;
            internal AbilityData <$>oldAbilityData;
            internal SkillData <$>newLeaderSkillData;
            internal UnitEnhanceV3 <>f__this;

            public <PlayTobiraLevelupMaxEffect>c__Iterator165()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0073;

                    case 2:
                        goto Label_0197;
                }
                goto Label_01B4;
            Label_0025:
                this.<effect_request>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.PREFAB_PATH_TOBIRA_LEVEL_MAX);
                if ((this.<effect_request>__0 == null) || (this.<effect_request>__0.isDone != null))
                {
                    goto Label_0073;
                }
                this.$current = this.<effect_request>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_01B6;
            Label_0073:
                if (this.<effect_request>__0 == null)
                {
                    goto Label_0197;
                }
                this.<>f__this.mTobiraLevelupMaxEffectObject = Object.Instantiate(this.<effect_request>__0.asset) as GameObject;
                this.<popup>__1 = this.<>f__this.mTobiraLevelupMaxEffectObject.GetComponent<UnitTobiraLearnAbilityPopup>();
                if ((this.<popup>__1 != null) == null)
                {
                    goto Label_0197;
                }
                this.<newAbilityParam>__2 = (this.newAbilityData == null) ? null : this.newAbilityData.Param;
                this.<oldAbilityParam>__3 = (this.oldAbilityData == null) ? null : this.oldAbilityData.Param;
                this.<newLeaderSkillParam>__4 = (this.newLeaderSkillData == null) ? null : this.newLeaderSkillData.SkillParam;
                if (this.<newAbilityParam>__2 == null)
                {
                    goto Label_0158;
                }
                this.<popup>__1.Setup(this.<>f__this.mCurrentUnit, this.<newAbilityParam>__2, this.<oldAbilityParam>__3);
            Label_0158:
                if (this.<newLeaderSkillParam>__4 == null)
                {
                    goto Label_0197;
                }
                this.<popup>__1.Setup(this.<>f__this.mCurrentUnit, this.<newLeaderSkillParam>__4);
                goto Label_0197;
            Label_0184:
                this.$current = null;
                this.$PC = 2;
                goto Label_01B6;
            Label_0197:
                if ((this.<>f__this.mTobiraLevelupMaxEffectObject != null) != null)
                {
                    goto Label_0184;
                }
                this.$PC = -1;
            Label_01B4:
                return 0;
            Label_01B6:
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
        private sealed class <PlayTobiraOpenEffect>c__Iterator163 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <effect_request>__0;
            internal GameObject <effect_obj>__1;
            internal Animator <anim>__2;
            internal TobiraParam.Category selected_tobira_category;
            internal int $PC;
            internal object $current;
            internal TobiraParam.Category <$>selected_tobira_category;
            internal UnitEnhanceV3 <>f__this;

            public <PlayTobiraOpenEffect>c__Iterator163()
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
                        goto Label_002D;

                    case 1:
                        goto Label_0091;

                    case 2:
                        goto Label_016C;

                    case 3:
                        goto Label_01AB;

                    case 4:
                        goto Label_01C3;
                }
                goto Label_021E;
            Label_002D:
                this.<>f__this.RebuildUnitData();
                this.<>f__this.RefreshSortedUnits();
                this.<effect_request>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.PREFAB_PATH_TOBIRA_OPEN);
                if (this.<effect_request>__0 == null)
                {
                    goto Label_0091;
                }
                if (this.<effect_request>__0.isDone != null)
                {
                    goto Label_0091;
                }
                this.$current = this.<effect_request>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0220;
            Label_0091:
                this.<effect_obj>__1 = null;
                if (this.<effect_request>__0 == null)
                {
                    goto Label_0107;
                }
                this.<effect_obj>__1 = Object.Instantiate(this.<effect_request>__0.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<effect_obj>__1);
                this.<anim>__2 = this.<effect_obj>__1.GetComponent<Animator>();
                if ((this.<anim>__2 != null) == null)
                {
                    goto Label_0107;
                }
                this.<anim>__2.SetInteger("state", this.selected_tobira_category);
            Label_0107:
                this.<>f__this.PlayReaction();
                this.<>f__this.PlayUnitVoice(this.<>f__this.mVO_TobiraOpenCueID);
                if ((this.<>f__this.mBGTobiraEffectAnimator != null) == null)
                {
                    goto Label_0159;
                }
                this.<>f__this.mBGTobiraEffectAnimator.SetInteger("state", this.selected_tobira_category);
            Label_0159:
                this.$current = null;
                this.$PC = 2;
                goto Label_0220;
            Label_016C:
                if ((this.<>f__this.mBGTobiraEffectAnimator != null) == null)
                {
                    goto Label_0198;
                }
                this.<>f__this.mBGTobiraEffectAnimator.SetInteger("state", 0);
            Label_0198:
                this.$current = null;
                this.$PC = 3;
                goto Label_0220;
            Label_01AB:
                goto Label_01C3;
            Label_01B0:
                this.$current = null;
                this.$PC = 4;
                goto Label_0220;
            Label_01C3:
                if ((this.<effect_obj>__1 != null) != null)
                {
                    goto Label_01B0;
                }
                this.<>f__this.DestroyOverlay();
                this.<>f__this.SpawnStatusChangeEffects();
                this.<>f__this.RefreshBasicParameters(0);
                this.<>f__this.RefreshEXPImmediate();
                this.<>f__this.RefreshExpInfo();
                this.<>f__this.RefreshLevelCap();
                this.$PC = -1;
            Label_021E:
                return 0;
            Label_0220:
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
        private sealed class <PostAbilityRankUp>c__Iterator157 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal List<SkillParam> newSkills;
            internal string <skilllist_prefab>__1;
            internal GameObject <go>__2;
            internal UnitLearnSkillWindow <w>__3;
            internal UnitData <newUnit>__4;
            internal int $PC;
            internal object $current;
            internal List<SkillParam> <$>newSkills;
            internal UnitEnhanceV3 <>f__this;

            public <PostAbilityRankUp>c__Iterator157()
            {
                base..ctor();
                return;
            }

            internal bool <>m__458(UnitData p)
            {
                return (p.UnitID == this.<>f__this.mCurrentUnit.UnitID);
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
                        goto Label_002D;

                    case 1:
                        goto Label_00E9;

                    case 2:
                        goto Label_0180;

                    case 3:
                        goto Label_01A9;

                    case 4:
                        goto Label_029A;
                }
                goto Label_02A1;
            Label_002D:
                this.<req>__0 = null;
                if ((string.IsNullOrEmpty(this.<>f__this.Prefab_NewSkillList) != null) || (string.IsNullOrEmpty(this.<>f__this.Prefab_NewSkillList2) != null))
                {
                    goto Label_00E9;
                }
                this.<skilllist_prefab>__1 = (this.newSkills.Count <= 1) ? this.<>f__this.Prefab_NewSkillList : this.<>f__this.Prefab_NewSkillList2;
                if (string.IsNullOrEmpty(this.<skilllist_prefab>__1) != null)
                {
                    goto Label_00E9;
                }
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.<skilllist_prefab>__1);
                if (this.<req>__0 == null)
                {
                    goto Label_00E9;
                }
                if (this.<req>__0.isDone != null)
                {
                    goto Label_00E9;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_02A3;
            Label_00E9:
                if (this.<req>__0 == null)
                {
                    goto Label_01A9;
                }
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_0109;
                }
                goto Label_02A1;
            Label_0109:
                this.<go>__2 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__2);
                this.<w>__3 = this.<go>__2.GetComponentInChildren<UnitLearnSkillWindow>();
                if ((this.<w>__3 != null) == null)
                {
                    goto Label_0180;
                }
                this.<w>__3.Skills = this.newSkills;
                goto Label_0180;
            Label_016D:
                this.$current = null;
                this.$PC = 2;
                goto Label_02A3;
            Label_0180:
                if ((this.<go>__2 != null) != null)
                {
                    goto Label_016D;
                }
                goto Label_01A9;
            Label_0196:
                this.$current = null;
                this.$PC = 3;
                goto Label_02A3;
            Label_01A9:
                if (this.<>f__this.mAbilityRankUpRequestSent == null)
                {
                    goto Label_0196;
                }
                this.<>f__this.DestroyOverlay();
                this.<newUnit>__4 = this.<>f__this.Units.Find(new Predicate<UnitData>(this.<>m__458));
                if (this.<newUnit>__4 == null)
                {
                    goto Label_023C;
                }
                if (this.<newUnit>__4.IsOpenCharacterQuest() == null)
                {
                    goto Label_023C;
                }
                if (this.<>f__this.mChQuestProg == null)
                {
                    goto Label_023C;
                }
                if (this.<>f__this.mCurrentUnit.IsQuestUnlocked(this.<>f__this.mChQuestProg) == null)
                {
                    goto Label_023C;
                }
                this.<>f__this.OpenCharacterQuestPopupInternal();
            Label_023C:
                this.<>f__this.mChQuestProg = null;
                this.<>f__this.StartCoroutine(this.<>f__this.ShowUnlockSkillEffect());
                this.<>f__this.RebuildUnitData();
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.<>f__this.mKeepWindowLocked = 0;
                this.$current = null;
                this.$PC = 4;
                goto Label_02A3;
            Label_029A:
                this.$PC = -1;
            Label_02A1:
                return 0;
            Label_02A3:
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
        private sealed class <PostEquip>c__Iterator14C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal EquipData[] <old_equipments>__0;
            internal bool[] <is_old_equipments>__1;
            internal int <i>__2;
            internal GameManager <gm>__3;
            internal EquipData[] <currentEquipments>__4;
            internal BaseStatus <status>__5;
            internal int <i>__6;
            internal bool <is_master>__7;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <PostEquip>c__Iterator14C()
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

            public unsafe bool MoveNext()
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
                        goto Label_007D;

                    case 2:
                        goto Label_031A;

                    case 3:
                        goto Label_034D;
                }
                goto Label_036A;
            Label_0029:
                MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
                MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
                MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
                if ((this.<>f__this.mEquipmentWindow != null) == null)
                {
                    goto Label_0097;
                }
                goto Label_007D;
            Label_006A:
                this.$current = null;
                this.$PC = 1;
                goto Label_036C;
            Label_007D:
                if (this.<>f__this.mEquipmentWindow.GetComponent<WindowController>().IsOpened != null)
                {
                    goto Label_006A;
                }
            Label_0097:
                this.<old_equipments>__0 = this.<>f__this.mCurrentUnit.GetRankupEquips(this.<>f__this.mCurrentUnit.JobIndex);
                this.<is_old_equipments>__1 = new bool[(int) this.<old_equipments>__0.Length];
                this.<i>__2 = 0;
                goto Label_0130;
            Label_00DC:
                if (this.<old_equipments>__0[this.<i>__2].IsValid() != null)
                {
                    goto Label_00F8;
                }
                goto Label_0122;
            Label_00F8:
                if (this.<old_equipments>__0[this.<i>__2].IsEquiped() != null)
                {
                    goto Label_0114;
                }
                goto Label_0122;
            Label_0114:
                this.<is_old_equipments>__1[this.<i>__2] = 1;
            Label_0122:
                this.<i>__2 += 1;
            Label_0130:
                if (this.<i>__2 < ((int) this.<old_equipments>__0.Length))
                {
                    goto Label_00DC;
                }
                this.<>f__this.BeginStatusChangeCheck();
                this.<>f__this.RebuildUnitData();
                this.<gm>__3 = MonoSingleton<GameManager>.GetInstanceDirect();
                if (this.<>f__this.mIsJobLvMaxAllEquip != null)
                {
                    goto Label_0195;
                }
                this.<gm>__3.Player.OnSoubiSet(this.<>f__this.mCurrentUnit.UnitID, 1);
            Label_0195:
                this.<currentEquipments>__4 = this.<>f__this.mCurrentUnit.GetRankupEquips(this.<>f__this.mCurrentUnit.JobIndex);
                this.<>f__this.RefreshEquipments(-1);
                this.<>f__this.UpdateJobRankUpButtonState();
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_01E7;
                }
                goto Label_036A;
            Label_01E7:
                this.<status>__5 = new BaseStatus();
                this.<>f__this.mCurrentUnit.CalcStatus(this.<>f__this.mCurrentUnit.Lv, this.<>f__this.mSelectedJobIndex, &this.<status>__5, this.<>f__this.mSelectedJobIndex);
                this.<status>__5.CopyTo(this.<>f__this.mCurrentUnit.Status);
                this.<i>__6 = 0;
                goto Label_029D;
            Label_0255:
                if (this.<is_old_equipments>__1[this.<i>__6] != null)
                {
                    goto Label_028F;
                }
                if (this.<currentEquipments>__4[this.<i>__6].IsEquiped() == null)
                {
                    goto Label_028F;
                }
                this.<>f__this.SpawnEquipEffect(this.<i>__6);
            Label_028F:
                this.<i>__6 += 1;
            Label_029D:
                if (this.<i>__6 < ((int) this.<is_old_equipments>__1.Length))
                {
                    goto Label_0255;
                }
                this.<is_master>__7 = this.<>f__this.mCurrentUnit.Jobs[this.<>f__this.mSelectedJobIndex].CheckJobMaster();
                this.<>f__this.SpawnStatusChangeEffects();
                this.<>f__this.RefreshBasicParameters(this.<is_master>__7);
                this.<>f__this.PlayReaction();
                this.$current = new WaitForSeconds(0.5f);
                this.$PC = 2;
                goto Label_036C;
            Label_031A:
                if (this.<is_master>__7 == null)
                {
                    goto Label_0352;
                }
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.PostJobMaster());
                this.$PC = 3;
                goto Label_036C;
            Label_034D:
                goto Label_0363;
            Label_0352:
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
            Label_0363:
                this.$PC = -1;
            Label_036A:
                return 0;
            Label_036C:
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
        private sealed class <PostJobChange>c__Iterator150 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal LoadRequest <req>__1;
            internal UnitJobClassChange <param>__2;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <PostJobChange>c__Iterator150()
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
                        goto Label_002D;

                    case 1:
                        goto Label_0045;

                    case 2:
                        goto Label_00B4;

                    case 3:
                        goto Label_015B;

                    case 4:
                        goto Label_0223;
                }
                goto Label_022A;
            Label_002D:
                goto Label_0045;
            Label_0032:
                this.$current = null;
                this.$PC = 1;
                goto Label_022C;
            Label_0045:
                if (this.<>f__this.mJobChangeRequestSent == null)
                {
                    goto Label_0032;
                }
                this.<go>__0 = null;
                if (string.IsNullOrEmpty(this.<>f__this.Prefab_JobChange) != null)
                {
                    goto Label_015B;
                }
                this.<req>__1 = AssetManager.LoadAsync(this.<>f__this.Prefab_JobChange);
                if (this.<req>__1.isDone != null)
                {
                    goto Label_00B4;
                }
                this.$current = this.<req>__1.StartCoroutine();
                this.$PC = 2;
                goto Label_022C;
            Label_00B4:
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_00C9;
                }
                goto Label_022A;
            Label_00C9:
                this.<go>__0 = Object.Instantiate(this.<req>__1.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__0);
                this.<param>__2 = this.<go>__0.GetComponentInChildren<UnitJobClassChange>();
                if ((this.<param>__2 != null) == null)
                {
                    goto Label_015B;
                }
                this.<param>__2.PrevJobID = this.<>f__this.mPrevJobID;
                this.<param>__2.NextJobID = this.<>f__this.mNextJobID;
                goto Label_015B;
            Label_0148:
                this.$current = null;
                this.$PC = 3;
                goto Label_022C;
            Label_015B:
                if ((this.<go>__0 != null) != null)
                {
                    goto Label_0148;
                }
                this.<>f__this.mCurrentJobUniqueID = this.<>f__this.mCurrentUnit.CurrentJob.UniqueID;
                this.<>f__this.RebuildUnitData();
                this.<>f__this.RefreshBasicParameters(0);
                this.<>f__this.RefreshEquipments(-1);
                this.<>f__this.RefreshJobInfo();
                this.<>f__this.RefreshJobIcons(0);
                this.<>f__this.RefreshAbilitySlots(0);
                this.<>f__this.UpdateJobChangeButtonState();
                this.<>f__this.UpdateJobRankUpButtonState();
                this.<>f__this.DestroyOverlay();
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.<>f__this.mKeepWindowLocked = 0;
                this.$current = null;
                this.$PC = 4;
                goto Label_022C;
            Label_0223:
                this.$PC = -1;
            Label_022A:
                return 0;
            Label_022C:
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
        private sealed class <PostJobMaster>c__Iterator14D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal BaseStatus <old_status>__1;
            internal LoadRequest <req>__2;
            internal UnitJobMasterWindow <w>__3;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <PostJobMaster>c__Iterator14D()
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

            public unsafe bool MoveNext()
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
                        goto Label_00E5;

                    case 2:
                        goto Label_01B2;

                    case 3:
                        goto Label_022C;
                }
                goto Label_0233;
            Label_0029:
                this.<go>__0 = null;
                this.<old_status>__1 = new BaseStatus();
                this.<>f__this.mCurrentUnit.CalcStatus(this.<>f__this.mCurrentUnit.Lv, this.<>f__this.mSelectedJobIndex, &this.<old_status>__1, this.<>f__this.mSelectedJobIndex);
                this.<>f__this.BeginStatusChangeCheck();
                this.<>f__this.RebuildUnitData();
                if (string.IsNullOrEmpty(this.<>f__this.Prefab_JobMaster) != null)
                {
                    goto Label_01C3;
                }
                this.<req>__2 = AssetManager.LoadAsync(this.<>f__this.Prefab_JobMaster);
                if (this.<req>__2.isDone != null)
                {
                    goto Label_00E5;
                }
                this.$current = this.<req>__2.StartCoroutine();
                this.$PC = 1;
                goto Label_0235;
            Label_00E5:
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_00FA;
                }
                goto Label_0233;
            Label_00FA:
                this.<go>__0 = Object.Instantiate(this.<req>__2.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__0);
                this.<w>__3 = this.<go>__0.GetComponentInChildren<UnitJobMasterWindow>();
                if ((this.<w>__3 != null) == null)
                {
                    goto Label_0169;
                }
                this.<w>__3.Refresh(this.<old_status>__1, this.<>f__this.mCurrentUnit.Status);
            Label_0169:
                if ((this.<>f__this.mBGAnimator != null) == null)
                {
                    goto Label_01B2;
                }
                this.<>f__this.mBGAnimator.SetTrigger(this.<>f__this.BGAnimatorPlayTrigger);
                goto Label_01B2;
            Label_019F:
                this.$current = null;
                this.$PC = 2;
                goto Label_0235;
            Label_01B2:
                if ((this.<go>__0 != null) != null)
                {
                    goto Label_019F;
                }
            Label_01C3:
                this.<>f__this.SpawnStatusChangeEffects();
                this.<>f__this.RefreshBasicParameters(0);
                this.<>f__this.RefreshJobInfo();
                this.<>f__this.RefreshJobIcons(0);
                this.<>f__this.DestroyOverlay();
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.<>f__this.mKeepWindowLocked = 0;
                this.$current = null;
                this.$PC = 3;
                goto Label_0235;
            Label_022C:
                this.$PC = -1;
            Label_0233:
                return 0;
            Label_0235:
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
        private sealed class <PostJobRankUp>c__Iterator151 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool <isJobLvUpIkkatsu>__0;
            internal List<AbilityData> <oldAbilities>__1;
            internal List<ItemData> <returnItems>__2;
            internal bool <isClassChange>__3;
            internal LoadRequest <reqNewAbilityList>__4;
            internal LoadRequest <reqRankUpEffect>__5;
            internal LoadRequest <reqReturnItems>__6;
            internal LoadRequest <reqJobMasterEffect>__7;
            internal JobParam <jpNext>__8;
            internal JobParam <jpPrev>__9;
            internal long <jobUniqueID>__10;
            internal UnitData <cacheUnitData>__11;
            internal int <i>__12;
            internal int target_rank;
            internal string <cueName>__13;
            internal GameManager <gm>__14;
            internal PlayerData <player>__15;
            internal int <current_rank>__16;
            internal JobData <baseJob>__17;
            internal int <baseJobIndex>__18;
            internal int <i>__19;
            internal JobSetParam <jobset>__20;
            internal int <is_equip>__21;
            internal bool can_jobmaster;
            internal bool can_jobmax;
            internal List<AbilityData> <newAbilities>__22;
            internal int <i>__23;
            internal UnitData <unit>__24;
            internal JobData <job>__25;
            internal int <i>__26;
            internal GameObject <go>__27;
            internal UnitJobClassChange <param>__28;
            internal UnitData <src>__29;
            internal BaseStatus <old_status>__30;
            internal UnitData <prev>__31;
            internal BaseStatus <new_status>__32;
            internal GameObject <go>__33;
            internal UnitJobMasterWindow <w>__34;
            internal GameObject <go>__35;
            internal UnitLearnAbilityWindow <w>__36;
            internal GameObject <go>__37;
            internal ReturnItemWindow <w>__38;
            internal bool <jobExists>__39;
            internal int <i>__40;
            internal GameObject <eff>__41;
            internal int $PC;
            internal object $current;
            internal int <$>target_rank;
            internal bool <$>can_jobmaster;
            internal bool <$>can_jobmax;
            internal UnitEnhanceV3 <>f__this;

            public <PostJobRankUp>c__Iterator151()
            {
                base..ctor();
                return;
            }

            internal bool <>m__457(AbilityData v)
            {
                return (v.UniqueID == this.<newAbilities>__22[this.<i>__23].UniqueID);
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                JobParam param;
                int num2;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_004D;

                    case 1:
                        goto Label_06B8;

                    case 2:
                        goto Label_0838;

                    case 3:
                        goto Label_0870;

                    case 4:
                        goto Label_08A8;

                    case 5:
                        goto Label_08E0;

                    case 6:
                        goto Label_08F8;

                    case 7:
                        goto Label_09B3;

                    case 8:
                        goto Label_0B7A;

                    case 9:
                        goto Label_0D20;

                    case 10:
                        goto Label_0DD9;

                    case 11:
                        goto Label_0E82;

                    case 12:
                        goto Label_10A1;
                }
                goto Label_10A8;
            Label_004D:
                this.<isJobLvUpIkkatsu>__0 = this.<>f__this.mIsJobLvUpAllEquip;
                this.<>f__this.mIsJobLvUpAllEquip = 0;
                this.<>f__this.mChQuestProg = null;
                if (this.<>f__this.mCurrentUnit.IsOpenCharacterQuest() == null)
                {
                    goto Label_00A6;
                }
                this.<>f__this.mChQuestProg = this.<>f__this.mCurrentUnit.SaveUnlockProgress();
            Label_00A6:
                this.<oldAbilities>__1 = this.<>f__this.mCurrentUnit.GetAllLearnedAbilities(0);
                this.<returnItems>__2 = null;
                this.<isClassChange>__3 = (this.<>f__this.mCurrentUnit.JobIndex < this.<>f__this.mCurrentUnit.NumJobsAvailable) == 0;
                this.<reqNewAbilityList>__4 = null;
                this.<reqRankUpEffect>__5 = null;
                this.<reqReturnItems>__6 = null;
                this.<reqJobMasterEffect>__7 = null;
                this.<jpNext>__8 = null;
                this.<jpPrev>__9 = null;
                this.<jobUniqueID>__10 = this.<>f__this.mCurrentUnit.CurrentJob.UniqueID;
                this.<cacheUnitData>__11 = new UnitData();
                if (this.<isClassChange>__3 != null)
                {
                    goto Label_01BC;
                }
                this.<cacheUnitData>__11.Setup(this.<>f__this.mCurrentUnit);
                this.<i>__12 = this.<cacheUnitData>__11.Jobs[this.<>f__this.mSelectedJobIndex].Rank;
                goto Label_01AB;
            Label_0187:
                this.<cacheUnitData>__11.JobRankUp(this.<>f__this.mSelectedJobIndex);
                this.<i>__12 += 1;
            Label_01AB:
                if (this.<i>__12 < this.target_rank)
                {
                    goto Label_0187;
                }
            Label_01BC:
                this.<>f__this.FadeUnitImage(1f, 0f, 0.5f);
                this.<>f__this.BeginStatusChangeCheck();
                this.<cueName>__13 = null;
                this.<gm>__14 = MonoSingleton<GameManager>.GetInstanceDirect();
                this.<player>__15 = this.<gm>__14.Player;
                this.<current_rank>__16 = this.<>f__this.mCurrentUnit.CurrentJob.Rank;
                if (this.<isClassChange>__3 == null)
                {
                    goto Label_03AB;
                }
                this.<baseJob>__17 = this.<>f__this.mCurrentUnit.GetBaseJob(this.<>f__this.mCurrentUnit.CurrentJob.Param.iname);
                this.<jpPrev>__9 = this.<baseJob>__17.Param;
                this.<jpNext>__8 = this.<>f__this.mCurrentUnit.CurrentJob.Param;
                this.<baseJobIndex>__18 = this.<>f__this.mCurrentUnit.JobIndex;
                this.<i>__19 = 0;
                goto Label_0319;
            Label_02A8:
                if ((this.<>f__this.mCurrentUnit.Jobs[this.<i>__19] == null) || ((this.<>f__this.mCurrentUnit.Jobs[this.<i>__19].Param.iname == this.<jpPrev>__9.iname) == null))
                {
                    goto Label_030B;
                }
                this.<baseJobIndex>__18 = this.<i>__19;
                goto Label_0336;
            Label_030B:
                this.<i>__19 += 1;
            Label_0319:
                if (this.<i>__19 < ((int) this.<>f__this.mCurrentUnit.Jobs.Length))
                {
                    goto Label_02A8;
                }
            Label_0336:
                this.<returnItems>__2 = this.<player>__15.GetJobRankUpReturnItemData(this.<>f__this.mCurrentUnit, this.<baseJobIndex>__18, this.<isJobLvUpIkkatsu>__0);
                this.<player>__15.ClassChangeUnit(this.<>f__this.mCurrentUnit, this.<>f__this.mCurrentUnit.JobIndex);
                this.<reqRankUpEffect>__5 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_JobCC);
                this.<cueName>__13 = "chara_0020";
                goto Label_048A;
            Label_03AB:
                this.<returnItems>__2 = this.<player>__15.GetJobRankUpReturnItemData(this.<>f__this.mCurrentUnit, this.<>f__this.mCurrentUnit.JobIndex, this.<isJobLvUpIkkatsu>__0);
                this.<player>__15.JobRankUpUnit(this.<>f__this.mCurrentUnit, this.<>f__this.mCurrentUnit.JobIndex);
                if (this.<>f__this.mCurrentUnit.CurrentJob.Rank != 1)
                {
                    goto Label_0445;
                }
                this.<reqRankUpEffect>__5 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_JobUnlock);
                this.<cueName>__13 = "chara_0019";
                goto Label_0466;
            Label_0445:
                this.<reqRankUpEffect>__5 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_JobRankUp);
                this.<cueName>__13 = "chara_0018";
            Label_0466:
                this.<jpPrev>__9 = this.<jpNext>__8 = this.<>f__this.mCurrentUnit.CurrentJob.Param;
            Label_048A:
                this.<>f__this.mJobRankUpRequestSent = 0;
                if (Network.Mode != null)
                {
                    goto Label_0694;
                }
                if (this.<isJobLvUpIkkatsu>__0 == null)
                {
                    goto Label_064A;
                }
                this.<jobset>__20 = this.<>f__this.mCurrentUnit.GetJobSetParam(this.<>f__this.mCurrentUnit.JobIndex);
                this.<is_equip>__21 = 0;
                if (this.can_jobmaster == null)
                {
                    goto Label_04EF;
                }
                this.<is_equip>__21 = 1;
                goto Label_0575;
            Label_04EF:
                if (((this.target_rank <= 0) || (this.<>f__this.mCurrentUnit.GetJobRankCap() == JobParam.MAX_JOB_RANK)) || (this.<>f__this.mCurrentUnit.GetJobRankCap() != this.target_rank))
                {
                    goto Label_0575;
                }
                if (this.<>f__this.IsBulk == null)
                {
                    goto Label_055D;
                }
                this.<is_equip>__21 = (this.can_jobmax == null) ? 0 : 1;
                goto Label_0575;
            Label_055D:
                if (this.<current_rank>__16 != this.target_rank)
                {
                    goto Label_0575;
                }
                this.<is_equip>__21 = 1;
            Label_0575:
                if (this.<jobset>__20 == null)
                {
                    goto Label_06B8;
                }
                if (this.<is_equip>__21 != 1)
                {
                    goto Label_05E5;
                }
                Network.RequestAPI(new ReqJobRankupAll(this.<>f__this.mCurrentUnit.UniqueID, this.<jobset>__20.iname, this.<>f__this.mIsCommon, this.<current_rank>__16, this.target_rank, this.<is_equip>__21, new Network.ResponseCallback(this.<>f__this.OnEquipAllResult)), 0);
                goto Label_0639;
            Label_05E5:
                Network.RequestAPI(new ReqJobRankupAll(this.<>f__this.mCurrentUnit.UniqueID, this.<jobset>__20.iname, this.<>f__this.mIsCommon, this.<current_rank>__16, this.target_rank, this.<is_equip>__21, new Network.ResponseCallback(this.<>f__this.OnJobRankUpResult)), 0);
            Label_0639:
                this.<>f__this.SetIsCommon(0);
                goto Label_068F;
            Label_064A:
                Network.RequestAPI(new ReqJobRankup(this.<jobUniqueID>__10, new Network.ResponseCallback(this.<>f__this.OnJobRankUpResult)), 0);
                this.target_rank = Mathf.Min(this.<>f__this.mCurrentUnit.GetJobRankCap(), this.<current_rank>__16 + 1);
            Label_068F:
                goto Label_06A0;
            Label_0694:
                this.<>f__this.mJobRankUpRequestSent = 1;
            Label_06A0:
                goto Label_06B8;
            Label_06A5:
                this.$current = null;
                this.$PC = 1;
                goto Label_10AA;
            Label_06B8:
                if (this.<>f__this.mJobRankUpRequestSent == null)
                {
                    goto Label_06A5;
                }
                this.<>f__this.SetUnitDirty();
                if (this.can_jobmaster == null)
                {
                    goto Label_06F4;
                }
                this.<reqJobMasterEffect>__7 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_JobMaster);
            Label_06F4:
                this.<newAbilities>__22 = (this.<isClassChange>__3 == null) ? this.<cacheUnitData>__11.GetAllLearnedAbilities(0) : this.<>f__this.mCurrentUnit.GetAllLearnedAbilities(0);
                this.<i>__23 = 0;
                goto Label_0779;
            Label_0733:
                if (this.<oldAbilities>__1.Find(new Predicate<AbilityData>(this.<>m__457)) == null)
                {
                    goto Label_076B;
                }
                this.<newAbilities>__22.RemoveAt(this.<i>__23--);
            Label_076B:
                this.<i>__23 += 1;
            Label_0779:
                if (this.<i>__23 < this.<newAbilities>__22.Count)
                {
                    goto Label_0733;
                }
                this.<>f__this.mAbilitySlotDirty = 1;
                this.<>f__this.mAbilityListDirty = 1;
                if (this.<newAbilities>__22.Count <= 0)
                {
                    goto Label_07CE;
                }
                this.<reqNewAbilityList>__4 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_NewAbilityList);
            Label_07CE:
                if ((this.<returnItems>__2 == null) || (this.<returnItems>__2.Count <= 0))
                {
                    goto Label_0800;
                }
                this.<reqReturnItems>__6 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_ReturnItems);
            Label_0800:
                if ((this.<reqRankUpEffect>__5 == null) || (this.<reqRankUpEffect>__5.isDone != null))
                {
                    goto Label_0838;
                }
                this.$current = this.<reqRankUpEffect>__5.StartCoroutine();
                this.$PC = 2;
                goto Label_10AA;
            Label_0838:
                if ((this.<reqNewAbilityList>__4 == null) || (this.<reqNewAbilityList>__4.isDone != null))
                {
                    goto Label_0870;
                }
                this.$current = this.<reqNewAbilityList>__4.StartCoroutine();
                this.$PC = 3;
                goto Label_10AA;
            Label_0870:
                if ((this.<reqReturnItems>__6 == null) || (this.<reqReturnItems>__6.isDone != null))
                {
                    goto Label_08A8;
                }
                this.$current = this.<reqReturnItems>__6.StartCoroutine();
                this.$PC = 4;
                goto Label_10AA;
            Label_08A8:
                if ((this.<reqJobMasterEffect>__7 == null) || (this.<reqJobMasterEffect>__7.isDone != null))
                {
                    goto Label_08F8;
                }
                this.$current = this.<reqJobMasterEffect>__7.StartCoroutine();
                this.$PC = 5;
                goto Label_10AA;
            Label_08E0:
                goto Label_08F8;
            Label_08E5:
                this.$current = null;
                this.$PC = 6;
                goto Label_10AA;
            Label_08F8:
                if (this.<>f__this.IsUnitImageFading != null)
                {
                    goto Label_08E5;
                }
                if ((this.<>f__this.mBGAnimator != null) == null)
                {
                    goto Label_0939;
                }
                this.<>f__this.mBGAnimator.SetTrigger(this.<>f__this.BGAnimatorPlayTrigger);
            Label_0939:
                this.<>f__this.PlayReaction();
                this.<>f__this.PlayUnitVoice(this.<cueName>__13);
                if (this.<reqRankUpEffect>__5 == null)
                {
                    goto Label_0B8B;
                }
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_0975;
                }
                goto Label_10A8;
            Label_0975:
                if ((this.<isJobLvUpIkkatsu>__0 == null) || (this.<>f__this.mCurrentUnit.CurrentJob.Rank != 1))
                {
                    goto Label_0AA4;
                }
                goto Label_09B3;
            Label_09A0:
                this.$current = null;
                this.$PC = 7;
                goto Label_10AA;
            Label_09B3:
                if (this.<>f__this.mJobRankUpRequestSent == null)
                {
                    goto Label_09A0;
                }
                this.<unit>__24 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
                if (this.<unit>__24 == null)
                {
                    goto Label_0AA4;
                }
                this.<job>__25 = null;
                this.<i>__26 = 0;
                goto Label_0A5C;
            Label_0A00:
                if ((this.<unit>__24.Jobs[this.<i>__26].Param.iname == this.<jpNext>__8.iname) == null)
                {
                    goto Label_0A4E;
                }
                this.<job>__25 = this.<unit>__24.Jobs[this.<i>__26];
                goto Label_0A74;
            Label_0A4E:
                this.<i>__26 += 1;
            Label_0A5C:
                if (this.<i>__26 < ((int) this.<unit>__24.Jobs.Length))
                {
                    goto Label_0A00;
                }
            Label_0A74:
                if ((this.<job>__25 == null) || (this.<job>__25.IsActivated == null))
                {
                    goto Label_0AA4;
                }
                GlobalVars.SelectedJobUniqueID.Set(this.<job>__25.UniqueID);
            Label_0AA4:
                this.<go>__27 = Object.Instantiate(this.<reqRankUpEffect>__5.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__27);
                this.<param>__28 = this.<go>__27.GetComponentInChildren<UnitJobClassChange>();
                if ((this.<param>__28 != null) == null)
                {
                    goto Label_0B7A;
                }
                this.<param>__28.PrevJobID = (this.<jpPrev>__9 == null) ? null : this.<jpPrev>__9.iname;
                this.<param>__28.NextJobID = (this.<jpNext>__8 == null) ? null : this.<jpNext>__8.iname;
                this.<param>__28.CurrentJobRank = this.<current_rank>__16;
                this.<param>__28.NextJobRank = this.target_rank;
                goto Label_0B7A;
            Label_0B67:
                this.$current = null;
                this.$PC = 8;
                goto Label_10AA;
            Label_0B7A:
                if ((this.<go>__27 != null) != null)
                {
                    goto Label_0B67;
                }
            Label_0B8B:
                this.<>f__this.FadeUnitImage(0f, 1f, 1f);
                if (this.<reqJobMasterEffect>__7 == null)
                {
                    goto Label_0D31;
                }
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_0BC5;
                }
                goto Label_10A8;
            Label_0BC5:
                this.<src>__29 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.<>f__this.mCurrentUnitID);
                this.<old_status>__30 = new BaseStatus();
                this.<prev>__31 = new UnitData();
                this.<prev>__31.Setup(this.<src>__29);
                this.<prev>__31.CalcStatus(this.<prev>__31.Lv, this.<>f__this.mSelectedJobIndex, &this.<old_status>__30, this.<>f__this.mSelectedJobIndex);
                this.<new_status>__32 = new BaseStatus();
                this.<src>__29.CalcStatus(this.<src>__29.Lv, this.<>f__this.mSelectedJobIndex, &this.<new_status>__32, -1);
                this.<go>__33 = Object.Instantiate(this.<reqJobMasterEffect>__7.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__33);
                this.<w>__34 = this.<go>__33.GetComponentInChildren<UnitJobMasterWindow>();
                if ((this.<w>__34 != null) == null)
                {
                    goto Label_0CD6;
                }
                this.<w>__34.Refresh(this.<old_status>__30, this.<new_status>__32);
            Label_0CD6:
                if ((this.<>f__this.mBGAnimator != null) == null)
                {
                    goto Label_0D20;
                }
                this.<>f__this.mBGAnimator.SetTrigger(this.<>f__this.BGAnimatorPlayTrigger);
                goto Label_0D20;
            Label_0D0C:
                this.$current = null;
                this.$PC = 9;
                goto Label_10AA;
            Label_0D20:
                if ((this.<go>__33 != null) != null)
                {
                    goto Label_0D0C;
                }
            Label_0D31:
                if (this.<reqNewAbilityList>__4 == null)
                {
                    goto Label_0DEA;
                }
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_0D51;
                }
                goto Label_10A8;
            Label_0D51:
                this.<go>__35 = Object.Instantiate(this.<reqNewAbilityList>__4.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__35);
                this.<>f__this.PlayUnitVoice("chara_0023");
                this.<w>__36 = this.<go>__35.GetComponentInChildren<UnitLearnAbilityWindow>();
                if ((this.<w>__36 != null) == null)
                {
                    goto Label_0DD9;
                }
                this.<w>__36.AbilityList = this.<newAbilities>__22;
                goto Label_0DD9;
            Label_0DC5:
                this.$current = null;
                this.$PC = 10;
                goto Label_10AA;
            Label_0DD9:
                if ((this.<go>__35 != null) != null)
                {
                    goto Label_0DC5;
                }
            Label_0DEA:
                if (this.<reqReturnItems>__6 == null)
                {
                    goto Label_0E93;
                }
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_0E0A;
                }
                goto Label_10A8;
            Label_0E0A:
                this.<go>__37 = Object.Instantiate(this.<reqReturnItems>__6.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__37);
                this.<w>__38 = this.<go>__37.GetComponentInChildren<ReturnItemWindow>();
                if ((this.<w>__38 != null) == null)
                {
                    goto Label_0E82;
                }
                this.<w>__38.ReturnItems = this.<returnItems>__2;
                goto Label_0E82;
            Label_0E6E:
                this.$current = null;
                this.$PC = 11;
                goto Label_10AA;
            Label_0E82:
                if ((this.<go>__37 != null) != null)
                {
                    goto Label_0E6E;
                }
            Label_0E93:
                this.<>f__this.SpawnStatusChangeEffects();
                this.<>f__this.RebuildUnitData();
                this.<jobExists>__39 = 0;
                this.<i>__40 = 0;
                goto Label_0F02;
            Label_0EBC:
                if (this.<>f__this.mCurrentUnit.Jobs[this.<i>__40].UniqueID != this.<>f__this.mCurrentJobUniqueID)
                {
                    goto Label_0EF4;
                }
                this.<jobExists>__39 = 1;
                goto Label_0F1F;
            Label_0EF4:
                this.<i>__40 += 1;
            Label_0F02:
                if (this.<i>__40 < ((int) this.<>f__this.mCurrentUnit.Jobs.Length))
                {
                    goto Label_0EBC;
                }
            Label_0F1F:
                if (this.<jobExists>__39 != null)
                {
                    goto Label_0F4A;
                }
                this.<>f__this.mCurrentJobUniqueID = this.<>f__this.mCurrentUnit.CurrentJob.UniqueID;
            Label_0F4A:
                this.<>f__this.RefreshEquipments(-1);
                this.<>f__this.RefreshJobInfo();
                this.<>f__this.RefreshJobIcons(0);
                this.<>f__this.RefreshAbilitySlots(0);
                this.<>f__this.RefreshBasicParameters(0);
                this.<>f__this.UpdateJobRankUpButtonState();
                this.<>f__this.UpdateJobChangeButtonState();
                this.<>f__this.RefreshAbilitySlotButtonState();
                if ((this.<>f__this.JobRank != null) == null)
                {
                    goto Label_1009;
                }
                if ((this.<>f__this.JobRankUpEffect != null) == null)
                {
                    goto Label_1009;
                }
                this.<eff>__41 = Object.Instantiate<GameObject>(this.<>f__this.JobRankUpEffect);
                this.<eff>__41.get_transform().SetParent(this.<>f__this.JobRank.get_transform(), 0);
            Label_1009:
                this.<>f__this.DestroyOverlay();
                if (this.<>f__this.mChQuestProg == null)
                {
                    goto Label_104F;
                }
                if (this.<>f__this.mCurrentUnit.IsQuestUnlocked(this.<>f__this.mChQuestProg) == null)
                {
                    goto Label_104F;
                }
                this.<>f__this.OpenCharacterQuestPopupInternal();
            Label_104F:
                this.<>f__this.mChQuestProg = null;
                MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(0);
                this.<>f__this.CheckPlayBackUnlock();
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                CriticalSection.Leave(2);
                this.$current = null;
                this.$PC = 12;
                goto Label_10AA;
            Label_10A1:
                this.$PC = -1;
            Label_10A8:
                return 0;
            Label_10AA:
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
        private sealed class <PostUnitEvolution>c__Iterator15B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <go>__1;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <PostUnitEvolution>c__Iterator15B()
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
                        goto Label_0031;

                    case 1:
                        goto Label_0060;

                    case 2:
                        goto Label_0078;

                    case 3:
                        goto Label_00E0;

                    case 4:
                        goto Label_0154;

                    case 5:
                        goto Label_02A1;
                }
                goto Label_02A8;
            Label_0031:
                this.<>f__this.GetComponent<WindowController>().SetCollision(0);
                this.$current = this.<>f__this.WaitForKyokaRequestAsync(0);
                this.$PC = 1;
                goto Label_02AA;
            Label_0060:
                goto Label_0078;
            Label_0065:
                this.$current = null;
                this.$PC = 2;
                goto Label_02AA;
            Label_0078:
                if (this.<>f__this.mEvolutionRequestSent == null)
                {
                    goto Label_0065;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Prefab_Evolution) != null)
                {
                    goto Label_0165;
                }
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_Evolution);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_00E0;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 3;
                goto Label_02AA;
            Label_00E0:
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_00F5;
                }
                goto Label_02A8;
            Label_00F5:
                this.<go>__1 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__1);
                this.<>f__this.PlayReaction();
                this.<>f__this.PlayUnitVoice("chara_0022");
                goto Label_0154;
            Label_0141:
                this.$current = null;
                this.$PC = 4;
                goto Label_02AA;
            Label_0154:
                if ((this.<go>__1 != null) != null)
                {
                    goto Label_0141;
                }
            Label_0165:
                this.<>f__this.RebuildUnitData();
                this.<>f__this.RefreshEXPImmediate();
                this.<>f__this.RefreshExpInfo();
                this.<>f__this.RefreshLevelCap();
                this.<>f__this.UpdateUnitKakuseiButtonState();
                this.<>f__this.UpdateUnitEvolutionButtonState(0);
                this.<>f__this.UpdateUnitTobiraButtonState();
                if ((this.<>f__this.UnitInfo != null) == null)
                {
                    goto Label_01FF;
                }
                if ((this.<>f__this.UnitParamInfo != null) == null)
                {
                    goto Label_01FF;
                }
                GameParameter.UpdateAll(this.<>f__this.UnitInfo);
                GameParameter.UpdateAll(this.<>f__this.UnitParamInfo);
            Label_01FF:
                if (this.<>f__this.mCurrentUnit.IsOpenCharacterQuest() == null)
                {
                    goto Label_0272;
                }
                this.<>f__this.CharaQuestButton.get_gameObject().SetActive(1);
                if (this.<>f__this.mCurrentUnit.OpenCharacterQuestOnRarityUp(this.<>f__this.mCurrentUnit.Rarity - 1) == null)
                {
                    goto Label_0272;
                }
                this.<>f__this.CharaQuestButton.get_gameObject().SetActive(1);
                this.<>f__this.OpenCharacterQuestPopupInternal();
            Label_0272:
                this.<>f__this.DestroyOverlay();
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.$current = null;
                this.$PC = 5;
                goto Label_02AA;
            Label_02A1:
                this.$PC = -1;
            Label_02A8:
                return 0;
            Label_02AA:
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
        private sealed class <PostUnitKakusei>c__Iterator159 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <go>__1;
            internal UnitKakusei <kakusei>__2;
            internal List<JobParam> <cache_jobs>__3;
            internal JobSetParam[] <cc_jobs>__4;
            internal int <i>__5;
            internal JobSetParam <jobset>__6;
            internal bool <isCCJob>__7;
            internal int <j>__8;
            internal int <i>__9;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <PostUnitKakusei>c__Iterator159()
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
                        goto Label_0031;

                    case 1:
                        goto Label_004F;

                    case 2:
                        goto Label_0067;

                    case 3:
                        goto Label_00CF;

                    case 4:
                        goto Label_0395;

                    case 5:
                        goto Label_0541;
                }
                goto Label_0548;
            Label_0031:
                this.$current = this.<>f__this.WaitForKyokaRequestAsync(0);
                this.$PC = 1;
                goto Label_054A;
            Label_004F:
                goto Label_0067;
            Label_0054:
                this.$current = null;
                this.$PC = 2;
                goto Label_054A;
            Label_0067:
                if (this.<>f__this.mKakuseiRequestSent == null)
                {
                    goto Label_0054;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Prefab_Kakusei) != null)
                {
                    goto Label_03A6;
                }
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_Kakusei);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_00CF;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 3;
                goto Label_054A;
            Label_00CF:
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_00E4;
                }
                goto Label_0548;
            Label_00E4:
                this.<go>__1 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__1);
                this.<kakusei>__2 = this.<go>__1.GetComponent<UnitKakusei>();
                if ((this.<kakusei>__2 != null) == null)
                {
                    goto Label_0325;
                }
                this.<cache_jobs>__3 = new List<JobParam>();
                this.<cc_jobs>__4 = MonoSingleton<GameManager>.Instance.MasterParam.GetClassChangeJobSetParam(this.<>f__this.mCurrentUnit.UnitParam.iname);
                this.<i>__5 = 0;
                goto Label_02BA;
            Label_0173:
                if (this.<i>__5 != null)
                {
                    goto Label_0183;
                }
                goto Label_02AC;
            Label_0183:
                if (this.<>f__this.mCurrentUnit.CheckJobUnlockable(this.<i>__5) == null)
                {
                    goto Label_01A3;
                }
                goto Label_02AC;
            Label_01A3:
                this.<jobset>__6 = this.<>f__this.mCurrentUnit.GetJobSetParam(this.<i>__5);
                if (this.<jobset>__6 != null)
                {
                    goto Label_01CF;
                }
                goto Label_02AC;
            Label_01CF:
                if (this.<cc_jobs>__4 == null)
                {
                    goto Label_025A;
                }
                if (((int) this.<cc_jobs>__4.Length) <= 0)
                {
                    goto Label_025A;
                }
                this.<isCCJob>__7 = 0;
                this.<j>__8 = 0;
                goto Label_0237;
            Label_01FB:
                if ((this.<cc_jobs>__4[this.<j>__8].iname == this.<jobset>__6.iname) == null)
                {
                    goto Label_0229;
                }
                this.<isCCJob>__7 = 1;
            Label_0229:
                this.<j>__8 += 1;
            Label_0237:
                if (this.<j>__8 < ((int) this.<cc_jobs>__4.Length))
                {
                    goto Label_01FB;
                }
                if (this.<isCCJob>__7 == null)
                {
                    goto Label_025A;
                }
                goto Label_02AC;
            Label_025A:
                if (this.<jobset>__6.lock_awakelv <= (this.<>f__this.mCurrentUnit.AwakeLv + this.<>f__this.mCacheAwakeLv))
                {
                    goto Label_028B;
                }
                goto Label_02AC;
            Label_028B:
                this.<cache_jobs>__3.Add(this.<>f__this.mCurrentUnit.GetJobParam(this.<i>__5));
            Label_02AC:
                this.<i>__5 += 1;
            Label_02BA:
                if (this.<i>__5 < ((int) this.<>f__this.mCurrentUnit.Jobs.Length))
                {
                    goto Label_0173;
                }
                this.<kakusei>__2.UnlockJobParams = this.<cache_jobs>__3.ToArray();
                this.<kakusei>__2.UpdateLevelValue = Mathf.Max(1, this.<>f__this.mCacheAwakeLv);
                this.<kakusei>__2.UpdateCombValue = Mathf.Max(1, this.<>f__this.mCacheAwakeLv);
            Label_0325:
                this.<>f__this.mCacheAwakeLv = 1;
                if ((this.<>f__this.mBGAnimator != null) == null)
                {
                    goto Label_0362;
                }
                this.<>f__this.mBGAnimator.SetTrigger(this.<>f__this.BGAnimatorPlayTrigger);
            Label_0362:
                this.<>f__this.PlayReaction();
                this.<>f__this.PlayUnitVoice("chara_0023");
                goto Label_0395;
            Label_0382:
                this.$current = null;
                this.$PC = 4;
                goto Label_054A;
            Label_0395:
                if ((this.<go>__1 != null) != null)
                {
                    goto Label_0382;
                }
            Label_03A6:
                this.<>f__this.SetCacheUnitData(this.<>f__this.mCurrentUnit, this.<>f__this.mSelectedJobIndex);
                this.<>f__this.RebuildUnitData();
                this.<>f__this.RefreshEXPImmediate();
                this.<>f__this.RefreshExpInfo();
                if (this.<>f__this.mUnlockJobParam == null)
                {
                    goto Label_0489;
                }
                this.<i>__9 = 0;
                goto Label_046C;
            Label_0404:
                if (this.<>f__this.mCurrentUnit.Jobs[this.<i>__9].Param != this.<>f__this.mUnlockJobParam)
                {
                    goto Label_045E;
                }
                this.<>f__this.ChangeJobSlot(this.<i>__9, null);
                this.<>f__this.TabChange(this.<>f__this.Tab_Equipments);
                goto Label_0489;
            Label_045E:
                this.<i>__9 += 1;
            Label_046C:
                if (this.<i>__9 < ((int) this.<>f__this.mCurrentUnit.Jobs.Length))
                {
                    goto Label_0404;
                }
            Label_0489:
                this.<>f__this.RefreshBasicParameters(0);
                this.<>f__this.RefreshJobInfo();
                this.<>f__this.RefreshJobIcons(0);
                this.<>f__this.RefreshEquipments(-1);
                this.<>f__this.RefreshAbilityList();
                this.<>f__this.RefreshAbilitySlots(0);
                this.<>f__this.UpdateJobRankUpButtonState();
                this.<>f__this.UpdateJobChangeButtonState();
                this.<>f__this.UpdateUnitKakuseiButtonState();
                this.<>f__this.UpdateUnitEvolutionButtonState(0);
                this.<>f__this.UpdateUnitTobiraButtonState();
                this.<>f__this.DestroyOverlay();
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.<>f__this.SetNotifyReleaseClassChangeJob();
                this.$current = null;
                this.$PC = 5;
                goto Label_054A;
            Label_0541:
                this.$PC = -1;
            Label_0548:
                return 0;
            Label_054A:
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
        private sealed class <PostUnitLevelUp>c__Iterator14E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <go>__1;
            internal int prevLv;
            internal int $PC;
            internal object $current;
            internal int <$>prevLv;
            internal UnitEnhanceV3 <>f__this;

            public <PostUnitLevelUp>c__Iterator14E()
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
                        goto Label_0039;

                    case 1:
                        goto Label_006A;

                    case 2:
                        goto Label_009D;

                    case 3:
                        goto Label_0105;

                    case 4:
                        goto Label_01AD;

                    case 5:
                        goto Label_023D;

                    case 6:
                        goto Label_0265;

                    case 7:
                        goto Label_02B0;
                }
                goto Label_02B7;
            Label_0039:
                this.<req>__0 = null;
                this.<go>__1 = null;
                this.<>f__this.CreateOverlay();
                goto Label_006A;
            Label_0057:
                this.$current = null;
                this.$PC = 1;
                goto Label_02B9;
            Label_006A:
                if (this.<>f__this.mExpStart < this.<>f__this.mExpEnd)
                {
                    goto Label_0057;
                }
                goto Label_009D;
            Label_008A:
                this.$current = null;
                this.$PC = 2;
                goto Label_02B9;
            Label_009D:
                if (this.<>f__this.mSendingKyokaRequest != null)
                {
                    goto Label_008A;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Prefab_LeveUp) != null)
                {
                    goto Label_0146;
                }
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_LeveUp);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0105;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 3;
                goto Label_02B9;
            Label_0105:
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_011A;
                }
                goto Label_02B7;
            Label_011A:
                this.<go>__1 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<go>__1);
            Label_0146:
                this.<>f__this.PlayUnitVoice("chara_0017");
                this.<>f__this.SpawnStatusChangeEffects();
                this.<>f__this.RefreshBasicParameters(0);
                this.<>f__this.mEquipmentPanelDirty = 1;
                this.<>f__this.mAbilityListDirty = 1;
                MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(0);
                goto Label_01AD;
            Label_019A:
                this.$current = null;
                this.$PC = 4;
                goto Label_02B9;
            Label_01AD:
                if ((this.<go>__1 != null) != null)
                {
                    goto Label_019A;
                }
                if (this.<>f__this.mCurrentUnit.IsOpenCharacterQuest() == null)
                {
                    goto Label_023D;
                }
                this.<>f__this.CharaQuestButton.get_gameObject().SetActive(1);
                if (this.<>f__this.mCurrentUnit.OpenCharacterQuestOnLevelUp(this.prevLv) != null)
                {
                    goto Label_0220;
                }
                if (this.<>f__this.mCurrentUnit.OpenNewCharacterEpisodeOnLevelUp(this.prevLv, null) == null)
                {
                    goto Label_023D;
                }
            Label_0220:
                this.$current = this.<>f__this.OpenCharacterQuestPopupInternalAsync();
                this.$PC = 5;
                goto Label_02B9;
            Label_023D:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.ShowUnlockSkillEffect());
                this.$PC = 6;
                goto Label_02B9;
            Label_0265:
                this.<>f__this.UpdateUnitEvolutionButtonState(0);
                this.<>f__this.UpdateUnitTobiraButtonState();
                this.<>f__this.DestroyOverlay();
                this.<>f__this.mKeepWindowLocked = 0;
                this.$current = this.<>f__this.SyncKyokaRequest();
                this.$PC = 7;
                goto Label_02B9;
            Label_02B0:
                this.$PC = -1;
            Label_02B7:
                return 0;
            Label_02B9:
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
        private sealed class <PostUnitUnlockTobira>c__Iterator15E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <effect_request>__0;
            internal GameObject <effect_obj>__1;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <PostUnitUnlockTobira>c__Iterator15E()
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
                        goto Label_002D;

                    case 1:
                        goto Label_0056;

                    case 2:
                        goto Label_00B4;

                    case 3:
                        goto Label_012A;

                    case 4:
                        goto Label_021A;
                }
                goto Label_0221;
            Label_002D:
                this.<>f__this.GetComponent<WindowController>().SetCollision(0);
                goto Label_0056;
            Label_0043:
                this.$current = null;
                this.$PC = 1;
                goto Label_0223;
            Label_0056:
                if (this.<>f__this.mUnlockTobiraRequestSent == null)
                {
                    goto Label_0043;
                }
                this.<effect_request>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.Prefab_Unlock_Tobira);
                if (this.<effect_request>__0 == null)
                {
                    goto Label_00B4;
                }
                if (this.<effect_request>__0.isDone != null)
                {
                    goto Label_00B4;
                }
                this.$current = this.<effect_request>__0.StartCoroutine();
                this.$PC = 2;
                goto Label_0223;
            Label_00B4:
                if (this.<>f__this.mSceneChanging == null)
                {
                    goto Label_00C9;
                }
                goto Label_0221;
            Label_00C9:
                this.<effect_obj>__1 = null;
                if (this.<effect_request>__0 == null)
                {
                    goto Label_0107;
                }
                this.<effect_obj>__1 = Object.Instantiate(this.<effect_request>__0.asset) as GameObject;
                this.<>f__this.AttachOverlay(this.<effect_obj>__1);
            Label_0107:
                this.<>f__this.PlayReaction();
                goto Label_012A;
            Label_0117:
                this.$current = null;
                this.$PC = 3;
                goto Label_0223;
            Label_012A:
                if ((this.<effect_obj>__1 != null) != null)
                {
                    goto Label_0117;
                }
                this.<>f__this.DestroyOverlay();
                this.<>f__this.PlayUnitVoice(this.<>f__this.m_VO_TobiraUnlock);
                this.<>f__this.RebuildUnitData();
                this.<>f__this.RefreshEXPImmediate();
                this.<>f__this.RefreshExpInfo();
                this.<>f__this.RefreshLevelCap();
                this.<>f__this.UpdateUnitKakuseiButtonState();
                this.<>f__this.UpdateUnitEvolutionButtonState(0);
                this.<>f__this.UpdateUnitTobiraButtonState();
                if ((this.<>f__this.UnitInfo != null) == null)
                {
                    goto Label_01F6;
                }
                if ((this.<>f__this.UnitParamInfo != null) == null)
                {
                    goto Label_01F6;
                }
                GameParameter.UpdateAll(this.<>f__this.UnitInfo);
                GameParameter.UpdateAll(this.<>f__this.UnitParamInfo);
            Label_01F6:
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.$current = null;
                this.$PC = 4;
                goto Label_0223;
            Label_021A:
                this.$PC = -1;
            Label_0221:
                return 0;
            Label_0223:
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
        private sealed class <RefreshAsync>c__Iterator152 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool immediate;
            internal bool is_job_icon_hide;
            internal DateTime <date>__0;
            internal int $PC;
            internal object $current;
            internal bool <$>immediate;
            internal bool <$>is_job_icon_hide;
            internal UnitEnhanceV3 <>f__this;

            public <RefreshAsync>c__Iterator152()
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

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0045;

                    case 1:
                        goto Label_009B;

                    case 2:
                        goto Label_0100;

                    case 3:
                        goto Label_0129;

                    case 4:
                        goto Label_0158;

                    case 5:
                        goto Label_0182;

                    case 6:
                        goto Label_01EE;

                    case 7:
                        goto Label_0218;

                    case 8:
                        goto Label_023B;

                    case 9:
                        goto Label_028A;

                    case 10:
                        goto Label_032C;
                }
                goto Label_0333;
            Label_0045:
                if ((this.<>f__this.UnitInfo != null) == null)
                {
                    goto Label_009B;
                }
                if ((this.<>f__this.UnitParamInfo != null) == null)
                {
                    goto Label_009B;
                }
                this.<>f__this.RefreshBasicParameters(0);
                if (this.immediate != null)
                {
                    goto Label_009B;
                }
                this.$current = null;
                this.$PC = 1;
                goto Label_0335;
            Label_009B:
                this.<>f__this.FadeUnitImage(0f, 0f, 0f);
                this.<>f__this.StartCoroutine(this.<>f__this.RefreshUnitImage());
                this.<>f__this.RefreshEXPImmediate();
                this.<>f__this.RefreshExpInfo();
                if (this.immediate != null)
                {
                    goto Label_0100;
                }
                this.$current = null;
                this.$PC = 2;
                goto Label_0335;
            Label_0100:
                this.<>f__this.RefreshJobInfo();
                if (this.immediate != null)
                {
                    goto Label_0129;
                }
                this.$current = null;
                this.$PC = 3;
                goto Label_0335;
            Label_0129:
                this.<>f__this.RefreshJobIcons(this.is_job_icon_hide);
                if (this.immediate != null)
                {
                    goto Label_0158;
                }
                this.$current = null;
                this.$PC = 4;
                goto Label_0335;
            Label_0158:
                this.<>f__this.RefreshEquipments(-1);
                if (this.immediate != null)
                {
                    goto Label_0182;
                }
                this.$current = null;
                this.$PC = 5;
                goto Label_0335;
            Label_0182:
                this.<>f__this.UpdateJobRankUpButtonState();
                this.<>f__this.UpdateJobChangeButtonState();
                this.<>f__this.UpdateUnitKakuseiButtonState();
                this.<>f__this.UpdateUnitEvolutionButtonState(1);
                this.<>f__this.UpdateUnitTobiraButtonState();
                this.<>f__this.RefreshAbilitySlotButtonState();
                this.<>f__this.RefreshAbilityList();
                if (this.immediate != null)
                {
                    goto Label_01EE;
                }
                this.$current = null;
                this.$PC = 6;
                goto Label_0335;
            Label_01EE:
                this.<>f__this.RefreshAbilitySlots(1);
                if (this.immediate != null)
                {
                    goto Label_0218;
                }
                this.$current = null;
                this.$PC = 7;
                goto Label_0335;
            Label_0218:
                if (this.immediate != null)
                {
                    goto Label_0245;
                }
                goto Label_023B;
            Label_0228:
                this.$current = null;
                this.$PC = 8;
                goto Label_0335;
            Label_023B:
                if (AssetManager.IsLoading != null)
                {
                    goto Label_0228;
                }
            Label_0245:
                this.<>f__this.ReloadPreviewModels();
                if ((this.<>f__this.mCurrentPreview != null) == null)
                {
                    goto Label_02AB;
                }
                if (this.immediate != null)
                {
                    goto Label_029F;
                }
                goto Label_028A;
            Label_0276:
                this.$current = null;
                this.$PC = 9;
                goto Label_0335;
            Label_028A:
                if (this.<>f__this.mCurrentPreview.IsLoading != null)
                {
                    goto Label_0276;
                }
            Label_029F:
                this.<>f__this.SetPreviewVisible(1);
            Label_02AB:
                if (this.<>f__this.IsCanPlayLeftVoice() == null)
                {
                    goto Label_02E2;
                }
                this.<date>__0 = DateTime.Now;
                this.<>f__this.PlayUnitVoice(UnitEnhanceV3.UnitHourVoices[&this.<date>__0.Hour]);
            Label_02E2:
                this.<>f__this.FadeUnitImage(0f, 1f, 1f);
                this.<>f__this.mReloading = 0;
                this.<>f__this.GetComponent<WindowController>().Open();
                this.$current = null;
                this.$PC = 10;
                goto Label_0335;
            Label_032C:
                this.$PC = -1;
            Label_0333:
                return 0;
            Label_0335:
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
        private sealed class <RefreshJobIcons>c__AnonStorey3BF
        {
            internal int i;
            internal UnitEnhanceV3 <>f__this;

            public <RefreshJobIcons>c__AnonStorey3BF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__450(JobSetParam jobset)
            {
                return (jobset.job == this.<>f__this.mCurrentUnit.Jobs[this.i].JobID);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshJobIcons>c__AnonStorey3C0
        {
            internal JobSetParam base_jobset_param;

            public <RefreshJobIcons>c__AnonStorey3C0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__451(JobData job)
            {
                return (job.JobID == this.base_jobset_param.job);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshJobIcons>c__AnonStorey3C1
        {
            internal JobSetParam[] cc_jobset_array;
            internal UnitEnhanceV3 <>f__this;

            public <RefreshJobIcons>c__AnonStorey3C1()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshJobIcons>c__AnonStorey3C2
        {
            internal int j;
            internal UnitEnhanceV3.<RefreshJobIcons>c__AnonStorey3C1 <>f__ref$961;

            public <RefreshJobIcons>c__AnonStorey3C2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__452(JobData job)
            {
                return (job.JobID == this.<>f__ref$961.cc_jobset_array[this.j].job);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUnitImage>c__Iterator154 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <RefreshUnitImage>c__Iterator154()
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
                        goto Label_0025;

                    case 1:
                        goto Label_009E;

                    case 2:
                        goto Label_00E7;
                }
                goto Label_00EE;
            Label_0025:
                if ((this.<>f__this.mBGUnitImage != null) == null)
                {
                    goto Label_00D4;
                }
                this.<req>__0 = AssetManager.LoadAsync<Texture2D>(AssetPath.UnitSkinImage2(this.<>f__this.mCurrentUnit.UnitParam, this.<>f__this.mCurrentUnit.GetSelectedSkin(-1), this.<>f__this.mCurrentUnit.CurrentJob.JobID));
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00F0;
            Label_009E:
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_00D4;
                }
                this.<>f__this.mBGUnitImage.set_texture(this.<req>__0.asset as Texture2D);
            Label_00D4:
                this.$current = null;
                this.$PC = 2;
                goto Label_00F0;
            Label_00E7:
                this.$PC = -1;
            Label_00EE:
                return 0;
            Label_00F0:
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
        private sealed class <SetNotifyReleaseClassChangeJob>c__AnonStorey3C3
        {
            internal JobSetParam[] cc_jobset_array;

            public <SetNotifyReleaseClassChangeJob>c__AnonStorey3C3()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <SetNotifyReleaseClassChangeJob>c__AnonStorey3C4
        {
            internal int i;
            internal UnitEnhanceV3.<SetNotifyReleaseClassChangeJob>c__AnonStorey3C3 <>f__ref$963;

            public <SetNotifyReleaseClassChangeJob>c__AnonStorey3C4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__455(JobData job)
            {
                return (job.JobID == this.<>f__ref$963.cc_jobset_array[this.i].job);
            }
        }

        [CompilerGenerated]
        private sealed class <ShiftUnitAsync>c__Iterator15F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal long unitUniqueID;
            internal int $PC;
            internal object $current;
            internal long <$>unitUniqueID;
            internal UnitEnhanceV3 <>f__this;

            public <ShiftUnitAsync>c__Iterator15F()
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
                        goto Label_0052;

                    case 2:
                        goto Label_00A5;

                    case 3:
                        goto Label_013C;
                }
                goto Label_0143;
            Label_0029:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.WaitForKyokaRequestAsync(0));
                this.$PC = 1;
                goto Label_0145;
            Label_0052:
                this.<>f__this.SetPreviewVisible(0);
                this.<>f__this.FadeUnitImage(0f, 0f, 0.5f);
                this.<>f__this.Refresh(this.unitUniqueID, 0L, 0, 1);
                goto Label_00A5;
            Label_0092:
                this.$current = null;
                this.$PC = 2;
                goto Label_0145;
            Label_00A5:
                if (this.<>f__this.IsLoading != null)
                {
                    goto Label_0092;
                }
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
                this.<>f__this.mKeepWindowLocked = 0;
                if ((this.<>f__this.mUnitModelViewer != null) == null)
                {
                    goto Label_0129;
                }
                if (GameObjectExtensions.GetActive(this.<>f__this.mUnitModelViewer.get_gameObject()) == null)
                {
                    goto Label_0129;
                }
                if ((this.<>f__this.mUnitModelViewer != null) == null)
                {
                    goto Label_0129;
                }
                this.<>f__this.mUnitModelViewer.Refresh(1);
            Label_0129:
                this.$current = null;
                this.$PC = 3;
                goto Label_0145;
            Label_013C:
                this.$PC = -1;
            Label_0143:
                return 0;
            Label_0145:
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
        private sealed class <ShowUnlockSkillEffect>c__Iterator162 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal UnitData <newUnitData>__0;
            internal QuestClearUnlockUnitDataParam[] <unlocks>__1;
            internal LoadRequest <request>__2;
            internal GameObject <clearUnlockPopup>__3;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <ShowUnlockSkillEffect>c__Iterator162()
            {
                base..ctor();
                return;
            }

            internal bool <>m__459(UnitData p)
            {
                return (p.UniqueID == this.<>f__this.mCurrentUnit.UniqueID);
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
                        goto Label_0025;

                    case 1:
                        goto Label_00CD;

                    case 2:
                        goto Label_0154;
                }
                goto Label_017C;
            Label_0025:
                this.<newUnitData>__0 = MonoSingleton<GameManager>.Instance.Player.Units.Find(new Predicate<UnitData>(this.<>m__459));
                if (string.IsNullOrEmpty(this.<>f__this.PREFAB_PATH_UNITDATA_UNLOCK_POPUP) != null)
                {
                    goto Label_0165;
                }
                if (this.<newUnitData>__0 == null)
                {
                    goto Label_0165;
                }
                this.<unlocks>__1 = this.<newUnitData>__0.UnlockedSkillDiff(this.<>f__this.mCurrentUnitUnlocks);
                if (((int) this.<unlocks>__1.Length) < 1)
                {
                    goto Label_0165;
                }
                this.<request>__2 = AssetManager.LoadAsync(this.<>f__this.PREFAB_PATH_UNITDATA_UNLOCK_POPUP);
                goto Label_00CD;
            Label_00B0:
                this.$current = this.<request>__2.StartCoroutine();
                this.$PC = 1;
                goto Label_017E;
            Label_00CD:
                if (this.<request>__2.isDone == null)
                {
                    goto Label_00B0;
                }
                if ((this.<request>__2.asset != null) == null)
                {
                    goto Label_0165;
                }
                this.<clearUnlockPopup>__3 = Object.Instantiate(this.<request>__2.asset) as GameObject;
                this.<clearUnlockPopup>__3.SetActive(1);
                DataSource.Bind<UnitData>(this.<clearUnlockPopup>__3, this.<newUnitData>__0);
                DataSource.Bind<QuestClearUnlockUnitDataParam[]>(this.<clearUnlockPopup>__3, this.<unlocks>__1);
                goto Label_0154;
            Label_0141:
                this.$current = null;
                this.$PC = 2;
                goto Label_017E;
            Label_0154:
                if ((this.<clearUnlockPopup>__3 != null) != null)
                {
                    goto Label_0141;
                }
            Label_0165:
                this.<>f__this.mCurrentUnitUnlocks = string.Empty;
                this.$PC = -1;
            Label_017C:
                return 0;
            Label_017E:
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
        private sealed class <Start>c__Iterator14A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal Dictionary<UnitEnhanceV3.eTabButtons, SRPG_ToggleButton>.KeyCollection.Enumerator <$s_1166>__1;
            internal UnitEnhanceV3.eTabButtons <tab>__2;
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <Start>c__Iterator14A()
            {
                base..ctor();
                return;
            }

            internal void <>m__456(SRPG_Button button)
            {
                this.<>f__this.OpenMapEffectJob();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                GameManager local2;
                GameManager local1;
                uint num;
                Dictionary<UnitEnhanceV3.eTabButtons, SRPG_ToggleButton> dictionary;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_028B;

                    case 2:
                        goto Label_052D;
                }
                goto Label_07ED;
            Label_0025:
                this.<>f__this.mJobIconScrollListController.Init();
                this.<>f__this.mJobIconScrollListController.CreateInstance();
                this.<>f__this.mLeftTime = Time.get_realtimeSinceStartup();
                if ((this.<>f__this.LeaderSkillDetailButton != null) == null)
                {
                    goto Label_008C;
                }
                this.<>f__this.LeaderSkillDetailButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OpenLeaderSkillDetail));
            Label_008C:
                if ((this.<>f__this.JobChangeButton != null) == null)
                {
                    goto Label_00C3;
                }
                this.<>f__this.JobChangeButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnJobChangeClick));
            Label_00C3:
                if (string.IsNullOrEmpty(this.<>f__this.BGAnimatorID) != null)
                {
                    goto Label_00F3;
                }
                this.<>f__this.mBGAnimator = GameObjectID.FindGameObject<Animator>(this.<>f__this.BGAnimatorID);
            Label_00F3:
                if (string.IsNullOrEmpty(this.<>f__this.BGUnitImageID) != null)
                {
                    goto Label_0123;
                }
                this.<>f__this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.<>f__this.BGUnitImageID);
            Label_0123:
                if (string.IsNullOrEmpty(this.<>f__this.BGTobiraImageID) != null)
                {
                    goto Label_0153;
                }
                this.<>f__this.mBGTobiraAnimator = GameObjectID.FindGameObject<Animator>(this.<>f__this.BGTobiraImageID);
            Label_0153:
                if (string.IsNullOrEmpty(this.<>f__this.BGTobiraEffectImageID) != null)
                {
                    goto Label_0183;
                }
                this.<>f__this.mBGTobiraEffectAnimator = GameObjectID.FindGameObject<Animator>(this.<>f__this.BGTobiraEffectImageID);
            Label_0183:
                local1 = MonoSingleton<GameManager>.Instance;
                local1.OnSceneChange = (GameManager.SceneChangeEvent) Delegate.Combine(local1.OnSceneChange, new GameManager.SceneChangeEvent(this.<>f__this.OnSceneCHange));
                local2 = MonoSingleton<GameManager>.Instance;
                local2.OnAbilityRankUpCountPreReset = (GameManager.RankUpCountChangeEvent) Delegate.Combine(local2.OnAbilityRankUpCountPreReset, new GameManager.RankUpCountChangeEvent(this.<>f__this.OnAbilityRankUpCountPreReset));
                this.<>f__this.GetComponent<WindowController>().OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.<>f__this.OnWindowStateChange);
                if ((this.<>f__this.NextUnitButton != null) == null)
                {
                    goto Label_0231;
                }
                this.<>f__this.NextUnitButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnShiftUnit));
            Label_0231:
                if ((this.<>f__this.PrevUnitButton != null) == null)
                {
                    goto Label_0268;
                }
                this.<>f__this.PrevUnitButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnShiftUnit));
            Label_0268:
                if (this.<>f__this.FastStart != null)
                {
                    goto Label_028B;
                }
                this.$current = null;
                this.$PC = 1;
                goto Label_07EF;
            Label_028B:
                this.<>f__this.mStatusParamSlots = new Text[StatusParam.MAX_STATUS];
                this.<>f__this.mStatusParamSlots[0] = this.<>f__this.Status_HP;
                this.<>f__this.mStatusParamSlots[1] = this.<>f__this.Status_MP;
                this.<>f__this.mStatusParamSlots[2] = this.<>f__this.Status_MPIni;
                this.<>f__this.mStatusParamSlots[3] = this.<>f__this.Status_Atk;
                this.<>f__this.mStatusParamSlots[4] = this.<>f__this.Status_Def;
                this.<>f__this.mStatusParamSlots[5] = this.<>f__this.Status_Mag;
                this.<>f__this.mStatusParamSlots[6] = this.<>f__this.Status_Mnd;
                this.<>f__this.mStatusParamSlots[7] = this.<>f__this.Status_Rec;
                this.<>f__this.mStatusParamSlots[8] = this.<>f__this.Status_Dex;
                this.<>f__this.mStatusParamSlots[9] = this.<>f__this.Status_Spd;
                this.<>f__this.mStatusParamSlots[10] = this.<>f__this.Status_Cri;
                this.<>f__this.mStatusParamSlots[11] = this.<>f__this.Status_Luk;
                this.<>f__this.mStatusParamSlots[12] = this.<>f__this.Status_Mov;
                this.<>f__this.mStatusParamSlots[13] = this.<>f__this.Status_Jmp;
                this.<i>__0 = 0;
                goto Label_044E;
            Label_0401:
                if ((this.<>f__this.mStatusParamSlots[this.<i>__0] != null) == null)
                {
                    goto Label_0440;
                }
                this.<>f__this.AddParamTooltip(this.<>f__this.mStatusParamSlots[this.<i>__0].get_gameObject());
            Label_0440:
                this.<i>__0 += 1;
            Label_044E:
                if (this.<i>__0 < ((int) this.<>f__this.mStatusParamSlots.Length))
                {
                    goto Label_0401;
                }
                if ((this.<>f__this.Param_Renkei != null) == null)
                {
                    goto Label_0497;
                }
                this.<>f__this.AddParamTooltip(this.<>f__this.Param_Renkei.get_gameObject());
            Label_0497:
                if ((this.<>f__this.Prefab_AbilityPicker != null) == null)
                {
                    goto Label_052D;
                }
                this.<>f__this.mAbilityPicker = Object.Instantiate<UnitAbilityPicker>(this.<>f__this.Prefab_AbilityPicker);
                this.<>f__this.mAbilityPicker.OnAbilitySelect = new UnitAbilityPicker.AbilityPickerEvent(this.<>f__this.OnSlotAbilitySelect);
                this.<>f__this.mAbilityPicker.OnAbilityRankUp = new UnitAbilityPicker.AbilityPickerEvent(this.<>f__this.OnAbilityRankUp);
                if (this.<>f__this.FastStart != null)
                {
                    goto Label_052D;
                }
                this.$current = null;
                this.$PC = 2;
                goto Label_07EF;
            Label_052D:
                this.<>f__this.mTabPages = new UnitEnhancePanel[4];
                dictionary = new Dictionary<UnitEnhanceV3.eTabButtons, SRPG_ToggleButton>();
                dictionary.Add(0, this.<>f__this.Tab_Equipments);
                dictionary.Add(1, this.<>f__this.Tab_Kyoka);
                dictionary.Add(2, this.<>f__this.Tab_AbilityList);
                dictionary.Add(3, this.<>f__this.Tab_AbilitySlot);
                this.<>f__this.mTabButtons = dictionary;
                this.<$s_1166>__1 = this.<>f__this.mTabButtons.Keys.GetEnumerator();
            Label_05B3:
                try
                {
                    goto Label_061B;
                Label_05B8:
                    this.<tab>__2 = &this.<$s_1166>__1.Current;
                    if ((this.<>f__this.mTabButtons[this.<tab>__2] == null) == null)
                    {
                        goto Label_05EF;
                    }
                    goto Label_061B;
                Label_05EF:
                    this.<>f__this.mTabButtons[this.<tab>__2].AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnTabChange));
                Label_061B:
                    if (&this.<$s_1166>__1.MoveNext() != null)
                    {
                        goto Label_05B8;
                    }
                    goto Label_0641;
                }
                finally
                {
                Label_0630:
                    ((Dictionary<UnitEnhanceV3.eTabButtons, SRPG_ToggleButton>.KeyCollection.Enumerator) this.<$s_1166>__1).Dispose();
                }
            Label_0641:
                if (this.<>f__this.mTabButtons.Count <= 0)
                {
                    goto Label_0690;
                }
                if ((this.<>f__this.mTabButtons[0] != null) == null)
                {
                    goto Label_0690;
                }
                this.<>f__this.TabChange(this.<>f__this.mTabButtons[0]);
            Label_0690:
                if ((this.<>f__this.UnitKakuseiButton != null) == null)
                {
                    goto Label_06C7;
                }
                this.<>f__this.UnitKakuseiButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnKakuseiButtonClick));
            Label_06C7:
                if ((this.<>f__this.UnitEvolutionButton != null) == null)
                {
                    goto Label_06FE;
                }
                this.<>f__this.UnitEvolutionButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnEvolutionButtonClick));
            Label_06FE:
                if ((this.<>f__this.CharaQuestButton != null) == null)
                {
                    goto Label_0735;
                }
                this.<>f__this.CharaQuestButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnCharacterQuestOpen));
            Label_0735:
                if ((this.<>f__this.SkinButton != null) == null)
                {
                    goto Label_076C;
                }
                this.<>f__this.SkinButton.AddListener(new SRPG_Button.ButtonClickEvent(this.<>f__this.OnSkinSelectOpen));
            Label_076C:
                if ((this.<>f__this.ButtonMapEffectJob != null) == null)
                {
                    goto Label_07CE;
                }
                this.<>f__this.ButtonMapEffectJob.AddListener(new SRPG_Button.ButtonClickEvent(this.<>m__456));
                if (string.IsNullOrEmpty(this.<>f__this.PrefabMapEffectJob) != null)
                {
                    goto Label_07CE;
                }
                this.<>f__this.mReqMapEffectJob = AssetManager.LoadAsync<GameObject>(this.<>f__this.PrefabMapEffectJob);
            Label_07CE:
                this.<>f__this.mStarted = 1;
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 1);
                this.$PC = -1;
            Label_07ED:
                return 0;
            Label_07EF:
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
        private sealed class <UnitUnlockTobira>c__Iterator15D : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <UnitUnlockTobira>c__Iterator15D()
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
                        goto Label_0044;
                }
                goto Label_00DB;
            Label_0021:
                if (this.<>f__this.IsKyokaRequestQueued == null)
                {
                    goto Label_0044;
                }
                this.$current = null;
                this.$PC = 1;
                goto Label_00DD;
            Label_0044:
                if (this.<>f__this.mCurrentUnit.MeetsTobiraConditions(0) == null)
                {
                    goto Label_00D4;
                }
                this.<>f__this.mUnlockTobiraRequestSent = 0;
                if (Network.Mode != null)
                {
                    goto Label_00A6;
                }
                this.<>f__this.RequestAPI(new ReqTobiraUnlock(this.<>f__this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.<>f__this.OnUnitUnlockTobiraResult)));
                goto Label_00B2;
            Label_00A6:
                this.<>f__this.mUnlockTobiraRequestSent = 1;
            Label_00B2:
                this.<>f__this.SetUnitDirty();
                this.<>f__this.StartCoroutine(this.<>f__this.PostUnitUnlockTobira());
            Label_00D4:
                this.$PC = -1;
            Label_00DB:
                return 0;
            Label_00DD:
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
        private sealed class <WaitForKyokaRequestAndInvokeUserClose>c__Iterator155 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitEnhanceV3 <>f__this;

            public <WaitForKyokaRequestAndInvokeUserClose>c__Iterator155()
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
                goto Label_005C;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_005E;
            Label_0039:
                if (this.<>f__this.ExecQueuedKyokaRequest(null) != null)
                {
                    goto Label_0026;
                }
                this.<>f__this.InvokeUserClose();
                this.$PC = -1;
            Label_005C:
                return 0;
            Label_005E:
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
        private sealed class <WaitForKyokaRequestAsync>c__Iterator14F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool unlockWindow;
            internal int $PC;
            internal object $current;
            internal bool <$>unlockWindow;
            internal UnitEnhanceV3 <>f__this;

            public <WaitForKyokaRequestAsync>c__Iterator14F()
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
                goto Label_006C;
            Label_0021:
                goto Label_0039;
            Label_0026:
                this.$current = null;
                this.$PC = 1;
                goto Label_006E;
            Label_0039:
                if (this.<>f__this.mSendingKyokaRequest != null)
                {
                    goto Label_0026;
                }
                if (this.unlockWindow == null)
                {
                    goto Label_0065;
                }
                this.<>f__this.GetComponent<WindowController>().SetCollision(1);
            Label_0065:
                this.$PC = -1;
            Label_006C:
                return 0;
            Label_006E:
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

        public delegate void CloseEvent();

        private delegate void DeferredJob();

        private enum eTabButtons
        {
            Equipments,
            Kyoka,
            AbilityList,
            AbilitySlot,
            Max
        }

        private class ExpItemTouchController : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
        {
            public DelegateOnPointerDown OnPointerDownFunc;
            public DelegateOnPointerDown OnPointerUpFunc;
            public DelegateUseItem UseItemFunc;
            public float UseItemSpan;
            public float HoldDuration;
            public bool Holding;
            private int UseNum;
            private int NextSettingIndex;
            private Vector2 mDragStartPos;

            public ExpItemTouchController()
            {
                this.UseItemSpan = 0.5f;
                base..ctor();
                return;
            }

            public void OnDestroy()
            {
                this.StatusReset();
                if (this.OnPointerDownFunc == null)
                {
                    goto Label_0018;
                }
                this.OnPointerDownFunc = null;
            Label_0018:
                if (this.UseItemFunc == null)
                {
                    goto Label_002A;
                }
                this.UseItemFunc = null;
            Label_002A:
                return;
            }

            public void OnPointerDown(PointerEventData eventData)
            {
                if (this.OnPointerDownFunc == null)
                {
                    goto Label_002A;
                }
                this.OnPointerDownFunc(this);
                this.Holding = 1;
                this.mDragStartPos = eventData.get_position();
            Label_002A:
                return;
            }

            public void OnPointerUp()
            {
                if (this.OnPointerUpFunc == null)
                {
                    goto Label_0017;
                }
                this.OnPointerUpFunc(this);
            Label_0017:
                this.StatusReset();
                return;
            }

            public unsafe void StatusReset()
            {
                this.HoldDuration = 0f;
                this.Holding = 0;
                this.UseNum = 0;
                this.NextSettingIndex = 0;
                &this.mDragStartPos.Set(0f, 0f);
                return;
            }

            public unsafe void UpdateTimer(float deltaTime)
            {
                bool flag;
                GameSettings settings;
                float num;
                bool flag2;
                GameSettings.HoldCountSettings[] settingsArray;
                Vector2 vector;
                if (this.UseItemFunc != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                flag = Input.GetMouseButton(0);
                if (this.Holding == null)
                {
                    goto Label_0067;
                }
                if (flag != null)
                {
                    goto Label_0067;
                }
                if (this.HoldDuration >= this.UseItemSpan)
                {
                    goto Label_0060;
                }
                if (this.UseNum >= 1)
                {
                    goto Label_0060;
                }
                this.UseItemFunc(base.get_gameObject());
                this.UseNum += 1;
            Label_0060:
                this.OnPointerUp();
                return;
            Label_0067:
                settings = GameSettings.Instance;
                num = (float) (settings.HoldMargin * settings.HoldMargin);
                vector = this.mDragStartPos - Input.get_mousePosition();
                flag2 = &vector.get_sqrMagnitude() > num;
                if (this.HoldDuration >= this.UseItemSpan)
                {
                    goto Label_00C8;
                }
                if (this.UseNum >= 1)
                {
                    goto Label_00C8;
                }
                if (flag2 == null)
                {
                    goto Label_00C8;
                }
                this.StatusReset();
                return;
            Label_00C8:
                if (this.Holding == null)
                {
                    goto Label_0182;
                }
                this.HoldDuration += Time.get_unscaledDeltaTime();
                if (this.HoldDuration < this.UseItemSpan)
                {
                    goto Label_0182;
                }
                this.HoldDuration -= this.UseItemSpan;
                this.UseItemFunc(base.get_gameObject());
                this.UseNum += 1;
                settingsArray = settings.HoldCount;
                if (((int) settingsArray.Length) <= this.NextSettingIndex)
                {
                    goto Label_0182;
                }
                if (&(settingsArray[this.NextSettingIndex]).Count >= this.UseNum)
                {
                    goto Label_0182;
                }
                this.UseItemSpan = &(settingsArray[this.NextSettingIndex]).UseSpan;
                this.NextSettingIndex += 1;
            Label_0182:
                return;
            }

            public delegate void DelegateOnPointerDown(UnitEnhanceV3.ExpItemTouchController controller);

            public delegate void DelegateOnPointerUp(UnitEnhanceV3.ExpItemTouchController controller);

            public delegate void DelegateUseItem(GameObject listItem);
        }
    }
}

