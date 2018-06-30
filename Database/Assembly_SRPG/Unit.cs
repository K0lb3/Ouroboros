namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class Unit : BaseObject
    {
        public const int DIRECTION_MAX = 4;
        private static string[] mStrNameUnitConds;
        private static string[] mStrDescUnitConds;
        public static readonly int MAX_AI;
        public static OInt MAX_UNIT_CONDITION;
        private static OInt UNIT_INDEX;
        public static OInt UNIT_CAST_INDEX;
        public static readonly int[,] DIRECTION_OFFSETS;
        public static EUnitDirection[] ReverseDirection;
        private string mUnitName;
        private string mUniqueName;
        private SRPG.UnitData mUnitData;
        private EUnitSide mSide;
        private OInt mUnitFlag;
        private OInt mCommandFlag;
        private OIntVector2 mGridPosition;
        private OIntVector2 mGridPositionTurnStart;
        private OInt mTurnStartDir;
        private NPCSetting mSettingNPC;
        private OInt mUnitIndex;
        private OInt mSearched;
        private BaseStatus mMaximumStatus;
        private int mMaximumStatusHp;
        private BaseStatus mCurrentStatus;
        private int mUnitChangedHp;
        private List<AIParam> mAI;
        private OInt mAITop;
        private AIActionTable mAIActionTable;
        private OInt mAIActionIndex;
        private OInt mAIActionTurnCount;
        private AIPatrolTable mAIPatrolTable;
        private OInt mAIPatrolIndex;
        private SkillData mAIForceSkill;
        private MapBreakObj mBreakObj;
        private string mCreateBreakObjId;
        private int mCreateBreakObjClock;
        private int mTeamId;
        private SRPG.FriendStates mFriendStates;
        private OInt mKeepHp;
        private BaseStatus mMaximumStatusWithMap;
        private List<BuffAttachment> mBuffAttachments;
        private List<CondAttachment> mCondAttachments;
        private OLong mCurrentCondition;
        private OLong mDisableCondition;
        public Unit Target;
        public Grid TreasureGainTarget;
        public EUnitDirection Direction;
        public bool IsPartyMember;
        public bool IsSub;
        private UnitDrop mDrop;
        private UnitSteal mSteal;
        private List<UnitShield> mShields;
        private List<UnitMhmDamage> mMhmDamageLists;
        private SRPG.EventTrigger mEventTrigger;
        private List<UnitEntryTrigger> mEntryTriggers;
        private OBool mEntryTriggerAndCheck;
        private OInt mWaitEntryClock;
        private OInt mMoveWaitTurn;
        private OInt mActionCount;
        private OInt mDeathCount;
        private OInt mAutoJewel;
        private OInt mChargeTime;
        private SkillData mCastSkill;
        private OInt mCastTime;
        private OInt mCastIndex;
        private Unit mUnitTarget;
        private Grid mGridTarget;
        private GridMap<bool> mCastSkillGridMap;
        private Unit mRageTarget;
        private Unit mGuardTarget;
        private OInt mGuardTurn;
        private Dictionary<SkillData, OInt> mSkillUseCount;
        private List<SkillData> mJudgeHpLists;
        private string mParentUniqueName;
        private int mTowerStartHP;
        public List<OString> mNotifyUniqueNames;
        private int mKillCount;
        private bool mDropDirection;
        private List<AbilityChange> mAbilityChangeLists;
        private List<AbilityData> mBattleAbilitys;
        private List<SkillData> mBattleSkills;
        private List<AbilityData> mAddedAbilitys;
        private List<SkillData> mAddedSkills;
        public int OwnerPlayerIndex;
        private int mTurnCount;
        private bool mEntryUnit;
        private static uint mCondLinkageID;
        public BuffBit CondLinkageBuff;
        public BuffBit CondLinkageDebuff;
        private SkillData mPushCastSkill;
        private MySound.Voice mBattleVoice;
        private static BaseStatus BuffWorkStatus;
        private static BaseStatus BuffWorkScaleStatus;
        private static BaseStatus DebuffWorkScaleStatus;
        private static BaseStatus PassiveWorkScaleStatus;
        private static BaseStatus BuffDupliScaleStatus;
        private static BaseStatus DebuffDupliScaleStatus;
        private static BaseStatus BuffConceptCardStatus;
        private static BaseStatus DebuffConceptCardStatus;
        private static BaseStatus BuffConceptCardScaleStatus;
        private static BaseStatus DebuffConceptCardScaleStatus;
        private static BaseStatus DupliConceptCardStatus;
        [CompilerGenerated]
        private bool <ReqRevive>k__BackingField;
        [CompilerGenerated]
        private static Comparison<UnitShield> <>f__am$cache66;
        [CompilerGenerated]
        private static Comparison<AbilityData> <>f__am$cache67;

        static Unit()
        {
            EUnitDirection[] directionArray1;
            string[] textArray2;
            string[] textArray1;
            BuffWorkStatus = new BaseStatus();
            BuffWorkScaleStatus = new BaseStatus();
            DebuffWorkScaleStatus = new BaseStatus();
            PassiveWorkScaleStatus = new BaseStatus();
            BuffDupliScaleStatus = new BaseStatus();
            DebuffDupliScaleStatus = new BaseStatus();
            BuffConceptCardStatus = new BaseStatus();
            DebuffConceptCardStatus = new BaseStatus();
            BuffConceptCardScaleStatus = new BaseStatus();
            DebuffConceptCardScaleStatus = new BaseStatus();
            DupliConceptCardStatus = new BaseStatus();
            textArray1 = new string[] { 
                "quest.BUD_COND_POISON", "quest.BUD_COND_PARALYSED", "quest.BUD_COND_STUN", "quest.BUD_COND_SLEEP", "quest.BUD_COND_CHARM", "quest.BUD_COND_STONE", "quest.BUD_COND_BLINDNESS", "quest.BUD_COND_DISABLESKILL", "quest.BUD_COND_DISABLEMOVE", "quest.BUD_COND_DISABLEATTACK", "quest.BUD_COND_ZOMBIE", "quest.BUD_COND_DEATHSENTENCE", "quest.BUD_COND_BERSERK", "quest.BUD_COND_DISABLEKNOCKBACK", "quest.BUD_COND_DISABLEBUFF", "quest.BUD_COND_DISABLEDEBUFF",
                "quest.BUD_COND_STOP", "quest.BUD_COND_FAST", "quest.BUD_COND_SLOW", "quest.BUD_COND_AUTOHEAL", "quest.BUD_COND_DONSOKU", "quest.BUD_COND_RAGE", "quest.BUD_COND_GOODSLEEP", "quest.BUD_COND_AUTOJEWEL", "quest.BUD_COND_DISABLEHEAL", "quest.BUD_COND_DISABLESINGLEATTACK", "quest.BUD_COND_DISABLEAREAATTACK", "quest.BUD_COND_DISABLEDECCT", "quest.BUD_COND_DISABLEINCCT", "quest.BUD_COND_DISABLEESAFIRE", "quest.BUD_COND_DISABLEESAWATER", "quest.BUD_COND_DISABLEESAWIND",
                "quest.BUD_COND_DISABLEESATHUNDER", "quest.BUD_COND_DISABLEESASHINE", "quest.BUD_COND_DISABLEESADARK", "quest.BUD_COND_DISABLEMAXDAMAGEHP", "quest.BUD_COND_DISABLEMAXDAMAGEMP", "quest.BUD_COND_SHIELD"
            };
            mStrNameUnitConds = textArray1;
            textArray2 = new string[] { 
                "quest.BUD_COND_DESC_POISON", "quest.BUD_COND_DESC_PARALYSED", "quest.BUD_COND_DESC_STUN", "quest.BUD_COND_DESC_SLEEP", "quest.BUD_COND_DESC_CHARM", "quest.BUD_COND_DESC_STONE", "quest.BUD_COND_DESC_BLINDNESS", "quest.BUD_COND_DESC_DISABLESKILL", "quest.BUD_COND_DESC_DISABLEMOVE", "quest.BUD_COND_DESC_DISABLEATTACK", "quest.BUD_COND_DESC_ZOMBIE", "quest.BUD_COND_DESC_DEATHSENTENCE", "quest.BUD_COND_DESC_BERSERK", "quest.BUD_COND_DESC_DISABLEKNOCKBACK", "quest.BUD_COND_DESC_DISABLEBUFF", "quest.BUD_COND_DESC_DISABLEDEBUFF",
                "quest.BUD_COND_DESC_STOP", "quest.BUD_COND_DESC_FAST", "quest.BUD_COND_DESC_SLOW", "quest.BUD_COND_DESC_AUTOHEAL", "quest.BUD_COND_DESC_DONSOKU", "quest.BUD_COND_DESC_RAGE", "quest.BUD_COND_DESC_GOODSLEEP", "quest.BUD_COND_DESC_AUTOJEWEL", "quest.BUD_COND_DESC_DISABLEHEAL", "quest.BUD_COND_DESC_DISABLESINGLEATTACK", "quest.BUD_COND_DESC_DISABLEAREAATTACK", "quest.BUD_COND_DESC_DISABLEDECCT", "quest.BUD_COND_DESC_DISABLEINCCT", "quest.BUD_COND_DESC_DISABLEESAFIRE", "quest.BUD_COND_DESC_DISABLEESAWATER", "quest.BUD_COND_DESC_DISABLEESAWIND",
                "quest.BUD_COND_DESC_DISABLEESATHUNDER", "quest.BUD_COND_DESC_DISABLEESASHINE", "quest.BUD_COND_DESC_DISABLEESADARK", "quest.BUD_COND_DESC_DISABLEMAXDAMAGEHP", "quest.BUD_COND_DESC_DISABLEMAXDAMAGEMP", "quest.BUD_COND_DESC_SHIELD"
            };
            mStrDescUnitConds = textArray2;
            MAX_AI = 2;
            MAX_UNIT_CONDITION = (int) Enum.GetNames(typeof(EUnitCondition)).Length;
            UNIT_INDEX = 0;
            UNIT_CAST_INDEX = 0;
            DIRECTION_OFFSETS = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
            directionArray1 = new EUnitDirection[5];
            directionArray1[0] = 2;
            directionArray1[1] = 3;
            directionArray1[3] = 1;
            directionArray1[4] = 2;
            ReverseDirection = directionArray1;
            mCondLinkageID = 0;
            return;
        }

        public Unit()
        {
            OIntVector2 vector;
            OIntVector2 vector2;
            this.mUnitFlag = 0;
            this.mCommandFlag = 0;
            this.mGridPosition = new OIntVector2();
            this.mGridPositionTurnStart = new OIntVector2();
            this.mSearched = 0;
            this.mMaximumStatus = new BaseStatus();
            this.mCurrentStatus = new BaseStatus();
            this.mAI = new List<AIParam>(MAX_AI);
            this.mAITop = 0;
            this.mAIActionTable = new AIActionTable();
            this.mAIActionIndex = 0;
            this.mAIActionTurnCount = 0;
            this.mAIPatrolTable = new AIPatrolTable();
            this.mAIPatrolIndex = 0;
            this.mMaximumStatusWithMap = new BaseStatus();
            this.mBuffAttachments = new List<BuffAttachment>(8);
            this.mCondAttachments = new List<CondAttachment>(8);
            this.mDrop = new UnitDrop();
            this.mSteal = new UnitSteal();
            this.mShields = new List<UnitShield>();
            this.mMhmDamageLists = new List<UnitMhmDamage>();
            this.mEntryTriggerAndCheck = 0;
            this.mWaitEntryClock = 0;
            this.mMoveWaitTurn = 0;
            this.mActionCount = 0;
            this.mDeathCount = 0;
            this.mAutoJewel = 0;
            this.mSkillUseCount = new Dictionary<SkillData, OInt>();
            this.mJudgeHpLists = new List<SkillData>();
            this.mAbilityChangeLists = new List<AbilityChange>();
            this.mAddedAbilitys = new List<AbilityData>();
            this.mAddedSkills = new List<SkillData>();
            this.CondLinkageBuff = new BuffBit();
            this.CondLinkageDebuff = new BuffBit();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <AddShieldSuspend>m__4A7(UnitShield src, UnitShield dsc)
        {
            if (src.shieldType == dsc.shieldType)
            {
                goto Label_0041;
            }
            if (src.shieldType != 3)
            {
                goto Label_001F;
            }
            return -1;
        Label_001F:
            if (dsc.shieldType != 3)
            {
                goto Label_002D;
            }
            return 1;
        Label_002D:
            return ((src.shieldType != 1) ? 1 : -1);
        Label_0041:
            if (src.damageType == dsc.damageType)
            {
                goto Label_006E;
            }
            if (src.damageType != 1)
            {
                goto Label_0060;
            }
            return -1;
        Label_0060:
            if (dsc.damageType != 1)
            {
                goto Label_006E;
            }
            return 1;
        Label_006E:
            return 0;
        }

        [CompilerGenerated]
        private static int <RefleshBattleAbilitysAndSkills>m__4A9(AbilityData ad1, AbilityData ad2)
        {
            if (ad1.Param == null)
            {
                goto Label_00BA;
            }
            if (ad2.Param == null)
            {
                goto Label_00BA;
            }
            if (ad1.Param.is_fixed == ad2.Param.is_fixed)
            {
                goto Label_0055;
            }
            if (ad1.Param.is_fixed == null)
            {
                goto Label_0043;
            }
            return -1;
        Label_0043:
            if (ad2.Param.is_fixed == null)
            {
                goto Label_0055;
            }
            return 1;
        Label_0055:
            if (ad1.Param.slot == ad2.Param.slot)
            {
                goto Label_00BA;
            }
            if (ad1.Param.slot != null)
            {
                goto Label_0082;
            }
            return -1;
        Label_0082:
            if (ad2.Param.slot != null)
            {
                goto Label_0094;
            }
            return 1;
        Label_0094:
            if (ad1.Param.slot != 2)
            {
                goto Label_00A7;
            }
            return -1;
        Label_00A7:
            if (ad2.Param.slot != 2)
            {
                goto Label_00BA;
            }
            return 1;
        Label_00BA:
            return 0;
        }

        private void AbsorbAndGiveExchangeBuff(Unit self, Unit target, SkillData skill, BuffEffect buff_effect, List<Unit> aag_unit_lists, BaseStatus buff_add, BaseStatus buff_scl, BaseStatus debuff_add, BaseStatus debuff_scl)
        {
            eAbsorbAndGive give;
            bool flag;
            bool flag2;
            BaseStatus status;
            int num;
            Unit unit;
            BaseStatus status2;
            BaseStatus status3;
            if (self == null)
            {
                goto Label_0020;
            }
            if (target == null)
            {
                goto Label_0020;
            }
            if (skill == null)
            {
                goto Label_0020;
            }
            if (buff_effect == null)
            {
                goto Label_0020;
            }
            if (aag_unit_lists != null)
            {
                goto Label_0021;
            }
        Label_0020:
            return;
        Label_0021:
            give = skill.SkillParam.AbsorbAndGive;
            if (give != null)
            {
                goto Label_0034;
            }
            return;
        Label_0034:
            flag = buff_effect.CheckBuffCalcType(0, 1);
            flag2 = buff_effect.CheckBuffCalcType(1, 1);
            if (SkillParam.IsAagTypeGive(give) == null)
            {
                goto Label_011D;
            }
            status = self.UnitData.Status;
            if (self == target)
            {
                goto Label_007B;
            }
            if (SkillParam.IsAagTypeSame(give) != null)
            {
                goto Label_007B;
            }
            buff_add.Swap(debuff_add, 0);
        Label_007B:
            if (flag == null)
            {
                goto Label_00AC;
            }
            if (self == target)
            {
                goto Label_0093;
            }
            if (SkillParam.IsAagTypeSame(give) == null)
            {
                goto Label_00A2;
            }
        Label_0093:
            buff_add.AddConvRate(buff_scl, status);
            goto Label_00AC;
        Label_00A2:
            debuff_add.SubConvRate(buff_scl, status);
        Label_00AC:
            if (flag2 == null)
            {
                goto Label_00DD;
            }
            if (self == target)
            {
                goto Label_00C4;
            }
            if (SkillParam.IsAagTypeSame(give) == null)
            {
                goto Label_00D3;
            }
        Label_00C4:
            debuff_add.AddConvRate(debuff_scl, status);
            goto Label_00DD;
        Label_00D3:
            buff_add.SubConvRate(debuff_scl, status);
        Label_00DD:
            if (SkillParam.IsAagTypeDiv(give) == null)
            {
                goto Label_01E2;
            }
            if (aag_unit_lists.Count <= 1)
            {
                goto Label_01E2;
            }
            if (self == target)
            {
                goto Label_01E2;
            }
            buff_add.Div(aag_unit_lists.Count);
            debuff_add.Div(aag_unit_lists.Count);
            goto Label_01E2;
        Label_011D:
            if (self != target)
            {
                goto Label_01B3;
            }
            buff_add.Swap(debuff_add, 0);
            if (aag_unit_lists.Count <= 1)
            {
                goto Label_0157;
            }
            buff_add.Mul(aag_unit_lists.Count);
            debuff_add.Mul(aag_unit_lists.Count);
        Label_0157:
            num = 0;
            goto Label_01A0;
        Label_015F:
            unit = aag_unit_lists[num];
            status2 = unit.UnitData.Status;
            if (flag == null)
            {
                goto Label_0189;
            }
            debuff_add.SubConvRate(buff_scl, status2);
        Label_0189:
            if (flag2 == null)
            {
                goto Label_019A;
            }
            buff_add.SubConvRate(debuff_scl, status2);
        Label_019A:
            num += 1;
        Label_01A0:
            if (num < aag_unit_lists.Count)
            {
                goto Label_015F;
            }
            goto Label_01E2;
        Label_01B3:
            status3 = target.UnitData.Status;
            if (flag == null)
            {
                goto Label_01D1;
            }
            buff_add.AddConvRate(buff_scl, status3);
        Label_01D1:
            if (flag2 == null)
            {
                goto Label_01E2;
            }
            debuff_add.AddConvRate(debuff_scl, status3);
        Label_01E2:
            return;
        }

        public void AddJudgeHpLists(SkillData skill)
        {
            if (skill != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            this.mJudgeHpLists.Add(skill);
            return;
        }

        public void AddMhmDamage(eTypeMhmDamage type, int damage)
        {
            this.mMhmDamageLists.Add(new UnitMhmDamage(type, damage));
            return;
        }

        public void AddShield(SkillData skill)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (skill == null)
            {
                goto Label_001D;
            }
            if (skill.EffectType != 8)
            {
                goto Label_001D;
            }
            if (skill.ShieldType != null)
            {
                goto Label_001E;
            }
        Label_001D:
            return;
        Label_001E:
            if (this.IsBreakObj == null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            num = skill.ShieldValue;
            if (num != null)
            {
                goto Label_003D;
            }
            return;
        Label_003D:
            num2 = skill.ShieldTurn;
            if (num2 > 0)
            {
                goto Label_0052;
            }
            num2 = -1;
        Label_0052:
            num3 = skill.ControlDamageRate;
            num4 = skill.ControlDamageValue;
            this.AddShieldSuspend(skill.SkillParam, num, num, num2, num2, num3, num4);
            return;
        }

        private void AddShieldSuspend(SkillParam skill_param, int hp, int hp_max, int turn, int turn_max, int damage_rate, int damage_value)
        {
            UnitShield shield;
            int num;
            if (skill_param != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            shield = new UnitShield();
            shield.shieldType = skill_param.shield_type;
            shield.damageType = skill_param.shield_damage_type;
            shield.hp = hp;
            shield.hpMax = hp_max;
            shield.turn = turn;
            shield.turnMax = turn_max;
            shield.damage_rate = damage_rate;
            shield.damage_value = damage_value;
            shield.skill_param = skill_param;
            num = 0;
            goto Label_00D0;
        Label_007F:
            if (this.mShields[num].shieldType != shield.shieldType)
            {
                goto Label_00CC;
            }
            if (this.mShields[num].damageType != shield.damageType)
            {
                goto Label_00CC;
            }
            this.mShields.RemoveAt(num--);
        Label_00CC:
            num += 1;
        Label_00D0:
            if (num < this.mShields.Count)
            {
                goto Label_007F;
            }
            this.mShields.Add(shield);
            if (<>f__am$cache66 != null)
            {
                goto Label_010B;
            }
            <>f__am$cache66 = new Comparison<UnitShield>(Unit.<AddShieldSuspend>m__4A7);
        Label_010B:
            MySort<UnitShield>.Sort(this.mShields, <>f__am$cache66);
            return;
        }

        public void AddSkillUseCount(AbilityData ad)
        {
            int num;
            SkillData data;
            if (ad == null)
            {
                goto Label_0011;
            }
            if (ad.Skills != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            num = 0;
            goto Label_006E;
        Label_0019:
            data = ad.Skills[num];
            if (data.SkillParam.count > 0)
            {
                goto Label_0041;
            }
            goto Label_006A;
        Label_0041:
            if (this.mSkillUseCount.ContainsKey(data) == null)
            {
                goto Label_0057;
            }
            goto Label_006A;
        Label_0057:
            this.mSkillUseCount.Add(data, this.GetSkillUseCountMax(data));
        Label_006A:
            num += 1;
        Label_006E:
            if (num < ad.Skills.Count)
            {
                goto Label_0019;
            }
            return;
        }

        public void BeginDropDirection()
        {
            this.mDropDirection = 1;
            return;
        }

        private bool CalcBuffStatus(BuffAttachment buff, int enemy_dead_count)
        {
            SkillData data;
            BaseStatus status;
            int num;
            int num2;
            int num3;
            bool flag;
            bool flag2;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            BuffAttachment attachment;
            ESkillCondition condition;
            SkillParamCalcTypes types;
            if (buff != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (buff.IsCalculated == null)
            {
                goto Label_0015;
            }
            return 0;
        Label_0015:
            data = buff.skill;
            status = new BaseStatus(buff.status);
            if (((buff.IsPassive != null) || ((data != null) && (data.IsPassiveSkill() != null))) || (((buff.Param != null) && (buff.Param.IsNoDisabled != null)) || (this.IsEnableBuffEffect(buff.BuffType) != null)))
            {
                goto Label_0077;
            }
            return 0;
        Label_0077:
            if (buff.UseCondition == null)
            {
                goto Label_00D6;
            }
            condition = buff.UseCondition;
            if (condition == 1)
            {
                goto Label_009F;
            }
            if (condition == 5)
            {
                goto Label_00B1;
            }
            goto Label_00D6;
        Label_009F:
            if (this.IsDying() != null)
            {
                goto Label_00D6;
            }
            return 0;
            goto Label_00D6;
        Label_00B1:
            if (buff.skill != null)
            {
                goto Label_00BE;
            }
            return 0;
        Label_00BE:
            if (buff.skill.IsJudgeHp(this) != null)
            {
                goto Label_00D6;
            }
            return 0;
        Label_00D6:
            if (buff.Param == null)
            {
                goto Label_01C2;
            }
            num = 0;
            if (buff.Param.mAppType != 1)
            {
                goto Label_0115;
            }
            if (enemy_dead_count != null)
            {
                goto Label_00FC;
            }
            return 0;
        Label_00FC:
            num = Math.Min(enemy_dead_count, buff.Param.mAppMct) - 1;
            goto Label_014C;
        Label_0115:
            if (buff.Param.mAppType != 2)
            {
                goto Label_014C;
            }
            if (this.mKillCount != null)
            {
                goto Label_0133;
            }
            return 0;
        Label_0133:
            num = Math.Min(this.mKillCount, buff.Param.mAppMct) - 1;
        Label_014C:
            if ((buff.Param.mEffRange != 1) || (buff.Param.mAppMct <= 0))
            {
                goto Label_0199;
            }
            num2 = this.GetAllyUnitNum(buff.user);
            if (num2 != null)
            {
                goto Label_0183;
            }
            return 0;
        Label_0183:
            num += Math.Min(num2, buff.Param.mAppMct) - 1;
        Label_0199:
            if (num <= 0)
            {
                goto Label_01C2;
            }
            num3 = 0;
            goto Label_01BA;
        Label_01A8:
            status.Add(buff.status);
            num3 += 1;
        Label_01BA:
            if (num3 < num)
            {
                goto Label_01A8;
            }
        Label_01C2:
            flag = (data == null) ? 0 : (data.Condition == 4);
            flag2 = buff.BuffType == 0;
            if (flag == null)
            {
                goto Label_01FA;
            }
            if (buff.IsNegativeValueIsBuff == null)
            {
                goto Label_01FA;
            }
            flag2 = flag2 == 0;
        Label_01FA:
            num4 = this.GetBuffAttachmentDuplicateCount(buff);
            if (flag == null)
            {
                goto Label_0228;
            }
            if (buff.skill == null)
            {
                goto Label_0228;
            }
            if (buff.skill.IsPassiveSkill() == null)
            {
                goto Label_0228;
            }
            num4 = 1;
        Label_0228:
            if (num4 <= 1)
            {
                goto Label_0484;
            }
            switch (buff.CalcType)
            {
                case 0:
                    goto Label_0250;

                case 1:
                    goto Label_02D9;

                case 2:
                    goto Label_0250;
            }
            goto Label_043A;
        Label_0250:
            if (flag == null)
            {
                goto Label_02B2;
            }
            DupliConceptCardStatus.Clear();
            num5 = 0;
            goto Label_027A;
        Label_0269:
            DupliConceptCardStatus.Add(status);
            num5 += 1;
        Label_027A:
            if (num5 < num4)
            {
                goto Label_0269;
            }
            if (flag2 == null)
            {
                goto Label_029E;
            }
            BuffConceptCardStatus.ReplaceHighest(DupliConceptCardStatus);
            goto Label_02AD;
        Label_029E:
            DebuffConceptCardStatus.ReplaceLowest(DupliConceptCardStatus);
        Label_02AD:
            goto Label_02D4;
        Label_02B2:
            num6 = 0;
            goto Label_02CB;
        Label_02BA:
            BuffWorkStatus.Add(status);
            num6 += 1;
        Label_02CB:
            if (num6 < num4)
            {
                goto Label_02BA;
            }
        Label_02D4:
            goto Label_043A;
        Label_02D9:
            if (buff.skill == null)
            {
                goto Label_037D;
            }
            if (buff.skill.IsPassiveSkill() == null)
            {
                goto Label_037D;
            }
            if (flag == null)
            {
                goto Label_0356;
            }
            DupliConceptCardStatus.Clear();
            num7 = 0;
            goto Label_031E;
        Label_030D:
            DupliConceptCardStatus.Add(status);
            num7 += 1;
        Label_031E:
            if (num7 < num4)
            {
                goto Label_030D;
            }
            if (flag2 == null)
            {
                goto Label_0342;
            }
            BuffConceptCardScaleStatus.ReplaceHighest(DupliConceptCardStatus);
            goto Label_0351;
        Label_0342:
            DebuffConceptCardScaleStatus.ReplaceLowest(DupliConceptCardStatus);
        Label_0351:
            goto Label_0378;
        Label_0356:
            num8 = 0;
            goto Label_036F;
        Label_035E:
            PassiveWorkScaleStatus.Add(status);
            num8 += 1;
        Label_036F:
            if (num8 < num4)
            {
                goto Label_035E;
            }
        Label_0378:
            goto Label_0435;
        Label_037D:
            if (flag2 == null)
            {
                goto Label_03DF;
            }
            BuffDupliScaleStatus.Clear();
            num9 = 0;
            goto Label_03A7;
        Label_0396:
            BuffDupliScaleStatus.Add(status);
            num9 += 1;
        Label_03A7:
            if (num9 < num4)
            {
                goto Label_0396;
            }
            if (flag == null)
            {
                goto Label_03CB;
            }
            BuffConceptCardScaleStatus.ReplaceHighest(BuffDupliScaleStatus);
            goto Label_03DA;
        Label_03CB:
            BuffWorkScaleStatus.ReplaceHighest(BuffDupliScaleStatus);
        Label_03DA:
            goto Label_0435;
        Label_03DF:
            DebuffDupliScaleStatus.Clear();
            num10 = 0;
            goto Label_0402;
        Label_03F1:
            DebuffDupliScaleStatus.Add(status);
            num10 += 1;
        Label_0402:
            if (num10 < num4)
            {
                goto Label_03F1;
            }
            if (flag == null)
            {
                goto Label_0426;
            }
            DebuffConceptCardScaleStatus.ReplaceLowest(DebuffDupliScaleStatus);
            goto Label_0435;
        Label_0426:
            DebuffWorkScaleStatus.ReplaceLowest(DebuffDupliScaleStatus);
        Label_0435:;
        Label_043A:
            num11 = 0;
            goto Label_046D;
        Label_0442:
            attachment = this.BuffAttachments[num11];
            if (this.isSameBuffAttachment(attachment, buff) == null)
            {
                goto Label_0467;
            }
            attachment.IsCalculated = 1;
        Label_0467:
            num11 += 1;
        Label_046D:
            if (num11 < this.BuffAttachments.Count)
            {
                goto Label_0442;
            }
            goto Label_0595;
        Label_0484:
            switch (buff.CalcType)
            {
                case 0:
                    goto Label_04A4;

                case 1:
                    goto Label_04E2;

                case 2:
                    goto Label_0590;
            }
            goto Label_0595;
        Label_04A4:
            if (flag == null)
            {
                goto Label_04D2;
            }
            if (flag2 == null)
            {
                goto Label_04C2;
            }
            BuffConceptCardStatus.ReplaceHighest(status);
            goto Label_04CD;
        Label_04C2:
            DebuffConceptCardStatus.ReplaceLowest(status);
        Label_04CD:
            goto Label_04DD;
        Label_04D2:
            BuffWorkStatus.Add(status);
        Label_04DD:
            goto Label_0595;
        Label_04E2:
            if (buff.skill == null)
            {
                goto Label_053B;
            }
            if (buff.skill.IsPassiveSkill() == null)
            {
                goto Label_053B;
            }
            if (flag == null)
            {
                goto Label_052B;
            }
            if (flag2 == null)
            {
                goto Label_051B;
            }
            BuffConceptCardScaleStatus.ReplaceHighest(status);
            goto Label_0526;
        Label_051B:
            DebuffConceptCardScaleStatus.ReplaceLowest(status);
        Label_0526:
            goto Label_0536;
        Label_052B:
            PassiveWorkScaleStatus.Add(status);
        Label_0536:
            goto Label_058B;
        Label_053B:
            if (flag2 == null)
            {
                goto Label_0569;
            }
            if (flag == null)
            {
                goto Label_0559;
            }
            BuffConceptCardScaleStatus.ReplaceHighest(status);
            goto Label_0564;
        Label_0559:
            BuffWorkScaleStatus.ReplaceHighest(status);
        Label_0564:
            goto Label_058B;
        Label_0569:
            if (flag == null)
            {
                goto Label_0580;
            }
            DebuffConceptCardScaleStatus.ReplaceLowest(status);
            goto Label_058B;
        Label_0580:
            DebuffWorkScaleStatus.ReplaceLowest(status);
        Label_058B:
            goto Label_0595;
        Label_0590:;
        Label_0595:
            return flag;
        }

        public void CalcCurrentStatus(bool is_initialized, bool is_predict)
        {
            int num;
            int num2;
            int num3;
            SceneBattle battle;
            int num4;
            bool flag;
            int num5;
            BuffAttachment attachment;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            int num6;
            BuffAttachment attachment2;
            int num7;
            FixParam param;
            int num8;
            int num9;
            bool flag2;
            int num10;
            CondAttachment attachment3;
            int num11;
            int num12;
            CondEffect effect;
            int num13;
            int num14;
            CondEffect effect2;
            int num15;
            int num16;
            int num17;
            int num18;
            FixParam param2;
            int num19;
            int num20;
            bool flag3;
            int num21;
            CondAttachment attachment4;
            int num22;
            int num23;
            CondEffect effect3;
            int num24;
            int num25;
            CondEffect effect4;
            int num26;
            int num27;
            int num28;
            int num29;
            int num30;
            BaseStatus status6;
            BattleBonus bonus;
            OInt num31;
            BaseStatus status7;
            BaseStatus status8;
            StatusTypes types;
            BaseStatus status9;
            BaseStatus status10;
            BaseStatus status11;
            num = this.CurrentStatus.param.hp;
            num2 = this.CurrentStatus.param.mp;
            BuffWorkStatus.Clear();
            BuffWorkScaleStatus.Clear();
            DebuffWorkScaleStatus.Clear();
            PassiveWorkScaleStatus.Clear();
            BuffDupliScaleStatus.Clear();
            DebuffDupliScaleStatus.Clear();
            BuffConceptCardStatus.Clear();
            DebuffConceptCardStatus.Clear();
            BuffConceptCardScaleStatus.Clear();
            DebuffConceptCardScaleStatus.Clear();
            DupliConceptCardStatus.Clear();
            this.mAutoJewel = 0;
            this.mMaximumStatusWithMap.CopyTo(this.MaximumStatus);
            num3 = 0;
            battle = SceneBattle.Instance;
            if ((battle == null) || (battle.Battle == null))
            {
                goto Label_00E1;
            }
            num3 = battle.Battle.GetDeadCountEnemy();
        Label_00E1:
            num4 = 0;
            goto Label_0102;
        Label_00E9:
            this.BuffAttachments[num4].IsCalculated = 0;
            num4 += 1;
        Label_0102:
            if (num4 < this.BuffAttachments.Count)
            {
                goto Label_00E9;
            }
            flag = 0;
            num5 = 0;
            goto Label_0153;
        Label_011F:
            attachment = this.BuffAttachments[num5];
            if (attachment.IsCalcLaterCondition == null)
            {
                goto Label_013F;
            }
            goto Label_014D;
        Label_013F:
            flag |= this.CalcBuffStatus(attachment, num3);
        Label_014D:
            num5 += 1;
        Label_0153:
            if (num5 < this.BuffAttachments.Count)
            {
                goto Label_011F;
            }
            status = new BaseStatus();
            status.Add(BuffWorkScaleStatus);
            status.Add(DebuffWorkScaleStatus);
            status.Add(PassiveWorkScaleStatus);
            if (flag == null)
            {
                goto Label_022D;
            }
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            status5 = new BaseStatus();
            BuffConceptCardStatus.CopyTo(status2);
            DebuffConceptCardStatus.CopyTo(status3);
            BuffConceptCardScaleStatus.CopyTo(status4);
            DebuffConceptCardScaleStatus.CopyTo(status5);
            status2.ChoiceHighest(status4, this.MaximumStatus);
            status3.ChoiceLowest(status5, this.MaximumStatus);
            status.Add(status4);
            status.Add(status5);
            this.MaximumStatus.Add(status2);
            this.MaximumStatus.Add(status3);
        Label_022D:
            this.MaximumStatus.Add(BuffWorkStatus);
            this.MaximumStatus.AddRate(status);
            num6 = 0;
            goto Label_0286;
        Label_0252:
            attachment2 = this.BuffAttachments[num6];
            if (attachment2.IsCalcLaterCondition != null)
            {
                goto Label_0272;
            }
            goto Label_0280;
        Label_0272:
            flag |= this.CalcBuffStatus(attachment2, num3);
        Label_0280:
            num6 += 1;
        Label_0286:
            if (num6 < this.BuffAttachments.Count)
            {
                goto Label_0252;
            }
            this.mMaximumStatusWithMap.CopyTo(this.MaximumStatus);
            num7 = 0;
            goto Label_02CA;
        Label_02B1:
            this.BuffAttachments[num7].IsCalculated = 0;
            num7 += 1;
        Label_02CA:
            if (num7 < this.BuffAttachments.Count)
            {
                goto Label_02B1;
            }
            if (this.IsUnitCondition(0x40L) == null)
            {
                goto Label_0683;
            }
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            num8 = 0;
            num9 = 0;
            flag2 = 0;
            num10 = 0;
            goto Label_05F8;
        Label_030C:
            attachment3 = this.CondAttachments[num10];
            if ((attachment3 == null) || (attachment3.IsFailCondition() == null))
            {
                goto Label_05F2;
            }
            if (attachment3.ContainsCondition(0x40L) != null)
            {
                goto Label_0342;
            }
            goto Label_05F2;
        Label_0342:
            num11 = 0;
            num12 = 0;
            if (attachment3.skill == null)
            {
                goto Label_04C8;
            }
            effect = attachment3.skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_0402;
            }
            num13 = (effect.param.v_blink_hit == null) ? param.BlindnessHitRate : effect.param.v_blink_hit;
            num14 = (effect.param.v_blink_avo == null) ? param.BlindnessAvoidRate : effect.param.v_blink_avo;
            if ((Math.Abs(num11) + Math.Abs(num12)) >= (Math.Abs(num13) + Math.Abs(num14)))
            {
                goto Label_03FF;
            }
            num11 = num13;
            num12 = num14;
        Label_03FF:
            flag2 = 1;
        Label_0402:
            effect2 = (attachment3.user != this) ? null : attachment3.skill.GetCondEffect(1);
            if (effect2 == null)
            {
                goto Label_05C7;
            }
            num15 = (effect2.param.v_blink_hit == null) ? param.BlindnessHitRate : effect2.param.v_blink_hit;
            num16 = (effect2.param.v_blink_avo == null) ? param.BlindnessAvoidRate : effect2.param.v_blink_avo;
            if ((Math.Abs(num11) + Math.Abs(num12)) >= (Math.Abs(num15) + Math.Abs(num16)))
            {
                goto Label_04C0;
            }
            num11 = num15;
            num12 = num16;
        Label_04C0:
            flag2 = 1;
            goto Label_05C7;
        Label_04C8:
            if (attachment3.Param == null)
            {
                goto Label_0571;
            }
            num17 = (attachment3.Param.v_blink_hit == null) ? param.BlindnessHitRate : attachment3.Param.v_blink_hit;
            num18 = (attachment3.Param.v_blink_avo == null) ? param.BlindnessAvoidRate : attachment3.Param.v_blink_avo;
            if ((Math.Abs(num11) + Math.Abs(num12)) >= (Math.Abs(num17) + Math.Abs(num18)))
            {
                goto Label_0569;
            }
            num11 = num17;
            num12 = num18;
        Label_0569:
            flag2 = 1;
            goto Label_05C7;
        Label_0571:
            if ((Math.Abs(num11) + Math.Abs(num12)) >= (Math.Abs(param.BlindnessHitRate) + Math.Abs(param.BlindnessAvoidRate)))
            {
                goto Label_05C4;
            }
            num11 = param.BlindnessHitRate;
            num12 = param.BlindnessAvoidRate;
        Label_05C4:
            flag2 = 1;
        Label_05C7:
            if ((Math.Abs(num8) + Math.Abs(num9)) >= (Math.Abs(num11) + Math.Abs(num12)))
            {
                goto Label_05F2;
            }
            num8 = num11;
            num9 = num12;
        Label_05F2:
            num10 += 1;
        Label_05F8:
            if (num10 < this.CondAttachments.Count)
            {
                goto Label_030C;
            }
            if (flag2 != null)
            {
                goto Label_062D;
            }
            num8 = param.BlindnessHitRate;
            num9 = param.BlindnessAvoidRate;
        Label_062D:
            num31 = status6[bonus];
            (status6 = BuffWorkStatus)[bonus = 3] = num31 + num8;
            num31 = status7[bonus];
            (status7 = BuffWorkStatus)[bonus = 4] = num31 + num9;
        Label_0683:
            if (this.IsUnitCondition(0x1000L) == null)
            {
                goto Label_0A7D;
            }
            param2 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            num19 = 0;
            num20 = 0;
            flag3 = 0;
            num21 = 0;
            goto Label_099C;
        Label_06B6:
            attachment4 = this.CondAttachments[num21];
            if ((attachment4 == null) || (attachment4.IsFailCondition() == null))
            {
                goto Label_0996;
            }
            if (attachment4.ContainsCondition(0x1000L) != null)
            {
                goto Label_06EF;
            }
            goto Label_0996;
        Label_06EF:
            num22 = 0;
            num23 = 0;
            if (attachment4.skill == null)
            {
                goto Label_086F;
            }
            effect3 = attachment4.skill.GetCondEffect(0);
            if (effect3 == null)
            {
                goto Label_07AC;
            }
            num24 = (effect3.param.v_berserk_atk == null) ? param2.BerserkAtkRate : effect3.param.v_berserk_atk;
            num25 = (effect3.param.v_berserk_def == null) ? param2.BerserkDefRate : effect3.param.v_berserk_def;
            if ((Math.Abs(num22) + Math.Abs(num23)) >= (Math.Abs(num24) + Math.Abs(num25)))
            {
                goto Label_07AC;
            }
            num22 = num24;
            num23 = num25;
        Label_07AC:
            effect4 = (attachment4.user != this) ? null : attachment4.skill.GetCondEffect(1);
            if (effect4 == null)
            {
                goto Label_0968;
            }
            num26 = (effect4.param.v_berserk_atk == null) ? param2.BerserkAtkRate : effect4.param.v_berserk_atk;
            num27 = (effect4.param.v_berserk_def == null) ? param2.BerserkDefRate : effect4.param.v_berserk_def;
            if ((Math.Abs(num22) + Math.Abs(num23)) >= (Math.Abs(num26) + Math.Abs(num27)))
            {
                goto Label_0968;
            }
            num22 = num26;
            num23 = num27;
            goto Label_0968;
        Label_086F:
            if (attachment4.Param == null)
            {
                goto Label_0915;
            }
            num28 = (attachment4.Param.v_berserk_atk == null) ? param2.BerserkAtkRate : attachment4.Param.v_berserk_atk;
            num29 = (attachment4.Param.v_berserk_def == null) ? param2.BerserkDefRate : attachment4.Param.v_berserk_def;
            if ((Math.Abs(num22) + Math.Abs(num23)) >= (Math.Abs(num28) + Math.Abs(num29)))
            {
                goto Label_0968;
            }
            num22 = num28;
            num23 = num29;
            goto Label_0968;
        Label_0915:
            if ((Math.Abs(num22) + Math.Abs(num23)) >= (Math.Abs(param2.BerserkAtkRate) + Math.Abs(param2.BerserkDefRate)))
            {
                goto Label_0968;
            }
            num22 = param2.BerserkAtkRate;
            num23 = param2.BerserkDefRate;
        Label_0968:
            if ((Math.Abs(num19) + Math.Abs(num20)) >= (Math.Abs(num22) + Math.Abs(num23)))
            {
                goto Label_0993;
            }
            num19 = num22;
            num20 = num23;
        Label_0993:
            flag3 = 1;
        Label_0996:
            num21 += 1;
        Label_099C:
            if (num21 < this.CondAttachments.Count)
            {
                goto Label_06B6;
            }
            if (flag3 != null)
            {
                goto Label_09D1;
            }
            num19 = param2.BerserkAtkRate;
            num20 = param2.BerserkDefRate;
        Label_09D1:
            num31 = status8[types];
            (status8 = PassiveWorkScaleStatus)[types = 3] = num31 + num19;
            num31 = status9[types];
            (status9 = PassiveWorkScaleStatus)[types = 5] = num31 + num19;
            num31 = status10[types];
            (status10 = PassiveWorkScaleStatus)[types = 4] = num31 + num20;
            num31 = status11[types];
            (status11 = PassiveWorkScaleStatus)[types = 6] = num31 + num20;
        Label_0A7D:
            BuffWorkScaleStatus.Add(DebuffWorkScaleStatus);
            BuffWorkScaleStatus.Add(PassiveWorkScaleStatus);
            if (flag == null)
            {
                goto Label_0B0A;
            }
            BuffConceptCardStatus.ChoiceHighest(BuffConceptCardScaleStatus, this.MaximumStatus);
            DebuffConceptCardStatus.ChoiceLowest(DebuffConceptCardScaleStatus, this.MaximumStatus);
            BuffWorkScaleStatus.Add(BuffConceptCardScaleStatus);
            BuffWorkScaleStatus.Add(DebuffConceptCardScaleStatus);
            this.MaximumStatus.Add(BuffConceptCardStatus);
            this.MaximumStatus.Add(DebuffConceptCardStatus);
        Label_0B0A:
            this.MaximumStatus.Add(BuffWorkStatus);
            this.MaximumStatus.AddRate(BuffWorkScaleStatus);
            this.MaximumStatus.param.ApplyMinVal();
            this.MaximumStatus.CopyTo(this.CurrentStatus);
            this.mAutoJewel = this.CurrentStatus[0x11];
            if (is_initialized == null)
            {
                goto Label_0B84;
            }
            this.CurrentStatus.param.mp = this.GetStartGems();
            goto Label_0C1B;
        Label_0B84:
            if (is_predict == null)
            {
                goto Label_0BBB;
            }
            this.CurrentStatus.param.hp = num;
            this.CurrentStatus.param.mp = num2;
            goto Label_0C1B;
        Label_0BBB:
            this.CurrentStatus.param.hp = Math.Min(num, this.MaximumStatus.param.hp);
            this.CurrentStatus.param.mp = Math.Min(num2, this.MaximumStatus.param.mp);
        Label_0C1B:
            this.mMaximumStatusHp = this.mMaximumStatus.param.hp;
            this.ReflectMhmDamage();
            num30 = 0;
            goto Label_0C99;
        Label_0C44:
            if (this.mShields[num30].skill_param.IsShieldReset() == null)
            {
                goto Label_0C65;
            }
            goto Label_0C93;
        Label_0C65:
            if (this.mShields[num30].hp != null)
            {
                goto Label_0C93;
            }
            this.mShields.RemoveAt(num30--);
        Label_0C93:
            num30 += 1;
        Label_0C99:
            if (num30 < this.mShields.Count)
            {
                goto Label_0C44;
            }
            if (this.mRageTarget == null)
            {
                goto Label_0CD4;
            }
            if (this.mRageTarget.IsDeadCondition() == null)
            {
                goto Label_0CD4;
            }
            this.CureCondEffects(0x200000L, 1, 1);
        Label_0CD4:
            return;
        }

        public int CalcParamRecover(int val)
        {
            if (this.CurrentStatus.param.rec > 0)
            {
                goto Label_001D;
            }
            return 0;
        Label_001D:
            return ((val * this.CurrentStatus.param.rec) / 100);
        }

        public unsafe void CalcShieldDamage(DamageTypes damageType, ref int damage, bool bEnableShieldBreak, AttackDetailTypes attack_detail_type, RandXorshift rand, SRPG.LogSkill.Target log_target)
        {
            int num;
            UnitShield shield;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            if (*(((int*) damage)) > 0)
            {
                goto Label_0009;
            }
            return;
        Label_0009:
            num = 0;
            goto Label_0225;
        Label_0010:
            shield = this.mShields[num];
            if (shield.hp > 0)
            {
                goto Label_0033;
            }
            goto Label_0221;
        Label_0033:
            if (shield.damageType == damageType)
            {
                goto Label_0044;
            }
            goto Label_0221;
        Label_0044:
            if (shield.skill_param.IsReactionDet(attack_detail_type) != null)
            {
                goto Label_005B;
            }
            goto Label_0221;
        Label_005B:
            num2 = shield.damage_rate;
            if (0 >= num2)
            {
                goto Label_008D;
            }
            if (num2 >= 100)
            {
                goto Label_008D;
            }
            num3 = rand.Get() % 100;
            if (num3 <= num2)
            {
                goto Label_008D;
            }
            goto Label_0221;
        Label_008D:
            if (shield.shieldType != 1)
            {
                goto Label_0119;
            }
            num4 = *((int*) damage);
            if (shield.damage_value == null)
            {
                goto Label_00E1;
            }
            num4 = Math.Min(Math.Max(*(((int*) damage)) - this.CalcShieldEffectValue(shield.skill_param.control_damage_calc, shield.damage_value, *((int*) damage)), 0), *((int*) damage));
        Label_00E1:
            *((int*) damage) = Math.Max(*(((int*) damage)) - num4, 0);
            if (bEnableShieldBreak == null)
            {
                goto Label_0236;
            }
            shield.hp = OInt.op_Decrement(shield.hp);
            if (log_target == null)
            {
                goto Label_0236;
            }
            log_target.isProcShield = 1;
            goto Label_0236;
        Label_0119:
            if (shield.shieldType != 2)
            {
                goto Label_01DA;
            }
            num5 = 0;
            if (shield.damage_value == null)
            {
                goto Label_016C;
            }
            num5 = Math.Min(Math.Max(*(((int*) damage)) - this.CalcShieldEffectValue(shield.skill_param.control_damage_calc, shield.damage_value, *((int*) damage)), 0), *((int*) damage));
        Label_016C:
            num6 = *(((int*) damage)) - num5;
            num7 = shield.hp;
            num7 = Math.Max(shield.hp - num5, 0);
            *((int*) damage) = Math.Max(num5 - shield.hp, 0);
            *((int*) damage) += num6;
            if (bEnableShieldBreak == null)
            {
                goto Label_0236;
            }
            shield.hp = num7;
            if (log_target == null)
            {
                goto Label_0236;
            }
            log_target.isProcShield = 1;
            goto Label_0236;
        Label_01DA:
            if (shield.shieldType != 3)
            {
                goto Label_0221;
            }
            if (*(((int*) damage)) > shield.hp)
            {
                goto Label_0221;
            }
            *((int*) damage) = 0;
            if (bEnableShieldBreak == null)
            {
                goto Label_0236;
            }
            shield.is_efficacy = 1;
            if (log_target == null)
            {
                goto Label_0236;
            }
            log_target.isProcShield = 1;
            goto Label_0236;
        Label_0221:
            num += 1;
        Label_0225:
            if (num < this.mShields.Count)
            {
                goto Label_0010;
            }
        Label_0236:
            return;
        }

        private int CalcShieldEffectValue(SkillParamCalcTypes calctype, int skillval, int target)
        {
            int num;
            int num2;
            SkillParamCalcTypes types;
            types = calctype;
            switch (types)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_001D;

                case 2:
                    goto Label_0051;
            }
            goto Label_0051;
        Label_0019:
            return (target + skillval);
        Label_001D:
            num = skillval;
            if (num != 50)
            {
                goto Label_0030;
            }
            num += 1;
            goto Label_003C;
        Label_0030:
            if (num != -50)
            {
                goto Label_003C;
            }
            num -= 1;
        Label_003C:
            num2 = Mathf.RoundToInt((((float) target) * ((float) num)) / 100f);
            return (target + num2);
        Label_0051:;
        Label_0056:
            return skillval;
        }

        public int CalcTowerDamege()
        {
            if (this.IsUnitFlag(0x80000) == null)
            {
                goto Label_001E;
            }
            return (this.mTowerStartHP - this.mUnitChangedHp);
        Label_001E:
            return (this.mTowerStartHP - this.CurrentStatus.param.hp);
        }

        public void CancelCastSkill()
        {
            this.mCastSkill = null;
            this.mCastTime = 0;
            this.mCastIndex = 0;
            this.mUnitTarget = null;
            this.mGridTarget = null;
            this.mCastSkillGridMap = null;
            return;
        }

        public void CancelGuradTarget()
        {
            this.mGuardTarget = null;
            this.mGuardTurn = 0;
            return;
        }

        public bool CheckActionSkillBuffAttachments(BuffTypes type)
        {
            int num;
            num = 0;
            goto Label_0049;
        Label_0007:
            if (this.BuffAttachments[num].IsPassive == null)
            {
                goto Label_0027;
            }
            goto Label_0045;
        Label_0027:
            if (this.BuffAttachments[num].BuffType == type)
            {
                goto Label_0043;
            }
            goto Label_0045;
        Label_0043:
            return 1;
        Label_0045:
            num += 1;
        Label_0049:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public bool CheckAutoHpHeal()
        {
            int num;
            return (this.GetAutoHpHealValue() > 0);
        }

        public bool CheckAutoMpHeal()
        {
            int num;
            return (this.GetAutoMpHealValue() > 0);
        }

        public bool CheckCancelSkillCureCondition(EUnitCondition condition)
        {
            if (this.CastSkill != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if ((condition & 0x200000L) == null)
            {
                goto Label_002B;
            }
            if (this.IsUnitCondition(0x200000L) != null)
            {
                goto Label_0061;
            }
        Label_002B:
            if ((condition & 0x10L) == null)
            {
                goto Label_0043;
            }
            if (this.IsUnitCondition(0x10L) != null)
            {
                goto Label_0061;
            }
        Label_0043:
            if ((condition & 0x1000L) == null)
            {
                goto Label_0063;
            }
            if (this.IsUnitCondition(0x1000L) == null)
            {
                goto Label_0063;
            }
        Label_0061:
            return 1;
        Label_0063:
            return 0;
        }

        public bool CheckCancelSkillFailCondition(EUnitCondition condition)
        {
            if (this.CastSkill != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if ((condition & 4L) != null)
            {
                goto Label_005A;
            }
            if ((condition & 8L) != null)
            {
                goto Label_005A;
            }
            if ((condition & 0x20L) != null)
            {
                goto Label_005A;
            }
            if ((condition & 0x80L) != null)
            {
                goto Label_005A;
            }
            if ((condition & 0x200000L) != null)
            {
                goto Label_005A;
            }
            if ((condition & 0x10L) != null)
            {
                goto Label_005A;
            }
            if ((condition & 0x1000L) == null)
            {
                goto Label_005C;
            }
        Label_005A:
            return 1;
        Label_005C:
            if (this.CastSkill.IsDamagedSkill() == null)
            {
                goto Label_007B;
            }
            if ((condition & 0x200L) == null)
            {
                goto Label_007B;
            }
            return 1;
        Label_007B:
            return 0;
        }

        public bool CheckCastTimeFullOver()
        {
            if (this.IsUnitCondition(0x10000L) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            if (this.CastSkill != null)
            {
                goto Label_0020;
            }
            return 0;
        Label_0020:
            return ((this.mCastTime < this.CastTimeMax) == 0);
        }

        public bool CheckChargeTimeFullOver()
        {
            if (this.IsUnitCondition(0x10000L) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return ((this.mChargeTime < this.ChargeTimeMax) == 0);
        }

        public bool CheckCollision(Grid grid)
        {
            return this.CheckCollision(grid.x, grid.y, 1);
        }

        public bool CheckCollision(int cx, int cy, bool is_check_exist)
        {
            int num;
            int num2;
            if (is_check_exist == null)
            {
                goto Label_0013;
            }
            if (this.CheckExistMap() != null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            num = 0;
            goto Label_0058;
        Label_001A:
            num2 = 0;
            goto Label_0048;
        Label_0021:
            if ((this.x + num2) != cx)
            {
                goto Label_0044;
            }
            if ((this.y + num) == cy)
            {
                goto Label_0042;
            }
            goto Label_0044;
        Label_0042:
            return 1;
        Label_0044:
            num2 += 1;
        Label_0048:
            if (num2 < this.SizeX)
            {
                goto Label_0021;
            }
            num += 1;
        Label_0058:
            if (num < this.SizeY)
            {
                goto Label_001A;
            }
            return 0;
        }

        public bool CheckCollision(int x0, int y0, int x1, int y1)
        {
            if (this.CheckExistMap() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (x1 < this.x)
            {
                goto Label_002E;
            }
            if (((this.x + this.SizeX) - 1) >= x0)
            {
                goto Label_0030;
            }
        Label_002E:
            return 0;
        Label_0030:
            if (y1 < this.y)
            {
                goto Label_0052;
            }
            if (((this.y + this.SizeY) - 1) >= y0)
            {
                goto Label_0054;
            }
        Label_0052:
            return 0;
        Label_0054:
            return 1;
        }

        public bool CheckCollisionDirect(int tx, int ty, int cx, int cy, bool is_check_exist)
        {
            int num;
            int num2;
            if (is_check_exist == null)
            {
                goto Label_0014;
            }
            if (this.CheckExistMap() != null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            num = 0;
            goto Label_0050;
        Label_001B:
            num2 = 0;
            goto Label_0040;
        Label_0022:
            if ((tx + num2) != cx)
            {
                goto Label_003C;
            }
            if ((ty + num) == cy)
            {
                goto Label_003A;
            }
            goto Label_003C;
        Label_003A:
            return 1;
        Label_003C:
            num2 += 1;
        Label_0040:
            if (num2 < this.SizeX)
            {
                goto Label_0022;
            }
            num += 1;
        Label_0050:
            if (num < this.SizeY)
            {
                goto Label_001B;
            }
            return 0;
        }

        public bool CheckDamageActionStart()
        {
            if (this.IsUnitFlag(0x10000) == null)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            if (this.NotifyUniqueNames == null)
            {
                goto Label_0030;
            }
            if (this.NotifyUniqueNames.Count <= 0)
            {
                goto Label_0030;
            }
            return 1;
        Label_0030:
            return 0;
        }

        public bool CheckEnableCureCondition(EUnitCondition condition)
        {
            int num;
            CondAttachment attachment;
            if (this.IsUnitCondition(condition) != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            if (this.IsPassiveUnitCondition(condition) == null)
            {
                goto Label_001C;
            }
            return 0;
        Label_001C:
            num = 0;
            goto Label_0068;
        Label_0023:
            attachment = this.CondAttachments[num];
            if (attachment != null)
            {
                goto Label_003B;
            }
            goto Label_0064;
        Label_003B:
            if (attachment.IsFailCondition() == null)
            {
                goto Label_0064;
            }
            if (attachment.Condition == condition)
            {
                goto Label_0057;
            }
            goto Label_0064;
        Label_0057:
            if (attachment.IsCurse == null)
            {
                goto Label_0064;
            }
            return 0;
        Label_0064:
            num += 1;
        Label_0068:
            if (num < this.CondAttachments.Count)
            {
                goto Label_0023;
            }
            return 1;
        }

        public bool CheckEnableEntry()
        {
            bool flag;
            int num;
            UnitEntryTrigger trigger;
            if (this.IsUnitFlag(0x1000000) == null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            if (this.IsDead == null)
            {
                goto Label_001F;
            }
            return 0;
        Label_001F:
            if (((this.EntryUnit == null) || (this.IsUnitFlag(0x400000) != null)) || (this.IsSub == null))
            {
                goto Label_0047;
            }
            return 1;
        Label_0047:
            if ((this.IsSub == null) && (this.IsEntry == null))
            {
                goto Label_005F;
            }
            return 0;
        Label_005F:
            if (this.EntryUnit == null)
            {
                goto Label_006C;
            }
            return 1;
        Label_006C:
            if (this.mEntryTriggers == null)
            {
                goto Label_00E0;
            }
            flag = 1;
            num = 0;
            goto Label_00B7;
        Label_0080:
            trigger = this.mEntryTriggers[num];
            if ((this.IsEntryTriggerAndCheck != null) || (trigger.on == null))
            {
                goto Label_00AA;
            }
            return 1;
        Label_00AA:
            flag &= trigger.on;
            num += 1;
        Label_00B7:
            if (num < this.mEntryTriggers.Count)
            {
                goto Label_0080;
            }
            return ((this.IsEntryTriggerAndCheck == null) ? 0 : flag);
        Label_00E0:
            return (this.mWaitEntryClock == 0);
        }

        public bool CheckEnableFailCondition(EUnitCondition condition)
        {
            if (this.IsUnitCondition(condition) == null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            if (this.IsDisableUnitCondition(condition) == null)
            {
                goto Label_001C;
            }
            return 0;
        Label_001C:
            return 1;
        }

        public bool CheckEnableShieldType(DamageTypes type)
        {
            int num;
            num = 0;
            goto Label_0040;
        Label_0007:
            if (this.mShields[num].hp <= 0)
            {
                goto Label_003C;
            }
            if (this.mShields[num].damageType != type)
            {
                goto Label_003C;
            }
            return 1;
        Label_003C:
            num += 1;
        Label_0040:
            if (num < this.mShields.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public bool CheckEnableSkillUseCount(SkillData skill)
        {
            if (this.IsNPC == null)
            {
                goto Label_0018;
            }
            if (skill.CheckCount != null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            if (skill.SkillType == 1)
            {
                goto Label_0032;
            }
            if (skill.SkillType == 4)
            {
                goto Label_0032;
            }
            return 0;
        Label_0032:
            if (this.mSkillUseCount.ContainsKey(skill) != null)
            {
                goto Label_0045;
            }
            return 0;
        Label_0045:
            return 1;
        }

        public bool CheckEnableUseSkill(SkillData skill, bool bCheckCondOnly)
        {
            int num;
            int num2;
            SceneBattle battle;
            BattleCore core;
            ESkillType type;
            ESkillCondition condition;
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            switch (skill.SkillType)
            {
                case 0:
                    goto Label_002C;

                case 1:
                    goto Label_003F;

                case 2:
                    goto Label_0065;

                case 3:
                    goto Label_0052;
            }
            goto Label_0065;
        Label_002C:
            if (this.IsEnableAttackCondition(bCheckCondOnly) != null)
            {
                goto Label_0065;
            }
            return 0;
            goto Label_0065;
        Label_003F:
            if (this.IsEnableSkillCondition(bCheckCondOnly) != null)
            {
                goto Label_0065;
            }
            return 0;
            goto Label_0065;
        Label_0052:
            if (this.IsEnableItemCondition(bCheckCondOnly) != null)
            {
                goto Label_0065;
            }
            return 0;
        Label_0065:
            num = this.Gems - this.GetSkillUsedCost(skill);
            if (num >= 0)
            {
                goto Label_007D;
            }
            return 0;
        Label_007D:
            if (skill.HpCostRate != 100)
            {
                goto Label_00AF;
            }
            if (skill.GetHpCost(this) < this.CurrentStatus.param.hp)
            {
                goto Label_00AF;
            }
            return 0;
        Label_00AF:
            if (this.CheckEnableSkillUseCount(skill) == null)
            {
                goto Label_00CE;
            }
            if (this.GetSkillUseCount(skill) != null)
            {
                goto Label_00CE;
            }
            return 0;
        Label_00CE:
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_00F1;
            }
            if (this.IsUnitCondition(0x200L) == null)
            {
                goto Label_00FE;
            }
            return 0;
            goto Label_00FE;
        Label_00F1:
            if (this.GetRageTarget() == null)
            {
                goto Label_00FE;
            }
            return 0;
        Label_00FE:
            if (skill.Condition == null)
            {
                goto Label_014B;
            }
            condition = skill.Condition;
            if (condition == 1)
            {
                goto Label_0126;
            }
            if (condition == 5)
            {
                goto Label_0138;
            }
            goto Label_014B;
        Label_0126:
            if (this.IsDying() != null)
            {
                goto Label_014B;
            }
            return 0;
            goto Label_014B;
        Label_0138:
            if (skill.IsJudgeHp(this) != null)
            {
                goto Label_014B;
            }
            return 0;
        Label_014B:
            if (skill.IsSetBreakObjSkill() == null)
            {
                goto Label_0183;
            }
            battle = SceneBattle.Instance;
            core = null;
            if (battle == null)
            {
                goto Label_0170;
            }
            core = battle.Battle;
        Label_0170:
            if (core == null)
            {
                goto Label_0183;
            }
            if (core.IsAllowBreakObjEntryMax() != null)
            {
                goto Label_0183;
            }
            return 0;
        Label_0183:
            return 1;
        }

        public bool CheckEnemySide(Unit target)
        {
            if (this != target)
            {
                goto Label_0009;
            }
            return 0;
        Label_0009:
            if (this.IsUnitCondition(0x10L) != null)
            {
                goto Label_0028;
            }
            if (this.IsUnitCondition(0x400L) == null)
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
            return (this.Side == target.Side);
        Label_0058:
            return ((this.Side == target.Side) == 0);
        }

        public bool CheckEventTrigger(EEventTrigger trigger)
        {
            if (this.mEventTrigger == null)
            {
                goto Label_002B;
            }
            if (this.mEventTrigger.Count <= 0)
            {
                goto Label_002B;
            }
            return (this.mEventTrigger.Trigger == trigger);
        Label_002B:
            return 0;
        }

        public bool CheckExistMap()
        {
            return (((this.IsDead != null) || (this.IsEntry == null)) ? 0 : (this.IsSub == 0));
        }

        public bool CheckItemDrop(bool waitDirection)
        {
            SceneBattle battle;
            TacticsUnitController controller;
            if (this.Side != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.IsGimmick == null)
            {
                goto Label_0062;
            }
            if (this.IsBreakObj == null)
            {
                goto Label_0032;
            }
            if (this.IsDead != null)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            return 1;
        Label_0032:
            if (this.IsDisableGimmick() != null)
            {
                goto Label_003F;
            }
            return 0;
        Label_003F:
            if (this.EventTrigger == null)
            {
                goto Label_005B;
            }
            if (this.EventTrigger.EventType == 3)
            {
                goto Label_006F;
            }
        Label_005B:
            return 0;
            goto Label_006F;
        Label_0062:
            if (this.IsDead != null)
            {
                goto Label_006F;
            }
            return 0;
        Label_006F:
            if (this.IsUnitFlag(0x100000) == null)
            {
                goto Label_0081;
            }
            return 0;
        Label_0081:
            if (waitDirection == null)
            {
                goto Label_00C7;
            }
            if (this.IsGimmick != null)
            {
                goto Label_00BA;
            }
            battle = SceneBattle.Instance;
            if ((battle != null) == null)
            {
                goto Label_00BA;
            }
            if ((battle.FindUnitController(this) != null) == null)
            {
                goto Label_00BA;
            }
            return 0;
        Label_00BA:
            if (this.IsDropDirection() == null)
            {
                goto Label_00C7;
            }
            return 0;
        Label_00C7:
            return 1;
        }

        public bool CheckLoseEventTrigger()
        {
            if (this.mEventTrigger == null)
            {
                goto Label_0075;
            }
            if (this.mEventTrigger.EventType != 2)
            {
                goto Label_0075;
            }
            if (this.mEventTrigger.Trigger != 1)
            {
                goto Label_0034;
            }
            return this.IsDead;
        Label_0034:
            if (this.mEventTrigger.Trigger != 4)
            {
                goto Label_0075;
            }
            return ((((this.mMaximumStatusHp * this.mEventTrigger.IntValue) / 100) < this.CurrentStatus.param.hp) == 0);
        Label_0075:
            return 0;
        }

        public bool CheckNeedEscaped()
        {
            int num;
            int num2;
            int num3;
            if (this.AI == null)
            {
                goto Label_0020;
            }
            if (this.AI.escape_border != null)
            {
                goto Label_0022;
            }
        Label_0020:
            return 0;
        Label_0022:
            if (this.IsUnitCondition(0x1000000L) == null)
            {
                goto Label_0035;
            }
            return 0;
        Label_0035:
            if (this.MaximumStatus.param.hp == null)
            {
                goto Label_009A;
            }
            num = this.CurrentStatus.param.hp;
            num3 = this.MaximumStatus.param.hp * this.AI.escape_border;
            if (num3 < (num * 100))
            {
                goto Label_009A;
            }
            return 1;
        Label_009A:
            return 0;
        }

        public bool CheckWinEventTrigger()
        {
            if (this.mEventTrigger == null)
            {
                goto Label_0075;
            }
            if (this.mEventTrigger.EventType != 1)
            {
                goto Label_0075;
            }
            if (this.mEventTrigger.Trigger != 1)
            {
                goto Label_0034;
            }
            return this.IsDead;
        Label_0034:
            if (this.mEventTrigger.Trigger != 4)
            {
                goto Label_0075;
            }
            return ((((this.mMaximumStatusHp * this.mEventTrigger.IntValue) / 100) < this.CurrentStatus.param.hp) == 0);
        Label_0075:
            return 0;
        }

        public void ClearBuffEffects()
        {
            int num;
            num = 0;
            goto Label_0071;
        Label_0007:
            if (this.BuffAttachments[num].IsPassive == null)
            {
                goto Label_0027;
            }
            goto Label_006D;
        Label_0027:
            if (this.BuffAttachments[num].skill == null)
            {
                goto Label_005D;
            }
            if (this.BuffAttachments[num].skill.IsPassiveSkill() == null)
            {
                goto Label_005D;
            }
            goto Label_006D;
        Label_005D:
            this.BuffAttachments.RemoveAt(num--);
        Label_006D:
            num += 1;
        Label_0071:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void ClearCondLinkageBuffBits()
        {
            this.CondLinkageBuff.Clear();
            this.CondLinkageDebuff.Clear();
            return;
        }

        public void ClearJudgeHpLists()
        {
            this.mJudgeHpLists.Clear();
            return;
        }

        public void ClearMhmDamage()
        {
            this.mMhmDamageLists.Clear();
            return;
        }

        public void ClearPassiveBuffEffects()
        {
            int num;
            num = 0;
            goto Label_0071;
        Label_0007:
            if (this.BuffAttachments[num].IsPassive != null)
            {
                goto Label_0027;
            }
            goto Label_006D;
        Label_0027:
            if (this.BuffAttachments[num].skill == null)
            {
                goto Label_005D;
            }
            if (this.BuffAttachments[num].skill.IsPassiveSkill() != null)
            {
                goto Label_005D;
            }
            goto Label_006D;
        Label_005D:
            this.BuffAttachments.RemoveAt(num--);
        Label_006D:
            num += 1;
        Label_0071:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private unsafe bool CondLinkageBuffAttach(CondAttachment cond_attachment, int turn)
        {
            BuffEffect effect;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            BaseStatus status6;
            bool flag;
            BuffAttachment attachment;
            BuffAttachment attachment2;
            BuffAttachment attachment3;
            BuffAttachment attachment4;
            BuffAttachment attachment5;
            BuffAttachment attachment6;
            if (cond_attachment != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            effect = cond_attachment.LinkageBuff;
            if (effect != null)
            {
                goto Label_0017;
            }
            return 0;
        Label_0017:
            if (effect.CheckEnableBuffTarget(this) != null)
            {
                goto Label_0025;
            }
            return 0;
        Label_0025:
            cond_attachment.LinkageID = this.GenerateCondLinkageID();
            status = new BaseStatus();
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            status5 = new BaseStatus();
            status6 = new BaseStatus();
            effect.CalcBuffStatus(&status, this.Element, 0, 1, 0, 0, 0);
            effect.CalcBuffStatus(&status2, this.Element, 0, 1, 1, 0, 0);
            effect.CalcBuffStatus(&status3, this.Element, 0, 0, 0, 1, 0);
            effect.CalcBuffStatus(&status4, this.Element, 1, 1, 0, 0, 0);
            effect.CalcBuffStatus(&status5, this.Element, 1, 1, 1, 0, 0);
            effect.CalcBuffStatus(&status6, this.Element, 1, 0, 0, 1, 0);
            flag = 0;
            if (effect.CheckBuffCalcType(0, 0, 0) == null)
            {
                goto Label_00FB;
            }
            attachment = this.CreateCondLinkageBuffAttachment(this, cond_attachment, 0, 0, 0, status, turn);
            if (this.SetBuffAttachment(attachment, 0) == null)
            {
                goto Label_00FB;
            }
            flag = 1;
        Label_00FB:
            if (effect.CheckBuffCalcType(0, 0, 1) == null)
            {
                goto Label_0129;
            }
            attachment2 = this.CreateCondLinkageBuffAttachment(this, cond_attachment, 0, 1, 0, status2, turn);
            if (this.SetBuffAttachment(attachment2, 0) == null)
            {
                goto Label_0129;
            }
            flag = 1;
        Label_0129:
            if (effect.CheckBuffCalcType(0, 1) == null)
            {
                goto Label_0156;
            }
            attachment3 = this.CreateCondLinkageBuffAttachment(this, cond_attachment, 0, 0, 1, status3, turn);
            if (this.SetBuffAttachment(attachment3, 0) == null)
            {
                goto Label_0156;
            }
            flag = 1;
        Label_0156:
            if (effect.CheckBuffCalcType(1, 0, 0) == null)
            {
                goto Label_0185;
            }
            attachment4 = this.CreateCondLinkageBuffAttachment(this, cond_attachment, 1, 0, 0, status4, turn);
            if (this.SetBuffAttachment(attachment4, 0) == null)
            {
                goto Label_0185;
            }
            flag = 1;
        Label_0185:
            if (effect.CheckBuffCalcType(1, 0, 1) == null)
            {
                goto Label_01B4;
            }
            attachment5 = this.CreateCondLinkageBuffAttachment(this, cond_attachment, 1, 1, 0, status5, turn);
            if (this.SetBuffAttachment(attachment5, 0) == null)
            {
                goto Label_01B4;
            }
            flag = 1;
        Label_01B4:
            if (effect.CheckBuffCalcType(1, 1) == null)
            {
                goto Label_01E2;
            }
            attachment6 = this.CreateCondLinkageBuffAttachment(this, cond_attachment, 1, 0, 1, status6, turn);
            if (this.SetBuffAttachment(attachment6, 0) == null)
            {
                goto Label_01E2;
            }
            flag = 1;
        Label_01E2:
            if (flag == null)
            {
                goto Label_0225;
            }
            status.Add(status4);
            status.Add(status5);
            status3.Add(status6);
            BattleCore.SetBuffBits(status, &this.CondLinkageBuff, &this.CondLinkageDebuff);
            BattleCore.SetBuffBits(status3, &this.CondLinkageBuff, &this.CondLinkageDebuff);
        Label_0225:
            this.UpdateBuffEffects();
            return 1;
        }

        public unsafe bool ContainsSkillAttachment(SkillData skill)
        {
            int num;
            BuffAttachment attachment;
            CondAttachment attachment2;
            List<CondAttachment>.Enumerator enumerator;
            bool flag;
            num = 0;
            goto Label_0040;
        Label_0007:
            attachment = this.BuffAttachments[num];
            if (attachment.skill == null)
            {
                goto Label_003C;
            }
            if ((attachment.skill.SkillID == skill.SkillID) == null)
            {
                goto Label_003C;
            }
            return 1;
        Label_003C:
            num += 1;
        Label_0040:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0007;
            }
            enumerator = this.CondAttachments.GetEnumerator();
        Label_005D:
            try
            {
                goto Label_0098;
            Label_0062:
                attachment2 = &enumerator.Current;
                if (attachment2.skill == null)
                {
                    goto Label_0098;
                }
                if ((attachment2.skill.SkillID == skill.SkillID) == null)
                {
                    goto Label_0098;
                }
                flag = 1;
                goto Label_00B7;
            Label_0098:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0062;
                }
                goto Label_00B5;
            }
            finally
            {
            Label_00A9:
                ((List<CondAttachment>.Enumerator) enumerator).Dispose();
            }
        Label_00B5:
            return 0;
        Label_00B7:
            return flag;
        }

        public bool CreateAddedAbilityAndSkills(AbilityParam ap, int ab_exp)
        {
            int num;
            AbilityData data;
            int num2;
            AbilityData data2;
            AbilityData data3;
            List<SkillData> list;
            int num3;
            SkillData data4;
            if (ap != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            num = 0;
            goto Label_0033;
        Label_000F:
            data = this.mUnitData.BattleAbilitys[num];
            if (data.Param != ap)
            {
                goto Label_002F;
            }
            return 0;
        Label_002F:
            num += 1;
        Label_0033:
            if (num < this.mUnitData.BattleAbilitys.Count)
            {
                goto Label_000F;
            }
            num2 = 0;
            goto Label_006F;
        Label_0050:
            data2 = this.mAddedAbilitys[num2];
            if (data2.Param != ap)
            {
                goto Label_006B;
            }
            return 0;
        Label_006B:
            num2 += 1;
        Label_006F:
            if (num2 < this.mAddedAbilitys.Count)
            {
                goto Label_0050;
            }
            data3 = new AbilityData();
            data3.Setup(this.mUnitData, this.GetFreeAbilityIID(), ap.iname, Math.Max(ab_exp, 0), 0);
            this.mAddedAbilitys.Add(data3);
            list = new List<SkillData>(this.mUnitData.BattleSkills);
            list.AddRange(this.mAddedSkills);
            data3.UpdateLearningsSkill(1, list);
            num3 = 0;
            goto Label_0127;
        Label_00E6:
            data4 = data3.Skills[num3];
            if (data4 == null)
            {
                goto Label_0121;
            }
            if (this.mAddedSkills.Contains(data4) == null)
            {
                goto Label_0114;
            }
            goto Label_0121;
        Label_0114:
            this.mAddedSkills.Add(data4);
        Label_0121:
            num3 += 1;
        Label_0127:
            if (num3 < data3.Skills.Count)
            {
                goto Label_00E6;
            }
            this.AddSkillUseCount(data3);
            return 1;
        }

        private BuffAttachment CreateCondLinkageBuffAttachment(Unit target, CondAttachment cond_attachment, BuffTypes buff_type, bool is_negative_value_is_buff, SkillParamCalcTypes calc_type, BaseStatus status, int turn)
        {
            BuffEffect effect;
            BuffAttachment attachment;
            if (cond_attachment != null)
            {
                goto Label_0008;
            }
            return null;
        Label_0008:
            effect = cond_attachment.LinkageBuff;
            if (effect != null)
            {
                goto Label_0017;
            }
            return null;
        Label_0017:
            attachment = new BuffAttachment(effect.param);
            attachment.user = null;
            attachment.skill = null;
            attachment.skilltarget = 1;
            attachment.IsPassive = effect.param.chk_timing == 1;
            attachment.CheckTarget = null;
            attachment.DuplicateCount = 0;
            attachment.CheckTiming = effect.param.chk_timing;
            if (turn != null)
            {
                goto Label_0089;
            }
            turn = effect.param.turn;
        Label_0089:
            attachment.turn = turn;
            attachment.BuffType = buff_type;
            attachment.IsNegativeValueIsBuff = is_negative_value_is_buff;
            attachment.CalcType = calc_type;
            attachment.UseCondition = effect.param.cond;
            attachment.LinkageID = cond_attachment.LinkageID;
            status.CopyTo(attachment.status);
            return attachment;
        }

        public void CureCondEffects(EUnitCondition target, bool updated, bool forced)
        {
            int num;
            CondAttachment attachment;
            ConditionEffectTypes types;
            if (this.CheckCancelSkillCureCondition(target) == null)
            {
                goto Label_0012;
            }
            this.CancelCastSkill();
        Label_0012:
            num = 0;
            goto Label_00B7;
        Label_0019:
            attachment = this.CondAttachments[num];
            if (attachment.IsPassive == null)
            {
                goto Label_003B;
            }
            goto Label_00B3;
        Label_003B:
            if (attachment.UseCondition == null)
            {
                goto Label_0057;
            }
            if (attachment.UseCondition == 3)
            {
                goto Label_0057;
            }
            goto Label_00B3;
        Label_0057:
            if (attachment.IsCurse == null)
            {
                goto Label_006D;
            }
            if (forced != null)
            {
                goto Label_006D;
            }
            goto Label_00B3;
        Label_006D:
            if (attachment.Condition != target)
            {
                goto Label_00B3;
            }
            switch ((attachment.CondType - 2))
            {
                case 0:
                    goto Label_0099;

                case 1:
                    goto Label_0099;

                case 2:
                    goto Label_0099;
            }
            goto Label_00AE;
        Label_0099:
            this.CondAttachments.RemoveAt(num--);
            goto Label_00B3;
        Label_00AE:;
        Label_00B3:
            num += 1;
        Label_00B7:
            if (num < this.CondAttachments.Count)
            {
                goto Label_0019;
            }
            if (updated == null)
            {
                goto Label_00D4;
            }
            this.UpdateCondEffects();
        Label_00D4:
            return;
        }

        public void Damage(int value, bool is_check_dying)
        {
            bool flag;
            flag = this.IsDying();
            this.CurrentStatus.param.hp = Math.Max(this.CurrentStatus.param.hp - value, 0);
            if (is_check_dying == null)
            {
                goto Label_005C;
            }
            if (flag != null)
            {
                goto Label_005C;
            }
            if (this.IsDying() == null)
            {
                goto Label_005C;
            }
            this.SetUnitFlag(0x800000, 1);
        Label_005C:
            return;
        }

        public void DecrementTriggerCount()
        {
            int num;
            if (this.mEventTrigger == null)
            {
                goto Label_0032;
            }
            this.mEventTrigger.Count = Math.Min(this.mEventTrigger.Count -= 1, 0);
        Label_0032:
            return;
        }

        public void EndDropDirection()
        {
            this.mDropDirection = 0;
            return;
        }

        public bool ExecuteAbilityChange(AbilityParam fr_ap, AbilityParam to_ap, int turn, bool is_reset)
        {
            eAcType type;
            AbilityChange change;
            int num;
            AbilityChange change2;
            int num2;
            AbilityData data;
            eAcType type2;
            if ((fr_ap != null) && (to_ap != null))
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            if ((fr_ap.slot != 1) && (to_ap.slot != 1))
            {
                goto Label_0043;
            }
            DebugUtility.LogError(string.Format("アビリティ切替：サポートスキルは対象外！ from={0} to={1}", fr_ap.iname, to_ap.iname));
            return 0;
        Label_0043:
            if (fr_ap.slot == to_ap.slot)
            {
                goto Label_0071;
            }
            DebugUtility.LogError(string.Format("アビリティ切替：スロットは同系統のみ！ from={0} to={1}", fr_ap.iname, to_ap.iname));
            return 0;
        Label_0071:
            type = 0;
            change = null;
            num = 0;
            goto Label_00A0;
        Label_007C:
            change2 = this.mAbilityChangeLists[num];
            if (change2.IsInclude(fr_ap) == null)
            {
                goto Label_009C;
            }
            change = change2;
            goto Label_00B1;
        Label_009C:
            num += 1;
        Label_00A0:
            if (num < this.mAbilityChangeLists.Count)
            {
                goto Label_007C;
            }
        Label_00B1:
            if (change != null)
            {
                goto Label_0149;
            }
            num2 = 0;
            goto Label_012D;
        Label_00BF:
            data = this.mUnitData.BattleAbilitys[num2];
            if ((data.AbilityID == fr_ap.iname) == null)
            {
                goto Label_0127;
            }
            type = 1;
            change = new AbilityChange(fr_ap, to_ap, (turn == null) ? 0 : (turn + 1), is_reset, data.Exp, turn == 0);
            if (change == null)
            {
                goto Label_01A2;
            }
            this.mAbilityChangeLists.Add(change);
            goto Label_0144;
        Label_0127:
            num2 += 1;
        Label_012D:
            if (num2 < this.mUnitData.BattleAbilitys.Count)
            {
                goto Label_00BF;
            }
        Label_0144:
            goto Label_01A2;
        Label_0149:
            if (change.IsCancel(fr_ap, to_ap) == null)
            {
                goto Label_015D;
            }
            type = 2;
            goto Label_01A2;
        Label_015D:
            if (change.IsBack(fr_ap, to_ap) == null)
            {
                goto Label_0171;
            }
            type = 3;
            goto Label_01A2;
        Label_0171:
            if (change.GetToAp() != fr_ap)
            {
                goto Label_01A2;
            }
            type = 4;
            change.Add(fr_ap, to_ap, (turn == null) ? 0 : (turn + 1), is_reset, change.GetLastExp(), turn == 0);
        Label_01A2:
            if (type == null)
            {
                goto Label_01AE;
            }
            if (change != null)
            {
                goto Label_01CB;
            }
        Label_01AE:
            DebugUtility.LogWarning(string.Format("アビリティ切替スキル設定不整合！ from={0} to={1}", fr_ap.iname, to_ap.iname));
            return 0;
        Label_01CB:
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_01EC;

                case 1:
                    goto Label_01FF;

                case 2:
                    goto Label_0217;

                case 3:
                    goto Label_01EC;
            }
            goto Label_0222;
        Label_01EC:
            this.CreateAddedAbilityAndSkills(to_ap, change.GetLastExp());
            goto Label_0222;
        Label_01FF:
            change.Clear();
            this.mAbilityChangeLists.Remove(change);
            goto Label_0222;
        Label_0217:
            change.RemoveLast();
        Label_0222:
            this.RefleshBattleAbilitysAndSkills();
            return 1;
        }

        public void ForceDead()
        {
            int num;
            int num2;
            this.CurrentStatus.param.hp = 0;
            this.mDeathCount = 0;
            this.mChargeTime = 0;
            this.CancelCastSkill();
            this.CancelGuradTarget();
            this.SetUnitFlag(0x2000, 1);
            num = 0;
            goto Label_00B2;
        Label_004D:
            if (this.BuffAttachments[num].IsPassive != null)
            {
                goto Label_00AE;
            }
            if (this.BuffAttachments[num].skill == null)
            {
                goto Label_009E;
            }
            if (this.BuffAttachments[num].skill.IsPassiveSkill() == null)
            {
                goto Label_009E;
            }
            goto Label_00AE;
        Label_009E:
            this.BuffAttachments.RemoveAt(num--);
        Label_00AE:
            num += 1;
        Label_00B2:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_004D;
            }
            num2 = 0;
            goto Label_012F;
        Label_00CA:
            if (this.CondAttachments[num2].IsPassive != null)
            {
                goto Label_012B;
            }
            if (this.CondAttachments[num2].skill == null)
            {
                goto Label_011B;
            }
            if (this.CondAttachments[num2].skill.IsPassiveSkill() == null)
            {
                goto Label_011B;
            }
            goto Label_012B;
        Label_011B:
            this.CondAttachments.RemoveAt(num2--);
        Label_012B:
            num2 += 1;
        Label_012F:
            if (num2 < this.CondAttachments.Count)
            {
                goto Label_00CA;
            }
            this.UpdateBuffEffects();
            this.UpdateCondEffects();
            return;
        }

        private uint GenerateCondLinkageID()
        {
            mCondLinkageID += 1;
            if (mCondLinkageID != null)
            {
                goto Label_0022;
            }
            mCondLinkageID += 1;
        Label_0022:
            return mCondLinkageID;
        }

        public AbilityData GetAbilityData(long iid)
        {
            int num;
            if (iid > 0L)
            {
                goto Label_000A;
            }
            return null;
        Label_000A:
            num = 0;
            goto Label_0039;
        Label_0011:
            if (iid != this.BattleAbilitys[num].UniqueID)
            {
                goto Label_0035;
            }
            return this.BattleAbilitys[num];
        Label_0035:
            num += 1;
        Label_0039:
            if (num < this.BattleAbilitys.Count)
            {
                goto Label_0011;
            }
            return null;
        }

        public int GetActionSkillBuffValue(BuffTypes type, SkillParamCalcTypes calc, ParamTypes param)
        {
            int num;
            int num2;
            BuffAttachment attachment;
            num = 0;
            num2 = 0;
            goto Label_006E;
        Label_0009:
            attachment = this.BuffAttachments[num2];
            if (attachment.IsPassive == null)
            {
                goto Label_002B;
            }
            goto Label_006A;
        Label_002B:
            if (attachment.BuffType == type)
            {
                goto Label_003C;
            }
            goto Label_006A;
        Label_003C:
            if (attachment.CalcType == calc)
            {
                goto Label_004D;
            }
            goto Label_006A;
        Label_004D:
            num = Math.Max(Math.Abs(attachment.status[param]), num);
        Label_006A:
            num2 += 1;
        Label_006E:
            if (num2 < this.BuffAttachments.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public unsafe int GetAllyUnitNum(Unit target_unit)
        {
            SceneBattle battle;
            BattleCore core;
            BattleMap map;
            int num;
            int num2;
            TacticsUnitController controller;
            IntVector2 vector;
            int num3;
            int num4;
            Grid grid;
            Unit unit;
            if (target_unit == null)
            {
                goto Label_0011;
            }
            if (target_unit.IsDead == null)
            {
                goto Label_0013;
            }
        Label_0011:
            return 0;
        Label_0013:
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0026;
            }
            return 0;
        Label_0026:
            core = battle.Battle;
            if (core == null)
            {
                goto Label_003E;
            }
            if (core.CurrentMap != null)
            {
                goto Label_0040;
            }
        Label_003E:
            return 0;
        Label_0040:
            map = core.CurrentMap;
            num = target_unit.x;
            num2 = target_unit.y;
            if (core.IsBattleFlag(6) != null)
            {
                goto Label_00A3;
            }
            if (target_unit != core.CurrentUnit)
            {
                goto Label_00A3;
            }
            controller = battle.FindUnitController(target_unit);
            if (controller == null)
            {
                goto Label_00A3;
            }
            vector = battle.CalcCoord(controller.CenterPosition);
            num = &vector.x;
            num2 = &vector.y;
        Label_00A3:
            num3 = 0;
            num4 = 0;
            goto Label_0138;
        Label_00AE:
            grid = map[num + DIRECTION_OFFSETS[num4, 0], num2 + DIRECTION_OFFSETS[num4, 1]];
            if (grid != null)
            {
                goto Label_00E1;
            }
            goto Label_0132;
        Label_00E1:
            unit = null;
            if (core.IsBattleFlag(6) == null)
            {
                goto Label_00FF;
            }
            unit = core.FindUnitAtGrid(grid);
            goto Label_0109;
        Label_00FF:
            unit = core.DirectFindUnitAtGrid(grid);
        Label_0109:
            if (unit != null)
            {
                goto Label_0115;
            }
            goto Label_0132;
        Label_0115:
            if (unit.mSide == target_unit.mSide)
            {
                goto Label_012C;
            }
            goto Label_0132;
        Label_012C:
            num3 += 1;
        Label_0132:
            num4 += 1;
        Label_0138:
            if (num4 < 4)
            {
                goto Label_00AE;
            }
            return num3;
        }

        public int GetAttackHeight()
        {
            return this.GetAttackHeight(this.GetAttackSkill(), 0);
        }

        public int GetAttackHeight(SkillData skill, bool is_range)
        {
            int num;
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            num = this.UnitData.GetAttackHeight(skill, is_range);
            if (skill.IsEnableChangeRange() == null)
            {
                goto Label_0035;
            }
            num += this.mCurrentStatus[2];
        Label_0035:
            return Math.Max(num, 1);
        }

        public int GetAttackRangeMax()
        {
            return this.GetAttackRangeMax(this.GetAttackSkill());
        }

        public int GetAttackRangeMax(SkillData skill)
        {
            int num;
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            num = this.UnitData.GetAttackRangeMax(skill);
            if (skill.IsEnableChangeRange() == null)
            {
                goto Label_0034;
            }
            num += this.mCurrentStatus[0];
        Label_0034:
            if (skill.SkillParam.select_range != 4)
            {
                goto Label_0048;
            }
            num = 0x63;
        Label_0048:
            return Math.Max(num, 0);
        }

        public int GetAttackRangeMin()
        {
            return this.GetAttackRangeMin(this.GetAttackSkill());
        }

        public int GetAttackRangeMin(SkillData skill)
        {
            int num;
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (skill.SkillParam.select_range != 4)
            {
                goto Label_001B;
            }
            return 0;
        Label_001B:
            return this.UnitData.GetAttackRangeMin(skill);
        }

        public int GetAttackScope()
        {
            return this.GetAttackScope(this.GetAttackSkill());
        }

        public int GetAttackScope(SkillData skill)
        {
            int num;
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            num = this.UnitData.GetAttackScope(skill);
            if (skill.IsEnableChangeRange() == null)
            {
                goto Label_0034;
            }
            num += this.mCurrentStatus[1];
        Label_0034:
            return Math.Max(num, 0);
        }

        public SkillData GetAttackSkill()
        {
            return this.UnitData.GetAttackSkill();
        }

        public int GetAutoHpHealValue()
        {
            int num;
            int num2;
            bool flag;
            FixParam param;
            int num3;
            CondAttachment attachment;
            int num4;
            CondEffect effect;
            CondEffect effect2;
            if (this.IsUnitCondition(0x1000000L) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            num = 0;
            if (this.IsUnitCondition(0x80000L) == null)
            {
                goto Label_02CA;
            }
            num2 = 0;
            flag = 0;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            num3 = 0;
            goto Label_0289;
        Label_0042:
            attachment = this.CondAttachments[num3];
            if ((attachment == null) || (attachment.IsFailCondition() == null))
            {
                goto Label_0283;
            }
            if (attachment.ContainsCondition(0x80000L) != null)
            {
                goto Label_007B;
            }
            goto Label_0283;
        Label_007B:
            num4 = 0;
            if (attachment.skill == null)
            {
                goto Label_01C4;
            }
            effect = attachment.skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_011B;
            }
            if (effect.param.v_auto_hp_heal <= 0)
            {
                goto Label_00EA;
            }
            num4 = Math.Max(num4, (this.MaximumStatus.param.hp * effect.param.v_auto_hp_heal) / 100);
        Label_00EA:
            if (effect.param.v_auto_hp_heal_fix <= 0)
            {
                goto Label_011B;
            }
            num4 = Math.Max(num4, effect.param.v_auto_hp_heal_fix);
        Label_011B:
            effect2 = (attachment.user != this) ? null : attachment.skill.GetCondEffect(1);
            if (effect2 == null)
            {
                goto Label_024B;
            }
            if (effect2.param.v_auto_hp_heal <= 0)
            {
                goto Label_018E;
            }
            num4 = Math.Max(num4, (this.MaximumStatus.param.hp * effect2.param.v_auto_hp_heal) / 100);
        Label_018E:
            if (effect2.param.v_auto_hp_heal_fix <= 0)
            {
                goto Label_024B;
            }
            num4 = Math.Max(num4, effect2.param.v_auto_hp_heal_fix);
            goto Label_024B;
        Label_01C4:
            if (attachment.Param == null)
            {
                goto Label_024B;
            }
            if (attachment.Param.v_auto_hp_heal <= 0)
            {
                goto Label_021A;
            }
            num4 = Math.Max(num4, (this.MaximumStatus.param.hp * attachment.Param.v_auto_hp_heal) / 100);
        Label_021A:
            if (attachment.Param.v_auto_hp_heal_fix <= 0)
            {
                goto Label_024B;
            }
            num4 = Math.Max(num4, attachment.Param.v_auto_hp_heal_fix);
        Label_024B:
            if (num4 != null)
            {
                goto Label_0278;
            }
            num4 = (this.MaximumStatus.param.hp * param.HpAutoHealRate) / 100;
        Label_0278:
            flag = 1;
            num2 = Math.Max(num2, num4);
        Label_0283:
            num3 += 1;
        Label_0289:
            if (num3 < this.CondAttachments.Count)
            {
                goto Label_0042;
            }
            if (flag != null)
            {
                goto Label_02C6;
            }
            num2 = (this.MaximumStatus.param.hp * param.HpAutoHealRate) / 100;
        Label_02C6:
            num += num2;
        Label_02CA:
            if (this.IsUnitCondition(8L) == null)
            {
                goto Label_02F1;
            }
            if (this.IsUnitCondition(0x400000L) == null)
            {
                goto Label_02F1;
            }
            num += this.GetGoodSleepHpHealValue();
        Label_02F1:
            return num;
        }

        public int GetAutoMpHealValue()
        {
            int num;
            bool flag;
            int num2;
            FixParam param;
            int num3;
            CondAttachment attachment;
            int num4;
            CondEffect effect;
            CondEffect effect2;
            num = this.AutoJewel;
            if (this.IsUnitCondition(0x800000L) == null)
            {
                goto Label_02BC;
            }
            flag = 0;
            num2 = 0;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            num3 = 0;
            goto Label_027B;
        Label_0034:
            attachment = this.CondAttachments[num3];
            if ((attachment == null) || (attachment.IsFailCondition() == null))
            {
                goto Label_0275;
            }
            if (attachment.ContainsCondition(0x800000L) != null)
            {
                goto Label_006D;
            }
            goto Label_0275;
        Label_006D:
            num4 = 0;
            if (attachment.skill == null)
            {
                goto Label_01B6;
            }
            effect = attachment.skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_010D;
            }
            if (effect.param.v_auto_mp_heal <= 0)
            {
                goto Label_00DC;
            }
            num4 = Math.Max(num4, (this.MaximumStatus.param.mp * effect.param.v_auto_mp_heal) / 100);
        Label_00DC:
            if (effect.param.v_auto_mp_heal_fix <= 0)
            {
                goto Label_010D;
            }
            num4 = Math.Max(num4, effect.param.v_auto_mp_heal_fix);
        Label_010D:
            effect2 = (attachment.user != this) ? null : attachment.skill.GetCondEffect(1);
            if (effect2 == null)
            {
                goto Label_023D;
            }
            if (effect2.param.v_auto_mp_heal <= 0)
            {
                goto Label_0180;
            }
            num4 = Math.Max(num4, (this.MaximumStatus.param.mp * effect2.param.v_auto_mp_heal) / 100);
        Label_0180:
            if (effect2.param.v_auto_mp_heal_fix <= 0)
            {
                goto Label_023D;
            }
            num4 = Math.Max(num4, effect2.param.v_auto_mp_heal_fix);
            goto Label_023D;
        Label_01B6:
            if (attachment.Param == null)
            {
                goto Label_023D;
            }
            if (attachment.Param.v_auto_mp_heal <= 0)
            {
                goto Label_020C;
            }
            num4 = Math.Max(num4, (this.MaximumStatus.param.mp * attachment.Param.v_auto_mp_heal) / 100);
        Label_020C:
            if (attachment.Param.v_auto_mp_heal_fix <= 0)
            {
                goto Label_023D;
            }
            num4 = Math.Max(num4, attachment.Param.v_auto_mp_heal_fix);
        Label_023D:
            if (num4 != null)
            {
                goto Label_026A;
            }
            num4 = (this.MaximumStatus.param.mp * param.MpAutoHealRate) / 100;
        Label_026A:
            flag = 1;
            num2 = Math.Max(num2, num4);
        Label_0275:
            num3 += 1;
        Label_027B:
            if (num3 < this.CondAttachments.Count)
            {
                goto Label_0034;
            }
            if (flag != null)
            {
                goto Label_02B8;
            }
            num2 = (this.MaximumStatus.param.mp * param.MpAutoHealRate) / 100;
        Label_02B8:
            num += num2;
        Label_02BC:
            if (this.IsUnitCondition(8L) == null)
            {
                goto Label_02E3;
            }
            if (this.IsUnitCondition(0x400000L) == null)
            {
                goto Label_02E3;
            }
            num += this.GetGoodSleepMpHealValue();
        Label_02E3:
            return num;
        }

        public int GetBaseAvoidRate()
        {
            UnitJobOverwriteParam param;
            if (this.Job != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            param = this.UnitData.GetUnitJobOverwriteParam(this.Job.JobID);
            if (param == null)
            {
                goto Label_0031;
            }
            return param.mAvoid;
        Label_0031:
            return this.Job.GetJobRankAvoidRate();
        }

        public int GetBuffAttachmentDuplicateCount(BuffAttachment buff)
        {
            int num;
            int num2;
            BuffAttachment attachment;
            num = 0;
            num2 = 0;
            goto Label_002B;
        Label_0009:
            attachment = this.BuffAttachments[num2];
            if (this.isSameBuffAttachment(attachment, buff) == null)
            {
                goto Label_0027;
            }
            num += 1;
        Label_0027:
            num2 += 1;
        Label_002B:
            if (num2 < this.BuffAttachments.Count)
            {
                goto Label_0009;
            }
            return num;
        }

        public int GetBuffPriority(SkillData skill, SkillEffectTargets target)
        {
            int num;
            BuffEffect effect;
            int num2;
            int num3;
            int num4;
            num = 0xff;
            if (this.AI == null)
            {
                goto Label_0021;
            }
            if (this.AI.BuffPriorities != null)
            {
                goto Label_0023;
            }
        Label_0021:
            return num;
        Label_0023:
            effect = skill.GetBuffEffect(target);
            if (effect == null)
            {
                goto Label_008F;
            }
            if (effect.targets == null)
            {
                goto Label_008F;
            }
            num2 = 0;
            goto Label_007E;
        Label_0043:
            num3 = Array.IndexOf<ParamTypes>(this.AI.BuffPriorities, effect.targets[num2].paramType);
            if (num3 == -1)
            {
                goto Label_007A;
            }
            num = Math.Max(Math.Min(num, num3), 0);
        Label_007A:
            num2 += 1;
        Label_007E:
            if (num2 < effect.targets.Count)
            {
                goto Label_0043;
            }
        Label_008F:
            if (skill.ControlChargeTimeValue == null)
            {
                goto Label_00CA;
            }
            num4 = Array.IndexOf<ParamTypes>(this.AI.BuffPriorities, 0x5e);
            if (num4 == -1)
            {
                goto Label_00CA;
            }
            num = Math.Max(Math.Min(num, num4), 0);
        Label_00CA:
            return num;
        }

        public OInt GetCastSpeed()
        {
            return this.GetCastSpeed(this.mCastSkill);
        }

        public OInt GetCastSpeed(SkillData skill)
        {
            int num;
            if (this.IsUnitCondition(0x10000L) == null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            if (skill != null)
            {
                goto Label_0025;
            }
            return 0;
        Label_0025:
            num = skill.CastSpeed;
            if (this.IsUnitCondition(0x20000L) == null)
            {
                goto Label_004C;
            }
            num += (num * 50) / 100;
        Label_004C:
            if (this.IsUnitCondition(0x40000L) == null)
            {
                goto Label_0067;
            }
            num -= (num * 50) / 100;
        Label_0067:
            return num;
        }

        public OInt GetChargeSpeed()
        {
            int num;
            int num2;
            int num3;
            CondAttachment attachment;
            CondEffect effect;
            CondEffect effect2;
            int num4;
            int num5;
            CondAttachment attachment2;
            CondEffect effect3;
            CondEffect effect4;
            if (this.IsUnitCondition(0x10000L) == null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            num = this.CurrentStatus.param.spd;
            if (this.IsUnitCondition(0x20000L) == null)
            {
                goto Label_0190;
            }
            num2 = 0;
            num3 = 0;
            goto Label_0151;
        Label_0048:
            attachment = this.CondAttachments[num3];
            if ((attachment == null) || (attachment.IsFailCondition() == null))
            {
                goto Label_014D;
            }
            if (attachment.ContainsCondition(0x20000L) != null)
            {
                goto Label_007C;
            }
            goto Label_014D;
        Label_007C:
            if (attachment.skill == null)
            {
                goto Label_0102;
            }
            effect = attachment.skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_00B9;
            }
            num2 = Math.Max(num2, Math.Abs(effect.param.v_fast));
        Label_00B9:
            effect2 = (attachment.user != this) ? null : attachment.skill.GetCondEffect(1);
            if (effect2 == null)
            {
                goto Label_014D;
            }
            num2 = Math.Max(num2, Math.Abs(effect2.param.v_fast));
            goto Label_014D;
        Label_0102:
            if (attachment.Param == null)
            {
                goto Label_012E;
            }
            num2 = Math.Max(num2, Math.Abs(attachment.Param.v_fast));
            goto Label_014D;
        Label_012E:
            num2 = Math.Abs(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockUpValue);
        Label_014D:
            num3 += 1;
        Label_0151:
            if (num3 < this.CondAttachments.Count)
            {
                goto Label_0048;
            }
            if (num2 != null)
            {
                goto Label_0187;
            }
            num2 = Math.Abs(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockUpValue);
        Label_0187:
            num += (num * num2) / 100;
        Label_0190:
            if (this.IsUnitCondition(0x40000L) == null)
            {
                goto Label_030C;
            }
            num4 = 0;
            num5 = 0;
            goto Label_02C9;
        Label_01AC:
            attachment2 = this.CondAttachments[num5];
            if ((attachment2 == null) || (attachment2.IsFailCondition() == null))
            {
                goto Label_02C3;
            }
            if (attachment2.ContainsCondition(0x40000L) != null)
            {
                goto Label_01E5;
            }
            goto Label_02C3;
        Label_01E5:
            if (attachment2.skill == null)
            {
                goto Label_0273;
            }
            effect3 = attachment2.skill.GetCondEffect(0);
            if (effect3 == null)
            {
                goto Label_0226;
            }
            num4 = Math.Max(num4, Math.Abs(effect3.param.v_slow));
        Label_0226:
            effect4 = (attachment2.user != this) ? null : attachment2.skill.GetCondEffect(1);
            if (effect4 == null)
            {
                goto Label_02C3;
            }
            num4 = Math.Max(num4, Math.Abs(effect4.param.v_slow));
            goto Label_02C3;
        Label_0273:
            if (attachment2.Param == null)
            {
                goto Label_02A3;
            }
            num4 = Math.Max(num4, Math.Abs(attachment2.Param.v_slow));
            goto Label_02C3;
        Label_02A3:
            num4 = Math.Abs(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockDownValue);
        Label_02C3:
            num5 += 1;
        Label_02C9:
            if (num5 < this.CondAttachments.Count)
            {
                goto Label_01AC;
            }
            if (num4 != null)
            {
                goto Label_0302;
            }
            num4 = Math.Abs(MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.DefaultClockDownValue);
        Label_0302:
            num -= (num * num4) / 100;
        Label_030C:
            return num;
        }

        public int GetCombination()
        {
            return this.UnitData.GetCombination();
        }

        public int GetCombinationRange()
        {
            return (this.UnitData.GetCombinationRange() + this.CurrentStatus[0x16]);
        }

        public int GetConditionPriority(SkillData skill, SkillEffectTargets target)
        {
            int num;
            CondEffect effect;
            int num2;
            int num3;
            num = 0xff;
            if (this.AI == null)
            {
                goto Label_0021;
            }
            if (this.AI.ConditionPriorities != null)
            {
                goto Label_0023;
            }
        Label_0021:
            return num;
        Label_0023:
            effect = skill.GetCondEffect(target);
            if (effect == null)
            {
                goto Label_009D;
            }
            if (effect.param == null)
            {
                goto Label_009D;
            }
            if (effect.param.conditions == null)
            {
                goto Label_009D;
            }
            num2 = 0;
            goto Label_008A;
        Label_0053:
            num3 = Array.IndexOf<EUnitCondition>(this.AI.ConditionPriorities, effect.param.conditions[num2]);
            if (num3 == -1)
            {
                goto Label_0086;
            }
            num = Math.Max(Math.Min(num, num3), 0);
        Label_0086:
            num2 += 1;
        Label_008A:
            if (num2 < ((int) effect.param.conditions.Length))
            {
                goto Label_0053;
            }
        Label_009D:
            return num;
        }

        public AIAction GetCurrentAIAction()
        {
            if (this.IsAIActionTable() != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.mAIActionTurnCount >= this.mAIActionTable.actions[this.mAIActionIndex].turn)
            {
                goto Label_0044;
            }
            return null;
        Label_0044:
            return this.mAIActionTable.actions[this.mAIActionIndex];
        }

        public AIPatrolPoint GetCurrentPatrolPoint()
        {
            if (this.IsAIPatrolTable() != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            return this.mAIPatrolTable.routes[this.mAIPatrolIndex];
        }

        public unsafe void GetEnableBetterBuffEffect(Unit self, SkillData skill, SkillEffectTargets effect_target, out int buff_count, out int buff_value, bool bRequestOnly)
        {
            BuffEffect effect;
            int num;
            BuffEffect.BuffTarget target;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            <GetEnableBetterBuffEffect>c__AnonStorey3F0 storeyf;
            storeyf = new <GetEnableBetterBuffEffect>c__AnonStorey3F0();
            storeyf.skill = skill;
            *((int*) buff_count) = 0;
            *((int*) buff_value) = 0;
            effect = storeyf.skill.GetBuffEffect(effect_target);
            if (((effect == null) || (effect.param == null)) || (effect.param.buffs == null))
            {
                goto Label_0246;
            }
            num = 0;
            goto Label_0235;
        Label_004D:
            target = effect.targets[num];
            if (target.buffType != null)
            {
                goto Label_00D5;
            }
            if (this.IsUnitCondition(0x4000L) == null)
            {
                goto Label_007B;
            }
            goto Label_0231;
        Label_007B:
            num2 = ((self == null) || (self.AI == null)) ? 0 : self.AI.buff_border;
            if (Math.Max(100 - this.CurrentStatus.enchant_resist.resist_buff, 0) > num2)
            {
                goto Label_014E;
            }
            goto Label_0231;
            goto Label_014E;
        Label_00D5:
            if (target.buffType != 1)
            {
                goto Label_014E;
            }
            if (this.IsUnitCondition(0x8000L) == null)
            {
                goto Label_00F7;
            }
            goto Label_0231;
        Label_00F7:
            num4 = ((self == null) || (self.AI == null)) ? 0 : self.AI.buff_border;
            if (Math.Max(100 - this.CurrentStatus.enchant_resist.resist_debuff, 0) > num4)
            {
                goto Label_014E;
            }
            goto Label_0231;
        Label_014E:
            if (this.BuffAttachments.Find(new Predicate<BuffAttachment>(storeyf.<>m__4A8)) == null)
            {
                goto Label_0170;
            }
            goto Label_0231;
        Label_0170:
            num6 = this.GetActionSkillBuffValue(target.buffType, target.calcType, target.paramType);
            num8 = Math.Max(Math.Abs(target.value) - num6, 0);
            if (num8 == null)
            {
                goto Label_0231;
            }
            if (bRequestOnly == null)
            {
                goto Label_0220;
            }
            if (this.AI == null)
            {
                goto Label_0220;
            }
            if (this.AI.BuffPriorities == null)
            {
                goto Label_0220;
            }
            num9 = 0;
            goto Label_0207;
        Label_01DA:
            if (target.paramType != this.AI.BuffPriorities[num9])
            {
                goto Label_0201;
            }
            *((int*) buff_value) += num8;
            goto Label_021B;
        Label_0201:
            num9 += 1;
        Label_0207:
            if (num9 < ((int) this.AI.BuffPriorities.Length))
            {
                goto Label_01DA;
            }
        Label_021B:
            goto Label_0229;
        Label_0220:
            *((int*) buff_value) += num8;
        Label_0229:
            *((int*) buff_count) += 1;
        Label_0231:
            num += 1;
        Label_0235:
            if (num < effect.targets.Count)
            {
                goto Label_004D;
            }
        Label_0246:
            if (storeyf.skill.ControlChargeTimeValue == null)
            {
                goto Label_0281;
            }
            *((int*) buff_value) += Math.Abs(storeyf.skill.ControlChargeTimeValue);
            *((int*) buff_count) += 1;
        Label_0281:
            return;
        }

        private long GetFreeAbilityIID()
        {
            long num;
            List<AbilityData> list;
            bool flag;
            int num2;
            AbilityData data;
            num = 1L;
            list = new List<AbilityData>();
            list.AddRange(this.mUnitData.BattleAbilitys);
            list.AddRange(this.mAddedAbilitys);
            goto Label_0071;
        Label_002B:
            flag = 0;
            num2 = 0;
            goto Label_005A;
        Label_0034:
            data = list[num2];
            if (data.UniqueID != num)
            {
                goto Label_0056;
            }
            num += 1L;
            flag = 1;
            goto Label_0066;
        Label_0056:
            num2 += 1;
        Label_005A:
            if (num2 < list.Count)
            {
                goto Label_0034;
            }
        Label_0066:
            if (flag != null)
            {
                goto Label_0071;
            }
            goto Label_0080;
        Label_0071:
            if (num < 0x7fffffffffffffffL)
            {
                goto Label_002B;
            }
        Label_0080:
            return num;
        }

        public int GetGoodSleepHpHealValue()
        {
            if (this.IsUnitCondition(0x1000000L) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return Math.Max((this.MaximumStatus.param.hp * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GoodSleepHpHealRate) / 100, 1);
        }

        public int GetGoodSleepMpHealValue()
        {
            return Math.Max((this.MaximumStatus.param.mp * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GoodSleepMpHealRate) / 100, 1);
        }

        public Unit GetGuardTarget()
        {
            if (this.IsDeadCondition() == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (this.CheckExistMap() == null)
            {
                goto Label_003F;
            }
            if (this.IsUnitCondition(4L) != null)
            {
                goto Label_003F;
            }
            if (this.IsUnitCondition(2L) != null)
            {
                goto Label_003F;
            }
            if (this.IsUnitCondition(8L) == null)
            {
                goto Label_0041;
            }
        Label_003F:
            return null;
        Label_0041:
            if (this.mGuardTarget == null)
            {
                goto Label_007D;
            }
            if (this.CheckEnemySide(this.mGuardTarget) != null)
            {
                goto Label_007D;
            }
            if (this.mGuardTarget.IsDeadCondition() != null)
            {
                goto Label_007D;
            }
            if (this.mGuardTarget.CheckExistMap() != null)
            {
                goto Label_007F;
            }
        Label_007D:
            return null;
        Label_007F:
            return this.mGuardTarget;
        }

        public int GetMapBreakNowStage(int hp)
        {
            int num;
            if (this.mBreakObj == null)
            {
                goto Label_002D;
            }
            if (this.mBreakObj.rest_hps == null)
            {
                goto Label_002D;
            }
            if (((int) this.mBreakObj.rest_hps.Length) != null)
            {
                goto Label_002F;
            }
        Label_002D:
            return 0;
        Label_002F:
            num = ((int) this.mBreakObj.rest_hps.Length) - 1;
            goto Label_005F;
        Label_0044:
            if (hp > this.mBreakObj.rest_hps[num])
            {
                goto Label_005B;
            }
            return (num + 1);
        Label_005B:
            num -= 1;
        Label_005F:
            if (num >= 0)
            {
                goto Label_0044;
            }
            return 0;
        }

        public int GetMoveCount(bool bCondOnly)
        {
            int num;
            bool flag;
            int num2;
            CondAttachment attachment;
            int num3;
            CondEffect effect;
            CondEffect effect2;
            if (this.IsEnableMoveCondition(bCondOnly) != null)
            {
                goto Label_000E;
            }
            return 0;
        Label_000E:
            if (this.IsUnitCondition(0x100000L) == null)
            {
                goto Label_018E;
            }
            num = 0xff;
            flag = 0;
            num2 = 0;
            goto Label_016F;
        Label_002E:
            attachment = this.CondAttachments[num2];
            if ((attachment == null) || (attachment.IsFailCondition() == null))
            {
                goto Label_016B;
            }
            if (attachment.ContainsCondition(0x100000L) != null)
            {
                goto Label_0062;
            }
            goto Label_016B;
        Label_0062:
            num3 = 0xff;
            if (attachment.skill == null)
            {
                goto Label_0117;
            }
            effect = attachment.skill.GetCondEffect(0);
            if ((effect == null) || (effect.param.v_donmov <= 0))
            {
                goto Label_00BA;
            }
            num3 = Math.Min(num3, effect.param.v_donmov);
        Label_00BA:
            effect2 = (attachment.user != this) ? null : attachment.skill.GetCondEffect(1);
            if ((effect2 == null) || (effect2.param.v_donmov <= 0))
            {
                goto Label_0151;
            }
            num3 = Math.Min(num3, effect2.param.v_donmov);
            goto Label_0151;
        Label_0117:
            if ((attachment.Param == null) || (attachment.Param.v_donmov <= 0))
            {
                goto Label_0151;
            }
            num3 = Math.Min(num3, attachment.Param.v_donmov);
        Label_0151:
            if (num3 != 0xff)
            {
                goto Label_0160;
            }
            num3 = 1;
        Label_0160:
            num = Math.Min(num, num3);
            flag = 1;
        Label_016B:
            num2 += 1;
        Label_016F:
            if (num2 < this.CondAttachments.Count)
            {
                goto Label_002E;
            }
            return ((flag == null) ? 1 : num);
        Label_018E:
            return this.mCurrentStatus.param.mov;
        }

        public int GetMoveHeight()
        {
            return Math.Max(this.mCurrentStatus.param.jmp, 1);
        }

        public int GetParalyseRate()
        {
            int num;
            bool flag;
            FixParam param;
            int num2;
            CondAttachment attachment;
            int num3;
            CondEffect effect;
            CondEffect effect2;
            if (this.IsUnitCondition(2L) != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            num = 0;
            flag = 0;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            num2 = 0;
            goto Label_0170;
        Label_002A:
            attachment = this.CondAttachments[num2];
            if ((attachment == null) || (attachment.IsFailCondition() == null))
            {
                goto Label_016C;
            }
            if (attachment.ContainsCondition(2L) != null)
            {
                goto Label_005E;
            }
            goto Label_016C;
        Label_005E:
            num3 = 0;
            if (attachment.skill == null)
            {
                goto Label_0111;
            }
            effect = attachment.skill.GetCondEffect(0);
            if ((effect == null) || (effect.param.v_paralyse_rate == null))
            {
                goto Label_00B3;
            }
            num3 = Math.Max(num3, effect.param.v_paralyse_rate);
        Label_00B3:
            effect2 = (attachment.user != this) ? null : attachment.skill.GetCondEffect(1);
            if (effect2 == null)
            {
                goto Label_014D;
            }
            if (effect2.param.v_paralyse_rate == null)
            {
                goto Label_014D;
            }
            num3 = Math.Max(num3, effect2.param.v_paralyse_rate);
            goto Label_014D;
        Label_0111:
            if (attachment.Param == null)
            {
                goto Label_014D;
            }
            if (attachment.Param.v_paralyse_rate == null)
            {
                goto Label_014D;
            }
            num3 = Math.Max(num3, attachment.Param.v_paralyse_rate);
        Label_014D:
            if (num3 != null)
            {
                goto Label_0161;
            }
            num3 = param.ParalysedRate;
        Label_0161:
            flag = 1;
            num = Math.Max(num, num3);
        Label_016C:
            num2 += 1;
        Label_0170:
            if (num2 < this.CondAttachments.Count)
            {
                goto Label_002A;
            }
            if (flag != null)
            {
                goto Label_0193;
            }
            num = param.ParalysedRate;
        Label_0193:
            return num;
        }

        public int GetPoisonDamage()
        {
            FixParam param;
            bool flag;
            int num;
            int num2;
            int num3;
            CondAttachment attachment;
            int num4;
            int num5;
            CondEffect effect;
            CondEffect effect2;
            int num6;
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            flag = 0;
            num = 0;
            num2 = 0;
            num3 = 0;
            goto Label_0264;
        Label_001E:
            attachment = this.CondAttachments[num3];
            if ((attachment == null) || (attachment.IsFailCondition() == null))
            {
                goto Label_025E;
            }
            if (attachment.ContainsCondition(1L) != null)
            {
                goto Label_0053;
            }
            goto Label_025E;
        Label_0053:
            num4 = 0;
            num5 = 0;
            if (attachment.skill == null)
            {
                goto Label_0169;
            }
            effect = attachment.skill.GetCondEffect(0);
            if (effect == null)
            {
                goto Label_00DB;
            }
            if (effect.param.v_poison_rate == null)
            {
                goto Label_00AB;
            }
            num4 = Math.Max(num4, effect.param.v_poison_rate);
        Label_00AB:
            if (effect.param.v_poison_fix == null)
            {
                goto Label_00DB;
            }
            num5 = Math.Max(num5, effect.param.v_poison_fix);
        Label_00DB:
            effect2 = (attachment.user != this) ? null : attachment.skill.GetCondEffect(1);
            if (effect2 == null)
            {
                goto Label_01D5;
            }
            if (effect2.param.v_poison_rate == null)
            {
                goto Label_0134;
            }
            num4 = Math.Max(num4, effect2.param.v_poison_rate);
        Label_0134:
            if (effect2.param.v_poison_fix == null)
            {
                goto Label_01D5;
            }
            num5 = Math.Max(num5, effect2.param.v_poison_fix);
            goto Label_01D5;
        Label_0169:
            if (attachment.Param == null)
            {
                goto Label_01D5;
            }
            if (attachment.Param.v_poison_rate == null)
            {
                goto Label_01A5;
            }
            num4 = Math.Max(num4, attachment.Param.v_poison_rate);
        Label_01A5:
            if (attachment.Param.v_poison_fix == null)
            {
                goto Label_01D5;
            }
            num5 = Math.Max(num5, attachment.Param.v_poison_fix);
        Label_01D5:
            if ((num4 != null) || (num5 != null))
            {
                goto Label_01F0;
            }
            num4 = param.PoisonDamageRate;
        Label_01F0:
            if (attachment.user == null)
            {
                goto Label_024A;
            }
            if (num4 == null)
            {
                goto Label_0220;
            }
            num4 += attachment.user.CurrentStatus[0x19];
        Label_0220:
            if (num5 == null)
            {
                goto Label_024A;
            }
            num5 += (num5 * attachment.user.CurrentStatus[0x19]) / 100;
        Label_024A:
            flag = 1;
            num = Math.Max(num, num4);
            num2 = Math.Max(num2, num5);
        Label_025E:
            num3 += 1;
        Label_0264:
            if (num3 < this.CondAttachments.Count)
            {
                goto Label_001E;
            }
            if (flag != null)
            {
                goto Label_0288;
            }
            num = param.PoisonDamageRate;
        Label_0288:
            num6 = Math.Max((num == null) ? 0 : ((this.MaximumStatus.param.hp * num) / 100), 1);
            return Math.Max(num2, num6);
        }

        public Unit GetRageTarget()
        {
            return ((this.IsUnitCondition(0x200000L) == null) ? null : this.mRageTarget);
        }

        public int GetSearchRange()
        {
            return this.mSearched;
        }

        public unsafe SkillData GetSkillData(string iname)
        {
            SkillData data;
            int num;
            ConceptCardData data2;
            List<SkillData> list;
            SkillData data3;
            List<SkillData>.Enumerator enumerator;
            SkillData data4;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            data = this.GetAttackSkill();
            if (data == null)
            {
                goto Label_002D;
            }
            if ((iname == data.SkillID) == null)
            {
                goto Label_002D;
            }
            return data;
        Label_002D:
            num = 0;
            goto Label_0061;
        Label_0034:
            if ((iname == this.BattleSkills[num].SkillID) == null)
            {
                goto Label_005D;
            }
            return this.BattleSkills[num];
        Label_005D:
            num += 1;
        Label_0061:
            if (num < this.BattleSkills.Count)
            {
                goto Label_0034;
            }
            data2 = this.UnitData.ConceptCard;
            if (data2 == null)
            {
                goto Label_00E0;
            }
            enumerator = data2.GetEnableCardSkills(this.UnitData).GetEnumerator();
        Label_0099:
            try
            {
                goto Label_00C2;
            Label_009E:
                data3 = &enumerator.Current;
                if ((iname == data3.SkillID) == null)
                {
                    goto Label_00C2;
                }
                data4 = data3;
                goto Label_00E2;
            Label_00C2:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_009E;
                }
                goto Label_00E0;
            }
            finally
            {
            Label_00D3:
                ((List<SkillData>.Enumerator) enumerator).Dispose();
            }
        Label_00E0:
            return null;
        Label_00E2:
            return data4;
        }

        public SkillData GetSkillForUseCount(string skill_iname, int offs)
        {
            SkillData[] dataArray;
            SkillData data;
            int num;
            SkillData data2;
            dataArray = Enumerable.ToArray<SkillData>(this.mSkillUseCount.Keys);
            data = null;
            num = 0;
            goto Label_005B;
        Label_001A:
            data2 = dataArray[num];
            if (data2 != null)
            {
                goto Label_0029;
            }
            goto Label_0057;
        Label_0029:
            if ((data2.SkillParam.iname == skill_iname) == null)
            {
                goto Label_0057;
            }
            data = data2;
            if (offs > 0)
            {
                goto Label_004D;
            }
            goto Label_0064;
        Label_004D:
            offs -= 1;
        Label_0057:
            num += 1;
        Label_005B:
            if (num < ((int) dataArray.Length))
            {
                goto Label_001A;
            }
        Label_0064:
            return data;
        }

        public Dictionary<SkillData, OInt> GetSkillUseCount()
        {
            return this.mSkillUseCount;
        }

        public OInt GetSkillUseCount(SkillData skill)
        {
            if (this.mSkillUseCount.ContainsKey(skill) != null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            return this.mSkillUseCount[skill];
        }

        public OInt GetSkillUseCountMax(SkillData skill)
        {
            return (skill.SkillParam.count + this.MaximumStatus[0x18]);
        }

        public int GetSkillUsedCost(SkillData skill)
        {
            int num;
            num = skill.Cost;
            if (skill.EffectType == 14)
            {
                goto Label_005F;
            }
            if (skill.IsNormalAttack() != null)
            {
                goto Label_005F;
            }
            if (skill.IsItem() != null)
            {
                goto Label_005F;
            }
            num = Math.Max(num + this.CurrentStatus[0x2a], 0);
            num += (num * this.CurrentStatus[14]) / 100;
        Label_005F:
            return num;
        }

        public int GetStartGems()
        {
            int num;
            int num2;
            UnitJobOverwriteParam param;
            num = 0;
            num2 = 100;
            if (this.Job != null)
            {
                goto Label_003E;
            }
            num2 += (this.UnitParam.no_job_status == null) ? 0 : this.UnitParam.no_job_status.inimp;
            goto Label_0077;
        Label_003E:
            param = this.UnitData.GetUnitJobOverwriteParam(this.Job.JobID);
            if (param == null)
            {
                goto Label_0069;
            }
            num2 += param.mInimp;
            goto Label_0077;
        Label_0069:
            num2 += this.Job.GetJobRankInitJewelRate();
        Label_0077:
            num = (this.MaximumStatus.param.mp * num2) / 100;
            return Math.Max(num + this.CurrentStatus.param.imp, 0);
        }

        public string[] GetTags()
        {
            return this.UnitParam.tags;
        }

        public string GetUnitSkinVoiceCueName(int jobIndex)
        {
            return this.UnitData.GetUnitSkinVoiceCueName(jobIndex);
        }

        public string GetUnitSkinVoiceSheetName(int jobIndex)
        {
            return this.UnitData.GetUnitSkinVoiceSheetName(jobIndex);
        }

        public unsafe Unit GetUnitUseCollaboSkill(SkillData skill, bool is_use_tuc)
        {
            int num;
            int num2;
            SceneBattle battle;
            TacticsUnitController controller;
            IntVector2 vector;
            num = this.x;
            num2 = this.y;
            if (is_use_tuc == null)
            {
                goto Label_0066;
            }
            battle = SceneBattle.Instance;
            if ((battle == null) == null)
            {
                goto Label_0028;
            }
            return null;
        Label_0028:
            controller = battle.FindUnitController(this);
            if ((controller != null) == null)
            {
                goto Label_0066;
            }
            if (this.IsUnitFlag(2) != null)
            {
                goto Label_0066;
            }
            vector = battle.CalcCoord(controller.CenterPosition);
            num = &vector.x;
            num2 = &vector.y;
        Label_0066:
            return this.GetUnitUseCollaboSkill(skill, num, num2);
        }

        public Unit GetUnitUseCollaboSkill(SkillData skill, int ux, int uy)
        {
            string str;
            SceneBattle battle;
            BattleCore core;
            Grid grid;
            int num;
            int num2;
            Grid grid2;
            Unit unit;
            str = CollaboSkillParam.GetPartnerIname(this.mUnitData.UnitParam.iname, skill.SkillParam.iname);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_002E;
            }
            return null;
        Label_002E:
            battle = SceneBattle.Instance;
            if ((battle == null) == null)
            {
                goto Label_0042;
            }
            return null;
        Label_0042:
            core = battle.Battle;
            if (core != null)
            {
                goto Label_0051;
            }
            return null;
        Label_0051:
            if (core.CurrentMap != null)
            {
                goto Label_005E;
            }
            return null;
        Label_005E:
            grid = core.CurrentMap[ux, uy];
            num = grid.y - 1;
            goto Label_0172;
        Label_007B:
            num2 = grid.x - 1;
            goto Label_015D;
        Label_008A:
            if (num2 != grid.x)
            {
                goto Label_00A9;
            }
            if (num != grid.y)
            {
                goto Label_00A9;
            }
            goto Label_0157;
        Label_00A9:
            grid2 = core.CurrentMap[num2, num];
            if (grid2 != null)
            {
                goto Label_00C6;
            }
            goto Label_0157;
        Label_00C6:
            unit = core.FindUnitAtGrid(grid2);
            if (unit != null)
            {
                goto Label_00DC;
            }
            goto Label_0157;
        Label_00DC:
            if (unit.mSide == this.mSide)
            {
                goto Label_00F3;
            }
            goto Label_0157;
        Label_00F3:
            if ((unit.UnitParam.iname != str) == null)
            {
                goto Label_010F;
            }
            goto Label_0157;
        Label_010F:
            if (unit.GetSkillForUseCount(skill.SkillParam.iname, 0) != null)
            {
                goto Label_012C;
            }
            goto Label_0157;
        Label_012C:
            if (Math.Abs(grid.height - grid2.height) > skill.SkillParam.CollaboHeight)
            {
                goto Label_0157;
            }
            return unit;
        Label_0157:
            num2 += 1;
        Label_015D:
            if (num2 <= (grid.x + 1))
            {
                goto Label_008A;
            }
            num += 1;
        Label_0172:
            if (num <= (grid.y + 1))
            {
                goto Label_007B;
            }
            return null;
        }

        public EElement GetWeakElement()
        {
            return SRPG.UnitParam.GetWeakElement(this.Element);
        }

        public void Heal(int value)
        {
            this.CurrentStatus.param.hp = Math.Min(this.CurrentStatus.param.hp + value, this.MaximumStatus.param.hp);
            return;
        }

        public void InitializeSkillUseCount()
        {
            int num;
            AbilityData data;
            int num2;
            SkillData data2;
            int num3;
            SkillData data3;
            this.mSkillUseCount.Clear();
            num = 0;
            goto Label_0090;
        Label_0012:
            data = this.BattleAbilitys[num];
            num2 = 0;
            goto Label_007B;
        Label_0026:
            data2 = data.Skills[num2];
            if (data2.SkillParam.count > 0)
            {
                goto Label_004E;
            }
            goto Label_0077;
        Label_004E:
            if (this.mSkillUseCount.ContainsKey(data2) == null)
            {
                goto Label_0064;
            }
            goto Label_0077;
        Label_0064:
            this.mSkillUseCount.Add(data2, this.GetSkillUseCountMax(data2));
        Label_0077:
            num2 += 1;
        Label_007B:
            if (num2 < data.Skills.Count)
            {
                goto Label_0026;
            }
            num += 1;
        Label_0090:
            if (num < this.BattleAbilitys.Count)
            {
                goto Label_0012;
            }
            num3 = 0;
            goto Label_0106;
        Label_00A9:
            data3 = this.mAddedSkills[num3];
            if (data3.SkillParam.count > 0)
            {
                goto Label_00D4;
            }
            goto Label_0100;
        Label_00D4:
            if (this.mSkillUseCount.ContainsKey(data3) == null)
            {
                goto Label_00EB;
            }
            goto Label_0100;
        Label_00EB:
            this.mSkillUseCount.Add(data3, this.GetSkillUseCountMax(data3));
        Label_0100:
            num3 += 1;
        Label_0106:
            if (num3 < this.mAddedSkills.Count)
            {
                goto Label_00A9;
            }
            return;
        }

        public bool IsAIActionTable()
        {
            if (this.mAIActionTable.actions.Count != null)
            {
                goto Label_0017;
            }
            return 0;
        Label_0017:
            if (this.mAIActionIndex < 0)
            {
                goto Label_0048;
            }
            if (this.mAIActionTable.actions.Count > this.mAIActionIndex)
            {
                goto Label_004A;
            }
        Label_0048:
            return 0;
        Label_004A:
            return 1;
        }

        public bool IsAIPatrolTable()
        {
            if (this.mAIPatrolTable == null)
            {
                goto Label_001B;
            }
            if (this.mAIPatrolTable.routes != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return 0;
        Label_001D:
            if (this.mAIPatrolIndex < 0)
            {
                goto Label_004B;
            }
            if (((int) this.mAIPatrolTable.routes.Length) > this.mAIPatrolIndex)
            {
                goto Label_004D;
            }
        Label_004B:
            return 0;
        Label_004D:
            if (this.mAIPatrolTable.keeped != null)
            {
                goto Label_006B;
            }
            if (this.IsUnitFlag(8) == null)
            {
                goto Label_006B;
            }
            return 0;
        Label_006B:
            return 1;
        }

        public bool IsAutoCureCondition(EUnitCondition condition)
        {
            EUnitCondition condition2;
            condition2 = condition;
            if (condition2 == 0x20L)
            {
                goto Label_0040;
            }
            if (condition2 == 0x400L)
            {
                goto Label_0040;
            }
            if (condition2 == 0x800L)
            {
                goto Label_0040;
            }
            if (condition2 == 0x2000L)
            {
                goto Label_0040;
            }
            if (condition2 == 0x400000L)
            {
                goto Label_0040;
            }
            goto Label_0042;
        Label_0040:
            return 0;
        Label_0042:
            return 1;
        }

        public bool IsClockCureCondition(EUnitCondition condition)
        {
            EUnitCondition condition2;
            condition2 = condition;
            if (condition2 == 0x10000L)
            {
                goto Label_002B;
            }
            if (condition2 == 0x20000L)
            {
                goto Label_002B;
            }
            if (condition2 == 0x40000L)
            {
                goto Label_002B;
            }
            goto Label_002D;
        Label_002B:
            return 1;
        Label_002D:
            return 0;
        }

        public bool IsCommandFlag(EUnitCommandFlag tgt)
        {
            return (((this.mCommandFlag & tgt) == 0) == 0);
        }

        public bool IsContainsJudgeHpLists(SkillData skill)
        {
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return this.mJudgeHpLists.Contains(skill);
        }

        public bool IsCurseUnitCondition(EUnitCondition condition)
        {
            int num;
            CondAttachment attachment;
            num = 0;
            goto Label_003C;
        Label_0007:
            attachment = this.CondAttachments[num];
            if (attachment.IsFailCondition() == null)
            {
                goto Label_0038;
            }
            if (attachment.Condition != condition)
            {
                goto Label_0038;
            }
            if (attachment.IsCurse == null)
            {
                goto Label_0038;
            }
            return 1;
        Label_0038:
            num += 1;
        Label_003C:
            if (num < this.CondAttachments.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public bool IsDeadCondition()
        {
            if (this.IsDead != null)
            {
                goto Label_0019;
            }
            if (this.IsUnitCondition(0x20L) == null)
            {
                goto Label_001B;
            }
        Label_0019:
            return 1;
        Label_001B:
            return 0;
        }

        public bool IsDisableGimmick()
        {
            if (this.IsGimmick != null)
            {
                goto Label_000D;
            }
            return 1;
        Label_000D:
            if (this.IsBreakObj == null)
            {
                goto Label_001F;
            }
            return this.IsDead;
        Label_001F:
            if (this.mEventTrigger == null)
            {
                goto Label_0039;
            }
            return (this.mEventTrigger.Count == 0);
        Label_0039:
            return 0;
        }

        public bool IsDisableUnitCondition(EUnitCondition condition)
        {
            return (((this.mDisableCondition & condition) == 0L) == 0);
        }

        public bool IsDropDirection()
        {
            return this.mDropDirection;
        }

        public bool IsDying()
        {
            int num;
            int num2;
            num = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam.HpDyingRate;
            num2 = (this.MaximumStatus.param.hp * num) / 100;
            return ((num2 < this.CurrentStatus.param.hp) == 0);
        }

        public bool IsEnableActionCondition()
        {
            return (((this.IsEnableAttackCondition(0) != null) || (this.IsEnableSkillCondition(0) != null)) ? 1 : this.IsEnableItemCondition(0));
        }

        public bool IsEnableAttackCondition(bool bCondOnly)
        {
            if (bCondOnly != null)
            {
                goto Label_0014;
            }
            if (this.IsUnitFlag(4) == null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            if (this.CheckExistMap() == null)
            {
                goto Label_0079;
            }
            if (this.IsUnitFlag(0x200) != null)
            {
                goto Label_0079;
            }
            if (this.IsUnitCondition(4L) != null)
            {
                goto Label_0079;
            }
            if (this.IsUnitCondition(8L) != null)
            {
                goto Label_0079;
            }
            if (this.IsUnitCondition(0x20L) != null)
            {
                goto Label_0079;
            }
            if (this.IsUnitCondition(0x200L) != null)
            {
                goto Label_0079;
            }
            if (this.IsUnitCondition(0x10000L) == null)
            {
                goto Label_007B;
            }
        Label_0079:
            return 0;
        Label_007B:
            if (this.GetAttackSkill() != null)
            {
                goto Label_0088;
            }
            return 0;
        Label_0088:
            return 1;
        }

        public bool IsEnableAutoHealCondition()
        {
            if (this.IsDeadCondition() == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.mCastSkill == null)
            {
                goto Label_002B;
            }
            if (this.mCastSkill.CastType != 2)
            {
                goto Label_002B;
            }
            return 0;
        Label_002B:
            return 1;
        }

        public bool IsEnableAutoMode()
        {
            if (this.IsUnitFlag(0x1000) == null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            if (this.IsControl == null)
            {
                goto Label_0033;
            }
            if (this.IsEntry == null)
            {
                goto Label_0033;
            }
            if (this.IsSub == null)
            {
                goto Label_0035;
            }
        Label_0033:
            return 0;
        Label_0035:
            if (this.IsDeadCondition() == null)
            {
                goto Label_0042;
            }
            return 0;
        Label_0042:
            if (this.IsUnitCondition(0x10L) == null)
            {
                goto Label_0074;
            }
            if (this.IsUnitCondition(0x400L) == null)
            {
                goto Label_0074;
            }
            if (this.IsUnitCondition(0x1000L) == null)
            {
                goto Label_0074;
            }
            return 0;
        Label_0074:
            if (this.GetRageTarget() == null)
            {
                goto Label_0081;
            }
            return 0;
        Label_0081:
            return 1;
        }

        public bool IsEnableBuffEffect(BuffTypes type)
        {
            if (type != null)
            {
                goto Label_002A;
            }
            return (((this.IsUnitCondition(0x4000L) != null) ? 1 : this.IsDisableUnitCondition(0x4000L)) == 0);
        Label_002A:
            if (type != 1)
            {
                goto Label_0055;
            }
            return (((this.IsUnitCondition(0x8000L) != null) ? 1 : this.IsDisableUnitCondition(0x8000L)) == 0);
        Label_0055:
            return 0;
        }

        public bool IsEnableControlCondition()
        {
            if (this.CheckExistMap() == null)
            {
                goto Label_005C;
            }
            if (this.IsUnitFlag(0x1000) != null)
            {
                goto Label_005C;
            }
            if (this.IsUnitCondition(0x10L) != null)
            {
                goto Label_005C;
            }
            if (this.IsUnitCondition(0x400L) != null)
            {
                goto Label_005C;
            }
            if (this.IsUnitCondition(0x1000L) != null)
            {
                goto Label_005C;
            }
            if (this.IsUnitCondition(0x200000L) == null)
            {
                goto Label_005E;
            }
        Label_005C:
            return 0;
        Label_005E:
            return 1;
        }

        public bool IsEnableGimmickCondition()
        {
            return 1;
        }

        public bool IsEnableHelpCondition()
        {
            if (this.CheckExistMap() == null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitFlag(0x200) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(4L) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(8L) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(0x20L) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(0x10L) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(0x400L) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(0x200L) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(0x1000L) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(0x10000L) != null)
            {
                goto Label_00A6;
            }
            if (this.IsUnitCondition(0x200000L) == null)
            {
                goto Label_00A8;
            }
        Label_00A6:
            return 0;
        Label_00A8:
            if (this.GetAttackSkill() != null)
            {
                goto Label_00B5;
            }
            return 0;
        Label_00B5:
            return 1;
        }

        public bool IsEnableItemCondition(bool bCondOnly)
        {
            if (bCondOnly != null)
            {
                goto Label_0014;
            }
            if (this.IsUnitFlag(4) == null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            if (this.CheckExistMap() == null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitFlag(0x200) != null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitCondition(4L) != null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitCondition(8L) != null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitCondition(0x20L) != null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitCondition(0x10L) != null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitCondition(0x400L) != null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitCondition(0x1000L) != null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitCondition(0x10000L) != null)
            {
                goto Label_00A9;
            }
            if (this.IsUnitCondition(0x200000L) == null)
            {
                goto Label_00AB;
            }
        Label_00A9:
            return 0;
        Label_00AB:
            return 1;
        }

        public bool IsEnableMoveCondition(bool bCondOnly)
        {
            if (this.IsUnitFlag(0x400) == null)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            if (bCondOnly != null)
            {
                goto Label_0026;
            }
            if (this.IsUnitFlag(2) == null)
            {
                goto Label_0026;
            }
            return 0;
        Label_0026:
            if (this.CheckExistMap() == null)
            {
                goto Label_008B;
            }
            if (this.IsUnitFlag(0x200) != null)
            {
                goto Label_008B;
            }
            if (this.IsUnitCondition(4L) != null)
            {
                goto Label_008B;
            }
            if (this.IsUnitCondition(8L) != null)
            {
                goto Label_008B;
            }
            if (this.IsUnitCondition(0x20L) != null)
            {
                goto Label_008B;
            }
            if (this.IsUnitCondition(0x100L) != null)
            {
                goto Label_008B;
            }
            if (this.IsUnitCondition(0x10000L) == null)
            {
                goto Label_008D;
            }
        Label_008B:
            return 0;
        Label_008D:
            return 1;
        }

        public bool IsEnablePlayAnimCondition()
        {
            if (this.IsUnitCondition(0x10000L) != null)
            {
                goto Label_001F;
            }
            if (this.IsUnitCondition(0x20L) == null)
            {
                goto Label_0021;
            }
        Label_001F:
            return 0;
        Label_0021:
            return 1;
        }

        public bool IsEnableReactionCondition()
        {
            if (this.CheckExistMap() == null)
            {
                goto Label_0070;
            }
            if (this.IsJump != null)
            {
                goto Label_0070;
            }
            if (this.IsUnitFlag(0x200) != null)
            {
                goto Label_0070;
            }
            if (this.IsUnitCondition(4L) != null)
            {
                goto Label_0070;
            }
            if (this.IsUnitCondition(0x10000L) != null)
            {
                goto Label_0070;
            }
            if (this.IsUnitCondition(8L) != null)
            {
                goto Label_0070;
            }
            if (this.IsUnitCondition(0x20L) != null)
            {
                goto Label_0070;
            }
            if (this.IsUnitCondition(0x80L) == null)
            {
                goto Label_0072;
            }
        Label_0070:
            return 0;
        Label_0072:
            return 1;
        }

        public bool IsEnableReactionSkill(SkillData skill)
        {
            ESkillTiming timing;
            if (skill.IsReactionSkill() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (skill.SkillParam.count <= 0)
            {
                goto Label_0036;
            }
            if (this.GetSkillUseCount(skill) != null)
            {
                goto Label_0036;
            }
            return 0;
        Label_0036:
            switch ((skill.Timing - 4))
            {
                case 0:
                    goto Label_005A;

                case 1:
                    goto Label_005A;

                case 2:
                    goto Label_005A;

                case 3:
                    goto Label_005A;
            }
            goto Label_005F;
        Label_005A:
            goto Label_0061;
        Label_005F:
            return 0;
        Label_0061:
            if (this.IsEnableReactionCondition() != null)
            {
                goto Label_006E;
            }
            return 0;
        Label_006E:
            if (skill.IsDamagedSkill() == null)
            {
                goto Label_008C;
            }
            if (this.IsUnitCondition(0x200L) == null)
            {
                goto Label_008C;
            }
            return 0;
        Label_008C:
            return 1;
        }

        public bool IsEnableSelectDirectionCondition()
        {
            if (this.CheckExistMap() == null)
            {
                goto Label_0044;
            }
            if (this.IsUnitCondition(4L) != null)
            {
                goto Label_0044;
            }
            if (this.IsUnitCondition(0x10000L) != null)
            {
                goto Label_0044;
            }
            if (this.IsUnitCondition(8L) != null)
            {
                goto Label_0044;
            }
            if (this.IsUnitCondition(0x20L) == null)
            {
                goto Label_0046;
            }
        Label_0044:
            return 0;
        Label_0046:
            return 1;
        }

        public bool IsEnableSkillCondition(bool bCondOnly)
        {
            if (bCondOnly != null)
            {
                goto Label_0014;
            }
            if (this.IsUnitFlag(4) == null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            if (this.CheckExistMap() == null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitFlag(0x200) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(4L) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(8L) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(0x20L) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(0x10L) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(0x400L) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(0x80L) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(0x1000L) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(0x10000L) != null)
            {
                goto Label_00BA;
            }
            if (this.IsUnitCondition(0x200000L) == null)
            {
                goto Label_00BC;
            }
        Label_00BA:
            return 0;
        Label_00BC:
            return 1;
        }

        public bool IsEnableSteal()
        {
            if (this.IsGimmick != null)
            {
                goto Label_0016;
            }
            if (this.Steal != null)
            {
                goto Label_0018;
            }
        Label_0016:
            return 0;
        Label_0018:
            return 1;
        }

        public bool IsJudgeHP(SkillData skill)
        {
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            return skill.IsJudgeHp(this);
        }

        public bool IsJumpBreakCondition()
        {
            if (this.IsUnitCondition(0x6112bcL) == null)
            {
                goto Label_0013;
            }
            return 1;
        Label_0013:
            return 0;
        }

        public bool IsJumpBreakNoMotionCondition()
        {
            if (this.IsUnitCondition(0x410028L) == null)
            {
                goto Label_0013;
            }
            return 1;
        Label_0013:
            return 0;
        }

        public bool IsPassiveUnitCondition(EUnitCondition condition)
        {
            int num;
            CondAttachment attachment;
            num = 0;
            goto Label_005B;
        Label_0007:
            attachment = this.CondAttachments[num];
            if (attachment != null)
            {
                goto Label_001F;
            }
            goto Label_0057;
        Label_001F:
            if (attachment.IsPassive != null)
            {
                goto Label_0034;
            }
            goto Label_0057;
        Label_0034:
            if (attachment.IsFailCondition() != null)
            {
                goto Label_0044;
            }
            goto Label_0057;
        Label_0044:
            if (attachment.Condition == condition)
            {
                goto Label_0055;
            }
            goto Label_0057;
        Label_0055:
            return 1;
        Label_0057:
            num += 1;
        Label_005B:
            if (num < this.CondAttachments.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        private bool isSameBuffAttachment(BuffAttachment sba, BuffAttachment dba)
        {
            if (sba == null)
            {
                goto Label_00BE;
            }
            if (dba == null)
            {
                goto Label_00BE;
            }
            if (sba.skill == null)
            {
                goto Label_00BE;
            }
            if (dba.skill == null)
            {
                goto Label_00BE;
            }
            if ((sba.skill.SkillID == dba.skill.SkillID) == null)
            {
                goto Label_00BE;
            }
            if (sba.BuffType != dba.BuffType)
            {
                goto Label_00BE;
            }
            if (sba.CalcType != dba.CalcType)
            {
                goto Label_00BE;
            }
            if (sba.CheckTiming != dba.CheckTiming)
            {
                goto Label_00BE;
            }
            if (sba.Param == null)
            {
                goto Label_00BE;
            }
            if (dba.Param == null)
            {
                goto Label_00BE;
            }
            if ((sba.Param.iname == dba.Param.iname) == null)
            {
                goto Label_00BE;
            }
            if (sba.IsNegativeValueIsBuff != dba.IsNegativeValueIsBuff)
            {
                goto Label_00BE;
            }
            return 1;
        Label_00BE:
            return 0;
        }

        public bool IsUnitCondition(EUnitCondition condition)
        {
            return (((this.mCurrentCondition & condition) == 0L) == 0);
        }

        public bool IsUnitConditionDamage()
        {
            if (this.IsUnitCondition(1L) == null)
            {
                goto Label_000F;
            }
            return 1;
        Label_000F:
            return 0;
        }

        public bool IsUnitFlag(EUnitFlag tgt)
        {
            return (((this.mUnitFlag & tgt) == 0) == 0);
        }

        public bool IsUseSkillCollabo(SkillData skill, bool is_use_tuc)
        {
            Unit unit;
            int num;
            if (skill.IsCollabo != null)
            {
                goto Label_0012;
            }
            return 1;
        Label_0012:
            unit = this.GetUnitUseCollaboSkill(skill, is_use_tuc);
            if (unit != null)
            {
                goto Label_0023;
            }
            return 0;
        Label_0023:
            num = unit.Gems - unit.GetSkillUsedCost(skill);
            return ((num < 0) == 0);
        }

        public void LoadBattleVoice()
        {
            string str;
            string str2;
            string str3;
            if (this.mBattleVoice == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            str = this.GetUnitSkinVoiceSheetName(-1);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            str2 = "VO_" + str;
            str3 = this.GetUnitSkinVoiceCueName(-1) + "_";
            this.mBattleVoice = new MySound.Voice(str2, str, str3, 0);
            return;
        }

        public void NextAIAction()
        {
            if (this.IsAIActionTable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mAIActionIndex = OInt.op_Increment(this.mAIActionIndex);
            this.mAIActionTurnCount = 0;
            if (this.mAIActionTable.looped == null)
            {
                goto Label_0060;
            }
            this.mAIActionIndex = this.mAIActionIndex % this.mAIActionTable.actions.Count;
        Label_0060:
            return;
        }

        public void NextPatrolPoint()
        {
            if (this.IsAIPatrolTable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mAIPatrolIndex = OInt.op_Increment(this.mAIPatrolIndex);
            if (this.mAIPatrolTable.looped == null)
            {
                goto Label_0051;
            }
            this.mAIPatrolIndex = this.mAIPatrolIndex % ((int) this.mAIPatrolTable.routes.Length);
        Label_0051:
            return;
        }

        public void NotifyActionEnd()
        {
            FixParam param;
            this.UpdateAbilityChange();
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            this.mChargeTime -= param.ChargeTimeDecWait;
            if (this.IsCommandFlag(1) == null)
            {
                goto Label_0066;
            }
            this.mChargeTime -= param.ChargeTimeDecMove;
        Label_0066:
            if (this.IsCommandFlag(2) == null)
            {
                goto Label_0094;
            }
            this.mChargeTime -= param.ChargeTimeDecAction;
        Label_0094:
            this.mChargeTime = Math.Max(this.mChargeTime, 0);
            this.CalcCurrentStatus(0, 0);
            return;
        }

        public void NotifyActionEndAfter(List<Unit> others)
        {
            int num;
            Unit unit;
            if (others == null)
            {
                goto Label_0077;
            }
            num = 0;
            goto Label_006B;
        Label_000D:
            unit = others[num];
            unit.UpdateCondEffectTurnCount(8, this);
            unit.UpdateCondEffects();
            unit.UpdateBuffEffectTurnCount(8, this);
            unit.UpdateBuffEffectTurnCount(7, this);
            unit.UpdateBuffEffects();
            unit.setRelatedBuff(unit.x, unit.y, 0);
            TrickData.MomentBuff(unit, unit.x, unit.y, 7);
            unit.CalcCurrentStatus(0, 0);
            num += 1;
        Label_006B:
            if (num < others.Count)
            {
                goto Label_000D;
            }
        Label_0077:
            return;
        }

        public void NotifyActionStart(List<Unit> others)
        {
            int num;
            Unit unit;
            OInt num2;
            if (others == null)
            {
                goto Label_0094;
            }
            num = 0;
            goto Label_0088;
        Label_000D:
            unit = others[num];
            unit.UpdateCondEffectTurnCount(0, this);
            unit.UpdateCondEffects();
            unit.UpdateBuffEffectTurnCount(0, this);
            unit.UpdateBuffEffectTurnCount(7, this);
            unit.UpdateBuffEffectTurnCount(9, this);
            unit.UpdateBuffEffects();
            unit.setRelatedBuff(unit.x, unit.y, 0);
            TrickData.MomentBuff(unit, unit.x, unit.y, 7);
            TrickData.MomentBuff(unit, unit.x, unit.y, 9);
            unit.CalcCurrentStatus(0, 0);
            num += 1;
        Label_0088:
            if (num < others.Count)
            {
                goto Label_000D;
            }
        Label_0094:
            this.UpdateShieldTurn();
            this.UpdateGuardTurn();
            this.UpdateDeathSentence();
            if (this.mRageTarget == null)
            {
                goto Label_00CF;
            }
            if (this.mRageTarget.IsDeadCondition() == null)
            {
                goto Label_00CF;
            }
            this.CureCondEffects(0x200000L, 1, 1);
        Label_00CF:
            this.startX = this.x;
            this.startY = this.y;
            this.startDir = this.Direction;
            if (this.IsDead != null)
            {
                goto Label_0173;
            }
            if (this.IsEntry == null)
            {
                goto Label_0173;
            }
            if (this.IsUnitFlag(0x4000) != null)
            {
                goto Label_0173;
            }
            if (this.IsPartyMember == null)
            {
                goto Label_0167;
            }
            if (this.IsUnitFlag(0x8000) != null)
            {
                goto Label_0167;
            }
            if (SceneBattle.Instance.Battle.IsMultiPlay == null)
            {
                goto Label_015C;
            }
            if (SceneBattle.Instance.Battle.MultiFinishLoad == null)
            {
                goto Label_0167;
            }
        Label_015C:
            this.PlayBattleVoice("battle_0030");
        Label_0167:
            this.SetUnitFlag(0x4000, 1);
        Label_0173:
            this.SetUnitFlag(2, this.mMoveWaitTurn > 0);
            this.SetUnitFlag(4, 0);
            this.SetUnitFlag(0x80, 0);
            this.SetUnitFlag(0x100, 0);
            this.SetUnitFlag(0x200, 0);
            this.SetUnitFlag(0x400, 0);
            this.SetCommandFlag(1, 0);
            this.SetCommandFlag(2, 0);
            this.mMoveWaitTurn = Math.Max(this.mMoveWaitTurn = OInt.op_Decrement(this.mMoveWaitTurn), 0);
            this.mActionCount = OInt.op_Increment(this.mActionCount);
            if (this.mAIActionTable == null)
            {
                goto Label_026D;
            }
            if (this.mAIActionTable.actions == null)
            {
                goto Label_026D;
            }
            if (this.mAIActionTable.actions.Count <= 0)
            {
                goto Label_026D;
            }
            if (this.mAIActionTable.actions.Count <= this.mAIActionIndex)
            {
                goto Label_026D;
            }
            this.mAIActionTurnCount = OInt.op_Increment(this.mAIActionTurnCount);
        Label_026D:
            return;
        }

        public void NotifyCommandStart()
        {
            this.CalcCurrentStatus(0, 0);
            return;
        }

        public void NotifyContinue()
        {
            this.CurrentStatus.param.hp = this.MaximumStatus.param.hp;
            this.CurrentStatus.param.mp = this.MaximumStatus.param.mp;
            this.CancelCastSkill();
            this.CancelGuradTarget();
            this.NotifyMapStart();
            this.mChargeTime = this.ChargeTimeMax;
            return;
        }

        public void NotifyMapStart()
        {
            int num;
            int num2;
            this.SetUnitFlag(2, 0);
            this.SetUnitFlag(4, 0);
            this.SetUnitFlag(0x100, 0);
            this.SetUnitFlag(0x200, 0);
            this.SetUnitFlag(0x400, 0);
            this.SetUnitFlag(0x2000, 0);
            this.SetUnitFlag(0x20000, 0);
            this.SetUnitFlag(0x100000, 0);
            this.SetUnitFlag(0x400000, 0);
            this.SetUnitFlag(0x800000, 0);
            num = 0;
            goto Label_00DC;
        Label_0077:
            if (this.BuffAttachments[num].IsPassive != null)
            {
                goto Label_00D8;
            }
            if (this.BuffAttachments[num].skill == null)
            {
                goto Label_00C8;
            }
            if (this.BuffAttachments[num].skill.IsPassiveSkill() == null)
            {
                goto Label_00C8;
            }
            goto Label_00D8;
        Label_00C8:
            this.BuffAttachments.RemoveAt(num--);
        Label_00D8:
            num += 1;
        Label_00DC:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0077;
            }
            num2 = 0;
            goto Label_0159;
        Label_00F4:
            if (this.CondAttachments[num2].IsPassive != null)
            {
                goto Label_0155;
            }
            if (this.CondAttachments[num2].skill == null)
            {
                goto Label_0145;
            }
            if (this.CondAttachments[num2].skill.IsPassiveSkill() == null)
            {
                goto Label_0145;
            }
            goto Label_0155;
        Label_0145:
            this.CondAttachments.RemoveAt(num2--);
        Label_0155:
            num2 += 1;
        Label_0159:
            if (num2 < this.CondAttachments.Count)
            {
                goto Label_00F4;
            }
            this.ClearMhmDamage();
            this.mAbilityChangeLists.Clear();
            this.RefleshBattleAbilitysAndSkills();
            this.UpdateBuffEffects();
            this.UpdateCondEffects();
            this.CalcCurrentStatus(0, 0);
            this.InitializeSkillUseCount();
            this.ClearJudgeHpLists();
            return;
        }

        public void PlayBattleVoice(string cueID)
        {
            if (this.mBattleVoice != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mBattleVoice.Play(cueID, 0f, 0);
            return;
        }

        public void PopCastSkill()
        {
            if (this.mPushCastSkill != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mCastSkill = this.mPushCastSkill;
            this.mPushCastSkill = null;
            return;
        }

        public void PushCastSkill()
        {
            this.mPushCastSkill = this.mCastSkill;
            this.mCastSkill = null;
            return;
        }

        private unsafe void ReflectMhmDamage()
        {
            UnitMhmDamage damage;
            List<UnitMhmDamage>.Enumerator enumerator;
            enumerator = this.mMhmDamageLists.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0031;
            Label_0011:
                damage = &enumerator.Current;
                this.ReflectMhmDamage(damage.mType, damage.mDamage, 0);
            Label_0031:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_004E;
            }
            finally
            {
            Label_0042:
                ((List<UnitMhmDamage>.Enumerator) enumerator).Dispose();
            }
        Label_004E:
            return;
        }

        public void ReflectMhmDamage(eTypeMhmDamage type, int damage, bool is_allow_to_dead)
        {
            bool flag;
            eTypeMhmDamage damage2;
            damage2 = type;
            if (damage2 == null)
            {
                goto Label_0014;
            }
            if (damage2 == 1)
            {
                goto Label_00E6;
            }
            goto Label_0161;
        Label_0014:
            this.MaximumStatus.param.hp = Math.Max(this.MaximumStatus.param.hp - damage, 0);
            flag = 0;
            if (this.MaximumStatus.param.hp > 0)
            {
                goto Label_007B;
            }
            flag = 1;
            this.MaximumStatus.param.hp = 1;
        Label_007B:
            this.CurrentStatus.param.hp = Math.Min(this.CurrentStatus.param.hp, this.MaximumStatus.param.hp);
            if (flag == null)
            {
                goto Label_0161;
            }
            if (is_allow_to_dead == null)
            {
                goto Label_0161;
            }
            this.CurrentStatus.param.hp = 0;
            goto Label_0161;
        Label_00E6:
            this.MaximumStatus.param.mp = Math.Max(this.MaximumStatus.param.mp - damage, 0);
            this.CurrentStatus.param.mp = Math.Min(this.CurrentStatus.param.mp, this.MaximumStatus.param.mp);
        Label_0161:
            return;
        }

        public void RefleshBattleAbilitysAndSkills()
        {
            bool flag;
            int num;
            AbilityChange change;
            AbilityParam param;
            AbilityParam param2;
            int num2;
            LearningSkill skill;
            LearningSkill[] skillArray;
            int num3;
            int num4;
            int num5;
            LearningSkill skill2;
            LearningSkill[] skillArray2;
            int num6;
            int num7;
            if (this.mBattleAbilitys != null)
            {
                goto Label_0016;
            }
            this.mBattleAbilitys = new List<AbilityData>();
        Label_0016:
            this.mBattleAbilitys.Clear();
            this.mBattleAbilitys.AddRange(this.mUnitData.BattleAbilitys);
            if (this.mBattleSkills != null)
            {
                goto Label_004D;
            }
            this.mBattleSkills = new List<SkillData>();
        Label_004D:
            this.mBattleSkills.Clear();
            this.mBattleSkills.AddRange(this.mUnitData.BattleSkills);
            flag = 0;
            num = 0;
            goto Label_0278;
        Label_0077:
            change = this.mAbilityChangeLists[num];
            if (change != null)
            {
                goto Label_008F;
            }
            goto Label_0274;
        Label_008F:
            param = change.GetFromAp();
            param2 = change.GetToAp();
            if (param == null)
            {
                goto Label_0274;
            }
            if (param2 != null)
            {
                goto Label_00B0;
            }
            goto Label_0274;
        Label_00B0:
            num2 = this.mBattleAbilitys.Count - 1;
            goto Label_00FE;
        Label_00C4:
            if ((this.mBattleAbilitys[num2].AbilityID == param.iname) == null)
            {
                goto Label_00F8;
            }
            this.mBattleAbilitys.RemoveAt(num2);
            goto Label_0106;
        Label_00F8:
            num2 -= 1;
        Label_00FE:
            if (num2 >= 0)
            {
                goto Label_00C4;
            }
        Label_0106:
            skillArray = param.skills;
            num3 = 0;
            goto Label_017A;
        Label_0116:
            skill = skillArray[num3];
            num4 = this.mBattleSkills.Count - 1;
            goto Label_016C;
        Label_0131:
            if ((this.mBattleSkills[num4].SkillID == skill.iname) == null)
            {
                goto Label_0166;
            }
            this.mBattleSkills.RemoveAt(num4);
            goto Label_0174;
        Label_0166:
            num4 -= 1;
        Label_016C:
            if (num4 >= 0)
            {
                goto Label_0131;
            }
        Label_0174:
            num3 += 1;
        Label_017A:
            if (num3 < ((int) skillArray.Length))
            {
                goto Label_0116;
            }
            num5 = this.mAddedAbilitys.Count - 1;
            goto Label_01DF;
        Label_0199:
            if ((this.mAddedAbilitys[num5].AbilityID == param2.iname) == null)
            {
                goto Label_01D9;
            }
            this.mBattleAbilitys.Add(this.mAddedAbilitys[num5]);
            goto Label_01E7;
        Label_01D9:
            num5 -= 1;
        Label_01DF:
            if (num5 >= 0)
            {
                goto Label_0199;
            }
        Label_01E7:
            skillArray2 = param2.skills;
            num6 = 0;
            goto Label_0267;
        Label_01F8:
            skill2 = skillArray2[num6];
            num7 = this.mAddedSkills.Count - 1;
            goto Label_0259;
        Label_0213:
            if ((this.mAddedSkills[num7].SkillID == skill2.iname) == null)
            {
                goto Label_0253;
            }
            this.mBattleSkills.Add(this.mAddedSkills[num7]);
            goto Label_0261;
        Label_0253:
            num7 -= 1;
        Label_0259:
            if (num7 >= 0)
            {
                goto Label_0213;
            }
        Label_0261:
            num6 += 1;
        Label_0267:
            if (num6 < ((int) skillArray2.Length))
            {
                goto Label_01F8;
            }
            flag = 1;
        Label_0274:
            num += 1;
        Label_0278:
            if (num < this.mAbilityChangeLists.Count)
            {
                goto Label_0077;
            }
            if (flag == null)
            {
                goto Label_02B7;
            }
            if (<>f__am$cache67 != null)
            {
                goto Label_02AD;
            }
            <>f__am$cache67 = new Comparison<AbilityData>(Unit.<RefleshBattleAbilitysAndSkills>m__4A9);
        Label_02AD:
            MySort<AbilityData>.Sort(this.mBattleAbilitys, <>f__am$cache67);
        Label_02B7:
            return;
        }

        public void RefleshMomentBuff(bool is_direct, int grid_x, int grid_y)
        {
            int num;
            int num2;
            num = this.x;
            num2 = this.y;
            if (is_direct == null)
            {
                goto Label_0026;
            }
            if (grid_x < 0)
            {
                goto Label_001D;
            }
            num = grid_x;
        Label_001D:
            if (grid_y < 0)
            {
                goto Label_0026;
            }
            num2 = grid_y;
        Label_0026:
            this.UpdateBuffEffectTurnCount(7, this);
            this.UpdateBuffEffects();
            this.setRelatedBuff(num, num2, is_direct);
            TrickData.MomentBuff(this, num, num2, 7);
            this.CalcCurrentStatus(0, 0);
            return;
        }

        public unsafe void RefleshMomentBuff(List<Unit> others, bool is_direct, int grid_x, int grid_y)
        {
            Unit unit;
            List<Unit>.Enumerator enumerator;
            int num;
            int num2;
            if (others == null)
            {
                goto Label_008E;
            }
            enumerator = others.GetEnumerator();
        Label_000D:
            try
            {
                goto Label_0071;
            Label_0012:
                unit = &enumerator.Current;
                num = unit.x;
                num2 = unit.y;
                if (is_direct == null)
                {
                    goto Label_0049;
                }
                if (unit != this)
                {
                    goto Label_0049;
                }
                if (grid_x < 0)
                {
                    goto Label_003E;
                }
                num = grid_x;
            Label_003E:
                if (grid_y < 0)
                {
                    goto Label_0049;
                }
                num2 = grid_y;
            Label_0049:
                unit.UpdateBuffEffectTurnCount(7, this);
                unit.UpdateBuffEffects();
                unit.setRelatedBuff(num, num2, is_direct);
                TrickData.MomentBuff(unit, num, num2, 7);
                unit.CalcCurrentStatus(0, 0);
            Label_0071:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0012;
                }
                goto Label_008E;
            }
            finally
            {
            Label_0082:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_008E:
            return;
        }

        public bool RemoveBuffPrevApply()
        {
            bool flag;
            int num;
            BuffAttachment attachment;
            flag = 0;
            num = 0;
            goto Label_003E;
        Label_0009:
            attachment = this.BuffAttachments[num];
            if (attachment.CheckTiming == 10)
            {
                goto Label_0028;
            }
            goto Label_003A;
        Label_0028:
            this.BuffAttachments.RemoveAt(num--);
            flag = 1;
        Label_003A:
            num += 1;
        Label_003E:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0009;
            }
            return flag;
        }

        public bool RemoveJudgeHpLists(SkillData skill)
        {
            if (skill != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if (this.mJudgeHpLists.Contains(skill) == null)
            {
                goto Label_0028;
            }
            this.mJudgeHpLists.Remove(skill);
            return 1;
        Label_0028:
            return 0;
        }

        public unsafe SkillData SearchArtifactSkill(string skill_id)
        {
            JobData data;
            List<ArtifactData> list;
            ArtifactData data2;
            ArtifactData data3;
            List<ArtifactData>.Enumerator enumerator;
            SkillData data4;
            if (string.IsNullOrEmpty(skill_id) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            data = this.Job;
            if (data != null)
            {
                goto Label_001C;
            }
            return null;
        Label_001C:
            list = new List<ArtifactData>();
            if (data.ArtifactDatas == null)
            {
                goto Label_0046;
            }
            if (((int) data.ArtifactDatas.Length) == null)
            {
                goto Label_0046;
            }
            list.AddRange(data.ArtifactDatas);
        Label_0046:
            if (string.IsNullOrEmpty(data.SelectedSkin) != null)
            {
                goto Label_006A;
            }
            data2 = data.GetSelectedSkinData();
            if (data2 == null)
            {
                goto Label_006A;
            }
            list.Add(data2);
        Label_006A:
            enumerator = list.GetEnumerator();
        Label_0072:
            try
            {
                goto Label_00CD;
            Label_0077:
                data3 = &enumerator.Current;
                if (data3 == null)
                {
                    goto Label_00CD;
                }
                if (data3.BattleEffectSkill == null)
                {
                    goto Label_00CD;
                }
                if (data3.BattleEffectSkill.SkillParam != null)
                {
                    goto Label_00A5;
                }
                goto Label_00CD;
            Label_00A5:
                if ((data3.BattleEffectSkill.SkillParam.iname == skill_id) == null)
                {
                    goto Label_00CD;
                }
                data4 = data3.BattleEffectSkill;
                goto Label_00ED;
            Label_00CD:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0077;
                }
                goto Label_00EB;
            }
            finally
            {
            Label_00DE:
                ((List<ArtifactData>.Enumerator) enumerator).Dispose();
            }
        Label_00EB:
            return null;
        Label_00ED:
            return data4;
        }

        public SkillData SearchItemSkill(BattleCore bc, string skill_id)
        {
            ItemData[] dataArray;
            ItemData data;
            ItemData[] dataArray2;
            int num;
            if (string.IsNullOrEmpty(skill_id) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            dataArray = bc.mInventory;
            if (dataArray != null)
            {
                goto Label_001C;
            }
            return null;
        Label_001C:
            dataArray2 = dataArray;
            num = 0;
            goto Label_0075;
        Label_0025:
            data = dataArray2[num];
            if (data == null)
            {
                goto Label_0071;
            }
            if (data.Skill == null)
            {
                goto Label_0071;
            }
            if (data.Skill.SkillParam != null)
            {
                goto Label_004F;
            }
            goto Label_0071;
        Label_004F:
            if ((data.Skill.SkillParam.iname == skill_id) == null)
            {
                goto Label_0071;
            }
            return data.Skill;
        Label_0071:
            num += 1;
        Label_0075:
            if (num < ((int) dataArray2.Length))
            {
                goto Label_0025;
            }
            return null;
        }

        public AIAction SetAIAction(int index)
        {
            if (this.IsAIActionTable() != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (index < 0)
            {
                goto Label_002A;
            }
            if (index < this.mAIActionTable.actions.Count)
            {
                goto Label_002C;
            }
        Label_002A:
            return null;
        Label_002C:
            if (this.mAIActionIndex == index)
            {
                goto Label_005D;
            }
            if (this.mAIActionTable.actions[index].turn == null)
            {
                goto Label_005F;
            }
        Label_005D:
            return null;
        Label_005F:
            this.mAIActionIndex = index;
            this.mAIActionTurnCount = 0;
            return this.mAIActionTable.actions[this.mAIActionIndex];
        }

        public bool SetBuffAttachment(BuffAttachment buff, bool is_duplicate)
        {
            SkillData data;
            int num;
            int num2;
            int num3;
            int num4;
            BuffAttachment attachment;
            int num5;
            bool flag;
            BattleCore core;
            if (buff != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if ((this.IsBreakObj == null) || ((buff.IsPassive != null) && (buff.user == this)))
            {
                goto Label_0031;
            }
            return 0;
        Label_0031:
            data = buff.skill;
            if (data == null)
            {
                goto Label_0109;
            }
            if (((this.mCastSkill == null) || (this.mCastSkill == data)) || ((this.mCastSkill.CastType != 2) || (data.SkillParam.TargetEx != null)))
            {
                goto Label_0078;
            }
            return 0;
        Label_0078:
            if (is_duplicate != null)
            {
                goto Label_0109;
            }
            num = 1;
            if (buff.DuplicateCount <= 0)
            {
                goto Label_00A5;
            }
            num2 = this.GetBuffAttachmentDuplicateCount(buff);
            num = Math.Max((1 + num2) - buff.DuplicateCount, 0);
        Label_00A5:
            if (num <= 0)
            {
                goto Label_0109;
            }
            num3 = 0;
            num4 = 0;
            goto Label_00F8;
        Label_00B6:
            attachment = this.BuffAttachments[num3];
            if (this.isSameBuffAttachment(attachment, buff) == null)
            {
                goto Label_00F4;
            }
            this.BuffAttachments.RemoveAt(num3--);
            if ((num4 += 1) < num)
            {
                goto Label_00F4;
            }
            goto Label_0109;
        Label_00F4:
            num3 += 1;
        Label_00F8:
            if (num3 < this.BuffAttachments.Count)
            {
                goto Label_00B6;
            }
        Label_0109:
            this.BuffAttachments.Add(buff);
            num5 = 0;
            if ((((buff.Param == null) || (buff.Param.IsUpReplenish == null)) || ((buff.CheckTiming == 7) || (buff.CheckTiming == 10))) || (buff.status[2] == null))
            {
                goto Label_0179;
            }
            num5 = this.MaximumStatus.param.hp;
        Label_0179:
            flag = 0;
            core = (SceneBattle.Instance == null) ? null : SceneBattle.Instance.Battle;
            if (core == null)
            {
                goto Label_01AE;
            }
            flag = core.IsBattleFlag(5);
        Label_01AE:
            this.CalcCurrentStatus(0, flag);
            if (num5 == null)
            {
                goto Label_01FD;
            }
            this.CurrentStatus.param.hp += Math.Max(this.MaximumStatus.param.hp - num5, 0);
        Label_01FD:
            this.UpdateBuffEffects();
            return 1;
        }

        public void SetCastSkill(SkillData skill, Unit unit, Grid grid)
        {
            this.mCastSkill = skill;
            this.mCastTime = 0;
            UNIT_CAST_INDEX = OInt.op_Increment(UNIT_CAST_INDEX);
            this.mCastIndex = UNIT_CAST_INDEX;
            this.mUnitTarget = unit;
            this.mGridTarget = grid;
            this.mCastSkillGridMap = null;
            return;
        }

        public void SetCastTime(int time)
        {
            this.mCastTime = time;
            return;
        }

        public void SetCommandFlag(EUnitCommandFlag tgt, bool sw)
        {
            if (sw == null)
            {
                goto Label_0023;
            }
            this.mCommandFlag |= tgt;
            goto Label_003C;
        Label_0023:
            this.mCommandFlag &= ~tgt;
        Label_003C:
            return;
        }

        public bool SetCondAttachment(CondAttachment cond)
        {
            FixParam param;
            SkillData data;
            EUnitCondition condition;
            bool flag;
            int num;
            CondEffect effect;
            CondEffect effect2;
            ConditionEffectTypes types;
            if (cond != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
            if (((cond.IsFailCondition() == null) || (cond.CheckTiming == 1)) || (cond.IsPassive != null))
            {
                goto Label_00E7;
            }
            if (this.IsClockCureCondition(cond.Condition) == null)
            {
                goto Label_0075;
            }
            cond.CheckTarget = this;
            cond.CheckTiming = 6;
            cond.turn = param.DefaultCondTurns[cond.Condition];
        Label_0075:
            if (cond.turn != null)
            {
                goto Label_009C;
            }
            cond.turn = param.DefaultCondTurns[cond.Condition];
        Label_009C:
            if ((cond.user == null) || (cond.Condition != 1L))
            {
                goto Label_0136;
            }
            cond.turn += cond.user.CurrentStatus[0x1a];
            goto Label_0136;
        Label_00E7:
            if (((cond.CondType != 5) || (cond.CheckTiming == 1)) || ((cond.IsPassive != null) || (cond.turn != null)))
            {
                goto Label_0136;
            }
            cond.turn = param.DefaultCondTurns[cond.Condition];
        Label_0136:
            if ((this.IsBreakObj == null) || ((cond.IsPassive != null) && (cond.user == this)))
            {
                goto Label_015F;
            }
            return 0;
        Label_015F:
            data = cond.skill;
            if ((((data == null) || (this.mCastSkill == null)) || ((this.mCastSkill == data) || (this.mCastSkill.CastType != 2))) || (data.SkillParam.TargetEx != null))
            {
                goto Label_01A6;
            }
            return 0;
        Label_01A6:
            condition = cond.Condition;
            switch ((cond.CondType - 1))
            {
                case 0:
                    goto Label_01D7;

                case 1:
                    goto Label_01E5;

                case 2:
                    goto Label_01E5;

                case 3:
                    goto Label_01E5;

                case 4:
                    goto Label_0420;
            }
            goto Label_0431;
        Label_01D7:
            this.CureCondEffects(condition, 1, 0);
            goto Label_0433;
        Label_01E5:
            if ((this.IsDisableUnitCondition(condition) != null) && (cond.CondType != 3))
            {
                goto Label_0433;
            }
            if (cond.IsPassive != null)
            {
                goto Label_03EA;
            }
            if (this.IsUnitCondition(0x20L) == null)
            {
                goto Label_021D;
            }
            return 0;
        Label_021D:
            if ((condition & 0x800L) == null)
            {
                goto Label_0382;
            }
            flag = 0;
            num = 0x3e7;
            if (cond.skill == null)
            {
                goto Label_02E5;
            }
            effect = cond.skill.GetCondEffect(0);
            if ((effect == null) || (effect.param.v_death_count <= 0))
            {
                goto Label_0286;
            }
            num = Math.Min(num, effect.param.v_death_count);
            flag = 1;
        Label_0286:
            effect2 = (cond.user != this) ? null : cond.skill.GetCondEffect(1);
            if (effect2 == null)
            {
                goto Label_0321;
            }
            if (effect2.param.v_death_count <= 0)
            {
                goto Label_0321;
            }
            num = Math.Min(num, effect2.param.v_death_count);
            flag = 1;
            goto Label_0321;
        Label_02E5:
            if (cond.Param == null)
            {
                goto Label_0321;
            }
            if (cond.Param.v_death_count <= 0)
            {
                goto Label_0321;
            }
            num = Math.Min(num, cond.Param.v_death_count);
            flag = 1;
        Label_0321:
            if (flag != null)
            {
                goto Label_0334;
            }
            num = param.DefaultDeathCount;
        Label_0334:
            if (this.mDeathCount <= 0)
            {
                goto Label_0362;
            }
            this.mDeathCount = Math.Min(this.mDeathCount, num);
        Label_0362:
            if (this.IsUnitCondition(0x800L) == null)
            {
                goto Label_0375;
            }
            return 0;
        Label_0375:
            this.mDeathCount = num;
        Label_0382:
            if ((condition & 0x20000L) == null)
            {
                goto Label_039D;
            }
            this.CureCondEffects(0x40000L, 1, 0);
        Label_039D:
            if ((condition & 0x40000L) == null)
            {
                goto Label_03B8;
            }
            this.CureCondEffects(0x20000L, 1, 0);
        Label_03B8:
            if ((condition & 0x200000L) == null)
            {
                goto Label_03EA;
            }
            if (cond.user == null)
            {
                goto Label_03DC;
            }
            if (cond.user != this)
            {
                goto Label_03DE;
            }
        Label_03DC:
            return 0;
        Label_03DE:
            this.mRageTarget = cond.user;
        Label_03EA:
            if (this.CheckCancelSkillFailCondition(condition) == null)
            {
                goto Label_03FC;
            }
            this.CancelCastSkill();
        Label_03FC:
            this.CondAttachments.Add(cond);
            this.CondLinkageBuffAttach(cond, cond.turn);
            goto Label_0433;
        Label_0420:
            this.CondAttachments.Add(cond);
            goto Label_0433;
        Label_0431:
            return 0;
        Label_0433:
            this.UpdateCondEffects();
            return 1;
        }

        public void SetCreateBreakObj(string break_obj_id, int create_clock)
        {
            this.mCreateBreakObjId = break_obj_id;
            this.mCreateBreakObjClock = create_clock;
            return;
        }

        public void SetDeathCount(int count)
        {
            this.mDeathCount = count;
            return;
        }

        public void SetGuardTarget(Unit target, int turn)
        {
            this.mGuardTarget = target;
            this.mGuardTurn = turn;
            return;
        }

        private unsafe void setRelatedBuff(int grid_x, int grid_y, bool is_direct)
        {
            SceneBattle battle;
            BattleCore core;
            BattleMap map;
            List<Unit> list;
            int num;
            Grid grid;
            Unit unit;
            BuffAttachment attachment;
            List<BuffAttachment>.Enumerator enumerator;
            Unit unit2;
            List<Unit>.Enumerator enumerator2;
            BuffAttachment attachment2;
            List<BuffAttachment>.Enumerator enumerator3;
            BuffAttachment attachment3;
            bool flag;
            BuffAttachment attachment4;
            List<BuffAttachment>.Enumerator enumerator4;
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            core = battle.Battle;
            if (core == null)
            {
                goto Label_002A;
            }
            if (core.CurrentMap != null)
            {
                goto Label_002B;
            }
        Label_002A:
            return;
        Label_002B:
            map = core.CurrentMap;
            if (map != null)
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            list = new List<Unit>(core.Units.Count);
            num = 0;
            goto Label_0153;
        Label_0052:
            grid = map[grid_x + DIRECTION_OFFSETS[num, 0], grid_y + DIRECTION_OFFSETS[num, 1]];
            if (grid != null)
            {
                goto Label_0084;
            }
            goto Label_014D;
        Label_0084:
            unit = null;
            if (is_direct == null)
            {
                goto Label_009C;
            }
            unit = core.DirectFindUnitAtGrid(grid);
            goto Label_00A6;
        Label_009C:
            unit = core.FindUnitAtGrid(grid);
        Label_00A6:
            if (unit != null)
            {
                goto Label_00B2;
            }
            goto Label_014D;
        Label_00B2:
            if (unit != this)
            {
                goto Label_00BF;
            }
            goto Label_014D;
        Label_00BF:
            enumerator = unit.BuffAttachments.GetEnumerator();
        Label_00CD:
            try
            {
                goto Label_012F;
            Label_00D2:
                attachment = &enumerator.Current;
                if (attachment.CheckTiming != 7)
                {
                    goto Label_00ED;
                }
                goto Label_012F;
            Label_00ED:
                if (attachment.Param != null)
                {
                    goto Label_00FE;
                }
                goto Label_012F;
            Label_00FE:
                if (attachment.Param.mEffRange != 1)
                {
                    goto Label_012F;
                }
                if (unit.mSide != this.mSide)
                {
                    goto Label_012F;
                }
                list.Add(unit);
                goto Label_013B;
            Label_012F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00D2;
                }
            Label_013B:
                goto Label_014D;
            }
            finally
            {
            Label_0140:
                ((List<BuffAttachment>.Enumerator) enumerator).Dispose();
            }
        Label_014D:
            num += 1;
        Label_0153:
            if (num < 4)
            {
                goto Label_0052;
            }
            if (list.Count != null)
            {
                goto Label_0167;
            }
            return;
        Label_0167:
            enumerator2 = list.GetEnumerator();
        Label_016F:
            try
            {
                goto Label_02A7;
            Label_0174:
                unit2 = &enumerator2.Current;
                enumerator3 = unit2.BuffAttachments.GetEnumerator();
            Label_018B:
                try
                {
                    goto Label_0289;
                Label_0190:
                    attachment2 = &enumerator3.Current;
                    if (attachment2.CheckTiming != 7)
                    {
                        goto Label_01AB;
                    }
                    goto Label_0289;
                Label_01AB:
                    if (attachment2.Param != null)
                    {
                        goto Label_01BC;
                    }
                    goto Label_0289;
                Label_01BC:
                    if (attachment2.Param.mEffRange != 1)
                    {
                        goto Label_0289;
                    }
                    if (unit2.mSide != this.mSide)
                    {
                        goto Label_0289;
                    }
                    attachment3 = new BuffAttachment();
                    attachment2.CopyTo(attachment3);
                    attachment3.CheckTiming = 7;
                    attachment3.IsPassive = 0;
                    attachment3.turn = 1;
                    flag = 0;
                    enumerator4 = this.BuffAttachments.GetEnumerator();
                Label_0222:
                    try
                    {
                        goto Label_025A;
                    Label_0227:
                        attachment4 = &enumerator4.Current;
                        if (this.isSameBuffAttachment(attachment4, attachment3) == null)
                        {
                            goto Label_025A;
                        }
                        if (attachment4.user != attachment3.user)
                        {
                            goto Label_025A;
                        }
                        flag = 1;
                        goto Label_0266;
                    Label_025A:
                        if (&enumerator4.MoveNext() != null)
                        {
                            goto Label_0227;
                        }
                    Label_0266:
                        goto Label_0278;
                    }
                    finally
                    {
                    Label_026B:
                        ((List<BuffAttachment>.Enumerator) enumerator4).Dispose();
                    }
                Label_0278:
                    if (flag != null)
                    {
                        goto Label_0289;
                    }
                    this.SetBuffAttachment(attachment3, 0);
                Label_0289:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_0190;
                    }
                    goto Label_02A7;
                }
                finally
                {
                Label_029A:
                    ((List<BuffAttachment>.Enumerator) enumerator3).Dispose();
                }
            Label_02A7:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0174;
                }
                goto Label_02C5;
            }
            finally
            {
            Label_02B8:
                ((List<Unit>.Enumerator) enumerator2).Dispose();
            }
        Label_02C5:
            return;
        }

        public void SetSearchRange(int value)
        {
            this.mSearched = value;
            return;
        }

        public void SetUnitFlag(EUnitFlag tgt, bool sw)
        {
            if (sw == null)
            {
                goto Label_0023;
            }
            this.mUnitFlag |= tgt;
            goto Label_003C;
        Label_0023:
            this.mUnitFlag &= ~tgt;
        Label_003C:
            return;
        }

        public unsafe bool Setup(SRPG.UnitData unitdata, UnitSetting setting, DropItem dropitem, DropItem stealitem)
        {
            string str;
            int num;
            int num2;
            int num3;
            EElement element;
            string str2;
            int num4;
            int num5;
            AbilityData data;
            SkillData data2;
            AbilityData data3;
            int num6;
            SkillData data4;
            SkillData data5;
            string str3;
            JobData data6;
            AIParam param;
            <Setup>c__AnonStorey3ED storeyed;
            <Setup>c__AnonStorey3EE storeyee;
            <Setup>c__AnonStorey3EB storeyeb;
            <Setup>c__AnonStorey3EC storeyec;
            <Setup>c__AnonStorey3EF storeyef;
            eMapUnitCtCalcType type;
            if (setting != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            if ((setting as NPCSetting) == null)
            {
                goto Label_0802;
            }
            storeyed = new <Setup>c__AnonStorey3ED();
            storeyed.npc = setting as NPCSetting;
            str = storeyed.npc.iname;
            num = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(storeyed.npc.lv);
            num2 = storeyed.npc.rare;
            num3 = storeyed.npc.awake;
            element = storeyed.npc.elem;
            this.mUnitData = new SRPG.UnitData();
            if (this.mUnitData.Setup(str, num, num2, num3, null, 1, element, 0) != null)
            {
                goto Label_00B7;
            }
            return 0;
        Label_00B7:
            if (storeyed.npc.abilities == null)
            {
                goto Label_0656;
            }
            storeyee = new <Setup>c__AnonStorey3EE();
            storeyee.i = 0;
            goto Label_0548;
        Label_00DC:
            storeyeb = new <Setup>c__AnonStorey3EB();
            storeyeb.<>f__ref$1005 = storeyed;
            storeyeb.<>f__ref$1006 = storeyee;
            str2 = storeyed.npc.abilities[storeyee.i].iname;
            num4 = storeyed.npc.abilities[storeyee.i].rank;
            num5 = num4 - 1;
            if (string.IsNullOrEmpty(str2) == null)
            {
                goto Label_014C;
            }
            goto Label_0538;
        Label_014C:
            storeyeb.ab_param = MonoSingleton<GameManager>.Instance.GetAbilityParam(str2);
            if (storeyeb.ab_param != null)
            {
                goto Label_0170;
            }
            goto Label_0538;
        Label_0170:
            if (storeyeb.ab_param.skills == null)
            {
                goto Label_02ED;
            }
            data = this.mUnitData.BattleAbilitys.Find(new Predicate<AbilityData>(storeyeb.<>m__4A1));
            if (data == null)
            {
                goto Label_01E8;
            }
            if (data.Rank < num4)
            {
                goto Label_01BA;
            }
            goto Label_0538;
        Label_01BA:
            data.Setup(this.mUnitData, data.UniqueID, storeyeb.ab_param.iname, Math.Max(num5, 0), 0);
            goto Label_0234;
        Label_01E8:
            data = new AbilityData();
            data.Setup(this.mUnitData, (long) this.mUnitData.BattleAbilitys.Count, storeyeb.ab_param.iname, Math.Max(num5, 0), 0);
            this.mUnitData.BattleAbilitys.Add(data);
        Label_0234:
            data.UpdateLearningsSkill(0, null);
            storeyec = new <Setup>c__AnonStorey3EC();
            storeyec.<>f__ref$1003 = storeyeb;
            storeyec.j = 0;
            goto Label_02D3;
        Label_025A:
            data2 = this.mUnitData.BattleSkills.Find(new Predicate<SkillData>(storeyec.<>m__4A2));
            if (data2 != null)
            {
                goto Label_0299;
            }
            data2 = new SkillData();
            this.mUnitData.BattleSkills.Add(data2);
        Label_0299:
            data2.Setup(storeyeb.ab_param.skills[storeyec.j].iname, num4, data.GetRankMaxCap(), null);
            storeyec.j += 1;
        Label_02D3:
            if (storeyec.j < ((int) storeyeb.ab_param.skills.Length))
            {
                goto Label_025A;
            }
        Label_02ED:
            if (storeyed.npc.abilities[storeyee.i].skills == null)
            {
                goto Label_0538;
            }
            data3 = this.mUnitData.BattleAbilitys.Find(new Predicate<AbilityData>(storeyeb.<>m__4A3));
            num6 = 0;
            goto Label_0516;
        Label_0332:
            storeyef = new <Setup>c__AnonStorey3EF();
            if (storeyed.npc.abilities[storeyee.i].skills[num6] != null)
            {
                goto Label_035F;
            }
            goto Label_0510;
        Label_035F:
            storeyef.sk_iname = storeyed.npc.abilities[storeyee.i].skills[num6].iname;
            data4 = this.mUnitData.BattleSkills.Find(new Predicate<SkillData>(storeyef.<>m__4A4));
            if (data4 != null)
            {
                goto Label_03B7;
            }
            goto Label_0510;
        Label_03B7:
            data4.UseRate = storeyed.npc.abilities[storeyee.i].skills[num6].rate;
            data4.UseCondition = storeyed.npc.abilities[storeyee.i].skills[num6].cond;
            data4.CheckCount = (storeyed.npc.abilities[storeyee.i].skills[num6].check_cnt > 0) | storeyed.npc.control;
            if ((data3 == null) || (data3.Skills == null))
            {
                goto Label_0510;
            }
            data5 = data3.Skills.Find(new Predicate<SkillData>(storeyef.<>m__4A5));
            if (data5 == null)
            {
                goto Label_0510;
            }
            data5.UseRate = storeyed.npc.abilities[storeyee.i].skills[num6].rate;
            data5.UseCondition = storeyed.npc.abilities[storeyee.i].skills[num6].cond;
            data5.CheckCount = (storeyed.npc.abilities[storeyee.i].skills[num6].check_cnt > 0) | storeyed.npc.control;
        Label_0510:
            num6 += 1;
        Label_0516:
            if (num6 < ((int) storeyed.npc.abilities[storeyee.i].skills.Length))
            {
                goto Label_0332;
            }
        Label_0538:
            storeyee.i += 1;
        Label_0548:
            if (storeyee.i < ((int) storeyed.npc.abilities.Length))
            {
                goto Label_00DC;
            }
            this.mUnitData.CalcStatus();
            this.mAIActionTable.Clear();
            if ((storeyed.npc.acttbl == null) || (storeyed.npc.acttbl.actions == null))
            {
                goto Label_05B6;
            }
            storeyed.npc.acttbl.CopyTo(this.mAIActionTable);
        Label_05B6:
            this.mAIPatrolTable.Clear();
            if (((storeyed.npc.patrol == null) || (storeyed.npc.patrol.routes == null)) || (((int) storeyed.npc.patrol.routes.Length) <= 0))
            {
                goto Label_0618;
            }
            storeyed.npc.patrol.CopyTo(this.mAIPatrolTable);
        Label_0618:
            if (string.IsNullOrEmpty(storeyed.npc.fskl) != null)
            {
                goto Label_0656;
            }
            this.mAIForceSkill = this.mUnitData.BattleSkills.Find(new Predicate<SkillData>(storeyed.<>m__4A6));
        Label_0656:
            if (dropitem == null)
            {
                goto Label_067D;
            }
            this.mDrop.items.Clear();
            this.mDrop.items.Add(dropitem);
        Label_067D:
            this.mDrop.exp = storeyed.npc.exp;
            this.mDrop.gems = storeyed.npc.gems;
            this.mDrop.gold = storeyed.npc.gold;
            this.mDrop.gained = 0;
            if (this.UnitType != 2)
            {
                goto Label_06F9;
            }
            this.mDrop.gems = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.GemsGainValue;
        Label_06F9:
            if (stealitem == null)
            {
                goto Label_0722;
            }
            this.mSteal.items.Clear();
            this.mSteal.items.Add(stealitem);
        Label_0722:
            this.mSteal.is_item_steeled = 0;
            this.mSteal.is_gold_steeled = 0;
            this.mSearched = storeyed.npc.search;
            this.SetUnitFlag(0x10000, (storeyed.npc.notice_damage == 0) == 0);
            if (storeyed.npc.notice_members == null)
            {
                goto Label_0796;
            }
            this.mNotifyUniqueNames = new List<OString>(storeyed.npc.notice_members);
        Label_0796:
            if (setting.side != null)
            {
                goto Label_07BD;
            }
            this.IsPartyMember = storeyed.npc.control;
        Label_07BD:
            this.mBreakObj = new MapBreakObj();
            if (storeyed.npc.break_obj == null)
            {
                goto Label_07F0;
            }
            storeyed.npc.break_obj.CopyTo(this.mBreakObj);
        Label_07F0:
            this.mSettingNPC = storeyed.npc;
            goto Label_0827;
        Label_0802:
            if (unitdata != null)
            {
                goto Label_080A;
            }
            return 0;
        Label_080A:
            this.mUnitData = unitdata;
            this.mSearched = this.UnitParam.search;
        Label_0827:
            this.mUnitName = this.UnitParam.name;
            this.mUniqueName = setting.uniqname;
            this.mParentUniqueName = setting.parent;
            this.SetupStatus();
            this.mSide = (this.IsGimmick == null) ? setting.side : 2;
            str3 = null;
            if (string.IsNullOrEmpty(setting.ai) != null)
            {
                goto Label_08AC;
            }
            str3 = setting.ai;
            goto Label_08FC;
        Label_08AC:
            str3 = (this.IsGimmick == null) ? SRPG.UnitParam.AI_TYPE_DEFAULT : null;
            data6 = this.mUnitData.CurrentJob;
            if (data6 == null)
            {
                goto Label_08FC;
            }
            if (string.IsNullOrEmpty(data6.Param.ai) != null)
            {
                goto Label_08FC;
            }
            str3 = data6.Param.ai;
        Label_08FC:
            if (string.IsNullOrEmpty(str3) != null)
            {
                goto Label_0946;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetAIParam(str3);
            DebugUtility.Assert((param == null) == 0, "ai " + str3 + " not found");
            this.mAI.Add(param);
        Label_0946:
            if (setting.trigger == null)
            {
                goto Label_096D;
            }
            this.mEventTrigger = new SRPG.EventTrigger();
            this.mEventTrigger.Setup(setting.trigger);
        Label_096D:
            if (setting.entries == null)
            {
                goto Label_09B6;
            }
            if (setting.entries.Count <= 0)
            {
                goto Label_09B6;
            }
            this.mEntryTriggers = new List<UnitEntryTrigger>(setting.entries);
            this.mEntryTriggerAndCheck = (setting.entries_and == 0) == 0;
        Label_09B6:
            this.x = &setting.pos.x;
            this.y = &setting.pos.y;
            this.Direction = setting.dir;
            this.mWaitEntryClock = setting.waitEntryClock;
            this.mMoveWaitTurn = setting.waitMoveTurn;
            if (setting.startCtVal == null)
            {
                goto Label_0A8E;
            }
            type = setting.startCtCalc;
            if (type == null)
            {
                goto Label_0A37;
            }
            if (type == 1)
            {
                goto Label_0A48;
            }
            goto Label_0A72;
        Label_0A37:
            this.mChargeTime = setting.startCtVal;
            goto Label_0A72;
        Label_0A48:
            this.mChargeTime = (this.ChargeTimeMax * setting.startCtVal) / 100;
        Label_0A72:
            this.mChargeTime = Math.Max(this.mChargeTime, 0);
        Label_0A8E:
            if (setting.DisableFirceVoice == null)
            {
                goto Label_0AA5;
            }
            this.SetUnitFlag(0x8000, 1);
        Label_0AA5:
            UNIT_INDEX = OInt.op_Increment(UNIT_INDEX);
            this.mUnitIndex = UNIT_INDEX;
            this.SetUnitFlag(1, this.CheckEnableEntry());
            base.IsInitialized = 1;
            return 1;
        }

        public bool SetupDummy(EUnitSide side, string iname, int lv, int rare, int awake, string job_iname, int job_rank)
        {
            int num;
            num = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitLevelExp(lv);
            this.mUnitData = new SRPG.UnitData();
            if (this.mUnitData.Setup(iname, num, rare, awake, job_iname, job_rank, 0, 0) != null)
            {
                goto Label_003A;
            }
            return 0;
        Label_003A:
            this.mSide = side;
            this.mUnitName = this.UnitParam.name;
            this.mUnitData.Status.CopyTo(this.mMaximumStatus);
            this.mUnitData.Status.CopyTo(this.mCurrentStatus);
            this.mMaximumStatusHp = this.mMaximumStatus.param.hp;
            this.SetUnitFlag(1, 0);
            this.SetUnitFlag(1, this.CheckEnableEntry());
            this.SetupStatus();
            UNIT_INDEX = OInt.op_Increment(UNIT_INDEX);
            this.mUnitIndex = UNIT_INDEX;
            base.IsInitialized = 1;
            return 1;
        }

        public unsafe bool SetupResume(MultiPlayResumeUnitData data, Unit target, Unit rage, Unit casttarget, List<MultiPlayResumeSkillData> buffskill, List<MultiPlayResumeSkillData> condskill)
        {
            SceneBattle battle;
            BattleCore core;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            BaseStatus status6;
            BattleMap map;
            GridMap<bool> map2;
            int num;
            int num2;
            int num3;
            GameManager manager;
            int num4;
            AbilityChange change;
            int num5;
            MultiPlayResumeAbilChg.Data data2;
            AbilityParam param;
            AbilityParam param2;
            int num6;
            AbilityParam param3;
            int num7;
            int num8;
            int num9;
            SkillData data3;
            int num10;
            BuffAttachment attachment;
            SkillData data4;
            BuffEffect effect;
            SkillEffectTargets targets;
            int num11;
            ESkillCondition condition;
            EffectCheckTargets targets2;
            EffectCheckTimings timings;
            int num12;
            ConceptCardData data5;
            List<BuffEffect> list;
            BaseStatus status7;
            BaseStatus status8;
            BaseStatus status9;
            BaseStatus status10;
            BaseStatus status11;
            BaseStatus status12;
            int num13;
            List<Unit> list2;
            int num14;
            Unit unit;
            int num15;
            int num16;
            int num17;
            BuffAttachment attachment2;
            int num18;
            int num19;
            BuffAttachment attachment3;
            SRPG.EventTrigger trigger;
            BuffAttachment attachment4;
            int num20;
            CondEffectParam param4;
            SkillEffectTargets targets3;
            CondEffect effect2;
            CondAttachment attachment5;
            int num21;
            CondAttachment attachment6;
            bool flag;
            MultiPlayResumeBuff buff;
            MultiPlayResumeBuff[] buffArray;
            int num22;
            MultiPlayResumeShield shield;
            MultiPlayResumeShield[] shieldArray;
            int num23;
            SkillParam param5;
            string str;
            string[] strArray;
            int num24;
            SkillData data6;
            MultiPlayResumeMhmDmg dmg;
            MultiPlayResumeMhmDmg[] dmgArray;
            int num25;
            int num26;
            EUnitDirection direction;
            SkillParamCalcTypes types;
            if ((this.UnitName != data.name) == null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            battle = SceneBattle.Instance;
            core = null;
            if ((battle != null) == null)
            {
                goto Label_0033;
            }
            core = battle.Battle;
        Label_0033:
            status = new BaseStatus();
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            status5 = new BaseStatus();
            status6 = new BaseStatus();
            this.CurrentStatus.param.hp = data.hp;
            this.mUnitChangedHp = data.chp;
            this.Gems = data.gem;
            num26 = data.x;
            this.x = num26;
            this.startX = num26;
            num26 = data.y;
            this.y = num26;
            this.startY = num26;
            this.startDir = this.Direction = data.dir;
            this.Target = target;
            this.mRageTarget = rage;
            if (string.IsNullOrEmpty(data.castskill) != null)
            {
                goto Label_01FA;
            }
            this.mCastSkill = this.GetSkillData(data.castskill);
            this.mCastTime = data.casttime;
            this.mCastIndex = data.castindex;
            this.mUnitTarget = casttarget;
            if ((data.ctx == -1) || (data.cty == -1))
            {
                goto Label_0174;
            }
            map = battle.Battle.CurrentMap;
            if (map == null)
            {
                goto Label_0174;
            }
            this.mGridTarget = map[data.ctx, data.cty];
        Label_0174:
            if (data.castgrid == null)
            {
                goto Label_01FA;
            }
            map2 = new GridMap<bool>(data.grid_w, data.grid_h);
            num = 0;
            goto Label_01E5;
        Label_019A:
            num2 = 0;
            goto Label_01D2;
        Label_01A2:
            map2.set(num, num2, (data.castgrid[num + (num2 * data.grid_w)] == null) ? 0 : 1);
            num2 += 1;
        Label_01D2:
            if (num2 < data.grid_h)
            {
                goto Label_01A2;
            }
            num += 1;
        Label_01E5:
            if (num < data.grid_w)
            {
                goto Label_019A;
            }
            this.CastSkillGridMap = map2;
        Label_01FA:
            this.mChargeTime = data.chargetime;
            this.mDeathCount = data.deathcnt;
            this.mAutoJewel = data.autojewel;
            this.mWaitEntryClock = data.waitturn;
            this.mMoveWaitTurn = data.moveturn;
            this.mActionCount = data.actcnt;
            this.mAIActionIndex = data.aiindex;
            this.mAIActionTurnCount = data.aiturn;
            this.mAIPatrolIndex = data.aipatrol;
            this.mTurnCount = data.turncnt;
            if (this.mEventTrigger == null)
            {
                goto Label_02BB;
            }
            this.mEventTrigger.Count = data.trgcnt;
        Label_02BB:
            this.mKillCount = data.killcnt;
            this.mCreateBreakObjId = data.boi;
            this.mCreateBreakObjClock = data.boc;
            this.OwnerPlayerIndex = data.own;
            if ((this.mEntryTriggers == null) || (data.etr == null))
            {
                goto Label_0356;
            }
            num3 = 0;
            goto Label_0347;
        Label_0309:
            if (num3 < this.mEntryTriggers.Count)
            {
                goto Label_0320;
            }
            goto Label_0356;
        Label_0320:
            this.mEntryTriggers[num3].on = (data.etr[num3] == 0) == 0;
            num3 += 1;
        Label_0347:
            if (num3 < ((int) data.etr.Length))
            {
                goto Label_0309;
            }
        Label_0356:
            manager = MonoSingleton<GameManager>.Instance;
            this.mAbilityChangeLists.Clear();
            if (data.abilchgs == null)
            {
                goto Label_048D;
            }
            num4 = 0;
            goto Label_047E;
        Label_037B:
            if (data.abilchgs[num4] == null)
            {
                goto Label_0478;
            }
            if (data.abilchgs[num4].acd != null)
            {
                goto Label_03A1;
            }
            goto Label_0478;
        Label_03A1:
            change = new AbilityChange();
            num5 = 0;
            goto Label_043C;
        Label_03B0:
            data2 = data.abilchgs[num4].acd[num5];
            param = manager.MasterParam.GetAbilityParam(data2.fid);
            param2 = manager.MasterParam.GetAbilityParam(data2.tid);
            if ((param != null) && (param2 != null))
            {
                goto Label_0403;
            }
            change = null;
            goto Label_0453;
        Label_0403:
            change.Add(param, param2, data2.tur, (data2.irs == 0) == 0, data2.exp, (data2.iif == 0) == 0);
            num5 += 1;
        Label_043C:
            if (num5 < ((int) data.abilchgs[num4].acd.Length))
            {
                goto Label_03B0;
            }
        Label_0453:
            if ((change == null) || (change.mDataLists.Count == null))
            {
                goto Label_0478;
            }
            this.mAbilityChangeLists.Add(change);
        Label_0478:
            num4 += 1;
        Label_047E:
            if (num4 < ((int) data.abilchgs.Length))
            {
                goto Label_037B;
            }
        Label_048D:
            this.mAddedAbilitys.Clear();
            this.mAddedSkills.Clear();
            if (data.addedabils == null)
            {
                goto Label_0523;
            }
            num6 = 0;
            goto Label_050E;
        Label_04B6:
            if (data.addedabils[num6] != null)
            {
                goto Label_04C9;
            }
            goto Label_0508;
        Label_04C9:
            param3 = manager.MasterParam.GetAbilityParam(data.addedabils[num6].aid);
            if (param3 != null)
            {
                goto Label_04F1;
            }
            goto Label_0508;
        Label_04F1:
            this.CreateAddedAbilityAndSkills(param3, data.addedabils[num6].exp);
        Label_0508:
            num6 += 1;
        Label_050E:
            if (num6 < ((int) data.addedabils.Length))
            {
                goto Label_04B6;
            }
            this.RefleshBattleAbilitysAndSkills();
        Label_0523:
            if (data.skillname == null)
            {
                goto Label_05BC;
            }
            num7 = 0;
            goto Label_05AD;
        Label_0536:
            num8 = 0;
            num9 = 0;
            goto Label_0569;
        Label_0541:
            if ((data.skillname[num7] == data.skillname[num9]) == null)
            {
                goto Label_0563;
            }
            num8 += 1;
        Label_0563:
            num9 += 1;
        Label_0569:
            if (num9 < num7)
            {
                goto Label_0541;
            }
            data3 = this.GetSkillForUseCount(data.skillname[num7], num8);
            if (data3 == null)
            {
                goto Label_05A7;
            }
            this.mSkillUseCount[data3] = data.skillcnt[num7];
        Label_05A7:
            num7 += 1;
        Label_05AD:
            if (num7 < ((int) data.skillname.Length))
            {
                goto Label_0536;
            }
        Label_05BC:
            this.SuspendClearBuffCondEffects(1);
            if ((data.buff == null) || (core == null))
            {
                goto Label_0D71;
            }
            num10 = 0;
            goto Label_0D62;
        Label_05DC:
            attachment = null;
            if (buffskill[num10].skill == null)
            {
                goto Label_0CEE;
            }
            data4 = buffskill[num10].skill;
            effect = null;
            targets = data.buff[num10].skilltarget;
            effect = data4.GetBuffEffect(targets);
            if (effect != null)
            {
                goto Label_062C;
            }
            goto Label_0D5C;
        Label_062C:
            num11 = data.buff[num10].turn;
            condition = effect.param.cond;
            targets2 = effect.param.chk_target;
            timings = effect.param.chk_timing;
            num12 = data4.DuplicateCount;
            status.Clear();
            status5.Clear();
            status3.Clear();
            status2.Clear();
            status4.Clear();
            status6.Clear();
            data4.BuffSkill(data4.Timing, this.UnitData.Element, status, status3, status5, status2, status4, status6, null, targets, 1, null);
            if ((buffskill[num10].user == null) || (buffskill[num10].user.UnitData.ConceptCard == null))
            {
                goto Label_0873;
            }
            list = buffskill[num10].user.UnitData.ConceptCard.GetEnableCardSkillAddBuffs(buffskill[num10].user.UnitData, data4.SkillParam);
            status7 = new BaseStatus();
            status8 = new BaseStatus();
            status9 = new BaseStatus();
            status10 = new BaseStatus();
            status11 = new BaseStatus();
            status12 = new BaseStatus();
            num13 = 0;
            goto Label_0865;
        Label_075F:
            status7.Clear();
            status8.Clear();
            status9.Clear();
            status10.Clear();
            status11.Clear();
            status12.Clear();
            list[num13].CalcBuffStatus(&status7, this.Element, 0, 1, 0, 0, 0);
            list[num13].CalcBuffStatus(&status8, this.Element, 0, 1, 1, 0, 0);
            list[num13].CalcBuffStatus(&status9, this.Element, 0, 0, 0, 1, 0);
            list[num13].CalcBuffStatus(&status10, this.Element, 1, 1, 0, 0, 0);
            list[num13].CalcBuffStatus(&status11, this.Element, 1, 1, 1, 0, 0);
            list[num13].CalcBuffStatus(&status12, this.Element, 1, 0, 0, 1, 0);
            status.Add(status7);
            status5.Add(status9);
            status3.Add(status8);
            status2.Add(status10);
            status4.Add(status11);
            status6.Add(status12);
            num13 += 1;
        Label_0865:
            if (num13 < list.Count)
            {
                goto Label_075F;
            }
        Label_0873:
            list2 = new List<Unit>();
            if (data4.SkillParam.AbsorbAndGive == null)
            {
                goto Label_0919;
            }
            if (data.buff[num10].atl.Count == null)
            {
                goto Label_08F8;
            }
            num14 = 0;
            goto Label_08DE;
        Label_08AB:
            unit = BattleSuspend.GetUnitFromAllUnits(null, data.buff[num10].atl[num14]);
            if (unit == null)
            {
                goto Label_08D8;
            }
            list2.Add(unit);
        Label_08D8:
            num14 += 1;
        Label_08DE:
            if (num14 < data.buff[num10].atl.Count)
            {
                goto Label_08AB;
            }
        Label_08F8:
            this.AbsorbAndGiveExchangeBuff(buffskill[num10].user, this, data4, effect, list2, status, status5, status2, status6);
        Label_0919:
            if (data.buff[num10].type != null)
            {
                goto Label_0A46;
            }
            types = data.buff[num10].calc;
            if (types == null)
            {
                goto Label_0950;
            }
            if (types == 1)
            {
                goto Label_09EA;
            }
            goto Label_0A41;
        Label_0950:
            if (data.buff[num10].vtp != null)
            {
                goto Label_09A6;
            }
            attachment = core.CreateBuffAttachment(buffskill[num10].user, this, data4, targets, effect.param, 0, 0, 0, status, condition, num11, targets2, timings, data.buff[num10].passive, num12);
            goto Label_09E5;
        Label_09A6:
            attachment = core.CreateBuffAttachment(buffskill[num10].user, this, data4, targets, effect.param, 0, 1, 0, status3, condition, num11, targets2, timings, data.buff[num10].passive, num12);
        Label_09E5:
            goto Label_0A41;
        Label_09EA:
            attachment = core.CreateBuffAttachment(buffskill[num10].user, this, data4, targets, effect.param, 0, (data.buff[num10].vtp == 0) == 0, 1, status5, condition, num11, targets2, timings, data.buff[num10].passive, num12);
        Label_0A41:
            goto Label_0B5B;
        Label_0A46:
            types = data.buff[num10].calc;
            if (types == null)
            {
                goto Label_0A6A;
            }
            if (types == 1)
            {
                goto Label_0B04;
            }
            goto Label_0B5B;
        Label_0A6A:
            if (data.buff[num10].vtp != null)
            {
                goto Label_0AC0;
            }
            attachment = core.CreateBuffAttachment(buffskill[num10].user, this, data4, targets, effect.param, 1, 0, 0, status2, condition, num11, targets2, timings, data.buff[num10].passive, num12);
            goto Label_0AFF;
        Label_0AC0:
            attachment = core.CreateBuffAttachment(buffskill[num10].user, this, data4, targets, effect.param, 1, 1, 0, status4, condition, num11, targets2, timings, data.buff[num10].passive, num12);
        Label_0AFF:
            goto Label_0B5B;
        Label_0B04:
            attachment = core.CreateBuffAttachment(buffskill[num10].user, this, data4, targets, effect.param, 1, (data.buff[num10].vtp == 0) == 0, 1, status6, condition, num11, targets2, timings, data.buff[num10].passive, num12);
        Label_0B5B:
            if (attachment == null)
            {
                goto Label_0D5C;
            }
            attachment.turn = num11;
            attachment.LinkageID = data.buff[num10].lid;
            attachment.UpBuffCount = data.buff[num10].ubc;
            if ((attachment.Param == null) || (attachment.Param.mIsUpBuff == null))
            {
                goto Label_0BCB;
            }
            this.UpdateUpBuffEffect(attachment, 0, 0);
        Label_0BCB:
            attachment.AagTargetLists = list2;
            if ((attachment.skill == null) || ((attachment.skill.Condition == 4) && (attachment.skill.IsPassiveSkill() != null)))
            {
                goto Label_0CDC;
            }
            num15 = 1;
            if (attachment.DuplicateCount <= 0)
            {
                goto Label_0C6E;
            }
            num16 = 0;
            num17 = 0;
            goto Label_0C48;
        Label_0C1E:
            attachment2 = this.BuffAttachments[num17];
            if (this.isSameBuffAttachment(attachment2, attachment) == null)
            {
                goto Label_0C42;
            }
            num16 += 1;
        Label_0C42:
            num17 += 1;
        Label_0C48:
            if (num17 < this.BuffAttachments.Count)
            {
                goto Label_0C1E;
            }
            num15 = Math.Max((1 + num16) - attachment.DuplicateCount, 0);
        Label_0C6E:
            if (num15 <= 0)
            {
                goto Label_0CDC;
            }
            num18 = 0;
            num19 = 0;
            goto Label_0CCA;
        Label_0C81:
            attachment3 = this.BuffAttachments[num19];
            if (this.isSameBuffAttachment(attachment3, attachment) == null)
            {
                goto Label_0CC4;
            }
            this.BuffAttachments.RemoveAt(num19--);
            if ((num18 += 1) < num15)
            {
                goto Label_0CC4;
            }
            goto Label_0CDC;
        Label_0CC4:
            num19 += 1;
        Label_0CCA:
            if (num19 < this.BuffAttachments.Count)
            {
                goto Label_0C81;
            }
        Label_0CDC:
            this.BuffAttachments.Add(attachment);
            goto Label_0D5C;
        Label_0CEE:
            if (buffskill[num10].user == null)
            {
                goto Label_0D5C;
            }
            trigger = buffskill[num10].user.EventTrigger;
            if (trigger == null)
            {
                goto Label_0D5C;
            }
            attachment4 = trigger.MakeBuff(buffskill[num10].user, this);
            attachment4.turn = data.buff[num10].turn;
            this.BuffAttachments.Add(attachment4);
        Label_0D5C:
            num10 += 1;
        Label_0D62:
            if (num10 < ((int) data.buff.Length))
            {
                goto Label_05DC;
            }
        Label_0D71:
            if ((data.cond == null) || (core == null))
            {
                goto Label_1045;
            }
            num20 = 0;
            goto Label_1036;
        Label_0D8A:
            param4 = null;
            targets3 = data.cond[num20].skilltarget;
            if (condskill[num20].skill == null)
            {
                goto Label_0DD7;
            }
            effect2 = condskill[num20].skill.GetCondEffect(targets3);
            if (effect2 == null)
            {
                goto Label_0DD7;
            }
            param4 = effect2.param;
        Label_0DD7:
            if ((param4 != null) || (string.IsNullOrEmpty(data.cond[num20].bc_id) != null))
            {
                goto Label_0E15;
            }
            param4 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetCondEffectParam(data.cond[num20].bc_id);
        Label_0E15:
            attachment5 = core.CreateCondAttachment(condskill[num20].user, this, condskill[num20].skill, targets3, param4, data.cond[num20].type, data.cond[num20].condition, (long) data.cond[num20].calc, 0, data.cond[num20].timing, data.cond[num20].turn, data.cond[num20].passive, (data.cond[num20].curse != null) ? 1 : 0);
            if (attachment5 == null)
            {
                goto Label_1030;
            }
            attachment5.CheckTarget = condskill[num20].check;
            attachment5.LinkageID = data.cond[num20].lid;
            attachment5.CondId = data.cond[num20].bc_id;
            if (attachment5.skill == null)
            {
                goto Label_0F9A;
            }
            num21 = 0;
            goto Label_0F88;
        Label_0F08:
            attachment6 = this.CondAttachments[num21];
            if (attachment6.skill == null)
            {
                goto Label_0F82;
            }
            if ((attachment6.skill.SkillID == attachment5.skill.SkillID) == null)
            {
                goto Label_0F82;
            }
            if (attachment6.CondType != attachment5.CondType)
            {
                goto Label_0F82;
            }
            if (attachment6.Condition != attachment5.Condition)
            {
                goto Label_0F82;
            }
            this.CondAttachments.RemoveAt(num21--);
            goto Label_0F9A;
        Label_0F82:
            num21 += 1;
        Label_0F88:
            if (num21 < this.CondAttachments.Count)
            {
                goto Label_0F08;
            }
        Label_0F9A:
            this.CondAttachments.Add(attachment5);
            if (attachment5.LinkageID == null)
            {
                goto Label_1030;
            }
            flag = 0;
            if (data.buff == null)
            {
                goto Label_1014;
            }
            buffArray = data.buff;
            num22 = 0;
            goto Label_1009;
        Label_0FD1:
            buff = buffArray[num22];
            if (buff.lid != attachment5.LinkageID)
            {
                goto Label_1003;
            }
            this.CondLinkageBuffAttach(attachment5, buff.turn);
            flag = 1;
            goto Label_1014;
        Label_1003:
            num22 += 1;
        Label_1009:
            if (num22 < ((int) buffArray.Length))
            {
                goto Label_0FD1;
            }
        Label_1014:
            if (flag != null)
            {
                goto Label_1030;
            }
            this.CondLinkageBuffAttach(attachment5, attachment5.turn);
        Label_1030:
            num20 += 1;
        Label_1036:
            if (num20 < ((int) data.cond.Length))
            {
                goto Label_0D8A;
            }
        Label_1045:
            this.mShields.Clear();
            if (data.shields == null)
            {
                goto Label_10D9;
            }
            shieldArray = data.shields;
            num23 = 0;
            goto Label_10CE;
        Label_106B:
            shield = shieldArray[num23];
            param5 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetSkillParam(shield.inm);
            if (param5 != null)
            {
                goto Label_1096;
            }
            goto Label_10C8;
        Label_1096:
            this.AddShieldSuspend(param5, shield.nhp, shield.mhp, shield.ntu, shield.mtu, shield.drt, shield.dvl);
        Label_10C8:
            num23 += 1;
        Label_10CE:
            if (num23 < ((int) shieldArray.Length))
            {
                goto Label_106B;
            }
        Label_10D9:
            this.ClearJudgeHpLists();
            if (data.hpis == null)
            {
                goto Label_1130;
            }
            strArray = data.hpis;
            num24 = 0;
            goto Label_1125;
        Label_10FA:
            str = strArray[num24];
            data6 = this.GetSkillData(str);
            if (data6 != null)
            {
                goto Label_1117;
            }
            goto Label_111F;
        Label_1117:
            this.AddJudgeHpLists(data6);
        Label_111F:
            num24 += 1;
        Label_1125:
            if (num24 < ((int) strArray.Length))
            {
                goto Label_10FA;
            }
        Label_1130:
            this.ClearMhmDamage();
            if (data.mhm_dmgs == null)
            {
                goto Label_117D;
            }
            dmgArray = data.mhm_dmgs;
            num25 = 0;
            goto Label_1172;
        Label_1151:
            dmg = dmgArray[num25];
            this.AddMhmDamage(dmg.typ, dmg.dmg);
            num25 += 1;
        Label_1172:
            if (num25 < ((int) dmgArray.Length))
            {
                goto Label_1151;
            }
        Label_117D:
            this.CurrentStatus.param.hp = data.hp;
            this.UpdateBuffEffects();
            this.UpdateCondEffects();
            this.CalcCurrentStatus(0, 0);
            this.mUnitFlag = data.flag;
            this.mEntryUnit = (data.entry == 0) == 0;
            return 1;
        }

        private unsafe void SetupStatus()
        {
            BaseStatus status;
            QuestParam param;
            GameManager manager;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            BuffEffectParam param2;
            BuffEffect effect;
            status = new BaseStatus();
            this.mUnitData.Status.CopyTo(status);
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0119;
            }
            if (this.IsBreakObj != null)
            {
                goto Label_0119;
            }
            param = SceneBattle.Instance.CurrentQuest;
            if (param == null)
            {
                goto Label_0119;
            }
            manager = MonoSingleton<GameManager>.Instance;
            if ((manager != null) == null)
            {
                goto Label_0119;
            }
            if (string.IsNullOrEmpty(param.MapBuff) != null)
            {
                goto Label_0119;
            }
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            status5 = new BaseStatus();
            effect = BuffEffect.CreateBuffEffect(manager.MasterParam.GetBuffEffectParam(param.MapBuff), 0, 0);
            effect.CalcBuffStatus(&status2, this.Element, 0, 0, 0, 0, 0);
            effect.CalcBuffStatus(&status3, this.Element, 0, 0, 0, 1, 0);
            effect.CalcBuffStatus(&status4, this.Element, 1, 0, 0, 0, 0);
            effect.CalcBuffStatus(&status5, this.Element, 1, 0, 0, 1, 0);
            status3.Add(status4);
            status.AddRate(status3);
            status.Add(status2);
            status.Add(status4);
            status.param.ApplyMinVal();
        Label_0119:
            status.CopyTo(this.mMaximumStatusWithMap);
            status.CopyTo(this.mMaximumStatus);
            status.CopyTo(this.mCurrentStatus);
            this.mMaximumStatusHp = this.mMaximumStatus.param.hp;
            return;
        }

        public unsafe bool SetupSuspend(BattleCore bc, BattleSuspend.Data.UnitInfo unit_info)
        {
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            GridMap<bool> map;
            int num;
            int num2;
            GameManager manager;
            int num3;
            BattleSuspend.Data.UnitInfo.AbilChg chg;
            AbilityChange change;
            int num4;
            BattleSuspend.Data.UnitInfo.AbilChg.Data data;
            AbilityParam param;
            AbilityParam param2;
            int num5;
            BattleSuspend.Data.UnitInfo.AddedAbil abil;
            AbilityParam param3;
            int num6;
            BattleSuspend.Data.UnitInfo.SkillUse use;
            int num7;
            int num8;
            SkillData data2;
            BattleSuspend.Data.UnitInfo.Buff buff;
            List<BattleSuspend.Data.UnitInfo.Buff>.Enumerator enumerator;
            Unit unit;
            Unit unit2;
            SkillData data3;
            SRPG.EventTrigger trigger;
            BuffAttachment attachment;
            SkillEffectTargets targets;
            BuffEffect effect;
            BuffAttachment attachment2;
            List<BuffAttachment>.Enumerator enumerator2;
            List<Unit> list;
            int num9;
            Unit unit3;
            ESkillCondition condition;
            EffectCheckTargets targets2;
            EffectCheckTimings timings;
            int num10;
            int num11;
            BuffAttachment attachment3;
            BattleSuspend.Data.UnitInfo.Cond cond;
            List<BattleSuspend.Data.UnitInfo.Cond>.Enumerator enumerator3;
            Unit unit4;
            Unit unit5;
            SkillData data4;
            SkillEffectTargets targets3;
            CondEffectParam param4;
            CondEffect effect2;
            CondAttachment attachment4;
            bool flag;
            BattleSuspend.Data.UnitInfo.Buff buff2;
            List<BattleSuspend.Data.UnitInfo.Buff>.Enumerator enumerator4;
            BattleSuspend.Data.UnitInfo.Shield shield;
            List<BattleSuspend.Data.UnitInfo.Shield>.Enumerator enumerator5;
            SkillParam param5;
            string str;
            List<string>.Enumerator enumerator6;
            SkillData data5;
            BattleSuspend.Data.UnitInfo.MhmDmg dmg;
            List<BattleSuspend.Data.UnitInfo.MhmDmg>.Enumerator enumerator7;
            SkillParamCalcTypes types;
            if (bc == null)
            {
                goto Label_000C;
            }
            if (unit_info != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            status = new BaseStatus();
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            this.CurrentStatus.param.hp = unit_info.nhp;
            this.mUnitChangedHp = unit_info.chp;
            this.Gems = unit_info.gem;
            this.x = unit_info.ugx;
            this.y = unit_info.ugy;
            this.Direction = unit_info.dir;
            this.UnitFlag = unit_info.ufg;
            this.IsSub = unit_info.isb;
            this.ChargeTime = unit_info.crt;
            this.Target = BattleSuspend.GetUnitFromAllUnits(bc, unit_info.tgi);
            this.mRageTarget = BattleSuspend.GetUnitFromAllUnits(bc, unit_info.rti);
            if (string.IsNullOrEmpty(unit_info.csi) != null)
            {
                goto Label_01AA;
            }
            this.mCastSkill = this.GetSkillData(unit_info.csi);
            this.mCastTime = unit_info.ctm;
            this.mCastIndex = unit_info.cid;
            if (unit_info.cgm == null)
            {
                goto Label_0170;
            }
            map = new GridMap<bool>(unit_info.cgw, unit_info.cgh);
            if (map == null)
            {
                goto Label_0170;
            }
            num = 0;
            goto Label_0159;
        Label_013B:
            map.set(num, (unit_info.cgm[num] == 0) == 0);
            num += 1;
        Label_0159:
            if (num < ((int) unit_info.cgm.Length))
            {
                goto Label_013B;
            }
            this.CastSkillGridMap = map;
        Label_0170:
            if (bc.CurrentMap == null)
            {
                goto Label_0198;
            }
            this.mGridTarget = bc.CurrentMap[unit_info.ctx, unit_info.cty];
        Label_0198:
            this.mUnitTarget = BattleSuspend.GetUnitFromAllUnits(bc, unit_info.cti);
        Label_01AA:
            this.mDeathCount = unit_info.dct;
            this.mAutoJewel = unit_info.ajw;
            this.WaitClock = unit_info.wtt;
            this.WaitMoveTurn = unit_info.mvt;
            this.mActionCount = unit_info.acc;
            this.mTurnCount = unit_info.tuc;
            if (this.mEventTrigger == null)
            {
                goto Label_021D;
            }
            this.mEventTrigger.Count = unit_info.trc;
        Label_021D:
            this.KillCount = unit_info.klc;
            if (this.mEntryTriggers == null)
            {
                goto Label_0294;
            }
            if (unit_info.etr == null)
            {
                goto Label_0294;
            }
            num2 = 0;
            goto Label_0285;
        Label_0247:
            if (num2 < this.mEntryTriggers.Count)
            {
                goto Label_025E;
            }
            goto Label_0294;
        Label_025E:
            this.mEntryTriggers[num2].on = (unit_info.etr[num2] == 0) == 0;
            num2 += 1;
        Label_0285:
            if (num2 < ((int) unit_info.etr.Length))
            {
                goto Label_0247;
            }
        Label_0294:
            this.mAIActionIndex = unit_info.aid;
            this.mAIActionTurnCount = unit_info.atu;
            this.mAIPatrolIndex = unit_info.apt;
            this.mCreateBreakObjId = unit_info.boi;
            this.mCreateBreakObjClock = unit_info.boc;
            this.mTeamId = unit_info.tid;
            this.mFriendStates = unit_info.fst;
            manager = MonoSingleton<GameManager>.Instance;
            this.mAbilityChangeLists.Clear();
            if (unit_info.acl.Count == null)
            {
                goto Label_042E;
            }
            num3 = 0;
            goto Label_041C;
        Label_0321:
            chg = unit_info.acl[num3];
            if (chg.acd.Count != null)
            {
                goto Label_0346;
            }
            goto Label_0416;
        Label_0346:
            change = new AbilityChange();
            num4 = 0;
            goto Label_03DE;
        Label_0355:
            data = chg.acd[num4];
            param = manager.MasterParam.GetAbilityParam(data.fid);
            param2 = manager.MasterParam.GetAbilityParam(data.tid);
            if (param == null)
            {
                goto Label_039D;
            }
            if (param2 != null)
            {
                goto Label_03A5;
            }
        Label_039D:
            change = null;
            goto Label_03F1;
        Label_03A5:
            change.Add(param, param2, data.tur, (data.irs == 0) == 0, data.exp, (data.iif == 0) == 0);
            num4 += 1;
        Label_03DE:
            if (num4 < chg.acd.Count)
            {
                goto Label_0355;
            }
        Label_03F1:
            if (change == null)
            {
                goto Label_0416;
            }
            if (change.mDataLists.Count == null)
            {
                goto Label_0416;
            }
            this.mAbilityChangeLists.Add(change);
        Label_0416:
            num3 += 1;
        Label_041C:
            if (num3 < unit_info.acl.Count)
            {
                goto Label_0321;
            }
        Label_042E:
            this.mAddedAbilitys.Clear();
            this.mAddedSkills.Clear();
            if (unit_info.aal.Count == null)
            {
                goto Label_04BA;
            }
            num5 = 0;
            goto Label_04A2;
        Label_045C:
            abil = unit_info.aal[num5];
            param3 = manager.MasterParam.GetAbilityParam(abil.aid);
            if (param3 != null)
            {
                goto Label_048C;
            }
            goto Label_049C;
        Label_048C:
            this.CreateAddedAbilityAndSkills(param3, abil.exp);
        Label_049C:
            num5 += 1;
        Label_04A2:
            if (num5 < unit_info.aal.Count)
            {
                goto Label_045C;
            }
            this.RefleshBattleAbilitysAndSkills();
        Label_04BA:
            num6 = 0;
            goto Label_054B;
        Label_04C2:
            use = unit_info.sul[num6];
            num7 = 0;
            num8 = 0;
            goto Label_050B;
        Label_04DC:
            if ((use.sid == unit_info.sul[num8].sid) == null)
            {
                goto Label_0505;
            }
            num7 += 1;
        Label_0505:
            num8 += 1;
        Label_050B:
            if (num8 < num6)
            {
                goto Label_04DC;
            }
            data2 = this.GetSkillForUseCount(use.sid, num7);
            if (data2 == null)
            {
                goto Label_0545;
            }
            this.mSkillUseCount[data2] = use.ctr;
        Label_0545:
            num6 += 1;
        Label_054B:
            if (num6 < unit_info.sul.Count)
            {
                goto Label_04C2;
            }
            this.SuspendClearBuffCondEffects(0);
            enumerator = unit_info.bfl.GetEnumerator();
        Label_0571:
            try
            {
                goto Label_0A3F;
            Label_0576:
                buff = &enumerator.Current;
                unit = BattleSuspend.GetUnitFromAllUnits(bc, buff.uni);
                unit2 = BattleSuspend.GetUnitFromAllUnits(bc, buff.cui);
                data3 = (unit == null) ? null : unit.GetSkillData(buff.sid);
                if (data3 != null)
                {
                    goto Label_05DE;
                }
                data3 = (unit == null) ? null : unit.SearchArtifactSkill(buff.sid);
            Label_05DE:
                if (data3 != null)
                {
                    goto Label_05F5;
                }
                data3 = this.SearchItemSkill(bc, buff.sid);
            Label_05F5:
                if (unit2 != null)
                {
                    goto Label_0601;
                }
                goto Label_0A3F;
            Label_0601:
                if (data3 != null)
                {
                    goto Label_065A;
                }
                if (buff.ipa == null)
                {
                    goto Label_0619;
                }
                goto Label_0A3F;
            Label_0619:
                trigger = unit.EventTrigger;
                if (trigger == null)
                {
                    goto Label_0A3F;
                }
                attachment = trigger.MakeBuff(unit, this);
                attachment.turn = buff.tur;
                this.BuffAttachments.Add(attachment);
                goto Label_0A3F;
            Label_065A:
                targets = buff.stg;
                effect = data3.GetBuffEffect(targets);
                if (effect == null)
                {
                    goto Label_0A3F;
                }
                if (effect.param != null)
                {
                    goto Label_0686;
                }
                goto Label_0A3F;
            Label_0686:
                if (buff.ipa == null)
                {
                    goto Label_078D;
                }
                if (data3 == null)
                {
                    goto Label_078D;
                }
                if (data3.IsSubActuate() != null)
                {
                    goto Label_06B2;
                }
                if (this.ContainsSkillAttachment(data3) == null)
                {
                    goto Label_078D;
                }
            Label_06B2:
                if (effect.param.mIsUpBuff == null)
                {
                    goto Label_0A3F;
                }
                enumerator2 = this.BuffAttachments.GetEnumerator();
            Label_06D5:
                try
                {
                    goto Label_076A;
                Label_06DA:
                    attachment2 = &enumerator2.Current;
                    if (attachment2.skill == null)
                    {
                        goto Label_076A;
                    }
                    if ((attachment2.skill.SkillID == data3.SkillID) == null)
                    {
                        goto Label_076A;
                    }
                    if (attachment2.Param == null)
                    {
                        goto Label_076A;
                    }
                    if ((attachment2.Param.iname == effect.param.iname) == null)
                    {
                        goto Label_076A;
                    }
                    if (attachment2.skilltarget != buff.stg)
                    {
                        goto Label_076A;
                    }
                    attachment2.UpBuffCount = buff.ubc;
                    this.UpdateUpBuffEffect(attachment2, 0, 0);
                Label_076A:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_06DA;
                    }
                    goto Label_0788;
                }
                finally
                {
                Label_077B:
                    ((List<BuffAttachment>.Enumerator) enumerator2).Dispose();
                }
            Label_0788:
                goto Label_0A3F;
            Label_078D:
                status.Clear();
                status3.Clear();
                status2.Clear();
                status4.Clear();
                data3.BuffSkill(data3.Timing, this.UnitData.Element, status, status, status3, status2, status2, status4, null, targets, 1, null);
                list = new List<Unit>();
                if (data3.SkillParam.AbsorbAndGive == null)
                {
                    goto Label_084D;
                }
                if (buff.atl.Count == null)
                {
                    goto Label_083A;
                }
                num9 = 0;
                goto Label_0827;
            Label_07FB:
                unit3 = BattleSuspend.GetUnitFromAllUnits(bc, buff.atl[num9]);
                if (unit3 == null)
                {
                    goto Label_0821;
                }
                list.Add(unit3);
            Label_0821:
                num9 += 1;
            Label_0827:
                if (num9 < buff.atl.Count)
                {
                    goto Label_07FB;
                }
            Label_083A:
                this.AbsorbAndGiveExchangeBuff(unit, this, data3, effect, list, status, status3, status2, status4);
            Label_084D:
                condition = effect.param.cond;
                targets2 = effect.param.chk_target;
                timings = effect.param.chk_timing;
                num10 = data3.DuplicateCount;
                num11 = buff.tur;
                attachment3 = null;
                if (buff.btp != null)
                {
                    goto Label_0932;
                }
                types = buff.ctp;
                if (types == null)
                {
                    goto Label_08B5;
                }
                if (types == 1)
                {
                    goto Label_08F1;
                }
                goto Label_092D;
            Label_08B5:
                attachment3 = bc.CreateBuffAttachment(unit, this, data3, targets, effect.param, 0, (buff.vtp == 0) == 0, 0, status, condition, num11, targets2, timings, buff.ipa, num10);
                goto Label_092D;
            Label_08F1:
                attachment3 = bc.CreateBuffAttachment(unit, this, data3, targets, effect.param, 0, (buff.vtp == 0) == 0, 1, status3, condition, num11, targets2, timings, buff.ipa, num10);
            Label_092D:
                goto Label_09C7;
            Label_0932:
                types = buff.ctp;
                if (types == null)
                {
                    goto Label_094F;
                }
                if (types == 1)
                {
                    goto Label_098B;
                }
                goto Label_09C7;
            Label_094F:
                attachment3 = bc.CreateBuffAttachment(unit, this, data3, targets, effect.param, 1, (buff.vtp == 0) == 0, 0, status2, condition, num11, targets2, timings, buff.ipa, num10);
                goto Label_09C7;
            Label_098B:
                attachment3 = bc.CreateBuffAttachment(unit, this, data3, targets, effect.param, 1, (buff.vtp == 0) == 0, 1, status4, condition, num11, targets2, timings, buff.ipa, num10);
            Label_09C7:
                if (attachment3 == null)
                {
                    goto Label_0A3F;
                }
                attachment3.turn = num11;
                attachment3.LinkageID = buff.lid;
                attachment3.UpBuffCount = buff.ubc;
                if (attachment3.Param == null)
                {
                    goto Label_0A29;
                }
                if (attachment3.Param.mIsUpBuff == null)
                {
                    goto Label_0A29;
                }
                this.UpdateUpBuffEffect(attachment3, 0, 0);
            Label_0A29:
                attachment3.AagTargetLists = list;
                this.BuffAttachments.Add(attachment3);
            Label_0A3F:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0576;
                }
                goto Label_0A5D;
            }
            finally
            {
            Label_0A50:
                ((List<BattleSuspend.Data.UnitInfo.Buff>.Enumerator) enumerator).Dispose();
            }
        Label_0A5D:
            enumerator3 = unit_info.cdl.GetEnumerator();
        Label_0A6A:
            try
            {
                goto Label_0CB0;
            Label_0A6F:
                cond = &enumerator3.Current;
                unit4 = BattleSuspend.GetUnitFromAllUnits(bc, cond.uni);
                unit5 = BattleSuspend.GetUnitFromAllUnits(bc, cond.cui);
                data4 = (unit4 == null) ? null : unit4.GetSkillData(cond.sid);
                if (data4 != null)
                {
                    goto Label_0AD7;
                }
                data4 = (unit4 == null) ? null : unit4.SearchArtifactSkill(cond.sid);
            Label_0AD7:
                if (data4 != null)
                {
                    goto Label_0AEE;
                }
                data4 = this.SearchItemSkill(bc, cond.sid);
            Label_0AEE:
                if (unit5 != null)
                {
                    goto Label_0AFA;
                }
                goto Label_0CB0;
            Label_0AFA:
                if (data4 != null)
                {
                    goto Label_0B17;
                }
                if (cond.ipa == null)
                {
                    goto Label_0B46;
                }
                goto Label_0CB0;
                goto Label_0B46;
            Label_0B17:
                if (cond.ipa == null)
                {
                    goto Label_0B46;
                }
                if (data4.IsSubActuate() == null)
                {
                    goto Label_0B34;
                }
                goto Label_0CB0;
            Label_0B34:
                if (this.ContainsSkillAttachment(data4) == null)
                {
                    goto Label_0B46;
                }
                goto Label_0CB0;
            Label_0B46:
                targets3 = cond.stg;
                param4 = null;
                if (data4 == null)
                {
                    goto Label_0B74;
                }
                effect2 = data4.GetCondEffect(targets3);
                if (effect2 == null)
                {
                    goto Label_0B74;
                }
                param4 = effect2.param;
            Label_0B74:
                if (param4 != null)
                {
                    goto Label_0BA4;
                }
                if (string.IsNullOrEmpty(cond.cid) != null)
                {
                    goto Label_0BA4;
                }
                param4 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetCondEffectParam(cond.cid);
            Label_0BA4:
                attachment4 = bc.CreateCondAttachment(unit4, this, data4, targets3, param4, cond.cdt, cond.ucd, (long) cond.cnd, 0, cond.tim, cond.tur, cond.ipa, cond.icu);
                if (attachment4 == null)
                {
                    goto Label_0CB0;
                }
                attachment4.CheckTarget = unit5;
                attachment4.LinkageID = cond.lid;
                attachment4.CondId = cond.cid;
                this.CondAttachments.Add(attachment4);
                if (attachment4.LinkageID == null)
                {
                    goto Label_0CB0;
                }
                flag = 0;
                enumerator4 = unit_info.bfl.GetEnumerator();
            Label_0C3D:
                try
                {
                    goto Label_0C76;
                Label_0C42:
                    buff2 = &enumerator4.Current;
                    if (buff2.lid != attachment4.LinkageID)
                    {
                        goto Label_0C76;
                    }
                    this.CondLinkageBuffAttach(attachment4, buff2.tur);
                    flag = 1;
                    goto Label_0C82;
                Label_0C76:
                    if (&enumerator4.MoveNext() != null)
                    {
                        goto Label_0C42;
                    }
                Label_0C82:
                    goto Label_0C94;
                }
                finally
                {
                Label_0C87:
                    ((List<BattleSuspend.Data.UnitInfo.Buff>.Enumerator) enumerator4).Dispose();
                }
            Label_0C94:
                if (flag != null)
                {
                    goto Label_0CB0;
                }
                this.CondLinkageBuffAttach(attachment4, attachment4.turn);
            Label_0CB0:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_0A6F;
                }
                goto Label_0CCE;
            }
            finally
            {
            Label_0CC1:
                ((List<BattleSuspend.Data.UnitInfo.Cond>.Enumerator) enumerator3).Dispose();
            }
        Label_0CCE:
            this.mShields.Clear();
            enumerator5 = unit_info.shl.GetEnumerator();
        Label_0CE6:
            try
            {
                goto Label_0D4A;
            Label_0CEB:
                shield = &enumerator5.Current;
                param5 = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetSkillParam(shield.inm);
                if (param5 != null)
                {
                    goto Label_0D18;
                }
                goto Label_0D4A;
            Label_0D18:
                this.AddShieldSuspend(param5, shield.nhp, shield.mhp, shield.ntu, shield.mtu, shield.drt, shield.dvl);
            Label_0D4A:
                if (&enumerator5.MoveNext() != null)
                {
                    goto Label_0CEB;
                }
                goto Label_0D68;
            }
            finally
            {
            Label_0D5B:
                ((List<BattleSuspend.Data.UnitInfo.Shield>.Enumerator) enumerator5).Dispose();
            }
        Label_0D68:
            this.ClearJudgeHpLists();
            enumerator6 = unit_info.hpi.GetEnumerator();
        Label_0D7B:
            try
            {
                goto Label_0DA7;
            Label_0D80:
                str = &enumerator6.Current;
                data5 = this.GetSkillData(str);
                if (data5 != null)
                {
                    goto Label_0D9F;
                }
                goto Label_0DA7;
            Label_0D9F:
                this.AddJudgeHpLists(data5);
            Label_0DA7:
                if (&enumerator6.MoveNext() != null)
                {
                    goto Label_0D80;
                }
                goto Label_0DC5;
            }
            finally
            {
            Label_0DB8:
                ((List<string>.Enumerator) enumerator6).Dispose();
            }
        Label_0DC5:
            this.ClearMhmDamage();
            enumerator7 = unit_info.mhl.GetEnumerator();
        Label_0DD8:
            try
            {
                goto Label_0DFA;
            Label_0DDD:
                dmg = &enumerator7.Current;
                this.AddMhmDamage(dmg.typ, dmg.dmg);
            Label_0DFA:
                if (&enumerator7.MoveNext() != null)
                {
                    goto Label_0DDD;
                }
                goto Label_0E18;
            }
            finally
            {
            Label_0E0B:
                ((List<BattleSuspend.Data.UnitInfo.MhmDmg>.Enumerator) enumerator7).Dispose();
            }
        Label_0E18:
            this.CurrentStatus.param.hp = unit_info.nhp;
            this.UpdateBuffEffects();
            this.UpdateCondEffects();
            this.CalcCurrentStatus(0, 0);
            return 1;
        }

        public void SuspendClearBuffCondEffects(bool is_multi)
        {
            int num;
            BuffAttachment attachment;
            int num2;
            CondAttachment attachment2;
            num = 0;
            goto Label_00EB;
        Label_0007:
            attachment = this.BuffAttachments[num];
            if (is_multi == null)
            {
                goto Label_0081;
            }
            if (attachment.skill == null)
            {
                goto Label_0081;
            }
            if (attachment.skill.Condition != 4)
            {
                goto Label_004B;
            }
            this.BuffAttachments.RemoveAt(num--);
            goto Label_00E7;
        Label_004B:
            if (attachment.IsPassive == null)
            {
                goto Label_0081;
            }
            if (attachment.skill.DuplicateCount <= 0)
            {
                goto Label_0081;
            }
            this.BuffAttachments.RemoveAt(num--);
            goto Label_00E7;
        Label_0081:
            if (attachment.IsPassive == null)
            {
                goto Label_0096;
            }
            goto Label_00E7;
        Label_0096:
            if (attachment.skill == null)
            {
                goto Label_00B6;
            }
            if (attachment.skill.IsPassiveSkill() == null)
            {
                goto Label_00B6;
            }
            goto Label_00E7;
        Label_00B6:
            if (attachment.skill == null)
            {
                goto Label_00E7;
            }
            if (attachment.skill.Timing != 8)
            {
                goto Label_00E7;
            }
            this.BuffAttachments.RemoveAt(num--);
        Label_00E7:
            num += 1;
        Label_00EB:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0007;
            }
            num2 = 0;
            goto Label_01AB;
        Label_0103:
            attachment2 = this.CondAttachments[num2];
            if (attachment2.IsPassive == null)
            {
                goto Label_0125;
            }
            goto Label_01A7;
        Label_0125:
            if (attachment2.skill == null)
            {
                goto Label_0145;
            }
            if (attachment2.skill.IsPassiveSkill() == null)
            {
                goto Label_0145;
            }
            goto Label_01A7;
        Label_0145:
            if (attachment2.skill == null)
            {
                goto Label_0176;
            }
            if (attachment2.skill.Timing != 8)
            {
                goto Label_0176;
            }
            this.CondAttachments.RemoveAt(num2--);
            goto Label_01A7;
        Label_0176:
            if (attachment2.UseCondition != 3)
            {
                goto Label_01A7;
            }
            if (attachment2.IsPassive != null)
            {
                goto Label_01A7;
            }
            this.CondAttachments.RemoveAt(num2--);
        Label_01A7:
            num2 += 1;
        Label_01AB:
            if (num2 < this.CondAttachments.Count)
            {
                goto Label_0103;
            }
            return;
        }

        public void UpdateAbilityChange()
        {
            bool flag;
            int num;
            AbilityChange change;
            int num2;
            AbilityChange.Data data;
            bool flag2;
            int num3;
            AbilityChange.Data data2;
            flag = 0;
            num = this.mAbilityChangeLists.Count - 1;
            goto Label_0113;
        Label_0015:
            change = this.mAbilityChangeLists[num];
            num2 = change.mDataLists.Count - 1;
            goto Label_0073;
        Label_0035:
            data = change.mDataLists[num2];
            if (data.mIsInfinite != null)
            {
                goto Label_006F;
            }
            if (data.mTurn != null)
            {
                goto Label_0060;
            }
            goto Label_006F;
        Label_0060:
            data.mTurn -= 1;
        Label_006F:
            num2 -= 1;
        Label_0073:
            if (num2 >= 0)
            {
                goto Label_0035;
            }
            flag2 = 0;
            num3 = change.mDataLists.Count - 1;
            goto Label_00DE;
        Label_0091:
            data2 = change.mDataLists[num3];
            if (data2.mIsInfinite != null)
            {
                goto Label_00E6;
            }
            if (data2.mTurn == null)
            {
                goto Label_00BD;
            }
            goto Label_00E6;
        Label_00BD:
            flag2 |= data2.mIsReset;
            change.mDataLists.RemoveAt(num3);
            flag = 1;
            num3 -= 1;
        Label_00DE:
            if (num3 >= 0)
            {
                goto Label_0091;
            }
        Label_00E6:
            if (change.mDataLists.Count == null)
            {
                goto Label_00FD;
            }
            if (flag2 == null)
            {
                goto Label_010F;
            }
        Label_00FD:
            change.Clear();
            this.mAbilityChangeLists.RemoveAt(num);
        Label_010F:
            num -= 1;
        Label_0113:
            if (num >= 0)
            {
                goto Label_0015;
            }
            if (flag == null)
            {
                goto Label_0126;
            }
            this.RefleshBattleAbilitysAndSkills();
        Label_0126:
            return;
        }

        public unsafe void UpdateBuffEffects()
        {
            int num;
            BuffAttachment attachment;
            SkillData data;
            bool flag;
            CondAttachment attachment2;
            List<CondAttachment>.Enumerator enumerator;
            Unit unit;
            SkillData data2;
            num = 0;
            goto Label_01DC;
        Label_0007:
            attachment = this.BuffAttachments[num];
            data = attachment.skill;
            if (attachment.LinkageID == null)
            {
                goto Label_0095;
            }
            flag = 0;
            enumerator = this.CondAttachments.GetEnumerator();
        Label_0035:
            try
            {
                goto Label_005C;
            Label_003A:
                attachment2 = &enumerator.Current;
                if (attachment2.LinkageID != attachment.LinkageID)
                {
                    goto Label_005C;
                }
                flag = 1;
                goto Label_0068;
            Label_005C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_003A;
                }
            Label_0068:
                goto Label_007A;
            }
            finally
            {
            Label_006D:
                ((List<CondAttachment>.Enumerator) enumerator).Dispose();
            }
        Label_007A:
            if (flag != null)
            {
                goto Label_0095;
            }
            this.BuffAttachments.RemoveAt(num--);
            goto Label_01D8;
        Label_0095:
            if (attachment.IsPassive == null)
            {
                goto Label_0112;
            }
            if (data == null)
            {
                goto Label_01D8;
            }
            if (data.IsSubActuate() != null)
            {
                goto Label_01D8;
            }
            unit = attachment.user;
            if (unit == null)
            {
                goto Label_01D8;
            }
            if (unit.IsDead == null)
            {
                goto Label_01D8;
            }
            data2 = unit.LeaderSkill;
            if (data2 == null)
            {
                goto Label_00F8;
            }
            if ((data.SkillID != data2.SkillID) == null)
            {
                goto Label_01D8;
            }
        Label_00F8:
            this.BuffAttachments.RemoveAt(num--);
            goto Label_01D8;
            goto Label_01D8;
        Label_0112:
            if (data == null)
            {
                goto Label_0123;
            }
            if (data.IsPassiveSkill() != null)
            {
                goto Label_0164;
            }
        Label_0123:
            if (attachment.Param == null)
            {
                goto Label_013E;
            }
            if (attachment.Param.IsNoDisabled != null)
            {
                goto Label_0164;
            }
        Label_013E:
            if (this.IsEnableBuffEffect(attachment.BuffType) != null)
            {
                goto Label_0164;
            }
            this.BuffAttachments.RemoveAt(num--);
            goto Label_01D8;
        Label_0164:
            if (attachment.CheckTarget == null)
            {
                goto Label_0194;
            }
            if (attachment.CheckTarget.IsDeadCondition() == null)
            {
                goto Label_0194;
            }
            this.BuffAttachments.RemoveAt(num--);
            goto Label_01D8;
        Label_0194:
            if (attachment.CheckTiming == 1)
            {
                goto Label_01D8;
            }
            if (attachment.CheckTiming != 10)
            {
                goto Label_01B2;
            }
            goto Label_01D8;
        Label_01B2:
            if (attachment.turn <= 0)
            {
                goto Label_01C8;
            }
            goto Label_01D8;
        Label_01C8:
            this.BuffAttachments.RemoveAt(num--);
        Label_01D8:
            num += 1;
        Label_01DC:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void UpdateBuffEffectTurnCount(EffectCheckTimings timing, Unit current)
        {
            int num;
            BuffAttachment attachment;
            EffectCheckTimings timings;
            num = 0;
            goto Label_00B7;
        Label_0007:
            attachment = this.BuffAttachments[num];
            if (current == null)
            {
                goto Label_002A;
            }
            if (current != this)
            {
                goto Label_002A;
            }
            this.UpdateUpBuffEffect(attachment, timing, 1);
        Label_002A:
            if (attachment.IsPassive == null)
            {
                goto Label_003F;
            }
            goto Label_00B3;
        Label_003F:
            timings = attachment.CheckTiming;
            if (timings == 6)
            {
                goto Label_008F;
            }
            if (timings == 7)
            {
                goto Label_008F;
            }
            if (timings == 9)
            {
                goto Label_008F;
            }
            if (attachment.CheckTarget == null)
            {
                goto Label_007D;
            }
            if (attachment.CheckTarget == current)
            {
                goto Label_008F;
            }
            goto Label_00B3;
            goto Label_008F;
        Label_007D:
            if (current == null)
            {
                goto Label_008F;
            }
            if (current == this)
            {
                goto Label_008F;
            }
            goto Label_00B3;
        Label_008F:
            if (timings != timing)
            {
                goto Label_00B3;
            }
            if (timings != 1)
            {
                goto Label_00A2;
            }
            goto Label_00B3;
        Label_00A2:
            attachment.turn = OInt.op_Decrement(attachment.turn);
        Label_00B3:
            num += 1;
        Label_00B7:
            if (num < this.BuffAttachments.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public bool UpdateCastTime()
        {
            if (this.mCastSkill == null)
            {
                goto Label_0042;
            }
            if (this.IsUnitCondition(0x10000L) == null)
            {
                goto Label_001E;
            }
            return 0;
        Label_001E:
            this.mCastTime += this.GetCastSpeed();
            return 1;
        Label_0042:
            return 0;
        }

        public bool UpdateChargeTime()
        {
            if (this.IsUnitCondition(0x10000L) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            if (this.mCastSkill == null)
            {
                goto Label_0035;
            }
            if (this.mCastSkill.SkillParam.IsNoChargeCalcCT() == null)
            {
                goto Label_0035;
            }
            return 0;
        Label_0035:
            this.mChargeTime += this.GetChargeSpeed();
            return 1;
        }

        public void UpdateClockTime()
        {
            OInt num;
            if (this.IsEntry != null)
            {
                goto Label_0035;
            }
            this.mWaitEntryClock = Math.Max(this.mWaitEntryClock = OInt.op_Decrement(this.mWaitEntryClock), 0);
        Label_0035:
            this.UpdateBuffEffectTurnCount(6, this);
            this.UpdateCondEffectTurnCount(6, this);
            this.UpdateBuffEffects();
            this.UpdateCondEffects();
            return;
        }

        public void UpdateCondEffects()
        {
            long num;
            long num2;
            long num3;
            bool flag;
            int num4;
            CondAttachment attachment;
            int num5;
            CondAttachment attachment2;
            int num6;
            EUnitCondition condition;
            int num7;
            long num8;
            ESkillCondition condition2;
            ConditionEffectTypes types;
            num = 0L;
            num2 = 0L;
            num3 = 0L;
            flag = 0;
            num4 = 0;
            goto Label_00F3;
        Label_0013:
            attachment = this.CondAttachments[num4];
            if (attachment.IsPassive != null)
            {
                goto Label_00ED;
            }
            if (attachment.CheckTiming != 1)
            {
                goto Label_0045;
            }
            goto Label_00ED;
        Label_0045:
            if (attachment.IsCurse == null)
            {
                goto Label_0093;
            }
            if (attachment.user == null)
            {
                goto Label_00ED;
            }
            if (attachment.user.IsDead == null)
            {
                goto Label_00ED;
            }
            if (attachment.LinkageBuff == null)
            {
                goto Label_007C;
            }
            flag = 1;
        Label_007C:
            this.CondAttachments.RemoveAt(num4--);
            goto Label_00ED;
        Label_0093:
            if (attachment.turn <= 0)
            {
                goto Label_00AA;
            }
            goto Label_00ED;
        Label_00AA:
            if (attachment.IsFailCondition() == null)
            {
                goto Label_00CD;
            }
            if (this.IsAutoCureCondition(attachment.Condition) != null)
            {
                goto Label_00CD;
            }
            goto Label_00ED;
        Label_00CD:
            if (attachment.LinkageBuff == null)
            {
                goto Label_00DB;
            }
            flag = 1;
        Label_00DB:
            this.CondAttachments.RemoveAt(num4--);
        Label_00ED:
            num4 += 1;
        Label_00F3:
            if (num4 < this.CondAttachments.Count)
            {
                goto Label_0013;
            }
            if (flag == null)
            {
                goto Label_0111;
            }
            this.UpdateBuffEffects();
        Label_0111:
            num5 = 0;
            goto Label_0242;
        Label_0119:
            attachment2 = this.CondAttachments[num5];
            if (attachment2.UseCondition == null)
            {
                goto Label_0194;
            }
            condition2 = attachment2.UseCondition;
            if (condition2 == 1)
            {
                goto Label_0152;
            }
            if (condition2 == 5)
            {
                goto Label_0167;
            }
            goto Label_0194;
        Label_0152:
            if (this.IsDying() != null)
            {
                goto Label_0194;
            }
            goto Label_023C;
            goto Label_0194;
        Label_0167:
            if (attachment2.skill != null)
            {
                goto Label_0178;
            }
            goto Label_023C;
        Label_0178:
            if (attachment2.skill.IsJudgeHp(this) != null)
            {
                goto Label_0194;
            }
            goto Label_023C;
        Label_0194:
            num6 = 0;
            goto Label_022B;
        Label_019C:
            condition = 1L << (num6 & 0x3f);
            if (attachment2.Condition == condition)
            {
                goto Label_01B9;
            }
            goto Label_0225;
        Label_01B9:
            switch ((attachment2.CondType - 2))
            {
                case 0:
                    goto Label_01E0;

                case 1:
                    goto Label_01E0;

                case 2:
                    goto Label_01E0;

                case 3:
                    goto Label_020D;
            }
            goto Label_0220;
        Label_01E0:
            if (attachment2.IsCurse == null)
            {
                goto Label_01FA;
            }
            num3 |= 1L << ((num6 & 0x3f) & 0x3f);
        Label_01FA:
            num |= 1L << ((num6 & 0x3f) & 0x3f);
            goto Label_0225;
        Label_020D:
            num2 |= 1L << ((num6 & 0x3f) & 0x3f);
            goto Label_0225;
        Label_0220:;
        Label_0225:
            num6 += 1;
        Label_022B:
            if (num6 < MAX_UNIT_CONDITION)
            {
                goto Label_019C;
            }
        Label_023C:
            num5 += 1;
        Label_0242:
            if (num5 < this.CondAttachments.Count)
            {
                goto Label_0119;
            }
            this.mCurrentCondition = 0L;
            this.mDisableCondition = 0L;
            num7 = 0;
            goto Label_02CE;
        Label_0276:
            num8 = 1L << (num7 & 0x3f);
            if ((num2 & num8) == null)
            {
                goto Label_02A1;
            }
            this.CureCondEffects(num8, 0, 0);
            if ((num3 & num8) != null)
            {
                goto Label_02A1;
            }
            goto Label_02C8;
        Label_02A1:
            if ((num & num8) == null)
            {
                goto Label_02C8;
            }
            this.mCurrentCondition |= num8;
        Label_02C8:
            num7 += 1;
        Label_02CE:
            if (num7 < MAX_UNIT_CONDITION)
            {
                goto Label_0276;
            }
            this.mDisableCondition = num2;
            if (this.IsUnitCondition(0x800L) != null)
            {
                goto Label_0308;
            }
            this.mDeathCount = -1;
        Label_0308:
            if (this.IsUnitCondition(0x200000L) != null)
            {
                goto Label_0320;
            }
            this.mRageTarget = null;
        Label_0320:
            if (this.IsUnitCondition(2L) != null)
            {
                goto Label_0339;
            }
            this.SetUnitFlag(0x200, 0);
        Label_0339:
            if (this.IsUnitCondition(0x20L) == null)
            {
                goto Label_0355;
            }
            this.mCurrentCondition = 0x20L;
        Label_0355:
            return;
        }

        public void UpdateCondEffectTurnCount(EffectCheckTimings timing, Unit current)
        {
            int num;
            CondAttachment attachment;
            EffectCheckTimings timings;
            EUnitCondition condition;
            num = 0;
            goto Label_010E;
        Label_0007:
            attachment = this.CondAttachments[num];
            if (attachment.IsPassive == null)
            {
                goto Label_0029;
            }
            goto Label_010A;
        Label_0029:
            if (attachment.CheckTiming == 6)
            {
                goto Label_0074;
            }
            if (attachment.CheckTiming == 7)
            {
                goto Label_0074;
            }
            if (attachment.CheckTarget == null)
            {
                goto Label_0062;
            }
            if (attachment.CheckTarget == current)
            {
                goto Label_0074;
            }
            goto Label_010A;
            goto Label_0074;
        Label_0062:
            if (current == null)
            {
                goto Label_0074;
            }
            if (current == this)
            {
                goto Label_0074;
            }
            goto Label_010A;
        Label_0074:
            timings = attachment.CheckTiming;
            if (timings != timing)
            {
                goto Label_010A;
            }
            if (timings != 1)
            {
                goto Label_008E;
            }
            goto Label_010A;
        Label_008E:
            if (attachment.IsFailCondition() == null)
            {
                goto Label_00F9;
            }
            condition = attachment.Condition;
            if (this.mCastSkill == null)
            {
                goto Label_00CC;
            }
            if (this.mCastSkill.CastType != 2)
            {
                goto Label_00CC;
            }
            if (attachment.CheckTiming != null)
            {
                goto Label_00CC;
            }
            goto Label_010A;
        Label_00CC:
            if (this.IsUnitCondition(0x20L) == null)
            {
                goto Label_00E8;
            }
            if (condition == 0x20L)
            {
                goto Label_00E8;
            }
            goto Label_010A;
        Label_00E8:
            if (this.IsAutoCureCondition(condition) != null)
            {
                goto Label_00F9;
            }
            goto Label_010A;
        Label_00F9:
            attachment.turn = OInt.op_Decrement(attachment.turn);
        Label_010A:
            num += 1;
        Label_010E:
            if (num < this.CondAttachments.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private void UpdateDeathSentence()
        {
            OInt num;
            if (this.IsUnitCondition(0x800L) != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.IsUnitCondition(0x20L) == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if (this.mCastSkill == null)
            {
                goto Label_003E;
            }
            if (this.mCastSkill.CastType != 2)
            {
                goto Label_003E;
            }
            return;
        Label_003E:
            this.mDeathCount = Math.Max(this.mDeathCount = OInt.op_Decrement(this.mDeathCount), 0);
            return;
        }

        private void UpdateGuardTurn()
        {
            OInt num;
            if (this.mGuardTarget != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mGuardTurn = Math.Max(this.mGuardTurn = OInt.op_Decrement(this.mGuardTurn), 0);
            if (this.mGuardTurn == null)
            {
                goto Label_0077;
            }
            if (this.CheckEnemySide(this.mGuardTarget) != null)
            {
                goto Label_0077;
            }
            if (this.mGuardTarget.IsDeadCondition() != null)
            {
                goto Label_0077;
            }
            if (this.mGuardTarget.CheckExistMap() != null)
            {
                goto Label_007D;
            }
        Label_0077:
            this.CancelGuradTarget();
        Label_007D:
            return;
        }

        private void UpdateShieldTurn()
        {
            UnitShield local1;
            int num;
            UnitShield shield;
            num = 0;
            goto Label_0098;
        Label_0007:
            shield = this.mShields[num];
            if (shield.skill_param.IsShieldReset() == null)
            {
                goto Label_0030;
            }
            shield.hp = shield.hpMax;
        Label_0030:
            if (this.mShields[num].turn <= 0)
            {
                goto Label_0094;
            }
            local1 = this.mShields[num];
            local1.turn = OInt.op_Decrement(local1.turn);
            if (this.mShields[num].turn > 0)
            {
                goto Label_0094;
            }
            this.mShields.RemoveAt(num--);
        Label_0094:
            num += 1;
        Label_0098:
            if (num < this.mShields.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void UpdateSkillUseCount(SkillData skill, int count)
        {
            int num;
            if (this.CheckEnableSkillUseCount(skill) != null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            num = this.GetSkillUseCount(skill);
            this.mSkillUseCount[skill] = Math.Min(Math.Max(num + count, 0), this.GetSkillUseCountMax(skill));
            return;
        }

        private unsafe void UpdateUpBuffEffect(BuffAttachment buff, EffectCheckTimings timing, bool is_count_up)
        {
            BuffEffect effect;
            if (buff == null)
            {
                goto Label_001C;
            }
            if (buff.skill == null)
            {
                goto Label_001C;
            }
            if (buff.Param != null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            if (buff.Param.mIsUpBuff != null)
            {
                goto Label_0033;
            }
            return;
        Label_0033:
            if (is_count_up == null)
            {
                goto Label_005C;
            }
            if (buff.Param.mUpTiming == timing)
            {
                goto Label_004B;
            }
            return;
        Label_004B:
            buff.UpBuffCount = OInt.op_Increment(buff.UpBuffCount);
        Label_005C:
            effect = BuffEffect.CreateBuffEffect(buff.Param, buff.skill.Rank, buff.skill.GetRankCap());
            if (effect != null)
            {
                goto Label_0085;
            }
            return;
        Label_0085:
            buff.status.Clear();
            effect.CalcBuffStatus(&buff.status, this.Element, buff.BuffType, 1, buff.IsNegativeValueIsBuff, buff.CalcType, buff.UpBuffCount);
            return;
        }

        public static string[] StrNameUnitConds
        {
            get
            {
                return mStrNameUnitConds;
            }
        }

        public static string[] StrDescUnitConds
        {
            get
            {
                return mStrDescUnitConds;
            }
        }

        public OInt AIActionIndex
        {
            get
            {
                return this.mAIActionIndex;
            }
        }

        public OInt AIActionTurnCount
        {
            get
            {
                return this.mAIActionTurnCount;
            }
        }

        public OInt AIPatrolIndex
        {
            get
            {
                return this.mAIPatrolIndex;
            }
        }

        public bool IsNPC
        {
            get
            {
                return ((this.mSettingNPC == null) == 0);
            }
        }

        public int Gems
        {
            get
            {
                return this.CurrentStatus.param.mp;
            }
            set
            {
                this.CurrentStatus.param.mp = Math.Max(Math.Min(value, this.MaximumStatus.param.mp), 0);
                return;
            }
        }

        public int WaitClock
        {
            get
            {
                return this.mWaitEntryClock;
            }
            set
            {
                this.mWaitEntryClock = value;
                return;
            }
        }

        public int WaitMoveTurn
        {
            get
            {
                return this.mMoveWaitTurn;
            }
            set
            {
                this.mMoveWaitTurn = value;
                return;
            }
        }

        public List<SkillData> JudgeHpLists
        {
            get
            {
                return this.mJudgeHpLists;
            }
        }

        public int TurnCount
        {
            get
            {
                return this.mTurnCount;
            }
            set
            {
                this.mTurnCount = value;
                return;
            }
        }

        public bool EntryUnit
        {
            get
            {
                return this.mEntryUnit;
            }
        }

        public string UniqueName
        {
            get
            {
                return this.mUniqueName;
            }
        }

        public string UnitName
        {
            get
            {
                return this.mUnitName;
            }
            set
            {
                this.mUnitName = value;
                return;
            }
        }

        public SRPG.UnitData UnitData
        {
            get
            {
                return this.mUnitData;
            }
        }

        public SRPG.UnitParam UnitParam
        {
            get
            {
                return this.mUnitData.UnitParam;
            }
        }

        public EUnitType UnitType
        {
            get
            {
                return ((this.UnitParam == null) ? 0 : this.UnitParam.type);
            }
        }

        public int Lv
        {
            get
            {
                return this.mUnitData.Lv;
            }
        }

        public JobData Job
        {
            get
            {
                return this.mUnitData.CurrentJob;
            }
        }

        public SkillData LeaderSkill
        {
            get
            {
                return this.mUnitData.LeaderSkill;
            }
        }

        public List<AbilityData> BattleAbilitys
        {
            get
            {
                if (this.mBattleAbilitys != null)
                {
                    goto Label_0011;
                }
                this.RefleshBattleAbilitysAndSkills();
            Label_0011:
                return this.mBattleAbilitys;
            }
        }

        public List<SkillData> BattleSkills
        {
            get
            {
                if (this.mBattleAbilitys != null)
                {
                    goto Label_0011;
                }
                this.RefleshBattleAbilitysAndSkills();
            Label_0011:
                return this.mBattleSkills;
            }
        }

        public List<BuffAttachment> BuffAttachments
        {
            get
            {
                return this.mBuffAttachments;
            }
        }

        public List<CondAttachment> CondAttachments
        {
            get
            {
                return this.mCondAttachments;
            }
        }

        public EquipData[] CurrentEquips
        {
            get
            {
                return this.mUnitData.CurrentEquips;
            }
        }

        public AIParam AI
        {
            get
            {
                return ((this.mAI.Count <= 0) ? null : this.mAI[this.mAITop]);
            }
        }

        public SkillData AIForceSkill
        {
            get
            {
                return this.mAIForceSkill;
            }
        }

        public BaseStatus MaximumStatus
        {
            get
            {
                return this.mMaximumStatus;
            }
        }

        public int MaximumStatusHp
        {
            get
            {
                return this.mMaximumStatusHp;
            }
        }

        public BaseStatus CurrentStatus
        {
            get
            {
                return this.mCurrentStatus;
            }
        }

        public int UnitChangedHp
        {
            get
            {
                return this.mUnitChangedHp;
            }
            set
            {
                this.mUnitChangedHp = value;
                return;
            }
        }

        public bool IsDead
        {
            get
            {
                return ((this.CurrentStatus.param.hp != null) ? 0 : (this.MaximumStatus.param.hp > 0));
            }
        }

        public bool IsEntry
        {
            get
            {
                return this.IsUnitFlag(1);
            }
        }

        public bool IsControl
        {
            get
            {
                MyPhoton photon;
                if (this.IsEnableControlCondition() != null)
                {
                    goto Label_000D;
                }
                return 0;
            Label_000D:
                if ((PunMonoSingleton<MyPhoton>.Instance.IsMultiVersus == null) && (MonoSingleton<GameManager>.Instance.AudienceMode == null))
                {
                    goto Label_002F;
                }
                return 1;
            Label_002F:
                return ((this.mSide != null) ? 0 : this.IsPartyMember);
            }
        }

        public bool IsGimmick
        {
            get
            {
                return ((this.UnitType == 0) == 0);
            }
        }

        public bool IsIntoUnit
        {
            get
            {
                return this.mUnitData.IsIntoUnit;
            }
        }

        public bool IsJump
        {
            get
            {
                return ((this.mCastSkill == null) ? 0 : (this.mCastSkill.CastType == 2));
            }
        }

        public EUnitSide Side
        {
            get
            {
                return this.mSide;
            }
            set
            {
                this.mSide = value;
                return;
            }
        }

        public int UnitFlag
        {
            get
            {
                return this.mUnitFlag;
            }
            set
            {
                this.mUnitFlag = value;
                return;
            }
        }

        public UnitDrop Drop
        {
            get
            {
                return this.mDrop;
            }
        }

        public UnitSteal Steal
        {
            get
            {
                return this.mSteal;
            }
        }

        public List<UnitShield> Shields
        {
            get
            {
                return this.mShields;
            }
        }

        public List<UnitMhmDamage> MhmDamageLists
        {
            get
            {
                return this.mMhmDamageLists;
            }
        }

        public int DisableMoveGridHeight
        {
            get
            {
                return Math.Max(this.GetMoveHeight() + 1, BattleMap.MAP_FLOOR_HEIGHT);
            }
        }

        public EElement Element
        {
            get
            {
                return this.UnitData.Element;
            }
        }

        public JobTypes JobType
        {
            get
            {
                return this.UnitData.JobType;
            }
        }

        public RoleTypes RoleType
        {
            get
            {
                return this.UnitData.RoleType;
            }
        }

        public int x
        {
            get
            {
                return &this.mGridPosition.x;
            }
            set
            {
                &this.mGridPosition.x = value;
                return;
            }
        }

        public int y
        {
            get
            {
                return &this.mGridPosition.y;
            }
            set
            {
                &this.mGridPosition.y = value;
                return;
            }
        }

        public int startX
        {
            get
            {
                return &this.mGridPositionTurnStart.x;
            }
            set
            {
                &this.mGridPositionTurnStart.x = value;
                return;
            }
        }

        public int startY
        {
            get
            {
                return &this.mGridPositionTurnStart.y;
            }
            set
            {
                &this.mGridPositionTurnStart.y = value;
                return;
            }
        }

        public EUnitDirection startDir
        {
            get
            {
                return this.mTurnStartDir;
            }
            set
            {
                this.mTurnStartDir = value;
                return;
            }
        }

        public NPCSetting SettingNPC
        {
            get
            {
                return this.mSettingNPC;
            }
        }

        public int SizeX
        {
            get
            {
                return 1;
            }
        }

        public int SizeY
        {
            get
            {
                return 1;
            }
        }

        public SRPG.EventTrigger EventTrigger
        {
            get
            {
                return this.mEventTrigger;
            }
        }

        public List<UnitEntryTrigger> EntryTriggers
        {
            get
            {
                return this.mEntryTriggers;
            }
        }

        public OBool IsEntryTriggerAndCheck
        {
            get
            {
                return this.mEntryTriggerAndCheck;
            }
        }

        public int ActionCount
        {
            get
            {
                return this.mActionCount;
            }
        }

        public int DeathCount
        {
            get
            {
                return this.mDeathCount;
            }
        }

        public int AutoJewel
        {
            get
            {
                return this.mAutoJewel;
            }
        }

        public OInt ChargeTime
        {
            get
            {
                return this.mChargeTime;
            }
            set
            {
                this.mChargeTime = value;
                return;
            }
        }

        public OInt ChargeTimeMax
        {
            get
            {
                FixParam param;
                param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
                if (param != null)
                {
                    goto Label_001D;
                }
                return 1;
            Label_001D:
                return Math.Max(SkillParam.CalcSkillEffectValue(1, this.CurrentStatus.bonus[0x12], param.ChargeTimeMax), 1);
            }
        }

        public SkillData CastSkill
        {
            get
            {
                return this.mCastSkill;
            }
        }

        public OInt CastTimeMax
        {
            get
            {
                FixParam param;
                param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
                if (param != null)
                {
                    goto Label_001D;
                }
                return 1;
            Label_001D:
                return Math.Max(SkillParam.CalcSkillEffectValue(1, this.CurrentStatus.bonus[0x13], param.ChargeTimeMax), 1);
            }
        }

        public OInt CastTime
        {
            get
            {
                return this.mCastTime;
            }
            set
            {
                this.mCastTime = value;
                return;
            }
        }

        public OInt CastIndex
        {
            get
            {
                return this.mCastIndex;
            }
        }

        public Unit UnitTarget
        {
            get
            {
                return this.mUnitTarget;
            }
        }

        public Grid GridTarget
        {
            get
            {
                return this.mGridTarget;
            }
        }

        public GridMap<bool> CastSkillGridMap
        {
            get
            {
                return this.mCastSkillGridMap;
            }
            set
            {
                this.mCastSkillGridMap = value;
                return;
            }
        }

        public Unit RageTarget
        {
            get
            {
                return this.mRageTarget;
            }
        }

        public OInt UnitIndex
        {
            get
            {
                return this.mUnitIndex;
            }
        }

        public string ParentUniqueName
        {
            get
            {
                return this.mParentUniqueName;
            }
        }

        public List<OString> NotifyUniqueNames
        {
            get
            {
                return this.mNotifyUniqueNames;
            }
        }

        public int TowerStartHP
        {
            get
            {
                return this.mTowerStartHP;
            }
            set
            {
                this.mTowerStartHP = value;
                return;
            }
        }

        public int KillCount
        {
            get
            {
                return this.mKillCount;
            }
            set
            {
                this.mKillCount = value;
                return;
            }
        }

        public bool IsBreakObj
        {
            get
            {
                return (this.UnitType == 4);
            }
        }

        public eMapBreakClashType BreakObjClashType
        {
            get
            {
                return ((this.mBreakObj == null) ? 0 : this.mBreakObj.clash_type);
            }
        }

        public eMapBreakAIType BreakObjAIType
        {
            get
            {
                return ((this.mBreakObj == null) ? 0 : this.mBreakObj.ai_type);
            }
        }

        public eMapBreakSideType BreakObjSideType
        {
            get
            {
                return ((this.mBreakObj == null) ? 0 : this.mBreakObj.side_type);
            }
        }

        public eMapBreakRayType BreakObjRayType
        {
            get
            {
                return ((this.mBreakObj == null) ? 0 : this.mBreakObj.ray_type);
            }
        }

        public bool IsBreakDispUI
        {
            get
            {
                return ((this.mBreakObj == null) ? 0 : ((this.mBreakObj.is_ui == 0) == 0));
            }
        }

        public string CreateBreakObjId
        {
            get
            {
                return this.mCreateBreakObjId;
            }
        }

        public int CreateBreakObjClock
        {
            get
            {
                return this.mCreateBreakObjClock;
            }
        }

        public int TeamId
        {
            get
            {
                return this.mTeamId;
            }
            set
            {
                this.mTeamId = value;
                return;
            }
        }

        public SRPG.FriendStates FriendStates
        {
            get
            {
                return this.mFriendStates;
            }
            set
            {
                this.mFriendStates = value;
                return;
            }
        }

        public OInt KeepHp
        {
            get
            {
                return this.mKeepHp;
            }
            set
            {
                this.mKeepHp = value;
                return;
            }
        }

        public List<AbilityChange> AbilityChangeLists
        {
            get
            {
                return this.mAbilityChangeLists;
            }
        }

        public List<AbilityData> AddedAbilitys
        {
            get
            {
                return this.mAddedAbilitys;
            }
        }

        public bool ReqRevive
        {
            [CompilerGenerated]
            get
            {
                return this.<ReqRevive>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<ReqRevive>k__BackingField = value;
                return;
            }
        }

        public bool IsNormalSize
        {
            get
            {
                if (this.SizeX != 1)
                {
                    goto Label_001A;
                }
                if (this.SizeY != 1)
                {
                    goto Label_001A;
                }
                return 1;
            Label_001A:
                return 0;
            }
        }

        public bool IsThrow
        {
            get
            {
                return this.mUnitData.IsThrow;
            }
        }

        public bool IsKnockBack
        {
            get
            {
                return this.mUnitData.IsKnockBack;
            }
        }

        [CompilerGenerated]
        private sealed class <GetEnableBetterBuffEffect>c__AnonStorey3F0
        {
            internal SkillData skill;

            public <GetEnableBetterBuffEffect>c__AnonStorey3F0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4A8(BuffAttachment p)
            {
                return (p.skill == this.skill);
            }
        }

        [CompilerGenerated]
        private sealed class <Setup>c__AnonStorey3EB
        {
            internal AbilityParam ab_param;
            internal Unit.<Setup>c__AnonStorey3ED <>f__ref$1005;
            internal Unit.<Setup>c__AnonStorey3EE <>f__ref$1006;

            public <Setup>c__AnonStorey3EB()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4A1(AbilityData p)
            {
                return (p.Param == this.ab_param);
            }

            internal bool <>m__4A3(AbilityData abil)
            {
                return (abil.AbilityID == this.<>f__ref$1005.npc.abilities[this.<>f__ref$1006.i].iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Setup>c__AnonStorey3EC
        {
            internal int j;
            internal Unit.<Setup>c__AnonStorey3EB <>f__ref$1003;

            public <Setup>c__AnonStorey3EC()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4A2(SkillData p)
            {
                return (p.SkillID == this.<>f__ref$1003.ab_param.skills[this.j].iname);
            }
        }

        [CompilerGenerated]
        private sealed class <Setup>c__AnonStorey3ED
        {
            internal NPCSetting npc;

            public <Setup>c__AnonStorey3ED()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4A6(SkillData p)
            {
                return (p.SkillID == this.npc.fskl);
            }
        }

        [CompilerGenerated]
        private sealed class <Setup>c__AnonStorey3EE
        {
            internal int i;

            public <Setup>c__AnonStorey3EE()
            {
                base..ctor();
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Setup>c__AnonStorey3EF
        {
            internal string sk_iname;

            public <Setup>c__AnonStorey3EF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__4A4(SkillData p)
            {
                return (p.SkillID == this.sk_iname);
            }

            internal bool <>m__4A5(SkillData sk)
            {
                return (sk.SkillID == this.sk_iname);
            }
        }

        public class AbilityChange
        {
            public List<Data> mDataLists;

            public AbilityChange()
            {
                this.mDataLists = new List<Data>();
                base..ctor();
                return;
            }

            public AbilityChange(AbilityParam fr_ap, AbilityParam to_ap, int turn, bool is_reset, int exp, bool is_infinite)
            {
                this.mDataLists = new List<Data>();
                base..ctor();
                this.mDataLists.Clear();
                this.mDataLists.Add(new Data(fr_ap, to_ap, turn, is_reset, exp, is_infinite));
                return;
            }

            public void Add(AbilityParam fr_ap, AbilityParam to_ap, int turn, bool is_reset, int exp, bool is_infinite)
            {
                this.mDataLists.Add(new Data(fr_ap, to_ap, turn, is_reset, exp, is_infinite));
                return;
            }

            public void Clear()
            {
                if (this.mDataLists.Count != null)
                {
                    goto Label_0011;
                }
                return;
            Label_0011:
                this.mDataLists.Clear();
                return;
            }

            public AbilityParam GetFromAp()
            {
                if (this.mDataLists.Count != null)
                {
                    goto Label_0012;
                }
                return null;
            Label_0012:
                return this.mDataLists[0].mFromAp;
            }

            public int GetLastExp()
            {
                if (this.mDataLists.Count != null)
                {
                    goto Label_0012;
                }
                return 0;
            Label_0012:
                return this.mDataLists[this.mDataLists.Count - 1].mExp;
            }

            public AbilityParam GetToAp()
            {
                if (this.mDataLists.Count != null)
                {
                    goto Label_0012;
                }
                return null;
            Label_0012:
                return this.mDataLists[this.mDataLists.Count - 1].mToAp;
            }

            public bool IsBack(AbilityParam fr_ap, AbilityParam to_ap)
            {
                Data data;
                if (this.mDataLists.Count >= 2)
                {
                    goto Label_0013;
                }
                return 0;
            Label_0013:
                data = this.mDataLists[this.mDataLists.Count - 1];
                return ((data.mFromAp != to_ap) ? 0 : (data.mToAp == fr_ap));
            }

            public bool IsCancel(AbilityParam fr_ap, AbilityParam to_ap)
            {
                if (this.mDataLists.Count != null)
                {
                    goto Label_0012;
                }
                return 0;
            Label_0012:
                return ((this.GetFromAp() != to_ap) ? 0 : (this.GetToAp() == fr_ap));
            }

            public bool IsInclude(AbilityParam ap)
            {
                bool flag;
                int num;
                Data data;
                flag = 0;
                num = 0;
                goto Label_0039;
            Label_0009:
                data = this.mDataLists[num];
                if (data.mFromAp == ap)
                {
                    goto Label_002E;
                }
                if (data.mToAp != ap)
                {
                    goto Label_0035;
                }
            Label_002E:
                flag = 1;
                goto Label_004A;
            Label_0035:
                num += 1;
            Label_0039:
                if (num < this.mDataLists.Count)
                {
                    goto Label_0009;
                }
            Label_004A:
                return flag;
            }

            public void RemoveLast()
            {
                if (this.mDataLists.Count != null)
                {
                    goto Label_0011;
                }
                return;
            Label_0011:
                this.mDataLists.RemoveAt(this.mDataLists.Count - 1);
                return;
            }

            public class Data
            {
                public AbilityParam mFromAp;
                public AbilityParam mToAp;
                public int mTurn;
                public bool mIsReset;
                public int mExp;
                public bool mIsInfinite;

                public Data(AbilityParam fr_ap, AbilityParam to_ap, int turn, bool is_reset, int exp, bool is_infinite)
                {
                    base..ctor();
                    this.mFromAp = fr_ap;
                    this.mToAp = to_ap;
                    this.mTurn = turn;
                    this.mIsReset = is_reset;
                    this.mExp = exp;
                    this.mIsInfinite = is_infinite;
                    return;
                }
            }
        }

        public class DropItem
        {
            public ItemParam itemParam;
            public ConceptCardParam conceptCardParam;
            public OInt num;
            public OBool is_secret;

            public DropItem()
            {
                base..ctor();
                return;
            }

            public static bool IsNullOrEmpty(Unit.DropItem value)
            {
                if (value != null)
                {
                    goto Label_0008;
                }
                return 1;
            Label_0008:
                if (value.num != null)
                {
                    goto Label_001A;
                }
                return 1;
            Label_001A:
                if (value.itemParam != null)
                {
                    goto Label_0032;
                }
                if (value.conceptCardParam != null)
                {
                    goto Label_0032;
                }
                return 1;
            Label_0032:
                return 0;
            }

            public bool isItem
            {
                get
                {
                    return ((this.itemParam == null) == 0);
                }
            }

            public bool isConceptCard
            {
                get
                {
                    return ((this.conceptCardParam == null) == 0);
                }
            }
        }

        private enum eAcType
        {
            UNKNOWN,
            NEW,
            CANCEL,
            BACK,
            ADD
        }

        public enum eTypeMhmDamage
        {
            HP,
            MP
        }

        public class UnitDrop
        {
            public List<Unit.DropItem> items;
            public OInt exp;
            public OInt gems;
            public OInt gold;
            public bool gained;

            public UnitDrop()
            {
                this.items = new List<Unit.DropItem>();
                base..ctor();
                return;
            }

            public void CopyTo(Unit.UnitDrop other)
            {
                int num;
                Unit.DropItem item;
                other.exp = this.exp;
                other.gems = this.gems;
                other.gold = this.gold;
                other.gained = this.gained;
                other.items.Clear();
                num = 0;
                goto Label_00B8;
            Label_0042:
                if (Unit.DropItem.IsNullOrEmpty(this.items[num]) == null)
                {
                    goto Label_005D;
                }
                goto Label_00B4;
            Label_005D:
                item = new Unit.DropItem();
                item.itemParam = this.items[num].itemParam;
                item.conceptCardParam = this.items[num].conceptCardParam;
                item.num = this.items[num].num;
                other.items.Add(item);
            Label_00B4:
                num += 1;
            Label_00B8:
                if (num < this.items.Count)
                {
                    goto Label_0042;
                }
                return;
            }

            public void Drop()
            {
                this.gained = 1;
                return;
            }

            public bool IsEnableDrop()
            {
                return ((this.gained != null) ? 0 : ((((this.exp > 0) || (this.gems > 0)) || (this.gold > 0)) ? 1 : (this.items.Count > 0)));
            }
        }

        public class UnitMhmDamage
        {
            public Unit.eTypeMhmDamage mType;
            public OInt mDamage;

            public UnitMhmDamage(Unit.eTypeMhmDamage type, int damage)
            {
                base..ctor();
                this.mType = type;
                this.mDamage = damage;
                return;
            }

            public void CopyTo(Unit.UnitMhmDamage other)
            {
                other.mType = this.mType;
                other.mDamage = this.mDamage;
                return;
            }
        }

        public class UnitShield
        {
            public ShieldTypes shieldType;
            public DamageTypes damageType;
            public OInt hp;
            public OInt hpMax;
            public OInt turn;
            public OInt turnMax;
            public OInt damage_rate;
            public OInt damage_value;
            public SkillParam skill_param;
            public OBool is_efficacy;

            public UnitShield()
            {
                this.is_efficacy = 0;
                base..ctor();
                return;
            }

            public void CopyTo(Unit.UnitShield other)
            {
                other.shieldType = this.shieldType;
                other.damageType = this.damageType;
                other.hp = this.hp;
                other.hpMax = this.hpMax;
                other.turn = this.turn;
                other.turnMax = this.turnMax;
                other.damage_rate = this.damage_rate;
                other.damage_value = this.damage_value;
                other.skill_param = this.skill_param;
                other.is_efficacy = this.is_efficacy;
                return;
            }
        }

        public class UnitSteal
        {
            public List<Unit.DropItem> items;
            public OInt gold;
            public bool is_item_steeled;
            public bool is_gold_steeled;

            public UnitSteal()
            {
                this.items = new List<Unit.DropItem>();
                base..ctor();
                return;
            }

            public void CopyTo(Unit.UnitSteal other)
            {
                int num;
                Unit.DropItem item;
                other.gold = this.gold;
                other.is_item_steeled = this.is_item_steeled;
                other.is_gold_steeled = this.is_gold_steeled;
                other.items.Clear();
                num = 0;
                goto Label_00AC;
            Label_0036:
                if (Unit.DropItem.IsNullOrEmpty(this.items[num]) == null)
                {
                    goto Label_0051;
                }
                goto Label_00A8;
            Label_0051:
                item = new Unit.DropItem();
                item.itemParam = this.items[num].itemParam;
                item.conceptCardParam = this.items[num].conceptCardParam;
                item.num = this.items[num].num;
                other.items.Add(item);
            Label_00A8:
                num += 1;
            Label_00AC:
                if (num < this.items.Count)
                {
                    goto Label_0036;
                }
                return;
            }

            public bool IsEnableGoldSteal()
            {
                return (this.is_gold_steeled == 0);
            }

            public bool IsEnableItemSteal()
            {
                return ((this.is_item_steeled != null) ? 0 : (this.items.Count > 0));
            }
        }
    }
}

