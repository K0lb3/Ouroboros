namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class GlobalVars
    {
        [ResetOnLogin]
        public static GlobalVar<int> SelectedPartyIndex;
        [ResetOnLogin]
        public static GlobalVar<string> SelectedChapter;
        [ResetOnLogin]
        public static GlobalVar<string> SelectedSection;
        [ResetOnLogin]
        public static GlobalVar<int> SelectedStoryPart;
        public static int BanStatus;
        public static string CustomerID;
        public static Vector2 Location;
        public static GlobalVar<long> BtlID;
        public static QuestTypes QuestType;
        public static GlobalVar<long> ContinueBtlID;
        public static BattleCore.Record ContinueBtlRecord;
        public static GlobalVar<SupportData> SelectedSupport;
        public static int SelectedTeamIndex;
        public static string SelectedFriendID;
        public static string SelectedQuestID;
        public static string SelectedItemID;
        public static string SelectedCreateItemID;
        public static string EditPlayerName;
        public static string SelectedArtifactID;
        [ResetOnLogin]
        public static GlobalVar<string> LastPlayedQuest;
        [ResetOnLogin]
        public static GlobalVar<QuestStates> LastQuestState;
        [ResetOnLogin]
        public static GlobalVar<BattleCore.QuestResult> LastQuestResult;
        [ResetOnLogin]
        public static GlobalVar<string> SelectedAbilityID;
        public static string UnlockUnitID;
        public static GlobalVar<ArenaPlayer> SelectedArenaPlayer;
        public static ArenaBattleResponse ResultArenaBattleResponse;
        public static GlobalVar<string> SelectedTrophy;
        public static GlobalVar<long> SelectedUnitUniqueID;
        public static GlobalVar<long> SelectedEquipUniqueID;
        public static GlobalVar<long> SelectedJobUniqueID;
        public static GlobalVar<long> PreBattleUnitUniqueID;
        public static GlobalVar<TobiraParam.Category> PreBattleUnitTobiraCategory;
        public static GlobalVar<long> SelectedArtifactUniqueID;
        public static GlobalVar<string> SelectedArtifactIname;
        public static Dictionary<string, int> UsedArtifactExpItems;
        public static List<long> SellArtifactsList;
        public static List<long> ConvertArtifactsList;
        public static GlobalVar<int> SelectedEquipmentSlot;
        public static GlobalVar<int> SelectedUnitJobIndex;
        public static List<ItemParam> SelectedItemParamTree;
        public static GlobalVar<int> SelectedAbilitySlot;
        public static GlobalVar<long> SelectedAbilityUniqueID;
        public static GlobalVar<long> SelectedMailUniqueID;
        public static GlobalVar<int> SelectedMailPeriod;
        public static GlobalVar<int> SelectedMailPage;
        public static GlobalVar<RewardData> LastReward;
        public static Unit MainTarget;
        public static Unit SubTarget;
        public static FriendData SelectedFriend;
        public static FriendData FoundFriend;
        public static int RaidNum;
        public static SRPG.RaidResult RaidResult;
        public static GlobalVar<int> PlayerExpOld;
        public static GlobalVar<int> PlayerExpNew;
        public static GlobalVar<bool> PlayerLevelChanged;
        public static string SelectedGachaTableId;
        public static EShopType ShopType;
        public static int ShopBuyIndex;
        public static int ShopBuyAmount;
        public static List<ShopItem> TimeOutShopItems;
        public static LimitedShopListItem LimitedShopItem;
        public static EventShopInfo EventShopItem;
        public static List<EventShopListItem> EventShopListItems;
        public static EventShopListItem SelectionEventShop;
        public static CoinListSelectionType SelectionCoinListType;
        public static SellItem SelectSellItem;
        public static List<SellItem> SellItemList;
        public static List<SellItem> ConvertAwakePieceList;
        public static GlobalVar<JobRankUpTypes> JobRankUpType;
        public static List<AbilityData> LearningAbilities;
        public static List<ItemData> ReturnItems;
        public static Dictionary<long, int> AbilitiesRankUp;
        public static EquipData SelectedEquipData;
        public static List<EnhanceMaterial> SelectedEnhanceMaterials;
        public static Dictionary<string, int> UsedUnitExpItems;
        public static string EditMultiPlayRoomComment;
        public static string EditMultiPlayRoomPassCode;
        public static bool SelectedMultiPlayQuestIsEvent;
        public static JSON_MyPhotonRoomParam.EType SelectedMultiPlayRoomType;
        public static string SelectedMultiPlayArea;
        public static string SelectedMultiPlayRoomName;
        public static string SelectedMultiPlayRoomComment;
        public static string SelectedMultiPlayRoomPassCodeHash;
        public static JSON_MyPhotonPlayerParam SelectedMultiPlayerParam;
        public static List<int> SelectedMultiPlayerUnitIDs;
        public static VERSUS_TYPE SelectedMultiPlayVersusType;
        public static string MultiPlayVersusKey;
        public static bool VersusRoomReuse;
        public static bool SelectedMultiPlayLimit;
        public static bool MultiPlayClearOnly;
        public static int MultiPlayJoinUnitLv;
        public static string SelectedMultiTowerID;
        public static int SelectedMultiTowerFloor;
        public static bool CreateAutoMultiTower;
        public static bool InvtationSameUser;
        public static GlobalVar<VersusCpuData> VersusCpu;
        public static long VersusFreeMatchTime;
        public static bool IsVersusDraftMode;
        public static EMultiPlayContinue SelectedMultiPlayContinue;
        public static BattleCore.Json_BattleCont MultiPlayBattleCont;
        public static int ResumeMultiplayPlayerID;
        public static int ResumeMultiplaySeatID;
        public static int MultiInvitation;
        public static string MultiInvitationRoomOwner;
        public static bool MultiInvitationRoomLocked;
        public static bool MultiInvitaionFlag;
        public static string MultiInvitaionComment;
        public static int SelectedMultiPlayRoomID;
        public static string SelectedMultiPlayPhotonAppID;
        public static int SelectedTowerMultiPartyIndex;
        public static List<List<VersusRankReward>> RankMatchSeasonReward;
        public static string SelectedProductID;
        public static int EditedYear;
        public static int EditedMonth;
        public static int EditedDay;
        public static int BeforeCoin;
        public static int AfterCoin;
        public static bool IsTutorialEnd;
        public static bool DebugIsPlayTutorial;
        public static string PubHash;
        public static string UrgencyPubHash;
        public static string SelectedChallengeMissionTrophy;
        public static GlobalVar<int> CurrentChatChannel;
        public static int ChatChannelViewNum;
        public static int ChatChannelMax;
        [ResetOnLogin]
        public static GlobalVar<string> ReplaySelectedChapter;
        [ResetOnLogin]
        public static GlobalVar<string> ReplaySelectedSection;
        [ResetOnLogin]
        public static GlobalVar<string> ReplaySelectedQuestID;
        public static UnitGetParam UnitGetReward;
        public static string PreEventName;
        public static bool ForceSceneChange;
        public static EventQuestListType ReqEventPageListType;
        public static bool KeyQuestTimeOver;
        public static long mDropTableGeneratedUnixTime;
        public static string SelectedTowerID;
        public static string SelectedFloorID;
        public static GlobalVar<long> SelectedSupportUnitUniqueID;
        [ResetOnLogin]
        public static GlobalVar<bool> IsEventShopOpen;
        public static SRPG.ItemSelectListItemData ItemSelectListItemData;
        public static ArtifactSelectListItemData ArtifactListItem;
        public static string[] ConditionJobs;
        public static CollaboSkillParam.Pair SelectedCollaboSkillPair;
        public static string TeamName;
        public static UserSelectionPartyData UserSelectionPartyDataInfo;
        public static bool PartyUploadFinished;
        public static bool RankingQuestSelected;
        [ResetOnLogin]
        public static GlobalVar<bool> IsTitleStart;
        public static RankingQuestParam SelectedRankingQuestParam;
        public static List<PartyEditData> OrdealParties;
        public static List<SupportData> OrdealSupports;
        [ResetOnLogin]
        public static GlobalVar<bool> IsDirtyConceptCardData;
        public static GlobalVar<ConceptCardData> SelectedConceptCardData;
        public static bool RestoreBeginnerQuest;
        [ResetOnLogin]
        public static GlobalVar<int> ConceptCardNum;
        public static bool IsSkipQuestDemo;
        public static RecommendTeamSetting RecommendTeamSettingValue;
        public static SummonCoinInfo NewSummonCoinInfo;
        public static SummonCoinInfo OldSummonCoinInfo;
        public static string LastReadTips;
        public static List<string> BlockList;

        static GlobalVars()
        {
            FieldInfo[] infoArray;
            int num;
            Type[] typeArray;
            BanStatus = 0;
            CustomerID = string.Empty;
            Location = Vector2.get_zero();
            UsedArtifactExpItems = new Dictionary<string, int>();
            SellArtifactsList = new List<long>();
            ConvertArtifactsList = new List<long>();
            SelectedEquipmentSlot = new GlobalVar<int>(-1);
            SelectedItemParamTree = new List<ItemParam>();
            EventShopListItems = new List<EventShopListItem>();
            SelectionCoinListType = 0;
            AbilitiesRankUp = new Dictionary<long, int>();
            UsedUnitExpItems = new Dictionary<string, int>();
            VersusFreeMatchTime = -1L;
            IsVersusDraftMode = 0;
            MultiInvitaionFlag = 0;
            MultiInvitaionComment = string.Empty;
            RankMatchSeasonReward = new List<List<VersusRankReward>>();
            CurrentChatChannel = new GlobalVar<int>(-1);
            mDropTableGeneratedUnixTime = 0L;
            IsEventShopOpen = new GlobalVar<bool>(0);
            IsTitleStart = new GlobalVar<bool>(0);
            OrdealParties = new List<PartyEditData>();
            OrdealSupports = new List<SupportData>();
            IsDirtyConceptCardData = new GlobalVar<bool>(1);
            SelectedConceptCardData = new GlobalVar<ConceptCardData>();
            ConceptCardNum = new GlobalVar<int>(0);
            BlockList = new List<string>();
            infoArray = typeof(GlobalVars).GetFields(0x18);
            num = 0;
            goto Label_0169;
        Label_0117:
            typeArray = infoArray[num].FieldType.GetInterfaces();
            if (typeArray == null)
            {
                goto Label_0165;
            }
            if (Array.IndexOf<Type>(typeArray, typeof(IGlobalVar)) < 0)
            {
                goto Label_0165;
            }
            if (infoArray[num].GetValue(null) != null)
            {
                goto Label_0165;
            }
            infoArray[num].SetValue(null, Activator.CreateInstance(infoArray[num].FieldType));
        Label_0165:
            num += 1;
        Label_0169:
            if (num < ((int) infoArray.Length))
            {
                goto Label_0117;
            }
            return;
        }

        public static DateTime GetDropTableGeneratedDateTime()
        {
            return TimeManager.FromUnixTime(mDropTableGeneratedUnixTime);
        }

        public static long GetDropTableGeneratedUnixTime()
        {
            return mDropTableGeneratedUnixTime;
        }

        public static void ResetVarsWithAttribute(Type attrType)
        {
            FieldInfo[] infoArray;
            int num;
            Type[] typeArray;
            IGlobalVar var;
            infoArray = typeof(GlobalVars).GetFields(0x18);
            num = 0;
            goto Label_009B;
        Label_0019:
            if (((int) infoArray[num].GetCustomAttributes(attrType, 1).Length) <= 0)
            {
                goto Label_0097;
            }
            typeArray = infoArray[num].FieldType.GetInterfaces();
            if (typeArray == null)
            {
                goto Label_006F;
            }
            if (Array.IndexOf<Type>(typeArray, typeof(IGlobalVar)) < 0)
            {
                goto Label_006F;
            }
            var = (IGlobalVar) infoArray[num].GetValue(null);
            var.Reset();
            goto Label_0097;
        Label_006F:
            if (infoArray[num].FieldType.IsValueType == null)
            {
                goto Label_0097;
            }
            infoArray[num].SetValue(null, Activator.CreateInstance(infoArray[num].FieldType));
        Label_0097:
            num += 1;
        Label_009B:
            if (num < ((int) infoArray.Length))
            {
                goto Label_0019;
            }
            return;
        }

        public static void SetDropTableGeneratedTime()
        {
            mDropTableGeneratedUnixTime = Network.GetServerTime();
            return;
        }

        public enum CoinListSelectionType
        {
            None,
            EventShop,
            ArenaShop,
            MultiShop
        }

        public enum EMultiPlayContinue
        {
            PENDING,
            CONTINUE,
            CANCEL
        }

        public enum EventQuestListType
        {
            EventQuest,
            KeyQuest,
            Tower,
            RankingQuest,
            BeginnerQuest
        }

        public class GlobalVar<T> : GlobalVars.IGlobalVar
        {
            private VariableChangeEvent<T> mListeners;
            private T mValue;
            private T mDefaultValue;

            public GlobalVar()
            {
                T local;
                T local2;
                base..ctor();
                this.mValue = default(T);
                this.mDefaultValue = default(T);
                this.mListeners = null;
                return;
            }

            public GlobalVar(T defaultValue)
            {
                base..ctor();
                this.mValue = defaultValue;
                this.mDefaultValue = defaultValue;
                this.mListeners = null;
                return;
            }

            public void AddChangeEventListener(VariableChangeEvent<T> callback)
            {
                if (this.mListeners != null)
                {
                    goto Label_0017;
                }
                this.mListeners = callback;
                goto Label_002E;
            Label_0017:
                this.mListeners = (VariableChangeEvent<T>) Delegate.Combine(this.mListeners, callback);
            Label_002E:
                return;
            }

            public T Get()
            {
                return this.mValue;
            }

            public static implicit operator T(GlobalVars.GlobalVar<T> src)
            {
                return src.mValue;
            }

            public void RemoveChangeEventListener(VariableChangeEvent<T> callback)
            {
                this.mListeners = (VariableChangeEvent<T>) Delegate.Remove(this.mListeners, callback);
                return;
            }

            public void Reset()
            {
                this.mValue = this.mDefaultValue;
                return;
            }

            public unsafe void Set(T value)
            {
                if (((T) value) != null)
                {
                    goto Label_001B;
                }
                if (((T) this.mValue) != null)
                {
                    goto Label_0043;
                }
            Label_001B:
                if (((T) value) == null)
                {
                    goto Label_0060;
                }
                if (&value.Equals((T) this.mValue) != null)
                {
                    goto Label_0060;
                }
            Label_0043:
                this.mValue = value;
                if (this.mListeners == null)
                {
                    goto Label_0060;
                }
                this.mListeners();
            Label_0060:
                return;
            }

            public override unsafe string ToString()
            {
                if (((T) this.mValue) == null)
                {
                    goto Label_0022;
                }
                return &this.mValue.ToString();
            Label_0022:
                return null;
            }

            public delegate void VariableChangeEvent();
        }

        public interface IGlobalVar
        {
            void Reset();
        }

        public enum JobRankUpTypes
        {
            RankUp,
            Unlock,
            ClassChange
        }

        [Serializable]
        public class RecommendTeamSetting
        {
            public GlobalVars.RecommendType recommendedType;
            public EElement recommendedElement;

            public RecommendTeamSetting(GlobalVars.RecommendType type, EElement elem)
            {
                base..ctor();
                this.recommendedType = type;
                this.recommendedElement = elem;
                return;
            }
        }

        public enum RecommendType
        {
            Total,
            Attack,
            Defence,
            Magic,
            Mind,
            Speed,
            AttackTypeSlash,
            AttackTypeStab,
            AttackTypeBlow,
            AttackTypeShot,
            AttackTypeMagic,
            AttackTypeNone,
            Hp
        }

        public class ResetOnLogin : Attribute
        {
            public ResetOnLogin()
            {
                base..ctor();
                return;
            }
        }

        public class SummonCoinInfo
        {
            public int ConvertedSummonCoin;
            public int ReceivedStone;
            public int SummonCoinStock;
            public long Period;
            public long ConvertedDate;

            public SummonCoinInfo()
            {
                base..ctor();
                return;
            }
        }

        public class UserSelectionPartyData
        {
            public UnitData[] unitData;
            public SupportData supportData;
            public int[] achievements;
            public ItemData[] usedItems;

            public UserSelectionPartyData()
            {
                base..ctor();
                return;
            }
        }
    }
}

