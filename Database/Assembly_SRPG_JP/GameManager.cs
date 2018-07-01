// Decompiled with JetBrains decompiler
// Type: SRPG.GameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using gu3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Manager/Game")]
  public class GameManager : MonoSingleton<GameManager>
  {
    private static readonly int SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY = 41213;
    private PlayerData mPlayer = new PlayerData();
    private OptionData mOption = new OptionData();
    private MasterParam mMasterParam = new MasterParam();
    private List<SectionParam> mWorlds = new List<SectionParam>();
    private List<ChapterParam> mAreas = new List<ChapterParam>();
    private List<QuestParam> mQuests = new List<QuestParam>();
    private Dictionary<string, QuestParam> mQuestsDict = new Dictionary<string, QuestParam>();
    private List<ObjectiveParam> mObjectives = new List<ObjectiveParam>();
    private List<ObjectiveParam> mTowerObjectives = new List<ObjectiveParam>();
    private List<MagnificationParam> mMagnifications = new List<MagnificationParam>();
    private List<QuestCondParam> mConditions = new List<QuestCondParam>();
    private List<QuestPartyParam> mParties = new List<QuestPartyParam>();
    private List<QuestCampaignParentParam> mCampaignParents = new List<QuestCampaignParentParam>();
    private List<QuestCampaignChildParam> mCampaignChildren = new List<QuestCampaignChildParam>();
    private List<QuestCampaignTrust> mCampaignTrust = new List<QuestCampaignTrust>();
    private List<QuestParam> mTowerBaseQuests = new List<QuestParam>();
    private List<TowerFloorParam> mTowerFloors = new List<TowerFloorParam>();
    private Dictionary<string, TowerFloorParam> mTowerFloorsDict = new Dictionary<string, TowerFloorParam>();
    private List<TowerRewardParam> mTowerRewards = new List<TowerRewardParam>();
    private List<TowerRoundRewardParam> mTowerRoundRewards = new List<TowerRoundRewardParam>();
    private List<TowerParam> mTowers = new List<TowerParam>();
    private TowerResuponse mTowerResuponse = new TowerResuponse();
    private List<ArenaPlayer> mArenaPlayers = new List<ArenaPlayer>();
    private List<ArenaPlayer>[] mArenaRanking = new List<ArenaPlayer>[2]
    {
      new List<ArenaPlayer>(),
      new List<ArenaPlayer>()
    };
    private List<ArenaPlayerHistory> mArenaHistory = new List<ArenaPlayerHistory>();
    private List<GachaParam> mGachas = new List<GachaParam>();
    private List<ChatChannelMasterParam> mChatChannelMasters = new List<ChatChannelMasterParam>();
    private List<AchievementParam> mAchievement = new List<AchievementParam>();
    private List<MultiRanking> mMultiUnitRank = new List<MultiRanking>();
    private Dictionary<string, RankingData> mUnitRanking = new Dictionary<string, RankingData>();
    private List<VersusTowerParam> mVersusTowerFloor = new List<VersusTowerParam>();
    private VsTowerMatchEndParam mVersusEndParam = new VsTowerMatchEndParam();
    private List<VersusScheduleParam> mVersusScheduleParam = new List<VersusScheduleParam>();
    private List<VersusCoinParam> mVersusCoinParam = new List<VersusCoinParam>();
    private List<VersusFriendScore> mVersusFriendScore = new List<VersusFriendScore>();
    private List<RankingQuestParam> mRankingQuestParam = new List<RankingQuestParam>();
    private List<RankingQuestRewardParam> mRankingQuestRewardParam = new List<RankingQuestRewardParam>();
    private List<RankingQuestScheduleParam> mRankingQuestScheduleParam = new List<RankingQuestScheduleParam>();
    private List<RankingQuestParam> mAvailableRankingQuesstParams = new List<RankingQuestParam>();
    private VersusAudienceManager mAudienceManager = new VersusAudienceManager();
    private List<MultiTowerFloorParam> mMultiTowerFloor = new List<MultiTowerFloorParam>();
    private List<MultiTowerRewardParam> mMultiTowerRewards = new List<MultiTowerRewardParam>();
    private MultiTowerRoundParam mMultiTowerRound = new MultiTowerRoundParam();
    private List<VersusFirstWinBonusParam> mVersusFirstWinBonus = new List<VersusFirstWinBonusParam>();
    private List<VersusStreakWinScheduleParam> mVersusStreakSchedule = new List<VersusStreakWinScheduleParam>();
    private List<VersusStreakWinBonusParam> mVersusStreakWinBonus = new List<VersusStreakWinBonusParam>();
    private List<VersusRuleParam> mVersusRule = new List<VersusRuleParam>();
    private List<VersusCoinCampParam> mVersusCoinCamp = new List<VersusCoinCampParam>();
    private List<VersusEnableTimeParam> mVersusEnableTime = new List<VersusEnableTimeParam>();
    private List<VersusRankParam> mVersusRank = new List<VersusRankParam>();
    private List<VersusRankClassParam> mVersusRankClass = new List<VersusRankClassParam>();
    private List<VersusRankRankingRewardParam> mVersusRankRankingReward = new List<VersusRankRankingRewardParam>();
    private List<VersusRankRewardParam> mVersusRankReward = new List<VersusRankRewardParam>();
    private List<VersusRankMissionScheduleParam> mVersusRankMissionSchedule = new List<VersusRankMissionScheduleParam>();
    private List<VersusRankMissionParam> mVersusRankMission = new List<VersusRankMissionParam>();
    private List<GuerrillaShopAdventQuestParam> mGuerrillaShopAdventQuest = new List<GuerrillaShopAdventQuestParam>();
    private List<GuerrillaShopScheduleParam> mGuerrillaShopScheduleParam = new List<GuerrillaShopScheduleParam>();
    private List<VersusDraftUnitParam> mVersusDraftUnit = new List<VersusDraftUnitParam>();
    private List<string> mTips = new List<string>();
    private List<SRPG.VersusCpuData> mVersusCpuData = new List<SRPG.VersusCpuData>();
    public GameManager.DayChangeEvent OnDayChange = (GameManager.DayChangeEvent) (() => {});
    public GameManager.StaminaChangeEvent OnStaminaChange = (GameManager.StaminaChangeEvent) (() => {});
    public GameManager.RankUpCountChangeEvent OnAbilityRankUpCountChange = (GameManager.RankUpCountChangeEvent) (n => {});
    public GameManager.RankUpCountChangeEvent OnAbilityRankUpCountPreReset = (GameManager.RankUpCountChangeEvent) (n => {});
    public GameManager.PlayerLvChangeEvent OnPlayerLvChange = (GameManager.PlayerLvChangeEvent) (() => {});
    public GameManager.PlayerCurrencyChangeEvent OnPlayerCurrencyChange = (GameManager.PlayerCurrencyChangeEvent) (() => {});
    private List<Coroutine> mImportantJobs = new List<Coroutine>();
    public GameManager.SceneChangeEvent OnSceneChange = (GameManager.SceneChangeEvent) (() => true);
    private List<GameManager.TextureRequest> mTextureRequests = new List<GameManager.TextureRequest>();
    private List<AssetList.Item> mTutorialDLAssets = new List<AssetList.Item>(1024);
    public UpdateTrophyLock update_trophy_lock = new UpdateTrophyLock();
    public UpdateTrophyInterval update_trophy_interval = new UpdateTrophyInterval();
    private const string MasterParamPath = "Data/MasterParam";
    private const string QuestParamPath = "Data/QuestParam";
    private const float DesiredFrameTime = 0.026f;
    private const float MaxFrameTime = 0.03f;
    private const float MinAnimationFrameTime = 0.001f;
    private const float MaxAnimationFrameTime = 0.006f;
    private bool mRelogin;
    private List<UnitParam> mBadgeUnitUnlocks;
    private GameObject mNotifyList;
    private List<SRPG.MapEffectParam> mMapEffectParam;
    private List<WeatherSetParam> mWeatherSetParam;
    private int mVersusNowStreakWinCnt;
    private int mVersusBestStreakWinCnt;
    private int mVersusAllPriodStreakWinCount;
    private int mVersusBestAllPriodStreakWinCount;
    private bool mVersusFirstWinRewardRecived;
    private long mVersusFreeExpiredTime;
    private long mVersusFreeNextTime;
    private VersusDraftType mVersusDraftType;
    private bool mSelectedVersusCpuBattle;
    public ReqFgGAuth.eAuthStatus AuthStatus;
    public string FgGAuthHost;
    private string mReloadMasterDataError;
    private MyGUID mMyGuid;
    public GameManager.BadgeTypes IsBusyUpdateBadges;
    public GameManager.BadgeTypes BadgeFlags;
    private DateTime mLastUpdateTime;
    private int mLastStamina;
    private long mLastGold;
    private int mLastAbilityRankUpCount;
    public bool EnableAnimationFrameSkipping;
    private bool mHasLoggedIn;
    private static bool mUpscaleMode;
    private GameObject mBuyCoinWindow;
    private GameManager.BuyCoinEvent mOnBuyCoinEnd;
    private GameManager.BuyCoinEvent mOnBuyCoinCancel;
    private Coroutine mImportantJobCoroutine;
    private int mTutorialStep;
    private bool mScannedTutorialAssets;
    private Coroutine mWaitDownloadThread;
    private string mSavedUpdateTrophyListString;
    private List<TrophyState> mUpdateTrophyList;
    private List<TrophyState> mUpdateChallengeList;
    private List<TrophyState> mUpdateTrophyAward;
    private bool is_start_server_sync_trophy_exec;
    private List<TrophyState> mServerSyncTrophyList;
    private List<TrophyState> mServerSyncChallengeList;
    private List<TrophyState> mServerSyncTrophyAward;
    private List<UnitData> mCharacterQuestUnits;
    private long mLimitedShopEndAt;
    private JSON_ShopListArray.Shops[] mLimitedShopList;
    private bool mIsLimitedShopOpen;
    private GameManager.VersusRange[] mFreeMatchRange;

    public bool IsRelogin
    {
      get
      {
        return this.mRelogin;
      }
      set
      {
        this.mRelogin = value;
      }
    }

    private string AgreedVer
    {
      get
      {
        return (string) MonoSingleton<UserInfoManager>.Instance.GetValue("tou_agreed_ver");
      }
      set
      {
        MonoSingleton<UserInfoManager>.Instance.SetValue("tou_agreed_ver", (object) value, true);
      }
    }

    public bool VersusTowerMatchBegin { get; set; }

    public bool VersusTowerMatchReceipt { get; set; }

    public string VersusTowerMatchName { get; set; }

    public long VersusTowerMatchEndAt { get; set; }

    public int VersusCoinRemainCnt { get; set; }

    public string VersusLastUid { get; set; }

    public bool RankMatchBegin { get; set; }

    public int RankMatchScheduleId { get; set; }

    public ReqRankMatchStatus.RankingStatus RankMatchRankingStatus { get; set; }

    public long RankMatchExpiredTime { get; set; }

    public long RankMatchNextTime { get; set; }

    public string[] RankMatchMatchedEnemies { get; set; }

    public string DigestHash { get; set; }

    public string PrevCheckHash { get; set; }

    public string AlterCheckHash { get; set; }

    public List<SRPG.MapEffectParam> MapEffectParam
    {
      get
      {
        return this.mMapEffectParam;
      }
    }

    public List<RankingQuestParam> RankingQuestParams
    {
      get
      {
        return this.mRankingQuestParam;
      }
    }

    public List<RankingQuestRewardParam> RankingQuestRewardParams
    {
      get
      {
        return this.mRankingQuestRewardParam;
      }
    }

    public List<RankingQuestScheduleParam> RankingQuestScheduleParams
    {
      get
      {
        return this.mRankingQuestScheduleParam;
      }
    }

    public List<RankingQuestParam> AvailableRankingQuesstParams
    {
      get
      {
        return this.mAvailableRankingQuesstParams;
      }
    }

    public bool AudienceMode { get; set; }

    public MyPhoton.MyRoom AudienceRoom { get; set; }

    public VersusAudienceManager AudienceManager
    {
      get
      {
        return this.mAudienceManager;
      }
    }

    public List<string> Tips
    {
      get
      {
        return this.mTips;
      }
      set
      {
        this.mTips = value;
      }
    }

    public int VS_StreakWinCnt_Now
    {
      get
      {
        return this.mVersusNowStreakWinCnt;
      }
      set
      {
        this.mVersusNowStreakWinCnt = value;
      }
    }

    public int VS_StreakWinCnt_Best
    {
      get
      {
        return this.mVersusBestStreakWinCnt;
      }
      set
      {
        this.mVersusBestStreakWinCnt = value;
      }
    }

    public int VS_StreakWinCnt_NowAllPriod
    {
      get
      {
        return this.mVersusAllPriodStreakWinCount;
      }
      set
      {
        this.mVersusAllPriodStreakWinCount = value;
      }
    }

    public int VS_StreakWinCnt_BestAllPriod
    {
      get
      {
        return this.mVersusBestAllPriodStreakWinCount;
      }
      set
      {
        this.mVersusBestAllPriodStreakWinCount = value;
      }
    }

    public bool IsVSFirstWinRewardRecived
    {
      get
      {
        return this.mVersusFirstWinRewardRecived;
      }
      set
      {
        this.mVersusFirstWinRewardRecived = value;
      }
    }

    public long VSFreeExpiredTime
    {
      get
      {
        return this.mVersusFreeExpiredTime;
      }
      set
      {
        this.mVersusFreeExpiredTime = value;
      }
    }

    public long VSFreeNextTime
    {
      get
      {
        return this.mVersusFreeNextTime;
      }
      set
      {
        this.mVersusFreeNextTime = value;
      }
    }

    public long mVersusEnableId { get; set; }

    public VersusDraftType VSDraftType
    {
      get
      {
        return this.mVersusDraftType;
      }
      set
      {
        this.mVersusDraftType = value;
      }
    }

    public long VSDraftId { get; set; }

    public string VSDraftQuestId { get; set; }

    public bool IsVSCpuBattle
    {
      get
      {
        return this.mSelectedVersusCpuBattle;
      }
      set
      {
        this.mSelectedVersusCpuBattle = value;
      }
    }

    public List<SRPG.VersusCpuData> VersusCpuData
    {
      get
      {
        return this.mVersusCpuData;
      }
    }

    public VersusEnableTimeParam GetVersusEnableTime(long schedule_id)
    {
      return this.mVersusEnableTime.Find((Predicate<VersusEnableTimeParam>) (VersusET => (long) VersusET.ScheduleId == schedule_id));
    }

    public VersusRankParam GetVersusRankParam(int schedule_id)
    {
      return this.mVersusRank.Find((Predicate<VersusRankParam>) (vr => vr.Id == schedule_id));
    }

    public List<VersusEnableTimeScheduleParam> GetVersusRankMapSchedule(int schedule_id)
    {
      VersusEnableTimeParam versusEnableTimeParam = this.mVersusEnableTime.Find((Predicate<VersusEnableTimeParam>) (VersusET => VersusET.ScheduleId == schedule_id));
      if (versusEnableTimeParam == null)
        return new List<VersusEnableTimeScheduleParam>();
      return versusEnableTimeParam.Schedule;
    }

    public int GetNextVersusRankClass(int schedule_id, RankMatchClass current_class, int point)
    {
      VersusRankClassParam versusRankClassParam = this.mVersusRankClass.Find((Predicate<VersusRankClassParam>) (vrc =>
      {
        if (vrc.ScheduleId == schedule_id)
          return vrc.Class == current_class;
        return false;
      }));
      if (versusRankClassParam == null)
        return 0;
      return versusRankClassParam.UpPoint - point;
    }

    public VersusRankClassParam GetVersusRankClass(int schedule_id, RankMatchClass current_class)
    {
      return this.mVersusRankClass.Find((Predicate<VersusRankClassParam>) (vrc =>
      {
        if (vrc.ScheduleId == schedule_id)
          return vrc.Class == current_class;
        return false;
      })) ?? (VersusRankClassParam) null;
    }

    public int GetMaxBattlePoint(int schedule_id)
    {
      VersusRankParam versusRankParam = this.mVersusRank.Find((Predicate<VersusRankParam>) (vr => vr.Id == schedule_id));
      if (versusRankParam == null)
        return 0;
      return versusRankParam.Limit;
    }

    public List<VersusRankClassParam> GetVersusRankClassList(int schedule_id)
    {
      List<VersusRankClassParam> all = this.mVersusRankClass.FindAll((Predicate<VersusRankClassParam>) (vrc => vrc.ScheduleId == schedule_id));
      all.Sort((Comparison<VersusRankClassParam>) ((a, b) => a.Class - b.Class));
      return all;
    }

    public List<VersusRankRankingRewardParam> GetVersusRankRankingRewardList(int schedule_id)
    {
      List<VersusRankRankingRewardParam> all = this.mVersusRankRankingReward.FindAll((Predicate<VersusRankRankingRewardParam>) (vrrr => vrrr.ScheduleId == schedule_id));
      all.Sort((Comparison<VersusRankRankingRewardParam>) ((a, b) => a.RankBegin - b.RankBegin));
      return all;
    }

    public List<VersusRankReward> GetVersusRankClassRewardList(string reward_id)
    {
      VersusRankRewardParam versusRankRewardParam = this.mVersusRankReward.Find((Predicate<VersusRankRewardParam>) (reward => reward.RewardId == reward_id));
      if (versusRankRewardParam == null)
        return new List<VersusRankReward>();
      return versusRankRewardParam.RewardList;
    }

    public List<VersusRankMissionParam> GetVersusRankMissionList(int schedule_id)
    {
      List<VersusRankMissionParam> mission_list = new List<VersusRankMissionParam>();
      List<VersusRankMissionScheduleParam> all = this.mVersusRankMissionSchedule.FindAll((Predicate<VersusRankMissionScheduleParam>) (vrms => vrms.ScheduleId == schedule_id));
      if (all == null)
        return mission_list;
      all.ForEach((Action<VersusRankMissionScheduleParam>) (schedule =>
      {
        VersusRankMissionParam rankMissionParam = this.mVersusRankMission.Find((Predicate<VersusRankMissionParam>) (vrm => vrm.IName == schedule.IName));
        if (rankMissionParam == null)
          return;
        mission_list.Add(rankMissionParam);
      }));
      mission_list.Sort((Comparison<VersusRankMissionParam>) ((a, b) => a.IName.CompareTo(b.IName)));
      return mission_list;
    }

    public List<VersusDraftUnitParam> GetVersusDraftUnits(long schedule_id)
    {
      return this.mVersusDraftUnit.FindAll((Predicate<VersusDraftUnitParam>) (vdu => vdu.Id == schedule_id));
    }

    protected override void Release()
    {
      GlobalEvent.RemoveListener("TOU_AGREE", new GlobalEvent.Delegate(this.OnAgreeTermsOfUse));
      SceneAwakeObserver.ClearListeners();
      CriticalSection.ForceReset();
      ButtonEvent.Reset();
      CharacterDB.UnloadAll();
      AssetManager.UnloadAll();
      AssetDownloader.Reset();
      SkillSequence.UnloadAll();
      HomeWindow.SetRestorePoint(RestorePoints.Home);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<MySound>.GetInstanceDirect(), (UnityEngine.Object) null))
        MonoSingleton<MySound>.Instance.StopBGM();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNotifyList, (UnityEngine.Object) null))
      {
        GameUtility.DestroyGameObject(this.mNotifyList);
        this.mNotifyList = (GameObject) null;
      }
      this.mScannedTutorialAssets = false;
    }

    public void ResetData()
    {
      this.Release();
    }

    public void InitNotifyList(GameObject notifyListTemplate)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) notifyListTemplate, (UnityEngine.Object) null) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mNotifyList, (UnityEngine.Object) null))
        return;
      this.mNotifyList = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) notifyListTemplate);
    }

    public SectionParam FindWorld(string iname)
    {
      for (int index = this.mWorlds.Count - 1; index >= 0; --index)
      {
        if (this.mWorlds[index].iname == iname)
          return this.mWorlds[index];
      }
      return (SectionParam) null;
    }

    public ChapterParam FindArea(string iname)
    {
      for (int index = this.mAreas.Count - 1; index >= 0; --index)
      {
        if (this.mAreas[index].iname == iname)
          return this.mAreas[index];
      }
      return (ChapterParam) null;
    }

    public QuestParam FindQuest(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (QuestParam) null;
      QuestParam questParam;
      if (this.mQuestsDict.TryGetValue(iname, out questParam))
        return questParam;
      return this.FindTowerFloor(iname)?.Clone((QuestParam) null, true);
    }

    public QuestParam FindQuest(QuestTypes type)
    {
      for (int index = this.mQuests.Count - 1; index >= 0; --index)
      {
        if (this.mQuests[index].type == type)
          return this.mQuests[index];
      }
      return (QuestParam) null;
    }

    public ObjectiveParam FindObjective(string iname)
    {
      for (int index = this.mObjectives.Count - 1; index >= 0; --index)
      {
        if (this.mObjectives[index].iname == iname)
          return this.mObjectives[index];
      }
      return (ObjectiveParam) null;
    }

    public ObjectiveParam FindTowerObjective(string iname)
    {
      for (int index = this.mTowerObjectives.Count - 1; index >= 0; --index)
      {
        if (this.mTowerObjectives[index].iname == iname)
          return this.mTowerObjectives[index];
      }
      return (ObjectiveParam) null;
    }

    public MagnificationParam FindMagnification(string iname)
    {
      for (int index = this.mMagnifications.Count - 1; index >= 0; --index)
      {
        if (this.mMagnifications[index].iname == iname)
          return this.mMagnifications[index];
      }
      return (MagnificationParam) null;
    }

    public QuestCondParam FindQuestCond(string iname)
    {
      if (this.mConditions != null)
      {
        for (int index = this.mConditions.Count - 1; index >= 0; --index)
        {
          if (this.mConditions[index].iname == iname)
            return this.mConditions[index];
        }
      }
      return (QuestCondParam) null;
    }

    public QuestPartyParam FindQuestParty(string iname)
    {
      if (this.mParties != null)
      {
        for (int index = this.mParties.Count - 1; index >= 0; --index)
        {
          if (this.mParties[index].iname == iname)
            return this.mParties[index];
        }
      }
      return (QuestPartyParam) null;
    }

    public QuestCampaignData[] FindQuestCampaigns(string[] inames)
    {
      List<QuestCampaignData> questCampaignDataList = new List<QuestCampaignData>();
      if (this.mCampaignChildren != null && inames != null && inames.Length > 0)
      {
        using (List<QuestCampaignChildParam>.Enumerator enumerator = this.mCampaignChildren.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            QuestCampaignChildParam current = enumerator.Current;
            foreach (string iname in inames)
            {
              if (!(iname != current.iname))
              {
                QuestCampaignData[] questCampaignDataArray = current.MakeData();
                foreach (QuestCampaignData questCampaignData1 in questCampaignDataArray)
                {
                  QuestCampaignData d = questCampaignData1;
                  QuestCampaignData questCampaignData2 = questCampaignDataList.Find((Predicate<QuestCampaignData>) (value => value.type == d.type));
                  if (questCampaignData2 == null || questCampaignData2.type == QuestCampaignValueTypes.ExpUnit && !(questCampaignData2.unit == d.unit))
                    questCampaignDataList.Add(d);
                }
                break;
              }
            }
          }
        }
      }
      return questCampaignDataList.ToArray();
    }

    public QuestCampaignData[] FindQuestCampaigns(QuestParam questParam)
    {
      List<QuestCampaignData> questCampaignDataList = new List<QuestCampaignData>();
      DateTime serverTime = TimeManager.ServerTime;
      if (this.mCampaignChildren != null)
      {
        for (int index = this.mCampaignChildren.Count - 1; index >= 0; --index)
        {
          QuestCampaignChildParam mCampaignChild = this.mCampaignChildren[index];
          if (mCampaignChild.IsValidQuest(questParam) && mCampaignChild.IsAvailablePeriod(serverTime))
          {
            QuestCampaignData[] questCampaignDataArray = mCampaignChild.MakeData();
            foreach (QuestCampaignData questCampaignData1 in questCampaignDataArray)
            {
              QuestCampaignData d = questCampaignData1;
              QuestCampaignData questCampaignData2 = questCampaignDataList.Find((Predicate<QuestCampaignData>) (value => value.type == d.type));
              if (questCampaignData2 == null || questCampaignData2.type == QuestCampaignValueTypes.ExpUnit && !(questCampaignData2.unit == d.unit))
                questCampaignDataList.Add(d);
            }
          }
        }
      }
      return questCampaignDataList.ToArray();
    }

    public QuestParam FindBaseQuest(QuestTypes questType, string iname)
    {
      if (questType == QuestTypes.Tower)
      {
        for (int index = this.mTowerBaseQuests.Count - 1; index >= 0; --index)
        {
          if (this.mTowerBaseQuests[index].iname == iname)
            return this.mTowerBaseQuests[index];
        }
      }
      return (QuestParam) null;
    }

    public TowerFloorParam FindTowerFloor(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (TowerFloorParam) null;
      TowerFloorParam towerFloorParam;
      if (this.mTowerFloorsDict.TryGetValue(iname, out towerFloorParam))
        return towerFloorParam;
      return (TowerFloorParam) null;
    }

    public TowerFloorParam FindFirstTowerFloor(string towerID)
    {
      if (this.mTowerFloors == null)
        return (TowerFloorParam) null;
      return this.FindTowerFloors(towerID)?.Find((Predicate<TowerFloorParam>) (floorParam => string.IsNullOrEmpty(floorParam.cond_quest)));
    }

    public TowerFloorParam FindLastTowerFloor(string towerID)
    {
      if (this.mTowerFloors == null)
        return (TowerFloorParam) null;
      List<TowerFloorParam> towerFloors = this.FindTowerFloors(towerID);
      if (towerFloors == null)
        return (TowerFloorParam) null;
      if (towerFloors.Count < 1)
        return (TowerFloorParam) null;
      return towerFloors[towerFloors.Count - 1];
    }

    public TowerFloorParam FindNextTowerFloor(string towerID, string currentFloorID)
    {
      if (this.mTowerFloors == null)
        return (TowerFloorParam) null;
      return this.FindTowerFloors(towerID)?.Find((Predicate<TowerFloorParam>) (floorParam =>
      {
        if (string.IsNullOrEmpty(floorParam.cond_quest))
          return false;
        return floorParam.cond_quest == currentFloorID;
      }));
    }

    public List<TowerFloorParam> FindTowerFloors(string towerID)
    {
      if (this.mTowerFloors != null)
        return this.mTowerFloors.FindAll((Predicate<TowerFloorParam>) (floor => floor.tower_id == towerID));
      return (List<TowerFloorParam>) null;
    }

    public TowerRewardParam FindTowerReward(string iname)
    {
      if (this.mTowerResuponse.round > (byte) 0)
      {
        if (this.mTowerRoundRewards != null)
        {
          for (int index = this.mTowerRoundRewards.Count - 1; index >= 0; --index)
          {
            if (this.mTowerRoundRewards[index].iname == iname)
              return (TowerRewardParam) this.mTowerRoundRewards[index];
          }
        }
      }
      else if (this.mTowerRewards != null)
      {
        for (int index = this.mTowerRewards.Count - 1; index >= 0; --index)
        {
          if (this.mTowerRewards[index].iname == iname)
            return this.mTowerRewards[index];
        }
      }
      return (TowerRewardParam) null;
    }

    public TowerParam FindTower(string iname)
    {
      if (this.mTowers != null)
      {
        for (int index = this.mTowers.Count - 1; index >= 0; --index)
        {
          if (this.mTowers[index].iname == iname)
            return this.mTowers[index];
        }
      }
      return (TowerParam) null;
    }

    public int GetQuestTypeCount(QuestTypes type)
    {
      int num = 0;
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        if (this.mQuests[index].type == type)
          ++num;
      }
      return num;
    }

    public List<QuestParam> GetQuestTypeList(QuestTypes type)
    {
      List<QuestParam> questParamList = new List<QuestParam>();
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        if (this.mQuests[index].type == type)
          questParamList.Add(this.mQuests[index]);
      }
      return questParamList;
    }

    public int CalcTowerScore(bool isNow = true)
    {
      TowerResuponse towerResuponse = this.TowerResuponse;
      int num1 = 0;
      int num2 = !isNow ? towerResuponse.spd_score : towerResuponse.turn_num;
      int num3 = !isNow ? towerResuponse.tec_score : towerResuponse.died_num;
      int num4 = !isNow ? towerResuponse.ret_score : towerResuponse.retire_num;
      int num5 = !isNow ? towerResuponse.rcv_score : towerResuponse.recover_num;
      int num6 = 0;
      TowerScoreParam[] towerScoreParam = this.mMasterParam.FindTowerScoreParam(this.FindTower(towerResuponse.TowerID).score_iname);
      for (int index = 0; index < towerScoreParam.Length; ++index)
      {
        if (num2 <= (int) towerScoreParam[index].TurnCnt && (num6 & 1) == 0)
        {
          num1 += (int) towerScoreParam[index].Score;
          num6 |= 1;
        }
        if (num3 <= (int) towerScoreParam[index].DiedCnt && (num6 & 2) == 0)
        {
          num1 += (int) towerScoreParam[index].Score;
          num6 |= 2;
        }
        if (num4 <= (int) towerScoreParam[index].RetireCnt && (num6 & 4) == 0)
        {
          num1 += (int) towerScoreParam[index].Score;
          num6 |= 4;
        }
        if (num5 <= (int) towerScoreParam[index].RecoverCnt && (num6 & 8) == 0)
        {
          num1 += (int) towerScoreParam[index].Score;
          num6 |= 8;
        }
      }
      for (int index = 0; index < 4; ++index)
      {
        if ((num6 & 1 << index) == 0)
          num1 += (int) towerScoreParam[towerScoreParam.Length - 1].Score;
      }
      return num1 / 4;
    }

    public TOWER_RANK CalcTowerRank(bool isNow = true)
    {
      int num = this.CalcTowerScore(isNow);
      TOWER_RANK towerRank1 = TOWER_RANK.E_MINUS;
      OInt[] towerRank2 = this.mMasterParam.TowerRank;
      for (int index = 0; index < towerRank2.Length; ++index)
      {
        if (num <= (int) towerRank2[index])
        {
          towerRank1 = (TOWER_RANK) index;
          break;
        }
      }
      return towerRank1;
    }

    public string ConvertTowerScoreToRank(TowerParam towerParam, int score, TOWER_SCORE_TYPE type)
    {
      TowerScoreParam[] towerScoreParam = this.mMasterParam.FindTowerScoreParam(towerParam.score_iname);
      string rank = towerScoreParam[towerScoreParam.Length - 1].Rank;
      for (int index = 0; index < towerScoreParam.Length; ++index)
      {
        switch (type)
        {
          case TOWER_SCORE_TYPE.TURN:
            if (score <= (int) towerScoreParam[index].TurnCnt)
            {
              rank = towerScoreParam[index].Rank;
              goto label_12;
            }
            else
              break;
          case TOWER_SCORE_TYPE.DIED:
            if (score <= (int) towerScoreParam[index].DiedCnt)
            {
              rank = towerScoreParam[index].Rank;
              goto label_12;
            }
            else
              break;
          case TOWER_SCORE_TYPE.RETIRE:
            if (score <= (int) towerScoreParam[index].RetireCnt)
            {
              rank = towerScoreParam[index].Rank;
              goto label_12;
            }
            else
              break;
          case TOWER_SCORE_TYPE.RECOVER:
            if (score <= (int) towerScoreParam[index].RecoverCnt)
            {
              rank = towerScoreParam[index].Rank;
              goto label_12;
            }
            else
              break;
        }
      }
label_12:
      return rank;
    }

    public ArenaPlayer FindArenaPlayer(string fuid)
    {
      return this.mArenaPlayers.Find((Predicate<ArenaPlayer>) (p => p.FUID == fuid));
    }

    public ArenaPlayer[] ArenaPlayers
    {
      get
      {
        return this.mArenaPlayers.ToArray();
      }
    }

    public ArenaPlayer[] GetArenaRanking(ReqBtlColoRanking.RankingTypes type)
    {
      return this.mArenaRanking[(int) type].ToArray();
    }

    public AchievementParam FindAchievement(int id)
    {
      for (int index = this.mAchievement.Count - 1; index >= 0; --index)
      {
        if (this.mAchievement[index].id == id)
          return this.mAchievement[index];
      }
      return (AchievementParam) null;
    }

    public ArenaPlayerHistory[] GetArenaHistory()
    {
      return this.mArenaHistory.ToArray();
    }

    public MailData FindMail(long mailID)
    {
      if (this.mPlayer == null)
        return (MailData) null;
      if (this.mPlayer.CurrentMails == null)
        return (MailData) null;
      return this.mPlayer.CurrentMails.Find((Predicate<MailData>) (mail => mail.mid == mailID));
    }

    public PlayerData Player
    {
      get
      {
        return this.mPlayer;
      }
    }

    public OptionData Option
    {
      get
      {
        return this.mOption;
      }
    }

    public MasterParam MasterParam
    {
      get
      {
        return this.mMasterParam;
      }
    }

    public SectionParam[] Sections
    {
      get
      {
        return this.mWorlds.ToArray();
      }
    }

    public ChapterParam[] Chapters
    {
      get
      {
        return this.mAreas.ToArray();
      }
    }

    public QuestParam[] Quests
    {
      get
      {
        return this.mQuests.ToArray();
      }
    }

    public ObjectiveParam[] Objectives
    {
      get
      {
        return this.mObjectives.ToArray();
      }
    }

    public TrophyParam[] Trophies
    {
      get
      {
        return this.mMasterParam.Trophies;
      }
    }

    public TrophyObjective[] GetTrophiesOfType(TrophyConditionTypes type)
    {
      return this.mMasterParam.GetTrophiesOfType(type);
    }

    public ConceptCardParam GetConceptCardParam(string iname)
    {
      return this.mMasterParam.GetConceptCardParam(iname);
    }

    public AchievementParam[] Achievement
    {
      get
      {
        return this.mAchievement.ToArray();
      }
    }

    public GachaParam[] Gachas
    {
      get
      {
        return this.mGachas.ToArray();
      }
    }

    public TowerParam[] Towers
    {
      get
      {
        return this.mTowers.ToArray();
      }
    }

    public TowerResuponse TowerResuponse
    {
      get
      {
        return this.mTowerResuponse;
      }
    }

    public List<TowerResuponse.PlayerUnit> TowerResuponsePlayerUnit
    {
      get
      {
        return this.TowerResuponse.pdeck;
      }
    }

    public List<TowerResuponse.EnemyUnit> TowerEnemyUnit
    {
      get
      {
        return this.TowerResuponse.edeck;
      }
    }

    public VersusFriendScore[] VersusFriendInfo
    {
      get
      {
        return this.mVersusFriendScore.ToArray();
      }
    }

    protected override void Initialize()
    {
      this.OnDayChange += new GameManager.DayChangeEvent(this.DayChanged);
      GlobalEvent.AddListener("TOU_AGREE", new GlobalEvent.Delegate(this.OnAgreeTermsOfUse));
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }

    public bool ReloadMasterData(string masterParam = null, string questParam = null)
    {
      if (this.IsRelogin)
        return true;
      try
      {
        if (masterParam == null)
          masterParam = AssetManager.LoadTextData("Data/MasterParam");
        if (!string.IsNullOrEmpty(this.DigestHash))
        {
          byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(masterParam));
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 0; index < hash.Length; ++index)
            stringBuilder.AppendFormat("{0:x2}", (object) hash[index]);
          string str = stringBuilder.ToString();
          if ((string.IsNullOrEmpty(this.PrevCheckHash) || !string.IsNullOrEmpty(this.PrevCheckHash) && this.PrevCheckHash != str) && str != this.DigestHash)
            this.AlterCheckHash = str;
        }
        JSON_MasterParam json1 = (JSON_MasterParam) JsonUtility.FromJson<JSON_MasterParam>(masterParam);
        if (json1 == null)
          throw new InvalidJSONException();
        this.Deserialize2(json1);
        if (questParam == null)
          questParam = AssetManager.LoadTextData("Data/QuestParam");
        Json_QuestList json2 = (Json_QuestList) JsonUtility.FromJson<Json_QuestList>(questParam);
        if (json2 == null)
          throw new InvalidJSONException();
        this.Deserialize(json2);
      }
      catch (Exception ex)
      {
        this.mReloadMasterDataError = ex.ToString();
        return false;
      }
      return true;
    }

    private void Start()
    {
      this.UpdateResolution();
      LogMonitor.Start();
      DebugMenu.Start();
      ExceptionMonitor.Start();
    }

    public bool Deserialize(JSON_MasterParam json)
    {
      bool flag = true & this.mMasterParam.Deserialize(json);
      if (flag)
        this.mMasterParam.CacheReferences();
      return flag;
    }

    public bool Deserialize2(JSON_MasterParam json)
    {
      bool flag = true & this.mMasterParam.Deserialize2(json);
      if (flag)
        this.mMasterParam.CacheReferences();
      return flag;
    }

    public void Deserialize(Json_PlayerData json)
    {
      int num = 0;
      if (this.Player != null)
        num = this.Player.Lv;
      this.mPlayer.Deserialize(json);
      if (num == 0 || num == this.Player.Lv)
        return;
      this.OnPlayerLvChange();
    }

    public void Deserialize(Json_TrophyPlayerData json)
    {
      int num = 0;
      if (this.Player != null)
        num = this.Player.Lv;
      this.mPlayer.Deserialize(json);
      if (num == 0 || num == this.Player.Lv)
        return;
      this.OnPlayerLvChange();
    }

    public void Deserialize(Json_Unit[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Item[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Artifact[] json, bool differenceUpdate = false)
    {
      this.mPlayer.Deserialize(json, differenceUpdate);
    }

    public void Deserialize(JSON_ConceptCard[] json, bool is_data_override = true)
    {
      this.mPlayer.Deserialize(json, is_data_override);
    }

    public void Deserialize(JSON_ConceptCardMaterial[] json, bool is_data_override = true)
    {
      this.mPlayer.Deserialize(json, is_data_override);
    }

    public void Deserialize(Json_Skin[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Party[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public bool Deserialize(Json_Mail[] json)
    {
      return this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Friend[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Friend[] json, FriendStates state)
    {
      this.mPlayer.Deserialize(json, state);
    }

    public void Deserialize(Json_Support[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_MultiFuids[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(FriendPresentWishList.Json[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(FriendPresentReceiveList.Json[] json)
    {
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Notify json)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_Versus json)
    {
      if (json == null)
        return;
      this.mPlayer.Deserialize(json);
    }

    public void Deserialize(Json_QuestList json)
    {
      this.mWorlds.Clear();
      this.mAreas.Clear();
      this.mObjectives.Clear();
      this.mTowerObjectives.Clear();
      this.mMagnifications.Clear();
      this.mParties.Clear();
      this.mConditions.Clear();
      this.mQuests.Clear();
      this.mQuestsDict.Clear();
      this.mMagnifications.Clear();
      this.mConditions.Clear();
      this.mCampaignParents.Clear();
      this.mCampaignChildren.Clear();
      this.mCampaignTrust.Clear();
      this.mTowerRewards.Clear();
      this.mTowerBaseQuests.Clear();
      this.mVersusTowerFloor.Clear();
      this.mTowerFloors.Clear();
      this.mTowerFloorsDict.Clear();
      this.mVersusScheduleParam.Clear();
      this.mVersusCoinParam.Clear();
      this.mMultiTowerFloor.Clear();
      this.mMultiTowerRewards.Clear();
      this.mRankingQuestParam.Clear();
      this.mRankingQuestRewardParam.Clear();
      this.mRankingQuestScheduleParam.Clear();
      this.mAvailableRankingQuesstParams.Clear();
      this.mVersusFirstWinBonus.Clear();
      this.mVersusStreakWinBonus.Clear();
      this.mVersusRule.Clear();
      this.mVersusCoinCamp.Clear();
      this.mVersusRank.Clear();
      this.mVersusRankClass.Clear();
      this.mVersusRankRankingReward.Clear();
      this.mVersusRankReward.Clear();
      this.mVersusRankMissionSchedule.Clear();
      this.mVersusRankMission.Clear();
      this.mVersusDraftUnit.Clear();
      DebugUtility.Verify((object) json, typeof (Json_QuestList));
      for (int index = 0; index < json.worlds.Length; ++index)
      {
        SectionParam sectionParam = new SectionParam();
        sectionParam.Deserialize(json.worlds[index]);
        this.mWorlds.Add(sectionParam);
      }
      for (int index = 0; index < json.areas.Length; ++index)
      {
        ChapterParam chapterParam = new ChapterParam();
        chapterParam.Deserialize(json.areas[index]);
        this.mAreas.Add(chapterParam);
        if (!string.IsNullOrEmpty(chapterParam.section))
          chapterParam.sectionParam = this.FindWorld(chapterParam.section);
      }
      for (int index = 0; index < json.areas.Length; ++index)
      {
        if (!string.IsNullOrEmpty(json.areas[index].parent))
        {
          ChapterParam area = this.FindArea(json.areas[index].iname);
          if (area != null)
          {
            area.parent = this.FindArea(json.areas[index].parent);
            if (area.parent != null)
              area.parent.children.Add(area);
          }
        }
      }
      for (int index = 0; index < json.objectives.Length; ++index)
      {
        ObjectiveParam objectiveParam = new ObjectiveParam();
        objectiveParam.Deserialize(json.objectives[index]);
        this.mObjectives.Add(objectiveParam);
      }
      if (json.towerObjectives != null)
      {
        for (int index = 0; index < json.towerObjectives.Length; ++index)
        {
          ObjectiveParam objectiveParam = new ObjectiveParam();
          objectiveParam.Deserialize(json.towerObjectives[index]);
          this.mTowerObjectives.Add(objectiveParam);
        }
      }
      if (json.magnifications != null)
      {
        for (int index = 0; index < json.magnifications.Length; ++index)
        {
          MagnificationParam magnificationParam = new MagnificationParam();
          magnificationParam.Deserialize(json.magnifications[index]);
          this.mMagnifications.Add(magnificationParam);
        }
      }
      if (json.conditions != null)
      {
        for (int index = 0; index < json.conditions.Length; ++index)
        {
          QuestCondParam questCondParam = new QuestCondParam();
          questCondParam.Deserialize(json.conditions[index]);
          this.mConditions.Add(questCondParam);
        }
      }
      if (json.parties != null)
      {
        for (int index = 0; index < json.parties.Length; ++index)
        {
          QuestPartyParam questPartyParam = new QuestPartyParam();
          questPartyParam.Deserialize(json.parties[index]);
          this.mParties.Add(questPartyParam);
        }
      }
      for (int index = 0; index < json.quests.Length; ++index)
      {
        QuestParam questParam = new QuestParam();
        questParam.Deserialize(json.quests[index]);
        this.mQuests.Add(questParam);
        this.mQuestsDict.Add(questParam.iname, questParam);
        if (!string.IsNullOrEmpty(questParam.ChapterID))
        {
          questParam.Chapter = this.FindArea(questParam.ChapterID);
          if (questParam.Chapter != null)
            questParam.Chapter.quests.Add(questParam);
        }
        if (questParam.type == QuestTypes.Tower)
          this.mTowerBaseQuests.Add(questParam);
      }
      if (this.mPlayer.UnitNum >= 1)
      {
        for (int index = 0; index < this.mPlayer.Units.Count; ++index)
          this.mPlayer.Units[index].ResetCharacterQuestParams();
      }
      if (json.CampaignParents != null)
      {
        for (int index = 0; index < json.CampaignParents.Length; ++index)
        {
          QuestCampaignParentParam campaignParentParam = new QuestCampaignParentParam();
          if (campaignParentParam.Deserialize(json.CampaignParents[index]))
            this.mCampaignParents.Add(campaignParentParam);
        }
      }
      if (json.CampaignChildren != null)
      {
        for (int index = 0; index < json.CampaignChildren.Length; ++index)
        {
          QuestCampaignChildParam campaignChildParam = new QuestCampaignChildParam();
          if (campaignChildParam.Deserialize(json.CampaignChildren[index]))
          {
            List<QuestCampaignParentParam> campaignParentParamList = new List<QuestCampaignParentParam>();
            using (List<QuestCampaignParentParam>.Enumerator enumerator = this.mCampaignParents.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                QuestCampaignParentParam current = enumerator.Current;
                if (current.IsChild(campaignChildParam.iname))
                  campaignParentParamList.Add(current);
              }
            }
            campaignChildParam.parents = campaignParentParamList.ToArray();
            this.mCampaignChildren.Add(campaignChildParam);
          }
        }
      }
      if (json.CampaignTrust != null)
      {
        for (int index = 0; index < json.CampaignTrust.Length; ++index)
        {
          QuestCampaignTrust param = new QuestCampaignTrust();
          if (param.Deserialize(json.CampaignTrust[index]))
          {
            this.mCampaignTrust.Add(param);
            QuestCampaignChildParam campaignChildParam = this.mCampaignChildren.Find((Predicate<QuestCampaignChildParam>) (value => value.iname == param.iname));
            if (campaignChildParam != null)
              campaignChildParam.campaignTrust = param;
          }
        }
      }
      if (json.towerRewards != null)
      {
        for (int index = 0; index < json.towerRewards.Length; ++index)
        {
          TowerRewardParam towerRewardParam = new TowerRewardParam();
          towerRewardParam.Deserialize(json.towerRewards[index]);
          this.mTowerRewards.Add(towerRewardParam);
        }
      }
      if (json.towerRoundRewards != null)
      {
        for (int index = 0; index < json.towerRoundRewards.Length; ++index)
        {
          TowerRoundRewardParam roundRewardParam = new TowerRoundRewardParam();
          roundRewardParam.Deserialize(json.towerRoundRewards[index]);
          this.mTowerRoundRewards.Add(roundRewardParam);
        }
      }
      if (json.towerFloors != null)
      {
        for (int index = 0; index < json.towerFloors.Length; ++index)
        {
          TowerFloorParam towerFloorParam = new TowerFloorParam();
          towerFloorParam.Deserialize(json.towerFloors[index]);
          this.mTowerFloors.Add(towerFloorParam);
          this.mTowerFloorsDict.Add(towerFloorParam.iname, towerFloorParam);
        }
      }
      if (json.towers != null)
      {
        for (int index = 0; index < json.towers.Length; ++index)
        {
          TowerParam towerParam = new TowerParam();
          towerParam.Deserialize(json.towers[index]);
          this.mTowers.Add(towerParam);
        }
      }
      if (json.versusTowerFloor != null)
      {
        this.mVersusTowerFloor = new List<VersusTowerParam>(json.versusTowerFloor.Length);
        for (int index = 0; index < json.versusTowerFloor.Length; ++index)
        {
          VersusTowerParam versusTowerParam = new VersusTowerParam();
          versusTowerParam.Deserialize(json.versusTowerFloor[index]);
          this.mVersusTowerFloor.Add(versusTowerParam);
        }
      }
      if (json.versusschedule != null)
      {
        this.mVersusScheduleParam = new List<VersusScheduleParam>(json.versusschedule.Length);
        for (int index = 0; index < json.versusschedule.Length; ++index)
        {
          VersusScheduleParam versusScheduleParam = new VersusScheduleParam();
          versusScheduleParam.Deserialize(json.versusschedule[index]);
          this.mVersusScheduleParam.Add(versusScheduleParam);
        }
      }
      if (json.versuscoin != null)
      {
        this.mVersusCoinParam = new List<VersusCoinParam>(json.versuscoin.Length);
        for (int index = 0; index < json.versuscoin.Length; ++index)
        {
          VersusCoinParam versusCoinParam = new VersusCoinParam();
          versusCoinParam.Deserialize(json.versuscoin[index]);
          this.mVersusCoinParam.Add(versusCoinParam);
        }
      }
      if (json.multitowerFloor != null)
      {
        this.mMultiTowerFloor = new List<MultiTowerFloorParam>(json.multitowerFloor.Length);
        for (int index = 0; index < json.multitowerFloor.Length; ++index)
        {
          MultiTowerFloorParam multiTowerFloorParam = new MultiTowerFloorParam();
          multiTowerFloorParam.Deserialize(json.multitowerFloor[index]);
          this.mMultiTowerFloor.Add(multiTowerFloorParam);
        }
      }
      if (json.multitowerRewards != null)
      {
        for (int index = 0; index < json.multitowerRewards.Length; ++index)
        {
          MultiTowerRewardParam towerRewardParam = new MultiTowerRewardParam();
          towerRewardParam.Deserialize(json.multitowerRewards[index]);
          this.mMultiTowerRewards.Add(towerRewardParam);
        }
      }
      if (json.MapEffect != null)
      {
        List<SRPG.MapEffectParam> mapEffectParamList = new List<SRPG.MapEffectParam>(json.MapEffect.Length);
        for (int index = 0; index < json.MapEffect.Length; ++index)
        {
          SRPG.MapEffectParam mapEffectParam = new SRPG.MapEffectParam();
          mapEffectParam.Deserialize(json.MapEffect[index]);
          mapEffectParamList.Add(mapEffectParam);
        }
        this.mMapEffectParam = mapEffectParamList;
        this.mMasterParam.MakeMapEffectHaveJobLists();
      }
      if (json.WeatherSet != null)
      {
        List<WeatherSetParam> weatherSetParamList = new List<WeatherSetParam>(json.WeatherSet.Length);
        for (int index = 0; index < json.WeatherSet.Length; ++index)
        {
          WeatherSetParam weatherSetParam = new WeatherSetParam();
          weatherSetParam.Deserialize(json.WeatherSet[index]);
          weatherSetParamList.Add(weatherSetParam);
        }
        this.mWeatherSetParam = weatherSetParamList;
      }
      if (json.rankingQuestSchedule != null)
      {
        this.mRankingQuestScheduleParam = new List<RankingQuestScheduleParam>(json.rankingQuestSchedule.Length);
        for (int index = 0; index < json.rankingQuestSchedule.Length; ++index)
        {
          RankingQuestScheduleParam questScheduleParam = new RankingQuestScheduleParam();
          questScheduleParam.Deserialize(json.rankingQuestSchedule[index]);
          this.mRankingQuestScheduleParam.Add(questScheduleParam);
        }
      }
      if (json.rankingQuestRewards != null)
      {
        this.mRankingQuestRewardParam = new List<RankingQuestRewardParam>(json.rankingQuestRewards.Length);
        for (int index = 0; index < json.rankingQuestRewards.Length; ++index)
        {
          RankingQuestRewardParam questRewardParam = new RankingQuestRewardParam();
          questRewardParam.Deserialize(json.rankingQuestRewards[index]);
          this.mRankingQuestRewardParam.Add(questRewardParam);
        }
      }
      if (json.rankingQuests != null)
      {
        this.mRankingQuestParam = new List<RankingQuestParam>(json.rankingQuests.Length);
        for (int index = 0; index < json.rankingQuests.Length; ++index)
        {
          RankingQuestParam rankingQuestParam = new RankingQuestParam();
          rankingQuestParam.Deserialize(json.rankingQuests[index]);
          rankingQuestParam.rewardParam = RankingQuestRewardParam.FindByID(rankingQuestParam.reward_id);
          rankingQuestParam.scheduleParam = RankingQuestScheduleParam.FindByID(rankingQuestParam.schedule_id);
          this.mRankingQuestParam.Add(rankingQuestParam);
        }
      }
      if (json.versusfirstwinbonus != null)
      {
        int length = json.versusfirstwinbonus.Length;
        this.mVersusFirstWinBonus = new List<VersusFirstWinBonusParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusFirstWinBonusParam firstWinBonusParam = new VersusFirstWinBonusParam();
          if (firstWinBonusParam != null && firstWinBonusParam.Deserialize(json.versusfirstwinbonus[index]))
            this.mVersusFirstWinBonus.Add(firstWinBonusParam);
        }
      }
      if (json.versusstreakwinschedule != null)
      {
        int length = json.versusstreakwinschedule.Length;
        this.mVersusStreakSchedule = new List<VersusStreakWinScheduleParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusStreakWinScheduleParam winScheduleParam = new VersusStreakWinScheduleParam();
          if (winScheduleParam != null && winScheduleParam.Deserialize(json.versusstreakwinschedule[index]))
            this.mVersusStreakSchedule.Add(winScheduleParam);
        }
      }
      if (json.versusstreakwinbonus != null)
      {
        int length = json.versusstreakwinbonus.Length;
        this.mVersusStreakWinBonus = new List<VersusStreakWinBonusParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusStreakWinBonusParam streakWinBonusParam = new VersusStreakWinBonusParam();
          if (streakWinBonusParam != null && streakWinBonusParam.Deserialize(json.versusstreakwinbonus[index]))
            this.mVersusStreakWinBonus.Add(streakWinBonusParam);
        }
      }
      if (json.versusrule != null)
      {
        int length = json.versusrule.Length;
        this.mVersusRule = new List<VersusRuleParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRuleParam versusRuleParam = new VersusRuleParam();
          if (versusRuleParam != null && versusRuleParam.Deserialize(json.versusrule[index]))
            this.mVersusRule.Add(versusRuleParam);
        }
      }
      if (json.versuscoincamp != null)
      {
        int length = json.versuscoincamp.Length;
        this.mVersusCoinCamp = new List<VersusCoinCampParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusCoinCampParam versusCoinCampParam = new VersusCoinCampParam();
          if (versusCoinCampParam != null && versusCoinCampParam.Deserialize(json.versuscoincamp[index]))
            this.mVersusCoinCamp.Add(versusCoinCampParam);
        }
      }
      if (json.versusenabletime != null)
      {
        int length = json.versusenabletime.Length;
        this.mVersusEnableTime = new List<VersusEnableTimeParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusEnableTimeParam versusEnableTimeParam = new VersusEnableTimeParam();
          if (versusEnableTimeParam.Deserialize(json.versusenabletime[index]))
            this.mVersusEnableTime.Add(versusEnableTimeParam);
        }
      }
      if (json.VersusRank != null)
      {
        int length = json.VersusRank.Length;
        this.mVersusRank = new List<VersusRankParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankParam versusRankParam = new VersusRankParam();
          if (versusRankParam.Deserialize(json.VersusRank[index]))
            this.mVersusRank.Add(versusRankParam);
        }
      }
      if (json.VersusRankClass != null)
      {
        int length = json.VersusRankClass.Length;
        this.mVersusRankClass = new List<VersusRankClassParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankClassParam versusRankClassParam = new VersusRankClassParam();
          if (versusRankClassParam.Deserialize(json.VersusRankClass[index]))
            this.mVersusRankClass.Add(versusRankClassParam);
        }
      }
      if (json.VersusRankRankingReward != null)
      {
        int length = json.VersusRankRankingReward.Length;
        this.mVersusRankRankingReward = new List<VersusRankRankingRewardParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankRankingRewardParam rankingRewardParam = new VersusRankRankingRewardParam();
          if (rankingRewardParam.Deserialize(json.VersusRankRankingReward[index]))
            this.mVersusRankRankingReward.Add(rankingRewardParam);
        }
      }
      if (json.VersusRankReward != null)
      {
        int length = json.VersusRankReward.Length;
        this.mVersusRankReward = new List<VersusRankRewardParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankRewardParam versusRankRewardParam = new VersusRankRewardParam();
          if (versusRankRewardParam.Deserialize(json.VersusRankReward[index]))
            this.mVersusRankReward.Add(versusRankRewardParam);
        }
      }
      if (json.VersusRankMissionSchedule != null)
      {
        int length = json.VersusRankMissionSchedule.Length;
        this.mVersusRankMissionSchedule = new List<VersusRankMissionScheduleParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankMissionScheduleParam missionScheduleParam = new VersusRankMissionScheduleParam();
          if (missionScheduleParam.Deserialize(json.VersusRankMissionSchedule[index]))
            this.mVersusRankMissionSchedule.Add(missionScheduleParam);
        }
      }
      if (json.VersusRankMission != null)
      {
        int length = json.VersusRankMission.Length;
        this.mVersusRankMission = new List<VersusRankMissionParam>(length);
        for (int index = 0; index < length; ++index)
        {
          VersusRankMissionParam rankMissionParam = new VersusRankMissionParam();
          if (rankMissionParam.Deserialize(json.VersusRankMission[index]))
            this.mVersusRankMission.Add(rankMissionParam);
        }
      }
      if (json.GuerrillaShopAdventQuest != null)
      {
        int length = json.GuerrillaShopAdventQuest.Length;
        this.mGuerrillaShopAdventQuest = new List<GuerrillaShopAdventQuestParam>(length);
        for (int index = 0; index < length; ++index)
        {
          GuerrillaShopAdventQuestParam adventQuestParam = new GuerrillaShopAdventQuestParam();
          if (adventQuestParam.Deserialize(json.GuerrillaShopAdventQuest[index]))
            this.mGuerrillaShopAdventQuest.Add(adventQuestParam);
        }
      }
      if (json.GuerrillaShopSchedule != null)
      {
        int length = json.GuerrillaShopSchedule.Length;
        this.mGuerrillaShopScheduleParam = new List<GuerrillaShopScheduleParam>(length);
        for (int index = 0; index < length; ++index)
        {
          GuerrillaShopScheduleParam shopScheduleParam = new GuerrillaShopScheduleParam();
          if (shopScheduleParam.Deserialize(json.GuerrillaShopSchedule[index]))
            this.mGuerrillaShopScheduleParam.Add(shopScheduleParam);
        }
      }
      if (json.VersusDraftUnit == null)
        return;
      int length1 = json.VersusDraftUnit.Length;
      this.mVersusDraftUnit = new List<VersusDraftUnitParam>(length1);
      for (int index = 0; index < length1; ++index)
      {
        VersusDraftUnitParam versusDraftUnitParam = new VersusDraftUnitParam();
        if (versusDraftUnitParam.Deserialize((long) index + 1L, json.VersusDraftUnit[index]))
          this.mVersusDraftUnit.Add(versusDraftUnitParam);
      }
    }

    public bool Deserialize(Json_AchievementList json)
    {
      this.mAchievement.Clear();
      int num = 0;
      while (num < json.achievements.Length)
        ++num;
      return true;
    }

    public void ResetJigenQuests()
    {
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        this.mQuests[index].start = 0L;
        this.mQuests[index].end = 0L;
        if (this.mQuests[index].IsKeyQuest)
          this.mQuests[index].Chapter.key_end = 0L;
        this.mQuests[index].gps_enable = false;
      }
    }

    public void ResetGpsQuests()
    {
      for (int index = 0; index < this.mQuests.Count; ++index)
        this.mQuests[index].gps_enable = false;
    }

    public bool DeserializeGps(JSON_QuestProgress[] jsons)
    {
      if (jsons == null)
        return false;
      for (int index = 0; index < jsons.Length; ++index)
      {
        JSON_QuestProgress json = jsons[index];
        if (json != null)
        {
          QuestParam quest = this.FindQuest(json.i);
          if (quest != null && (quest.IsGps || quest.IsMultiAreaQuest))
            quest.gps_enable = true;
        }
      }
      return true;
    }

    public bool Deserialize(JSON_QuestProgress[] json)
    {
      if (json == null)
        return true;
      for (int index = 0; index < json.Length; ++index)
      {
        if (!this.Deserialize(json[index]))
          return false;
      }
      return true;
    }

    public bool Deserialize(JSON_QuestProgress json)
    {
      if (json == null)
        return true;
      QuestParam quest = this.FindQuest(json.i);
      if (quest == null)
        return true;
      OInt m = (OInt) json.m;
      quest.clear_missions = (int) m;
      quest.state = (QuestStates) json.t;
      quest.start = json.s;
      quest.end = json.e;
      quest.key_end = json.n;
      quest.key_cnt = json.c;
      if (quest.Chapter != null)
      {
        quest.Chapter.start = quest.Chapter.start > 0L ? Math.Min(quest.Chapter.start, json.s) : json.s;
        quest.Chapter.end = Math.Max(quest.Chapter.end, json.e);
        if (quest.Chapter.IsKeyQuest())
          quest.Chapter.key_end = json.e != 0L ? Math.Max(quest.Chapter.key_end, Math.Min(json.e, json.n)) : Math.Max(quest.Chapter.key_end, json.n);
      }
      if (json.d != null)
      {
        quest.dailyCount = CheckCast.to_short(json.d.num);
        quest.dailyReset = CheckCast.to_short(json.d.reset);
      }
      return true;
    }

    public bool Deserialize(Json_GachaList json)
    {
      if (json == null || json.gachas == null)
        return false;
      this.mGachas.Clear();
      for (int index = 0; index < json.gachas.Length; ++index)
      {
        GachaParam gachaParam = new GachaParam();
        gachaParam.Deserialize(json.gachas[index]);
        this.mGachas.Add(gachaParam);
      }
      return true;
    }

    public void Deserialize(Json_GachaResult json)
    {
      this.mPlayer.Deserialize(json.player);
      this.mPlayer.Deserialize(json.items);
      this.mPlayer.Deserialize(json.units);
      this.mPlayer.Deserialize(json.mails);
      if (json.artifacts == null)
        return;
      this.mPlayer.Deserialize(json.artifacts, true);
    }

    public void Deserialize(Json_GoogleReview json)
    {
      this.mPlayer.Deserialize(json.player);
      this.mPlayer.Deserialize(json.items);
      this.mPlayer.Deserialize(json.units);
      this.mPlayer.Deserialize(json.mails);
    }

    public bool Deserialize(Json_ArenaPlayers json)
    {
      this.mArenaPlayers.Clear();
      if (!this.Player.Deserialize(json))
        return false;
      if (json.coloenemies != null)
      {
        for (int index = 0; index < json.coloenemies.Length; ++index)
        {
          ArenaPlayer arenaPlayer = new ArenaPlayer();
          try
          {
            arenaPlayer.Deserialize(json.coloenemies[index]);
            this.mArenaPlayers.Add(arenaPlayer);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public bool Deserialize(JSON_ArenaRanking json, ReqBtlColoRanking.RankingTypes type)
    {
      List<ArenaPlayer> arenaPlayerList = this.mArenaRanking[(int) type];
      arenaPlayerList.Clear();
      if (json.coloenemies != null)
      {
        for (int index = 0; index < json.coloenemies.Length; ++index)
        {
          ArenaPlayer arenaPlayer = new ArenaPlayer();
          try
          {
            arenaPlayer.Deserialize(json.coloenemies[index]);
            arenaPlayerList.Add(arenaPlayer);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public bool Deserialize(JSON_ArenaHistory json)
    {
      List<ArenaPlayerHistory> mArenaHistory = this.mArenaHistory;
      mArenaHistory.Clear();
      if (json.colohistories != null)
      {
        for (int index = 0; index < json.colohistories.Length; ++index)
        {
          ArenaPlayerHistory arenaPlayerHistory = new ArenaPlayerHistory();
          try
          {
            arenaPlayerHistory.Deserialize(json.colohistories[index]);
            mArenaHistory.Add(arenaPlayerHistory);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public void Deserialize(JSON_ReqTowerResuponse json)
    {
      TowerResuponse towerResuponse = new TowerResuponse();
      towerResuponse.Deserialize(json);
      this.mTowerResuponse = towerResuponse;
    }

    public bool Deserialize(JSON_ChatChannelMaster json)
    {
      if (json == null || json.channels == null)
        return false;
      this.mChatChannelMasters.Clear();
      for (int index = 0; index < json.channels.Length; ++index)
      {
        ChatChannelMasterParam channelMasterParam = new ChatChannelMasterParam();
        channelMasterParam.Deserialize(json.channels[index]);
        this.mChatChannelMasters.Add(channelMasterParam);
      }
      return true;
    }

    public bool Deserialize(Json_VersusFriendScore[] json)
    {
      if (json == null || this.mVersusFriendScore == null)
        return false;
      this.mVersusFriendScore.Clear();
      for (int index = 0; index < json.Length; ++index)
      {
        VersusFriendScore versusFriendScore = new VersusFriendScore();
        versusFriendScore.floor = json[index].floor;
        versusFriendScore.name = json[index].name;
        versusFriendScore.unit = new UnitData();
        versusFriendScore.unit.Deserialize(json[index].unit);
        this.mVersusFriendScore.Add(versusFriendScore);
      }
      return true;
    }

    public bool SetVersusWinCount(int wincnt)
    {
      PlayerData player = this.Player;
      if (player != null)
      {
        player.SetVersusWinCount(GlobalVars.SelectedMultiPlayVersusType, wincnt);
        player.AddVersusTotalCount(GlobalVars.SelectedMultiPlayVersusType, 1);
      }
      return true;
    }

    public void SetVersuTowerEndParam(bool rankup, bool winbonus, int key, int floor, int arravied)
    {
      if (this.mVersusEndParam == null)
        return;
      this.mVersusEndParam.rankup = rankup;
      this.mVersusEndParam.winbonus = winbonus;
      this.mVersusEndParam.key = key;
      this.mVersusEndParam.floor = floor;
      this.mVersusEndParam.arravied = arravied;
    }

    public VsTowerMatchEndParam GetVsTowerMatchEndParam()
    {
      return this.mVersusEndParam;
    }

    public void SetAvailableRankingQuestParams(List<RankingQuestParam> value)
    {
      this.mAvailableRankingQuesstParams = value;
    }

    public UnitParam GetUnitParam(string key)
    {
      return this.mMasterParam.GetUnitParam(key);
    }

    public SkillParam GetSkillParam(string key)
    {
      return this.mMasterParam.GetSkillParam(key);
    }

    public AbilityParam GetAbilityParam(string key)
    {
      return this.mMasterParam.GetAbilityParam(key);
    }

    public ItemParam GetItemParam(string key)
    {
      return this.mMasterParam.GetItemParam(key);
    }

    public AwardParam GetAwardParam(string key)
    {
      return this.mMasterParam.GetAwardParam(key);
    }

    public GeoParam GetGeoParam(string key)
    {
      return this.mMasterParam.GetGeoParam(key);
    }

    public WeaponParam GetWeaponParam(string key)
    {
      return this.mMasterParam.GetWeaponParam(key);
    }

    public RecipeParam GetRecipeParam(string key)
    {
      return this.mMasterParam.GetRecipeParam(key);
    }

    public JobParam GetJobParam(string key)
    {
      return this.mMasterParam.GetJobParam(key);
    }

    public JobParam[] GetAllJobs()
    {
      return this.mMasterParam.GetAllJobs();
    }

    public JobSetParam GetJobSetParam(string key)
    {
      return this.mMasterParam.GetJobSetParam(key);
    }

    public JobSetParam[] GetClassChangeJobSetParam(string key)
    {
      return this.mMasterParam.GetClassChangeJobSetParam(key);
    }

    public GrowParam GetGrowParam(string key)
    {
      return this.mMasterParam.GetGrowParam(key);
    }

    public AIParam GetAIParam(string key)
    {
      return this.mMasterParam.GetAIParam(key);
    }

    public RarityParam GetRarityParam(int rarity)
    {
      return this.mMasterParam.GetRarityParam(rarity);
    }

    public List<GachaParam> GetGachaList(string category)
    {
      List<GachaParam> gachaParamList = new List<GachaParam>();
      for (int index = 0; index < this.mGachas.Count; ++index)
      {
        if (this.mGachas[index].category == category)
          gachaParamList.Add(this.mGachas[index]);
      }
      return gachaParamList;
    }

    public ChatChannelMasterParam[] GetChatChannelMaster()
    {
      if (this.mChatChannelMasters != null)
        return this.mChatChannelMasters.ToArray();
      return new ChatChannelMasterParam[0];
    }

    public string DeviceId
    {
      get
      {
        if (this.mMyGuid != null)
          return this.mMyGuid.device_id;
        return (string) null;
      }
    }

    public string SecretKey
    {
      get
      {
        if (this.mMyGuid != null)
          return this.mMyGuid.secret_key;
        return (string) null;
      }
    }

    public string UdId
    {
      get
      {
        if (this.mMyGuid != null)
          return this.mMyGuid.udid;
        return (string) null;
      }
    }

    public bool IsDeviceId()
    {
      return this.DeviceId != null;
    }

    public bool InitAuth()
    {
      if (this.mMyGuid == null)
        this.mMyGuid = new MyGUID();
      this.mMyGuid.Init(31221512);
      return true;
    }

    public void SaveAuth(string device_id)
    {
      this.mMyGuid.SaveAuth(31221512, this.SecretKey, device_id, this.UdId);
    }

    public void SaveAuthWithKey(string device_id, string secretKey)
    {
      this.mMyGuid.SetSecretKey(secretKey);
      this.mMyGuid.SaveAuth(31221512, this.SecretKey, device_id, this.UdId);
    }

    public void ResetAuth()
    {
      this.mMyGuid.ResetCache();
      Network.SessionID = string.Empty;
    }

    private string GenerateSalt()
    {
      byte[] numArray = new byte[24];
      new RNGCryptoServiceProvider().GetBytes(numArray);
      return Convert.ToBase64String(numArray);
    }

    private string GenerateHash(string pass, string salt)
    {
      string empty = string.Empty;
      System.Security.Cryptography.SHA256 shA256 = System.Security.Cryptography.SHA256.Create();
      byte[] bytes = new UTF8Encoding().GetBytes(pass + salt);
      string base64String = Convert.ToBase64String(shA256.ComputeHash(bytes));
      shA256.Clear();
      return base64String;
    }

    public string Encrypt(string key, string iv, string src)
    {
      RijndaelManaged rijndaelManaged1 = new RijndaelManaged();
      rijndaelManaged1.Padding = PaddingMode.Zeros;
      rijndaelManaged1.Mode = CipherMode.CBC;
      RijndaelManaged rijndaelManaged2 = rijndaelManaged1;
      int num1 = 128;
      rijndaelManaged1.BlockSize = num1;
      int num2 = num1;
      rijndaelManaged2.KeySize = num2;
      byte[] bytes1 = Encoding.UTF8.GetBytes(key);
      byte[] bytes2 = Encoding.UTF8.GetBytes(iv);
      byte[] bytes3 = Encoding.UTF8.GetBytes(src);
      ICryptoTransform encryptor = rijndaelManaged1.CreateEncryptor(bytes1, bytes2);
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write);
      cryptoStream.Write(bytes3, 0, bytes3.Length);
      cryptoStream.FlushFinalBlock();
      return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string key, string iv, string src)
    {
      RijndaelManaged rijndaelManaged1 = new RijndaelManaged();
      rijndaelManaged1.Padding = PaddingMode.Zeros;
      rijndaelManaged1.Mode = CipherMode.CBC;
      RijndaelManaged rijndaelManaged2 = rijndaelManaged1;
      int num1 = 128;
      rijndaelManaged1.BlockSize = num1;
      int num2 = num1;
      rijndaelManaged2.KeySize = num2;
      byte[] bytes1 = Encoding.UTF8.GetBytes(key);
      byte[] bytes2 = Encoding.UTF8.GetBytes(iv);
      byte[] buffer = Convert.FromBase64String(src);
      byte[] numArray = new byte[buffer.Length];
      ICryptoTransform decryptor = rijndaelManaged1.CreateDecryptor(bytes1, bytes2);
      new CryptoStream((Stream) new MemoryStream(buffer), decryptor, CryptoStreamMode.Read).Read(numArray, 0, numArray.Length);
      return Encoding.UTF8.GetString(numArray);
    }

    public bool CheckBadges(GameManager.BadgeTypes type)
    {
      return (this.BadgeFlags & type) != ~GameManager.BadgeTypes.All;
    }

    public bool CheckBusyBadges(GameManager.BadgeTypes type)
    {
      return (this.IsBusyUpdateBadges & type) != ~GameManager.BadgeTypes.All;
    }

    public void RequestUpdateBadges(GameManager.BadgeTypes type)
    {
      if ((type & GameManager.BadgeTypes.DailyMission) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.DailyMission;
        this.Player.UpdateTrophyStates();
        if (this.Player.TrophyStates != null)
        {
          for (int index = 0; index < this.Player.TrophyStates.Length; ++index)
          {
            TrophyState trophyState = this.Player.TrophyStates[index];
            if (trophyState.Param.IsShowBadge(trophyState))
            {
              this.BadgeFlags |= GameManager.BadgeTypes.DailyMission;
              break;
            }
          }
        }
      }
      if ((type & GameManager.BadgeTypes.GiftBox) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.GiftBox;
        if (this.Player.UnreadMail || this.Player.UnreadMailPeriod)
          this.BadgeFlags |= GameManager.BadgeTypes.GiftBox;
      }
      if ((type & GameManager.BadgeTypes.Friend) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.Friend;
        if (0 < this.Player.FollowerNum)
          this.BadgeFlags |= GameManager.BadgeTypes.Friend;
      }
      if ((type & GameManager.BadgeTypes.Unit) != ~GameManager.BadgeTypes.All)
        this.StartCoroutine(this.UpdateUnitsBadges());
      if ((type & GameManager.BadgeTypes.UnitUnlock) != ~GameManager.BadgeTypes.All)
        this.StartCoroutine(this.UpdateUnitUnlockBadges());
      if ((type & GameManager.BadgeTypes.GoldGacha) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.GoldGacha;
        if (this.Player.CheckFreeGachaGold())
          this.BadgeFlags |= GameManager.BadgeTypes.GoldGacha;
      }
      if ((type & GameManager.BadgeTypes.RareGacha) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.RareGacha;
        if (this.Player.CheckFreeGachaCoin())
          this.BadgeFlags |= GameManager.BadgeTypes.RareGacha;
      }
      if ((type & GameManager.BadgeTypes.Arena) != ~GameManager.BadgeTypes.All)
      {
        this.BadgeFlags &= ~GameManager.BadgeTypes.Arena;
        if (this.Player.CheckUnlock(UnlockTargets.Arena) && this.Player.ChallengeArenaNum == this.Player.ChallengeArenaMax)
          this.BadgeFlags |= GameManager.BadgeTypes.Arena;
      }
      if ((type & GameManager.BadgeTypes.Multiplay) == ~GameManager.BadgeTypes.All)
        return;
      this.BadgeFlags &= ~GameManager.BadgeTypes.Multiplay;
      if (!this.Player.CheckUnlock(UnlockTargets.MultiPlay) || this.Player.ChallengeMultiNum != 0)
        return;
      this.BadgeFlags |= GameManager.BadgeTypes.Multiplay;
    }

    [DebuggerHidden]
    private IEnumerator UpdateUnitsBadges()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CUpdateUnitsBadges\u003Ec__IteratorD2()
      {
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator UpdateUnitUnlockBadges()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CUpdateUnitUnlockBadges\u003Ec__IteratorD3()
      {
        \u003C\u003Ef__this = this
      };
    }

    public bool CheckEnableUnitUnlock(UnitParam unit)
    {
      return this.mBadgeUnitUnlocks != null && this.mBadgeUnitUnlocks.Contains(unit);
    }

    public void GetLearningAbilitySource(UnitData unit, string abilityID, out JobParam job, out int rank)
    {
      for (int index = 0; index < unit.Jobs.Length; ++index)
      {
        rank = unit.Jobs[index].Param.FindRankOfAbility(abilityID);
        if (rank != -1)
        {
          job = unit.Jobs[index].Param;
          return;
        }
      }
      job = (JobParam) null;
      rank = -1;
    }

    public SectionParam GetCurrentSectionParam()
    {
      SectionParam sectionParam = (SectionParam) null;
      if (!string.IsNullOrEmpty((string) GlobalVars.SelectedSection))
      {
        sectionParam = this.FindWorld((string) GlobalVars.SelectedSection);
        if (sectionParam != null && (string.IsNullOrEmpty(sectionParam.home) || sectionParam.hidden))
          sectionParam = (SectionParam) null;
      }
      if (sectionParam == null)
      {
        QuestParam lastStoryQuest = this.Player.FindLastStoryQuest();
        if (lastStoryQuest != null)
        {
          ChapterParam area = this.FindArea(lastStoryQuest.ChapterID);
          if (area != null)
            sectionParam = this.FindWorld(area.section);
        }
      }
      return sectionParam;
    }

    public void PostLogin()
    {
      this.mHasLoggedIn = true;
      this.mTutorialStep = 0;
      this.mLastStamina = this.Player.Stamina;
      this.mLastGold = (long) this.Player.Gold;
      this.mLastAbilityRankUpCount = this.Player.AbilityRankUpCountNum;
      this.Player.ClearItemFlags(ItemData.ItemFlags.NewItem | ItemData.ItemFlags.NewSkin);
      GlobalVars.SelectedUnitUniqueID.Set(0L);
      GlobalVars.SelectedJobUniqueID.Set(0L);
      GlobalVars.SelectedEquipmentSlot.Set(-1);
      GlobalVars.ResetVarsWithAttribute(typeof (GlobalVars.ResetOnLogin));
      this.mLastUpdateTime = TimeManager.ServerTime;
      this.Player.OnLoginCount();
      HomeWindow.EnterHomeCount = 0;
      MySmartBeat.SetupUserInfo();
    }

    public void NotifyAbilityRankUpCountChanged()
    {
      int abilityRankUpCountNum = this.Player.AbilityRankUpCountNum;
      this.mLastAbilityRankUpCount = abilityRankUpCountNum;
      this.OnAbilityRankUpCountChange(abilityRankUpCountNum);
    }

    private void UpdateResolution()
    {
      bool flag = false;
      if (GameManager.mUpscaleMode == flag)
        return;
      GameManager.mUpscaleMode = flag;
      int defaultScreenWidth = ScreenUtility.DefaultScreenWidth;
      int h = ScreenUtility.DefaultScreenHeight;
      if (flag)
      {
        float num1 = (float) defaultScreenWidth / (float) h;
        int num2 = Mathf.Min(h, 750);
        defaultScreenWidth = Mathf.FloorToInt(num1 * (float) num2);
        h = num2;
      }
      ScreenUtility.SetResolution(defaultScreenWidth, h);
      DebugUtility.Log(string.Format("Changing Resolution to [{0} x {1}]", (object) defaultScreenWidth, (object) h));
    }

    private void Update()
    {
      if (this.mHasLoggedIn)
      {
        DateTime serverTime = TimeManager.ServerTime;
        long num1 = serverTime.Ticks / 864000000000L;
        long num2 = this.mLastUpdateTime.Ticks / 864000000000L;
        this.mLastUpdateTime = serverTime;
        if (num1 - num2 < 1L)
          ;
        int stamina = this.Player.Stamina;
        if (stamina != this.mLastStamina)
        {
          this.mLastStamina = stamina;
          this.OnStaminaChange();
        }
        this.Player.UpdateAbilityRankUpCount();
        int abilityRankUpCountNum = this.Player.AbilityRankUpCountNum;
        if (abilityRankUpCountNum != this.mLastAbilityRankUpCount)
        {
          this.OnAbilityRankUpCountChange(abilityRankUpCountNum);
          this.mLastAbilityRankUpCount = abilityRankUpCountNum;
        }
        if ((long) this.Player.Gold != this.mLastGold)
        {
          this.mLastGold = (long) this.Player.Gold;
          this.OnPlayerCurrencyChange();
        }
        this.UpdateTrophy();
        if (this.mAudienceManager != null)
          this.mAudienceManager.Update();
      }
      this.EnableAnimationFrameSkipping = false;
      AnimationPlayer.MaxUpdateTime = !this.EnableAnimationFrameSkipping ? long.MaxValue : (long) ((double) Mathf.Lerp(1f / 1000f, 3f / 500f, 1f - Mathf.Clamp01((float) (((double) Time.get_unscaledDeltaTime() - 0.0260000005364418) / 0.00399999879300594))) * 10000000.0);
      this.UpdateTextureLoadRequests();
    }

    private void DayChanged()
    {
      if (this.Player == null)
        return;
      this.Player.ResetStaminaRecoverCount();
      this.Player.ResetBuyGoldNum();
      this.Player.ResetQuestChallengeResets();
      this.Player.ResetQuestChallenges();
      this.Player.UpdateTrophyStates();
    }

    public void StartBuyStaminaSequence(bool staminaLacking)
    {
      this.StartBuyStaminaSequence(staminaLacking, (PartyWindow2) null);
    }

    public void StartBuyStaminaSequence(bool _staminaLacking, PartyWindow2 _pwindow)
    {
      if (this.Player.IsHaveHealAPItems())
      {
        this.StartCoroutine(this.StartBuyStaminaSequence2(_pwindow));
      }
      else
      {
        FixParam fixParam = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
        string empty = string.Empty;
        string text;
        if (_staminaLacking)
          text = LocalizedText.Get("sys.STAMINA_NOT_ENOUGH", (object) this.Player.GetStaminaRecoveryCost(false), (object) fixParam.StaminaAdd);
        else
          text = LocalizedText.Get("sys.RESET_STAMINA", (object) this.Player.GetStaminaRecoveryCost(false), (object) fixParam.StaminaAdd);
        UIUtility.ConfirmBox(text, (string) null, (UIUtility.DialogResultEvent) (go => this.ContinueBuyStamina()), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
    }

    [DebuggerHidden]
    private IEnumerator StartBuyStaminaSequence2(PartyWindow2 _pwindow)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CStartBuyStaminaSequence2\u003Ec__IteratorD4()
      {
        _pwindow = _pwindow,
        \u003C\u0024\u003E_pwindow = _pwindow
      };
    }

    private void ContinueBuyStamina()
    {
      if (this.Player.StaminaBuyNum >= this.MasterParam.GetVipBuyStaminaLimit(this.Player.VipRank))
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.STAMINA_BUY_LIMIT"), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
      }
      else
      {
        if (!this.NotRequiredHeal() || !this.CoinShortage() || Network.Mode != Network.EConnectMode.Online)
          return;
        Network.RequestAPI((WebAPI) new ReqItemAddStmPaid(new Network.ResponseCallback(this.OnBuyStamina)), false);
      }
    }

    public bool NotRequiredHeal()
    {
      if (this.Player.StaminaStockCap > this.Player.Stamina)
        return true;
      UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.STAMINAFULL"), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
      return false;
    }

    public bool CoinShortage()
    {
      if (this.Player.Coin >= this.Player.GetStaminaRecoveryCost(false))
        return true;
      this.ConfirmBuyCoin((GameManager.BuyCoinEvent) null, (GameManager.BuyCoinEvent) null);
      return false;
    }

    private void OnBuyStamina(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.StaminaCoinShort)
          Network.RemoveAPI();
        else
          FlowNode_Network.Retry();
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        if (jsonObject.body == null)
        {
          FlowNode_Network.Retry();
        }
        else
        {
          int staminaRecoveryCost = this.Player.GetStaminaRecoveryCost(false);
          if (!this.Player.Deserialize(jsonObject.body.player, PlayerData.EDeserializeFlags.Coin | PlayerData.EDeserializeFlags.Stamina))
          {
            FlowNode_Network.Retry();
          }
          else
          {
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("BuyStamina", staminaRecoveryCost);
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.STAMINARECOVERED", new object[1]
            {
              (object) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaAdd
            }), (UIUtility.DialogResultEvent) (go => {}), (GameObject) null, false, -1);
            GlobalEvent.Invoke(PredefinedGlobalEvents.REFRESH_COIN_STATUS.ToString(), (object) this);
          }
        }
      }
    }

    public void ConfirmBuyCoin(GameManager.BuyCoinEvent onEnd, GameManager.BuyCoinEvent onCancel)
    {
      this.mOnBuyCoinEnd = onEnd;
      this.mOnBuyCoinCancel = onCancel;
      this.mBuyCoinWindow = UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), (string) null, (UIUtility.DialogResultEvent) (go => this.StartBuyCoinSequence()), new UIUtility.DialogResultEvent(this.OnBuyCoinConfirmCancel), (GameObject) null, false, -1);
    }

    private void OnBuyCoinEnd(GameObject go)
    {
      if (this.mOnBuyCoinEnd == null)
        return;
      GameManager.BuyCoinEvent mOnBuyCoinEnd = this.mOnBuyCoinEnd;
      this.mOnBuyCoinEnd = (GameManager.BuyCoinEvent) null;
      mOnBuyCoinEnd();
    }

    private void OnBuyCoinConfirmCancel(GameObject go)
    {
      if (this.mOnBuyCoinCancel == null)
        return;
      GameManager.BuyCoinEvent mOnBuyCoinCancel = this.mOnBuyCoinCancel;
      this.mOnBuyCoinCancel = (GameManager.BuyCoinEvent) null;
      mOnBuyCoinCancel();
    }

    public void StartBuyCoinSequence()
    {
      GameObject dialogBuyCoin = GameSettings.Instance.Dialog_BuyCoin;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dialogBuyCoin, (UnityEngine.Object) null))
        return;
      this.mBuyCoinWindow = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) dialogBuyCoin);
      this.mBuyCoinWindow.RequireComponent<DestroyEventListener>().Listeners += new DestroyEventListener.DestroyEvent(this.OnBuyCoinEnd);
    }

    public bool IsBuyCoinWindowOpen
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mBuyCoinWindow, (UnityEngine.Object) null))
          return true;
        this.mBuyCoinWindow = (GameObject) null;
        return false;
      }
    }

    public void RegisterImportantJob(Coroutine co)
    {
      this.mImportantJobs.Add(co);
      if (this.mImportantJobCoroutine != null)
        return;
      this.mImportantJobCoroutine = this.StartCoroutine(this.AsyncWaitForImportantJobs());
    }

    [DebuggerHidden]
    private IEnumerator AsyncWaitForImportantJobs()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CAsyncWaitForImportantJobs\u003Ec__IteratorD5()
      {
        \u003C\u003Ef__this = this
      };
    }

    public bool IsImportantJobRunning
    {
      get
      {
        return this.mImportantJobs.Count > 0;
      }
    }

    public bool PrepareSceneChange()
    {
      foreach (System.Delegate invocation in this.OnSceneChange.GetInvocationList())
      {
        if (!(invocation as GameManager.SceneChangeEvent)())
          return false;
      }
      return true;
    }

    public void ApplyTextureAsync(RawImage target, string path)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) target, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.mTextureRequests.Count; ++index)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTextureRequests[index].Target, (UnityEngine.Object) target))
        {
          if (!(this.mTextureRequests[index].Path != path))
            return;
          if (string.IsNullOrEmpty(path))
          {
            this.mTextureRequests[index].Target.set_texture((Texture) null);
            this.mTextureRequests.RemoveAt(index);
            return;
          }
          this.mTextureRequests[index].Request = (LoadRequest) null;
          this.mTextureRequests[index].Path = path;
          return;
        }
      }
      if (string.IsNullOrEmpty(path))
      {
        target.set_texture((Texture) null);
      }
      else
      {
        target.set_texture((Texture) null);
        GameManager.TextureRequest texReq = new GameManager.TextureRequest();
        texReq.Target = target;
        texReq.Path = path;
        this.mTextureRequests.Add(texReq);
        if (AssetManager.IsLoading)
          return;
        this.RequestTexture(texReq);
      }
    }

    private bool RequestTexture(GameManager.TextureRequest texReq)
    {
      texReq.Request = AssetManager.LoadAsync<Texture2D>(texReq.Path);
      if (!texReq.Request.isDone)
        return false;
      texReq.Target.set_texture((Texture) (texReq.Request.asset as Texture2D));
      return true;
    }

    public void CancelTextureLoadRequest(RawImage target)
    {
      for (int index = 0; index < this.mTextureRequests.Count; ++index)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTextureRequests[index].Target, (UnityEngine.Object) target))
        {
          this.mTextureRequests.RemoveAt(index);
          break;
        }
      }
    }

    private void UpdateTextureLoadRequests()
    {
      for (int index = 0; index < this.mTextureRequests.Count; ++index)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTextureRequests[index].Target, (UnityEngine.Object) null))
          this.mTextureRequests.RemoveAt(index--);
        else if (this.mTextureRequests[index].Request == null)
        {
          if (!AssetManager.IsLoading)
            this.RequestTexture(this.mTextureRequests[index]);
        }
        else if (this.mTextureRequests[index].Request.isDone)
        {
          this.mTextureRequests[index].Target.set_texture((Texture) (this.mTextureRequests[index].Request.asset as Texture2D));
          this.mTextureRequests.RemoveAt(index--);
        }
      }
    }

    public void CompleteTutorialStep()
    {
      ++this.mTutorialStep;
    }

    public string GetNextTutorialStep()
    {
      GameSettings instance = GameSettings.Instance;
      if (this.mTutorialStep >= instance.Tutorial_Steps.Length)
        return (string) null;
      return instance.Tutorial_Steps[this.mTutorialStep];
    }

    public void UpdateTutorialFlags(long add)
    {
      if ((this.Player.TutorialFlags & add) == add)
        return;
      this.Player.TutorialFlags |= add;
      if (Network.Mode == Network.EConnectMode.Offline)
        return;
      Network.RequestAPI((WebAPI) new ReqTutUpdate(this.Player.TutorialFlags, new Network.ResponseCallback(this.OnTutorialFlagUpdate)), false);
    }

    public void UpdateTutorialFlags(string flagName)
    {
      long tutorialFlagMask = GameSettings.Instance.CreateTutorialFlagMask(flagName);
      if (tutorialFlagMask == 0L)
        return;
      this.UpdateTutorialFlags(tutorialFlagMask);
    }

    public bool IsTutorialFlagSet(string flagName)
    {
      long tutorialFlagMask = GameSettings.Instance.CreateTutorialFlagMask(flagName);
      if (tutorialFlagMask != 0L)
        return (this.Player.TutorialFlags & tutorialFlagMask) != 0L;
      return false;
    }

    public int TutorialStep
    {
      get
      {
        return this.mTutorialStep;
      }
    }

    private void OnTutorialFlagUpdate(WWWResult www)
    {
      if (Network.IsError)
        FlowNode_Network.Retry();
      else
        Network.RemoveAPI();
    }

    public void DownloadAndTransitScene(string sceneName)
    {
      if (AssetManager.IsAssetBundle(sceneName))
      {
        CriticalSection.Enter(CriticalSections.SceneChange);
        this.StartCoroutine(this.DownloadAndTransitSceneAsync(sceneName));
      }
      else
        SceneManager.LoadScene(sceneName);
    }

    [DebuggerHidden]
    private IEnumerator DownloadAndTransitSceneAsync(string sceneName)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CDownloadAndTransitSceneAsync\u003Ec__IteratorD6()
      {
        sceneName = sceneName,
        \u003C\u0024\u003EsceneName = sceneName
      };
    }

    public void UpdateTutorialStep()
    {
      GameSettings instance = GameSettings.Instance;
      this.mTutorialStep = instance.Tutorial_Steps.Length;
      for (int index = instance.Tutorial_Steps.Length - 1; index >= 0; --index)
      {
        if (!string.IsNullOrEmpty(instance.Tutorial_Steps[index]))
        {
          QuestParam quest = this.FindQuest(instance.Tutorial_Steps[index]);
          if (quest != null && quest.state == QuestStates.Cleared)
            break;
          this.mTutorialStep = index;
        }
      }
    }

    public void OnAgreeTermsOfUse(object caller)
    {
      this.AgreedVer = Application.get_version();
    }

    public bool IsAgreeTermsOfUse()
    {
      if (this.AgreedVer != null)
        return this.AgreedVer.Length > 0;
      return false;
    }

    private void RefreshTutorialDLAssets()
    {
      if (this.mScannedTutorialAssets)
        return;
      this.mScannedTutorialAssets = true;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      AssetList.Item[] assets = AssetManager.AssetList.Assets;
      for (int index = 0; index < assets.Length; ++index)
      {
        if ((assets[index].Flags & AssetBundleFlags.Tutorial) != (AssetBundleFlags) 0 && ((assets[index].Flags & AssetBundleFlags.TutorialMovie) == (AssetBundleFlags) 0 || (!GameUtility.IsDebugBuild || GlobalVars.DebugIsPlayTutorial) && (instance.Player.TutorialFlags & 1L) == 0L) && !AssetManager.IsAssetInCache(assets[index].IDStr))
          this.mTutorialDLAssets.Add(assets[index]);
      }
      this.mTutorialDLAssets.Sort((Comparison<AssetList.Item>) ((x, y) =>
      {
        if (x.Size == y.Size)
          return 0;
        return x.Size > y.Size ? 1 : -1;
      }));
    }

    public bool HasTutorialDLAssets
    {
      get
      {
        if (!this.mScannedTutorialAssets)
          this.RefreshTutorialDLAssets();
        return this.mTutorialDLAssets.Count > 0;
      }
    }

    public void DownloadTutorialAssets()
    {
      for (int index = 0; index < this.mTutorialDLAssets.Count; ++index)
      {
        if (AssetManager.IsAssetInCache(this.mTutorialDLAssets[index].IDStr))
          this.mTutorialDLAssets.RemoveAt(index--);
        else
          AssetDownloader.Add(this.mTutorialDLAssets[index].IDStr);
      }
      this.mTutorialDLAssets.Clear();
    }

    public bool PartialDownloadTutorialAssets()
    {
      if (!AssetDownloader.isDone || this.mWaitDownloadThread != null || this.mTutorialDLAssets.Count <= 0)
        return false;
      List<AssetList.Item> queue = new List<AssetList.Item>();
      int networkBgdlChunkSize = GameSettings.Instance.Network_BGDLChunkSize;
      int num = 0;
      for (int index = 0; index < this.mTutorialDLAssets.Count; ++index)
      {
        if (AssetManager.IsAssetInCache(this.mTutorialDLAssets[index].IDStr))
        {
          this.mTutorialDLAssets.RemoveAt(index--);
        }
        else
        {
          queue.Add(this.mTutorialDLAssets[index]);
          AssetDownloader.Add(this.mTutorialDLAssets[index].IDStr);
          num += this.mTutorialDLAssets[index].Size;
          if (num >= networkBgdlChunkSize)
            break;
        }
      }
      if (num <= 0)
        return false;
      AssetDownloader.DownloadState state = AssetDownloader.StartDownload(false, false, ThreadPriority.Lowest);
      if (state == null)
        return false;
      this.mWaitDownloadThread = this.StartCoroutine(this.WaitDownloadAsync(queue, state));
      return true;
    }

    [DebuggerHidden]
    private IEnumerator WaitDownloadAsync(List<AssetList.Item> queue, AssetDownloader.DownloadState state)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CWaitDownloadAsync\u003Ec__IteratorD7()
      {
        state = state,
        queue = queue,
        \u003C\u0024\u003Estate = state,
        \u003C\u0024\u003Equeue = queue,
        \u003C\u003Ef__this = this
      };
    }

    private void OnApplicationFocus(bool focus)
    {
      MyLocalNotification.CancelStamina();
      if (FlowNode_GetCurrentScene.IsAfterLogin())
        MyLocalNotification.SetStamina(this.MasterParam.LocalNotificationParam, this.Player);
      List<LocalNotificationInfo> localoNotifications = MyLocalNotification.LocaloNotifications;
      if (focus)
      {
        LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_MORNING);
        LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_NOON);
        LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_AFTERNOON);
        if (localoNotifications == null || localoNotifications.Count <= 0)
          return;
        using (List<LocalNotificationInfo>.Enumerator enumerator = localoNotifications.GetEnumerator())
        {
          while (enumerator.MoveNext())
            LocalNotification.CancelNotificationsWithCategory(enumerator.Current.trophy_iname);
        }
      }
      else
      {
        TrophyParam[] trophies = MonoSingleton<GameManager>.Instance.Trophies;
        if (localoNotifications == null || localoNotifications.Count <= 0 || trophies == null)
          return;
        using (List<LocalNotificationInfo>.Enumerator enumerator = localoNotifications.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            LocalNotificationInfo lparam = enumerator.Current;
            TrophyParam trophyParam = Array.Find<TrophyParam>(trophies, (Predicate<TrophyParam>) (p => p.iname == lparam.trophy_iname));
            if (trophyParam != null && lparam.push_flg != 0)
            {
              string pushWord = lparam.push_word;
              string trophyIname = lparam.trophy_iname;
              for (int index = trophyParam.Objectives.Length - 1; index >= 0; --index)
              {
                int hour = int.Parse(trophyParam.Objectives[index].sval_base.Substring(0, 2));
                MyLocalNotification.SetRegular(new RegularLocalNotificationParam(pushWord, trophyIname, hour, 0, 0), this.Player);
              }
            }
          }
        }
      }
    }

    public void LoadUpdateTrophyList()
    {
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY))
        return;
      string s = PlayerPrefsUtility.GetString(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY, string.Empty);
      byte[] data = !string.IsNullOrEmpty(s) ? Convert.FromBase64String(s) : (byte[]) null;
      string src;
      try
      {
        src = data != null ? MyEncrypt.Decrypt(GameManager.SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY, data, false) : (string) null;
      }
      catch (Exception ex)
      {
        src = string.Empty;
      }
      this.mSavedUpdateTrophyListString = src;
      JSON_TrophyResponse jsonTrophyResponse;
      try
      {
        jsonTrophyResponse = !string.IsNullOrEmpty(src) ? JSONParser.parseJSONObject<JSON_TrophyResponse>(src) : (JSON_TrophyResponse) null;
      }
      catch (Exception ex)
      {
        jsonTrophyResponse = (JSON_TrophyResponse) null;
      }
      JSON_TrophyProgress[] trophyprogs = jsonTrophyResponse?.trophyprogs;
      if (trophyprogs == null || trophyprogs.Length <= 0)
        return;
      TrophyState[] trophyStates = this.Player.TrophyStates;
      for (int index1 = 0; index1 < trophyprogs.Length; ++index1)
      {
        if (trophyprogs[index1] != null)
        {
          TrophyParam trophy = this.MasterParam.GetTrophy(trophyprogs[index1].iname);
          if (trophy != null)
          {
            if (trophyStates != null)
            {
              TrophyState server = (TrophyState) null;
              for (int index2 = 0; index2 < trophyStates.Length; ++index2)
              {
                if (trophyStates[index2].iname == trophyprogs[index1].iname)
                {
                  server = trophyStates[index2];
                  break;
                }
              }
              if (server != null && !this.IsSavedUpdateTrophyStateNeedToSend(server, trophyprogs[index1].ymd, trophyprogs[index1].pts, trophyprogs[index1].rewarded_at != 0))
                continue;
            }
            TrophyState trophyCounter = this.Player.GetTrophyCounter(trophy, false);
            if (trophyCounter != null)
            {
              trophyCounter.StartYMD = trophyprogs[index1].ymd;
              if (trophyprogs[index1].pts != null)
              {
                for (int index2 = 0; index2 < trophyprogs[index1].pts.Length; ++index2)
                  trophyCounter.Count[index2] = trophyprogs[index1].pts[index2];
              }
              trophyCounter.IsEnded = trophyprogs[index1].rewarded_at != 0;
              trophyCounter.IsDirty = true;
              DebugUtility.LogWarning("LoadSavedTrophy: " + trophyprogs[index1].iname + " / " + trophy.Name);
            }
          }
        }
      }
      this.Player.UpdateTrophyStates();
    }

    public void SaveUpdateTrophyList(List<TrophyState> updateList)
    {
      string msg = (string) null;
      if (updateList != null && updateList.Count > 0)
      {
        string str = "{\"trophyprogs\":[";
        bool flag = true;
        using (List<TrophyState>.Enumerator enumerator = updateList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TrophyState current = enumerator.Current;
            if (current != null)
            {
              if (flag)
                flag = false;
              else
                str += ",";
              str += "{";
              str = str + "\"iname\":\"" + current.iname + "\"";
              if (current.Count != null)
              {
                str += ",\"pts\":[";
                for (int index = 0; index < current.Count.Length; ++index)
                {
                  if (index > 0)
                    str += ",";
                  str += (string) (object) current.Count[index];
                }
                str += "]";
              }
              str = str + ",\"ymd\":" + (object) current.StartYMD;
              str += "}";
            }
          }
        }
        msg = str + "]}";
      }
      if (this.mSavedUpdateTrophyListString == msg)
        return;
      byte[] inArray = !string.IsNullOrEmpty(msg) ? MyEncrypt.Encrypt(GameManager.SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY, msg, false) : (byte[]) null;
      string str1 = inArray != null ? Convert.ToBase64String(inArray) : string.Empty;
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY, str1, true);
      this.mSavedUpdateTrophyListString = msg;
      DebugUtility.LogWarning("SaveTrophy:" + (msg ?? "null"));
    }

    private bool IsSavedUpdateTrophyStateNeedToSend(TrophyState server, int ymd, int[] count, bool isEnded)
    {
      if (server == null || this.MasterParam == null)
        return false;
      TrophyParam trophy = this.MasterParam.GetTrophy(server.iname);
      if (trophy == null)
        return false;
      int num1 = trophy.Objectives != null ? trophy.Objectives.Length : 0;
      int num2 = count != null ? count.Length : 0;
      int num3 = server.Count != null ? server.Count.Length : 0;
      if (num2 != num1 || num2 != num3)
        return false;
      bool flag1 = true;
      for (int index = 0; index < num2; ++index)
      {
        if (count[index] < trophy.Objectives[index].RequiredCount)
        {
          flag1 = false;
          break;
        }
      }
      bool flag2 = false;
      if (ymd > server.StartYMD)
        flag2 = true;
      else if (ymd >= server.StartYMD && (!server.IsEnded || !isEnded) && !server.IsEnded)
      {
        if (isEnded)
          flag2 = true;
        else if ((!server.IsCompleted || !flag1) && !server.IsCompleted)
        {
          if (flag1)
          {
            flag2 = true;
          }
          else
          {
            int num4 = 0;
            int num5 = 0;
            for (int index = 0; index < num2; ++index)
            {
              num4 += server.Count[index];
              num5 += count[index];
            }
            if (num4 < num5)
              flag2 = true;
          }
        }
      }
      return flag2;
    }

    public void CreateUpdateTrophyList(out List<TrophyState> updateTrophyList, out List<TrophyState> updateChallengeList, out List<TrophyState> updateTrophyAward)
    {
      updateTrophyList = new List<TrophyState>();
      updateChallengeList = new List<TrophyState>();
      updateTrophyAward = new List<TrophyState>();
      TrophyState[] trophyStates = this.Player.TrophyStates;
      if (trophyStates == null)
        return;
      for (int index = 0; index < trophyStates.Length; ++index)
      {
        if ((trophyStates[index].IsDirty || trophyStates[index].IsSending) && !trophyStates[index].IsEnded)
        {
          TrophyParam trophy = this.MasterParam.GetTrophy(trophyStates[index].iname);
          if (trophy != null)
          {
            if (trophy.IsChallengeMission)
              updateChallengeList.Add(trophyStates[index]);
            else if (trophyStates[index].Param.DispType == TrophyDispType.Award && trophyStates[index].IsCompleted)
              updateTrophyAward.Add(trophyStates[index]);
            else
              updateTrophyList.Add(trophyStates[index]);
          }
        }
      }
    }

    public bool IsExternalPermit()
    {
      return string.IsNullOrEmpty(FlowNode_Variable.Get("IS_EXTERNAL_API_PERMIT"));
    }

    private void UpdateTrophy()
    {
      if (Network.Mode != Network.EConnectMode.Online || !this.update_trophy_interval.PlayCheckUpdate() || (this.update_trophy_lock.IsLock || !this.IsExternalPermit()) || CriticalSection.IsActive)
        return;
      this.update_trophy_interval.SetUpdateInterval();
      this.UpdateTrophyAPI();
    }

    public void UpdateTrophyAPI()
    {
      List<TrophyState> updateTrophyList;
      List<TrophyState> updateChallengeList;
      List<TrophyState> updateTrophyAward;
      this.CreateUpdateTrophyList(out updateTrophyList, out updateChallengeList, out updateTrophyAward);
      List<TrophyState> updateList = new List<TrophyState>(updateTrophyList.Count + updateChallengeList.Count + updateTrophyAward.Count);
      updateList.AddRange((IEnumerable<TrophyState>) updateTrophyList);
      updateList.AddRange((IEnumerable<TrophyState>) updateChallengeList);
      updateList.AddRange((IEnumerable<TrophyState>) updateTrophyAward);
      this.SaveUpdateTrophyList(updateList);
      if (updateList.Count <= 0)
        return;
      if (updateTrophyList.Count > 0)
      {
        this.mUpdateTrophyList = updateTrophyList;
        using (List<TrophyState>.Enumerator enumerator = this.mUpdateTrophyList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TrophyState current = enumerator.Current;
            current.IsDirty = false;
            current.IsSending = true;
          }
        }
        Network.RequestAPI((WebAPI) new ReqUpdateTrophy(this.mUpdateTrophyList, new Network.ResponseCallback(this.UpdateTrophyResponseCallback), false), false);
      }
      if (updateChallengeList.Count > 0)
      {
        this.mUpdateChallengeList = updateChallengeList;
        using (List<TrophyState>.Enumerator enumerator = this.mUpdateChallengeList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TrophyState current = enumerator.Current;
            current.IsDirty = false;
            current.IsSending = true;
          }
        }
        Network.RequestAPI((WebAPI) new ReqUpdateBingo(this.mUpdateChallengeList, new Network.ResponseCallback(this.UpdateChallengeResponseCallback), false), false);
      }
      if (updateTrophyAward.Count <= 0)
        return;
      this.mUpdateTrophyAward = updateTrophyAward;
      using (List<TrophyState>.Enumerator enumerator = this.mUpdateTrophyAward.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TrophyState current = enumerator.Current;
          current.IsDirty = false;
          current.IsSending = true;
        }
      }
      Network.RequestAPI((WebAPI) new ReqUpdateTrophy(this.mUpdateTrophyAward, new Network.ResponseCallback(this.UpdateTrophyAwardResponseCallback), true), false);
    }

    private void UpdateTrophyResponseCallback(WWWResult www)
    {
      this.update_trophy_interval.SetSyncInterval();
      if (Network.IsError)
      {
        FlowNode_Network.Retry();
      }
      else
      {
        using (List<TrophyState>.Enumerator enumerator = this.mUpdateTrophyList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TrophyState current = enumerator.Current;
            current.IsSending = false;
            if (current.IsCompleted)
            {
              if (current.Param.IsDaily)
                NotifyList.PushDailyTrophy(current.Param);
              else
                NotifyList.PushTrophy(current.Param);
            }
          }
        }
        this.mUpdateTrophyList = (List<TrophyState>) null;
        this.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        Network.RemoveAPI();
      }
    }

    private void UpdateChallengeResponseCallback(WWWResult www)
    {
      this.update_trophy_interval.SetSyncInterval();
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.BingoOutofDateReceive)
        {
          Network.ResetError();
        }
        else
        {
          FlowNode_Network.Retry();
          return;
        }
      }
      using (List<TrophyState>.Enumerator enumerator = this.mUpdateChallengeList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TrophyState current = enumerator.Current;
          current.IsSending = false;
          if (current.IsCompleted)
          {
            if (current.Param.IsDaily)
              NotifyList.PushDailyTrophy(current.Param);
            else
              NotifyList.PushTrophy(current.Param);
          }
        }
      }
      this.mUpdateChallengeList = (List<TrophyState>) null;
      Network.RemoveAPI();
    }

    private void UpdateTrophyAwardResponseCallback(WWWResult www)
    {
      this.update_trophy_interval.SetSyncInterval();
      if (Network.IsError)
      {
        FlowNode_Network.Retry();
      }
      else
      {
        using (List<TrophyState>.Enumerator enumerator = this.mUpdateTrophyAward.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TrophyState current = enumerator.Current;
            current.IsSending = false;
            if (current.IsCompleted)
              NotifyList.PushAward(current.Param);
          }
        }
        this.mUpdateTrophyAward = (List<TrophyState>) null;
        Network.RemoveAPI();
        WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        if (jsonObject.body == null)
          return;
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
        }
        catch (Exception ex)
        {
          DebugUtility.LogException(ex);
        }
      }
    }

    public void ServerSyncTrophyExecStart(out string trophy_progs, out string bingo_progs)
    {
      trophy_progs = string.Empty;
      bingo_progs = string.Empty;
      if (this.is_start_server_sync_trophy_exec)
      {
        DebugUtility.Log("ServerSyncTrophyExecBegin が連続で呼ばれています。");
      }
      else
      {
        this.is_start_server_sync_trophy_exec = true;
        MonoSingleton<GameManager>.Instance.update_trophy_lock.Lock();
        MonoSingleton<GameManager>.Instance.CreateUpdateTrophyList(out this.mServerSyncTrophyList, out this.mServerSyncChallengeList, out this.mServerSyncTrophyAward);
        if (0 >= this.mServerSyncTrophyList.Count + this.mServerSyncChallengeList.Count + this.mServerSyncTrophyAward.Count)
          return;
        if (0 < this.mServerSyncTrophyList.Count || 0 < this.mServerSyncTrophyAward.Count)
        {
          ReqUpdateTrophy reqUpdateTrophy = new ReqUpdateTrophy();
          reqUpdateTrophy.BeginTrophyReqString();
          reqUpdateTrophy.AddTrophyReqString(this.mServerSyncTrophyList, false);
          reqUpdateTrophy.AddTrophyReqString(this.mServerSyncTrophyAward, true);
          reqUpdateTrophy.EndTrophyReqString();
          trophy_progs = reqUpdateTrophy.GetTrophyReqString();
          using (List<TrophyState>.Enumerator enumerator = this.mServerSyncTrophyList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TrophyState current = enumerator.Current;
              current.IsDirty = false;
              current.IsSending = false;
            }
          }
          using (List<TrophyState>.Enumerator enumerator = this.mServerSyncTrophyAward.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TrophyState current = enumerator.Current;
              current.IsDirty = false;
              current.IsSending = false;
            }
          }
        }
        if (0 >= this.mServerSyncChallengeList.Count)
          return;
        ReqUpdateBingo reqUpdateBingo = new ReqUpdateBingo();
        reqUpdateBingo.BeginBingoReqString();
        reqUpdateBingo.AddBingoReqString(this.mServerSyncChallengeList, false);
        reqUpdateBingo.EndBingoReqString();
        bingo_progs = reqUpdateBingo.GetBingoReqString();
        using (List<TrophyState>.Enumerator enumerator = this.mServerSyncChallengeList.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            TrophyState current = enumerator.Current;
            current.IsDirty = false;
            current.IsSending = false;
          }
        }
      }
    }

    public void ServerSyncTrophyExecEnd(WWWResult www)
    {
      if (!this.is_start_server_sync_trophy_exec)
      {
        DebugUtility.Log("ServerSyncTrophyExecBegin が呼ばれていません。");
      }
      else
      {
        this.is_start_server_sync_trophy_exec = false;
        if (this.mServerSyncTrophyList == null || this.mServerSyncChallengeList == null || this.mServerSyncTrophyAward == null)
          return;
        MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncInterval();
        List<TrophyState> updateList = new List<TrophyState>(this.mServerSyncTrophyList.Count + this.mServerSyncChallengeList.Count + this.mServerSyncTrophyAward.Count);
        updateList.AddRange((IEnumerable<TrophyState>) this.mServerSyncTrophyList);
        updateList.AddRange((IEnumerable<TrophyState>) this.mServerSyncChallengeList);
        updateList.AddRange((IEnumerable<TrophyState>) this.mServerSyncTrophyAward);
        MonoSingleton<GameManager>.Instance.SaveUpdateTrophyList(updateList);
        if (this.mServerSyncTrophyList.Count > 0)
        {
          using (List<TrophyState>.Enumerator enumerator = this.mServerSyncTrophyList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TrophyState current = enumerator.Current;
              current.IsDirty = false;
              current.IsSending = false;
              if (current.IsCompleted)
              {
                if (current.Param.IsDaily)
                  NotifyList.PushDailyTrophy(current.Param);
                else
                  NotifyList.PushTrophy(current.Param);
              }
            }
          }
        }
        if (this.mServerSyncChallengeList.Count > 0)
        {
          using (List<TrophyState>.Enumerator enumerator = this.mServerSyncChallengeList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TrophyState current = enumerator.Current;
              current.IsDirty = false;
              current.IsSending = false;
              if (current.IsCompleted)
                NotifyList.PushTrophy(current.Param);
            }
          }
        }
        if (this.mServerSyncTrophyAward.Count > 0)
        {
          using (List<TrophyState>.Enumerator enumerator = this.mServerSyncTrophyAward.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TrophyState current = enumerator.Current;
              current.IsDirty = false;
              current.IsSending = false;
              if (current.IsCompleted)
                NotifyList.PushAward(current.Param);
            }
          }
          try
          {
            WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(www.text);
            DebugUtility.Assert(jsonObject != null, "res == null");
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.player);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.items);
            MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
        MonoSingleton<GameManager>.Instance.RequestUpdateBadges(GameManager.BadgeTypes.DailyMission);
        this.mServerSyncTrophyList = (List<TrophyState>) null;
        this.mServerSyncChallengeList = (List<TrophyState>) null;
        this.mServerSyncTrophyAward = (List<TrophyState>) null;
        MonoSingleton<GameManager>.Instance.update_trophy_lock.Unlock();
      }
    }

    public void AddCharacterQuestPopup(UnitData unit)
    {
      if (unit == null)
        return;
      if (this.mCharacterQuestUnits == null)
        this.mCharacterQuestUnits = new List<UnitData>();
      else if (this.mCharacterQuestUnits.Exists((Predicate<UnitData>) (u => u.UnitID == unit.UnitID)))
        return;
      this.mCharacterQuestUnits.Add(unit);
    }

    public void ShowCharacterQuestPopup(string template)
    {
      this.StartCoroutine(this.ShowCharacterQuestPopupAsync(template));
    }

    [DebuggerHidden]
    public IEnumerator ShowCharacterQuestPopupAsync(string template)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CShowCharacterQuestPopupAsync\u003Ec__IteratorD8()
      {
        template = template,
        \u003C\u0024\u003Etemplate = template,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    public IEnumerator SkinUnlockPopup(ArtifactParam unlockSkin)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CSkinUnlockPopup\u003Ec__IteratorD9()
      {
        unlockSkin = unlockSkin,
        \u003C\u0024\u003EunlockSkin = unlockSkin
      };
    }

    [DebuggerHidden]
    public IEnumerator SkinUnlockPopup(ItemParam[] rewardItems)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GameManager.\u003CSkinUnlockPopup\u003Ec__IteratorDA()
      {
        rewardItems = rewardItems,
        \u003C\u0024\u003ErewardItems = rewardItems
      };
    }

    public long LimitedShopEndAt
    {
      get
      {
        return this.mLimitedShopEndAt;
      }
      set
      {
        this.mLimitedShopEndAt = value;
      }
    }

    public JSON_ShopListArray.Shops[] LimitedShopList
    {
      get
      {
        return this.mLimitedShopList;
      }
      set
      {
        this.mLimitedShopList = value;
      }
    }

    public bool IsLimitedShopOpen
    {
      set
      {
        this.mIsLimitedShopOpen = value;
      }
      get
      {
        return this.mIsLimitedShopOpen;
      }
    }

    public void Deserialize(ReqMultiRank.Json_MultiRank json)
    {
      if (json == null)
        return;
      this.mMultiUnitRank = (List<MultiRanking>) null;
      this.mMultiUnitRank = new List<MultiRanking>();
      if (json.isReady != 1)
        return;
      for (int index = 0; index < json.ranking.Length; ++index)
        this.mMultiUnitRank.Add(new MultiRanking()
        {
          unit = json.ranking[index].unit_iname,
          job = json.ranking[index].job_iname
        });
    }

    public void Deserialize(RankingData[] datas)
    {
      if (this.mUnitRanking == null)
        this.mUnitRanking = new Dictionary<string, RankingData>();
      this.mUnitRanking.Clear();
      for (int index = 0; index < datas.Length; ++index)
      {
        if (datas[index] != null)
          this.mUnitRanking[datas[index].iname] = datas[index];
      }
    }

    public List<MultiRanking> MultiUnitRank
    {
      get
      {
        return this.mMultiUnitRank;
      }
    }

    public Dictionary<string, RankingData> UnitRanking
    {
      get
      {
        return this.mUnitRanking;
      }
    }

    public void CreateMatchingRange()
    {
      if (this.mFreeMatchRange != null)
        return;
      this.mFreeMatchRange = new GameManager.VersusRange[6]
      {
        new GameManager.VersusRange(1, 20),
        new GameManager.VersusRange(21, 40),
        new GameManager.VersusRange(41, 60),
        new GameManager.VersusRange(61, 84),
        new GameManager.VersusRange(85, 120),
        new GameManager.VersusRange(121, -1)
      };
    }

    public string GetVersusKey(VERSUS_TYPE type)
    {
      string str = string.Empty;
      VersusMatchingParam[] versusMatchingParam = this.mMasterParam.GetVersusMatchingParam();
      switch (type)
      {
        case VERSUS_TYPE.Free:
          int num = this.Player.CalcLevel();
          this.CreateMatchingRange();
          for (int index = 0; index < this.mFreeMatchRange.Length; ++index)
          {
            if (num >= this.mFreeMatchRange[index].min && (this.mFreeMatchRange[index].max == -1 || num <= this.mFreeMatchRange[index].max))
            {
              str = VersusMatchingParam.CalcHash("key" + string.Format("{0:D2}", (object) index));
              break;
            }
          }
          break;
        case VERSUS_TYPE.Tower:
          for (int index = 0; index < versusMatchingParam.Length; ++index)
          {
            if (versusMatchingParam[index].MatchKey == VersusMatchingParam.TOWER_KEY)
            {
              str = versusMatchingParam[index].MatchKeyHash;
              break;
            }
          }
          break;
        case VERSUS_TYPE.Friend:
          for (int index = 0; index < versusMatchingParam.Length; ++index)
          {
            if (versusMatchingParam[index].MatchKey == VersusMatchingParam.FRIEND_KEY)
            {
              str = versusMatchingParam[index].MatchKeyHash;
              break;
            }
          }
          break;
      }
      return str;
    }

    public void GetTowerMatchItems(int floor, List<string> Items, List<int> Nums, bool bWin)
    {
      VersusTowerParam[] versusTowerParam = this.GetVersusTowerParam();
      if (versusTowerParam == null || floor < 0 || floor >= versusTowerParam.Length)
        return;
      if (bWin && versusTowerParam[floor].WinIteminame != null)
      {
        for (int index = 0; index < versusTowerParam[floor].WinIteminame.Length; ++index)
        {
          Items.Add((string) versusTowerParam[floor].WinIteminame[index]);
          Nums.Add((int) versusTowerParam[floor].WinItemNum[index]);
        }
      }
      else
      {
        if (versusTowerParam[floor].JoinIteminame == null)
          return;
        for (int index = 0; index < versusTowerParam[floor].JoinIteminame.Length; ++index)
        {
          Items.Add((string) versusTowerParam[floor].JoinIteminame[index]);
          Nums.Add((int) versusTowerParam[floor].JoinItemNum[index]);
        }
      }
    }

    public void GetVersusTopFloorItems(int floor, List<string> Items, List<int> Nums)
    {
      VersusTowerParam[] versusTowerParam = this.GetVersusTowerParam();
      if (versusTowerParam == null || floor < 0 || (floor >= versusTowerParam.Length || versusTowerParam[floor].SpIteminame == null))
        return;
      for (int index = 0; index < versusTowerParam[floor].SpIteminame.Length; ++index)
      {
        Items.Add((string) versusTowerParam[floor].SpIteminame[index]);
        Nums.Add((int) versusTowerParam[floor].SpItemnum[index]);
      }
    }

    public VersusTowerParam[] GetVersusTowerParam()
    {
      return this.mVersusTowerFloor.ToArray();
    }

    public VersusTowerParam GetCurrentVersusTowerParam(int idx = -1)
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int floor = idx == -1 ? player.VersusTowerFloor : idx;
      string iname = this.VersusTowerMatchName;
      if (!string.IsNullOrEmpty(iname))
        return this.mVersusTowerFloor.Find((Predicate<VersusTowerParam>) (data =>
        {
          if ((string) data.VersusTowerID == iname)
            return (int) data.Floor == floor;
          return false;
        }));
      return (VersusTowerParam) null;
    }

    public void GetRankMatchCondition(out int lrange, out int frange)
    {
      lrange = -1;
      frange = -1;
      int versusTowerFloor = this.Player.VersusTowerFloor;
      VersusMatchCondParam[] matchingCondition = this.mMasterParam.GetVersusMatchingCondition();
      if (matchingCondition == null || versusTowerFloor < 0 || versusTowerFloor >= matchingCondition.Length)
        return;
      lrange = (int) matchingCondition[versusTowerFloor].LvRange;
      frange = (int) matchingCondition[versusTowerFloor].FloorRange;
    }

    public void InitAlterHash(string digest = null)
    {
      if (string.IsNullOrEmpty(digest))
        return;
      this.DigestHash = digest;
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH))
        return;
      this.PrevCheckHash = PlayerPrefsUtility.GetString(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH, string.Empty);
    }

    public VersusScheduleParam FindVersusTowerScheduleParam(string towerID)
    {
      if (string.IsNullOrEmpty(towerID))
        return (VersusScheduleParam) null;
      return this.mVersusScheduleParam.Find((Predicate<VersusScheduleParam>) (data => string.Equals(data.tower_iname, towerID)));
    }

    public bool ExistsOpenVersusTower(string towerID = null)
    {
      List<VersusScheduleParam> all = this.mVersusScheduleParam.FindAll((Predicate<VersusScheduleParam>) (data => data.IsOpen));
      if (all.Count < 1)
        return false;
      if (string.IsNullOrEmpty(towerID))
        return true;
      return all.Find((Predicate<VersusScheduleParam>) (data => string.Equals(data.tower_iname, towerID))) != null;
    }

    public VersusCoinParam GetVersusCoinParam(string iname)
    {
      if (this.mVersusCoinParam != null)
        return this.mVersusCoinParam.Find((Predicate<VersusCoinParam>) (x => x.iname == iname));
      return (VersusCoinParam) null;
    }

    public VersusFriendScore[] GetVersusFriendScore(int floor)
    {
      VersusTowerParam versusTowerParam = this.GetCurrentVersusTowerParam(floor);
      VersusFriendScore[] versusFriendInfo = this.VersusFriendInfo;
      List<VersusFriendScore> versusFriendScoreList = new List<VersusFriendScore>();
      if (versusTowerParam != null && versusFriendInfo != null)
      {
        for (int index = 0; index < versusFriendInfo.Length; ++index)
        {
          if (string.Compare((string) versusTowerParam.FloorName, versusFriendInfo[index].floor) == 0)
            versusFriendScoreList.Add(versusFriendInfo[index]);
        }
      }
      return versusFriendScoreList.ToArray();
    }

    public bool IsVersusMode()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      bool flag = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        flag &= instance.CurrentState == MyPhoton.MyState.ROOM;
      return flag & GlobalVars.SelectedMultiPlayRoomType == JSON_MyPhotonRoomParam.EType.VERSUS & GlobalVars.SelectedMultiPlayVersusType != VERSUS_TYPE.Friend;
    }

    public List<MultiTowerFloorParam> GetMTAllFloorParam(string type)
    {
      List<MultiTowerFloorParam> multiTowerFloorParamList = new List<MultiTowerFloorParam>();
      for (int index = 0; index < this.mMultiTowerFloor.Count; ++index)
      {
        if (this.mMultiTowerFloor[index].tower_id == type)
          multiTowerFloorParamList.Add(this.mMultiTowerFloor[index]);
      }
      return multiTowerFloorParamList;
    }

    public MultiTowerFloorParam GetMTFloorParam(string type, int floor)
    {
      return this.mMultiTowerFloor.Find((Predicate<MultiTowerFloorParam>) (data =>
      {
        if ((int) data.floor == floor)
          return data.tower_id == type;
        return false;
      }));
    }

    public MultiTowerFloorParam GetMTFloorParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (MultiTowerFloorParam) null;
      int length = iname.LastIndexOf('_');
      int result = -1;
      if (int.TryParse(iname.Substring(length + 1), out result))
        return this.GetMTFloorParam(iname.Substring(0, length), result);
      return (MultiTowerFloorParam) null;
    }

    public List<MultiTowerRewardItem> GetMTFloorReward(string iname, int round)
    {
      return this.mMultiTowerRewards.Find((Predicate<MultiTowerRewardParam>) (data => data.iname == iname))?.GetReward(round);
    }

    public int GetMTRound(int floor)
    {
      int index = floor - 1;
      if (this.mMultiTowerRound == null || this.mMultiTowerRound.Round == null || this.mMultiTowerRound.Round.Count <= index)
        return 1;
      return this.mMultiTowerRound.Round[index] + 1;
    }

    public int GetMTClearedMaxFloor()
    {
      if (this.mMultiTowerRound == null)
        return 0;
      return this.mMultiTowerRound.Now;
    }

    public int GetMTChallengeFloor()
    {
      if (this.mMultiTowerRound == null)
        return 1;
      List<MultiTowerFloorParam> mtAllFloorParam = this.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
      int num = this.mMultiTowerRound.Now + 1;
      if (mtAllFloorParam != null && mtAllFloorParam.Count > 0)
        num = Mathf.Clamp(num, 1, (int) mtAllFloorParam[mtAllFloorParam.Count - 1].floor);
      return num;
    }

    public void AddMTQuest(string iname, QuestParam param)
    {
      if (this.mQuests == null || this.mQuestsDict == null || this.mQuestsDict.ContainsKey(iname))
        return;
      this.mQuests.Add(param);
      this.mQuestsDict.Add(iname, param);
    }

    public void Deserialize(ReqMultiTwStatus.FloorParam[] param)
    {
      if (this.mMultiTowerRound == null)
        this.mMultiTowerRound = new MultiTowerRoundParam();
      this.mMultiTowerRound.Now = 0;
      if (this.mMultiTowerRound.Round == null)
        this.mMultiTowerRound.Round = new List<int>();
      this.mMultiTowerRound.Round.Clear();
      if (param == null)
        return;
      for (int index = 0; index < param.Length; ++index)
        this.mMultiTowerRound.Round.Add(param[index].clear_count);
      this.mMultiTowerRound.Now = param[param.Length - 1].floor;
    }

    public SRPG.MapEffectParam GetMapEffectParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (SRPG.MapEffectParam) null;
      if (this.mMapEffectParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetMapEffectParam no data!</color>"));
        return (SRPG.MapEffectParam) null;
      }
      SRPG.MapEffectParam mapEffectParam = this.mMapEffectParam.Find((Predicate<SRPG.MapEffectParam>) (d => d.Iname == iname));
      if (mapEffectParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetMapEffectParam data not found! iname={0}</color>", (object) iname));
      return mapEffectParam;
    }

    public WeatherSetParam GetWeatherSetParam(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (WeatherSetParam) null;
      if (this.mWeatherSetParam == null)
      {
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetWeatherSetParam no data!</color>"));
        return (WeatherSetParam) null;
      }
      WeatherSetParam weatherSetParam = this.mWeatherSetParam.Find((Predicate<WeatherSetParam>) (d => d.Iname == iname));
      if (weatherSetParam == null)
        DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetWeatherSetParam data not found! iname={0}</color>", (object) iname));
      return weatherSetParam;
    }

    public RankingQuestParam FindAvailableRankingQuest(string iname)
    {
      return this.mAvailableRankingQuesstParams.Find((Predicate<RankingQuestParam>) (param => param.iname == iname));
    }

    public List<string> GetMultiAreaQuestList()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        QuestParam mQuest = this.mQuests[index];
        if (mQuest != null && mQuest.gps_enable && mQuest.IsMultiAreaQuest)
          stringList.Add(mQuest.iname);
      }
      return stringList;
    }

    public bool IsValidAreaQuest()
    {
      bool flag = false;
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        QuestParam mQuest = this.mQuests[index];
        if (mQuest != null && mQuest.IsGps)
          flag |= mQuest.IsDateUnlock(-1L);
      }
      return flag;
    }

    public bool IsValidMultiAreaQuest()
    {
      bool flag = false;
      for (int index = 0; index < this.mQuests.Count; ++index)
      {
        QuestParam mQuest = this.mQuests[index];
        if (mQuest != null && mQuest.IsMultiAreaQuest)
          flag |= mQuest.IsDateUnlock(-1L);
      }
      return flag;
    }

    public VersusFirstWinBonusParam GetVSFirstWinBonus(long servertime = -1)
    {
      VersusFirstWinBonusParam firstWinBonusParam = (VersusFirstWinBonusParam) null;
      DateTime dateTime = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      for (int index = 0; index < this.mVersusFirstWinBonus.Count; ++index)
      {
        VersusFirstWinBonusParam versusFirstWinBonu = this.mVersusFirstWinBonus[index];
        if (dateTime >= versusFirstWinBonu.begin_at && dateTime <= versusFirstWinBonu.end_at)
        {
          firstWinBonusParam = versusFirstWinBonu;
          break;
        }
      }
      return firstWinBonusParam;
    }

    public int GetVSStreakSchedule(STREAK_JUDGE judge, DateTime time)
    {
      int num = -1;
      for (int index = 0; index < this.mVersusStreakSchedule.Count; ++index)
      {
        VersusStreakWinScheduleParam winScheduleParam = this.mVersusStreakSchedule[index];
        if (winScheduleParam.judge == judge && time >= winScheduleParam.begin_at && time <= winScheduleParam.end_at)
        {
          num = winScheduleParam.id;
          break;
        }
      }
      return num;
    }

    public VersusStreakWinBonusParam GetVSStreakWinBonus(int streakcnt, STREAK_JUDGE judge, long servertime = -1)
    {
      VersusStreakWinBonusParam streakWinBonusParam = (VersusStreakWinBonusParam) null;
      DateTime time = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      int vsStreakSchedule = this.GetVSStreakSchedule(judge, time);
      if (vsStreakSchedule != -1)
      {
        for (int index = 0; index < this.mVersusStreakWinBonus.Count; ++index)
        {
          VersusStreakWinBonusParam versusStreakWinBonu = this.mVersusStreakWinBonus[index];
          if (versusStreakWinBonu.id == vsStreakSchedule && versusStreakWinBonu.wincnt == streakcnt)
          {
            streakWinBonusParam = versusStreakWinBonu;
            break;
          }
        }
      }
      return streakWinBonusParam;
    }

    public VS_MODE GetVSMode(long servertime = -1)
    {
      VS_MODE vsMode = VS_MODE.THREE_ON_THREE;
      DateTime dateTime = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      for (int index = 0; index < this.mVersusRule.Count; ++index)
      {
        VersusRuleParam versusRuleParam = this.mVersusRule[index];
        if (dateTime >= versusRuleParam.begin_at && dateTime <= versusRuleParam.end_at)
        {
          vsMode = versusRuleParam.mode;
          break;
        }
      }
      return vsMode;
    }

    public int GetVSGetCoinRate(long servertime = -1)
    {
      int num = 1;
      DateTime dateTime = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      for (int index = 0; index < this.mVersusCoinCamp.Count; ++index)
      {
        VersusCoinCampParam versusCoinCampParam = this.mVersusCoinCamp[index];
        if (dateTime >= versusCoinCampParam.begin_at && dateTime <= versusCoinCampParam.end_at)
        {
          num = versusCoinCampParam.coinrate;
          break;
        }
      }
      return num;
    }

    public STREAK_JUDGE SearchVersusJudgeType(int id, long servertime = -1)
    {
      STREAK_JUDGE streakJudge = STREAK_JUDGE.None;
      DateTime dateTime = servertime != -1L ? TimeManager.FromUnixTime(servertime) : TimeManager.ServerTime;
      for (int index = 0; index < this.mVersusStreakSchedule.Count; ++index)
      {
        VersusStreakWinScheduleParam winScheduleParam = this.mVersusStreakSchedule[index];
        if (winScheduleParam.id == id && dateTime >= winScheduleParam.begin_at && dateTime <= winScheduleParam.end_at)
        {
          streakJudge = winScheduleParam.judge;
          break;
        }
      }
      return streakJudge;
    }

    public bool Deserialize(Json_VersusCpu json)
    {
      this.mVersusCpuData.Clear();
      if (json.comenemies != null)
      {
        for (int index = json.comenemies.Length - 1; index >= 0; --index)
        {
          SRPG.VersusCpuData versusCpuData = new SRPG.VersusCpuData();
          try
          {
            versusCpuData.Deserialize(json.comenemies[index], json.comenemies.Length - index);
            this.mVersusCpuData.Add(versusCpuData);
          }
          catch (Exception ex)
          {
            DebugUtility.LogException(ex);
          }
        }
      }
      return true;
    }

    public int GetStoryPartNum()
    {
      int num = 1;
      if (this.mWorlds.Count == 0)
        return num;
      using (List<SectionParam>.Enumerator enumerator = this.mWorlds.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SectionParam current = enumerator.Current;
          if (num < current.storyPart)
            num = current.storyPart;
        }
      }
      return num;
    }

    public int GetReleaseStoryPart(string quest_id)
    {
      if (this.mWorlds.Count == 0)
        return 0;
      using (List<SectionParam>.Enumerator enumerator = this.mWorlds.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SectionParam current = enumerator.Current;
          if (quest_id == current.releaseKeyQuest)
            return current.storyPart;
        }
      }
      return 0;
    }

    public int GetStoryPartNumPresentTime()
    {
      List<SectionParam> all = this.mWorlds.FindAll((Predicate<SectionParam>) (p => p.releaseKeyQuest != null));
      if (all == null || all.Count == 0)
        return 1;
      int num = 1;
      for (int index1 = 0; index1 < this.Quests.Length; ++index1)
      {
        if (this.Quests[index1].IsStory && this.Quests[index1].state == QuestStates.Cleared)
        {
          for (int index2 = 0; index2 < all.Count; ++index2)
          {
            if (all[index2].releaseKeyQuest == this.Quests[index1].iname && num < all[index2].storyPart)
              num = all[index2].storyPart;
          }
        }
      }
      return num;
    }

    public string GetReleaseStoryPartWorldName(int story_part)
    {
      SectionParam section_param = this.mWorlds.Find((Predicate<SectionParam>) (p => p.storyPart == story_part));
      if (section_param == null)
        return (string) null;
      QuestParam check_param = new List<QuestParam>((IEnumerable<QuestParam>) this.Quests).Find((Predicate<QuestParam>) (p => p.iname == section_param.releaseKeyQuest));
      if (check_param == null)
        return (string) null;
      section_param = this.mWorlds.Find((Predicate<SectionParam>) (p => p.iname == check_param.world));
      if (section_param == null)
        return (string) null;
      return section_param.name;
    }

    public bool CheckReleaseStoryPart()
    {
      QuestParam[] quests = this.Quests;
      QuestParam questParam = (QuestParam) null;
      for (int index = 0; index < quests.Length; ++index)
      {
        if (quests[index].IsStory)
        {
          if (quests[index].state == QuestStates.Cleared)
            questParam = quests[index];
          else
            break;
        }
      }
      if (questParam == null)
        return false;
      int releaseStoryPart = this.GetReleaseStoryPart(questParam.iname);
      return releaseStoryPart != 0 && !PlayerPrefsUtility.HasKey(PlayerPrefsUtility.RELEASE_STORY_PART_KEY + (object) releaseStoryPart);
    }

    [System.Flags]
    public enum BadgeTypes
    {
      Unit = 1,
      UnitUnlock = 2,
      GoldGacha = 4,
      RareGacha = 8,
      DailyMission = 16, // 0x00000010
      Arena = 32, // 0x00000020
      Multiplay = 64, // 0x00000040
      Friend = 128, // 0x00000080
      GiftBox = 256, // 0x00000100
      ItemEquipment = 512, // 0x00000200
      All = -1,
    }

    private class TextureRequest
    {
      public RawImage Target;
      public string Path;
      public LoadRequest Request;
    }

    private class VersusRange
    {
      public int min;
      public int max;

      public VersusRange(int _min, int _max)
      {
        this.min = _min;
        this.max = _max;
      }
    }

    public delegate void DayChangeEvent();

    public delegate void StaminaChangeEvent();

    public delegate void RankUpCountChangeEvent(int count);

    public delegate void PlayerLvChangeEvent();

    public delegate void PlayerCurrencyChangeEvent();

    public delegate void BuyCoinEvent();

    public delegate bool SceneChangeEvent();
  }
}
