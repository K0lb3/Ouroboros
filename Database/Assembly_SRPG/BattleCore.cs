namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;

    public class BattleCore
    {
        public static readonly int MAX_MAP;
        public static readonly int MAX_PARTY;
        public static readonly int MAX_ENEMY;
        public static readonly int MAX_ORDER;
        public static readonly int MAX_UNITS;
        public static readonly int MAX_GEMS;
        private QuestParam mQuestParam;
        private long mBtlID;
        private int mBtlFlags;
        private int mWinTriggerCount;
        private int mLoseTriggerCount;
        private int mActionCount;
        private int mKillstreak;
        private int mMaxKillstreak;
        private Dictionary<string, int> mTargetKillstreakDict;
        private Dictionary<string, int> mMaxTargetKillstreakDict;
        private bool mPlayByManually;
        private bool mIsUseAutoPlayMode;
        private int mTotalHeal;
        private int mTotalDamagesTaken;
        private int mTotalDamages;
        private int mNumUsedItems;
        private int mNumUsedSkills;
        private List<Unit> mAllUnits;
        private int mNpcStartIndex;
        private int mEntryUnitMax;
        private List<Unit> mUnits;
        private List<Unit> mPlayer;
        private List<Unit>[] mEnemys;
        private OInt mLeaderIndex;
        private OInt mEnemyLeaderIndex;
        private OInt mFriendIndex;
        private FriendStates mFriendStates;
        private List<Unit> mStartingMembers;
        private List<Unit> mHelperUnits;
        private List<BattleMap> mMap;
        private int mMapIndex;
        private OInt mClockTime;
        private OInt mClockTimeTotal;
        private int mContinueCount;
        private int mCurrentTeamId;
        private int mMaxTeamId;
        private string mFinisherIname;
        public ItemData[] mInventory;
        private List<OrderData> mOrder;
        private BattleLogServer mLogs;
        private uint mSeed;
        private RandXorshift mRand;
        private uint mSeedDamage;
        private RandXorshift mRandDamage;
        private RandXorshift CurrentRand;
        private Dictionary<string, SkillExecLog> mSkillExecLogs;
        private Record mRecord;
        private List<Grid> mGridLines;
        private List<Unit> mTreasures;
        private List<Unit> mGems;
        private string[] mQuestCampaignIds;
        private List<GimmickEvent> mGimmickEvents;
        private RankingQuestParam mRankingQuestParam;
        private int mMyPlayerIndex;
        private bool mMultiFinishLoad;
        private RESUME_STATE mResumeState;
        public LogCallback LogHandler;
        public LogCallback WarningHandler;
        public LogCallback ErrorHandler;
        private static BaseStatus BuffWorkStatus;
        private static BaseStatus DebuffWorkStatus;
        private static BaseStatus BuffNegativeWorkStatus;
        private static BaseStatus DebuffNegativeWorkStatus;
        private static BaseStatus BuffWorkScaleStatus;
        private static BaseStatus DebuffWorkScaleStatus;
        public bool[] EventTriggers;
        private bool mIsArenaSkip;
        private uint mArenaActionCountMax;
        private uint mArenaActionCount;
        private string mArenaQuestID;
        private Json_Battle mArenaQuestJsonBtl;
        private bool mIsArenaCalc;
        private QuestResult mArenaCalcResult;
        private eArenaCalcType mArenaCalcTypeNext;
        private List<Unit> sameJudgeUnitLists;
        private static EUnitDirection[] leftDirection;
        private static EUnitDirection[] rightDirection;
        private static BaseStatus BuffAagWorkStatus;
        private static BaseStatus DebuffAagWorkStatus;
        private static BaseStatus BuffAagNegativeWorkStatus;
        private static BaseStatus DebuffAagNegativeWorkStatus;
        private static BaseStatus AagWorkStatus;
        private bool IsEnableAagBuff;
        private bool IsEnableAagDebuff;
        private bool IsEnableAagBuffNegative;
        private bool IsEnableAagDebuffNegative;
        private List<BuffAttachment> AagBuffAttachmentLists;
        private List<Unit> AagTargetLists;
        private static List<SkillResult> mSkillResults;
        private List<Unit> mEnemyPriorities;
        private List<Unit> mGimmickPriorities;
        private GridMap<int> mMoveMap;
        private GridMap<bool> mRangeMap;
        private GridMap<bool> mScopeMap;
        private GridMap<bool> mSearchMap;
        private GridMap<int> mSafeMap;
        private GridMap<int> mSafeMapEx;
        private SRPG.SkillMap mSkillMap;
        private SRPG.TrickMap mTrickMap;
        [CompilerGenerated]
        private bool <SyncStart>k__BackingField;
        [CompilerGenerated]
        private bool <IsMultiPlay>k__BackingField;
        [CompilerGenerated]
        private bool <IsMultiVersus>k__BackingField;
        [CompilerGenerated]
        private bool <IsRankMatch>k__BackingField;
        [CompilerGenerated]
        private bool <IsMultiTower>k__BackingField;
        [CompilerGenerated]
        private bool <IsVSForceWin>k__BackingField;
        [CompilerGenerated]
        private bool <IsVSForceWinComfirm>k__BackingField;
        [CompilerGenerated]
        private bool <RequestAutoBattle>k__BackingField;
        [CompilerGenerated]
        private bool <IsAutoBattle>k__BackingField;
        [CompilerGenerated]
        private bool <EntryBattleMultiPlayTimeUp>k__BackingField;
        [CompilerGenerated]
        private bool <MultiPlayDisconnectAutoBattle>k__BackingField;
        [CompilerGenerated]
        private uint <VersusTurnMax>k__BackingField;
        [CompilerGenerated]
        private uint <VersusTurnCount>k__BackingField;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, bool> <>f__am$cache76;
        [CompilerGenerated]
        private static Func<PartySlotTypeUnitPair, string> <>f__am$cache77;
        [CompilerGenerated]
        private static Predicate<Unit> <>f__am$cache78;
        [CompilerGenerated]
        private static Comparison<MoveGoalTarget> <>f__am$cache79;

        static BattleCore()
        {
            EUnitDirection[] directionArray2;
            EUnitDirection[] directionArray1;
            mSkillResults = new List<SkillResult>();
            MAX_MAP = 3;
            MAX_PARTY = 7;
            MAX_ENEMY = 0x10;
            MAX_ORDER = MAX_PARTY + MAX_ENEMY;
            MAX_UNITS = MAX_PARTY + MAX_ENEMY;
            MAX_GEMS = 0x63;
            BuffWorkStatus = new BaseStatus();
            DebuffWorkStatus = new BaseStatus();
            BuffNegativeWorkStatus = new BaseStatus();
            DebuffNegativeWorkStatus = new BaseStatus();
            BuffWorkScaleStatus = new BaseStatus();
            DebuffWorkScaleStatus = new BaseStatus();
            directionArray1 = new EUnitDirection[5];
            directionArray1[0] = 1;
            directionArray1[1] = 2;
            directionArray1[2] = 3;
            directionArray1[4] = 1;
            leftDirection = directionArray1;
            directionArray2 = new EUnitDirection[5];
            directionArray2[0] = 3;
            directionArray2[2] = 1;
            directionArray2[3] = 2;
            directionArray2[4] = 3;
            rightDirection = directionArray2;
            BuffAagWorkStatus = new BaseStatus();
            DebuffAagWorkStatus = new BaseStatus();
            BuffAagNegativeWorkStatus = new BaseStatus();
            DebuffAagNegativeWorkStatus = new BaseStatus();
            AagWorkStatus = new BaseStatus();
            return;
        }

        public BattleCore()
        {
            this.mSkillMap = new SRPG.SkillMap();
            this.mTrickMap = new SRPG.TrickMap();
            this.mQuestParam = new QuestParam();
            this.mTargetKillstreakDict = new Dictionary<string, int>();
            this.mMaxTargetKillstreakDict = new Dictionary<string, int>();
            this.mAllUnits = new List<Unit>(MAX_UNITS);
            this.mUnits = new List<Unit>(MAX_UNITS);
            this.mPlayer = new List<Unit>(MAX_PARTY);
            this.mLeaderIndex = -1;
            this.mEnemyLeaderIndex = -1;
            this.mFriendIndex = -1;
            this.mStartingMembers = new List<Unit>();
            this.mHelperUnits = new List<Unit>(MAX_ENEMY);
            this.mMap = new List<BattleMap>(MAX_MAP);
            this.mClockTime = 0;
            this.mClockTimeTotal = 0;
            this.mInventory = new ItemData[5];
            this.mOrder = new List<OrderData>(MAX_ORDER);
            this.mLogs = new BattleLogServer();
            this.mRand = new RandXorshift("mRand");
            this.mRandDamage = new RandXorshift("mRandDamage");
            this.mSkillExecLogs = new Dictionary<string, SkillExecLog>();
            this.mRecord = new Record();
            this.mTreasures = new List<Unit>();
            this.mGems = new List<Unit>();
            this.mGimmickEvents = new List<GimmickEvent>();
            this.mArenaActionCountMax = 0x19;
            this.sameJudgeUnitLists = new List<Unit>();
            this.AagBuffAttachmentLists = new List<BuffAttachment>();
            this.AagTargetLists = new List<Unit>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static unsafe int <CalcMoveTargetAI>m__6A(MoveGoalTarget p1, MoveGoalTarget p2)
        {
            return &p1.step.CompareTo(p2.step);
        }

        [CompilerGenerated]
        private int <CalcOrder>m__5D(OrderData src, OrderData dsc)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            num = src.Unit.ChargeTime;
            num2 = src.Unit.CastTime;
            num3 = dsc.Unit.ChargeTime;
            num4 = dsc.Unit.CastTime;
            num5 = this.judgeSortOrder(src, dsc);
            src.Unit.ChargeTime = num;
            src.Unit.CastTime = num2;
            dsc.Unit.ChargeTime = num3;
            dsc.Unit.CastTime = num4;
            return num5;
        }

        [CompilerGenerated]
        private static bool <Deserialize>m__5A(PartySlotTypeUnitPair slot)
        {
            return (slot.Type == 3);
        }

        [CompilerGenerated]
        private static string <Deserialize>m__5B(PartySlotTypeUnitPair slot)
        {
            return slot.Unit;
        }

        [CompilerGenerated]
        private static bool <IsAllowBreakObjEntryMax>m__63(Unit unit)
        {
            return ((unit.IsBreakObj == null) ? 0 : (unit.IsDead == 0));
        }

        private bool AbilityChange(Unit self, Unit target, SkillData skill)
        {
            string str;
            string str2;
            GameManager manager;
            AbilityParam param;
            AbilityParam param2;
            Unit unit;
            if (self == null)
            {
                goto Label_001D;
            }
            if (target == null)
            {
                goto Label_001D;
            }
            if (skill == null)
            {
                goto Label_001D;
            }
            if (skill.SkillParam != null)
            {
                goto Label_001F;
            }
        Label_001D:
            return 0;
        Label_001F:
            str = skill.SkillParam.AcFromAbilId;
            str2 = skill.SkillParam.AcToAbilId;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_004D;
            }
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_004F;
            }
        Label_004D:
            return 0;
        Label_004F:
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.MasterParam.GetAbilityParam(str);
            param2 = manager.MasterParam.GetAbilityParam(str2);
            if (param == null)
            {
                goto Label_007D;
            }
            if (param2 != null)
            {
                goto Label_007F;
            }
        Label_007D:
            return 0;
        Label_007F:
            unit = target;
            if (skill.SkillParam.IsAcSelf() == null)
            {
                goto Label_0095;
            }
            unit = self;
        Label_0095:
            return unit.ExecuteAbilityChange(param, param2, skill.SkillParam.AcTurn, skill.SkillParam.IsAcReset());
        }

        private void AbsorbAndGiveApply(Unit self, SkillData skill, LogSkill log)
        {
            eAbsorbAndGive give;
            List<Unit> list;
            int num;
            int num2;
            Unit unit;
            int num3;
            BuffAttachment attachment;
            if (this.IsEnableAag != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (self == null)
            {
                goto Label_0029;
            }
            if (skill == null)
            {
                goto Label_0029;
            }
            if (skill.SkillParam == null)
            {
                goto Label_0029;
            }
            if (log != null)
            {
                goto Label_002A;
            }
        Label_0029:
            return;
        Label_002A:
            give = skill.SkillParam.AbsorbAndGive;
            if (give != null)
            {
                goto Label_003D;
            }
            return;
        Label_003D:
            list = new List<Unit>(this.AagTargetLists);
            num = 1;
            if (SkillParam.IsAagTypeGive(give) == null)
            {
                goto Label_00E5;
            }
            if (SkillParam.IsAagTypeDiv(give) == null)
            {
                goto Label_00AA;
            }
            if (this.AagTargetLists.Count <= 1)
            {
                goto Label_00AA;
            }
            num = this.AagTargetLists.Count;
            BuffAagWorkStatus.Div(num);
            BuffAagNegativeWorkStatus.Div(num);
            DebuffAagWorkStatus.Div(num);
            DebuffAagNegativeWorkStatus.Div(num);
        Label_00AA:
            num2 = 0;
            goto Label_00CF;
        Label_00B1:
            unit = this.AagTargetLists[num2];
            this.AbsorbAndGiveApplySetBuff(self, unit, skill, list, log);
            num2 += 1;
        Label_00CF:
            if (num2 < this.AagTargetLists.Count)
            {
                goto Label_00B1;
            }
            goto Label_00F0;
        Label_00E5:
            this.AbsorbAndGiveApplySetBuff(self, self, skill, list, log);
        Label_00F0:
            num3 = 0;
            goto Label_0115;
        Label_00F8:
            attachment = this.AagBuffAttachmentLists[num3];
            attachment.AagTargetLists = list;
            num3 += 1;
        Label_0115:
            if (num3 < this.AagBuffAttachmentLists.Count)
            {
                goto Label_00F8;
            }
            this.AbsorbAndGiveClear();
            return;
        }

        private unsafe void AbsorbAndGiveApplySetBuff(Unit self, Unit target, SkillData skill, List<Unit> at_lists, LogSkill log)
        {
            SkillEffectTargets targets;
            BuffEffect effect;
            ESkillCondition condition;
            int num;
            EffectCheckTargets targets2;
            EffectCheckTimings timings;
            int num2;
            bool flag;
            BuffAttachment attachment;
            BuffAttachment attachment2;
            BuffAttachment attachment3;
            BuffAttachment attachment4;
            BuffBit bit;
            BuffBit bit2;
            LogSkill.Target target2;
            if (self == null)
            {
                goto Label_0012;
            }
            if (target == null)
            {
                goto Label_0012;
            }
            if (skill != null)
            {
                goto Label_0013;
            }
        Label_0012:
            return;
        Label_0013:
            targets = 0;
            effect = skill.GetBuffEffect(targets);
            if (effect != null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            condition = effect.param.cond;
            num = effect.param.turn;
            targets2 = effect.param.chk_target;
            timings = effect.param.chk_timing;
            num2 = skill.DuplicateCount;
            if (at_lists != null)
            {
                goto Label_0071;
            }
            at_lists = new List<Unit>();
        Label_0071:
            flag = 0;
            if (this.IsEnableAagBuff == null)
            {
                goto Label_00C3;
            }
            attachment = this.CreateBuffAttachment(self, target, skill, targets, effect.param, 0, 0, 0, BuffAagWorkStatus, condition, num, targets2, timings, 0, num2);
            if (attachment == null)
            {
                goto Label_00B2;
            }
            attachment.AagTargetLists = at_lists;
        Label_00B2:
            if (target.SetBuffAttachment(attachment, 0) == null)
            {
                goto Label_00C3;
            }
            flag = 1;
        Label_00C3:
            if (this.IsEnableAagBuffNegative == null)
            {
                goto Label_0112;
            }
            attachment2 = this.CreateBuffAttachment(self, target, skill, targets, effect.param, 0, 1, 0, BuffAagNegativeWorkStatus, condition, num, targets2, timings, 0, num2);
            if (attachment2 == null)
            {
                goto Label_0101;
            }
            attachment2.AagTargetLists = at_lists;
        Label_0101:
            if (target.SetBuffAttachment(attachment2, 0) == null)
            {
                goto Label_0112;
            }
            flag = 1;
        Label_0112:
            if (this.IsEnableAagDebuff == null)
            {
                goto Label_0161;
            }
            attachment3 = this.CreateBuffAttachment(self, target, skill, targets, effect.param, 1, 0, 0, DebuffAagWorkStatus, condition, num, targets2, timings, 0, num2);
            if (attachment3 == null)
            {
                goto Label_0150;
            }
            attachment3.AagTargetLists = at_lists;
        Label_0150:
            if (target.SetBuffAttachment(attachment3, 0) == null)
            {
                goto Label_0161;
            }
            flag = 1;
        Label_0161:
            if (this.IsEnableAagDebuffNegative == null)
            {
                goto Label_01B0;
            }
            attachment4 = this.CreateBuffAttachment(self, target, skill, targets, effect.param, 1, 1, 0, DebuffAagNegativeWorkStatus, condition, num, targets2, timings, 0, num2);
            if (attachment4 == null)
            {
                goto Label_019F;
            }
            attachment4.AagTargetLists = at_lists;
        Label_019F:
            if (target.SetBuffAttachment(attachment4, 0) == null)
            {
                goto Label_01B0;
            }
            flag = 1;
        Label_01B0:
            if (log == null)
            {
                goto Label_0265;
            }
            if (flag == null)
            {
                goto Label_0265;
            }
            AagWorkStatus.Clear();
            AagWorkStatus.Add(BuffAagWorkStatus);
            AagWorkStatus.Add(BuffAagNegativeWorkStatus);
            AagWorkStatus.Add(DebuffAagWorkStatus);
            AagWorkStatus.Add(DebuffAagNegativeWorkStatus);
            bit = new BuffBit();
            bit2 = new BuffBit();
            SetBuffBits(AagWorkStatus, &bit, &bit2);
            target2 = log.FindTarget(target);
            if (target2 != null)
            {
                goto Label_0242;
            }
            target2 = log.self_effect;
            target2.target = target;
        Label_0242:
            if (target2 == null)
            {
                goto Label_0265;
            }
            bit.CopyTo(target2.buff);
            bit2.CopyTo(target2.debuff);
        Label_0265:
            return;
        }

        private void AbsorbAndGiveClear()
        {
            BuffAagWorkStatus.Clear();
            DebuffAagWorkStatus.Clear();
            BuffAagNegativeWorkStatus.Clear();
            DebuffAagNegativeWorkStatus.Clear();
            this.IsEnableAagBuff = 0;
            this.IsEnableAagDebuff = 0;
            this.IsEnableAagBuffNegative = 0;
            this.IsEnableAagDebuffNegative = 0;
            this.AagBuffAttachmentLists.Clear();
            this.AagTargetLists.Clear();
            return;
        }

        private void ActuatedSneaking(Unit unit)
        {
            if (unit.AI == null)
            {
                goto Label_001C;
            }
            if (unit.AI.CheckFlag(2) != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            unit.SetUnitFlag(0x100, 1);
            this.UpdateSearchMap(unit);
            if (this.CheckEnemyIntercept(unit) == null)
            {
                goto Label_0048;
            }
            unit.SetUnitFlag(0x100, 0);
        Label_0048:
            return;
        }

        private int AddGems(Unit unit, int gems)
        {
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_003A;
            }
            unit.Gems = Math.Max(Math.Min(unit.Gems + gems, unit.MaximumStatus.param.mp), 0);
        Label_003A:
            return unit.Gems;
        }

        public void AddReward(RewardType rewardType, string iname)
        {
            ArtifactParam param;
            ConceptCardParam param2;
            ItemParam param3;
            if (rewardType != 3)
            {
                goto Label_0034;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(iname);
            if (param == null)
            {
                goto Label_0095;
            }
            this.mRecord.artifacts.Add(param);
            goto Label_0095;
        Label_0034:
            if (rewardType != 6)
            {
                goto Label_006D;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
            if (param2 == null)
            {
                goto Label_0095;
            }
            this.mRecord.items.Add(new DropItemParam(param2));
            goto Label_0095;
        Label_006D:
            param3 = MonoSingleton<GameManager>.Instance.GetItemParam(iname);
            if (param3 == null)
            {
                goto Label_0095;
            }
            this.mRecord.items.Add(new DropItemParam(param3));
        Label_0095:
            return;
        }

        private void AddSkillExecLogForQuestMission(LogSkill log)
        {
            SkillExecLog local2;
            SkillExecLog local1;
            bool flag;
            int num;
            int num2;
            int num3;
            SkillExecLog log2;
            if (log.skill != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mQuestParam != null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if (this.mQuestParam.bonusObjective != null)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            flag = 0;
            num = 0;
            goto Label_0059;
        Label_0032:
            if (this.mQuestParam.bonusObjective[num].IsMissionTypeExecSkill() == null)
            {
                goto Label_004E;
            }
            goto Label_0055;
        Label_004E:
            flag = 1;
            goto Label_006C;
        Label_0055:
            num += 1;
        Label_0059:
            if (num < ((int) this.mQuestParam.bonusObjective.Length))
            {
                goto Label_0032;
            }
        Label_006C:
            if (flag != null)
            {
                goto Label_0073;
            }
            return;
        Label_0073:
            num2 = 0;
            num3 = 0;
            goto Label_00C5;
        Label_007C:
            if (log.targets[num3].target.Side == 1)
            {
                goto Label_009D;
            }
            goto Label_00C1;
        Label_009D:
            if (log.targets[num3].target.IsDead != null)
            {
                goto Label_00BD;
            }
            goto Label_00C1;
        Label_00BD:
            num2 += 1;
        Label_00C1:
            num3 += 1;
        Label_00C5:
            if (num3 < log.targets.Count)
            {
                goto Label_007C;
            }
            if (this.mSkillExecLogs.ContainsKey(log.skill.SkillID) != null)
            {
                goto Label_0137;
            }
            log2 = new SkillExecLog();
            log2.skill_iname = log.skill.SkillID;
            log2.use_count = 1;
            log2.kill_count = num2;
            this.mSkillExecLogs.Add(log.skill.SkillID, log2);
            goto Label_017D;
        Label_0137:
            local1 = this.mSkillExecLogs[log.skill.SkillID];
            local1.use_count += 1;
            local2 = this.mSkillExecLogs[log.skill.SkillID];
            local2.kill_count += num2;
        Label_017D:
            return;
        }

        public void ArenaCalcFinish()
        {
            this.mIsArenaCalc = 0;
            return;
        }

        public void ArenaCalcStart()
        {
            this.mArenaActionCount = this.mArenaActionCountMax;
            this.mIsArenaCalc = 1;
            this.mArenaCalcResult = 0;
            this.mArenaCalcTypeNext = 1;
            return;
        }

        public bool ArenaCalcStep()
        {
            int num;
            int num2;
            Unit unit;
            bool flag;
            eArenaCalcType type;
            num2 = 0;
        Label_0002:
            switch ((this.mArenaCalcTypeNext - 1))
            {
                case 0:
                    goto Label_0034;

                case 1:
                    goto Label_0046;

                case 2:
                    goto Label_0095;

                case 3:
                    goto Label_00C2;

                case 4:
                    goto Label_00D3;

                case 5:
                    goto Label_00E5;

                case 6:
                    goto Label_00F6;
            }
            goto Label_0128;
        Label_0034:
            this.MapStart();
            this.mArenaCalcTypeNext = 2;
            goto Label_016B;
        Label_0046:
            this.UnitStart();
            unit = this.CurrentUnit;
            if (unit == null)
            {
                goto Label_0089;
            }
            if (unit.CastSkill == null)
            {
                goto Label_0089;
            }
            if (unit.CastSkill.CastType != 2)
            {
                goto Label_0089;
            }
            this.CommandWait(0);
            this.mArenaCalcTypeNext = 4;
            goto Label_0090;
        Label_0089:
            this.mArenaCalcTypeNext = 3;
        Label_0090:
            goto Label_016B;
        Label_0095:
            this.IsAutoBattle = 1;
            flag = this.UpdateMapAI(0);
            this.IsAutoBattle = 0;
            if (flag == null)
            {
                goto Label_00B6;
            }
            goto Label_016B;
        Label_00B6:
            this.mArenaCalcTypeNext = 4;
            goto Label_016B;
        Label_00C2:
            this.UnitEnd();
            this.judgeStartTypeArenaCalc();
            goto Label_016B;
        Label_00D3:
            this.CastSkillStart();
            this.mArenaCalcTypeNext = 6;
            goto Label_016B;
        Label_00E5:
            this.CastSkillEnd();
            this.judgeStartTypeArenaCalc();
            goto Label_016B;
        Label_00F6:
            if (this.IsBattleFlag(2) == null)
            {
                goto Label_0108;
            }
            this.UnitEnd();
        Label_0108:
            if (this.IsBattleFlag(1) == null)
            {
                goto Label_011A;
            }
            this.MapEnd();
        Label_011A:
            this.mArenaCalcResult = this.GetQuestResult();
            return 1;
        Label_0128:
            DebugUtility.Log(string.Format("BattleCore/ArenaCalcStep Type unknown! type=", ((eArenaCalcType) this.mArenaCalcTypeNext).ToString()));
            if (this.IsBattleFlag(1) == null)
            {
                goto Label_015F;
            }
            this.mArenaCalcTypeNext = 7;
            goto Label_0166;
        Label_015F:
            this.mArenaCalcTypeNext = 1;
        Label_0166:;
        Label_016B:
            if (this.IsBattleFlag(1) == null)
            {
                goto Label_0182;
            }
            if (this.GetQuestResult() == null)
            {
                goto Label_0189;
            }
        Label_0182:
            this.mArenaCalcTypeNext = 7;
        Label_0189:
            if ((num2 += 1) < 0x40)
            {
                goto Label_0002;
            }
            return 0;
        }

        public void ArenaKeepQuestData(string quest_id, Json_Battle json_btl, int max_action_num)
        {
            this.mArenaQuestID = quest_id;
            this.mArenaQuestJsonBtl = json_btl;
            if (max_action_num <= 0)
            {
                goto Label_001C;
            }
            this.mArenaActionCountMax = max_action_num;
        Label_001C:
            return;
        }

        public bool ArenaResetQuestData()
        {
            this.mArenaActionCount = this.mArenaActionCountMax;
            if (string.IsNullOrEmpty(this.mArenaQuestID) != null)
            {
                goto Label_0027;
            }
            if (this.mArenaQuestJsonBtl != null)
            {
                goto Label_0029;
            }
        Label_0027:
            return 0;
        Label_0029:
            this.mMap.Clear();
            this.mPlayer.Clear();
            this.mAllUnits.Clear();
            this.mStartingMembers.Clear();
            this.Deserialize(this.mArenaQuestID, this.mArenaQuestJsonBtl, 0, null, null, 1, null, null);
            return 1;
        }

        private uint ArenaSubActionCount(uint count)
        {
            if (this.mArenaActionCount < count)
            {
                goto Label_001F;
            }
            this.mArenaActionCount -= count;
            goto Label_0026;
        Label_001F:
            this.mArenaActionCount = 0;
        Label_0026:
            return this.mArenaActionCount;
        }

        public static int Atan(int x)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = 0;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            goto Label_0029;
        Label_000D:
            num2 = -1 ^ ((num4 * ((1 / x) ^ ((2 * num4) + 1))) / ((2 * num4) + 1));
            num += num2;
            num4 += 1;
        Label_0029:
            if (num4 < 100)
            {
                goto Label_000D;
            }
            num3 = (0x2328 - ((180 * num) * 100)) / 0x13a;
            return num3;
        }

        private void AutoHealSkill(Unit self)
        {
            int num;
            LogAutoHeal heal;
            int num2;
            LogAutoHeal heal2;
            if (self.IsEnableAutoHealCondition() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = self.GetAutoHpHealValue();
            num = self.CalcParamRecover(num);
            if (num <= 0)
            {
                goto Label_009A;
            }
            heal = this.Log<LogAutoHeal>();
            heal.self = self;
            heal.type = 0;
            heal.value = num;
            heal.beforeHp = self.CurrentStatus.param.hp;
            self.Heal(num);
            if (self.IsPartyMember == null)
            {
                goto Label_009A;
            }
            this.mTotalHeal += Math.Max(self.CurrentStatus.param.hp - heal.beforeHp, 0);
        Label_009A:
            num2 = self.GetAutoMpHealValue();
            if (num2 <= 0)
            {
                goto Label_00E8;
            }
            heal2 = this.Log<LogAutoHeal>();
            heal2.self = self;
            heal2.type = 1;
            heal2.value = num2;
            heal2.beforeMp = self.CurrentStatus.param.mp;
            this.AddGems(self, num2);
        Label_00E8:
            return;
        }

        private unsafe void BeginBattlePassiveSkill()
        {
            int num;
            MyPhoton photon;
            List<JSON_MyPhotonPlayerParam> list;
            int num2;
            int num3;
            Unit unit;
            EquipData[] dataArray;
            int num4;
            EquipData data;
            int num5;
            ConceptCardData data2;
            List<SkillData> list2;
            SkillData data3;
            List<SkillData>.Enumerator enumerator;
            List<BuffEffect> list3;
            int num6;
            int num7;
            <BeginBattlePassiveSkill>c__AnonStorey1B6 storeyb;
            num = 0;
            goto Label_001C;
        Label_0007:
            this.mUnits[num].ClearPassiveBuffEffects();
            num += 1;
        Label_001C:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            if (this.IsMultiTower == null)
            {
                goto Label_00D7;
            }
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon != null) == null)
            {
                goto Label_00FB;
            }
            list = photon.GetMyPlayersStarted();
            if (list == null)
            {
                goto Label_00FB;
            }
            storeyb = new <BeginBattlePassiveSkill>c__AnonStorey1B6();
            storeyb.i = 0;
            goto Label_00C0;
        Label_006B:
            num2 = this.mPlayer.FindIndex(new Predicate<Unit>(storeyb.<>m__5C));
            if (num2 == -1)
            {
                goto Label_00B0;
            }
            this.InternalBattlePassiveSkill(this.mPlayer[num2], this.mPlayer[num2].LeaderSkill, 1, null);
        Label_00B0:
            storeyb.i += 1;
        Label_00C0:
            if (storeyb.i < list.Count)
            {
                goto Label_006B;
            }
            goto Label_00FB;
        Label_00D7:
            if (this.Leader == null)
            {
                goto Label_00FB;
            }
            this.InternalBattlePassiveSkill(this.Leader, this.Leader.LeaderSkill, 1, null);
        Label_00FB:
            if (this.Friend == null)
            {
                goto Label_012B;
            }
            if (this.mFriendStates != 1)
            {
                goto Label_012B;
            }
            this.InternalBattlePassiveSkill(this.Friend, this.Friend.LeaderSkill, 1, null);
        Label_012B:
            if (this.EnemyLeader == null)
            {
                goto Label_014F;
            }
            this.InternalBattlePassiveSkill(this.EnemyLeader, this.EnemyLeader.LeaderSkill, 1, null);
        Label_014F:
            num3 = 0;
            goto Label_02BB;
        Label_0157:
            unit = this.mAllUnits[num3];
            if (unit == null)
            {
                goto Label_02B5;
            }
            if (unit.IsDead == null)
            {
                goto Label_017E;
            }
            goto Label_02B5;
        Label_017E:
            if (unit.IsUnitFlag(0x1000000) == null)
            {
                goto Label_0194;
            }
            goto Label_02B5;
        Label_0194:
            dataArray = unit.CurrentEquips;
            if (dataArray == null)
            {
                goto Label_01F9;
            }
            num4 = 0;
            goto Label_01EE;
        Label_01AC:
            data = dataArray[num4];
            if (data == null)
            {
                goto Label_01E8;
            }
            if (data.IsValid() == null)
            {
                goto Label_01E8;
            }
            if (data.IsEquiped() != null)
            {
                goto Label_01D7;
            }
            goto Label_01E8;
        Label_01D7:
            this.InternalBattlePassiveSkill(unit, data.Skill, 0, null);
        Label_01E8:
            num4 += 1;
        Label_01EE:
            if (num4 < ((int) dataArray.Length))
            {
                goto Label_01AC;
            }
        Label_01F9:
            num5 = 0;
            goto Label_021F;
        Label_0201:
            this.InternalBattlePassiveSkill(unit, unit.BattleSkills[num5], 0, null);
            num5 += 1;
        Label_021F:
            if (num5 < unit.BattleSkills.Count)
            {
                goto Label_0201;
            }
            data2 = unit.UnitData.ConceptCard;
            if (data2 == null)
            {
                goto Label_02B5;
            }
            enumerator = data2.GetEnableCardSkills(unit.UnitData).GetEnumerator();
        Label_0260:
            try
            {
                goto Label_0297;
            Label_0265:
                data3 = &enumerator.Current;
                list3 = data2.GetEnableCardSkillAddBuffs(unit.UnitData, data3.SkillParam);
                this.InternalBattlePassiveSkill(unit, data3, 1, list3.ToArray());
            Label_0297:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0265;
                }
                goto Label_02B5;
            }
            finally
            {
            Label_02A8:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_02B5:
            num3 += 1;
        Label_02BB:
            if (num3 < this.mAllUnits.Count)
            {
                goto Label_0157;
            }
            num6 = 0;
            goto Label_02F7;
        Label_02D5:
            this.Player[num6].CalcCurrentStatus(this.mMapIndex == 0, 0);
            num6 += 1;
        Label_02F7:
            if (num6 < this.Player.Count)
            {
                goto Label_02D5;
            }
            num7 = 0;
            goto Label_032B;
        Label_0311:
            this.Enemys[num7].CalcCurrentStatus(1, 0);
            num7 += 1;
        Label_032B:
            if (num7 < this.Enemys.Count)
            {
                goto Label_0311;
            }
            return;
        }

        private unsafe void BeginBattlePassiveSkill(Unit unit)
        {
            EquipData[] dataArray;
            int num;
            EquipData data;
            int num2;
            ConceptCardData data2;
            List<SkillData> list;
            SkillData data3;
            List<SkillData>.Enumerator enumerator;
            List<BuffEffect> list2;
            int num3;
            Unit unit2;
            int num4;
            Unit unit3;
            if (unit == null)
            {
                goto Label_0011;
            }
            if (unit.IsDead == null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            dataArray = unit.CurrentEquips;
            if (dataArray == null)
            {
                goto Label_0067;
            }
            num = 0;
            goto Label_005E;
        Label_0026:
            data = dataArray[num];
            if (data == null)
            {
                goto Label_005A;
            }
            if (data.IsValid() == null)
            {
                goto Label_005A;
            }
            if (data.IsEquiped() != null)
            {
                goto Label_004B;
            }
            goto Label_005A;
        Label_004B:
            this.InternalBattlePassiveSkill(unit, data.Skill, 0, null);
        Label_005A:
            num += 1;
        Label_005E:
            if (num < ((int) dataArray.Length))
            {
                goto Label_0026;
            }
        Label_0067:
            num2 = 0;
            goto Label_0087;
        Label_006E:
            this.InternalBattlePassiveSkill(unit, unit.BattleSkills[num2], 0, null);
            num2 += 1;
        Label_0087:
            if (num2 < unit.BattleSkills.Count)
            {
                goto Label_006E;
            }
            data2 = unit.UnitData.ConceptCard;
            if (data2 == null)
            {
                goto Label_0123;
            }
            enumerator = data2.GetEnableCardSkills(unit.UnitData).GetEnumerator();
        Label_00C4:
            try
            {
                goto Label_0105;
            Label_00C9:
                data3 = &enumerator.Current;
                if (data3.IsSubActuate() != null)
                {
                    goto Label_0105;
                }
                list2 = data2.GetEnableCardSkillAddBuffs(unit.UnitData, data3.SkillParam);
                this.InternalBattlePassiveSkill(unit, data3, 1, list2.ToArray());
            Label_0105:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00C9;
                }
                goto Label_0123;
            }
            finally
            {
            Label_0116:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_0123:
            num3 = 0;
            goto Label_015C;
        Label_012B:
            unit2 = this.Player[num3];
            if (unit2 == null)
            {
                goto Label_0156;
            }
            if (unit2.IsDead != null)
            {
                goto Label_0156;
            }
            unit2.CalcCurrentStatus(0, 0);
        Label_0156:
            num3 += 1;
        Label_015C:
            if (num3 < this.Player.Count)
            {
                goto Label_012B;
            }
            num4 = 0;
            goto Label_01A7;
        Label_0176:
            unit3 = this.Enemys[num4];
            if (unit3 == null)
            {
                goto Label_01A1;
            }
            if (unit3.IsDead != null)
            {
                goto Label_01A1;
            }
            unit3.CalcCurrentStatus(0, 0);
        Label_01A1:
            num4 += 1;
        Label_01A7:
            if (num4 < this.Enemys.Count)
            {
                goto Label_0176;
            }
            return;
        }

        public Unit BreakObjCreate(string bo_id, int gx, int gy, Unit self, LogSkill log, int owner_index)
        {
            BreakObjParam param;
            NPCSetting setting;
            Unit unit;
            Grid grid;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetBreakObjParam(bo_id);
            setting = DownloadUtility.CreateBreakObjNPC(param, gx, gy);
            if (setting != null)
            {
                goto Label_0022;
            }
            return null;
        Label_0022:
            unit = new Unit();
            if (unit.Setup(null, setting, null, null) == null)
            {
                goto Label_0104;
            }
            unit.SetUnitFlag(0x40000, 1);
            unit.SetUnitFlag(0x200000, 1);
            unit.SetCreateBreakObj(bo_id, this.mClockTimeTotal);
            this.Enemys.Add(unit);
            this.mAllUnits.Add(unit);
            this.mUnits.Add(unit);
            if (param.AppearDir != 4)
            {
                goto Label_00AA;
            }
            if (self == null)
            {
                goto Label_00AA;
            }
            unit.Direction = self.Direction;
            goto Label_00B6;
        Label_00AA:
            unit.Direction = param.AppearDir;
        Label_00B6:
            grid = this.GetCorrectDuplicatePosition(unit);
            unit.x = grid.x;
            unit.y = grid.y;
            unit.OwnerPlayerIndex = owner_index;
            unit.SetUnitFlag(1, 1);
            if (self == null)
            {
                goto Label_010F;
            }
            if (log == null)
            {
                goto Label_010F;
            }
            log.SetSkillTarget(self, unit);
            goto Label_010F;
        Label_0104:
            this.DebugErr("BreakObjCreateForSkill: enemy unit setup failed");
        Label_010F:
            return unit;
        }

        private unsafe void BuffSkill(ESkillTiming timing, Unit self, Unit target, SkillData skill, bool is_passive, LogSkill log, SkillEffectTargets buff_target, bool is_duplicate, BuffEffect[] add_buff_effects)
        {
            BuffEffect effect;
            bool flag;
            bool flag2;
            bool flag3;
            int num;
            int num2;
            Unit unit;
            eAbsorbAndGive give;
            int num3;
            ESkillCondition condition;
            EffectCheckTargets targets;
            EffectCheckTimings timings;
            int num4;
            bool flag4;
            bool flag5;
            bool flag6;
            bool flag7;
            bool flag8;
            bool flag9;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            BaseStatus status6;
            BaseStatus status7;
            int num5;
            BuffAttachment attachment;
            BuffAttachment attachment2;
            BuffAttachment attachment3;
            BuffAttachment attachment4;
            BuffAttachment attachment5;
            BuffAttachment attachment6;
            BuffBit bit;
            BuffBit bit2;
            LogSkill.Target target2;
            SkillEffectTargets targets2;
            if (this.IsBattleFlag(6) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (timing == skill.Timing)
            {
                goto Label_001B;
            }
            return;
        Label_001B:
            BuffWorkStatus.Clear();
            DebuffWorkStatus.Clear();
            BuffNegativeWorkStatus.Clear();
            DebuffNegativeWorkStatus.Clear();
            BuffWorkScaleStatus.Clear();
            DebuffWorkScaleStatus.Clear();
            effect = skill.GetBuffEffect(buff_target);
            if (effect == null)
            {
                goto Label_0077;
            }
            if (effect.targets.Count != null)
            {
                goto Label_0078;
            }
        Label_0077:
            return;
        Label_0078:
            if (effect.CheckEnableBuffTarget(target) != null)
            {
                goto Label_0085;
            }
            return;
        Label_0085:
            flag = 0;
            flag2 = 1;
            flag3 = 1;
            if (skill.IsPassiveSkill() != null)
            {
                goto Label_01AB;
            }
            if (target.IsEnableBuffEffect(0) != null)
            {
                goto Label_00BA;
            }
            if (effect.param.IsNoDisabled != null)
            {
                goto Label_00BA;
            }
            flag2 = 0;
            goto Label_0121;
        Label_00BA:
            if (target.CurrentStatus.enchant_resist.resist_buff <= 0)
            {
                goto Label_0121;
            }
            if (target.CurrentStatus.enchant_resist.resist_buff >= 100)
            {
                goto Label_011F;
            }
            num = this.GetRandom() % 100;
            if (num >= target.CurrentStatus.enchant_resist.resist_buff)
            {
                goto Label_0121;
            }
            flag2 = 0;
            goto Label_0121;
        Label_011F:
            flag2 = 0;
        Label_0121:
            if (target.IsEnableBuffEffect(1) != null)
            {
                goto Label_0144;
            }
            if (effect.param.IsNoDisabled != null)
            {
                goto Label_0144;
            }
            flag3 = 0;
            goto Label_01AB;
        Label_0144:
            if (target.CurrentStatus.enchant_resist.resist_debuff <= 0)
            {
                goto Label_01AB;
            }
            if (target.CurrentStatus.enchant_resist.resist_buff >= 100)
            {
                goto Label_01A9;
            }
            num2 = this.GetRandom() % 100;
            if (num2 >= target.CurrentStatus.enchant_resist.resist_debuff)
            {
                goto Label_01AB;
            }
            flag3 = 0;
            goto Label_01AB;
        Label_01A9:
            flag2 = 0;
        Label_01AB:
            unit = target;
            give = skill.SkillParam.AbsorbAndGive;
            if (give == null)
            {
                goto Label_0200;
            }
            if (buff_target != null)
            {
                goto Label_01FD;
            }
            if (self == target)
            {
                goto Label_01FD;
            }
            if (SkillParam.IsAagTypeGive(give) == null)
            {
                goto Label_0200;
            }
            if (this.IsEnableAag == null)
            {
                goto Label_01F5;
            }
            this.AagTargetLists.Add(target);
            return;
        Label_01F5:
            unit = self;
            goto Label_0200;
        Label_01FD:
            give = 0;
        Label_0200:
            if (skill.BuffSkill(timing, unit.Element, BuffWorkStatus, BuffNegativeWorkStatus, BuffWorkScaleStatus, DebuffWorkStatus, DebuffNegativeWorkStatus, DebuffWorkScaleStatus, this.CurrentRand, buff_target, 0, null) != null)
            {
                goto Label_023D;
            }
            return;
        Label_023D:
            num3 = effect.param.turn;
            condition = effect.param.cond;
            targets = effect.param.chk_target;
            timings = effect.param.chk_timing;
            num4 = skill.DuplicateCount;
            flag4 = effect.CheckBuffCalcType(0, 0, 0);
            flag5 = effect.CheckBuffCalcType(0, 0, 1);
            flag6 = effect.CheckBuffCalcType(0, 1);
            flag7 = effect.CheckBuffCalcType(1, 0, 0);
            flag8 = effect.CheckBuffCalcType(1, 0, 1);
            flag9 = effect.CheckBuffCalcType(1, 1);
            if (give == null)
            {
                goto Label_0310;
            }
            status = unit.UnitData.Status;
            if (flag6 == null)
            {
                goto Label_02F2;
            }
            BuffWorkStatus.AddConvRate(BuffWorkScaleStatus, status);
            flag4 = 1;
            flag6 = 0;
        Label_02F2:
            if (flag9 == null)
            {
                goto Label_0310;
            }
            DebuffWorkStatus.AddConvRate(DebuffWorkScaleStatus, status);
            flag7 = 1;
            flag9 = 0;
        Label_0310:
            if (skill.Condition != 4)
            {
                goto Label_04DC;
            }
            if (add_buff_effects == null)
            {
                goto Label_04DC;
            }
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            status5 = new BaseStatus();
            status6 = new BaseStatus();
            status7 = new BaseStatus();
            num5 = 0;
            goto Label_04D1;
        Label_0356:
            if (add_buff_effects[num5] != null)
            {
                goto Label_0365;
            }
            goto Label_04CB;
        Label_0365:
            status2.Clear();
            status3.Clear();
            status4.Clear();
            status5.Clear();
            status6.Clear();
            status7.Clear();
            add_buff_effects[num5].CalcBuffStatus(&status2, target.Element, 0, 1, 0, 0, 0);
            add_buff_effects[num5].CalcBuffStatus(&status3, target.Element, 0, 1, 1, 0, 0);
            add_buff_effects[num5].CalcBuffStatus(&status4, target.Element, 0, 0, 0, 1, 0);
            add_buff_effects[num5].CalcBuffStatus(&status5, target.Element, 1, 1, 0, 0, 0);
            add_buff_effects[num5].CalcBuffStatus(&status6, target.Element, 1, 1, 1, 0, 0);
            add_buff_effects[num5].CalcBuffStatus(&status7, target.Element, 1, 0, 0, 1, 0);
            flag4 |= add_buff_effects[num5].CheckBuffCalcType(0, 0, 0);
            flag5 |= add_buff_effects[num5].CheckBuffCalcType(0, 0, 1);
            flag6 |= add_buff_effects[num5].CheckBuffCalcType(0, 1);
            flag7 |= add_buff_effects[num5].CheckBuffCalcType(1, 0, 0);
            flag8 |= add_buff_effects[num5].CheckBuffCalcType(1, 0, 1);
            flag9 |= add_buff_effects[num5].CheckBuffCalcType(1, 1);
            BuffWorkStatus.Add(status2);
            BuffNegativeWorkStatus.Add(status3);
            BuffWorkScaleStatus.Add(status4);
            DebuffWorkStatus.Add(status5);
            DebuffNegativeWorkStatus.Add(status6);
            DebuffWorkScaleStatus.Add(status7);
        Label_04CB:
            num5 += 1;
        Label_04D1:
            if (num5 < ((int) add_buff_effects.Length))
            {
                goto Label_0356;
            }
        Label_04DC:
            if (flag2 == null)
            {
                goto Label_064A;
            }
            if (flag4 == null)
            {
                goto Label_0575;
            }
            attachment = this.CreateBuffAttachment(self, unit, skill, buff_target, effect.param, 0, 0, 0, BuffWorkStatus, condition, num3, targets, timings, is_passive, num4);
            if (unit.SetBuffAttachment(attachment, is_duplicate) == null)
            {
                goto Label_0575;
            }
            flag = 1;
            if (give == null)
            {
                goto Label_0575;
            }
            this.AagBuffAttachmentLists.Add(attachment);
            if (SkillParam.IsAagTypeSame(give) == null)
            {
                goto Label_055F;
            }
            BuffAagWorkStatus.Add(BuffWorkStatus);
            this.IsEnableAagBuff = 1;
            goto Label_0575;
        Label_055F:
            DebuffAagWorkStatus.Sub(BuffWorkStatus);
            this.IsEnableAagDebuff = 1;
        Label_0575:
            if (flag5 == null)
            {
                goto Label_0608;
            }
            attachment2 = this.CreateBuffAttachment(self, unit, skill, buff_target, effect.param, 0, 1, 0, BuffNegativeWorkStatus, condition, num3, targets, timings, is_passive, num4);
            if (unit.SetBuffAttachment(attachment2, is_duplicate) == null)
            {
                goto Label_0608;
            }
            flag = 1;
            if (give == null)
            {
                goto Label_0608;
            }
            this.AagBuffAttachmentLists.Add(attachment2);
            if (SkillParam.IsAagTypeSame(give) == null)
            {
                goto Label_05F2;
            }
            BuffAagNegativeWorkStatus.Add(BuffNegativeWorkStatus);
            this.IsEnableAagBuffNegative = 1;
            goto Label_0608;
        Label_05F2:
            DebuffAagNegativeWorkStatus.Sub(BuffNegativeWorkStatus);
            this.IsEnableAagDebuffNegative = 1;
        Label_0608:
            if (flag6 == null)
            {
                goto Label_064A;
            }
            attachment3 = this.CreateBuffAttachment(self, unit, skill, buff_target, effect.param, 0, 0, 1, BuffWorkScaleStatus, condition, num3, targets, timings, is_passive, num4);
            if (unit.SetBuffAttachment(attachment3, is_duplicate) == null)
            {
                goto Label_064A;
            }
            flag = 1;
        Label_064A:
            if (flag3 == null)
            {
                goto Label_07B8;
            }
            if (flag7 == null)
            {
                goto Label_06E3;
            }
            attachment4 = this.CreateBuffAttachment(self, unit, skill, buff_target, effect.param, 1, 0, 0, DebuffWorkStatus, condition, num3, targets, timings, is_passive, num4);
            if (unit.SetBuffAttachment(attachment4, is_duplicate) == null)
            {
                goto Label_06E3;
            }
            flag = 1;
            if (give == null)
            {
                goto Label_06E3;
            }
            this.AagBuffAttachmentLists.Add(attachment4);
            if (SkillParam.IsAagTypeSame(give) == null)
            {
                goto Label_06CD;
            }
            DebuffAagWorkStatus.Add(DebuffWorkStatus);
            this.IsEnableAagDebuff = 1;
            goto Label_06E3;
        Label_06CD:
            BuffAagWorkStatus.Sub(DebuffWorkStatus);
            this.IsEnableAagBuff = 1;
        Label_06E3:
            if (flag8 == null)
            {
                goto Label_0776;
            }
            attachment5 = this.CreateBuffAttachment(self, unit, skill, buff_target, effect.param, 1, 1, 0, DebuffNegativeWorkStatus, condition, num3, targets, timings, is_passive, num4);
            if (unit.SetBuffAttachment(attachment5, is_duplicate) == null)
            {
                goto Label_0776;
            }
            flag = 1;
            if (give == null)
            {
                goto Label_0776;
            }
            this.AagBuffAttachmentLists.Add(attachment5);
            if (SkillParam.IsAagTypeSame(give) == null)
            {
                goto Label_0760;
            }
            DebuffAagNegativeWorkStatus.Add(DebuffNegativeWorkStatus);
            this.IsEnableAagDebuffNegative = 1;
            goto Label_0776;
        Label_0760:
            BuffAagNegativeWorkStatus.Sub(DebuffNegativeWorkStatus);
            this.IsEnableAagBuffNegative = 1;
        Label_0776:
            if (flag9 == null)
            {
                goto Label_07B8;
            }
            attachment6 = this.CreateBuffAttachment(self, unit, skill, buff_target, effect.param, 1, 0, 1, DebuffWorkScaleStatus, condition, num3, targets, timings, is_passive, num4);
            if (unit.SetBuffAttachment(attachment6, is_duplicate) == null)
            {
                goto Label_07B8;
            }
            flag = 1;
        Label_07B8:
            if (flag != null)
            {
                goto Label_07BF;
            }
            return;
        Label_07BF:
            if (give == null)
            {
                goto Label_07D2;
            }
            this.AagTargetLists.Add(target);
        Label_07D2:
            if (log == null)
            {
                goto Label_08D2;
            }
            BuffWorkStatus.Add(BuffNegativeWorkStatus);
            BuffWorkStatus.Add(DebuffWorkStatus);
            BuffWorkStatus.Add(DebuffNegativeWorkStatus);
            BuffWorkScaleStatus.Add(DebuffWorkScaleStatus);
            bit = new BuffBit();
            bit2 = new BuffBit();
            SetBuffBits(BuffWorkStatus, &bit, &bit2);
            SetBuffBits(BuffWorkScaleStatus, &bit, &bit2);
            target2 = null;
            targets2 = buff_target;
            if (targets2 == null)
            {
                goto Label_085A;
            }
            if (targets2 == 1)
            {
                goto Label_0890;
            }
            goto Label_08AF;
        Label_085A:
            if (give == null)
            {
                goto Label_0880;
            }
            if (self != unit)
            {
                goto Label_0880;
            }
            target2 = log.self_effect;
            target2.target = unit;
            goto Label_088B;
        Label_0880:
            target2 = log.FindTarget(unit);
        Label_088B:
            goto Label_08AF;
        Label_0890:
            if (self != unit)
            {
                goto Label_08AF;
            }
            target2 = log.self_effect;
            target2.target = unit;
        Label_08AF:
            if (target2 == null)
            {
                goto Label_08D2;
            }
            bit.CopyTo(target2.buff);
            bit2.CopyTo(target2.debuff);
        Label_08D2:
            return;
        }

        private unsafe int CalcAtkPointSkill(Unit attacker, Unit defender, SkillData skill, LogSkill log)
        {
            int num;
            int num2;
            Unit unit;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            FixParam param;
            EnchantParam param2;
            EnchantParam param3;
            ElementParam param4;
            EnchantParam param5;
            EnchantParam param6;
            EElement element;
            ElementParam param7;
            LogSkill.Target target;
            string str;
            string[] strArray;
            int num14;
            BuffAttachment attachment;
            List<BuffAttachment>.Enumerator enumerator;
            string[] strArray2;
            List<string> list;
            Grid grid;
            Grid grid2;
            int num15;
            JobData data;
            List<ArtifactData> list2;
            ArtifactData data2;
            int num16;
            ArtifactData data3;
            string str2;
            string[] strArray3;
            int num17;
            int num18;
            <CalcAtkPointSkill>c__AnonStorey1C9 storeyc;
            EElement element2;
            storeyc = new <CalcAtkPointSkill>c__AnonStorey1C9();
            storeyc.defender = defender;
            num = this.CalcAtkPointSkillBase(attacker, storeyc.defender, skill);
            num2 = num;
            if (skill.IsCollabo == null)
            {
                goto Label_0080;
            }
            unit = attacker.GetUnitUseCollaboSkill(skill, 0);
            if (unit == null)
            {
                goto Label_005B;
            }
            num3 = this.CalcAtkPointSkillBase(unit, storeyc.defender, skill);
            num = (num + num3) / 2;
            goto Label_0080;
        Label_005B:
            DebugUtility.LogWarning(string.Format("BattleCore/CalcAtkPointSkill collabo unit not found! unit_iname={0}, skill_iname={1}", attacker.UnitParam.iname, skill.SkillParam.iname));
        Label_0080:
            num4 = this.GetSkillEffectValue(attacker, storeyc.defender, skill, log);
            num = SkillParam.CalcSkillEffectValue(skill.EffectCalcType, num4, num);
            if (skill.IsSuicide() == null)
            {
                goto Label_00C7;
            }
            num += attacker.CurrentStatus.param.hp / 2;
        Label_00C7:
            num5 = 0;
            num6 = 0;
            num7 = 0;
            num8 = 0;
            num9 = 0;
            num10 = 0;
            num11 = 0;
            num12 = 0;
            num13 = 0;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            if (num == null)
            {
                goto Label_09F6;
            }
            num5 = this.GetAtkBonusForAttackDetailType(attacker, skill);
            if (storeyc.defender == null)
            {
                goto Label_09A7;
            }
            param2 = attacker.CurrentStatus.enchant_assist;
            param3 = storeyc.defender.CurrentStatus.enchant_resist;
            if (skill.IsAreaSkill() != null)
            {
                goto Label_0161;
            }
            num6 += param2[0x19] - param3[0x19];
            goto Label_0183;
        Label_0161:
            num6 += param2[0x1a] - param3[0x1a];
        Label_0183:
            if (skill.IsIgnoreElement() != null)
            {
                goto Label_054B;
            }
            if (attacker.Element == null)
            {
                goto Label_0266;
            }
            param4 = attacker.CurrentStatus.element_assist;
            num7 += param4[attacker.Element];
            if (attacker.Element != UnitParam.GetWeakElement(storeyc.defender.Element))
            {
                goto Label_01EF;
            }
            num7 += param.WeakUpRate;
            goto Label_021C;
        Label_01EF:
            if (attacker.Element != UnitParam.GetResistElement(storeyc.defender.Element))
            {
                goto Label_021C;
            }
            num7 += param.ResistDownRate;
        Label_021C:
            num12 += (UnitParam.GetWeakElement(storeyc.defender.Element) != attacker.Element) ? ((UnitParam.GetResistElement(storeyc.defender.Element) != attacker.Element) ? 0 : 1) : -1;
        Label_0266:
            if (skill.ElementType == null)
            {
                goto Label_0507;
            }
            num7 += skill.ElementValue;
            if (skill.ElementType != UnitParam.GetWeakElement(storeyc.defender.Element))
            {
                goto Label_02B3;
            }
            num7 += param.WeakUpRate;
            goto Label_02E0;
        Label_02B3:
            if (skill.ElementType != UnitParam.GetResistElement(storeyc.defender.Element))
            {
                goto Label_02E0;
            }
            num7 += param.ResistDownRate;
        Label_02E0:
            num12 += (UnitParam.GetWeakElement(storeyc.defender.Element) != skill.ElementType) ? ((UnitParam.GetResistElement(storeyc.defender.Element) != skill.ElementType) ? 0 : 1) : -1;
            if (skill.ElementType != UnitParam.GetWeakElement(storeyc.defender.Element))
            {
                goto Label_0507;
            }
            num8 = skill.ElementSpcAtkRate;
            param5 = attacker.CurrentStatus.enchant_assist;
            param6 = storeyc.defender.CurrentStatus.enchant_resist;
            switch ((storeyc.defender.Element - 1))
            {
                case 0:
                    goto Label_03A2;

                case 1:
                    goto Label_03DC;

                case 2:
                    goto Label_0416;

                case 3:
                    goto Label_0450;

                case 4:
                    goto Label_048D;

                case 5:
                    goto Label_04CA;
            }
            goto Label_0507;
        Label_03A2:
            if (storeyc.defender.IsDisableUnitCondition(0x20000000L) != null)
            {
                goto Label_0507;
            }
            num8 += param5.esa_fire - param6.esa_fire;
            goto Label_0507;
        Label_03DC:
            if (storeyc.defender.IsDisableUnitCondition(0x40000000L) != null)
            {
                goto Label_0507;
            }
            num8 += param5.esa_water - param6.esa_water;
            goto Label_0507;
        Label_0416:
            if (storeyc.defender.IsDisableUnitCondition(0x80000000L) != null)
            {
                goto Label_0507;
            }
            num8 += param5.esa_wind - param6.esa_wind;
            goto Label_0507;
        Label_0450:
            if (storeyc.defender.IsDisableUnitCondition(0x100000000L) != null)
            {
                goto Label_0507;
            }
            num8 += param5.esa_thunder - param6.esa_thunder;
            goto Label_0507;
        Label_048D:
            if (storeyc.defender.IsDisableUnitCondition(0x200000000L) != null)
            {
                goto Label_0507;
            }
            num8 += param5.esa_shine - param6.esa_shine;
            goto Label_0507;
        Label_04CA:
            if (storeyc.defender.IsDisableUnitCondition(0x400000000L) != null)
            {
                goto Label_0507;
            }
            num8 += param5.esa_dark - param6.esa_dark;
        Label_0507:
            element = skill.ElementType;
            if (element != null)
            {
                goto Label_051E;
            }
            element = attacker.Element;
        Label_051E:
            if (element == null)
            {
                goto Label_054B;
            }
            param7 = storeyc.defender.CurrentStatus.element_resist;
            num7 -= param7[element];
        Label_054B:
            if (log == null)
            {
                goto Label_0592;
            }
            if (log.targets == null)
            {
                goto Label_0592;
            }
            target = log.targets.Find(new Predicate<LogSkill.Target>(storeyc.<>m__70));
            if (target == null)
            {
                goto Label_0592;
            }
            target.element_effect_rate = num7;
            target.element_effect_resist = num12;
        Label_0592:
            str = skill.SkillParam.tokkou;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_063E;
            }
            strArray = storeyc.defender.GetTags();
            if (strArray == null)
            {
                goto Label_063E;
            }
            num14 = 0;
            goto Label_0633;
        Label_05C8:
            if (string.IsNullOrEmpty(strArray[num14]) == null)
            {
                goto Label_05DC;
            }
            goto Label_062D;
        Label_05DC:
            if ((str != strArray[num14]) == null)
            {
                goto Label_05F2;
            }
            goto Label_062D;
        Label_05F2:
            if (skill.SkillParam.tk_rate == null)
            {
                goto Label_0617;
            }
            num9 += skill.SkillParam.tk_rate;
            goto Label_0628;
        Label_0617:
            num9 += param.TokkouDamageRate;
        Label_0628:
            goto Label_063E;
        Label_062D:
            num14 += 1;
        Label_0633:
            if (num14 < ((int) strArray.Length))
            {
                goto Label_05C8;
            }
        Label_063E:
            enumerator = attacker.BuffAttachments.GetEnumerator();
        Label_064B:
            try
            {
                goto Label_070C;
            Label_0650:
                attachment = &enumerator.Current;
                if (attachment.skill != null)
                {
                    goto Label_066A;
                }
                goto Label_070C;
            Label_066A:
                if (string.IsNullOrEmpty(attachment.skill.SkillParam.tokkou) == null)
                {
                    goto Label_068A;
                }
                goto Label_070C;
            Label_068A:
                strArray2 = storeyc.defender.GetTags();
                if (strArray2 != null)
                {
                    goto Label_06A4;
                }
                goto Label_070C;
            Label_06A4:
                list = new List<string>(strArray2);
                if (list.Contains(attachment.skill.SkillParam.tokkou) == null)
                {
                    goto Label_070C;
                }
                if (attachment.skill.SkillParam.tk_rate == null)
                {
                    goto Label_06FB;
                }
                num9 += attachment.skill.SkillParam.tk_rate;
                goto Label_070C;
            Label_06FB:
                num9 += param.TokkouDamageRate;
            Label_070C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0650;
                }
                goto Label_072A;
            }
            finally
            {
            Label_071D:
                ((List<BuffAttachment>.Enumerator) enumerator).Dispose();
            }
        Label_072A:
            if (skill.IsEnableHeightParamAdjust() == null)
            {
                goto Label_07B2;
            }
            grid = this.GetUnitGridPosition(attacker);
            grid2 = this.GetUnitGridPosition(storeyc.defender);
            if (grid == null)
            {
                goto Label_07B2;
            }
            if (grid2 == null)
            {
                goto Label_07B2;
            }
            num15 = grid.height - grid2.height;
            if (num15 <= 0)
            {
                goto Label_078F;
            }
            num10 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.HighGridAtkRate;
        Label_078F:
            if (num15 >= 0)
            {
                goto Label_07B2;
            }
            num10 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DownGridAtkRate;
        Label_07B2:
            if (skill.IsNormalAttack() == null)
            {
                goto Label_0983;
            }
            data = attacker.Job;
            if (data == null)
            {
                goto Label_0983;
            }
            if (data.ArtifactDatas != null)
            {
                goto Label_07E9;
            }
            if (string.IsNullOrEmpty(data.SelectedSkin) != null)
            {
                goto Label_0983;
            }
        Label_07E9:
            list2 = new List<ArtifactData>();
            if (data.ArtifactDatas == null)
            {
                goto Label_0819;
            }
            if (((int) data.ArtifactDatas.Length) < 1)
            {
                goto Label_0819;
            }
            list2.AddRange(data.ArtifactDatas);
        Label_0819:
            if (string.IsNullOrEmpty(data.SelectedSkin) != null)
            {
                goto Label_0843;
            }
            data2 = data.GetSelectedSkinData();
            if (data2 == null)
            {
                goto Label_0843;
            }
            list2.Add(data2);
        Label_0843:
            num16 = 0;
            goto Label_0975;
        Label_084B:
            data3 = list2[num16];
            if (data3 != null)
            {
                goto Label_0862;
            }
            goto Label_096F;
        Label_0862:
            if (data3.ArtifactParam != null)
            {
                goto Label_0873;
            }
            goto Label_096F;
        Label_0873:
            if (data3.ArtifactParam.type == 1)
            {
                goto Label_088A;
            }
            goto Label_096F;
        Label_088A:
            if (data3.BattleEffectSkill != null)
            {
                goto Label_089B;
            }
            goto Label_096F;
        Label_089B:
            if (data3.BattleEffectSkill.SkillParam != null)
            {
                goto Label_08B1;
            }
            goto Label_096F;
        Label_08B1:
            str2 = data3.BattleEffectSkill.SkillParam.tokkou;
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_096F;
            }
            strArray3 = storeyc.defender.GetTags();
            if (strArray3 == null)
            {
                goto Label_096F;
            }
            num17 = 0;
            goto Label_0964;
        Label_08ED:
            if (string.IsNullOrEmpty(strArray3[num17]) == null)
            {
                goto Label_0901;
            }
            goto Label_095E;
        Label_0901:
            if ((str2 != strArray3[num17]) == null)
            {
                goto Label_0917;
            }
            goto Label_095E;
        Label_0917:
            if (data3.BattleEffectSkill.SkillParam.tk_rate == null)
            {
                goto Label_0948;
            }
            num9 += data3.BattleEffectSkill.SkillParam.tk_rate;
            goto Label_0959;
        Label_0948:
            num9 += param.TokkouDamageRate;
        Label_0959:
            goto Label_096F;
        Label_095E:
            num17 += 1;
        Label_0964:
            if (num17 < ((int) strArray3.Length))
            {
                goto Label_08ED;
            }
        Label_096F:
            num16 += 1;
        Label_0975:
            if (num16 < list2.Count)
            {
                goto Label_084B;
            }
        Label_0983:
            if (skill.JumpSpcAtkRate == null)
            {
                goto Label_09A7;
            }
            if (storeyc.defender.IsJump == null)
            {
                goto Label_09A7;
            }
            num13 = skill.JumpSpcAtkRate;
        Label_09A7:
            if (this.IsCombinationAttack(skill) == null)
            {
                goto Label_09CD;
            }
            num11 = this.mHelperUnits.Count * param.CombinationRate;
        Label_09CD:
            num18 = ((((((num5 + num7) + num8) + num9) + num10) + num11) + num6) + num13;
            num += ((100 * num) * num18) / 0x2710;
        Label_09F6:
            return num;
        }

        private int CalcAtkPointSkillBase(Unit attacker, Unit defender, SkillData skill)
        {
            int num;
            int num2;
            int num3;
            int num4;
            StatusParam param;
            StatusParam param2;
            WeaponParam param3;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            WeaponFormulaTypes types;
            num = 0;
            num2 = 1;
            num3 = 1;
            num4 = 1;
            param = attacker.CurrentStatus.param;
            param2 = (defender == null) ? null : defender.CurrentStatus.param;
            if (string.IsNullOrEmpty(skill.SkillParam.weapon) != null)
            {
                goto Label_0845;
            }
            param3 = MonoSingleton<GameManager>.Instance.GetWeaponParam(skill.SkillParam.weapon);
            switch ((param3.FormulaType - 1))
            {
                case 0:
                    goto Label_00E5;

                case 1:
                    goto Label_010D;

                case 2:
                    goto Label_0135;

                case 3:
                    goto Label_016A;

                case 4:
                    goto Label_019F;

                case 5:
                    goto Label_01D4;

                case 6:
                    goto Label_0209;

                case 7:
                    goto Label_023E;

                case 8:
                    goto Label_0273;

                case 9:
                    goto Label_02A8;

                case 10:
                    goto Label_0313;

                case 11:
                    goto Label_0379;

                case 12:
                    goto Label_03D6;

                case 13:
                    goto Label_0433;

                case 14:
                    goto Label_0479;

                case 15:
                    goto Label_04BF;

                case 0x10:
                    goto Label_04E7;

                case 0x11:
                    goto Label_050F;

                case 0x12:
                    goto Label_0537;

                case 0x13:
                    goto Label_055F;

                case 20:
                    goto Label_0587;

                case 0x15:
                    goto Label_05AF;

                case 0x16:
                    goto Label_0616;

                case 0x17:
                    goto Label_067D;

                case 0x18:
                    goto Label_06C2;

                case 0x19:
                    goto Label_0707;

                case 0x1a:
                    goto Label_0757;

                case 0x1b:
                    goto Label_07A7;

                case 0x1c:
                    goto Label_07EC;
            }
            goto Label_082E;
        Label_00E5:
            num = (param3.atk * ((100 * param.atk) / 10)) / 100;
            goto Label_0840;
        Label_010D:
            num = (param3.atk * ((100 * param.mag) / 10)) / 100;
            goto Label_0840;
        Label_0135:
            num = ((param3.atk * (100 * (param.atk + param.spd))) / 15) / 100;
            goto Label_0840;
        Label_016A:
            num = ((param3.atk * (100 * (param.mag + param.spd))) / 15) / 100;
            goto Label_0840;
        Label_019F:
            num = ((param3.atk * (100 * (param.atk + param.dex))) / 20) / 100;
            goto Label_0840;
        Label_01D4:
            num = ((param3.atk * (100 * (param.mag + param.dex))) / 20) / 100;
            goto Label_0840;
        Label_0209:
            num = ((param3.atk * (100 * (param.atk + param.luk))) / 20) / 100;
            goto Label_0840;
        Label_023E:
            num = ((param3.atk * (100 * (param.mag + param.luk))) / 20) / 100;
            goto Label_0840;
        Label_0273:
            num = ((param3.atk * (100 * (param.atk + param.mag))) / 20) / 100;
            goto Label_0840;
        Label_02A8:
            if (param.atk <= 0)
            {
                goto Label_02D3;
            }
            num2 += (int) (((ulong) this.GetRandom()) % ((long) param.atk));
        Label_02D3:
            num = ((param3.atk * ((100 * param.atk) / 10)) * (50 + ((100 * num2) / param.atk))) / 0x2710;
            goto Label_0840;
        Label_0313:
            num5 = 0;
            if (param2 == null)
            {
                goto Label_032B;
            }
            num5 = param2.mnd;
        Label_032B:
            num = ((param3.atk * ((100 * param.mag) / 10)) * (20 + ((100 / (param.mag + num5)) * param.mag))) / 0x2710;
            goto Label_0840;
        Label_0379:
            num = ((param3.atk * (100 * (((param.atk + (param.spd / 2)) + ((param.spd * attacker.Lv) / 100)) + (param.dex / 4)))) / 20) / 100;
            goto Label_0840;
        Label_03D6:
            num = ((param3.atk * (100 * (((param.mag + (param.spd / 2)) + ((param.spd * attacker.Lv) / 100)) + (param.dex / 4)))) / 20) / 100;
            goto Label_0840;
        Label_0433:
            num = ((param3.atk * (100 * ((param.atk + (param.dex / 2)) + (param.luk / 2)))) / 20) / 100;
            goto Label_0840;
        Label_0479:
            num = ((param3.atk * (100 * ((param.mag + (param.dex / 2)) + (param.luk / 2)))) / 20) / 100;
            goto Label_0840;
        Label_04BF:
            num = (param3.atk * ((100 * param.luk) / 10)) / 100;
            goto Label_0840;
        Label_04E7:
            num = (param3.atk * ((100 * param.dex) / 10)) / 100;
            goto Label_0840;
        Label_050F:
            num = (param3.atk * ((100 * param.spd) / 10)) / 100;
            goto Label_0840;
        Label_0537:
            num = (param3.atk * ((100 * param.cri) / 10)) / 100;
            goto Label_0840;
        Label_055F:
            num = (param3.atk * ((100 * param.def) / 10)) / 100;
            goto Label_0840;
        Label_0587:
            num = (param3.atk * ((100 * param.mnd) / 10)) / 100;
            goto Label_0840;
        Label_05AF:
            num6 = Mathf.CeilToInt(((float) attacker.Lv) / 10f);
            if (num6 > 0)
            {
                goto Label_05CE;
            }
            num6 = 1;
        Label_05CE:
            num3 += (int) (((ulong) this.GetRandom()) % ((long) num6));
            num = (param3.atk * ((100 * (param.atk + (num3 * (param.luk / 3)))) / 20)) / 100;
            goto Label_0840;
        Label_0616:
            num7 = Mathf.CeilToInt(((float) attacker.Lv) / 10f);
            if (num7 > 0)
            {
                goto Label_0635;
            }
            num7 = 1;
        Label_0635:
            num4 += (int) (((ulong) this.GetRandom()) % ((long) num7));
            num = (param3.atk * ((100 * (param.mag + (num4 * (param.luk / 3)))) / 20)) / 100;
            goto Label_0840;
        Label_067D:
            num8 = 0;
            if (param2 == null)
            {
                goto Label_0695;
            }
            num8 = param2.atk;
        Label_0695:
            num = (param3.atk * ((100 * ((param.atk * 2) - num8)) / 10)) / 100;
            goto Label_0840;
        Label_06C2:
            num9 = 0;
            if (param2 == null)
            {
                goto Label_06DA;
            }
            num9 = param2.mag;
        Label_06DA:
            num = (param3.atk * ((100 * ((param.mag * 2) - num9)) / 10)) / 100;
            goto Label_0840;
        Label_0707:
            num10 = 0;
            if (param2 == null)
            {
                goto Label_071F;
            }
            num10 = param2.def;
        Label_071F:
            num = (param3.atk * ((100 * ((param.atk + param.def) - num10)) / 10)) / 100;
            goto Label_0840;
        Label_0757:
            num11 = 0;
            if (param2 == null)
            {
                goto Label_076F;
            }
            num11 = param2.mnd;
        Label_076F:
            num = (param3.atk * ((100 * ((param.mag + param.mnd) - num11)) / 10)) / 100;
            goto Label_0840;
        Label_07A7:
            num12 = 0;
            if (param2 == null)
            {
                goto Label_07BF;
            }
            num12 = param2.luk;
        Label_07BF:
            num = (param3.atk * ((100 * ((param.luk * 2) - num12)) / 10)) / 100;
            goto Label_0840;
        Label_07EC:
            num13 = attacker.MaximumStatus.param.hp;
            num = (param3.atk * ((100 * (num13 - param.hp)) / 10)) / 100;
            goto Label_0840;
        Label_082E:
            num = param.atk;
        Label_0840:
            goto Label_086F;
        Label_0845:
            if (skill.IsPhysicalAttack() == null)
            {
                goto Label_0862;
            }
            num = param.atk;
            goto Label_086F;
        Label_0862:
            num = param.mag;
        Label_086F:
            if (num >= 0)
            {
                goto Label_0878;
            }
            num = 0;
        Label_0878:
            return num;
        }

        private int CalcDefPointSkill(Unit attacker, Unit defender, SkillData skill, LogSkill log)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            Grid grid;
            Grid grid2;
            int num6;
            int num7;
            num = 0;
            this.DefendSkill(attacker, defender, skill, log);
            if (skill.IsPhysicalAttack() == null)
            {
                goto Label_002E;
            }
            num = defender.CurrentStatus.param.def;
        Label_002E:
            if (skill.IsMagicalAttack() == null)
            {
                goto Label_004F;
            }
            num = defender.CurrentStatus.param.mnd;
        Label_004F:
            num2 = 0;
            num3 = 0;
            num4 = 0;
            num5 = 0;
            if (num <= 0)
            {
                goto Label_0149;
            }
            num5 = skill.SkillParam.ignore_defense_rate;
            if (attacker.IsUnitFlag(0x40) == null)
            {
                goto Label_0090;
            }
            if (skill.BackAttackDefenseDownRate == null)
            {
                goto Label_0090;
            }
            num2 = skill.BackAttackDefenseDownRate;
        Label_0090:
            if (attacker.IsUnitFlag(0x20) == null)
            {
                goto Label_00AF;
            }
            if (skill.SideAttackDefenseDownRate == null)
            {
                goto Label_00AF;
            }
            num3 = skill.SideAttackDefenseDownRate;
        Label_00AF:
            if (skill.IsEnableHeightParamAdjust() == null)
            {
                goto Label_012F;
            }
            grid = this.GetUnitGridPosition(attacker);
            grid2 = this.GetUnitGridPosition(defender);
            if (grid == null)
            {
                goto Label_012F;
            }
            if (grid2 == null)
            {
                goto Label_012F;
            }
            num6 = grid.height - grid2.height;
            if (num6 <= 0)
            {
                goto Label_010D;
            }
            num4 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.HighGridDefRate;
        Label_010D:
            if (num6 >= 0)
            {
                goto Label_012F;
            }
            num4 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.DownGridDefRate;
        Label_012F:
            num7 = ((num5 + num2) + num3) + num4;
            num += ((100 * num) * num7) / 0x2710;
        Label_0149:
            return num;
        }

        private int CalcGainedGems(Unit self, Unit target, SkillData skill, int damage, bool bCritical, bool bWeakPoint)
        {
            FixParam param;
            int num;
            int num2;
            if (skill == null)
            {
                goto Label_0011;
            }
            if (skill.IsNormalAttack() != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            if (target.IsBreakObj == null)
            {
                goto Label_0020;
            }
            return 0;
        Label_0020:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            num = param.GemsGainNormalAttack;
            if (bCritical == null)
            {
                goto Label_0051;
            }
            num += param.GemsGainCriticalAttack;
        Label_0051:
            if (bWeakPoint == null)
            {
                goto Label_0066;
            }
            num += param.GemsGainWeakAttack;
        Label_0066:
            if (self.IsUnitFlag(0x20) == null)
            {
                goto Label_0081;
            }
            num += param.GemsGainSideAttack;
        Label_0081:
            if (self.IsUnitFlag(0x40) == null)
            {
                goto Label_009C;
            }
            num += param.GemsGainBackAttack;
        Label_009C:
            if (target.IsDead == null)
            {
                goto Label_00B5;
            }
            num += param.GemsGainKillBonus;
        Label_00B5:
            if (param.GemsGainDiffFloorCount <= 0)
            {
                goto Label_0127;
            }
            num2 = this.CurrentMap[self.x, self.y].height - this.CurrentMap[target.x, target.y].height;
            if (num2 <= 0)
            {
                goto Label_0127;
            }
            num += Math.Min(num2 / param.GemsGainDiffFloorCount, param.GemsGainDiffFloorMax);
        Label_0127:
            num += self.CurrentStatus[13];
            num += (num * self.CurrentStatus[0x29]) / 100;
            return num;
        }

        public int CalcGridDistance(Grid start, Grid goal)
        {
            this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
            if (start == null)
            {
                goto Label_001D;
            }
            if (goal != null)
            {
                goto Label_0023;
            }
        Label_001D:
            return 0xff;
        Label_0023:
            return (Math.Abs(start.x - goal.x) + Math.Abs(start.y - goal.y));
        }

        public int CalcGridDistance(Unit self, Unit target)
        {
            this.DebugAssert((self == null) == 0, "self == null");
            this.DebugAssert((target == null) == 0, "target == null");
            return (Math.Abs(self.x - target.x) + Math.Abs(self.y - target.y));
        }

        private int CalcHeal(Unit self, Unit target, SkillData skill, LogSkill log)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (skill.EffectType != 4)
            {
                goto Label_003C;
            }
            num = this.GetSkillEffectValue(self, target, skill, log);
            return SkillParam.CalcSkillEffectValue(skill.EffectCalcType, num, self.CurrentStatus.param.mag);
        Label_003C:
            if (skill.EffectType != 0x13)
            {
                goto Label_0072;
            }
            num3 = this.GetSkillEffectValue(self, target, skill, log);
            num4 = (target.MaximumStatus.param.hp * num3) / 100;
            return num4;
        Label_0072:
            return 0;
        }

        private unsafe bool CalcMoveTargetAI(Unit self, bool forceAI)
        {
            Grid[] gridArray1;
            int num;
            Grid grid;
            AIPatrolPoint point;
            Grid grid2;
            Grid grid3;
            bool flag;
            Grid grid4;
            bool flag2;
            bool flag3;
            List<Grid> list;
            Grid grid5;
            List<Unit> list2;
            List<MoveGoalTarget> list3;
            List<Grid> list4;
            int num2;
            int num3;
            MoveGoalTarget target;
            Grid grid6;
            Grid[] gridArray;
            int num4;
            int num5;
            int num6;
            Comparison<MoveGoalTarget> comparison;
            int num7;
            Vector2 vector;
            Grid grid7;
            List<Grid> list5;
            bool flag4;
            int num8;
            int num9;
            <CalcMoveTargetAI>c__AnonStorey1C3 storeyc;
            storeyc = new <CalcMoveTargetAI>c__AnonStorey1C3();
            if (self.IsEnableMoveCondition(0) != null)
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            if (self.GetMoveCount(0) != null)
            {
                goto Label_0025;
            }
            return 0;
        Label_0025:
            storeyc.map = this.CurrentMap;
            storeyc.start = storeyc.map[self.x, self.y];
            if (self.IsUnitFlag(0x80) == null)
            {
                goto Label_008E;
            }
            grid = this.GetEscapePositionAI(self);
            if (grid == null)
            {
                goto Label_008E;
            }
            if (storeyc.start != grid)
            {
                goto Label_007E;
            }
            return 0;
        Label_007E:
            if (this.Move(self, grid, forceAI) == null)
            {
                goto Label_008E;
            }
            return 1;
        Label_008E:
            point = self.GetCurrentPatrolPoint();
            if (point == null)
            {
                goto Label_010B;
            }
            grid2 = storeyc.map[point.x, point.y];
            if (grid2 == null)
            {
                goto Label_010B;
            }
            if (storeyc.start != grid2)
            {
                goto Label_00C9;
            }
            return 0;
        Label_00C9:
            if (this.Move(self, grid2, forceAI) == null)
            {
                goto Label_010B;
            }
            grid3 = storeyc.map[self.x, self.y];
            if (grid3.step > point.length)
            {
                goto Label_0109;
            }
            self.NextPatrolPoint();
        Label_0109:
            return 1;
        Label_010B:
            flag = 0;
            if (self.IsUnitFlag(4) == null)
            {
                goto Label_013E;
            }
            if ((self.AI == null) || (self.AI.CheckFlag(8) == null))
            {
                goto Label_013B;
            }
            goto Label_013E;
        Label_013B:
            flag = 1;
        Label_013E:
            if (flag == null)
            {
                goto Label_0176;
            }
            grid4 = this.GetSafePositionAI(self);
            if (grid4 == null)
            {
                goto Label_0176;
            }
            if (storeyc.start != grid4)
            {
                goto Label_0165;
            }
            return 0;
        Label_0165:
            if (this.Move(self, grid4, forceAI) == null)
            {
                goto Label_0176;
            }
            return 1;
        Label_0176:
            flag2 = (self.AI == null) ? 0 : self.AI.CheckFlag(0x40);
            flag3 = self.IsUnitFlag(0x100);
            if (self.TreasureGainTarget == null)
            {
                goto Label_0227;
            }
            list = this.GetEnableMoveGridList(self, 1, flag2, flag3, 1, 1);
            grid5 = null;
            if (list.Contains(self.TreasureGainTarget) == null)
            {
                goto Label_01DD;
            }
            grid5 = self.TreasureGainTarget;
            goto Label_01FA;
        Label_01DD:
            grid5 = (list.Count <= 0) ? null : list[0];
        Label_01FA:
            if (((grid5 == null) || (storeyc.map.CalcMoveSteps(self, grid5, 0) == null)) || (this.Move(self, grid5, forceAI) == null))
            {
                goto Label_0227;
            }
            return 1;
        Label_0227:
            list2 = this.mEnemyPriorities;
            list2.AddRange(this.mGimmickPriorities);
            list3 = MoveGoalTarget.Create(list2);
            storeyc.map.CalcMoveSteps(self, this.GetUnitGridPosition(self), 0);
            list4 = this.GetEnableMoveGridList(self, 1, flag2, flag3, 0, 0);
            num2 = 0;
            goto Label_0289;
        Label_0273:
            list4[num2].step = 0x7f;
            num2 += 1;
        Label_0289:
            if (num2 < list4.Count)
            {
                goto Label_0273;
            }
            num3 = 0;
            goto Label_0462;
        Label_029F:
            target = list3[num3];
            grid6 = this.GetUnitGridPosition(target.unit);
            gridArray1 = new Grid[] { storeyc.map[grid6.x - 1, grid6.y], storeyc.map[grid6.x + 1, grid6.y], storeyc.map[grid6.x, grid6.y + 1], storeyc.map[grid6.x, grid6.y - 1] };
            gridArray = gridArray1;
            num4 = -1;
            num5 = 0;
            goto Label_03B9;
        Label_0348:
            if ((((gridArray[num5] == null) || (gridArray[num5].step == 0x7f)) || (Math.Abs(grid6.height - gridArray[num5].height) > this.mSkillMap.attackHeight)) || ((num4 != -1) && (gridArray[num5].step >= gridArray[num4].step)))
            {
                goto Label_03B3;
            }
            num4 = num5;
        Label_03B3:
            num5 += 1;
        Label_03B9:
            if (num5 < ((int) gridArray.Length))
            {
                goto Label_0348;
            }
            if (num4 == -1)
            {
                goto Label_0428;
            }
            &target.goal.x = (float) gridArray[num4].x;
            &target.goal.y = (float) gridArray[num4].y;
            target.step = (float) ((target.unit.IsGimmick == null) ? -1 : gridArray[num4].step);
            goto Label_045C;
        Label_0428:
            &target.goal.x = (float) grid6.x;
            &target.goal.y = (float) grid6.y;
            target.step = 255f;
        Label_045C:
            num3 += 1;
        Label_0462:
            if (num3 < list3.Count)
            {
                goto Label_029F;
            }
            if (this.mGimmickPriorities.Count <= 0)
            {
                goto Label_04A9;
            }
            if (<>f__am$cache79 != null)
            {
                goto Label_0499;
            }
            <>f__am$cache79 = new Comparison<MoveGoalTarget>(BattleCore.<CalcMoveTargetAI>m__6A);
        Label_0499:
            comparison = <>f__am$cache79;
            SortUtility.StableSort<MoveGoalTarget>(list3, comparison);
        Label_04A9:
            num7 = 0;
            goto Label_0608;
        Label_04B1:
            vector = list3[num7].goal;
            grid7 = this.GetUnitGridPosition((int) &vector.x, (int) &vector.y);
            if (storeyc.map.CalcMoveSteps(self, grid7, 0) != null)
            {
                goto Label_04F3;
            }
            goto Label_0602;
        Label_04F3:
            list5 = this.GetEnableMoveGridList(self, 1, flag2, flag3, 0, 1);
            if (flag3 == null)
            {
                goto Label_057F;
            }
            flag4 = 0;
            num8 = 0;
            goto Label_055B;
        Label_0515:
            if (list5[num8].step >= 0)
            {
                goto Label_052E;
            }
            goto Label_0555;
        Label_052E:
            if (list5[num8].step >= storeyc.start.step)
            {
                goto Label_0555;
            }
            flag4 = 1;
            goto Label_0569;
        Label_0555:
            num8 += 1;
        Label_055B:
            if (num8 < list5.Count)
            {
                goto Label_0515;
            }
        Label_0569:
            if (flag4 != null)
            {
                goto Label_057F;
            }
            list5 = this.GetEnableMoveGridList(self, 1, flag2, 0, 0, 1);
        Label_057F:
            MySort<Grid>.Sort(list5, new Comparison<Grid>(storeyc.<>m__6B));
            num9 = 0;
            goto Label_05D3;
        Label_059B:
            if (storeyc.start != list5[num9])
            {
                goto Label_05B5;
            }
            goto Label_05E1;
        Label_05B5:
            if (this.Move(self, list5[num9], forceAI) == null)
            {
                goto Label_05CD;
            }
            return 1;
        Label_05CD:
            num9 += 1;
        Label_05D3:
            if (num9 < list5.Count)
            {
                goto Label_059B;
            }
        Label_05E1:
            if (storeyc.start != grid7)
            {
                goto Label_05F1;
            }
            return 0;
        Label_05F1:
            if (this.Move(self, grid7, forceAI) == null)
            {
                goto Label_0602;
            }
            return 1;
        Label_0602:
            num7 += 1;
        Label_0608:
            if (num7 < list3.Count)
            {
                goto Label_04B1;
            }
            return 0;
        }

        public int CalcNearGridDistance(Unit self, Grid target)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            Grid grid;
            Grid grid2;
            int num4;
            this.DebugAssert((self == null) == 0, "self == null");
            this.DebugAssert((target == null) == 0, "target == null");
            map = this.CurrentMap;
            this.DebugAssert((map == null) == 0, "map == null");
            num = 0xff;
            num2 = 0;
            goto Label_00CE;
        Label_004A:
            num3 = 0;
            goto Label_00BE;
        Label_0051:
            grid = map[self.x + num2, self.y + num3];
            this.DebugAssert((grid == null) == 0, "start == null");
            grid2 = map[target.x, target.y];
            this.DebugAssert((grid2 == null) == 0, "goal == null");
            num4 = this.CalcGridDistance(grid, grid2);
            if (num4 >= num)
            {
                goto Label_00BA;
            }
            num = num4;
        Label_00BA:
            num3 += 1;
        Label_00BE:
            if (num3 < self.SizeY)
            {
                goto Label_0051;
            }
            num2 += 1;
        Label_00CE:
            if (num2 < self.SizeX)
            {
                goto Label_004A;
            }
            return num;
        }

        public unsafe int CalcNearGridDistance(Unit self, Unit target)
        {
            Grid grid;
            Grid grid2;
            return this.FindNearGridAndDistance(self, target, &grid, &grid2);
        }

        private void CalcOrder()
        {
            int num;
            Unit unit;
            OrderData data;
            OrderData data2;
            int num2;
            Unit unit2;
            OrderData data3;
        Label_0000:
            this.mOrder.Clear();
            num = 0;
            goto Label_00F1;
        Label_0012:
            unit = this.mUnits[num];
            if (unit.IsDeadCondition() != null)
            {
                goto Label_00ED;
            }
            if (unit.IsEntry == null)
            {
                goto Label_00ED;
            }
            if (unit.Side != null)
            {
                goto Label_0050;
            }
            if (unit.IsSub == null)
            {
                goto Label_0050;
            }
            goto Label_00ED;
        Label_0050:
            if (unit.IsUnitCondition(0x10000L) == null)
            {
                goto Label_0066;
            }
            goto Label_00ED;
        Label_0066:
            if (unit.IsGimmick == null)
            {
                goto Label_0081;
            }
            if (unit.AI != null)
            {
                goto Label_0081;
            }
            goto Label_00ED;
        Label_0081:
            if (unit.IsBreakObj == null)
            {
                goto Label_0091;
            }
            goto Label_00ED;
        Label_0091:
            data = new OrderData();
            data.Unit = unit;
            data.IsCharged = unit.CheckChargeTimeFullOver();
            this.mOrder.Add(data);
            if (unit.CastSkill == null)
            {
                goto Label_00ED;
            }
            data2 = new OrderData();
            data2.Unit = unit;
            data2.IsCastSkill = 1;
            data2.IsCharged = unit.CheckCastTimeFullOver();
            this.mOrder.Add(data2);
        Label_00ED:
            num += 1;
        Label_00F1:
            if (num < this.mUnits.Count)
            {
                goto Label_0012;
            }
            num2 = 0;
            goto Label_015C;
        Label_010A:
            unit2 = this.mUnits[num2];
            if (unit2.CheckEnableEntry() == null)
            {
                goto Label_0156;
            }
            this.EntryUnit(unit2);
            if (unit2.IsBreakObj != null)
            {
                goto Label_0156;
            }
            data3 = new OrderData();
            data3.Unit = unit2;
            this.mOrder.Add(data3);
        Label_0156:
            num2 += 1;
        Label_015C:
            if (num2 < this.mUnits.Count)
            {
                goto Label_010A;
            }
            if (this.mOrder.Count == null)
            {
                goto Label_0189;
            }
            if (this.CheckEnableNextClockTime() == null)
            {
                goto Label_019E;
            }
        Label_0189:
            this.NextClockTime();
            goto Label_0000;
            goto Label_019E;
        Label_019E:
            MySort<OrderData>.Sort(this.mOrder, new Comparison<OrderData>(this.<CalcOrder>m__5D));
            return;
        }

        public int CalcPlayerUnitsTotalParameter()
        {
            int num;
            StringBuilder builder;
            int num2;
            num = 0;
            builder = new StringBuilder(0x80);
            num2 = 0;
            goto Label_00F6;
        Label_0014:
            if (this.mUnits[num2] != null)
            {
                goto Label_002A;
            }
            goto Label_00F2;
        Label_002A:
            if (this.mUnits[num2].IsPartyMember == null)
            {
                goto Label_00F2;
            }
            if (this.mUnits[num2].IsNPC == null)
            {
                goto Label_005B;
            }
            goto Label_00F2;
        Label_005B:
            if (this.mUnits[num2].UnitData != null)
            {
                goto Label_0076;
            }
            goto Label_00F2;
        Label_0076:
            builder.Append(this.mUnits[num2].UnitData.UnitParam.name);
            builder.Append("\n    ");
            builder.Append("total : ");
            builder.Append(this.mUnits[num2].UnitData.CalcTotalParameter());
            builder.Append("\n");
            num += this.mUnits[num2].UnitData.CalcTotalParameter();
        Label_00F2:
            num2 += 1;
        Label_00F6:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0014;
            }
            builder.Append("総合値 : ");
            builder.Append(num);
            DebugUtility.Log(builder.ToString());
            return num;
        }

        private unsafe void CalcQuestRecord()
        {
            int num;
            int num2;
            bool flag;
            int num3;
            int num4;
            int num5;
            bool flag2;
            QuestBonusObjective objective;
            QuestBonusObjective objective2;
            List<int> list;
            int num6;
            this.mRecord.result = this.GetQuestResult();
            this.mRecord.playerexp = this.mQuestParam.pexp;
            this.mRecord.gold = this.mQuestParam.gold;
            this.mRecord.unitexp = this.mQuestParam.uexp;
            this.mRecord.multicoin = this.mQuestParam.mcoin;
            this.mRecord.items.Clear();
            this.mRecord.bonusFlags = 0;
            this.mRecord.allBonusFlags = 0;
            this.mRecord.bonusCount = 0;
            if (this.mRecord.takeoverProgressList == null)
            {
                goto Label_00D1;
            }
            this.mRecord.takeoverProgressList.Clear();
        Label_00D1:
            num = -1;
            num2 = 0;
            flag = 0;
            if (this.mQuestParam.type != 7)
            {
                goto Label_00EA;
            }
            flag = 1;
        Label_00EA:
            if (this.mRecord.result == 1)
            {
                goto Label_0101;
            }
            if (flag == null)
            {
                goto Label_03D5;
            }
        Label_0101:
            if (this.mQuestParam.bonusObjective == null)
            {
                goto Label_03D5;
            }
            if (this.mQuestParam.type != 7)
            {
                goto Label_0151;
            }
            num3 = 0;
            goto Label_013E;
        Label_0129:
            this.mRecord.takeoverProgressList.Add(0);
            num3 += 1;
        Label_013E:
            if (num3 < ((int) this.mQuestParam.bonusObjective.Length))
            {
                goto Label_0129;
            }
        Label_0151:
            num4 = 0;
            goto Label_0325;
        Label_0159:
            if (this.mQuestParam.bonusObjective[num4].Type != 0x1b)
            {
                goto Label_0176;
            }
            num = num4;
        Label_0176:
            if (this.mRecord.result != 2)
            {
                goto Label_01A4;
            }
            if (this.mQuestParam.bonusObjective[num4].IsMissionTypeAllowLose() != null)
            {
                goto Label_01A4;
            }
            goto Label_031F;
        Label_01A4:
            num5 = 0;
            flag2 = this.IsBonusObjectiveComplete(this.mQuestParam.bonusObjective[num4], &num5);
            if (num4 >= this.mRecord.takeoverProgressList.Count)
            {
                goto Label_0248;
            }
            if (this.mQuestParam.CheckMissionValueIsDefault(num4) == null)
            {
                goto Label_0201;
            }
            this.mRecord.takeoverProgressList[num4] = num5;
            goto Label_0248;
        Label_0201:
            this.mRecord.takeoverProgressList[num4] = this.mQuestParam.GetMissionValue(num4);
            num6 = list[num6];
            (list = this.mRecord.takeoverProgressList)[num6 = num4] = num6 + num5;
        Label_0248:
            if (this.mQuestParam.bonusObjective[num4].IsProgressMission() == null)
            {
                goto Label_02A0;
            }
            if (this.mRecord.result != 2)
            {
                goto Label_0279;
            }
            flag2 = 0;
            goto Label_02A0;
        Label_0279:
            flag2 = this.mQuestParam.bonusObjective[num4].CheckMissionValueAchievable(this.mRecord.takeoverProgressList[num4]);
        Label_02A0:
            if (flag2 == null)
            {
                goto Label_02C7;
            }
            this.mRecord.allBonusFlags |= 1 << ((num4 & 0x1f) & 0x1f);
            num2 += 1;
        Label_02C7:
            if ((this.mQuestParam.clear_missions & (1 << (num4 & 0x1f))) == null)
            {
                goto Label_02E4;
            }
            goto Label_031F;
        Label_02E4:
            if (flag2 == null)
            {
                goto Label_031F;
            }
            objective = this.mQuestParam.bonusObjective[num4];
            this.SetReward(objective);
            this.mRecord.bonusFlags |= 1 << ((num4 & 0x1f) & 0x1f);
        Label_031F:
            num4 += 1;
        Label_0325:
            if (num4 < ((int) this.mQuestParam.bonusObjective.Length))
            {
                goto Label_0159;
            }
            this.mRecord.bonusCount = (int) this.mQuestParam.bonusObjective.Length;
            if (num < 0)
            {
                goto Label_03D5;
            }
            if ((((int) this.mQuestParam.bonusObjective.Length) - num2) > 1)
            {
                goto Label_03D5;
            }
            this.mRecord.allBonusFlags |= 1 << ((num & 0x1f) & 0x1f);
            num2 += 1;
            if ((this.mQuestParam.clear_missions & (1 << (num & 0x1f))) != null)
            {
                goto Label_03D5;
            }
            objective2 = this.mQuestParam.bonusObjective[num];
            this.SetReward(objective2);
            this.mRecord.bonusFlags |= 1 << ((num & 0x1f) & 0x1f);
        Label_03D5:
            this.GainUnitSteal(this.mRecord);
            this.GainUnitDrop(this.mRecord, 0);
            this.mRecord.units.Clear();
            this.GainRankMatchItem();
            this.GainFreeVersusItem();
            return;
        }

        private bool CalcSearchingAI(Unit self)
        {
            List<Unit> list;
            int num;
            Unit unit;
            int num2;
            Unit unit2;
            bool flag;
            int num3;
            int num4;
            if (self.IsUnitFlag(8) == null)
            {
                goto Label_000E;
            }
            return 1;
        Label_000E:
            list = new List<Unit>(1);
            list.Add(self);
            if (string.IsNullOrEmpty(self.UniqueName) != null)
            {
                goto Label_009F;
            }
            num = 0;
            goto Label_008E;
        Label_0033:
            unit = this.mUnits[num];
            if (unit != self)
            {
                goto Label_004C;
            }
            goto Label_008A;
        Label_004C:
            if (unit.Side == self.Side)
            {
                goto Label_0062;
            }
            goto Label_008A;
        Label_0062:
            if ((unit.ParentUniqueName == self.UniqueName) == null)
            {
                goto Label_008A;
            }
            list.Add(this.mUnits[num]);
        Label_008A:
            num += 1;
        Label_008E:
            if (num < this.mUnits.Count)
            {
                goto Label_0033;
            }
        Label_009F:
            if (string.IsNullOrEmpty(self.ParentUniqueName) != null)
            {
                goto Label_013D;
            }
            num2 = 0;
            goto Label_012C;
        Label_00B6:
            unit2 = this.mUnits[num2];
            if (unit2 != self)
            {
                goto Label_00D1;
            }
            goto Label_0128;
        Label_00D1:
            if (unit2.Side == self.Side)
            {
                goto Label_00E8;
            }
            goto Label_0128;
        Label_00E8:
            if ((unit2.UniqueName == self.ParentUniqueName) != null)
            {
                goto Label_0116;
            }
            if ((unit2.ParentUniqueName == self.ParentUniqueName) == null)
            {
                goto Label_0128;
            }
        Label_0116:
            list.Add(this.mUnits[num2]);
        Label_0128:
            num2 += 1;
        Label_012C:
            if (num2 < this.mUnits.Count)
            {
                goto Label_00B6;
            }
        Label_013D:
            flag = 0;
            num3 = 0;
            goto Label_017C;
        Label_0148:
            if (list[num3].IsUnitFlag(8) != null)
            {
                goto Label_016E;
            }
            if (this.Searching(list[num3]) == null)
            {
                goto Label_0176;
            }
        Label_016E:
            flag = 1;
            goto Label_0189;
        Label_0176:
            num3 += 1;
        Label_017C:
            if (num3 < list.Count)
            {
                goto Label_0148;
            }
        Label_0189:
            if (flag == null)
            {
                goto Label_01BC;
            }
            num4 = 0;
            goto Label_01AD;
        Label_0198:
            list[num4].SetUnitFlag(8, 1);
            num4 += 1;
        Label_01AD:
            if (num4 < list.Count)
            {
                goto Label_0198;
            }
            return 1;
        Label_01BC:
            this.CommandWait(0);
            return 0;
        }

        public bool CalcUseActionAI(Unit self, AIAction action, Func<List<SkillResult>, bool> useskill)
        {
            List<SkillData> list;
            SkillData data;
            bool flag;
            SkillData data2;
            SkillData data3;
            <CalcUseActionAI>c__AnonStorey1BF storeybf;
            int num;
            storeybf = new <CalcUseActionAI>c__AnonStorey1BF();
            storeybf.action = action;
            if (storeybf.action != null)
            {
                goto Label_001D;
            }
            return 0;
        Label_001D:
            list = null;
            if (string.IsNullOrEmpty(storeybf.action.skill) == null)
            {
                goto Label_00B2;
            }
            switch (storeybf.action.type)
            {
                case 0:
                    goto Label_0097;

                case 1:
                    goto Label_0065;

                case 2:
                    goto Label_0092;
            }
            goto Label_0097;
        Label_0065:
            data = self.GetAttackSkill();
            if ((data == null) || (this.CheckEnableUseSkill(self, data, 0) == null))
            {
                goto Label_013B;
            }
            list = new List<SkillData>();
            list.Add(data);
            goto Label_00AD;
        Label_0092:
            goto Label_00AD;
        Label_0097:
            this.CommandWait(0);
            this.mSkillMap.NextAction(0);
            return 1;
        Label_00AD:
            goto Label_013B;
        Label_00B2:
            flag = 1;
            if (storeybf.action.cond == null)
            {
                goto Label_00D7;
            }
            flag = storeybf.action.cond.unlock;
        Label_00D7:
            if (flag == null)
            {
                goto Label_013B;
            }
            data2 = self.BattleSkills.Find(new Predicate<SkillData>(storeybf.<>m__66));
            data3 = self.GetSkillForUseCount(storeybf.action.skill, 0);
            data2 = (data3 != null) ? data3 : data2;
            if (this.CheckEnableUseSkill(self, data2, 0) == null)
            {
                goto Label_013B;
            }
            list = new List<SkillData>();
            list.Add(data2);
        Label_013B:
            if (list == null)
            {
                goto Label_0151;
            }
            if (this.CalcUseSkillsAI(self, list, useskill) == null)
            {
                goto Label_0151;
            }
            return 1;
        Label_0151:
            return 0;
        }

        public bool CalcUseSkillsAI(Unit self, List<SkillData> skills, Func<List<SkillResult>, bool> useskill)
        {
            int num;
            if (skills.Count != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            this.UpdateSkillMap(self, skills);
            mSkillResults.Clear();
            num = 0;
            goto Label_004E;
        Label_0026:
            if (skills[num] != null)
            {
                goto Label_0037;
            }
            goto Label_004A;
        Label_0037:
            this.GetUsedSkillResultAllEx(self, skills[num], mSkillResults);
        Label_004A:
            num += 1;
        Label_004E:
            if (num < skills.Count)
            {
                goto Label_0026;
            }
            if (mSkillResults.Count <= 0)
            {
                goto Label_007C;
            }
            if (useskill(mSkillResults) == null)
            {
                goto Label_007C;
            }
            return 1;
        Label_007C:
            return 0;
        }

        public void CastSkillEnd()
        {
            this.ExecuteEventTriggerOnGrid(this.CurrentUnit, 0);
            this.NextOrder(0, 1, 0, 1);
            return;
        }

        public void CastSkillStart()
        {
            Unit unit;
            SkillData data;
            unit = this.CurrentUnit;
            data = unit.CastSkill;
            if (data == null)
            {
                goto Label_0072;
            }
            if (unit.UnitTarget == null)
            {
                goto Label_0043;
            }
            this.UseSkill(unit, unit.UnitTarget.x, unit.UnitTarget.y, data, 0, 0, 0, 0);
            return;
        Label_0043:
            if (unit.GridTarget == null)
            {
                goto Label_0072;
            }
            this.UseSkill(unit, unit.GridTarget.x, unit.GridTarget.y, data, 0, 0, 0, 0);
            return;
        Label_0072:
            unit.CancelCastSkill();
            this.Log<LogCastSkillEnd>();
            return;
        }

        private unsafe void CastStart(Unit self, int gx, int gy, SkillData skill, bool bUnitLockTarget)
        {
            Unit unit;
            Grid grid;
            LogCast cast;
            BattleMap map;
            GridMap<bool> map2;
            unit = null;
            grid = this.CurrentMap[gx, gy];
            if (bUnitLockTarget == null)
            {
                goto Label_0027;
            }
            unit = this.FindUnitAtGrid(grid);
            if (unit == null)
            {
                goto Label_0027;
            }
            grid = null;
        Label_0027:
            cast = this.Log<LogCast>();
            cast.self = self;
            cast.type = skill.CastType;
            cast.dx = gx;
            cast.dy = gy;
            self.SetCastSkill(skill, unit, grid);
            self.SetUnitFlag(4, 1);
            self.SetCommandFlag(2, 1);
            map = this.CurrentMap;
            map2 = new GridMap<bool>(map.Width, map.Height);
            if (skill.IsAreaSkill() == null)
            {
                goto Label_00B0;
            }
            this.CreateScopeGridMap(self, self.x, self.y, gx, gy, skill, &map2, 0);
            goto Label_00BA;
        Label_00B0:
            map2.set(gx, gy, 1);
        Label_00BA:
            if (skill.TeleportType != 3)
            {
                goto Label_00D1;
            }
            map2.set(gx, gy, 0);
        Label_00D1:
            self.CastSkillGridMap = map2;
            this.Log<LogMapCommand>();
            return;
        }

        private bool ChangeWeatherForSkill(Unit self, SkillData skill)
        {
            int num;
            string str;
            WeatherParam param;
            WeatherData data;
            int num2;
            WeatherData data2;
            if (self == null)
            {
                goto Label_000C;
            }
            if (skill != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            if (WeatherData.IsAllowWeatherChange != null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            num = skill.WeatherRate;
            str = skill.WeatherId;
            if (num <= 0)
            {
                goto Label_003A;
            }
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_003C;
            }
        Label_003A:
            return 0;
        Label_003C:
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetWeatherParam(str) != null)
            {
                goto Label_0055;
            }
            return 0;
        Label_0055:
            data = WeatherData.CurrentWeatherData;
            if (data == null)
            {
                goto Label_0070;
            }
            num -= data.GetResistRate(this.ClockTimeTotal);
        Label_0070:
            if (num >= 100)
            {
                goto Label_008D;
            }
            num2 = this.GetRandom() % 100;
            if (num2 < num)
            {
                goto Label_008D;
            }
            return 0;
        Label_008D:
            if (WeatherData.ChangeWeather(str, this.Units, this.ClockTimeTotal, this.CurrentRand, self, skill.Rank, skill.GetRankCap()) != null)
            {
                goto Label_00BD;
            }
            return 0;
        Label_00BD:
            return 1;
        }

        private bool CheckAvoid(Unit self, Unit target, SkillData skill)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = skill.SkillParam.random_hit_rate;
            if (num <= 0)
            {
                goto Label_002B;
            }
            num2 = this.GetRandom() % 100;
            if (num <= num2)
            {
                goto Label_002B;
            }
            return 1;
        Label_002B:
            num3 = this.GetAvoidRate(self, target, skill);
            num4 = this.GetRandom() % 100;
            return (num3 > num4);
        }

        private bool CheckBackAttack(Unit self, Unit target, SkillData skill)
        {
            return this.CheckBackAttack(self.x, self.y, target, skill);
        }

        private bool CheckBackAttack(int sx, int sy, Unit target, SkillData skill)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            num = 0;
            num2 = target.Direction;
            num3 = target.x - sx;
            num4 = target.y - sy;
            if (num3 <= 0)
            {
                goto Label_0024;
            }
            num = 0;
        Label_0024:
            if (num3 >= 0)
            {
                goto Label_002D;
            }
            num = 2;
        Label_002D:
            if (num4 <= 0)
            {
                goto Label_0036;
            }
            num = 1;
        Label_0036:
            if (num4 >= 0)
            {
                goto Label_003F;
            }
            num = 3;
        Label_003F:
            num5 = Unit.DIRECTION_OFFSETS[num, 0] + Unit.DIRECTION_OFFSETS[num2, 0];
            num6 = Unit.DIRECTION_OFFSETS[num, 1] + Unit.DIRECTION_OFFSETS[num2, 1];
            if (Math.Abs(num5) == 2)
            {
                goto Label_008F;
            }
            if (Math.Abs(num6) != 2)
            {
                goto Label_0091;
            }
        Label_008F:
            return 1;
        Label_0091:
            return 0;
        }

        private void CheckBreakObjKill()
        {
            int num;
            Unit unit;
            BreakObjParam param;
            num = 0;
            goto Label_00B6;
        Label_0007:
            unit = this.mUnits[num];
            if (unit.IsDead != null)
            {
                goto Label_00B2;
            }
            if (unit.IsBreakObj == null)
            {
                goto Label_00B2;
            }
            if (string.IsNullOrEmpty(unit.CreateBreakObjId) == null)
            {
                goto Label_003F;
            }
            goto Label_00B2;
        Label_003F:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetBreakObjParam(unit.CreateBreakObjId);
            if (param != null)
            {
                goto Label_0060;
            }
            goto Label_00B2;
        Label_0060:
            if (param.AliveClock != null)
            {
                goto Label_0070;
            }
            goto Label_00B2;
        Label_0070:
            if ((unit.CreateBreakObjClock + param.AliveClock) <= this.mClockTimeTotal)
            {
                goto Label_0092;
            }
            goto Label_00B2;
        Label_0092:
            unit.CurrentStatus.param.hp = 0;
            this.Dead(null, unit, 0, 0);
        Label_00B2:
            num += 1;
        Label_00B6:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private bool CheckCombination(Unit self, Unit other)
        {
            int num;
            int num2;
            int num3;
            num = self.GetCombination() + other.GetCombination();
            num2 = (num * 100) / 0x80;
            num3 = this.GetRandom() % 100;
            if (num2 >= num3)
            {
                goto Label_002C;
            }
            return 0;
        Label_002C:
            return 1;
        }

        private bool CheckCritical(Unit self, Unit target, SkillData skill)
        {
            int num;
            int num2;
            this.DebugAssert((self == null) == 0, "self == null");
            num = this.GetCriticalRate(self, target, skill);
            num2 = this.GetRandom() % 100;
            if (num2 <= num)
            {
                goto Label_002F;
            }
            return 0;
        Label_002F:
            return 1;
        }

        public bool CheckDisableAbilities(Unit self)
        {
            if (self.Side == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mQuestParam.CheckDisableAbilities();
        }

        public bool CheckDisableItems(Unit self)
        {
            if (self.Side == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return this.mQuestParam.CheckDisableItems();
        }

        private bool CheckEnableAttackHeight(Grid start, Grid goal, int diff_ok)
        {
            int num;
            if (Math.Abs(goal.height - start.height) <= diff_ok)
            {
                goto Label_001C;
            }
            return 0;
        Label_001C:
            return 1;
        }

        private bool CheckEnableGimmickEvent(GimmickEvent gimmick)
        {
            int num;
            int num2;
            int num3;
            int num4;
            Unit unit;
            if (gimmick.IsCompleted == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (gimmick.condition.count == null)
            {
                goto Label_0034;
            }
            return ((gimmick.condition.count > gimmick.count) == 0);
        Label_0034:
            if (gimmick.condition.type != 3)
            {
                goto Label_0148;
            }
            num = 0;
            goto Label_0130;
        Label_004C:
            num2 = gimmick.condition.grids[num].x;
            num3 = gimmick.condition.grids[num].y;
            num4 = 0;
            goto Label_011B;
        Label_0081:
            unit = this.Units[num4];
            if (unit.IsGimmick != null)
            {
                goto Label_0117;
            }
            if (unit.CheckExistMap() != null)
            {
                goto Label_00AC;
            }
            goto Label_0117;
        Label_00AC:
            if (gimmick.condition.units.Count <= 0)
            {
                goto Label_00DE;
            }
            if (gimmick.condition.units.Contains(unit) != null)
            {
                goto Label_00DE;
            }
            goto Label_0117;
        Label_00DE:
            if (gimmick.IsStarter == null)
            {
                goto Label_00FB;
            }
            if (unit == gimmick.starter)
            {
                goto Label_00FB;
            }
            goto Label_0117;
        Label_00FB:
            if (unit.x != num2)
            {
                goto Label_0117;
            }
            if (unit.y != num3)
            {
                goto Label_0117;
            }
            return 1;
        Label_0117:
            num4 += 1;
        Label_011B:
            if (num4 < this.Units.Count)
            {
                goto Label_0081;
            }
            num += 1;
        Label_0130:
            if (num < gimmick.condition.grids.Count)
            {
                goto Label_004C;
            }
            return 0;
        Label_0148:
            return 0;
        }

        private bool CheckEnableNextClockTime()
        {
            int num;
            num = 0;
            goto Label_0023;
        Label_0007:
            if (this.mOrder[num].CheckChargeTimeFullOver() == null)
            {
                goto Label_001F;
            }
            return 0;
        Label_001F:
            num += 1;
        Label_0023:
            if (num < this.mOrder.Count)
            {
                goto Label_0007;
            }
            return 1;
        }

        public bool CheckEnableRemainingActionCount(QuestMonitorCondition cond)
        {
            int num;
            UnitMonitorCondition condition;
            int num2;
            if (cond == null)
            {
                goto Label_0016;
            }
            if (cond.actions.Count != null)
            {
                goto Label_0018;
            }
        Label_0016:
            return 0;
        Label_0018:
            num = 0;
            goto Label_00F2;
        Label_001F:
            condition = cond.actions[num];
            if (condition.turn <= 0)
            {
                goto Label_00EE;
            }
            if (string.IsNullOrEmpty(condition.tag) != null)
            {
                goto Label_009A;
            }
            if ((condition.tag == "pall") != null)
            {
                goto Label_0098;
            }
            if ((condition.tag == "eall") != null)
            {
                goto Label_0098;
            }
            if ((condition.tag == "nall") != null)
            {
                goto Label_0098;
            }
            if (this.FindUnitByUniqueName(condition.tag) == null)
            {
                goto Label_009A;
            }
        Label_0098:
            return 1;
        Label_009A:
            if (string.IsNullOrEmpty(condition.iname) != null)
            {
                goto Label_00EE;
            }
            num2 = 0;
            goto Label_00DD;
        Label_00B1:
            if ((this.Units[num2].UnitParam.iname == condition.iname) == null)
            {
                goto Label_00D9;
            }
            return 1;
        Label_00D9:
            num2 += 1;
        Label_00DD:
            if (num2 < this.Units.Count)
            {
                goto Label_00B1;
            }
        Label_00EE:
            num += 1;
        Label_00F2:
            if (num < cond.actions.Count)
            {
                goto Label_001F;
            }
            return 0;
        }

        public bool CheckEnableSuspendSave()
        {
            if (this.mQuestParam.CheckEnableSuspendStart() == null)
            {
                goto Label_001B;
            }
            if (this.IsMultiPlay == null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 0;
        Label_001D:
            return 1;
        }

        public bool CheckEnableSuspendStart()
        {
            if (MonoSingleton<GameManager>.Instance.MasterParam.FixParam.IsDisableSuspend != null)
            {
                goto Label_0039;
            }
            if (this.mQuestParam.CheckEnableSuspendStart() == null)
            {
                goto Label_0039;
            }
            if (this.IsMultiPlay == null)
            {
                goto Label_003B;
            }
        Label_0039:
            return 0;
        Label_003B:
            return BattleSuspend.IsExistData();
        }

        private bool CheckEnableUseSkill(Unit self, SkillData skill, bool bCheckCondOnly)
        {
            return self.CheckEnableUseSkill(skill, bCheckCondOnly);
        }

        private bool CheckEnemyIntercept(Unit self)
        {
            int num;
            Unit unit;
            num = 0;
            goto Label_0087;
        Label_0007:
            unit = this.mUnits[num];
            if (unit != self)
            {
                goto Label_0020;
            }
            goto Label_0083;
        Label_0020:
            if (unit.Side != self.Side)
            {
                goto Label_0036;
            }
            goto Label_0083;
        Label_0036:
            if (unit.IsDeadCondition() != null)
            {
                goto Label_0083;
            }
            if (unit.IsSub != null)
            {
                goto Label_0083;
            }
            if (unit.IsEntry == null)
            {
                goto Label_0083;
            }
            if (unit.IsGimmick == null)
            {
                goto Label_0067;
            }
            goto Label_0083;
        Label_0067:
            if (unit.IsUnitFlag(8) == null)
            {
                goto Label_0075;
            }
            return 1;
        Label_0075:
            if (this.CheckSearchMap(self) == null)
            {
                goto Label_0083;
            }
            return 1;
        Label_0083:
            num += 1;
        Label_0087:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public bool CheckEnemySide(Unit self, Unit target)
        {
            if (self != target)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            if (self.IsUnitCondition(0x10L) != null)
            {
                goto Label_0028;
            }
            if (self.IsUnitCondition(0x400L) == null)
            {
                goto Label_0058;
            }
        Label_0028:
            if (target.IsUnitCondition(0x10L) != null)
            {
                goto Label_0047;
            }
            if (target.IsUnitCondition(0x400L) == null)
            {
                goto Label_0049;
            }
        Label_0047:
            return 0;
        Label_0049:
            return (self.Side == target.Side);
        Label_0058:
            return ((self.Side == target.Side) == 0);
        }

        private bool CheckEscapeAI(Unit self)
        {
            int num;
            List<Unit> list;
            if (this.QuestType != 2)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            if (self.IsEnableMoveCondition(0) != null)
            {
                goto Label_001C;
            }
            return 0;
        Label_001C:
            if (self.GetMoveCount(0) != null)
            {
                goto Label_002C;
            }
            return 0;
        Label_002C:
            if (self.CheckNeedEscaped() != null)
            {
                goto Label_0039;
            }
            return 0;
        Label_0039:
            return (this.GetHealer(self).Count > 0);
        }

        private bool CheckFailCondition(Unit target, int val, int resist, EUnitCondition condition)
        {
            int num;
            int num2;
            if (val > 0)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            num = val - resist;
            if (num <= 0)
            {
                goto Label_0023;
            }
            num2 = this.GetRandom() % 100;
            return (num > num2);
        Label_0023:
            return 0;
        }

        public bool CheckFriendlyFireOnGridMap(Unit self, Grid grid)
        {
            int num;
            Unit unit;
            num = 0;
            goto Label_009F;
        Label_0007:
            unit = this.Units[num];
            if (unit.CastSkill == null)
            {
                goto Label_009B;
            }
            if (unit.CastSkill.IsAllEffect() == null)
            {
                goto Label_0034;
            }
            goto Label_009B;
        Label_0034:
            if (unit.UnitTarget != self)
            {
                goto Label_0045;
            }
            goto Label_009B;
        Label_0045:
            if (unit.CastSkill.IsDamagedSkill() != null)
            {
                goto Label_005A;
            }
            goto Label_009B;
        Label_005A:
            if (this.CheckSkillTarget(unit, self, unit.CastSkill) != null)
            {
                goto Label_0072;
            }
            goto Label_009B;
        Label_0072:
            if (unit.CastSkillGridMap == null)
            {
                goto Label_009B;
            }
            if (unit.CastSkillGridMap.get(grid.x, grid.y) == null)
            {
                goto Label_009B;
            }
            return 1;
        Label_009B:
            num += 1;
        Label_009F:
            if (num < this.Units.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public bool CheckGimmickEnemySide(Unit self, Unit target)
        {
            EUnitSide side;
            if (self == null)
            {
                goto Label_000C;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            if (target.IsGimmick != null)
            {
                goto Label_001B;
            }
            return 0;
        Label_001B:
            if (self != target)
            {
                goto Label_0024;
            }
            return 0;
        Label_0024:
            side = 2;
            if (this.IsMultiVersus == null)
            {
                goto Label_00AD;
            }
            if (target.BreakObjSideType != 1)
            {
                goto Label_006F;
            }
            side = 0;
            if (this.mMyPlayerIndex <= 0)
            {
                goto Label_00CE;
            }
            if (target.OwnerPlayerIndex <= 0)
            {
                goto Label_00CE;
            }
            if (this.mMyPlayerIndex == target.OwnerPlayerIndex)
            {
                goto Label_00CE;
            }
            side = 1;
            goto Label_00A8;
        Label_006F:
            if (target.BreakObjSideType != 2)
            {
                goto Label_00CE;
            }
            side = 1;
            if (this.mMyPlayerIndex <= 0)
            {
                goto Label_00CE;
            }
            if (target.OwnerPlayerIndex <= 0)
            {
                goto Label_00CE;
            }
            if (this.mMyPlayerIndex == target.OwnerPlayerIndex)
            {
                goto Label_00CE;
            }
            side = 0;
        Label_00A8:
            goto Label_00CE;
        Label_00AD:
            if (target.BreakObjSideType != 1)
            {
                goto Label_00C0;
            }
            side = 0;
            goto Label_00CE;
        Label_00C0:
            if (target.BreakObjSideType != 2)
            {
                goto Label_00CE;
            }
            side = 1;
        Label_00CE:
            if (self.IsUnitCondition(0x10L) != null)
            {
                goto Label_00ED;
            }
            if (self.IsUnitCondition(0x400L) == null)
            {
                goto Label_00F7;
            }
        Label_00ED:
            return (self.Side == side);
        Label_00F7:
            return ((self.Side == side) == 0);
        }

        public bool CheckGridEventTrigger(Unit self, EEventTrigger trigger)
        {
            Grid grid;
            if (self == null)
            {
                goto Label_0011;
            }
            if (self.IsDead == null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            grid = this.GetUnitGridPosition(self);
            return this.CheckGridEventTrigger(self, grid, trigger);
        }

        public bool CheckGridEventTrigger(Unit self, Grid grid, EEventTrigger trigger)
        {
            Unit unit;
            EEventType type;
            if (self == null)
            {
                goto Label_0017;
            }
            if (self.IsDead != null)
            {
                goto Label_0017;
            }
            if (grid != null)
            {
                goto Label_0019;
            }
        Label_0017:
            return 0;
        Label_0019:
            unit = this.FindGimmickAtGrid(grid, 0, null);
            if (unit == null)
            {
                goto Label_0030;
            }
            if (self != unit)
            {
                goto Label_0032;
            }
        Label_0030:
            return 0;
        Label_0032:
            if (unit.CheckEventTrigger(trigger) != null)
            {
                goto Label_0040;
            }
            return 0;
        Label_0040:
            switch ((unit.EventTrigger.EventType - 1))
            {
                case 0:
                    goto Label_0069;

                case 1:
                    goto Label_006E;

                case 2:
                    goto Label_0073;

                case 3:
                    goto Label_0073;
            }
            goto Label_0085;
        Label_0069:
            goto Label_008A;
        Label_006E:
            goto Label_008A;
        Label_0073:
            if (self.Side == null)
            {
                goto Label_008A;
            }
            return 0;
            goto Label_008A;
        Label_0085:;
        Label_008A:
            return 1;
        }

        private bool CheckGridOnLine(int x1, int y1, int x2, int y2, int sx, int sy, int tx, int ty)
        {
            long num;
            long num2;
            long num3;
            long num4;
            long num5;
            long num6;
            long num7;
            long num8;
            long num9;
            long num10;
            long num11;
            long num12;
            long num13;
            long num14;
            long num15;
            long num16;
            long num17;
            long num18;
            num = (long) (tx - sx);
            num2 = (long) (ty - sy);
            num3 = (long) (x1 - sx);
            num4 = (long) (y1 - sy);
            num5 = (long) (x2 - sx);
            num6 = (long) (y2 - sy);
            num7 = (num * num4) - (num2 * num3);
            num8 = (num * num6) - (num2 * num5);
            num9 = num7 * num8;
            if (num9 <= 0L)
            {
                goto Label_004F;
            }
            return 0;
        Label_004F:
            num10 = (long) (x2 - x1);
            num11 = (long) (y2 - y1);
            num12 = (long) (sx - x1);
            num13 = (long) (sy - y1);
            num14 = (long) (tx - x1);
            num15 = (long) (ty - y1);
            num16 = (num10 * num13) - (num11 * num12);
            num17 = (num10 * num15) - (num11 * num14);
            num18 = num16 * num17;
            if (num18 <= 0L)
            {
                goto Label_00A4;
            }
            return 0;
        Label_00A4:
            return 1;
        }

        private bool CheckGuts(Unit self)
        {
            int num;
            int num2;
            if (self == null)
            {
                goto Label_0011;
            }
            if (self.IsDead != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            num = self.CurrentStatus[0x10];
            if (num <= 0)
            {
                goto Label_0045;
            }
            num2 = this.CurrentRand.Get() % 100;
            if (num2 >= num)
            {
                goto Label_0045;
            }
            return 1;
        Label_0045:
            if (this.mQuestParam.type != 3)
            {
                goto Label_0063;
            }
            if (self.Side != null)
            {
                goto Label_0063;
            }
            return 1;
        Label_0063:
            return 0;
        }

        private bool CheckJudgeBattle()
        {
            QuestResult result;
            result = this.GetQuestResult();
            if (result == 1)
            {
                goto Label_001C;
            }
            if (result == 2)
            {
                goto Label_001C;
            }
            if (result != 4)
            {
                goto Label_001E;
            }
        Label_001C:
            return 1;
        Label_001E:
            return 0;
        }

        private bool checkKnockBack(Unit self, Unit target, SkillData skill)
        {
            EnchantParam param;
            EnchantParam param2;
            int num;
            int num2;
            if (self == null)
            {
                goto Label_0012;
            }
            if (target == null)
            {
                goto Label_0012;
            }
            if (skill != null)
            {
                goto Label_0014;
            }
        Label_0012:
            return 0;
        Label_0014:
            if (this.isKnockBack(skill) != null)
            {
                goto Label_0022;
            }
            return 0;
        Label_0022:
            if (target.IsKnockBack != null)
            {
                goto Label_002F;
            }
            return 0;
        Label_002F:
            if (target.IsDisableUnitCondition(0x2000L) == null)
            {
                goto Label_0042;
            }
            return 0;
        Label_0042:
            param = self.CurrentStatus.enchant_assist;
            param2 = target.CurrentStatus.enchant_resist;
            num = (skill.KnockBackRate + param[13]) - param2[13];
            if (num > 0)
            {
                goto Label_008B;
            }
            return 0;
        Label_008B:
            if (num >= 100)
            {
                goto Label_00A6;
            }
            num2 = this.GetRandom() % 100;
            if (num2 < num)
            {
                goto Label_00A6;
            }
            return 0;
        Label_00A6:
            return 1;
        }

        private bool CheckMatchUniqueName(Unit self, string tag)
        {
            if (string.IsNullOrEmpty(tag) != null)
            {
                goto Label_006C;
            }
            if ((tag == self.UniqueName) == null)
            {
                goto Label_001E;
            }
            return 1;
        Label_001E:
            if ((tag == "pall") == null)
            {
                goto Label_0038;
            }
            return (self.Side == 0);
        Label_0038:
            if ((tag == "eall") == null)
            {
                goto Label_0052;
            }
            return (self.Side == 1);
        Label_0052:
            if ((tag == "nall") == null)
            {
                goto Label_006C;
            }
            return (self.Side == 2);
        Label_006C:
            return 0;
        }

        public bool CheckMonitorActionCount(QuestMonitorCondition cond)
        {
            int num;
            UnitMonitorCondition condition;
            int num2;
            int num3;
            int num4;
            Unit unit;
            int num5;
            Unit unit2;
            if (cond.actions.Count <= 0)
            {
                goto Label_0221;
            }
            num = 0;
            goto Label_0210;
        Label_0018:
            condition = cond.actions[num];
            if (string.IsNullOrEmpty(condition.tag) != null)
            {
                goto Label_019C;
            }
            if ((condition.tag == "pall") == null)
            {
                goto Label_00A0;
            }
            num2 = 0;
            goto Label_008A;
        Label_0051:
            if (this.mUnits[num2].Side == null)
            {
                goto Label_006C;
            }
            goto Label_0086;
        Label_006C:
            if (this.CheckMonitorActionCountCondition(this.mUnits[num2], condition) == null)
            {
                goto Label_0086;
            }
            return 1;
        Label_0086:
            num2 += 1;
        Label_008A:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0051;
            }
            goto Label_020C;
        Label_00A0:
            if ((condition.tag == "eall") == null)
            {
                goto Label_010C;
            }
            num3 = 0;
            goto Label_00F6;
        Label_00BC:
            if (this.mUnits[num3].Side == 1)
            {
                goto Label_00D8;
            }
            goto Label_00F2;
        Label_00D8:
            if (this.CheckMonitorActionCountCondition(this.mUnits[num3], condition) == null)
            {
                goto Label_00F2;
            }
            return 1;
        Label_00F2:
            num3 += 1;
        Label_00F6:
            if (num3 < this.mUnits.Count)
            {
                goto Label_00BC;
            }
            goto Label_020C;
        Label_010C:
            if ((condition.tag == "nall") == null)
            {
                goto Label_017E;
            }
            num4 = 0;
            goto Label_0167;
        Label_0129:
            if (this.mUnits[num4].Side == 2)
            {
                goto Label_0146;
            }
            goto Label_0161;
        Label_0146:
            if (this.CheckMonitorActionCountCondition(this.mUnits[num4], condition) == null)
            {
                goto Label_0161;
            }
            return 1;
        Label_0161:
            num4 += 1;
        Label_0167:
            if (num4 < this.mUnits.Count)
            {
                goto Label_0129;
            }
            goto Label_020C;
        Label_017E:
            unit = this.FindUnitByUniqueName(condition.tag);
            if (this.CheckMonitorActionCountCondition(unit, condition) == null)
            {
                goto Label_019C;
            }
            return 1;
        Label_019C:
            if (string.IsNullOrEmpty(condition.iname) != null)
            {
                goto Label_020C;
            }
            num5 = 0;
            goto Label_01FA;
        Label_01B4:
            unit2 = this.mUnits[num5];
            if ((unit2.UnitParam.iname != condition.iname) == null)
            {
                goto Label_01E4;
            }
            goto Label_01F4;
        Label_01E4:
            if (this.CheckMonitorActionCountCondition(unit2, condition) == null)
            {
                goto Label_01F4;
            }
            return 1;
        Label_01F4:
            num5 += 1;
        Label_01FA:
            if (num5 < this.mUnits.Count)
            {
                goto Label_01B4;
            }
        Label_020C:
            num += 1;
        Label_0210:
            if (num < cond.actions.Count)
            {
                goto Label_0018;
            }
        Label_0221:
            return 0;
        }

        private bool CheckMonitorActionCountCondition(Unit unit, UnitMonitorCondition monitor)
        {
            if (unit == null)
            {
                goto Label_002F;
            }
            if (unit.IsGimmick != null)
            {
                goto Label_002F;
            }
            if (unit.CheckExistMap() == null)
            {
                goto Label_002F;
            }
            if (unit.ActionCount < monitor.turn)
            {
                goto Label_002F;
            }
            return 1;
        Label_002F:
            return 0;
        }

        private bool CheckMonitorGoalCondition(Unit unit, UnitMonitorCondition monitor)
        {
            if (unit == null)
            {
                goto Label_005F;
            }
            if (unit.IsGimmick != null)
            {
                goto Label_005F;
            }
            if (unit.CheckExistMap() == null)
            {
                goto Label_005F;
            }
            if (unit.x != monitor.x)
            {
                goto Label_005F;
            }
            if (unit.y != monitor.y)
            {
                goto Label_005F;
            }
            if (monitor.turn > 0)
            {
                goto Label_004C;
            }
            return 1;
        Label_004C:
            if (monitor.turn < unit.ActionCount)
            {
                goto Label_005F;
            }
            return 1;
        Label_005F:
            return 0;
        }

        private bool CheckMonitorGoalUnit(QuestMonitorCondition cond)
        {
            int num;
            UnitMonitorCondition condition;
            int num2;
            int num3;
            int num4;
            Unit unit;
            int num5;
            Unit unit2;
            if (cond.goals.Count <= 0)
            {
                goto Label_0221;
            }
            num = 0;
            goto Label_0210;
        Label_0018:
            condition = cond.goals[num];
            if (string.IsNullOrEmpty(condition.tag) != null)
            {
                goto Label_019C;
            }
            if ((condition.tag == "pall") == null)
            {
                goto Label_00A0;
            }
            num2 = 0;
            goto Label_008A;
        Label_0051:
            if (this.mUnits[num2].Side == null)
            {
                goto Label_006C;
            }
            goto Label_0086;
        Label_006C:
            if (this.CheckMonitorGoalCondition(this.mUnits[num2], condition) == null)
            {
                goto Label_0086;
            }
            return 1;
        Label_0086:
            num2 += 1;
        Label_008A:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0051;
            }
            goto Label_020C;
        Label_00A0:
            if ((condition.tag == "eall") == null)
            {
                goto Label_010C;
            }
            num3 = 0;
            goto Label_00F6;
        Label_00BC:
            if (this.mUnits[num3].Side == 1)
            {
                goto Label_00D8;
            }
            goto Label_00F2;
        Label_00D8:
            if (this.CheckMonitorGoalCondition(this.mUnits[num3], condition) == null)
            {
                goto Label_00F2;
            }
            return 1;
        Label_00F2:
            num3 += 1;
        Label_00F6:
            if (num3 < this.mUnits.Count)
            {
                goto Label_00BC;
            }
            goto Label_020C;
        Label_010C:
            if ((condition.tag == "nall") == null)
            {
                goto Label_017E;
            }
            num4 = 0;
            goto Label_0167;
        Label_0129:
            if (this.mUnits[num4].Side == 2)
            {
                goto Label_0146;
            }
            goto Label_0161;
        Label_0146:
            if (this.CheckMonitorGoalCondition(this.mUnits[num4], condition) == null)
            {
                goto Label_0161;
            }
            return 1;
        Label_0161:
            num4 += 1;
        Label_0167:
            if (num4 < this.mUnits.Count)
            {
                goto Label_0129;
            }
            goto Label_020C;
        Label_017E:
            unit = this.FindUnitByUniqueName(condition.tag);
            if (this.CheckMonitorGoalCondition(unit, condition) == null)
            {
                goto Label_019C;
            }
            return 1;
        Label_019C:
            if (string.IsNullOrEmpty(condition.iname) != null)
            {
                goto Label_020C;
            }
            num5 = 0;
            goto Label_01FA;
        Label_01B4:
            unit2 = this.mUnits[num5];
            if ((unit2.UnitParam.iname != condition.iname) == null)
            {
                goto Label_01E4;
            }
            goto Label_01F4;
        Label_01E4:
            if (this.CheckMonitorGoalCondition(unit2, condition) == null)
            {
                goto Label_01F4;
            }
            return 1;
        Label_01F4:
            num5 += 1;
        Label_01FA:
            if (num5 < this.mUnits.Count)
            {
                goto Label_01B4;
            }
        Label_020C:
            num += 1;
        Label_0210:
            if (num < cond.goals.Count)
            {
                goto Label_0018;
            }
        Label_0221:
            return 0;
        }

        private bool CheckMonitorWithdrawCondition(Unit unit)
        {
            if (unit == null)
            {
                goto Label_0023;
            }
            if (unit.IsDead == null)
            {
                goto Label_0023;
            }
            if (unit.IsUnitFlag(0x100000) == null)
            {
                goto Label_0023;
            }
            return 1;
        Label_0023:
            return 0;
        }

        private unsafe bool CheckMonitorWithdrawUnit(QuestMonitorCondition cond)
        {
            int num;
            UnitMonitorCondition condition;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            Unit unit2;
            List<Unit>.Enumerator enumerator2;
            Unit unit3;
            List<Unit>.Enumerator enumerator3;
            Unit unit4;
            Unit unit5;
            List<Unit>.Enumerator enumerator4;
            bool flag;
            if (cond.withdraw.Count == null)
            {
                goto Label_024D;
            }
            num = 0;
            goto Label_023C;
        Label_0017:
            condition = cond.withdraw[num];
            if (string.IsNullOrEmpty(condition.tag) != null)
            {
                goto Label_01B9;
            }
            if ((condition.tag == "pall") == null)
            {
                goto Label_00A8;
            }
            enumerator = this.mUnits.GetEnumerator();
        Label_0055:
            try
            {
                goto Label_0086;
            Label_005A:
                unit = &enumerator.Current;
                if (unit.Side == null)
                {
                    goto Label_0072;
                }
                goto Label_0086;
            Label_0072:
                if (this.CheckMonitorWithdrawCondition(unit) == null)
                {
                    goto Label_0086;
                }
                flag = 1;
                goto Label_024F;
            Label_0086:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_005A;
                }
                goto Label_00A3;
            }
            finally
            {
            Label_0097:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_00A3:
            goto Label_0238;
        Label_00A8:
            if ((condition.tag == "eall") == null)
            {
                goto Label_0122;
            }
            enumerator2 = this.mUnits.GetEnumerator();
        Label_00CA:
            try
            {
                goto Label_00FF;
            Label_00CF:
                unit2 = &enumerator2.Current;
                if (unit2.Side == 1)
                {
                    goto Label_00EA;
                }
                goto Label_00FF;
            Label_00EA:
                if (this.CheckMonitorWithdrawCondition(unit2) == null)
                {
                    goto Label_00FF;
                }
                flag = 1;
                goto Label_024F;
            Label_00FF:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00CF;
                }
                goto Label_011D;
            }
            finally
            {
            Label_0110:
                ((List<Unit>.Enumerator) enumerator2).Dispose();
            }
        Label_011D:
            goto Label_0238;
        Label_0122:
            if ((condition.tag == "nall") == null)
            {
                goto Label_019C;
            }
            enumerator3 = this.mUnits.GetEnumerator();
        Label_0144:
            try
            {
                goto Label_0179;
            Label_0149:
                unit3 = &enumerator3.Current;
                if (unit3.Side == 2)
                {
                    goto Label_0164;
                }
                goto Label_0179;
            Label_0164:
                if (this.CheckMonitorWithdrawCondition(unit3) == null)
                {
                    goto Label_0179;
                }
                flag = 1;
                goto Label_024F;
            Label_0179:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0149;
                }
                goto Label_0197;
            }
            finally
            {
            Label_018A:
                ((List<Unit>.Enumerator) enumerator3).Dispose();
            }
        Label_0197:
            goto Label_0238;
        Label_019C:
            unit4 = this.FindUnitByUniqueName(condition.tag);
            if (this.CheckMonitorWithdrawCondition(unit4) == null)
            {
                goto Label_01B9;
            }
            return 1;
        Label_01B9:
            if (string.IsNullOrEmpty(condition.iname) != null)
            {
                goto Label_0238;
            }
            enumerator4 = this.mUnits.GetEnumerator();
        Label_01D6:
            try
            {
                goto Label_021A;
            Label_01DB:
                unit5 = &enumerator4.Current;
                if ((unit5.UnitParam.iname != condition.iname) == null)
                {
                    goto Label_0205;
                }
                goto Label_021A;
            Label_0205:
                if (this.CheckMonitorWithdrawCondition(unit5) == null)
                {
                    goto Label_021A;
                }
                flag = 1;
                goto Label_024F;
            Label_021A:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_01DB;
                }
                goto Label_0238;
            }
            finally
            {
            Label_022B:
                ((List<Unit>.Enumerator) enumerator4).Dispose();
            }
        Label_0238:
            num += 1;
        Label_023C:
            if (num < cond.withdraw.Count)
            {
                goto Label_0017;
            }
        Label_024D:
            return 0;
        Label_024F:
            return flag;
        }

        public bool CheckMove(Unit self, Grid goal)
        {
            BattleMap map;
            Unit unit;
            this.DebugAssert((self == null) == 0, "self == null");
            if (goal != null)
            {
                goto Label_001A;
            }
            return 0;
        Label_001A:
            map = this.CurrentMap;
            this.DebugAssert((map == null) == 0, "map == null");
            unit = null;
            unit = this.FindUnitAtGrid(goal);
            if (unit == null)
            {
                goto Label_004C;
            }
            if (self == unit)
            {
                goto Label_004C;
            }
            return 0;
        Label_004C:
            return 1;
        }

        public bool CheckNextMap()
        {
            return (this.mMapIndex < (this.mMap.Count - 1));
        }

        private bool CheckPerfectAvoidSkill(Unit attacker, Unit defender, SkillData atkskl, LogSkill log)
        {
            int num;
            SkillData data;
            int num2;
            int num3;
            bool flag;
            DamageTypes types;
            if ((atkskl.IsDamagedSkill() != null) || (defender.IsEnableReactionCondition() != null))
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            num = 0;
            goto Label_0184;
        Label_001F:
            data = defender.BattleSkills[num];
            if ((data == null) || (data.IsReactionSkill() == null))
            {
                goto Label_0180;
            }
            if (data.EffectType == 0x15)
            {
                goto Label_004F;
            }
            goto Label_0180;
        Label_004F:
            if ((data.Timing == 6) || (data.Timing == 4))
            {
                goto Label_006C;
            }
            goto Label_0180;
        Label_006C:
            if (defender.IsEnableReactionSkill(data) != null)
            {
                goto Label_007D;
            }
            goto Label_0180;
        Label_007D:
            if (this.CheckSkillCondition(defender, data) != null)
            {
                goto Label_008F;
            }
            goto Label_0180;
        Label_008F:
            num2 = data.EffectRate;
            if ((num2 <= 0) || (num2 >= 100))
            {
                goto Label_00C0;
            }
            num3 = this.GetRandom() % 100;
            if (num3 <= num2)
            {
                goto Label_00C0;
            }
            goto Label_0180;
        Label_00C0:
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_00D1;
            }
            goto Label_0180;
        Label_00D1:
            flag = 0;
            switch ((data.ReactionDamageType - 1))
            {
                case 0:
                    goto Label_0138;

                case 1:
                    goto Label_00F6;

                case 2:
                    goto Label_0117;
            }
            goto Label_0159;
        Label_00F6:
            flag = (atkskl.IsPhysicalAttack() == null) ? 0 : data.IsReactionDet(atkskl.AttackDetailType);
            goto Label_0159;
        Label_0117:
            flag = (atkskl.IsMagicalAttack() == null) ? 0 : data.IsReactionDet(atkskl.AttackDetailType);
            goto Label_0159;
        Label_0138:
            flag = (atkskl.IsDamagedSkill() == null) ? 0 : data.IsReactionDet(atkskl.AttackDetailType);
        Label_0159:
            if (flag == null)
            {
                goto Label_0180;
            }
            if (data.SkillParam.count <= 0)
            {
                goto Label_017E;
            }
            defender.UpdateSkillUseCount(data, -1);
        Label_017E:
            return 1;
        Label_0180:
            num += 1;
        Label_0184:
            if (num < defender.BattleSkills.Count)
            {
                goto Label_001F;
            }
            return 0;
        }

        private bool CheckSearchMap(Unit self)
        {
            BattleMap map;
            Grid grid;
            int num;
            int num2;
            Grid grid2;
            DebugUtility.Assert((this.mSearchMap == null) == 0, "mSearchMap == null");
            map = this.CurrentMap;
            DebugUtility.Assert((map == null) == 0, "map == null");
            grid = map[self.x, self.y];
            DebugUtility.Assert((grid == null) == 0, "grid == null");
            num = 0;
            goto Label_00A4;
        Label_0059:
            num2 = 0;
            goto Label_008F;
        Label_0060:
            if (this.mSearchMap.get(num, num2) == null)
            {
                goto Label_008B;
            }
            grid2 = map[num, num2];
            if (self.CheckCollision(grid2) == null)
            {
                goto Label_008B;
            }
            return 1;
        Label_008B:
            num2 += 1;
        Label_008F:
            if (num2 < this.mSearchMap.h)
            {
                goto Label_0060;
            }
            num += 1;
        Label_00A4:
            if (num < this.mSearchMap.w)
            {
                goto Label_0059;
            }
            return 0;
        }

        private bool CheckSideAttack(Unit self, Unit target, SkillData skill)
        {
            return this.CheckSideAttack(self.x, self.y, target, skill);
        }

        private bool CheckSideAttack(int sx, int sy, Unit target, SkillData skill)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            num = 0;
            num2 = target.Direction;
            num3 = target.x - sx;
            num4 = target.y - sy;
            if (num3 <= 0)
            {
                goto Label_0024;
            }
            num = 0;
        Label_0024:
            if (num3 >= 0)
            {
                goto Label_002D;
            }
            num = 2;
        Label_002D:
            if (num4 <= 0)
            {
                goto Label_0036;
            }
            num = 1;
        Label_0036:
            if (num4 >= 0)
            {
                goto Label_003F;
            }
            num = 3;
        Label_003F:
            num5 = Unit.DIRECTION_OFFSETS[num, 0] + Unit.DIRECTION_OFFSETS[num2, 0];
            num6 = Unit.DIRECTION_OFFSETS[num, 1] + Unit.DIRECTION_OFFSETS[num2, 1];
            if (Math.Abs(num5) != 1)
            {
                goto Label_0091;
            }
            if (Math.Abs(num6) != 1)
            {
                goto Label_0091;
            }
            return 1;
        Label_0091:
            return 0;
        }

        private bool CheckSkillCondition(Unit self, SkillData skill)
        {
            ESkillCondition condition;
            if (self == null)
            {
                goto Label_000C;
            }
            if (skill != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            condition = skill.Condition;
            if (condition != 1)
            {
                goto Label_0023;
            }
            return self.IsDying();
        Label_0023:
            if (condition != 5)
            {
                goto Label_0032;
            }
            return skill.IsJudgeHp(self);
        Label_0032:
            return 1;
        }

        public bool CheckSkillScopeMultiPlay(Unit self, Unit target, int gx, int gy, SkillData skill)
        {
            bool flag;
            flag = 1;
            return flag;
        }

        public bool CheckSkillTarget(Unit self, Unit target, SkillData skill)
        {
            eSkillTargetEx ex;
            bool flag;
            ESkillTarget target2;
            eSkillTargetEx ex2;
            ESkillTarget target3;
            ESkillTarget target4;
            SkillEffectTypes types;
            this.DebugAssert((self == null) == 0, "self == null");
            this.DebugAssert((skill == null) == 0, "failed. skill != null");
            if (target != null)
            {
                goto Label_002C;
            }
            return 0;
        Label_002C:
            if (target.IsGimmick == null)
            {
                goto Label_0049;
            }
            if (this.IsTargetBreakUnit(self, target, skill) == null)
            {
                goto Label_0047;
            }
            return 1;
        Label_0047:
            return 0;
        Label_0049:
            ex = skill.SkillParam.TargetEx;
            if (target.CastSkill == null)
            {
                goto Label_009C;
            }
            if (target.CastSkill.CastType != 2)
            {
                goto Label_009C;
            }
            ex2 = skill.SkillParam.TargetEx;
            if (ex2 == 1)
            {
                goto Label_00A5;
            }
            if (ex2 == 2)
            {
                goto Label_00A5;
            }
            goto Label_0095;
            goto Label_0097;
        Label_0095:
            return 0;
        Label_0097:
            goto Label_00A5;
        Label_009C:
            if (ex != 2)
            {
                goto Label_00A5;
            }
            return 0;
        Label_00A5:
            flag = 0;
            switch (skill.Target)
            {
                case 0:
                    goto Label_00D5;

                case 1:
                    goto Label_00DF;

                case 2:
                    goto Label_00F0;

                case 3:
                    goto Label_00FE;

                case 4:
                    goto Label_0105;

                case 5:
                    goto Label_0112;
            }
            goto Label_018E;
        Label_00D5:
            flag = self == target;
            goto Label_0193;
        Label_00DF:
            flag = this.CheckEnemySide(self, target) == 0;
            goto Label_0193;
        Label_00F0:
            flag = this.CheckEnemySide(self, target);
            goto Label_0193;
        Label_00FE:
            flag = 1;
            goto Label_0193;
        Label_0105:
            flag = (self == target) == 0;
            goto Label_0193;
        Label_0112:
            if (skill.TeleportType == null)
            {
                goto Label_0187;
            }
            switch (skill.TeleportTarget)
            {
                case 0:
                    goto Label_0145;

                case 1:
                    goto Label_014F;

                case 2:
                    goto Label_0160;

                case 3:
                    goto Label_016E;

                case 4:
                    goto Label_0175;
            }
            goto Label_0182;
        Label_0145:
            flag = self == target;
            goto Label_0182;
        Label_014F:
            flag = this.CheckEnemySide(self, target) == 0;
            goto Label_0182;
        Label_0160:
            flag = this.CheckEnemySide(self, target);
            goto Label_0182;
        Label_016E:
            flag = 1;
            goto Label_0182;
        Label_0175:
            flag = (self == target) == 0;
        Label_0182:
            goto Label_0189;
        Label_0187:
            flag = 0;
        Label_0189:
            goto Label_0193;
        Label_018E:;
        Label_0193:
            if (flag != null)
            {
                goto Label_019B;
            }
            return 0;
        Label_019B:
            if (skill.EffectType == 7)
            {
                goto Label_01B0;
            }
            goto Label_01B7;
        Label_01B0:
            return target.IsDead;
        Label_01B7:;
        Label_01BC:
            return (target.IsDead == 0);
        }

        private bool CheckSkillTargetAI(Unit self, Unit target, SkillData skill)
        {
            bool flag;
            ESkillTarget target2;
            ESkillTarget target3;
            SkillEffectTypes types;
            if (this.CheckSkillTarget(self, target, skill) == null)
            {
                goto Label_0110;
            }
            flag = 0;
            switch ((skill.Target - 3))
            {
                case 0:
                    goto Label_0030;

                case 1:
                    goto Label_0030;

                case 2:
                    goto Label_0037;
            }
            goto Label_0068;
        Label_0030:
            flag = 1;
            goto Label_0068;
        Label_0037:
            if (skill.TeleportType == null)
            {
                goto Label_0068;
            }
            target3 = skill.TeleportTarget;
            if (target3 == 3)
            {
                goto Label_005C;
            }
            if (target3 == 4)
            {
                goto Label_005C;
            }
            goto Label_0063;
        Label_005C:
            flag = 1;
        Label_0063:;
        Label_0068:
            if (flag == null)
            {
                goto Label_010E;
            }
            switch ((skill.EffectType - 4))
            {
                case 0:
                    goto Label_00CE;

                case 1:
                    goto Label_00CE;

                case 2:
                    goto Label_0105;

                case 3:
                    goto Label_00CE;

                case 4:
                    goto Label_00CE;

                case 5:
                    goto Label_0105;

                case 6:
                    goto Label_0105;

                case 7:
                    goto Label_0105;

                case 8:
                    goto Label_00CE;

                case 9:
                    goto Label_0105;

                case 10:
                    goto Label_00CE;

                case 11:
                    goto Label_00DA;

                case 12:
                    goto Label_00CE;

                case 13:
                    goto Label_0100;

                case 14:
                    goto Label_0100;

                case 15:
                    goto Label_00CE;

                case 0x10:
                    goto Label_0105;

                case 0x11:
                    goto Label_0105;

                case 0x12:
                    goto Label_0100;
            }
            goto Label_0105;
        Label_00CE:
            return (this.CheckEnemySide(self, target) == 0);
        Label_00DA:
            if (skill.EffectValue <= 0)
            {
                goto Label_00F7;
            }
            return (this.CheckEnemySide(self, target) == 0);
        Label_00F7:
            return this.CheckEnemySide(self, target);
        Label_0100:
            goto Label_010E;
        Label_0105:
            return this.CheckEnemySide(self, target);
        Label_010E:
            return 1;
        Label_0110:
            return 0;
        }

        private bool CheckUseSkill(Unit self, SkillData skill, bool is_no_add_rate)
        {
            int num;
            bool flag;
            CondEffect effect;
            bool flag2;
            int num2;
            CondEffect effect2;
            bool flag3;
            int num3;
            EUnitCondition condition;
            bool flag4;
            bool flag5;
            int num4;
            int num5;
            int num6;
            int num7;
            if (this.QuestType != 2)
            {
                goto Label_0030;
            }
            num = this.GetRandom() % 100;
            if (num >= skill.SkillParam.rate)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            return 1;
        Label_0030:
            if (skill.IsNormalAttack() == null)
            {
                goto Label_003D;
            }
            return 1;
        Label_003D:
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_007A;
            }
            if (((skill.IsJewelAttack() == null) || (self.AI == null)) || (self.AI.CheckFlag(0x80) == null))
            {
                goto Label_02EA;
            }
            return 0;
            goto Label_02EA;
        Label_007A:
            if (self.GetRageTarget() == null)
            {
                goto Label_0087;
            }
            return 0;
        Label_0087:
            flag = 0;
            if (skill.IsHealSkill() == null)
            {
                goto Label_0099;
            }
            goto Label_020C;
        Label_0099:
            if (skill.IsSupportSkill() == null)
            {
                goto Label_00AB;
            }
            flag = 1;
            goto Label_020C;
        Label_00AB:
            if (skill.IsTrickSkill() == null)
            {
                goto Label_00BB;
            }
            goto Label_020C;
        Label_00BB:
            if (skill.EffectType != 12)
            {
                goto Label_0156;
            }
            effect = skill.GetCondEffect(0);
            if (((effect != null) && (effect.param != null)) && ((effect.param.conditions != null) && (effect.param.type == 1)))
            {
                goto Label_0104;
            }
            return 0;
        Label_0104:
            flag2 = 0;
            num2 = 0;
            goto Label_0135;
        Label_010E:
            if (this.GetFailCondSelfSideUnitCount(self, effect.param.conditions[num2]) == null)
            {
                goto Label_012F;
            }
            flag2 = 1;
            goto Label_0149;
        Label_012F:
            num2 += 1;
        Label_0135:
            if (num2 < ((int) effect.param.conditions.Length))
            {
                goto Label_010E;
            }
        Label_0149:
            if (flag2 != null)
            {
                goto Label_020C;
            }
            return 0;
            goto Label_020C;
        Label_0156:
            if (skill.EffectType != 11)
            {
                goto Label_0168;
            }
            goto Label_020C;
        Label_0168:
            if (skill.EffectType != 13)
            {
                goto Label_020C;
            }
            effect2 = skill.GetCondEffect(0);
            if (((effect2 != null) && (effect2.param != null)) && ((effect2.param.conditions != null) && (effect2.param.type == 5)))
            {
                goto Label_01B6;
            }
            return 0;
        Label_01B6:
            flag3 = 0;
            num3 = 0;
            goto Label_01EE;
        Label_01C1:
            condition = effect2.param.conditions[num3];
            if (this.IsFailCondSkillUseEnemies(self, condition) == null)
            {
                goto Label_01E8;
            }
            flag3 = 1;
            goto Label_0203;
        Label_01E8:
            num3 += 1;
        Label_01EE:
            if (num3 < ((int) effect2.param.conditions.Length))
            {
                goto Label_01C1;
            }
        Label_0203:
            if (flag3 != null)
            {
                goto Label_020C;
            }
            return 0;
        Label_020C:
            if (flag == null)
            {
                goto Label_02EA;
            }
            flag4 = 0;
            flag5 = 1;
            if (self.AI.DisableSupportActionHpBorder == null)
            {
                goto Label_029D;
            }
            num4 = (self.MaximumStatus.param.hp == null) ? 100 : ((100 * self.CurrentStatus.param.hp) / self.MaximumStatus.param.hp);
            flag4 = 1;
            flag5 &= (self.AI.DisableSupportActionHpBorder < num4) == 0;
        Label_029D:
            if (self.AI.DisableSupportActionMemberBorder == null)
            {
                goto Label_02DA;
            }
            num5 = this.GetAliveUnitCount(self);
            flag4 = 1;
            flag5 &= (self.AI.DisableSupportActionMemberBorder < num5) == 0;
        Label_02DA:
            if (flag4 == null)
            {
                goto Label_02EA;
            }
            if (flag5 == null)
            {
                goto Label_02EA;
            }
            return 0;
        Label_02EA:
            if (self.IsPartyMember == null)
            {
                goto Label_031B;
            }
            num6 = this.GetRandom() % 100;
            if (num6 >= skill.SkillParam.rate)
            {
                goto Label_0319;
            }
            return 0;
        Label_0319:
            return 1;
        Label_031B:
            if (skill.UseCondition == null)
            {
                goto Label_0348;
            }
            if (skill.UseCondition.type == null)
            {
                goto Label_0348;
            }
            if (skill.UseCondition.unlock != null)
            {
                goto Label_0348;
            }
            return 0;
        Label_0348:
            if (is_no_add_rate == null)
            {
                goto Label_0350;
            }
            return 1;
        Label_0350:
            num7 = this.GetRandom() % 100;
            if (num7 >= skill.UseRate)
            {
                goto Label_036F;
            }
            return 1;
        Label_036F:
            return 0;
        }

        private bool CheckWeakPoint(Unit self, Unit target, SkillData skill)
        {
            if (skill.ElementType != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            return (skill.ElementType == target.GetWeakElement());
        }

        private void CheckWithDrawUnit(Unit target)
        {
            this.GridEventStart(target, target, 5, null);
            this.GridEventStart(target, target, 6, null);
            this.GridEventStart(target, target, 7, null);
            this.GridEventStart(target, target, 8, null);
            this.GridEventStart(target, target, 9, null);
            return;
        }

        private void ClearAI()
        {
            BattleMap map;
            int num;
            int num2;
            map = this.CurrentMap;
            if (this.mEnemyPriorities != null)
            {
                goto Label_0028;
            }
            this.mEnemyPriorities = new List<Unit>(this.mUnits.Count);
        Label_0028:
            this.mEnemyPriorities.Clear();
            if (this.mGimmickPriorities != null)
            {
                goto Label_0054;
            }
            this.mGimmickPriorities = new List<Unit>(this.mUnits.Count);
        Label_0054:
            this.mGimmickPriorities.Clear();
            num = map.Width;
            num2 = map.Height;
            if (this.mMoveMap != null)
            {
                goto Label_0085;
            }
            this.mMoveMap = new GridMap<int>(num, num2);
        Label_0085:
            if (this.mMoveMap.w != num)
            {
                goto Label_00A7;
            }
            if (this.mMoveMap.h == num2)
            {
                goto Label_00B4;
            }
        Label_00A7:
            this.mMoveMap.resize(num, num2);
        Label_00B4:
            this.mMoveMap.fill(-1);
            if (this.mRangeMap != null)
            {
                goto Label_00D8;
            }
            this.mRangeMap = new GridMap<bool>(num, num2);
        Label_00D8:
            if (this.mRangeMap.w != num)
            {
                goto Label_00FA;
            }
            if (this.mRangeMap.h == num2)
            {
                goto Label_0107;
            }
        Label_00FA:
            this.mRangeMap.resize(num, num2);
        Label_0107:
            this.mRangeMap.fill(0);
            if (this.mScopeMap != null)
            {
                goto Label_012B;
            }
            this.mScopeMap = new GridMap<bool>(num, num2);
        Label_012B:
            if (this.mScopeMap.w != num)
            {
                goto Label_014D;
            }
            if (this.mScopeMap.h == num2)
            {
                goto Label_015A;
            }
        Label_014D:
            this.mScopeMap.resize(num, num2);
        Label_015A:
            this.mScopeMap.fill(0);
            if (this.mSearchMap != null)
            {
                goto Label_017E;
            }
            this.mSearchMap = new GridMap<bool>(num, num2);
        Label_017E:
            if (this.mSearchMap.w != num)
            {
                goto Label_01A0;
            }
            if (this.mSearchMap.h == num2)
            {
                goto Label_01AD;
            }
        Label_01A0:
            this.mSearchMap.resize(num, num2);
        Label_01AD:
            this.mSearchMap.fill(0);
            if (this.mSafeMap != null)
            {
                goto Label_01D1;
            }
            this.mSafeMap = new GridMap<int>(num, num2);
        Label_01D1:
            if (this.mSafeMap.w != num)
            {
                goto Label_01F3;
            }
            if (this.mSafeMap.h == num2)
            {
                goto Label_0200;
            }
        Label_01F3:
            this.mSafeMap.resize(num, num2);
        Label_0200:
            this.mSafeMap.fill(-1);
            if (this.mSafeMapEx != null)
            {
                goto Label_0224;
            }
            this.mSafeMapEx = new GridMap<int>(num, num2);
        Label_0224:
            if (this.mSafeMapEx.w != num)
            {
                goto Label_0246;
            }
            if (this.mSafeMapEx.h == num2)
            {
                goto Label_0253;
            }
        Label_0246:
            this.mSafeMapEx.resize(num, num2);
        Label_0253:
            this.mSafeMapEx.fill(-1);
            this.mTrickMap.Initialize(num, num2);
            this.mTrickMap.Clear();
            this.RefreshTreasureTargetAI();
            return;
        }

        public RandXorshift CloneRand()
        {
            return this.mRand.Clone();
        }

        public RandXorshift CloneRandDamage()
        {
            RandXorshift xorshift;
            xorshift = this.mRandDamage.Clone();
            if (xorshift == null)
            {
                goto Label_001E;
            }
            xorshift.Seed(this.mSeedDamage);
        Label_001E:
            return xorshift;
        }

        public bool CommandWait(EUnitDirection dir)
        {
            Unit unit;
            if (this.CurrentUnit.IsDead != null)
            {
                goto Label_001E;
            }
            this.CurrentUnit.Direction = dir;
        Label_001E:
            return this.CommandWait(0);
        }

        public bool CommandWait(bool is_skip_event)
        {
            LogMapWait wait;
            this.DebugAssert(this.IsMapCommand, "マップコマンド中のみコール可");
            this.Log<LogMapWait>().self = this.CurrentUnit;
            if (is_skip_event != null)
            {
                goto Label_0045;
            }
            this.TrickActionEndEffect(this.CurrentUnit, 1);
            this.ExecuteEventTriggerOnGrid(this.CurrentUnit, 0);
        Label_0045:
            this.InternalLogUnitEnd();
            return 1;
        }

        public bool ConditionalUnitEnd(bool ignoreMoveAndAction)
        {
            Unit unit;
            bool flag;
            bool flag2;
            this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
            this.DebugAssert(this.IsBattleFlag(1), "マップ開始済みのみコール可");
            this.DebugAssert(this.IsBattleFlag(2), "ユニット開始済みのみコール可");
            if (this.CheckJudgeBattle() == null)
            {
                goto Label_004E;
            }
            this.CalcQuestRecord();
            this.MapEnd();
            return 1;
        Label_004E:
            unit = this.CurrentUnit;
            this.DebugAssert((unit == null) == 0, "unit == null");
            if (unit.IsDead == null)
            {
                goto Label_007A;
            }
            this.InternalLogUnitEnd();
            return 1;
        Label_007A:
            flag = unit.IsEnableMoveCondition(0);
            flag2 = unit.IsEnableActionCondition();
            if (ignoreMoveAndAction != null)
            {
                goto Label_00A5;
            }
            if (flag2 != null)
            {
                goto Label_00A5;
            }
            if (flag != null)
            {
                goto Label_00A5;
            }
            this.CommandWait(0);
            return 1;
        Label_00A5:
            return 0;
        }

        private void CondSkill(ESkillTiming timing, Unit self, Unit target, SkillData skill, bool is_passive, LogSkill log, SkillEffectTargets cond_target, bool is_same_ow)
        {
            CondEffect effect;
            ConditionEffectTypes types;
            int num;
            int num2;
            LogSkill.Target target2;
            EnchantParam param;
            EnchantParam param2;
            int num3;
            EUnitCondition condition;
            int num4;
            EUnitCondition condition2;
            EnchantParam param3;
            EnchantParam param4;
            int num5;
            EUnitCondition condition3;
            EnchantParam param5;
            EnchantParam param6;
            int num6;
            EUnitCondition condition4;
            int num7;
            EUnitCondition condition5;
            int num8;
            CondAttachment attachment;
            SkillEffectTargets targets;
            ConditionEffectTypes types2;
            if (timing == skill.Timing)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            effect = skill.GetCondEffect(cond_target);
            types = 0;
            if (effect == null)
            {
                goto Label_0091;
            }
            if (effect.param == null)
            {
                goto Label_0091;
            }
            if (effect.CheckEnableCondTarget(target) != null)
            {
                goto Label_0038;
            }
            return;
        Label_0038:
            if (effect.param.type == null)
            {
                goto Label_0091;
            }
            if (effect.param.conditions == null)
            {
                goto Label_0091;
            }
            num = effect.rate;
            if (num <= 0)
            {
                goto Label_0085;
            }
            if (num >= 100)
            {
                goto Label_0085;
            }
            num2 = this.GetRandom() % 100;
            if (num2 <= num)
            {
                goto Label_0085;
            }
            return;
        Label_0085:
            types = effect.param.type;
        Label_0091:
            target2 = null;
            if (log == null)
            {
                goto Label_00F2;
            }
            targets = cond_target;
            if (targets == null)
            {
                goto Label_00B3;
            }
            if (targets == 1)
            {
                goto Label_00C2;
            }
            goto Label_00F1;
        Label_00B3:
            target2 = log.FindTarget(target);
            goto Label_00F2;
        Label_00C2:
            if (effect == null)
            {
                goto Label_00D3;
            }
            if (effect.param != null)
            {
                goto Label_00D4;
            }
        Label_00D3:
            return;
        Label_00D4:
            if (self != target)
            {
                goto Label_00F2;
            }
            log.self_effect.target = self;
            goto Label_00F2;
        Label_00F1:
            return;
        Label_00F2:
            types2 = types;
            switch (types2)
            {
                case 0:
                    goto Label_0119;

                case 1:
                    goto Label_01BF;

                case 2:
                    goto Label_0222;

                case 3:
                    goto Label_0463;

                case 4:
                    goto Label_0350;

                case 5:
                    goto Label_0512;
            }
            goto Label_05B5;
        Label_0119:
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_05B5;
            }
            param = self.CurrentStatus.enchant_assist;
            param2 = target.CurrentStatus.enchant_resist;
            num3 = 0;
            goto Label_01A9;
        Label_0147:
            condition = 1L << (num3 & 0x3f);
            if (target.IsDisableUnitCondition(condition) != null)
            {
                goto Label_01A3;
            }
            if (this.CheckFailCondition(target, param[condition], param2[condition], condition) == null)
            {
                goto Label_01A3;
            }
            this.FailCondition(self, target, skill, cond_target, null, 2, 0, condition, 0, 0, 0, is_passive, 0, log, is_same_ow);
        Label_01A3:
            num3 += 1;
        Label_01A9:
            if (num3 < Unit.MAX_UNIT_CONDITION)
            {
                goto Label_0147;
            }
            goto Label_05B5;
        Label_01BF:
            if (effect == null)
            {
                goto Label_05B5;
            }
            if (effect.param == null)
            {
                goto Label_05B5;
            }
            if (effect.param.conditions == null)
            {
                goto Label_05B5;
            }
            num4 = 0;
            goto Label_0209;
        Label_01E8:
            condition2 = effect.param.conditions[num4];
            this.CureCondition(target, condition2, log);
            num4 += 1;
        Label_0209:
            if (num4 < ((int) effect.param.conditions.Length))
            {
                goto Label_01E8;
            }
            goto Label_05B5;
        Label_0222:
            if (effect == null)
            {
                goto Label_05B5;
            }
            if (effect.param == null)
            {
                goto Label_05B5;
            }
            if (effect.param.conditions == null)
            {
                goto Label_05B5;
            }
            if (effect.value == null)
            {
                goto Label_05B5;
            }
            param3 = self.CurrentStatus.enchant_assist;
            param4 = target.CurrentStatus.enchant_resist;
            self.CurrentStatus.enchant_assist.CopyTo(param3);
            num5 = 0;
            goto Label_0337;
        Label_0287:
            condition3 = effect.param.conditions[num5];
            if (target.IsDisableUnitCondition(condition3) != null)
            {
                goto Label_0331;
            }
            if (this.CheckFailCondition(target, param3[condition3] + effect.value, param4[condition3], condition3) == null)
            {
                goto Label_0331;
            }
            this.FailCondition(self, target, skill, cond_target, effect.param, effect.param.type, effect.param.cond, condition3, effect.param.chk_target, effect.param.chk_timing, effect.turn, is_passive, effect.IsCurse, log, is_same_ow);
        Label_0331:
            num5 += 1;
        Label_0337:
            if (num5 < ((int) effect.param.conditions.Length))
            {
                goto Label_0287;
            }
            goto Label_05B5;
        Label_0350:
            if (effect == null)
            {
                goto Label_05B5;
            }
            if (effect.param == null)
            {
                goto Label_05B5;
            }
            if (effect.param.conditions == null)
            {
                goto Label_05B5;
            }
            if (effect.value == null)
            {
                goto Label_05B5;
            }
            param5 = self.CurrentStatus.enchant_assist;
            param6 = target.CurrentStatus.enchant_resist;
            num6 = (int) (((ulong) this.GetRandom()) % ((long) ((int) effect.param.conditions.Length)));
            condition4 = effect.param.conditions[num6];
            if (target.IsDisableUnitCondition(condition4) != null)
            {
                goto Label_05B5;
            }
            if (this.CheckFailCondition(target, param5[condition4] + effect.value, param6[condition4], condition4) == null)
            {
                goto Label_05B5;
            }
            this.FailCondition(self, target, skill, cond_target, effect.param, effect.param.type, effect.param.cond, condition4, effect.param.chk_target, effect.param.chk_timing, effect.turn, is_passive, effect.IsCurse, log, is_same_ow);
            goto Label_05B5;
        Label_0463:
            if (effect == null)
            {
                goto Label_05B5;
            }
            if (effect.param == null)
            {
                goto Label_05B5;
            }
            if (effect.param.conditions == null)
            {
                goto Label_05B5;
            }
            num7 = 0;
            goto Label_04F9;
        Label_048C:
            condition5 = effect.param.conditions[num7];
            this.FailCondition(self, target, skill, cond_target, effect.param, effect.param.type, effect.param.cond, condition5, effect.param.chk_target, effect.param.chk_timing, effect.turn, is_passive, effect.IsCurse, log, is_same_ow);
            num7 += 1;
        Label_04F9:
            if (num7 < ((int) effect.param.conditions.Length))
            {
                goto Label_048C;
            }
            goto Label_05B5;
        Label_0512:
            if (effect == null)
            {
                goto Label_05B5;
            }
            if (effect.param == null)
            {
                goto Label_05B5;
            }
            if (effect.param.conditions == null)
            {
                goto Label_05B5;
            }
            num8 = 0;
            goto Label_059C;
        Label_053B:
            attachment = this.CreateCondAttachment(self, target, skill, cond_target, effect.param, types, effect.param.cond, effect.param.conditions[num8], effect.param.chk_target, effect.param.chk_timing, effect.turn, is_passive, 0);
            target.SetCondAttachment(attachment);
            num8 += 1;
        Label_059C:
            if (num8 < ((int) effect.param.conditions.Length))
            {
                goto Label_053B;
            }
        Label_05B5:
            return;
        }

        private void CondSkillSetRateLog(ESkillTiming timing, Unit self, Unit target, SkillData skill, LogSkill log)
        {
            CondEffect effect;
            LogSkill.Target target2;
            EnchantParam param;
            EnchantParam param2;
            EUnitCondition condition;
            EUnitCondition[] conditionArray;
            int num;
            LogSkill.Target.CondHit hit;
            int num2;
            int num3;
            ConditionEffectTypes types;
            if (self == null)
            {
                goto Label_001A;
            }
            if (target == null)
            {
                goto Label_001A;
            }
            if (skill == null)
            {
                goto Label_001A;
            }
            if (log != null)
            {
                goto Label_001B;
            }
        Label_001A:
            return;
        Label_001B:
            if (timing == skill.Timing)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            effect = skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_0043;
            }
            if (effect.param != null)
            {
                goto Label_0044;
            }
        Label_0043:
            return;
        Label_0044:
            if (effect.CheckEnableCondTarget(target) != null)
            {
                goto Label_0051;
            }
            return;
        Label_0051:
            if (effect.param.type == null)
            {
                goto Label_0081;
            }
            if (effect.param.conditions == null)
            {
                goto Label_0081;
            }
            if (effect.value != null)
            {
                goto Label_0082;
            }
        Label_0081:
            return;
        Label_0082:
            target2 = log.FindTarget(target);
            if (target2 != null)
            {
                goto Label_0092;
            }
            return;
        Label_0092:
            target2.CondHitLists.Clear();
            switch ((effect.param.type - 2))
            {
                case 0:
                    goto Label_00C4;

                case 1:
                    goto Label_00C4;

                case 2:
                    goto Label_00C4;
            }
            goto Label_01B2;
        Label_00C4:
            param = self.CurrentStatus.enchant_assist;
            param2 = target.CurrentStatus.enchant_resist;
            conditionArray = effect.param.conditions;
            num = 0;
            goto Label_01A2;
        Label_00F1:
            condition = conditionArray[num];
            hit = new LogSkill.Target.CondHit(condition, 0);
            if (target.IsDisableUnitCondition(condition) != null)
            {
                goto Label_018F;
            }
            num2 = effect.rate;
            if (num2 <= 0)
            {
                goto Label_012D;
            }
            if (num2 <= 100)
            {
                goto Label_0131;
            }
        Label_012D:
            num2 = 100;
        Label_0131:
            if (effect.param.type == 3)
            {
                goto Label_0186;
            }
            num3 = (param[condition] + effect.value) - param2[condition];
            num3 = Math.Max(0, Math.Min(num3, 100));
            num2 = (num2 * num3) / 100;
        Label_0186:
            hit.Per = num2;
        Label_018F:
            target2.CondHitLists.Add(hit);
            num += 1;
        Label_01A2:
            if (num < ((int) conditionArray.Length))
            {
                goto Label_00F1;
            }
        Label_01B2:
            return;
        }

        public unsafe List<Unit> ContinueStart(long btlid, int seed)
        {
            List<Unit> list;
            int num;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            List<Unit> list2;
            int num2;
            Unit unit2;
            bool flag;
            Grid grid;
            int num3;
            Unit unit3;
            int num4;
            Unit unit4;
            int num5;
            Unit unit5;
            int num6;
            Unit unit6;
            bool flag2;
            Grid grid2;
            Unit unit7;
            List<Unit>.Enumerator enumerator2;
            Grid grid3;
            list = new List<Unit>(MAX_UNITS);
            this.mBtlID = btlid;
            this.mContinueCount += 1;
            this.mClockTime = 0;
            num = 0;
            goto Label_004E;
        Label_0033:
            this.Units[num].ChargeTime = 0;
            num += 1;
        Label_004E:
            if (num < this.Units.Count)
            {
                goto Label_0033;
            }
            enumerator = this.mPlayer.GetEnumerator();
        Label_006B:
            try
            {
                goto Label_00BC;
            Label_0070:
                unit = &enumerator.Current;
                if (unit.IsDead == null)
                {
                    goto Label_0088;
                }
                goto Label_00BC;
            Label_0088:
                if (unit.IsNPC == null)
                {
                    goto Label_00A3;
                }
                if (unit.IsEntry != null)
                {
                    goto Label_00A3;
                }
                goto Label_00BC;
            Label_00A3:
                unit.SetUnitFlag(0x2000, 1);
                unit.ForceDead();
                list.Add(unit);
            Label_00BC:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0070;
                }
                goto Label_00D9;
            }
            finally
            {
            Label_00CD:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_00D9:
            list2 = new List<Unit>();
            num2 = 0;
            goto Label_01B0;
        Label_00E8:
            unit2 = this.mPlayer[num2];
            if (unit2.IsNPC == null)
            {
                goto Label_0114;
            }
            if (unit2.IsEntry != null)
            {
                goto Label_0114;
            }
            goto Label_01AA;
        Label_0114:
            flag = unit2.IsDead;
            this.EventTriggerWithdrawContinue(unit2);
            unit2.NotifyContinue();
            grid = this.GetCorrectDuplicatePosition(unit2);
            unit2.x = grid.x;
            unit2.y = grid.y;
            if (flag != null)
            {
                goto Label_015F;
            }
            goto Label_01AA;
        Label_015F:
            unit2.SetUnitFlag(1, 1);
            if (this.mStartingMembers.Contains(unit2) != null)
            {
                goto Label_0194;
            }
            unit2.ChargeTime = 0;
            unit2.IsSub = 1;
            goto Label_01AA;
        Label_0194:
            this.Log<LogUnitEntry>().self = unit2;
            list2.Add(unit2);
        Label_01AA:
            num2 += 1;
        Label_01B0:
            if (num2 < this.mPlayer.Count)
            {
                goto Label_00E8;
            }
            num3 = 0;
            goto Label_01E3;
        Label_01CA:
            unit3 = list2[num3];
            this.BeginBattlePassiveSkill(unit3);
            num3 += 1;
        Label_01E3:
            if (num3 < list2.Count)
            {
                goto Label_01CA;
            }
            num4 = 0;
            goto Label_025E;
        Label_01F9:
            unit4 = list2[num4];
            unit4.UpdateBuffEffects();
            unit4.CalcCurrentStatus(0, 0);
            unit4.CurrentStatus.param.hp = unit4.MaximumStatus.param.hp;
            unit4.CurrentStatus.param.mp = unit4.MaximumStatus.param.mp;
            num4 += 1;
        Label_025E:
            if (num4 < list2.Count)
            {
                goto Label_01F9;
            }
            num5 = 0;
            goto Label_028E;
        Label_0274:
            unit5 = list2[num5];
            this.UseAutoSkills(unit5);
            num5 += 1;
        Label_028E:
            if (num5 < list2.Count)
            {
                goto Label_0274;
            }
            num6 = 0;
            goto Label_0333;
        Label_02A4:
            unit6 = this.Units[num6];
            if (unit6.IsBreakObj != null)
            {
                goto Label_02C4;
            }
            goto Label_032D;
        Label_02C4:
            if (unit6.CheckLoseEventTrigger() != null)
            {
                goto Label_02D5;
            }
            goto Label_032D;
        Label_02D5:
            flag2 = unit6.IsDead;
            unit6.NotifyContinue();
            grid2 = this.GetCorrectDuplicatePosition(unit6);
            unit6.x = grid2.x;
            unit6.y = grid2.y;
            if (flag2 != null)
            {
                goto Label_0317;
            }
            goto Label_032D;
        Label_0317:
            unit6.SetUnitFlag(1, 1);
            this.Log<LogUnitEntry>().self = unit6;
        Label_032D:
            num6 += 1;
        Label_0333:
            if (num6 < this.Units.Count)
            {
                goto Label_02A4;
            }
            if (this.CheckMonitorWithdrawUnit(this.CurrentMap.LoseMonitorCondition) == null)
            {
                goto Label_03FB;
            }
            enumerator2 = this.Enemys.GetEnumerator();
        Label_0368:
            try
            {
                goto Label_03DD;
            Label_036D:
                unit7 = &enumerator2.Current;
                if (this.CheckMonitorWithdrawCondition(unit7) != null)
                {
                    goto Label_0388;
                }
                goto Label_03DD;
            Label_0388:
                if (this.EventTriggerWithdrawContinue(unit7) != null)
                {
                    goto Label_039A;
                }
                goto Label_03DD;
            Label_039A:
                unit7.NotifyContinue();
                grid3 = this.GetCorrectDuplicatePosition(unit7);
                unit7.x = grid3.x;
                unit7.y = grid3.y;
                unit7.SetUnitFlag(1, 1);
                this.Log<LogUnitEntry>().self = unit7;
            Label_03DD:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_036D;
                }
                goto Label_03FB;
            }
            finally
            {
            Label_03EE:
                ((List<Unit>.Enumerator) enumerator2).Dispose();
            }
        Label_03FB:
            this.mRecord.result = 0;
            this.SetBattleFlag(1, 1);
            this.SetBattleFlag(2, 0);
            if (this.CheckEnableSuspendSave() == null)
            {
                goto Label_0429;
            }
            this.SaveSuspendData();
        Label_0429:
            this.NextOrder(1, 1, 0, 1);
            return list;
        }

        public List<Unit> CreateAttackTargetsAI(Unit self, SkillData skill, bool is_move)
        {
            GridMap<bool> map;
            List<Unit> list;
            int num;
            map = this.CreateSkillScopeMapAll(self, skill, is_move);
            list = new List<Unit>(this.mUnits.Count);
            this.SearchTargetsInGridMap(self, skill, map, list);
            num = 0;
            goto Label_005C;
        Label_002D:
            if (this.CheckSkillTargetAI(self, list[num], skill) == null)
            {
                goto Label_0046;
            }
            goto Label_0058;
        Label_0046:
            list.Remove(list[num--]);
        Label_0058:
            num += 1;
        Label_005C:
            if (num < list.Count)
            {
                goto Label_002D;
            }
            return list;
        }

        public BuffAttachment CreateBuffAttachment(Unit user, Unit target, SkillData skill, SkillEffectTargets skilltarget, BuffEffectParam param, BuffTypes buffType, bool is_negative_value_is_buff, SkillParamCalcTypes calcType, BaseStatus status, ESkillCondition cond, int turn, EffectCheckTargets chktgt, EffectCheckTimings timing, bool is_passive, int dupli)
        {
            BuffAttachment attachment;
            EffectCheckTargets targets;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_003E;
            }
            if (param == null)
            {
                goto Label_003A;
            }
            if (0 >= param.rate)
            {
                goto Label_003A;
            }
            if (param.rate >= 100)
            {
                goto Label_003A;
            }
            return null;
        Label_003A:
            timing = 10;
        Label_003E:
            attachment = new BuffAttachment(param);
            attachment.user = user;
            attachment.turn = turn;
            attachment.skill = skill;
            attachment.skilltarget = skilltarget;
            attachment.IsPassive = is_passive;
            attachment.BuffType = buffType;
            attachment.IsNegativeValueIsBuff = is_negative_value_is_buff;
            attachment.CalcType = calcType;
            attachment.CheckTiming = timing;
            attachment.CheckTarget = null;
            attachment.UseCondition = cond;
            attachment.DuplicateCount = dupli;
            targets = chktgt;
            if (targets == null)
            {
                goto Label_00C2;
            }
            if (targets == 1)
            {
                goto Label_00CE;
            }
            goto Label_00DA;
        Label_00C2:
            attachment.CheckTarget = target;
            goto Label_00DA;
        Label_00CE:
            attachment.CheckTarget = user;
        Label_00DA:
            if (user == null)
            {
                goto Label_0163;
            }
            if (timing == 1)
            {
                goto Label_0163;
            }
            if (is_passive != null)
            {
                goto Label_0163;
            }
            if (param == null)
            {
                goto Label_0102;
            }
            if (param.IsNoBuffTurn != null)
            {
                goto Label_0163;
            }
        Label_0102:
            if (buffType != null)
            {
                goto Label_0132;
            }
            attachment.turn += user.CurrentStatus[20];
        Label_0132:
            if (buffType != 1)
            {
                goto Label_0163;
            }
            attachment.turn += user.CurrentStatus[0x15];
        Label_0163:
            status.CopyTo(attachment.status);
            return attachment;
        }

        public CondAttachment CreateCondAttachment(Unit user, Unit target, SkillData skill, SkillEffectTargets skilltarget, CondEffectParam param, ConditionEffectTypes type, ESkillCondition cond, EUnitCondition condition, EffectCheckTargets chktgt, EffectCheckTimings timing, int turn, bool is_passive, bool is_curse)
        {
            CondAttachment attachment;
            EffectCheckTargets targets;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_000E;
            }
            return null;
        Label_000E:
            if (type != null)
            {
                goto Label_0028;
            }
            if (skill == null)
            {
                goto Label_0028;
            }
            if (skill.IsDamagedSkill() != null)
            {
                goto Label_0028;
            }
            return null;
        Label_0028:
            attachment = new CondAttachment(param);
            attachment.user = user;
            attachment.turn = turn;
            attachment.skill = skill;
            attachment.IsPassive = is_passive;
            attachment.CondType = type;
            attachment.Condition = condition;
            attachment.CheckTarget = null;
            attachment.CheckTiming = timing;
            attachment.UseCondition = cond;
            attachment.skilltarget = skilltarget;
            attachment.SetupLinkageBuff();
            targets = chktgt;
            if (targets == null)
            {
                goto Label_00A2;
            }
            if (targets == 1)
            {
                goto Label_00AE;
            }
            goto Label_00BA;
        Label_00A2:
            attachment.CheckTarget = target;
            goto Label_00BA;
        Label_00AE:
            attachment.CheckTarget = user;
        Label_00BA:
            if (attachment.IsFailCondition() == null)
            {
                goto Label_00CD;
            }
            attachment.IsCurse = is_curse;
        Label_00CD:
            return attachment;
        }

        private unsafe void CreateGimmickEvents()
        {
            char[] chArray1;
            BattleMap map;
            int num;
            JSON_GimmickEvent event2;
            GimmickEvent event3;
            bool flag;
            bool flag2;
            string[] strArray;
            int num2;
            int num3;
            Grid grid;
            map = this.CurrentMap;
            if (map.GimmickEvents != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            num = 0;
            goto Label_032D;
        Label_001A:
            event2 = map.GimmickEvents[num];
            if (string.IsNullOrEmpty(event2.skill) == null)
            {
                goto Label_0047;
            }
            if (event2.ev_type != null)
            {
                goto Label_0047;
            }
            goto Label_0329;
        Label_0047:
            if (event2.type != null)
            {
                goto Label_0057;
            }
            goto Label_0329;
        Label_0057:
            event3 = new GimmickEvent();
            event3.ev_type = event2.ev_type;
            flag = 0;
            if (event3.ev_type == 1)
            {
                goto Label_00D3;
            }
            this.GetConditionUnitByUnitID(event3.users, event2.su_iname);
            this.GetConditionUnitByUniqueName(event3.users, event2.su_tag, &flag);
            if (string.IsNullOrEmpty(event2.su_iname) == null)
            {
                goto Label_00BE;
            }
            if (string.IsNullOrEmpty(event2.su_tag) != null)
            {
                goto Label_00D3;
            }
        Label_00BE:
            if (event3.users.Count != null)
            {
                goto Label_00D3;
            }
            goto Label_0329;
        Label_00D3:
            this.GetConditionUnitByUnitID(event3.targets, event2.st_iname);
            flag2 = 0;
            this.GetConditionUnitByUniqueName(event3.targets, event2.st_tag, &flag2);
            event3.IsStarter = flag2;
            this.GetConditionTrickByTrickID(event3.td_targets, event2.st_iname);
            this.GetConditionTrickByTag(event3.td_targets, event2.st_tag);
            event3.td_iname = event2.st_iname;
            event3.td_tag = event2.st_tag;
            if (event3.ev_type != null)
            {
                goto Label_019D;
            }
            chArray1 = new char[] { 0x2c };
            strArray = event2.skill.Split(chArray1);
            if (strArray == null)
            {
                goto Label_019D;
            }
            if (((int) strArray.Length) <= 0)
            {
                goto Label_019D;
            }
            num2 = 0;
            goto Label_0192;
        Label_017C:
            event3.skills.Add(strArray[num2]);
            num2 += 1;
        Label_0192:
            if (num2 < ((int) strArray.Length))
            {
                goto Label_017C;
            }
        Label_019D:
            event3.condition.type = event2.type;
            event3.condition.count = event2.count;
            event3.condition.grids = new List<Grid>();
            num3 = 0;
            goto Label_0215;
        Label_01D7:
            grid = map[event2.x[num3], event2.y[num3]];
            if (grid != null)
            {
                goto Label_01FD;
            }
            goto Label_020F;
        Label_01FD:
            event3.condition.grids.Add(grid);
        Label_020F:
            num3 += 1;
        Label_0215:
            if (num3 >= ((int) event2.x.Length))
            {
                goto Label_0233;
            }
            if (num3 < ((int) event2.y.Length))
            {
                goto Label_01D7;
            }
        Label_0233:
            this.GetConditionUnitByUnitID(event3.condition.units, event2.cu_iname);
            this.GetConditionUnitByUniqueName(event3.condition.units, event2.cu_tag, &flag);
            if (string.IsNullOrEmpty(event2.cu_iname) == null)
            {
                goto Label_0283;
            }
            if (string.IsNullOrEmpty(event2.cu_tag) != null)
            {
                goto Label_029D;
            }
        Label_0283:
            if (event3.condition.units.Count != null)
            {
                goto Label_029D;
            }
            goto Label_0329;
        Label_029D:
            this.GetConditionUnitByUnitID(event3.condition.targets, event2.ct_iname);
            this.GetConditionUnitByUniqueName(event3.condition.targets, event2.ct_tag, &flag);
            this.GetConditionTrickByTrickID(event3.condition.td_targets, event2.ct_iname);
            this.GetConditionTrickByTag(event3.condition.td_targets, event2.ct_tag);
            event3.condition.td_iname = event2.ct_iname;
            event3.condition.td_tag = event2.ct_tag;
            this.mGimmickEvents.Add(event3);
        Label_0329:
            num += 1;
        Label_032D:
            if (num < map.GimmickEvents.Count)
            {
                goto Label_001A;
            }
            return;
        }

        private void CreateGridMapBishop(Grid target, int range_min, int range_max, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            map = this.CurrentMap;
            num = -range_max;
            goto Label_00A0;
        Label_0016:
            if (range_min <= 0)
            {
                goto Label_0035;
            }
            if (range_max <= 0)
            {
                goto Label_0035;
            }
            if (range_min < Math.Abs(num))
            {
                goto Label_0035;
            }
            goto Label_009C;
        Label_0035:
            num2 = -range_max;
            goto Label_0095;
        Label_003D:
            if (range_min <= 0)
            {
                goto Label_005C;
            }
            if (range_max <= 0)
            {
                goto Label_005C;
            }
            if (range_min < Math.Abs(num2))
            {
                goto Label_005C;
            }
            goto Label_0091;
        Label_005C:
            if (Math.Abs(num2) == Math.Abs(num))
            {
                goto Label_0072;
            }
            goto Label_0091;
        Label_0072:
            this.SetGridMap(result, target, map[target.x + num2, target.y + num]);
        Label_0091:
            num2 += 1;
        Label_0095:
            if (num2 <= range_max)
            {
                goto Label_003D;
            }
        Label_009C:
            num += 1;
        Label_00A0:
            if (num <= range_max)
            {
                goto Label_0016;
            }
            return;
        }

        public void CreateGridMapCross(Grid target, int range_min, int range_max, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            Grid grid;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            map = this.CurrentMap;
            num = range_min + 1;
            goto Label_0078;
        Label_0017:
            num2 = 0;
            goto Label_006D;
        Label_001E:
            num3 = Unit.DIRECTION_OFFSETS[num2, 0] * num;
            num4 = Unit.DIRECTION_OFFSETS[num2, 1] * num;
            num5 = target.x + num3;
            num6 = target.y + num4;
            grid = map[num5, num6];
            this.SetGridMap(result, target, grid);
            num2 += 1;
        Label_006D:
            if (num2 < 4)
            {
                goto Label_001E;
            }
            num += 1;
        Label_0078:
            if (num <= range_max)
            {
                goto Label_0017;
            }
            return;
        }

        private void CreateGridMapDiamond(Grid target, int range_min, int range_max, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            Grid grid;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            map = this.CurrentMap;
            num = -range_max;
            goto Label_0094;
        Label_0016:
            num2 = -range_max;
            goto Label_0089;
        Label_001E:
            num3 = Math.Abs(num2) + Math.Abs(num);
            if (num3 <= range_max)
            {
                goto Label_0038;
            }
            goto Label_0085;
        Label_0038:
            num4 = target.x + num2;
            num5 = target.y + num;
            grid = map[num4, num5];
            if (range_min <= 0)
            {
                goto Label_007A;
            }
            if (range_max <= 0)
            {
                goto Label_007A;
            }
            if (this.CalcGridDistance(target, grid) > range_min)
            {
                goto Label_007A;
            }
            goto Label_0085;
        Label_007A:
            this.SetGridMap(result, target, grid);
        Label_0085:
            num2 += 1;
        Label_0089:
            if (num2 <= range_max)
            {
                goto Label_001E;
            }
            num += 1;
        Label_0094:
            if (num <= range_max)
            {
                goto Label_0016;
            }
            return;
        }

        private void CreateGridMapHorse(Grid target, int range_min, int range_max, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            map = this.CurrentMap;
            range_max += 1;
            num = -range_max;
            goto Label_00BD;
        Label_001B:
            if (range_min <= 0)
            {
                goto Label_003A;
            }
            if (range_max <= 0)
            {
                goto Label_003A;
            }
            if (range_min < Math.Abs(num))
            {
                goto Label_003A;
            }
            goto Label_00B9;
        Label_003A:
            num2 = -range_max;
            goto Label_00B2;
        Label_0042:
            if (range_min <= 0)
            {
                goto Label_0061;
            }
            if (range_max <= 0)
            {
                goto Label_0061;
            }
            if (range_min < Math.Abs(num2))
            {
                goto Label_0061;
            }
            goto Label_00AE;
        Label_0061:
            if (Math.Abs(num2) == Math.Abs(num))
            {
                goto Label_008F;
            }
            if (Math.Abs(num2) > 1)
            {
                goto Label_00AE;
            }
            if (Math.Abs(num) <= 1)
            {
                goto Label_008F;
            }
            goto Label_00AE;
        Label_008F:
            this.SetGridMap(result, target, map[target.x + num2, target.y + num]);
        Label_00AE:
            num2 += 1;
        Label_00B2:
            if (num2 <= range_max)
            {
                goto Label_0042;
            }
        Label_00B9:
            num += 1;
        Label_00BD:
            if (num <= range_max)
            {
                goto Label_001B;
            }
            return;
        }

        private void CreateGridMapLaser(Grid self, Grid target, int range_min, int range_max, int scope, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            Grid grid;
            if (self == target)
            {
                goto Label_000D;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            map = this.CurrentMap;
            num = this.UnitDirectionFromGrid(self, target);
            num2 = Math.Max(scope - 1, 0);
            num3 = range_min + 1;
            goto Label_00BA;
        Label_0032:
            num4 = -num2;
            goto Label_00AE;
        Label_003B:
            num5 = Unit.DIRECTION_OFFSETS[num, 0] * num3;
            num6 = Unit.DIRECTION_OFFSETS[num, 1] * num3;
            num7 = (self.x + num5) + (Unit.DIRECTION_OFFSETS[num, 1] * num4);
            num8 = (self.y + num6) + (Unit.DIRECTION_OFFSETS[num, 0] * num4);
            grid = map[num7, num8];
            this.SetGridMap(result, target, grid);
            num4 += 1;
        Label_00AE:
            if (num4 <= num2)
            {
                goto Label_003B;
            }
            num3 += 1;
        Label_00BA:
            if (num3 <= range_max)
            {
                goto Label_0032;
            }
            if (*(result).get(target.x, target.y) != null)
            {
                goto Label_00E4;
            }
            *(result).fill(0);
        Label_00E4:
            return;
        }

        private void CreateGridMapLaserSpread(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            if (self == target)
            {
                goto Label_000D;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            map = this.CurrentMap;
            num = this.UnitDirectionFromGrid(self, target);
            num2 = range_min;
            goto Label_00AB;
        Label_0025:
            num3 = Unit.DIRECTION_OFFSETS[num, 0] * (num2 + 1);
            num4 = Unit.DIRECTION_OFFSETS[num, 1] * (num2 + 1);
            num5 = -num2;
            goto Label_009F;
        Label_0051:
            num6 = Unit.DIRECTION_OFFSETS[num, 1] * num5;
            num7 = Unit.DIRECTION_OFFSETS[num, 0] * num5;
            this.SetGridMap(result, target, map[(self.x + num3) + num6, (self.y + num4) + num7]);
            num5 += 1;
        Label_009F:
            if (num5 <= num2)
            {
                goto Label_0051;
            }
            num2 += 1;
        Label_00AB:
            if (num2 <= range_max)
            {
                goto Label_0025;
            }
            if (is_chk_clear == null)
            {
                goto Label_00DC;
            }
            if (*(result).get(target.x, target.y) != null)
            {
                goto Label_00DC;
            }
            *(result).fill(0);
        Label_00DC:
            return;
        }

        private void CreateGridMapLaserTriple(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            if (self == target)
            {
                goto Label_000D;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            map = this.CurrentMap;
            num = this.UnitDirectionFromGrid(self, target);
            num2 = range_min;
            goto Label_0163;
        Label_0025:
            num3 = Unit.DIRECTION_OFFSETS[num, 0] * (num2 + 2);
            num4 = Unit.DIRECTION_OFFSETS[num, 1] * (num2 + 2);
            if (num2 != null)
            {
                goto Label_00EE;
            }
            num5 = Unit.DIRECTION_OFFSETS[num, 0];
            num6 = Unit.DIRECTION_OFFSETS[num, 1];
            this.SetGridMap(result, target, map[self.x + num5, self.y + num6]);
            num7 = -1;
            goto Label_00E1;
        Label_0093:
            num8 = Unit.DIRECTION_OFFSETS[num, 1] * num7;
            num9 = Unit.DIRECTION_OFFSETS[num, 0] * num7;
            this.SetGridMap(result, target, map[(self.x + num3) + num8, (self.y + num4) + num9]);
            num7 += 1;
        Label_00E1:
            if (num7 <= 1)
            {
                goto Label_0093;
            }
            goto Label_015F;
        Label_00EE:
            num10 = -2;
            goto Label_0157;
        Label_00F7:
            if (Math.Abs(num10) != 1)
            {
                goto Label_0109;
            }
            goto Label_0151;
        Label_0109:
            num11 = Unit.DIRECTION_OFFSETS[num, 1] * num10;
            num12 = Unit.DIRECTION_OFFSETS[num, 0] * num10;
            this.SetGridMap(result, target, map[(self.x + num3) + num11, (self.y + num4) + num12]);
        Label_0151:
            num10 += 1;
        Label_0157:
            if (num10 <= 2)
            {
                goto Label_00F7;
            }
        Label_015F:
            num2 += 1;
        Label_0163:
            if (num2 <= range_max)
            {
                goto Label_0025;
            }
            if (is_chk_clear == null)
            {
                goto Label_0194;
            }
            if (*(result).get(target.x, target.y) != null)
            {
                goto Label_0194;
            }
            *(result).fill(0);
        Label_0194:
            return;
        }

        private void CreateGridMapLaserTwin(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            if (self == target)
            {
                goto Label_000D;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            map = this.CurrentMap;
            num = this.UnitDirectionFromGridLaserTwin(self, target);
            num2 = range_min;
            goto Label_00BD;
        Label_0025:
            if (num2 != null)
            {
                goto Label_0030;
            }
            goto Label_00B9;
        Label_0030:
            num3 = Unit.DIRECTION_OFFSETS[num, 0] * num2;
            num4 = Unit.DIRECTION_OFFSETS[num, 1] * num2;
            num5 = -1;
            goto Label_00B1;
        Label_0057:
            if (num5 != null)
            {
                goto Label_0063;
            }
            goto Label_00AB;
        Label_0063:
            num6 = Unit.DIRECTION_OFFSETS[num, 1] * num5;
            num7 = Unit.DIRECTION_OFFSETS[num, 0] * num5;
            this.SetGridMap(result, target, map[(self.x + num3) + num6, (self.y + num4) + num7]);
        Label_00AB:
            num5 += 1;
        Label_00B1:
            if (num5 <= 1)
            {
                goto Label_0057;
            }
        Label_00B9:
            num2 += 1;
        Label_00BD:
            if (num2 <= range_max)
            {
                goto Label_0025;
            }
            if (is_chk_clear == null)
            {
                goto Label_00EE;
            }
            if (*(result).get(target.x, target.y) != null)
            {
                goto Label_00EE;
            }
            *(result).fill(0);
        Label_00EE:
            return;
        }

        private void CreateGridMapLaserWide(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result, bool is_chk_clear)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            if (self == target)
            {
                goto Label_000D;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            map = this.CurrentMap;
            num = this.UnitDirectionFromGrid(self, target);
            num2 = range_min;
            goto Label_00D1;
        Label_0025:
            if (num2 != null)
            {
                goto Label_0030;
            }
            goto Label_00CD;
        Label_0030:
            num3 = Unit.DIRECTION_OFFSETS[num, 0] * (num2 + 1);
            num4 = Unit.DIRECTION_OFFSETS[num, 1] * (num2 + 1);
            this.SetGridMap(result, target, map[self.x + num3, self.y + num4]);
            num5 = 0;
            goto Label_00C5;
        Label_007B:
            num6 = Unit.DIRECTION_OFFSETS[num5, 0];
            num7 = Unit.DIRECTION_OFFSETS[num5, 1];
            this.SetGridMap(result, target, map[(self.x + num3) + num6, (self.y + num4) + num7]);
            num5 += 1;
        Label_00C5:
            if (num5 < 4)
            {
                goto Label_007B;
            }
        Label_00CD:
            num2 += 1;
        Label_00D1:
            if (num2 <= range_max)
            {
                goto Label_0025;
            }
            if (is_chk_clear == null)
            {
                goto Label_0102;
            }
            if (*(result).get(target.x, target.y) != null)
            {
                goto Label_0102;
            }
            *(result).fill(0);
        Label_0102:
            return;
        }

        private void CreateGridMapSquare(Grid target, int range_min, int range_max, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            Grid grid;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            map = this.CurrentMap;
            num = -range_max;
            goto Label_0094;
        Label_0016:
            if (range_min <= 0)
            {
                goto Label_0035;
            }
            if (range_max <= 0)
            {
                goto Label_0035;
            }
            if (range_min < Math.Abs(num))
            {
                goto Label_0035;
            }
            goto Label_0090;
        Label_0035:
            num2 = -range_max;
            goto Label_0089;
        Label_003D:
            if (range_min <= 0)
            {
                goto Label_005C;
            }
            if (range_max <= 0)
            {
                goto Label_005C;
            }
            if (range_min < Math.Abs(num2))
            {
                goto Label_005C;
            }
            goto Label_0085;
        Label_005C:
            num3 = target.x + num2;
            num4 = target.y + num;
            grid = map[num3, num4];
            this.SetGridMap(result, target, grid);
        Label_0085:
            num2 += 1;
        Label_0089:
            if (num2 <= range_max)
            {
                goto Label_003D;
            }
        Label_0090:
            num += 1;
        Label_0094:
            if (num <= range_max)
            {
                goto Label_0016;
            }
            return;
        }

        private void CreateGridMapVictory(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            if (self == target)
            {
                goto Label_000D;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            map = this.CurrentMap;
            num = this.UnitDirectionFromGrid(self, target);
            num2 = -range_max;
            goto Label_00A9;
        Label_0027:
            if (Math.Abs(num2) >= range_min)
            {
                goto Label_0038;
            }
            goto Label_00A5;
        Label_0038:
            num3 = Unit.DIRECTION_OFFSETS[num, 1] * num2;
            num4 = Unit.DIRECTION_OFFSETS[num, 0] * num2;
            num3 += Unit.DIRECTION_OFFSETS[num, 0] * Math.Abs(num2);
            num4 += Unit.DIRECTION_OFFSETS[num, 1] * Math.Abs(num2);
            this.SetGridMap(result, target, map[target.x + num3, target.y + num4]);
        Label_00A5:
            num2 += 1;
        Label_00A9:
            if (num2 <= range_max)
            {
                goto Label_0027;
            }
            return;
        }

        private void CreateGridMapWall(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            if (self == target)
            {
                goto Label_000D;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            map = this.CurrentMap;
            num = this.UnitDirectionFromGrid(self, target);
            num2 = -range_max;
            goto Label_007B;
        Label_0027:
            if (Math.Abs(num2) >= range_min)
            {
                goto Label_0038;
            }
            goto Label_0077;
        Label_0038:
            num3 = Unit.DIRECTION_OFFSETS[num, 1] * num2;
            num4 = Unit.DIRECTION_OFFSETS[num, 0] * num2;
            this.SetGridMap(result, target, map[target.x + num3, target.y + num4]);
        Label_0077:
            num2 += 1;
        Label_007B:
            if (num2 <= range_max)
            {
                goto Label_0027;
            }
            return;
        }

        private void CreateGridMapWallPlus(Grid self, Grid target, int range_min, int range_max, ref GridMap<bool> result)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            if (self == target)
            {
                goto Label_000D;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            map = this.CurrentMap;
            num = this.UnitDirectionFromGrid(self, target);
            num2 = -range_max;
            goto Label_00BB;
        Label_0027:
            if (Math.Abs(num2) >= range_min)
            {
                goto Label_0038;
            }
            goto Label_00B7;
        Label_0038:
            num3 = Unit.DIRECTION_OFFSETS[num, 1] * num2;
            num4 = Unit.DIRECTION_OFFSETS[num, 0] * num2;
            this.SetGridMap(result, target, map[target.x + num3, target.y + num4]);
            num3 += Unit.DIRECTION_OFFSETS[num, 0];
            num4 += Unit.DIRECTION_OFFSETS[num, 1];
            this.SetGridMap(result, target, map[target.x + num3, target.y + num4]);
        Label_00B7:
            num2 += 1;
        Label_00BB:
            if (num2 <= range_max)
            {
                goto Label_0027;
            }
            return;
        }

        public GridMap<int> CreateMoveMap(Unit self, int movcount)
        {
            BattleMap map;
            GridMap<int> map2;
            map = this.CurrentMap;
            map2 = new GridMap<int>(map.Width, map.Height);
            if (0 >= movcount)
            {
                goto Label_002E;
            }
            this.UpdateMoveMap(self, map2, movcount);
            goto Label_0036;
        Label_002E:
            this.UpdateMoveMap(self, map2);
        Label_0036:
            return map2;
        }

        public unsafe GridMap<bool> CreateScopeGridMap(int gx, int gy, ESelectType shape, int scope, int height)
        {
            GridMap<bool> map;
            if (this.CurrentMap != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            map = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
            this.CreateScopeGridMap(null, 0, 0, gx, gy, 0, 0, scope, height, shape, &map, 0, 0);
            return map;
        }

        public unsafe GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, SkillData skill)
        {
            BattleMap map;
            GridMap<bool> map2;
            map = this.CurrentMap;
            map2 = new GridMap<bool>(map.Width, map.Height);
            this.CreateScopeGridMap(self, selfX, selfY, targetX, targetY, skill, &map2, 0);
            return map2;
        }

        public GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, SkillData skill, ref GridMap<bool> result, bool keeped)
        {
            SkillParam param;
            int num;
            int num2;
            int num3;
            int num4;
            ESelectType type;
            param = skill.SkillParam;
            num = self.GetAttackRangeMin(skill);
            num2 = self.GetAttackRangeMax(skill);
            num3 = self.GetAttackScope(skill);
            num4 = self.GetAttackHeight(skill, 0);
            type = param.select_scope;
            this.CreateScopeGridMap(self, selfX, selfY, targetX, targetY, num, num2, num3, num4, type, result, keeped, skill.TeleportType);
            return *(result);
        }

        public unsafe GridMap<bool> CreateScopeGridMap(Unit self, int selfX, int selfY, int targetX, int targetY, int range_min, int range_max, int scope, int enable_height, ESelectType scopetype, ref GridMap<bool> result, bool keeped, eTeleportType teleport_type)
        {
            GridMap<bool> map;
            bool flag;
            BattleMap map2;
            Grid grid;
            Grid grid2;
            Grid grid3;
            int num;
            int num2;
            Grid grid4;
            int num3;
            int num4;
            ESelectType type;
            map = null;
            if (keeped != null)
            {
                goto Label_0012;
            }
            map = *(result);
            goto Label_0028;
        Label_0012:
            map = new GridMap<bool>(*(result).w, *(result).h);
        Label_0028:
            map.fill(0);
            flag = 0;
            if (scope >= 1)
            {
                goto Label_0071;
            }
            if (SkillParam.IsTypeLaser(scopetype) == null)
            {
                goto Label_0049;
            }
            return *(result);
        Label_0049:
            type = scopetype;
            if (type == 4)
            {
                goto Label_0071;
            }
            goto Label_005F;
            goto Label_0071;
        Label_005F:
            map.set(targetX, targetY, 1);
            flag = 1;
        Label_0071:
            if (teleport_type != 3)
            {
                goto Label_008A;
            }
            map.set(targetX, targetY, 1);
            targetX = selfX;
            targetY = selfY;
        Label_008A:
            map2 = this.CurrentMap;
            grid = map2[targetX, targetY];
            grid2 = grid;
            if (flag != null)
            {
                goto Label_0287;
            }
            grid3 = map2[selfX, selfY];
            type = scopetype;
            switch (type)
            {
                case 0:
                    goto Label_026C;

                case 1:
                    goto Label_00FB;

                case 2:
                    goto Label_0116;

                case 3:
                    goto Label_0131;

                case 4:
                    goto Label_014B;

                case 5:
                    goto Label_0157;

                case 6:
                    goto Label_0174;

                case 7:
                    goto Label_0191;

                case 8:
                    goto Label_01AC;

                case 9:
                    goto Label_01C9;

                case 10:
                    goto Label_01E2;

                case 11:
                    goto Label_01FB;

                case 12:
                    goto Label_0216;

                case 13:
                    goto Label_022F;

                case 14:
                    goto Label_0248;
            }
            goto Label_026C;
        Label_00FB:
            this.SetGridMap(&map, grid, grid);
            this.CreateGridMapDiamond(grid, 0, scope, &map);
            goto Label_0287;
        Label_0116:
            this.SetGridMap(&map, grid, grid);
            this.CreateGridMapSquare(grid, 0, scope, &map);
            goto Label_0287;
        Label_0131:
            this.CreateGridMapLaser(grid3, grid, range_min, range_max, scope, &map);
            grid2 = grid3;
            goto Label_0287;
        Label_014B:
            map.fill(1);
            goto Label_0287;
        Label_0157:
            this.SetGridMap(&map, grid, grid);
            this.CreateGridMapWall(grid3, grid, 0, scope, &map);
            goto Label_0287;
        Label_0174:
            this.SetGridMap(&map, grid, grid);
            this.CreateGridMapWallPlus(grid3, grid, 0, scope, &map);
            goto Label_0287;
        Label_0191:
            this.SetGridMap(&map, grid, grid);
            this.CreateGridMapBishop(grid, 0, scope, &map);
            goto Label_0287;
        Label_01AC:
            this.SetGridMap(&map, grid, grid);
            this.CreateGridMapVictory(grid3, grid, 0, scope, &map);
            goto Label_0287;
        Label_01C9:
            this.CreateGridMapLaserSpread(grid3, grid, range_min, range_max, &map, 1);
            grid2 = grid3;
            goto Label_0287;
        Label_01E2:
            this.CreateGridMapLaserWide(grid3, grid, range_min, range_max, &map, 1);
            grid2 = grid3;
            goto Label_0287;
        Label_01FB:
            this.SetGridMap(&map, grid, grid);
            this.CreateGridMapHorse(grid, 0, scope, &map);
            goto Label_0287;
        Label_0216:
            this.CreateGridMapLaserTwin(grid3, grid, range_min, range_max, &map, 1);
            grid2 = grid3;
            goto Label_0287;
        Label_022F:
            this.CreateGridMapLaserTriple(grid3, grid, range_min, range_max, &map, 1);
            grid2 = grid3;
            goto Label_0287;
        Label_0248:
            this.CreateGridMapSquare(grid, 0, scope, &map);
            map.set(grid.x, grid.y, 0);
            goto Label_0287;
        Label_026C:
            this.SetGridMap(&map, grid, grid);
            this.CreateGridMapCross(grid, 0, scope, &map);
        Label_0287:
            num = map.h - 1;
            goto Label_0320;
        Label_0296:
            num2 = 0;
            goto Label_030D;
        Label_029E:
            if (map.get(num2, num) != null)
            {
                goto Label_02B2;
            }
            goto Label_0307;
        Label_02B2:
            grid4 = map2[num2, num];
            if (this.CheckEnableAttackHeight(grid2, grid4, enable_height) != null)
            {
                goto Label_02DA;
            }
            map.set(num2, num, 0);
        Label_02DA:
            if (grid4.geo == null)
            {
                goto Label_0307;
            }
            if (grid4.geo.DisableStopped == null)
            {
                goto Label_0307;
            }
            map.set(num2, num, 0);
        Label_0307:
            num2 += 1;
        Label_030D:
            if (num2 < map.w)
            {
                goto Label_029E;
            }
            num -= 1;
        Label_0320:
            if (num >= 0)
            {
                goto Label_0296;
            }
            if (keeped == null)
            {
                goto Label_0381;
            }
            num3 = 0;
            goto Label_0374;
        Label_0337:
            num4 = 0;
            goto Label_0361;
        Label_033F:
            if (map.get(num3, num4) == null)
            {
                goto Label_035B;
            }
            *(result).set(num3, num4, 1);
        Label_035B:
            num4 += 1;
        Label_0361:
            if (num4 < map.h)
            {
                goto Label_033F;
            }
            num3 += 1;
        Label_0374:
            if (num3 < map.w)
            {
                goto Label_0337;
            }
        Label_0381:
            return *(result);
        }

        public unsafe GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, SkillData skill)
        {
            BattleMap map;
            GridMap<bool> map2;
            map = this.CurrentMap;
            map2 = new GridMap<bool>(map.Width, map.Height);
            this.CreateSelectGridMap(self, targetX, targetY, skill, &map2, 0);
            return map2;
        }

        public GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, SkillData skill, ref GridMap<bool> result, bool keeped)
        {
            SkillParam param;
            int num;
            int num2;
            ELineType type;
            ESelectType type2;
            int num3;
            int num4;
            bool flag;
            bool flag2;
            bool flag3;
            param = skill.SkillParam;
            num = self.GetAttackRangeMin(skill);
            num2 = self.GetAttackRangeMax(skill);
            type = param.line_type;
            type2 = param.select_range;
            num3 = self.GetAttackScope(skill);
            num4 = self.GetAttackHeight(skill, 1);
            flag = skill.CheckGridLineSkill();
            flag2 = skill.IsEnableHeightRangeBonus();
            flag3 = skill.IsSelfTargetSelect();
            return this.CreateSelectGridMap(self, targetX, targetY, num, num2, type2, type, num3, flag, flag2, num4, flag3, result, keeped);
        }

        private unsafe GridMap<bool> CreateSelectGridMap(Unit self, int targetX, int targetY, int range_min, int range_max, ESelectType rangetype, ELineType linetype, int scope, bool bCheckGridLine, bool bHeightBonus, int attack_height, bool bSelfEffect, ref GridMap<bool> result, bool keeped)
        {
            BattleMap map;
            GridMap<bool> map2;
            Grid grid;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            Grid grid2;
            int num8;
            Grid grid3;
            int num9;
            Grid grid4;
            int num10;
            Grid grid5;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            int num16;
            int num17;
            Grid grid6;
            int num18;
            int num19;
            int num20;
            ESelectType type;
            map = this.CurrentMap;
            map2 = null;
            if (keeped != null)
            {
                goto Label_0019;
            }
            map2 = *(result);
            goto Label_002B;
        Label_0019:
            map2 = new GridMap<bool>(map.Width, map.Height);
        Label_002B:
            map2.fill(0);
            grid = map[targetX, targetY];
            if (range_max > 0)
            {
                goto Label_004B;
            }
            if (rangetype != 4)
            {
                goto Label_050A;
            }
        Label_004B:
            num = range_max;
            if (bHeightBonus == null)
            {
                goto Label_0065;
            }
            num += this.GetAttackRangeBonus(grid.height, 0);
        Label_0065:
            type = rangetype;
            switch (type)
            {
                case 0:
                    goto Label_02D1;

                case 1:
                    goto Label_00AD;

                case 2:
                    goto Label_00BE;

                case 3:
                    goto Label_00DB;

                case 4:
                    goto Label_00CF;

                case 5:
                    goto Label_02D1;

                case 6:
                    goto Label_02D1;

                case 7:
                    goto Label_0143;

                case 8:
                    goto Label_02D1;

                case 9:
                    goto Label_0154;

                case 10:
                    goto Label_01AF;

                case 11:
                    goto Label_020A;

                case 12:
                    goto Label_021B;

                case 13:
                    goto Label_0276;
            }
            goto Label_02D1;
        Label_00AD:
            this.CreateGridMapDiamond(grid, range_min, num, &map2);
            goto Label_02E2;
        Label_00BE:
            this.CreateGridMapSquare(grid, range_min, num, &map2);
            goto Label_02E2;
        Label_00CF:
            map2.fill(1);
            goto Label_02E2;
        Label_00DB:
            num2 = 0;
            goto Label_0136;
        Label_00E3:
            num3 = Unit.DIRECTION_OFFSETS[num2, 0];
            num4 = Unit.DIRECTION_OFFSETS[num2, 1];
            num5 = grid.x + num3;
            num6 = grid.y + num4;
            this.CreateGridMapLaser(grid, map[num5, num6], range_min, range_max, scope, &map2);
            num2 += 1;
        Label_0136:
            if (num2 < 4)
            {
                goto Label_00E3;
            }
            goto Label_02E2;
        Label_0143:
            this.CreateGridMapBishop(grid, range_min, num, &map2);
            goto Label_02E2;
        Label_0154:
            num7 = 0;
            goto Label_01A2;
        Label_015C:
            grid2 = map[grid.x + Unit.DIRECTION_OFFSETS[num7, 0], grid.y + Unit.DIRECTION_OFFSETS[num7, 1]];
            this.CreateGridMapLaserSpread(grid, grid2, range_min, range_max, &map2, 0);
            num7 += 1;
        Label_01A2:
            if (num7 < 4)
            {
                goto Label_015C;
            }
            goto Label_02E2;
        Label_01AF:
            num8 = 0;
            goto Label_01FD;
        Label_01B7:
            grid3 = map[grid.x + Unit.DIRECTION_OFFSETS[num8, 0], grid.y + Unit.DIRECTION_OFFSETS[num8, 1]];
            this.CreateGridMapLaserWide(grid, grid3, range_min, range_max, &map2, 0);
            num8 += 1;
        Label_01FD:
            if (num8 < 4)
            {
                goto Label_01B7;
            }
            goto Label_02E2;
        Label_020A:
            this.CreateGridMapHorse(grid, range_min, num, &map2);
            goto Label_02E2;
        Label_021B:
            num9 = 0;
            goto Label_0269;
        Label_0223:
            grid4 = map[grid.x + Unit.DIRECTION_OFFSETS[num9, 0], grid.y + Unit.DIRECTION_OFFSETS[num9, 1]];
            this.CreateGridMapLaserTwin(grid, grid4, range_min, range_max, &map2, 0);
            num9 += 1;
        Label_0269:
            if (num9 < 4)
            {
                goto Label_0223;
            }
            goto Label_02E2;
        Label_0276:
            num10 = 0;
            goto Label_02C4;
        Label_027E:
            grid5 = map[grid.x + Unit.DIRECTION_OFFSETS[num10, 0], grid.y + Unit.DIRECTION_OFFSETS[num10, 1]];
            this.CreateGridMapLaserTriple(grid, grid5, range_min, range_max, &map2, 0);
            num10 += 1;
        Label_02C4:
            if (num10 < 4)
            {
                goto Label_027E;
            }
            goto Label_02E2;
        Label_02D1:
            this.CreateGridMapCross(grid, range_min, num, &map2);
        Label_02E2:
            if (SkillParam.IsTypeLaser(rangetype) == null)
            {
                goto Label_0360;
            }
            num11 = 0;
            goto Label_034E;
        Label_02F6:
            num12 = 0;
            goto Label_033B;
        Label_02FE:
            if (map2.get(num11, num12) != null)
            {
                goto Label_0312;
            }
            goto Label_0335;
        Label_0312:
            if (this.CheckEnableAttackHeight(grid, map[num11, num12], attack_height) != null)
            {
                goto Label_0335;
            }
            map2.set(num11, num12, 0);
        Label_0335:
            num12 += 1;
        Label_033B:
            if (num12 < map2.h)
            {
                goto Label_02FE;
            }
            num11 += 1;
        Label_034E:
            if (num11 < map2.w)
            {
                goto Label_02F6;
            }
            goto Label_050A;
        Label_0360:
            num13 = -num;
            goto Label_0502;
        Label_0369:
            num14 = -num;
            goto Label_04F4;
        Label_0372:
            num15 = 0;
            if (rangetype != 2)
            {
                goto Label_0397;
            }
            num15 = Math.Max(Math.Abs(num14), Math.Abs(num13));
            goto Label_03A8;
        Label_0397:
            num15 = Math.Abs(num14) + Math.Abs(num13);
        Label_03A8:
            if (num15 <= num)
            {
                goto Label_03B5;
            }
            goto Label_04EE;
        Label_03B5:
            num16 = num14 + grid.x;
            num17 = num13 + grid.y;
            grid6 = map[num16, num17];
            if (grid6 == null)
            {
                goto Label_04EE;
            }
            if (grid6 != grid)
            {
                goto Label_03EB;
            }
            goto Label_04EE;
        Label_03EB:
            if (map2.get(num16, num17) != null)
            {
                goto Label_03FF;
            }
            goto Label_04EE;
        Label_03FF:
            num18 = 0;
            if (bHeightBonus == null)
            {
                goto Label_041E;
            }
            num18 = this.GetAttackRangeBonus(grid.height, grid6.height);
        Label_041E:
            if (num15 <= (range_max + num18))
            {
                goto Label_043A;
            }
            map2.set(num16, num17, 0);
            goto Label_04EE;
        Label_043A:
            if (grid6.geo == null)
            {
                goto Label_0476;
            }
            if (grid6.geo.DisableStopped == null)
            {
                goto Label_0476;
            }
            map2.set(grid6.x, grid6.y, 0);
            goto Label_04EE;
        Label_0476:
            if (linetype != null)
            {
                goto Label_04A7;
            }
            if (this.CheckEnableAttackHeight(grid, grid6, attack_height) != null)
            {
                goto Label_04EE;
            }
            map2.set(grid6.x, grid6.y, 0);
            goto Label_04EE;
        Label_04A7:
            this.GetSkillGridLines(grid.x, grid.y, num16, num17, range_min, range_max, attack_height, linetype, bHeightBonus, &this.mGridLines);
            map2.set(grid6.x, grid6.y, this.mGridLines.Contains(grid6));
        Label_04EE:
            num14 += 1;
        Label_04F4:
            if (num14 <= num)
            {
                goto Label_0372;
            }
            num13 += 1;
        Label_0502:
            if (num13 <= num)
            {
                goto Label_0369;
            }
        Label_050A:
            map2.set(grid.x, grid.y, bSelfEffect);
            num19 = 0;
            goto Label_0579;
        Label_0526:
            num20 = 0;
            goto Label_0566;
        Label_052E:
            if (*(result).get(num19, num20) != null)
            {
                goto Label_0560;
            }
            if (map2.get(num19, num20) != null)
            {
                goto Label_0553;
            }
            goto Label_0560;
        Label_0553:
            *(result).set(num19, num20, 1);
        Label_0560:
            num20 += 1;
        Label_0566:
            if (num20 < map2.h)
            {
                goto Label_052E;
            }
            num19 += 1;
        Label_0579:
            if (num19 < map2.w)
            {
                goto Label_0526;
            }
            return *(result);
        }

        private GridMap<bool> CreateSelectGridMapAI(Unit self, int targetX, int targetY, SkillData skill)
        {
            GridMap<bool> map;
            map = this.CreateSelectGridMap(self, self.x, self.y, skill);
            if (skill.TeleportType == null)
            {
                goto Label_002D;
            }
            map = this.RemoveCantMove(self, map, skill);
        Label_002D:
            return map;
        }

        private unsafe GridMap<bool> CreateSkillRangeMapAll(Unit self, SkillData skill, bool is_move)
        {
            GridMap<bool> map;
            GridMap<int> map2;
            int num;
            int num2;
            map = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
            map.fill(0);
            map2 = null;
            if (is_move == null)
            {
                goto Label_00A0;
            }
            if (self.IsUnitFlag(2) != null)
            {
                goto Label_00A0;
            }
            if (self.IsEnableMoveCondition(0) == null)
            {
                goto Label_00A0;
            }
            map2 = this.CreateMoveMap(self, 0);
            num = 0;
            goto Label_008F;
        Label_0053:
            num2 = 0;
            goto Label_007F;
        Label_005A:
            if (map2.get(num, num2) >= 0)
            {
                goto Label_006D;
            }
            goto Label_007B;
        Label_006D:
            this.CreateSelectGridMap(self, num, num2, skill, &map, 1);
        Label_007B:
            num2 += 1;
        Label_007F:
            if (num2 < map2.h)
            {
                goto Label_005A;
            }
            num += 1;
        Label_008F:
            if (num < map2.w)
            {
                goto Label_0053;
            }
            goto Label_00B8;
        Label_00A0:
            this.CreateSelectGridMap(self, self.x, self.y, skill, &map, 1);
        Label_00B8:
            return map;
        }

        private unsafe GridMap<bool> CreateSkillScopeMapAll(Unit self, SkillData skill, bool is_move)
        {
            GridMap<bool> map;
            GridMap<bool> map2;
            int num;
            int num2;
            map = this.CreateSkillRangeMapAll(self, skill, is_move);
            if (SkillParam.IsTypeLaser(skill.SkillParam.select_scope) == null)
            {
                goto Label_0021;
            }
            return map;
        Label_0021:
            map2 = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
            map2.fill(0);
            num = 0;
            goto Label_0091;
        Label_004B:
            num2 = 0;
            goto Label_0081;
        Label_0052:
            if (map.get(num, num2) != null)
            {
                goto Label_0064;
            }
            goto Label_007D;
        Label_0064:
            map2.set(num, num2, 1);
            this.CreateScopeGridMap(self, num, num2, num, num2, skill, &map2, 1);
        Label_007D:
            num2 += 1;
        Label_0081:
            if (num2 < map.h)
            {
                goto Label_0052;
            }
            num += 1;
        Label_0091:
            if (num < map.w)
            {
                goto Label_004B;
            }
            return map2;
        }

        private void CureCondition(Unit target, EUnitCondition condition, LogSkill logskl)
        {
            LogCureCondition condition2;
            bool flag;
            LogSkill.Target target2;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            condition2 = this.Log<LogCureCondition>();
            condition2.self = target;
            condition2.condition = condition;
            flag = target.IsUnitCondition(condition);
            target.CureCondEffects(condition, 1, 0);
            if (logskl == null)
            {
                goto Label_006D;
            }
            if (target == null)
            {
                goto Label_006D;
            }
            if (flag == null)
            {
                goto Label_006D;
            }
            if (target.IsUnitCondition(condition) != null)
            {
                goto Label_006D;
            }
            target2 = logskl.FindTarget(target);
            if (target2 == null)
            {
                goto Label_006D;
            }
            target2.cureCondition |= condition;
        Label_006D:
            return;
        }

        private unsafe void DamageControlSkill(Unit attacker, Unit defender, SkillData atkskl, ref int damage, LogSkill log)
        {
            int num;
            SkillData data;
            int num2;
            int num3;
            bool flag;
            LogSkill.Target target;
            DamageTypes types;
            if (atkskl.IsDamagedSkill() != null)
            {
                goto Label_0017;
            }
            if (defender.IsEnableReactionCondition() != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            num = 0;
            goto Label_0232;
        Label_001E:
            data = defender.BattleSkills[num];
            if (data == null)
            {
                goto Label_022E;
            }
            if (data.IsReactionSkill() != null)
            {
                goto Label_0041;
            }
            goto Label_022E;
        Label_0041:
            if (data.Timing == 6)
            {
                goto Label_005E;
            }
            if (data.Timing == 5)
            {
                goto Label_005E;
            }
            goto Label_022E;
        Label_005E:
            if (defender.IsEnableReactionSkill(data) != null)
            {
                goto Label_006F;
            }
            goto Label_022E;
        Label_006F:
            if (this.CheckSkillCondition(defender, data) != null)
            {
                goto Label_0081;
            }
            goto Label_022E;
        Label_0081:
            num2 = data.EffectRate;
            if (num2 <= 0)
            {
                goto Label_00B2;
            }
            if (num2 >= 100)
            {
                goto Label_00B2;
            }
            num3 = this.GetRandom() % 100;
            if (num3 <= num2)
            {
                goto Label_00B2;
            }
            goto Label_022E;
        Label_00B2:
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_00C3;
            }
            goto Label_022E;
        Label_00C3:
            if (data.ControlDamageValue != null)
            {
                goto Label_00D8;
            }
            goto Label_022E;
        Label_00D8:
            flag = 0;
            switch ((data.ReactionDamageType - 1))
            {
                case 0:
                    goto Label_017D;

                case 1:
                    goto Label_00FD;

                case 2:
                    goto Label_013D;
            }
            goto Label_01B2;
        Label_00FD:
            if (atkskl.IsPhysicalAttack() == null)
            {
                goto Label_01B2;
            }
            if (data.IsReactionDet(atkskl.AttackDetailType) == null)
            {
                goto Label_01B2;
            }
            *((int*) damage) = SkillParam.CalcSkillEffectValue(data.ControlDamageCalcType, data.ControlDamageValue, *((int*) damage));
            flag = 1;
            goto Label_01B2;
        Label_013D:
            if (atkskl.IsMagicalAttack() == null)
            {
                goto Label_01B2;
            }
            if (data.IsReactionDet(atkskl.AttackDetailType) == null)
            {
                goto Label_01B2;
            }
            *((int*) damage) = SkillParam.CalcSkillEffectValue(data.ControlDamageCalcType, data.ControlDamageValue, *((int*) damage));
            flag = 1;
            goto Label_01B2;
        Label_017D:
            if (data.IsReactionDet(atkskl.AttackDetailType) == null)
            {
                goto Label_01B2;
            }
            *((int*) damage) = SkillParam.CalcSkillEffectValue(data.ControlDamageCalcType, data.ControlDamageValue, *((int*) damage));
            flag = 1;
        Label_01B2:
            if (log == null)
            {
                goto Label_022E;
            }
            if (flag == null)
            {
                goto Label_022E;
            }
            target = log.FindTarget(defender);
            if (target == null)
            {
                goto Label_022E;
            }
            target.SetDefend(1);
            target.defSkill = data;
            target.defSkillUseCount = 0;
            if (data.SkillParam.count <= 0)
            {
                goto Label_021A;
            }
            defender.UpdateSkillUseCount(data, -1);
            target.defSkillUseCount = defender.GetSkillUseCount(data);
        Label_021A:
            if (data.Timing != 6)
            {
                goto Label_022E;
            }
            target.SetForceReaction(1);
        Label_022E:
            num += 1;
        Label_0232:
            if (num < defender.BattleSkills.Count)
            {
                goto Label_001E;
            }
            return;
        }

        private void DamageCureCondition(Unit target, LogSkill log)
        {
            if (target.IsUnitCondition(8L) == null)
            {
                goto Label_0017;
            }
            this.CureCondition(target, 8L, log);
        Label_0017:
            if (target.IsUnitCondition(0x10L) == null)
            {
                goto Label_0030;
            }
            this.CureCondition(target, 0x10L, log);
        Label_0030:
            return;
        }

        private unsafe void DamageSkill(Unit self, Unit target, SkillData skill, LogSkill log)
        {
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            bool flag5;
            int num;
            int num2;
            uint num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            LogSkill.Target target2;
            int num10;
            LogSkill.Target target3;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            bool flag6;
            SkillEffectTypes types;
            JewelDamageTypes types2;
            flag = 0;
            flag2 = 0;
            flag3 = 0;
            flag4 = 0;
            flag5 = 0;
            num = 0;
            num2 = 0;
            types = skill.EffectType;
            if (types == 2)
            {
                goto Label_0041;
            }
            if (types == 9)
            {
                goto Label_0110;
            }
            if (types == 20)
            {
                goto Label_0149;
            }
            if (types == 0x1c)
            {
                goto Label_01B1;
            }
            goto Label_0219;
        Label_0041:
            if (target.IsBreakObj != null)
            {
                goto Label_007F;
            }
            if (this.CheckBackAttack(self, target, skill) == null)
            {
                goto Label_0068;
            }
            self.SetUnitFlag(0x40, 1);
            goto Label_007F;
        Label_0068:
            if (this.CheckSideAttack(self, target, skill) == null)
            {
                goto Label_007F;
            }
            self.SetUnitFlag(0x20, 1);
        Label_007F:
            num = this.GetDamageSkill(self, target, skill, log);
            if (skill.IsJewelAttack() != null)
            {
                goto Label_021A;
            }
            if (this.IsCombinationAttack(skill) != null)
            {
                goto Label_00F9;
            }
            if (skill.IsNormalAttack() != null)
            {
                goto Label_00C3;
            }
            if (skill.SkillParam.IsCritical == null)
            {
                goto Label_00F9;
            }
        Label_00C3:
            flag3 = this.CheckCritical(self, target, skill);
            if (flag3 == null)
            {
                goto Label_00F9;
            }
            num3 = this.mRandDamage.Get();
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_00F9;
            }
            num = this.GetCriticalDamage(self, num, num3);
        Label_00F9:
            this.GetYuragiDamage(&num);
            flag4 = this.CheckWeakPoint(self, target, skill);
            goto Label_021A;
        Label_0110:
            if (log.reflect == null)
            {
                goto Label_021A;
            }
            num4 = this.GetSkillEffectValue(self, target, skill, log);
            num = SkillParam.CalcSkillEffectValue(skill.EffectCalcType, num4, log.reflect.damage);
            goto Label_021A;
        Label_0149:
            num5 = this.GetSkillEffectValue(self, target, skill, log);
            if (skill.IsJewelAttack() == null)
            {
                goto Label_0189;
            }
            num = ((target.MaximumStatus.param.mp * num5) * 100) / 0x2710;
            goto Label_01AC;
        Label_0189:
            num = ((target.MaximumStatus.param.hp * num5) * 100) / 0x2710;
        Label_01AC:
            goto Label_021A;
        Label_01B1:
            num6 = this.GetSkillEffectValue(self, target, skill, log);
            if (skill.IsJewelAttack() == null)
            {
                goto Label_01F1;
            }
            num = ((target.CurrentStatus.param.mp * num6) * 100) / 0x2710;
            goto Label_0214;
        Label_01F1:
            num = ((target.CurrentStatus.param.hp * num6) * 100) / 0x2710;
        Label_0214:
            goto Label_021A;
        Label_0219:
            return;
        Label_021A:
            num = (num * skill.SkillParam.ComboDamageRate) / 100;
            this.DamageControlSkill(self, target, skill, &num, log);
            num7 = this.mQuestParam.DamageRatePl;
            num8 = this.mQuestParam.DamageUpprPl;
            if (target.Side != 1)
            {
                goto Label_027F;
            }
            num7 = this.mQuestParam.DamageRateEn;
            num8 = this.mQuestParam.DamageUpprEn;
        Label_027F:
            num += (num * num7) / 100;
            if (num8 == null)
            {
                goto Label_029E;
            }
            num = Math.Min(num, num8);
        Label_029E:
            if (skill.IsFixedDamage() == null)
            {
                goto Label_031A;
            }
            num = skill.EffectValue;
            if (log == null)
            {
                goto Label_031A;
            }
            if (log.targets == null)
            {
                goto Label_031A;
            }
            if (log.targets.Count <= 1)
            {
                goto Label_031A;
            }
            num9 = skill.SkillParam.EffectHitTargetNumRate;
            if (num9 == null)
            {
                goto Label_031A;
            }
            num9 *= log.targets.Count - 1;
            num += ((100 * num) * num9) / 0x2710;
        Label_031A:
            if (skill.SkillParam.IsHitTargetNumDiv() == null)
            {
                goto Label_0360;
            }
            if (log == null)
            {
                goto Label_0360;
            }
            if (log.targets == null)
            {
                goto Label_0360;
            }
            if (log.targets.Count <= 1)
            {
                goto Label_0360;
            }
            num /= log.targets.Count;
        Label_0360:
            if (skill.MaxDamageValue == null)
            {
                goto Label_0380;
            }
            if (num <= skill.MaxDamageValue)
            {
                goto Label_0380;
            }
            num = skill.MaxDamageValue;
        Label_0380:
            num = Math.Max(num, 1);
            if (this.CheckPerfectAvoidSkill(self, target, skill, log) == null)
            {
                goto Label_03C4;
            }
            if (log == null)
            {
                goto Label_03BA;
            }
            target2 = log.FindTarget(target);
            if (target2 == null)
            {
                goto Label_03BA;
            }
            target2.SetPerfectAvoid(1);
        Label_03BA:
            flag2 = 1;
            flag5 = 1;
            goto Label_03CE;
        Label_03C4:
            flag2 = this.CheckAvoid(self, target, skill);
        Label_03CE:
            num = this.GetResistDamageForMhmDamage(self, target, skill, num);
            if (flag2 == null)
            {
                goto Label_03ED;
            }
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_04D6;
            }
        Label_03ED:
            if (num <= 0)
            {
                goto Label_04D6;
            }
            if (skill.DamageAbsorbRate > 0)
            {
                goto Label_04D6;
            }
            if (skill.IsJewelAttack() != null)
            {
                goto Label_04D6;
            }
            num10 = num;
            target3 = null;
            if (log == null)
            {
                goto Label_0424;
            }
            target3 = log.FindTarget(target);
        Label_0424:
            if (skill.IsPhysicalAttack() == null)
            {
                goto Label_0469;
            }
            target.CalcShieldDamage(2, &num, this.IsBattleFlag(5) == 0, skill.AttackDetailType, this.CurrentRand, target3);
            num = Math.Max((num * this.mQuestParam.PhysBonus) / 100, 0);
        Label_0469:
            if (skill.IsMagicalAttack() == null)
            {
                goto Label_04AE;
            }
            target.CalcShieldDamage(3, &num, this.IsBattleFlag(5) == 0, skill.AttackDetailType, this.CurrentRand, target3);
            num = Math.Max((num * this.mQuestParam.MagBonus) / 100, 0);
        Label_04AE:
            target.CalcShieldDamage(1, &num, this.IsBattleFlag(5) == 0, skill.AttackDetailType, this.CurrentRand, target3);
            num2 = num10 - num;
        Label_04D6:
            num11 = 0;
            num12 = 0;
            num13 = 0;
            num14 = 0;
            num15 = 0;
            if (skill.IsJewelAttack() != null)
            {
                goto Label_05C9;
            }
            num11 = num;
            if (skill.DamageAbsorbRate <= 0)
            {
                goto Label_051E;
            }
            num13 = ((num * skill.DamageAbsorbRate) * 100) / 0x2710;
            num13 = self.CalcParamRecover(num13);
        Label_051E:
            if (flag2 != null)
            {
                goto Label_05ED;
            }
            switch ((skill.SkillParam.JewelDamageType - 1))
            {
                case 0:
                    goto Label_054B;

                case 1:
                    goto Label_055E;

                case 2:
                    goto Label_0591;
            }
            goto Label_05AB;
        Label_054B:
            num12 += Sqrt(num) * 2;
            goto Label_05AB;
        Label_055E:
            num12 += (target.MaximumStatus.param.mp * skill.SkillParam.JewelDamageValue) / 100;
            goto Label_05AB;
        Label_0591:
            num12 += skill.SkillParam.JewelDamageValue;
        Label_05AB:
            if (skill.SkillParam.IsJewelAbsorb == null)
            {
                goto Label_05ED;
            }
            num14 = num12;
            goto Label_05ED;
        Label_05C9:
            num12 = num;
            if (skill.DamageAbsorbRate <= 0)
            {
                goto Label_05ED;
            }
            num14 = ((num * skill.DamageAbsorbRate) * 100) / 0x2710;
        Label_05ED:
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_07AE;
            }
            if (flag2 != null)
            {
                goto Label_079B;
            }
            if (self.IsPartyMember == null)
            {
                goto Label_0673;
            }
            if (num13 <= 0)
            {
                goto Label_0673;
            }
            if (target.Side != 1)
            {
                goto Label_0673;
            }
            this.mTotalHeal += Math.Min(self.CurrentStatus.param.hp + num13, self.MaximumStatus.param.hp) - self.CurrentStatus.param.hp;
        Label_0673:
            if (skill.IsMhmDamage() == null)
            {
                goto Label_06C1;
            }
            if (num11 <= 0)
            {
                goto Label_06A1;
            }
            target.AddMhmDamage(0, num11);
            target.ReflectMhmDamage(0, num11, 1);
            target.Damage(0, 0);
        Label_06A1:
            if (num12 <= 0)
            {
                goto Label_06D4;
            }
            target.AddMhmDamage(1, num12);
            target.ReflectMhmDamage(1, num12, 0);
            goto Label_06D4;
        Label_06C1:
            target.Damage(num11, 0);
            this.SubGems(target, num12);
        Label_06D4:
            self.Heal(num13);
            this.AddGems(self, num14);
            if (this.CheckGuts(target) == null)
            {
                goto Label_06FB;
            }
            flag = 1;
            target.Heal(1);
        Label_06FB:
            num15 = this.CalcGainedGems(self, target, skill, num, flag3, flag4);
            if (self.IsPartyMember == null)
            {
                goto Label_073D;
            }
            if (num <= 0)
            {
                goto Label_073D;
            }
            if (target.Side != 1)
            {
                goto Label_073D;
            }
            this.mTotalDamages += num;
            goto Label_076C;
        Label_073D:
            if (self.Side != 1)
            {
                goto Label_076C;
            }
            if (num <= 0)
            {
                goto Label_076C;
            }
            if (target.Side != 1)
            {
                goto Label_076C;
            }
            this.mTotalDamages += num;
        Label_076C:
            if (target.IsPartyMember == null)
            {
                goto Label_078E;
            }
            if (num <= 0)
            {
                goto Label_078E;
            }
            this.mTotalDamagesTaken += num;
        Label_078E:
            this.GimmickEventDamageCount(self, target);
            goto Label_07AE;
        Label_079B:
            num11 = 0;
            num12 = 0;
            num13 = 0;
            num14 = 0;
            num15 = 0;
            flag3 = 0;
            flag4 = 0;
        Label_07AE:
            flag6 = this.IsCombinationAttack(skill);
            log.Hit(self, target, num11, num12, 0, 0, 0, 0, 0, 0, num15, flag3, flag2, flag6, flag, num2, flag5, this.GetCriticalRate(self, target, skill), this.GetAvoidRate(self, target, skill));
            if (num13 != null)
            {
                goto Label_07F5;
            }
            if (num14 == null)
            {
                goto Label_080B;
            }
        Label_07F5:
            log.ToSelfSkillEffect(0, 0, 0, 0, num13, num14, 0, 0, 0, 0, 0, 0, 0);
        Label_080B:
            self.SetUnitFlag(0x40, 0);
            self.SetUnitFlag(0x20, 0);
            return;
        }

        private unsafe void Dead(Unit self, Unit target, DeadTypes type, bool is_resume)
        {
            string str;
            int num;
            int num2;
            LogDead dead;
            Unit unit;
            Grid grid;
            int num3;
            int num4;
            Unit unit2;
            int num5;
            if (target.IsUnitFlag(0x2000) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            target.SetUnitFlag(0x2000, 1);
            if (is_resume != null)
            {
                goto Label_0032;
            }
            if (this.DeadSkill(target, self) == null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            if (target.Side != 1)
            {
                goto Label_00D2;
            }
            this.mKillstreak += 1;
            this.mMaxKillstreak = Math.Max(this.mMaxKillstreak, this.mKillstreak);
            str = target.UnitParam.iname;
            if (this.mTargetKillstreakDict.TryGetValue(str, &num) == null)
            {
                goto Label_008B;
            }
            num += 1;
            goto Label_008D;
        Label_008B:
            num = 1;
        Label_008D:
            this.mTargetKillstreakDict[str] = num;
            if (this.mMaxTargetKillstreakDict.TryGetValue(str, &num2) == null)
            {
                goto Label_00C5;
            }
            this.mMaxTargetKillstreakDict[str] = Math.Max(num, num2);
            goto Label_00D2;
        Label_00C5:
            this.mMaxTargetKillstreakDict[str] = num;
        Label_00D2:
            if (self == null)
            {
                goto Label_00ED;
            }
            if (self == target)
            {
                goto Label_00ED;
            }
            self.KillCount += 1;
        Label_00ED:
            if (target.IsDead != null)
            {
                goto Label_00F9;
            }
            return;
        Label_00F9:
            dead = this.Logs.Last as LogDead;
            if (dead != null)
            {
                goto Label_0117;
            }
            dead = this.Log<LogDead>();
        Label_0117:
            dead.Add(target, type);
            target.ForceDead();
            this.GridEventStart(self, target, 1, null);
            if (target.IsPartyMember == null)
            {
                goto Label_03A1;
            }
            if (this.Friend == target)
            {
                goto Label_03A1;
            }
            if (this.GetQuestResult() != null)
            {
                goto Label_03A1;
            }
            if (target.IsNPC != null)
            {
                goto Label_03A1;
            }
            unit = null;
            if (this.IsMultiPlay == null)
            {
                goto Label_017E;
            }
            unit = this.GetSubMemberFirst(target.OwnerPlayerIndex);
            goto Label_0187;
        Label_017E:
            unit = this.GetSubMemberFirst(-1);
        Label_0187:
            if (unit == null)
            {
                goto Label_03A1;
            }
            grid = this.GetCorrectDuplicatePosition(target);
            unit.x = grid.x;
            unit.y = grid.y;
            unit.Direction = target.Direction;
            if (is_resume != null)
            {
                goto Label_01DC;
            }
            unit.IsSub = 0;
            unit.SetUnitFlag(0x400000, 1);
        Label_01DC:
            this.Log<LogUnitEntry>().self = unit;
            this.BeginBattlePassiveSkill(unit);
            unit.UpdateBuffEffects();
            unit.CalcCurrentStatus(0, 0);
            unit.CurrentStatus.param.hp = unit.MaximumStatus.param.hp;
            if (this.IsTower == null)
            {
                goto Label_027B;
            }
            num3 = MonoSingleton<GameManager>.Instance.TowerResuponse.GetUnitDamage(unit.UnitData);
            unit.CurrentStatus.param.hp = Math.Max(unit.CurrentStatus.param.hp - num3, 1);
        Label_027B:
            unit.CurrentStatus.param.mp = unit.GetStartGems();
            num4 = 0;
            goto Label_0386;
        Label_02A0:
            unit2 = this.Player[num4];
            if (unit2.IsSub == null)
            {
                goto Label_0380;
            }
            if (unit2.IsDead != null)
            {
                goto Label_0380;
            }
            if (unit2 != this.Friend)
            {
                goto Label_02D9;
            }
            goto Label_0380;
        Label_02D9:
            unit2.UpdateBuffEffects();
            unit2.CalcCurrentStatus(0, 0);
            unit2.CurrentStatus.param.hp = unit2.MaximumStatus.param.hp;
            if (this.IsTower == null)
            {
                goto Label_0363;
            }
            num5 = MonoSingleton<GameManager>.Instance.TowerResuponse.GetUnitDamage(unit2.UnitData);
            unit2.CurrentStatus.param.hp = Math.Max(unit2.CurrentStatus.param.hp - num5, 1);
        Label_0363:
            unit2.CurrentStatus.param.mp = unit2.GetStartGems();
        Label_0380:
            num4 += 1;
        Label_0386:
            if (num4 < this.Player.Count)
            {
                goto Label_02A0;
            }
            this.UseAutoSkills(unit);
        Label_03A1:
            this.GimmickEventDeadCount(self, target);
            this.UpdateGimmickEventTrick();
            this.UpdateEntryTriggers(3, target, null);
            if (target.IsUnitFlag(0x200000) == null)
            {
                goto Label_0422;
            }
            if (this.Enemys.Contains(target) == null)
            {
                goto Label_03E6;
            }
            this.Enemys.Remove(target);
        Label_03E6:
            if (this.mAllUnits.Contains(target) == null)
            {
                goto Label_0404;
            }
            this.mAllUnits.Remove(target);
        Label_0404:
            if (this.mUnits.Contains(target) == null)
            {
                goto Label_0422;
            }
            this.mUnits.Remove(target);
        Label_0422:
            return;
        }

        private unsafe bool DeadSkill(Unit self, Unit target)
        {
            bool flag;
            int num;
            SkillData data;
            Unit unit;
            LogSkill skill;
            if (self == null)
            {
                goto Label_0011;
            }
            if (self.IsDead != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            flag = 0;
            num = 0;
            goto Label_00D9;
        Label_001C:
            data = self.BattleSkills[num];
            if (data.Timing == 3)
            {
                goto Label_003A;
            }
            goto Label_00D5;
        Label_003A:
            if (data.IsTransformSkill() == null)
            {
                goto Label_00D5;
            }
            unit = this.SearchTransformUnit(self);
            if (unit != null)
            {
                goto Label_0058;
            }
            goto Label_00D5;
        Label_0058:
            skill = null;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_0073;
            }
            skill = new LogSkill();
            goto Label_007B;
        Label_0073:
            skill = this.Log<LogSkill>();
        Label_007B:
            skill.self = self;
            skill.skill = data;
            &skill.pos.x = self.x;
            &skill.pos.y = self.y;
            skill.is_append = data.IsCutin() == 0;
            skill.SetSkillTarget(self, unit);
            this.ExecuteSkill(3, skill, data);
            flag = 1;
        Label_00D5:
            num += 1;
        Label_00D9:
            if (num < self.BattleSkills.Count)
            {
                goto Label_001C;
            }
            return flag;
        }

        private void DebugAssert(bool condition, string msg)
        {
            if (condition != null)
            {
                goto Label_002E;
            }
            if (this.ErrorHandler == null)
            {
                goto Label_001D;
            }
            this.ErrorHandler(msg);
        Label_001D:
            throw new Exception("[Assertion Failed] " + msg);
        Label_002E:
            return;
        }

        private void DebugErr(string s)
        {
            if (this.ErrorHandler == null)
            {
                goto Label_0017;
            }
            this.ErrorHandler(s);
        Label_0017:
            return;
        }

        private void DebugLog(string s)
        {
            if (this.LogHandler == null)
            {
                goto Label_0017;
            }
            this.LogHandler(s);
        Label_0017:
            return;
        }

        private void DebugWarn(string s)
        {
            if (this.WarningHandler == null)
            {
                goto Label_0017;
            }
            this.WarningHandler(s);
        Label_0017:
            return;
        }

        private void DefendSkill(Unit attacker, Unit defender, SkillData atkskl, LogSkill log)
        {
            StatusParam param;
            int num;
            SkillData data;
            int num2;
            int num3;
            int num4;
            bool flag;
            LogSkill.Target target;
            DamageTypes types;
            if (atkskl.IsDamagedSkill() != null)
            {
                goto Label_0017;
            }
            if (defender.IsEnableReactionCondition() != null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            param = defender.CurrentStatus.param;
            num = 0;
            goto Label_027E;
        Label_002A:
            data = defender.BattleSkills[num];
            if (data == null)
            {
                goto Label_027A;
            }
            if (data.IsReactionSkill() == null)
            {
                goto Label_027A;
            }
            if (data.EffectType == 3)
            {
                goto Label_0059;
            }
            goto Label_027A;
        Label_0059:
            if (data.Timing == 6)
            {
                goto Label_0076;
            }
            if (data.Timing == 4)
            {
                goto Label_0076;
            }
            goto Label_027A;
        Label_0076:
            if (defender.IsEnableReactionSkill(data) != null)
            {
                goto Label_0087;
            }
            goto Label_027A;
        Label_0087:
            if (this.CheckSkillCondition(defender, data) != null)
            {
                goto Label_0099;
            }
            goto Label_027A;
        Label_0099:
            num2 = data.EffectRate;
            if (num2 <= 0)
            {
                goto Label_00CC;
            }
            if (num2 >= 100)
            {
                goto Label_00CC;
            }
            num3 = this.GetRandom() % 100;
            if (num3 <= num2)
            {
                goto Label_00CC;
            }
            goto Label_027A;
        Label_00CC:
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_00DD;
            }
            goto Label_027A;
        Label_00DD:
            num4 = this.GetSkillEffectValue(defender, attacker, data, null);
            if (num4 != null)
            {
                goto Label_00F5;
            }
            goto Label_027A;
        Label_00F5:
            flag = 0;
            switch ((data.ReactionDamageType - 1))
            {
                case 0:
                    goto Label_01A8;

                case 1:
                    goto Label_011A;

                case 2:
                    goto Label_0161;
            }
            goto Label_0212;
        Label_011A:
            if (atkskl.IsPhysicalAttack() == null)
            {
                goto Label_0212;
            }
            if (data.IsReactionDet(atkskl.AttackDetailType) == null)
            {
                goto Label_0212;
            }
            flag = 1;
            param.def = SkillParam.CalcSkillEffectValue(data.EffectCalcType, num4, param.def);
            goto Label_0212;
        Label_0161:
            if (atkskl.IsMagicalAttack() == null)
            {
                goto Label_0212;
            }
            if (data.IsReactionDet(atkskl.AttackDetailType) == null)
            {
                goto Label_0212;
            }
            flag = 1;
            param.mnd = SkillParam.CalcSkillEffectValue(data.EffectCalcType, num4, param.mnd);
            goto Label_0212;
        Label_01A8:
            if (atkskl.IsDamagedSkill() == null)
            {
                goto Label_0212;
            }
            if (data.IsReactionDet(atkskl.AttackDetailType) == null)
            {
                goto Label_0212;
            }
            flag = 1;
            param.def = SkillParam.CalcSkillEffectValue(data.EffectCalcType, num4, param.def);
            param.mnd = SkillParam.CalcSkillEffectValue(data.EffectCalcType, num4, param.mnd);
        Label_0212:
            if (log == null)
            {
                goto Label_027A;
            }
            if (flag == null)
            {
                goto Label_027A;
            }
            target = log.FindTarget(defender);
            if (target == null)
            {
                goto Label_027A;
            }
            target.SetDefend(1);
            target.defSkill = data;
            target.defSkillUseCount = 0;
            if (data.SkillParam.count <= 0)
            {
                goto Label_027A;
            }
            defender.UpdateSkillUseCount(data, -1);
            target.defSkillUseCount = defender.GetSkillUseCount(data);
        Label_027A:
            num += 1;
        Label_027E:
            if (num < defender.BattleSkills.Count)
            {
                goto Label_002A;
            }
            return;
        }

        public unsafe bool Deserialize(string questID, Json_Battle jsonBtl, int myPlayerIndex, UnitData[] units, int[] ownerPlayerIndex, bool is_restart, int[] placementIndex, bool[] sub)
        {
            GameManager manager;
            PlayerPartyTypes types;
            PlayerPartyTypes types2;
            int num;
            BattleMap map;
            RandDeckResult[] resultArray;
            List<UnitSetting> list;
            List<UnitSubSetting> list2;
            int num2;
            int num3;
            string[] strArray;
            PartySlotTypeUnitPair[] pairArray;
            int num4;
            string str;
            UnitData data;
            UnitData data2;
            Unit unit;
            int num5;
            long num6;
            UnitData data3;
            UnitData data4;
            TowerFloorParam param;
            int num7;
            PartyData data5;
            bool flag;
            UnitSetting setting;
            Unit unit2;
            UnitData data6;
            Exception exception;
            Unit unit3;
            int num8;
            Json_BtlOrdeal ordeal;
            int num9;
            long num10;
            UnitData data7;
            UnitData data8;
            UnitSetting setting2;
            Unit unit4;
            UnitData data9;
            Exception exception2;
            Unit unit5;
            OInt num11;
            string str2;
            int num12;
            int num13;
            int num14;
            int num15;
            List<NPCSetting> list3;
            int num16;
            Unit unit6;
            Unit.DropItem item;
            Json_BtlDrop[] dropArray;
            ItemParam param2;
            ConceptCardParam param3;
            Unit.DropItem item2;
            Json_BtlSteal[] stealArray;
            ItemParam param4;
            bool flag2;
            ArenaPlayer player;
            List<UnitSetting> list4;
            int num17;
            UnitData data10;
            Unit unit7;
            int num18;
            List<NPCSetting> list5;
            int num19;
            Unit unit8;
            Unit unit9;
            List<Unit>.Enumerator enumerator;
            List<NPCSetting> list6;
            int num20;
            Unit unit10;
            Unit unit11;
            List<Unit>.Enumerator enumerator2;
            List<NPCSetting> list7;
            int num21;
            Unit unit12;
            int num22;
            int num23;
            int num24;
            ItemData data11;
            ItemData data12;
            QuestTypes types3;
            EUnitSide side;
            if (((jsonBtl == null) | string.IsNullOrEmpty(questID)) == null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            this.mMyPlayerIndex = myPlayerIndex;
            if (this.mMyPlayerIndex > 0)
            {
                goto Label_0035;
            }
            this.DebugLog("[PUN]this is singleplay");
            goto Label_0047;
        Label_0035:
            this.IsMultiPlay = 1;
            this.DebugLog("[PUN]this is multiplay");
        Label_0047:
            manager = MonoSingleton<GameManager>.Instance;
            this.mBtlID = jsonBtl.btlid;
            this.mMapIndex = 0;
            this.mLeaderIndex = -1;
            this.mFriendIndex = -1;
            this.mFriendStates = 0;
            this.mWinTriggerCount = 0;
            this.mLoseTriggerCount = 0;
            if (((jsonBtl == null) || (jsonBtl.btlinfo == null)) || (jsonBtl.btlinfo.quest_ranking == null))
            {
                goto Label_00DA;
            }
            this.mRankingQuestParam = RankingQuestParam.FindRankingQuestParam(questID, jsonBtl.btlinfo.quest_ranking.schedule_id, jsonBtl.btlinfo.quest_ranking.type);
        Label_00DA:
            this.mQuestParam = MonoSingleton<GameManager>.Instance.FindQuest(questID);
            DebugUtility.Assert((this.mQuestParam == null) == 0, "mQuestParam == null");
            this.mQuestParam.GetPartyTypes(&types, &types2);
            this.mSeed = jsonBtl.btlinfo.seed;
            this.mRand.Seed(this.mSeed);
            this.CurrentRand = this.mRand;
            num = 0;
            goto Label_01AC;
        Label_0145:
            map = new BattleMap();
            map.mRandDeckResult = jsonBtl.btlinfo.lot_enemies;
            resultArray = jsonBtl.btlinfo.GetDeck();
            if (resultArray == null)
            {
                goto Label_017B;
            }
            map.mRandDeckResult = resultArray;
        Label_017B:
            if (map.Initialize(this, this.mQuestParam.map[num]) != null)
            {
                goto Label_019B;
            }
            return 0;
        Label_019B:
            this.mMap.Add(map);
            num += 1;
        Label_01AC:
            if (num < this.mQuestParam.map.Count)
            {
                goto Label_0145;
            }
            list = this.mMap[0].PartyUnitSettings;
            list2 = this.mMap[0].PartyUnitSubSettings;
            if ((list == null) || (list.Count <= 0))
            {
                goto Label_0A18;
            }
            if (((this.IsMultiPlay == null) && (manager.AudienceMode == null)) && (manager.IsVSCpuBattle == null))
            {
                goto Label_0237;
            }
            if (this.SetupMultiPlayUnit(units, ownerPlayerIndex, placementIndex, sub) != null)
            {
                goto Label_0A07;
            }
            return 0;
            goto Label_0A07;
        Label_0237:
            num2 = 0;
            num3 = 0;
            strArray = null;
            if (this.mQuestParam.questParty == null)
            {
                goto Label_02B4;
            }
            if (<>f__am$cache76 != null)
            {
                goto Label_027C;
            }
            <>f__am$cache76 = new Func<PartySlotTypeUnitPair, bool>(BattleCore.<Deserialize>m__5A);
        Label_027C:
            if (<>f__am$cache77 != null)
            {
                goto Label_029E;
            }
            <>f__am$cache77 = new Func<PartySlotTypeUnitPair, string>(BattleCore.<Deserialize>m__5B);
        Label_029E:
            strArray = Enumerable.ToArray<string>(Enumerable.Select<PartySlotTypeUnitPair, string>(Enumerable.Where<PartySlotTypeUnitPair>(this.mQuestParam.questParty.GetMainSubSlots(), <>f__am$cache76), <>f__am$cache77));
            goto Label_02C6;
        Label_02B4:
            strArray = this.mQuestParam.units.GetList();
        Label_02C6:
            if ((strArray == null) || (((int) strArray.Length) <= 0))
            {
                goto Label_03CC;
            }
            num4 = 0;
            goto Label_03C1;
        Label_02DF:
            str = strArray[num4];
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_02F7;
            }
            goto Label_03BB;
        Label_02F7:
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(str);
            if (data != null)
            {
                goto Label_031E;
            }
            this.DebugErr("player uniqueid not equal");
            return 0;
        Label_031E:
            data2 = new UnitData();
            data2.Setup(data);
            data2.SetJob(types);
            unit = new Unit();
            if (unit.Setup(data2, list[num2], null, null) != null)
            {
                goto Label_0363;
            }
            this.DebugErr("failed unit Setup");
            return 0;
        Label_0363:
            unit.IsPartyMember = 1;
            unit.SetUnitFlag(8, 1);
            unit.SetUnitFlag(0x800, 1);
            unit.SetUnitFlag(0x40000, 1);
            this.mPlayer.Add(unit);
            this.mAllUnits.Add(unit);
            this.mStartingMembers.Add(unit);
            num2 += 1;
        Label_03BB:
            num4 += 1;
        Label_03C1:
            if (num4 < ((int) strArray.Length))
            {
                goto Label_02DF;
            }
        Label_03CC:
            if (jsonBtl.btlinfo.units == null)
            {
                goto Label_0652;
            }
            num5 = 0;
            goto Label_063E;
        Label_03E4:
            num6 = (long) jsonBtl.btlinfo.units[num5].iid;
            if (num6 > 0L)
            {
                goto Label_0408;
            }
            goto Label_0638;
        Label_0408:
            data3 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(num6);
            if (data3 != null)
            {
                goto Label_042F;
            }
            this.DebugErr("player uniqueid not equal");
            return 0;
        Label_042F:
            data4 = new UnitData();
            data4.Setup(data3);
            if (this.mQuestParam.type != 7)
            {
                goto Label_0497;
            }
            num7 = (MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mQuestParam.iname).can_help == null) ? -1 : 0;
            if (list.Count > (num2 + num7))
            {
                goto Label_04AA;
            }
            goto Label_0638;
            goto Label_04AA;
        Label_0497:
            if (list.Count > num2)
            {
                goto Label_04AA;
            }
            goto Label_0638;
        Label_04AA:
            if (this.mQuestParam.IsUnitAllowed(data4) != null)
            {
                goto Label_04C1;
            }
            goto Label_0638;
        Label_04C1:
            data5 = MonoSingleton<GameManager>.Instance.Player.GetPartyCurrent();
            flag = num5 < data5.MAX_MAINMEMBER;
            if (flag == null)
            {
                goto Label_04FD;
            }
            if (num5 < this.mQuestParam.GetSelectMainMemberNum())
            {
                goto Label_04FD;
            }
            goto Label_0638;
        Label_04FD:
            setting = null;
            if (this.mQuestParam.type != 7)
            {
                goto Label_053F;
            }
            if (flag != null)
            {
                goto Label_052F;
            }
            setting = list[list.Count - 1];
            goto Label_053A;
        Label_052F:
            setting = list[num2];
        Label_053A:
            goto Label_054A;
        Label_053F:
            setting = list[num2];
        Label_054A:
            if (flag != null)
            {
                goto Label_05A3;
            }
            setting = new UnitSetting();
            setting.side = 0;
            if (num3 >= list2.Count)
            {
                goto Label_05A3;
            }
            setting.startCtCalc = list2[num3].startCtCalc;
            setting.startCtVal = list2[num3].startCtVal;
            num3 += 1;
        Label_05A3:
            unit2 = new Unit();
            if (unit2.Setup(data4, setting, null, null) != null)
            {
                goto Label_05C9;
            }
            this.DebugErr("failed unit Setup");
            return 0;
        Label_05C9:
            if (this.mLeaderIndex != -1)
            {
                goto Label_05E7;
            }
            this.mLeaderIndex = num2;
        Label_05E7:
            if (flag == null)
            {
                goto Label_0601;
            }
            this.mStartingMembers.Add(unit2);
            num2 += 1;
        Label_0601:
            unit2.SetUnitFlag(8, 1);
            unit2.IsPartyMember = 1;
            unit2.IsSub = flag == 0;
            this.mPlayer.Add(unit2);
            this.mAllUnits.Add(unit2);
        Label_0638:
            num5 += 1;
        Label_063E:
            if (num5 < ((int) jsonBtl.btlinfo.units.Length))
            {
                goto Label_03E4;
            }
        Label_0652:
            if (jsonBtl.btlinfo.help == null)
            {
                goto Label_0776;
            }
            if (num2 >= list.Count)
            {
                goto Label_0776;
            }
            data6 = new UnitData();
        Label_0677:
            try
            {
                data6.Deserialize(jsonBtl.btlinfo.help.unit);
                goto Label_06BD;
            }
            catch (Exception exception1)
            {
            Label_0693:
                exception = exception1;
                this.DebugErr("<EXCEPTION> " + exception.Message + "\n-----------------------\n" + exception.StackTrace);
                goto Label_06BD;
            }
        Label_06BD:
            unit3 = new Unit();
            if (unit3.Setup(data6, list[num2], null, null) != null)
            {
                goto Label_06EA;
            }
            this.DebugErr("failed unit Setup");
            return 0;
        Label_06EA:
            unit3.IsPartyMember = 1;
            unit3.IsSub = 0;
            unit3.SetUnitFlag(8, 1);
            unit3.SetUnitFlag(0x40000, 1);
            unit3.SetUnitFlag(0x2000000, 1);
            this.mFriendIndex = this.mPlayer.Count;
            this.mPlayer.Add(unit3);
            this.mAllUnits.Add(unit3);
            this.mStartingMembers.Add(unit3);
            this.mFriendStates = jsonBtl.btlinfo.help.isFriend;
            num2 += 1;
        Label_0776:
            this.mCurrentTeamId = 0;
            this.mMaxTeamId = 1;
            if (jsonBtl.btlinfo.ordeals == null)
            {
                goto Label_0A07;
            }
            if (((int) jsonBtl.btlinfo.ordeals.Length) <= 1)
            {
                goto Label_0A07;
            }
            num8 = 1;
            goto Label_09F3;
        Label_07AF:
            ordeal = jsonBtl.btlinfo.ordeals[num8];
            if (ordeal.units != null)
            {
                goto Label_07D0;
            }
            goto Label_09ED;
        Label_07D0:
            num9 = 0;
            goto Label_08C7;
        Label_07D8:
            num10 = (long) ordeal.units[num9].iid;
            if (num10 > 0L)
            {
                goto Label_07F8;
            }
            goto Label_08C1;
        Label_07F8:
            data7 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(num10);
            if (data7 != null)
            {
                goto Label_081F;
            }
            this.DebugErr("ordeals/player uniqueid not equal");
            return 0;
        Label_081F:
            data8 = new UnitData();
            data8.Setup(data7);
            if (this.mQuestParam.IsUnitAllowed(data8) != null)
            {
                goto Label_0846;
            }
            goto Label_08C1;
        Label_0846:
            setting2 = list[num9];
            unit4 = new Unit();
            if (unit4.Setup(data8, setting2, null, null) != null)
            {
                goto Label_0877;
            }
            this.DebugErr("ordeals/failed unit Setup");
            return 0;
        Label_0877:
            unit4.SetUnitFlag(8, 1);
            unit4.SetUnitFlag(1, 0);
            unit4.SetUnitFlag(0x1000000, 1);
            unit4.TeamId = num8;
            unit4.IsPartyMember = 1;
            this.mPlayer.Add(unit4);
            this.mAllUnits.Add(unit4);
        Label_08C1:
            num9 += 1;
        Label_08C7:
            if (num9 < ((int) ordeal.units.Length))
            {
                goto Label_07D8;
            }
            if (ordeal.help == null)
            {
                goto Label_09DF;
            }
            data9 = new UnitData();
        Label_08EA:
            try
            {
                data9.Deserialize(ordeal.help.unit);
                goto Label_092C;
            }
            catch (Exception exception3)
            {
            Label_0902:
                exception2 = exception3;
                this.DebugErr("ordeals/friend <EXCEPTION> " + exception2.Message + "\n-----------------------\n" + exception2.StackTrace);
                goto Label_092C;
            }
        Label_092C:
            unit5 = new Unit();
            if (unit5.Setup(data9, list[(int) ordeal.units.Length], null, null) != null)
            {
                goto Label_0960;
            }
            this.DebugErr("ordeals/failed unit Setup");
            return 0;
        Label_0960:
            unit5.IsPartyMember = 1;
            unit5.IsSub = 0;
            unit5.SetUnitFlag(8, 1);
            unit5.SetUnitFlag(0x40000, 1);
            unit5.SetUnitFlag(1, 0);
            unit5.SetUnitFlag(0x1000000, 1);
            unit5.SetUnitFlag(0x2000000, 1);
            unit5.TeamId = num8;
            unit5.FriendStates = ordeal.help.isFriend;
            this.mPlayer.Add(unit5);
            this.mAllUnits.Add(unit5);
        Label_09DF:
            this.mMaxTeamId += 1;
        Label_09ED:
            num8 += 1;
        Label_09F3:
            if (num8 < ((int) jsonBtl.btlinfo.ordeals.Length))
            {
                goto Label_07AF;
            }
        Label_0A07:
            this.mNpcStartIndex = this.mAllUnits.Count;
        Label_0A18:
            num11 = -1;
            if (this.mLeaderIndex != num11)
            {
                goto Label_0A54;
            }
            if (this.mPlayer.Count < 1)
            {
                goto Label_0A54;
            }
            this.mLeaderIndex = 0;
        Label_0A54:
            this.mEnemys = new List<Unit>[this.mMap.Count];
            switch (this.mQuestParam.type)
            {
                case 0:
                    goto Label_0AC7;

                case 1:
                    goto Label_0AC7;

                case 2:
                    goto Label_0E37;

                case 3:
                    goto Label_0AC7;

                case 4:
                    goto Label_0AC7;

                case 5:
                    goto Label_0AC7;

                case 6:
                    goto Label_0AC7;

                case 7:
                    goto Label_0AC7;

                case 8:
                    goto Label_100C;

                case 9:
                    goto Label_100C;

                case 10:
                    goto Label_0AC7;

                case 11:
                    goto Label_0AC7;

                case 12:
                    goto Label_0AC7;

                case 13:
                    goto Label_0AC7;

                case 14:
                    goto Label_0AC7;

                case 15:
                    goto Label_0AC7;

                case 0x10:
                    goto Label_1148;
            }
            goto Label_126C;
        Label_0AC7:
            str2 = null;
            num12 = 0;
            if (this.mQuestParam.questParty == null)
            {
                goto Label_0B01;
            }
            str2 = this.mQuestParam.questParty.GetNpcLeaderUnitIname();
            num12 = this.mQuestParam.questParty.l_npc_rare;
        Label_0B01:
            num13 = this.mPlayer.Count;
            num14 = 0;
            num15 = 0;
            goto Label_0E20;
        Label_0B19:
            this.mEnemys[num15] = new List<Unit>(MAX_ENEMY);
            list3 = this.mMap[num15].NPCUnitSettings;
            if (list3 != null)
            {
                goto Label_0B4C;
            }
            goto Label_0E1A;
        Label_0B4C:
            num16 = 0;
            goto Label_0E0C;
        Label_0B54:
            unit6 = new Unit();
            item = null;
            dropArray = jsonBtl.btlinfo.drops;
            if (dropArray == null)
            {
                goto Label_0C7E;
            }
            if (num14 >= ((int) dropArray.Length))
            {
                goto Label_0C7E;
            }
            if (string.IsNullOrEmpty(dropArray[num14].iname) != null)
            {
                goto Label_0C7E;
            }
            if (dropArray[num14].num <= 0)
            {
                goto Label_0C7E;
            }
            param2 = null;
            param3 = null;
            if (dropArray[num14].dropItemType != 2)
            {
                goto Label_0BD2;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetItemParam(dropArray[num14].iname);
            goto Label_0C25;
        Label_0BD2:
            if (dropArray[num14].dropItemType != 3)
            {
                goto Label_0C02;
            }
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(dropArray[num14].iname);
            goto Label_0C25;
        Label_0C02:
            DebugUtility.LogError(string.Format("不明なドロップ品が登録されています。iname:{0} (itype:{1})", dropArray[num14].iname, dropArray[num14].itype));
        Label_0C25:
            if (param2 != null)
            {
                goto Label_0C33;
            }
            if (param3 == null)
            {
                goto Label_0C7E;
            }
        Label_0C33:
            item = new Unit.DropItem();
            item.itemParam = param2;
            item.conceptCardParam = param3;
            item.num = dropArray[num14].num;
            item.is_secret = (dropArray[num14].secret == 0) == 0;
        Label_0C7E:
            item2 = null;
            stealArray = jsonBtl.btlinfo.steals;
            if (stealArray == null)
            {
                goto Label_0D07;
            }
            if (num14 >= ((int) stealArray.Length))
            {
                goto Label_0D07;
            }
            if (string.IsNullOrEmpty(stealArray[num14].iname) != null)
            {
                goto Label_0D07;
            }
            if (stealArray[num14].num <= 0)
            {
                goto Label_0D07;
            }
            param4 = MonoSingleton<GameManager>.Instance.GetItemParam(stealArray[num14].iname);
            if (param4 == null)
            {
                goto Label_0D07;
            }
            item2 = new Unit.DropItem();
            item2.itemParam = param4;
            item2.num = stealArray[num14].num;
        Label_0D07:
            flag2 = 0;
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_0D50;
            }
            if ((list3[num16].iname == str2) == null)
            {
                goto Label_0D50;
            }
            flag2 = 1;
            list3[num16].rare = num12;
            str2 = null;
        Label_0D50:
            if (unit6.Setup(null, list3[num16], item, item2) != null)
            {
                goto Label_0D77;
            }
            this.DebugErr("enemy unit setup failed");
            return 0;
        Label_0D77:
            unit6.SetUnitFlag(0x40000, 1);
            switch (unit6.Side)
            {
                case 0:
                    goto Label_0DA5;

                case 1:
                    goto Label_0DDE;

                case 2:
                    goto Label_0DDE;
            }
            goto Label_0DF3;
        Label_0DA5:
            if (flag2 == null)
            {
                goto Label_0DB9;
            }
            this.mLeaderIndex = num13;
        Label_0DB9:
            this.mPlayer.Add(unit6);
            this.mStartingMembers.Add(unit6);
            num13 += 1;
            goto Label_0DF3;
        Label_0DDE:
            this.mEnemys[num15].Add(unit6);
        Label_0DF3:
            this.mAllUnits.Add(unit6);
            num14 += 1;
            num16 += 1;
        Label_0E0C:
            if (num16 < list3.Count)
            {
                goto Label_0B54;
            }
        Label_0E1A:
            num15 += 1;
        Label_0E20:
            if (num15 < this.mMap.Count)
            {
                goto Label_0B19;
            }
            goto Label_126C;
        Label_0E37:
            player = GlobalVars.SelectedArenaPlayer;
            if (player == null)
            {
                goto Label_0F23;
            }
            list4 = this.mMap[0].ArenaUnitSettings;
            this.mEnemys[0] = new List<Unit>((int) player.Unit.Length);
            num17 = 0;
            goto Label_0F07;
        Label_0E7B:
            if (player.Unit[num17] != null)
            {
                goto Label_0E8F;
            }
            goto Label_0F01;
        Label_0E8F:
            data10 = new UnitData();
            data10.Setup(player.Unit[num17]);
            data10.SetJob(types2);
            unit7 = new Unit();
            if (unit7.Setup(data10, list4[num17], null, null) != null)
            {
                goto Label_0EDC;
            }
            this.DebugErr("failed unit Setup");
            return 0;
        Label_0EDC:
            unit7.SetUnitFlag(8, 1);
            this.mEnemys[0].Add(unit7);
            this.mAllUnits.Add(unit7);
        Label_0F01:
            num17 += 1;
        Label_0F07:
            if (num17 < ((int) player.Unit.Length))
            {
                goto Label_0E7B;
            }
            this.mEnemyLeaderIndex = 0;
        Label_0F23:
            num18 = 0;
            goto Label_0F49;
        Label_0F2B:
            this.mAllUnits[num18].SetUnitFlag(0x1000, 1);
            num18 += 1;
        Label_0F49:
            if (num18 < this.mAllUnits.Count)
            {
                goto Label_0F2B;
            }
            list5 = this.mMap[0].NPCUnitSettings;
            if (list5 == null)
            {
                goto Label_126C;
            }
            num19 = 0;
            goto Label_0FF9;
        Label_0F7D:
            unit8 = new Unit();
            if (unit8.Setup(null, list5[num19], null, null) != null)
            {
                goto Label_0FAC;
            }
            this.DebugErr("Arena: enemy unit setup failed");
            goto Label_0FF3;
        Label_0FAC:
            if (unit8.IsBreakObj == null)
            {
                goto Label_0FF3;
            }
            if (unit8.Side == 2)
            {
                goto Label_0FCA;
            }
            goto Label_0FF3;
        Label_0FCA:
            unit8.SetUnitFlag(0x40000, 1);
            this.mEnemys[0].Add(unit8);
            this.mAllUnits.Add(unit8);
        Label_0FF3:
            num19 += 1;
        Label_0FF9:
            if (num19 < list5.Count)
            {
                goto Label_0F7D;
            }
            goto Label_126C;
        Label_100C:
            this.mEnemyLeaderIndex = 0;
            this.mEnemys[0] = new List<Unit>(MAX_ENEMY);
            enumerator = this.AllUnits.GetEnumerator();
        Label_1037:
            try
            {
                goto Label_1079;
            Label_103C:
                unit9 = &enumerator.Current;
                if (unit9.Side != 1)
                {
                    goto Label_1079;
                }
                this.mEnemys[0].Add(unit9);
                if (manager.IsVSCpuBattle == null)
                {
                    goto Label_1079;
                }
                unit9.SetUnitFlag(0x1000, 1);
            Label_1079:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_103C;
                }
                goto Label_1097;
            }
            finally
            {
            Label_108A:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_1097:
            list6 = this.mMap[0].NPCUnitSettings;
            if (list6 == null)
            {
                goto Label_126C;
            }
            num20 = 0;
            goto Label_1135;
        Label_10B9:
            unit10 = new Unit();
            if (unit10.Setup(null, list6[num20], null, null) != null)
            {
                goto Label_10E8;
            }
            this.DebugErr("Versus: enemy unit setup failed");
            goto Label_112F;
        Label_10E8:
            if (unit10.IsBreakObj == null)
            {
                goto Label_112F;
            }
            if (unit10.Side == 2)
            {
                goto Label_1106;
            }
            goto Label_112F;
        Label_1106:
            unit10.SetUnitFlag(0x40000, 1);
            this.mEnemys[0].Add(unit10);
            this.mAllUnits.Add(unit10);
        Label_112F:
            num20 += 1;
        Label_1135:
            if (num20 < list6.Count)
            {
                goto Label_10B9;
            }
            goto Label_126C;
        Label_1148:
            this.mEnemyLeaderIndex = 0;
            this.mEnemys[0] = new List<Unit>(MAX_ENEMY);
            enumerator2 = this.AllUnits.GetEnumerator();
        Label_1173:
            try
            {
                goto Label_119D;
            Label_1178:
                unit11 = &enumerator2.Current;
                if (unit11.Side != 1)
                {
                    goto Label_119D;
                }
                this.mEnemys[0].Add(unit11);
            Label_119D:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_1178;
                }
                goto Label_11BB;
            }
            finally
            {
            Label_11AE:
                ((List<Unit>.Enumerator) enumerator2).Dispose();
            }
        Label_11BB:
            list7 = this.mMap[0].NPCUnitSettings;
            if (list7 == null)
            {
                goto Label_126C;
            }
            num21 = 0;
            goto Label_1259;
        Label_11DD:
            unit12 = new Unit();
            if (unit12.Setup(null, list7[num21], null, null) != null)
            {
                goto Label_120C;
            }
            this.DebugErr("Versus: enemy unit setup failed");
            goto Label_1253;
        Label_120C:
            if (unit12.IsBreakObj == null)
            {
                goto Label_1253;
            }
            if (unit12.Side == 2)
            {
                goto Label_122A;
            }
            goto Label_1253;
        Label_122A:
            unit12.SetUnitFlag(0x40000, 1);
            this.mEnemys[0].Add(unit12);
            this.mAllUnits.Add(unit12);
        Label_1253:
            num21 += 1;
        Label_1259:
            if (num21 < list7.Count)
            {
                goto Label_11DD;
            }
        Label_126C:
            this.mEntryUnitMax = this.mAllUnits.Count;
            if (jsonBtl.btlinfo.atkmags == null)
            {
                goto Label_12A3;
            }
            this.mQuestParam.SetAtkTypeMag(jsonBtl.btlinfo.atkmags);
        Label_12A3:
            if (jsonBtl.btlinfo.campaigns == null)
            {
                goto Label_12C4;
            }
            this.mQuestCampaignIds = jsonBtl.btlinfo.campaigns;
        Label_12C4:
            if (is_restart != null)
            {
                goto Label_1350;
            }
            num22 = this.mEntryUnitMax - this.mNpcStartIndex;
            if (num22 == null)
            {
                goto Label_1350;
            }
            this.mRecord.drops = new OInt[num22];
            Array.Clear(this.mRecord.drops, 0, num22);
            this.mRecord.item_steals = new OInt[num22];
            Array.Clear(this.mRecord.item_steals, 0, num22);
            this.mRecord.gold_steals = new OInt[num22];
            Array.Clear(this.mRecord.gold_steals, 0, num22);
        Label_1350:
            this.UpdateUnitName();
            if (this.CurrentMap == null)
            {
                goto Label_13A9;
            }
            if (this.mQuestParam.IsNoStartVoice == null)
            {
                goto Label_13A9;
            }
            num23 = 0;
            goto Label_1397;
        Label_1379:
            this.mAllUnits[num23].SetUnitFlag(0x8000, 1);
            num23 += 1;
        Label_1397:
            if (num23 < this.mAllUnits.Count)
            {
                goto Label_1379;
            }
        Label_13A9:
            this.BeginBattlePassiveSkill();
            if (this.mQuestParam.type != 7)
            {
                goto Label_13EB;
            }
            MonoSingleton<GameManager>.Instance.TowerResuponse.CalcDamage(this.Player);
            MonoSingleton<GameManager>.Instance.TowerResuponse.CalcEnemyDamage(this.Enemys, 0);
        Label_13EB:
            num24 = 0;
            goto Label_1463;
        Label_13F3:
            this.mInventory[num24] = null;
            data11 = MonoSingleton<GameManager>.GetInstanceDirect().Player.Inventory[num24];
            if (data11 != null)
            {
                goto Label_141D;
            }
            goto Label_145D;
        Label_141D:
            data12 = new ItemData();
            data12.Setup(data11.UniqueID, data11.Param, Math.Min(data11.Num, data11.Param.invcap));
            this.mInventory[num24] = data12;
        Label_145D:
            num24 += 1;
        Label_1463:
            if (num24 < 5)
            {
                goto Label_13F3;
            }
            this.mRand.Seed(this.mSeed);
            this.SetBattleFlag(0, 1);
            return 1;
        }

        public unsafe Unit DirectFindUnitAtGrid(Grid grid)
        {
            Unit unit;
            List<Unit>.Enumerator enumerator;
            bool flag;
            SceneBattle battle;
            TacticsUnitController controller;
            IntVector2 vector;
            Unit unit2;
            enumerator = this.mUnits.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_00C3;
            Label_0011:
                unit = &enumerator.Current;
                if (unit.IsGimmick == null)
                {
                    goto Label_0029;
                }
                goto Label_00C3;
            Label_0029:
                flag = 0;
                if (unit != this.CurrentUnit)
                {
                    goto Label_009D;
                }
                battle = SceneBattle.Instance;
                if ((battle != null) == null)
                {
                    goto Label_009D;
                }
                controller = battle.FindUnitController(unit);
                if (controller == null)
                {
                    goto Label_009D;
                }
                vector = battle.CalcCoord(controller.CenterPosition);
                if (unit.CheckCollisionDirect(&vector.x, &vector.y, grid.x, grid.y, 1) == null)
                {
                    goto Label_009B;
                }
                unit2 = unit;
                goto Label_00E2;
            Label_009B:
                flag = 1;
            Label_009D:
                if (flag != null)
                {
                    goto Label_00C3;
                }
                if (unit.CheckCollision(grid.x, grid.y, 1) == null)
                {
                    goto Label_00C3;
                }
                unit2 = unit;
                goto Label_00E2;
            Label_00C3:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_00E0;
            }
            finally
            {
            Label_00D4:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_00E0:
            return null;
        Label_00E2:
            return unit2;
        }

        private void EndMoveSkill(Unit self, int step)
        {
        }

        public bool EntryBattleMultiPlay(EBattleCommand type, Unit current, Unit enemy, SkillData skill, ItemData item, int gx, int gy, bool unitLockTarget)
        {
            return this.ExecMultiPlayerCommandCore(type, current, enemy, skill, item, gx, gy, unitLockTarget);
        }

        private void EntryUnit(Unit self)
        {
            Grid grid;
            grid = this.GetCorrectDuplicatePosition(self);
            self.x = grid.x;
            self.y = grid.y;
            self.SetUnitFlag(1, 1);
            self.SetUnitFlag(0x400000, 1);
            self.IsSub = 0;
            this.Log<LogUnitEntry>().self = self;
            return;
        }

        private bool EntryUseSkill(Unit self, SkillData skill, bool forced, bool is_no_add_rate)
        {
            List<SkillData> list;
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (skill.SkillType == null)
            {
                goto Label_002D;
            }
            if (skill.SkillType == 1)
            {
                goto Label_002D;
            }
            if (skill.SkillType == 3)
            {
                goto Label_002D;
            }
            return 0;
        Label_002D:
            if (skill.Timing == null)
            {
                goto Label_003A;
            }
            return 0;
        Label_003A:
            if (skill.EffectType != 0x16)
            {
                goto Label_0049;
            }
            return 0;
        Label_0049:
            if (skill.IsSetBreakObjSkill() == null)
            {
                goto Label_0056;
            }
            return 0;
        Label_0056:
            if (this.CheckEnableUseSkill(self, skill, 0) == null)
            {
                goto Label_0164;
            }
            if (forced == null)
            {
                goto Label_007D;
            }
            this.mSkillMap.forceSkillList.Add(skill);
            return 1;
        Label_007D:
            if (this.CheckUseSkill(self, skill, is_no_add_rate) == null)
            {
                goto Label_0164;
            }
            list = null;
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_00AA;
            }
            list = this.mSkillMap.damageSkills;
            goto Label_0153;
        Label_00AA:
            if (skill.IsHealSkill() == null)
            {
                goto Label_00C6;
            }
            list = this.mSkillMap.healSkills;
            goto Label_0153;
        Label_00C6:
            if (skill.IsSupportSkill() == null)
            {
                goto Label_00E2;
            }
            list = this.mSkillMap.supportSkills;
            goto Label_0153;
        Label_00E2:
            if (skill.EffectType != 12)
            {
                goto Label_0100;
            }
            list = this.mSkillMap.cureConditionSkills;
            goto Label_0153;
        Label_0100:
            if (skill.EffectType != 11)
            {
                goto Label_011E;
            }
            list = this.mSkillMap.failConditionSkills;
            goto Label_0153;
        Label_011E:
            if (skill.EffectType != 13)
            {
                goto Label_013C;
            }
            list = this.mSkillMap.disableConditionSkills;
            goto Label_0153;
        Label_013C:
            if (skill.TeleportType == null)
            {
                goto Label_0153;
            }
            list = this.mSkillMap.exeSkills;
        Label_0153:
            if (list != null)
            {
                goto Label_015B;
            }
            return 0;
        Label_015B:
            list.Add(skill);
            return 1;
        Label_0164:
            return 0;
        }

        private unsafe bool EventTriggerWithdrawContinue(Unit unit)
        {
            if (unit.SettingNPC == null)
            {
                goto Label_0026;
            }
            if (unit.EventTrigger == null)
            {
                goto Label_0026;
            }
            if (unit.EventTrigger.IsTriggerWithdraw != null)
            {
                goto Label_0028;
            }
        Label_0026:
            return 0;
        Label_0028:
            unit.EventTrigger.Count = 1;
            if (unit.SettingNPC.trigger == null)
            {
                goto Label_005F;
            }
            unit.EventTrigger.Count = unit.SettingNPC.trigger.Count;
        Label_005F:
            if (unit.EventTrigger.Trigger != 8)
            {
                goto Label_00BC;
            }
            unit.x = &unit.SettingNPC.pos.x;
            unit.y = &unit.SettingNPC.pos.y;
            unit.Direction = unit.SettingNPC.dir;
        Label_00BC:
            return 1;
        }

        private bool ExecMultiPlayerCommandCore(EBattleCommand type, Unit current, Unit enemy, SkillData skill, ItemData item, int gx, int gy, bool unitLockTarget)
        {
            bool flag;
            if (type != 1)
            {
                goto Label_004A;
            }
            if (this.CheckSkillScopeMultiPlay(current, enemy, gx, gy, current.GetAttackSkill()) != null)
            {
                goto Label_0023;
            }
            goto Label_0045;
        Label_0023:
            if (this.UseSkill(current, gx, gy, current.GetAttackSkill(), unitLockTarget, 0, 0, 0) != null)
            {
                goto Label_0043;
            }
            goto Label_0045;
        Label_0043:
            return 1;
        Label_0045:
            goto Label_00C7;
        Label_004A:
            if (type != 2)
            {
                goto Label_00C0;
            }
            if (this.CheckSkillScopeMultiPlay(current, enemy, gx, gy, skill) != null)
            {
                goto Label_0069;
            }
            goto Label_00BB;
        Label_0069:
            flag = 0;
            if (skill.EffectType != 0x16)
            {
                goto Label_009B;
            }
            flag = this.UseSkill(current, gx, gy, skill, unitLockTarget, enemy.x, enemy.y, 0);
            goto Label_00AE;
        Label_009B:
            flag = this.UseSkill(current, gx, gy, skill, unitLockTarget, 0, 0, 0);
        Label_00AE:
            if (flag != null)
            {
                goto Label_00B9;
            }
            goto Label_00BB;
        Label_00B9:
            return 1;
        Label_00BB:
            goto Label_00C7;
        Label_00C0:
            if (type != 3)
            {
                goto Label_00C7;
            }
        Label_00C7:
            return 0;
        }

        public bool ExecuteEventTriggerOnGrid(Unit self, EEventTrigger trigger)
        {
            Grid grid;
            Unit unit;
            if (this.CheckGridEventTrigger(self, trigger) != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            grid = this.GetUnitGridPosition(self);
            unit = this.FindGimmickAtGrid(grid, 0, null);
            return this.GridEventStart(self, unit, trigger, null);
        }

        private void ExecuteFirstReactionSkill(Unit attacker, List<Unit> targets, SkillData skill, int tx, int ty, List<LogSkill> results)
        {
            Grid grid;
            Unit unit;
            int num;
            Unit unit2;
            if (skill == null)
            {
                goto Label_0011;
            }
            if (skill.IsDamagedSkill() != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            if (attacker == null)
            {
                goto Label_0034;
            }
            if (attacker.IsDeadCondition() != null)
            {
                goto Label_0034;
            }
            if (targets == null)
            {
                goto Label_0034;
            }
            if (targets.Count != null)
            {
                goto Label_0035;
            }
        Label_0034:
            return;
        Label_0035:
            grid = this.CurrentMap[tx, ty];
            if (grid != null)
            {
                goto Label_004C;
            }
            return;
        Label_004C:
            unit = this.FindUnitAtGrid(grid);
            if (unit != null)
            {
                goto Label_0077;
            }
            unit = this.FindGimmickAtGrid(grid, 0, null);
            if (unit == null)
            {
                goto Label_0077;
            }
            if (unit.IsBreakObj != null)
            {
                goto Label_0077;
            }
            unit = null;
        Label_0077:
            num = 0;
            goto Label_00B9;
        Label_007E:
            unit2 = targets[num];
            if (attacker != unit2)
            {
                goto Label_0092;
            }
            goto Label_00B5;
        Label_0092:
            if (unit2.IsEnableReactionCondition() != null)
            {
                goto Label_00A2;
            }
            goto Label_00B5;
        Label_00A2:
            this.InternalReactionSkill(7, attacker, unit2, unit, skill, 0, 0, results, unit2 == unit);
        Label_00B5:
            num += 1;
        Label_00B9:
            if (num < targets.Count)
            {
                goto Label_007E;
            }
            return;
        }

        private unsafe void ExecuteReactionSkill(LogSkill log, List<LogSkill> results)
        {
            SkillData data;
            Unit unit;
            Grid grid;
            Unit unit2;
            int num;
            Unit unit3;
            int num2;
            if (log == null)
            {
                goto Label_0016;
            }
            if (log.targets.Count != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            data = log.skill;
            if (data.IsDamagedSkill() != null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            unit = log.self;
            grid = this.CurrentMap[&log.pos.x, &log.pos.y];
            if (grid != null)
            {
                goto Label_005A;
            }
            return;
        Label_005A:
            unit2 = this.FindUnitAtGrid(grid);
            if (unit2 != null)
            {
                goto Label_0085;
            }
            unit2 = this.FindGimmickAtGrid(grid, 0, null);
            if (unit2 == null)
            {
                goto Label_0085;
            }
            if (unit2.IsBreakObj != null)
            {
                goto Label_0085;
            }
            unit2 = null;
        Label_0085:
            if (unit.IsDead == null)
            {
                goto Label_0091;
            }
            return;
        Label_0091:
            num = 0;
            goto Label_0143;
        Label_0099:
            if (log.targets[num].guard == null)
            {
                goto Label_00B5;
            }
            goto Label_013D;
        Label_00B5:
            if (log.targets[num].IsAvoid() == null)
            {
                goto Label_00D1;
            }
            goto Label_013D;
        Label_00D1:
            unit3 = log.targets[num].target;
            if (unit != unit3)
            {
                goto Label_00F2;
            }
            goto Label_013D;
        Label_00F2:
            if (unit3.IsEnableReactionCondition() != null)
            {
                goto Label_0103;
            }
            goto Label_013D;
        Label_0103:
            num2 = log.targets[num].GetTotalHpDamage();
            this.InternalReactionSkill(6, unit, unit3, unit2, data, num2, log.targets[num].is_force_reaction, results, unit3 == unit2);
        Label_013D:
            num += 1;
        Label_0143:
            if (num < log.targets.Count)
            {
                goto Label_0099;
            }
            return;
        }

        private unsafe void ExecuteSkill(ESkillTiming timing, LogSkill log, SkillData skill)
        {
            LogSkill.Target local2;
            LogSkill.Target local1;
            Unit unit;
            bool flag;
            LogSkill.Target target;
            List<LogSkill.Target>.Enumerator enumerator;
            int num;
            Unit unit2;
            List<Unit> list;
            List<bool> list2;
            int num2;
            BattleLog log2;
            Unit unit3;
            int num3;
            int num4;
            int num5;
            int num6;
            Unit unit4;
            Grid grid;
            Unit unit5;
            bool flag2;
            Grid grid2;
            Grid grid3;
            int num7;
            int num8;
            Unit unit6;
            bool flag3;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            List<LogSkill.Target> list3;
            LogSkill.Target target2;
            List<LogSkill.Target>.Enumerator enumerator2;
            int num15;
            int num16;
            LogSkill.Target target3;
            List<LogSkill.Target>.Enumerator enumerator3;
            int num17;
            int num18;
            Unit unit7;
            JobData data;
            List<ArtifactData> list4;
            ArtifactData data2;
            int num19;
            ArtifactData data3;
            bool flag4;
            int num20;
            EnchantParam param;
            EnchantParam param2;
            int num21;
            bool flag5;
            int num22;
            int num23;
            int num24;
            JobData data4;
            List<ArtifactData> list5;
            ArtifactData data5;
            int num25;
            ArtifactData data6;
            int num26;
            Unit unit8;
            int num27;
            int num28;
            int num29;
            Unit unit9;
            bool flag6;
            LogFall fall;
            LogFall fall2;
            Grid grid4;
            int num30;
            int num31;
            int num32;
            LogSkill.Target target4;
            Unit unit10;
            List<Unit>.Enumerator enumerator4;
            bool flag7;
            Unit unit11;
            List<Unit>.Enumerator enumerator5;
            SkillEffectTypes types;
            if (timing == skill.Timing)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            unit = log.self;
            if (this.CheckSkillCondition(unit, skill) != null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            flag = unit.IsDying();
            if (log.targets == null)
            {
                goto Label_007B;
            }
            enumerator = log.targets.GetEnumerator();
        Label_0040:
            try
            {
                goto Label_005E;
            Label_0045:
                target = &enumerator.Current;
                target.IsOldDying = target.target.IsDying();
            Label_005E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0045;
                }
                goto Label_007B;
            }
            finally
            {
            Label_006F:
                ((List<LogSkill.Target>.Enumerator) enumerator).Dispose();
            }
        Label_007B:
            this.AbsorbAndGiveClear();
            if (this.IsBattleFlag(6) != null)
            {
                goto Label_0111;
            }
            if (skill.IsPrevApply() == null)
            {
                goto Label_0111;
            }
            num = 0;
            goto Label_00D9;
        Label_00A0:
            unit2 = log.targets[num].target;
            this.BuffSkill(timing, unit, unit2, skill, 0, log, 0, 0, null);
            unit2.CalcCurrentStatus(0, this.IsBattleFlag(5));
            num += 1;
        Label_00D9:
            if (num < log.targets.Count)
            {
                goto Label_00A0;
            }
            this.BuffSkill(timing, unit, unit, skill, 0, log, 1, 0, null);
            unit.CalcCurrentStatus(0, this.IsBattleFlag(5));
            this.AbsorbAndGiveApply(unit, skill, log);
        Label_0111:
            list = new List<Unit>(log.targets.Count);
            list2 = new List<bool>();
            num2 = 0;
            goto Label_0156;
        Label_0132:
            list2.Add(log.targets[num2].target.IsJump);
            num2 += 1;
        Label_0156:
            if (num2 < log.targets.Count)
            {
                goto Label_0132;
            }
            if (skill.IsChangeWeatherSkill() == null)
            {
                goto Label_01DF;
            }
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_01DE;
            }
            if (skill.WeatherRate <= 0)
            {
                goto Label_01DE;
            }
            if (string.IsNullOrEmpty(skill.WeatherId) != null)
            {
                goto Label_01DE;
            }
        Label_019B:
            log2 = this.mLogs.Last;
            if (log2 != null)
            {
                goto Label_01B4;
            }
            goto Label_01D5;
        Label_01B4:
            this.mLogs.RemoveLogLast();
            if ((log2 as LogSkill) == null)
            {
                goto Label_019B;
            }
            goto Label_01D5;
            goto Label_019B;
        Label_01D5:
            this.ChangeWeatherForSkill(unit, skill);
        Label_01DE:
            return;
        Label_01DF:
            if (skill.IsTransformSkill() == null)
            {
                goto Label_02D1;
            }
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0F53;
            }
            if (log.targets == null)
            {
                goto Label_0F53;
            }
            if (log.targets.Count <= 0)
            {
                goto Label_0F53;
            }
            unit3 = log.targets[0].target;
            unit3.x = unit.x;
            unit3.y = unit.y;
            unit3.Direction = unit.Direction;
            if (unit.CurrentStatus.param.hp == null)
            {
                goto Label_027C;
            }
            unit.KeepHp = unit.CurrentStatus.param.hp;
        Label_027C:
            if (skill.EffectType != 0x1d)
            {
                goto Label_02B0;
            }
            if (unit.KeepHp == null)
            {
                goto Label_02B0;
            }
            unit3.CurrentStatus.param.hp = unit.KeepHp;
        Label_02B0:
            unit.ForceDead();
            unit3.SetUnitFlag(1, 1);
            unit3.SetUnitFlag(0x400000, 1);
            goto Label_0F53;
        Label_02D1:
            if (skill.IsTrickSkill() == null)
            {
                goto Label_0313;
            }
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0F53;
            }
            num3 = &log.pos.x;
            num4 = &log.pos.y;
            this.TrickCreateForSkill(unit, num3, num4, skill);
            goto Label_0F53;
        Label_0313:
            if (skill.IsSetBreakObjSkill() == null)
            {
                goto Label_0367;
            }
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0F53;
            }
            num5 = &log.pos.x;
            num6 = &log.pos.y;
            this.BreakObjCreate(skill.SkillParam.BreakObjId, num5, num6, unit, log, unit.OwnerPlayerIndex);
            goto Label_0F53;
        Label_0367:
            if (skill.IsTargetGridNoUnit == null)
            {
                goto Label_04C9;
            }
            if (skill.TeleportType == null)
            {
                goto Label_0389;
            }
            if (skill.TeleportType != 1)
            {
                goto Label_04C9;
            }
        Label_0389:
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0F53;
            }
            types = skill.EffectType;
            if (types == 0x11)
            {
                goto Label_03B4;
            }
            if (types == 0x16)
            {
                goto Label_03DB;
            }
            goto Label_043C;
        Label_03B4:
            unit.x = &log.pos.x;
            unit.y = &log.pos.y;
            goto Label_043C;
        Label_03DB:
            if (log.targets == null)
            {
                goto Label_043C;
            }
            if (log.targets.Count <= 0)
            {
                goto Label_043C;
            }
            unit4 = log.targets[0].target;
            unit4.x = &log.pos.x;
            unit4.y = &log.pos.y;
            list.Add(unit4);
        Label_043C:
            if (skill.TeleportType != 1)
            {
                goto Label_0F53;
            }
            unit.x = &log.pos.x;
            unit.y = &log.pos.y;
            grid = this.GetCorrectDuplicatePosition(unit);
            if (grid == null)
            {
                goto Label_0494;
            }
            unit.x = grid.x;
            unit.y = grid.y;
        Label_0494:
            unit.startX = unit.x;
            unit.startY = unit.y;
            log.TeleportGrid = this.GetUnitGridPosition(unit.x, unit.y);
            goto Label_0F53;
        Label_04C9:
            if (skill.TeleportType == 2)
            {
                goto Label_04E1;
            }
            if (skill.TeleportType != 3)
            {
                goto Label_065F;
            }
        Label_04E1:
            if (skill.IsTargetTeleport == null)
            {
                goto Label_05CB;
            }
            if (log.targets == null)
            {
                goto Label_065F;
            }
            if (log.targets.Count <= 0)
            {
                goto Label_065F;
            }
            unit5 = log.targets[0].target;
            flag2 = 0;
            grid2 = this.GetTeleportGrid(unit, unit.x, unit.y, unit5, skill, &flag2);
            if (grid2 == null)
            {
                goto Label_065F;
            }
            if (flag2 == null)
            {
                goto Label_065F;
            }
            unit.x = grid2.x;
            unit.y = grid2.y;
            grid2 = this.GetCorrectDuplicatePosition(unit);
            if (grid2 == null)
            {
                goto Label_058A;
            }
            unit.x = grid2.x;
            unit.y = grid2.y;
        Label_058A:
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_05AE;
            }
            unit.startX = unit.x;
            unit.startY = unit.y;
        Label_05AE:
            log.TeleportGrid = this.GetUnitGridPosition(unit.x, unit.y);
            goto Label_065F;
        Label_05CB:
            unit.x = &log.pos.x;
            unit.y = &log.pos.y;
            if (skill.TeleportType == 2)
            {
                goto Label_0623;
            }
            grid3 = this.GetCorrectDuplicatePosition(unit);
            if (grid3 == null)
            {
                goto Label_0623;
            }
            unit.x = grid3.x;
            unit.y = grid3.y;
        Label_0623:
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0647;
            }
            unit.startX = unit.x;
            unit.startY = unit.y;
        Label_0647:
            log.TeleportGrid = this.GetUnitGridPosition(unit.x, unit.y);
        Label_065F:
            num7 = Math.Max(skill.SkillParam.ComboNum, 1);
            num8 = 0;
            goto Label_094B;
        Label_067F:
            unit6 = log.targets[num8].target;
            flag3 = 1;
            types = skill.EffectType;
            switch ((types - 9))
            {
                case 0:
                    goto Label_06FB;

                case 1:
                    goto Label_06D8;

                case 2:
                    goto Label_06D8;

                case 3:
                    goto Label_06D8;

                case 4:
                    goto Label_06D8;

                case 5:
                    goto Label_0747;

                case 6:
                    goto Label_07CC;

                case 7:
                    goto Label_085B;

                case 8:
                    goto Label_06D8;

                case 9:
                    goto Label_0875;

                case 10:
                    goto Label_0737;

                case 11:
                    goto Label_06FB;
            }
        Label_06D8:
            switch ((types - 2))
            {
                case 0:
                    goto Label_06FB;

                case 1:
                    goto Label_06ED;

                case 2:
                    goto Label_0737;
            }
        Label_06ED:
            if (types == 0x1c)
            {
                goto Label_06FB;
            }
            goto Label_090D;
        Label_06FB:
            num9 = 0;
            goto Label_0729;
        Label_0703:
            if (this.IsBattleFlag(6) != null)
            {
                goto Label_0718;
            }
            unit6.CalcCurrentStatus(0, 0);
        Label_0718:
            this.DamageSkill(unit, unit6, skill, log);
            num9 += 1;
        Label_0729:
            if (num9 < num7)
            {
                goto Label_0703;
            }
            goto Label_0912;
        Label_0737:
            this.HealSkill(unit, unit6, skill, log);
            goto Label_0912;
        Label_0747:
            num10 = this.GetSkillEffectValue(unit, unit6, skill, log);
            num11 = SkillParam.CalcSkillEffectValue(skill.EffectCalcType, num10, unit.Gems) + skill.Cost;
            num11 = Math.Min(num11, unit.Gems);
            log.Hit(unit, unit6, 0, 0, 0, 0, 0, num11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            log.ToSelfSkillEffect(0, num11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            this.SubGems(unit, num11);
            this.AddGems(unit6, num11);
            goto Label_0912;
        Label_07CC:
            num12 = this.GetSkillEffectValue(unit, unit6, skill, log);
            if (skill.EffectCalcType != 1)
            {
                goto Label_0803;
            }
            num12 = (unit6.MaximumStatus.param.mp * num12) / 100;
        Label_0803:
            if (num12 >= 0)
            {
                goto Label_0830;
            }
            log.Hit(unit, unit6, 0, Math.Abs(num12), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            goto Label_084B;
        Label_0830:
            log.Hit(unit, unit6, 0, 0, 0, 0, 0, num12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        Label_084B:
            this.AddGems(unit6, num12);
            goto Label_0912;
        Label_085B:
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0912;
            }
            unit.SetGuardTarget(unit6, 3);
            goto Label_0912;
        Label_0875:
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0912;
            }
            num13 = unit6.x;
            num14 = unit6.y;
            unit6.x = unit.x;
            unit6.y = unit.y;
            unit.x = num13;
            unit.y = num14;
            unit.startX = num13;
            unit.startY = num14;
            unit6.startX = unit6.x;
            unit6.startY = unit6.y;
            if (unit6.IsJump != null)
            {
                goto Label_0912;
            }
            this.TrickActionEndEffect(unit6, 1);
            this.ExecuteEventTriggerOnGrid(unit6, 0);
            goto Label_0912;
        Label_090D:;
        Label_0912:
            if (flag3 == null)
            {
                goto Label_0945;
            }
            if (log.targets[num8].IsAvoid() != null)
            {
                goto Label_0945;
            }
            if (unit6.CheckDamageActionStart() == null)
            {
                goto Label_0945;
            }
            this.NotifyDamagedActionStart(unit, unit6);
        Label_0945:
            num8 += 1;
        Label_094B:
            if (num8 < log.targets.Count)
            {
                goto Label_067F;
            }
            if (this.isKnockBack(skill) == null)
            {
                goto Label_0A7E;
            }
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_0A7E;
            }
            list3 = new List<LogSkill.Target>(log.targets.Count);
            enumerator2 = log.targets.GetEnumerator();
        Label_0993:
            try
            {
                goto Label_09E0;
            Label_0998:
                target2 = &enumerator2.Current;
                num15 = target2.GetTotalHpDamage();
                num16 = target2.GetTotalMpDamage();
                if (num15 > 0)
                {
                    goto Label_09C3;
                }
                if (num16 <= 0)
                {
                    goto Label_09E0;
                }
            Label_09C3:
                if (this.checkKnockBack(unit, target2.target, skill) == null)
                {
                    goto Label_09E0;
                }
                list3.Add(target2);
            Label_09E0:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0998;
                }
                goto Label_09FE;
            }
            finally
            {
            Label_09F1:
                ((List<LogSkill.Target>.Enumerator) enumerator2).Dispose();
            }
        Label_09FE:
            this.procKnockBack(unit, skill, &log.pos.x, &log.pos.y, list3);
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0A7E;
            }
            enumerator3 = list3.GetEnumerator();
        Label_0A33:
            try
            {
                goto Label_0A60;
            Label_0A38:
                target3 = &enumerator3.Current;
                if (target3.KnockBackGrid != null)
                {
                    goto Label_0A52;
                }
                goto Label_0A60;
            Label_0A52:
                list.Add(target3.target);
            Label_0A60:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0A38;
                }
                goto Label_0A7E;
            }
            finally
            {
            Label_0A71:
                ((List<LogSkill.Target>.Enumerator) enumerator3).Dispose();
            }
        Label_0A7E:
            num17 = log.GetGainJewel();
            if (num17 <= 0)
            {
                goto Label_0A98;
            }
            this.AddGems(unit, num17);
        Label_0A98:
            num18 = 0;
            goto Label_0F41;
        Label_0AA0:
            unit7 = log.targets[num18].target;
            if (unit7.IsDead == null)
            {
                goto Label_0AC5;
            }
            goto Label_0F3B;
        Label_0AC5:
            this.CondSkillSetRateLog(timing, unit, unit7, skill, log);
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_0AF8;
            }
            if (log.targets[num18].IsAvoid() == null)
            {
                goto Label_0AF8;
            }
            goto Label_0F3B;
        Label_0AF8:
            types = skill.EffectType;
            switch ((types - 2))
            {
                case 0:
                    goto Label_0B35;

                case 1:
                    goto Label_0B15;

                case 2:
                    goto Label_0B73;
            }
        Label_0B15:
            if (types == 0x13)
            {
                goto Label_0B73;
            }
            if (types == 20)
            {
                goto Label_0B35;
            }
            if (types == 0x1c)
            {
                goto Label_0B35;
            }
            goto Label_0B99;
        Label_0B35:
            if (log.targets[num18].GetTotalHpDamage() > 0)
            {
                goto Label_0B65;
            }
            if (log.targets[num18].shieldDamage <= 0)
            {
                goto Label_0B9E;
            }
        Label_0B65:
            this.DamageCureCondition(unit7, log);
            goto Label_0B9E;
        Label_0B73:
            if (log.targets[num18].GetTotalHpHeal() <= 0)
            {
                goto Label_0B9E;
            }
            this.HealCureCondition(unit7, log);
            goto Label_0B9E;
        Label_0B99:;
        Label_0B9E:
            if (skill.IsPrevApply() != null)
            {
                goto Label_0BB9;
            }
            this.BuffSkill(timing, unit, unit7, skill, 0, log, 0, 0, null);
        Label_0BB9:
            this.CondSkill(timing, unit, unit7, skill, 0, log, 0, 0);
            if (skill.IsNormalAttack() == null)
            {
                goto Label_0D06;
            }
            data = unit.Job;
            if (data == null)
            {
                goto Label_0D06;
            }
            if (data.ArtifactDatas != null)
            {
                goto Label_0BFF;
            }
            if (string.IsNullOrEmpty(data.SelectedSkin) != null)
            {
                goto Label_0D06;
            }
        Label_0BFF:
            list4 = new List<ArtifactData>();
            if (data.ArtifactDatas == null)
            {
                goto Label_0C2F;
            }
            if (((int) data.ArtifactDatas.Length) < 1)
            {
                goto Label_0C2F;
            }
            list4.AddRange(data.ArtifactDatas);
        Label_0C2F:
            if (string.IsNullOrEmpty(data.SelectedSkin) != null)
            {
                goto Label_0C59;
            }
            data2 = data.GetSelectedSkinData();
            if (data2 == null)
            {
                goto Label_0C59;
            }
            list4.Add(data2);
        Label_0C59:
            num19 = 0;
            goto Label_0CF8;
        Label_0C61:
            data3 = list4[num19];
            if (data3 != null)
            {
                goto Label_0C78;
            }
            goto Label_0CF2;
        Label_0C78:
            if (data3.ArtifactParam != null)
            {
                goto Label_0C89;
            }
            goto Label_0CF2;
        Label_0C89:
            if (data3.ArtifactParam.type == 1)
            {
                goto Label_0CA0;
            }
            goto Label_0CF2;
        Label_0CA0:
            if (data3.BattleEffectSkill != null)
            {
                goto Label_0CB1;
            }
            goto Label_0CF2;
        Label_0CB1:
            if (data3.BattleEffectSkill.SkillParam != null)
            {
                goto Label_0CC7;
            }
            goto Label_0CF2;
        Label_0CC7:
            this.BuffSkill(timing, unit, unit7, data3.BattleEffectSkill, 0, log, 0, 0, null);
            this.CondSkill(timing, unit, unit7, data3.BattleEffectSkill, 0, log, 0, 0);
        Label_0CF2:
            num19 += 1;
        Label_0CF8:
            if (num19 < list4.Count)
            {
                goto Label_0C61;
            }
        Label_0D06:
            this.StealSkill(unit, unit7, skill, log);
            this.ShieldSkill(unit7, skill);
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0F3B;
            }
            flag4 = 0;
            if (num18 >= list2.Count)
            {
                goto Label_0D42;
            }
            flag4 = list2[num18];
        Label_0D42:
            if (flag4 == null)
            {
                goto Label_0D8F;
            }
            if (skill.SkillParam.IsJumpBreak() != null)
            {
                goto Label_0D65;
            }
            if (unit7.IsJumpBreakCondition() == null)
            {
                goto Label_0DCB;
            }
        Label_0D65:
            unit7.CancelCastSkill();
            local1 = log.targets[num18];
            local1.hitType |= 0x100;
            goto Label_0DCB;
        Label_0D8F:
            if (skill.IsCastBreak() == null)
            {
                goto Label_0DCB;
            }
            if (unit7.CastSkill == null)
            {
                goto Label_0DCB;
            }
            unit7.CancelCastSkill();
            local2 = log.targets[num18];
            local2.hitType |= 0x100;
        Label_0DCB:
            if (skill.ControlChargeTimeRate == null)
            {
                goto Label_0F3B;
            }
            num20 = 100;
            param = unit.CurrentStatus.enchant_assist;
            param2 = unit7.CurrentStatus.enchant_resist;
            if (skill.ControlChargeTimeValue >= 0)
            {
                goto Label_0E4C;
            }
            if (unit7.IsDisableUnitCondition(0x8000000L) == null)
            {
                goto Label_0E25;
            }
            num20 = 0;
            goto Label_0E47;
        Label_0E25:
            num20 += param[0x1b] - param2[0x1b];
        Label_0E47:
            goto Label_0E99;
        Label_0E4C:
            if (skill.ControlChargeTimeValue <= 0)
            {
                goto Label_0E99;
            }
            if (unit7.IsDisableUnitCondition(0x10000000L) == null)
            {
                goto Label_0E77;
            }
            num20 = 0;
            goto Label_0E99;
        Label_0E77:
            num20 += param[0x1c] - param2[0x1c];
        Label_0E99:
            num21 = (skill.ControlChargeTimeRate * num20) / 100;
            if (num21 <= 0)
            {
                goto Label_0F3B;
            }
            flag5 = 1;
            if (num21 >= 100)
            {
                goto Label_0ED7;
            }
            num22 = this.GetRandom() % 100;
            if (num22 <= num21)
            {
                goto Label_0ED7;
            }
            flag5 = 0;
        Label_0ED7:
            if (flag5 == null)
            {
                goto Label_0F3B;
            }
            num23 = unit7.ChargeTime;
            unit7.ChargeTime = SkillParam.CalcSkillEffectValue(skill.ControlChargeTimeCalcType, skill.ControlChargeTimeValue, unit7.ChargeTime);
            log.targets[num18].ChangeValueCT = unit7.ChargeTime - num23;
        Label_0F3B:
            num18 += 1;
        Label_0F41:
            if (num18 < log.targets.Count)
            {
                goto Label_0AA0;
            }
        Label_0F53:
            num24 = this.GetHpCost(unit, skill);
            if (num24 <= 0)
            {
                goto Label_0F82;
            }
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0F7A;
            }
            unit.Damage(num24, 0);
        Label_0F7A:
            log.hp_cost = num24;
        Label_0F82:
            if (unit.IsDead != null)
            {
                goto Label_10F1;
            }
            if (skill.IsPrevApply() != null)
            {
                goto Label_0FA7;
            }
            this.BuffSkill(timing, unit, unit, skill, 0, log, 1, 0, null);
        Label_0FA7:
            this.CondSkill(timing, unit, unit, skill, 0, log, 1, 0);
            if (skill.IsNormalAttack() == null)
            {
                goto Label_10F1;
            }
            data4 = unit.Job;
            if (data4 == null)
            {
                goto Label_10F1;
            }
            if (data4.ArtifactDatas != null)
            {
                goto Label_0FEC;
            }
            if (string.IsNullOrEmpty(data4.SelectedSkin) != null)
            {
                goto Label_10F1;
            }
        Label_0FEC:
            list5 = new List<ArtifactData>();
            if (data4.ArtifactDatas == null)
            {
                goto Label_101C;
            }
            if (((int) data4.ArtifactDatas.Length) < 1)
            {
                goto Label_101C;
            }
            list5.AddRange(data4.ArtifactDatas);
        Label_101C:
            if (string.IsNullOrEmpty(data4.SelectedSkin) != null)
            {
                goto Label_1046;
            }
            data5 = data4.GetSelectedSkinData();
            if (data5 == null)
            {
                goto Label_1046;
            }
            list5.Add(data5);
        Label_1046:
            num25 = 0;
            goto Label_10E3;
        Label_104E:
            data6 = list5[num25];
            if (data6 != null)
            {
                goto Label_1065;
            }
            goto Label_10DD;
        Label_1065:
            if (data6.ArtifactParam != null)
            {
                goto Label_1076;
            }
            goto Label_10DD;
        Label_1076:
            if (data6.ArtifactParam.type == 1)
            {
                goto Label_108D;
            }
            goto Label_10DD;
        Label_108D:
            if (data6.BattleEffectSkill != null)
            {
                goto Label_109E;
            }
            goto Label_10DD;
        Label_109E:
            if (data6.BattleEffectSkill.SkillParam != null)
            {
                goto Label_10B4;
            }
            goto Label_10DD;
        Label_10B4:
            this.BuffSkill(timing, unit, unit, data6.BattleEffectSkill, 0, log, 1, 0, null);
            this.CondSkill(timing, unit, unit, data6.BattleEffectSkill, 0, log, 1, 0);
        Label_10DD:
            num25 += 1;
        Label_10E3:
            if (num25 < list5.Count)
            {
                goto Label_104E;
            }
        Label_10F1:
            if (this.IsBattleFlag(6) != null)
            {
                goto Label_1179;
            }
            if (this.IsBattleFlag(7) != null)
            {
                goto Label_1165;
            }
            num26 = 0;
            goto Label_1140;
        Label_1111:
            unit8 = log.targets[num26].target;
            if (unit8.RemoveBuffPrevApply() == null)
            {
                goto Label_113A;
            }
            unit8.CalcCurrentStatus(0, 0);
        Label_113A:
            num26 += 1;
        Label_1140:
            if (num26 < log.targets.Count)
            {
                goto Label_1111;
            }
            if (unit.RemoveBuffPrevApply() == null)
            {
                goto Label_1165;
            }
            unit.CalcCurrentStatus(0, 0);
        Label_1165:
            if (skill.IsPrevApply() != null)
            {
                goto Label_1179;
            }
            this.AbsorbAndGiveApply(unit, skill, log);
        Label_1179:
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_164B;
            }
            num27 = 0;
            goto Label_11AE;
        Label_118D:
            this.AbilityChange(unit, log.targets[num27].target, skill);
            num27 += 1;
        Label_11AE:
            if (num27 < log.targets.Count)
            {
                goto Label_118D;
            }
            num28 = 0;
            goto Label_11EA;
        Label_11C8:
            this.GridEventStart(unit, log.targets[num28].target, 4, null);
            num28 += 1;
        Label_11EA:
            if (num28 < log.targets.Count)
            {
                goto Label_11C8;
            }
            num29 = 0;
            goto Label_1304;
        Label_1204:
            unit9 = log.targets[num29].target;
            flag6 = 0;
            if (num29 >= list2.Count)
            {
                goto Label_1234;
            }
            flag6 = list2[num29];
        Label_1234:
            if (unit9.IsDead == null)
            {
                goto Label_1269;
            }
            if (flag6 == null)
            {
                goto Label_1259;
            }
            this.Log<LogFall>().Add(unit9, null);
        Label_1259:
            this.Dead(unit, unit9, 0, 0);
            goto Label_12FE;
        Label_1269:
            if (flag6 == null)
            {
                goto Label_12FE;
            }
            if (unit9.IsJump != null)
            {
                goto Label_12FE;
            }
            fall2 = this.Logs.Last as LogFall;
            if (fall2 == null)
            {
                goto Label_12A1;
            }
            if (fall2.mIsPlayDamageMotion != null)
            {
                goto Label_12A9;
            }
        Label_12A1:
            fall2 = this.Log<LogFall>();
        Label_12A9:
            grid4 = this.GetCorrectDuplicatePosition(unit9);
            fall2.Add(unit9, grid4);
            if (unit9.IsJumpBreakNoMotionCondition() != null)
            {
                goto Label_12D2;
            }
            fall2.mIsPlayDamageMotion = 1;
        Label_12D2:
            if (grid4 == null)
            {
                goto Label_12FE;
            }
            unit9.x = grid4.x;
            unit9.y = grid4.y;
            list.Add(unit9);
        Label_12FE:
            num29 += 1;
        Label_1304:
            if (num29 < log.targets.Count)
            {
                goto Label_1204;
            }
            if (unit.IsDead == null)
            {
                goto Label_132B;
            }
            this.Dead(unit, unit, 0, 0);
        Label_132B:
            if (unit.CastSkill == null)
            {
                goto Label_1395;
            }
            if (unit.CastSkill != skill)
            {
                goto Label_1395;
            }
            if (unit.CastSkill.CastType != 2)
            {
                goto Label_1395;
            }
            log.landing = this.GetCorrectDuplicatePosition(unit);
            if (log.landing == null)
            {
                goto Label_1395;
            }
            unit.x = log.landing.x;
            unit.y = log.landing.y;
            list.Add(unit);
        Label_1395:
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_14DB;
            }
            unit.UpdateBuffEffectTurnCount(3, unit);
            unit.UpdateCondEffectTurnCount(3, unit);
            num30 = 0;
            goto Label_14C9;
        Label_13B8:
            log.targets[num30].target.UpdateBuffEffectTurnCount(3, unit);
            log.targets[num30].target.UpdateCondEffectTurnCount(3, unit);
            if (log.targets[num30].IsAvoid() != null)
            {
                goto Label_14C3;
            }
            log.targets[num30].target.UpdateBuffEffectTurnCount(4, log.targets[num30].target);
            log.targets[num30].target.UpdateCondEffectTurnCount(4, log.targets[num30].target);
            if ((log.targets[num30].hitType & 0x20) == null)
            {
                goto Label_14C3;
            }
            log.targets[num30].target.UpdateBuffEffectTurnCount(5, log.targets[num30].target);
            log.targets[num30].target.UpdateCondEffectTurnCount(5, log.targets[num30].target);
        Label_14C3:
            num30 += 1;
        Label_14C9:
            if (num30 < log.targets.Count)
            {
                goto Label_13B8;
            }
        Label_14DB:
            num31 = 0;
            goto Label_151B;
        Label_14E3:
            log.targets[num31].target.UpdateBuffEffectTurnCount(2, unit);
            log.targets[num31].target.UpdateCondEffectTurnCount(2, unit);
            num31 += 1;
        Label_151B:
            if (num31 < log.targets.Count)
            {
                goto Label_14E3;
            }
            unit.UpdateBuffEffectTurnCount(2, unit);
            unit.UpdateCondEffectTurnCount(2, unit);
            num32 = 0;
            goto Label_15AF;
        Label_1545:
            target4 = log.targets[num32];
            target4.target.UpdateBuffEffects();
            target4.target.UpdateCondEffects();
            target4.target.CalcCurrentStatus(0, 0);
            if (target4.IsOldDying != null)
            {
                goto Label_15A9;
            }
            if (target4.target.IsDying() == null)
            {
                goto Label_15A9;
            }
            target4.target.SetUnitFlag(0x800000, 1);
        Label_15A9:
            num32 += 1;
        Label_15AF:
            if (num32 < log.targets.Count)
            {
                goto Label_1545;
            }
            unit.UpdateBuffEffects();
            unit.UpdateCondEffects();
            unit.CalcCurrentStatus(0, 0);
            if (flag != null)
            {
                goto Label_15F2;
            }
            if (unit.IsDying() == null)
            {
                goto Label_15F2;
            }
            unit.SetUnitFlag(0x800000, 1);
        Label_15F2:
            this.UpdateEntryTriggers(4, unit, skill.SkillParam);
            enumerator4 = this.mUnits.GetEnumerator();
        Label_160D:
            try
            {
                goto Label_162D;
            Label_1612:
                unit10 = &enumerator4.Current;
                this.GridEventStart(unit, unit10, 10, skill.SkillParam);
            Label_162D:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_1612;
                }
                goto Label_164B;
            }
            finally
            {
            Label_163E:
                ((List<Unit>.Enumerator) enumerator4).Dispose();
            }
        Label_164B:
            flag7 = 0;
            enumerator5 = list.GetEnumerator();
        Label_1657:
            try
            {
                goto Label_16BC;
            Label_165C:
                unit11 = &enumerator5.Current;
                if (unit11.IsDead == null)
                {
                    goto Label_1676;
                }
                goto Label_16BC;
            Label_1676:
                if (unit11.IsJump == null)
                {
                    goto Label_16A6;
                }
                if (unit11 != unit)
                {
                    goto Label_16BC;
                }
                unit11.PushCastSkill();
                this.TrickActionEndEffect(unit11, 1);
                unit11.PopCastSkill();
                goto Label_16BC;
            Label_16A6:
                this.TrickActionEndEffect(unit11, 0);
                this.ExecuteEventTriggerOnGrid(unit11, 0);
                flag7 = 1;
            Label_16BC:
                if (&enumerator5.MoveNext() != null)
                {
                    goto Label_165C;
                }
                goto Label_16DA;
            }
            finally
            {
            Label_16CD:
                ((List<Unit>.Enumerator) enumerator5).Dispose();
            }
        Label_16DA:
            if (flag7 == null)
            {
                goto Label_16F0;
            }
            unit.RefleshMomentBuff(this.Units, 0, -1, -1);
        Label_16F0:
            if (skill.WeatherRate <= 0)
            {
                goto Label_1721;
            }
            if (string.IsNullOrEmpty(skill.WeatherId) != null)
            {
                goto Label_1721;
            }
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_1721;
            }
            this.ChangeWeatherForSkill(unit, skill);
        Label_1721:
            return;
        }

        private void FailCondition(Unit self, Unit target, SkillData skill, SkillEffectTargets skilltarget, CondEffectParam param, ConditionEffectTypes type, ESkillCondition cond, EUnitCondition condition, EffectCheckTargets chk_target, EffectCheckTimings chk_timing, int turn, bool is_passive, bool is_curse, LogSkill logskl, bool is_same_ow)
        {
            LogFailCondition condition2;
            SceneBattle battle;
            TacticsUnitController controller;
            CondAttachment attachment;
            int num;
            CondAttachment attachment2;
            LogSkill.Target target2;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            condition2 = this.Log<LogFailCondition>();
            condition2.self = target;
            condition2.source = self;
            condition2.condition = condition;
            battle = SceneBattle.Instance;
            if ((battle != null) == null)
            {
                goto Label_005D;
            }
            controller = battle.FindUnitController(target);
            if ((controller != null) == null)
            {
                goto Label_005D;
            }
            controller.LockUpdateBadStatus(condition2.condition, 0);
        Label_005D:
            attachment = this.CreateCondAttachment(self, target, skill, skilltarget, param, type, cond, condition, chk_target, chk_timing, turn, is_passive, is_curse);
            if (is_same_ow == null)
            {
                goto Label_00D6;
            }
            num = 0;
            goto Label_00C4;
        Label_008A:
            attachment2 = target.CondAttachments[num];
            if (attachment.IsSame(attachment2, 1) == null)
            {
                goto Label_00BE;
            }
            target.CondAttachments.RemoveAt(num--);
        Label_00BE:
            num += 1;
        Label_00C4:
            if (num < target.CondAttachments.Count)
            {
                goto Label_008A;
            }
        Label_00D6:
            target.ClearCondLinkageBuffBits();
            target.SetCondAttachment(attachment);
            if (logskl == null)
            {
                goto Label_0148;
            }
            if (target.IsUnitCondition(condition) == null)
            {
                goto Label_0148;
            }
            target2 = logskl.FindTarget(target);
            if (target2 == null)
            {
                goto Label_0148;
            }
            target2.failCondition |= condition;
            if (attachment.LinkageBuff == null)
            {
                goto Label_0148;
            }
            target.CondLinkageBuff.CopyTo(target2.buff);
            target.CondLinkageDebuff.CopyTo(target2.debuff);
        Label_0148:
            return;
        }

        protected override void Finalize()
        {
        Label_0000:
            try
            {
                this.Release();
                goto Label_0012;
            }
            finally
            {
            Label_000B:
                base.Finalize();
            }
        Label_0012:
            return;
        }

        public Unit FindGimmickAtGrid(Grid grid, bool is_valid_disable, Unit ignore_unit)
        {
            Unit unit;
            int num;
            if (grid != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            unit = null;
            num = 0;
            goto Label_00B5;
        Label_0011:
            if (this.mUnits[num].IsGimmick != null)
            {
                goto Label_002C;
            }
            goto Label_00B1;
        Label_002C:
            if (ignore_unit == null)
            {
                goto Label_0049;
            }
            if (this.mUnits[num] != ignore_unit)
            {
                goto Label_0049;
            }
            goto Label_00B1;
        Label_0049:
            if (is_valid_disable != null)
            {
                goto Label_006A;
            }
            if (this.mUnits[num].IsDisableGimmick() == null)
            {
                goto Label_006A;
            }
            goto Label_00B1;
        Label_006A:
            if (this.mUnits[num].CheckCollision(grid) == null)
            {
                goto Label_00B1;
            }
            if (this.mUnits[num].IsBreakObj == null)
            {
                goto Label_00A4;
            }
            return this.mUnits[num];
        Label_00A4:
            unit = this.mUnits[num];
        Label_00B1:
            num += 1;
        Label_00B5:
            if (num < this.mUnits.Count)
            {
                goto Label_0011;
            }
            return unit;
        }

        public Unit FindGimmickAtGrid(int x, int y, bool is_valid_disable)
        {
            Grid grid;
            grid = this.CurrentMap[x, y];
            return this.FindGimmickAtGrid(grid, is_valid_disable, null);
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

        private int FindNearGridAndDistance(Unit self, Unit target, out Grid self_grid, out Grid target_grid)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            Grid grid;
            int num4;
            int num5;
            Grid grid2;
            int num6;
            this.DebugAssert((self == null) == 0, "self == null");
            this.DebugAssert((target == null) == 0, "target == null");
            map = this.CurrentMap;
            this.DebugAssert((map == null) == 0, "map == null");
            num = 0xff;
            *(self_grid) = null;
            *(target_grid) = null;
            num2 = 0;
            goto Label_011A;
        Label_0051:
            num3 = 0;
            goto Label_010A;
        Label_0058:
            grid = map[self.x + num2, self.y + num3];
            this.DebugAssert((grid == null) == 0, "start == null");
            num4 = 0;
            goto Label_00F9;
        Label_008B:
            num5 = 0;
            goto Label_00E6;
        Label_0093:
            grid2 = map[target.x + num4, target.y + num5];
            this.DebugAssert((grid2 == null) == 0, "goal == null");
            num6 = this.CalcGridDistance(grid, grid2);
            if (num6 >= num)
            {
                goto Label_00E0;
            }
            num = num6;
            *(self_grid) = grid;
            *(target_grid) = grid2;
        Label_00E0:
            num5 += 1;
        Label_00E6:
            if (num5 < target.SizeY)
            {
                goto Label_0093;
            }
            num4 += 1;
        Label_00F9:
            if (num4 < target.SizeX)
            {
                goto Label_008B;
            }
            num3 += 1;
        Label_010A:
            if (num3 < self.SizeY)
            {
                goto Label_0058;
            }
            num2 += 1;
        Label_011A:
            if (num2 < self.SizeX)
            {
                goto Label_0051;
            }
            return num;
        }

        public Unit FindUnitAtGrid(Grid grid)
        {
            int num;
            num = 0;
            goto Label_004A;
        Label_0007:
            if (this.mUnits[num].IsGimmick == null)
            {
                goto Label_0022;
            }
            goto Label_0046;
        Label_0022:
            if (this.mUnits[num].CheckCollision(grid) == null)
            {
                goto Label_0046;
            }
            return this.mUnits[num];
        Label_0046:
            num += 1;
        Label_004A:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public Unit FindUnitAtGrid(int x, int y)
        {
            Grid grid;
            grid = this.CurrentMap[x, y];
            return this.FindUnitAtGrid(grid);
        }

        public Unit FindUnitAtGridIgnoreSide(Grid grid, EUnitSide ignoreSide)
        {
            int num;
            num = 0;
            goto Label_0066;
        Label_0007:
            if (this.mUnits[num].IsGimmick == null)
            {
                goto Label_0022;
            }
            goto Label_0062;
        Label_0022:
            if (this.mUnits[num].Side == ignoreSide)
            {
                goto Label_003E;
            }
            goto Label_0062;
        Label_003E:
            if (this.mUnits[num].CheckCollision(grid) == null)
            {
                goto Label_0062;
            }
            return this.mUnits[num];
        Label_0062:
            num += 1;
        Label_0066:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public Unit FindUnitByUniqueName(string uniqname)
        {
            int num;
            if (string.IsNullOrEmpty(uniqname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            num = 0;
            goto Label_0046;
        Label_0014:
            if ((uniqname != this.mUnits[num].UniqueName) == null)
            {
                goto Label_0035;
            }
            goto Label_0042;
        Label_0035:
            return this.mUnits[num];
        Label_0042:
            num += 1;
        Label_0046:
            if (num < this.mUnits.Count)
            {
                goto Label_0014;
            }
            return null;
        }

        private unsafe void GainConvertedGold()
        {
            Dictionary<string, int> dictionary;
            int num;
            string str;
            KeyValuePair<string, int> pair;
            Dictionary<string, int>.Enumerator enumerator;
            int num2;
            ItemParam param;
            ItemData data;
            int num3;
            Dictionary<string, int> dictionary2;
            string str2;
            int num4;
            if (this.mRecord.items != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            dictionary = new Dictionary<string, int>();
            num = 0;
            goto Label_0093;
        Label_001E:
            if (this.mRecord.items[num].itemParam != null)
            {
                goto Label_003E;
            }
            goto Label_008F;
        Label_003E:
            str = this.mRecord.items[num].itemParam.iname;
            if (dictionary.ContainsKey(str) == null)
            {
                goto Label_0087;
            }
            num4 = dictionary2[str2];
            (dictionary2 = dictionary)[str2 = str] = num4 + 1;
            goto Label_008F;
        Label_0087:
            dictionary[str] = 1;
        Label_008F:
            num += 1;
        Label_0093:
            if (num < this.mRecord.items.Count)
            {
                goto Label_001E;
            }
            enumerator = dictionary.GetEnumerator();
        Label_00B1:
            try
            {
                goto Label_0155;
            Label_00B6:
                pair = &enumerator.Current;
                num2 = &pair.Value;
                param = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(&pair.Key);
                if (param != null)
                {
                    goto Label_00EB;
                }
                goto Label_0155;
            Label_00EB:
                data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(&pair.Key);
                if (data == null)
                {
                    goto Label_0116;
                }
                num2 += data.Num;
            Label_0116:
                num3 = num2 - param.cap;
                if (num3 > 0)
                {
                    goto Label_012F;
                }
                goto Label_0155;
            Label_012F:
                this.mRecord.gold += num3 * param.sell;
            Label_0155:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00B6;
                }
                goto Label_0173;
            }
            finally
            {
            Label_0166:
                ((Dictionary<string, int>.Enumerator) enumerator).Dispose();
            }
        Label_0173:
            return;
        }

        private void GainFreeVersusItem()
        {
            QuestResult result;
            GameManager manager;
            VersusFirstWinBonusParam param;
            int num;
            VersusStreakWinBonusParam param2;
            int num2;
            VersusStreakWinBonusParam param3;
            if (this.IsMultiVersus != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (GlobalVars.SelectedMultiPlayVersusType == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (this.GetQuestResult() == 1)
            {
                goto Label_0026;
            }
            return;
        Label_0026:
            manager = MonoSingleton<GameManager>.Instance;
            if (manager.IsVSFirstWinRewardRecived != null)
            {
                goto Label_0055;
            }
            param = manager.GetVSFirstWinBonus(GlobalVars.VersusFreeMatchTime);
            if (param == null)
            {
                goto Label_0055;
            }
            this.SetVersusReward(param.rewards);
        Label_0055:
            num = manager.VS_StreakWinCnt_Now + 1;
            param2 = manager.GetVSStreakWinBonus(num, 1, GlobalVars.VersusFreeMatchTime);
            if (param2 == null)
            {
                goto Label_0081;
            }
            this.SetVersusReward(param2.rewards);
        Label_0081:
            num2 = manager.VS_StreakWinCnt_NowAllPriod + 1;
            if (manager.VS_StreakWinCnt_BestAllPriod >= num2)
            {
                goto Label_00BC;
            }
            param3 = manager.GetVSStreakWinBonus(num2, 0, GlobalVars.VersusFreeMatchTime);
            if (param3 == null)
            {
                goto Label_00BC;
            }
            this.SetVersusReward(param3.rewards);
        Label_00BC:
            return;
        }

        private void GainRankMatchItem()
        {
            QuestResult result;
            GameManager manager;
            PlayerData data;
            List<string> list;
            List<int> list2;
            int num;
            ItemParam param;
            int num2;
            if (this.IsMultiVersus != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (GlobalVars.SelectedMultiPlayVersusType == 1)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            result = this.GetQuestResult();
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            list = new List<string>();
            list2 = new List<int>();
            manager.GetTowerMatchItems(data.VersusTowerFloor - 1, list, list2, result == 1);
            manager.GetVersusTopFloorItems(data.VersusTowerFloor - 1, list, list2);
            num = 0;
            goto Label_00B9;
        Label_0067:
            param = manager.GetItemParam(list[num]);
            if (param == null)
            {
                goto Label_00B3;
            }
            num2 = 0;
            goto Label_00A3;
        Label_0086:
            this.mRecord.items.Add(new DropItemParam(param));
            num2 += 1;
        Label_00A3:
            if (num2 < list2[num])
            {
                goto Label_0086;
            }
        Label_00B3:
            num += 1;
        Label_00B9:
            if (num < list.Count)
            {
                goto Label_0067;
            }
            return;
        }

        public unsafe void GainUnitDrop(Record record, bool waitDirection)
        {
            int num;
            int num2;
            int num3;
            Unit unit;
            Unit.UnitDrop drop;
            int num4;
            int num5;
            DropItemParam param;
            DropItemParam param2;
            num = this.mEntryUnitMax - this.mNpcStartIndex;
            if (num != null)
            {
                goto Label_0015;
            }
            return;
        Label_0015:
            if (record.drops != null)
            {
                goto Label_002C;
            }
            record.drops = new OInt[num];
        Label_002C:
            Array.Clear(record.drops, 0, num);
            num2 = this.mNpcStartIndex;
            num3 = 0;
            goto Label_01CD;
        Label_0047:
            unit = this.mAllUnits[num2];
            if (unit.CheckItemDrop(waitDirection) != null)
            {
                goto Label_0065;
            }
            goto Label_01C5;
        Label_0065:
            drop = unit.Drop;
            num4 = 0;
            goto Label_0178;
        Label_0075:
            num5 = 0;
            goto Label_0153;
        Label_007D:
            if (drop.items[num4].isItem == null)
            {
                goto Label_00E0;
            }
            param = new DropItemParam(drop.items[num4].itemParam);
            param.mIsSecret = drop.items[num4].is_secret;
            record.items.Add(param);
            goto Label_014D;
        Label_00E0:
            if (drop.items[num4].isConceptCard == null)
            {
                goto Label_0143;
            }
            param2 = new DropItemParam(drop.items[num4].conceptCardParam);
            param2.mIsSecret = drop.items[num4].is_secret;
            record.items.Add(param2);
            goto Label_014D;
        Label_0143:
            DebugUtility.LogError("不明なドロップ品");
        Label_014D:
            num5 += 1;
        Label_0153:
            if (num5 < drop.items[num4].num)
            {
                goto Label_007D;
            }
            num4 += 1;
        Label_0178:
            if (num4 < drop.items.Count)
            {
                goto Label_0075;
            }
            record.gold += drop.gold;
            *(&(record.drops[num3])) = 1;
        Label_01C5:
            num2 += 1;
            num3 += 1;
        Label_01CD:
            if (num2 < this.mEntryUnitMax)
            {
                goto Label_0047;
            }
            return;
        }

        public unsafe void GainUnitSteal(Record record)
        {
            int num;
            int num2;
            int num3;
            Unit unit;
            Unit.UnitSteal steal;
            int num4;
            num = this.mEntryUnitMax - this.mNpcStartIndex;
            if (num != null)
            {
                goto Label_0015;
            }
            return;
        Label_0015:
            if (record.item_steals != null)
            {
                goto Label_002C;
            }
            record.item_steals = new OInt[num];
        Label_002C:
            Array.Clear(record.item_steals, 0, num);
            if (record.gold_steals != null)
            {
                goto Label_0050;
            }
            record.gold_steals = new OInt[num];
        Label_0050:
            Array.Clear(record.gold_steals, 0, num);
            num2 = this.mNpcStartIndex;
            num3 = 0;
            goto Label_0141;
        Label_006B:
            unit = this.mAllUnits[num2];
            if (unit.IsGimmick == null)
            {
                goto Label_0084;
            }
            return;
        Label_0084:
            steal = unit.Steal;
            if (steal.is_item_steeled == null)
            {
                goto Label_00F3;
            }
            num4 = 0;
            goto Label_00C9;
        Label_00A0:
            record.items.Add(new DropItemParam(steal.items[num4].itemParam));
            num4 += 1;
        Label_00C9:
            if (num4 < steal.items.Count)
            {
                goto Label_00A0;
            }
            *(&(record.item_steals[num3])) = 1;
        Label_00F3:
            if (steal.is_gold_steeled == null)
            {
                goto Label_0139;
            }
            record.gold += steal.gold;
            *(&(record.gold_steals[num3])) = 1;
        Label_0139:
            num2 += 1;
            num3 += 1;
        Label_0141:
            if (num2 < this.mEntryUnitMax)
            {
                goto Label_006B;
            }
            return;
        }

        private int GetAliveUnitCount(Unit self)
        {
            int num;
            int num2;
            Unit unit;
            num = 0;
            num2 = 0;
            goto Label_0054;
        Label_0009:
            unit = this.mUnits[num2];
            if (unit.IsDead == null)
            {
                goto Label_0026;
            }
            goto Label_0050;
        Label_0026:
            if (unit.IsGimmick == null)
            {
                goto Label_0036;
            }
            goto Label_0050;
        Label_0036:
            if (unit.Side == self.Side)
            {
                goto Label_004C;
            }
            goto Label_0050;
        Label_004C:
            num += 1;
        Label_0050:
            num2 += 1;
        Label_0054:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        private int GetAtkBonusForAttackDetailType(Unit self, SkillData skill)
        {
            int num;
            AttackDetailTypes types;
            num = 0;
            if (skill.IsReactionSkill() == null)
            {
                goto Label_0022;
            }
            num += self.CurrentStatus[11];
        Label_0022:
            switch ((skill.AttackDetailType - 1))
            {
                case 0:
                    goto Label_004E;

                case 1:
                    goto Label_0076;

                case 2:
                    goto Label_009E;

                case 3:
                    goto Label_00C6;

                case 4:
                    goto Label_00EF;

                case 5:
                    goto Label_0118;
            }
            goto Label_0141;
        Label_004E:
            num += self.CurrentStatus[6];
            num += this.mQuestParam.GetAtkTypeMag(1);
            goto Label_0146;
        Label_0076:
            num += self.CurrentStatus[7];
            num += this.mQuestParam.GetAtkTypeMag(2);
            goto Label_0146;
        Label_009E:
            num += self.CurrentStatus[8];
            num += this.mQuestParam.GetAtkTypeMag(3);
            goto Label_0146;
        Label_00C6:
            num += self.CurrentStatus[9];
            num += this.mQuestParam.GetAtkTypeMag(4);
            goto Label_0146;
        Label_00EF:
            num += self.CurrentStatus[10];
            num += this.mQuestParam.GetAtkTypeMag(5);
            goto Label_0146;
        Label_0118:
            num += self.CurrentStatus[12];
            num += this.mQuestParam.GetAtkTypeMag(6);
            goto Label_0146;
        Label_0141:;
        Label_0146:
            return num;
        }

        public int GetAttackRangeBonus(int selfHeight, int targetHeight)
        {
            int num;
            int num2;
            num = selfHeight - targetHeight;
            if (Math.Abs(num) >= BattleMap.MAP_FLOOR_HEIGHT)
            {
                goto Label_0016;
            }
            return 0;
        Label_0016:
            num2 = num / BattleMap.MAP_FLOOR_HEIGHT;
            return num2;
        }

        private int GetAvoidRate(Unit self, Unit target, SkillData skill)
        {
            FixParam param;
            int num;
            int num2;
            int num3;
            EUnitCondition condition;
            AttackDetailTypes types;
            if (target.IsUnitCondition(8L) != null)
            {
                goto Label_0039;
            }
            if (target.IsUnitCondition(4L) != null)
            {
                goto Label_0039;
            }
            if (target.IsUnitCondition(0x20L) != null)
            {
                goto Label_0039;
            }
            if (target.IsUnitCondition(0x10000L) == null)
            {
                goto Label_003B;
            }
        Label_0039:
            return 0;
        Label_003B:
            if (skill.IsForceHit() == null)
            {
                goto Label_0048;
            }
            return 0;
        Label_0048:
            if (target.IsBreakObj == null)
            {
                goto Label_0064;
            }
            if (target.BreakObjClashType != 3)
            {
                goto Label_0062;
            }
            return 100;
        Label_0062:
            return 0;
        Label_0064:
            if (target.AI == null)
            {
                goto Label_0083;
            }
            if (target.AI.CheckFlag(0x20) == null)
            {
                goto Label_0083;
            }
            return 0;
        Label_0083:
            if (this.IsCombinationAttack(skill) == null)
            {
                goto Label_0091;
            }
            return 0;
        Label_0091:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            num = param.AvoidBaseRate;
            num2 = target.CurrentStatus.param.spd - (self.CurrentStatus.param.dex / 2);
            if (num2 == null)
            {
                goto Label_00F4;
            }
            num += (num2 * param.AvoidParamScale) / 100;
        Label_00F4:
            num += Sqrt((target.Lv - self.Lv) / 2);
            num += target.GetBaseAvoidRate();
            num += target.CurrentStatus[4];
            num -= self.CurrentStatus[3];
            if (self.IsUnitFlag(0x20) == null)
            {
                goto Label_0157;
            }
            num -= param.AddHitRateSide;
        Label_0157:
            if (self.IsUnitFlag(0x40) == null)
            {
                goto Label_0172;
            }
            num -= param.AddHitRateBack;
        Label_0172:
            num3 = 0;
            if (skill.IsReactionSkill() == null)
            {
                goto Label_019D;
            }
            num3 += skill.CalcBuffEffectValue(0x77, target.CurrentStatus[0x27], 0);
        Label_019D:
            switch ((skill.AttackDetailType - 1))
            {
                case 0:
                    goto Label_01CB;

                case 1:
                    goto Label_01E5;

                case 2:
                    goto Label_01FF;

                case 3:
                    goto Label_0219;

                case 4:
                    goto Label_0233;

                case 5:
                    goto Label_024D;
            }
            goto Label_0267;
        Label_01CB:
            num3 += target.CurrentStatus[0x22];
            goto Label_026C;
        Label_01E5:
            num3 += target.CurrentStatus[0x23];
            goto Label_026C;
        Label_01FF:
            num3 += target.CurrentStatus[0x24];
            goto Label_026C;
        Label_0219:
            num3 += target.CurrentStatus[0x25];
            goto Label_026C;
        Label_0233:
            num3 += target.CurrentStatus[0x26];
            goto Label_026C;
        Label_024D:
            num3 += target.CurrentStatus[40];
            goto Label_026C;
        Label_0267:;
        Label_026C:
            num += num3;
            if (skill.IsAreaSkill() != null)
            {
                goto Label_0294;
            }
            if (target.IsDisableUnitCondition(0x2000000L) == null)
            {
                goto Label_02A8;
            }
            num = 100;
            goto Label_02A8;
        Label_0294:
            if (target.IsDisableUnitCondition(0x4000000L) == null)
            {
                goto Label_02A8;
            }
            num = 100;
        Label_02A8:
            if (skill.IsMhmDamage() == null)
            {
                goto Label_02EC;
            }
            if (num >= 100)
            {
                goto Label_02EC;
            }
            condition = 0x800000000L;
            if (skill.IsJewelAttack() == null)
            {
                goto Label_02DC;
            }
            condition = 0x1000000000L;
        Label_02DC:
            if (target.IsDisableUnitCondition(condition) == null)
            {
                goto Label_02EC;
            }
            num = 100;
        Label_02EC:
            return Math.Max(Math.Min(num, param.MaxAvoidRate), 0);
        }

        public unsafe CommandResult GetCommandResult(Unit self, int x, int y, int tx, int ty, SkillData skill)
        {
            CommandResult result;
            int num;
            int num2;
            EUnitDirection direction;
            int num3;
            int num4;
            List<Unit> list;
            ShotTarget target;
            LogSkill skill2;
            int num5;
            List<LogSkill> list2;
            int num6;
            Unit unit;
            int num7;
            Unit unit2;
            UnitResult result2;
            LogSkill.Target.CondHit hit;
            List<LogSkill.Target.CondHit>.Enumerator enumerator;
            int num8;
            int num9;
            LogSkill.Target target2;
            Unit unit3;
            UnitResult result3;
            LogSkill.Target.CondHit hit2;
            List<LogSkill.Target.CondHit>.Enumerator enumerator2;
            eTeleportType type;
            this.SetBattleFlag(5, 1);
            result = new CommandResult();
            result.self = self;
            result.skill = skill;
            this.mRandDamage.Seed(this.mSeedDamage);
            this.CurrentRand = this.mRandDamage;
            num = self.x;
            num2 = self.y;
            direction = self.Direction;
            self.x = x;
            self.y = y;
            if (tx != x)
            {
                goto Label_006D;
            }
            if (ty == y)
            {
                goto Label_0081;
            }
        Label_006D:
            self.Direction = UnitDirectionFromVector(self, tx - x, ty - y);
        Label_0081:
            num3 = tx;
            num4 = ty;
            type = skill.TeleportType;
            if (type == 2)
            {
                goto Label_00A7;
            }
            if (type == 3)
            {
                goto Label_00C8;
            }
            goto Label_00DD;
        Label_00A7:
            if (skill.IsTargetGridNoUnit == null)
            {
                goto Label_00DD;
            }
            self.x = tx;
            self.y = ty;
            goto Label_00DD;
        Label_00C8:
            num3 = self.x;
            num4 = self.y;
        Label_00DD:
            if (this.CheckEnableUseSkill(self, skill, 0) == null)
            {
                goto Label_00FA;
            }
            if (this.IsUseSkillCollabo(self, skill) != null)
            {
                goto Label_010A;
            }
        Label_00FA:
            this.DebugErr("スキル使用条件を満たせなかった");
            goto Label_06C6;
        Label_010A:
            list = null;
            target = null;
            this.GetExecuteSkillLineTarget(self, num3, num4, skill, &list, &target);
            if (list == null)
            {
                goto Label_06C6;
            }
            if (list.Count <= 0)
            {
                goto Label_06C6;
            }
            skill2 = new LogSkill();
            skill2.self = self;
            skill2.skill = skill;
            &skill2.pos.x = tx;
            &skill2.pos.y = ty;
            skill2.reflect = null;
            skill2.is_append = skill.IsCutin() == 0;
            num5 = 0;
            goto Label_01A2;
        Label_018A:
            skill2.SetSkillTarget(self, list[num5]);
            num5 += 1;
        Label_01A2:
            if (num5 < list.Count)
            {
                goto Label_018A;
            }
            if (target == null)
            {
                goto Label_0219;
            }
            &skill2.pos.x = target.end.x;
            &skill2.pos.y = target.end.y;
            skill2.rad = (int) (target.rad * 100.0);
            skill2.height = (int) (target.height * 100.0);
        Label_0219:
            list2 = new List<LogSkill>();
            result.targets = new List<UnitResult>(skill2.targets.Count);
            result.reactions = new List<UnitResult>(skill2.targets.Count);
            this.SetBattleFlag(7, 1);
            this.ExecuteFirstReactionSkill(self, list, skill, tx, ty, list2);
            skill2.CheckAliveTarget();
            this.ExecuteSkill(0, skill2, skill);
            this.ExecuteReactionSkill(skill2, list2);
            this.SetBattleFlag(7, 0);
            num6 = 0;
            goto Label_02BD;
        Label_0293:
            unit = this.AllUnits[num6];
            if (unit.RemoveBuffPrevApply() == null)
            {
                goto Label_02B7;
            }
            unit.CalcCurrentStatus(0, 0);
        Label_02B7:
            num6 += 1;
        Label_02BD:
            if (num6 < this.AllUnits.Count)
            {
                goto Label_0293;
            }
            num7 = 0;
            goto Label_0438;
        Label_02D7:
            unit2 = skill2.targets[num7].target;
            result2 = new UnitResult();
            result2.unit = unit2;
            result2.hp_damage += skill2.targets[num7].GetTotalHpDamage();
            result2.hp_heal += skill2.targets[num7].GetTotalHpHeal();
            result2.mp_damage += skill2.targets[num7].GetTotalMpDamage();
            result2.mp_heal += skill2.targets[num7].GetTotalMpHeal();
            result2.critical = skill2.targets[num7].GetTotalCriticalRate();
            result2.avoid = skill2.targets[num7].GetTotalAvoidRate();
            result2.cond_hit_lists.Clear();
            enumerator = skill2.targets[num7].CondHitLists.GetEnumerator();
        Label_03DA:
            try
            {
                goto Label_0407;
            Label_03DF:
                hit = &enumerator.Current;
                result2.cond_hit_lists.Add(new LogSkill.Target.CondHit(hit.Cond, hit.Per));
            Label_0407:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_03DF;
                }
                goto Label_0425;
            }
            finally
            {
            Label_0418:
                ((List<LogSkill.Target.CondHit>.Enumerator) enumerator).Dispose();
            }
        Label_0425:
            result.targets.Add(result2);
            num7 += 1;
        Label_0438:
            if (num7 < skill2.targets.Count)
            {
                goto Label_02D7;
            }
            if (skill2.self_effect == null)
            {
                goto Label_04F1;
            }
            result.self_effect = new UnitResult();
            result.self_effect.unit = skill2.self_effect.target;
            result.self_effect.hp_damage += skill2.self_effect.GetTotalHpDamage();
            result.self_effect.hp_heal += skill2.self_effect.GetTotalHpHeal();
            result.self_effect.mp_damage += skill2.self_effect.GetTotalMpDamage();
            result.self_effect.mp_heal += skill2.self_effect.GetTotalMpHeal();
        Label_04F1:
            num8 = 0;
            goto Label_06B8;
        Label_04F9:
            num9 = 0;
            goto Label_0698;
        Label_0501:
            target2 = list2[num8].targets[num9];
            unit3 = target2.target;
            result3 = new UnitResult();
            result3.react_unit = list2[num8].self;
            result3.unit = unit3;
            result3.hp_damage += target2.GetTotalHpDamage();
            result3.hp_heal += target2.GetTotalHpHeal();
            result3.mp_damage += target2.GetTotalMpDamage();
            result3.mp_heal += target2.GetTotalMpHeal();
            result3.critical = target2.GetTotalCriticalRate();
            result3.avoid = target2.GetTotalAvoidRate();
            if (list2[num8].skill.EffectRate <= 0)
            {
                goto Label_0617;
            }
            if (list2[num8].skill.EffectRate >= 100)
            {
                goto Label_0617;
            }
            result3.reaction = list2[num8].skill.EffectRate;
            goto Label_0620;
        Label_0617:
            result3.reaction = 100;
        Label_0620:
            result3.cond_hit_lists.Clear();
            enumerator2 = target2.CondHitLists.GetEnumerator();
        Label_063A:
            try
            {
                goto Label_0667;
            Label_063F:
                hit2 = &enumerator2.Current;
                result3.cond_hit_lists.Add(new LogSkill.Target.CondHit(hit2.Cond, hit2.Per));
            Label_0667:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_063F;
                }
                goto Label_0685;
            }
            finally
            {
            Label_0678:
                ((List<LogSkill.Target.CondHit>.Enumerator) enumerator2).Dispose();
            }
        Label_0685:
            result.reactions.Add(result3);
            num9 += 1;
        Label_0698:
            if (num9 < list2[num8].targets.Count)
            {
                goto Label_0501;
            }
            num8 += 1;
        Label_06B8:
            if (num8 < list2.Count)
            {
                goto Label_04F9;
            }
        Label_06C6:
            self.x = num;
            self.y = num2;
            self.Direction = direction;
            this.CurrentRand = this.mRand;
            self.SetUnitFlag(0x20, 0);
            self.SetUnitFlag(0x40, 0);
            this.SetBattleFlag(5, 0);
            return result;
        }

        public unsafe void GetConditionTrickByTag(List<TrickData> results, string tags)
        {
            char[] chArray1;
            string[] strArray;
            List<TrickData> list;
            int num;
            TrickData data;
            List<TrickData>.Enumerator enumerator;
            if (results == null)
            {
                goto Label_0011;
            }
            if (string.IsNullOrEmpty(tags) == null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            chArray1 = new char[] { 0x2c };
            strArray = tags.Split(chArray1);
            if (strArray != null)
            {
                goto Label_0033;
            }
            if (((int) strArray.Length) != null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            list = TrickData.GetEffectAll();
            num = 0;
            goto Label_0099;
        Label_0040:
            enumerator = list.GetEnumerator();
        Label_0048:
            try
            {
                goto Label_0077;
            Label_004D:
                data = &enumerator.Current;
                if (this.IsMatchTrickTag(data, strArray[num]) == null)
                {
                    goto Label_0077;
                }
                if (results.Contains(data) != null)
                {
                    goto Label_0077;
                }
                results.Add(data);
            Label_0077:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_004D;
                }
                goto Label_0095;
            }
            finally
            {
            Label_0088:
                ((List<TrickData>.Enumerator) enumerator).Dispose();
            }
        Label_0095:
            num += 1;
        Label_0099:
            if (num < ((int) strArray.Length))
            {
                goto Label_0040;
            }
            return;
        }

        public unsafe void GetConditionTrickByTrickID(List<TrickData> results, string inames)
        {
            char[] chArray1;
            string[] strArray;
            List<TrickData> list;
            int num;
            TrickData data;
            List<TrickData>.Enumerator enumerator;
            if (results == null)
            {
                goto Label_0011;
            }
            if (string.IsNullOrEmpty(inames) == null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            chArray1 = new char[] { 0x2c };
            strArray = inames.Split(chArray1);
            if (strArray != null)
            {
                goto Label_0033;
            }
            if (((int) strArray.Length) != null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            list = TrickData.GetEffectAll();
            num = 0;
            goto Label_00A2;
        Label_0040:
            enumerator = list.GetEnumerator();
        Label_0048:
            try
            {
                goto Label_0080;
            Label_004D:
                data = &enumerator.Current;
                if ((strArray[num] == data.TrickParam.Name) == null)
                {
                    goto Label_0080;
                }
                if (results.Contains(data) != null)
                {
                    goto Label_0080;
                }
                results.Add(data);
            Label_0080:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_004D;
                }
                goto Label_009E;
            }
            finally
            {
            Label_0091:
                ((List<TrickData>.Enumerator) enumerator).Dispose();
            }
        Label_009E:
            num += 1;
        Label_00A2:
            if (num < ((int) strArray.Length))
            {
                goto Label_0040;
            }
            return;
        }

        public unsafe void GetConditionUnitByUniqueName(List<Unit> results, string tags, out bool is_starter)
        {
            char[] chArray1;
            string[] strArray;
            int num;
            int num2;
            *((sbyte*) is_starter) = 0;
            if (results == null)
            {
                goto Label_0014;
            }
            if (string.IsNullOrEmpty(tags) == null)
            {
                goto Label_0015;
            }
        Label_0014:
            return;
        Label_0015:
            chArray1 = new char[] { 0x2c };
            strArray = tags.Split(chArray1);
            if (strArray != null)
            {
                goto Label_0036;
            }
            if (((int) strArray.Length) != null)
            {
                goto Label_0036;
            }
            return;
        Label_0036:
            num = 0;
            goto Label_00B5;
        Label_003D:
            num2 = 0;
            goto Label_00A0;
        Label_0044:
            if (this.CheckMatchUniqueName(this.Units[num2], strArray[num]) == null)
            {
                goto Label_0087;
            }
            if (results.Contains(this.Units[num2]) != null)
            {
                goto Label_0087;
            }
            results.Add(this.Units[num2]);
        Label_0087:
            if ((strArray[num] == "starter") == null)
            {
                goto Label_009C;
            }
            *((sbyte*) is_starter) = 1;
        Label_009C:
            num2 += 1;
        Label_00A0:
            if (num2 < this.Units.Count)
            {
                goto Label_0044;
            }
            num += 1;
        Label_00B5:
            if (num < ((int) strArray.Length))
            {
                goto Label_003D;
            }
            return;
        }

        public void GetConditionUnitByUnitID(List<Unit> results, string inames)
        {
            char[] chArray1;
            string[] strArray;
            int num;
            int num2;
            if (results == null)
            {
                goto Label_0011;
            }
            if (string.IsNullOrEmpty(inames) == null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            chArray1 = new char[] { 0x2c };
            strArray = inames.Split(chArray1);
            if (strArray != null)
            {
                goto Label_0033;
            }
            if (((int) strArray.Length) != null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            num = 0;
            goto Label_00A6;
        Label_003A:
            num2 = 0;
            goto Label_0091;
        Label_0041:
            if ((strArray[num] == this.Units[num2].UnitParam.iname) == null)
            {
                goto Label_008D;
            }
            if (results.Contains(this.Units[num2]) != null)
            {
                goto Label_008D;
            }
            results.Add(this.Units[num2]);
        Label_008D:
            num2 += 1;
        Label_0091:
            if (num2 < this.Units.Count)
            {
                goto Label_0041;
            }
            num += 1;
        Label_00A6:
            if (num < ((int) strArray.Length))
            {
                goto Label_003A;
            }
            return;
        }

        public Grid GetCorrectDuplicatePosition(Unit self)
        {
            BattleMap map;
            Grid grid;
            int num;
            Unit unit;
            int num2;
            int num3;
            Grid grid2;
            int num4;
            int num5;
            int num6;
            int num7;
            Grid grid3;
            bool flag;
            int num8;
            int num9;
            map = this.CurrentMap;
            grid = map[self.x, self.y];
            num = 0;
            goto Label_0200;
        Label_0021:
            unit = this.Units[num];
            if (unit != self)
            {
                goto Label_003A;
            }
            goto Label_01FC;
        Label_003A:
            if (unit.CheckCollision(grid.x, grid.y, 1) == null)
            {
                goto Label_01FC;
            }
            num2 = Math.Max(map.Width, map.Height);
            num3 = 0x3e7;
            grid2 = null;
            num4 = -num2;
            goto Label_01DE;
        Label_0079:
            num5 = -num2;
            goto Label_01CF;
        Label_0083:
            if ((Math.Abs(num5) + Math.Abs(num4)) <= num2)
            {
                goto Label_009E;
            }
            goto Label_01C9;
        Label_009E:
            num6 = self.x + num5;
            num7 = self.y + num4;
            grid3 = map[num6, num7];
            if (map.CheckEnableMove(self, grid3, 0, 0) != null)
            {
                goto Label_00D5;
            }
            goto Label_01C9;
        Label_00D5:
            flag = 1;
            num8 = 0;
            goto Label_018F;
        Label_00E0:
            if (this.mUnits[num8].IsGimmick == null)
            {
                goto Label_00FC;
            }
            goto Label_0189;
        Label_00FC:
            if (this.mUnits[num8] != self)
            {
                goto Label_0114;
            }
            goto Label_0189;
        Label_0114:
            if (this.mUnits[num8].IsSub == null)
            {
                goto Label_0130;
            }
            goto Label_0189;
        Label_0130:
            if (this.mUnits[num8].IsDead == null)
            {
                goto Label_014C;
            }
            goto Label_0189;
        Label_014C:
            if (this.mUnits[num8].IsEntry != null)
            {
                goto Label_0168;
            }
            goto Label_0189;
        Label_0168:
            if (this.mUnits[num8].CheckCollision(grid3) == null)
            {
                goto Label_0189;
            }
            flag = 0;
            goto Label_01A1;
        Label_0189:
            num8 += 1;
        Label_018F:
            if (num8 < this.mUnits.Count)
            {
                goto Label_00E0;
            }
        Label_01A1:
            if (flag != null)
            {
                goto Label_01AD;
            }
            goto Label_01C9;
        Label_01AD:
            num9 = this.CalcGridDistance(grid, grid3);
            if (num9 >= num3)
            {
                goto Label_01C9;
            }
            num3 = num9;
            grid2 = grid3;
        Label_01C9:
            num5 += 1;
        Label_01CF:
            if (num5 <= num2)
            {
                goto Label_0083;
            }
            num4 += 1;
        Label_01DE:
            if (num4 <= num2)
            {
                goto Label_0079;
            }
            DebugUtility.Assert((grid2 == null) == 0, "空きグリッドが見つからなかった");
            return grid2;
        Label_01FC:
            num += 1;
        Label_0200:
            if (num < this.Units.Count)
            {
                goto Label_0021;
            }
            return grid;
        }

        public int GetCriticalDamage(Unit self, int damage, uint rand)
        {
            FixParam param;
            int num;
            int num2;
            int num3;
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            num = param.MinCriticalDamageRate;
            num2 = param.MaxCriticalDamageRate;
            num3 = num + ((int) (((ulong) rand) % ((long) (num2 - num))));
            damage += ((100 * damage) * num3) / 0x2710;
            return damage;
        }

        private int GetCriticalRate(Unit self, Unit target, SkillData skill)
        {
            FixParam param;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            Grid grid;
            Grid grid2;
            int num9;
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (skill.IsNormalAttack() != null)
            {
                goto Label_002A;
            }
            if (skill.SkillParam.IsCritical != null)
            {
                goto Label_002A;
            }
            return 0;
        Label_002A:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            num = 0x3e8;
            num2 = param.CriticalRate_Cri_Multiply;
            num3 = param.CriticalRate_Cri_Division;
            num4 = Sqrt((self.CurrentStatus.param.cri * num) * num);
            if (num4 == null)
            {
                goto Label_0097;
            }
            if (num2 == null)
            {
                goto Label_008B;
            }
            num4 *= num2;
        Label_008B:
            if (num3 == null)
            {
                goto Label_0097;
            }
            num4 /= num3;
        Label_0097:
            num5 = param.CriticalRate_Luk_Multiply;
            num6 = param.CriticalRate_Luk_Division;
            num7 = Sqrt((target.CurrentStatus.param.luk * num) * num);
            if (num7 == null)
            {
                goto Label_00F4;
            }
            if (num5 == null)
            {
                goto Label_00E6;
            }
            num7 *= num5;
        Label_00E6:
            if (num6 == null)
            {
                goto Label_00F4;
            }
            num7 /= num6;
        Label_00F4:
            num8 = num4 - num7;
            if (num8 == null)
            {
                goto Label_0108;
            }
            num8 /= num;
        Label_0108:
            grid = this.GetUnitGridPosition(self);
            grid2 = this.GetUnitGridPosition(target);
            if (grid == null)
            {
                goto Label_016E;
            }
            if (grid2 == null)
            {
                goto Label_016E;
            }
            num9 = grid.height - grid2.height;
            if (num9 <= 0)
            {
                goto Label_0156;
            }
            num8 += param.HighGridCriRate;
            goto Label_016E;
        Label_0156:
            if (num9 >= 0)
            {
                goto Label_016E;
            }
            num8 += param.DownGridCriRate;
        Label_016E:
            num8 += self.CurrentStatus[5];
            return Math.Min(Math.Max(num8, 0), 100);
        }

        private int GetCurrentEnemyNum(Unit self)
        {
            int num;
            int num2;
            Unit unit;
            num = 0;
            num2 = 0;
            goto Label_0068;
        Label_0009:
            unit = this.mUnits[num2];
            if (unit.IsDead != null)
            {
                goto Label_0064;
            }
            if (unit.IsEntry == null)
            {
                goto Label_0064;
            }
            if (unit.IsGimmick != null)
            {
                goto Label_0064;
            }
            if (unit.IsSub != null)
            {
                goto Label_0064;
            }
            if (unit != self)
            {
                goto Label_004E;
            }
            goto Label_0064;
        Label_004E:
            if (this.CheckEnemySide(self, unit) != null)
            {
                goto Label_0060;
            }
            goto Label_0064;
        Label_0060:
            num += 1;
        Label_0064:
            num2 += 1;
        Label_0068:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        private int GetDamageSkill(Unit attacker, Unit defender, SkillData skill, LogSkill log)
        {
            int num;
            int num2;
            int num3;
            num = this.CalcAtkPointSkill(attacker, defender, skill, log);
            num2 = this.CalcDefPointSkill(attacker, defender, skill, log);
            num3 = Math.Max(num - num2, 0);
            if (skill.IsJewelAttack() == null)
            {
                goto Label_003B;
            }
            num3 = Sqrt(num3) * 2;
            goto Label_004F;
        Label_003B:
            num3 = this.GetResistDamageForAttackDetailType(defender, skill, num3);
            num3 = this.GetResistDamageForUnitDefense(defender, skill, num3);
        Label_004F:
            return num3;
        }

        public int GetDeadCount(EUnitSide side)
        {
            int num;
            int num2;
            List<Unit> list;
            int num3;
            num = 0;
            if (side != null)
            {
                goto Label_0043;
            }
            num2 = 0;
            goto Label_002D;
        Label_000F:
            if (this.mPlayer[num2].IsDeadCondition() == null)
            {
                goto Label_0029;
            }
            num += 1;
        Label_0029:
            num2 += 1;
        Label_002D:
            if (num2 < this.mPlayer.Count)
            {
                goto Label_000F;
            }
            goto Label_0096;
        Label_0043:
            if (side != 1)
            {
                goto Label_0096;
            }
            list = this.mEnemys[this.MapIndex];
            num3 = 0;
            goto Label_008A;
        Label_005F:
            if (list[num3].Side != 1)
            {
                goto Label_0086;
            }
            if (list[num3].IsDeadCondition() == null)
            {
                goto Label_0086;
            }
            num += 1;
        Label_0086:
            num3 += 1;
        Label_008A:
            if (num3 < list.Count)
            {
                goto Label_005F;
            }
        Label_0096:
            return num;
        }

        public int GetDeadCountEnemy()
        {
            int num;
            int num2;
            num = 0;
            num2 = 0;
            goto Label_0059;
        Label_0009:
            if (this.Units[num2].Side != 1)
            {
                goto Label_0055;
            }
            if (this.Units[num2].IsDead == null)
            {
                goto Label_0055;
            }
            if (this.Units[num2].IsUnitFlag(0x100000) != null)
            {
                goto Label_0055;
            }
            num += 1;
        Label_0055:
            num2 += 1;
        Label_0059:
            if (num2 < this.Units.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        private int GetDeadUnitCount(Unit self)
        {
            int num;
            int num2;
            Unit unit;
            num = 0;
            num2 = 0;
            goto Label_0054;
        Label_0009:
            unit = this.mUnits[num2];
            if (unit.IsDead != null)
            {
                goto Label_0026;
            }
            goto Label_0050;
        Label_0026:
            if (unit.IsGimmick == null)
            {
                goto Label_0036;
            }
            goto Label_0050;
        Label_0036:
            if (unit.Side == self.Side)
            {
                goto Label_004C;
            }
            goto Label_0050;
        Label_004C:
            num += 1;
        Label_0050:
            num2 += 1;
        Label_0054:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        private List<Grid> GetEnableMoveGridList(Unit self, bool is_move, bool is_friendlyfire, bool is_sneaked, bool is_treasure, bool is_trickpanel)
        {
            BattleMap map;
            Grid grid;
            List<Grid> list;
            int num;
            int num2;
            Grid grid2;
            int num3;
            int num4;
            int num5;
            Grid grid3;
            int num6;
            int num7;
            int num8;
            int num9;
            Grid grid4;
            map = this.CurrentMap;
            grid = map[self.x, self.y];
            list = new List<Grid>(this.mMoveMap.w * this.mMoveMap.h);
            num = self.GetMoveCount(0);
            if ((self.IsUnitFlag(2) == null) && (self.IsEnableMoveCondition(0) != null))
            {
                goto Label_0059;
            }
            num = 0;
        Label_0059:
            if ((is_move == null) || (num <= 0))
            {
                goto Label_02E9;
            }
            num2 = 0xff;
            if ((self.TreasureGainTarget == null) || (is_treasure == null))
            {
                goto Label_00C3;
            }
            grid2 = map[self.x, self.y];
            map.CalcMoveSteps(self, self.TreasureGainTarget, 1);
            num2 = (grid2.step / num) + (((grid2.step % num) == null) ? 0 : 1);
        Label_00C3:
            num3 = 0;
            goto Label_01E3;
        Label_00CB:
            num4 = 0;
            goto Label_01CB;
        Label_00D3:
            if (this.mMoveMap.get(num3, num4) >= 0)
            {
                goto Label_00F1;
            }
            goto Label_01C5;
        Label_00F1:
            grid3 = map[num3, num4];
            if ((is_friendlyfire == null) || (this.CheckFriendlyFireOnGridMap(self, grid3) == null))
            {
                goto Label_0116;
            }
            goto Label_01C5;
        Label_0116:
            if ((is_trickpanel == null) || (this.IsFailTrickData(self, num3, num4) == null))
            {
                goto Label_0132;
            }
            goto Label_01C5;
        Label_0132:
            if ((self.TreasureGainTarget == null) || (is_treasure == null))
            {
                goto Label_0178;
            }
            num6 = (grid3.step / num) + (((grid3.step % num) == null) ? 0 : 1);
            if (num6 <= num2)
            {
                goto Label_0198;
            }
            goto Label_01C5;
            goto Label_0198;
        Label_0178:
            if (is_sneaked == null)
            {
                goto Label_0198;
            }
            if (this.mSearchMap.get(num3, num4) == null)
            {
                goto Label_0198;
            }
            goto Label_01C5;
        Label_0198:
            if (this.CheckMove(self, grid3) != null)
            {
                goto Label_01AB;
            }
            goto Label_01C5;
        Label_01AB:
            if (list.Contains(grid3) == null)
            {
                goto Label_01BD;
            }
            goto Label_01C5;
        Label_01BD:
            list.Add(grid3);
        Label_01C5:
            num4 += 1;
        Label_01CB:
            if (num4 < this.mMoveMap.h)
            {
                goto Label_00D3;
            }
            num3 += 1;
        Label_01E3:
            if (num3 < this.mMoveMap.w)
            {
                goto Label_00CB;
            }
            if (self.TreasureGainTarget == null)
            {
                goto Label_02F0;
            }
            if (is_treasure == null)
            {
                goto Label_02F0;
            }
            if (list.Contains(self.TreasureGainTarget) == null)
            {
                goto Label_022F;
            }
            list.Clear();
            list.Add(self.TreasureGainTarget);
            goto Label_02B0;
        Label_022F:
            num7 = 0xff;
            num8 = 0;
            goto Label_0267;
        Label_023E:
            if (num7 <= list[num8].step)
            {
                goto Label_0261;
            }
            num7 = list[num8].step;
        Label_0261:
            num8 += 1;
        Label_0267:
            if (num8 < list.Count)
            {
                goto Label_023E;
            }
            num9 = 0;
            goto Label_02A3;
        Label_027C:
            if (num7 >= list[num9].step)
            {
                goto Label_029D;
            }
            list.RemoveAt(num9--);
        Label_029D:
            num9 += 1;
        Label_02A3:
            if (num9 < list.Count)
            {
                goto Label_027C;
            }
        Label_02B0:
            if (self.IsEnableActionCondition() == null)
            {
                goto Label_02F0;
            }
            grid4 = map[self.x, self.y];
            if (list.Contains(grid4) != null)
            {
                goto Label_02F0;
            }
            list.Add(grid4);
            goto Label_02F0;
        Label_02E9:
            list.Add(grid);
        Label_02F0:
            return list;
        }

        private void GetEnemyPriorities(Unit self, List<Unit> enemyTargets, List<Unit> gimmickTargets)
        {
            Unit unit;
            int num;
            Unit unit2;
            if (self != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            enemyTargets.Clear();
            gimmickTargets.Clear();
            unit = self.GetRageTarget();
            if (unit == null)
            {
                goto Label_002C;
            }
            enemyTargets.Add(unit);
            goto Label_00ED;
        Label_002C:
            num = 0;
            goto Label_00D4;
        Label_0033:
            unit2 = this.mUnits[num];
            if (unit2 == self)
            {
                goto Label_00D0;
            }
            if (unit2.IsDead != null)
            {
                goto Label_00D0;
            }
            if (unit2.IsSub != null)
            {
                goto Label_00D0;
            }
            if (unit2.IsEntry != null)
            {
                goto Label_006D;
            }
            goto Label_00D0;
        Label_006D:
            if (unit2.IsGimmick == null)
            {
                goto Label_00B7;
            }
            if (unit2.IsBreakObj == null)
            {
                goto Label_00D0;
            }
            if (this.IsTargetBreakUnit(self, unit2, null) == null)
            {
                goto Label_00D0;
            }
            if (this.IsTargetBreakUnitAI(self, unit2) == null)
            {
                goto Label_00D0;
            }
            if (this.CheckGimmickEnemySide(self, unit2) == null)
            {
                goto Label_00D0;
            }
            gimmickTargets.Add(unit2);
            goto Label_00D0;
        Label_00B7:
            if (this.CheckEnemySide(self, unit2) != null)
            {
                goto Label_00C9;
            }
            goto Label_00D0;
        Label_00C9:
            enemyTargets.Add(unit2);
        Label_00D0:
            num += 1;
        Label_00D4:
            if (num < this.mUnits.Count)
            {
                goto Label_0033;
            }
            this.SortAttackTargets(self, enemyTargets);
        Label_00ED:
            return;
        }

        private Grid GetEscapePositionAI(Unit self)
        {
            BattleMap map;
            int num;
            bool flag;
            Grid grid;
            List<Unit> list;
            int num2;
            Grid grid2;
            bool flag2;
            int num3;
            int num4;
            Grid grid3;
            map = this.CurrentMap;
            if (((self.IsUnitFlag(2) != null) ? 0 : self.GetMoveCount(0)) != null)
            {
                goto Label_0029;
            }
            return null;
        Label_0029:
            flag = (self.AI == null) ? 0 : self.AI.CheckFlag(0x40);
            grid = map[self.x, self.y];
            list = this.GetHealer(self);
            num2 = 0;
            goto Label_0160;
        Label_0069:
            grid2 = map[list[num2].x, list[num2].y];
            if (map.CalcMoveSteps(self, grid2, 0) != null)
            {
                goto Label_00A1;
            }
            goto Label_015A;
        Label_00A1:
            flag2 = 0;
            num3 = 0;
            goto Label_013C;
        Label_00AC:
            num4 = 0;
            goto Label_0124;
        Label_00B4:
            if (this.mMoveMap.get(num3, num4) >= 0)
            {
                goto Label_00CE;
            }
            goto Label_011E;
        Label_00CE:
            grid3 = map[num3, num4];
            if (this.CheckMove(self, grid3) != null)
            {
                goto Label_00ED;
            }
            goto Label_011E;
        Label_00ED:
            if (flag == null)
            {
                goto Label_0106;
            }
            if (this.CheckFriendlyFireOnGridMap(self, grid3) == null)
            {
                goto Label_0106;
            }
            goto Label_011E;
        Label_0106:
            if (grid.step <= grid3.step)
            {
                goto Label_011E;
            }
            grid = grid3;
            flag2 = 1;
        Label_011E:
            num4 += 1;
        Label_0124:
            if (num4 < this.mMoveMap.h)
            {
                goto Label_00B4;
            }
            num3 += 1;
        Label_013C:
            if (num3 < this.mMoveMap.w)
            {
                goto Label_00AC;
            }
            if (flag2 == null)
            {
                goto Label_015A;
            }
            goto Label_016E;
        Label_015A:
            num2 += 1;
        Label_0160:
            if (num2 < list.Count)
            {
                goto Label_0069;
            }
        Label_016E:
            return grid;
        }

        private unsafe void GetExecuteSkillLineTarget(Unit self, int target_x, int target_y, SkillData skill, ref List<Unit> targets, ref ShotTarget shot)
        {
            BattleMap map;
            Unit unit;
            List<Unit> list;
            int num;
            Unit unit2;
            double num2;
            List<ShotTarget> list2;
            Grid grid;
            Grid grid2;
            double num3;
            int num4;
            int num5;
            int num6;
            double num7;
            double num8;
            double num9;
            int num10;
            double num11;
            double num12;
            double num13;
            double num14;
            double num15;
            double num16;
            double num17;
            double num18;
            double num19;
            ShotTarget target;
            int num20;
            int num21;
            int num22;
            int num23;
            double num24;
            double num25;
            double num26;
            double num27;
            double num28;
            double num29;
            double num30;
            double num31;
            Unit unit3;
            Unit unit4;
            int num32;
            int num33;
            int num34;
            int num35;
            Unit unit5;
            Unit unit6;
            Grid grid3;
            List<Unit> list3;
            int num36;
            ELineType type;
            if (*(targets) != null)
            {
                goto Label_001B;
            }
            *(targets) = new List<Unit>(this.Enemys.Count);
        Label_001B:
            *(shot) = null;
            this.mGridLines.Clear();
            map = this.CurrentMap;
            if (((self.x == target_x) && (self.y == target_y)) || (skill.CheckGridLineSkill() == null))
            {
                goto Label_0866;
            }
            if ((skill.CheckUnitSkillTarget() == null) || (self.CastSkill == skill))
            {
                goto Label_0107;
            }
            if (skill.IsAreaSkill() != null)
            {
                goto Label_00C6;
            }
            unit = this.FindUnitAtGrid(map[target_x, target_y]);
            if (unit != null)
            {
                goto Label_00B1;
            }
            unit = this.FindGimmickAtGrid(map[target_x, target_y], 0, null);
            if (this.IsTargetBreakUnit(self, unit, skill) != null)
            {
                goto Label_00B1;
            }
            unit = null;
        Label_00B1:
            if (this.CheckSkillTarget(self, unit, skill) != null)
            {
                goto Label_0107;
            }
            return;
            goto Label_0107;
        Label_00C6:
            this.CreateScopeGridMap(self, self.x, self.y, target_x, target_y, skill, &this.mScopeMap, 0);
            list = this.SearchTargetsInGridMap(self, skill, this.mScopeMap);
            if ((list != null) && (list.Count != null))
            {
                goto Label_0107;
            }
            return;
        Label_0107:
            this.GetSkillGridLines(self, target_x, target_y, skill, &this.mGridLines);
            switch ((skill.LineType - 1))
            {
                case 0:
                    goto Label_013B;

                case 1:
                    goto Label_0268;

                case 2:
                    goto Label_013B;
            }
            goto Label_07F9;
        Label_013B:
            if (*(shot) != null)
            {
                goto Label_014B;
            }
            *(shot) = new ShotTarget();
        Label_014B:
            *(shot).end = map[target_x, target_y];
            num = 0;
            goto Label_0252;
        Label_0162:
            unit2 = this.FindUnitAtGrid(this.mGridLines[num]);
            if (unit2 != null)
            {
                goto Label_01EC;
            }
            unit2 = this.FindGimmickAtGrid(this.mGridLines[num], 0, null);
            if (((unit2 == null) || (unit2.IsBreakObj == null)) || ((unit2.BreakObjClashType != 3) || (unit2.BreakObjRayType != 1)))
            {
                goto Label_01D9;
            }
            *(shot).end = this.mGridLines[num];
            goto Label_0263;
        Label_01D9:
            if (this.IsTargetBreakUnit(self, unit2, skill) != null)
            {
                goto Label_01EC;
            }
            unit2 = null;
        Label_01EC:
            if ((unit2 == null) || ((unit2.IsJump != null) && (skill.SkillParam.TargetEx == null)))
            {
                goto Label_024E;
            }
            *(shot).piercers.Add(unit2);
            *(targets).Add(unit2);
            if (skill.IsPierce() != null)
            {
                goto Label_024E;
            }
            *(shot).end = this.mGridLines[num];
            goto Label_0263;
        Label_024E:
            num += 1;
        Label_0252:
            if (num < this.mGridLines.Count)
            {
                goto Label_0162;
            }
        Label_0263:
            goto Label_07FE;
        Label_0268:
            num2 = (double) (self.GetAttackHeight() + 2);
            list2 = new List<ShotTarget>();
            grid = map[self.x, self.y];
            grid2 = map[target_x, target_y];
            num3 = 0.0;
            if (num3 >= ((double) grid.height))
            {
                goto Label_02BC;
            }
            num3 = (double) grid.height;
        Label_02BC:
            num4 = 0;
            goto Label_02F9;
        Label_02C4:
            if (num3 >= ((double) this.mGridLines[num4].height))
            {
                goto Label_02F3;
            }
            num3 = (double) this.mGridLines[num4].height;
        Label_02F3:
            num4 += 1;
        Label_02F9:
            if (num4 < this.mGridLines.Count)
            {
                goto Label_02C4;
            }
            if (num3 > ((double) grid.height))
            {
                goto Label_0328;
            }
            num3 += 1.0;
        Label_0328:
            num3 += 1.0;
            num5 = grid2.x - grid.x;
            num6 = grid2.y - grid.y;
            num7 = (double) Sqrt((num5 * num5) + (num6 * num6));
            num8 = (double) (grid.height - grid2.height);
            num9 = 9.8;
            num10 = 0;
            goto Label_06AB;
        Label_0390:
            num11 = num3 + ((double) num10);
            num12 = (2.0 * num9) * (num11 - num8);
            num13 = (2.0 * num9) * num11;
            num12 = (num12 <= 0.0) ? 0.0 : Math.Sqrt(num12);
            num13 = (num13 <= 0.0) ? 0.0 : Math.Sqrt(num13);
            num14 = (num12 + num13) / num9;
            num15 = Math.Pow(num7 / num14, 2.0) + ((2.0 * num9) * (num11 - num8));
            num16 = (num15 <= 0.0) ? 0.0 : Math.Sqrt(num15);
            num17 = (num14 * num12) / num7;
            num17 = Math.Atan(num17);
            num18 = (num17 * 180.0) / 3.1415926535897931;
            num19 = num14 / num7;
            target = new ShotTarget();
            target.rad = num18;
            target.height = num11;
            target.end = grid2;
            num20 = BattleMap.MAP_FLOOR_HEIGHT / 2;
            num21 = 0;
            goto Label_068A;
        Label_04C9:
            num22 = this.mGridLines[num21].x - grid.x;
            num23 = this.mGridLines[num21].y - grid.y;
            num24 = Math.Min((double) Sqrt((num22 * num22) + (num23 * num23)), num7);
            num25 = num19 * num24;
            num26 = Math.Sin(num17);
            num27 = Math.Pow(num25, 2.0);
            num28 = (num9 * num27) * 0.5;
            num29 = ((num16 * num25) * num26) - num28;
            num30 = ((double) (this.mGridLines[num21].height - grid.height)) - 0.01;
            num31 = num30 + ((double) num20);
            if (num29 >= num30)
            {
                goto Label_0598;
            }
            goto Label_069C;
        Label_0598:
            if (num29 >= num31)
            {
                goto Label_0684;
            }
            unit3 = this.FindUnitAtGrid(this.mGridLines[num21]);
            if (unit3 != null)
            {
                goto Label_062D;
            }
            unit3 = this.FindGimmickAtGrid(this.mGridLines[num21], 0, null);
            if (((unit3 == null) || (unit3.IsBreakObj == null)) || ((unit3.BreakObjClashType != 3) || (unit3.BreakObjRayType != 1)))
            {
                goto Label_061A;
            }
            target.end = this.mGridLines[num21];
            goto Label_069C;
        Label_061A:
            if (this.IsTargetBreakUnit(self, unit3, skill) != null)
            {
                goto Label_062D;
            }
            unit3 = null;
        Label_062D:
            if ((unit3 == null) || ((unit3.IsJump != null) && (skill.SkillParam.TargetEx == null)))
            {
                goto Label_0684;
            }
            target.piercers.Add(unit3);
            if (skill.IsPierce() != null)
            {
                goto Label_0684;
            }
            target.end = this.mGridLines[num21];
            goto Label_069C;
        Label_0684:
            num21 += 1;
        Label_068A:
            if (num21 < this.mGridLines.Count)
            {
                goto Label_04C9;
            }
        Label_069C:
            list2.Add(target);
            num10 += 1;
        Label_06AB:
            if (((double) num10) <= num2)
            {
                goto Label_0390;
            }
            unit4 = this.FindUnitAtGrid(grid2);
            if (unit4 != null)
            {
                goto Label_06E5;
            }
            unit4 = this.FindGimmickAtGrid(grid2, 0, null);
            if (this.IsTargetBreakUnit(self, unit4, skill) != null)
            {
                goto Label_06E5;
            }
            unit4 = null;
        Label_06E5:
            if ((unit4 == null) || ((unit4.IsJump != null) && (skill.SkillParam.TargetEx == null)))
            {
                goto Label_079A;
            }
            num32 = 0;
            goto Label_078C;
        Label_0711:
            if (list2[num32].piercers.Contains(unit4) != null)
            {
                goto Label_0730;
            }
            goto Label_0786;
        Label_0730:
            num33 = 0;
            goto Label_075B;
        Label_0738:
            *(targets).Add(list2[num32].piercers[num33]);
            num33 += 1;
        Label_075B:
            if (num33 < list2[num32].piercers.Count)
            {
                goto Label_0738;
            }
            *(shot) = list2[num32];
            goto Label_079A;
        Label_0786:
            num32 += 1;
        Label_078C:
            if (num32 < list2.Count)
            {
                goto Label_0711;
            }
        Label_079A:
            if (*(shot) != null)
            {
                goto Label_07FE;
            }
            *(shot) = list2[0];
            num34 = 0;
            goto Label_07E6;
        Label_07B5:
            if (grid2 == list2[num34].end)
            {
                goto Label_07CF;
            }
            goto Label_07E0;
        Label_07CF:
            *(shot) = list2[num34];
            goto Label_07F4;
        Label_07E0:
            num34 += 1;
        Label_07E6:
            if (num34 < list2.Count)
            {
                goto Label_07B5;
            }
        Label_07F4:
            goto Label_07FE;
        Label_07F9:;
        Label_07FE:
            num35 = 0;
            goto Label_0852;
        Label_0806:
            unit5 = *(targets)[num35];
            if (((unit5.x != target_x) || (unit5.y != target_y)) || (this.CheckSkillTarget(self, unit5, skill) != null))
            {
                goto Label_084C;
            }
            *(targets).RemoveAt(num35);
            num35 -= 1;
        Label_084C:
            num35 += 1;
        Label_0852:
            if (num35 < *(targets).Count)
            {
                goto Label_0806;
            }
            goto Label_08D6;
        Label_0866:
            unit6 = this.FindUnitAtGrid(map[target_x, target_y]);
            if (unit6 != null)
            {
                goto Label_08A2;
            }
            unit6 = this.FindGimmickAtGrid(map[target_x, target_y], 0, null);
            if (this.IsTargetBreakUnit(self, unit6, skill) != null)
            {
                goto Label_08A2;
            }
            unit6 = null;
        Label_08A2:
            if (((unit6 == null) || (this.CheckSkillTarget(self, unit6, skill) == null)) || (skill.SkillParam.select_scope == 14))
            {
                goto Label_08D6;
            }
            *(targets).Add(unit6);
        Label_08D6:
            if (skill.IsAreaSkill() == null)
            {
                goto Label_0985;
            }
            grid3 = (*(shot) != null) ? *(shot).end : map[target_x, target_y];
            this.CreateScopeGridMap(self, self.x, self.y, grid3.x, grid3.y, skill, &this.mScopeMap, 0);
            list3 = this.SearchTargetsInGridMap(self, skill, this.mScopeMap);
            num36 = 0;
            goto Label_0977;
        Label_0945:
            if (*(targets).Contains(list3[num36]) == null)
            {
                goto Label_0960;
            }
            goto Label_0971;
        Label_0960:
            *(targets).Add(list3[num36]);
        Label_0971:
            num36 += 1;
        Label_0977:
            if (num36 < list3.Count)
            {
                goto Label_0945;
            }
        Label_0985:
            return;
        }

        private int GetFailCondSelfSideUnitCount(Unit self, EUnitCondition condition)
        {
            int num;
            int num2;
            Unit unit;
            num = 0;
            num2 = 0;
            goto Label_0099;
        Label_0009:
            unit = this.mUnits[num2];
            if (this.mUnits[num2].IsSub != null)
            {
                goto Label_0095;
            }
            if (this.mUnits[num2].IsEntry == null)
            {
                goto Label_0095;
            }
            if (this.mUnits[num2].IsDead != null)
            {
                goto Label_0095;
            }
            if (this.mUnits[num2].IsGimmick == null)
            {
                goto Label_0073;
            }
            goto Label_0095;
        Label_0073:
            if (this.CheckEnemySide(self, unit) == null)
            {
                goto Label_0085;
            }
            goto Label_0095;
        Label_0085:
            if (unit.IsUnitCondition(condition) == null)
            {
                goto Label_0095;
            }
            num += 1;
        Label_0095:
            num2 += 1;
        Label_0099:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        private int GetFailTrickPriority(Unit self, Grid movpos)
        {
            int num;
            SRPG.TrickMap.Data data;
            int num2;
            float num3;
            num = 0;
            if (self == null)
            {
                goto Label_000E;
            }
            if (movpos != null)
            {
                goto Label_0010;
            }
        Label_000E:
            return num;
        Label_0010:
            data = this.mTrickMap.GetData(movpos.x, movpos.y);
            if (data != null)
            {
                goto Label_0030;
            }
            return num;
        Label_0030:
            if (data.IsVisual(self) == null)
            {
                goto Label_00C3;
            }
            if (data.IsVaild(self) == null)
            {
                goto Label_00C3;
            }
            if (data.IsFail(self) == null)
            {
                goto Label_00C3;
            }
            if (data.IsDamage() == null)
            {
                goto Label_009D;
            }
            num3 = ((float) data.CalcDamage(self)) / ((float) self.MaximumStatus.param.hp);
            if (num3 < 1f)
            {
                goto Label_0092;
            }
            num3 = 1f;
        Label_0092:
            num += (int) (num3 * 100f);
        Label_009D:
            if (data.IsCondEffect() == null)
            {
                goto Label_00AC;
            }
            num += 1;
        Label_00AC:
            if (data.IsBuffEffect() == null)
            {
                goto Label_00BB;
            }
            num += 1;
        Label_00BB:
            if (num != null)
            {
                goto Label_00C3;
            }
            num = 1;
        Label_00C3:
            return num;
        }

        public List<int> GetFinishHp(EUnitSide side)
        {
            List<int> list;
            int num;
            List<Unit> list2;
            int num2;
            list = new List<int>();
            if (side != null)
            {
                goto Label_0053;
            }
            num = 0;
            goto Label_003D;
        Label_0013:
            list.Add(this.mPlayer[num].CurrentStatus.param.hp);
            num += 1;
        Label_003D:
            if (num < this.mPlayer.Count)
            {
                goto Label_0013;
            }
            goto Label_00B2;
        Label_0053:
            if (side != 1)
            {
                goto Label_00B2;
            }
            list2 = this.mEnemys[this.MapIndex];
            num2 = 0;
            goto Label_00A6;
        Label_006F:
            if (list2[num2].Side != 1)
            {
                goto Label_00A2;
            }
            list.Add(list2[num2].CurrentStatus.param.hp);
        Label_00A2:
            num2 += 1;
        Label_00A6:
            if (num2 < list2.Count)
            {
                goto Label_006F;
            }
        Label_00B2:
            return list;
        }

        public int GetGems(Unit unit)
        {
            return unit.Gems;
        }

        private unsafe int GetGoodTrickPriority(Unit self, Grid movpos, ref int heal_trick)
        {
            int num;
            SRPG.TrickMap.Data data;
            float num2;
            int num3;
            num = 0;
            if (self == null)
            {
                goto Label_000E;
            }
            if (movpos != null)
            {
                goto Label_0010;
            }
        Label_000E:
            return num;
        Label_0010:
            data = this.mTrickMap.GetData(movpos.x, movpos.y);
            if (data != null)
            {
                goto Label_0030;
            }
            return num;
        Label_0030:
            if (data.IsVisual(self) == null)
            {
                goto Label_00DE;
            }
            if (data.IsVaild(self) == null)
            {
                goto Label_00DE;
            }
            if (data.IsFail(self) != null)
            {
                goto Label_00DE;
            }
            num = 1;
            if (data.IsHeal() == null)
            {
                goto Label_00C9;
            }
            num2 = ((float) self.CurrentStatus.param.hp) / ((float) self.MaximumStatus.param.hp);
            if (num2 >= 0.6f)
            {
                goto Label_00C9;
            }
            num2 = ((float) data.CalcHeal(self)) / ((float) self.MaximumStatus.param.hp);
            *((int*) heal_trick) += (int) (num2 * 100f);
        Label_00C9:
            if (data.IsBuffEffect() == null)
            {
                goto Label_00DE;
            }
            num += data.GetBuffPriority(self);
        Label_00DE:
            return num;
        }

        private Grid getGridKnockBack(Unit unit_att, Unit unit_def, SkillData skill, int gx, int gy, int kb_val, int kb_dir)
        {
            SceneBattle battle;
            int num;
            TacticsUnitController controller;
            eKnockBackDs ds;
            eKnockBackDir dir;
            battle = SceneBattle.Instance;
            if ((battle == null) == null)
            {
                goto Label_0014;
            }
            return null;
        Label_0014:
            if (this.isKnockBack(skill) != null)
            {
                goto Label_0022;
            }
            return null;
        Label_0022:
            if (unit_def.IsKnockBack != null)
            {
                goto Label_002F;
            }
            return null;
        Label_002F:
            num = this.unitDirectionFromPos(unit_att.x, unit_att.y, unit_def.x, unit_def.y);
            ds = skill.KnockBackDs;
            if (ds == 1)
            {
                goto Label_0068;
            }
            if (ds == 2)
            {
                goto Label_0088;
            }
            goto Label_00A4;
        Label_0068:
            controller = battle.FindUnitController(unit_att);
            if ((controller != null) == null)
            {
                goto Label_00A4;
            }
            num = controller.CalcUnitDirectionFromRotation();
            goto Label_00A4;
        Label_0088:
            num = this.unitDirectionFromPos(gx, gy, unit_def.x, unit_def.y);
        Label_00A4:
            switch ((skill.KnockBackDir - 1))
            {
                case 0:
                    goto Label_00C6;

                case 1:
                    goto Label_00D3;

                case 2:
                    goto Label_00E0;
            }
            goto Label_00ED;
        Label_00C6:
            num = Unit.ReverseDirection[num];
            goto Label_00ED;
        Label_00D3:
            num = leftDirection[num];
            goto Label_00ED;
        Label_00E0:
            num = rightDirection[num];
        Label_00ED:
            if (kb_val > 0)
            {
                goto Label_0102;
            }
            kb_val = skill.KnockBackVal;
        Label_0102:
            if (kb_dir < 0)
            {
                goto Label_010D;
            }
            num = kb_dir;
        Label_010D:
            if (skill.KnockBackDs != null)
            {
                goto Label_0128;
            }
            gx = unit_att.x;
            gy = unit_att.y;
        Label_0128:
            return this.GetGridKnockBack(unit_def, num, kb_val, skill, gx, gy);
        }

        public unsafe Grid GetGridKnockBack(Unit target, EUnitDirection dir, int kb_val, SkillData skill, int gx, int gy)
        {
            List<Unit> list;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            int num;
            int num2;
            Grid grid;
            Grid grid2;
            int num3;
            Grid grid3;
            Unit unit2;
            <GetGridKnockBack>c__AnonStorey1BB storeybb;
            storeybb = new <GetGridKnockBack>c__AnonStorey1BB();
            if (target.IsKnockBack != null)
            {
                goto Label_0014;
            }
            return null;
        Label_0014:
            list = new List<Unit>(this.sameJudgeUnitLists.Count);
            enumerator = this.sameJudgeUnitLists.GetEnumerator();
        Label_0031:
            try
            {
                goto Label_0055;
            Label_0036:
                unit = &enumerator.Current;
                if (unit.IsDead != null)
                {
                    goto Label_004E;
                }
                goto Label_0055;
            Label_004E:
                list.Add(unit);
            Label_0055:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0036;
                }
                goto Label_0072;
            }
            finally
            {
            Label_0066:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0072:
            num = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.KnockBackHeight;
            num2 = dir;
            storeybb.ux = target.x;
            storeybb.uy = target.y;
            grid = null;
            grid2 = this.CurrentMap[storeybb.ux, storeybb.uy];
            if (grid2 == null)
            {
                goto Label_01EE;
            }
            num3 = 0;
            goto Label_01E6;
        Label_00D6:
            storeybb.ux += Unit.DIRECTION_OFFSETS[num2, 0];
            storeybb.uy += Unit.DIRECTION_OFFSETS[num2, 1];
            grid3 = this.CurrentMap[storeybb.ux, storeybb.uy];
            if (grid3 != null)
            {
                goto Label_0135;
            }
            goto Label_01EE;
        Label_0135:
            if (Math.Abs(grid2.height - grid3.height) <= num)
            {
                goto Label_0154;
            }
            goto Label_01EE;
        Label_0154:
            if (this.CurrentMap.CheckEnableMove(target, grid3, 0, 0) != null)
            {
                goto Label_016E;
            }
            goto Label_01EE;
        Label_016E:
            if (list.Find(new Predicate<Unit>(storeybb.<>m__62)) == null)
            {
                goto Label_018F;
            }
            goto Label_01EE;
        Label_018F:
            grid = grid3;
            if (skill == null)
            {
                goto Label_01E0;
            }
            if (skill.KnockBackDir != 1)
            {
                goto Label_01E0;
            }
            if (skill.KnockBackDs == null)
            {
                goto Label_01C0;
            }
            if (skill.KnockBackDs != 2)
            {
                goto Label_01E0;
            }
        Label_01C0:
            num2 = Unit.ReverseDirection[this.unitDirectionFromPos(gx, gy, storeybb.ux, storeybb.uy)];
        Label_01E0:
            num3 += 1;
        Label_01E6:
            if (num3 < kb_val)
            {
                goto Label_00D6;
            }
        Label_01EE:
            return grid;
        }

        private void GetGridOnLine(Grid start, Grid target, ref List<Grid> results)
        {
            this.GetGridOnLine(start, target.x, target.y, results);
            return;
        }

        private void GetGridOnLine(Grid start, int tx, int ty, ref List<Grid> results)
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            Grid grid;
            int num8;
            int num9;
            int num10;
            int num11;
            <GetGridOnLine>c__AnonStorey1B9 storeyb;
            storeyb = new <GetGridOnLine>c__AnonStorey1B9();
            storeyb.start = start;
            storeyb.<>f__this = this;
            map = this.CurrentMap;
            *(results).Clear();
            num = 100;
            num2 = storeyb.start.x * num;
            num3 = storeyb.start.y * num;
            num4 = tx * num;
            num5 = ty * num;
            num6 = 0;
            goto Label_0184;
        Label_0059:
            num7 = 0;
            goto Label_0171;
        Label_0061:
            grid = map[num6, num7];
            if (grid != storeyb.start)
            {
                goto Label_0080;
            }
            goto Label_016B;
        Label_0080:
            num8 = (grid.y * num) + 0x2d;
            num9 = (grid.y * num) - 0x2d;
            num10 = (grid.x * num) - 0x2d;
            num11 = (grid.x * num) + 0x2d;
            if (Math.Min(num10, num11) > Math.Max(num2, num4))
            {
                goto Label_016B;
            }
            if (Math.Min(num8, num9) > Math.Max(num3, num5))
            {
                goto Label_016B;
            }
            if (Math.Min(num2, num4) > Math.Max(num10, num11))
            {
                goto Label_016B;
            }
            if (Math.Min(num3, num5) <= Math.Max(num8, num9))
            {
                goto Label_0115;
            }
            goto Label_016B;
        Label_0115:
            if (this.CheckGridOnLine(num10, num8, num11, num9, num2, num3, num4, num5) != null)
            {
                goto Label_0147;
            }
            if (this.CheckGridOnLine(num11, num8, num10, num9, num2, num3, num4, num5) == null)
            {
                goto Label_016B;
            }
        Label_0147:
            if (this.mGridLines.Contains(grid) != null)
            {
                goto Label_016B;
            }
            this.mGridLines.Add(grid);
        Label_016B:
            num7 += 1;
        Label_0171:
            if (num7 < map.Height)
            {
                goto Label_0061;
            }
            num6 += 1;
        Label_0184:
            if (num6 < map.Width)
            {
                goto Label_0059;
            }
            MySort<Grid>.Sort(*(results), new Comparison<Grid>(storeyb.<>m__60));
            return;
        }

        private Unit GetGuardMan(Unit self)
        {
            int num;
            num = 0;
            goto Label_002F;
        Label_0007:
            if (this.mUnits[num].GetGuardTarget() != self)
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

        private List<Unit> GetHealer(Unit self)
        {
            List<Unit> list;
            int num;
            int num2;
            int num3;
            Unit unit;
            int num4;
            SkillData data;
            SkillData data2;
            int num5;
            <GetHealer>c__AnonStorey1C6 storeyc;
            storeyc = new <GetHealer>c__AnonStorey1C6();
            storeyc.self = self;
            storeyc.<>f__this = this;
            list = new List<Unit>();
            num = storeyc.self.CurrentStatus.param.hp;
            num2 = storeyc.self.MaximumStatus.param.hp;
            num3 = 0;
            goto Label_01C3;
        Label_005C:
            unit = this.mUnits[num3];
            if (((unit.IsDead != null) || (unit.IsEntry == null)) || ((unit.IsGimmick != null) || (unit.IsSub != null)))
            {
                goto Label_01BF;
            }
            if (unit != storeyc.self)
            {
                goto Label_00AD;
            }
            goto Label_01BF;
        Label_00AD:
            if (unit.IsEnableSkillCondition(1) != null)
            {
                goto Label_00BF;
            }
            goto Label_01BF;
        Label_00BF:
            if (this.CheckEnemySide(storeyc.self, unit) == null)
            {
                goto Label_00D8;
            }
            goto Label_01BF;
        Label_00D8:
            num4 = 0;
            goto Label_01AC;
        Label_00E0:
            data = unit.BattleSkills[num4];
            if (data != null)
            {
                goto Label_00FC;
            }
            goto Label_01A6;
        Label_00FC:
            data2 = storeyc.self.GetSkillForUseCount(data.SkillID, 0);
            data = (data2 != null) ? data2 : data;
            if (data.IsHealSkill() != null)
            {
                goto Label_0135;
            }
            goto Label_01A6;
        Label_0135:
            if (this.CheckEnableUseSkill(unit, data, 1) == null)
            {
                goto Label_01A6;
            }
            if (this.IsUseSkillCollabo(unit, data) != null)
            {
                goto Label_0159;
            }
            goto Label_01A6;
        Label_0159:
            if (this.CheckSkillTargetAI(unit, storeyc.self, data) != null)
            {
                goto Label_0174;
            }
            goto Label_01A6;
        Label_0174:
            num5 = num2 * unit.AI.heal_border;
            if (num5 >= (num * 100))
            {
                goto Label_0199;
            }
            goto Label_01A6;
        Label_0199:
            list.Add(unit);
            goto Label_01BF;
        Label_01A6:
            num4 += 1;
        Label_01AC:
            if (num4 < unit.BattleSkills.Count)
            {
                goto Label_00E0;
            }
        Label_01BF:
            num3 += 1;
        Label_01C3:
            if (num3 < this.mUnits.Count)
            {
                goto Label_005C;
            }
            if (list.Count <= 0)
            {
                goto Label_01F3;
            }
            MySort<Unit>.Sort(list, new Comparison<Unit>(storeyc.<>m__6D));
        Label_01F3:
            return list;
        }

        private int GetHealUnitCount(Unit self)
        {
            int num;
            AIParam param;
            int num2;
            Unit unit;
            int num3;
            int num4;
            int num5;
            num = 0;
            param = self.AI;
            num2 = 0;
            goto Label_00E8;
        Label_0010:
            unit = this.mUnits[num2];
            if (unit.IsDead != null)
            {
                goto Label_00E4;
            }
            if (unit.IsEntry == null)
            {
                goto Label_00E4;
            }
            if (unit.IsSub == null)
            {
                goto Label_0043;
            }
            goto Label_00E4;
        Label_0043:
            if (this.CheckEnemySide(self, unit) == null)
            {
                goto Label_0078;
            }
            if (unit.IsGimmick == null)
            {
                goto Label_00E4;
            }
            if (this.IsTargetBreakUnit(self, unit, null) != null)
            {
                goto Label_0078;
            }
            goto Label_00E4;
            goto Label_0078;
            goto Label_00E4;
        Label_0078:
            num3 = unit.CurrentStatus.param.hp;
            num4 = unit.MaximumStatus.param.hp;
            if (param == null)
            {
                goto Label_00D2;
            }
            num5 = num4 * param.heal_border;
            if (num5 >= (num3 * 100))
            {
                goto Label_00E0;
            }
            goto Label_00E4;
            goto Label_00E0;
        Label_00D2:
            if (num3 != num4)
            {
                goto Label_00E0;
            }
            goto Label_00E4;
        Label_00E0:
            num += 1;
        Label_00E4:
            num2 += 1;
        Label_00E8:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0010;
            }
            return num;
        }

        private int GetHpCost(Unit self, SkillData skill)
        {
            int num;
            int num2;
            int num3;
            if (skill.IsSuicide() == null)
            {
                goto Label_0021;
            }
            return self.MaximumStatus.param.hp;
        Label_0021:
            num = skill.GetHpCost(self);
            num2 = skill.HpCostRate;
            if (num2 != null)
            {
                goto Label_0038;
            }
            return 0;
        Label_0038:
            if (num2 >= 100)
            {
                goto Label_00A9;
            }
            num3 = this.GetRandom() % 100;
            if (num3 <= num2)
            {
                goto Label_0053;
            }
            return 0;
        Label_0053:
            if (self.CurrentStatus.param.hp <= 1)
            {
                goto Label_00A7;
            }
            return ((self.CurrentStatus.param.hp <= num) ? (self.CurrentStatus.param.hp - 1) : num);
        Label_00A7:
            return 0;
        Label_00A9:
            return num;
        }

        private int GetHpRate(EUnitSide side)
        {
            int num;
            int num2;
            int num3;
            int num4;
            List<Unit> list;
            int num5;
            num = 0;
            num2 = 0;
            num3 = 0;
            if (side != null)
            {
                goto Label_0073;
            }
            num4 = 0;
            goto Label_005D;
        Label_0013:
            num += this.mPlayer[num4].MaximumStatus.param.hp;
            num2 += this.mPlayer[num4].CurrentStatus.param.hp;
            num4 += 1;
        Label_005D:
            if (num4 < this.mPlayer.Count)
            {
                goto Label_0013;
            }
            goto Label_00F9;
        Label_0073:
            if (side != 1)
            {
                goto Label_00F9;
            }
            list = this.mEnemys[this.MapIndex];
            num5 = 0;
            goto Label_00EB;
        Label_0091:
            if (list[num5].Side != 1)
            {
                goto Label_00E5;
            }
            num += list[num5].MaximumStatus.param.hp;
            num2 += list[num5].CurrentStatus.param.hp;
        Label_00E5:
            num5 += 1;
        Label_00EB:
            if (num5 < list.Count)
            {
                goto Label_0091;
            }
        Label_00F9:
            if (num <= 0)
            {
                goto Label_0107;
            }
            num3 = (num2 * 100) / num;
        Label_0107:
            return num3;
        }

        public List<string> GetPlayerName()
        {
            List<string> list;
            int num;
            list = new List<string>();
            num = 0;
            goto Label_0048;
        Label_000D:
            list.Add(this.mPlayer[num].UnitName + "_" + ((int) this.mPlayer[num].OwnerPlayerIndex));
            num += 1;
        Label_0048:
            if (num < this.mPlayer.Count)
            {
                goto Label_000D;
            }
            return list;
        }

        private int GetPlayerUnitDeadCount()
        {
            int num;
            int num2;
            Unit unit;
            num = 0;
            num2 = 0;
            goto Label_004F;
        Label_0009:
            unit = this.Units[num2];
            if (unit.Side != null)
            {
                goto Label_004B;
            }
            if (unit.IsPartyMember == null)
            {
                goto Label_004B;
            }
            if (unit.IsDead == null)
            {
                goto Label_004B;
            }
            if (unit.IsUnitFlag(0x80000) != null)
            {
                goto Label_004B;
            }
            num += 1;
        Label_004B:
            num2 += 1;
        Label_004F:
            if (num2 < this.Units.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public QuestParam GetQuest()
        {
            return MonoSingleton<GameManager>.Instance.FindQuest(this.QuestID);
        }

        public Record GetQuestRecord()
        {
            return this.mRecord;
        }

        public QuestResult GetQuestResult()
        {
            BattleMap map;
            int num;
            int num2;
            int num3;
            bool flag;
            int num4;
            Unit unit;
            bool flag2;
            int num5;
            Unit unit2;
            int num6;
            int num7;
            map = this.CurrentMap;
            if (this.CheckMonitorGoalUnit(map.WinMonitorCondition) == null)
            {
                goto Label_001A;
            }
            return 1;
        Label_001A:
            if (this.CheckMonitorGoalUnit(map.LoseMonitorCondition) == null)
            {
                goto Label_002D;
            }
            return 2;
        Label_002D:
            if (this.CheckMonitorActionCount(map.WinMonitorCondition) == null)
            {
                goto Label_0040;
            }
            return 1;
        Label_0040:
            if (this.CheckMonitorActionCount(map.LoseMonitorCondition) == null)
            {
                goto Label_0053;
            }
            return 2;
        Label_0053:
            if (this.CheckMonitorWithdrawUnit(map.WinMonitorCondition) == null)
            {
                goto Label_0066;
            }
            return 1;
        Label_0066:
            if (this.CheckMonitorWithdrawUnit(map.LoseMonitorCondition) == null)
            {
                goto Label_0079;
            }
            return 2;
        Label_0079:
            num = this.mWinTriggerCount;
            num2 = this.mLoseTriggerCount;
            num3 = 0;
            goto Label_00C6;
        Label_008E:
            if (this.mUnits[num3].CheckWinEventTrigger() == null)
            {
                goto Label_00A8;
            }
            num += 1;
        Label_00A8:
            if (this.mUnits[num3].CheckLoseEventTrigger() == null)
            {
                goto Label_00C2;
            }
            num2 += 1;
        Label_00C2:
            num3 += 1;
        Label_00C6:
            if (num3 < this.mUnits.Count)
            {
                goto Label_008E;
            }
            if (this.mQuestParam.win == null)
            {
                goto Label_0104;
            }
            if (this.mQuestParam.win > num)
            {
                goto Label_0104;
            }
            return 1;
        Label_0104:
            if (this.IsMultiVersus == null)
            {
                goto Label_015F;
            }
            if (this.IsVSForceWin == null)
            {
                goto Label_0129;
            }
            if (this.IsVSForceWinComfirm == null)
            {
                goto Label_0127;
            }
            return 1;
        Label_0127:
            return 0;
        Label_0129:
            if (this.IsAllDead(0) == null)
            {
                goto Label_0143;
            }
            if (this.IsAllDead(1) == null)
            {
                goto Label_0143;
            }
            return 4;
        Label_0143:
            if (this.IsAllDead(0) == null)
            {
                goto Label_0151;
            }
            return 2;
        Label_0151:
            if (this.IsAllDead(1) == null)
            {
                goto Label_015F;
            }
            return 1;
        Label_015F:
            if (this.mEnemys == null)
            {
                goto Label_01E1;
            }
            flag = 1;
            num4 = 0;
            goto Label_01BF;
        Label_0175:
            unit = this.mEnemys[this.MapIndex][num4];
            if (unit.IsEntry != null)
            {
                goto Label_019C;
            }
            goto Label_01B9;
        Label_019C:
            if (unit.IsGimmick == null)
            {
                goto Label_01AD;
            }
            goto Label_01B9;
        Label_01AD:
            flag &= unit.IsDead;
        Label_01B9:
            num4 += 1;
        Label_01BF:
            if (num4 < this.mEnemys[this.MapIndex].Count)
            {
                goto Label_0175;
            }
            if (flag == null)
            {
                goto Label_01E1;
            }
            return 1;
        Label_01E1:
            if (this.mQuestParam.OverClockTimeWin <= 0)
            {
                goto Label_020F;
            }
            if (this.mClockTimeTotal <= this.mQuestParam.OverClockTimeWin)
            {
                goto Label_020F;
            }
            return 1;
        Label_020F:
            if (this.mQuestParam.lose == null)
            {
                goto Label_023C;
            }
            if (this.mQuestParam.lose > num2)
            {
                goto Label_023C;
            }
            return 2;
        Label_023C:
            if (this.mPlayer == null)
            {
                goto Label_02B0;
            }
            flag2 = 1;
            num5 = 0;
            goto Label_0295;
        Label_0252:
            unit2 = this.mPlayer[num5];
            if (unit2.IsEntry != null)
            {
                goto Label_0272;
            }
            goto Label_028F;
        Label_0272:
            if (unit2.IsGimmick == null)
            {
                goto Label_0283;
            }
            goto Label_028F;
        Label_0283:
            flag2 &= unit2.IsDeadCondition();
        Label_028F:
            num5 += 1;
        Label_0295:
            if (num5 < this.mPlayer.Count)
            {
                goto Label_0252;
            }
            if (flag2 == null)
            {
                goto Label_02B0;
            }
            return 2;
        Label_02B0:
            if (this.mQuestParam.OverClockTimeLose <= 0)
            {
                goto Label_02DE;
            }
            if (this.mClockTimeTotal <= this.mQuestParam.OverClockTimeLose)
            {
                goto Label_02DE;
            }
            return 2;
        Label_02DE:
            if (this.mQuestParam.type != 2)
            {
                goto Label_030E;
            }
            if (this.mArenaActionCount != null)
            {
                goto Label_02FC;
            }
            return 2;
        Label_02FC:
            if (this.mIsArenaSkip == null)
            {
                goto Label_030E;
            }
            return this.mArenaCalcResult;
        Label_030E:
            if (this.IsMultiVersus == null)
            {
                goto Label_034E;
            }
            if (this.RemainVersusTurnCount != null)
            {
                goto Label_034E;
            }
            num6 = this.GetHpRate(0);
            num7 = this.GetHpRate(1);
            if (num6 <= num7)
            {
                goto Label_0341;
            }
            return 1;
        Label_0341:
            if (num7 <= num6)
            {
                goto Label_034C;
            }
            return 2;
        Label_034C:
            return 4;
        Label_034E:
            return 0;
        }

        public uint GetRandom()
        {
            return this.CurrentRand.Get();
        }

        public RankingQuestParam GetRankingQuestParam()
        {
            return this.mRankingQuestParam;
        }

        public int GetRemainingActionCount(QuestMonitorCondition cond)
        {
            int num;
            int num2;
            UnitMonitorCondition condition;
            int num3;
            int num4;
            int num5;
            Unit unit;
            int num6;
            if ((cond != null) && (cond.actions.Count != null))
            {
                goto Label_0018;
            }
            return -1;
        Label_0018:
            num = 0xff;
            num2 = 0;
            goto Label_0245;
        Label_0025:
            condition = cond.actions[num2];
            if (string.IsNullOrEmpty(condition.tag) != null)
            {
                goto Label_01C0;
            }
            if ((condition.tag == "pall") == null)
            {
                goto Label_00AD;
            }
            num3 = 0;
            goto Label_009C;
        Label_005E:
            if (this.mUnits[num3].Side == null)
            {
                goto Label_0079;
            }
            goto Label_0098;
        Label_0079:
            num = Math.Min(num, condition.turn - this.mUnits[num3].ActionCount);
        Label_0098:
            num3 += 1;
        Label_009C:
            if (num3 < this.mUnits.Count)
            {
                goto Label_005E;
            }
        Label_00AD:
            if ((condition.tag == "eall") == null)
            {
                goto Label_011F;
            }
            num4 = 0;
            goto Label_010D;
        Label_00CA:
            if (this.mUnits[num4].Side == 1)
            {
                goto Label_00E7;
            }
            goto Label_0107;
        Label_00E7:
            num = Math.Min(num, condition.turn - this.mUnits[num4].ActionCount);
        Label_0107:
            num4 += 1;
        Label_010D:
            if (num4 < this.mUnits.Count)
            {
                goto Label_00CA;
            }
        Label_011F:
            if ((condition.tag == "nall") == null)
            {
                goto Label_0191;
            }
            num5 = 0;
            goto Label_017F;
        Label_013C:
            if (this.mUnits[num5].Side == 2)
            {
                goto Label_0159;
            }
            goto Label_0179;
        Label_0159:
            num = Math.Min(num, condition.turn - this.mUnits[num5].ActionCount);
        Label_0179:
            num5 += 1;
        Label_017F:
            if (num5 < this.mUnits.Count)
            {
                goto Label_013C;
            }
        Label_0191:
            unit = this.FindUnitByUniqueName(condition.tag);
            if (unit == null)
            {
                goto Label_0241;
            }
            num = Math.Min(num, condition.turn - unit.ActionCount);
            goto Label_0241;
        Label_01C0:
            if (string.IsNullOrEmpty(condition.iname) != null)
            {
                goto Label_0241;
            }
            num6 = 0;
            goto Label_022A;
        Label_01D8:
            if ((this.mUnits[num6].UnitParam.iname != condition.iname) == null)
            {
                goto Label_0204;
            }
            goto Label_0224;
        Label_0204:
            num = Math.Min(num, condition.turn - this.mUnits[num6].ActionCount);
        Label_0224:
            num6 += 1;
        Label_022A:
            if (num6 < this.mUnits.Count)
            {
                goto Label_01D8;
            }
        Label_0241:
            num2 += 1;
        Label_0245:
            if (num2 < cond.actions.Count)
            {
                goto Label_0025;
            }
            return ((num == 0xff) ? -1 : Math.Max(num, 0));
        }

        private int GetResistDamageForAttackDetailType(Unit defender, SkillData skill, int damage)
        {
            int num;
            int num2;
            AttackDetailTypes types;
            num = damage;
            num2 = 0;
            if (skill.IsReactionSkill() == null)
            {
                goto Label_0024;
            }
            num2 += defender.CurrentStatus[0x20];
        Label_0024:
            switch ((skill.AttackDetailType - 1))
            {
                case 0:
                    goto Label_0050;

                case 1:
                    goto Label_006A;

                case 2:
                    goto Label_0084;

                case 3:
                    goto Label_009E;

                case 4:
                    goto Label_00B8;

                case 5:
                    goto Label_00D2;
            }
            goto Label_00EC;
        Label_0050:
            num2 += defender.CurrentStatus[0x1b];
            goto Label_00F1;
        Label_006A:
            num2 += defender.CurrentStatus[0x1c];
            goto Label_00F1;
        Label_0084:
            num2 += defender.CurrentStatus[0x1d];
            goto Label_00F1;
        Label_009E:
            num2 += defender.CurrentStatus[30];
            goto Label_00F1;
        Label_00B8:
            num2 += defender.CurrentStatus[0x1f];
            goto Label_00F1;
        Label_00D2:
            num2 += defender.CurrentStatus[0x21];
            goto Label_00F1;
        Label_00EC:;
        Label_00F1:
            if (num2 == null)
            {
                goto Label_0100;
            }
            num = damage - ((damage * num2) / 100);
        Label_0100:
            return num;
        }

        private int GetResistDamageForMhmDamage(Unit attacker, Unit defender, SkillData skill, int damage)
        {
            int num;
            EnchantTypes types;
            EnchantParam param;
            EnchantParam param2;
            int num2;
            num = damage;
            if (defender == null)
            {
                goto Label_000F;
            }
            if (skill != null)
            {
                goto Label_0011;
            }
        Label_000F:
            return num;
        Label_0011:
            if (skill.IsMhmDamage() != null)
            {
                goto Label_001E;
            }
            return num;
        Label_001E:
            types = 0x23;
            if (skill.IsJewelAttack() == null)
            {
                goto Label_002F;
            }
            types = 0x24;
        Label_002F:
            param = attacker.CurrentStatus.enchant_assist;
            param2 = defender.CurrentStatus.enchant_resist;
            num2 = param[types] - param2[types];
            if (num2 == null)
            {
                goto Label_0074;
            }
            num += (damage * num2) / 100;
        Label_0074:
            return Math.Max(num, 0);
        }

        private int GetResistDamageForUnitDefense(Unit defender, SkillData skill, int damage)
        {
            int num;
            int num2;
            EElement element;
            num = damage;
            if (defender == null)
            {
                goto Label_000E;
            }
            if (skill != null)
            {
                goto Label_0010;
            }
        Label_000E:
            return num;
        Label_0010:
            num2 = 0;
            switch ((defender.Element - 1))
            {
                case 0:
                    goto Label_003E;

                case 1:
                    goto Label_0058;

                case 2:
                    goto Label_0072;

                case 3:
                    goto Label_008C;

                case 4:
                    goto Label_00A6;

                case 5:
                    goto Label_00C0;
            }
            goto Label_00DA;
        Label_003E:
            num2 += defender.CurrentStatus[0x2b];
            goto Label_00DA;
        Label_0058:
            num2 += defender.CurrentStatus[0x2c];
            goto Label_00DA;
        Label_0072:
            num2 += defender.CurrentStatus[0x2d];
            goto Label_00DA;
        Label_008C:
            num2 += defender.CurrentStatus[0x2e];
            goto Label_00DA;
        Label_00A6:
            num2 += defender.CurrentStatus[0x2f];
            goto Label_00DA;
        Label_00C0:
            num2 += defender.CurrentStatus[0x30];
        Label_00DA:
            if (num2 == null)
            {
                goto Label_00E9;
            }
            num = damage - ((damage * num2) / 100);
        Label_00E9:
            return num;
        }

        private Grid GetSafePositionAI(Unit self)
        {
            int num;
            BattleMap map;
            int num2;
            bool flag;
            Grid grid;
            Grid grid2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            Grid grid3;
            int num8;
            int num9;
            num = this.GetCurrentEnemyNum(self);
            if (num != null)
            {
                goto Label_0010;
            }
            return null;
        Label_0010:
            map = this.CurrentMap;
            if (((self.IsUnitFlag(2) != null) ? 0 : self.GetMoveCount(0)) != null)
            {
                goto Label_0039;
            }
            return null;
        Label_0039:
            flag = (self.AI == null) ? 0 : self.AI.CheckFlag(0x40);
            grid = map[self.x, self.y];
            grid2 = map[self.x, self.y];
            num4 = Math.Max(this.mSafeMap.get(grid.x, grid.y) + ((self.AI == null) ? 0 : (self.AI.safe_border * num)), 0);
            num5 = 0;
            goto Label_019A;
        Label_00CE:
            num6 = 0;
            goto Label_0182;
        Label_00D6:
            if (this.mMoveMap.get(num5, num6) >= 0)
            {
                goto Label_00F0;
            }
            goto Label_017C;
        Label_00F0:
            if (this.mSafeMap.get(num5, num6) >= num4)
            {
                goto Label_010F;
            }
            goto Label_017C;
        Label_010F:
            grid3 = map[num5, num6];
            if (flag == null)
            {
                goto Label_0134;
            }
            if (this.CheckFriendlyFireOnGridMap(self, grid3) == null)
            {
                goto Label_0134;
            }
            goto Label_017C;
        Label_0134:
            if (this.CheckMove(self, grid3) != null)
            {
                goto Label_0147;
            }
            goto Label_017C;
        Label_0147:
            if (grid2 == null)
            {
                goto Label_0178;
            }
            num8 = this.CalcGridDistance(grid, grid2);
            if (this.CalcGridDistance(grid, grid3) >= num8)
            {
                goto Label_017C;
            }
            grid2 = grid3;
            goto Label_017C;
        Label_0178:
            grid2 = grid3;
        Label_017C:
            num6 += 1;
        Label_0182:
            if (num6 < this.mSafeMap.h)
            {
                goto Label_00D6;
            }
            num5 += 1;
        Label_019A:
            if (num5 < this.mSafeMap.w)
            {
                goto Label_00CE;
            }
            return grid2;
        }

        private bool GetSafePositionEx(Unit self, GridMap<bool> rangeMap, ref SVector2 result)
        {
            int num;
            bool flag;
            int num2;
            int num3;
            int num4;
            num = -2147483648;
            flag = 0;
            num2 = 0;
            goto Label_0072;
        Label_000F:
            num3 = 0;
            goto Label_0062;
        Label_0016:
            if (rangeMap.get(num2, num3) != null)
            {
                goto Label_0028;
            }
            goto Label_005E;
        Label_0028:
            num4 = this.mSafeMapEx.get(num2, num3);
            if (num4 == 0xff)
            {
                goto Label_005E;
            }
            if (num4 <= num)
            {
                goto Label_005E;
            }
            num = num4;
            result.x = num2;
            result.y = num3;
            flag = 1;
        Label_005E:
            num3 += 1;
        Label_0062:
            if (num3 < rangeMap.h)
            {
                goto Label_0016;
            }
            num2 += 1;
        Label_0072:
            if (num2 < rangeMap.w)
            {
                goto Label_000F;
            }
            return flag;
        }

        private bool GetSafePositionEx(Unit self, List<Grid> move_targets, ref Grid result)
        {
            int num;
            bool flag;
            BattleMap map;
            int num2;
            Grid grid;
            int num3;
            num = -2147483648;
            flag = 0;
            map = this.CurrentMap;
            num2 = 0;
            goto Label_0070;
        Label_0016:
            grid = move_targets[num2];
            if (map.CheckEnableMove(self, grid, 0, 0) != null)
            {
                goto Label_0034;
            }
            goto Label_006C;
        Label_0034:
            num3 = this.mSafeMapEx.get(grid.x, grid.y);
            if (num3 == 0xff)
            {
                goto Label_006C;
            }
            if (num3 <= num)
            {
                goto Label_006C;
            }
            num = num3;
            *(result) = grid;
            flag = 1;
        Label_006C:
            num2 += 1;
        Label_0070:
            if (num2 < move_targets.Count)
            {
                goto Label_0016;
            }
            return flag;
        }

        private int GetSafeValue(Unit self, Grid target)
        {
            Grid[] gridArray1;
            int num;
            int num2;
            BattleMap map;
            Grid[] gridArray;
            int num3;
            Grid grid;
            num = this.mSafeMapEx.get(target.x, target.y);
            num2 = 1;
            map = this.CurrentMap;
            gridArray1 = new Grid[] { map[target.x - 1, target.y], map[target.x + 1, target.y], map[target.x, target.y + 1], map[target.x, target.y - 1] };
            gridArray = gridArray1;
            num3 = 0;
            goto Label_00CF;
        Label_008C:
            grid = gridArray[num3];
            if (grid == null)
            {
                goto Label_00C9;
            }
            if (map.CheckEnableMove(self, grid, 0, 0) == null)
            {
                goto Label_00C9;
            }
            num2 += 1;
            num += this.mSafeMapEx.get(grid.x, grid.y);
        Label_00C9:
            num3 += 1;
        Label_00CF:
            if (num3 < ((int) gridArray.Length))
            {
                goto Label_008C;
            }
            return ((num * 10) / num2);
        }

        private int GetSkillEffectValue(Unit self, Unit target, SkillData skill, LogSkill log)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            num = skill.EffectValue;
            if (skill.IsSuicide() == null)
            {
                goto Label_006E;
            }
            num2 = (self.MaximumStatus.param.hp == null) ? 100 : ((100 * self.CurrentStatus.param.hp) / self.MaximumStatus.param.hp);
            num = (num * num2) / 100;
        Label_006E:
            num3 = skill.EffectRange;
            if (num3 == null)
            {
                goto Label_00AA;
            }
            num3 = ((int) (((ulong) this.GetRandom()) % ((long) Math.Abs(num3)))) * ((skill.EffectRange <= 0) ? -1 : 1);
        Label_00AA:
            num4 = (self.MaximumStatus.param.hp == null) ? 100 : ((100 * self.CurrentStatus.param.hp) / self.MaximumStatus.param.hp);
            num5 = (num4 * skill.EffectHpMaxRate) / 100;
            num6 = (self.MaximumStatus.param.mp == null) ? 0 : ((100 * self.CurrentStatus.param.mp) / self.MaximumStatus.param.mp);
            num7 = (num6 * skill.EffectGemsMaxRate) / 100;
            num8 = 0;
            num9 = skill.SkillParam.effect_dead_rate;
            if (num9 == null)
            {
                goto Label_021F;
            }
            num10 = 0;
            goto Label_020D;
        Label_0193:
            if (this.mUnits[num10].IsUnitFlag(0x1000000) == null)
            {
                goto Label_01B4;
            }
            goto Label_0207;
        Label_01B4:
            if (this.mUnits[num10].IsDead == null)
            {
                goto Label_0207;
            }
            if (this.mUnits[num10].IsGimmick == null)
            {
                goto Label_01E7;
            }
            goto Label_0207;
        Label_01E7:
            if (this.CheckEnemySide(self, this.mUnits[num10]) != null)
            {
                goto Label_0207;
            }
            num8 += num9;
        Label_0207:
            num10 += 1;
        Label_020D:
            if (num10 < this.mUnits.Count)
            {
                goto Label_0193;
            }
        Label_021F:
            num11 = 0;
            if (skill.SkillParam.effect_lvrate == null)
            {
                goto Label_026D;
            }
            if (target == null)
            {
                goto Label_026D;
            }
            num13 = target.Lv - self.Lv;
            if (num13 <= 0)
            {
                goto Label_026D;
            }
            num11 = num13 * skill.SkillParam.effect_lvrate;
        Label_026D:
            num14 = 0;
            num15 = skill.SkillParam.EffectHitTargetNumRate;
            if (num15 == null)
            {
                goto Label_02C4;
            }
            if (log == null)
            {
                goto Label_02C4;
            }
            if (log.targets == null)
            {
                goto Label_02C4;
            }
            if (log.targets.Count <= 1)
            {
                goto Label_02C4;
            }
            num14 += num15 * (log.targets.Count - 1);
        Label_02C4:
            return ((((((num + num3) + num5) + num7) + num8) + num11) + num14);
        }

        public void GetSkillGridLines(Unit self, int ex, int ey, SkillData skill, ref List<Grid> results)
        {
            int num;
            int num2;
            int num3;
            ELineType type;
            bool flag;
            DebugUtility.Assert((self == null) == 0, "self == null");
            DebugUtility.Assert((skill == null) == 0, "skill == null");
            num = self.GetAttackRangeMax(skill);
            if (num > 0)
            {
                goto Label_0034;
            }
            return;
        Label_0034:
            num2 = self.GetAttackRangeMin(skill);
            num3 = self.GetAttackHeight(skill, 1);
            type = skill.LineType;
            flag = skill.IsEnableHeightRangeBonus();
            this.GetSkillGridLines(self.x, self.y, ex, ey, num2, num, num3, type, flag, results);
            return;
        }

        public unsafe void GetSkillGridLines(int sx, int sy, int ex, int ey, int range_min, int range_max, int attack_height, ELineType line_type, bool bHeightBonus, ref List<Grid> results)
        {
            BattleMap map;
            Grid grid;
            Grid grid2;
            int num;
            int num2;
            int num3;
            int num4;
            SVector2 vector;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            int num16;
            int num17;
            int num18;
            int num19;
            int num20;
            int num21;
            double num22;
            int num23;
            bool flag;
            double num24;
            double num25;
            double num26;
            int num27;
            double num28;
            double num29;
            double num30;
            double num31;
            double num32;
            double num33;
            double num34;
            double num35;
            int num36;
            int num37;
            int num38;
            double num39;
            double num40;
            double num41;
            double num42;
            double num43;
            double num44;
            double num45;
            double num46;
            Unit unit;
            int num47;
            int num48;
            int num49;
            int num50;
            int num51;
            SVector2 vector2;
            int num52;
            int num53;
            int num54;
            int num55;
            int num56;
            int num57;
            Unit unit2;
            int num58;
            ELineType type;
            *(results).Clear();
            map = this.CurrentMap;
            grid = map[sx, sy];
            grid2 = map[ex, ey];
            if (range_max != null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            num = range_max;
            if (bHeightBonus == null)
            {
                goto Label_0044;
            }
            num += this.GetAttackRangeBonus(grid.height, 0);
        Label_0044:
            num2 = 100;
            num3 = (ex - sx) * num2;
            num4 = (ey - sy) * num2;
            &vector = new SVector2(num3, num4);
            num5 = &vector.Length();
            if (((range_min <= 0) || (range_max <= 0)) || (num5 > range_min))
            {
                goto Label_0087;
            }
            return;
        Label_0087:
            type = line_type;
            switch ((type - 1))
            {
                case 0:
                    goto Label_00A5;

                case 1:
                    goto Label_0193;

                case 2:
                    goto Label_023C;
            }
            goto Label_02E8;
        Label_00A5:
            num6 = sx;
            num7 = sy;
            if (num5 == null)
            {
                goto Label_0102;
            }
            &vector.x = (&vector.x * num2) / num5;
            &vector.y = (&vector.y * num2) / num5;
            vector *= num;
            num6 += &vector.x / num2;
            num7 += &vector.y / num2;
        Label_0102:
            this.GetGridOnLine(grid, num6, num7, results);
            num8 = 0;
            goto Label_017F;
        Label_0117:
            num9 = this.CalcGridDistance(grid, *(results)[num8]);
            num10 = range_max - num9;
            num11 = this.GetAttackRangeBonus(grid.height, *(results)[num8].height);
            if (bHeightBonus == null)
            {
                goto Label_015C;
            }
            num10 += num11;
        Label_015C:
            if (num10 >= 0)
            {
                goto Label_0179;
            }
            *(results).RemoveRange(num8, *(results).Count - num8);
        Label_0179:
            num8 += 1;
        Label_017F:
            if (num8 < *(results).Count)
            {
                goto Label_0117;
            }
            goto Label_02E9;
        Label_0193:
            this.GetGridOnLine(grid, ex, ey, results);
            num12 = 0;
            goto Label_0228;
        Label_01A7:
            num13 = this.CalcGridDistance(grid, *(results)[num12]);
            num14 = num - num13;
            num15 = 0;
            num16 = 0;
            goto Label_01F5;
        Label_01CB:
            num15 = Math.Min(num15, this.GetAttackRangeBonus(grid.height, *(results)[num16].height));
            num16 += 1;
        Label_01F5:
            if (num16 <= num12)
            {
                goto Label_01CB;
            }
            num14 += num15;
            if (num14 >= 0)
            {
                goto Label_0222;
            }
            *(results).RemoveRange(num12, *(results).Count - num12);
        Label_0222:
            num12 += 1;
        Label_0228:
            if (num12 < *(results).Count)
            {
                goto Label_01A7;
            }
            goto Label_02E9;
        Label_023C:
            num17 = (grid2.height - grid.height) * num2;
            num18 = (num * num2) + 50;
            num19 = Sqrt(((num3 * num3) + (num4 * num4)) + (num17 * num17));
            if (num18 >= num19)
            {
                goto Label_0279;
            }
            return;
        Label_0279:
            num20 = sx;
            num21 = sy;
            if (num5 == null)
            {
                goto Label_02D6;
            }
            &vector.x = (&vector.x * num2) / num5;
            &vector.y = (&vector.y * num2) / num5;
            vector *= num;
            num20 += &vector.x / num2;
            num21 += &vector.y / num2;
        Label_02D6:
            this.GetGridOnLine(grid, num20, num21, results);
            goto Label_02E9;
        Label_02E8:
            return;
        Label_02E9:
            type = line_type;
            switch ((type - 1))
            {
                case 0:
                    goto Label_063F;

                case 1:
                    goto Label_0307;

                case 2:
                    goto Label_063F;
            }
            goto Label_07A3;
        Label_0307:
            if (*(results).Contains(grid2) != null)
            {
                goto Label_031E;
            }
            *(results).Clear();
            return;
        Label_031E:
            num22 = 0.0;
            if (num22 >= ((double) grid.height))
            {
                goto Label_0340;
            }
            num22 = (double) grid.height;
        Label_0340:
            num23 = 0;
            goto Label_0377;
        Label_0348:
            if (num22 >= ((double) *(results)[num23].height))
            {
                goto Label_0371;
            }
            num22 = (double) *(results)[num23].height;
        Label_0371:
            num23 += 1;
        Label_0377:
            if (num23 < *(results).Count)
            {
                goto Label_0348;
            }
            if (num22 > ((double) grid.height))
            {
                goto Label_03A2;
            }
            num22 += 1.0;
        Label_03A2:
            num22 += 1.0;
            flag = 1;
            num24 = ((double) num5) / 100.0;
            num25 = (double) (grid.height - grid2.height);
            num26 = 9.8;
            num27 = 0;
            goto Label_0622;
        Label_03E5:
            num28 = num22 + ((double) num27);
            num29 = (2.0 * num26) * (num28 - num25);
            num30 = (2.0 * num26) * num28;
            num29 = (num29 <= 0.0) ? 0.0 : Math.Sqrt(num29);
            num30 = (num30 <= 0.0) ? 0.0 : Math.Sqrt(num30);
            num31 = (num29 + num30) / num26;
            num32 = Math.Pow(num24 / num31, 2.0) + ((2.0 * num26) * (num28 - num25));
            num33 = (num32 <= 0.0) ? 0.0 : Math.Sqrt(num32);
            num34 = num31 / num24;
            num35 = (num31 * num29) / num24;
            num35 = Math.Atan(num35);
            flag = 1;
            num36 = 0;
            goto Label_0601;
        Label_04DE:
            num37 = *(results)[num36].x - grid.x;
            num38 = *(results)[num36].y - grid.y;
            num39 = Math.Min((double) Sqrt((num37 * num37) + (num38 * num38)), num24);
            num40 = num34 * num39;
            num41 = Math.Sin(num35);
            num42 = Math.Pow(num40, 2.0);
            num43 = (num26 * num42) * 0.5;
            num44 = ((num33 * num40) * num41) - num43;
            num45 = ((double) (*(results)[num36].height - grid.height)) - 0.01;
            if (num44 >= num45)
            {
                goto Label_059C;
            }
            flag = 0;
            goto Label_0610;
        Label_059C:
            num46 = num45 + ((double) (BattleMap.MAP_FLOOR_HEIGHT / 2));
            if (num44 >= num46)
            {
                goto Label_05FB;
            }
            unit = this.FindGimmickAtGrid(*(results)[num36], 0, null);
            if (unit == null)
            {
                goto Label_05FB;
            }
            if (unit.IsBreakObj == null)
            {
                goto Label_05FB;
            }
            if (unit.BreakObjClashType != 3)
            {
                goto Label_05FB;
            }
            if (unit.BreakObjRayType != 1)
            {
                goto Label_05FB;
            }
            flag = 0;
            goto Label_0610;
        Label_05FB:
            num36 += 1;
        Label_0601:
            if (num36 < *(results).Count)
            {
                goto Label_04DE;
            }
        Label_0610:
            if (flag == null)
            {
                goto Label_061C;
            }
            goto Label_062B;
        Label_061C:
            num27 += 1;
        Label_0622:
            if (num27 < attack_height)
            {
                goto Label_03E5;
            }
        Label_062B:
            if (flag != null)
            {
                goto Label_07CA;
            }
            *(results).Clear();
            goto Label_07CA;
        Label_063F:
            num47 = (grid2.height - grid.height) * num2;
            num48 = (BattleMap.MAP_FLOOR_HEIGHT * num2) / 2;
            num49 = 0;
            goto Label_078F;
        Label_0665:
            num50 = (*(results)[num49].x - sx) * num2;
            num51 = (*(results)[num49].y - sy) * num2;
            &vector2 = new SVector2(num50, num51);
            num52 = &vector2.Length();
            num53 = 0;
            if (num47 == null)
            {
                goto Label_06CA;
            }
            if (num5 == null)
            {
                goto Label_06CA;
            }
            num54 = (num52 * num2) / num5;
            num53 = (num47 * num54) / num2;
        Label_06CA:
            num55 = ((grid.height * num2) + num48) + num53;
            num56 = *(results)[num49].height * num2;
            num57 = num56 + num48;
            if (num55 >= num56)
            {
                goto Label_0715;
            }
            *(results).RemoveRange(num49, *(results).Count - num49);
            return;
        Label_0715:
            if (num55 <= num57)
            {
                goto Label_0732;
            }
            *(results).RemoveAt(num49--);
            goto Label_0789;
        Label_0732:
            unit2 = this.FindGimmickAtGrid(*(results)[num49], 0, null);
            if (unit2 == null)
            {
                goto Label_0789;
            }
            if (unit2.IsBreakObj == null)
            {
                goto Label_0789;
            }
            if (unit2.BreakObjClashType != 3)
            {
                goto Label_0789;
            }
            if (unit2.BreakObjRayType != 1)
            {
                goto Label_0789;
            }
            *(results).RemoveRange(num49, *(results).Count - num49);
            return;
        Label_0789:
            num49 += 1;
        Label_078F:
            if (num49 < *(results).Count)
            {
                goto Label_0665;
            }
            goto Label_07CA;
        Label_07A3:
            num58 = grid2.height - grid.height;
            if (num58 <= attack_height)
            {
                goto Label_07CA;
            }
            *(results).Remove(grid2);
        Label_07CA:
            return;
        }

        public unsafe SkillResult GetSkillResult(AiCache cache, Unit self, SkillData skill, SRPG.SkillMap.Score score)
        {
            SkillResult result;
            LogSkill skill2;
            int num;
            LogSkill.Target target;
            Unit unit;
            Grid grid;
            int num2;
            LogSkill.Target target2;
            Unit unit2;
            int num3;
            int num4;
            int num5;
            int num6;
            List<BuffAttachment> list;
            BuffAttachment attachment;
            List<BuffAttachment>.Enumerator enumerator;
            GridMap<bool> map;
            SVector2 vector;
            Grid grid2;
            result = null;
            if (this.IsEnableUseSkillEffect(self, skill, score.log) == null)
            {
                goto Label_0651;
            }
            skill2 = score.log;
            result = new SkillResult();
            result.skill = skill;
            result.movpos = cache.map[self.x, self.y];
            result.usepos = cache.map[&score.pos.x, &score.pos.y];
            result.locked = (this.FindUnitAtGrid(result.usepos) == null) == 0;
            result.cond_prio = cache.cond_prio;
            result.cost_jewel = cache.cost_jewel;
            result.buff_prio = 0xff;
            result.buff_dup = 0xff;
            if (skill2 == null)
            {
                goto Label_0428;
            }
            result.log = skill2;
            result.heal = skill2.GetTruthTotalHpHeal();
            result.heal_num = skill2.GetTruthTotalHpHealCount();
            result.cure_num = skill2.GetTotalCureConditionCount();
            result.fail_num = skill2.GetTotalFailConditionCount();
            result.disable_num = skill2.GetTotalDisableConditionCount();
            result.gain_jewel = skill2.GetGainJewel();
            if (this.isKnockBack(skill) == null)
            {
                goto Label_01EA;
            }
            num = 0;
            goto Label_01D9;
        Label_011F:
            target = skill2.targets[num];
            unit = target.target;
            if (this.IsFailTrickData(unit, unit.x, unit.y) == null)
            {
                goto Label_0154;
            }
            goto Label_01D5;
        Label_0154:
            grid = target.KnockBackGrid;
            if (grid != null)
            {
                goto Label_0168;
            }
            goto Label_01D5;
        Label_0168:
            if ((this.IsGoodTrickData(unit, unit.x, unit.y) == null) || (this.IsGoodTrickData(unit, grid.x, grid.y) != null))
            {
                goto Label_01AC;
            }
            result.nockback_prio += 1;
        Label_01AC:
            if (this.IsFailTrickData(unit, grid.x, grid.y) == null)
            {
                goto Label_01D5;
            }
            result.nockback_prio += 1;
        Label_01D5:
            num += 1;
        Label_01D9:
            if (num < skill2.targets.Count)
            {
                goto Label_011F;
            }
        Label_01EA:
            num2 = 0;
            goto Label_031F;
        Label_01F2:
            target2 = skill2.targets[num2];
            unit2 = target2.target;
            num3 = target2.GetTotalHpDamage();
            if (unit2.IsBreakObj == null)
            {
                goto Label_0285;
            }
            result.ext_damage += Math.Max(Math.Min(num3, unit2.CurrentStatus.param.hp), 0);
            result.ext_dead_num += (num3 <= unit2.CurrentStatus.param.hp) ? 0 : 1;
            goto Label_02F5;
        Label_0285:
            result.unit_damage_t += num3;
            result.unit_damage += Math.Max(Math.Min(num3, unit2.CurrentStatus.param.hp), 0);
            result.unit_dead_num += (num3 <= unit2.CurrentStatus.param.hp) ? 0 : 1;
        Label_02F5:
            num4 = unit2.GetBuffPriority(skill, 0);
            result.buff_prio = Math.Max(Math.Min(num4, result.buff_prio), 0);
            num2 += 1;
        Label_031F:
            if (num2 < skill2.targets.Count)
            {
                goto Label_01F2;
            }
            skill2.GetTotalBuffEffect(&result.buff_num, &result.buff);
            if (skill.DuplicateCount <= 1)
            {
                goto Label_0428;
            }
            num5 = 0;
            goto Label_0416;
        Label_0357:
            num6 = 0;
            if (skill2.targets[num5].target.BuffAttachments.Count <= 0)
            {
                goto Label_03FD;
            }
            enumerator = skill2.targets[num5].target.BuffAttachments.GetEnumerator();
        Label_039E:
            try
            {
                goto Label_03DF;
            Label_03A3:
                attachment = &enumerator.Current;
                if (attachment.skill != null)
                {
                    goto Label_03BD;
                }
                goto Label_03DF;
            Label_03BD:
                if ((attachment.skill.SkillID == skill.SkillID) == null)
                {
                    goto Label_03DF;
                }
                num6 += 1;
            Label_03DF:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_03A3;
                }
                goto Label_03FD;
            }
            finally
            {
            Label_03F0:
                ((List<BuffAttachment>.Enumerator) enumerator).Dispose();
            }
        Label_03FD:
            result.buff_dup = Math.Min(result.buff_dup, num6);
            num5 += 1;
        Label_0416:
            if (num5 < skill2.targets.Count)
            {
                goto Label_0357;
            }
        Label_0428:
            if (result.buff_prio != 0xff)
            {
                goto Label_0446;
            }
            result.buff_prio = self.GetBuffPriority(skill, 1);
        Label_0446:
            if (skill.TeleportType != 2)
            {
                goto Label_04B5;
            }
            if (skill.IsTargetTeleport != null)
            {
                goto Label_04B5;
            }
            if (cache.baseRangeMap == null)
            {
                goto Label_04B5;
            }
            if (cache.baseRangeMap.get(result.usepos.x, result.usepos.y) == null)
            {
                goto Label_04B5;
            }
            result.movpos = cache.map[&cache.pos.x, &cache.pos.y];
        Label_04B5:
            result.distance = this.CalcGridDistance(result.usepos, result.movpos);
            result.score_prio = score.priority;
            result.fail_trick = this.GetFailTrickPriority(self, result.movpos);
            result.good_trick = this.GetGoodTrickPriority(self, result.movpos, &result.heal_trick);
            result.unit_prio = this.GetSkillTargetsHighestPriority(self, skill, skill2);
            result.ct = 0;
            if (self.x != &cache.pos.x)
            {
                goto Label_0548;
            }
            if (self.y == &cache.pos.y)
            {
                goto Label_0565;
            }
        Label_0548:
            result.ct -= cache.fixparam.ChargeTimeDecMove;
        Label_0565:
            result.ct -= cache.fixparam.ChargeTimeDecWait;
            result.ct -= cache.fixparam.ChargeTimeDecAction;
            if (skill.TeleportType != 3)
            {
                goto Label_0628;
            }
            map = this.CreateSelectGridMapAI(self, self.x, self.y, skill);
            &vector = new SVector2(result.movpos.x, result.movpos.y);
            this.GetSafePositionEx(self, map, &vector);
            result.usepos = cache.map[&vector.x, &vector.y];
            result.locked = 0;
            result.distance = this.CalcGridDistance(result.usepos, result.movpos);
        Label_0628:
            if (skill.IsTargetTeleport == null)
            {
                goto Label_0651;
            }
            grid2 = skill2.TeleportGrid;
            if (grid2 == null)
            {
                goto Label_0651;
            }
            result.teleport = this.GetSafeValue(self, grid2);
        Label_0651:
            return result;
        }

        private int GetSkillTargetsHighestPriority(Unit self, SkillData skill, LogSkill log)
        {
            int num;
            AIParam param;
            int num2;
            <GetSkillTargetsHighestPriority>c__AnonStorey1C0 storeyc;
            <GetSkillTargetsHighestPriority>c__AnonStorey1C1 storeyc2;
            storeyc = new <GetSkillTargetsHighestPriority>c__AnonStorey1C0();
            storeyc.log = log;
            num = 0xff;
            if (self == null)
            {
                goto Label_002A;
            }
            if (skill == null)
            {
                goto Label_002A;
            }
            if (storeyc.log != null)
            {
                goto Label_002C;
            }
        Label_002A:
            return num;
        Label_002C:
            param = self.AI;
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_00CF;
            }
            if (param == null)
            {
                goto Label_00CF;
            }
            if (param.CheckFlag(0x200) != null)
            {
                goto Label_00CF;
            }
            storeyc2 = new <GetSkillTargetsHighestPriority>c__AnonStorey1C1();
            storeyc2.<>f__ref$448 = storeyc;
            storeyc2.k = 0;
            goto Label_00B3;
        Label_0070:
            num2 = this.mEnemyPriorities.FindIndex(new Predicate<Unit>(storeyc2.<>m__67));
            if (num2 != -1)
            {
                goto Label_0095;
            }
            goto Label_00A3;
        Label_0095:
            num = Math.Max(Math.Min(num, num2), 0);
        Label_00A3:
            storeyc2.k += 1;
        Label_00B3:
            if (storeyc2.k < storeyc.log.targets.Count)
            {
                goto Label_0070;
            }
        Label_00CF:
            return num;
        }

        private Unit GetSubMemberFirst(int owner)
        {
            int num;
            num = 0;
            goto Label_0083;
        Label_0007:
            if (this.Player[num].IsSub == null)
            {
                goto Label_007F;
            }
            if (this.Player[num].IsDead != null)
            {
                goto Label_007F;
            }
            if (this.Player[num] != this.Friend)
            {
                goto Label_004F;
            }
            goto Label_007F;
        Label_004F:
            if (owner == -1)
            {
                goto Label_0072;
            }
            if (this.Player[num].OwnerPlayerIndex == owner)
            {
                goto Label_0072;
            }
            goto Label_007F;
        Label_0072:
            return this.Player[num];
        Label_007F:
            num += 1;
        Label_0083:
            if (num < this.Player.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public unsafe Grid GetTeleportGrid(Unit self, int gx, int gy, Unit target, SkillData skill, ref bool is_teleport)
        {
            Grid grid;
            Grid grid2;
            int num;
            int num2;
            int num3;
            Grid grid3;
            *((sbyte*) is_teleport) = 0;
            if (self == null)
            {
                goto Label_0023;
            }
            if (target == null)
            {
                goto Label_0023;
            }
            if (skill == null)
            {
                goto Label_0023;
            }
            if (this.CurrentMap != null)
            {
                goto Label_0025;
            }
        Label_0023:
            return null;
        Label_0025:
            grid = this.CurrentMap[gx, gy];
            grid2 = this.CurrentMap[target.x, target.y];
            if (grid == null)
            {
                goto Label_0059;
            }
            if (grid2 != null)
            {
                goto Label_005B;
            }
        Label_0059:
            return null;
        Label_005B:
            num = this.UnitDirectionFromGrid(grid2, grid);
            num2 = grid2.x + Unit.DIRECTION_OFFSETS[num, 0];
            num3 = grid2.y + Unit.DIRECTION_OFFSETS[num, 1];
            grid3 = this.CurrentMap[num2, num3];
            if (grid3 != null)
            {
                goto Label_00A6;
            }
            return null;
        Label_00A6:
            if (skill.IsTargetTeleport == null)
            {
                goto Label_00F4;
            }
            if (Math.Abs(grid2.height - grid3.height) > skill.SkillParam.effect_height)
            {
                goto Label_00F4;
            }
            if (this.CurrentMap.CheckEnableMove(self, grid3, 0, 0) == null)
            {
                goto Label_00F4;
            }
            *((sbyte*) is_teleport) = 1;
        Label_00F4:
            return grid3;
        }

        private int GetTotalKillCount()
        {
            int num;
            int num2;
            num = 0;
            num2 = 0;
            goto Label_0037;
        Label_0009:
            if (this.Units[num2].Side != null)
            {
                goto Label_0033;
            }
            num += this.Units[num2].KillCount;
        Label_0033:
            num2 += 1;
        Label_0037:
            if (num2 < this.Units.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        private unsafe int getUnitDeadCount(string unit_iname)
        {
            int num;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            if (string.IsNullOrEmpty(unit_iname) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            num = 0;
            enumerator = this.Units.GetEnumerator();
        Label_001B:
            try
            {
                goto Label_0057;
            Label_0020:
                unit = &enumerator.Current;
                if (unit.IsDead != null)
                {
                    goto Label_0038;
                }
                goto Label_0057;
            Label_0038:
                if ((unit.UnitParam.iname != unit_iname) == null)
                {
                    goto Label_0053;
                }
                goto Label_0057;
            Label_0053:
                num += 1;
            Label_0057:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0020;
                }
                goto Label_0074;
            }
            finally
            {
            Label_0068:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0074:
            return num;
        }

        public Grid GetUnitGridPosition(Unit unit)
        {
            BattleMap map;
            if (unit != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            map = this.CurrentMap;
            if (map != null)
            {
                goto Label_0017;
            }
            return null;
        Label_0017:
            return map[unit.x, unit.y];
        }

        public Grid GetUnitGridPosition(int x, int y)
        {
            BattleMap map;
            map = this.CurrentMap;
            if (map != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            return map[x, y];
        }

        public int GetUnitMaxAttackHeight(Unit self, SkillData skill)
        {
            return self.GetAttackHeight(skill, 0);
        }

        public Unit GetUnitUseCollaboSkill(Unit unit, SkillData skill)
        {
            if (unit != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            return unit.GetUnitUseCollaboSkill(skill, 0);
        }

        private unsafe void GetUsedSkillResultAllEx(Unit self, SkillData skill, List<SkillResult> results)
        {
            bool flag;
            AiCache cache;
            SRPG.SkillMap.Data data;
            Dictionary<int, SRPG.SkillMap.Target>.ValueCollection values;
            SRPG.SkillMap.Target target;
            Dictionary<int, SRPG.SkillMap.Target>.ValueCollection.Enumerator enumerator;
            Dictionary<int, SRPG.SkillMap.Score>.ValueCollection values2;
            SRPG.SkillMap.Score score;
            Dictionary<int, SRPG.SkillMap.Score>.ValueCollection.Enumerator enumerator2;
            SkillResult result;
            if (skill == null)
            {
                goto Label_000C;
            }
            if (results != null)
            {
                goto Label_000D;
            }
        Label_000C:
            return;
        Label_000D:
            flag = self.IsUnitFlag(2) == 0;
            if (skill.TeleportType != 1)
            {
                goto Label_0035;
            }
            if (flag != null)
            {
                goto Label_0034;
            }
            this.GetUsedTeleportSkillResult(self, skill, results);
            return;
        Label_0034:
            return;
        Label_0035:
            cache = new AiCache();
            cache.map = this.CurrentMap;
            cache.fixparam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            cache.cost_jewel = self.GetSkillUsedCost(skill);
            cache.cond_prio = self.GetConditionPriority(skill, 0);
            cache.pos = new SVector2(self.x, self.y);
            cache.baseRangeMap = null;
            if (skill.TeleportType == null)
            {
                goto Label_00BA;
            }
            cache.baseRangeMap = this.CreateSelectGridMapAI(self, self.x, self.y, skill);
        Label_00BA:
            if (self.IsUnitFlag(0x80) == null)
            {
                goto Label_00D7;
            }
            if (skill.IsHealSkill() != null)
            {
                goto Label_00D7;
            }
            flag = 0;
        Label_00D7:
            data = this.mSkillMap.Get(skill);
            if (data == null)
            {
                goto Label_0230;
            }
            enumerator = data.targets.Values.GetEnumerator();
        Label_00FE:
            try
            {
                goto Label_01F0;
            Label_0103:
                target = &enumerator.Current;
                if (flag != null)
                {
                    goto Label_014F;
                }
                if (&cache.pos.x != &target.pos.x)
                {
                    goto Label_01F0;
                }
                if (&cache.pos.y == &target.pos.y)
                {
                    goto Label_014F;
                }
                goto Label_01F0;
            Label_014F:
                self.x = &target.pos.x;
                self.y = &target.pos.y;
                if (this.IsUseSkillCollabo(self, skill) != null)
                {
                    goto Label_0185;
                }
                goto Label_01F0;
            Label_0185:
                enumerator2 = target.scores.Values.GetEnumerator();
            Label_019C:
                try
                {
                    goto Label_01D2;
                Label_01A1:
                    score = &enumerator2.Current;
                    if (score.log == null)
                    {
                        goto Label_01D2;
                    }
                    result = this.GetSkillResult(cache, self, skill, score);
                    if (result == null)
                    {
                        goto Label_01D2;
                    }
                    results.Add(result);
                Label_01D2:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_01A1;
                    }
                    goto Label_01F0;
                }
                finally
                {
                Label_01E3:
                    ((Dictionary<int, SRPG.SkillMap.Score>.ValueCollection.Enumerator) enumerator2).Dispose();
                }
            Label_01F0:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0103;
                }
                goto Label_020E;
            }
            finally
            {
            Label_0201:
                ((Dictionary<int, SRPG.SkillMap.Target>.ValueCollection.Enumerator) enumerator).Dispose();
            }
        Label_020E:
            self.x = &cache.pos.x;
            self.y = &cache.pos.y;
        Label_0230:
            return;
        }

        private unsafe void GetUsedTeleportSkillResult(Unit self, SkillData skill, List<SkillResult> results)
        {
            BattleMap map;
            GridMap<bool> map2;
            List<SVector2> list;
            int num;
            int num2;
            int num3;
            Unit unit;
            int num4;
            SVector2 vector;
            List<Unit> list2;
            int num5;
            Unit unit2;
            Grid grid;
            int num6;
            int num7;
            SVector2 vector2;
            int num8;
            int num9;
            int num10;
            SkillResult result;
            map = this.CurrentMap;
            map2 = this.CreateSelectGridMapAI(self, self.x, self.y, skill);
            list = new List<SVector2>();
            num = 0;
            goto Label_0069;
        Label_0029:
            num2 = 0;
            goto Label_0058;
        Label_0031:
            if (map2.get(num, num2) != null)
            {
                goto Label_0044;
            }
            goto Label_0052;
        Label_0044:
            list.Add(new SVector2(num, num2));
        Label_0052:
            num2 += 1;
        Label_0058:
            if (num2 < map2.h)
            {
                goto Label_0031;
            }
            num += 1;
        Label_0069:
            if (num < map2.w)
            {
                goto Label_0029;
            }
            num3 = self.GetMoveCount(1);
            unit = null;
            num4 = 0x7fffffff;
            &vector = new SVector2(self.x, self.y);
            if (this.mEnemyPriorities.Count != null)
            {
                goto Label_00BE;
            }
            this.GetEnemyPriorities(self, this.mEnemyPriorities, this.mGimmickPriorities);
        Label_00BE:
            list2 = this.mEnemyPriorities;
            num5 = 0;
            goto Label_01CB;
        Label_00CE:
            unit2 = list2[num5];
            grid = this.GetUnitGridPosition(unit2);
            if (map.CalcMoveSteps(self, grid, 0) != null)
            {
                goto Label_00F7;
            }
            goto Label_01C5;
        Label_00F7:
            num6 = map[self.x, self.y].step;
            num7 = 0;
            goto Label_01B8;
        Label_0118:
            vector2 = list[num7];
            if (map[&vector2.x, &vector2.y].step >= num6)
            {
                goto Label_01B2;
            }
            if (num6 <= num3)
            {
                goto Label_01B2;
            }
            int introduced20 = Math.Abs(&vector2.y - unit2.y);
            num9 = introduced20 + Math.Abs(&vector2.x - unit2.x);
            if (num4 <= num9)
            {
                goto Label_01B2;
            }
            if (this.mSafeMapEx.get(&vector2.x, &vector2.y) <= 1)
            {
                goto Label_01B2;
            }
            unit = unit2;
            num4 = num9;
            vector = vector2;
        Label_01B2:
            num7 += 1;
        Label_01B8:
            if (num7 < list.Count)
            {
                goto Label_0118;
            }
        Label_01C5:
            num5 += 1;
        Label_01CB:
            if (num5 < list2.Count)
            {
                goto Label_00CE;
            }
            if (unit == null)
            {
                goto Label_024C;
            }
            if (&vector.x != self.x)
            {
                goto Label_0204;
            }
            if (&vector.y == self.y)
            {
                goto Label_024C;
            }
        Label_0204:
            result = new SkillResult();
            result.skill = skill;
            result.movpos = this.GetUnitGridPosition(self);
            result.usepos = map[&vector.x, &vector.y];
            result.teleport = -1;
            results.Add(result);
        Label_024C:
            return;
        }

        private unsafe void GetYuragiDamage(ref int damage)
        {
            FixParam param;
            int num;
            int num2;
            if (*(((int*) damage)) >= 2)
            {
                goto Label_0009;
            }
            return;
        Label_0009:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            if (param.RandomEffectMax != null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            num = (param.RandomEffectMax * 2) + 1;
            num2 = ((this.GetRandom() % 100) % num) - param.RandomEffectMax;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_005F;
            }
            return;
        Label_005F:
            *((int*) damage) = Math.Max(*(((int*) damage)) + num2, 0);
            return;
        }

        private void GimmickEventDamageCount(Unit attacker, Unit defender)
        {
            int num;
            GimmickEvent event2;
            GimmickEventCondition condition;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            num = 0;
            goto Label_00C0;
        Label_0014:
            event2 = this.mGimmickEvents[num];
            if (event2.IsCompleted == null)
            {
                goto Label_0031;
            }
            goto Label_00BC;
        Label_0031:
            if (event2.condition.type == 1)
            {
                goto Label_0047;
            }
            goto Label_00BC;
        Label_0047:
            condition = event2.condition;
            if (condition.units.Count <= 0)
            {
                goto Label_0075;
            }
            if (condition.units.Contains(attacker) != null)
            {
                goto Label_0075;
            }
            goto Label_00BC;
        Label_0075:
            if (condition.targets.Contains(defender) != null)
            {
                goto Label_008B;
            }
            goto Label_00BC;
        Label_008B:
            event2.count += 1;
            if (event2.IsStarter == null)
            {
                goto Label_00BC;
            }
            if (condition.count > event2.count)
            {
                goto Label_00BC;
            }
            event2.starter = attacker;
        Label_00BC:
            num += 1;
        Label_00C0:
            if (num < this.mGimmickEvents.Count)
            {
                goto Label_0014;
            }
            return;
        }

        private void GimmickEventDeadCount(Unit self, Unit target)
        {
            int num;
            GimmickEvent event2;
            GimmickEventCondition condition;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (target == null)
            {
                goto Label_001E;
            }
            if (target.IsDead != null)
            {
                goto Label_001F;
            }
        Label_001E:
            return;
        Label_001F:
            num = 0;
            goto Label_00D2;
        Label_0026:
            event2 = this.mGimmickEvents[num];
            if (event2.IsCompleted == null)
            {
                goto Label_0043;
            }
            goto Label_00CE;
        Label_0043:
            if (event2.condition.type == 2)
            {
                goto Label_0059;
            }
            goto Label_00CE;
        Label_0059:
            condition = event2.condition;
            if (condition.units.Count <= 0)
            {
                goto Label_0087;
            }
            if (condition.units.Contains(self) != null)
            {
                goto Label_0087;
            }
            goto Label_00CE;
        Label_0087:
            if (condition.targets.Contains(target) != null)
            {
                goto Label_009D;
            }
            goto Label_00CE;
        Label_009D:
            event2.count += 1;
            if (event2.IsStarter == null)
            {
                goto Label_00CE;
            }
            if (condition.count > event2.count)
            {
                goto Label_00CE;
            }
            event2.starter = self;
        Label_00CE:
            num += 1;
        Label_00D2:
            if (num < this.mGimmickEvents.Count)
            {
                goto Label_0026;
            }
            return;
        }

        private void GimmickEventOnGrid()
        {
            int num;
            GimmickEvent event2;
            int num2;
            int num3;
            int num4;
            int num5;
            Unit unit;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            num = 0;
            goto Label_0157;
        Label_0014:
            event2 = this.mGimmickEvents[num];
            if (event2.IsCompleted != null)
            {
                goto Label_0153;
            }
            if (event2.IsStarter != null)
            {
                goto Label_003C;
            }
            goto Label_0153;
        Label_003C:
            if (event2.starter == null)
            {
                goto Label_004C;
            }
            goto Label_0153;
        Label_004C:
            if (event2.condition.type == 3)
            {
                goto Label_0062;
            }
            goto Label_0153;
        Label_0062:
            num2 = 0;
            goto Label_013D;
        Label_0069:
            num3 = event2.condition.grids[num2].x;
            num4 = event2.condition.grids[num2].y;
            num5 = 0;
            goto Label_0127;
        Label_00A0:
            unit = this.Units[num5];
            if (unit.IsGimmick != null)
            {
                goto Label_0121;
            }
            if (unit.CheckExistMap() != null)
            {
                goto Label_00CC;
            }
            goto Label_0121;
        Label_00CC:
            if (event2.condition.units.Count <= 0)
            {
                goto Label_00FE;
            }
            if (event2.condition.units.Contains(unit) != null)
            {
                goto Label_00FE;
            }
            goto Label_0121;
        Label_00FE:
            if (unit.x != num3)
            {
                goto Label_0121;
            }
            if (unit.y != num4)
            {
                goto Label_0121;
            }
            event2.starter = unit;
        Label_0121:
            num5 += 1;
        Label_0127:
            if (num5 < this.Units.Count)
            {
                goto Label_00A0;
            }
            num2 += 1;
        Label_013D:
            if (num2 < event2.condition.grids.Count)
            {
                goto Label_0069;
            }
        Label_0153:
            num += 1;
        Label_0157:
            if (num < this.mGimmickEvents.Count)
            {
                goto Label_0014;
            }
            return;
        }

        public unsafe void GimmickEventTrickKillCount(TrickData trick_data)
        {
            GimmickEvent event2;
            List<GimmickEvent>.Enumerator enumerator;
            GimmickEventCondition condition;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (trick_data != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            enumerator = this.mGimmickEvents.GetEnumerator();
        Label_0020:
            try
            {
                goto Label_007E;
            Label_0025:
                event2 = &enumerator.Current;
                if (event2.IsCompleted == null)
                {
                    goto Label_003D;
                }
                goto Label_007E;
            Label_003D:
                if (event2.condition.type == 2)
                {
                    goto Label_0053;
                }
                goto Label_007E;
            Label_0053:
                if (event2.condition.td_targets.Contains(trick_data) != null)
                {
                    goto Label_0070;
                }
                goto Label_007E;
            Label_0070:
                event2.count += 1;
            Label_007E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0025;
                }
                goto Label_009B;
            }
            finally
            {
            Label_008F:
                ((List<GimmickEvent>.Enumerator) enumerator).Dispose();
            }
        Label_009B:
            return;
        }

        private unsafe bool GridEventStart(Unit self, Unit target, EEventTrigger type, SkillParam skill_param)
        {
            char[] chArray1;
            EventTrigger trigger;
            bool flag;
            int num;
            List<Unit> list;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            bool flag2;
            Unit unit2;
            List<Unit>.Enumerator enumerator2;
            int num2;
            Unit unit3;
            List<Unit>.Enumerator enumerator3;
            Unit unit4;
            List<Unit>.Enumerator enumerator4;
            string[] strArray;
            int num3;
            int num4;
            Unit unit5;
            List<Unit>.Enumerator enumerator5;
            Unit unit6;
            List<Unit>.Enumerator enumerator6;
            LogMapEvent event2;
            EEventTrigger trigger2;
            EEventTrigger trigger3;
            EEventType type2;
            if (self == null)
            {
                goto Label_000C;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            if (target.CheckEventTrigger(type) != null)
            {
                goto Label_001C;
            }
            return 0;
        Label_001C:
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_002A;
            }
            return 0;
        Label_002A:
            trigger = target.EventTrigger;
            if (trigger != null)
            {
                goto Label_0039;
            }
            return 0;
        Label_0039:
            flag = self.IsDead;
            switch ((trigger.Trigger - 1))
            {
                case 0:
                    goto Label_0066;

                case 1:
                    goto Label_00DF;

                case 2:
                    goto Label_00DF;

                case 3:
                    goto Label_0092;
            }
            goto Label_00DF;
        Label_0066:
            if (trigger.EventType == 1)
            {
                goto Label_007E;
            }
            if (trigger.EventType != 2)
            {
                goto Label_0080;
            }
        Label_007E:
            return 0;
        Label_0080:
            if (target.IsDead != null)
            {
                goto Label_04F8;
            }
            return 0;
            goto Label_04F8;
        Label_0092:
            if (trigger.EventType == 1)
            {
                goto Label_00AA;
            }
            if (trigger.EventType != 2)
            {
                goto Label_00AC;
            }
        Label_00AA:
            return 0;
        Label_00AC:
            num = (target.MaximumStatusHp * trigger.IntValue) / 100;
            if (num >= target.CurrentStatus.param.hp)
            {
                goto Label_04F8;
            }
            return 0;
            goto Label_04F8;
        Label_00DF:
            if (trigger.IsTriggerWithdraw == null)
            {
                goto Label_04F8;
            }
            if (trigger.EventType == 5)
            {
                goto Label_00F8;
            }
            return 0;
        Label_00F8:
            if (target.IsDead == null)
            {
                goto Label_0105;
            }
            return 0;
        Label_0105:
            list = new List<Unit>();
            if (string.IsNullOrEmpty(trigger.Tag) != null)
            {
                goto Label_020B;
            }
            enumerator = this.mUnits.GetEnumerator();
        Label_0128:
            try
            {
                goto Label_01ED;
            Label_012D:
                unit = &enumerator.Current;
                if (unit.IsGimmick == null)
                {
                    goto Label_0153;
                }
                if (unit.IsBreakObj != null)
                {
                    goto Label_0153;
                }
                goto Label_01ED;
            Label_0153:
                if (trigger.Trigger != 9)
                {
                    goto Label_01A4;
                }
                if (unit.IsDead == null)
                {
                    goto Label_01ED;
                }
                if (unit.IsEntry == null)
                {
                    goto Label_01ED;
                }
                if (unit.IsSub == null)
                {
                    goto Label_0189;
                }
                goto Label_01ED;
            Label_0189:
                if (unit.IsUnitFlag(0x100000) == null)
                {
                    goto Label_01CD;
                }
                goto Label_01ED;
                goto Label_01CD;
            Label_01A4:
                if (unit.IsDead != null)
                {
                    goto Label_01ED;
                }
                if (unit.IsEntry == null)
                {
                    goto Label_01ED;
                }
                if (unit.IsSub == null)
                {
                    goto Label_01CD;
                }
                goto Label_01ED;
            Label_01CD:
                if (this.CheckMatchUniqueName(unit, trigger.Tag) != null)
                {
                    goto Label_01E5;
                }
                goto Label_01ED;
            Label_01E5:
                list.Add(unit);
            Label_01ED:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_012D;
                }
                goto Label_020B;
            }
            finally
            {
            Label_01FE:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_020B:
            if (trigger.Trigger == 9)
            {
                goto Label_022A;
            }
            if (list.Count != null)
            {
                goto Label_022A;
            }
            list.Add(target);
        Label_022A:
            flag2 = 0;
            switch ((trigger.Trigger - 5))
            {
                case 0:
                    goto Label_025B;

                case 1:
                    goto Label_02D1;

                case 2:
                    goto Label_0338;

                case 3:
                    goto Label_0399;

                case 4:
                    goto Label_045C;

                case 5:
                    goto Label_0480;
            }
            goto Label_04EA;
        Label_025B:
            enumerator2 = list.GetEnumerator();
        Label_0263:
            try
            {
                goto Label_02AE;
            Label_0268:
                unit2 = &enumerator2.Current;
                num2 = (unit2.MaximumStatusHp * trigger.IntValue) / 100;
                if (num2 >= unit2.CurrentStatus.param.hp)
                {
                    goto Label_02A6;
                }
                goto Label_02AE;
            Label_02A6:
                flag2 = 1;
                goto Label_02BA;
            Label_02AE:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0268;
                }
            Label_02BA:
                goto Label_02CC;
            }
            finally
            {
            Label_02BF:
                ((List<Unit>.Enumerator) enumerator2).Dispose();
            }
        Label_02CC:
            goto Label_04EA;
        Label_02D1:
            enumerator3 = list.GetEnumerator();
        Label_02D9:
            try
            {
                goto Label_0315;
            Label_02DE:
                unit3 = &enumerator3.Current;
                if (trigger.IntValue >= unit3.CurrentStatus.param.hp)
                {
                    goto Label_030D;
                }
                goto Label_0315;
            Label_030D:
                flag2 = 1;
                goto Label_0321;
            Label_0315:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_02DE;
                }
            Label_0321:
                goto Label_0333;
            }
            finally
            {
            Label_0326:
                ((List<Unit>.Enumerator) enumerator3).Dispose();
            }
        Label_0333:
            goto Label_04EA;
        Label_0338:
            enumerator4 = list.GetEnumerator();
        Label_0340:
            try
            {
                goto Label_0376;
            Label_0345:
                unit4 = &enumerator4.Current;
                if ((trigger.IntValue * (1 + this.mContinueCount)) <= unit4.ActionCount)
                {
                    goto Label_036E;
                }
                goto Label_0376;
            Label_036E:
                flag2 = 1;
                goto Label_0382;
            Label_0376:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_0345;
                }
            Label_0382:
                goto Label_0394;
            }
            finally
            {
            Label_0387:
                ((List<Unit>.Enumerator) enumerator4).Dispose();
            }
        Label_0394:
            goto Label_04EA;
        Label_0399:
            if (string.IsNullOrEmpty(trigger.StrValue) == null)
            {
                goto Label_03AB;
            }
            return 0;
        Label_03AB:
            chArray1 = new char[] { 0x2c };
            strArray = trigger.StrValue.Split(chArray1);
            if (strArray == null)
            {
                goto Label_03D4;
            }
            if (((int) strArray.Length) >= 2)
            {
                goto Label_03D6;
            }
        Label_03D4:
            return 0;
        Label_03D6:
            if (int.TryParse(strArray[0], &num3) != null)
            {
                goto Label_03E8;
            }
            return 0;
        Label_03E8:
            if (int.TryParse(strArray[1], &num4) != null)
            {
                goto Label_03FA;
            }
            return 0;
        Label_03FA:
            enumerator5 = list.GetEnumerator();
        Label_0402:
            try
            {
                goto Label_0439;
            Label_0407:
                unit5 = &enumerator5.Current;
                if (unit5.x != num3)
                {
                    goto Label_0439;
                }
                if (unit5.y == num4)
                {
                    goto Label_0431;
                }
                goto Label_0439;
            Label_0431:
                flag2 = 1;
                goto Label_0445;
            Label_0439:
                if (&enumerator5.MoveNext() != null)
                {
                    goto Label_0407;
                }
            Label_0445:
                goto Label_0457;
            }
            finally
            {
            Label_044A:
                ((List<Unit>.Enumerator) enumerator5).Dispose();
            }
        Label_0457:
            goto Label_04EA;
        Label_045C:
            if (list.Count == null)
            {
                goto Label_04EA;
            }
            if (list.Count < trigger.IntValue)
            {
                goto Label_04EA;
            }
            flag2 = 1;
            goto Label_04EA;
        Label_0480:
            enumerator6 = list.GetEnumerator();
        Label_0488:
            try
            {
                goto Label_04C7;
            Label_048D:
                unit6 = &enumerator6.Current;
                if (unit6 == self)
                {
                    goto Label_04A3;
                }
                goto Label_04C7;
            Label_04A3:
                if ((trigger.StrValue != skill_param.iname) == null)
                {
                    goto Label_04BF;
                }
                goto Label_04C7;
            Label_04BF:
                flag2 = 1;
                goto Label_04D3;
            Label_04C7:
                if (&enumerator6.MoveNext() != null)
                {
                    goto Label_048D;
                }
            Label_04D3:
                goto Label_04E5;
            }
            finally
            {
            Label_04D8:
                ((List<Unit>.Enumerator) enumerator6).Dispose();
            }
        Label_04E5:;
        Label_04EA:
            if (flag2 != null)
            {
                goto Label_04F8;
            }
            return 0;
        Label_04F8:
            if (trigger.EventType == 5)
            {
                goto Label_05C1;
            }
            event2 = this.Log<LogMapEvent>();
            event2.self = self;
            event2.target = target;
            event2.type = trigger.EventType;
            event2.gimmick = trigger.GimmickType;
            switch ((trigger.EventType - 1))
            {
                case 0:
                    goto Label_055C;

                case 1:
                    goto Label_056F;

                case 2:
                    goto Label_0582;

                case 3:
                    goto Label_0587;
            }
            goto Label_05B7;
        Label_055C:
            this.mWinTriggerCount += 1;
            goto Label_05BC;
        Label_056F:
            this.mLoseTriggerCount += 1;
            goto Label_05BC;
        Label_0582:
            goto Label_05BC;
        Label_0587:
            this.AddGems(self, MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GemsGainValue);
            trigger.ExecuteGimmickEffect(target, self, event2);
            goto Label_05BC;
        Label_05B7:;
        Label_05BC:
            goto Label_05C8;
        Label_05C1:
            this.UnitWithdraw(target);
        Label_05C8:
            target.DecrementTriggerCount();
            type2 = trigger.EventType;
            if (type2 == 3)
            {
                goto Label_05EB;
            }
            if (type2 == 4)
            {
                goto Label_05EB;
            }
            goto Label_0604;
        Label_05EB:
            if (trigger.Count != null)
            {
                goto Label_0604;
            }
            this.UpdateEntryTriggers(3, target, null);
        Label_0604:
            if (trigger.Trigger != 3)
            {
                goto Label_0620;
            }
            self.SetUnitFlag(4, 1);
            self.SetCommandFlag(2, 1);
        Label_0620:
            if (flag != null)
            {
                goto Label_063B;
            }
            if (self.IsDead == null)
            {
                goto Label_063B;
            }
            this.Dead(null, self, 0, 0);
        Label_063B:
            return 1;
        }

        private void Heal(Unit target, int value)
        {
            target.Heal(value);
            return;
        }

        private void HealCureCondition(Unit target, LogSkill log)
        {
        }

        private void HealSkill(Unit self, Unit target, SkillData skill, LogSkill log)
        {
            int num;
            num = 0;
            if (target.IsUnitCondition(0x1000000L) != null)
            {
                goto Label_0027;
            }
            num = this.CalcHeal(self, target, skill, log);
            num = self.CalcParamRecover(num);
        Label_0027:
            log.Hit(self, target, 0, 0, 0, 0, num, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_00B4;
            }
            if (target.IsPartyMember == null)
            {
                goto Label_00AC;
            }
            this.mTotalHeal += Math.Min(target.CurrentStatus.param.hp + num, target.MaximumStatus.param.hp) - target.CurrentStatus.param.hp;
        Label_00AC:
            this.Heal(target, num);
        Label_00B4:
            return;
        }

        public void IncrementMap()
        {
            this.DebugAssert(this.IsBattleFlag(1) == 0, "マップ未開始のみコール可");
            this.mMapIndex += 1;
            return;
        }

        public void InitSkillMap(Unit self)
        {
            AIAction action;
            List<AbilityData> list;
            int num;
            AbilityData data;
            int num2;
            this.mSkillMap.Clear();
            this.mSkillMap.owner = self;
            this.mSkillMap.skillSeed = this.mRand.GetSeed();
            action = self.GetCurrentAIAction();
            this.mSkillMap.SetAction(action);
            list = self.BattleAbilitys;
            num = 0;
            goto Label_009D;
        Label_004E:
            data = list[num];
            if (data == null)
            {
                goto Label_0099;
            }
            num2 = 0;
            goto Label_0087;
        Label_0064:
            this.mSkillMap.allSkills.Add(data.Skills[num2]);
            num2 += 1;
        Label_0087:
            if (num2 < data.Skills.Count)
            {
                goto Label_0064;
            }
        Label_0099:
            num += 1;
        Label_009D:
            if (num < list.Count)
            {
                goto Label_004E;
            }
            if (self.GetAttackSkill() == null)
            {
                goto Label_00CA;
            }
            this.mSkillMap.allSkills.Add(self.GetAttackSkill());
        Label_00CA:
            if (self.AIForceSkill == null)
            {
                goto Label_00EB;
            }
            this.mSkillMap.allSkills.Add(self.AIForceSkill);
        Label_00EB:
            this.RefreshUseSkillMap(self, 0);
            return;
        }

        private void InitWeather()
        {
            WeatherSetParam param;
            param = null;
            if (this.mQuestParam == null)
            {
                goto Label_0023;
            }
            param = MonoSingleton<GameManager>.Instance.GetWeatherSetParam(this.mQuestParam.WeatherSetId);
        Label_0023:
            WeatherData.Initialize(param, this.mQuestParam.IsWeatherNoChange == 0);
            return;
        }

        private void InternalBattlePassiveSkill(Unit self, SkillData skill, bool is_duplicate, BuffEffect[] buff_effects)
        {
            MapEffectParam param;
            int num;
            int num2;
            if (skill == null)
            {
                goto Label_0011;
            }
            if (skill.IsPassiveSkill() != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            if (skill.Condition != 2)
            {
                goto Label_004D;
            }
            param = MonoSingleton<GameManager>.Instance.GetMapEffectParam(this.mQuestParam.MapEffectId);
            if (param != null)
            {
                goto Label_003B;
            }
            return;
        Label_003B:
            if (param.IsValidSkill(skill.SkillID) != null)
            {
                goto Label_004D;
            }
            return;
        Label_004D:
            num = 0;
            goto Label_0097;
        Label_0054:
            if (skill.Condition != 4)
            {
                goto Label_007C;
            }
            if (ConceptCardUtility.IsEnableCardSkillForUnit(this.Player[num], skill) != null)
            {
                goto Label_007C;
            }
            goto Label_0093;
        Label_007C:
            this.InternalBattlePassiveSkill(self, this.Player[num], skill, is_duplicate, buff_effects);
        Label_0093:
            num += 1;
        Label_0097:
            if (num < this.Player.Count)
            {
                goto Label_0054;
            }
            num2 = 0;
            goto Label_00F2;
        Label_00AF:
            if (skill.Condition != 4)
            {
                goto Label_00D7;
            }
            if (ConceptCardUtility.IsEnableCardSkillForUnit(this.Enemys[num2], skill) != null)
            {
                goto Label_00D7;
            }
            goto Label_00EE;
        Label_00D7:
            this.InternalBattlePassiveSkill(self, this.Enemys[num2], skill, is_duplicate, buff_effects);
        Label_00EE:
            num2 += 1;
        Label_00F2:
            if (num2 < this.Enemys.Count)
            {
                goto Label_00AF;
            }
            return;
        }

        private void InternalBattlePassiveSkill(Unit self, Unit target, SkillData skill, bool is_duplicate, BuffEffect[] buff_effects)
        {
            bool flag;
            BuffEffect effect;
            SkillEffectTypes types;
            if (target == null)
            {
                goto Label_001C;
            }
            if (target.IsGimmick == null)
            {
                goto Label_001D;
            }
            if (target.IsBreakObj != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            if (target.IsUnitFlag(0x1000000) == null)
            {
                goto Label_002E;
            }
            return;
        Label_002E:
            if (self.IsSub == null)
            {
                goto Label_0045;
            }
            if (skill.IsSubActuate() != null)
            {
                goto Label_0045;
            }
            return;
        Label_0045:
            if (this.CheckSkillTarget(self, target, skill) != null)
            {
                goto Label_0054;
            }
            return;
        Label_0054:
            if (is_duplicate != null)
            {
                goto Label_0068;
            }
            if (target.ContainsSkillAttachment(skill) == null)
            {
                goto Label_0068;
            }
            return;
        Label_0068:
            types = skill.EffectType;
            switch ((types - 1))
            {
                case 0:
                    goto Label_00A9;

                case 1:
                    goto Label_008F;

                case 2:
                    goto Label_008F;

                case 3:
                    goto Label_008F;

                case 4:
                    goto Label_00A9;

                case 5:
                    goto Label_00A9;
            }
        Label_008F:
            switch ((types - 11))
            {
                case 0:
                    goto Label_00A9;

                case 1:
                    goto Label_0144;

                case 2:
                    goto Label_00A9;
            }
            goto Label_0144;
        Label_00A9:
            flag = string.IsNullOrEmpty(skill.SkillParam.tokkou) == 0;
            effect = skill.GetBuffEffect(0);
            if (effect == null)
            {
                goto Label_010D;
            }
            if (effect.param == null)
            {
                goto Label_010D;
            }
            if (effect.param.mAppType != null)
            {
                goto Label_010B;
            }
            if (effect.param.mEffRange != null)
            {
                goto Label_010B;
            }
            if (effect.param.mIsUpBuff == null)
            {
                goto Label_010D;
            }
        Label_010B:
            flag = 1;
        Label_010D:
            if (skill.Target != null)
            {
                goto Label_012E;
            }
            if (skill.Condition != null)
            {
                goto Label_012E;
            }
            if (flag != null)
            {
                goto Label_012E;
            }
            goto Label_0145;
        Label_012E:
            this.BuffSkill(1, self, target, skill, 1, null, 0, is_duplicate, buff_effects);
            goto Label_0145;
        Label_0144:
            return;
        Label_0145:
            this.CondSkill(1, self, target, skill, 1, null, 0, 0);
            return;
        }

        private void InternalLogUnitEnd()
        {
            LogUnitEnd end;
            this.Log<LogUnitEnd>().self = this.CurrentUnit;
            return;
        }

        private unsafe void InternalReactionSkill(ESkillTiming timing, Unit attacker, Unit defender, Unit main_target, SkillData received_skill, int damage, bool is_forced, List<LogSkill> results, bool is_main_target)
        {
            int num;
            SkillData data;
            Unit unit;
            Unit unit2;
            int num2;
            int num3;
            LogSkill.Reflection reflection;
            List<Unit> list;
            ShotTarget target;
            int num4;
            int num5;
            int num6;
            LogSkill skill;
            int num7;
            SkillEffectTypes types;
            DamageTypes types2;
            ESkillTarget target2;
            num = 0;
            goto Label_0490;
        Label_0007:
            data = defender.BattleSkills[num];
            if (timing == data.Timing)
            {
                goto Label_0025;
            }
            goto Label_048C;
        Label_0025:
            if (defender.IsEnableReactionSkill(data) != null)
            {
                goto Label_0036;
            }
            goto Label_048C;
        Label_0036:
            types = data.EffectType;
            switch ((types - 0x10))
            {
                case 0:
                    goto Label_0097;

                case 1:
                    goto Label_0097;

                case 2:
                    goto Label_0064;

                case 3:
                    goto Label_0064;

                case 4:
                    goto Label_0064;

                case 5:
                    goto Label_0097;

                case 6:
                    goto Label_0097;
            }
        Label_0064:
            switch ((types - 7))
            {
                case 0:
                    goto Label_0097;

                case 1:
                    goto Label_007D;

                case 2:
                    goto Label_007D;

                case 3:
                    goto Label_0097;
            }
        Label_007D:
            switch ((types - 1))
            {
                case 0:
                    goto Label_0097;

                case 1:
                    goto Label_009C;

                case 2:
                    goto Label_0097;
            }
            goto Label_009C;
        Label_0097:
            goto Label_048C;
        Label_009C:;
        Label_00A1:
            if (data.IsAllDamageReaction() != null)
            {
                goto Label_00C9;
            }
            if (received_skill.IsNormalAttack() != null)
            {
                goto Label_00BD;
            }
            goto Label_048C;
        Label_00BD:
            if (is_main_target != null)
            {
                goto Label_00C9;
            }
            goto Label_048C;
        Label_00C9:
            switch ((data.ReactionDamageType - 1))
            {
                case 0:
                    goto Label_013B;

                case 1:
                    goto Label_00EB;

                case 2:
                    goto Label_0113;
            }
            goto Label_0163;
        Label_00EB:
            if (received_skill.IsPhysicalAttack() == null)
            {
                goto Label_048C;
            }
            if (data.IsReactionDet(received_skill.AttackDetailType) != null)
            {
                goto Label_0168;
            }
            goto Label_048C;
            goto Label_0168;
        Label_0113:
            if (received_skill.IsMagicalAttack() == null)
            {
                goto Label_048C;
            }
            if (data.IsReactionDet(received_skill.AttackDetailType) != null)
            {
                goto Label_0168;
            }
            goto Label_048C;
            goto Label_0168;
        Label_013B:
            if (received_skill.IsDamagedSkill() == null)
            {
                goto Label_048C;
            }
            if (data.IsReactionDet(received_skill.AttackDetailType) != null)
            {
                goto Label_0168;
            }
            goto Label_048C;
            goto Label_0168;
        Label_0163:
            goto Label_048C;
        Label_0168:
            if (data.UseCondition == null)
            {
                goto Label_0198;
            }
            if (data.UseCondition.type == null)
            {
                goto Label_0198;
            }
            if (data.UseCondition.unlock != null)
            {
                goto Label_0198;
            }
            goto Label_048C;
        Label_0198:
            unit = defender;
            unit2 = null;
            switch (data.Target)
            {
                case 0:
                    goto Label_01C8;

                case 1:
                    goto Label_01C8;

                case 2:
                    goto Label_01CF;

                case 3:
                    goto Label_01CF;

                case 4:
                    goto Label_01CF;

                case 5:
                    goto Label_01D6;
            }
            goto Label_01D7;
        Label_01C8:
            unit2 = defender;
            goto Label_01F2;
        Label_01CF:
            unit2 = attacker;
            goto Label_01F2;
        Label_01D6:
            return;
        Label_01D7:
            DebugUtility.LogError("リアクションスキル\"" + data.Name + "\"に不相応なスキル効果対象が設定されている");
            return;
        Label_01F2:
            if (this.CheckSkillCondition(unit, data) != null)
            {
                goto Label_0204;
            }
            goto Label_048C;
        Label_0204:
            num2 = data.EffectRate;
            if (num2 <= 0)
            {
                goto Label_0242;
            }
            if (num2 >= 100)
            {
                goto Label_0242;
            }
            num3 = this.GetRandom() % 100;
            if (num3 <= num2)
            {
                goto Label_0242;
            }
            if (is_forced != null)
            {
                goto Label_0242;
            }
            goto Label_048C;
        Label_0242:
            reflection = null;
            if (data.EffectType != 9)
            {
                goto Label_0262;
            }
            reflection = new LogSkill.Reflection();
            reflection.damage = damage;
        Label_0262:
            list = null;
            target = null;
            num4 = unit2.x;
            num5 = unit2.y;
            if (unit.GetAttackRangeMax(data) <= 0)
            {
                goto Label_02E3;
            }
            this.CreateSelectGridMap(unit, unit.x, unit.y, data, &this.mRangeMap, 0);
            list = this.SearchTargetsInGridMap(unit, data, this.mRangeMap);
            if (list.Contains(unit2) != null)
            {
                goto Label_02C7;
            }
            goto Label_048C;
        Label_02C7:
            list.Clear();
            this.GetExecuteSkillLineTarget(unit, num4, num5, data, &list, &target);
            goto Label_0335;
        Label_02E3:
            num4 = unit.x;
            num5 = unit.y;
            this.CreateScopeGridMap(unit, unit.x, unit.y, num4, num5, data, &this.mScopeMap, 0);
            list = this.SearchTargetsInGridMap(unit, data, this.mScopeMap);
            if (list.Contains(unit2) != null)
            {
                goto Label_0335;
            }
            goto Label_048C;
        Label_0335:
            if (list == null)
            {
                goto Label_048C;
            }
            if (list.Count != null)
            {
                goto Label_034D;
            }
            goto Label_048C;
        Label_034D:
            skill = null;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_0368;
            }
            skill = new LogSkill();
            goto Label_0370;
        Label_0368:
            skill = this.Log<LogSkill>();
        Label_0370:
            skill.self = unit;
            skill.skill = data;
            &skill.pos.x = num4;
            &skill.pos.y = num5;
            skill.reflect = reflection;
            skill.is_append = data.IsCutin() == 0;
            skill.CauseOfReaction = attacker;
            if (target == null)
            {
                goto Label_0426;
            }
            &skill.pos.x = target.end.x;
            &skill.pos.y = target.end.y;
            skill.rad = (int) (target.rad * 100.0);
            skill.height = (int) (target.height * 100.0);
        Label_0426:
            num7 = 0;
            goto Label_0446;
        Label_042E:
            skill.SetSkillTarget(defender, list[num7]);
            num7 += 1;
        Label_0446:
            if (num7 < list.Count)
            {
                goto Label_042E;
            }
            this.ExecuteSkill(timing, skill, data);
            if (results == null)
            {
                goto Label_046E;
            }
            results.Add(skill);
        Label_046E:
            if (data.SkillParam.count <= 0)
            {
                goto Label_048C;
            }
            defender.UpdateSkillUseCount(data, -1);
        Label_048C:
            num += 1;
        Label_0490:
            if (num < defender.BattleSkills.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private void InvokeSkillBuffCond(Unit unit, SkillData skill, ESkillTiming timing)
        {
            BuffEffect effect;
            BuffEffect effect2;
            CondEffect effect3;
            CondEffect effect4;
            int num;
            int num2;
            effect = skill.GetBuffEffect(0);
            effect2 = skill.GetBuffEffect(1);
            effect3 = skill.GetCondEffect(0);
            effect4 = skill.GetCondEffect(1);
            if (effect == null)
            {
                goto Label_0044;
            }
            if (effect.param == null)
            {
                goto Label_0044;
            }
            if (effect.param.chk_timing != 1)
            {
                goto Label_0044;
            }
            effect = null;
        Label_0044:
            if (effect2 == null)
            {
                goto Label_0068;
            }
            if (effect2.param == null)
            {
                goto Label_0068;
            }
            if (effect2.param.chk_timing != 1)
            {
                goto Label_0068;
            }
            effect2 = null;
        Label_0068:
            if (effect3 == null)
            {
                goto Label_008C;
            }
            if (effect3.param == null)
            {
                goto Label_008C;
            }
            if (effect3.param.chk_timing != 1)
            {
                goto Label_008C;
            }
            effect3 = null;
        Label_008C:
            if (effect4 == null)
            {
                goto Label_00B0;
            }
            if (effect4.param == null)
            {
                goto Label_00B0;
            }
            if (effect4.param.chk_timing != 1)
            {
                goto Label_00B0;
            }
            effect4 = null;
        Label_00B0:
            if (effect != null)
            {
                goto Label_00C9;
            }
            if (effect2 != null)
            {
                goto Label_00C9;
            }
            if (effect3 != null)
            {
                goto Label_00C9;
            }
            if (effect4 != null)
            {
                goto Label_00C9;
            }
            return;
        Label_00C9:
            num = skill.EffectRate;
            if (num <= 0)
            {
                goto Label_00FC;
            }
            if (num >= 100)
            {
                goto Label_00FC;
            }
            num2 = this.GetRandom() % 100;
            if (num2 <= num)
            {
                goto Label_00FC;
            }
            return;
        Label_00FC:
            if (effect == null)
            {
                goto Label_0111;
            }
            this.BuffSkill(timing, unit, unit, skill, 0, null, 0, 0, null);
        Label_0111:
            if (effect2 == null)
            {
                goto Label_0126;
            }
            this.BuffSkill(timing, unit, unit, skill, 0, null, 1, 0, null);
        Label_0126:
            if (effect3 == null)
            {
                goto Label_013A;
            }
            this.CondSkill(timing, unit, unit, skill, 0, null, 0, 1);
        Label_013A:
            if (effect4 == null)
            {
                goto Label_014E;
            }
            this.CondSkill(timing, unit, unit, skill, 0, null, 1, 1);
        Label_014E:
            return;
        }

        public bool IsAllAlive()
        {
            int num;
            Unit unit;
            num = 0;
            goto Label_004B;
        Label_0007:
            unit = this.Units[num];
            if (unit.Side != null)
            {
                goto Label_0047;
            }
            if (unit.IsPartyMember == null)
            {
                goto Label_0047;
            }
            if (unit.IsDead == null)
            {
                goto Label_0047;
            }
            if (unit.IsUnitFlag(0x80000) != null)
            {
                goto Label_0047;
            }
            return 0;
        Label_0047:
            num += 1;
        Label_004B:
            if (num < this.Units.Count)
            {
                goto Label_0007;
            }
            return 1;
        }

        private bool IsAllDead(EUnitSide side)
        {
            bool flag;
            int num;
            Unit unit;
            int num2;
            Unit unit2;
            flag = 1;
            if (side != null)
            {
                goto Label_005F;
            }
            num = 0;
            goto Label_0049;
        Label_000F:
            unit = this.mPlayer[num];
            if (unit.IsEntry != null)
            {
                goto Label_002C;
            }
            goto Label_0045;
        Label_002C:
            if (unit.IsGimmick == null)
            {
                goto Label_003C;
            }
            goto Label_0045;
        Label_003C:
            flag &= unit.IsDeadCondition();
        Label_0045:
            num += 1;
        Label_0049:
            if (num < this.mPlayer.Count)
            {
                goto Label_000F;
            }
            goto Label_00CA;
        Label_005F:
            if (side != 1)
            {
                goto Label_00CA;
            }
            num2 = 0;
            goto Label_00B2;
        Label_006D:
            unit2 = this.mEnemys[this.MapIndex][num2];
            if (unit2.IsEntry != null)
            {
                goto Label_0093;
            }
            goto Label_00AE;
        Label_0093:
            if (unit2.IsGimmick == null)
            {
                goto Label_00A4;
            }
            goto Label_00AE;
        Label_00A4:
            flag &= unit2.IsDeadCondition();
        Label_00AE:
            num2 += 1;
        Label_00B2:
            if (num2 < this.mEnemys[this.MapIndex].Count)
            {
                goto Label_006D;
            }
        Label_00CA:
            return flag;
        }

        public unsafe bool IsAllowBreakObjEntryMax()
        {
            List<Unit> list;
            if (<>f__am$cache78 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache78 = new Predicate<Unit>(BattleCore.<IsAllowBreakObjEntryMax>m__63);
        Label_001E:
            return (this.mUnits.FindAll(<>f__am$cache78).Count < &GameSettings.Instance.Quest.BreakObjAllowEntryMax);
        }

        public bool IsBattleFlag(EBattleFlag tgt)
        {
            return (((this.mBtlFlags & (1 << (tgt & 0x1f))) == 0) == 0);
        }

        public unsafe bool IsBonusObjectiveComplete(QuestBonusObjective bonus, ref int takeoverProgress)
        {
            char[] chArray4;
            char[] chArray3;
            char[] chArray2;
            char[] chArray1;
            int num;
            int num2;
            int num3;
            int num4;
            EElement element;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            bool flag;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            bool flag2;
            Unit unit2;
            List<Unit>.Enumerator enumerator2;
            int num15;
            int num16;
            int num17;
            int num18;
            int num19;
            int num20;
            int num21;
            int num22;
            int num23;
            int num24;
            int num25;
            string[] strArray;
            string[] strArray2;
            string str;
            int num26;
            int num27;
            string str2;
            bool flag3;
            Unit unit3;
            List<Unit>.Enumerator enumerator3;
            UnitParam param;
            int num28;
            string[] strArray3;
            int num29;
            UnitParam param2;
            int num30;
            string[] strArray4;
            int num31;
            int num32;
            Unit unit4;
            List<Unit>.Enumerator enumerator4;
            int num33;
            int num34;
            int num35;
            int num36;
            int num37;
            int num38;
            int num39;
            int num40;
            int num41;
            ItemParam param3;
            int num42;
            int num43;
            int num44;
            int num45;
            int num46;
            EMissionType type;
            bool flag4;
            switch (bonus.Type)
            {
                case 0:
                    goto Label_049F;

                case 1:
                    goto Label_014C;

                case 2:
                    goto Label_0179;

                case 3:
                    goto Label_0E11;

                case 4:
                    goto Label_0122;

                case 5:
                    goto Label_00F8;

                case 6:
                    goto Label_050E;

                case 7:
                    goto Label_01D7;

                case 8:
                    goto Label_0278;

                case 9:
                    goto Label_0618;

                case 10:
                    goto Label_06FF;

                case 11:
                    goto Label_0755;

                case 12:
                    goto Label_0780;

                case 13:
                    goto Label_07D6;

                case 14:
                    goto Label_07AB;

                case 15:
                    goto Label_082C;

                case 0x10:
                    goto Label_0801;

                case 0x11:
                    goto Label_0857;

                case 0x12:
                    goto Label_088C;

                case 0x13:
                    goto Label_08B7;

                case 20:
                    goto Label_08DF;

                case 0x15:
                    goto Label_0948;

                case 0x16:
                    goto Label_09C8;

                case 0x17:
                    goto Label_0A4E;

                case 0x18:
                    goto Label_0AD4;

                case 0x19:
                    goto Label_068A;

                case 0x1a:
                    goto Label_02F4;

                case 0x1b:
                    goto Label_0E11;

                case 0x1c:
                    goto Label_03BC;

                case 0x1d:
                    goto Label_03D0;

                case 30:
                    goto Label_03E4;

                case 0x1f:
                    goto Label_03F8;

                case 0x20:
                    goto Label_0432;

                case 0x21:
                    goto Label_0446;

                case 0x22:
                    goto Label_045A;

                case 0x23:
                    goto Label_046E;

                case 0x24:
                    goto Label_040C;

                case 0x25:
                    goto Label_041F;

                case 0x26:
                    goto Label_0482;

                case 0x27:
                    goto Label_0B56;

                case 40:
                    goto Label_0D37;

                case 0x29:
                    goto Label_0CED;

                case 0x2a:
                    goto Label_0587;

                case 0x2b:
                    goto Label_0CDB;

                case 0x2c:
                    goto Label_015E;

                case 0x2d:
                    goto Label_03AA;

                case 0x2e:
                    goto Label_03B3;

                case 0x2f:
                    goto Label_01A3;

                case 0x30:
                    goto Label_08BE;

                case 0x31:
                    goto Label_0C91;

                case 50:
                    goto Label_072A;

                case 0x33:
                    goto Label_0BBF;

                case 0x34:
                    goto Label_0C28;

                case 0x35:
                    goto Label_0D96;

                case 0x36:
                    goto Label_0DB1;

                case 0x37:
                    goto Label_0DCC;

                case 0x38:
                    goto Label_0DEA;
            }
            goto Label_0E11;
        Label_00F8:
            *((int*) takeoverProgress) = this.mNumUsedItems;
            return ((int.TryParse(bonus.TypeParam, &num) == null) ? 0 : ((this.mNumUsedItems > num) == 0));
        Label_0122:
            *((int*) takeoverProgress) = this.mNumUsedSkills;
            return ((int.TryParse(bonus.TypeParam, &num2) == null) ? 0 : ((this.mNumUsedSkills > num2) == 0));
        Label_014C:
            if (this.IsAllAlive() == null)
            {
                goto Label_0159;
            }
            return 1;
        Label_0159:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_015E:
            return ((this.IsAllAlive() == null) ? 0 : ((this.ContinueCount > 0) == 0));
        Label_0179:
            *((int*) takeoverProgress) = this.mActionCount;
            return ((int.TryParse(bonus.TypeParam, &num3) == null) ? 0 : ((this.mActionCount > num3) == 0));
        Label_01A3:
            *((int*) takeoverProgress) = this.Leader.ActionCount;
            return ((int.TryParse(bonus.TypeParam, &num4) == null) ? 0 : ((this.Leader.ActionCount > num4) == 0));
        Label_01D7:
            element = 0;
        Label_01DA:
            try
            {
                element = (int) Enum.Parse(typeof(EElement), bonus.TypeParam, 1);
                goto Label_020A;
            }
            catch (Exception)
            {
            Label_01FC:
                flag4 = 0;
                goto Label_0E13;
            }
        Label_020A:
            num5 = 0;
            goto Label_0264;
        Label_0212:
            if (((this.Units[num5].Side != null) || (this.Units[num5].IsPartyMember == null)) || (this.Units[num5].Element == element))
            {
                goto Label_025E;
            }
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_025E:
            num5 += 1;
        Label_0264:
            if (num5 < this.Units.Count)
            {
                goto Label_0212;
            }
            return 1;
        Label_0278:
            num6 = 0;
            goto Label_02DD;
        Label_0280:
            if (((this.Units[num6].Side != null) || (this.Units[num6].IsPartyMember == null)) || ((this.Units[num6].UnitParam.iname == bonus.TypeParam) == null))
            {
                goto Label_02D7;
            }
            return 1;
        Label_02D7:
            num6 += 1;
        Label_02DD:
            if (num6 < this.Units.Count)
            {
                goto Label_0280;
            }
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_02F4:
            num7 = 0;
            goto Label_0393;
        Label_02FC:
            if (this.mStartingMembers.Contains(this.Units[num7]) == null)
            {
                goto Label_038D;
            }
            if (this.Units[num7] != this.Friend)
            {
                goto Label_0336;
            }
            goto Label_038D;
        Label_0336:
            if (((this.Units[num7].Side != null) || (this.Units[num7].IsPartyMember == null)) || ((this.Units[num7].UnitParam.iname == bonus.TypeParam) == null))
            {
                goto Label_038D;
            }
            return 1;
        Label_038D:
            num7 += 1;
        Label_0393:
            if (num7 < this.Units.Count)
            {
                goto Label_02FC;
            }
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_03AA:
            return this.IsMissionClearOnlyTargetUnitsConditions(bonus, 0);
        Label_03B3:
            return this.IsMissionClearOnlyTargetUnitsConditions(bonus, 1);
        Label_03BC:
            if (this.IsMissionClearArtifactTypeConditions(bonus, 0) == null)
            {
                goto Label_03CB;
            }
            return 1;
        Label_03CB:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_03D0:
            if (this.IsMissionClearArtifactTypeConditions(bonus, 1) == null)
            {
                goto Label_03DF;
            }
            return 1;
        Label_03DF:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_03E4:
            if (this.IsMissionClearPartyMemberJobConditions(bonus, 0) == null)
            {
                goto Label_03F3;
            }
            return 1;
        Label_03F3:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_03F8:
            if (this.IsMissionClearPartyMemberJobConditions(bonus, 1) == null)
            {
                goto Label_0407;
            }
            return 1;
        Label_0407:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_040C:
            if (this.IsMissionClearOnlyHeroConditions(0) == null)
            {
                goto Label_041A;
            }
            return 1;
        Label_041A:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_041F:
            if (this.IsMissionClearOnlyHeroConditions(1) == null)
            {
                goto Label_042D;
            }
            return 1;
        Label_042D:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_0432:
            if (this.IsMissionClearUnitBirthplaceConditions(bonus, 0) == null)
            {
                goto Label_0441;
            }
            return 1;
        Label_0441:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_0446:
            if (this.IsMissionClearUnitBirthplaceConditions(bonus, 1) == null)
            {
                goto Label_0455;
            }
            return 1;
        Label_0455:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_045A:
            if (this.IsMissionClearUnitSexConditions(bonus, 0) == null)
            {
                goto Label_0469;
            }
            return 1;
        Label_0469:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_046E:
            if (this.IsMissionClearUnitSexConditions(bonus, 1) == null)
            {
                goto Label_047D;
            }
            return 1;
        Label_047D:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_0482:
            if ((this.mFinisherIname == bonus.TypeParam) == null)
            {
                goto Label_049A;
            }
            return 1;
        Label_049A:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_049F:
            num8 = 0;
            goto Label_04FA;
        Label_04A7:
            if ((this.Units[num8].Side != 1) || ((this.Units[num8].IsDead != null) && (this.Units[num8].IsUnitFlag(0x100000) == null)))
            {
                goto Label_04F4;
            }
            return 0;
        Label_04F4:
            num8 += 1;
        Label_04FA:
            if (num8 < this.Units.Count)
            {
                goto Label_04A7;
            }
            return 1;
        Label_050E:
            num9 = 0;
            num10 = 0;
            goto Label_0553;
        Label_0519:
            if ((this.Units[num10].Side != null) || (this.Units[num10].IsPartyMember == null))
            {
                goto Label_054D;
            }
            num9 += 1;
        Label_054D:
            num10 += 1;
        Label_0553:
            if (num10 < this.Units.Count)
            {
                goto Label_0519;
            }
            if ((int.TryParse(bonus.TypeParam, &num11) == null) || (num9 > num11))
            {
                goto Label_0582;
            }
            return 1;
        Label_0582:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_0587:
            num12 = 0;
            num13 = 0;
            goto Label_05E4;
        Label_0592:
            if (((this.Units[num13].Side != null) || (this.Units[num13].IsPartyMember == null)) || (this.Units[num13] == this.Friend))
            {
                goto Label_05DE;
            }
            num12 += 1;
        Label_05DE:
            num13 += 1;
        Label_05E4:
            if (num13 < this.Units.Count)
            {
                goto Label_0592;
            }
            if ((int.TryParse(bonus.TypeParam, &num14) == null) || (num12 > num14))
            {
                goto Label_0613;
            }
            return 1;
        Label_0613:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_0618:
            flag = this.Friend == null;
            if ((flag == null) || (this.IsOrdeal == null))
            {
                goto Label_0687;
            }
            enumerator = this.Units.GetEnumerator();
        Label_0642:
            try
            {
                goto Label_0669;
            Label_0647:
                unit = &enumerator.Current;
                if (unit.IsUnitFlag(0x2000000) == null)
                {
                    goto Label_0669;
                }
                flag = 0;
                goto Label_0675;
            Label_0669:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0647;
                }
            Label_0675:
                goto Label_0687;
            }
            finally
            {
            Label_067A:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0687:
            return flag;
        Label_068A:
            flag2 = (this.Friend == null) == 0;
            if ((flag2 != null) || (this.IsOrdeal == null))
            {
                goto Label_06FC;
            }
            enumerator2 = this.Units.GetEnumerator();
        Label_06B7:
            try
            {
                goto Label_06DE;
            Label_06BC:
                unit2 = &enumerator2.Current;
                if (unit2.IsUnitFlag(0x2000000) == null)
                {
                    goto Label_06DE;
                }
                flag2 = 1;
                goto Label_06EA;
            Label_06DE:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_06BC;
                }
            Label_06EA:
                goto Label_06FC;
            }
            finally
            {
            Label_06EF:
                ((List<Unit>.Enumerator) enumerator2).Dispose();
            }
        Label_06FC:
            return flag2;
        Label_06FF:
            *((int*) takeoverProgress) = this.mMaxKillstreak;
            return ((int.TryParse(bonus.TypeParam, &num15) == null) ? 0 : ((this.mMaxKillstreak < num15) == 0));
        Label_072A:
            num16 = this.GetTotalKillCount();
            *((int*) takeoverProgress) = num16;
            return ((int.TryParse(bonus.TypeParam, &num17) == null) ? 0 : ((num16 < num17) == 0));
        Label_0755:
            *((int*) takeoverProgress) = this.mTotalHeal;
            return ((int.TryParse(bonus.TypeParam, &num18) == null) ? 0 : ((this.mTotalHeal > num18) == 0));
        Label_0780:
            *((int*) takeoverProgress) = this.mTotalHeal;
            return ((int.TryParse(bonus.TypeParam, &num19) == null) ? 0 : ((this.mTotalHeal < num19) == 0));
        Label_07AB:
            *((int*) takeoverProgress) = this.mTotalDamagesTaken;
            return ((int.TryParse(bonus.TypeParam, &num20) == null) ? 0 : ((this.mTotalDamagesTaken < num20) == 0));
        Label_07D6:
            *((int*) takeoverProgress) = this.mTotalDamagesTaken;
            return ((int.TryParse(bonus.TypeParam, &num21) == null) ? 0 : ((this.mTotalDamagesTaken > num21) == 0));
        Label_0801:
            *((int*) takeoverProgress) = this.mTotalDamages;
            return ((int.TryParse(bonus.TypeParam, &num22) == null) ? 0 : ((this.mTotalDamages < num22) == 0));
        Label_082C:
            *((int*) takeoverProgress) = this.mTotalDamages;
            return ((int.TryParse(bonus.TypeParam, &num23) == null) ? 0 : ((this.mTotalDamages > num23) == 0));
        Label_0857:
            *((int*) takeoverProgress) = this.mClockTimeTotal;
            return ((int.TryParse(bonus.TypeParam, &num24) == null) ? 0 : ((this.mClockTimeTotal > num24) == 0));
        Label_088C:
            *((int*) takeoverProgress) = this.mContinueCount;
            return ((int.TryParse(bonus.TypeParam, &num25) == null) ? 0 : ((this.mContinueCount > num25) == 0));
        Label_08B7:
            return this.IsNPCAllAlive();
        Label_08BE:
            chArray1 = new char[] { 0x2c };
            strArray = bonus.TypeParam.Split(chArray1);
            return this.IsNPCAlive(strArray);
        Label_08DF:
            chArray2 = new char[] { 0x2c };
            strArray2 = bonus.TypeParam.Split(chArray2);
            if (((int) strArray2.Length) >= 2)
            {
                goto Label_0906;
            }
            goto Label_0E11;
        Label_0906:
            str = strArray2[0].Trim();
            if (this.mMaxTargetKillstreakDict.TryGetValue(str, &num26) == null)
            {
                goto Label_0946;
            }
            *((int*) takeoverProgress) = num26;
            return ((int.TryParse(strArray2[1], &num27) == null) ? 0 : ((num26 < num27) == 0));
        Label_0946:
            return 0;
        Label_0948:
            str2 = bonus.TypeParam.Trim();
            flag3 = 0;
            enumerator3 = this.Units.GetEnumerator();
        Label_0965:
            try
            {
                goto Label_09A4;
            Label_096A:
                unit3 = &enumerator3.Current;
                if (unit3.Side != 1)
                {
                    goto Label_09A4;
                }
                if ((unit3.UnitParam.iname == str2) == null)
                {
                    goto Label_09A4;
                }
                flag3 |= unit3.IsDead;
            Label_09A4:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_096A;
                }
                goto Label_09C2;
            }
            finally
            {
            Label_09B5:
                ((List<Unit>.Enumerator) enumerator3).Dispose();
            }
        Label_09C2:
            return (flag3 == 0);
        Label_09C8:
            param = null;
            num28 = 1;
            chArray3 = new char[] { 0x2c };
            strArray3 = bonus.TypeParam.Split(chArray3);
            if (strArray3 == null)
            {
                goto Label_0A27;
            }
            if (((int) strArray3.Length) < 1)
            {
                goto Label_0A0C;
            }
            param = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray3[0].Trim());
        Label_0A0C:
            if (((int) strArray3.Length) < 2)
            {
                goto Label_0A27;
            }
            int.TryParse(strArray3[1].Trim(), &num28);
        Label_0A27:
            if (param == null)
            {
                goto Label_0A4C;
            }
            num29 = this.getUnitDeadCount(param.iname);
            *((int*) takeoverProgress) = num29;
            if (num29 > num28)
            {
                goto Label_0A4C;
            }
            return 1;
        Label_0A4C:
            return 0;
        Label_0A4E:
            param2 = null;
            num30 = 1;
            chArray4 = new char[] { 0x2c };
            strArray4 = bonus.TypeParam.Split(chArray4);
            if (strArray4 == null)
            {
                goto Label_0AAD;
            }
            if (((int) strArray4.Length) < 1)
            {
                goto Label_0A92;
            }
            param2 = MonoSingleton<GameManager>.Instance.GetUnitParam(strArray4[0].Trim());
        Label_0A92:
            if (((int) strArray4.Length) < 2)
            {
                goto Label_0AAD;
            }
            int.TryParse(strArray4[1].Trim(), &num30);
        Label_0AAD:
            if (param2 == null)
            {
                goto Label_0AD2;
            }
            num31 = this.getUnitDeadCount(param2.iname);
            *((int*) takeoverProgress) = num31;
            if (num31 < num30)
            {
                goto Label_0AD2;
            }
            return 1;
        Label_0AD2:
            return 0;
        Label_0AD4:
            num32 = 0;
            enumerator4 = this.Units.GetEnumerator();
        Label_0AE4:
            try
            {
                goto Label_0B2F;
            Label_0AE9:
                unit4 = &enumerator4.Current;
                if (unit4.IsUnitFlag(0x100000) != null)
                {
                    goto Label_0B08;
                }
                goto Label_0B2F;
            Label_0B08:
                if ((unit4.UnitParam.iname != bonus.TypeParam) == null)
                {
                    goto Label_0B29;
                }
                goto Label_0B2F;
            Label_0B29:
                num32 += 1;
            Label_0B2F:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_0AE9;
                }
                goto Label_0B4D;
            }
            finally
            {
            Label_0B40:
                ((List<Unit>.Enumerator) enumerator4).Dispose();
            }
        Label_0B4D:
            return ((num32 == 0) == 0);
        Label_0B56:
            num33 = 0;
            num34 = 0;
            goto Label_0B8A;
        Label_0B61:
            if (this.mTreasures[num34].EventTrigger.Count > 0)
            {
                goto Label_0B84;
            }
            num33 += 1;
        Label_0B84:
            num34 += 1;
        Label_0B8A:
            if (num34 < this.mTreasures.Count)
            {
                goto Label_0B61;
            }
            *((int*) takeoverProgress) = num33;
            return ((int.TryParse(bonus.TypeParam, &num35) == null) ? 0 : ((num33 < num35) == 0));
        Label_0BBF:
            num36 = 0;
            num37 = 0;
            goto Label_0BF3;
        Label_0BCA:
            if (this.mGems[num37].EventTrigger.Count > 0)
            {
                goto Label_0BED;
            }
            num36 += 1;
        Label_0BED:
            num37 += 1;
        Label_0BF3:
            if (num37 < this.mGems.Count)
            {
                goto Label_0BCA;
            }
            *((int*) takeoverProgress) = num36;
            return ((int.TryParse(bonus.TypeParam, &num38) == null) ? 0 : ((num36 < num38) == 0));
        Label_0C28:
            num39 = 0;
            num40 = 0;
            goto Label_0C5C;
        Label_0C33:
            if (this.mGems[num40].EventTrigger.Count > 0)
            {
                goto Label_0C56;
            }
            num39 += 1;
        Label_0C56:
            num40 += 1;
        Label_0C5C:
            if (num40 < this.mGems.Count)
            {
                goto Label_0C33;
            }
            *((int*) takeoverProgress) = num39;
            return ((int.TryParse(bonus.TypeParam, &num41) == null) ? 0 : ((num39 > num41) == 0));
        Label_0C91:
            if (this.mSkillExecLogs.ContainsKey(bonus.TypeParam) == null)
            {
                goto Label_0CD9;
            }
            *((int*) takeoverProgress) = this.mSkillExecLogs[bonus.TypeParam].use_count;
            return (this.mSkillExecLogs[bonus.TypeParam].use_count > 0);
        Label_0CD9:
            return 0;
        Label_0CDB:
            if (this.mIsUseAutoPlayMode != null)
            {
                goto Label_0CE8;
            }
            return 1;
        Label_0CE8:
            *((int*) takeoverProgress) = 1;
            return 0;
        Label_0CED:
            if (this.mSkillExecLogs.ContainsKey(bonus.TypeParam) == null)
            {
                goto Label_0D35;
            }
            *((int*) takeoverProgress) = this.mSkillExecLogs[bonus.TypeParam].kill_count;
            return (this.mSkillExecLogs[bonus.TypeParam].kill_count > 0);
        Label_0D35:
            return 0;
        Label_0D37:
            param3 = MonoSingleton<GameManager>.Instance.GetItemParam(bonus.TypeParam);
            if (this.mSkillExecLogs.ContainsKey(param3.skill) == null)
            {
                goto Label_0D94;
            }
            *((int*) takeoverProgress) = this.mSkillExecLogs[param3.skill].kill_count;
            return (this.mSkillExecLogs[param3.skill].kill_count > 0);
        Label_0D94:
            return 0;
        Label_0D96:
            num42 = 0;
            int.TryParse(bonus.TypeParam, &num42);
            return this.IsTeamPartySizeMax(num42, 1);
        Label_0DB1:
            num43 = 0;
            int.TryParse(bonus.TypeParam, &num43);
            return this.IsTeamPartySizeMax(num43, 0);
        Label_0DCC:
            num44 = 0;
            int.TryParse(bonus.TypeParam, &num44);
            *((int*) takeoverProgress) = 1;
            return this.IsTeamPartySizeMax(num44, 0);
        Label_0DEA:
            num45 = this.GetPlayerUnitDeadCount();
            num46 = 0;
            int.TryParse(bonus.TypeParam, &num46);
            *((int*) takeoverProgress) = num45;
            return ((num45 > num46) == 0);
        Label_0E11:
            return 0;
        Label_0E13:
            return flag4;
        }

        private bool IsBuffDebuffEffectiveEnemies(Unit self, BuffTypes type)
        {
            int num;
            Unit unit;
            num = 0;
            goto Label_0069;
        Label_0007:
            unit = this.mUnits[num];
            if (unit.IsSub != null)
            {
                goto Label_0065;
            }
            if (unit.IsEntry == null)
            {
                goto Label_0065;
            }
            if (unit.IsDead != null)
            {
                goto Label_0065;
            }
            if (unit.IsGimmick == null)
            {
                goto Label_0045;
            }
            goto Label_0065;
        Label_0045:
            if (this.CheckEnemySide(self, unit) != null)
            {
                goto Label_0057;
            }
            goto Label_0065;
        Label_0057:
            if (unit.CheckActionSkillBuffAttachments(type) == null)
            {
                goto Label_0065;
            }
            return 1;
        Label_0065:
            num += 1;
        Label_0069:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        private bool IsCombinationAttack(SkillData skill)
        {
            return (((skill == null) || (skill.IsNormalAttack() == null)) ? 0 : (this.mHelperUnits.Count > 0));
        }

        private unsafe bool IsEnableUseSkillEffect(Unit self, SkillData skill, LogSkill log)
        {
            int num;
            int num2;
            Unit unit;
            int num3;
            int num4;
            bool flag;
            int num5;
            BuffAttachment attachment;
            List<BuffAttachment>.Enumerator enumerator;
            BuffEffect effect;
            int num6;
            BuffTypes types;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            CondEffect effect2;
            bool flag2;
            int num13;
            int num14;
            int num15;
            EUnitCondition condition;
            int num16;
            int num17;
            <IsEnableUseSkillEffect>c__AnonStorey1C2 storeyc;
            storeyc = new <IsEnableUseSkillEffect>c__AnonStorey1C2();
            storeyc.self = self;
            if (((storeyc.self != null) && (skill != null)) && (log != null))
            {
                goto Label_0029;
            }
            return 0;
        Label_0029:
            num = 0;
            storeyc.rage = storeyc.self.GetRageTarget();
            if (storeyc.rage == null)
            {
                goto Label_0076;
            }
            if (skill.IsDamagedSkill() != null)
            {
                goto Label_0057;
            }
            return 0;
        Label_0057:
            if (log.targets.Find(new Predicate<LogSkill.Target>(storeyc.<>m__68)) != null)
            {
                goto Label_0076;
            }
            return 0;
        Label_0076:
            num2 = 0;
            goto Label_06EA;
        Label_007D:
            unit = log.targets[num2].target;
            if (this.CheckSkillTargetAI(storeyc.self, unit, skill) != null)
            {
                goto Label_00A5;
            }
            return 0;
        Label_00A5:
            if (unit.IsBreakObj == null)
            {
                goto Label_00E6;
            }
            if ((skill.IsDamagedSkill() != null) || (unit.BreakObjSideType != null))
            {
                goto Label_00CB;
            }
            goto Label_06E6;
        Label_00CB:
            if ((skill.IsDamagedSkill() != null) || (skill.IsHealSkill() != null))
            {
                goto Label_00E6;
            }
            goto Label_06E6;
        Label_00E6:
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_0112;
            }
            if ((unit.IsBreakObj == null) || (unit.BreakObjClashType != 3))
            {
                goto Label_06E2;
            }
            goto Label_06E6;
            goto Label_06E2;
        Label_0112:
            if (skill.IsHealSkill() == null)
            {
                goto Label_0179;
            }
            if (Math.Max(Math.Min(log.targets[num2].GetTotalHpHeal(), unit.MaximumStatus.param.hp - unit.CurrentStatus.param.hp), 0) != null)
            {
                goto Label_06E2;
            }
            goto Label_06E6;
            goto Label_06E2;
        Label_0179:
            if (skill.IsSupportSkill() == null)
            {
                goto Label_0489;
            }
            if (((storeyc.self.AI == null) || (storeyc.self.AI.CheckFlag(0x100) == null)) || (log.targets.Find(new Predicate<LogSkill.Target>(storeyc.<>m__69)) != null))
            {
                goto Label_01D2;
            }
            goto Label_06E6;
        Label_01D2:
            if (skill.ControlChargeTimeValue == null)
            {
                goto Label_01E7;
            }
            goto Label_0484;
        Label_01E7:
            flag = 0;
            if (unit.BuffAttachments.Count <= 0)
            {
                goto Label_029D;
            }
            num5 = 0;
            enumerator = unit.BuffAttachments.GetEnumerator();
        Label_020B:
            try
            {
                goto Label_024C;
            Label_0210:
                attachment = &enumerator.Current;
                if (attachment.skill != null)
                {
                    goto Label_022A;
                }
                goto Label_024C;
            Label_022A:
                if ((attachment.skill.SkillID == skill.SkillID) == null)
                {
                    goto Label_024C;
                }
                num5 += 1;
            Label_024C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0210;
                }
                goto Label_026A;
            }
            finally
            {
            Label_025D:
                ((List<BuffAttachment>.Enumerator) enumerator).Dispose();
            }
        Label_026A:
            if (skill.DuplicateCount > 1)
            {
                goto Label_0288;
            }
            if (num5 <= 0)
            {
                goto Label_029D;
            }
            goto Label_06E6;
            goto Label_029D;
        Label_0288:
            if (num5 < skill.DuplicateCount)
            {
                goto Label_029A;
            }
            goto Label_06E6;
        Label_029A:
            flag = 1;
        Label_029D:
            effect = skill.GetBuffEffect(0);
            if (effect == null)
            {
                goto Label_06E6;
            }
            if (effect.CheckEnableBuffTarget(unit) != null)
            {
                goto Label_02BF;
            }
            goto Label_06E6;
        Label_02BF:
            num6 = 0;
            goto Label_0465;
        Label_02C7:
            types = effect.targets[num6].buffType;
            if (types != null)
            {
                goto Label_0367;
            }
            if ((unit.IsEnableBuffEffect(0) != null) || (effect.param.IsNoDisabled != null))
            {
                goto Label_0305;
            }
            goto Label_045F;
        Label_0305:
            num7 = (storeyc.self.AI == null) ? 0 : storeyc.self.AI.buff_border;
            if (Math.Max(100 - unit.CurrentStatus.enchant_resist.resist_buff, 0) > num7)
            {
                goto Label_03EE;
            }
            goto Label_045F;
            goto Label_03EE;
        Label_0367:
            if (types != 1)
            {
                goto Label_03EE;
            }
            if ((unit.IsEnableBuffEffect(1) != null) || (effect.param.IsNoDisabled != null))
            {
                goto Label_0391;
            }
            goto Label_045F;
        Label_0391:
            num9 = (storeyc.self.AI == null) ? 0 : storeyc.self.AI.buff_border;
            if (Math.Max(100 - unit.CurrentStatus.enchant_resist.resist_debuff, 0) > num9)
            {
                goto Label_03EE;
            }
            goto Label_045F;
        Label_03EE:
            num11 = unit.GetActionSkillBuffValue(effect.targets[num6].buffType, effect.targets[num6].calcType, effect.targets[num6].paramType);
            num12 = Math.Abs(effect.targets[num6].value);
            if (num11 >= num12)
            {
                goto Label_045F;
            }
            flag = 1;
            goto Label_0478;
        Label_045F:
            num6 += 1;
        Label_0465:
            if (num6 < effect.targets.Count)
            {
                goto Label_02C7;
            }
        Label_0478:
            if (flag != null)
            {
                goto Label_06E2;
            }
            goto Label_06E6;
        Label_0484:
            goto Label_06E2;
        Label_0489:
            if (skill.IsConditionSkill() == null)
            {
                goto Label_06E2;
            }
            effect2 = skill.GetCondEffect(0);
            if ((effect2 == null) || (effect2.param.conditions == null))
            {
                goto Label_06E6;
            }
            if (effect2.CheckEnableCondTarget(unit) != null)
            {
                goto Label_04C7;
            }
            goto Label_06E6;
        Label_04C7:
            flag2 = 0;
            if (skill.EffectType != 12)
            {
                goto Label_0521;
            }
            num13 = 0;
            goto Label_0507;
        Label_04DF:
            if (unit.CheckEnableCureCondition(effect2.param.conditions[num13]) == null)
            {
                goto Label_0501;
            }
            flag2 = 1;
            goto Label_051C;
        Label_0501:
            num13 += 1;
        Label_0507:
            if (num13 < ((int) effect2.param.conditions.Length))
            {
                goto Label_04DF;
            }
        Label_051C:
            goto Label_06D6;
        Label_0521:
            if (skill.EffectType != 11)
            {
                goto Label_0684;
            }
            num14 = (storeyc.self.AI == null) ? 0 : storeyc.self.AI.cond_border;
            if (num14 <= 0)
            {
                goto Label_058F;
            }
            if (effect2.rate <= 0)
            {
                goto Label_058F;
            }
            if (effect2.rate >= num14)
            {
                goto Label_058F;
            }
            goto Label_06E6;
        Label_058F:
            num15 = 0;
            goto Label_066A;
        Label_0597:
            condition = effect2.param.conditions[num15];
            if (AIUtility.IsFailCondition(storeyc.self, unit, condition) != null)
            {
                goto Label_05BE;
            }
            return 0;
        Label_05BE:
            if (condition != 0x4000L)
            {
                goto Label_05E8;
            }
            if (this.IsBuffDebuffEffectiveEnemies(storeyc.self, 0) != null)
            {
                goto Label_060D;
            }
            goto Label_0664;
            goto Label_060D;
        Label_05E8:
            if (condition != 0x8000L)
            {
                goto Label_060D;
            }
            if (this.IsBuffDebuffEffectiveEnemies(storeyc.self, 1) != null)
            {
                goto Label_060D;
            }
            goto Label_0664;
        Label_060D:
            if (unit.CheckEnableFailCondition(condition) == null)
            {
                goto Label_0664;
            }
            if (num14 <= 0)
            {
                goto Label_065C;
            }
            if (Math.Max(effect2.value - unit.CurrentStatus.enchant_resist[condition], 0) >= num14)
            {
                goto Label_065C;
            }
            goto Label_0664;
        Label_065C:
            flag2 = 1;
            goto Label_067F;
        Label_0664:
            num15 += 1;
        Label_066A:
            if (num15 < ((int) effect2.param.conditions.Length))
            {
                goto Label_0597;
            }
        Label_067F:
            goto Label_06D6;
        Label_0684:
            if (skill.EffectType != 13)
            {
                goto Label_06D6;
            }
            num17 = 0;
            goto Label_06C1;
        Label_0699:
            if (unit.IsDisableUnitCondition(effect2.param.conditions[num17]) != null)
            {
                goto Label_06BB;
            }
            flag2 = 1;
            goto Label_06D6;
        Label_06BB:
            num17 += 1;
        Label_06C1:
            if (num17 < ((int) effect2.param.conditions.Length))
            {
                goto Label_0699;
            }
        Label_06D6:
            if (flag2 != null)
            {
                goto Label_06E2;
            }
            goto Label_06E6;
        Label_06E2:
            num += 1;
        Label_06E6:
            num2 += 1;
        Label_06EA:
            if (num2 < log.targets.Count)
            {
                goto Label_007D;
            }
            return ((num == 0) == 0);
        }

        private bool IsFailCondSkillUseEnemies(Unit self, EUnitCondition condition)
        {
            int num;
            Unit unit;
            int num2;
            SkillData data;
            CondEffect effect;
            if (condition == 0x80000L)
            {
                goto Label_0048;
            }
            if (condition == 0x400000L)
            {
                goto Label_0048;
            }
            if (condition == 0x800000L)
            {
                goto Label_0048;
            }
            if (condition == 0x4000L)
            {
                goto Label_0048;
            }
            if (condition == 0x8000L)
            {
                goto Label_0048;
            }
            if (condition != 0x2000L)
            {
                goto Label_004A;
            }
        Label_0048:
            return 0;
        Label_004A:
            num = 0;
            goto Label_01BB;
        Label_0051:
            unit = this.mUnits[num];
            if (unit.IsSub != null)
            {
                goto Label_01B7;
            }
            if (unit.IsEntry == null)
            {
                goto Label_01B7;
            }
            if (unit.IsDead != null)
            {
                goto Label_01B7;
            }
            if (unit.IsGimmick == null)
            {
                goto Label_008F;
            }
            goto Label_01B7;
        Label_008F:
            if (unit.IsEnableSkillCondition(1) != null)
            {
                goto Label_00A0;
            }
            goto Label_01B7;
        Label_00A0:
            if (this.CheckEnemySide(self, unit) != null)
            {
                goto Label_00B2;
            }
            goto Label_01B7;
        Label_00B2:
            if (unit.BattleSkills == null)
            {
                goto Label_01B7;
            }
            num2 = 0;
            goto Label_01A6;
        Label_00C4:
            data = unit.BattleSkills[num2];
            if (unit.IsPartyMember != null)
            {
                goto Label_0121;
            }
            if (data.UseRate != null)
            {
                goto Label_00F1;
            }
            goto Label_01A2;
        Label_00F1:
            if (data.UseCondition == null)
            {
                goto Label_0121;
            }
            if (data.UseCondition.type == null)
            {
                goto Label_0121;
            }
            if (data.UseCondition.unlock != null)
            {
                goto Label_0121;
            }
            goto Label_01A2;
        Label_0121:
            effect = data.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_01A2;
            }
            if (effect.param == null)
            {
                goto Label_01A2;
            }
            if (effect.param.conditions != null)
            {
                goto Label_0153;
            }
            goto Label_01A2;
        Label_0153:
            if (effect.param.type == 2)
            {
                goto Label_0189;
            }
            if (effect.param.type == 3)
            {
                goto Label_0189;
            }
            if (effect.param.type != 4)
            {
                goto Label_01A2;
            }
        Label_0189:
            return ((Array.IndexOf<EUnitCondition>(effect.param.conditions, condition) == -1) == 0);
        Label_01A2:
            num2 += 1;
        Label_01A6:
            if (num2 < unit.BattleSkills.Count)
            {
                goto Label_00C4;
            }
        Label_01B7:
            num += 1;
        Label_01BB:
            if (num < this.mUnits.Count)
            {
                goto Label_0051;
            }
            return 0;
        }

        private bool IsFailTrickData(Unit unit, int x, int y)
        {
            SRPG.TrickMap.Data data;
            data = this.mTrickMap.GetData(x, y);
            if (data == null)
            {
                goto Label_0034;
            }
            if (data.IsVisual(unit) == null)
            {
                goto Label_0034;
            }
            if (data.IsVaild(unit) == null)
            {
                goto Label_0034;
            }
            return data.IsFail(unit);
        Label_0034:
            return 0;
        }

        private bool IsGoodTrickData(Unit unit, int x, int y)
        {
            SRPG.TrickMap.Data data;
            data = this.mTrickMap.GetData(x, y);
            if (data == null)
            {
                goto Label_0037;
            }
            if (data.IsVisual(unit) == null)
            {
                goto Label_0037;
            }
            if (data.IsVaild(unit) == null)
            {
                goto Label_0037;
            }
            return (data.IsFail(unit) == 0);
        Label_0037:
            return 0;
        }

        private bool IsKillAllEnemyOnceBattle()
        {
            int num;
            int num2;
            int num3;
            num = this.GetTotalKillCount();
            num2 = 0;
            num3 = 0;
            goto Label_0062;
        Label_0010:
            if (this.Units[num3].Side != 1)
            {
                goto Label_005A;
            }
            if (this.Units[num3].IsDead == null)
            {
                goto Label_0058;
            }
            if (this.Units[num3].IsUnitFlag(0x100000) == null)
            {
                goto Label_005A;
            }
        Label_0058:
            return 0;
        Label_005A:
            num2 += 1;
            num3 += 1;
        Label_0062:
            if (num3 < this.Units.Count)
            {
                goto Label_0010;
            }
            return (num == num2);
        }

        private bool isKnockBack(SkillData skill)
        {
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return ((skill.KnockBackVal == null) ? 0 : ((skill.KnockBackRate == 0) == 0));
        }

        private bool IsMatchTrickTag(TrickData td, string tag)
        {
            if (string.IsNullOrEmpty(tag) != null)
            {
                goto Label_001E;
            }
            if ((tag == td.Tag) == null)
            {
                goto Label_001E;
            }
            return 1;
        Label_001E:
            return 0;
        }

        private bool IsMissionClearArtifactTypeConditions(QuestBonusObjective bonus, bool check_main_member_only)
        {
            char[] chArray1;
            string[] strArray;
            int num;
            ArtifactParam param;
            chArray1 = new char[] { 0x2c };
            strArray = bonus.TypeParam.Split(chArray1);
            if (((int) strArray.Length) > 0)
            {
                goto Label_0022;
            }
            return 0;
        Label_0022:
            num = 0;
            goto Label_00E4;
        Label_0029:
            if (this.Units[num].Side == null)
            {
                goto Label_0044;
            }
            goto Label_00E0;
        Label_0044:
            if (this.Units[num].IsPartyMember != null)
            {
                goto Label_005F;
            }
            goto Label_00E0;
        Label_005F:
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetArtifactParam(this.Units[num].Job.Param.artifact);
            if (param != null)
            {
                goto Label_0092;
            }
            return 0;
        Label_0092:
            if (Enumerable.Contains<string>(strArray, param.tag) != null)
            {
                goto Label_00A5;
            }
            return 0;
        Label_00A5:
            if (check_main_member_only == null)
            {
                goto Label_00E0;
            }
            if (this.Units[num] == this.Friend)
            {
                goto Label_00DE;
            }
            if (this.mStartingMembers.Contains(this.Units[num]) != null)
            {
                goto Label_00E0;
            }
        Label_00DE:
            return 0;
        Label_00E0:
            num += 1;
        Label_00E4:
            if (num < this.Units.Count)
            {
                goto Label_0029;
            }
            return 1;
        }

        private bool IsMissionClearOnlyHeroConditions(bool check_main_member_only)
        {
            int num;
            num = 0;
            goto Label_0099;
        Label_0007:
            if (this.Units[num].Side == null)
            {
                goto Label_0022;
            }
            goto Label_0095;
        Label_0022:
            if (this.Units[num].IsPartyMember != null)
            {
                goto Label_003D;
            }
            goto Label_0095;
        Label_003D:
            if (this.Units[num].UnitParam.IsHero() != null)
            {
                goto Label_005A;
            }
            return 0;
        Label_005A:
            if (check_main_member_only == null)
            {
                goto Label_0095;
            }
            if (this.Units[num] == this.Friend)
            {
                goto Label_0093;
            }
            if (this.mStartingMembers.Contains(this.Units[num]) != null)
            {
                goto Label_0095;
            }
        Label_0093:
            return 0;
        Label_0095:
            num += 1;
        Label_0099:
            if (num < this.Units.Count)
            {
                goto Label_0007;
            }
            return 1;
        }

        private bool IsMissionClearOnlyTargetUnitsConditions(QuestBonusObjective bonus, bool check_main_member_only)
        {
            char[] chArray1;
            string[] strArray;
            int num;
            chArray1 = new char[] { 0x2c };
            strArray = bonus.TypeParam.Split(chArray1);
            num = 0;
            goto Label_00B6;
        Label_001E:
            if (this.Units[num].Side == null)
            {
                goto Label_0039;
            }
            goto Label_00B2;
        Label_0039:
            if (this.Units[num].IsPartyMember != null)
            {
                goto Label_0054;
            }
            goto Label_00B2;
        Label_0054:
            if (Enumerable.Contains<string>(strArray, this.Units[num].UnitParam.iname) != null)
            {
                goto Label_0077;
            }
            return 0;
        Label_0077:
            if (check_main_member_only == null)
            {
                goto Label_00B2;
            }
            if (this.Units[num] == this.Friend)
            {
                goto Label_00B0;
            }
            if (this.mStartingMembers.Contains(this.Units[num]) != null)
            {
                goto Label_00B2;
            }
        Label_00B0:
            return 0;
        Label_00B2:
            num += 1;
        Label_00B6:
            if (num < this.Units.Count)
            {
                goto Label_001E;
            }
            return 1;
        }

        private bool IsMissionClearPartyMemberJobConditions(QuestBonusObjective bonus, bool check_main_member_only)
        {
            char[] chArray1;
            string[] strArray;
            int num;
            chArray1 = new char[] { 0x2c };
            strArray = bonus.TypeParam.Split(chArray1);
            if (((int) strArray.Length) > 0)
            {
                goto Label_0022;
            }
            return 0;
        Label_0022:
            num = 0;
            goto Label_00C1;
        Label_0029:
            if (this.Units[num].Side == null)
            {
                goto Label_0044;
            }
            goto Label_00BD;
        Label_0044:
            if (this.Units[num].IsPartyMember != null)
            {
                goto Label_005F;
            }
            goto Label_00BD;
        Label_005F:
            if (Enumerable.Contains<string>(strArray, this.Units[num].Job.JobID) != null)
            {
                goto Label_0082;
            }
            return 0;
        Label_0082:
            if (check_main_member_only == null)
            {
                goto Label_00BD;
            }
            if (this.Units[num] == this.Friend)
            {
                goto Label_00BB;
            }
            if (this.mStartingMembers.Contains(this.Units[num]) != null)
            {
                goto Label_00BD;
            }
        Label_00BB:
            return 0;
        Label_00BD:
            num += 1;
        Label_00C1:
            if (num < this.Units.Count)
            {
                goto Label_0029;
            }
            return 1;
        }

        private bool IsMissionClearUnitBirthplaceConditions(QuestBonusObjective bonus, bool check_main_member_only)
        {
            int num;
            num = 0;
            goto Label_00A9;
        Label_0007:
            if (this.Units[num].Side == null)
            {
                goto Label_0022;
            }
            goto Label_00A5;
        Label_0022:
            if (this.Units[num].IsPartyMember != null)
            {
                goto Label_003D;
            }
            goto Label_00A5;
        Label_003D:
            if ((this.Units[num].UnitParam.birth != bonus.TypeParam) == null)
            {
                goto Label_006A;
            }
            return 0;
        Label_006A:
            if (check_main_member_only == null)
            {
                goto Label_00A5;
            }
            if (this.Units[num] == this.Friend)
            {
                goto Label_00A3;
            }
            if (this.mStartingMembers.Contains(this.Units[num]) != null)
            {
                goto Label_00A5;
            }
        Label_00A3:
            return 0;
        Label_00A5:
            num += 1;
        Label_00A9:
            if (num < this.Units.Count)
            {
                goto Label_0007;
            }
            return 1;
        }

        private unsafe bool IsMissionClearUnitSexConditions(QuestBonusObjective bonus, bool check_main_member_only)
        {
            int num;
            int num2;
            int.TryParse(bonus.TypeParam, &num);
            num2 = 0;
            goto Label_00A8;
        Label_0015:
            if (this.Units[num2].Side == null)
            {
                goto Label_0030;
            }
            goto Label_00A4;
        Label_0030:
            if (this.Units[num2].IsPartyMember != null)
            {
                goto Label_004B;
            }
            goto Label_00A4;
        Label_004B:
            if (this.Units[num2].UnitParam.sex == num)
            {
                goto Label_0069;
            }
            return 0;
        Label_0069:
            if (check_main_member_only == null)
            {
                goto Label_00A4;
            }
            if (this.Units[num2] == this.Friend)
            {
                goto Label_00A2;
            }
            if (this.mStartingMembers.Contains(this.Units[num2]) != null)
            {
                goto Label_00A4;
            }
        Label_00A2:
            return 0;
        Label_00A4:
            num2 += 1;
        Label_00A8:
            if (num2 < this.Units.Count)
            {
                goto Label_0015;
            }
            return 1;
        }

        public unsafe bool IsNPCAlive(string[] target_unit_inames)
        {
            Unit unit;
            List<Unit>.Enumerator enumerator;
            bool flag;
            if (((int) target_unit_inames.Length) > 0)
            {
                goto Label_0010;
            }
            return this.IsNPCAllAlive();
        Label_0010:
            enumerator = this.Units.GetEnumerator();
        Label_001C:
            try
            {
                goto Label_007C;
            Label_0021:
                unit = &enumerator.Current;
                if (Enumerable.Contains<string>(target_unit_inames, unit.UnitParam.iname) != null)
                {
                    goto Label_0044;
                }
                goto Label_007C;
            Label_0044:
                if (unit.Side != null)
                {
                    goto Label_007C;
                }
                if (unit.IsPartyMember != null)
                {
                    goto Label_007C;
                }
                if (unit.IsDead == null)
                {
                    goto Label_007C;
                }
                if (unit.IsUnitFlag(0x80000) != null)
                {
                    goto Label_007C;
                }
                flag = 0;
                goto Label_009B;
            Label_007C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0021;
                }
                goto Label_0099;
            }
            finally
            {
            Label_008D:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0099:
            return 1;
        Label_009B:
            return flag;
        }

        public unsafe bool IsNPCAllAlive()
        {
            Unit unit;
            List<Unit>.Enumerator enumerator;
            bool flag;
            enumerator = this.Units.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0051;
            Label_0011:
                unit = &enumerator.Current;
                if (unit.Side != null)
                {
                    goto Label_0051;
                }
                if (unit.IsPartyMember != null)
                {
                    goto Label_0051;
                }
                if (unit.IsDead == null)
                {
                    goto Label_0051;
                }
                if (unit.IsUnitFlag(0x80000) != null)
                {
                    goto Label_0051;
                }
                flag = 0;
                goto Label_0070;
            Label_0051:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_006E;
            }
            finally
            {
            Label_0062:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_006E:
            return 1;
        Label_0070:
            return flag;
        }

        public unsafe bool IsOrdealValidNext()
        {
            int num;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            bool flag;
            num = this.mCurrentTeamId + 1;
            if (num < this.mMaxTeamId)
            {
                goto Label_0017;
            }
            return 0;
        Label_0017:
            enumerator = this.mPlayer.GetEnumerator();
        Label_0023:
            try
            {
                goto Label_0058;
            Label_0028:
                unit = &enumerator.Current;
                if (unit.TeamId == num)
                {
                    goto Label_0041;
                }
                goto Label_0058;
            Label_0041:
                if (unit.IsDead == null)
                {
                    goto Label_0051;
                }
                goto Label_0058;
            Label_0051:
                flag = 1;
                goto Label_0077;
            Label_0058:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0028;
                }
                goto Label_0075;
            }
            finally
            {
            Label_0069:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0075:
            return 0;
        Label_0077:
            return flag;
        }

        public bool IsTargetBreakUnit(Unit self, Unit target, SkillData skill)
        {
            bool flag;
            eMapBreakClashType type;
            ESkillTarget target2;
            ESkillTarget target3;
            if (self == null)
            {
                goto Label_000C;
            }
            if (target != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            if (target.IsBreakObj != null)
            {
                goto Label_001B;
            }
            return 0;
        Label_001B:
            if (skill == null)
            {
                goto Label_0030;
            }
            if (skill.EffectType != 0x12)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            flag = 0;
            switch (target.BreakObjClashType)
            {
                case 0:
                    goto Label_0054;

                case 1:
                    goto Label_005B;

                case 2:
                    goto Label_0089;

                case 3:
                    goto Label_00BA;
            }
            goto Label_00C1;
        Label_0054:
            flag = 1;
            goto Label_00C1;
        Label_005B:
            if (this.IsMultiVersus == null)
            {
                goto Label_007A;
            }
            flag = self.OwnerPlayerIndex == target.OwnerPlayerIndex;
            goto Label_0084;
        Label_007A:
            flag = self.Side == 0;
        Label_0084:
            goto Label_00C1;
        Label_0089:
            if (this.IsMultiVersus == null)
            {
                goto Label_00AB;
            }
            flag = (self.OwnerPlayerIndex == target.OwnerPlayerIndex) == 0;
            goto Label_00B5;
        Label_00AB:
            flag = self.Side == 1;
        Label_00B5:
            goto Label_00C1;
        Label_00BA:
            flag = 0;
        Label_00C1:
            if (flag == null)
            {
                goto Label_01B9;
            }
            if (skill == null)
            {
                goto Label_01B9;
            }
            if (target.BreakObjSideType == null)
            {
                goto Label_01B9;
            }
            switch (skill.Target)
            {
                case 0:
                    goto Label_0102;

                case 1:
                    goto Label_010C;

                case 2:
                    goto Label_011D;

                case 3:
                    goto Label_012B;

                case 4:
                    goto Label_0132;

                case 5:
                    goto Label_013F;
            }
            goto Label_01B9;
        Label_0102:
            flag = self == target;
            goto Label_01B9;
        Label_010C:
            flag = this.CheckGimmickEnemySide(self, target) == 0;
            goto Label_01B9;
        Label_011D:
            flag = this.CheckGimmickEnemySide(self, target);
            goto Label_01B9;
        Label_012B:
            flag = 1;
            goto Label_01B9;
        Label_0132:
            flag = (self == target) == 0;
            goto Label_01B9;
        Label_013F:
            if (skill.TeleportType == null)
            {
                goto Label_01B2;
            }
            switch (skill.TeleportTarget)
            {
                case 0:
                    goto Label_0170;

                case 1:
                    goto Label_017A;

                case 2:
                    goto Label_018B;

                case 3:
                    goto Label_0199;

                case 4:
                    goto Label_01A0;
            }
            goto Label_01AD;
        Label_0170:
            flag = self == target;
            goto Label_01AD;
        Label_017A:
            flag = this.CheckGimmickEnemySide(self, target) == 0;
            goto Label_01AD;
        Label_018B:
            flag = this.CheckGimmickEnemySide(self, target);
            goto Label_01AD;
        Label_0199:
            flag = 1;
            goto Label_01AD;
        Label_01A0:
            flag = (self == target) == 0;
        Label_01AD:
            goto Label_01B4;
        Label_01B2:
            flag = 0;
        Label_01B4:;
        Label_01B9:
            return flag;
        }

        private bool IsTargetBreakUnitAI(Unit self, Unit target)
        {
            EUnitSide side;
            bool flag;
            side = self.Side;
            if (self.IsUnitCondition(0x10L) != null)
            {
                goto Label_0026;
            }
            if (self.IsUnitCondition(0x400L) == null)
            {
                goto Label_0046;
            }
        Label_0026:
            if (self.Side != null)
            {
                goto Label_0038;
            }
            side = 1;
            goto Label_0046;
        Label_0038:
            if (self.Side != 1)
            {
                goto Label_0046;
            }
            side = 0;
        Label_0046:
            flag = 1;
            if (target.BreakObjAIType != 2)
            {
                goto Label_0061;
            }
            if (side == null)
            {
                goto Label_0088;
            }
            flag = 0;
            goto Label_0088;
        Label_0061:
            if (target.BreakObjAIType != 3)
            {
                goto Label_007B;
            }
            if (side == 1)
            {
                goto Label_0088;
            }
            flag = 0;
            goto Label_0088;
        Label_007B:
            if (target.BreakObjAIType != null)
            {
                goto Label_0088;
            }
            flag = 0;
        Label_0088:
            return flag;
        }

        private unsafe bool IsTeamPartySizeMax(int max_num, bool is_inc_mercenary)
        {
            bool flag;
            int num;
            int num2;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            flag = 1;
            num = 0;
            goto Label_00A0;
        Label_0009:
            num2 = 0;
            enumerator = this.Units.GetEnumerator();
        Label_0018:
            try
            {
                goto Label_0070;
            Label_001D:
                unit = &enumerator.Current;
                if (unit.Side != null)
                {
                    goto Label_0070;
                }
                if (unit.IsPartyMember != null)
                {
                    goto Label_0040;
                }
                goto Label_0070;
            Label_0040:
                if (unit.TeamId == num)
                {
                    goto Label_0051;
                }
                goto Label_0070;
            Label_0051:
                if (unit.IsUnitFlag(0x2000000) == null)
                {
                    goto Label_006C;
                }
                if (is_inc_mercenary != null)
                {
                    goto Label_006C;
                }
                goto Label_0070;
            Label_006C:
                num2 += 1;
            Label_0070:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001D;
                }
                goto Label_008E;
            }
            finally
            {
            Label_0081:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_008E:
            if (num2 <= max_num)
            {
                goto Label_009C;
            }
            flag = 0;
            goto Label_00AC;
        Label_009C:
            num += 1;
        Label_00A0:
            if (num < this.mMaxTeamId)
            {
                goto Label_0009;
            }
        Label_00AC:
            return flag;
        }

        private bool IsTrickExistUnit(int gx, int gy)
        {
            Unit unit;
            unit = this.FindUnitAtGrid(gx, gy);
            if (unit != null)
            {
                goto Label_002C;
            }
            unit = this.FindGimmickAtGrid(gx, gy, 0);
            if (unit == null)
            {
                goto Label_002C;
            }
            if (unit.IsBreakObj != null)
            {
                goto Label_002C;
            }
            unit = null;
        Label_002C:
            return ((unit == null) == 0);
        }

        public bool IsUnitAuto(Unit unit)
        {
            if (unit.IsControl != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            return this.IsAutoBattle;
        }

        public bool IsUseSkillCollabo(Unit unit, SkillData skill)
        {
            if (unit != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return unit.IsUseSkillCollabo(skill, 0);
        }

        private int judgeSortOrder(OrderData src, OrderData dst)
        {
            int num;
            int num2;
            bool flag;
            bool flag2;
            if ((src.CheckChargeTimeFullOver() == null) || (dst.CheckChargeTimeFullOver() == null))
            {
                goto Label_0107;
            }
            num = (src.GetChargeTime() * 0x3e8) / src.GetChargeTimeMax();
            num2 = (dst.GetChargeTime() * 0x3e8) / dst.GetChargeTimeMax();
            if (num2 == num)
            {
                goto Label_005D;
            }
            return (num2 - num);
        Label_005D:
            if ((src.IsCastSkill == null) || (dst.IsCastSkill == null))
            {
                goto Label_00C1;
            }
            if (src.Unit.CastIndex >= dst.Unit.CastIndex)
            {
                goto Label_009A;
            }
            return -1;
        Label_009A:
            if (src.Unit.CastIndex <= dst.Unit.CastIndex)
            {
                goto Label_00C1;
            }
            return 1;
        Label_00C1:
            if (src.IsCastSkill == dst.IsCastSkill)
            {
                goto Label_00E5;
            }
            return ((src.IsCastSkill == null) ? 1 : -1);
        Label_00E5:
            return (src.Unit.UnitIndex - dst.Unit.UnitIndex);
        Label_0107:
            if (src.CheckChargeTimeFullOver() == null)
            {
                goto Label_0114;
            }
            return -1;
        Label_0114:
            if (dst.CheckChargeTimeFullOver() == null)
            {
                goto Label_0121;
            }
            return 1;
        Label_0121:
            flag = src.UpdateChargeTime();
            flag2 = dst.UpdateChargeTime();
            if (flag != null)
            {
                goto Label_015D;
            }
            if (flag2 != null)
            {
                goto Label_015D;
            }
            return (src.Unit.UnitIndex - dst.Unit.UnitIndex);
        Label_015D:
            return this.judgeSortOrder(src, dst);
        }

        private void judgeStartTypeArenaCalc()
        {
            OrderData data;
            if (this.CurrentOrderData.IsCastSkill == null)
            {
                goto Label_001E;
            }
            this.mArenaCalcTypeNext = 5;
            goto Label_0025;
        Label_001E:
            this.mArenaCalcTypeNext = 2;
        Label_0025:
            return;
        }

        public bool LoadSuspendData()
        {
            if (BattleSuspend.LoadData() != null)
            {
                goto Label_000C;
            }
            return 0;
        Label_000C:
            this.Logs.Reset();
            WeatherData.IsExecuteUpdate = 0;
            WeatherData.IsEntryConditionLog = 0;
            this.NextOrder(1, 1, 0, 1);
            WeatherData.IsExecuteUpdate = 1;
            WeatherData.IsEntryConditionLog = 1;
            return 1;
        }

        public LogType Log<LogType>() where LogType: BattleLog, new()
        {
            if (this.mIsArenaCalc != null)
            {
                goto Label_001F;
            }
            if (MonoSingleton<GameManager>.Instance.AudienceManager.IsSkipEnd != null)
            {
                goto Label_0025;
            }
        Label_001F:
            return Activator.CreateInstance<LogType>();
        Label_0025:
            return this.mLogs.Log<LogType>();
        }

        public void MapCommandEnd(Unit self)
        {
            self.SetUnitFlag(2, 1);
            self.SetUnitFlag(4, 1);
            return;
        }

        private void MapEnd()
        {
            QuestResult result;
            QuestResult result2;
            if (this.IsOrdeal == null)
            {
                goto Label_0040;
            }
            if (this.GetQuestResult() == 2)
            {
                goto Label_0020;
            }
            goto Label_0040;
        Label_0020:
            if (this.IsOrdealValidNext() == null)
            {
                goto Label_0040;
            }
            this.SetBattleFlag(2, 0);
            this.Log<LogOrdealChangeNext>();
            return;
        Label_0040:
            this.Log<LogMapEnd>();
            this.SetBattleFlag(2, 0);
            this.SetBattleFlag(1, 0);
            return;
        }

        public unsafe void MapStart()
        {
            object[] objArray1;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            List<TrickSetting> list;
            TrickSetting setting;
            List<TrickSetting>.Enumerator enumerator;
            RandXorshift xorshift;
            RandXorshift xorshift2;
            this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
            this.DebugAssert(this.IsBattleFlag(1) == 0, "マップ未開始のみコール可");
            this.CurrentRand = this.mRand;
            this.mUnits.Clear();
            if (this.IsMultiVersus == null)
            {
                goto Label_0080;
            }
            num = 0;
            goto Label_006A;
        Label_004F:
            this.mUnits.Add(this.mAllUnits[num]);
            num += 1;
        Label_006A:
            if (num < this.mAllUnits.Count)
            {
                goto Label_004F;
            }
            goto Label_00F4;
        Label_0080:
            num2 = 0;
            goto Label_00A2;
        Label_0087:
            this.mUnits.Add(this.mPlayer[num2]);
            num2 += 1;
        Label_00A2:
            if (num2 < this.mPlayer.Count)
            {
                goto Label_0087;
            }
            num3 = 0;
            goto Label_00DC;
        Label_00BA:
            this.mUnits.Add(this.mEnemys[this.MapIndex][num3]);
            num3 += 1;
        Label_00DC:
            if (num3 < this.mEnemys[this.MapIndex].Count)
            {
                goto Label_00BA;
            }
        Label_00F4:
            this.mTreasures.Clear();
            num4 = 0;
            goto Label_0179;
        Label_0106:
            if (this.mUnits[num4].UnitType == 1)
            {
                goto Label_0122;
            }
            goto Label_0175;
        Label_0122:
            if (this.mUnits[num4].EventTrigger != null)
            {
                goto Label_013D;
            }
            goto Label_0175;
        Label_013D:
            if (this.mUnits[num4].EventTrigger.EventType == 3)
            {
                goto Label_015E;
            }
            goto Label_0175;
        Label_015E:
            this.mTreasures.Add(this.mUnits[num4]);
        Label_0175:
            num4 += 1;
        Label_0179:
            if (num4 < this.mUnits.Count)
            {
                goto Label_0106;
            }
            this.mGems.Clear();
            num5 = 0;
            goto Label_0216;
        Label_019D:
            if (this.mUnits[num5].UnitType == 2)
            {
                goto Label_01BA;
            }
            goto Label_0210;
        Label_01BA:
            if (this.mUnits[num5].EventTrigger != null)
            {
                goto Label_01D6;
            }
            goto Label_0210;
        Label_01D6:
            if (this.mUnits[num5].EventTrigger.EventType == 4)
            {
                goto Label_01F8;
            }
            goto Label_0210;
        Label_01F8:
            this.mGems.Add(this.mUnits[num5]);
        Label_0210:
            num5 += 1;
        Label_0216:
            if (num5 < this.mUnits.Count)
            {
                goto Label_019D;
            }
            num6 = 0;
            goto Label_027B;
        Label_0230:
            this.mUnits[num6].NotifyMapStart();
            this.mUnits[num6].TowerStartHP = this.mUnits[num6].MaximumStatus.param.hp;
            num6 += 1;
        Label_027B:
            if (num6 < this.mUnits.Count)
            {
                goto Label_0230;
            }
            this.SetBattleFlag(1, 1);
            this.SetBattleFlag(2, 0);
            this.mGridLines = new List<Grid>(this.CurrentMap.Width * this.CurrentMap.Height);
            this.mGridLines.Clear();
            this.InitWeather();
            WeatherData.IsEntryConditionLog = 0;
            this.UpdateWeather();
            TrickData.ClearEffect();
            enumerator = this.CurrentMap.TrickSettings.GetEnumerator();
        Label_02F8:
            try
            {
                goto Label_0336;
            Label_02FD:
                setting = &enumerator.Current;
                TrickData.EntryEffect(setting.mId, setting.mGx, setting.mGy, setting.mTag, null, 0, 1, 1);
            Label_0336:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_02FD;
                }
                goto Label_0354;
            }
            finally
            {
            Label_0347:
                ((List<TrickSetting>.Enumerator) enumerator).Dispose();
            }
        Label_0354:
            this.CreateGimmickEvents();
            this.UseAutoSkills();
            this.NextOrder(1, 1, 0, 1);
            WeatherData.IsEntryConditionLog = 1;
            xorshift = this.CloneRand();
            xorshift2 = this.CloneRandDamage();
            objArray1 = new object[] { "rnd:", (uint) xorshift.Get(), "rndDmg:", (uint) xorshift2.Get() };
            DebugUtility.Log(string.Concat(objArray1));
            return;
        }

        public bool Move(Unit self, Grid goal, bool forceAI)
        {
            return this.Move(self, goal, 4, 0, forceAI);
        }

        public bool Move(Unit self, Grid goal, EUnitDirection direction, bool isStickControl, bool forceAI)
        {
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            BattleMap map;
            bool flag;
            int num;
            LogMapMove move;
            int num2;
            Grid grid;
            LogMapMove move2;
            int num3;
            int num4;
            Grid grid2;
            bool flag2;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            LogMapMove move3;
            this.DebugAssert(this.IsMapCommand, "マップコマンド中のみコール可");
            this.DebugAssert((self == null) == 0, "self == null");
            if (goal != null)
            {
                goto Label_002B;
            }
            return 0;
        Label_002B:
            map = this.CurrentMap;
            this.DebugAssert((map == null) == 0, "map == null");
            flag = (forceAI != null) ? 1 : this.IsUnitAuto(self);
            if (((flag != null) || (self.IsUnitFlag(0x400) != null)) || (this.CheckMove(self, goal) != null))
            {
                goto Label_007B;
            }
            return 0;
        Label_007B:
            if (isStickControl == null)
            {
                goto Label_0149;
            }
            num = (self.x >= goal.x) ? (self.x - goal.x) : (goal.x - self.x);
            num += (self.y >= goal.y) ? (self.y - goal.y) : (goal.y - self.y);
            self.x = goal.x;
            self.y = goal.y;
            self.Direction = direction;
            move = this.Log<LogMapMove>();
            move.self = self;
            move.ex = self.x;
            move.ey = self.y;
            move.dir = self.Direction;
            move.auto = flag;
            this.MoveEnd(self, num, 0);
            return 1;
        Label_0149:
            map.ResetMoveRoutes();
            map.CalcMoveSteps(self, goal, 0);
            map.CalcMoveRoutes(self);
            num2 = map.GetMoveRoutesCount();
            if (map.GetNextMoveRoutes() != null)
            {
                goto Label_01B1;
            }
            if (direction == 4)
            {
                goto Label_018B;
            }
            self.Direction = direction;
            goto Label_01B1;
        Label_018B:
            self.Direction = UnitDirectionFromVector(self, goal.x - self.x, goal.y - self.y);
        Label_01B1:
            move2 = this.Log<LogMapMove>();
            move2.self = self;
            move2.ex = self.x;
            move2.ey = self.y;
            move2.dir = self.Direction;
            move2.auto = flag;
            objArray1 = new object[] { "[", self.UnitName, "] x:", (int) self.x, ", y:", (int) self.y, ", h:", (int) map[self.x, self.y].height, " から移動開始" };
            this.DebugLog(string.Concat(objArray1));
        Label_026E:
            num3 = self.x;
            num4 = self.y;
            grid2 = map.GetNextMoveRoutes();
            if (grid2 != null)
            {
                goto Label_029C;
            }
            this.MoveEnd(self, num2, 0);
            goto Label_0495;
        Label_029C:
            flag2 = map.IsLastMoveGrid(grid2);
            self.x = grid2.x;
            self.y = grid2.y;
            objArray2 = new object[] { "[", self.UnitName, "] x:", (int) self.x, ", y:", (int) self.y, ", h:", (int) grid2.height, " へ移動" };
            this.DebugLog(string.Concat(objArray2));
            if (flag2 == null)
            {
                goto Label_0348;
            }
            if (direction == 4)
            {
                goto Label_0348;
            }
            self.Direction = direction;
            goto Label_0449;
        Label_0348:
            num5 = grid2.x - num3;
            num6 = grid2.y - num4;
            if (flag2 == null)
            {
                goto Label_0439;
            }
            if (flag == null)
            {
                goto Label_0439;
            }
            if (self.IsUnitFlag(0x80) == null)
            {
                goto Label_0395;
            }
            num5 = num3 - grid2.x;
            num6 = num4 - grid2.y;
        Label_0395:
            num7 = this.mSafeMap.get(num3, num4);
            num8 = 0;
            goto Label_0431;
        Label_03AE:
            num9 = Unit.DIRECTION_OFFSETS[num8, 0];
            num10 = Unit.DIRECTION_OFFSETS[num8, 1];
            if (this.mSafeMap.isValid(grid2.x + num9, grid2.y + num10) != null)
            {
                goto Label_03F5;
            }
            goto Label_042B;
        Label_03F5:
            num11 = this.mSafeMap.get(grid2.x + num9, grid2.y + num10);
            if (num11 >= num7)
            {
                goto Label_042B;
            }
            num7 = num11;
            num5 = num9;
            num6 = num10;
        Label_042B:
            num8 += 1;
        Label_0431:
            if (num8 < 4)
            {
                goto Label_03AE;
            }
        Label_0439:
            self.Direction = UnitDirectionFromVector(self, num5, num6);
        Label_0449:
            move3 = this.Log<LogMapMove>();
            move3.self = self;
            move3.ex = grid2.x;
            move3.ey = grid2.y;
            move3.dir = self.Direction;
            move3.auto = flag;
            map.IncrementMoveStep();
            goto Label_026E;
        Label_0495:
            objArray3 = new object[] { "[", self.UnitName, "] x:", (int) self.x, ", y:", (int) self.y, ", h:", (int) map[self.x, self.y].height, " で停止" };
            this.DebugLog(string.Concat(objArray3));
            self.SetUnitFlag(0x80, 0);
            self.SetUnitFlag(2, 1);
            self.SetCommandFlag(1, 1);
            return 1;
        }

        private void MoveEnd(Unit self, int step, bool isMultiPlayer)
        {
            int num;
            int num2;
            self.SetUnitFlag(0x80, 0);
            self.SetUnitFlag(2, 1);
            self.SetCommandFlag(1, 1);
            this.EndMoveSkill(self, step);
            num = 0;
            goto Label_005C;
        Label_002B:
            if ((this.Logs[num] as LogMapEnd) != null)
            {
                goto Label_0057;
            }
            if ((this.Logs[num] as LogUnitEnd) == null)
            {
                goto Label_0058;
            }
        Label_0057:
            return;
        Label_0058:
            num += 1;
        Label_005C:
            if (num < this.Logs.Num)
            {
                goto Label_002B;
            }
            num2 = Math.Max(this.Logs.Num - 1, 0);
            if (self.CastSkill == null)
            {
                goto Label_00A2;
            }
            if (self.CastSkill.LineType == null)
            {
                goto Label_00A2;
            }
            self.CancelCastSkill();
        Label_00A2:
            if ((this.Logs[num2] as LogMapMove) != null)
            {
                goto Label_00DB;
            }
            if (self.IsDead == null)
            {
                goto Label_00CE;
            }
            this.InternalLogUnitEnd();
            goto Label_00DB;
        Label_00CE:
            if (isMultiPlayer != null)
            {
                goto Label_00DB;
            }
            this.Log<LogMapCommand>();
        Label_00DB:
            self.RefleshMomentBuff(this.Units, 0, -1, -1);
            return;
        }

        public bool MoveMultiPlayer(Unit self, int x, int y, EUnitDirection direction)
        {
            object[] objArray1;
            int num;
            <MoveMultiPlayer>c__AnonStorey1B8 storeyb;
            storeyb = new <MoveMultiPlayer>c__AnonStorey1B8();
            storeyb.self = self;
            if (this.CheckMove(storeyb.self, this.CurrentMap[x, y]) != null)
            {
                goto Label_002D;
            }
            return 0;
        Label_002D:
            num = (storeyb.self.x >= x) ? (storeyb.self.x - x) : (x - storeyb.self.x);
            num += (storeyb.self.y >= y) ? (storeyb.self.y - y) : (y - storeyb.self.y);
            storeyb.self.x = x;
            storeyb.self.y = y;
            storeyb.self.Direction = direction;
            objArray1 = new object[] { "[PUN]MoveMultiPlayer unitID:", (int) this.mPlayer.FindIndex(new Predicate<Unit>(storeyb.<>m__5F)), " x:", (int) x, " y:", (int) y, " d:", (EUnitDirection) direction };
            this.DebugLog(string.Concat(objArray1));
            this.MoveEnd(storeyb.self, num, 1);
            return 1;
        }

        private void NextClockTime()
        {
            int num;
            Unit unit;
            OrderData data;
            int num2;
            OrderData data2;
            this.mClockTime = OInt.op_Increment(this.mClockTime);
            this.mClockTimeTotal = OInt.op_Increment(this.mClockTimeTotal);
            if (TrickData.CheckClock(this.mClockTimeTotal) == null)
            {
                goto Label_003C;
            }
            TrickData.UpdateMarker();
        Label_003C:
            num = 0;
            goto Label_0090;
        Label_0043:
            unit = this.mUnits[num];
            unit.UpdateClockTime();
            if (unit.CheckEnableEntry() == null)
            {
                goto Label_008C;
            }
            this.EntryUnit(unit);
            if (unit.IsBreakObj != null)
            {
                goto Label_008C;
            }
            data = new OrderData();
            data.Unit = unit;
            this.mOrder.Add(data);
        Label_008C:
            num += 1;
        Label_0090:
            if (num < this.mUnits.Count)
            {
                goto Label_0043;
            }
            num2 = 0;
            goto Label_00C2;
        Label_00A8:
            data2 = this.mOrder[num2];
            data2.UpdateChargeTime();
            num2 += 1;
        Label_00C2:
            if (num2 < this.mOrder.Count)
            {
                goto Label_00A8;
            }
            return;
        }

        private void NextOrder(bool is_skip_update_et, bool sync, bool forceSync, bool judge)
        {
            bool flag;
            int num;
            OrderData data;
            GameManager manager;
            flag = (this.CurrentUnit == null) ? 0 : (this.CurrentUnit.OwnerPlayerIndex > 0);
            if (is_skip_update_et != null)
            {
                goto Label_009C;
            }
            num = 0;
            goto Label_006B;
        Label_002D:
            this.UpdateEntryTriggers(2, this.mUnits[num], null);
            this.UpdateEntryTriggers(5, this.mUnits[num], null);
            this.CheckWithDrawUnit(this.mUnits[num]);
            num += 1;
        Label_006B:
            if (num < this.mUnits.Count)
            {
                goto Label_002D;
            }
            this.UpdateEntryTriggers(1, null, null);
            this.UpdateCancelCastSkill();
            this.UpdateGimmickEventStart();
            this.UpdateGimmickEventTrick();
            TrickData.UpdateMarker();
        Label_009C:
            if (judge == null)
            {
                goto Label_00BB;
            }
            if (this.CheckJudgeBattle() == null)
            {
                goto Label_00BB;
            }
            this.CalcQuestRecord();
            this.MapEnd();
            return;
        Label_00BB:
            this.CalcOrder();
            this.UpdateWeather();
            if (this.UseAutoSkills() == null)
            {
                goto Label_00D9;
            }
            this.CalcOrder();
        Label_00D9:
            this.CheckBreakObjKill();
            if (this.CheckJudgeBattle() == null)
            {
                goto Label_00F7;
            }
            this.CalcQuestRecord();
            this.MapEnd();
            return;
        Label_00F7:
            data = this.CurrentOrderData;
            manager = MonoSingleton<GameManager>.Instance;
            if (this.IsMultiPlay == null)
            {
                goto Label_013E;
            }
            if (manager.AudienceMode != null)
            {
                goto Label_013E;
            }
            if (manager.IsVSCpuBattle != null)
            {
                goto Label_013E;
            }
            if (sync == null)
            {
                goto Label_0131;
            }
            if (flag != null)
            {
                goto Label_0137;
            }
        Label_0131:
            if (forceSync == null)
            {
                goto Label_013E;
            }
        Label_0137:
            this.Log<LogSync>();
        Label_013E:
            if (data.IsCastSkill == null)
            {
                goto Label_0155;
            }
            this.Log<LogCastSkillStart>();
            goto Label_015C;
        Label_0155:
            this.Log<LogUnitStart>();
        Label_015C:
            this.mKillstreak = 0;
            this.mTargetKillstreakDict.Clear();
            return;
        }

        private unsafe void NotifyDamagedActionStart(Unit attacker, Unit defender)
        {
            int num;
            int num2;
            int num3;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            Unit unit2;
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (this.CheckEnemySide(attacker, defender) == null)
            {
                goto Label_01F8;
            }
            if (defender.IsUnitFlag(0x10000) == null)
            {
                goto Label_003E;
            }
            defender.SetUnitFlag(0x10000, 0);
            defender.SetUnitFlag(8, 1);
        Label_003E:
            if (defender.NotifyUniqueNames == null)
            {
                goto Label_01F8;
            }
            num = 0;
            goto Label_01E7;
        Label_0050:
            if ((defender.NotifyUniqueNames[num] == "pall") == null)
            {
                goto Label_00BB;
            }
            num2 = 0;
            goto Label_00A5;
        Label_0077:
            this.mPlayer[num2].SetUnitFlag(0x10000, 0);
            this.mPlayer[num2].SetUnitFlag(8, 1);
            num2 += 1;
        Label_00A5:
            if (num2 < this.mPlayer.Count)
            {
                goto Label_0077;
            }
            goto Label_01E3;
        Label_00BB:
            if ((defender.NotifyUniqueNames[num] == "eall") == null)
            {
                goto Label_0126;
            }
            num3 = 0;
            goto Label_0110;
        Label_00E2:
            this.Enemys[num3].SetUnitFlag(0x10000, 0);
            this.Enemys[num3].SetUnitFlag(8, 1);
            num3 += 1;
        Label_0110:
            if (num3 < this.Enemys.Count)
            {
                goto Label_00E2;
            }
            goto Label_01E3;
        Label_0126:
            if ((defender.NotifyUniqueNames[num] == "nall") == null)
            {
                goto Label_01A8;
            }
            enumerator = this.mUnits.GetEnumerator();
        Label_0153:
            try
            {
                goto Label_0185;
            Label_0158:
                unit = &enumerator.Current;
                if (unit.Side == 2)
                {
                    goto Label_0171;
                }
                goto Label_0185;
            Label_0171:
                unit.SetUnitFlag(0x10000, 0);
                unit.SetUnitFlag(8, 1);
            Label_0185:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0158;
                }
                goto Label_01A3;
            }
            finally
            {
            Label_0196:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_01A3:
            goto Label_01E3;
        Label_01A8:
            unit2 = this.FindUnitByUniqueName(defender.NotifyUniqueNames[num]);
            if (unit2 != null)
            {
                goto Label_01CD;
            }
            goto Label_01E3;
        Label_01CD:
            unit2.SetUnitFlag(0x10000, 0);
            unit2.SetUnitFlag(8, 1);
        Label_01E3:
            num += 1;
        Label_01E7:
            if (num < defender.NotifyUniqueNames.Count)
            {
                goto Label_0050;
            }
        Label_01F8:
            return;
        }

        public void NotifyMapCommand()
        {
            Unit unit;
            unit = this.CurrentUnit;
            unit.NotifyCommandStart();
            if (unit.IsUnitFlag(2) != null)
            {
                goto Label_0020;
            }
            this.UpdateMoveMap(unit);
        Label_0020:
            return;
        }

        public unsafe void OrdealChangeNext()
        {
            Unit unit;
            List<Unit>.Enumerator enumerator;
            Unit unit2;
            List<Unit>.Enumerator enumerator2;
            List<Unit> list;
            int num;
            int num2;
            Unit unit3;
            Grid grid;
            Unit unit4;
            List<Unit>.Enumerator enumerator3;
            int num3;
            Unit unit5;
            int num4;
            Unit unit6;
            if (this.IsOrdealValidNext() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.CurrentMap != null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            enumerator = this.Units.GetEnumerator();
        Label_0024:
            try
            {
                goto Label_003D;
            Label_0029:
                unit = &enumerator.Current;
                unit.ChargeTime = 0;
            Label_003D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0029;
                }
                goto Label_005A;
            }
            finally
            {
            Label_004E:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_005A:
            enumerator2 = this.mPlayer.GetEnumerator();
        Label_0066:
            try
            {
                goto Label_00AA;
            Label_006B:
                unit2 = &enumerator2.Current;
                if (unit2.IsUnitFlag(0x1000000) == null)
                {
                    goto Label_0088;
                }
                goto Label_00AA;
            Label_0088:
                if (unit2.IsDead == null)
                {
                    goto Label_0098;
                }
                goto Label_00AA;
            Label_0098:
                unit2.SetUnitFlag(0x2000, 1);
                unit2.ForceDead();
            Label_00AA:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_006B;
                }
                goto Label_00C7;
            }
            finally
            {
            Label_00BB:
                ((List<Unit>.Enumerator) enumerator2).Dispose();
            }
        Label_00C7:
            this.mCurrentTeamId += 1;
            this.mFriendIndex = -1;
            list = new List<Unit>();
            num = 0;
            num2 = 0;
            goto Label_01E1;
        Label_00F3:
            unit3 = this.mPlayer[num2];
            unit3.SetUnitFlag(0x1000000, (unit3.TeamId == this.mCurrentTeamId) == 0);
            if (unit3.IsUnitFlag(0x1000000) == null)
            {
                goto Label_0136;
            }
            goto Label_01DB;
        Label_0136:
            if (num != null)
            {
                goto Label_014A;
            }
            this.mLeaderIndex = num2;
        Label_014A:
            if (unit3.IsUnitFlag(0x2000000) == null)
            {
                goto Label_0175;
            }
            this.mFriendIndex = num2;
            this.mFriendStates = unit3.FriendStates;
        Label_0175:
            grid = this.GetCorrectDuplicatePosition(unit3);
            unit3.x = grid.x;
            unit3.y = grid.y;
            unit3.SetUnitFlag(1, 1);
            unit3.ChargeTime = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.OrdealCT;
            list.Add(unit3);
            num += 1;
            this.Log<LogUnitEntry>().self = unit3;
        Label_01DB:
            num2 += 1;
        Label_01E1:
            if (num2 < this.mPlayer.Count)
            {
                goto Label_00F3;
            }
            if (this.Leader == null)
            {
                goto Label_0217;
            }
            this.InternalBattlePassiveSkill(this.Leader, this.Leader.LeaderSkill, 1, null);
        Label_0217:
            if (this.Friend == null)
            {
                goto Label_0247;
            }
            if (this.mFriendStates != 1)
            {
                goto Label_0247;
            }
            this.InternalBattlePassiveSkill(this.Friend, this.Friend.LeaderSkill, 1, null);
        Label_0247:
            enumerator3 = list.GetEnumerator();
        Label_0250:
            try
            {
                goto Label_026F;
            Label_0255:
                unit4 = &enumerator3.Current;
                unit4.CalcCurrentStatus(1, 0);
                this.BeginBattlePassiveSkill(unit4);
            Label_026F:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0255;
                }
                goto Label_028D;
            }
            finally
            {
            Label_0280:
                ((List<Unit>.Enumerator) enumerator3).Dispose();
            }
        Label_028D:
            num3 = 0;
            goto Label_02F5;
        Label_0295:
            unit5 = list[num3];
            unit5.UpdateBuffEffects();
            unit5.CalcCurrentStatus(0, 0);
            unit5.CurrentStatus.param.hp = unit5.MaximumStatus.param.hp;
            unit5.CurrentStatus.param.mp = unit5.GetStartGems();
            num3 += 1;
        Label_02F5:
            if (num3 < list.Count)
            {
                goto Label_0295;
            }
            num4 = 0;
            goto Label_0325;
        Label_030B:
            unit6 = list[num4];
            this.UseAutoSkills(unit6);
            num4 += 1;
        Label_0325:
            if (num4 < list.Count)
            {
                goto Label_030B;
            }
            this.mRecord.result = 0;
            this.NextOrder(1, 1, 0, 1);
            return;
        }

        public unsafe void OrdealRestore(int team_id)
        {
            List<Unit> list;
            int num;
            int num2;
            Unit unit;
            Unit unit2;
            List<Unit>.Enumerator enumerator;
            if (this.IsOrdeal != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (team_id != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            this.mFriendIndex = -1;
            list = new List<Unit>();
            num = 0;
            num2 = 0;
            goto Label_00B0;
        Label_002E:
            unit = this.mPlayer[num2];
            unit.SetUnitFlag(0x1000000, (unit.TeamId == team_id) == 0);
            if (unit.IsUnitFlag(0x1000000) == null)
            {
                goto Label_0067;
            }
            goto Label_00AC;
        Label_0067:
            if (num != null)
            {
                goto Label_0079;
            }
            this.mLeaderIndex = num2;
        Label_0079:
            if (unit.IsUnitFlag(0x2000000) == null)
            {
                goto Label_00A1;
            }
            this.mFriendIndex = num2;
            this.mFriendStates = unit.FriendStates;
        Label_00A1:
            list.Add(unit);
            num += 1;
        Label_00AC:
            num2 += 1;
        Label_00B0:
            if (num2 < this.mPlayer.Count)
            {
                goto Label_002E;
            }
            if (this.Leader == null)
            {
                goto Label_00E5;
            }
            this.InternalBattlePassiveSkill(this.Leader, this.Leader.LeaderSkill, 1, null);
        Label_00E5:
            if (this.Friend == null)
            {
                goto Label_0115;
            }
            if (this.mFriendStates != 1)
            {
                goto Label_0115;
            }
            this.InternalBattlePassiveSkill(this.Friend, this.Friend.LeaderSkill, 1, null);
        Label_0115:
            enumerator = list.GetEnumerator();
        Label_011D:
            try
            {
                goto Label_0133;
            Label_0122:
                unit2 = &enumerator.Current;
                this.BeginBattlePassiveSkill(unit2);
            Label_0133:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0122;
                }
                goto Label_0151;
            }
            finally
            {
            Label_0144:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0151:
            return;
        }

        public static int Pow(int val, int n)
        {
            int num;
            int num2;
            num = 1;
            num2 = 1;
            goto Label_0011;
        Label_0009:
            num *= val;
            num2 += 1;
        Label_0011:
            if (num2 <= n)
            {
                goto Label_0009;
            }
            return num;
        }

        private unsafe void procKnockBack(Unit self, SkillData skill, int gx, int gy, List<LogSkill.Target> ls_target_lists)
        {
            List<KnockBackTarget> list;
            LogSkill.Target target;
            List<LogSkill.Target>.Enumerator enumerator;
            int num;
            bool flag;
            KnockBackTarget target2;
            List<KnockBackTarget>.Enumerator enumerator2;
            Grid grid;
            KnockBackTarget target3;
            List<KnockBackTarget>.Enumerator enumerator3;
            if (self == null)
            {
                goto Label_001F;
            }
            if (skill == null)
            {
                goto Label_001F;
            }
            if (ls_target_lists == null)
            {
                goto Label_001F;
            }
            if (ls_target_lists.Count != null)
            {
                goto Label_0020;
            }
        Label_001F:
            return;
        Label_0020:
            this.sameJudgeUnitLists.Clear();
            list = new List<KnockBackTarget>(ls_target_lists.Count);
            enumerator = ls_target_lists.GetEnumerator();
        Label_0040:
            try
            {
                goto Label_009C;
            Label_0045:
                target = &enumerator.Current;
                this.sameJudgeUnitLists.Add(target.target);
                if (target.target.IsKnockBack != null)
                {
                    goto Label_0073;
                }
                goto Label_009C;
            Label_0073:
                target.KnockBackGrid = null;
                list.Add(new KnockBackTarget(target, target.target.x, target.target.y));
            Label_009C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0045;
                }
                goto Label_00B9;
            }
            finally
            {
            Label_00AD:
                ((List<LogSkill.Target>.Enumerator) enumerator).Dispose();
            }
        Label_00B9:
            num = 0;
            goto Label_0208;
        Label_00C0:
            flag = 0;
            enumerator2 = list.GetEnumerator();
        Label_00CB:
            try
            {
                goto Label_01DA;
            Label_00D0:
                target2 = &enumerator2.Current;
                if (target2.mLsTarget == null)
                {
                    goto Label_01DA;
                }
                if (target2.mMoveLen < skill.KnockBackVal)
                {
                    goto Label_0101;
                }
                goto Label_01DA;
            Label_0101:
                grid = this.getGridKnockBack(self, target2.mLsTarget.target, skill, gx, gy, skill.KnockBackVal - target2.mMoveLen, target2.mMoveDir);
                if (grid != null)
                {
                    goto Label_0140;
                }
                goto Label_01DA;
            Label_0140:
                target2.mLsTarget.KnockBackGrid = grid;
                target2.mLsTarget.target.x = grid.x;
                target2.mLsTarget.target.y = grid.y;
                target2.mMoveLen = Math.Abs(grid.x - target2.mUnitGx) + Math.Abs(grid.y - target2.mUnitGy);
                target2.mMoveDir = this.unitDirectionFromPos(target2.mUnitGx, target2.mUnitGy, grid.x, grid.y);
                flag = 1;
            Label_01DA:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00D0;
                }
                goto Label_01F8;
            }
            finally
            {
            Label_01EB:
                ((List<KnockBackTarget>.Enumerator) enumerator2).Dispose();
            }
        Label_01F8:
            if (flag != null)
            {
                goto Label_0204;
            }
            goto Label_0214;
        Label_0204:
            num += 1;
        Label_0208:
            if (num < list.Count)
            {
                goto Label_00C0;
            }
        Label_0214:
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_029A;
            }
            enumerator3 = list.GetEnumerator();
        Label_0228:
            try
            {
                goto Label_027C;
            Label_022D:
                target3 = &enumerator3.Current;
                if (target3.mLsTarget.KnockBackGrid != null)
                {
                    goto Label_024C;
                }
                goto Label_027C;
            Label_024C:
                target3.mLsTarget.target.x = target3.mUnitGx;
                target3.mLsTarget.target.y = target3.mUnitGy;
            Label_027C:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_022D;
                }
                goto Label_029A;
            }
            finally
            {
            Label_028D:
                ((List<KnockBackTarget>.Enumerator) enumerator3).Dispose();
            }
        Label_029A:
            this.sameJudgeUnitLists.Clear();
            return;
        }

        private void RefreshTreasureTargetAI()
        {
            int num;
            BattleMap map;
            int num2;
            Grid grid;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            <RefreshTreasureTargetAI>c__AnonStorey1C7 storeyc;
            <RefreshTreasureTargetAI>c__AnonStorey1C8 storeyc2;
            if (this.mQuestParam.CheckAllowedAutoBattle() != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if (GameUtility.Config_AutoMode_Treasure.Value != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if (this.mTreasures.Count != null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            num = 0;
            goto Label_004F;
        Label_0039:
            this.mUnits[num].TreasureGainTarget = null;
            num += 1;
        Label_004F:
            if (num < this.mUnits.Count)
            {
                goto Label_0039;
            }
            map = this.CurrentMap;
            num2 = 0;
            goto Label_02D2;
        Label_006E:
            storeyc = new <RefreshTreasureTargetAI>c__AnonStorey1C7();
            if ((this.mTreasures[num2].EventTrigger == null) || (this.mTreasures[num2].EventTrigger.EventType != 3))
            {
                goto Label_02CE;
            }
            if (this.mTreasures[num2].EventTrigger.Count != null)
            {
                goto Label_00C7;
            }
            goto Label_02CE;
        Label_00C7:
            storeyc.suited = null;
            grid = map[this.mTreasures[num2].x, this.mTreasures[num2].y];
            if (this.FindUnitAtGrid(grid) == null)
            {
                goto Label_0109;
            }
            goto Label_02CE;
        Label_0109:
            num3 = 0xff;
            num4 = 0;
            goto Label_02A3;
        Label_0118:
            storeyc2 = new <RefreshTreasureTargetAI>c__AnonStorey1C8();
            storeyc2.<>f__ref$455 = storeyc;
            storeyc2.unit = this.mPlayer[num4];
            if (storeyc2.unit.UnitType == null)
            {
                goto Label_0152;
            }
            goto Label_029D;
        Label_0152:
            if (storeyc2.unit.TreasureGainTarget == null)
            {
                goto Label_0168;
            }
            goto Label_029D;
        Label_0168:
            if (storeyc2.unit.IsEntry == null)
            {
                goto Label_029D;
            }
            if (storeyc2.unit.IsSub == null)
            {
                goto Label_018F;
            }
            goto Label_029D;
        Label_018F:
            if (storeyc2.unit.IsEnableAutoMode() != null)
            {
                goto Label_01A5;
            }
            goto Label_029D;
        Label_01A5:
            if (storeyc2.unit.IsEnableMoveCondition(1) != null)
            {
                goto Label_01BC;
            }
            goto Label_029D;
        Label_01BC:
            num5 = storeyc2.unit.GetMoveCount(1);
            if (num5 != null)
            {
                goto Label_01D7;
            }
            goto Label_029D;
        Label_01D7:
            map.CalcMoveSteps(storeyc2.unit, map[storeyc2.unit.x, storeyc2.unit.y], 0);
            num6 = (grid.step / num5) + (((grid.step % num5) <= 0) ? 0 : 1);
            if (num6 <= num3)
            {
                goto Label_0234;
            }
            goto Label_029D;
        Label_0234:
            if (num6 != num3)
            {
                goto Label_028B;
            }
            if (storeyc.suited == null)
            {
                goto Label_028B;
            }
            num7 = this.mOrder.FindIndex(new Predicate<OrderData>(storeyc2.<>m__6E));
            num8 = this.mOrder.FindIndex(new Predicate<OrderData>(storeyc2.<>m__6F));
            if (num7 >= num8)
            {
                goto Label_028B;
            }
            goto Label_029D;
        Label_028B:
            storeyc.suited = storeyc2.unit;
            num3 = num6;
        Label_029D:
            num4 += 1;
        Label_02A3:
            if (num4 < this.mPlayer.Count)
            {
                goto Label_0118;
            }
            if (storeyc.suited == null)
            {
                goto Label_02CE;
            }
            storeyc.suited.TreasureGainTarget = grid;
        Label_02CE:
            num2 += 1;
        Label_02D2:
            if (num2 < this.mTreasures.Count)
            {
                goto Label_006E;
            }
            return;
        }

        private void RefreshUseSkillMap(Unit self, bool is_add_act)
        {
            AIParam param;
            bool flag;
            bool flag2;
            int num;
            bool flag3;
            AIAction action;
            int num2;
            SkillData data;
            List<List<SkillData>> list;
            int num3;
            List<SkillData> list2;
            bool flag4;
            int num4;
            eAIActionNoExecAct act;
            SkillCategory category;
            param = self.AI;
            flag = 1;
            if ((self.IsEnableSkillCondition(0) != null) && (this.CheckDisableAbilities(self) == null))
            {
                goto Label_0023;
            }
            flag = 0;
        Label_0023:
            if (((this.mQuestParam.CheckAllowedAutoBattle() == null) || (self.IsEnableAutoMode() == null)) || (GameUtility.Config_AutoMode_DisableSkill.Value == null))
            {
                goto Label_004F;
            }
            flag = 0;
        Label_004F:
            if ((param == null) || (param.CheckFlag(0x10) == null))
            {
                goto Label_0064;
            }
            flag = 0;
        Label_0064:
            if (is_add_act == null)
            {
                goto Label_018A;
            }
            if (this.mSkillMap.useSkillLists == null)
            {
                goto Label_008A;
            }
            this.mSkillMap.useSkillLists.Clear();
        Label_008A:
            if (this.mSkillMap.forceSkillList == null)
            {
                goto Label_00AA;
            }
            this.mSkillMap.forceSkillList.Clear();
        Label_00AA:
            if (this.mSkillMap.healSkills == null)
            {
                goto Label_00CA;
            }
            this.mSkillMap.healSkills.Clear();
        Label_00CA:
            if (this.mSkillMap.damageSkills == null)
            {
                goto Label_00EA;
            }
            this.mSkillMap.damageSkills.Clear();
        Label_00EA:
            if (this.mSkillMap.supportSkills == null)
            {
                goto Label_010A;
            }
            this.mSkillMap.supportSkills.Clear();
        Label_010A:
            if (this.mSkillMap.cureConditionSkills == null)
            {
                goto Label_012A;
            }
            this.mSkillMap.cureConditionSkills.Clear();
        Label_012A:
            if (this.mSkillMap.failConditionSkills == null)
            {
                goto Label_014A;
            }
            this.mSkillMap.failConditionSkills.Clear();
        Label_014A:
            if (this.mSkillMap.disableConditionSkills == null)
            {
                goto Label_016A;
            }
            this.mSkillMap.disableConditionSkills.Clear();
        Label_016A:
            if (this.mSkillMap.exeSkills == null)
            {
                goto Label_018A;
            }
            this.mSkillMap.exeSkills.Clear();
        Label_018A:
            flag2 = 0;
            if (flag == null)
            {
                goto Label_02C3;
            }
            num = this.GetGems(self);
            flag3 = 1;
            if (self.IsAIActionTable() == null)
            {
                goto Label_0237;
            }
            action = this.mSkillMap.GetAction();
            if (action == null)
            {
                goto Label_0234;
            }
            if ((is_add_act == null) || (action.noExecAct == null))
            {
                goto Label_021E;
            }
            act = action.noExecAct;
            if ((act == 1) || (act == 2))
            {
                goto Label_01EC;
            }
            goto Label_0203;
        Label_01EC:
            flag3 = 1;
            if (action.noExecAct != 1)
            {
                goto Label_0237;
            }
            flag2 = 1;
            goto Label_0219;
        Label_0203:
            flag3 = action.type == 2;
        Label_0219:
            goto Label_022F;
        Label_021E:
            flag3 = action.type == 2;
        Label_022F:
            goto Label_0237;
        Label_0234:
            flag3 = 0;
        Label_0237:
            if (flag3 == null)
            {
                goto Label_02C3;
            }
            if (num < param.gems_border)
            {
                goto Label_02B3;
            }
            num2 = 0;
            goto Label_02A1;
        Label_0257:
            data = self.GetSkillForUseCount(self.BattleSkills[num2].SkillID, 0);
            data = (data != null) ? data : self.BattleSkills[num2];
            this.EntryUseSkill(self, data, 0, flag2);
            num2 += 1;
        Label_02A1:
            if (num2 < self.BattleSkills.Count)
            {
                goto Label_0257;
            }
        Label_02B3:
            this.EntryUseSkill(self, self.AIForceSkill, 1, flag2);
        Label_02C3:
            this.EntryUseSkill(self, self.GetAttackSkill(), this.mSkillMap.forceSkillList.Count > 0, flag2);
            list = this.mSkillMap.useSkillLists;
            if (param == null)
            {
                goto Label_04D3;
            }
            if (param.SkillCategoryPriorities == null)
            {
                goto Label_04D3;
            }
            num3 = 0;
            goto Label_0497;
        Label_030B:
            list2 = null;
            category = param.SkillCategoryPriorities[num3];
            switch (category)
            {
                case 0:
                    goto Label_033D;

                case 1:
                    goto Label_034F;

                case 2:
                    goto Label_036D;

                case 3:
                    goto Label_0420;

                case 4:
                    goto Label_0432;

                case 5:
                    goto Label_0444;
            }
            goto Label_0456;
        Label_033D:
            list2 = this.mSkillMap.damageSkills;
            goto Label_0456;
        Label_034F:
            if (this.GetHealUnitCount(self) == null)
            {
                goto Label_0456;
            }
            list2 = this.mSkillMap.healSkills;
            goto Label_0456;
        Label_036D:
            if (param.CheckFlag(0x100) == null)
            {
                goto Label_040E;
            }
            flag4 = 0;
            num4 = 0;
            goto Label_03F0;
        Label_0388:
            if (self.BuffAttachments[num4].IsPassive == null)
            {
                goto Label_03A9;
            }
            goto Label_03EA;
        Label_03A9:
            if (self.BuffAttachments[num4].BuffType == null)
            {
                goto Label_03C5;
            }
            goto Label_03EA;
        Label_03C5:
            if (self.BuffAttachments[num4].user == self)
            {
                goto Label_03E2;
            }
            goto Label_03EA;
        Label_03E2:
            flag4 = 1;
            goto Label_0402;
        Label_03EA:
            num4 += 1;
        Label_03F0:
            if (num4 < self.BuffAttachments.Count)
            {
                goto Label_0388;
            }
        Label_0402:
            if (flag4 == null)
            {
                goto Label_040E;
            }
            goto Label_0491;
        Label_040E:
            list2 = this.mSkillMap.supportSkills;
            goto Label_0456;
        Label_0420:
            list2 = this.mSkillMap.cureConditionSkills;
            goto Label_0456;
        Label_0432:
            list2 = this.mSkillMap.failConditionSkills;
            goto Label_0456;
        Label_0444:
            list2 = this.mSkillMap.disableConditionSkills;
        Label_0456:
            if (list2 == null)
            {
                goto Label_0470;
            }
            if (list.Contains(list2) == null)
            {
                goto Label_0470;
            }
            goto Label_0491;
        Label_0470:
            if (list2 != null)
            {
                goto Label_0488;
            }
            list.Add(new List<SkillData>());
            goto Label_0491;
        Label_0488:
            list.Add(list2);
        Label_0491:
            num3 += 1;
        Label_0497:
            if (num3 < ((int) param.SkillCategoryPriorities.Length))
            {
                goto Label_030B;
            }
            if (this.mSkillMap.exeSkills.Count <= 0)
            {
                goto Label_04E5;
            }
            list.Add(this.mSkillMap.exeSkills);
            goto Label_04E5;
        Label_04D3:
            list.Add(this.mSkillMap.damageSkills);
        Label_04E5:
            return;
        }

        public void Release()
        {
            int num;
            if (this.mLogs == null)
            {
                goto Label_001D;
            }
            this.mLogs.Release();
            this.mLogs = null;
        Label_001D:
            if (this.mOrder == null)
            {
                goto Label_003A;
            }
            this.mOrder.Clear();
            this.mOrder = null;
        Label_003A:
            this.mRecord = null;
            this.mRand = null;
            if (this.mAllUnits == null)
            {
                goto Label_0065;
            }
            this.mAllUnits.Clear();
            this.mAllUnits = null;
        Label_0065:
            if (this.mMap == null)
            {
                goto Label_00C5;
            }
            num = 0;
            goto Label_00A2;
        Label_0077:
            if (this.mMap[num] != null)
            {
                goto Label_008D;
            }
            goto Label_009E;
        Label_008D:
            this.mMap[num].Release();
        Label_009E:
            num += 1;
        Label_00A2:
            if (num < this.mMap.Count)
            {
                goto Label_0077;
            }
            this.mMap.Clear();
            this.mMap = null;
        Label_00C5:
            this.mMapIndex = 0;
            this.mEnemys = null;
            this.mPlayer = null;
            this.mQuestParam = null;
            this.ReleaseAi();
            return;
        }

        private void ReleaseAi()
        {
        }

        public unsafe void RelinkTrickGimmickEvents()
        {
            GimmickEvent event2;
            List<GimmickEvent>.Enumerator enumerator;
            enumerator = this.mGimmickEvents.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_00B5;
            Label_0011:
                event2 = &enumerator.Current;
                if (event2.td_targets.Count == null)
                {
                    goto Label_0058;
                }
                event2.td_targets.Clear();
                this.GetConditionTrickByTrickID(event2.td_targets, event2.td_iname);
                this.GetConditionTrickByTag(event2.td_targets, event2.td_tag);
            Label_0058:
                if (event2.condition.td_targets.Count == null)
                {
                    goto Label_00B5;
                }
                event2.condition.td_targets.Clear();
                this.GetConditionTrickByTrickID(event2.condition.td_targets, event2.condition.td_iname);
                this.GetConditionTrickByTag(event2.condition.td_targets, event2.condition.td_tag);
            Label_00B5:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_00D2;
            }
            finally
            {
            Label_00C6:
                ((List<GimmickEvent>.Enumerator) enumerator).Dispose();
            }
        Label_00D2:
            return;
        }

        private GridMap<bool> RemoveCantMove(Unit self, GridMap<bool> rangeMap, SkillData skill)
        {
            BattleMap map;
            int num;
            int num2;
            map = this.CurrentMap;
            num = 0;
            goto Label_0053;
        Label_000E:
            num2 = 0;
            goto Label_0043;
        Label_0015:
            if (rangeMap.get(num, num2) != null)
            {
                goto Label_0027;
            }
            goto Label_003F;
        Label_0027:
            rangeMap.set(num, num2, map.CheckEnableMoveTeleport(self, map[num, num2], skill));
        Label_003F:
            num2 += 1;
        Label_0043:
            if (num2 < rangeMap.h)
            {
                goto Label_0015;
            }
            num += 1;
        Label_0053:
            if (num < rangeMap.w)
            {
                goto Label_000E;
            }
            return rangeMap;
        }

        public void RemoveLog()
        {
            this.mLogs.RemoveLog();
            return;
        }

        public static void RemoveSuspendData()
        {
            BattleSuspend.RemoveData();
            return;
        }

        public void ResumeDead(Unit target)
        {
            this.Dead(null, target, 0, 1);
            return;
        }

        private void Revive(Unit target, int hp)
        {
            LogRevive revive;
            revive = this.Log<LogRevive>();
            revive.self = target;
            revive.hp = hp;
            return;
        }

        public bool SaveSuspendData()
        {
            if (BattleSuspend.SaveData() != null)
            {
                goto Label_000C;
            }
            return 0;
        Label_000C:
            return 1;
        }

        private unsafe bool Searching(Unit self)
        {
            BattleMap map;
            int num;
            GridMap<bool> map2;
            int num2;
            int num3;
            Unit unit;
            DebugUtility.Assert((self == null) == 0, "self == null");
            if (self.IsUnitFlag(8) == null)
            {
                goto Label_001F;
            }
            return 1;
        Label_001F:
            map = this.CurrentMap;
            DebugUtility.Assert((map == null) == 0, "map == null");
            num = self.GetSearchRange();
            if (num > 0)
            {
                goto Label_0047;
            }
            return 0;
        Label_0047:
            map2 = new GridMap<bool>(map.Width, map.Height);
            this.CreateSelectGridMap(self, self.x, self.y, 0, num, 1, 0, 0, 0, 0, 0x63, 0, &map2, 0);
            num2 = 0;
            goto Label_00E9;
        Label_0081:
            num3 = 0;
            goto Label_00D8;
        Label_0089:
            if (map2.get(num2, num3) != null)
            {
                goto Label_009C;
            }
            goto Label_00D2;
        Label_009C:
            unit = this.FindUnitAtGrid(map[num2, num3]);
            if (unit != null)
            {
                goto Label_00B9;
            }
            goto Label_00D2;
        Label_00B9:
            if (unit.Side != self.Side)
            {
                goto Label_00D0;
            }
            goto Label_00D2;
        Label_00D0:
            return 1;
        Label_00D2:
            num3 += 1;
        Label_00D8:
            if (num3 < map2.h)
            {
                goto Label_0089;
            }
            num2 += 1;
        Label_00E9:
            if (num2 < map2.w)
            {
                goto Label_0081;
            }
            return 0;
        }

        public List<Unit> SearchTargetsInGridMap(Unit self, SkillData skill, GridMap<bool> areamap)
        {
            List<Unit> list;
            list = new List<Unit>(this.mOrder.Count);
            this.SearchTargetsInGridMap(self, skill, areamap, list);
            return list;
        }

        public List<Unit> SearchTargetsInGridMap(Unit self, SkillData skill, GridMap<bool> areamap, List<Unit> targets)
        {
            BattleMap map;
            Grid grid;
            int num;
            int num2;
            Unit unit;
            int num3;
            int num4;
            Unit unit2;
            map = this.CurrentMap;
            targets.Clear();
            if (areamap != null)
            {
                goto Label_00A6;
            }
            grid = map[self.x, self.y];
            num = this.GetUnitMaxAttackHeight(self, skill);
            num2 = 0;
            goto Label_0092;
        Label_0037:
            unit = this.mUnits[num2];
            if (skill == null)
            {
                goto Label_005F;
            }
            if (this.CheckSkillTarget(self, unit, skill) != null)
            {
                goto Label_005F;
            }
            goto Label_008E;
        Label_005F:
            if (this.CheckEnableAttackHeight(grid, map[unit.x, unit.y], num) != null)
            {
                goto Label_0085;
            }
            goto Label_008E;
        Label_0085:
            targets.Add(unit);
        Label_008E:
            num2 += 1;
        Label_0092:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0037;
            }
            return targets;
        Label_00A6:
            num3 = 0;
            goto Label_015F;
        Label_00AE:
            num4 = 0;
            goto Label_014C;
        Label_00B6:
            if (areamap.get(num3, num4) != null)
            {
                goto Label_00CA;
            }
            goto Label_0146;
        Label_00CA:
            unit2 = this.FindUnitAtGrid(map[num3, num4]);
            if (unit2 != null)
            {
                goto Label_0109;
            }
            unit2 = this.FindGimmickAtGrid(map[num3, num4], 0, null);
            if (this.IsTargetBreakUnit(self, unit2, skill) != null)
            {
                goto Label_0109;
            }
            unit2 = null;
        Label_0109:
            if (unit2 == null)
            {
                goto Label_0146;
            }
            if (targets.Contains(unit2) == null)
            {
                goto Label_0123;
            }
            goto Label_0146;
        Label_0123:
            if (skill == null)
            {
                goto Label_013D;
            }
            if (this.CheckSkillTarget(self, unit2, skill) != null)
            {
                goto Label_013D;
            }
            goto Label_0146;
        Label_013D:
            targets.Add(unit2);
        Label_0146:
            num4 += 1;
        Label_014C:
            if (num4 < areamap.h)
            {
                goto Label_00B6;
            }
            num3 += 1;
        Label_015F:
            if (num3 < areamap.w)
            {
                goto Label_00AE;
            }
            return targets;
        }

        public unsafe Unit SearchTransformUnit(Unit self)
        {
            Unit unit;
            Unit unit2;
            List<Unit>.Enumerator enumerator;
            UnitEntryTrigger trigger;
            List<UnitEntryTrigger>.Enumerator enumerator2;
            unit = null;
            enumerator = this.mUnits.GetEnumerator();
        Label_000E:
            try
            {
                goto Label_00E7;
            Label_0013:
                unit2 = &enumerator.Current;
                if (unit2.IsGimmick == null)
                {
                    goto Label_0036;
                }
                if (unit2.IsBreakObj != null)
                {
                    goto Label_0036;
                }
                goto Label_00E7;
            Label_0036:
                if (unit2.IsEntry == null)
                {
                    goto Label_0046;
                }
                goto Label_00E7;
            Label_0046:
                if (unit2.IsDead == null)
                {
                    goto Label_0056;
                }
                goto Label_00E7;
            Label_0056:
                if (unit2.IsSub == null)
                {
                    goto Label_0066;
                }
                goto Label_00E7;
            Label_0066:
                if (unit2.EntryTriggers != null)
                {
                    goto Label_0076;
                }
                goto Label_00E7;
            Label_0076:
                enumerator2 = unit2.EntryTriggers.GetEnumerator();
            Label_0083:
                try
                {
                    goto Label_00BE;
                Label_0088:
                    trigger = &enumerator2.Current;
                    if (trigger.type == 6)
                    {
                        goto Label_00A1;
                    }
                    goto Label_00BE;
                Label_00A1:
                    if ((trigger.unit == self.UniqueName) == null)
                    {
                        goto Label_00BE;
                    }
                    unit = unit2;
                    goto Label_00CA;
                Label_00BE:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0088;
                    }
                Label_00CA:
                    goto Label_00DC;
                }
                finally
                {
                Label_00CF:
                    ((List<UnitEntryTrigger>.Enumerator) enumerator2).Dispose();
                }
            Label_00DC:
                if (unit == null)
                {
                    goto Label_00E7;
                }
                goto Label_00F3;
            Label_00E7:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0013;
                }
            Label_00F3:
                goto Label_0104;
            }
            finally
            {
            Label_00F8:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0104:
            return unit;
        }

        private void SetBattleFlag(EBattleFlag tgt, bool sw)
        {
            if (sw == null)
            {
                goto Label_0021;
            }
            this.mBtlFlags |= 1 << ((tgt & 0x1f) & 0x1f);
            goto Label_0038;
        Label_0021:
            this.mBtlFlags &= ~(1 << ((tgt & 0x1f) & 0x1f));
        Label_0038:
            return;
        }

        public void SetBattleID(long btlid)
        {
            this.mBtlID = btlid;
            return;
        }

        public static unsafe void SetBuffBits(BaseStatus status, ref BuffBit buff, ref BuffBit debuff)
        {
            int num;
            ParamTypes types;
            int num2;
            ParamTypes types2;
            ParamTypes types3;
            int num3;
            ParamTypes types4;
            ParamTypes types5;
            int num4;
            ParamTypes types6;
            num = 0;
            goto Label_005F;
        Label_0007:
            if (status.param[num] != null)
            {
                goto Label_0022;
            }
            goto Label_005B;
        Label_0022:
            types = status.param.GetParamTypes(num);
            if (status.param[num] <= 0)
            {
                goto Label_0053;
            }
            *(buff).SetBit(types);
            goto Label_005B;
        Label_0053:
            *(debuff).SetBit(types);
        Label_005B:
            num += 1;
        Label_005F:
            if (num < status.param.Length)
            {
                goto Label_0007;
            }
            num2 = 0;
            goto Label_0144;
        Label_0077:
            if (*(&(status.element_assist.values[num2])) == null)
            {
                goto Label_00DA;
            }
            types2 = status.element_assist.GetAssistParamTypes(num2);
            if (*(&(status.element_assist.values[num2])) <= 0)
            {
                goto Label_00D2;
            }
            *(buff).SetBit(types2);
            goto Label_00DA;
        Label_00D2:
            *(debuff).SetBit(types2);
        Label_00DA:
            if (*(&(status.element_resist.values[num2])) == null)
            {
                goto Label_0140;
            }
            types3 = status.element_resist.GetResistParamTypes(num2);
            if (*(&(status.element_resist.values[num2])) <= 0)
            {
                goto Label_0137;
            }
            *(buff).SetBit(types3);
            goto Label_0140;
        Label_0137:
            *(debuff).SetBit(types3);
        Label_0140:
            num2 += 1;
        Label_0144:
            if (num2 < ElementParam.MAX_ELEMENT)
            {
                goto Label_0077;
            }
            num3 = 0;
            goto Label_022F;
        Label_0157:
            if (*(&(status.enchant_assist.values[num3])) == null)
            {
                goto Label_01C0;
            }
            types4 = status.enchant_assist.GetAssistParamTypes(num3);
            if (*(&(status.enchant_assist.values[num3])) <= 0)
            {
                goto Label_01B7;
            }
            *(buff).SetBit(types4);
            goto Label_01C0;
        Label_01B7:
            *(debuff).SetBit(types4);
        Label_01C0:
            if (*(&(status.enchant_resist.values[num3])) == null)
            {
                goto Label_0229;
            }
            types5 = status.enchant_resist.GetResistParamTypes(num3);
            if (*(&(status.enchant_resist.values[num3])) <= 0)
            {
                goto Label_0220;
            }
            *(buff).SetBit(types5);
            goto Label_0229;
        Label_0220:
            *(debuff).SetBit(types5);
        Label_0229:
            num3 += 1;
        Label_022F:
            if (num3 < EnchantParam.MAX_ENCHANGT)
            {
                goto Label_0157;
            }
            num4 = 0;
            goto Label_02B7;
        Label_0243:
            if (*(&(status.bonus.values[num4])) != null)
            {
                goto Label_0269;
            }
            goto Label_02B1;
        Label_0269:
            types6 = status.bonus.GetParamTypes(num4);
            if (*(&(status.bonus.values[num4])) <= 0)
            {
                goto Label_02A8;
            }
            *(buff).SetBit(types6);
            goto Label_02B1;
        Label_02A8:
            *(debuff).SetBit(types6);
        Label_02B1:
            num4 += 1;
        Label_02B7:
            if (num4 < ((int) status.bonus.values.Length))
            {
                goto Label_0243;
            }
            return;
        }

        public void SetGems(Unit unit, int gems)
        {
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0033;
            }
            unit.Gems = Math.Max(Math.Min(gems, unit.MaximumStatus.param.mp), 0);
        Label_0033:
            return;
        }

        private void SetGridMap(ref GridMap<bool> gridmap, Grid start, Grid goal)
        {
            if (goal != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (*(gridmap).get(goal.x, goal.y) == null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            *(gridmap).set(goal.x, goal.y, 1);
            return;
        }

        public void SetManualPlayFlag()
        {
            if (this.IsAutoBattle != null)
            {
                goto Label_0022;
            }
            if (this.CurrentUnit.Side != null)
            {
                goto Label_0022;
            }
            this.PlayByManually = 1;
        Label_0022:
            return;
        }

        public void SetRandDamageSeed(int index, uint seed)
        {
            this.mRandDamage.SetSeed(index, seed);
            return;
        }

        public void SetRandSeed(int index, uint seed)
        {
            this.mRand.SetSeed(index, seed);
            return;
        }

        public void SetResumeWait()
        {
            this.mResumeState = 2;
            return;
        }

        private void SetReward(QuestBonusObjective bonus)
        {
            ArtifactParam param;
            int num;
            ConceptCardParam param2;
            int num2;
            ItemParam param3;
            int num3;
            <SetReward>c__AnonStorey1B7 storeyb;
            storeyb = new <SetReward>c__AnonStorey1B7();
            storeyb.bonus = bonus;
            if (storeyb.bonus.itemType != 100)
            {
                goto Label_0023;
            }
            return;
        Label_0023:
            if (storeyb.bonus.itemType != 3)
            {
                goto Label_0090;
            }
            param = Enumerable.FirstOrDefault<ArtifactParam>(MonoSingleton<GameManager>.Instance.MasterParam.Artifacts, new Func<ArtifactParam, bool>(storeyb.<>m__5E));
            if (param == null)
            {
                goto Label_0153;
            }
            num = 0;
            goto Label_0079;
        Label_0064:
            this.mRecord.artifacts.Add(param);
            num += 1;
        Label_0079:
            if (num < storeyb.bonus.itemNum)
            {
                goto Label_0064;
            }
            goto Label_0153;
        Label_0090:
            if (storeyb.bonus.itemType != 6)
            {
                goto Label_00FC;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(storeyb.bonus.item);
            if (param2 == null)
            {
                goto Label_0153;
            }
            num2 = 0;
            goto Label_00E5;
        Label_00CB:
            this.mRecord.items.Add(new DropItemParam(param2));
            num2 += 1;
        Label_00E5:
            if (num2 < storeyb.bonus.itemNum)
            {
                goto Label_00CB;
            }
            goto Label_0153;
        Label_00FC:
            param3 = MonoSingleton<GameManager>.Instance.GetItemParam(storeyb.bonus.item);
            if (param3 == null)
            {
                goto Label_0153;
            }
            num3 = 0;
            goto Label_0140;
        Label_0123:
            this.mRecord.items.Add(new DropItemParam(param3));
            num3 += 1;
        Label_0140:
            if (num3 < storeyb.bonus.itemNum)
            {
                goto Label_0123;
            }
        Label_0153:
            return;
        }

        public void SetUnitListKnockBack(List<Unit> unit_lists)
        {
            this.sameJudgeUnitLists.Clear();
            if (unit_lists != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.sameJudgeUnitLists = unit_lists;
            return;
        }

        public unsafe bool SetupMultiPlayUnit(UnitData[] units, int[] ownerPlayerIndex, int[] placementIndex, bool[] sub)
        {
            MyPhoton photon;
            GameManager manager;
            List<UnitSetting> list;
            List<UnitSetting> list2;
            PlayerPartyTypes types;
            PlayerPartyTypes types2;
            int num;
            Unit unit;
            int num2;
            Unit unit2;
            int num3;
            UnitData data;
            Unit unit3;
            photon = PunMonoSingleton<MyPhoton>.Instance;
            manager = MonoSingleton<GameManager>.Instance;
            list = this.mMap[0].PartyUnitSettings;
            list2 = this.mMap[0].ArenaUnitSettings;
            this.mQuestParam.GetPartyTypes(&types, &types2);
            if ((manager.AudienceMode == null) && (manager.IsVSCpuBattle == null))
            {
                goto Label_0175;
            }
            this.IsMultiPlay = 1;
            this.IsMultiVersus = 1;
            this.IsRankMatch = 0;
            this.mLeaderIndex = 0;
            this.mEnemyLeaderIndex = 0;
            this.VersusTurnMax = this.mQuestParam.VersusMoveCount;
            this.RemainVersusTurnCount = 0;
            num = 0;
            goto Label_0166;
        Label_00A2:
            if (units[num] != null)
            {
                goto Label_00B0;
            }
            goto Label_0160;
        Label_00B0:
            unit = new Unit();
            if (unit.Setup(units[num], (ownerPlayerIndex[num] != 1) ? list2[placementIndex[num]] : list[placementIndex[num]], null, null) != null)
            {
                goto Label_00F9;
            }
            this.DebugErr("failed unit Setup");
            return 0;
        Label_00F9:
            unit.IsPartyMember = 1;
            unit.SetUnitFlag(8, 1);
            unit.OwnerPlayerIndex = ownerPlayerIndex[num];
            unit.Side = (ownerPlayerIndex[num] != 1) ? 1 : 0;
            if (unit.Side != null)
            {
                goto Label_0146;
            }
            this.mPlayer.Add(unit);
        Label_0146:
            this.mAllUnits.Add(unit);
            this.mStartingMembers.Add(unit);
        Label_0160:
            num += 1;
        Label_0166:
            if (num < ((int) units.Length))
            {
                goto Label_00A2;
            }
            goto Label_03D2;
        Label_0175:
            if (photon.IsMultiVersus == null)
            {
                goto Label_02A8;
            }
            this.IsMultiVersus = photon.IsMultiVersus;
            this.IsRankMatch = photon.IsRankMatch;
            this.mLeaderIndex = 0;
            this.mEnemyLeaderIndex = 0;
            this.VersusTurnMax = this.mQuestParam.VersusMoveCount;
            this.RemainVersusTurnCount = 0;
            num2 = 0;
            goto Label_0299;
        Label_01D0:
            if (units[num2] != null)
            {
                goto Label_01DE;
            }
            goto Label_0293;
        Label_01DE:
            unit2 = new Unit();
            if (unit2.Setup(units[num2], (ownerPlayerIndex[num2] != 1) ? list2[placementIndex[num2]] : list[placementIndex[num2]], null, null) != null)
            {
                goto Label_0227;
            }
            this.DebugErr("failed unit Setup");
            return 0;
        Label_0227:
            unit2.IsPartyMember = 1;
            unit2.SetUnitFlag(8, 1);
            unit2.OwnerPlayerIndex = ownerPlayerIndex[num2];
            unit2.Side = (ownerPlayerIndex[num2] == photon.MyPlayerIndex) ? 0 : 1;
            if (unit2.Side != null)
            {
                goto Label_0279;
            }
            this.mPlayer.Add(unit2);
        Label_0279:
            this.mAllUnits.Add(unit2);
            this.mStartingMembers.Add(unit2);
        Label_0293:
            num2 += 1;
        Label_0299:
            if (num2 < ((int) units.Length))
            {
                goto Label_01D0;
            }
            goto Label_03D2;
        Label_02A8:
            this.IsMultiTower = GlobalVars.SelectedMultiPlayRoomType == 2;
            num3 = 0;
            goto Label_03C8;
        Label_02BE:
            if (units[num3] != null)
            {
                goto Label_02CC;
            }
            goto Label_03C2;
        Label_02CC:
            data = new UnitData();
            data.Setup(units[num3]);
            data.SetJob(types);
            if (this.mQuestParam.IsUnitAllowed(units[num3]) != null)
            {
                goto Label_0300;
            }
            goto Label_03C2;
        Label_0300:
            unit3 = new Unit();
            if (this.IsMultiTower == null)
            {
                goto Label_0340;
            }
            if (unit3.Setup(units[num3], list[placementIndex[num3]], null, null) != null)
            {
                goto Label_0367;
            }
            this.DebugErr("failed unit Setup");
            return 0;
            goto Label_0367;
        Label_0340:
            if (unit3.Setup(units[num3], list[num3], null, null) != null)
            {
                goto Label_0367;
            }
            this.DebugErr("failed unit Setup");
            return 0;
        Label_0367:
            unit3.IsPartyMember = 1;
            unit3.SetUnitFlag(8, 1);
            unit3.OwnerPlayerIndex = ownerPlayerIndex[num3];
            unit3.IsSub = sub[num3];
            this.mPlayer.Add(unit3);
            this.mAllUnits.Add(unit3);
            if (unit3.IsSub != null)
            {
                goto Label_03C2;
            }
            this.mStartingMembers.Add(unit3);
        Label_03C2:
            num3 += 1;
        Label_03C8:
            if (num3 < ((int) units.Length))
            {
                goto Label_02BE;
            }
        Label_03D2:
            return 1;
        }

        private unsafe bool SetupSkillMapScore(Unit self, Grid goal, SkillData skill, SRPG.SkillMap.Score score)
        {
            BattleMap map;
            int num;
            int num2;
            ShotTarget target;
            List<Unit> list;
            int num3;
            int num4;
            int num5;
            Unit unit;
            LogSkill skill2;
            int num6;
            map = this.CurrentMap;
            num = &score.pos.x;
            num2 = &score.pos.y;
            target = null;
            list = new List<Unit>();
            this.GetExecuteSkillLineTarget(self, num, num2, skill, &list, &target);
            if (skill.IsAreaSkill() == null)
            {
                goto Label_00AC;
            }
            num3 = 0;
            goto Label_0095;
        Label_004B:
            num4 = 0;
            goto Label_007D;
        Label_0053:
            if (this.mScopeMap.get(num4, num3) == null)
            {
                goto Label_0077;
            }
            &score.range.Set(num4, num3);
        Label_0077:
            num4 += 1;
        Label_007D:
            if (num4 < this.mScopeMap.w)
            {
                goto Label_0053;
            }
            num3 += 1;
        Label_0095:
            if (num3 < this.mScopeMap.h)
            {
                goto Label_004B;
            }
            goto Label_00BA;
        Label_00AC:
            &score.range.Set(num, num2);
        Label_00BA:
            if (skill.IsTrickSkill() == null)
            {
                goto Label_00CA;
            }
            goto Label_02A4;
        Label_00CA:
            num5 = 0;
            goto Label_0113;
        Label_00D2:
            unit = list[num5];
            if (unit == null)
            {
                goto Label_010D;
            }
            if (unit.IsBreakObj == null)
            {
                goto Label_010D;
            }
            if (this.IsTargetBreakUnitAI(self, unit) != null)
            {
                goto Label_010D;
            }
            list.RemoveAt(num5);
            num5 -= 1;
        Label_010D:
            num5 += 1;
        Label_0113:
            if (num5 < list.Count)
            {
                goto Label_00D2;
            }
            if (list.Count <= 0)
            {
                goto Label_02A4;
            }
            this.SetBattleFlag(5, 1);
            this.mRandDamage.Seed(this.mSeedDamage);
            this.CurrentRand = this.mRandDamage;
            skill2 = new LogSkill();
            skill2.self = self;
            skill2.skill = skill;
            &skill2.pos.x = num;
            &skill2.pos.y = num2;
            skill2.reflect = null;
            num6 = 0;
            goto Label_01AC;
        Label_0194:
            skill2.SetSkillTarget(self, list[num6]);
            num6 += 1;
        Label_01AC:
            if (num6 < list.Count)
            {
                goto Label_0194;
            }
            if (target == null)
            {
                goto Label_021E;
            }
            &skill2.pos.x = target.end.x;
            &skill2.pos.y = target.end.y;
            skill2.rad = (int) (target.rad * 100.0);
            skill2.height = (int) (target.height * 100.0);
        Label_021E:
            this.ExecuteSkill(0, skill2, skill);
            self.x = goal.x;
            self.y = goal.y;
            this.CurrentRand = this.mRand;
            self.SetUnitFlag(0x20, 0);
            self.SetUnitFlag(0x40, 0);
            this.SetBattleFlag(5, 0);
            if (skill.TeleportType == null)
            {
                goto Label_029B;
            }
            if (skill2.TeleportGrid == null)
            {
                goto Label_0299;
            }
            if (map.CheckEnableMove(self, skill2.TeleportGrid, 0, 0) != null)
            {
                goto Label_029B;
            }
            return 0;
            goto Label_029B;
        Label_0299:
            return 0;
        Label_029B:
            score.log = skill2;
        Label_02A4:
            return 1;
        }

        private void SetVersusReward(VersusWinBonusRewardParam[] rewards)
        {
            GameManager manager;
            int num;
            VersusWinBonusRewardParam param;
            ItemParam param2;
            int num2;
            VERSUS_REWARD_TYPE versus_reward_type;
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            goto Label_00A5;
        Label_000D:
            param = rewards[num];
            switch (param.type)
            {
                case 0:
                    goto Label_0058;

                case 1:
                    goto Label_0031;

                case 2:
                    goto Label_0058;
            }
            goto Label_00A1;
        Label_0031:
            this.mRecord.gold += param.num;
            goto Label_00A1;
        Label_0058:
            param2 = manager.GetItemParam(param.iname);
            if (param2 == null)
            {
                goto Label_00A1;
            }
            num2 = 0;
            goto Label_008F;
        Label_0073:
            this.mRecord.items.Add(new DropItemParam(param2));
            num2 += 1;
        Label_008F:
            if (num2 < param.num)
            {
                goto Label_0073;
            }
        Label_00A1:
            num += 1;
        Label_00A5:
            if (num < ((int) rewards.Length))
            {
                goto Label_000D;
            }
            return;
        }

        private void ShieldSkill(Unit target, SkillData skill)
        {
            int num;
            int num2;
            if (target != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (skill == null)
            {
                goto Label_0024;
            }
            if (skill.EffectType != 8)
            {
                goto Label_0024;
            }
            if (skill.ShieldType != null)
            {
                goto Label_0025;
            }
        Label_0024:
            return;
        Label_0025:
            num = skill.EffectRate;
            if (0 >= num)
            {
                goto Label_0052;
            }
            if (num >= 100)
            {
                goto Label_0052;
            }
            num2 = this.GetRandom() % 100;
            if (num2 <= num)
            {
                goto Label_0052;
            }
            return;
        Label_0052:
            if (this.IsBattleFlag(5) == null)
            {
                goto Label_005F;
            }
            return;
        Label_005F:
            target.AddShield(skill);
            return;
        }

        public static int Sin(int v)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            num = v - ((v / 0x274) * 0x274);
            num2 = num;
            num3 = num;
            num4 = 10;
            num5 = 1;
            goto Label_003A;
        Label_001F:
            num2 *= -(num * num) / (((2 * num4) + 1) * (2 * num4));
            num3 += num2;
            num5 += 1;
        Label_003A:
            if (num5 <= num4)
            {
                goto Label_001F;
            }
            return num3;
        }

        private void SortAttackTargets(Unit unit, List<Unit> targets)
        {
            <SortAttackTargets>c__AnonStorey1C5 storeyc;
            <SortAttackTargets>c__AnonStorey1C4 storeyc2;
            storeyc = new <SortAttackTargets>c__AnonStorey1C5();
            storeyc.unit = unit;
            storeyc.<>f__this = this;
            DebugUtility.Assert((storeyc.unit == null) == 0, "unit == null");
            if (targets.Count <= 0)
            {
                goto Label_006D;
            }
            storeyc2 = new <SortAttackTargets>c__AnonStorey1C4();
            storeyc2.<>f__ref$453 = storeyc;
            storeyc2.<>f__this = this;
            storeyc2.rage = storeyc.unit.GetRageTarget();
            MySort<Unit>.Sort(targets, new Comparison<Unit>(storeyc2.<>m__6C));
        Label_006D:
            return;
        }

        public void SortSkillResult(Unit self, List<SkillResult> results)
        {
            <SortSkillResult>c__AnonStorey1BE storeybe;
            storeybe = new <SortSkillResult>c__AnonStorey1BE();
            storeybe.self = self;
            storeybe.bPositioning = (storeybe.self.AI == null) ? 0 : storeybe.self.AI.CheckFlag(1);
            MySort<SkillResult>.Sort(results, new Comparison<SkillResult>(storeybe.<>m__65));
            return;
        }

        public static int Sqrt(int v)
        {
            int num;
            int num2;
            if (v > 0)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            num = (v <= 1) ? 1 : v;
        Label_0018:
            num2 = num;
            num = ((v / num) + num) / 2;
            if (num < num2)
            {
                goto Label_0018;
            }
            return num2;
        }

        public void StartOrder(bool sync, bool force, bool judge)
        {
            this.Logs.Reset();
            this.ResumeState = 0;
            WeatherData.IsExecuteUpdate = 0;
            WeatherData.IsEntryConditionLog = 0;
            this.NextOrder(1, sync, force, judge);
            WeatherData.IsExecuteUpdate = 1;
            WeatherData.IsEntryConditionLog = 1;
            this.ClearAI();
            return;
        }

        private void StealSkill(Unit self, Unit target, SkillData skill, LogSkill log)
        {
        }

        private int SubGems(Unit unit, int gems)
        {
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_003A;
            }
            unit.Gems = Math.Max(Math.Min(unit.Gems - gems, unit.MaximumStatus.param.mp), 0);
        Label_003A:
            return unit.Gems;
        }

        public unsafe void TrickActionEndEffect(Unit self, bool is_update_buff)
        {
            TrickData data;
            bool flag;
            List<Unit> list;
            List<Unit> list2;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            Unit unit2;
            List<Unit>.Enumerator enumerator2;
            LogMapTrick trick;
            LogMapTrick.TargetInfo info;
            List<LogMapTrick.TargetInfo>.Enumerator enumerator3;
            Unit unit3;
            int num;
            if (self != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (TrickData.SearchEffect(self.x, self.y) != null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            flag = 0;
            list = new List<Unit>();
            list.Add(self);
            goto Label_01EA;
        Label_0034:
            list2 = new List<Unit>();
            enumerator = list.GetEnumerator();
        Label_0042:
            try
            {
                goto Label_0058;
            Label_0047:
                unit = &enumerator.Current;
                list2.Add(unit);
            Label_0058:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0047;
                }
                goto Label_0076;
            }
            finally
            {
            Label_0069:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_0076:
            list.Clear();
            enumerator2 = list2.GetEnumerator();
        Label_0084:
            try
            {
                goto Label_01CC;
            Label_0089:
                unit2 = &enumerator2.Current;
                trick = this.Log<LogMapTrick>();
                TrickData.ActionEffect(2, unit2, unit2.x, unit2.y, this.CurrentRand, trick);
                enumerator3 = trick.TargetInfoLists.GetEnumerator();
            Label_00C6:
                try
                {
                    goto Label_01AE;
                Label_00CB:
                    info = &enumerator3.Current;
                    unit3 = info.Target;
                    if (info.Damage <= 0)
                    {
                        goto Label_0136;
                    }
                    num = info.Damage;
                    if (unit3.Side != 1)
                    {
                        goto Label_010F;
                    }
                    this.mTotalDamages += num;
                Label_010F:
                    if (unit3.Side != null)
                    {
                        goto Label_0136;
                    }
                    if (unit3.IsPartyMember == null)
                    {
                        goto Label_0136;
                    }
                    this.mTotalDamagesTaken += num;
                Label_0136:
                    if (unit3.IsDead == null)
                    {
                        goto Label_0193;
                    }
                    if (this.CheckGuts(unit3) == null)
                    {
                        goto Label_0170;
                    }
                    unit3.Heal(1);
                    unit3.UpdateBuffEffectTurnCount(5, unit3);
                    unit3.UpdateCondEffectTurnCount(5, unit3);
                    goto Label_018E;
                Label_0170:
                    this.Dead(null, unit3, 0, 0);
                    this.TrySetBattleFinisher(trick.TrickData.CreateUnit);
                Label_018E:
                    goto Label_01AE;
                Label_0193:
                    if (info.KnockBackGrid != null)
                    {
                        goto Label_01A4;
                    }
                    goto Label_01AE;
                Label_01A4:
                    list.Add(unit3);
                    flag = 1;
                Label_01AE:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_00CB;
                    }
                    goto Label_01CC;
                }
                finally
                {
                Label_01BF:
                    ((List<LogMapTrick.TargetInfo>.Enumerator) enumerator3).Dispose();
                }
            Label_01CC:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0089;
                }
                goto Label_01EA;
            }
            finally
            {
            Label_01DD:
                ((List<Unit>.Enumerator) enumerator2).Dispose();
            }
        Label_01EA:
            if (list.Count != null)
            {
                goto Label_0034;
            }
            if (flag == null)
            {
                goto Label_0210;
            }
            if (is_update_buff == null)
            {
                goto Label_0210;
            }
            self.RefleshMomentBuff(this.Units, 0, -1, -1);
        Label_0210:
            return;
        }

        public unsafe void TrickCreateForSkill(Unit self, int gx, int gy, SkillData skill)
        {
            string str;
            TrickParam param;
            GridMap<bool> map;
            int num;
            int num2;
            int num3;
            int num4;
            if (self == null)
            {
                goto Label_000D;
            }
            if (skill != null)
            {
                goto Label_000E;
            }
        Label_000D:
            return;
        Label_000E:
            str = skill.SkillParam.TrickId;
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0027;
            }
            return;
        Label_0027:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetTrickParam(str);
            if (param != null)
            {
                goto Label_003F;
            }
            return;
        Label_003F:
            map = new GridMap<bool>(this.CurrentMap.Width, this.CurrentMap.Height);
            if (skill.IsAreaSkill() == null)
            {
                goto Label_00F5;
            }
            this.CreateScopeGridMap(self, self.x, self.y, gx, gy, skill, &map, 0);
            if (skill.SkillParam.TrickSetType != null)
            {
                goto Label_0125;
            }
            num = 0;
            goto Label_00E4;
        Label_009A:
            num2 = 0;
            goto Label_00D3;
        Label_00A2:
            if (map.get(num, num2) != null)
            {
                goto Label_00B5;
            }
            goto Label_00CD;
        Label_00B5:
            if (this.IsTrickExistUnit(num, num2) == null)
            {
                goto Label_00CD;
            }
            map.set(num, num2, 0);
        Label_00CD:
            num2 += 1;
        Label_00D3:
            if (num2 < map.h)
            {
                goto Label_00A2;
            }
            num += 1;
        Label_00E4:
            if (num < map.w)
            {
                goto Label_009A;
            }
            goto Label_0125;
        Label_00F5:
            map.set(gx, gy, 1);
            if (skill.SkillParam.TrickSetType != null)
            {
                goto Label_0125;
            }
            if (this.IsTrickExistUnit(gx, gy) == null)
            {
                goto Label_0125;
            }
            map.set(gx, gy, 0);
        Label_0125:
            num3 = 0;
            goto Label_018D;
        Label_012D:
            num4 = 0;
            goto Label_017A;
        Label_0135:
            if (map.get(num3, num4) != null)
            {
                goto Label_0149;
            }
            goto Label_0174;
        Label_0149:
            TrickData.EntryEffect(param.Iname, num3, num4, null, self, this.mClockTimeTotal, skill.Rank, skill.GetRankCap());
        Label_0174:
            num4 += 1;
        Label_017A:
            if (num4 < map.h)
            {
                goto Label_0135;
            }
            num3 += 1;
        Label_018D:
            if (num3 < map.w)
            {
                goto Label_012D;
            }
            return;
        }

        private bool TrySetBattleFinisher(Unit _unit)
        {
            if (_unit != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (this.CheckJudgeBattle() != null)
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            if (_unit.Side == null)
            {
                goto Label_0022;
            }
            return 0;
        Label_0022:
            this.mFinisherIname = _unit.UnitParam.iname;
            return 1;
        }

        public void UnitChange(Unit self, int gx, int gy, EUnitDirection dir, Unit target)
        {
            int num;
            float num2;
            LogUnitEntry entry;
            int num3;
            int num4;
            Unit unit;
            int num5;
            this.DebugAssert(this.IsMapCommand, "マップコマンド中のみコール可");
            if ((self != null) && (target != null))
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            if ((self.IsDead == null) && (target.IsSub != null))
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            self.Direction = dir;
            num = self.ChargeTime;
            self.SetUnitFlag(0x2000, 1);
            self.SetUnitFlag(0x80000, 1);
            self.UnitChangedHp = self.CurrentStatus.param.hp;
            self.ForceDead();
            target.x = self.x;
            target.y = self.y;
            target.Direction = self.Direction;
            target.IsSub = 0;
            num2 = (self.ChargeTimeMax == null) ? 100f : ((((float) num) * 100f) / ((float) self.ChargeTimeMax));
            target.ChargeTime = Mathf.CeilToInt((((float) target.ChargeTimeMax) * num2) / 100f) + 1;
            entry = this.Log<LogUnitEntry>();
            entry.self = target;
            entry.kill_unit = self;
            this.BeginBattlePassiveSkill(target);
            target.UpdateBuffEffects();
            target.CalcCurrentStatus(0, 0);
            target.CurrentStatus.param.hp = target.MaximumStatus.param.hp;
            if (this.IsTower == null)
            {
                goto Label_01B1;
            }
            num3 = MonoSingleton<GameManager>.Instance.TowerResuponse.GetUnitDamage(target.UnitData);
            target.CurrentStatus.param.hp = Math.Max(target.CurrentStatus.param.hp - num3, 1);
        Label_01B1:
            target.CurrentStatus.param.mp = target.GetStartGems();
            num4 = 0;
            goto Label_02BC;
        Label_01D6:
            unit = this.Player[num4];
            if (unit.IsSub == null)
            {
                goto Label_02B6;
            }
            if (unit.IsDead != null)
            {
                goto Label_02B6;
            }
            if (unit != this.Friend)
            {
                goto Label_020F;
            }
            goto Label_02B6;
        Label_020F:
            unit.UpdateBuffEffects();
            unit.CalcCurrentStatus(0, 0);
            unit.CurrentStatus.param.hp = unit.MaximumStatus.param.hp;
            if (this.IsTower == null)
            {
                goto Label_0299;
            }
            num5 = MonoSingleton<GameManager>.Instance.TowerResuponse.GetUnitDamage(unit.UnitData);
            unit.CurrentStatus.param.hp = Math.Max(unit.CurrentStatus.param.hp - num5, 1);
        Label_0299:
            unit.CurrentStatus.param.mp = unit.GetStartGems();
        Label_02B6:
            num4 += 1;
        Label_02BC:
            if (num4 < this.Player.Count)
            {
                goto Label_01D6;
            }
            this.UseAutoSkills(target);
            return;
        }

        public EUnitDirection UnitDirectionFromGrid(Grid self, Grid target)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (self == null)
            {
                goto Label_008C;
            }
            if (target == null)
            {
                goto Label_008C;
            }
            num = target.x - self.x;
            num2 = target.y - self.y;
            num3 = Math.Abs(num);
            num4 = Math.Abs(num2);
            if (num3 <= num4)
            {
                goto Label_004F;
            }
            if (num >= 0)
            {
                goto Label_0046;
            }
            return 2;
        Label_0046:
            if (num <= 0)
            {
                goto Label_004F;
            }
            return 0;
        Label_004F:
            if (num3 >= num4)
            {
                goto Label_0068;
            }
            if (num2 >= 0)
            {
                goto Label_005F;
            }
            return 3;
        Label_005F:
            if (num2 <= 0)
            {
                goto Label_0068;
            }
            return 1;
        Label_0068:
            if (num <= 0)
            {
                goto Label_0071;
            }
            return 0;
        Label_0071:
            if (num >= 0)
            {
                goto Label_007A;
            }
            return 2;
        Label_007A:
            if (num2 <= 0)
            {
                goto Label_0083;
            }
            return 1;
        Label_0083:
            if (num2 >= 0)
            {
                goto Label_008C;
            }
            return 3;
        Label_008C:
            return 1;
        }

        public EUnitDirection UnitDirectionFromGridLaserTwin(Grid self, Grid target)
        {
            int num;
            int num2;
            int num3;
            int num4;
            num = target.x - self.x;
            num2 = target.y - self.y;
            num3 = Math.Abs(num);
            num4 = Math.Abs(num2);
            if (num3 <= num4)
            {
                goto Label_0043;
            }
            if (num >= 0)
            {
                goto Label_003A;
            }
            return 2;
        Label_003A:
            if (num <= 0)
            {
                goto Label_0043;
            }
            return 0;
        Label_0043:
            if (num3 >= num4)
            {
                goto Label_005C;
            }
            if (num2 >= 0)
            {
                goto Label_0053;
            }
            return 3;
        Label_0053:
            if (num2 <= 0)
            {
                goto Label_005C;
            }
            return 1;
        Label_005C:
            if (num <= 0)
            {
                goto Label_006E;
            }
            if (num2 >= 0)
            {
                goto Label_006C;
            }
            return 3;
        Label_006C:
            return 0;
        Label_006E:
            if (num >= 0)
            {
                goto Label_0080;
            }
            if (num2 <= 0)
            {
                goto Label_007E;
            }
            return 1;
        Label_007E:
            return 2;
        Label_0080:
            if (num2 <= 0)
            {
                goto Label_0089;
            }
            return 1;
        Label_0089:
            if (num2 >= 0)
            {
                goto Label_0092;
            }
            return 3;
        Label_0092:
            return 1;
        }

        private EUnitDirection unitDirectionFromPos(int src_gx, int src_gy, int dst_gx, int dst_gy)
        {
            return this.UnitDirectionFromGrid(this.GetUnitGridPosition(src_gx, src_gy), this.GetUnitGridPosition(dst_gx, dst_gy));
        }

        public static EUnitDirection UnitDirectionFromVector(Unit self, int x, int y)
        {
            int num;
            int num2;
            num = Math.Abs(x);
            num2 = Math.Abs(y);
            if (num <= num2)
            {
                goto Label_0027;
            }
            if (x >= 0)
            {
                goto Label_001E;
            }
            return 2;
        Label_001E:
            if (x <= 0)
            {
                goto Label_0027;
            }
            return 0;
        Label_0027:
            if (num >= num2)
            {
                goto Label_0040;
            }
            if (y >= 0)
            {
                goto Label_0037;
            }
            return 3;
        Label_0037:
            if (y <= 0)
            {
                goto Label_0040;
            }
            return 1;
        Label_0040:
            if (x <= 0)
            {
                goto Label_0049;
            }
            return 0;
        Label_0049:
            if (x >= 0)
            {
                goto Label_0052;
            }
            return 2;
        Label_0052:
            if (y <= 0)
            {
                goto Label_005B;
            }
            return 1;
        Label_005B:
            if (y >= 0)
            {
                goto Label_0064;
            }
            return 3;
        Label_0064:
            return ((self == null) ? 1 : self.Direction);
        }

        public unsafe void UnitEnd()
        {
            OrderData data;
            Unit unit;
            int num;
            Unit unit2;
            GridMap<bool> map;
            int num2;
            LogDamage damage;
            int num3;
            <UnitEnd>c__AnonStorey1BA storeyba;
            this.DebugAssert(this.IsInitialized, "初期化済みのみコール可");
            this.DebugAssert(this.IsBattleFlag(1), "マップ開始済みのみコール可");
            this.DebugAssert(this.IsBattleFlag(2), "ユニット開始済みのみコール可");
            data = this.CurrentOrderData;
            this.DebugAssert((data == null) == 0, "order == null");
            unit = this.CurrentUnit;
            this.DebugAssert((unit == null) == 0, "self == null");
            this.SetBattleFlag(2, 0);
            this.SetBattleFlag(3, 0);
            unit.NotifyActionEnd();
            num = 0;
            goto Label_017F;
        Label_0084:
            storeyba = new <UnitEnd>c__AnonStorey1BA();
            storeyba.unit = this.Units[num];
            if (storeyba.unit.CastSkill == null)
            {
                goto Label_017B;
            }
            if (storeyba.unit.UnitTarget == null)
            {
                goto Label_017B;
            }
            if (storeyba.unit.IsDeadCondition() != null)
            {
                goto Label_017B;
            }
            if (unit.CastSkillGridMap != null)
            {
                goto Label_00E1;
            }
            goto Label_017B;
        Label_00E1:
            unit2 = this.Units.Find(new Predicate<Unit>(storeyba.<>m__61));
            if (unit2 != null)
            {
                goto Label_0105;
            }
            goto Label_017B;
        Label_0105:
            map = unit.CastSkillGridMap;
            map.fill(0);
            if (storeyba.unit.CastSkill.IsAreaSkill() == null)
            {
                goto Label_015F;
            }
            this.CreateScopeGridMap(unit, unit.x, unit.y, unit2.x, unit2.y, storeyba.unit.CastSkill, &map, 0);
            goto Label_0173;
        Label_015F:
            map.set(unit2.x, unit2.y, 1);
        Label_0173:
            unit.CastSkillGridMap = map;
        Label_017B:
            num += 1;
        Label_017F:
            if (num < this.Units.Count)
            {
                goto Label_0084;
            }
            if (unit.IsDeadCondition() != null)
            {
                goto Label_02BF;
            }
            if (unit.IsUnitCondition(1L) == null)
            {
                goto Label_02BF;
            }
            num2 = unit.GetPoisonDamage();
            unit.Damage(num2, 1);
            if (unit.Side != 1)
            {
                goto Label_01DC;
            }
            if (num2 <= 0)
            {
                goto Label_01DC;
            }
            this.mTotalDamages += num2;
        Label_01DC:
            if (unit.Side != null)
            {
                goto Label_0209;
            }
            if (unit.IsPartyMember == null)
            {
                goto Label_0209;
            }
            if (num2 <= 0)
            {
                goto Label_0209;
            }
            this.mTotalDamagesTaken += num2;
        Label_0209:
            damage = this.Log<LogDamage>();
            damage.self = unit;
            damage.damage = num2;
            if (unit.IsDead == null)
            {
                goto Label_02BF;
            }
            if (this.CheckGuts(unit) == null)
            {
                goto Label_0255;
            }
            unit.Heal(1);
            unit.UpdateBuffEffectTurnCount(5, unit);
            unit.UpdateCondEffectTurnCount(5, unit);
            goto Label_02BF;
        Label_0255:
            num3 = 0;
            goto Label_02A3;
        Label_025D:
            if (unit.CondAttachments[num3].ContainsCondition(1L) != null)
            {
                goto Label_027B;
            }
            goto Label_029D;
        Label_027B:
            if (this.TrySetBattleFinisher(unit.CondAttachments[num3].user) == null)
            {
                goto Label_029D;
            }
            goto Label_02B5;
        Label_029D:
            num3 += 1;
        Label_02A3:
            if (num3 < unit.CondAttachments.Count)
            {
                goto Label_025D;
            }
        Label_02B5:
            this.Dead(unit, unit, 1, 0);
        Label_02BF:
            if (this.mQuestParam.type != 2)
            {
                goto Label_02E3;
            }
            if (unit.Side != null)
            {
                goto Label_02E3;
            }
            this.ArenaSubActionCount(1);
        Label_02E3:
            unit.NotifyActionEndAfter(this.Units);
            this.UpdateBattlePassiveSkill();
            this.NextOrder(0, 1, 0, 1);
            return;
        }

        public void UnitStart()
        {
            IEnumerator enumerator;
            enumerator = this.UnitStartAsync();
        Label_000C:
            if (enumerator.MoveNext() != null)
            {
                goto Label_000C;
            }
            return;
        }

        [DebuggerHidden]
        public IEnumerator UnitStartAsync()
        {
            <UnitStartAsync>c__Iterator2A iteratora;
            iteratora = new <UnitStartAsync>c__Iterator2A();
            iteratora.<>f__this = this;
            return iteratora;
        }

        public void UnitWithdraw(Unit self)
        {
            bool flag;
            int num;
            EUnitCondition condition;
            LogUnitWithdraw withdraw;
            if (self == null)
            {
                goto Label_0011;
            }
            if (self.IsDead == null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            flag = 0;
            num = 0;
            goto Label_0043;
        Label_001B:
            condition = 1L << (num & 0x3f);
            if (self.IsUnitCondition(condition) != null)
            {
                goto Label_0034;
            }
            goto Label_003F;
        Label_0034:
            self.CureCondEffects(condition, 0, 1);
            flag = 1;
        Label_003F:
            num += 1;
        Label_0043:
            if (num < Unit.MAX_UNIT_CONDITION)
            {
                goto Label_001B;
            }
            if (flag == null)
            {
                goto Label_005F;
            }
            self.UpdateCondEffects();
        Label_005F:
            self.SetUnitFlag(0x2000, 1);
            self.SetUnitFlag(0x100000, 1);
            self.ForceDead();
            this.UpdateEntryTriggers(7, self, null);
            this.Log<LogUnitWithdraw>().self = self;
            return;
        }

        private void UpdateAIActionUseCondition(Unit self)
        {
            int num;
            int num2;
            int num3;
            Unit unit;
            AIAction action;
            num = 0;
            num2 = 0;
            num3 = 0;
            goto Label_0060;
        Label_000B:
            unit = this.Units[num3];
            if (unit.IsGimmick != null)
            {
                goto Label_005C;
            }
            if (unit.CheckExistMap() != null)
            {
                goto Label_0033;
            }
            goto Label_005C;
        Label_0033:
            if (unit.Side != null)
            {
                goto Label_0047;
            }
            num += 1;
            goto Label_005C;
        Label_0047:
            if (unit.Side != 1)
            {
                goto Label_005C;
            }
            num2 += 1;
        Label_005C:
            num3 += 1;
        Label_0060:
            if (num3 < this.Units.Count)
            {
                goto Label_000B;
            }
            if (self.IsGimmick != null)
            {
                goto Label_00A6;
            }
            if (self.CheckExistMap() == null)
            {
                goto Label_00A6;
            }
            action = self.GetCurrentAIAction();
            if (action == null)
            {
                goto Label_00A6;
            }
            this.UpdateSkillUseCondition(self, action.cond, num, num2);
        Label_00A6:
            return;
        }

        private void UpdateBattlePassiveSkill()
        {
            int num;
            Unit unit;
            EquipData[] dataArray;
            int num2;
            EquipData data;
            SkillData data2;
            int num3;
            SkillData data3;
            int num4;
            int num5;
            num = 0;
            goto Label_013E;
        Label_0007:
            unit = this.mAllUnits[num];
            if (unit == null)
            {
                goto Label_013A;
            }
            if (unit.IsDead == null)
            {
                goto Label_002A;
            }
            goto Label_013A;
        Label_002A:
            if (unit.IsUnitFlag(0x1000000) == null)
            {
                goto Label_003F;
            }
            goto Label_013A;
        Label_003F:
            dataArray = unit.CurrentEquips;
            if (dataArray == null)
            {
                goto Label_00C9;
            }
            num2 = 0;
            goto Label_00C0;
        Label_0053:
            data = dataArray[num2];
            if (data == null)
            {
                goto Label_00BC;
            }
            if (data.IsValid() == null)
            {
                goto Label_00BC;
            }
            if (data.IsEquiped() != null)
            {
                goto Label_007C;
            }
            goto Label_00BC;
        Label_007C:
            data2 = data.Skill;
            if (data2 == null)
            {
                goto Label_00BC;
            }
            if (data2.Target == null)
            {
                goto Label_00BC;
            }
            if (data2.IsPassiveSkill() == null)
            {
                goto Label_00BC;
            }
            if (data2.Timing != 1)
            {
                goto Label_00BC;
            }
            this.InternalBattlePassiveSkill(unit, data2, 0, null);
        Label_00BC:
            num2 += 1;
        Label_00C0:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_0053;
            }
        Label_00C9:
            num3 = 0;
            goto Label_0128;
        Label_00D1:
            data3 = unit.BattleSkills[num3];
            if (data3 == null)
            {
                goto Label_0122;
            }
            if (data3.Target == null)
            {
                goto Label_0122;
            }
            if (data3.IsPassiveSkill() == null)
            {
                goto Label_0122;
            }
            if (data3.Timing != 1)
            {
                goto Label_0122;
            }
            this.InternalBattlePassiveSkill(unit, unit.BattleSkills[num3], 0, null);
        Label_0122:
            num3 += 1;
        Label_0128:
            if (num3 < unit.BattleSkills.Count)
            {
                goto Label_00D1;
            }
        Label_013A:
            num += 1;
        Label_013E:
            if (num < this.mAllUnits.Count)
            {
                goto Label_0007;
            }
            num4 = 0;
            goto Label_0171;
        Label_0157:
            this.Player[num4].CalcCurrentStatus(0, 0);
            num4 += 1;
        Label_0171:
            if (num4 < this.Player.Count)
            {
                goto Label_0157;
            }
            num5 = 0;
            goto Label_01A5;
        Label_018B:
            this.Enemys[num5].CalcCurrentStatus(0, 0);
            num5 += 1;
        Label_01A5:
            if (num5 < this.Enemys.Count)
            {
                goto Label_018B;
            }
            return;
        }

        private void UpdateCancelCastSkill()
        {
            int num;
            Unit unit;
            int num2;
            num = 0;
            goto Label_0042;
        Label_0007:
            unit = this.mUnits[num];
            if (unit.CastSkill == null)
            {
                goto Label_003E;
            }
            if (unit.GetSkillUsedCost(unit.CastSkill) <= unit.Gems)
            {
                goto Label_003E;
            }
            unit.CancelCastSkill();
        Label_003E:
            num += 1;
        Label_0042:
            if (num < this.mUnits.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private void UpdateEntryTriggers(UnitEntryTypes type, Unit target, SkillParam skill)
        {
            int num;
            int num2;
            int num3;
            int num4;
            Unit unit;
            int num5;
            Unit unit2;
            int num6;
            UnitEntryTrigger trigger;
            UnitEntryTypes types;
            num = type;
            num2 = 0;
            num3 = 0;
            if (type != 1)
            {
                goto Label_007F;
            }
            num4 = 0;
            goto Label_006E;
        Label_0014:
            unit = this.mUnits[num4];
            if (unit.IsGimmick == null)
            {
                goto Label_0033;
            }
            goto Label_006A;
        Label_0033:
            if (unit.IsDeadCondition() == null)
            {
                goto Label_006A;
            }
            if (unit.Side != null)
            {
                goto Label_0054;
            }
            num2 += 1;
            goto Label_006A;
        Label_0054:
            if (unit.Side != 1)
            {
                goto Label_006A;
            }
            num3 += 1;
        Label_006A:
            num4 += 1;
        Label_006E:
            if (num4 < this.mUnits.Count)
            {
                goto Label_0014;
            }
        Label_007F:
            num5 = 0;
            goto Label_03DD;
        Label_0087:
            unit2 = this.mUnits[num5];
            if (unit2.IsGimmick == null)
            {
                goto Label_00FB;
            }
            if (unit2.IsBreakObj != null)
            {
                goto Label_00FB;
            }
            if (unit2.EventTrigger == null)
            {
                goto Label_03D7;
            }
            if (unit2.EventTrigger.EventType == 3)
            {
                goto Label_00DE;
            }
            if (unit2.EventTrigger.EventType != 4)
            {
                goto Label_03D7;
            }
        Label_00DE:
            if (unit2.CheckEventTrigger(unit2.EventTrigger.Trigger) != null)
            {
                goto Label_00FB;
            }
            goto Label_03D7;
        Label_00FB:
            if (unit2.IsEntry == null)
            {
                goto Label_010C;
            }
            goto Label_03D7;
        Label_010C:
            if (unit2.IsDead == null)
            {
                goto Label_011D;
            }
            goto Label_03D7;
        Label_011D:
            if (unit2.IsSub == null)
            {
                goto Label_012E;
            }
            goto Label_03D7;
        Label_012E:
            if (unit2.EntryTriggers != null)
            {
                goto Label_013F;
            }
            goto Label_03D7;
        Label_013F:
            num6 = 0;
            goto Label_03C4;
        Label_0147:
            trigger = unit2.EntryTriggers[num6];
            if (trigger.on != null)
            {
                goto Label_03BE;
            }
            if (trigger.type == num)
            {
                goto Label_0175;
            }
            goto Label_03BE;
        Label_0175:
            types = type;
            switch ((types - 1))
            {
                case 0:
                    goto Label_01A2;

                case 1:
                    goto Label_01F2;

                case 2:
                    goto Label_023D;

                case 3:
                    goto Label_02CF;

                case 4:
                    goto Label_0316;

                case 5:
                    goto Label_0364;

                case 6:
                    goto Label_0369;
            }
            goto Label_03BE;
        Label_01A2:
            if (unit2.Side != null)
            {
                goto Label_01C7;
            }
            trigger.on = (num2 < trigger.value) == 0;
            goto Label_03BE;
        Label_01C7:
            if (unit2.Side != 1)
            {
                goto Label_03BE;
            }
            trigger.on = (num3 < trigger.value) == 0;
            goto Label_03BE;
            goto Label_03BE;
        Label_01F2:
            if (this.CheckMatchUniqueName(target, trigger.unit) != null)
            {
                goto Label_020A;
            }
            goto Label_03BE;
        Label_020A:
            if (trigger.value >= target.CurrentStatus.param.hp)
            {
                goto Label_0230;
            }
            goto Label_03BE;
        Label_0230:
            trigger.on = 1;
            goto Label_03BE;
        Label_023D:
            if (this.CheckMatchUniqueName(target, trigger.unit) != null)
            {
                goto Label_0255;
            }
            goto Label_03BE;
        Label_0255:
            if (target.IsGimmick == null)
            {
                goto Label_02B2;
            }
            if (target.IsBreakObj != null)
            {
                goto Label_02B2;
            }
            if (target.EventTrigger == null)
            {
                goto Label_03BE;
            }
            if (target.EventTrigger.EventType == 3)
            {
                goto Label_0298;
            }
            if (target.EventTrigger.EventType != 4)
            {
                goto Label_03BE;
            }
        Label_0298:
            if (target.EventTrigger.Count == null)
            {
                goto Label_02C2;
            }
            goto Label_03BE;
            goto Label_02C2;
        Label_02B2:
            if (target.IsDead != null)
            {
                goto Label_02C2;
            }
            goto Label_03BE;
        Label_02C2:
            trigger.on = 1;
            goto Label_03BE;
        Label_02CF:
            if (this.CheckMatchUniqueName(target, trigger.unit) != null)
            {
                goto Label_02E7;
            }
            goto Label_03BE;
        Label_02E7:
            if (skill == null)
            {
                goto Label_03BE;
            }
            if ((skill.iname != trigger.skill) == null)
            {
                goto Label_0309;
            }
            goto Label_03BE;
        Label_0309:
            trigger.on = 1;
            goto Label_03BE;
        Label_0316:
            if (this.CheckMatchUniqueName(target, trigger.unit) != null)
            {
                goto Label_032E;
            }
            goto Label_03BE;
        Label_032E:
            if (target.x != trigger.x)
            {
                goto Label_03BE;
            }
            if (target.y == trigger.y)
            {
                goto Label_0357;
            }
            goto Label_03BE;
        Label_0357:
            trigger.on = 1;
            goto Label_03BE;
        Label_0364:
            goto Label_03BE;
        Label_0369:
            if (this.CheckMatchUniqueName(target, trigger.unit) != null)
            {
                goto Label_0381;
            }
            goto Label_03BE;
        Label_0381:
            if (target.IsGimmick == null)
            {
                goto Label_0391;
            }
            goto Label_03BE;
        Label_0391:
            if (target.IsDead == null)
            {
                goto Label_03BE;
            }
            if (target.IsUnitFlag(0x100000) != null)
            {
                goto Label_03B1;
            }
            goto Label_03BE;
        Label_03B1:
            trigger.on = 1;
        Label_03BE:
            num6 += 1;
        Label_03C4:
            if (num6 < unit2.EntryTriggers.Count)
            {
                goto Label_0147;
            }
        Label_03D7:
            num5 += 1;
        Label_03DD:
            if (num5 < this.mUnits.Count)
            {
                goto Label_0087;
            }
            return;
        }

        private unsafe void UpdateGimmickEventStart()
        {
            bool flag;
            int num;
            GimmickEvent event2;
            int num2;
            int num3;
            int num4;
            Unit unit;
            int num5;
            SkillData data;
            LogSkill skill;
            int num6;
            int num7;
            int num8;
            Unit unit2;
            SkillData data2;
            LogSkill skill2;
            int num9;
            Unit unit3;
            eGimmickEventType type;
            this.GimmickEventOnGrid();
            flag = 1;
            goto Label_0484;
        Label_000D:
            flag = 0;
            num = 0;
            goto Label_0473;
        Label_0016:
            event2 = this.mGimmickEvents[num];
            if (this.CheckEnableGimmickEvent(event2) != null)
            {
                goto Label_0034;
            }
            goto Label_046F;
        Label_0034:
            if (event2.IsStarter == null)
            {
                goto Label_0081;
            }
            if (event2.starter == null)
            {
                goto Label_0081;
            }
            if (event2.starter.CheckExistMap() == null)
            {
                goto Label_0081;
            }
            if (event2.targets.Contains(event2.starter) != null)
            {
                goto Label_0081;
            }
            event2.targets.Add(event2.starter);
        Label_0081:
            num2 = 0;
            num3 = 0;
            goto Label_00AC;
        Label_008B:
            if (event2.targets[num3].CheckExistMap() == null)
            {
                goto Label_00A6;
            }
            num2 += 1;
        Label_00A6:
            num3 += 1;
        Label_00AC:
            if (num3 < event2.targets.Count)
            {
                goto Label_008B;
            }
            if (num2 != null)
            {
                goto Label_00C9;
            }
            goto Label_046F;
        Label_00C9:
            type = event2.ev_type;
            if (type == null)
            {
                goto Label_00E5;
            }
            if (type == 1)
            {
                goto Label_03E7;
            }
            goto Label_0468;
        Label_00E5:
            if (event2.users.Count <= 0)
            {
                goto Label_02B8;
            }
            num4 = 0;
            goto Label_02A1;
        Label_00FE:
            unit = event2.users[num4];
            if (unit.CheckExistMap() != null)
            {
                goto Label_011E;
            }
            goto Label_029B;
        Label_011E:
            num5 = 0;
            goto Label_0289;
        Label_0126:
            data = unit.GetSkillData(event2.skills[num5]);
            if (data != null)
            {
                goto Label_0161;
            }
            data = new SkillData();
            data.Setup(event2.skills[num5], 1, 1, null);
        Label_0161:
            skill = this.Log<LogSkill>();
            skill.self = unit;
            skill.skill = data;
            if (event2.targets.Count != 1)
            {
                goto Label_01D7;
            }
            if (data.IsAllEffect() != null)
            {
                goto Label_01D7;
            }
            &skill.pos.x = event2.targets[0].x;
            &skill.pos.y = event2.targets[0].y;
            goto Label_0207;
        Label_01D7:
            &skill.pos.x = skill.self.x;
            &skill.pos.y = skill.self.y;
        Label_0207:
            skill.is_append = data.IsCutin() == 0;
            skill.is_gimmick = 1;
            num6 = 0;
            goto Label_0266;
        Label_0228:
            if (event2.targets[num6].CheckExistMap() != null)
            {
                goto Label_0244;
            }
            goto Label_0260;
        Label_0244:
            skill.SetSkillTarget(skill.self, event2.targets[num6]);
        Label_0260:
            num6 += 1;
        Label_0266:
            if (num6 < event2.targets.Count)
            {
                goto Label_0228;
            }
            this.ExecuteSkill(0, skill, data);
            num5 += 1;
        Label_0289:
            if (num5 < event2.skills.Count)
            {
                goto Label_0126;
            }
        Label_029B:
            num4 += 1;
        Label_02A1:
            if (num4 < event2.users.Count)
            {
                goto Label_00FE;
            }
            goto Label_03E2;
        Label_02B8:
            num7 = 0;
            goto Label_03D0;
        Label_02C0:
            if (event2.targets[num7].CheckExistMap() != null)
            {
                goto Label_02DC;
            }
            goto Label_03CA;
        Label_02DC:
            num8 = 0;
            goto Label_03B8;
        Label_02E4:
            unit2 = event2.targets[num7];
            data2 = unit2.GetSkillData(event2.skills[num8]);
            if (data2 != null)
            {
                goto Label_032E;
            }
            data2 = new SkillData();
            data2.Setup(event2.skills[num8], 1, 1, null);
        Label_032E:
            skill2 = this.Log<LogSkill>();
            skill2.self = unit2;
            skill2.skill = data2;
            &skill2.pos.x = skill2.self.x;
            &skill2.pos.y = skill2.self.y;
            skill2.is_append = data2.IsCutin() == 0;
            skill2.is_gimmick = 1;
            skill2.SetSkillTarget(skill2.self, skill2.self);
            this.ExecuteSkill(0, skill2, data2);
            num8 += 1;
        Label_03B8:
            if (num8 < event2.skills.Count)
            {
                goto Label_02E4;
            }
        Label_03CA:
            num7 += 1;
        Label_03D0:
            if (num7 < event2.targets.Count)
            {
                goto Label_02C0;
            }
        Label_03E2:
            goto Label_0468;
        Label_03E7:
            num9 = 0;
            goto Label_0451;
        Label_03EF:
            unit3 = event2.targets[num9];
            if (unit3.CheckExistMap() != null)
            {
                goto Label_040F;
            }
            goto Label_044B;
        Label_040F:
            unit3.KeepHp = unit3.CurrentStatus.param.hp;
            unit3.CurrentStatus.param.hp = 0;
            this.Dead(null, unit3, 0, 0);
            flag = 1;
        Label_044B:
            num9 += 1;
        Label_0451:
            if (num9 < event2.targets.Count)
            {
                goto Label_03EF;
            }
        Label_0468:
            event2.IsCompleted = 1;
        Label_046F:
            num += 1;
        Label_0473:
            if (num < this.mGimmickEvents.Count)
            {
                goto Label_0016;
            }
        Label_0484:
            if (flag != null)
            {
                goto Label_000D;
            }
            return;
        }

        private unsafe void UpdateGimmickEventTrick()
        {
            bool flag;
            GimmickEvent event2;
            List<GimmickEvent>.Enumerator enumerator;
            TrickData data;
            List<TrickData>.Enumerator enumerator2;
        Label_0000:
            flag = 0;
            enumerator = this.mGimmickEvents.GetEnumerator();
        Label_000E:
            try
            {
                goto Label_009A;
            Label_0013:
                event2 = &enumerator.Current;
                if (this.CheckEnableGimmickEvent(event2) != null)
                {
                    goto Label_002C;
                }
                goto Label_009A;
            Label_002C:
                if (event2.td_targets.Count != null)
                {
                    goto Label_0041;
                }
                goto Label_009A;
            Label_0041:
                if (event2.ev_type == 1)
                {
                    goto Label_0052;
                }
                goto Label_009A;
            Label_0052:
                enumerator2 = event2.td_targets.GetEnumerator();
            Label_005F:
                try
                {
                    goto Label_0073;
                Label_0064:
                    data = &enumerator2.Current;
                    TrickData.RemoveEffect(data);
                Label_0073:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0064;
                    }
                    goto Label_0091;
                }
                finally
                {
                Label_0084:
                    ((List<TrickData>.Enumerator) enumerator2).Dispose();
                }
            Label_0091:
                event2.IsCompleted = 1;
                flag = 1;
            Label_009A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0013;
                }
                goto Label_00B7;
            }
            finally
            {
            Label_00AB:
                ((List<GimmickEvent>.Enumerator) enumerator).Dispose();
            }
        Label_00B7:
            if (flag != null)
            {
                goto Label_0000;
            }
            return;
        }

        private void UpdateHelperUnits(Unit self)
        {
            BattleMap map;
            int num;
            Unit unit;
            int num2;
            int num3;
            this.DebugAssert((self == null) == 0, "self == null");
            map = this.CurrentMap;
            this.DebugAssert((map == null) == 0, "map == null");
            this.mHelperUnits.Clear();
            if (self.IsEnableAttackCondition(0) == null)
            {
                goto Label_004D;
            }
            if (self.IsEnableHelpCondition() != null)
            {
                goto Label_004E;
            }
        Label_004D:
            return;
        Label_004E:
            if (this.IsMultiVersus == null)
            {
                goto Label_005A;
            }
            return;
        Label_005A:
            num = 0;
            goto Label_00EB;
        Label_0061:
            unit = this.mUnits[num];
            if (self != unit)
            {
                goto Label_007A;
            }
            goto Label_00E7;
        Label_007A:
            if (self.Side == unit.Side)
            {
                goto Label_0090;
            }
            goto Label_00E7;
        Label_0090:
            if (unit.IsDeadCondition() != null)
            {
                goto Label_00E7;
            }
            if (unit.IsEnableHelpCondition() != null)
            {
                goto Label_00AB;
            }
            goto Label_00E7;
        Label_00AB:
            num2 = self.GetCombinationRange();
            if (this.CalcGridDistance(self, unit) <= num2)
            {
                goto Label_00C9;
            }
            goto Label_00E7;
        Label_00C9:
            if (this.CheckCombination(self, unit) != null)
            {
                goto Label_00DB;
            }
            goto Label_00E7;
        Label_00DB:
            this.mHelperUnits.Add(unit);
        Label_00E7:
            num += 1;
        Label_00EB:
            if (num < this.mUnits.Count)
            {
                goto Label_0061;
            }
            return;
        }

        public bool UpdateMapAI(bool forceAI)
        {
            bool flag;
            return this.UpdateMapAI_Impl(forceAI);
        }

        private bool UpdateMapAI_Impl(bool forceAI)
        {
            AIAction action;
            Func<List<SkillResult>, bool> func;
            AIAction action2;
            List<SkillData> list;
            <UpdateMapAI_Impl>c__AnonStorey1BC storeybc;
            <UpdateMapAI_Impl>c__AnonStorey1BD storeybd;
            eAIActionNoExecAct act;
            storeybc = new <UpdateMapAI_Impl>c__AnonStorey1BC();
            storeybc.forceAI = forceAI;
            storeybc.<>f__this = this;
            DebugUtility.Assert((this.CurrentUnit == null) == 0, "CurrentUnit == null");
            storeybc.self = this.CurrentUnit;
            if (this.IsAutoBattle == null)
            {
                goto Label_005D;
            }
            if (storeybc.self.Side != null)
            {
                goto Label_005D;
            }
            this.IsUseAutoPlayMode = 1;
        Label_005D:
            if (storeybc.self.AI == null)
            {
                goto Label_00B8;
            }
            if (storeybc.self.AI.CheckFlag(4) == null)
            {
                goto Label_0093;
            }
            storeybc.self.SetUnitFlag(2, 1);
        Label_0093:
            if (storeybc.self.AI.CheckFlag(8) == null)
            {
                goto Label_00B8;
            }
            storeybc.self.SetUnitFlag(4, 1);
        Label_00B8:
            if (storeybc.self.IsUnitFlag(0x10000) == null)
            {
                goto Label_00D8;
            }
            this.CommandWait(0);
            return 0;
        Label_00D8:
            if (storeybc.self.IsAIPatrolTable() == null)
            {
                goto Label_0104;
            }
            if (this.CalcMoveTargetAI(storeybc.self, storeybc.forceAI) == null)
            {
                goto Label_0104;
            }
            return 1;
        Label_0104:
            action = this.mSkillMap.GetAction();
            if (action == null)
            {
                goto Label_0162;
            }
            if (storeybc.self.IsEnableActionCondition() == null)
            {
                goto Label_0162;
            }
            if (string.IsNullOrEmpty(action.skill) == null)
            {
                goto Label_0162;
            }
            if (action.type != null)
            {
                goto Label_0162;
            }
            this.CommandWait(0);
            this.mSkillMap.NextAction(0);
            return 0;
        Label_0162:
            if (this.CalcSearchingAI(storeybc.self) != null)
            {
                goto Label_0176;
            }
            return 0;
        Label_0176:
            this.GetEnemyPriorities(storeybc.self, this.mEnemyPriorities, this.mGimmickPriorities);
            if (this.CheckEscapeAI(storeybc.self) == null)
            {
                goto Label_01B3;
            }
            storeybc.self.SetUnitFlag(0x80, 1);
        Label_01B3:
            if (storeybc.self.IsUnitFlag(4) != null)
            {
                goto Label_0389;
            }
            storeybd = new <UpdateMapAI_Impl>c__AnonStorey1BD();
            storeybd.<>f__ref$444 = storeybc;
            storeybd.<>f__this = this;
            storeybd.result = this.mSkillMap.GetUseSkill();
            if (storeybd.result == null)
            {
                goto Label_024E;
            }
            if (this.UseSkillAI(storeybc.self, storeybd.result, storeybc.forceAI) == null)
            {
                goto Label_022F;
            }
            this.mSkillMap.NextAction(storeybd.result);
            return 1;
        Label_022F:
            storeybc.self.SetUnitFlag(4, 1);
            this.mSkillMap.SetUseSkill(null);
            goto Label_0384;
        Label_024E:
            this.mSkillMap.isNoExecActionSkill = 0;
            func = new Func<List<SkillResult>, bool>(storeybd.<>m__64);
            if (storeybc.self.CastSkill != null)
            {
                goto Label_0395;
            }
            if (action == null)
            {
                goto Label_0348;
            }
            action2 = action;
        Label_0281:
            if (this.CalcUseActionAI(storeybc.self, action2, func) == null)
            {
                goto Label_02BE;
            }
            if (string.IsNullOrEmpty(action2.skill) == null)
            {
                goto Label_02BC;
            }
            if (action2.type != null)
            {
                goto Label_02BC;
            }
            return 0;
        Label_02BC:
            return 1;
        Label_02BE:
            if (action2.noExecAct != 3)
            {
                goto Label_0304;
            }
            if (action2.nextActIdx <= 0)
            {
                goto Label_0304;
            }
            action2 = this.mSkillMap.SetAction(action2.nextActIdx - 1);
            if (action2 == null)
            {
                goto Label_0306;
            }
            action = action2;
            this.UpdateAIActionUseCondition(storeybc.self);
            goto Label_0306;
        Label_0304:
            action2 = null;
        Label_0306:
            if (action2 != null)
            {
                goto Label_0281;
            }
            this.mSkillMap.isNoExecActionSkill = 1;
            act = action.noExecAct;
            if (act == 1)
            {
                goto Label_0335;
            }
            if (act == 2)
            {
                goto Label_0335;
            }
            goto Label_0348;
        Label_0335:
            this.RefreshUseSkillMap(storeybc.self, 1);
        Label_0348:
            list = this.mSkillMap.GetSkillList();
            if (list == null)
            {
                goto Label_0395;
            }
            if (this.CalcUseSkillsAI(storeybc.self, list, func) == null)
            {
                goto Label_0370;
            }
            return 1;
        Label_0370:
            this.mSkillMap.NextSkillList();
            this.Log<LogMapCommand>();
            return 1;
        Label_0384:
            goto Label_0395;
        Label_0389:
            this.mSkillMap.SetUseSkill(null);
        Label_0395:
            if (this.CalcMoveTargetAI(storeybc.self, storeybc.forceAI) == null)
            {
                goto Label_03D2;
            }
            this.mSkillMap.useSkillNum = 0;
            this.mSkillMap.NextAction(1);
            return 1;
            goto Label_03D2;
        Label_03D2:
            this.CommandWait(0);
            return 0;
        }

        private void UpdateMoveMap(Unit self)
        {
            this.UpdateMoveMap(self, this.mMoveMap);
            return;
        }

        private void UpdateMoveMap(Unit self, GridMap<int> movmap)
        {
            int num;
            num = (self.IsUnitFlag(2) != null) ? 0 : self.GetMoveCount(0);
            this.UpdateMoveMap(self, movmap, num);
            return;
        }

        private void UpdateMoveMap(Unit self, GridMap<int> movmap, int movcount)
        {
            BattleMap map;
            Grid grid;
            int num;
            int num2;
            int num3;
            Grid grid2;
            int num4;
            movmap.fill(-1);
            map = this.CurrentMap;
            grid = map[self.x, self.y];
            map.CalcMoveSteps(self, grid, 0);
            num = movcount;
            num2 = -num;
            goto Label_00D8;
        Label_0035:
            num3 = -num;
            goto Label_00CC;
        Label_003E:
            if ((Math.Abs(num3) + Math.Abs(num2)) <= num)
            {
                goto Label_0057;
            }
            goto Label_00C6;
        Label_0057:
            grid2 = map[self.x + num3, self.y + num2];
            if (grid2 == null)
            {
                goto Label_00C6;
            }
            if (grid2.step != 0xff)
            {
                goto Label_008D;
            }
            goto Label_00C6;
        Label_008D:
            num4 = Math.Max(grid2.step - grid.step, 0);
            if (num4 <= num)
            {
                goto Label_00B0;
            }
            goto Label_00C6;
        Label_00B0:
            movmap.set(grid2.x, grid2.y, num4);
        Label_00C6:
            num3 += 1;
        Label_00CC:
            if (num3 <= num)
            {
                goto Label_003E;
            }
            num2 += 1;
        Label_00D8:
            if (num2 <= num)
            {
                goto Label_0035;
            }
            return;
        }

        private unsafe void UpdateSafeMap(Unit self)
        {
            BattleMap map;
            int num;
            int num2;
            Unit unit;
            Grid grid;
            Grid grid2;
            int num3;
            int num4;
            int num5;
            int num6;
            Grid grid3;
            int num7;
            int num8;
            Unit unit2;
            Grid grid4;
            int num9;
            int num10;
            Grid grid5;
            SkillData data;
            GridMap<bool> map2;
            int num11;
            int num12;
            List<IntVector2> list;
            int num13;
            int num14;
            int num15;
            Unit unit3;
            Grid grid6;
            int num16;
            IntVector2 vector;
            Grid grid7;
            map = this.CurrentMap;
            num = (self.IsUnitFlag(2) != null) ? 0 : self.GetMoveCount(0);
            this.mSafeMap.fill(-1);
            num2 = 0;
            goto Label_013B;
        Label_0034:
            unit = this.mUnits[num2];
            if (unit.IsGimmick != null)
            {
                goto Label_0137;
            }
            if (unit.IsDeadCondition() != null)
            {
                goto Label_0137;
            }
            if (unit.IsSub != null)
            {
                goto Label_0137;
            }
            if (unit.IsEntry != null)
            {
                goto Label_0072;
            }
            goto Label_0137;
        Label_0072:
            if (this.CheckEnemySide(self, unit) != null)
            {
                goto Label_0084;
            }
            goto Label_0137;
        Label_0084:
            this.FindNearGridAndDistance(self, unit, &grid, &grid2);
            if (map.CalcMoveSteps(self, grid2, 0) == null)
            {
                goto Label_0137;
            }
            num3 = -num;
            goto Label_012F;
        Label_00A9:
            num4 = -num;
            goto Label_0121;
        Label_00B2:
            num5 = self.x + num4;
            num6 = self.y + num3;
            grid3 = map[num5, num6];
            if (grid3 == null)
            {
                goto Label_011B;
            }
            if (grid3.step != 0xff)
            {
                goto Label_00F1;
            }
            goto Label_011B;
        Label_00F1:
            num7 = this.mSafeMap.get(num5, num6) + grid3.step;
            this.mSafeMap.set(num5, num6, num7);
        Label_011B:
            num4 += 1;
        Label_0121:
            if (num4 <= num)
            {
                goto Label_00B2;
            }
            num3 += 1;
        Label_012F:
            if (num3 <= num)
            {
                goto Label_00A9;
            }
        Label_0137:
            num2 += 1;
        Label_013B:
            if (num2 < this.mUnits.Count)
            {
                goto Label_0034;
            }
            this.mSafeMapEx.fill(0xff);
            num8 = 0;
            goto Label_0312;
        Label_0164:
            unit2 = this.mUnits[num8];
            if (unit2.IsGimmick != null)
            {
                goto Label_030C;
            }
            if (unit2.IsDeadCondition() != null)
            {
                goto Label_030C;
            }
            if (unit2.IsSub != null)
            {
                goto Label_030C;
            }
            if (unit2.IsEntry != null)
            {
                goto Label_01A8;
            }
            goto Label_030C;
        Label_01A8:
            if (this.CheckEnemySide(self, unit2) != null)
            {
                goto Label_01BB;
            }
            goto Label_030C;
        Label_01BB:
            grid4 = map[unit2.x, unit2.y];
            map.CalcMoveSteps(unit2, grid4, 0);
            num9 = 0;
            goto Label_025D;
        Label_01E5:
            num10 = 0;
            goto Label_024A;
        Label_01ED:
            grid5 = map[num10, num9];
            if (grid5 == null)
            {
                goto Label_0244;
            }
            if (grid5.step != 0x7f)
            {
                goto Label_0213;
            }
            goto Label_0244;
        Label_0213:
            if (grid5.step >= this.mSafeMapEx.get(num10, num9))
            {
                goto Label_0244;
            }
            this.mSafeMapEx.set(num10, num9, grid5.step);
        Label_0244:
            num10 += 1;
        Label_024A:
            if (num10 < map.Width)
            {
                goto Label_01ED;
            }
            num9 += 1;
        Label_025D:
            if (num9 < map.Height)
            {
                goto Label_01E5;
            }
            if (unit2.CastSkill == null)
            {
                goto Label_030C;
            }
            if (unit2.UnitTarget == self)
            {
                goto Label_030C;
            }
            data = unit2.CastSkill;
            if (data.IsAllEffect() != null)
            {
                goto Label_030C;
            }
            if (data.IsHealSkill() != null)
            {
                goto Label_030C;
            }
            map2 = unit2.CastSkillGridMap;
            if (map2 == null)
            {
                goto Label_030C;
            }
            num11 = 0;
            goto Label_02FE;
        Label_02BC:
            num12 = 0;
            goto Label_02EA;
        Label_02C4:
            if (map2.get(num12, num11) == null)
            {
                goto Label_02E4;
            }
            this.mSafeMapEx.set(num12, num11, -1);
        Label_02E4:
            num12 += 1;
        Label_02EA:
            if (num12 < map2.w)
            {
                goto Label_02C4;
            }
            num11 += 1;
        Label_02FE:
            if (num11 < map2.h)
            {
                goto Label_02BC;
            }
        Label_030C:
            num8 += 1;
        Label_0312:
            if (num8 < this.mUnits.Count)
            {
                goto Label_0164;
            }
            list = new List<IntVector2>();
            num13 = 0;
            goto Label_0382;
        Label_0333:
            num14 = 0;
            goto Label_036A;
        Label_033B:
            if (this.mSafeMapEx.get(num14, num13) != 0xff)
            {
                goto Label_0364;
            }
            list.Add(new IntVector2(num14, num13));
        Label_0364:
            num14 += 1;
        Label_036A:
            if (num14 < this.mSafeMapEx.w)
            {
                goto Label_033B;
            }
            num13 += 1;
        Label_0382:
            if (num13 < this.mSafeMapEx.h)
            {
                goto Label_0333;
            }
            num15 = 0;
            goto Label_04B7;
        Label_039C:
            unit3 = this.mUnits[num15];
            if (unit3.IsGimmick != null)
            {
                goto Label_04B1;
            }
            if (unit3.IsDeadCondition() != null)
            {
                goto Label_04B1;
            }
            if (unit3.IsSub != null)
            {
                goto Label_04B1;
            }
            if (unit3.IsEntry != null)
            {
                goto Label_03E0;
            }
            goto Label_04B1;
        Label_03E0:
            if (this.CheckEnemySide(self, unit3) != null)
            {
                goto Label_03F3;
            }
            goto Label_04B1;
        Label_03F3:
            grid6 = map[unit3.x, unit3.y];
            map.CalcMoveSteps(unit3, grid6, 1);
            num16 = 0;
            goto Label_04A3;
        Label_041D:
            vector = list[num16];
            grid7 = map[&vector.x, &vector.y];
            if (grid7 == null)
            {
                goto Label_049D;
            }
            if (grid7.step != 0x7f)
            {
                goto Label_0458;
            }
            goto Label_049D;
        Label_0458:
            if (this.mSafeMapEx.get(&vector.x, &vector.y) <= grid7.step)
            {
                goto Label_049D;
            }
            this.mSafeMapEx.set(&vector.x, &vector.y, grid7.step);
        Label_049D:
            num16 += 1;
        Label_04A3:
            if (num16 < list.Count)
            {
                goto Label_041D;
            }
        Label_04B1:
            num15 += 1;
        Label_04B7:
            if (num15 < this.mUnits.Count)
            {
                goto Label_039C;
            }
            return;
        }

        private unsafe void UpdateSearchMap(Unit self)
        {
            BattleMap map;
            GridMap<bool> map2;
            int num;
            Unit unit;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            map = this.CurrentMap;
            this.mSearchMap.fill(0);
            map2 = new GridMap<bool>(map.Width, map.Height);
            map2.fill(0);
            num = 0;
            goto Label_012C;
        Label_0033:
            unit = this.mUnits[num];
            if (unit != self)
            {
                goto Label_004C;
            }
            goto Label_0128;
        Label_004C:
            if (unit.Side != self.Side)
            {
                goto Label_0062;
            }
            goto Label_0128;
        Label_0062:
            if (unit.IsDead != null)
            {
                goto Label_0128;
            }
            if (unit.IsGimmick != null)
            {
                goto Label_0128;
            }
            if (unit.IsSub != null)
            {
                goto Label_0128;
            }
            if (unit.IsEntry != null)
            {
                goto Label_0093;
            }
            goto Label_0128;
        Label_0093:
            num2 = unit.GetSearchRange() + 1;
            if (num2 != null)
            {
                goto Label_00A9;
            }
            goto Label_0128;
        Label_00A9:
            num3 = unit.x;
            num4 = unit.y;
            this.CreateSelectGridMap(unit, num3, num4, 0, num2, 1, 0, 0, 0, 0, 0x63, 0, &map2, 0);
            num5 = 0;
            goto Label_011B;
        Label_00DB:
            num6 = 0;
            goto Label_0108;
        Label_00E3:
            if (map2.get(num5, num6) == null)
            {
                goto Label_0102;
            }
            this.mSearchMap.set(num5, num6, 1);
        Label_0102:
            num6 += 1;
        Label_0108:
            if (num6 < map2.h)
            {
                goto Label_00E3;
            }
            num5 += 1;
        Label_011B:
            if (num5 < map2.w)
            {
                goto Label_00DB;
            }
        Label_0128:
            num += 1;
        Label_012C:
            if (num < this.mUnits.Count)
            {
                goto Label_0033;
            }
            return;
        }

        private unsafe void UpdateSkillMap(Unit self, List<SkillData> skills)
        {
            BattleMap map;
            IntVector2 vector;
            SRPG.SkillMap.Data data;
            SkillData data2;
            int num;
            int num2;
            List<SRPG.SkillMap.Data> list;
            bool flag;
            List<Grid> list2;
            Grid grid;
            SRPG.SkillMap.Target target;
            GridMap<bool> map2;
            SRPG.SkillMap.Score score;
            int num3;
            int num4;
            SRPG.SkillMap.Score score2;
            this.mSkillMap.Reset();
            map = this.CurrentMap;
            &vector = new IntVector2(self.x, self.y);
            data = null;
            data2 = null;
            num = 0;
            num2 = 0;
            num = 0;
            goto Label_00FA;
        Label_0037:
            data2 = null;
            num2 = 0;
            goto Label_008D;
        Label_0041:
            if ((this.mSkillMap.allSkills[num2].SkillID == skills[num].SkillID) == null)
            {
                goto Label_0087;
            }
            data2 = this.mSkillMap.allSkills[num2];
            goto Label_00A4;
        Label_0087:
            num2 += 1;
        Label_008D:
            if (num2 < this.mSkillMap.allSkills.Count)
            {
                goto Label_0041;
            }
        Label_00A4:
            if (data2 == null)
            {
                goto Label_00F4;
            }
            if ((self.Gems < self.GetSkillUsedCost(data2)) || (this.mSkillMap.attackHeight >= data2.EnableAttackGridHeight))
            {
                goto Label_00E3;
            }
            this.mSkillMap.attackHeight = data2.EnableAttackGridHeight;
        Label_00E3:
            this.mSkillMap.Add(new SRPG.SkillMap.Data(data2));
        Label_00F4:
            num += 1;
        Label_00FA:
            if (num < skills.Count)
            {
                goto Label_0037;
            }
            this.SetBattleFlag(6, 1);
            list = this.mSkillMap.list;
            flag = (self.AI == null) ? 0 : self.AI.CheckFlag(0x40);
            list2 = this.GetEnableMoveGridList(self, 1, flag, 0, 1, 0);
            num2 = 0;
            goto Label_034F;
        Label_0150:
            grid = list2[num2];
            if (map.CheckEnableMove(self, grid, 0, 0) != null)
            {
                goto Label_0170;
            }
            goto Label_0349;
        Label_0170:
            self.x = grid.x;
            self.y = grid.y;
            self.RefleshMomentBuff(0, -1, -1);
            num = 0;
            goto Label_033B;
        Label_019B:
            data = list[num];
            data2 = data.skill;
            target = new SRPG.SkillMap.Target();
            target.pos = new IntVector2(self.x, self.y);
            target.scores = new Dictionary<int, SRPG.SkillMap.Score>();
            map2 = null;
            if (data2.TeleportType == 3)
            {
                goto Label_026F;
            }
            if (data2.SkillParam.select_scope == 4)
            {
                goto Label_0207;
            }
            if (data2.SkillParam.target != null)
            {
                goto Label_0259;
            }
        Label_0207:
            if (&vector.x != self.x)
            {
                goto Label_022B;
            }
            if (&vector.y == self.y)
            {
                goto Label_0247;
            }
        Label_022B:
            if (this.mTrickMap.IsGoodData(self.x, self.y) == null)
            {
                goto Label_0335;
            }
        Label_0247:
            map2 = null;
            goto Label_0254;
            goto Label_0335;
        Label_0254:
            goto Label_026F;
        Label_0259:
            map2 = this.CreateSelectGridMapAI(self, self.x, self.y, data2);
        Label_026F:
            if (map2 != null)
            {
                goto Label_02B4;
            }
            score = new SRPG.SkillMap.Score(self.x, self.y, map.Width, map.Height);
            if (this.SetupSkillMapScore(self, grid, data2, score) == null)
            {
                goto Label_032D;
            }
            target.Add(score);
            goto Label_032D;
        Label_02B4:
            num3 = 0;
            goto Label_031F;
        Label_02BC:
            num4 = 0;
            goto Label_030B;
        Label_02C4:
            if (map2.get(num3, num4) == null)
            {
                goto Label_0305;
            }
            score2 = new SRPG.SkillMap.Score(num3, num4, map.Width, map.Height);
            if (this.SetupSkillMapScore(self, grid, data2, score2) == null)
            {
                goto Label_0305;
            }
            target.Add(score2);
        Label_0305:
            num4 += 1;
        Label_030B:
            if (num4 < map2.h)
            {
                goto Label_02C4;
            }
            num3 += 1;
        Label_031F:
            if (num3 < map2.w)
            {
                goto Label_02BC;
            }
        Label_032D:
            data.Add(target);
        Label_0335:
            num += 1;
        Label_033B:
            if (num < list.Count)
            {
                goto Label_019B;
            }
        Label_0349:
            num2 += 1;
        Label_034F:
            if (num2 < list2.Count)
            {
                goto Label_0150;
            }
            self.x = &vector.x;
            self.y = &vector.y;
            self.RefleshMomentBuff(0, -1, -1);
            this.SetBattleFlag(6, 0);
            return;
        }

        private void UpdateSkillUseCondition()
        {
            int num;
            int num2;
            int num3;
            int num4;
            Unit unit;
            int num5;
            SkillData data;
            AIAction action;
            num = 0;
            num2 = 0;
            num3 = 0;
            goto Label_007F;
        Label_000B:
            if (this.Units[num3].IsGimmick != null)
            {
                goto Label_007B;
            }
            if (this.Units[num3].CheckExistMap() != null)
            {
                goto Label_003C;
            }
            goto Label_007B;
        Label_003C:
            if (this.Units[num3].Side != null)
            {
                goto Label_005B;
            }
            num += 1;
            goto Label_007B;
        Label_005B:
            if (this.Units[num3].Side != 1)
            {
                goto Label_007B;
            }
            num2 += 1;
        Label_007B:
            num3 += 1;
        Label_007F:
            if (num3 < this.Units.Count)
            {
                goto Label_000B;
            }
            num4 = 0;
            goto Label_015F;
        Label_0097:
            unit = this.Units[num4];
            if (unit.IsGimmick == null)
            {
                goto Label_00B6;
            }
            goto Label_015B;
        Label_00B6:
            if (unit.CheckExistMap() != null)
            {
                goto Label_00C7;
            }
            goto Label_015B;
        Label_00C7:
            num5 = 0;
            goto Label_0127;
        Label_00CF:
            this.UpdateSkillUseCondition(unit, unit.BattleSkills[num5].UseCondition, num, num2);
            data = unit.GetSkillForUseCount(unit.BattleSkills[num5].SkillID, 0);
            if (data == null)
            {
                goto Label_0121;
            }
            this.UpdateSkillUseCondition(unit, data.UseCondition, num, num2);
        Label_0121:
            num5 += 1;
        Label_0127:
            if (num5 < unit.BattleSkills.Count)
            {
                goto Label_00CF;
            }
            action = unit.GetCurrentAIAction();
            if (action == null)
            {
                goto Label_015B;
            }
            this.UpdateSkillUseCondition(unit, action.cond, num, num2);
        Label_015B:
            num4 += 1;
        Label_015F:
            if (num4 < this.Units.Count)
            {
                goto Label_0097;
            }
            return;
        }

        private void UpdateSkillUseCondition(Unit unit, SkillLockCondition condition, int p_count, int e_count)
        {
            SkillLockTypes types;
            bool flag;
            int num;
            SkillLockTypes types2;
            if (condition != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            types = condition.type;
            types2 = types;
            switch ((types2 - 1))
            {
                case 0:
                    goto Label_0039;

                case 1:
                    goto Label_0039;

                case 2:
                    goto Label_0068;

                case 3:
                    goto Label_0068;

                case 4:
                    goto Label_0099;

                case 5:
                    goto Label_00C4;

                case 6:
                    goto Label_00EF;
            }
            goto Label_0164;
        Label_0039:
            condition.unlock = (types != 1) ? ((condition.value < p_count) == 0) : ((condition.value > p_count) == 0);
            goto Label_0164;
        Label_0068:
            condition.unlock = (types != 3) ? ((condition.value < e_count) == 0) : ((condition.value > e_count) == 0);
            goto Label_0164;
        Label_0099:
            condition.unlock = (unit.CurrentStatus.param.hp < condition.value) == 0;
            goto Label_0164;
        Label_00C4:
            condition.unlock = (unit.CurrentStatus.param.hp > condition.value) == 0;
            goto Label_0164;
        Label_00EF:
            if (condition.unlock != null)
            {
                goto Label_0164;
            }
            if (condition.x == null)
            {
                goto Label_0164;
            }
            flag = 0;
            num = 0;
            goto Label_0147;
        Label_010E:
            if (condition.x[num] != unit.x)
            {
                goto Label_0143;
            }
            if (condition.y[num] != unit.y)
            {
                goto Label_0143;
            }
            flag = 1;
            goto Label_0158;
        Label_0143:
            num += 1;
        Label_0147:
            if (num < condition.x.Count)
            {
                goto Label_010E;
            }
        Label_0158:
            condition.unlock = flag;
        Label_0164:
            return;
        }

        private void UpdateTrickMap(Unit self)
        {
            List<TrickData> list;
            int num;
            SRPG.TrickMap.Data data;
            this.mTrickMap.owner = self;
            this.mTrickMap.Clear();
            list = TrickData.GetEffectAll();
            num = 0;
            goto Label_0041;
        Label_0024:
            data = new SRPG.TrickMap.Data(list[num]);
            this.mTrickMap.SetData(data);
            num += 1;
        Label_0041:
            if (num < list.Count)
            {
                goto Label_0024;
            }
            return;
        }

        private void UpdateUnitCondition(Unit self)
        {
            int num;
            int num2;
            if (self != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            self.SetUnitFlag(0x200, 0);
            num = self.GetParalyseRate();
            num2 = this.GetRandom() % 100;
            if (num2 >= num)
            {
                goto Label_0037;
            }
            self.SetUnitFlag(0x200, 1);
        Label_0037:
            return;
        }

        private unsafe void UpdateUnitDyingTurn()
        {
            Unit unit;
            List<Unit>.Enumerator enumerator;
            SkillData data;
            List<SkillData>.Enumerator enumerator2;
            enumerator = this.Units.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_00D5;
            Label_0011:
                unit = &enumerator.Current;
                if (unit.IsUnitFlag(0x800000) != null)
                {
                    goto Label_002E;
                }
                goto Label_00D5;
            Label_002E:
                unit.SetUnitFlag(0x800000, 0);
                if (unit.IsDead != null)
                {
                    goto Label_00D5;
                }
                if (unit.IsDying() != null)
                {
                    goto Label_0055;
                }
                goto Label_00D5;
            Label_0055:
                enumerator2 = unit.BattleSkills.GetEnumerator();
            Label_0061:
                try
                {
                    goto Label_00B8;
                Label_0066:
                    data = &enumerator2.Current;
                    if (data.Timing != 9)
                    {
                        goto Label_00B8;
                    }
                    if (data.Condition == 1)
                    {
                        goto Label_008C;
                    }
                    goto Label_00B8;
                Label_008C:
                    if (data.IsPassiveSkill() == null)
                    {
                        goto Label_009C;
                    }
                    goto Label_00B8;
                Label_009C:
                    if (this.CheckSkillCondition(unit, data) != null)
                    {
                        goto Label_00AE;
                    }
                    goto Label_00B8;
                Label_00AE:
                    this.InvokeSkillBuffCond(unit, data, 9);
                Label_00B8:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0066;
                    }
                    goto Label_00D5;
                }
                finally
                {
                Label_00C9:
                    ((List<SkillData>.Enumerator) enumerator2).Dispose();
                }
            Label_00D5:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_00F2;
            }
            finally
            {
            Label_00E6:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_00F2:
            return;
        }

        private unsafe void UpdateUnitJudgeHPTurn()
        {
            Unit unit;
            List<Unit>.Enumerator enumerator;
            SkillData data;
            List<SkillData>.Enumerator enumerator2;
            enumerator = this.Units.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_00C9;
            Label_0011:
                unit = &enumerator.Current;
                if (unit.IsDead == null)
                {
                    goto Label_0029;
                }
                goto Label_00C9;
            Label_0029:
                enumerator2 = unit.BattleSkills.GetEnumerator();
            Label_0035:
                try
                {
                    goto Label_00AC;
                Label_003A:
                    data = &enumerator2.Current;
                    if (data.Timing != 10)
                    {
                        goto Label_00AC;
                    }
                    if (data.Condition == 5)
                    {
                        goto Label_0060;
                    }
                    goto Label_00AC;
                Label_0060:
                    if (data.IsPassiveSkill() == null)
                    {
                        goto Label_0070;
                    }
                    goto Label_00AC;
                Label_0070:
                    if (this.CheckSkillCondition(unit, data) != null)
                    {
                        goto Label_008A;
                    }
                    unit.RemoveJudgeHpLists(data);
                    goto Label_00AC;
                Label_008A:
                    if (unit.IsContainsJudgeHpLists(data) == null)
                    {
                        goto Label_009B;
                    }
                    goto Label_00AC;
                Label_009B:
                    this.InvokeSkillBuffCond(unit, data, 10);
                    unit.AddJudgeHpLists(data);
                Label_00AC:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_003A;
                    }
                    goto Label_00C9;
                }
                finally
                {
                Label_00BD:
                    ((List<SkillData>.Enumerator) enumerator2).Dispose();
                }
            Label_00C9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_00E6;
            }
            finally
            {
            Label_00DA:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_00E6:
            return;
        }

        private void UpdateUnitName()
        {
            int num;
            char ch;
            int num2;
            int num3;
            List<Unit> list;
            int num4;
            char ch2;
            int num5;
            if (this.mPlayer == null)
            {
                goto Label_00D5;
            }
            num = 0;
            goto Label_00C4;
        Label_0012:
            ch = 0x41;
            num2 = 0;
            goto Label_007D;
        Label_001C:
            if (this.mPlayer[num] != this.mPlayer[num2])
            {
                goto Label_003E;
            }
            goto Label_008E;
        Label_003E:
            if ((this.mPlayer[num].UnitParam.iname == this.mPlayer[num2].UnitParam.iname) == null)
            {
                goto Label_0079;
            }
            ch = (ushort) (ch + 1);
        Label_0079:
            num2 += 1;
        Label_007D:
            if (num2 < this.mPlayer.Count)
            {
                goto Label_001C;
            }
        Label_008E:
            this.mPlayer[num].UnitName = this.mPlayer[num].UnitParam.name + ((char) ch);
            num += 1;
        Label_00C4:
            if (num < this.mPlayer.Count)
            {
                goto Label_0012;
            }
        Label_00D5:
            if (this.mEnemys == null)
            {
                goto Label_01C2;
            }
            num3 = 0;
            goto Label_01B1;
        Label_00E7:
            list = this.mEnemys[num3];
            num4 = 0;
            goto Label_019F;
        Label_00F9:
            ch2 = 0x41;
            num5 = 0;
            goto Label_015E;
        Label_0105:
            if (list[num4] != list[num5])
            {
                goto Label_0121;
            }
            goto Label_016C;
        Label_0121:
            if ((list[num4].UnitParam.iname == list[num5].UnitParam.iname) == null)
            {
                goto Label_0158;
            }
            ch2 = (ushort) (ch2 + 1);
        Label_0158:
            num5 += 1;
        Label_015E:
            if (num5 < list.Count)
            {
                goto Label_0105;
            }
        Label_016C:
            list[num4].UnitName = list[num4].UnitParam.name + ((char) ch2);
            num4 += 1;
        Label_019F:
            if (num4 < list.Count)
            {
                goto Label_00F9;
            }
            num3 += 1;
        Label_01B1:
            if (num3 < this.mMap.Count)
            {
                goto Label_00E7;
            }
        Label_01C2:
            return;
        }

        private bool UpdateWeather()
        {
            return WeatherData.UpdateWeather(this.Units, this.ClockTimeTotal, this.CurrentRand);
        }

        private bool UseAutoSkills()
        {
            bool flag;
            int num;
            Unit unit;
            flag = 0;
            num = 0;
            goto Label_0028;
        Label_0009:
            unit = this.mAllUnits[num];
            if (this.UseAutoSkills(unit) == null)
            {
                goto Label_0024;
            }
            flag = 1;
        Label_0024:
            num += 1;
        Label_0028:
            if (num < this.mAllUnits.Count)
            {
                goto Label_0009;
            }
            return flag;
        }

        private bool UseAutoSkills(Unit unit)
        {
            bool flag;
            int num;
            SkillData data;
            MapEffectParam param;
            flag = 0;
            if (unit.IsUnitFlag(1) == null)
            {
                goto Label_00CD;
            }
            if (unit.IsSub != null)
            {
                goto Label_00CD;
            }
            if (unit.IsUnitFlag(0x20000) != null)
            {
                goto Label_00CD;
            }
            unit.SetUnitFlag(0x20000, 1);
            num = 0;
            goto Label_00BC;
        Label_003C:
            data = unit.BattleSkills[num];
            if (data.Timing == 8)
            {
                goto Label_005A;
            }
            goto Label_00B8;
        Label_005A:
            if (data.Condition != 2)
            {
                goto Label_009D;
            }
            param = MonoSingleton<GameManager>.Instance.GetMapEffectParam(this.mQuestParam.MapEffectId);
            if (param != null)
            {
                goto Label_0087;
            }
            goto Label_00B8;
        Label_0087:
            if (param.IsValidSkill(data.SkillID) != null)
            {
                goto Label_009D;
            }
            goto Label_00B8;
        Label_009D:
            this.UseSkill(unit, unit.x, unit.y, data, 0, 0, 0, 1);
            flag = 1;
        Label_00B8:
            num += 1;
        Label_00BC:
            if (num < unit.BattleSkills.Count)
            {
                goto Label_003C;
            }
        Label_00CD:
            return flag;
        }

        public bool UseItem(Unit self, int gx, int gy, ItemData item)
        {
            Dictionary<OString, OInt> dictionary;
            OString str;
            OInt num;
            this.DebugAssert((item == null) == 0, "item == null");
            if (this.UseSkill(self, gx, gy, item.Skill, 0, 0, 0, 0) != null)
            {
                goto Label_002E;
            }
            return 0;
        Label_002E:
            if (this.IsMultiPlay == null)
            {
                goto Label_004A;
            }
            if (this.mMyPlayerIndex != self.OwnerPlayerIndex)
            {
                goto Label_00C8;
            }
        Label_004A:
            item.Used(1);
            if (this.mRecord.used_items.ContainsKey(item.ItemID) != null)
            {
                goto Label_009A;
            }
            this.mRecord.used_items[item.ItemID] = 1;
            goto Label_00C8;
        Label_009A:
            num = dictionary[str];
            (dictionary = this.mRecord.used_items)[str = item.ItemID] = OInt.op_Increment(num);
        Label_00C8:
            return 1;
        }

        public unsafe bool UseSkill(Unit self, int gx, int gy, SkillData skill, bool bUnitLockTarget, int ux, int uy, bool is_call_auto_skill)
        {
            Unit unit;
            Grid grid;
            Grid grid2;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            List<Unit> list;
            ShotTarget target;
            int num6;
            int num7;
            Grid grid3;
            int num8;
            int num9;
            List<Unit> list2;
            Unit unit2;
            List<Unit>.Enumerator enumerator;
            bool flag;
            LogSkill skill2;
            int num10;
            int num11;
            Unit unit3;
            LogSkill.Target target2;
            Unit unit4;
            SkillData data;
            BattleLog log;
            int num12;
            bool flag2;
            OrderData data2;
            bool flag3;
            eTeleportType type;
            int num13;
            DebugUtility.Assert((self == null) == 0, "self == null");
            DebugUtility.Assert((skill == null) == 0, "skill == null");
            unit = null;
            if (skill.EffectType != 0x16)
            {
                goto Label_0063;
            }
            unit = this.FindUnitAtGrid(ux, uy);
            if (unit != null)
            {
                goto Label_0063;
            }
            unit = this.FindGimmickAtGrid(ux, uy, 0);
            if ((unit != null) && (unit.IsBreakObj != null))
            {
                goto Label_0063;
            }
            return 0;
        Label_0063:
            if ((skill.IsCastSkill() == null) || (self.CastSkill == skill))
            {
                goto Label_00B5;
            }
            if ((this.CheckEnableUseSkill(self, skill, 0) != null) && (this.IsUseSkillCollabo(self, skill) != null))
            {
                goto Label_00A6;
            }
            this.DebugErr("スキル使用条件を満たせなかった");
            return 0;
        Label_00A6:
            this.CastStart(self, gx, gy, skill, bUnitLockTarget);
            return 1;
        Label_00B5:
            if (self.Side != null)
            {
                goto Label_0108;
            }
            if (skill.SkillType != 3)
            {
                goto Label_00E0;
            }
            this.mNumUsedItems += 1;
            goto Label_0108;
        Label_00E0:
            if ((skill.SkillType != 1) || (skill.Timing == 8))
            {
                goto Label_0108;
            }
            this.mNumUsedSkills += 1;
        Label_0108:
            this.mRandDamage.Seed(this.mSeedDamage);
            this.CurrentRand = this.mRandDamage;
            if (((gx == self.x) && (gy == self.y)) || (skill.IsCastSkill() != null))
            {
                goto Label_01B1;
            }
            if (skill.SkillParam.select_scope != 12)
            {
                goto Label_0195;
            }
            grid = this.CurrentMap[self.x, self.y];
            grid2 = this.CurrentMap[gx, gy];
            self.Direction = this.UnitDirectionFromGridLaserTwin(grid, grid2);
            goto Label_01B1;
        Label_0195:
            self.Direction = UnitDirectionFromVector(self, gx - self.x, gy - self.y);
        Label_01B1:
            num = skill.EffectRate;
            if ((num <= 0) || (num >= 100))
            {
                goto Label_0206;
            }
            num2 = this.GetRandom() % 100;
            if (num2 <= num)
            {
                goto Label_0206;
            }
            self.SetUnitFlag(4, 1);
            self.SetCommandFlag(2, 1);
            if (skill.Timing == 8)
            {
                goto Label_0204;
            }
            this.Log<LogMapCommand>();
        Label_0204:
            return 1;
        Label_0206:
            num3 = self.GetSkillUsedCost(skill);
            num4 = gx;
            num5 = gy;
            list = null;
            target = null;
            num6 = self.x;
            num7 = self.y;
            type = skill.TeleportType;
            if (type == 2)
            {
                goto Label_024A;
            }
            if (type == 3)
            {
                goto Label_02B0;
            }
            goto Label_02C5;
        Label_024A:
            if (skill.IsTargetGridNoUnit == null)
            {
                goto Label_02C5;
            }
            self.x = num4;
            self.y = num5;
            grid3 = this.GetCorrectDuplicatePosition(self);
            if (grid3 == null)
            {
                goto Label_02C5;
            }
            num13 = num4 = grid3.x;
            self.x = num13;
            gx = num13;
            num13 = num5 = grid3.y;
            self.y = num13;
            gy = num13;
        Label_02AB:
            goto Label_02C5;
        Label_02B0:
            num4 = self.x;
            num5 = self.y;
        Label_02C5:
            if (skill.EffectType != 0x16)
            {
                goto Label_02E8;
            }
            list = new List<Unit>(1);
            list.Add(unit);
            goto Label_0312;
        Label_02E8:
            if (skill.IsSetBreakObjSkill() == null)
            {
                goto Label_0301;
            }
            list = new List<Unit>(1);
            goto Label_0312;
        Label_0301:
            this.GetExecuteSkillLineTarget(self, num4, num5, skill, &list, &target);
        Label_0312:
            num8 = self.x;
            num9 = self.y;
            this.ExecuteFirstReactionSkill(self, list, skill, gx, gy, null);
            this.mRandDamage.Seed(this.mSeedDamage);
            this.CurrentRand = this.mRandDamage;
            if ((self.x == num8) && (self.y == num9))
            {
                goto Label_0405;
            }
            this.CreateSelectGridMap(self, self.x, self.y, skill, &this.mRangeMap, 0);
            list2 = new List<Unit>(list.Count);
            enumerator = list.GetEnumerator();
        Label_039B:
            try
            {
                goto Label_03D0;
            Label_03A0:
                unit2 = &enumerator.Current;
                if (this.mRangeMap.get(unit2.x, unit2.y) == null)
                {
                    goto Label_03D0;
                }
                list2.Add(unit2);
            Label_03D0:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_03A0;
                }
                goto Label_03EE;
            }
            finally
            {
            Label_03E1:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_03EE:
            if (list.Count == list2.Count)
            {
                goto Label_0405;
            }
            list = list2;
        Label_0405:
            self.x = num6;
            self.y = num7;
            flag = self.IsDead == 0;
            if ((skill.IsCastSkill() == null) || (self.CastSkill == skill))
            {
                goto Label_043C;
            }
            flag = 0;
        Label_043C:
            if (flag == null)
            {
                goto Label_077A;
            }
            skill2 = this.Log<LogSkill>();
            skill2.self = self;
            skill2.skill = skill;
            &skill2.pos.x = gx;
            &skill2.pos.y = gy;
            skill2.is_append = skill.IsCutin() == 0;
            if (target == null)
            {
                goto Label_04F0;
            }
            &skill2.pos.x = target.end.x;
            &skill2.pos.y = target.end.y;
            skill2.rad = (int) (target.rad * 100.0);
            skill2.height = (int) (target.height * 100.0);
        Label_04F0:
            num10 = 0;
            goto Label_0510;
        Label_04F8:
            skill2.SetSkillTarget(self, list[num10]);
            num10 += 1;
        Label_0510:
            if (num10 < list.Count)
            {
                goto Label_04F8;
            }
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_05AF;
            }
            num11 = 0;
            goto Label_05A1;
        Label_0532:
            unit3 = this.GetGuardMan(list[num11]);
            if ((unit3 == null) || (unit3 == self))
            {
                goto Label_059B;
            }
            if (list.Contains(unit3) == null)
            {
                goto Label_0565;
            }
            goto Label_059B;
        Label_0565:
            target2 = skill2.FindTarget(list[num11]);
            target2.guard = list[num11];
            target2.target = unit3;
            list[num11] = unit3;
        Label_059B:
            num11 += 1;
        Label_05A1:
            if (num11 < list.Count)
            {
                goto Label_0532;
            }
        Label_05AF:
            if (this.IsBattleFlag(5) != null)
            {
                goto Label_0642;
            }
            unit4 = null;
            if (skill.IsCollabo == null)
            {
                goto Label_05DA;
            }
            unit4 = self.GetUnitUseCollaboSkill(skill, 0);
        Label_05DA:
            if (skill.EffectType == 14)
            {
                goto Label_060B;
            }
            this.SubGems(self, num3);
            if (unit4 == null)
            {
                goto Label_060B;
            }
            this.SubGems(unit4, unit4.GetSkillUsedCost(skill));
        Label_060B:
            self.UpdateSkillUseCount(skill, -1);
            if (unit4 == null)
            {
                goto Label_0642;
            }
            data = unit4.GetSkillForUseCount(skill.SkillParam.iname, 0);
            if (data == null)
            {
                goto Label_0642;
            }
            unit4.UpdateSkillUseCount(data, -1);
        Label_0642:
            skill2.CheckAliveTarget();
            this.ExecuteSkill((skill.Timing != 8) ? 0 : 8, skill2, skill);
            if ((skill.Timing != 8) || ((string.IsNullOrEmpty(skill.SkillParam.motion) == null) && (string.IsNullOrEmpty(skill.SkillParam.effect) == null)))
            {
                goto Label_06DD;
            }
        Label_06A0:
            log = this.mLogs.Last;
            if (log != null)
            {
                goto Label_06B9;
            }
            goto Label_06DA;
        Label_06B9:
            this.mLogs.RemoveLogLast();
            if ((log as LogSkill) == null)
            {
                goto Label_06A0;
            }
            goto Label_06DA;
            goto Label_06A0;
        Label_06DA:
            skill2 = null;
        Label_06DD:
            self.CancelCastSkill();
            this.ExecuteReactionSkill(skill2, null);
            if ((this.IsBattleFlag(5) != null) || (skill2 == null))
            {
                goto Label_077A;
            }
            this.AddSkillExecLogForQuestMission(skill2);
            num12 = 0;
            goto Label_0767;
        Label_070F:
            if (skill2.targets[num12].target.Side == 1)
            {
                goto Label_0732;
            }
            goto Label_0761;
        Label_0732:
            if (skill2.targets[num12].target.IsDead != null)
            {
                goto Label_0754;
            }
            goto Label_0761;
        Label_0754:
            this.TrySetBattleFinisher(self);
            goto Label_077A;
        Label_0761:
            num12 += 1;
        Label_0767:
            if (num12 < skill2.targets.Count)
            {
                goto Label_070F;
            }
        Label_077A:
            self.SetUnitFlag(4, 1);
            self.SetCommandFlag(2, 1);
            if ((skill.TeleportType == null) || (skill.TeleportIsMove != null))
            {
                goto Label_07BE;
            }
            self.SetUnitFlag(0x80, 0);
            self.SetUnitFlag(2, 1);
            self.SetCommandFlag(1, 1);
        Label_07BE:
            this.CurrentRand = this.mRand;
            if ((skill.Timing == 8) || (this.GetQuestResult() == null))
            {
                goto Label_07FC;
            }
            this.ExecuteEventTriggerOnGrid(self, 0);
            this.CalcQuestRecord();
            this.MapEnd();
            goto Label_08D2;
        Label_07FC:
            flag2 = skill.IsCastSkill();
            if (is_call_auto_skill != null)
            {
                goto Label_0824;
            }
            data2 = this.CurrentOrderData;
            if (data2 == null)
            {
                goto Label_0824;
            }
            flag2 = data2.IsCastSkill;
        Label_0824:
            if (flag2 == null)
            {
                goto Label_0854;
            }
            this.Log<LogCastSkillEnd>();
            if (skill.TeleportType == null)
            {
                goto Label_08D2;
            }
            this.TrickActionEndEffect(self, 1);
            this.ExecuteEventTriggerOnGrid(self, 0);
            goto Label_08D2;
        Label_0854:
            flag3 = 0;
            if (self != this.CurrentUnit)
            {
                goto Label_08AC;
            }
            if (self.IsDead == null)
            {
                goto Label_0876;
            }
            flag3 = 1;
            goto Label_08AC;
        Label_0876:
            if (self.IsUnitFlag(2) == null)
            {
                goto Label_0892;
            }
            flag3 = self.IsEnableSelectDirectionCondition() == 0;
            goto Label_08AC;
        Label_0892:
            flag3 = (self.IsEnableMoveCondition(0) != null) ? 0 : (self.IsEnableSelectDirectionCondition() == 0);
        Label_08AC:
            if (flag3 == null)
            {
                goto Label_08BE;
            }
            this.InternalLogUnitEnd();
            goto Label_08D2;
        Label_08BE:
            if (skill.Timing == 8)
            {
                goto Label_08D2;
            }
            this.Log<LogMapCommand>();
        Label_08D2:
            return 1;
        }

        private bool UseSkillAI(Unit self, SkillResult result, bool forceAI)
        {
            bool flag;
            if (self == null)
            {
                goto Label_000C;
            }
            if (result != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            if (self.x != result.movpos.x)
            {
                goto Label_003A;
            }
            if (self.y == result.movpos.y)
            {
                goto Label_004F;
            }
        Label_003A:
            if (this.Move(self, result.movpos, forceAI) == null)
            {
                goto Label_004F;
            }
            return 1;
        Label_004F:
            flag = 0;
            if (result.skill.IsCastSkill() == null)
            {
                goto Label_00B6;
            }
            if (result.skill.IsEnableUnitLockTarget() == null)
            {
                goto Label_00B6;
            }
            if (result.skill.IsAreaSkill() == null)
            {
                goto Label_00AF;
            }
            if (result.skill.Target == 3)
            {
                goto Label_00B6;
            }
            if (result.skill.Target == 4)
            {
                goto Label_00B6;
            }
            flag = result.locked;
            goto Label_00B6;
        Label_00AF:
            flag = result.locked;
        Label_00B6:
            return this.UseSkill(self, result.usepos.x, result.usepos.y, result.skill, flag, 0, 0, 0);
        }

        public uint Seed
        {
            get
            {
                return this.mSeed;
            }
            set
            {
                this.mSeed = value;
                return;
            }
        }

        public RandXorshift Rand
        {
            get
            {
                return this.mRand;
            }
        }

        public uint DamageSeed
        {
            get
            {
                return this.mSeedDamage;
            }
            set
            {
                this.mSeedDamage = value;
                return;
            }
        }

        public Dictionary<string, SkillExecLog> SkillExecLogs
        {
            get
            {
                return this.mSkillExecLogs;
            }
        }

        public bool SyncStart
        {
            [CompilerGenerated]
            get
            {
                return this.<SyncStart>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<SyncStart>k__BackingField = value;
                return;
            }
        }

        public int MyPlayerIndex
        {
            get
            {
                return this.mMyPlayerIndex;
            }
        }

        public bool IsMultiPlay
        {
            [CompilerGenerated]
            get
            {
                return this.<IsMultiPlay>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<IsMultiPlay>k__BackingField = value;
                return;
            }
        }

        public bool IsMultiVersus
        {
            [CompilerGenerated]
            get
            {
                return this.<IsMultiVersus>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<IsMultiVersus>k__BackingField = value;
                return;
            }
        }

        public bool IsRankMatch
        {
            [CompilerGenerated]
            get
            {
                return this.<IsRankMatch>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<IsRankMatch>k__BackingField = value;
                return;
            }
        }

        public bool IsMultiTower
        {
            [CompilerGenerated]
            get
            {
                return this.<IsMultiTower>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<IsMultiTower>k__BackingField = value;
                return;
            }
        }

        public bool IsTower
        {
            get
            {
                return ((this.mQuestParam == null) ? 0 : (this.mQuestParam.type == 7));
            }
        }

        public bool IsOrdeal
        {
            get
            {
                return ((this.mQuestParam == null) ? 0 : (this.mQuestParam.type == 15));
            }
        }

        public bool IsVSForceWin
        {
            [CompilerGenerated]
            get
            {
                return this.<IsVSForceWin>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsVSForceWin>k__BackingField = value;
                return;
            }
        }

        public bool IsVSForceWinComfirm
        {
            [CompilerGenerated]
            get
            {
                return this.<IsVSForceWinComfirm>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsVSForceWinComfirm>k__BackingField = value;
                return;
            }
        }

        public bool IsRankingQuest
        {
            get
            {
                return ((this.mRankingQuestParam == null) == 0);
            }
        }

        public bool MultiFinishLoad
        {
            get
            {
                return this.mMultiFinishLoad;
            }
            set
            {
                this.mMultiFinishLoad = value;
                return;
            }
        }

        public RESUME_STATE ResumeState
        {
            get
            {
                return this.mResumeState;
            }
            set
            {
                this.mResumeState = value;
                return;
            }
        }

        public bool IsResume
        {
            get
            {
                return (this.mResumeState == 2);
            }
        }

        public string QuestID
        {
            get
            {
                return ((this.mQuestParam == null) ? null : this.mQuestParam.iname);
            }
        }

        public string QuestName
        {
            get
            {
                return ((this.mQuestParam == null) ? null : this.mQuestParam.name);
            }
        }

        public string QuestTerms
        {
            get
            {
                return ((this.mQuestParam == null) ? null : this.mQuestParam.cond);
            }
        }

        public QuestTypes QuestType
        {
            get
            {
                return ((this.mQuestParam == null) ? 0 : this.mQuestParam.type);
            }
        }

        public string QuestClearEventName
        {
            get
            {
                return ((this.mQuestParam == null) ? null : this.mQuestParam.event_clear);
            }
        }

        public bool IsUnitChange
        {
            get
            {
                return ((this.mQuestParam == null) ? 0 : this.mQuestParam.IsUnitChange);
            }
        }

        public bool IsMultiLeaderSkill
        {
            get
            {
                return ((this.mQuestParam == null) ? 0 : this.mQuestParam.IsMultiLeaderSkill);
            }
        }

        public List<BattleMap> Map
        {
            get
            {
                return this.mMap;
            }
        }

        public List<Unit> AllUnits
        {
            get
            {
                return this.mAllUnits;
            }
        }

        public List<Unit> Units
        {
            get
            {
                return this.mUnits;
            }
        }

        public List<OrderData> Order
        {
            get
            {
                return this.mOrder;
            }
        }

        public List<Unit> StartingMembers
        {
            get
            {
                return this.mStartingMembers;
            }
        }

        public List<Unit> HelperUnits
        {
            get
            {
                return this.mHelperUnits;
            }
        }

        public int MapIndex
        {
            get
            {
                return this.mMapIndex;
            }
        }

        public BattleMap CurrentMap
        {
            get
            {
                return ((((this.mMap == null) || (0 > this.mMapIndex)) || (this.mMapIndex >= this.mMap.Count)) ? null : this.mMap[this.mMapIndex]);
            }
        }

        public Unit CurrentUnit
        {
            get
            {
                return ((this.mOrder.Count <= 0) ? null : this.mOrder[0].Unit);
            }
        }

        public OrderData CurrentOrderData
        {
            get
            {
                return ((this.mOrder.Count <= 0) ? null : this.mOrder[0]);
            }
        }

        public BattleLogServer Logs
        {
            get
            {
                return this.mLogs;
            }
        }

        public List<Unit> Player
        {
            get
            {
                return this.mPlayer;
            }
        }

        public List<Unit> Enemys
        {
            get
            {
                return (((this.MapIndex < 0) || (this.MapIndex >= ((int) this.mEnemys.Length))) ? null : this.mEnemys[this.MapIndex]);
            }
        }

        public Unit Leader
        {
            get
            {
                if ((((this.IsMultiPlay != null) || (this.mLeaderIndex == -1)) && (((this.IsMultiPlay == null) || (this.mLeaderIndex == -1)) || (this.mQuestParam.IsMultiLeaderSkill == null))) && ((this.IsMultiVersus == null) || (this.mLeaderIndex == -1)))
                {
                    goto Label_00B8;
                }
                return ((((this.mPlayer == null) || (this.mLeaderIndex < 0)) || (this.mLeaderIndex >= this.mPlayer.Count)) ? null : this.mPlayer[this.mLeaderIndex]);
            Label_00B8:
                return null;
            }
        }

        public Unit Friend
        {
            get
            {
                if ((this.IsMultiPlay != null) || (this.mFriendIndex == -1))
                {
                    goto Label_0070;
                }
                return ((((this.mPlayer == null) || (this.mFriendIndex < 0)) || (this.mFriendIndex >= this.mPlayer.Count)) ? null : this.mPlayer[this.mFriendIndex]);
            Label_0070:
                return null;
            }
        }

        public Unit EnemyLeader
        {
            get
            {
                if (((this.IsMultiPlay != null) || (this.mEnemyLeaderIndex == -1)) && ((this.IsMultiVersus == null) || (this.mEnemyLeaderIndex == -1)))
                {
                    goto Label_008C;
                }
                return ((((this.Enemys == null) || (this.mEnemyLeaderIndex < 0)) || (this.mEnemyLeaderIndex >= this.Enemys.Count)) ? null : this.Enemys[this.mEnemyLeaderIndex]);
            Label_008C:
                return null;
            }
        }

        public bool IsFriendStatus
        {
            get
            {
                return ((this.Friend == null) ? 0 : (this.mFriendStates == 1));
            }
        }

        public long BtlID
        {
            get
            {
                return this.mBtlID;
            }
        }

        public int WinTriggerCount
        {
            get
            {
                return this.mWinTriggerCount;
            }
            set
            {
                this.mWinTriggerCount = value;
                return;
            }
        }

        public int LoseTriggerCount
        {
            get
            {
                return this.mLoseTriggerCount;
            }
            set
            {
                this.mLoseTriggerCount = value;
                return;
            }
        }

        public int ActionCount
        {
            get
            {
                return this.mActionCount;
            }
            set
            {
                this.mActionCount = value;
                return;
            }
        }

        public int Killstreak
        {
            get
            {
                return this.mKillstreak;
            }
            set
            {
                this.mKillstreak = value;
                return;
            }
        }

        public int MaxKillstreak
        {
            get
            {
                return this.mMaxKillstreak;
            }
            set
            {
                this.mMaxKillstreak = value;
                return;
            }
        }

        public Dictionary<string, int> TargetKillstreak
        {
            get
            {
                return this.mTargetKillstreakDict;
            }
            set
            {
                this.mTargetKillstreakDict = value;
                return;
            }
        }

        public Dictionary<string, int> MaxTargetKillstreak
        {
            get
            {
                return this.mMaxTargetKillstreakDict;
            }
            set
            {
                this.mMaxTargetKillstreakDict = value;
                return;
            }
        }

        public bool PlayByManually
        {
            get
            {
                return this.mPlayByManually;
            }
            set
            {
                this.mPlayByManually = value;
                return;
            }
        }

        public bool IsUseAutoPlayMode
        {
            get
            {
                return this.mIsUseAutoPlayMode;
            }
            set
            {
                this.mIsUseAutoPlayMode = value;
                return;
            }
        }

        public int TotalHeal
        {
            get
            {
                return this.mTotalHeal;
            }
            set
            {
                this.mTotalHeal = value;
                return;
            }
        }

        public int TotalDamagesTaken
        {
            get
            {
                return this.mTotalDamagesTaken;
            }
            set
            {
                this.mTotalDamagesTaken = value;
                return;
            }
        }

        public int TotalDamages
        {
            get
            {
                return this.mTotalDamages;
            }
            set
            {
                this.mTotalDamages = value;
                return;
            }
        }

        public int NumUsedItems
        {
            get
            {
                return this.mNumUsedItems;
            }
            set
            {
                this.mNumUsedItems = value;
                return;
            }
        }

        public int NumUsedSkills
        {
            get
            {
                return this.mNumUsedSkills;
            }
            set
            {
                this.mNumUsedSkills = value;
                return;
            }
        }

        public int ClockTime
        {
            get
            {
                return this.mClockTime;
            }
            set
            {
                this.mClockTime = value;
                return;
            }
        }

        public int ClockTimeTotal
        {
            get
            {
                return this.mClockTimeTotal;
            }
            set
            {
                this.mClockTimeTotal = value;
                return;
            }
        }

        public int ContinueCount
        {
            get
            {
                return this.mContinueCount;
            }
            set
            {
                this.mContinueCount = value;
                return;
            }
        }

        public int CurrentTeamId
        {
            get
            {
                return this.mCurrentTeamId;
            }
            set
            {
                this.mCurrentTeamId = value;
                return;
            }
        }

        public int MaxTeamId
        {
            get
            {
                return this.mMaxTeamId;
            }
            set
            {
                this.mMaxTeamId = value;
                return;
            }
        }

        public string FinisherIname
        {
            get
            {
                return this.mFinisherIname;
            }
            set
            {
                this.mFinisherIname = value;
                return;
            }
        }

        public bool RequestAutoBattle
        {
            [CompilerGenerated]
            get
            {
                return this.<RequestAutoBattle>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<RequestAutoBattle>k__BackingField = value;
                return;
            }
        }

        public bool IsAutoBattle
        {
            [CompilerGenerated]
            get
            {
                return this.<IsAutoBattle>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsAutoBattle>k__BackingField = value;
                return;
            }
        }

        public bool IsSkillDirection
        {
            get
            {
                if (this.IsMultiPlay != null)
                {
                    goto Label_0016;
                }
                if (this.IsMultiVersus == null)
                {
                    goto Label_0018;
                }
            Label_0016:
                return 1;
            Label_0018:
                if (this.mQuestParam == null)
                {
                    goto Label_0036;
                }
                if (this.mQuestParam.type != 2)
                {
                    goto Label_0036;
                }
                return 1;
            Label_0036:
                return GameUtility.Config_DirectionCut.Value;
            }
        }

        public string[] QuestCampaignIds
        {
            get
            {
                return this.mQuestCampaignIds;
            }
        }

        public List<GimmickEvent> GimmickEventList
        {
            get
            {
                return this.mGimmickEvents;
            }
        }

        public bool IsInitialized
        {
            get
            {
                return this.IsBattleFlag(0);
            }
        }

        public bool IsMapCommand
        {
            get
            {
                return this.IsBattleFlag(3);
            }
        }

        public bool IsBattle
        {
            get
            {
                return this.IsBattleFlag(4);
            }
        }

        public bool EntryBattleMultiPlayTimeUp
        {
            [CompilerGenerated]
            get
            {
                return this.<EntryBattleMultiPlayTimeUp>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<EntryBattleMultiPlayTimeUp>k__BackingField = value;
                return;
            }
        }

        public bool MultiPlayDisconnectAutoBattle
        {
            [CompilerGenerated]
            get
            {
                return this.<MultiPlayDisconnectAutoBattle>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<MultiPlayDisconnectAutoBattle>k__BackingField = value;
                return;
            }
        }

        public bool IsArenaSkip
        {
            get
            {
                return this.mIsArenaSkip;
            }
            set
            {
                this.mIsArenaSkip = value;
                return;
            }
        }

        public uint ArenaActionCount
        {
            get
            {
                return this.mArenaActionCount;
            }
        }

        public bool IsArenaCalc
        {
            get
            {
                return this.mIsArenaCalc;
            }
        }

        public QuestResult ArenaCalcResult
        {
            get
            {
                return this.mArenaCalcResult;
            }
        }

        public uint VersusTurnMax
        {
            [CompilerGenerated]
            get
            {
                return this.<VersusTurnMax>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VersusTurnMax>k__BackingField = value;
                return;
            }
        }

        public uint VersusTurnCount
        {
            [CompilerGenerated]
            get
            {
                return this.<VersusTurnCount>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<VersusTurnCount>k__BackingField = value;
                return;
            }
        }

        public uint RemainVersusTurnCount
        {
            get
            {
                return (this.VersusTurnMax - this.VersusTurnCount);
            }
            set
            {
                this.VersusTurnCount = value;
                return;
            }
        }

        public bool IsEnableAag
        {
            get
            {
                return ((((this.IsEnableAagBuff != null) || (this.IsEnableAagDebuff != null)) || (this.IsEnableAagBuffNegative != null)) ? 1 : this.IsEnableAagDebuffNegative);
            }
        }

        public GridMap<int> MoveMap
        {
            get
            {
                return this.mMoveMap;
            }
        }

        public GridMap<bool> RangeMap
        {
            get
            {
                return this.mRangeMap;
            }
        }

        public GridMap<bool> ScopeMap
        {
            get
            {
                return this.mScopeMap;
            }
        }

        public GridMap<bool> SearchMap
        {
            get
            {
                return this.mSearchMap;
            }
        }

        public SRPG.SkillMap SkillMap
        {
            get
            {
                return this.mSkillMap;
            }
        }

        public SRPG.TrickMap TrickMap
        {
            get
            {
                return this.mTrickMap;
            }
        }

        [CompilerGenerated]
        private sealed class <BeginBattlePassiveSkill>c__AnonStorey1B6
        {
            internal int i;

            public <BeginBattlePassiveSkill>c__AnonStorey1B6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__5C(Unit data)
            {
                return (data.OwnerPlayerIndex == (this.i + 1));
            }
        }

        [CompilerGenerated]
        private sealed class <CalcAtkPointSkill>c__AnonStorey1C9
        {
            internal Unit defender;

            public <CalcAtkPointSkill>c__AnonStorey1C9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__70(LogSkill.Target p)
            {
                return (p.target == this.defender);
            }
        }

        [CompilerGenerated]
        private sealed class <CalcMoveTargetAI>c__AnonStorey1C3
        {
            internal Grid start;
            internal BattleMap map;

            public <CalcMoveTargetAI>c__AnonStorey1C3()
            {
                base..ctor();
                return;
            }

            internal int <>m__6B(Grid src, Grid dsc)
            {
                int num;
                int num2;
                int num3;
                int num4;
                Grid grid;
                Grid grid2;
                Grid grid3;
                Grid grid4;
                Grid grid5;
                Grid grid6;
                Grid grid7;
                Grid grid8;
                if ((src.step != dsc.step) || (src.step > this.start.step))
                {
                    goto Label_02A0;
                }
                num = 0;
                num2 = 0;
                num3 = 0;
                num4 = 0;
                grid = this.map[src.x - 1, src.y];
                grid2 = this.map[src.x + 1, src.y];
                grid3 = this.map[src.x, src.y - 1];
                grid4 = this.map[src.x, src.y + 1];
                if ((grid == null) || (Math.Abs(src.step - grid.step) != 1))
                {
                    goto Label_00C9;
                }
                num += grid.step;
                num2 += 1;
            Label_00C9:
                if ((grid2 == null) || (Math.Abs(src.step - grid2.step) != 1))
                {
                    goto Label_00F7;
                }
                num += grid2.step;
                num2 += 1;
            Label_00F7:
                if ((grid3 == null) || (Math.Abs(src.step - grid3.step) != 1))
                {
                    goto Label_0125;
                }
                num += grid3.step;
                num2 += 1;
            Label_0125:
                if ((grid4 == null) || (Math.Abs(src.step - grid4.step) != 1))
                {
                    goto Label_0153;
                }
                num += grid4.step;
                num2 += 1;
            Label_0153:
                grid5 = this.map[dsc.x - 1, dsc.y];
                grid6 = this.map[dsc.x + 1, dsc.y];
                grid7 = this.map[dsc.x, dsc.y - 1];
                grid8 = this.map[dsc.x, dsc.y + 1];
                if ((grid5 == null) || (Math.Abs(dsc.step - grid5.step) != 1))
                {
                    goto Label_01ED;
                }
                num3 += grid5.step;
                num4 += 1;
            Label_01ED:
                if ((grid6 == null) || (Math.Abs(dsc.step - grid6.step) != 1))
                {
                    goto Label_021B;
                }
                num3 += grid6.step;
                num4 += 1;
            Label_021B:
                if ((grid7 == null) || (Math.Abs(dsc.step - grid7.step) != 1))
                {
                    goto Label_0249;
                }
                num3 += grid7.step;
                num4 += 1;
            Label_0249:
                if ((grid8 == null) || (Math.Abs(dsc.step - grid8.step) != 1))
                {
                    goto Label_0277;
                }
                num3 += grid8.step;
                num4 += 1;
            Label_0277:
                if ((num == null) || (num3 == null))
                {
                    goto Label_02A0;
                }
                num = (num * 100) / num2;
                num3 = (num3 * 100) / num4;
                return ((num >= num3) ? 1 : -1);
            Label_02A0:
                return (src.step - dsc.step);
            }
        }

        [CompilerGenerated]
        private sealed class <CalcUseActionAI>c__AnonStorey1BF
        {
            internal AIAction action;

            public <CalcUseActionAI>c__AnonStorey1BF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__66(SkillData p)
            {
                return (p.SkillID == this.action.skill);
            }
        }

        [CompilerGenerated]
        private sealed class <GetGridKnockBack>c__AnonStorey1BB
        {
            internal int ux;
            internal int uy;

            public <GetGridKnockBack>c__AnonStorey1BB()
            {
                base..ctor();
                return;
            }

            internal bool <>m__62(Unit du)
            {
                return du.CheckCollision(this.ux, this.uy, 0);
            }
        }

        [CompilerGenerated]
        private sealed class <GetGridOnLine>c__AnonStorey1B9
        {
            internal Grid start;
            internal BattleCore <>f__this;

            public <GetGridOnLine>c__AnonStorey1B9()
            {
                base..ctor();
                return;
            }

            internal int <>m__60(Grid src, Grid dsc)
            {
                int num;
                int num2;
                if (src != dsc)
                {
                    goto Label_0009;
                }
                return 0;
            Label_0009:
                num = this.<>f__this.CalcGridDistance(this.start, src);
                num2 = this.<>f__this.CalcGridDistance(this.start, dsc);
                return (num - num2);
            }
        }

        [CompilerGenerated]
        private sealed class <GetHealer>c__AnonStorey1C6
        {
            internal Unit self;
            internal BattleCore <>f__this;

            public <GetHealer>c__AnonStorey1C6()
            {
                base..ctor();
                return;
            }

            internal int <>m__6D(Unit src, Unit dsc)
            {
                int num;
                int num2;
                int num3;
                int num4;
                int num5;
                int num6;
                int num7;
                int num8;
                int num9;
                int num10;
                if (src != dsc)
                {
                    goto Label_0009;
                }
                return 0;
            Label_0009:
                num = src.ChargeTime;
                num2 = src.ChargeTimeMax;
                num3 = dsc.ChargeTime;
                num4 = dsc.ChargeTimeMax;
                if (num == num3)
                {
                    goto Label_00C3;
                }
                if ((num < num2) || (num3 < num4))
                {
                    goto Label_0056;
                }
                return ((num3 - num4) - (num - num2));
            Label_0056:
                if (num < num2)
                {
                    goto Label_005F;
                }
                return -1;
            Label_005F:
                if (num3 < num4)
                {
                    goto Label_0068;
                }
                return 1;
            Label_0068:
                num5 = src.GetChargeSpeed();
                num6 = dsc.GetChargeSpeed();
                num7 = ((num2 - num) == null) ? 0 : (((num2 - num) * 100) / num5);
                num8 = ((num4 - num3) == null) ? 0 : (((num4 - num3) * 100) / num6);
                if (num7 == num8)
                {
                    goto Label_00C3;
                }
                return (num7 - num8);
            Label_00C3:
                num9 = this.<>f__this.CalcNearGridDistance(this.self, src);
                num10 = this.<>f__this.CalcNearGridDistance(this.self, dsc);
                return (num9 - num10);
            }
        }

        [CompilerGenerated]
        private sealed class <GetSkillTargetsHighestPriority>c__AnonStorey1C0
        {
            internal LogSkill log;

            public <GetSkillTargetsHighestPriority>c__AnonStorey1C0()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <GetSkillTargetsHighestPriority>c__AnonStorey1C1
        {
            internal int k;
            internal BattleCore.<GetSkillTargetsHighestPriority>c__AnonStorey1C0 <>f__ref$448;

            public <GetSkillTargetsHighestPriority>c__AnonStorey1C1()
            {
                base..ctor();
                return;
            }

            internal bool <>m__67(Unit p)
            {
                return (p == this.<>f__ref$448.log.targets[this.k].target);
            }
        }

        [CompilerGenerated]
        private sealed class <IsEnableUseSkillEffect>c__AnonStorey1C2
        {
            internal Unit rage;
            internal Unit self;

            public <IsEnableUseSkillEffect>c__AnonStorey1C2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__68(LogSkill.Target p)
            {
                return (p.target == this.rage);
            }

            internal bool <>m__69(LogSkill.Target p)
            {
                return (p.target == this.self);
            }
        }

        [CompilerGenerated]
        private sealed class <MoveMultiPlayer>c__AnonStorey1B8
        {
            internal Unit self;

            public <MoveMultiPlayer>c__AnonStorey1B8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__5F(Unit p)
            {
                return (p == this.self);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshTreasureTargetAI>c__AnonStorey1C7
        {
            internal Unit suited;

            public <RefreshTreasureTargetAI>c__AnonStorey1C7()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshTreasureTargetAI>c__AnonStorey1C8
        {
            internal Unit unit;
            internal BattleCore.<RefreshTreasureTargetAI>c__AnonStorey1C7 <>f__ref$455;

            public <RefreshTreasureTargetAI>c__AnonStorey1C8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__6E(BattleCore.OrderData p)
            {
                return (p.Unit == this.<>f__ref$455.suited);
            }

            internal bool <>m__6F(BattleCore.OrderData p)
            {
                return (p.Unit == this.unit);
            }
        }

        [CompilerGenerated]
        private sealed class <SetReward>c__AnonStorey1B7
        {
            internal QuestBonusObjective bonus;

            public <SetReward>c__AnonStorey1B7()
            {
                base..ctor();
                return;
            }

            internal bool <>m__5E(ArtifactParam arti)
            {
                return (arti.iname == this.bonus.item);
            }
        }

        [CompilerGenerated]
        private sealed class <SortAttackTargets>c__AnonStorey1C4
        {
            internal Unit rage;
            internal BattleCore.<SortAttackTargets>c__AnonStorey1C5 <>f__ref$453;
            internal BattleCore <>f__this;

            public <SortAttackTargets>c__AnonStorey1C4()
            {
                base..ctor();
                return;
            }

            internal int <>m__6C(Unit src, Unit dsc)
            {
                AIParam param;
                RoleTypes types;
                RoleTypes types2;
                RoleTypes types3;
                int num;
                int num2;
                int num3;
                ParamTypes types4;
                ParamPriorities priorities;
                if (src != dsc)
                {
                    goto Label_0009;
                }
                return 0;
            Label_0009:
                if ((src.UnitType == null) || (dsc.UnitType != null))
                {
                    goto Label_0021;
                }
                return 1;
            Label_0021:
                if ((dsc.UnitType == null) || (src.UnitType != null))
                {
                    goto Label_0039;
                }
                return -1;
            Label_0039:
                if (this.rage == null)
                {
                    goto Label_0060;
                }
                if (src != this.rage)
                {
                    goto Label_0052;
                }
                return -1;
            Label_0052:
                if (dsc != this.rage)
                {
                    goto Label_0060;
                }
                return 1;
            Label_0060:
                param = this.<>f__ref$453.unit.AI;
                if (param == null)
                {
                    goto Label_0297;
                }
                types = param.role;
                types2 = src.RoleType;
                types3 = dsc.RoleType;
                if ((types == null) || (types2 == types3))
                {
                    goto Label_00AB;
                }
                if (types != types2)
                {
                    goto Label_00A2;
                }
                return -1;
            Label_00A2:
                if (types != types3)
                {
                    goto Label_00AB;
                }
                return 1;
            Label_00AB:
                if (param.param_prio == null)
                {
                    goto Label_0297;
                }
                num = 0;
                switch ((param.param - 1))
                {
                    case 0:
                        goto Label_00EF;

                    case 1:
                        goto Label_0121;

                    case 2:
                        goto Label_021B;

                    case 3:
                        goto Label_021B;

                    case 4:
                        goto Label_0153;

                    case 5:
                        goto Label_0185;

                    case 6:
                        goto Label_01B7;

                    case 7:
                        goto Label_01E9;
                }
                goto Label_021B;
            Label_00EF:
                num = src.CurrentStatus.param.hp - dsc.CurrentStatus.param.hp;
                goto Label_0220;
            Label_0121:
                num = src.MaximumStatus.param.hp - dsc.MaximumStatus.param.hp;
                goto Label_0220;
            Label_0153:
                num = src.CurrentStatus.param.atk - dsc.CurrentStatus.param.atk;
                goto Label_0220;
            Label_0185:
                num = src.CurrentStatus.param.def - dsc.CurrentStatus.param.def;
                goto Label_0220;
            Label_01B7:
                num = src.CurrentStatus.param.mag - dsc.CurrentStatus.param.mag;
                goto Label_0220;
            Label_01E9:
                num = src.CurrentStatus.param.mnd - dsc.CurrentStatus.param.mnd;
                goto Label_0220;
            Label_021B:;
            Label_0220:
                num *= (param.param_prio != 1) ? 1 : -1;
                if (num == null)
                {
                    goto Label_0297;
                }
                if (param.CheckFlag(0x400) == null)
                {
                    goto Label_0294;
                }
                priorities = param.param_prio;
                if (priorities == 1)
                {
                    goto Label_027A;
                }
                if (priorities == 2)
                {
                    goto Label_026C;
                }
                goto Label_028A;
            Label_026C:
                return Math.Min(Math.Abs(num), 1);
            Label_027A:
                return (Math.Min(Math.Abs(num), 1) * -1);
            Label_028A:;
            Label_028F:
                goto Label_0297;
            Label_0294:
                return num;
            Label_0297:
                num2 = this.<>f__this.CalcNearGridDistance(this.<>f__ref$453.unit, src);
                num3 = this.<>f__this.CalcNearGridDistance(this.<>f__ref$453.unit, dsc);
                return (num2 - num3);
            }
        }

        [CompilerGenerated]
        private sealed class <SortAttackTargets>c__AnonStorey1C5
        {
            internal Unit unit;
            internal BattleCore <>f__this;

            public <SortAttackTargets>c__AnonStorey1C5()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <SortSkillResult>c__AnonStorey1BE
        {
            internal Unit self;
            internal bool bPositioning;

            public <SortSkillResult>c__AnonStorey1BE()
            {
                base..ctor();
                return;
            }

            internal int <>m__65(BattleCore.SkillResult src, BattleCore.SkillResult dsc)
            {
                int num;
                int num2;
                if (src != dsc)
                {
                    goto Label_0009;
                }
                return 0;
            Label_0009:
                if (dsc.score_prio == src.score_prio)
                {
                    goto Label_0028;
                }
                return (dsc.score_prio - src.score_prio);
            Label_0028:
                if (dsc.fail_trick == src.fail_trick)
                {
                    goto Label_0047;
                }
                return (src.fail_trick - dsc.fail_trick);
            Label_0047:
                if (dsc.heal_trick == src.heal_trick)
                {
                    goto Label_0066;
                }
                return (dsc.heal_trick - src.heal_trick);
            Label_0066:
                if (dsc.unit_prio == src.unit_prio)
                {
                    goto Label_0085;
                }
                return (src.unit_prio - dsc.unit_prio);
            Label_0085:
                if (src.skill.IsDamagedSkill() == null)
                {
                    goto Label_0257;
                }
                if (dsc.unit_dead_num == src.unit_dead_num)
                {
                    goto Label_00B4;
                }
                return (dsc.unit_dead_num - src.unit_dead_num);
            Label_00B4:
                if (dsc.nockback_prio == src.nockback_prio)
                {
                    goto Label_00D3;
                }
                return (dsc.nockback_prio - src.nockback_prio);
            Label_00D3:
                if (dsc.unit_damage == src.unit_damage)
                {
                    goto Label_0130;
                }
                if (this.self.AI == null)
                {
                    goto Label_0130;
                }
                if (Math.Abs(dsc.unit_damage - src.unit_damage) <= this.self.AI.gosa_border)
                {
                    goto Label_0130;
                }
                return (dsc.unit_damage - src.unit_damage);
            Label_0130:
                if (dsc.unit_dead_num != null)
                {
                    goto Label_01D6;
                }
                if (dsc.cond_prio == src.cond_prio)
                {
                    goto Label_015A;
                }
                return (src.cond_prio - dsc.cond_prio);
            Label_015A:
                if (dsc.fail_num == src.fail_num)
                {
                    goto Label_0179;
                }
                return (dsc.fail_num - src.fail_num);
            Label_0179:
                if (dsc.buff_prio == src.buff_prio)
                {
                    goto Label_0198;
                }
                return (src.buff_prio - dsc.buff_prio);
            Label_0198:
                if (dsc.buff == src.buff)
                {
                    goto Label_01B7;
                }
                return (dsc.buff - src.buff);
            Label_01B7:
                if (dsc.buff_num == src.buff_num)
                {
                    goto Label_01D6;
                }
                return (dsc.buff_num - src.buff_num);
            Label_01D6:
                if (dsc.ext_dead_num == src.ext_dead_num)
                {
                    goto Label_01F5;
                }
                return (dsc.ext_dead_num - src.ext_dead_num);
            Label_01F5:
                if (dsc.ext_damage == src.ext_damage)
                {
                    goto Label_0435;
                }
                if (this.self.AI == null)
                {
                    goto Label_0435;
                }
                if (Math.Abs(dsc.ext_damage - src.ext_damage) <= this.self.AI.gosa_border)
                {
                    goto Label_0435;
                }
                return (dsc.ext_damage - src.ext_damage);
                goto Label_0435;
            Label_0257:
                if (src.skill.IsHealSkill() == null)
                {
                    goto Label_02AA;
                }
                if (dsc.heal == src.heal)
                {
                    goto Label_0286;
                }
                return (dsc.heal - src.heal);
            Label_0286:
                if (dsc.heal_num == src.heal_num)
                {
                    goto Label_0435;
                }
                return (dsc.heal_num - src.heal_num);
                goto Label_0435;
            Label_02AA:
                if (src.skill.IsSupportSkill() == null)
                {
                    goto Label_033B;
                }
                if (dsc.buff_prio == src.buff_prio)
                {
                    goto Label_02D9;
                }
                return (src.buff_prio - dsc.buff_prio);
            Label_02D9:
                if (dsc.buff == src.buff)
                {
                    goto Label_02F8;
                }
                return (dsc.buff - src.buff);
            Label_02F8:
                if (dsc.buff_num == src.buff_num)
                {
                    goto Label_0317;
                }
                return (dsc.buff_num - src.buff_num);
            Label_0317:
                if (dsc.buff_dup == src.buff_dup)
                {
                    goto Label_0435;
                }
                return (src.buff_dup - dsc.buff_dup);
                goto Label_0435;
            Label_033B:
                if (src.skill.EffectType != 12)
                {
                    goto Label_0390;
                }
                if (dsc.cond_prio == src.cond_prio)
                {
                    goto Label_036C;
                }
                return (src.cond_prio - dsc.cond_prio);
            Label_036C:
                if (dsc.cure_num == src.cure_num)
                {
                    goto Label_0435;
                }
                return (dsc.cure_num - src.cure_num);
                goto Label_0435;
            Label_0390:
                if (src.skill.EffectType != 11)
                {
                    goto Label_03E5;
                }
                if (dsc.cond_prio == src.cond_prio)
                {
                    goto Label_03C1;
                }
                return (src.cond_prio - dsc.cond_prio);
            Label_03C1:
                if (dsc.fail_num == src.fail_num)
                {
                    goto Label_0435;
                }
                return (dsc.fail_num - src.fail_num);
                goto Label_0435;
            Label_03E5:
                if (src.skill.EffectType != 13)
                {
                    goto Label_0435;
                }
                if (dsc.cond_prio == src.cond_prio)
                {
                    goto Label_0416;
                }
                return (src.cond_prio - dsc.cond_prio);
            Label_0416:
                if (dsc.disable_num == src.disable_num)
                {
                    goto Label_0435;
                }
                return (dsc.disable_num - src.disable_num);
            Label_0435:
                if (src.teleport == dsc.teleport)
                {
                    goto Label_0454;
                }
                return (dsc.teleport - src.teleport);
            Label_0454:
                if (dsc.good_trick == src.good_trick)
                {
                    goto Label_0473;
                }
                return (dsc.good_trick - src.good_trick);
            Label_0473:
                if (dsc.cost_jewel == src.cost_jewel)
                {
                    goto Label_0492;
                }
                return (src.cost_jewel - dsc.cost_jewel);
            Label_0492:
                if (src.skill != dsc.skill)
                {
                    goto Label_0525;
                }
                if (src.skill.IsCastSkill() == null)
                {
                    goto Label_0525;
                }
                if (src.skill.IsAreaSkill() == null)
                {
                    goto Label_0525;
                }
                if (src.skill.IsEnableUnitLockTarget() == null)
                {
                    goto Label_0525;
                }
                if (src.skill.Target == 3)
                {
                    goto Label_0525;
                }
                if (src.skill.Target == 4)
                {
                    goto Label_0525;
                }
                if (dsc.locked == null)
                {
                    goto Label_050D;
                }
                if (src.locked != null)
                {
                    goto Label_050D;
                }
                return 1;
            Label_050D:
                if (src.locked == null)
                {
                    goto Label_0525;
                }
                if (dsc.locked != null)
                {
                    goto Label_0525;
                }
                return -1;
            Label_0525:
                if (dsc.ct == src.ct)
                {
                    goto Label_0544;
                }
                return (dsc.ct - src.ct);
            Label_0544:
                if (dsc.distance != src.distance)
                {
                    goto Label_05D2;
                }
                if (this.bPositioning == null)
                {
                    goto Label_0593;
                }
                if (dsc.movpos.height == src.movpos.height)
                {
                    goto Label_0593;
                }
                return (dsc.movpos.height - src.movpos.height);
            Label_0593:
                if (src.skill.IsNormalAttack() == null)
                {
                    goto Label_05D2;
                }
                if (dsc.skill.IsNormalAttack() == null)
                {
                    goto Label_05D2;
                }
                if (dsc.gain_jewel == src.gain_jewel)
                {
                    goto Label_05D2;
                }
                return (dsc.gain_jewel - src.gain_jewel);
            Label_05D2:
                return (dsc.distance - src.distance);
            }
        }

        [CompilerGenerated]
        private sealed class <UnitEnd>c__AnonStorey1BA
        {
            internal Unit unit;

            public <UnitEnd>c__AnonStorey1BA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__61(Unit p)
            {
                return (p == this.unit.UnitTarget);
            }
        }

        [CompilerGenerated]
        private sealed class <UnitStartAsync>c__Iterator2A : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Unit <unit>__0;
            internal int <interval>__1;
            internal int <i>__2;
            internal int $PC;
            internal object $current;
            internal BattleCore <>f__this;

            public <UnitStartAsync>c__Iterator2A()
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
                        goto Label_0247;
                }
                goto Label_041F;
            Label_0021:
                this.<>f__this.DebugAssert(this.<>f__this.IsInitialized, "初期化済みのみコール可");
                this.<>f__this.DebugAssert(this.<>f__this.IsBattleFlag(1), "マップ開始済みのみコール可");
                this.<>f__this.DebugAssert(this.<>f__this.IsBattleFlag(2) == 0, "ユニット未開始のみコール可");
                this.<unit>__0 = this.<>f__this.CurrentUnit;
                this.<>f__this.IsAutoBattle = this.<>f__this.RequestAutoBattle;
                if (this.<>f__this.CheckEnableSuspendSave() == null)
                {
                    goto Label_010B;
                }
                if (this.<unit>__0.IsPartyMember == null)
                {
                    goto Label_010B;
                }
                this.<interval>__1 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.SuspendSaveInterval;
                if (this.<interval>__1 == null)
                {
                    goto Label_00FF;
                }
                if ((this.<>f__this.mActionCount % this.<interval>__1) != null)
                {
                    goto Label_010B;
                }
            Label_00FF:
                this.<>f__this.SaveSuspendData();
            Label_010B:
                this.<>f__this.mSeedDamage = this.<>f__this.mRand.Get();
                this.<>f__this.CurrentRand = this.<>f__this.mRand;
                this.<>f__this.ClearAI();
                TrickData.ActionEffect(1, this.<unit>__0, this.<unit>__0.x, this.<unit>__0.y, this.<>f__this.CurrentRand, null);
                this.<>f__this.UpdateUnitDyingTurn();
                this.<>f__this.UpdateUnitJudgeHPTurn();
                this.<unit>__0.NotifyActionStart(this.<>f__this.Units);
                this.<>f__this.UpdateSkillUseCondition();
                this.<>f__this.UpdateUnitCondition(this.<unit>__0);
                this.<>f__this.AutoHealSkill(this.<unit>__0);
                this.<>f__this.ActuatedSneaking(this.<unit>__0);
                this.<>f__this.UpdateHelperUnits(this.<unit>__0);
                this.<>f__this.UpdateTrickMap(this.<unit>__0);
                this.<>f__this.UpdateMoveMap(this.<unit>__0);
                this.<>f__this.UpdateSafeMap(this.<unit>__0);
                this.<>f__this.InitSkillMap(this.<unit>__0);
                this.$current = null;
                this.$PC = 1;
                goto Label_0421;
            Label_0247:
                this.<>f__this.mKillstreak = 0;
                this.<>f__this.mTargetKillstreakDict.Clear();
                this.<>f__this.SetBattleFlag(2, 1);
                this.<>f__this.SetBattleFlag(3, 1);
                if (this.<unit>__0.IsUnitCondition(0x800L) == null)
                {
                    goto Label_0378;
                }
                if (this.<unit>__0.DeathCount > 0)
                {
                    goto Label_0378;
                }
                this.<unit>__0.CurrentStatus.param.hp = 0;
                this.<i>__2 = 0;
                goto Label_0334;
            Label_02CB:
                if (this.<unit>__0.CondAttachments[this.<i>__2].ContainsCondition(0x800L) != null)
                {
                    goto Label_02F6;
                }
                goto Label_0326;
            Label_02F6:
                if (this.<>f__this.TrySetBattleFinisher(this.<unit>__0.CondAttachments[this.<i>__2].user) == null)
                {
                    goto Label_0326;
                }
                goto Label_034F;
            Label_0326:
                this.<i>__2 += 1;
            Label_0334:
                if (this.<i>__2 < this.<unit>__0.CondAttachments.Count)
                {
                    goto Label_02CB;
                }
            Label_034F:
                this.<>f__this.Dead(this.<unit>__0, this.<unit>__0, 2, 0);
                this.<>f__this.InternalLogUnitEnd();
                goto Label_041F;
            Label_0378:
                if (this.<unit>__0.IsPartyMember == null)
                {
                    goto Label_039B;
                }
                this.<>f__this.mActionCount += 1;
            Label_039B:
                this.<>f__this.Log<LogMapCommand>();
                objArray1 = new object[] { "CTが溜まってないのに行動が行われた。【現在】", (OInt) this.<unit>__0.ChargeTime, " /【最大】", (OInt) this.<unit>__0.ChargeTimeMax };
                this.<>f__this.DebugAssert((this.<unit>__0.ChargeTimeMax > this.<unit>__0.ChargeTime) == 0, string.Concat(objArray1));
                this.$PC = -1;
            Label_041F:
                return 0;
            Label_0421:
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
        private sealed class <UpdateMapAI_Impl>c__AnonStorey1BC
        {
            internal Unit self;
            internal bool forceAI;
            internal BattleCore <>f__this;

            public <UpdateMapAI_Impl>c__AnonStorey1BC()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateMapAI_Impl>c__AnonStorey1BD
        {
            internal BattleCore.SkillResult result;
            internal BattleCore.<UpdateMapAI_Impl>c__AnonStorey1BC <>f__ref$444;
            internal BattleCore <>f__this;

            public <UpdateMapAI_Impl>c__AnonStorey1BD()
            {
                base..ctor();
                return;
            }

            internal bool <>m__64(List<BattleCore.SkillResult> results)
            {
                this.<>f__this.SortSkillResult(this.<>f__ref$444.self, results);
                this.result = results[0];
                if (this.<>f__this.UseSkillAI(this.<>f__ref$444.self, this.result, this.<>f__ref$444.forceAI) == null)
                {
                    goto Label_007E;
                }
                this.<>f__this.mSkillMap.SetUseSkill(this.result);
                this.<>f__this.mSkillMap.NextAction(this.result);
                return 1;
            Label_007E:
                return 0;
            }
        }

        public class AiCache
        {
            public BattleMap map;
            public FixParam fixparam;
            public BattleCore.SVector2 pos;
            public int cond_prio;
            public int cost_jewel;
            public Grid goal;
            public GridMap<bool> baseRangeMap;

            public AiCache()
            {
                base..ctor();
                return;
            }
        }

        public class ChainUnit
        {
            public Unit self;
            public List<BattleCore.HitData> hits;

            public ChainUnit()
            {
                this.hits = new List<BattleCore.HitData>(4);
                base..ctor();
                return;
            }
        }

        public class CommandResult
        {
            public Unit self;
            public SkillData skill;
            public BattleCore.UnitResult self_effect;
            public List<BattleCore.UnitResult> targets;
            public List<BattleCore.UnitResult> reactions;

            public CommandResult()
            {
                base..ctor();
                return;
            }
        }

        public class DropItemParam
        {
            private ItemParam mItemParam;
            private ConceptCardParam mConceptCardParam;
            public bool mIsSecret;

            public DropItemParam(ConceptCardParam ccp)
            {
                base..ctor();
                this.mConceptCardParam = ccp;
                return;
            }

            public DropItemParam(ItemParam ip)
            {
                base..ctor();
                this.mItemParam = ip;
                return;
            }

            public string Name
            {
                get
                {
                    if (this.IsItem == null)
                    {
                        goto Label_0017;
                    }
                    return this.mItemParam.name;
                Label_0017:
                    if (this.IsConceptCard == null)
                    {
                        goto Label_002E;
                    }
                    return this.mConceptCardParam.name;
                Label_002E:
                    return string.Empty;
                }
            }

            public string Iname
            {
                get
                {
                    if (this.IsItem == null)
                    {
                        goto Label_0017;
                    }
                    return this.mItemParam.iname;
                Label_0017:
                    if (this.IsConceptCard == null)
                    {
                        goto Label_002E;
                    }
                    return this.mConceptCardParam.iname;
                Label_002E:
                    return string.Empty;
                }
            }

            public bool IsItem
            {
                get
                {
                    return ((this.mItemParam == null) == 0);
                }
            }

            public bool IsConceptCard
            {
                get
                {
                    return ((this.mConceptCardParam == null) == 0);
                }
            }

            public ItemParam itemParam
            {
                get
                {
                    return this.mItemParam;
                }
            }

            public ConceptCardParam conceptCardParam
            {
                get
                {
                    return this.mConceptCardParam;
                }
            }
        }

        public enum eArenaCalcType
        {
            UNKNOWN,
            MAP_START,
            UNIT_START,
            AI,
            UNIT_END,
            CAST_SKILL_START,
            CAST_SKILL_END,
            MAP_END
        }

        public class HitData
        {
            public int hp_damage;
            public int mp_damage;
            public int ch_damage;
            public int ca_damage;
            public int hp_heal;
            public int mp_heal;
            public int ch_heal;
            public int ca_heal;
            public bool is_critical;
            public bool is_avoid;
            public bool is_pf_avoid;
            public int critical_rate;
            public int avoid_rate;

            public HitData(int _hp_damage_, int _mp_damage_, int _ch_damage_, int _ca_damage_, int _hp_heal_, int _mp_heal_, int _ch_heal_, int _ca_heal_, bool _critical_, bool _avoid_, bool _pf_avoid_, int _critical_rate_, int _avoid_rate_)
            {
                base..ctor();
                this.hp_damage = _hp_damage_;
                this.mp_damage = _mp_damage_;
                this.ch_damage = _ch_damage_;
                this.ca_damage = _ca_damage_;
                this.hp_heal = _hp_heal_;
                this.mp_heal = _mp_heal_;
                this.ch_heal = _ch_heal_;
                this.ca_heal = _ca_heal_;
                this.is_critical = _critical_;
                this.is_avoid = _avoid_;
                this.is_pf_avoid = _pf_avoid_;
                this.critical_rate = _critical_rate_;
                this.avoid_rate = _avoid_rate_;
                return;
            }
        }

        public class Json_Battle
        {
            public long btlid;
            public BattleCore.Json_BtlInfo btlinfo;
            public Json_Unit[] coloenemyunits;
            public int is_rehash;

            public Json_Battle()
            {
                base..ctor();
                return;
            }
        }

        public class Json_BattleCont
        {
            public long btlid;
            public BattleCore.Json_BtlInfo btlinfo;
            public Json_PlayerData player;

            public Json_BattleCont()
            {
                base..ctor();
                return;
            }
        }

        public class Json_BtlDrop
        {
            public string iname;
            public string itype;
            public int gold;
            public int num;
            public int secret;
            [CompilerGenerated]
            private static Dictionary<string, int> <>f__switch$map7;

            public Json_BtlDrop()
            {
                base..ctor();
                return;
            }

            public EBattleRewardType dropItemType
            {
                get
                {
                    string str;
                    Dictionary<string, int> dictionary;
                    int num;
                    if (string.IsNullOrEmpty(this.iname) == null)
                    {
                        goto Label_0012;
                    }
                    return 0;
                Label_0012:
                    str = this.itype;
                    if (str == null)
                    {
                        goto Label_0076;
                    }
                    if (<>f__switch$map7 != null)
                    {
                        goto Label_004E;
                    }
                    dictionary = new Dictionary<string, int>(2);
                    dictionary.Add("item", 0);
                    dictionary.Add("card", 1);
                    <>f__switch$map7 = dictionary;
                Label_004E:
                    if (<>f__switch$map7.TryGetValue(str, &num) == null)
                    {
                        goto Label_0076;
                    }
                    if (num == null)
                    {
                        goto Label_0072;
                    }
                    if (num == 1)
                    {
                        goto Label_0074;
                    }
                    goto Label_0076;
                Label_0072:
                    return 2;
                Label_0074:
                    return 3;
                Label_0076:
                    return 1;
                }
            }
        }

        public class Json_BtlInfo
        {
            public string qid;
            public BattleCore.Json_BtlUnit[] units;
            public Json_Support help;
            public BattleCore.Json_BtlDrop[] drops;
            public BattleCore.Json_BtlSteal[] steals;
            public BattleCore.Json_BtlReward[] rewards;
            public string key;
            public int seed;
            public int[] atkmags;
            public string[] campaigns;
            public long start_at;
            public int multi_floor;
            public int roomid;
            public BattleCore.Json_BtlInfoRankingQuest quest_ranking;
            public BattleCore.Json_BtlOrdeal[] ordeals;
            public BattleCore.JSON_RankMatchRank myRank;
            public BattleCore.JSON_RankMatchRank enemyRank;
            public RandDeckResult[] lot_enemies;

            public Json_BtlInfo()
            {
                base..ctor();
                return;
            }

            public virtual RandDeckResult[] GetDeck()
            {
                return null;
            }
        }

        public class Json_BtlInfoRankingQuest
        {
            public int type;
            public int schedule_id;

            public Json_BtlInfoRankingQuest()
            {
                base..ctor();
                return;
            }
        }

        public class Json_BtlOrdeal
        {
            public BattleCore.Json_BtlUnit[] units;
            public Json_Support help;

            public Json_BtlOrdeal()
            {
                base..ctor();
                return;
            }
        }

        public class Json_BtlReward
        {
            public int gold;

            public Json_BtlReward()
            {
                base..ctor();
                return;
            }
        }

        public class Json_BtlSteal
        {
            public string iname;
            public int gold;
            public int num;

            public Json_BtlSteal()
            {
                base..ctor();
                return;
            }
        }

        public class Json_BtlUnit
        {
            public int iid;

            public Json_BtlUnit()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_RankMatchRank
        {
            public int score;
            public int type;

            public JSON_RankMatchRank()
            {
                base..ctor();
                return;
            }
        }

        private class KnockBackTarget
        {
            public LogSkill.Target mLsTarget;
            public int mUnitGx;
            public int mUnitGy;
            public int mMoveLen;
            public int mMoveDir;

            public KnockBackTarget(LogSkill.Target ls_target, int gx, int gy)
            {
                this.mMoveDir = -1;
                base..ctor();
                this.mLsTarget = ls_target;
                this.mUnitGx = gx;
                this.mUnitGy = gy;
                this.mMoveLen = 0;
                this.mMoveDir = -1;
                return;
            }
        }

        public delegate void LogCallback(string s);

        public class MoveGoalTarget
        {
            public Unit unit;
            public Vector2 goal;
            public float step;

            public MoveGoalTarget()
            {
                base..ctor();
                return;
            }

            public static List<BattleCore.MoveGoalTarget> Create(List<Unit> targets)
            {
                List<BattleCore.MoveGoalTarget> list;
                int num;
                BattleCore.MoveGoalTarget target;
                list = new List<BattleCore.MoveGoalTarget>();
                num = 0;
                goto Label_0036;
            Label_000D:
                target = new BattleCore.MoveGoalTarget();
                target.unit = targets[num];
                target.goal = Vector2.get_zero();
                list.Add(target);
                num += 1;
            Label_0036:
                if (num < targets.Count)
                {
                    goto Label_000D;
                }
                return list;
            }

            public override unsafe string ToString()
            {
                object[] objArray1;
                objArray1 = new object[] { "[", (float) &this.goal.x, ",", (float) &this.goal.y, "](", (float) this.step, "):", this.unit.UnitName };
                return string.Concat(objArray1);
            }
        }

        public class OrderData
        {
            public SRPG.Unit Unit;
            public bool IsCastSkill;
            public bool IsCharged;

            public OrderData()
            {
                base..ctor();
                return;
            }

            public bool CheckChargeTimeFullOver()
            {
                return ((this.IsCastSkill == null) ? this.Unit.CheckChargeTimeFullOver() : this.Unit.CheckCastTimeFullOver());
            }

            public OInt GetChargeSpeed()
            {
                return ((this.IsCastSkill == null) ? this.Unit.GetChargeSpeed() : this.Unit.GetCastSpeed());
            }

            public OInt GetChargeTime()
            {
                return ((this.IsCastSkill == null) ? this.Unit.ChargeTime : this.Unit.CastTime);
            }

            public OInt GetChargeTimeMax()
            {
                return ((this.IsCastSkill == null) ? this.Unit.ChargeTimeMax : this.Unit.CastTimeMax);
            }

            public bool UpdateChargeTime()
            {
                if (this.IsCastSkill == null)
                {
                    goto Label_0017;
                }
                return this.Unit.UpdateCastTime();
            Label_0017:
                return this.Unit.UpdateChargeTime();
            }
        }

        public enum QuestResult
        {
            Pending,
            Win,
            Lose,
            Retreat,
            Draw
        }

        public class Record
        {
            public BattleCore.QuestResult result;
            public OInt playerexp;
            public OInt unitexp;
            public OInt gold;
            public OInt chain;
            public OInt multicoin;
            public OInt pvpcoin;
            public List<UnitParam> units;
            public List<BattleCore.DropItemParam> items;
            public List<ArtifactParam> artifacts;
            public int bonusFlags;
            public int allBonusFlags;
            public int bonusCount;
            public List<int> takeoverProgressList;
            public OInt[] drops;
            public OInt[] item_steals;
            public OInt[] gold_steals;
            public Dictionary<OString, OInt> used_items;

            public Record()
            {
                this.playerexp = 0;
                this.unitexp = 0;
                this.gold = 0;
                this.chain = 0;
                this.multicoin = 0;
                this.pvpcoin = 0;
                this.units = new List<UnitParam>(4);
                this.items = new List<BattleCore.DropItemParam>(4);
                this.artifacts = new List<ArtifactParam>(4);
                this.takeoverProgressList = new List<int>();
                this.used_items = new Dictionary<OString, OInt>();
                base..ctor();
                return;
            }

            public bool IsMissionClearAll(bool onlyCurrentBattle)
            {
                int num;
                bool flag;
                int num2;
                num = 0;
                if (onlyCurrentBattle == null)
                {
                    goto Label_0014;
                }
                num = this.bonusFlags;
                goto Label_001B;
            Label_0014:
                num = this.allBonusFlags;
            Label_001B:
                flag = 1;
                num2 = 0;
                goto Label_0037;
            Label_0024:
                if ((num & (1 << (num2 & 0x1f))) == null)
                {
                    goto Label_0033;
                }
                flag = 0;
            Label_0033:
                num2 += 1;
            Label_0037:
                if (num2 < this.bonusCount)
                {
                    goto Label_0024;
                }
                return flag;
            }

            public bool IsZero
            {
                get
                {
                    if (this.gold == null)
                    {
                        goto Label_0012;
                    }
                    return 0;
                Label_0012:
                    if (this.playerexp == null)
                    {
                        goto Label_0024;
                    }
                    return 0;
                Label_0024:
                    if (this.unitexp == null)
                    {
                        goto Label_0036;
                    }
                    return 0;
                Label_0036:
                    if (this.items.Count <= 0)
                    {
                        goto Label_0049;
                    }
                    return 0;
                Label_0049:
                    if (this.multicoin == null)
                    {
                        goto Label_005B;
                    }
                    return 0;
                Label_005B:
                    return 1;
                }
            }
        }

        public enum RESUME_STATE
        {
            NONE,
            REQUEST,
            WAIT
        }

        private class ShotTarget
        {
            public List<Unit> piercers;
            public Grid end;
            public double rad;
            public double height;

            public ShotTarget()
            {
                this.piercers = new List<Unit>();
                base..ctor();
                return;
            }
        }

        public class SkillExecLog
        {
            public string skill_iname;
            public int use_count;
            public int kill_count;

            public SkillExecLog()
            {
                base..ctor();
                return;
            }

            public void Restore(BattleSuspend.Data.SkillExecLogInfo _log_info)
            {
                this.skill_iname = _log_info.inm;
                this.use_count = _log_info.ucnt;
                this.kill_count = _log_info.kcnt;
                return;
            }
        }

        public class SkillResult
        {
            public SkillData skill;
            public Grid movpos;
            public Grid usepos;
            public LogSkill log;
            public bool locked;
            public int score_prio;
            public int unit_prio;
            public int cost_jewel;
            public int gain_jewel;
            public int heal;
            public int heal_num;
            public int cure_num;
            public int fail_num;
            public int disable_num;
            public int cond_prio;
            public int buff;
            public int buff_num;
            public int buff_prio;
            public int buff_dup;
            public int unit_damage_t;
            public int unit_damage;
            public int unit_dead_num;
            public int ext_damage;
            public int ext_dead_num;
            public int nockback_prio;
            public int distance;
            public int teleport;
            public int ct;
            public int fail_trick;
            public int good_trick;
            public int heal_trick;

            public SkillResult()
            {
                base..ctor();
                return;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SVector2
        {
            public int x;
            public int y;
            public SVector2(int _x_, int _y_)
            {
                this.x = _x_;
                this.y = _y_;
                return;
            }

            public unsafe SVector2(BattleCore.SVector2 v)
            {
                this.x = &v.x;
                this.y = &v.y;
                return;
            }

            public int Length()
            {
                return BattleCore.Sqrt((this.x * this.x) + (this.y * this.y));
            }

            public static int DotProduct(ref BattleCore.SVector2 s, ref BattleCore.SVector2 t)
            {
                return ((s.x * t.x) + (s.y * t.y));
            }

            public override bool Equals(object obj)
            {
                if ((obj as BattleCore.SVector2) == null)
                {
                    goto Label_001D;
                }
                return (*(this) == ((BattleCore.SVector2) obj));
            Label_001D:
                return 0;
            }

            public override int GetHashCode()
            {
                return 0;
            }

            public static unsafe BattleCore.SVector2 operator +(BattleCore.SVector2 a, BattleCore.SVector2 b)
            {
                return new BattleCore.SVector2(&a.x + &b.x, &a.y + &b.y);
            }

            public static unsafe BattleCore.SVector2 operator -(BattleCore.SVector2 a, BattleCore.SVector2 b)
            {
                return new BattleCore.SVector2(&a.x - &b.x, &a.y - &b.y);
            }

            public static unsafe BattleCore.SVector2 operator *(BattleCore.SVector2 a, BattleCore.SVector2 b)
            {
                return new BattleCore.SVector2(&a.x * &b.x, &a.y * &b.y);
            }

            public static unsafe BattleCore.SVector2 operator *(BattleCore.SVector2 a, int mul)
            {
                return new BattleCore.SVector2(&a.x * mul, &a.y * mul);
            }

            public static unsafe bool operator ==(BattleCore.SVector2 a, BattleCore.SVector2 b)
            {
                return ((&a.x != &b.x) ? 0 : (&a.y == &b.y));
            }

            public static unsafe bool operator !=(BattleCore.SVector2 a, BattleCore.SVector2 b)
            {
                return ((&a.x != &b.x) ? 1 : ((&a.y == &b.y) == 0));
            }
        }

        public class UnitResult
        {
            public Unit react_unit;
            public Unit unit;
            public int hp_damage;
            public int mp_damage;
            public int hp_heal;
            public int mp_heal;
            public int avoid;
            public int critical;
            public int reaction;
            public List<LogSkill.Target.CondHit> cond_hit_lists;

            public UnitResult()
            {
                this.cond_hit_lists = new List<LogSkill.Target.CondHit>();
                base..ctor();
                return;
            }
        }
    }
}

