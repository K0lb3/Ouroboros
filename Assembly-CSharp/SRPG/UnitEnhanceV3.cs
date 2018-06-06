// Decompiled with JetBrains decompiler
// Type: SRPG.UnitEnhanceV3
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
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "閉じる", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(1, "初期化完了", FlowNode.PinTypes.Output, 1)]
  public class UnitEnhanceV3 : MonoBehaviour, IFlowInterface
  {
    public static UnitEnhanceV3 Instance = (UnitEnhanceV3) null;
    private static string[] UnitHourVoices = new string[24]{ "chara_0007", "chara_0007", "chara_0007", "chara_0007", "chara_0002", "chara_0002", "chara_0002", "chara_0002", "chara_0003", "chara_0003", "chara_0003", "chara_0003", "chara_0004", "chara_0004", "chara_0004", "chara_0004", "chara_0005", "chara_0005", "chara_0005", "chara_0005", "chara_0006", "chara_0006", "chara_0006", "chara_0006" };
    private const float ExpAnimSpan = 1f;
    [Space(10f)]
    public string PreviewParentID;
    public string PreviewBaseID;
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
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobRankUp;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobUnlock;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobCC;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_NewAbilityList;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_NewSkillList;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_NewSkillList2;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_ReturnItems;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_LeveUp;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_Kakusei;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_Evolution;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobChange;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_JobMaster;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_UnitPieceWindow;
    [StringIsResourcePath(typeof (GameObject))]
    public string Prefab_ProfileWindow;
    public Tooltip Prefab_LockedJobTooltip;
    public Tooltip Prefab_ParamTooltip;
    public UnitCharacterQuestWindow Prefab_CharaQuestListWindow;
    public SRPG_Button CharaQuestButton;
    public SRPG_Button SkinButton;
    [StringIsResourcePath(typeof (GameObject))]
    public string SkinSelectTemplate;
    public GameObject Prefab_UnitDataUnlockPopup;
    [Space(10f)]
    public bool ShowNoStockExpPotions;
    [Space(10f)]
    public RectTransform ActiveJobIndicator;
    public RectTransform JobIconParent;
    public AnimatedToggle JobIconTemplate_Normal;
    public AnimatedToggle JobIconTemplate_CC;
    public string JobIconUnlockBool;
    public string JobIconUnlockHilitBool;
    public SRPG_ToggleButton JobChangeButton;
    public UnityEngine.UI.Text JobRank;
    [Space(10f)]
    public SRPG_ToggleButton Tab_Equipments;
    public SRPG_ToggleButton Tab_Kyoka;
    public SRPG_ToggleButton Tab_AbilityList;
    public SRPG_ToggleButton Tab_AbilitySlot;
    public SRPG_Button UnitKakuseiButton;
    public SRPG_Button UnitEvolutionButton;
    public string UnitKakuseiButtonHilitBool;
    public string UnitEvolutionButtonHilitBool;
    public string JobRankUpButtonHilitBool;
    public string AllEquipButtonHilitBool;
    [Space(10f)]
    public RectTransform TabPageParent;
    public UnitEnhancePanel Prefab_Equipments;
    public UnitEnhancePanel Prefab_Kyoka;
    public UnitEnhancePanel Prefab_AbilityList;
    public UnitEnhancePanel Prefab_AbilitySlots;
    public UnitAbilityPicker Prefab_AbilityPicker;
    public UnitEvolutionWindow Prefab_EvolutionWindow;
    public GameObject Prefab_ArtifactWindow;
    public GameObject Prefab_IkkatsuEquip;
    [Space(10f)]
    public SRPG_Button NextUnitButton;
    public SRPG_Button PrevUnitButton;
    [Space(10f)]
    public UnityEngine.UI.Text Status_HP;
    public UnityEngine.UI.Text Status_MP;
    public UnityEngine.UI.Text Status_MPIni;
    public UnityEngine.UI.Text Status_Atk;
    public UnityEngine.UI.Text Status_Def;
    public UnityEngine.UI.Text Status_Mag;
    public UnityEngine.UI.Text Status_Mnd;
    public UnityEngine.UI.Text Status_Rec;
    public UnityEngine.UI.Text Status_Dex;
    public UnityEngine.UI.Text Status_Spd;
    public UnityEngine.UI.Text Status_Cri;
    public UnityEngine.UI.Text Status_Luk;
    public UnityEngine.UI.Text Status_Mov;
    public UnityEngine.UI.Text Status_Jmp;
    public UnityEngine.UI.Text Param_Renkei;
    [Space(10f)]
    public Vector3 PreviewWindowDir;
    [Space(10f)]
    public GameObject UnitInfo;
    public GameObject JobInfo;
    public GenericSlot LeaderSkillInfo;
    public SRPG_Button LeaderSkillDetailButton;
    public GameObject Prefab_LeaderSkillDetail;
    public GameObject UnitExpInfo;
    public GameObject UnitRarityInfo;
    public SliderAnimator UnitEXPSlider;
    public UnityEngine.UI.Text UnitLevel;
    public Color32 UnitLevelColor;
    public Color32 CappedUnitLevelColor;
    public GameObject UnitLevelCapInfo;
    public UnityEngine.UI.Text UnitExp;
    public UnityEngine.UI.Text UnitExpMax;
    public UnityEngine.UI.Text UnitExpNext;
    [Space(10f)]
    public string BGAnimatorID;
    public string BGAnimatorPlayTrigger;
    public string BGUnitImageID;
    public float BGUnitImageFadeTime;
    public string ExpOverflowWarning;
    public Tooltip Prefab_LockedArtifactSlotTooltip;
    private bool mStarted;
    private Canvas mOverlayCanvas;
    private Animator mBGAnimator;
    private RawImage mBGUnitImage;
    private long mCurrentUnitID;
    private long mCurrentJobUniqueID;
    private long mIsSetJobUniqueID;
    private UnitData mCurrentUnit;
    private float mLastSyncTime;
    private UnitEnhancePanel mEquipmentPanel;
    private UnitEnhancePanel mKyokaPanel;
    private UnitEnhancePanel mAbilityListPanel;
    private UnitEnhancePanel mAbilitySlotPanel;
    private UnitAbilityPicker mAbilityPicker;
    private UnitEvolutionWindow mEvolutionWindow;
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
    private List<AnimatedToggle> mJobIcons;
    private List<AnimatedToggle> mCCIcons;
    private List<AnimatedToggle> mJobSlots;
    private Transform mPreviewParent;
    private GameObject mPreviewBase;
    private UnitPreview mCurrentPreview;
    private List<UnitPreview> mPreviewControllers;
    private List<string> mPreviewJobNames;
    private int mSelectedJobIndex;
    private SRPG_ToggleButton[] mTabButtons;
    private UnitEnhancePanel[] mTabPages;
    private int mActiveTabIndex;
    private bool mReloading;
    private UnityEngine.UI.Text[] mStatusParamSlots;
    private bool mCloseRequested;
    private float mBGUnitImgAlphaStart;
    private float mBGUnitImgAlphaEnd;
    private float mBGUnitImgFadeTime;
    private float mBGUnitImgFadeTimeMax;
    private UnitEquipmentWindow mEquipmentWindow;
    private UnitKakeraWindow mKakeraWindow;
    private UnitCharacterQuestWindow mCharacterQuestWindow;
    private float mExpStart;
    private float mExpEnd;
    private float mExpAnimTime;
    private float mLeftTime;
    private UnitEnhanceV3.DeferredJob mDefferedCallFunc;
    private float mDefferedCallDelay;
    private long[] mOriginalAbilities;
    private List<long> mRankedUpAbilities;
    private List<ItemParam> mUsedExpItems;
    private List<long> mDirtyUnits;
    public UnitEnhanceV3.CloseEvent OnUserClose;
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
    private UnitEnhanceV3.ExpItemTouchController mCurrentKyoukaItemHold;
    private List<UnitData> SortedUnits;
    private bool mSceneChanging;
    private bool mIsJobLvUpAllEquip;
    private bool mIsJobLvMaxAllEquip;
    private BaseStatus mCurrentStatus;
    private int mCurrentRenkei;
    [NonSerialized]
    public bool MuteVoice;
    private GameObject ClickItem;
    private bool mSendingKyokaRequest;
    private bool mKeepWindowLocked;
    private bool mJobChangeRequestSent;
    private string mNextJobID;
    private string mPrevJobID;
    private bool mJobRankUpRequestSent;
    private bool mDesiredPreviewVisibility;
    private bool mUpdatePreviewVisibility;
    private bool mAppPausing;
    private UnitAbilityListItemEvents.ListItemTouchController mCurrentAbilityRankUpHold;
    private bool mAbilityRankUpRequestSent;
    private LoadRequest mProfileWindowLoadRequest;
    private bool mKakuseiRequestSent;
    private bool mEvolutionRequestSent;

    public UnitEnhanceV3()
    {
      base.\u002Ector();
    }

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) UnitEnhanceV3.Instance, (Object) null))
        return;
      UnitEnhanceV3.Instance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) UnitEnhanceV3.Instance, (Object) this))
        return;
      UnitEnhanceV3.Instance = (UnitEnhanceV3) null;
    }

    public UnitData CurrentUnit
    {
      get
      {
        return this.mCurrentUnit;
      }
    }

    public long IsSetJobUniqueID
    {
      get
      {
        return this.mIsSetJobUniqueID;
      }
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CStart\u003Ec__IteratorDA() { \u003C\u003Ef__this = this };
    }

    private void OnDestroy()
    {
      this.DestroyOverlay();
      if (Object.op_Inequality((Object) this.mArtifactSelector, (Object) null))
      {
        Object.Destroy((Object) this.mArtifactSelector.get_gameObject());
        this.mArtifactSelector = (GameObject) null;
      }
      if (Object.op_Inequality((Object) this.mParamTooltip, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mParamTooltip).get_gameObject());
        this.mParamTooltip = (Tooltip) null;
      }
      if (Object.op_Inequality((Object) this.mUnitProfileWindow, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mUnitProfileWindow).get_gameObject());
        this.mUnitProfileWindow = (UnitProfileWindow) null;
      }
      if (Object.op_Inequality((Object) this.mUnitUnlockWindow, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mUnitUnlockWindow).get_gameObject());
        this.mUnitUnlockWindow = (UnitUnlockWindow) null;
      }
      if (Object.op_Inequality((Object) this.mJobUnlockTooltip, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mJobUnlockTooltip).get_gameObject());
        this.mJobUnlockTooltip = (Tooltip) null;
      }
      if (Object.op_Inequality((Object) this.mLeaderSkillDetail, (Object) null))
        Object.Destroy((Object) this.mLeaderSkillDetail.get_gameObject());
      if (Object.op_Inequality((Object) this.mEvolutionWindow, (Object) null))
        Object.Destroy((Object) ((Component) this.mEvolutionWindow).get_gameObject());
      if (Object.op_Inequality((Object) this.mSkinSelectWindow, (Object) null))
        Object.Destroy((Object) this.mSkinSelectWindow.get_gameObject());
      if (Object.op_Inequality((Object) this.mAbilityPicker, (Object) null))
        Object.Destroy((Object) ((Component) this.mAbilityPicker).get_gameObject());
      if (Object.op_Inequality((Object) this.mCharacterQuestWindow, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mCharacterQuestWindow).get_gameObject());
        this.mCharacterQuestWindow = (UnitCharacterQuestWindow) null;
      }
      if (Object.op_Inequality((Object) this.mIkkatsuEquipWindow, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mIkkatsuEquipWindow).get_gameObject());
        this.mIkkatsuEquipWindow = (UnitJobRankUpConfirm) null;
      }
      if (Object.op_Inequality((Object) this.mEquipArtifactUnlockTooltip, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mEquipArtifactUnlockTooltip).get_gameObject());
        this.mEquipArtifactUnlockTooltip = (Tooltip) null;
      }
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (Object.op_Inequality((Object) instanceDirect, (Object) null))
      {
        instanceDirect.OnSceneChange -= new GameManager.SceneChangeEvent(this.OnSceneCHange);
        instanceDirect.OnAbilityRankUpCountPreReset -= new GameManager.RankUpCountChangeEvent(this.OnAbilityRankUpCountPreReset);
      }
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      GameUtility.DestroyGameObjects<AnimatedToggle>(this.mJobIcons);
      GameUtility.DestroyGameObjects<AnimatedToggle>(this.mCCIcons);
      this.mUnitVoice = (MySound.Voice) null;
    }

    private bool OnSceneCHange()
    {
      if (!this.mSceneChanging)
      {
        this.mSceneChanging = true;
        MonoSingleton<GameManager>.Instance.RegisterImportantJob(this.StartCoroutine(this.OnSceneChangeAsync()));
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator OnSceneChangeAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003COnSceneChangeAsync\u003Ec__IteratorDB() { \u003C\u003Ef__this = this };
    }

    private void OnEquip()
    {
      JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(this.mSelectedJobIndex);
      if (jobSetParam == null)
        return;
      ((WindowController) ((Component) this.EquipmentWindow).GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      WindowController.CloseIfAvailable((Component) this.mEquipmentWindow);
      if (Network.Mode == Network.EConnectMode.Online)
        Network.RequestAPI((WebAPI) new ReqJobEquipV2(this.mCurrentUnit.UniqueID, jobSetParam.iname, (long) this.mSelectedEquipmentSlot, new Network.ResponseCallback(this.OnEquipResult)), false);
      else
        this.StartCoroutine(this.PostEquip());
      this.SetUnitDirty();
    }

    private void OnEquipResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobSetEquip:
            FlowNode_Network.Failed();
            break;
          case Network.EErrCode.NoEquipItem:
          case Network.EErrCode.Equipped:
            Network.RemoveAPI();
            Network.ResetError();
            ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(true);
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        try
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          instance.Deserialize(jsonObject.body.player);
          instance.Deserialize(jsonObject.body.units);
          instance.Deserialize(jsonObject.body.items);
          this.mLastSyncTime = Time.get_realtimeSinceStartup();
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.RefreshSortedUnits();
        this.StartCoroutine(this.PostEquip());
      }
    }

    private void OnEquipAll()
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.Prefab_IkkatsuEquip);
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      DataSource.Bind<UnitData>(gameObject, this.mCurrentUnit);
      UnitJobRankUpConfirm component = (UnitJobRankUpConfirm) gameObject.GetComponent<UnitJobRankUpConfirm>();
      if (Object.op_Equality((Object) component, (Object) null) || this.mCurrentUnit == null || this.mCurrentUnit.CurrentJob == null)
        return;
      if (this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) > this.mCurrentUnit.CurrentJob.Rank)
        component.OnAllEquipAccept = new UnitJobRankUpConfirm.OnAccept(this.OnJobRankUpEquipAllAccept);
      else
        component.OnAllEquipAccept = new UnitJobRankUpConfirm.OnAccept(this.OnEquipAllAccept);
    }

    private void OnJobRankUpEquipAllAccept()
    {
      this.mIsJobLvUpAllEquip = true;
      this.StartJobRankUp();
    }

    private void OnEquipAllAccept()
    {
      if (Network.Mode == Network.EConnectMode.Online)
      {
        JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(this.mSelectedJobIndex);
        if (jobSetParam != null)
          Network.RequestAPI((WebAPI) new ReqJobRankupAll(this.mCurrentUnit.UniqueID, jobSetParam.iname, new Network.ResponseCallback(this.OnEquipAllResult)), false);
        else
          this.StartCoroutine(this.PostEquip());
      }
      this.SetUnitDirty();
    }

    private void OnEquipAllResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobSetEquip:
            FlowNode_Network.Failed();
            break;
          case Network.EErrCode.NoEquipItem:
            Network.ResetError();
            Network.RemoveAPI();
            this.mJobRankUpRequestSent = true;
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        try
        {
          GameManager instance = MonoSingleton<GameManager>.Instance;
          instance.Deserialize(jsonObject.body.player);
          instance.Deserialize(jsonObject.body.units);
          instance.Deserialize(jsonObject.body.items);
          this.mLastSyncTime = Time.get_realtimeSinceStartup();
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.RefreshSortedUnits();
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
        if (this.mIsJobLvMaxAllEquip)
          this.StartCoroutine(this.PostEquip());
        else
          this.mJobRankUpRequestSent = true;
      }
    }

    private void BeginStatusChangeCheck()
    {
      this.mCurrentStatus = new BaseStatus(this.mCurrentUnit.Status);
      this.mCurrentRenkei = this.mCurrentUnit.GetCombination();
    }

    private void SpawnStatusChangeEffects()
    {
      BaseStatus status = this.mCurrentUnit.Status;
      for (int index = 0; index < (int) StatusParam.MAX_STATUS; ++index)
      {
        if (!Object.op_Equality((Object) this.mStatusParamSlots[index], (Object) null))
        {
          int delta = (int) status.param[(StatusTypes) index] - (int) this.mCurrentStatus.param[(StatusTypes) index];
          if (delta != 0)
            this.SpawnParamDeltaEffect(((Component) this.mStatusParamSlots[index]).get_gameObject(), delta);
        }
      }
      if (!Object.op_Inequality((Object) this.Param_Renkei, (Object) null))
        return;
      this.SpawnParamDeltaEffect(((Component) this.Param_Renkei).get_gameObject(), this.mCurrentUnit.GetCombination() - this.mCurrentRenkei);
    }

    private void SpawnParamDeltaEffect(GameObject go, int delta)
    {
      if (delta == 0)
        return;
      GameObject gameObject = (GameObject) null;
      StringBuilder stringBuilder = GameUtility.GetStringBuilder();
      if (delta > 0)
      {
        gameObject = this.ParamUpEffect;
        stringBuilder.Append('+');
      }
      else if (delta < 0)
        gameObject = this.ParamDownEffect;
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      GameObject go1 = (GameObject) Object.Instantiate<GameObject>((M0) gameObject);
      go1.get_transform().SetParent(go.get_transform(), false);
      (go1.get_transform() as RectTransform).set_anchoredPosition(Vector2.get_zero());
      stringBuilder.Append(delta);
      UnityEngine.UI.Text componentInChildren = (UnityEngine.UI.Text) go1.GetComponentInChildren<UnityEngine.UI.Text>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        componentInChildren.set_text(stringBuilder.ToString());
      go1.RequireComponent<DestructTimer>();
    }

    [DebuggerHidden]
    private IEnumerator PostEquip()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostEquip\u003Ec__IteratorDC() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator PostJobMaster()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostJobMaster\u003Ec__IteratorDD() { \u003C\u003Ef__this = this };
    }

    private void PlayReaction()
    {
      if (Object.op_Equality((Object) this.mCurrentPreview, (Object) null))
        return;
      this.mCurrentPreview.PlayAction = true;
    }

    private void PlayUnitVoice(string name)
    {
      if (this.MuteVoice || this.mCurrentUnit == null)
        return;
      if (this.mUnitVoice != null)
        this.mUnitVoice.StopAll(1f);
      string skinVoiceSheetName = this.mCurrentUnit.GetUnitSkinVoiceSheetName(-1);
      if (string.IsNullOrEmpty(skinVoiceSheetName))
        return;
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = this.mCurrentUnit.GetUnitSkinVoiceCueName(-1) + "_";
      this.mUnitVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix);
      this.mUnitVoice.Play(name, 0.0f);
      this.mLeftTime = Time.get_realtimeSinceStartup();
    }

    private void SpawnJobChangeButtonEffect()
    {
      if (Object.op_Equality((Object) this.JobChangeButton, (Object) null) || Object.op_Equality((Object) this.JobChangeButton, (Object) null))
        return;
      UIUtility.SpawnParticle(this.JobChangeButtonEffect, ((Component) this.JobChangeButton).get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
    }

    private void SpawnEquipEffect(int slot)
    {
      if (Object.op_Equality((Object) this.EquipSlotEffect, (Object) null) || slot < 0 || (slot >= this.mEquipmentPanel.EquipmentSlots.Length || Object.op_Equality((Object) this.mEquipmentPanel.EquipmentSlots[slot], (Object) null)))
        return;
      UIUtility.SpawnParticle(this.EquipSlotEffect, ((Component) this.mEquipmentPanel.EquipmentSlots[slot]).get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
    }

    private void OnTabChange(SRPG_Button button)
    {
      if (!this.TabChange(button))
        return;
      UnitEnhancePanel mTabPage = this.mTabPages[this.mActiveTabIndex];
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      if (Object.op_Equality((Object) mTabPage, (Object) this.mEquipmentPanel))
      {
        this.PlayUnitVoice("chara_0012");
        if (this.mEquipmentPanelDirty)
          this.RefreshEquipments(-1);
      }
      if (Object.op_Equality((Object) mTabPage, (Object) this.mAbilityListPanel))
      {
        this.PlayUnitVoice("chara_0014");
        if (this.mAbilityListDirty)
          this.RefreshAbilityList();
      }
      if (Object.op_Equality((Object) mTabPage, (Object) this.mAbilitySlotPanel))
      {
        this.PlayUnitVoice("chara_0015");
        if (this.mAbilitySlotDirty)
          this.RefreshAbilitySlots();
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if ((instance.Player.TutorialFlags & 1L) == 0L && instance.GetNextTutorialStep() == "ShowAbilitySetupTab")
          instance.CompleteTutorialStep();
      }
      if (Object.op_Equality((Object) mTabPage, (Object) this.mKyokaPanel))
        this.PlayUnitVoice("chara_0016");
      this.mLeftTime = Time.get_realtimeSinceStartup();
    }

    private bool TabChange(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return false;
      int num = Array.IndexOf<SRPG_Button>((SRPG_Button[]) this.mTabButtons, button);
      this.mActiveTabIndex = num;
      for (int index = 0; index < this.mTabButtons.Length; ++index)
      {
        bool flag = index == num;
        if (Object.op_Inequality((Object) this.mTabButtons[index], (Object) null))
          this.mTabButtons[index].IsOn = flag;
        if (Object.op_Inequality((Object) this.mTabPages[index], (Object) null))
        {
          Canvas component = (Canvas) ((Component) this.mTabPages[index]).GetComponent<Canvas>();
          if (Object.op_Inequality((Object) component, (Object) null))
            ((Behaviour) component).set_enabled(flag);
        }
      }
      return true;
    }

    private UnitEnhancePanel InitTabPage(int pageIndex, UnitEnhancePanel prefab, bool visible)
    {
      if (Object.op_Equality((Object) prefab, (Object) null))
        return (UnitEnhancePanel) null;
      this.mTabPages[pageIndex] = (UnitEnhancePanel) Object.Instantiate<UnitEnhancePanel>((M0) prefab);
      ((Component) this.mTabPages[pageIndex]).get_transform().SetParent((Transform) this.TabPageParent, false);
      Canvas component = (Canvas) ((Component) this.mTabPages[pageIndex]).GetComponent<Canvas>();
      if (Object.op_Inequality((Object) component, (Object) null))
        ((Behaviour) component).set_enabled(visible);
      return this.mTabPages[pageIndex];
    }

    private void InitEquipmentPanel(UnitEnhancePanel panel)
    {
      for (int index = 0; index < panel.EquipmentSlots.Length; ++index)
      {
        if (Object.op_Inequality((Object) panel.EquipmentSlots[index], (Object) null))
          panel.EquipmentSlots[index].OnSelect = new ListItemEvents.ListItemEvent(this.OnEquipmentSlotSelect);
      }
      if (Object.op_Inequality((Object) panel.JobRankUpButton, (Object) null))
        panel.JobRankUpButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
      if (Object.op_Inequality((Object) panel.JobUnlockButton, (Object) null))
        panel.JobUnlockButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
      if (Object.op_Inequality((Object) panel.AllEquipButton, (Object) null))
        panel.AllEquipButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnJobRankUpClick));
      if (Object.op_Inequality((Object) panel.ArtifactSlot, (Object) null))
        panel.ArtifactSlot.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
      if (Object.op_Inequality((Object) panel.ArtifactSlot2, (Object) null))
        panel.ArtifactSlot2.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
      if (!Object.op_Inequality((Object) panel.ArtifactSlot3, (Object) null))
        return;
      panel.ArtifactSlot3.OnSelect = new GenericSlot.SelectEvent(this.OnArtifactClick);
    }

    private void InitKyokaPanel(UnitEnhancePanel panel)
    {
      RectTransform expItemList = panel.ExpItemList;
      ListItemEvents expItemTemplate = panel.ExpItemTemplate;
      if (Object.op_Equality((Object) expItemList, (Object) null) || Object.op_Equality((Object) expItemTemplate, (Object) null))
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < player.Items.Count; ++index)
      {
        ItemData data = player.Items[index];
        if (data != null && data.Param != null && (data.Param.type == EItemType.ExpUpUnit && this.ShowNoStockExpPotions) && data.Num > 0)
        {
          ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) expItemTemplate);
          if (this.HoldUseItemEnable)
          {
            ((Component) listItemEvents).get_gameObject().AddComponent<UnitEnhanceV3.ExpItemTouchController>();
            UnitEnhanceV3.ExpItemTouchController component = (UnitEnhanceV3.ExpItemTouchController) ((Component) listItemEvents).get_gameObject().GetComponent<UnitEnhanceV3.ExpItemTouchController>();
            component.OnPointerDownFunc = new UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown(this.OnExpItemButtonDown);
            component.OnPointerUpFunc = new UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown(this.OnExpItemButtonUp);
            component.UseItemFunc = new UnitEnhanceV3.ExpItemTouchController.DelegateUseItem(this.OnExpItemHoldUse);
          }
          else
            listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnExpItemClick);
          DataSource.Bind<ItemData>(((Component) listItemEvents).get_gameObject(), data);
          ((Component) listItemEvents).get_gameObject().SetActive(true);
          ((Component) listItemEvents).get_transform().SetParent(((Component) expItemList).get_transform(), false);
        }
      }
    }

    private void OnExpItemButtonDown(UnitEnhanceV3.ExpItemTouchController controller)
    {
      this.mCurrentKyoukaItemHold = controller;
    }

    private void OnExpItemButtonUp(UnitEnhanceV3.ExpItemTouchController controller)
    {
      if (!Object.op_Inequality((Object) this.mCurrentKyoukaItemHold, (Object) null) || !Object.op_Inequality((Object) this.mKyoukaItemScroll, (Object) null))
        return;
      ((Behaviour) this.mKyoukaItemScroll).set_enabled(true);
      this.mKyoukaItemScroll = (ScrollRect) null;
    }

    private void OnExpItemHoldUse(GameObject listItem)
    {
      this.OnExpItemClick(listItem);
      if (!Object.op_Inequality((Object) this.mCurrentKyoukaItemHold, (Object) null) || !Object.op_Equality((Object) this.mKyoukaItemScroll, (Object) null))
        return;
      this.mKyoukaItemScroll = (ScrollRect) ((Component) this.mKyokaPanel).GetComponentInChildren<ScrollRect>();
      if (!Object.op_Inequality((Object) this.mKyoukaItemScroll, (Object) null))
        return;
      ((Behaviour) this.mKyoukaItemScroll).set_enabled(false);
    }

    private void OnExpOverflowOk(GameObject dialog)
    {
      this.OnExpItemClick(this.ClickItem);
    }

    private void OnExpOverflowNo(GameObject dialog)
    {
      this.ClickItem = (GameObject) null;
    }

    private void OnExpItemClick(GameObject go)
    {
      if (this.mSceneChanging || this.ExecQueuedKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitUnitKyoka)))
        return;
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null || dataOfClass.Num <= 0)
      {
        Button component = (Button) go.GetComponent<Button>();
        if (!Object.op_Inequality((Object) component, (Object) null) || !((Selectable) component).get_interactable())
          return;
        ((Selectable) component).set_interactable(false);
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0015", 0.0f);
      }
      else if (!this.mCurrentUnit.CheckGainExp())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.LEVEL_CAPPED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        if (!this.HoldUseItemEnable || !Object.op_Inequality((Object) this.mCurrentKyoukaItemHold, (Object) null))
          return;
        this.mCurrentKyoukaItemHold.StatusReset();
        this.mCurrentKyoukaItemHold = (UnitEnhanceV3.ExpItemTouchController) null;
      }
      else
      {
        if (Object.op_Equality((Object) this.ClickItem, (Object) null))
        {
          GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
          int lv = instanceDirect.Player.Lv;
          int levelCap = this.mCurrentUnit.GetLevelCap(false);
          if ((lv >= levelCap ? instanceDirect.MasterParam.GetUnitLevelExp(levelCap) - this.mCurrentUnit.Exp : instanceDirect.MasterParam.GetUnitLevelExp(lv + 1) - 1 - this.mCurrentUnit.Exp) < (int) dataOfClass.Param.value)
          {
            this.ClickItem = go;
            this.mCurrentKyoukaItemHold.StatusReset();
            this.mCurrentKyoukaItemHold = (UnitEnhanceV3.ExpItemTouchController) null;
            UIUtility.ConfirmBox(LocalizedText.Get(this.ExpOverflowWarning), new UIUtility.DialogResultEvent(this.OnExpOverflowOk), new UIUtility.DialogResultEvent(this.OnExpOverflowNo), (GameObject) null, true, -1);
            return;
          }
        }
        if (Object.op_Inequality((Object) this.ClickItem, (Object) null))
          this.ClickItem = (GameObject) null;
        this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
        this.mUsedExpItems.Add(dataOfClass.Param);
        int lv1 = this.mCurrentUnit.Lv;
        int exp = this.mCurrentUnit.Exp;
        this.BeginStatusChangeCheck();
        if (!MonoSingleton<GameManager>.Instance.Player.UseExpPotion(this.mCurrentUnit, dataOfClass))
          return;
        if (dataOfClass.Num <= 0)
        {
          Button component = (Button) go.GetComponent<Button>();
          if (Object.op_Inequality((Object) component, (Object) null) && ((Selectable) component).get_interactable())
          {
            ((Selectable) component).set_interactable(false);
            MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0015", 0.0f);
          }
        }
        int delta = this.mCurrentUnit.Exp - exp;
        if (delta <= 0)
          this.SpawnAddExpEffect(delta, dataOfClass.Param);
        this.AnimateGainExp(this.mCurrentUnit.Exp);
        this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitUnitKyoka), 0.0f);
        GameParameter.UpdateAll(go);
        if (this.mCurrentUnit.Lv == lv1)
          return;
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.Unit);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.ItemEquipment);
        this.mKeepWindowLocked = true;
        this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
        if (this.HoldUseItemEnable && this.HoldUseItemLvUpStop && Object.op_Inequality((Object) this.mCurrentKyoukaItemHold, (Object) null))
        {
          this.mCurrentKyoukaItemHold.StatusReset();
          this.mCurrentKyoukaItemHold = (UnitEnhanceV3.ExpItemTouchController) null;
        }
        this.StartCoroutine(this.PostUnitLevelUp(lv1));
      }
    }

    private void SpawnAddExpEffect(int delta, ItemParam item)
    {
    }

    private void AnimateGainExp(int desiredExp)
    {
      if ((double) this.mExpStart < (double) this.mExpEnd && (double) this.mExpAnimTime < 1.0)
        this.mExpStart = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / 1f);
      this.mExpAnimTime = 0.0f;
      this.mExpEnd = (float) desiredExp;
    }

    [DebuggerHidden]
    private IEnumerator PostUnitLevelUp(int prevLv)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostUnitLevelUp\u003Ec__IteratorDE() { prevLv = prevLv, \u003C\u0024\u003EprevLv = prevLv, \u003C\u003Ef__this = this };
    }

    private void RefreshLevelCap()
    {
      int lv = MonoSingleton<GameManager>.Instance.Player.Lv;
      int levelCap = this.mCurrentUnit.GetLevelCap(false);
      if (Object.op_Inequality((Object) this.UnitLevel, (Object) null))
      {
        this.UnitLevel.set_text(this.mCurrentUnit.Lv.ToString());
        ((Graphic) this.UnitLevel).set_color(Color32.op_Implicit(this.mCurrentUnit.Lv >= levelCap || this.mCurrentUnit.Lv >= lv ? this.CappedUnitLevelColor : this.UnitLevelColor));
      }
      if (!Object.op_Inequality((Object) this.UnitLevelCapInfo, (Object) null))
        return;
      this.UnitLevelCapInfo.SetActive(this.mCurrentUnit.Lv >= lv);
    }

    private void RefreshEXPImmediate()
    {
      if (Object.op_Equality((Object) this.UnitEXPSlider, (Object) null))
        return;
      int exp1 = this.mCurrentUnit.Exp;
      int exp2 = this.mCurrentUnit.GetExp();
      int num = exp2 + this.mCurrentUnit.GetNextExp();
      this.mExpStart = this.mExpEnd = (float) exp1;
      this.RefreshLevelCap();
      if (Object.op_Inequality((Object) this.UnitExp, (Object) null))
        this.UnitExp.set_text(exp1.ToString());
      if (Object.op_Inequality((Object) this.UnitExpMax, (Object) null))
        this.UnitExpMax.set_text(num.ToString());
      if (Object.op_Inequality((Object) this.UnitExpNext, (Object) null))
        this.UnitExpNext.set_text(this.mCurrentUnit.GetNextExp().ToString());
      if (num <= 0)
        num = 1;
      this.UnitEXPSlider.AnimateValue(Mathf.Clamp01((float) exp2 / (float) num), 0.0f);
    }

    private bool ExecQueuedKyokaRequest(UnitEnhanceV3.DeferredJob func)
    {
      if (this.mDefferedCallFunc == null || !((MulticastDelegate) this.mDefferedCallFunc != (MulticastDelegate) func))
        return this.mSendingKyokaRequest;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
      UnitEnhanceV3.DeferredJob defferedCallFunc = this.mDefferedCallFunc;
      this.mDefferedCallFunc = (UnitEnhanceV3.DeferredJob) null;
      this.mSendingKyokaRequest = true;
      this.mDefferedCallDelay = 0.0f;
      defferedCallFunc();
      return true;
    }

    private bool IsKyokaRequestQueued
    {
      get
      {
        return this.mDefferedCallFunc != null;
      }
    }

    private void FinishKyokaRequest()
    {
      this.mSendingKyokaRequest = false;
      this.RefreshSortedUnits();
      this.UpdateJobRankUpButtonState();
      if (this.mKeepWindowLocked || !((WindowController) ((Component) this).GetComponent<WindowController>()).IsOpened)
        return;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(true);
    }

    private Coroutine SyncKyokaRequest()
    {
      return this.StartCoroutine(this.WaitForKyokaRequestAsync(true));
    }

    [DebuggerHidden]
    private IEnumerator WaitForKyokaRequestAsync(bool unlockWindow)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CWaitForKyokaRequestAsync\u003Ec__IteratorDF() { unlockWindow = unlockWindow, \u003C\u0024\u003EunlockWindow = unlockWindow, \u003C\u003Ef__this = this };
    }

    private void QueueKyokaRequest(UnitEnhanceV3.DeferredJob func, float delay)
    {
      this.mDefferedCallFunc = func;
      this.mDefferedCallDelay = delay;
    }

    private bool IsUnitImageFading
    {
      get
      {
        return (double) this.mBGUnitImgFadeTime < (double) this.mBGUnitImgFadeTimeMax;
      }
    }

    private void FadeUnitImage(float alphaStart, float alphaEnd, float duration)
    {
      this.mBGUnitImgAlphaStart = alphaStart;
      this.mBGUnitImgAlphaEnd = alphaEnd;
      this.mBGUnitImgFadeTime = 0.0f;
      this.mBGUnitImgFadeTimeMax = duration;
      if ((double) duration > 0.0)
        return;
      this.SetUnitImageAlpha(this.mBGUnitImgAlphaEnd);
    }

    private void SetUnitImageAlpha(float alpha)
    {
      if (Object.op_Equality((Object) this.mBGUnitImage, (Object) null))
        return;
      Color color = ((Graphic) this.mBGUnitImage).get_color();
      color.a = (__Null) (double) alpha;
      ((Graphic) this.mBGUnitImage).set_color(color);
    }

    private void Update()
    {
      if (((WindowController) ((Component) this).GetComponent<WindowController>()).IsOpened)
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID);
        if (unitDataByUniqueId != null && (double) unitDataByUniqueId.LastSyncTime > (double) this.mLastSyncTime)
        {
          long jobUniqueID = GlobalVars.SelectedJobUniqueID.Get();
          this.Refresh(this.mCurrentUnitID, unitDataByUniqueId.Jobs[this.mSelectedJobIndex].UniqueID, true);
          this.RefreshReturningJobState(jobUniqueID);
        }
      }
      if (this.mUpdatePreviewVisibility && this.mDesiredPreviewVisibility && (Object.op_Inequality((Object) this.mCurrentPreview, (Object) null) && !this.mCurrentPreview.IsLoading))
      {
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerCH, true);
        this.mUpdatePreviewVisibility = false;
      }
      if ((double) this.mBGUnitImgFadeTime < (double) this.mBGUnitImgFadeTimeMax && Object.op_Inequality((Object) this.mBGUnitImage, (Object) null))
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
      if ((double) this.mExpStart < (double) this.mExpEnd)
        this.AnimateExp();
      if ((double) this.mDefferedCallDelay > 0.0)
      {
        this.mDefferedCallDelay -= Time.get_unscaledDeltaTime();
        if ((double) this.mDefferedCallDelay <= 0.0)
          this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      }
      if (Object.op_Inequality((Object) this.mCurrentKyoukaItemHold, (Object) null) && !this.mKeepWindowLocked)
      {
        if (this.mCurrentKyoukaItemHold.Holding)
        {
          this.mCurrentKyoukaItemHold.UpdateTimer(Time.get_unscaledDeltaTime());
        }
        else
        {
          this.mCurrentKyoukaItemHold = (UnitEnhanceV3.ExpItemTouchController) null;
          if (Object.op_Inequality((Object) this.mKyoukaItemScroll, (Object) null))
            this.mKyoukaItemScroll.set_scrollSensitivity(1f);
        }
      }
      if (Object.op_Inequality((Object) this.mCurrentAbilityRankUpHold, (Object) null))
      {
        if (this.mCurrentAbilityRankUpHold.Holding)
          this.mCurrentAbilityRankUpHold.UpdatePress(Time.get_unscaledDeltaTime());
        else
          this.mCurrentAbilityRankUpHold = (UnitAbilityListItemEvents.ListItemTouchController) null;
      }
      if ((double) Time.get_realtimeSinceStartup() - (double) this.mLeftTime <= 60.0)
        return;
      this.PlayUnitVoice("chara_0008");
    }

    private void AnimateExp()
    {
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      int levelCap = this.mCurrentUnit.GetLevelCap(false);
      float num1 = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / 1f);
      this.mExpAnimTime += Time.get_unscaledDeltaTime();
      float num2 = Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / 1f));
      int totalExp1 = Mathf.FloorToInt(num1);
      int totalExp2 = Mathf.FloorToInt(num2);
      int num3 = masterParam.CalcUnitLevel(totalExp1, levelCap);
      int lv = masterParam.CalcUnitLevel(totalExp2, levelCap);
      if (num3 == lv)
        ;
      int unitLevelExp = masterParam.GetUnitLevelExp(lv);
      int num4 = masterParam.GetUnitLevelExp(Mathf.Min(lv + 1, levelCap)) - unitLevelExp;
      if (Object.op_Inequality((Object) this.UnitLevel, (Object) null))
        this.UnitLevel.set_text(lv.ToString());
      if (Object.op_Inequality((Object) this.UnitExp, (Object) null))
        this.UnitExp.set_text((totalExp2 - unitLevelExp).ToString());
      if (Object.op_Inequality((Object) this.UnitExpMax, (Object) null))
        this.UnitExpMax.set_text(num4.ToString());
      if (Object.op_Inequality((Object) this.UnitExpNext, (Object) null))
        this.UnitExpNext.set_text(Mathf.FloorToInt((float) (num4 - (totalExp2 - unitLevelExp))).ToString());
      if (Object.op_Inequality((Object) this.UnitEXPSlider, (Object) null))
        this.UnitEXPSlider.AnimateValue(num4 <= 0 ? 1f : (num2 - (float) unitLevelExp) / (float) num4, 0.0f);
      if ((double) this.mExpAnimTime < 1.0)
        return;
      this.mExpStart = this.mExpEnd;
      this.mExpAnimTime = 0.0f;
      this.RefreshLevelCap();
    }

    private void SubmitUnitKyoka()
    {
      Dictionary<string, int> usedItems = new Dictionary<string, int>();
      for (int index1 = 0; index1 < this.mUsedExpItems.Count; ++index1)
      {
        string iname = this.mUsedExpItems[index1].iname;
        if (usedItems.ContainsKey(iname))
        {
          Dictionary<string, int> dictionary;
          string index2;
          (dictionary = usedItems)[index2 = iname] = dictionary[index2] + 1;
        }
        else
          usedItems[iname] = 1;
      }
      this.mUsedExpItems.Clear();
      if (Network.Mode == Network.EConnectMode.Online)
        Network.RequestAPI((WebAPI) new ReqUnitExpAdd(this.mCurrentUnitID, usedItems, new Network.ResponseCallback(this.OnUnitKyokaResult)), false);
      else
        this.FinishKyokaRequest();
      this.SetUnitDirty();
    }

    private void OnUnitKyokaResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.ExpMaterialShort)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        if (jsonObject.body == null)
        {
          FlowNode_Network.Failed();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            this.mLastSyncTime = Time.get_realtimeSinceStartup();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Failed();
            return;
          }
          Network.RemoveAPI();
          this.FinishKyokaRequest();
        }
      }
    }

    public void OpenEquipmentSlot(int slotIndex)
    {
      if (slotIndex < 0)
        return;
      this.mSelectedEquipmentSlot = slotIndex;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
      ((WindowController) ((Component) this.EquipmentWindow).GetComponent<WindowController>()).OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnEquipmentWindowCancel);
      WindowController.OpenIfAvailable((Component) this.EquipmentWindow);
      if (Object.op_Inequality((Object) this.EquipmentWindow.SubWindow, (Object) null))
        this.EquipmentWindow.SubWindow.SetActive(false);
      this.EquipmentWindow.Refresh(this.mCurrentUnit, slotIndex);
    }

    private void OnEquipmentSlotSelect(GameObject go)
    {
      int slotIndex = Array.IndexOf<UnitEquipmentSlotEvents>(this.mEquipmentPanel.EquipmentSlots, (UnitEquipmentSlotEvents) go.GetComponent<UnitEquipmentSlotEvents>());
      GlobalVars.SelectedEquipmentSlot.Set(slotIndex);
      this.OpenEquipmentSlot(slotIndex);
    }

    private void OnEquipmentWindowCancel(GameObject go, bool visible)
    {
      if (visible)
        return;
      ((WindowController) ((Component) this.EquipmentWindow).GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      this.RefreshEquipments(-1);
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(true);
    }

    private void OnJobChangeClick(SRPG_Button button)
    {
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      if (this.mCurrentUnit.CurrentJob.Rank <= 0)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.UE_JOB_NOT_UNLOCKED"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        if (this.mCurrentUnit.CurrentJob.UniqueID == this.mCurrentJobUniqueID || !((Selectable) button).IsInteractable())
          return;
        JobParam jobParam = this.mCurrentUnit.CurrentJob.Param;
        StringBuilder stringBuilder = GameUtility.GetStringBuilder();
        stringBuilder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_CONFIRM", new object[1]
        {
          (object) jobParam.name
        }));
        stringBuilder.Append('\n');
        string[] jobChangeItems = jobParam.GetJobChangeItems(this.mCurrentUnit.CurrentJob.Rank);
        int[] jobChangeItemNums = jobParam.GetJobChangeItemNums(this.mCurrentUnit.CurrentJob.Rank);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        if (jobChangeItems != null && jobChangeItemNums != null)
        {
          for (int index = 0; index < jobChangeItems.Length; ++index)
          {
            if (!string.IsNullOrEmpty(jobChangeItems[index]))
            {
              ItemParam itemParam = instance.GetItemParam(jobChangeItems[index]);
              if (itemParam != null)
              {
                int num = jobChangeItemNums[index];
                stringBuilder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_REQITEM", (object) itemParam.name, (object) num));
                stringBuilder.Append('\n');
              }
            }
          }
        }
        int jobChangeCost = jobParam.GetJobChangeCost(this.mCurrentUnit.CurrentJob.Rank);
        if (jobChangeCost > 0)
          stringBuilder.Append(LocalizedText.Get("sys.UE_JOBCHANGE_REQGOLD", new object[1]
          {
            (object) jobChangeCost
          }));
        this.mPrevJobID = (string) null;
        for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
        {
          if (this.mCurrentUnit.Jobs[index].UniqueID == this.mCurrentJobUniqueID)
            this.mPrevJobID = this.mCurrentUnit.Jobs[index].Param.iname;
        }
        this.mNextJobID = this.mCurrentUnit.CurrentJob.Param.iname;
        UIUtility.ConfirmBox(stringBuilder.ToString(), new UIUtility.DialogResultEvent(this.OnJobChangeAccept), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
    }

    private void OnJobChangeAccept(GameObject go)
    {
      JobParam jobParam = this.mCurrentUnit.CurrentJob.Param;
      string[] jobChangeItems = jobParam.GetJobChangeItems(this.mCurrentUnit.CurrentJob.Rank);
      int[] jobChangeItemNums = jobParam.GetJobChangeItemNums(this.mCurrentUnit.CurrentJob.Rank);
      bool flag1 = true;
      bool flag2 = true;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (jobChangeItems != null && jobChangeItemNums != null)
      {
        for (int index = 0; index < jobChangeItems.Length; ++index)
        {
          if (!string.IsNullOrEmpty(jobChangeItems[index]))
          {
            ItemParam itemParam = instance.GetItemParam(jobChangeItems[index]);
            if (itemParam != null && flag1 && instance.Player.FindItemDataByItemParam(itemParam).Num < jobChangeItemNums[index])
              flag1 = false;
          }
        }
      }
      int jobChangeCost = jobParam.GetJobChangeCost(this.mCurrentUnit.CurrentJob.Rank);
      if (jobChangeCost > 0 && instance.Player.Gold < jobChangeCost)
        flag2 = false;
      if (!flag1 && !flag2)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.UE_JOBCHANGE_NOITEMGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (!flag1)
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.UE_JOBCHANGE_NOITEM"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      else if (!flag2)
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.UE_JOBCHANGE_NOGOLD"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.SpawnJobChangeButtonEffect();
        this.mKeepWindowLocked = true;
        ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
        this.mJobChangeRequestSent = false;
        if (Network.Mode == Network.EConnectMode.Online)
        {
          this.RequestAPI((WebAPI) new ReqUnitJob(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, new Network.ResponseCallback(this.OnJobChangeResult)));
        }
        else
        {
          MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID).SetJobIndex(this.mCurrentUnit.JobIndex);
          this.mJobChangeRequestSent = true;
          instance.Player.OnJobChange(this.mCurrentUnit.UnitID);
        }
        this.SetUnitDirty();
        this.StartCoroutine(this.PostJobChange());
      }
    }

    [DebuggerHidden]
    private IEnumerator PostJobChange()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostJobChange\u003Ec__IteratorE0() { \u003C\u003Ef__this = this };
    }

    private void OnJobChangeResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobSetJob:
          case Network.EErrCode.CantSelectJob:
          case Network.EErrCode.NoUnitSetJob:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          this.mLastSyncTime = Time.get_realtimeSinceStartup();
          Network.RemoveAPI();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        this.mJobChangeRequestSent = true;
        this.RefreshSortedUnits();
        MonoSingleton<GameManager>.Instance.Player.OnJobChange(this.mCurrentUnit.UnitID);
      }
    }

    private void ShowLockEquipArtifactTooltip(GenericSlot slot)
    {
      if (Object.op_Equality((Object) slot, (Object) null))
        return;
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCurrentUnit.Jobs.Length; ++index2)
      {
        if (this.mCurrentUnit.Jobs[index2].UniqueID == (long) GlobalVars.SelectedJobUniqueID)
        {
          index1 = index2;
          break;
        }
      }
      bool flag = this.mCurrentUnit.Jobs[index1].Rank > 0;
      if (flag && Object.op_Equality((Object) slot, (Object) this.mEquipmentPanel.ArtifactSlot))
        return;
      int index3 = 0;
      if (Object.op_Equality((Object) slot, (Object) this.mEquipmentPanel.ArtifactSlot2))
        index3 = 1;
      else if (Object.op_Equality((Object) slot, (Object) this.mEquipmentPanel.ArtifactSlot3))
        index3 = 2;
      if (Object.op_Equality((Object) this.Prefab_LockedArtifactSlotTooltip, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mEquipArtifactUnlockTooltip, (Object) null))
      {
        this.mEquipArtifactUnlockTooltip.Close();
        this.mEquipArtifactUnlockTooltip = (Tooltip) null;
      }
      else
      {
        Tooltip.SetTooltipPosition(((Component) this.mEquipmentPanel.ArtifactSlot2).get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
        if (Object.op_Equality((Object) this.mEquipArtifactUnlockTooltip, (Object) null))
          this.mEquipArtifactUnlockTooltip = (Tooltip) Object.Instantiate<Tooltip>((M0) this.Prefab_LockedArtifactSlotTooltip);
        else
          this.mEquipArtifactUnlockTooltip.ResetPosition();
        if (!Object.op_Inequality((Object) this.mEquipArtifactUnlockTooltip.TooltipText, (Object) null))
          return;
        if (flag)
        {
          FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
          if (fixParam == null || fixParam.EquipArtifactSlotUnlock == null || fixParam.EquipArtifactSlotUnlock.Length <= 0)
            return;
          this.mEquipArtifactUnlockTooltip.TooltipText.set_text(LocalizedText.Get("sys.EQUIP_ARTIFACT_SLOT_TOOLTIP", new object[1]
          {
            (object) fixParam.EquipArtifactSlotUnlock[index3].ToString()
          }));
        }
        else
          this.mEquipArtifactUnlockTooltip.TooltipText.set_text(LocalizedText.Get("sys.TOOLTIP_ARIFACT_UNLOCK"));
      }
    }

    private void OnArtifactClick(GenericSlot slot, bool interactable)
    {
      if (!interactable)
      {
        this.ShowLockEquipArtifactTooltip(slot);
      }
      else
      {
        if (Object.op_Equality((Object) this.Prefab_ArtifactWindow, (Object) null))
          return;
        this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
        bool flag = false;
        if (Object.op_Equality((Object) this.mArtifactSelector, (Object) null))
        {
          this.mArtifactSelector = (GameObject) Object.Instantiate<GameObject>((M0) this.Prefab_ArtifactWindow);
        }
        else
        {
          this.mArtifactSelector.SetActive(true);
          flag = true;
        }
        ArtifactWindow component = (ArtifactWindow) this.mArtifactSelector.GetComponent<ArtifactWindow>();
        if (Object.op_Equality((Object) component, (Object) null))
          return;
        if (Object.op_Inequality((Object) slot, (Object) null))
        {
          if (Object.op_Equality((Object) slot, (Object) this.mEquipmentPanel.ArtifactSlot))
            component.SelectArtifactSlot = ArtifactTypes.Arms;
          else if (Object.op_Equality((Object) slot, (Object) this.mEquipmentPanel.ArtifactSlot2))
            component.SelectArtifactSlot = ArtifactTypes.Armor;
          else if (Object.op_Equality((Object) slot, (Object) this.mEquipmentPanel.ArtifactSlot3))
            component.SelectArtifactSlot = ArtifactTypes.Accessory;
        }
        component.OnEquip = new ArtifactWindow.EquipEvent(this.OnArtifactSelect);
        component.SetOwnerUnit(this.mCurrentUnit, this.mSelectedJobIndex);
        if (!Object.op_Inequality((Object) component.ArtifactList, (Object) null))
          return;
        if (component.ArtifactList.TestOwner != this.mCurrentUnit)
        {
          component.ArtifactList.TestOwner = this.mCurrentUnit;
          flag = true;
        }
        long iid = 0;
        if (this.mCurrentUnit.CurrentJob.Artifacts != null)
        {
          for (int index = 0; index < this.mCurrentUnit.CurrentJob.Artifacts.Length; ++index)
          {
            if (this.mCurrentUnit.CurrentJob.Artifacts[index] != 0L)
            {
              iid = this.mCurrentUnit.CurrentJob.Artifacts[index];
              break;
            }
          }
        }
        ArtifactData artifactData = (ArtifactData) null;
        if (iid != 0L)
          artifactData = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(iid);
        if (artifactData != null)
          component.ArtifactList.SetSelection((object[]) new ArtifactData[1]
          {
            artifactData
          }, true, true);
        else
          component.ArtifactList.SetSelection((object[]) new ArtifactData[0], true, true);
        ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(((Component) slot).get_gameObject(), (ArtifactData) null);
        ArtifactTypes select_slot = dataOfClass == null ? ArtifactTypes.None : dataOfClass.ArtifactParam.type;
        component.ArtifactList.FiltersPriority = this.SetEquipArtifactFilters(select_slot);
        if (!flag)
          return;
        component.ArtifactList.Refresh();
      }
    }

    private string[] SetEquipArtifactFilters(ArtifactTypes select_slot = ArtifactTypes.None)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < RarityParam.MAX_RARITY; ++index)
        stringList.Add("RARE:" + (object) (index + 1));
      JobData currentJob = this.mCurrentUnit.CurrentJob;
      for (int index = 0; index < currentJob.ArtifactDatas.Length; ++index)
      {
        ArtifactData artifactData = currentJob.ArtifactDatas[index];
        if (artifactData == null)
          stringList.Add("TYPE:" + (object) (index + 1));
        else if (artifactData.ArtifactParam.type == select_slot)
          stringList.Add("TYPE:" + (object) (index + 1));
      }
      return stringList.ToArray();
    }

    public int GetUnlockAritfactSlot()
    {
      int num = 0;
      int awakeLv = this.mCurrentUnit.AwakeLv;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (fixParam != null && fixParam.EquipArtifactSlotUnlock != null && fixParam.EquipArtifactSlotUnlock.Length > 0)
      {
        for (int index = 0; index < fixParam.EquipArtifactSlotUnlock.Length; ++index)
        {
          if (awakeLv >= (int) fixParam.EquipArtifactSlotUnlock[index])
            ++num;
        }
      }
      return Mathf.Max(num, 1);
    }

    private void OnArtifactSelect(ArtifactData artifact, ArtifactTypes type = ArtifactTypes.None)
    {
      this.BeginStatusChangeCheck();
      if (artifact != null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        UnitData unit = (UnitData) null;
        JobData job = (JobData) null;
        if (player.FindOwner(artifact, out unit, out job))
        {
          for (int slot = 0; slot < job.Artifacts.Length; ++slot)
          {
            if (job.Artifacts[slot] == (long) artifact.UniqueID)
            {
              job.SetEquipArtifact(slot, (ArtifactData) null);
              break;
            }
          }
        }
      }
      List<ArtifactData> artifactDataList = new List<ArtifactData>();
      for (int slot = 0; slot < this.mCurrentUnit.CurrentJob.ArtifactDatas.Length; ++slot)
      {
        if (this.mCurrentUnit.CurrentJob.ArtifactDatas[slot] != null)
          artifactDataList.Add(this.mCurrentUnit.CurrentJob.ArtifactDatas[slot]);
        this.mCurrentUnit.CurrentJob.SetEquipArtifact(slot, (ArtifactData) null);
      }
      int unlockAritfactSlot = this.GetUnlockAritfactSlot();
      int slot1 = -1;
      if (artifact != null)
      {
        slot1 = JobData.GetArtifactSlotIndex(artifact.ArtifactParam.type);
        if (slot1 >= 0)
          this.mCurrentUnit.CurrentJob.SetEquipArtifact(slot1, artifact);
      }
      if (artifactDataList != null && artifactDataList.Count > 0 && unlockAritfactSlot >= artifactDataList.Count)
      {
        for (int index = 0; index < artifactDataList.Count; ++index)
        {
          ArtifactData artifact1 = artifactDataList[index];
          if (artifact1 != null)
          {
            int artifactSlotIndex = JobData.GetArtifactSlotIndex(artifact1.ArtifactParam.type);
            if (artifact != null)
            {
              if (artifactSlotIndex >= 0 && slot1 != -1 && (artifactSlotIndex != slot1 && (ArtifactTypes) (index + 1) != type))
                this.mCurrentUnit.CurrentJob.SetEquipArtifact(artifactSlotIndex, artifact1);
            }
            else if (artifactSlotIndex >= 0 && (ArtifactTypes) (index + 1) != type)
              this.mCurrentUnit.CurrentJob.SetEquipArtifact(artifactSlotIndex, artifact1);
          }
        }
      }
      this.mCurrentUnit.UpdateArtifact(this.mCurrentUnit.JobIndex, true);
      if (Network.Mode == Network.EConnectMode.Online)
        Network.RequestAPI((WebAPI) new ReqArtifactSet(this.mCurrentUnit.UniqueID, this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.Artifacts, new Network.ResponseCallback(this.OnArtifactSetResult)), false);
      else
        this.ShowArtifactSetResult();
      this.SetUnitDirty();
    }

    private void OnArtifactSetResult(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.artifacts, false);
            MonoSingleton<GameManager>.Instance.Player.UpdateArtifactOwner();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          this.RefreshSortedUnits();
          this.ShowArtifactSetResult();
        }
      }
    }

    private void ShowArtifactSetResult()
    {
      this.SpawnStatusChangeEffects();
      this.RebuildUnitData();
      this.ReloadPreviewModels();
      this.SetPreviewVisible(true);
      this.RefreshArtifactSlot();
      this.RefreshBasicParameters(false);
    }

    private void ShowSkinSetResult()
    {
      this.SpawnStatusChangeEffects();
      ArtifactParam[] artifactParamArray = this.mCurrentUnit == null ? (ArtifactParam[]) null : this.mCurrentUnit.GetSelectedSkins();
      this.RebuildUnitData();
      if (this.mCurrentUnit != null && this.mCurrentUnit.Jobs != null)
      {
        for (int jobIndex = 0; jobIndex < this.mCurrentUnit.Jobs.Length; ++jobIndex)
        {
          if (artifactParamArray != null && artifactParamArray[jobIndex] != null)
            this.mCurrentUnit.SetJobSkin(artifactParamArray[jobIndex].iname, jobIndex);
          else
            this.mCurrentUnit.SetJobSkin((string) null, jobIndex);
        }
      }
      this.mCurrentUnit.CalcStatus();
      this.ReloadPreviewModels();
      this.SetPreviewVisible(true);
      this.RefreshBasicParameters(false);
    }

    private void OnJobRankUpClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable())
        return;
      if (Array.FindIndex<EquipData>(this.mCurrentUnit.CurrentEquips, (Predicate<EquipData>) (eq => !eq.IsEquiped())) != -1)
      {
        this.OnEquipAll();
      }
      else
      {
        if (this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) <= this.mCurrentUnit.CurrentJob.Rank)
          return;
        if (this.mCurrentUnit.JobIndex >= this.mCurrentUnit.NumJobsAvailable)
        {
          JobData baseJob = this.mCurrentUnit.GetBaseJob(this.mCurrentUnit.CurrentJob.JobID);
          if (Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, baseJob) < 0)
            return;
          UIUtility.ConfirmBox(string.Format(LocalizedText.Get("sys.CONFIRM_CLASSCHANGE"), baseJob == null ? (object) string.Empty : (object) baseJob.Name, (object) this.mCurrentUnit.CurrentJob.Name), (UIUtility.DialogResultEvent) (go => this.StartJobRankUp()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
        }
        else
          this.StartJobRankUp();
      }
    }

    private void StartJobRankUp()
    {
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
      CriticalSection.Enter(CriticalSections.Network);
      this.StartCoroutine(this.PostJobRankUp());
      MonoSingleton<GameManager>.GetInstanceDirect().Player.OnJobLevelChange(this.mCurrentUnit.UnitID, this.mCurrentUnit.CurrentJob.JobID, this.mCurrentUnit.CurrentJob.Rank, false);
    }

    private void OnJobRankUpResult(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoJobLvUpEquip:
            FlowNode_Network.Failed();
            break;
          case Network.EErrCode.EquipNotComp:
            Network.ResetError();
            Network.RemoveAPI();
            this.mJobRankUpRequestSent = true;
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          this.mLastSyncTime = Time.get_realtimeSinceStartup();
          Network.RemoveAPI();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        this.RefreshSortedUnits();
        this.mJobRankUpRequestSent = true;
      }
    }

    [DebuggerHidden]
    private IEnumerator PostJobRankUp()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostJobRankUp\u003Ec__IteratorE1() { \u003C\u003Ef__this = this };
    }

    private void RebuildUnitData()
    {
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID);
      this.mSelectedJobIndex = unitDataByUniqueId.JobIndex;
      this.mLastSyncTime = unitDataByUniqueId.LastSyncTime;
      string str = this.mCurrentUnit == null ? (string) null : this.mCurrentUnit.CurrentJob.JobID;
      this.mCurrentUnit = new UnitData();
      this.mCurrentUnit.Setup(unitDataByUniqueId);
      this.StartCoroutine(this.LoadAllUnitImage());
      this.mIsSetJobUniqueID = this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex].UniqueID;
      if (string.IsNullOrEmpty(str))
        return;
      for (int jobNo = 0; jobNo < this.mCurrentUnit.Jobs.Length; ++jobNo)
      {
        if (this.mCurrentUnit.Jobs[jobNo].JobID == str)
        {
          this.mCurrentUnit.SetJobIndex(jobNo);
          this.mSelectedJobIndex = jobNo;
          GlobalVars.SelectedJobUniqueID.Set(this.mCurrentUnit.Jobs[jobNo].UniqueID);
          break;
        }
      }
    }

    public void Refresh(long uniqueID, long jobUniqueID = 0, bool immediate = false)
    {
      if (this.mReloading)
        return;
      this.mCurrentUnitID = uniqueID;
      this.mCurrentUnit = (UnitData) null;
      if (this.mStartSelectUnitUniqueID < 0L)
        this.mStartSelectUnitUniqueID = uniqueID;
      this.RefreshSortedUnits();
      GlobalVars.SelectedUnitUniqueID.Set(uniqueID);
      this.RebuildUnitData();
      this.RefreshUnitShiftButton();
      this.mSelectedJobIndex = this.mCurrentUnit.JobIndex;
      ((Component) this.CharaQuestButton).get_gameObject().SetActive(this.mCurrentUnit.IsOpenCharacterQuest());
      ((Component) this.SkinButton).get_gameObject().SetActive(this.mCurrentUnit.IsSkinUnlocked());
      this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
      this.mCurrentJobUniqueID = this.mCurrentUnit.CurrentJob.UniqueID;
      GlobalVars.SelectedJobUniqueID.Set(this.mCurrentJobUniqueID);
      this.mReloading = true;
      this.StartCoroutine(this.RefreshAsync(immediate));
    }

    [DebuggerHidden]
    private IEnumerator RefreshAsync(bool immediate)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CRefreshAsync\u003Ec__IteratorE2() { immediate = immediate, \u003C\u0024\u003Eimmediate = immediate, \u003C\u003Ef__this = this };
    }

    public void RefreshReturningJobState(long jobUniqueID = 0)
    {
      if (jobUniqueID != 0L && this.mIsSetJobUniqueID != 0L)
      {
        for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
        {
          if (this.mCurrentUnit.Jobs[index].UniqueID == jobUniqueID && this.mIsSetJobUniqueID != jobUniqueID)
          {
            this.ChangeJobSlot(index);
            this.RefreshJobIcons();
          }
        }
      }
      this.mSelectedJobIndex = this.mCurrentUnit.JobIndex;
    }

    private void SetPreviewVisible(bool visible)
    {
      if (!Object.op_Inequality((Object) this.mCurrentPreview, (Object) null))
        return;
      this.mDesiredPreviewVisibility = visible;
      if (!visible)
        GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
      else
        this.mUpdatePreviewVisibility = true;
      if (!Object.op_Inequality((Object) this.mPreviewBase, (Object) null) || this.mPreviewBase.get_activeSelf() || !visible)
        return;
      this.mPreviewBase.SetActive(true);
    }

    private void UpdateJobRankUpButtonState()
    {
      if (this.mCurrentUnit == null)
        return;
      bool flag1 = this.mCurrentUnit.CurrentJob.Rank == 0;
      bool flag2 = !flag1 ? this.mCurrentUnit.CheckJobRankUpAllEquip(this.mCurrentUnit.JobIndex) : this.mCurrentUnit.CheckJobUnlock(this.mCurrentUnit.JobIndex, true);
      bool flag3 = this.mCurrentUnit.CurrentJob.GetJobRankCap(this.mCurrentUnit) <= this.mCurrentUnit.CurrentJob.Rank;
      if (Object.op_Inequality((Object) this.mEquipmentPanel.JobRankUpButton, (Object) null))
      {
        ((Component) this.mEquipmentPanel.JobRankUpButton).get_gameObject().SetActive(!flag1 && !flag3);
        ((Selectable) this.mEquipmentPanel.JobRankUpButton).set_interactable(flag2);
        this.mEquipmentPanel.JobRankUpButton.UpdateButtonState();
        if (!string.IsNullOrEmpty(this.JobRankUpButtonHilitBool))
        {
          Animator component = (Animator) ((Component) this.mEquipmentPanel.JobRankUpButton).GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetBool(this.JobRankUpButtonHilitBool, flag2);
            component.Update(0.0f);
          }
        }
      }
      if (Object.op_Inequality((Object) this.mEquipmentPanel.JobUnlockButton, (Object) null))
      {
        ((Component) this.mEquipmentPanel.JobUnlockButton).get_gameObject().SetActive(flag1 && !flag3);
        ((Selectable) this.mEquipmentPanel.JobUnlockButton).set_interactable(flag2);
        this.mEquipmentPanel.JobUnlockButton.UpdateButtonState();
        if (!string.IsNullOrEmpty(this.JobRankUpButtonHilitBool))
        {
          Animator component = (Animator) ((Component) this.mEquipmentPanel.JobUnlockButton).GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetBool(this.JobRankUpButtonHilitBool, flag2);
            component.Update(0.0f);
          }
        }
      }
      this.mIsJobLvMaxAllEquip = false;
      bool flag4 = false;
      if (Object.op_Inequality((Object) this.mEquipmentPanel.AllEquipButton, (Object) null))
      {
        bool flag5 = -1 == Array.FindIndex<EquipData>(this.mCurrentUnit.CurrentEquips, (Predicate<EquipData>) (eq => !eq.IsEquiped()));
        bool equipItemAll = MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateEquipItemAll(this.mCurrentUnit, this.mCurrentUnit.CurrentEquips);
        if (!flag3 || flag5)
        {
          ((Component) this.mEquipmentPanel.AllEquipButton).get_gameObject().SetActive(false);
        }
        else
        {
          this.mIsJobLvMaxAllEquip = true;
          flag4 = true;
          ((Component) this.mEquipmentPanel.AllEquipButton).get_gameObject().SetActive(true);
          ((Selectable) this.mEquipmentPanel.AllEquipButton).set_interactable(equipItemAll);
        }
        this.mEquipmentPanel.AllEquipButton.UpdateButtonState();
        if (!string.IsNullOrEmpty(this.AllEquipButtonHilitBool))
        {
          Animator component = (Animator) ((Component) this.mEquipmentPanel.AllEquipButton).GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.SetBool(this.AllEquipButtonHilitBool, flag2);
            component.Update(0.0f);
          }
        }
      }
      if (Object.op_Inequality((Object) this.mEquipmentPanel.JobRankCapCaution, (Object) null))
        this.mEquipmentPanel.JobRankCapCaution.SetActive(flag3 && !flag4);
      Canvas component1 = (Canvas) ((Component) this.mEquipmentPanel).GetComponent<Canvas>();
      if (!Object.op_Inequality((Object) component1, (Object) null) || !((Behaviour) component1).get_enabled())
        return;
      ((Behaviour) component1).set_enabled(false);
      ((Behaviour) component1).set_enabled(true);
    }

    private void ReloadPreviewModels()
    {
      if (this.mCurrentUnit == null || Object.op_Equality((Object) this.mPreviewParent, (Object) null))
        return;
      GameUtility.DestroyGameObjects<UnitPreview>(this.mPreviewControllers);
      this.mPreviewControllers.Clear();
      this.mPreviewJobNames.Clear();
      this.mCurrentPreview = (UnitPreview) null;
      for (int jobIndex = 0; jobIndex < this.mCurrentUnit.Jobs.Length; ++jobIndex)
      {
        UnitPreview unitPreview = (UnitPreview) null;
        if (this.mCurrentUnit.Jobs[jobIndex] != null && this.mCurrentUnit.Jobs[jobIndex].Param != null)
        {
          GameObject gameObject = new GameObject("Preview", new System.Type[1]{ typeof (UnitPreview) });
          unitPreview = (UnitPreview) gameObject.GetComponent<UnitPreview>();
          unitPreview.DefaultLayer = GameUtility.LayerHidden;
          unitPreview.SetupUnit(this.mCurrentUnit, jobIndex);
          gameObject.get_transform().SetParent(this.mPreviewParent, false);
          if (jobIndex == this.mCurrentUnit.JobIndex)
            this.mCurrentPreview = unitPreview;
        }
        this.mPreviewControllers.Add(unitPreview);
        this.mPreviewJobNames.Add(this.mCurrentUnit.Jobs[jobIndex].JobID);
      }
      if (!Object.op_Equality((Object) this.mCurrentPreview, (Object) null))
        return;
      GameObject gameObject1 = new GameObject("Preview", new System.Type[1]{ typeof (UnitPreview) });
      this.mCurrentPreview = (UnitPreview) gameObject1.GetComponent<UnitPreview>();
      this.mCurrentPreview.DefaultLayer = GameUtility.LayerHidden;
      this.mCurrentPreview.SetupUnit(this.mCurrentUnit, -1);
      gameObject1.get_transform().SetParent(this.mPreviewParent, false);
      this.mPreviewControllers.Add(this.mCurrentPreview);
    }

    [DebuggerHidden]
    private IEnumerator LoadAllUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CLoadAllUnitImage\u003Ec__IteratorE3() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator RefreshUnitImage()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CRefreshUnitImage\u003Ec__IteratorE4() { \u003C\u003Ef__this = this };
    }

    private void RefreshJobInfo()
    {
      if (Object.op_Equality((Object) this.JobInfo, (Object) null))
        return;
      DataSource.Bind<UnitData>(this.JobInfo, this.mCurrentUnit);
      GameParameter.UpdateAll(this.JobInfo);
    }

    private void RefreshJobIcons()
    {
      if (this.mCurrentUnit == null)
        return;
      int index1 = 0;
      int index2 = 0;
      this.mJobSlots.Clear();
      int index3 = -1;
      Toggle.ToggleEvent toggleEvent = new Toggle.ToggleEvent();
      for (int jobNo = 0; jobNo < this.mCurrentUnit.Jobs.Length; ++jobNo)
      {
        JobData job = this.mCurrentUnit.Jobs[jobNo];
        if (job != null && job.Param != null)
        {
          if (this.mCurrentUnit.IsJobAvailable(jobNo))
          {
            this.mJobSlots.Add(this.mJobIcons[index1]);
            ++index1;
          }
          else
          {
            this.mJobSlots.Add(this.mCCIcons[index2]);
            ++index2;
          }
          AnimatedToggle mJobSlot = this.mJobSlots[this.mJobSlots.Count - 1];
          ((Component) mJobSlot).get_gameObject().SetActive(true);
          ((Selectable) mJobSlot).set_interactable(this.mCurrentUnit.CheckJobUnlockable(jobNo));
          Toggle.ToggleEvent onValueChanged = (Toggle.ToggleEvent) mJobSlot.onValueChanged;
          mJobSlot.onValueChanged = (__Null) toggleEvent;
          mJobSlot.set_isOn(jobNo == this.mCurrentUnit.JobIndex);
          mJobSlot.onValueChanged = (__Null) onValueChanged;
          Animator component = (Animator) ((Component) mJobSlot).GetComponent<Animator>();
          component.SetBool(this.JobIconUnlockBool, job.IsActivated);
          if (!string.IsNullOrEmpty(this.JobIconUnlockHilitBool))
          {
            bool flag = job.Rank == 0 && (this.mCurrentUnit.CheckJobUnlock(jobNo, false) || this.mCurrentUnit.CheckJobRankUpAllEquip(jobNo));
            component.SetBool(this.JobIconUnlockHilitBool, flag);
          }
          DataSource.Bind<JobData>(((Component) mJobSlot).get_gameObject(), this.mCurrentUnit.Jobs[jobNo]);
          GameParameter.UpdateAll(((Component) mJobSlot).get_gameObject());
          if (job.UniqueID == this.mCurrentJobUniqueID)
            index3 = jobNo;
        }
      }
      for (int index4 = index2; index4 < this.mCCIcons.Count; ++index4)
        ((Component) this.mCCIcons[index4]).get_gameObject().SetActive(false);
      for (int index4 = index1; index4 < this.mJobIcons.Count; ++index4)
        ((Component) this.mJobIcons[index4]).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.ActiveJobIndicator, (Object) null))
      {
        if (index3 >= 0)
        {
          RectTransform transform = ((Component) this.ActiveJobIndicator).get_transform() as RectTransform;
          transform.set_anchoredPosition(Vector2.get_zero());
          ((Transform) transform).SetParent(((Component) this.mJobSlots[index3]).get_transform(), false);
          ((Component) this.ActiveJobIndicator).get_gameObject().SetActive(true);
        }
        else
          ((Component) this.ActiveJobIndicator).get_gameObject().SetActive(false);
      }
      this.UpdateJobSlotStates(true);
    }

    private void SetActivePreview(string jobID)
    {
      int index = this.mPreviewJobNames.IndexOf(jobID);
      if (index < 0)
        index = 0;
      this.SetActivePreview(index);
    }

    private void SetActivePreview(int index)
    {
      UnitPreview previewController = this.mPreviewControllers[index];
      if (Object.op_Equality((Object) previewController, (Object) this.mCurrentPreview))
        return;
      GameUtility.SetLayer((Component) this.mCurrentPreview, GameUtility.LayerHidden, true);
      GameUtility.SetLayer((Component) previewController, GameUtility.LayerCH, true);
      this.mCurrentPreview = previewController;
    }

    private void UpdateJobSlotStates(bool immediate)
    {
      for (int index = 0; index < this.mJobSlots.Count; ++index)
      {
        this.mJobSlots[index].set_isOn(this.mCurrentUnit.JobIndex == index);
        Animator component = (Animator) ((Component) this.mJobSlots[index]).GetComponent<Animator>();
        int num = 0;
        do
        {
          component.Update(!immediate ? 0.0f : 1f);
          ++num;
        }
        while (component.IsInTransition(0) && num < 10);
      }
    }

    private void UpdateJobChangeButtonState()
    {
      if (!Object.op_Inequality((Object) this.JobChangeButton, (Object) null))
        return;
      this.JobChangeButton.IsOn = this.mCurrentUnit.CurrentJob.UniqueID == this.mCurrentJobUniqueID;
      this.JobChangeButton.UpdateButtonState();
    }

    private void UpdateUnitKakuseiButtonState()
    {
      if (!Object.op_Inequality((Object) this.UnitKakuseiButton, (Object) null))
        return;
      ((Selectable) this.UnitKakuseiButton).set_interactable(this.mCurrentUnit.AwakeLv < this.mCurrentUnit.GetAwakeLevelCap());
      if (string.IsNullOrEmpty(this.UnitKakuseiButtonHilitBool))
        return;
      Animator component = (Animator) ((Component) this.UnitKakuseiButton).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetBool(this.UnitKakuseiButtonHilitBool, this.mCurrentUnit.CheckUnitAwaking());
      component.Update(0.0f);
    }

    private void UpdateUnitEvolutionButtonState(bool immediate = false)
    {
      if (!Object.op_Inequality((Object) this.UnitEvolutionButton, (Object) null))
        return;
      ((Selectable) this.UnitEvolutionButton).set_interactable(this.mCurrentUnit.Rarity < this.mCurrentUnit.GetRarityCap());
      if (immediate)
        this.UnitEvolutionButton.UpdateButtonState();
      if (string.IsNullOrEmpty(this.UnitEvolutionButtonHilitBool))
        return;
      Animator component = (Animator) ((Component) this.UnitEvolutionButton).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetBool(this.UnitEvolutionButtonHilitBool, this.mCurrentUnit.CheckUnitRarityUp());
      component.Update(0.0f);
    }

    private void RefreshBasicParameters(bool bDisableJobMaster = false)
    {
      DataSource.Bind<UnitData>(this.UnitInfo, this.mCurrentUnit);
      int jobNo = -1;
      for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
      {
        if (this.mCurrentUnit.Jobs[index].UniqueID == this.mCurrentJobUniqueID)
        {
          jobNo = index;
          break;
        }
      }
      int num = Array.IndexOf<JobData>(this.mCurrentUnit.Jobs, this.mCurrentUnit.CurrentJob);
      if (jobNo >= 0 && num >= 0 && jobNo != num)
      {
        this.mCurrentUnit.SetJobIndex(jobNo);
        BaseStatus status1 = new BaseStatus(this.mCurrentUnit.Status);
        if (bDisableJobMaster)
        {
          this.mCurrentUnit.CalcStatus(this.mCurrentUnit.Lv, jobNo, ref status1, num);
          status1.CopyTo(this.mCurrentUnit.Status);
        }
        int combination1 = this.mCurrentUnit.GetCombination();
        this.mCurrentUnit.SetJobIndex(num);
        BaseStatus status2 = new BaseStatus(this.mCurrentUnit.Status);
        if (bDisableJobMaster)
        {
          this.mCurrentUnit.CalcStatus(this.mCurrentUnit.Lv, num, ref status2, num);
          status2.CopyTo(this.mCurrentUnit.Status);
        }
        int combination2 = this.mCurrentUnit.GetCombination();
        for (int index = 0; index < (int) StatusParam.MAX_STATUS; ++index)
        {
          if (Object.op_Inequality((Object) this.mStatusParamSlots[index], (Object) null))
          {
            this.SetParamColor((Graphic) this.mStatusParamSlots[index], (int) status2.param[(StatusTypes) index] - (int) status1.param[(StatusTypes) index]);
            this.mStatusParamSlots[index].set_text(this.mCurrentUnit.Status.param[(StatusTypes) index].ToString());
          }
        }
        if (Object.op_Inequality((Object) this.Param_Renkei, (Object) null))
        {
          this.SetParamColor((Graphic) this.Param_Renkei, combination2 - combination1);
          this.Param_Renkei.set_text(this.mCurrentUnit.GetCombination().ToString());
        }
      }
      else
      {
        for (int index = 0; index < (int) StatusParam.MAX_STATUS; ++index)
        {
          if (Object.op_Inequality((Object) this.mStatusParamSlots[index], (Object) null))
          {
            this.SetParamColor((Graphic) this.mStatusParamSlots[index], 0);
            this.mStatusParamSlots[index].set_text(this.mCurrentUnit.Status.param[(StatusTypes) index].ToString());
          }
        }
        if (Object.op_Inequality((Object) this.Param_Renkei, (Object) null))
        {
          this.SetParamColor((Graphic) this.Param_Renkei, 0);
          this.Param_Renkei.set_text(this.mCurrentUnit.GetCombination().ToString());
        }
      }
      GameParameter.UpdateAll(this.UnitInfo);
      if (!Object.op_Inequality((Object) this.LeaderSkillInfo, (Object) null))
        return;
      this.LeaderSkillInfo.SetSlotData<SkillData>(this.mCurrentUnit.LeaderSkill);
    }

    private void SetParamColor(Graphic g, int delta)
    {
      if (delta < 0)
        g.set_color(Color32.op_Implicit(this.ParamDownColor));
      else if (delta > 0)
        g.set_color(Color32.op_Implicit(this.ParamUpColor));
      else
        g.set_color(Color32.op_Implicit(this.DefaultParamColor));
    }

    private void RefreshEquipments(int slot = -1)
    {
      if (Object.op_Inequality((Object) this.mActivePanel, (Object) this.mEquipmentPanel))
      {
        this.mEquipmentPanelDirty = true;
      }
      else
      {
        this.mEquipmentPanelDirty = false;
        if (this.mCurrentUnit == null)
          return;
        EquipData[] rankupEquips = this.mCurrentUnit.GetRankupEquips(this.mCurrentUnit.JobIndex);
        UnitEquipmentSlotEvents[] equipmentSlots = this.mEquipmentPanel.EquipmentSlots;
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        for (int index = 0; index < equipmentSlots.Length; ++index)
        {
          if (!Object.op_Equality((Object) equipmentSlots[index], (Object) null) && (slot == -1 || index == slot))
          {
            if (!rankupEquips[index].IsValid())
            {
              ((Component) equipmentSlots[index]).get_gameObject().SetActive(false);
            }
            else
            {
              ((Component) equipmentSlots[index]).get_gameObject().SetActive(true);
              ItemParam itemParam = rankupEquips[index].ItemParam;
              DataSource.Bind<ItemParam>(((Component) equipmentSlots[index]).get_gameObject(), itemParam);
              DataSource.Bind<EquipData>(((Component) equipmentSlots[index]).get_gameObject(), rankupEquips[index]);
              GameParameter.UpdateAll(((Component) equipmentSlots[index]).get_gameObject());
              UnitEquipmentSlotEvents.SlotStateTypes slotStateTypes = !rankupEquips[index].IsEquiped() ? (!player.HasItem(itemParam.iname) ? (!player.CheckEnableCreateItem(itemParam, true, 1) ? UnitEquipmentSlotEvents.SlotStateTypes.Empty : ((int) itemParam.equipLv <= this.mCurrentUnit.Lv ? UnitEquipmentSlotEvents.SlotStateTypes.EnableCraft : UnitEquipmentSlotEvents.SlotStateTypes.EnableCraftNeedMoreLevel)) : ((int) itemParam.equipLv <= this.mCurrentUnit.Lv ? UnitEquipmentSlotEvents.SlotStateTypes.HasEquipment : UnitEquipmentSlotEvents.SlotStateTypes.NeedMoreLevel)) : UnitEquipmentSlotEvents.SlotStateTypes.Equipped;
              equipmentSlots[index].StateType = slotStateTypes;
            }
          }
        }
        this.RefreshArtifactSlot();
      }
    }

    public bool CheckEquipArtifactSlot(int slot, JobData job, UnitData unit)
    {
      if (job == null || unit == null)
        return false;
      int awakeLv = unit.AwakeLv;
      FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
      if (fixParam == null || fixParam.EquipArtifactSlotUnlock == null || (fixParam.EquipArtifactSlotUnlock.Length <= 0 || fixParam.EquipArtifactSlotUnlock.Length < slot))
        return false;
      return (int) fixParam.EquipArtifactSlotUnlock[slot] <= awakeLv;
    }

    public void RefreshArtifactSlot()
    {
      long[] numArray = new long[3];
      int index1 = 0;
      JobData currentJob = this.mCurrentUnit.CurrentJob;
      for (int index2 = 0; index2 < currentJob.Artifacts.Length; ++index2)
      {
        if (currentJob.Artifacts[index2] != 0L)
        {
          numArray[index1] = currentJob.Artifacts[index2];
          ++index1;
        }
      }
      if (Object.op_Inequality((Object) this.mEquipmentPanel, (Object) null) && Object.op_Inequality((Object) this.mEquipmentPanel.ArtifactSlot, (Object) null))
      {
        if (this.mCurrentUnit.CurrentJob.Rank > 0)
        {
          ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(numArray[0]);
          this.mEquipmentPanel.ArtifactSlot.SetLocked(!this.CheckEquipArtifactSlot(0, currentJob, this.mCurrentUnit));
          this.mEquipmentPanel.ArtifactSlot.SetSlotData<ArtifactData>(artifactByUniqueId);
        }
        else
        {
          this.mEquipmentPanel.ArtifactSlot.SetLocked(true);
          this.mEquipmentPanel.ArtifactSlot.SetSlotData<ArtifactData>((ArtifactData) null);
        }
      }
      if (Object.op_Inequality((Object) this.mEquipmentPanel, (Object) null) && Object.op_Inequality((Object) this.mEquipmentPanel.ArtifactSlot2, (Object) null))
      {
        if (this.mCurrentUnit.CurrentJob.Rank > 0)
        {
          ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(numArray[1]);
          this.mEquipmentPanel.ArtifactSlot2.SetLocked(!this.CheckEquipArtifactSlot(1, currentJob, this.mCurrentUnit));
          this.mEquipmentPanel.ArtifactSlot2.SetSlotData<ArtifactData>(artifactByUniqueId);
        }
        else
        {
          this.mEquipmentPanel.ArtifactSlot2.SetLocked(true);
          this.mEquipmentPanel.ArtifactSlot2.SetSlotData<ArtifactData>((ArtifactData) null);
        }
      }
      if (!Object.op_Inequality((Object) this.mEquipmentPanel, (Object) null) || !Object.op_Inequality((Object) this.mEquipmentPanel.ArtifactSlot3, (Object) null))
        return;
      if (this.mCurrentUnit.CurrentJob.Rank > 0)
      {
        ArtifactData artifactByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindArtifactByUniqueID(numArray[2]);
        this.mEquipmentPanel.ArtifactSlot3.SetLocked(!this.CheckEquipArtifactSlot(2, currentJob, this.mCurrentUnit));
        this.mEquipmentPanel.ArtifactSlot3.SetSlotData<ArtifactData>(artifactByUniqueId);
      }
      else
      {
        this.mEquipmentPanel.ArtifactSlot3.SetLocked(true);
        this.mEquipmentPanel.ArtifactSlot3.SetSlotData<ArtifactData>((ArtifactData) null);
      }
    }

    public void OnJobSlotClick(AnimatedToggle toggle)
    {
      this.ChangeJobSlot(this.mJobSlots.IndexOf(toggle));
    }

    private void ChangeJobSlot(int index)
    {
      if (this.mCurrentUnit == null || index < 0 || this.mCurrentUnit.JobIndex == index)
        return;
      if (!this.mCurrentUnit.CheckJobUnlockable(index))
      {
        if (Object.op_Equality((Object) this.Prefab_LockedJobTooltip, (Object) null))
          return;
        JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(index);
        if (jobSetParam == null)
          return;
        if (Object.op_Inequality((Object) this.mJobUnlockTooltip, (Object) null))
        {
          this.mJobUnlockTooltip.Close();
          this.mJobUnlockTooltip = (Tooltip) null;
          if (this.mJobUnlockTooltipIndex == index)
            return;
        }
        Tooltip.SetTooltipPosition(((Component) this.mJobSlots[index]).get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
        Tooltip tooltip = (Tooltip) Object.Instantiate<Tooltip>((M0) this.Prefab_LockedJobTooltip);
        DataSource.Bind<JobSetParam>(((Component) tooltip).get_gameObject(), jobSetParam);
        JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(jobSetParam.job);
        DataSource.Bind<JobParam>(((Component) tooltip).get_gameObject(), jobParam);
        this.mJobUnlockTooltip = tooltip;
        this.mJobUnlockTooltipIndex = index;
      }
      else
      {
        this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
        this.mCurrentUnit.SetJobIndex(index);
        this.RefreshBasicParameters(false);
        this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
        this.SetActivePreview(this.mCurrentUnit.CurrentJob.JobID);
        this.mSelectedJobIndex = index;
        this.RefreshJobInfo();
        this.RefreshEquipments(-1);
        this.UpdateJobChangeButtonState();
        this.UpdateJobRankUpButtonState();
        this.RefreshAbilitySlotButtonState();
        if (this.mCurrentUnit.CurrentJob.Rank == 0)
        {
          if (Object.op_Equality((Object) this.mActivePanel, (Object) this.mAbilitySlotPanel))
            this.OnTabChange((SRPG_Button) this.Tab_Equipments);
        }
        else
          this.RefreshAbilitySlots();
        this.FadeUnitImage(0.0f, 0.0f, 0.0f);
        this.StartCoroutine(this.RefreshUnitImage());
        this.FadeUnitImage(0.0f, 1f, this.BGUnitImageFadeTime);
        GlobalVars.SelectedJobUniqueID.Set(this.mCurrentUnit.CurrentJob.UniqueID);
      }
    }

    private void RefreshAbilitySlotButtonState()
    {
      if (!Object.op_Inequality((Object) this.Tab_AbilitySlot, (Object) null))
        return;
      ((Selectable) this.Tab_AbilitySlot).set_interactable(this.mCurrentUnit.CurrentJob.Rank > 0);
    }

    private void Req_UpdateEquippedAbility()
    {
      bool flag = false;
      for (int index = 0; index < this.mCurrentUnit.CurrentJob.AbilitySlots.Length; ++index)
      {
        if (this.mCurrentUnit.CurrentJob.AbilitySlots[index] != this.mOriginalAbilities[index])
        {
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        this.FinishKyokaRequest();
      }
      else
      {
        long uniqueId = this.mCurrentUnit.CurrentJob.UniqueID;
        this.mOriginalAbilities = (long[]) this.mCurrentUnit.CurrentJob.AbilitySlots.Clone();
        this.RequestAPI((WebAPI) new ReqJobAbility(uniqueId, this.mCurrentUnit.CurrentJob.AbilitySlots, new Network.ResponseCallback(this.Res_UpdateEquippedAbility)));
        this.SetUnitDirty();
      }
    }

    private void Res_UpdateEquippedAbility(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoAbilitySetAbility:
          case Network.EErrCode.NoJobSetAbility:
          case Network.EErrCode.UnsetAbility:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.get_realtimeSinceStartup();
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.FinishKyokaRequest();
      }
    }

    public UnitEquipmentWindow EquipmentWindow
    {
      set
      {
        this.mEquipmentWindow = value;
        if (!Object.op_Inequality((Object) this.mEquipmentWindow, (Object) null))
          return;
        this.mEquipmentWindow.OnEquip = new UnitEquipmentWindow.EquipEvent(this.OnEquip);
      }
      get
      {
        return this.mEquipmentWindow;
      }
    }

    public UnitKakeraWindow KakeraWindow
    {
      set
      {
        this.mKakeraWindow = value;
        if (!Object.op_Inequality((Object) this.mKakeraWindow, (Object) null))
          return;
        this.mKakeraWindow.OnKakuseiAccept = new UnitKakeraWindow.KakuseiWindowEvent(this.OnUnitKakusei);
      }
      get
      {
        return this.mKakeraWindow;
      }
    }

    private void OnWindowStateChange(GameObject go, bool visible)
    {
      if (visible || !this.mCloseRequested)
        return;
      this.mCloseRequested = false;
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
      this.SetPreviewVisible(false);
      if (Object.op_Inequality((Object) this.mPreviewBase, (Object) null))
        this.mPreviewBase.SetActive(false);
      if (this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null))
        this.StartCoroutine(this.WaitForKyokaRequestAndInvokeUserClose());
      else
        this.InvokeUserClose();
    }

    private void InvokeUserClose()
    {
      if (this.mStartSelectUnitUniqueID > 0L)
      {
        long num = -1;
        if (this.mStartSelectUnitUniqueID != this.mCurrentUnitID)
        {
          num = this.mCurrentUnitID;
          this.SetUnitDirty();
        }
        FlowNode_Variable.Set("LAST_SELECTED_UNITID", num <= 0L ? string.Empty : num.ToString());
      }
      this.mStartSelectUnitUniqueID = -1L;
      if (this.OnUserClose == null)
        return;
      this.OnUserClose();
    }

    [DebuggerHidden]
    private IEnumerator WaitForKyokaRequestAndInvokeUserClose()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CWaitForKyokaRequestAndInvokeUserClose\u003Ec__IteratorE5() { \u003C\u003Ef__this = this };
    }

    private void RequestAPI(WebAPI api)
    {
      if (this.mAppPausing)
      {
        int num = (int) Network.RequestAPIImmediate(api, true);
      }
      else
        Network.RequestAPI(api, false);
    }

    private void RefreshSortedUnits()
    {
      this.SortedUnits = MonoSingleton<GameManager>.Instance.Player.GetSortedUnits("UNITLIST", true);
      if (!PlayerPrefs.HasKey("UNITLIST&"))
        return;
      UnitListV2.FilterUnitsRevert(this.SortedUnits, (List<int>) null, PlayerPrefs.GetString("UNITLIST&").Split('|'));
    }

    private void RefreshUnitShiftButton()
    {
      int count = this.SortedUnits.Count;
      if (this.mCurrentUnit != null && this.SortedUnits.Find((Predicate<UnitData>) (unit => unit.UniqueID == this.mCurrentUnit.UniqueID)) == null)
        ++count;
      bool flag = count >= 2;
      ((Selectable) this.PrevUnitButton).set_interactable(flag);
      ((Selectable) this.NextUnitButton).set_interactable(flag);
    }

    private void RefreshAbilityList()
    {
      if (Object.op_Inequality((Object) this.mActivePanel, (Object) this.mAbilityListPanel))
      {
        this.mAbilityListDirty = true;
      }
      else
      {
        this.mAbilityListDirty = false;
        if (Object.op_Equality((Object) this.mAbilityListPanel, (Object) null) || Object.op_Equality((Object) this.mAbilityListPanel.AbilityList, (Object) null))
          return;
        this.mAbilityListPanel.AbilityList.Unit = this.mCurrentUnit;
        this.mAbilityListPanel.AbilityList.DisplayAll();
      }
    }

    private void RefreshAbilitySlots()
    {
      if (Object.op_Inequality((Object) this.mActivePanel, (Object) this.mAbilitySlotPanel))
      {
        this.mAbilitySlotDirty = true;
      }
      else
      {
        this.mAbilitySlotDirty = false;
        if (Object.op_Equality((Object) this.mAbilitySlotPanel, (Object) null) || Object.op_Equality((Object) this.mAbilitySlotPanel.AbilitySlots, (Object) null))
          return;
        this.mAbilitySlotPanel.AbilitySlots.Unit = this.mCurrentUnit;
        this.mAbilitySlotPanel.AbilitySlots.DisplaySlots();
      }
    }

    private void OnAbilitySlotSelect(int slotIndex)
    {
      if (Object.op_Equality((Object) this.mAbilityPicker, (Object) null))
        return;
      this.ExecQueuedKyokaRequest(new UnitEnhanceV3.DeferredJob(this.Req_UpdateEquippedAbility));
      this.mAbilityPicker.UnitData = this.mCurrentUnit;
      this.mAbilityPicker.AbilitySlot = slotIndex;
      this.mAbilityPicker.Refresh();
      ((WindowController) ((Component) this.mAbilityPicker).GetComponent<WindowController>()).Open();
    }

    private void OnSlotAbilitySelect(AbilityData ability, GameObject itemGO)
    {
      int abilitySlot = this.mAbilityPicker.AbilitySlot;
      ((WindowController) ((Component) this.mAbilityPicker).GetComponent<WindowController>()).Close();
      this.BeginStatusChangeCheck();
      this.mCurrentUnit.SetEquipAbility(this.mCurrentUnit.JobIndex, abilitySlot, ability == null ? 0L : ability.UniqueID);
      MonoSingleton<GameManager>.Instance.Player.OnChangeAbilitySet(this.mCurrentUnit.UnitID);
      this.SpawnStatusChangeEffects();
      this.RefreshBasicParameters(false);
      this.RefreshAbilitySlots();
      this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.Req_UpdateEquippedAbility), 0.0f);
    }

    private void OnRankUpButtonPressHold(AbilityData abilityData, GameObject gobj)
    {
      if (!Object.op_Inequality((Object) gobj, (Object) null))
        return;
      UnitAbilityListItemEvents component = (UnitAbilityListItemEvents) gobj.GetComponent<UnitAbilityListItemEvents>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      this.mCurrentAbilityRankUpHold = component.ItemTouchController;
    }

    private void OnAbilityRankUpSet(AbilityData abilityData, GameObject itemGO)
    {
      if (this.ExecQueuedKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitAbilityRankUpRequest)))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!instance.Player.CheckRankUpAbility(abilityData))
        return;
      this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
      if (!instance.Player.RankUpAbility(abilityData))
        return;
      int rank = abilityData.Rank;
      MonoSingleton<GameManager>.Instance.NotifyAbilityRankUpCountChanged();
      MonoSingleton<GameManager>.Instance.Player.OnAbilityPowerUp(this.mCurrentUnit.UnitID, abilityData.AbilityID, rank, false);
      if (Object.op_Inequality((Object) itemGO, (Object) null))
      {
        if (!string.IsNullOrEmpty(this.AbilityRankUpTrigger))
        {
          Animator component = (Animator) itemGO.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.SetTrigger(this.AbilityRankUpTrigger);
        }
        if (Object.op_Inequality((Object) this.AbilityRankUpEffect, (Object) null))
          UIUtility.SpawnParticle(this.AbilityRankUpEffect, itemGO.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
      }
      this.mAbilityPicker.ListBody.UpdateItem(abilityData);
      this.mAbilityListPanel.AbilityList.UpdateItem(abilityData);
      this.mAbilitySlotPanel.AbilitySlots.UpdateItem(abilityData);
      this.mRankedUpAbilities.Add(abilityData.UniqueID);
      this.PlayUnitVoice("chara_0017");
    }

    private void OnAbilityRankUpExec(AbilityData abilityData, GameObject go)
    {
      if (abilityData == null || this.mRankedUpAbilities == null || this.mRankedUpAbilities.Count <= 0)
        return;
      this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitAbilityRankUpRequest), 0.0f);
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
      List<string> learningSkillList2 = this.mCurrentUnit.GetAbilityData(abilityData.UniqueID).GetLearningSkillList2(abilityData.Rank);
      if (learningSkillList2 != null && learningSkillList2.Count > 0)
      {
        List<SkillParam> newSkills = new List<SkillParam>(learningSkillList2.Count);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < learningSkillList2.Count; ++index)
        {
          try
          {
            SkillParam skillParam = instance.GetSkillParam(learningSkillList2[index]);
            newSkills.Add(skillParam);
          }
          catch (Exception ex)
          {
          }
        }
        this.mKeepWindowLocked = true;
        this.StartCoroutine(this.PostAbilityRankUp(newSkills));
      }
      else
      {
        ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
        this.mKeepWindowLocked = true;
        this.StartCoroutine(this.AbilityRankUpSkillUnlockEffect());
      }
    }

    private void OnAbilityRankUp(AbilityData abilityData, GameObject itemGO)
    {
      if (this.ExecQueuedKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitAbilityRankUpRequest)) || !MonoSingleton<GameManager>.Instance.Player.CheckRankUpAbility(abilityData))
        return;
      this.mChQuestProg = (UnitData.CharacterQuestUnlockProgress) null;
      if (this.mCurrentUnit.IsOpenCharacterQuest())
        this.mChQuestProg = this.mCurrentUnit.SaveUnlockProgress();
      this.mCurrentUnitUnlocks = this.mCurrentUnit.UnlockedSkillIds();
      if (!MonoSingleton<GameManager>.Instance.Player.RankUpAbility(abilityData))
        return;
      int rank = abilityData.Rank;
      MonoSingleton<GameManager>.Instance.NotifyAbilityRankUpCountChanged();
      MonoSingleton<GameManager>.Instance.Player.OnAbilityPowerUp(this.mCurrentUnit.UnitID, abilityData.AbilityID, rank, false);
      if (Object.op_Inequality((Object) itemGO, (Object) null))
      {
        if (!string.IsNullOrEmpty(this.AbilityRankUpTrigger))
        {
          Animator component = (Animator) itemGO.GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.SetTrigger(this.AbilityRankUpTrigger);
        }
        if (Object.op_Inequality((Object) this.AbilityRankUpEffect, (Object) null))
          UIUtility.SpawnParticle(this.AbilityRankUpEffect, itemGO.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
      }
      this.mAbilityPicker.ListBody.UpdateItem(abilityData);
      this.mAbilityListPanel.AbilityList.UpdateItem(abilityData);
      this.mAbilitySlotPanel.AbilitySlots.UpdateItem(abilityData);
      this.mRankedUpAbilities.Add(abilityData.UniqueID);
      this.QueueKyokaRequest(new UnitEnhanceV3.DeferredJob(this.SubmitAbilityRankUpRequest), 0.0f);
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.PlayUnitVoice("chara_0017");
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
      GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
      List<string> learningSkillList = abilityData.GetLearningSkillList(rank);
      if (learningSkillList != null && learningSkillList.Count > 0)
      {
        List<SkillParam> newSkills = new List<SkillParam>(learningSkillList.Count);
        GameManager instance = MonoSingleton<GameManager>.Instance;
        for (int index = 0; index < learningSkillList.Count; ++index)
        {
          try
          {
            SkillParam skillParam = instance.GetSkillParam(learningSkillList[index]);
            newSkills.Add(skillParam);
          }
          catch (Exception ex)
          {
          }
        }
        this.mKeepWindowLocked = true;
        this.StartCoroutine(this.PostAbilityRankUp(newSkills));
      }
      else
      {
        ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
        this.mKeepWindowLocked = true;
        this.StartCoroutine(this.AbilityRankUpSkillUnlockEffect());
      }
      AnalyticsManager.TrackCurrencyUse(AnalyticsManager.CurrencyType.Zeni, AnalyticsManager.CurrencySubType.FREE, (long) MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityNextGold(abilityData.Rank), "Rank Up", (Dictionary<string, object>) null);
    }

    [DebuggerHidden]
    private IEnumerator AbilityRankUpSkillUnlockEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CAbilityRankUpSkillUnlockEffect\u003Ec__IteratorE6() { \u003C\u003Ef__this = this };
    }

    private void SubmitAbilityRankUpRequest()
    {
      this.mKeepWindowLocked = false;
      Dictionary<long, int> abilities = new Dictionary<long, int>();
      for (int index1 = 0; index1 < this.mRankedUpAbilities.Count; ++index1)
      {
        long mRankedUpAbility = this.mRankedUpAbilities[index1];
        if (abilities.ContainsKey(mRankedUpAbility))
        {
          Dictionary<long, int> dictionary;
          long index2;
          (dictionary = abilities)[index2 = mRankedUpAbility] = dictionary[index2] + 1;
        }
        else
          abilities[mRankedUpAbility] = 1;
      }
      this.mRankedUpAbilities.Clear();
      this.mAbilityRankUpRequestSent = false;
      string trophy_progs;
      string bingo_progs;
      MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
      this.RequestAPI((WebAPI) new ReqAbilityRankUp(abilities, new Network.ResponseCallback(this.OnSubmitAbilityRankUpResult), trophy_progs, bingo_progs));
      this.SetUnitDirty();
    }

    private void OnSubmitAbilityRankUpResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.AbilityMaterialShort)
          FlowNode_Network.Back();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.get_realtimeSinceStartup();
          Network.RemoveAPI();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        this.FinishKyokaRequest();
        this.mAbilityRankUpRequestSent = true;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
      }
    }

    private void OnAbilityRankUpCountPreReset(int unused)
    {
      if (this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null))
        ;
    }

    [DebuggerHidden]
    private IEnumerator PostAbilityRankUp(List<SkillParam> newSkills)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostAbilityRankUp\u003Ec__IteratorE7() { newSkills = newSkills, \u003C\u0024\u003EnewSkills = newSkills, \u003C\u003Ef__this = this };
    }

    private void OnApplicationPause(bool pausing)
    {
      if (!pausing)
        return;
      this.mAppPausing = true;
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.mAppPausing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
      if (focus)
        return;
      this.OnApplicationPause(true);
    }

    private void OnKakuseiButtonClick(SRPG_Button button)
    {
      this.OpenKakeraWindow();
    }

    public void OpenKakeraWindow()
    {
      if (Object.op_Equality((Object) this.mKakeraWindow, (Object) null))
        return;
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.mUnlockJobParam = (JobParam) null;
      if (this.mCurrentUnit.Jobs != null)
      {
        int rarity = this.mCurrentUnit.Rarity;
        int awakeLv = this.mCurrentUnit.AwakeLv;
        int awakeLevelCap = this.mCurrentUnit.GetAwakeLevelCap();
        if (awakeLv < awakeLevelCap)
        {
          MasterParam masterParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
          for (int jobNo = 0; jobNo < this.mCurrentUnit.Jobs.Length; ++jobNo)
          {
            if (!this.mCurrentUnit.Jobs[jobNo].IsActivated)
            {
              JobSetParam jobSetParam = this.mCurrentUnit.GetJobSetParam(jobNo);
              if (jobSetParam != null && rarity >= jobSetParam.lock_rarity)
              {
                JobSetParam[] changeJobSetParam = masterParam.GetClassChangeJobSetParam(this.mCurrentUnit.UnitParam.iname);
                if (changeJobSetParam != null && changeJobSetParam.Length > 0)
                {
                  bool flag = false;
                  for (int index = 0; index < changeJobSetParam.Length; ++index)
                  {
                    if (changeJobSetParam[index].iname == jobSetParam.iname)
                    {
                      flag = true;
                      break;
                    }
                  }
                  if (flag)
                    continue;
                }
                if (jobSetParam.lock_jobs != null)
                {
                  for (int index = 0; index < jobSetParam.lock_jobs.Length; ++index)
                  {
                    if (jobSetParam.lock_jobs[index] != null)
                    {
                      string iname = jobSetParam.lock_jobs[index].iname;
                      if (jobSetParam.lock_jobs[index].lv <= this.mCurrentUnit.GetJobLevelByJobID(iname))
                        ;
                    }
                  }
                }
                if (awakeLv + 1 == jobSetParam.lock_awakelv)
                {
                  this.mUnlockJobParam = this.mCurrentUnit.Jobs[jobNo].Param;
                  break;
                }
              }
            }
          }
        }
      }
      WindowController.OpenIfAvailable((Component) this.mKakeraWindow);
      this.mKakeraWindow.Refresh(this.mCurrentUnit, this.mUnlockJobParam);
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
      ((WindowController) ((Component) this.mKakeraWindow).GetComponent<WindowController>()).OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnKakuseiCancel);
    }

    public void OpenProfile()
    {
      if (!Object.op_Equality((Object) this.mUnitProfileWindow, (Object) null) || this.mProfileWindowLoadRequest != null)
        return;
      this.StartCoroutine(this._OpenProfileWindow());
    }

    [DebuggerHidden]
    private IEnumerator _OpenProfileWindow()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003C_OpenProfileWindow\u003Ec__IteratorE8() { \u003C\u003Ef__this = this };
    }

    private void OnUnitKakusei()
    {
      if (this.IsKyokaRequestQueued)
        return;
      if (!this.mCurrentUnit.CheckUnitAwaking())
      {
        Debug.LogError((object) "なんかエラーだす");
      }
      else
      {
        ((WindowController) ((Component) this.mKakeraWindow).GetComponent<WindowController>()).Close();
        ((WindowController) ((Component) this.mKakeraWindow).GetComponent<WindowController>()).OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnKakuseiAccept);
        this.mKakuseiRequestSent = false;
        if (Network.Mode == Network.EConnectMode.Online)
        {
          MonoSingleton<GameManager>.Instance.Player.OnLimitBreak(this.mCurrentUnit.UnitID);
          string trophy_progs;
          string bingo_progs;
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
          this.RequestAPI((WebAPI) new ReqUnitPlus(this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.OnUnitKakuseiResult), trophy_progs, bingo_progs));
        }
        else
        {
          this.mKakuseiRequestSent = true;
          MonoSingleton<GameManager>.GetInstanceDirect().Player.OnLimitBreak(this.mCurrentUnit.UnitID);
        }
        this.SetUnitDirty();
      }
    }

    private void OnUnitKakuseiResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.PlusMaterialShot)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.get_realtimeSinceStartup();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.RefreshSortedUnits();
        this.mKakuseiRequestSent = true;
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
      }
    }

    private void OnKakuseiAccept(GameObject go, bool visible)
    {
      if (visible)
        return;
      ((WindowController) ((Component) this.mKakeraWindow).GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      this.StartCoroutine(this.PostUnitKakusei());
    }

    [DebuggerHidden]
    private IEnumerator PostUnitKakusei()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostUnitKakusei\u003Ec__IteratorE9() { \u003C\u003Ef__this = this };
    }

    private void OnKakuseiCancel(GameObject go, bool visible)
    {
      if (visible)
        return;
      ((WindowController) go.GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(true);
    }

    private void OnEvolutionButtonClick(SRPG_Button button)
    {
      if (this.mCurrentUnit.GetRarityCap() <= this.mCurrentUnit.Rarity)
      {
        UIUtility.NegativeSystemMessage(string.Empty, LocalizedText.Get("sys.DISABLE_EVOLUTION"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
        this.mEvolutionWindow.Unit = this.mCurrentUnit;
        this.mEvolutionWindow.Refresh();
        ((WindowController) ((Component) this.mEvolutionWindow).GetComponent<WindowController>()).Open();
        ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
        ((WindowController) ((Component) this.mEvolutionWindow).GetComponent<WindowController>()).OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnEvolutionCancel);
      }
    }

    private void OnUnitEvolve()
    {
      if (this.IsKyokaRequestQueued)
        return;
      if (!this.mCurrentUnit.CheckUnitRarityUp())
      {
        Debug.LogError((object) "なんかエラーだす");
      }
      else
      {
        ((WindowController) ((Component) this.mEvolutionWindow).GetComponent<WindowController>()).Close();
        ((WindowController) ((Component) this.mEvolutionWindow).GetComponent<WindowController>()).OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnEvolutionStart);
        this.mEvolutionRequestSent = false;
        if (Network.Mode == Network.EConnectMode.Online)
        {
          MonoSingleton<GameManager>.Instance.Player.OnEvolutionChange(this.mCurrentUnit.UnitID, this.mCurrentUnit.Rarity);
          string trophy_progs;
          string bingo_progs;
          MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecStart(out trophy_progs, out bingo_progs);
          this.RequestAPI((WebAPI) new ReqUnitRare(this.mCurrentUnit.UniqueID, new Network.ResponseCallback(this.OnUnitEvolutionResult), trophy_progs, bingo_progs));
        }
        else
        {
          this.mEvolutionRequestSent = true;
          MonoSingleton<GameManager>.GetInstanceDirect().Player.OnEvolutionChange(this.mCurrentUnit.UnitID, this.mCurrentUnit.Rarity);
        }
        this.SetUnitDirty();
      }
    }

    private void OnEvolutionStart(GameObject go, bool visible)
    {
      if (visible)
        return;
      ((WindowController) go.GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      this.StartCoroutine(this.PostUnitEvolution());
    }

    private void OnEvolutionCancel(GameObject go, bool visible)
    {
      if (visible)
        return;
      ((WindowController) go.GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(true);
    }

    private void OnUnitEvolutionResult(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.RareMaterialShort)
          FlowNode_Network.Failed();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          this.mLastSyncTime = Time.get_realtimeSinceStartup();
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
          FlowNode_Network.Retry();
          return;
        }
        Network.RemoveAPI();
        this.mEvolutionRequestSent = true;
        this.RefreshSortedUnits();
        this.RefreshJobInfo();
        this.RefreshJobIcons();
        this.RefreshEquipments(-1);
        this.RefreshBasicParameters(false);
        this.UpdateJobRankUpButtonState();
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_GOLD_STATUS.ToString(), (object) null);
        GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) null);
        MonoSingleton<GameManager>.Instance.ServerSyncTrophyExecEnd(www);
      }
    }

    [DebuggerHidden]
    private IEnumerator PostUnitEvolution()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CPostUnitEvolution\u003Ec__IteratorEA() { \u003C\u003Ef__this = this };
    }

    private void RefreshExpInfo()
    {
      if (!Object.op_Inequality((Object) this.UnitExpInfo, (Object) null))
        return;
      GameParameter.UpdateAll(this.UnitExpInfo);
    }

    private void OnShiftUnit(SRPG_Button button)
    {
      if (!((Selectable) button).get_interactable())
        return;
      int num = !Object.op_Equality((Object) button, (Object) this.NextUnitButton) ? -1 : 1;
      int count = this.Units.Count;
      if (count == 0)
        return;
      long uniqueId = this.Units[(this.CurrentUnitIndex + num + count) % count].UniqueID;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
      this.mKeepWindowLocked = true;
      this.ExecQueuedKyokaRequest((UnitEnhanceV3.DeferredJob) null);
      this.StartCoroutine(this.ShiftUnitAsync(uniqueId));
    }

    [DebuggerHidden]
    private IEnumerator ShiftUnitAsync(long unitUniqueID)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CShiftUnitAsync\u003Ec__IteratorEB() { unitUniqueID = unitUniqueID, \u003C\u0024\u003EunitUniqueID = unitUniqueID, \u003C\u003Ef__this = this };
    }

    private int CurrentUnitIndex
    {
      get
      {
        return this.Units.IndexOf(MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnitID));
      }
    }

    private List<UnitData> Units
    {
      get
      {
        return this.SortedUnits;
      }
    }

    private UnitEnhancePanel mActivePanel
    {
      get
      {
        return this.mTabPages[this.mActiveTabIndex];
      }
    }

    private void CreateOverlay()
    {
      if (!Object.op_Equality((Object) this.mOverlayCanvas, (Object) null))
        return;
      this.mOverlayCanvas = UIUtility.PushCanvas(false, -1);
    }

    private void AttachOverlay(GameObject go)
    {
      this.CreateOverlay();
      go.get_transform().SetParent(((Component) this.mOverlayCanvas).get_transform(), false);
    }

    private void DestroyOverlay()
    {
      if (!Object.op_Inequality((Object) this.mOverlayCanvas, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mOverlayCanvas).get_gameObject());
    }

    public bool IsLoading
    {
      get
      {
        return this.mReloading;
      }
    }

    private void SetUnitDirty()
    {
      if (!this.mDirtyUnits.Contains(this.mCurrentUnitID))
        this.mDirtyUnits.Add(this.mCurrentUnitID);
      this.mLeftTime = Time.get_realtimeSinceStartup();
    }

    public long[] GetDirtyUnits()
    {
      return this.mDirtyUnits.ToArray();
    }

    public void ClearDirtyUnits()
    {
      this.mDirtyUnits.Clear();
    }

    private void OpenLeaderSkillDetail(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable() || !Object.op_Inequality((Object) this.LeaderSkillDetailButton, (Object) null) || (!Object.op_Inequality((Object) this.Prefab_LeaderSkillDetail, (Object) null) || !Object.op_Equality((Object) this.mLeaderSkillDetail, (Object) null)))
        return;
      this.mLeaderSkillDetail = (GameObject) Object.Instantiate<GameObject>((M0) this.Prefab_LeaderSkillDetail);
      DataSource.Bind<UnitData>(this.mLeaderSkillDetail, this.mCurrentUnit);
    }

    private void ShowParamTooltip(PointerEventData eventData)
    {
      if (Object.op_Equality((Object) this.Prefab_ParamTooltip, (Object) null))
        return;
      RaycastResult pointerCurrentRaycast = eventData.get_pointerCurrentRaycast();
      // ISSUE: explicit reference operation
      GameObject gameObject = ((RaycastResult) @pointerCurrentRaycast).get_gameObject();
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      this.mParamTooltipTarget = gameObject;
      Tooltip.SetTooltipPosition((RectTransform) gameObject.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f));
      if (Object.op_Equality((Object) this.mParamTooltip, (Object) null))
        this.mParamTooltip = (Tooltip) Object.Instantiate<Tooltip>((M0) this.Prefab_ParamTooltip);
      else
        this.mParamTooltip.ResetPosition();
      if (!Object.op_Inequality((Object) this.mParamTooltip.TooltipText, (Object) null))
        return;
      this.mParamTooltip.TooltipText.set_text(LocalizedText.Get("sys.UE_HELP_" + ((Object) gameObject).get_name().ToUpper()));
    }

    private void AddParamTooltip(GameObject go)
    {
      if (!Object.op_Inequality((Object) go, (Object) null))
        return;
      UIEventListener uiEventListener = go.RequireComponent<UIEventListener>();
      if (uiEventListener.onMove == null)
        uiEventListener.onPointerEnter = new UIEventListener.PointerEvent(this.ShowParamTooltip);
      else
        uiEventListener.onPointerEnter += new UIEventListener.PointerEvent(this.ShowParamTooltip);
    }

    private void OnCharacterQuestOpen(SRPG_Button button)
    {
      this.OnCharacterQuestOpen(button, false);
    }

    private void OnCharacterQuestOpen(SRPG_Button button, bool isRestore)
    {
      if (Object.op_Equality((Object) this.Prefab_CharaQuestListWindow, (Object) null))
        return;
      UnitCharacterQuestWindow characterQuestWindow = (UnitCharacterQuestWindow) Object.Instantiate<UnitCharacterQuestWindow>((M0) this.Prefab_CharaQuestListWindow);
      if (Object.op_Equality((Object) characterQuestWindow, (Object) null))
        return;
      characterQuestWindow.IsRestore = isRestore;
      GlobalVars.SelectedUnitUniqueID.Set(this.mCurrentUnit.UniqueID);
      this.mCharacterQuestWindow = characterQuestWindow;
      this.mCharacterQuestWindow.CurrentUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mCurrentUnit.UniqueID);
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
      ((WindowController) ((Component) this.mCharacterQuestWindow).GetComponent<WindowController>()).OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnCharacterQuestSelectCancel);
      WindowController.OpenIfAvailable((Component) this.mCharacterQuestWindow);
    }

    private void OnCharacterQuestSelectCancel(GameObject go, bool visible)
    {
      if (visible)
        return;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(true);
      if (Object.op_Equality((Object) this.mCharacterQuestWindow, (Object) null) || visible)
        return;
      ((WindowController) ((Component) this.mCharacterQuestWindow).GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      Object.Destroy((Object) ((Component) this.mCharacterQuestWindow).get_gameObject());
      this.mCharacterQuestWindow = (UnitCharacterQuestWindow) null;
    }

    public void OnCharacterQuestRestore()
    {
      this.OnCharacterQuestOpen(this.CharaQuestButton, true);
    }

    private void OpenCharacterQuestPopupInternal()
    {
      MonoSingleton<GameManager>.Instance.AddCharacterQuestPopup(this.mCurrentUnit);
      MonoSingleton<GameManager>.Instance.ShowCharacterQuestPopup(GameSettings.Instance.CharacterQuest_Unlock);
    }

    public bool HasStarted
    {
      get
      {
        return this.mStarted;
      }
    }

    private void OnSkinSelectOpen(SRPG_Button button)
    {
      if (this.SkinSelectTemplate == null || this.mCurrentUnit == null)
        return;
      this.StartCoroutine(this.OnSkinSelectOpenAsync());
    }

    private void OnSkinSetResult(WWWResult www)
    {
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(true);
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          try
          {
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
            this.mLastSyncTime = Time.get_realtimeSinceStartup();
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
            FlowNode_Network.Retry();
            return;
          }
          Network.RemoveAPI();
          this.RefreshSortedUnits();
          this.SetUnitDirty();
        }
      }
    }

    [DebuggerHidden]
    private IEnumerator OnSkinSelectOpenAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003COnSkinSelectOpenAsync\u003Ec__IteratorEC() { \u003C\u003Ef__this = this };
    }

    private void OnSkinSelect(ArtifactParam artifact)
    {
      this.ShowSkinParamChange(artifact, true);
    }

    private void OnSkinDecide(ArtifactParam artifact)
    {
      this.ShowSkinParamChange(artifact, false);
    }

    private void OnSkinDecideAll(ArtifactParam artifact)
    {
      this.UpdataSkinParamChange(artifact, true);
    }

    private void OnSkinRemoved()
    {
      this.UpdataSkinParamChange((ArtifactParam) null, true);
    }

    private void OnSkinRemovedAll()
    {
      this.UpdataSkinParamChange((ArtifactParam) null, true);
    }

    private void UpdataSkinParamChange(ArtifactParam artifact, bool isAll)
    {
      if (Network.Mode != Network.EConnectMode.Online)
        return;
      Dictionary<long, string> sets = new Dictionary<long, string>();
      if (isAll)
      {
        for (int index = 0; index < this.mCurrentUnit.Jobs.Length; ++index)
        {
          if (this.mCurrentUnit.Jobs[index] != null && this.mCurrentUnit.Jobs[index].IsActivated)
            sets[this.mCurrentUnit.Jobs[index].UniqueID] = artifact == null ? string.Empty : artifact.iname;
        }
      }
      else if (this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex] != null)
        sets[this.mCurrentUnit.Jobs[this.mCurrentUnit.JobIndex].UniqueID] = artifact == null || artifact.iname == null ? string.Empty : artifact.iname;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).SetCollision(false);
      Network.RequestAPI((WebAPI) new ReqSkinSet(sets, new Network.ResponseCallback(this.OnSkinSetResult)), false);
    }

    private void ShowSkinParamChange(ArtifactParam artifact, bool isAll)
    {
      if (isAll)
      {
        for (int jobIndex = 0; jobIndex < this.mCurrentUnit.Jobs.Length; ++jobIndex)
          this.mCurrentUnit.SetJobSkin(artifact == null ? (string) null : artifact.iname, jobIndex);
      }
      else
        this.mCurrentUnit.SetJobSkin(artifact == null ? (string) null : artifact.iname, this.mCurrentUnit.JobIndex);
      this.BeginStatusChangeCheck();
      this.mCurrentUnit.UpdateArtifact(this.mCurrentUnit.JobIndex, true);
      this.ShowSkinSetResult();
      this.FadeUnitImage(0.0f, 0.0f, 0.0f);
      this.StartCoroutine(this.RefreshUnitImage());
      this.FadeUnitImage(0.0f, 1f, 1f);
    }

    [DebuggerHidden]
    private IEnumerator ShowUnlockSkillEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitEnhanceV3.\u003CShowUnlockSkillEffect\u003Ec__IteratorED() { \u003C\u003Ef__this = this };
    }

    public void Activated(int pinID)
    {
      if (pinID != 100)
        return;
      this.mCloseRequested = true;
      ((WindowController) ((Component) this).GetComponent<WindowController>()).Close();
    }

    private class ExpItemTouchController : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
    {
      public UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown OnPointerDownFunc;
      public UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown OnPointerUpFunc;
      public UnitEnhanceV3.ExpItemTouchController.DelegateUseItem UseItemFunc;
      public float UseItemSpan;
      public float HoldDuration;
      public bool Holding;
      private int UseNum;
      private int NextSettingIndex;
      private Vector2 mDragStartPos;

      public ExpItemTouchController()
      {
        base.\u002Ector();
      }

      public void OnPointerDown(PointerEventData eventData)
      {
        if (this.OnPointerDownFunc == null)
          return;
        this.OnPointerDownFunc(this);
        this.Holding = true;
        this.mDragStartPos = eventData.get_position();
      }

      public void OnPointerUp()
      {
        if (this.OnPointerUpFunc != null)
          this.OnPointerUpFunc(this);
        this.StatusReset();
      }

      public void OnDestroy()
      {
        this.StatusReset();
        if (this.OnPointerDownFunc != null)
          this.OnPointerDownFunc = (UnitEnhanceV3.ExpItemTouchController.DelegateOnPointerDown) null;
        if (this.UseItemFunc == null)
          return;
        this.UseItemFunc = (UnitEnhanceV3.ExpItemTouchController.DelegateUseItem) null;
      }

      public void UpdateTimer(float deltaTime)
      {
        if (this.UseItemFunc == null)
          return;
        if (this.Holding && !Input.GetMouseButton(0))
        {
          if ((double) this.HoldDuration < (double) this.UseItemSpan && this.UseNum < 1)
          {
            this.UseItemFunc(((Component) this).get_gameObject());
            ++this.UseNum;
          }
          this.OnPointerUp();
        }
        else
        {
          GameSettings instance = GameSettings.Instance;
          float num = (float) (instance.HoldMargin * instance.HoldMargin);
          Vector2 vector2 = Vector2.op_Subtraction(this.mDragStartPos, Vector2.op_Implicit(Input.get_mousePosition()));
          // ISSUE: explicit reference operation
          if ((double) this.HoldDuration < (double) this.UseItemSpan && this.UseNum < 1 && (double) ((Vector2) @vector2).get_sqrMagnitude() > (double) num)
          {
            this.StatusReset();
          }
          else
          {
            if (!this.Holding)
              return;
            this.HoldDuration += Time.get_unscaledDeltaTime();
            if ((double) this.HoldDuration < (double) this.UseItemSpan)
              return;
            this.HoldDuration -= this.UseItemSpan;
            this.UseItemFunc(((Component) this).get_gameObject());
            ++this.UseNum;
            GameSettings.HoldCountSettings[] holdCount = instance.HoldCount;
            if (holdCount.Length <= this.NextSettingIndex || holdCount[this.NextSettingIndex].Count >= this.UseNum)
              return;
            this.UseItemSpan = holdCount[this.NextSettingIndex].UseSpan;
            ++this.NextSettingIndex;
          }
        }
      }

      public void StatusReset()
      {
        this.HoldDuration = 0.0f;
        this.Holding = false;
        this.UseNum = 0;
        this.NextSettingIndex = 0;
        // ISSUE: explicit reference operation
        ((Vector2) @this.mDragStartPos).Set(0.0f, 0.0f);
      }

      public delegate void DelegateOnPointerDown(UnitEnhanceV3.ExpItemTouchController controller);

      public delegate void DelegateOnPointerUp(UnitEnhanceV3.ExpItemTouchController controller);

      public delegate void DelegateUseItem(GameObject listItem);
    }

    private delegate void DeferredJob();

    public delegate void CloseEvent();
  }
}
