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
    using UnityEngine.UI;

    [Pin(10, "GachaConfirm", 0, 0), Pin(0x3e7, "WebView起動", 1, 0x3e7), Pin(200, "召喚結果へ強制遷移", 1, 200), Pin(0x71, "リフレッシュ完了", 1, 0x71), Pin(0x70, "チケット召喚をキャンセル", 0, 0x70), Pin(0x6f, "チケット召喚を行う", 0, 0x6f), Pin(110, "チケット召喚(単発)を選択", 1, 110), Pin(0x66, "Closed", 0, 0x66), Pin(0x65, "Opened", 0, 0x65), Pin(100, "ガチャ実行", 1, 100), Pin(3, "DecisionRedraw", 0, 3), Pin(2, "ExecFailed", 0, 2), Pin(1, "Refresh", 0, 1)]
    public class GachaWindow : MonoBehaviour, IFlowInterface
    {
        private const int PIN_OT_TO_RESULT = 200;
        private const int PIN_IN_DECISION_REDRAW_GACHA = 3;
        private static readonly int IN_GACHA_CONFIRM;
        private static readonly string GACHA_URL_PREFIX;
        private static readonly float WAIT_SWAP_BG;
        private static readonly string DEFAULT_BGIMAGE_PATH;
        private static readonly string BG_TEXTURE_PATH;
        private static readonly int ConversionConstant;
        private static readonly string TAB_SPRITES_PATH;
        private static readonly string TEXT_CONFIRM_GACHA_COIN;
        private static readonly string TEXT_COST_GACHA_COIN;
        private static readonly string TEXT_COST_GACHA_PAIDCOIN;
        private static readonly string TEXT_CONFIRM_GACHA_COST_ZERO;
        public GameObject SelectJobIcon;
        public float mWaitSecondsChangeUnitJob;
        public string PickupPreviewParentID;
        public string PickUpPreviewBaseID;
        public string PickUpCameraID;
        public string BGUnitImageID;
        private Transform mPreviewParent;
        private GameObject mPreviewBase;
        private Camera mPreviewCamera;
        private GachaUnitPreview mCurrentPreview;
        private List<GachaUnitPreview> mPreviewControllers;
        private RawImage mBGUnitImage;
        private UnitData mCurrentUnit;
        private List<UnitData> mPickupUnits;
        private int mCurrentIndex;
        private int mCurrentJobIndex;
        private StateMachine<GachaWindow> mState;
        private bool mInitialized;
        private bool mChangeUnit;
        private bool mChangeJob;
        private bool mClicked;
        public GameObject ChangeUnitEffectObj;
        public GameObject ChangeArtifactEffectObj;
        public float ChangeEffectWaitTime;
        public float ChangeUnitWaitEffectTime;
        public float ChangeJobWaitEffectTime;
        public float WaitTimeNextAction;
        public GameObject UnitEffectObj;
        public GameObject JobEffectObj;
        public GameObject ArtifactEffectObj;
        private RenderTexture mPreviewUnitRT;
        public RawImage PreviewImage;
        private bool mDesiredPreviewVisibility;
        private bool mUpdatePreviewVisibility;
        public string PickupPreviewArtifact;
        public GameObject DefaultPanel;
        public GameObject TicketPanel;
        public GameObject ButtonPanel;
        public GameObject TabPanel;
        public GameObject OptionPanel;
        public GameObject TicketButtonTemplate;
        public Transform TicketListRoot;
        public GameObject TicketNotListView;
        private List<GachaTopParamNew> mGachaListRare;
        private List<GachaTopParamNew> mGachaListNormal;
        private List<GachaTopParamNew> mGachaListTicket;
        private List<GachaTopParamNew> mGachaListArtifact;
        private List<GachaTopParamNewGroups> mGachaListSpecials;
        private List<GachaTopParamNew> mGachaListAll;
        public SRPG_Button RareTab;
        public SRPG_Button NormalTab;
        public SRPG_Button TicketTab;
        public SRPG_Button ArtifactTab;
        public SRPG_Button TabTemplate;
        private List<SRPG_Button> mTabList;
        private GachaTabCategory mSelectTab;
        public GameObject UnitInfoPanel;
        public GameObject ArtifactInfoPanel;
        public GameObject BonusPanel;
        public Text BonusItemName;
        public GameObject BonusPanelItem;
        public GameObject BonusPanelUnit;
        public GameObject BonusPanelArtifact;
        public GameObject BonusPanelConceptCard;
        public StatusList Status;
        public GameObject WeaponAbilityInfo;
        public GameObject ArtifactRarityPanel;
        public Text ArtifactType;
        private List<ArtifactData> mPickupArtifacts;
        private ArtifactData mCurrentArtifact;
        public RawImage PreviewArtifactImage;
        private Transform mCurrentArtifactPreview;
        private Transform mPreviewArtifact;
        private List<Transform> mPreviewArtifactControllers;
        public Transform BGRoot;
        public GameObject BonusMsgPanel;
        public Text BonusMsgText;
        private int mCurrentTabSPIndex;
        private int mCurrentPickupArtIndex;
        private Dictionary<string, GameObject> mTicketButtonLists;
        private bool mLoadGachaTabSprites;
        private Dictionary<string, Sprite> mCacheTabImages;
        private List<Transform> mBGObjects;
        private bool mLoadBackGroundTexture;
        private Dictionary<string, Texture2D> mCacheBGImages;
        private Texture2D mDefaultBG;
        private bool IsTabChanging;
        private int mCurrentTabIndex;
        private float mWaitSwapBGTime;
        private bool mExistSwapBG;
        private int mEnableBGIndex;
        private List<GachaTabCategory> mTabCategoryList;
        private string mDetailURL;
        public string DESCRIPTION_URL;
        private string mDescriptionURL;
        [SerializeField]
        private Button DetailButton;
        [SerializeField]
        private Button DescriptionButton;
        [SerializeField]
        private GameObject GachaConfirmWindow;
        private GachaRequestParam m_request;
        [SerializeField]
        private Transform RootObject;
        private float mBGUnitImgAlphaStart;
        private float mBGUnitImgAlphaEnd;
        private float mBGUnitImgFadeTime;
        private float mBGUnitImgFadeTimeMax;
        private bool IsRefreshingGachaBG;
        [SerializeField]
        private GameObject GachaButtonTemplate;
        private List<GachaButton> m_GachaButtons;
        private List<GachaTopParamNew> m_CacheList_Gold;
        private List<GachaTopParamNew> m_CacheList_Coin;
        private List<GachaTopParamNew> m_CacheList_CoinPaid;
        [SerializeField]
        private GameObject SummonCoin;
        private string CONFIRM_WINDOW_PATH;
        private GachaRequestParam m_CurrentGachaRequestParam;
        [CompilerGenerated]
        private static Predicate<EventCoinData> <>f__am$cache7C;
        [CompilerGenerated]
        private static Comparison<GachaTopParamNew> <>f__am$cache7D;
        [CompilerGenerated]
        private static Comparison<GachaTopParamNew> <>f__am$cache7E;
        [CompilerGenerated]
        private static Comparison<GachaTopParamNew> <>f__am$cache7F;
        [CompilerGenerated]
        private static Comparison<GachaTopParamNew> <>f__am$cache80;

        static GachaWindow()
        {
            IN_GACHA_CONFIRM = 10;
            GACHA_URL_PREFIX = "notice/detail/gacha/";
            WAIT_SWAP_BG = 5f;
            DEFAULT_BGIMAGE_PATH = "GachaImages_Default";
            BG_TEXTURE_PATH = "Gachas/BGTables";
            ConversionConstant = 0xfee0;
            TAB_SPRITES_PATH = "Gachas/GachaTabSprites";
            TEXT_CONFIRM_GACHA_COIN = "sys.CONFIRM_GACHA_COIN";
            TEXT_COST_GACHA_COIN = "sys.GACHA_COST_COIN";
            TEXT_COST_GACHA_PAIDCOIN = "sys.GACHA_COST_PAIDCOIN";
            TEXT_CONFIRM_GACHA_COST_ZERO = "sys.CONFIRM_GACHA_COST_ZERO";
            return;
        }

        public GachaWindow()
        {
            this.mWaitSecondsChangeUnitJob = 5f;
            this.PickUpPreviewBaseID = "GACHAUNITPREVIEWBASE";
            this.PickUpCameraID = "GACHAUNITPRVIEWCAMERA";
            this.BGUnitImageID = "GACHA_TOP_UNIT_IMG";
            this.mPreviewControllers = new List<GachaUnitPreview>();
            this.ChangeEffectWaitTime = 1f;
            this.ChangeUnitWaitEffectTime = 1f;
            this.ChangeJobWaitEffectTime = 1f;
            this.WaitTimeNextAction = 1f;
            this.PickupPreviewArtifact = "GACHAARTIFACTPREVIEW";
            this.mGachaListRare = new List<GachaTopParamNew>();
            this.mGachaListNormal = new List<GachaTopParamNew>();
            this.mGachaListTicket = new List<GachaTopParamNew>();
            this.mGachaListArtifact = new List<GachaTopParamNew>();
            this.mGachaListSpecials = new List<GachaTopParamNewGroups>();
            this.mGachaListAll = new List<GachaTopParamNew>();
            this.mTabList = new List<SRPG_Button>();
            this.mPreviewArtifactControllers = new List<Transform>();
            this.mCurrentTabSPIndex = -1;
            this.mTicketButtonLists = new Dictionary<string, GameObject>();
            this.mCacheTabImages = new Dictionary<string, Sprite>();
            this.mBGObjects = new List<Transform>();
            this.mCacheBGImages = new Dictionary<string, Texture2D>();
            this.mCurrentTabIndex = -1;
            this.mTabCategoryList = new List<GachaTabCategory>();
            this.mDetailURL = string.Empty;
            this.DESCRIPTION_URL = "description";
            this.mDescriptionURL = string.Empty;
            this.m_GachaButtons = new List<GachaButton>();
            this.m_CacheList_Gold = new List<GachaTopParamNew>();
            this.m_CacheList_Coin = new List<GachaTopParamNew>();
            this.m_CacheList_CoinPaid = new List<GachaTopParamNew>();
            this.CONFIRM_WINDOW_PATH = "UI/GachaConfirmWindow";
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private bool <CheckPrevGachaRequest>m__33A(GachaTopParamNew p)
        {
            return (p.iname == this.m_CurrentGachaRequestParam.Iname);
        }

        [CompilerGenerated]
        private static bool <RefreshSummonCoin>m__327(EventCoinData f)
        {
            return f.iname.Equals("IT_US_SUMMONS_01");
        }

        [CompilerGenerated]
        private bool <RefreshTabList>m__331(SRPG_Button tab)
        {
            return (tab == this.RareTab);
        }

        [CompilerGenerated]
        private bool <RefreshTabList>m__332(SRPG_Button tab)
        {
            return (tab == this.RareTab);
        }

        [CompilerGenerated]
        private bool <RefreshTabList>m__333(SRPG_Button tab)
        {
            return (tab == this.TicketTab);
        }

        [CompilerGenerated]
        private static int <SetupGachaList>m__32C(GachaTopParamNew a, GachaTopParamNew b)
        {
            return (a.num - b.num);
        }

        [CompilerGenerated]
        private static int <SortGachaList>m__32D(GachaTopParamNew a, GachaTopParamNew b)
        {
            return (a.num - b.num);
        }

        [CompilerGenerated]
        private static int <SortGachaList>m__32E(GachaTopParamNew a, GachaTopParamNew b)
        {
            return (a.num - b.num);
        }

        [CompilerGenerated]
        private static int <SortGachaList>m__32F(GachaTopParamNew a, GachaTopParamNew b)
        {
            return (a.num - b.num);
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_001A;
            }
            this.Refresh();
            FlowNode_GameObject.ActivateOutputLinks(this, 0x71);
            goto Label_0076;
        Label_001A:
            if (pinID != 2)
            {
                goto Label_002D;
            }
            this.mClicked = 0;
            goto Label_0076;
        Label_002D:
            if (pinID != 3)
            {
                goto Label_003F;
            }
            this.OnDecisionRedrawGacha();
            goto Label_0076;
        Label_003F:
            if (pinID != IN_GACHA_CONFIRM)
            {
                goto Label_0055;
            }
            this.CheckPrevGachaRequest();
            goto Label_0076;
        Label_0055:
            if (pinID != 0x6f)
            {
                goto Label_0068;
            }
            this.OnDecideForTicketSelect();
            goto Label_0076;
        Label_0068:
            if (pinID != 0x70)
            {
                goto Label_0076;
            }
            this.OnCancel();
        Label_0076:
            return;
        }

        private void Awake()
        {
            StringBuilder builder;
            if ((this.DefaultPanel != null) == null)
            {
                goto Label_001D;
            }
            this.DefaultPanel.SetActive(0);
        Label_001D:
            if ((this.TicketPanel != null) == null)
            {
                goto Label_003A;
            }
            this.TicketPanel.SetActive(0);
        Label_003A:
            if ((this.ButtonPanel != null) == null)
            {
                goto Label_0057;
            }
            this.ButtonPanel.SetActive(0);
        Label_0057:
            if ((this.RareTab != null) == null)
            {
                goto Label_0079;
            }
            this.RareTab.get_gameObject().SetActive(0);
        Label_0079:
            if ((this.ArtifactTab != null) == null)
            {
                goto Label_009B;
            }
            this.ArtifactTab.get_gameObject().SetActive(0);
        Label_009B:
            if ((this.TicketTab != null) == null)
            {
                goto Label_00BD;
            }
            this.TicketTab.get_gameObject().SetActive(0);
        Label_00BD:
            if ((this.NormalTab != null) == null)
            {
                goto Label_00DF;
            }
            this.NormalTab.get_gameObject().SetActive(0);
        Label_00DF:
            if ((this.TabTemplate != null) == null)
            {
                goto Label_0101;
            }
            this.TabTemplate.get_gameObject().SetActive(0);
        Label_0101:
            if ((this.TicketButtonTemplate != null) == null)
            {
                goto Label_011E;
            }
            this.TicketButtonTemplate.SetActive(0);
        Label_011E:
            this.mPreviewUnitRT = this.CreateRenderTexture();
            this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
            if ((this.mPreviewCamera != null) == null)
            {
                goto Label_015D;
            }
            this.mPreviewCamera.set_targetTexture(this.mPreviewUnitRT);
        Label_015D:
            builder = new StringBuilder();
            builder.Append(Network.SiteHost);
            builder.Append(GACHA_URL_PREFIX);
            builder.Append(this.DESCRIPTION_URL);
            this.mDescriptionURL = builder.ToString();
            if ((this.DetailButton != null) == null)
            {
                goto Label_01C1;
            }
            this.DetailButton.get_onClick().AddListener(new UnityAction(this, this.OnClickDetail));
        Label_01C1:
            if ((this.DescriptionButton != null) == null)
            {
                goto Label_01EE;
            }
            this.DescriptionButton.get_onClick().AddListener(new UnityAction(this, this.OnClickDescription));
        Label_01EE:
            if ((this.GachaButtonTemplate != null) == null)
            {
                goto Label_020B;
            }
            this.GachaButtonTemplate.SetActive(0);
        Label_020B:
            return;
        }

        private void ChangePreviewArtifact()
        {
            this.mCurrentPickupArtIndex += 1;
            if (this.mCurrentPickupArtIndex <= (this.mPickupArtifacts.Count - 1))
            {
                goto Label_002D;
            }
            this.mCurrentPickupArtIndex = 0;
        Label_002D:
            return;
        }

        private void ChangePreviewUnit()
        {
            this.mCurrentIndex += 1;
            if (this.mCurrentIndex <= (this.mPickupUnits.Count - 1))
            {
                goto Label_002D;
            }
            this.mCurrentIndex = 0;
        Label_002D:
            return;
        }

        private void CheckPrevGachaRequest()
        {
            GachaRequestParam param;
            GachaTopParamNew new2;
            int num;
            PlayerData data;
            if (this.m_CurrentGachaRequestParam != null)
            {
                goto Label_0058;
            }
            if ((GachaResultData.drops == null) || (GachaResultData.IsPending == null))
            {
                goto Label_004D;
            }
            param = this.CreateGachaRequest(GachaResultData.receipt.iname);
            if (param != null)
            {
                goto Label_0041;
            }
            DebugUtility.LogError("召喚リクエストの生成に失敗しました.");
            return;
        Label_0041:
            this.m_CurrentGachaRequestParam = param;
            goto Label_0058;
        Label_004D:
            DebugUtility.LogError("前回の召喚リクエストが存在しません");
            return;
        Label_0058:
            if ((this.m_CurrentGachaRequestParam.IsRedrawGacha == null) || (GachaResultData.drops == null))
            {
                goto Label_008D;
            }
            this.m_CurrentGachaRequestParam.SetRedraw(GachaResultData.RedrawRest, this.m_CurrentGachaRequestParam.RedrawNum);
        Label_008D:
            if (this.m_CurrentGachaRequestParam.IsTicketGacha == null)
            {
                goto Label_012A;
            }
            if (this.m_CurrentGachaRequestParam.IsRedrawGacha == null)
            {
                goto Label_00C6;
            }
            base.StartCoroutine(this.CreateConfirm(this.m_CurrentGachaRequestParam, 1));
            goto Label_0125;
        Label_00C6:
            FlowNode_Variable.Set("USE_TICKET_INAME", this.m_CurrentGachaRequestParam.Ticket);
            new2 = this.mGachaListTicket.Find(new Predicate<GachaTopParamNew>(this.<CheckPrevGachaRequest>m__33A));
            if (new2 == null)
            {
                goto Label_011D;
            }
            FlowNode_Variable.Set("USE_TICKET_MAX", (new2.redraw == null) ? string.Empty : "1");
        Label_011D:
            FlowNode_GameObject.ActivateOutputLinks(this, 110);
        Label_0125:
            goto Label_0231;
        Label_012A:
            if (this.m_CurrentGachaRequestParam.IsSingle == null)
            {
                goto Label_01F2;
            }
            num = this.m_CurrentGachaRequestParam.Free;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (this.m_CurrentGachaRequestParam.Category != 2)
            {
                goto Label_019B;
            }
            if (this.IsFreePause(this.mGachaListNormal.ToArray()) != null)
            {
                goto Label_01E6;
            }
            num = ((data.CheckFreeGachaGold() == null) || (data.CheckFreeGachaGoldMax() != null)) ? 0 : 1;
            goto Label_01E6;
        Label_019B:
            if (((this.m_CurrentGachaRequestParam.Category != 1) || (this.m_CurrentGachaRequestParam.CostType != 1)) || (this.IsFreePause(this.mGachaListRare.ToArray()) != null))
            {
                goto Label_01E6;
            }
            num = (data.CheckFreeGachaCoin() == null) ? 0 : 1;
        Label_01E6:
            this.m_CurrentGachaRequestParam.SetFree(num);
        Label_01F2:
            if (this.m_CurrentGachaRequestParam.IsGold != null)
            {
                goto Label_0212;
            }
            if (this.m_CurrentGachaRequestParam.IsFree == null)
            {
                goto Label_021D;
            }
        Label_0212:
            this.OnDecide();
            goto Label_0231;
        Label_021D:
            base.StartCoroutine(this.CreateConfirm(this.m_CurrentGachaRequestParam, 1));
        Label_0231:
            return;
        }

        private void ClearBGSprites()
        {
            if (this.mCacheBGImages == null)
            {
                goto Label_002E;
            }
            if (this.mCacheBGImages.Count <= 0)
            {
                goto Label_002E;
            }
            this.mCacheBGImages.Clear();
            this.mCacheBGImages = null;
        Label_002E:
            return;
        }

        private void ClearTabSprites()
        {
            if (this.mCacheTabImages == null)
            {
                goto Label_002E;
            }
            if (this.mCacheTabImages.Count <= 0)
            {
                goto Label_002E;
            }
            this.mCacheTabImages.Clear();
            this.mCacheTabImages = null;
        Label_002E:
            return;
        }

        private string ConvertFullWidth(string half)
        {
            string str;
            int num;
            str = null;
            num = 0;
            goto Label_0027;
        Label_0009:
            str = str + ((char) ((ushort) (half[num] + ConversionConstant)));
            num += 1;
        Label_0027:
            if (num < half.Length)
            {
                goto Label_0009;
            }
            return str;
        }

        private ArtifactData CreateArtifactData(ArtifactParam param)
        {
            ArtifactData data;
            Json_Artifact artifact;
            RarityParam param2;
            data = new ArtifactData();
            artifact = new Json_Artifact();
            artifact.iid = 1L;
            artifact.iname = param.iname;
            artifact.rare = param.raremax;
            artifact.fav = 0;
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(param.raremax);
            artifact.exp = ArtifactData.StaticCalcExpFromLevel(param2.ArtifactLvCap);
            data.Deserialize(artifact);
            return data;
        }

        [DebuggerHidden]
        private IEnumerator CreateConfirm(GachaRequestParam _param, bool _is_coin_status)
        {
            <CreateConfirm>c__Iterator118 iterator;
            iterator = new <CreateConfirm>c__Iterator118();
            iterator._param = _param;
            iterator.<$>_param = _param;
            iterator.<>f__this = this;
            return iterator;
        }

        private void CreateGachaConfirmWindow(string confirm, int cost, string ticket, GachaCostType type, bool isShowCoinStatus)
        {
            GameObject obj2;
            SRPG.GachaConfirmWindow window;
            ItemParam param;
            if ((this.GachaConfirmWindow == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            obj2 = Object.Instantiate<GameObject>(this.GachaConfirmWindow);
            if ((obj2 == null) == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            window = obj2.GetComponent<SRPG.GachaConfirmWindow>();
            if ((window == null) == null)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            window.ConfirmText = confirm;
            window.Cost = cost;
            window.UseTicket = ticket;
            window.GachaCostType = type;
            window.IsShowCoinStatus = isShowCoinStatus;
            window.OnDecide = new SRPG.GachaConfirmWindow.DecideEvent(this.OnDecide);
            window.OnCancel = new SRPG.GachaConfirmWindow.CancelEvent(this.OnCancel);
            DataSource.Bind<ItemParam>(obj2, null);
            if (string.IsNullOrEmpty(ticket) != null)
            {
                goto Label_00B3;
            }
            param = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(ticket);
            if (param == null)
            {
                goto Label_00B3;
            }
            DataSource.Bind<ItemParam>(obj2, param);
        Label_00B3:
            return;
        }

        private GachaRequestParam CreateGachaRequest(string _iname)
        {
            GachaRequestParam param;
            GachaTopParamNew new2;
            string str;
            int num;
            string str2;
            string str3;
            int num2;
            string str4;
            GachaCostType type;
            bool flag;
            bool flag2;
            int num3;
            int num4;
            <CreateGachaRequest>c__AnonStorey347 storey;
            storey = new <CreateGachaRequest>c__AnonStorey347();
            storey._iname = _iname;
            param = null;
            if (string.IsNullOrEmpty(storey._iname) != null)
            {
                goto Label_011C;
            }
            new2 = this.mGachaListAll.Find(new Predicate<GachaTopParamNew>(storey.<>m__339));
            if (new2 != null)
            {
                goto Label_0050;
            }
            DebugUtility.LogError("前回の召喚リクエスト情報がありません");
            goto Label_011C;
        Label_0050:
            str = new2.iname;
            num = new2.Cost;
            str2 = new2.category;
            str3 = new2.ticket_iname;
            num2 = (string.IsNullOrEmpty(str3) == null) ? 1 : new2.num;
            str4 = new2.confirm;
            type = new2.CostType;
            flag = (string.IsNullOrEmpty(str3) == null) ? 0 : ((((new2.limit != null) || (new2.step != null)) || (new2.limit_cnt != null)) ? 0 : (new2.redraw == 0));
            flag2 = new2.IsFreePause;
            num3 = new2.redraw_rest;
            num4 = new2.redraw_num;
            param = this.CreateGachaRequest(str, num, str2, str3, num2, str4, type, flag, flag2, num3, num4);
            if (param != null)
            {
                goto Label_011C;
            }
            DebugUtility.LogError("召喚リクエストの生成に失敗しました.");
        Label_011C:
            return param;
        }

        private GachaRequestParam CreateGachaRequest(string _iname, int _cost, string _category, string _ticket, int _num, string _confirm, GachaCostType _cost_type, bool _is_use_onemore, bool _is_no_use_free, int _redraw_rest, int _redraw_num)
        {
            GachaCategory category;
            int num;
            string str;
            GachaRequestParam param;
            category = 0;
            if (string.IsNullOrEmpty(_category) != null)
            {
                goto Label_0036;
            }
            if (_category.Contains("gold") == null)
            {
                goto Label_0024;
            }
            category = 2;
            goto Label_0036;
        Label_0024:
            if (_category.Contains("coin") == null)
            {
                goto Label_0036;
            }
            category = 1;
        Label_0036:
            num = 0;
            if (((category != 1) || (_num != 1)) || ((_cost_type != 1) || (_is_no_use_free != null)))
            {
                goto Label_0077;
            }
            num = (MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaCoin() == null) ? 0 : 1;
            goto Label_00B6;
        Label_0077:
            if ((category != 2) || (_num != 1))
            {
                goto Label_00B6;
            }
            num = ((MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaGold() == null) || (MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaGoldMax() != null)) ? 0 : 1;
        Label_00B6:
            str = this.GetConfirmText(_cost, _cost_type == 2, _confirm);
            param = new GachaRequestParam(_iname, _cost, str, _cost_type, category, _is_use_onemore, 0);
            if (string.IsNullOrEmpty(_ticket) != null)
            {
                goto Label_00EA;
            }
            param.SetTicketInfo(_ticket, _num);
        Label_00EA:
            if (_redraw_rest <= 0)
            {
                goto Label_0104;
            }
            if (_redraw_num <= 0)
            {
                goto Label_0104;
            }
            param.SetRedraw(_redraw_rest, _redraw_num);
        Label_0104:
            param.SetFree(num);
            param.SetNum(_num);
            return param;
        }

        private void CreatePickupArtifactlist(ArtifactParam[] artifacts)
        {
            int num;
            ArtifactData data;
            this.mPickupArtifacts = new List<ArtifactData>();
            num = 0;
            goto Label_0032;
        Label_0012:
            data = this.CreateArtifactData(artifacts[num]);
            if (artifacts == null)
            {
                goto Label_002E;
            }
            this.mPickupArtifacts.Add(data);
        Label_002E:
            num += 1;
        Label_0032:
            if (num < ((int) artifacts.Length))
            {
                goto Label_0012;
            }
            return;
        }

        private void CreatePickupUnitsList(UnitParam[] units)
        {
            int num;
            UnitData data;
            this.mPickupUnits = new List<UnitData>();
            num = 0;
            goto Label_0032;
        Label_0012:
            data = this.CreateUnitData(units[num]);
            if (data == null)
            {
                goto Label_002E;
            }
            this.mPickupUnits.Add(data);
        Label_002E:
            num += 1;
        Label_0032:
            if (num < ((int) units.Length))
            {
                goto Label_0012;
            }
            return;
        }

        private RenderTexture CreateRenderTexture()
        {
            int num;
            num = Mathf.FloorToInt(864f);
            return RenderTexture.GetTemporary(num, num, 0x10, 7);
        }

        private UnitData CreateUnitData(UnitParam param)
        {
            UnitData data;
            Json_Unit unit;
            List<Json_Job> list;
            int num;
            int num2;
            JobSetParam param2;
            Json_Job job;
            data = new UnitData();
            unit = new Json_Unit();
            unit.iid = 1L;
            unit.iname = param.iname;
            unit.exp = 0;
            unit.lv = 1;
            unit.plus = 0;
            unit.rare = 0;
            unit.select = new Json_UnitSelectable();
            unit.select.job = 0L;
            unit.jobs = null;
            unit.abil = null;
            unit.abil = null;
            if (param.jobsets == null)
            {
                goto Label_011E;
            }
            if (((int) param.jobsets.Length) <= 0)
            {
                goto Label_011E;
            }
            list = new List<Json_Job>((int) param.jobsets.Length);
            num = 1;
            num2 = 0;
            goto Label_0103;
        Label_009A:
            param2 = MonoSingleton<GameManager>.Instance.GetJobSetParam(param.jobsets[num2]);
            if (param2 != null)
            {
                goto Label_00BB;
            }
            goto Label_00FD;
        Label_00BB:
            job = new Json_Job();
            job.iid = (long) num++;
            job.iname = param2.job;
            job.rank = 0;
            job.equips = null;
            job.abils = null;
            list.Add(job);
        Label_00FD:
            num2 += 1;
        Label_0103:
            if (num2 < ((int) param.jobsets.Length))
            {
                goto Label_009A;
            }
            unit.jobs = list.ToArray();
        Label_011E:
            data.Deserialize(unit);
            data.SetUniqueID(1L);
            data.JobRankUp(0);
            return data;
        }

        private void FadeUnitImage(float alphastart, float alphaend, float duration)
        {
            this.mBGUnitImgAlphaStart = alphastart;
            this.mBGUnitImgAlphaEnd = alphaend;
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

        private unsafe string GetConfirmText(int _cost, bool _ispaid, string _confirm)
        {
            object[] objArray1;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            str = null;
            str2 = TEXT_COST_GACHA_COIN;
            str3 = TEXT_CONFIRM_GACHA_COIN;
            if (_ispaid == null)
            {
                goto Label_001A;
            }
            str2 = TEXT_COST_GACHA_PAIDCOIN;
        Label_001A:
            str4 = LocalizedText.Get(str2);
            str5 = LocalizedText.Get(_confirm);
            if (string.IsNullOrEmpty(str5) == null)
            {
                goto Label_0051;
            }
            objArray1 = new object[] { str4, &_cost.ToString() };
            str5 = LocalizedText.Get(str3, objArray1);
        Label_0051:
            if (_cost != null)
            {
                goto Label_0063;
            }
            str5 = LocalizedText.Get(TEXT_CONFIRM_GACHA_COST_ZERO);
        Label_0063:
            str = str5;
            return str;
        }

        public GachaTopParamNew[] GetCurrentGachaLists(GachaTabCategory category)
        {
            if (category != 1)
            {
                goto Label_0013;
            }
            return this.mGachaListRare.ToArray();
        Label_0013:
            if (category != 4)
            {
                goto Label_0026;
            }
            return this.mGachaListNormal.ToArray();
        Label_0026:
            if (category != 2)
            {
                goto Label_0039;
            }
            return this.mGachaListArtifact.ToArray();
        Label_0039:
            if (category != 3)
            {
                goto Label_004C;
            }
            return this.mGachaListTicket.ToArray();
        Label_004C:
            if (category != 5)
            {
                goto Label_009C;
            }
            if (this.mGachaListSpecials == null)
            {
                goto Label_009C;
            }
            if (this.mCurrentTabSPIndex < 0)
            {
                goto Label_009C;
            }
            if (this.mGachaListSpecials.Count <= this.mCurrentTabSPIndex)
            {
                goto Label_009C;
            }
            return this.mGachaListSpecials[this.mCurrentTabSPIndex].lists.ToArray();
        Label_009C:
            return null;
        }

        private GachaTabCategory GetTabCategory(SRPG_Button tab)
        {
            GachaTabCategory category;
            SerializeValueBehaviour behaviour;
            if ((tab == null) == null)
            {
                goto Label_0018;
            }
            DebugUtility.LogError("召喚タブが指定されていませ.");
            return 0;
        Label_0018:
            category = 0;
            behaviour = tab.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_003F;
            }
            category = (byte) behaviour.list.GetInt("category");
        Label_003F:
            return category;
        }

        private void InitGachaArtifactPreview(bool reload)
        {
            if (string.IsNullOrEmpty(this.PickupPreviewArtifact) != null)
            {
                goto Label_0021;
            }
            this.mPreviewArtifact = GameObjectID.FindGameObject<Transform>(this.PickupPreviewArtifact);
        Label_0021:
            if (string.IsNullOrEmpty(this.PickUpCameraID) != null)
            {
                goto Label_0042;
            }
            this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
        Label_0042:
            if (reload == null)
            {
                goto Label_0056;
            }
            base.StartCoroutine(this.ReloadPickupArtifactPreview(1));
        Label_0056:
            return;
        }

        private void InitGachaUnitPreview(bool reload)
        {
            if (string.IsNullOrEmpty(this.PickupPreviewParentID) != null)
            {
                goto Label_0021;
            }
            this.mPreviewParent = GameObjectID.FindGameObject<Transform>(this.PickupPreviewParentID);
        Label_0021:
            if (string.IsNullOrEmpty(this.PickUpPreviewBaseID) != null)
            {
                goto Label_005F;
            }
            this.mPreviewBase = GameObjectID.FindGameObject(this.PickUpPreviewBaseID);
            if ((this.mPreviewBase != null) == null)
            {
                goto Label_005F;
            }
            this.mPreviewBase.SetActive(0);
        Label_005F:
            if (string.IsNullOrEmpty(this.PickUpCameraID) != null)
            {
                goto Label_0080;
            }
            this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
        Label_0080:
            if (reload == null)
            {
                goto Label_008C;
            }
            this.ReloadPickUpUnitView();
        Label_008C:
            return;
        }

        public bool IsFreePause(GachaTopParamNew[] _list)
        {
            bool flag;
            int num;
            flag = 0;
            num = 0;
            goto Label_0021;
        Label_0009:
            if (_list[num].IsFreePause == null)
            {
                goto Label_001D;
            }
            flag = 1;
            goto Label_002A;
        Label_001D:
            num += 1;
        Label_0021:
            if (num < ((int) _list.Length))
            {
                goto Label_0009;
            }
        Label_002A:
            return flag;
        }

        private bool IsGachaPending()
        {
            bool flag;
            string str;
            flag = 0;
            str = FlowNode_Variable.Get("REDRAW_GACHA_PENDING");
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_003B;
            }
            if ((str != "1") == null)
            {
                goto Label_0039;
            }
            if (GachaResultData.IsPending == null)
            {
                goto Label_003B;
            }
            flag = 1;
            goto Label_003B;
        Label_0039:
            flag = 1;
        Label_003B:
            return flag;
        }

        [DebuggerHidden]
        private IEnumerator LoadGachaBGTextures()
        {
            <LoadGachaBGTextures>c__Iterator115 iterator;
            iterator = new <LoadGachaBGTextures>c__Iterator115();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator LoadGachaTabSprites()
        {
            <LoadGachaTabSprites>c__Iterator116 iterator;
            iterator = new <LoadGachaTabSprites>c__Iterator116();
            iterator.<>f__this = this;
            return iterator;
        }

        private void OnCancel()
        {
            this.mClicked = 0;
            return;
        }

        private void OnCancel(GameObject dialog)
        {
            this.OnCancel();
            return;
        }

        private void OnClickDescription()
        {
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.mDescriptionURL);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e7);
            return;
        }

        private void OnClickDetail()
        {
            FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.mDetailURL);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3e7);
            return;
        }

        private void OnDecide()
        {
            FlowNode_ExecGacha2 gacha;
            this.mClicked = 0;
            gacha = base.GetComponentInParent<FlowNode_ExecGacha2>();
            if ((gacha == null) == null)
            {
                goto Label_001B;
            }
            return;
        Label_001B:
            if (this.m_CurrentGachaRequestParam != null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            DataSource.Bind<GachaRequestParam>(this.RootObject.get_parent().get_gameObject(), this.m_CurrentGachaRequestParam);
            this.mState.GotoState<State_PauseState>();
            gacha.OnExecGacha(this.m_CurrentGachaRequestParam);
            return;
        }

        private void OnDecide(GameObject dialog)
        {
            this.OnDecide();
            return;
        }

        private void OnDecideForTicketSelect()
        {
            int num;
            num = MonoSingleton<GachaManager>.Instance.UseTicketNum;
            if (this.m_CurrentGachaRequestParam == null)
            {
                goto Label_0022;
            }
            this.m_CurrentGachaRequestParam.SetNum(num);
        Label_0022:
            this.OnDecide();
            return;
        }

        private void OnDecisionRedrawGacha()
        {
            FlowNode_ExecGacha2 gacha;
            if (this.m_CurrentGachaRequestParam != null)
            {
                goto Label_005B;
            }
            if (GachaResultData.drops == null)
            {
                goto Label_0050;
            }
            if (GachaResultData.IsPending == null)
            {
                goto Label_0050;
            }
            this.m_CurrentGachaRequestParam = this.CreateGachaRequest(GachaResultData.receipt.iname);
            if (this.m_CurrentGachaRequestParam != null)
            {
                goto Label_005B;
            }
            DebugUtility.LogError("召喚リクエストがありません.");
            return;
            goto Label_005B;
        Label_0050:
            DebugUtility.LogError("前回の召喚リクエストが存在しません");
            return;
        Label_005B:
            gacha = base.GetComponentInParent<FlowNode_ExecGacha2>();
            if ((gacha == null) == null)
            {
                goto Label_0079;
            }
            DebugUtility.LogError("FlowNode_ExecGacha2がありません.");
            return;
        Label_0079:
            this.mState.GotoState<State_PauseState>();
            gacha.OnExecGachaDecision(this.m_CurrentGachaRequestParam);
            return;
        }

        private unsafe void OnDestroy()
        {
            string str;
            Dictionary<string, GameObject>.KeyCollection.Enumerator enumerator;
            GameUtility.DestroyGameObject(this.mCurrentPreview);
            this.mCurrentPreview = null;
            GameUtility.DestroyGameObjects<GachaUnitPreview>(this.mPreviewControllers);
            if ((this.mPreviewUnitRT != null) == null)
            {
                goto Label_0040;
            }
            RenderTexture.ReleaseTemporary(this.mPreviewUnitRT);
            this.mPreviewUnitRT = null;
        Label_0040:
            GameUtility.DestroyGameObject(this.mCurrentArtifactPreview);
            this.mCurrentArtifactPreview = null;
            GameUtility.DestroyGameObjects<Transform>(this.mPreviewArtifactControllers);
            this.ClearTabSprites();
            GameUtility.DestroyGameObjects<SRPG_Button>(this.mTabList.ToArray());
            this.mTabList.Clear();
            this.mDefaultBG = null;
            this.ClearBGSprites();
            enumerator = this.mTicketButtonLists.Keys.GetEnumerator();
        Label_009C:
            try
            {
                goto Label_00BA;
            Label_00A1:
                str = &enumerator.Current;
                GameUtility.DestroyGameObject(this.mTicketButtonLists[str]);
            Label_00BA:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00A1;
                }
                goto Label_00D7;
            }
            finally
            {
            Label_00CB:
                ((Dictionary<string, GameObject>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_00D7:
            this.mTicketButtonLists = null;
            return;
        }

        private void OnEnable()
        {
            if ((HomeWindow.Current != null) == null)
            {
                goto Label_001B;
            }
            HomeWindow.Current.SetVisible(1);
        Label_001B:
            return;
        }

        private void OnExecGacha(string iname, string input, int cost, string type, string category, string ticket, int num, bool isConfirm, string confirm, GachaCostType cost_type, bool isUseOneMore, bool isNoUseFree)
        {
            this.OnExecGacha2(iname, cost, category, ticket, num, confirm, cost_type, isUseOneMore, isNoUseFree, 0, 0);
            return;
        }

        private void OnExecGacha2(string _iname, int _cost, string _category, string _ticket, int _num, string _confirm, GachaCostType _cost_type, bool _is_use_onemore, bool _is_no_use_free, int _redraw_rest, int _redraw_num)
        {
            GachaRequestParam param;
            GachaTopParamNew new2;
            <OnExecGacha2>c__AnonStorey346 storey;
            storey = new <OnExecGacha2>c__AnonStorey346();
            storey._iname = _iname;
            if ((this.Initialized != null) && (this.Clicked == null))
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            this.mClicked = 1;
            DataSource.Bind<GachaRequestParam>(this.RootObject.get_parent().get_gameObject(), null);
            param = this.CreateGachaRequest(storey._iname, _cost, _category, _ticket, _num, _confirm, _cost_type, _is_use_onemore, _is_no_use_free, _redraw_rest, _redraw_num);
            this.m_CurrentGachaRequestParam = param;
            if ((_cost_type != 3) && (param.IsFree == null))
            {
                goto Label_0085;
            }
            this.OnDecide();
            goto Label_00FA;
        Label_0085:
            if (param.IsTicketGacha == null)
            {
                goto Label_00EB;
            }
            FlowNode_Variable.Set("USE_TICKET_INAME", _ticket);
            new2 = this.mGachaListTicket.Find(new Predicate<GachaTopParamNew>(storey.<>m__338));
            if (new2 == null)
            {
                goto Label_00DE;
            }
            FlowNode_Variable.Set("USE_TICKET_MAX", (new2.redraw == null) ? string.Empty : "1");
        Label_00DE:
            FlowNode_GameObject.ActivateOutputLinks(this, 110);
            goto Label_00FA;
        Label_00EB:
            base.StartCoroutine(this.CreateConfirm(param, 1));
        Label_00FA:
            return;
        }

        private void OnSelectTicket(int index)
        {
            GachaTopParamNew new2;
            string str;
            int num;
            string str2;
            string str3;
            int num2;
            string str4;
            int num3;
            int num4;
            new2 = this.mGachaListTicket[index];
            str = new2.iname;
            num = 0;
            str2 = string.Empty;
            str3 = this.mGachaListTicket[index].ticket_iname;
            num2 = 1;
            str4 = this.mGachaListTicket[index].confirm;
            num3 = new2.redraw_rest;
            num4 = new2.redraw_num;
            this.OnExecGacha2(str, num, str2, str3, num2, str4, 4, 1, 0, num3, num4);
            return;
        }

        private void OnTabChange(SRPG_Button button, GachaTabCategory category, int index)
        {
            if (this.TabChange(button, category, index) != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            this.RefreshGachaDetailSelectID(this.mSelectTab);
            this.mState.GotoState<State_CheckInitState>();
            return;
        }

        private void PlayChangeEffect()
        {
            Animator animator;
            if (this.mSelectTab == 1)
            {
                goto Label_0019;
            }
            if (this.mSelectTab == 2)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            animator = null;
            if (this.mSelectTab != 1)
            {
                goto Label_0038;
            }
            animator = this.ChangeUnitEffectObj.GetComponent<Animator>();
            goto Label_0050;
        Label_0038:
            if (this.mSelectTab != 2)
            {
                goto Label_0050;
            }
            animator = this.ChangeArtifactEffectObj.GetComponent<Animator>();
        Label_0050:
            if ((animator == null) == null)
            {
                goto Label_005D;
            }
            return;
        Label_005D:
            animator.ResetTrigger("onResetTrigger");
            if (this.ChangeJob == null)
            {
                goto Label_0083;
            }
            animator.SetTrigger("onChangeJobF");
            goto Label_008E;
        Label_0083:
            animator.SetTrigger("onChangeUnitF");
        Label_008E:
            return;
        }

        private void Refresh()
        {
            this.SetupGachaList(MonoSingleton<GameManager>.Instance.Gachas);
            this.SetupTabList();
            this.RefreshTabList();
            this.RefreshTabState(this.mCurrentTabIndex, this.mCurrentTabSPIndex);
            this.RefreshTabActive(1);
            this.RefreshDefaultPanel();
            this.RefreshTicketButtonList();
            this.RefreshGachaState();
            this.RefreshSummonCoin();
            this.mClicked = 0;
            return;
        }

        private unsafe void RefreshArtifactInfo()
        {
            object[] objArray1;
            List<AbilityData> list;
            BaseStatus status;
            BaseStatus status2;
            this.UpdateCurrentArtifactInfo();
            if (this.mCurrentArtifact == null)
            {
                goto Label_014C;
            }
            list = this.mCurrentArtifact.LearningAbilities;
            DataSource.Bind<AbilityData>(this.WeaponAbilityInfo, null);
            if (list == null)
            {
                goto Label_005E;
            }
            if (list.Count <= 0)
            {
                goto Label_005E;
            }
            this.WeaponAbilityInfo.SetActive(1);
            DataSource.Bind<AbilityData>(this.WeaponAbilityInfo, list[0]);
            goto Label_006A;
        Label_005E:
            this.WeaponAbilityInfo.SetActive(0);
        Label_006A:
            DataSource.Bind<ArtifactParam>(this.ArtifactInfoPanel, this.mCurrentArtifact.ArtifactParam);
            if ((this.ArtifactRarityPanel != null) == null)
            {
                goto Label_00A2;
            }
            DataSource.Bind<ArtifactData>(this.ArtifactRarityPanel, this.mCurrentArtifact);
        Label_00A2:
            GameParameter.UpdateAll(this.ArtifactInfoPanel);
            if ((this.Status != null) == null)
            {
                goto Label_00F5;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_00F5;
            }
            status = new BaseStatus();
            status2 = new BaseStatus();
            this.mCurrentArtifact.GetHomePassiveBuffStatus(&status, &status2, null, 0, 1);
            this.Status.SetValues(status, status2, 0);
        Label_00F5:
            if ((this.ArtifactType != null) == null)
            {
                goto Label_013F;
            }
            if (this.mCurrentArtifact == null)
            {
                goto Label_013F;
            }
            objArray1 = new object[] { this.mCurrentArtifact.ArtifactParam.tag };
            this.ArtifactType.set_text(LocalizedText.Get("sys.TITLE_ARTIFACT_TYPE", objArray1));
        Label_013F:
            this.InitGachaArtifactPreview(1);
            this.SetGachaPreviewArtifactCamera();
        Label_014C:
            return;
        }

        private void RefreshArtifactInfoPanel()
        {
            if (this.mSelectTab == 2)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (this.mGachaListArtifact == null)
            {
                goto Label_0029;
            }
            if (this.mGachaListArtifact.Count > 0)
            {
                goto Label_002A;
            }
        Label_0029:
            return;
        Label_002A:
            this.CreatePickupArtifactlist(this.mGachaListArtifact[0].artifacts.ToArray());
            if (this.mPickupArtifacts == null)
            {
                goto Label_0062;
            }
            if (this.mPickupArtifacts.Count > 0)
            {
                goto Label_0063;
            }
        Label_0062:
            return;
        Label_0063:
            this.mCurrentPickupArtIndex = 0;
            this.RefreshArtifactInfo();
            return;
        }

        private unsafe void RefreshButtonPanel()
        {
            char[] chArray1;
            GachaTopParamNew[] newArray;
            int num;
            int num2;
            GameObject obj2;
            GachaButton button;
            GachaButton button2;
            List<GachaButton>.Enumerator enumerator;
            int num3;
            GachaButton button3;
            string str;
            string str2;
            int num4;
            int num5;
            int num6;
            int num7;
            bool flag;
            GachaCategory category;
            GachaButtonParam param;
            string str3;
            List<GachaBonusParam> list;
            GameObject obj3;
            string str4;
            GachaBonusParam param2;
            List<GachaBonusParam>.Enumerator enumerator2;
            string[] strArray;
            ItemParam param3;
            StringBuilder builder;
            UnitParam param4;
            UnitData data;
            StringBuilder builder2;
            ArtifactParam param5;
            StringBuilder builder3;
            ConceptCardData data2;
            ConceptCardIcon icon;
            StringBuilder builder4;
            <RefreshButtonPanel>c__AnonStorey344 storey;
            newArray = this.GetCurrentGachaLists(this.mSelectTab);
            if ((newArray != null) && (((int) newArray.Length) > 0))
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            if ((this.GachaButtonTemplate == null) == null)
            {
                goto Label_0039;
            }
            DebugUtility.LogError("召喚ボタンのテンプレートが指定されていません.");
            return;
        Label_0039:
            this.BonusPanel.SetActive(0);
            if ((this.BonusPanelItem != null) == null)
            {
                goto Label_0062;
            }
            this.BonusPanelItem.SetActive(0);
        Label_0062:
            if ((this.BonusPanelUnit != null) == null)
            {
                goto Label_007F;
            }
            this.BonusPanelUnit.SetActive(0);
        Label_007F:
            if ((this.BonusPanelArtifact != null) == null)
            {
                goto Label_009C;
            }
            this.BonusPanelArtifact.SetActive(0);
        Label_009C:
            if ((this.BonusPanelConceptCard != null) == null)
            {
                goto Label_00B9;
            }
            this.BonusPanelConceptCard.SetActive(0);
        Label_00B9:
            if ((this.BonusMsgPanel != null) == null)
            {
                goto Label_00D6;
            }
            this.BonusMsgPanel.SetActive(0);
        Label_00D6:
            if (this.m_GachaButtons.Count >= ((int) newArray.Length))
            {
                goto Label_0143;
            }
            num = ((int) newArray.Length) - this.m_GachaButtons.Count;
            num2 = 0;
            goto Label_013C;
        Label_0100:
            obj2 = Object.Instantiate<GameObject>(this.GachaButtonTemplate);
            obj2.get_transform().SetParent(this.ButtonPanel.get_transform(), 0);
            button = obj2.GetComponent<GachaButton>();
            this.m_GachaButtons.Add(button);
            num2 += 1;
        Label_013C:
            if (num2 < num)
            {
                goto Label_0100;
            }
        Label_0143:
            enumerator = this.m_GachaButtons.GetEnumerator();
        Label_0150:
            try
            {
                goto Label_016B;
            Label_0155:
                button2 = &enumerator.Current;
                button2.get_gameObject().SetActive(0);
            Label_016B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0155;
                }
                goto Label_0189;
            }
            finally
            {
            Label_017C:
                ((List<GachaButton>.Enumerator) enumerator).Dispose();
            }
        Label_0189:
            num3 = 0;
            goto Label_045E;
        Label_0191:
            storey = new <RefreshButtonPanel>c__AnonStorey344();
            storey.<>f__this = this;
            button3 = this.m_GachaButtons[num3];
            storey.param = newArray[num3];
            storey.cost = -1;
            str = string.Empty;
            str2 = string.Empty;
            num4 = 0;
            num5 = 0;
            num6 = 0;
            storey.exec_num = 0;
            num7 = 0;
            flag = 1;
            storey.is_nouse_free = 0;
            storey.cost_type = storey.param.CostType;
            if (storey.cost_type != 1)
            {
                goto Label_0227;
            }
            storey.cost = storey.param.coin;
            goto Label_026C;
        Label_0227:
            if (storey.cost_type != 2)
            {
                goto Label_024C;
            }
            storey.cost = storey.param.coin_p;
            goto Label_026C;
        Label_024C:
            if (storey.cost_type != 3)
            {
                goto Label_026C;
            }
            storey.cost = storey.param.gold;
        Label_026C:
            category = storey.param.Category;
            num4 = storey.param.step_index;
            num5 = storey.param.step_num;
            num6 = storey.param.ticket_num;
            storey.exec_num = storey.param.num;
            num7 = storey.param.appeal_type;
            str = storey.param.btext;
            str2 = storey.param.appeal_message;
            flag = storey.param.IsStepUpUIHide == 0;
            storey.is_nouse_free = storey.param.IsFreePause;
            param = new GachaButtonParam(storey.cost, num4, num5, num6, storey.exec_num, num7, str, str2, flag, storey.is_nouse_free, storey.cost_type, category);
            button3.SetupGachaButtonParam(param);
            this.m_GachaButtons[num3].UpdateTrigger = 1;
            this.m_GachaButtons[num3].get_gameObject().SetActive(1);
            storey.iname = string.Empty;
            storey.category = string.Empty;
            storey.ticket = string.Empty;
            storey.confirm = string.Empty;
            storey.isUseOnemore = 0;
            storey.iname = storey.param.iname;
            storey.confirm = storey.param.confirm;
            storey.ticket = storey.param.ticket_iname;
            storey.category = storey.param.category;
            storey.isUseOnemore = (((storey.param.limit != null) || (storey.param.step != null)) || ((storey.param.limit_cnt != null) || (storey.param.redraw != null))) ? 0 : 1;
            button3.SetGachaButtonEvent(new UnityAction(storey, this.<>m__335));
            num3 += 1;
        Label_045E:
            if (num3 < ((int) newArray.Length))
            {
                goto Label_0191;
            }
            if (((int) newArray.Length) != 1)
            {
                goto Label_0872;
            }
            if (string.IsNullOrEmpty(newArray[0].bonus_msg) != null)
            {
                goto Label_04E8;
            }
            if ((this.BonusMsgText != null) == null)
            {
                goto Label_04BA;
            }
            str3 = newArray[0].bonus_msg.Replace("<br>", "\n");
            this.BonusMsgText.set_text(str3);
        Label_04BA:
            if ((this.BonusMsgPanel != null) == null)
            {
                goto Label_04E7;
            }
            this.BonusMsgPanel.get_transform().SetAsLastSibling();
            this.BonusMsgPanel.SetActive(1);
        Label_04E7:
            return;
        Label_04E8:
            list = newArray[0].bonus_items;
            if (list == null)
            {
                goto Label_0872;
            }
            if (list.Count <= 0)
            {
                goto Label_0872;
            }
            DataSource.Bind<ItemParam>(this.BonusPanel, null);
            DataSource.Bind<UnitParam>(this.BonusPanel, null);
            DataSource.Bind<ArtifactParam>(this.BonusPanel, null);
            obj3 = null;
            str4 = string.Empty;
            enumerator2 = list.GetEnumerator();
        Label_053D:
            try
            {
                goto Label_082D;
            Label_0542:
                param2 = &enumerator2.Current;
                if (param2 == null)
                {
                    goto Label_0563;
                }
                if (string.IsNullOrEmpty(param2.iname) == null)
                {
                    goto Label_0572;
                }
            Label_0563:
                DebugUtility.LogError("オマケ表示に必要な情報が設定されていません");
                goto Label_082D;
            Label_0572:
                chArray1 = new char[] { 0x5f };
                strArray = param2.iname.Split(chArray1);
                obj3 = null;
                str4 = string.Empty;
                if ((strArray[0] == "IT") == null)
                {
                    goto Label_062F;
                }
                param3 = MonoSingleton<GameManager>.Instance.GetItemParam(param2.iname);
                if (param3 != null)
                {
                    goto Label_05C7;
                }
                goto Label_082D;
            Label_05C7:
                builder = GameUtility.GetStringBuilder();
                builder.Append(param3.name);
                builder.Append("x" + &param2.num.ToString());
                str4 = builder.ToString();
                if ((this.BonusPanel != null) == null)
                {
                    goto Label_0622;
                }
                DataSource.Bind<ItemParam>(this.BonusPanel, param3);
            Label_0622:
                obj3 = this.BonusPanelItem;
                goto Label_07FA;
            Label_062F:
                if ((strArray[0] == "UN") == null)
                {
                    goto Label_06C1;
                }
                param4 = MonoSingleton<GameManager>.Instance.GetUnitParam(param2.iname);
                if (param4 != null)
                {
                    goto Label_0661;
                }
                goto Label_082D;
            Label_0661:
                data = this.CreateUnitData(param4);
                if (data != null)
                {
                    goto Label_0677;
                }
                goto Label_082D;
            Label_0677:
                builder2 = GameUtility.GetStringBuilder();
                builder2.Append(param4.name);
                str4 = builder2.ToString();
                if ((this.BonusPanel != null) == null)
                {
                    goto Label_06B4;
                }
                DataSource.Bind<UnitData>(this.BonusPanel, data);
            Label_06B4:
                obj3 = this.BonusPanelUnit;
                goto Label_07FA;
            Label_06C1:
                if ((strArray[0] == "AF") == null)
                {
                    goto Label_0760;
                }
                param5 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(param2.iname);
                if (param5 != null)
                {
                    goto Label_06F8;
                }
                goto Label_082D;
            Label_06F8:
                builder3 = GameUtility.GetStringBuilder();
                builder3.Append(param5.name);
                builder3.Append("x" + &param2.num.ToString());
                str4 = builder3.ToString();
                if ((this.BonusPanel != null) == null)
                {
                    goto Label_0753;
                }
                DataSource.Bind<ArtifactParam>(this.BonusPanel, param5);
            Label_0753:
                obj3 = this.BonusPanelArtifact;
                goto Label_07FA;
            Label_0760:
                if ((strArray[0] == "TS") == null)
                {
                    goto Label_07FA;
                }
                data2 = ConceptCardData.CreateConceptCardDataForDisplay(param2.iname);
                icon = this.BonusPanelConceptCard.GetComponent<ConceptCardIcon>();
                if (data2 == null)
                {
                    goto Label_082D;
                }
                if ((icon == null) == null)
                {
                    goto Label_07A7;
                }
                goto Label_082D;
            Label_07A7:
                builder4 = GameUtility.GetStringBuilder();
                builder4.Append(data2.Param.name);
                builder4.Append("x" + &param2.num.ToString());
                str4 = builder4.ToString();
                icon.Setup(data2);
                obj3 = this.BonusPanelConceptCard;
            Label_07FA:
                if ((this.BonusItemName != null) == null)
                {
                    goto Label_0818;
                }
                this.BonusItemName.set_text(str4);
            Label_0818:
                if ((obj3 != null) == null)
                {
                    goto Label_082D;
                }
                obj3.SetActive(1);
            Label_082D:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0542;
                }
                goto Label_084B;
            }
            finally
            {
            Label_083E:
                ((List<GachaBonusParam>.Enumerator) enumerator2).Dispose();
            }
        Label_084B:
            GameParameter.UpdateAll(this.BonusPanel);
            this.BonusPanel.get_transform().SetAsLastSibling();
            this.BonusPanel.SetActive(1);
        Label_0872:
            return;
        }

        private void RefreshDefaultPanel()
        {
            if ((this.UnitInfoPanel != null) == null)
            {
                goto Label_0025;
            }
            this.UnitInfoPanel.SetActive(this.mSelectTab == 1);
        Label_0025:
            if ((this.ArtifactInfoPanel != null) == null)
            {
                goto Label_004A;
            }
            this.ArtifactInfoPanel.SetActive(this.mSelectTab == 2);
        Label_004A:
            if ((this.BonusPanel != null) == null)
            {
                goto Label_006F;
            }
            this.BonusPanel.SetActive(this.mSelectTab == 5);
        Label_006F:
            if ((this.BGRoot != null) == null)
            {
                goto Label_0086;
            }
            this.RefreshGachaBackGround();
        Label_0086:
            this.RefreshUnitInfoPanel();
            this.RefreshArtifactInfoPanel();
            this.RefreshButtonPanel();
            this.RefreshPreviews();
            return;
        }

        private void RefreshGachaBackGround()
        {
            GachaTopParamNew[] newArray;
            string str;
            if (this.mSelectTab != 3)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((this.BGRoot == null) == null)
            {
                goto Label_0029;
            }
            DebugUtility.LogError("=== GachaWindow->RefreshGachaBackGround:BGRoot is null ===");
            return;
        Label_0029:
            newArray = this.GetCurrentGachaLists(this.mSelectTab);
            if (newArray == null)
            {
                goto Label_0045;
            }
            if (((int) newArray.Length) > 0)
            {
                goto Label_0050;
            }
        Label_0045:
            DebugUtility.LogError("=== GachaWindow->RefreshGachaBackGround:param is Null ===");
            return;
        Label_0050:
            str = newArray[0].asset_bg;
            this.IsRefreshingGachaBG = 1;
            this.RefreshGachaBGImage(str);
            return;
        }

        private void RefreshGachaBGImage(string image)
        {
            RawImage image2;
            RawImage image3;
            CanvasGroup group;
            CanvasGroup group2;
            Texture2D textured;
            Texture2D textured2;
            <RefreshGachaBGImage>c__AnonStorey345 storey;
            storey = new <RefreshGachaBGImage>c__AnonStorey345();
            if ((((this.mDefaultBG == null) == null) || (this.mCacheBGImages == null)) || (this.mCacheBGImages.Count <= 0))
            {
                goto Label_006F;
            }
            DebugUtility.Log("=== GachaWindow.cs->RefreshGachaBGImage:mDefaultBG Initalize");
            this.mDefaultBG = (this.mCacheBGImages.ContainsKey(DEFAULT_BGIMAGE_PATH) == null) ? null : this.mCacheBGImages[DEFAULT_BGIMAGE_PATH];
        Label_006F:
            storey.bg00 = ((this.mBGObjects == null) || (this.mBGObjects.Count < 2)) ? this.BGRoot.FindChild("bg00") : this.mBGObjects[0];
            storey.bg01 = ((this.mBGObjects == null) || (this.mBGObjects.Count < 2)) ? this.BGRoot.FindChild("bg01") : this.mBGObjects[1];
            if (((storey.bg00 == null) == null) && ((storey.bg01 == null) == null))
            {
                goto Label_012D;
            }
            DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():bg00 or bg01 is Null ===");
            this.IsRefreshingGachaBG = 0;
            return;
        Label_012D:
            image2 = storey.bg00.GetComponent<RawImage>();
            image3 = storey.bg01.GetComponent<RawImage>();
            if (((image2 == null) == null) && ((image3 == null) == null))
            {
                goto Label_0171;
            }
            DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():bg00_raw or bg01_raw is Null ===");
            this.IsRefreshingGachaBG = 0;
            return;
        Label_0171:
            group = storey.bg00.get_gameObject().GetComponent<CanvasGroup>();
            group2 = storey.bg01.get_gameObject().GetComponent<CanvasGroup>();
            if (((group == null) == null) && ((group2 == null) == null))
            {
                goto Label_01BF;
            }
            DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():canvas00 or canvas01 is Null ===");
            this.IsRefreshingGachaBG = 0;
            return;
        Label_01BF:
            if (this.mBGObjects.FindIndex(new Predicate<Transform>(storey.<>m__336)) != -1)
            {
                goto Label_01EF;
            }
            this.mBGObjects.Add(storey.bg00);
        Label_01EF:
            if (this.mBGObjects.FindIndex(new Predicate<Transform>(storey.<>m__337)) != -1)
            {
                goto Label_021F;
            }
            this.mBGObjects.Add(storey.bg01);
        Label_021F:
            if (((this.mCacheBGImages == null) || (this.mCacheBGImages.Count <= 0)) || (string.IsNullOrEmpty(image) != null))
            {
                goto Label_02DA;
            }
            textured = this.mDefaultBG;
            textured2 = null;
            textured = (this.mCacheBGImages.ContainsKey(image + "_0") == null) ? textured : this.mCacheBGImages[image + "_0"];
            textured2 = (this.mCacheBGImages.ContainsKey(image + "_1") == null) ? textured2 : this.mCacheBGImages[image + "_1"];
            image2.set_texture(textured);
            image3.set_texture(textured2);
            goto Label_02ED;
        Label_02DA:
            image2.set_texture(this.mDefaultBG);
            image3.set_texture(null);
        Label_02ED:
            group.set_alpha(1f);
            group2.set_alpha(0f);
            this.mExistSwapBG = image3.get_texture() != null;
            this.mWaitSwapBGTime = WAIT_SWAP_BG;
            this.mEnableBGIndex = 0;
            this.IsRefreshingGachaBG = 0;
            return;
        }

        private void RefreshGachaDetailSelectID(GachaTabCategory category)
        {
            GachaTopParamNew[] newArray;
            StringBuilder builder;
            FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.TITLE_POPUP_GACHA_DETAIL"));
            newArray = this.GetCurrentGachaLists(category);
            if (newArray == null)
            {
                goto Label_0064;
            }
            if (((int) newArray.Length) <= 0)
            {
                goto Label_0064;
            }
            builder = new StringBuilder();
            builder.Append(Network.SiteHost);
            builder.Append(GACHA_URL_PREFIX);
            builder.Append(newArray[0].detail_url);
            this.mDetailURL = builder.ToString();
        Label_0064:
            return;
        }

        private void RefreshGachaState()
        {
            int num;
            bool flag;
            if (this.mSelectTab != 1)
            {
                goto Label_0032;
            }
            this.mCurrentIndex = this.mCurrentJobIndex = 0;
            this.UpdateCurrentUnitInfo();
            this.mState.GotoState<State_WaitActionAnimation>();
            goto Label_0059;
        Label_0032:
            if (this.mSelectTab != 2)
            {
                goto Label_0059;
            }
            flag = 0;
            this.ChangeJob = flag;
            this.ChangeUnit = flag;
            this.mState.GotoState<State_WaitActionAnimation>();
        Label_0059:
            return;
        }

        private void RefreshJobs()
        {
            if ((this.SelectJobIcon == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.SelectJobIcon.GetComponent<GameParameter>().Index = this.mCurrentJobIndex;
            return;
        }

        private void RefreshPreviews()
        {
            if ((this.mPreviewBase != null) == null)
            {
                goto Label_003E;
            }
            if (this.mPreviewBase.get_activeSelf() != null)
            {
                goto Label_003E;
            }
            GameUtility.SetLayer(this.mPreviewBase, GameUtility.LayerCH, 1);
            this.mPreviewBase.SetActive(1);
        Label_003E:
            this.mDesiredPreviewVisibility = 1;
            this.mUpdatePreviewVisibility = 1;
            return;
        }

        private void RefreshSummonCoin()
        {
            List<EventCoinData> list;
            EventCoinData data;
            if ((this.SummonCoin != null) == null)
            {
                goto Label_006B;
            }
            MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
            if (<>f__am$cache7C != null)
            {
                goto Label_0049;
            }
            <>f__am$cache7C = new Predicate<EventCoinData>(GachaWindow.<RefreshSummonCoin>m__327);
        Label_0049:
            data = MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find(<>f__am$cache7C);
            DataSource.Bind<EventCoinData>(this.SummonCoin, data);
            GameParameter.UpdateAll(this.SummonCoin);
        Label_006B:
            return;
        }

        private void RefreshTabActive(bool value)
        {
            SRPG_Button[] buttonArray;
            SRPG_Button button;
            SRPG_Button[] buttonArray2;
            int num;
            buttonArray2 = this.mTabList.ToArray();
            num = 0;
            goto Label_0029;
        Label_0015:
            button = buttonArray2[num];
            button.get_gameObject().SetActive(value);
            num += 1;
        Label_0029:
            if (num < ((int) buttonArray2.Length))
            {
                goto Label_0015;
            }
            return;
        }

        private void RefreshTabEnable(bool state)
        {
            SRPG_Button[] buttonArray;
            SRPG_Button button;
            SRPG_Button[] buttonArray2;
            int num;
            GachaTabListItem item;
            buttonArray2 = this.mTabList.ToArray();
            num = 0;
            goto Label_004A;
        Label_0015:
            button = buttonArray2[num];
            item = button.GetComponent<GachaTabListItem>();
            if ((item != null) == null)
            {
                goto Label_003F;
            }
            if (item.Disabled == null)
            {
                goto Label_003F;
            }
            goto Label_0046;
        Label_003F:
            button.set_enabled(state);
        Label_0046:
            num += 1;
        Label_004A:
            if (num < ((int) buttonArray2.Length))
            {
                goto Label_0015;
            }
            return;
        }

        private void RefreshTabList()
        {
            bool flag;
            int num;
            int num2;
            flag = MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaCoin();
            num = -1;
            if (flag == null)
            {
                goto Label_006A;
            }
            num = this.mTabList.FindIndex(new Predicate<SRPG_Button>(this.<RefreshTabList>m__331));
            if (num == -1)
            {
                goto Label_0055;
            }
            this.mTabList.RemoveAt(num);
            this.mTabList.Insert(0, this.RareTab);
        Label_0055:
            this.RareTab.get_transform().SetAsFirstSibling();
            goto Label_00D8;
        Label_006A:
            num = this.mTabList.FindIndex(new Predicate<SRPG_Button>(this.<RefreshTabList>m__332));
            num2 = -1;
            if (num == -1)
            {
                goto Label_00C8;
            }
            this.mTabList.RemoveAt(num);
            num2 = this.mTabList.FindIndex(new Predicate<SRPG_Button>(this.<RefreshTabList>m__333));
            if (num2 == -1)
            {
                goto Label_00C8;
            }
            this.mTabList.Insert(num2, this.RareTab);
        Label_00C8:
            this.RareTab.get_transform().SetAsLastSibling();
        Label_00D8:
            this.TicketTab.get_transform().SetAsLastSibling();
            this.NormalTab.get_transform().SetAsLastSibling();
            return;
        }

        private void RefreshTabState(int index, int sp_index)
        {
            SRPG_Button button;
            GachaTopParamNew new2;
            int num;
            SRPG_Button button2;
            GachaTopParamNew new3;
            GachaTabListItem item;
            SRPG_Button[] buttonArray;
            int num2;
            Transform transform;
            if (index < 0)
            {
                goto Label_0018;
            }
            if (index < this.mTabList.Count)
            {
                goto Label_001E;
            }
        Label_0018:
            index = 0;
            sp_index = 0;
        Label_001E:
            button = this.mTabList[index];
            new2 = DataSource.FindDataOfClass<GachaTopParamNew>(button.get_gameObject(), null);
            if (new2 == null)
            {
                goto Label_00D7;
            }
            if (new2.disabled == null)
            {
                goto Label_00D7;
            }
            num = 0;
            goto Label_00C1;
        Label_0050:
            if (num != index)
            {
                goto Label_005C;
            }
            goto Label_00BD;
        Label_005C:
            button2 = this.mTabList[num];
            new3 = DataSource.FindDataOfClass<GachaTopParamNew>(button2.get_gameObject(), null);
            if (new3 == null)
            {
                goto Label_00BD;
            }
            if (new3.disabled != null)
            {
                goto Label_00BD;
            }
            index = num;
            item = button2.GetComponent<GachaTabListItem>();
            if ((item != null) == null)
            {
                goto Label_00F3;
            }
            if (item.ListIndex < 0)
            {
                goto Label_00F3;
            }
            sp_index = item.ListIndex;
            goto Label_00D2;
        Label_00BD:
            num += 1;
        Label_00C1:
            if (num < this.mTabList.Count)
            {
                goto Label_0050;
            }
        Label_00D2:
            goto Label_00F3;
        Label_00D7:
            if (new2 != null)
            {
                goto Label_00F3;
            }
            index = Mathf.Max(0, index - 1);
            sp_index = Mathf.Max(0, sp_index - 1);
        Label_00F3:
            buttonArray = this.mTabList.ToArray();
            num2 = 0;
            goto Label_01C0;
        Label_0108:
            if ((buttonArray[num2] == null) == null)
            {
                goto Label_011D;
            }
            goto Label_01BA;
        Label_011D:
            transform = buttonArray[num2].get_transform().Find("cursor");
            if (num2 != index)
            {
                goto Label_017D;
            }
            if ((transform != null) == null)
            {
                goto Label_0155;
            }
            transform.get_gameObject().SetActive(1);
        Label_0155:
            buttonArray[num2].get_transform().set_localScale(new Vector3(1f, 1f, 1f));
            goto Label_01BA;
        Label_017D:
            if ((transform != null) == null)
            {
                goto Label_0197;
            }
            transform.get_gameObject().SetActive(0);
        Label_0197:
            buttonArray[num2].get_transform().set_localScale(new Vector3(0.9f, 0.9f, 0.9f));
        Label_01BA:
            num2 += 1;
        Label_01C0:
            if (num2 < ((int) buttonArray.Length))
            {
                goto Label_0108;
            }
            this.mCurrentTabIndex = Mathf.Max(index, 0);
            this.mCurrentTabSPIndex = Mathf.Max(sp_index, 0);
            this.mSelectTab = this.GetTabCategory(this.mTabList[index]);
            this.RefreshGachaDetailSelectID(this.mSelectTab);
            return;
        }

        private void RefreshTicketButtonList()
        {
            int num;
            int num2;
            GameObject obj2;
            <RefreshTicketButtonList>c__AnonStorey33F storeyf;
            <RefreshTicketButtonList>c__AnonStorey340 storey;
            storeyf = new <RefreshTicketButtonList>c__AnonStorey33F();
            storeyf.<>f__this = this;
            if (this.mSelectTab == 3)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            storeyf.iname = this.m_CurrentGachaRequestParam.Iname;
            if (string.IsNullOrEmpty(storeyf.iname) != null)
            {
                goto Label_0193;
            }
            if (this.mTicketButtonLists.ContainsKey(storeyf.iname) == null)
            {
                goto Label_0193;
            }
            if (this.mGachaListTicket == null)
            {
                goto Label_006D;
            }
            if (this.mGachaListTicket.Count > 0)
            {
                goto Label_0091;
            }
        Label_006D:
            this.TicketNotListView.SetActive(1);
            this.mTicketButtonLists[storeyf.iname].SetActive(0);
            return;
        Label_0091:
            if (this.mGachaListTicket.FindIndex(new Predicate<GachaTopParamNew>(storeyf.<>m__328)) >= 0)
            {
                goto Label_0193;
            }
            GameUtility.DestroyGameObject(this.mTicketButtonLists[storeyf.iname]);
            this.mTicketButtonLists.Remove(storeyf.iname);
            num2 = 0;
            goto Label_0182;
        Label_00DF:
            if (this.mTicketButtonLists.ContainsKey(this.mGachaListTicket[num2].iname) == null)
            {
                goto Label_017E;
            }
            storey = new <RefreshTicketButtonList>c__AnonStorey340();
            storey.<>f__this = this;
            obj2 = this.mTicketButtonLists[this.mGachaListTicket[num2].iname];
            storey.ticketlistitem = obj2.GetComponent<GachaTicketListItem>();
            if ((storey.ticketlistitem != null) == null)
            {
                goto Label_017E;
            }
            storey.ticketlistitem.Refresh(this.mGachaListTicket[num2], num2);
            storey.ticketlistitem.SetGachaButtonEvent(new UnityAction(storey, this.<>m__329));
        Label_017E:
            num2 += 1;
        Label_0182:
            if (num2 < this.mGachaListTicket.Count)
            {
                goto Label_00DF;
            }
        Label_0193:
            return;
        }

        [DebuggerHidden]
        private IEnumerator RefreshUnitImage()
        {
            <RefreshUnitImage>c__Iterator119 iterator;
            iterator = new <RefreshUnitImage>c__Iterator119();
            iterator.<>f__this = this;
            return iterator;
        }

        private void RefreshUnitInfo()
        {
            this.UpdateCurrentUnitInfo();
            this.RefreshJobs();
            if (this.mPickupUnits == null)
            {
                goto Label_004F;
            }
            if (this.mPickupUnits.Count <= 0)
            {
                goto Label_004F;
            }
            DataSource.Bind<UnitData>(this.UnitInfoPanel, this.mPickupUnits[this.mCurrentIndex]);
            GameParameter.UpdateAll(this.UnitInfoPanel);
        Label_004F:
            this.FadeUnitImage(0f, 0f, 0f);
            base.StartCoroutine(this.RefreshUnitImage());
            this.FadeUnitImage(0f, 1f, 1f);
            this.InitGachaUnitPreview(1);
            this.SetGachaPreviewCamera();
            return;
        }

        private void RefreshUnitInfoPanel()
        {
            if (this.mSelectTab == 1)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (this.mGachaListRare == null)
            {
                goto Label_0029;
            }
            if (this.mGachaListRare.Count > 0)
            {
                goto Label_002A;
            }
        Label_0029:
            return;
        Label_002A:
            if (string.IsNullOrEmpty(this.BGUnitImageID) != null)
            {
                goto Label_004B;
            }
            this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.BGUnitImageID);
        Label_004B:
            this.CreatePickupUnitsList(this.mGachaListRare[0].units.ToArray());
            if (this.mPickupUnits == null)
            {
                goto Label_0083;
            }
            if (this.mPickupUnits.Count > 0)
            {
                goto Label_0084;
            }
        Label_0083:
            return;
        Label_0084:
            this.mCurrentIndex = 0;
            this.mCurrentJobIndex = 0;
            this.RefreshUnitInfo();
            return;
        }

        [DebuggerHidden]
        private IEnumerator ReloadPickupArtifactPreview(bool reload)
        {
            <ReloadPickupArtifactPreview>c__Iterator117 iterator;
            iterator = new <ReloadPickupArtifactPreview>c__Iterator117();
            iterator.<>f__this = this;
            return iterator;
        }

        private void ReloadPickUpUnitView()
        {
            Type[] typeArray2;
            Type[] typeArray1;
            int num;
            GachaUnitPreview preview;
            GameObject obj2;
            GameObject obj3;
            if (this.CurrentUnit == null)
            {
                goto Label_001C;
            }
            if ((this.mPreviewParent == null) == null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            GameUtility.DestroyGameObjects<GachaUnitPreview>(this.mPreviewControllers);
            this.mPreviewControllers.Clear();
            this.mCurrentPreview = null;
            num = 0;
            goto Label_011C;
        Label_0041:
            preview = null;
            if (this.mCurrentUnit.Jobs[num] == null)
            {
                goto Label_010C;
            }
            if (this.mCurrentUnit.Jobs[num].Param == null)
            {
                goto Label_010C;
            }
            typeArray1 = new Type[] { typeof(GachaUnitPreview) };
            obj2 = new GameObject("Preview", typeArray1);
            preview = obj2.GetComponent<GachaUnitPreview>();
            preview.DefaultLayer = GameUtility.LayerHidden;
            preview.SetGachaUnitData(this.mCurrentUnit, this.mCurrentUnit.Jobs[num].JobID);
            preview.SetupUnit(this.mCurrentUnit.UnitParam.iname, this.mCurrentUnit.Jobs[num].JobID);
            obj2.get_transform().SetParent(this.mPreviewParent, 0);
            if (num != this.mCurrentUnit.JobIndex)
            {
                goto Label_010C;
            }
            this.mCurrentPreview = preview;
        Label_010C:
            this.mPreviewControllers.Add(preview);
            num += 1;
        Label_011C:
            if (num < ((int) this.mCurrentUnit.UnitParam.jobsets.Length))
            {
                goto Label_0041;
            }
            if ((this.mCurrentPreview == null) == null)
            {
                goto Label_01B4;
            }
            typeArray2 = new Type[] { typeof(GachaUnitPreview) };
            obj3 = new GameObject("Preview", typeArray2);
            this.mCurrentPreview = obj3.GetComponent<GachaUnitPreview>();
            this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
            this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
            obj3.get_transform().SetParent(this.mPreviewParent, 0);
            this.mPreviewControllers.Add(this.mCurrentPreview);
        Label_01B4:
            return;
        }

        private void ResetChangeEffect()
        {
            Animator animator;
            Animator animator2;
            animator = this.ChangeUnitEffectObj.GetComponent<Animator>();
            animator.ResetTrigger("onChangeJobF");
            animator.ResetTrigger("onChangeUnitF");
            animator.SetTrigger("onResetTrigger");
            animator2 = this.ChangeArtifactEffectObj.GetComponent<Animator>();
            animator2.ResetTrigger("onChangeJobF");
            animator2.ResetTrigger("onChangeUnitF");
            animator2.SetTrigger("onResetTrigger");
            return;
        }

        private bool SelectJob()
        {
            this.mCurrentJobIndex += 1;
            if (this.mCurrentJobIndex < this.mCurrentUnit.NumJobsAvailable)
            {
                goto Label_0034;
            }
            this.mChangeJob = 0;
            this.mCurrentJobIndex = 0;
            return 0;
        Label_0034:
            this.mChangeJob = 1;
            return 1;
        }

        private void SetActivePreview(int index)
        {
            GachaUnitPreview preview;
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

        private void SetActivePreviewArtifact(int index)
        {
            Transform transform;
            if (this.mPreviewArtifactControllers.Count > index)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            transform = this.mPreviewArtifactControllers[index];
            if ((transform == null) == null)
            {
                goto Label_002C;
            }
            return;
        Label_002C:
            GameUtility.SetLayer(this.mCurrentArtifactPreview, GameUtility.LayerHidden, 1);
            GameUtility.SetLayer(transform, GameUtility.LayerCH, 1);
            this.mCurrentArtifactPreview = transform;
            return;
        }

        public void SetGachaPreviewArtifactCamera()
        {
            if ((this.mPreviewCamera == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mPreviewCamera.set_targetTexture(this.mPreviewUnitRT);
            if ((this.PreviewArtifactImage != null) == null)
            {
                goto Label_0045;
            }
            this.PreviewArtifactImage.set_texture(this.mPreviewUnitRT);
        Label_0045:
            return;
        }

        public void SetGachaPreviewCamera()
        {
            if ((this.mPreviewCamera == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mPreviewCamera.set_targetTexture(this.mPreviewUnitRT);
            if ((this.PreviewImage != null) == null)
            {
                goto Label_0045;
            }
            this.PreviewImage.set_texture(this.mPreviewUnitRT);
        Label_0045:
            return;
        }

        private void SetTabValue(SRPG_Button tab, GachaTopParamNew param, GachaTabCategory tab_category, int tab_index, bool is_sibling)
        {
            SerializeValueBehaviour behaviour;
            <SetTabValue>c__AnonStorey342 storey;
            storey = new <SetTabValue>c__AnonStorey342();
            storey.tab = tab;
            storey.tab_category = tab_category;
            storey.tab_index = tab_index;
            storey.<>f__this = this;
            if ((storey.tab == null) != null)
            {
                goto Label_003F;
            }
            if (storey.tab_category != null)
            {
                goto Label_0040;
            }
        Label_003F:
            return;
        Label_0040:
            if (param == null)
            {
                goto Label_0057;
            }
            DataSource.Bind<GachaTopParamNew>(storey.tab.get_gameObject(), param);
        Label_0057:
            storey.tab.get_gameObject().SetActive(0);
            if (is_sibling == null)
            {
                goto Label_007F;
            }
            storey.tab.get_transform().SetAsLastSibling();
        Label_007F:
            storey.tab.get_onClick().AddListener(new UnityAction(storey, this.<>m__330));
            behaviour = storey.tab.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_00C9;
            }
            behaviour.list.SetField("category", storey.tab_category);
        Label_00C9:
            return;
        }

        private void SetTicketButtonList()
        {
            int num;
            GameObject obj2;
            <SetTicketButtonList>c__AnonStorey343 storey;
            if (this.mGachaListTicket == null)
            {
                goto Label_001C;
            }
            if (this.mGachaListTicket.Count > 0)
            {
                goto Label_0029;
            }
        Label_001C:
            this.TicketNotListView.SetActive(1);
            return;
        Label_0029:
            num = 0;
            goto Label_00F1;
        Label_0030:
            if (this.mTicketButtonLists.ContainsKey(this.mGachaListTicket[num].iname) != null)
            {
                goto Label_00ED;
            }
            storey = new <SetTicketButtonList>c__AnonStorey343();
            storey.<>f__this = this;
            obj2 = Object.Instantiate<GameObject>(this.TicketButtonTemplate);
            storey.ticketlistitem = obj2.GetComponent<GachaTicketListItem>();
            if ((storey.ticketlistitem != null) == null)
            {
                goto Label_00D0;
            }
            storey.ticketlistitem.Refresh(this.mGachaListTicket[num], num);
            storey.ticketlistitem.SetGachaButtonEvent(new UnityAction(storey, this.<>m__334));
            obj2.get_transform().SetParent(this.TicketListRoot, 0);
            obj2.SetActive(1);
        Label_00D0:
            this.mTicketButtonLists.Add(this.mGachaListTicket[num].iname, obj2);
        Label_00ED:
            num += 1;
        Label_00F1:
            if (num < this.mGachaListTicket.Count)
            {
                goto Label_0030;
            }
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

        public unsafe void SetupGachaList(GachaParam[] gparms)
        {
            int num;
            GachaTopParamNew new2;
            GachaTopParamNewGroups groups;
            GachaTopParamNew[] newArray;
            GachaTopParamNewGroups groups2;
            List<GachaTopParamNewGroups>.Enumerator enumerator;
            GachaTopParamNew[] newArray2;
            <SetupGachaList>c__AnonStorey341 storey;
            this.mGachaListRare.Clear();
            this.mGachaListNormal.Clear();
            this.mGachaListArtifact.Clear();
            this.mGachaListTicket.Clear();
            this.mGachaListSpecials.Clear();
            this.mGachaListAll.Clear();
            num = 0;
            goto Label_01DA;
        Label_0049:
            new2 = new GachaTopParamNew();
            new2.Deserialize(gparms[num]);
            if (new2.category.Contains("coin") == null)
            {
                goto Label_007E;
            }
            this.mGachaListRare.Add(new2);
            goto Label_01CA;
        Label_007E:
            if (new2.category.Contains("gold") == null)
            {
                goto Label_00A4;
            }
            this.mGachaListNormal.Add(new2);
            goto Label_01CA;
        Label_00A4:
            if (new2.group.Contains("bugu-") == null)
            {
                goto Label_00CA;
            }
            this.mGachaListArtifact.Add(new2);
            goto Label_01CA;
        Label_00CA:
            if (string.IsNullOrEmpty(new2.ticket_iname) != null)
            {
                goto Label_00EB;
            }
            this.mGachaListTicket.Add(new2);
            goto Label_01CA;
        Label_00EB:
            storey = new <SetupGachaList>c__AnonStorey341();
            if (string.IsNullOrEmpty(new2.asset_bg) == null)
            {
                goto Label_0117;
            }
            if (string.IsNullOrEmpty(new2.asset_title) == null)
            {
                goto Label_0117;
            }
            goto Label_01D6;
        Label_0117:
            storey.group = new2.group;
            if (this.mGachaListSpecials == null)
            {
                goto Label_0193;
            }
            if (string.IsNullOrEmpty(storey.group) != null)
            {
                goto Label_0193;
            }
            if (this.mGachaListSpecials.FindIndex(new Predicate<GachaTopParamNewGroups>(storey.<>m__32A)) == -1)
            {
                goto Label_0193;
            }
            groups = this.mGachaListSpecials[this.mGachaListSpecials.FindIndex(new Predicate<GachaTopParamNewGroups>(storey.<>m__32B))];
            groups.lists.Add(new2);
            goto Label_01CA;
        Label_0193:
            groups = new GachaTopParamNewGroups();
            groups.lists.Add(new2);
            groups.group = storey.group;
            groups.tab_image = new2.asset_title;
            this.mGachaListSpecials.Add(groups);
        Label_01CA:
            this.mGachaListAll.Add(new2);
        Label_01D6:
            num += 1;
        Label_01DA:
            if (num < ((int) gparms.Length))
            {
                goto Label_0049;
            }
            if (this.mGachaListRare == null)
            {
                goto Label_0223;
            }
            if (this.mGachaListRare.Count <= 1)
            {
                goto Label_0223;
            }
            newArray = this.SortGachaList(this.mGachaListRare);
            this.mGachaListRare.Clear();
            this.mGachaListRare.AddRange(newArray);
        Label_0223:
            if (this.mGachaListNormal == null)
            {
                goto Label_0267;
            }
            if (this.mGachaListNormal.Count <= 1)
            {
                goto Label_0267;
            }
            if (<>f__am$cache7D != null)
            {
                goto Label_025D;
            }
            <>f__am$cache7D = new Comparison<GachaTopParamNew>(GachaWindow.<SetupGachaList>m__32C);
        Label_025D:
            this.mGachaListNormal.Sort(<>f__am$cache7D);
        Label_0267:
            if (this.mGachaListSpecials == null)
            {
                goto Label_02EC;
            }
            if (this.mGachaListSpecials.Count <= 0)
            {
                goto Label_02EC;
            }
            enumerator = this.mGachaListSpecials.GetEnumerator();
        Label_0290:
            try
            {
                goto Label_02CE;
            Label_0295:
                groups2 = &enumerator.Current;
                if (groups2 == null)
                {
                    goto Label_02CE;
                }
                newArray2 = this.SortGachaList(groups2.lists);
                groups2.lists.Clear();
                groups2.lists.AddRange(newArray2);
            Label_02CE:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0295;
                }
                goto Label_02EC;
            }
            finally
            {
            Label_02DF:
                ((List<GachaTopParamNewGroups>.Enumerator) enumerator).Dispose();
            }
        Label_02EC:
            return;
        }

        private void SetupTabList()
        {
            int num;
            int num2;
            GachaTopParamNew new2;
            SRPG_Button button;
            int num3;
            Image image;
            string str;
            Sprite sprite;
            GachaTabListItem item;
            BadgeValidator validator;
            num = 0;
            goto Label_0042;
        Label_0007:
            if (this.mTabList[num].get_name().IndexOf("sp") == -1)
            {
                goto Label_003E;
            }
            Object.Destroy(this.mTabList[num].get_gameObject());
        Label_003E:
            num += 1;
        Label_0042:
            if (num < this.mTabList.Count)
            {
                goto Label_0007;
            }
            this.mTabList.Clear();
            this.mTabCategoryList.Clear();
            if ((this.mGachaListSpecials == null) || (this.mGachaListSpecials.Count <= 0))
            {
                goto Label_0248;
            }
            num2 = 0;
            goto Label_0237;
        Label_008C:
            new2 = this.mGachaListSpecials[num2].lists[0];
            button = Object.Instantiate<SRPG_Button>(this.TabTemplate);
            this.mTabList.Add(button);
            num3 = num2;
            button.get_transform().SetParent(this.TabTemplate.get_transform().get_parent(), 0);
            button.set_name("sp" + ((int) num2));
            this.SetTabValue(button, new2, 5, num3, 0);
            image = button.get_gameObject().GetComponent<Image>();
            str = this.mGachaListSpecials[num2].tab_image;
            if (((image != null) == null) || (string.IsNullOrEmpty(str) != null))
            {
                goto Label_0172;
            }
            if (this.mCacheTabImages.ContainsKey(str) == null)
            {
                goto Label_017A;
            }
            sprite = this.mCacheTabImages[str];
            if ((sprite != null) == null)
            {
                goto Label_017A;
            }
            image.set_sprite(sprite);
            goto Label_017A;
        Label_0172:
            image.set_sprite(null);
        Label_017A:
            item = button.GetComponent<GachaTabListItem>();
            if ((item != null) == null)
            {
                goto Label_0224;
            }
            item.EndAt = this.mGachaListSpecials[num2].lists[0].GetTimerAt();
            item.Disabled = this.mGachaListSpecials[num2].lists[0].disabled;
            item.GachaStartAt = this.mGachaListSpecials[num2].lists[0].startat;
            item.GachaEndtAt = this.mGachaListSpecials[num2].lists[0].endat;
            item.ListIndex = num3;
        Label_0224:
            button.set_interactable(new2.disabled == 0);
            num2 += 1;
        Label_0237:
            if (num2 < this.mGachaListSpecials.Count)
            {
                goto Label_008C;
            }
        Label_0248:
            this.mTabList.Add(this.RareTab);
            this.SetTabValue(this.RareTab, ((this.mGachaListRare == null) || (this.mGachaListRare.Count <= 0)) ? null : this.mGachaListRare[0], 1, -1, 1);
            if (this.IsFreePause(this.mGachaListRare.ToArray()) == null)
            {
                goto Label_02DB;
            }
            validator = this.RareTab.GetComponentInChildren<BadgeValidator>();
            if ((validator != null) == null)
            {
                goto Label_02DB;
            }
            validator.set_enabled(0);
            validator.get_gameObject().SetActive(0);
        Label_02DB:
            this.mTabList.Add(this.TicketTab);
            this.SetTabValue(this.TicketTab, ((this.mGachaListTicket == null) || (this.mGachaListTicket.Count <= 0)) ? null : this.mGachaListTicket[0], 3, -1, 1);
            this.mTabList.Add(this.NormalTab);
            this.SetTabValue(this.NormalTab, ((this.mGachaListNormal == null) || (this.mGachaListNormal.Count <= 0)) ? null : this.mGachaListNormal[0], 4, -1, 1);
            return;
        }

        public unsafe GachaTopParamNew[] SortGachaList(List<GachaTopParamNew> _list)
        {
            List<GachaTopParamNew> list;
            GachaTopParamNew new2;
            List<GachaTopParamNew>.Enumerator enumerator;
            list = new List<GachaTopParamNew>();
            this.m_CacheList_Coin.Clear();
            this.m_CacheList_Gold.Clear();
            this.m_CacheList_CoinPaid.Clear();
            enumerator = _list.GetEnumerator();
        Label_002E:
            try
            {
                goto Label_008D;
            Label_0033:
                new2 = &enumerator.Current;
                if (new2.CostType != 1)
                {
                    goto Label_0058;
                }
                this.m_CacheList_Coin.Add(new2);
                goto Label_008D;
            Label_0058:
                if (new2.CostType != 2)
                {
                    goto Label_0075;
                }
                this.m_CacheList_CoinPaid.Add(new2);
                goto Label_008D;
            Label_0075:
                if (new2.CostType != 3)
                {
                    goto Label_008D;
                }
                this.m_CacheList_Gold.Add(new2);
            Label_008D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0033;
                }
                goto Label_00AA;
            }
            finally
            {
            Label_009E:
                ((List<GachaTopParamNew>.Enumerator) enumerator).Dispose();
            }
        Label_00AA:
            if (<>f__am$cache7E != null)
            {
                goto Label_00C8;
            }
            <>f__am$cache7E = new Comparison<GachaTopParamNew>(GachaWindow.<SortGachaList>m__32D);
        Label_00C8:
            this.m_CacheList_Coin.Sort(<>f__am$cache7E);
            if (<>f__am$cache7F != null)
            {
                goto Label_00F0;
            }
            <>f__am$cache7F = new Comparison<GachaTopParamNew>(GachaWindow.<SortGachaList>m__32E);
        Label_00F0:
            this.m_CacheList_CoinPaid.Sort(<>f__am$cache7F);
            if (<>f__am$cache80 != null)
            {
                goto Label_0118;
            }
            <>f__am$cache80 = new Comparison<GachaTopParamNew>(GachaWindow.<SortGachaList>m__32F);
        Label_0118:
            this.m_CacheList_Gold.Sort(<>f__am$cache80);
            list.AddRange(this.m_CacheList_Gold);
            list.AddRange(this.m_CacheList_Coin);
            list.AddRange(this.m_CacheList_CoinPaid);
            return list.ToArray();
        }

        private void Start()
        {
            this.SetupGachaList(MonoSingleton<GameManager>.Instance.Gachas);
            this.ClearTabSprites();
            this.ClearBGSprites();
            base.StartCoroutine(this.LoadGachaTabSprites());
            base.StartCoroutine(this.LoadGachaBGTextures());
            this.RefreshSummonCoin();
            this.mState = new StateMachine<GachaWindow>(this);
            this.mState.GotoState<State_Init>();
            return;
        }

        private bool TabChange(SRPG_Button button, GachaTabCategory category, int index)
        {
            SRPG_Button[] buttonArray;
            int num;
            int num2;
            bool flag;
            Transform transform;
            GachaTabListItem item;
            bool flag2;
            if (button.IsInteractable() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            buttonArray = this.mTabList.ToArray();
            num = Array.IndexOf<SRPG_Button>(buttonArray, button);
            if (category != 5)
            {
                goto Label_003B;
            }
            if (this.mCurrentTabSPIndex != index)
            {
                goto Label_0049;
            }
            return 0;
            goto Label_0049;
        Label_003B:
            if (this.mSelectTab != category)
            {
                goto Label_0049;
            }
            return 0;
        Label_0049:
            num2 = 0;
            goto Label_0113;
        Label_0050:
            flag = num2 == num;
            if ((buttonArray[num2] != null) == null)
            {
                goto Label_0091;
            }
            transform = buttonArray[num2].get_transform().Find("cursor");
            if ((transform != null) == null)
            {
                goto Label_0091;
            }
            transform.get_gameObject().SetActive(flag);
        Label_0091:
            if (flag == null)
            {
                goto Label_00BD;
            }
            buttonArray[num2].get_transform().set_localScale(new Vector3(1f, 1f, 1f));
            goto Label_010F;
        Label_00BD:
            buttonArray[num2].get_transform().set_localScale(new Vector3(0.9f, 0.9f, 0.9f));
            item = buttonArray[num2].GetComponent<GachaTabListItem>();
            if ((item != null) == null)
            {
                goto Label_0106;
            }
            if (item.Disabled == null)
            {
                goto Label_0106;
            }
            goto Label_010F;
        Label_0106:
            buttonArray[num2].set_enabled(0);
        Label_010F:
            num2 += 1;
        Label_0113:
            if (num2 < ((int) buttonArray.Length))
            {
                goto Label_0050;
            }
            this.mSelectTab = category;
            this.mCurrentTabSPIndex = -1;
            if (category != 5)
            {
                goto Label_0138;
            }
            this.mCurrentTabSPIndex = index;
        Label_0138:
            flag2 = 1;
            if (category != 3)
            {
                goto Label_0145;
            }
            flag2 = 0;
        Label_0145:
            this.ResetChangeEffect();
            this.DefaultPanel.SetActive(flag2);
            this.ButtonPanel.SetActive(flag2);
            this.TicketPanel.SetActive(flag2 == 0);
            if (this.DefaultPanel.get_activeInHierarchy() == null)
            {
                goto Label_018B;
            }
            this.RefreshDefaultPanel();
        Label_018B:
            if (this.TicketPanel.get_activeInHierarchy() == null)
            {
                goto Label_01A1;
            }
            this.SetTicketButtonList();
        Label_01A1:
            this.IsTabChanging = 1;
            this.mCurrentTabIndex = num;
            this.RefreshGachaDetailSelectID(this.mSelectTab);
            return 1;
        }

        private void Update()
        {
            float num;
            if (this.mState == null)
            {
                goto Label_0016;
            }
            this.mState.Update();
        Label_0016:
            if (this.mUpdatePreviewVisibility == null)
            {
                goto Label_00FD;
            }
            if (this.mDesiredPreviewVisibility == null)
            {
                goto Label_00FD;
            }
            if (this.mSelectTab == 1)
            {
                goto Label_0044;
            }
            if (this.mSelectTab != 2)
            {
                goto Label_005C;
            }
        Label_0044:
            if (this.IsTabChanging == null)
            {
                goto Label_005C;
            }
            this.PlayChangeEffect();
            this.IsTabChanging = 0;
        Label_005C:
            if (this.mSelectTab != 1)
            {
                goto Label_00B7;
            }
            if ((this.mCurrentPreview != null) == null)
            {
                goto Label_00FD;
            }
            if (this.mCurrentPreview.IsLoading != null)
            {
                goto Label_00FD;
            }
            GameUtility.SetLayer(this.mCurrentPreview, GameUtility.LayerCH, 1);
            GameUtility.SetLayer(this.mCurrentArtifactPreview, GameUtility.LayerHidden, 1);
            this.mUpdatePreviewVisibility = 0;
            goto Label_00FD;
        Label_00B7:
            if (this.mSelectTab != 2)
            {
                goto Label_00FD;
            }
            if ((this.mCurrentArtifactPreview != null) == null)
            {
                goto Label_00FD;
            }
            GameUtility.SetLayer(this.mCurrentPreview, GameUtility.LayerHidden, 1);
            GameUtility.SetLayer(this.mCurrentArtifactPreview, GameUtility.LayerCH, 1);
            this.mUpdatePreviewVisibility = 0;
        Label_00FD:
            if (this.mBGUnitImgFadeTime >= this.mBGUnitImgFadeTimeMax)
            {
                goto Label_017D;
            }
            if ((this.mBGUnitImage != null) == null)
            {
                goto Label_017D;
            }
            this.mBGUnitImgFadeTime += Time.get_unscaledDeltaTime();
            num = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
            this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num));
            if (num < 1f)
            {
                goto Label_017D;
            }
            this.mBGUnitImgFadeTime = 0f;
            this.mBGUnitImgFadeTimeMax = 0f;
        Label_017D:
            this.UpdateBG();
            return;
        }

        private void UpdateBG()
        {
            Transform transform;
            Transform transform2;
            RawImage image;
            RawImage image2;
            CanvasGroup group;
            CanvasGroup group2;
            float num;
            if (this.mExistSwapBG != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mWaitSwapBGTime -= Time.get_deltaTime();
            if (this.mWaitSwapBGTime >= 0f)
            {
                goto Label_0259;
            }
            transform = (((this.mBGObjects != null) || (this.mBGObjects.Count < 2)) || ((this.mBGObjects[0] != null) == null)) ? this.BGRoot.FindChild("bg00") : this.mBGObjects[0];
            transform2 = (((this.mBGObjects != null) || (this.mBGObjects.Count < 2)) || ((this.mBGObjects[1] != null) == null)) ? this.BGRoot.FindChild("bg01") : this.mBGObjects[1];
            if (((transform == null) == null) && ((transform2 == null) == null))
            {
                goto Label_00FB;
            }
            DebugUtility.LogError("=== GachaWindow->UpdateBG:bg00 or bg01 is null ===");
            return;
        Label_00FB:
            image = transform.GetComponent<RawImage>();
            image2 = transform2.GetComponent<RawImage>();
            if (((image != null) != null) && ((image2 != null) != null))
            {
                goto Label_012C;
            }
            DebugUtility.LogError("=== GachaWindow->UpdateBG:bg00_image & bg01_image is null ===");
            return;
        Label_012C:
            group = image.get_gameObject().GetComponent<CanvasGroup>();
            group2 = image2.get_gameObject().GetComponent<CanvasGroup>();
            if (((group != null) != null) && ((group2 != null) != null))
            {
                goto Label_016B;
            }
            DebugUtility.LogError("=== GachaWindow->UpdateBG:canvas00 & canvas01 is null ===");
            return;
        Label_016B:
            num = Time.get_deltaTime();
            group.set_alpha(group.get_alpha() + ((this.mEnableBGIndex != null) ? num : -num));
            group2.set_alpha(group2.get_alpha() + ((this.mEnableBGIndex != null) ? -num : num));
            group.set_alpha(Mathf.Clamp(group.get_alpha() + ((this.mEnableBGIndex != null) ? num : -num), 0f, 1f));
            group2.set_alpha(Mathf.Clamp(group2.get_alpha() + ((this.mEnableBGIndex != null) ? -num : num), 0f, 1f));
            if (group.get_alpha() <= 0f)
            {
                goto Label_0240;
            }
            if (group.get_alpha() < 1f)
            {
                goto Label_0259;
            }
        Label_0240:
            this.mEnableBGIndex ^= 1;
            this.mWaitSwapBGTime = WAIT_SWAP_BG;
        Label_0259:
            return;
        }

        private void UpdateCurrentArtifactInfo()
        {
            if (this.mPickupArtifacts.Count <= 0)
            {
                goto Label_0028;
            }
            this.mCurrentArtifact = this.mPickupArtifacts[this.mCurrentPickupArtIndex];
        Label_0028:
            return;
        }

        private void UpdateCurrentUnitInfo()
        {
            if (this.mPickupUnits.Count <= 0)
            {
                goto Label_003E;
            }
            this.mCurrentUnit = this.mPickupUnits[this.mCurrentIndex];
            this.mCurrentUnit.SetJobIndex(this.mCurrentJobIndex);
            goto Label_0048;
        Label_003E:
            Debug.LogError("mPickupUnits.unitsがNullもしくはCount=0です.");
        Label_0048:
            return;
        }

        public ArtifactData CurrentArtifact
        {
            get
            {
                return this.mCurrentArtifact;
            }
        }

        public bool ChangeUnit
        {
            get
            {
                return this.mChangeUnit;
            }
            set
            {
                this.mChangeUnit = value;
                return;
            }
        }

        public bool ChangeJob
        {
            get
            {
                return this.mChangeJob;
            }
            set
            {
                this.mChangeJob = value;
                return;
            }
        }

        public bool Initialized
        {
            get
            {
                return this.mInitialized;
            }
        }

        public UnitData CurrentUnit
        {
            get
            {
                return this.mCurrentUnit;
            }
        }

        public bool Clicked
        {
            get
            {
                return this.mClicked;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateConfirm>c__Iterator118 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <obj>__1;
            internal GachaConfirmWindow <window>__2;
            internal GachaRequestParam _param;
            internal ItemParam <item>__3;
            internal int $PC;
            internal object $current;
            internal GachaRequestParam <$>_param;
            internal GachaWindow <>f__this;

            public <CreateConfirm>c__Iterator118()
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
                        goto Label_007D;

                    case 2:
                        goto Label_0184;
                }
                goto Label_018B;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.CONFIRM_WINDOW_PATH) != null)
                {
                    goto Label_0171;
                }
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.<>f__this.CONFIRM_WINDOW_PATH);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_007D;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_018D;
            Label_007D:
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_0171;
                }
                this.<obj>__1 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                if ((this.<obj>__1 != null) == null)
                {
                    goto Label_0171;
                }
                this.<window>__2 = this.<obj>__1.GetComponent<GachaConfirmWindow>();
                if ((this.<window>__2 != null) == null)
                {
                    goto Label_0171;
                }
                this.<window>__2.Set(this._param);
                this.<window>__2.OnDecide = new GachaConfirmWindow.DecideEvent(this.<>f__this.OnDecide);
                this.<window>__2.OnCancel = new GachaConfirmWindow.CancelEvent(this.<>f__this.OnCancel);
                if (this._param.IsTicketGacha == null)
                {
                    goto Label_0171;
                }
                this.<item>__3 = MonoSingleton<GameManager>.Instance.GetItemParam(this._param.Ticket);
                if (this.<item>__3 == null)
                {
                    goto Label_0171;
                }
                DataSource.Bind<ItemParam>(this.<obj>__1, this.<item>__3);
            Label_0171:
                this.$current = null;
                this.$PC = 2;
                goto Label_018D;
            Label_0184:
                this.$PC = -1;
            Label_018B:
                return 0;
            Label_018D:
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
        private sealed class <CreateGachaRequest>c__AnonStorey347
        {
            internal string _iname;

            public <CreateGachaRequest>c__AnonStorey347()
            {
                base..ctor();
                return;
            }

            internal bool <>m__339(GachaTopParamNew p)
            {
                return (p.iname == this._iname);
            }
        }

        [CompilerGenerated]
        private sealed class <LoadGachaBGTextures>c__Iterator115 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <texture_resources>__0;
            internal GachaBGTextures <textures>__1;
            internal Texture2D[] <items>__2;
            internal int <i>__3;
            internal int $PC;
            internal object $current;
            internal GachaWindow <>f__this;

            public <LoadGachaBGTextures>c__Iterator115()
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
                        goto Label_0061;

                    case 2:
                        goto Label_014F;
                }
                goto Label_0156;
            Label_0025:
                if (string.IsNullOrEmpty(GachaWindow.BG_TEXTURE_PATH) != null)
                {
                    goto Label_013C;
                }
                this.<texture_resources>__0 = AssetManager.LoadAsync<GachaBGTextures>(GachaWindow.BG_TEXTURE_PATH);
                this.$current = this.<texture_resources>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0158;
            Label_0061:
                this.<textures>__1 = this.<texture_resources>__0.asset as GachaBGTextures;
                if ((this.<textures>__1 != null) == null)
                {
                    goto Label_013C;
                }
                if (this.<textures>__1.Textures == null)
                {
                    goto Label_013C;
                }
                if (((int) this.<textures>__1.Textures.Length) <= 0)
                {
                    goto Label_013C;
                }
                this.<items>__2 = this.<textures>__1.Textures;
                this.<i>__3 = 0;
                goto Label_011D;
            Label_00C8:
                if ((this.<items>__2[this.<i>__3] != null) == null)
                {
                    goto Label_010F;
                }
                this.<>f__this.mCacheBGImages.Add(this.<items>__2[this.<i>__3].get_name(), this.<items>__2[this.<i>__3]);
            Label_010F:
                this.<i>__3 += 1;
            Label_011D:
                if (this.<i>__3 < ((int) this.<items>__2.Length))
                {
                    goto Label_00C8;
                }
                this.<>f__this.mLoadBackGroundTexture = 1;
            Label_013C:
                this.$current = null;
                this.$PC = 2;
                goto Label_0158;
            Label_014F:
                this.$PC = -1;
            Label_0156:
                return 0;
            Label_0158:
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
        private sealed class <LoadGachaTabSprites>c__Iterator116 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <sprite_resources>__0;
            internal GachaTabSprites <sprites>__1;
            internal Sprite[] <items>__2;
            internal int <i>__3;
            internal int $PC;
            internal object $current;
            internal GachaWindow <>f__this;

            public <LoadGachaTabSprites>c__Iterator116()
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
                        goto Label_0061;

                    case 2:
                        goto Label_014F;
                }
                goto Label_0156;
            Label_0025:
                if (string.IsNullOrEmpty(GachaWindow.TAB_SPRITES_PATH) != null)
                {
                    goto Label_0130;
                }
                this.<sprite_resources>__0 = AssetManager.LoadAsync<GachaTabSprites>(GachaWindow.TAB_SPRITES_PATH);
                this.$current = this.<sprite_resources>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_0158;
            Label_0061:
                this.<sprites>__1 = this.<sprite_resources>__0.asset as GachaTabSprites;
                if ((this.<sprites>__1 != null) == null)
                {
                    goto Label_0130;
                }
                if (this.<sprites>__1.Sprites == null)
                {
                    goto Label_0130;
                }
                if (((int) this.<sprites>__1.Sprites.Length) <= 0)
                {
                    goto Label_0130;
                }
                this.<items>__2 = this.<sprites>__1.Sprites;
                this.<i>__3 = 0;
                goto Label_011D;
            Label_00C8:
                if ((this.<items>__2[this.<i>__3] != null) == null)
                {
                    goto Label_010F;
                }
                this.<>f__this.mCacheTabImages.Add(this.<items>__2[this.<i>__3].get_name(), this.<items>__2[this.<i>__3]);
            Label_010F:
                this.<i>__3 += 1;
            Label_011D:
                if (this.<i>__3 < ((int) this.<items>__2.Length))
                {
                    goto Label_00C8;
                }
            Label_0130:
                this.<>f__this.mLoadGachaTabSprites = 1;
                this.$current = null;
                this.$PC = 2;
                goto Label_0158;
            Label_014F:
                this.$PC = -1;
            Label_0156:
                return 0;
            Label_0158:
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
        private sealed class <OnExecGacha2>c__AnonStorey346
        {
            internal string _iname;

            public <OnExecGacha2>c__AnonStorey346()
            {
                base..ctor();
                return;
            }

            internal bool <>m__338(GachaTopParamNew p)
            {
                return (p.iname == this._iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshButtonPanel>c__AnonStorey344
        {
            internal string iname;
            internal int cost;
            internal string category;
            internal string ticket;
            internal int exec_num;
            internal string confirm;
            internal GachaCostType cost_type;
            internal bool isUseOnemore;
            internal bool is_nouse_free;
            internal GachaTopParamNew param;
            internal GachaWindow <>f__this;

            public <RefreshButtonPanel>c__AnonStorey344()
            {
                base..ctor();
                return;
            }

            internal void <>m__335()
            {
                this.<>f__this.OnExecGacha2(this.iname, this.cost, this.category, this.ticket, this.exec_num, this.confirm, this.cost_type, this.isUseOnemore, this.is_nouse_free, this.param.redraw_rest, this.param.redraw_num);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshGachaBGImage>c__AnonStorey345
        {
            internal Transform bg00;
            internal Transform bg01;

            public <RefreshGachaBGImage>c__AnonStorey345()
            {
                base..ctor();
                return;
            }

            internal bool <>m__336(Transform s)
            {
                return (s.get_gameObject() == this.bg00.get_gameObject());
            }

            internal bool <>m__337(Transform s)
            {
                return (s.get_gameObject() == this.bg01.get_gameObject());
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshTicketButtonList>c__AnonStorey33F
        {
            internal string iname;
            internal GachaWindow <>f__this;

            public <RefreshTicketButtonList>c__AnonStorey33F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__328(GachaTopParamNew x)
            {
                return (x.iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshTicketButtonList>c__AnonStorey340
        {
            internal GachaTicketListItem ticketlistitem;
            internal GachaWindow <>f__this;

            public <RefreshTicketButtonList>c__AnonStorey340()
            {
                base..ctor();
                return;
            }

            internal void <>m__329()
            {
                this.<>f__this.OnSelectTicket(this.ticketlistitem.SelectIndex);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUnitImage>c__Iterator119 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal GachaWindow <>f__this;

            public <RefreshUnitImage>c__Iterator119()
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
                        goto Label_008D;

                    case 2:
                        goto Label_00C0;
                }
                goto Label_00C7;
            Label_0025:
                if ((this.<>f__this.mBGUnitImage != null) == null)
                {
                    goto Label_00AD;
                }
                this.<req>__0 = AssetManager.LoadAsync<Texture2D>(AssetPath.UnitImage2(this.<>f__this.mCurrentUnit.UnitParam, this.<>f__this.mCurrentUnit.CurrentJob.JobID));
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00C9;
            Label_008D:
                this.<>f__this.mBGUnitImage.set_texture(this.<req>__0.asset as Texture2D);
            Label_00AD:
                this.$current = null;
                this.$PC = 2;
                goto Label_00C9;
            Label_00C0:
                this.$PC = -1;
            Label_00C7:
                return 0;
            Label_00C9:
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
        private sealed class <ReloadPickupArtifactPreview>c__Iterator117 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal LoadRequest <artifactReq>__1;
            internal ArtifactData <current_artifact>__2;
            internal EquipmentSet <artifact>__3;
            internal GameObject <primary>__4;
            internal Transform <preview>__5;
            internal int $PC;
            internal object $current;
            internal GachaWindow <>f__this;

            public <ReloadPickupArtifactPreview>c__Iterator117()
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
                        goto Label_00FF;
                }
                goto Label_024B;
            Label_0021:
                if (this.<>f__this.CurrentArtifact == null)
                {
                    goto Label_024B;
                }
                if ((this.<>f__this.mPreviewArtifact == null) == null)
                {
                    goto Label_004C;
                }
                goto Label_024B;
            Label_004C:
                GameUtility.DestroyGameObjects<Transform>(this.<>f__this.mPreviewArtifactControllers);
                this.<>f__this.mPreviewArtifactControllers.Clear();
                GameUtility.DestroyGameObject(this.<>f__this.mCurrentArtifactPreview);
                this.<>f__this.mCurrentArtifactPreview = null;
                this.<i>__0 = 0;
                goto Label_0229;
            Label_0094:
                this.<artifactReq>__1 = null;
                this.<current_artifact>__2 = this.<>f__this.mPickupArtifacts[this.<i>__0];
                this.<artifactReq>__1 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.Artifacts(this.<current_artifact>__2.ArtifactParam));
                if (this.<artifactReq>__1.isDone != null)
                {
                    goto Label_00FF;
                }
                this.$current = this.<artifactReq>__1.StartCoroutine();
                this.$PC = 1;
                goto Label_024D;
            Label_00FF:
                this.<artifactReq>__1 = ((this.<artifactReq>__1.asset != null) == null) ? null : this.<artifactReq>__1;
                if (this.<artifactReq>__1 == null)
                {
                    goto Label_021B;
                }
                this.<artifact>__3 = (this.<artifactReq>__1 == null) ? null : (this.<artifactReq>__1.asset as EquipmentSet);
                this.<primary>__4 = null;
                this.<primary>__4 = this.<artifact>__3.PrimaryHand;
                if ((this.<primary>__4 != null) == null)
                {
                    goto Label_021B;
                }
                if (this.<>f__this.mPreviewArtifact == null)
                {
                    goto Label_021B;
                }
                this.<preview>__5 = Object.Instantiate<Transform>(this.<primary>__4.get_transform());
                this.<preview>__5.get_transform().SetParent(this.<>f__this.mPreviewArtifact, 0);
                this.<preview>__5.get_gameObject().set_layer(GameUtility.LayerHidden);
                this.<>f__this.mPreviewArtifactControllers.Add(this.<preview>__5);
                if ((this.<>f__this.mCurrentArtifactPreview == null) == null)
                {
                    goto Label_021B;
                }
                this.<>f__this.mCurrentArtifactPreview = this.<preview>__5;
            Label_021B:
                this.<i>__0 += 1;
            Label_0229:
                if (this.<i>__0 < this.<>f__this.mPickupArtifacts.Count)
                {
                    goto Label_0094;
                }
                this.$PC = -1;
            Label_024B:
                return 0;
            Label_024D:
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
        private sealed class <SetTabValue>c__AnonStorey342
        {
            internal SRPG_Button tab;
            internal GachaWindow.GachaTabCategory tab_category;
            internal int tab_index;
            internal GachaWindow <>f__this;

            public <SetTabValue>c__AnonStorey342()
            {
                base..ctor();
                return;
            }

            internal void <>m__330()
            {
                this.<>f__this.OnTabChange(this.tab, this.tab_category, this.tab_index);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <SetTicketButtonList>c__AnonStorey343
        {
            internal GachaTicketListItem ticketlistitem;
            internal GachaWindow <>f__this;

            public <SetTicketButtonList>c__AnonStorey343()
            {
                base..ctor();
                return;
            }

            internal void <>m__334()
            {
                this.<>f__this.OnSelectTicket(this.ticketlistitem.SelectIndex);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <SetupGachaList>c__AnonStorey341
        {
            internal string group;

            public <SetupGachaList>c__AnonStorey341()
            {
                base..ctor();
                return;
            }

            internal bool <>m__32A(GachaWindow.GachaTopParamNewGroups s)
            {
                return (s.group == this.group);
            }

            internal bool <>m__32B(GachaWindow.GachaTopParamNewGroups s)
            {
                return (s.group == this.group);
            }
        }

        public enum GachaTabCategory : byte
        {
            NONE = 0,
            RARE = 1,
            ARTIFACT = 2,
            TICKET = 3,
            NORMAL = 4,
            SPECIAL = 5
        }

        public class GachaTopParamNewGroups
        {
            public List<GachaTopParamNew> lists;
            public string group;
            public string tab_image;

            public GachaTopParamNewGroups()
            {
                this.lists = new List<GachaTopParamNew>();
                base..ctor();
                return;
            }
        }

        private class State_CheckInitState : State<GachaWindow>
        {
            public State_CheckInitState()
            {
                base..ctor();
                return;
            }

            public override void Update(GachaWindow self)
            {
                bool flag;
                if (self.IsRefreshingGachaBG != null)
                {
                    goto Label_0077;
                }
                self.RefreshTabActive(1);
                self.RefreshTabEnable(1);
                if (self.IsGachaPending() == null)
                {
                    goto Label_0034;
                }
                self.mState.GotoState<GachaWindow.State_ToGachaResult>();
                goto Label_0077;
            Label_0034:
                if (self.mSelectTab == 1)
                {
                    goto Label_004C;
                }
                if (self.mSelectTab != 2)
                {
                    goto Label_006C;
                }
            Label_004C:
                flag = 0;
                self.ChangeJob = flag;
                self.ChangeUnit = flag;
                self.mState.GotoState<GachaWindow.State_WaitActionAnimation>();
                goto Label_0077;
            Label_006C:
                self.mState.GotoState<GachaWindow.State_PauseState>();
            Label_0077:
                return;
            }
        }

        private class State_CheckPreviewState : State<GachaWindow>
        {
            public State_CheckPreviewState()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaWindow self)
            {
                if (self.mSelectTab != 2)
                {
                    goto Label_0017;
                }
                self.ChangePreviewArtifact();
                goto Label_0049;
            Label_0017:
                if (self.mSelectTab != 1)
                {
                    goto Label_0049;
                }
                self.ChangeJob = 0;
                self.ChangeUnit = 0;
                if (self.SelectJob() != null)
                {
                    goto Label_0049;
                }
                self.ChangePreviewUnit();
                self.ChangeUnit = 1;
            Label_0049:
                self.mState.GotoState<GachaWindow.State_RefreshPreview>();
                return;
            }
        }

        private class State_Init : State<GachaWindow>
        {
            public State_Init()
            {
                base..ctor();
                return;
            }

            public override void Update(GachaWindow self)
            {
                SRPG_Button button;
                GachaWindow.GachaTabCategory category;
                int num;
                if (self.mLoadGachaTabSprites == null)
                {
                    goto Label_0068;
                }
                if (self.mLoadBackGroundTexture == null)
                {
                    goto Label_0068;
                }
                self.SetupTabList();
                self.RefreshTabList();
                button = self.mTabList[0];
                category = self.GetTabCategory(button);
                num = 0;
                self.TabChange(button, category, num);
                self.RefreshGachaDetailSelectID(self.mSelectTab);
                self.mInitialized = 1;
                self.mClicked = 0;
                self.mState.GotoState<GachaWindow.State_CheckInitState>();
            Label_0068:
                return;
            }
        }

        private class State_PauseState : State<GachaWindow>
        {
            public State_PauseState()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaWindow self)
            {
            }
        }

        private class State_RefreshPreview : State<GachaWindow>
        {
            private WaitForSeconds wait;

            public State_RefreshPreview()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaWindow self)
            {
                this.wait = new WaitForSeconds((self.ChangeUnit == null) ? self.ChangeJobWaitEffectTime : self.ChangeUnitWaitEffectTime);
                self.StartCoroutine(this.RebuildPreviewController(self));
                return;
            }

            private void RebuildArtifactPreview()
            {
                base.self.RefreshArtifactInfo();
                base.self.SetActivePreviewArtifact(base.self.mCurrentPickupArtIndex);
                base.self.RefreshPreviews();
                return;
            }

            [DebuggerHidden]
            private IEnumerator RebuildPreviewController(GachaWindow self)
            {
                <RebuildPreviewController>c__Iterator11A iteratora;
                iteratora = new <RebuildPreviewController>c__Iterator11A();
                iteratora.self = self;
                iteratora.<$>self = self;
                iteratora.<>f__this = this;
                return iteratora;
            }

            private void RebuildUnitPreview()
            {
                base.self.UpdateCurrentUnitInfo();
                if (base.self.ChangeUnit == null)
                {
                    goto Label_0036;
                }
                base.self.RefreshUnitInfo();
                base.self.RefreshPreviews();
                goto Label_0057;
            Label_0036:
                base.self.RefreshJobs();
                base.self.SetActivePreview(base.self.mCurrentJobIndex);
            Label_0057:
                if (base.self.Initialized == null)
                {
                    goto Label_0077;
                }
                GameParameter.UpdateAll(base.self.get_gameObject());
            Label_0077:
                return;
            }

            [CompilerGenerated]
            private sealed class <RebuildPreviewController>c__Iterator11A : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal GachaWindow self;
                internal int $PC;
                internal object $current;
                internal GachaWindow <$>self;
                internal GachaWindow.State_RefreshPreview <>f__this;

                public <RebuildPreviewController>c__Iterator11A()
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
                            goto Label_00D1;
                    }
                    goto Label_00F3;
                Label_0025:
                    if (this.self.mSelectTab != 1)
                    {
                        goto Label_0083;
                    }
                    if (this.self.CurrentUnit != null)
                    {
                        goto Label_004B;
                    }
                    goto Label_00F3;
                Label_004B:
                    this.self.PlayChangeEffect();
                    this.$current = this.<>f__this.wait;
                    this.$PC = 1;
                    goto Label_00F5;
                Label_0073:
                    this.<>f__this.RebuildUnitPreview();
                    goto Label_00DC;
                Label_0083:
                    if (this.self.mSelectTab != 2)
                    {
                        goto Label_00DC;
                    }
                    if (this.self.CurrentArtifact != null)
                    {
                        goto Label_00A9;
                    }
                    goto Label_00F3;
                Label_00A9:
                    this.self.PlayChangeEffect();
                    this.$current = this.<>f__this.wait;
                    this.$PC = 2;
                    goto Label_00F5;
                Label_00D1:
                    this.<>f__this.RebuildArtifactPreview();
                Label_00DC:
                    this.self.mState.GotoState<GachaWindow.State_WaitActionAnimation>();
                    this.$PC = -1;
                Label_00F3:
                    return 0;
                Label_00F5:
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
        }

        private class State_ToGachaResult : State<GachaWindow>
        {
            public State_ToGachaResult()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaWindow self)
            {
                FlowNode_GameObject.ActivateOutputLinks(self, 200);
                self.mState.GotoState<GachaWindow.State_PauseState>();
                return;
            }
        }

        private class State_WaitActionAnimation : State<GachaWindow>
        {
            private float mTimer;

            public State_WaitActionAnimation()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaWindow self)
            {
                if (self.mSelectTab != 2)
                {
                    goto Label_0018;
                }
                self.mState.GotoState<GachaWindow.State_WaitPreviewUnit>();
                return;
            Label_0018:
                this.mTimer = self.WaitTimeNextAction;
                return;
            }

            public override void Update(GachaWindow self)
            {
                this.mTimer -= Time.get_deltaTime();
                if (this.mTimer > 0f)
                {
                    goto Label_0059;
                }
                if ((self.mCurrentPreview != null) == null)
                {
                    goto Label_004F;
                }
                self.mCurrentPreview.PlayAction = 1;
                self.mState.GotoState<GachaWindow.State_WaitPreviewUnit>();
                goto Label_0059;
            Label_004F:
                Debug.LogError("mCurrentPreviewがNullです");
            Label_0059:
                return;
            }
        }

        private class State_WaitPreviewUnit : State<GachaWindow>
        {
            private float mTimer;

            public State_WaitPreviewUnit()
            {
                base..ctor();
                return;
            }

            public override void Begin(GachaWindow self)
            {
                this.mTimer = self.mWaitSecondsChangeUnitJob;
                return;
            }

            public override void Update(GachaWindow self)
            {
                this.mTimer -= Time.get_deltaTime();
                if (this.mTimer > 0f)
                {
                    goto Label_002D;
                }
                self.mState.GotoState<GachaWindow.State_CheckPreviewState>();
            Label_002D:
                return;
            }
        }
    }
}

