// Decompiled with JetBrains decompiler
// Type: SRPG.GachaWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "GachaConfirm", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(999, "WebView起動", FlowNode.PinTypes.Output, 999)]
  [FlowNode.Pin(200, "召喚結果へ強制遷移", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(113, "リフレッシュ完了", FlowNode.PinTypes.Output, 113)]
  [FlowNode.Pin(112, "チケット召喚をキャンセル", FlowNode.PinTypes.Input, 112)]
  [FlowNode.Pin(111, "チケット召喚を行う", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(110, "チケット召喚(単発)を選択", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(102, "Closed", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(101, "Opened", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "ガチャ実行", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(3, "DecisionRedraw", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(2, "ExecFailed", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class GachaWindow : MonoBehaviour, IFlowInterface
  {
    private static readonly int IN_GACHA_CONFIRM = 10;
    private static readonly string GACHA_URL_PREFIX = "notice/detail/gacha/";
    private static readonly float WAIT_SWAP_BG = 5f;
    private static readonly string DEFAULT_BGIMAGE_PATH = "GachaImages_Default";
    private static readonly string BG_TEXTURE_PATH = "Gachas/BGTables";
    private static readonly int ConversionConstant = 65248;
    private static readonly string TAB_SPRITES_PATH = "Gachas/GachaTabSprites";
    private static readonly string TEXT_CONFIRM_GACHA_COIN = "sys.CONFIRM_GACHA_COIN";
    private static readonly string TEXT_COST_GACHA_COIN = "sys.GACHA_COST_COIN";
    private static readonly string TEXT_COST_GACHA_PAIDCOIN = "sys.GACHA_COST_PAIDCOIN";
    private static readonly string TEXT_CONFIRM_GACHA_COST_ZERO = "sys.CONFIRM_GACHA_COST_ZERO";
    private const int PIN_OT_TO_RESULT = 200;
    private const int PIN_IN_DECISION_REDRAW_GACHA = 3;
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
    private List<GachaWindow.GachaTopParamNewGroups> mGachaListSpecials;
    private List<GachaTopParamNew> mGachaListAll;
    public SRPG_Button RareTab;
    public SRPG_Button NormalTab;
    public SRPG_Button TicketTab;
    public SRPG_Button ArtifactTab;
    public SRPG_Button TabTemplate;
    private List<SRPG_Button> mTabList;
    private GachaWindow.GachaTabCategory mSelectTab;
    public GameObject UnitInfoPanel;
    public GameObject ArtifactInfoPanel;
    public GameObject BonusPanel;
    public UnityEngine.UI.Text BonusItemName;
    public GameObject BonusPanelItem;
    public GameObject BonusPanelUnit;
    public GameObject BonusPanelArtifact;
    public GameObject BonusPanelConceptCard;
    public StatusList Status;
    public GameObject WeaponAbilityInfo;
    public GameObject ArtifactRarityPanel;
    public UnityEngine.UI.Text ArtifactType;
    private List<ArtifactData> mPickupArtifacts;
    private ArtifactData mCurrentArtifact;
    public RawImage PreviewArtifactImage;
    private Transform mCurrentArtifactPreview;
    private Transform mPreviewArtifact;
    private List<Transform> mPreviewArtifactControllers;
    public Transform BGRoot;
    public GameObject BonusMsgPanel;
    public UnityEngine.UI.Text BonusMsgText;
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
    private List<GachaWindow.GachaTabCategory> mTabCategoryList;
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

    public GachaWindow()
    {
      base.\u002Ector();
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

    private void Awake()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DefaultPanel, (UnityEngine.Object) null))
        this.DefaultPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TicketPanel, (UnityEngine.Object) null))
        this.TicketPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonPanel, (UnityEngine.Object) null))
        this.ButtonPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RareTab, (UnityEngine.Object) null))
        ((Component) this.RareTab).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactTab, (UnityEngine.Object) null))
        ((Component) this.ArtifactTab).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TicketTab, (UnityEngine.Object) null))
        ((Component) this.TicketTab).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NormalTab, (UnityEngine.Object) null))
        ((Component) this.NormalTab).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TabTemplate, (UnityEngine.Object) null))
        ((Component) this.TabTemplate).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TicketButtonTemplate, (UnityEngine.Object) null))
        this.TicketButtonTemplate.SetActive(false);
      this.mPreviewUnitRT = this.CreateRenderTexture();
      this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewCamera, (UnityEngine.Object) null))
        this.mPreviewCamera.set_targetTexture(this.mPreviewUnitRT);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.SiteHost);
      stringBuilder.Append(GachaWindow.GACHA_URL_PREFIX);
      stringBuilder.Append(this.DESCRIPTION_URL);
      this.mDescriptionURL = stringBuilder.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DetailButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DescriptionButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickDescription)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GachaButtonTemplate, (UnityEngine.Object) null))
        return;
      this.GachaButtonTemplate.SetActive(false);
    }

    private void Start()
    {
      this.SetupGachaList(MonoSingleton<GameManager>.Instance.Gachas);
      this.ClearTabSprites();
      this.ClearBGSprites();
      this.StartCoroutine(this.LoadGachaTabSprites());
      this.StartCoroutine(this.LoadGachaBGTextures());
      this.RefreshSummonCoin();
      this.mState = new StateMachine<GachaWindow>(this);
      this.mState.GotoState<GachaWindow.State_Init>();
    }

    private void OnEnable()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) HomeWindow.Current, (UnityEngine.Object) null))
        return;
      HomeWindow.Current.SetVisible(true);
    }

    private void Update()
    {
      if (this.mState != null)
        this.mState.Update();
      if (this.mUpdatePreviewVisibility && this.mDesiredPreviewVisibility)
      {
        if ((this.mSelectTab == GachaWindow.GachaTabCategory.RARE || this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT) && this.IsTabChanging)
        {
          this.PlayChangeEffect();
          this.IsTabChanging = false;
        }
        if (this.mSelectTab == GachaWindow.GachaTabCategory.RARE)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null) && !this.mCurrentPreview.IsLoading)
          {
            GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerCH, true);
            GameUtility.SetLayer((Component) this.mCurrentArtifactPreview, GameUtility.LayerHidden, true);
            this.mUpdatePreviewVisibility = false;
          }
        }
        else if (this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentArtifactPreview, (UnityEngine.Object) null))
        {
          GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
          GameUtility.SetLayer((Component) this.mCurrentArtifactPreview, GameUtility.LayerCH, true);
          this.mUpdatePreviewVisibility = false;
        }
      }
      if ((double) this.mBGUnitImgFadeTime < (double) this.mBGUnitImgFadeTimeMax && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBGUnitImage, (UnityEngine.Object) null))
      {
        this.mBGUnitImgFadeTime += Time.get_unscaledDeltaTime();
        float num = Mathf.Clamp01(this.mBGUnitImgFadeTime / this.mBGUnitImgFadeTimeMax);
        this.SetUnitImageAlpha(Mathf.Lerp(this.mBGUnitImgAlphaStart, this.mBGUnitImgAlphaEnd, num));
        if ((double) num >= 1.0)
        {
          this.mBGUnitImgFadeTime = 0.0f;
          this.mBGUnitImgFadeTimeMax = 0.0f;
        }
      }
      this.UpdateBG();
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject((Component) this.mCurrentPreview);
      this.mCurrentPreview = (GachaUnitPreview) null;
      GameUtility.DestroyGameObjects<GachaUnitPreview>(this.mPreviewControllers);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewUnitRT, (UnityEngine.Object) null))
      {
        RenderTexture.ReleaseTemporary(this.mPreviewUnitRT);
        this.mPreviewUnitRT = (RenderTexture) null;
      }
      GameUtility.DestroyGameObject((Component) this.mCurrentArtifactPreview);
      this.mCurrentArtifactPreview = (Transform) null;
      GameUtility.DestroyGameObjects<Transform>(this.mPreviewArtifactControllers);
      this.ClearTabSprites();
      GameUtility.DestroyGameObjects<SRPG_Button>(this.mTabList.ToArray());
      this.mTabList.Clear();
      this.mDefaultBG = (Texture2D) null;
      this.ClearBGSprites();
      using (Dictionary<string, GameObject>.KeyCollection.Enumerator enumerator = this.mTicketButtonLists.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
          GameUtility.DestroyGameObject(this.mTicketButtonLists[enumerator.Current]);
      }
      this.mTicketButtonLists = (Dictionary<string, GameObject>) null;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Refresh();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 113);
          break;
        case 2:
          this.mClicked = false;
          break;
        case 3:
          this.OnDecisionRedrawGacha();
          break;
        default:
          if (pinID == GachaWindow.IN_GACHA_CONFIRM)
          {
            this.CheckPrevGachaRequest();
            break;
          }
          if (pinID == 111)
          {
            this.OnDecideForTicketSelect();
            break;
          }
          if (pinID != 112)
            break;
          this.OnCancel();
          break;
      }
    }

    private bool IsGachaPending()
    {
      bool flag = false;
      string str = FlowNode_Variable.Get("REDRAW_GACHA_PENDING");
      if (!string.IsNullOrEmpty(str))
      {
        if (str != "1")
        {
          if (GachaResultData.IsPending)
            flag = true;
        }
        else
          flag = true;
      }
      return flag;
    }

    public GachaTopParamNew[] GetCurrentGachaLists(GachaWindow.GachaTabCategory category)
    {
      switch (category)
      {
        case GachaWindow.GachaTabCategory.RARE:
          return this.mGachaListRare.ToArray();
        case GachaWindow.GachaTabCategory.ARTIFACT:
          return this.mGachaListArtifact.ToArray();
        case GachaWindow.GachaTabCategory.TICKET:
          return this.mGachaListTicket.ToArray();
        case GachaWindow.GachaTabCategory.NORMAL:
          return this.mGachaListNormal.ToArray();
        case GachaWindow.GachaTabCategory.SPECIAL:
          if (this.mGachaListSpecials != null && this.mCurrentTabSPIndex >= 0 && this.mGachaListSpecials.Count > this.mCurrentTabSPIndex)
            return this.mGachaListSpecials[this.mCurrentTabSPIndex].lists.ToArray();
          break;
      }
      return (GachaTopParamNew[]) null;
    }

    public bool IsFreePause(GachaTopParamNew[] _list)
    {
      bool flag = false;
      for (int index = 0; index < _list.Length; ++index)
      {
        if (_list[index].IsFreePause)
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    private void Refresh()
    {
      this.SetupGachaList(MonoSingleton<GameManager>.Instance.Gachas);
      this.SetupTabList();
      this.RefreshTabList();
      this.RefreshTabState(this.mCurrentTabIndex, this.mCurrentTabSPIndex);
      this.RefreshTabActive(true);
      this.RefreshDefaultPanel();
      this.RefreshTicketButtonList();
      this.RefreshGachaState();
      this.RefreshSummonCoin();
      this.mClicked = false;
    }

    private void RefreshSummonCoin()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SummonCoin, (UnityEngine.Object) null))
        return;
      MonoSingleton<GameManager>.Instance.Player.UpdateEventCoin();
      DataSource.Bind<EventCoinData>(this.SummonCoin, MonoSingleton<GameManager>.Instance.Player.EventCoinList.Find((Predicate<EventCoinData>) (f => f.iname.Equals("IT_US_SUMMONS_01"))));
      GameParameter.UpdateAll(this.SummonCoin);
    }

    private void RefreshTicketButtonList()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey33F listCAnonStorey33F = new GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey33F();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey33F.\u003C\u003Ef__this = this;
      if (this.mSelectTab != GachaWindow.GachaTabCategory.TICKET)
        return;
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey33F.iname = this.m_CurrentGachaRequestParam.Iname;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (string.IsNullOrEmpty(listCAnonStorey33F.iname) || !this.mTicketButtonLists.ContainsKey(listCAnonStorey33F.iname))
        return;
      if (this.mGachaListTicket == null || this.mGachaListTicket.Count <= 0)
      {
        this.TicketNotListView.SetActive(true);
        // ISSUE: reference to a compiler-generated field
        this.mTicketButtonLists[listCAnonStorey33F.iname].SetActive(false);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        if (this.mGachaListTicket.FindIndex(new Predicate<GachaTopParamNew>(listCAnonStorey33F.\u003C\u003Em__328)) >= 0)
          return;
        // ISSUE: reference to a compiler-generated field
        GameUtility.DestroyGameObject(this.mTicketButtonLists[listCAnonStorey33F.iname]);
        // ISSUE: reference to a compiler-generated field
        this.mTicketButtonLists.Remove(listCAnonStorey33F.iname);
        for (int index = 0; index < this.mGachaListTicket.Count; ++index)
        {
          if (this.mTicketButtonLists.ContainsKey(this.mGachaListTicket[index].iname))
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey340 listCAnonStorey340 = new GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey340();
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey340.\u003C\u003Ef__this = this;
            GameObject ticketButtonList = this.mTicketButtonLists[this.mGachaListTicket[index].iname];
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey340.ticketlistitem = (GachaTicketListItem) ticketButtonList.GetComponent<GachaTicketListItem>();
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listCAnonStorey340.ticketlistitem, (UnityEngine.Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              listCAnonStorey340.ticketlistitem.Refresh(this.mGachaListTicket[index], index);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method pointer
              listCAnonStorey340.ticketlistitem.SetGachaButtonEvent(new UnityAction((object) listCAnonStorey340, __methodptr(\u003C\u003Em__329)));
            }
          }
        }
      }
    }

    public void SetupGachaList(GachaParam[] gparms)
    {
      this.mGachaListRare.Clear();
      this.mGachaListNormal.Clear();
      this.mGachaListArtifact.Clear();
      this.mGachaListTicket.Clear();
      this.mGachaListSpecials.Clear();
      this.mGachaListAll.Clear();
      for (int index = 0; index < gparms.Length; ++index)
      {
        GachaTopParamNew gachaTopParamNew = new GachaTopParamNew();
        gachaTopParamNew.Deserialize(gparms[index]);
        if (gachaTopParamNew.category.Contains("coin"))
          this.mGachaListRare.Add(gachaTopParamNew);
        else if (gachaTopParamNew.category.Contains("gold"))
          this.mGachaListNormal.Add(gachaTopParamNew);
        else if (gachaTopParamNew.group.Contains("bugu-"))
          this.mGachaListArtifact.Add(gachaTopParamNew);
        else if (!string.IsNullOrEmpty(gachaTopParamNew.ticket_iname))
          this.mGachaListTicket.Add(gachaTopParamNew);
        else if (!string.IsNullOrEmpty(gachaTopParamNew.asset_bg) || !string.IsNullOrEmpty(gachaTopParamNew.asset_title))
        {
          string group = gachaTopParamNew.group;
          if (this.mGachaListSpecials != null && !string.IsNullOrEmpty(group) && this.mGachaListSpecials.FindIndex((Predicate<GachaWindow.GachaTopParamNewGroups>) (s => s.group == group)) != -1)
            this.mGachaListSpecials[this.mGachaListSpecials.FindIndex((Predicate<GachaWindow.GachaTopParamNewGroups>) (s => s.group == group))].lists.Add(gachaTopParamNew);
          else
            this.mGachaListSpecials.Add(new GachaWindow.GachaTopParamNewGroups()
            {
              lists = {
                gachaTopParamNew
              },
              group = group,
              tab_image = gachaTopParamNew.asset_title
            });
        }
        else
          continue;
        this.mGachaListAll.Add(gachaTopParamNew);
      }
      if (this.mGachaListRare != null && this.mGachaListRare.Count > 1)
      {
        GachaTopParamNew[] gachaTopParamNewArray = this.SortGachaList(this.mGachaListRare);
        this.mGachaListRare.Clear();
        this.mGachaListRare.AddRange((IEnumerable<GachaTopParamNew>) gachaTopParamNewArray);
      }
      if (this.mGachaListNormal != null && this.mGachaListNormal.Count > 1)
        this.mGachaListNormal.Sort((Comparison<GachaTopParamNew>) ((a, b) => a.num - b.num));
      if (this.mGachaListSpecials == null || this.mGachaListSpecials.Count <= 0)
        return;
      using (List<GachaWindow.GachaTopParamNewGroups>.Enumerator enumerator = this.mGachaListSpecials.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GachaWindow.GachaTopParamNewGroups current = enumerator.Current;
          if (current != null)
          {
            GachaTopParamNew[] gachaTopParamNewArray = this.SortGachaList(current.lists);
            current.lists.Clear();
            current.lists.AddRange((IEnumerable<GachaTopParamNew>) gachaTopParamNewArray);
          }
        }
      }
    }

    public GachaTopParamNew[] SortGachaList(List<GachaTopParamNew> _list)
    {
      List<GachaTopParamNew> gachaTopParamNewList = new List<GachaTopParamNew>();
      this.m_CacheList_Coin.Clear();
      this.m_CacheList_Gold.Clear();
      this.m_CacheList_CoinPaid.Clear();
      using (List<GachaTopParamNew>.Enumerator enumerator = _list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GachaTopParamNew current = enumerator.Current;
          if (current.CostType == GachaCostType.COIN)
            this.m_CacheList_Coin.Add(current);
          else if (current.CostType == GachaCostType.COIN_P)
            this.m_CacheList_CoinPaid.Add(current);
          else if (current.CostType == GachaCostType.GOLD)
            this.m_CacheList_Gold.Add(current);
        }
      }
      this.m_CacheList_Coin.Sort((Comparison<GachaTopParamNew>) ((a, b) => a.num - b.num));
      this.m_CacheList_CoinPaid.Sort((Comparison<GachaTopParamNew>) ((a, b) => a.num - b.num));
      this.m_CacheList_Gold.Sort((Comparison<GachaTopParamNew>) ((a, b) => a.num - b.num));
      gachaTopParamNewList.AddRange((IEnumerable<GachaTopParamNew>) this.m_CacheList_Gold);
      gachaTopParamNewList.AddRange((IEnumerable<GachaTopParamNew>) this.m_CacheList_Coin);
      gachaTopParamNewList.AddRange((IEnumerable<GachaTopParamNew>) this.m_CacheList_CoinPaid);
      return gachaTopParamNewList.ToArray();
    }

    [DebuggerHidden]
    private IEnumerator LoadGachaBGTextures()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CLoadGachaBGTextures\u003Ec__Iterator115()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator LoadGachaTabSprites()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CLoadGachaTabSprites\u003Ec__Iterator116()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void ClearTabSprites()
    {
      if (this.mCacheTabImages == null || this.mCacheTabImages.Count <= 0)
        return;
      this.mCacheTabImages.Clear();
      this.mCacheTabImages = (Dictionary<string, Sprite>) null;
    }

    private void ClearBGSprites()
    {
      if (this.mCacheBGImages == null || this.mCacheBGImages.Count <= 0)
        return;
      this.mCacheBGImages.Clear();
      this.mCacheBGImages = (Dictionary<string, Texture2D>) null;
    }

    private void SetupTabList()
    {
      for (int index = 0; index < this.mTabList.Count; ++index)
      {
        if (((UnityEngine.Object) this.mTabList[index]).get_name().IndexOf("sp") != -1)
          UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mTabList[index]).get_gameObject());
      }
      this.mTabList.Clear();
      this.mTabCategoryList.Clear();
      if (this.mGachaListSpecials != null && this.mGachaListSpecials.Count > 0)
      {
        for (int index = 0; index < this.mGachaListSpecials.Count; ++index)
        {
          GachaTopParamNew list = this.mGachaListSpecials[index].lists[0];
          SRPG_Button tab = (SRPG_Button) UnityEngine.Object.Instantiate<SRPG_Button>((M0) this.TabTemplate);
          this.mTabList.Add(tab);
          int tab_index = index;
          ((Component) tab).get_transform().SetParent(((Component) this.TabTemplate).get_transform().get_parent(), false);
          ((UnityEngine.Object) tab).set_name("sp" + (object) index);
          this.SetTabValue(tab, list, GachaWindow.GachaTabCategory.SPECIAL, tab_index, false);
          Image component1 = (Image) ((Component) tab).get_gameObject().GetComponent<Image>();
          string tabImage = this.mGachaListSpecials[index].tab_image;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null) && !string.IsNullOrEmpty(tabImage))
          {
            if (this.mCacheTabImages.ContainsKey(tabImage))
            {
              Sprite mCacheTabImage = this.mCacheTabImages[tabImage];
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) mCacheTabImage, (UnityEngine.Object) null))
                component1.set_sprite(mCacheTabImage);
            }
          }
          else
            component1.set_sprite((Sprite) null);
          GachaTabListItem component2 = (GachaTabListItem) ((Component) tab).GetComponent<GachaTabListItem>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          {
            component2.EndAt = this.mGachaListSpecials[index].lists[0].GetTimerAt();
            component2.Disabled = this.mGachaListSpecials[index].lists[0].disabled;
            component2.GachaStartAt = this.mGachaListSpecials[index].lists[0].startat;
            component2.GachaEndtAt = this.mGachaListSpecials[index].lists[0].endat;
            component2.ListIndex = tab_index;
          }
          ((Selectable) tab).set_interactable(!list.disabled);
        }
      }
      this.mTabList.Add(this.RareTab);
      this.SetTabValue(this.RareTab, this.mGachaListRare == null || this.mGachaListRare.Count <= 0 ? (GachaTopParamNew) null : this.mGachaListRare[0], GachaWindow.GachaTabCategory.RARE, -1, true);
      if (this.IsFreePause(this.mGachaListRare.ToArray()))
      {
        BadgeValidator componentInChildren = (BadgeValidator) ((Component) this.RareTab).GetComponentInChildren<BadgeValidator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        {
          ((Behaviour) componentInChildren).set_enabled(false);
          ((Component) componentInChildren).get_gameObject().SetActive(false);
        }
      }
      this.mTabList.Add(this.TicketTab);
      this.SetTabValue(this.TicketTab, this.mGachaListTicket == null || this.mGachaListTicket.Count <= 0 ? (GachaTopParamNew) null : this.mGachaListTicket[0], GachaWindow.GachaTabCategory.TICKET, -1, true);
      this.mTabList.Add(this.NormalTab);
      this.SetTabValue(this.NormalTab, this.mGachaListNormal == null || this.mGachaListNormal.Count <= 0 ? (GachaTopParamNew) null : this.mGachaListNormal[0], GachaWindow.GachaTabCategory.NORMAL, -1, true);
    }

    private void SetTabValue(SRPG_Button tab, GachaTopParamNew param, GachaWindow.GachaTabCategory tab_category, int tab_index = -1, bool is_sibling = true)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      GachaWindow.\u003CSetTabValue\u003Ec__AnonStorey342 valueCAnonStorey342 = new GachaWindow.\u003CSetTabValue\u003Ec__AnonStorey342();
      // ISSUE: reference to a compiler-generated field
      valueCAnonStorey342.tab = tab;
      // ISSUE: reference to a compiler-generated field
      valueCAnonStorey342.tab_category = tab_category;
      // ISSUE: reference to a compiler-generated field
      valueCAnonStorey342.tab_index = tab_index;
      // ISSUE: reference to a compiler-generated field
      valueCAnonStorey342.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) valueCAnonStorey342.tab, (UnityEngine.Object) null) || valueCAnonStorey342.tab_category == GachaWindow.GachaTabCategory.NONE)
        return;
      if (param != null)
      {
        // ISSUE: reference to a compiler-generated field
        DataSource.Bind<GachaTopParamNew>(((Component) valueCAnonStorey342.tab).get_gameObject(), param);
      }
      // ISSUE: reference to a compiler-generated field
      ((Component) valueCAnonStorey342.tab).get_gameObject().SetActive(false);
      if (is_sibling)
      {
        // ISSUE: reference to a compiler-generated field
        ((Component) valueCAnonStorey342.tab).get_transform().SetAsLastSibling();
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      ((UnityEvent) valueCAnonStorey342.tab.get_onClick()).AddListener(new UnityAction((object) valueCAnonStorey342, __methodptr(\u003C\u003Em__330)));
      // ISSUE: reference to a compiler-generated field
      SerializeValueBehaviour component = (SerializeValueBehaviour) ((Component) valueCAnonStorey342.tab).GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      // ISSUE: reference to a compiler-generated field
      component.list.SetField("category", (int) valueCAnonStorey342.tab_category);
    }

    private GachaWindow.GachaTabCategory GetTabCategory(SRPG_Button tab)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("召喚タブが指定されていませ.");
        return GachaWindow.GachaTabCategory.NONE;
      }
      GachaWindow.GachaTabCategory gachaTabCategory = GachaWindow.GachaTabCategory.NONE;
      SerializeValueBehaviour component = (SerializeValueBehaviour) ((Component) tab).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        gachaTabCategory = (GachaWindow.GachaTabCategory) component.list.GetInt("category");
      return gachaTabCategory;
    }

    private void RefreshTabList()
    {
      if (MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaCoin())
      {
        int index = this.mTabList.FindIndex((Predicate<SRPG_Button>) (tab => UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) this.RareTab)));
        if (index != -1)
        {
          this.mTabList.RemoveAt(index);
          this.mTabList.Insert(0, this.RareTab);
        }
        ((Component) this.RareTab).get_transform().SetAsFirstSibling();
      }
      else
      {
        int index1 = this.mTabList.FindIndex((Predicate<SRPG_Button>) (tab => UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) this.RareTab)));
        if (index1 != -1)
        {
          this.mTabList.RemoveAt(index1);
          int index2 = this.mTabList.FindIndex((Predicate<SRPG_Button>) (tab => UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) this.TicketTab)));
          if (index2 != -1)
            this.mTabList.Insert(index2, this.RareTab);
        }
        ((Component) this.RareTab).get_transform().SetAsLastSibling();
      }
      ((Component) this.TicketTab).get_transform().SetAsLastSibling();
      ((Component) this.NormalTab).get_transform().SetAsLastSibling();
    }

    private void OnTabChange(SRPG_Button button, GachaWindow.GachaTabCategory category, int index = -1)
    {
      if (!this.TabChange(button, category, index))
        return;
      this.RefreshGachaDetailSelectID(this.mSelectTab);
      this.mState.GotoState<GachaWindow.State_CheckInitState>();
    }

    private void RefreshTabState(int index, int sp_index)
    {
      if (index < 0 || index >= this.mTabList.Count)
      {
        index = 0;
        sp_index = 0;
      }
      GachaTopParamNew dataOfClass1 = DataSource.FindDataOfClass<GachaTopParamNew>(((Component) this.mTabList[index]).get_gameObject(), (GachaTopParamNew) null);
      if (dataOfClass1 != null && dataOfClass1.disabled)
      {
        for (int index1 = 0; index1 < this.mTabList.Count; ++index1)
        {
          if (index1 != index)
          {
            SRPG_Button mTab = this.mTabList[index1];
            GachaTopParamNew dataOfClass2 = DataSource.FindDataOfClass<GachaTopParamNew>(((Component) mTab).get_gameObject(), (GachaTopParamNew) null);
            if (dataOfClass2 != null && !dataOfClass2.disabled)
            {
              index = index1;
              GachaTabListItem component = (GachaTabListItem) ((Component) mTab).GetComponent<GachaTabListItem>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && component.ListIndex >= 0)
              {
                sp_index = component.ListIndex;
                break;
              }
              break;
            }
          }
        }
      }
      else if (dataOfClass1 == null)
      {
        index = Mathf.Max(0, index - 1);
        sp_index = Mathf.Max(0, sp_index - 1);
      }
      SRPG_Button[] array = this.mTabList.ToArray();
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) array[index1], (UnityEngine.Object) null))
        {
          Transform transform = ((Component) array[index1]).get_transform().Find("cursor");
          if (index1 == index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
              ((Component) transform).get_gameObject().SetActive(true);
            ((Component) array[index1]).get_transform().set_localScale(new Vector3(1f, 1f, 1f));
          }
          else
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
              ((Component) transform).get_gameObject().SetActive(false);
            ((Component) array[index1]).get_transform().set_localScale(new Vector3(0.9f, 0.9f, 0.9f));
          }
        }
      }
      this.mCurrentTabIndex = Mathf.Max(index, 0);
      this.mCurrentTabSPIndex = Mathf.Max(sp_index, 0);
      this.mSelectTab = this.GetTabCategory(this.mTabList[index]);
      this.RefreshGachaDetailSelectID(this.mSelectTab);
    }

    private bool TabChange(SRPG_Button button, GachaWindow.GachaTabCategory category, int index = -1)
    {
      if (!((Selectable) button).IsInteractable())
        return false;
      SRPG_Button[] array = this.mTabList.ToArray();
      int num = Array.IndexOf<SRPG_Button>(array, button);
      if (category == GachaWindow.GachaTabCategory.SPECIAL)
      {
        if (this.mCurrentTabSPIndex == index)
          return false;
      }
      else if (this.mSelectTab == category)
        return false;
      for (int index1 = 0; index1 < array.Length; ++index1)
      {
        bool flag = index1 == num;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) array[index1], (UnityEngine.Object) null))
        {
          Transform transform = ((Component) array[index1]).get_transform().Find("cursor");
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
            ((Component) transform).get_gameObject().SetActive(flag);
        }
        if (flag)
        {
          ((Component) array[index1]).get_transform().set_localScale(new Vector3(1f, 1f, 1f));
        }
        else
        {
          ((Component) array[index1]).get_transform().set_localScale(new Vector3(0.9f, 0.9f, 0.9f));
          GachaTabListItem component = (GachaTabListItem) ((Component) array[index1]).GetComponent<GachaTabListItem>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || !component.Disabled)
            ((Behaviour) array[index1]).set_enabled(false);
        }
      }
      this.mSelectTab = category;
      this.mCurrentTabSPIndex = -1;
      if (category == GachaWindow.GachaTabCategory.SPECIAL)
        this.mCurrentTabSPIndex = index;
      bool flag1 = true;
      if (category == GachaWindow.GachaTabCategory.TICKET)
        flag1 = false;
      this.ResetChangeEffect();
      this.DefaultPanel.SetActive(flag1);
      this.ButtonPanel.SetActive(flag1);
      this.TicketPanel.SetActive(!flag1);
      if (this.DefaultPanel.get_activeInHierarchy())
        this.RefreshDefaultPanel();
      if (this.TicketPanel.get_activeInHierarchy())
        this.SetTicketButtonList();
      this.IsTabChanging = true;
      this.mCurrentTabIndex = num;
      this.RefreshGachaDetailSelectID(this.mSelectTab);
      return true;
    }

    private void RefreshTabEnable(bool state)
    {
      foreach (SRPG_Button srpgButton in this.mTabList.ToArray())
      {
        GachaTabListItem component = (GachaTabListItem) ((Component) srpgButton).GetComponent<GachaTabListItem>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) || !component.Disabled)
          ((Behaviour) srpgButton).set_enabled(state);
      }
    }

    private void RefreshTabActive(bool value)
    {
      foreach (Component component in this.mTabList.ToArray())
        component.get_gameObject().SetActive(value);
    }

    private void SetTicketButtonList()
    {
      if (this.mGachaListTicket == null || this.mGachaListTicket.Count <= 0)
      {
        this.TicketNotListView.SetActive(true);
      }
      else
      {
        for (int index = 0; index < this.mGachaListTicket.Count; ++index)
        {
          if (!this.mTicketButtonLists.ContainsKey(this.mGachaListTicket[index].iname))
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            GachaWindow.\u003CSetTicketButtonList\u003Ec__AnonStorey343 listCAnonStorey343 = new GachaWindow.\u003CSetTicketButtonList\u003Ec__AnonStorey343();
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey343.\u003C\u003Ef__this = this;
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TicketButtonTemplate);
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey343.ticketlistitem = (GachaTicketListItem) gameObject.GetComponent<GachaTicketListItem>();
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listCAnonStorey343.ticketlistitem, (UnityEngine.Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              listCAnonStorey343.ticketlistitem.Refresh(this.mGachaListTicket[index], index);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method pointer
              listCAnonStorey343.ticketlistitem.SetGachaButtonEvent(new UnityAction((object) listCAnonStorey343, __methodptr(\u003C\u003Em__334)));
              gameObject.get_transform().SetParent(this.TicketListRoot, false);
              gameObject.SetActive(true);
            }
            this.mTicketButtonLists.Add(this.mGachaListTicket[index].iname, gameObject);
          }
        }
      }
    }

    private void OnSelectTicket(int index)
    {
      GachaTopParamNew gachaTopParamNew = this.mGachaListTicket[index];
      this.OnExecGacha2(gachaTopParamNew.iname, 0, string.Empty, this.mGachaListTicket[index].ticket_iname, 1, this.mGachaListTicket[index].confirm, GachaCostType.TICKET, true, false, gachaTopParamNew.redraw_rest, gachaTopParamNew.redraw_num);
    }

    private void RefreshDefaultPanel()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitInfoPanel, (UnityEngine.Object) null))
        this.UnitInfoPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.RARE);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactInfoPanel, (UnityEngine.Object) null))
        this.ArtifactInfoPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
        this.BonusPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.SPECIAL);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BGRoot, (UnityEngine.Object) null))
        this.RefreshGachaBackGround();
      this.RefreshUnitInfoPanel();
      this.RefreshArtifactInfoPanel();
      this.RefreshButtonPanel();
      this.RefreshPreviews();
    }

    private void RefreshUnitInfoPanel()
    {
      if (this.mSelectTab != GachaWindow.GachaTabCategory.RARE || this.mGachaListRare == null || this.mGachaListRare.Count <= 0)
        return;
      if (!string.IsNullOrEmpty(this.BGUnitImageID))
        this.mBGUnitImage = GameObjectID.FindGameObject<RawImage>(this.BGUnitImageID);
      this.CreatePickupUnitsList(this.mGachaListRare[0].units.ToArray());
      if (this.mPickupUnits == null || this.mPickupUnits.Count <= 0)
        return;
      this.mCurrentIndex = 0;
      this.mCurrentJobIndex = 0;
      this.RefreshUnitInfo();
    }

    private void RefreshUnitInfo()
    {
      this.UpdateCurrentUnitInfo();
      this.RefreshJobs();
      if (this.mPickupUnits != null && this.mPickupUnits.Count > 0)
      {
        DataSource.Bind<UnitData>(this.UnitInfoPanel, this.mPickupUnits[this.mCurrentIndex]);
        GameParameter.UpdateAll(this.UnitInfoPanel);
      }
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
      this.StartCoroutine(this.RefreshUnitImage());
      this.FadeUnitImage(0.0f, 1f, 1f);
      this.InitGachaUnitPreview(true);
      this.SetGachaPreviewCamera();
    }

    private void RefreshArtifactInfoPanel()
    {
      if (this.mSelectTab != GachaWindow.GachaTabCategory.ARTIFACT || this.mGachaListArtifact == null || this.mGachaListArtifact.Count <= 0)
        return;
      this.CreatePickupArtifactlist(this.mGachaListArtifact[0].artifacts.ToArray());
      if (this.mPickupArtifacts == null || this.mPickupArtifacts.Count <= 0)
        return;
      this.mCurrentPickupArtIndex = 0;
      this.RefreshArtifactInfo();
    }

    private void RefreshArtifactInfo()
    {
      this.UpdateCurrentArtifactInfo();
      if (this.mCurrentArtifact == null)
        return;
      List<AbilityData> learningAbilities = this.mCurrentArtifact.LearningAbilities;
      DataSource.Bind<AbilityData>(this.WeaponAbilityInfo, (AbilityData) null);
      if (learningAbilities != null && learningAbilities.Count > 0)
      {
        this.WeaponAbilityInfo.SetActive(true);
        DataSource.Bind<AbilityData>(this.WeaponAbilityInfo, learningAbilities[0]);
      }
      else
        this.WeaponAbilityInfo.SetActive(false);
      DataSource.Bind<ArtifactParam>(this.ArtifactInfoPanel, this.mCurrentArtifact.ArtifactParam);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactRarityPanel, (UnityEngine.Object) null))
        DataSource.Bind<ArtifactData>(this.ArtifactRarityPanel, this.mCurrentArtifact);
      GameParameter.UpdateAll(this.ArtifactInfoPanel);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Status, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
      {
        BaseStatus fixed_status = new BaseStatus();
        BaseStatus scale_status = new BaseStatus();
        this.mCurrentArtifact.GetHomePassiveBuffStatus(ref fixed_status, ref scale_status, (UnitData) null, 0, true);
        this.Status.SetValues(fixed_status, scale_status, false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactType, (UnityEngine.Object) null) && this.mCurrentArtifact != null)
        this.ArtifactType.set_text(LocalizedText.Get("sys.TITLE_ARTIFACT_TYPE", new object[1]
        {
          (object) this.mCurrentArtifact.ArtifactParam.tag
        }));
      this.InitGachaArtifactPreview(true);
      this.SetGachaPreviewArtifactCamera();
    }

    private void RefreshButtonPanel()
    {
      GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(this.mSelectTab);
      if (currentGachaLists == null || currentGachaLists.Length <= 0)
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.GachaButtonTemplate, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("召喚ボタンのテンプレートが指定されていません.");
      }
      else
      {
        this.BonusPanel.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelItem, (UnityEngine.Object) null))
          this.BonusPanelItem.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelUnit, (UnityEngine.Object) null))
          this.BonusPanelUnit.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelArtifact, (UnityEngine.Object) null))
          this.BonusPanelArtifact.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelConceptCard, (UnityEngine.Object) null))
          this.BonusPanelConceptCard.SetActive(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusMsgPanel, (UnityEngine.Object) null))
          this.BonusMsgPanel.SetActive(false);
        if (this.m_GachaButtons.Count < currentGachaLists.Length)
        {
          int num = currentGachaLists.Length - this.m_GachaButtons.Count;
          for (int index = 0; index < num; ++index)
          {
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.GachaButtonTemplate);
            gameObject.get_transform().SetParent(this.ButtonPanel.get_transform(), false);
            this.m_GachaButtons.Add((GachaButton) gameObject.GetComponent<GachaButton>());
          }
        }
        using (List<GachaButton>.Enumerator enumerator = this.m_GachaButtons.GetEnumerator())
        {
          while (enumerator.MoveNext())
            ((Component) enumerator.Current).get_gameObject().SetActive(false);
        }
        for (int index = 0; index < currentGachaLists.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          GachaWindow.\u003CRefreshButtonPanel\u003Ec__AnonStorey344 panelCAnonStorey344 = new GachaWindow.\u003CRefreshButtonPanel\u003Ec__AnonStorey344();
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.\u003C\u003Ef__this = this;
          GachaButton gachaButton = this.m_GachaButtons[index];
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.param = currentGachaLists[index];
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.cost = -1;
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.exec_num = 0;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.is_nouse_free = false;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.cost_type = panelCAnonStorey344.param.CostType;
          // ISSUE: reference to a compiler-generated field
          if (panelCAnonStorey344.cost_type == GachaCostType.COIN)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey344.cost = panelCAnonStorey344.param.coin;
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (panelCAnonStorey344.cost_type == GachaCostType.COIN_P)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              panelCAnonStorey344.cost = panelCAnonStorey344.param.coin_p;
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (panelCAnonStorey344.cost_type == GachaCostType.GOLD)
              {
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                panelCAnonStorey344.cost = panelCAnonStorey344.param.gold;
              }
            }
          }
          // ISSUE: reference to a compiler-generated field
          GachaCategory category = panelCAnonStorey344.param.Category;
          // ISSUE: reference to a compiler-generated field
          int stepIndex = panelCAnonStorey344.param.step_index;
          // ISSUE: reference to a compiler-generated field
          int stepNum = panelCAnonStorey344.param.step_num;
          // ISSUE: reference to a compiler-generated field
          int ticketNum = panelCAnonStorey344.param.ticket_num;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.exec_num = panelCAnonStorey344.param.num;
          // ISSUE: reference to a compiler-generated field
          int appealType = panelCAnonStorey344.param.appeal_type;
          // ISSUE: reference to a compiler-generated field
          string btext = panelCAnonStorey344.param.btext;
          // ISSUE: reference to a compiler-generated field
          string appealMessage = panelCAnonStorey344.param.appeal_message;
          // ISSUE: reference to a compiler-generated field
          bool _is_show_stepup = !panelCAnonStorey344.param.IsStepUpUIHide;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.is_nouse_free = panelCAnonStorey344.param.IsFreePause;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          GachaButtonParam gachaButtonParam = new GachaButtonParam(panelCAnonStorey344.cost, stepIndex, stepNum, ticketNum, panelCAnonStorey344.exec_num, appealType, btext, appealMessage, _is_show_stepup, panelCAnonStorey344.is_nouse_free, panelCAnonStorey344.cost_type, category);
          gachaButton.SetupGachaButtonParam(gachaButtonParam);
          this.m_GachaButtons[index].UpdateTrigger = true;
          ((Component) this.m_GachaButtons[index]).get_gameObject().SetActive(true);
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.iname = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.category = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.ticket = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.confirm = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.isUseOnemore = false;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.iname = panelCAnonStorey344.param.iname;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.confirm = panelCAnonStorey344.param.confirm;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.ticket = panelCAnonStorey344.param.ticket_iname;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.category = panelCAnonStorey344.param.category;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey344.isUseOnemore = !panelCAnonStorey344.param.limit && !panelCAnonStorey344.param.step && (!panelCAnonStorey344.param.limit_cnt && !panelCAnonStorey344.param.redraw);
          // ISSUE: method pointer
          gachaButton.SetGachaButtonEvent(new UnityAction((object) panelCAnonStorey344, __methodptr(\u003C\u003Em__335)));
        }
        if (currentGachaLists.Length != 1)
          return;
        if (!string.IsNullOrEmpty(currentGachaLists[0].bonus_msg))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusMsgText, (UnityEngine.Object) null))
            this.BonusMsgText.set_text(currentGachaLists[0].bonus_msg.Replace("<br>", "\n"));
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusMsgPanel, (UnityEngine.Object) null))
            return;
          this.BonusMsgPanel.get_transform().SetAsLastSibling();
          this.BonusMsgPanel.SetActive(true);
        }
        else
        {
          List<GachaBonusParam> bonusItems = currentGachaLists[0].bonus_items;
          if (bonusItems == null || bonusItems.Count <= 0)
            return;
          DataSource.Bind<ItemParam>(this.BonusPanel, (ItemParam) null);
          DataSource.Bind<UnitParam>(this.BonusPanel, (UnitParam) null);
          DataSource.Bind<ArtifactParam>(this.BonusPanel, (ArtifactParam) null);
          string empty1 = string.Empty;
          using (List<GachaBonusParam>.Enumerator enumerator = bonusItems.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              GachaBonusParam current = enumerator.Current;
              if (current == null || string.IsNullOrEmpty(current.iname))
              {
                DebugUtility.LogError("オマケ表示に必要な情報が設定されていません");
              }
              else
              {
                string[] strArray = current.iname.Split('_');
                GameObject gameObject = (GameObject) null;
                string empty2 = string.Empty;
                if (strArray[0] == "IT")
                {
                  ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(current.iname);
                  if (itemParam != null)
                  {
                    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
                    stringBuilder.Append(itemParam.name);
                    stringBuilder.Append("x" + current.num.ToString());
                    empty2 = stringBuilder.ToString();
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
                      DataSource.Bind<ItemParam>(this.BonusPanel, itemParam);
                    gameObject = this.BonusPanelItem;
                  }
                  else
                    continue;
                }
                else if (strArray[0] == "UN")
                {
                  UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(current.iname);
                  if (unitParam != null)
                  {
                    UnitData unitData = this.CreateUnitData(unitParam);
                    if (unitData != null)
                    {
                      StringBuilder stringBuilder = GameUtility.GetStringBuilder();
                      stringBuilder.Append(unitParam.name);
                      empty2 = stringBuilder.ToString();
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
                        DataSource.Bind<UnitData>(this.BonusPanel, unitData);
                      gameObject = this.BonusPanelUnit;
                    }
                    else
                      continue;
                  }
                  else
                    continue;
                }
                else if (strArray[0] == "AF")
                {
                  ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(current.iname);
                  if (artifactParam != null)
                  {
                    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
                    stringBuilder.Append(artifactParam.name);
                    stringBuilder.Append("x" + current.num.ToString());
                    empty2 = stringBuilder.ToString();
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
                      DataSource.Bind<ArtifactParam>(this.BonusPanel, artifactParam);
                    gameObject = this.BonusPanelArtifact;
                  }
                  else
                    continue;
                }
                else if (strArray[0] == "TS")
                {
                  ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(current.iname);
                  ConceptCardIcon component = (ConceptCardIcon) this.BonusPanelConceptCard.GetComponent<ConceptCardIcon>();
                  if (cardDataForDisplay != null && !UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
                  {
                    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
                    stringBuilder.Append(cardDataForDisplay.Param.name);
                    stringBuilder.Append("x" + current.num.ToString());
                    empty2 = stringBuilder.ToString();
                    component.Setup(cardDataForDisplay);
                    gameObject = this.BonusPanelConceptCard;
                  }
                  else
                    continue;
                }
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusItemName, (UnityEngine.Object) null))
                  this.BonusItemName.set_text(empty2);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
                  gameObject.SetActive(true);
              }
            }
          }
          GameParameter.UpdateAll(this.BonusPanel);
          this.BonusPanel.get_transform().SetAsLastSibling();
          this.BonusPanel.SetActive(true);
        }
      }
    }

    private void RefreshGachaDetailSelectID(GachaWindow.GachaTabCategory category)
    {
      FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.TITLE_POPUP_GACHA_DETAIL"));
      GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(category);
      if (currentGachaLists == null || currentGachaLists.Length <= 0)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.SiteHost);
      stringBuilder.Append(GachaWindow.GACHA_URL_PREFIX);
      stringBuilder.Append(currentGachaLists[0].detail_url);
      this.mDetailURL = stringBuilder.ToString();
    }

    private void OnClickDetail()
    {
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.mDetailURL);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 999);
    }

    private void OnClickDescription()
    {
      FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", this.mDescriptionURL);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 999);
    }

    private void CreatePickupUnitsList(UnitParam[] units)
    {
      this.mPickupUnits = new List<UnitData>();
      for (int index = 0; index < units.Length; ++index)
      {
        UnitData unitData = this.CreateUnitData(units[index]);
        if (unitData != null)
          this.mPickupUnits.Add(unitData);
      }
    }

    private void CreatePickupArtifactlist(ArtifactParam[] artifacts)
    {
      this.mPickupArtifacts = new List<ArtifactData>();
      for (int index = 0; index < artifacts.Length; ++index)
      {
        ArtifactData artifactData = this.CreateArtifactData(artifacts[index]);
        if (artifacts != null)
          this.mPickupArtifacts.Add(artifactData);
      }
    }

    private void PlayChangeEffect()
    {
      if (this.mSelectTab != GachaWindow.GachaTabCategory.RARE && this.mSelectTab != GachaWindow.GachaTabCategory.ARTIFACT)
        return;
      Animator animator = (Animator) null;
      if (this.mSelectTab == GachaWindow.GachaTabCategory.RARE)
        animator = (Animator) this.ChangeUnitEffectObj.GetComponent<Animator>();
      else if (this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
        animator = (Animator) this.ChangeArtifactEffectObj.GetComponent<Animator>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) animator, (UnityEngine.Object) null))
        return;
      animator.ResetTrigger("onResetTrigger");
      if (this.ChangeJob)
        animator.SetTrigger("onChangeJobF");
      else
        animator.SetTrigger("onChangeUnitF");
    }

    private void ResetChangeEffect()
    {
      Animator component1 = (Animator) this.ChangeUnitEffectObj.GetComponent<Animator>();
      component1.ResetTrigger("onChangeJobF");
      component1.ResetTrigger("onChangeUnitF");
      component1.SetTrigger("onResetTrigger");
      Animator component2 = (Animator) this.ChangeArtifactEffectObj.GetComponent<Animator>();
      component2.ResetTrigger("onChangeJobF");
      component2.ResetTrigger("onChangeUnitF");
      component2.SetTrigger("onResetTrigger");
    }

    private void RefreshGachaState()
    {
      if (this.mSelectTab == GachaWindow.GachaTabCategory.RARE)
      {
        this.mCurrentIndex = this.mCurrentJobIndex = 0;
        this.UpdateCurrentUnitInfo();
        this.mState.GotoState<GachaWindow.State_WaitActionAnimation>();
      }
      else
      {
        if (this.mSelectTab != GachaWindow.GachaTabCategory.ARTIFACT)
          return;
        bool flag = false;
        this.ChangeJob = flag;
        this.ChangeUnit = flag;
        this.mState.GotoState<GachaWindow.State_WaitActionAnimation>();
      }
    }

    private void RefreshGachaBackGround()
    {
      if (this.mSelectTab == GachaWindow.GachaTabCategory.TICKET)
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.BGRoot, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("=== GachaWindow->RefreshGachaBackGround:BGRoot is null ===");
      }
      else
      {
        GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(this.mSelectTab);
        if (currentGachaLists == null || currentGachaLists.Length <= 0)
        {
          DebugUtility.LogError("=== GachaWindow->RefreshGachaBackGround:param is Null ===");
        }
        else
        {
          string assetBg = currentGachaLists[0].asset_bg;
          this.IsRefreshingGachaBG = true;
          this.RefreshGachaBGImage(assetBg);
        }
      }
    }

    private void RefreshGachaBGImage(string image)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDefaultBG, (UnityEngine.Object) null) && this.mCacheBGImages != null && this.mCacheBGImages.Count > 0)
      {
        DebugUtility.Log("=== GachaWindow.cs->RefreshGachaBGImage:mDefaultBG Initalize");
        this.mDefaultBG = !this.mCacheBGImages.ContainsKey(GachaWindow.DEFAULT_BGIMAGE_PATH) ? (Texture2D) null : this.mCacheBGImages[GachaWindow.DEFAULT_BGIMAGE_PATH];
      }
      Transform bg00 = this.mBGObjects == null || this.mBGObjects.Count < 2 ? this.BGRoot.FindChild("bg00") : this.mBGObjects[0];
      Transform bg01 = this.mBGObjects == null || this.mBGObjects.Count < 2 ? this.BGRoot.FindChild("bg01") : this.mBGObjects[1];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) bg00, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) bg01, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():bg00 or bg01 is Null ===");
        this.IsRefreshingGachaBG = false;
      }
      else
      {
        RawImage component1 = (RawImage) ((Component) bg00).GetComponent<RawImage>();
        RawImage component2 = (RawImage) ((Component) bg01).GetComponent<RawImage>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():bg00_raw or bg01_raw is Null ===");
          this.IsRefreshingGachaBG = false;
        }
        else
        {
          CanvasGroup component3 = (CanvasGroup) ((Component) bg00).get_gameObject().GetComponent<CanvasGroup>();
          CanvasGroup component4 = (CanvasGroup) ((Component) bg01).get_gameObject().GetComponent<CanvasGroup>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component3, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) component4, (UnityEngine.Object) null))
          {
            DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():canvas00 or canvas01 is Null ===");
            this.IsRefreshingGachaBG = false;
          }
          else
          {
            if (this.mBGObjects.FindIndex((Predicate<Transform>) (s => UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) s).get_gameObject(), (UnityEngine.Object) ((Component) bg00).get_gameObject()))) == -1)
              this.mBGObjects.Add(bg00);
            if (this.mBGObjects.FindIndex((Predicate<Transform>) (s => UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) s).get_gameObject(), (UnityEngine.Object) ((Component) bg01).get_gameObject()))) == -1)
              this.mBGObjects.Add(bg01);
            if (this.mCacheBGImages != null && this.mCacheBGImages.Count > 0 && !string.IsNullOrEmpty(image))
            {
              Texture2D mDefaultBg = this.mDefaultBG;
              Texture2D texture2D1 = (Texture2D) null;
              Texture2D texture2D2 = !this.mCacheBGImages.ContainsKey(image + "_0") ? mDefaultBg : this.mCacheBGImages[image + "_0"];
              Texture2D texture2D3 = !this.mCacheBGImages.ContainsKey(image + "_1") ? texture2D1 : this.mCacheBGImages[image + "_1"];
              component1.set_texture((Texture) texture2D2);
              component2.set_texture((Texture) texture2D3);
            }
            else
            {
              component1.set_texture((Texture) this.mDefaultBG);
              component2.set_texture((Texture) null);
            }
            component3.set_alpha(1f);
            component4.set_alpha(0.0f);
            this.mExistSwapBG = UnityEngine.Object.op_Inequality((UnityEngine.Object) component2.get_texture(), (UnityEngine.Object) null);
            this.mWaitSwapBGTime = GachaWindow.WAIT_SWAP_BG;
            this.mEnableBGIndex = 0;
            this.IsRefreshingGachaBG = false;
          }
        }
      }
    }

    private void UpdateBG()
    {
      if (!this.mExistSwapBG)
        return;
      this.mWaitSwapBGTime -= Time.get_deltaTime();
      if ((double) this.mWaitSwapBGTime >= 0.0)
        return;
      Transform transform1 = this.mBGObjects != null || this.mBGObjects.Count < 2 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBGObjects[0], (UnityEngine.Object) null) ? this.BGRoot.FindChild("bg00") : this.mBGObjects[0];
      Transform transform2 = this.mBGObjects != null || this.mBGObjects.Count < 2 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBGObjects[1], (UnityEngine.Object) null) ? this.BGRoot.FindChild("bg01") : this.mBGObjects[1];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) transform1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) transform2, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("=== GachaWindow->UpdateBG:bg00 or bg01 is null ===");
      }
      else
      {
        RawImage component1 = (RawImage) ((Component) transform1).GetComponent<RawImage>();
        RawImage component2 = (RawImage) ((Component) transform2).GetComponent<RawImage>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("=== GachaWindow->UpdateBG:bg00_image & bg01_image is null ===");
        }
        else
        {
          CanvasGroup component3 = (CanvasGroup) ((Component) component1).get_gameObject().GetComponent<CanvasGroup>();
          CanvasGroup component4 = (CanvasGroup) ((Component) component2).get_gameObject().GetComponent<CanvasGroup>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) component4, (UnityEngine.Object) null))
          {
            DebugUtility.LogError("=== GachaWindow->UpdateBG:canvas00 & canvas01 is null ===");
          }
          else
          {
            float deltaTime = Time.get_deltaTime();
            CanvasGroup canvasGroup1 = component3;
            canvasGroup1.set_alpha(canvasGroup1.get_alpha() + (this.mEnableBGIndex != 0 ? deltaTime : -deltaTime));
            CanvasGroup canvasGroup2 = component4;
            canvasGroup2.set_alpha(canvasGroup2.get_alpha() + (this.mEnableBGIndex != 0 ? -deltaTime : deltaTime));
            component3.set_alpha(Mathf.Clamp(component3.get_alpha() + (this.mEnableBGIndex != 0 ? deltaTime : -deltaTime), 0.0f, 1f));
            component4.set_alpha(Mathf.Clamp(component4.get_alpha() + (this.mEnableBGIndex != 0 ? -deltaTime : deltaTime), 0.0f, 1f));
            if ((double) component3.get_alpha() > 0.0 && (double) component3.get_alpha() < 1.0)
              return;
            this.mEnableBGIndex ^= 1;
            this.mWaitSwapBGTime = GachaWindow.WAIT_SWAP_BG;
          }
        }
      }
    }

    private void InitGachaUnitPreview(bool reload = true)
    {
      if (!string.IsNullOrEmpty(this.PickupPreviewParentID))
        this.mPreviewParent = GameObjectID.FindGameObject<Transform>(this.PickupPreviewParentID);
      if (!string.IsNullOrEmpty(this.PickUpPreviewBaseID))
      {
        this.mPreviewBase = GameObjectID.FindGameObject(this.PickUpPreviewBaseID);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null))
          this.mPreviewBase.SetActive(false);
      }
      if (!string.IsNullOrEmpty(this.PickUpCameraID))
        this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
      if (!reload)
        return;
      this.ReloadPickUpUnitView();
    }

    private void InitGachaArtifactPreview(bool reload = true)
    {
      if (!string.IsNullOrEmpty(this.PickupPreviewArtifact))
        this.mPreviewArtifact = GameObjectID.FindGameObject<Transform>(this.PickupPreviewArtifact);
      if (!string.IsNullOrEmpty(this.PickUpCameraID))
        this.mPreviewCamera = GameObjectID.FindGameObject<Camera>(this.PickUpCameraID);
      if (!reload)
        return;
      this.StartCoroutine(this.ReloadPickupArtifactPreview(true));
    }

    [DebuggerHidden]
    private IEnumerator ReloadPickupArtifactPreview(bool reload = true)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CReloadPickupArtifactPreview\u003Ec__Iterator117()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void ReloadPickUpUnitView()
    {
      if (this.CurrentUnit == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewParent, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects<GachaUnitPreview>(this.mPreviewControllers);
      this.mPreviewControllers.Clear();
      this.mCurrentPreview = (GachaUnitPreview) null;
      for (int index = 0; index < this.mCurrentUnit.UnitParam.jobsets.Length; ++index)
      {
        GachaUnitPreview gachaUnitPreview = (GachaUnitPreview) null;
        if (this.mCurrentUnit.Jobs[index] != null && this.mCurrentUnit.Jobs[index].Param != null)
        {
          GameObject gameObject = new GameObject("Preview", new System.Type[1]
          {
            typeof (GachaUnitPreview)
          });
          gachaUnitPreview = (GachaUnitPreview) gameObject.GetComponent<GachaUnitPreview>();
          gachaUnitPreview.DefaultLayer = GameUtility.LayerHidden;
          gachaUnitPreview.SetGachaUnitData(this.mCurrentUnit, this.mCurrentUnit.Jobs[index].JobID);
          gachaUnitPreview.SetupUnit(this.mCurrentUnit.UnitParam.iname, this.mCurrentUnit.Jobs[index].JobID);
          gameObject.get_transform().SetParent(this.mPreviewParent, false);
          if (index == this.mCurrentUnit.JobIndex)
            this.mCurrentPreview = gachaUnitPreview;
        }
        this.mPreviewControllers.Add(gachaUnitPreview);
      }
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentPreview, (UnityEngine.Object) null))
        return;
      GameObject gameObject1 = new GameObject("Preview", new System.Type[1]
      {
        typeof (GachaUnitPreview)
      });
      this.mCurrentPreview = (GachaUnitPreview) gameObject1.GetComponent<GachaUnitPreview>();
      this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
      this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
      gameObject1.get_transform().SetParent(this.mPreviewParent, false);
      this.mPreviewControllers.Add(this.mCurrentPreview);
    }

    private void UpdateCurrentUnitInfo()
    {
      if (this.mPickupUnits.Count > 0)
      {
        this.mCurrentUnit = this.mPickupUnits[this.mCurrentIndex];
        this.mCurrentUnit.SetJobIndex(this.mCurrentJobIndex);
      }
      else
        Debug.LogError((object) "mPickupUnits.unitsがNullもしくはCount=0です.");
    }

    private void UpdateCurrentArtifactInfo()
    {
      if (this.mPickupArtifacts.Count <= 0)
        return;
      this.mCurrentArtifact = this.mPickupArtifacts[this.mCurrentPickupArtIndex];
    }

    private void ChangePreviewUnit()
    {
      ++this.mCurrentIndex;
      if (this.mCurrentIndex <= this.mPickupUnits.Count - 1)
        return;
      this.mCurrentIndex = 0;
    }

    private void ChangePreviewArtifact()
    {
      ++this.mCurrentPickupArtIndex;
      if (this.mCurrentPickupArtIndex <= this.mPickupArtifacts.Count - 1)
        return;
      this.mCurrentPickupArtIndex = 0;
    }

    private void RefreshPreviews()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreviewBase, (UnityEngine.Object) null) && !this.mPreviewBase.get_activeSelf())
      {
        GameUtility.SetLayer(this.mPreviewBase, GameUtility.LayerCH, true);
        this.mPreviewBase.SetActive(true);
      }
      this.mDesiredPreviewVisibility = true;
      this.mUpdatePreviewVisibility = true;
    }

    private void SetActivePreview(int index)
    {
      GachaUnitPreview previewController = this.mPreviewControllers[index];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) previewController, (UnityEngine.Object) this.mCurrentPreview))
        return;
      GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
      GameUtility.SetLayer((Component) previewController, GameUtility.LayerCH, true);
      this.mCurrentPreview = previewController;
    }

    private void SetActivePreviewArtifact(int index)
    {
      if (this.mPreviewArtifactControllers.Count <= index)
        return;
      Transform artifactController = this.mPreviewArtifactControllers[index];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) artifactController, (UnityEngine.Object) null))
        return;
      GameUtility.SetLayer((Component) this.mCurrentArtifactPreview, GameUtility.LayerHidden, true);
      GameUtility.SetLayer((Component) artifactController, GameUtility.LayerCH, true);
      this.mCurrentArtifactPreview = artifactController;
    }

    private bool SelectJob()
    {
      ++this.mCurrentJobIndex;
      if (this.mCurrentJobIndex >= this.mCurrentUnit.NumJobsAvailable)
      {
        this.mChangeJob = false;
        this.mCurrentJobIndex = 0;
        return false;
      }
      this.mChangeJob = true;
      return true;
    }

    private void RefreshJobs()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectJobIcon, (UnityEngine.Object) null))
        return;
      ((GameParameter) this.SelectJobIcon.GetComponent<GameParameter>()).Index = this.mCurrentJobIndex;
    }

    [DebuggerHidden]
    private IEnumerator CreateConfirm(GachaRequestParam _param, bool _is_coin_status = true)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CCreateConfirm\u003Ec__Iterator118()
      {
        _param = _param,
        \u003C\u0024\u003E_param = _param,
        \u003C\u003Ef__this = this
      };
    }

    private void CreateGachaConfirmWindow(string confirm, int cost, string ticket = "", GachaCostType type = GachaCostType.NONE, bool isShowCoinStatus = true)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.GachaConfirmWindow, (UnityEngine.Object) null))
        return;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.GachaConfirmWindow);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      SRPG.GachaConfirmWindow component = (SRPG.GachaConfirmWindow) gameObject.GetComponent<SRPG.GachaConfirmWindow>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.ConfirmText = confirm;
      component.Cost = cost;
      component.UseTicket = ticket;
      component.GachaCostType = type;
      component.IsShowCoinStatus = isShowCoinStatus;
      component.OnDecide = new SRPG.GachaConfirmWindow.DecideEvent(this.OnDecide);
      component.OnCancel = new SRPG.GachaConfirmWindow.CancelEvent(this.OnCancel);
      DataSource.Bind<ItemParam>(gameObject, (ItemParam) null);
      if (string.IsNullOrEmpty(ticket))
        return;
      ItemParam itemParam = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(ticket);
      if (itemParam == null)
        return;
      DataSource.Bind<ItemParam>(gameObject, itemParam);
    }

    private void OnExecGacha(string iname, string input, int cost, string type, string category = "", string ticket = "", int num = 0, bool isConfirm = false, string confirm = "", GachaCostType cost_type = GachaCostType.NONE, bool isUseOneMore = false, bool isNoUseFree = false)
    {
      this.OnExecGacha2(iname, cost, category, ticket, num, confirm, cost_type, isUseOneMore, isNoUseFree, 0, 0);
    }

    private void OnExecGacha2(string _iname, int _cost, string _category, string _ticket, int _num, string _confirm, GachaCostType _cost_type, bool _is_use_onemore = false, bool _is_no_use_free = false, int _redraw_rest = 0, int _redraw_num = 0)
    {
      if (!this.Initialized || this.Clicked)
        return;
      this.mClicked = true;
      DataSource.Bind<GachaRequestParam>(((Component) this.RootObject.get_parent()).get_gameObject(), (GachaRequestParam) null);
      GachaRequestParam gachaRequest = this.CreateGachaRequest(_iname, _cost, _category, _ticket, _num, _confirm, _cost_type, _is_use_onemore, _is_no_use_free, _redraw_rest, _redraw_num);
      this.m_CurrentGachaRequestParam = gachaRequest;
      if (_cost_type == GachaCostType.GOLD || gachaRequest.IsFree)
        this.OnDecide();
      else if (gachaRequest.IsTicketGacha)
      {
        FlowNode_Variable.Set("USE_TICKET_INAME", _ticket);
        GachaTopParamNew gachaTopParamNew = this.mGachaListTicket.Find((Predicate<GachaTopParamNew>) (p => p.iname == _iname));
        if (gachaTopParamNew != null)
          FlowNode_Variable.Set("USE_TICKET_MAX", !gachaTopParamNew.redraw ? string.Empty : "1");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
      }
      else
        this.StartCoroutine(this.CreateConfirm(gachaRequest, true));
    }

    private void OnDecide(GameObject dialog)
    {
      this.OnDecide();
    }

    private void OnDecideForTicketSelect()
    {
      int useTicketNum = MonoSingleton<GachaManager>.Instance.UseTicketNum;
      if (this.m_CurrentGachaRequestParam != null)
        this.m_CurrentGachaRequestParam.SetNum(useTicketNum);
      this.OnDecide();
    }

    private void OnDecisionRedrawGacha()
    {
      if (this.m_CurrentGachaRequestParam == null)
      {
        if (GachaResultData.drops != null && GachaResultData.IsPending)
        {
          this.m_CurrentGachaRequestParam = this.CreateGachaRequest(GachaResultData.receipt.iname);
          if (this.m_CurrentGachaRequestParam == null)
          {
            DebugUtility.LogError("召喚リクエストがありません.");
            return;
          }
        }
        else
        {
          DebugUtility.LogError("前回の召喚リクエストが存在しません");
          return;
        }
      }
      FlowNode_ExecGacha2 componentInParent = (FlowNode_ExecGacha2) ((Component) this).GetComponentInParent<FlowNode_ExecGacha2>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("FlowNode_ExecGacha2がありません.");
      }
      else
      {
        this.mState.GotoState<GachaWindow.State_PauseState>();
        componentInParent.OnExecGachaDecision(this.m_CurrentGachaRequestParam);
      }
    }

    private void OnDecide()
    {
      this.mClicked = false;
      FlowNode_ExecGacha2 componentInParent = (FlowNode_ExecGacha2) ((Component) this).GetComponentInParent<FlowNode_ExecGacha2>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null) || this.m_CurrentGachaRequestParam == null)
        return;
      DataSource.Bind<GachaRequestParam>(((Component) this.RootObject.get_parent()).get_gameObject(), this.m_CurrentGachaRequestParam);
      this.mState.GotoState<GachaWindow.State_PauseState>();
      componentInParent.OnExecGacha(this.m_CurrentGachaRequestParam);
    }

    private void OnCancel(GameObject dialog)
    {
      this.OnCancel();
    }

    private void OnCancel()
    {
      this.mClicked = false;
    }

    private GachaRequestParam CreateGachaRequest(string _iname)
    {
      GachaRequestParam gachaRequestParam = (GachaRequestParam) null;
      if (!string.IsNullOrEmpty(_iname))
      {
        GachaTopParamNew gachaTopParamNew = this.mGachaListAll.Find((Predicate<GachaTopParamNew>) (p => p.iname == _iname));
        if (gachaTopParamNew == null)
        {
          DebugUtility.LogError("前回の召喚リクエスト情報がありません");
        }
        else
        {
          string iname = gachaTopParamNew.iname;
          int cost = gachaTopParamNew.Cost;
          string category = gachaTopParamNew.category;
          string ticketIname = gachaTopParamNew.ticket_iname;
          int _num = !string.IsNullOrEmpty(ticketIname) ? 1 : gachaTopParamNew.num;
          string confirm = gachaTopParamNew.confirm;
          GachaCostType costType = gachaTopParamNew.CostType;
          bool _is_use_onemore = string.IsNullOrEmpty(ticketIname) && (!gachaTopParamNew.limit && !gachaTopParamNew.step && !gachaTopParamNew.limit_cnt && !gachaTopParamNew.redraw);
          bool isFreePause = gachaTopParamNew.IsFreePause;
          int redrawRest = gachaTopParamNew.redraw_rest;
          int redrawNum = gachaTopParamNew.redraw_num;
          gachaRequestParam = this.CreateGachaRequest(iname, cost, category, ticketIname, _num, confirm, costType, _is_use_onemore, isFreePause, redrawRest, redrawNum);
          if (gachaRequestParam == null)
            DebugUtility.LogError("召喚リクエストの生成に失敗しました.");
        }
      }
      return gachaRequestParam;
    }

    private void CheckPrevGachaRequest()
    {
      if (this.m_CurrentGachaRequestParam == null)
      {
        if (GachaResultData.drops != null && GachaResultData.IsPending)
        {
          GachaRequestParam gachaRequest = this.CreateGachaRequest(GachaResultData.receipt.iname);
          if (gachaRequest == null)
          {
            DebugUtility.LogError("召喚リクエストの生成に失敗しました.");
            return;
          }
          this.m_CurrentGachaRequestParam = gachaRequest;
        }
        else
        {
          DebugUtility.LogError("前回の召喚リクエストが存在しません");
          return;
        }
      }
      if (this.m_CurrentGachaRequestParam.IsRedrawGacha && GachaResultData.drops != null)
        this.m_CurrentGachaRequestParam.SetRedraw(GachaResultData.RedrawRest, this.m_CurrentGachaRequestParam.RedrawNum);
      if (this.m_CurrentGachaRequestParam.IsTicketGacha)
      {
        if (this.m_CurrentGachaRequestParam.IsRedrawGacha)
        {
          this.StartCoroutine(this.CreateConfirm(this.m_CurrentGachaRequestParam, true));
        }
        else
        {
          FlowNode_Variable.Set("USE_TICKET_INAME", this.m_CurrentGachaRequestParam.Ticket);
          GachaTopParamNew gachaTopParamNew = this.mGachaListTicket.Find((Predicate<GachaTopParamNew>) (p => p.iname == this.m_CurrentGachaRequestParam.Iname));
          if (gachaTopParamNew != null)
            FlowNode_Variable.Set("USE_TICKET_MAX", !gachaTopParamNew.redraw ? string.Empty : "1");
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
        }
      }
      else
      {
        if (this.m_CurrentGachaRequestParam.IsSingle)
        {
          int _free = this.m_CurrentGachaRequestParam.Free;
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          if (this.m_CurrentGachaRequestParam.Category == GachaCategory.DEFAULT_NORMAL)
          {
            if (!this.IsFreePause(this.mGachaListNormal.ToArray()))
              _free = !player.CheckFreeGachaGold() || player.CheckFreeGachaGoldMax() ? 0 : 1;
          }
          else if (this.m_CurrentGachaRequestParam.Category == GachaCategory.DEFAULT_RARE && this.m_CurrentGachaRequestParam.CostType == GachaCostType.COIN && !this.IsFreePause(this.mGachaListRare.ToArray()))
            _free = !player.CheckFreeGachaCoin() ? 0 : 1;
          this.m_CurrentGachaRequestParam.SetFree(_free);
        }
        if (this.m_CurrentGachaRequestParam.IsGold || this.m_CurrentGachaRequestParam.IsFree)
          this.OnDecide();
        else
          this.StartCoroutine(this.CreateConfirm(this.m_CurrentGachaRequestParam, true));
      }
    }

    private string GetConfirmText(int _cost, bool _ispaid, string _confirm = null)
    {
      string key = GachaWindow.TEXT_COST_GACHA_COIN;
      string confirmGachaCoin = GachaWindow.TEXT_CONFIRM_GACHA_COIN;
      if (_ispaid)
        key = GachaWindow.TEXT_COST_GACHA_PAIDCOIN;
      string str1 = LocalizedText.Get(key);
      string str2 = LocalizedText.Get(_confirm);
      if (string.IsNullOrEmpty(str2))
        str2 = LocalizedText.Get(confirmGachaCoin, (object) str1, (object) _cost.ToString());
      if (_cost == 0)
        str2 = LocalizedText.Get(GachaWindow.TEXT_CONFIRM_GACHA_COST_ZERO);
      return str2;
    }

    private GachaRequestParam CreateGachaRequest(string _iname, int _cost, string _category, string _ticket, int _num, string _confirm, GachaCostType _cost_type, bool _is_use_onemore = false, bool _is_no_use_free = false, int _redraw_rest = 0, int _redraw_num = 0)
    {
      GachaCategory _category1 = GachaCategory.NONE;
      if (!string.IsNullOrEmpty(_category))
      {
        if (_category.Contains("gold"))
          _category1 = GachaCategory.DEFAULT_NORMAL;
        else if (_category.Contains("coin"))
          _category1 = GachaCategory.DEFAULT_RARE;
      }
      int _free = 0;
      if (_category1 == GachaCategory.DEFAULT_RARE && _num == 1 && (_cost_type == GachaCostType.COIN && !_is_no_use_free))
        _free = !MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaCoin() ? 0 : 1;
      else if (_category1 == GachaCategory.DEFAULT_NORMAL && _num == 1)
        _free = !MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaGold() || MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaGoldMax() ? 0 : 1;
      string confirmText = this.GetConfirmText(_cost, _cost_type == GachaCostType.COIN_P, _confirm);
      GachaRequestParam gachaRequestParam = new GachaRequestParam(_iname, _cost, confirmText, _cost_type, _category1, _is_use_onemore, false);
      if (!string.IsNullOrEmpty(_ticket))
        gachaRequestParam.SetTicketInfo(_ticket, _num);
      if (_redraw_rest > 0 && _redraw_num > 0)
        gachaRequestParam.SetRedraw(_redraw_rest, _redraw_num);
      gachaRequestParam.SetFree(_free);
      gachaRequestParam.SetNum(_num);
      return gachaRequestParam;
    }

    private RenderTexture CreateRenderTexture()
    {
      int num = Mathf.FloorToInt(864f);
      return RenderTexture.GetTemporary(num, num, 16, (RenderTextureFormat) 7);
    }

    public void SetGachaPreviewCamera()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewCamera, (UnityEngine.Object) null))
        return;
      this.mPreviewCamera.set_targetTexture(this.mPreviewUnitRT);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreviewImage, (UnityEngine.Object) null))
        return;
      this.PreviewImage.set_texture((Texture) this.mPreviewUnitRT);
    }

    public void SetGachaPreviewArtifactCamera()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewCamera, (UnityEngine.Object) null))
        return;
      this.mPreviewCamera.set_targetTexture(this.mPreviewUnitRT);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PreviewArtifactImage, (UnityEngine.Object) null))
        return;
      this.PreviewArtifactImage.set_texture((Texture) this.mPreviewUnitRT);
    }

    private void FadeUnitImage(float alphastart, float alphaend, float duration)
    {
      this.mBGUnitImgAlphaStart = alphastart;
      this.mBGUnitImgAlphaEnd = alphaend;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = duration;
      if ((double) duration > 0.0)
        return;
      this.SetUnitImageAlpha(this.mBGUnitImgAlphaEnd);
    }

    private void SetUnitImageAlpha(float alpha)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBGUnitImage, (UnityEngine.Object) null))
        return;
      Color color = ((Graphic) this.mBGUnitImage).get_color();
      color.a = (__Null) (double) alpha;
      ((Graphic) this.mBGUnitImage).set_color(color);
    }

    [DebuggerHidden]
    private IEnumerator RefreshUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CRefreshUnitImage\u003Ec__Iterator119()
      {
        \u003C\u003Ef__this = this
      };
    }

    private UnitData CreateUnitData(UnitParam param)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit()
      {
        iid = 1,
        iname = param.iname,
        exp = 0,
        lv = 1,
        plus = 0,
        rare = 0,
        select = new Json_UnitSelectable(),
        jobs = (Json_Job[]) null,
        abil = (Json_MasterAbility) null
      };
      json.abil = (Json_MasterAbility) null;
      if (param.jobsets != null && param.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(param.jobsets.Length);
        int num = 1;
        for (int index = 0; index < param.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(param.jobsets[index]);
          if (jobSetParam != null)
            jsonJobList.Add(new Json_Job()
            {
              iid = (long) num++,
              iname = jobSetParam.job,
              rank = 0,
              equips = (Json_Equip[]) null,
              abils = (Json_Ability[]) null
            });
        }
        json.jobs = jsonJobList.ToArray();
      }
      unitData.Deserialize(json);
      unitData.SetUniqueID(1L);
      unitData.JobRankUp(0);
      return unitData;
    }

    private ArtifactData CreateArtifactData(ArtifactParam param)
    {
      ArtifactData artifactData = new ArtifactData();
      Json_Artifact json = new Json_Artifact();
      json.iid = 1L;
      json.iname = param.iname;
      json.rare = param.raremax;
      json.fav = 0;
      RarityParam rarityParam = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(param.raremax);
      json.exp = ArtifactData.StaticCalcExpFromLevel((int) rarityParam.ArtifactLvCap);
      artifactData.Deserialize(json);
      return artifactData;
    }

    private string ConvertFullWidth(string half)
    {
      string str = (string) null;
      for (int index = 0; index < half.Length; ++index)
        str += (string) (object) (char) ((uint) half[index] + (uint) GachaWindow.ConversionConstant);
      return str;
    }

    public class GachaTopParamNewGroups
    {
      public List<GachaTopParamNew> lists = new List<GachaTopParamNew>();
      public string group;
      public string tab_image;
    }

    public enum GachaTabCategory : byte
    {
      NONE,
      RARE,
      ARTIFACT,
      TICKET,
      NORMAL,
      SPECIAL,
    }

    private class State_Init : State<GachaWindow>
    {
      public override void Update(GachaWindow self)
      {
        if (!self.mLoadGachaTabSprites || !self.mLoadBackGroundTexture)
          return;
        self.SetupTabList();
        self.RefreshTabList();
        SRPG_Button mTab = self.mTabList[0];
        GachaWindow.GachaTabCategory tabCategory = self.GetTabCategory(mTab);
        int index = 0;
        self.TabChange(mTab, tabCategory, index);
        self.RefreshGachaDetailSelectID(self.mSelectTab);
        self.mInitialized = true;
        self.mClicked = false;
        self.mState.GotoState<GachaWindow.State_CheckInitState>();
      }
    }

    private class State_CheckInitState : State<GachaWindow>
    {
      public override void Update(GachaWindow self)
      {
        if (self.IsRefreshingGachaBG)
          return;
        self.RefreshTabActive(true);
        self.RefreshTabEnable(true);
        if (self.IsGachaPending())
          self.mState.GotoState<GachaWindow.State_ToGachaResult>();
        else if (self.mSelectTab == GachaWindow.GachaTabCategory.RARE || self.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
        {
          GachaWindow gachaWindow = self;
          bool flag = false;
          self.ChangeJob = flag;
          int num = flag ? 1 : 0;
          gachaWindow.ChangeUnit = num != 0;
          self.mState.GotoState<GachaWindow.State_WaitActionAnimation>();
        }
        else
          self.mState.GotoState<GachaWindow.State_PauseState>();
      }
    }

    private class State_RefreshPreview : State<GachaWindow>
    {
      private WaitForSeconds wait;

      public override void Begin(GachaWindow self)
      {
        this.wait = new WaitForSeconds(!self.ChangeUnit ? self.ChangeJobWaitEffectTime : self.ChangeUnitWaitEffectTime);
        self.StartCoroutine(this.RebuildPreviewController(self));
      }

      [DebuggerHidden]
      private IEnumerator RebuildPreviewController(GachaWindow self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaWindow.State_RefreshPreview.\u003CRebuildPreviewController\u003Ec__Iterator11A()
        {
          self = self,
          \u003C\u0024\u003Eself = self,
          \u003C\u003Ef__this = this
        };
      }

      private void RebuildUnitPreview()
      {
        this.self.UpdateCurrentUnitInfo();
        if (this.self.ChangeUnit)
        {
          this.self.RefreshUnitInfo();
          this.self.RefreshPreviews();
        }
        else
        {
          this.self.RefreshJobs();
          this.self.SetActivePreview(this.self.mCurrentJobIndex);
        }
        if (!this.self.Initialized)
          return;
        GameParameter.UpdateAll(((Component) this.self).get_gameObject());
      }

      private void RebuildArtifactPreview()
      {
        this.self.RefreshArtifactInfo();
        this.self.SetActivePreviewArtifact(this.self.mCurrentPickupArtIndex);
        this.self.RefreshPreviews();
      }
    }

    private class State_WaitActionAnimation : State<GachaWindow>
    {
      private float mTimer;

      public override void Begin(GachaWindow self)
      {
        if (self.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
          self.mState.GotoState<GachaWindow.State_WaitPreviewUnit>();
        else
          this.mTimer = self.WaitTimeNextAction;
      }

      public override void Update(GachaWindow self)
      {
        this.mTimer -= Time.get_deltaTime();
        if ((double) this.mTimer > 0.0)
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCurrentPreview, (UnityEngine.Object) null))
        {
          self.mCurrentPreview.PlayAction = true;
          self.mState.GotoState<GachaWindow.State_WaitPreviewUnit>();
        }
        else
          Debug.LogError((object) "mCurrentPreviewがNullです");
      }
    }

    private class State_WaitPreviewUnit : State<GachaWindow>
    {
      private float mTimer;

      public override void Begin(GachaWindow self)
      {
        this.mTimer = self.mWaitSecondsChangeUnitJob;
      }

      public override void Update(GachaWindow self)
      {
        this.mTimer -= Time.get_deltaTime();
        if ((double) this.mTimer > 0.0)
          return;
        self.mState.GotoState<GachaWindow.State_CheckPreviewState>();
      }
    }

    private class State_CheckPreviewState : State<GachaWindow>
    {
      public override void Begin(GachaWindow self)
      {
        if (self.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
          self.ChangePreviewArtifact();
        else if (self.mSelectTab == GachaWindow.GachaTabCategory.RARE)
        {
          self.ChangeJob = false;
          self.ChangeUnit = false;
          if (!self.SelectJob())
          {
            self.ChangePreviewUnit();
            self.ChangeUnit = true;
          }
        }
        self.mState.GotoState<GachaWindow.State_RefreshPreview>();
      }
    }

    private class State_PauseState : State<GachaWindow>
    {
      public override void Begin(GachaWindow self)
      {
      }
    }

    private class State_ToGachaResult : State<GachaWindow>
    {
      public override void Begin(GachaWindow self)
      {
        FlowNode_GameObject.ActivateOutputLinks((Component) self, 200);
        self.mState.GotoState<GachaWindow.State_PauseState>();
      }
    }
  }
}
