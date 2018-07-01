// Decompiled with JetBrains decompiler
// Type: SRPG.SceneBattle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Scene/Battle")]
  public class SceneBattle : Scene
  {
    public static readonly string QUEST_TEXTTABLE = "quest";
    private static readonly Color RenkeiChargeColor = new Color(0.0f, 0.5f, 1.5f, 0.5f);
    public static readonly string DefaultExitPoint = "Home";
    private static int MAX_MASK_BATTLE_UI = Enum.GetNames(typeof (SceneBattle.eMaskBattleUI)).Length;
    private List<BattleSceneSettings> mBattleScenes = new List<BattleSceneSettings>();
    private List<TacticsUnitController> mTacticsUnits = new List<TacticsUnitController>(20);
    private List<TacticsUnitController> mHotTargets = new List<TacticsUnitController>();
    private SceneBattle.CloseBattleUIWindow mCloseBattleUIWindow = new SceneBattle.CloseBattleUIWindow();
    public SceneBattle.QuestEndEvent OnQuestEnd = (SceneBattle.QuestEndEvent) (() => {});
    private List<TacticsUnitController> mUnitsInBattle = new List<TacticsUnitController>();
    private List<TacticsUnitController> mIgnoreShieldEffect = new List<TacticsUnitController>();
    private List<SceneBattle.EventRecvSkillUnit> mEventRecvSkillUnitLists = new List<SceneBattle.EventRecvSkillUnit>();
    private bool IsStoppedWeatherEffect = true;
    private readonly float MULTI_PLAY_INPUT_TIME_LIMIT_SEC = 10f;
    private readonly float MULTI_PLAY_INPUT_EXT_MOVE = 10f;
    private readonly float MULTI_PLAY_INPUT_EXT_SELECT = 10f;
    private readonly float SEND_TURN_SEC = 1f;
    private readonly int CPUBATTLE_PLAYER_NUM = 2;
    public readonly float SYNC_INTERVAL = 0.1f;
    public readonly float RESUME_REQUEST_INTERVAL = 0.1f;
    public readonly float RESUME_SUCCESS_INTERVAL = 0.1f;
    private int mPrevGridX = -1;
    private int mPrevGridY = -1;
    private EUnitDirection mPrevDir = EUnitDirection.Auto;
    private List<SceneBattle.MultiPlayRecvData> mRecvBattle = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvCheck = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvGoodJob = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvContinue = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvIgnoreMyDisconnect = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvResume = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvResumeRequest = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mAudienceDisconnect = new List<SceneBattle.MultiPlayRecvData>();
    private Dictionary<int, SceneBattle.MultiPlayRecvData> mRecvCheckData = new Dictionary<int, SceneBattle.MultiPlayRecvData>();
    private Dictionary<int, SceneBattle.MultiPlayRecvData> mRecvCheckMyData = new Dictionary<int, SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayCheck> mMultiPlayCheckList = new List<SceneBattle.MultiPlayCheck>();
    private List<int> mResumeAlreadyStartPlayer = new List<int>();
    private string mPhotonErrString = string.Empty;
    public List<SceneBattle.ReqCreateBreakObjUc> ReqCreateBreakObjUcLists = new List<SceneBattle.ReqCreateBreakObjUc>();
    private Dictionary<string, GameObject> mTrickMarkerDics = new Dictionary<string, GameObject>();
    private GemParticle[] mGemDrainEffect_Front = new GemParticle[32];
    private GemParticle[] mGemDrainEffect_Side = new GemParticle[32];
    private GemParticle[] mGemDrainEffect_Back = new GemParticle[32];
    private List<KeyValuePair<Unit, GameObject>> mJumpSpotEffects = new List<KeyValuePair<Unit, GameObject>>();
    private List<KeyValuePair<SceneBattle.PupupData, GameObject>> mPopups = new List<KeyValuePair<SceneBattle.PupupData, GameObject>>();
    private Dictionary<RectTransform, Vector3> mPopupPositionCache = new Dictionary<RectTransform, Vector3>();
    private List<RectTransform> mLayoutGauges = new List<RectTransform>(16);
    private Dictionary<RectTransform, Vector2> mGaugePositionCache = new Dictionary<RectTransform, Vector2>();
    private float mGaugeCollisionRadius = 50f;
    private float mGaugeYAspectRatio = 1f;
    private Dictionary<SkillParam, GameObject> mShieldEffects = new Dictionary<SkillParam, GameObject>();
    private const float RenkeiSceneFadeTime = 0.5f;
    private const float RenkeiSceneBrightness = 0.25f;
    private const float RenkeiChargeTime = 1f;
    private const float RenkeiAssistInterval = 0.5f;
    private const float MinLoadSkillTime = 0.75f;
    private const uint CHARGE_TARGET_ATTR_GRN = 1;
    private const uint CHARGE_TARGET_ATTR_RED = 2;
    private const float UPVIEW_CAMERADISTANCE_BASE = 5f;
    private const int GRIDLAYER_MOVABLE = 0;
    private const int GRIDLAYER_SHATEI = 1;
    private const int GRIDLAYER_KOUKA = 2;
    private const int GRIDLAYER_CHARGE_GRN = 3;
    private const int GRIDLAYER_CHARGE_RED = 4;
    private const string GRIDLAYER_MATPATH_CHARGE_GRN = "BG/GridMaterialGrn";
    private const string GRIDLAYER_MATPATH_CHARGE_RED = "BG/GridMaterialRed";
    public static SceneBattle Instance;
    private int mStartPlayerLevel;
    private BattleCore mBattle;
    private StateMachine<SceneBattle> mState;
    private Unit mSelectedBattleCommandTarget;
    private int mSelectedBattleCommandGridX;
    private int mSelectedBattleCommandGridY;
    private SkillData mSelectedBattleCommandSkill;
    private ItemData mSelectedBattleCommandItem;
    private GameObject mNavigation;
    private FlowNode_TutorialTrigger[] mTutorialTriggers;
    private bool mStartCalled;
    private bool mStartQuestCalled;
    private int mUnitStartCount;
    private int mUnitStartCountTotal;
    private FlowNode_BattleUI mBattleUI;
    private FlowNode_BattleUI_MultiPlay mBattleUI_MultiPlay;
    private bool mUISignal;
    private UnitCursor UnitCursor;
    private TacticsSceneSettings mTacticsSceneRoot;
    private BattleSceneSettings mBattleSceneRoot;
    private BattleSceneSettings mDefaultBattleScene;
    private DropGoldEffect mTreasureGoldTemplate;
    private TreasureBox mTreasureBoxTemplate;
    private DropItemEffect mTreasureDropTemplate;
    private GameObject mMapAddGemEffectTemplate;
    private Vector3 mCameraCenter;
    private QuestParam mCurrentQuest;
    private QuestStates mStartQuestState;
    private bool mDownloadTutorialAssets;
    private string mEventName;
    private int mNumHotTargets;
    private bool mIsRankingQuestNewScore;
    private bool mIsRankingQuestJoinReward;
    private int mRankingQuestNewRank;
    public string FirstClearItemId;
    public string EventPlayBgmID;
    private bool m_IsCardSendMail;
    private SceneBattle.eArenaSubmitMode mArenaSubmitMode;
    private int mCurrentUnitStartX;
    private int mCurrentUnitStartY;
    private List<JSON_MyPhotonPlayerParam> mAudiencePlayers;
    private bool mQuestStart;
    private SceneBattle.StateTransitionRequest mOnRequestStateChange;
    private Json_Unit mEditorSupportUnit;
    private bool mIsFirstPlay;
    private bool mIsFirstWin;
    private GridMap<float> mHeightMap;
    private float mWorldPosZ;
    private bool mMapReady;
    private bool mInterpAmbientLight;
    private bool mFirstTurn;
    private bool mIsShowCastSkill;
    private bool mAutoActivateGimmick;
    [NonSerialized]
    public int GoldCount;
    [NonSerialized]
    public int TreasureCount;
    public int DispTreasureCount;
    [NonSerialized]
    public SceneBattle.ExitRequests ExitRequest;
    private FlowNode_Network mReqSubmit;
    private bool mQuestResultSending;
    private bool mQuestResultSent;
    private int mFirstContact;
    private bool mRevertQuestNewIfRetire;
    private bool mIsForceEndQuest;
    private QuestResultData mSavedResult;
    private GameObject GoResultBg;
    private bool mSceneExiting;
    private int mPauseReqCount;
    private Unit mLastPlayerSideUseSkillUnit;
    private bool mWaitSkillSplashClose;
    private SkillSplash mSkillSplash;
    private Unit mCollaboMainUnit;
    private TacticsUnitController mCollaboTargetTuc;
    private bool mIsInstigatorSubUnit;
    private SkillSplashCollabo mSkillSplashCollabo;
    private bool isScreenMirroring;
    private GameObject GoWeatherEffect;
    private bool IsSetWeatherEffect;
    private SceneBattle.CameraMode m_CameraMode;
    private float m_CameraYaw;
    private bool m_NewCamera;
    private bool m_FullRotationCamera;
    private TargetCamera m_TargetCamera;
    private float m_CameraYawMin;
    private float m_CameraYawMax;
    private float m_DefaultCameraYawMin;
    private float m_DefaultCameraYawMax;
    private bool m_UpdateCamera;
    private bool m_BattleCamera;
    private bool m_AllowCameraRotation;
    private bool m_AllowCameraTranslation;
    private float m_CameraAngle;
    private Vector3 m_CameraPosition;
    private bool m_TargetCameraDistanceInterp;
    private float m_TargetCameraDistance;
    private bool m_TargetCameraPositionInterp;
    private Vector3 m_TargetCameraPosition;
    private bool m_TargetCameraAngleInterp;
    private float m_TargetCameraAngle;
    private float m_TargetCameraAngleStart;
    private float m_TargetCameraAngleTime;
    private float m_TargetCameraAngleTimeMax;
    public float mRestSyncInterval;
    public float mRestResumeRequestInterval;
    public float mRestResumeSuccessInterval;
    private List<SceneBattle.MultiPlayInput> mSendList;
    private float mSendTime;
    private bool mBeginMultiPlay;
    private bool mAudiencePause;
    private bool mAudienceSkip;
    private SceneBattle.MultiPlayRecvData mAudienceRetire;
    private SceneBattle.EDisconnectType mDisconnectType;
    private List<SceneBattle.MultiPlayer> mMultiPlayer;
    private List<SceneBattle.MultiPlayerUnit> mMultiPlayerUnit;
    private bool mResumeMultiPlay;
    private bool mResumeSend;
    private bool mResumeOnlyPlayer;
    private bool mResumeSuccess;
    private bool mMapViewMode;
    private bool mAlreadyEndBattle;
    private bool mCheater;
    private bool mAudienceForceEnd;
    private int mCurrentSendInputUnitID;
    private int mMultiPlaySendID;
    private bool mExecDisconnected;
    private bool mShowInputTimeLimit;
    private int mThinkingPlayerIndex;
    private bool mSetupGoodJob;
    private float mGoodJobWait;
    private DirectionArrow DirectionArrowTemplate;
    private GameObject TargetMarkerTemplate;
    private SceneBattle.MoveInput mMoveInput;
    private EventScript mEventScript;
    private EventScript.Sequence mEventSequence;
    private UnitGauge GaugeOverlayTemplate;
    private UnitGauge EnemyGaugeOverlayTemplate;
    private GameObject mContinueWindowRes;
    [NonSerialized]
    private Unit mSelectedTarget;
    [NonSerialized]
    public bool UIParam_TargetValid;
    [NonSerialized]
    public AbilityData UIParam_CurrentAbility;
    private bool mIsWaitingForBattleSignal;
    private bool mIsUnitChgActive;
    private SceneBattle.TargetSelectorParam mTargetSelectorParam;
    private EUnitDirection mSkillDirectionByKouka;
    private bool mIsBackSelectSkill;
    private uint mControlDisableMask;
    private TutorialButtonImage[] mTutorialButtonImages;
    private Quest_MoveUnit mUIMoveUnit;
    private GameObject mRenkeiAuraEffect;
    private GameObject mRenkeiAssistEffect;
    private GameObject mRenkeiChargeEffect;
    private GameObject mRenkeiHitEffect;
    private GameObject mSummonUnitEffect;
    private GameObject mUnitChangeEffect;
    private GameObject mWithdrawUnitEffect;
    private DamagePopup mDamageTemplate;
    private DamageDsgPopup mDamageDsgTemplate;
    private DamagePopup mHpHealTemplate;
    private DamagePopup mMpHealTemplate;
    private GameObject mAutoHealEffectTemplate;
    private GameObject mDrainHpEffectTemplate;
    private GameObject mDrainMpEffectTemplate;
    private GameObject mParamChangeEffectTemplate;
    private GameObject mConditionChangeEffectTemplate;
    private GameObject mChargeGrnTargetUnitEffect;
    private GameObject mChargeRedTargetUnitEffect;
    private GameObject mKnockBackEffect;
    private GameObject mTrickMarker;
    private GameObject mJumpFallEffect;
    private SkillNamePlate mSkillNamePlate;
    private SkillTargetWindow mSkillTargetWindow;
    private GameObject mJumpSpotEffectTemplate;
    private GameObject mGuardPopup;
    private GameObject mAbsorbPopup;
    private GameObject mCriticalPopup;
    private GameObject mBackstabPopup;
    private GameObject mMissPopup;
    private GameObject mPerfectAvoidPopup;
    private GameObject mWeakPopup;
    private GameObject mResistPopup;
    private GameObject mGutsPopup;
    private GameObject mUnitOwnerIndex;
    private GameObject mGemDrainHitEffect;
    private GameObject mTargetMarkerTemplate;
    private GameObject mAssistMarkerTemplate;
    private GameObject mBlockedGridMarker;
    private bool mDisplayBlockedGridMarker;
    private Grid mGridDisplayBlockedGridMarker;
    private GameObject[] mUnitMarkerTemplates;
    private List<GameObject>[] mUnitMarkers;
    private bool mLoadedAllUI;
    private TouchController mTouchController;
    private GameObject mVersusPlayerTarget;
    private GameObject mVersusEnemyTarget;
    private GameObject mGoWeatherAttach;
    private SceneBattle.GridClickEvent mOnGridClick;
    private SceneBattle.UnitClickEvent mOnUnitClick;
    private SceneBattle.UnitFocusEvent mOnUnitFocus;
    private TacticsUnitController mFocusedUnit;
    private TacticsUnitController mMapModeFocusedUnit;
    private SceneBattle.ScreenClickEvent mOnScreenClick;
    private float mLoadProgress_UI;
    private float mLoadProgress_Scene;
    private float mLoadProgress_Animation;
    private SceneBattle.FocusTargetEvent mOnFocusTarget;
    private SceneBattle.SelectTargetEvent mOnSelectTarget;
    private SceneBattle.CancelTargetSelectEvent mOnCancelTargetSelect;
    private bool mLoadingShieldEffects;

    public int UnitStartCount
    {
      get
      {
        return this.mUnitStartCount;
      }
    }

    public int UnitStartCountTotal
    {
      get
      {
        return this.mUnitStartCountTotal;
      }
    }

    public FlowNode_BattleUI BattleUI
    {
      get
      {
        return this.mBattleUI;
      }
    }

    public GameObject CurrentScene
    {
      get
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTacticsSceneRoot))
          return ((Component) this.mTacticsSceneRoot).get_gameObject();
        return (GameObject) null;
      }
    }

    public QuestParam CurrentQuest
    {
      get
      {
        return this.mCurrentQuest;
      }
    }

    public bool IsRankingQuestNewScore
    {
      get
      {
        return this.mIsRankingQuestNewScore;
      }
    }

    public bool IsRankingQuestJoinReward
    {
      get
      {
        return this.mIsRankingQuestJoinReward;
      }
    }

    public int RankingQuestNewRank
    {
      get
      {
        return this.mRankingQuestNewRank;
      }
    }

    public bool ValidateRankingQuestRank
    {
      get
      {
        return this.mRankingQuestNewRank > 0;
      }
    }

    public bool IsOrdealQuest
    {
      get
      {
        if (this.mCurrentQuest != null)
          return this.mCurrentQuest.type == QuestTypes.Ordeal;
        return false;
      }
    }

    public bool IsGetFirstClearItem
    {
      get
      {
        return !string.IsNullOrEmpty(this.FirstClearItemId);
      }
    }

    public bool IsCardSendMail
    {
      get
      {
        return this.m_IsCardSendMail;
      }
    }

    public BattleCore Battle
    {
      get
      {
        return this.mBattle;
      }
    }

    public List<JSON_MyPhotonPlayerParam> AudiencePlayer
    {
      get
      {
        return this.mAudiencePlayers;
      }
    }

    public bool QuestStart
    {
      get
      {
        return this.mQuestStart;
      }
      set
      {
        this.mQuestStart = value;
      }
    }

    private void ToggleBattleScene(bool visible, string sceneName = null)
    {
      if (visible)
      {
        BattleSceneSettings battleSceneSettings = (BattleSceneSettings) null;
        if (!string.IsNullOrEmpty(sceneName))
        {
          for (int index = 0; index < this.mBattleScenes.Count; ++index)
          {
            if (((UnityEngine.Object) this.mBattleScenes[index]).get_name() == sceneName)
            {
              battleSceneSettings = this.mBattleScenes[index];
              break;
            }
          }
        }
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) battleSceneSettings, (UnityEngine.Object) null))
          battleSceneSettings = this.mDefaultBattleScene;
        this.mBattleSceneRoot = battleSceneSettings;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleSceneRoot, (UnityEngine.Object) null))
        ((Component) this.mBattleSceneRoot).get_gameObject().SetActive(visible);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsSceneRoot, (UnityEngine.Object) null))
        ((Component) this.mTacticsSceneRoot).get_gameObject().SetActive(!visible);
      RenderPipeline component = (RenderPipeline) ((Component) Camera.get_main()).GetComponent<RenderPipeline>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.EnableVignette = !visible;
    }

    private void ToggleUserInterface(bool isEnabled)
    {
      string[] strArray = new string[5]
      {
        this.mBattleUI.QueueObjectID,
        this.mBattleUI.QuestStatusID,
        this.mBattleUI.MapHeightID,
        this.mBattleUI.ElementDiagram,
        this.mBattleUI.FukanCameraID
      };
      foreach (string name in strArray)
      {
        Canvas gameObject = GameObjectID.FindGameObject<Canvas>(name);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          ((Behaviour) gameObject).set_enabled(isEnabled);
      }
      BattleCameraControl gameObject1 = GameObjectID.FindGameObject<BattleCameraControl>(this.mBattleUI.CameraControllerID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        gameObject1.SetDisp(isEnabled);
      if (!this.Battle.IsMultiVersus)
        return;
      List<Unit> units = this.Battle.Units;
      if (units == null)
        return;
      for (int index = 0; index < units.Count; ++index)
      {
        TacticsUnitController unitController = this.FindUnitController(units[index]);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          unitController.ShowVersusCursor(isEnabled);
      }
    }

    public void EnableUserInterface()
    {
      this.ToggleUserInterface(true);
    }

    public void DisableUserInterface()
    {
      this.ToggleUserInterface(false);
    }

    private void Start()
    {
      MonoSingleton<GameManager>.Instance.Player.ClearItemFlags(ItemData.ItemFlags.NewItem | ItemData.ItemFlags.NewSkin);
      LocalizedText.LoadTable(SceneBattle.QUEST_TEXTTABLE, false);
      FadeController.Instance.ResetSceneFade(0.0f);
      this.InitCamera();
      int length = Enum.GetValues(typeof (SceneBattle.UnitMarkerTypes)).Length;
      this.mUnitMarkerTemplates = new GameObject[length];
      this.mUnitMarkers = new List<GameObject>[length];
      for (int index = 0; index < length; ++index)
        this.mUnitMarkers[index] = new List<GameObject>(12);
      this.mBattle = new BattleCore();
      this.mState = new StateMachine<SceneBattle>(this);
      SceneBattle.SimpleEvent.Clear();
      SceneBattle.SimpleEvent.Add(SceneBattle.TreasureEvent.GROUP, (SceneBattle.SimpleEvent.Interface) new SceneBattle.TreasureEvent());
      if (!this.mStartQuestCalled)
        this.GotoState<SceneBattle.State_ReqBtlComReq>();
      this.mStartCalled = true;
    }

    public bool IsPlayingArenaQuest
    {
      get
      {
        return this.mCurrentQuest.type == QuestTypes.Arena;
      }
    }

    public bool IsArenaRankupInfo()
    {
      if (GlobalVars.ResultArenaBattleResponse != null)
        return GlobalVars.ResultArenaBattleResponse.reward_info.IsReward();
      return false;
    }

    public int CalcArenaRankDelta()
    {
      if (GlobalVars.ResultArenaBattleResponse != null)
        return Mathf.Max(MonoSingleton<GameManager>.Instance.Player.ArenaRankBest - GlobalVars.ResultArenaBattleResponse.new_rank, 0);
      return 0;
    }

    public bool IsPlayingMultiQuest
    {
      get
      {
        return this.mCurrentQuest.IsMulti;
      }
    }

    public bool IsPlayingTower
    {
      get
      {
        return this.mCurrentQuest.type == QuestTypes.Tower;
      }
    }

    private void OnEnable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      SceneBattle.Instance = this;
    }

    private void OnDisable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) this))
        return;
      SceneBattle.Instance = (SceneBattle) null;
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoResultBg))
        UnityEngine.Object.Destroy((UnityEngine.Object) this.GoResultBg.get_gameObject());
      this.GoResultBg = (GameObject) null;
      LocalizedText.UnloadTable(SceneBattle.QUEST_TEXTTABLE);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNavigation, (UnityEngine.Object) null))
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mNavigation.get_gameObject());
        this.mNavigation = (GameObject) null;
        this.mTutorialTriggers = (FlowNode_TutorialTrigger[]) null;
      }
      this.CleanupGoodJob();
      this.DestroyCamera();
      this.DestroyUI(false);
      if (this.mBattle == null)
        return;
      this.mBattle.Release();
      this.mBattle = (BattleCore) null;
    }

    private void OnLoadTacticsScene(GameObject root)
    {
      TacticsSceneSettings component1 = (TacticsSceneSettings) root.GetComponent<TacticsSceneSettings>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnLoadTacticsScene));
      CriticalSection.Leave(CriticalSections.SceneChange);
      GameUtility.DeactivateActiveChildComponents<Camera>((Component) component1);
      this.mTacticsSceneRoot = component1;
      for (int index1 = 0; index1 < root.get_transform().get_childCount(); ++index1)
      {
        Transform child1 = root.get_transform().GetChild(index1);
        if (((UnityEngine.Object) child1).get_name() == "light")
        {
          for (int index2 = 0; index2 < child1.get_childCount(); ++index2)
          {
            Transform child2 = child1.GetChild(index2);
            if (((UnityEngine.Object) child2).get_name() != "Ambient Light")
              UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) child2).get_gameObject());
          }
          break;
        }
      }
      RenderPipeline component2 = (RenderPipeline) ((Component) Camera.get_main()).GetComponent<RenderPipeline>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
      {
        component2.BackgroundImage = (Texture) component1.BackgroundImage;
        component2.ScreenFilter = (Texture) component1.ScreenFilter;
      }
      if (!component1.OverrideCameraSettings)
        return;
      this.SetCameraYawRange(component1.CameraYawMin, component1.CameraYawMax);
    }

    public void UpdateUnitHP(TacticsUnitController controller)
    {
    }

    public void UpdateUnitMP(TacticsUnitController controller)
    {
    }

    private void FocusUnit(Unit unit)
    {
      if (unit == null)
        return;
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      GameSettings instance = GameSettings.Instance;
      ObjectAnimator.Get((Component) Camera.get_main()).AnimateTo(Vector3.op_Addition(((Component) unitController).get_transform().get_position(), ((Component) instance.Quest.UnitCamera).get_transform().get_position()), ((Component) instance.Quest.UnitCamera).get_transform().get_rotation(), 0.3f, ObjectAnimator.CurveType.EaseOut);
    }

    public void GotoNextState()
    {
      if (this.mOnRequestStateChange == null)
        return;
      this.mOnRequestStateChange(SceneBattle.StateTransitionTypes.Forward);
    }

    public void GotoPreviousState()
    {
      if (this.mOnRequestStateChange == null)
        return;
      this.mOnRequestStateChange(SceneBattle.StateTransitionTypes.Back);
    }

    public static Vector3 RaycastGround(Vector3 start)
    {
      RaycastHit raycastHit;
      if (Physics.Raycast(Vector3.op_Addition(start, Vector3.op_Multiply(Vector3.get_up(), 10f)), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
      {
        // ISSUE: explicit reference operation
        return ((RaycastHit) @raycastHit).get_point();
      }
      return start;
    }

    private TacticsUnitController FindClosestUnitController(Vector3 position, float maxDistance)
    {
      TacticsUnitController tacticsUnitController = (TacticsUnitController) null;
      float num1 = maxDistance;
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        TacticsUnitController mTacticsUnit = this.mTacticsUnits[index];
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mTacticsUnit, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) tacticsUnitController, (UnityEngine.Object) null) && this.CalcCoord(tacticsUnitController.CenterPosition) == this.CalcCoord(mTacticsUnit.CenterPosition) && !mTacticsUnit.Unit.IsGimmick)
          {
            tacticsUnitController = mTacticsUnit;
          }
          else
          {
            float num2 = GameUtility.CalcDistance2D(((Component) mTacticsUnit).get_transform().get_position(), position);
            if ((double) num2 < (double) num1)
            {
              tacticsUnitController = mTacticsUnit;
              num1 = num2;
            }
          }
        }
      }
      return tacticsUnitController;
    }

    public int GetDisplayHeight(Unit unit)
    {
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
      {
        IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
        Grid current = this.mBattle.CurrentMap[intVector2.x, intVector2.y];
        if (current != null)
          return current.height;
      }
      return 0;
    }

    public IntVector2 CalcCoord(Vector3 position)
    {
      return new IntVector2(Mathf.FloorToInt((float) position.x), Mathf.FloorToInt((float) (position.z - ((Component) this.mTacticsSceneRoot).get_transform().get_position().z)));
    }

    public Vector3 CalcGridCenter(Grid grid)
    {
      if (grid == null)
        return Vector3.get_zero();
      return new Vector3((float) grid.x + 0.5f, this.mHeightMap.get(grid.x, grid.y), (float) grid.y + 0.5f);
    }

    public Vector3 CalcGridCenter(int x, int y)
    {
      return this.CalcGridCenter(this.mBattle.CurrentMap[x, y]);
    }

    public EUnitDirection UnitDirectionFromPosition(Vector3 self, Vector3 target, SkillParam skill_param)
    {
      IntVector2 intVector2_1 = this.CalcCoord(self);
      IntVector2 intVector2_2 = this.CalcCoord(target);
      Grid current1 = this.mBattle.CurrentMap[intVector2_1.x, intVector2_1.y];
      Grid current2 = this.mBattle.CurrentMap[intVector2_2.x, intVector2_2.y];
      if (skill_param != null && skill_param.select_scope == ESelectType.LaserTwin)
        return this.mBattle.UnitDirectionFromGridLaserTwin(current1, current2);
      return this.mBattle.UnitDirectionFromGrid(current1, current2);
    }

    public void RemoveLog()
    {
      if (this.mBattle.Logs.Peek != null)
        DebugUtility.Log("RemoveLog(): " + (object) this.mBattle.Logs.Peek.GetType());
      this.Battle.Logs.RemoveLog();
    }

    private void Update()
    {
      this.UpdateMultiPlayer();
      this.UpdateAudiencePlayer();
      if (this.mState != null && this.Battle.ResumeState != BattleCore.RESUME_STATE.WAIT)
        this.mState.Update();
      this.UpdateCameraControl(false);
      MyEncrypt.EncryptCount = 0;
      MyEncrypt.DecryptCount = 0;
      if (this.mDownloadTutorialAssets && GameUtility.Config_UseAssetBundles.Value && (MonoSingleton<GameManager>.Instance.HasTutorialDLAssets && AssetDownloader.isDone))
        MonoSingleton<GameManager>.Instance.PartialDownloadTutorialAssets();
      if (this.CurrentQuest == null || this.CurrentQuest.type == QuestTypes.Tutorial)
        return;
      GlobalVars.IsTutorialEnd = true;
    }

    private void StartDownloadNextQuestAsync()
    {
      this.StartCoroutine(this.DownloadNextQuestAsync());
    }

    [DebuggerHidden]
    private IEnumerator DownloadNextQuestAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CDownloadNextQuestAsync\u003Ec__Iterator2D()
      {
        \u003C\u003Ef__this = this
      };
    }

    public void GotoState<StateType>() where StateType : State<SceneBattle>, new()
    {
      this.mState.GotoState<StateType>();
    }

    public bool IsInState<StateType>() where StateType : State<SceneBattle>
    {
      return this.mState.IsInState<StateType>();
    }

    [DebuggerHidden]
    private IEnumerator DownloadQuestAsync(QuestParam quest)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CDownloadQuestAsync\u003Ec__Iterator2E()
      {
        quest = quest,
        \u003C\u0024\u003Equest = quest,
        \u003C\u003Ef__this = this
      };
    }

    public void StartQuest(string questID, BattleCore.Json_Battle json)
    {
      this.mStartQuestCalled = true;
      this.StartCoroutine(this.StartQuestAsync(questID, json));
    }

    public bool IsFirstPlay
    {
      get
      {
        return this.mIsFirstPlay;
      }
    }

    public bool IsFirstWin
    {
      get
      {
        return this.mIsFirstWin;
      }
    }

    public bool IsPlayLastDemo
    {
      get
      {
        if (this.mBattle != null && this.mCurrentQuest != null && (this.mBattle.GetQuestResult() == BattleCore.QuestResult.Win && !string.IsNullOrEmpty(this.mCurrentQuest.event_clear)))
          return this.mIsFirstWin;
        return false;
      }
    }

    [DebuggerHidden]
    private IEnumerator StartQuestAsync(string questID, BattleCore.Json_Battle jsonBtl)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CStartQuestAsync\u003Ec__Iterator2F()
      {
        questID = questID,
        jsonBtl = jsonBtl,
        \u003C\u0024\u003EquestID = questID,
        \u003C\u0024\u003EjsonBtl = jsonBtl,
        \u003C\u003Ef__this = this
      };
    }

    public void PlayBGM()
    {
      if (!string.IsNullOrEmpty(this.EventPlayBgmID))
        MonoSingleton<MySound>.Instance.PlayBGM(this.EventPlayBgmID, (string) null, EventAction.IsUnManagedAssets(this.EventPlayBgmID, true));
      else
        MonoSingleton<MySound>.Instance.PlayBGM(this.mBattle.CurrentMap.BGMName, (string) null, false);
    }

    public void StopBGM()
    {
      MonoSingleton<MySound>.Instance.PlayBGM((string) null, (string) null, false);
    }

    private void UpdateBGM()
    {
      if (string.IsNullOrEmpty(this.mBattle.CurrentMap.BGMName))
        this.StopBGM();
      else
        this.PlayBGM();
    }

    private void GotoQuestStart()
    {
      this.mRevertQuestNewIfRetire = false;
      this.GotoState_WaitSignal<SceneBattle.State_QuestStartV2>();
    }

    private void CalcGridHeights()
    {
      this.mHeightMap = new GridMap<float>(this.mBattle.CurrentMap.Width, this.mBattle.CurrentMap.Height);
      for (int y = 0; y < this.mBattle.CurrentMap.Height; ++y)
      {
        for (int x = 0; x < this.mBattle.CurrentMap.Width; ++x)
          this.mHeightMap.set(x, y, this.CalcHeight((float) x + 0.5f, (float) y + 0.5f));
      }
    }

    private void BeginLoadMapAsync()
    {
      this.mMapReady = false;
      this.StartCoroutine(this.LoadMapAsync());
    }

    [DebuggerHidden]
    private IEnumerator LoadMapAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CLoadMapAsync\u003Ec__Iterator30()
      {
        \u003C\u003Ef__this = this
      };
    }

    private bool IsUnitLoading
    {
      get
      {
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          if (this.mTacticsUnits[index].IsLoading)
            return true;
        }
        return false;
      }
    }

    private void ResetUnitPosition(TacticsUnitController controller)
    {
      Vector3 vector3 = this.CalcGridCenter(this.mBattle.GetUnitGridPosition(controller.Unit));
      ((Component) controller).get_transform().set_position(vector3);
      controller.ResetRotation();
    }

    private void GotoMapStart()
    {
      this.ArenaActionCountEnable(this.IsPlayingArenaQuest || this.Battle.IsMultiVersus);
      this.RankingQuestActionCountEnable(this.Battle.IsRankingQuest);
      if (this.IsPlayingArenaQuest)
      {
        this.GotoState<SceneBattle.State_ArenaCalc>();
      }
      else
      {
        if (this.Battle.IsMultiVersus)
          this.ArenaActionCountSet(this.Battle.RemainVersusTurnCount);
        if (this.Battle.IsRankingQuest)
          this.RankingQuestActionCountSet((uint) this.Battle.ActionCount);
        this.Battle.MapStart();
        if (this.Battle.CheckEnableSuspendStart() && this.Battle.LoadSuspendData())
          this.Battle.RelinkTrickGimmickEvents();
        bool pc_enable = false;
        bool ec_enable = false;
        int num1 = 0;
        int num2 = 0;
        BattleMap currentMap = this.Battle.CurrentMap;
        if (currentMap != null)
        {
          pc_enable = this.Battle.CheckEnableRemainingActionCount(currentMap.WinMonitorCondition);
          num1 = this.Battle.GetRemainingActionCount(currentMap.WinMonitorCondition);
          ec_enable = this.Battle.CheckEnableRemainingActionCount(currentMap.LoseMonitorCondition);
          num2 = this.Battle.GetRemainingActionCount(currentMap.LoseMonitorCondition);
          if (num1 == -1)
            pc_enable = false;
          if (num2 == -1)
            ec_enable = false;
        }
        this.RemainingActionCountEnable(pc_enable, ec_enable);
        this.RemainingActionCountSet((uint) num1, (uint) num2);
        this.GotoState<SceneBattle.State_LoadMapV2>();
      }
    }

    private Unit RayPickUnit(Vector2 pos)
    {
      Camera main = Camera.get_main();
      Vector3 position = ((Component) main).get_transform().get_position();
      float num1 = (float) (Screen.get_width() / this.mBattle.CurrentMap.Width) * 0.5f;
      Unit unit1 = (Unit) null;
      float num2 = float.MaxValue;
      for (int index = 0; index < this.mBattle.Units.Count; ++index)
      {
        Unit unit2 = this.mBattle.Units[index];
        TacticsUnitController unitController = this.FindUnitController(unit2);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          Vector3 vector3 = GameUtility.DeformPosition(((Component) unitController).get_transform().get_position(), (float) position.z);
          Vector3 screenPoint = main.WorldToScreenPoint(vector3);
          Vector2 vector2 = pos;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local1 = @vector2;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local1).x = (^local1).x - screenPoint.x;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local2 = @vector2;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local2).y = (^local2).y - screenPoint.y;
          // ISSUE: explicit reference operation
          if ((double) ((Vector2) @vector2).get_magnitude() <= (double) num1 && screenPoint.z < (double) num2)
          {
            unit1 = unit2;
            num2 = (float) screenPoint.z;
          }
        }
      }
      return unit1;
    }

    private void ShowUnitCursor(Unit unit)
    {
      if (unit == null)
        return;
      GameSettings instance = GameSettings.Instance;
      this.ShowUnitCursor(unit, unit.Side != EUnitSide.Player ? instance.Colors.Enemy : instance.Colors.Player);
    }

    private void ShowUnitCursor(Unit unit, Color color)
    {
      if (unit == null)
        return;
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      unitController.ShowCursor(this.UnitCursor, color);
    }

    private void HideUnitCursor(Unit unit)
    {
      if (unit == null)
        return;
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      unitController.HideCursor(false);
    }

    private void HideUnitCursors(bool immediate)
    {
      for (int index = this.mTacticsUnits.Count - 1; index >= 0; --index)
        this.mTacticsUnits[index].HideCursor(immediate);
    }

    private void CloseBattleUI()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI, (UnityEngine.Object) null))
      {
        this.mBattleUI.OnInputTimeLimit();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.TargetMain, (UnityEngine.Object) null))
          this.mBattleUI.TargetMain.ForceClose(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.TargetSub, (UnityEngine.Object) null))
          this.mBattleUI.TargetSub.ForceClose(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          this.mBattleUI.TargetObjectSub.ForceClose(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          this.mBattleUI.TargetTrickSub.ForceClose(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          this.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.ItemWindow, (UnityEngine.Object) null))
          this.mBattleUI.ItemWindow.OnSelectItem = (BattleInventory.SelectEvent) null;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.SkillWindow, (UnityEngine.Object) null))
          this.mBattleUI.SkillWindow.OnSelectSkill = (UnitAbilitySkillList.SelectSkillEvent) null;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillTargetWindow, (UnityEngine.Object) null))
          this.mSkillTargetWindow.ForceHide();
        if (this.mTacticsUnits != null)
        {
          for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          {
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTacticsUnits[index], (UnityEngine.Object) null))
              this.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          }
        }
      }
      this.mOnUnitFocus = (SceneBattle.UnitFocusEvent) null;
      this.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
      this.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      this.mOnGridClick = (SceneBattle.GridClickEvent) null;
      this.mOnScreenClick = (SceneBattle.ScreenClickEvent) null;
      this.HideUnitMarkers(false);
      this.HideGrid();
      if (this.mCloseBattleUIWindow == null)
        return;
      this.mCloseBattleUIWindow.CloseAll();
    }

    private void HighlightTargetGrid(Unit current, int range, int difffloor)
    {
      if (range <= 0)
        return;
      BattleMap currentMap = this.mBattle.CurrentMap;
      Grid grid1 = currentMap[current.x, current.y];
      GridMap<bool> grid2 = new GridMap<bool>(currentMap.Width, currentMap.Height);
      for (int y = 0; y < grid2.h; ++y)
      {
        for (int x = 0; x < grid2.w; ++x)
        {
          if (x == current.x || y == current.y)
          {
            int num = Mathf.Max(Mathf.Abs(x - current.x), Mathf.Abs(y - current.y));
            if (num > 0 && num <= range)
            {
              if (difffloor != 0)
              {
                Grid grid3 = currentMap[x, y];
                if (grid3 == null || Mathf.Abs(grid1.height - grid3.height) > difffloor)
                  continue;
              }
              grid2.set(x, y, true);
            }
          }
        }
      }
      this.mTacticsSceneRoot.ShowGridLayer(0, grid2, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea), false);
    }

    private void InternalShowCastSkill()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      List<Unit> unitList = new List<Unit>();
      using (List<BattleCore.OrderData>.Enumerator enumerator = this.mBattle.Order.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          BattleCore.OrderData current = enumerator.Current;
          if (current.Unit.Side == EUnitSide.Player && current.IsCastSkill && (current.Unit != null && current.Unit.CastSkill != null))
            unitList.Add(current.Unit);
        }
      }
      if (this.mBattle.CurrentUnit != null && this.mBattle.CurrentUnit.CastSkill != null && (!unitList.Contains(this.mBattle.CurrentUnit) && this.mBattle.CurrentUnit.Side == EUnitSide.Player))
        unitList.Add(this.mBattle.CurrentUnit);
      if (unitList.Count <= 0)
        return;
      List<SceneBattle.ChargeTarget> chargeTargetList = new List<SceneBattle.ChargeTarget>();
      Color32 src1 = Color32.op_Implicit(GameSettings.Instance.Colors.ChargeAreaGrn);
      Color32 src2 = Color32.op_Implicit(GameSettings.Instance.Colors.ChargeAreaRed);
      GridMap<Color32> grid1 = new GridMap<Color32>(this.mBattle.CurrentMap.Width, this.mBattle.CurrentMap.Height);
      GridMap<Color32> grid2 = new GridMap<Color32>(this.mBattle.CurrentMap.Width, this.mBattle.CurrentMap.Height);
      using (List<Unit>.Enumerator enumerator = unitList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Unit unit = enumerator.Current;
          bool flag = false;
          if (unit.CastSkill.IsAdvantage())
            flag = true;
          int num1 = 0;
          int num2 = 0;
          if (unit.UnitTarget != null)
          {
            num1 = unit.UnitTarget.x;
            num2 = unit.UnitTarget.y;
            if (unit.UnitTarget == this.mBattle.CurrentUnit)
            {
              IntVector2 intVector2 = this.CalcCoord(this.FindUnitController(unit.UnitTarget).CenterPosition);
              num1 = intVector2.x;
              num2 = intVector2.y;
            }
          }
          else if (unit.GridTarget != null)
          {
            num1 = unit.GridTarget.x;
            num2 = unit.GridTarget.y;
          }
          GridMap<bool> scopeGridMap = this.mBattle.CreateScopeGridMap(unit, unit.x, unit.y, num1, num2, unit.CastSkill);
          if (SkillParam.IsTypeLaser(unit.CastSkill.SkillParam.select_scope) && unit.UnitTarget != null)
            scopeGridMap.set(num1, num2, true);
          if (unit.CastSkill.TeleportType == eTeleportType.Only || unit.CastSkill.TeleportType == eTeleportType.AfterSkill)
            scopeGridMap.set(num1, num2, false);
          for (int x = 0; x < scopeGridMap.w; ++x)
          {
            if (x < grid1.w)
            {
              for (int y = 0; y < scopeGridMap.h; ++y)
              {
                if (y < grid1.h && scopeGridMap.get(x, y))
                {
                  if (unit.UnitTarget != null && x == num1 && y == num2)
                  {
                    SceneBattle.ChargeTarget chargeTarget = chargeTargetList.Find((Predicate<SceneBattle.ChargeTarget>) (ctl => ctl.mUnit == unit.UnitTarget));
                    if (chargeTarget != null)
                      chargeTarget.AddAttr(!flag ? 2U : 1U);
                    else
                      chargeTargetList.Add(new SceneBattle.ChargeTarget(unit.UnitTarget, !flag ? 2U : 1U));
                  }
                  if (flag)
                    grid1.set(x, y, src1);
                  else
                    grid2.set(x, y, src2);
                }
              }
            }
          }
        }
      }
      this.mTacticsSceneRoot.ShowGridLayer(3, grid1, "BG/GridMaterialGrn");
      this.mTacticsSceneRoot.ShowGridLayer(4, grid2, "BG/GridMaterialRed");
      using (List<SceneBattle.ChargeTarget>.Enumerator enumerator = chargeTargetList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SceneBattle.ChargeTarget current = enumerator.Current;
          TacticsUnitController unitController = this.FindUnitController(current.mUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            if (((int) current.mAttr & 1) != 0)
              unitController.EnableChargeTargetUnit(this.mChargeGrnTargetUnitEffect, true);
            if (((int) current.mAttr & 2) != 0)
              unitController.EnableChargeTargetUnit(this.mChargeRedTargetUnitEffect, false);
          }
        }
      }
    }

    private void InternalHideCastSkill(bool is_only_target_unit = false)
    {
      if (!is_only_target_unit)
      {
        this.mTacticsSceneRoot.HideGridLayer(3);
        this.mTacticsSceneRoot.HideGridLayer(4);
      }
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        this.mTacticsUnits[index].DisableChargeTargetUnit();
    }

    private void ShowCastSkill()
    {
      this.mIsShowCastSkill = true;
      if (!GameUtility.Config_ChargeDisp.Value)
        return;
      this.InternalShowCastSkill();
    }

    private void HideCastSkill(bool is_only_target_unit = false)
    {
      this.mIsShowCastSkill = false;
      if (!GameUtility.Config_ChargeDisp.Value)
        return;
      this.InternalHideCastSkill(is_only_target_unit);
    }

    public void ReflectCastSkill(bool is_disp)
    {
      if (is_disp)
      {
        if (!this.mIsShowCastSkill)
          return;
        this.InternalShowCastSkill();
      }
      else
      {
        if (!this.mIsShowCastSkill)
          return;
        this.InternalHideCastSkill(false);
      }
    }

    private void GotoInputMovement()
    {
      this.mBattleUI.OnInputMoveStart();
      this.GotoState_WaitSignal<SceneBattle.State_MapMoveSelect_Stick>();
    }

    private Unit FindTarget(Unit current, SkillData skill, GridMap<bool> map, EUnitSide targetSide)
    {
      TacticsUnitController unitController = this.FindUnitController(current);
      IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
      Vector3 forward = ((Component) unitController).get_transform().get_forward();
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector((float) forward.x, (float) forward.z);
      float num1 = float.MinValue;
      Unit unit1 = (Unit) null;
      for (int index = this.mBattle.Units.Count - 1; index >= 0; --index)
      {
        Unit unit2 = this.mBattle.Units[index];
        if ((!unit2.IsGimmick || unit2.IsBreakObj) && (!unit2.IsDead && unit2.IsEntry) && (!unit2.IsSub && (unit2.Side == targetSide || this.Battle.IsTargetBreakUnit(current, unit2, skill))) && (map.isValid(unit2.x, unit2.y) && map.get(unit2.x, unit2.y)))
        {
          int x = unit2.x;
          int y = unit2.y;
          Vector2 vector2_2;
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_2).\u002Ector((float) (x - current.x), (float) (y - current.y));
          // ISSUE: explicit reference operation
          float magnitude = ((Vector2) @vector2_2).get_magnitude();
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_2).Normalize();
          float num2 = Vector2.Dot(vector2_1, vector2_2) - 1f - magnitude;
          if ((double) num1 < (double) num2)
          {
            BattleCore.CommandResult commandResult = this.mBattle.GetCommandResult(current, intVector2.x, intVector2.y, unit2.x, unit2.y, skill);
            if (commandResult != null && commandResult.targets != null && commandResult.targets.Count > 0)
            {
              unit1 = unit2;
              num1 = num2;
            }
          }
        }
      }
      return unit1 ?? (Unit) null;
    }

    private void GotoSelectAttackTarget()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
      SkillData attackSkill = this.mBattle.CurrentUnit.GetAttackSkill();
      int x1 = currentUnit.x;
      int y1 = currentUnit.y;
      currentUnit.x = intVector2.x;
      currentUnit.y = intVector2.y;
      List<Unit> attackTargetsAi = this.mBattle.CreateAttackTargetsAI(currentUnit, attackSkill, false);
      Vector3 forward = ((Component) unitController).get_transform().get_forward();
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector((float) forward.x, (float) forward.z);
      float num1 = float.MinValue;
      Unit defaultTarget = (Unit) null;
      for (int index = 0; index < attackTargetsAi.Count; ++index)
      {
        if (currentUnit != attackTargetsAi[index])
        {
          int x2 = attackTargetsAi[index].x;
          int y2 = attackTargetsAi[index].y;
          Vector2 vector2_2;
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_2).\u002Ector((float) (x2 - currentUnit.x), (float) (y2 - currentUnit.y));
          // ISSUE: explicit reference operation
          float magnitude = ((Vector2) @vector2_2).get_magnitude();
          // ISSUE: explicit reference operation
          ((Vector2) @vector2_2).Normalize();
          float num2 = Vector2.Dot(vector2_1, vector2_2) - 1f - magnitude;
          if ((double) num1 < (double) num2)
          {
            defaultTarget = attackTargetsAi[index];
            num1 = num2;
          }
        }
      }
      currentUnit.x = x1;
      currentUnit.y = y1;
      this.GotoSelectTarget(attackSkill, new SceneBattle.SelectTargetCallback(this.GotoMapCommand), new SceneBattle.SelectTargetPositionWithSkill(this.OnSelectAttackTarget), defaultTarget, true);
    }

    private bool ApplyUnitMovement(bool test = false)
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && !currentUnit.IsUnitFlag(EUnitFlag.Moved))
      {
        IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
        EUnitDirection direction = unitController.CalcUnitDirectionFromRotation();
        if (currentUnit.x != intVector2.x || currentUnit.y != intVector2.y)
        {
          Grid current = this.mBattle.CurrentMap[intVector2.x, intVector2.y];
          if (test)
            return this.mBattle.CheckMove(currentUnit, current);
          if (!this.Battle.Move(this.Battle.CurrentUnit, current, direction, true, false))
          {
            DebugUtility.LogError("移動に失敗");
            return false;
          }
          this.SendInputGridXY(this.Battle.CurrentUnit, intVector2.x, intVector2.y, currentUnit.Direction, true);
          this.SendInputMoveEnd(this.Battle.CurrentUnit, false);
        }
        else if (currentUnit.Direction != direction)
          this.Battle.CurrentUnit.Direction = direction;
      }
      return true;
    }

    private void OnSelectAttackTarget(int x, int y, SkillData skill, bool bUnitLockTarget)
    {
      this.HideAllHPGauges();
      this.HideAllUnitOwnerIndex();
      Unit currentUnit = this.mBattle.CurrentUnit;
      if (!this.ApplyUnitMovement(false))
        return;
      if (this.Battle.IsMultiPlay)
        this.SendInputEntryBattle(EBattleCommand.Attack, this.Battle.CurrentUnit, this.mSelectedTarget, (SkillData) null, (ItemData) null, x, y, bUnitLockTarget);
      if (!this.Battle.UseSkill(currentUnit, x, y, skill, bUnitLockTarget, 0, 0, false))
        DebugUtility.LogError("failed use skill. Unit:" + currentUnit.UnitName + ", x:" + (object) x + ", y:" + (object) y + ", skill:" + (object) currentUnit.GetAttackSkill());
      else
        this.GotoState<SceneBattle.State_WaitForLog>();
    }

    private int CountAccessibleGrids(GridMap<int> grids)
    {
      int num = 0;
      for (int y = 0; y < grids.h; ++y)
      {
        for (int x = 0; x < grids.w; ++x)
        {
          if (grids.get(x, y) >= 0)
            ++num;
        }
      }
      return num;
    }

    private void ResetUnitPosition()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      Grid unitGridPosition = this.mBattle.GetUnitGridPosition(currentUnit);
      ((Component) unitController).get_transform().set_position(this.CalcGridCenter(unitGridPosition));
    }

    private Vector3[] FindPath(int startX, int startY, int goalX, int goalY, int disableHeight, GridMap<int> walkableField)
    {
      Grid[] path = this.mBattle.CurrentMap.FindPath(startX, startY, goalX, goalY, disableHeight, walkableField);
      if (path == null)
        return (Vector3[]) null;
      Vector3[] vector3Array = new Vector3[path.Length];
      for (int index = 0; index < vector3Array.Length; ++index)
        vector3Array[index] = this.CalcGridCenter(path[index]);
      return vector3Array;
    }

    private void HideGrid()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsSceneRoot, (UnityEngine.Object) null))
        return;
      this.mTacticsSceneRoot.HideGridLayers();
      this.HideCastSkill(true);
    }

    private Vector3 AdjustPositionToCurrentScene(Vector3 pos)
    {
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector3& local = @pos;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local).z = (^local).z + ((Component) this.mTacticsSceneRoot).get_transform().get_position().z;
      return pos;
    }

    private void ShowUnitCursorOnCurrent()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      if (this.FindUnitController(currentUnit).HasCursor)
        return;
      this.ShowUnitCursor(currentUnit);
    }

    private GridMap<int> CreateCurrentAccessMap()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      int x = currentUnit.x;
      int y = currentUnit.y;
      currentUnit.x = currentUnit.startX;
      currentUnit.y = currentUnit.startY;
      GridMap<int> moveMap = this.mBattle.MoveMap;
      currentUnit.x = x;
      currentUnit.y = y;
      return moveMap.clone();
    }

    private void ShowWalkableGrids(GridMap<int> accessMap, int layerIndex)
    {
      GridMap<Color32> grid = new GridMap<Color32>(accessMap.w, accessMap.h);
      GameSettings instance = GameSettings.Instance;
      for (int x = 0; x < accessMap.w; ++x)
      {
        for (int y = 0; y < accessMap.h; ++y)
        {
          if (accessMap.get(x, y) >= 0)
          {
            accessMap.set(x, y, 0);
            if (this.mCurrentUnitStartX == x && this.mCurrentUnitStartY == y)
              grid.set(x, y, Color32.op_Implicit(instance.Colors.StartGrid));
            else
              grid.set(x, y, Color32.op_Implicit(instance.Colors.WalkableArea));
          }
        }
      }
      this.mTacticsSceneRoot.ShowGridLayer(layerIndex, grid, false);
    }

    private void OnUnitDeath(Unit unit)
    {
      if (unit.Side != EUnitSide.Enemy || this.mCurrentQuest.type == QuestTypes.Tutorial)
        return;
      MonoSingleton<GameManager>.Instance.Player.OnEnemyKill(unit.UnitParam.iname, 1);
    }

    private bool ConditionalGotoGimmickState()
    {
      switch (((LogMapEvent) this.mBattle.Logs.Peek).target.EventTrigger.EventType)
      {
        case EEventType.Treasure:
        case EEventType.Gem:
          this.GotoState<SceneBattle.State_PickupGimmick>();
          return true;
        default:
          this.RemoveLog();
          return false;
      }
    }

    private void FinishGimmickState()
    {
      if (this.mBattle.Logs.Num > 0)
        this.GotoState<SceneBattle.State_WaitForLog>();
      else
        this.GotoMapCommand();
    }

    private void AddGoldCount(int num)
    {
      this.GoldCount += num;
    }

    private void AddTreasureCount(int num)
    {
      this.TreasureCount += num;
    }

    private void AddDispTreasureCount(int num)
    {
      this.DispTreasureCount += num;
    }

    public void RestoreTreasureCount(int num)
    {
      this.TreasureCount = num;
      this.DispTreasureCount = num;
    }

    public TacticsUnitController[] GetActiveUnits()
    {
      return this.mUnitsInBattle.ToArray();
    }

    public TacticsUnitController FindUnitController(Unit unit)
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (this.mTacticsUnits[index].Unit == unit)
          return this.mTacticsUnits[index];
      }
      return (TacticsUnitController) null;
    }

    private void GotoMapCommand()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.IsJumpCant())
      {
        this.mBattle.CommandWait(false);
        this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }
      else
      {
        this.Battle.NotifyMapCommand();
        if (this.Battle.IsMultiPlay && !this.Battle.IsUnitAuto(currentUnit) && this.Battle.EntryBattleMultiPlayTimeUp)
        {
          if (this.Battle.MultiPlayDisconnectAutoBattle && !this.Battle.IsVSForceWin)
          {
            this.GotoState_WaitSignal<SceneBattle.State_MapCommandAI>();
          }
          else
          {
            this.mBattle.CommandWait(false);
            this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
          }
        }
        else
        {
          for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          {
            this.mTacticsUnits[index].AutoUpdateRotation = true;
            this.mTacticsUnits[index].ResetHPGauge();
          }
          if (currentUnit.IsControl && this.mAutoActivateGimmick && (!currentUnit.IsUnitFlag(EUnitFlag.Action) && this.mBattle.CheckGridEventTrigger(currentUnit, EEventTrigger.ExecuteOnGrid)))
          {
            this.mAutoActivateGimmick = false;
            this.GotoState_WaitSignal<SceneBattle.State_SelectGridEventV2>();
          }
          else if (!this.Battle.IsUnitAuto(currentUnit))
          {
            if (currentUnit.IsEnableMoveCondition(false) || currentUnit.IsEnableActionCondition())
            {
              if (this.Battle.IsMultiPlay && currentUnit.OwnerPlayerIndex != this.Battle.MyPlayerIndex)
              {
                this.RefreshOnlyMapCommand();
                this.mBattleUI.OnCommandSelectStart();
                this.GotoState_WaitSignal<SceneBattle.State_MapCommandVersus>();
              }
              else
              {
                this.RefreshMapCommands();
                this.mBattleUI.OnCommandSelectStart();
                this.GotoState_WaitSignal<SceneBattle.State_MapCommandV2>();
              }
            }
            else if (!currentUnit.IsEnableSelectDirectionCondition())
            {
              this.mBattle.CommandWait(false);
              this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
            }
            else if (this.Battle.IsMultiPlay && currentUnit.OwnerPlayerIndex != this.Battle.MyPlayerIndex)
            {
              this.GotoState_WaitSignal<SceneBattle.State_MapCommandMultiPlay>();
            }
            else
            {
              this.mBattleUI.OnInputDirectionStart();
              this.mBattleUI.CommandWindow.CancelButton.SetActive(false);
              this.GotoState_WaitSignal<SceneBattle.State_InputDirection>();
            }
          }
          else
            this.GotoState_WaitSignal<SceneBattle.State_MapCommandAI>();
        }
      }
    }

    private void GotoMapEnd()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI, (UnityEngine.Object) null))
      {
        this.mBattleUI.OnQuestEnd();
        this.mBattleUI.OnMapEnd();
      }
      this.mBattleUI.OnMapEnd();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        this.mBattleUI_MultiPlay.OnMapEnd();
      this.EndMultiPlayer();
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        this.GotoState_WaitSignal<SceneBattle.State_AudienceEnd>();
      else
        this.GotoState_WaitSignal<SceneBattle.State_MapEndV2>();
    }

    private void TriggerWinEvent()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventScript, (UnityEngine.Object) null))
      {
        switch (this.Battle.GetQuestResult())
        {
          case BattleCore.QuestResult.Win:
            this.mEventSequence = this.mEventScript.OnQuestWin();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventSequence, (UnityEngine.Object) null))
            {
              this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_PreQuestResult>>();
              return;
            }
            break;
          case BattleCore.QuestResult.Lose:
            this.mEventSequence = this.mEventScript.OnQuestLose();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventSequence, (UnityEngine.Object) null))
            {
              this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_PreQuestResult>>();
              return;
            }
            break;
        }
      }
      this.GotoState<SceneBattle.State_PreQuestResult>();
    }

    public int FirstContact
    {
      get
      {
        return this.mFirstContact;
      }
    }

    public void SubmitResult()
    {
      if (this.mQuestResultSending)
        return;
      this.mQuestResultSending = true;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        long btlId = this.mBattle.BtlID;
        if (this.mCurrentQuest.IsScenario)
          this.SubmitScenarioResult(btlId);
        else
          this.SubmitBattleResult(btlId, this.mCurrentQuest);
      }
      else
      {
        if (this.mBattle.GetQuestResult() == BattleCore.QuestResult.Win)
        {
          MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
          if (this.IsOrdealQuest && this.mIsFirstPlay)
          {
            this.FirstClearItemId = (string) null;
            if (this.mCurrentQuest.FirstClearItems != null && this.mCurrentQuest.FirstClearItems.Length != 0)
            {
              string firstClearItem = this.mCurrentQuest.FirstClearItems[0];
              if (!string.IsNullOrEmpty(firstClearItem))
              {
                string[] strArray = firstClearItem.Split(',');
                if (strArray != null && strArray.Length != 0)
                {
                  this.FirstClearItemId = strArray[0];
                  ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.FirstClearItemId);
                  if (itemParam == null || itemParam.type != EItemType.Unit)
                    this.FirstClearItemId = (string) null;
                }
              }
            }
          }
        }
        BattleCore.RemoveSuspendData();
        this.mQuestResultSent = true;
      }
    }

    public void OnColoRankModify()
    {
      string errMsg = Network.ErrMsg;
      Network.RemoveAPI();
      Network.ResetError();
      UIUtility.SystemMessage((string) null, errMsg, (UIUtility.DialogResultEvent) (go =>
      {
        this.mQuestResultSent = true;
        this.mArenaSubmitMode = SceneBattle.eArenaSubmitMode.FAILED;
      }), (GameObject) null, true, -1);
    }

    private void SubmitResultCallbackImpl(WWWResult www, bool isArenaType = false)
    {
      if (FlowNode_Network.HasCommonError(www) || this.Battle.QuestType == QuestTypes.Tower && TowerErrorHandle.Error((FlowNode_Network) null))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestEnd:
          case Network.EErrCode.ColoNoBattle:
            FlowNode_Network.Failed();
            break;
          case Network.EErrCode.ColoRankLower:
          case Network.EErrCode.ColoRankModify:
          case Network.EErrCode.ColoMyRankModify:
            if (isArenaType)
            {
              this.OnColoRankModify();
              break;
            }
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        JSON_QuestProgress[] json = (JSON_QuestProgress[]) null;
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonBodyResponse;
        if (this.Battle.QuestType == QuestTypes.Tower)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          TowerResuponse towerResuponse = instance.TowerResuponse;
          WebAPI.JSON_BodyResponse<Json_TowerBtlResult> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TowerBtlResult>>(www.text);
          towerResuponse.Deserialize(jsonObject.body.pdeck);
          towerResuponse.arrived_num = jsonObject.body.arrived_num;
          towerResuponse.clear = jsonObject.body.clear;
          if (jsonObject.body.ranking != null)
          {
            towerResuponse.speedRank = jsonObject.body.ranking.spd_rank;
            towerResuponse.techRank = jsonObject.body.ranking.tec_rank;
            towerResuponse.turn_num = jsonObject.body.ranking.turn_num;
            towerResuponse.died_num = jsonObject.body.ranking.died_num;
            towerResuponse.retire_num = jsonObject.body.ranking.retire_num;
            towerResuponse.recover_num = jsonObject.body.ranking.recovery_num;
            towerResuponse.spd_score = jsonObject.body.ranking.spd_score;
            towerResuponse.tec_score = jsonObject.body.ranking.tec_score;
            towerResuponse.ret_score = jsonObject.body.ranking.ret_score;
            towerResuponse.rcv_score = jsonObject.body.ranking.rcv_score;
          }
          QuestParam quest = instance.FindQuest(this.Battle.QuestID);
          if (quest != null && quest.HasMission())
          {
            BattleCore.Record questRecord = this.Battle.GetQuestRecord();
            if (questRecord.takeoverProgressList != null)
            {
              for (int index = 0; index < questRecord.takeoverProgressList.Count; ++index)
                quest.SetMissionValue(index, questRecord.takeoverProgressList[index]);
            }
          }
          if (jsonObject.body.artis != null)
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artis, false);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.Tower && this.Battle.IsMultiVersus)
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          WebAPI.JSON_BodyResponse<Json_VersusEndEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusEndEnd>>(www.text);
          instance.SetVersusWinCount(jsonObject.body.wincnt);
          instance.SetVersuTowerEndParam(jsonObject.body.rankup == 1, jsonObject.body.win_bonus == 1, jsonObject.body.key, jsonObject.body.floor, jsonObject.body.arravied);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.RankMatch && this.Battle.IsMultiVersus)
        {
          WebAPI.JSON_BodyResponse<Json_VersusEndEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusEndEnd>>(www.text);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else if (GlobalVars.SelectedMultiPlayVersusType != VERSUS_TYPE.Tower && this.Battle.IsMultiVersus)
        {
          WebAPI.JSON_BodyResponse<Json_VersusEndEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusEndEnd>>(www.text);
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if (!instance.IsVSCpuBattle)
            instance.SetVersusWinCount(jsonObject.body.wincnt);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else
        {
          WebAPI.JSON_BodyResponse<Json_BtlComEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_BtlComEnd>>(www.text);
          MonoSingleton<GameManager>.Instance.Player.RegistTrophyStateDictByProgExtra(jsonObject.body.trophyprogs);
          json = jsonObject.body.quests;
          if (jsonObject.body.quest_ranking != null)
          {
            this.mIsRankingQuestNewScore = jsonObject.body.quest_ranking.IsNewScore;
            this.mRankingQuestNewRank = jsonObject.body.quest_ranking.rank;
            this.mIsRankingQuestJoinReward = jsonObject.body.quest_ranking.IsJoinReward;
          }
          this.FirstClearItemId = (string) null;
          if (jsonObject.body.fclr_items != null && jsonObject.body.fclr_items.Length != 0)
          {
            this.FirstClearItemId = jsonObject.body.fclr_items[0].iname;
            ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.FirstClearItemId);
            if (itemParam == null || itemParam.type != EItemType.Unit)
              this.FirstClearItemId = (string) null;
          }
          if (jsonObject.body.cards != null)
          {
            for (int index = 0; index < jsonObject.body.cards.Length; ++index)
            {
              GlobalVars.IsDirtyConceptCardData.Set(true);
              if (jsonObject.body.cards[index].IsGetUnit)
              {
                FlowNode_ConceptCardGetUnit.AddConceptCardData(ConceptCardData.CreateConceptCardDataForDisplay(jsonObject.body.cards[index].iname));
                this.mBattle.AddReward(RewardType.Unit, jsonObject.body.cards[index].get_unit);
              }
            }
          }
          this.m_IsCardSendMail = jsonObject.body.is_mail_cards == 1;
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        if (jsonBodyResponse.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          List<ConceptCardData> conceptCardDataList = new List<ConceptCardData>();
          for (int index = 0; index < this.Battle.Units.Count; ++index)
          {
            if (this.Battle.Units[index] != null && this.Battle.Units[index].UnitData.ConceptCard != null && this.Battle.Units[index].IsPartyMember)
              conceptCardDataList.Add(this.Battle.Units[index].UnitData.ConceptCard);
          }
          try
          {
            GameManager instance = MonoSingleton<GameManager>.Instance;
            VersusCoinParam versusCoinParam = instance.GetVersusCoinParam(this.CurrentQuest.iname);
            int multiCoin1 = instance.Player.MultiCoin;
            int num = 0;
            if (versusCoinParam != null)
              num = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(versusCoinParam.coin_iname);
            instance.Deserialize(jsonBodyResponse.body.player);
            instance.Deserialize(jsonBodyResponse.body.units);
            instance.Deserialize(jsonBodyResponse.body.items);
            if (json != null)
              MonoSingleton<GameManager>.Instance.Deserialize(json);
            if (jsonBodyResponse.body.mails != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonBodyResponse.body.mails);
            if (jsonBodyResponse.body.fuids != null && (this.mCurrentQuest.type == QuestTypes.Multi || this.mCurrentQuest.type == QuestTypes.MultiGps || this.mCurrentQuest.IsMultiTower))
              MonoSingleton<GameManager>.Instance.Deserialize(jsonBodyResponse.body.fuids);
            int multiCoin2 = MonoSingleton<GameManager>.Instance.Player.MultiCoin;
            if (this.Battle.IsMultiPlay)
            {
              BattleCore.Record questRecord = this.Battle.GetQuestRecord();
              questRecord.multicoin = (OInt) (multiCoin2 - multiCoin1);
              if (versusCoinParam != null)
              {
                int itemAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(versusCoinParam.coin_iname);
                questRecord.pvpcoin = (OInt) (itemAmount - num);
              }
            }
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          if (isArenaType)
          {
            WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_ArenaPlayerDataAll>>(www.text);
            if (jsonObject.body == null)
            {
              FlowNode_Network.Retry();
              return;
            }
            ArenaBattleResponse arenaBattleResponse = new ArenaBattleResponse();
            arenaBattleResponse.Deserialize(jsonObject.body.btlres);
            GlobalVars.ResultArenaBattleResponse = arenaBattleResponse;
            this.mArenaSubmitMode = SceneBattle.eArenaSubmitMode.SUCCESS;
          }
          if (this.mBattle.IsAutoBattle)
            GameUtility.SetDefaultSleepSetting();
          if (this.IsPlayingMultiQuest)
            this.mFirstContact = jsonBodyResponse.body.first_contact;
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          this.mQuestResultSent = true;
          Network.RemoveAPI();
          this.TrophyLvupCheck();
          for (int index1 = 0; index1 < conceptCardDataList.Count; ++index1)
          {
            if (jsonBodyResponse.body.units != null)
            {
              for (int index2 = 0; index2 < jsonBodyResponse.body.units.Length; ++index2)
              {
                if (jsonBodyResponse.body.units[index2].concept_card != null && (long) conceptCardDataList[index1].UniqueID == jsonBodyResponse.body.units[index2].concept_card.iid)
                  MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrustMaxTrophy(jsonBodyResponse.body.units[index2].concept_card.iname, jsonBodyResponse.body.units[index2].concept_card.trust);
              }
            }
          }
          if (this.mIsForceEndQuest)
            MonoSingleton<GameManager>.Instance.Player.SetQuestMissionFlags(this.mCurrentQuest.iname, this.mBattle.GetQuestRecord().bonusFlags);
          BattleCore.RemoveSuspendData();
        }
      }
    }

    private void SubmitResultCallback(WWWResult www)
    {
      this.SubmitResultCallbackImpl(www, false);
    }

    private void SubmitArenaResultCallback(WWWResult www)
    {
      this.SubmitResultCallbackImpl(www, true);
    }

    private void SubmitBattleResult(long btlid, QuestParam quest)
    {
      int time = 0;
      bool isMultiPlay = PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay;
      BattleCore.Record questRecord = this.mBattle.GetQuestRecord();
      BtlResultTypes result = BtlResultTypes.Win;
      if (questRecord.result == BattleCore.QuestResult.Pending)
      {
        result = BtlResultTypes.Retire;
        if (this.mRevertQuestNewIfRetire)
        {
          result = BtlResultTypes.Cancel;
          MonoSingleton<GameManager>.Instance.Player.SetQuestState(quest.name, QuestStates.New);
        }
      }
      else if (questRecord.result == BattleCore.QuestResult.Draw)
        result = BtlResultTypes.Draw;
      else if (questRecord.result != BattleCore.QuestResult.Win)
        result = BtlResultTypes.Lose;
      if (questRecord.result == BattleCore.QuestResult.Win)
        MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
      if (questRecord.result == BattleCore.QuestResult.Win)
        MonoSingleton<GameManager>.Instance.Player.IncrementQuestChallangeNumDaily(this.mCurrentQuest.iname);
      if (questRecord.drops == null)
        questRecord.drops = new OInt[0];
      if (questRecord.item_steals == null)
        questRecord.item_steals = new OInt[0];
      if (questRecord.gold_steals == null)
        questRecord.gold_steals = new OInt[0];
      int[] beats = new int[questRecord.drops.Length];
      for (int index = 0; index < questRecord.drops.Length; ++index)
        beats[index] = (int) questRecord.drops[index];
      int[] itemSteals = new int[questRecord.item_steals.Length];
      for (int index = 0; index < questRecord.item_steals.Length; ++index)
        itemSteals[index] = (int) questRecord.item_steals[index];
      int[] goldSteals = new int[questRecord.gold_steals.Length];
      for (int index = 0; index < questRecord.gold_steals.Length; ++index)
        goldSteals[index] = (int) questRecord.gold_steals[index];
      int[] missions = new int[questRecord.bonusCount];
      for (int index = 0; index < missions.Length; ++index)
        missions[index] = (questRecord.bonusFlags & 1 << index) == 0 ? 0 : 1;
      this.UpdateTrophy();
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      string trophy_progs;
      string bingo_progs;
      instance1.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      if (quest.type == QuestTypes.Arena)
      {
        ArenaPlayer selectedArenaPlayer = (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
        Network.RequestAPI((WebAPI) new ReqBtlComEnd(selectedArenaPlayer.FUID, selectedArenaPlayer.ArenaRank, MonoSingleton<GameManager>.Instance.Player.ArenaRank, result, beats, itemSteals, goldSteals, missions, (string[]) null, questRecord.used_items, new Network.ResponseCallback(this.SubmitArenaResultCallback), BtlEndTypes.colo, trophy_progs, bingo_progs), false);
      }
      else if (quest.type == QuestTypes.Tower)
      {
        int[] missions_value = new int[questRecord.bonusCount];
        for (int index = 0; index < missions_value.Length && index < questRecord.takeoverProgressList.Count; ++index)
          missions_value[index] = questRecord.takeoverProgressList[index];
        int round = (int) instance1.TowerResuponse.round;
        TowerFloorParam towerFloorParam = instance1.TowerResuponse.GetCurrentFloor() ?? instance1.FindTowerFloor(quest.iname);
        byte floor = towerFloorParam == null ? (byte) 0 : towerFloorParam.floor;
        Network.RequestAPI((WebAPI) new ReqTowerBtlComEnd(btlid, this.Battle.Player.ToArray(), this.Battle.Enemys.ToArray(), this.Battle.ActionCount, round, floor, result, this.Battle.Map[0].mRandDeckResult, new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs, missions, missions_value), false);
      }
      else if (quest.type == QuestTypes.VersusFree || quest.type == QuestTypes.VersusRank)
      {
        MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
        List<int> finishHp1 = this.Battle.GetFinishHp(EUnitSide.Player);
        List<int> finishHp2 = this.Battle.GetFinishHp(EUnitSide.Enemy);
        int deadCount = this.Battle.GetDeadCount(EUnitSide.Enemy);
        List<int> intList = new List<int>();
        PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[7];
        for (int index = 0; index < party.MAX_UNIT; ++index)
          intList.Add(PlayerPrefsUtility.GetInt(PlayerPrefsUtility.VERSUS_ID_KEY + (object) index, 0));
        if (instance1.IsVSCpuBattle)
        {
          Network.RequestAPI((WebAPI) new ReqVersusCpuEnd(btlid, result, this.Battle.VersusTurnCount, finishHp1.ToArray(), finishHp2.ToArray(), this.Battle.TotalDamages, this.Battle.TotalDamagesTaken, this.Battle.TotalHeal, deadCount, intList.ToArray(), new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs), false);
        }
        else
        {
          JSON_MyPhotonPlayerParam photonPlayerParam = pt.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex != pt.MyPlayerIndex));
          Network.RequestAPI((WebAPI) new ReqVersusEnd(btlid, result, photonPlayerParam.UID, photonPlayerParam.FUID, this.Battle.VersusTurnCount, finishHp1.ToArray(), finishHp2.ToArray(), this.Battle.TotalDamages, this.Battle.TotalDamagesTaken, this.Battle.TotalHeal, deadCount, intList.ToArray(), new Network.ResponseCallback(this.SubmitResultCallback), GlobalVars.SelectedMultiPlayVersusType, trophy_progs, bingo_progs), false);
        }
      }
      else if (quest.type == QuestTypes.RankMatch)
      {
        MyPhoton pt = PunMonoSingleton<MyPhoton>.Instance;
        List<int> finishHp1 = this.Battle.GetFinishHp(EUnitSide.Player);
        List<int> finishHp2 = this.Battle.GetFinishHp(EUnitSide.Enemy);
        int deadCount = this.Battle.GetDeadCount(EUnitSide.Enemy);
        List<int> intList = new List<int>();
        PartyData party = MonoSingleton<GameManager>.Instance.Player.Partys[10];
        for (int index = 0; index < party.MAX_UNIT; ++index)
          intList.Add(PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RANKMATCH_ID_KEY + (object) index, 0));
        JSON_MyPhotonPlayerParam photonPlayerParam = pt.GetMyPlayersStarted().Find((Predicate<JSON_MyPhotonPlayerParam>) (p => p.playerIndex != pt.MyPlayerIndex));
        instance1.Player.RankMatchResult = result;
        if (instance1.Player.RankMatchBattlePoint > 0)
        {
          instance1.Player.IncrementRankMatchMission(RankMatchMissionType.Battle);
          if (result == BtlResultTypes.Win)
          {
            instance1.Player.IncrementRankMatchMission(RankMatchMissionType.Win);
            instance1.Player.SetMaxProgRankMatchMission(RankMatchMissionType.StreakWin, instance1.Player.RankMatchStreakWin + 1);
            MyPhoton.MyRoom currentRoom = pt.GetCurrentRoom();
            if (currentRoom != null)
            {
              JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
              int num1 = 0;
              if (myPhotonRoomParam != null && myPhotonRoomParam.players != null && myPhotonRoomParam.players.Length > 1)
              {
                for (int index = 0; index < myPhotonRoomParam.players.Length; ++index)
                {
                  if (myPhotonRoomParam.players[index].playerID != pt.GetMyPlayer().playerID)
                  {
                    num1 = myPhotonRoomParam.players[index].rankmatch_score;
                    break;
                  }
                }
              }
              VersusRankParam versusRankParam = instance1.GetVersusRankParam(instance1.RankMatchScheduleId);
              VersusRankClassParam versusRankClass = instance1.GetVersusRankClass(instance1.RankMatchScheduleId, instance1.Player.RankMatchClass);
              if (versusRankParam != null && versusRankClass != null)
              {
                int num2 = Mathf.Clamp((int) Math.Truncate((double) versusRankParam.WinPointBase * ((double) (num1 - instance1.Player.RankMatchScore) * 0.001 + 1.0)), versusRankClass.WinPointMin, versusRankClass.WinPointMax);
                instance1.Player.SetMaxProgRankMatchMission(RankMatchMissionType.GetPoint, instance1.Player.RankMatchScore + num2);
              }
            }
          }
        }
        string missionProgressString = instance1.Player.GetMissionProgressString();
        Network.RequestAPI((WebAPI) new ReqRankMatchEnd(btlid, result, photonPlayerParam.UID, photonPlayerParam.FUID, this.Battle.VersusTurnCount, finishHp1.ToArray(), finishHp2.ToArray(), this.Battle.TotalDamages, this.Battle.TotalDamagesTaken, this.Battle.TotalHeal, deadCount, intList.ToArray(), new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs, missionProgressString), false);
      }
      else
      {
        MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance2.GetMyPlayersStarted();
        string[] fuid = (string[]) null;
        if (GlobalVars.LastQuestResult.Get() == BattleCore.QuestResult.Win && myPlayersStarted != null)
        {
          fuid = new string[myPlayersStarted.Count];
          int num = 0;
          for (int index = 0; index < myPlayersStarted.Count; ++index)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted[index];
            if (photonPlayerParam != null && photonPlayerParam.playerIndex != instance2.MyPlayerIndex && !string.IsNullOrEmpty(photonPlayerParam.FUID))
              fuid[num++] = photonPlayerParam.FUID;
          }
        }
        SupportData supportData = MonoSingleton<GameManager>.Instance.Player.Supports.Find((Predicate<SupportData>) (f => f.FUID == GlobalVars.SelectedFriendID));
        if (this.Battle.IsMultiTower)
        {
          List<int> finishHp = this.Battle.GetFinishHp(EUnitSide.Player);
          List<string> playerName = this.Battle.GetPlayerName();
          Network.RequestAPI((WebAPI) new ReqBtlMultiTwEnd(btlid, time, result, finishHp.ToArray(), playerName.ToArray(), fuid, new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs), false);
        }
        else if (this.Battle.IsRankingQuest && questRecord.result == BattleCore.QuestResult.Win)
        {
          RankingQuestParam rankingQuestParam = this.Battle.GetRankingQuestParam();
          int main_score = 0;
          if (rankingQuestParam.type == RankingQuestType.ActionCount)
            main_score = this.Battle.ActionCount;
          int sub_score = this.Battle.CalcPlayerUnitsTotalParameter();
          string rankingQuestEndParam = ReqBtlComEnd.CreateRankingQuestEndParam(main_score, sub_score);
          Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, time, result, beats, itemSteals, goldSteals, missions, fuid, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), !isMultiPlay ? BtlEndTypes.com : BtlEndTypes.multi, trophy_progs, bingo_progs, supportData == null ? 0 : (int) supportData.UnitElement, rankingQuestEndParam, false, new bool?()), false);
        }
        else
        {
          BtlEndTypes apiType = !isMultiPlay ? BtlEndTypes.com : BtlEndTypes.multi;
          if (quest.type == QuestTypes.Ordeal)
            apiType = BtlEndTypes.ordeal;
          bool? is_skip = new bool?();
          if (quest.type == QuestTypes.Tutorial)
            is_skip = new bool?(GlobalVars.IsSkipQuestDemo);
          Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, time, result, beats, itemSteals, goldSteals, missions, fuid, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), apiType, trophy_progs, bingo_progs, supportData == null ? 0 : (int) supportData.UnitElement, (string) null, false, is_skip), false);
        }
      }
      MonoSingleton<GameManager>.Instance.Player.ResetMissionClearAt();
    }

    private void SubmitScenarioResult(long btlid)
    {
      Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, 0, BtlResultTypes.Win, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.SubmitResultCallback), BtlEndTypes.com, (string) null, (string) null, 0, (string) null, false, new bool?()), false);
      MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
    }

    public void ForceEndQuest()
    {
      this.mIsForceEndQuest = true;
      this.mExecDisconnected = true;
      if (this.Battle.IsMultiTower)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          instance.Disconnect();
      }
      if (this.IsInState<SceneBattle.State_ExitQuest>())
        return;
      this.GotoState<SceneBattle.State_ExitQuest>();
    }

    private void UpdateTrophy()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      BattleCore.QuestResult questResult = this.mBattle.GetQuestResult();
      if (this.mCurrentQuest.type == QuestTypes.Arena)
      {
        if (this.mBattle.GetQuestRecord().result == BattleCore.QuestResult.Win)
          player.OnQuestWin(this.mCurrentQuest.iname, (BattleCore.Record) null);
        else
          player.OnQuestLose(this.mCurrentQuest.iname);
      }
      else
      {
        switch (questResult)
        {
          case BattleCore.QuestResult.Win:
            BattleCore.Record questRecord = this.Battle.GetQuestRecord();
            player.OnQuestWin(this.mCurrentQuest.iname, questRecord);
            for (int index = 0; index < questRecord.items.Count; ++index)
            {
              if (questRecord.items[index].itemParam != null)
                player.OnItemQuantityChange(questRecord.items[index].itemParam.iname, 1);
            }
            break;
          case BattleCore.QuestResult.Lose:
            player.OnQuestLose(this.mCurrentQuest.iname);
            break;
        }
        if (!this.mCurrentQuest.IsMultiTower)
          return;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        int mtClearedMaxFloor = MonoSingleton<GameManager>.Instance.GetMTClearedMaxFloor();
        int selectedMultiTowerFloor = GlobalVars.SelectedMultiTowerFloor;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
          return;
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        if (roomPlayerList == null)
          return;
        for (int index = 0; index < roomPlayerList.Count; ++index)
        {
          JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index].json);
          if (photonPlayerParam != null && photonPlayerParam.playerIndex != instance.MyPlayerIndex && (selectedMultiTowerFloor <= mtClearedMaxFloor && selectedMultiTowerFloor > photonPlayerParam.mtClearedFloor))
            player.OnMultiTowerHelp();
        }
      }
    }

    private void TrophyLvupCheck()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (this.mStartPlayerLevel >= player.Lv)
        return;
      player.OnPlayerLevelChange(player.Lv - this.mStartPlayerLevel);
    }

    public void ForceEndQuestInArena()
    {
      if (this.Battle.IsArenaSkip)
        return;
      this.Pause(false);
      GlobalEvent.Invoke("CLOSE_QUESTMENU", (object) this);
      if (this.Battle.GetQuestResult() != BattleCore.QuestResult.Pending)
        return;
      this.Battle.IsArenaSkip = true;
      SRPG_TouchInputModule.LockInput();
      this.GotoState_WaitSignal<SceneBattle.State_ArenaSkipWait>();
    }

    public QuestResultData ResultData
    {
      get
      {
        return this.mSavedResult;
      }
    }

    private void SaveResult()
    {
      this.mSavedResult = new QuestResultData(MonoSingleton<GameManager>.Instance.Player, this.mCurrentQuest.clear_missions, this.mBattle.GetQuestRecord(), this.Battle.AllUnits, this.mIsFirstWin);
    }

    private void OFFLINE_APPLY_QUEST_RESULT()
    {
      if (this.mCurrentQuest.IsScenario)
      {
        MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
      }
      else
      {
        BattleCore.Record questRecord = this.Battle.GetQuestRecord();
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (questRecord.result != BattleCore.QuestResult.Win)
          return;
        player.GainGold((int) questRecord.gold);
        for (int index = 0; index < this.Battle.Player.Count; ++index)
          this.Battle.Player[index].UnitData.GainExp((int) questRecord.unitexp, player.Lv);
        if (questRecord.items != null)
        {
          for (int index = 0; index < questRecord.items.Count; ++index)
          {
            if (questRecord.items[index].itemParam != null)
              player.GainItem(questRecord.items[index].itemParam.iname, 1);
          }
        }
        MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
        MonoSingleton<GameManager>.Instance.Player.SetQuestMissionFlags(this.mCurrentQuest.iname, questRecord.bonusFlags);
      }
    }

    private void ExitScene()
    {
      if (this.mSceneExiting)
        return;
      this.mSceneExiting = true;
      MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(false);
      if (this.mCurrentQuest.type == QuestTypes.Tutorial)
      {
        MyMetaps.TrackTutorialPoint(this.mCurrentQuest.iname);
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "TUTORIAL_EXIT");
      }
      else
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "EXIT");
    }

    public void Pause(bool flag)
    {
      if (flag)
        ++this.mPauseReqCount;
      else
        this.mPauseReqCount = Math.Max(0, this.mPauseReqCount - 1);
      TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.Game, this.mPauseReqCount <= 0 ? 1f : 0.0f);
    }

    public bool IsPause()
    {
      return this.mPauseReqCount != 0;
    }

    public void CleanUpMultiPlay()
    {
      this.CleanupGoodJob();
      if (!this.Battle.IsMultiTower || this.Battle.GetQuestResult() == BattleCore.QuestResult.Lose)
        PunMonoSingleton<MyPhoton>.Instance.Reset();
      DebugUtility.Log("CleanUpMultiPlay done.");
    }

    private SceneBattle.PosRot GetCameraOffset_Unit()
    {
      Transform transform = ((Component) GameSettings.Instance.Quest.UnitCamera).get_transform();
      return new SceneBattle.PosRot()
      {
        Position = transform.get_localPosition(),
        Rotation = transform.get_localRotation()
      };
    }

    private void GotoPrepareSkill()
    {
      LogSkill peek = this.mBattle.Logs.Peek as LogSkill;
      if (peek.skill != null && peek.skill.IsCastSkill() && peek.skill.CastType == ECastTypes.Jump)
      {
        TacticsUnitController unitController = this.FindUnitController(peek.self);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          IntVector2 pos = peek.pos;
          Vector3 vector3 = this.CalcGridCenter(peek.pos.x, peek.pos.y);
          unitController.JumpMapFallPos = pos;
          unitController.JumpFallPos = vector3;
        }
      }
      if (peek != null && peek.self != null && peek.self.Side == EUnitSide.Player)
        this.mLastPlayerSideUseSkillUnit = peek.self;
      this.mEventRecvSkillUnitLists.Clear();
      if (peek != null && peek.skill != null && peek.targets != null)
      {
        TacticsUnitController unitController1 = this.FindUnitController(peek.self);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController1))
        {
          EElement elem = peek.skill.ElementType;
          if (elem == EElement.None)
            elem = unitController1.Unit.Element;
          using (List<LogSkill.Target>.Enumerator enumerator = peek.targets.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              LogSkill.Target current = enumerator.Current;
              TacticsUnitController unitController2 = this.FindUnitController(current.target);
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController2))
              {
                if (peek.skill.IsDamagedSkill() && !current.IsAvoid())
                  this.mEventRecvSkillUnitLists.Add(new SceneBattle.EventRecvSkillUnit(unitController2, elem));
                if (current.IsFailCondition())
                  this.mEventRecvSkillUnitLists.Add(new SceneBattle.EventRecvSkillUnit(unitController2, current.failCondition));
              }
            }
          }
        }
      }
      if (this.mBattle.IsUnitAuto(this.mBattle.CurrentUnit) || this.mBattle.EntryBattleMultiPlayTimeUp)
        this.HideGrid();
      this.CancelMapViewMode();
      BattleCameraFukan gameObject = GameObjectID.FindGameObject<BattleCameraFukan>(this.mBattleUI.FukanCameraID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        gameObject.SetCameraMode(SceneBattle.CameraMode.DEFAULT);
        gameObject.SetDisp(false);
      }
      if (!this.mBattle.IsSkillDirection)
      {
        if (peek != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController1 = this.FindUnitController(peek.self);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null) && peek.targets != null && peek.targets.Count != 0)
          {
            List<TacticsUnitController> TargetLists = new List<TacticsUnitController>();
            using (List<LogSkill.Target>.Enumerator enumerator = peek.targets.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                TacticsUnitController unitController2 = this.FindUnitController(enumerator.Current.target);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
                  TargetLists.Add(unitController2);
              }
            }
            if (TargetLists.Count != 0)
            {
              this.mEventSequence = this.mEventScript.OnUseSkill(EventScript.SkillTiming.BEFORE, unitController1, peek.skill, TargetLists, this.mIsFirstPlay);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventSequence, (UnityEngine.Object) null))
              {
                this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitGC<SceneBattle.State_DirectionOffSkill>>>();
                return;
              }
            }
          }
        }
        this.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_DirectionOffSkill>>();
      }
      else
      {
        if (peek != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController1 = this.FindUnitController(peek.self);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null) && peek.targets != null && peek.targets.Count != 0)
          {
            List<TacticsUnitController> TargetLists = new List<TacticsUnitController>();
            using (List<LogSkill.Target>.Enumerator enumerator = peek.targets.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                TacticsUnitController unitController2 = this.FindUnitController(enumerator.Current.target);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
                  TargetLists.Add(unitController2);
              }
            }
            if (TargetLists.Count != 0)
            {
              this.mEventSequence = this.mEventScript.OnUseSkill(EventScript.SkillTiming.BEFORE, unitController1, peek.skill, TargetLists, this.mIsFirstPlay);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventSequence, (UnityEngine.Object) null))
              {
                this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitGC<SceneBattle.State_PrepareSkill>>>();
                return;
              }
            }
          }
        }
        this.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_PrepareSkill>>();
      }
    }

    private void CancelMapViewMode()
    {
      if (!this.Battle.IsMultiPlay || !this.VersusMapView)
        return;
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        this.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
        this.mTacticsUnits[index].SetHPChangeYosou(this.mTacticsUnits[index].VisibleHPValue, 0);
      }
      this.mBattleUI.OnCommandSelect();
      GlobalEvent.Invoke("BATTLE_UNIT_DETAIL_CLOSE", (object) this);
      this.mBattleUI.HideTargetWindows();
      this.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
      this.mBattleUI.OnMapViewEnd();
      this.VersusMapView = false;
    }

    private Unit mCollaboTargetUnit
    {
      get
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mCollaboTargetTuc))
          return this.mCollaboTargetTuc.Unit;
        return (Unit) null;
      }
    }

    public Unit CollaboMainUnit
    {
      get
      {
        if (!this.mIsInstigatorSubUnit)
          return this.mCollaboMainUnit;
        return this.mCollaboTargetUnit;
      }
    }

    public Unit CollaboSubUnit
    {
      get
      {
        if (this.mIsInstigatorSubUnit)
          return this.mCollaboMainUnit;
        return this.mCollaboTargetUnit;
      }
    }

    private void SetScreenMirroring(bool mirror)
    {
      RenderPipeline component = (RenderPipeline) ((Component) Camera.get_main()).GetComponent<RenderPipeline>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.FlipHorizontally = mirror;
      this.isScreenMirroring = mirror;
    }

    private bool FindChangedShield(out TacticsUnitController tuc, out TacticsUnitController.ShieldState shield)
    {
      List<SceneBattle.FindShield> l = new List<SceneBattle.FindShield>();
      for (int index1 = 0; index1 < this.mTacticsUnits.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.mTacticsUnits[index1].Shields.Count; ++index2)
        {
          if (this.mTacticsUnits[index1].Shields[index2].Dirty)
            l.Add(new SceneBattle.FindShield(this.mTacticsUnits[index1], this.mTacticsUnits[index1].Shields[index2]));
        }
        if (l.Count != 0)
          break;
      }
      tuc = (TacticsUnitController) null;
      shield = (TacticsUnitController.ShieldState) null;
      if (l.Count == 0)
        return false;
      if (l.Count > 1)
        MySort<SceneBattle.FindShield>.Sort(l, (Comparison<SceneBattle.FindShield>) ((src, dst) =>
        {
          if ((int) src.mShield.Target.hp != (int) dst.mShield.Target.hp)
          {
            if ((int) src.mShield.Target.hp < (int) dst.mShield.Target.hp)
              return -1;
            if ((int) src.mShield.Target.hp > (int) dst.mShield.Target.hp)
              return 1;
          }
          return 0;
        }));
      SceneBattle.FindShield findShield = l[0];
      tuc = findShield.mTuc;
      shield = findShield.mShield;
      return true;
    }

    [DebuggerHidden]
    private IEnumerator SetWeatherEffect(WeatherData wd)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CSetWeatherEffect\u003Ec__Iterator31()
      {
        wd = wd,
        \u003C\u0024\u003Ewd = wd,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator StopWeatherEffect(bool is_immidiate = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CStopWeatherEffect\u003Ec__Iterator32()
      {
        is_immidiate = is_immidiate,
        \u003C\u0024\u003Eis_immidiate = is_immidiate,
        \u003C\u003Ef__this = this
      };
    }

    public bool isUpView
    {
      get
      {
        return this.m_CameraMode == SceneBattle.CameraMode.UPVIEW;
      }
    }

    public bool isNewCamera
    {
      get
      {
        return this.m_NewCamera;
      }
    }

    public bool isFullRotationCamera
    {
      get
      {
        return this.m_FullRotationCamera;
      }
    }

    public bool mUpdateCameraPosition
    {
      set
      {
        this.m_UpdateCamera = value;
      }
      get
      {
        return this.m_UpdateCamera;
      }
    }

    public bool isBattleCamera
    {
      get
      {
        return this.m_BattleCamera;
      }
    }

    public TargetCamera targetCamera
    {
      get
      {
        return this.m_TargetCamera;
      }
    }

    private bool IsCameraMoving
    {
      get
      {
        return ObjectAnimator.Get((Component) Camera.get_main()).isMoving || this.mUpdateCameraPosition && (this.m_TargetCameraPositionInterp || this.m_TargetCameraDistanceInterp);
      }
    }

    public bool isCameraLeftMove
    {
      get
      {
        if (this.isFullRotationCamera)
          return true;
        return (double) this.m_CameraAngle - (double) this.m_CameraYawMin > 0.00999999977648258;
      }
    }

    public bool isCameraRightMove
    {
      get
      {
        if (this.isFullRotationCamera)
          return true;
        return (double) this.m_CameraYawMax - (double) this.m_CameraAngle > 0.00999999977648258;
      }
    }

    private Vector3 mCameraTarget
    {
      set
      {
        this.m_CameraPosition = value;
      }
      get
      {
        return this.m_CameraPosition;
      }
    }

    private bool mAllowCameraRotation
    {
      set
      {
        this.m_AllowCameraRotation = value;
      }
      get
      {
        return this.m_AllowCameraRotation;
      }
    }

    private bool mAllowCameraTranslation
    {
      set
      {
        this.m_AllowCameraTranslation = value;
      }
      get
      {
        return this.m_AllowCameraTranslation;
      }
    }

    private bool mDesiredCameraTargetSet
    {
      set
      {
        this.m_TargetCameraPositionInterp = value;
      }
      get
      {
        return this.m_TargetCameraPositionInterp;
      }
    }

    public float CameraYawRatio
    {
      get
      {
        return Mathf.Clamp01((float) (((double) this.m_CameraAngle - (double) this.m_CameraYawMin) / ((double) this.m_CameraYawMax - (double) this.m_CameraYawMin)));
      }
    }

    public void SetMoveCamera()
    {
      this.ResetMoveCamera();
    }

    private void SetCameraOffset(Transform transform)
    {
    }

    private void InterpCameraOffset(Transform transform)
    {
    }

    private void InitCamera()
    {
      GameSettings instance = GameSettings.Instance;
      CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
      RenderPipeline.Setup(Camera.get_main());
      this.m_TargetCamera = GameUtility.RequireComponent<TargetCamera>(((Component) Camera.get_main()).get_gameObject());
      this.m_TargetCamera.Pitch = instance.GameCamera_AngleX;
      this.m_DefaultCameraYawMin = instance.GameCamera_YawMin;
      this.m_DefaultCameraYawMax = instance.GameCamera_YawMax;
      this.m_CameraYawMin = this.m_DefaultCameraYawMin;
      this.m_CameraYawMax = this.m_DefaultCameraYawMax;
      this.m_CameraAngle = this.m_DefaultCameraYawMin;
      this.m_TargetCameraDistance = instance.GameCamera_DefaultDistance;
      this.m_TargetCameraDistanceInterp = true;
      this.m_NewCamera = false;
      this.SetFullRotationCamera(true);
    }

    private void DestroyCamera()
    {
      CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
    }

    public void ResetCameraTarget()
    {
      this.m_TargetCamera.Reset();
      if (!this.isUpView)
      {
        this.m_CameraAngle = this.m_TargetCamera.Yaw;
        this.m_TargetCameraDistance = this.m_TargetCamera.CameraDistance;
        this.m_TargetCameraDistanceInterp = false;
      }
      this.m_CameraPosition = this.m_TargetCamera.TargetPosition;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector3& local = @this.m_CameraPosition;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local).y = (__Null) ((^local).y - (double) GameSettings.Instance.GameCamera_UnitHeightOffset);
    }

    public void ResetMoveCamera()
    {
      this.InterpCameraTarget((Component) ((Component) this.FindUnitController(this.mBattle.CurrentUnit)).get_transform());
      this.m_TargetCamera.Pitch = GameSettings.Instance.GameCamera_AngleX;
    }

    private void UpdateCameraControl(bool immediate = false)
    {
      if (this.isBattleCamera)
        return;
      if (this.isUpView)
      {
        this.UpdateCameraControlUpView(immediate);
      }
      else
      {
        Camera main = Camera.get_main();
        Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null) ? (Transform) null : ((Component) main).get_transform();
        GameSettings instance = GameSettings.Instance;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsSceneRoot, (UnityEngine.Object) null) && ((Component) this.mTacticsSceneRoot).get_gameObject().get_activeInHierarchy())
          main.set_fieldOfView(GameSettings.Instance.GameCamera_TacticsSceneFOV);
        else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleSceneRoot, (UnityEngine.Object) null) && ((Component) this.mBattleSceneRoot).get_gameObject().get_activeInHierarchy())
          main.set_fieldOfView(GameSettings.Instance.GameCamera_BattleSceneFOV);
        if (!ObjectAnimator.Get((Component) main).isMoving && this.mUpdateCameraPosition)
        {
          bool flag = this.UpdateCameraRotationInterp(immediate);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null))
          {
            this.UpdateCameraRotationTouchMove(!flag, immediate);
            this.UpdateCameraPositionTouchMove(transform.get_right(), transform.get_forward());
          }
          this.UpdateCameraPositionInterp(immediate);
          if (this.m_TargetCameraDistanceInterp)
          {
            float num = !immediate ? Time.get_deltaTime() * 8f : 1f;
            float targetCameraDistance = this.m_TargetCameraDistance;
            this.m_TargetCamera.CameraDistance = Mathf.Lerp(this.m_TargetCamera.CameraDistance, targetCameraDistance, num);
            if ((double) Mathf.Abs(targetCameraDistance - this.m_TargetCamera.CameraDistance) <= 0.00999999977648258)
            {
              this.m_TargetCamera.CameraDistance = targetCameraDistance;
              this.m_TargetCameraDistanceInterp = false;
            }
          }
          this.m_TargetCamera.SetPositionYaw(Vector3.op_Addition(this.m_CameraPosition, Vector3.op_Multiply(Vector3.get_up(), instance.GameCamera_UnitHeightOffset)), this.m_CameraAngle);
        }
        this.OnCameraForcus();
      }
    }

    private bool UpdateCameraRotationTouchMove(bool isEnable, bool immediate)
    {
      bool flag = false;
      if (isEnable && this.m_AllowCameraRotation && this.IsControlBattleUI(SceneBattle.eMaskBattleUI.CAMERA))
      {
        float num1 = (float) -this.mTouchController.AngularVelocity.x * (float) (1.0 / (double) Screen.get_width() * 180.0);
        if (!this.isNewCamera)
        {
          float cameraYawSoftLimit = GameSettings.Instance.GameCamera_YawSoftLimit;
          float num2 = 2f;
          if ((double) this.m_CameraAngle < (double) this.m_CameraYawMin && (double) num1 < 0.0)
          {
            float num3 = Mathf.Pow(1f - Mathf.Clamp01((float) -((double) this.m_CameraAngle - (double) this.m_CameraYawMin) / cameraYawSoftLimit), num2);
            num1 *= num3;
          }
          else if ((double) this.m_CameraAngle > (double) this.m_CameraYawMax && (double) num1 > 0.0)
          {
            float num3 = Mathf.Pow(1f - Mathf.Clamp01((this.m_CameraAngle - this.m_CameraYawMax) / cameraYawSoftLimit), num2);
            num1 *= num3;
          }
          this.m_CameraAngle += num1;
          if (!SRPG_TouchInputModule.IsMultiTouching)
          {
            float num3 = !immediate ? Time.get_deltaTime() * 10f : 1f;
            if ((double) this.m_CameraAngle < (double) this.m_CameraYawMin)
              this.m_CameraAngle = (double) Mathf.Abs(this.m_CameraAngle - this.m_CameraYawMin) >= 0.00999999977648258 ? Mathf.Lerp(this.m_CameraAngle, this.m_CameraYawMin, num3) : this.m_CameraYawMin;
            else if ((double) this.m_CameraAngle > (double) this.m_CameraYawMax)
              this.m_CameraAngle = (double) Mathf.Abs(this.m_CameraAngle - this.m_CameraYawMax) >= 0.00999999977648258 ? Mathf.Lerp(this.m_CameraAngle, this.m_CameraYawMax, num3) : this.m_CameraYawMax;
          }
        }
        else
        {
          this.m_CameraAngle += num1;
          if (!this.isFullRotationCamera)
          {
            if ((double) this.m_CameraAngle < (double) this.m_CameraYawMin)
              this.m_CameraAngle = this.m_CameraYawMin;
            else if ((double) this.m_CameraAngle > (double) this.m_CameraYawMax)
              this.m_CameraAngle = this.m_CameraYawMax;
          }
        }
        flag = true;
      }
      this.mTouchController.AngularVelocity = Vector2.get_zero();
      return flag;
    }

    private bool UpdateCameraPositionTouchMove(Vector3 xAxis, Vector3 yAxis)
    {
      Camera main = Camera.get_main();
      bool flag = false;
      // ISSUE: explicit reference operation
      if (this.m_AllowCameraTranslation && (double) ((Vector2) @this.mTouchController.Velocity).get_magnitude() > 0.0 && this.IsControlBattleUI(SceneBattle.eMaskBattleUI.CAMERA))
      {
        Vector2 velocity = this.mTouchController.Velocity;
        yAxis.y = (__Null) 0.0;
        // ISSUE: explicit reference operation
        ((Vector3) @yAxis).Normalize();
        xAxis.y = (__Null) 0.0;
        // ISSUE: explicit reference operation
        ((Vector3) @xAxis).Normalize();
        Vector3 screenPoint = main.WorldToScreenPoint(this.m_CameraPosition);
        Vector2 vector2 = Vector2.op_Implicit(Vector3.op_Subtraction(main.WorldToScreenPoint(Vector3.op_Addition(Vector3.op_Addition(this.m_CameraPosition, xAxis), yAxis)), screenPoint));
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @velocity;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local1).x = (__Null) ((^local1).x / (double) Mathf.Abs((float) vector2.x));
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @velocity;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local2).y = (__Null) ((^local2).y / (double) Mathf.Abs((float) vector2.y));
        // ISSUE: explicit reference operation
        ((Vector2) @velocity).\u002Ector((float) (xAxis.x * velocity.x + yAxis.x * velocity.y), (float) (xAxis.z * velocity.x + yAxis.z * velocity.y));
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local3 = @this.m_CameraPosition;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local3).x = (^local3).x - velocity.x;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local4 = @this.m_CameraPosition;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local4).z = (^local4).z - velocity.y;
        this.m_CameraPosition.x = (__Null) (double) Mathf.Clamp((float) this.m_CameraPosition.x, 0.1f, (float) this.mBattle.CurrentMap.Width - 0.1f);
        this.m_CameraPosition.z = (__Null) (double) Mathf.Clamp((float) this.m_CameraPosition.z, 0.1f, (float) this.mBattle.CurrentMap.Height - 0.1f);
        this.m_CameraPosition.y = (__Null) (double) this.CalcHeight((float) this.m_CameraPosition.x, (float) this.m_CameraPosition.z);
        flag = true;
      }
      this.mTouchController.Velocity = Vector2.get_zero();
      return flag;
    }

    private bool UpdateCameraRotationInterp(bool immediate)
    {
      if (!this.m_TargetCameraAngleInterp)
        return false;
      this.m_TargetCameraAngleTime = !immediate ? Mathf.Min(this.m_TargetCameraAngleTime + Time.get_deltaTime(), this.m_TargetCameraAngleTimeMax) : this.m_TargetCameraAngleTimeMax;
      this.m_CameraAngle = Mathf.Lerp(this.m_TargetCameraAngleStart, this.m_TargetCameraAngle, Mathf.Sin((float) (1.57079637050629 * ((double) this.m_TargetCameraAngleTime / (double) this.m_TargetCameraAngleTimeMax))));
      if ((double) this.m_TargetCameraAngleTime >= (double) this.m_TargetCameraAngleTimeMax)
        this.m_TargetCameraAngleInterp = false;
      return true;
    }

    private bool UpdateCameraPositionInterp(bool immediate)
    {
      float num = !immediate ? Time.get_deltaTime() * 8f : 1f;
      if (!this.m_TargetCameraPositionInterp)
        return false;
      this.m_CameraPosition = Vector3.Lerp(this.m_CameraPosition, this.m_TargetCameraPosition, num);
      Vector3 vector3 = Vector3.op_Subtraction(this.m_TargetCameraPosition, this.m_CameraPosition);
      // ISSUE: explicit reference operation
      if ((double) ((Vector3) @vector3).get_magnitude() <= 0.00999999977648258)
      {
        this.m_CameraPosition = this.m_TargetCameraPosition;
        this.m_TargetCameraPositionInterp = false;
      }
      return true;
    }

    public void SetBattleCamera(bool value)
    {
      this.m_BattleCamera = value;
    }

    public void SetNewCamera(TacticsSceneCamera camera)
    {
      this.m_NewCamera = true;
      this.m_FullRotationCamera = camera.allRange.enable;
      if (!this.m_FullRotationCamera && camera.moveRange.isOverride)
      {
        this.m_CameraYawMin = 360f - camera.moveRange.min;
        this.m_CameraYawMax = 360f - camera.moveRange.max;
        this.m_CameraAngle = 360f - camera.moveRange.start;
        if ((double) this.m_CameraYawMin <= (double) this.m_CameraYawMax)
          return;
        float cameraYawMin = this.m_CameraYawMin;
        this.m_CameraYawMin = this.m_CameraYawMax;
        this.m_CameraYawMax = cameraYawMin;
      }
      else
      {
        this.m_CameraYawMin = this.m_DefaultCameraYawMin;
        this.m_CameraYawMax = this.m_DefaultCameraYawMax;
        this.m_CameraAngle = this.m_DefaultCameraYawMin;
      }
    }

    public void SetFullRotationCamera(bool value)
    {
      if (value)
      {
        this.m_NewCamera = true;
      }
      else
      {
        this.m_CameraYawMin = this.m_DefaultCameraYawMin;
        this.m_CameraYawMax = this.m_DefaultCameraYawMax;
        this.m_CameraAngle = this.m_DefaultCameraYawMin;
        this.m_NewCamera = false;
      }
      this.m_FullRotationCamera = value;
    }

    public float GetCameraDistance()
    {
      return this.m_TargetCameraDistance;
    }

    private void SetCameraTarget(Vector3 position)
    {
      this.m_CameraPosition = position;
      this.m_TargetCameraPositionInterp = false;
    }

    private void SetCameraTarget(Component position)
    {
      this.SetCameraTarget(position.get_transform().get_position());
    }

    private void SetCameraTarget(float x, float y)
    {
      this.m_CameraPosition.x = (__Null) (double) x;
      this.m_CameraPosition.y = (__Null) (double) this.CalcHeight(x, y);
      this.m_CameraPosition.z = (__Null) (double) y;
    }

    public void InterpCameraTarget(Vector3 position)
    {
      this.m_TargetCameraPosition = position;
      this.m_TargetCameraPositionInterp = true;
      this.mUpdateCameraPosition = true;
    }

    private void InterpCameraTarget(Component position)
    {
      this.InterpCameraTarget(position.get_transform().get_position());
    }

    private void InterpCameraTargetToCurrent()
    {
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      this.InterpCameraTarget((Component) unitController);
    }

    public void InterpCameraDistance(float distance)
    {
      this.m_TargetCameraDistance = distance;
      this.m_TargetCameraDistanceInterp = true;
    }

    public void SetCameraYawRange(float min, float max)
    {
      this.m_DefaultCameraYawMin = min;
      this.m_DefaultCameraYawMax = max;
    }

    public void RotateCamera(float delta, float duration)
    {
      if (!this.IsControlBattleUI(SceneBattle.eMaskBattleUI.CAMERA) || !this.mUpdateCameraPosition || (this.isBattleCamera || this.isUpView) || ObjectAnimator.Get((Component) Camera.get_main()).isMoving)
        return;
      float num1 = this.isNewCamera ? ((double) delta >= 0.0 ? 45f : -45f) : delta * (this.m_CameraYawMax - this.m_CameraYawMin);
      if (this.m_TargetCameraAngleInterp)
      {
        this.m_TargetCameraAngleStart = this.m_CameraAngle;
        this.m_TargetCameraAngle += num1;
      }
      else
      {
        this.m_TargetCameraAngleStart = this.m_CameraAngle;
        this.m_TargetCameraAngleInterp = true;
        this.m_TargetCameraAngle = this.m_CameraAngle + num1;
      }
      if (this.isNewCamera)
      {
        if ((double) num1 > 0.0)
          this.m_TargetCameraAngle = (float) Mathf.FloorToInt((float) (((double) this.m_TargetCameraAngle + 1.0) / 45.0)) * 45f;
        else if ((double) num1 < 0.0)
          this.m_TargetCameraAngle = (float) Mathf.CeilToInt((float) (((double) this.m_TargetCameraAngle - 1.0) / 45.0)) * 45f;
      }
      float num2 = duration;
      duration = Mathf.Abs(this.m_TargetCameraAngle - this.m_TargetCameraAngleStart) * (duration / 45f);
      if ((double) duration > (double) num2)
        duration = num2;
      else if ((double) duration < 0.100000001490116)
        duration = 0.1f;
      if (!this.isNewCamera)
        this.m_TargetCameraAngle = Mathf.Clamp(this.m_TargetCameraAngle, this.m_CameraYawMin, this.m_CameraYawMax);
      else if (!this.isFullRotationCamera)
        this.m_TargetCameraAngle = Mathf.Clamp(this.m_TargetCameraAngle, this.m_CameraYawMin, this.m_CameraYawMax);
      else if ((double) this.m_TargetCameraAngle >= 360.0)
      {
        this.m_CameraAngle -= 360f;
        this.m_TargetCameraAngleStart -= 360f;
        this.m_TargetCameraAngle -= 360f;
      }
      else if ((double) this.m_TargetCameraAngle < 0.0)
      {
        this.m_CameraAngle += 360f;
        this.m_TargetCameraAngleStart += 360f;
        this.m_TargetCameraAngle += 360f;
      }
      this.m_TargetCameraAngleTime = 0.0f;
      this.m_TargetCameraAngleTimeMax = duration;
    }

    public float GetCameraAngle()
    {
      return this.m_CameraAngle;
    }

    public void GetCameraTargetView(out Vector3 center, out float distance, Vector3[] targets)
    {
      Camera main = Camera.get_main();
      float cameraDistance = this.m_TargetCamera.CameraDistance;
      Vector3 targetPosition = this.m_TargetCamera.TargetPosition;
      distance = cameraDistance;
      if (targets.Length == 1)
      {
        center = targets[0];
      }
      else
      {
        // ISSUE: explicit reference operation
        ((Vector3) @center).\u002Ector(0.0f, float.MaxValue, 0.0f);
        for (int index = 0; index < targets.Length; ++index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local1 = @center;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local1).x = (^local1).x + targets[index].x;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local2 = @center;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local2).z = (^local2).z + targets[index].z;
          center.y = (__Null) (double) Mathf.Min((float) targets[index].y, (float) center.y);
        }
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local3 = @center;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local3).x = (__Null) ((^local3).x / (double) targets.Length);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local4 = @center;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local4).y = (__Null) ((^local4).y - 0.5);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local5 = @center;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local5).z = (__Null) ((^local5).z / (double) targets.Length);
        bool flag;
        do
        {
          flag = false;
          this.m_TargetCamera.CameraDistance = distance;
          this.m_TargetCamera.SetPositionYaw(center, this.m_CameraAngle);
          for (int index = 0; index < targets.Length; ++index)
          {
            Vector3 viewportPoint = main.WorldToViewportPoint(targets[index]);
            if (viewportPoint.x < 0.0 || viewportPoint.x > 1.0 || (viewportPoint.y < 0.0 || viewportPoint.y > 0.899999976158142))
            {
              ++distance;
              flag = true;
              break;
            }
          }
        }
        while (flag && (double) distance < (double) GameSettings.Instance.GameCamera_MaxDistance);
        center = Vector3.op_Subtraction(center, Vector3.op_Multiply(Vector3.get_up(), GameSettings.Instance.GameCamera_UnitHeightOffset));
        this.m_TargetCamera.CameraDistance = cameraDistance;
        this.m_TargetCamera.TargetPosition = targetPosition;
      }
    }

    private void OnCameraPreCull(Camera cam)
    {
      if (!(((Component) cam).get_tag() == "MainCamera") || ((Component) cam).get_gameObject().get_layer() != GameUtility.LayerDefault)
        return;
      this.LayoutPopups(cam);
      this.LayoutGauges(cam);
    }

    private void OnCameraForcus()
    {
      if (this.mOnUnitFocus != null)
      {
        if (this.m_TargetCameraPositionInterp)
          return;
        TacticsUnitController closestUnitController = this.FindClosestUnitController(this.m_CameraPosition, 1f);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mFocusedUnit, (UnityEngine.Object) closestUnitController))
          return;
        DebugUtility.Log("Focus:" + (object) closestUnitController);
        this.mFocusedUnit = closestUnitController;
        this.mOnUnitFocus(this.mFocusedUnit);
      }
      else
        this.mFocusedUnit = (TacticsUnitController) null;
    }

    private void UpdateCameraControlUpView(bool immediate = false)
    {
      Camera main = Camera.get_main();
      Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null) ? (Transform) null : ((Component) main).get_transform();
      GameSettings instance = GameSettings.Instance;
      main.set_fieldOfView(GameSettings.Instance.GameCamera_TacticsSceneFOV);
      if (this.mUpdateCameraPosition)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null) && !ObjectAnimator.Get((Component) main).isMoving)
          this.UpdateCameraPositionTouchMove(transform.get_right(), transform.get_up());
        this.UpdateCameraPositionInterp(immediate);
        if (this.m_TargetCameraDistanceInterp)
        {
          float num1 = !immediate ? Time.get_deltaTime() * 8f : 1f;
          float num2 = 5f + this.m_TargetCameraDistance;
          this.m_TargetCamera.CameraDistance = Mathf.Lerp(this.m_TargetCamera.CameraDistance, num2, num1);
          if ((double) Mathf.Abs(num2 - this.m_TargetCamera.CameraDistance) <= 0.00999999977648258)
          {
            this.m_TargetCamera.CameraDistance = num2;
            this.m_TargetCameraDistanceInterp = false;
          }
        }
        this.m_TargetCamera.SetPositionYawPitch(Vector3.op_Addition(this.m_CameraPosition, Vector3.op_Multiply(Vector3.get_up(), instance.GameCamera_UnitHeightOffset)), 90f, 270f);
      }
      this.OnCameraForcus();
    }

    public void OnCameraModeChange(SceneBattle.CameraMode nextMode)
    {
      if (this.m_CameraMode == nextMode)
        return;
      GameSettings instance = GameSettings.Instance;
      this.m_CameraMode = nextMode;
      if (this.m_TargetCameraPositionInterp)
      {
        this.m_CameraPosition = this.m_TargetCameraPosition;
        this.m_TargetCameraPositionInterp = false;
      }
      if (this.m_TargetCameraAngleInterp)
      {
        this.m_CameraAngle = this.m_TargetCameraAngle;
        this.m_TargetCameraAngleInterp = false;
      }
      if (nextMode == SceneBattle.CameraMode.DEFAULT)
      {
        this.m_CameraAngle = this.m_CameraYaw;
        this.m_TargetCameraDistanceInterp = false;
        this.m_TargetCamera.CameraDistance = this.m_TargetCameraDistance;
        this.m_TargetCamera.SetPositionYawPitch(Vector3.op_Addition(this.m_CameraPosition, Vector3.op_Multiply(Vector3.get_up(), instance.GameCamera_UnitHeightOffset)), this.m_CameraAngle, instance.GameCamera_AngleX);
      }
      else
      {
        if (nextMode != SceneBattle.CameraMode.UPVIEW)
          return;
        this.m_CameraYaw = this.m_CameraAngle;
        this.m_TargetCameraDistanceInterp = false;
        this.m_TargetCamera.CameraDistance = 5f + this.m_TargetCameraDistance;
        this.m_TargetCamera.SetPositionYawPitch(Vector3.op_Addition(this.m_CameraPosition, Vector3.op_Multiply(Vector3.get_up(), instance.GameCamera_UnitHeightOffset)), 90f, 270f);
      }
    }

    public bool AudiencePause
    {
      get
      {
        return this.mAudiencePause;
      }
      set
      {
        this.mAudiencePause = value;
      }
    }

    public bool AudienceSkip
    {
      get
      {
        return this.mAudienceSkip;
      }
      set
      {
        this.mAudienceSkip = value;
      }
    }

    private void MultiPlayLog(string str)
    {
    }

    public int MultiPlayerCount
    {
      get
      {
        return this.mMultiPlayer.Count;
      }
    }

    private float MultiPlayInputTimeLimit { get; set; }

    private bool MultiPlayExtMoveInputTime { get; set; }

    private bool MultiPlayExtSelectInputTime { get; set; }

    public float MultiPlayAddInputTime { get; set; }

    public bool ResumeSuccess
    {
      get
      {
        return this.mResumeSuccess;
      }
      set
      {
        this.mResumeSuccess = value;
      }
    }

    public bool ResumeOnly
    {
      get
      {
        return this.mResumeOnlyPlayer;
      }
    }

    public bool VersusMapView
    {
      get
      {
        return this.mMapViewMode;
      }
      set
      {
        this.mMapViewMode = value;
      }
    }

    public bool AlreadyEndBattle
    {
      get
      {
        return this.mAlreadyEndBattle;
      }
      set
      {
        this.mAlreadyEndBattle = value;
      }
    }

    public bool IsExistResume
    {
      get
      {
        if (this.mRecvResumeRequest != null)
          return this.mRecvResumeRequest.Count > 0;
        return false;
      }
    }

    public bool AudienceForceEnd
    {
      get
      {
        return this.mAudienceForceEnd;
      }
      set
      {
        this.mAudienceForceEnd = value;
      }
    }

    public bool IsSend
    {
      get
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        return (instance.AudienceMode ? 1 : (instance.IsVSCpuBattle ? 1 : 0)) == 0;
      }
    }

    public void ExtentionMultiInputTime(bool bMove)
    {
      if ((double) this.MultiPlayInputTimeLimit <= 0.0)
        return;
      if (bMove && !this.MultiPlayExtMoveInputTime)
      {
        this.MultiPlayInputTimeLimit += this.MULTI_PLAY_INPUT_EXT_MOVE;
        this.MultiPlayAddInputTime = this.MULTI_PLAY_INPUT_EXT_MOVE;
        this.MultiPlayExtMoveInputTime = true;
        this.mBattleUI_MultiPlay.OnExtInput();
      }
      if (bMove || this.MultiPlayExtSelectInputTime)
        return;
      this.MultiPlayInputTimeLimit += this.MULTI_PLAY_INPUT_EXT_SELECT;
      this.MultiPlayAddInputTime = this.MULTI_PLAY_INPUT_EXT_SELECT;
      this.MultiPlayExtSelectInputTime = true;
      this.mBattleUI_MultiPlay.OnExtInput();
    }

    private void CreateMultiPlayer()
    {
      if (!this.Battle.IsMultiPlay)
        return;
      this.mMultiPlayer = new List<SceneBattle.MultiPlayer>();
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
      for (int index = 0; index < myPlayersStarted.Count; ++index)
      {
        JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted[index];
        if (photonPlayerParam.playerIndex != instance.MyPlayerIndex)
          this.mMultiPlayer.Add(new SceneBattle.MultiPlayer(this, photonPlayerParam.playerIndex, photonPlayerParam.playerID));
      }
      this.mMultiPlayerUnit = new List<SceneBattle.MultiPlayerUnit>();
      for (int unitID = 0; unitID < this.Battle.AllUnits.Count; ++unitID)
      {
        Unit allUnit = this.Battle.AllUnits[unitID];
        if (allUnit != null)
        {
          int playerIndex = allUnit.OwnerPlayerIndex;
          if (playerIndex != instance.MyPlayerIndex && playerIndex > 0)
          {
            SceneBattle.MultiPlayer owner = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == playerIndex));
            if (owner != null)
              this.mMultiPlayerUnit.Add(new SceneBattle.MultiPlayerUnit(this, unitID, allUnit, owner));
          }
        }
      }
      this.mSendList = new List<SceneBattle.MultiPlayInput>();
      this.mResumeMultiPlay = instance.IsResume();
      this.mResumeAlreadyStartPlayer.Clear();
    }

    private void CreateAudiencePlayer()
    {
      if (!MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      this.mMultiPlayer = new List<SceneBattle.MultiPlayer>();
      List<JSON_MyPhotonPlayerParam> mAudiencePlayers = this.mAudiencePlayers;
      for (int index = 0; index < mAudiencePlayers.Count; ++index)
      {
        JSON_MyPhotonPlayerParam photonPlayerParam = mAudiencePlayers[index];
        this.mMultiPlayer.Add(new SceneBattle.MultiPlayer(this, photonPlayerParam.playerIndex, photonPlayerParam.playerID));
      }
      this.mMultiPlayerUnit = new List<SceneBattle.MultiPlayerUnit>();
      for (int unitID = 0; unitID < this.Battle.AllUnits.Count; ++unitID)
      {
        Unit allUnit = this.Battle.AllUnits[unitID];
        if (allUnit != null)
        {
          int playerIndex = allUnit.OwnerPlayerIndex;
          if (playerIndex > 0)
          {
            SceneBattle.MultiPlayer owner = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == playerIndex));
            if (owner != null)
              this.mMultiPlayerUnit.Add(new SceneBattle.MultiPlayerUnit(this, unitID, allUnit, owner));
          }
        }
      }
      this.mSendList = new List<SceneBattle.MultiPlayInput>();
    }

    private void CreateCpuBattlePlayer()
    {
      if (!MonoSingleton<GameManager>.Instance.IsVSCpuBattle)
        return;
      this.mMultiPlayer = new List<SceneBattle.MultiPlayer>();
      for (int index = 0; index < this.CPUBATTLE_PLAYER_NUM; ++index)
        this.mMultiPlayer.Add(new SceneBattle.MultiPlayer(this, index + 1, index + 1));
      this.mMultiPlayerUnit = new List<SceneBattle.MultiPlayerUnit>();
      for (int unitID = 0; unitID < this.Battle.AllUnits.Count; ++unitID)
      {
        Unit allUnit = this.Battle.AllUnits[unitID];
        if (allUnit != null)
        {
          int playerIndex = allUnit.OwnerPlayerIndex;
          if (playerIndex > 0)
          {
            SceneBattle.MultiPlayer owner = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == playerIndex));
            if (owner != null)
              this.mMultiPlayerUnit.Add(new SceneBattle.MultiPlayerUnit(this, unitID, allUnit, owner));
          }
        }
      }
      this.mSendList = new List<SceneBattle.MultiPlayInput>();
    }

    private void BeginMultiPlayer()
    {
      if (!this.Battle.IsMultiPlay)
        return;
      if (this.mBeginMultiPlay)
      {
        this.MultiPlayLog("[PUN]already BeginMultiPlayer");
      }
      else
      {
        this.mBeginMultiPlay = true;
        this.mSendList.Clear();
        this.mSendTime = 0.0f;
        this.MultiPlayInputTimeLimit = 0.0f;
        this.MultiPlayExtMoveInputTime = false;
        this.MultiPlayExtSelectInputTime = false;
        this.Battle.EntryBattleMultiPlayTimeUp = false;
        this.Battle.MultiPlayDisconnectAutoBattle = false;
        this.mPrevGridX = -1;
        this.mPrevGridY = -1;
        this.mPrevDir = EUnitDirection.Auto;
        Unit unit = this.Battle.CurrentUnit;
        this.mCurrentSendInputUnitID = this.Battle.AllUnits.FindIndex((Predicate<Unit>) (u => u == unit));
        this.MultiPlayLog("[PUN]BeginMultiPlayer********** turn:" + (object) this.UnitStartCountTotal + " unitID:" + (object) this.mCurrentSendInputUnitID + " sqID:" + (object) this.mMultiPlaySendID);
        SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == unit.OwnerPlayerIndex));
        if (multiPlayer != null)
          multiPlayer.Begin(this);
        SceneBattle.MultiPlayerUnit multiPlayerUnit = this.mMultiPlayerUnit.Find((Predicate<SceneBattle.MultiPlayerUnit>) (u => u.Unit == unit));
        if (multiPlayerUnit != null)
          multiPlayerUnit.Begin(this);
        if (unit.OwnerPlayerIndex == this.Battle.MyPlayerIndex)
          this.MultiPlayInputTimeLimit = this.MULTI_PLAY_INPUT_TIME_LIMIT_SEC;
        if (this.Battle.IsMultiPlay)
          this.CloseBattleUI();
        MyPhoton instance1 = PunMonoSingleton<MyPhoton>.Instance;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance1, (UnityEngine.Object) null) && multiPlayer != null && (instance1.IsOldestPlayer() && multiPlayer.Disconnected))
          this.SendOtherPlayerDisconnect(multiPlayer.PlayerIndex);
        GameManager instance2 = MonoSingleton<GameManager>.Instance;
        if (!instance2.AudienceMode)
          return;
        if (multiPlayer != null)
          multiPlayer.Disconnected = false;
        if (!instance2.AudienceManager.IsSkipEnd)
          return;
        this.mBattleUI_MultiPlay.OnAudienceMode();
      }
    }

    private void EndMultiPlayer()
    {
      if (!this.Battle.IsMultiPlay)
        return;
      if (!this.mBeginMultiPlay)
      {
        this.MultiPlayLog("[PUN]not begin EndMultiPlayer");
      }
      else
      {
        this.mBeginMultiPlay = false;
        this.MultiPlayLog("[PUN]EndMultiPlayer*******");
        for (int index = 0; index < this.mMultiPlayerUnit.Count; ++index)
          this.mMultiPlayerUnit[index].End(this);
        for (int index = 0; index < this.mMultiPlayer.Count; ++index)
          this.mMultiPlayer[index].End(this);
        this.MultiPlayInputTimeLimit = 0.0f;
      }
    }

    private byte[] CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader header, int unitID, List<SceneBattle.MultiPlayInput> sendList)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
      int myPlayerIndex = instance.MyPlayerIndex;
      int num = myPlayer != null ? myPlayer.playerID : 0;
      if (sendList == null)
        sendList = new List<SceneBattle.MultiPlayInput>();
      if (sendList.Count <= 0)
        sendList.Add(new SceneBattle.MultiPlayInput()
        {
          c = 0
        });
      List<SceneBattle.MultiPlayInput> multiPlayInputList1 = new List<SceneBattle.MultiPlayInput>();
      if (header == SceneBattle.EMultiPlayRecvDataHeader.INPUT)
      {
        List<SceneBattle.MultiPlayInput> multiPlayInputList2 = new List<SceneBattle.MultiPlayInput>();
        SceneBattle.MultiPlayInput multiPlayInput = (SceneBattle.MultiPlayInput) null;
        using (List<SceneBattle.MultiPlayInput>.Enumerator enumerator = sendList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SceneBattle.MultiPlayInput current = enumerator.Current;
            if (current.c == 2)
            {
              multiPlayInput = current;
            }
            else
            {
              if (multiPlayInput != null)
              {
                multiPlayInputList2.Add(multiPlayInput);
                multiPlayInput = (SceneBattle.MultiPlayInput) null;
              }
              multiPlayInputList2.Add(current);
            }
          }
        }
        using (List<SceneBattle.MultiPlayInput>.Enumerator enumerator = sendList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SceneBattle.MultiPlayInput input = enumerator.Current;
            if (input.c == 6)
            {
              multiPlayInputList1.Add(input);
            }
            else
            {
              multiPlayInputList1.RemoveAll((Predicate<SceneBattle.MultiPlayInput>) (d => d.c == input.c));
              multiPlayInputList1.Add(input);
            }
          }
        }
        if (multiPlayInput != null)
        {
          multiPlayInputList2.Add(multiPlayInput);
          this.MultiPlayLog("send lastMove x:" + (object) multiPlayInput.x + " z:" + (object) multiPlayInput.z);
        }
        sendList = multiPlayInputList1;
      }
      SceneBattle.MultiPlayRecvData multiPlayRecvData = new SceneBattle.MultiPlayRecvData();
      SceneBattle.MultiPlayInput def = new SceneBattle.MultiPlayInput();
      multiPlayRecvData.h = (int) header;
      multiPlayRecvData.b = this.UnitStartCountTotal;
      multiPlayRecvData.sq = this.mMultiPlaySendID;
      multiPlayRecvData.pidx = myPlayerIndex;
      multiPlayRecvData.pid = num;
      multiPlayRecvData.uid = unitID;
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.c != def.c)) != null)
      {
        multiPlayRecvData.c = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.c[index] = sendList[index].c;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.u != def.u)) != null)
      {
        multiPlayRecvData.u = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.u[index] = sendList[index].u;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => !s.s.Equals(def.s))) != null)
      {
        multiPlayRecvData.s = new string[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.s[index] = sendList[index].s;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => !s.i.Equals(def.i))) != null)
      {
        multiPlayRecvData.i = new string[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.i[index] = sendList[index].i;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.gx != def.gx)) != null)
      {
        multiPlayRecvData.gx = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.gx[index] = sendList[index].gx;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.gy != def.gy)) != null)
      {
        multiPlayRecvData.gy = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.gy[index] = sendList[index].gy;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.ul != def.ul)) != null)
      {
        multiPlayRecvData.ul = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.ul[index] = sendList[index].ul;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.d != def.d)) != null)
      {
        multiPlayRecvData.d = new int[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.d[index] = sendList[index].d;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.x != (double) def.x)) != null)
      {
        multiPlayRecvData.x = new float[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.x[index] = sendList[index].x;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.z != (double) def.z)) != null)
      {
        multiPlayRecvData.z = new float[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.z[index] = sendList[index].z;
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.r != (double) def.r)) != null)
      {
        multiPlayRecvData.r = new float[sendList.Count];
        for (int index = 0; index < sendList.Count; ++index)
          multiPlayRecvData.r[index] = sendList[index].r;
      }
      return GameUtility.Object2Binary<SceneBattle.MultiPlayRecvData>(multiPlayRecvData);
    }

    private string CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader header, int unitID, List<SceneBattle.MultiPlayInput> sendList)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      int myPlayerIndex = instance.MyPlayerIndex;
      MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
      int num1 = myPlayer != null ? myPlayer.playerID : 0;
      if (sendList == null)
        sendList = new List<SceneBattle.MultiPlayInput>();
      if (sendList.Count <= 0)
        sendList.Add(new SceneBattle.MultiPlayInput()
        {
          c = 0
        });
      List<SceneBattle.MultiPlayInput> multiPlayInputList1 = new List<SceneBattle.MultiPlayInput>();
      if (header == SceneBattle.EMultiPlayRecvDataHeader.INPUT)
      {
        List<SceneBattle.MultiPlayInput> multiPlayInputList2 = new List<SceneBattle.MultiPlayInput>();
        SceneBattle.MultiPlayInput multiPlayInput = (SceneBattle.MultiPlayInput) null;
        using (List<SceneBattle.MultiPlayInput>.Enumerator enumerator = sendList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SceneBattle.MultiPlayInput current = enumerator.Current;
            if (current.c == 2)
            {
              multiPlayInput = current;
            }
            else
            {
              if (multiPlayInput != null)
              {
                multiPlayInputList2.Add(multiPlayInput);
                multiPlayInput = (SceneBattle.MultiPlayInput) null;
              }
              multiPlayInputList2.Add(current);
            }
          }
        }
        using (List<SceneBattle.MultiPlayInput>.Enumerator enumerator = sendList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SceneBattle.MultiPlayInput input = enumerator.Current;
            if (input.c == 6)
            {
              multiPlayInputList1.Add(input);
            }
            else
            {
              multiPlayInputList1.RemoveAll((Predicate<SceneBattle.MultiPlayInput>) (d => d.c == input.c));
              multiPlayInputList1.Add(input);
            }
          }
        }
        if (multiPlayInput != null)
        {
          multiPlayInputList2.Add(multiPlayInput);
          this.MultiPlayLog("send lastMove x:" + (object) multiPlayInput.x + " z:" + (object) multiPlayInput.z);
        }
        sendList = multiPlayInputList1;
      }
      SceneBattle.MultiPlayInput def = new SceneBattle.MultiPlayInput();
      ++this.mMultiPlaySendID;
      string str1 = "{\"h\":" + (object) header + ",\"b\":" + (object) this.UnitStartCountTotal + ",\"sq\":" + (object) this.mMultiPlaySendID + ",\"pidx\":" + (object) myPlayerIndex + ",\"pid\":" + (object) num1 + ",\"uid\":" + (object) unitID;
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.c != def.c)) != null)
      {
        string str2 = str1 + ",\"c\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].c;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.u != def.u)) != null)
      {
        string str2 = str1 + ",\"u\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].u;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => !s.s.Equals(def.s))) != null)
      {
        string str2 = str1 + ",\"s\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? ",\"" : "\"") + JsonEscape.Escape(sendList[index].s) + "\"";
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => !s.i.Equals(def.i))) != null)
      {
        string str2 = str1 + ",\"i\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? ",\"" : "\"") + JsonEscape.Escape(sendList[index].i) + "\"";
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.gx != def.gx)) != null)
      {
        string str2 = str1 + ",\"gx\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].gx;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.gy != def.gy)) != null)
      {
        string str2 = str1 + ",\"gy\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].gy;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.ul != def.ul)) != null)
      {
        string str2 = str1 + ",\"ul\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].ul;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => s.d != def.d)) != null)
      {
        string str2 = str1 + ",\"d\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].d;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.x != (double) def.x)) != null)
      {
        string str2 = str1 + ",\"x\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].x;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.z != (double) def.z)) != null)
      {
        string str2 = str1 + ",\"z\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].z;
        str1 = str2 + "]";
      }
      if (sendList.Find((Predicate<SceneBattle.MultiPlayInput>) (s => (double) s.r != (double) def.r)) != null)
      {
        string str2 = str1 + ",\"r\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].r;
        str1 = str2 + "]";
      }
      string str3 = str1 + "}";
      this.MultiPlayLog("[PUN] send packet sq:" + (object) this.mMultiPlaySendID + " pid:" + (object) num1 + " pidx:" + (object) myPlayerIndex + " h:" + (object) header + " b:" + (object) this.UnitStartCountTotal);
      if (header == SceneBattle.EMultiPlayRecvDataHeader.INPUT)
      {
        int num2 = 0;
        while (num2 < sendList.Count)
          ++num2;
      }
      Debug.LogWarning((object) ("SendJson:" + (object) str3.Length));
      return str3;
    }

    private bool GainMultiPlayInputTimeLimit()
    {
      if (!this.IsInState<SceneBattle.State_MapMoveSelect_Stick>() && !this.IsInState<SceneBattle.State_SelectItemV2>() && (!this.IsInState<SceneBattle.State_SelectSkillV2>() && !this.IsInState<SceneBattle.State_SelectGridEventV2>()) && (!this.IsInState<SceneBattle.State_SelectTargetV2>() && !this.IsInState<SceneBattle.State_InputDirection>() && (!this.IsInState<SceneBattle.State_MapWait>() && !this.IsInState<SceneBattle.State_MapCommandV2>())))
        return false;
      Unit currentUnit = this.Battle.CurrentUnit;
      if (currentUnit.CastSkill != null && currentUnit.CastSkill.CastType == ECastTypes.Jump)
        return false;
      TacticsUnitController unitController = this.FindUnitController(currentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.IsJumpCant())
        return false;
      string s = FlowNode_Variable.Get("DisableTimeLimit");
      return (string.IsNullOrEmpty(s) || long.Parse(s) == 0L) && currentUnit.IsControl;
    }

    private TacticsUnitController ResetMultiPlayerTransform(Unit unit)
    {
      TacticsUnitController tacticsUnitController = unit != null ? this.FindUnitController(unit) : (TacticsUnitController) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) tacticsUnitController, (UnityEngine.Object) null))
        return (TacticsUnitController) null;
      ((Component) tacticsUnitController).get_transform().set_position(this.CalcGridCenter(unit.startX, unit.startY));
      tacticsUnitController.CancelAction();
      ((Component) tacticsUnitController).get_transform().set_rotation(unit.startDir.ToRotation());
      return tacticsUnitController;
    }

    public int DisplayMultiPlayInputTimeLimit { get; set; }

    public JSON_MyPhotonPlayerParam CurrentNotifyDisconnectedPlayer { get; set; }

    public JSON_MyPhotonPlayerParam CurrentResumePlayer { get; set; }

    public SceneBattle.ENotifyDisconnectedPlayerType CurrentNotifyDisconnectedPlayerType { get; set; }

    private int GetMultiPlayInputTimeLimit()
    {
      int playInputTimeLimit = (int) this.MultiPlayInputTimeLimit;
      if ((double) this.MultiPlayInputTimeLimit > (double) playInputTimeLimit)
        ++playInputTimeLimit;
      return playInputTimeLimit;
    }

    public int GetNextMyTurn()
    {
      int num = 0;
      for (int index = 0; index < this.Battle.Order.Count; ++index)
      {
        Unit unit = this.Battle.Order[(0 + index) % this.Battle.Order.Count].Unit;
        if (!unit.IsDead && unit.IsEntry && !unit.IsSub)
        {
          if (unit.OwnerPlayerIndex == this.Battle.MyPlayerIndex)
            return num;
          ++num;
        }
      }
      return -1;
    }

    public string PhotonErrorString
    {
      get
      {
        return this.mPhotonErrString;
      }
    }

    private void OnSuccessCheat(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        FlowNode_Network.Failed();
      }
      Network.RemoveAPI();
    }

    private void RecvResumeSuccess(SceneBattle.MultiPlayer mp, SceneBattle.MultiPlayRecvData data)
    {
      this.mResumeSend = false;
      if (this.mRecvResumeRequest.Count > 0)
        this.mRecvResumeRequest.RemoveAll((Predicate<SceneBattle.MultiPlayRecvData>) (p => p.pidx == data.pidx));
      this.Battle.ResumeState = this.mRecvResumeRequest.Count > 0 ? BattleCore.RESUME_STATE.WAIT : BattleCore.RESUME_STATE.NONE;
      if (mp != null)
      {
        if (mp.NotifyDisconnected && this.mRecvResume.Find((Predicate<SceneBattle.MultiPlayRecvData>) (d => d.pidx == data.pidx)) == null)
          this.mRecvResume.Add(data);
        mp.Disconnected = false;
        mp.NotifyDisconnected = false;
      }
      this.mRecvCheck.Clear();
      this.mMultiPlayCheckList.Clear();
      this.mRecvCheckData.Clear();
      this.mRecvCheckMyData.Clear();
      if (!this.Battle.IsResume)
        return;
      this.SendResumeInfo();
    }

    private void RecvEvent()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      List<MyPhoton.MyEvent> events = instance.GetEvents();
      if (events == null)
        return;
      while (events.Count > 0)
      {
        Debug.LogWarning((object) events[0].code);
        if (this.mResumeMultiPlay)
        {
          if (events[0].code == MyPhoton.SEND_TYPE.Resume)
          {
            this.StartCoroutine(this.RecvResume(events[0].binary));
          }
          else
          {
            SceneBattle.MultiPlayRecvData data = (SceneBattle.MultiPlayRecvData) null;
            if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out data, events[0].binary))
            {
              SceneBattle.MultiPlayer mp = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == data.pid)) ?? this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == data.pidx));
              if (data.h == 4)
                this.mRecvContinue.Add(data);
              else if (data.h == 6)
              {
                if (mp != null)
                  mp.FinishLoad = true;
              }
              else if (data.h == 9)
                this.RecvResumeSuccess(mp, data);
            }
          }
        }
        else if (events[0].code == MyPhoton.SEND_TYPE.Normal)
        {
          if (events[0].binary == null)
          {
            events.RemoveAt(0);
            continue;
          }
          SceneBattle.MultiPlayRecvData data = (SceneBattle.MultiPlayRecvData) null;
          SceneBattle.MultiPlayer mp = (SceneBattle.MultiPlayer) null;
          if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out data, events[0].binary))
          {
            mp = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == data.pid)) ?? this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == data.pidx));
            this.MultiPlayLog("[PUN] recv packet sq:" + (object) data.sq + " pid:" + (object) data.pid + " pidx:" + (object) data.pidx + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) this.UnitStartCountTotal);
          }
          else
            data = (SceneBattle.MultiPlayRecvData) null;
          if (data != null)
          {
            if (data.h == 4)
              this.mRecvContinue.Add(data);
            else if (data.h == 3)
              this.mRecvGoodJob.Add(data);
            else if (data.h == 2)
            {
              this.mRecvCheck.RemoveAll((Predicate<SceneBattle.MultiPlayRecvData>) (p => p.pidx == data.pidx));
              this.mRecvCheck.Add(data);
              if (!this.mRecvCheckData.ContainsKey(data.b))
                this.mRecvCheckData.Add(data.b, data);
            }
            else if (data.h == 1)
              this.mRecvBattle.Add(data);
            else if (data.h == 5)
              this.mRecvIgnoreMyDisconnect.Add(data);
            else if (data.h == 6)
            {
              if (mp != null)
                mp.FinishLoad = true;
            }
            else if (data.h == 7)
            {
              if (this.mRecvResumeRequest.Find((Predicate<SceneBattle.MultiPlayRecvData>) (d => d.pidx == data.pidx)) == null)
              {
                this.Battle.ResumeState = BattleCore.RESUME_STATE.REQUEST;
                this.mResumeSend = false;
                Debug.Log((object) "*********************");
                Debug.Log((object) "ResumeRequest!!");
                Debug.Log((object) "*********************");
                this.mRecvResumeRequest.Add(data);
              }
            }
            else if (data.h == 9)
              this.RecvResumeSuccess(mp, data);
            else if (data.h == 10)
            {
              if (mp != null)
                mp.SyncWait = true;
            }
            else if (data.h == 11)
            {
              if (mp != null)
                mp.SyncResumeWait = true;
            }
            else if ((data.h == 14 || data.h == 15 || data.h == 16) && (data.uid == instance.MyPlayerIndex && !this.mCheater))
            {
              int type = 0;
              Unit unit = (Unit) null;
              for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
              {
                if (this.Battle.AllUnits[index].OwnerPlayerIndex == instance.MyPlayerIndex)
                {
                  unit = this.Battle.AllUnits[index];
                  break;
                }
              }
              string val = string.Empty;
              if (unit != null)
              {
                val = "unit = " + unit.UnitParam.iname;
                if (data.h == 15)
                {
                  List<SkillData> battleSkills = unit.BattleSkills;
                  if (battleSkills != null)
                  {
                    for (int index = 0; index < battleSkills.Count; ++index)
                    {
                      int cost = battleSkills[index].Cost;
                      int hpCostRate = battleSkills[index].HpCostRate;
                      val = val + "[" + (object) index + "]:" + battleSkills[index].SkillParam.iname + "cost(mp):" + (object) cost + "cost(hp):" + (object) hpCostRate + ",";
                    }
                  }
                }
              }
              Network.RequestAPI((WebAPI) new ReqMultiCheat(type, val, new Network.ResponseCallback(this.OnSuccessCheat)), false);
              this.mCheater = true;
            }
          }
        }
        events.RemoveAt(0);
      }
    }

    private bool DisconnetEvent()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (this.mExecDisconnected && this.Battle.IsMultiTower || (MonoSingleton<GameManager>.Instance.IsVSCpuBattle || instance.CurrentState == MyPhoton.MyState.ROOM) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        return false;
      if (!this.mExecDisconnected && !this.mIsWaitingForBattleSignal && !BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED))
      {
        this.mExecDisconnected = true;
        this.mBattleUI_MultiPlay.OnMyDisconnected();
        if (this.mDisconnectType == SceneBattle.EDisconnectType.BAN)
        {
          this.MultiPlayLog("OnBan");
          this.mBattleUI.OnBan();
        }
        else if (this.mDisconnectType == SceneBattle.EDisconnectType.DISCONNECTED)
        {
          if (instance.LastError == MyPhoton.MyError.TIMEOUT2)
          {
            this.mPhotonErrString = "2:TimeOut:" + (object) this.mMultiPlaySendID;
            using (List<SceneBattle.MultiPlayer>.Enumerator enumerator = this.mMultiPlayer.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SceneBattle.MultiPlayer current = enumerator.Current;
                SceneBattle sceneBattle = this;
                sceneBattle.mPhotonErrString = sceneBattle.mPhotonErrString + "-" + current.RecvInputNum.ToString();
              }
            }
          }
          else
            this.mPhotonErrString = instance.LastError != MyPhoton.MyError.TIMEOUT ? (instance.LastError != MyPhoton.MyError.RAISE_EVENT_FAILED ? "0" : "3:SendFailed") : "1:TimeOut";
          this.MultiPlayLog("OnDisconnected");
          this.mBattleUI.OnDisconnected();
        }
        else
        {
          this.MultiPlayLog("OnSequenceError");
          this.mBattleUI.OnSequenceError();
        }
        this.GotoState<SceneBattle.State_Disconnected>();
      }
      return true;
    }

    private void ShowTimeLimit()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        return;
      bool flag = false;
      if (this.mBeginMultiPlay && this.Battle.CurrentUnit != null && (this.Battle.CurrentUnit.OwnerPlayerIndex == this.Battle.MyPlayerIndex && this.GainMultiPlayInputTimeLimit()))
        flag = true;
      int playInputTimeLimit = this.GetMultiPlayInputTimeLimit();
      if (flag && this.DisplayMultiPlayInputTimeLimit != playInputTimeLimit)
      {
        this.DisplayMultiPlayInputTimeLimit = playInputTimeLimit;
        GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.MULTI_INPUT_TIME_LIMIT);
      }
      if (playInputTimeLimit <= 0)
        flag = false;
      if (this.mShowInputTimeLimit != flag)
      {
        if (flag)
          this.mBattleUI_MultiPlay.ShowInputTimeLimit();
        else
          this.mBattleUI_MultiPlay.HideInputTimeLimit();
      }
      this.mShowInputTimeLimit = flag;
    }

    private void ShowThinking()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null))
        return;
      int num = 0;
      if ((this.IsInState<SceneBattle.State_MapCommandMultiPlay>() || this.IsInState<SceneBattle.State_MapCommandVersus>()) && (this.Battle.CurrentUnit != null && this.Battle.CurrentUnit.OwnerPlayerIndex > 0))
      {
        string s = FlowNode_Variable.Get("DisableThinkingUI");
        if (string.IsNullOrEmpty(s) || long.Parse(s) == 0L)
          num = this.Battle.CurrentUnit.OwnerPlayerIndex;
      }
      if (MonoSingleton<GameManager>.Instance.AudienceMode && this.Battle.CurrentUnit != null && this.Battle.CurrentUnit.OwnerPlayerIndex > 0)
        num = this.Battle.CurrentUnit.OwnerPlayerIndex;
      if (num != this.mThinkingPlayerIndex)
      {
        if (num <= 0 || this.Battle.CurrentUnit.OwnerPlayerIndex == this.Battle.MyPlayerIndex)
          this.mBattleUI_MultiPlay.HideThinking();
        else
          this.mBattleUI_MultiPlay.ShowThinking();
      }
      this.mThinkingPlayerIndex = num;
    }

    private void OtherPlayerDisconnect()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (this.Battle.GetQuestResult() != BattleCore.QuestResult.Pending || this.mExecDisconnected && this.Battle.IsMultiTower)
        return;
      List<MyPhoton.MyPlayer> roomPlayerList1 = instance.GetRoomPlayerList();
      if (roomPlayerList1 != null)
      {
        using (List<SceneBattle.MultiPlayer>.Enumerator enumerator = this.mMultiPlayer.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SceneBattle.MultiPlayer current = enumerator.Current;
            if (roomPlayerList1 != null && instance.FindPlayer(roomPlayerList1, current.PlayerID, current.PlayerIndex) == null)
            {
              if (instance.IsOldestPlayer() && !current.Disconnected)
                this.SendOtherPlayerDisconnect(current.PlayerIndex);
              current.Disconnected = true;
            }
          }
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) && !this.mIsWaitingForBattleSignal && (!BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED) && this.CurrentNotifyDisconnectedPlayer == null))
      {
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
        MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
        using (List<SceneBattle.MultiPlayer>.Enumerator enumerator1 = this.mMultiPlayer.GetEnumerator())
        {
          while (enumerator1.MoveNext())
          {
            SceneBattle.MultiPlayer mp = enumerator1.Current;
            if (myPlayersStarted != null && roomPlayerList1 != null && (myPlayer != null && !mp.NotifyDisconnected) && (instance.FindPlayer(roomPlayerList1, mp.PlayerID, mp.PlayerIndex) == null && this.mRecvIgnoreMyDisconnect.Find((Predicate<SceneBattle.MultiPlayRecvData>) (r => r.pid == mp.PlayerID)) == null))
            {
              this.CurrentNotifyDisconnectedPlayer = myPlayersStarted?.Find((Predicate<JSON_MyPhotonPlayerParam>) (sp => sp.playerIndex == mp.PlayerIndex));
              int num1 = 0;
              int num2 = 0;
              using (List<JSON_MyPhotonPlayerParam>.Enumerator enumerator2 = myPlayersStarted.GetEnumerator())
              {
                while (enumerator2.MoveNext())
                {
                  JSON_MyPhotonPlayerParam sp = enumerator2.Current;
                  SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (t => t.PlayerIndex == sp.playerIndex));
                  if (multiPlayer == null || !multiPlayer.NotifyDisconnected)
                  {
                    if (sp.playerIndex < num1 || num1 == 0)
                      num1 = sp.playerIndex;
                    if (sp.playerIndex != mp.PlayerIndex && (sp.playerIndex < num2 || num2 == 0))
                      num2 = sp.playerIndex;
                  }
                }
              }
              this.CurrentNotifyDisconnectedPlayerType = num1 == mp.PlayerIndex ? (num2 != instance.MyPlayerIndex ? SceneBattle.ENotifyDisconnectedPlayerType.OWNER : SceneBattle.ENotifyDisconnectedPlayerType.OWNER_AND_I_AM_OWNER) : SceneBattle.ENotifyDisconnectedPlayerType.NORMAL;
              if (this.CurrentResumePlayer != null)
                this.mBattleUI_MultiPlay.OnOtherPlayerResumeClose();
              this.mBattleUI_MultiPlay.OnMyPlayerResumeClose();
              mp.NotifyDisconnected = true;
              mp.RecvInputNum = 0;
              this.mBattleUI_MultiPlay.OnOtherDisconnected();
              break;
            }
          }
        }
      }
      if (this.Battle.ResumeState == BattleCore.RESUME_STATE.NONE)
        return;
      List<MyPhoton.MyPlayer> roomPlayerList2 = instance.GetRoomPlayerList();
      for (int index = this.mRecvResumeRequest.Count - 1; index >= 0; --index)
      {
        SceneBattle.MultiPlayRecvData data = this.mRecvResumeRequest[index];
        if (roomPlayerList2.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == data.pid)) == null)
          this.mRecvResumeRequest.RemoveAt(index);
      }
      if (this.mRecvResumeRequest.Count != 0)
        return;
      this.Battle.ResumeState = BattleCore.RESUME_STATE.NONE;
      this.mRecvCheck.Clear();
      this.mMultiPlayCheckList.Clear();
    }

    private void OtherPlayerResume()
    {
      if (this.mRecvResume.Count <= 0)
        return;
      List<JSON_MyPhotonPlayerParam> myPlayersStarted = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) || this.mIsWaitingForBattleSignal || (BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED) || this.CurrentResumePlayer != null))
        return;
      SceneBattle.MultiPlayRecvData data = this.mRecvResume[0];
      this.CurrentResumePlayer = myPlayersStarted?.Find((Predicate<JSON_MyPhotonPlayerParam>) (sp => sp.playerIndex == data.pidx));
      if (this.CurrentNotifyDisconnectedPlayer != null)
        this.mBattleUI_MultiPlay.OnOtherDisconnectedForce();
      this.mBattleUI_MultiPlay.OnMyPlayerResumeClose();
      this.mBattleUI_MultiPlay.OnOtherPlayerResume();
      this.mRecvResume.RemoveAt(0);
    }

    private void CheckStart()
    {
      if (this.Battle.SyncStart)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      this.Battle.SyncStart = true;
      using (List<SceneBattle.MultiPlayer>.Enumerator enumerator = this.mMultiPlayer.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SceneBattle.MultiPlayer current = enumerator.Current;
          if (!current.NotifyDisconnected)
            this.Battle.SyncStart &= current.FinishLoad;
        }
      }
      if (this.mResumeMultiPlay)
      {
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        if (roomPlayerList.Count == 1)
        {
          this.mResumeMultiPlay = false;
          this.mResumeOnlyPlayer = true;
          using (List<SceneBattle.MultiPlayer>.Enumerator enumerator = this.mMultiPlayer.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.NotifyDisconnected = true;
          }
          if (this.Battle.IsMultiVersus)
            this.GotoState<SceneBattle.State_ComfirmFinishbattle>();
          else
            this.mBattleUI_MultiPlay.OnMyPlayerResume();
        }
        else
        {
          bool flag = false;
          using (List<int>.Enumerator enumerator = this.mResumeAlreadyStartPlayer.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              int playerID = enumerator.Current;
              flag |= roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == playerID)) != null;
            }
          }
          if (this.Battle.IsMultiTower)
          {
            MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
            if (currentRoom != null && !currentRoom.battle)
            {
              flag = false;
              this.mExecDisconnected = true;
              instance.Disconnect();
              using (List<SceneBattle.MultiPlayerUnit>.Enumerator enumerator = this.mMultiPlayerUnit.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  SceneBattle.MultiPlayerUnit current = enumerator.Current;
                  if (current != null && current.Owner.PlayerIndex != instance.MyPlayerIndex)
                    current.Owner.Disconnected = true;
                }
              }
            }
          }
          this.mResumeMultiPlay = flag;
          this.Battle.SyncStart = !flag;
        }
      }
      this.Battle.SyncStart &= !this.mResumeMultiPlay;
      if (!this.Battle.SyncStart)
        return;
      instance.AddMyPlayerParam("BattleStart", (object) true);
    }

    private void UpdateMultiBattleInfo()
    {
      while (this.mRecvBattle.Count > 0)
      {
        SceneBattle.MultiPlayRecvData data = this.mRecvBattle[0];
        if (data.b > this.UnitStartCountTotal)
        {
          DebugUtility.LogWarning("[PUN] new turn data. sq:" + (object) data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) this.UnitStartCountTotal + " test:" + (object) this.mRecvBattle.FindIndex((Predicate<SceneBattle.MultiPlayRecvData>) (r => r.b < data.b)));
          break;
        }
        if (data.b < this.UnitStartCountTotal)
          DebugUtility.LogWarning("[PUN] old turn data. sq:" + (object) data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) this.UnitStartCountTotal);
        else if (data.h == 1)
        {
          SceneBattle.MultiPlayerUnit multiPlayerUnit = this.mMultiPlayerUnit.Find((Predicate<SceneBattle.MultiPlayerUnit>) (p => p.UnitID == data.uid));
          if (multiPlayerUnit != null)
            multiPlayerUnit.RecvInput(this, data);
        }
        this.mRecvBattle.RemoveAt(0);
      }
      if (!MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      for (int i = 0; i < this.mAudienceDisconnect.Count; ++i)
      {
        if (this.mAudienceDisconnect[i].b == this.UnitStartCountTotal)
        {
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerIndex == this.mAudienceDisconnect[i].uid));
          if (multiPlayer != null && multiPlayer.StartBegin)
          {
            multiPlayer.Disconnected = true;
            List<SceneBattle.MultiPlayRecvData> audienceDisconnect = this.mAudienceDisconnect;
            int num;
            i = (num = i) - 1;
            int index = num;
            audienceDisconnect.RemoveAt(index);
          }
        }
      }
      if (this.mAudienceRetire == null || this.mAudienceRetire.b != this.UnitStartCountTotal || this.mRecvBattle.Count > 0)
        return;
      SceneBattle.MultiPlayer multiPlayer1 = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == this.mAudienceRetire.pid));
      if (multiPlayer1 != null)
        multiPlayer1.NotifyDisconnected = true;
      this.mAudienceRetire = (SceneBattle.MultiPlayRecvData) null;
      this.Battle.IsVSForceWin = true;
      this.GotoState_WaitSignal<SceneBattle.State_AudienceRetire>();
    }

    private bool CheckInputTimeLimit()
    {
      if (this.Battle.IsResume || (double) this.MultiPlayInputTimeLimit == 0.0)
        return true;
      if ((double) this.MultiPlayInputTimeLimit > 0.0)
      {
        if (!this.GainMultiPlayInputTimeLimit())
          return true;
        this.MultiPlayInputTimeLimit -= Time.get_unscaledDeltaTime();
        if ((double) this.MultiPlayInputTimeLimit > 0.0)
          return true;
        this.MultiPlayInputTimeLimit = 0.0f;
        this.MultiPlayLog("[PUN]TimeUp!");
      }
      return false;
    }

    private void SendTimeLimit()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      if (this.mIsWaitingForBattleSignal)
      {
        this.MultiPlayLog("[PUN]TimeUp but waiting for battle signal...");
        this.MultiPlayInputTimeLimit = -1f;
      }
      else
      {
        this.MultiPlayInputTimeLimit = 0.0f;
        if (this.IsInState<SceneBattle.State_MapMoveSelect_Stick>())
        {
          this.Battle.EntryBattleMultiPlayTimeUp = true;
        }
        else
        {
          if (!this.Battle.CurrentUnit.IsUnitFlag(EUnitFlag.Moved))
          {
            TacticsUnitController tacticsUnitController = this.ResetMultiPlayerTransform(this.Battle.CurrentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) tacticsUnitController, (UnityEngine.Object) null))
              this.SetCameraTarget(((Component) tacticsUnitController).get_transform().get_position());
            this.SendInputMoveEnd(this.Battle.CurrentUnit, true);
          }
          this.SendInputUnitTimeLimit(this.Battle.CurrentUnit);
          this.SendInputFlush(false);
          this.CloseBattleUI();
          this.Battle.EntryBattleMultiPlayTimeUp = true;
          this.GotoMapCommand();
        }
      }
    }

    public bool CheckSync()
    {
      List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
      bool flag = true;
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MyPhoton.MyPlayer player = enumerator.Current;
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == player.playerID));
          if (player.start && multiPlayer != null && !multiPlayer.NotifyDisconnected)
          {
            flag &= multiPlayer.SyncWait;
            Debug.Log((object) (multiPlayer.SyncWait.ToString() + "/" + (object) multiPlayer.PlayerIndex));
          }
        }
      }
      return flag;
    }

    public void ResetSync()
    {
      using (List<SceneBattle.MultiPlayer>.Enumerator enumerator = this.mMultiPlayer.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SceneBattle.MultiPlayer current = enumerator.Current;
          current.SyncWait = false;
          current.SyncResumeWait = false;
        }
      }
    }

    public bool CheckResumeSync()
    {
      List<MyPhoton.MyPlayer> roomPlayerList = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList();
      bool flag = true;
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MyPhoton.MyPlayer player = enumerator.Current;
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == player.playerID));
          if (player.start && multiPlayer != null && !multiPlayer.NotifyDisconnected)
            flag &= multiPlayer.SyncResumeWait;
        }
      }
      return flag;
    }

    private void UpdateMultiPlayer()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || !this.Battle.MultiFinishLoad)
        return;
      this.UpdateInterval();
      this.RecvEvent();
      if (this.DisconnetEvent())
        return;
      this.SendRequestResume();
      this.SendResumeInfo();
      this.CheckStart();
      this.ShowTimeLimit();
      this.ShowThinking();
      this.OtherPlayerDisconnect();
      this.OtherPlayerResume();
      if (!this.mBeginMultiPlay)
        return;
      this.CheckMultiPlay();
      this.UpdateGoodJob();
      this.UpdateMultiBattleInfo();
      for (int index = 0; index < this.mMultiPlayer.Count; ++index)
        this.mMultiPlayer[index].Update(this);
      for (int index = 0; index < this.mMultiPlayerUnit.Count; ++index)
        this.mMultiPlayerUnit[index].Update(this);
      this.mSendTime += Time.get_deltaTime();
      if ((double) this.mSendTime >= (double) this.SEND_TURN_SEC)
        this.SendInputFlush(false);
      if (this.CheckInputTimeLimit())
        return;
      this.SendTimeLimit();
    }

    private bool RecvEventAudience(bool isSkip = false)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int num1 = -1;
      int num2 = -1;
      int cnt = 0;
      bool flag = true;
      bool check = false;
      while (true)
      {
        SceneBattle.MultiPlayRecvData data;
        do
        {
          do
          {
            if (isSkip && instance.AudienceManager.IsSkipEnd)
            {
              flag = false;
              goto label_20;
            }
            else
            {
              data = (SceneBattle.MultiPlayRecvData) null;
              data = instance.AudienceManager.GetData();
              ++cnt;
              if (data == null)
                goto label_20;
            }
          }
          while (data.b == 0 || data.h == 10 || (data.h == 11 || data.h == 9));
          if (isSkip && num2 >= 0 && data.b != num2)
          {
            instance.AudienceManager.Restore();
            goto label_20;
          }
          else
          {
            num2 = data.b;
            SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == data.pid));
            if (multiPlayer != null && !isSkip)
            {
              if (num1 == -1)
                num1 = multiPlayer.PlayerID;
              else if (num1 != multiPlayer.PlayerID)
              {
                instance.AudienceManager.Restore();
                goto label_20;
              }
            }
            if (data.h == 1)
              this.mRecvBattle.Add(data);
            else if (data.h == 12)
              this.mAudienceRetire = data;
            else if (data.h == 17)
              this.mAudienceDisconnect.Add(data);
          }
        }
        while (data == null || data.h != 2);
        check = true;
      }
label_20:
      return flag & this.CheckSkipLogEnd(isSkip, cnt, check);
    }

    private bool CheckSkipLogEnd(bool isSkip, int cnt, bool check)
    {
      if (isSkip)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (!check)
        {
          for (int index = 0; index < cnt; ++index)
            instance.AudienceManager.Restore();
          this.mRecvBattle.Clear();
        }
      }
      return check;
    }

    private void UpdateAudiencePlayer()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.AudienceMode || !this.Battle.MultiFinishLoad || !this.QuestStart)
        return;
      this.RecvEventAudience(false);
      this.ShowThinking();
      this.UpdateMultiBattleInfo();
      for (int index = 0; index < this.mMultiPlayerUnit.Count; ++index)
        this.mMultiPlayerUnit[index].Update(this);
      if (!instance.AudienceManager.IsEnd || this.Battle.RemainVersusTurnCount == 0U || (this.IsInState<SceneBattle.State_AudienceForceEnd>() || this.IsInState<SceneBattle.State_AudienceEnd>()) || (this.IsInState<SceneBattle.State_AudienceRetire>() || this.IsInState<SceneBattle.State_ExitQuest>() || this.CheckAudienceResult() != BattleCore.QuestResult.Pending))
        return;
      this.GotoState<SceneBattle.State_AudienceForceEnd>();
    }

    public void SkipLog()
    {
      if (this.ReqCreateBreakObjUcLists.Count != 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!this.RecvEventAudience(true))
      {
        this.Battle.MultiFinishLoad = true;
        instance.AudienceManager.SkipMode = false;
        this.Battle.StartOrder(false, false, true);
        this.Battle.RemainVersusTurnCount = (uint) this.UnitStartCountTotal;
        this.ArenaActionCountSet(this.Battle.RemainVersusTurnCount);
      }
      else
      {
        ++this.mUnitStartCount;
        ++this.mUnitStartCountTotal;
        this.Battle.UnitStart();
        this.BeginMultiPlayer();
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsUnits[index], (UnityEngine.Object) null))
            this.mTacticsUnits[index].UpdateBadStatus();
        }
        this.UpdateMultiBattleInfo();
        for (int index = 0; index < this.mMultiPlayerUnit.Count; ++index)
          this.mMultiPlayerUnit[index].UpdateSkip(this);
        this.Battle.CommandWait(false);
        if (this.Battle.GetQuestResult() == BattleCore.QuestResult.Pending)
          this.Battle.UnitEnd();
        for (BattleCore.OrderData currentOrderData = this.Battle.CurrentOrderData; currentOrderData != null && currentOrderData.IsCastSkill; currentOrderData = this.Battle.CurrentOrderData)
        {
          Unit currentUnit = this.Battle.CurrentUnit;
          SkillData castSkill = currentUnit.CastSkill;
          int x = 0;
          int y = 0;
          if (castSkill != null)
          {
            if (currentUnit.UnitTarget != null)
            {
              x = currentUnit.UnitTarget.x;
              y = currentUnit.UnitTarget.y;
            }
            if (currentUnit.GridTarget != null)
            {
              x = currentUnit.GridTarget.x;
              y = currentUnit.GridTarget.y;
            }
          }
          this.Battle.CastSkillStart();
          this.Battle.CastSkillEnd();
          if (castSkill != null && castSkill.IsSetBreakObjSkill())
          {
            Unit gimmickAtGrid = this.Battle.FindGimmickAtGrid(x, y, false);
            if (gimmickAtGrid != null && gimmickAtGrid.IsBreakObj)
              this.ReqCreateBreakObjUcLists.Add(new SceneBattle.ReqCreateBreakObjUc(castSkill, gimmickAtGrid));
          }
        }
        for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
        {
          TacticsUnitController unitController = this.FindUnitController(this.Battle.AllUnits[index]);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            if (this.Battle.AllUnits[index].IsGimmick && !this.Battle.AllUnits[index].IsBreakObj)
            {
              if (this.Battle.AllUnits[index].IsDisableGimmick())
                ((Component) unitController).get_gameObject().SetActive(false);
            }
            else
            {
              if (this.Battle.AllUnits[index].IsBreakObj)
                unitController.ReflectDispModel();
              if (unitController.Unit.IsDead)
              {
                ((Component) unitController).get_gameObject().SetActive(false);
                unitController.ShowHPGauge(false);
                unitController.ShowVersusCursor(false);
                this.mTacticsUnits.Remove(unitController);
              }
            }
          }
        }
        for (int index = this.mTacticsUnits.Count - 1; index >= 0; --index)
        {
          TacticsUnitController tuc = this.mTacticsUnits[index];
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) tuc))
          {
            if (this.Battle.AllUnits.Find((Predicate<Unit>) (unit => unit == tuc.Unit)) == null)
            {
              ((Component) tuc).get_gameObject().SetActive(false);
              tuc.ShowHPGauge(false);
              tuc.ShowVersusCursor(false);
            }
            else
              continue;
          }
          this.mTacticsUnits.RemoveAt(index);
        }
        if (this.ReqCreateBreakObjUcLists.Count != 0)
        {
          for (int index = 0; index < this.ReqCreateBreakObjUcLists.Count; ++index)
          {
            SceneBattle.ReqCreateBreakObjUc rcb = this.ReqCreateBreakObjUcLists[index];
            rcb.mIsLoad = true;
            this.StartCoroutine(SceneBattle.State_PrepareSkill.loadBreakObjUnit(this, rcb.mTargetUnit, (Action) (() =>
            {
              TacticsUnitController unitController = this.FindUnitController(rcb.mTargetUnit);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                unitController.SetVisible(true);
              rcb.mIsLoad = false;
              this.ReqCreateBreakObjUcLists.Remove(rcb);
            })));
          }
        }
        this.Battle.Logs.Reset();
        this.EndMultiPlayer();
      }
    }

    private void SendInputFlush(bool force = false)
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if (instance1.AudienceMode || instance1.IsVSCpuBattle)
        return;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      if (this.mSendList.Count > 0)
      {
        byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.INPUT, this.mCurrentSendInputUnitID, this.mSendList);
        instance2.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
        if (force)
          instance2.SendFlush();
      }
      this.mSendList.Clear();
      this.mSendTime = 0.0f;
    }

    private void SendInputMove(Unit unit, TacticsUnitController controller)
    {
    }

    private void SendInputGridXY(Unit unit, int gridX, int gridY, EUnitDirection dir, bool send = true)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.AudienceMode || instance.IsVSCpuBattle || (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp) || this.mPrevGridX == gridX && this.mPrevGridY == gridY && this.mPrevDir == dir)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 3,
        gx = gridX,
        gy = gridY,
        d = (int) dir
      });
      this.MultiPlayLog("[PUN] SendInputGridXY x:" + (object) gridX + " y:" + (object) gridY);
      if (send)
        this.SendInputFlush(true);
      this.mPrevGridX = gridX;
      this.mPrevGridY = gridY;
      this.mPrevDir = dir;
    }

    private void SendInputMoveStart(Unit unit)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.AudienceMode || instance.IsVSCpuBattle || (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp))
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 1,
        gx = unit.x,
        gy = unit.y
      });
    }

    private void SendInputMoveEnd(Unit unit, bool cancel)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.AudienceMode || instance.IsVSCpuBattle || (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp))
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = !cancel ? 4 : 5,
        gx = unit.x,
        gy = unit.y,
        d = (int) unit.Direction
      });
      this.MultiPlayLog("[PUN] SendInputMoveEnd cancel:" + (object) cancel);
    }

    private void SendInputEntryBattle(EBattleCommand type, Unit unit, Unit enemy, SkillData skill, ItemData item, int gx, int gy, bool bUnitLockTarget)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (instance.AudienceMode || instance.IsVSCpuBattle || (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp))
        return;
      this.SendInputUnitXYDir(unit, unit.x, unit.y, unit.Direction);
      SceneBattle.MultiPlayInput multiPlayInput = new SceneBattle.MultiPlayInput();
      multiPlayInput.c = 6;
      if (enemy != null)
        multiPlayInput.u = this.Battle.AllUnits.FindIndex((Predicate<Unit>) (u => u == enemy));
      if (skill != null)
      {
        multiPlayInput.s = skill.SkillID;
        if (skill.CastType == ECastTypes.Jump)
          this.MultiPlayInputTimeLimit = 0.0f;
      }
      if (item != null)
        multiPlayInput.i = item.ItemID;
      multiPlayInput.gx = gx;
      multiPlayInput.gy = gy;
      multiPlayInput.d = (int) type;
      multiPlayInput.ul = !bUnitLockTarget ? 0 : 1;
      this.mSendList.Add(multiPlayInput);
      this.MultiPlayLog("[PUN] SendInputEntryBattle (" + (object) unit.x + "," + (object) unit.y + ")" + (object) unit.Direction + "(" + (enemy != null ? (object) enemy.UnitName : (object) "null") + "[" + (object) multiPlayInput.u + "]," + (skill != null ? (object) skill.SkillID : (object) "null") + "," + (item != null ? (object) item.ItemID : (object) "null") + ")");
      this.SendInputFlush(false);
    }

    private void SendInputGridEvent(Unit unit)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      if (!this.Battle.CurrentUnit.IsUnitFlag(EUnitFlag.Moved))
        DebugUtility.LogWarning("SendInputGridEvent not moved");
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 7,
        gx = unit.x,
        gy = unit.y,
        d = (int) unit.Direction
      });
      this.MultiPlayLog("[PUN] SendInputUnitTimeLimit");
    }

    private void SendInputUnitEnd(Unit unit, EUnitDirection dir)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      if (!this.Battle.CurrentUnit.IsUnitFlag(EUnitFlag.Moved))
        DebugUtility.LogWarning("SendInputUnitEnd not moved");
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 8,
        gx = unit.x,
        gy = unit.y,
        d = (int) dir
      });
      this.MultiPlayLog("[PUN] SendInputUnitEnd");
      this.SendInputFlush(false);
      this.MultiPlayInputTimeLimit = 0.0f;
    }

    private void SendInputUnitTimeLimit(Unit unit)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 9,
        gx = unit.x,
        gy = unit.y,
        d = (int) unit.Direction
      });
      this.MultiPlayLog("[PUN] SendInputUnitTimeLimit");
    }

    private void SendInputUnitXYDir(Unit unit, int gridX, int gridY, EUnitDirection dir)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 10,
        gx = gridX,
        gy = gridY,
        d = (int) dir
      });
      this.MultiPlayLog("[PUN] SendInputUnitXYDir x:" + (object) gridX + " y:" + (object) gridY + " dir:" + (object) dir);
    }

    private bool SendIgnoreMyDisconnect()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || MonoSingleton<GameManager>.Instance.IsVSCpuBattle || !this.Battle.IsMultiPlay)
        return true;
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.IGNORE_MY_DISCONNECT, 0, (List<SceneBattle.MultiPlayInput>) null);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      instance.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
      instance.SendFlush();
      this.MultiPlayLog("[PUN]SendIgnoreMyDisconnect");
      return true;
    }

    public void SendOtherPlayerDisconnect(int uid)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay)
        return;
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.OTHERPLAYER_DISCONNECT, uid, (List<SceneBattle.MultiPlayInput>) null);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      instance.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
      instance.SendFlush();
      this.MultiPlayLog("[PUN]SendIgnoreMyDisconnect");
    }

    public void ResetCheckData()
    {
      this.mRecvCheck.Clear();
      this.mMultiPlayCheckList.Clear();
    }

    private bool SendCheckMultiPlay()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.Battle.IsMultiPlay)
        return true;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) && this.mBattleUI_MultiPlay.CheckRandCheat;
      List<SceneBattle.MultiPlayInput> sendList = new List<SceneBattle.MultiPlayInput>();
      for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
      {
        Unit allUnit = this.Battle.AllUnits[index];
        SceneBattle.MultiPlayInput multiPlayInput = new SceneBattle.MultiPlayInput();
        multiPlayInput.c = (int) allUnit.CurrentStatus.param.hp;
        multiPlayInput.gx = allUnit.x;
        multiPlayInput.gy = allUnit.y;
        multiPlayInput.d = (int) allUnit.Direction;
        RandXorshift randXorshift1 = !flag ? (RandXorshift) null : this.Battle.CloneRand();
        RandXorshift randXorshift2 = !flag ? (RandXorshift) null : this.Battle.CloneRandDamage();
        uint num1 = randXorshift1 != null ? randXorshift1.Get() : 0U;
        uint num2 = randXorshift2 != null ? randXorshift2.Get() : 0U;
        multiPlayInput.s = num1.ToString() + " / " + num2.ToString();
        sendList.Add(multiPlayInput);
      }
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.CHECK, 0, sendList);
      if (instance.IsOldestPlayer())
      {
        instance.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
        instance.SendFlush();
      }
      SceneBattle.MultiPlayRecvData data = (SceneBattle.MultiPlayRecvData) null;
      if (GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out data, sendBinary))
      {
        this.mRecvCheck.RemoveAll((Predicate<SceneBattle.MultiPlayRecvData>) (p => p.pidx == data.pidx));
        this.mRecvCheck.Add(data);
        if (!this.mRecvCheckMyData.ContainsKey(data.b))
          this.mRecvCheckMyData.Add(data.b, data);
      }
      Debug.Log((object) ("*****SendCheckData******::" + (object) data.b));
      return true;
    }

    private bool CheckMultiPlay()
    {
      if (!this.Battle.IsMultiPlay || this.mRecvCheckData.Count <= 0 || this.mRecvCheckMyData.Count <= 0)
        return true;
      bool flag = true;
      SceneBattle.MultiPlayRecvData multiPlayRecvData1 = (SceneBattle.MultiPlayRecvData) null;
      using (List<KeyValuePair<int, SceneBattle.MultiPlayRecvData>>.Enumerator enumerator = this.mRecvCheckMyData.ToList<KeyValuePair<int, SceneBattle.MultiPlayRecvData>>().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<int, SceneBattle.MultiPlayRecvData> current = enumerator.Current;
          SceneBattle.MultiPlayCheck dst = new SceneBattle.MultiPlayCheck();
          SceneBattle.MultiPlayRecvData multiPlayRecvData2 = current.Value;
          dst.playerID = multiPlayRecvData2.pid;
          dst.playerIndex = multiPlayRecvData2.pidx;
          dst.battleTurn = multiPlayRecvData2.b;
          dst.hp = multiPlayRecvData2.c;
          dst.gx = multiPlayRecvData2.gx;
          dst.gy = multiPlayRecvData2.gy;
          dst.dir = multiPlayRecvData2.d;
          dst.rnd = multiPlayRecvData2.s == null || multiPlayRecvData2.s.Length <= 0 ? (string) null : multiPlayRecvData2.s[0];
          if (this.mRecvCheckData.TryGetValue(current.Key, out multiPlayRecvData1))
          {
            SceneBattle.MultiPlayCheck multiPlayCheck = new SceneBattle.MultiPlayCheck();
            multiPlayCheck.playerID = multiPlayRecvData1.pid;
            multiPlayCheck.playerIndex = multiPlayRecvData1.pidx;
            multiPlayCheck.battleTurn = multiPlayRecvData1.b;
            multiPlayCheck.hp = multiPlayRecvData1.c;
            multiPlayCheck.gx = multiPlayRecvData1.gx;
            multiPlayCheck.gy = multiPlayRecvData1.gy;
            multiPlayCheck.dir = multiPlayRecvData1.d;
            multiPlayCheck.rnd = multiPlayRecvData1.s == null || multiPlayRecvData1.s.Length <= 0 ? (string) null : multiPlayRecvData1.s[0];
            flag &= multiPlayCheck.IsEqual(dst);
            if (!flag)
            {
              string msg = string.Empty;
              for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
                msg = msg + this.Battle.AllUnits[index].UnitName + "-pos:" + (object) this.Battle.AllUnits[index].x + "," + (object) this.Battle.AllUnits[index].y;
              DebugUtility.LogWarning(msg);
            }
            this.mRecvCheckMyData.Remove(current.Key);
            this.mRecvCheckData.Remove(current.Key);
            DebugUtility.LogWarning(dst.ToString());
            DebugUtility.LogWarning(multiPlayCheck.ToString());
          }
        }
      }
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (flag)
      {
        Debug.Log((object) "Check OK!");
        return true;
      }
      this.mDisconnectType = SceneBattle.EDisconnectType.BAN;
      instance.Disconnect();
      return false;
    }

    private SRPG_TouchInputModule GetTouchInputModule()
    {
      GameObject gameObject = GameObject.Find("EVENTSYSTEM");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return (SRPG_TouchInputModule) null;
      return (SRPG_TouchInputModule) gameObject.GetComponent<SRPG_TouchInputModule>();
    }

    public void SetupGoodJob()
    {
      if (this.mSetupGoodJob)
        return;
      SRPG_TouchInputModule touchInputModule = this.GetTouchInputModule();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) touchInputModule, (UnityEngine.Object) null))
        return;
      touchInputModule.OnDoubleTap += new SRPG_TouchInputModule.OnDoubleTapDelegate(this.OnDoubleTap);
      this.mSetupGoodJob = true;
    }

    public void CleanupGoodJob()
    {
      if (!this.mSetupGoodJob)
        return;
      SRPG_TouchInputModule touchInputModule = this.GetTouchInputModule();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) touchInputModule, (UnityEngine.Object) null))
        return;
      touchInputModule.OnDoubleTap -= new SRPG_TouchInputModule.OnDoubleTapDelegate(this.OnDoubleTap);
      this.mSetupGoodJob = false;
    }

    private void AddGoodJob(int gx, int gy, int unitID = -1)
    {
      if (!this.Battle.IsMultiPlay || (double) this.mGoodJobWait > 0.0)
        return;
      this.mGoodJobWait = 1f;
      if (gx < 0 || gx >= this.Battle.CurrentMap.Width || (gy < 0 || gy >= this.Battle.CurrentMap.Height))
        return;
      BattleStamp battleStamp = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) ? this.mBattleUI_MultiPlay.StampWindow : (BattleStamp) null;
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.GOODJOB, unitID, new List<SceneBattle.MultiPlayInput>()
      {
        new SceneBattle.MultiPlayInput()
        {
          gx = gx,
          gy = gy,
          c = !UnityEngine.Object.op_Equality((UnityEngine.Object) battleStamp, (UnityEngine.Object) null) ? battleStamp.SelectStampID : -1
        }
      });
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
      SceneBattle.MultiPlayRecvData buffer = (SceneBattle.MultiPlayRecvData) null;
      if (!GameUtility.Binary2Object<SceneBattle.MultiPlayRecvData>(out buffer, sendBinary))
        return;
      this.mRecvGoodJob.Add(buffer);
    }

    public void OnDoubleTap(Vector2 pos)
    {
    }

    private void OnGoodJobClickGrid(Grid grid)
    {
      this.AddGoodJob(grid.x, grid.y, -1);
    }

    private void OnGoodJobClickUnit(TacticsUnitController controller)
    {
      Unit unit = controller.Unit;
      int index = this.Battle.AllUnits.FindIndex((Predicate<Unit>) (u => u == unit));
      if (index < 0)
        return;
      this.AddGoodJob(0, 0, index);
    }

    private void UpdateGoodJob()
    {
      if (!this.mSetupGoodJob)
        return;
      if ((double) this.mGoodJobWait > 0.0)
        this.mGoodJobWait -= Time.get_deltaTime();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) && this.mBattleUI_MultiPlay.StampWindowIsOpened)
      {
        if (this.mOnGridClick == null)
          this.mOnGridClick = new SceneBattle.GridClickEvent(this.OnGoodJobClickGrid);
        if (this.mOnUnitClick == null)
          this.mOnUnitClick = new SceneBattle.UnitClickEvent(this.OnGoodJobClickUnit);
      }
      else
      {
        if ((MulticastDelegate) this.mOnGridClick == (MulticastDelegate) new SceneBattle.GridClickEvent(this.OnGoodJobClickGrid))
          this.mOnGridClick = (SceneBattle.GridClickEvent) null;
        if ((MulticastDelegate) this.mOnUnitClick == (MulticastDelegate) new SceneBattle.UnitClickEvent(this.OnGoodJobClickUnit))
          this.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
      }
      SceneBattle.MultiPlayInput multiPlayInput = new SceneBattle.MultiPlayInput();
      using (List<SceneBattle.MultiPlayRecvData>.Enumerator enumerator = this.mRecvGoodJob.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SceneBattle.MultiPlayRecvData current = enumerator.Current;
          int index1 = current.c == null || current.c.Length <= 0 ? multiPlayInput.c : current.c[0];
          int index2 = current.gx == null || current.gx.Length <= 0 ? multiPlayInput.gx : current.gx[0];
          int index3 = current.gy == null || current.gy.Length <= 0 ? multiPlayInput.gy : current.gy[0];
          int uid = current.uid;
          if (0 <= uid && uid < this.Battle.AllUnits.Count)
          {
            index2 = this.Battle.AllUnits[uid].x;
            index3 = this.Battle.AllUnits[uid].y;
          }
          if (this.Battle.CurrentMap != null && this.mHeightMap != null && (0 <= index2 && index2 < this.Battle.CurrentMap.Width) && (0 <= index3 && index3 < this.Battle.CurrentMap.Height))
          {
            Vector3 position = this.CalcGridCenter(this.Battle.CurrentMap[index2, index3]);
            BattleStamp battleStamp = !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mBattleUI_MultiPlay, (UnityEngine.Object) null) ? this.mBattleUI_MultiPlay.StampWindow : (BattleStamp) null;
            Sprite sprite = UnityEngine.Object.op_Equality((UnityEngine.Object) battleStamp, (UnityEngine.Object) null) || battleStamp.Sprites == null || (index1 < 0 || index1 >= battleStamp.Sprites.Length) ? (Sprite) null : battleStamp.Sprites[index1];
            GameObject prefab = UnityEngine.Object.op_Equality((UnityEngine.Object) battleStamp, (UnityEngine.Object) null) || battleStamp.Prefabs == null || (index1 < 0 || index1 >= battleStamp.Prefabs.Length) ? (GameObject) null : battleStamp.Prefabs[index1];
            this.PopupGoodJob(position, prefab, sprite);
          }
        }
      }
      this.mRecvGoodJob.Clear();
    }

    public bool SendFinishLoad()
    {
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      if (instance1.AudienceMode || instance1.IsVSCpuBattle)
        return true;
      MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
      MyPhoton.MyPlayer me = instance2.GetMyPlayer();
      SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == me.playerID));
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.FINISH_LOAD, 0, (List<SceneBattle.MultiPlayInput>) null);
      bool flag = instance2.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
      if (multiPlayer != null)
        multiPlayer.FinishLoad = true;
      return flag;
    }

    public void SendResumeSuccess()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || this.mResumeSend || (double) this.mRestResumeSuccessInterval > 0.0)
        return;
      this.mRestResumeSuccessInterval = this.RESUME_SUCCESS_INTERVAL;
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessageBinary(true, this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.RESUME_SUCCESS, 0, (List<SceneBattle.MultiPlayInput>) null), MyPhoton.SEND_TYPE.Normal, false);
    }

    public void SendRequestResume()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode || !this.mResumeMultiPlay || (this.mResumeSend || (double) this.mRestResumeRequestInterval > 0.0))
        return;
      this.mRestResumeRequestInterval = this.RESUME_REQUEST_INTERVAL;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      byte[] sendBinary = this.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.REQUEST_RESUME, 0, (List<SceneBattle.MultiPlayInput>) null);
      instance.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
      List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          MyPhoton.MyPlayer player = enumerator.Current;
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find((Predicate<SceneBattle.MultiPlayer>) (p => p.PlayerID == player.playerID));
          if (player.start)
          {
            this.mResumeAlreadyStartPlayer.Add(player.playerID);
            if (multiPlayer != null)
              multiPlayer.FinishLoad = true;
          }
        }
      }
    }

    public int SearchUnitIndex(Unit target)
    {
      if (target == null)
        return -1;
      for (int index = 0; index < this.Battle.AllUnits.Count; ++index)
      {
        if (this.Battle.AllUnits[index].Equals((object) target))
          return index;
      }
      return -1;
    }

    public void SendResumeInfo()
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (!this.Battle.IsResume || !instance.IsHost() || (this.mResumeSend || this.mRecvResumeRequest.Count <= 0))
        return;
      byte[] sendResumeInfo = this.CreateSendResumeInfo();
      if (sendResumeInfo != null)
        this.mResumeSend = instance.SendRoomMessageBinary(true, sendResumeInfo, MyPhoton.SEND_TYPE.Resume, false);
      Debug.Log((object) "Send ResumeInfo!!!");
    }

    private byte[] CreateSendResumeInfo()
    {
      MultiPlayResumeParam multiPlayResumeParam = new MultiPlayResumeParam();
      multiPlayResumeParam.unit = new MultiPlayResumeUnitData[this.Battle.AllUnits.Count];
      string msg = string.Empty;
      for (int index1 = 0; index1 < this.Battle.AllUnits.Count; ++index1)
      {
        Unit allUnit = this.Battle.AllUnits[index1];
        if (allUnit != null)
        {
          multiPlayResumeParam.unit[index1] = new MultiPlayResumeUnitData();
          BaseStatus currentStatus = allUnit.CurrentStatus;
          MultiPlayResumeUnitData playResumeUnitData = multiPlayResumeParam.unit[index1];
          playResumeUnitData.name = allUnit.UnitName;
          playResumeUnitData.hp = (int) currentStatus.param.hp;
          playResumeUnitData.chp = allUnit.UnitChangedHp;
          playResumeUnitData.gem = allUnit.Gems;
          playResumeUnitData.dir = (int) allUnit.Direction;
          playResumeUnitData.x = allUnit.x;
          playResumeUnitData.y = allUnit.y;
          playResumeUnitData.target = this.SearchUnitIndex(allUnit.Target);
          playResumeUnitData.ragetarget = this.SearchUnitIndex(allUnit.RageTarget);
          playResumeUnitData.aiindex = (int) allUnit.AIActionIndex;
          playResumeUnitData.aiturn = (int) allUnit.AIActionTurnCount;
          playResumeUnitData.aipatrol = (int) allUnit.AIPatrolIndex;
          playResumeUnitData.flag = allUnit.UnitFlag;
          playResumeUnitData.casttarget = -1;
          if (allUnit.CastSkill != null)
          {
            playResumeUnitData.castskill = allUnit.CastSkill.SkillParam.iname;
            playResumeUnitData.casttime = (int) allUnit.CastTime;
            playResumeUnitData.castindex = (int) allUnit.CastIndex;
            if (allUnit.CastSkillGridMap != null)
            {
              playResumeUnitData.grid_w = allUnit.CastSkillGridMap.w;
              playResumeUnitData.grid_h = allUnit.CastSkillGridMap.h;
              if (allUnit.CastSkillGridMap.data != null)
                playResumeUnitData.castgrid = new int[allUnit.CastSkillGridMap.data.Length];
              for (int index2 = 0; index2 < allUnit.CastSkillGridMap.data.Length; ++index2)
                playResumeUnitData.castgrid[index2] = !allUnit.CastSkillGridMap.data[index2] ? 0 : 1;
            }
            playResumeUnitData.ctx = allUnit.GridTarget == null ? -1 : allUnit.GridTarget.x;
            playResumeUnitData.cty = allUnit.GridTarget == null ? -1 : allUnit.GridTarget.y;
            playResumeUnitData.casttarget = this.SearchUnitIndex(allUnit.UnitTarget);
          }
          playResumeUnitData.chargetime = (int) allUnit.ChargeTime;
          playResumeUnitData.isDead = !allUnit.IsGimmick ? (!allUnit.IsDead ? 0 : 1) : (!allUnit.IsDisableGimmick() ? 0 : 1);
          playResumeUnitData.deathcnt = allUnit.DeathCount;
          playResumeUnitData.autojewel = allUnit.AutoJewel;
          playResumeUnitData.waitturn = allUnit.WaitClock;
          playResumeUnitData.moveturn = allUnit.WaitMoveTurn;
          playResumeUnitData.actcnt = allUnit.ActionCount;
          playResumeUnitData.turncnt = allUnit.TurnCount;
          playResumeUnitData.search = !allUnit.IsUnitFlag(EUnitFlag.Searched) ? 0 : 1;
          playResumeUnitData.entry = !allUnit.IsUnitFlag(EUnitFlag.Reinforcement) ? 0 : 1;
          playResumeUnitData.to_dying = !allUnit.IsUnitFlag(EUnitFlag.ToDying) ? 0 : 1;
          playResumeUnitData.paralyse = !allUnit.IsUnitFlag(EUnitFlag.Paralysed) ? 0 : 1;
          playResumeUnitData.trgcnt = 0;
          if (allUnit.EventTrigger != null)
            playResumeUnitData.trgcnt = allUnit.EventTrigger.Count;
          playResumeUnitData.killcnt = allUnit.KillCount;
          playResumeUnitData.boi = allUnit.CreateBreakObjId;
          playResumeUnitData.boc = allUnit.CreateBreakObjClock;
          playResumeUnitData.own = allUnit.OwnerPlayerIndex;
          if (allUnit.EntryTriggers != null)
          {
            playResumeUnitData.etr = new int[allUnit.EntryTriggers.Count];
            for (int index2 = 0; index2 < allUnit.EntryTriggers.Count; ++index2)
              playResumeUnitData.etr[index2] = !allUnit.EntryTriggers[index2].on ? 0 : 1;
          }
          if (allUnit.AbilityChangeLists.Count != 0)
          {
            playResumeUnitData.abilchgs = new MultiPlayResumeAbilChg[allUnit.AbilityChangeLists.Count];
            for (int index2 = 0; index2 < allUnit.AbilityChangeLists.Count; ++index2)
            {
              Unit.AbilityChange abilityChangeList = allUnit.AbilityChangeLists[index2];
              if (abilityChangeList != null && abilityChangeList.mDataLists.Count != 0)
              {
                playResumeUnitData.abilchgs[index2] = new MultiPlayResumeAbilChg();
                playResumeUnitData.abilchgs[index2].acd = new MultiPlayResumeAbilChg.Data[abilityChangeList.mDataLists.Count];
                for (int index3 = 0; index3 < abilityChangeList.mDataLists.Count; ++index3)
                {
                  Unit.AbilityChange.Data mDataList = abilityChangeList.mDataLists[index3];
                  playResumeUnitData.abilchgs[index2].acd[index3] = new MultiPlayResumeAbilChg.Data();
                  playResumeUnitData.abilchgs[index2].acd[index3].fid = mDataList.mFromAp.iname;
                  playResumeUnitData.abilchgs[index2].acd[index3].tid = mDataList.mToAp.iname;
                  playResumeUnitData.abilchgs[index2].acd[index3].tur = mDataList.mTurn;
                  playResumeUnitData.abilchgs[index2].acd[index3].irs = !mDataList.mIsReset ? 0 : 1;
                  playResumeUnitData.abilchgs[index2].acd[index3].exp = mDataList.mExp;
                  playResumeUnitData.abilchgs[index2].acd[index3].iif = !mDataList.mIsInfinite ? 0 : 1;
                }
              }
            }
          }
          if (allUnit.AddedAbilitys.Count != 0)
          {
            playResumeUnitData.addedabils = new MultiPlayResumeAddedAbil[allUnit.AddedAbilitys.Count];
            for (int index2 = 0; index2 < allUnit.AddedAbilitys.Count; ++index2)
            {
              AbilityData addedAbility = allUnit.AddedAbilitys[index2];
              playResumeUnitData.addedabils[index2] = new MultiPlayResumeAddedAbil();
              playResumeUnitData.addedabils[index2].aid = addedAbility.AbilityID;
              playResumeUnitData.addedabils[index2].exp = addedAbility.Exp;
            }
          }
          Dictionary<SkillData, OInt> skillUseCount = allUnit.GetSkillUseCount();
          int count = skillUseCount.Keys.Count;
          if (count > 0)
          {
            playResumeUnitData.skillname = new string[count];
            playResumeUnitData.skillcnt = new int[count];
            for (int index2 = 0; index2 < count; ++index2)
            {
              playResumeUnitData.skillname[index2] = skillUseCount.Keys.ToArray<SkillData>()[index2].SkillParam.iname;
              playResumeUnitData.skillcnt[index2] = (int) skillUseCount.Values.ToArray<OInt>()[index2];
            }
          }
          List<BuffAttachment> buffAttachments = allUnit.BuffAttachments;
          if (buffAttachments.Count > 0)
          {
            List<MultiPlayResumeBuff> multiPlayResumeBuffList = new List<MultiPlayResumeBuff>();
            for (int index2 = 0; index2 < buffAttachments.Count; ++index2)
            {
              if (buffAttachments[index2].CheckTiming != EffectCheckTimings.Moment && buffAttachments[index2].CheckTiming != EffectCheckTimings.MomentStart)
              {
                MultiPlayResumeBuff multiPlayResumeBuff = new MultiPlayResumeBuff();
                multiPlayResumeBuff.unitindex = this.SearchUnitIndex(buffAttachments[index2].user);
                multiPlayResumeBuff.checkunit = this.SearchUnitIndex(buffAttachments[index2].CheckTarget);
                multiPlayResumeBuff.timing = (int) buffAttachments[index2].CheckTiming;
                multiPlayResumeBuff.condition = (int) buffAttachments[index2].UseCondition;
                multiPlayResumeBuff.turn = (int) buffAttachments[index2].turn;
                multiPlayResumeBuff.passive = (bool) buffAttachments[index2].IsPassive;
                multiPlayResumeBuff.type = (int) buffAttachments[index2].BuffType;
                multiPlayResumeBuff.vtp = !buffAttachments[index2].IsNegativeValueIsBuff ? 0 : 1;
                multiPlayResumeBuff.calc = (int) buffAttachments[index2].CalcType;
                multiPlayResumeBuff.skilltarget = (int) buffAttachments[index2].skilltarget;
                multiPlayResumeBuff.lid = buffAttachments[index2].LinkageID;
                multiPlayResumeBuff.ubc = (int) buffAttachments[index2].UpBuffCount;
                multiPlayResumeBuff.atl.Clear();
                if (buffAttachments[index2].AagTargetLists != null)
                {
                  for (int index3 = 0; index3 < buffAttachments[index2].AagTargetLists.Count; ++index3)
                  {
                    int num = this.SearchUnitIndex(buffAttachments[index2].AagTargetLists[index3]);
                    if (num >= 0)
                      multiPlayResumeBuff.atl.Add(num);
                  }
                }
                multiPlayResumeBuff.iname = (string) null;
                if (buffAttachments[index2].skill != null)
                  multiPlayResumeBuff.iname = buffAttachments[index2].skill.SkillParam.iname;
                multiPlayResumeBuffList.Add(multiPlayResumeBuff);
              }
            }
            playResumeUnitData.buff = multiPlayResumeBuffList.ToArray();
          }
          List<CondAttachment> condAttachments = allUnit.CondAttachments;
          if (condAttachments.Count > 0)
          {
            playResumeUnitData.cond = new MultiPlayResumeBuff[condAttachments.Count];
            for (int index2 = 0; index2 < condAttachments.Count; ++index2)
            {
              playResumeUnitData.cond[index2] = new MultiPlayResumeBuff();
              playResumeUnitData.cond[index2].unitindex = this.SearchUnitIndex(condAttachments[index2].user);
              playResumeUnitData.cond[index2].checkunit = this.SearchUnitIndex(condAttachments[index2].CheckTarget);
              playResumeUnitData.cond[index2].timing = (int) condAttachments[index2].CheckTiming;
              playResumeUnitData.cond[index2].condition = (int) condAttachments[index2].UseCondition;
              playResumeUnitData.cond[index2].turn = (int) condAttachments[index2].turn;
              playResumeUnitData.cond[index2].passive = (bool) condAttachments[index2].IsPassive;
              playResumeUnitData.cond[index2].type = (int) condAttachments[index2].CondType;
              playResumeUnitData.cond[index2].calc = (int) condAttachments[index2].Condition;
              playResumeUnitData.cond[index2].curse = !condAttachments[index2].IsCurse ? 0 : 1;
              playResumeUnitData.cond[index2].skilltarget = (int) condAttachments[index2].skilltarget;
              playResumeUnitData.cond[index2].bc_id = condAttachments[index2].CondId;
              playResumeUnitData.cond[index2].lid = condAttachments[index2].LinkageID;
              playResumeUnitData.cond[index2].iname = (string) null;
              if (condAttachments[index2].skill != null)
                playResumeUnitData.cond[index2].iname = condAttachments[index2].skill.SkillParam.iname;
            }
          }
          if (allUnit.Shields != null && allUnit.Shields.Count != 0)
          {
            playResumeUnitData.shields = new MultiPlayResumeShield[allUnit.Shields.Count];
            for (int index2 = 0; index2 < allUnit.Shields.Count; ++index2)
            {
              Unit.UnitShield shield = allUnit.Shields[index2];
              playResumeUnitData.shields[index2] = new MultiPlayResumeShield();
              playResumeUnitData.shields[index2].inm = shield.skill_param.iname;
              playResumeUnitData.shields[index2].nhp = (int) shield.hp;
              playResumeUnitData.shields[index2].mhp = (int) shield.hpMax;
              playResumeUnitData.shields[index2].ntu = (int) shield.turn;
              playResumeUnitData.shields[index2].mtu = (int) shield.turnMax;
              playResumeUnitData.shields[index2].drt = (int) shield.damage_rate;
              playResumeUnitData.shields[index2].dvl = (int) shield.damage_value;
            }
          }
          if (allUnit.JudgeHpLists != null && allUnit.JudgeHpLists.Count != 0)
          {
            playResumeUnitData.hpis = new string[allUnit.JudgeHpLists.Count];
            for (int index2 = 0; index2 < allUnit.JudgeHpLists.Count; ++index2)
              playResumeUnitData.hpis[index2] = allUnit.JudgeHpLists[index2].SkillID;
          }
          if (allUnit.MhmDamageLists != null && allUnit.MhmDamageLists.Count != 0)
          {
            playResumeUnitData.mhm_dmgs = new MultiPlayResumeMhmDmg[allUnit.MhmDamageLists.Count];
            for (int index2 = 0; index2 < allUnit.MhmDamageLists.Count; ++index2)
            {
              Unit.UnitMhmDamage mhmDamageList = allUnit.MhmDamageLists[index2];
              playResumeUnitData.mhm_dmgs[index2] = new MultiPlayResumeMhmDmg();
              playResumeUnitData.mhm_dmgs[index2].typ = (int) mhmDamageList.mType;
              playResumeUnitData.mhm_dmgs[index2].dmg = (int) mhmDamageList.mDamage;
            }
          }
          msg = msg + allUnit.UnitName + "- pos:" + (object) allUnit.x + "," + (object) allUnit.y;
        }
      }
      DebugUtility.LogWarning(msg);
      multiPlayResumeParam.unitcastindex = (int) Unit.UNIT_CAST_INDEX;
      RandXorshift randXorshift1 = this.Battle.CloneRand();
      RandXorshift randXorshift2 = this.Battle.CloneRandDamage();
      uint[] seed1 = randXorshift1.GetSeed();
      uint[] seed2 = randXorshift2.GetSeed();
      multiPlayResumeParam.rndseed = new uint[4];
      multiPlayResumeParam.dmgrndseed = new uint[4];
      for (int index = 0; index < 4; ++index)
      {
        multiPlayResumeParam.rndseed[index] = seed1[index];
        multiPlayResumeParam.dmgrndseed[index] = seed2[index];
      }
      multiPlayResumeParam.seed = this.Battle.Seed;
      multiPlayResumeParam.damageseed = this.Battle.DamageSeed;
      multiPlayResumeParam.unitstartcount = this.UnitStartCountTotal;
      multiPlayResumeParam.treasurecount = this.TreasureCount;
      multiPlayResumeParam.versusturn = this.Battle.VersusTurnCount;
      multiPlayResumeParam.ctm = this.Battle.ClockTime;
      multiPlayResumeParam.ctt = this.Battle.ClockTimeTotal;
      if (this.mRecvResumeRequest != null && this.mRecvResumeRequest.Count > 0)
        multiPlayResumeParam.resumeID = this.mRecvResumeRequest[0].pidx;
      if (this.mRecvResumeRequest.Count > 1)
      {
        multiPlayResumeParam.otherresume = new int[this.mRecvResumeRequest.Count - 1];
        for (int index = 1; index < this.mRecvResumeRequest.Count; ++index)
          multiPlayResumeParam.otherresume[index - 1] = this.mRecvResumeRequest[index].pidx;
      }
      List<GimmickEvent> gimmickEventList = this.Battle.GimmickEventList;
      if (gimmickEventList.Count > 0)
      {
        multiPlayResumeParam.gimmick = new MultiPlayGimmickEventParam[gimmickEventList.Count];
        for (int index = 0; index < gimmickEventList.Count; ++index)
        {
          multiPlayResumeParam.gimmick[index] = new MultiPlayGimmickEventParam();
          multiPlayResumeParam.gimmick[index].count = gimmickEventList[index].count;
          multiPlayResumeParam.gimmick[index].completed = !gimmickEventList[index].IsCompleted ? 0 : 1;
        }
      }
      List<TrickData> effectAll = TrickData.GetEffectAll();
      if (effectAll.Count > 0)
      {
        multiPlayResumeParam.trick = new MultiPlayTrickParam[effectAll.Count];
        for (int index = 0; index < effectAll.Count; ++index)
        {
          multiPlayResumeParam.trick[index] = new MultiPlayTrickParam();
          multiPlayResumeParam.trick[index].tid = effectAll[index].TrickParam.Iname;
          multiPlayResumeParam.trick[index].val = (bool) effectAll[index].Valid;
          multiPlayResumeParam.trick[index].cun = this.SearchUnitIndex(effectAll[index].CreateUnit);
          multiPlayResumeParam.trick[index].rnk = (int) effectAll[index].Rank;
          multiPlayResumeParam.trick[index].rcp = (int) effectAll[index].RankCap;
          multiPlayResumeParam.trick[index].grx = (int) effectAll[index].GridX;
          multiPlayResumeParam.trick[index].gry = (int) effectAll[index].GridY;
          multiPlayResumeParam.trick[index].rac = (int) effectAll[index].RestActionCount;
          multiPlayResumeParam.trick[index].ccl = (int) effectAll[index].CreateClock;
          multiPlayResumeParam.trick[index].tag = effectAll[index].Tag;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEventScript, (UnityEngine.Object) null) && this.mEventScript.mSequences != null && this.mEventScript.mSequences.Length != 0)
      {
        multiPlayResumeParam.scr_ev_trg = new bool[this.mEventScript.mSequences.Length];
        for (int index = 0; index < this.mEventScript.mSequences.Length; ++index)
          multiPlayResumeParam.scr_ev_trg[index] = this.mEventScript.mSequences[index].Triggered;
      }
      multiPlayResumeParam.wti.wid = (string) null;
      WeatherData currentWeatherData = WeatherData.CurrentWeatherData;
      if (currentWeatherData != null)
      {
        multiPlayResumeParam.wti.wid = currentWeatherData.WeatherParam.Iname;
        multiPlayResumeParam.wti.mun = this.SearchUnitIndex(currentWeatherData.ModifyUnit);
        multiPlayResumeParam.wti.rnk = (int) currentWeatherData.Rank;
        multiPlayResumeParam.wti.rcp = (int) currentWeatherData.RankCap;
        multiPlayResumeParam.wti.ccl = (int) currentWeatherData.ChangeClock;
      }
      return GameUtility.Object2Binary<MultiPlayResumeParam>(multiPlayResumeParam);
    }

    [DebuggerHidden]
    private IEnumerator RecvResume(byte[] resumedata)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CRecvResume\u003Ec__Iterator33()
      {
        resumedata = resumedata,
        \u003C\u0024\u003Eresumedata = resumedata,
        \u003C\u003Ef__this = this
      };
    }

    private void RecvBuildBuffAndCond(ref List<MultiPlayResumeSkillData> buff, ref List<MultiPlayResumeSkillData> cond, MultiPlayResumeUnitData unit)
    {
      buff.Clear();
      if (unit.buff != null)
      {
        for (int index = 0; index < unit.buff.Length; ++index)
        {
          MultiPlayResumeBuff multiPlayResumeBuff = unit.buff[index];
          MultiPlayResumeSkillData playResumeSkillData = new MultiPlayResumeSkillData();
          playResumeSkillData.user = multiPlayResumeBuff.unitindex == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.unitindex];
          playResumeSkillData.check = multiPlayResumeBuff.checkunit == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.checkunit];
          playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.GetSkillData(multiPlayResumeBuff.iname);
          if (playResumeSkillData.skill == null)
            playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.SearchArtifactSkill(multiPlayResumeBuff.iname);
          buff.Add(playResumeSkillData);
        }
      }
      cond.Clear();
      if (unit.cond == null)
        return;
      for (int index = 0; index < unit.cond.Length; ++index)
      {
        MultiPlayResumeBuff multiPlayResumeBuff = unit.cond[index];
        MultiPlayResumeSkillData playResumeSkillData = new MultiPlayResumeSkillData();
        playResumeSkillData.user = multiPlayResumeBuff.unitindex == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.unitindex];
        playResumeSkillData.check = multiPlayResumeBuff.checkunit == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.checkunit];
        playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.GetSkillData(multiPlayResumeBuff.iname);
        if (playResumeSkillData.skill == null)
          playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.SearchArtifactSkill(multiPlayResumeBuff.iname);
        cond.Add(playResumeSkillData);
      }
    }

    private void RecvUnitDead(TacticsUnitController controller, Unit target)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) controller, (UnityEngine.Object) null) || target == null)
        return;
      if (target.IsDead)
      {
        this.Battle.ResumeDead(target);
        ((Component) controller).get_gameObject().SetActive(false);
        controller.ShowHPGauge(false);
        controller.ShowVersusCursor(false);
        this.mTacticsUnits.Remove(controller);
      }
      else
      {
        if (controller.IsJumpCant())
          controller.SetCastJump();
        controller.UpdateShields(true);
        controller.ClearBadStatusLocks();
        controller.UpdateBadStatus();
      }
      if (!target.IsGimmick)
        return;
      if (target.IsBreakObj)
        controller.ReflectDispModel();
      if (!target.IsDisableGimmick())
        return;
      ((Component) controller).get_gameObject().SetActive(false);
    }

    [DebuggerHidden]
    private IEnumerator RecvReinforcementUnit()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CRecvReinforcementUnit\u003Ec__Iterator34()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator RestoreWeather(WeatherData wth_data)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CRestoreWeather\u003Ec__Iterator35()
      {
        wth_data = wth_data,
        \u003C\u0024\u003Ewth_data = wth_data,
        \u003C\u003Ef__this = this
      };
    }

    public void SendCheat(SceneBattle.CHEAT_TYPE type, int uid)
    {
      if (MonoSingleton<GameManager>.Instance.AudienceMode)
        return;
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessageBinary(true, this.CreateSendBinary((SceneBattle.EMultiPlayRecvDataHeader) (14 + type), uid, (List<SceneBattle.MultiPlayInput>) null), MyPhoton.SEND_TYPE.Normal, false);
    }

    public BattleCore.QuestResult CheckAudienceResult()
    {
      BattleCore.QuestResult questResult = BattleCore.QuestResult.Pending;
      if (this.Battle.IsVSForceWin)
      {
        using (List<SceneBattle.MultiPlayer>.Enumerator enumerator = this.mMultiPlayer.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            SceneBattle.MultiPlayer current = enumerator.Current;
            if (current != null && current.NotifyDisconnected)
            {
              questResult = current.PlayerIndex != 1 ? BattleCore.QuestResult.Win : BattleCore.QuestResult.Lose;
              break;
            }
          }
        }
      }
      else
        questResult = this.Battle.GetQuestResult();
      return questResult;
    }

    private void UpdateInterval()
    {
      this.mRestSyncInterval = Mathf.Max(this.mRestSyncInterval - Time.get_deltaTime(), 0.0f);
      this.mRestResumeRequestInterval = Mathf.Max(this.mRestResumeRequestInterval - Time.get_deltaTime(), 0.0f);
      this.mRestResumeSuccessInterval = Mathf.Max(this.mRestResumeSuccessInterval - Time.get_deltaTime(), 0.0f);
    }

    public SceneBattle.MoveInput VirtualStickMoveInput
    {
      get
      {
        return this.mMoveInput;
      }
    }

    public EventScript EventScript
    {
      get
      {
        return this.mEventScript;
      }
    }

    public GameObject continueWindowRes
    {
      get
      {
        return this.mContinueWindowRes;
      }
    }

    private UnitGauge GetGaugeTemplateFor(Unit unit)
    {
      if ((unit.Side == EUnitSide.Enemy || unit.IsBreakObj) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.EnemyGaugeOverlayTemplate, (UnityEngine.Object) null))
        return this.EnemyGaugeOverlayTemplate;
      return this.GaugeOverlayTemplate;
    }

    public void OnGimmickUpdate()
    {
      List<TacticsUnitController> tacticsUnitControllerList1 = new List<TacticsUnitController>();
      List<TacticsUnitController> tacticsUnitControllerList2 = new List<TacticsUnitController>();
      for (int index = 0; index < this.mBattle.Units.Count; ++index)
      {
        TacticsUnitController unitController = this.FindUnitController(this.mBattle.Units[index]);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          if (this.mBattle.Units[index].IsGimmick && !this.mBattle.Units[index].IsDisableGimmick() && !this.mBattle.Units[index].IsBreakObj)
            tacticsUnitControllerList2.Add(unitController);
          else if (!this.mBattle.Units[index].IsDead && !this.mBattle.Units[index].IsJump)
            tacticsUnitControllerList1.Add(unitController);
        }
      }
      for (int index1 = 0; index1 < tacticsUnitControllerList1.Count; ++index1)
      {
        TacticsUnitController tacticsUnitController1 = tacticsUnitControllerList1[index1];
        IntVector2 intVector2_1 = this.CalcCoord(tacticsUnitController1.CenterPosition);
        bool flag = false;
        for (int index2 = 0; index2 < tacticsUnitControllerList2.Count; ++index2)
        {
          TacticsUnitController tacticsUnitController2 = tacticsUnitControllerList2[index2];
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) tacticsUnitController1, (UnityEngine.Object) tacticsUnitController2))
          {
            IntVector2 intVector2_2 = this.CalcCoord(tacticsUnitController2.CenterPosition);
            if (intVector2_1 == intVector2_2)
            {
              if (tacticsUnitControllerList1[index1].Unit.CastSkill == null)
              {
                flag = true;
                tacticsUnitController1.SetGimmickIcon(tacticsUnitControllerList2[index2].Unit);
              }
              tacticsUnitController2.ScaleHide();
              tacticsUnitControllerList2.Remove(tacticsUnitController2);
              break;
            }
            tacticsUnitController2.ScaleShow();
          }
        }
        if (!flag)
        {
          if (tacticsUnitControllerList1[index1].Unit == this.Battle.CurrentUnit)
            tacticsUnitController1.HideGimmickIcon(tacticsUnitControllerList1[index1].Unit.UnitType);
          else
            tacticsUnitController1.DeleteGimmickIconAll();
        }
      }
    }

    private void DeleteOnGimmickIcon()
    {
      for (int index = 0; index < this.mBattle.Units.Count; ++index)
      {
        if (!this.mBattle.Units[index].IsGimmick)
        {
          TacticsUnitController unitController = this.FindUnitController(this.mBattle.Units[index]);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            unitController.DeleteGimmickIconAll();
        }
      }
    }

    private void SetUnitUiHeight(Unit FocusUnit)
    {
      MapHeight gameObject = GameObjectID.FindGameObject<MapHeight>(this.mBattleUI.MapHeightID);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      gameObject.FocusUnit = FocusUnit;
    }

    private void SetUiHeight(int Height)
    {
      MapHeight gameObject = GameObjectID.FindGameObject<MapHeight>(this.mBattleUI.MapHeightID);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      gameObject.FocusUnit = (Unit) null;
      gameObject.Height = Height;
    }

    private void ArenaActionCountEnable(bool enable)
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.mBattleUI.ArenaActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        return;
      gameObject.SetActive(enable);
    }

    private void ArenaActionCountSet(uint count)
    {
      ArenaActionCount gameObject = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.ArenaActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject) || (int) gameObject.ActionCount == (int) count)
        return;
      gameObject.ActionCount = count;
      gameObject.PlayEffect();
    }

    private void RemainingActionCountEnable(bool pc_enable, bool ec_enable)
    {
      GameObject gameObject1 = GameObjectID.FindGameObject(this.mBattleUI.PlayerActionCountID);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject1))
        gameObject1.SetActive(pc_enable);
      GameObject gameObject2 = GameObjectID.FindGameObject(this.mBattleUI.EnemyActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject2))
        return;
      gameObject2.SetActive(ec_enable);
    }

    private void RemainingActionCountSet(uint pc_count, uint ec_count)
    {
      ArenaActionCount gameObject1 = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.PlayerActionCountID);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null) && ((Behaviour) gameObject1).get_isActiveAndEnabled() && (int) gameObject1.ActionCount != (int) pc_count)
      {
        gameObject1.ActionCount = pc_count;
        gameObject1.PlayEffect();
      }
      ArenaActionCount gameObject2 = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.EnemyActionCountID);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null) || !((Behaviour) gameObject2).get_isActiveAndEnabled() || (int) gameObject2.ActionCount == (int) ec_count)
        return;
      gameObject2.ActionCount = ec_count;
      gameObject2.PlayEffect();
    }

    private void ReflectWeatherInfo(WeatherData wd = null)
    {
      if (wd == null)
        wd = WeatherData.CurrentWeatherData;
      WeatherInfo gameObject = GameObjectID.FindGameObject<WeatherInfo>(this.mBattleUI.WeatherInfoID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        return;
      gameObject.Refresh(wd);
    }

    private GameObject GetWeatherEffectAttach()
    {
      return this.mGoWeatherAttach;
    }

    private void EnableWeatherEffect(bool is_enable)
    {
      GameObject weatherEffectAttach = this.GetWeatherEffectAttach();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) weatherEffectAttach))
        return;
      weatherEffectAttach.SetActive(is_enable);
    }

    private void RankingQuestActionCountEnable(bool enable)
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.mBattleUI.RankingQuestActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        return;
      gameObject.SetActive(enable);
    }

    private void RankingQuestActionCountSet(uint count)
    {
      RankingQuestActionCount gameObject = GameObjectID.FindGameObject<RankingQuestActionCount>(this.mBattleUI.RankingQuestActionCountID);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject) || (int) gameObject.ActionCount == (int) count)
        return;
      gameObject.ActionCount = count;
      gameObject.PlayEffect();
    }

    private void StepToNear(Unit unit)
    {
      TacticsUnitController unitController = this.FindUnitController(unit);
      IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
      Grid current = this.mBattle.CurrentMap[intVector2.x, intVector2.y];
      unitController.StepTo(this.CalcGridCenter(current));
    }

    private void GotoState_WaitSignal<T>() where T : State<SceneBattle>, new()
    {
      this.GotoState<SceneBattle.State_WaitSignal<T>>();
    }

    private void ReflectUnitChgButton(Unit unit, bool is_update = false)
    {
      bool is_active;
      if (!is_update)
      {
        this.mIsUnitChgActive = false;
        if (unit.Side == EUnitSide.Player && !unit.IsUnitFlag(EUnitFlag.DisableUnitChange) && (!unit.IsUnitFlag(EUnitFlag.Moved | EUnitFlag.Action) && this.mBattle.Player != null))
        {
          using (List<Unit>.Enumerator enumerator = this.mBattle.Player.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              if (!this.mBattle.StartingMembers.Contains(enumerator.Current))
              {
                this.mIsUnitChgActive = true;
                break;
              }
            }
          }
        }
        is_active = this.mIsUnitChgActive;
      }
      else
      {
        if (!this.mBattle.IsUnitChange || !this.mIsUnitChgActive || !this.mBattleUI.CommandWindow.IsEnableUnitChgButton)
          return;
        is_active = true;
        TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
          if (intVector2.x != unit.x || intVector2.y != unit.y)
            is_active = false;
        }
      }
      this.mBattleUI.CommandWindow.EnableUnitChgButton(this.mBattle.IsUnitChange, is_active);
    }

    private void RefreshMapCommands()
    {
      Unit currentUnit = this.mBattle.CurrentUnit;
      UnitCommands.ButtonTypes buttonTypes1 = (UnitCommands.ButtonTypes) 0;
      if (currentUnit.IsEnableMoveCondition(false))
        buttonTypes1 |= UnitCommands.ButtonTypes.Move;
      if (!currentUnit.IsUnitFlag(EUnitFlag.Action))
      {
        buttonTypes1 |= UnitCommands.ButtonTypes.Action;
        if (currentUnit.IsEnableItemCondition(false) && !this.mBattle.CheckDisableItems(currentUnit))
          buttonTypes1 |= UnitCommands.ButtonTypes.Item;
        if (currentUnit.IsEnableAttackCondition(false))
        {
          buttonTypes1 |= UnitCommands.ButtonTypes.Attack;
          if (this.mBattle.HelperUnits.Count > 0)
            buttonTypes1 |= UnitCommands.ButtonTypes.IsRenkei;
        }
        if (currentUnit.IsEnableSkillCondition(false) && !this.mBattle.CheckDisableAbilities(currentUnit))
          buttonTypes1 |= UnitCommands.ButtonTypes.Skill;
      }
      UnitCommands.ButtonTypes buttonTypes2 = buttonTypes1 | UnitCommands.ButtonTypes.Misc | UnitCommands.ButtonTypes.Map;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.CommandWindow, (UnityEngine.Object) null))
        return;
      this.mBattleUI.CommandWindow.CancelButton.SetActive(true);
      this.mBattleUI.CommandWindow.OKButton.SetActive(true);
      this.mBattleUI.CommandWindow.VisibleButtons = buttonTypes2;
    }

    private void RefreshOnlyMapCommand()
    {
      UnitCommands.ButtonTypes buttonTypes = (UnitCommands.ButtonTypes) (0 | 256);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.CommandWindow, (UnityEngine.Object) null))
        return;
      this.mBattleUI.CommandWindow.CancelButton.SetActive(false);
      this.mBattleUI.CommandWindow.OKButton.SetActive(false);
      this.mBattleUI.CommandWindow.VisibleButtons = buttonTypes;
    }

    private void GotoMapViewMode()
    {
      this.mBattleUI.OnMapViewStart();
      this.GotoSelectTarget((SkillData) null, new SceneBattle.SelectTargetCallback(this.GotoMapCommand), (SceneBattle.SelectTargetPositionWithSkill) null, (Unit) null, true);
    }

    private void GotoItemSelect()
    {
      this.mBattleUI.OnItemSelectStart();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.ItemWindow, (UnityEngine.Object) null))
        this.mBattleUI.ItemWindow.Refresh();
      this.GotoState_WaitSignal<SceneBattle.State_SelectItemV2>();
    }

    private void OnSelectItemTarget(int x, int y, ItemData item)
    {
      if (!this.ApplyUnitMovement(false))
        return;
      this.HideAllHPGauges();
      this.HideAllUnitOwnerIndex();
      if (this.Battle.IsMultiPlay)
      {
        Debug.LogError((object) "Item! Cheat!");
        this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }
      else
      {
        this.mBattle.UseItem(this.mBattle.CurrentUnit, x, y, item);
        this.mBattle.SetManualPlayFlag();
        this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }
    }

    private void GotoUnitChgSelect(bool is_back = false)
    {
      this.mBattleUI.OnUnitChgSelectStart();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI.UnitChgWindow, (UnityEngine.Object) null))
        this.mBattleUI.UnitChgWindow.Refresh();
      if (!is_back)
      {
        this.HideGrid();
        TacticsUnitController unitController = this.FindUnitController(this.Battle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          IntVector2 intVector2 = this.CalcCoord(unitController.CenterPosition);
          GridMap<Color32> grid1 = new GridMap<Color32>(this.Battle.CurrentMap.Width, this.Battle.CurrentMap.Height);
          grid1.set(intVector2.x, intVector2.y, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea));
          GridMap<Color32> grid2 = new GridMap<Color32>(this.Battle.CurrentMap.Width, this.Battle.CurrentMap.Height);
          grid2.set(intVector2.x, intVector2.y, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea2));
          this.mTacticsSceneRoot.ShowGridLayer(1, grid1, true);
          this.mTacticsSceneRoot.ShowGridLayer(2, grid2, false);
        }
      }
      this.GotoState_WaitSignal<SceneBattle.State_SelectUnitChgV2>();
    }

    private void GotoSelectTarget(SkillData skill, SceneBattle.SelectTargetCallback cancel, SceneBattle.SelectTargetPositionWithSkill accept, Unit defaultTarget = null, bool allowTargetChange = true)
    {
      this.mTargetSelectorParam.Skill = skill;
      this.mTargetSelectorParam.Item = (ItemData) null;
      this.mTargetSelectorParam.OnAccept = (object) accept;
      this.mTargetSelectorParam.OnCancel = cancel;
      this.mTargetSelectorParam.DefaultTarget = defaultTarget;
      this.mTargetSelectorParam.AllowTargetChange = allowTargetChange;
      this.mTargetSelectorParam.IsThrowTargetSelect = false;
      this.mTargetSelectorParam.DefaultThrowTarget = (Unit) null;
      this.mTargetSelectorParam.ThrowTarget = (Unit) null;
      if (skill != null)
      {
        if (skill.EffectType == SkillEffectTypes.Throw)
        {
          this.mTargetSelectorParam.IsThrowTargetSelect = true;
          this.GotoState_WaitSignal<SceneBattle.State_PreThrowTargetSelect>();
        }
        else
          this.GotoState_WaitSignal<SceneBattle.State_PreSelectTargetV2>();
      }
      else
        this.GotoState_WaitSignal<SceneBattle.State_PreMapviewV2>();
    }

    private void GotoSelectTarget(ItemData item, SceneBattle.SelectTargetCallback cancel, SceneBattle.SelectTargetPositionWithItem accept, Unit defaultTarget = null, bool allowTargetChange = true)
    {
      this.mTargetSelectorParam.Item = item;
      this.mTargetSelectorParam.Skill = item == null ? (SkillData) null : item.Skill;
      this.mTargetSelectorParam.OnAccept = (object) accept;
      this.mTargetSelectorParam.OnCancel = cancel;
      this.mTargetSelectorParam.DefaultTarget = defaultTarget;
      this.mTargetSelectorParam.AllowTargetChange = allowTargetChange;
      this.mTargetSelectorParam.IsThrowTargetSelect = false;
      this.mTargetSelectorParam.DefaultThrowTarget = (Unit) null;
      this.mTargetSelectorParam.ThrowTarget = (Unit) null;
      this.GotoState_WaitSignal<SceneBattle.State_PreSelectTargetV2>();
    }

    private EUnitDirection GetSkillDirectionByTargetArea(Unit unit, int curX, int curY, GridMap<bool> targetArea)
    {
      int x1 = 0;
      int y1 = 0;
      for (int y2 = 0; y2 < targetArea.h; ++y2)
      {
        for (int x2 = 0; x2 < targetArea.w; ++x2)
        {
          if (targetArea.get(x2, y2))
          {
            x1 += x2 - curX;
            y1 += y2 - curY;
          }
        }
      }
      return BattleCore.UnitDirectionFromVector(unit, x1, y1);
    }

    public void SelectUnitDir(EUnitDirection dir)
    {
      if (!this.mState.IsInState<SceneBattle.State_InputDirection>())
        return;
      ((SceneBattle.State_InputDirection) this.mState.State).SelectDirection(dir);
    }

    private void GotoSkillSelect()
    {
      UnitAbilitySkillList skillWindow = this.mBattleUI.SkillWindow;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) skillWindow, (UnityEngine.Object) null))
      {
        DataSource.Bind<AbilityData>(((Component) skillWindow).get_gameObject(), this.UIParam_CurrentAbility);
        skillWindow.Refresh(this.mBattle.CurrentUnit);
      }
      this.mBattleUI.OnSkillSelectStart();
      this.mIsBackSelectSkill = true;
      this.GotoState_WaitSignal<SceneBattle.State_SelectSkillV2>();
      this.mIsBackSelectSkill = false;
    }

    private void OnSelectSkillTarget(int x, int y, SkillData skill, bool bUnitLockTarget)
    {
      if (!this.ApplyUnitMovement(false))
        return;
      this.HideAllHPGauges();
      this.HideAllUnitOwnerIndex();
      if (skill.EffectType == SkillEffectTypes.Throw)
        this.mSelectedTarget = this.mTargetSelectorParam.ThrowTarget;
      if (this.Battle.IsMultiPlay)
        this.SendInputEntryBattle(EBattleCommand.Skill, this.Battle.CurrentUnit, this.mSelectedTarget, skill, (ItemData) null, x, y, bUnitLockTarget);
      if (skill.EffectType == SkillEffectTypes.Throw)
        this.mBattle.UseSkill(this.mBattle.CurrentUnit, x, y, skill, bUnitLockTarget, this.mSelectedTarget.x, this.mSelectedTarget.y, false);
      else
        this.mBattle.UseSkill(this.mBattle.CurrentUnit, x, y, skill, bUnitLockTarget, 0, 0, false);
      this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
    }

    private TutorialButtonImage[] TutorialButtonImages
    {
      get
      {
        if (this.mTutorialButtonImages == null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI, (UnityEngine.Object) null))
          this.mTutorialButtonImages = (TutorialButtonImage[]) ((Component) this.mBattleUI).GetComponentsInChildren<TutorialButtonImage>(true);
        return this.mTutorialButtonImages;
      }
    }

    public bool EnableControlBattleUI(SceneBattle.eMaskBattleUI emask, bool is_enable)
    {
      return this.EnableControlBattleUI((uint) emask, is_enable);
    }

    public bool EnableControlBattleUI(uint mask, bool is_enable)
    {
      if (this.mCurrentQuest == null || this.mCurrentQuest.type != QuestTypes.Tutorial || mask == 0U)
        return false;
      uint controlDisableMask = this.mControlDisableMask;
      if (is_enable)
        this.mControlDisableMask &= ~mask;
      else
        this.mControlDisableMask |= mask;
      List<TutorialButtonImage> tutorialButtonImageList = new List<TutorialButtonImage>((IEnumerable<TutorialButtonImage>) this.TutorialButtonImages);
      for (int index1 = 0; index1 < SceneBattle.MAX_MASK_BATTLE_UI; ++index1)
      {
        SceneBattle.eMaskBattleUI emask = (SceneBattle.eMaskBattleUI) (1 << index1);
        bool flag1 = ((SceneBattle.eMaskBattleUI) this.mControlDisableMask & emask) != (SceneBattle.eMaskBattleUI) 0;
        bool flag2 = ((SceneBattle.eMaskBattleUI) controlDisableMask & emask) != (SceneBattle.eMaskBattleUI) 0;
        if (flag1 != flag2)
        {
          List<TutorialButtonImage> all = tutorialButtonImageList.FindAll((Predicate<TutorialButtonImage>) (tbi => tbi.MaskType == emask));
          for (int index2 = 0; index2 < all.Count; ++index2)
          {
            TutorialButtonImage tutorialButtonImage = all[index2];
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) tutorialButtonImage))
              ((Component) tutorialButtonImage).get_gameObject().SetActive(flag1);
          }
        }
      }
      return (int) controlDisableMask != (int) this.mControlDisableMask;
    }

    public bool IsControlBattleUI(SceneBattle.eMaskBattleUI emask)
    {
      return ((SceneBattle.eMaskBattleUI) this.mControlDisableMask & emask) == (SceneBattle.eMaskBattleUI) 0;
    }

    public GameObject KnockBackEffect
    {
      get
      {
        return this.mKnockBackEffect;
      }
    }

    public GameObject TrickMarker
    {
      get
      {
        return this.mTrickMarker;
      }
    }

    public Dictionary<string, GameObject> TrickMarkerDics
    {
      get
      {
        return this.mTrickMarkerDics;
      }
    }

    public GameObject JumpFallEffect
    {
      get
      {
        return this.mJumpFallEffect;
      }
    }

    private Canvas OverlayCanvas
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null))
          return (Canvas) ((Component) this.mTouchController).GetComponent<Canvas>();
        return (Canvas) null;
      }
    }

    private void ShowAllHPGauges()
    {
      this.ShowPlayerHPGauges();
      this.ShowEnemyHPGauges();
      this.ShowBreakObjHPGauges();
    }

    private bool IsHPGaugeChanging
    {
      get
      {
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          if (this.mTacticsUnits[index].IsHPGaugeChanging)
            return true;
        }
        return false;
      }
    }

    private void HideAllHPGauges()
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        this.mTacticsUnits[index].ShowHPGauge(false);
    }

    private void HideAllUnitOwnerIndex()
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        this.mTacticsUnits[index].ShowOwnerIndexUI(false);
    }

    private void ShowPlayerHPGauges()
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (this.mTacticsUnits[index].Unit.Side == EUnitSide.Player)
          this.mTacticsUnits[index].ShowHPGauge(true);
      }
    }

    private void ShowEnemyHPGauges()
    {
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (this.mTacticsUnits[index].Unit.Side == EUnitSide.Enemy)
          this.mTacticsUnits[index].ShowHPGauge(true);
      }
    }

    private void ShowBreakObjHPGauges()
    {
      using (List<TacticsUnitController>.Enumerator enumerator = this.mTacticsUnits.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TacticsUnitController current = enumerator.Current;
          if (current.Unit.IsBreakObj)
            current.ShowHPGauge(true);
        }
      }
    }

    private void RefreshUnitStatus(Unit unit)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) UnitQueue.Instance, (UnityEngine.Object) null))
        return;
      UnitQueue.Instance.Refresh(unit);
    }

    private float CalcHeight(float x, float y)
    {
      float num1 = 100f;
      float num2 = Mathf.Floor(x - 0.5f) + 0.5f;
      float num3 = Mathf.Floor(y - 0.5f) + 0.5f;
      float num4 = Mathf.Ceil(x - 0.5f) + 0.5f;
      float num5 = Mathf.Ceil(y - 0.5f) + 0.5f;
      float num6 = 0.0f;
      float num7 = 0.0f;
      float num8 = 0.0f;
      float num9 = 0.0f;
      RaycastHit raycastHit;
      if (Physics.Raycast(new Vector3(num2, num1, num3), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
      {
        // ISSUE: explicit reference operation
        num6 = (float) ((RaycastHit) @raycastHit).get_point().y;
      }
      if (Physics.Raycast(new Vector3(num4, num1, num3), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
      {
        // ISSUE: explicit reference operation
        num7 = (float) ((RaycastHit) @raycastHit).get_point().y;
      }
      if (Physics.Raycast(new Vector3(num2, num1, num5), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
      {
        // ISSUE: explicit reference operation
        num8 = (float) ((RaycastHit) @raycastHit).get_point().y;
      }
      if (Physics.Raycast(new Vector3(num4, num1, num5), Vector3.op_UnaryNegation(Vector3.get_up()), ref raycastHit))
      {
        // ISSUE: explicit reference operation
        num9 = (float) ((RaycastHit) @raycastHit).get_point().y;
      }
      float num10 = x - num2;
      float num11 = y - num3;
      return Mathf.Lerp(Mathf.Lerp(num6, num7, num10), Mathf.Lerp(num8, num9, num10), num11);
    }

    private IntVector2 CalcClickedGrid(Vector2 screenPosition)
    {
      Ray ray = Camera.get_main().ScreenPointToRay(Vector2.op_Implicit(screenPosition));
      IntVector2 intVector2 = new IntVector2(-1, -1);
      RaycastHit raycastHit;
      if (Physics.Raycast(ray, ref raycastHit))
      {
        float num = float.MaxValue;
        for (int y = 0; y < this.mHeightMap.h; ++y)
        {
          for (int x = 0; x < this.mHeightMap.w; ++x)
          {
            // ISSUE: explicit reference operation
            Vector3 vector3 = Vector3.op_Subtraction(((RaycastHit) @raycastHit).get_point(), new Vector3((float) x + 0.5f, this.mHeightMap.get(x, y), (float) y + 0.5f));
            // ISSUE: explicit reference operation
            float magnitude = ((Vector3) @vector3).get_magnitude();
            if ((double) magnitude < (double) num)
            {
              intVector2.x = x;
              intVector2.y = y;
              num = magnitude;
            }
          }
        }
      }
      return intVector2;
    }

    private void OnClickBackground(Vector2 screenPosition)
    {
      if (this.mOnGridClick != null)
      {
        IntVector2 intVector2 = this.CalcClickedGrid(screenPosition);
        Grid current = this.mBattle.CurrentMap[intVector2.x, intVector2.y];
        if (intVector2.x != -1 && intVector2.y != -1 && current != null)
          this.mOnGridClick(current);
      }
      if (this.mOnUnitClick != null)
      {
        float num1 = float.MaxValue;
        TacticsUnitController controller = (TacticsUnitController) null;
        Vector3 vector3 = Vector3.op_Multiply(Vector3.get_up(), 0.5f);
        Camera main = Camera.get_main();
        float num2 = (float) (Screen.get_height() / 5);
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Implicit(main.WorldToScreenPoint(Vector3.op_Addition(((Component) this.mTacticsUnits[index]).get_transform().get_position(), vector3))), screenPosition);
          // ISSUE: explicit reference operation
          float magnitude = ((Vector2) @vector2).get_magnitude();
          if ((double) magnitude < (double) num1 && (double) magnitude <= (double) num2)
          {
            controller = this.mTacticsUnits[index];
            num1 = magnitude;
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null))
          this.mOnUnitClick(controller);
      }
      if (this.mOnScreenClick == null)
        return;
      this.mOnScreenClick(screenPosition);
    }

    private T InstantiateSafe<T>(UnityEngine.Object obj) where T : UnityEngine.Object
    {
      return (T) UnityEngine.Object.Instantiate(obj);
    }

    private void UpdateLoadProgress()
    {
      ProgressWindow.SetLoadProgress((this.mLoadProgress_UI + this.mLoadProgress_Scene + this.mLoadProgress_Animation) * 0.5f);
    }

    [DebuggerHidden]
    private IEnumerator LoadUIAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CLoadUIAsync\u003Ec__Iterator36()
      {
        \u003C\u003Ef__this = this
      };
    }

    private void InitTouchArea()
    {
      GameObject gameObject = new GameObject("TouchArea", new System.Type[6]
      {
        typeof (Canvas),
        typeof (GraphicRaycaster),
        typeof (CanvasStack),
        typeof (NullGraphic),
        typeof (TouchController),
        typeof (SRPG_CanvasScaler)
      });
      this.mTouchController = (TouchController) gameObject.GetComponent<TouchController>();
      this.mTouchController.OnClick = new TouchController.ClickEvent(this.OnClickBackground);
      this.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
      this.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
      ((Canvas) gameObject.GetComponent<Canvas>()).set_renderMode((RenderMode) 0);
      ((CanvasStack) gameObject.GetComponent<CanvasStack>()).Priority = 0;
      this.mGoWeatherAttach = new GameObject("WeatherAttach");
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mGoWeatherAttach))
        return;
      this.mGoWeatherAttach.get_transform().SetParent(gameObject.get_transform(), false);
    }

    private void DestroyUI(bool is_part = false)
    {
      BadStatusEffects.UnloadEffects();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillTargetWindow, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mSkillTargetWindow);
        this.mSkillTargetWindow = (SkillTargetWindow) null;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTouchController, (UnityEngine.Object) null))
      {
        this.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        this.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
      }
      if (!is_part && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBattleUI, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mBattleUI);
        this.mBattleUI = (FlowNode_BattleUI) null;
      }
      this.mBattleSceneRoot = (BattleSceneSettings) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefaultBattleScene, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject((Component) this.mDefaultBattleScene);
        this.mDefaultBattleScene = (BattleSceneSettings) null;
      }
      for (int index = 0; index < this.mBattleScenes.Count; ++index)
        GameUtility.DestroyGameObject((Component) this.mBattleScenes[index]);
      this.mBattleScenes.Clear();
      if (this.mUnitMarkers == null)
        return;
      for (int index = 0; index < this.mUnitMarkers.Length; ++index)
        GameUtility.DestroyGameObjects(this.mUnitMarkers[index]);
      this.mUnitMarkers = (List<GameObject>[]) null;
    }

    private void HideUnitMarkers(bool deactivate = false)
    {
      for (int index = this.mUnitMarkers.Length - 1; index >= 0; --index)
        this.HideUnitMarkers((SceneBattle.UnitMarkerTypes) index);
      if (!deactivate)
        return;
      this.DeactivateUnusedUnitMarkers();
    }

    private void HideUnitMarkers(SceneBattle.UnitMarkerTypes markerType)
    {
      int index1 = (int) markerType;
      for (int index2 = this.mUnitMarkers[index1].Count - 1; index2 >= 0; --index2)
      {
        this.mUnitMarkers[index1][index2].get_transform().SetParent((Transform) null, false);
        GameUtility.SetLayer(this.mUnitMarkers[index1][index2], GameUtility.LayerHidden, true);
      }
    }

    private void HideUnitMarkers(Unit unit)
    {
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      Transform transform = ((Component) unitController).get_transform();
      for (int index1 = this.mUnitMarkers.Length - 1; index1 >= 0; --index1)
      {
        for (int index2 = this.mUnitMarkers[index1].Count - 1; index2 >= 0; --index2)
        {
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitMarkers[index1][index2].get_transform().get_parent(), (UnityEngine.Object) transform))
          {
            GameUtility.SetLayer(this.mUnitMarkers[index1][index2], GameUtility.LayerHidden, true);
            this.mUnitMarkers[index1][index2].get_transform().SetParent((Transform) null, false);
          }
        }
      }
    }

    private void ShowUnitMarker(List<Unit> units, SceneBattle.UnitMarkerTypes markerType)
    {
      for (int index = units.Count - 1; index >= 0; --index)
        this.ShowUnitMarker(units[index], markerType);
    }

    private void ShowUnitMarker(Unit unit, SceneBattle.UnitMarkerTypes markerType)
    {
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        return;
      Transform transform = ((Component) unitController).get_transform();
      List<GameObject> mUnitMarker = this.mUnitMarkers[(int) markerType];
      for (int index = mUnitMarker.Count - 1; index >= 0; --index)
      {
        if (mUnitMarker[index].get_layer() != GameUtility.LayerHidden && UnityEngine.Object.op_Equality((UnityEngine.Object) mUnitMarker[index].get_transform().get_parent(), (UnityEngine.Object) transform))
          return;
      }
      for (int index = mUnitMarker.Count - 1; index >= 0; --index)
      {
        if (mUnitMarker[index].get_layer() == GameUtility.LayerHidden)
        {
          mUnitMarker[index].get_transform().SetParent(transform, false);
          GameUtility.SetLayer(mUnitMarker[index], GameUtility.LayerUI, true);
          mUnitMarker[index].SetActive(true);
          return;
        }
      }
      GameObject go = UnityEngine.Object.Instantiate((UnityEngine.Object) this.mUnitMarkerTemplates[(int) markerType], Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
      go.get_transform().SetParent(((Component) unitController).get_transform(), false);
      go.get_transform().set_localPosition(Vector3.op_Multiply(Vector3.get_up(), 2f));
      GameUtility.SetLayer(go, GameUtility.LayerUI, true);
      mUnitMarker.Add(go);
    }

    private void DeactivateUnusedUnitMarkers()
    {
      for (int index1 = 0; index1 < this.mUnitMarkers.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.mUnitMarkers[index1].Count; ++index2)
        {
          if (this.mUnitMarkers[index1][index2].get_layer() == GameUtility.LayerHidden)
            this.mUnitMarkers[index1][index2].SetActive(false);
        }
      }
    }

    private void ShowEnemyUnitMarkers()
    {
      this.HideUnitMarkers(SceneBattle.UnitMarkerTypes.Enemy);
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        Unit unit = this.mTacticsUnits[index].Unit;
        if (unit.Side == EUnitSide.Enemy && !unit.IsDead)
          this.ShowUnitMarker(unit, SceneBattle.UnitMarkerTypes.Enemy);
      }
    }

    public GameObject PopupDamageNumber(Vector3 position, int number)
    {
      return this.PopupNumber(position, number, Color.get_white(), this.mDamageTemplate);
    }

    public GameObject PopupHpHealNumber(Vector3 position, int number)
    {
      return this.PopupNumber(position, number, Color.get_white(), this.mHpHealTemplate);
    }

    public GameObject PopupMpHealNumber(Vector3 position, int number)
    {
      return this.PopupNumber(position, number, Color.get_white(), this.mMpHealTemplate);
    }

    public GameObject PopupNumber(Vector3 position, int number, Color color, DamagePopup popup)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) popup, (UnityEngine.Object) null))
      {
        Debug.LogError((object) "mDamageTemplate == null");
        return (GameObject) null;
      }
      DamagePopup damagePopup = (DamagePopup) UnityEngine.Object.Instantiate<DamagePopup>((M0) popup);
      damagePopup.Value = number;
      damagePopup.DigitColor = color;
      SceneBattle.Popup2D(((Component) damagePopup).get_gameObject(), position, 1, 0.0f);
      return ((Component) damagePopup).get_gameObject();
    }

    public GameObject PopupDamageDsgNumber(Vector3 position, int number, eDamageDispType ddt)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mDamageDsgTemplate))
      {
        Debug.LogError((object) "mDamageDsgTemplate == null");
        return (GameObject) null;
      }
      DamageDsgPopup damageDsgPopup = (DamageDsgPopup) UnityEngine.Object.Instantiate<DamageDsgPopup>((M0) this.mDamageDsgTemplate);
      damageDsgPopup.Setup(number, Color.get_white(), ddt);
      SceneBattle.Popup2D(((Component) damageDsgPopup).get_gameObject(), position, 1, 0.0f);
      return ((Component) damageDsgPopup).get_gameObject();
    }

    public bool HasMissPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMissPopup, (UnityEngine.Object) null);
      }
    }

    public bool HasPerfectAvoidPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPerfectAvoidPopup, (UnityEngine.Object) null);
      }
    }

    public bool HasGuardPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGuardPopup, (UnityEngine.Object) null);
      }
    }

    public bool HasAbsorbPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAbsorbPopup, (UnityEngine.Object) null);
      }
    }

    public bool HasCriticalPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCriticalPopup, (UnityEngine.Object) null);
      }
    }

    public bool HasBackstabPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBackstabPopup, (UnityEngine.Object) null);
      }
    }

    public bool HasWeakPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mWeakPopup, (UnityEngine.Object) null);
      }
    }

    public bool HasResistPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mResistPopup, (UnityEngine.Object) null);
      }
    }

    public bool HasGutsPopup
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGutsPopup, (UnityEngine.Object) null);
      }
    }

    public void PopupMiss(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mMissPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mMissPopup), position, 0, yOffset);
    }

    public void PopupPefectAvoid(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPerfectAvoidPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mPerfectAvoidPopup), position, 0, yOffset);
    }

    public void PopupGuard(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGuardPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mGuardPopup), position, 0, yOffset);
    }

    public void PopupAbsorb(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAbsorbPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mAbsorbPopup), position, 0, yOffset);
    }

    public void PopupCritical(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCriticalPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mCriticalPopup), position, 0, yOffset);
    }

    public void PopupBackstab(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBackstabPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mBackstabPopup), position, 0, yOffset);
    }

    public void PopupWeak(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mWeakPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mWeakPopup), position, 0, yOffset);
    }

    public void PopupResist(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mResistPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mResistPopup), position, 0, yOffset);
    }

    public void PopupGuts(Vector3 position, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGutsPopup, (UnityEngine.Object) null))
        return;
      SceneBattle.Popup2D((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mGutsPopup), position, 0, yOffset);
    }

    public void PopupGoodJob(Vector3 position, GameObject prefab, Sprite sprite = null)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) prefab, (UnityEngine.Object) null))
        return;
      GameObject go = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) prefab);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return;
      Image image = !UnityEngine.Object.op_Equality((UnityEngine.Object) sprite, (UnityEngine.Object) null) ? (Image) go.GetComponentInChildren<Image>() : (Image) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) image, (UnityEngine.Object) null))
        image.set_sprite(sprite);
      SceneBattle.Popup2D(go, position, 0, 0.0f);
    }

    private Transform CreateParamChangeEffect(ParamTypes type, bool isDebuff)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mParamChangeEffectTemplate, (UnityEngine.Object) null))
        return (Transform) null;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mParamChangeEffectTemplate);
      BuffEffectText component = (BuffEffectText) gameObject.GetComponent<BuffEffectText>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.SetText(type, isDebuff);
      return gameObject.get_transform();
    }

    private Transform CreateEffectChangeCT(int val)
    {
      if (val == 0)
        return (Transform) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mParamChangeEffectTemplate, (UnityEngine.Object) null))
        return (Transform) null;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mParamChangeEffectTemplate);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
        return (Transform) null;
      BuffEffectText component = (BuffEffectText) gameObject.GetComponent<BuffEffectText>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) component.Text, (UnityEngine.Object) null))
      {
        string key = "quest.POP_INC_CT";
        component.Text.BottomColor = GameSettings.Instance.Buff_TextBottomColor;
        component.Text.TopColor = GameSettings.Instance.Buff_TextTopColor;
        if (val < 0)
        {
          val *= -1;
          key = "quest.POP_DEC_CT";
          component.Text.BottomColor = GameSettings.Instance.Debuff_TextBottomColor;
          component.Text.TopColor = GameSettings.Instance.Debuff_TextTopColor;
        }
        component.Text.text = string.Format(LocalizedText.Get(key), (object) val);
      }
      return gameObject.get_transform();
    }

    public void PopupParamChange(Vector3 position, BuffBit buff, BuffBit debuff, int ct_change_val = 0)
    {
      if (buff == null || debuff == null)
        return;
      GameObject go = new GameObject("Params", new System.Type[2]
      {
        typeof (RectTransform),
        typeof (DelayStart)
      });
      Transform transform = go.get_transform();
      for (int index = 0; index < SkillParam.MAX_PARAMTYPES; ++index)
      {
        ParamTypes type = (ParamTypes) index;
        if (buff.CheckBit(type))
        {
          Transform paramChangeEffect = this.CreateParamChangeEffect(type, false);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) paramChangeEffect, (UnityEngine.Object) null))
            paramChangeEffect.SetParent(transform, false);
        }
        if (debuff.CheckBit(type))
        {
          Transform paramChangeEffect = this.CreateParamChangeEffect(type, true);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) paramChangeEffect, (UnityEngine.Object) null))
            paramChangeEffect.SetParent(transform, false);
        }
      }
      int val = ct_change_val / 10;
      if (val != 0)
      {
        Transform effectChangeCt = this.CreateEffectChangeCT(val);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) effectChangeCt, (UnityEngine.Object) null))
          effectChangeCt.SetParent(transform, false);
      }
      SceneBattle.Popup2D(go, position, 0, 0.0f);
    }

    private Transform CreateConditionChangeEffect(EUnitCondition condition)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mConditionChangeEffectTemplate, (UnityEngine.Object) null))
        return (Transform) null;
      GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.mConditionChangeEffectTemplate);
      CondEffectText component = (CondEffectText) gameObject.GetComponent<CondEffectText>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        component.SetText(condition);
      return gameObject.get_transform();
    }

    public void PopupConditionChange(Vector3 position, EUnitCondition fails)
    {
      GameObject go = new GameObject("Conditions", new System.Type[2]
      {
        typeof (RectTransform),
        typeof (DelayStart)
      });
      Transform transform = go.get_transform();
      for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
      {
        EUnitCondition condition = (EUnitCondition) (1L << index);
        if ((condition & fails) != (EUnitCondition) 0)
        {
          Transform conditionChangeEffect = this.CreateConditionChangeEffect(condition);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) conditionChangeEffect, (UnityEngine.Object) null))
            conditionChangeEffect.SetParent(transform, false);
        }
      }
      SceneBattle.Popup2D(go, position, 0, 0.0f);
    }

    public void ShowSkillNamePlate(Unit unit, SkillData skill, string skill_name = "", float speed = 1f)
    {
      if (skill == null)
        this.mSkillNamePlate.SetSkillName(skill_name, unit.Side, EElement.None, AttackDetailTypes.None, AttackTypes.None);
      else
        this.mSkillNamePlate.SetSkillName(skill.Name, unit.Side, skill.ElementType, skill.AttackDetailType, skill.AttackType);
      this.mSkillNamePlate.Open(speed);
      ((Component) this.mSkillNamePlate).get_transform().SetParent(((Component) this.OverlayCanvas).get_transform(), false);
    }

    public void ShowSkillNamePlate(string skill_name, EUnitSide side = EUnitSide.Player, float speed = 1f)
    {
      this.mSkillNamePlate.SetSkillName(skill_name, side, EElement.None, AttackDetailTypes.None, AttackTypes.None);
      this.mSkillNamePlate.Open(speed);
      ((Component) this.mSkillNamePlate).get_transform().SetParent(((Component) this.OverlayCanvas).get_transform(), false);
    }

    public bool IsClosedSkillNamePlate()
    {
      return this.mSkillNamePlate.IsClosed();
    }

    public static void Popup2D(GameObject go, Vector3 position, int priority = 0, float yOffset = 0)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        return;
      SceneBattle.Instance.InternalPopup2D(go, position, priority, yOffset);
    }

    private void InternalPopup2D(GameObject go, Vector3 position, int priority, float yOffset = 0)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
        return;
      this.mPopups.Add(new KeyValuePair<SceneBattle.PupupData, GameObject>(new SceneBattle.PupupData(position, priority, yOffset), go));
      go.get_transform().SetParent(((Component) this.OverlayCanvas).get_transform(), false);
      this.mPopupPositionCache.Clear();
      RectTransform transform = go.get_transform() as RectTransform;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
        return;
      transform.set_anchoredPosition(new Vector2((float) -Screen.get_width(), (float) -Screen.get_height()));
    }

    private void LayoutPopups(Camera cam)
    {
      Canvas overlayCanvas = this.OverlayCanvas;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) overlayCanvas, (UnityEngine.Object) null))
        return;
      RectTransform transform1 = ((Component) overlayCanvas).get_transform() as RectTransform;
      int[] array = new int[this.mPopups.Count];
      for (int index = 0; index < this.mPopups.Count; ++index)
      {
        GameObject gameObject = this.mPopups[index].Value;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          array[index] = gameObject.get_transform().GetSiblingIndex();
      }
      Array.Sort<int>(array);
      int[] numArray = new int[this.mPopups.Count];
      GameObject[] gameObjectArray = new GameObject[this.mPopups.Count];
      for (int index = 0; index < this.mPopups.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPopups[index].Value, (UnityEngine.Object) null))
        {
          numArray[index] = this.mPopups[index].Key.priority;
          gameObjectArray[index] = this.mPopups[index].Value;
        }
      }
      for (int index1 = 0; index1 < numArray.Length - 1; ++index1)
      {
        for (int index2 = index1 + 1; index2 < numArray.Length; ++index2)
        {
          if (numArray[index1] > numArray[index2])
          {
            int num = numArray[index1];
            numArray[index1] = numArray[index2];
            numArray[index2] = num;
            GameObject gameObject = gameObjectArray[index1];
            gameObjectArray[index1] = gameObjectArray[index2];
            gameObjectArray[index2] = gameObject;
          }
        }
      }
      for (int index = 0; index < gameObjectArray.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObjectArray[index], (UnityEngine.Object) null))
          gameObjectArray[index].get_transform().SetSiblingIndex(array[index]);
      }
      for (int index1 = 0; index1 < this.mPopups.Count; ++index1)
      {
        GameObject gameObject = this.mPopups[index1].Value;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          List<KeyValuePair<SceneBattle.PupupData, GameObject>> mPopups = this.mPopups;
          int index2 = index1;
          int num = index2 - 1;
          mPopups.RemoveAt(index2);
          break;
        }
        Vector3 screenPoint = cam.WorldToScreenPoint(this.mPopups[index1].Key.position);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local1 = @screenPoint;
        double num1 = screenPoint.x / (double) Screen.get_width();
        Rect rect1 = transform1.get_rect();
        // ISSUE: explicit reference operation
        double width = (double) ((Rect) @rect1).get_width();
        double num2 = num1 * width;
        // ISSUE: explicit reference operation
        (^local1).x = (__Null) num2;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local2 = @screenPoint;
        double num3 = screenPoint.y / (double) Screen.get_height();
        Rect rect2 = transform1.get_rect();
        // ISSUE: explicit reference operation
        double height = (double) ((Rect) @rect2).get_height();
        double num4 = num3 * height;
        // ISSUE: explicit reference operation
        (^local2).y = (__Null) num4;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local3 = @screenPoint;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local3).y = (__Null) ((^local3).y + (double) this.mPopups[index1].Key.yOffset);
        if (this.isScreenMirroring)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local4 = @screenPoint;
          Rect rect3 = transform1.get_rect();
          // ISSUE: explicit reference operation
          double num5 = (double) ((Rect) @rect3).get_width() - screenPoint.x;
          // ISSUE: explicit reference operation
          (^local4).x = (__Null) num5;
        }
        RectTransform transform2 = gameObject.get_transform() as RectTransform;
        if (!this.mPopupPositionCache.ContainsKey(transform2) || Vector3.op_Inequality(this.mPopupPositionCache[transform2], screenPoint))
        {
          RectTransform rectTransform = transform2;
          Vector2 zero = Vector2.get_zero();
          transform2.set_anchorMax(zero);
          Vector2 vector2 = zero;
          rectTransform.set_anchorMin(vector2);
          transform2.set_anchoredPosition(Vector2.op_Implicit(screenPoint));
          this.mPopupPositionCache[transform2] = screenPoint;
        }
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBlockedGridMarker, (UnityEngine.Object) null))
        return;
      if (this.mDisplayBlockedGridMarker)
      {
        TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          Vector3 vector3 = unitController.CenterPosition;
          if (this.mGridDisplayBlockedGridMarker != null)
            vector3 = Vector3.op_Addition(this.CalcGridCenter(this.mGridDisplayBlockedGridMarker), Vector3.op_Multiply(Vector3.get_up(), unitController.Height * 0.5f));
          Vector3 screenPoint = cam.WorldToScreenPoint(vector3);
          Vector2 vector2;
          RectTransformUtility.ScreenPointToLocalPointInRectangle(this.mTouchController.GetRectTransform(), Vector2.op_Implicit(screenPoint), (Camera) null, ref vector2);
          RectTransform component = (RectTransform) this.mBlockedGridMarker.GetComponent<RectTransform>();
          if (!this.mPopupPositionCache.ContainsKey(component) || Vector3.op_Inequality(this.mPopupPositionCache[component], screenPoint))
          {
            this.mPopupPositionCache[component] = screenPoint;
            component.set_anchoredPosition(vector2);
          }
        }
      }
      else
        GameUtility.ToggleGraphic(this.mBlockedGridMarker, true);
      ((Animator) this.mBlockedGridMarker.GetComponent<Animator>()).SetBool("visible", this.mDisplayBlockedGridMarker);
    }

    private void LayoutGauges(Camera cam)
    {
      Canvas overlayCanvas = this.OverlayCanvas;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) overlayCanvas, (UnityEngine.Object) null))
        return;
      List<TacticsUnitController> instances = TacticsUnitController.Instances;
      RectTransform transform = ((Component) overlayCanvas).get_transform() as RectTransform;
      this.mLayoutGauges.Clear();
      List<Vector3> vector3List = new List<Vector3>();
      for (int index = 0; index < instances.Count; ++index)
      {
        TacticsUnitController tacticsUnitController = instances[index];
        RectTransform hpGaugeTransform = tacticsUnitController.HPGaugeTransform;
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) hpGaugeTransform, (UnityEngine.Object) null) && ((Component) hpGaugeTransform).get_gameObject().get_activeInHierarchy())
        {
          Vector3 vector3 = Vector3.op_Addition(((Component) tacticsUnitController).get_transform().get_position(), Vector3.get_up());
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Transform) hpGaugeTransform).get_parent(), (UnityEngine.Object) transform))
            ((Transform) hpGaugeTransform).SetParent((Transform) transform, false);
          Vector3 screenPoint = cam.WorldToScreenPoint(vector3);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local1 = @screenPoint;
          double num1 = screenPoint.x / (double) Screen.get_width();
          Rect rect1 = transform.get_rect();
          // ISSUE: explicit reference operation
          double width = (double) ((Rect) @rect1).get_width();
          double num2 = num1 * width;
          // ISSUE: explicit reference operation
          (^local1).x = (__Null) num2;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local2 = @screenPoint;
          double num3 = screenPoint.y / (double) Screen.get_height();
          Rect rect2 = transform.get_rect();
          // ISSUE: explicit reference operation
          double height = (double) ((Rect) @rect2).get_height();
          double num4 = num3 * height;
          // ISSUE: explicit reference operation
          (^local2).y = (__Null) num4;
          vector3List.Add(screenPoint);
          this.mLayoutGauges.Add(hpGaugeTransform);
        }
      }
      Vector4[] points = new Vector4[this.mLayoutGauges.Count];
      for (int index = 0; index < this.mLayoutGauges.Count; ++index)
      {
        points[index].x = vector3List[index].x;
        points[index].y = vector3List[index].y;
        points[index].z = (__Null) (double) this.mGaugeCollisionRadius;
        points[index].w = (__Null) 0.0;
      }
      GameUtility.ApplyAvoidance(ref points, 3, this.mGaugeYAspectRatio);
      for (int index = 0; index < this.mLayoutGauges.Count; ++index)
      {
        RectTransform mLayoutGauge = this.mLayoutGauges[index];
        Vector2 vector2_1;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2_1).\u002Ector((float) points[index].x, (float) points[index].y);
        if (!this.mGaugePositionCache.ContainsKey(mLayoutGauge) || Vector2.op_Inequality(this.mGaugePositionCache[mLayoutGauge], vector2_1))
        {
          this.mGaugePositionCache[mLayoutGauge] = vector2_1;
          RectTransform rectTransform = mLayoutGauge;
          Vector2 zero = Vector2.get_zero();
          mLayoutGauge.set_anchorMax(zero);
          Vector2 vector2_2 = zero;
          rectTransform.set_anchorMin(vector2_2);
          mLayoutGauge.set_anchoredPosition(vector2_1);
        }
      }
    }

    public bool UISignal
    {
      get
      {
        return this.mUISignal;
      }
      set
      {
        this.mUISignal = value;
      }
    }

    private void SetPrioritizedUnit(TacticsUnitController controller)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null))
        controller.AlwaysUpdate = true;
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTacticsUnits[index], (UnityEngine.Object) controller))
          this.mTacticsUnits[index].AlwaysUpdate = false;
      }
    }

    private void SetPrioritizedUnits(List<TacticsUnitController> controllers)
    {
      if (controllers == null)
      {
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          this.mTacticsUnits[index].AlwaysUpdate = false;
      }
      else
      {
        for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        {
          if (controllers.Contains(this.mTacticsUnits[index]))
            this.mTacticsUnits[index].AlwaysUpdate = true;
          else
            this.mTacticsUnits[index].AlwaysUpdate = false;
        }
      }
    }

    private void ToggleRenkeiAura(bool visible)
    {
      if (visible)
      {
        List<Unit> helperUnits = this.mBattle.HelperUnits;
        if (helperUnits.Count > 0)
        {
          TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            unitController.SetRenkeiAura(this.mRenkeiAuraEffect);
          for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          {
            if (helperUnits.Contains(this.mTacticsUnits[index].Unit))
              this.mTacticsUnits[index].SetRenkeiAura(this.mRenkeiAuraEffect);
          }
          return;
        }
      }
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
        this.mTacticsUnits[index].StopRenkeiAura();
    }

    private void OnDrag()
    {
      if (!this.mBattle.IsMapCommand || this.mBattle.IsUnitAuto(this.mBattle.CurrentUnit))
        return;
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.IsStartSkill())
        return;
      VirtualStick2 virtualStick = this.mBattleUI.VirtualStick;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) virtualStick, (UnityEngine.Object) null) || !this.IsControlBattleUI(SceneBattle.eMaskBattleUI.VS_SWIPE))
        return;
      virtualStick.Visible = true;
      virtualStick.SetPosition(this.mTouchController.DragStart);
      virtualStick.Delta = this.mTouchController.DragDelta;
    }

    private void OnDragEnd()
    {
      if (!this.mBattle.IsMapCommand || this.mBattle.IsUnitAuto(this.mBattle.CurrentUnit))
        return;
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.IsStartSkill())
        return;
      VirtualStick2 virtualStick = this.mBattleUI.VirtualStick;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) virtualStick, (UnityEngine.Object) null))
        return;
      virtualStick.Visible = false;
    }

    private void ToggleJumpSpots(bool visible)
    {
      for (int index = 0; index < this.mJumpSpotEffects.Count; ++index)
        this.mJumpSpotEffects[index].Value.SetActive(visible);
    }

    private void RefreshJumpSpots()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mJumpSpotEffectTemplate, (UnityEngine.Object) null))
        return;
      for (int index1 = 0; index1 < this.mBattle.Units.Count; ++index1)
      {
        Unit unit = this.mBattle.Units[index1];
        if (!unit.IsDead && unit.CastSkill != null && (unit.CastSkill.CastType == ECastTypes.Jump || unit.CastSkill.TeleportType != eTeleportType.None))
        {
          bool flag = false;
          for (int index2 = 0; index2 < this.mJumpSpotEffects.Count; ++index2)
          {
            if (this.mJumpSpotEffects[index2].Key == unit)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            int x = unit.x;
            int y = unit.y;
            if (unit.CastSkill.TeleportType != eTeleportType.None)
            {
              x = unit.GridTarget.x;
              y = unit.GridTarget.y;
            }
            GameObject gameObject = UnityEngine.Object.Instantiate((UnityEngine.Object) this.mJumpSpotEffectTemplate, this.CalcGridCenter(x, y), Quaternion.get_identity()) as GameObject;
            this.mJumpSpotEffects.Add(new KeyValuePair<Unit, GameObject>(unit, gameObject));
          }
        }
      }
      for (int index = this.mJumpSpotEffects.Count - 1; index >= 0; --index)
      {
        Unit key = this.mJumpSpotEffects[index].Key;
        if (key.IsDead || key.CastSkill == null || key.CastSkill.CastType != ECastTypes.Jump && key.CastSkill.TeleportType == eTeleportType.None)
        {
          GameUtility.StopEmitters(this.mJumpSpotEffects[index].Value);
          this.mJumpSpotEffects[index].Value.AddComponent<OneShotParticle>();
          this.mJumpSpotEffects.RemoveAt(index);
        }
      }
    }

    public GameObject GetJumpSpotEffect(Unit unit)
    {
      for (int index = 0; index < this.mJumpSpotEffects.Count; ++index)
      {
        if (this.mJumpSpotEffects[index].Key == unit)
          return this.mJumpSpotEffects[index].Value;
      }
      return (GameObject) null;
    }

    private void LoadShieldEffects()
    {
      List<Unit> allUnits = this.mBattle.AllUnits;
      List<SkillParam> skills = (List<SkillParam>) null;
      for (int index1 = 0; index1 < allUnits.Count; ++index1)
      {
        Unit unit = allUnits[index1];
        for (int index2 = 0; index2 < unit.Shields.Count; ++index2)
        {
          SkillParam skillParam = unit.Shields[index2].skill_param;
          if (!string.IsNullOrEmpty(skillParam.defend_effect) && !this.mShieldEffects.ContainsKey(skillParam))
          {
            if (skills == null)
              skills = new List<SkillParam>();
            else if (skills.Contains(skillParam))
              continue;
            skills.Add(skillParam);
          }
        }
      }
      if (skills == null)
        return;
      this.mLoadingShieldEffects = true;
      this.StartCoroutine(this.LoadShieldEffectsAsync(skills));
    }

    [DebuggerHidden]
    private IEnumerator LoadShieldEffectsAsync(List<SkillParam> skills)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CLoadShieldEffectsAsync\u003Ec__Iterator37()
      {
        skills = skills,
        \u003C\u0024\u003Eskills = skills,
        \u003C\u003Ef__this = this
      };
    }

    private GameObject SpawnShieldEffect(TacticsUnitController unit, SkillParam skill, int value, int valueMax, int turn, int turnMax)
    {
      if (!this.mShieldEffects.ContainsKey(skill))
        return (GameObject) null;
      GameObject mShieldEffect = this.mShieldEffects[skill];
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) mShieldEffect, (UnityEngine.Object) null))
        return (GameObject) null;
      GameObject gameObject = UnityEngine.Object.Instantiate((UnityEngine.Object) mShieldEffect, ((Component) unit).get_transform().get_position(), Quaternion.get_identity()) as GameObject;
      Animator component = (Animator) gameObject.GetComponent<Animator>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
      {
        component.SetInteger("shield_val", value);
        if (valueMax > 0)
        {
          component.SetFloat("shield_val_norm", (float) value / (float) valueMax);
          float num = (float) value * 100f / (float) valueMax;
          component.SetInteger("shield_val_per", (double) num >= 50.0 ? Mathf.FloorToInt(num) : Mathf.CeilToInt(num));
        }
        component.SetInteger("shield_turn", turn);
        if (turnMax > 0)
        {
          component.SetFloat("shield_turn_norm", (float) turn / (float) turnMax);
          float num = (float) turn * 100f / (float) turnMax;
          component.SetInteger("shield_turn_per", (double) num >= 50.0 ? Mathf.FloorToInt(num) : Mathf.CeilToInt(num));
        }
      }
      return gameObject;
    }

    public class MoveInput
    {
      public SceneBattle SceneOwner;
      public SceneBattle.MoveInput.TargetSelectEvent OnAttackTargetSelect;

      public virtual void MoveUnit(Vector3 target_screen_pos)
      {
      }

      public virtual void Start()
      {
      }

      public virtual void End()
      {
      }

      public virtual bool IsBusy
      {
        get
        {
          return false;
        }
      }

      public virtual void Reset()
      {
      }

      public virtual void Update()
      {
      }

      protected void SelectAttackTarget(Unit unit)
      {
        if (this.OnAttackTargetSelect == null)
          return;
        this.OnAttackTargetSelect(unit);
      }

      public delegate void TargetSelectEvent(Unit unit);
    }

    public class SimpleEvent
    {
      private static Dictionary<int, SceneBattle.SimpleEvent.Interface> m_Group = new Dictionary<int, SceneBattle.SimpleEvent.Interface>();

      public static void Clear()
      {
        SceneBattle.SimpleEvent.m_Group.Clear();
      }

      public static bool HasGroup(int group)
      {
        return SceneBattle.SimpleEvent.m_Group.ContainsKey(group);
      }

      public static void Send(int group, string key, object obj)
      {
        SceneBattle.SimpleEvent.Interface nterface = (SceneBattle.SimpleEvent.Interface) null;
        if (!SceneBattle.SimpleEvent.m_Group.TryGetValue(group, out nterface))
          return;
        nterface.OnEvent(key, obj);
      }

      public static void Add(int group, SceneBattle.SimpleEvent.Interface inst)
      {
        if (SceneBattle.SimpleEvent.HasGroup(group))
          return;
        SceneBattle.SimpleEvent.m_Group.Add(group, inst);
      }

      public static void Remove(int group)
      {
        SceneBattle.SimpleEvent.m_Group.Remove(group);
      }

      public interface Interface
      {
        void OnEvent(string key, object obj);
      }
    }

    private enum eArenaSubmitMode
    {
      BUSY,
      SUCCESS,
      FAILED,
    }

    public enum StateTransitionTypes
    {
      Forward,
      Back,
    }

    private class State_ReqBtlComReq : State<SceneBattle>
    {
      private GUIStyle mItemStyle = new GUIStyle();
      private int mSelectChangeUnitIndex = -1;
      private bool[] mEnableUnitTypeToggle = new bool[4];
      private string mUnitFilter = string.Empty;
      private int mSelectChangeInventoryIndex = -1;
      private int mSelectChangeArtifactUnitIndex = -1;
      private int mSelectChangeArtifactJobIndex = -1;
      private int mSelectChangeArtifactIndex = -1;
      private string mFindQuestName = string.Empty;
      private List<string> mQuestHistoryIds = new List<string>();
      private const int HistoryMax = 10;
      private GUIEventListener mGUIEvent;
      private bool mQuestStarting;
      private int mSelectedPartyIndex;
      private Vector2 mQuestListScrollPos;
      private int mSelectedQuestIndex;
      private bool mShowParty;
      private Vector2 mPartyScrollPos;
      private UnitData[] mPartyUnits;
      private int mUnitDispType;
      private bool mShowInventory;
      private Vector2 mInventoryScrollPos;
      private bool mShowArtifact;
      private int mArtifactTab;
      private bool mShowHistory;
      private List<QuestParam> QuestParamList;
      private int mQuestFilterFlg;
      private bool mInitialized;

      public override void Begin(SceneBattle self)
      {
      }

      public override void End(SceneBattle self)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGUIEvent, (UnityEngine.Object) null))
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mGUIEvent);
        this.mGUIEvent = (GUIEventListener) null;
      }

      public override void Update(SceneBattle self)
      {
        if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGUIEvent, (UnityEngine.Object) null) || Network.Mode != Network.EConnectMode.Offline)
          return;
        this.mGUIEvent = (GUIEventListener) ((Component) self).get_gameObject().AddComponent<GUIEventListener>();
        this.mGUIEvent.Listeners = new GUIEventListener.GUIEvent(this.OnGUI);
        GameUtility.FadeOut(0.0f);
      }

      private void SetQuestFilter(SceneBattle.State_ReqBtlComReq.EQuestFilter type, bool flag)
      {
        if (flag)
          this.mQuestFilterFlg |= (int) type;
        else
          this.mQuestFilterFlg &= (int) ~type;
      }

      private bool GetQuestFilter(SceneBattle.State_ReqBtlComReq.EQuestFilter type)
      {
        return (SceneBattle.State_ReqBtlComReq.EQuestFilter) 0 < ((SceneBattle.State_ReqBtlComReq.EQuestFilter) this.mQuestFilterFlg & type);
      }

      private void AddHistory(string newIdName)
      {
        if (this.mQuestHistoryIds.Contains(newIdName))
          this.mQuestHistoryIds.Remove(newIdName);
        this.mQuestHistoryIds.Insert(0, newIdName);
        if (this.mQuestHistoryIds.Count <= 10)
          return;
        this.mQuestHistoryIds.RemoveRange(10, this.mQuestHistoryIds.Count - 10);
      }

      private void CreateSaveHistory()
      {
        if (this.mQuestHistoryIds.Count <= 10)
          return;
        this.mQuestHistoryIds.RemoveRange(10, this.mQuestHistoryIds.Count - 10);
      }

      private void Init()
      {
        string key1 = "Debug_Quest_History_Show";
        if (EditorPlayerPrefs.HasKey(key1))
          this.mShowHistory = EditorPlayerPrefs.GetInt(key1) != 0;
        string key2 = "Debug_Quest_History";
        string empty = string.Empty;
        if (EditorPlayerPrefs.HasKey(key2))
          empty = EditorPlayerPrefs.GetString("Debug_Quest_History");
        GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
        this.QuestParamList = new List<QuestParam>((IEnumerable<QuestParam>) instanceDirect.Quests);
        List<QuestParam> questParamList = new List<QuestParam>();
        for (int index1 = 0; index1 < this.QuestParamList.Count; ++index1)
        {
          List<TowerFloorParam> towerFloors = instanceDirect.FindTowerFloors(this.QuestParamList[index1].iname);
          if (towerFloors != null && towerFloors.Count > 0)
          {
            for (int index2 = 0; index2 < towerFloors.Count; ++index2)
              this.QuestParamList.Add(towerFloors[index2].Clone(this.QuestParamList[index1], false));
            questParamList.Add(this.QuestParamList[index1]);
          }
        }
        for (int index = 0; index < questParamList.Count; ++index)
          this.QuestParamList.Remove(questParamList[index]);
        if (!string.IsNullOrEmpty(empty))
        {
          bool flag = false;
          string[] strArray = empty.Split(',');
          for (int index1 = 0; index1 < strArray.Length; ++index1)
          {
            for (int index2 = 0; index2 < this.QuestParamList.Count; ++index2)
            {
              if (this.QuestParamList[index2].iname == strArray[index1])
                this.mQuestHistoryIds.Add(strArray[index1]);
              if (!flag && 0 < this.mQuestHistoryIds.Count && this.mQuestHistoryIds[0] == this.QuestParamList[index2].iname)
              {
                flag = true;
                this.mSelectedQuestIndex = index2;
              }
            }
          }
        }
        this.mQuestFilterFlg = 15;
        this.mPartyUnits = new UnitData[MonoSingleton<GameManager>.Instance.Player.Partys[this.mSelectedPartyIndex].MAX_UNIT];
        this.mInitialized = true;
      }

      private void OnGUI(GameObject go)
      {
      }

      private enum EQuestFilter
      {
        Normal = 1,
        Hard = 2,
        Event = 4,
        Opening = 8,
      }
    }

    private class State_Start : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        QuestParam mCurrentQuest = self.mCurrentQuest;
        if (string.IsNullOrEmpty(mCurrentQuest.event_start) || !self.mIsFirstPlay || self.Battle.CheckEnableSuspendStart())
          this.GotoNextState();
        else
          self.StartCoroutine(this.LoadAndExecuteEvent(mCurrentQuest.event_start));
      }

      [DebuggerHidden]
      private IEnumerator LoadAndExecuteEvent(string eventName)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Start.\u003CLoadAndExecuteEvent\u003Ec__Iterator38()
        {
          eventName = eventName,
          \u003C\u0024\u003EeventName = eventName,
          \u003C\u003Ef__this = this
        };
      }

      private void GotoNextState()
      {
        if (this.self.mCurrentQuest.IsScenario)
        {
          this.self.GotoState<SceneBattle.State_ExitQuest>();
        }
        else
        {
          GameUtility.SetNeverSleep();
          if (this.self.mIsFirstPlay && !string.IsNullOrEmpty(this.self.mCurrentQuest.event_start))
            ProgressWindow.OpenQuestLoadScreen((string) null, (string) null);
          else
            ProgressWindow.OpenQuestLoadScreen(this.self.mCurrentQuest);
          this.self.GotoState<SceneBattle.State_InitUI>();
        }
      }
    }

    private class State_InitUI : State<SceneBattle>
    {
      private float mWait;
      private float mElapsed;
      private bool mLoaded;

      public override void Begin(SceneBattle self)
      {
        GameSettings instance = GameSettings.Instance;
        if (!GameUtility.IsDebugBuild || MonoSingleton<GameManager>.Instance.Player.OkyakusamaCode != instance.QuestLoad_OkyakusamaCode)
          this.mWait = 0.0f;
        else
          this.mWait = instance.QuestLoad_WaitSecond;
      }

      public override void Update(SceneBattle self)
      {
        this.mElapsed += Time.get_deltaTime();
        if ((double) this.mWait >= (double) this.mElapsed || this.mLoaded)
          return;
        self.StartCoroutine(self.LoadUIAsync());
        this.mLoaded = true;
      }
    }

    private class State_WaitForLog : State<SceneBattle>
    {
      private bool CheckShieldEffect()
      {
        TacticsUnitController unitController = this.self.FindUnitController(this.self.Battle.CurrentUnit);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
        {
          unitController.UpdateShields(true);
          TacticsUnitController tuc;
          TacticsUnitController.ShieldState shield;
          if (this.self.FindChangedShield(out tuc, out shield))
          {
            this.self.mIgnoreShieldEffect.Clear();
            this.self.GotoState<SceneBattle.State_SpawnShieldEffects>();
            return true;
          }
        }
        return false;
      }

      public override void Update(SceneBattle self)
      {
        BattleLogServer logs = self.Battle.Logs;
        while (logs.Num > 0)
        {
          BattleLog peek = logs.Peek;
          if (peek is LogError)
          {
            self.GotoState<SceneBattle.State_Error>();
            break;
          }
          if (peek is LogConnect)
          {
            self.GotoState<SceneBattle.State_Connect>();
            break;
          }
          if (peek is LogUnitStart)
          {
            ++self.mUnitStartCount;
            ++self.mUnitStartCountTotal;
            self.mBattleUI.OnUnitStart();
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
            {
              if (self.Battle.CurrentUnit.OwnerPlayerIndex > 0 && self.Battle.CurrentUnit.OwnerPlayerIndex == self.Battle.MyPlayerIndex)
              {
                self.mBattleUI_MultiPlay.OnMyUnitStart();
              }
              else
              {
                Unit unit = self.Battle.AllUnits.Find((Predicate<Unit>) (u => u.OwnerPlayerIndex == self.Battle.MyPlayerIndex));
                EUnitSide eunitSide = unit == null ? self.Battle.CurrentUnit.Side : unit.Side;
                if (self.Battle.CurrentUnit.Side == eunitSide)
                  self.mBattleUI_MultiPlay.OnOtherUnitStart();
                else if (self.Battle.IsMultiVersus)
                  self.mBattleUI_MultiPlay.OnOtherUnitStart();
                else
                  self.mBattleUI_MultiPlay.OnEnemyUnitStart();
              }
            }
            self.GotoState<SceneBattle.State_PreUnitStart>();
            break;
          }
          if (peek is LogUnitEntry)
          {
            self.GotoState<SceneBattle.State_SpawnUnit>();
            break;
          }
          if (peek is LogUnitWithdraw)
          {
            self.GotoState<SceneBattle.State_UnitWithdraw>();
            break;
          }
          if (peek is LogWeather)
          {
            self.GotoState<SceneBattle.State_Weather>();
            break;
          }
          if (peek is LogMapCommand)
          {
            if (this.CheckShieldEffect())
              break;
            self.RemoveLog();
            self.GotoMapCommand();
            break;
          }
          if (peek is LogMapWait)
          {
            self.Battle.SetManualPlayFlag();
            self.GotoState<SceneBattle.State_MapWait>();
            break;
          }
          if (peek is LogMapMove)
          {
            self.Battle.SetManualPlayFlag();
            self.GotoState<SceneBattle.State_AnimateMove>();
            break;
          }
          if (peek is LogMapEvent)
          {
            if (self.ConditionalGotoGimmickState())
              break;
          }
          else
          {
            if (peek is LogSkill)
            {
              if (((LogSkill) peek).skill.AttackType == AttackTypes.MagAttack)
                self.Battle.SetManualPlayFlag();
              self.GotoPrepareSkill();
              break;
            }
            if (peek is LogCastSkillStart)
            {
              self.mBattleUI.OnCastSkillStart();
              self.GotoState<SceneBattle.State_CastSkillStart>();
              break;
            }
            if (peek is LogCastSkillEnd)
            {
              self.GotoState<SceneBattle.State_CastSkillEnd>();
              break;
            }
            if (peek is LogMapTrick)
            {
              self.GotoState<SceneBattle.State_MapTrick>();
              break;
            }
            if (peek is LogUnitEnd)
            {
              if (this.CheckShieldEffect())
                break;
              if (!UnityEngine.Object.op_Equality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
              {
                if (self.Battle.CurrentUnit.OwnerPlayerIndex > 0 && self.Battle.CurrentUnit.OwnerPlayerIndex == self.Battle.MyPlayerIndex)
                {
                  self.mBattleUI_MultiPlay.OnMyUnitEnd();
                }
                else
                {
                  Unit unit = self.Battle.AllUnits.Find((Predicate<Unit>) (u => u.OwnerPlayerIndex == self.Battle.MyPlayerIndex));
                  EUnitSide eunitSide = unit == null ? self.Battle.CurrentUnit.Side : unit.Side;
                  if (self.Battle.CurrentUnit.Side == eunitSide)
                    self.mBattleUI_MultiPlay.OnOtherUnitEnd();
                  else
                    self.mBattleUI_MultiPlay.OnEnemyUnitEnd();
                }
              }
              if (self.Battle.IsMultiVersus)
              {
                self.Battle.RemainVersusTurnCount = (uint) self.UnitStartCountTotal;
                self.ArenaActionCountSet(self.Battle.RemainVersusTurnCount);
              }
              LogUnitEnd logUnitEnd = peek as LogUnitEnd;
              if (logUnitEnd.self != null)
              {
                TacticsUnitController unitController = self.FindUnitController(logUnitEnd.self);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                  unitController.ClearBadStatusLocks();
              }
              self.GotoState<SceneBattle.State_UnitEnd>();
              break;
            }
            if (peek is LogOrdealChangeNext)
            {
              self.GotoState<SceneBattle.State_OrdealChangeNext>();
              break;
            }
            if (peek is LogMapEnd)
            {
              if (self.IsPlayingArenaQuest)
                self.ArenaActionCountSet(self.Battle.ArenaActionCount);
              self.GotoMapEnd();
              break;
            }
            if (peek is LogDead)
            {
              if (!self.Battle.IsBattle)
              {
                self.GotoState<SceneBattle.State_EventMapDead>();
                break;
              }
              self.GotoState<SceneBattle.State_BattleDead>();
              break;
            }
            if (peek is LogRevive)
            {
              if (!self.Battle.IsBattle)
              {
                self.GotoState<SceneBattle.State_MapRevive>();
                break;
              }
              self.RemoveLog();
            }
            else if (peek is LogDamage)
            {
              DebugUtility.LogWarning("warning damage.");
              self.RemoveLog();
            }
            else
            {
              if (peek is LogAutoHeal)
              {
                self.GotoState<SceneBattle.State_AutoHeal>();
                break;
              }
              if (peek is LogFailCondition)
              {
                LogFailCondition logFailCondition = peek as LogFailCondition;
                if (logFailCondition.self != null)
                {
                  TacticsUnitController unitController = self.FindUnitController(logFailCondition.self);
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                  {
                    unitController.UnlockUpdateBadStatus(logFailCondition.condition);
                    unitController.UpdateBadStatus();
                  }
                }
                self.RemoveLog();
              }
              else if (peek is LogCureCondition)
              {
                self.RemoveLog();
              }
              else
              {
                if (peek is LogCast)
                {
                  self.Battle.SetManualPlayFlag();
                  self.GotoState<SceneBattle.State_PrepareCast>();
                  break;
                }
                if (peek is LogFall)
                {
                  self.GotoState<SceneBattle.State_JumpFall>();
                  break;
                }
                if (peek is LogSync)
                {
                  self.RemoveLog();
                  self.GotoState<SceneBattle.State_MultiPlaySync>();
                  break;
                }
                DebugUtility.LogError("不明なログを検出しました " + peek.GetType().ToString());
                self.RemoveLog();
                break;
              }
            }
          }
        }
      }
    }

    private class State_Error : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
      }

      public override void Update(SceneBattle self)
      {
      }

      public override void End(SceneBattle self)
      {
        self.RemoveLog();
      }
    }

    private class State_Connect : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        if (Network.IsBusy)
          return;
        if (Network.IsError)
        {
          DebugUtility.LogError("connection error.");
        }
        else
        {
          if (self.Battle.Logs.Num == 0)
            DebugUtility.LogError("failed. logs not found.");
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }
    }

    private class State_QuestStartV2 : State<SceneBattle>
    {
      private bool is_call_map_start;

      public override void Update(SceneBattle self)
      {
        if (this.is_call_map_start)
          return;
        self.GotoMapStart();
        this.is_call_map_start = true;
      }
    }

    private class State_MapCommandAI : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          return;
        unitController.HideCursor(false);
      }

      public override void Update(SceneBattle self)
      {
        if (self.IsCameraMoving)
          return;
        if (self.mBattle.ConditionalUnitEnd(false))
        {
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          bool forceAI = self.Battle.IsMultiPlay && !self.Battle.IsUnitAuto(self.Battle.CurrentUnit);
          self.Battle.UpdateMapAI(forceAI);
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }
    }

    private class State_MapCommandMultiPlay : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.ShowAllHPGauges();
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          return;
        unitController.HideCursor(false);
      }

      public override void End(SceneBattle self)
      {
      }

      private void SyncCameraPosition(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          return;
        GameSettings instance = GameSettings.Instance;
        Transform transform = ((Component) Camera.get_main()).get_transform();
        transform.set_position(Vector3.op_Addition(((Component) unitController).get_transform().get_position(), ((Component) instance.Quest.MoveCamera).get_transform().get_position()));
        transform.set_rotation(((Component) instance.Quest.MoveCamera).get_transform().get_rotation());
        self.SetCameraTarget((Component) ((Component) unitController).get_transform());
        self.mUpdateCameraPosition = true;
      }

      public override void Update(SceneBattle self)
      {
        this.SyncCameraPosition(self);
        if (!self.mBattle.ConditionalUnitEnd(true))
          return;
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    public class CloseBattleUIWindow
    {
      public List<Win_Btn_Decide_Title_Flx> mMsgBox = new List<Win_Btn_Decide_Title_Flx>();
      public List<Win_Btn_DecideCancel_FL_C> mYesNoDlg = new List<Win_Btn_DecideCancel_FL_C>();

      public void Add(GameObject go)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) go, (UnityEngine.Object) null))
          return;
        Win_Btn_Decide_Title_Flx component1 = (Win_Btn_Decide_Title_Flx) go.GetComponent<Win_Btn_Decide_Title_Flx>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
          this.mMsgBox.Add(component1);
        Win_Btn_DecideCancel_FL_C component2 = (Win_Btn_DecideCancel_FL_C) go.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
          return;
        this.mYesNoDlg.Add(component2);
      }

      public void CloseAll()
      {
        using (List<Win_Btn_Decide_Title_Flx>.Enumerator enumerator = this.mMsgBox.GetEnumerator())
        {
          while (enumerator.MoveNext())
            enumerator.Current.BeginClose();
        }
        this.mMsgBox.Clear();
        using (List<Win_Btn_DecideCancel_FL_C>.Enumerator enumerator = this.mYesNoDlg.GetEnumerator())
        {
          while (enumerator.MoveNext())
            enumerator.Current.BeginClose();
        }
        this.mYesNoDlg.Clear();
      }
    }

    private class ChargeTarget
    {
      public Unit mUnit;
      public uint mAttr;

      public ChargeTarget(Unit unit, uint attr)
      {
        this.mUnit = unit;
        this.mAttr = attr;
      }

      public void AddAttr(uint attr)
      {
        this.mAttr |= attr;
      }
    }

    private class State_MapWait : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.HideUnitCursor(self.mBattle.CurrentUnit);
      }

      public override void Update(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            self.mEventSequence = self.mEventScript.OnStandbyGrid(unitController, self.mIsFirstPlay);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
            {
              self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitForLog>>();
              return;
            }
          }
        }
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self)
      {
        self.RemoveLog();
      }
    }

    private class State_MapMoveSelect_Stick : State<SceneBattle>
    {
      private Vector2 mBasePos = Vector2.get_zero();
      private Vector2 mTargetPos = Vector2.get_zero();
      private const float STOP_RADIUS = 0.1f;
      private SceneBattle mScene;
      private TacticsUnitController mController;
      private Vector3 mTapStart;
      private float mPressTime;
      private bool mTapped;
      private GridMap<int> mAccessMap;
      private GridMap<int> mWalkableField;
      private Vector3 mStart;
      private bool mAllowTap;
      private int mDestX;
      private int mDestY;
      private Grid mStartGrid;
      private bool mTargetSet;
      private bool mMoveStarted;
      private bool mClickedOK;
      private float mGridSnapTime;
      private bool mJumping;
      private bool mMoving;
      private bool mHasDesiredRotation;
      private bool mGridSnapping;
      private Quaternion mDesiredRotation;

      private void Reset()
      {
        ((Component) this.mController).get_transform().set_position(this.mStart);
        this.mController.CancelAction();
        this.mScene.SendInputMoveEnd(this.mScene.Battle.CurrentUnit, true);
        this.mMoveStarted = false;
      }

      public override void Begin(SceneBattle self)
      {
        this.mScene = self;
        self.mAllowCameraTranslation = false;
        Unit currentUnit = self.Battle.CurrentUnit;
        this.mController = self.FindUnitController(currentUnit);
        this.mController.AutoUpdateRotation = false;
        self.ShowUnitCursorOnCurrent();
        BattleMap currentMap = self.Battle.CurrentMap;
        this.mAccessMap = self.CreateCurrentAccessMap();
        this.mWalkableField = this.mAccessMap.clone();
        this.mController.WalkableField = this.mWalkableField;
        self.ShowWalkableGrids(this.mWalkableField, 0);
        Grid grid = currentMap[currentUnit.x, currentUnit.y];
        this.mStartGrid = grid;
        this.mStart = self.CalcGridCenter(grid);
        this.mDestX = Mathf.FloorToInt((float) this.mStart.x);
        this.mDestY = Mathf.FloorToInt((float) (this.mStart.z - ((Component) self.mTacticsSceneRoot).get_transform().get_position().z));
        this.mBasePos.x = (__Null) ((double) currentUnit.x + 0.5);
        this.mBasePos.y = (__Null) ((double) currentUnit.y + 0.5);
        this.mTargetPos = this.mBasePos;
        self.SetMoveCamera();
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.SendInputMoveStart(self.Battle.CurrentUnit);
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes transition)
      {
        switch (transition)
        {
          case SceneBattle.StateTransitionTypes.Forward:
            Grid current = this.mScene.mBattle.CurrentMap[this.mDestX, this.mDestY];
            EUnitDirection direction = this.mController.CalcUnitDirectionFromRotation();
            if (this.mScene.Battle.Move(this.mScene.Battle.CurrentUnit, current, direction, true, false))
            {
              this.mClickedOK = true;
              this.mController.HideCursor(false);
              ((Component) this.mController).get_transform().set_position(this.mScene.CalcGridCenter(current));
              this.mScene.SendInputGridXY(this.mScene.Battle.CurrentUnit, this.mDestX, this.mDestY, this.mScene.Battle.CurrentUnit.Direction, true);
              this.mScene.SendInputMoveEnd(this.mScene.Battle.CurrentUnit, false);
              this.mScene.mBattleUI.OnInputMoveEnd();
              if (current == this.mStartGrid)
                break;
              this.self.mAutoActivateGimmick = true;
              break;
            }
            this.self.mCloseBattleUIWindow.Add(UIUtility.NegativeSystemMessage(LocalizedText.Get("sys.TITLE"), LocalizedText.Get("err.TARGET_GRID_BLOCKED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1));
            break;
          case SceneBattle.StateTransitionTypes.Back:
            this.Reset();
            this.mClickedOK = true;
            this.mScene.GotoMapCommand();
            break;
        }
      }

      public override void End(SceneBattle self)
      {
        self.mAllowCameraTranslation = true;
        if (!this.mClickedOK)
          this.Reset();
        this.mController.AutoUpdateRotation = true;
        this.mController.StopRunning();
        this.mController.WalkableField = (GridMap<int>) null;
        this.mController.HideCursor(false);
        self.mTacticsSceneRoot.HideGridLayer(0);
      }

      private bool IsGridBlocked(Vector2 co)
      {
        return this.IsGridBlocked((float) co.x, (float) co.y);
      }

      private bool IsGridBlocked(float x, float y)
      {
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);
        if (this.mAccessMap.isValid(x1, y1))
          return this.mAccessMap.get(x1, y1) < 0;
        return true;
      }

      private bool CanMoveToAdj(Vector2 from, Vector2 to)
      {
        if (this.IsGridBlocked(to))
          return false;
        if (this.IsGridBlocked((float) from.x, (float) to.y))
          return !this.IsGridBlocked((float) to.x, (float) from.y);
        return true;
      }

      private bool CanMoveToAdjDirect(Vector2 from, Vector2 to)
      {
        if (!this.IsGridBlocked((float) from.x, (float) to.y))
          return !this.IsGridBlocked((float) to.x, (float) from.y);
        return false;
      }

      private bool GridEqualIn2D(Vector2 a, Vector2 b)
      {
        if ((int) a.x == (int) b.x)
          return (int) a.y == (int) b.y;
        return false;
      }

      private void AdjustTargetPos(ref Vector2 basePos, ref Vector2 targetPos, Vector2 inputDir, Vector2 unitPos)
      {
        if (this.CanMoveToAdj(basePos, targetPos))
          return;
        bool flag = false;
        Vector2 vector2 = Vector2.op_Subtraction(targetPos, basePos);
        // ISSUE: explicit reference operation
        if ((double) Vector3.Dot(Vector2.op_Implicit(((Vector2) @vector2).get_normalized()), Vector2.op_Implicit(Vector2.op_Subtraction(unitPos, basePos))) >= -0.100000001490116)
        {
          Vector2[] vector2Array = new Vector2[8]
          {
            new Vector2(-1f, 1f),
            new Vector2(0.0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0.0f),
            new Vector2(1f, -1f),
            new Vector2(0.0f, -1f),
            new Vector2(-1f, -1f),
            new Vector2(-1f, 0.0f)
          };
          float[] numArray = new float[vector2Array.Length];
          // ISSUE: explicit reference operation
          ((Vector2) @inputDir).Normalize();
          for (int index = 0; index < vector2Array.Length; ++index)
          {
            // ISSUE: explicit reference operation
            numArray[index] = Vector2.Dot(((Vector2) @vector2Array[index]).get_normalized(), inputDir);
          }
          for (int index1 = 0; index1 < vector2Array.Length; ++index1)
          {
            for (int index2 = index1 + 1; index2 < vector2Array.Length; ++index2)
            {
              if ((double) numArray[index1] < (double) numArray[index2])
              {
                GameUtility.swap<float>(ref numArray[index1], ref numArray[index2]);
                GameUtility.swap<Vector2>(ref vector2Array[index1], ref vector2Array[index2]);
              }
            }
          }
          Vector2 zero = Vector2.get_zero();
          for (int index = 1; index < vector2Array.Length && (double) numArray[index] >= 0.5; ++index)
          {
            zero.x = basePos.x + vector2Array[index].x;
            zero.y = basePos.y + vector2Array[index].y;
            if (this.CanMoveToAdj(this.mBasePos, zero))
            {
              targetPos = zero;
              flag = true;
              break;
            }
          }
        }
        if (flag && this.CanMoveToAdj(basePos, targetPos))
          return;
        targetPos = basePos;
      }

      private Vector2 Velocity
      {
        get
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) VirtualStick.Instance, (UnityEngine.Object) null))
            return VirtualStick.Instance.GetVelocity(((Component) Camera.get_main()).get_transform());
          return Vector2.get_zero();
        }
      }

      private void SyncCameraPosition()
      {
        Vector3 position = ((Component) this.mController).get_transform().get_position();
        this.mScene.SetCameraTarget((float) position.x, (float) position.z);
      }

      public override void Update(SceneBattle self)
      {
        if (this.mGridSnapping)
        {
          this.SyncCameraPosition();
          if (!this.mController.isIdle)
            return;
          this.mGridSnapping = false;
        }
        if (!this.mMoving && (double) this.mGridSnapTime <= 0.0 && self.Battle.EntryBattleMultiPlayTimeUp)
        {
          self.Battle.EntryBattleMultiPlayTimeUp = false;
          Unit currentUnit = self.Battle.CurrentUnit;
          EUnitDirection direction = this.mController.CalcUnitDirectionFromRotation();
          if (!self.Battle.MoveMultiPlayer(currentUnit, this.mDestX, this.mDestY, direction))
          {
            this.Reset();
          }
          else
          {
            self.SendInputGridXY(currentUnit, currentUnit.x, currentUnit.y, currentUnit.Direction, true);
            self.SendInputMove(currentUnit, this.mController);
            self.SendInputMoveEnd(currentUnit, false);
            DebugUtility.Log("MoveEnd x:" + (object) currentUnit.x + " y:" + (object) currentUnit.y + " action:" + (object) this.mController.IsPlayingFieldAction);
          }
          self.SendInputUnitTimeLimit(currentUnit);
          self.SendInputFlush(false);
          self.Battle.EntryBattleMultiPlayTimeUp = true;
          this.mClickedOK = true;
          self.CloseBattleUI();
          self.GotoMapCommand();
        }
        else if (this.mClickedOK)
        {
          self.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if (ObjectAnimator.Get((Component) Camera.get_main()).isMoving)
            return;
          if (this.mController.IsPlayingFieldAction)
          {
            this.SyncCameraPosition();
          }
          else
          {
            if (this.mJumping)
            {
              this.mJumping = false;
              bool battleMultiPlayTimeUp = this.mScene.Battle.EntryBattleMultiPlayTimeUp;
              self.Battle.EntryBattleMultiPlayTimeUp = false;
              self.SendInputMove(self.Battle.CurrentUnit, this.mController);
              self.Battle.EntryBattleMultiPlayTimeUp = battleMultiPlayTimeUp;
            }
            Transform transform1 = ((Component) this.mController).get_transform();
            Vector2 vector2;
            // ISSUE: explicit reference operation
            ((Vector2) @vector2).\u002Ector((float) transform1.get_position().x, (float) transform1.get_position().z);
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Vector2& local = @vector2;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            (^local).y = (^local).y - ((Component) self.mTacticsSceneRoot).get_transform().get_position().z;
            if (this.mTargetSet && this.GridEqualIn2D(vector2, this.mTargetPos))
            {
              this.mBasePos = this.mTargetPos;
              this.mDestX = Mathf.FloorToInt((float) this.mBasePos.x);
              this.mDestY = Mathf.FloorToInt((float) this.mBasePos.y);
              bool battleMultiPlayTimeUp = this.mScene.Battle.EntryBattleMultiPlayTimeUp;
              self.Battle.EntryBattleMultiPlayTimeUp = false;
              self.Battle.EntryBattleMultiPlayTimeUp = battleMultiPlayTimeUp;
            }
            Vector2 inputDir = this.Velocity;
            if (self.Battle.EntryBattleMultiPlayTimeUp)
              inputDir = Vector2.get_zero();
            // ISSUE: explicit reference operation
            if ((double) ((Vector2) @inputDir).get_sqrMagnitude() > 0.0)
            {
              if (!this.mMoveStarted)
                this.mMoveStarted = true;
              float num1 = Mathf.Floor((float) (((double) (Mathf.Atan2((float) inputDir.y, (float) inputDir.x) * 57.29578f) + 22.5) / 45.0)) * 45f;
              float num2 = Mathf.Cos(num1 * ((float) Math.PI / 180f));
              float num3 = Mathf.Sin(num1 * ((float) Math.PI / 180f));
              float num4 = (double) Mathf.Abs(num2) < 9.99999974737875E-05 ? 0.0f : Mathf.Sign(num2);
              float num5 = (double) Mathf.Abs(num3) < 9.99999974737875E-05 ? 0.0f : Mathf.Sign(num3);
              this.mTargetPos.x = (__Null) (this.mBasePos.x + (double) num4);
              this.mTargetPos.y = (__Null) (this.mBasePos.y + (double) num5);
              this.AdjustTargetPos(ref this.mBasePos, ref this.mTargetPos, inputDir, vector2);
              Vector3 vector3;
              // ISSUE: explicit reference operation
              ((Vector3) @vector3).\u002Ector((float) inputDir.x, 0.0f, (float) inputDir.y);
              this.mDesiredRotation = Quaternion.LookRotation(vector3);
              this.mHasDesiredRotation = true;
              this.mTargetSet = true;
              this.mGridSnapTime = GameSettings.Instance.Quest.GridSnapDelay;
            }
            else
            {
              this.mMoving = false;
              this.mTargetSet = false;
              this.mController.StopRunning();
              if ((double) this.mGridSnapTime >= 0.0 && !this.mHasDesiredRotation)
              {
                this.mGridSnapTime -= Time.get_deltaTime();
                if ((double) this.mGridSnapTime <= 0.0)
                {
                  Grid current = self.mBattle.CurrentMap[this.mDestX, this.mDestY];
                  this.mController.StepTo(self.CalcGridCenter(current));
                  this.mGridSnapping = true;
                  if (self.Battle.CurrentUnit != null)
                    self.SendInputGridXY(self.Battle.CurrentUnit, this.mDestX, this.mDestY, self.Battle.CurrentUnit.Direction, true);
                }
              }
            }
            Vector3 vector3_1 = Vector3.get_zero();
            if (this.mTargetSet)
            {
              if (this.CanMoveToAdjDirect(this.mBasePos, this.mTargetPos))
                vector3_1 = Vector2.op_Implicit(Vector2.op_Subtraction(this.mTargetPos, vector2));
              else if (this.IsGridBlocked((float) vector2.x, (float) this.mTargetPos.y))
                vector3_1.x = this.mTargetPos.x - vector2.x;
              else if (this.IsGridBlocked((float) this.mTargetPos.x, (float) vector2.y))
                vector3_1.y = this.mTargetPos.y - vector2.y;
              else
                vector3_1 = Vector2.op_Implicit(Vector2.op_Subtraction(this.mTargetPos, vector2));
              // ISSUE: explicit reference operation
              if ((double) ((Vector3) @vector3_1).get_magnitude() < 0.100000001490116)
                vector3_1 = Vector2.op_Implicit(Vector2.get_zero());
            }
            // ISSUE: explicit reference operation
            bool flag = (double) ((Vector3) @vector3_1).get_sqrMagnitude() > 0.0;
            if (this.mHasDesiredRotation || flag)
            {
              Quaternion quaternion = !flag ? this.mDesiredRotation : Quaternion.LookRotation(new Vector3((float) vector3_1.x, 0.0f, (float) vector3_1.y));
              GameSettings instance = GameSettings.Instance;
              // ISSUE: explicit reference operation
              float magnitude = ((Vector2) @inputDir).get_magnitude();
              float num1 = 0.0f;
              if (this.mMoving)
              {
                num1 = magnitude;
                ((Component) this.mController).get_transform().set_rotation(Quaternion.Slerp(((Component) this.mController).get_transform().get_rotation(), quaternion, Time.get_deltaTime() * 5f));
              }
              else
              {
                ((Component) this.mController).get_transform().set_rotation(Quaternion.Slerp(((Component) this.mController).get_transform().get_rotation(), quaternion, Time.get_deltaTime() * 10f));
                float num2 = Quaternion.Angle(quaternion, ((Component) this.mController).get_transform().get_rotation());
                if ((double) magnitude > 0.100000001490116)
                {
                  if ((double) num2 < 1.0)
                  {
                    this.mMoving = true;
                    num1 = magnitude;
                  }
                  else
                  {
                    float num3 = 15f;
                    float num4 = Mathf.Clamp01((float) (1.0 - (double) num2 / (double) num3));
                    num1 = magnitude * num4;
                  }
                }
                if ((double) num2 < 1.0)
                {
                  ((Component) this.mController).get_transform().set_rotation(quaternion);
                  this.mHasDesiredRotation = false;
                }
              }
              if ((double) num1 > 0.0 && flag)
              {
                this.mController.StartRunning();
                float num2 = Mathf.Lerp(instance.Quest.MapRunSpeedMin, instance.Quest.MapRunSpeedMax, num1);
                Vector3 vector3_2;
                // ISSUE: explicit reference operation
                ((Vector3) @vector3_2).\u002Ector((float) vector3_1.x, 0.0f, (float) vector3_1.y);
                // ISSUE: explicit reference operation
                Vector3 velocity = Vector3.op_Multiply(((Vector3) @vector3_2).get_normalized(), num2);
                if (this.mController.TriggerFieldAction(velocity, false))
                {
                  Vector2 fieldActionPoint = this.mController.FieldActionPoint;
                  this.mDestX = Mathf.FloorToInt((float) fieldActionPoint.x);
                  this.mDestY = Mathf.FloorToInt((float) fieldActionPoint.y);
                  this.mHasDesiredRotation = false;
                  this.mTargetSet = false;
                  this.mJumping = true;
                  bool battleMultiPlayTimeUp = this.mScene.Battle.EntryBattleMultiPlayTimeUp;
                  self.Battle.EntryBattleMultiPlayTimeUp = false;
                  self.SendInputGridXY(self.Battle.CurrentUnit, this.mDestX, this.mDestY, self.Battle.CurrentUnit.Direction, true);
                  self.Battle.EntryBattleMultiPlayTimeUp = battleMultiPlayTimeUp;
                }
                else
                {
                  Transform transform2 = transform1;
                  transform2.set_position(Vector3.op_Addition(transform2.get_position(), Vector3.op_Multiply(velocity, Time.get_deltaTime())));
                }
              }
              bool battleMultiPlayTimeUp1 = this.mScene.Battle.EntryBattleMultiPlayTimeUp;
              self.Battle.EntryBattleMultiPlayTimeUp = false;
              self.SendInputMove(self.Battle.CurrentUnit, this.mController);
              self.Battle.EntryBattleMultiPlayTimeUp = battleMultiPlayTimeUp1;
            }
            this.SyncCameraPosition();
          }
        }
      }
    }

    private class State_AnimateMove : State<SceneBattle>
    {
      private float mWaitCount = 1f;
      private Unit mMovingUnit;
      private TacticsUnitController mController;
      private bool mMoveUnit;
      private Vector3 mStartPosition;
      private Vector3 mEndPosition;

      public override void Begin(SceneBattle self)
      {
        LogMapMove peek = (LogMapMove) self.mBattle.Logs.Peek;
        this.mMovingUnit = peek.self;
        this.mController = self.FindUnitController(this.mMovingUnit);
        this.mController.WalkableField = self.CreateCurrentAccessMap();
        this.mMoveUnit = peek.auto || this.mMovingUnit.IsUnitFlag(EUnitFlag.ForceMoved);
        if (self.Battle.IsMultiPlay && !self.Battle.IsUnitAuto(this.mMovingUnit) && self.Battle.EntryBattleMultiPlayTimeUp)
          this.mMoveUnit = true;
        IntVector2 intVector2 = self.CalcCoord(this.mController.CenterPosition);
        EUnitDirection eunitDirection = this.mController.CalcUnitDirectionFromRotation();
        if (intVector2.x == this.mMovingUnit.x && intVector2.y == this.mMovingUnit.y && eunitDirection == this.mMovingUnit.Direction)
          this.mMoveUnit = false;
        self.mUpdateCameraPosition = false;
        BattleCameraFukan gameObject = GameObjectID.FindGameObject<BattleCameraFukan>(self.mBattleUI.FukanCameraID);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) && gameObject.isDisp)
          gameObject.SetDisp(false);
        if (this.mMoveUnit)
        {
          self.ShowWalkableGrids(self.CreateCurrentAccessMap(), 0);
        }
        else
        {
          while (self.mBattle.Logs.Num > 0 && self.mBattle.Logs.Peek is LogMapMove && ((LogMapMove) self.mBattle.Logs.Peek).self == this.mMovingUnit)
            self.RemoveLog();
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mMoveUnit)
        {
          float num = GameSettings.Instance.AiUnit_MoveWait * Time.get_deltaTime();
          float mWaitCount = this.mWaitCount;
          this.mWaitCount -= num;
          if (0.0 < (double) this.mWaitCount)
            return;
          if (0.0 < (double) mWaitCount)
            this.Move();
        }
        if (!this.mController.isIdle)
        {
          self.OnGimmickUpdate();
        }
        else
        {
          self.ResetCameraTarget();
          self.mUpdateCameraPosition = true;
          BattleLog peek = self.mBattle.Logs.Peek;
          if (peek == null || !(peek is LogMapMove))
          {
            BattleCameraFukan gameObject = GameObjectID.FindGameObject<BattleCameraFukan>(self.mBattleUI.FukanCameraID);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) && !gameObject.isDisp)
              gameObject.SetDisp(true);
          }
          if (self.mBattle.Logs.Num > 0)
            self.GotoState<SceneBattle.State_WaitForLog>();
          else
            self.GotoMapCommand();
        }
      }

      private void Move()
      {
        LogMapMove peek1 = (LogMapMove) this.self.mBattle.Logs.Peek;
        int length = 0;
        for (int index = 0; index < this.self.mBattle.Logs.Num && this.self.mBattle.Logs[index] is LogMapMove; ++index)
        {
          LogMapMove log = (LogMapMove) this.self.mBattle.Logs[index];
          if (log != null && log.self == peek1.self)
            ++length;
          else
            break;
        }
        BattleMap currentMap = this.self.mBattle.CurrentMap;
        Vector3[] route = new Vector3[length];
        for (int index = 0; index < length; ++index)
        {
          LogMapMove peek2 = (LogMapMove) this.self.mBattle.Logs.Peek;
          route[index] = this.self.CalcGridCenter(currentMap[peek2.ex, peek2.ey]);
          this.self.RemoveLog();
        }
        this.mStartPosition = route[0];
        this.mEndPosition = route[route.Length - 1];
        ObjectAnimator.Get((Component) ((Component) Camera.get_main()).get_transform()).AnimateTo(Vector3.op_Addition(((Component) Camera.get_main()).get_transform().get_position(), Vector3.op_Subtraction(this.mEndPosition, this.mStartPosition)), ((Component) Camera.get_main()).get_transform().get_rotation(), this.mController.StartMove(route, -1f), ObjectAnimator.CurveType.EaseInOut);
      }
    }

    private class State_EventMapDead : State<SceneBattle>
    {
      private TacticsUnitController mController;

      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          LogDead peek = self.mBattle.Logs.Peek as LogDead;
          if (peek != null)
          {
            List<TacticsUnitController> tacticsUnitControllerList = new List<TacticsUnitController>();
            using (List<LogDead.Param>.Enumerator enumerator = peek.list_normal.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                LogDead.Param current = enumerator.Current;
                TacticsUnitController unitController = self.FindUnitController(current.self);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                  tacticsUnitControllerList.Add(unitController);
              }
            }
            using (List<LogDead.Param>.Enumerator enumerator = peek.list_sentence.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                LogDead.Param current = enumerator.Current;
                TacticsUnitController unitController = self.FindUnitController(current.self);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                  tacticsUnitControllerList.Add(unitController);
              }
            }
            using (List<TacticsUnitController>.Enumerator enumerator = tacticsUnitControllerList.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                TacticsUnitController current = enumerator.Current;
                self.mEventSequence = self.mEventScript.OnUnitDead(current, self.mIsFirstPlay);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
                {
                  this.mController = current;
                  self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
                  self.InterpCameraTarget((Component) this.mController);
                  return;
                }
              }
            }
          }
        }
        self.GotoState<SceneBattle.State_MapDead>();
      }

      public override void Update(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) && self.IsCameraMoving)
          return;
        self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_EventMapDead>>();
      }
    }

    private class State_MapDead : State<SceneBattle>
    {
      private List<SceneBattle.State_MapDead.DirectionBase> mList = new List<SceneBattle.State_MapDead.DirectionBase>();

      public List<SceneBattle.State_MapDead.DirectionBase> list
      {
        get
        {
          return this.mList;
        }
      }

      public override void Begin(SceneBattle self)
      {
        LogDead peek = (LogDead) self.mBattle.Logs.Peek;
        List<Vector3> vector3List = new List<Vector3>();
        for (int index = 0; index < peek.list_normal.Count; ++index)
        {
          LogDead.Param obj = peek.list_normal[index];
          TacticsUnitController unitController = self.FindUnitController(obj.self);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            SceneBattle.State_MapDead.Dead.Normal normal = new SceneBattle.State_MapDead.Dead.Normal(self, this);
            normal.Initialize(10, unitController, obj.type);
            this.mList.Add((SceneBattle.State_MapDead.DirectionBase) normal);
            vector3List.Add(unitController.CenterPosition);
          }
        }
        if (vector3List.Count > 0)
        {
          SceneBattle.State_MapDead.Camera.Normal normal = new SceneBattle.State_MapDead.Camera.Normal(self, this);
          normal.Initialize(0, vector3List.ToArray());
          this.mList.Add((SceneBattle.State_MapDead.DirectionBase) normal);
        }
        for (int index = 0; index < peek.list_sentence.Count; ++index)
        {
          LogDead.Param obj = peek.list_sentence[index];
          TacticsUnitController unitController = self.FindUnitController(obj.self);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            SceneBattle.State_MapDead.Dead.CameraForcus cameraForcus = new SceneBattle.State_MapDead.Dead.CameraForcus(self, this);
            cameraForcus.Initialize(100 + index, unitController, obj.type);
            this.mList.Add((SceneBattle.State_MapDead.DirectionBase) cameraForcus);
          }
        }
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        for (int index = 0; index < this.mList.Count; ++index)
        {
          SceneBattle.State_MapDead.DirectionBase m = this.mList[index];
          if (!m.Update())
          {
            m.Release();
            this.mList.RemoveAt(index);
            --index;
          }
        }
        if (this.mList.Count != 0)
          return;
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.OnGimmickUpdate();
        self.RefreshJumpSpots();
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }

      public override void End(SceneBattle self)
      {
        for (int index = 0; index < this.mList.Count; ++index)
          this.mList[index].Release();
        this.mList.Clear();
      }

      public class DirectionBase
      {
        private SceneBattle mSceneBattle;
        private SceneBattle.State_MapDead mState;
        private int mPriority;
        private bool mDeleteFlag;
        private SceneBattle.State_MapDead.DirectionBase.Proc mProc;
        private int mProcCount;

        public DirectionBase(SceneBattle self, SceneBattle.State_MapDead state)
        {
          this.mSceneBattle = self;
          this.mState = state;
          this.SetProc(SceneBattle.State_MapDead.DirectionBase.Proc.PREPARE);
        }

        public SceneBattle scene
        {
          get
          {
            return this.mSceneBattle;
          }
        }

        public SceneBattle.State_MapDead state
        {
          get
          {
            return this.mState;
          }
        }

        public bool Update()
        {
          if (this.GetPriority() >= 0)
          {
            List<SceneBattle.State_MapDead.DirectionBase> list = this.mState.list;
            for (int index = 0; index < list.Count; ++index)
            {
              if (list[index].GetPriority() < this.GetPriority())
                return true;
            }
          }
          switch (this.mProc)
          {
            case SceneBattle.State_MapDead.DirectionBase.Proc.PREPARE:
              this.OnPrepare();
              this.SetProc(SceneBattle.State_MapDead.DirectionBase.Proc.BEGIN);
              break;
            case SceneBattle.State_MapDead.DirectionBase.Proc.BEGIN:
              if (!this.OnLoading())
              {
                this.OnBegin();
                this.SetProc(SceneBattle.State_MapDead.DirectionBase.Proc.MAIN);
                break;
              }
              break;
            case SceneBattle.State_MapDead.DirectionBase.Proc.MAIN:
              if (!this.OnMain())
              {
                this.Delete();
                break;
              }
              break;
          }
          if (this.mDeleteFlag)
          {
            this.SetProc(SceneBattle.State_MapDead.DirectionBase.Proc.END);
            this.OnEnd();
          }
          return !this.mDeleteFlag;
        }

        public void Delete()
        {
          this.mDeleteFlag = true;
        }

        public void SetPriority(int pri)
        {
          this.mPriority = pri;
        }

        public int GetPriority()
        {
          return this.mPriority;
        }

        protected void SetProc(SceneBattle.State_MapDead.DirectionBase.Proc proc)
        {
          this.mProc = proc;
          this.mProcCount = 0;
        }

        protected SceneBattle.State_MapDead.DirectionBase.Proc GetProc()
        {
          return this.mProc;
        }

        protected int GetProcCount()
        {
          return this.mProcCount;
        }

        protected void SetProcCount(int value)
        {
          this.mProcCount = value;
        }

        protected void IncProcCount()
        {
          ++this.mProcCount;
        }

        protected void DecProcCount()
        {
          --this.mProcCount;
        }

        public virtual void Initialize(int priority)
        {
          this.mPriority = priority;
        }

        public virtual void Release()
        {
        }

        protected virtual void OnPrepare()
        {
        }

        protected virtual bool OnLoading()
        {
          return false;
        }

        protected virtual void OnBegin()
        {
        }

        protected virtual bool OnMain()
        {
          return false;
        }

        protected virtual void OnEnd()
        {
        }

        protected enum Proc
        {
          PREPARE,
          BEGIN,
          MAIN,
          END,
        }
      }

      public class Camera : SceneBattle.State_MapDead.DirectionBase
      {
        protected Vector3 m_Center;
        protected float m_Distance;

        public Camera(SceneBattle self, SceneBattle.State_MapDead state)
          : base(self, state)
        {
        }

        public void Initialize(int priority, Vector3[] targets)
        {
          this.Initialize(priority);
          this.scene.GetCameraTargetView(out this.m_Center, out this.m_Distance, targets);
        }

        protected override void OnBegin()
        {
          this.scene.InterpCameraDistance(this.m_Distance);
          this.scene.InterpCameraTarget(this.m_Center);
        }

        protected override bool OnMain()
        {
          Vector3 vector3 = Vector3.op_Subtraction(this.scene.mCameraTarget, this.m_Center);
          vector3.y = (__Null) 0.0;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          return (double) ((Vector3) @vector3).get_magnitude() * (double) ((Vector3) @vector3).get_magnitude() >= 0.25;
        }

        public class Normal : SceneBattle.State_MapDead.Camera
        {
          public Normal(SceneBattle self, SceneBattle.State_MapDead state)
            : base(self, state)
          {
          }
        }
      }

      public class Dead : SceneBattle.State_MapDead.DirectionBase
      {
        protected TacticsUnitController mController;
        protected Unit mUnit;
        protected DeadTypes mDeadType;

        public Dead(SceneBattle self, SceneBattle.State_MapDead state)
          : base(self, state)
        {
        }

        public TacticsUnitController controller
        {
          get
          {
            return this.mController;
          }
        }

        public Unit unit
        {
          get
          {
            return this.mUnit;
          }
        }

        public DeadTypes deadType
        {
          get
          {
            return this.mDeadType;
          }
        }

        public void Initialize(int priority, TacticsUnitController controller, DeadTypes deadType)
        {
          this.Initialize(priority);
          this.mController = controller;
          this.mUnit = controller.Unit;
          this.mDeadType = deadType;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
            return;
          this.mController.ReflectDispModel();
          this.mController.LoadDeathAnimation(TacticsUnitController.DeathAnimationTypes.Normal);
          if (this.mUnit == null || this.mUnit.Drop == null || this.mUnit.Side != EUnitSide.Enemy && !this.mUnit.IsBreakObj || !this.mUnit.Drop.IsEnableDrop())
            return;
          SceneBattle.State_MapDead.TreasureDrop.Normal normal = new SceneBattle.State_MapDead.TreasureDrop.Normal(this.scene, this.state);
          normal.Initialize(this.GetPriority() + 1, this.mUnit, this.mUnit.Drop, ((Component) controller).get_transform().get_position());
          this.state.list.Add((SceneBattle.State_MapDead.DirectionBase) normal);
        }

        protected override bool OnLoading()
        {
          return this.mController.IsLoading;
        }

        protected override void OnBegin()
        {
          this.mController.PlayDead(TacticsUnitController.DeathAnimationTypes.Normal);
          this.scene.OnUnitDeath(this.mController.Unit);
          this.scene.mTacticsUnits.Remove(this.mController);
          this.mController.ShowHPGauge(false);
          this.mController.ShowVersusCursor(false);
        }

        protected override bool OnMain()
        {
          return !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null);
        }

        public class Normal : SceneBattle.State_MapDead.Dead
        {
          public Normal(SceneBattle self, SceneBattle.State_MapDead state)
            : base(self, state)
          {
          }
        }

        public class CameraForcus : SceneBattle.State_MapDead.Dead
        {
          public CameraForcus(SceneBattle self, SceneBattle.State_MapDead state)
            : base(self, state)
          {
          }

          protected override void OnPrepare()
          {
            this.scene.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
            this.scene.InterpCameraTarget((Component) this.mController);
          }

          protected override bool OnLoading()
          {
            if (base.OnLoading())
              return true;
            switch (this.GetProcCount())
            {
              case 0:
                Vector3 vector3 = Vector3.op_Subtraction(this.scene.mCameraTarget, ((Component) this.mController).get_transform().get_position());
                vector3.y = (__Null) 0.0;
                // ISSUE: explicit reference operation
                // ISSUE: explicit reference operation
                if ((double) ((Vector3) @vector3).get_magnitude() * (double) ((Vector3) @vector3).get_magnitude() < 0.25)
                {
                  this.mController.DeathSentenceCountDown(true, 1f);
                  this.IncProcCount();
                  break;
                }
                break;
              case 1:
                if (this.mController.IsDeathSentenceCountDownPlaying())
                  return true;
                break;
            }
            return false;
          }
        }
      }

      public class TreasureDrop : SceneBattle.State_MapDead.DirectionBase
      {
        protected Unit m_Owner;
        protected Unit.UnitDrop m_Drop;
        protected TreasureBox m_TreasureBox;

        public TreasureDrop(SceneBattle self, SceneBattle.State_MapDead state)
          : base(self, state)
        {
        }

        public void Initialize(int priority, Unit owner, Unit.UnitDrop drop, Vector3 pos)
        {
          this.Initialize(priority);
          this.m_Owner = owner;
          if (this.m_Owner != null)
            this.m_Owner.BeginDropDirection();
          this.m_Drop = drop;
          this.m_TreasureBox = (TreasureBox) UnityEngine.Object.Instantiate<TreasureBox>((M0) this.scene.mTreasureBoxTemplate);
          this.m_TreasureBox.owner = owner;
          ((Component) this.m_TreasureBox).get_transform().set_parent(((Component) this.scene).get_transform());
          ((Component) this.m_TreasureBox).get_transform().set_position(pos);
          ((Component) this.m_TreasureBox).get_gameObject().SetActive(false);
        }

        protected override void OnBegin()
        {
          Unit.DropItem DropItem = (Unit.DropItem) null;
          for (int index = this.m_Drop.items.Count - 1; index >= 0; --index)
          {
            if (!Unit.DropItem.IsNullOrEmpty(this.m_Drop.items[index]))
            {
              DropItem = this.m_Drop.items[index];
              break;
            }
          }
          SceneBattle.Instance.AddTreasureCount(1);
          this.m_TreasureBox.Open(DropItem, this.scene.mTreasureDropTemplate, (int) this.m_Drop.gold, this.scene.mTreasureGoldTemplate);
        }

        protected override bool OnMain()
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_TreasureBox, (UnityEngine.Object) null))
            return false;
          if (!((Component) this.m_TreasureBox).get_gameObject().get_activeInHierarchy())
            ((Component) this.m_TreasureBox).get_gameObject().SetActive(true);
          return true;
        }

        public class Normal : SceneBattle.State_MapDead.TreasureDrop
        {
          public Normal(SceneBattle self, SceneBattle.State_MapDead state)
            : base(self, state)
          {
          }
        }
      }
    }

    public class TreasureEvent : SceneBattle.SimpleEvent.Interface
    {
      public static int GROUP = nameof (TreasureEvent).GetHashCode();

      public void OnEvent(string key, object obj)
      {
        if (key == "DropItemEffect.End")
        {
          DropItemEffect dropItemEffect = obj as DropItemEffect;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dropItemEffect, (UnityEngine.Object) null))
            return;
          Unit dropOwner = dropItemEffect.DropOwner;
          if (dropOwner != null)
            dropOwner.EndDropDirection();
          SceneBattle.Instance.AddDispTreasureCount(1);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dropItemEffect, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) dropItemEffect.TargetRect, (UnityEngine.Object) null))
            return;
          GameParameter.UpdateAll(((Component) dropItemEffect.TargetRect).get_gameObject());
        }
        else
        {
          if (!(key == "DropGoldEffect.End"))
            return;
          DropGoldEffect dropGoldEffect = obj as DropGoldEffect;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dropGoldEffect, (UnityEngine.Object) null))
            return;
          Unit dropOwner = dropGoldEffect.DropOwner;
          if (dropOwner != null)
            dropOwner.EndDropDirection();
          SceneBattle.Instance.AddGoldCount(dropGoldEffect.Gold);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dropGoldEffect.TargetRect, (UnityEngine.Object) null))
            return;
          GameParameter.UpdateAll(((Component) dropGoldEffect.TargetRect).get_gameObject());
        }
      }
    }

    private class State_MapRevive : SceneBattle.State_SpawnUnit
    {
    }

    private class State_PickupGimmick : State<SceneBattle>
    {
      private SceneBattle mScene;
      private TacticsUnitController mUnitController;
      private TacticsUnitController mGimmickController;
      private Unit.UnitDrop mDrop;
      private MapPickup mPickup;
      private bool mLoadFinished;
      private bool mItemDrop;
      private LogMapEvent mLog;
      private DropItemEffect mDropItemEffect;

      public override void Begin(SceneBattle self)
      {
        this.mLog = (LogMapEvent) self.mBattle.Logs.Peek;
        this.mScene = self;
        this.mGimmickController = self.FindUnitController(this.mLog.target);
        this.mUnitController = self.FindUnitController(this.mLog.self);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitController, (UnityEngine.Object) null))
        {
          self.RemoveLog();
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if (this.mLog.target != null)
            this.mLog.target.BeginDropDirection();
          self.mUpdateCameraPosition = true;
          self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).get_transform());
          self.InterpCameraTarget((Component) this.mUnitController);
          this.mGimmickController.ResetScale();
          this.mDrop = this.mLog.target.Drop;
          if (this.mDrop != null)
          {
            this.mItemDrop = this.mGimmickController.Unit != null && this.mGimmickController.Unit.CheckItemDrop(false);
            if (this.mItemDrop)
              SceneBattle.Instance.AddTreasureCount(1);
          }
          this.mUnitController.BeginLoadPickupAnimation();
          this.mPickup = (MapPickup) ((Component) this.mGimmickController).GetComponentInChildren<MapPickup>();
          this.mPickup.OnPickup = new MapPickup.PickupEvent(this.OnPickupDone);
          this.mGimmickController.BeginLoadPickupAnimation();
          if (!this.mItemDrop || this.mDrop == null || this.mDrop.items.Count <= 0)
            return;
          this.mDropItemEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) this.mScene.mTreasureDropTemplate, Vector3.op_Addition(((Component) this.mGimmickController).get_transform().get_position(), this.mPickup.DropEffectOffset), (Quaternion) null) as DropItemEffect;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDropItemEffect, (UnityEngine.Object) null))
            return;
          ((Component) this.mDropItemEffect).get_transform().SetParent(((Component) self.OverlayCanvas).get_transform(), false);
          ((DropItemIcon) ((Component) this.mDropItemEffect).get_gameObject().GetComponent<DropItemIcon>()).UpdateValue();
          ((Component) this.mDropItemEffect).get_gameObject().SetActive(false);
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mUnitController.IsLoading || this.mGimmickController.IsLoading)
          return;
        this.mGimmickController.CollideGround = false;
        if (!this.mLoadFinished)
        {
          this.mLoadFinished = true;
          if (((LogMapEvent) self.mBattle.Logs.Peek).type == EEventType.Treasure)
          {
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0504", 0.0f);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0501", 0.0f);
          }
          this.mUnitController.PlayPickup(((Component) this.mGimmickController).get_gameObject());
        }
        if (!this.mUnitController.isIdle)
          return;
        self.RemoveLog();
        self.FinishGimmickState();
      }

      public override void End(SceneBattle self)
      {
        this.mPickup.OnPickup = (MapPickup.PickupEvent) null;
        this.mGimmickController.PlayIdle(0.0f);
        this.mGimmickController.UnloadPickupAnimation();
        ((Component) this.mGimmickController).get_gameObject().SetActive(false);
        self.RefreshUnitStatus(this.mUnitController.Unit);
        if (this.mLog.target == null)
          return;
        this.mLog.target.EndDropDirection();
      }

      private void OnPickupDone()
      {
        if (this.mUnitController.Unit.Side != EUnitSide.Player)
          return;
        if (this.mDrop != null)
        {
          Vector3 position = ((Component) this.mGimmickController).get_transform().get_position();
          if ((int) this.mDrop.gold > 0)
          {
            DropGoldEffect dropGoldEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) this.mScene.mTreasureGoldTemplate, position, Quaternion.get_identity()) as DropGoldEffect;
            dropGoldEffect.DropOwner = this.mLog.target;
            dropGoldEffect.Gold = (int) this.mDrop.gold;
          }
          if (this.mDrop.items.Count > 0 && this.mItemDrop)
          {
            ((Component) this.mDropItemEffect).get_gameObject().SetActive(true);
            this.mDropItemEffect.DropOwner = this.mLog.target;
            this.mDropItemEffect.DropItem = this.mDrop.items[this.mDrop.items.Count - 1];
          }
          if ((int) this.mDrop.gems > 0)
            this.self.StartCoroutine(this.GemPramChangePopup());
        }
        this.mGimmickController.PlayTakenAnimation();
      }

      [DebuggerHidden]
      private IEnumerator GemPramChangePopup()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PickupGimmick.\u003CGemPramChangePopup\u003Ec__Iterator3A()
        {
          \u003C\u003Ef__this = this
        };
      }
    }

    private class State_BattleDead : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        LogDead peek = (LogDead) self.mBattle.Logs.Peek;
        List<LogDead.Param> objList = new List<LogDead.Param>();
        objList.AddRange((IEnumerable<LogDead.Param>) peek.list_normal);
        objList.AddRange((IEnumerable<LogDead.Param>) peek.list_sentence);
        using (List<LogDead.Param>.Enumerator enumerator = objList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Unit self1 = enumerator.Current.self;
            self.OnUnitDeath(self1);
            self.HideUnitMarkers(self1);
            TacticsUnitController unitController = self.FindUnitController(self1);
            self.mTacticsUnits.Remove(unitController);
            UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) unitController).get_gameObject());
          }
        }
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void Update(SceneBattle self)
      {
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_CastSkillStart : State<SceneBattle>
    {
      private TacticsUnitController mCasterController;

      public override void Begin(SceneBattle self)
      {
        this.mCasterController = self.FindUnitController(self.mBattle.CurrentUnit);
        SkillData castSkill = this.mCasterController.Unit.CastSkill;
        if (castSkill != null)
        {
          if (castSkill.CastType == ECastTypes.Jump)
          {
            self.GotoState<SceneBattle.State_CastSkillStartJump>();
            return;
          }
          this.mCasterController.ChargeIcon.Close();
          if (castSkill.EffectType == SkillEffectTypes.Changing)
            self.GotoState<SceneBattle.State_CastSkillStartChange>();
        }
        self.Battle.CastSkillStart();
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_CastSkillStartJump : State<SceneBattle>
    {
      private TacticsUnitController mCasterController;

      public override void Begin(SceneBattle self)
      {
        this.mCasterController = self.FindUnitController(self.mBattle.CurrentUnit);
        self.Battle.CastSkillStart();
        self.RemoveLog();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCasterController, (UnityEngine.Object) null))
          this.mCasterController.ChargeIcon.Close();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_CastSkillStartChange : State<SceneBattle>
    {
      private TacticsUnitController mCasterController;

      public override void Begin(SceneBattle self)
      {
        this.mCasterController = self.FindUnitController(self.mBattle.CurrentUnit);
        self.Battle.CastSkillStart();
        self.RemoveLog();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCasterController, (UnityEngine.Object) null))
          ;
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_CastSkillEnd : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.Battle.CastSkillEnd();
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_MapTrick : State<SceneBattle>
    {
      private List<SceneBattle.State_MapTrick.TucMapTrick> mTucMapTrickLists = new List<SceneBattle.State_MapTrick.TucMapTrick>();
      private List<GameObject> mGoEffectLists = new List<GameObject>();
      private List<GameObject> mGoPopupLists = new List<GameObject>();
      private SceneBattle.State_MapTrick.eSeqState mSeqState;
      private float mPassedTime;
      private LogMapTrick mLog;
      private LoadRequest mLoadReqEffect;
      private bool mIsAction;
      private bool mIsDamaged;

      public override void Begin(SceneBattle self)
      {
        this.mLog = self.Battle.Logs.Peek as LogMapTrick;
        if (this.mLog != null && this.mLog.TrickData != null)
        {
          this.mTucMapTrickLists.Clear();
          using (List<LogMapTrick.TargetInfo>.Enumerator enumerator = this.mLog.TargetInfoLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              LogMapTrick.TargetInfo current = enumerator.Current;
              TacticsUnitController unitController = self.FindUnitController(current.Target);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                this.mTucMapTrickLists.Add(new SceneBattle.State_MapTrick.TucMapTrick(unitController, current));
            }
          }
          if (this.mTucMapTrickLists.Count != 0)
          {
            if (!string.IsNullOrEmpty(this.mLog.TrickData.TrickParam.EffectName))
              this.mLoadReqEffect = AssetManager.LoadAsync<GameObject>(AssetPath.TrickEffect(this.mLog.TrickData.TrickParam.EffectName));
            using (List<SceneBattle.State_MapTrick.TucMapTrick>.Enumerator enumerator = this.mTucMapTrickLists.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SceneBattle.State_MapTrick.TucMapTrick current = enumerator.Current;
                LogMapTrick.TargetInfo mTargetInfo = current.mTargetInfo;
                if (mTargetInfo.Damage != 0 || mTargetInfo.Heal != 0 || mTargetInfo.KnockBackGrid != null)
                {
                  if (mTargetInfo.Damage != 0)
                    current.mController.LoadDamageAnimations();
                  this.mIsAction = true;
                }
              }
            }
            if (this.mLoadReqEffect != null || this.mIsAction)
            {
              this.mSeqState = SceneBattle.State_MapTrick.eSeqState.INIT;
              return;
            }
          }
        }
        this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EXIT;
      }

      public override void Update(SceneBattle self)
      {
        if (this.mLog == null || this.mLog.TrickData == null || this.mTucMapTrickLists.Count == 0)
          this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EXIT;
        switch (this.mSeqState)
        {
          case SceneBattle.State_MapTrick.eSeqState.INIT:
            if (this.mLoadReqEffect != null)
            {
              this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EFF_LOAD;
              break;
            }
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.ACT_INIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.EFF_LOAD:
            if (this.mLoadReqEffect != null)
            {
              if (!this.mLoadReqEffect.isDone)
                break;
              this.mGoEffectLists.Clear();
              GameObject asset = this.mLoadReqEffect.asset as GameObject;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) asset))
              {
                using (List<SceneBattle.State_MapTrick.TucMapTrick>.Enumerator enumerator = this.mTucMapTrickLists.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    SceneBattle.State_MapTrick.TucMapTrick current = enumerator.Current;
                    GameObject go = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) asset);
                    if (UnityEngine.Object.op_Implicit((UnityEngine.Object) go))
                    {
                      go.get_transform().SetParent(((Component) current.mController).get_transform(), false);
                      ParticleSystem[] componentsInChildren = (ParticleSystem[]) go.GetComponentsInChildren<ParticleSystem>();
                      if (componentsInChildren != null)
                      {
                        foreach (ParticleSystem particleSystem in componentsInChildren)
                        {
                          ParticleSystem.SubEmittersModule subEmitters = particleSystem.get_subEmitters();
                          // ISSUE: explicit reference operation
                          ((ParticleSystem.SubEmittersModule) @subEmitters).set_enabled(true);
                        }
                      }
                      GameUtility.RequireComponent<OneShotParticle>(go);
                      this.mGoEffectLists.Add(go);
                    }
                  }
                }
              }
              this.mLoadReqEffect = (LoadRequest) null;
              this.mPassedTime = 0.0f;
              this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EFF_PLAY;
              break;
            }
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.ACT_INIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.EFF_PLAY:
            bool flag1 = false;
            this.mPassedTime += Time.get_deltaTime();
            if (this.mGoEffectLists.Count != 0)
            {
              for (int index = 0; index < this.mGoEffectLists.Count; ++index)
              {
                if ((double) this.mPassedTime < (double) GameSettings.Instance.Quest.TrickEffectWaitMaxTime && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mGoEffectLists[index]))
                {
                  flag1 = true;
                  break;
                }
                this.mGoEffectLists[index] = (GameObject) null;
              }
            }
            if (flag1)
              break;
            this.mGoEffectLists.Clear();
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.ACT_INIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.ACT_INIT:
            if (this.mIsAction)
            {
              bool flag2 = false;
              using (List<SceneBattle.State_MapTrick.TucMapTrick>.Enumerator enumerator = this.mTucMapTrickLists.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  if (enumerator.Current.mController.IsLoading)
                  {
                    flag2 = true;
                    break;
                  }
                }
              }
              if (flag2)
                break;
              this.mGoPopupLists.Clear();
              using (List<SceneBattle.State_MapTrick.TucMapTrick>.Enumerator enumerator = this.mTucMapTrickLists.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  SceneBattle.State_MapTrick.TucMapTrick current = enumerator.Current;
                  TacticsUnitController mController = current.mController;
                  LogMapTrick.TargetInfo mTargetInfo = current.mTargetInfo;
                  GameObject gameObject = (GameObject) null;
                  if (mTargetInfo.Damage != 0)
                  {
                    gameObject = self.PopupDamageNumber(mController.CenterPosition, mTargetInfo.Damage);
                    mController.PlayDamage(HitReactionTypes.Normal);
                    MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mLog.TrickData.CreateUnit, mTargetInfo.Target, mTargetInfo.Damage);
                    this.mIsDamaged = true;
                  }
                  else if (mTargetInfo.Heal != 0)
                    gameObject = self.PopupHpHealNumber(mController.CenterPosition, mTargetInfo.Heal);
                  if (UnityEngine.Object.op_Implicit((UnityEngine.Object) gameObject))
                    this.mGoPopupLists.Add(gameObject);
                  if (mTargetInfo.KnockBackGrid != null)
                  {
                    mController.KnockBackGrid = mTargetInfo.KnockBackGrid;
                    mController.PlayTrickKnockBack(false);
                  }
                }
              }
              this.mSeqState = SceneBattle.State_MapTrick.eSeqState.ACT_PLAY;
              break;
            }
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EXIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.ACT_PLAY:
            bool flag3 = false;
            if (this.mGoPopupLists.Count != 0)
            {
              for (int index = 0; index < this.mGoPopupLists.Count; ++index)
              {
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mGoPopupLists[index]))
                {
                  flag3 = true;
                  break;
                }
                this.mGoPopupLists[index] = (GameObject) null;
              }
            }
            using (List<SceneBattle.State_MapTrick.TucMapTrick>.Enumerator enumerator = this.mTucMapTrickLists.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                if (enumerator.Current.mController.IsBusy)
                {
                  flag3 = true;
                  break;
                }
              }
            }
            if (flag3)
              break;
            this.mGoPopupLists.Clear();
            using (List<SceneBattle.State_MapTrick.TucMapTrick>.Enumerator enumerator = this.mTucMapTrickLists.GetEnumerator())
            {
              while (enumerator.MoveNext())
                enumerator.Current.mController.UnloadBattleAnimations();
            }
            this.mSeqState = SceneBattle.State_MapTrick.eSeqState.EXIT;
            break;
          case SceneBattle.State_MapTrick.eSeqState.EXIT:
            TrickData.CheckRemoveMarker(this.mLog.TrickData);
            self.RefreshJumpSpots();
            this.mLog = (LogMapTrick) null;
            self.RemoveLog();
            if (this.mIsDamaged)
            {
              self.GotoState<SceneBattle.State_TriggerHPEvents>();
              break;
            }
            self.GotoState<SceneBattle.State_WaitForLog>();
            break;
        }
      }

      private enum eSeqState
      {
        INIT,
        EFF_LOAD,
        EFF_PLAY,
        ACT_INIT,
        ACT_PLAY,
        EXIT,
      }

      private class TucMapTrick
      {
        public TacticsUnitController mController;
        public LogMapTrick.TargetInfo mTargetInfo;

        public TucMapTrick(TacticsUnitController tuc, LogMapTrick.TargetInfo ti)
        {
          this.mController = tuc;
          this.mTargetInfo = ti;
        }
      }
    }

    private class State_UnitEnd : State<SceneBattle>
    {
      private bool mIsPopDamage;
      private bool mIsShakeStart;
      private Unit mCurrentUnit;
      private TacticsUnitController mController;
      private bool mIsDamaged;

      public override void Begin(SceneBattle self)
      {
        self.ToggleRenkeiAura(false);
        if (self.mTutorialTriggers != null)
        {
          TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
              self.mTutorialTriggers[index].OnUnitEnd(self.mBattle.CurrentUnit, unitController.Unit.TurnCount);
          }
        }
        self.HideUnitCursor(((LogUnitEnd) self.mBattle.Logs.Peek).self);
        if (self.mBattle.IsUnitAuto(self.mBattle.CurrentUnit))
          self.HideGrid();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTouchController, (UnityEngine.Object) null))
          self.mTouchController.IgnoreCurrentTouch();
        VirtualStick2 virtualStick = self.mBattleUI.VirtualStick;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) virtualStick, (UnityEngine.Object) null))
          virtualStick.Visible = false;
        this.mCurrentUnit = self.Battle.CurrentUnit;
        this.mController = self.FindUnitController(this.mCurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) null, (UnityEngine.Object) this.mController))
        {
          this.mController.InitShake(((Component) this.mController).get_transform().get_position(), this.mCurrentUnit.Direction.ToVector());
          if (self.Battle.IsMultiVersus)
            this.mController.PlayVersusCursor(false);
        }
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        if (self.IsHPGaugeChanging)
          return;
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) null, (UnityEngine.Object) this.mController))
        {
          if (self.Battle.IsMultiPlay)
            self.EndMultiPlayer();
          self.Battle.UnitEnd();
          if (self.IsPlayingArenaQuest)
            self.ArenaActionCountSet(self.Battle.ArenaActionCount);
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if (!this.mController.ShakeStart)
          {
            if (self.Battle.IsMultiPlay)
              self.EndMultiPlayer();
            bool isDead = this.mCurrentUnit.IsDead;
            self.Battle.UnitEnd();
            if (self.IsPlayingArenaQuest)
              self.ArenaActionCountSet(self.Battle.ArenaActionCount);
            LogDamage peek = self.Battle.Logs.Peek as LogDamage;
            if (peek != null && !this.mCurrentUnit.IsDead)
            {
              this.mController.ShakeStart = true;
              return;
            }
            if (peek != null && isDead != this.mCurrentUnit.IsDead)
            {
              self.PopupDamageNumber(((Component) this.mController).get_transform().get_position(), peek.damage);
              this.mController.ReflectDispModel();
              this.mIsDamaged = true;
              for (int index = 0; index < this.mCurrentUnit.CondAttachments.Count; ++index)
              {
                if (this.mCurrentUnit.CondAttachments[index].user != null)
                {
                  MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mCurrentUnit.CondAttachments[index].user, this.mCurrentUnit, peek.damage);
                  break;
                }
              }
            }
          }
          else
          {
            this.mController.AdvanceShake();
            if (0.300000011920929 <= (double) this.mController.GetShakeProgress() && !this.mIsPopDamage)
            {
              this.mIsPopDamage = true;
              LogDamage peek = self.Battle.Logs.Peek as LogDamage;
              self.PopupDamageNumber(((Component) this.mController).get_transform().get_position(), peek.damage);
              this.mController.ReflectDispModel();
              this.mIsDamaged = true;
              for (int index = 0; index < this.mCurrentUnit.CondAttachments.Count; ++index)
              {
                if (this.mCurrentUnit.CondAttachments[index].user != null)
                {
                  MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mCurrentUnit.CondAttachments[index].user, this.mCurrentUnit, peek.damage);
                  break;
                }
              }
            }
            if (!this.mController.IsShakeEnd())
              return;
          }
          if (this.mIsDamaged)
            self.GotoState<SceneBattle.State_TriggerHPEvents>();
          else
            self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
        if (this.mCurrentUnit.OwnerPlayerIndex > 0)
          self.SendCheckMultiPlay();
        self.HideAllHPGauges();
        if (self.mFirstTurn)
          self.mFirstTurn = false;
        self.DeleteOnGimmickIcon();
      }
    }

    private class State_OrdealChangeNext : State<SceneBattle>
    {
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        this.mIsFinished = false;
        self.StartCoroutine(this.StepOrdealChangeNextTeam());
      }

      [DebuggerHidden]
      private IEnumerator StepOrdealChangeNextTeam()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_OrdealChangeNext.\u003CStepOrdealChangeNextTeam\u003Ec__Iterator3B()
        {
          \u003C\u003Ef__this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_MapEndV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.SendCheckMultiPlay();
        self.RemoveLog();
        if (self.mTutorialTriggers != null)
        {
          for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
            self.mTutorialTriggers[index].OnMapEnd();
        }
        switch (self.Battle.GetQuestResult())
        {
          case BattleCore.QuestResult.Win:
            if (self.Battle.CheckNextMap())
            {
              self.mTacticsUnits.Clear();
              self.Battle.IncrementMap();
              self.GotoMapStart();
              break;
            }
            self.TriggerWinEvent();
            break;
          default:
            self.TriggerWinEvent();
            break;
        }
      }
    }

    public enum ExitRequests
    {
      None,
      End,
      Restart,
    }

    private class State_ArenaSkipWait : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        SRPG_TouchInputModule.UnlockInput(false);
        self.Battle.Logs.Reset();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI, (UnityEngine.Object) null))
        {
          self.mBattleUI.OnQuestEnd();
          self.mBattleUI.OnMapEnd();
        }
        self.GotoState_WaitSignal<SceneBattle.State_PreQuestResult>();
      }
    }

    private class State_PreQuestResult : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        BattleCore.QuestResult questResult = self.Battle.GetQuestResult();
        if (!self.IsPlayingArenaQuest)
        {
          self.SaveResult();
          GlobalVars.LastQuestResult.Set(questResult);
        }
        if (self.Battle.IsMultiTower)
        {
          MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
            instance.ForceCloseRoom();
        }
        switch (questResult)
        {
          case BattleCore.QuestResult.Win:
            if (self.Battle.IsMultiPlay && !self.Battle.IsMultiTower && !self.Battle.IsMultiVersus)
            {
              MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
                instance.ForceCloseRoom();
            }
            self.SendIgnoreMyDisconnect();
            self.OnQuestEnd();
            if (self.Battle.IsMultiPlay)
              MonoSingleton<GameManager>.Instance.Player.IncrementChallengeMultiNum();
            if (!self.IsPlayingArenaQuest)
              self.SubmitResult();
            if (!self.CurrentQuest.Silent)
            {
              if (self.IsPlayingArenaQuest)
                self.mBattleUI.OnArenaWin();
              else if (self.Battle.IsMultiVersus)
                self.mBattleUI.OnVersusWin();
              else
                self.mBattleUI.OnQuestWin();
              MonoSingleton<MySound>.Instance.PlayJingle("JIN_0002", 0.0f, (string) null);
              Unit unit;
              if (self.IsPlayingArenaQuest && self.Battle.IsArenaSkip)
              {
                unit = self.Battle.Units.Find((Predicate<Unit>) (u =>
                {
                  if (u == self.Battle.Leader && u.Side == EUnitSide.Player && u.IsEntry)
                    return !u.IsSub;
                  return false;
                }));
              }
              else
              {
                unit = self.Battle.CurrentUnit;
                if (unit != null && unit.Side != EUnitSide.Player && self.mLastPlayerSideUseSkillUnit != null)
                  unit = self.mLastPlayerSideUseSkillUnit;
              }
              if (unit != null)
                unit.PlayBattleVoice("battle_0029");
            }
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
            {
              if (!self.Battle.IsMultiVersus)
                self.mBattleUI_MultiPlay.OnQuestWin();
              else
                self.mExecDisconnected = true;
            }
            if (self.IsPlayingArenaQuest)
            {
              GameUtility.SetDefaultSleepSetting();
              self.GotoState_WaitSignal<SceneBattle.State_Result>();
              break;
            }
            self.GotoState_WaitSignal<SceneBattle.State_Result>();
            break;
          case BattleCore.QuestResult.Lose:
            self.OnQuestEnd();
            if (self.IsPlayingArenaQuest)
              self.mBattleUI.OnArenaLose();
            else if (self.Battle.IsMultiVersus)
              self.mBattleUI.OnVersusLose();
            else
              self.mBattleUI.OnQuestLose();
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null) && !self.Battle.IsMultiVersus)
              self.mBattleUI_MultiPlay.OnQuestLose();
            MonoSingleton<MySound>.Instance.PlayJingle("JIN_0003", 0.0f, (string) null);
            if (self.Battle.IsMultiPlay)
            {
              if (!self.CurrentQuest.CheckDisableContinue() && !self.Battle.IsMultiVersus)
              {
                self.GotoState_WaitSignal<SceneBattle.State_MultiPlayContinue>();
                break;
              }
              self.mExecDisconnected = true;
              self.SendIgnoreMyDisconnect();
              if (!self.Battle.IsMultiVersus)
              {
                self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
                break;
              }
              if (!self.mQuestResultSent)
                self.SubmitResult();
              self.GotoState_WaitSignal<SceneBattle.State_Result>();
              break;
            }
            if (self.IsPlayingArenaQuest)
            {
              if (self.Battle.IsArenaSkip)
              {
                Unit unit = self.Battle.Units.Find((Predicate<Unit>) (u =>
                {
                  if (u == self.Battle.Leader && u.Side == EUnitSide.Player && u.IsEntry)
                    return !u.IsSub;
                  return false;
                }));
                if (unit != null)
                  unit.PlayBattleVoice("battle_0028");
              }
              GameUtility.SetDefaultSleepSetting();
              self.GotoState_WaitSignal<SceneBattle.State_Result>();
              break;
            }
            if (self.IsPlayingTower)
            {
              if (self.mCurrentQuest.HasMission())
              {
                if (!self.mQuestResultSent)
                  self.SubmitResult();
                self.GotoState_WaitSignal<SceneBattle.State_Result>();
                break;
              }
              self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
              break;
            }
            bool flag = !self.CurrentQuest.CheckDisableContinue();
            if (flag)
            {
              BattleMap currentMap = self.Battle.CurrentMap;
              if (currentMap != null)
                flag = !self.Battle.CheckMonitorActionCount(currentMap.LoseMonitorCondition);
            }
            if (self.Battle.IsOrdeal)
              flag = false;
            if (flag)
            {
              self.GotoState_WaitSignal<SceneBattle.State_ConfirmContinue>();
              break;
            }
            self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
            break;
          case BattleCore.QuestResult.Draw:
            if (!self.Battle.IsMultiVersus)
              break;
            self.OnQuestEnd();
            self.mBattleUI.OnVersusDraw();
            self.mExecDisconnected = true;
            self.SendIgnoreMyDisconnect();
            if (!self.mQuestResultSent)
              self.SubmitResult();
            self.GotoState_WaitSignal<SceneBattle.State_Result>();
            break;
          default:
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
              self.mBattleUI_MultiPlay.OnQuestRetreat();
            GameUtility.FadeOut(2f);
            self.GotoState<SceneBattle.State_ExitQuest>();
            break;
        }
      }
    }

    private class State_Result : State<SceneBattle>
    {
      private bool mIsResDestroy;
      private LoadRequest mResLoadReq;
      private float mResPassedTime;
      private bool mOnResult;

      public override void Begin(SceneBattle self)
      {
        BattleCore.Record questRecord = self.mBattle.GetQuestRecord();
        if (self.IsPlayingArenaQuest)
        {
          this.mOnResult = true;
          MonoSingleton<MySound>.Instance.PlayBGM("BGM_0006", (string) null, false);
          self.mBattleUI.OnResult_Arena();
        }
        else
        {
          bool flag1 = false;
          bool flag2 = false;
          if (self.Battle.IsMultiPlay)
          {
            List<MultiFuid> multiFuids = MonoSingleton<GameManager>.Instance.Player.MultiFuids;
            MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
            List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
            if (myPlayersStarted != null && MonoSingleton<GameManager>.Instance.Player != null)
            {
              for (int index = 0; index < myPlayersStarted.Count; ++index)
              {
                JSON_MyPhotonPlayerParam startedPlayer = myPlayersStarted[index];
                if (startedPlayer != null && startedPlayer.playerIndex != instance.MyPlayerIndex && !string.IsNullOrEmpty(startedPlayer.FUID))
                {
                  MultiFuid multiFuid = multiFuids?.Find((Predicate<MultiFuid>) (f =>
                  {
                    if (f.fuid != null)
                      return f.fuid.Equals(startedPlayer.FUID);
                    return false;
                  }));
                  if (multiFuid != null && multiFuid.status.Equals("follower"))
                  {
                    flag1 = true;
                    break;
                  }
                }
              }
              flag2 = instance.IsMultiVersus;
            }
          }
          if (questRecord.IsZero && !flag1 && (self.CurrentQuest.type != QuestTypes.Tower && !flag2) && (!self.Battle.IsMultiTower && !MonoSingleton<GameManager>.Instance.IsVSCpuBattle))
          {
            self.ExitRequest = SceneBattle.ExitRequests.End;
          }
          else
          {
            if (self.IsOrdealQuest && !string.IsNullOrEmpty(self.FirstClearItemId))
            {
              GameManager instance = MonoSingleton<GameManager>.Instance;
              string firstClearItemId = self.FirstClearItemId;
              ItemParam itemParam = instance.GetItemParam(firstClearItemId);
              if (itemParam != null && itemParam.type == EItemType.Unit)
              {
                UnitParam unitParam = instance.GetUnitParam(firstClearItemId);
                if (unitParam != null)
                  DownloadUtility.DownloadUnit(unitParam, (JobData[]) null);
              }
            }
            for (int index = 0; index < questRecord.items.Count; ++index)
            {
              BattleCore.DropItemParam dropItemParam = questRecord.items[index];
              if (dropItemParam.IsConceptCard && !string.IsNullOrEmpty(dropItemParam.conceptCardParam.first_get_unit))
                DownloadUtility.DownloadConceptCard(dropItemParam.conceptCardParam);
            }
            if (!AssetDownloader.isDone)
              AssetDownloader.StartDownload(false, true, ThreadPriority.Normal);
            string resultBgResourcePath = GameSettings.Instance.BattleResultBg_ResourcePath;
            if (!string.IsNullOrEmpty(resultBgResourcePath))
            {
              this.mResLoadReq = AssetManager.LoadAsync<GameObject>(resultBgResourcePath);
              if (this.mResLoadReq != null)
                return;
              this.mIsResDestroy = true;
            }
            else
              this.mIsResDestroy = true;
          }
        }
      }

      private void StartResult()
      {
        if (this.mOnResult)
          return;
        this.mOnResult = true;
        MonoSingleton<MySound>.Instance.PlayBGM("BGM_0006", (string) null, false);
        if (this.self.Battle.IsMultiPlay)
        {
          if (this.self.Battle.IsMultiVersus)
          {
            if (this.self.Battle.QuestType == QuestTypes.RankMatch)
              this.self.mBattleUI.OnResult_RankMatch();
            else
              this.self.mBattleUI.OnResult_Versus();
          }
          else if (GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.TOWER)
            this.self.mBattleUI.OnResult_MultiTower();
          else
            this.self.mBattleUI.OnResult_MP();
        }
        else if (this.self.Battle.QuestType == QuestTypes.Tower)
          this.self.mBattleUI.OnResult_Tower();
        else if (this.self.Battle.QuestType == QuestTypes.VersusFree || this.self.Battle.QuestType == QuestTypes.VersusRank)
          this.self.mBattleUI.OnResult_Versus();
        else
          this.self.mBattleUI.OnResult();
      }

      public override void Update(SceneBattle self)
      {
        if (!self.mQuestResultSent || !AssetDownloader.isDone)
          return;
        if (!this.mIsResDestroy)
        {
          if (this.mResLoadReq != null)
          {
            if (!this.mResLoadReq.isDone)
              return;
            this.mIsResDestroy = true;
            if (UnityEngine.Object.op_Inequality(this.mResLoadReq.asset, (UnityEngine.Object) null))
            {
              self.GoResultBg = UnityEngine.Object.Instantiate(this.mResLoadReq.asset) as GameObject;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.GoResultBg))
              {
                self.GoResultBg.get_transform().SetParent(((Component) self.mBattleUI).get_transform(), false);
                if (GameSettings.Instance.BattleResultBg_UseBattleBG)
                {
                  ResultMask component = (ResultMask) self.GoResultBg.GetComponent<ResultMask>();
                  if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component) && UnityEngine.Object.op_Implicit((UnityEngine.Object) Camera.get_main()))
                  {
                    RenderPipeline renderPipeline = GameUtility.RequireComponent<RenderPipeline>(((Component) Camera.get_main()).get_gameObject());
                    if (UnityEngine.Object.op_Implicit((UnityEngine.Object) renderPipeline))
                      component.SetBg(renderPipeline.BackgroundImage as Texture2D);
                  }
                }
                this.mIsResDestroy = false;
              }
            }
            this.mResLoadReq = (LoadRequest) null;
            return;
          }
          this.mResPassedTime += Time.get_deltaTime();
          if ((double) this.mResPassedTime < (double) GameSettings.Instance.BattleResultBg_WaitTime)
            return;
          for (int index = self.mTacticsUnits.Count - 1; index >= 0; --index)
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) self.mTacticsUnits[index]).get_gameObject());
            self.mTacticsUnits.RemoveAt(index);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTacticsSceneRoot, (UnityEngine.Object) null))
          {
            UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) self.mTacticsSceneRoot).get_gameObject());
            self.mTacticsSceneRoot = (TacticsSceneSettings) null;
          }
          self.DestroyUI(true);
          this.mIsResDestroy = true;
        }
        if (self.ExitRequest == SceneBattle.ExitRequests.End)
        {
          if (self.mBattle.GetQuestResult() == BattleCore.QuestResult.Win && !this.mOnResult && (self.IsPlayingMultiQuest && self.mFirstContact > 0))
            self.mBattleUI.OnFirstContact();
          self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
        }
        else if (self.ExitRequest == SceneBattle.ExitRequests.Restart)
          self.GotoState<SceneBattle.State_Restart_SelectSupport>();
        else
          this.StartResult();
      }
    }

    private class State_RestartQuest : State<SceneBattle>
    {
      private bool mRequested;

      public override void Begin(SceneBattle self)
      {
        GameUtility.FadeOut(1f);
      }

      public override void Update(SceneBattle self)
      {
        if (GameUtility.IsScreenFading || this.mRequested)
          return;
        this.mRequested = true;
        GameUtility.RequestScene("Battle");
      }
    }

    private class State_ContinueQuest : State<SceneBattle>
    {
      private bool mRequested;
      private SceneBattle mScene;

      public override void Begin(SceneBattle self)
      {
        this.mScene = self;
        GameUtility.FadeOut(1f);
      }

      public override void Update(SceneBattle self)
      {
        if (GameUtility.IsScreenFading || this.mRequested)
          return;
        BattleCore.Record questRecord = self.Battle.GetQuestRecord();
        if (Network.Mode == Network.EConnectMode.Online)
          Network.RequestAPI((WebAPI) new ReqBtlComCont(self.Battle.BtlID, questRecord, new Network.ResponseCallback(this.OnSuccess), PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay, self.Battle.IsMultiTower), false);
        else
          this.ResBtlComCont(new BattleCore.Json_Battle()
          {
            btlid = this.mScene.Battle.BtlID,
            btlinfo = new BattleCore.Json_BtlInfo()
          });
        this.mRequested = true;
      }

      private void OnSuccess(WWWResult www)
      {
        if (Network.IsError)
        {
          switch (Network.ErrCode)
          {
            case Network.EErrCode.ContinueCostShort:
              this.self.GotoState<SceneBattle.State_ExitQuest>();
              break;
            case Network.EErrCode.CantContinue:
              FlowNode_Network.Failed();
              break;
            default:
              FlowNode_Network.Retry();
              break;
          }
        }
        else
        {
          WebAPI.JSON_BodyResponse<BattleCore.Json_Battle> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<BattleCore.Json_Battle>>(www.text);
          DebugUtility.Assert(jsonObject != null, "res == null");
          if (jsonObject.body == null)
          {
            FlowNode_Network.Retry();
          }
          else
          {
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("ContinueQuest", (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost);
            this.ResBtlComCont(jsonObject.body);
          }
        }
      }

      private void ResBtlComCont(BattleCore.Json_Battle response)
      {
        if (response == null)
          return;
        this.mScene.CloseBattleUI();
        List<Unit> player = this.mScene.Battle.Player;
        for (int index = 0; index < player.Count; ++index)
        {
          TacticsUnitController unitController = this.mScene.FindUnitController(player[index]);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            if (player[index].IsJump)
              unitController.SetVisible(true);
            if (unitController.Unit.IsDead)
            {
              this.mScene.mTacticsUnits.Remove(unitController);
              GameUtility.DestroyGameObject(((Component) unitController).get_gameObject());
            }
          }
        }
        GameUtility.FadeIn(1f);
        this.mScene.mUnitStartCount = 0;
        using (List<Unit>.Enumerator enumerator = this.mScene.Battle.ContinueStart(response.btlid, response.btlinfo.seed).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TacticsUnitController unitController = this.mScene.FindUnitController(enumerator.Current);
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            {
              this.mScene.mTacticsUnits.Remove(unitController);
              GameUtility.DestroyGameObject(((Component) unitController).get_gameObject());
            }
          }
        }
        this.mScene.RefreshJumpSpots();
        for (int index = 0; index < player.Count; ++index)
        {
          TacticsUnitController unitController = this.mScene.FindUnitController(player[index]);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            unitController.UpdateBadStatus();
        }
        UnitQueue.Instance.Refresh(0);
        this.self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_ExitQuest : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        if (!self.mQuestResultSent && !MonoSingleton<GameManager>.Instance.AudienceMode)
          self.SubmitResult();
        self.mPauseReqCount = 0;
        TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.Game, 1f);
        self.StartCoroutine(this.ExitAsync());
      }

      [DebuggerHidden]
      private IEnumerator ExitAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_ExitQuest.\u003CExitAsync\u003Ec__Iterator3C()
        {
          \u003C\u003Ef__this = this
        };
      }
    }

    private class State_Restart_SelectSupport : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.mBattleUI.OnSupportSelectStart();
      }

      public override void End(SceneBattle self)
      {
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
        {
          SupportData selectedSupport = (SupportData) GlobalVars.SelectedSupport;
          GlobalVars.SelectedFriendID = selectedSupport == null ? (string) null : selectedSupport.FUID;
          this.self.GotoState<SceneBattle.State_RestartQuest>();
        }
        else
          this.self.GotoState<SceneBattle.State_ExitQuest>();
      }
    }

    private class State_ConfirmContinue : State<SceneBattle>
    {
      private void OnDecide(GameObject dialog)
      {
        if (MonoSingleton<GameManager>.Instance.Player.Coin >= (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost)
        {
          MonoSingleton<GameManager>.Instance.Player.DEBUG_CONSUME_COIN((int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost);
          this.self.GotoState<SceneBattle.State_ContinueQuest>();
        }
        else
          MonoSingleton<GameManager>.Instance.ConfirmBuyCoin(new GameManager.BuyCoinEvent(this.OnBuyCoinEnd), new GameManager.BuyCoinEvent(this.OnBuyCoinCancel));
      }

      private void OnBuyCoinEnd()
      {
        this.Confirm();
      }

      private void OnBuyCoinCancel()
      {
        this.self.GotoState<SceneBattle.State_ExitQuest>();
      }

      private void OnCancel(GameObject dialog)
      {
        this.self.GotoState<SceneBattle.State_ExitQuest>();
      }

      public override void Begin(SceneBattle self)
      {
        this.Confirm();
      }

      private void Confirm()
      {
        if (ContinueWindow.Create(this.self.mContinueWindowRes, new ContinueWindow.ResultEvent(this.OnDecide), new ContinueWindow.ResultEvent(this.OnCancel)))
          return;
        this.OnCancel((GameObject) null);
      }
    }

    private struct PosRot
    {
      public Vector3 Position;
      public Quaternion Rotation;

      public void Apply(Transform target)
      {
        target.set_position(this.Position);
        target.set_rotation(this.Rotation);
      }

      public void Apply(Transform target, Vector3 deltaPosition)
      {
        target.set_position(Vector3.op_Addition(this.Position, deltaPosition));
        target.set_rotation(this.Rotation);
      }

      public void AddPosition(Component component)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        SceneBattle.PosRot& local = @this;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).Position = Vector3.op_Addition((^local).Position, component.get_transform().get_position());
      }
    }

    private class State_AutoHeal : State<SceneBattle>
    {
      private GameObject mHealEffect;
      private TacticsUnitController mController;
      private LogAutoHeal mLog;
      private SceneBattle.State_AutoHeal.State mState;
      private int mHealdHp;
      private int mHealdMp;

      public override void Begin(SceneBattle self)
      {
        this.mLog = self.Battle.Logs.Peek as LogAutoHeal;
        this.mController = self.FindUnitController(this.mLog.self);
        this.mHealdHp = (int) this.mLog.self.CurrentStatus.param.hp;
        this.mHealdMp = (int) this.mLog.self.CurrentStatus.param.mp;
        this.mLog.self.CurrentStatus.param.hp = (OInt) this.mLog.beforeHp;
        if (this.mLog.type == LogAutoHeal.HealType.Hp)
          this.mController.ResetHPGauge();
        else
          this.mController.ShowHPGauge(false);
        this.mState = SceneBattle.State_AutoHeal.State.Start;
        self.RemoveLog();
      }

      public override void End(SceneBattle self)
      {
        this.mLog.self.CurrentStatus.param.hp = (OInt) this.mHealdHp;
        this.mLog.self.CurrentStatus.param.mp = (OShort) this.mHealdMp;
        this.mController.ResetHPGauge();
        this.mController.ShowHPGauge(false);
        this.mController.ShowOwnerIndexUI(false);
        base.End(self);
      }

      public override void Update(SceneBattle self)
      {
        if (this.mState == SceneBattle.State_AutoHeal.State.Start)
        {
          this.mState = SceneBattle.State_AutoHeal.State.Wait;
          self.StartCoroutine(this.Wait(0.1f));
        }
        else
        {
          if (this.mState == SceneBattle.State_AutoHeal.State.Wait)
            return;
          if (this.mState == SceneBattle.State_AutoHeal.State.EffectStart)
            this.StartEffect();
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) this.mHealEffect, (UnityEngine.Object) null) || this.mController.IsHPGaugeChanging)
            return;
          if (this.mLog.type == LogAutoHeal.HealType.Jewel)
            UnitQueue.Instance.Refresh(this.mLog.self);
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      [DebuggerHidden]
      private IEnumerator Wait(float wait)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_AutoHeal.\u003CWait\u003Ec__Iterator3D()
        {
          wait = wait,
          \u003C\u0024\u003Ewait = wait,
          \u003C\u003Ef__this = this
        };
      }

      private void StartEffect()
      {
        GameObject gameObject = this.mLog.type != LogAutoHeal.HealType.Hp ? this.self.mMapAddGemEffectTemplate : this.self.mAutoHealEffectTemplate;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        {
          this.mHealEffect = UnityEngine.Object.Instantiate((UnityEngine.Object) gameObject, ((Component) this.mController).get_transform().get_position(), Quaternion.get_identity()) as GameObject;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mHealEffect, (UnityEngine.Object) null))
          {
            this.mHealEffect.get_transform().SetParent(((Component) this.mController).get_transform());
            this.mHealEffect.RequireComponent<OneShotParticle>();
          }
        }
        SceneBattle.Instance.PopupHpHealNumber(this.mController.CenterPosition, this.mLog.value);
        this.mController.ReflectDispModel();
        if (this.mLog.type != LogAutoHeal.HealType.Jewel)
          this.mController.UpdateHPRelative(this.mLog.value, 0.5f, false);
        this.mState = SceneBattle.State_AutoHeal.State.Effect;
      }

      private enum State
      {
        Start,
        Wait,
        End,
        EffectStart,
        Effect,
      }
    }

    private class State_PrepareCast : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        LogCast peek = self.Battle.Logs.Peek as LogCast;
        switch (peek.type)
        {
          case ECastTypes.Chant:
            this.EnterChant(peek);
            self.RemoveLog();
            self.GotoState<SceneBattle.State_WaitForLog>();
            break;
          case ECastTypes.Charge:
            this.EnterChant(peek);
            self.RemoveLog();
            self.GotoState<SceneBattle.State_WaitForLog>();
            break;
          case ECastTypes.Jump:
            self.GotoState<SceneBattle.State_JumpCastStart>();
            break;
        }
      }

      private void EnterChant(LogCast Log)
      {
        TacticsUnitController unitController = this.self.FindUnitController(Log.self);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          unitController.ChargeIcon.Open();
        if (Log.self == null || Log.self.CastSkill == null || Log.self.CastSkill.TeleportType == eTeleportType.None)
          return;
        this.self.RefreshJumpSpots();
      }
    }

    private class State_JumpCastStart : State<SceneBattle>
    {
      private float WaitTime = 1f;
      private bool mDirection = true;
      private TacticsUnitController mCasterController;
      private float CountTime;

      public override void Begin(SceneBattle self)
      {
        LogCast peek = self.Battle.Logs.Peek as LogCast;
        this.mCasterController = self.FindUnitController(peek.self);
        self.RemoveLog();
        this.mDirection = self.Battle.IsSkillDirection;
        if (!this.mDirection)
          return;
        self.mUpdateCameraPosition = true;
        self.InterpCameraTarget((Component) this.mCasterController);
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance);
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mDirection)
        {
          this.mCasterController.SetVisible(false);
          this.mCasterController.ChargeIcon.Open();
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if ((double) this.WaitTime > (double) this.CountTime)
          {
            this.CountTime += Time.get_deltaTime();
            if ((double) this.WaitTime > (double) this.CountTime)
              return;
            this.mCasterController.CastJump();
            this.mCasterController.ChargeIcon.Open();
          }
          if (!this.mCasterController.IsCastJumpStartComplete())
            return;
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
        self.RefreshJumpSpots();
        self.mUpdateCameraPosition = false;
      }
    }

    private class State_JumpFall : State<SceneBattle>
    {
      private bool mDirection = true;
      private List<SceneBattle.State_JumpFall.Caster> mCasterLists = new List<SceneBattle.State_JumpFall.Caster>();
      private const float WAIT_TIME = 0.5f;
      private const float DELAY_TIME = 0.1f;
      private LogFall mLog;
      private SceneBattle.State_JumpFall.eDirectionMode mDirectionMode;

      public override void Begin(SceneBattle self)
      {
        this.mLog = self.Battle.Logs.Peek as LogFall;
        if (this.mLog == null || this.mLog.mLists.Count == 0)
        {
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          for (int index = 0; index < this.mLog.mLists.Count; ++index)
          {
            LogFall.Param mList = this.mLog.mLists[index];
            TacticsUnitController unitController = self.FindUnitController(mList.mSelf);
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            {
              if (mList.mLanding != null)
              {
                Vector3 pos = self.CalcGridCenter(mList.mLanding);
                unitController.SetStartPos(pos);
                ((Component) unitController).get_transform().set_position(pos);
              }
              this.mCasterLists.Add(new SceneBattle.State_JumpFall.Caster(unitController));
            }
          }
          self.RemoveLog();
          this.mDirection = self.Battle.IsSkillDirection;
          if (!this.mDirection)
            return;
          Vector3 center = Vector3.get_zero();
          float distance = GameSettings.Instance.GameCamera_SkillCameraDistance;
          List<Vector3> vector3List = new List<Vector3>();
          for (int index = 0; index < this.mCasterLists.Count; ++index)
            vector3List.Add(this.mCasterLists[index].mController.CenterPosition);
          self.GetCameraTargetView(out center, out distance, vector3List.ToArray());
          self.mUpdateCameraPosition = true;
          self.InterpCameraTarget(center);
          self.InterpCameraDistance(distance);
          this.mDirectionMode = SceneBattle.State_JumpFall.eDirectionMode.FALL;
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mDirection)
        {
          switch (this.mDirectionMode)
          {
            case SceneBattle.State_JumpFall.eDirectionMode.FALL:
              bool flag = true;
              for (int index = 0; index < this.mCasterLists.Count; ++index)
              {
                float num = (float) (0.5 + 0.100000001490116 * (double) index);
                SceneBattle.State_JumpFall.Caster mCasterList = this.mCasterLists[index];
                TacticsUnitController mController = mCasterList.mController;
                if ((double) mCasterList.mPassedTime < (double) num)
                {
                  mCasterList.mPassedTime += Time.get_deltaTime();
                  if ((double) mCasterList.mPassedTime >= (double) num)
                  {
                    mController.CastJumpFall(this.mLog.mIsPlayDamageMotion);
                    mController.ChargeIcon.Close();
                  }
                  flag = false;
                }
                else if (!mController.IsFinishedCastJumpFall())
                  flag = false;
              }
              if (!flag)
                break;
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
              this.mDirectionMode = SceneBattle.State_JumpFall.eDirectionMode.CAMERA_WAIT;
              break;
            case SceneBattle.State_JumpFall.eDirectionMode.CAMERA_WAIT:
              this.mDirectionMode = SceneBattle.State_JumpFall.eDirectionMode.EXIT;
              break;
            case SceneBattle.State_JumpFall.eDirectionMode.EXIT:
              self.GotoState<SceneBattle.State_WaitForLog>();
              break;
          }
        }
        else
        {
          for (int index = 0; index < this.mCasterLists.Count; ++index)
          {
            TacticsUnitController mController = this.mCasterLists[index].mController;
            mController.SetVisible(true);
            mController.ChargeIcon.Close();
          }
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
        self.RefreshJumpSpots();
        self.mUpdateCameraPosition = false;
      }

      private enum eDirectionMode
      {
        FALL,
        CAMERA_WAIT,
        EXIT,
      }

      private class Caster
      {
        public TacticsUnitController mController;
        public float mPassedTime;

        public Caster(TacticsUnitController controller)
        {
          this.mController = controller;
          this.mPassedTime = 0.0f;
        }
      }
    }

    private enum eCsImage
    {
      U2_MAIN,
      U2_SUB,
      UE_MAIN,
      UE_SUB,
      MAX,
    }

    private class State_PrepareSkill : State<SceneBattle>
    {
      private bool mLoadSplash;
      private float mWaitCount;
      private bool mIsLoadTransformUnit;
      private bool mIsLoadBreakObjUnit;

      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        SkillData skill = peek.skill;
        if (skill == null)
        {
          DebugUtility.LogError("SkillParam が存在しません。");
        }
        else
        {
          this.mLoadSplash = !peek.is_append;
          self.LoadShieldEffects();
          Unit self1 = peek.self;
          TacticsUnitController unitController = self.FindUnitController(self1);
          this.mIsLoadTransformUnit = skill.IsTransformSkill();
          if (this.mIsLoadTransformUnit)
          {
            if (peek.targets != null && peek.targets.Count != 0)
              self.StartCoroutine(SceneBattle.State_PrepareSkill.loadTransformUnit(self, peek.targets[0].target, unitController, (Action) (() => this.mIsLoadTransformUnit = false)));
            else
              this.mIsLoadTransformUnit = false;
          }
          this.mIsLoadBreakObjUnit = skill.IsSetBreakObjSkill();
          if (this.mIsLoadBreakObjUnit)
          {
            if (peek.targets != null && peek.targets.Count != 0)
              self.StartCoroutine(SceneBattle.State_PrepareSkill.loadBreakObjUnit(self, peek.targets[0].target, (Action) (() => this.mIsLoadBreakObjUnit = false)));
            else
              this.mIsLoadBreakObjUnit = false;
          }
          if ((int) skill.SkillParam.absorb_damage_rate > 0)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mDrainMpEffectTemplate, (UnityEngine.Object) null) && skill.SkillParam.IsJewelAttack())
              unitController.SetDrainEffect(self.mDrainMpEffectTemplate);
            else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mDrainHpEffectTemplate, (UnityEngine.Object) null))
              unitController.SetDrainEffect(self.mDrainHpEffectTemplate);
          }
          if ((self.Battle.IsUnitAuto(self1) || self.Battle.EntryBattleMultiPlayTimeUp) && !skill.IsTransformSkill())
          {
            IntVector2 intVector2 = self.CalcCoord(unitController.CenterPosition);
            GridMap<bool> selectGridMap = self.Battle.CreateSelectGridMap(self1, intVector2.x, intVector2.y, skill);
            Color32 src1 = Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea);
            GridMap<Color32> grid1 = new GridMap<Color32>(selectGridMap.w, selectGridMap.h);
            for (int x = 0; x < selectGridMap.w; ++x)
            {
              for (int y = 0; y < selectGridMap.h; ++y)
              {
                if (selectGridMap.get(x, y))
                  grid1.set(x, y, src1);
              }
            }
            GridMap<bool> scopeGridMap = self.mBattle.CreateScopeGridMap(self1, intVector2.x, intVector2.y, peek.pos.x, peek.pos.y, skill);
            Color32 src2 = Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea2);
            GridMap<Color32> grid2 = new GridMap<Color32>(scopeGridMap.w, scopeGridMap.h);
            for (int x = 0; x < scopeGridMap.w; ++x)
            {
              for (int y = 0; y < scopeGridMap.h; ++y)
              {
                if (scopeGridMap.get(x, y))
                  grid2.set(x, y, src2);
              }
            }
            self.mTacticsSceneRoot.ShowGridLayer(1, grid1, true);
            self.mTacticsSceneRoot.ShowGridLayer(2, grid2, false);
            this.mWaitCount = 1f;
          }
          self.DeleteOnGimmickIcon();
        }
      }

      [DebuggerHidden]
      private IEnumerator PrepareSkill(Unit unit, SkillData skill)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CPrepareSkill\u003Ec__Iterator3E()
        {
          skill = skill,
          unit = unit,
          \u003C\u0024\u003Eskill = skill,
          \u003C\u0024\u003Eunit = unit,
          \u003C\u003Ef__this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator cutinVoice(Unit unit, SkillData skill)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CcutinVoice\u003Ec__Iterator3F()
        {
          unit = unit,
          skill = skill,
          \u003C\u0024\u003Eunit = unit,
          \u003C\u0024\u003Eskill = skill,
          \u003C\u003Ef__this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (this.mIsLoadTransformUnit || this.mIsLoadBreakObjUnit)
          return;
        this.mWaitCount -= GameSettings.Instance.AiUnit_SkillWait * Time.get_deltaTime();
        if (0.0 < (double) this.mWaitCount)
          return;
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        SkillData skill = peek.skill;
        self.mCollaboMainUnit = peek.self;
        self.mCollaboTargetTuc = (TacticsUnitController) null;
        self.mIsInstigatorSubUnit = false;
        if ((bool) peek.skill.IsCollabo)
        {
          Unit unitUseCollaboSkill = peek.self.GetUnitUseCollaboSkill(peek.skill, peek.self.x, peek.self.y);
          if (unitUseCollaboSkill != null)
          {
            self.mCollaboTargetTuc = self.FindUnitController(unitUseCollaboSkill);
            if (unitUseCollaboSkill.UnitParam.iname == skill.SkillParam.CollaboMainId)
              self.mIsInstigatorSubUnit = true;
          }
        }
        self.StartCoroutine(this.PrepareSkill(peek.self, skill));
        Unit self1 = peek.self;
        if (self.Battle.IsUnitAuto(self1) || self.Battle.EntryBattleMultiPlayTimeUp)
        {
          self.mTacticsSceneRoot.HideGridLayer(1);
          self.mTacticsSceneRoot.HideGridLayer(2);
        }
        if (skill.IsMapSkill())
        {
          self.GotoState<SceneBattle.State_Map_PrepareSkill>();
        }
        else
        {
          self.mBattleUI.OnBattleStart();
          self.mBattleUI.OnCommandSelect();
          self.GotoState_WaitSignal<SceneBattle.State_Battle_PrepareSkill>();
        }
      }

      [DebuggerHidden]
      public static IEnumerator loadTransformUnit(SceneBattle self, Unit unit, TacticsUnitController bef_tuc = null, Action callback = null)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CloadTransformUnit\u003Ec__Iterator40()
        {
          unit = unit,
          self = self,
          bef_tuc = bef_tuc,
          callback = callback,
          \u003C\u0024\u003Eunit = unit,
          \u003C\u0024\u003Eself = self,
          \u003C\u0024\u003Ebef_tuc = bef_tuc,
          \u003C\u0024\u003Ecallback = callback
        };
      }

      [DebuggerHidden]
      public static IEnumerator loadBreakObjUnit(SceneBattle self, Unit unit, Action callback = null)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CloadBreakObjUnit\u003Ec__Iterator41()
        {
          unit = unit,
          self = self,
          callback = callback,
          \u003C\u0024\u003Eunit = unit,
          \u003C\u0024\u003Eself = self,
          \u003C\u0024\u003Ecallback = callback
        };
      }
    }

    private class State_Battle_PrepareSkill : State<SceneBattle>
    {
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>();
      private SkillParam mSkill;
      private TacticsUnitController mInstigator;

      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        this.mInstigator = self.FindUnitController(peek.self);
        self.mUnitsInBattle.Clear();
        this.mSkill = peek.skill.SkillParam;
        bool flag = peek.skill.IsDamagedSkill();
        this.mInstigator.AutoUpdateRotation = false;
        this.mInstigator.LoadSkillSequence(peek.skill.SkillParam, false, true, (bool) peek.skill.IsCollabo, self.mIsInstigatorSubUnit);
        if (!string.IsNullOrEmpty(peek.skill.SkillParam.effect))
          this.mInstigator.LoadSkillEffect(peek.skill.SkillParam.effect, self.mIsInstigatorSubUnit);
        self.mUnitsInBattle.Add(this.mInstigator);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
        {
          self.mCollaboTargetTuc.AutoUpdateRotation = false;
          self.mCollaboTargetTuc.LoadSkillSequence(peek.skill.SkillParam, false, true, (bool) peek.skill.IsCollabo, !self.mIsInstigatorSubUnit);
          if (!string.IsNullOrEmpty(peek.skill.SkillParam.effect))
            self.mCollaboTargetTuc.LoadSkillEffect(peek.skill.SkillParam.effect, !self.mIsInstigatorSubUnit);
          self.mUnitsInBattle.Add(self.mCollaboTargetTuc);
        }
        if (0 < (int) this.mSkill.hp_cost)
        {
          this.mInstigator.SetHpCostSkill(peek.skill.GetHpCost(peek.self));
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
            self.mCollaboTargetTuc.SetHpCostSkill(peek.skill.GetHpCost(peek.self));
        }
        for (int index = 0; index < peek.targets.Count; ++index)
        {
          LogSkill.Target target = peek.targets[index];
          TacticsUnitController unitController = self.FindUnitController(target.target);
          this.mTargets.Add(unitController);
          if (this.mSkill.IsTransformSkill())
          {
            string nameTransformSkill = this.mInstigator.GetAnmNameTransformSkill();
            if (!string.IsNullOrEmpty(nameTransformSkill))
              unitController.LoadTransformAnimation(nameTransformSkill);
          }
          if (flag)
          {
            if (peek.targets[index].IsAvoid())
            {
              unitController.LoadDodgeAnimation();
            }
            else
            {
              if (peek.targets[index].IsCombo() && peek.targets[index].IsAvoidJustOne())
                unitController.LoadDodgeAnimation();
              unitController.LoadDamageAnimations();
            }
          }
          unitController.AutoUpdateRotation = false;
          self.mUnitsInBattle.Add(unitController);
        }
        self.SetPrioritizedUnits(self.mUnitsInBattle);
        self.DisableUserInterface();
        GameUtility.FadeOut(0.2f);
      }

      public override void Update(SceneBattle self)
      {
        if (GameUtility.IsScreenFading)
          return;
        for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
        {
          if (self.mUnitsInBattle[index].IsLoading)
            return;
        }
        if (self.mLoadingShieldEffects)
          return;
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        self.mUpdateCameraPosition = false;
        self.SetScreenMirroring(this.mInstigator.IsSkillMirror());
        self.ToggleBattleScene(true, peek.skill.SkillParam.SceneName);
        self.EnableWeatherEffect(false);
        Vector3 vector3_1 = self.mBattleSceneRoot.PlayerStart2.get_position();
        Vector3 vector3_2 = self.mBattleSceneRoot.PlayerStart2.get_position();
        Vector3 vector3_3 = self.mBattleSceneRoot.PlayerStart1.get_position();
        if (this.mInstigator.IsSkillParentPosZero)
        {
          Vector3 zero;
          vector3_3 = zero = Vector3.get_zero();
          vector3_2 = zero;
          vector3_1 = zero;
        }
        if (self.mIsInstigatorSubUnit)
        {
          Vector3 vector3_4 = vector3_1;
          vector3_1 = vector3_2;
          vector3_2 = vector3_4;
        }
        ((Component) this.mInstigator).get_transform().set_position(vector3_1);
        ((Component) this.mInstigator).get_transform().set_rotation(Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position())));
        ((Component) this.mInstigator).get_transform().SetParent(((Component) self.mBattleSceneRoot).get_transform(), false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
        {
          ((Component) self.mCollaboTargetTuc).get_transform().set_position(vector3_2);
          ((Component) self.mCollaboTargetTuc).get_transform().set_rotation(Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position())));
          ((Component) self.mCollaboTargetTuc).get_transform().SetParent(((Component) self.mBattleSceneRoot).get_transform(), false);
        }
        if (this.mTargets.Count == 1 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTargets[0], (UnityEngine.Object) this.mInstigator))
        {
          TacticsUnitController mTarget = this.mTargets[0];
          if (peek.skill.IsTransformSkill())
          {
            ((Component) mTarget).get_transform().set_position(vector3_1);
            ((Component) mTarget).get_transform().set_rotation(Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position())));
            ((Component) mTarget).get_transform().SetParent(((Component) self.mBattleSceneRoot).get_transform(), false);
          }
          else
          {
            ((Component) mTarget).get_transform().set_position(vector3_3);
            ((Component) mTarget).get_transform().set_rotation(Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart2.get_position(), self.mBattleSceneRoot.PlayerStart1.get_position())));
            ((Component) mTarget).get_transform().SetParent(((Component) self.mBattleSceneRoot).get_transform(), false);
          }
        }
        Transform transform = ((Component) Camera.get_main()).get_transform();
        transform.set_position(Vector3.op_Addition(GameSettings.Instance.Quest.BattleCamera.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position()));
        transform.set_rotation(GameSettings.Instance.Quest.BattleCamera.get_rotation());
        this.mInstigator.SetHitInfoSelf(peek.self_effect);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.SetHitInfoSelf(new LogSkill.Target());
        for (int index = 0; index < peek.targets.Count; ++index)
        {
          TacticsUnitController unitController = self.FindUnitController(peek.targets[index].target);
          LogSkill.Target target = peek.targets[index];
          unitController.ShouldDodgeHits = target.IsAvoid();
          unitController.ShouldPerfectDodge = target.IsPerfectAvoid();
          unitController.SetHitInfo(peek.targets[index]);
        }
        self.ToggleJumpSpots(false);
        if (peek.skill.CastType == ECastTypes.Jump)
          this.mInstigator.SetLandingGrid(peek.landing);
        if (peek.skill.TeleportType != eTeleportType.None)
          this.mInstigator.SetTeleportGrid(peek.TeleportGrid);
        this.mInstigator.StartSkill(self.CalcGridCenter(peek.pos.x, peek.pos.y), Camera.get_main(), this.mTargets.ToArray(), (Vector3[]) null, this.mSkill);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.StartSkill(self.CalcGridCenter(self.mCollaboTargetTuc.Unit.x, self.mCollaboTargetTuc.Unit.y), Camera.get_main(), this.mTargets.ToArray(), (Vector3[]) null, this.mSkill);
        GameUtility.FadeIn(0.2f);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplash, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplash))
            self.mSkillSplash.Close();
          self.mSkillSplash = (SkillSplash) null;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplashCollabo, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplashCollabo))
            self.mSkillSplashCollabo.Close();
          self.mSkillSplashCollabo = (SkillSplashCollabo) null;
        }
        self.GotoState<SceneBattle.State_Battle_PlaySkill>();
      }
    }

    private class State_Battle_PlaySkill : State<SceneBattle>
    {
      public override void Update(SceneBattle self)
      {
        for (int index = 0; index < self.mUnitsInBattle.Count; ++index)
        {
          if (!self.mUnitsInBattle[index].isIdle)
            return;
        }
        GameUtility.FadeOut(0.25f);
        self.GotoState<SceneBattle.State_Battle_EndSkill>();
      }
    }

    private class State_Battle_EndSkill : State<SceneBattle>
    {
      private bool mIsBusy;
      private SkillData mSkillData;
      private LogSkill mLogSkill;

      public override void Begin(SceneBattle self)
      {
        this.mIsBusy = true;
        this.mLogSkill = self.mBattle.Logs.Peek as LogSkill;
        self.StartCoroutine(this.AsyncUpdate());
      }

      private void UpdateHPGauges()
      {
        LogSkill peek = this.self.mBattle.Logs.Peek as LogSkill;
        for (int index = 0; index < peek.targets.Count; ++index)
        {
          int delta = -peek.targets[index].GetTotalHpDamage() + peek.targets[index].GetTotalHpHeal();
          if (delta != 0)
          {
            TacticsUnitController unitController = this.self.FindUnitController(peek.targets[index].target);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              unitController.UpdateHPRelative(delta, 0.5f, false);
          }
        }
      }

      [DebuggerHidden]
      private IEnumerator AsyncUpdate()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Battle_EndSkill.\u003CAsyncUpdate\u003Ec__Iterator42()
        {
          \u003C\u003Ef__this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (this.mIsBusy)
          return;
        if (this.mSkillData != null && this.mSkillData.IsTransformSkill())
        {
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
          self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
        }
        else
        {
          self.mIgnoreShieldEffect.Clear();
          if (this.mLogSkill != null)
          {
            TacticsUnitController unitController1 = self.FindUnitController(this.mLogSkill.self);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
              self.mIgnoreShieldEffect.Add(unitController1);
            using (List<LogSkill.Target>.Enumerator enumerator = this.mLogSkill.targets.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                LogSkill.Target current = enumerator.Current;
                if (!current.isProcShield)
                {
                  TacticsUnitController unitController2 = self.FindUnitController(current.target);
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
                    self.mIgnoreShieldEffect.Add(unitController2);
                }
              }
            }
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
            {
              TacticsUnitController unitController2 = self.FindUnitController(this.mLogSkill.self);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null) && this.mLogSkill.targets != null && this.mLogSkill.targets.Count != 0)
              {
                List<TacticsUnitController> TargetLists = new List<TacticsUnitController>();
                using (List<LogSkill.Target>.Enumerator enumerator = this.mLogSkill.targets.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    LogSkill.Target current = enumerator.Current;
                    TacticsUnitController unitController3 = self.FindUnitController(current.target);
                    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController3, (UnityEngine.Object) null))
                      TargetLists.Add(unitController3);
                  }
                }
                if (TargetLists.Count != 0)
                {
                  self.mEventSequence = self.mEventScript.OnUseSkill(EventScript.SkillTiming.AFTER, unitController2, this.mLogSkill.skill, TargetLists, self.mIsFirstPlay);
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
                  {
                    self.GotoState_WaitSignal<SceneBattle.State_WaitEvent<SceneBattle.State_SpawnShieldEffects>>();
                    return;
                  }
                }
              }
            }
          }
          self.GotoState_WaitSignal<SceneBattle.State_SpawnShieldEffects>();
        }
      }
    }

    private class State_Map_PrepareSkill : State<SceneBattle>
    {
      private TacticsUnitController controller;
      private LogSkill log;

      public override void Begin(SceneBattle self)
      {
        this.log = self.mBattle.Logs.Peek as LogSkill;
        LogSkill log = this.log;
        SkillParam skillParam = log.skill.SkillParam;
        this.controller = self.FindUnitController(log.self);
        self.mUnitsInBattle.Clear();
        self.mUnitsInBattle.Add(this.controller);
        this.controller.AutoUpdateRotation = false;
        this.controller.LoadSkillSequence(skillParam, false, false, (bool) log.skill.IsCollabo, self.mIsInstigatorSubUnit);
        if (!string.IsNullOrEmpty(skillParam.effect))
          this.controller.LoadSkillEffect(skillParam.effect, false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
        {
          self.mCollaboTargetTuc.LoadSkillSequence(skillParam, false, false, (bool) log.skill.IsCollabo, !self.mIsInstigatorSubUnit);
          if (!string.IsNullOrEmpty(skillParam.effect))
            self.mCollaboTargetTuc.LoadSkillEffect(skillParam.effect, !self.mIsInstigatorSubUnit);
          self.mUnitsInBattle.Add(self.mCollaboTargetTuc);
        }
        if (0 < (int) skillParam.hp_cost)
        {
          this.controller.SetHpCostSkill(log.skill.GetHpCost(log.self));
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
            self.mCollaboTargetTuc.SetHpCostSkill(log.skill.GetHpCost(log.self));
        }
        for (int index = 0; index < log.targets.Count; ++index)
        {
          TacticsUnitController unitController = self.FindUnitController(log.targets[index].target);
          if (skillParam.IsTransformSkill())
          {
            string nameTransformSkill = this.controller.GetAnmNameTransformSkill();
            if (!string.IsNullOrEmpty(nameTransformSkill))
              unitController.LoadTransformAnimation(nameTransformSkill);
          }
          if (log.targets[index].IsAvoid())
          {
            unitController.LoadDodgeAnimation();
          }
          else
          {
            if (log.targets[index].IsCombo() && log.targets[index].IsAvoidJustOne())
              unitController.LoadDodgeAnimation();
            unitController.LoadDamageAnimations();
          }
          self.mUnitsInBattle.Add(unitController);
        }
      }

      public override void Update(SceneBattle self)
      {
        LogSkill log = this.log;
        if (!string.IsNullOrEmpty(log.skill.SkillParam.effect) && !this.controller.IsFinishedLoadSkillEffect())
          return;
        if (self.Battle.IsUnitAuto(log.self) || self.Battle.IsMultiPlay && self.Battle.CurrentUnit.OwnerPlayerIndex != self.Battle.MyPlayerIndex || log.skill.IsCastSkill())
        {
          IntVector2 intVector2 = self.CalcCoord(this.controller.CenterPosition);
          GridMap<bool> scopeGridMap = self.mBattle.CreateScopeGridMap(this.controller.Unit, intVector2.x, intVector2.y, log.pos.x, log.pos.y, log.skill);
          if (scopeGridMap != null)
            scopeGridMap.set(log.pos.x, log.pos.y, true);
          self.mSkillDirectionByKouka = self.GetSkillDirectionByTargetArea(this.controller.Unit, intVector2.x, intVector2.y, scopeGridMap);
        }
        this.controller.SkillTurn(log, self.mSkillDirectionByKouka);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.SkillTurn(log, self.mSkillDirectionByKouka);
        self.SetPrioritizedUnits(self.mUnitsInBattle);
        bool flag = this.log.IsRenkei();
        self.mAllowCameraRotation = false;
        self.mAllowCameraTranslation = false;
        if (flag)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.controller, (UnityEngine.Object) null))
            this.controller.LoadGenkidamaAnimation(true);
          self.GotoState<SceneBattle.State_Map_LoadSkill_Renkei1>();
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.controller, (UnityEngine.Object) null))
          {
            self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).get_transform());
            self.InterpCameraTarget((Component) this.controller);
            self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance);
          }
          self.GotoState<SceneBattle.State_Map_LoadSkill>();
        }
      }
    }

    private class State_Map_LoadSkill : State<SceneBattle>
    {
      private float mWaitTime;

      public override void Begin(SceneBattle self)
      {
        this.mWaitTime = 0.75f;
      }

      public override void Update(SceneBattle self)
      {
        if ((double) this.mWaitTime > 0.0)
          this.mWaitTime -= Time.get_deltaTime();
        if (self.mDesiredCameraTargetSet || ObjectAnimator.Get((Component) Camera.get_main()).isMoving)
          return;
        for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
        {
          if (self.mUnitsInBattle[index].IsLoading || !self.mUnitsInBattle[index].isIdle)
            return;
        }
        if (self.mLoadingShieldEffects || (double) this.mWaitTime > 0.0)
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplash, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplash))
            self.mSkillSplash.Close();
          if (self.mWaitSkillSplashClose)
            return;
          self.mSkillSplash = (SkillSplash) null;
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplashCollabo, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplashCollabo))
            self.mSkillSplashCollabo.Close();
          if (self.mWaitSkillSplashClose)
            return;
          self.mSkillSplashCollabo = (SkillSplashCollabo) null;
        }
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        TacticsUnitController unitController = self.FindUnitController(peek.self);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && unitController.HasPreSkillAnimation)
        {
          self.mBattleUI.OnCommandSelect();
          self.GotoState<SceneBattle.State_Map_PlayPreSkillAnim>();
        }
        else
          self.GotoState<SceneBattle.State_Map_PlayPreDodge>();
      }
    }

    private class State_Map_PlayPreSkillAnim : State<SceneBattle>
    {
      private TacticsUnitController mInstigator;
      private bool mFadeIn;

      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        this.mInstigator = self.FindUnitController(peek.self);
        self.SetScreenMirroring(this.mInstigator.IsSkillMirror());
        self.ToggleBattleScene(true, peek.skill.SkillParam.SceneName);
        self.EnableWeatherEffect(false);
        Vector3 vector3_1 = self.mBattleSceneRoot.PlayerStart2.get_position();
        Vector3 vector3_2 = self.mBattleSceneRoot.PlayerStart2.get_position();
        if (this.mInstigator.IsPreSkillParentPosZero)
          vector3_1 = vector3_2 = Vector3.get_zero();
        if (self.mIsInstigatorSubUnit)
        {
          Vector3 vector3_3 = vector3_1;
          vector3_1 = vector3_2;
          vector3_2 = vector3_3;
        }
        Transform transform1 = ((Component) this.mInstigator).get_transform();
        transform1.set_position(vector3_1);
        transform1.set_rotation(Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position())));
        transform1.SetParent(((Component) self.mBattleSceneRoot).get_transform(), false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
        {
          ((Component) self.mCollaboTargetTuc).get_transform().set_position(vector3_2);
          ((Component) self.mCollaboTargetTuc).get_transform().set_rotation(Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position())));
          ((Component) self.mCollaboTargetTuc).get_transform().SetParent(((Component) self.mBattleSceneRoot).get_transform(), false);
        }
        Transform transform2 = ((Component) Camera.get_main()).get_transform();
        transform2.set_position(Vector3.op_Addition(GameSettings.Instance.Quest.BattleCamera.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position()));
        transform2.set_rotation(GameSettings.Instance.Quest.BattleCamera.get_rotation());
        self.mUpdateCameraPosition = false;
        self.UpdateCameraControl(true);
        self.DisableUserInterface();
        if (self.Battle.IsMultiVersus)
        {
          self.HideAllHPGauges();
          self.CloseBattleUI();
        }
        GameUtility.FadeOut(0.2f);
        self.StartCoroutine(this.AsyncWork());
      }

      [DebuggerHidden]
      private IEnumerator AsyncWork()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Map_PlayPreSkillAnim.\u003CAsyncWork\u003Ec__Iterator43()
        {
          \u003C\u003Ef__this = this
        };
      }
    }

    private class State_Map_LoadSkill_Renkei1 : State<SceneBattle>
    {
      private int mRenkeiUnitIndex;
      private float mInterval;

      public override void Begin(SceneBattle self)
      {
        FadeController.Instance.BeginSceneFade(new Color(0.25f, 0.25f, 0.25f), 0.5f, self.mUnitsInBattle.ToArray(), (TacticsUnitController[]) null);
        Unit currentUnit = self.mBattle.CurrentUnit;
        if (currentUnit == null)
          return;
        currentUnit.PlayBattleVoice("battle_0015");
      }

      public override void Update(SceneBattle self)
      {
        if ((double) this.mInterval > 0.0)
          this.mInterval = -Time.get_deltaTime();
        if (self.mDesiredCameraTargetSet || ObjectAnimator.Get((Component) Camera.get_main()).isMoving)
          return;
        if (this.mRenkeiUnitIndex < self.mBattle.HelperUnits.Count)
        {
          TacticsUnitController unitController = self.FindUnitController(self.mBattle.HelperUnits[this.mRenkeiUnitIndex]);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).get_transform());
            self.InterpCameraTarget((Component) unitController);
            self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance);
            GameUtility.SpawnParticle(self.mRenkeiAssistEffect, ((Component) unitController).get_transform(), ((Component) self).get_gameObject());
            this.mInterval = 0.5f;
          }
          ++this.mRenkeiUnitIndex;
        }
        else
        {
          for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
          {
            if (self.mUnitsInBattle[index].IsLoading)
              return;
          }
          self.GotoState<SceneBattle.State_Map_LoadSkill_Renkei2>();
        }
      }
    }

    private class State_Map_LoadSkill_Renkei2 : State<SceneBattle>
    {
      private float mDelay;

      public override void Begin(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).get_transform());
          self.InterpCameraTarget((Component) unitController);
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance);
          unitController.PlayGenkidama();
          GameUtility.SpawnParticle(self.mRenkeiChargeEffect, ((Component) unitController).get_transform(), ((Component) self).get_gameObject());
          unitController.FadeBlendColor(SceneBattle.RenkeiChargeColor, 1f);
          unitController.SetLastHitEffect(self.mRenkeiHitEffect);
        }
        this.mDelay = 1f;
      }

      public override void Update(SceneBattle self)
      {
        if ((double) this.mDelay > 0.0)
        {
          this.mDelay -= Time.get_deltaTime();
        }
        else
        {
          if (self.mDesiredCameraTargetSet || ObjectAnimator.Get((Component) Camera.get_main()).isMoving)
            return;
          for (int index = self.mUnitsInBattle.Count - 1; index >= 0; --index)
          {
            if (self.mUnitsInBattle[index].IsLoading || !self.mUnitsInBattle[index].isIdle)
              return;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplash, (UnityEngine.Object) null))
          {
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplash))
              self.mSkillSplash.Close();
            self.mSkillSplash = (SkillSplash) null;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mSkillSplashCollabo, (UnityEngine.Object) null))
          {
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mSkillSplashCollabo))
              self.mSkillSplashCollabo.Close();
            self.mSkillSplashCollabo = (SkillSplashCollabo) null;
          }
          self.GotoState<SceneBattle.State_Map_PlayPreDodge>();
        }
      }
    }

    private class State_Map_PlayPreDodge : State<SceneBattle>
    {
      private List<TacticsUnitController> mPollControllers = new List<TacticsUnitController>();
      private float mDelay = 0.1f;

      public override void Begin(SceneBattle self)
      {
        BattleLog peek = self.mBattle.Logs.Peek;
        if (peek is LogSkill)
        {
          LogSkill logSkill = peek as LogSkill;
          TacticsUnitController unitController1 = self.FindUnitController(logSkill.self);
          for (int index = 0; index < logSkill.targets.Count; ++index)
          {
            if (logSkill.targets[index].IsAvoid())
            {
              TacticsUnitController unitController2 = self.FindUnitController(logSkill.targets[index].target);
              unitController2.AutoUpdateRotation = false;
              unitController2.LookAt(((Component) unitController1).get_transform().get_position());
              this.mPollControllers.Add(unitController2);
            }
          }
        }
        if (this.mPollControllers.Count > 0)
          return;
        self.GotoState<SceneBattle.State_Map_PlaySkill>();
      }

      public override void Update(SceneBattle self)
      {
        for (int index = this.mPollControllers.Count - 1; index >= 0; --index)
        {
          if (!this.mPollControllers[index].isIdle)
            return;
        }
        if ((double) this.mDelay > 0.0)
          this.mDelay -= Time.get_deltaTime();
        else
          self.GotoState<SceneBattle.State_Map_PlaySkill>();
      }
    }

    private class State_Map_PlaySkill : State<SceneBattle>
    {
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>();
      private TacticsUnitController mInstigator;
      private float mEndWait;
      private LogSkill mActionInfo;
      private IntVector2 mCameraStart;

      public override void Begin(SceneBattle self)
      {
        this.mActionInfo = self.mBattle.Logs.Peek as LogSkill;
        this.mInstigator = self.FindUnitController(this.mActionInfo.self);
        if (this.mActionInfo.skill.CastType != ECastTypes.Jump)
          self.mUpdateCameraPosition = false;
        if (this.mActionInfo.skill.EffectType == SkillEffectTypes.Changing && this.mActionInfo.targets.Count > 0 && this.mActionInfo.targets[0] != null)
        {
          this.mCameraStart.x = this.mActionInfo.targets[0].target.x;
          this.mCameraStart.y = this.mActionInfo.targets[0].target.y;
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance * 1.5f);
        }
        else
        {
          switch (this.mInstigator.GetSkillCameraType())
          {
            case SkillSequence.MapCameraTypes.FirstTargetCenter:
              this.mCameraStart.x = this.mActionInfo.self.x;
              this.mCameraStart.y = this.mActionInfo.self.y;
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance * 1.5f);
              break;
            case SkillSequence.MapCameraTypes.FarDistance:
              self.mUpdateCameraPosition = true;
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
              break;
            case SkillSequence.MapCameraTypes.MoreFarDistance:
              self.mUpdateCameraPosition = true;
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_MoreFarDistance);
              break;
            case SkillSequence.MapCameraTypes.AllTargetsCenter:
            case SkillSequence.MapCameraTypes.AllTargetsAndSelfCenter:
              Vector3 center = Vector3.get_zero();
              float distance = GameSettings.Instance.GameCamera_SkillCameraDistance;
              List<Vector3> vector3List1 = new List<Vector3>();
              switch (this.mInstigator.GetSkillCameraType())
              {
                case SkillSequence.MapCameraTypes.AllTargetsCenter:
                  using (List<LogSkill.Target>.Enumerator enumerator = this.mActionInfo.targets.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      LogSkill.Target current = enumerator.Current;
                      TacticsUnitController unitController = self.FindUnitController(current.target);
                      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController))
                        vector3List1.Add(unitController.CenterPosition);
                    }
                    break;
                  }
                case SkillSequence.MapCameraTypes.AllTargetsAndSelfCenter:
                  vector3List1.Add(this.mInstigator.CenterPosition);
                  goto case SkillSequence.MapCameraTypes.AllTargetsCenter;
              }
              self.GetCameraTargetView(out center, out distance, vector3List1.ToArray());
              self.InterpCameraTarget(center);
              self.InterpCameraDistance(distance);
              break;
          }
        }
        SkillData skill = this.mActionInfo.skill;
        SkillParam skillParam = skill.SkillParam;
        List<Vector3> vector3List2 = (List<Vector3>) null;
        if (skillParam.IsAreaSkill())
        {
          vector3List2 = new List<Vector3>();
          GridMap<bool> scopeGridMap = self.mBattle.CreateScopeGridMap(this.mActionInfo.self, this.mActionInfo.self.x, this.mActionInfo.self.y, this.mActionInfo.pos.x, this.mActionInfo.pos.y, skill);
          for (int y = 0; y < scopeGridMap.h; ++y)
          {
            for (int x = 0; x < scopeGridMap.w; ++x)
            {
              if (scopeGridMap.get(x, y))
                vector3List2.Add(self.CalcGridCenter(x, y));
            }
          }
        }
        this.mInstigator.SetArrowTrajectoryHeight((float) this.mActionInfo.height / 100f);
        SkillEffect loadedSkillEffect = this.mInstigator.LoadedSkillEffect;
        bool flag = false;
        GameSettings instance = GameSettings.Instance;
        if (this.mActionInfo.skill.CastType == ECastTypes.Jump)
        {
          self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).get_transform());
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance);
        }
        this.mInstigator.SetHitInfoSelf(this.mActionInfo.self_effect);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.SetHitInfoSelf(new LogSkill.Target());
        TacticsUnitController[] targets = new TacticsUnitController[this.mActionInfo.targets.Count];
        for (int index = 0; index < this.mActionInfo.targets.Count; ++index)
        {
          LogSkill.Target target = this.mActionInfo.targets[index];
          TacticsUnitController unitController = self.FindUnitController(this.mActionInfo.targets[index].target);
          targets[index] = unitController;
          unitController.ShouldDodgeHits = target.IsAvoid();
          unitController.ShouldPerfectDodge = target.IsPerfectAvoid();
          unitController.ShouldDefendHits = target.IsDefend();
          unitController.SetHitInfo(target);
          if (!unitController.ShouldDodgeHits)
          {
            unitController.ShowCriticalEffectOnHit = target.IsCritical();
            unitController.ShowBackstabEffectOnHit = (target.hitType & LogSkill.EHitTypes.BackAttack) != (LogSkill.EHitTypes) 0 && skill.IsNormalAttack();
            unitController.ShouldDefendHits = target.IsDefend();
            unitController.ShowElementEffectOnHit = !target.IsNormalEffectElement() ? (!target.IsWeakEffectElement() ? TacticsUnitController.EElementEffectTypes.NotEffective : TacticsUnitController.EElementEffectTypes.Effective) : TacticsUnitController.EElementEffectTypes.Normal;
          }
          else
          {
            unitController.ShowCriticalEffectOnHit = false;
            unitController.ShowBackstabEffectOnHit = false;
            unitController.ShouldDefendHits = false;
            unitController.ShowElementEffectOnHit = TacticsUnitController.EElementEffectTypes.Normal;
          }
          if (target.gems > 0)
          {
            if ((target.hitType & LogSkill.EHitTypes.SideAttack) != (LogSkill.EHitTypes) 0)
            {
              unitController.DrainGemsOnHit = instance.Gem_DrainCount_SideHit;
              unitController.GemDrainEffects = self.mGemDrainEffect_Side;
            }
            else if ((target.hitType & LogSkill.EHitTypes.BackAttack) != (LogSkill.EHitTypes) 0)
            {
              unitController.DrainGemsOnHit = instance.Gem_DrainCount_BackHit;
              unitController.GemDrainEffects = self.mGemDrainEffect_Back;
            }
            else
            {
              unitController.DrainGemsOnHit = instance.Gem_DrainCount_FrontHit;
              unitController.GemDrainEffects = self.mGemDrainEffect_Front;
            }
            unitController.GemDrainHitEffect = self.mGemDrainHitEffect;
          }
          else
            unitController.DrainGemsOnHit = 0;
          unitController.KnockBackGrid = target.KnockBackGrid;
          this.mTargets.Add(unitController);
        }
        foreach (SkillEffect.SFX explosionSound in loadedSkillEffect.ExplosionSounds)
          explosionSound.IsCritical = false;
        if (this.mActionInfo.skill.IsNormalAttack() && loadedSkillEffect.ExplosionSounds.Length > 0)
          loadedSkillEffect.ExplosionSounds[0].IsCritical = flag;
        if (this.mActionInfo.skill.CastType == ECastTypes.Jump)
          this.mInstigator.SetLandingGrid(this.mActionInfo.landing);
        if (this.mActionInfo.skill.TeleportType != eTeleportType.None)
          this.mInstigator.SetTeleportGrid(this.mActionInfo.TeleportGrid);
        this.mInstigator.StartSkill(self.CalcGridCenter(this.mActionInfo.pos.x, this.mActionInfo.pos.y), Camera.get_main(), targets, vector3List2 == null ? (Vector3[]) null : vector3List2.ToArray(), skill.SkillParam);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
          self.mCollaboTargetTuc.StartSkill(self.CalcGridCenter(this.mActionInfo.pos.x, this.mActionInfo.pos.y), Camera.get_main(), targets, vector3List2 == null ? (Vector3[]) null : vector3List2.ToArray(), skill.SkillParam);
        if (skill.SkillType == ESkillType.Skill || skill.SkillType == ESkillType.Reaction)
          self.ShowSkillNamePlate(this.mActionInfo.self, skill, string.Empty, 1f);
        this.mEndWait = GameSettings.Instance.Quest.BattleTurnEndWait;
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mInstigator.isIdle)
        {
          if (this.mActionInfo.skill.CastType == ECastTypes.Jump)
          {
            Vector3 position = self.CalcGridCenter(this.mActionInfo.pos.x, this.mActionInfo.pos.y);
            self.InterpCameraTarget(position);
          }
          else
          {
            if (this.mActionInfo.skill.EffectType != SkillEffectTypes.Changing && this.mInstigator.GetSkillCameraType() != SkillSequence.MapCameraTypes.FirstTargetCenter)
              return;
            IntVector2 intVector2 = new IntVector2((this.mActionInfo.pos.x - this.mCameraStart.x) / 2, (this.mActionInfo.pos.y - this.mCameraStart.y) / 2);
            Vector3 position = self.CalcGridCenter(this.mCameraStart.x + intVector2.x, this.mCameraStart.y + intVector2.y);
            self.InterpCameraTarget(position);
          }
        }
        else
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null) && !self.mCollaboTargetTuc.isIdle)
            return;
          using (List<TacticsUnitController>.Enumerator enumerator = this.mTargets.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TacticsUnitController current = enumerator.Current;
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) current) && current.IsBusy)
                return;
            }
          }
          if ((double) this.mEndWait > 0.0)
          {
            this.mEndWait -= Time.get_deltaTime();
          }
          else
          {
            if (self.IsHPGaugeChanging)
              return;
            self.HideAllHPGauges();
            for (int index = 0; index < this.mTargets.Count; ++index)
            {
              this.mTargets[index].ShouldDodgeHits = false;
              this.mTargets[index].ShouldPerfectDodge = false;
              this.mTargets[index].ShouldDefendHits = false;
              this.mTargets[index].ShowCriticalEffectOnHit = false;
              this.mTargets[index].ShowBackstabEffectOnHit = false;
              this.mTargets[index].DrainGemsOnHit = 0;
              this.mTargets[index].ShowElementEffectOnHit = TacticsUnitController.EElementEffectTypes.Normal;
            }
            if (this.mActionInfo.IsRenkei())
              self.ToggleRenkeiAura(false);
            self.RefreshJumpSpots();
            TrickData.AddMarker();
            this.mInstigator.AnimateVessel(0.0f, 0.5f);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mCollaboTargetTuc, (UnityEngine.Object) null))
              self.mCollaboTargetTuc.AnimateVessel(0.0f, 0.5f);
            FadeController.Instance.BeginSceneFade(Color.get_white(), 0.5f, self.mUnitsInBattle.ToArray(), (TacticsUnitController[]) null);
            self.RefreshUnitStatus(this.mInstigator.Unit);
            SkillData skill = this.mActionInfo.skill;
            if ((bool) skill.IsCollabo)
            {
              Unit unitUseCollaboSkill = self.Battle.GetUnitUseCollaboSkill(this.mInstigator.Unit, skill);
              if (unitUseCollaboSkill != null)
                self.RefreshUnitStatus(unitUseCollaboSkill);
            }
            self.mSkillNamePlate.Close();
            self.ResetCameraTarget();
            if (this.mActionInfo.skill.IsTransformSkill())
            {
              self.HideUnitMarkers(this.mInstigator.Unit);
              self.mTacticsUnits.Remove(this.mInstigator);
              UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mInstigator).get_gameObject());
              this.mInstigator = (TacticsUnitController) null;
              if (this.mTargets.Count != 0)
                this.mTargets[0].SetVisible(true);
              self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
              self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
            }
            else
            {
              if (this.mActionInfo.skill.IsSetBreakObjSkill() && this.mTargets.Count != 0)
              {
                this.mTargets[0].SetVisible(true);
                self.OnGimmickUpdate();
              }
              self.mIgnoreShieldEffect.Clear();
              if (this.mActionInfo != null)
              {
                self.mIgnoreShieldEffect.Add(this.mInstigator);
                using (List<LogSkill.Target>.Enumerator enumerator = this.mActionInfo.targets.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    LogSkill.Target current = enumerator.Current;
                    if (!current.isProcShield)
                    {
                      TacticsUnitController unitController = self.FindUnitController(current.target);
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
                        self.mIgnoreShieldEffect.Add(unitController);
                    }
                  }
                }
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
                {
                  TacticsUnitController unitController1 = self.FindUnitController(this.mActionInfo.self);
                  if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null) && this.mActionInfo.targets != null && this.mActionInfo.targets.Count != 0)
                  {
                    List<TacticsUnitController> TargetLists = new List<TacticsUnitController>();
                    using (List<LogSkill.Target>.Enumerator enumerator = this.mActionInfo.targets.GetEnumerator())
                    {
                      while (enumerator.MoveNext())
                      {
                        LogSkill.Target current = enumerator.Current;
                        TacticsUnitController unitController2 = self.FindUnitController(current.target);
                        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
                          TargetLists.Add(unitController2);
                      }
                    }
                    if (TargetLists.Count != 0)
                    {
                      self.mEventSequence = self.mEventScript.OnUseSkill(EventScript.SkillTiming.AFTER, unitController1, this.mActionInfo.skill, TargetLists, self.mIsFirstPlay);
                      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
                      {
                        self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_SpawnShieldEffects>>();
                        return;
                      }
                    }
                  }
                }
              }
              self.GotoState<SceneBattle.State_SpawnShieldEffects>();
            }
          }
        }
      }
    }

    private class FindShield
    {
      public TacticsUnitController mTuc;
      public TacticsUnitController.ShieldState mShield;

      public FindShield(TacticsUnitController tuc, TacticsUnitController.ShieldState shield)
      {
        this.mTuc = tuc;
        this.mShield = shield;
      }
    }

    private class State_SpawnShieldEffects : State<SceneBattle>
    {
      private TacticsUnitController mUnit;
      private TacticsUnitController.ShieldState mShield;
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        using (List<TacticsUnitController>.Enumerator enumerator = self.mTacticsUnits.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TacticsUnitController current = enumerator.Current;
            if (!self.mIgnoreShieldEffect.Contains(current))
              current.UpdateShields(false);
          }
        }
        if (!self.FindChangedShield(out this.mUnit, out this.mShield))
        {
          self.GotoState<SceneBattle.State_TriggerHPEvents>();
        }
        else
        {
          this.mIsFinished = false;
          self.StartCoroutine(this.SpawnEffectsAsync());
        }
      }

      [DebuggerHidden]
      private IEnumerator SpawnEffectsAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_SpawnShieldEffects.\u003CSpawnEffectsAsync\u003Ec__Iterator44()
        {
          \u003C\u003Ef__this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        self.mIgnoreShieldEffect.Clear();
        self.GotoState<SceneBattle.State_TriggerHPEvents>();
      }
    }

    private class EventRecvSkillUnit
    {
      public bool mValid;
      public SceneBattle.EventRecvSkillUnit.eType mType;
      public TacticsUnitController mController;
      public EElement mElem;
      public EUnitCondition mCond;

      public EventRecvSkillUnit()
      {
      }

      public EventRecvSkillUnit(TacticsUnitController tuc, EElement elem)
      {
        this.mType = SceneBattle.EventRecvSkillUnit.eType.ELEM;
        this.mController = tuc;
        this.mElem = elem;
        this.mValid = true;
      }

      public EventRecvSkillUnit(TacticsUnitController tuc, EUnitCondition cond)
      {
        this.mType = SceneBattle.EventRecvSkillUnit.eType.COND;
        this.mController = tuc;
        this.mCond = cond;
        this.mValid = true;
      }

      public enum eType
      {
        UNKNOWN,
        ELEM,
        COND,
      }
    }

    private class State_TriggerHPEvents : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          {
            self.mEventSequence = self.mEventScript.OnUnitHPChange(self.mTacticsUnits[index]);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
            {
              self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_TriggerHPEvents>>();
              return;
            }
          }
          using (List<TacticsUnitController>.Enumerator enumerator = self.mTacticsUnits.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TacticsUnitController current = enumerator.Current;
              self.mEventSequence = self.mEventScript.OnUnitRestHP(current, self.mIsFirstPlay);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
              {
                self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_TriggerHPEvents>>();
                return;
              }
            }
          }
          using (List<SceneBattle.EventRecvSkillUnit>.Enumerator enumerator = self.mEventRecvSkillUnitLists.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SceneBattle.EventRecvSkillUnit current = enumerator.Current;
              if (current.mValid)
              {
                self.mEventSequence = (EventScript.Sequence) null;
                switch (current.mType)
                {
                  case SceneBattle.EventRecvSkillUnit.eType.ELEM:
                    self.mEventSequence = self.mEventScript.OnRecvSkillElem(current.mController, current.mElem, self.mIsFirstPlay);
                    current.mValid = false;
                    break;
                  case SceneBattle.EventRecvSkillUnit.eType.COND:
                    self.mEventSequence = self.mEventScript.OnRecvSkillCond(current.mController, current.mCond, self.mIsFirstPlay);
                    current.mValid = false;
                    break;
                }
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
                {
                  self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_TriggerHPEvents>>();
                  return;
                }
              }
            }
          }
        }
        self.mEventRecvSkillUnitLists.Clear();
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_WaitGC<NextState> : State<SceneBattle> where NextState : State<SceneBattle>, new()
    {
      private AsyncOperation mAsyncOp;

      public override void Begin(SceneBattle self)
      {
      }

      public override void Update(SceneBattle self)
      {
        self.GotoState<NextState>();
      }
    }

    private class State_Weather : State<SceneBattle>
    {
      private LogWeather mLog;
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        this.mLog = self.Battle.Logs.Peek as LogWeather;
        if (this.mLog == null || this.mLog.WeatherData == null)
          return;
        this.mIsFinished = false;
        self.StartCoroutine(this.AsyncWeather());
      }

      [DebuggerHidden]
      private IEnumerator AsyncWeather()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Weather.\u003CAsyncWeather\u003Ec__Iterator45()
        {
          \u003C\u003Ef__this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        this.mLog = (LogWeather) null;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_SpawnUnit : State<SceneBattle>
    {
      private LoadRequest mLoadRequest;
      private SceneBattle mScene;
      private bool mIsFinished;
      private Unit mSpawnUnit;

      public override void Begin(SceneBattle self)
      {
        this.mScene = self;
        this.mIsFinished = true;
        LogUnitEntry peek = self.mBattle.Logs.Peek as LogUnitEntry;
        if (peek == null)
          return;
        this.mIsFinished = false;
        this.mSpawnUnit = peek.self;
        self.StartCoroutine(this.AsyncWork(peek.self, peek.kill_unit));
        self.RemoveLog();
      }

      [DebuggerHidden]
      private IEnumerator AsyncWork(Unit unit, Unit kill_unit)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_SpawnUnit.\u003CAsyncWork\u003Ec__Iterator39()
        {
          unit = unit,
          kill_unit = kill_unit,
          \u003C\u0024\u003Eunit = unit,
          \u003C\u0024\u003Ekill_unit = kill_unit,
          \u003C\u003Ef__this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        if (this.mSpawnUnit != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController = self.FindUnitController(this.mSpawnUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          {
            self.mEventSequence = self.mEventScript.OnUnitAppear(unitController, self.mIsFirstPlay);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
            {
              self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>>();
              return;
            }
          }
        }
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_UnitWithdraw : State<SceneBattle>
    {
      private bool mIsFinished;

      public override void Begin(SceneBattle self)
      {
        this.mIsFinished = true;
        LogUnitWithdraw peek = self.mBattle.Logs.Peek as LogUnitWithdraw;
        if (peek == null)
          return;
        this.mIsFinished = false;
        self.StartCoroutine(this.AsyncWork(peek.self));
      }

      [DebuggerHidden]
      private IEnumerator AsyncWork(Unit unit)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_UnitWithdraw.\u003CAsyncWork\u003Ec__Iterator46()
        {
          unit = unit,
          \u003C\u0024\u003Eunit = unit,
          \u003C\u003Ef__this = this
        };
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFinished)
          return;
        self.RemoveLog();
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_ArenaCalc : State<SceneBattle>
    {
      private GameObject mGoWin;

      public override void Begin(SceneBattle self)
      {
        self.Battle.ArenaCalcStart();
        self.ArenaActionCountSet(self.Battle.ArenaActionCount);
      }

      public override void Update(SceneBattle self)
      {
        if (!self.Battle.IsArenaCalc || !self.Battle.ArenaCalcStep())
          return;
        self.Battle.ArenaCalcFinish();
        self.StartCoroutine(this.sendResultStartReplay(self));
      }

      [DebuggerHidden]
      private IEnumerator sendResultStartReplay(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_ArenaCalc.\u003CsendResultStartReplay\u003Ec__Iterator47()
        {
          self = self,
          \u003C\u0024\u003Eself = self
        };
      }
    }

    private class State_DirectionOffSkill : State<SceneBattle>
    {
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>();
      private IEnumerator mTask;
      private IEnumerator mTaskNext;
      private LogSkill mActionInfo;
      private TacticsUnitController mInstigator;

      private void ResetTask()
      {
        this.mTask = (IEnumerator) null;
        this.mTaskNext = (IEnumerator) null;
      }

      private void SetTask(IEnumerator enumrator)
      {
        this.mTaskNext = enumrator;
      }

      private bool NextTask()
      {
        if (this.mTaskNext == null)
          return false;
        this.mTask = this.mTaskNext;
        this.mTaskNext = (IEnumerator) null;
        return true;
      }

      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        this.ResetTask();
        if (peek.skill == null)
        {
          self.RemoveLog();
          DebugUtility.LogError("SkillParam が存在しません。");
        }
        else
        {
          this.mActionInfo = peek;
          this.mInstigator = self.FindUnitController(this.mActionInfo.self);
          this.SetTask(this.Task_PrepareSkill(self));
          self.RemoveLog();
        }
      }

      public override void Update(SceneBattle self)
      {
        bool flag = !this.NextTask();
        if (this.mTask != null)
          flag = !this.mTask.MoveNext() && !this.NextTask();
        if (!flag)
          return;
        this.mTask = (IEnumerator) null;
      }

      public override void End(SceneBattle self)
      {
      }

      [DebuggerHidden]
      private IEnumerator Task_PrepareSkill(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_DirectionOffSkill.\u003CTask_PrepareSkill\u003Ec__Iterator48()
        {
          self = self,
          \u003C\u0024\u003Eself = self,
          \u003C\u003Ef__this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator Task_MapPrepareSkill(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_DirectionOffSkill.\u003CTask_MapPrepareSkill\u003Ec__Iterator49()
        {
          self = self,
          \u003C\u0024\u003Eself = self,
          \u003C\u003Ef__this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator Task_MapPlaySkill(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_DirectionOffSkill.\u003CTask_MapPlaySkill\u003Ec__Iterator4A()
        {
          self = self,
          \u003C\u0024\u003Eself = self,
          \u003C\u003Ef__this = this
        };
      }

      [DebuggerHidden]
      private IEnumerator Task_Execute(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_DirectionOffSkill.\u003CTask_Execute\u003Ec__Iterator4B()
        {
          self = self,
          \u003C\u0024\u003Eself = self,
          \u003C\u003Ef__this = this
        };
      }

      private enum EndGotoState
      {
        NONE,
        WAIT_FOR_LOG,
        WAITEVENT_SPAWN_SHIELD_EFFECT,
        SPAWN_SHIELD_EFFECT,
      }
    }

    public enum CameraMode
    {
      DEFAULT,
      UPVIEW,
    }

    public enum EMultiPlayCommand
    {
      NOP,
      MOVE_START,
      MOVE,
      GRID_XY,
      MOVE_END,
      MOVE_CANCEL,
      ENTRY_BATTLE,
      GRID_EVENT,
      UNIT_END,
      UNIT_TIME_LIMIT,
      UNIT_XYDIR,
      NUM,
    }

    public class MultiPlayInput
    {
      public int u = -1;
      public string s = string.Empty;
      public string i = string.Empty;
      public int d = 1;
      public int b;
      public int t;
      public int c;
      public int gx;
      public int gy;
      public int ul;
      public float x;
      public float z;
      public float r;

      public bool IsValid(SceneBattle self)
      {
        if (this.b < 0 || this.t < 0 || (this.c < 0 || this.c >= 11) || (this.u < -1 || this.u >= self.Battle.AllUnits.Count || this.s == null) || (this.s != string.Empty && MonoSingleton<GameManager>.Instance.GetSkillParam(this.s) == null || this.i == null || (this.i != string.Empty && MonoSingleton<GameManager>.Instance.GetItemParam(this.i) == null || (this.gx < 0 || this.gx >= self.Battle.CurrentMap.Width))) || (this.gy < 0 || this.gy >= self.Battle.CurrentMap.Height))
          return false;
        if (this.c == 6)
        {
          if (this.d != 1 && this.d != 2 && (this.d != 3 && this.d != 4))
            return false;
        }
        else if (this.d != 0 && this.d != 2 && (this.d != 1 && this.d != 3))
          return false;
        return (double) this.x >= 0.0 && (double) this.x <= (double) self.Battle.CurrentMap.Width && ((double) this.z >= 0.0 && (double) this.z <= (double) self.Battle.CurrentMap.Height);
      }
    }

    public enum EMultiPlayRecvDataHeader
    {
      NOP,
      INPUT,
      CHECK,
      GOODJOB,
      CONTINUE,
      IGNORE_MY_DISCONNECT,
      FINISH_LOAD,
      REQUEST_RESUME,
      RESUME_INFO,
      RESUME_SUCCESS,
      SYNC,
      SYNC_RESUME,
      VS_RETIRE,
      VS_RETIRE_COMFIRM,
      CHEAT_MOVE,
      CHEAT_MP,
      CHEAT_RANGE,
      OTHERPLAYER_DISCONNECT,
      NUM,
    }

    public enum CHEAT_TYPE
    {
      MOVE,
      MP,
      RANGE,
    }

    public class MultiPlayRecvData
    {
      public int sq;
      public int h;
      public int b;
      public int pidx;
      public int pid;
      public int uid;
      public int[] c;
      public int[] u;
      public string[] s;
      public string[] i;
      public int[] gx;
      public int[] gy;
      public int[] ul;
      public int[] d;
      public float[] x;
      public float[] z;
      public float[] r;
    }

    public class MultiPlayRecvBinData
    {
      public byte[] bin;
    }

    private class MultiPlayCheck
    {
      public int work;
      public int playerIndex;
      public int playerID;
      public int battleTurn;
      public int[] hp;
      public int[] gx;
      public int[] gy;
      public int[] dir;
      public string rnd;

      private bool IsEqual(int[] s0, int[] s1)
      {
        if (s0 == null && s1 == null)
          return true;
        if (s0 == null || s1 == null)
          return false;
        return ((IEnumerable<int>) s0).SequenceEqual<int>((IEnumerable<int>) s1);
      }

      public bool IsEqual(SceneBattle.MultiPlayCheck dst)
      {
        if (this.battleTurn != dst.battleTurn)
        {
          DebugUtility.LogError("battleTurn not match");
          return false;
        }
        if (!this.IsEqual(this.hp, dst.hp))
        {
          DebugUtility.LogError("hp not match");
          return false;
        }
        if (!this.IsEqual(this.gx, dst.gx))
        {
          DebugUtility.LogError("gx not match");
          return false;
        }
        if (!this.IsEqual(this.gy, dst.gy))
        {
          DebugUtility.LogError("gy not match");
          return false;
        }
        if (!this.IsEqual(this.dir, dst.dir))
        {
          DebugUtility.LogError("dir not match");
          return false;
        }
        if (string.IsNullOrEmpty(this.rnd) || string.IsNullOrEmpty(dst.rnd) || !(this.rnd != dst.rnd))
          return true;
        DebugUtility.LogError("rnd not match");
        return false;
      }

      private string GetIntListString(int[] a)
      {
        if (a == null)
          return "[null]";
        string str = string.Empty;
        for (int index = 0; index < a.Length; ++index)
          str = str + a[index].ToString() + ",";
        return str;
      }

      public override string ToString()
      {
        string str = string.Empty + "pid:" + (object) this.playerID + "pidx:" + (object) this.playerIndex + " bt:" + (object) this.battleTurn + " gx:" + this.GetIntListString(this.gx) + " gy:" + this.GetIntListString(this.gy) + " hp:" + this.GetIntListString(this.hp) + " dir:" + this.GetIntListString(this.dir);
        if (!string.IsNullOrEmpty(this.rnd))
          str = str + " rnd:" + this.rnd;
        return str;
      }
    }

    private enum EDisconnectType
    {
      DISCONNECTED,
      BAN,
      SEQUENCE_ERROR,
    }

    public enum ENotifyDisconnectedPlayerType
    {
      NORMAL,
      OWNER,
      OWNER_AND_I_AM_OWNER,
    }

    public class ReqCreateBreakObjUc
    {
      public SkillData mSkill;
      public Unit mTargetUnit;
      public bool mIsLoad;

      public ReqCreateBreakObjUc(SkillData skill, Unit target_unit)
      {
        this.mSkill = skill;
        this.mTargetUnit = target_unit;
        this.mIsLoad = false;
      }
    }

    private class State_Disconnected : State<SceneBattle>
    {
    }

    private class State_MultiPlayContinueBase : State<SceneBattle>
    {
      private List<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest> mReqPool = new List<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>();
      private bool mInitFlag;
      private int mRoomOwnerPlayerID;

      public int Seed { get; set; }

      public long BtlID { get; set; }

      private SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest SendMultiPlayContinueRequest(SceneBattle self, bool flag, List<int> units, int seed, long btlid)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
        if (myPlayer == null)
          return (SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest) null;
        List<SceneBattle.MultiPlayInput> sendList = new List<SceneBattle.MultiPlayInput>();
        if (units == null || units.Count <= 0)
        {
          sendList.Add(new SceneBattle.MultiPlayInput()
          {
            s = seed.ToString(),
            i = btlid.ToString()
          });
        }
        else
        {
          for (int index = 0; index < units.Count; ++index)
            sendList.Add(new SceneBattle.MultiPlayInput()
            {
              s = seed.ToString(),
              i = btlid.ToString(),
              u = units[index]
            });
        }
        byte[] sendBinary = self.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.CONTINUE, !flag ? 0 : 1, sendList);
        instance.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
        SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest = new SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest();
        playContinueRequest.playerID = myPlayer.playerID;
        playContinueRequest.flag = flag;
        playContinueRequest.units = units;
        playContinueRequest.seed = seed;
        playContinueRequest.btlid = btlid;
        this.mReqPool.Add(playContinueRequest);
        return playContinueRequest;
      }

      public override void Update(SceneBattle self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        MyPhoton.MyPlayer me = instance.GetMyPlayer();
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
        if (me == null || roomPlayerList == null || (myPlayersStarted == null || UnityEngine.Object.op_Equality((UnityEngine.Object) self.mBattleUI_MultiPlay, (UnityEngine.Object) null)))
        {
          this.Cancel();
        }
        else
        {
          if (!this.mInitFlag)
          {
            GlobalVars.SelectedMultiPlayContinue = GlobalVars.EMultiPlayContinue.PENDING;
            GlobalVars.SelectedMultiPlayerUnitIDs = (List<int>) null;
            this.mReqPool.Clear();
            this.mRoomOwnerPlayerID = instance.GetOldestPlayer();
            this.OpenUI(this.mRoomOwnerPlayerID == me.playerID);
            this.mInitFlag = true;
          }
          while (self.mRecvContinue.Count > 0)
          {
            SceneBattle.MultiPlayRecvData data = self.mRecvContinue[0];
            if (data.b > self.UnitStartCountTotal)
            {
              DebugUtility.LogWarning("[PUN] new turn data. sq:" + (object) data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) self.UnitStartCountTotal + " test:" + (object) self.mRecvBattle.FindIndex((Predicate<SceneBattle.MultiPlayRecvData>) (r => r.b < data.b)));
              break;
            }
            if (data.b < self.UnitStartCountTotal)
              DebugUtility.LogWarning("[PUN] old turn data. sq:" + (object) data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) data.h + " b:" + (object) data.b + "/" + (object) self.UnitStartCountTotal);
            else if (data.h == 4)
            {
              this.mReqPool.RemoveAll((Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>) (r => r.playerID == data.pid));
              SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest = new SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest();
              playContinueRequest.playerID = data.pid;
              playContinueRequest.flag = data.uid != 0;
              if (data.i != null && data.i.Length > 0)
                long.TryParse(data.i[0], out playContinueRequest.btlid);
              if (data.s != null && data.s.Length > 0)
                int.TryParse(data.s[0], out playContinueRequest.seed);
              if (data.u != null)
              {
                for (int index = 0; index < data.u.Length; ++index)
                {
                  if (data.u[index] >= 0)
                    playContinueRequest.units.Add(data.u[index]);
                }
              }
              this.mReqPool.Add(playContinueRequest);
            }
            self.mRecvContinue.RemoveAt(0);
          }
          if (this.mReqPool.Count == 0 && roomPlayerList.Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == this.mRoomOwnerPlayerID)) == null)
          {
            this.mInitFlag = false;
            this.CloseUI(this.mRoomOwnerPlayerID == me.playerID, false);
          }
          else
          {
            SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest1 = this.mReqPool.Find((Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>) (r => r.playerID == me.playerID));
            if (playContinueRequest1 == null)
            {
              if (me.playerID == this.mRoomOwnerPlayerID)
              {
                if (GlobalVars.SelectedMultiPlayContinue == GlobalVars.EMultiPlayContinue.PENDING)
                  return;
                if (GlobalVars.MultiPlayBattleCont != null && GlobalVars.MultiPlayBattleCont.btlinfo != null)
                {
                  this.BtlID = GlobalVars.MultiPlayBattleCont.btlid;
                  this.Seed = GlobalVars.MultiPlayBattleCont.btlinfo.seed;
                }
                playContinueRequest1 = this.SendMultiPlayContinueRequest(self, GlobalVars.SelectedMultiPlayContinue == GlobalVars.EMultiPlayContinue.CONTINUE, GlobalVars.SelectedMultiPlayerUnitIDs, this.Seed, this.BtlID);
              }
              else
              {
                int roomOwnerPlayerID = instance.GetOldestPlayer();
                SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest2 = this.mReqPool.Find((Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>) (r => r.playerID == roomOwnerPlayerID));
                if (playContinueRequest2 == null)
                  return;
                this.BtlID = playContinueRequest2.btlid;
                this.Seed = playContinueRequest2.seed;
                playContinueRequest1 = this.SendMultiPlayContinueRequest(self, playContinueRequest2.flag, playContinueRequest2.units, this.Seed, this.BtlID);
              }
            }
            using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                MyPhoton.MyPlayer player = enumerator.Current;
                if (player.start && this.mReqPool.Find((Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>) (r => r.playerID == player.playerID)) == null)
                  return;
              }
            }
            using (List<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>.Enumerator enumerator = this.mReqPool.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest current = enumerator.Current;
                if (playContinueRequest1.flag != current.flag || !playContinueRequest1.units.SequenceEqual<int>((IEnumerable<int>) current.units) || (playContinueRequest1.seed != current.seed || playContinueRequest1.btlid != current.btlid))
                {
                  this.mInitFlag = false;
                  this.CloseUI(this.mRoomOwnerPlayerID == me.playerID, false);
                  return;
                }
              }
            }
            this.CloseUI(this.mRoomOwnerPlayerID == me.playerID, true);
            if (!playContinueRequest1.flag)
            {
              this.Cancel();
            }
            else
            {
              this.ExecContinue(playContinueRequest1.units);
              self.ResetCheckData();
            }
          }
        }
      }

      protected virtual void OpenUI(bool roomOwner)
      {
      }

      protected virtual void CloseUI(bool roomOwner, bool decided)
      {
      }

      protected virtual void ExecContinue(List<int> units)
      {
      }

      protected virtual void Cancel()
      {
      }

      protected class MultiPlayContinueRequest
      {
        public List<int> units = new List<int>();
        public int playerID;
        public bool flag;
        public long btlid;
        public int seed;
      }
    }

    private class State_MultiPlayRevive : SceneBattle.State_MultiPlayContinueBase
    {
      public override void Begin(SceneBattle self)
      {
        int num1 = 0;
        using (List<Unit>.Enumerator enumerator = self.Battle.Units.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Unit current = enumerator.Current;
            if (current.OwnerPlayerIndex > 0 && current.IsDead)
              ++num1;
          }
        }
        QuestParam quest = self.Battle.GetQuest();
        int num2 = Math.Max(1, quest != null ? (int) quest.multiDead : 1);
        if (num1 >= num2)
          return;
        this.Cancel();
      }

      protected override void OpenUI(bool roomOwner)
      {
        if (roomOwner)
          this.self.mBattleUI_MultiPlay.StartSelectRevive();
        else
          this.self.mBattleUI_MultiPlay.ShowWaitRevive();
      }

      protected override void CloseUI(bool roomOwner, bool decided)
      {
        if (roomOwner)
          return;
        this.self.mBattleUI_MultiPlay.HideWaitRevive();
      }

      protected override void ExecContinue(List<int> units)
      {
        if (units != null)
        {
          for (int index = 0; index < units.Count; ++index)
          {
            if (units[index] >= 0 && units[index] < this.self.Battle.AllUnits.Count)
            {
              Unit allUnit = this.self.Battle.AllUnits[units[index]];
              if (allUnit != null && allUnit.IsDead)
                allUnit.ReqRevive = true;
            }
          }
        }
        this.self.GotoState<SceneBattle.State_WaitForLog>();
      }

      protected override void Cancel()
      {
        this.self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_MultiPlayContinue : SceneBattle.State_MultiPlayContinueBase
    {
      private bool mOpenMenu;
      private bool mOpenMenuReq;

      protected override void OpenUI(bool roomOwner)
      {
        if (roomOwner)
        {
          this.self.mBattleUI_MultiPlay.StartSelectContinue();
          this.mOpenMenuReq = false;
        }
        else
        {
          this.self.mBattleUI_MultiPlay.ShowWaitContinue();
          this.mOpenMenuReq = true;
        }
      }

      protected override void CloseUI(bool roomOwner, bool decided)
      {
        if (roomOwner)
        {
          this.mOpenMenuReq = false;
        }
        else
        {
          this.self.mBattleUI_MultiPlay.HideWaitContinue();
          if (!decided)
            return;
          this.mOpenMenuReq = false;
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mOpenMenu && !this.mOpenMenuReq)
        {
          self.mBattleUI.OnMPSelectContinueWaitingEnd();
          this.mOpenMenu = this.mOpenMenuReq;
        }
        else if (!this.mOpenMenu && this.mOpenMenuReq && UnityEngine.Object.op_Equality((UnityEngine.Object) ((Component) self.mBattleUI).get_gameObject().get_transform().Find("quest_lose(Clone)"), (UnityEngine.Object) null))
        {
          self.mBattleUI.OnMPSelectContinueWaitingStart();
          this.mOpenMenu = this.mOpenMenuReq;
        }
        base.Update(self);
      }

      protected override void ExecContinue(List<int> units)
      {
        if (this.mOpenMenu)
          this.self.mBattleUI.OnMPSelectContinueWaitingEnd();
        this.self.MultiPlayLog("[MultiPlayContinue] btlid:" + (object) this.self.Battle.BtlID + " > " + (object) this.BtlID + ", seed:" + (object) this.Seed);
        List<Unit> unitList = this.self.Battle.ContinueStart(this.self.Battle.BtlID, this.Seed);
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (instance.IsHost())
        {
          bool isMultiTower = this.self.Battle.IsMultiTower;
          instance.OpenRoom(isMultiTower, true);
        }
        using (List<Unit>.Enumerator enumerator = unitList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TacticsUnitController unitController = this.self.FindUnitController(enumerator.Current);
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            {
              this.self.mTacticsUnits.Remove(unitController);
              GameUtility.DestroyGameObject(((Component) unitController).get_gameObject());
            }
          }
        }
        this.self.RefreshJumpSpots();
        UnitQueue.Instance.Refresh(0);
        this.self.GotoState<SceneBattle.State_WaitForLog>();
      }

      protected override void Cancel()
      {
        if (this.mOpenMenu)
          this.self.mBattleUI.OnMPSelectContinueWaitingEnd();
        this.self.mExecDisconnected = true;
        this.self.SendIgnoreMyDisconnect();
        this.self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
      }
    }

    private class State_MultiPlaySync : State<SceneBattle>
    {
      private bool mSend;
      private bool mSendResume;

      public override void Begin(SceneBattle self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        this.mSend = false;
        this.mSendResume = false;
        if (instance.GetCurrentRoom().playerCount <= 1)
          return;
        self.mBattleUI_MultiPlay.OnOtherPlayerSyncStart();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mSend || !this.mSendResume && self.Battle.ResumeState != BattleCore.RESUME_STATE.NONE)
          this.SendSync();
        if (!self.CheckSync() && !self.CheckResumeSync())
          return;
        self.ResetSync();
        if (self.ResumeSuccess)
        {
          self.Battle.StartOrder(false, false, true);
          self.ResumeSuccess = false;
        }
        if (self.IsExistResume)
        {
          self.Battle.SetResumeWait();
          self.GotoState<SceneBattle.State_SyncResume>();
        }
        else
        {
          self.mBattleUI_MultiPlay.OnOtherPlayerSyncEnd();
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
      }

      private void SendSync()
      {
        if (MonoSingleton<GameManager>.Instance.AudienceMode || (double) this.self.mRestSyncInterval > 0.0)
          return;
        this.self.mRestSyncInterval = this.self.SYNC_INTERVAL;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        if (this.self.Battle.ResumeState != BattleCore.RESUME_STATE.NONE)
        {
          byte[] sendBinary = this.self.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.SYNC_RESUME, 0, (List<SceneBattle.MultiPlayInput>) null);
          instance.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
          this.mSendResume = true;
        }
        else
        {
          byte[] sendBinary = this.self.CreateSendBinary(SceneBattle.EMultiPlayRecvDataHeader.SYNC, 0, (List<SceneBattle.MultiPlayInput>) null);
          instance.SendRoomMessageBinary(true, sendBinary, MyPhoton.SEND_TYPE.Normal, false);
        }
        if (!this.self.ResumeSuccess)
          return;
        this.self.SendResumeSuccess();
        if (!this.self.IsExistResume)
          return;
        this.self.ResetSync();
        this.self.GotoState<SceneBattle.State_SyncResume>();
      }
    }

    private class State_SyncResume : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.mBattleUI_MultiPlay.OnOtherPlayerSyncStart();
      }

      public override void Update(SceneBattle self)
      {
        if (self.Battle.ResumeState != BattleCore.RESUME_STATE.NONE)
          return;
        self.GotoState<SceneBattle.State_MultiPlaySync>();
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_MapCommandVersus : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          self.mBattleUI.CommandWindow.OnCommandSelect = new UnitCommands.CommandEvent(this.OnCommandSelect);
        self.ShowAllHPGauges();
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          unitController.HideCursor(false);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          self.mBattleUI.TargetMain.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          self.mBattleUI.TargetSub.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          self.mBattleUI.TargetObjectSub.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          self.mBattleUI.TargetTrickSub.Close();
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.VersusMapView = false;
        if (!MonoSingleton<GameManager>.Instance.AudienceMode)
          return;
        self.AudiencePause = false;
      }

      public override void Update(SceneBattle self)
      {
        this.SyncCameraPosition(self);
        if (!self.mBattle.ConditionalUnitEnd(true))
          return;
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          return;
        self.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
      }

      private void SyncCameraPosition(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
          return;
        GameSettings instance = GameSettings.Instance;
        Transform transform = ((Component) Camera.get_main()).get_transform();
        transform.set_position(Vector3.op_Addition(((Component) unitController).get_transform().get_position(), ((Component) instance.Quest.MoveCamera).get_transform().get_position()));
        transform.set_rotation(((Component) instance.Quest.MoveCamera).get_transform().get_rotation());
        Vector3 position = ((Component) unitController).get_transform().get_position();
        self.SetCameraTarget((float) position.x, (float) position.z);
        self.mUpdateCameraPosition = true;
      }

      private void OnCommandSelect(UnitCommands.CommandTypes command, object ability)
      {
        this.self.mBattleUI.OnCommandSelect();
        if (command != UnitCommands.CommandTypes.Map)
          return;
        this.self.VersusMapView = true;
        this.self.GotoMapViewMode();
        if (!MonoSingleton<GameManager>.Instance.AudienceMode)
          return;
        this.self.AudiencePause = true;
      }
    }

    private class State_ComfirmFinishbattle : State<SceneBattle>
    {
      private bool mIsFadeIn;

      public override void Begin(SceneBattle self)
      {
        this.mIsFadeIn = false;
        GameUtility.SetDefaultSleepSetting();
        ProgressWindow.Close();
        GameUtility.FadeOut(1f);
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mIsFadeIn)
        {
          if (GameUtility.IsScreenFading)
            return;
          GameUtility.FadeIn(1f);
          self.mBattleUI_MultiPlay.OnAlreadyFinish();
          this.mIsFadeIn = true;
        }
        if (!self.AlreadyEndBattle)
          return;
        self.ForceEndQuest();
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_ForceWinComfirm : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
      }

      public override void Update(SceneBattle self)
      {
        if (!self.Battle.IsVSForceWinComfirm)
          return;
        self.mBattleUI.OnCommandSelect();
        self.Battle.StartOrder(false, false, true);
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_AudienceRetire : State<SceneBattle>
    {
      private bool mClose;

      public override void Begin(SceneBattle self)
      {
        string msg = string.Format(LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_RETIRE"), self.CheckAudienceResult() != BattleCore.QuestResult.Win ? (object) "1P" : (object) "2P");
        this.mClose = false;
        UIUtility.SystemMessage(string.Empty, msg, new UIUtility.DialogResultEvent(this.ClickEvent), (GameObject) null, false, 0);
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mClose)
          return;
        self.GotoState<SceneBattle.State_AudienceEnd>();
      }

      public void ClickEvent(GameObject go)
      {
        this.mClose = true;
      }
    }

    private class State_AudienceEnd : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        switch (self.CheckAudienceResult())
        {
          case BattleCore.QuestResult.Win:
          case BattleCore.QuestResult.Lose:
            self.mBattleUI.OnAudienceWin();
            break;
          default:
            self.mBattleUI.OnVersusDraw();
            break;
        }
        self.GotoState_WaitSignal<SceneBattle.State_ExitQuest>();
        Network.Abort();
      }
    }

    private class State_AudienceForceEnd : State<SceneBattle>
    {
      private bool mClose;

      public override void Begin(SceneBattle self)
      {
        string msg = LocalizedText.Get("sys.MULTI_VERSUS_AUDIENCE_END");
        this.mClose = false;
        UIUtility.SystemMessage(string.Empty, msg, new UIUtility.DialogResultEvent(this.ClickEvent), (GameObject) null, false, 0);
        Network.ResetError();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mClose)
          return;
        self.GotoState<SceneBattle.State_ExitQuest>();
      }

      public void ClickEvent(GameObject go)
      {
        this.mClose = true;
      }
    }

    private class MultiPlayer
    {
      public MultiPlayer(SceneBattle self, int playerIndex, int playerID)
      {
        this.PlayerIndex = playerIndex;
        this.PlayerID = playerID;
        this.NotifyDisconnected = false;
        this.Disconnected = false;
        self.MultiPlayLog("[PUN] new MultiPlayer playerIndex:" + (object) playerIndex + " playerID:" + (object) playerID);
      }

      public int PlayerIndex { get; private set; }

      public int PlayerID { get; private set; }

      public bool NotifyDisconnected { get; set; }

      public bool Disconnected { get; set; }

      public int RecvInputNum { get; set; }

      public bool FinishLoad { get; set; }

      public bool SyncWait { get; set; }

      public bool SyncResumeWait { get; set; }

      public bool StartBegin { get; set; }

      public void Begin(SceneBattle self)
      {
        this.StartBegin = true;
      }

      public void Update(SceneBattle self)
      {
      }

      public void End(SceneBattle self)
      {
        this.StartBegin = false;
      }
    }

    private class MultiPlayerUnit
    {
      private EUnitDirection mDir = EUnitDirection.NegativeX;
      private List<SceneBattle.MultiPlayInput> mRecv = new List<SceneBattle.MultiPlayInput>();
      private Unit mUnit;
      private int mGridX;
      private int mGridY;
      private int mTargetX;
      private int mTargetY;
      private SceneBattle.MultiPlayerUnit.EGridSnap mGridSnap;
      private bool mIsRunning;
      private int mRecvTurnNum;
      private int mCurrentTurn;
      private float mTurnSec;
      private int mTurnCmdNum;
      private int mTurnCmdDoneNum;
      private GridMap<int> mMoveGrid;
      private bool mBegin;

      public MultiPlayerUnit(SceneBattle self, int unitID, Unit unit, SceneBattle.MultiPlayer owner)
      {
        this.Owner = owner;
        this.UnitID = unitID;
        this.mUnit = unit;
        self.MultiPlayLog("[PUN] new MultiPlayerUnit unitID:" + (object) unitID + " name:" + unit.UnitName + " ownerPlayerIndex:" + (object) (owner != null ? owner.PlayerIndex : 0));
      }

      public SceneBattle.MultiPlayer Owner { get; private set; }

      public int UnitID { get; private set; }

      public Unit Unit
      {
        get
        {
          return this.mUnit;
        }
      }

      public bool IsMoveCompleted(SceneBattle self, int x, int y, TacticsUnitController controller)
      {
        if (!this.mBegin)
          return true;
        if (this.mGridSnap != SceneBattle.MultiPlayerUnit.EGridSnap.DONE || this.mIsRunning)
          return false;
        IntVector2 intVector2 = self.CalcCoord(((Component) controller).get_transform().get_position());
        return intVector2.x == x && intVector2.y == y;
      }

      public bool IsExistRecvData
      {
        get
        {
          return this.mRecv.Count > 0;
        }
      }

      public void RecvInput(SceneBattle self, SceneBattle.MultiPlayRecvData data)
      {
        int num = Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(Math.Max(0, data.c != null ? data.c.Length : 0), data.u != null ? data.u.Length : 0), data.s != null ? data.s.Length : 0), data.i != null ? data.i.Length : 0), data.gx != null ? data.gx.Length : 0), data.gy != null ? data.gy.Length : 0), data.d != null ? data.d.Length : 0), data.x != null ? data.x.Length : 0), data.z != null ? data.z.Length : 0), data.r != null ? data.r.Length : 0);
        for (int index = 0; index < num; ++index)
        {
          if (data.c[index] != 0)
          {
            SceneBattle.MultiPlayInput multiPlayInput = new SceneBattle.MultiPlayInput();
            multiPlayInput.b = data.b;
            multiPlayInput.t = this.mRecvTurnNum;
            multiPlayInput.c = data.c == null || index >= data.c.Length ? multiPlayInput.c : data.c[index];
            multiPlayInput.u = data.u == null || index >= data.u.Length ? multiPlayInput.u : data.u[index];
            multiPlayInput.s = data.s == null || index >= data.s.Length ? multiPlayInput.s : data.s[index];
            multiPlayInput.i = data.i == null || index >= data.i.Length ? multiPlayInput.i : data.i[index];
            multiPlayInput.gx = data.gx == null || index >= data.gx.Length ? multiPlayInput.gx : data.gx[index];
            multiPlayInput.gy = data.gy == null || index >= data.gy.Length ? multiPlayInput.gy : data.gy[index];
            multiPlayInput.ul = data.ul == null || index >= data.ul.Length ? multiPlayInput.ul : data.ul[index];
            multiPlayInput.d = data.d == null || index >= data.d.Length ? multiPlayInput.d : data.d[index];
            multiPlayInput.x = data.x == null || index >= data.x.Length ? multiPlayInput.x : data.x[index];
            multiPlayInput.z = data.z == null || index >= data.z.Length ? multiPlayInput.z : data.z[index];
            multiPlayInput.r = data.r == null || index >= data.r.Length ? multiPlayInput.r : data.r[index];
            if (!multiPlayInput.IsValid(self))
            {
              DebugUtility.LogError("[PUN] illegal input recv.");
            }
            else
            {
              if (multiPlayInput.c == 4)
                self.MultiPlayLog("[PUN] recv MOVE_END");
              else if (multiPlayInput.c == 6)
                self.MultiPlayLog("[PUN] recv ENTRY_BATTLE");
              else if (multiPlayInput.c == 7)
                self.MultiPlayLog("[PUN] recv GRID_EVENT");
              else if (multiPlayInput.c == 8)
                self.MultiPlayLog("[PUN] recv UNIT_END");
              else if (multiPlayInput.c == 9)
                self.MultiPlayLog("[PUN] recv TIME_LIMIT");
              this.mRecv.Add(multiPlayInput);
            }
          }
        }
        ++this.mRecvTurnNum;
        self.MultiPlayLog("[PUN]RecvInput from unitID:" + (object) this.UnitID + " " + (object) this.mRecvTurnNum + " sq:" + (object) data.sq + " b:" + (object) data.b + "/" + (object) self.UnitStartCountTotal);
      }

      public void Begin(SceneBattle self)
      {
        if (this.mBegin)
          return;
        this.mBegin = true;
        this.mRecvTurnNum = 0;
        this.mCurrentTurn = 0;
        this.mTurnSec = 0.0f;
        this.mTurnCmdNum = -1;
        this.mTurnCmdDoneNum = 0;
        this.mRecv.RemoveAll((Predicate<SceneBattle.MultiPlayInput>) (r => r.b < self.mUnitStartCountTotal));
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        self.MultiPlayLog("[PUN]Begin MultiPlayer unitID: " + (object) this.UnitID + " name:" + this.mUnit.UnitName + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y + " dir:" + (object) this.mUnit.Direction + " sx:" + (object) this.mUnit.startX + " sy:" + (object) this.mUnit.startY);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || this.mUnit.IsDead)
          return;
        unitController.AutoUpdateRotation = false;
        this.mGridSnap = SceneBattle.MultiPlayerUnit.EGridSnap.DONE;
        this.mIsRunning = false;
        this.mGridX = this.mTargetX = this.mUnit.x;
        this.mGridY = this.mTargetY = this.mUnit.y;
        unitController.WalkableField = self.CreateCurrentAccessMap();
        if (!self.Battle.IsMultiVersus)
          unitController.ShowOwnerIndexUI(true);
        this.mMoveGrid = self.CreateCurrentAccessMap();
      }

      public void End(SceneBattle self)
      {
        if (!this.mBegin)
          return;
        this.mBegin = false;
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          if (this.mIsRunning)
          {
            this.mIsRunning = false;
            unitController.StopRunning();
          }
          ((Component) unitController).get_transform().set_position(self.CalcGridCenter(this.mUnit.x, this.mUnit.y));
          unitController.AutoUpdateRotation = true;
          unitController.WalkableField = (GridMap<int>) null;
          unitController.ShowOwnerIndexUI(false);
        }
        self.MultiPlayLog("MultiPlayerEnd. ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y + " gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY);
      }

      private Vector3 GetPosition3D(SceneBattle self, float x, float z)
      {
        Vector3 position;
        // ISSUE: explicit reference operation
        ((Vector3) @position).\u002Ector(x, 0.0f, z);
        IntVector2 intVector2 = self.CalcCoord(position);
        Vector3 vector3 = self.CalcGridCenter(intVector2.x, intVector2.y);
        position.y = vector3.y;
        return position;
      }

      private bool CheckMoveable(int x, int y)
      {
        if (this.mMoveGrid != null)
          return this.mMoveGrid.get(x, y) >= 0;
        return true;
      }

      public void Update(SceneBattle self)
      {
        if (!this.mBegin)
          return;
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || this.mUnit.IsDead || !self.IsInState<SceneBattle.State_MapCommandMultiPlay>() && !self.IsInState<SceneBattle.State_MapCommandVersus>() && !self.IsInState<SceneBattle.State_SelectTargetV2>())
          return;
        unitController.AutoUpdateRotation = false;
        if (unitController.IsPlayingFieldAction)
          return;
        GameManager instance1 = MonoSingleton<GameManager>.Instance;
        bool audienceMode = instance1.AudienceMode;
        if (this.mRecv.Count <= 0 && !audienceMode && !instance1.IsVSCpuBattle || audienceMode && this.Owner.Disconnected && this.mRecv.Count <= 0 || instance1.IsVSCpuBattle && unitController.Unit.OwnerPlayerIndex == 2)
        {
          if (this.Owner != null && this.IsMoveCompleted(self, this.mGridX, this.mGridY, unitController))
          {
            MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
            List<MyPhoton.MyPlayer> roomPlayerList = instance2.GetRoomPlayerList();
            MyPhoton.MyPlayer player = instance2.FindPlayer(roomPlayerList, this.Owner.PlayerID, this.Owner.PlayerIndex);
            if (player == null || !player.start && this.Owner.Disconnected)
            {
              ((Component) unitController).get_transform().set_position(self.CalcGridCenter(this.mUnit.x, this.mUnit.y));
              unitController.CancelAction();
              ((Component) unitController).get_transform().set_rotation(this.mUnit.Direction.ToRotation());
              unitController.AutoUpdateRotation = true;
              self.Battle.EntryBattleMultiPlayTimeUp = true;
              self.Battle.MultiPlayDisconnectAutoBattle = true;
              self.GotoMapCommand();
              DebugUtility.LogWarning("[PUN] detect player disconnected");
            }
          }
        }
        else if (this.Owner != null)
        {
          this.mTurnSec += Time.get_deltaTime();
          if (this.mTurnCmdNum < 0 && this.mRecv.Count > 0)
          {
            this.mTurnCmdNum = 0;
            int t = this.mRecv[0].t;
            while (this.mTurnCmdNum < this.mRecv.Count && this.mRecv[this.mTurnCmdNum].t == t)
              ++this.mTurnCmdNum;
            self.MultiPlayLog("[PUN] start send turn:" + (object) t + "/" + (object) this.mCurrentTurn + " cmdNum:" + (object) this.mTurnCmdNum + " recvTurnNum:" + (object) this.mRecvTurnNum + " recvCount:" + (object) this.mRecv.Count);
          }
          int num1 = Math.Min(this.mTurnCmdNum, (int) ((double) this.mTurnSec * (double) this.mTurnCmdNum) + 1);
          while (this.mTurnCmdDoneNum < num1 && this.mRecv.Count > 0)
          {
            int num2 = 0;
            SceneBattle.MultiPlayInput multiPlayInput = this.mRecv[0];
            if (multiPlayInput.c == 4 || multiPlayInput.c == 3 || (multiPlayInput.c == 7 || multiPlayInput.c == 8))
            {
              if (multiPlayInput.c != 4 && multiPlayInput.c != 8 || !unitController.isMoving)
              {
                IntVector2 intVector2 = self.CalcCoord(((Component) unitController).get_transform().get_position());
                if (this.mGridX != intVector2.x || this.mGridY != intVector2.y)
                {
                  this.mGridX = intVector2.x;
                  this.mGridY = intVector2.y;
                }
                this.mTargetX = multiPlayInput.gx;
                this.mTargetY = multiPlayInput.gy;
                if (this.mTargetX != this.mGridX || this.mTargetY != this.mGridY)
                {
                  if (multiPlayInput.c == 3)
                  {
                    this.mDir = (EUnitDirection) multiPlayInput.d;
                    this.mRecv.RemoveAt(0);
                    break;
                  }
                  break;
                }
              }
              else
                break;
            }
            if (multiPlayInput.c != 0)
            {
              if (multiPlayInput.c == 3)
              {
                this.mDir = (EUnitDirection) multiPlayInput.d;
                self.MultiPlayLog("recv GRID_XY:" + (object) this.mGridX + " " + (object) this.mGridY);
              }
              else if (multiPlayInput.c != 1)
              {
                if (multiPlayInput.c == 4)
                {
                  self.MultiPlayLog("recv MOVE_END:" + (object) this.mGridX + " " + (object) this.mGridY);
                  if (this.mGridX != multiPlayInput.gx || this.mGridY != multiPlayInput.gy)
                    DebugUtility.LogWarning("move pos not match gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tx:" + (object) multiPlayInput.gx + " ty:" + (object) multiPlayInput.gy);
                  self.Battle.MoveMultiPlayer(this.mUnit, this.mGridX, this.mGridY, this.mDir);
                  if (!this.CheckMoveable(this.mGridX, this.mGridY))
                    self.SendCheat(SceneBattle.CHEAT_TYPE.MOVE, this.Owner.PlayerIndex);
                }
                else if (multiPlayInput.c == 5)
                {
                  this.mGridX = this.mTargetX = this.mUnit.startX;
                  this.mGridY = this.mTargetY = this.mUnit.startY;
                  this.mDir = this.mUnit.startDir;
                  self.ResetMultiPlayerTransform(this.mUnit);
                  self.MultiPlayLog("[PUN] recv MOVE_CANCEL (" + (object) this.mGridX + "," + (object) this.mGridY + ")" + (object) this.mDir);
                }
                else if (multiPlayInput.c != 2)
                {
                  if (multiPlayInput.c == 6)
                  {
                    EBattleCommand d = (EBattleCommand) multiPlayInput.d;
                    if (d == EBattleCommand.Wait)
                      self.Battle.MapCommandEnd(this.mUnit);
                    else if (!unitController.isMoving)
                    {
                      Unit enemy = multiPlayInput.u >= 0 ? self.Battle.AllUnits[multiPlayInput.u] : (Unit) null;
                      SkillData skillData = this.mUnit.GetSkillData(multiPlayInput.s);
                      if (skillData != null && !this.mUnit.CheckEnableUseSkill(skillData, true))
                        self.SendCheat(SceneBattle.CHEAT_TYPE.MP, this.Owner.PlayerIndex);
                      ItemData inventoryByItemId = MonoSingleton<GameManager>.Instance.Player.FindInventoryByItemID(multiPlayInput.i);
                      int gx = multiPlayInput.gx;
                      int gy = multiPlayInput.gy;
                      bool unitLockTarget = multiPlayInput.ul != 0;
                      if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                      {
                        self.MultiPlayLog("[PUN]waiting move completed for ENTRY_BATTLE... SNAP:" + (object) this.mGridSnap + " RUN:" + (object) this.mIsRunning + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                        IntVector2 intVector2 = self.CalcCoord(((Component) unitController).get_transform().get_position());
                        this.mGridX = intVector2.x;
                        this.mGridY = intVector2.y;
                        break;
                      }
                      if (skillData != null && !self.Battle.CreateSelectGridMap(this.mUnit, this.mUnit.x, this.mUnit.y, skillData).get(gx, gy))
                        self.SendCheat(SceneBattle.CHEAT_TYPE.RANGE, this.Owner.PlayerIndex);
                      self.MultiPlayLog("[PUN]MultiPlayerUnit ENTRY_BATTLE");
                      self.HideAllHPGauges();
                      self.HideAllUnitOwnerIndex();
                      self.Battle.EntryBattleMultiPlay(d, this.mUnit, enemy, skillData, inventoryByItemId, gx, gy, unitLockTarget);
                      num2 = 1;
                      this.mDir = this.mUnit.Direction;
                      this.mGridX = this.mTargetX = this.mUnit.x;
                      this.mGridY = this.mTargetY = this.mUnit.y;
                    }
                    else
                      break;
                  }
                  else if (multiPlayInput.c == 7)
                  {
                    if (this.mGridX != multiPlayInput.gx || this.mGridY != multiPlayInput.gy)
                      DebugUtility.LogWarning("GRID_EVENT move pos not match gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tx:" + (object) multiPlayInput.gx + " ty:" + (object) multiPlayInput.gy);
                    if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                    {
                      self.MultiPlayLog("[PUN]waiting move completed for GRID_EVENT...begin:" + (object) this.mBegin + " run:" + (object) this.mIsRunning + " snap:" + (object) this.mGridSnap + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                      break;
                    }
                    self.MultiPlayLog("[PUN]MultiPlayerUnit GRID_EVENT");
                    self.Battle.ExecuteEventTriggerOnGrid(this.mUnit, EEventTrigger.ExecuteOnGrid);
                    num2 = 1;
                  }
                  else if (multiPlayInput.c == 8)
                  {
                    if (this.mGridX != multiPlayInput.gx || this.mGridY != multiPlayInput.gy)
                    {
                      DebugUtility.LogWarning("UNIT_END move pos not match gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tx:" + (object) multiPlayInput.gx + " ty:" + (object) multiPlayInput.gy);
                      this.mGridX = multiPlayInput.gx;
                      this.mGridY = multiPlayInput.gy;
                      break;
                    }
                    if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                    {
                      self.MultiPlayLog("[PUN]waiting move completed for UNIT_END...begin:" + (object) this.mBegin + " run:" + (object) this.mIsRunning + " snap:" + (object) this.mGridSnap + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                      break;
                    }
                    self.MultiPlayLog("[PUN]MultiPlayerUnit UNIT_END");
                    self.Battle.CommandWait((EUnitDirection) multiPlayInput.d);
                    num2 = 1;
                  }
                  else if (multiPlayInput.c == 9)
                  {
                    unitController.StopRunning();
                    this.mUnit.x = this.mGridX = this.mTargetX = multiPlayInput.gx;
                    this.mUnit.y = this.mGridY = this.mTargetY = multiPlayInput.gy;
                    ((Component) unitController).get_transform().set_position(self.CalcGridCenter(multiPlayInput.gx, multiPlayInput.gy));
                    if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                    {
                      self.MultiPlayLog("[PUN]waiting move completed for UNIT_TIME_LIMIT...begin:" + (object) this.mBegin + " run:" + (object) this.mIsRunning + " snap:" + (object) this.mGridSnap + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                      break;
                    }
                    this.mUnit.Direction = (EUnitDirection) multiPlayInput.d;
                    num2 = 2;
                    self.MultiPlayLog("[PUN]UnitTimeUp!");
                  }
                  else if (multiPlayInput.c == 10)
                  {
                    this.mUnit.x = this.mGridX = multiPlayInput.gx;
                    this.mUnit.y = this.mGridY = multiPlayInput.gy;
                    this.mUnit.Direction = this.mDir = (EUnitDirection) multiPlayInput.d;
                    self.MultiPlayLog("recv UNIT_XYDIR:" + (object) this.mGridX + " " + (object) this.mGridY + " " + (object) this.mDir);
                  }
                }
              }
            }
            this.mRecv.RemoveAt(0);
            ++this.mTurnCmdDoneNum;
            switch (num2)
            {
              case 1:
                unitController.AutoUpdateRotation = true;
                self.GotoState<SceneBattle.State_WaitForLog>();
                goto label_70;
              case 2:
                unitController.AutoUpdateRotation = true;
                self.Battle.EntryBattleMultiPlayTimeUp = true;
                self.GotoMapCommand();
                goto label_70;
              default:
                continue;
            }
          }
label_70:
          if (this.mTurnCmdDoneNum >= this.mTurnCmdNum)
          {
            self.MultiPlayLog("[PUN] done send turn:" + (object) this.mCurrentTurn + " cmdNum:" + (object) this.mTurnCmdNum + " recvTurnNum:" + (object) this.mRecvTurnNum + " recvCount:" + (object) this.mRecv.Count);
            ++this.mCurrentTurn;
            this.mTurnSec = 0.0f;
            this.mTurnCmdDoneNum = 0;
            this.mTurnCmdNum = -1;
          }
        }
        if (unitController.AutoUpdateRotation)
          return;
        this.Move(self, this.mTargetX, this.mTargetY);
      }

      private void Move(SceneBattle self, int tx, int ty)
      {
        if (tx == this.mGridX && ty == this.mGridY)
          return;
        this.mMoveGrid = self.CreateCurrentAccessMap();
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        Vector3[] path = self.FindPath(this.mGridX, this.mGridY, tx, ty, this.mUnit.DisableMoveGridHeight, this.mMoveGrid);
        if (path != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          double num = (double) unitController.StartMove(path, -1f);
          this.mGridX = tx;
          this.mGridY = ty;
        }
        else
        {
          this.mGridX = tx;
          this.mGridY = ty;
        }
      }

      public void UpdateSkip(SceneBattle self)
      {
        if (!this.mBegin)
          return;
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || this.mUnit.IsDead)
          return;
        unitController.AutoUpdateRotation = false;
        if (this.mRecv.Count <= 0)
        {
          if (!this.Owner.Disconnected && this.mUnit.IsControl)
            return;
          do
            ;
          while (self.Battle.UpdateMapAI(false));
          ((Component) unitController).get_transform().set_position(self.CalcGridCenter(this.mUnit.x, this.mUnit.y));
        }
        else
        {
          if (this.Owner == null)
            return;
          if (!this.mUnit.IsControl)
          {
            do
              ;
            while (self.Battle.UpdateMapAI(false) && !this.mUnit.IsControl);
            ((Component) unitController).get_transform().set_position(self.CalcGridCenter(this.mUnit.x, this.mUnit.y));
          }
          while (this.mRecv.Count > 0)
          {
            SceneBattle.MultiPlayInput multiPlayInput = this.mRecv[0];
            if (multiPlayInput.c == 4 || multiPlayInput.c == 3 || (multiPlayInput.c == 7 || multiPlayInput.c == 8))
            {
              this.mUnit.x = this.mGridX = this.mTargetX = multiPlayInput.gx;
              this.mUnit.y = this.mGridY = this.mTargetY = multiPlayInput.gy;
              ((Component) unitController).get_transform().set_position(self.CalcGridCenter(multiPlayInput.gx, multiPlayInput.gy));
            }
            if (multiPlayInput.c == 3)
              this.mDir = (EUnitDirection) multiPlayInput.d;
            else if (multiPlayInput.c == 4)
              self.Battle.MoveMultiPlayer(this.mUnit, this.mGridX, this.mGridY, this.mDir);
            else if (multiPlayInput.c == 5)
            {
              this.mGridX = this.mTargetX = this.mUnit.startX;
              this.mGridY = this.mTargetY = this.mUnit.startY;
              this.mDir = this.mUnit.startDir;
              self.ResetMultiPlayerTransform(this.mUnit);
            }
            else if (multiPlayInput.c == 6)
            {
              EBattleCommand d = (EBattleCommand) multiPlayInput.d;
              if (d == EBattleCommand.Wait)
              {
                self.Battle.MapCommandEnd(this.mUnit);
              }
              else
              {
                Unit enemy = multiPlayInput.u >= 0 ? self.Battle.AllUnits[multiPlayInput.u] : (Unit) null;
                SkillData skillData = this.mUnit.GetSkillData(multiPlayInput.s);
                ItemData inventoryByItemId = MonoSingleton<GameManager>.Instance.Player.FindInventoryByItemID(multiPlayInput.i);
                int gx = multiPlayInput.gx;
                int gy = multiPlayInput.gy;
                bool unitLockTarget = multiPlayInput.ul != 0;
                self.Battle.EntryBattleMultiPlay(d, this.mUnit, enemy, skillData, inventoryByItemId, gx, gy, unitLockTarget);
                if (skillData != null && skillData.IsSetBreakObjSkill())
                {
                  Unit gimmickAtGrid = self.Battle.FindGimmickAtGrid(gx, gy, false);
                  if (gimmickAtGrid != null && gimmickAtGrid.IsBreakObj)
                    self.ReqCreateBreakObjUcLists.Add(new SceneBattle.ReqCreateBreakObjUc(skillData, gimmickAtGrid));
                }
                this.mDir = this.mUnit.Direction;
                this.mGridX = this.mTargetX = this.mUnit.x;
                this.mGridY = this.mTargetY = this.mUnit.y;
              }
            }
            else if (multiPlayInput.c == 7)
              self.Battle.ExecuteEventTriggerOnGrid(this.mUnit, EEventTrigger.ExecuteOnGrid);
            else if (multiPlayInput.c == 8)
              this.mUnit.Direction = (EUnitDirection) multiPlayInput.d;
            else if (multiPlayInput.c == 9)
            {
              unitController.StopRunning();
              this.mGridX = this.mTargetX = multiPlayInput.gx;
              this.mGridY = this.mTargetY = multiPlayInput.gy;
              ((Component) unitController).get_transform().set_position(self.CalcGridCenter(multiPlayInput.gx, multiPlayInput.gy));
              this.mUnit.Direction = (EUnitDirection) multiPlayInput.d;
            }
            else if (multiPlayInput.c == 10)
            {
              this.mUnit.x = this.mGridX = multiPlayInput.gx;
              this.mUnit.y = this.mGridY = multiPlayInput.gy;
              this.mUnit.Direction = this.mDir = (EUnitDirection) multiPlayInput.d;
            }
            this.mRecv.RemoveAt(0);
          }
          if (!this.Owner.Disconnected && this.mUnit.IsControl)
            return;
          do
            ;
          while (self.Battle.UpdateMapAI(false));
          ((Component) unitController).get_transform().set_position(self.CalcGridCenter(this.mUnit.x, this.mUnit.y));
        }
      }

      private enum EGridSnap
      {
        NOP,
        ACTIVE,
        DONE,
        NUM,
      }
    }

    public enum TargetActionTypes
    {
      None,
      Attack,
      Skill,
      Heal,
    }

    private class State_WaitSignal<T> : State<SceneBattle> where T : State<SceneBattle>, new()
    {
      public override void Begin(SceneBattle self)
      {
        self.mIsWaitingForBattleSignal = true;
        this.Update(self);
      }

      public override void Update(SceneBattle self)
      {
        if (self.mUISignal)
          return;
        self.mIsWaitingForBattleSignal = false;
        self.GotoState<T>();
      }
    }

    private class State_LoadMapV2 : State<SceneBattle>
    {
      private bool mReady;

      public override void Begin(SceneBattle self)
      {
        while (TacticsSceneSettings.SceneCount > 0)
          TacticsSceneSettings.PopFirstScene();
        self.IsLoaded = true;
        self.mUpdateCameraPosition = true;
        Debug.Log((object) ("======= LOAD MAP START (" + (object) self.Battle.MapIndex + ")"));
        self.BeginLoadMapAsync();
        self.SetUnitUiHeight(self.mBattle.CurrentUnit);
      }

      public override void Update(SceneBattle self)
      {
        if (self.IsCameraMoving || !self.mMapReady || ProgressWindow.ShouldKeepVisible)
          return;
        if (!this.mReady)
        {
          GameManager instance1 = MonoSingleton<GameManager>.Instance;
          if (self.Battle.IsMultiPlay && !instance1.AudienceMode)
          {
            if (instance1.IsVSCpuBattle)
            {
              self.Battle.MultiFinishLoad = true;
            }
            else
            {
              if (!self.Battle.MultiFinishLoad)
              {
                MyPhoton instance2 = PunMonoSingleton<MyPhoton>.Instance;
                self.Battle.MultiFinishLoad = self.SendFinishLoad();
                if (!instance2.IsConnected())
                  self.Battle.MultiFinishLoad = true;
              }
              if (!self.Battle.SyncStart)
                return;
            }
          }
          else if (instance1.AudienceMode && !self.Battle.MultiFinishLoad)
          {
            if (self.AudienceSkip)
            {
              self.SkipLog();
              return;
            }
            self.AudienceSkip = true;
            instance1.AudienceManager.FinishLoad();
            instance1.AudienceManager.SkipMode = true;
            return;
          }
          if (self.mTutorialTriggers != null)
          {
            for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
              self.mTutorialTriggers[index].OnMapStart();
          }
          this.mReady = true;
          GameUtility.SetDefaultSleepSetting();
          GameUtility.FadeOut(1f);
        }
        if (GameUtility.IsScreenFading)
          return;
        ProgressWindow.SetDestroyDelay(0.0f);
        ProgressWindow.Close();
        self.StartDownloadNextQuestAsync();
        GlobalEvent.Invoke("BATTLE_MAP_LOADED", (object) null);
        GlobalEvent.Invoke("CLOSE_UNIT_TOOLTIP", (object) null);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) (self.mEventSequence = self.mEventScript.OnPostMapLoad()), (UnityEngine.Object) null))
        {
          self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_StartEvent>>();
        }
        else
        {
          GameUtility.FadeIn(1f);
          self.GotoState<SceneBattle.State_WaitFade<SceneBattle.State_StartEvent>>();
        }
      }
    }

    private class State_StartEvent : State<SceneBattle>
    {
      private EventScript.Sequence mSequence;

      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          this.Finish();
        }
        else
        {
          this.mSequence = self.mEventScript.OnStart(0, true);
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSequence, (UnityEngine.Object) null))
            this.Finish();
          else
            self.mUpdateCameraPosition = false;
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mSequence.IsPlaying)
          return;
        UnityEngine.Object.Destroy((UnityEngine.Object) this.mSequence);
        self.ResetCameraTarget();
        this.Finish();
      }

      private void Finish()
      {
        if (this.self.IsPlayingArenaQuest)
          this.self.mBattleUI.OnQuestStart_Arena();
        else if (this.self.Battle.IsMultiVersus)
        {
          if (this.self.Battle.IsRankMatch)
            this.self.mBattleUI.OnQuestStart_RankMatch();
          else
            this.self.mBattleUI.OnQuestStart_VS();
        }
        else if (string.IsNullOrEmpty(this.self.mCurrentQuest.cond))
          this.self.mBattleUI.OnQuestStart_Short();
        else
          this.self.mBattleUI.OnQuestStart();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI_MultiPlay, (UnityEngine.Object) null))
          this.self.mBattleUI_MultiPlay.OnMapStart();
        this.self.GotoState_WaitSignal<SceneBattle.State_MapStartV2>();
      }
    }

    private class State_MapStartV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.QuestStart = true;
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_PreUnitStart : State<SceneBattle>
    {
      private IEnumerator m_Task;

      [DebuggerHidden]
      private IEnumerator Task_Begin(SceneBattle self)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PreUnitStart.\u003CTask_Begin\u003Ec__Iterator4C()
        {
          self = self,
          \u003C\u0024\u003Eself = self
        };
      }

      public override void Begin(SceneBattle self)
      {
        self.RemoveLog();
        this.m_Task = this.Task_Begin(self);
      }

      public override void Update(SceneBattle self)
      {
        if (this.m_Task != null)
        {
          if (this.m_Task.MoveNext())
            return;
          this.m_Task = (IEnumerator) null;
        }
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTacticsUnits[index], (UnityEngine.Object) null) && self.mTacticsUnits[index].IsHPGaugeChanging)
            return;
        }
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTacticsUnits[index], (UnityEngine.Object) null))
            self.mTacticsUnits[index].ResetHPGauge();
        }
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
          self.mEventSequence = self.mEventScript.OnUnitStart(unitController);
        self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_UnitStartV2>>();
      }
    }

    private class State_UnitStartV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        self.mAutoActivateGimmick = false;
        if (self.mTutorialTriggers != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
            self.mTutorialTriggers[index].OnUnitStart(self.mBattle.CurrentUnit, unitController.Unit.TurnCount);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          self.SetPrioritizedUnit(unitController);
          Unit currentUnit = self.Battle.CurrentUnit;
          if (currentUnit != null && !currentUnit.IsDead)
          {
            self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
            self.InterpCameraTarget((Component) unitController);
          }
          else
            Debug.Log((object) "[PUN]I'm dead or null...");
          self.mCurrentUnitStartX = unitController.Unit.startX;
          self.mCurrentUnitStartY = unitController.Unit.startY;
        }
        self.ToggleRenkeiAura(true);
        if (!self.Battle.IsMultiPlay)
          return;
        self.BeginMultiPlayer();
      }

      public override void Update(SceneBattle self)
      {
        Unit currentUnit = self.mBattle.CurrentUnit;
        if (currentUnit != null && !currentUnit.IsDead)
        {
          if (self.IsCameraMoving)
            return;
          TacticsUnitController unitController1 = self.FindUnitController(currentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
            unitController1.ResetRotation();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          {
            List<AbilityData> abilityDataList1 = new List<AbilityData>((IEnumerable<AbilityData>) currentUnit.BattleAbilitys);
            List<AbilityData> abilityDataList2 = new List<AbilityData>();
            for (int index = 0; index < abilityDataList1.Count; ++index)
            {
              if (abilityDataList1[index].IsNoneCategory && abilityDataList1[index].AbilityType == EAbilityType.Active)
              {
                abilityDataList2.Add(abilityDataList1[index]);
                abilityDataList1.RemoveAt(index--);
              }
              else if (abilityDataList1[index].AbilityType != EAbilityType.Active)
                abilityDataList1.RemoveAt(index--);
            }
            if (abilityDataList2.Count > 0)
              abilityDataList1.Add((AbilityData) AbilityData.ToMix(abilityDataList2.ToArray(), self.mBattleUI.CommandWindow.OtherSkillName, self.mBattleUI.CommandWindow.OtherSkillIconName));
            self.mBattleUI.CommandWindow.SetAbilities(abilityDataList1.ToArray(), currentUnit);
            self.ReflectUnitChgButton(currentUnit, false);
          }
          if (self.mTutorialTriggers != null)
          {
            TacticsUnitController unitController2 = self.FindUnitController(self.mBattle.CurrentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
            {
              for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
                self.mTutorialTriggers[index].OnFinishCameraUnitFocus(self.mBattle.CurrentUnit, unitController2.Unit.TurnCount);
            }
          }
        }
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTacticsUnits[index], (UnityEngine.Object) null))
            self.mTacticsUnits[index].UpdateBadStatus();
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventScript, (UnityEngine.Object) null))
        {
          TacticsUnitController unitController = self.FindUnitController(currentUnit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
            self.mEventSequence = self.mEventScript.OnUnitTurnStart(unitController, self.mIsFirstPlay);
        }
        self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_WaitFade<T> : State<SceneBattle> where T : State<SceneBattle>, new()
    {
      public override void Begin(SceneBattle self)
      {
        this.Update(self);
      }

      public override void Update(SceneBattle self)
      {
        if (GameUtility.IsScreenFading)
          return;
        self.GotoState<T>();
      }
    }

    private class State_WaitEvent<T> : State<SceneBattle> where T : State<SceneBattle>, new()
    {
      public override void Begin(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI, (UnityEngine.Object) null))
            self.mBattleUI.Hide();
          self.mUpdateCameraPosition = false;
          self.SetPrioritizedUnit((TacticsUnitController) null);
          MonoSingleton<GameManager>.Instance.EnableAnimationFrameSkipping = false;
          self.HideAllHPGauges();
          self.HideAllUnitOwnerIndex();
          for (int index = 0; index < self.mTacticsUnits.Count; ++index)
            self.mTacticsUnits[index].AutoUpdateRotation = false;
        }
        this.Update(self);
      }

      public override void End(SceneBattle self)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI, (UnityEngine.Object) null))
          return;
        self.mBattleUI.Show();
      }

      public override void Update(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null) && self.IsCameraMoving || !UnityEngine.Object.op_Equality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null) && self.mEventSequence.IsPlaying)
          return;
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          self.mTacticsUnits[index].AutoUpdateRotation = true;
        self.ResetCameraTarget();
        self.mUpdateCameraPosition = true;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mEventSequence, (UnityEngine.Object) null))
        {
          UnityEngine.Object.DestroyImmediate((UnityEngine.Object) ((Component) self.mEventSequence).get_gameObject());
          self.mEventSequence = (EventScript.Sequence) null;
        }
        MonoSingleton<GameManager>.Instance.EnableAnimationFrameSkipping = true;
        self.GotoState<T>();
      }
    }

    private class State_MapCommandV2 : State<SceneBattle>
    {
      private Unit mQueuedTarget;
      private UnitCommands.CommandTypes mQueuedCommand;
      private object mSelectedAbility;
      private bool mShouldRefreshCommands;
      private bool mMoved;
      private int mStartX;
      private int mStartY;
      private Unit mCurrentUnit;
      private int mHotTargets;
      private TacticsUnitController mCurrentController;

      public override void Begin(SceneBattle self)
      {
        this.mCurrentUnit = self.mBattle.CurrentUnit;
        this.mStartX = this.mCurrentUnit.x;
        this.mStartY = this.mCurrentUnit.y;
        self.InterpCameraTarget((Component) self.FindUnitController(this.mCurrentUnit));
        self.mTouchController.IgnoreCurrentTouch();
        this.mCurrentController = self.FindUnitController(this.mCurrentUnit);
        this.mCurrentController.ResetHPGauge();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
        {
          self.mBattleUI.CommandWindow.OnCommandSelect = new UnitCommands.CommandEvent(this.OnCommandSelect);
          self.mBattleUI.CommandWindow.OnUnitChgSelect = new UnitCommands.UnitChgEvent(this.OnUnitChgSelect);
        }
        self.m_AllowCameraRotation = true;
        self.m_AllowCameraTranslation = false;
        self.ShowAllHPGauges();
        if (!this.mCurrentUnit.IsUnitFlag(EUnitFlag.Moved))
          self.ShowWalkableGrids(self.CreateCurrentAccessMap(), 0);
        if (self.mBattle.CurrentUnit.IsUnitFlag(EUnitFlag.Moved))
          return;
        self.mMoveInput = (SceneBattle.MoveInput) new SceneBattle.VirtualStickInput();
        self.mMoveInput.SceneOwner = self;
        self.mMoveInput.OnAttackTargetSelect = new SceneBattle.MoveInput.TargetSelectEvent(this.OnTargetSelect);
        self.mMoveInput.Start();
      }

      private void CanNotMove()
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.MOVE_BLOCKED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }

      public override void End(SceneBattle self)
      {
        if (self.mMoveInput != null)
        {
          self.mMoveInput.End();
          self.mMoveInput = (SceneBattle.MoveInput) null;
        }
        self.m_AllowCameraRotation = false;
        self.m_AllowCameraTranslation = false;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.CommandWindow, (UnityEngine.Object) null))
          return;
        self.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
      }

      public override void Update(SceneBattle self)
      {
        if (self.mMoveInput != null)
        {
          self.mMoveInput.Update();
          self.ReflectUnitChgButton(self.mBattle.CurrentUnit, true);
          IntVector2 intVector2 = self.CalcCoord(this.mCurrentController.CenterPosition);
          if (!this.mMoved && (intVector2.x != this.mStartX || intVector2.y != this.mStartY))
          {
            TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && self.mTutorialTriggers != null)
            {
              for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
                self.mTutorialTriggers[index].OnUnitMoveStart(this.mCurrentUnit, unitController.Unit.TurnCount);
            }
            this.mMoved = true;
            if (self.Battle.IsMultiPlay)
              self.ExtentionMultiInputTime(true);
          }
          if (self.mNumHotTargets != this.mHotTargets)
          {
            TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) && self.mTutorialTriggers != null && self.mNumHotTargets > 0)
            {
              for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
                self.mTutorialTriggers[index].OnHotTargetsChange(this.mCurrentUnit, self.mHotTargets[0].Unit, unitController.Unit.TurnCount);
            }
            this.mHotTargets = self.mNumHotTargets;
          }
          self.OnGimmickUpdate();
        }
        if (!this.IsInputBusy)
        {
          if (!self.IsPause())
          {
            self.Battle.IsAutoBattle = self.Battle.RequestAutoBattle;
            if (self.Battle.IsAutoBattle)
            {
              TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
              {
                ((Component) unitController).get_transform().set_position(self.CalcGridCenter(self.mBattle.CurrentUnit.x, self.mBattle.CurrentUnit.y));
                self.InterpCameraTarget((Component) unitController);
              }
              self.HideAllHPGauges();
              self.HideAllUnitOwnerIndex();
              self.CloseBattleUI();
              self.GotoState_WaitSignal<SceneBattle.State_MapCommandAI>();
              return;
            }
          }
          if (this.mShouldRefreshCommands)
          {
            this.mShouldRefreshCommands = false;
            self.RefreshMapCommands();
          }
          if (this.mQueuedTarget != null)
          {
            if (!this.IsGridValid)
              this.CanNotMove();
            else
              this.SelectAttackTarget(this.mQueuedTarget);
            this.mQueuedTarget = (Unit) null;
          }
          else
          {
            if (this.mQueuedCommand == UnitCommands.CommandTypes.None)
              return;
            if (!this.IsGridValid)
              this.CanNotMove();
            else
              this.SelectCommand(this.mQueuedCommand, this.mSelectedAbility);
            this.mQueuedCommand = UnitCommands.CommandTypes.None;
          }
        }
        else
          this.mShouldRefreshCommands = true;
      }

      private bool IsInputBusy
      {
        get
        {
          if (this.self.mMoveInput != null)
            return this.self.mMoveInput.IsBusy;
          return false;
        }
      }

      private bool IsGridValid
      {
        get
        {
          return this.self.ApplyUnitMovement(true);
        }
      }

      private void OnTargetSelect(Unit target)
      {
        if (this.mQueuedTarget != null || this.mQueuedCommand != UnitCommands.CommandTypes.None)
          return;
        if (this.IsInputBusy)
          this.mQueuedTarget = target;
        else if (!this.IsGridValid)
          this.CanNotMove();
        else
          this.SelectAttackTarget(target);
      }

      private void SelectAttackTarget(Unit target)
      {
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        SkillData attackSkill = currentUnit.GetAttackSkill();
        TacticsUnitController unitController = this.self.FindUnitController(currentUnit);
        IntVector2 intVector2 = this.self.CalcCoord(unitController.CenterPosition);
        unitController.Unit.RefleshMomentBuff(this.self.mBattle.Units, true, intVector2.x, intVector2.y);
        GridMap<bool> selectGridMap = this.self.mBattle.CreateSelectGridMap(currentUnit, intVector2.x, intVector2.y, attackSkill);
        if (!selectGridMap.isValid(target.x, target.y) || !selectGridMap.get(target.x, target.y) || !this.self.mBattle.CheckSkillTarget(currentUnit, target, attackSkill))
          return;
        this.self.GotoSelectTarget(attackSkill, new SceneBattle.SelectTargetCallback(this.self.GotoMapCommand), new SceneBattle.SelectTargetPositionWithSkill(this.self.OnSelectAttackTarget), target, true);
      }

      private void SelectCommand(UnitCommands.CommandTypes command, object ability)
      {
        if (this.self.Battle.IsMultiPlay)
        {
          switch (command)
          {
            case UnitCommands.CommandTypes.Attack:
            case UnitCommands.CommandTypes.Ability:
              this.self.ExtentionMultiInputTime(false);
              break;
            case UnitCommands.CommandTypes.Map:
              this.self.ExtentionMultiInputTime(true);
              break;
          }
        }
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(currentUnit).CenterPosition);
        currentUnit.RefleshMomentBuff(this.self.mBattle.Units, true, intVector2.x, intVector2.y);
        if (command == UnitCommands.CommandTypes.Gimmick)
        {
          Grid current = this.self.mBattle.CurrentMap[intVector2.x, intVector2.y];
          if (!this.self.mBattle.CheckGridEventTrigger(currentUnit, current, EEventTrigger.ExecuteOnGrid))
            return;
        }
        this.self.mBattleUI.OnCommandSelect();
        switch (command - 1)
        {
          case UnitCommands.CommandTypes.None:
            this.self.GotoInputMovement();
            break;
          case UnitCommands.CommandTypes.Move:
            this.self.GotoSelectAttackTarget();
            break;
          case UnitCommands.CommandTypes.Attack:
            this.self.UIParam_CurrentAbility = (AbilityData) ability;
            this.self.GotoSkillSelect();
            break;
          case UnitCommands.CommandTypes.Ability:
            this.self.GotoItemSelect();
            break;
          case UnitCommands.CommandTypes.Item:
            this.self.GotoState_WaitSignal<SceneBattle.State_SelectGridEventV2>();
            break;
          case UnitCommands.CommandTypes.Gimmick:
            this.self.GotoMapViewMode();
            break;
          case UnitCommands.CommandTypes.Map:
            this.self.HideGrid();
            this.self.mBattleUI.OnInputDirectionStart();
            this.self.GotoState_WaitSignal<SceneBattle.State_InputDirection>();
            break;
        }
      }

      private void OnCommandSelect(UnitCommands.CommandTypes command, object ability)
      {
        if (this.mQueuedTarget != null)
          return;
        if (this.IsInputBusy)
        {
          this.mQueuedCommand = command;
          this.mSelectedAbility = ability;
        }
        else if (!this.IsGridValid)
          this.CanNotMove();
        else
          this.SelectCommand(command, ability);
      }

      private void OnUnitChgSelect()
      {
        this.self.ReflectUnitChgButton(this.self.mBattle.CurrentUnit, true);
        if (!this.self.mBattleUI.CommandWindow.IsEnableUnitChgButton || !this.self.mBattleUI.CommandWindow.IsActiveUnitChgButton)
          return;
        this.self.mBattleUI.OnCommandSelect();
        this.self.GotoUnitChgSelect(false);
      }
    }

    private class State_SelectItemV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.ItemWindow, (UnityEngine.Object) null))
          self.mBattleUI.ItemWindow.OnSelectItem = new BattleInventory.SelectEvent(this.OnSelectItem);
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.StepToNear(self.Battle.CurrentUnit);
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.ItemWindow, (UnityEngine.Object) null))
          self.mBattleUI.ItemWindow.OnSelectItem = (BattleInventory.SelectEvent) null;
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnSelectItem(ItemData itemData)
      {
        this.self.mBattleUI.OnItemSelectEnd();
        this.self.GotoSelectTarget(itemData, new SceneBattle.SelectTargetCallback(this.self.GotoItemSelect), new SceneBattle.SelectTargetPositionWithItem(this.self.OnSelectItemTarget), (Unit) null, true);
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
          return;
        this.self.mBattleUI.OnItemSelectEnd();
        this.self.GotoMapCommand();
      }
    }

    private class State_SelectUnitChgV2 : State<SceneBattle>
    {
      private List<Unit> mTargets = new List<Unit>(2);
      private Unit mUnitChgTo;

      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        this.mTargets.Clear();
        using (List<Unit>.Enumerator enumerator = self.Battle.Player.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            Unit current = enumerator.Current;
            if (!self.Battle.StartingMembers.Contains(current) && !current.IsDead && (current.IsEntry && current.IsSub))
              this.mTargets.Add(current);
          }
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.UnitChgWindow, (UnityEngine.Object) null))
          self.mBattleUI.UnitChgWindow.OnSelectUnit = new BattleUnitChg.SelectEvent(this.OnSelectUnit);
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.StepToNear(self.Battle.CurrentUnit);
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.TargetSub, (UnityEngine.Object) null))
        {
          self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
          self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mBattleUI.UnitChgWindow, (UnityEngine.Object) null))
          self.mBattleUI.UnitChgWindow.OnSelectUnit = (BattleUnitChg.SelectEvent) null;
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnSelectUnit(Unit unit_chg_to)
      {
        this.self.mBattleUI.OnUnitChgSelectEnd();
        this.mUnitChgTo = unit_chg_to;
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        TacticsUnitController unitController = this.self.FindUnitController(currentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null))
        {
          this.self.HideUnitMarkers(false);
          this.self.ShowUnitMarker(currentUnit, SceneBattle.UnitMarkerTypes.Target);
          unitController.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Change, (SkillData) null, (Unit) null);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          {
            this.self.mBattleUI.TargetMain.ResetHpGauge(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp);
            this.self.mBattleUI.TargetMain.SetNoAction(currentUnit, (List<LogSkill.Target.CondHit>) null);
            this.self.mBattleUI.TargetMain.Open();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          {
            this.self.mBattleUI.TargetSub.ResetHpGauge(unit_chg_to.Side, (int) unit_chg_to.CurrentStatus.param.hp, (int) unit_chg_to.MaximumStatus.param.hp);
            if (this.mTargets.Count > 1)
            {
              this.self.mBattleUI.TargetSub.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
              this.self.mBattleUI.TargetSub.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
            }
            else
            {
              this.self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
              this.self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
            }
            this.self.mBattleUI.TargetSub.SetNoAction(unit_chg_to, (List<LogSkill.Target.CondHit>) null);
            this.self.mBattleUI.TargetSub.Open();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetObjectSub.Close();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetTrickSub.Close();
          this.self.mBattleUI.OnVersusStart();
        }
        this.self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        this.self.mBattleUI.OnUnitChgConfirmStart();
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
          return;
        this.self.mBattleUI.OnUnitChgSelectEnd();
        this.self.HideGrid();
        this.self.GotoMapCommand();
      }

      private void ShiftTarget(int delta)
      {
        if (this.mTargets.Count == 0 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          return;
        int num = this.mTargets.IndexOf(this.mUnitChgTo);
        if (num < 0)
          num = 0;
        this.mUnitChgTo = this.mTargets[(num + delta + this.mTargets.Count) % this.mTargets.Count];
        this.self.mBattleUI.TargetSub.ResetHpGauge(this.mUnitChgTo.Side, (int) this.mUnitChgTo.CurrentStatus.param.hp, (int) this.mUnitChgTo.MaximumStatus.param.hp);
        this.self.mBattleUI.TargetSub.SetNoAction(this.mUnitChgTo, (List<LogSkill.Target.CondHit>) null);
        this.self.mBattleUI.TargetSub.Open();
      }

      private void OnNextTargetClick(GameObject go)
      {
        this.ShiftTarget(1);
      }

      private void OnPrevTargetClick(GameObject go)
      {
        this.ShiftTarget(-1);
      }

      private void OnYesNoSelect(bool yes)
      {
        this.self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        this.self.mBattleUI.OnUnitChgConfirmEnd();
        Unit currentUnit = this.self.Battle.CurrentUnit;
        Unit mUnitChgTo = this.mUnitChgTo;
        TacticsUnitController unitController1 = this.self.FindUnitController(currentUnit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
        {
          this.self.HideUnitMarkers(UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController1));
          unitController1.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          this.self.mBattleUI.TargetMain.ForceClose(true);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
        {
          this.self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
          this.self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
          this.self.mBattleUI.TargetSub.ForceClose(true);
        }
        this.self.mBattleUI.OnVersusEnd();
        if (yes)
        {
          this.self.HideGrid();
          if (currentUnit != null)
          {
            IntVector2 intVector2 = new IntVector2(currentUnit.x, currentUnit.y);
            EUnitDirection dir = currentUnit.Direction;
            TacticsUnitController unitController2 = this.self.FindUnitController(currentUnit);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
            {
              intVector2 = this.self.CalcCoord(unitController2.CenterPosition);
              dir = unitController2.CalcUnitDirectionFromRotation();
            }
            this.self.Battle.UnitChange(currentUnit, intVector2.x, intVector2.y, dir, mUnitChgTo);
          }
          this.self.Battle.MapCommandEnd(this.self.Battle.CurrentUnit);
          this.self.Battle.CommandWait(true);
          this.self.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
        }
        else
          this.self.GotoUnitChgSelect(true);
      }
    }

    private struct TargetSelectorParam
    {
      public ItemData Item;
      public SkillData Skill;
      public SceneBattle.SelectTargetCallback OnCancel;
      public object OnAccept;
      public Unit DefaultTarget;
      public bool AllowTargetChange;
      public bool IsThrowTargetSelect;
      public Unit DefaultThrowTarget;
      public Unit ThrowTarget;
    }

    private class State_PreThrowTargetSelect : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.UIParam_TargetValid = false;
        self.mBattleUI.OnThrowTargetSelectStart();
        self.GotoState_WaitSignal<SceneBattle.State_ThrowTargetSelect>();
      }
    }

    private class State_ThrowTargetSelect : State<SceneBattle>
    {
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>(4);
      private const int THROW_TARGET_RANGE = 1;
      private TacticsUnitController mSelectedTarget;
      private GridMap<bool> mTargetGrids;
      private IntVector2 mTargetPosition;
      private bool mDragScroll;
      private float mYScrollPos;
      private bool mIgnoreDragVelocity;
      private float mDragY;

      public override void Begin(SceneBattle self)
      {
        bool flag = true;
        Unit currentUnit = self.mBattle.CurrentUnit;
        TacticsUnitController unitController1 = self.FindUnitController(currentUnit);
        IntVector2 intVector2 = self.CalcCoord(unitController1.CenterPosition);
        this.mTargetGrids = new GridMap<bool>(self.Battle.CurrentMap.Width, self.Battle.CurrentMap.Height);
        Grid current1 = self.Battle.CurrentMap[intVector2.x, intVector2.y];
        self.Battle.CreateGridMapCross(current1, 0, 1, ref this.mTargetGrids);
        int throwHeight = (int) MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.ThrowHeight;
        for (int x = 0; x < this.mTargetGrids.w; ++x)
        {
          for (int y = 0; y < this.mTargetGrids.h; ++y)
          {
            if (this.mTargetGrids.get(x, y))
            {
              Grid current2 = self.Battle.CurrentMap[x, y];
              if (current2 != null)
              {
                if (current2.geo != null && (bool) current2.geo.DisableStopped)
                  this.mTargetGrids.set(x, y, false);
                if (Math.Abs(current1.height - current2.height) > throwHeight)
                  this.mTargetGrids.set(x, y, false);
              }
            }
          }
        }
        Color32 src = Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea);
        GridMap<Color32> grid = new GridMap<Color32>(this.mTargetGrids.w, this.mTargetGrids.h);
        for (int x = 0; x < this.mTargetGrids.w; ++x)
        {
          for (int y = 0; y < this.mTargetGrids.h; ++y)
          {
            if (this.mTargetGrids.get(x, y))
              grid.set(x, y, src);
          }
        }
        self.mTacticsSceneRoot.ShowGridLayer(1, grid, true);
        this.mTargets.Clear();
        for (int x = 0; x < self.Battle.CurrentMap.Width; ++x)
        {
          for (int y = 0; y < self.Battle.CurrentMap.Height; ++y)
          {
            if (this.mTargetGrids.get(x, y))
            {
              Unit unit = self.Battle.FindUnitAtGrid(self.Battle.CurrentMap[x, y]);
              if (unit == null)
              {
                unit = self.Battle.FindGimmickAtGrid(self.Battle.CurrentMap[x, y], false, (Unit) null);
                if (unit == null || !unit.IsBreakObj)
                  continue;
              }
              if (unit != currentUnit && unit.IsThrow && (unit.IsNormalSize && !unit.IsJump))
              {
                TacticsUnitController unitController2 = self.FindUnitController(unit);
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController2))
                  this.mTargets.Add(unitController2);
              }
            }
          }
        }
        if (this.mTargets.Count > 0)
        {
          self.mTargetSelectorParam.DefaultThrowTarget = this.mTargets[0].Unit;
          flag = false;
        }
        self.mOnUnitClick = new SceneBattle.UnitClickEvent(this.OnClickUnit);
        self.mOnUnitFocus = new SceneBattle.UnitFocusEvent(this.OnFocus);
        self.m_AllowCameraRotation = flag;
        self.m_AllowCameraTranslation = flag;
        if (self.mTargetSelectorParam.DefaultThrowTarget != null)
        {
          TacticsUnitController unitController2 = self.FindUnitController(self.mTargetSelectorParam.DefaultThrowTarget);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
            this.OnFocus(unitController2);
          self.InterpCameraTarget((Component) unitController2);
          this.SetYesButtonEnable(true);
        }
        self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mBattleUI.TargetSub))
        {
          if (this.mTargets.Count >= 2)
          {
            self.mBattleUI.TargetSub.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
            self.mBattleUI.TargetSub.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
          }
          else
          {
            self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
            self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
          }
        }
        self.StepToNear(self.Battle.CurrentUnit);
        self.OnGimmickUpdate();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mDragScroll)
          return;
        this.mYScrollPos += (float) ((self.mTouchController.DragDelta.y <= 0.0 ? -1.0 : 1.0) * (double) Time.get_unscaledDeltaTime() * 2.0);
        if (!this.mIgnoreDragVelocity)
          this.mYScrollPos += this.mDragY / 20f;
        if ((double) this.mYScrollPos <= -1.0)
        {
          this.mYScrollPos = 0.0f;
          this.mIgnoreDragVelocity = true;
          this.ShiftTarget(-1);
        }
        else
        {
          if ((double) this.mYScrollPos < 1.0)
            return;
          this.mYScrollPos = 0.0f;
          this.mIgnoreDragVelocity = true;
          this.ShiftTarget(1);
        }
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mBattleUI.TargetSub))
        {
          self.mBattleUI.TargetSub.DeactivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
          self.mBattleUI.TargetSub.DeactivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
        }
        this.SetYesButtonEnable(true);
        self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
        self.HideUnitMarkers(false);
        self.m_AllowCameraRotation = false;
        self.m_AllowCameraTranslation = false;
        self.mOnUnitFocus = (SceneBattle.UnitFocusEvent) null;
        self.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
        self.mOnGridClick = (SceneBattle.GridClickEvent) null;
        self.HideGrid();
        self.SetUnitUiHeight(self.mBattle.CurrentUnit);
      }

      private void SetYesButtonEnable(bool enable)
      {
        Selectable component = (Selectable) this.self.mBattleUI.CommandWindow.OKButton.GetComponent<Selectable>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.set_interactable(enable);
      }

      private void OnYesNoSelect(bool yes)
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue, 0);
        }
        this.self.mBattleUI.OnThrowTargetSelectEnd();
        if (yes)
        {
          this.self.mTargetSelectorParam.ThrowTarget = this.mSelectedTarget.Unit;
          this.self.mBattleUI.HideTargetWindows();
          this.self.GotoState_WaitSignal<SceneBattle.State_PreSelectTargetV2>();
        }
        else
        {
          this.self.mTargetSelectorParam.OnCancel();
          this.self.mBattleUI.HideTargetWindows();
        }
      }

      private bool IsGridSelectable(int x, int y)
      {
        if (this.mTargetGrids == null)
          return false;
        bool flag = this.mTargetGrids.get(x, y);
        if (flag)
        {
          Unit unit = this.self.Battle.FindUnitAtGrid(this.self.Battle.CurrentMap[x, y]);
          if (unit == null)
          {
            unit = this.self.Battle.FindGimmickAtGrid(this.self.Battle.CurrentMap[x, y], false, (Unit) null);
            if (unit != null && !unit.IsBreakObj)
              unit = (Unit) null;
          }
          if (unit == null || unit == this.self.mBattle.CurrentUnit || (!unit.IsThrow || !unit.IsNormalSize) || unit.IsJump)
            flag = false;
        }
        return flag;
      }

      private bool IsGridSelectable(Unit unit)
      {
        if (unit == null)
          return false;
        int x = unit.x;
        int y = unit.y;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mFocusedUnit, (UnityEngine.Object) null) && this.self.mFocusedUnit.Unit != null && this.self.mFocusedUnit.Unit == unit)
        {
          IntVector2 intVector2 = this.self.CalcCoord(this.self.mFocusedUnit.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
        return this.IsGridSelectable(x, y);
      }

      private void OnFocus(TacticsUnitController controller)
      {
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) controller) || controller.Unit.IsGimmick && !this.self.Battle.IsTargetBreakUnit(this.self.mBattle.CurrentUnit, controller.Unit, (SkillData) null))
          return;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedTarget, (UnityEngine.Object) null))
          this.self.HideUnitMarkers(this.mSelectedTarget.Unit);
        this.self.ShowUnitMarker(controller.Unit, SceneBattle.UnitMarkerTypes.Target);
        this.mSelectedTarget = controller;
        IntVector2 intVector2 = this.self.CalcCoord(controller.CenterPosition);
        this.mTargetPosition.x = intVector2.x;
        this.mTargetPosition.y = intVector2.y;
        this.self.mSelectedTarget = controller.Unit;
        this.self.UIParam_TargetValid = false;
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        TacticsUnitController unitController = this.self.FindUnitController(currentUnit);
        int YosokuDamageHp = skill == null ? 0 : skill.GetHpCost(currentUnit);
        unitController.SetHPChangeYosou((int) currentUnit.CurrentStatus.param.hp - YosokuDamageHp, 0);
        this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp, YosokuDamageHp, 0, false);
        this.self.mBattleUI.TargetMain.UpdateHpGauge();
        this.self.mBattleUI.TargetSub.ResetHpGauge(this.self.mSelectedTarget.Side, (int) this.self.mSelectedTarget.CurrentStatus.param.hp, (int) this.self.mSelectedTarget.MaximumStatus.param.hp);
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          if (0 >= YosokuDamageHp || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.self.mTacticsUnits[index], (UnityEngine.Object) controller))
            this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue, 0);
        }
        if (this.IsGridSelectable(this.self.mSelectedTarget))
        {
          this.self.UIParam_TargetValid = true;
          this.mSelectedTarget.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Target, (SkillData) null, (Unit) null);
          GridMap<bool> grid = new GridMap<bool>(this.self.Battle.CurrentMap.Width, this.self.Battle.CurrentMap.Height);
          grid.set(this.mTargetPosition.x, this.mTargetPosition.y, true);
          this.self.mTacticsSceneRoot.ShowGridLayer(2, grid, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea2), false);
        }
        this.SetYesButtonEnable(this.self.UIParam_TargetValid);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
        {
          this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit, (List<LogSkill.Target.CondHit>) null);
          this.self.mBattleUI.TargetMain.Open();
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
        {
          if (this.self.UIParam_TargetValid)
          {
            this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget, (List<LogSkill.Target.CondHit>) null);
            this.self.mBattleUI.TargetSub.Open();
          }
          else if (this.self.mSelectedTarget.UnitType == EUnitType.Unit || this.self.mSelectedTarget.UnitType == EUnitType.EventUnit || this.self.mSelectedTarget.IsBreakObj && this.self.mSelectedTarget.IsBreakDispUI)
          {
            this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget, (List<LogSkill.Target.CondHit>) null);
            this.self.mBattleUI.TargetSub.Open();
          }
          else
            this.self.mBattleUI.TargetSub.ForceClose(false);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          this.self.mBattleUI.TargetObjectSub.Close();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          this.self.mBattleUI.TargetTrickSub.Close();
        this.self.OnGimmickUpdate();
        this.self.SetUnitUiHeight(controller.Unit);
      }

      private void OnClickUnit(TacticsUnitController controller)
      {
        this.self.InterpCameraTarget((Component) controller);
        this.self.mFocusedUnit = controller;
        this.OnFocus(controller);
      }

      private void ShiftTarget(int delta)
      {
        if (this.mTargets.Count == 0)
          return;
        int num = this.mTargets.IndexOf(this.self.mFocusedUnit);
        if (num < 0)
          num = 0;
        this.OnClickUnit(this.mTargets[(num + delta + this.mTargets.Count) % this.mTargets.Count]);
      }

      private void OnNextTargetClick(GameObject go)
      {
        this.ShiftTarget(1);
      }

      private void OnPrevTargetClick(GameObject go)
      {
        this.ShiftTarget(-1);
      }

      private void OnDrag()
      {
        if (!this.mIgnoreDragVelocity)
          this.mDragY += (float) this.self.mTouchController.DragDelta.y;
        if (this.mTargets.Count == 0)
          return;
        this.mDragScroll = true;
      }

      private void OnDragEnd()
      {
        this.mDragY = 0.0f;
        this.mYScrollPos = 0.0f;
        this.mDragScroll = false;
        this.mIgnoreDragVelocity = false;
      }
    }

    private class State_PreSelectTargetV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.UIParam_TargetValid = false;
        self.mBattleUI.OnVersusStart();
        self.mBattleUI.OnTargetSelectStart();
        self.GotoState_WaitSignal<SceneBattle.State_SelectTargetV2>();
      }
    }

    private class State_PreMapviewV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.UIParam_TargetValid = false;
        self.mBattleUI.OnMapStart();
        self.GotoState_WaitSignal<SceneBattle.State_SelectTargetV2>();
      }
    }

    private class State_SelectTargetV2 : State<SceneBattle>
    {
      private bool mSelectGrid = true;
      private List<TacticsUnitController> mTargets = new List<TacticsUnitController>(32);
      private SceneBattle mScene;
      private TacticsUnitController mSelectedTarget;
      private GridMap<bool> mTargetGrids;
      private GridMap<bool> mTargetAreaGridMap;
      private IntVector2 mTargetPosition;
      private bool mDragScroll;
      private float mYScrollPos;
      private bool mIgnoreDragVelocity;
      private float mDragY;
      private GUIEventListener mGUIEvent;

      public override void Begin(SceneBattle self)
      {
        bool flag1 = true;
        this.mScene = self;
        SkillData skill = self.mTargetSelectorParam.Skill;
        self.mSelectedTarget = self.Battle.CurrentUnit;
        Unit currentUnit = self.mBattle.CurrentUnit;
        TacticsUnitController unitController1 = self.FindUnitController(currentUnit);
        IntVector2 intVector2_1 = self.CalcCoord(unitController1.CenterPosition);
        this.mTargets.Clear();
        if (skill != null)
        {
          this.mSelectGrid = skill.SkillParam.IsAreaSkill();
          if (skill.IsTargetGridNoUnit || skill.IsTargetValidGrid)
            this.mSelectGrid = true;
          this.mTargetGrids = self.Battle.CreateSelectGridMap(currentUnit, intVector2_1.x, intVector2_1.y, self.mTargetSelectorParam.Skill);
          if (!this.mSelectGrid)
          {
            int x = currentUnit.x;
            int y = currentUnit.y;
            currentUnit.x = intVector2_1.x;
            currentUnit.y = intVector2_1.y;
            List<Unit> attackTargetsAi = self.mBattle.CreateAttackTargetsAI(currentUnit, self.mTargetSelectorParam.Skill, false);
            if (attackTargetsAi.Count > 0)
            {
              for (int index = 0; index < attackTargetsAi.Count; ++index)
              {
                TacticsUnitController unitController2 = self.FindUnitController(attackTargetsAi[index]);
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null) && this.mTargetGrids.get(attackTargetsAi[index].x, attackTargetsAi[index].y) && (!unitController2.Unit.IsBreakObj || unitController2.Unit.IsBreakDispUI))
                  this.mTargets.Add(unitController2);
              }
              flag1 = false;
            }
            currentUnit.x = x;
            currentUnit.y = y;
          }
          Color32 src = Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea);
          GridMap<Color32> grid = new GridMap<Color32>(this.mTargetGrids.w, this.mTargetGrids.h);
          for (int x = 0; x < this.mTargetGrids.w; ++x)
          {
            for (int y = 0; y < this.mTargetGrids.h; ++y)
            {
              if (this.mTargetGrids.get(x, y))
                grid.set(x, y, src);
            }
          }
          bool flag2 = false;
          if (self.mTargetSelectorParam.DefaultTarget == null)
          {
            Unit unit = (Unit) null;
            switch (skill.SkillParam.target)
            {
              case ESkillTarget.Self:
              case ESkillTarget.SelfSide:
                unit = self.FindTarget(currentUnit, skill, this.mTargetGrids, EUnitSide.Player);
                break;
              case ESkillTarget.EnemySide:
                unit = self.FindTarget(currentUnit, skill, this.mTargetGrids, EUnitSide.Enemy);
                break;
              case ESkillTarget.UnitAll:
              case ESkillTarget.NotSelf:
                unit = self.FindTarget(currentUnit, skill, this.mTargetGrids, EUnitSide.Enemy);
                if (unit == null)
                {
                  unit = self.FindTarget(currentUnit, skill, this.mTargetGrids, EUnitSide.Player);
                  if (unit != null && !skill.SkillParam.IsSelfTargetSelect() && (unit.x == currentUnit.x && unit.y == currentUnit.y))
                    unit = (Unit) null;
                }
                if (unit == null)
                {
                  unit = self.FindTarget(currentUnit, skill, this.mTargetGrids, EUnitSide.Neutral);
                  break;
                }
                break;
              case ESkillTarget.GridNoUnit:
              case ESkillTarget.ValidGrid:
                unit = currentUnit;
                break;
            }
            self.mTargetSelectorParam.DefaultTarget = unit;
            flag2 = unit != null;
          }
          self.mTacticsSceneRoot.ShowGridLayer(1, grid, true);
          self.ShowCastSkill();
          if (self.mTargetSelectorParam.AllowTargetChange && !flag2 && (self.mTargetSelectorParam.DefaultTarget == null || !self.mTargetSelectorParam.DefaultTarget.IsBreakObj || self.mTargetSelectorParam.DefaultTarget.IsBreakDispUI))
            this.OnFocusGrid(self.Battle.CurrentMap[intVector2_1.x, intVector2_1.y]);
        }
        else
        {
          int x = currentUnit.x;
          int y = currentUnit.y;
          currentUnit.x = intVector2_1.x;
          currentUnit.y = intVector2_1.y;
          GridMap<int> moveMap = self.mBattle.CreateMoveMap(currentUnit, 0);
          currentUnit.x = x;
          currentUnit.y = y;
          self.ShowWalkableGrids(moveMap, 0);
          this.HilitNormalAttack(self.mBattle.CurrentUnit, false);
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_MapDistance);
          this.OnFocusGrid(self.Battle.CurrentMap[self.mSelectedTarget.x, self.mSelectedTarget.y]);
          using (List<Unit>.Enumerator enumerator = self.mBattle.Units.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Unit current = enumerator.Current;
              if (!current.IsGimmick || current.IsBreakObj)
              {
                TacticsUnitController unitController2 = self.FindUnitController(current);
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitController2))
                  this.mTargets.Add(unitController2);
              }
            }
          }
        }
        if (self.mTargetSelectorParam.AllowTargetChange)
        {
          if (this.mSelectGrid)
          {
            self.mOnGridClick = new SceneBattle.GridClickEvent(this.OnClickGrid);
            this.mTargetPosition.x = Mathf.FloorToInt((float) self.m_CameraPosition.x);
            this.mTargetPosition.y = Mathf.FloorToInt((float) self.m_CameraPosition.y);
          }
          else
          {
            self.mOnUnitClick = new SceneBattle.UnitClickEvent(this.OnClickUnit);
            self.mOnUnitFocus = new SceneBattle.UnitFocusEvent(this.OnFocus);
          }
          self.m_AllowCameraRotation = flag1;
          self.m_AllowCameraTranslation = flag1;
        }
        else
        {
          self.m_AllowCameraRotation = false;
          self.m_AllowCameraTranslation = false;
        }
        if (self.mTargetSelectorParam.DefaultTarget != null)
        {
          TacticsUnitController unitController2 = self.FindUnitController(self.mTargetSelectorParam.DefaultTarget);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
          {
            this.OnFocus(unitController2);
            IntVector2 intVector2_2 = self.CalcCoord(unitController2.CenterPosition);
            Grid current = self.Battle.CurrentMap[intVector2_2.x, intVector2_2.y];
            this.HilitArea(current.x, current.y);
          }
          self.InterpCameraTarget((Component) unitController2);
          bool flag2 = true;
          if (skill.IsTargetGridNoUnit)
            flag2 = false;
          else if (skill.IsTargetValidGrid)
          {
            self.mFocusedUnit = unitController2;
            self.mMapModeFocusedUnit = self.mFocusedUnit;
            flag2 = this.IsGridSelectable(self.mTargetSelectorParam.DefaultTarget);
          }
          if (skill.IsTargetTeleport && self.mTargetSelectorParam.DefaultTarget != null)
          {
            bool is_teleport = false;
            self.mBattle.GetTeleportGrid(currentUnit, intVector2_1.x, intVector2_1.y, self.mTargetSelectorParam.DefaultTarget, skill, ref is_teleport);
            if (!is_teleport)
              flag2 = false;
          }
          self.UIParam_TargetValid = flag2;
          this.SetYesButtonEnable(self.UIParam_TargetValid);
        }
        self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mBattleUI.CommandWindow.OnMapExitSelect += new UnitCommands.MapExitEvent(this.OnMapExitSelect);
        self.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mBattleUI.TargetSub))
        {
          if (this.mTargets.Count >= 2)
          {
            self.mBattleUI.TargetSub.ActivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
            self.mBattleUI.TargetSub.ActivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
          }
          else
          {
            self.mBattleUI.TargetSub.SetNextTargetArrowActive(false);
            self.mBattleUI.TargetSub.SetPrevTargetArrowActive(false);
          }
        }
        self.StepToNear(self.Battle.CurrentUnit);
        self.OnGimmickUpdate();
      }

      public override void End(SceneBattle self)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) self.mBattleUI.TargetSub))
        {
          self.mBattleUI.TargetSub.DeactivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
          self.mBattleUI.TargetSub.DeactivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGUIEvent, (UnityEngine.Object) null))
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this.mGUIEvent);
          this.mGUIEvent = (GUIEventListener) null;
        }
        this.SetYesButtonEnable(true);
        self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mBattleUI.CommandWindow.OnMapExitSelect -= new UnitCommands.MapExitEvent(this.OnMapExitSelect);
        self.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
        self.HideUnitMarkers(false);
        self.m_AllowCameraRotation = false;
        self.m_AllowCameraTranslation = false;
        self.mOnUnitFocus = (SceneBattle.UnitFocusEvent) null;
        self.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
        self.mOnGridClick = (SceneBattle.GridClickEvent) null;
        SkillData skill = self.mTargetSelectorParam.Skill;
        if (skill != null && skill.IsTargetTeleport)
        {
          self.mDisplayBlockedGridMarker = false;
          self.mGridDisplayBlockedGridMarker = (Grid) null;
        }
        if (self.mIsBackSelectSkill)
        {
          self.mTacticsSceneRoot.HideGridLayer(0);
          self.mTacticsSceneRoot.HideGridLayer(1);
          self.mTacticsSceneRoot.HideGridLayer(2);
        }
        else
          self.HideGrid();
        self.SetUnitUiHeight(self.mBattle.CurrentUnit);
      }

      private bool NeedsTargetKakunin(Unit unit)
      {
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        if (skill != null && skill.IsCastSkill() && skill.IsEnableUnitLockTarget())
        {
          Grid current = this.mScene.Battle.CurrentMap[this.mTargetPosition.x, this.mTargetPosition.y];
          IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(unit).CenterPosition);
          int x = unit.x;
          int y = unit.y;
          unit.x = intVector2.x;
          unit.y = intVector2.y;
          Unit unitAtGrid = this.mScene.Battle.FindUnitAtGrid(current);
          unit.x = x;
          unit.y = y;
          if (this.mScene.Battle.CheckSkillTarget(this.self.Battle.CurrentUnit, unitAtGrid, skill))
            return true;
        }
        return false;
      }

      private void OnNextTargetClick(GameObject go)
      {
        this.ShiftTarget(1);
      }

      private void OnPrevTargetClick(GameObject go)
      {
        this.ShiftTarget(-1);
      }

      private void OnDragEnd()
      {
        this.mDragY = 0.0f;
        this.mYScrollPos = 0.0f;
        this.mDragScroll = false;
        this.mIgnoreDragVelocity = false;
      }

      private void OnDrag()
      {
        if (!this.mIgnoreDragVelocity)
          this.mDragY += (float) this.self.mTouchController.DragDelta.y;
        if (this.mTargets.Count == 0)
          return;
        this.mDragScroll = true;
      }

      private void SetYesButtonEnable(bool enable)
      {
        Selectable component = (Selectable) this.self.mBattleUI.CommandWindow.OKButton.GetComponent<Selectable>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        component.set_interactable(enable);
      }

      private void OnYesNoSelect(bool yes)
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue, 0);
        }
        bool flag1 = false;
        if (yes)
        {
          if (this.self.UIParam_TargetValid)
          {
            if (this.self.mTargetSelectorParam.Item != null)
            {
              this.self.mBattleUI.OnTargetSelectEnd();
              ((SceneBattle.SelectTargetPositionWithItem) this.self.mTargetSelectorParam.OnAccept)(this.mTargetPosition.x, this.mTargetPosition.y, this.self.mTargetSelectorParam.Item);
              this.self.mBattleUI.HideTargetWindows();
            }
            else
            {
              Unit currentUnit1 = this.self.Battle.CurrentUnit;
              bool flag2 = false;
              if (this.self.mTargetSelectorParam.Skill.EffectType == SkillEffectTypes.Changing)
              {
                IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(currentUnit1).CenterPosition);
                if (currentUnit1.x == intVector2.x && currentUnit1.y == intVector2.y)
                  flag2 = true;
              }
              if (this.NeedsTargetKakunin(currentUnit1))
              {
                this.StartTargetKakunin();
                flag1 = true;
              }
              else
              {
                this.self.mBattleUI.OnTargetSelectEnd();
                bool bUnitTarget = false;
                SkillData skill = this.self.mTargetSelectorParam.Skill;
                if (skill != null && skill.IsForceUnitLock() && (int) skill.SkillParam.range_max == 0)
                  bUnitTarget = true;
                ((SceneBattle.SelectTargetPositionWithSkill) this.self.mTargetSelectorParam.OnAccept)(this.mTargetPosition.x, this.mTargetPosition.y, skill, bUnitTarget);
                this.self.mBattleUI.HideTargetWindows();
              }
              if (flag2)
              {
                this.self.mCurrentUnitStartX = currentUnit1.x;
                this.self.mCurrentUnitStartY = currentUnit1.y;
              }
              Unit currentUnit2 = this.self.Battle.CurrentUnit;
              this.self.mSkillDirectionByKouka = this.self.GetSkillDirectionByTargetArea(currentUnit2, currentUnit2.x, currentUnit2.y, this.mTargetAreaGridMap);
            }
          }
        }
        else
        {
          this.self.mBattleUI.OnTargetSelectEnd();
          if (this.self.mTargetSelectorParam.IsThrowTargetSelect)
            this.self.GotoState_WaitSignal<SceneBattle.State_PreThrowTargetSelect>();
          else
            this.self.mTargetSelectorParam.OnCancel();
          this.self.mBattleUI.HideTargetWindows();
        }
        if (flag1)
          return;
        this.self.mBattleUI.OnVersusEnd();
      }

      private void StartTargetKakunin()
      {
        this.self.mSkillTargetWindow.OnCancel = new SkillTargetWindow.CancelEvent(this.OnCancelTarget);
        this.self.mSkillTargetWindow.OnTargetSelect = new SkillTargetWindow.TargetSelectEvent(this.OnSelectTargetMode);
        this.self.mSkillTargetWindow.Show();
      }

      private void OnCancelTarget()
      {
        this.self.mSkillTargetWindow.Hide();
      }

      private void OnSelectTargetMode(bool targetIsGrid)
      {
        this.self.mSkillTargetWindow.Hide();
        this.self.mBattleUI.OnTargetSelectEnd();
        ((SceneBattle.SelectTargetPositionWithSkill) this.self.mTargetSelectorParam.OnAccept)(this.mTargetPosition.x, this.mTargetPosition.y, this.self.mTargetSelectorParam.Skill, !targetIsGrid);
        this.self.mBattleUI.HideTargetWindows();
        this.self.mBattleUI.OnVersusEnd();
      }

      private void OnMapExitSelect()
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue, 0);
        }
        this.self.mBattleUI.OnTargetSelectEnd();
        this.self.mTargetSelectorParam.OnCancel();
        this.self.mBattleUI.HideTargetWindows();
        this.self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        this.self.mBattleUI.OnMapViewEnd();
      }

      private void HilitArea(int x, int y)
      {
        if (this.self.mTargetSelectorParam.Skill == null)
          return;
        IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(this.self.mBattle.CurrentUnit).CenterPosition);
        this.mTargetAreaGridMap = this.self.mBattle.CreateScopeGridMap(this.self.mBattle.CurrentUnit, intVector2.x, intVector2.y, x, y, this.self.mTargetSelectorParam.Skill);
        if (SkillParam.IsTypeLaser(this.self.mTargetSelectorParam.Skill.SkillParam.select_scope) && !this.mTargetAreaGridMap.get(x, y))
          this.mTargetAreaGridMap.fill(false);
        this.self.mTacticsSceneRoot.ShowGridLayer(2, this.mTargetAreaGridMap, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea2), false);
      }

      private void HilitNormalAttack(Unit Attacker, bool showOnly = false)
      {
        if (!showOnly)
        {
          SkillData attackSkill = Attacker.GetAttackSkill();
          IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(Attacker).CenterPosition);
          this.mTargetAreaGridMap = this.self.mBattle.CreateSelectGridMap(Attacker, intVector2.x, intVector2.y, attackSkill);
        }
        this.self.mTacticsSceneRoot.ShowGridLayer(1, this.mTargetAreaGridMap, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea), true);
        this.self.ShowCastSkill();
      }

      private void OnClickGrid(Grid grid)
      {
        this.self.InterpCameraTarget(this.self.CalcGridCenter(grid));
        this.OnFocusGrid(grid);
      }

      private void OnClickUnit(TacticsUnitController controller)
      {
        this.self.InterpCameraTarget((Component) controller);
        this.self.mFocusedUnit = controller;
        this.self.mMapModeFocusedUnit = this.self.mFocusedUnit;
        this.OnFocus(controller);
      }

      private void OnFocusGrid(Grid grid)
      {
        Unit currentUnit = this.self.mBattle.CurrentUnit;
        TacticsUnitController unitController1 = this.self.FindUnitController(currentUnit);
        IntVector2 intVector2_1;
        intVector2_1.x = currentUnit.x;
        intVector2_1.y = currentUnit.y;
        IntVector2 intVector2_2 = this.self.CalcCoord(unitController1.CenterPosition);
        currentUnit.x = intVector2_2.x;
        currentUnit.y = intVector2_2.y;
        this.HilitArea(grid.x, grid.y);
        Unit unitAtGrid = this.self.mBattle.FindUnitAtGrid(grid);
        if (unitAtGrid != null)
        {
          TacticsUnitController unitController2 = this.self.FindUnitController(unitAtGrid);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
          {
            this.self.mFocusedUnit = unitController2;
            this.self.mMapModeFocusedUnit = this.self.mFocusedUnit;
            this.OnFocus(unitController2);
          }
        }
        else if (this.self.mTargetSelectorParam.Skill != null && this.self.mTargetSelectorParam.Skill.IsAllEffect())
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
          {
            this.self.mFocusedUnit = unitController1;
            this.self.mMapModeFocusedUnit = this.self.mFocusedUnit;
            this.OnFocus(unitController1);
          }
        }
        else
        {
          this.mSelectedTarget = (TacticsUnitController) null;
          this.self.UIParam_TargetValid = false;
          for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
          {
            this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
            this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue, 0);
          }
          SkillData skill = this.self.mTargetSelectorParam.Skill;
          Unit unit1 = (Unit) null;
          if (skill != null)
          {
            int hpCost = skill.GetHpCost(currentUnit);
            unitController1.SetHPChangeYosou((int) currentUnit.CurrentStatus.param.hp - hpCost, 0);
            this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp, hpCost, 0, false);
            this.self.mBattleUI.TargetMain.UpdateHpGauge();
            if (this.self.mTargetSelectorParam.OnAccept != null && this.mTargetGrids.get(grid.x, grid.y))
            {
              bool flag = true;
              if (skill.IsTargetGridNoUnit)
              {
                Unit unit2 = this.self.mBattle.FindUnitAtGrid(grid);
                if (unit2 == null)
                {
                  unit2 = this.self.mBattle.FindGimmickAtGrid(grid, false, (Unit) null);
                  if (unit2 != null && !unit2.IsBreakObj)
                    unit2 = (Unit) null;
                }
                flag = unit2 == null;
              }
              if (flag)
              {
                BattleCore.CommandResult commandResult = this.self.mBattle.GetCommandResult(currentUnit, intVector2_2.x, intVector2_2.y, grid.x, grid.y, skill);
                if (commandResult != null && commandResult.targets != null)
                {
                  for (int index = 0; index < commandResult.targets.Count; ++index)
                  {
                    TacticsUnitController unitController2 = this.self.FindUnitController(commandResult.targets[index].unit);
                    if (commandResult.skill.IsDamagedSkill())
                    {
                      unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Attack, skill, currentUnit);
                      int newHP = (int) commandResult.targets[index].unit.CurrentStatus.param.hp - commandResult.targets[index].hp_damage;
                      int hpmax_damage = 0;
                      if (commandResult.skill.IsMhmDamage())
                      {
                        newHP = (int) commandResult.targets[index].unit.CurrentStatus.param.hp;
                        hpmax_damage = commandResult.targets[index].hp_damage;
                      }
                      unitController2.SetHPChangeYosou(newHP, hpmax_damage);
                      this.self.mBattleUI.TargetSub.SetHpGaugeParam(commandResult.targets[index].unit.Side, (int) commandResult.targets[index].unit.CurrentStatus.param.hp, (int) commandResult.targets[index].unit.MaximumStatus.param.hp, commandResult.targets[index].hp_damage, 0, commandResult.skill.IsMhmDamage());
                    }
                    else
                    {
                      unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Target, (SkillData) null, (Unit) null);
                      unitController2.SetHPChangeYosou((int) commandResult.targets[index].unit.CurrentStatus.param.hp + commandResult.targets[index].hp_heal, 0);
                      this.self.mBattleUI.TargetSub.SetHpGaugeParam(commandResult.targets[index].unit.Side, (int) commandResult.targets[index].unit.CurrentStatus.param.hp, (int) commandResult.targets[index].unit.MaximumStatus.param.hp, 0, commandResult.targets[index].hp_heal, false);
                    }
                    this.self.mBattleUI.TargetSub.UpdateHpGauge();
                    this.self.UIParam_TargetValid = true;
                  }
                }
              }
              if (skill.IsTargetGridNoUnit)
              {
                this.self.UIParam_TargetValid = flag;
                if (this.self.UIParam_TargetValid && skill.EffectType == SkillEffectTypes.SetTrick)
                {
                  TrickData trickData = TrickData.SearchEffect(grid.x, grid.y);
                  if (trickData != null && (bool) trickData.TrickParam.IsNoOverWrite)
                    this.self.UIParam_TargetValid = false;
                }
              }
              else if (skill.IsTargetValidGrid)
                this.self.UIParam_TargetValid = true;
              else if (skill.IsCastSkill() && !this.self.UIParam_TargetValid)
                this.self.UIParam_TargetValid = true;
            }
          }
          else
            unit1 = this.self.mBattle.FindGimmickAtGrid(grid, false, (Unit) null);
          this.mTargetPosition.x = grid.x;
          this.mTargetPosition.y = grid.y;
          this.SetYesButtonEnable(this.self.UIParam_TargetValid);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit, (List<LogSkill.Target.CondHit>) null);
              this.self.mBattleUI.TargetMain.Close();
            }
            else
            {
              if (!this.self.UIParam_TargetValid)
                this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit, (List<LogSkill.Target.CondHit>) null);
              this.self.mBattleUI.TargetMain.Open();
            }
          }
          this.self.mBattleUI.ClearEnableAll();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              if (unit1 != null)
              {
                if (unit1.IsBreakObj && unit1.IsBreakDispUI)
                {
                  this.self.mBattleUI.TargetSub.ResetHpGauge(unit1.Side, (int) unit1.CurrentStatus.param.hp, (int) unit1.MaximumStatus.param.hp);
                  this.self.mBattleUI.TargetSub.SetNoAction(unit1, (List<LogSkill.Target.CondHit>) null);
                  this.self.mBattleUI.TargetSub.Open();
                  this.self.mBattleUI.OnMapViewSelectUnit();
                  this.self.mBattleUI.IsEnableUnit = true;
                }
                else
                {
                  this.self.mBattleUI.OnMapViewSelectGrid();
                  this.self.mBattleUI.TargetSub.ForceClose(false);
                }
              }
              else
              {
                this.self.mBattleUI.OnMapViewSelectGrid();
                this.self.mBattleUI.TargetSub.ForceClose(false);
              }
            }
            else
            {
              unit1 = this.self.mBattle.FindGimmickAtGrid(grid, false, (Unit) null);
              if (unit1 != null && unit1.IsBreakObj && unit1.IsBreakDispUI)
              {
                this.self.mBattleUI.TargetSub.ResetHpGauge(unit1.Side, (int) unit1.CurrentStatus.param.hp, (int) unit1.MaximumStatus.param.hp);
                this.self.mBattleUI.TargetSub.SetNoAction(unit1, (List<LogSkill.Target.CondHit>) null);
                this.self.mBattleUI.TargetSub.Open();
              }
              else
                this.self.mBattleUI.TargetSub.ForceClose(false);
            }
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              if (unit1 != null)
              {
                if (!unit1.IsBreakObj)
                {
                  this.self.mBattleUI.TargetObjectSub.SetNoAction(unit1, (List<LogSkill.Target.CondHit>) null);
                  this.self.mBattleUI.TargetObjectSub.Open();
                  this.self.mBattleUI.OnMapViewSelectGrid();
                  this.self.mBattleUI.IsEnableGimmick = true;
                }
                else
                  this.self.mBattleUI.TargetObjectSub.Close();
              }
              else
              {
                this.self.mBattleUI.TargetObjectSub.Close();
                this.self.mBattleUI.OnMapViewSelectGrid();
              }
            }
            else
              this.self.mBattleUI.TargetObjectSub.Close();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              TrickData trickData = TrickData.SearchEffect(grid.x, grid.y);
              if (trickData != null && trickData.IsVisualized())
              {
                this.self.mBattleUI.TargetTrickSub.SetTrick(trickData.TrickParam);
                this.self.mBattleUI.IsEnableTrick = true;
              }
              if (this.self.mBattleUI.IsEnableTrick && unit1 == null)
                this.self.mBattleUI.TargetTrickSub.Open();
              else
                this.self.mBattleUI.TargetTrickSub.Close();
              if (unit1 == null || !unit1.IsBreakObj)
                this.self.mBattleUI.OnMapViewSelectGrid();
            }
            else
              this.self.mBattleUI.TargetTrickSub.Close();
          }
          bool is_enable = this.self.mBattleUI.IsNeedFlip();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetSub.SetEnableFlipButton(is_enable);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetObjectSub.SetEnableFlipButton(is_enable);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetTrickSub.SetEnableFlipButton(is_enable);
          this.self.OnGimmickUpdate();
          this.self.SetUiHeight(grid.height);
        }
        currentUnit.x = intVector2_1.x;
        currentUnit.y = intVector2_1.y;
      }

      private bool IsGridSelectable(int x, int y)
      {
        return this.mTargetGrids.get(x, y);
      }

      private bool IsGridSelectable(Unit unit)
      {
        int x = unit.x;
        int y = unit.y;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mScene.mFocusedUnit, (UnityEngine.Object) null) && this.mScene.mFocusedUnit.Unit != null && this.mScene.mFocusedUnit.Unit == unit)
        {
          IntVector2 intVector2 = this.mScene.CalcCoord(this.mScene.mFocusedUnit.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
        return this.IsGridSelectable(x, y);
      }

      private bool IsValidSkillTarget(Unit target)
      {
        if (target == null || this.self.mTargetSelectorParam.Skill == null || !this.IsGridSelectable(target))
          return false;
        return this.self.mBattle.CheckSkillTarget(this.self.mBattle.CurrentUnit, target, this.self.mTargetSelectorParam.Skill);
      }

      private void OnFocus(TacticsUnitController controller)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null))
          return;
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        if (skill == null && controller.Unit.IsGimmick)
        {
          if (this.self.mBattle.CurrentMap == null)
            return;
          this.OnFocusGrid(this.self.Battle.CurrentMap[controller.Unit.x, controller.Unit.y]);
        }
        else
        {
          if (skill != null)
          {
            if (skill.IsAllEffect())
              controller = this.self.FindUnitController(this.self.mBattle.CurrentUnit);
            else if (!this.IsValidSkillTarget(controller.Unit))
            {
              for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
              {
                if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mTacticsUnits[index], (UnityEngine.Object) controller) && this.self.mTacticsUnits[index].Unit.x == controller.Unit.x && (this.self.mTacticsUnits[index].Unit.y == controller.Unit.y && this.IsValidSkillTarget(this.self.mTacticsUnits[index].Unit)))
                {
                  controller = this.self.mTacticsUnits[index];
                  break;
                }
              }
            }
          }
          if (controller.Unit.IsGimmick && controller.Unit.IsDisableGimmick())
            return;
          if (!this.mSelectGrid)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSelectedTarget, (UnityEngine.Object) null))
              this.self.HideUnitMarkers(this.mSelectedTarget.Unit);
            this.self.ShowUnitMarker(controller.Unit, SceneBattle.UnitMarkerTypes.Target);
          }
          this.mSelectedTarget = controller;
          IntVector2 intVector2_1 = this.self.CalcCoord(controller.CenterPosition);
          this.mTargetPosition.x = intVector2_1.x;
          this.mTargetPosition.y = intVector2_1.y;
          this.self.mSelectedTarget = controller.Unit;
          this.self.UIParam_TargetValid = false;
          Unit currentUnit = this.self.mBattle.CurrentUnit;
          TacticsUnitController unitController1 = this.self.FindUnitController(currentUnit);
          int YosokuDamageHp = skill == null ? 0 : skill.GetHpCost(currentUnit);
          if (skill == null)
            this.self.ShowWalkableGrids(this.self.mBattle.CreateMoveMap(controller.Unit, (int) controller.Unit.CurrentStatus.param.mov), 0);
          else if (skill.IsTargetTeleport)
          {
            this.self.mTacticsSceneRoot.HideGridLayer(0);
            this.self.mDisplayBlockedGridMarker = false;
            this.self.mGridDisplayBlockedGridMarker = (Grid) null;
          }
          unitController1.SetHPChangeYosou((int) currentUnit.CurrentStatus.param.hp - YosokuDamageHp, 0);
          this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp, YosokuDamageHp, 0, false);
          this.self.mBattleUI.TargetMain.UpdateHpGauge();
          this.self.mBattleUI.TargetSub.ResetHpGauge(this.self.mSelectedTarget.Side, (int) this.self.mSelectedTarget.CurrentStatus.param.hp, (int) this.self.mSelectedTarget.MaximumStatus.param.hp);
          bool flag1 = false;
          BattleCore.UnitResult unitResult = (BattleCore.UnitResult) null;
          if (this.self.mTargetSelectorParam.Skill != null)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) controller, (UnityEngine.Object) null) && this.self.mTutorialTriggers != null)
            {
              for (int index = 0; index < this.self.mTutorialTriggers.Length; ++index)
                this.self.mTutorialTriggers[index].OnTargetChange(this.mSelectedTarget.Unit, controller.Unit.TurnCount);
            }
            for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
            {
              this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
              if (0 >= YosokuDamageHp || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.self.mTacticsUnits[index], (UnityEngine.Object) unitController1))
                this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue, 0);
            }
            if (this.self.mTargetSelectorParam.OnAccept != null && this.IsGridSelectable(this.self.mSelectedTarget))
            {
              IntVector2 intVector2_2 = this.self.CalcCoord(unitController1.CenterPosition);
              int x = this.self.mSelectedTarget.x;
              int y = this.self.mSelectedTarget.y;
              if (currentUnit == this.self.mSelectedTarget)
              {
                x = intVector2_2.x;
                y = intVector2_2.y;
              }
              if (!this.mSelectGrid)
                this.HilitArea(x, y);
              if (!skill.IsTargetGridNoUnit)
              {
                if (skill.IsTargetValidGrid)
                {
                  this.self.UIParam_TargetValid = true;
                }
                else
                {
                  BattleCore.CommandResult commandResult = this.self.mBattle.GetCommandResult(currentUnit, intVector2_2.x, intVector2_2.y, x, y, skill);
                  if (commandResult != null && commandResult.targets != null)
                  {
                    for (int index = 0; index < commandResult.reactions.Count; ++index)
                    {
                      if (this.self.mSelectedTarget == commandResult.reactions[index].react_unit && currentUnit == commandResult.reactions[index].unit)
                      {
                        unitResult = commandResult.reactions[index];
                        break;
                      }
                    }
                    for (int index = 0; index < commandResult.targets.Count; ++index)
                    {
                      TacticsUnitController unitController2 = this.self.FindUnitController(commandResult.targets[index].unit);
                      if (commandResult.skill.IsDamagedSkill())
                      {
                        unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Attack, skill, currentUnit);
                        int newHP = (int) commandResult.targets[index].unit.CurrentStatus.param.hp - commandResult.targets[index].hp_damage;
                        int hpmax_damage = 0;
                        if (commandResult.skill.IsMhmDamage())
                        {
                          newHP = (int) commandResult.targets[index].unit.CurrentStatus.param.hp;
                          hpmax_damage = commandResult.targets[index].hp_damage;
                        }
                        unitController2.SetHPChangeYosou(newHP, hpmax_damage);
                      }
                      else
                      {
                        unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Target, (SkillData) null, (Unit) null);
                        unitController2.SetHPChangeYosou((int) commandResult.targets[index].unit.CurrentStatus.param.hp + commandResult.targets[index].hp_heal, 0);
                      }
                    }
                    if (commandResult.self_effect.hp_damage > 0 || commandResult.self_effect.hp_heal > 0)
                    {
                      int newHP = Mathf.Min((int) currentUnit.CurrentStatus.param.hp - commandResult.self_effect.hp_damage + commandResult.self_effect.hp_heal - YosokuDamageHp, (int) currentUnit.MaximumStatus.param.hp);
                      unitController1.SetHPChangeYosou(newHP, 0);
                      this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp, commandResult.self_effect.hp_damage + YosokuDamageHp, commandResult.self_effect.hp_heal, false);
                      this.self.mBattleUI.TargetMain.UpdateHpGauge();
                    }
                    if (commandResult.targets.Count > 0)
                    {
                      bool flag2 = skill.IsNormalAttack();
                      int index = commandResult.targets.FindIndex((Predicate<BattleCore.UnitResult>) (p => p.unit == this.self.mSelectedTarget));
                      if (index != -1 && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
                      {
                        BattleCore.UnitResult target = commandResult.targets[index];
                        EUnitSide side = commandResult.targets[index].unit.Side;
                        int hp1 = (int) commandResult.targets[index].unit.CurrentStatus.param.hp;
                        int hp2 = (int) commandResult.targets[index].unit.MaximumStatus.param.hp;
                        if (skill.SkillParam.IsHealSkill())
                        {
                          this.self.mBattleUI.TargetMain.SetHealAction(this.self.mBattle.CurrentUnit, target.hp_heal, target.critical, target.avoid);
                          this.self.mBattleUI.TargetSub.SetHpGaugeParam(side, hp1, hp2, 0, commandResult.targets[index].hp_heal, false);
                        }
                        else if (skill.SkillParam.IsDamagedSkill() || skill.SkillParam.effect_type == SkillEffectTypes.Debuff)
                        {
                          if (flag2)
                            this.self.mBattleUI.TargetMain.SetAttackAction(this.self.mBattle.CurrentUnit, target.hp_damage, target.critical, 100 - target.avoid, target.cond_hit_lists);
                          else
                            this.self.mBattleUI.TargetMain.SetSkillAction(this.self.mBattle.CurrentUnit, target.hp_damage <= 0 ? target.mp_damage : target.hp_damage, target.critical, 100 - target.avoid, target.cond_hit_lists);
                          this.self.mBattleUI.TargetSub.SetHpGaugeParam(side, hp1, hp2, commandResult.targets[index].hp_damage, 0, skill.IsMhmDamage());
                        }
                        else
                          this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit, target.cond_hit_lists);
                        this.self.mBattleUI.TargetSub.UpdateHpGauge();
                        flag1 = true;
                      }
                      this.self.UIParam_TargetValid = true;
                      if (!skill.IsAreaSkill() && this.self.mSelectedTarget != null)
                        this.self.UIParam_TargetValid = this.self.Battle.CheckSkillTarget(currentUnit, this.self.mSelectedTarget, skill);
                      if (skill.IsTargetTeleport && this.self.mSelectedTarget != null)
                      {
                        bool is_teleport = false;
                        Grid teleportGrid = this.self.mBattle.GetTeleportGrid(currentUnit, intVector2_2.x, intVector2_2.y, this.self.mSelectedTarget, skill, ref is_teleport);
                        if (teleportGrid != null && this.self.mBattle.CurrentMap != null)
                        {
                          BattleMap currentMap = this.self.mBattle.CurrentMap;
                          GridMap<int> accessMap = new GridMap<int>(currentMap.Width, currentMap.Height);
                          accessMap.fill(-1);
                          accessMap.set(teleportGrid.x, teleportGrid.y, 0);
                          this.self.ShowWalkableGrids(accessMap, 0);
                        }
                        if (!is_teleport)
                        {
                          this.self.mDisplayBlockedGridMarker = true;
                          this.self.mGridDisplayBlockedGridMarker = teleportGrid;
                          this.self.UIParam_TargetValid = false;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
          else
            this.HilitNormalAttack(controller.Unit, false);
          this.SetYesButtonEnable(this.self.UIParam_TargetValid);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetMain, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit, (List<LogSkill.Target.CondHit>) null);
              this.self.mBattleUI.TargetMain.Close();
            }
            else
            {
              if (!flag1)
                this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit, (List<LogSkill.Target.CondHit>) null);
              this.self.mBattleUI.TargetMain.Open();
            }
          }
          this.self.mBattleUI.ClearEnableAll();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              if (controller.Unit.IsGimmick)
              {
                this.self.mBattleUI.TargetSub.ForceClose(false);
                this.self.mBattleUI.OnMapViewSelectGrid();
              }
              else
              {
                this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget, (List<LogSkill.Target.CondHit>) null);
                this.self.mBattleUI.TargetSub.Open();
                this.self.mBattleUI.OnMapViewSelectUnit();
                this.self.mBattleUI.IsEnableUnit = true;
              }
            }
            else if (flag1)
            {
              if (!this.self.mSelectedTarget.IsBreakObj || this.self.mSelectedTarget.IsBreakDispUI)
              {
                if (unitResult != null)
                  this.self.mBattleUI.TargetSub.SetAttackAction(this.self.mSelectedTarget, unitResult.hp_damage <= 0 ? unitResult.mp_damage : unitResult.hp_damage, 0, 100 - unitResult.avoid, unitResult.cond_hit_lists);
                else
                  this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget, (List<LogSkill.Target.CondHit>) null);
                this.self.mBattleUI.TargetSub.Open();
              }
              else
                this.self.mBattleUI.TargetSub.ForceClose(false);
            }
            else if (this.self.mSelectedTarget.UnitType == EUnitType.Unit || this.self.mSelectedTarget.UnitType == EUnitType.EventUnit || this.self.mSelectedTarget.IsBreakObj && this.self.mSelectedTarget.IsBreakDispUI)
            {
              this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget, (List<LogSkill.Target.CondHit>) null);
              this.self.mBattleUI.TargetSub.Open();
            }
            else
              this.self.mBattleUI.TargetSub.ForceClose(false);
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
          {
            if (skill == null)
            {
              if (controller.Unit.IsGimmick && !controller.Unit.IsDisableGimmick())
              {
                this.self.mBattleUI.OnMapViewSelectGrid();
                this.self.mBattleUI.TargetObjectSub.Open();
                this.self.mBattleUI.IsEnableGimmick = true;
              }
              else
              {
                this.self.mBattleUI.OnMapViewSelectUnit();
                this.self.mBattleUI.TargetObjectSub.Close();
              }
            }
            else
              this.self.mBattleUI.TargetObjectSub.Close();
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
          {
            TrickData trickData = TrickData.SearchEffect(intVector2_1.x, intVector2_1.y);
            if (trickData != null && trickData.IsVisualized())
            {
              this.self.mBattleUI.TargetTrickSub.SetTrick(trickData.TrickParam);
              this.self.mBattleUI.IsEnableTrick = true;
            }
            this.self.mBattleUI.TargetTrickSub.Close();
          }
          bool is_enable = this.self.mBattleUI.IsNeedFlip();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetSub.SetEnableFlipButton(is_enable);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetObjectSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetObjectSub.SetEnableFlipButton(is_enable);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.self.mBattleUI.TargetTrickSub, (UnityEngine.Object) null))
            this.self.mBattleUI.TargetTrickSub.SetEnableFlipButton(is_enable);
          this.self.OnGimmickUpdate();
          this.self.SetUnitUiHeight(controller.Unit);
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mDragScroll)
        {
          this.mYScrollPos += (float) ((self.mTouchController.DragDelta.y <= 0.0 ? -1.0 : 1.0) * (double) Time.get_unscaledDeltaTime() * 2.0);
          if (!this.mIgnoreDragVelocity)
            this.mYScrollPos += this.mDragY / 20f;
          if ((double) this.mYScrollPos <= -1.0)
          {
            this.mYScrollPos = 0.0f;
            this.mIgnoreDragVelocity = true;
            if (self.mTargetSelectorParam.Skill != null)
              this.ShiftTarget(-1);
          }
          else if ((double) this.mYScrollPos >= 1.0)
          {
            this.mYScrollPos = 0.0f;
            this.mIgnoreDragVelocity = true;
            if (self.mTargetSelectorParam.Skill != null)
              this.ShiftTarget(1);
          }
        }
        if (!this.mSelectGrid || self.m_TargetCameraPositionInterp)
          return;
        int index1 = Mathf.Clamp(Mathf.FloorToInt((float) self.m_CameraPosition.x), 0, self.mBattle.CurrentMap.Width);
        int index2 = Mathf.Clamp(Mathf.FloorToInt((float) self.m_CameraPosition.z), 0, self.mBattle.CurrentMap.Height);
        if (this.mTargetPosition.x == index1 && this.mTargetPosition.y == index2)
          return;
        Grid current = self.mBattle.CurrentMap[index1, index2];
        if (current == null)
          return;
        this.OnFocusGrid(current);
      }

      private void ShiftTarget(int delta)
      {
        if (this.mTargets.Count == 0)
          return;
        TacticsUnitController tacticsUnitController = this.self.mFocusedUnit;
        if (this.self.mTargetSelectorParam.Skill == null)
          tacticsUnitController = this.self.mMapModeFocusedUnit;
        int num = this.mTargets.IndexOf(tacticsUnitController);
        if (num < 0)
          num = 0;
        this.OnClickUnit(this.mTargets[(num + delta + this.mTargets.Count) % this.mTargets.Count]);
        if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) BattleUnitDetail.Instance))
          return;
        BattleUnitDetail.Instance.Refresh(this.self.mFocusedUnit.Unit);
      }
    }

    private class State_InputDirection : State<SceneBattle>
    {
      private DirectionArrow[] mArrows = new DirectionArrow[4];
      private int mSelectedDirection = -1;
      private bool mCancelButtonActive = true;
      private Unit mCurrentUnit;
      private TacticsUnitController mController;
      private bool mIsStepEnd;
      private bool mIsAuto;

      public override void Begin(SceneBattle self)
      {
        this.mCurrentUnit = self.mBattle.CurrentUnit;
        this.mController = self.FindUnitController(this.mCurrentUnit);
        this.mController.AutoUpdateRotation = true;
        self.ToggleRenkeiAura(false);
        self.InterpCameraTarget((Component) this.mController);
        self.HideAllHPGauges();
        self.HideAllUnitOwnerIndex();
        Selectable component = (Selectable) self.mBattleUI.CommandWindow.CancelButton.GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          this.mCancelButtonActive = component.get_interactable();
          if (!this.mCurrentUnit.IsEnableActionCondition() && !this.mCurrentUnit.IsEnableMoveCondition(false))
            component.set_interactable(false);
          else
            component.set_interactable(true);
        }
        self.mBattleUI.CommandWindow.OKButton.SetActive(true);
        IntVector2 intVector2 = self.CalcCoord(this.mController.CenterPosition);
        Grid current = self.mBattle.CurrentMap[intVector2.x, intVector2.y];
        this.mController.StepTo(self.CalcGridCenter(current));
        this.mCurrentUnit.Direction = self.FindUnitController(self.mBattle.CurrentUnit).CalcUnitDirectionFromRotation();
      }

      private void OnStepEnd()
      {
        this.self.mOnScreenClick = new SceneBattle.ScreenClickEvent(this.OnScreenClick);
        for (int index = 0; index < 4; ++index)
        {
          Quaternion rotation = ((EUnitDirection) index).ToRotation();
          Vector3 vector3 = Quaternion.op_Multiply(rotation, ((Component) this.self.DirectionArrowTemplate).get_transform().get_position());
          this.mArrows[index] = UnityEngine.Object.Instantiate((UnityEngine.Object) this.self.DirectionArrowTemplate, Vector3.op_Addition(((Component) this.mController).get_transform().get_position(), vector3), rotation) as DirectionArrow;
        }
        this.UpdateArrows();
        this.self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        this.self.mOnScreenClick = new SceneBattle.ScreenClickEvent(this.OnScreenClick);
        this.self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        TacticsUnitController unitController = this.self.FindUnitController(this.self.mBattle.CurrentUnit);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unitController, (UnityEngine.Object) null) || this.self.mTutorialTriggers == null)
          return;
        for (int index = 0; index < this.self.mTutorialTriggers.Length; ++index)
          this.self.mTutorialTriggers[index].OnSelectDirectionStart(this.mCurrentUnit, unitController.Unit.TurnCount);
      }

      private void OnYesNoSelect(bool yes)
      {
        if (yes)
        {
          this.SelectDirection(this.self.mBattle.CurrentUnit.Direction);
          this.self.Battle.MapCommandEnd(this.self.Battle.CurrentUnit);
          if (!this.self.Battle.IsMultiPlay)
            return;
          this.self.SendInputEntryBattle(EBattleCommand.Wait, this.self.Battle.CurrentUnit, (Unit) null, (SkillData) null, (ItemData) null, this.self.Battle.CurrentUnit.x, this.self.Battle.CurrentUnit.y, false);
        }
        else
        {
          this.self.mBattleUI.OnInputDirectionEnd();
          this.self.ToggleRenkeiAura(true);
          this.self.GotoMapCommand();
        }
      }

      private void UpdateArrows()
      {
        for (int index = 0; index < 4; ++index)
        {
          EUnitDirection eunitDirection = (EUnitDirection) index;
          this.mArrows[index].State = eunitDirection != this.mCurrentUnit.Direction ? DirectionArrow.ArrowStates.Normal : DirectionArrow.ArrowStates.Hilit;
          this.mArrows[index].Direction = eunitDirection;
        }
      }

      public override void Update(SceneBattle self)
      {
        self.Battle.IsAutoBattle = self.Battle.RequestAutoBattle;
        if (this.mController.isIdle && !this.mIsStepEnd)
        {
          this.OnStepEnd();
          this.mIsStepEnd = true;
          if (self.Battle.IsAutoBattle)
          {
            this.SelectDirection(this.mCurrentUnit.Direction);
            return;
          }
        }
        if ((double) Time.get_timeScale() <= 0.0)
        {
          if (self.Battle.IsAutoBattle)
            this.mIsAuto = true;
          else
            this.mIsAuto = false;
        }
        else if (this.mIsAuto)
          this.SelectDirection(this.mCurrentUnit.Direction);
        else if (self.Battle.IsAutoBattle)
        {
          if ((double) Time.get_timeScale() > 0.0)
            return;
          this.SelectDirection(this.mCurrentUnit.Direction);
        }
        else
        {
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) self.mTouchController, (UnityEngine.Object) null))
            return;
          Vector2 worldSpaceVelocity = self.mTouchController.WorldSpaceVelocity;
          // ISSUE: explicit reference operation
          if ((double) ((Vector2) @worldSpaceVelocity).get_magnitude() <= 5.0)
            return;
          // ISSUE: explicit reference operation
          ((Vector2) @worldSpaceVelocity).Normalize();
          this.mCurrentUnit.Direction = TacticsUnitController.CalcUnitDirection((float) worldSpaceVelocity.x, (float) worldSpaceVelocity.y);
          this.UpdateArrows();
        }
      }

      public override void End(SceneBattle self)
      {
        self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        for (int index = 0; index < 4; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mArrows[index], (UnityEngine.Object) null) && index != this.mSelectedDirection)
            this.mArrows[index].State = DirectionArrow.ArrowStates.Close;
        }
        Selectable component = (Selectable) self.mBattleUI.CommandWindow.CancelButton.GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.set_interactable(this.mCancelButtonActive);
        self.mOnScreenClick = (SceneBattle.ScreenClickEvent) null;
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnScreenClick(Vector2 position)
      {
        Camera main = Camera.get_main();
        float num1 = (float) (Screen.get_height() / 5);
        int num2 = -1;
        float num3 = float.MaxValue;
        for (int index = 0; index < this.mArrows.Length; ++index)
        {
          Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Implicit(main.WorldToScreenPoint(((Component) this.mArrows[index]).get_transform().get_position())), position);
          // ISSUE: explicit reference operation
          float magnitude = ((Vector2) @vector2).get_magnitude();
          if ((double) magnitude < (double) num1 && (double) magnitude < (double) num3)
          {
            num3 = magnitude;
            num2 = index;
          }
        }
        if (num2 == -1)
          return;
        this.SelectDirection((EUnitDirection) num2);
      }

      public void SelectDirection(EUnitDirection dir)
      {
        this.self.ApplyUnitMovement(false);
        this.mArrows[(int) dir].State = DirectionArrow.ArrowStates.Press;
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0003", 0.0f);
        this.mSelectedDirection = (int) dir;
        this.self.mBattleUI.OnInputDirectionEnd();
        if (this.self.Battle.IsMultiPlay)
        {
          this.self.SendInputUnitEnd(this.self.Battle.CurrentUnit, dir);
          this.self.SendInputFlush(false);
        }
        this.self.mBattle.CommandWait(dir);
        this.self.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
          return;
        this.self.mBattleUI.OnInputDirectionEnd();
        this.self.GotoMapCommand();
      }
    }

    private class State_SelectSkillV2 : State<SceneBattle>
    {
      private UnitAbilitySkillList mSkillList;

      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        this.mSkillList = self.mBattleUI.SkillWindow;
        Unit currentUnit = self.mBattle.CurrentUnit;
        TacticsUnitController unitController = self.FindUnitController(currentUnit);
        EUnitDirection direction = unitController.CalcUnitDirectionFromRotation();
        ((Component) unitController).get_transform().set_rotation(direction.ToRotation());
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillList, (UnityEngine.Object) null))
          this.mSkillList.OnSelectSkill = new UnitAbilitySkillList.SelectSkillEvent(this.OnSelectSkill);
        self.StepToNear(currentUnit);
      }

      public override void End(SceneBattle self)
      {
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSkillList, (UnityEngine.Object) null))
          return;
        this.mSkillList.OnSelectSkill = (UnitAbilitySkillList.SelectSkillEvent) null;
      }

      private void OnSelectSkill(SkillData skill)
      {
        this.self.mBattleUI.OnSkillSelectEnd();
        this.self.GotoSelectTarget(skill, new SceneBattle.SelectTargetCallback(this.self.GotoSkillSelect), new SceneBattle.SelectTargetPositionWithSkill(this.self.OnSelectSkillTarget), (Unit) null, true);
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
        {
          this.self.mBattleUI.OnSkillSelectEnd();
        }
        else
        {
          this.self.mBattleUI.OnSkillSelectEnd();
          this.self.GotoMapCommand();
        }
      }
    }

    private class State_SelectGridEventV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        self.mBattleUI.OnGridEventSelectStart();
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
      }

      public override void End(SceneBattle self)
      {
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
      }

      private void OnStateChange(SceneBattle.StateTransitionTypes req)
      {
        if (req == SceneBattle.StateTransitionTypes.Forward)
        {
          if (!this.self.ApplyUnitMovement(false))
            return;
          if (this.self.Battle.ExecuteEventTriggerOnGrid(this.self.Battle.CurrentUnit, EEventTrigger.ExecuteOnGrid))
            this.self.SendInputGridEvent(this.self.Battle.CurrentUnit);
          this.self.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
        }
        else
        {
          this.self.mBattleUI.OnGridEventSelectEnd();
          this.self.GotoMapCommand();
        }
      }
    }

    public enum eMaskBattleUI
    {
      MENU = 1,
      MAP = 2,
      CAMERA = 4,
      CMD_ATTACK = 8,
      CMD_ABILITYS = 16, // 0x00000010
      CMD_ITEM = 32, // 0x00000020
      CMD_END = 64, // 0x00000040
      VS_GRID_TAP = 128, // 0x00000080
      VS_SWIPE = 256, // 0x00000100
      BACK_KEY = 512, // 0x00000200
    }

    private enum UnitMarkerTypes
    {
      Target,
      Enemy,
      Assist,
    }

    private class PupupData
    {
      public int priority;
      public Vector3 position;
      public float yOffset;

      public PupupData(Vector3 position, int priority, float yOffset)
      {
        this.priority = priority;
        this.position = position;
        this.yOffset = yOffset;
      }
    }

    public class VirtualStickInput : SceneBattle.MoveInput
    {
      private Vector2 mBasePos = Vector2.get_zero();
      private Vector2 mTargetPos = Vector2.get_zero();
      private const float STOP_RADIUS = 0.1f;
      private TacticsUnitController mController;
      private GridMap<int> mWalkableGrids;
      private Vector3 mStart;
      private int mDestX;
      private int mDestY;
      private bool mTargetSet;
      private bool mMoveStarted;
      private bool mClickedOK;
      private float mGridSnapTime;
      private bool mJumping;
      private bool mHasInput;
      private int mCurrentX;
      private int mCurrentY;
      private GridMap<bool> mShateiGrid;
      private float mStopTime;
      private bool mRouteSet;
      private bool mFullAccel;
      private bool mHasDesiredRotation;
      private bool mGridSnapping;
      private Quaternion mDesiredRotation;
      private bool mUpdateShateiGrid;
      private bool mShateiVisible;

      public override bool IsBusy
      {
        get
        {
          if (!this.mHasInput && !this.mGridSnapping && (double) this.mGridSnapTime < 0.0)
            return this.mHasDesiredRotation;
          return true;
        }
      }

      public override void Reset()
      {
        ((Component) this.mController).get_transform().set_position(this.mStart);
        this.mController.CancelAction();
        this.SceneOwner.SetCameraTarget(this.mStart);
        this.SceneOwner.SendInputMoveEnd(this.SceneOwner.Battle.CurrentUnit, true);
        this.mMoveStarted = false;
      }

      public override void Start()
      {
        this.SceneOwner.m_AllowCameraTranslation = false;
        this.mController = this.SceneOwner.FindUnitController(this.SceneOwner.Battle.CurrentUnit);
        this.mController.AutoUpdateRotation = false;
        this.SceneOwner.ShowUnitCursorOnCurrent();
        BattleMap currentMap = this.SceneOwner.Battle.CurrentMap;
        this.mWalkableGrids = this.SceneOwner.CreateCurrentAccessMap();
        this.mController.WalkableField = this.mWalkableGrids;
        this.SceneOwner.ShowWalkableGrids(this.mWalkableGrids, 0);
        IntVector2 intVector2 = this.SceneOwner.CalcCoord(this.mController.CenterPosition);
        this.mStart = this.SceneOwner.CalcGridCenter(currentMap[intVector2.x, intVector2.y]);
        this.mDestX = intVector2.x;
        this.mDestY = intVector2.y;
        this.mCurrentX = this.mDestX;
        this.mCurrentY = this.mDestY;
        this.mBasePos.x = (__Null) ((double) intVector2.x + 0.5);
        this.mBasePos.y = (__Null) ((double) intVector2.y + 0.5);
        this.mTargetPos = this.mBasePos;
        this.SceneOwner.ResetMoveCamera();
        this.SceneOwner.SendInputMoveStart(this.SceneOwner.Battle.CurrentUnit);
        this.SceneOwner.mTouchController.OnClick += new TouchController.ClickEvent(this.OnClick);
        this.SceneOwner.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
        this.SceneOwner.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
        this.RecalcAttackTargets();
      }

      public override void End()
      {
        this.SceneOwner.mTouchController.OnClick -= new TouchController.ClickEvent(this.OnClick);
        this.SceneOwner.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        this.SceneOwner.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
        this.SceneOwner.m_AllowCameraTranslation = true;
        this.SceneOwner.mDisplayBlockedGridMarker = false;
        List<TacticsUnitController> mTacticsUnits = this.SceneOwner.mTacticsUnits;
        for (int index = 0; index < mTacticsUnits.Count; ++index)
        {
          mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          mTacticsUnits[index].SetHPChangeYosou(mTacticsUnits[index].VisibleHPValue, 0);
        }
        if (this.SceneOwner.Battle.EntryBattleMultiPlayTimeUp)
          this.mController.AutoUpdateRotation = true;
        this.mController.StopRunning();
        this.mController.WalkableField = (GridMap<int>) null;
        this.mController.HideCursor(false);
        this.SceneOwner.mTacticsSceneRoot.HideGridLayer(0);
      }

      public override void MoveUnit(Vector3 target_screen_pos)
      {
        this.OnClick(Vector2.op_Implicit(target_screen_pos));
      }

      private void OnDrag()
      {
      }

      private void OnDragEnd()
      {
        this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, this.mDestX, this.mDestY, this.SceneOwner.Battle.CurrentUnit.Direction, true);
      }

      private void OnClick(Vector2 screenPos)
      {
        if (this.SceneOwner.Battle.EntryBattleMultiPlayTimeUp || !this.SceneOwner.IsControlBattleUI(SceneBattle.eMaskBattleUI.VS_GRID_TAP))
          return;
        Unit unit1 = this.mController.Unit;
        if (this.mShateiGrid != null)
        {
          RectTransform transform = ((Component) this.SceneOwner.mTouchController).get_transform() as RectTransform;
          Vector2 vector2_1;
          RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, screenPos, (Camera) null, ref vector2_1);
          Vector2 vector2_2 = vector2_1;
          Rect rect = transform.get_rect();
          // ISSUE: explicit reference operation
          Vector2 position = ((Rect) @rect).get_position();
          Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, position);
          Unit unit2 = (Unit) null;
          float num = float.MaxValue;
          SkillData attackSkill = unit1.GetAttackSkill();
          for (int index = 0; index < this.SceneOwner.mTacticsUnits.Count; ++index)
          {
            TacticsUnitController mTacticsUnit = this.SceneOwner.mTacticsUnits[index];
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) mTacticsUnit, (UnityEngine.Object) null))
            {
              Unit unit3 = mTacticsUnit.Unit;
              if (unit3 != null)
              {
                RectTransform hpGaugeTransform = mTacticsUnit.HPGaugeTransform;
                if (!UnityEngine.Object.op_Equality((UnityEngine.Object) hpGaugeTransform, (UnityEngine.Object) null) && ((Component) hpGaugeTransform).get_gameObject().get_activeInHierarchy() && (this.mShateiGrid.isValid(unit3.x, unit3.y) && this.mShateiGrid.get(unit3.x, unit3.y)) && this.SceneOwner.mBattle.CheckSkillTarget(unit1, unit3, attackSkill))
                {
                  Vector2 vector2_4 = Vector2.op_Subtraction(hpGaugeTransform.get_anchoredPosition(), vector2_3);
                  // ISSUE: explicit reference operation
                  float magnitude = ((Vector2) @vector2_4).get_magnitude();
                  if ((double) magnitude < (double) num && (double) magnitude < 80.0)
                  {
                    unit2 = unit3;
                    num = magnitude;
                  }
                }
              }
            }
          }
          if (unit2 != null)
          {
            this.SelectAttackTarget(unit2);
            return;
          }
        }
        IntVector2 intVector2_1 = this.SceneOwner.CalcClickedGrid(screenPos);
        IntVector2 intVector2_2 = this.SceneOwner.CalcCoord(((Component) this.mController).get_transform().get_position());
        if (!(intVector2_2 != intVector2_1) || this.mController.IsPlayingFieldAction)
          return;
        Vector3[] path = this.SceneOwner.FindPath(intVector2_2.x, intVector2_2.y, intVector2_1.x, intVector2_1.y, unit1.DisableMoveGridHeight, this.mWalkableGrids);
        if (path == null)
          return;
        path[0] = this.mController.CenterPosition;
        double num1 = (double) this.mController.StartMove(path, -1f);
        if (!this.mRouteSet)
          this.HideShatei();
        this.mRouteSet = true;
        this.mTargetSet = false;
        this.mGridSnapping = false;
        this.mDestX = intVector2_1.x;
        this.mDestY = intVector2_1.y;
        this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, intVector2_1.x, intVector2_1.y, this.SceneOwner.Battle.CurrentUnit.Direction, true);
      }

      private void UpdateBlockMarker()
      {
        IntVector2 intVector2 = this.SceneOwner.CalcCoord(this.mController.CenterPosition);
        Grid current = this.SceneOwner.mBattle.CurrentMap[intVector2.x, intVector2.y];
        this.SceneOwner.mDisplayBlockedGridMarker = current == null || !this.SceneOwner.mBattle.CheckMove(this.mController.Unit, current);
      }

      private bool IsGridBlocked(Vector2 co)
      {
        return this.IsGridBlocked((float) co.x, (float) co.y);
      }

      private bool IsGridBlocked(float x, float y)
      {
        int x1 = Mathf.FloorToInt(x);
        int y1 = Mathf.FloorToInt(y);
        if (this.mWalkableGrids.isValid(x1, y1))
          return this.mWalkableGrids.get(x1, y1) < 0;
        return true;
      }

      private bool CanMoveToAdj(Vector2 from, Vector2 to)
      {
        if (this.IsGridBlocked(to))
          return false;
        if (this.IsGridBlocked((float) from.x, (float) to.y))
          return !this.IsGridBlocked((float) to.x, (float) from.y);
        return true;
      }

      private bool CanMoveToAdjDirect(Vector2 from, Vector2 to)
      {
        if (!this.IsGridBlocked((float) from.x, (float) to.y))
          return !this.IsGridBlocked((float) to.x, (float) from.y);
        return false;
      }

      private bool GridEqualIn2D(Vector2 a, Vector2 b)
      {
        if ((int) a.x == (int) b.x)
          return (int) a.y == (int) b.y;
        return false;
      }

      private void AdjustTargetPos(ref Vector2 basePos, ref Vector2 targetPos, Vector2 inputDir, Vector2 unitPos)
      {
        if (this.CanMoveToAdj(basePos, targetPos))
          return;
        bool flag = false;
        Vector2 vector2 = Vector2.op_Subtraction(targetPos, basePos);
        // ISSUE: explicit reference operation
        if ((double) Vector3.Dot(Vector2.op_Implicit(((Vector2) @vector2).get_normalized()), Vector2.op_Implicit(Vector2.op_Subtraction(unitPos, basePos))) >= -0.100000001490116)
        {
          Vector2[] vector2Array = new Vector2[8]
          {
            new Vector2(-1f, 1f),
            new Vector2(0.0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0.0f),
            new Vector2(1f, -1f),
            new Vector2(0.0f, -1f),
            new Vector2(-1f, -1f),
            new Vector2(-1f, 0.0f)
          };
          float[] numArray = new float[vector2Array.Length];
          // ISSUE: explicit reference operation
          ((Vector2) @inputDir).Normalize();
          for (int index = 0; index < vector2Array.Length; ++index)
          {
            // ISSUE: explicit reference operation
            numArray[index] = Vector2.Dot(((Vector2) @vector2Array[index]).get_normalized(), inputDir);
          }
          for (int index1 = 0; index1 < vector2Array.Length; ++index1)
          {
            for (int index2 = index1 + 1; index2 < vector2Array.Length; ++index2)
            {
              if ((double) numArray[index1] < (double) numArray[index2])
              {
                GameUtility.swap<float>(ref numArray[index1], ref numArray[index2]);
                GameUtility.swap<Vector2>(ref vector2Array[index1], ref vector2Array[index2]);
              }
            }
          }
          Vector2 zero = Vector2.get_zero();
          for (int index = 1; index < vector2Array.Length && (double) numArray[index] >= 0.5; ++index)
          {
            zero.x = basePos.x + vector2Array[index].x;
            zero.y = basePos.y + vector2Array[index].y;
            if (this.CanMoveToAdj(this.mBasePos, zero))
            {
              targetPos = zero;
              flag = true;
              break;
            }
          }
        }
        if (flag && this.CanMoveToAdj(basePos, targetPos))
          return;
        targetPos = basePos;
      }

      private Vector2 Velocity
      {
        get
        {
          Camera main = Camera.get_main();
          VirtualStick2 virtualStick = this.SceneOwner.mBattleUI.VirtualStick;
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) virtualStick, (UnityEngine.Object) null) || !this.SceneOwner.IsControlBattleUI(SceneBattle.eMaskBattleUI.VS_SWIPE))
            return Vector2.get_zero();
          Transform transform = ((Component) main).get_transform();
          Vector3 vector3 = !this.SceneOwner.isUpView ? transform.get_forward() : transform.get_up();
          Vector3 right = transform.get_right();
          vector3.y = (__Null) 0.0;
          ((Vector3) @vector3).Normalize();
          right.y = (__Null) 0.0;
          ((Vector3) @right).Normalize();
          Vector2 velocity = virtualStick.Velocity;
          return new Vector2((float) (right.x * velocity.x + vector3.x * velocity.y), (float) (right.z * velocity.x + vector3.z * velocity.y));
        }
      }

      private void SyncCameraPosition()
      {
        Vector3 position = ((Component) this.mController).get_transform().get_position();
        this.SceneOwner.SetCameraTarget((float) position.x, (float) position.z);
      }

      public override void Update()
      {
        if (this.mGridSnapping)
        {
          this.SyncCameraPosition();
          if (!this.mController.isIdle)
            return;
          this.UpdateBlockMarker();
          this.mGridSnapping = false;
        }
        if (this.mClickedOK)
        {
          this.SceneOwner.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
        }
        else
        {
          if (this.SceneOwner.IsCameraMoving)
            return;
          if (this.mController.IsPlayingFieldAction)
          {
            this.SyncCameraPosition();
          }
          else
          {
            if (this.mJumping)
            {
              this.mJumping = false;
              this.SceneOwner.SendInputMove(this.SceneOwner.Battle.CurrentUnit, this.mController);
            }
            Transform transform = ((Component) this.mController).get_transform();
            Vector3 pos = this.mController.CenterPosition;
            bool flag1 = false;
            Vector2 vector2;
            // ISSUE: explicit reference operation
            ((Vector2) @vector2).\u002Ector((float) pos.x, (float) pos.z);
            if (this.mTargetSet && this.GridEqualIn2D(vector2, this.mTargetPos))
            {
              this.mBasePos = this.mTargetPos;
              this.mDestX = Mathf.FloorToInt((float) this.mBasePos.x);
              this.mDestY = Mathf.FloorToInt((float) this.mBasePos.y);
              this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, this.mDestX, this.mDestY, this.SceneOwner.Battle.CurrentUnit.Direction, false);
            }
            Vector2 velocity1 = this.Velocity;
            if (this.mRouteSet)
            {
              this.SyncCameraPosition();
              if (this.mController.isMoving)
              {
                Vector2 velocity2 = this.Velocity;
                // ISSUE: explicit reference operation
                if ((double) ((Vector2) @velocity2).get_magnitude() < 0.100000001490116 || this.mController.IsPlayingFieldAction)
                  goto label_17;
              }
              this.mRouteSet = false;
              this.mTargetSet = false;
              this.mGridSnapping = false;
              this.mController.StopRunning();
              IntVector2 intVector2 = this.SceneOwner.CalcCoord(this.mController.CenterPosition);
              this.mDestX = intVector2.x;
              this.mDestY = intVector2.y;
              this.mBasePos.x = (__Null) ((double) this.mDestX + 0.5);
              this.mBasePos.y = (__Null) ((double) this.mDestY + 0.5);
label_17:
              this.UpdateBlockMarker();
            }
            else
            {
              // ISSUE: explicit reference operation
              if ((double) ((Vector2) @velocity1).get_sqrMagnitude() > 0.0)
              {
                if (!this.mMoveStarted)
                  this.mMoveStarted = true;
                float num1 = Mathf.Floor((float) (((double) (Mathf.Atan2((float) velocity1.y, (float) velocity1.x) * 57.29578f) + 22.5) / 45.0)) * 45f;
                float num2 = Mathf.Cos(num1 * ((float) Math.PI / 180f));
                float num3 = Mathf.Sin(num1 * ((float) Math.PI / 180f));
                float num4 = (double) Mathf.Abs(num2) < 9.99999974737875E-05 ? 0.0f : Mathf.Sign(num2);
                float num5 = (double) Mathf.Abs(num3) < 9.99999974737875E-05 ? 0.0f : Mathf.Sign(num3);
                this.mTargetPos.x = (__Null) (this.mBasePos.x + (double) num4);
                this.mTargetPos.y = (__Null) (this.mBasePos.y + (double) num5);
                this.AdjustTargetPos(ref this.mBasePos, ref this.mTargetPos, velocity1, vector2);
                Vector3 vector3;
                // ISSUE: explicit reference operation
                ((Vector3) @vector3).\u002Ector((float) velocity1.x, 0.0f, (float) velocity1.y);
                this.mDesiredRotation = Quaternion.LookRotation(vector3);
                this.mHasDesiredRotation = true;
                this.mTargetSet = true;
                this.mGridSnapTime = GameSettings.Instance.Quest.GridSnapDelay;
                this.mHasInput = true;
              }
              else
              {
                this.mFullAccel = false;
                this.mTargetSet = false;
                this.mHasInput = false;
                this.mController.StopRunning();
                if ((double) this.mGridSnapTime >= 0.0 && !this.mHasDesiredRotation)
                {
                  this.mGridSnapTime -= Time.get_deltaTime();
                  if ((double) this.mGridSnapTime <= 0.0)
                  {
                    this.mController.StepTo(this.SceneOwner.CalcGridCenter(this.SceneOwner.mBattle.CurrentMap[this.mDestX, this.mDestY]));
                    this.mGridSnapping = true;
                    if (this.SceneOwner.Battle.CurrentUnit != null)
                      this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, this.mDestX, this.mDestY, this.SceneOwner.Battle.CurrentUnit.Direction, true);
                  }
                }
              }
              Vector3 vector3_1 = Vector3.get_zero();
              if (this.mTargetSet)
              {
                if (this.CanMoveToAdjDirect(this.mBasePos, this.mTargetPos))
                  vector3_1 = Vector2.op_Implicit(Vector2.op_Subtraction(this.mTargetPos, vector2));
                else if (this.IsGridBlocked((float) vector2.x, (float) this.mTargetPos.y))
                  vector3_1.x = this.mTargetPos.x - vector2.x;
                else if (this.IsGridBlocked((float) this.mTargetPos.x, (float) vector2.y))
                  vector3_1.y = this.mTargetPos.y - vector2.y;
                else
                  vector3_1 = Vector2.op_Implicit(Vector2.op_Subtraction(this.mTargetPos, vector2));
                // ISSUE: explicit reference operation
                if ((double) ((Vector3) @vector3_1).get_magnitude() < 0.100000001490116)
                  vector3_1 = Vector2.op_Implicit(Vector2.get_zero());
              }
              // ISSUE: explicit reference operation
              bool flag2 = (double) ((Vector3) @vector3_1).get_sqrMagnitude() > 0.0;
              if (this.mHasDesiredRotation || flag2)
              {
                Quaternion quaternion = !flag2 ? this.mDesiredRotation : Quaternion.LookRotation(new Vector3((float) vector3_1.x, 0.0f, (float) vector3_1.y));
                GameSettings instance = GameSettings.Instance;
                // ISSUE: explicit reference operation
                float magnitude = ((Vector2) @velocity1).get_magnitude();
                float num1 = 0.0f;
                if (this.mFullAccel)
                {
                  num1 = magnitude;
                  ((Component) this.mController).get_transform().set_rotation(Quaternion.Slerp(((Component) this.mController).get_transform().get_rotation(), quaternion, Time.get_deltaTime() * 5f));
                }
                else
                {
                  ((Component) this.mController).get_transform().set_rotation(Quaternion.Slerp(((Component) this.mController).get_transform().get_rotation(), quaternion, Time.get_deltaTime() * 10f));
                  float num2 = Quaternion.Angle(quaternion, ((Component) this.mController).get_transform().get_rotation());
                  if ((double) magnitude > 0.100000001490116)
                  {
                    if ((double) num2 < 1.0)
                    {
                      this.mFullAccel = true;
                      num1 = magnitude;
                    }
                    else
                    {
                      float num3 = 15f;
                      float num4 = Mathf.Clamp01((float) (1.0 - (double) num2 / (double) num3));
                      num1 = magnitude * num4;
                    }
                  }
                  if ((double) num2 < 1.0)
                  {
                    ((Component) this.mController).get_transform().set_rotation(quaternion);
                    this.mHasDesiredRotation = false;
                  }
                }
                if ((double) num1 > 0.0 && flag2)
                {
                  this.mController.StartRunning();
                  float num2 = Mathf.Lerp(instance.Quest.MapRunSpeedMin, instance.Quest.MapRunSpeedMax, num1);
                  Vector3 vector3_2;
                  // ISSUE: explicit reference operation
                  ((Vector3) @vector3_2).\u002Ector((float) vector3_1.x, 0.0f, (float) vector3_1.y);
                  // ISSUE: explicit reference operation
                  Vector3 velocity2 = Vector3.op_Multiply(((Vector3) @vector3_2).get_normalized(), num2);
                  if (this.mController.TriggerFieldAction(velocity2, false))
                  {
                    Vector2 fieldActionPoint = this.mController.FieldActionPoint;
                    this.mDestX = Mathf.FloorToInt((float) fieldActionPoint.x);
                    this.mDestY = Mathf.FloorToInt((float) fieldActionPoint.y);
                    this.mHasDesiredRotation = false;
                    this.mTargetSet = false;
                    this.mJumping = true;
                    this.mRouteSet = true;
                  }
                  else
                  {
                    pos = Vector3.op_Addition(pos, Vector3.op_Multiply(velocity2, Time.get_deltaTime()));
                    flag1 = true;
                    for (int index1 = -1; index1 <= 1; ++index1)
                    {
                      for (int index2 = -1; index2 <= 1; ++index2)
                      {
                        if ((index2 != 0 || index1 != 0) && this.IsGridBlocked((float) Mathf.FloorToInt((float) (pos.x + (double) index2 * 0.300000011920929)), (float) Mathf.FloorToInt((float) (pos.z + (double) index1 * 0.300000011920929))))
                        {
                          if (index2 < 0)
                            this.mController.AdjustMovePos(EUnitDirection.NegativeX, ref pos);
                          if (index2 > 0)
                            this.mController.AdjustMovePos(EUnitDirection.PositiveX, ref pos);
                          if (index1 < 0)
                            this.mController.AdjustMovePos(EUnitDirection.NegativeY, ref pos);
                          if (index1 > 0)
                            this.mController.AdjustMovePos(EUnitDirection.PositiveY, ref pos);
                        }
                      }
                    }
                  }
                }
                this.SceneOwner.SendInputMove(this.SceneOwner.Battle.CurrentUnit, this.mController);
              }
              IntVector2 intVector2 = this.SceneOwner.CalcCoord(pos);
              BattleMap currentMap = this.SceneOwner.Battle.CurrentMap;
              intVector2.x = Math.Min(Math.Max(0, intVector2.x), currentMap.Width - 1);
              intVector2.y = Math.Min(Math.Max(0, intVector2.y), currentMap.Height - 1);
              if (intVector2.x != this.mCurrentX || intVector2.y != this.mCurrentY)
              {
                this.mCurrentX = intVector2.x;
                this.mCurrentY = intVector2.y;
                this.RecalcAttackTargets();
              }
              if (flag1)
              {
                transform.set_position(pos);
                this.UpdateBlockMarker();
                this.mStopTime = 0.0f;
                this.HideShatei();
              }
              else
              {
                this.mStopTime += Time.get_deltaTime();
                if ((double) this.mStopTime > 0.300000011920929 && (this.mUpdateShateiGrid || !this.mShateiVisible) && ((double) this.mGridSnapTime <= 0.0 && this.mController.isIdle))
                {
                  this.mUpdateShateiGrid = false;
                  if (this.mShateiGrid != null)
                    this.SceneOwner.mTacticsSceneRoot.ShowGridLayer(1, this.mShateiGrid, Color32.op_Implicit(GameSettings.Instance.Colors.AttackArea), true);
                  this.mShateiVisible = true;
                  this.SceneOwner.ShowCastSkill();
                  this.mController.Unit.RefleshMomentBuff(this.SceneOwner.Battle.Units, true, intVector2.x, intVector2.y);
                }
              }
              this.SyncCameraPosition();
            }
          }
        }
      }

      private void HideShatei()
      {
        this.SceneOwner.mTacticsSceneRoot.HideGridLayer(1);
        this.mShateiVisible = false;
        this.SceneOwner.HideCastSkill(false);
      }

      private void RecalcAttackTargets()
      {
        Unit unit1 = this.mController.Unit;
        if (!unit1.IsEnableAttackCondition(false))
          return;
        this.mController.Unit.RefleshMomentBuff(this.SceneOwner.Battle.Units, true, this.mCurrentX, this.mCurrentY);
        SkillData attackSkill = unit1.GetAttackSkill();
        this.mUpdateShateiGrid = true;
        this.mShateiGrid = this.SceneOwner.Battle.CreateSelectGridMap(unit1, this.mCurrentX, this.mCurrentY, attackSkill);
        this.SceneOwner.mNumHotTargets = 0;
        this.SceneOwner.mHotTargets.Clear();
        List<Unit> units = this.SceneOwner.mBattle.Units;
        for (int index1 = 0; index1 < units.Count; ++index1)
        {
          TacticsUnitController unitController1 = this.SceneOwner.FindUnitController(units[index1]);
          if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController1, (UnityEngine.Object) null))
          {
            if (this.mShateiGrid.isValid(units[index1].x, units[index1].y) && this.mShateiGrid.get(units[index1].x, units[index1].y) && this.SceneOwner.mBattle.CheckSkillTarget(unit1, units[index1], attackSkill))
            {
              BattleCore.CommandResult commandResult = this.SceneOwner.mBattle.GetCommandResult(unit1, this.mCurrentX, this.mCurrentY, units[index1].x, units[index1].y, attackSkill);
              if (commandResult != null && commandResult.targets != null && commandResult.targets.Count > 0)
              {
                bool flag = false;
                for (int index2 = 0; index2 < commandResult.targets.Count; ++index2)
                {
                  Unit unit2 = commandResult.targets[index2].unit;
                  TacticsUnitController unitController2 = this.SceneOwner.FindUnitController(unit2);
                  if (!UnityEngine.Object.op_Equality((UnityEngine.Object) unitController2, (UnityEngine.Object) null))
                  {
                    int newHP = (int) unit2.CurrentStatus.param.hp - commandResult.targets[index2].hp_damage;
                    if (commandResult.skill != null && commandResult.skill.IsMhmDamage())
                      newHP = (int) unit2.CurrentStatus.param.hp;
                    unitController2.SetHPChangeYosou(newHP, 0);
                    unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Attack, attackSkill, unit1);
                    ++this.SceneOwner.mNumHotTargets;
                    this.SceneOwner.mHotTargets.Add(unitController1);
                    if (UnityEngine.Object.op_Equality((UnityEngine.Object) unitController1, (UnityEngine.Object) unitController2))
                      flag = true;
                  }
                }
                if (!flag)
                {
                  unitController1.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
                  unitController1.SetHPChangeYosou(unitController1.VisibleHPValue, 0);
                }
              }
              else
              {
                unitController1.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
                unitController1.SetHPChangeYosou(unitController1.VisibleHPValue, 0);
              }
            }
            else
            {
              unitController1.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
              unitController1.SetHPChangeYosou(unitController1.VisibleHPValue, 0);
            }
          }
        }
      }
    }

    public delegate void StateTransitionRequest(SceneBattle.StateTransitionTypes type);

    public delegate void QuestEndEvent();

    private delegate void SelectTargetCallback();

    private delegate void SelectTargetPositionWithSkill(int x, int y, SkillData skill, bool bUnitTarget);

    private delegate void SelectTargetPositionWithItem(int x, int y, ItemData item);

    private delegate void GridClickEvent(Grid grid);

    private delegate void UnitClickEvent(TacticsUnitController controller);

    private delegate void UnitFocusEvent(TacticsUnitController controller);

    private delegate void ScreenClickEvent(Vector2 position);

    private delegate void FocusTargetEvent(Unit unit);

    private delegate void SelectTargetEvent(Unit unit);

    private delegate void CancelTargetSelectEvent();
  }
}
