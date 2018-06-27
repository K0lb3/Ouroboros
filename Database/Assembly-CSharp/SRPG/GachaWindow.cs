// Decompiled with JetBrains decompiler
// Type: SRPG.GachaWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
  [FlowNode.Pin(111, "チケット召喚を行う", FlowNode.PinTypes.Input, 111)]
  [FlowNode.Pin(112, "チケット召喚をキャンセル", FlowNode.PinTypes.Input, 112)]
  [FlowNode.Pin(110, "チケット召喚(単発)を選択", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(102, "Closed", FlowNode.PinTypes.Input, 102)]
  [FlowNode.Pin(101, "Opened", FlowNode.PinTypes.Input, 101)]
  [FlowNode.Pin(100, "ガチャ実行", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(10, "GachaConfirm", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "ExecFailed", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(999, "WebView起動", FlowNode.PinTypes.Output, 999)]
  [FlowNode.Pin(113, "リフレッシュ完了", FlowNode.PinTypes.Output, 113)]
  public class GachaWindow : MonoBehaviour, IFlowInterface
  {
    public static readonly int IN_GACHA_CONFIRM = 10;
    private static readonly string BackGroundTexturesPath = "Gachas/BGTables";
    private static readonly string TEXT_CONFIRM_GACHA_COIN = "sys.CONFIRM_GACHA_COIN";
    private static readonly string TEXT_COST_GACHA_COIN = "sys.GACHA_COST_COIN";
    private static readonly string TEXT_COST_GACHA_PAIDCOIN = "sys.GACHA_COST_PAIDCOIN";
    private const string TabSpritesPath = "Gachas/GachaTabSprites";
    private const int ConversionConstant = 65248;
    private const string BGIMAGE_DIR = "Gachas/";
    private const string DEFAULT_BGIMAGE_PATH = "GachaImages_Default";
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
    public SRPG_Button RareTab;
    public SRPG_Button NormalTab;
    public SRPG_Button TicketTab;
    public SRPG_Button ArtifactTab;
    public SRPG_Button TabTemplate;
    private List<SRPG_Button> mTabList;
    private GachaWindow.GachaTabCategory mSelectTab;
    public GameObject UnitInfoPanel;
    public GameObject ArtifactInfoPanel;
    public GameObject SingleBtn;
    public GameObject MultiBtn;
    public GameObject PaidBtn;
    public GameObject BonusPanel;
    public UnityEngine.UI.Text BonusItemName;
    public GameObject BonusPanelItem;
    public GameObject BonusPanelUnit;
    public GameObject BonusPanelArtifact;
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
    private readonly float WAIT_SWAP_BG;
    private float mWaitSwapBGTime;
    private bool mExistSwapBG;
    private int mEnableBGIndex;
    private List<GachaWindow.GachaTabCategory> mTabCategoryList;
    private readonly string GACHA_URL_PREFIX;
    private string mDetailURL;
    public string DESCRIPTION_URL;
    private string mDescriptionURL;
    [SerializeField]
    private Button DetailButton;
    [SerializeField]
    private Button DescriptionButton;
    [SerializeField]
    private GameObject GachaConfirmWindow;
    private GachaRequsetParam m_request;
    [SerializeField]
    private Transform RootObject;
    [SerializeField]
    private GameObject TicketGachaBadge;
    private bool IsRefreshingGachaBG;
    private string mCurrentIname;
    private string mCurrentInput;
    private int mCurrentCost;
    private string mCurrentType;
    private int mCurrentFree;
    private string mCurrentTicket;
    private int mCurrentNum;
    private bool mCurrentUseOneMore;
    private string mCurrentConfirmText;
    private GachaCategory mCurrentCategory;
    private GachaButton.GachaCostType mCurrentCostType;
    private int mCurrentStepIndex;
    private float mBGUnitImgAlphaStart;
    private float mBGUnitImgAlphaEnd;
    private float mBGUnitImgFadeTime;
    private float mBGUnitImgFadeTimeMax;

    public GachaWindow()
    {
      base.\u002Ector();
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

    public ArtifactData CurrentArtifact
    {
      get
      {
        return this.mCurrentArtifact;
      }
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
      stringBuilder.Append(Network.NewsHost);
      stringBuilder.Append(this.GACHA_URL_PREFIX);
      stringBuilder.Append(this.DESCRIPTION_URL);
      this.mDescriptionURL = stringBuilder.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DetailButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.DetailButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickDetail)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.DescriptionButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnClickDescription)));
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        this.Refresh();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 113);
      }
      else if (pinID == 2)
      {
        this.mClicked = false;
        this.mCurrentIname = this.mCurrentInput = this.mCurrentType = this.mCurrentTicket = string.Empty;
        this.mCurrentCost = this.mCurrentFree = this.mCurrentNum = 0;
      }
      else if (pinID == GachaWindow.IN_GACHA_CONFIRM)
        this.CheckPrevGachaRequest();
      else if (pinID == 111)
      {
        this.OnDecideForTicketSelect();
      }
      else
      {
        if (pinID != 112)
          return;
        this.OnCancel();
      }
    }

    private void Refresh()
    {
      this.SetupGachaList(MonoSingleton<GameManager>.Instance.Gachas);
      this.RefreshTicketButtonList();
      this.RefreshGachaState();
      this.SetupTabList();
      this.RefreshButtonPanel();
      this.RefreshTabState(this.mCurrentTabIndex);
      this.RefreshTabActive(true);
      this.RefreshGachaDetailSelectID(this.mSelectTab);
      this.RefreshDefaultPanel();
      this.mClicked = false;
    }

    private void RefreshTicketButtonList()
    {
      if (this.mSelectTab != GachaWindow.GachaTabCategory.TICKET || string.IsNullOrEmpty(this.mCurrentIname) || !this.mTicketButtonLists.ContainsKey(this.mCurrentIname))
        return;
      if (this.mGachaListTicket == null || this.mGachaListTicket.Count <= 0)
      {
        this.TicketNotListView.SetActive(true);
        this.mTicketButtonLists[this.mCurrentIname].SetActive(false);
      }
      else
      {
        if (this.mGachaListTicket.FindIndex((Predicate<GachaTopParamNew>) (x => x.iname == this.mCurrentIname)) >= 0)
          return;
        GameUtility.DestroyGameObject(this.mTicketButtonLists[this.mCurrentIname]);
        this.mTicketButtonLists.Remove(this.mCurrentIname);
        for (int index = 0; index < this.mGachaListTicket.Count; ++index)
        {
          if (this.mTicketButtonLists.ContainsKey(this.mGachaListTicket[index].iname))
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey334 listCAnonStorey334 = new GachaWindow.\u003CRefreshTicketButtonList\u003Ec__AnonStorey334();
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey334.\u003C\u003Ef__this = this;
            GameObject ticketButtonList = this.mTicketButtonLists[this.mGachaListTicket[index].iname];
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey334.ticketlistitem = (GachaTicketListItem) ticketButtonList.GetComponent<GachaTicketListItem>();
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listCAnonStorey334.ticketlistitem, (UnityEngine.Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              listCAnonStorey334.ticketlistitem.Refresh(this.mGachaListTicket[index], index);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method pointer
              listCAnonStorey334.ticketlistitem.SetGachaButtonEvent(new UnityAction((object) listCAnonStorey334, __methodptr(\u003C\u003Em__380)));
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
        {
          this.mGachaListTicket.Add(gachaTopParamNew);
        }
        else
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          GachaWindow.\u003CSetupGachaList\u003Ec__AnonStorey335 listCAnonStorey335 = new GachaWindow.\u003CSetupGachaList\u003Ec__AnonStorey335();
          // ISSUE: reference to a compiler-generated field
          listCAnonStorey335.group = gachaTopParamNew.group;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          if (this.mGachaListSpecials != null && !string.IsNullOrEmpty(listCAnonStorey335.group) && this.mGachaListSpecials.FindIndex(new Predicate<GachaWindow.GachaTopParamNewGroups>(listCAnonStorey335.\u003C\u003Em__381)) != -1)
          {
            // ISSUE: reference to a compiler-generated method
            this.mGachaListSpecials[this.mGachaListSpecials.FindIndex(new Predicate<GachaWindow.GachaTopParamNewGroups>(listCAnonStorey335.\u003C\u003Em__382))].lists.Add(gachaTopParamNew);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            this.mGachaListSpecials.Add(new GachaWindow.GachaTopParamNewGroups()
            {
              lists = {
                gachaTopParamNew
              },
              group = listCAnonStorey335.group,
              tab_image = gachaTopParamNew.asset_title
            });
          }
        }
      }
      if (this.mGachaListTicket == null || this.mGachaListTicket.Count <= 0)
        this.TicketGachaBadge.SetActive(false);
      else
        this.TicketGachaBadge.SetActive(true);
    }

    [DebuggerHidden]
    private IEnumerator LoadGachaBGTextures()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CLoadGachaBGTextures\u003Ec__IteratorF6() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator LoadGachaTabSprites()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GachaWindow.\u003CLoadGachaTabSprites\u003Ec__IteratorF7() { \u003C\u003Ef__this = this };
    }

    private void Start()
    {
      this.SetupGachaList(MonoSingleton<GameManager>.Instance.Gachas);
      this.ClearTabSprites();
      this.ClearBGSprites();
      this.StartCoroutine(this.LoadGachaTabSprites());
      this.StartCoroutine(this.LoadGachaBGTextures());
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
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          GachaWindow.\u003CSetupTabList\u003Ec__AnonStorey336 listCAnonStorey336 = new GachaWindow.\u003CSetupTabList\u003Ec__AnonStorey336();
          // ISSUE: reference to a compiler-generated field
          listCAnonStorey336.\u003C\u003Ef__this = this;
          // ISSUE: reference to a compiler-generated field
          listCAnonStorey336.tab = (SRPG_Button) UnityEngine.Object.Instantiate<SRPG_Button>((M0) this.TabTemplate);
          // ISSUE: reference to a compiler-generated field
          listCAnonStorey336.tab_index = index;
          // ISSUE: reference to a compiler-generated field
          ((Component) listCAnonStorey336.tab).get_transform().SetParent(((Component) this.TabTemplate).get_transform().get_parent(), false);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((UnityEvent) listCAnonStorey336.tab.get_onClick()).AddListener(new UnityAction((object) listCAnonStorey336, __methodptr(\u003C\u003Em__383)));
          // ISSUE: reference to a compiler-generated field
          ((UnityEngine.Object) listCAnonStorey336.tab).set_name("sp" + (object) index);
          // ISSUE: reference to a compiler-generated field
          Image component1 = (Image) ((Component) listCAnonStorey336.tab).get_gameObject().GetComponent<Image>();
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
          // ISSUE: reference to a compiler-generated field
          GachaTabListItem component2 = (GachaTabListItem) ((Component) listCAnonStorey336.tab).GetComponent<GachaTabListItem>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          {
            component2.EndAt = this.mGachaListSpecials[index].lists[0].GetTimerAt();
            component2.Disabled = this.mGachaListSpecials[index].lists[0].disabled;
            component2.GachaStartAt = this.mGachaListSpecials[index].lists[0].startat;
            component2.GachaEndtAt = this.mGachaListSpecials[index].lists[0].endat;
            // ISSUE: reference to a compiler-generated field
            component2.ListIndex = listCAnonStorey336.tab_index;
          }
          if (this.mGachaListSpecials != null && this.mGachaListSpecials.Count > 0)
          {
            // ISSUE: reference to a compiler-generated field
            DataSource.Bind<GachaTopParamNew>(((Component) listCAnonStorey336.tab).get_gameObject(), this.mGachaListSpecials[index].lists[0]);
          }
          // ISSUE: reference to a compiler-generated field
          this.mTabList.Add(listCAnonStorey336.tab);
          // ISSUE: reference to a compiler-generated field
          ((Component) listCAnonStorey336.tab).get_gameObject().SetActive(false);
          this.mTabCategoryList.Add(GachaWindow.GachaTabCategory.SPECIAL);
          // ISSUE: reference to a compiler-generated field
          ((Selectable) listCAnonStorey336.tab).set_interactable(!this.mGachaListSpecials[index].lists[0].disabled);
        }
      }
      if (this.mGachaListRare != null && this.mGachaListRare.Count > 0)
        DataSource.Bind<GachaTopParamNew>(((Component) this.RareTab).get_gameObject(), this.mGachaListRare[0]);
      this.mTabList.Add(this.RareTab);
      ((Component) this.RareTab).get_gameObject().SetActive(false);
      ((Component) this.RareTab).get_transform().SetAsLastSibling();
      // ISSUE: method pointer
      ((UnityEvent) this.RareTab.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CSetupTabList\u003Em__384)));
      this.mTabCategoryList.Add(GachaWindow.GachaTabCategory.RARE);
      if (this.mGachaListArtifact != null && this.mGachaListArtifact.Count > 0)
        DataSource.Bind<GachaTopParamNew>(((Component) this.ArtifactTab).get_gameObject(), this.mGachaListArtifact[0]);
      this.mTabList.Add(this.ArtifactTab);
      ((Component) this.ArtifactTab).get_gameObject().SetActive(false);
      ((Component) this.ArtifactTab).get_transform().SetAsLastSibling();
      // ISSUE: method pointer
      ((UnityEvent) this.ArtifactTab.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CSetupTabList\u003Em__385)));
      this.mTabCategoryList.Add(GachaWindow.GachaTabCategory.ARTIFACT);
      if (this.mGachaListTicket != null && this.mGachaListTicket.Count > 0)
        DataSource.Bind<GachaTopParamNew>(((Component) this.TicketTab).get_gameObject(), this.mGachaListTicket[0]);
      this.mTabList.Add(this.TicketTab);
      ((Component) this.TicketTab).get_gameObject().SetActive(false);
      ((Component) this.TicketTab).get_transform().SetAsLastSibling();
      // ISSUE: method pointer
      ((UnityEvent) this.TicketTab.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CSetupTabList\u003Em__386)));
      this.mTabCategoryList.Add(GachaWindow.GachaTabCategory.TICKET);
      if (this.mGachaListNormal != null && this.mGachaListNormal.Count > 0)
        DataSource.Bind<GachaTopParamNew>(((Component) this.NormalTab).get_gameObject(), this.mGachaListNormal[0]);
      this.mTabList.Add(this.NormalTab);
      ((Component) this.NormalTab).get_gameObject().SetActive(false);
      ((Component) this.NormalTab).get_transform().SetAsLastSibling();
      // ISSUE: method pointer
      ((UnityEvent) this.NormalTab.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CSetupTabList\u003Em__387)));
      this.mTabCategoryList.Add(GachaWindow.GachaTabCategory.NORMAL);
    }

    private void OnTabChange(SRPG_Button button, GachaWindow.GachaTabCategory category, int index = -1)
    {
      if (!this.TabChange(button, category, index))
        return;
      this.RefreshGachaDetailSelectID(this.mSelectTab);
      this.mState.GotoState<GachaWindow.State_CheckInitState>();
    }

    private void RefreshTabState(int index)
    {
      if (index < 0 || index >= this.mTabList.Count)
        index = 0;
      int num = index;
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
                num = component.ListIndex;
                break;
              }
              break;
            }
          }
        }
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
      this.mCurrentTabIndex = index;
      this.mSelectTab = this.mTabCategoryList[this.mCurrentTabIndex];
      this.mCurrentTabSPIndex = num;
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
            GachaWindow.\u003CSetTicketButtonList\u003Ec__AnonStorey337 listCAnonStorey337 = new GachaWindow.\u003CSetTicketButtonList\u003Ec__AnonStorey337();
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey337.\u003C\u003Ef__this = this;
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TicketButtonTemplate);
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey337.ticketlistitem = (GachaTicketListItem) gameObject.GetComponent<GachaTicketListItem>();
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) listCAnonStorey337.ticketlistitem, (UnityEngine.Object) null))
            {
              // ISSUE: reference to a compiler-generated field
              listCAnonStorey337.ticketlistitem.Refresh(this.mGachaListTicket[index], index);
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method pointer
              listCAnonStorey337.ticketlistitem.SetGachaButtonEvent(new UnityAction((object) listCAnonStorey337, __methodptr(\u003C\u003Em__388)));
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
      this.OnExecGacha(this.mGachaListTicket[index].iname, "ticket", 0, string.Empty, string.Empty, this.mGachaListTicket[index].ticket_iname, 1, true, this.mGachaListTicket[index].confirm, GachaButton.GachaCostType.TICKET, true, this.mGachaListTicket[index].step_index);
    }

    private void RefreshDefaultPanel()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UnitInfoPanel, (UnityEngine.Object) null))
        this.UnitInfoPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.RARE);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ArtifactInfoPanel, (UnityEngine.Object) null))
        this.ArtifactInfoPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
        this.BonusPanel.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.SPECIAL);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PaidBtn, (UnityEngine.Object) null))
        this.PaidBtn.SetActive(this.mSelectTab == GachaWindow.GachaTabCategory.RARE);
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
        this.Status.SetValues(fixed_status, scale_status);
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
      this.MultiBtn.SetActive(false);
      this.SingleBtn.SetActive(false);
      this.PaidBtn.SetActive(false);
      this.BonusPanel.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelItem, (UnityEngine.Object) null))
        this.BonusPanelItem.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelUnit, (UnityEngine.Object) null))
        this.BonusPanelUnit.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelArtifact, (UnityEngine.Object) null))
        this.BonusPanelArtifact.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusMsgPanel, (UnityEngine.Object) null))
        this.BonusMsgPanel.SetActive(false);
      for (int index = 0; index < currentGachaLists.Length; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        GachaWindow.\u003CRefreshButtonPanel\u003Ec__AnonStorey338 panelCAnonStorey338 = new GachaWindow.\u003CRefreshButtonPanel\u003Ec__AnonStorey338();
        // ISSUE: reference to a compiler-generated field
        panelCAnonStorey338.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        panelCAnonStorey338.gbtn = currentGachaLists[index].coin_p <= 0 ? (currentGachaLists[index].num <= 1 ? (GachaButton) this.SingleBtn.GetComponent<GachaButton>() : (GachaButton) this.MultiBtn.GetComponent<GachaButton>()) : (GachaButton) this.PaidBtn.GetComponent<GachaButton>();
        // ISSUE: reference to a compiler-generated field
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) panelCAnonStorey338.gbtn, (UnityEngine.Object) null))
        {
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.cost = 0;
          string str = currentGachaLists[index].btext;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.iname = currentGachaLists[index].iname;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.input = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.ticket = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.num = currentGachaLists[index].num;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.isConfirm = true;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.category = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.type = string.Empty;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.confirm = currentGachaLists[index].confirm;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.is_use_onemore = !currentGachaLists[index].limit && !currentGachaLists[index].step;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.cost_type = GachaButton.GachaCostType.NONE;
          GachaButton.GachaCategoryType gachaCategoryType;
          if (currentGachaLists[index].coin > 0)
          {
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.cost_type = GachaButton.GachaCostType.COIN;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.cost = currentGachaLists[index].coin;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.category = currentGachaLists[index].category;
            gachaCategoryType = !currentGachaLists[index].step ? GachaButton.GachaCategoryType.RARE : GachaButton.GachaCategoryType.STEPUP;
            if (string.IsNullOrEmpty(str))
            {
              // ISSUE: reference to a compiler-generated field
              str = LocalizedText.Get("sys.BTN_MULTI_GACHA", new object[1]
              {
                (object) panelCAnonStorey338.num.ToString()
              });
            }
          }
          else if (currentGachaLists[index].gold > 0)
          {
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.cost_type = GachaButton.GachaCostType.GOLD;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.cost = currentGachaLists[index].gold;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.category = currentGachaLists[index].category;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.isConfirm = false;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.type = "gold";
            gachaCategoryType = !currentGachaLists[index].step ? GachaButton.GachaCategoryType.NORMAL : GachaButton.GachaCategoryType.STEPUP;
            if (string.IsNullOrEmpty(str))
            {
              // ISSUE: reference to a compiler-generated field
              str = LocalizedText.Get("sys.BTN_MULTI_GACHA", new object[1]
              {
                (object) panelCAnonStorey338.num.ToString()
              });
            }
          }
          else if (currentGachaLists[index].coin_p > 0)
          {
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.cost_type = GachaButton.GachaCostType.COIN_P;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.cost = currentGachaLists[index].coin_p;
            gachaCategoryType = !currentGachaLists[index].step ? GachaButton.GachaCategoryType.RARE : GachaButton.GachaCategoryType.STEPUP;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.input = string.Empty;
            str = !string.IsNullOrEmpty(str) ? str : LocalizedText.Get("sys.BTN_CHARGE_GACHA");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.cost_type = GachaButton.GachaCostType.NONE;
            gachaCategoryType = GachaButton.GachaCategoryType.NONE;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.category = string.Empty;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.type = string.Empty;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.isConfirm = true;
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.confirm = currentGachaLists[index].confirm;
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (panelCAnonStorey338.cost_type == GachaButton.GachaCostType.COIN && panelCAnonStorey338.num <= 1 && (panelCAnonStorey338.category == "coin_1" && MonoSingleton<GameManager>.Instance.Player.CheckFreeGachaCoin()))
          {
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.isConfirm = false;
          }
          if (currentGachaLists[index].step)
            str = !string.IsNullOrEmpty(str) ? str : LocalizedText.Get("sys.BTN_STEPUP_GACHA");
          // ISSUE: reference to a compiler-generated field
          if (string.IsNullOrEmpty(panelCAnonStorey338.input))
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            panelCAnonStorey338.input = panelCAnonStorey338.num <= 1 ? "single" : "multiple";
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.Cost = panelCAnonStorey338.cost;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.ButtonText = str;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.Category = panelCAnonStorey338.category;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          int num = !string.IsNullOrEmpty(panelCAnonStorey338.ticket) ? MonoSingleton<GameManager>.Instance.Player.GetItemAmount(panelCAnonStorey338.ticket) : 0;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.TicketNum = num;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.StepIndex = currentGachaLists[index].step_index;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.StepMax = currentGachaLists[index].step_num;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.CostType = panelCAnonStorey338.cost_type;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.CategoryType = gachaCategoryType;
          // ISSUE: reference to a compiler-generated field
          panelCAnonStorey338.gbtn.UpdateTrigger = true;
          // ISSUE: reference to a compiler-generated field
          ((Component) panelCAnonStorey338.gbtn).get_gameObject().SetActive(true);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          panelCAnonStorey338.gbtn.SetGachaButtonEvent(new UnityAction((object) panelCAnonStorey338, __methodptr(\u003C\u003Em__389)));
        }
      }
      if (currentGachaLists.Length != 1)
        return;
      if (!string.IsNullOrEmpty(currentGachaLists[0].bonus_msg))
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusMsgText, (UnityEngine.Object) null))
          this.BonusMsgText.set_text(currentGachaLists[0].bonus_msg.Replace("<br>", "\n"));
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusMsgPanel, (UnityEngine.Object) null))
          return;
        this.BonusMsgPanel.SetActive(true);
      }
      else
      {
        List<GachaBonusParam> bonusItems = currentGachaLists[0].bonus_items;
        if (bonusItems == null || bonusItems.Count != 1)
          return;
        DataSource.Bind<ItemParam>(this.BonusPanel, (ItemParam) null);
        DataSource.Bind<UnitParam>(this.BonusPanel, (UnitParam) null);
        DataSource.Bind<ArtifactParam>(this.BonusPanel, (ArtifactParam) null);
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
              if (strArray[0] == "IT")
              {
                ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(current.iname);
                if (itemParam != null)
                {
                  StringBuilder stringBuilder = GameUtility.GetStringBuilder();
                  stringBuilder.Append(itemParam.name);
                  stringBuilder.Append(" × " + current.num.ToString());
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusItemName, (UnityEngine.Object) null))
                    this.BonusItemName.set_text(stringBuilder.ToString());
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
                    DataSource.Bind<ItemParam>(this.BonusPanel, itemParam);
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
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusItemName, (UnityEngine.Object) null))
                      this.BonusItemName.set_text(stringBuilder.ToString());
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
                      DataSource.Bind<UnitData>(this.BonusPanel, unitData);
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
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusItemName, (UnityEngine.Object) null))
                    this.BonusItemName.set_text(stringBuilder.ToString());
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanel, (UnityEngine.Object) null))
                    DataSource.Bind<ArtifactParam>(this.BonusPanel, artifactParam);
                }
                else
                  continue;
              }
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelItem, (UnityEngine.Object) null))
                this.BonusPanelItem.SetActive(strArray[0] == "IT");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelUnit, (UnityEngine.Object) null))
                this.BonusPanelUnit.SetActive(strArray[0] == "UN");
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BonusPanelArtifact, (UnityEngine.Object) null))
                this.BonusPanelArtifact.SetActive(strArray[0] == "AF");
            }
          }
        }
        GameParameter.UpdateAll(this.BonusPanel);
        this.BonusPanel.SetActive(true);
      }
    }

    private void RefreshGachaDetailSelectID(GachaWindow.GachaTabCategory category)
    {
      FlowNode_Variable.Set("SHARED_WEBWINDOW_TITLE", LocalizedText.Get("sys.TITLE_POPUP_GACHA_DETAIL"));
      GachaTopParamNew[] currentGachaLists = this.GetCurrentGachaLists(category);
      if (currentGachaLists == null || currentGachaLists.Length <= 0)
        return;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(Network.NewsHost);
      stringBuilder.Append(this.GACHA_URL_PREFIX);
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

    private string ConvertFullWidth(string half)
    {
      string str = (string) null;
      for (int index = 0; index < half.Length; ++index)
        str += (string) (object) (char) ((uint) half[index] + 65248U);
      return str;
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

    private UnitData CreateUnitData(UnitParam param)
    {
      UnitData unitData = new UnitData();
      Json_Unit json = new Json_Unit();
      json.iid = 1L;
      json.iname = param.iname;
      json.exp = 0;
      json.lv = 1;
      json.plus = 0;
      json.rare = 0;
      json.select = new Json_UnitSelectable();
      json.select.job = 0L;
      json.jobs = (Json_Job[]) null;
      json.abil = (Json_MasterAbility) null;
      json.abil = (Json_MasterAbility) null;
      if (param.jobsets != null && param.jobsets.Length > 0)
      {
        List<Json_Job> jsonJobList = new List<Json_Job>(param.jobsets.Length);
        int num = 1;
        for (int index = 0; index < param.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam((string) param.jobsets[index]);
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      GachaWindow.\u003CRefreshGachaBGImage\u003Ec__AnonStorey339 imageCAnonStorey339 = new GachaWindow.\u003CRefreshGachaBGImage\u003Ec__AnonStorey339();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mDefaultBG, (UnityEngine.Object) null) && this.mCacheBGImages != null && this.mCacheBGImages.Count > 0)
      {
        DebugUtility.Log("=== GachaWindow.cs->RefreshGachaBGImage:mDefaultBG Initalize");
        this.mDefaultBG = !this.mCacheBGImages.ContainsKey("GachaImages_Default") ? (Texture2D) null : this.mCacheBGImages["GachaImages_Default"];
      }
      // ISSUE: reference to a compiler-generated field
      imageCAnonStorey339.bg00 = this.mBGObjects == null || this.mBGObjects.Count < 2 ? this.BGRoot.FindChild("bg00") : this.mBGObjects[0];
      // ISSUE: reference to a compiler-generated field
      imageCAnonStorey339.bg01 = this.mBGObjects == null || this.mBGObjects.Count < 2 ? this.BGRoot.FindChild("bg01") : this.mBGObjects[1];
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) imageCAnonStorey339.bg00, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) imageCAnonStorey339.bg01, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():bg00 or bg01 is Null ===");
        this.IsRefreshingGachaBG = false;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        RawImage component1 = (RawImage) ((Component) imageCAnonStorey339.bg00).GetComponent<RawImage>();
        // ISSUE: reference to a compiler-generated field
        RawImage component2 = (RawImage) ((Component) imageCAnonStorey339.bg01).GetComponent<RawImage>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        {
          DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():bg00_raw or bg01_raw is Null ===");
          this.IsRefreshingGachaBG = false;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          CanvasGroup component3 = (CanvasGroup) ((Component) imageCAnonStorey339.bg00).get_gameObject().GetComponent<CanvasGroup>();
          // ISSUE: reference to a compiler-generated field
          CanvasGroup component4 = (CanvasGroup) ((Component) imageCAnonStorey339.bg01).get_gameObject().GetComponent<CanvasGroup>();
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) component3, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) component4, (UnityEngine.Object) null))
          {
            DebugUtility.LogError("=== GachaWindow.cs->RefreshGachaBGImage2():canvas00 or canvas01 is Null ===");
            this.IsRefreshingGachaBG = false;
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            if (this.mBGObjects.FindIndex(new Predicate<Transform>(imageCAnonStorey339.\u003C\u003Em__38A)) == -1)
            {
              // ISSUE: reference to a compiler-generated field
              this.mBGObjects.Add(imageCAnonStorey339.bg00);
            }
            // ISSUE: reference to a compiler-generated method
            if (this.mBGObjects.FindIndex(new Predicate<Transform>(imageCAnonStorey339.\u003C\u003Em__38B)) == -1)
            {
              // ISSUE: reference to a compiler-generated field
              this.mBGObjects.Add(imageCAnonStorey339.bg01);
            }
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
            this.mWaitSwapBGTime = this.WAIT_SWAP_BG;
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
            this.mWaitSwapBGTime = this.WAIT_SWAP_BG;
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
      return (IEnumerator) new GachaWindow.\u003CReloadPickupArtifactPreview\u003Ec__IteratorF8() { \u003C\u003Ef__this = this };
    }

    private void ReloadPickUpUnitView()
    {
      if (this.CurrentUnit == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mPreviewParent, (UnityEngine.Object) null))
        return;
      GameUtility.DestroyGameObjects<GachaUnitPreview>(this.mPreviewControllers);
      this.mPreviewControllers.Clear();
      this.mCurrentPreview = (GachaUnitPreview) null;
      for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
      {
        GachaUnitPreview gachaUnitPreview = (GachaUnitPreview) null;
        if (this.mCurrentUnit.Jobs[index] != null && this.mCurrentUnit.Jobs[index].Param != null)
        {
          GameObject gameObject = new GameObject("Preview", new System.Type[1]{ typeof (GachaUnitPreview) });
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
      GameObject gameObject1 = new GameObject("Preview", new System.Type[1]{ typeof (GachaUnitPreview) });
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

    private void CreateGachaConfirmWindow(string confirm, int cost, string ticket = "", GachaButton.GachaCostType type = GachaButton.GachaCostType.NONE, bool isShowCoinStatus = true)
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

    private void OnExecGacha(string iname, string input, int cost, string type, string category = "", string ticket = "", int num = 0, bool isConfirm = false, string confirm = "", GachaButton.GachaCostType cost_type = GachaButton.GachaCostType.NONE, bool isUseOneMore = false, int inGachaStepIndex = 0)
    {
      if (!this.Initialized || this.Clicked)
        return;
      this.mClicked = true;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      this.m_request = (GachaRequsetParam) null;
      DataSource.Bind<GachaRequsetParam>(((Component) this.RootObject.get_parent()).get_gameObject(), (GachaRequsetParam) null);
      int num1 = 0;
      if (input == "single" && category == "coin_1")
        num1 = !instance.Player.CheckFreeGachaCoin() ? 0 : 1;
      else if (input == "single" && category == "gold_1")
        num1 = !instance.Player.CheckFreeGachaGold() || instance.Player.CheckFreeGachaGoldMax() ? 0 : 1;
      this.ResetCurrentGachaData();
      this.mCurrentIname = iname;
      this.mCurrentInput = input;
      this.mCurrentCost = cost;
      this.mCurrentType = type;
      this.mCurrentFree = num1;
      this.mCurrentTicket = ticket;
      this.mCurrentNum = num;
      this.mCurrentCostType = cost_type;
      this.mCurrentStepIndex = inGachaStepIndex;
      this.mCurrentUseOneMore = isUseOneMore;
      if (!string.IsNullOrEmpty(category))
      {
        if (category.Contains("gold"))
          this.mCurrentCategory = GachaCategory.DEFAULT_NORMAL;
        else if (category.Contains("coin"))
          this.mCurrentCategory = GachaCategory.DEFAULT_RARE;
      }
      if (type == "gold" || num1 == 1)
      {
        this.OnDecide();
        this.SummonAnalyticsTracking(this.mCurrentCostType, this.mSelectTab, this.mCurrentIname, this.mCurrentFree, this.mCurrentCost, this.mCurrentNum, this.mCurrentStepIndex);
      }
      else if (input == nameof (ticket))
      {
        FlowNode_Variable.Set("USE_TICKET_INAME", ticket);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
      }
      else
      {
        string confirmText = this.GetConfirmText(cost, cost_type == GachaButton.GachaCostType.COIN_P, confirm);
        this.mCurrentConfirmText = confirmText;
        this.CreateGachaConfirmWindow(confirmText, cost, ticket, cost_type, true);
      }
    }

    private void OnDecide(GameObject dialog)
    {
      this.OnDecide();
    }

    private void OnDecideForTicketSelect()
    {
      this.mCurrentNum = MonoSingleton<GachaManager>.Instance.UseTicketNum;
      if (this.m_request != null)
        this.m_request.SetNum(this.mCurrentNum);
      this.OnDecide();
    }

    private void OnDecide()
    {
      this.mClicked = false;
      FlowNode_ExecGacha2 componentInParent = (FlowNode_ExecGacha2) ((Component) this).GetComponentInParent<FlowNode_ExecGacha2>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) componentInParent, (UnityEngine.Object) null))
        return;
      if (this.m_request == null)
        this.m_request = new GachaRequsetParam(this.mCurrentIname, this.mCurrentInput, this.mCurrentCost, this.mCurrentType, this.mCurrentFree, this.mCurrentTicket, this.mCurrentNum, this.mCurrentCostType == GachaButton.GachaCostType.COIN_P, this.mCurrentConfirmText, this.mCurrentUseOneMore, this.mCurrentCategory);
      DataSource.Bind<GachaRequsetParam>(((Component) this.RootObject.get_parent()).get_gameObject(), this.m_request);
      this.mState.GotoState<GachaWindow.State_PauseState>();
      this.SummonAnalyticsTracking(this.mCurrentCostType, this.mSelectTab, this.mCurrentIname, this.mCurrentFree, this.mCurrentCost, this.mCurrentNum, this.mCurrentStepIndex);
      componentInParent.OnExecGacha(this.mCurrentIname, this.mCurrentInput, this.mCurrentCost, this.mCurrentType, this.mCurrentFree, this.mCurrentTicket, this.mCurrentNum, this.mCurrentCostType == GachaButton.GachaCostType.COIN_P);
    }

    private void OnCancel(GameObject dialog)
    {
      this.OnCancel();
    }

    private void OnCancel()
    {
      this.mClicked = false;
      this.ResetCurrentGachaData();
    }

    private void ResetCurrentGachaData()
    {
      this.mCurrentIname = this.mCurrentInput = this.mCurrentType = this.mCurrentTicket = this.mCurrentConfirmText = (string) null;
      this.mCurrentCost = this.mCurrentFree = this.mCurrentNum = 0;
      this.mCurrentUseOneMore = false;
      this.mCurrentCategory = GachaCategory.NONE;
      this.mCurrentCostType = GachaButton.GachaCostType.NONE;
    }

    private RenderTexture CreateRenderTexture()
    {
      int num = Mathf.FloorToInt((float) Screen.get_height() * 0.8f);
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
      return (IEnumerator) new GachaWindow.\u003CRefreshUnitImage\u003Ec__IteratorF9() { \u003C\u003Ef__this = this };
    }

    public void OnClickReset()
    {
      this.mClicked = false;
    }

    private void CheckPrevGachaRequest()
    {
      if (this.m_request == null)
        DebugUtility.LogError("前回の召喚リクエストが存在しません");
      else if (this.m_request.IsTicketGacha)
      {
        FlowNode_Variable.Set("USE_TICKET_INAME", this.m_request.Ticket);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
      }
      else
      {
        int _free = this.m_request.Free;
        if (this.m_request.Input == "single")
        {
          PlayerData player = MonoSingleton<GameManager>.Instance.Player;
          if (this.m_request.Category == GachaCategory.DEFAULT_NORMAL)
            _free = !player.CheckFreeGachaGold() || player.CheckFreeGachaGoldMax() ? 0 : 1;
          else if (this.m_request.Category == GachaCategory.DEFAULT_RARE)
            _free = !player.CheckFreeGachaCoin() ? 0 : 1;
          this.m_request.SetFree(_free);
        }
        if (this.m_request.Type == "gold" || this.m_request.Free == 1)
        {
          this.OnDecide();
        }
        else
        {
          int cost = this.m_request.Cost;
          this.mCurrentFree = _free;
          this.CreateGachaConfirmWindow(this.GetConfirmText(cost, this.m_request.IsPaid, this.m_request.ConfirmText), cost, this.m_request.Ticket, GachaButton.GachaCostType.NONE, true);
        }
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
      return str2;
    }

    private void SummonAnalyticsTracking(GachaButton.GachaCostType inGachaCostType, GachaWindow.GachaTabCategory inGachaTabCategory, string inGachaGateName, int inIsFree, int inCurrencyCost, int inNumberOfThingsSummoned, int inGachaCurrentStep)
    {
      bool inIsFree1 = inIsFree == 1;
      int inSummonCost = !inIsFree1 ? inCurrencyCost : 0;
      AnalyticsManager.SetSummonTracking(inGachaCostType, inGachaTabCategory, inGachaGateName, inIsFree1, inSummonCost, inNumberOfThingsSummoned, inGachaCurrentStep);
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
      public override void Begin(GachaWindow self)
      {
      }

      public override void Update(GachaWindow self)
      {
        if (!self.mLoadGachaTabSprites || !self.mLoadBackGroundTexture)
          return;
        self.SetupTabList();
        SRPG_Button mTab = self.mTabList[0];
        GachaWindow.GachaTabCategory category = GachaWindow.GachaTabCategory.SPECIAL;
        int index1 = 0;
        if (self.mGachaListSpecials == null || self.mGachaListSpecials.Count <= 0)
        {
          category = GachaWindow.GachaTabCategory.RARE;
          index1 = 1;
        }
        else
        {
          for (int index2 = 0; index2 < self.mTabList.Count; ++index2)
          {
            if (((UnityEngine.Object) self.mTabList[index2]).get_name() == "sp0")
              mTab = self.mTabList[index2];
          }
        }
        if (MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(GameManager.BadgeTypes.RareGacha))
        {
          category = GachaWindow.GachaTabCategory.RARE;
          for (int index2 = 0; index2 < self.mTabList.Count; ++index2)
          {
            if (((UnityEngine.Object) self.mTabList[index2]).get_name() == "rare")
              mTab = self.mTabList[index2];
          }
        }
        else if (MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(GameManager.BadgeTypes.GoldGacha))
        {
          category = GachaWindow.GachaTabCategory.NORMAL;
          for (int index2 = 0; index2 < self.mTabList.Count; ++index2)
          {
            if (((UnityEngine.Object) self.mTabList[index2]).get_name() == "normal")
              mTab = self.mTabList[index2];
          }
        }
        self.TabChange(mTab, category, index1);
        self.RefreshGachaDetailSelectID(self.mSelectTab);
        self.mInitialized = true;
        self.mClicked = false;
        self.mState.GotoState<GachaWindow.State_CheckInitState>();
      }
    }

    private class State_CheckInitState : State<GachaWindow>
    {
      public override void Begin(GachaWindow self)
      {
      }

      public override void Update(GachaWindow self)
      {
        if (self.IsRefreshingGachaBG)
          return;
        self.RefreshTabActive(true);
        self.RefreshTabEnable(true);
        if (self.mSelectTab == GachaWindow.GachaTabCategory.RARE || self.mSelectTab == GachaWindow.GachaTabCategory.ARTIFACT)
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
        return (IEnumerator) new GachaWindow.State_RefreshPreview.\u003CRebuildPreviewController\u003Ec__IteratorFA() { self = self, \u003C\u0024\u003Eself = self, \u003C\u003Ef__this = this };
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
  }
}
