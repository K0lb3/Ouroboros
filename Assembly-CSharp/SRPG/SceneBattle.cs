// Decompiled with JetBrains decompiler
// Type: SRPG.SceneBattle
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    private List<BattleSceneSettings> mBattleScenes = new List<BattleSceneSettings>();
    private List<TacticsUnitController> mTacticsUnits = new List<TacticsUnitController>(20);
    private SceneBattle.CloseBattleUIWindow mCloseBattleUIWindow = new SceneBattle.CloseBattleUIWindow();
    private List<SceneBattle.TreasureSpawnInfo> mDroppedTreasures = new List<SceneBattle.TreasureSpawnInfo>(8);
    public SceneBattle.QuestEndEvent OnQuestEnd = (SceneBattle.QuestEndEvent) (() => {});
    private List<TacticsUnitController> mUnitsInBattle = new List<TacticsUnitController>();
    private Vector3 mDesiredCameraOffset = Vector3.get_zero();
    private readonly float MULTI_PLAY_INPUT_TIME_LIMIT_SEC = 10f;
    private readonly float MULTI_PLAY_INPUT_EXT_MOVE = 10f;
    private readonly float MULTI_PLAY_INPUT_EXT_SELECT = 10f;
    private readonly float SEND_TURN_SEC = 1f;
    private List<SceneBattle.MultiPlayRecvData> mRecvBattle = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvCheck = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvGoodJob = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvContinue = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvIgnoreMyDisconnect = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvResume = new List<SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayRecvData> mRecvResumeRequest = new List<SceneBattle.MultiPlayRecvData>();
    private Dictionary<int, SceneBattle.MultiPlayRecvData> mRecvCheckData = new Dictionary<int, SceneBattle.MultiPlayRecvData>();
    private Dictionary<int, SceneBattle.MultiPlayRecvData> mRecvCheckMyData = new Dictionary<int, SceneBattle.MultiPlayRecvData>();
    private List<SceneBattle.MultiPlayCheck> mMultiPlayCheckList = new List<SceneBattle.MultiPlayCheck>();
    private List<int> mResumeAlreadyStartPlayer = new List<int>();
    private string mPhotonErrString = string.Empty;
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
    private int mArenaEndFinish;
    private int mCurrentUnitStartX;
    private int mCurrentUnitStartY;
    private SceneBattle.StateTransitionRequest mOnRequestStateChange;
    private Json_Unit mEditorSupportUnit;
    private bool mIsFirstPlay;
    private bool mIsFirstWin;
    private bool mBattleStarted;
    private eDamageDispType mSkillDamageDispType;
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
    [NonSerialized]
    public SceneBattle.ExitRequests ExitRequest;
    private FlowNode_Network mReqSubmit;
    private bool mQuestResultSending;
    private bool mQuestResultSent;
    private int mFirstContact;
    private bool mRevertQuestNewIfRetire;
    private QuestResultData mSavedResult;
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
    private TargetCamera mTargetCamera;
    private bool mAllowCameraRotation;
    private bool mAllowCameraTranslation;
    private float mCameraAngle;
    private float mCameraYawMin;
    private float mCameraYawMax;
    private Vector2 mCameraTranslation;
    private float mDesiredCameraDistance;
    private bool _mUpdateCameraPosition;
    private Vector3 mCameraTarget;
    private Vector3 mBaseCameraOffset;
    private bool mDesiredCameraOffsetSet;
    private Vector3 mDesiredCameraTarget;
    private bool mDesiredCameraTargetSet;
    private float mDesiredCameraAngle;
    private bool mDesiredCameraAngleSet;
    private float mCameraAngleStart;
    private float mCameraRotateTime;
    private float mCameraRotateTimeMax;
    private List<SceneBattle.MultiPlayInput> mSendList;
    private float mSendTime;
    private bool mBeginMultiPlay;
    private SceneBattle.EDisconnectType mDisconnectType;
    private List<SceneBattle.MultiPlayer> mMultiPlayer;
    private List<SceneBattle.MultiPlayerUnit> mMultiPlayerUnit;
    private bool mResumeMultiPlay;
    private bool mResumeSend;
    private bool mResumeOnlyPlayer;
    private bool mResumeSuccess;
    private bool mMapViewMode;
    private bool mRetireComfirm;
    private bool mAlreadyEndBattle;
    private bool mCheater;
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
    [NonSerialized]
    private Unit mSelectedTarget;
    [NonSerialized]
    public bool UIParam_TargetValid;
    [NonSerialized]
    public AbilityData UIParam_CurrentAbility;
    private bool mIsWaitingForBattleSignal;
    private SceneBattle.TargetSelectorParam mTargetSelectorParam;
    private EUnitDirection mSkillDirectionByKouka;
    private bool mIsBackSelectSkill;
    private Quest_MoveUnit mUIMoveUnit;
    private GameObject mRenkeiAuraEffect;
    private GameObject mRenkeiAssistEffect;
    private GameObject mRenkeiChargeEffect;
    private GameObject mRenkeiHitEffect;
    private GameObject mSummonUnitEffect;
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
    private GameObject mUnitOwnerIndex;
    private GameObject mGemDrainHitEffect;
    private GameObject mTargetMarkerTemplate;
    private GameObject mAssistMarkerTemplate;
    private GameObject mBlockedGridMarker;
    private bool mDisplayBlockedGridMarker;
    private GameObject[] mUnitMarkerTemplates;
    private List<GameObject>[] mUnitMarkers;
    private bool mLoadedAllUI;
    private TouchController mTouchController;
    private GameObject mVersusPlayerTarget;
    private GameObject mVersusEnemyTarget;
    private SceneBattle.GridClickEvent mOnGridClick;
    private SceneBattle.UnitClickEvent mOnUnitClick;
    private SceneBattle.UnitFocusEvent mOnUnitFocus;
    private TacticsUnitController mFocusedUnit;
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

    public GameObject CurrentScene
    {
      get
      {
        return ((Component) this.mTacticsSceneRoot).get_gameObject();
      }
    }

    public QuestParam CurrentQuest
    {
      get
      {
        return this.mCurrentQuest;
      }
    }

    public BattleCore Battle
    {
      get
      {
        return this.mBattle;
      }
    }

    public int ArenaEndFinish
    {
      get
      {
        return this.mArenaEndFinish;
      }
      set
      {
        this.mArenaEndFinish = value;
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
            if (((Object) this.mBattleScenes[index]).get_name() == sceneName)
            {
              battleSceneSettings = this.mBattleScenes[index];
              break;
            }
          }
        }
        if (Object.op_Equality((Object) battleSceneSettings, (Object) null))
          battleSceneSettings = this.mDefaultBattleScene;
        this.mBattleSceneRoot = battleSceneSettings;
      }
      if (Object.op_Inequality((Object) this.mBattleSceneRoot, (Object) null))
        ((Component) this.mBattleSceneRoot).get_gameObject().SetActive(visible);
      if (Object.op_Inequality((Object) this.mTacticsSceneRoot, (Object) null))
        ((Component) this.mTacticsSceneRoot).get_gameObject().SetActive(!visible);
      RenderPipeline component = (RenderPipeline) ((Component) Camera.get_main()).GetComponent<RenderPipeline>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.EnableVignette = !visible;
    }

    private void ToggleUserInterface(bool isEnabled)
    {
      string[] strArray = new string[4]{ this.mBattleUI.QueueObjectID, this.mBattleUI.CameraControllerID, this.mBattleUI.QuestStatusID, this.mBattleUI.MapHeightID };
      foreach (string name in strArray)
      {
        Canvas gameObject = GameObjectID.FindGameObject<Canvas>(name);
        if (Object.op_Inequality((Object) gameObject, (Object) null))
          ((Behaviour) gameObject).set_enabled(isEnabled);
      }
      if (!this.Battle.IsMultiVersus)
        return;
      List<Unit> units = this.Battle.Units;
      if (units == null)
        return;
      for (int index = 0; index < units.Count; ++index)
      {
        TacticsUnitController unitController = this.FindUnitController(units[index]);
        if (Object.op_Inequality((Object) unitController, (Object) null))
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
      OInt oint;
      GlobalVars.MaxLevelInBattle = oint = (OInt) 0;
      GlobalVars.MaxDamageInBattle = oint;
      GlobalVars.MaxHpInBattle = oint;
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

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) SceneBattle.Instance, (Object) null))
        return;
      SceneBattle.Instance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) SceneBattle.Instance, (Object) this))
        return;
      SceneBattle.Instance = (SceneBattle) null;
    }

    private void OnDestroy()
    {
      LocalizedText.UnloadTable(SceneBattle.QUEST_TEXTTABLE);
      if (Object.op_Inequality((Object) this.mNavigation, (Object) null))
      {
        Object.Destroy((Object) this.mNavigation.get_gameObject());
        this.mNavigation = (GameObject) null;
        this.mTutorialTriggers = (FlowNode_TutorialTrigger[]) null;
      }
      this.CleanupGoodJob();
      this.DestroyCamera();
      this.DestroyUI();
      if (this.mBattle == null)
        return;
      this.mBattle.Release();
      this.mBattle = (BattleCore) null;
    }

    private void OnLoadTacticsScene(GameObject root)
    {
      TacticsSceneSettings component1 = (TacticsSceneSettings) root.GetComponent<TacticsSceneSettings>();
      if (Object.op_Equality((Object) component1, (Object) null))
        return;
      SceneAwakeObserver.RemoveListener(new SceneAwakeObserver.SceneEvent(this.OnLoadTacticsScene));
      CriticalSection.Leave(CriticalSections.SceneChange);
      GameUtility.DeactivateActiveChildComponents<Camera>((Component) component1);
      this.mTacticsSceneRoot = component1;
      RenderPipeline component2 = (RenderPipeline) ((Component) Camera.get_main()).GetComponent<RenderPipeline>();
      if (Object.op_Inequality((Object) component2, (Object) null))
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
      if (Object.op_Equality((Object) unitController, (Object) null))
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
        if (!Object.op_Equality((Object) mTacticsUnit, (Object) null))
        {
          if (Object.op_Inequality((Object) tacticsUnitController, (Object) null) && this.CalcCoord(tacticsUnitController.CenterPosition) == this.CalcCoord(mTacticsUnit.CenterPosition) && !mTacticsUnit.Unit.IsGimmick)
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
      if (Object.op_Inequality((Object) unitController, (Object) null))
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
      return new Vector3((float) grid.x + 0.5f, this.mHeightMap.get(grid.x, grid.y), (float) grid.y + 0.5f);
    }

    public Vector3 CalcGridCenter(int x, int y)
    {
      return this.CalcGridCenter(this.mBattle.CurrentMap[x, y]);
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
      if (this.mState != null && this.Battle.ResumeState != BattleCore.RESUME_STATE.WAIT)
        this.mState.Update();
      this.UpdateCameraControl(false);
      MyEncrypt.EncryptCount = 0;
      MyEncrypt.DecryptCount = 0;
      if (!this.mDownloadTutorialAssets || !GameUtility.Config_UseAssetBundles.Value || (!MonoSingleton<GameManager>.Instance.HasTutorialDLAssets || !AssetDownloader.isDone))
        ;
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
      return (IEnumerator) new SceneBattle.\u003CDownloadNextQuestAsync\u003Ec__Iterator32() { \u003C\u003Ef__this = this };
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
      return (IEnumerator) new SceneBattle.\u003CDownloadQuestAsync\u003Ec__Iterator33() { quest = quest, \u003C\u0024\u003Equest = quest, \u003C\u003Ef__this = this };
    }

    public void StartQuest(string questID, BattleCore.Json_Battle json)
    {
      this.mStartQuestCalled = true;
      this.StartCoroutine(this.StartQuestAsync(questID, json));
    }

    [DebuggerHidden]
    private IEnumerator StartQuestAsync(string questID, BattleCore.Json_Battle jsonBtl)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CStartQuestAsync\u003Ec__Iterator34() { questID = questID, jsonBtl = jsonBtl, \u003C\u0024\u003EquestID = questID, \u003C\u0024\u003EjsonBtl = jsonBtl, \u003C\u003Ef__this = this };
    }

    public void PlayBGM()
    {
      MonoSingleton<MySound>.Instance.PlayBGM(this.mBattle.CurrentMap.BGMName, (string) null);
    }

    public void StopBGM()
    {
      MonoSingleton<MySound>.Instance.PlayBGM((string) null, (string) null);
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
      return (IEnumerator) new SceneBattle.\u003CLoadMapAsync\u003Ec__Iterator35() { \u003C\u003Ef__this = this };
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
      if (this.IsPlayingArenaQuest)
      {
        this.GotoState<SceneBattle.State_ArenaCalc>();
      }
      else
      {
        if (this.Battle.IsMultiVersus)
          this.ArenaActionCountSet(this.Battle.RemainVersusTurnCount);
        if (this.Battle.CheckEnableSuspendStart() && this.Battle.LoadSuspendData())
        {
          if (!this.Battle.SuspendStart())
            return;
        }
        else
          this.Battle.MapStart();
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
        if (!Object.op_Equality((Object) unitController, (Object) null))
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
      if (Object.op_Equality((Object) unitController, (Object) null))
        return;
      unitController.ShowCursor(this.UnitCursor, color);
    }

    private void HideUnitCursor(Unit unit)
    {
      if (unit == null)
        return;
      TacticsUnitController unitController = this.FindUnitController(unit);
      if (Object.op_Equality((Object) unitController, (Object) null))
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
      if (Object.op_Inequality((Object) this.mBattleUI, (Object) null))
      {
        this.mBattleUI.OnInputTimeLimit();
        if (Object.op_Inequality((Object) this.mBattleUI.TargetMain, (Object) null))
          this.mBattleUI.TargetMain.ForceClose();
        if (Object.op_Inequality((Object) this.mBattleUI.TargetSub, (Object) null))
          this.mBattleUI.TargetSub.ForceClose();
        if (Object.op_Inequality((Object) this.mBattleUI.TargetObjectSub, (Object) null))
          this.mBattleUI.TargetObjectSub.ForceClose();
        if (Object.op_Inequality((Object) this.mBattleUI.CommandWindow, (Object) null))
          this.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
        if (Object.op_Inequality((Object) this.mBattleUI.ItemWindow, (Object) null))
          this.mBattleUI.ItemWindow.OnSelectItem = (BattleInventory.SelectEvent) null;
        if (Object.op_Inequality((Object) this.mBattleUI.SkillWindow, (Object) null))
          this.mBattleUI.SkillWindow.OnSelectSkill = (UnitAbilitySkillList.SelectSkillEvent) null;
        if (Object.op_Inequality((Object) this.mSkillTargetWindow, (Object) null))
          this.mSkillTargetWindow.ForceHide();
        if (this.mTacticsUnits != null)
        {
          for (int index = 0; index < this.mTacticsUnits.Count; ++index)
          {
            if (!Object.op_Equality((Object) this.mTacticsUnits[index], (Object) null))
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
      if (this.mBattle.CurrentUnit != null && this.mBattle.CurrentUnit.CastSkill != null && !unitList.Contains(this.mBattle.CurrentUnit))
        unitList.Add(this.mBattle.CurrentUnit);
      if (unitList.Count <= 0)
        return;
      List<SceneBattle.ChargeTarget> chargeTargetList = new List<SceneBattle.ChargeTarget>();
      Color32 src1 = Color32.op_Implicit(GameSettings.Instance.Colors.ChargeAreaGrn);
      Color32 src2 = Color32.op_Implicit(GameSettings.Instance.Colors.ChargeAreaRed);
      GridMap<Color32> grid1 = new GridMap<Color32>(this.mBattle.CurrentMap.Width, this.mBattle.CurrentMap.Height);
      GridMap<Color32> grid2 = new GridMap<Color32>(this.mBattle.CurrentMap.Width, this.mBattle.CurrentMap.Height);
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneBattle.\u003CInternalShowCastSkill\u003Ec__AnonStorey1C1 skillCAnonStorey1C1 = new SceneBattle.\u003CInternalShowCastSkill\u003Ec__AnonStorey1C1();
      using (List<Unit>.Enumerator enumerator = unitList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          skillCAnonStorey1C1.unit = enumerator.Current;
          bool flag = false;
          // ISSUE: reference to a compiler-generated field
          if (skillCAnonStorey1C1.unit.CastSkill.IsAdvantage())
            flag = true;
          int num1 = 0;
          int num2 = 0;
          // ISSUE: reference to a compiler-generated field
          if (skillCAnonStorey1C1.unit.UnitTarget != null)
          {
            // ISSUE: reference to a compiler-generated field
            num1 = skillCAnonStorey1C1.unit.UnitTarget.x;
            // ISSUE: reference to a compiler-generated field
            num2 = skillCAnonStorey1C1.unit.UnitTarget.y;
            // ISSUE: reference to a compiler-generated field
            if (skillCAnonStorey1C1.unit.UnitTarget == this.mBattle.CurrentUnit)
            {
              // ISSUE: reference to a compiler-generated field
              IntVector2 intVector2 = this.CalcCoord(this.FindUnitController(skillCAnonStorey1C1.unit.UnitTarget).CenterPosition);
              num1 = intVector2.x;
              num2 = intVector2.y;
            }
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (skillCAnonStorey1C1.unit.GridTarget != null)
            {
              // ISSUE: reference to a compiler-generated field
              num1 = skillCAnonStorey1C1.unit.GridTarget.x;
              // ISSUE: reference to a compiler-generated field
              num2 = skillCAnonStorey1C1.unit.GridTarget.y;
            }
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          GridMap<bool> scopeGridMap = this.mBattle.CreateScopeGridMap(skillCAnonStorey1C1.unit, skillCAnonStorey1C1.unit.x, skillCAnonStorey1C1.unit.y, num1, num2, skillCAnonStorey1C1.unit.CastSkill);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (skillCAnonStorey1C1.unit.CastSkill.SkillParam.select_scope == ESelectType.Laser && skillCAnonStorey1C1.unit.UnitTarget != null)
            scopeGridMap.set(num1, num2, true);
          for (int x = 0; x < scopeGridMap.w; ++x)
          {
            if (x < grid1.w)
            {
              for (int y = 0; y < scopeGridMap.h; ++y)
              {
                if (y < grid1.h && scopeGridMap.get(x, y))
                {
                  // ISSUE: reference to a compiler-generated field
                  if (skillCAnonStorey1C1.unit.UnitTarget != null && x == num1 && y == num2)
                  {
                    // ISSUE: reference to a compiler-generated method
                    SceneBattle.ChargeTarget chargeTarget = chargeTargetList.Find(new Predicate<SceneBattle.ChargeTarget>(skillCAnonStorey1C1.\u003C\u003Em__162));
                    if (chargeTarget != null)
                    {
                      chargeTarget.AddAttr(!flag ? 2U : 1U);
                    }
                    else
                    {
                      // ISSUE: reference to a compiler-generated field
                      chargeTargetList.Add(new SceneBattle.ChargeTarget(skillCAnonStorey1C1.unit.UnitTarget, !flag ? 2U : 1U));
                    }
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
          if (Object.op_Inequality((Object) unitController, (Object) null))
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
        if (!unit2.IsDead && unit2.IsEntry && (!unit2.IsSub && unit2.Side == targetSide) && (map.isValid(unit2.x, unit2.y) && map.get(unit2.x, unit2.y)))
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
      if (Object.op_Inequality((Object) unitController, (Object) null) && !currentUnit.IsUnitFlag(EUnitFlag.Moved))
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
          this.SendInputGridXY(this.Battle.CurrentUnit, intVector2.x, intVector2.y, currentUnit.Direction);
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
      if (!this.Battle.UseSkill(currentUnit, x, y, skill, bUnitLockTarget, 0, 0))
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
      if (!Object.op_Inequality((Object) unitController, (Object) null))
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
      if (!Object.op_Inequality((Object) this.mTacticsSceneRoot, (Object) null))
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
      if (Object.op_Inequality((Object) unitController, (Object) null) && unitController.IsJumpCant())
      {
        this.mBattle.CommandWait();
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
            this.mBattle.CommandWait();
            this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
          }
        }
        else
        {
          for (int index = 0; index < this.mTacticsUnits.Count; ++index)
            this.mTacticsUnits[index].AutoUpdateRotation = true;
          if (currentUnit.IsControl && this.mAutoActivateGimmick && (!currentUnit.IsUnitFlag(EUnitFlag.Action) && this.mBattle.CheckGridEventTrigger(currentUnit, EEventTrigger.ExecuteOnGrid, false)))
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
                if (this.Battle.IsMultiVersus)
                {
                  this.RefreshOnlyMapCommand();
                  this.mBattleUI.OnCommandSelectStart();
                  this.GotoState_WaitSignal<SceneBattle.State_MapCommandVersus>();
                }
                else
                  this.GotoState_WaitSignal<SceneBattle.State_MapCommandMultiPlay>();
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
              this.mBattle.CommandWait();
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
      if (Object.op_Inequality((Object) this.mBattleUI, (Object) null))
      {
        this.mBattleUI.OnQuestEnd();
        this.mBattleUI.OnMapEnd();
      }
      this.mBattleUI.OnMapEnd();
      if (Object.op_Inequality((Object) this.mBattleUI_MultiPlay, (Object) null))
        this.mBattleUI_MultiPlay.OnMapEnd();
      this.EndMultiPlayer();
      this.GotoState_WaitSignal<SceneBattle.State_MapEndV2>();
    }

    private void TriggerWinEvent()
    {
      if (this.Battle.GetQuestResult() == BattleCore.QuestResult.Win && Object.op_Inequality((Object) this.mEventScript, (Object) null))
      {
        this.mEventSequence = this.mEventScript.OnQuestWin();
        if (Object.op_Inequality((Object) this.mEventSequence, (Object) null))
        {
          this.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_PreQuestResult>>();
          return;
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
          MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
        BattleCore.RemoveSuspendData();
        this.mQuestResultSent = true;
      }
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
          TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
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
        else if (GlobalVars.SelectedMultiPlayVersusType != VERSUS_TYPE.Tower && this.Battle.IsMultiVersus)
        {
          WebAPI.JSON_BodyResponse<Json_VersusEndEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_VersusEndEnd>>(www.text);
          MonoSingleton<GameManager>.Instance.SetVersusWinCount(jsonObject.body.wincnt);
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        else
        {
          WebAPI.JSON_BodyResponse<Json_BtlComEnd> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_BtlComEnd>>(www.text);
          json = jsonObject.body.quests;
          jsonBodyResponse = new WebAPI.JSON_BodyResponse<Json_PlayerDataAll>();
          jsonBodyResponse.body = (Json_PlayerDataAll) jsonObject.body;
        }
        if (jsonBodyResponse.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            int multiCoin1 = MonoSingleton<GameManager>.Instance.Player.MultiCoin;
            MonoSingleton<GameManager>.Instance.Deserialize(jsonBodyResponse.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonBodyResponse.body.units);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonBodyResponse.body.items);
            if (json != null)
              MonoSingleton<GameManager>.Instance.Deserialize(json);
            if (jsonBodyResponse.body.mails != null)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonBodyResponse.body.mails);
            if (jsonBodyResponse.body.fuids != null && this.mCurrentQuest.type == QuestTypes.Multi)
              MonoSingleton<GameManager>.Instance.Deserialize(jsonBodyResponse.body.fuids);
            int multiCoin2 = MonoSingleton<GameManager>.Instance.Player.MultiCoin;
            if (this.Battle.IsMultiPlay)
              this.Battle.GetQuestRecord().multicoin = (OInt) (multiCoin2 - multiCoin1);
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
            this.ArenaEndFinish = 2;
          }
          if (this.mBattle.IsAutoBattle)
            GameUtility.SetDefaultSleepSetting();
          if (this.IsPlayingMultiQuest)
            this.mFirstContact = jsonBodyResponse.body.first_contact;
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
          this.mQuestResultSent = true;
          Network.RemoveAPI();
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
      int[] missions = new int[3];
      for (int index = 0; index < missions.Length; ++index)
        missions[index] = (questRecord.bonusFlags & 1 << index) == 0 ? 0 : 1;
      this.UpdateTrophy();
      string trophy_progs;
      string bingo_progs;
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      string maxdata = string.Format("\"hpleveldamage\": [ {0}, {1}, {2} ]", (object) GlobalVars.MaxHpInBattle, (object) GlobalVars.MaxLevelInBattle, (object) GlobalVars.MaxDamageInBattle);
      if (quest.type == QuestTypes.Arena)
        Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, time, result, beats, itemSteals, goldSteals, missions, (string[]) null, questRecord.used_items, new Network.ResponseCallback(this.SubmitArenaResultCallback), BtlEndTypes.colo, trophy_progs, bingo_progs, maxdata), false);
      else if (quest.type == QuestTypes.Tower)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        int round = (int) instance.TowerResuponse.round;
        TowerFloorParam towerFloorParam = instance.TowerResuponse.GetCurrentFloor() ?? instance.FindTowerFloor(quest.iname);
        byte floor = towerFloorParam == null ? (byte) 0 : towerFloorParam.floor;
        Network.RequestAPI((WebAPI) new ReqTowerBtlComEnd(btlid, this.Battle.Player.ToArray(), this.Battle.Enemys.ToArray(), this.Battle.ActionCount, round, floor, result, this.Battle.Map[0].mRandDeckResult, new Network.ResponseCallback(this.SubmitResultCallback), trophy_progs, bingo_progs, maxdata), false);
      }
      else if (quest.type == QuestTypes.VersusFree || quest.type == QuestTypes.VersusRank)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SceneBattle.\u003CSubmitBattleResult\u003Ec__AnonStorey1C2 resultCAnonStorey1C2 = new SceneBattle.\u003CSubmitBattleResult\u003Ec__AnonStorey1C2();
        // ISSUE: reference to a compiler-generated field
        resultCAnonStorey1C2.pt = PunMonoSingleton<MyPhoton>.Instance;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        JSON_MyPhotonPlayerParam photonPlayerParam = resultCAnonStorey1C2.pt.GetMyPlayersStarted().Find(new Predicate<JSON_MyPhotonPlayerParam>(resultCAnonStorey1C2.\u003C\u003Em__163));
        Network.RequestAPI((WebAPI) new ReqVersusEnd(btlid, time, result, beats, photonPlayerParam.UID, photonPlayerParam.FUID, new Network.ResponseCallback(this.SubmitResultCallback), GlobalVars.SelectedMultiPlayVersusType, trophy_progs, bingo_progs, maxdata), false);
      }
      else
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
        string[] fuid = (string[]) null;
        if (GlobalVars.LastQuestResult.Get() == BattleCore.QuestResult.Win && myPlayersStarted != null)
        {
          fuid = new string[myPlayersStarted.Count];
          int num = 0;
          for (int index = 0; index < myPlayersStarted.Count; ++index)
          {
            JSON_MyPhotonPlayerParam photonPlayerParam = myPlayersStarted[index];
            if (photonPlayerParam != null && photonPlayerParam.playerIndex != instance.MyPlayerIndex && !string.IsNullOrEmpty(photonPlayerParam.FUID))
              fuid[num++] = photonPlayerParam.FUID;
          }
        }
        Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, time, result, beats, itemSteals, goldSteals, missions, fuid, questRecord.used_items, new Network.ResponseCallback(this.SubmitResultCallback), !isMultiPlay ? BtlEndTypes.com : BtlEndTypes.multi, trophy_progs, bingo_progs, maxdata), false);
      }
      MonoSingleton<GameManager>.Instance.Player.ResetMissionClearAt();
    }

    private void SubmitScenarioResult(long btlid)
    {
      Network.RequestAPI((WebAPI) new ReqBtlComEnd(btlid, 0, BtlResultTypes.Win, (int[]) null, (int[]) null, (int[]) null, (int[]) null, (string[]) null, (Dictionary<OString, OInt>) null, new Network.ResponseCallback(this.SubmitResultCallback), BtlEndTypes.com, (string) null, (string) null, (string) null), false);
      MonoSingleton<GameManager>.Instance.Player.MarkQuestCleared(this.mCurrentQuest.iname);
    }

    public void ForceEndQuest()
    {
      this.mExecDisconnected = true;
      if (this.Battle.GetQuestResult() == BattleCore.QuestResult.Pending && this.Battle.IsMultiVersus)
      {
        this.SendRetire();
        this.GotoState<SceneBattle.State_RetireComfirm>();
      }
      else
      {
        if (this.IsInState<SceneBattle.State_ExitQuest>())
          return;
        this.GotoState<SceneBattle.State_ExitQuest>();
      }
    }

    private void UpdateTrophy()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      BattleCore.QuestResult questResult = this.mBattle.GetQuestResult();
      if (this.mCurrentQuest.type == QuestTypes.Arena)
      {
        if (this.mBattle.GetQuestRecord().result == BattleCore.QuestResult.Win)
          player.OnQuestWin(this.mCurrentQuest.iname);
        else
          player.OnQuestLose(this.mCurrentQuest.iname);
      }
      else
      {
        switch (questResult)
        {
          case BattleCore.QuestResult.Win:
            player.OnQuestWin(this.mCurrentQuest.iname);
            BattleCore.Record questRecord = this.Battle.GetQuestRecord();
            for (int index = 0; index < questRecord.items.Count; ++index)
              player.OnItemQuantityChange(questRecord.items[index].iname, 1);
            if ((int) questRecord.gold > 0)
              player.OnGoldChange((int) questRecord.gold);
            if (this.mStartPlayerLevel >= player.Lv)
              break;
            player.OnPlayerLevelChange(player.Lv - this.mStartPlayerLevel);
            break;
          case BattleCore.QuestResult.Lose:
            player.OnQuestLose(this.mCurrentQuest.iname);
            break;
        }
      }
    }

    public void ForceEndQuestInArena()
    {
      if (this.Battle.IsArenaSkip)
        return;
      this.Battle.IsArenaSkip = true;
      this.Pause(false);
      GlobalEvent.Invoke("CLOSE_QUESTMENU", (object) this);
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
      this.mSavedResult = new QuestResultData(MonoSingleton<GameManager>.Instance.Player, (int) this.mCurrentQuest.clear_missions, this.mBattle.GetQuestRecord(), this.Battle.AllUnits, this.mIsFirstWin);
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
            player.GainItem(questRecord.items[index].iname, 1);
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
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "TUTORIAL_EXIT");
      else
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "EXIT");
    }

    public void Pause(bool flag)
    {
      if (flag)
        ++this.mPauseReqCount;
      else
        this.mPauseReqCount = Math.Max(0, this.mPauseReqCount - 1);
      if ((double) TimeManager.PreviousTimeScale > 1.0)
        TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.Game, this.mPauseReqCount <= 0 ? TimeManager.PreviousTimeScale : 0.0f);
      else
        TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.Game, this.mPauseReqCount <= 0 ? 1f : 0.0f);
    }

    public bool IsPause()
    {
      return this.mPauseReqCount != 0;
    }

    public void CleanUpMultiPlay()
    {
      this.CleanupGoodJob();
      PunMonoSingleton<MyPhoton>.Instance.Reset();
      DebugUtility.Log("CleanUpMultiPlay done.");
    }

    private SceneBattle.PosRot GetCameraOffset_Unit()
    {
      Transform transform = ((Component) GameSettings.Instance.Quest.UnitCamera).get_transform();
      return new SceneBattle.PosRot() { Position = transform.get_localPosition(), Rotation = transform.get_localRotation() };
    }

    private void GotoPrepareSkill()
    {
      LogSkill peek = this.mBattle.Logs.Peek as LogSkill;
      if (peek != null && peek.self != null && peek.self.Side == EUnitSide.Player)
        this.mLastPlayerSideUseSkillUnit = peek.self;
      if (this.mBattle.IsUnitAuto(this.mBattle.CurrentUnit) || this.mBattle.EntryBattleMultiPlayTimeUp)
        this.HideGrid();
      this.CancelMapViewMode();
      this.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_PrepareSkill>>();
    }

    private void CancelMapViewMode()
    {
      if (!this.Battle.IsMultiVersus || !this.VersusMapView)
        return;
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        this.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
        this.mTacticsUnits[index].SetHPChangeYosou(this.mTacticsUnits[index].VisibleHPValue);
      }
      this.mBattleUI.OnCommandSelect();
      if (Object.op_Inequality((Object) this.mBattleUI.TargetMain, (Object) null))
        this.mBattleUI.TargetMain.Close();
      if (Object.op_Inequality((Object) this.mBattleUI.TargetSub, (Object) null))
        this.mBattleUI.TargetSub.Close();
      if (Object.op_Inequality((Object) this.mBattleUI.TargetObjectSub, (Object) null))
        this.mBattleUI.TargetObjectSub.Close();
      this.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
      this.mBattleUI.OnMapViewEnd();
      this.VersusMapView = false;
    }

    private Unit mCollaboTargetUnit
    {
      get
      {
        if (Object.op_Implicit((Object) this.mCollaboTargetTuc))
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
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.FlipHorizontally = mirror;
      this.isScreenMirroring = mirror;
    }

    private void InitCamera()
    {
      GameSettings instance = GameSettings.Instance;
      CameraHook.AddPreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
      RenderPipeline.Setup(Camera.get_main());
      this.mTargetCamera = GameUtility.RequireComponent<TargetCamera>(((Component) Camera.get_main()).get_gameObject());
      this.mTargetCamera.Pitch = instance.GameCamera_AngleX;
      this.mCameraYawMin = instance.GameCamera_YawMin;
      this.mCameraYawMax = instance.GameCamera_YawMax;
      this.mCameraAngle = instance.GameCamera_YawMin;
      this.mDesiredCameraDistance = instance.GameCamera_DefaultDistance;
    }

    private void DestroyCamera()
    {
      CameraHook.RemovePreCullEventListener(new CameraHook.PreCullEvent(this.OnCameraPreCull));
    }

    private void OnCameraPreCull(Camera cam)
    {
      if (!(((Component) cam).get_tag() == "MainCamera") || ((Component) cam).get_gameObject().get_layer() != GameUtility.LayerDefault)
        return;
      this.LayoutPopups(cam);
      this.LayoutGauges(cam);
    }

    public bool mUpdateCameraPosition
    {
      get
      {
        return this._mUpdateCameraPosition;
      }
      set
      {
        this._mUpdateCameraPosition = value;
      }
    }

    private void SetCameraOffset(Transform transform)
    {
    }

    private void InterpCameraOffset(Transform transform)
    {
    }

    private void SetCameraTarget(Component position)
    {
      this.SetCameraTarget(position.get_transform().get_position());
    }

    private void SetCameraTarget(Vector3 position)
    {
      this.mCameraTarget = position;
      this.mDesiredCameraTargetSet = false;
    }

    private void SetCameraTarget(float x, float y)
    {
      this.mCameraTarget.x = (__Null) (double) x;
      this.mCameraTarget.y = (__Null) (double) this.CalcHeight(x, y);
      this.mCameraTarget.z = (__Null) (double) y;
    }

    private void InterpCameraTarget(Component position)
    {
      this.InterpCameraTarget(position.get_transform().get_position());
    }

    private void InterpCameraTarget(Vector3 position)
    {
      this.mDesiredCameraTarget = position;
      this.mDesiredCameraTargetSet = true;
      this.mUpdateCameraPosition = true;
    }

    private void InterpCameraTargetToCurrent()
    {
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      if (!Object.op_Inequality((Object) unitController, (Object) null))
        return;
      this.InterpCameraTarget((Component) unitController);
    }

    private void InterpCameraDistance(float distance)
    {
      if (GameUtility.Config_SkillAnimation)
        return;
      this.mDesiredCameraDistance = GameSettings.Instance.GameCamera_DefaultDistance;
    }

    public void SetCameraYawRange(float min, float max)
    {
      this.mCameraYawMin = min;
      this.mCameraYawMax = max;
    }

    public void RotateCamera(float delta, float duration)
    {
      if (GlobalVars.SelectedQuestID == "QE_OP_0002" && !MonoSingleton<GameManager>.Instance.BattleGuidanceComplete())
        return;
      float num = this.mCameraYawMax - this.mCameraYawMin;
      this.mCameraAngleStart = this.mCameraAngle;
      this.mDesiredCameraAngleSet = true;
      this.mDesiredCameraAngle = this.mCameraAngle + delta * num;
      this.mDesiredCameraAngle = Mathf.Clamp(this.mDesiredCameraAngle, this.mCameraYawMin, this.mCameraYawMax);
      this.mCameraRotateTime = 0.0f;
      this.mCameraRotateTimeMax = duration;
    }

    public float CameraYawRatio
    {
      get
      {
        return Mathf.Clamp01((float) (((double) this.mCameraAngle - (double) this.mCameraYawMin) / ((double) this.mCameraYawMax - (double) this.mCameraYawMin)));
      }
    }

    private void UpdateCameraControl(bool immediate = false)
    {
      GameSettings instance = GameSettings.Instance;
      Camera main = Camera.get_main();
      Transform transform = !Object.op_Inequality((Object) main, (Object) null) ? (Transform) null : ((Component) main).get_transform();
      if (Object.op_Inequality((Object) this.mTacticsSceneRoot, (Object) null) && ((Component) this.mTacticsSceneRoot).get_gameObject().get_activeInHierarchy())
        main.set_fieldOfView(GameSettings.Instance.GameCamera_TacticsSceneFOV);
      else if (Object.op_Inequality((Object) this.mBattleSceneRoot, (Object) null) && ((Component) this.mBattleSceneRoot).get_gameObject().get_activeInHierarchy())
        main.set_fieldOfView(GameSettings.Instance.GameCamera_BattleSceneFOV);
      if (Object.op_Inequality((Object) this.mTouchController, (Object) null) && !ObjectAnimator.Get((Component) main).isMoving && this.mUpdateCameraPosition)
      {
        if (this.mDesiredCameraAngleSet)
        {
          this.mCameraRotateTime = !immediate ? Mathf.Min(this.mCameraRotateTime + Time.get_deltaTime(), this.mCameraRotateTimeMax) : this.mCameraRotateTimeMax;
          this.mCameraAngle = Mathf.Lerp(this.mCameraAngleStart, this.mDesiredCameraAngle, Mathf.Sin((float) (1.57079637050629 * ((double) this.mCameraRotateTime / (double) this.mCameraRotateTimeMax))));
          if ((double) this.mCameraRotateTime >= (double) this.mCameraRotateTimeMax)
            this.mDesiredCameraAngleSet = false;
        }
        else if (this.mAllowCameraRotation)
        {
          float num1 = (float) -this.mTouchController.AngularVelocity.x * (float) (1.0 / (double) Screen.get_width() * 180.0);
          float cameraYawSoftLimit = instance.GameCamera_YawSoftLimit;
          float num2 = 2f;
          if ((double) this.mCameraAngle < (double) this.mCameraYawMin && (double) num1 < 0.0)
          {
            float num3 = Mathf.Pow(1f - Mathf.Clamp01((float) -((double) this.mCameraAngle - (double) this.mCameraYawMin) / cameraYawSoftLimit), num2);
            num1 *= num3;
          }
          else if ((double) this.mCameraAngle > (double) this.mCameraYawMax && (double) num1 > 0.0)
          {
            float num3 = Mathf.Pow(1f - Mathf.Clamp01((this.mCameraAngle - this.mCameraYawMax) / cameraYawSoftLimit), num2);
            num1 *= num3;
          }
          this.mCameraAngle += num1;
          if (!SRPG_TouchInputModule.IsMultiTouching)
          {
            float num3 = !immediate ? Time.get_deltaTime() * 10f : 1f;
            if ((double) this.mCameraAngle < (double) this.mCameraYawMin)
              this.mCameraAngle = Mathf.Lerp(this.mCameraAngle, this.mCameraYawMin, num3);
            else if ((double) this.mCameraAngle > (double) this.mCameraYawMax)
              this.mCameraAngle = Mathf.Lerp(this.mCameraAngle, this.mCameraYawMax, num3);
          }
        }
        // ISSUE: explicit reference operation
        if (this.mAllowCameraTranslation && (double) ((Vector2) @this.mTouchController.Velocity).get_magnitude() > 0.0)
        {
          Vector2 velocity = this.mTouchController.Velocity;
          Vector3 forward = transform.get_forward();
          Vector3 right = transform.get_right();
          forward.y = (__Null) 0.0;
          // ISSUE: explicit reference operation
          ((Vector3) @forward).Normalize();
          right.y = (__Null) 0.0;
          // ISSUE: explicit reference operation
          ((Vector3) @right).Normalize();
          Vector3 screenPoint = main.WorldToScreenPoint(this.mCameraTarget);
          Vector2 vector2 = Vector2.op_Implicit(Vector3.op_Subtraction(main.WorldToScreenPoint(Vector3.op_Addition(Vector3.op_Addition(this.mCameraTarget, right), forward)), screenPoint));
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
          ((Vector2) @velocity).\u002Ector((float) (right.x * velocity.x + forward.x * velocity.y), (float) (right.z * velocity.x + forward.z * velocity.y));
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local3 = @this.mCameraTarget;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local3).x = (^local3).x - velocity.x;
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector3& local4 = @this.mCameraTarget;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^local4).z = (^local4).z - velocity.y;
          this.mCameraTarget.x = (__Null) (double) Mathf.Clamp((float) this.mCameraTarget.x, 0.1f, (float) this.mBattle.CurrentMap.Width - 0.1f);
          this.mCameraTarget.z = (__Null) (double) Mathf.Clamp((float) this.mCameraTarget.z, 0.1f, (float) this.mBattle.CurrentMap.Height - 0.1f);
          this.mCameraTarget.y = (__Null) (double) this.CalcHeight((float) this.mCameraTarget.x, (float) this.mCameraTarget.z);
        }
        this.mTouchController.AngularVelocity = Vector2.get_zero();
        this.mTouchController.Velocity = Vector2.get_zero();
      }
      if (this.mUpdateCameraPosition)
      {
        float num = !immediate ? Time.get_deltaTime() * 8f : 1f;
        if (this.mDesiredCameraOffsetSet)
        {
          this.mBaseCameraOffset = Vector3.Lerp(this.mBaseCameraOffset, this.mDesiredCameraOffset, num);
          Vector3 vector3 = Vector3.op_Subtraction(this.mDesiredCameraOffset, this.mBaseCameraOffset);
          // ISSUE: explicit reference operation
          if ((double) ((Vector3) @vector3).get_magnitude() <= 0.00999999977648258)
          {
            this.mBaseCameraOffset = this.mDesiredCameraOffset;
            this.mDesiredCameraOffsetSet = false;
          }
        }
        if (this.mDesiredCameraTargetSet)
        {
          this.mCameraTarget = Vector3.Lerp(this.mCameraTarget, this.mDesiredCameraTarget, num);
          Vector3 vector3 = Vector3.op_Subtraction(this.mDesiredCameraTarget, this.mCameraTarget);
          // ISSUE: explicit reference operation
          if ((double) ((Vector3) @vector3).get_magnitude() <= 0.00999999977648258)
          {
            this.mCameraTarget = this.mDesiredCameraTarget;
            this.mDesiredCameraTargetSet = false;
          }
        }
        this.mTargetCamera.CameraDistance = Mathf.Lerp(this.mTargetCamera.CameraDistance, this.mDesiredCameraDistance, num);
        this.mTargetCamera.SetPositionYaw(Vector3.op_Addition(this.mCameraTarget, Vector3.op_Multiply(Vector3.get_up(), instance.GameCamera_UnitHeightOffset)), this.mCameraAngle);
      }
      if (this.mOnUnitFocus != null)
      {
        if (this.mDesiredCameraTargetSet)
          return;
        TacticsUnitController closestUnitController = this.FindClosestUnitController(this.mCameraTarget, 1f);
        if (!Object.op_Inequality((Object) this.mFocusedUnit, (Object) closestUnitController))
          return;
        DebugUtility.Log("Focus:" + (object) closestUnitController);
        this.mFocusedUnit = closestUnitController;
        this.mOnUnitFocus(this.mFocusedUnit);
      }
      else
        this.mFocusedUnit = (TacticsUnitController) null;
    }

    private void ResetCameraTarget()
    {
      this.mTargetCamera.Reset();
      this.mCameraAngle = this.mTargetCamera.Yaw;
      this.mDesiredCameraDistance = this.mTargetCamera.CameraDistance;
      this.mCameraTarget = this.mTargetCamera.TargetPosition;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector3& local = @this.mCameraTarget;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local).y = (__Null) ((^local).y - (double) GameSettings.Instance.GameCamera_UnitHeightOffset);
    }

    private bool IsCameraMoving
    {
      get
      {
        return ObjectAnimator.Get((Component) Camera.get_main()).isMoving || this.mUpdateCameraPosition && (this.mDesiredCameraOffsetSet || this.mDesiredCameraTargetSet);
      }
    }

    public void SetMoveCamera()
    {
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      this.InterpCameraOffset(GameSettings.Instance.Quest.MoveCamera);
      this.InterpCameraTarget((Component) ((Component) unitController).get_transform());
      this.mTargetCamera.Pitch = GameSettings.Instance.GameCamera_AngleX;
    }

    private void MultiPlayLog(string str)
    {
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

    public bool IsRetireComfirm
    {
      get
      {
        return this.mRetireComfirm;
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
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SceneBattle.\u003CCreateMultiPlayer\u003Ec__AnonStorey1C3 playerCAnonStorey1C3 = new SceneBattle.\u003CCreateMultiPlayer\u003Ec__AnonStorey1C3();
        Unit allUnit = this.Battle.AllUnits[unitID];
        if (allUnit != null)
        {
          // ISSUE: reference to a compiler-generated field
          playerCAnonStorey1C3.playerIndex = allUnit.OwnerPlayerIndex;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (playerCAnonStorey1C3.playerIndex != instance.MyPlayerIndex && playerCAnonStorey1C3.playerIndex > 0)
          {
            // ISSUE: reference to a compiler-generated method
            SceneBattle.MultiPlayer owner = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(playerCAnonStorey1C3.\u003C\u003Em__164));
            if (owner != null)
              this.mMultiPlayerUnit.Add(new SceneBattle.MultiPlayerUnit(this, unitID, allUnit, owner));
          }
        }
      }
      this.mSendList = new List<SceneBattle.MultiPlayInput>();
      this.mResumeMultiPlay = instance.IsResume();
      this.mResumeAlreadyStartPlayer.Clear();
    }

    private void BeginMultiPlayer()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneBattle.\u003CBeginMultiPlayer\u003Ec__AnonStorey1C4 playerCAnonStorey1C4 = new SceneBattle.\u003CBeginMultiPlayer\u003Ec__AnonStorey1C4();
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
        // ISSUE: reference to a compiler-generated field
        playerCAnonStorey1C4.unit = this.Battle.CurrentUnit;
        // ISSUE: reference to a compiler-generated method
        this.mCurrentSendInputUnitID = this.Battle.AllUnits.FindIndex(new Predicate<Unit>(playerCAnonStorey1C4.\u003C\u003Em__165));
        this.MultiPlayLog("[PUN]BeginMultiPlayer********** turn:" + (object) this.UnitStartCountTotal + " unitID:" + (object) this.mCurrentSendInputUnitID + " sqID:" + (object) this.mMultiPlaySendID);
        // ISSUE: reference to a compiler-generated method
        SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(playerCAnonStorey1C4.\u003C\u003Em__166));
        if (multiPlayer != null)
          multiPlayer.Begin(this);
        // ISSUE: reference to a compiler-generated method
        SceneBattle.MultiPlayerUnit multiPlayerUnit = this.mMultiPlayerUnit.Find(new Predicate<SceneBattle.MultiPlayerUnit>(playerCAnonStorey1C4.\u003C\u003Em__167));
        if (multiPlayerUnit != null)
          multiPlayerUnit.Begin(this);
        // ISSUE: reference to a compiler-generated field
        if (playerCAnonStorey1C4.unit.OwnerPlayerIndex == this.Battle.MyPlayerIndex)
          this.MultiPlayInputTimeLimit = this.MULTI_PLAY_INPUT_TIME_LIMIT_SEC;
        if (!this.Battle.IsMultiVersus)
          return;
        this.CloseBattleUI();
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

    private string CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader header, int unitID, List<SceneBattle.MultiPlayInput> sendList)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneBattle.\u003CCreateMultiPlayInputList\u003Ec__AnonStorey1C6 listCAnonStorey1C6 = new SceneBattle.\u003CCreateMultiPlayInputList\u003Ec__AnonStorey1C6();
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
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SceneBattle.\u003CCreateMultiPlayInputList\u003Ec__AnonStorey1C5 listCAnonStorey1C5 = new SceneBattle.\u003CCreateMultiPlayInputList\u003Ec__AnonStorey1C5();
        using (List<SceneBattle.MultiPlayInput>.Enumerator enumerator = sendList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            // ISSUE: reference to a compiler-generated field
            listCAnonStorey1C5.input = enumerator.Current;
            // ISSUE: reference to a compiler-generated field
            if (listCAnonStorey1C5.input.c == 6)
            {
              // ISSUE: reference to a compiler-generated field
              multiPlayInputList1.Add(listCAnonStorey1C5.input);
            }
            else
            {
              // ISSUE: reference to a compiler-generated method
              multiPlayInputList1.RemoveAll(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C5.\u003C\u003Em__168));
              // ISSUE: reference to a compiler-generated field
              multiPlayInputList1.Add(listCAnonStorey1C5.input);
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
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey1C6.def = new SceneBattle.MultiPlayInput();
      ++this.mMultiPlaySendID;
      string str1 = "{\"h\":" + (object) header + ",\"b\":" + (object) this.UnitStartCountTotal + ",\"sq\":" + (object) this.mMultiPlaySendID + ",\"pidx\":" + (object) myPlayerIndex + ",\"pid\":" + (object) num1 + ",\"uid\":" + (object) unitID;
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__169)) != null)
      {
        string str2 = str1 + ",\"c\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].c;
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__16A)) != null)
      {
        string str2 = str1 + ",\"u\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].u;
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__16B)) != null)
      {
        string str2 = str1 + ",\"s\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? ",\"" : "\"") + JsonEscape.Escape(sendList[index].s) + "\"";
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__16C)) != null)
      {
        string str2 = str1 + ",\"i\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? ",\"" : "\"") + JsonEscape.Escape(sendList[index].i) + "\"";
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__16D)) != null)
      {
        string str2 = str1 + ",\"gx\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].gx;
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__16E)) != null)
      {
        string str2 = str1 + ",\"gy\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].gy;
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__16F)) != null)
      {
        string str2 = str1 + ",\"ul\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].ul;
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__170)) != null)
      {
        string str2 = str1 + ",\"d\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].d;
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__171)) != null)
      {
        string str2 = str1 + ",\"x\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].x;
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__172)) != null)
      {
        string str2 = str1 + ",\"z\":[";
        for (int index = 0; index < sendList.Count; ++index)
          str2 = str2 + (index > 0 ? "," : string.Empty) + (object) sendList[index].z;
        str1 = str2 + "]";
      }
      // ISSUE: reference to a compiler-generated method
      if (sendList.Find(new Predicate<SceneBattle.MultiPlayInput>(listCAnonStorey1C6.\u003C\u003Em__173)) != null)
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
      if (Object.op_Inequality((Object) unitController, (Object) null) && unitController.IsJumpCant())
        return false;
      string s = FlowNode_Variable.Get("DisableTimeLimit");
      return (string.IsNullOrEmpty(s) || long.Parse(s) == 0L) && currentUnit.IsControl;
    }

    private TacticsUnitController ResetMultiPlayerTransform(Unit unit)
    {
      TacticsUnitController tacticsUnitController = unit != null ? this.FindUnitController(unit) : (TacticsUnitController) null;
      if (Object.op_Equality((Object) tacticsUnitController, (Object) null))
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
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      List<MyPhoton.MyEvent> events = instance.GetEvents();
      if (events == null)
        return;
      while (events.Count > 0)
      {
        if (this.mResumeMultiPlay)
        {
          if (events[0].code == MyPhoton.SEND_TYPE.Resume)
          {
            this.RecvResume(events[0].json);
            if (!this.mResumeMultiPlay)
            {
              this.mResumeSend = false;
              this.ResumeSuccess = true;
              this.Battle.SyncStart = true;
              instance.AddMyPlayerParam("BattleStart", (object) true);
              this.mBattleUI_MultiPlay.OnMyPlayerResume();
            }
          }
          else
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            SceneBattle.\u003CRecvEvent\u003Ec__AnonStorey1C8 eventCAnonStorey1C8 = new SceneBattle.\u003CRecvEvent\u003Ec__AnonStorey1C8();
            string json = events[0].json;
            // ISSUE: reference to a compiler-generated field
            eventCAnonStorey1C8.data = !string.IsNullOrEmpty(json) ? JSONParser.parseJSONObject<SceneBattle.MultiPlayRecvData>(json) : (SceneBattle.MultiPlayRecvData) null;
            // ISSUE: reference to a compiler-generated method
            SceneBattle.MultiPlayer mp = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(eventCAnonStorey1C8.\u003C\u003Em__176));
            // ISSUE: reference to a compiler-generated field
            if (eventCAnonStorey1C8.data.h == 4)
            {
              // ISSUE: reference to a compiler-generated field
              this.mRecvContinue.Add(eventCAnonStorey1C8.data);
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (eventCAnonStorey1C8.data.h == 6)
              {
                if (mp != null)
                  mp.FinishLoad = true;
              }
              else
              {
                // ISSUE: reference to a compiler-generated field
                if (eventCAnonStorey1C8.data.h == 9)
                {
                  // ISSUE: reference to a compiler-generated field
                  this.RecvResumeSuccess(mp, eventCAnonStorey1C8.data);
                }
              }
            }
          }
        }
        else if (events[0].code == MyPhoton.SEND_TYPE.Normal)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          SceneBattle.\u003CRecvEvent\u003Ec__AnonStorey1C9 eventCAnonStorey1C9 = new SceneBattle.\u003CRecvEvent\u003Ec__AnonStorey1C9();
          string json = events[0].json;
          // ISSUE: reference to a compiler-generated field
          eventCAnonStorey1C9.data = !string.IsNullOrEmpty(json) ? JSONParser.parseJSONObject<SceneBattle.MultiPlayRecvData>(json) : (SceneBattle.MultiPlayRecvData) null;
          // ISSUE: reference to a compiler-generated method
          SceneBattle.MultiPlayer mp = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(eventCAnonStorey1C9.\u003C\u003Em__177));
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.MultiPlayLog("[PUN] recv packet sq:" + (object) eventCAnonStorey1C9.data.sq + " pid:" + (object) eventCAnonStorey1C9.data.pid + " pidx:" + (object) eventCAnonStorey1C9.data.pidx + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) eventCAnonStorey1C9.data.h + " b:" + (object) eventCAnonStorey1C9.data.b + "/" + (object) this.UnitStartCountTotal);
          // ISSUE: reference to a compiler-generated field
          if (eventCAnonStorey1C9.data != null)
          {
            // ISSUE: reference to a compiler-generated field
            if (eventCAnonStorey1C9.data.h == 4)
            {
              // ISSUE: reference to a compiler-generated field
              this.mRecvContinue.Add(eventCAnonStorey1C9.data);
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (eventCAnonStorey1C9.data.h == 3)
              {
                // ISSUE: reference to a compiler-generated field
                this.mRecvGoodJob.Add(eventCAnonStorey1C9.data);
              }
              else
              {
                // ISSUE: reference to a compiler-generated field
                if (eventCAnonStorey1C9.data.h == 2)
                {
                  // ISSUE: reference to a compiler-generated method
                  this.mRecvCheck.RemoveAll(new Predicate<SceneBattle.MultiPlayRecvData>(eventCAnonStorey1C9.\u003C\u003Em__178));
                  // ISSUE: reference to a compiler-generated field
                  this.mRecvCheck.Add(eventCAnonStorey1C9.data);
                  // ISSUE: reference to a compiler-generated field
                  if (!this.mRecvCheckData.ContainsKey(eventCAnonStorey1C9.data.b))
                  {
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    this.mRecvCheckData.Add(eventCAnonStorey1C9.data.b, eventCAnonStorey1C9.data);
                  }
                }
                else
                {
                  // ISSUE: reference to a compiler-generated field
                  if (eventCAnonStorey1C9.data.h == 1)
                  {
                    // ISSUE: reference to a compiler-generated field
                    this.mRecvBattle.Add(eventCAnonStorey1C9.data);
                  }
                  else
                  {
                    // ISSUE: reference to a compiler-generated field
                    if (eventCAnonStorey1C9.data.h == 5)
                    {
                      // ISSUE: reference to a compiler-generated field
                      this.mRecvIgnoreMyDisconnect.Add(eventCAnonStorey1C9.data);
                    }
                    else
                    {
                      // ISSUE: reference to a compiler-generated field
                      if (eventCAnonStorey1C9.data.h == 6)
                      {
                        if (mp != null)
                          mp.FinishLoad = true;
                      }
                      else
                      {
                        // ISSUE: reference to a compiler-generated field
                        if (eventCAnonStorey1C9.data.h == 7)
                        {
                          // ISSUE: reference to a compiler-generated method
                          if (this.mRecvResumeRequest.Find(new Predicate<SceneBattle.MultiPlayRecvData>(eventCAnonStorey1C9.\u003C\u003Em__179)) == null)
                          {
                            this.Battle.ResumeState = BattleCore.RESUME_STATE.REQUEST;
                            this.mResumeSend = false;
                            Debug.Log((object) "*********************");
                            Debug.Log((object) "ResumeRequest!!");
                            Debug.Log((object) "*********************");
                            // ISSUE: reference to a compiler-generated field
                            this.mRecvResumeRequest.Add(eventCAnonStorey1C9.data);
                          }
                        }
                        else
                        {
                          // ISSUE: reference to a compiler-generated field
                          if (eventCAnonStorey1C9.data.h == 9)
                          {
                            // ISSUE: reference to a compiler-generated field
                            this.RecvResumeSuccess(mp, eventCAnonStorey1C9.data);
                          }
                          else
                          {
                            // ISSUE: reference to a compiler-generated field
                            if (eventCAnonStorey1C9.data.h == 10)
                            {
                              if (mp != null)
                                mp.SyncWait = true;
                            }
                            else
                            {
                              // ISSUE: reference to a compiler-generated field
                              if (eventCAnonStorey1C9.data.h == 11)
                              {
                                if (mp != null)
                                  mp.SyncResumeWait = true;
                              }
                              else
                              {
                                // ISSUE: reference to a compiler-generated field
                                if (eventCAnonStorey1C9.data.h == 12)
                                {
                                  if (mp != null)
                                    mp.NotifyDisconnected = true;
                                  this.Battle.IsVSForceWin = true;
                                  this.SendRetireComfirm();
                                  if (!this.IsInState<SceneBattle.State_RetireComfirm>())
                                  {
                                    this.mBattleUI_MultiPlay.OnForceWin();
                                    this.GotoState<SceneBattle.State_ForceWinComfirm>();
                                  }
                                }
                                else
                                {
                                  // ISSUE: reference to a compiler-generated field
                                  if (eventCAnonStorey1C9.data.h == 13)
                                  {
                                    this.mRetireComfirm = true;
                                  }
                                  else
                                  {
                                    // ISSUE: reference to a compiler-generated field
                                    // ISSUE: reference to a compiler-generated field
                                    // ISSUE: reference to a compiler-generated field
                                    // ISSUE: reference to a compiler-generated field
                                    if ((eventCAnonStorey1C9.data.h == 14 || eventCAnonStorey1C9.data.h == 15 || eventCAnonStorey1C9.data.h == 16) && (eventCAnonStorey1C9.data.uid == instance.MyPlayerIndex && !this.mCheater))
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
                                        // ISSUE: reference to a compiler-generated field
                                        if (eventCAnonStorey1C9.data.h == 15)
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
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        events.RemoveAt(0);
      }
    }

    private bool DisconnetEvent()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState == MyPhoton.MyState.ROOM || !Object.op_Inequality((Object) this.mBattleUI_MultiPlay, (Object) null))
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
      if (!Object.op_Inequality((Object) this.mBattleUI_MultiPlay, (Object) null))
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
      if (!Object.op_Inequality((Object) this.mBattleUI_MultiPlay, (Object) null))
        return;
      int num = 0;
      if ((this.IsInState<SceneBattle.State_MapCommandMultiPlay>() || this.IsInState<SceneBattle.State_MapCommandVersus>() || this.IsInState<SceneBattle.State_SelectTargetV2>()) && (this.Battle.CurrentUnit != null && this.Battle.CurrentUnit.OwnerPlayerIndex > 0))
      {
        string s = FlowNode_Variable.Get("DisableThinkingUI");
        if (string.IsNullOrEmpty(s) || long.Parse(s) == 0L)
          num = this.Battle.CurrentUnit.OwnerPlayerIndex;
      }
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
      if (this.Battle.GetQuestResult() != BattleCore.QuestResult.Pending)
        return;
      List<MyPhoton.MyPlayer> roomPlayerList1 = instance.GetRoomPlayerList();
      if (roomPlayerList1 != null)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SceneBattle.\u003COtherPlayerDisconnect\u003Ec__AnonStorey1CA disconnectCAnonStorey1Ca = new SceneBattle.\u003COtherPlayerDisconnect\u003Ec__AnonStorey1CA();
        using (List<SceneBattle.MultiPlayer>.Enumerator enumerator = this.mMultiPlayer.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            // ISSUE: reference to a compiler-generated field
            disconnectCAnonStorey1Ca.mp = enumerator.Current;
            // ISSUE: reference to a compiler-generated method
            if (roomPlayerList1 != null && roomPlayerList1.Find(new Predicate<MyPhoton.MyPlayer>(disconnectCAnonStorey1Ca.\u003C\u003Em__17A)) == null)
            {
              // ISSUE: reference to a compiler-generated field
              disconnectCAnonStorey1Ca.mp.Disconnected = true;
            }
          }
        }
      }
      if (Object.op_Inequality((Object) this.mBattleUI_MultiPlay, (Object) null) && !this.mIsWaitingForBattleSignal && (!BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED) && this.CurrentNotifyDisconnectedPlayer == null))
      {
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
        MyPhoton.MyPlayer myPlayer = instance.GetMyPlayer();
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SceneBattle.\u003COtherPlayerDisconnect\u003Ec__AnonStorey1CB disconnectCAnonStorey1Cb = new SceneBattle.\u003COtherPlayerDisconnect\u003Ec__AnonStorey1CB();
        using (List<SceneBattle.MultiPlayer>.Enumerator enumerator1 = this.mMultiPlayer.GetEnumerator())
        {
          while (enumerator1.MoveNext())
          {
            // ISSUE: reference to a compiler-generated field
            disconnectCAnonStorey1Cb.mp = enumerator1.Current;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            if (myPlayersStarted != null && roomPlayerList1 != null && (myPlayer != null && !disconnectCAnonStorey1Cb.mp.NotifyDisconnected) && (roomPlayerList1.Find(new Predicate<MyPhoton.MyPlayer>(disconnectCAnonStorey1Cb.\u003C\u003Em__17B)) == null && this.mRecvIgnoreMyDisconnect.Find(new Predicate<SceneBattle.MultiPlayRecvData>(disconnectCAnonStorey1Cb.\u003C\u003Em__17C)) == null))
            {
              // ISSUE: reference to a compiler-generated method
              this.CurrentNotifyDisconnectedPlayer = myPlayersStarted != null ? myPlayersStarted.Find(new Predicate<JSON_MyPhotonPlayerParam>(disconnectCAnonStorey1Cb.\u003C\u003Em__17D)) : (JSON_MyPhotonPlayerParam) null;
              int num1 = 0;
              int num2 = 0;
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              SceneBattle.\u003COtherPlayerDisconnect\u003Ec__AnonStorey1CC disconnectCAnonStorey1Cc = new SceneBattle.\u003COtherPlayerDisconnect\u003Ec__AnonStorey1CC();
              using (List<JSON_MyPhotonPlayerParam>.Enumerator enumerator2 = myPlayersStarted.GetEnumerator())
              {
                while (enumerator2.MoveNext())
                {
                  // ISSUE: reference to a compiler-generated field
                  disconnectCAnonStorey1Cc.sp = enumerator2.Current;
                  // ISSUE: reference to a compiler-generated method
                  SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(disconnectCAnonStorey1Cc.\u003C\u003Em__17E));
                  if (multiPlayer == null || !multiPlayer.NotifyDisconnected)
                  {
                    // ISSUE: reference to a compiler-generated field
                    if (disconnectCAnonStorey1Cc.sp.playerIndex < num1 || num1 == 0)
                    {
                      // ISSUE: reference to a compiler-generated field
                      num1 = disconnectCAnonStorey1Cc.sp.playerIndex;
                    }
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    // ISSUE: reference to a compiler-generated field
                    if (disconnectCAnonStorey1Cc.sp.playerIndex != disconnectCAnonStorey1Cb.mp.PlayerIndex && (disconnectCAnonStorey1Cc.sp.playerIndex < num2 || num2 == 0))
                    {
                      // ISSUE: reference to a compiler-generated field
                      num2 = disconnectCAnonStorey1Cc.sp.playerIndex;
                    }
                  }
                }
              }
              // ISSUE: reference to a compiler-generated field
              this.CurrentNotifyDisconnectedPlayerType = num1 == disconnectCAnonStorey1Cb.mp.PlayerIndex ? (num2 != instance.MyPlayerIndex ? SceneBattle.ENotifyDisconnectedPlayerType.OWNER : SceneBattle.ENotifyDisconnectedPlayerType.OWNER_AND_I_AM_OWNER) : SceneBattle.ENotifyDisconnectedPlayerType.NORMAL;
              if (this.CurrentResumePlayer != null)
                this.mBattleUI_MultiPlay.OnOtherPlayerResumeClose();
              this.mBattleUI_MultiPlay.OnMyPlayerResumeClose();
              // ISSUE: reference to a compiler-generated field
              disconnectCAnonStorey1Cb.mp.NotifyDisconnected = true;
              // ISSUE: reference to a compiler-generated field
              disconnectCAnonStorey1Cb.mp.RecvInputNum = 0;
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
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: reference to a compiler-generated method
        if (roomPlayerList2.Find(new Predicate<MyPhoton.MyPlayer>(new SceneBattle.\u003COtherPlayerDisconnect\u003Ec__AnonStorey1CD() { data = this.mRecvResumeRequest[index] }.\u003C\u003Em__17F)) == null)
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
      if (!Object.op_Inequality((Object) this.mBattleUI_MultiPlay, (Object) null) || this.mIsWaitingForBattleSignal || (BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED) || this.CurrentResumePlayer != null))
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      this.CurrentResumePlayer = myPlayersStarted != null ? myPlayersStarted.Find(new Predicate<JSON_MyPhotonPlayerParam>(new SceneBattle.\u003COtherPlayerResume\u003Ec__AnonStorey1CE()
      {
        data = this.mRecvResume[0]
      }.\u003C\u003Em__180)) : (JSON_MyPhotonPlayerParam) null;
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
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          SceneBattle.\u003CCheckStart\u003Ec__AnonStorey1CF startCAnonStorey1Cf = new SceneBattle.\u003CCheckStart\u003Ec__AnonStorey1CF();
          using (List<int>.Enumerator enumerator = this.mResumeAlreadyStartPlayer.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              // ISSUE: reference to a compiler-generated field
              startCAnonStorey1Cf.playerID = enumerator.Current;
              // ISSUE: reference to a compiler-generated method
              flag |= roomPlayerList.Find(new Predicate<MyPhoton.MyPlayer>(startCAnonStorey1Cf.\u003C\u003Em__181)) != null;
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
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SceneBattle.\u003CUpdateMultiBattleInfo\u003Ec__AnonStorey1D0 infoCAnonStorey1D0 = new SceneBattle.\u003CUpdateMultiBattleInfo\u003Ec__AnonStorey1D0();
        // ISSUE: reference to a compiler-generated field
        infoCAnonStorey1D0.data = this.mRecvBattle[0];
        // ISSUE: reference to a compiler-generated field
        if (infoCAnonStorey1D0.data.b > this.UnitStartCountTotal)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          DebugUtility.LogWarning("[PUN] new turn data. sq:" + (object) infoCAnonStorey1D0.data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) infoCAnonStorey1D0.data.h + " b:" + (object) infoCAnonStorey1D0.data.b + "/" + (object) this.UnitStartCountTotal + " test:" + (object) this.mRecvBattle.FindIndex(new Predicate<SceneBattle.MultiPlayRecvData>(infoCAnonStorey1D0.\u003C\u003Em__182)));
          break;
        }
        // ISSUE: reference to a compiler-generated field
        if (infoCAnonStorey1D0.data.b < this.UnitStartCountTotal)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          DebugUtility.LogWarning("[PUN] old turn data. sq:" + (object) infoCAnonStorey1D0.data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) infoCAnonStorey1D0.data.h + " b:" + (object) infoCAnonStorey1D0.data.b + "/" + (object) this.UnitStartCountTotal);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (infoCAnonStorey1D0.data.h == 1)
          {
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated field
            this.mMultiPlayerUnit.Find(new Predicate<SceneBattle.MultiPlayerUnit>(infoCAnonStorey1D0.\u003C\u003Em__183)).RecvInput(this, infoCAnonStorey1D0.data);
          }
        }
        this.mRecvBattle.RemoveAt(0);
      }
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
            if (Object.op_Inequality((Object) tacticsUnitController, (Object) null))
              this.SetCameraTarget(((Component) tacticsUnitController).get_transform().get_position());
            this.SendInputMoveEnd(this.Battle.CurrentUnit, true);
          }
          this.SendInputUnitTimeLimit(this.Battle.CurrentUnit);
          this.SendInputFlush();
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneBattle.\u003CCheckSync\u003Ec__AnonStorey1D1 syncCAnonStorey1D1 = new SceneBattle.\u003CCheckSync\u003Ec__AnonStorey1D1();
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          syncCAnonStorey1D1.player = enumerator.Current;
          // ISSUE: reference to a compiler-generated method
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(syncCAnonStorey1D1.\u003C\u003Em__184));
          // ISSUE: reference to a compiler-generated field
          if (syncCAnonStorey1D1.player.start && multiPlayer != null && !multiPlayer.NotifyDisconnected)
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneBattle.\u003CCheckResumeSync\u003Ec__AnonStorey1D2 syncCAnonStorey1D2 = new SceneBattle.\u003CCheckResumeSync\u003Ec__AnonStorey1D2();
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          syncCAnonStorey1D2.player = enumerator.Current;
          // ISSUE: reference to a compiler-generated method
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(syncCAnonStorey1D2.\u003C\u003Em__185));
          // ISSUE: reference to a compiler-generated field
          if (syncCAnonStorey1D2.player.start && multiPlayer != null && !multiPlayer.NotifyDisconnected)
            flag &= multiPlayer.SyncResumeWait;
        }
      }
      return flag;
    }

    private void UpdateMultiPlayer()
    {
      if (!this.Battle.IsMultiPlay || !this.Battle.FinishLoad)
        return;
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
        this.SendInputFlush();
      if (this.CheckInputTimeLimit())
        return;
      this.SendTimeLimit();
    }

    private void SendInputFlush()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (this.mSendList.Count > 0)
      {
        string multiPlayInputList = this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.INPUT, this.mCurrentSendInputUnitID, this.mSendList);
        instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
      }
      this.mSendList.Clear();
      this.mSendTime = 0.0f;
    }

    private void SendInputMove(Unit unit, TacticsUnitController controller)
    {
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      Vector3 position = ((Component) controller).get_transform().get_position();
      Quaternion rotation = ((Component) controller).get_transform().get_rotation();
      // ISSUE: explicit reference operation
      float y = (float) ((Quaternion) @rotation).get_eulerAngles().y;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 2,
        x = (float) position.x,
        z = (float) position.z,
        r = y
      });
    }

    private void SendInputGridXY(Unit unit, int gridX, int gridY, EUnitDirection dir)
    {
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 3,
        gx = gridX,
        gy = gridY,
        d = (int) dir
      });
      this.MultiPlayLog("[PUN] SendInputGridXY x:" + (object) gridX + " y:" + (object) gridY);
    }

    private void SendInputMoveStart(Unit unit)
    {
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
        return;
      this.mSendList.Add(new SceneBattle.MultiPlayInput()
      {
        c = 1
      });
    }

    private void SendInputMoveEnd(Unit unit, bool cancel)
    {
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
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
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
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
    }

    private void SendInputGridEvent(Unit unit)
    {
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
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
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
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
      this.MultiPlayInputTimeLimit = 0.0f;
    }

    private void SendInputUnitTimeLimit(Unit unit)
    {
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
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
      if (!this.Battle.IsMultiPlay || this.Battle.EntryBattleMultiPlayTimeUp)
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
      if (!this.Battle.IsMultiPlay)
        return true;
      string multiPlayInputList = this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.IGNORE_MY_DISCONNECT, 0, (List<SceneBattle.MultiPlayInput>) null);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
      instance.SendFlush();
      this.MultiPlayLog("[PUN]SendIgnoreMyDisconnect");
      return true;
    }

    public void ResetCheckData()
    {
      this.mRecvCheck.Clear();
      this.mMultiPlayCheckList.Clear();
    }

    private bool SendCheckMultiPlay()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneBattle.\u003CSendCheckMultiPlay\u003Ec__AnonStorey1D4 playCAnonStorey1D4 = new SceneBattle.\u003CSendCheckMultiPlay\u003Ec__AnonStorey1D4();
      if (!this.Battle.IsMultiPlay)
        return true;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag = !Object.op_Equality((Object) this.mBattleUI_MultiPlay, (Object) null) && this.mBattleUI_MultiPlay.CheckRandCheat;
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
      string multiPlayInputList = this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.CHECK, 0, sendList);
      if (instance.IsOldestPlayer())
        instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
      // ISSUE: reference to a compiler-generated field
      playCAnonStorey1D4.data = JSONParser.parseJSONObject<SceneBattle.MultiPlayRecvData>(multiPlayInputList);
      // ISSUE: reference to a compiler-generated method
      this.mRecvCheck.RemoveAll(new Predicate<SceneBattle.MultiPlayRecvData>(playCAnonStorey1D4.\u003C\u003Em__187));
      // ISSUE: reference to a compiler-generated field
      this.mRecvCheck.Add(playCAnonStorey1D4.data);
      // ISSUE: reference to a compiler-generated field
      if (!this.mRecvCheckMyData.ContainsKey(playCAnonStorey1D4.data.b))
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.mRecvCheckMyData.Add(playCAnonStorey1D4.data.b, playCAnonStorey1D4.data);
      }
      // ISSUE: reference to a compiler-generated field
      Debug.Log((object) ("*****SendCheckData******::" + (object) playCAnonStorey1D4.data.b));
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
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return (SRPG_TouchInputModule) null;
      return (SRPG_TouchInputModule) gameObject.GetComponent<SRPG_TouchInputModule>();
    }

    public void SetupGoodJob()
    {
      if (this.mSetupGoodJob)
        return;
      SRPG_TouchInputModule touchInputModule = this.GetTouchInputModule();
      if (Object.op_Equality((Object) touchInputModule, (Object) null))
        return;
      touchInputModule.OnDoubleTap += new SRPG_TouchInputModule.OnDoubleTapDelegate(this.OnDoubleTap);
      this.mSetupGoodJob = true;
    }

    public void CleanupGoodJob()
    {
      if (!this.mSetupGoodJob)
        return;
      SRPG_TouchInputModule touchInputModule = this.GetTouchInputModule();
      if (Object.op_Equality((Object) touchInputModule, (Object) null))
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
      BattleStamp battleStamp = !Object.op_Equality((Object) this.mBattleUI_MultiPlay, (Object) null) ? this.mBattleUI_MultiPlay.StampWindow : (BattleStamp) null;
      string multiPlayInputList = this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.GOODJOB, unitID, new List<SceneBattle.MultiPlayInput>() { new SceneBattle.MultiPlayInput() { gx = gx, gy = gy, c = !Object.op_Equality((Object) battleStamp, (Object) null) ? battleStamp.SelectStampID : -1 } });
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
      this.mRecvGoodJob.Add(JSONParser.parseJSONObject<SceneBattle.MultiPlayRecvData>(multiPlayInputList));
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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      int index = this.Battle.AllUnits.FindIndex(new Predicate<Unit>(new SceneBattle.\u003COnGoodJobClickUnit\u003Ec__AnonStorey1D5() { unit = controller.Unit }.\u003C\u003Em__188));
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
      if (Object.op_Inequality((Object) this.mBattleUI_MultiPlay, (Object) null) && this.mBattleUI_MultiPlay.StampWindowIsOpened)
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
            BattleStamp battleStamp = !Object.op_Equality((Object) this.mBattleUI_MultiPlay, (Object) null) ? this.mBattleUI_MultiPlay.StampWindow : (BattleStamp) null;
            Sprite sprite = Object.op_Equality((Object) battleStamp, (Object) null) || battleStamp.Sprites == null || (index1 < 0 || index1 >= battleStamp.Sprites.Length) ? (Sprite) null : battleStamp.Sprites[index1];
            GameObject prefab = Object.op_Equality((Object) battleStamp, (Object) null) || battleStamp.Prefabs == null || (index1 < 0 || index1 >= battleStamp.Prefabs.Length) ? (GameObject) null : battleStamp.Prefabs[index1];
            this.PopupGoodJob(position, prefab, sprite);
          }
        }
      }
      this.mRecvGoodJob.Clear();
    }

    public bool SendFinishLoad()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneBattle.\u003CSendFinishLoad\u003Ec__AnonStorey1D6 loadCAnonStorey1D6 = new SceneBattle.\u003CSendFinishLoad\u003Ec__AnonStorey1D6();
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      // ISSUE: reference to a compiler-generated field
      loadCAnonStorey1D6.me = instance.GetMyPlayer();
      // ISSUE: reference to a compiler-generated method
      SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(loadCAnonStorey1D6.\u003C\u003Em__189));
      string multiPlayInputList = this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.FINISH_LOAD, 0, (List<SceneBattle.MultiPlayInput>) null);
      bool flag = instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
      if (multiPlayer != null)
        multiPlayer.FinishLoad = true;
      return flag;
    }

    public void SendResumeSuccess()
    {
      if (this.mResumeSend)
        return;
      this.mResumeSend = PunMonoSingleton<MyPhoton>.Instance.SendRoomMessage(true, this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.RESUME_SUCCESS, 0, (List<SceneBattle.MultiPlayInput>) null), MyPhoton.SEND_TYPE.Normal);
    }

    public void SendRequestResume()
    {
      if (!this.mResumeMultiPlay || this.mResumeSend)
        return;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      string multiPlayInputList = this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.REQUEST_RESUME, 0, (List<SceneBattle.MultiPlayInput>) null);
      this.mResumeSend = instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
      List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SceneBattle.\u003CSendRequestResume\u003Ec__AnonStorey1D7 resumeCAnonStorey1D7 = new SceneBattle.\u003CSendRequestResume\u003Ec__AnonStorey1D7();
      using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          // ISSUE: reference to a compiler-generated field
          resumeCAnonStorey1D7.player = enumerator.Current;
          // ISSUE: reference to a compiler-generated method
          SceneBattle.MultiPlayer multiPlayer = this.mMultiPlayer.Find(new Predicate<SceneBattle.MultiPlayer>(resumeCAnonStorey1D7.\u003C\u003Em__18A));
          // ISSUE: reference to a compiler-generated field
          if (resumeCAnonStorey1D7.player.start)
          {
            // ISSUE: reference to a compiler-generated field
            this.mResumeAlreadyStartPlayer.Add(resumeCAnonStorey1D7.player.playerID);
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
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (!this.Battle.IsResume || !instance.IsHost() || (this.mResumeSend || this.mRecvResumeRequest.Count <= 0))
        return;
      MultiPlayResumeParam multiPlayResumeParam = new MultiPlayResumeParam();
      multiPlayResumeParam.unit = new MultiPlayResumeUnitData[this.Battle.AllUnits.Count];
      string msg = string.Empty;
      for (int index1 = 0; index1 < this.Battle.AllUnits.Count; ++index1)
      {
        Unit allUnit = this.Battle.AllUnits[index1];
        if (allUnit != null)
        {
          TacticsUnitController unitController = this.FindUnitController(this.Battle.AllUnits[index1]);
          multiPlayResumeParam.unit[index1] = new MultiPlayResumeUnitData();
          BaseStatus currentStatus = allUnit.CurrentStatus;
          MultiPlayResumeUnitData playResumeUnitData = multiPlayResumeParam.unit[index1];
          playResumeUnitData.name = allUnit.UnitName;
          playResumeUnitData.hp = (int) currentStatus.param.hp;
          playResumeUnitData.gem = allUnit.Gems;
          playResumeUnitData.dir = (int) allUnit.Direction;
          playResumeUnitData.x = allUnit.x;
          playResumeUnitData.y = allUnit.y;
          playResumeUnitData.target = this.SearchUnitIndex(allUnit.Target);
          playResumeUnitData.ragetarget = this.SearchUnitIndex(allUnit.RageTarget);
          playResumeUnitData.aiindex = (int) allUnit.AIActionIndex;
          playResumeUnitData.aiturn = (int) allUnit.AIActionTurnCount;
          playResumeUnitData.aipatrol = (int) allUnit.AIPatrolIndex;
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
            playResumeUnitData.targetgrid = allUnit.GridTarget;
            playResumeUnitData.casttarget = this.SearchUnitIndex(allUnit.UnitTarget);
          }
          playResumeUnitData.chargetime = (int) allUnit.ChargeTime;
          playResumeUnitData.isDead = !allUnit.IsGimmick ? (!allUnit.IsDead ? 0 : 1) : (!allUnit.IsDisableGimmick() ? 0 : 1);
          playResumeUnitData.deathcnt = allUnit.DeathCount;
          playResumeUnitData.autojewel = allUnit.AutoJewel;
          playResumeUnitData.waitturn = allUnit.WaitClock;
          playResumeUnitData.moveturn = allUnit.WaitMoveTurn;
          playResumeUnitData.actcnt = allUnit.ActionCount;
          playResumeUnitData.turncnt = !Object.op_Inequality((Object) unitController, (Object) null) ? 0 : unitController.TurnCount;
          playResumeUnitData.search = !allUnit.IsUnitFlag(EUnitFlag.Searched) ? 0 : 1;
          playResumeUnitData.entry = !allUnit.IsUnitFlag(EUnitFlag.Entried) ? 0 : 1;
          playResumeUnitData.trgcnt = 0;
          if (allUnit.EventTrigger != null)
            playResumeUnitData.trgcnt = allUnit.EventTrigger.Count;
          playResumeUnitData.killcnt = allUnit.KillCount;
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
            playResumeUnitData.buff = new MultiPlayResumeBuff[buffAttachments.Count];
            for (int index2 = 0; index2 < buffAttachments.Count; ++index2)
            {
              playResumeUnitData.buff[index2] = new MultiPlayResumeBuff();
              playResumeUnitData.buff[index2].unitindex = this.SearchUnitIndex(buffAttachments[index2].user);
              playResumeUnitData.buff[index2].checkunit = this.SearchUnitIndex(buffAttachments[index2].CheckTarget);
              playResumeUnitData.buff[index2].timing = (int) buffAttachments[index2].CheckTiming;
              playResumeUnitData.buff[index2].condition = (int) buffAttachments[index2].UseCondition;
              playResumeUnitData.buff[index2].turn = (int) buffAttachments[index2].turn;
              playResumeUnitData.buff[index2].passive = (bool) buffAttachments[index2].IsPassive;
              playResumeUnitData.buff[index2].type = (int) buffAttachments[index2].BuffType;
              playResumeUnitData.buff[index2].calc = (int) buffAttachments[index2].CalcType;
              playResumeUnitData.buff[index2].skilltarget = (int) buffAttachments[index2].skilltarget;
              playResumeUnitData.buff[index2].iname = (string) null;
              if (buffAttachments[index2].skill != null)
                playResumeUnitData.buff[index2].iname = buffAttachments[index2].skill.SkillParam.iname;
            }
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
              playResumeUnitData.cond[index2].iname = (string) null;
              if (condAttachments[index2].skill != null)
                playResumeUnitData.cond[index2].iname = condAttachments[index2].skill.SkillParam.iname;
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
      multiPlayResumeParam.resumeID = this.mRecvResumeRequest[0].pidx;
      if (this.mRecvResumeRequest.Count > 1)
      {
        multiPlayResumeParam.otherresume = new int[this.mRecvResumeRequest.Count - 1];
        for (int index = 1; index < this.mRecvResumeRequest.Count; ++index)
          multiPlayResumeParam.otherresume[index - 1] = this.mRecvResumeRequest[index].pidx;
      }
      string json = JsonUtility.ToJson((object) multiPlayResumeParam);
      this.mResumeSend = instance.SendRoomMessage(true, json, MyPhoton.SEND_TYPE.Resume);
      Debug.Log((object) "Send ResumeInfo!!!");
    }

    private void RecvResume(string json)
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      MultiPlayResumeParam multiPlayResumeParam = (MultiPlayResumeParam) JsonUtility.FromJson<MultiPlayResumeParam>(json);
      if (multiPlayResumeParam.resumeID != instance.MyPlayerIndex)
        return;
      List<MultiPlayResumeSkillData> buffskill = new List<MultiPlayResumeSkillData>();
      List<MultiPlayResumeSkillData> condskill = new List<MultiPlayResumeSkillData>();
      for (int index1 = 0; index1 < multiPlayResumeParam.unit.Length; ++index1)
      {
        if (multiPlayResumeParam.unit[index1] != null)
        {
          Unit target = multiPlayResumeParam.unit[index1].target == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeParam.unit[index1].target];
          Unit rage = multiPlayResumeParam.unit[index1].ragetarget == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeParam.unit[index1].ragetarget];
          Unit casttarget = multiPlayResumeParam.unit[index1].casttarget == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeParam.unit[index1].casttarget];
          buffskill.Clear();
          for (int index2 = 0; index2 < multiPlayResumeParam.unit[index1].buff.Length; ++index2)
          {
            MultiPlayResumeBuff multiPlayResumeBuff = multiPlayResumeParam.unit[index1].buff[index2];
            MultiPlayResumeSkillData playResumeSkillData = new MultiPlayResumeSkillData();
            playResumeSkillData.user = multiPlayResumeBuff.unitindex == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.unitindex];
            playResumeSkillData.check = multiPlayResumeBuff.checkunit == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.checkunit];
            playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.GetSkillData(multiPlayResumeBuff.iname);
            buffskill.Add(playResumeSkillData);
          }
          condskill.Clear();
          for (int index2 = 0; index2 < multiPlayResumeParam.unit[index1].cond.Length; ++index2)
          {
            MultiPlayResumeBuff multiPlayResumeBuff = multiPlayResumeParam.unit[index1].cond[index2];
            MultiPlayResumeSkillData playResumeSkillData = new MultiPlayResumeSkillData();
            playResumeSkillData.user = multiPlayResumeBuff.unitindex == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.unitindex];
            playResumeSkillData.check = multiPlayResumeBuff.checkunit == -1 ? (Unit) null : this.Battle.AllUnits[multiPlayResumeBuff.checkunit];
            playResumeSkillData.skill = playResumeSkillData.user == null ? (SkillData) null : playResumeSkillData.user.GetSkillData(multiPlayResumeBuff.iname);
            condskill.Add(playResumeSkillData);
          }
          if (this.Battle.AllUnits[index1] != null)
          {
            this.Battle.AllUnits[index1].SetupResume(multiPlayResumeParam.unit[index1], target, rage, casttarget, buffskill, condskill);
            TacticsUnitController unitController = this.FindUnitController(this.Battle.AllUnits[index1]);
            if (Object.op_Inequality((Object) unitController, (Object) null))
            {
              unitController.TurnCount = multiPlayResumeParam.unit[index1].turncnt;
              if (this.Battle.AllUnits[index1].IsDead)
              {
                this.Battle.ResumeDead(this.Battle.AllUnits[index1]);
                ((Component) unitController).get_gameObject().SetActive(false);
                unitController.ShowHPGauge(false);
                unitController.ShowVersusCursor(false);
                this.mTacticsUnits.Remove(unitController);
              }
              else if (unitController.IsJumpCant())
                unitController.SetCastJump();
              if (this.Battle.AllUnits[index1].IsGimmick && this.Battle.AllUnits[index1].IsDisableGimmick())
                ((Component) unitController).get_gameObject().SetActive(false);
            }
            this.ResetMultiPlayerTransform(this.Battle.AllUnits[index1]);
          }
        }
      }
      Unit.UNIT_CAST_INDEX = (OInt) multiPlayResumeParam.unitcastindex;
      for (int index = 0; index < multiPlayResumeParam.rndseed.Length; ++index)
        this.Battle.SetRandSeed(index, multiPlayResumeParam.rndseed[index]);
      for (int index = 0; index < multiPlayResumeParam.dmgrndseed.Length; ++index)
        this.Battle.SetRandDamageSeed(index, multiPlayResumeParam.dmgrndseed[index]);
      this.Battle.DamageSeed = multiPlayResumeParam.damageseed;
      this.Battle.Seed = multiPlayResumeParam.seed;
      this.mUnitStartCountTotal = multiPlayResumeParam.unitstartcount;
      this.TreasureCount = multiPlayResumeParam.treasurecount;
      this.Battle.VersusTurnCount = multiPlayResumeParam.versusturn;
      if (this.Battle.IsMultiVersus)
        this.ArenaActionCountSet(this.Battle.RemainVersusTurnCount);
      GameObject gameObject = GameObjectID.FindGameObject("UI_TREASURE");
      if (Object.op_Inequality((Object) gameObject, (Object) null))
        GameParameter.UpdateAll(gameObject);
      this.Battle.StartOrder(true, true);
      this.RefreshJumpSpots();
      if (multiPlayResumeParam.otherresume != null)
      {
        for (int index = 0; index < multiPlayResumeParam.otherresume.Length; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          SceneBattle.\u003CRecvResume\u003Ec__AnonStorey1D8 resumeCAnonStorey1D8 = new SceneBattle.\u003CRecvResume\u003Ec__AnonStorey1D8();
          // ISSUE: reference to a compiler-generated field
          resumeCAnonStorey1D8.data = new SceneBattle.MultiPlayRecvData();
          // ISSUE: reference to a compiler-generated field
          resumeCAnonStorey1D8.data.pidx = multiPlayResumeParam.otherresume[index];
          // ISSUE: reference to a compiler-generated method
          if (this.mRecvResumeRequest.Find(new Predicate<SceneBattle.MultiPlayRecvData>(resumeCAnonStorey1D8.\u003C\u003Em__18B)) == null)
          {
            // ISSUE: reference to a compiler-generated field
            this.mRecvResumeRequest.Add(resumeCAnonStorey1D8.data);
            // ISSUE: reference to a compiler-generated field
            Debug.Log((object) ("OtherPlayerResume:" + (object) resumeCAnonStorey1D8.data.pidx));
          }
        }
        this.Battle.ResumeState = BattleCore.RESUME_STATE.REQUEST;
      }
      this.mResumeMultiPlay = false;
    }

    public void SendRetire()
    {
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessage(true, this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.VS_RETIRE, 0, (List<SceneBattle.MultiPlayInput>) null), MyPhoton.SEND_TYPE.Normal);
    }

    public void SendRetireComfirm()
    {
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessage(true, this.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.VS_RETIRE_COMFIRM, 0, (List<SceneBattle.MultiPlayInput>) null), MyPhoton.SEND_TYPE.Normal);
    }

    public void SendCheat(SceneBattle.CHEAT_TYPE type, int uid)
    {
      PunMonoSingleton<MyPhoton>.Instance.SendRoomMessage(true, this.CreateMultiPlayInputList((SceneBattle.EMultiPlayRecvDataHeader) (14 + type), uid, (List<SceneBattle.MultiPlayInput>) null), MyPhoton.SEND_TYPE.Normal);
    }

    private UnitGauge GetGaugeTemplateFor(Unit unit)
    {
      if (unit.Side == EUnitSide.Enemy && Object.op_Inequality((Object) this.EnemyGaugeOverlayTemplate, (Object) null))
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
        if (!Object.op_Equality((Object) unitController, (Object) null))
        {
          if (this.mBattle.Units[index].IsGimmick && !this.mBattle.Units[index].IsDisableGimmick())
            tacticsUnitControllerList2.Add(unitController);
          else if (!this.mBattle.Units[index].IsDead)
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
          if (!Object.op_Equality((Object) tacticsUnitController1, (Object) tacticsUnitController2))
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
          if (Object.op_Inequality((Object) unitController, (Object) null))
            unitController.DeleteGimmickIconAll();
        }
      }
    }

    private void SetUnitUiHeight(Unit FocusUnit)
    {
      MapHeight gameObject = GameObjectID.FindGameObject<MapHeight>(this.mBattleUI.MapHeightID);
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      gameObject.FocusUnit = FocusUnit;
    }

    private void SetUiHeight(int Height)
    {
      MapHeight gameObject = GameObjectID.FindGameObject<MapHeight>(this.mBattleUI.MapHeightID);
      if (!Object.op_Inequality((Object) gameObject, (Object) null))
        return;
      gameObject.FocusUnit = (Unit) null;
      gameObject.Height = Height;
    }

    private void ArenaActionCountEnable(bool enable)
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.mBattleUI.ArenaActionCountID);
      if (!Object.op_Implicit((Object) gameObject))
        return;
      gameObject.SetActive(enable);
    }

    private void ArenaActionCountSet(uint count)
    {
      ArenaActionCount gameObject = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.ArenaActionCountID);
      if (!Object.op_Implicit((Object) gameObject) || (int) gameObject.ActionCount == (int) count)
        return;
      gameObject.ActionCount = count;
      gameObject.PlayEffect();
    }

    private void RemainingActionCountEnable(bool pc_enable, bool ec_enable)
    {
      GameObject gameObject1 = GameObjectID.FindGameObject(this.mBattleUI.PlayerActionCountID);
      if (Object.op_Implicit((Object) gameObject1))
        gameObject1.SetActive(pc_enable);
      GameObject gameObject2 = GameObjectID.FindGameObject(this.mBattleUI.EnemyActionCountID);
      if (!Object.op_Implicit((Object) gameObject2))
        return;
      gameObject2.SetActive(ec_enable);
    }

    private void RemainingActionCountSet(uint pc_count, uint ec_count)
    {
      ArenaActionCount gameObject1 = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.PlayerActionCountID);
      if (Object.op_Inequality((Object) gameObject1, (Object) null) && ((Behaviour) gameObject1).get_isActiveAndEnabled() && (int) gameObject1.ActionCount != (int) pc_count)
      {
        gameObject1.ActionCount = pc_count;
        gameObject1.PlayEffect();
      }
      ArenaActionCount gameObject2 = GameObjectID.FindGameObject<ArenaActionCount>(this.mBattleUI.EnemyActionCountID);
      if (!Object.op_Inequality((Object) gameObject2, (Object) null) || !((Behaviour) gameObject2).get_isActiveAndEnabled() || (int) gameObject2.ActionCount == (int) ec_count)
        return;
      gameObject2.ActionCount = ec_count;
      gameObject2.PlayEffect();
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
      if (!Object.op_Inequality((Object) this.mBattleUI.CommandWindow, (Object) null))
        return;
      this.mBattleUI.CommandWindow.CancelButton.SetActive(true);
      this.mBattleUI.CommandWindow.OKButton.SetActive(true);
      this.mBattleUI.CommandWindow.VisibleButtons = buttonTypes2;
    }

    private void RefreshOnlyMapCommand()
    {
      UnitCommands.ButtonTypes buttonTypes = (UnitCommands.ButtonTypes) (0 | 256);
      if (!Object.op_Inequality((Object) this.mBattleUI.CommandWindow, (Object) null))
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
      if (Object.op_Inequality((Object) this.mBattleUI.ItemWindow, (Object) null))
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
        this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
      }
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

    private EUnitDirection GetSkillDirectionByTargetArea(Unit unit, GridMap<bool> targetArea)
    {
      int x1 = 0;
      int y1 = 0;
      int x2 = unit.x;
      int y2 = unit.y;
      for (int y3 = 0; y3 < targetArea.h; ++y3)
      {
        for (int x3 = 0; x3 < targetArea.w; ++x3)
        {
          if (targetArea.get(x3, y3))
          {
            x1 += x3 - x2;
            y1 += y3 - y2;
          }
        }
      }
      return BattleCore.UnitDirectionFromVector(unit, x1, y1);
    }

    private void GotoSkillSelect()
    {
      UnitAbilitySkillList skillWindow = this.mBattleUI.SkillWindow;
      if (Object.op_Inequality((Object) skillWindow, (Object) null))
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
        this.mBattle.UseSkill(this.mBattle.CurrentUnit, x, y, skill, bUnitLockTarget, this.mSelectedTarget.x, this.mSelectedTarget.y);
      else
        this.mBattle.UseSkill(this.mBattle.CurrentUnit, x, y, skill, bUnitLockTarget, 0, 0);
      this.GotoState_WaitSignal<SceneBattle.State_WaitForLog>();
    }

    public GameObject KnockBackEffect
    {
      get
      {
        return this.mKnockBackEffect;
      }
    }

    private Canvas OverlayCanvas
    {
      get
      {
        if (Object.op_Inequality((Object) this.mTouchController, (Object) null))
          return (Canvas) ((Component) this.mTouchController).GetComponent<Canvas>();
        return (Canvas) null;
      }
    }

    private void ShowAllHPGauges()
    {
      this.ShowPlayerHPGauges();
      this.ShowEnemyHPGauges();
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

    private void RefreshUnitStatus(Unit unit)
    {
      if (!Object.op_Inequality((Object) UnitQueue.Instance, (Object) null))
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
        if (Object.op_Inequality((Object) controller, (Object) null))
          this.mOnUnitClick(controller);
      }
      if (this.mOnScreenClick == null)
        return;
      this.mOnScreenClick(screenPosition);
    }

    private T InstantiateSafe<T>(Object obj) where T : Object
    {
      return (T) Object.Instantiate(obj);
    }

    private void UpdateLoadProgress()
    {
      ProgressWindow.SetLoadProgress((this.mLoadProgress_UI + this.mLoadProgress_Scene + this.mLoadProgress_Animation) * 0.5f);
    }

    [DebuggerHidden]
    private IEnumerator LoadUIAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SceneBattle.\u003CLoadUIAsync\u003Ec__Iterator36() { \u003C\u003Ef__this = this };
    }

    private void InitTouchArea()
    {
      GameObject gameObject = new GameObject("TouchArea", new System.Type[6]{ typeof (Canvas), typeof (GraphicRaycaster), typeof (CanvasStack), typeof (NullGraphic), typeof (TouchController), typeof (SRPG_CanvasScaler) });
      this.mTouchController = (TouchController) gameObject.GetComponent<TouchController>();
      this.mTouchController.OnClick = new TouchController.ClickEvent(this.OnClickBackground);
      this.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
      this.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
      ((Canvas) gameObject.GetComponent<Canvas>()).set_renderMode((RenderMode) 0);
      ((CanvasStack) gameObject.GetComponent<CanvasStack>()).Priority = 0;
    }

    private void DestroyUI()
    {
      BadStatusEffects.UnloadEffects();
      GameUtility.DestroyGameObject((Component) this.mSkillTargetWindow);
      this.mSkillTargetWindow = (SkillTargetWindow) null;
      if (Object.op_Inequality((Object) this.mTouchController, (Object) null))
      {
        this.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        this.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
      }
      GameUtility.DestroyGameObject((Component) this.mBattleUI);
      this.mBattleUI = (FlowNode_BattleUI) null;
      this.mBattleSceneRoot = (BattleSceneSettings) null;
      if (Object.op_Inequality((Object) this.mDefaultBattleScene, (Object) null))
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
      if (Object.op_Equality((Object) unitController, (Object) null))
        return;
      Transform transform = ((Component) unitController).get_transform();
      for (int index1 = this.mUnitMarkers.Length - 1; index1 >= 0; --index1)
      {
        for (int index2 = this.mUnitMarkers[index1].Count - 1; index2 >= 0; --index2)
        {
          if (Object.op_Equality((Object) this.mUnitMarkers[index1][index2].get_transform().get_parent(), (Object) transform))
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
      if (Object.op_Equality((Object) unitController, (Object) null))
        return;
      Transform transform = ((Component) unitController).get_transform();
      List<GameObject> mUnitMarker = this.mUnitMarkers[(int) markerType];
      for (int index = mUnitMarker.Count - 1; index >= 0; --index)
      {
        if (mUnitMarker[index].get_layer() != GameUtility.LayerHidden && Object.op_Equality((Object) mUnitMarker[index].get_transform().get_parent(), (Object) transform))
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
      GameObject go = Object.Instantiate((Object) this.mUnitMarkerTemplates[(int) markerType], Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
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

    public void PopupDamageNumber(Vector3 position, int number)
    {
      this.PopupNumber(position, number, Color.get_white(), this.mDamageTemplate);
    }

    public void PopupHpHealNumber(Vector3 position, int number)
    {
      this.PopupNumber(position, number, Color.get_white(), this.mHpHealTemplate);
    }

    public void PopupMpHealNumber(Vector3 position, int number)
    {
      this.PopupNumber(position, number, Color.get_white(), this.mMpHealTemplate);
    }

    public void PopupNumber(Vector3 position, int number, Color color, DamagePopup popup)
    {
      if (Object.op_Equality((Object) popup, (Object) null))
      {
        Debug.LogError((object) "mDamageTemplate == null");
      }
      else
      {
        DamagePopup damagePopup = (DamagePopup) Object.Instantiate<DamagePopup>((M0) popup);
        damagePopup.Value = number;
        damagePopup.DigitColor = color;
        SceneBattle.Popup2D(((Component) damagePopup).get_gameObject(), position, 1, 0.0f);
      }
    }

    public void PopupDamageDsgNumber(Vector3 position, int number, eDamageDispType ddt)
    {
      if (!Object.op_Implicit((Object) this.mDamageDsgTemplate))
      {
        Debug.LogError((object) "mDamageDsgTemplate == null");
      }
      else
      {
        DamageDsgPopup damageDsgPopup = (DamageDsgPopup) Object.Instantiate<DamageDsgPopup>((M0) this.mDamageDsgTemplate);
        damageDsgPopup.Setup(number, Color.get_white(), ddt);
        SceneBattle.Popup2D(((Component) damageDsgPopup).get_gameObject(), position, 1, 0.0f);
      }
    }

    public bool HasMissPopup
    {
      get
      {
        return Object.op_Inequality((Object) this.mMissPopup, (Object) null);
      }
    }

    public bool HasPerfectAvoidPopup
    {
      get
      {
        return Object.op_Inequality((Object) this.mPerfectAvoidPopup, (Object) null);
      }
    }

    public bool HasGuardPopup
    {
      get
      {
        return Object.op_Inequality((Object) this.mGuardPopup, (Object) null);
      }
    }

    public bool HasAbsorbPopup
    {
      get
      {
        return Object.op_Inequality((Object) this.mAbsorbPopup, (Object) null);
      }
    }

    public bool HasCriticalPopup
    {
      get
      {
        return Object.op_Inequality((Object) this.mCriticalPopup, (Object) null);
      }
    }

    public bool HasBackstabPopup
    {
      get
      {
        return Object.op_Inequality((Object) this.mBackstabPopup, (Object) null);
      }
    }

    public bool HasWeakPopup
    {
      get
      {
        return Object.op_Inequality((Object) this.mWeakPopup, (Object) null);
      }
    }

    public bool HasResistPopup
    {
      get
      {
        return Object.op_Inequality((Object) this.mResistPopup, (Object) null);
      }
    }

    public void PopupMiss(Vector3 position, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) this.mMissPopup, (Object) null))
        return;
      SceneBattle.Popup2D((GameObject) Object.Instantiate<GameObject>((M0) this.mMissPopup), position, 0, yOffset);
    }

    public void PopupPefectAvoid(Vector3 position, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) this.mPerfectAvoidPopup, (Object) null))
        return;
      SceneBattle.Popup2D((GameObject) Object.Instantiate<GameObject>((M0) this.mPerfectAvoidPopup), position, 0, yOffset);
    }

    public void PopupGuard(Vector3 position, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) this.mGuardPopup, (Object) null))
        return;
      SceneBattle.Popup2D((GameObject) Object.Instantiate<GameObject>((M0) this.mGuardPopup), position, 0, yOffset);
    }

    public void PopupAbsorb(Vector3 position, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) this.mAbsorbPopup, (Object) null))
        return;
      SceneBattle.Popup2D((GameObject) Object.Instantiate<GameObject>((M0) this.mAbsorbPopup), position, 0, yOffset);
    }

    public void PopupCritical(Vector3 position, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) this.mCriticalPopup, (Object) null))
        return;
      SceneBattle.Popup2D((GameObject) Object.Instantiate<GameObject>((M0) this.mCriticalPopup), position, 0, yOffset);
    }

    public void PopupBackstab(Vector3 position, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) this.mBackstabPopup, (Object) null))
        return;
      SceneBattle.Popup2D((GameObject) Object.Instantiate<GameObject>((M0) this.mBackstabPopup), position, 0, yOffset);
    }

    public void PopupWeak(Vector3 position, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) this.mWeakPopup, (Object) null))
        return;
      SceneBattle.Popup2D((GameObject) Object.Instantiate<GameObject>((M0) this.mWeakPopup), position, 0, yOffset);
    }

    public void PopupResist(Vector3 position, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) this.mResistPopup, (Object) null))
        return;
      SceneBattle.Popup2D((GameObject) Object.Instantiate<GameObject>((M0) this.mResistPopup), position, 0, yOffset);
    }

    public void PopupGoodJob(Vector3 position, GameObject prefab, Sprite sprite = null)
    {
      if (Object.op_Equality((Object) prefab, (Object) null))
        return;
      GameObject go = (GameObject) Object.Instantiate<GameObject>((M0) prefab);
      if (Object.op_Equality((Object) go, (Object) null))
        return;
      Image image = !Object.op_Equality((Object) sprite, (Object) null) ? (Image) go.GetComponentInChildren<Image>() : (Image) null;
      if (Object.op_Inequality((Object) image, (Object) null))
        image.set_sprite(sprite);
      SceneBattle.Popup2D(go, position, 0, 0.0f);
    }

    private Transform CreateParamChangeEffect(ParamTypes type, bool isDebuff)
    {
      if (Object.op_Equality((Object) this.mParamChangeEffectTemplate, (Object) null))
        return (Transform) null;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.mParamChangeEffectTemplate);
      BuffEffectText component = (BuffEffectText) gameObject.GetComponent<BuffEffectText>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetText(type, isDebuff);
      return gameObject.get_transform();
    }

    public void PopupParamChange(Vector3 position, BuffBit buff, BuffBit debuff)
    {
      if (buff == null || debuff == null)
        return;
      GameObject go = new GameObject("Params", new System.Type[2]{ typeof (RectTransform), typeof (DelayStart) });
      Transform transform = go.get_transform();
      for (int index = 0; index < SkillParam.MAX_PARAMTYPES; ++index)
      {
        ParamTypes type = (ParamTypes) index;
        if (buff.CheckBit(type))
        {
          Transform paramChangeEffect = this.CreateParamChangeEffect(type, false);
          if (Object.op_Inequality((Object) paramChangeEffect, (Object) null))
            paramChangeEffect.SetParent(transform, false);
        }
        if (debuff.CheckBit(type))
        {
          Transform paramChangeEffect = this.CreateParamChangeEffect(type, true);
          if (Object.op_Inequality((Object) paramChangeEffect, (Object) null))
            paramChangeEffect.SetParent(transform, false);
        }
      }
      SceneBattle.Popup2D(go, position, 0, 0.0f);
    }

    private Transform CreateConditionChangeEffect(EUnitCondition condition)
    {
      if (Object.op_Equality((Object) this.mConditionChangeEffectTemplate, (Object) null))
        return (Transform) null;
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.mConditionChangeEffectTemplate);
      CondEffectText component = (CondEffectText) gameObject.GetComponent<CondEffectText>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetText(condition);
      return gameObject.get_transform();
    }

    public void PopupConditionChange(Vector3 position, EUnitCondition fails)
    {
      GameObject go = new GameObject("Conditions", new System.Type[2]{ typeof (RectTransform), typeof (DelayStart) });
      Transform transform = go.get_transform();
      for (int index = 0; index < (int) Unit.MAX_UNIT_CONDITION; ++index)
      {
        EUnitCondition condition = (EUnitCondition) (1 << index);
        if ((condition & fails) != (EUnitCondition) 0)
        {
          Transform conditionChangeEffect = this.CreateConditionChangeEffect(condition);
          if (Object.op_Inequality((Object) conditionChangeEffect, (Object) null))
            conditionChangeEffect.SetParent(transform, false);
        }
      }
      SceneBattle.Popup2D(go, position, 0, 0.0f);
    }

    public void ShowSkillNamePlate(string skillName)
    {
      this.mSkillNamePlate.SetSkillName(skillName);
      this.mSkillNamePlate.Open();
      ((Component) this.mSkillNamePlate).get_transform().SetParent(((Component) this.OverlayCanvas).get_transform(), false);
    }

    public static void Popup2D(GameObject go, Vector3 position, int priority = 0, float yOffset = 0)
    {
      if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        return;
      SceneBattle.Instance.InternalPopup2D(go, position, priority, yOffset);
    }

    private void InternalPopup2D(GameObject go, Vector3 position, int priority, float yOffset = 0)
    {
      if (Object.op_Equality((Object) go, (Object) null))
        return;
      this.mPopups.Add(new KeyValuePair<SceneBattle.PupupData, GameObject>(new SceneBattle.PupupData(position, priority, yOffset), go));
      go.get_transform().SetParent(((Component) this.OverlayCanvas).get_transform(), false);
      this.mPopupPositionCache.Clear();
    }

    private void LayoutPopups(Camera cam)
    {
      Canvas overlayCanvas = this.OverlayCanvas;
      if (Object.op_Equality((Object) overlayCanvas, (Object) null))
        return;
      RectTransform transform1 = ((Component) overlayCanvas).get_transform() as RectTransform;
      int[] array = new int[this.mPopups.Count];
      for (int index = 0; index < this.mPopups.Count; ++index)
      {
        GameObject gameObject = this.mPopups[index].Value;
        if (Object.op_Inequality((Object) gameObject, (Object) null))
          array[index] = gameObject.get_transform().GetSiblingIndex();
      }
      Array.Sort<int>(array);
      int[] numArray = new int[this.mPopups.Count];
      GameObject[] gameObjectArray = new GameObject[this.mPopups.Count];
      for (int index = 0; index < this.mPopups.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mPopups[index].Value, (Object) null))
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
        if (Object.op_Inequality((Object) gameObjectArray[index], (Object) null))
          gameObjectArray[index].get_transform().SetSiblingIndex(array[index]);
      }
      for (int index1 = 0; index1 < this.mPopups.Count; ++index1)
      {
        GameObject gameObject = this.mPopups[index1].Value;
        if (Object.op_Equality((Object) gameObject, (Object) null))
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
      if (!Object.op_Inequality((Object) this.mBlockedGridMarker, (Object) null))
        return;
      if (this.mDisplayBlockedGridMarker)
      {
        TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
        if (Object.op_Inequality((Object) unitController, (Object) null))
        {
          Vector3 screenPoint = cam.WorldToScreenPoint(unitController.CenterPosition);
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
      if (Object.op_Equality((Object) overlayCanvas, (Object) null))
        return;
      List<TacticsUnitController> instances = TacticsUnitController.Instances;
      RectTransform transform = ((Component) overlayCanvas).get_transform() as RectTransform;
      this.mLayoutGauges.Clear();
      List<Vector3> vector3List = new List<Vector3>();
      for (int index = 0; index < instances.Count; ++index)
      {
        TacticsUnitController tacticsUnitController = instances[index];
        RectTransform hpGaugeTransform = tacticsUnitController.HPGaugeTransform;
        if (!Object.op_Equality((Object) hpGaugeTransform, (Object) null) && ((Component) hpGaugeTransform).get_gameObject().get_activeInHierarchy())
        {
          Vector3 vector3 = Vector3.op_Addition(((Component) tacticsUnitController).get_transform().get_position(), Vector3.get_up());
          if (Object.op_Inequality((Object) ((Transform) hpGaugeTransform).get_parent(), (Object) transform))
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
      if (Object.op_Inequality((Object) controller, (Object) null))
        controller.AlwaysUpdate = true;
      for (int index = 0; index < this.mTacticsUnits.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mTacticsUnits[index], (Object) controller))
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
          if (Object.op_Inequality((Object) unitController, (Object) null))
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
      if (MonoSingleton<GameManager>.Instance.IsTutorial() && Object.op_Inequality((Object) SGHighlightObject.Instance(), (Object) null) || (!this.mBattle.IsMapCommand || this.mBattle.IsUnitAuto(this.mBattle.CurrentUnit)))
        return;
      TacticsUnitController unitController = this.FindUnitController(this.mBattle.CurrentUnit);
      if (Object.op_Inequality((Object) unitController, (Object) null) && unitController.IsStartSkill())
        return;
      VirtualStick2 virtualStick = this.mBattleUI.VirtualStick;
      if (!Object.op_Inequality((Object) virtualStick, (Object) null))
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
      if (Object.op_Inequality((Object) unitController, (Object) null) && unitController.IsStartSkill())
        return;
      VirtualStick2 virtualStick = this.mBattleUI.VirtualStick;
      if (!Object.op_Inequality((Object) virtualStick, (Object) null))
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
      if (Object.op_Equality((Object) this.mJumpSpotEffectTemplate, (Object) null))
        return;
      for (int index1 = 0; index1 < this.mBattle.Units.Count; ++index1)
      {
        Unit unit = this.mBattle.Units[index1];
        if (!unit.IsDead && unit.CastSkill != null && unit.CastSkill.CastType == ECastTypes.Jump)
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
            GameObject gameObject = Object.Instantiate((Object) this.mJumpSpotEffectTemplate, this.CalcGridCenter(unit.x, unit.y), Quaternion.get_identity()) as GameObject;
            this.mJumpSpotEffects.Add(new KeyValuePair<Unit, GameObject>(unit, gameObject));
          }
        }
      }
      for (int index = this.mJumpSpotEffects.Count - 1; index >= 0; --index)
      {
        Unit key = this.mJumpSpotEffects[index].Key;
        if (key.IsDead || key.CastSkill == null || key.CastSkill.CastType != ECastTypes.Jump)
        {
          GameUtility.StopEmitters(this.mJumpSpotEffects[index].Value);
          this.mJumpSpotEffects[index].Value.AddComponent<OneShotParticle>();
          this.mJumpSpotEffects.RemoveAt(index);
        }
      }
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
          SkillParam skillParam = unit.Shields[index2].skill.SkillParam;
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
      return (IEnumerator) new SceneBattle.\u003CLoadShieldEffectsAsync\u003Ec__Iterator37() { skills = skills, \u003C\u0024\u003Eskills = skills, \u003C\u003Ef__this = this };
    }

    private GameObject SpawnShieldEffect(TacticsUnitController unit, SkillParam skill, int value, int valueMax, int turn, int turnMax)
    {
      if (!this.mShieldEffects.ContainsKey(skill))
        return (GameObject) null;
      GameObject mShieldEffect = this.mShieldEffects[skill];
      if (Object.op_Equality((Object) mShieldEffect, (Object) null))
        return (GameObject) null;
      GameObject gameObject = Object.Instantiate((Object) mShieldEffect, ((Component) unit).get_transform().get_position(), Quaternion.get_identity()) as GameObject;
      Animator component = (Animator) gameObject.GetComponent<Animator>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.SetInteger("shield_val", value);
        if (valueMax > 0)
          component.SetFloat("shield_val_norm", (float) value / (float) valueMax);
        component.SetInteger("shield_turn", turn);
        if (turnMax > 0)
          component.SetFloat("shield_turn_norm", (float) turn / (float) turnMax);
      }
      return gameObject;
    }

    public class MoveInput
    {
      public SceneBattle SceneOwner;
      public SceneBattle.MoveInput.TargetSelectEvent OnAttackTargetSelect;

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
        if (!Object.op_Inequality((Object) this.mGUIEvent, (Object) null))
          return;
        Object.Destroy((Object) this.mGUIEvent);
        this.mGUIEvent = (GUIEventListener) null;
      }

      public override void Update(SceneBattle self)
      {
        if (!string.IsNullOrEmpty(GlobalVars.SelectedQuestID) || !Object.op_Equality((Object) this.mGUIEvent, (Object) null) || Network.Mode != Network.EConnectMode.Offline)
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
        return (IEnumerator) new SceneBattle.State_Start.\u003CLoadAndExecuteEvent\u003Ec__Iterator38() { eventName = eventName, \u003C\u0024\u003EeventName = eventName, \u003C\u003Ef__this = this };
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
            if (!Object.op_Equality((Object) self.mBattleUI_MultiPlay, (Object) null))
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
                else
                  self.mBattleUI_MultiPlay.OnEnemyUnitStart();
              }
            }
            self.mSkillDamageDispType = eDamageDispType.Standard;
            self.GotoState<SceneBattle.State_PreUnitStart>();
            break;
          }
          if (peek is LogUnitEntry)
          {
            self.GotoState<SceneBattle.State_SpawnUnit>();
            break;
          }
          if (peek is LogMapCommand)
          {
            self.RemoveLog();
            self.GotoMapCommand();
            break;
          }
          if (peek is LogMapWait)
          {
            self.GotoState<SceneBattle.State_MapWait>();
            break;
          }
          if (peek is LogMapMove)
          {
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
              self.GotoPrepareSkill();
              break;
            }
            if (peek is LogCastSkillStart)
            {
              self.GotoState<SceneBattle.State_CastSkillStart>();
              break;
            }
            if (peek is LogCastSkillEnd)
            {
              self.GotoState<SceneBattle.State_CastSkillEnd>();
              break;
            }
            if (peek is LogUnitEnd)
            {
              if (!Object.op_Equality((Object) self.mBattleUI_MultiPlay, (Object) null))
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
              self.GotoState<SceneBattle.State_UnitEnd>();
              break;
            }
            if (peek is LogTurnEnd)
            {
              self.GotoState<SceneBattle.State_TurnEnd>();
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
                self.GotoState<SceneBattle.State_MapDead>();
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
            else if (peek is LogHeal)
            {
              DebugUtility.LogWarning("warning heal.");
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
                self.RemoveLog();
              else if (peek is LogCureCondition)
              {
                self.RemoveLog();
              }
              else
              {
                if (peek is LogCast)
                {
                  self.GotoState<SceneBattle.State_PrepareCast>();
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

    private class State_QuestStart : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.UpdateBGM();
      }

      public override void Update(SceneBattle self)
      {
        self.GotoMapStart();
      }
    }

    private class State_QuestStartV2 : State<SceneBattle>
    {
      public override void Update(SceneBattle self)
      {
        self.GotoMapStart();
      }
    }

    private class State_MapCommandAI : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (!Object.op_Inequality((Object) unitController, (Object) null))
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
        if (!Object.op_Inequality((Object) unitController, (Object) null))
          return;
        unitController.HideCursor(false);
      }

      public override void End(SceneBattle self)
      {
      }

      private void SyncCameraPosition(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (Object.op_Equality((Object) unitController, (Object) null))
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
        if (Object.op_Equality((Object) go, (Object) null))
          return;
        Win_Btn_Decide_Title_Flx component1 = (Win_Btn_Decide_Title_Flx) go.GetComponent<Win_Btn_Decide_Title_Flx>();
        if (Object.op_Inequality((Object) component1, (Object) null))
          this.mMsgBox.Add(component1);
        Win_Btn_DecideCancel_FL_C component2 = (Win_Btn_DecideCancel_FL_C) go.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (!Object.op_Inequality((Object) component2, (Object) null))
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
              this.mScene.SendInputGridXY(this.mScene.Battle.CurrentUnit, this.mDestX, this.mDestY, this.mScene.Battle.CurrentUnit.Direction);
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
          Vector2[] vector2Array = new Vector2[8]{ new Vector2(-1f, 1f), new Vector2(0.0f, 1f), new Vector2(1f, 1f), new Vector2(1f, 0.0f), new Vector2(1f, -1f), new Vector2(0.0f, -1f), new Vector2(-1f, -1f), new Vector2(-1f, 0.0f) };
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
          if (Object.op_Inequality((Object) VirtualStick.Instance, (Object) null))
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
            self.SendInputGridXY(currentUnit, currentUnit.x, currentUnit.y, currentUnit.Direction);
            self.SendInputMove(currentUnit, this.mController);
            self.SendInputMoveEnd(currentUnit, false);
            DebugUtility.Log("MoveEnd x:" + (object) currentUnit.x + " y:" + (object) currentUnit.y + " action:" + (object) this.mController.IsPlayingFieldAction);
          }
          self.SendInputUnitTimeLimit(currentUnit);
          self.SendInputFlush();
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
              self.SendInputGridXY(self.Battle.CurrentUnit, this.mDestX, this.mDestY, self.Battle.CurrentUnit.Direction);
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
                  self.SendInputGridXY(self.Battle.CurrentUnit, this.mDestX, this.mDestY, self.Battle.CurrentUnit.Direction);
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

    private class State_MapDead : State<SceneBattle>
    {
      private TacticsUnitController mController;
      private bool mIsPlayDead;
      private DeadTypes mDeadType;
      private bool mIsCountDownStart;

      public override void Begin(SceneBattle self)
      {
        LogDead peek = (LogDead) self.mBattle.Logs.Peek;
        this.mController = self.FindUnitController(peek.self);
        if (Object.op_Inequality((Object) this.mController, (Object) null))
        {
          this.mController.LoadDeathAnimation(TacticsUnitController.DeathAnimationTypes.Normal);
          if (peek.self.Side == EUnitSide.Enemy && peek.self.Drop != null && peek.self.Drop.IsEnableDrop())
            self.mDroppedTreasures.Add(new SceneBattle.TreasureSpawnInfo()
            {
              Drop = peek.self.Drop,
              Position = ((Component) this.mController).get_transform().get_position()
            });
          this.mDeadType = peek.type;
          if (this.mDeadType == DeadTypes.DeathSentence)
            this.OnDeathSentenceEnd();
        }
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        if (Object.op_Inequality((Object) this.mController, (Object) null))
        {
          if (this.mController.IsLoading)
            return;
          if (this.mDeadType == DeadTypes.DeathSentence)
          {
            this.OnDeathSentenceUpdate();
            if (!this.mIsCountDownStart || this.mController.IsDeathSentenceCountDownPlaying())
              return;
          }
          if (!this.mIsPlayDead)
          {
            this.mController.PlayDead(TacticsUnitController.DeathAnimationTypes.Normal);
            self.OnUnitDeath(this.mController.Unit);
            self.mTacticsUnits.Remove(this.mController);
            this.mIsPlayDead = true;
            this.mController.ShowHPGauge(false);
            this.mController.ShowVersusCursor(false);
          }
        }
        else if (this.mIsPlayDead)
          self.OnGimmickUpdate();
        else
          self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
        if (self.mDroppedTreasures.Count > 0)
        {
          self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_Treasure>>();
        }
        else
        {
          if (!this.mIsPlayDead || !Object.op_Equality((Object) this.mController, (Object) null))
            return;
          self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
        }
      }

      private void OnDeathSentenceEnd()
      {
        this.self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        this.self.InterpCameraTarget((Component) this.mController);
      }

      private void OnDeathSentenceUpdate()
      {
        if (this.mIsCountDownStart)
          return;
        Vector3 vector3 = Vector3.op_Subtraction(this.self.mCameraTarget, ((Component) this.mController).get_transform().get_position());
        vector3.y = (__Null) 0.0;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        if ((double) ((Vector3) @vector3).get_magnitude() * (double) ((Vector3) @vector3).get_magnitude() >= 0.25)
          return;
        this.mController.DeathSentenceCountDown(true, 1f);
        this.mIsCountDownStart = true;
      }
    }

    private class State_MapRevive : SceneBattle.State_SpawnUnit
    {
      protected override Unit ReadLog()
      {
        Unit self = ((LogRevive) this.self.mBattle.Logs.Peek).self;
        this.self.RemoveLog();
        return self;
      }
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
        if (Object.op_Equality((Object) this.mUnitController, (Object) null))
        {
          self.RemoveLog();
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
        else
        {
          self.mUpdateCameraPosition = true;
          self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).get_transform());
          self.InterpCameraTarget((Component) this.mUnitController);
          this.mGimmickController.ResetScale();
          this.mDrop = this.mLog.target.Drop;
          if (this.mDrop != null)
          {
            self.AddGoldCount((int) this.mDrop.gold);
            this.mItemDrop = this.mGimmickController.Unit != null && this.mGimmickController.Unit.CheckItemDrop();
            if (this.mItemDrop)
            {
              for (int index = this.mDrop.items.Count - 1; index >= 0; --index)
              {
                if (this.mDrop.items[index] != null && this.mDrop.items[index].param != null && (int) this.mDrop.items[index].num != 0)
                {
                  self.AddTreasureCount(1);
                  break;
                }
              }
            }
          }
          this.mUnitController.BeginLoadPickupAnimation();
          this.mPickup = (MapPickup) ((Component) this.mGimmickController).GetComponentInChildren<MapPickup>();
          this.mPickup.OnPickup = new MapPickup.PickupEvent(this.OnPickupDone);
          this.mGimmickController.BeginLoadPickupAnimation();
          if (!this.mItemDrop || this.mDrop == null || this.mDrop.items.Count <= 0)
            return;
          this.mDropItemEffect = Object.Instantiate((Object) this.mScene.mTreasureDropTemplate, Vector3.op_Addition(((Component) this.mGimmickController).get_transform().get_position(), this.mPickup.DropEffectOffset), (Quaternion) null) as DropItemEffect;
          if (!Object.op_Inequality((Object) this.mDropItemEffect, (Object) null))
            return;
          ((Component) this.mDropItemEffect).get_transform().SetParent(((Component) self.OverlayCanvas).get_transform(), false);
          ((ItemIcon) ((Component) this.mDropItemEffect).get_gameObject().GetComponent<ItemIcon>()).UpdateValue();
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
      }

      private void OnPickupDone()
      {
        if (this.mUnitController.Unit.Side != EUnitSide.Player)
          return;
        if (this.mDrop != null)
        {
          Vector3 position = ((Component) this.mGimmickController).get_transform().get_position();
          if ((int) this.mDrop.gold > 0)
            (Object.Instantiate((Object) this.mScene.mTreasureGoldTemplate, position, Quaternion.get_identity()) as DropGoldEffect).Gold = (int) this.mDrop.gold;
          if (this.mDrop.items.Count > 0 && this.mItemDrop)
          {
            ((Component) this.mDropItemEffect).get_gameObject().SetActive(true);
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
        return (IEnumerator) new SceneBattle.State_PickupGimmick.\u003CGemPramChangePopup\u003Ec__Iterator3A() { \u003C\u003Ef__this = this };
      }
    }

    private struct TreasureSpawnInfo
    {
      public Vector3 Position;
      public Unit.UnitDrop Drop;
    }

    private class State_Treasure : State<SceneBattle>
    {
      private float mDelay;
      private TreasureBox mTreasureBox;

      public override void Begin(SceneBattle self)
      {
        this.mDelay = GameSettings.Instance.Quest.TreasureTime;
        for (int index1 = self.mDroppedTreasures.Count - 1; index1 >= 0; --index1)
        {
          Unit.UnitDrop drop = self.mDroppedTreasures[index1].Drop;
          TreasureBox treasureBox = (TreasureBox) Object.Instantiate<TreasureBox>((M0) self.mTreasureBoxTemplate);
          ((Component) treasureBox).get_transform().set_parent(((Component) self).get_transform());
          ((Component) treasureBox).get_transform().set_position(self.mDroppedTreasures[index1].Position);
          Unit.DropItem DropItem = (Unit.DropItem) null;
          if (0 < drop.items.Count)
            DropItem = drop.items[drop.items.Count - 1];
          treasureBox.Open(DropItem, self.mTreasureDropTemplate, (int) drop.gold, self.mTreasureGoldTemplate);
          for (int index2 = 0; index2 < drop.items.Count; ++index2)
          {
            if (drop.items[index2] != null && drop.items[index1].param != null && (int) drop.items[index1].num != 0)
            {
              self.AddTreasureCount(1);
              break;
            }
          }
          self.AddGoldCount((int) drop.gold);
          this.mTreasureBox = treasureBox;
          ((Component) this.mTreasureBox).get_gameObject().SetActive(false);
        }
        self.mDroppedTreasures.Clear();
      }

      public override void Update(SceneBattle self)
      {
        this.mDelay -= Time.get_deltaTime();
        if ((double) this.mDelay > 0.0)
          return;
        if (Object.op_Inequality((Object) this.mTreasureBox, (Object) null))
        {
          if (((Component) this.mTreasureBox).get_gameObject().get_activeInHierarchy())
            return;
          ((Component) this.mTreasureBox).get_gameObject().SetActive(true);
        }
        else
          self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_BattleDead : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        Unit self1 = ((LogDead) self.mBattle.Logs.Peek).self;
        self.OnUnitDeath(self1);
        self.HideUnitMarkers(self1);
        TacticsUnitController unitController = self.FindUnitController(self1);
        self.mTacticsUnits.Remove(unitController);
        Object.Destroy((Object) ((Component) unitController).get_gameObject());
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
        }
        if (castSkill.EffectType == SkillEffectTypes.Changing)
          self.GotoState<SceneBattle.State_CastSkillStartChange>();
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
        if (Object.op_Inequality((Object) this.mCasterController, (Object) null))
        {
          LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
          IntVector2 pos = peek.pos;
          Vector3 vector3 = self.CalcGridCenter(peek.pos.x, peek.pos.y);
          this.mCasterController.JumpMapFallPos = pos;
          this.mCasterController.JumpFallPos = vector3;
          this.mCasterController.ChargeIcon.Close();
        }
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
        if (!Object.op_Inequality((Object) this.mCasterController, (Object) null))
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

    private class State_UnitEnd : State<SceneBattle>
    {
      private bool mIsPopDamage;
      private bool mIsShakeStart;
      private Unit mCurrentUnit;
      private TacticsUnitController mController;

      public override void Begin(SceneBattle self)
      {
        self.ToggleRenkeiAura(false);
        if (self.mTutorialTriggers != null)
        {
          TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
          if (Object.op_Inequality((Object) unitController, (Object) null))
          {
            for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
              self.mTutorialTriggers[index].OnUnitEnd(self.mBattle.CurrentUnit, unitController.TurnCount);
          }
        }
        if (self.Battle.CurrentUnit.OwnerPlayerIndex > 0)
          self.SendCheckMultiPlay();
        self.HideUnitCursor(((LogUnitEnd) self.mBattle.Logs.Peek).self);
        if (self.mBattle.IsUnitAuto(self.mBattle.CurrentUnit))
          self.HideGrid();
        if (Object.op_Inequality((Object) self.mTouchController, (Object) null))
          self.mTouchController.IgnoreCurrentTouch();
        VirtualStick2 virtualStick = self.mBattleUI.VirtualStick;
        if (Object.op_Inequality((Object) virtualStick, (Object) null))
          virtualStick.Visible = false;
        this.mCurrentUnit = self.Battle.CurrentUnit;
        this.mController = self.FindUnitController(this.mCurrentUnit);
        if (Object.op_Inequality((Object) null, (Object) this.mController))
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
        if (Object.op_Equality((Object) null, (Object) this.mController))
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
            if (peek != null && this.mCurrentUnit.IsUnitConditionDamage() && !this.mCurrentUnit.IsDead)
            {
              this.mController.ShakeStart = true;
              return;
            }
            if (peek != null && isDead != this.mCurrentUnit.IsDead)
              self.PopupDamageNumber(((Component) this.mController).get_transform().get_position(), peek.damage);
          }
          else
          {
            this.mController.AdvanceShake();
            if (0.300000011920929 <= (double) this.mController.GetShakeProgress() && !this.mIsPopDamage)
            {
              this.mIsPopDamage = true;
              LogDamage peek = self.Battle.Logs.Peek as LogDamage;
              if (self.mSkillDamageDispType != eDamageDispType.Standard)
                SceneBattle.Instance.PopupDamageDsgNumber(((Component) this.mController).get_transform().get_position(), peek.damage, self.mSkillDamageDispType);
              else
                self.PopupDamageNumber(((Component) this.mController).get_transform().get_position(), peek.damage);
            }
            if (!this.mController.IsShakeEnd())
              return;
          }
          self.GotoState<SceneBattle.State_WaitForLog>();
        }
      }

      public override void End(SceneBattle self)
      {
        self.HideAllHPGauges();
        if (self.mFirstTurn)
          self.mFirstTurn = false;
        self.DeleteOnGimmickIcon();
      }
    }

    private class State_TurnEnd : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.SendCheckMultiPlay();
        self.RemoveLog();
      }

      public override void Update(SceneBattle self)
      {
        if (self.Battle.IsMultiPlay)
          self.GotoState<SceneBattle.State_MultiPlayRevive>();
        else
          self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self)
      {
      }
    }

    private class State_MapEndV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.SendCheckMultiPlay();
        self.RemoveLog();
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
            DebugUtility.Log(">>>>>>>>>>>>" + self.Battle.QuestID);
            AnalyticsManager.SetFirstMissionClear(self.Battle.QuestID);
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
        self.RemoveLog();
        if (Object.op_Inequality((Object) self.mBattleUI, (Object) null))
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
        switch (questResult)
        {
          case BattleCore.QuestResult.Win:
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
            if (Object.op_Inequality((Object) self.mBattleUI_MultiPlay, (Object) null))
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
            if (Object.op_Inequality((Object) self.mBattleUI_MultiPlay, (Object) null) && !self.Battle.IsMultiVersus)
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
              self.GotoState_WaitSignal<SceneBattle.State_Result>();
              break;
            }
            if (!self.CurrentQuest.CheckDisableContinue())
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
            if (Object.op_Inequality((Object) self.mBattleUI_MultiPlay, (Object) null))
              self.mBattleUI_MultiPlay.OnQuestRetreat();
            GameUtility.FadeOut(2f);
            self.GotoState<SceneBattle.State_ExitQuest>();
            break;
        }
      }
    }

    private class State_Result : State<SceneBattle>
    {
      private bool mOnResult;

      public override void Begin(SceneBattle self)
      {
        BattleCore.Record questRecord = self.mBattle.GetQuestRecord();
        if (self.IsPlayingArenaQuest)
        {
          this.mOnResult = true;
          MonoSingleton<MySound>.Instance.PlayBGM("BGM_0006", (string) null);
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
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                SceneBattle.State_Result.\u003CBegin\u003Ec__AnonStorey1DB beginCAnonStorey1Db = new SceneBattle.State_Result.\u003CBegin\u003Ec__AnonStorey1DB();
                // ISSUE: reference to a compiler-generated field
                beginCAnonStorey1Db.startedPlayer = myPlayersStarted[index];
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                if (beginCAnonStorey1Db.startedPlayer != null && beginCAnonStorey1Db.startedPlayer.playerIndex != instance.MyPlayerIndex && !string.IsNullOrEmpty(beginCAnonStorey1Db.startedPlayer.FUID))
                {
                  // ISSUE: reference to a compiler-generated method
                  MultiFuid multiFuid = multiFuids != null ? multiFuids.Find(new Predicate<MultiFuid>(beginCAnonStorey1Db.\u003C\u003Em__193)) : (MultiFuid) null;
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
          if (!questRecord.IsZero || flag1 || (self.CurrentQuest.type == QuestTypes.Tower || flag2))
            return;
          self.ExitRequest = SceneBattle.ExitRequests.End;
        }
      }

      private void StartResult()
      {
        if (this.mOnResult)
          return;
        this.mOnResult = true;
        MonoSingleton<MySound>.Instance.PlayBGM("BGM_0006", (string) null);
        if (this.self.Battle.IsMultiPlay)
        {
          if (this.self.Battle.IsMultiVersus)
            this.self.mBattleUI.OnResult_Versus();
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
        if (!self.mQuestResultSent)
          return;
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
        {
          Network.RequestAPI((WebAPI) new ReqBtlComCont(self.Battle.BtlID, questRecord, new Network.ResponseCallback(this.OnSuccess), PunMonoSingleton<MyPhoton>.Instance.IsMultiPlay), false);
        }
        else
        {
          BattleCore.Json_Battle response = new BattleCore.Json_Battle();
          response.btlid = this.mScene.Battle.BtlID;
          response.btlinfo = new BattleCore.Json_BtlInfo();
          response.btlinfo.seed = 0;
          this.ResBtlComCont(response);
        }
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
            AnalyticsManager.TrackSpendCoin("ContinueQuest", (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost);
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
          if (!Object.op_Equality((Object) unitController, (Object) null) && unitController.Unit.IsDead)
          {
            this.mScene.mTacticsUnits.Remove(unitController);
            GameUtility.DestroyGameObject(((Component) unitController).get_gameObject());
          }
        }
        GameUtility.FadeIn(1f);
        this.mScene.mUnitStartCount = 0;
        this.mScene.Battle.ContinueStart(response.btlid, response.btlinfo.seed);
        for (int index = 0; index < player.Count; ++index)
        {
          TacticsUnitController unitController = this.mScene.FindUnitController(player[index]);
          if (!Object.op_Equality((Object) unitController, (Object) null))
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
        if (!self.mQuestResultSent)
          self.SubmitResult();
        self.mPauseReqCount = 0;
        TimeManager.SetTimeScale(TimeManager.TimeScaleGroups.Game, 1f);
        self.StartCoroutine(this.ExitAsync());
      }

      [DebuggerHidden]
      private IEnumerator ExitAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_ExitQuest.\u003CExitAsync\u003Ec__Iterator3B() { \u003C\u003Ef__this = this };
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
        UIUtility.ConfirmBox(LocalizedText.Get("sys.CONFIRM_CONTINUE", (object) MonoSingleton<GameManager>.Instance.Player.Coin, (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ContinueCoinCost), (string) null, new UIUtility.DialogResultEvent(this.OnDecide), new UIUtility.DialogResultEvent(this.OnCancel), (GameObject) null, false, -1);
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
          if (!Object.op_Equality((Object) this.mHealEffect, (Object) null) || this.mController.IsHPGaugeChanging)
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
        return (IEnumerator) new SceneBattle.State_AutoHeal.\u003CWait\u003Ec__Iterator3C() { wait = wait, \u003C\u0024\u003Ewait = wait, \u003C\u003Ef__this = this };
      }

      private void StartEffect()
      {
        GameObject gameObject = this.mLog.type != LogAutoHeal.HealType.Hp ? this.self.mMapAddGemEffectTemplate : this.self.mAutoHealEffectTemplate;
        if (Object.op_Inequality((Object) gameObject, (Object) null))
        {
          this.mHealEffect = Object.Instantiate((Object) gameObject, ((Component) this.mController).get_transform().get_position(), Quaternion.get_identity()) as GameObject;
          if (Object.op_Inequality((Object) this.mHealEffect, (Object) null))
          {
            this.mHealEffect.get_transform().SetParent(((Component) this.mController).get_transform());
            this.mHealEffect.RequireComponent<OneShotParticle>();
          }
        }
        SceneBattle.Instance.PopupHpHealNumber(this.mController.CenterPosition, this.mLog.value);
        if (this.mLog.type != LogAutoHeal.HealType.Jewel)
          this.mController.UpdateHPRelative(this.mLog.value, 0.5f);
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
        if (!Object.op_Inequality((Object) unitController, (Object) null))
          return;
        unitController.ChargeIcon.Open();
      }
    }

    private class State_JumpCastStart : State<SceneBattle>
    {
      private float WaitTime = 1f;
      private TacticsUnitController mCasterController;
      private float CountTime;

      public override void Begin(SceneBattle self)
      {
        LogCast peek = self.Battle.Logs.Peek as LogCast;
        this.mCasterController = self.FindUnitController(peek.self);
        self.RemoveLog();
        self.mUpdateCameraPosition = true;
        self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).get_transform());
        self.InterpCameraTarget((Component) this.mCasterController);
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance);
      }

      public override void Update(SceneBattle self)
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

      public override void End(SceneBattle self)
      {
        self.RefreshJumpSpots();
        self.mUpdateCameraPosition = false;
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
          self.mSkillDamageDispType = skill.SkillParam.DamageDispType;
          this.mLoadSplash = !peek.is_append;
          self.LoadShieldEffects();
          Unit self1 = peek.self;
          TacticsUnitController unitController = self.FindUnitController(self1);
          if ((int) skill.SkillParam.absorb_damage_rate > 0)
          {
            if (Object.op_Inequality((Object) self.mDrainMpEffectTemplate, (Object) null) && skill.SkillParam.IsJewelAttack())
              unitController.SetDrainEffect(self.mDrainMpEffectTemplate);
            else if (Object.op_Inequality((Object) self.mDrainHpEffectTemplate, (Object) null))
              unitController.SetDrainEffect(self.mDrainHpEffectTemplate);
          }
          if (self.Battle.IsUnitAuto(self1) || self.Battle.EntryBattleMultiPlayTimeUp)
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
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CPrepareSkill\u003Ec__Iterator3D() { skill = skill, unit = unit, \u003C\u0024\u003Eskill = skill, \u003C\u0024\u003Eunit = unit, \u003C\u003Ef__this = this };
      }

      [DebuggerHidden]
      private IEnumerator cutinVoice(Unit unit, SkillData skill)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_PrepareSkill.\u003CcutinVoice\u003Ec__Iterator3E() { unit = unit, skill = skill, \u003C\u0024\u003Eunit = unit, \u003C\u0024\u003Eskill = skill, \u003C\u003Ef__this = this };
      }

      public override void Update(SceneBattle self)
      {
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
          self.GotoState_WaitSignal<SceneBattle.State_Battle_PrepareSkill>();
        }
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
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
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
          if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
            self.mCollaboTargetTuc.SetHpCostSkill(peek.skill.GetHpCost(peek.self));
        }
        for (int index = 0; index < peek.targets.Count; ++index)
        {
          LogSkill.Target target = peek.targets[index];
          TacticsUnitController unitController = self.FindUnitController(target.target);
          this.mTargets.Add(unitController);
          if (flag)
          {
            if (peek.targets[index].IsAvoid())
              unitController.LoadDodgeAnimation();
            else
              unitController.LoadDamageAnimations();
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
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
        {
          ((Component) self.mCollaboTargetTuc).get_transform().set_position(vector3_2);
          ((Component) self.mCollaboTargetTuc).get_transform().set_rotation(Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart1.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position())));
          ((Component) self.mCollaboTargetTuc).get_transform().SetParent(((Component) self.mBattleSceneRoot).get_transform(), false);
        }
        if (this.mTargets.Count == 1 && Object.op_Inequality((Object) this.mTargets[0], (Object) this.mInstigator))
        {
          ((Component) this.mTargets[0]).get_transform().set_position(vector3_3);
          ((Component) this.mTargets[0]).get_transform().set_rotation(Quaternion.LookRotation(Vector3.op_Subtraction(self.mBattleSceneRoot.PlayerStart2.get_position(), self.mBattleSceneRoot.PlayerStart1.get_position())));
          ((Component) this.mTargets[0]).get_transform().SetParent(((Component) self.mBattleSceneRoot).get_transform(), false);
        }
        Transform transform = ((Component) Camera.get_main()).get_transform();
        transform.set_position(Vector3.op_Addition(GameSettings.Instance.Quest.BattleCamera.get_position(), self.mBattleSceneRoot.PlayerStart2.get_position()));
        transform.set_rotation(GameSettings.Instance.Quest.BattleCamera.get_rotation());
        this.mInstigator.SetHitInfoSelf(peek.self_effect);
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
          self.mCollaboTargetTuc.SetHitInfoSelf(new LogSkill.Target());
        for (int index = 0; index < peek.targets.Count; ++index)
          self.FindUnitController(peek.targets[index].target).SetHitInfo(peek.targets[index]);
        self.ToggleJumpSpots(false);
        this.mInstigator.StartSkillWithAnimationOption(self.CalcGridCenter(peek.pos.x, peek.pos.y), Camera.get_main(), this.mTargets.ToArray(), (Vector3[]) null, this.mSkill, GameUtility.Config_SkillAnimation);
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
          self.mCollaboTargetTuc.StartSkill(self.CalcGridCenter(self.mCollaboTargetTuc.Unit.x, self.mCollaboTargetTuc.Unit.y), Camera.get_main(), this.mTargets.ToArray(), (Vector3[]) null, this.mSkill);
        GameUtility.FadeIn(0.2f);
        if (Object.op_Inequality((Object) self.mSkillSplash, (Object) null))
        {
          if (Object.op_Implicit((Object) self.mSkillSplash))
            self.mSkillSplash.Close();
          self.mSkillSplash = (SkillSplash) null;
        }
        if (Object.op_Inequality((Object) self.mSkillSplashCollabo, (Object) null))
        {
          if (Object.op_Implicit((Object) self.mSkillSplashCollabo))
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
      public override void Begin(SceneBattle self)
      {
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
            if (Object.op_Inequality((Object) unitController, (Object) null))
              unitController.UpdateHPRelative(delta, 0.5f);
          }
        }
      }

      [DebuggerHidden]
      private IEnumerator AsyncUpdate()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_Battle_EndSkill.\u003CAsyncUpdate\u003Ec__Iterator3F() { \u003C\u003Ef__this = this };
      }
    }

    private class State_Map_PrepareSkill : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        LogSkill Act = peek;
        SkillParam skillParam = Act.skill.SkillParam;
        TacticsUnitController unitController1 = self.FindUnitController(Act.self);
        self.mUnitsInBattle.Clear();
        self.mUnitsInBattle.Add(unitController1);
        unitController1.AutoUpdateRotation = false;
        unitController1.LoadSkillSequence(skillParam, false, false, (bool) Act.skill.IsCollabo, self.mIsInstigatorSubUnit);
        if (!string.IsNullOrEmpty(skillParam.effect))
          unitController1.LoadSkillEffect(skillParam.effect, false);
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
        {
          self.mCollaboTargetTuc.LoadSkillSequence(skillParam, false, false, (bool) Act.skill.IsCollabo, !self.mIsInstigatorSubUnit);
          if (!string.IsNullOrEmpty(skillParam.effect))
            self.mCollaboTargetTuc.LoadSkillEffect(skillParam.effect, !self.mIsInstigatorSubUnit);
          self.mUnitsInBattle.Add(self.mCollaboTargetTuc);
        }
        if (0 < (int) skillParam.hp_cost)
        {
          unitController1.SetHpCostSkill(Act.skill.GetHpCost(Act.self));
          if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
            self.mCollaboTargetTuc.SetHpCostSkill(Act.skill.GetHpCost(Act.self));
        }
        for (int index = 0; index < Act.targets.Count; ++index)
        {
          TacticsUnitController unitController2 = self.FindUnitController(Act.targets[index].target);
          if (Act.targets[index].IsAvoid())
            unitController2.LoadDodgeAnimation();
          else
            unitController2.LoadDamageAnimations();
          self.mUnitsInBattle.Add(unitController2);
        }
        if (self.Battle.IsUnitAuto(Act.self))
        {
          IntVector2 intVector2 = self.CalcCoord(unitController1.CenterPosition);
          GridMap<bool> scopeGridMap = self.mBattle.CreateScopeGridMap(self.mBattle.CurrentUnit, intVector2.x, intVector2.y, Act.pos.x, Act.pos.y, Act.skill);
          self.mSkillDirectionByKouka = self.GetSkillDirectionByTargetArea(self.Battle.CurrentUnit, scopeGridMap);
        }
        unitController1.SkillTurn(Act, self.mSkillDirectionByKouka);
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
          self.mCollaboTargetTuc.SkillTurn(Act, self.mSkillDirectionByKouka);
        self.SetPrioritizedUnits(self.mUnitsInBattle);
        bool flag = peek.IsRenkei();
        self.mAllowCameraRotation = false;
        self.mAllowCameraTranslation = false;
        if (flag)
        {
          if (Object.op_Inequality((Object) unitController1, (Object) null))
            unitController1.LoadGenkidamaAnimation(true);
          self.GotoState<SceneBattle.State_Map_LoadSkill_Renkei1>();
        }
        else
        {
          if (Object.op_Inequality((Object) unitController1, (Object) null))
          {
            self.SetCameraOffset(((Component) GameSettings.Instance.Quest.UnitCamera).get_transform());
            self.InterpCameraTarget((Component) unitController1);
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
        if (Object.op_Inequality((Object) self.mSkillSplash, (Object) null))
        {
          if (Object.op_Implicit((Object) self.mSkillSplash))
            self.mSkillSplash.Close();
          if (self.mWaitSkillSplashClose)
            return;
          self.mSkillSplash = (SkillSplash) null;
        }
        if (Object.op_Inequality((Object) self.mSkillSplashCollabo, (Object) null))
        {
          if (Object.op_Implicit((Object) self.mSkillSplashCollabo))
            self.mSkillSplashCollabo.Close();
          if (self.mWaitSkillSplashClose)
            return;
          self.mSkillSplashCollabo = (SkillSplashCollabo) null;
        }
        LogSkill peek = self.mBattle.Logs.Peek as LogSkill;
        TacticsUnitController unitController = self.FindUnitController(peek.self);
        if (Object.op_Inequality((Object) unitController, (Object) null) && unitController.HasPreSkillAnimation)
          self.GotoState<SceneBattle.State_Map_PlayPreSkillAnim>();
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
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
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
        return (IEnumerator) new SceneBattle.State_Map_PlayPreSkillAnim.\u003CAsyncWork\u003Ec__Iterator40() { \u003C\u003Ef__this = this };
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
          if (Object.op_Inequality((Object) unitController, (Object) null))
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
        if (Object.op_Inequality((Object) unitController, (Object) null))
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
          if (Object.op_Inequality((Object) self.mSkillSplash, (Object) null))
          {
            if (Object.op_Implicit((Object) self.mSkillSplash))
              self.mSkillSplash.Close();
            self.mSkillSplash = (SkillSplash) null;
          }
          if (Object.op_Inequality((Object) self.mSkillSplashCollabo, (Object) null))
          {
            if (Object.op_Implicit((Object) self.mSkillSplashCollabo))
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
        else if (this.mInstigator.GetSkillCameraType() == SkillSequence.MapCameraTypes.FirstTargetCenter)
        {
          this.mCameraStart.x = this.mActionInfo.self.x;
          this.mCameraStart.y = this.mActionInfo.self.y;
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_SkillCameraDistance * 1.5f);
        }
        else if (this.mInstigator.GetSkillCameraType() == SkillSequence.MapCameraTypes.FarDistance)
        {
          self.mUpdateCameraPosition = true;
          self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        }
        SkillData skill = this.mActionInfo.skill;
        SkillParam skillParam = skill.SkillParam;
        List<Vector3> vector3List = (List<Vector3>) null;
        if (skillParam.IsAreaSkill())
        {
          vector3List = new List<Vector3>();
          GridMap<bool> scopeGridMap = self.mBattle.CreateScopeGridMap(this.mActionInfo.self, this.mActionInfo.self.x, this.mActionInfo.self.y, this.mActionInfo.pos.x, this.mActionInfo.pos.y, skill);
          for (int y = 0; y < scopeGridMap.h; ++y)
          {
            for (int x = 0; x < scopeGridMap.w; ++x)
            {
              if (scopeGridMap.get(x, y))
                vector3List.Add(self.CalcGridCenter(x, y));
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
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
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
            unitController.ShowElementEffectOnHit = !target.IsNormalEffectElement() ? (!target.IsEffectiveElement() ? TacticsUnitController.EElementEffectTypes.NotEffective : TacticsUnitController.EElementEffectTypes.Effective) : TacticsUnitController.EElementEffectTypes.Normal;
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
        this.mInstigator.StartSkillWithAnimationOption(self.CalcGridCenter(this.mActionInfo.pos.x, this.mActionInfo.pos.y), Camera.get_main(), targets, vector3List == null ? (Vector3[]) null : vector3List.ToArray(), skill.SkillParam, GameUtility.Config_SkillAnimation);
        if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
          self.mCollaboTargetTuc.StartSkill(self.CalcGridCenter(this.mActionInfo.pos.x, this.mActionInfo.pos.y), Camera.get_main(), targets, vector3List == null ? (Vector3[]) null : vector3List.ToArray(), skill.SkillParam);
        if (skill.SkillType == ESkillType.Skill || skill.SkillType == ESkillType.Reaction)
          self.ShowSkillNamePlate(skill.Name);
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
          if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null) && !self.mCollaboTargetTuc.isIdle)
            return;
          using (List<TacticsUnitController>.Enumerator enumerator = this.mTargets.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TacticsUnitController current = enumerator.Current;
              if (Object.op_Implicit((Object) current) && current.IsBusy)
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
            }
            if (this.mActionInfo.IsRenkei())
              self.ToggleRenkeiAura(false);
            self.RefreshJumpSpots();
            this.mInstigator.AnimateVessel(0.0f, 0.5f);
            if (Object.op_Inequality((Object) self.mCollaboTargetTuc, (Object) null))
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
            self.GotoState<SceneBattle.State_SpawnShieldEffects>();
          }
        }
      }
    }

    private class State_SpawnShieldEffects : State<SceneBattle>
    {
      private TacticsUnitController mUnit;
      private TacticsUnitController.ShieldState mShield;

      public override void Begin(SceneBattle self)
      {
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          self.mTacticsUnits[index].UpdateShields();
        if (!this.FindChangedShield(out this.mUnit, out this.mShield))
          self.GotoState<SceneBattle.State_TriggerHPEvents>();
        else
          self.StartCoroutine(this.SpawnEffectsAsync());
      }

      private bool FindChangedShield(out TacticsUnitController unit, out TacticsUnitController.ShieldState shield)
      {
        for (int index1 = 0; index1 < this.self.mTacticsUnits.Count; ++index1)
        {
          for (int index2 = 0; index2 < this.self.mTacticsUnits[index1].Shields.Count; ++index2)
          {
            if (this.self.mTacticsUnits[index1].Shields[index2].Dirty)
            {
              unit = this.self.mTacticsUnits[index1];
              shield = this.self.mTacticsUnits[index1].Shields[index2];
              return true;
            }
          }
        }
        unit = (TacticsUnitController) null;
        shield = (TacticsUnitController.ShieldState) null;
        return false;
      }

      [DebuggerHidden]
      private IEnumerator SpawnEffectsAsync()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_SpawnShieldEffects.\u003CSpawnEffectsAsync\u003Ec__Iterator41() { \u003C\u003Ef__this = this };
      }
    }

    private class State_TriggerHPEvents : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        if (Object.op_Inequality((Object) self.mEventScript, (Object) null))
        {
          for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          {
            self.mEventSequence = self.mEventScript.OnUnitHPChange(self.mTacticsUnits[index]);
            if (Object.op_Inequality((Object) self.mEventSequence, (Object) null))
            {
              self.GotoState<SceneBattle.State_WaitEvent<SceneBattle.State_TriggerHPEvents>>();
              return;
            }
          }
        }
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.GotoState<SceneBattle.State_WaitGC<SceneBattle.State_WaitForLog>>();
      }
    }

    private class State_WaitGC<NextState> : State<SceneBattle> where NextState : State<SceneBattle>, new()
    {
      private AsyncOperation mAsyncOp;

      public override void Begin(SceneBattle self)
      {
        this.mAsyncOp = AssetManager.UnloadUnusedAssets();
      }

      public override void Update(SceneBattle self)
      {
        if (!this.mAsyncOp.get_isDone())
          return;
        self.GotoState<NextState>();
      }
    }

    private class State_SpawnUnit : State<SceneBattle>
    {
      private LoadRequest mLoadRequest;

      protected virtual Unit ReadLog()
      {
        Unit self = (this.self.mBattle.Logs.Peek as LogUnitEntry).self;
        this.self.RemoveLog();
        return self;
      }

      public override void Begin(SceneBattle self)
      {
        Unit unit = this.ReadLog();
        self.StartCoroutine(this.AsyncWork(unit));
      }

      [DebuggerHidden]
      private IEnumerator AsyncWork(Unit unit)
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new SceneBattle.State_SpawnUnit.\u003CAsyncWork\u003Ec__Iterator39() { unit = unit, \u003C\u0024\u003Eunit = unit, \u003C\u003Ef__this = this };
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
        return (IEnumerator) new SceneBattle.State_ArenaCalc.\u003CsendResultStartReplay\u003Ec__Iterator42() { self = self, \u003C\u0024\u003Eself = self };
      }
    }

    private enum EMultiPlayCommand
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

    private class MultiPlayInput
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

    private enum EMultiPlayRecvDataHeader
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
      NUM,
    }

    public enum CHEAT_TYPE
    {
      MOVE,
      MP,
      RANGE,
    }

    private class MultiPlayRecvData
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

      public void Begin(SceneBattle self)
      {
      }

      public void Update(SceneBattle self)
      {
      }

      public void End(SceneBattle self)
      {
      }
    }

    private class MultiPlayerUnit
    {
      private EUnitDirection mDir = EUnitDirection.NegativeX;
      private List<SceneBattle.MultiPlayInput> mRecv = new List<SceneBattle.MultiPlayInput>();
      private Unit mUnit;
      private int mGridX;
      private int mGridY;
      private Vector3 mTargetPos3D;
      private Quaternion mTargetRot;
      private SceneBattle.MultiPlayerUnit.EGridSnap mGridSnap;
      private bool mIsRunning;
      private float mStopTime;
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
        this.mTargetPos3D = self.CalcGridCenter(this.mUnit.x, this.mUnit.y);
        TacticsUnitController unitController = self.FindUnitController(this.mUnit);
        self.MultiPlayLog("[PUN]Begin MultiPlayer unitID: " + (object) this.UnitID + " name:" + this.mUnit.UnitName + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y + " dir:" + (object) this.mUnit.Direction + " sx:" + (object) this.mUnit.startX + " sy:" + (object) this.mUnit.startY);
        if (Object.op_Equality((Object) unitController, (Object) null) || this.mUnit.IsDead)
          return;
        this.mTargetRot = ((Component) unitController).get_transform().get_rotation();
        unitController.AutoUpdateRotation = false;
        this.mGridSnap = SceneBattle.MultiPlayerUnit.EGridSnap.DONE;
        this.mIsRunning = false;
        this.mStopTime = 0.0f;
        this.mGridX = this.mUnit.x;
        this.mGridY = this.mUnit.y;
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
        if (Object.op_Inequality((Object) unitController, (Object) null))
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
        if (Object.op_Equality((Object) unitController, (Object) null) || this.mUnit.IsDead || !self.IsInState<SceneBattle.State_MapCommandMultiPlay>() && !self.IsInState<SceneBattle.State_MapCommandVersus>() && !self.IsInState<SceneBattle.State_SelectTargetV2>())
          return;
        unitController.AutoUpdateRotation = false;
        if (this.mGridSnap == SceneBattle.MultiPlayerUnit.EGridSnap.ACTIVE)
        {
          if (!unitController.isIdle)
            return;
          this.mGridSnap = SceneBattle.MultiPlayerUnit.EGridSnap.DONE;
        }
        if (unitController.IsPlayingFieldAction)
          return;
        if (this.mRecv.Count <= 0)
        {
          if (this.Owner != null && this.IsMoveCompleted(self, this.mGridX, this.mGridY, unitController))
          {
            MyPhoton.MyPlayer myPlayer = PunMonoSingleton<MyPhoton>.Instance.GetRoomPlayerList().Find((Predicate<MyPhoton.MyPlayer>) (p => p.playerID == this.Owner.PlayerID));
            if (myPlayer == null || !myPlayer.start && this.Owner.Disconnected)
            {
              ((Component) unitController).get_transform().set_position(this.mTargetPos3D = self.CalcGridCenter(this.mUnit.x, this.mUnit.y));
              unitController.CancelAction();
              ((Component) unitController).get_transform().set_rotation(this.mTargetRot = this.mUnit.Direction.ToRotation());
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
          if (this.mTurnCmdNum < 0)
          {
            this.mTurnCmdNum = 0;
            int t = this.mRecv[0].t;
            if (t > this.mCurrentTurn)
              DebugUtility.LogError("[PUN] illegal send turn:" + (object) t + " <> " + (object) this.mCurrentTurn);
            else if (t < this.mCurrentTurn)
              DebugUtility.LogWarning("[PUN] skip send turn:" + (object) t + " <> " + (object) this.mCurrentTurn);
            while (this.mTurnCmdNum < this.mRecv.Count && this.mRecv[this.mTurnCmdNum].t == t)
              ++this.mTurnCmdNum;
            self.MultiPlayLog("[PUN] start send turn:" + (object) t + "/" + (object) this.mCurrentTurn + " cmdNum:" + (object) this.mTurnCmdNum + " recvTurnNum:" + (object) this.mRecvTurnNum + " recvCount:" + (object) this.mRecv.Count);
          }
          int num1 = Math.Min(this.mTurnCmdNum, (int) ((double) this.mTurnSec * (double) this.mTurnCmdNum / (double) self.SEND_TURN_SEC));
          while (this.mTurnCmdDoneNum < num1)
          {
            int num2 = 0;
            SceneBattle.MultiPlayInput multiPlayInput = this.mRecv[0];
            if (multiPlayInput.c != 0)
            {
              if (multiPlayInput.c == 3)
              {
                this.mGridX = multiPlayInput.gx;
                this.mGridY = multiPlayInput.gy;
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
                  this.mGridX = this.mUnit.startX;
                  this.mGridY = this.mUnit.startY;
                  this.mDir = this.mUnit.startDir;
                  self.ResetMultiPlayerTransform(this.mUnit);
                  this.mTargetPos3D = ((Component) unitController).get_transform().get_position();
                  this.mTargetRot = ((Component) unitController).get_transform().get_rotation();
                  this.mGridSnap = SceneBattle.MultiPlayerUnit.EGridSnap.DONE;
                  self.MultiPlayLog("[PUN] recv MOVE_CANCEL (" + (object) this.mGridX + "," + (object) this.mGridY + ")" + (object) this.mDir);
                }
                else if (multiPlayInput.c == 2)
                {
                  this.mTargetPos3D = this.GetPosition3D(self, multiPlayInput.x, multiPlayInput.z);
                  this.mTargetRot = Quaternion.AngleAxis(multiPlayInput.r, Vector3.get_up());
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
                    if (skillData != null && !this.mUnit.CheckEnableUseSkill(skillData, true))
                      self.SendCheat(SceneBattle.CHEAT_TYPE.MP, this.Owner.PlayerIndex);
                    ItemData inventoryByItemId = MonoSingleton<GameManager>.Instance.Player.FindInventoryByItemID(multiPlayInput.i);
                    int gx = multiPlayInput.gx;
                    int gy = multiPlayInput.gy;
                    bool unitLockTarget = multiPlayInput.ul != 0;
                    if (!this.IsMoveCompleted(self, this.mUnit.x, this.mUnit.y, unitController))
                    {
                      self.MultiPlayLog("[PUN]waiting move completed for ENTRY_BATTLE... SNAP:" + (object) this.mGridSnap + " RUN:" + (object) this.mIsRunning + " ux:" + (object) this.mUnit.x + " uy:" + (object) this.mUnit.y);
                      break;
                    }
                    if (skillData != null && !self.Battle.CreateSelectGridMap(this.mUnit, gx, gy, skillData).get(gx, gy))
                      self.SendCheat(SceneBattle.CHEAT_TYPE.RANGE, this.Owner.PlayerIndex);
                    self.MultiPlayLog("[PUN]MultiPlayerUnit ENTRY_BATTLE");
                    self.HideAllHPGauges();
                    self.HideAllUnitOwnerIndex();
                    self.Battle.EntryBattleMultiPlay(d, this.mUnit, enemy, skillData, inventoryByItemId, gx, gy, unitLockTarget);
                    num2 = 1;
                    this.mDir = this.mUnit.Direction;
                    this.mTargetRot = this.mDir.ToRotation();
                    this.mGridX = this.mUnit.x;
                    this.mGridY = this.mUnit.y;
                    this.mTargetPos3D = self.CalcGridCenter(this.mGridX, this.mGridY);
                  }
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
                  self.Battle.ExecuteEventTriggerOnGrid(this.mUnit, EEventTrigger.ExecuteOnGrid, false);
                  num2 = 1;
                }
                else if (multiPlayInput.c == 8)
                {
                  if (this.mGridX != multiPlayInput.gx || this.mGridY != multiPlayInput.gy)
                  {
                    DebugUtility.LogWarning("UNIT_END move pos not match gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tx:" + (object) multiPlayInput.gx + " ty:" + (object) multiPlayInput.gy);
                    this.mGridX = multiPlayInput.gx;
                    this.mGridY = multiPlayInput.gy;
                    this.mTargetPos3D = self.CalcGridCenter(this.mGridX, this.mGridY);
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
                  if (this.mGridX != multiPlayInput.gx || this.mGridY != multiPlayInput.gy)
                  {
                    DebugUtility.LogWarning("UNIT_TIME_LIMIT move pos not match gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tx:" + (object) multiPlayInput.gx + " ty:" + (object) multiPlayInput.gy);
                    this.mGridX = multiPlayInput.gx;
                    this.mGridY = multiPlayInput.gy;
                    this.mTargetPos3D = self.CalcGridCenter(this.mGridX, this.mGridY);
                    break;
                  }
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
            this.mRecv.RemoveAt(0);
            ++this.mTurnCmdDoneNum;
            switch (num2)
            {
              case 1:
                unitController.AutoUpdateRotation = true;
                self.GotoState<SceneBattle.State_WaitForLog>();
                goto label_71;
              case 2:
                unitController.AutoUpdateRotation = true;
                self.Battle.EntryBattleMultiPlayTimeUp = true;
                self.GotoMapCommand();
                goto label_71;
              default:
                continue;
            }
          }
label_71:
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
        int num3 = 2;
        Vector3 vector3 = Vector3.op_Subtraction(this.mTargetPos3D, ((Component) unitController).get_transform().get_position());
        vector3.y = (__Null) 0.0;
        // ISSUE: explicit reference operation
        float num4 = ((Vector3) @vector3).get_magnitude();
        // ISSUE: explicit reference operation
        float num5 = (float) (((Component) unitController).get_transform().get_eulerAngles().y - ((Quaternion) @this.mTargetRot).get_eulerAngles().y);
        while ((double) num5 < -180.0)
          num5 += 360f;
        while ((double) num5 > 180.0)
          num5 -= 360f;
        if ((double) num4 >= 0.00999999977648258)
        {
          this.mGridSnap = SceneBattle.MultiPlayerUnit.EGridSnap.NOP;
        }
        else
        {
          ((Component) unitController).get_transform().set_position(this.mTargetPos3D);
          num4 = 0.0f;
          --num3;
        }
        if ((double) Math.Abs(num5) < 1.0)
        {
          ((Component) unitController).get_transform().set_rotation(this.mTargetRot);
          --num3;
        }
        if (num3 <= 0)
        {
          this.mStopTime += Time.get_deltaTime();
          this.mStopTime = Math.Min(this.mStopTime, 1f);
        }
        else
        {
          Vector3 velocity = Vector3.op_Subtraction(this.mTargetPos3D, ((Component) unitController).get_transform().get_position());
          if (unitController.TriggerFieldAction(velocity, false))
          {
            this.mIsRunning = false;
            this.mTargetPos3D = this.GetPosition3D(self, (float) unitController.FieldActionPoint.x, (float) unitController.FieldActionPoint.y);
            self.MultiPlayLog("start jump gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tgt:(" + (object) (float) this.mTargetPos3D.x + "," + (object) (float) this.mTargetPos3D.y + "," + (object) (float) this.mTargetPos3D.z + ") pos:(" + (object) (float) ((Component) unitController).get_transform().get_position().x + "," + (object) (float) ((Component) unitController).get_transform().get_position().y + "," + (object) (float) ((Component) unitController).get_transform().get_position().z + ")");
          }
          else
          {
            Transform transform = ((Component) unitController).get_transform();
            transform.set_position(Vector3.op_Addition(transform.get_position(), Vector3.op_Multiply(Vector3.op_Multiply(velocity, GameSettings.Instance.Quest.MapRunSpeedMax), Time.get_deltaTime())));
            if ((double) num4 > 0.0 && !this.mIsRunning)
            {
              unitController.StartRunning();
              this.mIsRunning = true;
            }
          }
          ((Component) unitController).get_transform().set_rotation(Quaternion.Slerp(((Component) unitController).get_transform().get_rotation(), this.mTargetRot, Math.Min(1f, Time.get_deltaTime() * 10f)));
          this.mStopTime = 0.0f;
        }
        if ((double) this.mStopTime < 0.5)
          return;
        if (this.mIsRunning)
        {
          unitController.StopRunning();
          this.mIsRunning = false;
        }
        if (this.mGridSnap != SceneBattle.MultiPlayerUnit.EGridSnap.NOP)
        {
          self.MultiPlayLog("skip snap gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tgt:(" + (object) (float) this.mTargetPos3D.x + "," + (object) (float) this.mTargetPos3D.y + "," + (object) (float) this.mTargetPos3D.z + ") pos:(" + (object) (float) ((Component) unitController).get_transform().get_position().x + "," + (object) (float) ((Component) unitController).get_transform().get_position().y + "," + (object) (float) ((Component) unitController).get_transform().get_position().z + ")");
        }
        else
        {
          this.mTargetPos3D = self.CalcGridCenter(this.mGridX, this.mGridY);
          unitController.StepTo(this.mTargetPos3D);
          this.mGridSnap = SceneBattle.MultiPlayerUnit.EGridSnap.ACTIVE;
          self.MultiPlayLog("start snap gx:" + (object) this.mGridX + " gy:" + (object) this.mGridY + " tgt:(" + (object) (float) this.mTargetPos3D.x + "," + (object) (float) this.mTargetPos3D.y + "," + (object) (float) this.mTargetPos3D.z + ") pos:(" + (object) (float) ((Component) unitController).get_transform().get_position().x + "," + (object) (float) ((Component) unitController).get_transform().get_position().y + "," + (object) (float) ((Component) unitController).get_transform().get_position().z + ")");
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

    public enum ENotifyDisconnectedPlayerType
    {
      NORMAL,
      OWNER,
      OWNER_AND_I_AM_OWNER,
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
        string multiPlayInputList = self.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.CONTINUE, !flag ? 0 : 1, sendList);
        instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
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
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SceneBattle.State_MultiPlayContinueBase.\u003CUpdate\u003Ec__AnonStorey1DE updateCAnonStorey1De = new SceneBattle.State_MultiPlayContinueBase.\u003CUpdate\u003Ec__AnonStorey1DE();
        // ISSUE: reference to a compiler-generated field
        updateCAnonStorey1De.\u003C\u003Ef__this = this;
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        // ISSUE: reference to a compiler-generated field
        updateCAnonStorey1De.me = instance.GetMyPlayer();
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        List<JSON_MyPhotonPlayerParam> myPlayersStarted = instance.GetMyPlayersStarted();
        // ISSUE: reference to a compiler-generated field
        if (updateCAnonStorey1De.me == null || roomPlayerList == null || (myPlayersStarted == null || Object.op_Equality((Object) self.mBattleUI_MultiPlay, (Object) null)))
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
            // ISSUE: reference to a compiler-generated field
            this.OpenUI(this.mRoomOwnerPlayerID == updateCAnonStorey1De.me.playerID);
            this.mInitFlag = true;
          }
          while (self.mRecvContinue.Count > 0)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            SceneBattle.State_MultiPlayContinueBase.\u003CUpdate\u003Ec__AnonStorey1DD updateCAnonStorey1Dd = new SceneBattle.State_MultiPlayContinueBase.\u003CUpdate\u003Ec__AnonStorey1DD();
            // ISSUE: reference to a compiler-generated field
            updateCAnonStorey1Dd.data = self.mRecvContinue[0];
            // ISSUE: reference to a compiler-generated field
            if (updateCAnonStorey1Dd.data.b > self.UnitStartCountTotal)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated method
              DebugUtility.LogWarning("[PUN] new turn data. sq:" + (object) updateCAnonStorey1Dd.data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) updateCAnonStorey1Dd.data.h + " b:" + (object) updateCAnonStorey1Dd.data.b + "/" + (object) self.UnitStartCountTotal + " test:" + (object) self.mRecvBattle.FindIndex(new Predicate<SceneBattle.MultiPlayRecvData>(updateCAnonStorey1Dd.\u003C\u003Em__196)));
              break;
            }
            // ISSUE: reference to a compiler-generated field
            if (updateCAnonStorey1Dd.data.b < self.UnitStartCountTotal)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              DebugUtility.LogWarning("[PUN] old turn data. sq:" + (object) updateCAnonStorey1Dd.data.sq + " h:" + (object) (SceneBattle.EMultiPlayRecvDataHeader) updateCAnonStorey1Dd.data.h + " b:" + (object) updateCAnonStorey1Dd.data.b + "/" + (object) self.UnitStartCountTotal);
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              if (updateCAnonStorey1Dd.data.h == 4)
              {
                // ISSUE: reference to a compiler-generated method
                this.mReqPool.RemoveAll(new Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>(updateCAnonStorey1Dd.\u003C\u003Em__197));
                SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest = new SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest();
                // ISSUE: reference to a compiler-generated field
                playContinueRequest.playerID = updateCAnonStorey1Dd.data.pid;
                // ISSUE: reference to a compiler-generated field
                playContinueRequest.flag = updateCAnonStorey1Dd.data.uid != 0;
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                if (updateCAnonStorey1Dd.data.i != null && updateCAnonStorey1Dd.data.i.Length > 0)
                {
                  // ISSUE: reference to a compiler-generated field
                  long.TryParse(updateCAnonStorey1Dd.data.i[0], out playContinueRequest.btlid);
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                if (updateCAnonStorey1Dd.data.s != null && updateCAnonStorey1Dd.data.s.Length > 0)
                {
                  // ISSUE: reference to a compiler-generated field
                  int.TryParse(updateCAnonStorey1Dd.data.s[0], out playContinueRequest.seed);
                }
                // ISSUE: reference to a compiler-generated field
                if (updateCAnonStorey1Dd.data.u != null)
                {
                  // ISSUE: reference to a compiler-generated field
                  for (int index = 0; index < updateCAnonStorey1Dd.data.u.Length; ++index)
                  {
                    // ISSUE: reference to a compiler-generated field
                    if (updateCAnonStorey1Dd.data.u[index] >= 0)
                    {
                      // ISSUE: reference to a compiler-generated field
                      playContinueRequest.units.Add(updateCAnonStorey1Dd.data.u[index]);
                    }
                  }
                }
                this.mReqPool.Add(playContinueRequest);
              }
            }
            self.mRecvContinue.RemoveAt(0);
          }
          // ISSUE: reference to a compiler-generated method
          if (this.mReqPool.Count == 0 && roomPlayerList.Find(new Predicate<MyPhoton.MyPlayer>(updateCAnonStorey1De.\u003C\u003Em__198)) == null)
          {
            this.mInitFlag = false;
            // ISSUE: reference to a compiler-generated field
            this.CloseUI(this.mRoomOwnerPlayerID == updateCAnonStorey1De.me.playerID, false);
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest1 = this.mReqPool.Find(new Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>(updateCAnonStorey1De.\u003C\u003Em__199));
            if (playContinueRequest1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              if (updateCAnonStorey1De.me.playerID == this.mRoomOwnerPlayerID)
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
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: reference to a compiler-generated method
                SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest playContinueRequest2 = this.mReqPool.Find(new Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>(new SceneBattle.State_MultiPlayContinueBase.\u003CUpdate\u003Ec__AnonStorey1DF() { roomOwnerPlayerID = instance.GetOldestPlayer() }.\u003C\u003Em__19A));
                if (playContinueRequest2 == null)
                  return;
                this.BtlID = playContinueRequest2.btlid;
                this.Seed = playContinueRequest2.seed;
                playContinueRequest1 = this.SendMultiPlayContinueRequest(self, playContinueRequest2.flag, playContinueRequest2.units, this.Seed, this.BtlID);
              }
            }
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            SceneBattle.State_MultiPlayContinueBase.\u003CUpdate\u003Ec__AnonStorey1E0 updateCAnonStorey1E0 = new SceneBattle.State_MultiPlayContinueBase.\u003CUpdate\u003Ec__AnonStorey1E0();
            using (List<MyPhoton.MyPlayer>.Enumerator enumerator = roomPlayerList.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                // ISSUE: reference to a compiler-generated field
                updateCAnonStorey1E0.player = enumerator.Current;
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated method
                if (updateCAnonStorey1E0.player.start && this.mReqPool.Find(new Predicate<SceneBattle.State_MultiPlayContinueBase.MultiPlayContinueRequest>(updateCAnonStorey1E0.\u003C\u003Em__19B)) == null)
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
                  // ISSUE: reference to a compiler-generated field
                  this.CloseUI(this.mRoomOwnerPlayerID == updateCAnonStorey1De.me.playerID, false);
                  return;
                }
              }
            }
            // ISSUE: reference to a compiler-generated field
            this.CloseUI(this.mRoomOwnerPlayerID == updateCAnonStorey1De.me.playerID, true);
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
        else if (!this.mOpenMenu && this.mOpenMenuReq && Object.op_Equality((Object) ((Component) self.mBattleUI).get_gameObject().get_transform().Find("quest_lose(Clone)"), (Object) null))
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
        this.self.Battle.ContinueStart(this.self.Battle.BtlID, this.Seed);
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
        self.mBattleUI_MultiPlay.OnOtherPlayerSyncEnd();
        if (self.ResumeSuccess)
        {
          self.Battle.StartOrder(false, false);
          self.ResumeSuccess = false;
        }
        if (self.IsExistResume)
        {
          self.Battle.SetResumeWait();
          self.GotoState<SceneBattle.State_SyncResume>();
        }
        else
          self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self)
      {
      }

      private void SendSync()
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        string empty = string.Empty;
        if (this.self.Battle.ResumeState != BattleCore.RESUME_STATE.NONE)
        {
          string multiPlayInputList = this.self.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.SYNC_RESUME, 0, (List<SceneBattle.MultiPlayInput>) null);
          this.mSend = instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
          this.mSendResume = true;
        }
        else
        {
          string multiPlayInputList = this.self.CreateMultiPlayInputList(SceneBattle.EMultiPlayRecvDataHeader.SYNC, 0, (List<SceneBattle.MultiPlayInput>) null);
          this.mSend = instance.SendRoomMessage(true, multiPlayInputList, MyPhoton.SEND_TYPE.Normal);
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
        if (Object.op_Inequality((Object) self.mBattleUI.CommandWindow, (Object) null))
          self.mBattleUI.CommandWindow.OnCommandSelect = new UnitCommands.CommandEvent(this.OnCommandSelect);
        self.ShowAllHPGauges();
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (Object.op_Inequality((Object) unitController, (Object) null))
          unitController.HideCursor(false);
        if (Object.op_Inequality((Object) self.mBattleUI.TargetMain, (Object) null))
          self.mBattleUI.TargetMain.Close();
        if (Object.op_Inequality((Object) self.mBattleUI.TargetSub, (Object) null))
          self.mBattleUI.TargetSub.Close();
        if (Object.op_Inequality((Object) self.mBattleUI.TargetObjectSub, (Object) null))
          self.mBattleUI.TargetObjectSub.Close();
        self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        self.VersusMapView = false;
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
        if (!Object.op_Inequality((Object) self.mBattleUI.CommandWindow, (Object) null))
          return;
        self.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
      }

      private void SyncCameraPosition(SceneBattle self)
      {
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (Object.op_Equality((Object) unitController, (Object) null))
          return;
        GameSettings instance = GameSettings.Instance;
        Transform transform = ((Component) Camera.get_main()).get_transform();
        transform.set_position(Vector3.op_Addition(((Component) unitController).get_transform().get_position(), ((Component) instance.Quest.MoveCamera).get_transform().get_position()));
        transform.set_rotation(((Component) instance.Quest.MoveCamera).get_transform().get_rotation());
        self.SetCameraTarget((Component) ((Component) unitController).get_transform());
        self.mUpdateCameraPosition = true;
      }

      private void OnCommandSelect(UnitCommands.CommandTypes command, object ability)
      {
        this.self.mBattleUI.OnCommandSelect();
        if (command != UnitCommands.CommandTypes.Map)
          return;
        this.self.VersusMapView = true;
        this.self.GotoMapViewMode();
      }
    }

    private class State_RetireComfirm : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
      }

      public override void Update(SceneBattle self)
      {
        MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
        bool flag = false;
        if (Object.op_Inequality((Object) instance, (Object) null))
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          SceneBattle.State_RetireComfirm.\u003CUpdate\u003Ec__AnonStorey1E1 updateCAnonStorey1E1 = new SceneBattle.State_RetireComfirm.\u003CUpdate\u003Ec__AnonStorey1E1();
          MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
          if (currentRoom != null)
            flag = currentRoom.playerCount == 1;
          // ISSUE: reference to a compiler-generated field
          updateCAnonStorey1E1.me = instance.GetMyPlayer();
          List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
          // ISSUE: reference to a compiler-generated field
          if (roomPlayerList != null && updateCAnonStorey1E1.me != null)
          {
            // ISSUE: reference to a compiler-generated method
            MyPhoton.MyPlayer myPlayer = roomPlayerList.Find(new Predicate<MyPhoton.MyPlayer>(updateCAnonStorey1E1.\u003C\u003Em__19C));
            if (myPlayer != null)
              flag = !myPlayer.start;
          }
        }
        if (!self.IsRetireComfirm && !self.Battle.IsVSForceWin && !flag)
          return;
        self.mBattleUI_MultiPlay.OnForceWinClose();
        self.GotoState<SceneBattle.State_ExitQuest>();
      }

      public override void End(SceneBattle self)
      {
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
        self.Battle.StartOrder(false, false);
        self.GotoState<SceneBattle.State_WaitForLog>();
      }

      public override void End(SceneBattle self)
      {
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
          if (self.Battle.IsMultiPlay)
          {
            if (!self.Battle.FinishLoad)
            {
              MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
              self.Battle.FinishLoad = self.SendFinishLoad();
              if (!instance.IsConnected())
                self.Battle.FinishLoad = true;
            }
            if (!self.Battle.SyncStart)
              return;
          }
          this.mReady = true;
          GameUtility.SetDefaultSleepSetting();
          ProgressWindow.Close();
          GameUtility.FadeOut(1f);
        }
        if (GameUtility.IsScreenFading)
          return;
        self.StartDownloadNextQuestAsync();
        if (Object.op_Inequality((Object) self.mEventScript, (Object) null) && Object.op_Inequality((Object) (self.mEventSequence = self.mEventScript.OnPostMapLoad()), (Object) null))
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
        if (Object.op_Equality((Object) self.mEventScript, (Object) null))
        {
          this.Finish();
        }
        else
        {
          this.mSequence = self.mEventScript.OnStart(0);
          if (Object.op_Equality((Object) this.mSequence, (Object) null))
            this.Finish();
          else
            self.mUpdateCameraPosition = false;
        }
      }

      public override void Update(SceneBattle self)
      {
        if (this.mSequence.IsPlaying)
          return;
        Object.Destroy((Object) this.mSequence);
        self.ResetCameraTarget();
        this.Finish();
      }

      private void Finish()
      {
        if (this.self.IsPlayingArenaQuest)
          this.self.mBattleUI.OnQuestStart_Arena();
        else if (string.IsNullOrEmpty(this.self.mCurrentQuest.cond))
          this.self.mBattleUI.OnQuestStart_Short();
        else
          this.self.mBattleUI.OnQuestStart();
        if (Object.op_Inequality((Object) this.self.mBattleUI_MultiPlay, (Object) null))
          this.self.mBattleUI_MultiPlay.OnMapStart();
        this.self.GotoState_WaitSignal<SceneBattle.State_MapStartV2>();
      }
    }

    private class State_MapStartV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.GotoState<SceneBattle.State_WaitForLog>();
      }
    }

    private class State_PreUnitStart : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.RemoveLog();
        self.Battle.UnitStart();
        BattleMap currentMap = self.Battle.CurrentMap;
        if (currentMap != null)
        {
          int remainingActionCount1 = self.Battle.GetRemainingActionCount(currentMap.WinMonitorCondition);
          int remainingActionCount2 = self.Battle.GetRemainingActionCount(currentMap.LoseMonitorCondition);
          self.RemainingActionCountSet((uint) remainingActionCount1, (uint) remainingActionCount2);
        }
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (Object.op_Inequality((Object) unitController, (Object) null))
        {
          ++unitController.TurnCount;
          if (self.Battle.IsMultiVersus)
            unitController.PlayVersusCursor(true);
        }
        self.SetUnitUiHeight(self.mBattle.CurrentUnit);
      }

      public override void Update(SceneBattle self)
      {
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
        {
          if (Object.op_Inequality((Object) self.mTacticsUnits[index], (Object) null) && self.mTacticsUnits[index].IsHPGaugeChanging)
            return;
        }
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
        {
          if (Object.op_Inequality((Object) self.mTacticsUnits[index], (Object) null))
            self.mTacticsUnits[index].ResetHPGauge();
        }
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (Object.op_Inequality((Object) self.mEventScript, (Object) null))
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
        if (self.mTutorialTriggers != null && Object.op_Inequality((Object) unitController, (Object) null))
        {
          for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
            self.mTutorialTriggers[index].OnUnitStart(self.mBattle.CurrentUnit, unitController.TurnCount);
        }
        if (Object.op_Inequality((Object) unitController, (Object) null))
        {
          self.SetPrioritizedUnit(unitController);
          Unit currentUnit = self.Battle.CurrentUnit;
          if (currentUnit != null && !currentUnit.IsDead)
          {
            self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
            self.InterpCameraTarget((Component) unitController);
            if (currentUnit.UniqueName == "logi" && unitController.TurnCount == 2)
              AnalyticsManager.TrackTutorialCustomEvent("funnel.tutorial.guide_battle_freeplay", new Dictionary<string, object>()
              {
                {
                  "step_number",
                  (object) "7.6"
                }
              });
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
          if (Object.op_Inequality((Object) self.mBattleUI.CommandWindow, (Object) null))
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
          }
        }
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
        {
          if (Object.op_Inequality((Object) self.mTacticsUnits[index], (Object) null))
            self.mTacticsUnits[index].UpdateBadStatus();
        }
        self.GotoState<SceneBattle.State_WaitForLog>();
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
        if (Object.op_Inequality((Object) self.mEventSequence, (Object) null))
        {
          if (Object.op_Inequality((Object) self.mBattleUI, (Object) null))
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
        if (!Object.op_Inequality((Object) self.mBattleUI, (Object) null))
          return;
        self.mBattleUI.Show();
      }

      public override void Update(SceneBattle self)
      {
        if (Object.op_Inequality((Object) self.mEventSequence, (Object) null) && self.IsCameraMoving || !Object.op_Equality((Object) self.mEventSequence, (Object) null) && self.mEventSequence.IsPlaying)
          return;
        for (int index = 0; index < self.mTacticsUnits.Count; ++index)
          self.mTacticsUnits[index].AutoUpdateRotation = true;
        self.ResetCameraTarget();
        self.mUpdateCameraPosition = true;
        if (Object.op_Inequality((Object) self.mEventSequence, (Object) null))
        {
          Object.DestroyImmediate((Object) ((Component) self.mEventSequence).get_gameObject());
          self.mEventSequence = (EventScript.Sequence) null;
        }
        MonoSingleton<GameManager>.Instance.EnableAnimationFrameSkipping = true;
        self.GotoState<T>();
      }
    }

    private class State_PostUnitStart : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.GotoMapCommand();
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
        self.InterpCameraOffset(GameSettings.Instance.Quest.MoveCamera);
        self.InterpCameraTarget((Component) self.FindUnitController(this.mCurrentUnit));
        self.mTouchController.IgnoreCurrentTouch();
        this.mCurrentController = self.FindUnitController(this.mCurrentUnit);
        this.mCurrentController.ResetHPGauge();
        if (Object.op_Inequality((Object) self.mBattleUI.CommandWindow, (Object) null))
          self.mBattleUI.CommandWindow.OnCommandSelect = new UnitCommands.CommandEvent(this.OnCommandSelect);
        self.mAllowCameraRotation = true;
        self.mAllowCameraTranslation = false;
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
        self.mAllowCameraRotation = false;
        self.mAllowCameraTranslation = false;
        if (!Object.op_Inequality((Object) self.mBattleUI.CommandWindow, (Object) null))
          return;
        self.mBattleUI.CommandWindow.OnCommandSelect = (UnitCommands.CommandEvent) null;
      }

      public override void Update(SceneBattle self)
      {
        if (self.mMoveInput != null)
        {
          self.mMoveInput.Update();
          IntVector2 intVector2 = self.CalcCoord(this.mCurrentController.CenterPosition);
          if (!this.mMoved && (intVector2.x != this.mStartX || intVector2.y != this.mStartY))
          {
            TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
            if (Object.op_Inequality((Object) unitController, (Object) null) && self.mTutorialTriggers != null)
            {
              for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
                self.mTutorialTriggers[index].OnUnitMoveStart(this.mCurrentUnit, unitController.TurnCount);
            }
            this.mMoved = true;
            if (self.Battle.IsMultiPlay)
              self.ExtentionMultiInputTime(true);
          }
          if (self.mNumHotTargets != this.mHotTargets)
          {
            TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
            if (Object.op_Inequality((Object) unitController, (Object) null) && self.mTutorialTriggers != null && self.mNumHotTargets > 0)
            {
              for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
                self.mTutorialTriggers[index].OnHotTargetsChange(this.mCurrentUnit, unitController.TurnCount);
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
              if (Object.op_Inequality((Object) unitController, (Object) null))
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
        IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(currentUnit).CenterPosition);
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
        if (command == UnitCommands.CommandTypes.Gimmick)
        {
          Unit currentUnit = this.self.mBattle.CurrentUnit;
          IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(currentUnit).CenterPosition);
          Grid current = this.self.mBattle.CurrentMap[intVector2.x, intVector2.y];
          if (!this.self.mBattle.CheckGridEventTrigger(currentUnit, current, EEventTrigger.ExecuteOnGrid, false))
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
    }

    private class State_SelectItemV2 : State<SceneBattle>
    {
      public override void Begin(SceneBattle self)
      {
        self.InterpCameraTargetToCurrent();
        if (Object.op_Inequality((Object) self.mBattleUI.ItemWindow, (Object) null))
          self.mBattleUI.ItemWindow.OnSelectItem = new BattleInventory.SelectEvent(this.OnSelectItem);
        self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        self.StepToNear(self.Battle.CurrentUnit);
      }

      public override void End(SceneBattle self)
      {
        if (Object.op_Inequality((Object) self.mBattleUI.ItemWindow, (Object) null))
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
              Unit unitAtGrid = self.Battle.FindUnitAtGrid(self.Battle.CurrentMap[x, y]);
              if (unitAtGrid != null && unitAtGrid != currentUnit && (unitAtGrid.IsThrow && unitAtGrid.IsNormalSize) && !unitAtGrid.IsJump)
              {
                TacticsUnitController unitController2 = self.FindUnitController(unitAtGrid);
                if (Object.op_Implicit((Object) unitController2))
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
        self.mAllowCameraRotation = flag;
        self.mAllowCameraTranslation = flag;
        if (self.mTargetSelectorParam.DefaultThrowTarget != null)
        {
          TacticsUnitController unitController2 = self.FindUnitController(self.mTargetSelectorParam.DefaultThrowTarget);
          if (Object.op_Inequality((Object) unitController2, (Object) null))
            this.OnFocus(unitController2);
          self.InterpCameraTarget((Component) unitController2);
          this.SetYesButtonEnable(true);
        }
        self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
        if (Object.op_Implicit((Object) self.mBattleUI.TargetSub))
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
        if (Object.op_Implicit((Object) self.mBattleUI.TargetSub))
        {
          self.mBattleUI.TargetSub.DeactivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
          self.mBattleUI.TargetSub.DeactivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
        }
        this.SetYesButtonEnable(true);
        self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
        self.HideUnitMarkers(false);
        self.mAllowCameraRotation = false;
        self.mAllowCameraTranslation = false;
        self.mOnUnitFocus = (SceneBattle.UnitFocusEvent) null;
        self.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
        self.mOnGridClick = (SceneBattle.GridClickEvent) null;
        self.HideGrid();
        self.SetUnitUiHeight(self.mBattle.CurrentUnit);
      }

      private void SetYesButtonEnable(bool enable)
      {
        Selectable component = (Selectable) this.self.mBattleUI.CommandWindow.OKButton.GetComponent<Selectable>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.set_interactable(enable);
      }

      private void HideTargetWindows()
      {
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetMain, (Object) null))
          this.self.mBattleUI.TargetMain.Close();
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetSub, (Object) null))
          this.self.mBattleUI.TargetSub.Close();
        if (!Object.op_Inequality((Object) this.self.mBattleUI.TargetObjectSub, (Object) null))
          return;
        this.self.mBattleUI.TargetObjectSub.Close();
      }

      private void OnYesNoSelect(bool yes)
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
        }
        this.self.mBattleUI.OnThrowTargetSelectEnd();
        if (yes)
        {
          this.self.mTargetSelectorParam.ThrowTarget = this.mSelectedTarget.Unit;
          this.HideTargetWindows();
          this.self.GotoState_WaitSignal<SceneBattle.State_PreSelectTargetV2>();
        }
        else
        {
          this.self.mTargetSelectorParam.OnCancel();
          this.HideTargetWindows();
        }
      }

      private bool IsGridSelectable(int x, int y)
      {
        if (this.mTargetGrids == null)
          return false;
        bool flag = this.mTargetGrids.get(x, y);
        if (flag)
        {
          Unit unitAtGrid = this.self.Battle.FindUnitAtGrid(this.self.Battle.CurrentMap[x, y]);
          if (unitAtGrid != null && (unitAtGrid == this.self.mBattle.CurrentUnit || !unitAtGrid.IsThrow || (!unitAtGrid.IsNormalSize || unitAtGrid.IsJump)))
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
        if (Object.op_Inequality((Object) this.self.mFocusedUnit, (Object) null) && this.self.mFocusedUnit.Unit != null && this.self.mFocusedUnit.Unit == unit)
        {
          IntVector2 intVector2 = this.self.CalcCoord(this.self.mFocusedUnit.CenterPosition);
          x = intVector2.x;
          y = intVector2.y;
        }
        return this.IsGridSelectable(x, y);
      }

      private void OnFocus(TacticsUnitController controller)
      {
        if (!Object.op_Implicit((Object) controller) || controller.Unit.IsGimmick)
          return;
        if (Object.op_Inequality((Object) this.mSelectedTarget, (Object) null))
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
        unitController.SetHPChangeYosou((int) currentUnit.CurrentStatus.param.hp - YosokuDamageHp);
        this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp, YosokuDamageHp, 0);
        this.self.mBattleUI.TargetMain.UpdateHpGauge();
        this.self.mBattleUI.TargetSub.ResetHpGauge(this.self.mSelectedTarget.Side, (int) this.self.mSelectedTarget.CurrentStatus.param.hp, (int) this.self.mSelectedTarget.MaximumStatus.param.hp);
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          if (0 >= YosokuDamageHp || !Object.op_Equality((Object) this.self.mTacticsUnits[index], (Object) controller))
            this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
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
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetMain, (Object) null))
        {
          this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
          this.self.mBattleUI.TargetMain.Open();
        }
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetSub, (Object) null))
        {
          if (this.self.UIParam_TargetValid)
          {
            this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
            this.self.mBattleUI.TargetSub.Open();
          }
          else if (this.self.mSelectedTarget.UnitType == EUnitType.Unit || this.self.mSelectedTarget.UnitType == EUnitType.EventUnit)
          {
            this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
            this.self.mBattleUI.TargetSub.Open();
          }
          else
            this.self.mBattleUI.TargetSub.Close();
        }
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetObjectSub, (Object) null))
          this.self.mBattleUI.TargetObjectSub.Close();
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
        if (this.mTargets == null)
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
      private SceneBattle mScene;
      private TacticsUnitController mSelectedTarget;
      private GridMap<bool> mTargetGrids;
      private GridMap<bool> mTargetAreaGridMap;
      private IntVector2 mTargetPosition;
      private List<TacticsUnitController> mTargets;
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
        if (skill != null)
        {
          this.mSelectGrid = skill.SkillParam.IsAreaSkill();
          if (skill.IsTargetGridNoUnit)
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
              this.mTargets = new List<TacticsUnitController>(32);
              for (int index = 0; index < attackTargetsAi.Count; ++index)
              {
                TacticsUnitController unitController2 = self.FindUnitController(attackTargetsAi[index]);
                if (Object.op_Inequality((Object) unitController2, (Object) null) && this.mTargetGrids.get(attackTargetsAi[index].x, attackTargetsAi[index].y))
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
                unit = currentUnit;
                break;
            }
            self.mTargetSelectorParam.DefaultTarget = unit;
            flag2 = unit != null;
          }
          self.mTacticsSceneRoot.ShowGridLayer(1, grid, true);
          self.ShowCastSkill();
          if (self.mTargetSelectorParam.AllowTargetChange && !flag2)
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
        }
        if (self.mTargetSelectorParam.AllowTargetChange)
        {
          if (this.mSelectGrid)
          {
            self.mOnGridClick = new SceneBattle.GridClickEvent(this.OnClickGrid);
            this.mTargetPosition.x = Mathf.FloorToInt((float) self.mCameraTarget.x);
            this.mTargetPosition.y = Mathf.FloorToInt((float) self.mCameraTarget.y);
          }
          else
          {
            self.mOnUnitClick = new SceneBattle.UnitClickEvent(this.OnClickUnit);
            self.mOnUnitFocus = new SceneBattle.UnitFocusEvent(this.OnFocus);
          }
          self.mAllowCameraRotation = flag1;
          self.mAllowCameraTranslation = flag1;
        }
        else
        {
          self.mAllowCameraRotation = false;
          self.mAllowCameraTranslation = false;
        }
        if (self.mTargetSelectorParam.DefaultTarget != null)
        {
          TacticsUnitController unitController2 = self.FindUnitController(self.mTargetSelectorParam.DefaultTarget);
          if (Object.op_Inequality((Object) unitController2, (Object) null))
          {
            this.OnFocus(unitController2);
            IntVector2 intVector2_2 = self.CalcCoord(unitController2.CenterPosition);
            Grid current = self.Battle.CurrentMap[intVector2_2.x, intVector2_2.y];
            this.HilitArea(current.x, current.y);
          }
          self.InterpCameraTarget((Component) unitController2);
          bool enable = true;
          if (skill.IsTargetGridNoUnit)
            enable = false;
          this.SetYesButtonEnable(enable);
        }
        self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mBattleUI.CommandWindow.OnMapExitSelect += new UnitCommands.MapExitEvent(this.OnMapExitSelect);
        self.mTouchController.OnDragDelegate += new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate += new TouchController.DragEvent(this.OnDragEnd);
        if (Object.op_Implicit((Object) self.mBattleUI.TargetSub))
        {
          if (this.mTargets != null && this.mTargets.Count >= 2)
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
        if (Object.op_Implicit((Object) self.mBattleUI.TargetSub))
        {
          self.mBattleUI.TargetSub.DeactivateNextTargetArrow(new ButtonExt.ButtonClickEvent(this.OnNextTargetClick));
          self.mBattleUI.TargetSub.DeactivatePrevTargetArrow(new ButtonExt.ButtonClickEvent(this.OnPrevTargetClick));
        }
        if (Object.op_Inequality((Object) this.mGUIEvent, (Object) null))
        {
          Object.Destroy((Object) this.mGUIEvent);
          this.mGUIEvent = (GUIEventListener) null;
        }
        this.SetYesButtonEnable(true);
        self.mBattleUI.CommandWindow.OnYesNoSelect -= new UnitCommands.YesNoEvent(this.OnYesNoSelect);
        self.mBattleUI.CommandWindow.OnMapExitSelect -= new UnitCommands.MapExitEvent(this.OnMapExitSelect);
        self.mTouchController.OnDragDelegate -= new TouchController.DragEvent(this.OnDrag);
        self.mTouchController.OnDragEndDelegate -= new TouchController.DragEvent(this.OnDragEnd);
        self.HideUnitMarkers(false);
        self.mAllowCameraRotation = false;
        self.mAllowCameraTranslation = false;
        self.mOnUnitFocus = (SceneBattle.UnitFocusEvent) null;
        self.mOnUnitClick = (SceneBattle.UnitClickEvent) null;
        self.mOnGridClick = (SceneBattle.GridClickEvent) null;
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
        if (this.mTargets == null)
          return;
        this.mDragScroll = true;
      }

      private void SetYesButtonEnable(bool enable)
      {
        Selectable component = (Selectable) this.self.mBattleUI.CommandWindow.OKButton.GetComponent<Selectable>();
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.set_interactable(enable);
      }

      private void OnYesNoSelect(bool yes)
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
        }
        if (yes)
        {
          if (this.self.UIParam_TargetValid)
          {
            if (this.self.mTargetSelectorParam.Item != null)
            {
              this.self.mBattleUI.OnTargetSelectEnd();
              ((SceneBattle.SelectTargetPositionWithItem) this.self.mTargetSelectorParam.OnAccept)(this.mTargetPosition.x, this.mTargetPosition.y, this.self.mTargetSelectorParam.Item);
              this.HideTargetWindows();
            }
            else
            {
              Unit currentUnit = this.self.Battle.CurrentUnit;
              bool flag = false;
              if (this.self.mTargetSelectorParam.Skill.EffectType == SkillEffectTypes.Changing)
              {
                IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(currentUnit).CenterPosition);
                if (currentUnit.x == intVector2.x && currentUnit.y == intVector2.y)
                  flag = true;
              }
              if (this.NeedsTargetKakunin(currentUnit))
              {
                this.StartTargetKakunin();
              }
              else
              {
                this.self.mBattleUI.OnTargetSelectEnd();
                ((SceneBattle.SelectTargetPositionWithSkill) this.self.mTargetSelectorParam.OnAccept)(this.mTargetPosition.x, this.mTargetPosition.y, this.self.mTargetSelectorParam.Skill, false);
                this.HideTargetWindows();
              }
              if (flag)
              {
                this.self.mCurrentUnitStartX = currentUnit.x;
                this.self.mCurrentUnitStartY = currentUnit.y;
              }
              this.self.mSkillDirectionByKouka = this.self.GetSkillDirectionByTargetArea(this.self.Battle.CurrentUnit, this.mTargetAreaGridMap);
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
          this.HideTargetWindows();
        }
        this.self.mBattleUI.OnVersusEnd();
      }

      private void HideTargetWindows()
      {
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetMain, (Object) null))
          this.self.mBattleUI.TargetMain.Close();
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetSub, (Object) null))
          this.self.mBattleUI.TargetSub.Close();
        if (!Object.op_Inequality((Object) this.self.mBattleUI.TargetObjectSub, (Object) null))
          return;
        this.self.mBattleUI.TargetObjectSub.Close();
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
        this.HideTargetWindows();
      }

      private void OnMapExitSelect()
      {
        for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
        {
          this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
        }
        this.self.mBattleUI.OnTargetSelectEnd();
        this.self.mTargetSelectorParam.OnCancel();
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetMain, (Object) null))
          this.self.mBattleUI.TargetMain.Close();
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetSub, (Object) null))
          this.self.mBattleUI.TargetSub.Close();
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetObjectSub, (Object) null))
          this.self.mBattleUI.TargetObjectSub.Close();
        this.self.InterpCameraDistance(GameSettings.Instance.GameCamera_DefaultDistance);
        this.self.mBattleUI.OnMapViewEnd();
      }

      private void HilitArea(int x, int y)
      {
        if (this.self.mTargetSelectorParam.Skill == null)
          return;
        IntVector2 intVector2 = this.self.CalcCoord(this.self.FindUnitController(this.self.mBattle.CurrentUnit).CenterPosition);
        this.mTargetAreaGridMap = this.self.mBattle.CreateScopeGridMap(this.self.mBattle.CurrentUnit, intVector2.x, intVector2.y, x, y, this.self.mTargetSelectorParam.Skill);
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
          if (Object.op_Inequality((Object) unitController2, (Object) null))
          {
            this.self.mFocusedUnit = unitController2;
            this.OnFocus(unitController2);
          }
        }
        else if (this.self.mTargetSelectorParam.Skill != null && this.self.mTargetSelectorParam.Skill.IsAllEffect())
        {
          if (Object.op_Inequality((Object) unitController1, (Object) null))
          {
            this.self.mFocusedUnit = unitController1;
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
            this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
          }
          SkillData skill = this.self.mTargetSelectorParam.Skill;
          Unit unit = (Unit) null;
          if (skill != null)
          {
            int hpCost = skill.GetHpCost(currentUnit);
            unitController1.SetHPChangeYosou((int) currentUnit.CurrentStatus.param.hp - hpCost);
            this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp, hpCost, 0);
            this.self.mBattleUI.TargetMain.UpdateHpGauge();
            if (this.self.mTargetSelectorParam.OnAccept != null && this.mTargetGrids.get(grid.x, grid.y))
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
                    unitController2.SetHPChangeYosou((int) commandResult.targets[index].unit.CurrentStatus.param.hp - commandResult.targets[index].hp_damage);
                    this.self.mBattleUI.TargetSub.SetHpGaugeParam(commandResult.targets[index].unit.Side, (int) commandResult.targets[index].unit.CurrentStatus.param.hp, (int) commandResult.targets[index].unit.MaximumStatus.param.hp, commandResult.targets[index].hp_damage, 0);
                  }
                  else
                  {
                    unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Target, (SkillData) null, (Unit) null);
                    unitController2.SetHPChangeYosou((int) commandResult.targets[index].unit.CurrentStatus.param.hp + commandResult.targets[index].hp_heal);
                    this.self.mBattleUI.TargetSub.SetHpGaugeParam(commandResult.targets[index].unit.Side, (int) commandResult.targets[index].unit.CurrentStatus.param.hp, (int) commandResult.targets[index].unit.MaximumStatus.param.hp, 0, commandResult.targets[index].hp_heal);
                  }
                  this.self.mBattleUI.TargetSub.UpdateHpGauge();
                  this.self.UIParam_TargetValid = true;
                }
              }
              if (skill.IsTargetGridNoUnit)
                this.self.UIParam_TargetValid = !this.self.UIParam_TargetValid;
              else if (skill.IsCastSkill() && !this.self.UIParam_TargetValid)
                this.self.UIParam_TargetValid = true;
            }
          }
          else
            unit = this.self.mBattle.FindGimmickAtGrid(grid, false);
          this.mTargetPosition.x = grid.x;
          this.mTargetPosition.y = grid.y;
          this.SetYesButtonEnable(this.self.UIParam_TargetValid);
          if (Object.op_Inequality((Object) this.self.mBattleUI.TargetMain, (Object) null))
          {
            if (skill == null)
            {
              this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
              this.self.mBattleUI.TargetMain.Close();
            }
            else
            {
              if (!this.self.UIParam_TargetValid)
                this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
              this.self.mBattleUI.TargetMain.Open();
            }
          }
          if (Object.op_Inequality((Object) this.self.mBattleUI.TargetSub, (Object) null))
          {
            if (skill == null)
            {
              if (unit != null)
              {
                this.self.mBattleUI.OnMapViewSelectGrid();
                this.self.mBattleUI.TargetSub.Close();
              }
              else
              {
                this.self.mBattleUI.OnMapViewSelectGrid();
                this.self.mBattleUI.TargetSub.Close();
              }
            }
            else
              this.self.mBattleUI.TargetSub.Close();
          }
          if (Object.op_Inequality((Object) this.self.mBattleUI.TargetObjectSub, (Object) null))
          {
            if (skill == null)
            {
              if (unit != null)
              {
                this.self.mBattleUI.TargetObjectSub.SetNoAction(unit);
                this.self.mBattleUI.TargetObjectSub.Open();
                this.self.mBattleUI.OnMapViewSelectGrid();
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
        if (Object.op_Inequality((Object) this.mScene.mFocusedUnit, (Object) null) && this.mScene.mFocusedUnit.Unit != null && this.mScene.mFocusedUnit.Unit == unit)
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
        if (!Object.op_Inequality((Object) controller, (Object) null))
          return;
        SkillData skill = this.self.mTargetSelectorParam.Skill;
        if (skill != null)
        {
          if (skill.IsAllEffect())
            controller = this.self.FindUnitController(this.self.mBattle.CurrentUnit);
          else if (!this.IsValidSkillTarget(controller.Unit))
          {
            for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
            {
              if (Object.op_Inequality((Object) this.self.mTacticsUnits[index], (Object) controller) && this.self.mTacticsUnits[index].Unit.x == controller.Unit.x && (this.self.mTacticsUnits[index].Unit.y == controller.Unit.y && this.IsValidSkillTarget(this.self.mTacticsUnits[index].Unit)))
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
          if (Object.op_Inequality((Object) this.mSelectedTarget, (Object) null))
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
        unitController1.SetHPChangeYosou((int) currentUnit.CurrentStatus.param.hp - YosokuDamageHp);
        this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp, YosokuDamageHp, 0);
        this.self.mBattleUI.TargetMain.UpdateHpGauge();
        this.self.mBattleUI.TargetSub.ResetHpGauge(this.self.mSelectedTarget.Side, (int) this.self.mSelectedTarget.CurrentStatus.param.hp, (int) this.self.mSelectedTarget.MaximumStatus.param.hp);
        bool flag1 = false;
        BattleCore.UnitResult unitResult = (BattleCore.UnitResult) null;
        if (this.self.mTargetSelectorParam.Skill != null)
        {
          if (Object.op_Inequality((Object) controller, (Object) null) && this.self.mTutorialTriggers != null)
          {
            for (int index = 0; index < this.self.mTutorialTriggers.Length; ++index)
              this.self.mTutorialTriggers[index].OnTargetChange(this.mSelectedTarget.Unit, controller.TurnCount);
          }
          for (int index = 0; index < this.self.mTacticsUnits.Count; ++index)
          {
            this.self.mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
            if (0 >= YosokuDamageHp || !Object.op_Equality((Object) this.self.mTacticsUnits[index], (Object) controller))
              this.self.mTacticsUnits[index].SetHPChangeYosou(this.self.mTacticsUnits[index].VisibleHPValue);
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
            BattleCore.CommandResult commandResult = this.self.mBattle.GetCommandResult(currentUnit, intVector2_2.x, intVector2_2.y, x, y, skill);
            if (commandResult != null && commandResult.targets != null)
            {
              for (int index = 0; index < commandResult.reactions.Count; ++index)
              {
                if (this.self.mSelectedTarget == commandResult.reactions[index].react_unit)
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
                  unitController2.SetHPChangeYosou((int) commandResult.targets[index].unit.CurrentStatus.param.hp - commandResult.targets[index].hp_damage);
                }
                else
                {
                  unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Target, (SkillData) null, (Unit) null);
                  unitController2.SetHPChangeYosou((int) commandResult.targets[index].unit.CurrentStatus.param.hp + commandResult.targets[index].hp_heal);
                }
              }
              if (commandResult.self_effect.hp_damage > 0 || commandResult.self_effect.hp_heal > 0)
              {
                int newHP = Mathf.Min((int) currentUnit.CurrentStatus.param.hp - commandResult.self_effect.hp_damage + commandResult.self_effect.hp_heal - YosokuDamageHp, (int) currentUnit.MaximumStatus.param.hp);
                unitController1.SetHPChangeYosou(newHP);
                this.self.mBattleUI.TargetMain.SetHpGaugeParam(currentUnit.Side, (int) currentUnit.CurrentStatus.param.hp, (int) currentUnit.MaximumStatus.param.hp, commandResult.self_effect.hp_damage + YosokuDamageHp, commandResult.self_effect.hp_heal);
                this.self.mBattleUI.TargetMain.UpdateHpGauge();
              }
              if (commandResult.targets.Count > 0)
              {
                bool flag2 = skill.IsNormalAttack();
                int index = commandResult.targets.FindIndex((Predicate<BattleCore.UnitResult>) (p => p.unit == this.self.mSelectedTarget));
                if (index != -1 && Object.op_Inequality((Object) this.self.mBattleUI.TargetMain, (Object) null))
                {
                  BattleCore.UnitResult target = commandResult.targets[index];
                  EUnitSide side = commandResult.targets[index].unit.Side;
                  int hp1 = (int) commandResult.targets[index].unit.CurrentStatus.param.hp;
                  int hp2 = (int) commandResult.targets[index].unit.MaximumStatus.param.hp;
                  if (skill.SkillParam.IsHealSkill())
                  {
                    this.self.mBattleUI.TargetMain.SetHealAction(this.self.mBattle.CurrentUnit, target.hp_heal, target.critical, target.avoid);
                    this.self.mBattleUI.TargetSub.SetHpGaugeParam(side, hp1, hp2, 0, commandResult.targets[index].hp_heal);
                  }
                  else if (skill.SkillParam.IsDamagedSkill() || skill.SkillParam.effect_type == SkillEffectTypes.Debuff)
                  {
                    if (flag2)
                      this.self.mBattleUI.TargetMain.SetAttackAction(this.self.mBattle.CurrentUnit, target.hp_damage, target.critical, 100 - target.avoid);
                    else
                      this.self.mBattleUI.TargetMain.SetSkillAction(this.self.mBattle.CurrentUnit, target.hp_damage <= 0 ? target.mp_damage : target.hp_damage, target.critical, 100 - target.avoid);
                    this.self.mBattleUI.TargetSub.SetHpGaugeParam(side, hp1, hp2, commandResult.targets[index].hp_damage, 0);
                  }
                  else
                    this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
                  this.self.mBattleUI.TargetSub.UpdateHpGauge();
                  flag1 = true;
                }
                this.self.UIParam_TargetValid = true;
                if (!skill.IsAreaSkill() && this.self.mSelectedTarget != null)
                  this.self.UIParam_TargetValid = this.self.Battle.CheckSkillTarget(currentUnit, this.self.mSelectedTarget, skill);
              }
            }
          }
        }
        else
          this.HilitNormalAttack(controller.Unit, false);
        this.SetYesButtonEnable(this.self.UIParam_TargetValid);
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetMain, (Object) null))
        {
          if (skill == null)
          {
            this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
            this.self.mBattleUI.TargetMain.Close();
          }
          else
          {
            if (!flag1)
              this.self.mBattleUI.TargetMain.SetNoAction(this.self.mBattle.CurrentUnit);
            this.self.mBattleUI.TargetMain.Open();
          }
        }
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetSub, (Object) null))
        {
          if (skill == null)
          {
            if (controller.Unit.IsGimmick)
            {
              this.self.mBattleUI.TargetSub.Close();
              this.self.mBattleUI.OnMapViewSelectGrid();
            }
            else
            {
              this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
              this.self.mBattleUI.TargetSub.Open();
              this.self.mBattleUI.OnMapViewSelectUnit();
            }
          }
          else if (flag1)
          {
            if (unitResult != null)
              this.self.mBattleUI.TargetSub.SetAttackAction(this.self.mSelectedTarget, unitResult.hp_damage <= 0 ? unitResult.mp_damage : unitResult.hp_damage, 0, 100 - unitResult.avoid);
            else
              this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
            this.self.mBattleUI.TargetSub.Open();
          }
          else if (this.self.mSelectedTarget.UnitType == EUnitType.Unit || this.self.mSelectedTarget.UnitType == EUnitType.EventUnit)
          {
            this.self.mBattleUI.TargetSub.SetNoAction(this.self.mSelectedTarget);
            this.self.mBattleUI.TargetSub.Open();
          }
          else
            this.self.mBattleUI.TargetSub.Close();
        }
        if (Object.op_Inequality((Object) this.self.mBattleUI.TargetObjectSub, (Object) null))
        {
          if (skill == null)
          {
            if (controller.Unit.IsGimmick && !controller.Unit.IsDisableGimmick())
            {
              this.self.mBattleUI.OnMapViewSelectGrid();
              this.self.mBattleUI.TargetObjectSub.Open();
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
        this.self.OnGimmickUpdate();
        this.self.SetUnitUiHeight(controller.Unit);
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
            this.ShiftTarget(-1);
          }
          else if ((double) this.mYScrollPos >= 1.0)
          {
            this.mYScrollPos = 0.0f;
            this.mIgnoreDragVelocity = true;
            this.ShiftTarget(1);
          }
        }
        if (!this.mSelectGrid || self.mDesiredCameraTargetSet)
          return;
        int index1 = Mathf.Clamp(Mathf.FloorToInt((float) self.mCameraTarget.x), 0, self.mBattle.CurrentMap.Width);
        int index2 = Mathf.Clamp(Mathf.FloorToInt((float) self.mCameraTarget.z), 0, self.mBattle.CurrentMap.Height);
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
        int num = this.mTargets.IndexOf(this.self.mFocusedUnit);
        if (num < 0)
          num = 0;
        this.OnClickUnit(this.mTargets[(num + delta + this.mTargets.Count) % this.mTargets.Count]);
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
        TacticsUnitController unitController = self.FindUnitController(self.mBattle.CurrentUnit);
        if (Object.op_Inequality((Object) unitController, (Object) null) && self.mTutorialTriggers != null)
        {
          for (int index = 0; index < self.mTutorialTriggers.Length; ++index)
            self.mTutorialTriggers[index].OnSelectDirectionStart(this.mCurrentUnit, unitController.TurnCount);
        }
        self.InterpCameraOffset(GameSettings.Instance.Quest.MoveCamera);
        self.InterpCameraTarget((Component) this.mController);
        self.HideAllHPGauges();
        self.HideAllUnitOwnerIndex();
        Selectable component = (Selectable) self.mBattleUI.CommandWindow.CancelButton.GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component, (Object) null))
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
        this.mCurrentUnit.Direction = unitController.CalcUnitDirectionFromRotation();
      }

      private void OnStepEnd()
      {
        this.self.mOnScreenClick = new SceneBattle.ScreenClickEvent(this.OnScreenClick);
        for (int index = 0; index < 4; ++index)
        {
          Quaternion rotation = ((EUnitDirection) index).ToRotation();
          Vector3 vector3 = Quaternion.op_Multiply(rotation, ((Component) this.self.DirectionArrowTemplate).get_transform().get_position());
          this.mArrows[index] = Object.Instantiate((Object) this.self.DirectionArrowTemplate, Vector3.op_Addition(((Component) this.mController).get_transform().get_position(), vector3), rotation) as DirectionArrow;
        }
        this.UpdateArrows();
        this.self.mOnRequestStateChange = new SceneBattle.StateTransitionRequest(this.OnStateChange);
        this.self.mOnScreenClick = new SceneBattle.ScreenClickEvent(this.OnScreenClick);
        this.self.mBattleUI.CommandWindow.OnYesNoSelect += new UnitCommands.YesNoEvent(this.OnYesNoSelect);
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
          if (!Object.op_Inequality((Object) self.mTouchController, (Object) null))
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
          if (Object.op_Inequality((Object) this.mArrows[index], (Object) null) && index != this.mSelectedDirection)
            this.mArrows[index].State = DirectionArrow.ArrowStates.Close;
        }
        Selectable component = (Selectable) self.mBattleUI.CommandWindow.CancelButton.GetComponent<Selectable>();
        if (Object.op_Inequality((Object) component, (Object) null))
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
        if (MonoSingleton<GameManager>.Instance.IsTutorial() && Object.op_Inequality((Object) SGHighlightObject.Instance(), (Object) null))
          SGHighlightObject.Instance().GridSelected(-1, -1);
        this.SelectDirection((EUnitDirection) num2);
      }

      private void SelectDirection(EUnitDirection dir)
      {
        this.self.ApplyUnitMovement(false);
        this.mArrows[(int) dir].State = DirectionArrow.ArrowStates.Press;
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0003", 0.0f);
        this.mSelectedDirection = (int) dir;
        this.self.mBattleUI.OnInputDirectionEnd();
        if (this.self.Battle.IsMultiPlay)
        {
          this.self.SendInputUnitEnd(this.self.Battle.CurrentUnit, dir);
          this.self.SendInputFlush();
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
        if (Object.op_Inequality((Object) this.mSkillList, (Object) null))
          this.mSkillList.OnSelectSkill = new UnitAbilitySkillList.SelectSkillEvent(this.OnSelectSkill);
        self.StepToNear(currentUnit);
      }

      public override void End(SceneBattle self)
      {
        self.mOnRequestStateChange = (SceneBattle.StateTransitionRequest) null;
        if (!Object.op_Inequality((Object) this.mSkillList, (Object) null))
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
          if (this.self.Battle.ExecuteEventTriggerOnGrid(this.self.Battle.CurrentUnit, EEventTrigger.ExecuteOnGrid, false))
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
        this.SceneOwner.mAllowCameraTranslation = false;
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
        this.SceneOwner.SetMoveCamera();
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
        this.SceneOwner.mAllowCameraTranslation = true;
        this.SceneOwner.mDisplayBlockedGridMarker = false;
        List<TacticsUnitController> mTacticsUnits = this.SceneOwner.mTacticsUnits;
        for (int index = 0; index < mTacticsUnits.Count; ++index)
        {
          mTacticsUnits[index].SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
          mTacticsUnits[index].SetHPChangeYosou(mTacticsUnits[index].VisibleHPValue);
        }
        if (this.SceneOwner.Battle.EntryBattleMultiPlayTimeUp)
          this.mController.AutoUpdateRotation = true;
        this.mController.StopRunning();
        this.mController.WalkableField = (GridMap<int>) null;
        this.mController.HideCursor(false);
        this.SceneOwner.mTacticsSceneRoot.HideGridLayer(0);
      }

      private void OnDrag()
      {
      }

      private void OnDragEnd()
      {
      }

      private void OnClick(Vector2 screenPos)
      {
        if (this.SceneOwner.Battle.EntryBattleMultiPlayTimeUp)
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
          vector2_1 = Vector2.op_Subtraction(vector2_2, position);
          Unit unit2 = (Unit) null;
          float num = float.MaxValue;
          SkillData attackSkill = unit1.GetAttackSkill();
          for (int index = 0; index < this.SceneOwner.mTacticsUnits.Count; ++index)
          {
            TacticsUnitController mTacticsUnit = this.SceneOwner.mTacticsUnits[index];
            if (!Object.op_Equality((Object) mTacticsUnit, (Object) null))
            {
              Unit unit3 = mTacticsUnit.Unit;
              if (unit3 != null)
              {
                RectTransform hpGaugeTransform = mTacticsUnit.HPGaugeTransform;
                if (!Object.op_Equality((Object) hpGaugeTransform, (Object) null) && ((Component) hpGaugeTransform).get_gameObject().get_activeInHierarchy() && (this.mShateiGrid.isValid(unit3.x, unit3.y) && this.mShateiGrid.get(unit3.x, unit3.y)) && this.SceneOwner.mBattle.CheckSkillTarget(unit1, unit3, attackSkill))
                {
                  Vector2 vector2_3 = Vector2.op_Subtraction(hpGaugeTransform.get_anchoredPosition(), vector2_1);
                  // ISSUE: explicit reference operation
                  float magnitude = ((Vector2) @vector2_3).get_magnitude();
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
        if (MonoSingleton<GameManager>.Instance.IsTutorial() && Object.op_Inequality((Object) SGHighlightObject.Instance(), (Object) null))
        {
          if (!(intVector2_1 == SGHighlightObject.Instance().highlightedGrid))
            return;
          SGHighlightObject.Instance().GridSelected(intVector2_1.x, intVector2_1.y);
        }
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
          Vector2[] vector2Array = new Vector2[8]{ new Vector2(-1f, 1f), new Vector2(0.0f, 1f), new Vector2(1f, 1f), new Vector2(1f, 0.0f), new Vector2(1f, -1f), new Vector2(0.0f, -1f), new Vector2(-1f, -1f), new Vector2(-1f, 0.0f) };
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
          if (!Object.op_Inequality((Object) main, (Object) null) || !Object.op_Inequality((Object) virtualStick, (Object) null))
            return Vector2.get_zero();
          Transform transform = ((Component) main).get_transform();
          Vector3 forward = transform.get_forward();
          Vector3 right = transform.get_right();
          forward.y = (__Null) 0.0;
          ((Vector3) @forward).Normalize();
          right.y = (__Null) 0.0;
          ((Vector3) @right).Normalize();
          Vector2 velocity = virtualStick.Velocity;
          return new Vector2((float) (right.x * velocity.x + forward.x * velocity.y), (float) (right.z * velocity.x + forward.z * velocity.y));
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
            Vector3 position = this.mController.CenterPosition;
            bool flag1 = false;
            Vector2 vector2;
            // ISSUE: explicit reference operation
            ((Vector2) @vector2).\u002Ector((float) position.x, (float) position.z);
            if (this.mTargetSet && this.GridEqualIn2D(vector2, this.mTargetPos))
            {
              this.mBasePos = this.mTargetPos;
              this.mDestX = Mathf.FloorToInt((float) this.mBasePos.x);
              this.mDestY = Mathf.FloorToInt((float) this.mBasePos.y);
              this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, this.mDestX, this.mDestY, this.SceneOwner.Battle.CurrentUnit.Direction);
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
              IntVector2 intVector2_1 = this.SceneOwner.CalcCoord(this.mController.CenterPosition);
              this.mDestX = intVector2_1.x;
              this.mDestY = intVector2_1.y;
              this.mBasePos.x = (__Null) ((double) this.mDestX + 0.5);
              this.mBasePos.y = (__Null) ((double) this.mDestY + 0.5);
label_17:
              this.UpdateBlockMarker();
              IntVector2 intVector2_2 = this.SceneOwner.CalcCoord(this.mController.CenterPosition);
              this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, intVector2_2.x, intVector2_2.y, this.SceneOwner.Battle.CurrentUnit.Direction);
              this.SceneOwner.SendInputMove(this.SceneOwner.Battle.CurrentUnit, this.mController);
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
                    this.SceneOwner.SendInputGridXY(this.SceneOwner.Battle.CurrentUnit, this.mDestX, this.mDestY, this.SceneOwner.Battle.CurrentUnit.Direction);
                  }
                  else
                  {
                    position = Vector3.op_Addition(position, Vector3.op_Multiply(velocity2, Time.get_deltaTime()));
                    flag1 = true;
                  }
                }
                this.SceneOwner.SendInputMove(this.SceneOwner.Battle.CurrentUnit, this.mController);
              }
              IntVector2 intVector2 = this.SceneOwner.CalcCoord(position);
              if (intVector2.x != this.mCurrentX || intVector2.y != this.mCurrentY)
              {
                this.mCurrentX = intVector2.x;
                this.mCurrentY = intVector2.y;
                this.RecalcAttackTargets();
              }
              if (flag1)
              {
                transform.set_position(position);
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
        SkillData attackSkill = unit1.GetAttackSkill();
        this.mUpdateShateiGrid = true;
        this.mShateiGrid = this.SceneOwner.Battle.CreateSelectGridMap(unit1, this.mCurrentX, this.mCurrentY, attackSkill);
        this.SceneOwner.mNumHotTargets = 0;
        List<Unit> units = this.SceneOwner.mBattle.Units;
        for (int index1 = 0; index1 < units.Count; ++index1)
        {
          TacticsUnitController unitController1 = this.SceneOwner.FindUnitController(units[index1]);
          if (!Object.op_Equality((Object) unitController1, (Object) null))
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
                  if (!Object.op_Equality((Object) unitController2, (Object) null))
                  {
                    unitController2.SetHPChangeYosou((int) unit2.CurrentStatus.param.hp - commandResult.targets[index2].hp_damage);
                    unitController2.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Attack, attackSkill, unit1);
                    ++this.SceneOwner.mNumHotTargets;
                    if (Object.op_Equality((Object) unitController1, (Object) unitController2))
                      flag = true;
                  }
                }
                if (!flag)
                {
                  unitController1.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
                  unitController1.SetHPChangeYosou(unitController1.VisibleHPValue);
                }
              }
            }
            else
            {
              unitController1.SetHPGaugeMode(TacticsUnitController.HPGaugeModes.Normal, (SkillData) null, (Unit) null);
              unitController1.SetHPChangeYosou(unitController1.VisibleHPValue);
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
