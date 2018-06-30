namespace SRPG
{
    using GR;
    using Gsc.Purchase;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;

    public class PlayerData
    {
        public const string SAIGONI_HOME_HIRAITA_LV = "lastplv";
        public const string SAIGONI_HOME_HIRAITA_VIPLV = "lastviplv";
        public const int INVENTORY_SIZE = 5;
        public static readonly int INI_UNIT_CAPACITY;
        public static readonly int MAX_UNIT_CAPACITY;
        private static readonly string PLAYRE_DATA_VERSION;
        public static readonly string TEAM_ID_KEY;
        public static readonly string MULTI_PLAY_TEAM_ID_KEY;
        public static readonly string ARENA_TEAM_ID_KEY;
        public static readonly string ROOM_COMMENT_KEY;
        public static readonly string VERSUS_ID_KEY;
        private string mName;
        private string mCuid;
        private string mFuid;
        private string mTuid;
        private long mTuidExpiredAt;
        private int mLoginCount;
        private OInt mNewGameAt;
        private OInt mLv;
        private OInt mExp;
        private OInt mGold;
        private OInt mFreeCoin;
        private OInt mPaidCoin;
        private OInt mComCoin;
        private OInt mTourCoin;
        private OInt mArenaCoin;
        private OInt mMultiCoin;
        private OInt mAbilityPoint;
        private OInt mPiecePoint;
        private OInt mVipRank;
        private OInt mVipPoint;
        private OLong mVipExpiredAt;
        private List<EventCoinData> mEventCoinList;
        private TimeRecoveryValue mStamina;
        private TimeRecoveryValue mCaveStamina;
        private TimeRecoveryValue mAbilityRankUpCount;
        public int mArenaResetCount;
        public DateTime LoginDate;
        public long TutorialFlags;
        private Json_LoginBonus[] mLoginBonus;
        private int mLoginBonusCount;
        private bool mFirstLogin;
        private Dictionary<string, Json_LoginBonusTable> mLoginBonusTables;
        private Json_LoginBonusTable mLoginBonus28days;
        private OInt mChallengeMultiNum;
        private OInt mStaminaBuyNum;
        private OInt mGoldBuyNum;
        private OInt mChallengeArenaNum;
        private TimeRecoveryValue mChallengeArenaTimer;
        private OInt mTourNum;
        private OInt mUnitCap;
        private int mArenaRank;
        private int mBestArenaRank;
        private DateTime mArenaLastAt;
        private int mArenaSeed;
        private int mArenaMaxActionNum;
        private DateTime mArenaEndAt;
        private List<UnitData> mUnits;
        private List<ItemData> mItems;
        private List<PartyData> mPartys;
        private List<ArtifactData> mArtifacts;
        private Dictionary<string, Dictionary<int, int>> mArtifactsNumByRarity;
        private List<ConceptCardData> mConceptCards;
        private Dictionary<string, int> mConceptCardNum;
        private List<ConceptCardMaterialData> mConceptCardExpMaterials;
        private List<ConceptCardMaterialData> mConceptCardTrustMaterials;
        private List<string> mSkins;
        private Dictionary<long, UnitData> mUniqueID2UnitData;
        private Dictionary<string, ItemData> mID2ItemData;
        private List<TrophyState> mTrophyStates;
        private Dictionary<string, List<TrophyState>> mTrophyStatesInameDict;
        private ShopData[] mShops;
        private LimitedShopData mLimitedShops;
        private EventShopData mEventShops;
        private List<SRPG.RankMatchMissionState> mRankMatchMissionState;
        public RankMatchSeasonResult mRankMatchSeasonResult;
        private List<QuestParam> mAvailableQuests;
        private bool mQuestListDirty;
        public List<FriendData> Friends;
        public List<FriendData> FriendsFollower;
        public List<FriendData> FriendsFollow;
        public int mFriendNum;
        public int mFollowerNum;
        public List<string> mFollowerUID;
        public List<MultiFuid> MultiFuids;
        public List<SupportData> Supports;
        public SRPG.FriendPresentWishList FriendPresentWishList;
        public SRPG.FriendPresentReceiveList FriendPresentReceiveList;
        public List<MailData> Mails;
        public List<MailData> CurrentMails;
        public MailPageData MailPage;
        public OInt mUnlocks;
        public FreeGacha FreeGachaGold;
        public FreeGacha FreeGachaCoin;
        public SRPG.PaidGacha PaidGacha;
        public Dictionary<string, PaymentInfo> PaymentInfos;
        private bool mUnreadMailPeriod;
        private bool mUnreadMail;
        private bool mValidGpsGift;
        private bool mValidFriendPresent;
        private string mSelectedAward;
        private List<string> mHaveAward;
        private int mVersusPoint;
        private int[] mVersusWinCount;
        private int[] mVersusTotalCount;
        private int mVersusFreeWin;
        private int mVersusRankWin;
        private int mVersusFriendWin;
        private int mVersusTwFloor;
        private int mVersusTwKey;
        private int mVersusTwWinCnt;
        private bool mVersusSeasonGift;
        private int mRankMatchRank;
        private int mRankMatchScore;
        private SRPG.RankMatchClass mRankMatchClass;
        private int mRankMatchBattlePoint;
        private int mRankMatchStreakWin;
        private int mRankMatchOldRank;
        private int mRankMatchOldScore;
        private SRPG.RankMatchClass mRankMatchOldClass;
        private bool mMultiInvitaionFlag;
        private string mMultiInvitaionComment;
        private int mFirstFriendCount;
        private int mFirstChargeStatus;
        private long mGuerrillaShopStart;
        private long mGuerrillaShopEnd;
        private bool mIsGuerrillaShopStarted;
        public int SupportCount;
        public int SupportGold;
        private ItemData[] mInventory;
        private long mMissionClearAt;
        private Dictionary<string, int> mConsumeMaterials;
        private int mCreateItemCost;
        private int mPrevCheckHour;
        private UpdateTrophyInterval mUpdateInterval;
        private Queue<string> mLoginBonusQueue;
        [CompilerGenerated]
        private BtlResultTypes <RankMatchResult>k__BackingField;
        [CompilerGenerated]
        private int <RankMatchWinCount>k__BackingField;
        [CompilerGenerated]
        private int <RankMatchLoseCount>k__BackingField;
        [CompilerGenerated]
        private static Predicate<ItemData> <>f__am$cache88;
        [CompilerGenerated]
        private static Comparison<TrophyState> <>f__am$cache89;
        [CompilerGenerated]
        private static Func<ItemData, bool> <>f__am$cache8A;
        [CompilerGenerated]
        private static Predicate<ConceptCardMaterialData> <>f__am$cache8B;
        [CompilerGenerated]
        private static Predicate<ConceptCardMaterialData> <>f__am$cache8C;

        static PlayerData()
        {
            INI_UNIT_CAPACITY = 20;
            MAX_UNIT_CAPACITY = 50;
            PLAYRE_DATA_VERSION = "38.0";
            TEAM_ID_KEY = "TeamID";
            MULTI_PLAY_TEAM_ID_KEY = "MultiPlayTeamID";
            ARENA_TEAM_ID_KEY = "ArenaTeamID";
            ROOM_COMMENT_KEY = "MultiPlayRoomComment";
            VERSUS_ID_KEY = "VERSUS_PLACEMENT_";
            return;
        }

        public PlayerData()
        {
            int num;
            PartyData data;
            this.mNewGameAt = 0;
            this.mLv = 0;
            this.mExp = 0;
            this.mGold = 0;
            this.mFreeCoin = 0;
            this.mPaidCoin = 0;
            this.mComCoin = 0;
            this.mTourCoin = 0;
            this.mArenaCoin = 0;
            this.mMultiCoin = 0;
            this.mAbilityPoint = 0;
            this.mPiecePoint = 0;
            this.mVipRank = 0;
            this.mVipPoint = 0;
            this.mVipExpiredAt = 0L;
            this.mEventCoinList = new List<EventCoinData>();
            this.mStamina = new TimeRecoveryValue();
            this.mCaveStamina = new TimeRecoveryValue();
            this.mAbilityRankUpCount = new TimeRecoveryValue();
            this.mLoginBonusTables = new Dictionary<string, Json_LoginBonusTable>();
            this.mChallengeMultiNum = 0;
            this.mStaminaBuyNum = 0;
            this.mGoldBuyNum = 0;
            this.mChallengeArenaNum = 0;
            this.mChallengeArenaTimer = new TimeRecoveryValue();
            this.mTourNum = 0;
            this.mUnitCap = INI_UNIT_CAPACITY;
            this.mArenaLastAt = GameUtility.UnixtimeToLocalTime(0L);
            this.mArenaEndAt = GameUtility.UnixtimeToLocalTime(0L);
            this.mArtifacts = new List<ArtifactData>();
            this.mArtifactsNumByRarity = new Dictionary<string, Dictionary<int, int>>();
            this.mConceptCards = new List<ConceptCardData>();
            this.mConceptCardNum = new Dictionary<string, int>();
            this.mConceptCardExpMaterials = new List<ConceptCardMaterialData>();
            this.mConceptCardTrustMaterials = new List<ConceptCardMaterialData>();
            this.mSkins = new List<string>();
            this.mUniqueID2UnitData = new Dictionary<long, UnitData>();
            this.mID2ItemData = new Dictionary<string, ItemData>();
            this.mTrophyStates = new List<TrophyState>(0x20);
            this.mTrophyStatesInameDict = new Dictionary<string, List<TrophyState>>();
            this.mShops = new ShopData[(int) Enum.GetNames(typeof(EShopType)).Length];
            this.mLimitedShops = new LimitedShopData();
            this.mEventShops = new EventShopData();
            this.mRankMatchMissionState = new List<SRPG.RankMatchMissionState>();
            this.mRankMatchSeasonResult = new RankMatchSeasonResult();
            this.mAvailableQuests = new List<QuestParam>();
            this.mQuestListDirty = 1;
            this.Friends = new List<FriendData>();
            this.FriendsFollower = new List<FriendData>();
            this.FriendsFollow = new List<FriendData>();
            this.mFollowerUID = new List<string>();
            this.MultiFuids = new List<MultiFuid>();
            this.Supports = new List<SupportData>();
            this.FriendPresentWishList = new SRPG.FriendPresentWishList();
            this.FriendPresentReceiveList = new SRPG.FriendPresentReceiveList();
            this.Mails = new List<MailData>();
            this.CurrentMails = new List<MailData>();
            this.mUnlocks = 0;
            this.FreeGachaGold = new FreeGacha();
            this.FreeGachaCoin = new FreeGacha();
            this.PaidGacha = new SRPG.PaidGacha();
            this.PaymentInfos = new Dictionary<string, PaymentInfo>();
            this.mHaveAward = new List<string>();
            this.mVersusWinCount = new int[4];
            this.mVersusTotalCount = new int[4];
            this.mFirstChargeStatus = -1;
            this.mInventory = new ItemData[5];
            this.mMissionClearAt = -1L;
            this.mConsumeMaterials = new Dictionary<string, int>(0x10);
            this.mPrevCheckHour = -1;
            this.mUpdateInterval = new UpdateTrophyInterval();
            this.mLoginBonusQueue = new Queue<string>();
            base..ctor();
            this.LoginDate = DateTime.Now;
            this.mPartys = new List<PartyData>(11);
            num = 0;
            goto Label_039B;
        Label_0365:
            data = new PartyData(num);
            data.Name = "パーティ" + ((int) (num + 1));
            data.PartyType = num;
            this.mPartys.Add(data);
            num += 1;
        Label_039B:
            if (num < 11)
            {
                goto Label_0365;
            }
            return;
        }

        [CompilerGenerated]
        private static int <AddTrophyStateDict>m__149(TrophyState a, TrophyState b)
        {
            return (a.StartYMD - b.StartYMD);
        }

        [CompilerGenerated]
        private static bool <CheckEnableConvertGold>m__144(ItemData item)
        {
            return ((item.ItemType != 9) ? 0 : (item.Num > 0));
        }

        [CompilerGenerated]
        private static bool <IsHaveConceptCardExpMaterial>m__157(ConceptCardMaterialData p)
        {
            return (p.Num > 0);
        }

        [CompilerGenerated]
        private static bool <IsHaveConceptCardTrustMaterial>m__158(ConceptCardMaterialData p)
        {
            return (p.Num > 0);
        }

        [CompilerGenerated]
        private static bool <IsHaveHealAPItems>m__156(ItemData x)
        {
            return (x.ItemType == 20);
        }

        private unsafe void AddArtifaceNumByRarity(ArtifactData item)
        {
            Dictionary<int, int> dictionary;
            int num;
            Dictionary<int, int> dictionary2;
            if (this.mArtifactsNumByRarity.TryGetValue(item.ArtifactParam.iname, &dictionary) == null)
            {
                goto Label_0065;
            }
            if (dictionary.TryGetValue(item.Rarity, &num) == null)
            {
                goto Label_004E;
            }
            dictionary[item.Rarity] = num + 1;
            goto Label_0060;
        Label_004E:
            dictionary.Add(item.Rarity, 1);
        Label_0060:
            goto Label_0094;
        Label_0065:
            dictionary2 = new Dictionary<int, int>();
            dictionary2.Add(item.Rarity, 1);
            this.mArtifactsNumByRarity.Add(item.ArtifactParam.iname, dictionary2);
        Label_0094:
            return;
        }

        private void AddArtifact(ArtifactData item)
        {
            this.mArtifacts.Add(item);
            this.AddArtifaceNumByRarity(item);
            return;
        }

        public void AddPaymentInfo(string productId, int num)
        {
            if (this.PaymentInfos.ContainsKey(productId) == null)
            {
                goto Label_0028;
            }
            this.PaymentInfos[productId].AddNum(num);
            goto Label_003B;
        Label_0028:
            this.PaymentInfos.Add(productId, new PaymentInfo(productId, num));
        Label_003B:
            return;
        }

        public void AddTrophyCounter(TrophyObjective obj, int value)
        {
            this.AddTrophyCounter(obj.Param, obj.index, value);
            return;
        }

        public void AddTrophyCounter(TrophyParam trophyParam, int countIndex, int value)
        {
            bool flag;
            if (this.AddTrophyCounterExec(trophyParam, countIndex, value) == null)
            {
                goto Label_0016;
            }
            this.DailyAllCompleteCheck();
        Label_0016:
            return;
        }

        private unsafe bool AddTrophyCounterExec(TrophyParam trophyParam, int countIndex, int value)
        {
            TrophyState state;
            int num;
            state = null;
            if (this.CheckTrophyCount(trophyParam, countIndex, value, &state) != null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            num = state.Count[countIndex];
            *((int*) &(state.Count[countIndex])) += value;
            if (this.CheckDailyMissionDayChange(state, countIndex) != null)
            {
                goto Label_0046;
            }
            state.Count[countIndex] = num;
            return 0;
        Label_0046:
            state.IsDirty = 1;
            MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncNow();
            if (state.IsCompleted == null)
            {
                goto Label_0069;
            }
            return 1;
        Label_0069:
            return 0;
        }

        private void AddTrophyStateDict(TrophyState _state)
        {
            this.mTrophyStates.Add(_state);
            if (this.mTrophyStatesInameDict.ContainsKey(_state.iname) != null)
            {
                goto Label_0038;
            }
            this.mTrophyStatesInameDict.Add(_state.iname, new List<TrophyState>());
        Label_0038:
            this.mTrophyStatesInameDict[_state.iname].Add(_state);
            if (<>f__am$cache89 != null)
            {
                goto Label_0078;
            }
            <>f__am$cache89 = new Comparison<TrophyState>(PlayerData.<AddTrophyStateDict>m__149);
        Label_0078:
            this.mTrophyStatesInameDict[_state.iname].Sort(<>f__am$cache89);
            return;
        }

        public void AddVersusTotalCount(VERSUS_TYPE type, int addcnt)
        {
            VERSUS_TYPE versus_type;
            versus_type = type;
            switch (versus_type)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0019;

                case 2:
                    goto Label_0019;
            }
            goto Label_0030;
        Label_0019:
            this.mVersusTotalCount[type] = addcnt + this.mVersusTotalCount[type];
        Label_0030:
            return;
        }

        public void AutoSetLeaderUnit()
        {
            List<UnitData> list;
            int num;
            PartyData data;
            int num2;
            UnitData data2;
            bool flag;
            int num3;
            list = MonoSingleton<GameManager>.Instance.Player.Units;
            if (list.Count > 0)
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            num = 0;
            goto Label_00CB;
        Label_0024:
            data = this.mPartys[num];
            if (data.GetUnitUniqueID(0) == null)
            {
                goto Label_0042;
            }
            goto Label_00C7;
        Label_0042:
            num2 = 0;
            goto Label_00BB;
        Label_0049:
            data2 = list[num2];
            if (data2 != null)
            {
                goto Label_005E;
            }
            goto Label_00B7;
        Label_005E:
            flag = 0;
            num3 = 0;
            goto Label_008B;
        Label_0069:
            if (data.GetUnitUniqueID(num3) != data2.UniqueID)
            {
                goto Label_0085;
            }
            flag = 1;
            goto Label_0098;
        Label_0085:
            num3 += 1;
        Label_008B:
            if (num3 < data.MAX_UNIT)
            {
                goto Label_0069;
            }
        Label_0098:
            if (flag == null)
            {
                goto Label_00A4;
            }
            goto Label_00B7;
        Label_00A4:
            data.SetUnitUniqueID(0, data2.UniqueID);
            goto Label_00C7;
        Label_00B7:
            num2 += 1;
        Label_00BB:
            if (num2 < list.Count)
            {
                goto Label_0049;
            }
        Label_00C7:
            num += 1;
        Label_00CB:
            if (num < this.mPartys.Count)
            {
                goto Label_0024;
            }
            return;
        }

        public bool AwakingUnit(UnitData unit)
        {
            int num;
            if (unit.CheckUnitAwaking() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            num = unit.GetAwakeNeedPieces();
            if (unit.UnitAwaking() != null)
            {
                goto Label_0021;
            }
            return 0;
        Label_0021:
            this.ConsumeAwakePieces(unit, num);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(2);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
            return 1;
        }

        public int CalcLevel()
        {
            return CalcLevelFromExp(this.mExp);
        }

        public static int CalcLevelFromExp(int current)
        {
            MasterParam param;
            int num;
            int num2;
            int num3;
            int num4;
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num = param.GetPlayerLevelCap();
            num2 = 0;
            num3 = 0;
            num4 = 0;
            goto Label_0043;
        Label_001E:
            num2 += param.GetPlayerNextExp(num4 + 1);
            if (num2 > current)
            {
                goto Label_003B;
            }
            num3 += 1;
            goto Label_003D;
        Label_003B:
            return num3;
        Label_003D:
            num4 += 1;
        Label_0043:
            if (num4 < num)
            {
                goto Label_001E;
            }
            return Math.Min(Math.Max(num3, 1), num);
        }

        public static int CalcVipRankFromPoint(int current)
        {
            MasterParam param;
            int num;
            int num2;
            int num3;
            int num4;
            if (current != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num = 0;
            num2 = 0;
            num3 = param.GetVipRankCap();
            num4 = 0;
            goto Label_0049;
        Label_0026:
            num += param.GetVipRankNextPoint(num4 + 1);
            if (num > current)
            {
                goto Label_0043;
            }
            num2 += 1;
        Label_0043:
            num4 += 1;
        Label_0049:
            if (num4 < num3)
            {
                goto Label_0026;
            }
            return num2;
        }

        public bool ChallengeArena()
        {
            if (this.ChallengeArenaNum < this.ChallengeArenaMax)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            this.mChallengeArenaNum = OInt.op_Decrement(this.mChallengeArenaNum);
            this.mChallengeArenaTimer.val = 0;
            this.mChallengeArenaTimer.at = Network.GetServerTime();
            return 1;
        }

        private void CheckAllCompleteMissionTrophy()
        {
            GameManager manager;
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            QuestParam param;
            manager = MonoSingleton<GameManager>.Instance;
            objectiveArray = manager.GetTrophiesOfType(100);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_006A;
        Label_001A:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) == null)
            {
                goto Label_0033;
            }
            goto Label_0066;
        Label_0033:
            param = manager.FindQuest(objective.sval_base);
            if (param != null)
            {
                goto Label_004D;
            }
            goto Label_0066;
        Label_004D:
            if (param.IsMissionCompleteALL() != null)
            {
                goto Label_005E;
            }
            goto Label_0066;
        Label_005E:
            this.AddTrophyCounter(objective, 1);
        Label_0066:
            num -= 1;
        Label_006A:
            if (num >= 0)
            {
                goto Label_001A;
            }
            return;
        }

        private void CheckAllConceptCardLevelupTrophy()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyState state;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x53);
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x54);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_00DE;
        Label_0025:
            objective = objectiveArray[num];
            num2 = 0;
            goto Label_00C9;
        Label_0030:
            if (string.Equals(objective.sval_base, this.ConceptCards[num2].Param.iname) == null)
            {
                goto Label_00C5;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(objective.Param, 0);
            if (state == null)
            {
                goto Label_00C5;
            }
            if (((int) state.Count.Length) <= 0)
            {
                goto Label_00C5;
            }
            if (state.Count[0] > this.ConceptCards[num2].Lv)
            {
                goto Label_00C5;
            }
            this.SetTrophyCounter(objective, this.ConceptCards[num2].Lv);
        Label_00C5:
            num2 += 1;
        Label_00C9:
            if (num2 < this.ConceptCards.Count)
            {
                goto Label_0030;
            }
            num -= 1;
        Label_00DE:
            if (num >= 0)
            {
                goto Label_0025;
            }
            return;
        }

        public void CheckAllConceptCardLimitBreakTrophy()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyState state;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x56);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_00D1;
        Label_0018:
            objective = objectiveArray[num];
            num2 = 0;
            goto Label_00BC;
        Label_0023:
            if (string.Equals(objective.sval_base, this.ConceptCards[num2].Param.iname) == null)
            {
                goto Label_00B8;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(objective.Param, 0);
            if (state == null)
            {
                goto Label_00B8;
            }
            if (((int) state.Count.Length) <= 0)
            {
                goto Label_00B8;
            }
            if (state.Count[0] > this.ConceptCards[num2].AwakeCount)
            {
                goto Label_00B8;
            }
            this.SetTrophyCounter(objective, this.ConceptCards[num2].AwakeCount);
        Label_00B8:
            num2 += 1;
        Label_00BC:
            if (num2 < this.ConceptCards.Count)
            {
                goto Label_0023;
            }
            num -= 1;
        Label_00D1:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        private void CheckAllConceptCardTrustMaxTrophy()
        {
            TrophyObjective[] objectiveArray;
            int num;
            int num2;
            TrophyObjective objective;
            int num3;
            int num4;
            TrophyState state;
            int num5;
            int num6;
            TrophyState state2;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x59);
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_0190;
        Label_0032:
            objective = objectiveArray[num2];
            if (string.IsNullOrEmpty(objective.sval_base) == null)
            {
                goto Label_00D8;
            }
            num3 = 0;
            num4 = 0;
            goto Label_007A;
        Label_0051:
            if (this.ConceptCards[num4].Trust < num)
            {
                goto Label_0074;
            }
            num3 += 1;
        Label_0074:
            num4 += 1;
        Label_007A:
            if (num4 < this.ConceptCards.Count)
            {
                goto Label_0051;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(objective.Param, 0);
            if (state == null)
            {
                goto Label_018C;
            }
            if (((int) state.Count.Length) <= 0)
            {
                goto Label_018C;
            }
            if (state.Count[0] > num3)
            {
                goto Label_018C;
            }
            this.SetTrophyCounter(objective, num3);
            goto Label_018C;
        Label_00D8:
            num5 = 0;
            num6 = 0;
            goto Label_0133;
        Label_00E3:
            if ((objective.sval_base == this.ConceptCards[num6].Param.iname) == null)
            {
                goto Label_012D;
            }
            if (this.ConceptCards[num6].Trust < num)
            {
                goto Label_012D;
            }
            num5 += 1;
        Label_012D:
            num6 += 1;
        Label_0133:
            if (num6 < this.ConceptCards.Count)
            {
                goto Label_00E3;
            }
            state2 = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(objective.Param, 0);
            if (state2 == null)
            {
                goto Label_018C;
            }
            if (((int) state2.Count.Length) <= 0)
            {
                goto Label_018C;
            }
            if (state2.Count[0] > num5)
            {
                goto Label_018C;
            }
            this.SetTrophyCounter(objective, num5);
        Label_018C:
            num2 -= 1;
        Label_0190:
            if (num2 >= 0)
            {
                goto Label_0032;
            }
            return;
        }

        public void CheckAllConceptCardTrustUpTrophy()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyState state;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x58);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_00D1;
        Label_0018:
            objective = objectiveArray[num];
            num2 = 0;
            goto Label_00BC;
        Label_0023:
            if (string.Equals(objective.sval_base, this.ConceptCards[num2].Param.iname) == null)
            {
                goto Label_00B8;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(objective.Param, 0);
            if (state == null)
            {
                goto Label_00B8;
            }
            if (((int) state.Count.Length) <= 0)
            {
                goto Label_00B8;
            }
            if (state.Count[0] > this.ConceptCards[num2].Trust)
            {
                goto Label_00B8;
            }
            this.SetTrophyCounter(objective, this.ConceptCards[num2].Trust);
        Label_00B8:
            num2 += 1;
        Label_00BC:
            if (num2 < this.ConceptCards.Count)
            {
                goto Label_0023;
            }
            num -= 1;
        Label_00D1:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        private void CheckAllSinsTobiraNonTargetTrophy()
        {
            this.SetSinsTobiraTrophyByAllUnit(1, 0x5c);
            this.SetSinsTobiraTrophyByAllUnit(2, 0x5d);
            this.SetSinsTobiraTrophyByAllUnit(3, 0x5e);
            this.SetSinsTobiraTrophyByAllUnit(4, 0x61);
            this.SetSinsTobiraTrophyByAllUnit(5, 0x60);
            this.SetSinsTobiraTrophyByAllUnit(6, 0x5f);
            this.SetSinsTobiraTrophyByAllUnit(7, 0x62);
            return;
        }

        public bool CheckChangeArena()
        {
            if (this.ChallengeArenaNum < this.ChallengeArenaMax)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return (this.mChallengeArenaTimer.val == this.mChallengeArenaTimer.valMax);
        }

        public bool CheckConceptCardCapacity(int adddValue)
        {
            int num;
            num = GlobalVars.ConceptCardNum + adddValue;
            return ((num > this.ConceptCardCap) == 0);
        }

        public CreateItemResult CheckCreateItem(ItemParam item)
        {
            RecipeParam param;
            bool flag;
            int num;
            RecipeItem item2;
            int num2;
            int num3;
            ItemParam param2;
            ItemParam param3;
            int num4;
            if (item == null)
            {
                goto Label_0016;
            }
            if (string.IsNullOrEmpty(item.recipe) == null)
            {
                goto Label_0018;
            }
        Label_0016:
            return 0;
        Label_0018:
            param = MonoSingleton<GameManager>.Instance.GetRecipeParam(item.recipe);
            if (param != null)
            {
                goto Label_0031;
            }
            return 0;
        Label_0031:
            flag = 0;
            num = 0;
            goto Label_00C0;
        Label_003A:
            item2 = param.items[num];
            num2 = item2.num;
            num3 = this.GetItemAmount(item2.iname);
            if (num3 >= num2)
            {
                goto Label_00BC;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(item2.iname);
            if (param2 == null)
            {
                goto Label_0087;
            }
            if (param2.IsCommon != null)
            {
                goto Label_0089;
            }
        Label_0087:
            return 0;
        Label_0089:
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetCommonEquip(param2, 0);
            num4 = this.GetItemAmount(param3.iname);
            if ((num3 + num4) >= num2)
            {
                goto Label_00BA;
            }
            return 0;
        Label_00BA:
            flag = 1;
        Label_00BC:
            num += 1;
        Label_00C0:
            if (num < ((int) param.items.Length))
            {
                goto Label_003A;
            }
            if (flag == null)
            {
                goto Label_00D6;
            }
            return 2;
        Label_00D6:
            return 1;
        }

        private bool CheckDailyMissionDayChange(TrophyState state, int countIndex)
        {
            int num;
            num = SRPG_Extensions.ToYMD(this.GetMissionClearAt());
            if (state.Param.IsDaily == null)
            {
                goto Label_0035;
            }
            if (num <= state.StartYMD)
            {
                goto Label_0035;
            }
            if (state.IsCompleted != null)
            {
                goto Label_0035;
            }
            return 0;
        Label_0035:
            return 1;
        }

        public unsafe bool CheckEnable2(UnitData self, EquipData[] equips_base, ref Dictionary<string, int> consume, ref int cost, ref int target_rank, ref bool can_jobmaster, ref bool can_jobmax, NeedEquipItemList item_list)
        {
            JobParam param;
            int num;
            int num2;
            EquipData[] dataArray;
            int num3;
            bool flag;
            int num4;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(self.CurrentJob.JobID);
            num = self.CurrentJob.Rank;
            num2 = self.CurrentJob.GetJobRankCap(self);
            this.mConsumeMaterials.Clear();
            this.mCreateItemCost = 0;
            dataArray = new EquipData[6];
            num3 = num;
            goto Label_010E;
        Label_0055:
            if (num3 != num)
            {
                goto Label_006E;
            }
            flag = this.CheckEnableCreateEquipItemAll2(self, equips_base, item_list);
            goto Label_00AF;
        Label_006E:
            num4 = 0;
            goto Label_0099;
        Label_0076:
            dataArray[num4] = new EquipData();
            dataArray[num4].Setup(param.GetRankupItemID(num3, num4));
            num4 += 1;
        Label_0099:
            if (num4 < ((int) dataArray.Length))
            {
                goto Label_0076;
            }
            flag = this.CheckEnableCreateEquipItemAll2(self, dataArray, item_list);
        Label_00AF:
            if (flag != null)
            {
                goto Label_00BB;
            }
            goto Label_0116;
        Label_00BB:
            if (num2 != JobParam.MAX_JOB_RANK)
            {
                goto Label_00D9;
            }
            if (num3 != num2)
            {
                goto Label_00D9;
            }
            if (flag == null)
            {
                goto Label_00D9;
            }
            *((sbyte*) can_jobmaster) = 1;
        Label_00D9:
            if (num3 != num2)
            {
                goto Label_00E5;
            }
            *((sbyte*) can_jobmax) = 1;
        Label_00E5:
            *(consume) = new Dictionary<string, int>(this.mConsumeMaterials);
            *((int*) cost) = this.mCreateItemCost;
            *((int*) target_rank) = Mathf.Min(num3 + 1, num2);
            num3 += 1;
        Label_010E:
            if (num3 <= num2)
            {
                goto Label_0055;
            }
        Label_0116:
            return 1;
        }

        public bool CheckEnableConvertGold()
        {
            if (<>f__am$cache88 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache88 = new Predicate<ItemData>(PlayerData.<CheckEnableConvertGold>m__144);
        Label_001E:
            return ((this.Items.Find(<>f__am$cache88) == null) == 0);
        }

        public unsafe bool CheckEnableCreateEquipItemAll(UnitData self, EquipData[] equips, NeedEquipItemList item_list)
        {
            return this.CheckEnableCreateEquipItemAll(self, equips, &this.mConsumeMaterials, &this.mCreateItemCost, item_list);
        }

        public unsafe bool CheckEnableCreateEquipItemAll(UnitData self, EquipData[] equips, ref Dictionary<string, int> consume, ref int cost, NeedEquipItemList item_list)
        {
            int num;
            EquipData data;
            ItemData data2;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            Dictionary<string, int> dictionary;
            string str;
            int num7;
            if ((self != null) && (equips != null))
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            this.mConsumeMaterials.Clear();
            this.mCreateItemCost = 0;
            num = 0;
            goto Label_01A3;
        Label_0027:
            data = equips[num];
            if ((data != null) && (data.ItemParam.equipLv <= self.Lv))
            {
                goto Label_0058;
            }
            if (item_list == null)
            {
                goto Label_0056;
            }
            item_list.IsNotEnough = 1;
        Label_0056:
            return 0;
        Label_0058:
            if (data.IsEquiped() == null)
            {
                goto Label_0068;
            }
            goto Label_019F;
        Label_0068:
            data2 = this.FindItemDataByItemParam(data.ItemParam);
            num2 = (data2 == null) ? 0 : data2.Num;
            num3 = 1;
            if (this.mConsumeMaterials.ContainsKey(data.ItemID) == null)
            {
                goto Label_0102;
            }
            num5 = Math.Min(Math.Max(num2 - this.mConsumeMaterials[data.ItemID], 0), num3);
            if (num5 <= 0)
            {
                goto Label_012E;
            }
            num7 = dictionary[str];
            (dictionary = this.mConsumeMaterials)[str = data.ItemID] = num7 + num5;
            num3 -= num5;
            goto Label_012E;
        Label_0102:
            num6 = Math.Min(num2, num3);
            if (num6 <= 0)
            {
                goto Label_012E;
            }
            this.mConsumeMaterials.Add(data.ItemID, num6);
            num3 -= num6;
        Label_012E:
            if (num3 != null)
            {
                goto Label_013A;
            }
            goto Label_019F;
        Label_013A:
            if (this.CheckEnableCreateItem(equips[num].ItemParam, 0, num3, item_list) != null)
            {
                goto Label_019F;
            }
            if (equips[num].ItemParam.Recipe != null)
            {
                goto Label_018A;
            }
            if ((equips[num].ItemParam.cmn_type - 1) == 2)
            {
                goto Label_018A;
            }
            if (item_list == null)
            {
                goto Label_0188;
            }
            item_list.IsNotEnough = 1;
        Label_0188:
            return 0;
        Label_018A:
            if (item_list == null)
            {
                goto Label_019D;
            }
            if (item_list.IsEnoughCommon() != null)
            {
                goto Label_019F;
            }
        Label_019D:
            return 0;
        Label_019F:
            num += 1;
        Label_01A3:
            if (num < ((int) equips.Length))
            {
                goto Label_0027;
            }
            *(consume) = this.mConsumeMaterials;
            *((int*) cost) = this.mCreateItemCost;
            if (this.Gold >= *(((int*) cost)))
            {
                goto Label_01DC;
            }
            if (item_list == null)
            {
                goto Label_01DA;
            }
            item_list.IsNotEnough = 1;
        Label_01DA:
            return 0;
        Label_01DC:
            return 1;
        }

        public bool CheckEnableCreateEquipItemAll2(UnitData self, EquipData[] equips, NeedEquipItemList item_list)
        {
            int num;
            EquipData data;
            ItemData data2;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            Dictionary<string, int> dictionary;
            string str;
            int num7;
            if ((self != null) && (equips != null))
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            num = 0;
            goto Label_0161;
        Label_0015:
            data = equips[num];
            if (((data != null) && (string.IsNullOrEmpty(data.ItemID) == null)) && (data.ItemParam.equipLv <= self.Lv))
            {
                goto Label_0047;
            }
            return 0;
        Label_0047:
            if (data.IsEquiped() == null)
            {
                goto Label_0057;
            }
            goto Label_015D;
        Label_0057:
            data2 = this.FindItemDataByItemParam(data.ItemParam);
            num2 = (data2 == null) ? 0 : data2.Num;
            num3 = 1;
            if (this.mConsumeMaterials.ContainsKey(data.ItemID) == null)
            {
                goto Label_00F1;
            }
            num5 = Math.Min(Math.Max(num2 - this.mConsumeMaterials[data.ItemID], 0), num3);
            if (num5 <= 0)
            {
                goto Label_011D;
            }
            num7 = dictionary[str];
            (dictionary = this.mConsumeMaterials)[str = data.ItemID] = num7 + num5;
            num3 -= num5;
            goto Label_011D;
        Label_00F1:
            num6 = Math.Min(num2, num3);
            if (num6 <= 0)
            {
                goto Label_011D;
            }
            this.mConsumeMaterials.Add(data.ItemID, num6);
            num3 -= num6;
        Label_011D:
            if (num3 != null)
            {
                goto Label_0129;
            }
            goto Label_015D;
        Label_0129:
            if (this.CheckEnableCreateItem(data.ItemParam, 0, num3, item_list) != null)
            {
                goto Label_015D;
            }
            if (item_list == null)
            {
                goto Label_014F;
            }
            if (item_list.IsEnoughCommon() != null)
            {
                goto Label_015D;
            }
        Label_014F:
            if (item_list == null)
            {
                goto Label_015B;
            }
            item_list.Remove();
        Label_015B:
            return 0;
        Label_015D:
            num += 1;
        Label_0161:
            if (num < ((int) equips.Length))
            {
                goto Label_0015;
            }
            if (this.Gold >= this.mCreateItemCost)
            {
                goto Label_018A;
            }
            if (item_list == null)
            {
                goto Label_0188;
            }
            item_list.IsNotEnough = 1;
        Label_0188:
            return 0;
        Label_018A:
            return 1;
        }

        public unsafe bool CheckEnableCreateItem(ItemParam param, bool root, int needNum, NeedEquipItemList item_list)
        {
            bool flag;
            flag = 0;
            return this.CheckEnableCreateItem(param, &flag, root, needNum, item_list);
        }

        public unsafe bool CheckEnableCreateItem(ItemParam param, ref bool is_ikkatsu, bool root, int needNum, NeedEquipItemList item_list)
        {
            RecipeParam param2;
            bool flag;
            int num;
            RecipeItem item;
            ItemData data;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            ItemParam param3;
            bool flag2;
            Dictionary<string, int> dictionary;
            string str;
            int num7;
            if (root == null)
            {
                goto Label_001B;
            }
            this.mConsumeMaterials.Clear();
            this.mCreateItemCost = 0;
            *((sbyte*) is_ikkatsu) = 0;
        Label_001B:
            if ((param != null) && (string.IsNullOrEmpty(param.recipe) == null))
            {
                goto Label_005D;
            }
            if (((item_list == null) || (param.IsCommon == null)) || ((param.cmn_type - 1) != 2))
            {
                goto Label_005B;
            }
            item_list.Add(param, 1, 1);
        Label_005B:
            return 0;
        Label_005D:
            param2 = param.Recipe;
            if ((param2 != null) && (param2.items != null))
            {
                goto Label_0077;
            }
            return 0;
        Label_0077:
            this.mCreateItemCost += param2.cost * needNum;
            flag = 1;
            num = 0;
            goto Label_022C;
        Label_0096:
            item = param2.items[num];
            data = this.FindItemDataByItemID(item.iname);
            num2 = (data == null) ? 0 : data.Num;
            num3 = item.num * needNum;
            if (this.mConsumeMaterials.ContainsKey(item.iname) == null)
            {
                goto Label_0146;
            }
            num5 = Math.Min(Math.Max(num2 - this.mConsumeMaterials[item.iname], 0), num3);
            if (num5 <= 0)
            {
                goto Label_0173;
            }
            num7 = dictionary[str];
            (dictionary = this.mConsumeMaterials)[str = item.iname] = num7 + num5;
            num3 -= num5;
            goto Label_0173;
        Label_0146:
            num6 = Math.Min(num2, num3);
            if (num6 <= 0)
            {
                goto Label_0173;
            }
            this.mConsumeMaterials.Add(item.iname, num6);
            num3 -= num6;
        Label_0173:
            if (num3 <= 0)
            {
                goto Label_0228;
            }
            param3 = MonoSingleton<GameManager>.GetInstanceDirect().GetItemParam(item.iname);
            if (item_list == null)
            {
                goto Label_01F6;
            }
            flag2 = (param3.IsCommon == null) ? 0 : (num == 0);
            if (flag2 == null)
            {
                goto Label_01C1;
            }
            item_list.Add(param3, num3, 0);
            goto Label_01E6;
        Label_01C1:
            if (param3.IsCommon != null)
            {
                goto Label_01E6;
            }
            if (string.IsNullOrEmpty(param3.recipe) == null)
            {
                goto Label_01E6;
            }
            item_list.IsNotEnough = 1;
        Label_01E6:
            item_list.SetRecipeTree(new RecipeTree(param3), flag2);
        Label_01F6:
            if (this.CheckEnableCreateItem(param3, is_ikkatsu, 0, num3, item_list) != null)
            {
                goto Label_020B;
            }
            flag = 0;
        Label_020B:
            if (item_list == null)
            {
                goto Label_0219;
            }
            item_list.UpRecipeTree();
        Label_0219:
            if (param3.recipe == null)
            {
                goto Label_0228;
            }
            *((sbyte*) is_ikkatsu) = 1;
        Label_0228:
            num += 1;
        Label_022C:
            if (num < ((int) param2.items.Length))
            {
                goto Label_0096;
            }
            return flag;
        }

        public bool CheckEnableCreateItem(ItemParam param, ref bool is_ikkatsu, ref int cost, ref Dictionary<string, int> consumes, NeedEquipItemList item_list)
        {
            return this.CheckEnableCreateItem(param, 1, is_ikkatsu, cost, consumes, item_list);
        }

        public unsafe bool CheckEnableCreateItem(ItemParam param, int count, ref bool is_ikkatsu, ref int cost, ref Dictionary<string, int> consumes, NeedEquipItemList item_list)
        {
            bool flag;
            flag = this.CheckEnableCreateItem(param, is_ikkatsu, 1, count, item_list);
            *((int*) cost) = this.mCreateItemCost;
            *(consumes) = this.mConsumeMaterials;
            return flag;
        }

        public bool CheckEnableEquipUnit(ItemParam item)
        {
            int num;
            if (item == null)
            {
                goto Label_0012;
            }
            if (item.type == 3)
            {
                goto Label_0014;
            }
        Label_0012:
            return 0;
        Label_0014:
            num = 0;
            goto Label_0038;
        Label_001B:
            if (this.Units[num].CheckEnableEquipment(item) == null)
            {
                goto Label_0034;
            }
            return 1;
        Label_0034:
            num += 1;
        Label_0038:
            if (num < this.Units.Count)
            {
                goto Label_001B;
            }
            return 0;
        }

        public bool CheckEnableVipCard()
        {
            return (Network.GetServerTime() < this.mVipExpiredAt);
        }

        public bool CheckFreeGachaCoin()
        {
            return (this.GetNextFreeGachaCoinCoolDownSec() == 0L);
        }

        public unsafe bool CheckFreeGachaGold()
        {
            long num;
            DateTime time;
            DateTime time2;
            time = TimeManager.FromUnixTime(Network.GetServerTime());
            time2 = TimeManager.FromUnixTime(this.FreeGachaGold.at);
            if (&time.Year < &time2.Year)
            {
                goto Label_0057;
            }
            if (&time.Month < &time2.Month)
            {
                goto Label_0057;
            }
            if (&time.Day >= &time2.Day)
            {
                goto Label_0059;
            }
        Label_0057:
            return 1;
        Label_0059:
            if (this.FreeGachaGold.num != null)
            {
                goto Label_006B;
            }
            return 1;
        Label_006B:
            if (this.FreeGachaGold.num != MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldMax)
            {
                goto Label_0096;
            }
            return 0;
        Label_0096:
            return (this.GetNextFreeGachaGoldCoolDownSec() == 0L);
        }

        public unsafe bool CheckFreeGachaGoldMax()
        {
            long num;
            DateTime time;
            DateTime time2;
            time = TimeManager.FromUnixTime(Network.GetServerTime());
            time2 = TimeManager.FromUnixTime(this.FreeGachaGold.at);
            if (&time.Year < &time2.Year)
            {
                goto Label_0057;
            }
            if (&time.Month < &time2.Month)
            {
                goto Label_0057;
            }
            if (&time.Day >= &time2.Day)
            {
                goto Label_0059;
            }
        Label_0057:
            return 0;
        Label_0059:
            return (this.FreeGachaGold.num == MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldMax);
        }

        public bool CheckFriend(string fuid)
        {
            FriendData data;
            <CheckFriend>c__AnonStorey23B storeyb;
            storeyb = new <CheckFriend>c__AnonStorey23B();
            storeyb.fuid = fuid;
            if (string.IsNullOrEmpty(storeyb.fuid) == null)
            {
                goto Label_001F;
            }
            return 0;
        Label_001F:
            data = this.Friends.Find(new Predicate<FriendData>(storeyb.<>m__142));
            return ((data == null) ? 0 : data.IsFriend());
        }

        public bool CheckItemCapacity(ItemParam item, int num)
        {
            int num2;
            num2 = this.GetItemAmount(item.iname) + num;
            return ((num2 > item.cap) == 0);
        }

        public bool CheckPaidGacha()
        {
            return (this.PaidGacha.num == 0);
        }

        public bool CheckRankUpAbility(AbilityData ability)
        {
            if (ability.Rank < ability.GetRankCap())
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            if (this.AbilityRankUpCountNum != null)
            {
                goto Label_0020;
            }
            return 0;
        Label_0020:
            if (this.Gold >= MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityNextGold(ability.Rank))
            {
                goto Label_0042;
            }
            return 0;
        Label_0042:
            return 1;
        }

        public bool CheckShopUpdateCost(EShopType type)
        {
            ShopData data;
            ShopParam param;
            int num;
            int num2;
            if (this.GetShopData(type) != null)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(type);
            if (param != null)
            {
                goto Label_0029;
            }
            return 0;
        Label_0029:
            num = this.GetShopUpdateCost(type, 0);
            num2 = this.GetShopTypeCostAmount(param.UpdateCostType);
            return ((num > num2) == 0);
        }

        private unsafe bool CheckTrophyCount(TrophyParam trophyParam, int countIndex, int value, ref TrophyState state)
        {
            if (countIndex < 0)
            {
                goto Label_0014;
            }
            if (value <= 0)
            {
                goto Label_0014;
            }
            if (trophyParam != null)
            {
                goto Label_0016;
            }
        Label_0014:
            return 0;
        Label_0016:
            if (trophyParam.IsAvailablePeriod(this.GetMissionClearAt(), 0) != null)
            {
                goto Label_002A;
            }
            return 0;
        Label_002A:
            if (trophyParam.RequiredTrophies == null)
            {
                goto Label_004E;
            }
            if (TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, trophyParam, trophyParam.IsChallengeMission, 1) != null)
            {
                goto Label_004E;
            }
            return 0;
        Label_004E:
            *(state) = this.GetTrophyCounter(trophyParam, 0);
            if (*(state).IsEnded == null)
            {
                goto Label_0068;
            }
            return 0;
        Label_0068:
            if (((int) *(state).Count.Length) > countIndex)
            {
                goto Label_0088;
            }
            Array.Resize<int>(&*(state).Count, countIndex + 1);
        Label_0088:
            if (*(state).IsCompleted == null)
            {
                goto Label_0097;
            }
            return 0;
        Label_0097:
            return 1;
        }

        public bool CheckUnlock(UnlockTargets target)
        {
            return (((this.mUnlocks & target) == 0) == 0);
        }

        public bool CheckUnlockShopType(EShopType type)
        {
            UnlockTargets targets;
            targets = SRPG_Extensions.ToUnlockTargets(type);
            if (targets == null)
            {
                goto Label_0015;
            }
            return this.CheckUnlock(targets);
        Label_0015:
            return 0;
        }

        public bool ClassChangeUnit(UnitData unit, int index)
        {
            if (unit.CheckJobClassChange(index) != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            unit.JobClassChange(index);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(2);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
            return 1;
        }

        public void ClearArtifacts()
        {
            this.mArtifacts.Clear();
            this.mArtifactsNumByRarity.Clear();
            return;
        }

        public void ClearItemFlags(ItemData.ItemFlags flags)
        {
            int num;
            if (flags != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = this.mItems.Count - 1;
            goto Label_0041;
        Label_001A:
            if (this.mItems[num] == null)
            {
                goto Label_003D;
            }
            this.mItems[num].ResetFlag(flags);
        Label_003D:
            num -= 1;
        Label_0041:
            if (num >= 0)
            {
                goto Label_001A;
            }
            return;
        }

        public void ClearItems()
        {
            if (this.mItems == null)
            {
                goto Label_0016;
            }
            this.mItems.Clear();
        Label_0016:
            if (this.mID2ItemData == null)
            {
                goto Label_002C;
            }
            this.mID2ItemData.Clear();
        Label_002C:
            return;
        }

        public void ClearNewItemFlags()
        {
            int num;
            num = this.mItems.Count - 1;
            goto Label_003A;
        Label_0013:
            if (this.mItems[num] == null)
            {
                goto Label_0036;
            }
            this.mItems[num].IsNew = 0;
        Label_0036:
            num -= 1;
        Label_003A:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return;
        }

        public void ClearTrophies()
        {
            this.mTrophyStates = new List<TrophyState>();
            this.mTrophyStatesInameDict = new Dictionary<string, List<TrophyState>>();
            return;
        }

        private void ClearTrophyCounter(TrophyState _st)
        {
            if (this.mTrophyStates.Contains(_st) == null)
            {
                goto Label_001E;
            }
            this.mTrophyStates.Remove(_st);
        Label_001E:
            if (this.mTrophyStatesInameDict.ContainsKey(_st.iname) == null)
            {
                goto Label_007A;
            }
            this.mTrophyStatesInameDict[_st.iname].Remove(_st);
            if (this.mTrophyStatesInameDict[_st.iname].Count > 0)
            {
                goto Label_007A;
            }
            this.mTrophyStatesInameDict.Remove(_st.iname);
        Label_007A:
            return;
        }

        public void ClearUnits()
        {
            if (this.mUnits == null)
            {
                goto Label_0016;
            }
            this.mUnits.Clear();
        Label_0016:
            if (this.mUniqueID2UnitData == null)
            {
                goto Label_002C;
            }
            this.mUniqueID2UnitData.Clear();
        Label_002C:
            return;
        }

        private void ConsumeAwakePieces(UnitData unit, int num)
        {
            ItemData data;
            ItemData data2;
            ItemData data3;
            int num2;
            int num3;
            int num4;
            data = this.FindItemDataByItemID(unit.UnitParam.piece);
            data2 = this.FindItemDataByItemID(unit.UnitParam.piece);
            data3 = this.FindItemDataByItemID(unit.UnitParam.piece);
            if ((data == null) || (data.Num <= 0))
            {
                goto Label_006D;
            }
            num2 = (data.Num < num) ? data.Num : num;
            data.Used(num2);
            num -= num2;
        Label_006D:
            if (num >= 1)
            {
                goto Label_0075;
            }
            return;
        Label_0075:
            if ((data2 == null) || (data2.Num <= 0))
            {
                goto Label_00AF;
            }
            num3 = (data2.Num < num) ? data2.Num : num;
            data2.Used(num3);
            num -= num3;
        Label_00AF:
            if (num >= 1)
            {
                goto Label_00B7;
            }
            return;
        Label_00B7:
            if ((data3 == null) || (data3.Num <= 0))
            {
                goto Label_00F1;
            }
            num4 = (data3.Num < num) ? data3.Num : num;
            data3.Used(num4);
            num -= num4;
        Label_00F1:
            if (num >= 1)
            {
                goto Label_00F9;
            }
            return;
        Label_00F9:
            Debug.LogError("減算できていない欠片個数: " + ((int) num));
            return;
        }

        public bool ConsumeStamina(int stamina)
        {
            if (this.Stamina >= stamina)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            if (this.mStamina.val < this.mStamina.valMax)
            {
                goto Label_0048;
            }
            this.mStamina.at = Network.GetServerTime();
        Label_0048:
            this.mStamina.val = Mathf.Max(this.mStamina.val - stamina, 0);
            return 1;
        }

        public void CreateInheritingExtraTrophy(Dictionary<int, List<JSON_TrophyProgress>> progs)
        {
            GameManager manager;
            TrophyParam[] paramArray;
            int num;
            int num2;
            List<JSON_TrophyProgress> list;
            JSON_TrophyProgress progress;
            int num3;
            int num4;
            TrophyState state;
            <CreateInheritingExtraTrophy>c__AnonStorey241 storey;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.Trophies;
            num = 0;
            goto Label_0102;
        Label_0019:
            storey = new <CreateInheritingExtraTrophy>c__AnonStorey241();
            storey.param = paramArray[num];
            if (TrophyConditionTypesEx.IsExtraClear(paramArray[num].Objectives[0].type) != null)
            {
                goto Label_0048;
            }
            goto Label_00FE;
        Label_0048:
            num2 = paramArray[num].Objectives[0].type;
            if (progs.ContainsKey(num2) != null)
            {
                goto Label_0069;
            }
            goto Label_00FE;
        Label_0069:
            list = progs[num2];
            if (list.Find(new Predicate<JSON_TrophyProgress>(storey.<>m__148)) == null)
            {
                goto Label_0094;
            }
            goto Label_00FE;
        Label_0094:
            num3 = 0;
            num4 = 0;
            goto Label_00CE;
        Label_009F:
            if (num3 >= list[num4].pts[0])
            {
                goto Label_00C8;
            }
            num3 = list[num4].pts[0];
        Label_00C8:
            num4 += 1;
        Label_00CE:
            if (num4 < list.Count)
            {
                goto Label_009F;
            }
            state = this.CreateTrophyState(storey.param);
            state.Count[0] = num3;
            this.AddTrophyStateDict(state);
        Label_00FE:
            num += 1;
        Label_0102:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0019;
            }
            return;
        }

        public bool CreateItem(ItemParam item)
        {
            RecipeParam param;
            int num;
            RecipeItem item2;
            param = MonoSingleton<GameManager>.Instance.GetRecipeParam(item.recipe);
            if (this.CheckItemCapacity(item, 1) != null)
            {
                goto Label_0020;
            }
            return 0;
        Label_0020:
            if (param.cost <= this.Gold)
            {
                goto Label_0033;
            }
            return 0;
        Label_0033:
            if (this.CheckCreateItem(item) != null)
            {
                goto Label_0041;
            }
            return 0;
        Label_0041:
            this.GainGold(-param.cost);
            num = 0;
            goto Label_0075;
        Label_0055:
            item2 = param.items[num];
            this.GainItem(item2.iname, -item2.num);
            num += 1;
        Label_0075:
            if (num < ((int) param.items.Length))
            {
                goto Label_0055;
            }
            this.GainItem(item.iname, 1);
            return 1;
        }

        public unsafe bool CreateItemAll(ItemParam item)
        {
            Dictionary<string, int> dictionary;
            bool flag;
            int num;
            string str;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            if (this.CheckItemCapacity(item, 1) != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            dictionary = null;
            flag = 0;
            num = 0;
            if (this.CheckEnableCreateItem(item, &flag, &num, &dictionary, null) != null)
            {
                goto Label_002A;
            }
            return 0;
        Label_002A:
            if (num <= this.Gold)
            {
                goto Label_0038;
            }
            return 0;
        Label_0038:
            this.GainGold(-num);
            if (dictionary == null)
            {
                goto Label_008D;
            }
            enumerator = dictionary.Keys.GetEnumerator();
        Label_0053:
            try
            {
                goto Label_006F;
            Label_0058:
                str = &enumerator.Current;
                this.GainItem(str, -dictionary[str]);
            Label_006F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0058;
                }
                goto Label_008D;
            }
            finally
            {
            Label_0080:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_008D:
            this.GainItem(item.iname, 1);
            return 1;
        }

        public Dictionary<ItemParam, int> CreateItemSnapshot()
        {
            Dictionary<ItemParam, int> dictionary;
            int num;
            dictionary = new Dictionary<ItemParam, int>();
            num = 0;
            goto Label_0039;
        Label_000D:
            dictionary[this.mItems[num].Param] = this.mItems[num].NumNonCap;
            num += 1;
        Label_0039:
            if (num < this.mItems.Count)
            {
                goto Label_000D;
            }
            return dictionary;
        }

        private TrophyState CreateTrophyState(TrophyParam _trophy)
        {
            TrophyState state;
            state = new TrophyState();
            state.iname = _trophy.iname;
            state.StartYMD = SRPG_Extensions.ToYMD(TimeManager.ServerTime);
            state.Count = new int[(int) _trophy.Objectives.Length];
            state.IsDirty = 0;
            state.Param = _trophy;
            return state;
        }

        public void DailyAllCompleteCheck()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x34);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0033;
        Label_0018:
            objective = objectiveArray[num];
            if (this.IsDailyAllComplete() == null)
            {
                goto Label_002F;
            }
            this.AddTrophyCounter(objective, 1);
        Label_002F:
            num -= 1;
        Label_0033:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void DEBUG_ADD_ARTIFACTS(ArtifactData artifact)
        {
            if (this.mArtifacts != null)
            {
                goto Label_0016;
            }
            this.mArtifacts = new List<ArtifactData>();
        Label_0016:
            if (artifact == null)
            {
                goto Label_0034;
            }
            if (this.mArtifacts.Contains(artifact) != null)
            {
                goto Label_0034;
            }
            this.AddArtifact(artifact);
        Label_0034:
            return;
        }

        public void DEBUG_ADD_COIN(int free, int paid, int com)
        {
            this.mFreeCoin += free;
            this.mPaidCoin += paid;
            this.mComCoin += com;
            this.GainVipPoint(paid);
            return;
        }

        public void DEBUG_ADD_GOLD(int gold)
        {
            this.mGold += gold;
            return;
        }

        public void DEBUG_BUY_ITEM(EShopType shoptype, int index)
        {
            ShopData data;
            ShopItem item;
            ItemData data2;
            ItemParam param;
            ESaleType type;
            data = this.GetShopData(shoptype);
            if (data != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            item = data.items[index];
            if (item.is_soldout == null)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            data2 = this.FindItemDataByItemID(item.iname);
            if (data2 == null)
            {
                goto Label_004D;
            }
            if (data2.Num != data2.HaveCap)
            {
                goto Label_004D;
            }
            return;
        Label_004D:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
            switch (item.saleType)
            {
                case 0:
                    goto Label_0092;

                case 1:
                    goto Label_00C1;

                case 2:
                    goto Label_00F3;

                case 3:
                    goto Label_0122;

                case 4:
                    goto Label_0151;

                case 5:
                    goto Label_0180;

                case 6:
                    goto Label_01AF;

                case 7:
                    goto Label_00DA;
            }
            goto Label_01BE;
        Label_0092:
            this.mGold = Math.Max(this.mGold - (param.buy * item.num), 0);
            goto Label_01BE;
        Label_00C1:
            this.DEBUG_CONSUME_COIN(param.coin * item.num);
            goto Label_01BE;
        Label_00DA:
            this.DEBUG_CONSUME_PAID_COIN(param.coin * item.num);
            goto Label_01BE;
        Label_00F3:
            this.mTourCoin = Math.Max(this.mTourCoin - (param.tour_coin * item.num), 0);
            goto Label_01BE;
        Label_0122:
            this.mArenaCoin = Math.Max(this.mArenaCoin - (param.arena_coin * item.num), 0);
            goto Label_01BE;
        Label_0151:
            this.mPiecePoint = Math.Max(this.mPiecePoint - (param.piece_point * item.num), 0);
            goto Label_01BE;
        Label_0180:
            this.mMultiCoin = Math.Max(this.mMultiCoin - (param.multi_coin * item.num), 0);
            goto Label_01BE;
        Label_01AF:
            DebugUtility.Assert("There is no common price in the event coin.");
        Label_01BE:
            this.GainItem(item.iname, item.num);
            item.is_soldout = 1;
            return;
        }

        public void DEBUG_BUY_ITEM_UPDATED(EShopType shoptype)
        {
            ShopData data;
            ShopParam param;
            int num;
            int num2;
            string str;
            ESaleType type;
            data = this.GetShopData(shoptype);
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(shoptype);
            if (data == null)
            {
                goto Label_018F;
            }
            if (param == null)
            {
                goto Label_018F;
            }
            if (this.CheckShopUpdateCost(shoptype) != null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            num = 0;
            goto Label_004F;
        Label_0039:
            data.items[num].is_soldout = 0;
            num += 1;
        Label_004F:
            if (num < data.items.Count)
            {
                goto Label_0039;
            }
            num2 = this.GetShopUpdateCost(shoptype, 0);
            switch (param.UpdateCostType)
            {
                case 0:
                    goto Label_009D;

                case 1:
                    goto Label_00C0;

                case 2:
                    goto Label_00DA;

                case 3:
                    goto Label_00FD;

                case 4:
                    goto Label_0120;

                case 5:
                    goto Label_0143;

                case 6:
                    goto Label_0166;

                case 7:
                    goto Label_00CD;
            }
            goto Label_018F;
        Label_009D:
            this.mGold = Math.Max(this.mGold - num2, 0);
            goto Label_018F;
        Label_00C0:
            this.DEBUG_CONSUME_COIN(num2);
            goto Label_018F;
        Label_00CD:
            this.DEBUG_CONSUME_PAID_COIN(num2);
            goto Label_018F;
        Label_00DA:
            this.mTourCoin = Math.Max(this.mTourCoin - num2, 0);
            goto Label_018F;
        Label_00FD:
            this.mArenaCoin = Math.Max(this.mArenaCoin - num2, 0);
            goto Label_018F;
        Label_0120:
            this.mPiecePoint = Math.Max(this.mPiecePoint - num2, 0);
            goto Label_018F;
        Label_0143:
            this.mMultiCoin = Math.Max(this.mMultiCoin - num2, 0);
            goto Label_018F;
        Label_0166:
            str = GlobalVars.EventShopItem.shop_cost_iname;
            this.SetEventCoinNum(str, Math.Max(this.EventCoinNum(str) - num2, 0));
        Label_018F:
            return;
        }

        public bool DEBUG_CONSUME_COIN(int coin)
        {
            if (this.Coin >= coin)
            {
                goto Label_0029;
            }
            return 0;
            goto Label_0029;
        Label_0013:
            coin -= 1;
            this.mFreeCoin = OInt.op_Decrement(this.mFreeCoin);
        Label_0029:
            if (this.mFreeCoin <= 0)
            {
                goto Label_005C;
            }
            if (coin > 0)
            {
                goto Label_0013;
            }
            goto Label_005C;
        Label_0046:
            coin -= 1;
            this.mComCoin = OInt.op_Decrement(this.mComCoin);
        Label_005C:
            if (this.mComCoin <= 0)
            {
                goto Label_008F;
            }
            if (coin > 0)
            {
                goto Label_0046;
            }
            goto Label_008F;
        Label_0079:
            coin -= 1;
            this.mPaidCoin = OInt.op_Decrement(this.mPaidCoin);
        Label_008F:
            if (this.mPaidCoin <= 0)
            {
                goto Label_00A7;
            }
            if (coin > 0)
            {
                goto Label_0079;
            }
        Label_00A7:
            return 1;
        }

        public bool DEBUG_CONSUME_PAID_COIN(int coin)
        {
            if (this.PaidCoin >= coin)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            this.mPaidCoin -= coin;
            return 1;
        }

        public void DEBUG_GAIN_ALL_ARTIFACT()
        {
            List<ArtifactParam> list;
            long num;
            int num2;
            ArtifactParam param;
            Json_Artifact artifact;
            ArtifactData data;
            list = MonoSingleton<GameManager>.Instance.MasterParam.Artifacts;
            num = 1L;
            num2 = 0;
            goto Label_008C;
        Label_001A:
            param = list[num2];
            if (param.is_create != null)
            {
                goto Label_0032;
            }
            goto Label_0088;
        Label_0032:
            artifact = new Json_Artifact();
            num += 1L;
            artifact.iid = num;
            artifact.exp = 0;
            artifact.iname = param.iname;
            artifact.rare = param.rareini;
            artifact.fav = 0;
            data = new ArtifactData();
            data.Deserialize(artifact);
            this.AddArtifact(data);
        Label_0088:
            num2 += 1;
        Label_008C:
            if (num2 < list.Count)
            {
                goto Label_001A;
            }
            return;
        }

        public void DEBUG_GAIN_ALL_ITEMS()
        {
            List<ItemParam> list;
            int num;
            list = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.Items;
            num = 0;
            goto Label_002F;
        Label_0017:
            this.GainItem(list[num].iname, 10);
            num += 1;
        Label_002F:
            if (num < list.Count)
            {
                goto Label_0017;
            }
            return;
        }

        public void DEBUG_REPAIR_STAMINA()
        {
            this.mStamina.val = this.mStamina.valMax;
            this.mStamina.Update();
            return;
        }

        public void DEBUG_TRASH_ALL_ARTIFACT()
        {
            this.mArtifacts.Clear();
            this.mArtifactsNumByRarity.Clear();
            return;
        }

        public void DEBUG_TRASH_ALL_ITEMS()
        {
            this.Items.Clear();
            return;
        }

        public void DeleteTrophies(JSON_TrophyProgress[] trophies)
        {
            int num;
            <DeleteTrophies>c__AnonStorey23E storeye;
            <DeleteTrophies>c__AnonStorey23F storeyf;
            storeye = new <DeleteTrophies>c__AnonStorey23E();
            storeye.trophies = trophies;
            if (storeye.trophies != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            if (this.mTrophyStates == null)
            {
                goto Label_0076;
            }
            storeyf = new <DeleteTrophies>c__AnonStorey23F();
            storeyf.<>f__ref$574 = storeye;
            storeyf.i = 0;
            goto Label_0063;
        Label_003D:
            this.mTrophyStates.RemoveAll(new Predicate<TrophyState>(storeyf.<>m__146));
            storeyf.i += 1;
        Label_0063:
            if (storeyf.i < ((int) storeye.trophies.Length))
            {
                goto Label_003D;
            }
        Label_0076:
            if (this.mTrophyStatesInameDict == null)
            {
                goto Label_00D0;
            }
            num = 0;
            goto Label_00C2;
        Label_0088:
            if (this.mTrophyStatesInameDict.ContainsKey(storeye.trophies[num].iname) == null)
            {
                goto Label_00BE;
            }
            this.mTrophyStatesInameDict.Remove(storeye.trophies[num].iname);
        Label_00BE:
            num += 1;
        Label_00C2:
            if (num < ((int) storeye.trophies.Length))
            {
                goto Label_0088;
            }
        Label_00D0:
            return;
        }

        public string DequeueNextLoginBonusTableID()
        {
            if (this.mLoginBonusQueue.Count >= 1)
            {
                goto Label_0013;
            }
            return null;
        Label_0013:
            return this.mLoginBonusQueue.Dequeue();
        }

        public bool Deserialize(Json_ArenaPlayers json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mArenaRank = json.rank_myself;
            this.mBestArenaRank = json.best_myself;
            this.mArenaLastAt = GameUtility.UnixtimeToLocalTime(json.btl_at);
            GlobalVars.SelectedQuestID = (string.IsNullOrEmpty(json.quest_iname) == null) ? json.quest_iname : string.Empty;
            this.mArenaSeed = json.seed;
            this.mArenaMaxActionNum = json.maxActionNum;
            this.mArenaEndAt = GameUtility.UnixtimeToLocalTime(json.end_at);
            if (json.end_at == null)
            {
                goto Label_00AC;
            }
            if ((this.mArenaEndAt < TimeManager.ServerTime) == null)
            {
                goto Label_00AC;
            }
            this.mArenaEndAt = GameUtility.UnixtimeToLocalTime(0L);
        Label_00AC:
            return 1;
        }

        public void Deserialize(Json_Currencies json)
        {
            this.mGold = json.gold;
            if (json.coin == null)
            {
                goto Label_005E;
            }
            this.mFreeCoin = json.coin.free;
            this.mPaidCoin = json.coin.paid;
            this.mComCoin = json.coin.com;
        Label_005E:
            this.mArenaCoin = json.arenacoin;
            this.mMultiCoin = json.multicoin;
            this.mTourCoin = json.enseicoin;
            this.mPiecePoint = json.kakeracoin;
            return;
        }

        public void Deserialize(Json_MailInfo json)
        {
            this.mUnreadMail = json.mail_f_unread == 0;
            this.mUnreadMailPeriod = json.mail_unread == 0;
            return;
        }

        public bool Deserialize(Json_Mails mails)
        {
            this.MailPage = new MailPageData();
            if (mails != null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            this.MailPage.Deserialize(mails.list);
            this.MailPage.Deserialize(mails.option);
            return 1;
        }

        public bool Deserialize(Json_Notify notify)
        {
            int num;
            if (notify != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            this.mFirstLogin = (((notify.bonus >> 5) & 1) == 0) == 0;
            this.mLoginBonusCount = notify.bonus & 0x1f;
            this.mLoginBonus = notify.logbonus;
            this.mLoginBonus28days = null;
            if (notify.logbotables == null)
            {
                goto Label_00F0;
            }
            num = 0;
            goto Label_00E2;
        Label_0052:
            if (notify.logbotables[num] == null)
            {
                goto Label_00DE;
            }
            if (string.IsNullOrEmpty(notify.logbotables[num].type) == null)
            {
                goto Label_007B;
            }
            goto Label_00DE;
        Label_007B:
            this.mLoginBonusTables[notify.logbotables[num].type] = notify.logbotables[num];
            if (this.mFirstLogin == null)
            {
                goto Label_00BE;
            }
            this.mLoginBonusQueue.Enqueue(notify.logbotables[num].type);
        Label_00BE:
            if (notify.logbotables[num].bonus_units == null)
            {
                goto Label_00DE;
            }
            this.mLoginBonus28days = notify.logbotables[num];
        Label_00DE:
            num += 1;
        Label_00E2:
            if (num < ((int) notify.logbotables.Length))
            {
                goto Label_0052;
            }
        Label_00F0:
            this.SupportCount = 1;
            this.SupportGold = notify.supgold;
            return 1;
        }

        public void Deserialize(Json_PlayerData json)
        {
            FixParam param;
            int num;
            long num2;
            long num3;
            DateTime time;
            DateTime time2;
            DateTime time3;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.mName = json.name;
            this.mCuid = json.cuid;
            this.mFuid = json.fuid;
            this.mTuid = null;
            this.mTuidExpiredAt = 0L;
            this.mExp = json.exp;
            this.mGold = json.gold;
            this.mLv = this.CalcLevel();
            this.mUnitCap = json.unitbox_max;
            this.mAbilityPoint = json.abilpt;
            this.mFreeCoin = 0;
            this.mPaidCoin = 0;
            this.mComCoin = 0;
            this.mTourCoin = json.enseicoin;
            this.mArenaCoin = json.arenacoin;
            this.mMultiCoin = json.multicoin;
            this.mPiecePoint = json.kakeracoin;
            this.mVipPoint = 0;
            this.mVipRank = 0;
            this.mNewGameAt = json.newgame_at;
            this.mLoginCount = json.logincont;
            if (json.multi == null)
            {
                goto Label_016A;
            }
            this.mMultiInvitaionFlag = (json.multi.is_multi_push != null) ? 1 : 0;
            this.mMultiInvitaionComment = json.multi.multi_comment;
        Label_016A:
            if (json.vip == null)
            {
                goto Label_018B;
            }
            this.mVipExpiredAt = json.vip.expired_at;
        Label_018B:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (json.tuid == null)
            {
                goto Label_01C8;
            }
            this.mTuid = json.tuid.id;
            this.mTuidExpiredAt = json.tuid.expired_at;
        Label_01C8:
            if (json.coin == null)
            {
                goto Label_0215;
            }
            this.mFreeCoin = json.coin.free;
            this.mPaidCoin = json.coin.paid;
            this.mComCoin = json.coin.com;
        Label_0215:
            if (json.stamina == null)
            {
                goto Label_0293;
            }
            this.mStamina.val = json.stamina.pt;
            this.mStamina.valMax = json.stamina.max;
            this.mStamina.valRecover = param.StaminaRecoveryVal;
            this.mStamina.interval = param.StaminaRecoverySec;
            this.mStamina.at = json.stamina.at;
        Label_0293:
            if (json.cave == null)
            {
                goto Label_0307;
            }
            this.mCaveStamina.val = json.cave.pt;
            this.mCaveStamina.valMax = param.CaveStaminaMax;
            this.mCaveStamina.valRecover = param.CaveStaminaRecoveryVal;
            this.mCaveStamina.interval = param.CaveStaminaRecoverySec;
            this.mCaveStamina.at = json.cave.at;
        Label_0307:
            if (json.abilup == null)
            {
                goto Label_037B;
            }
            this.mAbilityRankUpCount.val = json.abilup.num;
            this.mAbilityRankUpCount.valMax = param.AbilityRankUpCountMax;
            this.mAbilityRankUpCount.valRecover = param.AbilityRankUpCountRecoveryVal;
            this.mAbilityRankUpCount.interval = param.AbilityRankUpCountRecoverySec;
            this.mAbilityRankUpCount.at = json.abilup.at;
        Label_037B:
            if (json.arena == null)
            {
                goto Label_040C;
            }
            this.mChallengeArenaNum = json.arena.num;
            this.mChallengeArenaTimer.val = 0;
            this.mChallengeArenaTimer.valMax = 1;
            this.mChallengeArenaTimer.valRecover = 1;
            this.mChallengeArenaTimer.interval = param.ChallengeArenaCoolDownSec;
            this.mChallengeArenaTimer.at = json.arena.at;
            this.mArenaResetCount = json.arena.cnt_resetcost;
        Label_040C:
            if (json.tour == null)
            {
                goto Label_042D;
            }
            this.mTourNum = json.tour.num;
        Label_042D:
            if (json.gachag == null)
            {
                goto Label_0464;
            }
            this.FreeGachaGold.num = json.gachag.num;
            this.FreeGachaGold.at = json.gachag.at;
        Label_0464:
            if (json.gachac == null)
            {
                goto Label_049B;
            }
            this.FreeGachaCoin.num = json.gachac.num;
            this.FreeGachaCoin.at = json.gachac.at;
        Label_049B:
            if (json.gachap == null)
            {
                goto Label_04D2;
            }
            this.PaidGacha.num = json.gachap.num;
            this.PaidGacha.at = json.gachap.at;
        Label_04D2:
            if (json.friends == null)
            {
                goto Label_0557;
            }
            this.mFriendNum = json.friends.friendnum;
            if (json.friends.follower == null)
            {
                goto Label_0550;
            }
            this.mFollowerNum = (int) json.friends.follower.Length;
            this.mFollowerUID.Clear();
            num = 0;
            goto Label_053F;
        Label_0523:
            this.mFollowerUID.Add(json.friends.follower[num]);
            num += 1;
        Label_053F:
            if (num < this.mFollowerNum)
            {
                goto Label_0523;
            }
            goto Label_0557;
        Label_0550:
            this.mFollowerNum = 0;
        Label_0557:
            this.mUnreadMail = json.mail_f_unread == 0;
            this.mUnreadMailPeriod = json.mail_unread == 0;
            this.mChallengeMultiNum = json.cnt_multi;
            this.mStaminaBuyNum = json.cnt_stmrecover;
            this.mGoldBuyNum = json.cnt_buygold;
            this.mSelectedAward = json.selected_award;
            if (json.g_shop == null)
            {
                goto Label_064B;
            }
            num2 = json.g_shop.time_start;
            num3 = json.g_shop.time_end;
            if (this.mGuerrillaShopStart != num2)
            {
                goto Label_05EF;
            }
            if (this.mGuerrillaShopEnd == num3)
            {
                goto Label_064B;
            }
        Label_05EF:
            time = TimeManager.ServerTime;
            time2 = TimeManager.FromUnixTime(json.g_shop.time_start);
            time3 = TimeManager.FromUnixTime(json.g_shop.time_end);
            if ((time >= time2) == null)
            {
                goto Label_063D;
            }
            if ((time < time3) == null)
            {
                goto Label_063D;
            }
            this.mIsGuerrillaShopStarted = 1;
        Label_063D:
            this.mGuerrillaShopStart = num2;
            this.mGuerrillaShopEnd = num3;
        Label_064B:
            this.UpdateUnlocks();
            AppGuardClient.SetUserId(this.mCuid);
            return;
        }

        public void Deserialize(Json_TrophyPlayerData json)
        {
            FixParam param;
            if (json != null)
            {
                goto Label_000C;
            }
            throw new InvalidJSONException();
        Label_000C:
            this.mExp = json.exp;
            this.mGold = json.gold;
            this.mLv = this.CalcLevel();
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (json.coin == null)
            {
                goto Label_009C;
            }
            this.mFreeCoin = json.coin.free;
            this.mPaidCoin = json.coin.paid;
            this.mComCoin = json.coin.com;
        Label_009C:
            if (json.stamina == null)
            {
                goto Label_011A;
            }
            this.mStamina.val = json.stamina.pt;
            this.mStamina.valMax = json.stamina.max;
            this.mStamina.valRecover = param.StaminaRecoveryVal;
            this.mStamina.interval = param.StaminaRecoverySec;
            this.mStamina.at = json.stamina.at;
        Label_011A:
            this.UpdateUnlocks();
            return;
        }

        public void Deserialize(SRPG.FriendPresentReceiveList.Json[] jsons)
        {
            Exception exception;
        Label_0000:
            try
            {
                this.FriendPresentReceiveList.Clear();
                if (jsons == null)
                {
                    goto Label_001D;
                }
                this.FriendPresentReceiveList.Deserialize(jsons);
            Label_001D:
                goto Label_002E;
            }
            catch (Exception exception1)
            {
            Label_0022:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_002E;
            }
        Label_002E:
            return;
        }

        public void Deserialize(SRPG.FriendPresentWishList.Json[] jsons)
        {
            Exception exception;
        Label_0000:
            try
            {
                this.FriendPresentWishList.Clear();
                if (jsons == null)
                {
                    goto Label_001D;
                }
                this.FriendPresentWishList.Deserialize(jsons);
            Label_001D:
                goto Label_002E;
            }
            catch (Exception exception1)
            {
            Label_0022:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_002E;
            }
        Label_002E:
            return;
        }

        public void Deserialize(Json_Friend[] friends)
        {
            this.Deserialize(friends, 1);
            this.Deserialize(friends, 3);
            this.Deserialize(friends, 2);
            return;
        }

        public void Deserialize(Json_Item[] items)
        {
            int num;
            ItemData data;
            bool flag;
            Exception exception;
            if (items != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.mItems != null)
            {
                goto Label_0020;
            }
            this.mItems = new List<ItemData>((int) items.Length);
        Label_0020:
            num = 0;
            goto Label_00DB;
        Label_0027:
            data = this.FindByItemID(items[num].iname);
            flag = 0;
            if (data != null)
            {
                goto Label_006D;
            }
            data = new ItemData();
            data.IsNew = 1;
            this.mItems.Add(data);
            this.mID2ItemData[items[num].iname] = data;
            flag = 1;
        Label_006D:
            try
            {
                data.Deserialize(items[num]);
                if (flag == null)
                {
                    goto Label_00A0;
                }
                data.IsNewSkin = (data.Param == null) ? 0 : (data.Param.type == 15);
            Label_00A0:
                goto Label_00D7;
            }
            catch (Exception exception1)
            {
            Label_00A5:
                exception = exception1;
                this.mItems.Remove(data);
                this.mID2ItemData.Remove(items[num].iname);
                DebugUtility.LogException(exception);
                goto Label_00D7;
            }
        Label_00D7:
            num += 1;
        Label_00DB:
            if (num < ((int) items.Length))
            {
                goto Label_0027;
            }
            this.UpdateInventory();
            return;
        }

        public bool Deserialize(Json_Mail[] mails)
        {
            int num;
            MailData data;
            this.Mails.Clear();
            if (mails != null)
            {
                goto Label_0013;
            }
            return 1;
        Label_0013:
            num = 0;
            goto Label_004A;
        Label_001A:
            data = new MailData();
            if (data.Deserialize(mails[num]) != null)
            {
                goto Label_003A;
            }
            DebugUtility.Assert("Failed Mail Deserialize.");
            return 0;
        Label_003A:
            this.Mails.Add(data);
            num += 1;
        Label_004A:
            if (num < ((int) mails.Length))
            {
                goto Label_001A;
            }
            return 1;
        }

        public void Deserialize(JSON_ConceptCard concept_cards)
        {
            ConceptCardData data;
            Exception exception;
            UnitData data2;
            <Deserialize>c__AnonStorey232 storey;
            storey = new <Deserialize>c__AnonStorey232();
            storey.concept_cards = concept_cards;
            if (storey.concept_cards != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            data = this.FindConceptCardByUniqueID(storey.concept_cards.iid);
            if (data == null)
            {
                goto Label_004F;
            }
        Label_0031:
            try
            {
                data.Deserialize(storey.concept_cards);
                goto Label_004F;
            }
            catch (Exception exception1)
            {
            Label_0043:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_004F;
            }
        Label_004F:
            data2 = this.Units.Find(new Predicate<UnitData>(storey.<>m__139));
            if (data2 == null)
            {
                goto Label_0074;
            }
            data2.ConceptCard = data;
        Label_0074:
            return;
        }

        public void Deserialize(Json_MultiFuids[] fuids)
        {
            int num;
            MultiFuid fuid;
            Exception exception;
            this.MultiFuids.Clear();
            if (fuids == null)
            {
                goto Label_0052;
            }
            num = 0;
            goto Label_0049;
        Label_0018:
            fuid = new MultiFuid();
        Label_001E:
            try
            {
                fuid.Deserialize(fuids[num]);
                this.MultiFuids.Add(fuid);
                goto Label_0045;
            }
            catch (Exception exception1)
            {
            Label_0039:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0045;
            }
        Label_0045:
            num += 1;
        Label_0049:
            if (num < ((int) fuids.Length))
            {
                goto Label_0018;
            }
        Label_0052:
            return;
        }

        public unsafe void Deserialize(Json_Party[] parties)
        {
            int num;
            int num2;
            int num3;
            int num4;
            PartyWindow2.EditPartyTypes types;
            List<PartyEditData> list;
            int num5;
            int num6;
            PartyEditData data;
            num = 0;
            goto Label_001C;
        Label_0007:
            this.mPartys[num].Reset();
            num += 1;
        Label_001C:
            if (num < this.mPartys.Count)
            {
                goto Label_0007;
            }
            if (parties != null)
            {
                goto Label_0039;
            }
            throw new InvalidJSONException();
        Label_0039:
            num2 = 0;
            goto Label_00EC;
        Label_0040:
            num3 = num2;
            if (string.IsNullOrEmpty(parties[num2].ptype) != null)
            {
                goto Label_0062;
            }
            num3 = PartyData.GetPartyTypeFromString(parties[num2].ptype);
        Label_0062:
            this.mPartys[num3].Deserialize(parties[num2]);
            num4 = 0;
            types = SRPG_Extensions.ToEditPartyType(num3);
            if (PartyUtility.LoadTeamPresets(types, &num4, 0) != null)
            {
                goto Label_00E8;
            }
            num5 = SRPG_Extensions.GetMaxTeamCount(types);
            list = new List<PartyEditData>();
            num6 = 0;
            goto Label_00D4;
        Label_00AB:
            data = new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(num6), this.mPartys[num3]);
            list.Add(data);
            num6 += 1;
        Label_00D4:
            if (num6 < num5)
            {
                goto Label_00AB;
            }
            PartyUtility.SaveTeamPresets(types, 0, list, 0);
        Label_00E8:
            num2 += 1;
        Label_00EC:
            if (num2 < ((int) parties.Length))
            {
                goto Label_0040;
            }
            return;
        }

        public void Deserialize(Json_Skin[] skins)
        {
            int num;
            if (this.mSkins != null)
            {
                goto Label_0016;
            }
            this.mSkins = new List<string>();
        Label_0016:
            this.mSkins.Clear();
            if (skins == null)
            {
                goto Label_0030;
            }
            if (((int) skins.Length) >= 1)
            {
                goto Label_0031;
            }
        Label_0030:
            return;
        Label_0031:
            num = 0;
            goto Label_0069;
        Label_0038:
            if (skins[num] == null)
            {
                goto Label_0065;
            }
            if (string.IsNullOrEmpty(skins[num].iname) != null)
            {
                goto Label_0065;
            }
            this.mSkins.Add(skins[num].iname);
        Label_0065:
            num += 1;
        Label_0069:
            if (num < ((int) skins.Length))
            {
                goto Label_0038;
            }
            return;
        }

        public void Deserialize(Json_Support[] supports)
        {
            int num;
            SupportData data;
            Exception exception;
            this.Supports.Clear();
            if (supports == null)
            {
                goto Label_0051;
            }
            num = 0;
            goto Label_0048;
        Label_0018:
            data = new SupportData();
        Label_001E:
            try
            {
                data.Deserialize(supports[num]);
                this.Supports.Add(data);
                goto Label_0044;
            }
            catch (Exception exception1)
            {
            Label_0038:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0044;
            }
        Label_0044:
            num += 1;
        Label_0048:
            if (num < ((int) supports.Length))
            {
                goto Label_0018;
            }
        Label_0051:
            return;
        }

        public void Deserialize(Json_Unit[] units)
        {
            int num;
            UnitData data;
            Exception exception;
            if (units != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.mUnits != null)
            {
                goto Label_0028;
            }
            this.mUnits = new List<UnitData>(this.mUnitCap);
        Label_0028:
            num = 0;
            goto Label_00AE;
        Label_002F:
            data = this.FindUnitDataByUniqueID(units[num].iid);
            if (data != null)
            {
                goto Label_006A;
            }
            data = new UnitData();
            this.mUnits.Add(data);
            this.mUniqueID2UnitData[units[num].iid] = data;
        Label_006A:
            try
            {
                data.Deserialize(units[num]);
                goto Label_00AA;
            }
            catch (Exception exception1)
            {
            Label_0078:
                exception = exception1;
                this.mUnits.Remove(data);
                this.mUniqueID2UnitData.Remove(units[num].iid);
                DebugUtility.LogException(exception);
                goto Label_00AA;
            }
        Label_00AA:
            num += 1;
        Label_00AE:
            if (num < ((int) units.Length))
            {
                goto Label_002F;
            }
            return;
        }

        public void Deserialize(Json_Versus json)
        {
            VERSUS_TYPE versus_type;
            int num;
            versus_type = 0;
            this.mVersusPoint = json.point;
            if (json.counts == null)
            {
                goto Label_00ED;
            }
            num = 0;
            goto Label_00DF;
        Label_0020:
            if (string.Compare(json.counts[num].type, ((VERSUS_TYPE) 0).ToString().ToLower()) != null)
            {
                goto Label_004E;
            }
            versus_type = 0;
            goto Label_00A5;
        Label_004E:
            if (string.Compare(json.counts[num].type, ((VERSUS_TYPE) 1).ToString().ToLower()) != null)
            {
                goto Label_007C;
            }
            versus_type = 1;
            goto Label_00A5;
        Label_007C:
            if (string.Compare(json.counts[num].type, ((VERSUS_TYPE) 2).ToString().ToLower()) != null)
            {
                goto Label_00A5;
            }
            versus_type = 2;
        Label_00A5:
            this.SetVersusWinCount(versus_type, json.counts[num].win);
            this.SetVersusTotalCount(versus_type, json.counts[num].win + json.counts[num].lose);
            num += 1;
        Label_00DF:
            if (num < ((int) json.counts.Length))
            {
                goto Label_0020;
            }
        Label_00ED:
            return;
        }

        public void Deserialize(Json_Friend[] friends, FriendStates state)
        {
            int num;
            FriendData data;
            Exception exception;
            FriendStates states;
            states = state;
            switch ((states - 1))
            {
                case 0:
                    goto Label_001B;

                case 1:
                    goto Label_003B;

                case 2:
                    goto Label_002B;
            }
            goto Label_004B;
        Label_001B:
            this.Friends.Clear();
            goto Label_004C;
        Label_002B:
            this.FriendsFollower.Clear();
            goto Label_004C;
        Label_003B:
            this.FriendsFollow.Clear();
            goto Label_004C;
        Label_004B:
            return;
        Label_004C:
            if (friends == null)
            {
                goto Label_012C;
            }
            num = 0;
            goto Label_00DC;
        Label_0059:
            data = new FriendData();
        Label_005F:
            try
            {
                data.Deserialize(friends[num]);
                if (data.State != state)
                {
                    goto Label_00C7;
                }
                switch ((data.State - 1))
                {
                    case 0:
                        goto Label_0094;

                    case 1:
                        goto Label_00B6;

                    case 2:
                        goto Label_00A5;
                }
                goto Label_00C7;
            Label_0094:
                this.Friends.Add(data);
                goto Label_00C7;
            Label_00A5:
                this.FriendsFollower.Add(data);
                goto Label_00C7;
            Label_00B6:
                this.FriendsFollow.Add(data);
            Label_00C7:
                goto Label_00D8;
            }
            catch (Exception exception1)
            {
            Label_00CC:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00D8;
            }
        Label_00D8:
            num += 1;
        Label_00DC:
            if (num < ((int) friends.Length))
            {
                goto Label_0059;
            }
            states = state;
            switch ((states - 1))
            {
                case 0:
                    goto Label_0100;

                case 1:
                    goto Label_012C;

                case 2:
                    goto Label_0116;
            }
            goto Label_012C;
        Label_0100:
            this.FriendNum = this.Friends.Count;
            goto Label_012C;
        Label_0116:
            this.FollowerNum = this.FriendsFollower.Count;
        Label_012C:
            return;
        }

        public bool Deserialize(Json_PlayerData json, EDeserializeFlags flag)
        {
            FixParam param;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if ((flag & 1) != 1)
            {
                goto Label_0043;
            }
            this.mGold = json.gold;
            this.mGoldBuyNum = json.cnt_buygold;
        Label_0043:
            if ((flag & 2) != 2)
            {
                goto Label_0099;
            }
            if (json.coin == null)
            {
                goto Label_0099;
            }
            this.mFreeCoin = json.coin.free;
            this.mPaidCoin = json.coin.paid;
            this.mComCoin = json.coin.com;
        Label_0099:
            if ((flag & 4) != 4)
            {
                goto Label_0131;
            }
            if (json.stamina == null)
            {
                goto Label_0120;
            }
            this.mStamina.val = json.stamina.pt;
            this.mStamina.valMax = json.stamina.max;
            this.mStamina.valRecover = param.StaminaRecoveryVal;
            this.mStamina.interval = param.StaminaRecoverySec;
            this.mStamina.at = json.stamina.at;
        Label_0120:
            this.mStaminaBuyNum = json.cnt_stmrecover;
        Label_0131:
            if ((flag & 8) != 8)
            {
                goto Label_01AE;
            }
            if (json.cave == null)
            {
                goto Label_01AE;
            }
            this.mCaveStamina.val = json.cave.pt;
            this.mCaveStamina.valMax = param.CaveStaminaMax;
            this.mCaveStamina.valRecover = param.CaveStaminaRecoveryVal;
            this.mCaveStamina.interval = param.CaveStaminaRecoverySec;
            this.mCaveStamina.at = json.cave.at;
        Label_01AE:
            if ((flag & 0x10) != 0x10)
            {
                goto Label_022D;
            }
            if (json.abilup == null)
            {
                goto Label_022D;
            }
            this.mAbilityRankUpCount.val = json.abilup.num;
            this.mAbilityRankUpCount.valMax = param.AbilityRankUpCountMax;
            this.mAbilityRankUpCount.valRecover = param.AbilityRankUpCountRecoveryVal;
            this.mAbilityRankUpCount.interval = param.AbilityRankUpCountRecoverySec;
            this.mAbilityRankUpCount.at = json.abilup.at;
        Label_022D:
            if ((flag & 0x20) != 0x20)
            {
                goto Label_02B8;
            }
            if (json.arena == null)
            {
                goto Label_02B8;
            }
            this.mChallengeArenaNum = json.arena.num;
            this.mChallengeArenaTimer.valMax = 1;
            this.mChallengeArenaTimer.valRecover = 1;
            this.mChallengeArenaTimer.interval = param.ChallengeArenaCoolDownSec;
            this.mChallengeArenaTimer.at = json.arena.at;
            this.mArenaResetCount = json.arena.cnt_resetcost;
        Label_02B8:
            if ((flag & 0x40) != 0x40)
            {
                goto Label_02E4;
            }
            if (json.tour == null)
            {
                goto Label_02E4;
            }
            this.mTourNum = json.tour.num;
        Label_02E4:
            return 1;
        }

        public void Deserialize(Json_Artifact[] items, bool differenceUpdate)
        {
            int num;
            bool flag;
            ArtifactData data;
            Exception exception;
            <Deserialize>c__AnonStorey231 storey;
            if (items != null)
            {
                goto Label_001D;
            }
            this.mArtifacts.Clear();
            this.mArtifactsNumByRarity.Clear();
            return;
        Label_001D:
            num = 0;
            goto Label_008B;
        Label_0024:
            flag = 0;
            data = this.FindArtifactByUniqueID(items[num].iid);
            if (data != null)
            {
                goto Label_0048;
            }
            data = new ArtifactData();
            flag = 1;
            goto Label_004F;
        Label_0048:
            this.RemoveArtifactNumByRarity(data);
        Label_004F:
            try
            {
                data.Deserialize(items[num]);
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_005D:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0087;
            }
        Label_006E:
            if (flag == null)
            {
                goto Label_0080;
            }
            this.mArtifacts.Add(data);
        Label_0080:
            this.AddArtifaceNumByRarity(data);
        Label_0087:
            num += 1;
        Label_008B:
            if (num < ((int) items.Length))
            {
                goto Label_0024;
            }
            if (differenceUpdate != null)
            {
                goto Label_0124;
            }
            storey = new <Deserialize>c__AnonStorey231();
            storey.<>f__this = this;
            storey.i = 0;
            goto Label_010D;
        Label_00B6:
            if (Array.Find<Json_Artifact>(items, new Predicate<Json_Artifact>(storey.<>m__138)) == null)
            {
                goto Label_00E3;
            }
            storey.i += 1;
            goto Label_010D;
        Label_00E3:
            this.RemoveArtifactNumByRarity(this.mArtifacts[storey.i]);
            this.mArtifacts.RemoveAt(storey.i);
        Label_010D:
            if (storey.i < this.mArtifacts.Count)
            {
                goto Label_00B6;
            }
        Label_0124:
            return;
        }

        public void Deserialize(JSON_ConceptCard[] concept_cards, bool is_data_override)
        {
            ConceptCardData data;
            Exception exception;
            UnitData data2;
            UnitData data3;
            <Deserialize>c__AnonStorey233 storey;
            <Deserialize>c__AnonStorey234 storey2;
            <Deserialize>c__AnonStorey235 storey3;
            storey = new <Deserialize>c__AnonStorey233();
            storey.concept_cards = concept_cards;
            storey.<>f__this = this;
            if (is_data_override == null)
            {
                goto Label_004F;
            }
            if (storey.concept_cards == null)
            {
                goto Label_0038;
            }
            if (((int) storey.concept_cards.Length) > 0)
            {
                goto Label_004F;
            }
        Label_0038:
            this.mConceptCards.Clear();
            this.mConceptCardNum.Clear();
            return;
        Label_004F:
            if (storey.concept_cards != null)
            {
                goto Label_005C;
            }
            return;
        Label_005C:
            storey2 = new <Deserialize>c__AnonStorey234();
            storey2.<>f__ref$563 = storey;
            storey2.i = 0;
            goto Label_010E;
        Label_0079:
            data = this.FindConceptCardByUniqueID(storey.concept_cards[storey2.i].iid);
            if (data != null)
            {
                goto Label_00D8;
            }
        Label_009A:
            try
            {
                data = new ConceptCardData();
                data.Deserialize(storey.concept_cards[storey2.i]);
                this.mConceptCards.Add(data);
                goto Label_00D8;
            }
            catch (Exception exception1)
            {
            Label_00C7:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00FE;
            }
        Label_00D8:
            data2 = this.Units.Find(new Predicate<UnitData>(storey2.<>m__13A));
            if (data2 == null)
            {
                goto Label_00FE;
            }
            data2.ConceptCard = data;
        Label_00FE:
            storey2.i += 1;
        Label_010E:
            if (storey2.i < ((int) storey.concept_cards.Length))
            {
                goto Label_0079;
            }
            if (is_data_override == null)
            {
                goto Label_01C7;
            }
            storey3 = new <Deserialize>c__AnonStorey235();
            storey3.<>f__this = this;
            storey3.i = 0;
            goto Label_01B0;
        Label_0145:
            if (Array.Find<JSON_ConceptCard>(storey.concept_cards, new Predicate<JSON_ConceptCard>(storey3.<>m__13B)) == null)
            {
                goto Label_0178;
            }
            storey3.i += 1;
            goto Label_01B0;
        Label_0178:
            data3 = this.Units.Find(new Predicate<UnitData>(storey3.<>m__13C));
            if (data3 == null)
            {
                goto Label_019E;
            }
            data3.ConceptCard = null;
        Label_019E:
            this.mConceptCards.RemoveAt(storey3.i);
        Label_01B0:
            if (storey3.i < this.mConceptCards.Count)
            {
                goto Label_0145;
            }
        Label_01C7:
            this.UpdateConceptCardNum();
            return;
        }

        public void Deserialize(JSON_ConceptCardMaterial[] concept_card_materials, bool is_data_override)
        {
            int num;
            ConceptCardParam param;
            ConceptCardMaterialData data;
            Exception exception;
            if (is_data_override == null)
            {
                goto Label_002C;
            }
            if (concept_card_materials == null)
            {
                goto Label_0015;
            }
            if (((int) concept_card_materials.Length) > 0)
            {
                goto Label_002C;
            }
        Label_0015:
            this.mConceptCardExpMaterials.Clear();
            this.mConceptCardTrustMaterials.Clear();
            return;
        Label_002C:
            if (concept_card_materials != null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            this.mConceptCardExpMaterials.Clear();
            this.mConceptCardTrustMaterials.Clear();
            num = 0;
            goto Label_00CD;
        Label_0050:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(concept_card_materials[num].iname);
            if (param == null)
            {
                goto Label_00C9;
            }
        Label_006E:
            try
            {
                data = new ConceptCardMaterialData();
                data.Deserialize(concept_card_materials[num]);
                if (param.type != 2)
                {
                    goto Label_009B;
                }
                this.mConceptCardExpMaterials.Add(data);
                goto Label_00B3;
            Label_009B:
                if (param.type != 3)
                {
                    goto Label_00B3;
                }
                this.mConceptCardTrustMaterials.Add(data);
            Label_00B3:
                goto Label_00C9;
            }
            catch (Exception exception1)
            {
            Label_00B8:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_00C9;
            }
        Label_00C9:
            num += 1;
        Label_00CD:
            if (num < ((int) concept_card_materials.Length))
            {
                goto Label_0050;
            }
            return;
        }

        public int EventCoinNum(string cost_iname)
        {
            EventCoinData data;
            <EventCoinNum>c__AnonStorey243 storey;
            storey = new <EventCoinNum>c__AnonStorey243();
            storey.cost_iname = cost_iname;
            if (storey.cost_iname != null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            data = this.mEventCoinList.Find(new Predicate<EventCoinData>(storey.<>m__14B));
            if (data == null)
            {
                goto Label_004F;
            }
            if (data.have == null)
            {
                goto Label_004F;
            }
            return data.have.Num;
        Label_004F:
            return 0;
        }

        public ArtifactData FindArtifactByUniqueID(long iid)
        {
            <FindArtifactByUniqueID>c__AnonStorey238 storey;
            storey = new <FindArtifactByUniqueID>c__AnonStorey238();
            storey.iid = iid;
            return this.mArtifacts.Find(new Predicate<ArtifactData>(storey.<>m__13F));
        }

        public List<ArtifactData> FindArtifactsByArtifactID(string iname)
        {
            <FindArtifactsByArtifactID>c__AnonStorey23A storeya;
            storeya = new <FindArtifactsByArtifactID>c__AnonStorey23A();
            storeya.iname = iname;
            return this.mArtifacts.FindAll(new Predicate<ArtifactData>(storeya.<>m__141));
        }

        public List<ArtifactData> FindArtifactsByIDs(HashSet<string> ids)
        {
            <FindArtifactsByIDs>c__AnonStorey239 storey;
            storey = new <FindArtifactsByIDs>c__AnonStorey239();
            storey.ids = ids;
            return this.mArtifacts.FindAll(new Predicate<ArtifactData>(storey.<>m__140));
        }

        private ItemData FindByItemID(string itemID)
        {
            ItemData data;
        Label_0000:
            try
            {
                data = this.mID2ItemData[itemID];
                goto Label_0024;
            }
            catch (Exception)
            {
            Label_0017:
                data = null;
                goto Label_0024;
            }
        Label_0024:
            return data;
        }

        public ConceptCardData FindConceptCardByUniqueID(long iid)
        {
            <FindConceptCardByUniqueID>c__AnonStorey245 storey;
            storey = new <FindConceptCardByUniqueID>c__AnonStorey245();
            storey.iid = iid;
            return this.mConceptCards.Find(new Predicate<ConceptCardData>(storey.<>m__14D));
        }

        public ItemData FindInventoryByItemID(string iname)
        {
            int num;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            num = 0;
            goto Label_0046;
        Label_0014:
            if (this.mInventory[num] == null)
            {
                goto Label_0042;
            }
            if ((iname == this.mInventory[num].ItemID) == null)
            {
                goto Label_0042;
            }
            return this.mInventory[num];
        Label_0042:
            num += 1;
        Label_0046:
            if (num < ((int) this.mInventory.Length))
            {
                goto Label_0014;
            }
            return null;
        }

        public ItemData FindItemDataByItemID(string iname)
        {
            <FindItemDataByItemID>c__AnonStorey236 storey;
            storey = new <FindItemDataByItemID>c__AnonStorey236();
            storey.iname = iname;
            if (string.IsNullOrEmpty(storey.iname) == null)
            {
                goto Label_001F;
            }
            return null;
        Label_001F:
            return this.mItems.Find(new Predicate<ItemData>(storey.<>m__13D));
        }

        public ItemData FindItemDataByItemParam(ItemParam param)
        {
            <FindItemDataByItemParam>c__AnonStorey237 storey;
            storey = new <FindItemDataByItemParam>c__AnonStorey237();
            storey.param = param;
            return this.mItems.Find(new Predicate<ItemData>(storey.<>m__13E));
        }

        public QuestParam FindLastStoryQuest()
        {
            QuestParam[] paramArray;
            int num;
            string str;
            QuestParam param;
            int num2;
            QuestParam param2;
            int num3;
            paramArray = this.AvailableQuests;
            num = 0;
            str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.LAST_SELECTED_STORY_QUEST_ID, string.Empty);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0078;
            }
            param = MonoSingleton<GameManager>.Instance.FindQuest(str);
            if (param == null)
            {
                goto Label_0078;
            }
            if (param.Chapter == null)
            {
                goto Label_0078;
            }
            if (param.Chapter.sectionParam == null)
            {
                goto Label_0078;
            }
            if (param.Chapter.sectionParam.storyPart <= 0)
            {
                goto Label_0078;
            }
            num = param.Chapter.sectionParam.storyPart;
        Label_0078:
            num2 = 0;
            goto Label_017F;
        Label_0080:
            if (paramArray[num2].IsStory == null)
            {
                goto Label_0179;
            }
            if (string.IsNullOrEmpty(paramArray[num2].ChapterID) != null)
            {
                goto Label_0179;
            }
            if (num <= 0)
            {
                goto Label_00E7;
            }
            if (paramArray[num2].Chapter == null)
            {
                goto Label_00E7;
            }
            if (paramArray[num2].Chapter.sectionParam == null)
            {
                goto Label_00E7;
            }
            if (num == paramArray[num2].Chapter.sectionParam.storyPart)
            {
                goto Label_00E7;
            }
            goto Label_0179;
        Label_00E7:
            param2 = paramArray[num2];
            num3 = num2 + 1;
            goto Label_016C;
        Label_00F8:
            if (paramArray[num3].IsStory == null)
            {
                goto Label_0166;
            }
            if (num <= 0)
            {
                goto Label_014C;
            }
            if (paramArray[num3].Chapter == null)
            {
                goto Label_014C;
            }
            if (paramArray[num3].Chapter.sectionParam == null)
            {
                goto Label_014C;
            }
            if (num == paramArray[num3].Chapter.sectionParam.storyPart)
            {
                goto Label_014C;
            }
            goto Label_0166;
        Label_014C:
            param2 = paramArray[num3];
            if (paramArray[num3].state == 2)
            {
                goto Label_0166;
            }
            return paramArray[num3];
        Label_0166:
            num3 += 1;
        Label_016C:
            if (num3 < ((int) paramArray.Length))
            {
                goto Label_00F8;
            }
            return param2;
        Label_0179:
            num2 += 1;
        Label_017F:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0080;
            }
            return null;
        }

        public Json_LoginBonus[] FindLoginBonuses(string type)
        {
            if (string.IsNullOrEmpty(type) == null)
            {
                goto Label_0012;
            }
            return this.mLoginBonus;
        Label_0012:
            if (this.mLoginBonusTables.ContainsKey(type) != null)
            {
                goto Label_0025;
            }
            return null;
        Label_0025:
            return this.mLoginBonusTables[type].bonuses;
        }

        public bool FindOwner(ArtifactData arti, out UnitData unit, out JobData job)
        {
            int num;
            int num2;
            int num3;
            *(unit) = null;
            *(job) = null;
            num = 0;
            goto Label_00AF;
        Label_000D:
            num2 = 0;
            goto Label_0092;
        Label_0014:
            num3 = 0;
            goto Label_006E;
        Label_001B:
            if (this.mUnits[num].Jobs[num2].Artifacts[num3] != arti.UniqueID)
            {
                goto Label_006A;
            }
            *(unit) = this.mUnits[num];
            *(job) = this.mUnits[num].Jobs[num2];
            return 1;
        Label_006A:
            num3 += 1;
        Label_006E:
            if (num3 < ((int) this.mUnits[num].Jobs[num2].Artifacts.Length))
            {
                goto Label_001B;
            }
            num2 += 1;
        Label_0092:
            if (num2 < ((int) this.mUnits[num].Jobs.Length))
            {
                goto Label_0014;
            }
            num += 1;
        Label_00AF:
            if (num < this.mUnits.Count)
            {
                goto Label_000D;
            }
            return 0;
        }

        public PartyData FindPartyOfType(PlayerPartyTypes type)
        {
            return this.mPartys[type];
        }

        public Json_LoginBonus FindRecentLoginBonus(string type)
        {
            Json_LoginBonus[] bonusArray;
            int num;
            bonusArray = this.FindLoginBonuses(type);
            if (bonusArray != null)
            {
                goto Label_0010;
            }
            return null;
        Label_0010:
            num = this.LoginCountWithType(type);
            if (num < 1)
            {
                goto Label_0028;
            }
            if (((int) bonusArray.Length) >= num)
            {
                goto Label_002A;
            }
        Label_0028:
            return null;
        Label_002A:
            return bonusArray[num - 1];
        }

        public UnitData FindUnitDataByUniqueID(long iid)
        {
            UnitData data;
        Label_0000:
            try
            {
                data = this.mUniqueID2UnitData[iid];
                goto Label_0024;
            }
            catch (Exception)
            {
            Label_0017:
                data = null;
                goto Label_0024;
            }
        Label_0024:
            return data;
        }

        public UnitData FindUnitDataByUniqueParam(UnitParam unit)
        {
            int num;
            num = 0;
            goto Label_002F;
        Label_0007:
            if (unit != this.mUnits[num].UnitParam)
            {
                goto Label_002B;
            }
            return this.mUnits[num];
        Label_002B:
            num += 1;
        Label_002F:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public UnitData FindUnitDataByUnitID(string iname)
        {
            int num;
            num = 0;
            goto Label_0039;
        Label_0007:
            if ((iname == this.mUnits[num].UnitParam.iname) == null)
            {
                goto Label_0035;
            }
            return this.mUnits[num];
        Label_0035:
            num += 1;
        Label_0039:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public void ForceFirstLogin()
        {
            this.mFirstLogin = 1;
            return;
        }

        public void GainExp(int exp)
        {
            int num;
            num = this.mLv;
            this.mExp += exp;
            this.mLv = this.CalcLevel();
            if (num == this.mLv)
            {
                goto Label_0059;
            }
            this.PlayerLevelUp(this.mLv - num);
        Label_0059:
            return;
        }

        public void GainGold(int gold)
        {
            this.mGold = Math.Max(this.mGold + gold, 0);
            return;
        }

        public void GainItem(string itemID, int num)
        {
            ItemData data;
            data = this.FindByItemID(itemID);
            if (data != null)
            {
                goto Label_0056;
            }
            data = new ItemData();
            data.Setup(0L, itemID, num);
            data.IsNew = 1;
            data.IsNewSkin = (data.Param == null) ? 0 : (data.Param.type == 15);
            this.Items.Add(data);
            return;
        Label_0056:
            data.Gain(num);
            return;
        }

        public void GainPiecePoint(int point)
        {
            this.mPiecePoint = Math.Max(this.mPiecePoint + point, 0);
            return;
        }

        public void GainUnit(UnitData unit)
        {
            this.mUnits.Add(unit);
            this.mUniqueID2UnitData[unit.UniqueID] = unit;
            return;
        }

        public unsafe void GainUnit(string unitID)
        {
            UnitParam param;
            UnitData data;
            List<long> list;
            UnitData data2;
            List<UnitData>.Enumerator enumerator;
            long num;
            long num2;
            bool flag;
            long num3;
            List<long>.Enumerator enumerator2;
            Json_Unit unit;
            List<Json_Job> list2;
            int num4;
            int num5;
            JobSetParam param2;
            Json_Job job;
            int num6;
            JobSetParam param3;
            Json_Job job2;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(unitID);
            data = new UnitData();
            list = new List<long>();
            enumerator = this.mUnits.GetEnumerator();
        Label_002A:
            try
            {
                goto Label_0043;
            Label_002F:
                data2 = &enumerator.Current;
                list.Add(data2.UniqueID);
            Label_0043:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002F;
                }
                goto Label_0061;
            }
            finally
            {
            Label_0054:
                ((List<UnitData>.Enumerator) enumerator).Dispose();
            }
        Label_0061:
            num = 1L;
            num2 = 1L;
            goto Label_00CD;
        Label_006E:
            flag = 0;
            enumerator2 = list.GetEnumerator();
        Label_0079:
            try
            {
                goto Label_0098;
            Label_007E:
                num3 = &enumerator2.Current;
                if (num2 != num3)
                {
                    goto Label_0098;
                }
                flag = 1;
                goto Label_00A4;
            Label_0098:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_007E;
                }
            Label_00A4:
                goto Label_00B6;
            }
            finally
            {
            Label_00A9:
                ((List<long>.Enumerator) enumerator2).Dispose();
            }
        Label_00B6:
            if (flag != null)
            {
                goto Label_00C6;
            }
            num = num2;
            goto Label_00DA;
        Label_00C6:
            num2 += 1L;
        Label_00CD:
            if (num2 < 0x3e8L)
            {
                goto Label_006E;
            }
        Label_00DA:
            unit = new Json_Unit();
            unit.iid = num;
            unit.iname = param.iname;
            unit.exp = 0;
            unit.lv = 1;
            unit.plus = 0;
            unit.rare = 0;
            unit.select = new Json_UnitSelectable();
            unit.select.job = 0L;
            unit.jobs = null;
            unit.abil = null;
            if (param.jobsets == null)
            {
                goto Label_02B9;
            }
            if (((int) param.jobsets.Length) <= 0)
            {
                goto Label_02B9;
            }
            list2 = new List<Json_Job>((int) param.jobsets.Length);
            num4 = 1;
            num5 = 0;
            goto Label_01E8;
        Label_0174:
            param2 = MonoSingleton<GameManager>.Instance.GetJobSetParam(param.jobsets[num5]);
            if (param2 != null)
            {
                goto Label_0195;
            }
            goto Label_01E2;
        Label_0195:
            job = new Json_Job();
            job.iid = (long) num4++;
            job.iname = param2.job;
            job.rank = 0;
            job.equips = null;
            job.abils = null;
            job.artis = null;
            list2.Add(job);
        Label_01E2:
            num5 += 1;
        Label_01E8:
            if (num5 < ((int) param.jobsets.Length))
            {
                goto Label_0174;
            }
            num6 = 0;
            goto Label_029C;
        Label_01FF:
            param3 = MonoSingleton<GameManager>.Instance.GetJobSetParam(param.jobsets[num6]);
            goto Label_0285;
        Label_0219:
            param3 = MonoSingleton<GameManager>.Instance.GetJobSetParam(param3.jobchange);
            if (param3 != null)
            {
                goto Label_0238;
            }
            goto Label_0296;
        Label_0238:
            job2 = new Json_Job();
            job2.iid = (long) num4++;
            job2.iname = param3.job;
            job2.rank = 0;
            job2.equips = null;
            job2.abils = null;
            job2.artis = null;
            list2.Add(job2);
        Label_0285:
            if (string.IsNullOrEmpty(param3.jobchange) == null)
            {
                goto Label_0219;
            }
        Label_0296:
            num6 += 1;
        Label_029C:
            if (num6 < ((int) param.jobsets.Length))
            {
                goto Label_01FF;
            }
            unit.jobs = list2.ToArray();
        Label_02B9:
            data.Deserialize(unit);
            data.SetUniqueID(num);
            data.JobRankUp(0);
            this.mUnits.Add(data);
            this.mUniqueID2UnitData[data.UniqueID] = data;
            return;
        }

        public void GainVipPoint(int point)
        {
        }

        public long GenerateUnitUniqueID()
        {
            long num;
            int num2;
            num = 0L;
            num2 = 0;
            goto Label_0037;
        Label_000A:
            if (this.mUnits[num2].UniqueID <= num)
            {
                goto Label_0033;
            }
            num = this.mUnits[num2].UniqueID;
        Label_0033:
            num2 += 1;
        Label_0037:
            if (num2 < this.mUnits.Count)
            {
                goto Label_000A;
            }
            return (num + 1L);
        }

        public unsafe int GetArtifactNumByRarity(string iname, int rarity)
        {
            Dictionary<int, int> dictionary;
            int num;
            if (string.IsNullOrEmpty(iname) != null)
            {
                goto Label_002E;
            }
            if (this.mArtifactsNumByRarity.TryGetValue(iname, &dictionary) == null)
            {
                goto Label_002E;
            }
            if (dictionary.TryGetValue(rarity, &num) == null)
            {
                goto Label_002E;
            }
            return num;
        Label_002E:
            return 0;
        }

        public unsafe DateTime GetBeginnerEndTime()
        {
            double num;
            DateTime time;
            DateTime time2;
            num = (double) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.BeginnerDays;
            return &TimeManager.FromUnixTime((long) this.mNewGameAt).AddDays(num);
        }

        public unsafe int GetCaveStaminaRecoveryCost()
        {
            FixParam param;
            int num;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            num = 0;
            num = Math.Max(Math.Min(num, (int) param.CaveStaminaAddCost.Length), 0);
            return *(&(param.CaveStaminaAddCost[num]));
        }

        public unsafe int GetChallengeArenaCost()
        {
            FixParam param;
            int num;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (param.ArenaResetTicketCost == null)
            {
                goto Label_004E;
            }
            num = Math.Max(Math.Min(this.mArenaResetCount, ((int) param.ArenaResetTicketCost.Length) - 1), 0);
            return *(&(param.ArenaResetTicketCost[num]));
        Label_004E:
            return 0;
        }

        public TrophyParam[] GetCompletedTrophies()
        {
            List<TrophyParam> list;
            int num;
            TrophyState state;
            list = new List<TrophyParam>(this.mTrophyStates.Count);
            num = this.mTrophyStates.Count - 1;
            goto Label_0061;
        Label_0024:
            state = this.mTrophyStates[num];
            if (state.IsEnded == null)
            {
                goto Label_0041;
            }
            goto Label_005D;
        Label_0041:
            if (state.IsCompleted != null)
            {
                goto Label_0051;
            }
            goto Label_005D;
        Label_0051:
            list.Add(state.Param);
        Label_005D:
            num -= 1;
        Label_0061:
            if (num >= 0)
            {
                goto Label_0024;
            }
            return list.ToArray();
        }

        public int GetConceptCardMaterialNum(string iname)
        {
            int num;
            ConceptCardParam param;
            ConceptCardMaterialData data;
            <GetConceptCardMaterialNum>c__AnonStorey248 storey;
            storey = new <GetConceptCardMaterialNum>c__AnonStorey248();
            storey.iname = iname;
            num = 0;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(storey.iname);
            if (param != null)
            {
                goto Label_002D;
            }
            return num;
        Label_002D:
            data = null;
            if (param.type != 2)
            {
                goto Label_0058;
            }
            data = this.mConceptCardExpMaterials.Find(new Predicate<ConceptCardMaterialData>(storey.<>m__150));
            goto Label_007C;
        Label_0058:
            if (param.type != 3)
            {
                goto Label_007C;
            }
            data = this.mConceptCardTrustMaterials.Find(new Predicate<ConceptCardMaterialData>(storey.<>m__151));
        Label_007C:
            if (data == null)
            {
                goto Label_008E;
            }
            num = data.Num;
        Label_008E:
            return num;
        }

        public OLong GetConceptCardMaterialUniqueID(string iname)
        {
            OLong @long;
            ConceptCardParam param;
            ConceptCardMaterialData data;
            <GetConceptCardMaterialUniqueID>c__AnonStorey249 storey;
            storey = new <GetConceptCardMaterialUniqueID>c__AnonStorey249();
            storey.iname = iname;
            @long = -1L;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(storey.iname);
            if (param != null)
            {
                goto Label_0033;
            }
            return @long;
        Label_0033:
            data = null;
            if (param.type != 2)
            {
                goto Label_005E;
            }
            data = this.mConceptCardExpMaterials.Find(new Predicate<ConceptCardMaterialData>(storey.<>m__152));
            goto Label_0082;
        Label_005E:
            if (param.type != 3)
            {
                goto Label_0082;
            }
            data = this.mConceptCardTrustMaterials.Find(new Predicate<ConceptCardMaterialData>(storey.<>m__153));
        Label_0082:
            if (data == null)
            {
                goto Label_008F;
            }
            @long = data.UniqueID;
        Label_008F:
            return @long;
        }

        public unsafe int GetConceptCardNum(string iname)
        {
            int num;
            num = 0;
            this.mConceptCardNum.TryGetValue(iname, &num);
            return num;
        }

        public unsafe int GetCreateItemCost(ItemParam param)
        {
            bool flag;
            flag = 0;
            this.CheckEnableCreateItem(param, &flag, 1, 1, null);
            return this.mCreateItemCost;
        }

        public int GetDefensePartyIndex()
        {
            int num;
            num = 1;
            goto Label_0023;
        Label_0007:
            if (this.mPartys[num].IsDefense == null)
            {
                goto Label_001F;
            }
            return num;
        Label_001F:
            num += 1;
        Label_0023:
            if (num < this.mPartys.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public int GetEnhanceConceptCardMaterial()
        {
            int num;
            num = 0;
            if (this.mConceptCardExpMaterials == null)
            {
                goto Label_001B;
            }
            num += this.mConceptCardExpMaterials.Count;
        Label_001B:
            if (this.mConceptCardTrustMaterials == null)
            {
                goto Label_0034;
            }
            num += this.mConceptCardTrustMaterials.Count;
        Label_0034:
            return num;
        }

        public EventShopData GetEventShopData()
        {
            return this.mEventShops;
        }

        public int GetExp()
        {
            int num;
            int num2;
            num = MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerLevelExp(this.mLv);
            num2 = this.mExp - num;
            return num2;
        }

        public int GetItemAmount(string iname)
        {
            ItemData data;
            data = this.FindItemDataByItemID(iname);
            return ((data == null) ? 0 : data.Num);
        }

        public int GetItemSlotAmount()
        {
            int num;
            int num2;
            num = 0;
            num2 = 0;
            goto Label_002C;
        Label_0009:
            if (this.mItems[num2].Num != null)
            {
                goto Label_0024;
            }
            goto Label_0028;
        Label_0024:
            num += 1;
        Label_0028:
            num2 += 1;
        Label_002C:
            if (num2 < this.mItems.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public List<ItemData> GetJobRankUpReturnItemData(UnitData self, int jobNo, bool ignoreEquiped)
        {
            return self.GetJobRankUpReturnItemData(jobNo, ignoreEquiped);
        }

        public int GetLevelExp()
        {
            return MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerNextExp(this.mLv);
        }

        public LimitedShopData GetLimitedShopData()
        {
            return this.mLimitedShops;
        }

        public string GetLoginBonusePrefabName(string type)
        {
            if (string.IsNullOrEmpty(type) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mLoginBonusTables.ContainsKey(type) != null)
            {
                goto Label_0020;
            }
            return null;
        Label_0020:
            return this.mLoginBonusTables[type].prefab;
        }

        public string[] GetLoginBonuseUnitIDs(string type)
        {
            if (string.IsNullOrEmpty(type) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mLoginBonusTables.ContainsKey(type) != null)
            {
                goto Label_0020;
            }
            return null;
        Label_0020:
            return this.mLoginBonusTables[type].bonus_units;
        }

        public DateTime GetMissionClearAt()
        {
            return ((this.mMissionClearAt >= 0L) ? TimeManager.FromUnixTime(this.mMissionClearAt) : TimeManager.ServerTime);
        }

        public string GetMissionProgressString()
        {
            string str;
            int num;
            str = "\"missionprogs\":[";
            num = 0;
            goto Label_0085;
        Label_000D:
            if (num <= 0)
            {
                goto Label_0020;
            }
            str = str + ",";
        Label_0020:
            str = (((str + "{") + "\"iname\":\"" + JsonEscape.Escape(this.RankMatchMissionState[num].IName) + "\"") + ",\"prog\":" + ((int) this.RankMatchMissionState[num].Progress)) + "}";
            num += 1;
        Label_0085:
            if (num < this.RankMatchMissionState.Count)
            {
                goto Label_000D;
            }
            return (str + "]");
        }

        public long GetNextAbilityRankUpCountRecoverySec()
        {
            return this.mAbilityRankUpCount.GetNextRecoverySec();
        }

        public long GetNextCaveStaminaRecoverySec()
        {
            return this.mCaveStamina.GetNextRecoverySec();
        }

        public long GetNextChallengeArenaCoolDownSec()
        {
            return this.mChallengeArenaTimer.GetNextRecoverySec();
        }

        public int GetNextExp()
        {
            MasterParam param;
            int num;
            int num2;
            int num3;
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num = param.GetPlayerLevelCap();
            num2 = 0;
            num3 = 0;
            goto Label_004F;
        Label_001B:
            num2 += param.GetPlayerNextExp(num3 + 1);
            if (num2 > this.mExp)
            {
                goto Label_003D;
            }
            goto Label_004B;
        Label_003D:
            return (num2 - this.mExp);
        Label_004B:
            num3 += 1;
        Label_004F:
            if (num3 < num)
            {
                goto Label_001B;
            }
            return 0;
        }

        public long GetNextFreeGachaCoinCoolDownSec()
        {
            long num;
            long num2;
            long num3;
            num2 = Network.GetServerTime() - this.FreeGachaCoin.at;
            return Math.Max(MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaCoinCoolDownSec - num2, 0L);
        }

        public unsafe long GetNextFreeGachaGoldCoolDownSec()
        {
            long num;
            DateTime time;
            DateTime time2;
            long num2;
            long num3;
            num = Network.GetServerTime();
            time = TimeManager.FromUnixTime(num);
            time2 = TimeManager.FromUnixTime(this.FreeGachaGold.at);
            if (&time.Year < &time2.Year)
            {
                goto Label_0057;
            }
            if (&time.Month < &time2.Month)
            {
                goto Label_0057;
            }
            if (&time.Day >= &time2.Day)
            {
                goto Label_005A;
            }
        Label_0057:
            return 0L;
        Label_005A:
            num2 = num - this.FreeGachaGold.at;
            return Math.Max(MonoSingleton<GameManager>.Instance.MasterParam.FixParam.FreeGachaGoldCoolDownSec - num2, 0L);
        }

        public long GetNextStaminaRecoverySec()
        {
            return this.mStamina.GetNextRecoverySec();
        }

        public PartyData GetPartyCurrent()
        {
            int num;
            num = this.GetPartyCurrentIndex();
            return this.Partys[num];
        }

        public int GetPartyCurrentIndex()
        {
            int num;
            num = 0;
            goto Label_0023;
        Label_0007:
            if (this.mPartys[num].Selected == null)
            {
                goto Label_001F;
            }
            return num;
        Label_001F:
            num += 1;
        Label_0023:
            if (num < this.mPartys.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public ShopData GetShopData(EShopType type)
        {
            if (type != 10)
            {
                goto Label_0014;
            }
            return this.GetLimitedShopData().GetShopData();
        Label_0014:
            if (type != 9)
            {
                goto Label_0028;
            }
            return this.GetEventShopData().GetShopData();
        Label_0028:
            return this.mShops[type];
        }

        public string GetShopName(EShopType type)
        {
            string str;
            EShopType type2;
            str = string.Empty;
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_003F;

                case 1:
                    goto Label_004A;

                case 2:
                    goto Label_0055;

                case 3:
                    goto Label_0060;

                case 4:
                    goto Label_006B;

                case 5:
                    goto Label_0076;

                case 6:
                    goto Label_0081;

                case 7:
                    goto Label_008C;

                case 8:
                    goto Label_0097;

                case 9:
                    goto Label_00AD;

                case 10:
                    goto Label_00A2;
            }
            goto Label_00B8;
        Label_003F:
            str = "sys.SHOPNAME_NORMAL";
            goto Label_00BD;
        Label_004A:
            str = "sys.SHOPNAME_TABI";
            goto Label_00BD;
        Label_0055:
            str = "sys.SHOPNAME_KIMAGURE";
            goto Label_00BD;
        Label_0060:
            str = "sys.SHOPNAME_MONOZUKI";
            goto Label_00BD;
        Label_006B:
            str = "sys.SHOPNAME_TOUR";
            goto Label_00BD;
        Label_0076:
            str = "sys.SHOPNAME_ARENA";
            goto Label_00BD;
        Label_0081:
            str = "sys.SHOPNAME_MULTI";
            goto Label_00BD;
        Label_008C:
            str = "sys.SHOPNAME_KAKERA";
            goto Label_00BD;
        Label_0097:
            str = "sys.SHOPNAME_ARTIFACT";
            goto Label_00BD;
        Label_00A2:
            str = "sys.SHOPNAME_LIMITED";
            goto Label_00BD;
        Label_00AD:
            str = "sys.SHOPNAME_EVENT";
            goto Label_00BD;
        Label_00B8:;
        Label_00BD:
            return (((str == string.Empty) == null) ? LocalizedText.Get(str) : str);
        }

        public int GetShopTypeCostAmount(ESaleType type)
        {
            ESaleType type2;
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_0034;

                case 2:
                    goto Label_0042;

                case 3:
                    goto Label_0049;

                case 4:
                    goto Label_0050;

                case 5:
                    goto Label_0057;

                case 6:
                    goto Label_005E;

                case 7:
                    goto Label_003B;
            }
            goto Label_006A;
        Label_002D:
            return this.Gold;
        Label_0034:
            return this.Coin;
        Label_003B:
            return this.PaidCoin;
        Label_0042:
            return this.TourCoin;
        Label_0049:
            return this.ArenaCoin;
        Label_0050:
            return this.PiecePoint;
        Label_0057:
            return this.MultiCoin;
        Label_005E:
            DebugUtility.Assert("There is no common price in the event coin.");
            return 0;
        Label_006A:
            return 0;
        }

        public int GetShopUpdateCost(EShopType type, bool getOldCost)
        {
            ShopData data;
            ShopParam param;
            int num;
            data = this.GetShopData(type);
            if (data != null)
            {
                goto Label_0010;
            }
            return 0;
        Label_0010:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetShopParam(type);
            if (param != null)
            {
                goto Label_0029;
            }
            return 0;
        Label_0029:
            if (param.UpdateCosts == null)
            {
                goto Label_006E;
            }
            if (((int) param.UpdateCosts.Length) <= 0)
            {
                goto Label_006E;
            }
            num = data.UpdateCount;
            if (getOldCost == null)
            {
                goto Label_0053;
            }
            num -= 1;
        Label_0053:
            num = Mathf.Clamp(num, 0, ((int) param.UpdateCosts.Length) - 1);
            return param.UpdateCosts[num];
        Label_006E:
            return 0;
        }

        public List<UnitData> GetSortedUnits(string menuID, bool includeShujinko)
        {
            GameUtility.UnitSortModes modes;
            bool flag;
            string str;
            modes = 0;
            flag = 0;
            if (string.IsNullOrEmpty(menuID) != null)
            {
                goto Label_0060;
            }
            if (PlayerPrefsUtility.HasKey(menuID) == null)
            {
                goto Label_0060;
            }
            str = PlayerPrefsUtility.GetString(menuID, string.Empty);
            flag = (PlayerPrefsUtility.GetInt(menuID + "#", 0) == 0) == 0;
        Label_003E:
            try
            {
                modes = (int) Enum.Parse(typeof(GameUtility.UnitSortModes), str, 1);
                goto Label_0060;
            }
            catch (Exception)
            {
            Label_005A:
                goto Label_0060;
            }
        Label_0060:
            return this.GetSortedUnits(modes, flag, includeShujinko);
        }

        public unsafe List<UnitData> GetSortedUnits(GameUtility.UnitSortModes sortMode, bool ascending, bool includeShujinko)
        {
            List<UnitData> list;
            List<UnitData> list2;
            int num;
            UnitData data;
            int[] numArray;
            list = MonoSingleton<GameManager>.Instance.Player.Units;
            list2 = new List<UnitData>();
            num = 0;
            goto Label_004B;
        Label_001D:
            data = list[num];
            if (includeShujinko != null)
            {
                goto Label_0040;
            }
            if (data.UnitParam.IsHero() == null)
            {
                goto Label_0040;
            }
            goto Label_0047;
        Label_0040:
            list2.Add(data);
        Label_0047:
            num += 1;
        Label_004B:
            if (num < list.Count)
            {
                goto Label_001D;
            }
            numArray = null;
            if (sortMode == null)
            {
                goto Label_0070;
            }
            GameUtility.SortUnits(list2, sortMode, 0, &numArray, 1);
            goto Label_0076;
        Label_0070:
            ascending = ascending == 0;
        Label_0076:
            if (ascending == null)
            {
                goto Label_0082;
            }
            list2.Reverse();
        Label_0082:
            return list2;
        }

        public unsafe int GetStaminaRecoveryCost(bool getOldCost)
        {
            FixParam param;
            int num;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            num = this.mStaminaBuyNum;
            if (getOldCost == null)
            {
                goto Label_0026;
            }
            num -= 1;
        Label_0026:
            num = Math.Max(Math.Min(num, ((int) param.StaminaAddCost.Length) - 1), 0);
            return *(&(param.StaminaAddCost[num]));
        }

        public unsafe TrophyState GetTrophyCounter(TrophyParam trophy, bool daily_old_data)
        {
            List<TrophyState> list;
            int num;
            TrophyState state;
            if (this.mTrophyStatesInameDict.TryGetValue(trophy.iname, &list) == null)
            {
                goto Label_0078;
            }
            if (trophy.IsDaily == null)
            {
                goto Label_0070;
            }
            if (daily_old_data == null)
            {
                goto Label_0031;
            }
            return list[0];
        Label_0031:
            num = 0;
            goto Label_005F;
        Label_0038:
            if (list[num].StartYMD != SRPG_Extensions.ToYMD(TimeManager.ServerTime))
            {
                goto Label_005B;
            }
            return list[num];
        Label_005B:
            num += 1;
        Label_005F:
            if (num < list.Count)
            {
                goto Label_0038;
            }
            goto Label_0078;
        Label_0070:
            return list[0];
        Label_0078:
            state = this.CreateTrophyState(trophy);
            this.AddTrophyStateDict(state);
            return state;
        }

        public UnitData GetUnitData(long iid)
        {
            int num;
            num = 0;
            goto Label_002F;
        Label_0007:
            if (this.mUnits[num].UniqueID != iid)
            {
                goto Label_002B;
            }
            return this.mUnits[num];
        Label_002B:
            num += 1;
        Label_002F:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public List<TobiraParam.Category> GetUnlockTobiraCategorys(UnitData unitData)
        {
            List<TobiraParam.Category> list;
            list = new List<TobiraParam.Category>();
            if (unitData.CheckTobiraIsUnlocked(1) == null)
            {
                goto Label_0019;
            }
            list.Add(1);
        Label_0019:
            if (unitData.CheckTobiraIsUnlocked(3) == null)
            {
                goto Label_002C;
            }
            list.Add(3);
        Label_002C:
            if (unitData.CheckTobiraIsUnlocked(4) == null)
            {
                goto Label_003F;
            }
            list.Add(4);
        Label_003F:
            if (unitData.CheckTobiraIsUnlocked(2) == null)
            {
                goto Label_0052;
            }
            list.Add(2);
        Label_0052:
            if (unitData.CheckTobiraIsUnlocked(6) == null)
            {
                goto Label_0065;
            }
            list.Add(6);
        Label_0065:
            if (unitData.CheckTobiraIsUnlocked(5) == null)
            {
                goto Label_0078;
            }
            list.Add(5);
        Label_0078:
            if (unitData.CheckTobiraIsUnlocked(7) == null)
            {
                goto Label_008B;
            }
            list.Add(7);
        Label_008B:
            return list;
        }

        public int GetVersusPlacement(string key)
        {
            return PlayerPrefsUtility.GetInt(key, 0);
        }

        public bool HasItem(string iname)
        {
            ItemData data;
            data = this.FindItemDataByItemID(iname);
            return ((data == null) ? 0 : (data.Num > 0));
        }

        public void IncrementChallengeMultiNum()
        {
            this.mChallengeMultiNum = OInt.op_Increment(this.mChallengeMultiNum);
            return;
        }

        public void IncrementQuestChallangeNumDaily(string name)
        {
            QuestParam param;
            int num;
            param = MonoSingleton<GameManager>.Instance.FindQuest(name);
            if (param == null)
            {
                goto Label_0023;
            }
            num = param.GetChallangeCount() + 1;
            this.SetQuestChallengeNumDaily(name, num);
        Label_0023:
            return;
        }

        public void IncrementRankMatchMission(RankMatchMissionType type)
        {
            GameManager manager;
            List<VersusRankMissionParam> list;
            <IncrementRankMatchMission>c__AnonStorey22B storeyb;
            storeyb = new <IncrementRankMatchMission>c__AnonStorey22B();
            storeyb.type = type;
            storeyb.<>f__this = this;
            manager = MonoSingleton<GameManager>.Instance;
            manager.GetVersusRankMissionList(manager.RankMatchScheduleId).ForEach(new Action<VersusRankMissionParam>(storeyb.<>m__134));
            return;
        }

        public void InitPlayerPrefs()
        {
        }

        private void InternalSavePlayerPrefsParty()
        {
            object[] objArray2;
            object[] objArray1;
            int num;
            int num2;
            num = 0;
            goto Label_00D2;
        Label_0007:
            num2 = 0;
            goto Label_00B7;
        Label_000E:
            objArray1 = new object[] { "Hensei", (int) num, "_UNIT", (int) num2, "_ID" };
            EditorPlayerPrefs.SetInt(string.Concat(objArray1), (int) this.mPartys[num].GetUnitUniqueID(num2));
            objArray2 = new object[] { "Hensei", (int) num, "_UNIT", (int) num2, "_LEADER" };
            EditorPlayerPrefs.SetInt(string.Concat(objArray2), (this.mPartys[num].LeaderIndex != num2) ? 0 : 1);
            num2 += 1;
        Label_00B7:
            if (num2 < this.mPartys[num].MAX_UNIT)
            {
                goto Label_000E;
            }
            num += 1;
        Label_00D2:
            if (num < this.mPartys.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public unsafe bool IsBeginner()
        {
            double num;
            double num2;
            double num3;
            double num4;
            TimeSpan span;
            DateTime time;
            TimeSpan span2;
            DateTime time2;
            num = (double) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.BeginnerDays;
            &span = new TimeSpan(&TimeManager.FromUnixTime((long) this.mNewGameAt).Ticks);
            num2 = &span.TotalDays;
            &span2 = new TimeSpan(&TimeManager.FromUnixTime(Network.GetServerTime()).Ticks);
            num4 = &span2.TotalDays - num2;
            if (num > num4)
            {
                goto Label_0073;
            }
            return 0;
        Label_0073:
            return 1;
        }

        public bool IsDailyAllComplete()
        {
            GameManager manager;
            TrophyParam[] paramArray;
            PlayerData data;
            TrophyState state;
            TrophyState[] stateArray;
            int num;
            int num2;
            TrophyParam param;
            bool flag;
            int num3;
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Trophies;
            data = manager.Player;
            if (paramArray == null)
            {
                goto Label_0023;
            }
            if (((int) paramArray.Length) > 0)
            {
                goto Label_0025;
            }
        Label_0023:
            return 1;
        Label_0025:
            stateArray = new TrophyState[(int) paramArray.Length];
            num = 0;
            goto Label_0066;
        Label_0037:
            if (paramArray[num].IsChallengeMission == null)
            {
                goto Label_0050;
            }
            stateArray[num] = null;
            goto Label_0060;
        Label_0050:
            stateArray[num] = data.GetTrophyCounter(paramArray[num], 0);
        Label_0060:
            num += 1;
        Label_0066:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0037;
            }
            num2 = 0;
            goto Label_012C;
        Label_0078:
            state = stateArray[num2];
            if (state == null)
            {
                goto Label_0126;
            }
            if (state.IsCompleted != null)
            {
                goto Label_0126;
            }
            param = paramArray[num2];
            flag = 0;
            num3 = 0;
            goto Label_00C4;
        Label_00A0:
            if (param.Objectives[num3].type != 0x34)
            {
                goto Label_00BE;
            }
            flag = 1;
            goto Label_00D4;
        Label_00BE:
            num3 += 1;
        Label_00C4:
            if (num3 < ((int) param.Objectives.Length))
            {
                goto Label_00A0;
            }
        Label_00D4:
            if (flag == null)
            {
                goto Label_00E0;
            }
            goto Label_0126;
        Label_00E0:
            if (param.DispType == 2)
            {
                goto Label_0126;
            }
            if (param.DispType != 1)
            {
                goto Label_00FF;
            }
            goto Label_0126;
        Label_00FF:
            if (param.IsDaily != null)
            {
                goto Label_0110;
            }
            goto Label_0126;
        Label_0110:
            if (this.IsMakeTrophyPlate(param, state, 0) != null)
            {
                goto Label_0124;
            }
            goto Label_0126;
        Label_0124:
            return 0;
        Label_0126:
            num2 += 1;
        Label_012C:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0078;
            }
            return 1;
        }

        public bool IsGuerrillaShopOpen()
        {
            bool flag;
            DateTime time;
            DateTime time2;
            flag = 0;
            if (MonoSingleton<GameManager>.Instance.Player.GuerrillaShopEnd == null)
            {
                goto Label_003F;
            }
            time = TimeManager.FromUnixTime(MonoSingleton<GameManager>.Instance.Player.GuerrillaShopEnd);
            if ((TimeManager.ServerTime < time) == null)
            {
                goto Label_003F;
            }
            flag = 1;
        Label_003F:
            return flag;
        }

        public bool IsHaveAward(string award)
        {
            if (this.mHaveAward != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mHaveAward.Contains(award);
        }

        public bool IsHaveConceptCardExpMaterial()
        {
            bool flag;
            List<ConceptCardMaterialData> list;
            if (this.mConceptCardExpMaterials == null)
            {
                goto Label_001B;
            }
            if (this.mConceptCardExpMaterials.Count != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 0;
        Label_001D:
            flag = 0;
            if (<>f__am$cache8B != null)
            {
                goto Label_003D;
            }
            <>f__am$cache8B = new Predicate<ConceptCardMaterialData>(PlayerData.<IsHaveConceptCardExpMaterial>m__157);
        Label_003D:
            list = this.mConceptCardExpMaterials.FindAll(<>f__am$cache8B);
            if (list == null)
            {
                goto Label_005C;
            }
            if (list.Count <= 0)
            {
                goto Label_005C;
            }
            flag = 1;
        Label_005C:
            return flag;
        }

        public bool IsHaveConceptCardTrustMaterial()
        {
            bool flag;
            List<ConceptCardMaterialData> list;
            if (this.mConceptCardTrustMaterials == null)
            {
                goto Label_001B;
            }
            if (this.mConceptCardTrustMaterials.Count != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 0;
        Label_001D:
            flag = 0;
            if (<>f__am$cache8C != null)
            {
                goto Label_003D;
            }
            <>f__am$cache8C = new Predicate<ConceptCardMaterialData>(PlayerData.<IsHaveConceptCardTrustMaterial>m__158);
        Label_003D:
            list = this.mConceptCardTrustMaterials.FindAll(<>f__am$cache8C);
            if (list == null)
            {
                goto Label_005C;
            }
            if (list.Count <= 0)
            {
                goto Label_005C;
            }
            flag = 1;
        Label_005C:
            return flag;
        }

        public bool IsHaveHealAPItems()
        {
            bool flag;
            List<ItemData> list;
            int num;
            flag = 0;
            if (<>f__am$cache8A != null)
            {
                goto Label_0020;
            }
            <>f__am$cache8A = new Func<ItemData, bool>(PlayerData.<IsHaveHealAPItems>m__156);
        Label_0020:
            list = Enumerable.ToList<ItemData>(Enumerable.Where<ItemData>(this.Items, <>f__am$cache8A));
            if (list == null)
            {
                goto Label_0066;
            }
            num = 0;
            goto Label_005A;
        Label_003D:
            if (list[num].Num <= 0)
            {
                goto Label_0056;
            }
            flag = 1;
            goto Label_0066;
        Label_0056:
            num += 1;
        Label_005A:
            if (num < list.Count)
            {
                goto Label_003D;
            }
        Label_0066:
            return flag;
        }

        public bool IsLastLoginBonus(string type)
        {
            if (string.IsNullOrEmpty(type) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.mLoginBonusTables.ContainsKey(type) != null)
            {
                goto Label_0020;
            }
            return 0;
        Label_0020:
            return (this.mLoginBonusTables[type].lastday > 0);
        }

        private bool IsMakeTrophyPlate(TrophyParam trophy, TrophyState st, bool is_achievement)
        {
            if (trophy.IsInvisibleVip() == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (trophy.IsInvisibleCard() == null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            if (trophy.IsInvisibleStamina() == null)
            {
                goto Label_0027;
            }
            return 0;
        Label_0027:
            if (trophy.RequiredTrophies == null)
            {
                goto Label_0046;
            }
            if (TrophyParam.CheckRequiredTrophies(MonoSingleton<GameManager>.Instance, trophy, 1, 1) != null)
            {
                goto Label_0046;
            }
            return 0;
        Label_0046:
            if (trophy.IsAvailablePeriod(TimeManager.ServerTime, is_achievement) != null)
            {
                goto Label_0059;
            }
            return 0;
        Label_0059:
            return 1;
        }

        public bool IsQuestAvailable(string questID)
        {
            bool flag;
            bool flag2;
            <IsQuestAvailable>c__AnonStorey230 storey;
            storey = new <IsQuestAvailable>c__AnonStorey230();
            storey.questparam = MonoSingleton<GameManager>.Instance.FindQuest(questID);
            if (storey.questparam == null)
            {
                goto Label_0059;
            }
            flag = storey.questparam.IsDateUnlock(-1L);
            flag2 = (Array.Find<QuestParam>(this.AvailableQuests, new Predicate<QuestParam>(storey.<>m__137)) == null) == 0;
            return ((flag == null) ? 0 : flag2);
        Label_0059:
            return 0;
        }

        public bool IsQuestCleared(string questID)
        {
            QuestParam param;
            param = MonoSingleton<GameManager>.Instance.FindQuest(questID);
            if (param == null)
            {
                goto Label_001C;
            }
            return (param.state == 2);
        Label_001C:
            return 0;
        }

        public bool IsRequestFriend()
        {
            return (this.FriendNum < this.FriendCap);
        }

        public bool IsTrophyDirty()
        {
            int num;
            num = this.mTrophyStates.Count - 1;
            goto Label_002F;
        Label_0013:
            if (this.mTrophyStates[num].IsDirty == null)
            {
                goto Label_002B;
            }
            return 1;
        Label_002B:
            num -= 1;
        Label_002F:
            if (num >= 0)
            {
                goto Label_0013;
            }
            return 0;
        }

        public bool ItemEntryExists(string iname)
        {
            bool flag;
            return this.mID2ItemData.ContainsKey(iname);
        }

        public bool JobRankUpUnit(UnitData unit, int jobIndex)
        {
            if (unit.CheckJobRankUpAllEquip(jobIndex, 1) != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            unit.JobRankUp(jobIndex);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(2);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
            return 1;
        }

        public void LoadPlayerPrefs()
        {
            object[] objArray6;
            object[] objArray5;
            object[] objArray4;
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            bool flag;
            PlayerParam param;
            FixParam param2;
            int num;
            int num2;
            string str;
            Json_Artifact artifact;
            ArtifactData data;
            List<Json_Ability> list;
            int num3;
            int num4;
            UnitData data2;
            string str2;
            Json_Unit unit;
            List<Json_Job> list2;
            int num5;
            string str3;
            Json_Job job;
            int num6;
            string str4;
            List<Json_Ability> list3;
            int num7;
            string str5;
            Json_Ability ability;
            bool flag2;
            int num8;
            int num9;
            string str6;
            int num10;
            int num11;
            string str7;
            string str8;
            List<ArtifactData> list4;
            ArtifactData data3;
            Json_Artifact artifact2;
            ArtifactData data4;
            Exception exception;
            int num12;
            Json_Party party;
            PartyData data5;
            int num13;
            int num14;
            int num15;
            string str9;
            Json_Item item;
            ItemData data6;
            <LoadPlayerPrefs>c__AnonStorey23D storeyd;
            flag = 1;
            if (EditorPlayerPrefs.HasKey("Version") == null)
            {
                goto Label_0026;
            }
            flag = PLAYRE_DATA_VERSION != EditorPlayerPrefs.GetString("Version");
        Label_0026:
            if (flag == null)
            {
                goto Label_0032;
            }
            this.InitPlayerPrefs();
        Label_0032:
            if (EditorPlayerPrefs.HasKey("Gold") == null)
            {
                goto Label_0056;
            }
            this.mGold = EditorPlayerPrefs.GetInt("Gold");
        Label_0056:
            if (EditorPlayerPrefs.HasKey("PaidCoin") == null)
            {
                goto Label_007A;
            }
            this.mPaidCoin = EditorPlayerPrefs.GetInt("PaidCoin");
        Label_007A:
            if (EditorPlayerPrefs.HasKey("FreeCoin") == null)
            {
                goto Label_009E;
            }
            this.mFreeCoin = EditorPlayerPrefs.GetInt("FreeCoin");
        Label_009E:
            if (EditorPlayerPrefs.HasKey("ComCoin") == null)
            {
                goto Label_00C2;
            }
            this.mComCoin = EditorPlayerPrefs.GetInt("ComCoin");
        Label_00C2:
            if (EditorPlayerPrefs.HasKey("TourCoin") == null)
            {
                goto Label_00E6;
            }
            this.mTourCoin = EditorPlayerPrefs.GetInt("TourCoin");
        Label_00E6:
            if (EditorPlayerPrefs.HasKey("ArenaCoin") == null)
            {
                goto Label_010A;
            }
            this.mArenaCoin = EditorPlayerPrefs.GetInt("ArenaCoin");
        Label_010A:
            if (EditorPlayerPrefs.HasKey("MultiCoin") == null)
            {
                goto Label_012E;
            }
            this.mMultiCoin = EditorPlayerPrefs.GetInt("MultiCoin");
        Label_012E:
            if (EditorPlayerPrefs.HasKey("PiecePoint") == null)
            {
                goto Label_0152;
            }
            this.mPiecePoint = EditorPlayerPrefs.GetInt("PiecePoint");
        Label_0152:
            if (EditorPlayerPrefs.HasKey("PlayerExp") == null)
            {
                goto Label_0176;
            }
            this.mExp = EditorPlayerPrefs.GetInt("PlayerExp");
        Label_0176:
            if (string.IsNullOrEmpty(this.mCuid) == null)
            {
                goto Label_01B3;
            }
            this.mCuid = "1";
            this.mName = "GUMI";
            this.mLv = this.CalcLevel();
            this.UpdateUnlocks();
        Label_01B3:
            if (EditorPlayerPrefs.HasKey("Stamina") == null)
            {
                goto Label_01DC;
            }
            this.mStamina.val = EditorPlayerPrefs.GetInt("Stamina");
        Label_01DC:
            if (EditorPlayerPrefs.HasKey("StaminaAt") == null)
            {
                goto Label_020A;
            }
            this.mStamina.at = Convert.ToInt64(EditorPlayerPrefs.GetString("StaminaAt"));
        Label_020A:
            if (EditorPlayerPrefs.HasKey("CaveStamina") == null)
            {
                goto Label_0233;
            }
            this.mCaveStamina.val = EditorPlayerPrefs.GetInt("CaveStamina");
        Label_0233:
            if (EditorPlayerPrefs.HasKey("CaveStaminaAt") == null)
            {
                goto Label_0261;
            }
            this.mCaveStamina.at = Convert.ToInt64(EditorPlayerPrefs.GetString("CaveStaminaAt"));
        Label_0261:
            if (EditorPlayerPrefs.HasKey("AbilRankUpCount") == null)
            {
                goto Label_028A;
            }
            this.mAbilityRankUpCount.val = EditorPlayerPrefs.GetInt("AbilRankUpCount");
        Label_028A:
            if (EditorPlayerPrefs.HasKey("AbilRankUpCountAt") == null)
            {
                goto Label_02B8;
            }
            this.mAbilityRankUpCount.at = Convert.ToInt64(EditorPlayerPrefs.GetString("AbilRankUpCountAt"));
        Label_02B8:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerParam(this.mLv);
            if (param == null)
            {
                goto Label_02F6;
            }
            this.mUnitCap = param.ucap;
            this.mStamina.valMax = param.pt;
        Label_02F6:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            this.mStamina.valRecover = param2.StaminaRecoveryVal;
            this.mStamina.interval = param2.StaminaRecoverySec;
            this.mCaveStamina.valMax = param2.CaveStaminaMax;
            this.mCaveStamina.valRecover = param2.CaveStaminaRecoveryVal;
            this.mCaveStamina.interval = param2.CaveStaminaRecoverySec;
            this.mAbilityRankUpCount.valMax = param2.AbilityRankUpCountMax;
            this.mAbilityRankUpCount.valRecover = param2.AbilityRankUpCountRecoveryVal;
            this.mAbilityRankUpCount.interval = param2.AbilityRankUpCountRecoverySec;
            if (EditorPlayerPrefs.HasKey("ARTI_NUM") == null)
            {
                goto Label_0414;
            }
            num = EditorPlayerPrefs.GetInt("ARTI_NUM");
            num2 = 0;
            goto Label_040C;
        Label_03B0:
            str = EditorPlayerPrefs.GetString("ARTI_" + ((int) num2));
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_03D9;
            }
            goto Label_0406;
        Label_03D9:
            artifact = JSONParser.parseJSONObject<Json_Artifact>(str);
            if (artifact != null)
            {
                goto Label_03EE;
            }
            goto Label_0406;
        Label_03EE:
            data = new ArtifactData();
            data.Deserialize(artifact);
            this.AddArtifact(data);
        Label_0406:
            num2 += 1;
        Label_040C:
            if (num2 < num)
            {
                goto Label_03B0;
            }
        Label_0414:
            if (this.mUnits != null)
            {
                goto Label_0435;
            }
            this.mUnits = new List<UnitData>(this.mUnitCap);
        Label_0435:
            this.mUnits.Clear();
            this.mUniqueID2UnitData.Clear();
            list = new List<Json_Ability>(5);
            num3 = EditorPlayerPrefs.GetInt("UnitNum");
            num4 = 0;
            goto Label_0A9E;
        Label_0467:
            data2 = new UnitData();
            if (data2 == null)
            {
                goto Label_0A98;
            }
            list.Clear();
            str2 = "Unit" + ((int) num4) + "_";
            unit = new Json_Unit();
            unit.iname = EditorPlayerPrefs.GetString(str2 + "Iname");
            unit.iid = (long) EditorPlayerPrefs.GetInt(str2 + "Iid");
            unit.exp = EditorPlayerPrefs.GetInt(str2 + "Exp");
            unit.plus = EditorPlayerPrefs.GetInt(str2 + "Plus");
            unit.rare = EditorPlayerPrefs.GetInt(str2 + "Rarity");
            list2 = new List<Json_Job>(4);
            num5 = 0;
            goto Label_08BC;
        Label_0524:
            objArray1 = new object[] { str2, "Job", (int) num5, "_" };
            str3 = string.Concat(objArray1);
            if (EditorPlayerPrefs.HasKey(str3 + "Iname") != null)
            {
                goto Label_056B;
            }
            goto Label_08B6;
        Label_056B:
            if (string.IsNullOrEmpty(EditorPlayerPrefs.GetString(str3 + "Iname")) == null)
            {
                goto Label_058B;
            }
            goto Label_08B6;
        Label_058B:
            job = new Json_Job();
            job.iname = EditorPlayerPrefs.GetString(str3 + "Iname");
            job.iid = (long) EditorPlayerPrefs.GetInt(str3 + "Iid");
            job.rank = EditorPlayerPrefs.GetInt(str3 + "Rank");
            job.equips = new Json_Equip[6];
            num6 = 0;
            goto Label_06B8;
        Label_05F0:
            objArray2 = new object[] { str3, "Equip", (int) num6, "_" };
            str4 = string.Concat(objArray2);
            if (EditorPlayerPrefs.HasKey(str4 + "Iname") == null)
            {
                goto Label_06A7;
            }
            job.equips[num6] = new Json_Equip();
            job.equips[num6].iname = EditorPlayerPrefs.GetString(str4 + "Iname");
            job.equips[num6].iid = (long) EditorPlayerPrefs.GetInt(str4 + "Iid");
            job.equips[num6].exp = EditorPlayerPrefs.GetInt(str4 + "Exp");
            goto Label_06B2;
        Label_06A7:
            job.equips[num6] = null;
        Label_06B2:
            num6 += 1;
        Label_06B8:
            if (num6 < ((int) job.equips.Length))
            {
                goto Label_05F0;
            }
            list3 = new List<Json_Ability>(8);
            num7 = 0;
            goto Label_07E6;
        Label_06D8:
            objArray3 = new object[] { str3, "Ability", (int) num7, "_" };
            str5 = string.Concat(objArray3);
            if (EditorPlayerPrefs.HasKey(str5 + "Iname") == null)
            {
                goto Label_07E0;
            }
            if (string.IsNullOrEmpty(EditorPlayerPrefs.GetString(str5 + "Iname")) == null)
            {
                goto Label_073A;
            }
            goto Label_07E0;
        Label_073A:
            ability = new Json_Ability();
            ability.iname = EditorPlayerPrefs.GetString(str5 + "Iname");
            ability.iid = (long) EditorPlayerPrefs.GetInt(str5 + "Iid");
            ability.exp = EditorPlayerPrefs.GetInt(str5 + "Exp");
            flag2 = 0;
            num8 = 0;
            goto Label_07BD;
        Label_0795:
            if ((list3[num8].iname == ability.iname) == null)
            {
                goto Label_07B7;
            }
            flag2 = 1;
        Label_07B7:
            num8 += 1;
        Label_07BD:
            if (num8 < list3.Count)
            {
                goto Label_0795;
            }
            if (flag2 == null)
            {
                goto Label_07D7;
            }
            goto Label_07E0;
        Label_07D7:
            list3.Add(ability);
        Label_07E0:
            num7 += 1;
        Label_07E6:
            if (num7 < 8)
            {
                goto Label_06D8;
            }
            job.abils = (list3.Count <= 0) ? null : list3.ToArray();
            job.select = new Json_JobSelectable();
            job.select.abils = new long[5];
            Array.Clear(job.select.abils, 0, (int) job.select.abils.Length);
            num9 = 0;
            goto Label_0898;
        Label_0855:
            str6 = str3 + "Select_Ability" + ((int) num9);
            if (EditorPlayerPrefs.HasKey(str6) != null)
            {
                goto Label_087B;
            }
            goto Label_0892;
        Label_087B:
            job.select.abils[num9] = (long) EditorPlayerPrefs.GetInt(str6);
        Label_0892:
            num9 += 1;
        Label_0898:
            if (num9 < ((int) job.select.abils.Length))
            {
                goto Label_0855;
            }
            list2.Add(job);
        Label_08B6:
            num5 += 1;
        Label_08BC:
            if (num5 < 4)
            {
                goto Label_0524;
            }
            unit.jobs = list2.ToArray();
            unit.select = new Json_UnitSelectable();
            unit.select.job = (long) EditorPlayerPrefs.GetInt(str2 + "Select_Job");
        Label_08FC:
            try
            {
                data2.Deserialize(unit);
                num10 = 0;
                goto Label_0A54;
            Label_090D:
                num11 = 0;
                goto Label_0A36;
            Label_0915:
                storeyd = new <LoadPlayerPrefs>c__AnonStorey23D();
                objArray4 = new object[] { str2, "Job", (int) num10, "_" };
                str7 = string.Concat(objArray4);
                objArray5 = new object[] { str7, "Artifact", (int) num11, "_Iid" };
                str8 = string.Concat(objArray5);
                if (EditorPlayerPrefs.HasKey(str8) != null)
                {
                    goto Label_0985;
                }
                goto Label_0A30;
            Label_0985:
                storeyd.iid = (long) EditorPlayerPrefs.GetInt(str8);
                data3 = MonoSingleton<GameManager>.Instance.Player.Artifacts.Find(new Predicate<ArtifactData>(storeyd.<>m__145));
                if (data3 == null)
                {
                    goto Label_0A30;
                }
                artifact2 = new Json_Artifact();
                artifact2.iid = data3.UniqueID;
                artifact2.iname = data3.ArtifactParam.iname;
                artifact2.rare = data3.Rarity;
                artifact2.exp = 0;
                data4 = new ArtifactData();
                data4.Reset();
                data4.Deserialize(artifact2);
                data2.SetEquipArtifactData(num10, num11, data4, 1);
            Label_0A30:
                num11 += 1;
            Label_0A36:
                if (num11 < ((int) data2.Jobs[num10].Artifacts.Length))
                {
                    goto Label_0915;
                }
                num10 += 1;
            Label_0A54:
                if (num10 < ((int) data2.Jobs.Length))
                {
                    goto Label_090D;
                }
                this.mUnits.Add(data2);
                this.mUniqueID2UnitData[data2.UniqueID] = data2;
                goto Label_0A98;
            }
            catch (Exception exception1)
            {
            Label_0A8A:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_0A98;
            }
        Label_0A98:
            num4 += 1;
        Label_0A9E:
            if (num4 < num3)
            {
                goto Label_0467;
            }
            list = null;
            num12 = 0;
            goto Label_0B54;
        Label_0AB2:
            party = new Json_Party();
            data5 = new PartyData(num12);
            party.units = new long[data5.MAX_UNIT];
            num13 = 0;
            goto Label_0B2A;
        Label_0ADD:
            objArray6 = new object[] { "Hensei", (int) num12, "_UNIT", (int) num13, "_ID" };
            party.units[num13] = (long) EditorPlayerPrefs.GetInt(string.Concat(objArray6));
            num13 += 1;
        Label_0B2A:
            if (num13 < ((int) party.units.Length))
            {
                goto Label_0ADD;
            }
            this.mPartys[num12].Deserialize(party);
            num12 += 1;
        Label_0B54:
            if (num12 < 11)
            {
                goto Label_0AB2;
            }
            Debug.Log("LoadPlayerPrefs Items");
            num14 = EditorPlayerPrefs.GetInt("ItemNum");
            if (this.mItems != null)
            {
                goto Label_0B8B;
            }
            this.mItems = new List<ItemData>(num14);
        Label_0B8B:
            this.mItems.Clear();
            this.mID2ItemData.Clear();
            num15 = 0;
            goto Label_0C7F;
        Label_0BA9:
            str9 = "Item" + ((int) num15) + "_";
            item = new Json_Item();
            item.iname = EditorPlayerPrefs.GetString(str9 + "Iname");
            item.iid = (long) EditorPlayerPrefs.GetInt(str9 + "Iid");
            item.num = EditorPlayerPrefs.GetInt(str9 + "Num");
            if (MonoSingleton<GameManager>.Instance.GetItemParam(item.iname) != null)
            {
                goto Label_0C47;
            }
            DebugUtility.Log("存在しないアイテム[" + item.iname + "]が指定された");
            goto Label_0C79;
        Label_0C47:
            data6 = new ItemData();
            data6.Deserialize(item);
            this.mItems.Add(data6);
            this.mID2ItemData[item.iname] = data6;
        Label_0C79:
            num15 += 1;
        Label_0C7F:
            if (num15 < num14)
            {
                goto Label_0BA9;
            }
            return;
        }

        public int LoginCountWithType(string type)
        {
            if (string.IsNullOrEmpty(type) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.mLoginBonusTables.ContainsKey(type) != null)
            {
                goto Label_0020;
            }
            return 0;
        Label_0020:
            return this.mLoginBonusTables[type].count;
        }

        public void MarkQuestChallenged(string name)
        {
            QuestParam param;
            param = MonoSingleton<GameManager>.Instance.FindQuest(name);
            if (param == null)
            {
                goto Label_0025;
            }
            if (param.state != null)
            {
                goto Label_0025;
            }
            this.SetQuestState(name, 1);
        Label_0025:
            return;
        }

        public void MarkQuestCleared(string name)
        {
            this.SetQuestState(name, 2);
            return;
        }

        public void MarkTrophiesEnded(TrophyParam[] trophies)
        {
            int num;
            TrophyState state;
            num = 0;
            goto Label_002F;
        Label_0007:
            state = this.GetTrophyCounter(trophies[num], 1);
            state.IsEnded = 1;
            state.IsDirty = 1;
            state.RewardedAt = TimeManager.ServerTime;
            num += 1;
        Label_002F:
            if (num < ((int) trophies.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void OfflineSellArtifacts(ArtifactData[] artifacts)
        {
            int num;
            if (artifacts != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            num = 0;
            goto Label_002E;
        Label_000E:
            this.RemoveArtifact(artifacts[num]);
            this.GainGold(artifacts[num].ArtifactParam.sell);
            num += 1;
        Label_002E:
            if (num < ((int) artifacts.Length))
            {
                goto Label_000E;
            }
            return;
        }

        public void OnAbilityPowerUp(string unitID, string abilityID, int level, bool verify)
        {
            char[] chArray1;
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyObjective objective2;
            char[] chArray;
            string[] strArray;
            if (verify != null)
            {
                goto Label_0036;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(9);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_002F;
        Label_001F:
            objective = objectiveArray[num];
            this.AddTrophyCounter(objective, 1);
            num -= 1;
        Label_002F:
            if (num >= 0)
            {
                goto Label_001F;
            }
        Label_0036:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x21);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_00E3;
        Label_004E:
            objective2 = objectiveArray[num2];
            if (objective2.ival > level)
            {
                goto Label_00DF;
            }
            if (string.IsNullOrEmpty(objective2.sval_base) == null)
            {
                goto Label_007F;
            }
            this.AddTrophyCounter(objective2, 1);
            goto Label_00DF;
        Label_007F:
            chArray1 = new char[] { 0x2c };
            chArray = chArray1;
            strArray = objective2.sval_base.Split(chArray);
            if (string.IsNullOrEmpty(strArray[1]) != null)
            {
                goto Label_00B9;
            }
            if ((abilityID == strArray[1]) == null)
            {
                goto Label_00DF;
            }
        Label_00B9:
            if (string.IsNullOrEmpty(strArray[0]) != null)
            {
                goto Label_00D6;
            }
            if ((unitID == strArray[0]) == null)
            {
                goto Label_00DF;
            }
        Label_00D6:
            this.AddTrophyCounter(objective2, 1);
        Label_00DF:
            num2 -= 1;
        Label_00E3:
            if (num2 >= 0)
            {
                goto Label_004E;
            }
            return;
        }

        public void OnArtifactEvolution(string artifactID)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x27);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0049;
        Label_0018:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_003D;
            }
            if ((objective.sval_base == artifactID) == null)
            {
                goto Label_0045;
            }
        Label_003D:
            this.AddTrophyCounter(objective, 1);
        Label_0045:
            num -= 1;
        Label_0049:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnArtifactStrength(string artifactID, int useItemNum, int beforeLevel, int currentLevel)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            int num3;
            TrophyObjective objective2;
            int num4;
            TrophyObjective objective3;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x26);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0049;
        Label_0018:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_003D;
            }
            if ((objective.sval_base == artifactID) == null)
            {
                goto Label_0045;
            }
        Label_003D:
            this.AddTrophyCounter(objective, useItemNum);
        Label_0045:
            num -= 1;
        Label_0049:
            if (num >= 0)
            {
                goto Label_0018;
            }
            num2 = currentLevel - beforeLevel;
            if (num2 < 1)
            {
                goto Label_00B5;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x38);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_00AD;
        Label_0075:
            objective2 = objectiveArray[num3];
            if (string.IsNullOrEmpty(objective2.sval_base) != null)
            {
                goto Label_009E;
            }
            if (string.Equals(objective2.sval_base, artifactID) == null)
            {
                goto Label_00A7;
            }
        Label_009E:
            this.AddTrophyCounter(objective2, num2);
        Label_00A7:
            num3 -= 1;
        Label_00AD:
            if (num3 >= 0)
            {
                goto Label_0075;
            }
        Label_00B5:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x39);
            num4 = ((int) objectiveArray.Length) - 1;
            goto Label_011A;
        Label_00CE:
            objective3 = objectiveArray[num4];
            if (currentLevel >= objective3.ival)
            {
                goto Label_00E7;
            }
            goto Label_0114;
        Label_00E7:
            if (string.IsNullOrEmpty(objective3.sval_base) != null)
            {
                goto Label_010A;
            }
            if (string.Equals(objective3.sval_base, artifactID) == null)
            {
                goto Label_0114;
            }
        Label_010A:
            this.SetTrophyCounter(objective3, currentLevel);
        Label_0114:
            num4 -= 1;
        Label_011A:
            if (num4 >= 0)
            {
                goto Label_00CE;
            }
            return;
        }

        public void OnArtifactTransmute(string artifactID)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x25);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0049;
        Label_0018:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_003D;
            }
            if ((objective.sval_base == artifactID) == null)
            {
                goto Label_0045;
            }
        Label_003D:
            this.AddTrophyCounter(objective, 1);
        Label_0045:
            num -= 1;
        Label_0049:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnBuyAtShop(string shopID, string itemID, int num)
        {
            char[] chArray1;
            TrophyObjective[] objectiveArray;
            int num2;
            TrophyObjective objective;
            char[] chArray;
            string[] strArray;
            if (num <= 0)
            {
                goto Label_00A7;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x24);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_00A0;
        Label_001F:
            objective = objectiveArray[num2];
            if (string.IsNullOrEmpty(objective.sval_base) == null)
            {
                goto Label_0040;
            }
            this.AddTrophyCounter(objective, num);
            goto Label_009C;
        Label_0040:
            chArray1 = new char[] { 0x2c };
            chArray = chArray1;
            strArray = objective.sval_base.Split(chArray);
            if (string.IsNullOrEmpty(strArray[1]) != null)
            {
                goto Label_0077;
            }
            if ((itemID == strArray[1]) == null)
            {
                goto Label_009C;
            }
        Label_0077:
            if (string.IsNullOrEmpty(strArray[0]) != null)
            {
                goto Label_0094;
            }
            if ((shopID == strArray[0]) == null)
            {
                goto Label_009C;
            }
        Label_0094:
            this.AddTrophyCounter(objective, num);
        Label_009C:
            num2 -= 1;
        Label_00A0:
            if (num2 >= 0)
            {
                goto Label_001F;
            }
        Label_00A7:
            return;
        }

        public void OnBuyGold()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(11);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0028;
        Label_0018:
            objective = objectiveArray[num];
            this.AddTrophyCounter(objective, 1);
            num -= 1;
        Label_0028:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnChallengeMissionComplete(string trophyID)
        {
            TrophyParam[] paramArray;
            TrophyParam param;
            TrophyParam[] paramArray2;
            int num;
            paramArray2 = MonoSingleton<GameManager>.Instance.Trophies;
            num = 0;
            goto Label_0041;
        Label_0014:
            param = paramArray2[num];
            if (param.IsChallengeMissionRoot == null)
            {
                goto Label_003D;
            }
            if ((param.iname == trophyID) == null)
            {
                goto Label_003D;
            }
            this.AddTrophyCounter(param, 0, 1);
        Label_003D:
            num += 1;
        Label_0041:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0014;
            }
            return;
        }

        public void OnChangeAbilitySet(string unitID)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x20);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0049;
        Label_0018:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_003D;
            }
            if ((objective.sval_base == unitID) == null)
            {
                goto Label_0045;
            }
        Label_003D:
            this.AddTrophyCounter(objective, 1);
        Label_0045:
            num -= 1;
        Label_0049:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnCoinChange(int delta)
        {
        }

        public void OnDamageToEnemy(Unit unit, Unit target, int damage)
        {
            TrophyObjective[] objectiveArray;
            int num;
            if (unit == null)
            {
                goto Label_001C;
            }
            if (unit.Side != null)
            {
                goto Label_001C;
            }
            if (unit.IsPartyMember != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            if (target == null)
            {
                goto Label_002F;
            }
            if (target.Side == 1)
            {
                goto Label_0030;
            }
        Label_002F:
            return;
        Label_0030:
            if ((SceneBattle.Instance == null) == null)
            {
                goto Label_0041;
            }
            return;
        Label_0041:
            if (SceneBattle.Instance.IsPlayingArenaQuest == null)
            {
                goto Label_0051;
            }
            return;
        Label_0051:
            if (SceneBattle.Instance.Battle == null)
            {
                goto Label_009A;
            }
            if (SceneBattle.Instance.Battle.IsMultiPlay == null)
            {
                goto Label_009A;
            }
            if (PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex <= 0)
            {
                goto Label_0099;
            }
            if (PunMonoSingleton<MyPhoton>.Instance.MyPlayerIndex == unit.OwnerPlayerIndex)
            {
                goto Label_009A;
            }
        Label_0099:
            return;
        Label_009A:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x4e);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_00CE;
        Label_00B2:
            if (objectiveArray[num].ival > damage)
            {
                goto Label_00CA;
            }
            this.AddTrophyCounter(objectiveArray[num], 1);
        Label_00CA:
            num -= 1;
        Label_00CE:
            if (num >= 0)
            {
                goto Label_00B2;
            }
            return;
        }

        public void OnEnemyKill(string enemyID, int count)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(2);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0038;
        Label_0017:
            objective = objectiveArray[num];
            if ((objective.sval_base == enemyID) == null)
            {
                goto Label_0034;
            }
            this.AddTrophyCounter(objective, 1);
        Label_0034:
            num -= 1;
        Label_0038:
            if (num >= 0)
            {
                goto Label_0017;
            }
            return;
        }

        public void OnEvolutionChange(string unitID, int rarity)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyObjective objective2;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x15);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0045;
        Label_0018:
            objective = objectiveArray[num];
            if ((objective.sval_base == unitID) == null)
            {
                goto Label_0041;
            }
            if (objective.ival > rarity)
            {
                goto Label_0041;
            }
            this.AddTrophyCounter(objective, 1);
        Label_0041:
            num -= 1;
        Label_0045:
            if (num >= 0)
            {
                goto Label_0018;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(30);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_0099;
        Label_0064:
            objective2 = objectiveArray[num2];
            if (string.IsNullOrEmpty(objective2.sval_base) != null)
            {
                goto Label_008C;
            }
            if ((objective2.sval_base == unitID) == null)
            {
                goto Label_0095;
            }
        Label_008C:
            this.AddTrophyCounter(objective2, 1);
        Label_0095:
            num2 -= 1;
        Label_0099:
            if (num2 >= 0)
            {
                goto Label_0064;
            }
            return;
        }

        public void OnEvolutionCheck(string unitID, int rarity, int initialRarity)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            int num3;
            TrophyObjective objective2;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x15);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0045;
        Label_0018:
            objective = objectiveArray[num];
            if ((objective.sval_base == unitID) == null)
            {
                goto Label_0041;
            }
            if (objective.ival > rarity)
            {
                goto Label_0041;
            }
            this.AddTrophyCounter(objective, 1);
        Label_0041:
            num -= 1;
        Label_0045:
            if (num >= 0)
            {
                goto Label_0018;
            }
            num2 = rarity - initialRarity;
            if (num2 >= 1)
            {
                goto Label_0058;
            }
            return;
        Label_0058:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(30);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_00AE;
        Label_0071:
            objective2 = objectiveArray[num3];
            if (string.IsNullOrEmpty(objective2.sval_base) == null)
            {
                goto Label_008D;
            }
            goto Label_00A8;
        Label_008D:
            if ((objective2.sval_base == unitID) == null)
            {
                goto Label_00A8;
            }
            this.SetTrophyCounter(objective2, num2);
        Label_00A8:
            num3 -= 1;
        Label_00AE:
            if (num3 >= 0)
            {
                goto Label_0071;
            }
            return;
        }

        public void OnFgGIDLogin()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x13);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0028;
        Label_0018:
            objective = objectiveArray[num];
            this.AddTrophyCounter(objective, 1);
            num -= 1;
        Label_0028:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnGacha(GachaTypes type, int count)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            if (count > 0)
            {
                goto Label_0008;
            }
            return;
        Label_0008:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(7);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_009B;
        Label_001F:
            objective = objectiveArray[num];
            if ((objective.sval_base == "normal") == null)
            {
                goto Label_0048;
            }
            if (type == null)
            {
                goto Label_008F;
            }
            goto Label_0097;
            goto Label_008F;
        Label_0048:
            if ((objective.sval_base == "rare") == null)
            {
                goto Label_006E;
            }
            if (type == 1)
            {
                goto Label_008F;
            }
            goto Label_0097;
            goto Label_008F;
        Label_006E:
            if ((objective.sval_base == "vip") == null)
            {
                goto Label_008F;
            }
            if (type == 2)
            {
                goto Label_008F;
            }
            goto Label_0097;
        Label_008F:
            this.AddTrophyCounter(objective, count);
        Label_0097:
            num -= 1;
        Label_009B:
            if (num >= 0)
            {
                goto Label_001F;
            }
            return;
        }

        public void OnGoldChange(int delta)
        {
            TrophyObjective[] objectiveArray;
            int num;
            if (delta != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(80);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0040;
        Label_001F:
            if (this.Gold < objectiveArray[num].ival)
            {
                goto Label_003C;
            }
            this.AddTrophyCounter(objectiveArray[num], 1);
        Label_003C:
            num -= 1;
        Label_0040:
            if (num >= 0)
            {
                goto Label_001F;
            }
            return;
        }

        public void OnItemQuantityChange(string itemID, int delta)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            if (delta <= 0)
            {
                goto Label_0046;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(3);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_003F;
        Label_001E:
            objective = objectiveArray[num];
            if ((objective.sval_base == itemID) == null)
            {
                goto Label_003B;
            }
            this.AddTrophyCounter(objective, delta);
        Label_003B:
            num -= 1;
        Label_003F:
            if (num >= 0)
            {
                goto Label_001E;
            }
        Label_0046:
            return;
        }

        public void OnJobChange(string unitID)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x1f);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0049;
        Label_0018:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_003D;
            }
            if ((objective.sval_base == unitID) == null)
            {
                goto Label_0045;
            }
        Label_003D:
            this.AddTrophyCounter(objective, 1);
        Label_0045:
            num -= 1;
        Label_0049:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnJobLevelChange(string unitID, string jobID, int rank, bool verify, int rankDelta)
        {
            char[] chArray1;
            char[] chArray;
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            string[] strArray;
            int num2;
            TrophyObjective objective2;
            string[] strArray2;
            int num3;
            TrophyObjective objective3;
            string[] strArray3;
            chArray1 = new char[] { 0x2c };
            chArray = chArray1;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x16);
            if (verify != null)
            {
                goto Label_007A;
            }
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0073;
        Label_002B:
            objective = objectiveArray[num];
            strArray = objective.sval_base.Split(chArray);
            if ((strArray[0] == unitID) == null)
            {
                goto Label_006F;
            }
            if ((strArray[1] == jobID) == null)
            {
                goto Label_006F;
            }
            if (objective.ival > rank)
            {
                goto Label_006F;
            }
            this.AddTrophyCounter(objective, 1);
        Label_006F:
            num -= 1;
        Label_0073:
            if (num >= 0)
            {
                goto Label_002B;
            }
        Label_007A:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x1b);
            if (verify != null)
            {
                goto Label_0105;
            }
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_00FD;
        Label_009A:
            objective2 = objectiveArray[num2];
            if (string.IsNullOrEmpty(objective2.sval_base) == null)
            {
                goto Label_00C0;
            }
            this.AddTrophyCounter(objective2, rankDelta);
            goto Label_00F7;
        Label_00C0:
            strArray2 = objective2.sval_base.Split(chArray);
            if ((strArray2[0] == unitID) == null)
            {
                goto Label_00F7;
            }
            if ((strArray2[1] == jobID) == null)
            {
                goto Label_00F7;
            }
            this.AddTrophyCounter(objective2, rankDelta);
        Label_00F7:
            num2 -= 1;
        Label_00FD:
            if (num2 >= 0)
            {
                goto Label_009A;
            }
        Label_0105:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x1c);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_018C;
        Label_011E:
            objective3 = objectiveArray[num3];
            if (objective3.ival > rank)
            {
                goto Label_0186;
            }
            if (string.IsNullOrEmpty(objective3.sval_base) == null)
            {
                goto Label_0150;
            }
            this.AddTrophyCounter(objective3, 1);
            goto Label_0186;
        Label_0150:
            strArray3 = objective3.sval_base.Split(chArray);
            if ((strArray3[0] == unitID) == null)
            {
                goto Label_0186;
            }
            if ((strArray3[1] == jobID) == null)
            {
                goto Label_0186;
            }
            this.AddTrophyCounter(objective3, 1);
        Label_0186:
            num3 -= 1;
        Label_018C:
            if (num3 >= 0)
            {
                goto Label_011E;
            }
            return;
        }

        public void OnLimitBreak(string unitID, int delta)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x1d);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0049;
        Label_0018:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_003D;
            }
            if ((objective.sval_base == unitID) == null)
            {
                goto Label_0045;
            }
        Label_003D:
            this.AddTrophyCounter(objective, delta);
        Label_0045:
            num -= 1;
        Label_0049:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnLimitBreakCheck(string unitID, int awake)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            if (awake > 0)
            {
                goto Label_0008;
            }
            return;
        Label_0008:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x1d);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0056;
        Label_0020:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) == null)
            {
                goto Label_0039;
            }
            goto Label_0052;
        Label_0039:
            if ((objective.sval_base == unitID) == null)
            {
                goto Label_0052;
            }
            this.SetTrophyCounter(objective, awake);
        Label_0052:
            num -= 1;
        Label_0056:
            if (num >= 0)
            {
                goto Label_0020;
            }
            return;
        }

        public void OnLogin()
        {
            this.TrophyUpdateProgress();
            this.ResetPrevCheckHour();
            return;
        }

        public void OnLoginCount()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x17);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0039;
        Label_0018:
            objective = objectiveArray[num];
            if (objective.ival > this.LoginBonusCount)
            {
                goto Label_0035;
            }
            this.AddTrophyCounter(objective, 1);
        Label_0035:
            num -= 1;
        Label_0039:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnMixedConceptCard(string conceptCardID, int beforeLevel, int currentLevel, int beforeAwakeCount, int currentAwakeCount, int beforeTrust, int currentTrust)
        {
            MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardLevelupTrophy(conceptCardID, beforeLevel, currentLevel);
            MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardLimitBreakTrophy(conceptCardID, beforeAwakeCount, currentAwakeCount);
            MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrustUpTrophy(conceptCardID, beforeTrust, currentTrust);
            MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrustMaxTrophy(conceptCardID, currentTrust);
            return;
        }

        public void OnMultiTowerHelp()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x4c);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0028;
        Label_0018:
            objective = objectiveArray[num];
            this.AddTrophyCounter(objective, 1);
            num -= 1;
        Label_0028:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnOpenTobiraTrophy(long unitUniqueID)
        {
            UnitData data;
            data = this.FindUnitDataByUniqueID(unitUniqueID);
            this.UpdateSinsTobiraTrophy(data);
            this.CheckAllSinsTobiraNonTargetTrophy();
            return;
        }

        public void OnPlayerLevelChange(int delta)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            if (delta <= 0)
            {
                goto Label_0046;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(4);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_003F;
        Label_001E:
            objective = objectiveArray[num];
            if (objective.ival > this.Lv)
            {
                goto Label_003B;
            }
            this.AddTrophyCounter(objective, 1);
        Label_003B:
            num -= 1;
        Label_003F:
            if (num >= 0)
            {
                goto Label_001E;
            }
        Label_0046:
            return;
        }

        public void OnQuestLose(string questID)
        {
            QuestParam param;
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyObjective objective2;
            int num3;
            TrophyObjective objective3;
            int num4;
            TrophyObjective objective4;
            int num5;
            TrophyObjective objective5;
            TowerFloorParam param2;
            int num6;
            TrophyObjective objective6;
            int num7;
            TrophyObjective objective7;
            int num8;
            TrophyObjective objective8;
            param = MonoSingleton<GameManager>.Instance.FindQuest(questID);
            if (questID != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (param.type != 3)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x2f);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_00F0;
        Label_0038:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_0067;
            }
            if (objective.sval.Contains(questID) != null)
            {
                goto Label_00E4;
            }
            goto Label_00EC;
            goto Label_00E4;
        Label_0067:
            if (param.type == 5)
            {
                goto Label_00EC;
            }
            if (param.type == 13)
            {
                goto Label_00EC;
            }
            if (param.type == 2)
            {
                goto Label_00EC;
            }
            if (param.IsMulti != null)
            {
                goto Label_00EC;
            }
            if (param.type == 6)
            {
                goto Label_00EC;
            }
            if (param.difficulty != null)
            {
                goto Label_00EC;
            }
            if (param.type == 7)
            {
                goto Label_00EC;
            }
            if (param.IsVersus != null)
            {
                goto Label_00EC;
            }
            if (param.type == 15)
            {
                goto Label_00EC;
            }
            if (param.type != 0x10)
            {
                goto Label_00E4;
            }
            goto Label_00EC;
        Label_00E4:
            this.AddTrophyCounter(objective, 1);
        Label_00EC:
            num -= 1;
        Label_00F0:
            if (num >= 0)
            {
                goto Label_0038;
            }
            if (param.difficulty != 1)
            {
                goto Label_0139;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x30);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_0131;
        Label_011C:
            objective2 = objectiveArray[num2];
            this.AddTrophyCounter(objective2, 1);
            num2 -= 1;
        Label_0131:
            if (num2 >= 0)
            {
                goto Label_011C;
            }
        Label_0139:
            if (param.type != 2)
            {
                goto Label_017B;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x33);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_0173;
        Label_015E:
            objective3 = objectiveArray[num3];
            this.AddTrophyCounter(objective3, 1);
            num3 -= 1;
        Label_0173:
            if (num3 >= 0)
            {
                goto Label_015E;
            }
        Label_017B:
            if (param.type == 5)
            {
                goto Label_0193;
            }
            if (param.type != 7)
            {
                goto Label_01C9;
            }
        Label_0193:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x31);
            num4 = ((int) objectiveArray.Length) - 1;
            goto Label_01C1;
        Label_01AC:
            objective4 = objectiveArray[num4];
            this.AddTrophyCounter(objective4, 1);
            num4 -= 1;
        Label_01C1:
            if (num4 >= 0)
            {
                goto Label_01AC;
            }
        Label_01C9:
            if (param.type != 7)
            {
                goto Label_02A1;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(50);
            num5 = ((int) objectiveArray.Length) - 1;
            goto Label_0226;
        Label_01EE:
            objective5 = objectiveArray[num5];
            if (string.IsNullOrEmpty(objective5.sval_base) != null)
            {
                goto Label_0217;
            }
            if ((objective5.sval_base == questID) == null)
            {
                goto Label_0220;
            }
        Label_0217:
            this.AddTrophyCounter(objective5, 1);
        Label_0220:
            num5 -= 1;
        Label_0226:
            if (num5 >= 0)
            {
                goto Label_01EE;
            }
            param2 = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
            if (param2 == null)
            {
                goto Label_02A1;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(60);
            num6 = ((int) objectiveArray.Length) - 1;
            goto Label_0299;
        Label_025B:
            objective6 = objectiveArray[num6];
            if (string.IsNullOrEmpty(objective6.sval_base) != null)
            {
                goto Label_028A;
            }
            if ((objective6.sval_base == param2.tower_id) == null)
            {
                goto Label_0293;
            }
        Label_028A:
            this.AddTrophyCounter(objective6, 1);
        Label_0293:
            num6 -= 1;
        Label_0299:
            if (num6 >= 0)
            {
                goto Label_025B;
            }
        Label_02A1:
            if (param.IsVersus == null)
            {
                goto Label_035E;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x37);
            num7 = ((int) objectiveArray.Length) - 1;
            goto Label_02FD;
        Label_02C5:
            objective7 = objectiveArray[num7];
            if (string.IsNullOrEmpty(objective7.sval_base) != null)
            {
                goto Label_02EE;
            }
            if ((objective7.sval_base == questID) == null)
            {
                goto Label_02F7;
            }
        Label_02EE:
            this.AddTrophyCounter(objective7, 1);
        Label_02F7:
            num7 -= 1;
        Label_02FD:
            if (num7 >= 0)
            {
                goto Label_02C5;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x35);
            num8 = ((int) objectiveArray.Length) - 1;
            goto Label_0356;
        Label_031E:
            objective8 = objectiveArray[num8];
            if (string.IsNullOrEmpty(objective8.sval_base) != null)
            {
                goto Label_0347;
            }
            if ((objective8.sval_base == questID) == null)
            {
                goto Label_0350;
            }
        Label_0347:
            this.AddTrophyCounter(objective8, 1);
        Label_0350:
            num8 -= 1;
        Label_0356:
            if (num8 >= 0)
            {
                goto Label_031E;
            }
        Label_035E:
            return;
        }

        public void OnQuestStart(string questID)
        {
            QuestParam param;
            TrophyParam[] paramArray;
            int num;
            TrophyParam param2;
            int num2;
            TrophyParam[] paramArray2;
            int num3;
            TrophyParam param3;
            int num4;
            TrophyParam[] paramArray3;
            int num5;
            TrophyParam param4;
            int num6;
            param = MonoSingleton<GameManager>.Instance.FindQuest(questID);
            if (param != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (param.type != 3)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            if (param.type != 2)
            {
                goto Label_008F;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Trophies;
            num = ((int) paramArray.Length) - 1;
            goto Label_0088;
        Label_0042:
            param2 = paramArray[num];
            num2 = ((int) param2.Objectives.Length) - 1;
            goto Label_007C;
        Label_0057:
            if (param2.Objectives[num2].type != 15)
            {
                goto Label_0076;
            }
            this.AddTrophyCounter(param2, num2, 1);
        Label_0076:
            num2 -= 1;
        Label_007C:
            if (num2 >= 0)
            {
                goto Label_0057;
            }
            num -= 1;
        Label_0088:
            if (num >= 0)
            {
                goto Label_0042;
            }
        Label_008F:
            if (param.IsMulti == null)
            {
                goto Label_0112;
            }
            if (GlobalVars.ResumeMultiplayPlayerID != null)
            {
                goto Label_0112;
            }
            paramArray2 = MonoSingleton<GameManager>.Instance.Trophies;
            num3 = ((int) paramArray2.Length) - 1;
            goto Label_010A;
        Label_00BD:
            param3 = paramArray2[num3];
            num4 = ((int) param3.Objectives.Length) - 1;
            goto Label_00FC;
        Label_00D6:
            if (param3.Objectives[num4].type != 8)
            {
                goto Label_00F6;
            }
            this.AddTrophyCounter(param3, num4, 1);
        Label_00F6:
            num4 -= 1;
        Label_00FC:
            if (num4 >= 0)
            {
                goto Label_00D6;
            }
            num3 -= 1;
        Label_010A:
            if (num3 >= 0)
            {
                goto Label_00BD;
            }
        Label_0112:
            if (param.IsMultiTower == null)
            {
                goto Label_0196;
            }
            if (GlobalVars.ResumeMultiplayPlayerID != null)
            {
                goto Label_0196;
            }
            paramArray3 = MonoSingleton<GameManager>.Instance.Trophies;
            num5 = ((int) paramArray3.Length) - 1;
            goto Label_018E;
        Label_0140:
            param4 = paramArray3[num5];
            num6 = ((int) param4.Objectives.Length) - 1;
            goto Label_0180;
        Label_0159:
            if (param4.Objectives[num6].type != 0x4d)
            {
                goto Label_017A;
            }
            this.AddTrophyCounter(param4, num6, 1);
        Label_017A:
            num6 -= 1;
        Label_0180:
            if (num6 >= 0)
            {
                goto Label_0159;
            }
            num5 -= 1;
        Label_018E:
            if (num5 >= 0)
            {
                goto Label_0140;
            }
        Label_0196:
            return;
        }

        public unsafe void OnQuestWin(string questID, BattleCore.Record battleRecord)
        {
            QuestParam param;
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            TrophyObjective objective2;
            int num9;
            TrophyObjective objective3;
            int num10;
            TrophyObjective objective4;
            int num11;
            TrophyObjective objective5;
            SupportData data;
            SupportData data2;
            List<SupportData>.Enumerator enumerator;
            int num12;
            TrophyObjective objective6;
            int num13;
            TrophyObjective objective7;
            int num14;
            TrophyObjective objective8;
            List<JSON_MyPhotonPlayerParam> list;
            int num15;
            TrophyObjective objective9;
            List<JSON_MyPhotonPlayerParam> list2;
            int num16;
            TrophyObjective objective10;
            TowerFloorParam param2;
            int num17;
            TrophyObjective objective11;
            int num18;
            TrophyObjective objective12;
            int num19;
            TrophyObjective objective13;
            int num20;
            TrophyObjective objective14;
            param = MonoSingleton<GameManager>.Instance.FindQuest(questID);
            if (param != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (param.type != 3)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(1);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_00EF;
        Label_0037:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_0066;
            }
            if (objective.sval.Contains(questID) != null)
            {
                goto Label_00E3;
            }
            goto Label_00EB;
            goto Label_00E3;
        Label_0066:
            if (param.type == 5)
            {
                goto Label_00EB;
            }
            if (param.type == 13)
            {
                goto Label_00EB;
            }
            if (param.type == 2)
            {
                goto Label_00EB;
            }
            if (param.IsMulti != null)
            {
                goto Label_00EB;
            }
            if (param.type == 6)
            {
                goto Label_00EB;
            }
            if (param.difficulty != null)
            {
                goto Label_00EB;
            }
            if (param.type == 7)
            {
                goto Label_00EB;
            }
            if (param.IsVersus != null)
            {
                goto Label_00EB;
            }
            if (param.type == 15)
            {
                goto Label_00EB;
            }
            if (param.type != 0x10)
            {
                goto Label_00E3;
            }
            goto Label_00EB;
        Label_00E3:
            this.AddTrophyCounter(objective, 1);
        Label_00EB:
            num -= 1;
        Label_00EF:
            if (num >= 0)
            {
                goto Label_0037;
            }
            if (battleRecord == null)
            {
                goto Label_019A;
            }
            if (param.bonusObjective == null)
            {
                goto Label_019A;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x4f);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_0192;
        Label_0120:
            if ((objectiveArray[num2].sval_base != questID) == null)
            {
                goto Label_0139;
            }
            goto Label_018C;
        Label_0139:
            num3 = 0;
            num4 = 0;
            goto Label_0163;
        Label_0144:
            if ((battleRecord.allBonusFlags & (1 << (num4 & 0x1f))) == null)
            {
                goto Label_015D;
            }
            num3 += 1;
        Label_015D:
            num4 += 1;
        Label_0163:
            if (num4 < ((int) param.bonusObjective.Length))
            {
                goto Label_0144;
            }
            if (num3 < ((int) param.bonusObjective.Length))
            {
                goto Label_018C;
            }
            this.AddTrophyCounter(objectiveArray[num2], 1);
        Label_018C:
            num2 -= 1;
        Label_0192:
            if (num2 >= 0)
            {
                goto Label_0120;
            }
        Label_019A:
            if (battleRecord == null)
            {
                goto Label_0266;
            }
            if (param.bonusObjective == null)
            {
                goto Label_0266;
            }
            num5 = 0;
            num6 = 0;
            goto Label_01E2;
        Label_01B6:
            if ((battleRecord.allBonusFlags & (1 << (num6 & 0x1f))) != null)
            {
                goto Label_01D6;
            }
            if (param.IsMissionClear(num6) == null)
            {
                goto Label_01DC;
            }
        Label_01D6:
            num5 += 1;
        Label_01DC:
            num6 += 1;
        Label_01E2:
            if (num6 < ((int) param.bonusObjective.Length))
            {
                goto Label_01B6;
            }
            if (num5 < ((int) param.bonusObjective.Length))
            {
                goto Label_0266;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(100);
            num7 = ((int) objectiveArray.Length) - 1;
            goto Label_0243;
        Label_0219:
            if ((objectiveArray[num7].sval_base != questID) == null)
            {
                goto Label_0232;
            }
            goto Label_023D;
        Label_0232:
            this.AddTrophyCounter(objectiveArray[num7], 1);
        Label_023D:
            num7 -= 1;
        Label_0243:
            if (num7 >= 0)
            {
                goto Label_0219;
            }
            if (param.IsMissionCompleteALL() != null)
            {
                goto Label_0266;
            }
            MonoSingleton<GameManager>.Instance.Player.UpdateCompleteAllQuestCountTrophy(param);
        Label_0266:
            if (param.difficulty != 2)
            {
                goto Label_02A8;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x4b);
            num8 = ((int) objectiveArray.Length) - 1;
            goto Label_02A0;
        Label_028B:
            objective2 = objectiveArray[num8];
            this.AddTrophyCounter(objective2, 1);
            num8 -= 1;
        Label_02A0:
            if (num8 >= 0)
            {
                goto Label_028B;
            }
        Label_02A8:
            if (param.difficulty != 1)
            {
                goto Label_02E9;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(5);
            num9 = ((int) objectiveArray.Length) - 1;
            goto Label_02E1;
        Label_02CC:
            objective3 = objectiveArray[num9];
            this.AddTrophyCounter(objective3, 1);
            num9 -= 1;
        Label_02E1:
            if (num9 >= 0)
            {
                goto Label_02CC;
            }
        Label_02E9:
            if (param.type != 2)
            {
                goto Label_032B;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x10);
            num10 = ((int) objectiveArray.Length) - 1;
            goto Label_0323;
        Label_030E:
            objective4 = objectiveArray[num10];
            this.AddTrophyCounter(objective4, 1);
            num10 -= 1;
        Label_0323:
            if (num10 >= 0)
            {
                goto Label_030E;
            }
        Label_032B:
            if (param.type == 5)
            {
                goto Label_0343;
            }
            if (param.type != 7)
            {
                goto Label_0378;
            }
        Label_0343:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(6);
            num11 = ((int) objectiveArray.Length) - 1;
            goto Label_0370;
        Label_035B:
            objective5 = objectiveArray[num11];
            this.AddTrophyCounter(objective5, 1);
            num11 -= 1;
        Label_0370:
            if (num11 >= 0)
            {
                goto Label_035B;
            }
        Label_0378:
            data = GlobalVars.SelectedSupport;
            if (param.type != 15)
            {
                goto Label_03E6;
            }
            data = null;
            if (GlobalVars.OrdealSupports == null)
            {
                goto Label_03E6;
            }
            enumerator = GlobalVars.OrdealSupports.GetEnumerator();
        Label_03AA:
            try
            {
                goto Label_03C8;
            Label_03AF:
                data2 = &enumerator.Current;
                if (data2 == null)
                {
                    goto Label_03C8;
                }
                data = data2;
                goto Label_03D4;
            Label_03C8:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_03AF;
                }
            Label_03D4:
                goto Label_03E6;
            }
            finally
            {
            Label_03D9:
                ((List<SupportData>.Enumerator) enumerator).Dispose();
            }
        Label_03E6:
            if (data == null)
            {
                goto Label_0423;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x22);
            num12 = ((int) objectiveArray.Length) - 1;
            goto Label_041B;
        Label_0406:
            objective6 = objectiveArray[num12];
            this.AddTrophyCounter(objective6, 1);
            num12 -= 1;
        Label_041B:
            if (num12 >= 0)
            {
                goto Label_0406;
            }
        Label_0423:
            if (param.IsMulti == null)
            {
                goto Label_0585;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x23);
            num13 = ((int) objectiveArray.Length) - 1;
            goto Label_047F;
        Label_0447:
            objective7 = objectiveArray[num13];
            if (string.IsNullOrEmpty(objective7.sval_base) != null)
            {
                goto Label_0470;
            }
            if ((objective7.sval_base == questID) == null)
            {
                goto Label_0479;
            }
        Label_0470:
            this.AddTrophyCounter(objective7, 1);
        Label_0479:
            num13 -= 1;
        Label_047F:
            if (num13 >= 0)
            {
                goto Label_0447;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(40);
            num14 = ((int) objectiveArray.Length) - 1;
            goto Label_04FE;
        Label_04A0:
            objective8 = objectiveArray[num14];
            if (string.IsNullOrEmpty(objective8.sval_base) != null)
            {
                goto Label_04C9;
            }
            if ((objective8.sval_base == questID) == null)
            {
                goto Label_04F8;
            }
        Label_04C9:
            list = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
            if (list == null)
            {
                goto Label_04F8;
            }
            if (list.Count < objective8.ival)
            {
                goto Label_04F8;
            }
            this.AddTrophyCounter(objective8, 1);
        Label_04F8:
            num14 -= 1;
        Label_04FE:
            if (num14 >= 0)
            {
                goto Label_04A0;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x29);
            num15 = ((int) objectiveArray.Length) - 1;
            goto Label_057D;
        Label_051F:
            objective9 = objectiveArray[num15];
            if (string.IsNullOrEmpty(objective9.sval_base) != null)
            {
                goto Label_0548;
            }
            if ((objective9.sval_base == questID) == null)
            {
                goto Label_0577;
            }
        Label_0548:
            list2 = PunMonoSingleton<MyPhoton>.Instance.GetMyPlayersStarted();
            if (list2 == null)
            {
                goto Label_0577;
            }
            if (list2.Count > objective9.ival)
            {
                goto Label_0577;
            }
            this.AddTrophyCounter(objective9, 1);
        Label_0577:
            num15 -= 1;
        Label_057D:
            if (num15 >= 0)
            {
                goto Label_051F;
            }
        Label_0585:
            if (param.type != 7)
            {
                goto Label_065D;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x2e);
            num16 = ((int) objectiveArray.Length) - 1;
            goto Label_05E2;
        Label_05AA:
            objective10 = objectiveArray[num16];
            if (string.IsNullOrEmpty(objective10.sval_base) != null)
            {
                goto Label_05D3;
            }
            if ((objective10.sval_base == questID) == null)
            {
                goto Label_05DC;
            }
        Label_05D3:
            this.AddTrophyCounter(objective10, 1);
        Label_05DC:
            num16 -= 1;
        Label_05E2:
            if (num16 >= 0)
            {
                goto Label_05AA;
            }
            param2 = MonoSingleton<GameManager>.Instance.FindTowerFloor(questID);
            if (param2 == null)
            {
                goto Label_065D;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(60);
            num17 = ((int) objectiveArray.Length) - 1;
            goto Label_0655;
        Label_0617:
            objective11 = objectiveArray[num17];
            if (string.IsNullOrEmpty(objective11.sval_base) != null)
            {
                goto Label_0646;
            }
            if ((objective11.sval_base == param2.tower_id) == null)
            {
                goto Label_064F;
            }
        Label_0646:
            this.AddTrophyCounter(objective11, 1);
        Label_064F:
            num17 -= 1;
        Label_0655:
            if (num17 >= 0)
            {
                goto Label_0617;
            }
        Label_065D:
            if (param.IsVersus == null)
            {
                goto Label_071A;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x36);
            num18 = ((int) objectiveArray.Length) - 1;
            goto Label_06B9;
        Label_0681:
            objective12 = objectiveArray[num18];
            if (string.IsNullOrEmpty(objective12.sval_base) != null)
            {
                goto Label_06AA;
            }
            if ((objective12.sval_base == questID) == null)
            {
                goto Label_06B3;
            }
        Label_06AA:
            this.AddTrophyCounter(objective12, 1);
        Label_06B3:
            num18 -= 1;
        Label_06B9:
            if (num18 >= 0)
            {
                goto Label_0681;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x35);
            num19 = ((int) objectiveArray.Length) - 1;
            goto Label_0712;
        Label_06DA:
            objective13 = objectiveArray[num19];
            if (string.IsNullOrEmpty(objective13.sval_base) != null)
            {
                goto Label_0703;
            }
            if ((objective13.sval_base == questID) == null)
            {
                goto Label_070C;
            }
        Label_0703:
            this.AddTrophyCounter(objective13, 1);
        Label_070C:
            num19 -= 1;
        Label_0712:
            if (num19 >= 0)
            {
                goto Label_06DA;
            }
        Label_071A:
            if (param.type != 15)
            {
                goto Label_07AA;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x69);
            num20 = ((int) objectiveArray.Length) - 1;
            goto Label_07A2;
        Label_0740:
            objective14 = objectiveArray[num20];
            if (string.IsNullOrEmpty(objective14.sval_base) != null)
            {
                goto Label_077C;
            }
            if ((objective14.sval_base == param.iname) == null)
            {
                goto Label_079C;
            }
            this.AddTrophyCounter(objective14, 1);
            goto Label_079C;
        Label_077C:
            DebugUtility.LogError("レコードミッション「" + objective14.Param.Name + "」はクエストが指定されていません。");
        Label_079C:
            num20 -= 1;
        Label_07A2:
            if (num20 >= 0)
            {
                goto Label_0740;
            }
        Label_07AA:
            return;
        }

        public void OnReadTips(string trophyIname)
        {
            TrophyObjective[] objectiveArray;
            TrophyObjective objective;
            TrophyObjective[] objectiveArray2;
            int num;
            TrophyObjective[] objectiveArray3;
            TrophyObjective objective2;
            TrophyObjective[] objectiveArray4;
            int num2;
            objectiveArray2 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x51);
            num = 0;
            goto Label_003C;
        Label_0016:
            objective = objectiveArray2[num];
            if ((objective.sval_base != trophyIname) == null)
            {
                goto Label_0030;
            }
            goto Label_0038;
        Label_0030:
            this.AddTrophyCounter(objective, 1);
        Label_0038:
            num += 1;
        Label_003C:
            if (num < ((int) objectiveArray2.Length))
            {
                goto Label_0016;
            }
            objectiveArray4 = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x52);
            num2 = 0;
            goto Label_0075;
        Label_005F:
            objective2 = objectiveArray4[num2];
            this.AddTrophyCounter(objective2, 1);
            num2 += 1;
        Label_0075:
            if (num2 < ((int) objectiveArray4.Length))
            {
                goto Label_005F;
            }
            return;
        }

        public void OnSoubiPowerUp()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(10);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0028;
        Label_0018:
            objective = objectiveArray[num];
            this.AddTrophyCounter(objective, 1);
            num -= 1;
        Label_0028:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnSoubiSet(string unitID, int countUp)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x1a);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0049;
        Label_0018:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_003D;
            }
            if ((objective.sval_base == unitID) == null)
            {
                goto Label_0045;
            }
        Label_003D:
            this.AddTrophyCounter(objective, countUp);
        Label_0045:
            num -= 1;
        Label_0049:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void OnTowerScore(bool isNow)
        {
            GameManager manager;
            TowerResuponse resuponse;
            int num;
            TrophyObjective[] objectiveArray;
            int num2;
            TrophyObjective objective;
            manager = MonoSingleton<GameManager>.Instance;
            resuponse = manager.TowerResuponse;
            if (resuponse != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            if (string.IsNullOrEmpty(resuponse.TowerID) == null)
            {
                goto Label_0025;
            }
            return;
        Label_0025:
            if (resuponse.speedRank != null)
            {
                goto Label_003C;
            }
            if (resuponse.techRank != null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            num = manager.CalcTowerScore(isNow);
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x3d);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_00A7;
        Label_005D:
            objective = objectiveArray[num2];
            if (num > objective.ival)
            {
                goto Label_00A1;
            }
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_0098;
            }
            if (string.Equals(objective.sval_base, resuponse.TowerID) == null)
            {
                goto Label_00A1;
            }
        Label_0098:
            this.SetTrophyCounter(objective, num);
        Label_00A1:
            num2 -= 1;
        Label_00A7:
            if (num2 >= 0)
            {
                goto Label_005D;
            }
            return;
        }

        public void OnUnitLevelChange(string unitID, int delta, int level, bool verify)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyObjective objective2;
            int num3;
            TrophyObjective objective3;
            if (delta > 0)
            {
                goto Label_000E;
            }
            if (verify == null)
            {
                goto Label_0122;
            }
        Label_000E:
            if (verify != null)
            {
                goto Label_0061;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(20);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_005A;
        Label_002D:
            objective = objectiveArray[num];
            if ((objective.sval_base == unitID) == null)
            {
                goto Label_0056;
            }
            if (objective.ival > level)
            {
                goto Label_0056;
            }
            this.AddTrophyCounter(objective, delta);
        Label_0056:
            num -= 1;
        Label_005A:
            if (num >= 0)
            {
                goto Label_002D;
            }
        Label_0061:
            if (verify != null)
            {
                goto Label_00BC;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x18);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_00B5;
        Label_0080:
            objective2 = objectiveArray[num2];
            if (string.IsNullOrEmpty(objective2.sval_base) != null)
            {
                goto Label_00A8;
            }
            if ((objective2.sval_base == unitID) == null)
            {
                goto Label_00B1;
            }
        Label_00A8:
            this.AddTrophyCounter(objective2, delta);
        Label_00B1:
            num2 -= 1;
        Label_00B5:
            if (num2 >= 0)
            {
                goto Label_0080;
            }
        Label_00BC:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x19);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_011A;
        Label_00D5:
            objective3 = objectiveArray[num3];
            if (objective3.ival > level)
            {
                goto Label_0114;
            }
            if (string.IsNullOrEmpty(objective3.sval_base) != null)
            {
                goto Label_010B;
            }
            if ((objective3.sval_base == unitID) == null)
            {
                goto Label_0114;
            }
        Label_010B:
            this.AddTrophyCounter(objective3, 1);
        Label_0114:
            num3 -= 1;
        Label_011A:
            if (num3 >= 0)
            {
                goto Label_00D5;
            }
        Label_0122:
            return;
        }

        public void OnUnlockTobiraTrophy(long unitUniqueID)
        {
            UnitData data;
            data = this.FindUnitDataByUniqueID(unitUniqueID);
            this.UpdateUnlockTobiraUnitCountTrophy();
            this.UpdateUnlockTobiraUnitTrophy(data);
            return;
        }

        public void OverWriteConceptCardMaterials(JSON_ConceptCardMaterial[] concept_card_materials)
        {
            ConceptCardMaterialData data;
            int num;
            <OverWriteConceptCardMaterials>c__AnonStorey24A storeya;
            if (concept_card_materials != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            data = null;
            num = 0;
            goto Label_011C;
        Label_0010:
            storeya = new <OverWriteConceptCardMaterials>c__AnonStorey24A();
            storeya.param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(concept_card_materials[num].iname);
            if (storeya.param == null)
            {
                goto Label_0118;
            }
            if (storeya.param.type != 2)
            {
                goto Label_0085;
            }
            data = this.mConceptCardExpMaterials.Find(new Predicate<ConceptCardMaterialData>(storeya.<>m__154));
            if (data == null)
            {
                goto Label_00C7;
            }
            data.Num = concept_card_materials[num].num;
            goto Label_00C7;
        Label_0085:
            if (storeya.param.type != 3)
            {
                goto Label_00C7;
            }
            data = this.mConceptCardTrustMaterials.Find(new Predicate<ConceptCardMaterialData>(storeya.<>m__155));
            if (data == null)
            {
                goto Label_00C7;
            }
            data.Num = concept_card_materials[num].num;
        Label_00C7:
            if (data.Num != null)
            {
                goto Label_0118;
            }
            if (storeya.param.type != 2)
            {
                goto Label_00FA;
            }
            this.mConceptCardExpMaterials.Remove(data);
            goto Label_0118;
        Label_00FA:
            if (storeya.param.type != 3)
            {
                goto Label_0118;
            }
            this.mConceptCardTrustMaterials.Remove(data);
        Label_0118:
            num += 1;
        Label_011C:
            if (num < ((int) concept_card_materials.Length))
            {
                goto Label_0010;
            }
            return;
        }

        private void PlayerLevelUp(int delta)
        {
            GameManager manager;
            PlayerParam param;
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.MasterParam.GetPlayerParam(this.mLv);
            this.mStamina.valMax = param.pt;
            this.mStamina.val = Math.Min(this.mStamina.val + (manager.MasterParam.FixParam.StaminaAdd2 * delta), this.StaminaStockCap);
            this.UpdateUnlocks();
            if (Network.Mode != 1)
            {
                goto Label_0088;
            }
            this.SavePlayerPrefs();
        Label_0088:
            return;
        }

        public bool RankUpAbility(AbilityData ability)
        {
            OInt num;
            if (this.CheckRankUpAbility(ability) != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            this.GainGold(-ability.GetNextGold());
            ability.GainExp(1);
            this.mAbilityRankUpCount.val = Math.Max(this.mAbilityRankUpCount.val = OInt.op_Decrement(this.mAbilityRankUpCount.val), 0);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            return 1;
        }

        public bool RarityUpUnit(UnitData unit)
        {
            RecipeParam param;
            if (unit.CheckUnitRarityUp() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            param = unit.GetRarityUpRecipe();
            if (param.cost <= this.mGold)
            {
                goto Label_002C;
            }
            return 0;
        Label_002C:
            if (unit.UnitRarityUp() != null)
            {
                goto Label_0039;
            }
            return 0;
        Label_0039:
            this.mGold -= param.cost;
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(2);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x10);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
            return 1;
        }

        public void RecordAllCompleteCheck(TrophyCategoryParam category)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x6a);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0028;
        Label_0018:
            objective = objectiveArray[num];
            this.AddTrophyCounter(objective, 1);
            num -= 1;
        Label_0028:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public TrophyState RegistTrophyStateDictByProg(TrophyParam _trophy, JSON_TrophyProgress _prog)
        {
            TrophyState state;
            int num;
            state = this.CreateTrophyState(_trophy);
            num = 0;
            goto Label_0023;
        Label_000F:
            state.Count[num] = _prog.pts[num];
            num += 1;
        Label_0023:
            if (num >= ((int) _prog.pts.Length))
            {
                goto Label_003F;
            }
            if (num < ((int) state.Count.Length))
            {
                goto Label_000F;
            }
        Label_003F:
            state.StartYMD = _prog.ymd;
            state.IsEnded = (_prog.rewarded_at == 0) == 0;
            state.RewardedAt = DateTime.MinValue;
            if (_prog.rewarded_at == null)
            {
                goto Label_009A;
            }
        Label_0073:
            try
            {
                state.RewardedAt = SRPG_Extensions.FromYMD(_prog.rewarded_at);
                goto Label_009A;
            }
            catch
            {
            Label_0089:
                state.RewardedAt = DateTime.MinValue;
                goto Label_009A;
            }
        Label_009A:
            this.AddTrophyStateDict(state);
            return state;
        }

        public void RegistTrophyStateDictByProgExtra(JSON_TrophyProgress[] _prog)
        {
            int num;
            TrophyParam param;
            if (_prog == null)
            {
                goto Label_004E;
            }
            if (((int) _prog.Length) <= 0)
            {
                goto Label_004E;
            }
            num = 0;
            goto Label_0045;
        Label_0016:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetTrophy(_prog[num].iname);
            MonoSingleton<GameManager>.Instance.Player.RegistTrophyStateDictByProgExtra(param, _prog[num]);
            num += 1;
        Label_0045:
            if (num < ((int) _prog.Length))
            {
                goto Label_0016;
            }
        Label_004E:
            return;
        }

        public void RegistTrophyStateDictByProgExtra(TrophyParam _trophy, JSON_TrophyProgress _prog)
        {
            TrophyState state;
            int num;
            <RegistTrophyStateDictByProgExtra>c__AnonStorey240 storey;
            storey = new <RegistTrophyStateDictByProgExtra>c__AnonStorey240();
            storey._trophy = _trophy;
            if (this.mTrophyStatesInameDict.ContainsKey(storey._trophy.iname) != null)
            {
                goto Label_003A;
            }
            this.AddTrophyStateDict(this.CreateTrophyState(storey._trophy));
        Label_003A:
            state = this.mTrophyStatesInameDict[storey._trophy.iname].Find(new Predicate<TrophyState>(storey.<>m__147));
            if (state == null)
            {
                goto Label_0073;
            }
            if (state.IsCompleted == null)
            {
                goto Label_0074;
            }
        Label_0073:
            return;
        Label_0074:
            num = 0;
            goto Label_00A6;
        Label_007B:
            state.Count[num] = Math.Min(_prog.pts[num], storey._trophy.Objectives[num].ival);
            num += 1;
        Label_00A6:
            if (num >= ((int) storey._trophy.Objectives.Length))
            {
                goto Label_00D5;
            }
            if (num >= ((int) _prog.pts.Length))
            {
                goto Label_00D5;
            }
            if (num < ((int) state.Count.Length))
            {
                goto Label_007B;
            }
        Label_00D5:
            if (state.IsCompleted == null)
            {
                goto Label_00FC;
            }
            if (state.Param.DispType != 2)
            {
                goto Label_00FC;
            }
            NotifyList.PushAward(state.Param);
        Label_00FC:
            state.StartYMD = _prog.ymd;
            state.IsEnded = (_prog.rewarded_at == 0) == 0;
            state.IsDirty = 1;
            return;
        }

        private void RemoveArtifact(ArtifactData item)
        {
            this.mArtifacts.Remove(item);
            this.RemoveArtifactNumByRarity(item);
            return;
        }

        private unsafe void RemoveArtifactNumByRarity(ArtifactData item)
        {
            Dictionary<int, int> dictionary;
            int num;
            if (this.mArtifactsNumByRarity.TryGetValue(item.ArtifactParam.iname, &dictionary) == null)
            {
                goto Label_0069;
            }
            if (dictionary.TryGetValue(item.Rarity, &num) == null)
            {
                goto Label_0069;
            }
            num -= 1;
            if (num > 0)
            {
                goto Label_0057;
            }
            dictionary.Remove(item.Rarity);
            goto Label_0069;
        Label_0057:
            dictionary[item.Rarity] = num;
        Label_0069:
            return;
        }

        public void RemoveConceptCardData(long[] iids)
        {
            UnitData data;
            <RemoveConceptCardData>c__AnonStorey246 storey;
            <RemoveConceptCardData>c__AnonStorey247 storey2;
            storey = new <RemoveConceptCardData>c__AnonStorey246();
            storey.iids = iids;
            this.mConceptCards.RemoveAll(new Predicate<ConceptCardData>(storey.<>m__14E));
            storey2 = new <RemoveConceptCardData>c__AnonStorey247();
            storey2.<>f__ref$582 = storey;
            storey2.i = 0;
            goto Label_0071;
        Label_003E:
            data = this.Units.Find(new Predicate<UnitData>(storey2.<>m__14F));
            if (data == null)
            {
                goto Label_0063;
            }
            data.ConceptCard = null;
        Label_0063:
            storey2.i += 1;
        Label_0071:
            if (storey2.i < ((int) storey.iids.Length))
            {
                goto Label_003E;
            }
            this.UpdateConceptCardNum();
            return;
        }

        public void RemoveFriendFollower(string fuid)
        {
            FriendData data;
            <RemoveFriendFollower>c__AnonStorey23C storeyc;
            storeyc = new <RemoveFriendFollower>c__AnonStorey23C();
            storeyc.fuid = fuid;
            if (string.IsNullOrEmpty(storeyc.fuid) == null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            data = this.FriendsFollower.Find(new Predicate<FriendData>(storeyc.<>m__143));
            if (data == null)
            {
                goto Label_0057;
            }
            this.FriendsFollower.Remove(data);
            this.FollowerNum -= 1;
        Label_0057:
            return;
        }

        public void RemoveFriendFollowerAll()
        {
            MonoSingleton<GameManager>.Instance.Player.FriendsFollower.Clear();
            this.FollowerNum = 0;
            return;
        }

        private void ResetArtifacts()
        {
            this.mArtifacts.Clear();
            this.mArtifactsNumByRarity.Clear();
            return;
        }

        public void ResetBuyGoldNum()
        {
            this.mGoldBuyNum = 0;
            return;
        }

        public void ResetMissionClearAt()
        {
            this.mMissionClearAt = -1L;
            return;
        }

        private void ResetPrevCheckHour()
        {
            this.mPrevCheckHour = -1;
            return;
        }

        public unsafe void ResetQuestChallengeResets()
        {
            QuestParam[] paramArray;
            int num;
            long num2;
            DateTime time;
            paramArray = MonoSingleton<GameManager>.Instance.Quests;
            num = ((int) paramArray.Length) - 1;
            goto Label_005E;
        Label_0016:
            if (paramArray[num].dayReset <= 0)
            {
                goto Label_005A;
            }
            num2 = paramArray[num].end - paramArray[num].start;
            time = TimeManager.FromUnixTime(num2);
            if (paramArray[num].dayReset < &time.Day)
            {
                goto Label_005A;
            }
            paramArray[num].dailyReset = 0;
        Label_005A:
            num -= 1;
        Label_005E:
            if (num >= 0)
            {
                goto Label_0016;
            }
            this.mQuestListDirty = 1;
            return;
        }

        public unsafe void ResetQuestChallenges()
        {
            QuestParam[] paramArray;
            int num;
            long num2;
            DateTime time;
            paramArray = MonoSingleton<GameManager>.Instance.Quests;
            num = ((int) paramArray.Length) - 1;
            goto Label_005E;
        Label_0016:
            if (paramArray[num].dayReset <= 0)
            {
                goto Label_005A;
            }
            num2 = paramArray[num].end - paramArray[num].start;
            time = TimeManager.FromUnixTime(num2);
            if (paramArray[num].dayReset < &time.Day)
            {
                goto Label_005A;
            }
            paramArray[num].dailyCount = 0;
        Label_005A:
            num -= 1;
        Label_005E:
            if (num >= 0)
            {
                goto Label_0016;
            }
            this.mQuestListDirty = 1;
            return;
        }

        public void ResetQuestStates()
        {
            QuestParam[] paramArray;
            int num;
            paramArray = MonoSingleton<GameManager>.Instance.Quests;
            num = ((int) paramArray.Length) - 1;
            goto Label_0023;
        Label_0016:
            paramArray[num].state = 0;
            num -= 1;
        Label_0023:
            if (num >= 0)
            {
                goto Label_0016;
            }
            this.mQuestListDirty = 1;
            return;
        }

        public void ResetStaminaRecoverCount()
        {
            this.mStaminaBuyNum = 0;
            return;
        }

        public void RestoreAbilityRankUpCount()
        {
            this.mAbilityRankUpCount.val = this.mAbilityRankUpCount.valMax;
            this.mAbilityRankUpCount.at = Network.GetServerTime();
            return;
        }

        public void RewardedRankMatchMission(string iname)
        {
            SRPG.RankMatchMissionState state;
            <RewardedRankMatchMission>c__AnonStorey22F storeyf;
            storeyf = new <RewardedRankMatchMission>c__AnonStorey22F();
            storeyf.iname = iname;
            this.RankMatchMissionState.Find(new Predicate<SRPG.RankMatchMissionState>(storeyf.<>m__136)).Rewarded();
            return;
        }

        public void SaveInventory()
        {
            int num;
            num = 0;
            goto Label_0056;
        Label_0007:
            if (this.mInventory[num] == null)
            {
                goto Label_003D;
            }
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.PLAYERDATA_INVENTORY + ((int) num), this.mInventory[num].ItemID, 0);
            goto Label_0052;
        Label_003D:
            PlayerPrefsUtility.DeleteKey(PlayerPrefsUtility.PLAYERDATA_INVENTORY + ((int) num));
        Label_0052:
            num += 1;
        Label_0056:
            if (num < ((int) this.mInventory.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void SavePlayerPrefs()
        {
            IEnumerator enumerator;
            enumerator = this.SavePlayerPrefsAsync();
        Label_000C:
            if (enumerator.MoveNext() != null)
            {
                goto Label_000C;
            }
            return;
        }

        [DebuggerHidden]
        public IEnumerator SavePlayerPrefsAsync()
        {
            <SavePlayerPrefsAsync>c__Iterator7E iteratore;
            iteratore = new <SavePlayerPrefsAsync>c__Iterator7E();
            iteratore.<>f__this = this;
            return iteratore;
        }

        public void SavePlayerPrefsParty()
        {
            this.InternalSavePlayerPrefsParty();
            EditorPlayerPrefs.Flush();
            return;
        }

        public void SetCoinPurchaseResult(FulfillmentResult result)
        {
            if (result != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFreeCoin = result.CurrentFreeCoin;
            this.mPaidCoin = result.CurrentPaidCoin;
            this.mComCoin = result.CurrentCommonCoin;
            return;
        }

        public unsafe void SetCoinPurchaseResult(PaymentManager.CoinRecord record)
        {
            string str;
            string[] strArray;
            int num;
            FixParam param;
            long num2;
            DateTime time;
            DateTime time2;
            if (record != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFreeCoin = record.currentFreeCoin;
            this.mPaidCoin = record.currentPaidCoin;
            strArray = record.productIds;
            num = 0;
            goto Label_00F9;
        Label_0037:
            str = strArray[num];
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (str.Contains(param.VipCardProduct) == null)
            {
                goto Label_00F5;
            }
            num2 = Network.GetServerTime();
            if (num2 <= this.mVipExpiredAt)
            {
                goto Label_00C9;
            }
            time2 = &TimeManager.FromUnixTime(num2 + ((long) (((param.VipCardDate * 0x18) * 60) * 60))).Date + new TimeSpan(0x17, 0x3b, 0x3b);
            this.mVipExpiredAt = TimeManager.FromDateTime(time2);
            goto Label_00F5;
        Label_00C9:
            this.mVipExpiredAt += (long) (((param.VipCardDate * 0x18) * 60) * 60);
        Label_00F5:
            num += 1;
        Label_00F9:
            if (num < ((int) strArray.Length))
            {
                goto Label_0037;
            }
            return;
        }

        public void SetConceptCardNum(string iname, int value)
        {
            if (this.mConceptCardNum.ContainsKey(iname) == null)
            {
                goto Label_0023;
            }
            this.mConceptCardNum[iname] = value;
            goto Label_0030;
        Label_0023:
            this.mConceptCardNum.Add(iname, value);
        Label_0030:
            return;
        }

        public void SetDefenseParty(int index)
        {
            int num;
            num = 0;
            goto Label_0020;
        Label_0007:
            this.mPartys[num].IsDefense = index == num;
            num += 1;
        Label_0020:
            if (num < this.mPartys.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void SetEventCoinNum(string cost_iname, int num)
        {
            ItemData data;
            <SetEventCoinNum>c__AnonStorey244 storey;
            storey = new <SetEventCoinNum>c__AnonStorey244();
            storey.cost_iname = cost_iname;
            if (storey.cost_iname != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            data = MonoSingleton<GameManager>.Instance.Player.Items.Find(new Predicate<ItemData>(storey.<>m__14C));
            if (data == null)
            {
                goto Label_0047;
            }
            data.SetNum(num);
        Label_0047:
            return;
        }

        public void SetEventShopData(EventShopData shop)
        {
            this.mEventShops = shop;
            return;
        }

        public void SetHaveAward(string[] awards)
        {
            int num;
            if (awards == null)
            {
                goto Label_000F;
            }
            if (((int) awards.Length) > 0)
            {
                goto Label_0010;
            }
        Label_000F:
            return;
        Label_0010:
            this.mHaveAward.Clear();
            num = 0;
            goto Label_0041;
        Label_0022:
            if (string.IsNullOrEmpty(awards[num]) != null)
            {
                goto Label_003D;
            }
            this.mHaveAward.Add(awards[num]);
        Label_003D:
            num += 1;
        Label_0041:
            if (num < ((int) awards.Length))
            {
                goto Label_0022;
            }
            return;
        }

        public void SetInventory(int index, ItemData item)
        {
            if (0 > index)
            {
                goto Label_001E;
            }
            if (index >= ((int) this.mInventory.Length))
            {
                goto Label_001E;
            }
            this.mInventory[index] = item;
        Label_001E:
            return;
        }

        public void SetLimitedShopData(LimitedShopData shop)
        {
            this.mLimitedShops = shop;
            return;
        }

        public void SetMaxProgRankMatchMission(RankMatchMissionType type, int prog)
        {
            GameManager manager;
            List<VersusRankMissionParam> list;
            <SetMaxProgRankMatchMission>c__AnonStorey22D storeyd;
            storeyd = new <SetMaxProgRankMatchMission>c__AnonStorey22D();
            storeyd.type = type;
            storeyd.prog = prog;
            storeyd.<>f__this = this;
            manager = MonoSingleton<GameManager>.Instance;
            manager.GetVersusRankMissionList(manager.RankMatchScheduleId).ForEach(new Action<VersusRankMissionParam>(storeyd.<>m__135));
            return;
        }

        public void SetMissionClearAt(long unixTimeStamp)
        {
            this.mMissionClearAt = unixTimeStamp;
            return;
        }

        public unsafe void SetOrderResult(FulfillmentResult.OrderInfo order)
        {
            FixParam param;
            long num;
            DateTime time;
            DateTime time2;
            if (order != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (order.ProductId.Contains(param.VipCardProduct) == null)
            {
                goto Label_00C0;
            }
            num = Network.GetServerTime();
            if (num <= this.mVipExpiredAt)
            {
                goto Label_0094;
            }
            time2 = &TimeManager.FromUnixTime(num + ((long) (((param.VipCardDate * 0x18) * 60) * 60))).Date + new TimeSpan(0x17, 0x3b, 0x3b);
            this.mVipExpiredAt = TimeManager.FromDateTime(time2);
            goto Label_00C0;
        Label_0094:
            this.mVipExpiredAt += (long) (((param.VipCardDate * 0x18) * 60) * 60);
        Label_00C0:
            return;
        }

        public void SetParty(int index, PartyData party)
        {
            if (index < 0)
            {
                goto Label_001A;
            }
            if (index <= (this.mPartys.Count - 1))
            {
                goto Label_001B;
            }
        Label_001A:
            return;
        Label_001B:
            this.mPartys[index].SetParty(party);
            return;
        }

        public void SetPartyCurrentIndex(int index)
        {
            int num;
            num = 0;
            goto Label_0020;
        Label_0007:
            this.mPartys[num].Selected = index == num;
            num += 1;
        Label_0020:
            if (num < this.mPartys.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void SetQuestChallengeNumDaily(string name, int num)
        {
            QuestParam param;
            param = MonoSingleton<GameManager>.Instance.FindQuest(name);
            if (param == null)
            {
                goto Label_0019;
            }
            param.SetChallangeCount(num);
        Label_0019:
            return;
        }

        public void SetQuestListDirty()
        {
            this.mQuestListDirty = 1;
            return;
        }

        public void SetQuestMissionFlags(string name, bool[] missions)
        {
            int num;
            int num2;
            num = 0;
            if (missions == null)
            {
                goto Label_0030;
            }
            num2 = 0;
            goto Label_0027;
        Label_000F:
            if (missions[num2] == null)
            {
                goto Label_0023;
            }
            num |= 1 << ((num2 & 0x1f) & 0x1f);
        Label_0023:
            num2 += 1;
        Label_0027:
            if (num2 < ((int) missions.Length))
            {
                goto Label_000F;
            }
        Label_0030:
            this.SetQuestMissionFlags(name, num);
            return;
        }

        public void SetQuestMissionFlags(string name, int missions)
        {
            QuestParam param;
            param = MonoSingleton<GameManager>.Instance.FindQuest(name);
            if (param == null)
            {
                goto Label_0020;
            }
            param.clear_missions |= missions;
        Label_0020:
            return;
        }

        public void SetQuestState(string name, QuestStates st)
        {
            QuestParam param;
            param = MonoSingleton<GameManager>.Instance.FindQuest(name);
            if (param == null)
            {
                goto Label_0020;
            }
            param.state = st;
            this.mQuestListDirty = 1;
        Label_0020:
            return;
        }

        public void SetRankMatchInfo(int _rank, int _score, SRPG.RankMatchClass _class, int _battle_point, int _streak_win, int _wincnt, int _losecnt)
        {
            this.mRankMatchOldClass = this.mRankMatchClass;
            this.mRankMatchOldRank = this.mRankMatchRank;
            this.mRankMatchOldScore = this.mRankMatchScore;
            this.mRankMatchRank = _rank;
            this.mRankMatchScore = _score;
            this.mRankMatchClass = _class;
            this.mRankMatchBattlePoint = _battle_point;
            this.mRankMatchStreakWin = _streak_win;
            this.RankMatchWinCount = _wincnt;
            this.RankMatchLoseCount = _losecnt;
            return;
        }

        public void SetShopData(EShopType type, ShopData shop)
        {
            if (type != 10)
            {
                goto Label_0019;
            }
            this.mLimitedShops.SetShopData(shop);
            goto Label_002D;
        Label_0019:
            if (type != 9)
            {
                goto Label_002D;
            }
            this.mEventShops.SetShopData(shop);
        Label_002D:
            this.mShops[type] = shop;
            return;
        }

        private void SetSinsTobiraTrophyByAllUnit(TobiraParam.Category category, TrophyConditionTypes trophyType)
        {
            int num;
            int num2;
            TrophyObjective[] objectiveArray;
            int num3;
            TrophyObjective objective;
            num = 0;
            num2 = 0;
            goto Label_0028;
        Label_0009:
            if (this.Units[num2].CheckTobiraIsUnlocked(category) == null)
            {
                goto Label_0024;
            }
            num += 1;
        Label_0024:
            num2 += 1;
        Label_0028:
            if (num2 < this.Units.Count)
            {
                goto Label_0009;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(trophyType);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_0073;
        Label_0050:
            objective = objectiveArray[num3];
            if (string.IsNullOrEmpty(objective.sval_base) == null)
            {
                goto Label_006F;
            }
            this.SetTrophyCounter(objective, num);
        Label_006F:
            num3 -= 1;
        Label_0073:
            if (num3 >= 0)
            {
                goto Label_0050;
            }
            return;
        }

        public void SetTowerFloorResetCoin(ReqTowerFloorReset.Json_Response result)
        {
            if (result != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mFreeCoin = result.coin.free;
            this.mPaidCoin = result.coin.paid;
            this.mComCoin = result.coin.com;
            return;
        }

        public void SetTowerMatchInfo(int floor, int key, int wincnt, bool gift)
        {
            this.mVersusTwFloor = floor;
            this.mVersusTwKey = key;
            this.mVersusTwWinCnt = wincnt;
            this.mVersusSeasonGift = gift;
            return;
        }

        public void SetTrophyCounter(TrophyObjective obj, int value)
        {
            this.SetTrophyCounter(obj.Param, obj.index, value);
            return;
        }

        private void SetTrophyCounter(TrophyParam trophyParam, int countIndex, int value)
        {
            bool flag;
            if (this.SetTrophyCounterExec(trophyParam, countIndex, value) == null)
            {
                goto Label_0016;
            }
            this.DailyAllCompleteCheck();
        Label_0016:
            return;
        }

        private unsafe bool SetTrophyCounterExec(TrophyParam trophyParam, int countIndex, int value)
        {
            TrophyState state;
            int num;
            state = null;
            if (this.CheckTrophyCount(trophyParam, countIndex, value, &state) != null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            if (state.Count[countIndex] != value)
            {
                goto Label_0024;
            }
            return 0;
        Label_0024:
            num = state.Count[countIndex];
            state.Count[countIndex] = value;
            if (this.CheckDailyMissionDayChange(state, countIndex) != null)
            {
                goto Label_004E;
            }
            state.Count[countIndex] = num;
            return 0;
        Label_004E:
            state.IsDirty = 1;
            MonoSingleton<GameManager>.Instance.update_trophy_interval.SetSyncNow();
            if (state.IsCompleted == null)
            {
                goto Label_0071;
            }
            return 1;
        Label_0071:
            return 0;
        }

        public bool SetUnitEquipment(UnitData unit, int slotIndex)
        {
            EquipData data;
            ItemData data2;
            if (unit.CurrentJob.CheckEnableEquipSlot(slotIndex) != null)
            {
                goto Label_001D;
            }
            Debug.LogError("指定スロットに装備を装着する事はできません。");
            return 0;
        Label_001D:
            data = unit.GetRankupEquipData(unit.JobIndex, slotIndex);
            data2 = this.FindItemDataByItemID(data.ItemID);
            if (data2 == null)
            {
                goto Label_004A;
            }
            if (data2.Num > 0)
            {
                goto Label_0056;
            }
        Label_004A:
            Debug.LogError("装備アイテムを所持していません。");
            return 0;
        Label_0056:
            unit.CurrentJob.Equip(slotIndex);
            unit.CalcStatus();
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(1);
            MonoSingleton<GameManager>.Instance.RequestUpdateBadges(0x200);
            return 1;
        }

        public void SetupEventCoin()
        {
            List<ItemParam> list;
            int num;
            EventCoinData data;
            if (this.mEventCoinList.Count == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if (MonoSingleton<GameManager>.Instance.MasterParam.Items != null)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            list = MonoSingleton<GameManager>.Instance.MasterParam.Items;
            num = 0;
            goto Label_0085;
        Label_003D:
            if (list[num].type != 0x11)
            {
                goto Label_0081;
            }
            data = new EventCoinData();
            data.iname = list[num].iname;
            data.param = list[num];
            this.mEventCoinList.Add(data);
        Label_0081:
            num += 1;
        Label_0085:
            if (num < list.Count)
            {
                goto Label_003D;
            }
            return;
        }

        public void SetVersusPlacement(string key, int idx)
        {
            PlayerPrefsUtility.SetInt(key, idx, 0);
            return;
        }

        public void SetVersusRankpoint(int point)
        {
            this.mVersusPoint = point;
            return;
        }

        public void SetVersusTotalCount(VERSUS_TYPE type, int cnt)
        {
            VERSUS_TYPE versus_type;
            versus_type = type;
            switch (versus_type)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0019;

                case 2:
                    goto Label_0019;
            }
            goto Label_0027;
        Label_0019:
            this.mVersusTotalCount[type] = cnt;
        Label_0027:
            return;
        }

        public void SetVersusWinCount(VERSUS_TYPE type, int wincnt)
        {
            VERSUS_TYPE versus_type;
            versus_type = type;
            switch (versus_type)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0019;

                case 2:
                    goto Label_0019;
            }
            goto Label_0027;
        Label_0019:
            this.mVersusWinCount[type] = wincnt;
        Label_0027:
            return;
        }

        public void SetWishList(string iname, int priority)
        {
            this.FriendPresentWishList.Set(iname, priority);
            return;
        }

        public void SubAbilityRankUpCount(int value)
        {
            this.mAbilityRankUpCount.SubValue(value);
            return;
        }

        public void SubCaveStamina(int value)
        {
            this.mCaveStamina.SubValue(value);
            return;
        }

        public void SubStamina(int value)
        {
            this.mStamina.SubValue(value);
            return;
        }

        private void TrophyAllQuestTypeCompleteCount(QuestParam quest)
        {
            GameManager manager;
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            int num3;
            int num4;
            manager = MonoSingleton<GameManager>.Instance;
            objectiveArray = manager.GetTrophiesOfType(0x65);
            if (quest != null)
            {
                goto Label_0076;
            }
            num = ((int) objectiveArray.Length) - 1;
            goto Label_006A;
        Label_0020:
            objective = objectiveArray[num];
            num2 = 0;
            num3 = 0;
            goto Label_004E;
        Label_002F:
            if (manager.Quests[num3].IsMissionCompleteALL() == null)
            {
                goto Label_0048;
            }
            num2 += 1;
        Label_0048:
            num3 += 1;
        Label_004E:
            if (num3 < ((int) manager.Quests.Length))
            {
                goto Label_002F;
            }
            this.SetTrophyCounter(objective, num2);
            num -= 1;
        Label_006A:
            if (num >= 0)
            {
                goto Label_0020;
            }
            goto Label_009B;
        Label_0076:
            num4 = ((int) objectiveArray.Length) - 1;
            goto Label_0093;
        Label_0082:
            this.AddTrophyCounter(objectiveArray[num4], 1);
            num4 -= 1;
        Label_0093:
            if (num4 >= 0)
            {
                goto Label_0082;
            }
        Label_009B:
            return;
        }

        public void TrophyUpdateProgress()
        {
            MonoSingleton<GameManager>.Instance.Player.UpdateUnitTrophyStates(1);
            MonoSingleton<GameManager>.Instance.Player.UpdatePlayerTrophyStates();
            MonoSingleton<GameManager>.Instance.Player.UpdateArenaRankTrophyStates(-1, -1);
            MonoSingleton<GameManager>.Instance.Player.UpdateArtifactTrophyStates();
            MonoSingleton<GameManager>.Instance.Player.UpdateTobiraTrophyStates();
            MonoSingleton<GameManager>.Instance.Player.UpdateCompleteAllQuestCountTrophy(null);
            MonoSingleton<GameManager>.Instance.Player.CheckAllCompleteMissionTrophy();
            return;
        }

        public void UpdateAbilityRankUpCount()
        {
            this.mAbilityRankUpCount.Update();
            return;
        }

        public unsafe void UpdateAchievementTrophyStates()
        {
            List<AchievementParam> list;
            int num;
            AchievementParam param;
            List<TrophyState> list2;
            if (this.mTrophyStatesInameDict != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            list = GameCenterManager.GetAchievementData();
            if (list == null)
            {
                goto Label_0024;
            }
            if (list.Count >= 1)
            {
                goto Label_0025;
            }
        Label_0024:
            return;
        Label_0025:
            num = 0;
            goto Label_0067;
        Label_002C:
            param = list[num];
            if (this.mTrophyStatesInameDict.TryGetValue(param.iname, &list2) == null)
            {
                goto Label_0063;
            }
            if (list2[0].IsCompleted == null)
            {
                goto Label_0063;
            }
            GameCenterManager.SendAchievementProgress(param);
        Label_0063:
            num += 1;
        Label_0067:
            if (num < list.Count)
            {
                goto Label_002C;
            }
            return;
        }

        public void UpdateArenaRankTrophyStates(int currentRank, int bestRank)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyObjective objective2;
            objectiveArray = null;
            if (currentRank != -1)
            {
                goto Label_0011;
            }
            currentRank = this.ArenaRank;
        Label_0011:
            if (bestRank != -1)
            {
                goto Label_0020;
            }
            bestRank = this.ArenaRankBest;
        Label_0020:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x3a);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0065;
        Label_0038:
            objective = objectiveArray[num];
            if (currentRank == objective.ival)
            {
                goto Label_0054;
            }
            if (bestRank != objective.ival)
            {
                goto Label_0061;
            }
        Label_0054:
            this.SetTrophyCounter(objective, objective.ival);
        Label_0061:
            num -= 1;
        Label_0065:
            if (num >= 0)
            {
                goto Label_0038;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x3b);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_00A3;
        Label_0084:
            objective2 = objectiveArray[num2];
            if (bestRank > objective2.ival)
            {
                goto Label_009F;
            }
            this.SetTrophyCounter(objective2, bestRank);
        Label_009F:
            num2 -= 1;
        Label_00A3:
            if (num2 >= 0)
            {
                goto Label_0084;
            }
            return;
        }

        public unsafe void UpdateArtifactOwner()
        {
            int num;
            int num2;
            int num3;
            int num4;
            ArtifactData data;
            UnitData data2;
            JobData data3;
            int num5;
            int num6;
            num = 0;
            goto Label_00CA;
        Label_0007:
            if (this.mUnits[num].Jobs != null)
            {
                goto Label_0022;
            }
            goto Label_00C6;
        Label_0022:
            num2 = 0;
            goto Label_00AD;
        Label_0029:
            if (this.mUnits[num].Jobs[num2] == null)
            {
                goto Label_00A9;
            }
            if (this.mUnits[num].Jobs[num2].ArtifactDatas != null)
            {
                goto Label_0063;
            }
            goto Label_00A9;
        Label_0063:
            num3 = 0;
            goto Label_0089;
        Label_006A:
            this.mUnits[num].Jobs[num2].ArtifactDatas[num3] = null;
            num3 += 1;
        Label_0089:
            if (num3 < ((int) this.mUnits[num].Jobs[num2].ArtifactDatas.Length))
            {
                goto Label_006A;
            }
        Label_00A9:
            num2 += 1;
        Label_00AD:
            if (num2 < ((int) this.mUnits[num].Jobs.Length))
            {
                goto Label_0029;
            }
        Label_00C6:
            num += 1;
        Label_00CA:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            num4 = 0;
            goto Label_01BA;
        Label_00E2:
            data = this.mArtifacts[num4];
            if (data == null)
            {
                goto Label_01B6;
            }
            if (data.UniqueID != null)
            {
                goto Label_010D;
            }
            goto Label_01B6;
        Label_010D:
            data2 = null;
            data3 = null;
            if (this.FindOwner(data, &data2, &data3) != null)
            {
                goto Label_0129;
            }
            goto Label_01B6;
        Label_0129:
            num5 = Array.IndexOf<JobData>(data2.Jobs, data3);
            if (num5 == -1)
            {
                goto Label_01B6;
            }
            num6 = 0;
            goto Label_01A6;
        Label_0149:
            if (data.UniqueID != data3.Artifacts[num6])
            {
                goto Label_01A0;
            }
            data2.SetEquipArtifactData(num5, num6, data, data2.JobIndex == num5);
            if (data2.JobIndex == num5)
            {
                goto Label_01B6;
            }
            data2.UpdateArtifact(data2.JobIndex, 1, 0);
            goto Label_01B6;
        Label_01A0:
            num6 += 1;
        Label_01A6:
            if (num6 < ((int) data3.Artifacts.Length))
            {
                goto Label_0149;
            }
        Label_01B6:
            num4 += 1;
        Label_01BA:
            if (num4 < this.mArtifacts.Count)
            {
                goto Label_00E2;
            }
            return;
        }

        public void UpdateArtifactTrophyStates()
        {
            int num;
            Dictionary<string, ArtifactData> dictionary;
            int num2;
            ArtifactData data;
            ArtifactData data2;
            TrophyObjective[] objectiveArray;
            int num3;
            if (this.mArtifacts.Count >= 1)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            num = 1;
            dictionary = new Dictionary<string, ArtifactData>();
            num2 = 0;
            goto Label_00CD;
        Label_0021:
            data = this.mArtifacts[num2];
            if (data != null)
            {
                goto Label_0039;
            }
            goto Label_00C9;
        Label_0039:
            num = Mathf.Max(num, data.Lv);
            if (data.ArtifactParam != null)
            {
                goto Label_005B;
            }
            goto Label_00C9;
        Label_005B:
            if (dictionary.ContainsKey(data.ArtifactParam.iname) != null)
            {
                goto Label_0088;
            }
            dictionary.Add(data.ArtifactParam.iname, data);
            goto Label_00C9;
        Label_0088:
            data2 = dictionary[data.ArtifactParam.iname];
            if (data2.Lv >= data.Lv)
            {
                goto Label_00C9;
            }
            dictionary[data.ArtifactParam.iname] = data;
        Label_00C9:
            num2 += 1;
        Label_00CD:
            if (num2 < this.mArtifacts.Count)
            {
                goto Label_0021;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x39);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_015E;
        Label_00F9:
            if (string.IsNullOrEmpty(objectiveArray[num3].sval_base) == null)
            {
                goto Label_011E;
            }
            this.SetTrophyCounter(objectiveArray[num3], num);
            goto Label_0158;
        Label_011E:
            if (dictionary.ContainsKey(objectiveArray[num3].sval_base) == null)
            {
                goto Label_0158;
            }
            this.SetTrophyCounter(objectiveArray[num3], dictionary[objectiveArray[num3].sval_base].Lv);
        Label_0158:
            num3 -= 1;
        Label_015E:
            if (num3 >= 0)
            {
                goto Label_00F9;
            }
            return;
        }

        public void UpdateCardDailyMission()
        {
            DateTime time;
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            TrophyState state;
            if (this.mVipExpiredAt != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if ((TimeManager.FromUnixTime(this.mVipExpiredAt) < TimeManager.ServerTime) == null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(14);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0082;
        Label_004B:
            objective = objectiveArray[num];
            state = this.GetTrophyCounter(objective.Param, 0);
            if (state == null)
            {
                goto Label_007E;
            }
            if (state.IsCompleted == null)
            {
                goto Label_0076;
            }
            goto Label_007E;
        Label_0076:
            this.AddTrophyCounter(objective, 1);
        Label_007E:
            num -= 1;
        Label_0082:
            if (num >= 0)
            {
                goto Label_004B;
            }
            return;
        }

        public void UpdateCaveStamina()
        {
            this.mCaveStamina.Update();
            return;
        }

        public void UpdateChallengeArenaTimer()
        {
            this.mChallengeArenaTimer.Update();
            return;
        }

        public void UpdateClearOrdealTrophy(BattleCore.Record record, QuestTypes questType, string questIname)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            if (record.result == 1)
            {
                goto Label_0015;
            }
            if (questType == 15)
            {
                goto Label_0015;
            }
            return;
        Label_0015:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x69);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0082;
        Label_002D:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_005F;
            }
            if ((objective.sval_base == questIname) == null)
            {
                goto Label_007E;
            }
            this.AddTrophyCounter(objective, 1);
            goto Label_007E;
        Label_005F:
            DebugUtility.LogError("レコードミッション「" + objective.Param.Name + "」はクエストが指定されていません。");
        Label_007E:
            num -= 1;
        Label_0082:
            if (num >= 0)
            {
                goto Label_002D;
            }
            return;
        }

        public void UpdateCompleteAllQuestCountTrophy(QuestParam questParam)
        {
            this.UpdateCompleteMissionCount(0x66, questParam);
            this.UpdateCompleteMissionCount(0x67, questParam);
            this.UpdateCompleteMissionCount(0x68, questParam);
            this.UpdateCompleteMissionCount(0x65, questParam);
            return;
        }

        private void UpdateCompleteMissionCount(TrophyConditionTypes type, QuestParam quest)
        {
            QuestTypes types;
            GameManager manager;
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            int num2;
            TrophyObjective[] objectiveArray2;
            int num3;
            int num4;
            TrophyObjective objective2;
            int num5;
            int num6;
            int num7;
            TrophyConditionTypes types2;
            types = 0x7f;
            types2 = type;
            switch ((types2 - 0x65))
            {
                case 0:
                    goto Label_003B;

                case 1:
                    goto Label_0025;

                case 2:
                    goto Label_002C;

                case 3:
                    goto Label_0033;
            }
            goto Label_0043;
        Label_0025:
            types = 0;
            goto Label_004E;
        Label_002C:
            types = 5;
            goto Label_004E;
        Label_0033:
            types = 15;
            goto Label_004E;
        Label_003B:
            this.TrophyAllQuestTypeCompleteCount(quest);
            return;
        Label_0043:
            DebugUtility.LogError("指定できないミッションが設定されています。");
            return;
        Label_004E:
            manager = MonoSingleton<GameManager>.Instance;
            if (quest == null)
            {
                goto Label_011C;
            }
            objectiveArray = manager.GetTrophiesOfType(type);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0110;
        Label_006D:
            objective = objectiveArray[num];
            if (types == quest.type)
            {
                goto Label_0083;
            }
            goto Label_010C;
        Label_0083:
            if (objective.sval == null)
            {
                goto Label_0103;
            }
            if (objective.sval.Count <= 0)
            {
                goto Label_0103;
            }
            num2 = 0;
            goto Label_00EB;
        Label_00A9:
            if (quest.Chapter == null)
            {
                goto Label_00E5;
            }
            if ((objective.sval[num2] == quest.Chapter.iname) == null)
            {
                goto Label_00E5;
            }
            this.AddTrophyCounter(objective, 1);
            goto Label_00FE;
        Label_00E5:
            num2 += 1;
        Label_00EB:
            if (num2 < objective.sval.Count)
            {
                goto Label_00A9;
            }
        Label_00FE:
            goto Label_010C;
        Label_0103:
            this.AddTrophyCounter(objective, 1);
        Label_010C:
            num -= 1;
        Label_0110:
            if (num >= 0)
            {
                goto Label_006D;
            }
            goto Label_0288;
        Label_011C:
            objectiveArray2 = manager.GetTrophiesOfType(type);
            num3 = ((int) objectiveArray2.Length) - 1;
            goto Label_0280;
        Label_0132:
            num4 = 0;
            objective2 = objectiveArray2[num3];
            if (objective2.sval == null)
            {
                goto Label_021C;
            }
            if (objective2.sval.Count <= 0)
            {
                goto Label_021C;
            }
            num5 = 0;
            goto Label_0208;
        Label_0162:
            if (types == manager.Quests[num5].type)
            {
                goto Label_017B;
            }
            goto Label_0202;
        Label_017B:
            if (manager.Quests[num5].IsMissionCompleteALL() != null)
            {
                goto Label_0193;
            }
            goto Label_0202;
        Label_0193:
            if (manager.Quests[num5].Chapter != null)
            {
                goto Label_01AB;
            }
            goto Label_0202;
        Label_01AB:
            num6 = 0;
            goto Label_01EF;
        Label_01B3:
            if ((objective2.sval[num6] == manager.Quests[num5].Chapter.iname) == null)
            {
                goto Label_01E9;
            }
            num4 += 1;
            goto Label_0202;
        Label_01E9:
            num6 += 1;
        Label_01EF:
            if (num6 < objective2.sval.Count)
            {
                goto Label_01B3;
            }
        Label_0202:
            num5 += 1;
        Label_0208:
            if (num5 < ((int) manager.Quests.Length))
            {
                goto Label_0162;
            }
            goto Label_0270;
        Label_021C:
            num7 = 0;
            goto Label_0261;
        Label_0224:
            if (manager.Quests[num7].type == types)
            {
                goto Label_023D;
            }
            goto Label_025B;
        Label_023D:
            if (manager.Quests[num7].IsMissionCompleteALL() != null)
            {
                goto Label_0255;
            }
            goto Label_025B;
        Label_0255:
            num4 += 1;
        Label_025B:
            num7 += 1;
        Label_0261:
            if (num7 < ((int) manager.Quests.Length))
            {
                goto Label_0224;
            }
        Label_0270:
            this.SetTrophyCounter(objective2, num4);
            num3 -= 1;
        Label_0280:
            if (num3 >= 0)
            {
                goto Label_0132;
            }
        Label_0288:
            return;
        }

        public void UpdateConceptCardLevelupTrophy(string conceptCardID, int beforeLevel, int currentLevel)
        {
            TrophyObjective[] objectiveArray;
            int num;
            int num2;
            TrophyObjective objective;
            int num3;
            TrophyObjective objective2;
            TrophyState state;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x53);
            num = currentLevel - beforeLevel;
            if (num < 1)
            {
                goto Label_003A;
            }
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_0033;
        Label_0023:
            objective = objectiveArray[num2];
            this.AddTrophyCounter(objective, num);
            num2 -= 1;
        Label_0033:
            if (num2 >= 0)
            {
                goto Label_0023;
            }
        Label_003A:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x54);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_00B8;
        Label_0053:
            objective2 = objectiveArray[num3];
            if (string.Equals(objective2.sval_base, conceptCardID) == null)
            {
                goto Label_00B2;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(objective2.Param, 0);
            if (state == null)
            {
                goto Label_00B2;
            }
            if (((int) state.Count.Length) <= 0)
            {
                goto Label_00B2;
            }
            if (state.Count[0] > currentLevel)
            {
                goto Label_00B2;
            }
            this.SetTrophyCounter(objective2, currentLevel);
        Label_00B2:
            num3 -= 1;
        Label_00B8:
            if (num3 >= 0)
            {
                goto Label_0053;
            }
            return;
        }

        public void UpdateConceptCardLimitBreakTrophy(string conceptCardID, int beforeLimitBreak, int currentLimitBreak)
        {
            int num;
            TrophyObjective[] objectiveArray;
            int num2;
            TrophyObjective objective;
            int num3;
            TrophyObjective objective2;
            TrophyState state;
            if (currentLimitBreak > 0)
            {
                goto Label_0008;
            }
            return;
        Label_0008:
            num = currentLimitBreak - beforeLimitBreak;
            if (num < 1)
            {
                goto Label_0042;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x55);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_003B;
        Label_002B:
            objective = objectiveArray[num2];
            this.AddTrophyCounter(objective, num);
            num2 -= 1;
        Label_003B:
            if (num2 >= 0)
            {
                goto Label_002B;
            }
        Label_0042:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x56);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_00D6;
        Label_005B:
            objective2 = objectiveArray[num3];
            if (string.IsNullOrEmpty(objective2.sval_base) == null)
            {
                goto Label_0077;
            }
            goto Label_00D0;
        Label_0077:
            if (string.Equals(objective2.sval_base, conceptCardID) == null)
            {
                goto Label_00D0;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(objective2.Param, 0);
            if (state == null)
            {
                goto Label_00D0;
            }
            if (((int) state.Count.Length) <= 0)
            {
                goto Label_00D0;
            }
            if (state.Count[0] > currentLimitBreak)
            {
                goto Label_00D0;
            }
            this.SetTrophyCounter(objective2, currentLimitBreak);
        Label_00D0:
            num3 -= 1;
        Label_00D6:
            if (num3 >= 0)
            {
                goto Label_005B;
            }
            return;
        }

        public void UpdateConceptCardNum()
        {
            int num;
            string str;
            Dictionary<string, int> dictionary;
            string str2;
            int num2;
            this.mConceptCardNum.Clear();
            num = 0;
            goto Label_006D;
        Label_0012:
            str = this.mConceptCards[num].Param.iname;
            if (this.mConceptCardNum.ContainsKey(str) == null)
            {
                goto Label_005C;
            }
            num2 = dictionary[str2];
            (dictionary = this.mConceptCardNum)[str2 = str] = num2 + 1;
            goto Label_0069;
        Label_005C:
            this.mConceptCardNum.Add(str, 1);
        Label_0069:
            num += 1;
        Label_006D:
            if (num < this.mConceptCards.Count)
            {
                goto Label_0012;
            }
            return;
        }

        public void UpdateConceptCardNum(string[] inames)
        {
            int num;
            string str;
            Dictionary<string, int> dictionary;
            string str2;
            int num2;
            this.mConceptCardNum.Clear();
            num = 0;
            goto Label_005A;
        Label_0012:
            str = inames[num];
            if (this.mConceptCardNum.ContainsKey(str) == null)
            {
                goto Label_0049;
            }
            num2 = dictionary[str2];
            (dictionary = this.mConceptCardNum)[str2 = str] = num2 + 1;
            goto Label_0056;
        Label_0049:
            this.mConceptCardNum.Add(str, 1);
        Label_0056:
            num += 1;
        Label_005A:
            if (num < ((int) inames.Length))
            {
                goto Label_0012;
            }
            return;
        }

        public void UpdateConceptCardTrophyAll()
        {
            if (this.ConceptCards != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            MonoSingleton<GameManager>.Instance.Player.CheckAllConceptCardLevelupTrophy();
            MonoSingleton<GameManager>.Instance.Player.CheckAllConceptCardLimitBreakTrophy();
            MonoSingleton<GameManager>.Instance.Player.CheckAllConceptCardTrustUpTrophy();
            MonoSingleton<GameManager>.Instance.Player.CheckAllConceptCardTrustMaxTrophy();
            return;
        }

        public void UpdateConceptCardTrustMaxTrophy(string conceptCardID, int currentTrust)
        {
            TrophyObjective[] objectiveArray;
            int num;
            int num2;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x59);
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
            if (currentTrust < num)
            {
                goto Label_0089;
            }
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_0082;
        Label_0039:
            objective = objectiveArray[num2];
            if (string.IsNullOrEmpty(objective.sval_base) == null)
            {
                goto Label_0055;
            }
            this.AddTrophyCounter(objective, 1);
        Label_0055:
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_007E;
            }
            if (string.Equals(objective.sval_base, conceptCardID) == null)
            {
                goto Label_007E;
            }
            this.AddTrophyCounter(objective, 1);
        Label_007E:
            num2 -= 1;
        Label_0082:
            if (num2 >= 0)
            {
                goto Label_0039;
            }
        Label_0089:
            return;
        }

        public void UpdateConceptCardTrustUpTrophy(string conceptCardID, int beforeTrust, int currentTrust)
        {
            TrophyObjective[] objectiveArray;
            int num;
            int num2;
            TrophyObjective objective;
            int num3;
            TrophyObjective objective2;
            TrophyState state;
            if (currentTrust != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x57);
            num = currentTrust - beforeTrust;
            if (num < 1)
            {
                goto Label_0041;
            }
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_003A;
        Label_002A:
            objective = objectiveArray[num2];
            this.AddTrophyCounter(objective, num);
            num2 -= 1;
        Label_003A:
            if (num2 >= 0)
            {
                goto Label_002A;
            }
        Label_0041:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x58);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_00D5;
        Label_005A:
            objective2 = objectiveArray[num3];
            if (string.IsNullOrEmpty(objective2.sval_base) == null)
            {
                goto Label_0076;
            }
            goto Label_00CF;
        Label_0076:
            if (string.Equals(objective2.sval_base, conceptCardID) == null)
            {
                goto Label_00CF;
            }
            state = MonoSingleton<GameManager>.Instance.Player.GetTrophyCounter(objective2.Param, 0);
            if (state == null)
            {
                goto Label_00CF;
            }
            if (((int) state.Count.Length) <= 0)
            {
                goto Label_00CF;
            }
            if (state.Count[0] > currentTrust)
            {
                goto Label_00CF;
            }
            this.SetTrophyCounter(objective2, currentTrust);
        Label_00CF:
            num3 -= 1;
        Label_00D5:
            if (num3 >= 0)
            {
                goto Label_005A;
            }
            return;
        }

        public void UpdateEventCoin()
        {
            ItemData data;
            <UpdateEventCoin>c__AnonStorey242 storey;
            this.SetupEventCoin();
            storey = new <UpdateEventCoin>c__AnonStorey242();
            storey.<>f__this = this;
            storey.i = 0;
            goto Label_006B;
        Label_001F:
            data = MonoSingleton<GameManager>.Instance.Player.Items.Find(new Predicate<ItemData>(storey.<>m__14A));
            if (data == null)
            {
                goto Label_005D;
            }
            this.mEventCoinList[storey.i].have = data;
        Label_005D:
            storey.i += 1;
        Label_006B:
            if (storey.i < this.mEventCoinList.Count)
            {
                goto Label_001F;
            }
            return;
        }

        public void UpdateInventory()
        {
            int num;
            string str;
            ItemData data;
            num = 0;
            goto Label_007A;
        Label_0007:
            this.mInventory[num] = null;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PLAYERDATA_INVENTORY + ((int) num)) != null)
            {
                goto Label_002F;
            }
            goto Label_0076;
        Label_002F:
            str = PlayerPrefsUtility.GetString(PlayerPrefsUtility.PLAYERDATA_INVENTORY + ((int) num), string.Empty);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_005A;
            }
            goto Label_0076;
        Label_005A:
            data = this.FindItemDataByItemID(str);
            if (data != null)
            {
                goto Label_006D;
            }
            goto Label_0076;
        Label_006D:
            this.mInventory[num] = data;
        Label_0076:
            num += 1;
        Label_007A:
            if (num < ((int) this.mInventory.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void UpdatePlayerTrophyStates()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = null;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(4);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_003A;
        Label_0019:
            objective = objectiveArray[num];
            if (this.Lv < objective.ival)
            {
                goto Label_0036;
            }
            this.AddTrophyCounter(objective, 1);
        Label_0036:
            num -= 1;
        Label_003A:
            if (num >= 0)
            {
                goto Label_0019;
            }
            return;
        }

        public void UpdateSendFriendPresentTrophy()
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x63);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0028;
        Label_0018:
            objective = objectiveArray[num];
            this.AddTrophyCounter(objective, 1);
            num -= 1;
        Label_0028:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        private void UpdateSinsTobiraTrophy(UnitData unitData)
        {
            List<TobiraParam.Category> list;
            TrophyConditionTypes types;
            int num;
            TrophyObjective[] objectiveArray;
            int num2;
            TrophyObjective objective;
            TobiraParam.Category category;
            if (unitData.IsUnlockTobira != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            list = this.GetUnlockTobiraCategorys(unitData);
            types = 0x5c;
            num = 0;
            goto Label_00E3;
        Label_001E:
            category = list[num];
            switch ((category - 1))
            {
                case 0:
                    goto Label_0051;

                case 1:
                    goto Label_0059;

                case 2:
                    goto Label_0061;

                case 3:
                    goto Label_0069;

                case 4:
                    goto Label_0071;

                case 5:
                    goto Label_0079;

                case 6:
                    goto Label_0081;
            }
            goto Label_0089;
        Label_0051:
            types = 0x5c;
            goto Label_008E;
        Label_0059:
            types = 0x5d;
            goto Label_008E;
        Label_0061:
            types = 0x5e;
            goto Label_008E;
        Label_0069:
            types = 0x61;
            goto Label_008E;
        Label_0071:
            types = 0x60;
            goto Label_008E;
        Label_0079:
            types = 0x5f;
            goto Label_008E;
        Label_0081:
            types = 0x62;
            goto Label_008E;
        Label_0089:
            goto Label_00DF;
        Label_008E:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(types);
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_00D7;
        Label_00A6:
            objective = objectiveArray[num2];
            if (string.Equals(objective.sval_base, unitData.UnitParam.iname) == null)
            {
                goto Label_00D1;
            }
            this.SetTrophyCounter(objective, 1);
        Label_00D1:
            num2 -= 1;
        Label_00D7:
            if (num2 >= 0)
            {
                goto Label_00A6;
            }
        Label_00DF:
            num += 1;
        Label_00E3:
            if (num < list.Count)
            {
                goto Label_001E;
            }
            return;
        }

        public void UpdateStamina()
        {
            this.mStamina.Update();
            return;
        }

        public unsafe void UpdateStaminaDailyMission()
        {
            int num;
            TrophyObjective[] objectiveArray;
            List<int> list;
            int num2;
            TrophyObjective objective;
            TrophyState state;
            int num3;
            int num4;
            int num5;
            List<int>.Enumerator enumerator;
            DateTime time;
            if (this.mUpdateInterval.PlayCheckUpdate() != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            num = &TimeManager.ServerTime.Hour;
            if (num != this.mPrevCheckHour)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            this.mUpdateInterval.SetUpdateInterval();
            this.mPrevCheckHour = num;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(13);
            list = MonoSingleton<WatchManager>.Instance.GetMealHours();
            num2 = ((int) objectiveArray.Length) - 1;
            goto Label_0125;
        Label_0062:
            objective = objectiveArray[num2];
            state = this.GetTrophyCounter(objective.Param, 0);
            if (state == null)
            {
                goto Label_0121;
            }
            if (state.IsCompleted == null)
            {
                goto Label_008F;
            }
            goto Label_0121;
        Label_008F:
            num3 = int.Parse(objective.sval_base.Substring(0, 2));
            num4 = int.Parse(objective.sval_base.Substring(3, 2));
            if (num3 > num)
            {
                goto Label_00D2;
            }
            if (num >= num4)
            {
                goto Label_00D2;
            }
            this.AddTrophyCounter(objective, 1);
        Label_00D2:
            enumerator = list.GetEnumerator();
        Label_00DA:
            try
            {
                goto Label_0103;
            Label_00DF:
                num5 = &enumerator.Current;
                if (num3 > num5)
                {
                    goto Label_0103;
                }
                if (num5 >= num4)
                {
                    goto Label_0103;
                }
                this.AddTrophyCounter(objective, 1);
            Label_0103:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00DF;
                }
                goto Label_0121;
            }
            finally
            {
            Label_0114:
                ((List<int>.Enumerator) enumerator).Dispose();
            }
        Label_0121:
            num2 -= 1;
        Label_0125:
            if (num2 >= 0)
            {
                goto Label_0062;
            }
            return;
        }

        public void UpdateTobiraTrophyStates()
        {
            int num;
            this.UpdateUnlockTobiraUnitCountTrophy();
            num = 0;
            goto Label_0035;
        Label_000D:
            this.UpdateUnlockTobiraUnitTrophy(this.Units[num]);
            this.UpdateSinsTobiraTrophy(this.Units[num]);
            num += 1;
        Label_0035:
            if (num < this.Units.Count)
            {
                goto Label_000D;
            }
            this.CheckAllSinsTobiraNonTargetTrophy();
            return;
        }

        public void UpdateTowerTrophyStates()
        {
            this.OnTowerScore(0);
            return;
        }

        private unsafe void UpdateTrophyState(TrophyState st, int currentYMD)
        {
            int num;
            DateTime time;
            DateTime time2;
            int num2;
            int num3;
            TimeSpan span;
            if (st.Param.IsDaily != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            num = st.StartYMD;
            time = SRPG_Extensions.FromYMD(currentYMD);
            time2 = SRPG_Extensions.FromYMD(num);
            num2 = Math.Abs(&&time.Subtract(time2).Days);
            if (st.IsEnded == null)
            {
                goto Label_005B;
            }
            if (num2 < 1)
            {
                goto Label_00E5;
            }
            this.ClearTrophyCounter(st);
            goto Label_00E5;
        Label_005B:
            if (st.IsCompleted != null)
            {
                goto Label_0079;
            }
            if (num2 < 1)
            {
                goto Label_00E5;
            }
            this.ClearTrophyCounter(st);
            goto Label_00E5;
        Label_0079:
            if (num2 < 2)
            {
                goto Label_008C;
            }
            this.ClearTrophyCounter(st);
            goto Label_00E5;
        Label_008C:
            if (num2 < 1)
            {
                goto Label_00E5;
            }
            num3 = 0;
            goto Label_00D1;
        Label_009B:
            if (st.Param.Objectives[num3].type != 13)
            {
                goto Label_00CB;
            }
            goto Label_00BF;
            goto Label_00CB;
        Label_00BF:
            this.ClearTrophyCounter(st);
            goto Label_00E5;
        Label_00CB:
            num3 += 1;
        Label_00D1:
            if (num3 < ((int) st.Param.Objectives.Length))
            {
                goto Label_009B;
            }
        Label_00E5:
            return;
        }

        public void UpdateTrophyStates()
        {
            DateTime time;
            int num;
            TrophyState[] stateArray;
            int num2;
            num = SRPG_Extensions.ToYMD(TimeManager.ServerTime);
            stateArray = this.mTrophyStates.ToArray();
            num2 = 0;
            goto Label_0036;
        Label_0020:
            if (stateArray[num2] == null)
            {
                goto Label_0032;
            }
            this.UpdateTrophyState(stateArray[num2], num);
        Label_0032:
            num2 += 1;
        Label_0036:
            if (num2 < ((int) stateArray.Length))
            {
                goto Label_0020;
            }
            return;
        }

        public void UpdateUnitTrophyStates(bool verbose)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            MasterParam param;
            int num6;
            UnitData data;
            JobData[] dataArray;
            int num7;
            JobSetParam param2;
            JobSetParam param3;
            int num8;
            TrophyObjective[] objectiveArray;
            int num9;
            int num10;
            int num11;
            int num12;
            TrophyObjective objective;
            int num13;
            TrophyObjective objective2;
            int num14;
            UnitData data2;
            string str;
            JobData[] dataArray2;
            int num15;
            List<AbilityData> list;
            int num16;
            num = 0;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            num5 = 0;
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            num6 = 0;
            goto Label_0148;
        Label_001F:
            data = this.mUnits[num6];
            if (data == null)
            {
                goto Label_0142;
            }
            num += data.Lv;
            num2 += 1;
            dataArray = data.Jobs;
            if (dataArray == null)
            {
                goto Label_0108;
            }
            num7 = 0;
            goto Label_00FD;
        Label_005B:
            if (dataArray[num7] != null)
            {
                goto Label_006A;
            }
            goto Label_00F7;
        Label_006A:
            if (dataArray[num7].Rank < 11)
            {
                goto Label_0084;
            }
            num3 += 1;
            goto Label_0108;
        Label_0084:
            if (dataArray[num7].Rank <= 0)
            {
                goto Label_00F7;
            }
            param2 = data.UnitParam.GetJobSetFast(num7);
            if (param2 == null)
            {
                goto Label_00F7;
            }
            if (string.IsNullOrEmpty(param2.jobchange) != null)
            {
                goto Label_00F7;
            }
            param3 = param.GetJobSetParam(param2.jobchange);
            if (param3 == null)
            {
                goto Label_00F7;
            }
            if ((param3.job == dataArray[num7].JobID) == null)
            {
                goto Label_00F7;
            }
            num3 += 1;
            goto Label_0108;
        Label_00F7:
            num7 += 1;
        Label_00FD:
            if (num7 < ((int) dataArray.Length))
            {
                goto Label_005B;
            }
        Label_0108:
            if (data.UnitParam == null)
            {
                goto Label_0136;
            }
            num8 = data.Rarity - data.UnitParam.rare;
            if (num8 <= 0)
            {
                goto Label_0136;
            }
            num4 += 1;
        Label_0136:
            num5 += data.AwakeLv;
        Label_0142:
            num6 += 1;
        Label_0148:
            if (num6 < this.mUnits.Count)
            {
                goto Label_001F;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x2a);
            num9 = ((int) objectiveArray.Length) - 1;
            goto Label_0197;
        Label_0175:
            if (objectiveArray[num9].ival > num2)
            {
                goto Label_0191;
            }
            this.AddTrophyCounter(objectiveArray[num9], 1);
        Label_0191:
            num9 -= 1;
        Label_0197:
            if (num9 >= 0)
            {
                goto Label_0175;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x2b);
            num10 = ((int) objectiveArray.Length) - 1;
            goto Label_01DC;
        Label_01BA:
            if (objectiveArray[num10].ival > num3)
            {
                goto Label_01D6;
            }
            this.AddTrophyCounter(objectiveArray[num10], 1);
        Label_01D6:
            num10 -= 1;
        Label_01DC:
            if (num10 >= 0)
            {
                goto Label_01BA;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x2c);
            num11 = ((int) objectiveArray.Length) - 1;
            goto Label_0221;
        Label_01FF:
            if (objectiveArray[num11].ival > num)
            {
                goto Label_021B;
            }
            this.AddTrophyCounter(objectiveArray[num11], 1);
        Label_021B:
            num11 -= 1;
        Label_0221:
            if (num11 >= 0)
            {
                goto Label_01FF;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(30);
            num12 = ((int) objectiveArray.Length) - 1;
            goto Label_026B;
        Label_0244:
            objective = objectiveArray[num12];
            if (string.IsNullOrEmpty(objective.sval_base) == null)
            {
                goto Label_0265;
            }
            this.SetTrophyCounter(objective, num4);
        Label_0265:
            num12 -= 1;
        Label_026B:
            if (num12 >= 0)
            {
                goto Label_0244;
            }
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x1d);
            num13 = ((int) objectiveArray.Length) - 1;
            goto Label_02B6;
        Label_028E:
            objective2 = objectiveArray[num13];
            if (string.IsNullOrEmpty(objective2.sval_base) == null)
            {
                goto Label_02B0;
            }
            this.SetTrophyCounter(objective2, num5);
        Label_02B0:
            num13 -= 1;
        Label_02B6:
            if (num13 >= 0)
            {
                goto Label_028E;
            }
            if (verbose == null)
            {
                goto Label_03F9;
            }
            num14 = 0;
            goto Label_03E7;
        Label_02CC:
            data2 = this.mUnits[num14];
            if (data2 == null)
            {
                goto Label_02EE;
            }
            if (data2.UnitParam != null)
            {
                goto Label_02EF;
            }
        Label_02EE:
            return;
        Label_02EF:
            str = data2.UnitParam.iname;
            this.OnUnitLevelChange(str, 0, data2.Lv, 1);
            dataArray2 = data2.Jobs;
            if (dataArray2 == null)
            {
                goto Label_0355;
            }
            num15 = 0;
            goto Label_034A;
        Label_0326:
            this.OnJobLevelChange(str, dataArray2[num15].JobID, dataArray2[num15].Rank, 1, 1);
            num15 += 1;
        Label_034A:
            if (num15 < ((int) dataArray2.Length))
            {
                goto Label_0326;
            }
        Label_0355:
            list = data2.LearnAbilitys;
            num16 = 0;
            goto Label_0391;
        Label_0366:
            this.OnAbilityPowerUp(str, list[num16].AbilityID, list[num16].Rank, 1);
            num16 += 1;
        Label_0391:
            if (num16 < list.Count)
            {
                goto Label_0366;
            }
            if (data2.Rarity <= data2.UnitParam.rare)
            {
                goto Label_03D2;
            }
            this.OnEvolutionCheck(str, data2.Rarity, data2.UnitParam.rare);
        Label_03D2:
            this.OnLimitBreakCheck(str, data2.AwakeLv);
            num14 += 1;
        Label_03E7:
            if (num14 < this.mUnits.Count)
            {
                goto Label_02CC;
            }
        Label_03F9:
            return;
        }

        public void UpdateUnlocks()
        {
            UnlockTargets targets;
            UnlockParam[] paramArray;
            int num;
            UnlockParam param;
            targets = 0;
            this.mUnlocks = 0;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.Unlocks;
            num = 0;
            goto Label_008A;
        Label_0025:
            param = paramArray[num];
            if (param != null)
            {
                goto Label_0034;
            }
            goto Label_0086;
        Label_0034:
            targets |= param.UnlockTarget;
            if (param.PlayerLevel <= this.Lv)
            {
                goto Label_0053;
            }
            goto Label_0086;
        Label_0053:
            if (param.VipRank <= this.VipRank)
            {
                goto Label_0069;
            }
            goto Label_0086;
        Label_0069:
            this.mUnlocks |= param.UnlockTarget;
        Label_0086:
            num += 1;
        Label_008A:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0025;
            }
            this.mUnlocks |= ~targets;
            return;
        }

        private void UpdateUnlockTobiraUnitCountTrophy()
        {
            int num;
            int num2;
            TrophyObjective[] objectiveArray;
            int num3;
            TrophyObjective objective;
            if (this.Units != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            num2 = 0;
            goto Label_0033;
        Label_0015:
            if (this.Units[num2].IsUnlockTobira == null)
            {
                goto Label_002F;
            }
            num += 1;
        Label_002F:
            num2 += 1;
        Label_0033:
            if (num2 < this.Units.Count)
            {
                goto Label_0015;
            }
            if (num > 0)
            {
                goto Label_004C;
            }
            return;
        Label_004C:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(90);
            num3 = ((int) objectiveArray.Length) - 1;
            goto Label_0076;
        Label_0064:
            objective = objectiveArray[num3];
            this.SetTrophyCounter(objective, num);
            num3 -= 1;
        Label_0076:
            if (num3 >= 0)
            {
                goto Label_0064;
            }
            return;
        }

        private void UpdateUnlockTobiraUnitTrophy(UnitData unitData)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x5b);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0082;
        Label_0018:
            if (unitData.IsUnlockTobira == null)
            {
                goto Label_007E;
            }
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) == null)
            {
                goto Label_005B;
            }
            DebugUtility.LogError("トロフィー[" + objective.Param.Name + "]にはユニットが指定されていません。");
            goto Label_007E;
        Label_005B:
            if (string.Equals(objective.sval_base, unitData.UnitParam.iname) == null)
            {
                goto Label_007E;
            }
            this.SetTrophyCounter(objective, 1);
        Label_007E:
            num -= 1;
        Label_0082:
            if (num >= 0)
            {
                goto Label_0018;
            }
            return;
        }

        public void UpdateVersusTowerTrophyStates(string towerName, int currentFloor)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            objectiveArray = null;
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x3e);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0057;
        Label_001A:
            objective = objectiveArray[num];
            if (string.IsNullOrEmpty(objective.sval_base) != null)
            {
                goto Label_003F;
            }
            if (string.Equals(objective.sval_base, towerName) == null)
            {
                goto Label_0053;
            }
        Label_003F:
            if (currentFloor < objective.ival)
            {
                goto Label_0053;
            }
            this.SetTrophyCounter(objective, currentFloor);
        Label_0053:
            num -= 1;
        Label_0057:
            if (num >= 0)
            {
                goto Label_001A;
            }
            return;
        }

        public void UpdateViewNewsTrophy(string url)
        {
            TrophyObjective[] objectiveArray;
            int num;
            TrophyObjective objective;
            if (url.Contains(Network.NewsHost) != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            objectiveArray = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(0x6a);
            num = ((int) objectiveArray.Length) - 1;
            goto Label_0039;
        Label_0029:
            objective = objectiveArray[num];
            this.AddTrophyCounter(objective, 1);
            num -= 1;
        Label_0039:
            if (num >= 0)
            {
                goto Label_0029;
            }
            return;
        }

        public void UpdateVipDailyMission(int vipLv)
        {
        }

        public bool UseExpPotion(UnitData unit, ItemData item)
        {
            if (item == null)
            {
                goto Label_0029;
            }
            if (item.Param == null)
            {
                goto Label_0029;
            }
            if (item.Num <= 0)
            {
                goto Label_0029;
            }
            if (item.ItemType == 6)
            {
                goto Label_002B;
            }
        Label_0029:
            return 0;
        Label_002B:
            unit.GainExp(item.Param.value, MonoSingleton<GameManager>.Instance.Player.Lv);
            item.Used(1);
            return 1;
        }

        public List<SRPG.RankMatchMissionState> RankMatchMissionState
        {
            get
            {
                return this.mRankMatchMissionState;
            }
            set
            {
                this.mRankMatchMissionState = value;
                return;
            }
        }

        public QuestParam[] AvailableQuests
        {
            get
            {
                GameManager manager;
                int num;
                QuestParam param;
                if (this.mQuestListDirty == null)
                {
                    goto Label_0067;
                }
                this.mQuestListDirty = 0;
                this.mAvailableQuests.Clear();
                manager = MonoSingleton<GameManager>.Instance;
                num = 0;
                goto Label_0059;
            Label_002A:
                param = manager.Quests[num];
                if (param != null)
                {
                    goto Label_003E;
                }
                goto Label_0055;
            Label_003E:
                if (param.IsQuestCondition() == null)
                {
                    goto Label_0055;
                }
                this.mAvailableQuests.Add(param);
            Label_0055:
                num += 1;
            Label_0059:
                if (num < ((int) manager.Quests.Length))
                {
                    goto Label_002A;
                }
            Label_0067:
                return this.mAvailableQuests.ToArray();
            }
        }

        public string OkyakusamaCode
        {
            get
            {
                return this.mCuid;
            }
        }

        public BtlResultTypes RankMatchResult
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchResult>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchResult>k__BackingField = value;
                return;
            }
        }

        public int RankMatchTotalCount
        {
            get
            {
                return (this.RankMatchWinCount + this.RankMatchLoseCount);
            }
        }

        public int RankMatchWinCount
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchWinCount>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchWinCount>k__BackingField = value;
                return;
            }
        }

        public int RankMatchLoseCount
        {
            [CompilerGenerated]
            get
            {
                return this.<RankMatchLoseCount>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RankMatchLoseCount>k__BackingField = value;
                return;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
            set
            {
                this.mName = value;
                return;
            }
        }

        public string CUID
        {
            get
            {
                return this.mCuid;
            }
        }

        public string FUID
        {
            get
            {
                return this.mFuid;
            }
        }

        public string TUID
        {
            get
            {
                return this.mTuid;
            }
        }

        public long TuidExpiredAt
        {
            get
            {
                return this.mTuidExpiredAt;
            }
        }

        public int LoginCount
        {
            get
            {
                return this.mLoginCount;
            }
        }

        public int Lv
        {
            get
            {
                return this.mLv;
            }
        }

        public int Exp
        {
            get
            {
                return this.mExp;
            }
        }

        public int Gold
        {
            get
            {
                return this.mGold;
            }
        }

        public int Coin
        {
            get
            {
                return ((this.mFreeCoin + this.mPaidCoin) + this.mComCoin);
            }
        }

        public int FreeCoin
        {
            get
            {
                return this.mFreeCoin;
            }
        }

        public int PaidCoin
        {
            get
            {
                return this.mPaidCoin;
            }
        }

        public int ComCoin
        {
            get
            {
                return this.mComCoin;
            }
        }

        public int TourCoin
        {
            get
            {
                return this.mTourCoin;
            }
        }

        public int ArenaCoin
        {
            get
            {
                return this.mArenaCoin;
            }
        }

        public int MultiCoin
        {
            get
            {
                return this.mMultiCoin;
            }
        }

        public int AbilityPoint
        {
            get
            {
                return this.mAbilityPoint;
            }
        }

        public int PiecePoint
        {
            get
            {
                return this.mPiecePoint;
            }
        }

        public int VipRank
        {
            get
            {
                return this.mVipRank;
            }
        }

        public int VipPoint
        {
            get
            {
                return this.mVipPoint;
            }
        }

        public List<EventCoinData> EventCoinList
        {
            get
            {
                return this.mEventCoinList;
            }
        }

        public int Stamina
        {
            get
            {
                return this.mStamina.val;
            }
        }

        public int StaminaMax
        {
            get
            {
                return this.mStamina.valMax;
            }
        }

        public long StaminaRecverySec
        {
            get
            {
                return this.mStamina.interval;
            }
        }

        public long StaminaAt
        {
            get
            {
                return this.mStamina.at;
            }
        }

        public int StaminaStockCap
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.StaminaStockCap;
            }
        }

        public int CaveStamina
        {
            get
            {
                return this.mCaveStamina.val;
            }
        }

        public int CaveStaminaMax
        {
            get
            {
                return this.mCaveStamina.valMax;
            }
        }

        public long CaveStaminaRecverySec
        {
            get
            {
                return this.mCaveStamina.interval;
            }
        }

        public long CaveStaminaAt
        {
            get
            {
                return this.mCaveStamina.at;
            }
        }

        public int CaveStaminaStockCap
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CaveStaminaStockCap;
            }
        }

        public int AbilityRankUpCountNum
        {
            get
            {
                return this.mAbilityRankUpCount.val;
            }
        }

        public int AbilityRankUpCountMax
        {
            get
            {
                return this.mAbilityRankUpCount.valMax;
            }
        }

        public long AbilityRankUpCountRecverySec
        {
            get
            {
                return this.mAbilityRankUpCount.interval;
            }
        }

        public long AbilityRankUpCountAt
        {
            get
            {
                return this.mAbilityRankUpCount.at;
            }
        }

        public int ChallengeArenaNum
        {
            get
            {
                return this.mChallengeArenaNum;
            }
        }

        public int ChallengeArenaMax
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaMax;
            }
        }

        public long ChallengeArenaCoolDownSec
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeArenaCoolDownSec;
            }
        }

        public long ChallengeArenaAt
        {
            get
            {
                return this.mChallengeArenaTimer.at;
            }
        }

        public int ChallengeTourNum
        {
            get
            {
                return this.mTourNum;
            }
        }

        public int ChallengeTourMax
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeTourMax;
            }
        }

        public int ArenaRank
        {
            get
            {
                return this.mArenaRank;
            }
        }

        public int ArenaRankBest
        {
            get
            {
                return this.mBestArenaRank;
            }
        }

        public DateTime ArenaLastAt
        {
            get
            {
                return this.mArenaLastAt;
            }
        }

        public int ArenaSeed
        {
            get
            {
                return this.mArenaSeed;
            }
        }

        public int ArenaMaxActionNum
        {
            get
            {
                return this.mArenaMaxActionNum;
            }
        }

        public DateTime ArenaEndAt
        {
            get
            {
                return this.mArenaEndAt;
            }
        }

        public int ChallengeMultiNum
        {
            get
            {
                return this.mChallengeMultiNum;
            }
        }

        public int ChallengeMultiMax
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ChallengeMultiMax;
            }
        }

        public int StaminaBuyNum
        {
            get
            {
                return this.mStaminaBuyNum;
            }
        }

        public int GoldBuyNum
        {
            get
            {
                return this.mGoldBuyNum;
            }
        }

        public int UnitCap
        {
            get
            {
                return this.mUnitCap;
            }
        }

        public List<UnitData> Units
        {
            get
            {
                return this.mUnits;
            }
        }

        public int UnitNum
        {
            get
            {
                return ((this.mUnits == null) ? 0 : this.mUnits.Count);
            }
        }

        public List<ItemData> Items
        {
            get
            {
                return this.mItems;
            }
        }

        public List<ArtifactData> Artifacts
        {
            get
            {
                return this.mArtifacts;
            }
        }

        public int ArtifactNum
        {
            get
            {
                return this.mArtifacts.Count;
            }
        }

        public int ArtifactCap
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap;
            }
        }

        public List<string> Skins
        {
            get
            {
                return this.mSkins;
            }
        }

        public int FriendCap
        {
            get
            {
                PlayerParam param;
                return MonoSingleton<GameManager>.Instance.MasterParam.GetPlayerParam(this.Lv).fcap;
            }
        }

        public int FriendNum
        {
            get
            {
                return this.mFriendNum;
            }
            set
            {
                this.mFriendNum = value;
                return;
            }
        }

        public int FollowerNum
        {
            get
            {
                return this.mFollowerNum;
            }
            set
            {
                this.mFollowerNum = value;
                return;
            }
        }

        public List<string> FollowerUID
        {
            get
            {
                return this.mFollowerUID;
            }
        }

        public List<PartyData> Partys
        {
            get
            {
                return this.mPartys;
            }
        }

        public ItemData[] Inventory
        {
            get
            {
                return (this.mInventory.Clone() as ItemData[]);
            }
        }

        public int ConceptCardNum
        {
            get
            {
                return this.mConceptCards.Count;
            }
        }

        public int ConceptCardCap
        {
            get
            {
                return MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardMax;
            }
        }

        public bool UnreadMailPeriod
        {
            get
            {
                return this.mUnreadMailPeriod;
            }
            set
            {
                this.mUnreadMailPeriod = value;
                return;
            }
        }

        public bool UnreadMail
        {
            get
            {
                return this.mUnreadMail;
            }
        }

        public bool ValidGpsGift
        {
            get
            {
                return this.mValidGpsGift;
            }
            set
            {
                this.mValidGpsGift = value;
                return;
            }
        }

        public bool ValidFriendPresent
        {
            get
            {
                return this.mValidFriendPresent;
            }
            set
            {
                this.mValidFriendPresent = value;
                return;
            }
        }

        public string SelectedAward
        {
            get
            {
                return this.mSelectedAward;
            }
            set
            {
                this.mSelectedAward = value;
                return;
            }
        }

        public int VERSUS_POINT
        {
            get
            {
                return this.mVersusPoint;
            }
        }

        public int VersusFreeWinCnt
        {
            get
            {
                return this.mVersusWinCount[0];
            }
        }

        public int VersusTowerWinCnt
        {
            get
            {
                return this.mVersusWinCount[1];
            }
        }

        public int VersusFriendWinCnt
        {
            get
            {
                return this.mVersusWinCount[2];
            }
        }

        public int VersusFreeCnt
        {
            get
            {
                return this.mVersusTotalCount[0];
            }
        }

        public int VersusTowerCnt
        {
            get
            {
                return this.mVersusTotalCount[1];
            }
        }

        public int VersusFriendCnt
        {
            get
            {
                return this.mVersusTotalCount[2];
            }
        }

        public int VersusTowerFloor
        {
            get
            {
                return this.mVersusTwFloor;
            }
        }

        public int VersusTowerKey
        {
            get
            {
                return this.mVersusTwKey;
            }
        }

        public int VersusTowerWinBonus
        {
            get
            {
                return this.mVersusTwWinCnt;
            }
        }

        public bool VersusSeazonGiftReceipt
        {
            get
            {
                return this.mVersusSeasonGift;
            }
            set
            {
                this.mVersusSeasonGift = value;
                return;
            }
        }

        public int RankMatchRank
        {
            get
            {
                return this.mRankMatchRank;
            }
        }

        public int RankMatchScore
        {
            get
            {
                return this.mRankMatchScore;
            }
        }

        public SRPG.RankMatchClass RankMatchClass
        {
            get
            {
                return this.mRankMatchClass;
            }
        }

        public int RankMatchBattlePoint
        {
            get
            {
                return this.mRankMatchBattlePoint;
            }
        }

        public int RankMatchStreakWin
        {
            get
            {
                return this.mRankMatchStreakWin;
            }
        }

        public int RankMatchOldRank
        {
            get
            {
                return this.mRankMatchOldRank;
            }
        }

        public int RankMatchOldScore
        {
            get
            {
                return this.mRankMatchOldScore;
            }
        }

        public SRPG.RankMatchClass RankMatchOldClass
        {
            get
            {
                return this.mRankMatchOldClass;
            }
        }

        public bool MultiInvitaionFlag
        {
            get
            {
                return this.mMultiInvitaionFlag;
            }
        }

        public string MultiInvitaionComment
        {
            get
            {
                return this.mMultiInvitaionComment;
            }
        }

        public int FirstFriendCount
        {
            get
            {
                return this.mFirstFriendCount;
            }
            set
            {
                this.mFirstFriendCount = value;
                return;
            }
        }

        public int FirstChargeStatus
        {
            get
            {
                return this.mFirstChargeStatus;
            }
            set
            {
                this.mFirstChargeStatus = value;
                return;
            }
        }

        public long GuerrillaShopStart
        {
            get
            {
                return this.mGuerrillaShopStart;
            }
        }

        public long GuerrillaShopEnd
        {
            get
            {
                return this.mGuerrillaShopEnd;
            }
        }

        public bool IsGuerrillaShopStarted
        {
            get
            {
                return this.mIsGuerrillaShopStarted;
            }
            set
            {
                this.mIsGuerrillaShopStarted = value;
                return;
            }
        }

        public bool IsFirstLogin
        {
            get
            {
                return this.mFirstLogin;
            }
        }

        public Json_LoginBonus RecentLoginBonus
        {
            get
            {
                if (this.LoginBonus == null)
                {
                    goto Label_003A;
                }
                if (0 >= this.mLoginBonusCount)
                {
                    goto Label_003A;
                }
                if (this.mLoginBonusCount > ((int) this.LoginBonus.Length))
                {
                    goto Label_003A;
                }
                return this.LoginBonus[this.mLoginBonusCount - 1];
            Label_003A:
                return null;
            }
        }

        public Json_LoginBonusTable LoginBonus28days
        {
            get
            {
                return this.mLoginBonus28days;
            }
        }

        public Json_LoginBonus[] LoginBonus
        {
            get
            {
                return this.mLoginBonus;
            }
        }

        public int LoginBonusCount
        {
            get
            {
                return this.mLoginBonusCount;
            }
        }

        public DateTime VipExpiredAt
        {
            get
            {
                return TimeManager.FromUnixTime(this.mVipExpiredAt);
            }
        }

        public int ArenaResetCount
        {
            get
            {
                return this.mArenaResetCount;
            }
        }

        public TrophyState[] TrophyStates
        {
            get
            {
                return this.mTrophyStates.ToArray();
            }
        }

        public bool HasQueuedLoginBonus
        {
            get
            {
                return (this.mLoginBonusQueue.Count > 0);
            }
        }

        public List<ConceptCardData> ConceptCards
        {
            get
            {
                return this.mConceptCards;
            }
        }

        public List<ConceptCardMaterialData> ConceptCardExpMaterials
        {
            get
            {
                return this.mConceptCardExpMaterials;
            }
        }

        public List<ConceptCardMaterialData> ConceptCardTrustMaterials
        {
            get
            {
                return this.mConceptCardTrustMaterials;
            }
        }

        [CompilerGenerated]
        private sealed class <CheckFriend>c__AnonStorey23B
        {
            internal string fuid;

            public <CheckFriend>c__AnonStorey23B()
            {
                base..ctor();
                return;
            }

            internal bool <>m__142(FriendData p)
            {
                return (p.FUID == this.fuid);
            }
        }

        [CompilerGenerated]
        private sealed class <CreateInheritingExtraTrophy>c__AnonStorey241
        {
            internal TrophyParam param;

            public <CreateInheritingExtraTrophy>c__AnonStorey241()
            {
                base..ctor();
                return;
            }

            internal bool <>m__148(JSON_TrophyProgress x)
            {
                return (x.iname == this.param.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <DeleteTrophies>c__AnonStorey23E
        {
            internal JSON_TrophyProgress[] trophies;

            public <DeleteTrophies>c__AnonStorey23E()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <DeleteTrophies>c__AnonStorey23F
        {
            internal int i;
            internal PlayerData.<DeleteTrophies>c__AnonStorey23E <>f__ref$574;

            public <DeleteTrophies>c__AnonStorey23F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__146(TrophyState state)
            {
                return (state.iname == this.<>f__ref$574.trophies[this.i].iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey231
        {
            internal int i;
            internal PlayerData <>f__this;

            public <Deserialize>c__AnonStorey231()
            {
                base..ctor();
                return;
            }

            internal bool <>m__138(Json_Artifact p)
            {
                return (p.iid == this.<>f__this.mArtifacts[this.i].UniqueID);
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey232
        {
            internal JSON_ConceptCard concept_cards;

            public <Deserialize>c__AnonStorey232()
            {
                base..ctor();
                return;
            }

            internal bool <>m__139(UnitData ud)
            {
                return ((ud.ConceptCard == null) ? 0 : (ud.ConceptCard.UniqueID == this.concept_cards.iid));
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey233
        {
            internal JSON_ConceptCard[] concept_cards;
            internal PlayerData <>f__this;

            public <Deserialize>c__AnonStorey233()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey234
        {
            internal int i;
            internal PlayerData.<Deserialize>c__AnonStorey233 <>f__ref$563;

            public <Deserialize>c__AnonStorey234()
            {
                base..ctor();
                return;
            }

            internal bool <>m__13A(UnitData ud)
            {
                return ((ud.ConceptCard == null) ? 0 : (ud.ConceptCard.UniqueID == this.<>f__ref$563.concept_cards[this.i].iid));
            }
        }

        [CompilerGenerated]
        private sealed class <Deserialize>c__AnonStorey235
        {
            internal int i;
            internal PlayerData <>f__this;

            public <Deserialize>c__AnonStorey235()
            {
                base..ctor();
                return;
            }

            internal bool <>m__13B(JSON_ConceptCard p)
            {
                return (p.iid == this.<>f__this.mConceptCards[this.i].UniqueID);
            }

            internal bool <>m__13C(UnitData ud)
            {
                return ((ud.ConceptCard == null) ? 0 : (ud.ConceptCard.UniqueID == this.<>f__this.mConceptCards[this.i].UniqueID));
            }
        }

        [CompilerGenerated]
        private sealed class <EventCoinNum>c__AnonStorey243
        {
            internal string cost_iname;

            public <EventCoinNum>c__AnonStorey243()
            {
                base..ctor();
                return;
            }

            internal bool <>m__14B(EventCoinData f)
            {
                return f.iname.Equals(this.cost_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindArtifactByUniqueID>c__AnonStorey238
        {
            internal long iid;

            public <FindArtifactByUniqueID>c__AnonStorey238()
            {
                base..ctor();
                return;
            }

            internal bool <>m__13F(ArtifactData p)
            {
                return (p.UniqueID == this.iid);
            }
        }

        [CompilerGenerated]
        private sealed class <FindArtifactsByArtifactID>c__AnonStorey23A
        {
            internal string iname;

            public <FindArtifactsByArtifactID>c__AnonStorey23A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__141(ArtifactData p)
            {
                return (p.ArtifactParam.iname == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindArtifactsByIDs>c__AnonStorey239
        {
            internal HashSet<string> ids;

            public <FindArtifactsByIDs>c__AnonStorey239()
            {
                base..ctor();
                return;
            }

            internal bool <>m__140(ArtifactData artifact)
            {
                return this.ids.Contains(artifact.ArtifactParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindConceptCardByUniqueID>c__AnonStorey245
        {
            internal long iid;

            public <FindConceptCardByUniqueID>c__AnonStorey245()
            {
                base..ctor();
                return;
            }

            internal bool <>m__14D(ConceptCardData card)
            {
                return (card.UniqueID == this.iid);
            }
        }

        [CompilerGenerated]
        private sealed class <FindItemDataByItemID>c__AnonStorey236
        {
            internal string iname;

            public <FindItemDataByItemID>c__AnonStorey236()
            {
                base..ctor();
                return;
            }

            internal bool <>m__13D(ItemData p)
            {
                return (p.ItemID == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <FindItemDataByItemParam>c__AnonStorey237
        {
            internal ItemParam param;

            public <FindItemDataByItemParam>c__AnonStorey237()
            {
                base..ctor();
                return;
            }

            internal bool <>m__13E(ItemData p)
            {
                return (p.Param == this.param);
            }
        }

        [CompilerGenerated]
        private sealed class <GetConceptCardMaterialNum>c__AnonStorey248
        {
            internal string iname;

            public <GetConceptCardMaterialNum>c__AnonStorey248()
            {
                base..ctor();
                return;
            }

            internal bool <>m__150(ConceptCardMaterialData p)
            {
                return (p.IName == this.iname);
            }

            internal bool <>m__151(ConceptCardMaterialData p)
            {
                return (p.IName == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetConceptCardMaterialUniqueID>c__AnonStorey249
        {
            internal string iname;

            public <GetConceptCardMaterialUniqueID>c__AnonStorey249()
            {
                base..ctor();
                return;
            }

            internal bool <>m__152(ConceptCardMaterialData p)
            {
                return (p.IName == this.iname);
            }

            internal bool <>m__153(ConceptCardMaterialData p)
            {
                return (p.IName == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <IncrementRankMatchMission>c__AnonStorey22B
        {
            internal RankMatchMissionType type;
            internal PlayerData <>f__this;

            public <IncrementRankMatchMission>c__AnonStorey22B()
            {
                base..ctor();
                return;
            }

            internal void <>m__134(VersusRankMissionParam mission)
            {
                RankMatchMissionState state;
                <IncrementRankMatchMission>c__AnonStorey22C storeyc;
                storeyc = new <IncrementRankMatchMission>c__AnonStorey22C();
                storeyc.<>f__ref$555 = this;
                storeyc.mission = mission;
                if (storeyc.mission.Type == this.type)
                {
                    goto Label_002B;
                }
                return;
            Label_002B:
                state = this.<>f__this.RankMatchMissionState.Find(new Predicate<RankMatchMissionState>(storeyc.<>m__159));
                if (state != null)
                {
                    goto Label_0078;
                }
                state = new RankMatchMissionState();
                state.Deserialize(storeyc.mission.IName, 0, null);
                this.<>f__this.RankMatchMissionState.Add(state);
            Label_0078:
                state.Increment();
                return;
            }

            private sealed class <IncrementRankMatchMission>c__AnonStorey22C
            {
                internal VersusRankMissionParam mission;
                internal PlayerData.<IncrementRankMatchMission>c__AnonStorey22B <>f__ref$555;

                public <IncrementRankMatchMission>c__AnonStorey22C()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__159(RankMatchMissionState state)
                {
                    return (state.IName == this.mission.IName);
                }
            }
        }

        [CompilerGenerated]
        private sealed class <IsQuestAvailable>c__AnonStorey230
        {
            internal QuestParam questparam;

            public <IsQuestAvailable>c__AnonStorey230()
            {
                base..ctor();
                return;
            }

            internal bool <>m__137(QuestParam p)
            {
                return (p == this.questparam);
            }
        }

        [CompilerGenerated]
        private sealed class <LoadPlayerPrefs>c__AnonStorey23D
        {
            internal long iid;

            public <LoadPlayerPrefs>c__AnonStorey23D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__145(ArtifactData adl)
            {
                return (adl.UniqueID == this.iid);
            }
        }

        [CompilerGenerated]
        private sealed class <OverWriteConceptCardMaterials>c__AnonStorey24A
        {
            internal ConceptCardParam param;

            public <OverWriteConceptCardMaterials>c__AnonStorey24A()
            {
                base..ctor();
                return;
            }

            internal bool <>m__154(ConceptCardMaterialData p)
            {
                return (p.IName == this.param.iname);
            }

            internal bool <>m__155(ConceptCardMaterialData p)
            {
                return (p.IName == this.param.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RegistTrophyStateDictByProgExtra>c__AnonStorey240
        {
            internal TrophyParam _trophy;

            public <RegistTrophyStateDictByProgExtra>c__AnonStorey240()
            {
                base..ctor();
                return;
            }

            internal bool <>m__147(TrophyState x)
            {
                return (x.iname == this._trophy.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <RemoveConceptCardData>c__AnonStorey246
        {
            internal long[] iids;

            public <RemoveConceptCardData>c__AnonStorey246()
            {
                base..ctor();
                return;
            }

            internal bool <>m__14E(ConceptCardData card)
            {
                int num;
                num = 0;
                goto Label_0025;
            Label_0007:
                if (card.UniqueID != this.iids[num])
                {
                    goto Label_0021;
                }
                return 1;
            Label_0021:
                num += 1;
            Label_0025:
                if (num < ((int) this.iids.Length))
                {
                    goto Label_0007;
                }
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <RemoveConceptCardData>c__AnonStorey247
        {
            internal int i;
            internal PlayerData.<RemoveConceptCardData>c__AnonStorey246 <>f__ref$582;

            public <RemoveConceptCardData>c__AnonStorey247()
            {
                base..ctor();
                return;
            }

            internal bool <>m__14F(UnitData ud)
            {
                return ((ud.ConceptCard == null) ? 0 : (ud.ConceptCard.UniqueID == this.<>f__ref$582.iids[this.i]));
            }
        }

        [CompilerGenerated]
        private sealed class <RemoveFriendFollower>c__AnonStorey23C
        {
            internal string fuid;

            public <RemoveFriendFollower>c__AnonStorey23C()
            {
                base..ctor();
                return;
            }

            internal bool <>m__143(FriendData p)
            {
                return (p.FUID == this.fuid);
            }
        }

        [CompilerGenerated]
        private sealed class <RewardedRankMatchMission>c__AnonStorey22F
        {
            internal string iname;

            public <RewardedRankMatchMission>c__AnonStorey22F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__136(RankMatchMissionState state)
            {
                return (state.IName == this.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SavePlayerPrefsAsync>c__Iterator7E : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal UnitParam <param>__1;
            internal string <keyname>__2;
            internal JobData[] <jobs>__3;
            internal int <j>__4;
            internal string <jobname>__5;
            internal int <k>__6;
            internal string <eqname>__7;
            internal int <k>__8;
            internal string <abname>__9;
            internal int <k>__10;
            internal JobData <job>__11;
            internal int <k>__12;
            internal string <eqname>__13;
            internal int <k>__14;
            internal string <abname>__15;
            internal int <k>__16;
            internal int <k>__17;
            internal string <afname>__18;
            internal int <job_uniqid>__19;
            internal int <i>__20;
            internal string <itemname>__21;
            internal int <i>__22;
            internal ArtifactData <arti>__23;
            internal StringBuilder <sb>__24;
            internal int $PC;
            internal object $current;
            internal PlayerData <>f__this;

            public <SavePlayerPrefsAsync>c__Iterator7E()
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
                object[] objArray1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_014F;

                    case 2:
                        goto Label_0265;

                    case 3:
                        goto Label_0C90;
                }
                goto Label_0C97;
            Label_0029:
                EditorPlayerPrefs.SetString("Version", PlayerData.PLAYRE_DATA_VERSION);
                EditorPlayerPrefs.SetInt("PlayerExp", this.<>f__this.mExp);
                EditorPlayerPrefs.SetInt("Gold", this.<>f__this.mGold);
                EditorPlayerPrefs.SetInt("FreeCoin", this.<>f__this.mFreeCoin);
                EditorPlayerPrefs.SetInt("PaidCoin", this.<>f__this.mPaidCoin);
                EditorPlayerPrefs.SetInt("ComCoin", this.<>f__this.mComCoin);
                EditorPlayerPrefs.SetInt("TourCoin", this.<>f__this.mTourCoin);
                EditorPlayerPrefs.SetInt("ArenaCoin", this.<>f__this.mArenaCoin);
                EditorPlayerPrefs.SetInt("MultiCoin", this.<>f__this.mMultiCoin);
                EditorPlayerPrefs.SetInt("PiecePoint", this.<>f__this.mPiecePoint);
                EditorPlayerPrefs.SetInt("UnitNum", this.<>f__this.mUnits.Count);
                this.$current = null;
                this.$PC = 1;
                goto Label_0C99;
            Label_014F:
                if (this.<>f__this.mStamina == null)
                {
                    goto Label_019D;
                }
                EditorPlayerPrefs.SetInt("Stamina", this.<>f__this.mStamina.val);
                EditorPlayerPrefs.SetString("StaminaAt", &this.<>f__this.mStamina.at.ToString());
            Label_019D:
                if (this.<>f__this.mCaveStamina == null)
                {
                    goto Label_01EB;
                }
                EditorPlayerPrefs.SetInt("CaveStamina", this.<>f__this.mCaveStamina.val);
                EditorPlayerPrefs.SetString("CaveStaminaAt", &this.<>f__this.mCaveStamina.at.ToString());
            Label_01EB:
                if (this.<>f__this.mAbilityRankUpCount == null)
                {
                    goto Label_0239;
                }
                EditorPlayerPrefs.SetInt("AbilRankUpCount", this.<>f__this.mAbilityRankUpCount.val);
                EditorPlayerPrefs.SetString("AbilRankUpCountAt", &this.<>f__this.mAbilityRankUpCount.at.ToString());
            Label_0239:
                this.<i>__0 = 0;
                goto Label_09E6;
            Label_0245:
                if ((this.<i>__0 % 4) != null)
                {
                    goto Label_0265;
                }
                this.$current = null;
                this.$PC = 2;
                goto Label_0C99;
            Label_0265:
                this.<param>__1 = this.<>f__this.mUnits[this.<i>__0].UnitParam;
                this.<keyname>__2 = "Unit" + ((int) this.<i>__0) + "_";
                EditorPlayerPrefs.SetString(this.<keyname>__2 + "Iname", this.<param>__1.iname);
                EditorPlayerPrefs.SetInt(this.<keyname>__2 + "Iid", (int) this.<>f__this.mUnits[this.<i>__0].UniqueID);
                EditorPlayerPrefs.SetInt(this.<keyname>__2 + "Exp", this.<>f__this.mUnits[this.<i>__0].Exp);
                EditorPlayerPrefs.SetInt(this.<keyname>__2 + "Plus", this.<>f__this.mUnits[this.<i>__0].AwakeLv);
                EditorPlayerPrefs.SetInt(this.<keyname>__2 + "Rarity", this.<>f__this.mUnits[this.<i>__0].Rarity);
                this.<jobs>__3 = this.<>f__this.mUnits[this.<i>__0].Jobs;
                this.<j>__4 = 0;
                goto Label_0924;
            Label_03B4:
                objArray1 = new object[] { this.<keyname>__2, "Job", (int) this.<j>__4, "_" };
                this.<jobname>__5 = string.Concat(objArray1);
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + "Iname");
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + "Iid");
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + "Rank");
                this.<k>__6 = 0;
                goto Label_04B6;
            Label_0437:
                this.<eqname>__7 = "Equip" + ((int) this.<k>__6) + "_";
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + this.<eqname>__7 + "Iname");
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + this.<eqname>__7 + "Iid");
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + this.<eqname>__7 + "Exp");
                this.<k>__6 += 1;
            Label_04B6:
                if (this.<k>__6 < 6)
                {
                    goto Label_0437;
                }
                this.<k>__8 = 0;
                goto Label_054D;
            Label_04CE:
                this.<abname>__9 = "Ability" + ((int) this.<k>__8) + "_";
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + this.<abname>__9 + "Iname");
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + this.<abname>__9 + "Iid");
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + this.<abname>__9 + "Exp");
                this.<k>__8 += 1;
            Label_054D:
                if (this.<k>__8 < 8)
                {
                    goto Label_04CE;
                }
                this.<k>__10 = 0;
                goto Label_0593;
            Label_0565:
                EditorPlayerPrefs.DeleteKey(this.<jobname>__5 + "Select_Ability" + ((int) this.<k>__10));
                this.<k>__10 += 1;
            Label_0593:
                if (this.<k>__10 < 5)
                {
                    goto Label_0565;
                }
                if (this.<jobs>__3 == null)
                {
                    goto Label_0916;
                }
                if (this.<j>__4 < ((int) this.<jobs>__3.Length))
                {
                    goto Label_05C2;
                }
                goto Label_0916;
            Label_05C2:
                this.<job>__11 = this.<jobs>__3[this.<j>__4];
                EditorPlayerPrefs.SetString(this.<jobname>__5 + "Iname", this.<job>__11.JobID);
                EditorPlayerPrefs.SetInt(this.<jobname>__5 + "Iid", (int) this.<job>__11.UniqueID);
                EditorPlayerPrefs.SetInt(this.<jobname>__5 + "Rank", this.<job>__11.Rank);
                this.<k>__12 = 0;
                goto Label_0728;
            Label_0642:
                if (this.<job>__11.Equips[this.<k>__12].IsValid() != null)
                {
                    goto Label_0663;
                }
                goto Label_071A;
            Label_0663:
                this.<eqname>__13 = "Equip" + ((int) this.<k>__12) + "_";
                EditorPlayerPrefs.SetString(this.<jobname>__5 + this.<eqname>__13 + "Iname", this.<job>__11.Equips[this.<k>__12].ItemID);
                EditorPlayerPrefs.SetInt(this.<jobname>__5 + this.<eqname>__13 + "Iid", (int) this.<job>__11.Equips[this.<k>__12].UniqueID);
                EditorPlayerPrefs.SetInt(this.<jobname>__5 + this.<eqname>__13 + "Exp", this.<job>__11.Equips[this.<k>__12].Exp);
            Label_071A:
                this.<k>__12 += 1;
            Label_0728:
                if (this.<k>__12 < 6)
                {
                    goto Label_0642;
                }
                this.<k>__14 = 0;
                goto Label_0816;
            Label_0740:
                this.<abname>__15 = "Ability" + ((int) this.<k>__14) + "_";
                EditorPlayerPrefs.SetString(this.<jobname>__5 + this.<abname>__15 + "Iname", this.<job>__11.LearnAbilitys[this.<k>__14].Param.iname);
                EditorPlayerPrefs.SetInt(this.<jobname>__5 + this.<abname>__15 + "Iid", (int) this.<job>__11.LearnAbilitys[this.<k>__14].UniqueID);
                EditorPlayerPrefs.SetInt(this.<jobname>__5 + this.<abname>__15 + "Exp", this.<job>__11.LearnAbilitys[this.<k>__14].Exp);
                this.<k>__14 += 1;
            Label_0816:
                if (this.<k>__14 < this.<job>__11.LearnAbilitys.Count)
                {
                    goto Label_0740;
                }
                this.<k>__16 = 0;
                goto Label_087E;
            Label_083D:
                EditorPlayerPrefs.SetInt(this.<jobname>__5 + "Select_Ability" + ((int) this.<k>__16), (int) this.<job>__11.AbilitySlots[this.<k>__16]);
                this.<k>__16 += 1;
            Label_087E:
                if (this.<k>__16 < ((int) this.<job>__11.AbilitySlots.Length))
                {
                    goto Label_083D;
                }
                this.<k>__17 = 0;
                goto Label_08FE;
            Label_08A2:
                this.<afname>__18 = "Artifact" + ((int) this.<k>__17) + "_";
                EditorPlayerPrefs.SetInt(this.<jobname>__5 + this.<afname>__18 + "Iid", (int) this.<job>__11.Artifacts[this.<k>__17]);
                this.<k>__17 += 1;
            Label_08FE:
                if (this.<k>__17 < ((int) this.<job>__11.Artifacts.Length))
                {
                    goto Label_08A2;
                }
            Label_0916:
                this.<j>__4 += 1;
            Label_0924:
                if (this.<j>__4 < 4)
                {
                    goto Label_03B4;
                }
                this.<job_uniqid>__19 = 0;
                if (this.<>f__this.mUnits[this.<i>__0].Jobs == null)
                {
                    goto Label_09BD;
                }
                if (((int) this.<>f__this.mUnits[this.<i>__0].Jobs.Length) <= 0)
                {
                    goto Label_09BD;
                }
                this.<job_uniqid>__19 = (int) this.<>f__this.mUnits[this.<i>__0].Jobs[this.<>f__this.mUnits[this.<i>__0].JobIndex].UniqueID;
            Label_09BD:
                EditorPlayerPrefs.SetInt(this.<keyname>__2 + "Select_Job", this.<job_uniqid>__19);
                this.<i>__0 += 1;
            Label_09E6:
                if (this.<i>__0 < this.<>f__this.mUnits.Count)
                {
                    goto Label_0245;
                }
                this.<>f__this.InternalSavePlayerPrefsParty();
                EditorPlayerPrefs.SetInt("ItemNum", this.<>f__this.mItems.Count);
                this.<i>__20 = 0;
                goto Label_0AF1;
            Label_0A32:
                this.<itemname>__21 = "Item" + ((int) this.<i>__20) + "_";
                EditorPlayerPrefs.SetString(this.<itemname>__21 + "Iname", this.<>f__this.mItems[this.<i>__20].ItemID);
                EditorPlayerPrefs.SetInt(this.<itemname>__21 + "Iid", (int) this.<>f__this.mItems[this.<i>__20].UniqueID);
                EditorPlayerPrefs.SetInt(this.<itemname>__21 + "Num", this.<>f__this.mItems[this.<i>__20].Num);
                this.<i>__20 += 1;
            Label_0AF1:
                if (this.<i>__20 < this.<>f__this.mItems.Count)
                {
                    goto Label_0A32;
                }
                EditorPlayerPrefs.SetInt("ARTI_NUM", this.<>f__this.mArtifacts.Count);
                this.<i>__22 = 0;
                goto Label_0C5D;
            Label_0B32:
                this.<arti>__23 = this.<>f__this.mArtifacts[this.<i>__22];
                this.<sb>__24 = GameUtility.GetStringBuilder();
                this.<sb>__24.Append("{\"iid\":");
                this.<sb>__24.Append(this.<arti>__23.UniqueID);
                this.<sb>__24.Append(",\"exp\":");
                this.<sb>__24.Append(this.<arti>__23.Exp);
                this.<sb>__24.Append(",\"iname\":\"");
                this.<sb>__24.Append(this.<arti>__23.ArtifactParam.iname);
                this.<sb>__24.Append("\"");
                this.<sb>__24.Append(",\"rare\":");
                this.<sb>__24.Append(this.<arti>__23.Rarity);
                this.<sb>__24.Append("}");
                EditorPlayerPrefs.SetString("ARTI_" + ((int) this.<i>__22), this.<sb>__24.ToString());
                this.<i>__22 += 1;
            Label_0C5D:
                if (this.<i>__22 < this.<>f__this.mArtifacts.Count)
                {
                    goto Label_0B32;
                }
                EditorPlayerPrefs.Flush();
                this.$current = null;
                this.$PC = 3;
                goto Label_0C99;
            Label_0C90:
                this.$PC = -1;
            Label_0C97:
                return 0;
            Label_0C99:
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
        private sealed class <SetEventCoinNum>c__AnonStorey244
        {
            internal string cost_iname;

            public <SetEventCoinNum>c__AnonStorey244()
            {
                base..ctor();
                return;
            }

            internal bool <>m__14C(ItemData f)
            {
                return f.Param.iname.Equals(this.cost_iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SetMaxProgRankMatchMission>c__AnonStorey22D
        {
            internal RankMatchMissionType type;
            internal int prog;
            internal PlayerData <>f__this;

            public <SetMaxProgRankMatchMission>c__AnonStorey22D()
            {
                base..ctor();
                return;
            }

            internal void <>m__135(VersusRankMissionParam mission)
            {
                RankMatchMissionState state;
                <SetMaxProgRankMatchMission>c__AnonStorey22E storeye;
                storeye = new <SetMaxProgRankMatchMission>c__AnonStorey22E();
                storeye.<>f__ref$557 = this;
                storeye.mission = mission;
                if (storeye.mission.Type == this.type)
                {
                    goto Label_002B;
                }
                return;
            Label_002B:
                state = this.<>f__this.RankMatchMissionState.Find(new Predicate<RankMatchMissionState>(storeye.<>m__15A));
                if (state != null)
                {
                    goto Label_0078;
                }
                state = new RankMatchMissionState();
                state.Deserialize(storeye.mission.IName, 0, null);
                this.<>f__this.RankMatchMissionState.Add(state);
            Label_0078:
                if (state.Progress >= this.prog)
                {
                    goto Label_0095;
                }
                state.SetProgress(this.prog);
            Label_0095:
                return;
            }

            private sealed class <SetMaxProgRankMatchMission>c__AnonStorey22E
            {
                internal VersusRankMissionParam mission;
                internal PlayerData.<SetMaxProgRankMatchMission>c__AnonStorey22D <>f__ref$557;

                public <SetMaxProgRankMatchMission>c__AnonStorey22E()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__15A(RankMatchMissionState state)
                {
                    return (state.IName == this.mission.IName);
                }
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateEventCoin>c__AnonStorey242
        {
            internal int i;
            internal PlayerData <>f__this;

            public <UpdateEventCoin>c__AnonStorey242()
            {
                base..ctor();
                return;
            }

            internal bool <>m__14A(ItemData f)
            {
                return f.Param.iname.Equals(this.<>f__this.mEventCoinList[this.i].iname);
            }
        }

        [Flags]
        public enum EDeserializeFlags
        {
            None = 0,
            Gold = 1,
            Coin = 2,
            Stamina = 4,
            Cave = 8,
            AbilityUp = 0x10,
            Arena = 0x20,
            Tour = 0x40
        }

        public class Json_FriendData
        {
            public Json_Unit[] friends;

            public Json_FriendData()
            {
                base..ctor();
                return;
            }
        }

        public class Json_InitData
        {
            public PlayerData.Json_InitUnits[] units;
            public PlayerData.Json_InitItems[] items;
            public PlayerData.Json_InitParty[] party;
            public PlayerData.Json_InitUnits[] friends;

            public Json_InitData()
            {
                base..ctor();
                return;
            }
        }

        public class Json_InitItems
        {
            public string iname;
            public int num;

            public Json_InitItems()
            {
                base..ctor();
                return;
            }
        }

        public class Json_InitParty
        {
            public PlayerData.Json_InitPartyUnit[] units;

            public Json_InitParty()
            {
                base..ctor();
                return;
            }
        }

        public class Json_InitPartyUnit
        {
            public int iid;
            public int leader;

            public Json_InitPartyUnit()
            {
                base..ctor();
                return;
            }
        }

        public class Json_InitUnits
        {
            public string iname;
            public int exp;
            public string[] skills;

            public Json_InitUnits()
            {
                base..ctor();
                return;
            }
        }

        private class JSON_TrophyState
        {
            public string id;
            public int[] cnt;
            public long st;
            public int fin;

            public JSON_TrophyState()
            {
                this.id = string.Empty;
                base..ctor();
                return;
            }
        }
    }
}

