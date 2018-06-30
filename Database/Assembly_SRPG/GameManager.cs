namespace SRPG
{
    using GR;
    using gu3;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [AddComponentMenu("Scripts/SRPG/Manager/Game")]
    public class GameManager : MonoSingleton<GameManager>
    {
        private const string MasterParamPath = "Data/MasterParam";
        private const string QuestParamPath = "Data/QuestParam";
        private const float DesiredFrameTime = 0.026f;
        private const float MaxFrameTime = 0.03f;
        private const float MinAnimationFrameTime = 0.001f;
        private const float MaxAnimationFrameTime = 0.006f;
        private bool mRelogin;
        private PlayerData mPlayer;
        private OptionData mOption;
        private SRPG.MasterParam mMasterParam;
        private List<SectionParam> mWorlds;
        private List<ChapterParam> mAreas;
        private List<QuestParam> mQuests;
        private Dictionary<string, QuestParam> mQuestsDict;
        private List<ObjectiveParam> mObjectives;
        private List<ObjectiveParam> mTowerObjectives;
        private List<MagnificationParam> mMagnifications;
        private List<QuestCondParam> mConditions;
        private List<QuestPartyParam> mParties;
        private List<QuestCampaignParentParam> mCampaignParents;
        private List<QuestCampaignChildParam> mCampaignChildren;
        private List<QuestCampaignTrust> mCampaignTrust;
        private List<QuestParam> mTowerBaseQuests;
        private List<TowerFloorParam> mTowerFloors;
        private Dictionary<string, TowerFloorParam> mTowerFloorsDict;
        private List<TowerRewardParam> mTowerRewards;
        private List<TowerRoundRewardParam> mTowerRoundRewards;
        private List<TowerParam> mTowers;
        private SRPG.TowerResuponse mTowerResuponse;
        private List<ArenaPlayer> mArenaPlayers;
        private List<ArenaPlayer>[] mArenaRanking;
        private List<ArenaPlayerHistory> mArenaHistory;
        private List<GachaParam> mGachas;
        private List<ChatChannelMasterParam> mChatChannelMasters;
        private List<UnitParam> mBadgeUnitUnlocks;
        private List<AchievementParam> mAchievement;
        private GameObject mNotifyList;
        private List<MultiRanking> mMultiUnitRank;
        private Dictionary<string, RankingData> mUnitRanking;
        private List<VersusTowerParam> mVersusTowerFloor;
        private VsTowerMatchEndParam mVersusEndParam;
        private List<VersusScheduleParam> mVersusScheduleParam;
        private List<VersusCoinParam> mVersusCoinParam;
        private List<VersusFriendScore> mVersusFriendScore;
        private List<SRPG.MapEffectParam> mMapEffectParam;
        private List<WeatherSetParam> mWeatherSetParam;
        private List<RankingQuestParam> mRankingQuestParam;
        private List<RankingQuestRewardParam> mRankingQuestRewardParam;
        private List<RankingQuestScheduleParam> mRankingQuestScheduleParam;
        private List<RankingQuestParam> mAvailableRankingQuesstParams;
        private VersusAudienceManager mAudienceManager;
        private List<MultiTowerFloorParam> mMultiTowerFloor;
        private List<MultiTowerRewardParam> mMultiTowerRewards;
        private MultiTowerRoundParam mMultiTowerRound;
        private List<VersusFirstWinBonusParam> mVersusFirstWinBonus;
        private List<VersusStreakWinScheduleParam> mVersusStreakSchedule;
        private List<VersusStreakWinBonusParam> mVersusStreakWinBonus;
        private List<VersusRuleParam> mVersusRule;
        private List<VersusCoinCampParam> mVersusCoinCamp;
        private int mVersusNowStreakWinCnt;
        private List<VersusEnableTimeParam> mVersusEnableTime;
        private List<VersusRankParam> mVersusRank;
        private List<VersusRankClassParam> mVersusRankClass;
        private List<VersusRankRankingRewardParam> mVersusRankRankingReward;
        private List<VersusRankRewardParam> mVersusRankReward;
        private List<VersusRankMissionScheduleParam> mVersusRankMissionSchedule;
        private List<VersusRankMissionParam> mVersusRankMission;
        private List<GuerrillaShopAdventQuestParam> mGuerrillaShopAdventQuest;
        private List<GuerrillaShopScheduleParam> mGuerrillaShopScheduleParam;
        private List<VersusDraftUnitParam> mVersusDraftUnit;
        private List<string> mTips;
        private int mVersusBestStreakWinCnt;
        private int mVersusAllPriodStreakWinCount;
        private int mVersusBestAllPriodStreakWinCount;
        private bool mVersusFirstWinRewardRecived;
        private long mVersusFreeExpiredTime;
        private long mVersusFreeNextTime;
        private VersusDraftType mVersusDraftType;
        private bool mSelectedVersusCpuBattle;
        private List<SRPG.VersusCpuData> mVersusCpuData;
        public ReqFgGAuth.eAuthStatus AuthStatus;
        public string FgGAuthHost;
        private string mReloadMasterDataError;
        private MyGUID mMyGuid;
        public BadgeTypes IsBusyUpdateBadges;
        public BadgeTypes BadgeFlags;
        private DateTime mLastUpdateTime;
        private int mLastStamina;
        private long mLastGold;
        private int mLastAbilityRankUpCount;
        public DayChangeEvent OnDayChange;
        public StaminaChangeEvent OnStaminaChange;
        public RankUpCountChangeEvent OnAbilityRankUpCountChange;
        public RankUpCountChangeEvent OnAbilityRankUpCountPreReset;
        public PlayerLvChangeEvent OnPlayerLvChange;
        public PlayerCurrencyChangeEvent OnPlayerCurrencyChange;
        public bool EnableAnimationFrameSkipping;
        private bool mHasLoggedIn;
        private static bool mUpscaleMode;
        private GameObject mBuyCoinWindow;
        private BuyCoinEvent mOnBuyCoinEnd;
        private BuyCoinEvent mOnBuyCoinCancel;
        private List<Coroutine> mImportantJobs;
        private Coroutine mImportantJobCoroutine;
        public SceneChangeEvent OnSceneChange;
        private List<TextureRequest> mTextureRequests;
        private int mTutorialStep;
        private List<AssetList.Item> mTutorialDLAssets;
        private bool mScannedTutorialAssets;
        private Coroutine mWaitDownloadThread;
        private static readonly int SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY;
        private string mSavedUpdateTrophyListString;
        private List<TrophyState> mUpdateTrophyList;
        private List<TrophyState> mUpdateChallengeList;
        private List<TrophyState> mUpdateTrophyAward;
        public UpdateTrophyLock update_trophy_lock;
        public UpdateTrophyInterval update_trophy_interval;
        private bool is_start_server_sync_trophy_exec;
        private List<TrophyState> mServerSyncTrophyList;
        private List<TrophyState> mServerSyncChallengeList;
        private List<TrophyState> mServerSyncTrophyAward;
        private List<UnitData> mCharacterQuestUnits;
        private long mLimitedShopEndAt;
        private JSON_ShopListArray.Shops[] mLimitedShopList;
        private bool mIsLimitedShopOpen;
        private VersusRange[] mFreeMatchRange;
        [CompilerGenerated]
        private bool <VersusTowerMatchBegin>k__BackingField;
        [CompilerGenerated]
        private bool <VersusTowerMatchReceipt>k__BackingField;
        [CompilerGenerated]
        private string <VersusTowerMatchName>k__BackingField;
        [CompilerGenerated]
        private long <VersusTowerMatchEndAt>k__BackingField;
        [CompilerGenerated]
        private int <VersusCoinRemainCnt>k__BackingField;
        [CompilerGenerated]
        private string <VersusLastUid>k__BackingField;
        [CompilerGenerated]
        private bool <RankMatchBegin>k__BackingField;
        [CompilerGenerated]
        private int <RankMatchScheduleId>k__BackingField;
        [CompilerGenerated]
        private ReqRankMatchStatus.RankingStatus <RankMatchRankingStatus>k__BackingField;
        [CompilerGenerated]
        private long <RankMatchExpiredTime>k__BackingField;
        [CompilerGenerated]
        private long <RankMatchNextTime>k__BackingField;
        [CompilerGenerated]
        private string[] <RankMatchMatchedEnemies>k__BackingField;
        [CompilerGenerated]
        private string <DigestHash>k__BackingField;
        [CompilerGenerated]
        private string <PrevCheckHash>k__BackingField;
        [CompilerGenerated]
        private string <AlterCheckHash>k__BackingField;
        [CompilerGenerated]
        private bool <AudienceMode>k__BackingField;
        [CompilerGenerated]
        private MyPhoton.MyRoom <AudienceRoom>k__BackingField;
        [CompilerGenerated]
        private long <mVersusEnableId>k__BackingField;
        [CompilerGenerated]
        private long <VSDraftId>k__BackingField;
        [CompilerGenerated]
        private string <VSDraftQuestId>k__BackingField;
        [CompilerGenerated]
        private static DayChangeEvent <>f__am$cache8C;
        [CompilerGenerated]
        private static StaminaChangeEvent <>f__am$cache8D;
        [CompilerGenerated]
        private static RankUpCountChangeEvent <>f__am$cache8E;
        [CompilerGenerated]
        private static RankUpCountChangeEvent <>f__am$cache8F;
        [CompilerGenerated]
        private static PlayerLvChangeEvent <>f__am$cache90;
        [CompilerGenerated]
        private static PlayerCurrencyChangeEvent <>f__am$cache91;
        [CompilerGenerated]
        private static SceneChangeEvent <>f__am$cache92;
        [CompilerGenerated]
        private static Comparison<VersusRankClassParam> <>f__am$cache93;
        [CompilerGenerated]
        private static Comparison<VersusRankRankingRewardParam> <>f__am$cache94;
        [CompilerGenerated]
        private static Comparison<VersusRankMissionParam> <>f__am$cache95;
        [CompilerGenerated]
        private static Predicate<TowerFloorParam> <>f__am$cache96;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache97;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache98;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache99;
        [CompilerGenerated]
        private static Comparison<AssetList.Item> <>f__am$cache9A;
        [CompilerGenerated]
        private static Predicate<VersusScheduleParam> <>f__am$cache9B;
        [CompilerGenerated]
        private static Predicate<SectionParam> <>f__am$cache9C;

        static GameManager()
        {
            SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY = 0xa0fd;
            return;
        }

        public GameManager()
        {
            List<ArenaPlayer>[] listArray1;
            this.mPlayer = new PlayerData();
            this.mOption = new OptionData();
            this.mMasterParam = new SRPG.MasterParam();
            this.mWorlds = new List<SectionParam>();
            this.mAreas = new List<ChapterParam>();
            this.mQuests = new List<QuestParam>();
            this.mQuestsDict = new Dictionary<string, QuestParam>();
            this.mObjectives = new List<ObjectiveParam>();
            this.mTowerObjectives = new List<ObjectiveParam>();
            this.mMagnifications = new List<MagnificationParam>();
            this.mConditions = new List<QuestCondParam>();
            this.mParties = new List<QuestPartyParam>();
            this.mCampaignParents = new List<QuestCampaignParentParam>();
            this.mCampaignChildren = new List<QuestCampaignChildParam>();
            this.mCampaignTrust = new List<QuestCampaignTrust>();
            this.mTowerBaseQuests = new List<QuestParam>();
            this.mTowerFloors = new List<TowerFloorParam>();
            this.mTowerFloorsDict = new Dictionary<string, TowerFloorParam>();
            this.mTowerRewards = new List<TowerRewardParam>();
            this.mTowerRoundRewards = new List<TowerRoundRewardParam>();
            this.mTowers = new List<TowerParam>();
            this.mTowerResuponse = new SRPG.TowerResuponse();
            this.mArenaPlayers = new List<ArenaPlayer>();
            listArray1 = new List<ArenaPlayer>[] { new List<ArenaPlayer>(), new List<ArenaPlayer>() };
            this.mArenaRanking = listArray1;
            this.mArenaHistory = new List<ArenaPlayerHistory>();
            this.mGachas = new List<GachaParam>();
            this.mChatChannelMasters = new List<ChatChannelMasterParam>();
            this.mAchievement = new List<AchievementParam>();
            this.mMultiUnitRank = new List<MultiRanking>();
            this.mUnitRanking = new Dictionary<string, RankingData>();
            this.mVersusTowerFloor = new List<VersusTowerParam>();
            this.mVersusEndParam = new VsTowerMatchEndParam();
            this.mVersusScheduleParam = new List<VersusScheduleParam>();
            this.mVersusCoinParam = new List<VersusCoinParam>();
            this.mVersusFriendScore = new List<VersusFriendScore>();
            this.mRankingQuestParam = new List<RankingQuestParam>();
            this.mRankingQuestRewardParam = new List<RankingQuestRewardParam>();
            this.mRankingQuestScheduleParam = new List<RankingQuestScheduleParam>();
            this.mAvailableRankingQuesstParams = new List<RankingQuestParam>();
            this.mAudienceManager = new VersusAudienceManager();
            this.mMultiTowerFloor = new List<MultiTowerFloorParam>();
            this.mMultiTowerRewards = new List<MultiTowerRewardParam>();
            this.mMultiTowerRound = new MultiTowerRoundParam();
            this.mVersusFirstWinBonus = new List<VersusFirstWinBonusParam>();
            this.mVersusStreakSchedule = new List<VersusStreakWinScheduleParam>();
            this.mVersusStreakWinBonus = new List<VersusStreakWinBonusParam>();
            this.mVersusRule = new List<VersusRuleParam>();
            this.mVersusCoinCamp = new List<VersusCoinCampParam>();
            this.mVersusEnableTime = new List<VersusEnableTimeParam>();
            this.mVersusRank = new List<VersusRankParam>();
            this.mVersusRankClass = new List<VersusRankClassParam>();
            this.mVersusRankRankingReward = new List<VersusRankRankingRewardParam>();
            this.mVersusRankReward = new List<VersusRankRewardParam>();
            this.mVersusRankMissionSchedule = new List<VersusRankMissionScheduleParam>();
            this.mVersusRankMission = new List<VersusRankMissionParam>();
            this.mGuerrillaShopAdventQuest = new List<GuerrillaShopAdventQuestParam>();
            this.mGuerrillaShopScheduleParam = new List<GuerrillaShopScheduleParam>();
            this.mVersusDraftUnit = new List<VersusDraftUnitParam>();
            this.mTips = new List<string>();
            this.mVersusCpuData = new List<SRPG.VersusCpuData>();
            if (<>f__am$cache8C != null)
            {
                goto Label_02BE;
            }
            <>f__am$cache8C = new DayChangeEvent(GameManager.<OnDayChange>m__1DB);
        Label_02BE:
            this.OnDayChange = <>f__am$cache8C;
            if (<>f__am$cache8D != null)
            {
                goto Label_02E1;
            }
            <>f__am$cache8D = new StaminaChangeEvent(GameManager.<OnStaminaChange>m__1DC);
        Label_02E1:
            this.OnStaminaChange = <>f__am$cache8D;
            if (<>f__am$cache8E != null)
            {
                goto Label_0304;
            }
            <>f__am$cache8E = new RankUpCountChangeEvent(GameManager.<OnAbilityRankUpCountChange>m__1DD);
        Label_0304:
            this.OnAbilityRankUpCountChange = <>f__am$cache8E;
            if (<>f__am$cache8F != null)
            {
                goto Label_0327;
            }
            <>f__am$cache8F = new RankUpCountChangeEvent(GameManager.<OnAbilityRankUpCountPreReset>m__1DE);
        Label_0327:
            this.OnAbilityRankUpCountPreReset = <>f__am$cache8F;
            if (<>f__am$cache90 != null)
            {
                goto Label_034A;
            }
            <>f__am$cache90 = new PlayerLvChangeEvent(GameManager.<OnPlayerLvChange>m__1DF);
        Label_034A:
            this.OnPlayerLvChange = <>f__am$cache90;
            if (<>f__am$cache91 != null)
            {
                goto Label_036D;
            }
            <>f__am$cache91 = new PlayerCurrencyChangeEvent(GameManager.<OnPlayerCurrencyChange>m__1E0);
        Label_036D:
            this.OnPlayerCurrencyChange = <>f__am$cache91;
            this.mImportantJobs = new List<Coroutine>();
            if (<>f__am$cache92 != null)
            {
                goto Label_039B;
            }
            <>f__am$cache92 = new SceneChangeEvent(GameManager.<OnSceneChange>m__1E1);
        Label_039B:
            this.OnSceneChange = <>f__am$cache92;
            this.mTextureRequests = new List<TextureRequest>();
            this.mTutorialDLAssets = new List<AssetList.Item>(0x400);
            this.update_trophy_lock = new UpdateTrophyLock();
            this.update_trophy_interval = new UpdateTrophyInterval();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <ConfirmBuyCoin>m__1FD(GameObject go)
        {
            this.StartBuyCoinSequence();
            return;
        }

        [CompilerGenerated]
        private static void <ContinueBuyStamina>m__1FA(GameObject go)
        {
        }

        [CompilerGenerated]
        private static bool <ExistsOpenVersusTower>m__203(VersusScheduleParam data)
        {
            return data.IsOpen;
        }

        [CompilerGenerated]
        private static bool <FindFirstTowerFloor>m__1F3(TowerFloorParam floorParam)
        {
            return string.IsNullOrEmpty(floorParam.cond_quest);
        }

        [CompilerGenerated]
        private static bool <GetStoryPartNumPresentTime>m__20B(SectionParam p)
        {
            return ((p.releaseKeyQuest == null) == 0);
        }

        [CompilerGenerated]
        private static int <GetVersusRankClassList>m__1E9(VersusRankClassParam a, VersusRankClassParam b)
        {
            return (a.Class - b.Class);
        }

        [CompilerGenerated]
        private static int <GetVersusRankMissionList>m__1EF(VersusRankMissionParam a, VersusRankMissionParam b)
        {
            return a.IName.CompareTo(b.IName);
        }

        [CompilerGenerated]
        private static int <GetVersusRankRankingRewardList>m__1EB(VersusRankRankingRewardParam a, VersusRankRankingRewardParam b)
        {
            return (a.RankBegin - b.RankBegin);
        }

        [CompilerGenerated]
        private static void <NotRequiredHeal>m__1FB(GameObject go)
        {
        }

        [CompilerGenerated]
        private static void <OnAbilityRankUpCountChange>m__1DD(int n)
        {
        }

        [CompilerGenerated]
        private static void <OnAbilityRankUpCountPreReset>m__1DE(int n)
        {
        }

        [CompilerGenerated]
        private static void <OnBuyStamina>m__1FC(GameObject go)
        {
        }

        [CompilerGenerated]
        private static void <OnDayChange>m__1DB()
        {
        }

        [CompilerGenerated]
        private static void <OnPlayerCurrencyChange>m__1E0()
        {
        }

        [CompilerGenerated]
        private static void <OnPlayerLvChange>m__1DF()
        {
        }

        [CompilerGenerated]
        private static bool <OnSceneChange>m__1E1()
        {
            return 1;
        }

        [CompilerGenerated]
        private static void <OnStaminaChange>m__1DC()
        {
        }

        [CompilerGenerated]
        private static int <RefreshTutorialDLAssets>m__1FE(AssetList.Item x, AssetList.Item y)
        {
            return ((x.Size != y.Size) ? ((x.Size <= y.Size) ? -1 : 1) : 0);
        }

        [CompilerGenerated]
        private void <StartBuyStaminaSequence>m__1F9(GameObject go)
        {
            this.ContinueBuyStamina();
            return;
        }

        public void AddCharacterQuestPopup(UnitData unit)
        {
            <AddCharacterQuestPopup>c__AnonStorey298 storey;
            storey = new <AddCharacterQuestPopup>c__AnonStorey298();
            storey.unit = unit;
            if (storey.unit != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            if (this.mCharacterQuestUnits != null)
            {
                goto Label_0034;
            }
            this.mCharacterQuestUnits = new List<UnitData>();
            goto Label_0051;
        Label_0034:
            if (this.mCharacterQuestUnits.Exists(new Predicate<UnitData>(storey.<>m__200)) == null)
            {
                goto Label_0051;
            }
            return;
        Label_0051:
            this.mCharacterQuestUnits.Add(storey.unit);
            return;
        }

        public void AddMTQuest(string iname, QuestParam param)
        {
            if (this.mQuests == null)
            {
                goto Label_0040;
            }
            if (this.mQuestsDict == null)
            {
                goto Label_0040;
            }
            if (this.mQuestsDict.ContainsKey(iname) != null)
            {
                goto Label_0040;
            }
            this.mQuests.Add(param);
            this.mQuestsDict.Add(iname, param);
        Label_0040:
            return;
        }

        public void ApplyTextureAsync(RawImage target, string path)
        {
            int num;
            TextureRequest request;
            if ((target == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            num = 0;
            goto Label_00A8;
        Label_0014:
            if ((this.mTextureRequests[num].Target == target) == null)
            {
                goto Label_00A4;
            }
            if ((this.mTextureRequests[num].Path != path) == null)
            {
                goto Label_00A3;
            }
            if (string.IsNullOrEmpty(path) == null)
            {
                goto Label_007F;
            }
            this.mTextureRequests[num].Target.set_texture(null);
            this.mTextureRequests.RemoveAt(num);
            goto Label_00A3;
        Label_007F:
            this.mTextureRequests[num].Request = null;
            this.mTextureRequests[num].Path = path;
        Label_00A3:
            return;
        Label_00A4:
            num += 1;
        Label_00A8:
            if (num < this.mTextureRequests.Count)
            {
                goto Label_0014;
            }
            if (string.IsNullOrEmpty(path) == null)
            {
                goto Label_00CC;
            }
            target.set_texture(null);
            return;
        Label_00CC:
            target.set_texture(null);
            request = new TextureRequest();
            request.Target = target;
            request.Path = path;
            this.mTextureRequests.Add(request);
            if (AssetManager.IsLoading != null)
            {
                goto Label_0105;
            }
            this.RequestTexture(request);
        Label_0105:
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncWaitForImportantJobs()
        {
            <AsyncWaitForImportantJobs>c__IteratorD5 rd;
            rd = new <AsyncWaitForImportantJobs>c__IteratorD5();
            rd.<>f__this = this;
            return rd;
        }

        public unsafe TOWER_RANK CalcTowerRank(bool isNow)
        {
            int num;
            TOWER_RANK tower_rank;
            OInt[] numArray;
            int num2;
            num = this.CalcTowerScore(isNow);
            tower_rank = 15;
            numArray = this.mMasterParam.TowerRank;
            num2 = 0;
            goto Label_0040;
        Label_001E:
            if (num > *(&(numArray[num2])))
            {
                goto Label_003C;
            }
            tower_rank = num2;
            goto Label_0049;
        Label_003C:
            num2 += 1;
        Label_0040:
            if (num2 < ((int) numArray.Length))
            {
                goto Label_001E;
            }
        Label_0049:
            return tower_rank;
        }

        public int CalcTowerScore(bool isNow)
        {
            SRPG.TowerResuponse resuponse;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            TowerParam param;
            TowerScoreParam[] paramArray;
            int num7;
            int num8;
            resuponse = this.TowerResuponse;
            num = 0;
            num2 = (isNow == null) ? resuponse.spd_score : resuponse.turn_num;
            num3 = (isNow == null) ? resuponse.tec_score : resuponse.died_num;
            num4 = (isNow == null) ? resuponse.ret_score : resuponse.retire_num;
            num5 = (isNow == null) ? resuponse.rcv_score : resuponse.recover_num;
            num6 = 0;
            param = this.FindTower(resuponse.TowerID);
            paramArray = this.mMasterParam.FindTowerScoreParam(param.score_iname);
            num7 = 0;
            goto Label_0178;
        Label_0098:
            if (num2 > paramArray[num7].TurnCnt)
            {
                goto Label_00CE;
            }
            if ((num6 & 1) != null)
            {
                goto Label_00CE;
            }
            num += paramArray[num7].Score;
            num6 |= 1;
        Label_00CE:
            if (num3 > paramArray[num7].DiedCnt)
            {
                goto Label_0104;
            }
            if ((num6 & 2) != null)
            {
                goto Label_0104;
            }
            num += paramArray[num7].Score;
            num6 |= 2;
        Label_0104:
            if (num4 > paramArray[num7].RetireCnt)
            {
                goto Label_013B;
            }
            if ((num6 & 4) != null)
            {
                goto Label_013B;
            }
            num += paramArray[num7].Score;
            num6 |= 4;
        Label_013B:
            if (num5 > paramArray[num7].RecoverCnt)
            {
                goto Label_0172;
            }
            if ((num6 & 8) != null)
            {
                goto Label_0172;
            }
            num += paramArray[num7].Score;
            num6 |= 8;
        Label_0172:
            num7 += 1;
        Label_0178:
            if (num7 < ((int) paramArray.Length))
            {
                goto Label_0098;
            }
            num8 = 0;
            goto Label_01B6;
        Label_018B:
            if ((num6 & (1 << (num8 & 0x1f))) != null)
            {
                goto Label_01B0;
            }
            num += paramArray[((int) paramArray.Length) - 1].Score;
        Label_01B0:
            num8 += 1;
        Label_01B6:
            if (num8 < 4)
            {
                goto Label_018B;
            }
            return (num / 4);
        }

        public void CancelTextureLoadRequest(RawImage target)
        {
            int num;
            num = 0;
            goto Label_0034;
        Label_0007:
            if ((this.mTextureRequests[num].Target == target) == null)
            {
                goto Label_0030;
            }
            this.mTextureRequests.RemoveAt(num);
            return;
        Label_0030:
            num += 1;
        Label_0034:
            if (num < this.mTextureRequests.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public bool CheckBadges(BadgeTypes type)
        {
            return (((this.BadgeFlags & type) == 0) == 0);
        }

        public bool CheckBusyBadges(BadgeTypes type)
        {
            return (((this.IsBusyUpdateBadges & type) == 0) == 0);
        }

        public bool CheckEnableUnitUnlock(UnitParam unit)
        {
            if (this.mBadgeUnitUnlocks == null)
            {
                goto Label_001E;
            }
            if (this.mBadgeUnitUnlocks.Contains(unit) == null)
            {
                goto Label_001E;
            }
            return 1;
        Label_001E:
            return 0;
        }

        public bool CheckReleaseStoryPart()
        {
            QuestParam[] paramArray;
            QuestParam param;
            int num;
            int num2;
            string str;
            paramArray = this.Quests;
            param = null;
            num = 0;
            goto Label_0038;
        Label_0010:
            if (paramArray[num].IsStory == null)
            {
                goto Label_0034;
            }
            if (paramArray[num].state == 2)
            {
                goto Label_0030;
            }
            goto Label_0041;
        Label_0030:
            param = paramArray[num];
        Label_0034:
            num += 1;
        Label_0038:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0010;
            }
        Label_0041:
            if (param != null)
            {
                goto Label_0049;
            }
            return 0;
        Label_0049:
            num2 = this.GetReleaseStoryPart(param.iname);
            if (num2 != null)
            {
                goto Label_005E;
            }
            return 0;
        Label_005E:
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.RELEASE_STORY_PART_KEY + ((int) num2)) != null)
            {
                goto Label_007E;
            }
            return 1;
        Label_007E:
            return 0;
        }

        public bool CoinShortage()
        {
            if (this.Player.Coin >= this.Player.GetStaminaRecoveryCost(0))
            {
                goto Label_0026;
            }
            this.ConfirmBuyCoin(null, null);
            return 0;
        Label_0026:
            return 1;
        }

        public void CompleteTutorialStep()
        {
            this.mTutorialStep += 1;
            return;
        }

        public void ConfirmBuyCoin(BuyCoinEvent onEnd, BuyCoinEvent onCancel)
        {
            this.mOnBuyCoinEnd = onEnd;
            this.mOnBuyCoinCancel = onCancel;
            this.mBuyCoinWindow = UIUtility.ConfirmBox(LocalizedText.Get("sys.OUT_OF_COIN_CONFIRM_BUY_COIN"), null, new UIUtility.DialogResultEvent(this.<ConfirmBuyCoin>m__1FD), new UIUtility.DialogResultEvent(this.OnBuyCoinConfirmCancel), null, 0, -1);
            return;
        }

        private void ContinueBuyStamina()
        {
            int num;
            int num2;
            num = this.Player.StaminaBuyNum;
            num2 = this.MasterParam.GetVipBuyStaminaLimit(this.Player.VipRank);
            if (num < num2)
            {
                goto Label_005C;
            }
            if (<>f__am$cache97 != null)
            {
                goto Label_004D;
            }
            <>f__am$cache97 = new UIUtility.DialogResultEvent(GameManager.<ContinueBuyStamina>m__1FA);
        Label_004D:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.STAMINA_BUY_LIMIT"), <>f__am$cache97, null, 0, -1);
            return;
        Label_005C:
            if (this.NotRequiredHeal() != null)
            {
                goto Label_0068;
            }
            return;
        Label_0068:
            if (this.CoinShortage() != null)
            {
                goto Label_0074;
            }
            return;
        Label_0074:
            if (Network.Mode != null)
            {
                goto Label_0095;
            }
            Network.RequestAPI(new ReqItemAddStmPaid(new Network.ResponseCallback(this.OnBuyStamina)), 0);
        Label_0095:
            return;
        }

        public string ConvertTowerScoreToRank(TowerParam towerParam, int score, TOWER_SCORE_TYPE type)
        {
            TowerScoreParam[] paramArray;
            string str;
            int num;
            paramArray = this.mMasterParam.FindTowerScoreParam(towerParam.score_iname);
            str = paramArray[((int) paramArray.Length) - 1].Rank;
            num = 0;
            goto Label_00D8;
        Label_0026:
            if (type != null)
            {
                goto Label_0052;
            }
            if (score > paramArray[num].TurnCnt)
            {
                goto Label_00D4;
            }
            str = paramArray[num].Rank;
            goto Label_00E1;
            goto Label_00D4;
        Label_0052:
            if (type != 1)
            {
                goto Label_007F;
            }
            if (score > paramArray[num].DiedCnt)
            {
                goto Label_00D4;
            }
            str = paramArray[num].Rank;
            goto Label_00E1;
            goto Label_00D4;
        Label_007F:
            if (type != 2)
            {
                goto Label_00AC;
            }
            if (score > paramArray[num].RetireCnt)
            {
                goto Label_00D4;
            }
            str = paramArray[num].Rank;
            goto Label_00E1;
            goto Label_00D4;
        Label_00AC:
            if (type != 3)
            {
                goto Label_00D4;
            }
            if (score > paramArray[num].RecoverCnt)
            {
                goto Label_00D4;
            }
            str = paramArray[num].Rank;
            goto Label_00E1;
        Label_00D4:
            num += 1;
        Label_00D8:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0026;
            }
        Label_00E1:
            return str;
        }

        public void CreateMatchingRange()
        {
            VersusRange[] rangeArray1;
            if (this.mFreeMatchRange == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            rangeArray1 = new VersusRange[] { new VersusRange(1, 20), new VersusRange(0x15, 40), new VersusRange(0x29, 60), new VersusRange(0x3d, 0x54), new VersusRange(0x55, 120), new VersusRange(0x79, -1) };
            this.mFreeMatchRange = rangeArray1;
            return;
        }

        public void CreateUpdateTrophyList(out List<TrophyState> updateTrophyList, out List<TrophyState> updateChallengeList, out List<TrophyState> updateTrophyAward)
        {
            TrophyState[] stateArray;
            int num;
            TrophyParam param;
            *(updateTrophyList) = new List<TrophyState>();
            *(updateChallengeList) = new List<TrophyState>();
            *(updateTrophyAward) = new List<TrophyState>();
            stateArray = this.Player.TrophyStates;
            if (stateArray != null)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            num = 0;
            goto Label_00D6;
        Label_002F:
            if (stateArray[num].IsDirty != null)
            {
                goto Label_004E;
            }
            if (stateArray[num].IsSending != null)
            {
                goto Label_004E;
            }
            goto Label_00D2;
        Label_004E:
            if (stateArray[num].IsEnded == null)
            {
                goto Label_0060;
            }
            goto Label_00D2;
        Label_0060:
            param = this.MasterParam.GetTrophy(stateArray[num].iname);
            if (param != null)
            {
                goto Label_007F;
            }
            goto Label_00D2;
        Label_007F:
            if (param.IsChallengeMission == null)
            {
                goto Label_0099;
            }
            *(updateChallengeList).Add(stateArray[num]);
            goto Label_00D2;
        Label_0099:
            if (stateArray[num].Param.DispType != 2)
            {
                goto Label_00C8;
            }
            if (stateArray[num].IsCompleted == null)
            {
                goto Label_00C8;
            }
            *(updateTrophyAward).Add(stateArray[num]);
            goto Label_00D2;
        Label_00C8:
            *(updateTrophyList).Add(stateArray[num]);
        Label_00D2:
            num += 1;
        Label_00D6:
            if (num < ((int) stateArray.Length))
            {
                goto Label_002F;
            }
            return;
        }

        private void DayChanged()
        {
            if (this.Player == null)
            {
                goto Label_0042;
            }
            this.Player.ResetStaminaRecoverCount();
            this.Player.ResetBuyGoldNum();
            this.Player.ResetQuestChallengeResets();
            this.Player.ResetQuestChallenges();
            this.Player.UpdateTrophyStates();
        Label_0042:
            return;
        }

        public string Decrypt(string key, string iv, string src)
        {
            RijndaelManaged managed;
            byte[] buffer;
            byte[] buffer2;
            byte[] buffer3;
            byte[] buffer4;
            ICryptoTransform transform;
            MemoryStream stream;
            CryptoStream stream2;
            int num;
            managed = new RijndaelManaged();
            managed.Padding = 3;
            managed.Mode = 1;
            num = 0x80;
            managed.BlockSize = num;
            managed.KeySize = num;
            buffer = Encoding.UTF8.GetBytes(key);
            buffer2 = Encoding.UTF8.GetBytes(iv);
            buffer3 = Convert.FromBase64String(src);
            buffer4 = new byte[(int) buffer3.Length];
            transform = managed.CreateDecryptor(buffer, buffer2);
            stream = new MemoryStream(buffer3);
            stream2 = new CryptoStream(stream, transform, 0);
            stream2.Read(buffer4, 0, (int) buffer4.Length);
            return Encoding.UTF8.GetString(buffer4);
        }

        public bool Deserialize(Json_AchievementList json)
        {
            int num;
            this.mAchievement.Clear();
            num = 0;
            goto Label_0016;
        Label_0012:
            num += 1;
        Label_0016:
            if (num < ((int) json.achievements.Length))
            {
                goto Label_0012;
            }
            return 1;
        }

        public bool Deserialize(JSON_ArenaHistory json)
        {
            List<ArenaPlayerHistory> list;
            int num;
            ArenaPlayerHistory history;
            Exception exception;
            list = this.mArenaHistory;
            list.Clear();
            if (json.colohistories == null)
            {
                goto Label_005D;
            }
            num = 0;
            goto Label_004F;
        Label_001F:
            history = new ArenaPlayerHistory();
        Label_0025:
            try
            {
                history.Deserialize(json.colohistories[num]);
                list.Add(history);
                goto Label_004B;
            }
            catch (Exception exception1)
            {
            Label_003F:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_004B;
            }
        Label_004B:
            num += 1;
        Label_004F:
            if (num < ((int) json.colohistories.Length))
            {
                goto Label_001F;
            }
        Label_005D:
            return 1;
        }

        public bool Deserialize(Json_ArenaPlayers json)
        {
            int num;
            ArenaPlayer player;
            Exception exception;
            this.mArenaPlayers.Clear();
            if (this.Player.Deserialize(json) != null)
            {
                goto Label_001E;
            }
            return 0;
        Label_001E:
            if (json.coloenemies == null)
            {
                goto Label_0073;
            }
            num = 0;
            goto Label_0065;
        Label_0030:
            player = new ArenaPlayer();
        Label_0036:
            try
            {
                player.Deserialize(json.coloenemies[num]);
                this.mArenaPlayers.Add(player);
                goto Label_0061;
            }
            catch (Exception exception1)
            {
            Label_0055:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0061;
            }
        Label_0061:
            num += 1;
        Label_0065:
            if (num < ((int) json.coloenemies.Length))
            {
                goto Label_0030;
            }
        Label_0073:
            return 1;
        }

        public bool Deserialize(JSON_ChatChannelMaster json)
        {
            int num;
            ChatChannelMasterParam param;
            if (json == null)
            {
                goto Label_0011;
            }
            if (json.channels != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            this.mChatChannelMasters.Clear();
            num = 0;
            goto Label_0049;
        Label_0025:
            param = new ChatChannelMasterParam();
            param.Deserialize(json.channels[num]);
            this.mChatChannelMasters.Add(param);
            num += 1;
        Label_0049:
            if (num < ((int) json.channels.Length))
            {
                goto Label_0025;
            }
            return 1;
        }

        public bool Deserialize(Json_GachaList json)
        {
            int num;
            GachaParam param;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (json.gachas != null)
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            this.mGachas.Clear();
            num = 0;
            goto Label_004B;
        Label_0027:
            param = new GachaParam();
            param.Deserialize(json.gachas[num]);
            this.mGachas.Add(param);
            num += 1;
        Label_004B:
            if (num < ((int) json.gachas.Length))
            {
                goto Label_0027;
            }
            return 1;
        }

        public bool Deserialize(JSON_MasterParam json)
        {
            bool flag;
            flag = 1;
            flag &= this.mMasterParam.Deserialize(json);
            if (flag == null)
            {
                goto Label_0022;
            }
            this.mMasterParam.CacheReferences();
        Label_0022:
            return flag;
        }

        public void Deserialize(Json_PlayerData json)
        {
            int num;
            num = 0;
            if (this.Player == null)
            {
                goto Label_0019;
            }
            num = this.Player.Lv;
        Label_0019:
            this.mPlayer.Deserialize(json);
            if (num == null)
            {
                goto Label_0047;
            }
            if (num == this.Player.Lv)
            {
                goto Label_0047;
            }
            this.OnPlayerLvChange();
        Label_0047:
            return;
        }

        public bool Deserialize(JSON_QuestProgress json)
        {
            QuestParam param;
            OInt num;
            if (json != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            param = this.FindQuest(json.i);
            if (param != null)
            {
                goto Label_001D;
            }
            return 1;
        Label_001D:
            num = json.m;
            param.clear_missions = num;
            param.state = (byte) json.t;
            param.start = json.s;
            param.end = json.e;
            param.key_end = json.n;
            param.key_cnt = json.c;
            if (param.Chapter == null)
            {
                goto Label_0154;
            }
            if (param.Chapter.start > 0L)
            {
                goto Label_00A5;
            }
            param.Chapter.start = json.s;
            goto Label_00C6;
        Label_00A5:
            param.Chapter.start = Math.Min(param.Chapter.start, json.s);
        Label_00C6:
            param.Chapter.end = Math.Max(param.Chapter.end, json.e);
            if (param.Chapter.IsKeyQuest() == null)
            {
                goto Label_0154;
            }
            if (json.e != null)
            {
                goto Label_0128;
            }
            param.Chapter.key_end = Math.Max(param.Chapter.key_end, json.n);
            goto Label_0154;
        Label_0128:
            param.Chapter.key_end = Math.Max(param.Chapter.key_end, Math.Min(json.e, json.n));
        Label_0154:
            if (json.d == null)
            {
                goto Label_018B;
            }
            param.dailyCount = CheckCast.to_short(json.d.num);
            param.dailyReset = CheckCast.to_short(json.d.reset);
        Label_018B:
            return 1;
        }

        public void Deserialize(Json_TrophyPlayerData json)
        {
            int num;
            num = 0;
            if (this.Player == null)
            {
                goto Label_0019;
            }
            num = this.Player.Lv;
        Label_0019:
            this.mPlayer.Deserialize(json);
            if (num == null)
            {
                goto Label_0047;
            }
            if (num == this.Player.Lv)
            {
                goto Label_0047;
            }
            this.OnPlayerLvChange();
        Label_0047:
            return;
        }

        public bool Deserialize(Json_VersusCpu json)
        {
            int num;
            SRPG.VersusCpuData data;
            Exception exception;
            this.mVersusCpuData.Clear();
            if (json.comenemies == null)
            {
                goto Label_006D;
            }
            num = ((int) json.comenemies.Length) - 1;
            goto Label_0066;
        Label_0026:
            data = new SRPG.VersusCpuData();
        Label_002C:
            try
            {
                data.Deserialize(json.comenemies[num], ((int) json.comenemies.Length) - num);
                this.mVersusCpuData.Add(data);
                goto Label_0062;
            }
            catch (Exception exception1)
            {
            Label_0056:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0062;
            }
        Label_0062:
            num -= 1;
        Label_0066:
            if (num >= 0)
            {
                goto Label_0026;
            }
        Label_006D:
            return 1;
        }

        public void Deserialize(FriendPresentReceiveList.Json[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public void Deserialize(FriendPresentWishList.Json[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public void Deserialize(Json_Friend[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public void Deserialize(Json_Item[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public bool Deserialize(Json_Mail[] json)
        {
            return this.mPlayer.Deserialize(json);
        }

        public void Deserialize(Json_MultiFuids[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public void Deserialize(Json_Party[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public bool Deserialize(JSON_QuestProgress[] json)
        {
            int num;
            if (json != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            num = 0;
            goto Label_0023;
        Label_000F:
            if (this.Deserialize(json[num]) != null)
            {
                goto Label_001F;
            }
            return 0;
        Label_001F:
            num += 1;
        Label_0023:
            if (num < ((int) json.Length))
            {
                goto Label_000F;
            }
            return 1;
        }

        public void Deserialize(Json_Skin[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public void Deserialize(Json_Support[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public void Deserialize(Json_Unit[] json)
        {
            this.mPlayer.Deserialize(json);
            return;
        }

        public bool Deserialize(Json_VersusFriendScore[] json)
        {
            int num;
            VersusFriendScore score;
            if (json == null)
            {
                goto Label_0011;
            }
            if (this.mVersusFriendScore != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            this.mVersusFriendScore.Clear();
            num = 0;
            goto Label_0075;
        Label_0025:
            score = new VersusFriendScore();
            score.floor = json[num].floor;
            score.name = json[num].name;
            score.unit = new UnitData();
            score.unit.Deserialize(json[num].unit);
            this.mVersusFriendScore.Add(score);
            num += 1;
        Label_0075:
            if (num < ((int) json.Length))
            {
                goto Label_0025;
            }
            return 1;
        }

        public void Deserialize(Json_GachaResult json)
        {
            this.mPlayer.Deserialize(json.player);
            this.mPlayer.Deserialize(json.items);
            this.mPlayer.Deserialize(json.units);
            this.mPlayer.Deserialize(json.mails);
            if (json.artifacts == null)
            {
                goto Label_0062;
            }
            this.mPlayer.Deserialize(json.artifacts, 1);
        Label_0062:
            return;
        }

        public void Deserialize(Json_GoogleReview json)
        {
            this.mPlayer.Deserialize(json.player);
            this.mPlayer.Deserialize(json.items);
            this.mPlayer.Deserialize(json.units);
            this.mPlayer.Deserialize(json.mails);
            return;
        }

        public void Deserialize(Json_Notify json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mPlayer.Deserialize(json);
            return;
        }

        public unsafe void Deserialize(Json_QuestList json)
        {
            int num;
            SectionParam param;
            int num2;
            ChapterParam param2;
            int num3;
            ChapterParam param3;
            int num4;
            ObjectiveParam param4;
            int num5;
            ObjectiveParam param5;
            int num6;
            MagnificationParam param6;
            int num7;
            QuestCondParam param7;
            int num8;
            QuestPartyParam param8;
            int num9;
            QuestParam param9;
            int num10;
            int num11;
            QuestCampaignParentParam param10;
            int num12;
            QuestCampaignChildParam param11;
            List<QuestCampaignParentParam> list;
            QuestCampaignParentParam param12;
            List<QuestCampaignParentParam>.Enumerator enumerator;
            int num13;
            QuestCampaignChildParam param13;
            int num14;
            TowerRewardParam param14;
            int num15;
            TowerRoundRewardParam param15;
            int num16;
            TowerFloorParam param16;
            int num17;
            TowerParam param17;
            int num18;
            VersusTowerParam param18;
            int num19;
            VersusScheduleParam param19;
            int num20;
            VersusCoinParam param20;
            int num21;
            MultiTowerFloorParam param21;
            int num22;
            MultiTowerRewardParam param22;
            List<SRPG.MapEffectParam> list2;
            int num23;
            SRPG.MapEffectParam param23;
            List<WeatherSetParam> list3;
            int num24;
            WeatherSetParam param24;
            int num25;
            RankingQuestScheduleParam param25;
            int num26;
            RankingQuestRewardParam param26;
            int num27;
            RankingQuestParam param27;
            int num28;
            int num29;
            VersusFirstWinBonusParam param28;
            int num30;
            int num31;
            VersusStreakWinScheduleParam param29;
            int num32;
            int num33;
            VersusStreakWinBonusParam param30;
            int num34;
            int num35;
            VersusRuleParam param31;
            int num36;
            int num37;
            VersusCoinCampParam param32;
            int num38;
            int num39;
            VersusEnableTimeParam param33;
            int num40;
            int num41;
            VersusRankParam param34;
            int num42;
            int num43;
            VersusRankClassParam param35;
            int num44;
            int num45;
            VersusRankRankingRewardParam param36;
            int num46;
            int num47;
            VersusRankRewardParam param37;
            int num48;
            int num49;
            VersusRankMissionScheduleParam param38;
            int num50;
            int num51;
            VersusRankMissionParam param39;
            int num52;
            int num53;
            GuerrillaShopAdventQuestParam param40;
            int num54;
            int num55;
            GuerrillaShopScheduleParam param41;
            int num56;
            int num57;
            VersusDraftUnitParam param42;
            <Deserialize>c__AnonStorey296 storey;
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
            DebugUtility.Verify(json, typeof(Json_QuestList));
            num = 0;
            goto Label_01DD;
        Label_01B9:
            param = new SectionParam();
            param.Deserialize(json.worlds[num]);
            this.mWorlds.Add(param);
            num += 1;
        Label_01DD:
            if (num < ((int) json.worlds.Length))
            {
                goto Label_01B9;
            }
            num2 = 0;
            goto Label_0238;
        Label_01F2:
            param2 = new ChapterParam();
            param2.Deserialize(json.areas[num2]);
            this.mAreas.Add(param2);
            if (string.IsNullOrEmpty(param2.section) != null)
            {
                goto Label_0234;
            }
            param2.sectionParam = this.FindWorld(param2.section);
        Label_0234:
            num2 += 1;
        Label_0238:
            if (num2 < ((int) json.areas.Length))
            {
                goto Label_01F2;
            }
            num3 = 0;
            goto Label_02C3;
        Label_024E:
            if (string.IsNullOrEmpty(json.areas[num3].parent) != null)
            {
                goto Label_02BD;
            }
            param3 = this.FindArea(json.areas[num3].iname);
            if (param3 == null)
            {
                goto Label_02BD;
            }
            param3.parent = this.FindArea(json.areas[num3].parent);
            if (param3.parent == null)
            {
                goto Label_02BD;
            }
            param3.parent.children.Add(param3);
        Label_02BD:
            num3 += 1;
        Label_02C3:
            if (num3 < ((int) json.areas.Length))
            {
                goto Label_024E;
            }
            num4 = 0;
            goto Label_0304;
        Label_02DA:
            param4 = new ObjectiveParam();
            param4.Deserialize(json.objectives[num4]);
            this.mObjectives.Add(param4);
            num4 += 1;
        Label_0304:
            if (num4 < ((int) json.objectives.Length))
            {
                goto Label_02DA;
            }
            if (json.towerObjectives == null)
            {
                goto Label_035F;
            }
            num5 = 0;
            goto Label_0350;
        Label_0326:
            param5 = new ObjectiveParam();
            param5.Deserialize(json.towerObjectives[num5]);
            this.mTowerObjectives.Add(param5);
            num5 += 1;
        Label_0350:
            if (num5 < ((int) json.towerObjectives.Length))
            {
                goto Label_0326;
            }
        Label_035F:
            if (json.magnifications == null)
            {
                goto Label_03AB;
            }
            num6 = 0;
            goto Label_039C;
        Label_0372:
            param6 = new MagnificationParam();
            param6.Deserialize(json.magnifications[num6]);
            this.mMagnifications.Add(param6);
            num6 += 1;
        Label_039C:
            if (num6 < ((int) json.magnifications.Length))
            {
                goto Label_0372;
            }
        Label_03AB:
            if (json.conditions == null)
            {
                goto Label_03F8;
            }
            num7 = 0;
            goto Label_03E9;
        Label_03BE:
            param7 = new QuestCondParam();
            param7.Deserialize(json.conditions[num7]);
            this.mConditions.Add(param7);
            num7 += 1;
        Label_03E9:
            if (num7 < ((int) json.conditions.Length))
            {
                goto Label_03BE;
            }
        Label_03F8:
            if (json.parties == null)
            {
                goto Label_0445;
            }
            num8 = 0;
            goto Label_0436;
        Label_040B:
            param8 = new QuestPartyParam();
            param8.Deserialize(json.parties[num8]);
            this.mParties.Add(param8);
            num8 += 1;
        Label_0436:
            if (num8 < ((int) json.parties.Length))
            {
                goto Label_040B;
            }
        Label_0445:
            num9 = 0;
            goto Label_04E9;
        Label_044D:
            param9 = new QuestParam();
            param9.Deserialize(json.quests[num9]);
            this.mQuests.Add(param9);
            this.mQuestsDict.Add(param9.iname, param9);
            if (string.IsNullOrEmpty(param9.ChapterID) != null)
            {
                goto Label_04C9;
            }
            param9.Chapter = this.FindArea(param9.ChapterID);
            if (param9.Chapter == null)
            {
                goto Label_04C9;
            }
            param9.Chapter.quests.Add(param9);
        Label_04C9:
            if (param9.type != 7)
            {
                goto Label_04E3;
            }
            this.mTowerBaseQuests.Add(param9);
        Label_04E3:
            num9 += 1;
        Label_04E9:
            if (num9 < ((int) json.quests.Length))
            {
                goto Label_044D;
            }
            if (this.mPlayer.UnitNum < 1)
            {
                goto Label_0545;
            }
            num10 = 0;
            goto Label_052E;
        Label_0511:
            this.mPlayer.Units[num10].ResetCharacterQuestParams();
            num10 += 1;
        Label_052E:
            if (num10 < this.mPlayer.Units.Count)
            {
                goto Label_0511;
            }
        Label_0545:
            if (json.CampaignParents == null)
            {
                goto Label_0596;
            }
            num11 = 0;
            goto Label_0587;
        Label_0558:
            param10 = new QuestCampaignParentParam();
            if (param10.Deserialize(json.CampaignParents[num11]) == null)
            {
                goto Label_0581;
            }
            this.mCampaignParents.Add(param10);
        Label_0581:
            num11 += 1;
        Label_0587:
            if (num11 < ((int) json.CampaignParents.Length))
            {
                goto Label_0558;
            }
        Label_0596:
            if (json.CampaignChildren == null)
            {
                goto Label_0651;
            }
            num12 = 0;
            goto Label_0642;
        Label_05A9:
            param11 = new QuestCampaignChildParam();
            if (param11.Deserialize(json.CampaignChildren[num12]) == null)
            {
                goto Label_063C;
            }
            list = new List<QuestCampaignParentParam>();
            enumerator = this.mCampaignParents.GetEnumerator();
        Label_05D9:
            try
            {
                goto Label_0603;
            Label_05DE:
                param12 = &enumerator.Current;
                if (param12.IsChild(param11.iname) == null)
                {
                    goto Label_0603;
                }
                list.Add(param12);
            Label_0603:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_05DE;
                }
                goto Label_0621;
            }
            finally
            {
            Label_0614:
                ((List<QuestCampaignParentParam>.Enumerator) enumerator).Dispose();
            }
        Label_0621:
            param11.parents = list.ToArray();
            this.mCampaignChildren.Add(param11);
        Label_063C:
            num12 += 1;
        Label_0642:
            if (num12 < ((int) json.CampaignChildren.Length))
            {
                goto Label_05A9;
            }
        Label_0651:
            if (json.CampaignTrust == null)
            {
                goto Label_06E7;
            }
            num13 = 0;
            goto Label_06D8;
        Label_0664:
            storey = new <Deserialize>c__AnonStorey296();
            storey.param = new QuestCampaignTrust();
            if (storey.param.Deserialize(json.CampaignTrust[num13]) == null)
            {
                goto Label_06D2;
            }
            this.mCampaignTrust.Add(storey.param);
            param13 = this.mCampaignChildren.Find(new Predicate<QuestCampaignChildParam>(storey.<>m__1F8));
            if (param13 == null)
            {
                goto Label_06D2;
            }
            param13.campaignTrust = storey.param;
        Label_06D2:
            num13 += 1;
        Label_06D8:
            if (num13 < ((int) json.CampaignTrust.Length))
            {
                goto Label_0664;
            }
        Label_06E7:
            if (json.towerRewards == null)
            {
                goto Label_0733;
            }
            num14 = 0;
            goto Label_0724;
        Label_06FA:
            param14 = new TowerRewardParam();
            param14.Deserialize(json.towerRewards[num14]);
            this.mTowerRewards.Add(param14);
            num14 += 1;
        Label_0724:
            if (num14 < ((int) json.towerRewards.Length))
            {
                goto Label_06FA;
            }
        Label_0733:
            if (json.towerRoundRewards == null)
            {
                goto Label_077F;
            }
            num15 = 0;
            goto Label_0770;
        Label_0746:
            param15 = new TowerRoundRewardParam();
            param15.Deserialize(json.towerRoundRewards[num15]);
            this.mTowerRoundRewards.Add(param15);
            num15 += 1;
        Label_0770:
            if (num15 < ((int) json.towerRoundRewards.Length))
            {
                goto Label_0746;
            }
        Label_077F:
            if (json.towerFloors == null)
            {
                goto Label_07DF;
            }
            num16 = 0;
            goto Label_07D0;
        Label_0792:
            param16 = new TowerFloorParam();
            param16.Deserialize(json.towerFloors[num16]);
            this.mTowerFloors.Add(param16);
            this.mTowerFloorsDict.Add(param16.iname, param16);
            num16 += 1;
        Label_07D0:
            if (num16 < ((int) json.towerFloors.Length))
            {
                goto Label_0792;
            }
        Label_07DF:
            if (json.towers == null)
            {
                goto Label_082B;
            }
            num17 = 0;
            goto Label_081C;
        Label_07F2:
            param17 = new TowerParam();
            param17.Deserialize(json.towers[num17]);
            this.mTowers.Add(param17);
            num17 += 1;
        Label_081C:
            if (num17 < ((int) json.towers.Length))
            {
                goto Label_07F2;
            }
        Label_082B:
            if (json.versusTowerFloor == null)
            {
                goto Label_088A;
            }
            this.mVersusTowerFloor = new List<VersusTowerParam>((int) json.versusTowerFloor.Length);
            num18 = 0;
            goto Label_087B;
        Label_0851:
            param18 = new VersusTowerParam();
            param18.Deserialize(json.versusTowerFloor[num18]);
            this.mVersusTowerFloor.Add(param18);
            num18 += 1;
        Label_087B:
            if (num18 < ((int) json.versusTowerFloor.Length))
            {
                goto Label_0851;
            }
        Label_088A:
            if (json.versusschedule == null)
            {
                goto Label_08E9;
            }
            this.mVersusScheduleParam = new List<VersusScheduleParam>((int) json.versusschedule.Length);
            num19 = 0;
            goto Label_08DA;
        Label_08B0:
            param19 = new VersusScheduleParam();
            param19.Deserialize(json.versusschedule[num19]);
            this.mVersusScheduleParam.Add(param19);
            num19 += 1;
        Label_08DA:
            if (num19 < ((int) json.versusschedule.Length))
            {
                goto Label_08B0;
            }
        Label_08E9:
            if (json.versuscoin == null)
            {
                goto Label_0948;
            }
            this.mVersusCoinParam = new List<VersusCoinParam>((int) json.versuscoin.Length);
            num20 = 0;
            goto Label_0939;
        Label_090F:
            param20 = new VersusCoinParam();
            param20.Deserialize(json.versuscoin[num20]);
            this.mVersusCoinParam.Add(param20);
            num20 += 1;
        Label_0939:
            if (num20 < ((int) json.versuscoin.Length))
            {
                goto Label_090F;
            }
        Label_0948:
            if (json.multitowerFloor == null)
            {
                goto Label_09A7;
            }
            this.mMultiTowerFloor = new List<MultiTowerFloorParam>((int) json.multitowerFloor.Length);
            num21 = 0;
            goto Label_0998;
        Label_096E:
            param21 = new MultiTowerFloorParam();
            param21.Deserialize(json.multitowerFloor[num21]);
            this.mMultiTowerFloor.Add(param21);
            num21 += 1;
        Label_0998:
            if (num21 < ((int) json.multitowerFloor.Length))
            {
                goto Label_096E;
            }
        Label_09A7:
            if (json.multitowerRewards == null)
            {
                goto Label_09F3;
            }
            num22 = 0;
            goto Label_09E4;
        Label_09BA:
            param22 = new MultiTowerRewardParam();
            param22.Deserialize(json.multitowerRewards[num22]);
            this.mMultiTowerRewards.Add(param22);
            num22 += 1;
        Label_09E4:
            if (num22 < ((int) json.multitowerRewards.Length))
            {
                goto Label_09BA;
            }
        Label_09F3:
            if (json.MapEffect == null)
            {
                goto Label_0A5D;
            }
            list2 = new List<SRPG.MapEffectParam>((int) json.MapEffect.Length);
            num23 = 0;
            goto Label_0A3B;
        Label_0A15:
            param23 = new SRPG.MapEffectParam();
            param23.Deserialize(json.MapEffect[num23]);
            list2.Add(param23);
            num23 += 1;
        Label_0A3B:
            if (num23 < ((int) json.MapEffect.Length))
            {
                goto Label_0A15;
            }
            this.mMapEffectParam = list2;
            this.mMasterParam.MakeMapEffectHaveJobLists();
        Label_0A5D:
            if (json.WeatherSet == null)
            {
                goto Label_0ABC;
            }
            list3 = new List<WeatherSetParam>((int) json.WeatherSet.Length);
            num24 = 0;
            goto Label_0AA5;
        Label_0A7F:
            param24 = new WeatherSetParam();
            param24.Deserialize(json.WeatherSet[num24]);
            list3.Add(param24);
            num24 += 1;
        Label_0AA5:
            if (num24 < ((int) json.WeatherSet.Length))
            {
                goto Label_0A7F;
            }
            this.mWeatherSetParam = list3;
        Label_0ABC:
            if (json.rankingQuestSchedule == null)
            {
                goto Label_0B1C;
            }
            this.mRankingQuestScheduleParam = new List<RankingQuestScheduleParam>((int) json.rankingQuestSchedule.Length);
            num25 = 0;
            goto Label_0B0D;
        Label_0AE2:
            param25 = new RankingQuestScheduleParam();
            param25.Deserialize(json.rankingQuestSchedule[num25]);
            this.mRankingQuestScheduleParam.Add(param25);
            num25 += 1;
        Label_0B0D:
            if (num25 < ((int) json.rankingQuestSchedule.Length))
            {
                goto Label_0AE2;
            }
        Label_0B1C:
            if (json.rankingQuestRewards == null)
            {
                goto Label_0B7C;
            }
            this.mRankingQuestRewardParam = new List<RankingQuestRewardParam>((int) json.rankingQuestRewards.Length);
            num26 = 0;
            goto Label_0B6D;
        Label_0B42:
            param26 = new RankingQuestRewardParam();
            param26.Deserialize(json.rankingQuestRewards[num26]);
            this.mRankingQuestRewardParam.Add(param26);
            num26 += 1;
        Label_0B6D:
            if (num26 < ((int) json.rankingQuestRewards.Length))
            {
                goto Label_0B42;
            }
        Label_0B7C:
            if (json.rankingQuests == null)
            {
                goto Label_0C02;
            }
            this.mRankingQuestParam = new List<RankingQuestParam>((int) json.rankingQuests.Length);
            num27 = 0;
            goto Label_0BF3;
        Label_0BA2:
            param27 = new RankingQuestParam();
            param27.Deserialize(json.rankingQuests[num27]);
            param27.rewardParam = RankingQuestRewardParam.FindByID(param27.reward_id);
            param27.scheduleParam = RankingQuestScheduleParam.FindByID(param27.schedule_id);
            this.mRankingQuestParam.Add(param27);
            num27 += 1;
        Label_0BF3:
            if (num27 < ((int) json.rankingQuests.Length))
            {
                goto Label_0BA2;
            }
        Label_0C02:
            if (json.versusfirstwinbonus == null)
            {
                goto Label_0C6B;
            }
            num28 = (int) json.versusfirstwinbonus.Length;
            this.mVersusFirstWinBonus = new List<VersusFirstWinBonusParam>(num28);
            num29 = 0;
            goto Label_0C62;
        Label_0C2C:
            param28 = new VersusFirstWinBonusParam();
            if (param28 == null)
            {
                goto Label_0C5C;
            }
            if (param28.Deserialize(json.versusfirstwinbonus[num29]) == null)
            {
                goto Label_0C5C;
            }
            this.mVersusFirstWinBonus.Add(param28);
        Label_0C5C:
            num29 += 1;
        Label_0C62:
            if (num29 < num28)
            {
                goto Label_0C2C;
            }
        Label_0C6B:
            if (json.versusstreakwinschedule == null)
            {
                goto Label_0CD4;
            }
            num30 = (int) json.versusstreakwinschedule.Length;
            this.mVersusStreakSchedule = new List<VersusStreakWinScheduleParam>(num30);
            num31 = 0;
            goto Label_0CCB;
        Label_0C95:
            param29 = new VersusStreakWinScheduleParam();
            if (param29 == null)
            {
                goto Label_0CC5;
            }
            if (param29.Deserialize(json.versusstreakwinschedule[num31]) == null)
            {
                goto Label_0CC5;
            }
            this.mVersusStreakSchedule.Add(param29);
        Label_0CC5:
            num31 += 1;
        Label_0CCB:
            if (num31 < num30)
            {
                goto Label_0C95;
            }
        Label_0CD4:
            if (json.versusstreakwinbonus == null)
            {
                goto Label_0D3D;
            }
            num32 = (int) json.versusstreakwinbonus.Length;
            this.mVersusStreakWinBonus = new List<VersusStreakWinBonusParam>(num32);
            num33 = 0;
            goto Label_0D34;
        Label_0CFE:
            param30 = new VersusStreakWinBonusParam();
            if (param30 == null)
            {
                goto Label_0D2E;
            }
            if (param30.Deserialize(json.versusstreakwinbonus[num33]) == null)
            {
                goto Label_0D2E;
            }
            this.mVersusStreakWinBonus.Add(param30);
        Label_0D2E:
            num33 += 1;
        Label_0D34:
            if (num33 < num32)
            {
                goto Label_0CFE;
            }
        Label_0D3D:
            if (json.versusrule == null)
            {
                goto Label_0DA6;
            }
            num34 = (int) json.versusrule.Length;
            this.mVersusRule = new List<VersusRuleParam>(num34);
            num35 = 0;
            goto Label_0D9D;
        Label_0D67:
            param31 = new VersusRuleParam();
            if (param31 == null)
            {
                goto Label_0D97;
            }
            if (param31.Deserialize(json.versusrule[num35]) == null)
            {
                goto Label_0D97;
            }
            this.mVersusRule.Add(param31);
        Label_0D97:
            num35 += 1;
        Label_0D9D:
            if (num35 < num34)
            {
                goto Label_0D67;
            }
        Label_0DA6:
            if (json.versuscoincamp == null)
            {
                goto Label_0E0F;
            }
            num36 = (int) json.versuscoincamp.Length;
            this.mVersusCoinCamp = new List<VersusCoinCampParam>(num36);
            num37 = 0;
            goto Label_0E06;
        Label_0DD0:
            param32 = new VersusCoinCampParam();
            if (param32 == null)
            {
                goto Label_0E00;
            }
            if (param32.Deserialize(json.versuscoincamp[num37]) == null)
            {
                goto Label_0E00;
            }
            this.mVersusCoinCamp.Add(param32);
        Label_0E00:
            num37 += 1;
        Label_0E06:
            if (num37 < num36)
            {
                goto Label_0DD0;
            }
        Label_0E0F:
            if (json.versusenabletime == null)
            {
                goto Label_0E71;
            }
            num38 = (int) json.versusenabletime.Length;
            this.mVersusEnableTime = new List<VersusEnableTimeParam>(num38);
            num39 = 0;
            goto Label_0E68;
        Label_0E39:
            param33 = new VersusEnableTimeParam();
            if (param33.Deserialize(json.versusenabletime[num39]) == null)
            {
                goto Label_0E62;
            }
            this.mVersusEnableTime.Add(param33);
        Label_0E62:
            num39 += 1;
        Label_0E68:
            if (num39 < num38)
            {
                goto Label_0E39;
            }
        Label_0E71:
            if (json.VersusRank == null)
            {
                goto Label_0ED3;
            }
            num40 = (int) json.VersusRank.Length;
            this.mVersusRank = new List<VersusRankParam>(num40);
            num41 = 0;
            goto Label_0ECA;
        Label_0E9B:
            param34 = new VersusRankParam();
            if (param34.Deserialize(json.VersusRank[num41]) == null)
            {
                goto Label_0EC4;
            }
            this.mVersusRank.Add(param34);
        Label_0EC4:
            num41 += 1;
        Label_0ECA:
            if (num41 < num40)
            {
                goto Label_0E9B;
            }
        Label_0ED3:
            if (json.VersusRankClass == null)
            {
                goto Label_0F35;
            }
            num42 = (int) json.VersusRankClass.Length;
            this.mVersusRankClass = new List<VersusRankClassParam>(num42);
            num43 = 0;
            goto Label_0F2C;
        Label_0EFD:
            param35 = new VersusRankClassParam();
            if (param35.Deserialize(json.VersusRankClass[num43]) == null)
            {
                goto Label_0F26;
            }
            this.mVersusRankClass.Add(param35);
        Label_0F26:
            num43 += 1;
        Label_0F2C:
            if (num43 < num42)
            {
                goto Label_0EFD;
            }
        Label_0F35:
            if (json.VersusRankRankingReward == null)
            {
                goto Label_0F97;
            }
            num44 = (int) json.VersusRankRankingReward.Length;
            this.mVersusRankRankingReward = new List<VersusRankRankingRewardParam>(num44);
            num45 = 0;
            goto Label_0F8E;
        Label_0F5F:
            param36 = new VersusRankRankingRewardParam();
            if (param36.Deserialize(json.VersusRankRankingReward[num45]) == null)
            {
                goto Label_0F88;
            }
            this.mVersusRankRankingReward.Add(param36);
        Label_0F88:
            num45 += 1;
        Label_0F8E:
            if (num45 < num44)
            {
                goto Label_0F5F;
            }
        Label_0F97:
            if (json.VersusRankReward == null)
            {
                goto Label_0FF9;
            }
            num46 = (int) json.VersusRankReward.Length;
            this.mVersusRankReward = new List<VersusRankRewardParam>(num46);
            num47 = 0;
            goto Label_0FF0;
        Label_0FC1:
            param37 = new VersusRankRewardParam();
            if (param37.Deserialize(json.VersusRankReward[num47]) == null)
            {
                goto Label_0FEA;
            }
            this.mVersusRankReward.Add(param37);
        Label_0FEA:
            num47 += 1;
        Label_0FF0:
            if (num47 < num46)
            {
                goto Label_0FC1;
            }
        Label_0FF9:
            if (json.VersusRankMissionSchedule == null)
            {
                goto Label_105B;
            }
            num48 = (int) json.VersusRankMissionSchedule.Length;
            this.mVersusRankMissionSchedule = new List<VersusRankMissionScheduleParam>(num48);
            num49 = 0;
            goto Label_1052;
        Label_1023:
            param38 = new VersusRankMissionScheduleParam();
            if (param38.Deserialize(json.VersusRankMissionSchedule[num49]) == null)
            {
                goto Label_104C;
            }
            this.mVersusRankMissionSchedule.Add(param38);
        Label_104C:
            num49 += 1;
        Label_1052:
            if (num49 < num48)
            {
                goto Label_1023;
            }
        Label_105B:
            if (json.VersusRankMission == null)
            {
                goto Label_10BD;
            }
            num50 = (int) json.VersusRankMission.Length;
            this.mVersusRankMission = new List<VersusRankMissionParam>(num50);
            num51 = 0;
            goto Label_10B4;
        Label_1085:
            param39 = new VersusRankMissionParam();
            if (param39.Deserialize(json.VersusRankMission[num51]) == null)
            {
                goto Label_10AE;
            }
            this.mVersusRankMission.Add(param39);
        Label_10AE:
            num51 += 1;
        Label_10B4:
            if (num51 < num50)
            {
                goto Label_1085;
            }
        Label_10BD:
            if (json.GuerrillaShopAdventQuest == null)
            {
                goto Label_111F;
            }
            num52 = (int) json.GuerrillaShopAdventQuest.Length;
            this.mGuerrillaShopAdventQuest = new List<GuerrillaShopAdventQuestParam>(num52);
            num53 = 0;
            goto Label_1116;
        Label_10E7:
            param40 = new GuerrillaShopAdventQuestParam();
            if (param40.Deserialize(json.GuerrillaShopAdventQuest[num53]) == null)
            {
                goto Label_1110;
            }
            this.mGuerrillaShopAdventQuest.Add(param40);
        Label_1110:
            num53 += 1;
        Label_1116:
            if (num53 < num52)
            {
                goto Label_10E7;
            }
        Label_111F:
            if (json.GuerrillaShopSchedule == null)
            {
                goto Label_1181;
            }
            num54 = (int) json.GuerrillaShopSchedule.Length;
            this.mGuerrillaShopScheduleParam = new List<GuerrillaShopScheduleParam>(num54);
            num55 = 0;
            goto Label_1178;
        Label_1149:
            param41 = new GuerrillaShopScheduleParam();
            if (param41.Deserialize(json.GuerrillaShopSchedule[num55]) == null)
            {
                goto Label_1172;
            }
            this.mGuerrillaShopScheduleParam.Add(param41);
        Label_1172:
            num55 += 1;
        Label_1178:
            if (num55 < num54)
            {
                goto Label_1149;
            }
        Label_1181:
            if (json.VersusDraftUnit == null)
            {
                goto Label_11E9;
            }
            num56 = (int) json.VersusDraftUnit.Length;
            this.mVersusDraftUnit = new List<VersusDraftUnitParam>(num56);
            num57 = 0;
            goto Label_11E0;
        Label_11AB:
            param42 = new VersusDraftUnitParam();
            if (param42.Deserialize(((long) num57) + 1L, json.VersusDraftUnit[num57]) == null)
            {
                goto Label_11DA;
            }
            this.mVersusDraftUnit.Add(param42);
        Label_11DA:
            num57 += 1;
        Label_11E0:
            if (num57 < num56)
            {
                goto Label_11AB;
            }
        Label_11E9:
            return;
        }

        public void Deserialize(JSON_ReqTowerResuponse json)
        {
            SRPG.TowerResuponse resuponse;
            resuponse = new SRPG.TowerResuponse();
            resuponse.Deserialize(json);
            this.mTowerResuponse = resuponse;
            return;
        }

        public void Deserialize(Json_Versus json)
        {
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mPlayer.Deserialize(json);
            return;
        }

        public void Deserialize(ReqMultiRank.Json_MultiRank json)
        {
            int num;
            MultiRanking ranking;
            if (json != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mMultiUnitRank = null;
            this.mMultiUnitRank = new List<MultiRanking>();
            if (json.isReady != 1)
            {
                goto Label_0076;
            }
            num = 0;
            goto Label_0068;
        Label_002C:
            ranking = new MultiRanking();
            ranking.unit = json.ranking[num].unit_iname;
            ranking.job = json.ranking[num].job_iname;
            this.mMultiUnitRank.Add(ranking);
            num += 1;
        Label_0068:
            if (num < ((int) json.ranking.Length))
            {
                goto Label_002C;
            }
        Label_0076:
            return;
        }

        public void Deserialize(RankingData[] datas)
        {
            int num;
            if (this.mUnitRanking != null)
            {
                goto Label_0016;
            }
            this.mUnitRanking = new Dictionary<string, RankingData>();
        Label_0016:
            this.mUnitRanking.Clear();
            num = 0;
            goto Label_004A;
        Label_0028:
            if (datas[num] == null)
            {
                goto Label_0046;
            }
            this.mUnitRanking[datas[num].iname] = datas[num];
        Label_0046:
            num += 1;
        Label_004A:
            if (num < ((int) datas.Length))
            {
                goto Label_0028;
            }
            return;
        }

        public void Deserialize(ReqMultiTwStatus.FloorParam[] param)
        {
            int num;
            if (this.mMultiTowerRound != null)
            {
                goto Label_0016;
            }
            this.mMultiTowerRound = new MultiTowerRoundParam();
        Label_0016:
            this.mMultiTowerRound.Now = 0;
            if (this.mMultiTowerRound.Round != null)
            {
                goto Label_0042;
            }
            this.mMultiTowerRound.Round = new List<int>();
        Label_0042:
            this.mMultiTowerRound.Round.Clear();
            if (param == null)
            {
                goto Label_009B;
            }
            num = 0;
            goto Label_007B;
        Label_005F:
            this.mMultiTowerRound.Round.Add(param[num].clear_count);
            num += 1;
        Label_007B:
            if (num < ((int) param.Length))
            {
                goto Label_005F;
            }
            this.mMultiTowerRound.Now = param[((int) param.Length) - 1].floor;
        Label_009B:
            return;
        }

        public bool Deserialize(JSON_ArenaRanking json, ReqBtlColoRanking.RankingTypes type)
        {
            List<ArenaPlayer> list;
            int num;
            ArenaPlayer player;
            Exception exception;
            list = this.mArenaRanking[type];
            list.Clear();
            if (json.coloenemies == null)
            {
                goto Label_005F;
            }
            num = 0;
            goto Label_0051;
        Label_0021:
            player = new ArenaPlayer();
        Label_0027:
            try
            {
                player.Deserialize(json.coloenemies[num]);
                list.Add(player);
                goto Label_004D;
            }
            catch (Exception exception1)
            {
            Label_0041:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_004D;
            }
        Label_004D:
            num += 1;
        Label_0051:
            if (num < ((int) json.coloenemies.Length))
            {
                goto Label_0021;
            }
        Label_005F:
            return 1;
        }

        public void Deserialize(Json_Artifact[] json, bool differenceUpdate)
        {
            this.mPlayer.Deserialize(json, differenceUpdate);
            return;
        }

        public void Deserialize(JSON_ConceptCard[] json, bool is_data_override)
        {
            this.mPlayer.Deserialize(json, is_data_override);
            return;
        }

        public void Deserialize(JSON_ConceptCardMaterial[] json, bool is_data_override)
        {
            this.mPlayer.Deserialize(json, is_data_override);
            return;
        }

        public void Deserialize(Json_Friend[] json, FriendStates state)
        {
            this.mPlayer.Deserialize(json, state);
            return;
        }

        public bool Deserialize2(JSON_MasterParam json)
        {
            bool flag;
            flag = 1;
            flag &= this.mMasterParam.Deserialize2(json);
            if (flag == null)
            {
                goto Label_0022;
            }
            this.mMasterParam.CacheReferences();
        Label_0022:
            return flag;
        }

        public bool DeserializeGps(JSON_QuestProgress[] jsons)
        {
            int num;
            JSON_QuestProgress progress;
            QuestParam param;
            if (jsons != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            num = 0;
            goto Label_0052;
        Label_000F:
            progress = jsons[num];
            if (progress != null)
            {
                goto Label_001E;
            }
            goto Label_004E;
        Label_001E:
            param = this.FindQuest(progress.i);
            if (param == null)
            {
                goto Label_004E;
            }
            if (param.IsGps != null)
            {
                goto Label_0047;
            }
            if (param.IsMultiAreaQuest == null)
            {
                goto Label_004E;
            }
        Label_0047:
            param.gps_enable = 1;
        Label_004E:
            num += 1;
        Label_0052:
            if (num < ((int) jsons.Length))
            {
                goto Label_000F;
            }
            return 1;
        }

        public void DownloadAndTransitScene(string sceneName)
        {
            if (AssetManager.IsAssetBundle(sceneName) == null)
            {
                goto Label_0024;
            }
            CriticalSection.Enter(4);
            base.StartCoroutine(this.DownloadAndTransitSceneAsync(sceneName));
            goto Label_002A;
        Label_0024:
            SceneManager.LoadScene(sceneName);
        Label_002A:
            return;
        }

        [DebuggerHidden]
        private IEnumerator DownloadAndTransitSceneAsync(string sceneName)
        {
            <DownloadAndTransitSceneAsync>c__IteratorD6 rd;
            rd = new <DownloadAndTransitSceneAsync>c__IteratorD6();
            rd.sceneName = sceneName;
            rd.<$>sceneName = sceneName;
            return rd;
        }

        public void DownloadTutorialAssets()
        {
            int num;
            num = 0;
            goto Label_0051;
        Label_0007:
            if (AssetManager.IsAssetInCache(this.mTutorialDLAssets[num].IDStr) == null)
            {
                goto Label_0037;
            }
            this.mTutorialDLAssets.RemoveAt(num--);
            goto Label_004D;
        Label_0037:
            AssetDownloader.Add(this.mTutorialDLAssets[num].IDStr);
        Label_004D:
            num += 1;
        Label_0051:
            if (num < this.mTutorialDLAssets.Count)
            {
                goto Label_0007;
            }
            this.mTutorialDLAssets.Clear();
            return;
        }

        public string Encrypt(string key, string iv, string src)
        {
            RijndaelManaged managed;
            byte[] buffer;
            byte[] buffer2;
            byte[] buffer3;
            ICryptoTransform transform;
            MemoryStream stream;
            CryptoStream stream2;
            byte[] buffer4;
            int num;
            managed = new RijndaelManaged();
            managed.Padding = 3;
            managed.Mode = 1;
            num = 0x80;
            managed.BlockSize = num;
            managed.KeySize = num;
            buffer = Encoding.UTF8.GetBytes(key);
            buffer2 = Encoding.UTF8.GetBytes(iv);
            buffer3 = Encoding.UTF8.GetBytes(src);
            transform = managed.CreateEncryptor(buffer, buffer2);
            stream = new MemoryStream();
            stream2 = new CryptoStream(stream, transform, 1);
            stream2.Write(buffer3, 0, (int) buffer3.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());
        }

        public bool ExistsOpenVersusTower(string towerID)
        {
            List<VersusScheduleParam> list;
            VersusScheduleParam param;
            <ExistsOpenVersusTower>c__AnonStorey29B storeyb;
            storeyb = new <ExistsOpenVersusTower>c__AnonStorey29B();
            storeyb.towerID = towerID;
            if (<>f__am$cache9B != null)
            {
                goto Label_002B;
            }
            <>f__am$cache9B = new Predicate<VersusScheduleParam>(GameManager.<ExistsOpenVersusTower>m__203);
        Label_002B:
            list = this.mVersusScheduleParam.FindAll(<>f__am$cache9B);
            if (list.Count >= 1)
            {
                goto Label_0044;
            }
            return 0;
        Label_0044:
            if (string.IsNullOrEmpty(storeyb.towerID) == null)
            {
                goto Label_0056;
            }
            return 1;
        Label_0056:
            return ((list.Find(new Predicate<VersusScheduleParam>(storeyb.<>m__204)) == null) == 0);
        }

        public AchievementParam FindAchievement(int id)
        {
            int num;
            num = this.mAchievement.Count - 1;
            goto Label_003B;
        Label_0013:
            if (this.mAchievement[num].id != id)
            {
                goto Label_0037;
            }
            return this.mAchievement[num];
        Label_0037:
            num -= 1;
        Label_003B:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return null;
        }

        public ChapterParam FindArea(string iname)
        {
            int num;
            num = this.mAreas.Count - 1;
            goto Label_0040;
        Label_0013:
            if ((this.mAreas[num].iname == iname) == null)
            {
                goto Label_003C;
            }
            return this.mAreas[num];
        Label_003C:
            num -= 1;
        Label_0040:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return null;
        }

        public ArenaPlayer FindArenaPlayer(string fuid)
        {
            <FindArenaPlayer>c__AnonStorey294 storey;
            storey = new <FindArenaPlayer>c__AnonStorey294();
            storey.fuid = fuid;
            return this.mArenaPlayers.Find(new Predicate<ArenaPlayer>(storey.<>m__1F6));
        }

        public RankingQuestParam FindAvailableRankingQuest(string iname)
        {
            <FindAvailableRankingQuest>c__AnonStorey2A1 storeya;
            storeya = new <FindAvailableRankingQuest>c__AnonStorey2A1();
            storeya.iname = iname;
            return this.mAvailableRankingQuesstParams.Find(new Predicate<RankingQuestParam>(storeya.<>m__20A));
        }

        public QuestParam FindBaseQuest(QuestTypes questType, string iname)
        {
            int num;
            if (questType != 7)
            {
                goto Label_004E;
            }
            num = this.mTowerBaseQuests.Count - 1;
            goto Label_0047;
        Label_001A:
            if ((this.mTowerBaseQuests[num].iname == iname) == null)
            {
                goto Label_0043;
            }
            return this.mTowerBaseQuests[num];
        Label_0043:
            num -= 1;
        Label_0047:
            if (num >= 0)
            {
                goto Label_001A;
            }
        Label_004E:
            return null;
        }

        public TowerFloorParam FindFirstTowerFloor(string towerID)
        {
            List<TowerFloorParam> list;
            if (this.mTowerFloors == null)
            {
                goto Label_003F;
            }
            list = this.FindTowerFloors(towerID);
            if (list != null)
            {
                goto Label_001B;
            }
            return null;
        Label_001B:
            if (<>f__am$cache96 != null)
            {
                goto Label_0034;
            }
            <>f__am$cache96 = new Predicate<TowerFloorParam>(GameManager.<FindFirstTowerFloor>m__1F3);
        Label_0034:
            return list.Find(<>f__am$cache96);
        Label_003F:
            return null;
        }

        public TowerFloorParam FindLastTowerFloor(string towerID)
        {
            List<TowerFloorParam> list;
            if (this.mTowerFloors == null)
            {
                goto Label_0038;
            }
            list = this.FindTowerFloors(towerID);
            if (list != null)
            {
                goto Label_001B;
            }
            return null;
        Label_001B:
            if (list.Count >= 1)
            {
                goto Label_0029;
            }
            return null;
        Label_0029:
            return list[list.Count - 1];
        Label_0038:
            return null;
        }

        public MagnificationParam FindMagnification(string iname)
        {
            int num;
            num = this.mMagnifications.Count - 1;
            goto Label_0040;
        Label_0013:
            if ((this.mMagnifications[num].iname == iname) == null)
            {
                goto Label_003C;
            }
            return this.mMagnifications[num];
        Label_003C:
            num -= 1;
        Label_0040:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return null;
        }

        public MailData FindMail(long mailID)
        {
            <FindMail>c__AnonStorey295 storey;
            storey = new <FindMail>c__AnonStorey295();
            storey.mailID = mailID;
            if (this.mPlayer != null)
            {
                goto Label_001A;
            }
            return null;
        Label_001A:
            if (this.mPlayer.CurrentMails != null)
            {
                goto Label_002C;
            }
            return null;
        Label_002C:
            return this.mPlayer.CurrentMails.Find(new Predicate<MailData>(storey.<>m__1F7));
        }

        public TowerFloorParam FindNextTowerFloor(string towerID, string currentFloorID)
        {
            List<TowerFloorParam> list;
            <FindNextTowerFloor>c__AnonStorey292 storey;
            storey = new <FindNextTowerFloor>c__AnonStorey292();
            storey.currentFloorID = currentFloorID;
            if (this.mTowerFloors == null)
            {
                goto Label_003B;
            }
            list = this.FindTowerFloors(towerID);
            if (list != null)
            {
                goto Label_0028;
            }
            return null;
        Label_0028:
            return list.Find(new Predicate<TowerFloorParam>(storey.<>m__1F4));
        Label_003B:
            return null;
        }

        public ObjectiveParam FindObjective(string iname)
        {
            int num;
            num = this.mObjectives.Count - 1;
            goto Label_0040;
        Label_0013:
            if ((this.mObjectives[num].iname == iname) == null)
            {
                goto Label_003C;
            }
            return this.mObjectives[num];
        Label_003C:
            num -= 1;
        Label_0040:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return null;
        }

        public QuestParam FindQuest(QuestTypes type)
        {
            int num;
            num = this.mQuests.Count - 1;
            goto Label_003B;
        Label_0013:
            if (this.mQuests[num].type != type)
            {
                goto Label_0037;
            }
            return this.mQuests[num];
        Label_0037:
            num -= 1;
        Label_003B:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return null;
        }

        public unsafe QuestParam FindQuest(string iname)
        {
            QuestParam param;
            TowerFloorParam param2;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mQuestsDict.TryGetValue(iname, &param) == null)
            {
                goto Label_0022;
            }
            return param;
        Label_0022:
            param2 = this.FindTowerFloor(iname);
            if (param2 == null)
            {
                goto Label_0039;
            }
            return param2.Clone(null, 1);
        Label_0039:
            return null;
        }

        public unsafe QuestCampaignData[] FindQuestCampaigns(string[] inames)
        {
            List<QuestCampaignData> list;
            QuestCampaignChildParam param;
            List<QuestCampaignChildParam>.Enumerator enumerator;
            string str;
            string[] strArray;
            int num;
            QuestCampaignData[] dataArray;
            QuestCampaignData[] dataArray2;
            int num2;
            QuestCampaignData data;
            <FindQuestCampaigns>c__AnonStorey290 storey;
            list = new List<QuestCampaignData>();
            if (this.mCampaignChildren == null)
            {
                goto Label_0128;
            }
            if (inames == null)
            {
                goto Label_0128;
            }
            if (((int) inames.Length) <= 0)
            {
                goto Label_0128;
            }
            enumerator = this.mCampaignChildren.GetEnumerator();
        Label_002C:
            try
            {
                goto Label_010B;
            Label_0031:
                param = &enumerator.Current;
                strArray = inames;
                num = 0;
                goto Label_0100;
            Label_0044:
                str = strArray[num];
                if ((str != param.iname) == null)
                {
                    goto Label_0060;
                }
                goto Label_00FA;
            Label_0060:
                dataArray = param.MakeData();
                storey = new <FindQuestCampaigns>c__AnonStorey290();
                dataArray2 = dataArray;
                num2 = 0;
                goto Label_00EA;
            Label_007B:
                storey.d = dataArray2[num2];
                data = list.Find(new Predicate<QuestCampaignData>(storey.<>m__1F1));
                if (data == null)
                {
                    goto Label_00D7;
                }
                if (data.type == 1)
                {
                    goto Label_00B5;
                }
                goto Label_00E4;
            Label_00B5:
                if ((data.unit == storey.d.unit) == null)
                {
                    goto Label_00D7;
                }
                goto Label_00E4;
            Label_00D7:
                list.Add(storey.d);
            Label_00E4:
                num2 += 1;
            Label_00EA:
                if (num2 < ((int) dataArray2.Length))
                {
                    goto Label_007B;
                }
                goto Label_010B;
            Label_00FA:
                num += 1;
            Label_0100:
                if (num < ((int) strArray.Length))
                {
                    goto Label_0044;
                }
            Label_010B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0031;
                }
                goto Label_0128;
            }
            finally
            {
            Label_011C:
                ((List<QuestCampaignChildParam>.Enumerator) enumerator).Dispose();
            }
        Label_0128:
            return list.ToArray();
        }

        public QuestCampaignData[] FindQuestCampaigns(QuestParam questParam)
        {
            List<QuestCampaignData> list;
            DateTime time;
            int num;
            QuestCampaignChildParam param;
            QuestCampaignData[] dataArray;
            QuestCampaignData[] dataArray2;
            int num2;
            QuestCampaignData data;
            <FindQuestCampaigns>c__AnonStorey291 storey;
            list = new List<QuestCampaignData>();
            time = TimeManager.ServerTime;
            if (this.mCampaignChildren == null)
            {
                goto Label_00EF;
            }
            num = this.mCampaignChildren.Count - 1;
            goto Label_00E8;
        Label_002A:
            param = this.mCampaignChildren[num];
            if (param.IsValidQuest(questParam) == null)
            {
                goto Label_00E4;
            }
            if (param.IsAvailablePeriod(time) == null)
            {
                goto Label_00E4;
            }
            dataArray = param.MakeData();
            storey = new <FindQuestCampaigns>c__AnonStorey291();
            dataArray2 = dataArray;
            num2 = 0;
            goto Label_00D9;
        Label_006A:
            storey.d = dataArray2[num2];
            data = list.Find(new Predicate<QuestCampaignData>(storey.<>m__1F2));
            if (data == null)
            {
                goto Label_00C6;
            }
            if (data.type == 1)
            {
                goto Label_00A4;
            }
            goto Label_00D3;
        Label_00A4:
            if ((data.unit == storey.d.unit) == null)
            {
                goto Label_00C6;
            }
            goto Label_00D3;
        Label_00C6:
            list.Add(storey.d);
        Label_00D3:
            num2 += 1;
        Label_00D9:
            if (num2 < ((int) dataArray2.Length))
            {
                goto Label_006A;
            }
        Label_00E4:
            num -= 1;
        Label_00E8:
            if (num >= 0)
            {
                goto Label_002A;
            }
        Label_00EF:
            return list.ToArray();
        }

        public QuestCondParam FindQuestCond(string iname)
        {
            int num;
            if (this.mConditions == null)
            {
                goto Label_0052;
            }
            num = this.mConditions.Count - 1;
            goto Label_004B;
        Label_001E:
            if ((this.mConditions[num].iname == iname) == null)
            {
                goto Label_0047;
            }
            return this.mConditions[num];
        Label_0047:
            num -= 1;
        Label_004B:
            if (num >= 0)
            {
                goto Label_001E;
            }
        Label_0052:
            return null;
        }

        public QuestPartyParam FindQuestParty(string iname)
        {
            int num;
            if (this.mParties == null)
            {
                goto Label_0052;
            }
            num = this.mParties.Count - 1;
            goto Label_004B;
        Label_001E:
            if ((this.mParties[num].iname == iname) == null)
            {
                goto Label_0047;
            }
            return this.mParties[num];
        Label_0047:
            num -= 1;
        Label_004B:
            if (num >= 0)
            {
                goto Label_001E;
            }
        Label_0052:
            return null;
        }

        public TowerParam FindTower(string iname)
        {
            int num;
            if (this.mTowers == null)
            {
                goto Label_0052;
            }
            num = this.mTowers.Count - 1;
            goto Label_004B;
        Label_001E:
            if ((this.mTowers[num].iname == iname) == null)
            {
                goto Label_0047;
            }
            return this.mTowers[num];
        Label_0047:
            num -= 1;
        Label_004B:
            if (num >= 0)
            {
                goto Label_001E;
            }
        Label_0052:
            return null;
        }

        public unsafe TowerFloorParam FindTowerFloor(string iname)
        {
            TowerFloorParam param;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mTowerFloorsDict.TryGetValue(iname, &param) == null)
            {
                goto Label_0022;
            }
            return param;
        Label_0022:
            return null;
        }

        public List<TowerFloorParam> FindTowerFloors(string towerID)
        {
            <FindTowerFloors>c__AnonStorey293 storey;
            storey = new <FindTowerFloors>c__AnonStorey293();
            storey.towerID = towerID;
            if (this.mTowerFloors == null)
            {
                goto Label_0030;
            }
            return this.mTowerFloors.FindAll(new Predicate<TowerFloorParam>(storey.<>m__1F5));
        Label_0030:
            return null;
        }

        public ObjectiveParam FindTowerObjective(string iname)
        {
            int num;
            num = this.mTowerObjectives.Count - 1;
            goto Label_0040;
        Label_0013:
            if ((this.mTowerObjectives[num].iname == iname) == null)
            {
                goto Label_003C;
            }
            return this.mTowerObjectives[num];
        Label_003C:
            num -= 1;
        Label_0040:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return null;
        }

        public TowerRewardParam FindTowerReward(string iname)
        {
            int num;
            int num2;
            if (this.mTowerResuponse.round <= 0)
            {
                goto Label_0068;
            }
            if (this.mTowerRoundRewards == null)
            {
                goto Label_00BA;
            }
            num = this.mTowerRoundRewards.Count - 1;
            goto Label_005C;
        Label_002F:
            if ((this.mTowerRoundRewards[num].iname == iname) == null)
            {
                goto Label_0058;
            }
            return this.mTowerRoundRewards[num];
        Label_0058:
            num -= 1;
        Label_005C:
            if (num >= 0)
            {
                goto Label_002F;
            }
            goto Label_00BA;
        Label_0068:
            if (this.mTowerRewards == null)
            {
                goto Label_00BA;
            }
            num2 = this.mTowerRewards.Count - 1;
            goto Label_00B3;
        Label_0086:
            if ((this.mTowerRewards[num2].iname == iname) == null)
            {
                goto Label_00AF;
            }
            return this.mTowerRewards[num2];
        Label_00AF:
            num2 -= 1;
        Label_00B3:
            if (num2 >= 0)
            {
                goto Label_0086;
            }
        Label_00BA:
            return null;
        }

        public VersusScheduleParam FindVersusTowerScheduleParam(string towerID)
        {
            <FindVersusTowerScheduleParam>c__AnonStorey29A storeya;
            storeya = new <FindVersusTowerScheduleParam>c__AnonStorey29A();
            storeya.towerID = towerID;
            if (string.IsNullOrEmpty(storeya.towerID) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            return this.mVersusScheduleParam.Find(new Predicate<VersusScheduleParam>(storeya.<>m__202));
        }

        public SectionParam FindWorld(string iname)
        {
            int num;
            num = this.mWorlds.Count - 1;
            goto Label_0040;
        Label_0013:
            if ((this.mWorlds[num].iname == iname) == null)
            {
                goto Label_003C;
            }
            return this.mWorlds[num];
        Label_003C:
            num -= 1;
        Label_0040:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return null;
        }

        private string GenerateHash(string pass, string salt)
        {
            string str;
            SHA256 sha;
            string str2;
            UTF8Encoding encoding;
            byte[] buffer;
            byte[] buffer2;
            str = string.Empty;
            sha = SHA256.Create();
            str2 = pass + salt;
            encoding = new UTF8Encoding();
            buffer = encoding.GetBytes(str2);
            str = Convert.ToBase64String(sha.ComputeHash(buffer));
            sha.Clear();
            sha = null;
            return str;
        }

        private string GenerateSalt()
        {
            byte[] buffer;
            RNGCryptoServiceProvider provider;
            buffer = new byte[0x18];
            provider = new RNGCryptoServiceProvider();
            provider.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public AbilityParam GetAbilityParam(string key)
        {
            return this.mMasterParam.GetAbilityParam(key);
        }

        public AIParam GetAIParam(string key)
        {
            return this.mMasterParam.GetAIParam(key);
        }

        public JobParam[] GetAllJobs()
        {
            return this.mMasterParam.GetAllJobs();
        }

        public ArenaPlayerHistory[] GetArenaHistory()
        {
            return this.mArenaHistory.ToArray();
        }

        public ArenaPlayer[] GetArenaRanking(ReqBtlColoRanking.RankingTypes type)
        {
            return this.mArenaRanking[type].ToArray();
        }

        public AwardParam GetAwardParam(string key)
        {
            return this.mMasterParam.GetAwardParam(key);
        }

        public ChatChannelMasterParam[] GetChatChannelMaster()
        {
            return ((this.mChatChannelMasters == null) ? new ChatChannelMasterParam[0] : this.mChatChannelMasters.ToArray());
        }

        public JobSetParam[] GetClassChangeJobSetParam(string key)
        {
            return this.mMasterParam.GetClassChangeJobSetParam(key);
        }

        public ConceptCardParam GetConceptCardParam(string iname)
        {
            return this.mMasterParam.GetConceptCardParam(iname);
        }

        public SectionParam GetCurrentSectionParam()
        {
            SectionParam param;
            QuestParam param2;
            ChapterParam param3;
            param = null;
            if (string.IsNullOrEmpty(GlobalVars.SelectedSection) != null)
            {
                goto Label_004A;
            }
            param = this.FindWorld(GlobalVars.SelectedSection);
            if (param == null)
            {
                goto Label_004A;
            }
            if (string.IsNullOrEmpty(param.home) != null)
            {
                goto Label_0048;
            }
            if (param.hidden == null)
            {
                goto Label_004A;
            }
        Label_0048:
            param = null;
        Label_004A:
            if (param != null)
            {
                goto Label_0082;
            }
            param2 = this.Player.FindLastStoryQuest();
            if (param2 == null)
            {
                goto Label_0082;
            }
            param3 = this.FindArea(param2.ChapterID);
            if (param3 == null)
            {
                goto Label_0082;
            }
            param = this.FindWorld(param3.section);
        Label_0082:
            return param;
        }

        public VersusTowerParam GetCurrentVersusTowerParam(int idx)
        {
            GameManager manager;
            PlayerData data;
            <GetCurrentVersusTowerParam>c__AnonStorey299 storey;
            storey = new <GetCurrentVersusTowerParam>c__AnonStorey299();
            data = MonoSingleton<GameManager>.Instance.Player;
            storey.floor = (idx == -1) ? data.VersusTowerFloor : idx;
            storey.iname = this.VersusTowerMatchName;
            if (string.IsNullOrEmpty(storey.iname) != null)
            {
                goto Label_0060;
            }
            return this.mVersusTowerFloor.Find(new Predicate<VersusTowerParam>(storey.<>m__201));
        Label_0060:
            return null;
        }

        public List<GachaParam> GetGachaList(string category)
        {
            List<GachaParam> list;
            int num;
            list = new List<GachaParam>();
            num = 0;
            goto Label_003F;
        Label_000D:
            if ((this.mGachas[num].category == category) == null)
            {
                goto Label_003B;
            }
            list.Add(this.mGachas[num]);
        Label_003B:
            num += 1;
        Label_003F:
            if (num < this.mGachas.Count)
            {
                goto Label_000D;
            }
            return list;
        }

        public GeoParam GetGeoParam(string key)
        {
            return this.mMasterParam.GetGeoParam(key);
        }

        public GrowParam GetGrowParam(string key)
        {
            return this.mMasterParam.GetGrowParam(key);
        }

        public ItemParam GetItemParam(string key)
        {
            return this.mMasterParam.GetItemParam(key);
        }

        public JobParam GetJobParam(string key)
        {
            return this.mMasterParam.GetJobParam(key);
        }

        public JobSetParam GetJobSetParam(string key)
        {
            return this.mMasterParam.GetJobSetParam(key);
        }

        public unsafe void GetLearningAbilitySource(UnitData unit, string abilityID, out JobParam job, out int rank)
        {
            int num;
            num = 0;
            goto Label_003A;
        Label_0007:
            *((int*) rank) = unit.Jobs[num].Param.FindRankOfAbility(abilityID);
            if (*(((int*) rank)) == -1)
            {
                goto Label_0036;
            }
            *(job) = unit.Jobs[num].Param;
            return;
        Label_0036:
            num += 1;
        Label_003A:
            if (num < ((int) unit.Jobs.Length))
            {
                goto Label_0007;
            }
            *(job) = null;
            *((int*) rank) = -1;
            return;
        }

        public SRPG.MapEffectParam GetMapEffectParam(string iname)
        {
            SRPG.MapEffectParam param;
            <GetMapEffectParam>c__AnonStorey29F storeyf;
            storeyf = new <GetMapEffectParam>c__AnonStorey29F();
            storeyf.iname = iname;
            if (string.IsNullOrEmpty(storeyf.iname) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            if (this.mMapEffectParam != null)
            {
                goto Label_0041;
            }
            DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetMapEffectParam no data!</color>", new object[0]));
            return null;
        Label_0041:
            param = this.mMapEffectParam.Find(new Predicate<SRPG.MapEffectParam>(storeyf.<>m__208));
            if (param != null)
            {
                goto Label_0074;
            }
            DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetMapEffectParam data not found! iname={0}</color>", storeyf.iname));
        Label_0074:
            return param;
        }

        public int GetMaxBattlePoint(int schedule_id)
        {
            VersusRankParam param;
            <GetMaxBattlePoint>c__AnonStorey289 storey;
            storey = new <GetMaxBattlePoint>c__AnonStorey289();
            storey.schedule_id = schedule_id;
            param = this.mVersusRank.Find(new Predicate<VersusRankParam>(storey.<>m__1E7));
            if (param != null)
            {
                goto Label_002D;
            }
            return 0;
        Label_002D:
            return param.Limit;
        }

        public List<MultiTowerFloorParam> GetMTAllFloorParam(string type)
        {
            List<MultiTowerFloorParam> list;
            int num;
            list = new List<MultiTowerFloorParam>();
            num = 0;
            goto Label_003F;
        Label_000D:
            if ((this.mMultiTowerFloor[num].tower_id == type) == null)
            {
                goto Label_003B;
            }
            list.Add(this.mMultiTowerFloor[num]);
        Label_003B:
            num += 1;
        Label_003F:
            if (num < this.mMultiTowerFloor.Count)
            {
                goto Label_000D;
            }
            return list;
        }

        public int GetMTChallengeFloor()
        {
            List<MultiTowerFloorParam> list;
            int num;
            if (this.mMultiTowerRound != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            list = this.GetMTAllFloorParam(GlobalVars.SelectedMultiTowerID);
            num = this.mMultiTowerRound.Now + 1;
            if (list == null)
            {
                goto Label_0054;
            }
            if (list.Count <= 0)
            {
                goto Label_0054;
            }
            num = Mathf.Clamp(num, 1, list[list.Count - 1].floor);
        Label_0054:
            return num;
        }

        public int GetMTClearedMaxFloor()
        {
            if (this.mMultiTowerRound != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mMultiTowerRound.Now;
        }

        public unsafe MultiTowerFloorParam GetMTFloorParam(string iname)
        {
            int num;
            int num2;
            string str;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            num = iname.LastIndexOf(0x5f);
            num2 = -1;
            if (int.TryParse(iname.Substring(num + 1), &num2) == null)
            {
                goto Label_003F;
            }
            str = iname.Substring(0, num);
            return this.GetMTFloorParam(str, num2);
        Label_003F:
            return null;
        }

        public MultiTowerFloorParam GetMTFloorParam(string type, int floor)
        {
            MultiTowerFloorParam param;
            <GetMTFloorParam>c__AnonStorey29D storeyd;
            storeyd = new <GetMTFloorParam>c__AnonStorey29D();
            storeyd.floor = floor;
            storeyd.type = type;
            return this.mMultiTowerFloor.Find(new Predicate<MultiTowerFloorParam>(storeyd.<>m__206));
        }

        public List<MultiTowerRewardItem> GetMTFloorReward(string iname, int round)
        {
            MultiTowerRewardParam param;
            <GetMTFloorReward>c__AnonStorey29E storeye;
            storeye = new <GetMTFloorReward>c__AnonStorey29E();
            storeye.iname = iname;
            param = this.mMultiTowerRewards.Find(new Predicate<MultiTowerRewardParam>(storeye.<>m__207));
            if (param == null)
            {
                goto Label_0033;
            }
            return param.GetReward(round);
        Label_0033:
            return null;
        }

        public int GetMTRound(int floor)
        {
            int num;
            num = floor - 1;
            if (this.mMultiTowerRound == null)
            {
                goto Label_0035;
            }
            if (this.mMultiTowerRound.Round == null)
            {
                goto Label_0035;
            }
            if (this.mMultiTowerRound.Round.Count > num)
            {
                goto Label_0037;
            }
        Label_0035:
            return 1;
        Label_0037:
            return (this.mMultiTowerRound.Round[num] + 1);
        }

        public List<string> GetMultiAreaQuestList()
        {
            List<string> list;
            int num;
            QuestParam param;
            list = new List<string>();
            num = 0;
            goto Label_0046;
        Label_000D:
            param = this.mQuests[num];
            if (param == null)
            {
                goto Label_0042;
            }
            if (param.gps_enable == null)
            {
                goto Label_0042;
            }
            if (param.IsMultiAreaQuest == null)
            {
                goto Label_0042;
            }
            list.Add(param.iname);
        Label_0042:
            num += 1;
        Label_0046:
            if (num < this.mQuests.Count)
            {
                goto Label_000D;
            }
            return list;
        }

        public string GetNextTutorialStep()
        {
            GameSettings settings;
            settings = GameSettings.Instance;
            if (this.mTutorialStep < ((int) settings.Tutorial_Steps.Length))
            {
                goto Label_001B;
            }
            return null;
        Label_001B:
            return settings.Tutorial_Steps[this.mTutorialStep];
        }

        public int GetNextVersusRankClass(int schedule_id, RankMatchClass current_class, int point)
        {
            VersusRankClassParam param;
            <GetNextVersusRankClass>c__AnonStorey287 storey;
            storey = new <GetNextVersusRankClass>c__AnonStorey287();
            storey.schedule_id = schedule_id;
            storey.current_class = current_class;
            param = this.mVersusRankClass.Find(new Predicate<VersusRankClassParam>(storey.<>m__1E5));
            if (param != null)
            {
                goto Label_0034;
            }
            return 0;
        Label_0034:
            return (param.UpPoint - point);
        }

        public int GetQuestTypeCount(QuestTypes type)
        {
            int num;
            int num2;
            num = 0;
            num2 = 0;
            goto Label_0028;
        Label_0009:
            if (this.mQuests[num2].type != type)
            {
                goto Label_0024;
            }
            num += 1;
        Label_0024:
            num2 += 1;
        Label_0028:
            if (num2 < this.mQuests.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public List<QuestParam> GetQuestTypeList(QuestTypes type)
        {
            List<QuestParam> list;
            int num;
            list = new List<QuestParam>();
            num = 0;
            goto Label_003A;
        Label_000D:
            if (this.mQuests[num].type != type)
            {
                goto Label_0036;
            }
            list.Add(this.mQuests[num]);
        Label_0036:
            num += 1;
        Label_003A:
            if (num < this.mQuests.Count)
            {
                goto Label_000D;
            }
            return list;
        }

        public unsafe void GetRankMatchCondition(out int lrange, out int frange)
        {
            int num;
            VersusMatchCondParam[] paramArray;
            *((int*) lrange) = -1;
            *((int*) frange) = -1;
            num = this.Player.VersusTowerFloor;
            paramArray = this.mMasterParam.GetVersusMatchingCondition();
            if (paramArray == null)
            {
                goto Label_0052;
            }
            if (num < 0)
            {
                goto Label_0052;
            }
            if (num >= ((int) paramArray.Length))
            {
                goto Label_0052;
            }
            *((int*) lrange) = paramArray[num].LvRange;
            *((int*) frange) = paramArray[num].FloorRange;
        Label_0052:
            return;
        }

        public RarityParam GetRarityParam(int rarity)
        {
            return this.mMasterParam.GetRarityParam(rarity);
        }

        public RecipeParam GetRecipeParam(string key)
        {
            return this.mMasterParam.GetRecipeParam(key);
        }

        public unsafe int GetReleaseStoryPart(string quest_id)
        {
            SectionParam param;
            List<SectionParam>.Enumerator enumerator;
            int num;
            if (this.mWorlds.Count != null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            enumerator = this.mWorlds.GetEnumerator();
        Label_001E:
            try
            {
                goto Label_0048;
            Label_0023:
                param = &enumerator.Current;
                if ((quest_id == param.releaseKeyQuest) == null)
                {
                    goto Label_0048;
                }
                num = param.storyPart;
                goto Label_0067;
            Label_0048:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
                goto Label_0065;
            }
            finally
            {
            Label_0059:
                ((List<SectionParam>.Enumerator) enumerator).Dispose();
            }
        Label_0065:
            return 0;
        Label_0067:
            return num;
        }

        public string GetReleaseStoryPartWorldName(int story_part)
        {
            List<QuestParam> list;
            <GetReleaseStoryPartWorldName>c__AnonStorey2A2 storeya;
            storeya = new <GetReleaseStoryPartWorldName>c__AnonStorey2A2();
            storeya.story_part = story_part;
            storeya.section_param = this.mWorlds.Find(new Predicate<SectionParam>(storeya.<>m__20C));
            if (storeya.section_param != null)
            {
                goto Label_0037;
            }
            return null;
        Label_0037:
            list = new List<QuestParam>(this.Quests);
            storeya.check_param = list.Find(new Predicate<QuestParam>(storeya.<>m__20D));
            if (storeya.check_param != null)
            {
                goto Label_0068;
            }
            return null;
        Label_0068:
            storeya.section_param = this.mWorlds.Find(new Predicate<SectionParam>(storeya.<>m__20E));
            if (storeya.section_param != null)
            {
                goto Label_0092;
            }
            return null;
        Label_0092:
            return storeya.section_param.name;
        }

        public SkillParam GetSkillParam(string key)
        {
            return this.mMasterParam.GetSkillParam(key);
        }

        public unsafe int GetStoryPartNum()
        {
            int num;
            SectionParam param;
            List<SectionParam>.Enumerator enumerator;
            num = 1;
            if (this.mWorlds.Count != null)
            {
                goto Label_0014;
            }
            return num;
        Label_0014:
            enumerator = this.mWorlds.GetEnumerator();
        Label_0020:
            try
            {
                goto Label_0040;
            Label_0025:
                param = &enumerator.Current;
                if (num >= param.storyPart)
                {
                    goto Label_0040;
                }
                num = param.storyPart;
            Label_0040:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0025;
                }
                goto Label_005D;
            }
            finally
            {
            Label_0051:
                ((List<SectionParam>.Enumerator) enumerator).Dispose();
            }
        Label_005D:
            return num;
        }

        public int GetStoryPartNumPresentTime()
        {
            List<SectionParam> list;
            int num;
            int num2;
            int num3;
            if (<>f__am$cache9C != null)
            {
                goto Label_001E;
            }
            <>f__am$cache9C = new Predicate<SectionParam>(GameManager.<GetStoryPartNumPresentTime>m__20B);
        Label_001E:
            list = this.mWorlds.FindAll(<>f__am$cache9C);
            if (list == null)
            {
                goto Label_003A;
            }
            if (list.Count != null)
            {
                goto Label_003C;
            }
        Label_003A:
            return 1;
        Label_003C:
            num = 1;
            num2 = 0;
            goto Label_00C7;
        Label_0045:
            if (this.Quests[num2].IsStory == null)
            {
                goto Label_00C3;
            }
            if (this.Quests[num2].state != 2)
            {
                goto Label_00C3;
            }
            num3 = 0;
            goto Label_00B7;
        Label_0071:
            if ((list[num3].releaseKeyQuest == this.Quests[num2].iname) == null)
            {
                goto Label_00B3;
            }
            if (num >= list[num3].storyPart)
            {
                goto Label_00B3;
            }
            num = list[num3].storyPart;
        Label_00B3:
            num3 += 1;
        Label_00B7:
            if (num3 < list.Count)
            {
                goto Label_0071;
            }
        Label_00C3:
            num2 += 1;
        Label_00C7:
            if (num2 < ((int) this.Quests.Length))
            {
                goto Label_0045;
            }
            return num;
        }

        public unsafe void GetTowerMatchItems(int floor, List<string> Items, List<int> Nums, bool bWin)
        {
            VersusTowerParam[] paramArray;
            int num;
            int num2;
            paramArray = this.GetVersusTowerParam();
            if (paramArray == null)
            {
                goto Label_00F1;
            }
            if (floor < 0)
            {
                goto Label_00F1;
            }
            if (floor >= ((int) paramArray.Length))
            {
                goto Label_00F1;
            }
            if (bWin == null)
            {
                goto Label_008D;
            }
            if (paramArray[floor].WinIteminame == null)
            {
                goto Label_008D;
            }
            num = 0;
            goto Label_0078;
        Label_0038:
            Items.Add(*(&(paramArray[floor].WinIteminame[num])));
            Nums.Add(*(&(paramArray[floor].WinItemNum[num])));
            num += 1;
        Label_0078:
            if (num < ((int) paramArray[floor].WinIteminame.Length))
            {
                goto Label_0038;
            }
            goto Label_00F1;
        Label_008D:
            if (paramArray[floor].JoinIteminame == null)
            {
                goto Label_00F1;
            }
            num2 = 0;
            goto Label_00E1;
        Label_00A1:
            Items.Add(*(&(paramArray[floor].JoinIteminame[num2])));
            Nums.Add(*(&(paramArray[floor].JoinItemNum[num2])));
            num2 += 1;
        Label_00E1:
            if (num2 < ((int) paramArray[floor].JoinIteminame.Length))
            {
                goto Label_00A1;
            }
        Label_00F1:
            return;
        }

        public TrophyObjective[] GetTrophiesOfType(TrophyConditionTypes type)
        {
            return this.mMasterParam.GetTrophiesOfType(type);
        }

        public UnitParam GetUnitParam(string key)
        {
            return this.mMasterParam.GetUnitParam(key);
        }

        public VersusCoinParam GetVersusCoinParam(string iname)
        {
            VersusCoinParam param;
            <GetVersusCoinParam>c__AnonStorey29C storeyc;
            storeyc = new <GetVersusCoinParam>c__AnonStorey29C();
            storeyc.iname = iname;
            if (this.mVersusCoinParam == null)
            {
                goto Label_0032;
            }
            return this.mVersusCoinParam.Find(new Predicate<VersusCoinParam>(storeyc.<>m__205));
        Label_0032:
            return null;
        }

        public List<VersusDraftUnitParam> GetVersusDraftUnits(long schedule_id)
        {
            <GetVersusDraftUnits>c__AnonStorey28F storeyf;
            storeyf = new <GetVersusDraftUnits>c__AnonStorey28F();
            storeyf.schedule_id = schedule_id;
            return this.mVersusDraftUnit.FindAll(new Predicate<VersusDraftUnitParam>(storeyf.<>m__1F0));
        }

        public VersusEnableTimeParam GetVersusEnableTime(long schedule_id)
        {
            <GetVersusEnableTime>c__AnonStorey284 storey;
            storey = new <GetVersusEnableTime>c__AnonStorey284();
            storey.schedule_id = schedule_id;
            return this.mVersusEnableTime.Find(new Predicate<VersusEnableTimeParam>(storey.<>m__1E2));
        }

        public VersusFriendScore[] GetVersusFriendScore(int floor)
        {
            VersusTowerParam param;
            VersusFriendScore[] scoreArray;
            List<VersusFriendScore> list;
            int num;
            param = this.GetCurrentVersusTowerParam(floor);
            scoreArray = this.VersusFriendInfo;
            list = new List<VersusFriendScore>();
            if (param == null)
            {
                goto Label_005B;
            }
            if (scoreArray == null)
            {
                goto Label_005B;
            }
            num = 0;
            goto Label_0052;
        Label_0028:
            if (string.Compare(param.FloorName, scoreArray[num].floor) != null)
            {
                goto Label_004E;
            }
            list.Add(scoreArray[num]);
        Label_004E:
            num += 1;
        Label_0052:
            if (num < ((int) scoreArray.Length))
            {
                goto Label_0028;
            }
        Label_005B:
            return list.ToArray();
        }

        public string GetVersusKey(VERSUS_TYPE type)
        {
            string str;
            VersusMatchingParam[] paramArray;
            int num;
            int num2;
            int num3;
            string str2;
            int num4;
            VERSUS_TYPE versus_type;
            str = string.Empty;
            paramArray = this.mMasterParam.GetVersusMatchingParam();
            versus_type = type;
            switch (versus_type)
            {
                case 0:
                    goto Label_006B;

                case 1:
                    goto Label_002D;

                case 2:
                    goto Label_010F;
            }
            goto Label_0153;
        Label_002D:
            num = 0;
            goto Label_005D;
        Label_0034:
            if ((paramArray[num].MatchKey == VersusMatchingParam.TOWER_KEY) == null)
            {
                goto Label_0059;
            }
            str = paramArray[num].MatchKeyHash;
            goto Label_0066;
        Label_0059:
            num += 1;
        Label_005D:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0034;
            }
        Label_0066:
            goto Label_0153;
        Label_006B:
            num2 = this.Player.CalcLevel();
            this.CreateMatchingRange();
            num3 = 0;
            goto Label_00FB;
        Label_0085:
            if (num2 >= this.mFreeMatchRange[num3].min)
            {
                goto Label_009E;
            }
            goto Label_00F5;
        Label_009E:
            if (this.mFreeMatchRange[num3].max == -1)
            {
                goto Label_00CB;
            }
            if (num2 <= this.mFreeMatchRange[num3].max)
            {
                goto Label_00CB;
            }
            goto Label_00F5;
        Label_00CB:
            str = VersusMatchingParam.CalcHash("key" + string.Format("{0:D2}", (int) num3));
            goto Label_010A;
        Label_00F5:
            num3 += 1;
        Label_00FB:
            if (num3 < ((int) this.mFreeMatchRange.Length))
            {
                goto Label_0085;
            }
        Label_010A:
            goto Label_0153;
        Label_010F:
            num4 = 0;
            goto Label_0144;
        Label_0117:
            if ((paramArray[num4].MatchKey == VersusMatchingParam.FRIEND_KEY) == null)
            {
                goto Label_013E;
            }
            str = paramArray[num4].MatchKeyHash;
            goto Label_014E;
        Label_013E:
            num4 += 1;
        Label_0144:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_0117;
            }
        Label_014E:;
        Label_0153:
            return str;
        }

        public VersusRankClassParam GetVersusRankClass(int schedule_id, RankMatchClass current_class)
        {
            VersusRankClassParam param;
            <GetVersusRankClass>c__AnonStorey288 storey;
            storey = new <GetVersusRankClass>c__AnonStorey288();
            storey.schedule_id = schedule_id;
            storey.current_class = current_class;
            param = this.mVersusRankClass.Find(new Predicate<VersusRankClassParam>(storey.<>m__1E6));
            if (param != null)
            {
                goto Label_0034;
            }
            return null;
        Label_0034:
            return param;
        }

        public List<VersusRankClassParam> GetVersusRankClassList(int schedule_id)
        {
            List<VersusRankClassParam> list;
            <GetVersusRankClassList>c__AnonStorey28A storeya;
            storeya = new <GetVersusRankClassList>c__AnonStorey28A();
            storeya.schedule_id = schedule_id;
            list = this.mVersusRankClass.FindAll(new Predicate<VersusRankClassParam>(storeya.<>m__1E8));
            if (<>f__am$cache93 != null)
            {
                goto Label_003E;
            }
            <>f__am$cache93 = new Comparison<VersusRankClassParam>(GameManager.<GetVersusRankClassList>m__1E9);
        Label_003E:
            list.Sort(<>f__am$cache93);
            return list;
        }

        public List<VersusRankReward> GetVersusRankClassRewardList(string reward_id)
        {
            VersusRankRewardParam param;
            <GetVersusRankClassRewardList>c__AnonStorey28C storeyc;
            storeyc = new <GetVersusRankClassRewardList>c__AnonStorey28C();
            storeyc.reward_id = reward_id;
            param = this.mVersusRankReward.Find(new Predicate<VersusRankRewardParam>(storeyc.<>m__1EC));
            if (param != null)
            {
                goto Label_0031;
            }
            return new List<VersusRankReward>();
        Label_0031:
            return param.RewardList;
        }

        public List<VersusEnableTimeScheduleParam> GetVersusRankMapSchedule(int schedule_id)
        {
            VersusEnableTimeParam param;
            <GetVersusRankMapSchedule>c__AnonStorey286 storey;
            storey = new <GetVersusRankMapSchedule>c__AnonStorey286();
            storey.schedule_id = schedule_id;
            param = this.mVersusEnableTime.Find(new Predicate<VersusEnableTimeParam>(storey.<>m__1E4));
            if (param != null)
            {
                goto Label_0031;
            }
            return new List<VersusEnableTimeScheduleParam>();
        Label_0031:
            return param.Schedule;
        }

        public List<VersusRankMissionParam> GetVersusRankMissionList(int schedule_id)
        {
            List<VersusRankMissionScheduleParam> list;
            <GetVersusRankMissionList>c__AnonStorey28D storeyd;
            storeyd = new <GetVersusRankMissionList>c__AnonStorey28D();
            storeyd.schedule_id = schedule_id;
            storeyd.<>f__this = this;
            storeyd.mission_list = new List<VersusRankMissionParam>();
            list = this.mVersusRankMissionSchedule.FindAll(new Predicate<VersusRankMissionScheduleParam>(storeyd.<>m__1ED));
            if (list != null)
            {
                goto Label_0044;
            }
            return storeyd.mission_list;
        Label_0044:
            list.ForEach(new Action<VersusRankMissionScheduleParam>(storeyd.<>m__1EE));
            if (<>f__am$cache95 != null)
            {
                goto Label_0074;
            }
            <>f__am$cache95 = new Comparison<VersusRankMissionParam>(GameManager.<GetVersusRankMissionList>m__1EF);
        Label_0074:
            storeyd.mission_list.Sort(<>f__am$cache95);
            return storeyd.mission_list;
        }

        public VersusRankParam GetVersusRankParam(int schedule_id)
        {
            <GetVersusRankParam>c__AnonStorey285 storey;
            storey = new <GetVersusRankParam>c__AnonStorey285();
            storey.schedule_id = schedule_id;
            return this.mVersusRank.Find(new Predicate<VersusRankParam>(storey.<>m__1E3));
        }

        public List<VersusRankRankingRewardParam> GetVersusRankRankingRewardList(int schedule_id)
        {
            List<VersusRankRankingRewardParam> list;
            <GetVersusRankRankingRewardList>c__AnonStorey28B storeyb;
            storeyb = new <GetVersusRankRankingRewardList>c__AnonStorey28B();
            storeyb.schedule_id = schedule_id;
            list = this.mVersusRankRankingReward.FindAll(new Predicate<VersusRankRankingRewardParam>(storeyb.<>m__1EA));
            if (<>f__am$cache94 != null)
            {
                goto Label_003E;
            }
            <>f__am$cache94 = new Comparison<VersusRankRankingRewardParam>(GameManager.<GetVersusRankRankingRewardList>m__1EB);
        Label_003E:
            list.Sort(<>f__am$cache94);
            return list;
        }

        public unsafe void GetVersusTopFloorItems(int floor, List<string> Items, List<int> Nums)
        {
            VersusTowerParam[] paramArray;
            int num;
            paramArray = this.GetVersusTowerParam();
            if (paramArray == null)
            {
                goto Label_0081;
            }
            if (floor < 0)
            {
                goto Label_0081;
            }
            if (floor >= ((int) paramArray.Length))
            {
                goto Label_0081;
            }
            if (paramArray[floor].SpIteminame == null)
            {
                goto Label_0081;
            }
            num = 0;
            goto Label_0071;
        Label_0031:
            Items.Add(*(&(paramArray[floor].SpIteminame[num])));
            Nums.Add(*(&(paramArray[floor].SpItemnum[num])));
            num += 1;
        Label_0071:
            if (num < ((int) paramArray[floor].SpIteminame.Length))
            {
                goto Label_0031;
            }
        Label_0081:
            return;
        }

        public VersusTowerParam[] GetVersusTowerParam()
        {
            return this.mVersusTowerFloor.ToArray();
        }

        public VersusFirstWinBonusParam GetVSFirstWinBonus(long servertime)
        {
            VersusFirstWinBonusParam param;
            DateTime time;
            int num;
            VersusFirstWinBonusParam param2;
            param = null;
            if (servertime != -1L)
            {
                goto Label_0015;
            }
            time = TimeManager.ServerTime;
            goto Label_001C;
        Label_0015:
            time = TimeManager.FromUnixTime(servertime);
        Label_001C:
            num = 0;
            goto Label_005D;
        Label_0023:
            param2 = this.mVersusFirstWinBonus[num];
            if ((time >= param2.begin_at) == null)
            {
                goto Label_0059;
            }
            if ((time <= param2.end_at) == null)
            {
                goto Label_0059;
            }
            param = param2;
            goto Label_006E;
        Label_0059:
            num += 1;
        Label_005D:
            if (num < this.mVersusFirstWinBonus.Count)
            {
                goto Label_0023;
            }
        Label_006E:
            return param;
        }

        public int GetVSGetCoinRate(long servertime)
        {
            int num;
            DateTime time;
            int num2;
            VersusCoinCampParam param;
            num = 1;
            if (servertime != -1L)
            {
                goto Label_0015;
            }
            time = TimeManager.ServerTime;
            goto Label_001C;
        Label_0015:
            time = TimeManager.FromUnixTime(servertime);
        Label_001C:
            num2 = 0;
            goto Label_0062;
        Label_0023:
            param = this.mVersusCoinCamp[num2];
            if ((time >= param.begin_at) == null)
            {
                goto Label_005E;
            }
            if ((time <= param.end_at) == null)
            {
                goto Label_005E;
            }
            num = param.coinrate;
            goto Label_0073;
        Label_005E:
            num2 += 1;
        Label_0062:
            if (num2 < this.mVersusCoinCamp.Count)
            {
                goto Label_0023;
            }
        Label_0073:
            return num;
        }

        public VS_MODE GetVSMode(long servertime)
        {
            VS_MODE vs_mode;
            DateTime time;
            int num;
            VersusRuleParam param;
            vs_mode = 0;
            if (servertime != -1L)
            {
                goto Label_0015;
            }
            time = TimeManager.ServerTime;
            goto Label_001C;
        Label_0015:
            time = TimeManager.FromUnixTime(servertime);
        Label_001C:
            num = 0;
            goto Label_0062;
        Label_0023:
            param = this.mVersusRule[num];
            if ((time >= param.begin_at) == null)
            {
                goto Label_005E;
            }
            if ((time <= param.end_at) == null)
            {
                goto Label_005E;
            }
            vs_mode = param.mode;
            goto Label_0073;
        Label_005E:
            num += 1;
        Label_0062:
            if (num < this.mVersusRule.Count)
            {
                goto Label_0023;
            }
        Label_0073:
            return vs_mode;
        }

        public int GetVSStreakSchedule(STREAK_JUDGE judge, DateTime time)
        {
            int num;
            int num2;
            VersusStreakWinScheduleParam param;
            num = -1;
            num2 = 0;
            goto Label_0059;
        Label_0009:
            param = this.mVersusStreakSchedule[num2];
            if (param.judge == judge)
            {
                goto Label_0027;
            }
            goto Label_0055;
        Label_0027:
            if ((time >= param.begin_at) == null)
            {
                goto Label_0055;
            }
            if ((time <= param.end_at) == null)
            {
                goto Label_0055;
            }
            num = param.id;
            goto Label_006A;
        Label_0055:
            num2 += 1;
        Label_0059:
            if (num2 < this.mVersusStreakSchedule.Count)
            {
                goto Label_0009;
            }
        Label_006A:
            return num;
        }

        public VersusStreakWinBonusParam GetVSStreakWinBonus(int streakcnt, STREAK_JUDGE judge, long servertime)
        {
            VersusStreakWinBonusParam param;
            DateTime time;
            int num;
            int num2;
            VersusStreakWinBonusParam param2;
            param = null;
            if (servertime != -1L)
            {
                goto Label_0015;
            }
            time = TimeManager.ServerTime;
            goto Label_001C;
        Label_0015:
            time = TimeManager.FromUnixTime(servertime);
        Label_001C:
            num = this.GetVSStreakSchedule(judge, time);
            if (num == -1)
            {
                goto Label_0082;
            }
            num2 = 0;
            goto Label_0071;
        Label_0033:
            param2 = this.mVersusStreakWinBonus[num2];
            if (param2.id == num)
            {
                goto Label_0053;
            }
            goto Label_006D;
        Label_0053:
            if (param2.wincnt == streakcnt)
            {
                goto Label_0065;
            }
            goto Label_006D;
        Label_0065:
            param = param2;
            goto Label_0082;
        Label_006D:
            num2 += 1;
        Label_0071:
            if (num2 < this.mVersusStreakWinBonus.Count)
            {
                goto Label_0033;
            }
        Label_0082:
            return param;
        }

        public VsTowerMatchEndParam GetVsTowerMatchEndParam()
        {
            return this.mVersusEndParam;
        }

        public WeaponParam GetWeaponParam(string key)
        {
            return this.mMasterParam.GetWeaponParam(key);
        }

        public WeatherSetParam GetWeatherSetParam(string iname)
        {
            WeatherSetParam param;
            <GetWeatherSetParam>c__AnonStorey2A0 storeya;
            storeya = new <GetWeatherSetParam>c__AnonStorey2A0();
            storeya.iname = iname;
            if (string.IsNullOrEmpty(storeya.iname) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            if (this.mWeatherSetParam != null)
            {
                goto Label_0041;
            }
            DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetWeatherSetParam no data!</color>", new object[0]));
            return null;
        Label_0041:
            param = this.mWeatherSetParam.Find(new Predicate<WeatherSetParam>(storeya.<>m__209));
            if (param != null)
            {
                goto Label_0074;
            }
            DebugUtility.Log(string.Format("<color=yellow>QuestParam/GetWeatherSetParam data not found! iname={0}</color>", storeya.iname));
        Label_0074:
            return param;
        }

        public void InitAlterHash(string digest)
        {
            if (string.IsNullOrEmpty(digest) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.DigestHash = digest;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH) == null)
            {
                goto Label_0037;
            }
            this.PrevCheckHash = PlayerPrefsUtility.GetString(PlayerPrefsUtility.ALTER_PREV_CHECK_HASH, string.Empty);
        Label_0037:
            return;
        }

        public bool InitAuth()
        {
            if (this.mMyGuid != null)
            {
                goto Label_0016;
            }
            this.mMyGuid = new MyGUID();
        Label_0016:
            this.mMyGuid.Init(0x1dc6708);
            return 1;
        }

        protected override void Initialize()
        {
            this.OnDayChange = (DayChangeEvent) Delegate.Combine(this.OnDayChange, new DayChangeEvent(this.DayChanged));
            GlobalEvent.AddListener("TOU_AGREE", new GlobalEvent.Delegate(this.OnAgreeTermsOfUse));
            Object.DontDestroyOnLoad(this);
            return;
        }

        public void InitNotifyList(GameObject notifyListTemplate)
        {
            if ((notifyListTemplate == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((this.mNotifyList == null) == null)
            {
                goto Label_002A;
            }
            this.mNotifyList = Object.Instantiate<GameObject>(notifyListTemplate);
        Label_002A:
            return;
        }

        public bool IsAgreeTermsOfUse()
        {
            return ((this.AgreedVer == null) ? 0 : (this.AgreedVer.Length > 0));
        }

        public bool IsDeviceId()
        {
            return ((this.DeviceId == null) ? 0 : 1);
        }

        public bool IsExternalPermit()
        {
            return ((string.IsNullOrEmpty(FlowNode_Variable.Get("IS_EXTERNAL_API_PERMIT")) == null) ? 0 : 1);
        }

        private bool IsSavedUpdateTrophyStateNeedToSend(TrophyState server, int ymd, int[] count, bool isEnded)
        {
            TrophyParam param;
            int num;
            int num2;
            int num3;
            bool flag;
            int num4;
            bool flag2;
            int num5;
            int num6;
            int num7;
            if ((server != null) && (this.MasterParam != null))
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            param = this.MasterParam.GetTrophy(server.iname);
            if (param != null)
            {
                goto Label_002D;
            }
            return 0;
        Label_002D:
            num = (param.Objectives != null) ? ((int) param.Objectives.Length) : 0;
            num2 = (count != null) ? ((int) count.Length) : 0;
            num3 = (server.Count != null) ? ((int) server.Count.Length) : 0;
            if (num2 != num)
            {
                goto Label_007F;
            }
            if (num2 == num3)
            {
                goto Label_0081;
            }
        Label_007F:
            return 0;
        Label_0081:
            flag = 1;
            num4 = 0;
            goto Label_00B1;
        Label_008C:
            if (count[num4] >= param.Objectives[num4].RequiredCount)
            {
                goto Label_00AB;
            }
            flag = 0;
            goto Label_00B9;
        Label_00AB:
            num4 += 1;
        Label_00B1:
            if (num4 < num2)
            {
                goto Label_008C;
            }
        Label_00B9:
            flag2 = 0;
            if (ymd <= server.StartYMD)
            {
                goto Label_00D0;
            }
            flag2 = 1;
            goto Label_018C;
        Label_00D0:
            if (ymd >= server.StartYMD)
            {
                goto Label_00E1;
            }
            goto Label_018C;
        Label_00E1:
            if (server.IsEnded == null)
            {
                goto Label_00F8;
            }
            if (isEnded == null)
            {
                goto Label_00F8;
            }
            goto Label_018C;
        Label_00F8:
            if (server.IsEnded == null)
            {
                goto Label_0108;
            }
            goto Label_018C;
        Label_0108:
            if (isEnded == null)
            {
                goto Label_0117;
            }
            flag2 = 1;
            goto Label_018C;
        Label_0117:
            if (server.IsCompleted == null)
            {
                goto Label_012E;
            }
            if (flag == null)
            {
                goto Label_012E;
            }
            goto Label_018C;
        Label_012E:
            if (server.IsCompleted == null)
            {
                goto Label_013E;
            }
            goto Label_018C;
        Label_013E:
            if (flag == null)
            {
                goto Label_014D;
            }
            flag2 = 1;
            goto Label_018C;
        Label_014D:
            num5 = 0;
            num6 = 0;
            num7 = 0;
            goto Label_0178;
        Label_015B:
            num5 += server.Count[num7];
            num6 += count[num7];
            num7 += 1;
        Label_0178:
            if (num7 < num2)
            {
                goto Label_015B;
            }
            if (num5 >= num6)
            {
                goto Label_018C;
            }
            flag2 = 1;
        Label_018C:
            return flag2;
        }

        public bool IsTutorialFlagSet(string flagName)
        {
            long num;
            num = GameSettings.Instance.CreateTutorialFlagMask(flagName);
            return ((num == null) ? 0 : (((this.Player.TutorialFlags & num) == 0L) == 0));
        }

        public bool IsValidAreaQuest()
        {
            bool flag;
            int num;
            QuestParam param;
            flag = 0;
            num = 0;
            goto Label_0036;
        Label_0009:
            param = this.mQuests[num];
            if (param == null)
            {
                goto Label_0032;
            }
            if (param.IsGps == null)
            {
                goto Label_0032;
            }
            flag |= param.IsDateUnlock(-1L);
        Label_0032:
            num += 1;
        Label_0036:
            if (num < this.mQuests.Count)
            {
                goto Label_0009;
            }
            return flag;
        }

        public bool IsValidMultiAreaQuest()
        {
            bool flag;
            int num;
            QuestParam param;
            flag = 0;
            num = 0;
            goto Label_0036;
        Label_0009:
            param = this.mQuests[num];
            if (param == null)
            {
                goto Label_0032;
            }
            if (param.IsMultiAreaQuest == null)
            {
                goto Label_0032;
            }
            flag |= param.IsDateUnlock(-1L);
        Label_0032:
            num += 1;
        Label_0036:
            if (num < this.mQuests.Count)
            {
                goto Label_0009;
            }
            return flag;
        }

        public bool IsVersusMode()
        {
            MyPhoton photon;
            bool flag;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            flag = 1;
            if ((photon != null) == null)
            {
                goto Label_0020;
            }
            flag &= photon.CurrentState == 4;
        Label_0020:
            flag &= GlobalVars.SelectedMultiPlayRoomType == 1;
            flag &= (GlobalVars.SelectedMultiPlayVersusType == 2) == 0;
            return flag;
        }

        public void LoadUpdateTrophyList()
        {
            string str;
            byte[] buffer;
            string str2;
            JSON_TrophyResponse response;
            JSON_TrophyProgress[] progressArray;
            TrophyState[] stateArray;
            int num;
            TrophyParam param;
            TrophyState state;
            int num2;
            TrophyState state2;
            int num3;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY) != null)
            {
                goto Label_0010;
            }
            return;
        Label_0010:
            str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY, string.Empty);
            buffer = (string.IsNullOrEmpty(str) == null) ? Convert.FromBase64String(str) : null;
        Label_0038:
            try
            {
                str2 = (buffer != null) ? MyEncrypt.Decrypt(SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY, buffer, 0) : null;
                goto Label_0062;
            }
            catch (Exception)
            {
            Label_0056:
                str2 = string.Empty;
                goto Label_0062;
            }
        Label_0062:
            this.mSavedUpdateTrophyListString = str2;
        Label_0069:
            try
            {
                response = (string.IsNullOrEmpty(str2) == null) ? JSONParser.parseJSONObject<JSON_TrophyResponse>(str2) : null;
                goto Label_008E;
            }
            catch (Exception)
            {
            Label_0086:
                response = null;
                goto Label_008E;
            }
        Label_008E:
            progressArray = (response != null) ? response.trophyprogs : null;
            if (progressArray == null)
            {
                goto Label_00B3;
            }
            if (((int) progressArray.Length) > 0)
            {
                goto Label_00B4;
            }
        Label_00B3:
            return;
        Label_00B4:
            stateArray = this.Player.TrophyStates;
            num = 0;
            goto Label_0243;
        Label_00C9:
            if (progressArray[num] != null)
            {
                goto Label_00D8;
            }
            goto Label_023D;
        Label_00D8:
            param = this.MasterParam.GetTrophy(progressArray[num].iname);
            if (param != null)
            {
                goto Label_00FB;
            }
            goto Label_023D;
        Label_00FB:
            if (stateArray == null)
            {
                goto Label_0185;
            }
            state = null;
            num2 = 0;
            goto Label_013D;
        Label_010D:
            if ((stateArray[num2].iname == progressArray[num].iname) == null)
            {
                goto Label_0137;
            }
            state = stateArray[num2];
            goto Label_0148;
        Label_0137:
            num2 += 1;
        Label_013D:
            if (num2 < ((int) stateArray.Length))
            {
                goto Label_010D;
            }
        Label_0148:
            if (state == null)
            {
                goto Label_0185;
            }
            if (this.IsSavedUpdateTrophyStateNeedToSend(state, progressArray[num].ymd, progressArray[num].pts, (progressArray[num].rewarded_at == 0) == 0) != null)
            {
                goto Label_0185;
            }
            goto Label_023D;
        Label_0185:
            state2 = this.Player.GetTrophyCounter(param, 0);
            if (state2 != null)
            {
                goto Label_01A1;
            }
            goto Label_023D;
        Label_01A1:
            state2.StartYMD = progressArray[num].ymd;
            if (progressArray[num].pts == null)
            {
                goto Label_01F9;
            }
            num3 = 0;
            goto Label_01E6;
        Label_01C9:
            state2.Count[num3] = progressArray[num].pts[num3];
            num3 += 1;
        Label_01E6:
            if (num3 < ((int) progressArray[num].pts.Length))
            {
                goto Label_01C9;
            }
        Label_01F9:
            state2.IsEnded = (progressArray[num].rewarded_at == 0) == 0;
            state2.IsDirty = 1;
            DebugUtility.LogWarning("LoadSavedTrophy: " + progressArray[num].iname + " / " + param.Name);
        Label_023D:
            num += 1;
        Label_0243:
            if (num < ((int) progressArray.Length))
            {
                goto Label_00C9;
            }
            this.Player.UpdateTrophyStates();
            return;
        }

        public void NotifyAbilityRankUpCountChanged()
        {
            int num;
            num = this.Player.AbilityRankUpCountNum;
            this.mLastAbilityRankUpCount = num;
            this.OnAbilityRankUpCountChange(num);
            return;
        }

        public bool NotRequiredHeal()
        {
            if (this.Player.StaminaStockCap > this.Player.Stamina)
            {
                goto Label_004E;
            }
            if (<>f__am$cache98 != null)
            {
                goto Label_003E;
            }
            <>f__am$cache98 = new UIUtility.DialogResultEvent(GameManager.<NotRequiredHeal>m__1FB);
        Label_003E:
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.STAMINAFULL"), <>f__am$cache98, null, 0, -1);
            return 0;
        Label_004E:
            return 1;
        }

        public void OnAgreeTermsOfUse(object caller)
        {
            this.AgreedVer = Application.get_version();
            return;
        }

        private unsafe void OnApplicationFocus(bool focus)
        {
            List<LocalNotificationInfo> list;
            LocalNotificationInfo info;
            List<LocalNotificationInfo>.Enumerator enumerator;
            GameManager manager;
            TrophyParam[] paramArray;
            List<LocalNotificationInfo>.Enumerator enumerator2;
            TrophyParam param;
            string str;
            string str2;
            int num;
            int num2;
            <OnApplicationFocus>c__AnonStorey297 storey;
            MyLocalNotification.CancelStamina();
            if (FlowNode_GetCurrentScene.IsAfterLogin() == null)
            {
                goto Label_0025;
            }
            MyLocalNotification.SetStamina(this.MasterParam.LocalNotificationParam, this.Player);
        Label_0025:
            list = MyLocalNotification.LocaloNotifications;
            if (focus == null)
            {
                goto Label_00A2;
            }
            LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_MORNING);
            LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_NOON);
            LocalNotification.CancelNotificationsWithCategory(RegularLocalNotificationParam.CATEGORY_AFTERNOON);
            if (list == null)
            {
                goto Label_01B8;
            }
            if (list.Count <= 0)
            {
                goto Label_01B8;
            }
            enumerator = list.GetEnumerator();
        Label_0068:
            try
            {
                goto Label_0080;
            Label_006D:
                info = &enumerator.Current;
                LocalNotification.CancelNotificationsWithCategory(info.trophy_iname);
            Label_0080:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_006D;
                }
                goto Label_009D;
            }
            finally
            {
            Label_0091:
                ((List<LocalNotificationInfo>.Enumerator) enumerator).Dispose();
            }
        Label_009D:
            goto Label_01B8;
        Label_00A2:
            paramArray = MonoSingleton<GameManager>.Instance.Trophies;
            if (list == null)
            {
                goto Label_01B8;
            }
            if (list.Count <= 0)
            {
                goto Label_01B8;
            }
            if (paramArray == null)
            {
                goto Label_01B8;
            }
            storey = new <OnApplicationFocus>c__AnonStorey297();
            enumerator2 = list.GetEnumerator();
        Label_00D8:
            try
            {
                goto Label_019A;
            Label_00DD:
                storey.lparam = &enumerator2.Current;
                param = Array.Find<TrophyParam>(paramArray, new Predicate<TrophyParam>(storey.<>m__1FF));
                str = null;
                str2 = null;
                if (param != null)
                {
                    goto Label_0113;
                }
                goto Label_019A;
            Label_0113:
                if (storey.lparam.push_flg != null)
                {
                    goto Label_0129;
                }
                goto Label_019A;
            Label_0129:
                str = storey.lparam.push_word;
                str2 = storey.lparam.trophy_iname;
                num = ((int) param.Objectives.Length) - 1;
                goto Label_0192;
            Label_0157:
                num2 = int.Parse(param.Objectives[num].sval_base.Substring(0, 2));
                MyLocalNotification.SetRegular(new RegularLocalNotificationParam(str, str2, num2, 0, 0), this.Player);
                num -= 1;
            Label_0192:
                if (num >= 0)
                {
                    goto Label_0157;
                }
            Label_019A:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00DD;
                }
                goto Label_01B8;
            }
            finally
            {
            Label_01AB:
                ((List<LocalNotificationInfo>.Enumerator) enumerator2).Dispose();
            }
        Label_01B8:
            return;
        }

        private void OnBuyCoinConfirmCancel(GameObject go)
        {
            BuyCoinEvent event2;
            if (this.mOnBuyCoinCancel == null)
            {
                goto Label_001F;
            }
            event2 = this.mOnBuyCoinCancel;
            this.mOnBuyCoinCancel = null;
            event2();
        Label_001F:
            return;
        }

        private void OnBuyCoinEnd(GameObject go)
        {
            BuyCoinEvent event2;
            if (this.mOnBuyCoinEnd == null)
            {
                goto Label_001F;
            }
            event2 = this.mOnBuyCoinEnd;
            this.mOnBuyCoinEnd = null;
            event2();
        Label_001F:
            return;
        }

        private unsafe void OnBuyStamina(WWWResult www)
        {
            object[] objArray1;
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            int num;
            FixParam param;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002C;
            }
            if (Network.ErrCode == 0xb54)
            {
                goto Label_0020;
            }
            goto Label_0026;
        Label_0020:
            Network.RemoveAPI();
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
            FlowNode_Network.Retry();
            return;
        Label_004A:
            num = this.Player.GetStaminaRecoveryCost(0);
            if (this.Player.Deserialize(response.body.player, 6) != null)
            {
                goto Label_0079;
            }
            FlowNode_Network.Retry();
            return;
        Label_0079:
            Network.RemoveAPI();
            MyMetaps.TrackSpendCoin("BuyStamina", num);
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            objArray1 = new object[] { (OInt) param.StaminaAdd };
            if (<>f__am$cache99 != null)
            {
                goto Label_00D1;
            }
            <>f__am$cache99 = new UIUtility.DialogResultEvent(GameManager.<OnBuyStamina>m__1FC);
        Label_00D1:
            UIUtility.SystemMessage(null, LocalizedText.Get("sys.STAMINARECOVERED", objArray1), <>f__am$cache99, null, 0, -1);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), this);
            return;
        }

        private void OnTutorialFlagUpdate(WWWResult www)
        {
            if (Network.IsError == null)
            {
                goto Label_0010;
            }
            FlowNode_Network.Retry();
            return;
        Label_0010:
            Network.RemoveAPI();
            return;
        }

        public bool PartialDownloadTutorialAssets()
        {
            List<AssetList.Item> list;
            int num;
            int num2;
            int num3;
            AssetDownloader.DownloadState state;
            if (AssetDownloader.isDone == null)
            {
                goto Label_0015;
            }
            if (this.mWaitDownloadThread == null)
            {
                goto Label_0017;
            }
        Label_0015:
            return 0;
        Label_0017:
            if (this.mTutorialDLAssets.Count > 0)
            {
                goto Label_002A;
            }
            return 0;
        Label_002A:
            list = new List<AssetList.Item>();
            num = GameSettings.Instance.Network_BGDLChunkSize;
            num2 = 0;
            num3 = 0;
            goto Label_00C0;
        Label_0044:
            if (AssetManager.IsAssetInCache(this.mTutorialDLAssets[num3].IDStr) == null)
            {
                goto Label_0074;
            }
            this.mTutorialDLAssets.RemoveAt(num3--);
            goto Label_00BC;
        Label_0074:
            list.Add(this.mTutorialDLAssets[num3]);
            AssetDownloader.Add(this.mTutorialDLAssets[num3].IDStr);
            num2 += this.mTutorialDLAssets[num3].Size;
            if (num2 < num)
            {
                goto Label_00BC;
            }
            goto Label_00D1;
        Label_00BC:
            num3 += 1;
        Label_00C0:
            if (num3 < this.mTutorialDLAssets.Count)
            {
                goto Label_0044;
            }
        Label_00D1:
            if (num2 > 0)
            {
                goto Label_00DA;
            }
            return 0;
        Label_00DA:
            state = AssetDownloader.StartDownload(0, 0, 0);
            if (state != null)
            {
                goto Label_00ED;
            }
            return 0;
        Label_00ED:
            this.mWaitDownloadThread = base.StartCoroutine(this.WaitDownloadAsync(list, state));
            return 1;
        }

        public void PostLogin()
        {
            this.mHasLoggedIn = 1;
            this.mTutorialStep = 0;
            this.mLastStamina = this.Player.Stamina;
            this.mLastGold = (long) this.Player.Gold;
            this.mLastAbilityRankUpCount = this.Player.AbilityRankUpCountNum;
            this.Player.ClearItemFlags(3);
            GlobalVars.SelectedUnitUniqueID.Set(0L);
            GlobalVars.SelectedJobUniqueID.Set(0L);
            GlobalVars.SelectedEquipmentSlot.Set(-1);
            GlobalVars.ResetVarsWithAttribute(typeof(GlobalVars.ResetOnLogin));
            this.mLastUpdateTime = TimeManager.ServerTime;
            this.Player.OnLoginCount();
            HomeWindow.EnterHomeCount = 0;
            MySmartBeat.SetupUserInfo();
            return;
        }

        public bool PrepareSceneChange()
        {
            Delegate[] delegateArray;
            int num;
            SceneChangeEvent event2;
            delegateArray = this.OnSceneChange.GetInvocationList();
            num = 0;
            goto Label_002D;
        Label_0013:
            event2 = delegateArray[num] as SceneChangeEvent;
            if (event2() != null)
            {
                goto Label_0029;
            }
            return 0;
        Label_0029:
            num += 1;
        Label_002D:
            if (num < ((int) delegateArray.Length))
            {
                goto Label_0013;
            }
            return 1;
        }

        private void RefreshTutorialDLAssets()
        {
            GameManager manager;
            AssetList.Item[] itemArray;
            int num;
            if (this.mScannedTutorialAssets == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mScannedTutorialAssets = 1;
            manager = MonoSingleton<GameManager>.Instance;
            itemArray = AssetManager.AssetList.Assets;
            num = 0;
            goto Label_00A3;
        Label_002B:
            if ((itemArray[num].Flags & 0x10) == null)
            {
                goto Label_009F;
            }
            if ((itemArray[num].Flags & 0x80) == null)
            {
                goto Label_007F;
            }
            if (GameUtility.IsDebugBuild == null)
            {
                goto Label_0067;
            }
            if (GlobalVars.DebugIsPlayTutorial != null)
            {
                goto Label_0067;
            }
            goto Label_009F;
        Label_0067:
            if ((manager.Player.TutorialFlags & 1L) == null)
            {
                goto Label_007F;
            }
            goto Label_009F;
        Label_007F:
            if (AssetManager.IsAssetInCache(itemArray[num].IDStr) != null)
            {
                goto Label_009F;
            }
            this.mTutorialDLAssets.Add(itemArray[num]);
        Label_009F:
            num += 1;
        Label_00A3:
            if (num < ((int) itemArray.Length))
            {
                goto Label_002B;
            }
            if (<>f__am$cache9A != null)
            {
                goto Label_00CA;
            }
            <>f__am$cache9A = new Comparison<AssetList.Item>(GameManager.<RefreshTutorialDLAssets>m__1FE);
        Label_00CA:
            this.mTutorialDLAssets.Sort(<>f__am$cache9A);
            return;
        }

        public void RegisterImportantJob(Coroutine co)
        {
            this.mImportantJobs.Add(co);
            if (this.mImportantJobCoroutine != null)
            {
                goto Label_0029;
            }
            this.mImportantJobCoroutine = base.StartCoroutine(this.AsyncWaitForImportantJobs());
        Label_0029:
            return;
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
            HomeWindow.SetRestorePoint(0);
            if ((MonoSingleton<MySound>.GetInstanceDirect() != null) == null)
            {
                goto Label_0059;
            }
            MonoSingleton<MySound>.Instance.StopBGM();
        Label_0059:
            if ((this.mNotifyList != null) == null)
            {
                goto Label_007C;
            }
            GameUtility.DestroyGameObject(this.mNotifyList);
            this.mNotifyList = null;
        Label_007C:
            this.mScannedTutorialAssets = 0;
            return;
        }

        public bool ReloadMasterData(string masterParam, string questParam)
        {
            MD5 md;
            byte[] buffer;
            byte[] buffer2;
            StringBuilder builder;
            int num;
            string str;
            JSON_MasterParam param;
            Json_QuestList list;
            Exception exception;
            bool flag;
            if (this.IsRelogin == null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            try
            {
                if (masterParam != null)
                {
                    goto Label_001F;
                }
                masterParam = AssetManager.LoadTextData("Data/MasterParam");
            Label_001F:
                if (string.IsNullOrEmpty(this.DigestHash) != null)
                {
                    goto Label_00D0;
                }
                md = new MD5CryptoServiceProvider();
                buffer = Encoding.UTF8.GetBytes(masterParam);
                buffer2 = md.ComputeHash(buffer);
                builder = new StringBuilder();
                num = 0;
                goto Label_0072;
            Label_0057:
                builder.AppendFormat("{0:x2}", (byte) buffer2[num]);
                num += 1;
            Label_0072:
                if (num < ((int) buffer2.Length))
                {
                    goto Label_0057;
                }
                str = builder.ToString();
                if (string.IsNullOrEmpty(this.PrevCheckHash) != null)
                {
                    goto Label_00B6;
                }
                if (string.IsNullOrEmpty(this.PrevCheckHash) != null)
                {
                    goto Label_00D0;
                }
                if ((this.PrevCheckHash != str) == null)
                {
                    goto Label_00D0;
                }
            Label_00B6:
                if ((str != this.DigestHash) == null)
                {
                    goto Label_00D0;
                }
                this.AlterCheckHash = str;
            Label_00D0:
                param = JsonUtility.FromJson<JSON_MasterParam>(masterParam);
                if (param != null)
                {
                    goto Label_00E5;
                }
                throw new InvalidJSONException();
            Label_00E5:
                this.Deserialize2(param);
                param = null;
                if (questParam != null)
                {
                    goto Label_0103;
                }
                questParam = AssetManager.LoadTextData("Data/QuestParam");
            Label_0103:
                list = JsonUtility.FromJson<Json_QuestList>(questParam);
                if (list != null)
                {
                    goto Label_0118;
                }
                throw new InvalidJSONException();
            Label_0118:
                this.Deserialize(list);
                list = null;
                goto Label_0144;
            }
            catch (Exception exception1)
            {
            Label_0128:
                exception = exception1;
                this.mReloadMasterDataError = exception.ToString();
                flag = 0;
                goto Label_0146;
            }
        Label_0144:
            return 1;
        Label_0146:
            return flag;
        }

        private bool RequestTexture(TextureRequest texReq)
        {
            texReq.Request = AssetManager.LoadAsync<Texture2D>(texReq.Path);
            if (texReq.Request.isDone == null)
            {
                goto Label_003E;
            }
            texReq.Target.set_texture(texReq.Request.asset as Texture2D);
            return 1;
        Label_003E:
            return 0;
        }

        public void RequestUpdateBadges(BadgeTypes type)
        {
            int num;
            TrophyState state;
            if ((type & 0x10) == null)
            {
                goto Label_0084;
            }
            this.BadgeFlags &= -17;
            this.Player.UpdateTrophyStates();
            if (this.Player.TrophyStates == null)
            {
                goto Label_0084;
            }
            num = 0;
            goto Label_0071;
        Label_003A:
            state = this.Player.TrophyStates[num];
            if (state.Param.IsShowBadge(state) == null)
            {
                goto Label_006D;
            }
            this.BadgeFlags |= 0x10;
            goto Label_0084;
        Label_006D:
            num += 1;
        Label_0071:
            if (num < ((int) this.Player.TrophyStates.Length))
            {
                goto Label_003A;
            }
        Label_0084:
            if ((type & 0x100) == null)
            {
                goto Label_00D4;
            }
            this.BadgeFlags &= -257;
            if (this.Player.UnreadMail != null)
            {
                goto Label_00C2;
            }
            if (this.Player.UnreadMailPeriod == null)
            {
                goto Label_00D4;
            }
        Label_00C2:
            this.BadgeFlags |= 0x100;
        Label_00D4:
            if ((type & 0x80) == null)
            {
                goto Label_0115;
            }
            this.BadgeFlags &= -129;
            if (0 >= this.Player.FollowerNum)
            {
                goto Label_0115;
            }
            this.BadgeFlags |= 0x80;
        Label_0115:
            if ((type & 1) == null)
            {
                goto Label_012A;
            }
            base.StartCoroutine(this.UpdateUnitsBadges());
        Label_012A:
            if ((type & 2) == null)
            {
                goto Label_013F;
            }
            base.StartCoroutine(this.UpdateUnitUnlockBadges());
        Label_013F:
            if ((type & 4) == null)
            {
                goto Label_0174;
            }
            this.BadgeFlags &= -5;
            if (this.Player.CheckFreeGachaGold() == null)
            {
                goto Label_0174;
            }
            this.BadgeFlags |= 4;
        Label_0174:
            if ((type & 8) == null)
            {
                goto Label_01A9;
            }
            this.BadgeFlags &= -9;
            if (this.Player.CheckFreeGachaCoin() == null)
            {
                goto Label_01A9;
            }
            this.BadgeFlags |= 8;
        Label_01A9:
            if ((type & 0x20) == null)
            {
                goto Label_01FD;
            }
            this.BadgeFlags &= -33;
            if (this.Player.CheckUnlock(0x10) == null)
            {
                goto Label_01FD;
            }
            if (this.Player.ChallengeArenaNum != this.Player.ChallengeArenaMax)
            {
                goto Label_01FD;
            }
            this.BadgeFlags |= 0x20;
        Label_01FD:
            if ((type & 0x40) == null)
            {
                goto Label_0249;
            }
            this.BadgeFlags &= -65;
            if (this.Player.CheckUnlock(0x100) == null)
            {
                goto Label_0249;
            }
            if (this.Player.ChallengeMultiNum != null)
            {
                goto Label_0249;
            }
            this.BadgeFlags |= 0x40;
        Label_0249:
            return;
        }

        public void ResetAuth()
        {
            this.mMyGuid.ResetCache();
            Network.SessionID = string.Empty;
            return;
        }

        public void ResetData()
        {
            this.Release();
            return;
        }

        public void ResetGpsQuests()
        {
            int num;
            num = 0;
            goto Label_001D;
        Label_0007:
            this.mQuests[num].gps_enable = 0;
            num += 1;
        Label_001D:
            if (num < this.mQuests.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void ResetJigenQuests()
        {
            int num;
            num = 0;
            goto Label_0071;
        Label_0007:
            this.mQuests[num].start = 0L;
            this.mQuests[num].end = 0L;
            if (this.mQuests[num].IsKeyQuest == null)
            {
                goto Label_005B;
            }
            this.mQuests[num].Chapter.key_end = 0L;
        Label_005B:
            this.mQuests[num].gps_enable = 0;
            num += 1;
        Label_0071:
            if (num < this.mQuests.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void SaveAuth(string device_id)
        {
            this.mMyGuid.SaveAuth(0x1dc6708, this.SecretKey, device_id, this.UdId);
            return;
        }

        public void SaveAuthWithKey(string device_id, string secretKey)
        {
            this.mMyGuid.SetSecretKey(secretKey);
            this.mMyGuid.SaveAuth(0x1dc6708, this.SecretKey, device_id, this.UdId);
            return;
        }

        public unsafe void SaveUpdateTrophyList(List<TrophyState> updateList)
        {
            string str;
            bool flag;
            TrophyState state;
            List<TrophyState>.Enumerator enumerator;
            int num;
            byte[] buffer;
            string str2;
            str = null;
            if (updateList == null)
            {
                goto Label_0131;
            }
            if (updateList.Count > 0)
            {
                goto Label_0019;
            }
            goto Label_0131;
        Label_0019:
            str = "{\"trophyprogs\":[";
            flag = 1;
            enumerator = updateList.GetEnumerator();
        Label_0028:
            try
            {
                goto Label_0108;
            Label_002D:
                state = &enumerator.Current;
                if (state != null)
                {
                    goto Label_0040;
                }
                goto Label_0108;
            Label_0040:
                if (flag == null)
                {
                    goto Label_004D;
                }
                flag = 0;
                goto Label_0059;
            Label_004D:
                str = str + ",";
            Label_0059:
                str = str + "{";
                str = str + "\"iname\":\"" + state.iname + "\"";
                if (state.Count == null)
                {
                    goto Label_00E5;
                }
                str = str + ",\"pts\":[";
                num = 0;
                goto Label_00CA;
            Label_009B:
                if (num <= 0)
                {
                    goto Label_00AF;
                }
                str = str + ",";
            Label_00AF:
                str = str + ((int) state.Count[num]);
                num += 1;
            Label_00CA:
                if (num < ((int) state.Count.Length))
                {
                    goto Label_009B;
                }
                str = str + "]";
            Label_00E5:
                str = str + ",\"ymd\":" + ((int) state.StartYMD);
                str = str + "}";
            Label_0108:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002D;
                }
                goto Label_0125;
            }
            finally
            {
            Label_0119:
                ((List<TrophyState>.Enumerator) enumerator).Dispose();
            }
        Label_0125:
            str = str + "]}";
        Label_0131:
            if ((this.mSavedUpdateTrophyListString == str) == null)
            {
                goto Label_0143;
            }
            return;
        Label_0143:
            buffer = (string.IsNullOrEmpty(str) == null) ? MyEncrypt.Encrypt(SAVE_UPDATE_TROPHY_LIST_ENCODE_KEY, str, 0) : null;
            str2 = (buffer != null) ? Convert.ToBase64String(buffer) : string.Empty;
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.SAVE_UPDATE_TROPHY_LIST_KEY, str2, 1);
            this.mSavedUpdateTrophyListString = str;
            DebugUtility.LogWarning("SaveTrophy:" + ((str != null) ? str : "null"));
            return;
        }

        public STREAK_JUDGE SearchVersusJudgeType(int id, long servertime)
        {
            STREAK_JUDGE streak_judge;
            DateTime time;
            int num;
            VersusStreakWinScheduleParam param;
            streak_judge = -1;
            if (servertime != -1L)
            {
                goto Label_0015;
            }
            time = TimeManager.ServerTime;
            goto Label_001C;
        Label_0015:
            time = TimeManager.FromUnixTime(servertime);
        Label_001C:
            num = 0;
            goto Label_0073;
        Label_0023:
            param = this.mVersusStreakSchedule[num];
            if (param.id == id)
            {
                goto Label_0041;
            }
            goto Label_006F;
        Label_0041:
            if ((time >= param.begin_at) == null)
            {
                goto Label_006F;
            }
            if ((time <= param.end_at) == null)
            {
                goto Label_006F;
            }
            streak_judge = param.judge;
            goto Label_0084;
        Label_006F:
            num += 1;
        Label_0073:
            if (num < this.mVersusStreakSchedule.Count)
            {
                goto Label_0023;
            }
        Label_0084:
            return streak_judge;
        }

        public unsafe void ServerSyncTrophyExecEnd(WWWResult www)
        {
            List<TrophyState> list;
            TrophyState state;
            List<TrophyState>.Enumerator enumerator;
            TrophyState state2;
            List<TrophyState>.Enumerator enumerator2;
            TrophyState state3;
            List<TrophyState>.Enumerator enumerator3;
            WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> response;
            Exception exception;
            if (this.is_start_server_sync_trophy_exec != null)
            {
                goto Label_0016;
            }
            DebugUtility.Log("ServerSyncTrophyExecBegin が呼ばれていません。");
            return;
        Label_0016:
            this.is_start_server_sync_trophy_exec = 0;
            if (this.mServerSyncTrophyList == null)
            {
                goto Label_003E;
            }
            if (this.mServerSyncChallengeList == null)
            {
                goto Label_003E;
            }
            if (this.mServerSyncTrophyAward != null)
            {
                goto Label_003F;
            }
        Label_003E:
            return;
        Label_003F:
            MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncInterval();
            list = new List<TrophyState>((this.mServerSyncTrophyList.Count + this.mServerSyncChallengeList.Count) + this.mServerSyncTrophyAward.Count);
            list.AddRange(this.mServerSyncTrophyList);
            list.AddRange(this.mServerSyncChallengeList);
            list.AddRange(this.mServerSyncTrophyAward);
            MonoSingleton<GameManager>.Instance.SaveUpdateTrophyList(list);
            if (this.mServerSyncTrophyList.Count <= 0)
            {
                goto Label_0131;
            }
            enumerator = this.mServerSyncTrophyList.GetEnumerator();
        Label_00C3:
            try
            {
                goto Label_0114;
            Label_00C8:
                state = &enumerator.Current;
                state.IsDirty = 0;
                state.IsSending = 0;
                if (state.IsCompleted == null)
                {
                    goto Label_0114;
                }
                if (state.Param.IsDaily == null)
                {
                    goto Label_0109;
                }
                NotifyList.PushDailyTrophy(state.Param);
                goto Label_0114;
            Label_0109:
                NotifyList.PushTrophy(state.Param);
            Label_0114:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00C8;
                }
                goto Label_0131;
            }
            finally
            {
            Label_0125:
                ((List<TrophyState>.Enumerator) enumerator).Dispose();
            }
        Label_0131:
            if (this.mServerSyncChallengeList.Count <= 0)
            {
                goto Label_019E;
            }
            enumerator2 = this.mServerSyncChallengeList.GetEnumerator();
        Label_014F:
            try
            {
                goto Label_0180;
            Label_0154:
                state2 = &enumerator2.Current;
                state2.IsDirty = 0;
                state2.IsSending = 0;
                if (state2.IsCompleted == null)
                {
                    goto Label_0180;
                }
                NotifyList.PushTrophy(state2.Param);
            Label_0180:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0154;
                }
                goto Label_019E;
            }
            finally
            {
            Label_0191:
                ((List<TrophyState>.Enumerator) enumerator2).Dispose();
            }
        Label_019E:
            if (this.mServerSyncTrophyAward.Count <= 0)
            {
                goto Label_0285;
            }
            enumerator3 = this.mServerSyncTrophyAward.GetEnumerator();
        Label_01BC:
            try
            {
                goto Label_01F2;
            Label_01C1:
                state3 = &enumerator3.Current;
                state3.IsDirty = 0;
                state3.IsSending = 0;
                if (state3.IsCompleted == null)
                {
                    goto Label_01F2;
                }
                NotifyList.PushAward(state3.Param);
            Label_01F2:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_01C1;
                }
                goto Label_0210;
            }
            finally
            {
            Label_0203:
                ((List<TrophyState>.Enumerator) enumerator3).Dispose();
            }
        Label_0210:
            try
            {
                response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(&www.text);
                DebugUtility.Assert((response == null) == 0, "res == null");
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_0285;
            }
            catch (Exception exception1)
            {
            Label_0277:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0285;
            }
        Label_0285:
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            this.mServerSyncTrophyList = null;
            this.mServerSyncChallengeList = null;
            this.mServerSyncTrophyAward = null;
            MonoSingleton<GameManager>.Instance.update_trophy_lock.Unlock();
            return;
        }

        public unsafe void ServerSyncTrophyExecStart(out string trophy_progs, out string bingo_progs)
        {
            ReqUpdateTrophy trophy;
            TrophyState state;
            List<TrophyState>.Enumerator enumerator;
            TrophyState state2;
            List<TrophyState>.Enumerator enumerator2;
            ReqUpdateBingo bingo;
            TrophyState state3;
            List<TrophyState>.Enumerator enumerator3;
            *(trophy_progs) = string.Empty;
            *(bingo_progs) = string.Empty;
            if (this.is_start_server_sync_trophy_exec == null)
            {
                goto Label_0024;
            }
            DebugUtility.Log("ServerSyncTrophyExecBegin が連続で呼ばれています。");
            return;
        Label_0024:
            this.is_start_server_sync_trophy_exec = 1;
            MonoSingleton<GameManager>.Instance.update_trophy_lock.Lock();
            MonoSingleton<GameManager>.Instance.CreateUpdateTrophyList(&this.mServerSyncTrophyList, &this.mServerSyncChallengeList, &this.mServerSyncTrophyAward);
            if (0 < ((this.mServerSyncTrophyList.Count + this.mServerSyncChallengeList.Count) + this.mServerSyncTrophyAward.Count))
            {
                goto Label_0080;
            }
            return;
        Label_0080:
            if (0 < this.mServerSyncTrophyList.Count)
            {
                goto Label_00A2;
            }
            if (0 >= this.mServerSyncTrophyAward.Count)
            {
                goto Label_0160;
            }
        Label_00A2:
            trophy = new ReqUpdateTrophy();
            trophy.BeginTrophyReqString();
            trophy.AddTrophyReqString(this.mServerSyncTrophyList, 0);
            trophy.AddTrophyReqString(this.mServerSyncTrophyAward, 1);
            trophy.EndTrophyReqString();
            *(trophy_progs) = trophy.GetTrophyReqString();
            enumerator = this.mServerSyncTrophyList.GetEnumerator();
        Label_00E2:
            try
            {
                goto Label_00FD;
            Label_00E7:
                state = &enumerator.Current;
                state.IsDirty = 0;
                state.IsSending = 0;
            Label_00FD:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00E7;
                }
                goto Label_011A;
            }
            finally
            {
            Label_010E:
                ((List<TrophyState>.Enumerator) enumerator).Dispose();
            }
        Label_011A:
            enumerator2 = this.mServerSyncTrophyAward.GetEnumerator();
        Label_0127:
            try
            {
                goto Label_0142;
            Label_012C:
                state2 = &enumerator2.Current;
                state2.IsDirty = 0;
                state2.IsSending = 0;
            Label_0142:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_012C;
                }
                goto Label_0160;
            }
            finally
            {
            Label_0153:
                ((List<TrophyState>.Enumerator) enumerator2).Dispose();
            }
        Label_0160:
            if (0 >= this.mServerSyncChallengeList.Count)
            {
                goto Label_01E6;
            }
            bingo = new ReqUpdateBingo();
            bingo.BeginBingoReqString();
            bingo.AddBingoReqString(this.mServerSyncChallengeList, 0);
            bingo.EndBingoReqString();
            *(bingo_progs) = bingo.GetBingoReqString();
            enumerator3 = this.mServerSyncChallengeList.GetEnumerator();
        Label_01AA:
            try
            {
                goto Label_01C8;
            Label_01AF:
                state3 = &enumerator3.Current;
                state3.IsDirty = 0;
                state3.IsSending = 0;
            Label_01C8:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_01AF;
                }
                goto Label_01E6;
            }
            finally
            {
            Label_01D9:
                ((List<TrophyState>.Enumerator) enumerator3).Dispose();
            }
        Label_01E6:
            return;
        }

        public void SetAvailableRankingQuestParams(List<RankingQuestParam> value)
        {
            this.mAvailableRankingQuesstParams = value;
            return;
        }

        public bool SetVersusWinCount(int wincnt)
        {
            PlayerData data;
            data = this.Player;
            if (data == null)
            {
                goto Label_0025;
            }
            data.SetVersusWinCount(GlobalVars.SelectedMultiPlayVersusType, wincnt);
            data.AddVersusTotalCount(GlobalVars.SelectedMultiPlayVersusType, 1);
        Label_0025:
            return 1;
        }

        public void SetVersuTowerEndParam(bool rankup, bool winbonus, int key, int floor, int arravied)
        {
            if (this.mVersusEndParam == null)
            {
                goto Label_0049;
            }
            this.mVersusEndParam.rankup = rankup;
            this.mVersusEndParam.winbonus = winbonus;
            this.mVersusEndParam.key = key;
            this.mVersusEndParam.floor = floor;
            this.mVersusEndParam.arravied = arravied;
        Label_0049:
            return;
        }

        public void ShowCharacterQuestPopup(string template)
        {
            base.StartCoroutine(this.ShowCharacterQuestPopupAsync(template));
            return;
        }

        [DebuggerHidden]
        public IEnumerator ShowCharacterQuestPopupAsync(string template)
        {
            <ShowCharacterQuestPopupAsync>c__IteratorD8 rd;
            rd = new <ShowCharacterQuestPopupAsync>c__IteratorD8();
            rd.template = template;
            rd.<$>template = template;
            rd.<>f__this = this;
            return rd;
        }

        [DebuggerHidden]
        public IEnumerator SkinUnlockPopup(ArtifactParam unlockSkin)
        {
            <SkinUnlockPopup>c__IteratorD9 rd;
            rd = new <SkinUnlockPopup>c__IteratorD9();
            rd.unlockSkin = unlockSkin;
            rd.<$>unlockSkin = unlockSkin;
            return rd;
        }

        [DebuggerHidden]
        public IEnumerator SkinUnlockPopup(ItemParam[] rewardItems)
        {
            <SkinUnlockPopup>c__IteratorDA rda;
            rda = new <SkinUnlockPopup>c__IteratorDA();
            rda.rewardItems = rewardItems;
            rda.<$>rewardItems = rewardItems;
            return rda;
        }

        private void Start()
        {
            this.UpdateResolution();
            LogMonitor.Start();
            DebugMenu.Start();
            ExceptionMonitor.Start();
            return;
        }

        public void StartBuyCoinSequence()
        {
            DestroyEventListener local1;
            GameObject obj2;
            DestroyEventListener listener;
            obj2 = GameSettings.Instance.Dialog_BuyCoin;
            if ((obj2 != null) == null)
            {
                goto Label_0051;
            }
            this.mBuyCoinWindow = Object.Instantiate<GameObject>(obj2);
            local1 = SRPG_Extensions.RequireComponent<DestroyEventListener>(this.mBuyCoinWindow);
            local1.Listeners = (DestroyEventListener.DestroyEvent) Delegate.Combine(local1.Listeners, new DestroyEventListener.DestroyEvent(this.OnBuyCoinEnd));
        Label_0051:
            return;
        }

        public void StartBuyStaminaSequence(bool staminaLacking)
        {
            this.StartBuyStaminaSequence(staminaLacking, null);
            return;
        }

        public void StartBuyStaminaSequence(bool _staminaLacking, PartyWindow2 _pwindow)
        {
            object[] objArray2;
            object[] objArray1;
            bool flag;
            FixParam param;
            string str;
            if (this.Player.IsHaveHealAPItems() == null)
            {
                goto Label_0025;
            }
            base.StartCoroutine(this.StartBuyStaminaSequence2(_pwindow));
            goto Label_00C4;
        Label_0025:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            str = string.Empty;
            if (_staminaLacking == null)
            {
                goto Label_0079;
            }
            objArray1 = new object[] { (int) this.Player.GetStaminaRecoveryCost(0), (OInt) param.StaminaAdd };
            str = LocalizedText.Get("sys.STAMINA_NOT_ENOUGH", objArray1);
            goto Label_00AC;
        Label_0079:
            objArray2 = new object[] { (int) this.Player.GetStaminaRecoveryCost(0), (OInt) param.StaminaAdd };
            str = LocalizedText.Get("sys.RESET_STAMINA", objArray2);
        Label_00AC:
            UIUtility.ConfirmBox(str, null, new UIUtility.DialogResultEvent(this.<StartBuyStaminaSequence>m__1F9), null, null, 0, -1);
        Label_00C4:
            return;
        }

        [DebuggerHidden]
        private IEnumerator StartBuyStaminaSequence2(PartyWindow2 _pwindow)
        {
            <StartBuyStaminaSequence2>c__IteratorD4 rd;
            rd = new <StartBuyStaminaSequence2>c__IteratorD4();
            rd._pwindow = _pwindow;
            rd.<$>_pwindow = _pwindow;
            return rd;
        }

        private unsafe void Update()
        {
            DateTime time;
            long num;
            long num2;
            int num3;
            int num4;
            float num5;
            float num6;
            if (this.mHasLoggedIn == null)
            {
                goto Label_00FE;
            }
            time = TimeManager.ServerTime;
            num = &time.Ticks / 0xc92a69c000L;
            num2 = &this.mLastUpdateTime.Ticks / 0xc92a69c000L;
            this.mLastUpdateTime = time;
            if ((num - num2) < 1L)
            {
                goto Label_004A;
            }
        Label_004A:
            num3 = this.Player.Stamina;
            if (num3 == this.mLastStamina)
            {
                goto Label_0074;
            }
            this.mLastStamina = num3;
            this.OnStaminaChange();
        Label_0074:
            this.Player.UpdateAbilityRankUpCount();
            num4 = this.Player.AbilityRankUpCountNum;
            if (num4 == this.mLastAbilityRankUpCount)
            {
                goto Label_00AE;
            }
            this.OnAbilityRankUpCountChange(num4);
            this.mLastAbilityRankUpCount = num4;
        Label_00AE:
            if (((long) this.Player.Gold) == this.mLastGold)
            {
                goto Label_00E2;
            }
            this.mLastGold = (long) this.Player.Gold;
            this.OnPlayerCurrencyChange();
        Label_00E2:
            this.UpdateTrophy();
            if (this.mAudienceManager == null)
            {
                goto Label_00FE;
            }
            this.mAudienceManager.Update();
        Label_00FE:
            this.EnableAnimationFrameSkipping = 0;
            if (this.EnableAnimationFrameSkipping == null)
            {
                goto Label_0154;
            }
            num5 = Time.get_unscaledDeltaTime();
            num6 = 1f - Mathf.Clamp01((num5 - 0.026f) / 0.003999999f);
            AnimationPlayer.MaxUpdateTime = (long) (Mathf.Lerp(0.001f, 0.006f, num6) * 1E+07f);
            goto Label_0162;
        Label_0154:
            AnimationPlayer.MaxUpdateTime = 0x7fffffffffffffffL;
        Label_0162:
            this.UpdateTextureLoadRequests();
            return;
        }

        private unsafe void UpdateChallengeResponseCallback(WWWResult www)
        {
            TrophyState state;
            List<TrophyState>.Enumerator enumerator;
            this.update_trophy_interval.SetSyncInterval();
            if (Network.IsError == null)
            {
                goto Label_0034;
            }
            if (Network.ErrCode != 0x1010)
            {
                goto Label_002E;
            }
            Network.ResetError();
            goto Label_0034;
        Label_002E:
            FlowNode_Network.Retry();
            return;
        Label_0034:
            enumerator = this.mUpdateChallengeList.GetEnumerator();
        Label_0040:
            try
            {
                goto Label_008A;
            Label_0045:
                state = &enumerator.Current;
                state.IsSending = 0;
                if (state.IsCompleted == null)
                {
                    goto Label_008A;
                }
                if (state.Param.IsDaily == null)
                {
                    goto Label_007F;
                }
                NotifyList.PushDailyTrophy(state.Param);
                goto Label_008A;
            Label_007F:
                NotifyList.PushTrophy(state.Param);
            Label_008A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0045;
                }
                goto Label_00A7;
            }
            finally
            {
            Label_009B:
                ((List<TrophyState>.Enumerator) enumerator).Dispose();
            }
        Label_00A7:
            this.mUpdateChallengeList = null;
            Network.RemoveAPI();
            return;
        }

        private void UpdateResolution()
        {
            bool flag;
            int num;
            int num2;
            float num3;
            int num4;
            flag = 0;
            if (mUpscaleMode == flag)
            {
                goto Label_0068;
            }
            mUpscaleMode = flag;
            num = ScreenUtility.DefaultScreenWidth;
            num2 = ScreenUtility.DefaultScreenHeight;
            if (flag == null)
            {
                goto Label_0046;
            }
            num3 = ((float) num) / ((float) num2);
            num4 = Mathf.Min(num2, 750);
            num = Mathf.FloorToInt(num3 * ((float) num4));
            num2 = num4;
        Label_0046:
            ScreenUtility.SetResolution(num, num2);
            DebugUtility.Log(string.Format("Changing Resolution to [{0} x {1}]", (int) num, (int) num2));
        Label_0068:
            return;
        }

        private void UpdateTextureLoadRequests()
        {
            int num;
            num = 0;
            goto Label_00D5;
        Label_0007:
            if ((this.mTextureRequests[num].Target == null) == null)
            {
                goto Label_0038;
            }
            this.mTextureRequests.RemoveAt(num--);
            goto Label_00D1;
        Label_0038:
            if (this.mTextureRequests[num].Request != null)
            {
                goto Label_0070;
            }
            if (AssetManager.IsLoading != null)
            {
                goto Label_00D1;
            }
            this.RequestTexture(this.mTextureRequests[num]);
            goto Label_00D1;
        Label_0070:
            if (this.mTextureRequests[num].Request.isDone == null)
            {
                goto Label_00D1;
            }
            this.mTextureRequests[num].Target.set_texture(this.mTextureRequests[num].Request.asset as Texture2D);
            this.mTextureRequests.RemoveAt(num--);
        Label_00D1:
            num += 1;
        Label_00D5:
            if (num < this.mTextureRequests.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private void UpdateTrophy()
        {
            if (Network.Mode == null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            if (this.update_trophy_interval.PlayCheckUpdate() != null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            if (this.update_trophy_lock.IsLock == null)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            if (this.IsExternalPermit() != null)
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            if (CriticalSection.IsActive == null)
            {
                goto Label_0044;
            }
            return;
        Label_0044:
            this.update_trophy_interval.SetUpdateInterval();
            this.UpdateTrophyAPI();
            return;
        }

        public unsafe void UpdateTrophyAPI()
        {
            List<TrophyState> list;
            List<TrophyState> list2;
            List<TrophyState> list3;
            List<TrophyState> list4;
            TrophyState state;
            List<TrophyState>.Enumerator enumerator;
            ReqUpdateTrophy trophy;
            TrophyState state2;
            List<TrophyState>.Enumerator enumerator2;
            ReqUpdateBingo bingo;
            TrophyState state3;
            List<TrophyState>.Enumerator enumerator3;
            ReqUpdateTrophy trophy2;
            this.CreateUpdateTrophyList(&list, &list2, &list3);
            list4 = new List<TrophyState>((list.Count + list2.Count) + list3.Count);
            list4.AddRange(list);
            list4.AddRange(list2);
            list4.AddRange(list3);
            this.SaveUpdateTrophyList(list4);
            if (list4.Count > 0)
            {
                goto Label_004F;
            }
            return;
        Label_004F:
            if (list.Count <= 0)
            {
                goto Label_00CD;
            }
            this.mUpdateTrophyList = list;
            enumerator = this.mUpdateTrophyList.GetEnumerator();
        Label_006F:
            try
            {
                goto Label_008D;
            Label_0074:
                state = &enumerator.Current;
                state.IsDirty = 0;
                state.IsSending = 1;
            Label_008D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0074;
                }
                goto Label_00AB;
            }
            finally
            {
            Label_009E:
                ((List<TrophyState>.Enumerator) enumerator).Dispose();
            }
        Label_00AB:
            trophy = new ReqUpdateTrophy(this.mUpdateTrophyList, new Network.ResponseCallback(this.UpdateTrophyResponseCallback), 0);
            Network.RequestAPI(trophy, 0);
        Label_00CD:
            if (list2.Count <= 0)
            {
                goto Label_014B;
            }
            this.mUpdateChallengeList = list2;
            enumerator2 = this.mUpdateChallengeList.GetEnumerator();
        Label_00ED:
            try
            {
                goto Label_010B;
            Label_00F2:
                state2 = &enumerator2.Current;
                state2.IsDirty = 0;
                state2.IsSending = 1;
            Label_010B:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00F2;
                }
                goto Label_0129;
            }
            finally
            {
            Label_011C:
                ((List<TrophyState>.Enumerator) enumerator2).Dispose();
            }
        Label_0129:
            bingo = new ReqUpdateBingo(this.mUpdateChallengeList, new Network.ResponseCallback(this.UpdateChallengeResponseCallback), 0);
            Network.RequestAPI(bingo, 0);
        Label_014B:
            if (list3.Count <= 0)
            {
                goto Label_01C9;
            }
            this.mUpdateTrophyAward = list3;
            enumerator3 = this.mUpdateTrophyAward.GetEnumerator();
        Label_016B:
            try
            {
                goto Label_0189;
            Label_0170:
                state3 = &enumerator3.Current;
                state3.IsDirty = 0;
                state3.IsSending = 1;
            Label_0189:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0170;
                }
                goto Label_01A7;
            }
            finally
            {
            Label_019A:
                ((List<TrophyState>.Enumerator) enumerator3).Dispose();
            }
        Label_01A7:
            trophy2 = new ReqUpdateTrophy(this.mUpdateTrophyAward, new Network.ResponseCallback(this.UpdateTrophyAwardResponseCallback), 1);
            Network.RequestAPI(trophy2, 0);
        Label_01C9:
            return;
        }

        private unsafe void UpdateTrophyAwardResponseCallback(WWWResult www)
        {
            TrophyState state;
            List<TrophyState>.Enumerator enumerator;
            WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll> response;
            Exception exception;
            this.update_trophy_interval.SetSyncInterval();
            if (Network.IsError == null)
            {
                goto Label_001B;
            }
            FlowNode_Network.Retry();
            return;
        Label_001B:
            enumerator = this.mUpdateTrophyAward.GetEnumerator();
        Label_0027:
            try
            {
                goto Label_0051;
            Label_002C:
                state = &enumerator.Current;
                state.IsSending = 0;
                if (state.IsCompleted == null)
                {
                    goto Label_0051;
                }
                NotifyList.PushAward(state.Param);
            Label_0051:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002C;
                }
                goto Label_006E;
            }
            finally
            {
            Label_0062:
                ((List<TrophyState>.Enumerator) enumerator).Dispose();
            }
        Label_006E:
            this.mUpdateTrophyAward = null;
            Network.RemoveAPI();
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_TrophyPlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00A4;
            }
            return;
        Label_00A4:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.items);
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_00F9;
            }
            catch (Exception exception1)
            {
            Label_00E8:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00F9;
            }
        Label_00F9:
            return;
        }

        private unsafe void UpdateTrophyResponseCallback(WWWResult www)
        {
            TrophyState state;
            List<TrophyState>.Enumerator enumerator;
            this.update_trophy_interval.SetSyncInterval();
            if (Network.IsError == null)
            {
                goto Label_001B;
            }
            FlowNode_Network.Retry();
            return;
        Label_001B:
            enumerator = this.mUpdateTrophyList.GetEnumerator();
        Label_0027:
            try
            {
                goto Label_0071;
            Label_002C:
                state = &enumerator.Current;
                state.IsSending = 0;
                if (state.IsCompleted == null)
                {
                    goto Label_0071;
                }
                if (state.Param.IsDaily == null)
                {
                    goto Label_0066;
                }
                NotifyList.PushDailyTrophy(state.Param);
                goto Label_0071;
            Label_0066:
                NotifyList.PushTrophy(state.Param);
            Label_0071:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002C;
                }
                goto Label_008E;
            }
            finally
            {
            Label_0082:
                ((List<TrophyState>.Enumerator) enumerator).Dispose();
            }
        Label_008E:
            this.mUpdateTrophyList = null;
            this.RequestUpdateBadges(0x10);
            Network.RemoveAPI();
            return;
        }

        public void UpdateTutorialFlags(long add)
        {
            PlayerData data1;
            if ((this.Player.TutorialFlags & add) != add)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            data1 = this.Player;
            data1.TutorialFlags |= add;
            if (Network.Mode != 1)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            Network.RequestAPI(new ReqTutUpdate(this.Player.TutorialFlags, new Network.ResponseCallback(this.OnTutorialFlagUpdate)), 0);
            return;
        }

        public void UpdateTutorialFlags(string flagName)
        {
            long num;
            num = GameSettings.Instance.CreateTutorialFlagMask(flagName);
            if (num == null)
            {
                goto Label_0019;
            }
            this.UpdateTutorialFlags(num);
        Label_0019:
            return;
        }

        public void UpdateTutorialStep()
        {
            GameSettings settings;
            int num;
            QuestParam param;
            settings = GameSettings.Instance;
            this.mTutorialStep = (int) settings.Tutorial_Steps.Length;
            num = ((int) settings.Tutorial_Steps.Length) - 1;
            goto Label_006C;
        Label_0024:
            if (string.IsNullOrEmpty(settings.Tutorial_Steps[num]) == null)
            {
                goto Label_003B;
            }
            goto Label_0068;
        Label_003B:
            param = this.FindQuest(settings.Tutorial_Steps[num]);
            if (param == null)
            {
                goto Label_0061;
            }
            if (param.state != 2)
            {
                goto Label_0061;
            }
            goto Label_0073;
        Label_0061:
            this.mTutorialStep = num;
        Label_0068:
            num -= 1;
        Label_006C:
            if (num >= 0)
            {
                goto Label_0024;
            }
        Label_0073:
            return;
        }

        [DebuggerHidden]
        private IEnumerator UpdateUnitsBadges()
        {
            <UpdateUnitsBadges>c__IteratorD2 rd;
            rd = new <UpdateUnitsBadges>c__IteratorD2();
            rd.<>f__this = this;
            return rd;
        }

        [DebuggerHidden]
        private IEnumerator UpdateUnitUnlockBadges()
        {
            <UpdateUnitUnlockBadges>c__IteratorD3 rd;
            rd = new <UpdateUnitUnlockBadges>c__IteratorD3();
            rd.<>f__this = this;
            return rd;
        }

        [DebuggerHidden]
        private IEnumerator WaitDownloadAsync(List<AssetList.Item> queue, AssetDownloader.DownloadState state)
        {
            <WaitDownloadAsync>c__IteratorD7 rd;
            rd = new <WaitDownloadAsync>c__IteratorD7();
            rd.state = state;
            rd.queue = queue;
            rd.<$>state = state;
            rd.<$>queue = queue;
            rd.<>f__this = this;
            return rd;
        }

        public bool IsRelogin
        {
            get
            {
                return this.mRelogin;
            }
            set
            {
                this.mRelogin = value;
                return;
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
                MonoSingleton<UserInfoManager>.Instance.SetValue("tou_agreed_ver", value, 1);
                return;
            }
        }

        public bool VersusTowerMatchBegin
        {
            [CompilerGenerated]
            get
            {
                return this.<VersusTowerMatchBegin>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VersusTowerMatchBegin>k__BackingField = value;
                return;
            }
        }

        public bool VersusTowerMatchReceipt
        {
            [CompilerGenerated]
            get
            {
                return this.<VersusTowerMatchReceipt>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VersusTowerMatchReceipt>k__BackingField = value;
                return;
            }
        }

        public string VersusTowerMatchName
        {
            [CompilerGenerated]
            get
            {
                return this.<VersusTowerMatchName>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VersusTowerMatchName>k__BackingField = value;
                return;
            }
        }

        public long VersusTowerMatchEndAt
        {
            [CompilerGenerated]
            get
            {
                return this.<VersusTowerMatchEndAt>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VersusTowerMatchEndAt>k__BackingField = value;
                return;
            }
        }

        public int VersusCoinRemainCnt
        {
            [CompilerGenerated]
            get
            {
                return this.<VersusCoinRemainCnt>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VersusCoinRemainCnt>k__BackingField = value;
                return;
            }
        }

        public string VersusLastUid
        {
            [CompilerGenerated]
            get
            {
                return this.<VersusLastUid>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VersusLastUid>k__BackingField = value;
                return;
            }
        }

        public bool RankMatchBegin
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchBegin>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchBegin>k__BackingField = value;
                return;
            }
        }

        public int RankMatchScheduleId
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchScheduleId>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchScheduleId>k__BackingField = value;
                return;
            }
        }

        public ReqRankMatchStatus.RankingStatus RankMatchRankingStatus
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchRankingStatus>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchRankingStatus>k__BackingField = value;
                return;
            }
        }

        public long RankMatchExpiredTime
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchExpiredTime>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchExpiredTime>k__BackingField = value;
                return;
            }
        }

        public long RankMatchNextTime
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchNextTime>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchNextTime>k__BackingField = value;
                return;
            }
        }

        public string[] RankMatchMatchedEnemies
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchMatchedEnemies>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchMatchedEnemies>k__BackingField = value;
                return;
            }
        }

        public string DigestHash
        {
            [CompilerGenerated]
            get
            {
                return this.<DigestHash>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<DigestHash>k__BackingField = value;
                return;
            }
        }

        public string PrevCheckHash
        {
            [CompilerGenerated]
            get
            {
                return this.<PrevCheckHash>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<PrevCheckHash>k__BackingField = value;
                return;
            }
        }

        public string AlterCheckHash
        {
            [CompilerGenerated]
            get
            {
                return this.<AlterCheckHash>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<AlterCheckHash>k__BackingField = value;
                return;
            }
        }

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

        public bool AudienceMode
        {
            [CompilerGenerated]
            get
            {
                return this.<AudienceMode>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<AudienceMode>k__BackingField = value;
                return;
            }
        }

        public MyPhoton.MyRoom AudienceRoom
        {
            [CompilerGenerated]
            get
            {
                return this.<AudienceRoom>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<AudienceRoom>k__BackingField = value;
                return;
            }
        }

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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
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
                return;
            }
        }

        public long mVersusEnableId
        {
            [CompilerGenerated]
            get
            {
                return this.<mVersusEnableId>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<mVersusEnableId>k__BackingField = value;
                return;
            }
        }

        public VersusDraftType VSDraftType
        {
            get
            {
                return this.mVersusDraftType;
            }
            set
            {
                this.mVersusDraftType = value;
                return;
            }
        }

        public long VSDraftId
        {
            [CompilerGenerated]
            get
            {
                return this.<VSDraftId>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VSDraftId>k__BackingField = value;
                return;
            }
        }

        public string VSDraftQuestId
        {
            [CompilerGenerated]
            get
            {
                return this.<VSDraftQuestId>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VSDraftQuestId>k__BackingField = value;
                return;
            }
        }

        public bool IsVSCpuBattle
        {
            get
            {
                return this.mSelectedVersusCpuBattle;
            }
            set
            {
                this.mSelectedVersusCpuBattle = value;
                return;
            }
        }

        public List<SRPG.VersusCpuData> VersusCpuData
        {
            get
            {
                return this.mVersusCpuData;
            }
        }

        public ArenaPlayer[] ArenaPlayers
        {
            get
            {
                return this.mArenaPlayers.ToArray();
            }
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

        public SRPG.MasterParam MasterParam
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

        public SRPG.TowerResuponse TowerResuponse
        {
            get
            {
                return this.mTowerResuponse;
            }
        }

        public List<SRPG.TowerResuponse.PlayerUnit> TowerResuponsePlayerUnit
        {
            get
            {
                return this.TowerResuponse.pdeck;
            }
        }

        public List<SRPG.TowerResuponse.EnemyUnit> TowerEnemyUnit
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

        public string DeviceId
        {
            get
            {
                return ((this.mMyGuid == null) ? null : this.mMyGuid.device_id);
            }
        }

        public string SecretKey
        {
            get
            {
                return ((this.mMyGuid == null) ? null : this.mMyGuid.secret_key);
            }
        }

        public string UdId
        {
            get
            {
                return ((this.mMyGuid == null) ? null : this.mMyGuid.udid);
            }
        }

        public bool IsBuyCoinWindowOpen
        {
            get
            {
                if ((this.mBuyCoinWindow != null) == null)
                {
                    goto Label_0013;
                }
                return 1;
            Label_0013:
                this.mBuyCoinWindow = null;
                return 0;
            }
        }

        public bool IsImportantJobRunning
        {
            get
            {
                return (this.mImportantJobs.Count > 0);
            }
        }

        public int TutorialStep
        {
            get
            {
                return this.mTutorialStep;
            }
        }

        public bool HasTutorialDLAssets
        {
            get
            {
                if (this.mScannedTutorialAssets != null)
                {
                    goto Label_0011;
                }
                this.RefreshTutorialDLAssets();
            Label_0011:
                return (this.mTutorialDLAssets.Count > 0);
            }
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
                return;
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
                return;
            }
        }

        public bool IsLimitedShopOpen
        {
            get
            {
                return this.mIsLimitedShopOpen;
            }
            set
            {
                this.mIsLimitedShopOpen = value;
                return;
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

        [CompilerGenerated]
        private sealed class <AddCharacterQuestPopup>c__AnonStorey298
        {
            internal UnitData unit;

            public <AddCharacterQuestPopup>c__AnonStorey298()
            {
                base..ctor();
                return;
            }

            internal bool <>m__200(UnitData u)
            {
                return (u.UnitID == this.unit.UnitID);
            }
        }

        [CompilerGenerated]
        private sealed class <AsyncWaitForImportantJobs>c__IteratorD5 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal int $PC;
            internal object $current;
            internal GameManager <>f__this;

            public <AsyncWaitForImportantJobs>c__IteratorD5()
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
                        goto Label_0070;
                }
                goto Label_00BC;
            Label_0021:
                this.<i>__0 = 0;
                goto Label_007E;
            Label_002D:
                if (this.<>f__this.mImportantJobs[this.<i>__0] == null)
                {
                    goto Label_0070;
                }
                this.$current = this.<>f__this.mImportantJobs[this.<i>__0];
                this.$PC = 1;
                goto Label_00BE;
            Label_0070:
                this.<i>__0 += 1;
            Label_007E:
                if (this.<i>__0 < this.<>f__this.mImportantJobs.Count)
                {
                    goto Label_002D;
                }
                this.<>f__this.mImportantJobs.Clear();
                this.<>f__this.mImportantJobCoroutine = null;
                this.$PC = -1;
            Label_00BC:
                return 0;
            Label_00BE:
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
        private sealed class <Deserialize>c__AnonStorey296
        {
            internal QuestCampaignTrust param;

            public <Deserialize>c__AnonStorey296()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1F8(QuestCampaignChildParam value)
            {
                return (value.iname == this.param.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <DownloadAndTransitSceneAsync>c__IteratorD6 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string sceneName;
            internal int $PC;
            internal object $current;
            internal string <$>sceneName;

            public <DownloadAndTransitSceneAsync>c__IteratorD6()
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
                        goto Label_0060;

                    case 2:
                        goto Label_008F;
                }
                goto Label_0096;
            Label_0025:
                AssetManager.PrepareAssets(this.sceneName);
                if (AssetDownloader.isDone != null)
                {
                    goto Label_006A;
                }
                AssetDownloader.StartDownload(0, 1, 2);
                ProgressWindow.OpenGenericDownloadWindow();
                goto Label_0060;
            Label_004D:
                this.$current = null;
                this.$PC = 1;
                goto Label_0098;
            Label_0060:
                if (AssetDownloader.isDone == null)
                {
                    goto Label_004D;
                }
            Label_006A:
                AssetManager.LoadSceneImmediate(this.sceneName, 0);
                CriticalSection.Leave(4);
                this.$current = null;
                this.$PC = 2;
                goto Label_0098;
            Label_008F:
                this.$PC = -1;
            Label_0096:
                return 0;
            Label_0098:
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
        private sealed class <ExistsOpenVersusTower>c__AnonStorey29B
        {
            internal string towerID;

            public <ExistsOpenVersusTower>c__AnonStorey29B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__204(VersusScheduleParam data)
            {
                return string.Equals(data.tower_iname, this.towerID);
            }
        }

        [CompilerGenerated]
        private sealed class <FindArenaPlayer>c__AnonStorey294
        {
            internal string fuid;

            public <FindArenaPlayer>c__AnonStorey294()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1F6(ArenaPlayer p)
            {
                return (p.FUID == this.fuid);
            }
        }

        [CompilerGenerated]
        private sealed class <FindAvailableRankingQuest>c__AnonStorey2A1
        {
            internal string iname;

            public <FindAvailableRankingQuest>c__AnonStorey2A1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__20A(RankingQuestParam param)
            {
                return (param.iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindMail>c__AnonStorey295
        {
            internal long mailID;

            public <FindMail>c__AnonStorey295()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1F7(MailData mail)
            {
                return (mail.mid == this.mailID);
            }
        }

        [CompilerGenerated]
        private sealed class <FindNextTowerFloor>c__AnonStorey292
        {
            internal string currentFloorID;

            public <FindNextTowerFloor>c__AnonStorey292()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1F4(TowerFloorParam floorParam)
            {
                if (string.IsNullOrEmpty(floorParam.cond_quest) == null)
                {
                    goto Label_0012;
                }
                return 0;
            Label_0012:
                return (floorParam.cond_quest == this.currentFloorID);
            }
        }

        [CompilerGenerated]
        private sealed class <FindQuestCampaigns>c__AnonStorey290
        {
            internal QuestCampaignData d;

            public <FindQuestCampaigns>c__AnonStorey290()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1F1(QuestCampaignData value)
            {
                return (value.type == this.d.type);
            }
        }

        [CompilerGenerated]
        private sealed class <FindQuestCampaigns>c__AnonStorey291
        {
            internal QuestCampaignData d;

            public <FindQuestCampaigns>c__AnonStorey291()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1F2(QuestCampaignData value)
            {
                return (value.type == this.d.type);
            }
        }

        [CompilerGenerated]
        private sealed class <FindTowerFloors>c__AnonStorey293
        {
            internal string towerID;

            public <FindTowerFloors>c__AnonStorey293()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1F5(TowerFloorParam floor)
            {
                return (floor.tower_id == this.towerID);
            }
        }

        [CompilerGenerated]
        private sealed class <FindVersusTowerScheduleParam>c__AnonStorey29A
        {
            internal string towerID;

            public <FindVersusTowerScheduleParam>c__AnonStorey29A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__202(VersusScheduleParam data)
            {
                return string.Equals(data.tower_iname, this.towerID);
            }
        }

        [CompilerGenerated]
        private sealed class <GetCurrentVersusTowerParam>c__AnonStorey299
        {
            internal string iname;
            internal int floor;

            public <GetCurrentVersusTowerParam>c__AnonStorey299()
            {
                base..ctor();
                return;
            }

            internal bool <>m__201(VersusTowerParam data)
            {
                return (((data.VersusTowerID == this.iname) == null) ? 0 : (data.Floor == this.floor));
            }
        }

        [CompilerGenerated]
        private sealed class <GetMapEffectParam>c__AnonStorey29F
        {
            internal string iname;

            public <GetMapEffectParam>c__AnonStorey29F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__208(MapEffectParam d)
            {
                return (d.Iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetMaxBattlePoint>c__AnonStorey289
        {
            internal int schedule_id;

            public <GetMaxBattlePoint>c__AnonStorey289()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1E7(VersusRankParam vr)
            {
                return (vr.Id == this.schedule_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetMTFloorParam>c__AnonStorey29D
        {
            internal int floor;
            internal string type;

            public <GetMTFloorParam>c__AnonStorey29D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__206(MultiTowerFloorParam data)
            {
                return ((data.floor != this.floor) ? 0 : (data.tower_id == this.type));
            }
        }

        [CompilerGenerated]
        private sealed class <GetMTFloorReward>c__AnonStorey29E
        {
            internal string iname;

            public <GetMTFloorReward>c__AnonStorey29E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__207(MultiTowerRewardParam data)
            {
                return (data.iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetNextVersusRankClass>c__AnonStorey287
        {
            internal int schedule_id;
            internal RankMatchClass current_class;

            public <GetNextVersusRankClass>c__AnonStorey287()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1E5(VersusRankClassParam vrc)
            {
                return ((vrc.ScheduleId != this.schedule_id) ? 0 : (vrc.Class == this.current_class));
            }
        }

        [CompilerGenerated]
        private sealed class <GetReleaseStoryPartWorldName>c__AnonStorey2A2
        {
            internal int story_part;
            internal SectionParam section_param;
            internal QuestParam check_param;

            public <GetReleaseStoryPartWorldName>c__AnonStorey2A2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__20C(SectionParam p)
            {
                return (p.storyPart == this.story_part);
            }

            internal bool <>m__20D(QuestParam p)
            {
                return (p.iname == this.section_param.releaseKeyQuest);
            }

            internal bool <>m__20E(SectionParam p)
            {
                return (p.iname == this.check_param.world);
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusCoinParam>c__AnonStorey29C
        {
            internal string iname;

            public <GetVersusCoinParam>c__AnonStorey29C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__205(VersusCoinParam x)
            {
                return (x.iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusDraftUnits>c__AnonStorey28F
        {
            internal long schedule_id;

            public <GetVersusDraftUnits>c__AnonStorey28F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1F0(VersusDraftUnitParam vdu)
            {
                return (vdu.Id == this.schedule_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusEnableTime>c__AnonStorey284
        {
            internal long schedule_id;

            public <GetVersusEnableTime>c__AnonStorey284()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1E2(VersusEnableTimeParam VersusET)
            {
                return (((long) VersusET.ScheduleId) == this.schedule_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusRankClass>c__AnonStorey288
        {
            internal int schedule_id;
            internal RankMatchClass current_class;

            public <GetVersusRankClass>c__AnonStorey288()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1E6(VersusRankClassParam vrc)
            {
                return ((vrc.ScheduleId != this.schedule_id) ? 0 : (vrc.Class == this.current_class));
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusRankClassList>c__AnonStorey28A
        {
            internal int schedule_id;

            public <GetVersusRankClassList>c__AnonStorey28A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1E8(VersusRankClassParam vrc)
            {
                return (vrc.ScheduleId == this.schedule_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusRankClassRewardList>c__AnonStorey28C
        {
            internal string reward_id;

            public <GetVersusRankClassRewardList>c__AnonStorey28C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1EC(VersusRankRewardParam reward)
            {
                return (reward.RewardId == this.reward_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusRankMapSchedule>c__AnonStorey286
        {
            internal int schedule_id;

            public <GetVersusRankMapSchedule>c__AnonStorey286()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1E4(VersusEnableTimeParam VersusET)
            {
                return (VersusET.ScheduleId == this.schedule_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusRankMissionList>c__AnonStorey28D
        {
            internal int schedule_id;
            internal List<VersusRankMissionParam> mission_list;
            internal GameManager <>f__this;

            public <GetVersusRankMissionList>c__AnonStorey28D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1ED(VersusRankMissionScheduleParam vrms)
            {
                return (vrms.ScheduleId == this.schedule_id);
            }

            internal void <>m__1EE(VersusRankMissionScheduleParam schedule)
            {
                VersusRankMissionParam param;
                <GetVersusRankMissionList>c__AnonStorey28E storeye;
                storeye = new <GetVersusRankMissionList>c__AnonStorey28E();
                storeye.<>f__ref$653 = this;
                storeye.schedule = schedule;
                param = this.<>f__this.mVersusRankMission.Find(new Predicate<VersusRankMissionParam>(storeye.<>m__20F));
                if (param == null)
                {
                    goto Label_0043;
                }
                this.mission_list.Add(param);
            Label_0043:
                return;
            }

            private sealed class <GetVersusRankMissionList>c__AnonStorey28E
            {
                internal VersusRankMissionScheduleParam schedule;
                internal GameManager.<GetVersusRankMissionList>c__AnonStorey28D <>f__ref$653;

                public <GetVersusRankMissionList>c__AnonStorey28E()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__20F(VersusRankMissionParam vrm)
                {
                    return (vrm.IName == this.schedule.IName);
                }
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusRankParam>c__AnonStorey285
        {
            internal int schedule_id;

            public <GetVersusRankParam>c__AnonStorey285()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1E3(VersusRankParam vr)
            {
                return (vr.Id == this.schedule_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetVersusRankRankingRewardList>c__AnonStorey28B
        {
            internal int schedule_id;

            public <GetVersusRankRankingRewardList>c__AnonStorey28B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1EA(VersusRankRankingRewardParam vrrr)
            {
                return (vrrr.ScheduleId == this.schedule_id);
            }
        }

        [CompilerGenerated]
        private sealed class <GetWeatherSetParam>c__AnonStorey2A0
        {
            internal string iname;

            public <GetWeatherSetParam>c__AnonStorey2A0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__209(WeatherSetParam d)
            {
                return (d.Iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <OnApplicationFocus>c__AnonStorey297
        {
            internal LocalNotificationInfo lparam;

            public <OnApplicationFocus>c__AnonStorey297()
            {
                base..ctor();
                return;
            }

            internal bool <>m__1FF(TrophyParam p)
            {
                return (p.iname == this.lparam.trophy_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <ShowCharacterQuestPopupAsync>c__IteratorD8 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string template;
            internal LoadRequest <req>__0;
            internal List<UnitData>.Enumerator <$s_627>__1;
            internal UnitData <unit>__2;
            internal GameObject <unlockWindow>__3;
            internal UnlockCharacterQuestPopup <popup>__4;
            internal UnitData.CharacterQuestParam <chQuestParam>__5;
            internal int $PC;
            internal object $current;
            internal string <$>template;
            internal GameManager <>f__this;

            public <ShowCharacterQuestPopupAsync>c__IteratorD8()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                uint num;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_003B;

                    case 1:
                        goto Label_003B;

                    case 2:
                        goto Label_0025;
                }
                goto Label_003B;
            Label_0025:
                try
                {
                    goto Label_003B;
                }
                finally
                {
                Label_002A:
                    ((List<UnitData>.Enumerator) this.<$s_627>__1).Dispose();
                }
            Label_003B:
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                bool flag2;
                num = this.$PC;
                this.$PC = -1;
                flag = 0;
                switch (num)
                {
                    case 0:
                        goto Label_0027;

                    case 1:
                        goto Label_00A5;

                    case 2:
                        goto Label_00D8;
                }
                goto Label_01FC;
            Label_0027:
                if (this.<>f__this.mCharacterQuestUnits == null)
                {
                    goto Label_004D;
                }
                if (this.<>f__this.mCharacterQuestUnits.Count > 0)
                {
                    goto Label_005C;
                }
            Label_004D:
                Debug.Log("ShowCharacterQuestPopup: Units Empty");
                goto Label_01FC;
            Label_005C:
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.template);
                if (this.<req>__0 == null)
                {
                    goto Label_00A5;
                }
                if (this.<req>__0.isDone != null)
                {
                    goto Label_00A5;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_01FE;
            Label_00A5:
                if (this.<req>__0 != null)
                {
                    goto Label_00BF;
                }
                Debug.Log("ShowCharacterQuestPopup: Load Template Faild");
                goto Label_01FC;
            Label_00BF:
                this.<$s_627>__1 = this.<>f__this.mCharacterQuestUnits.GetEnumerator();
                num = -3;
            Label_00D8:
                try
                {
                    switch ((num - 2))
                    {
                        case 0:
                            goto Label_01AE;
                    }
                    goto Label_01BF;
                Label_00E9:
                    this.<unit>__2 = &this.<$s_627>__1.Current;
                    this.<unlockWindow>__3 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                    if ((this.<unlockWindow>__3 == null) == null)
                    {
                        goto Label_012B;
                    }
                    goto Label_01BF;
                Label_012B:
                    this.<popup>__4 = this.<unlockWindow>__3.GetComponent<UnlockCharacterQuestPopup>();
                    if ((this.<popup>__4 == null) == null)
                    {
                        goto Label_0152;
                    }
                    goto Label_01BF;
                Label_0152:
                    this.<chQuestParam>__5 = this.<unit>__2.GetCurrentCharaEpisodeData();
                    if (this.<chQuestParam>__5 != null)
                    {
                        goto Label_0183;
                    }
                    Object.Destroy(this.<unlockWindow>__3.get_gameObject());
                    goto Label_01BF;
                Label_0183:
                    DataSource.Bind<UnitData>(this.<popup>__4.get_gameObject(), this.<unit>__2);
                Label_0199:
                    this.$current = null;
                    this.$PC = 2;
                    flag = 1;
                    goto Label_01FE;
                Label_01AE:
                    if ((this.<popup>__4 != null) != null)
                    {
                        goto Label_0199;
                    }
                Label_01BF:
                    if (&this.<$s_627>__1.MoveNext() != null)
                    {
                        goto Label_00E9;
                    }
                    goto Label_01E9;
                }
                finally
                {
                Label_01D4:
                    if (flag == null)
                    {
                        goto Label_01D8;
                    }
                Label_01D8:
                    ((List<UnitData>.Enumerator) this.<$s_627>__1).Dispose();
                }
            Label_01E9:
                this.<>f__this.mCharacterQuestUnits = null;
                this.$PC = -1;
            Label_01FC:
                return 0;
            Label_01FE:
                return 1;
                return flag2;
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
        private sealed class <SkinUnlockPopup>c__IteratorD9 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string <title>__0;
            internal ArtifactParam unlockSkin;
            internal string <text>__1;
            internal GameObject <window>__2;
            internal int $PC;
            internal object $current;
            internal ArtifactParam <$>unlockSkin;

            public <SkinUnlockPopup>c__IteratorD9()
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
                object[] objArray1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0088;
                }
                goto Label_00A0;
            Label_0021:
                this.<title>__0 = LocalizedText.Get("sys.UNITLIST_SKIN_GET_TITLE");
                objArray1 = new object[] { this.unlockSkin.name };
                this.<text>__1 = LocalizedText.Get("sys.UNITLIST_SKIN_GET", objArray1);
                this.<window>__2 = UIUtility.SystemMessage(this.<title>__0, this.<text>__1, null, null, 0, -1);
                goto Label_0088;
            Label_0075:
                this.$current = null;
                this.$PC = 1;
                goto Label_00A2;
            Label_0088:
                if ((this.<window>__2 != null) != null)
                {
                    goto Label_0075;
                }
                this.$PC = -1;
            Label_00A0:
                return 0;
            Label_00A2:
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
        private sealed class <SkinUnlockPopup>c__IteratorDA : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal ItemParam[] rewardItems;
            internal GameManager <gm>__0;
            internal string <title>__1;
            internal bool <isShow>__2;
            internal int <i>__3;
            internal ItemParam <item>__4;
            internal string <text>__5;
            internal GameObject <window>__6;
            internal int $PC;
            internal object $current;
            internal ItemParam[] <$>rewardItems;

            public <SkinUnlockPopup>c__IteratorDA()
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
                object[] objArray1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0113;
                }
                goto Label_016D;
            Label_0021:
                if (this.rewardItems != null)
                {
                    goto Label_003F;
                }
                if (((int) this.rewardItems.Length) > 0)
                {
                    goto Label_003F;
                }
                goto Label_016D;
            Label_003F:
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<title>__1 = LocalizedText.Get("sys.UNITLIST_SKIN_GET_TITLE");
                UIUtility.PushCanvas(0, -1);
                this.<isShow>__2 = 0;
                this.<i>__3 = 0;
                goto Label_0132;
            Label_0075:
                this.<item>__4 = this.rewardItems[this.<i>__3];
                if (this.<item>__4 != null)
                {
                    goto Label_0098;
                }
                goto Label_0124;
            Label_0098:
                if (this.<item>__4.type != 15)
                {
                    goto Label_0124;
                }
                if (this.<isShow>__2 != null)
                {
                    goto Label_00BC;
                }
                this.<isShow>__2 = 1;
            Label_00BC:
                objArray1 = new object[] { this.<item>__4.name };
                this.<text>__5 = LocalizedText.Get("sys.UNITLIST_SKIN_GET", objArray1);
                this.<window>__6 = UIUtility.SystemMessage(this.<title>__1, this.<text>__5, null, null, 0, -1);
                goto Label_0113;
            Label_0100:
                this.$current = null;
                this.$PC = 1;
                goto Label_016F;
            Label_0113:
                if ((this.<window>__6 != null) != null)
                {
                    goto Label_0100;
                }
            Label_0124:
                this.<i>__3 += 1;
            Label_0132:
                if (this.<i>__3 < ((int) this.rewardItems.Length))
                {
                    goto Label_0075;
                }
                if (this.<isShow>__2 == null)
                {
                    goto Label_0161;
                }
                this.<gm>__0.Player.ClearItemFlags(2);
            Label_0161:
                UIUtility.PopCanvas();
                this.$PC = -1;
            Label_016D:
                return 0;
            Label_016F:
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
        private sealed class <StartBuyStaminaSequence2>c__IteratorD4 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal Canvas <canvas>__1;
            internal GameObject <h_obj>__2;
            internal HealAp <heal_ap>__3;
            internal PartyWindow2 _pwindow;
            internal int $PC;
            internal object $current;
            internal PartyWindow2 <$>_pwindow;

            public <StartBuyStaminaSequence2>c__IteratorD4()
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
                        goto Label_003C;

                    case 2:
                        goto Label_0079;

                    case 3:
                        goto Label_014C;
                }
                goto Label_0174;
            Label_0029:
                this.$current = null;
                this.$PC = 1;
                goto Label_0176;
            Label_003C:
                this.<req>__0 = AssetManager.LoadAsync<GameObject>("UI/HealAP");
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0079;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 2;
                goto Label_0176;
            Label_0079:
                if (this.<req>__0 == null)
                {
                    goto Label_016D;
                }
                if ((this.<req>__0.asset != null) == null)
                {
                    goto Label_016D;
                }
                this.<canvas>__1 = UIUtility.PushCanvas(0, -1);
                if ((this.<canvas>__1 != null) == null)
                {
                    goto Label_015D;
                }
                this.<h_obj>__2 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                if ((this.<h_obj>__2 != null) == null)
                {
                    goto Label_015D;
                }
                this.<h_obj>__2.get_transform().SetParent(this.<canvas>__1.get_transform(), 0);
                this.<heal_ap>__3 = this.<h_obj>__2.GetComponent<HealAp>();
                if ((this.<heal_ap>__3 != null) == null)
                {
                    goto Label_015D;
                }
                this.<heal_ap>__3.Refresh(1, this._pwindow);
                goto Label_014C;
            Label_0139:
                this.$current = null;
                this.$PC = 3;
                goto Label_0176;
            Label_014C:
                if ((this.<heal_ap>__3 != null) != null)
                {
                    goto Label_0139;
                }
            Label_015D:
                Object.Destroy(this.<canvas>__1.get_gameObject());
            Label_016D:
                this.$PC = -1;
            Label_0174:
                return 0;
            Label_0176:
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
        private sealed class <UpdateUnitsBadges>c__IteratorD2 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<UnitData> <units>__0;
            internal int <i>__1;
            internal int <count>__2;
            internal int <i>__3;
            internal int $PC;
            internal object $current;
            internal GameManager <>f__this;

            public <UpdateUnitsBadges>c__IteratorD2()
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
                int num2;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_00B3;
                }
                goto Label_016B;
            Label_0021:
                this.<units>__0 = this.<>f__this.Player.Units;
                this.<i>__1 = 0;
                this.<count>__2 = 0;
                goto Label_00C1;
            Label_004A:
                this.<>f__this.IsBusyUpdateBadges |= 1;
                this.<units>__0[this.<i>__1].UpdateBadge();
                if ((this.<count>__2 += 1) >= 50)
                {
                    goto Label_0090;
                }
                goto Label_00B3;
            Label_0090:
                this.<count>__2 = 0;
                this.$current = new WaitForSeconds(0.1f);
                this.$PC = 1;
                goto Label_016D;
            Label_00B3:
                this.<i>__1 += 1;
            Label_00C1:
                if (this.<i>__1 < this.<units>__0.Count)
                {
                    goto Label_004A;
                }
                this.<>f__this.BadgeFlags &= -2;
                this.<i>__3 = 0;
                goto Label_013A;
            Label_00F7:
                if ((this.<units>__0[this.<i>__3].BadgeState & 1) == null)
                {
                    goto Label_012C;
                }
                this.<>f__this.BadgeFlags |= 1;
                goto Label_0150;
            Label_012C:
                this.<i>__3 += 1;
            Label_013A:
                if (this.<i>__3 < this.<units>__0.Count)
                {
                    goto Label_00F7;
                }
            Label_0150:
                this.<>f__this.IsBusyUpdateBadges &= -2;
                this.$PC = -1;
            Label_016B:
                return 0;
            Label_016D:
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
        private sealed class <UpdateUnitUnlockBadges>c__IteratorD3 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal UnitParam[] <units>__0;
            internal int <i>__1;
            internal int <count>__2;
            internal int $PC;
            internal object $current;
            internal GameManager <>f__this;

            public <UpdateUnitUnlockBadges>c__IteratorD3()
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
                int num2;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0114;
                }
                goto Label_01AB;
            Label_0021:
                this.<units>__0 = this.<>f__this.MasterParam.GetAllUnits();
                if (this.<units>__0 == null)
                {
                    goto Label_0190;
                }
                if (this.<>f__this.mBadgeUnitUnlocks != null)
                {
                    goto Label_006A;
                }
                this.<>f__this.mBadgeUnitUnlocks = new List<UnitParam>((int) this.<units>__0.Length);
            Label_006A:
                this.<>f__this.mBadgeUnitUnlocks.Clear();
                this.<i>__1 = 0;
                this.<count>__2 = 0;
                goto Label_0122;
            Label_008D:
                this.<>f__this.IsBusyUpdateBadges |= 2;
                if (this.<units>__0[this.<i>__1].CheckEnableUnlock() == null)
                {
                    goto Label_00D4;
                }
                this.<>f__this.mBadgeUnitUnlocks.Add(this.<units>__0[this.<i>__1]);
            Label_00D4:
                if ((this.<count>__2 += 1) >= 100)
                {
                    goto Label_00F1;
                }
                goto Label_0114;
            Label_00F1:
                this.<count>__2 = 0;
                this.$current = new WaitForSeconds(0.1f);
                this.$PC = 1;
                goto Label_01AD;
            Label_0114:
                this.<i>__1 += 1;
            Label_0122:
                if (this.<i>__1 < ((int) this.<units>__0.Length))
                {
                    goto Label_008D;
                }
                if (this.<>f__this.mBadgeUnitUnlocks.Count <= 0)
                {
                    goto Label_0163;
                }
                this.<>f__this.BadgeFlags |= 2;
                goto Label_0177;
            Label_0163:
                this.<>f__this.BadgeFlags &= -3;
            Label_0177:
                this.<>f__this.IsBusyUpdateBadges &= -3;
                goto Label_01A4;
            Label_0190:
                this.<>f__this.BadgeFlags &= -3;
            Label_01A4:
                this.$PC = -1;
            Label_01AB:
                return 0;
            Label_01AD:
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
        private sealed class <WaitDownloadAsync>c__IteratorD7 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal AssetDownloader.DownloadState state;
            internal int <i>__0;
            internal List<AssetList.Item> queue;
            internal int $PC;
            internal object $current;
            internal AssetDownloader.DownloadState <$>state;
            internal List<AssetList.Item> <$>queue;
            internal GameManager <>f__this;

            public <WaitDownloadAsync>c__IteratorD7()
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
                        goto Label_00CE;
                }
                goto Label_00D5;
            Label_0025:
                goto Label_003D;
            Label_002A:
                this.$current = null;
                this.$PC = 1;
                goto Label_00D7;
            Label_003D:
                if (this.state.Finished == null)
                {
                    goto Label_002A;
                }
                if (this.state.HasError != null)
                {
                    goto Label_00AF;
                }
                this.<i>__0 = 0;
                goto Label_0099;
            Label_0069:
                this.<>f__this.mTutorialDLAssets.Remove(this.queue[this.<i>__0]);
                this.<i>__0 += 1;
            Label_0099:
                if (this.<i>__0 < this.queue.Count)
                {
                    goto Label_0069;
                }
            Label_00AF:
                this.<>f__this.mWaitDownloadThread = null;
                this.$current = null;
                this.$PC = 2;
                goto Label_00D7;
            Label_00CE:
                this.$PC = -1;
            Label_00D5:
                return 0;
            Label_00D7:
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

        [Flags]
        public enum BadgeTypes
        {
            Unit = 1,
            UnitUnlock = 2,
            GoldGacha = 4,
            RareGacha = 8,
            DailyMission = 0x10,
            Arena = 0x20,
            Multiplay = 0x40,
            Friend = 0x80,
            GiftBox = 0x100,
            ItemEquipment = 0x200,
            All = -1
        }

        public delegate void BuyCoinEvent();

        public delegate void DayChangeEvent();

        public delegate void PlayerCurrencyChangeEvent();

        public delegate void PlayerLvChangeEvent();

        public delegate void RankUpCountChangeEvent(int count);

        public delegate bool SceneChangeEvent();

        public delegate void StaminaChangeEvent();

        private class TextureRequest
        {
            public RawImage Target;
            public string Path;
            public LoadRequest Request;

            public TextureRequest()
            {
                base..ctor();
                return;
            }
        }

        private class VersusRange
        {
            public int min;
            public int max;

            public VersusRange(int _min, int _max)
            {
                base..ctor();
                this.min = _min;
                this.max = _max;
                return;
            }
        }
    }
}

