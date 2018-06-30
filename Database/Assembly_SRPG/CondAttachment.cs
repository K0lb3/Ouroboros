namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CondAttachment
    {
        public Unit user;
        public Unit CheckTarget;
        private OInt mCheckTiming;
        private OInt mUseCondition;
        public OInt turn;
        public OBool IsPassive;
        public SkillData skill;
        public SkillEffectTargets skilltarget;
        public string CondId;
        private CondEffectParam mParam;
        private OInt mCondType;
        private OLong mCondition;
        public BuffEffect LinkageBuff;
        public uint LinkageID;
        [CompilerGenerated]
        private bool <IsCurse>k__BackingField;

        public CondAttachment()
        {
            base..ctor();
            return;
        }

        public CondAttachment(CondEffectParam param)
        {
            base..ctor();
            this.mParam = param;
            return;
        }

        public bool ContainsCondition(EUnitCondition condition)
        {
            return (((this.mCondition & condition) == 0L) == 0);
        }

        public void CopyTo(CondAttachment dsc)
        {
            dsc.user = this.user;
            dsc.skill = this.skill;
            dsc.skilltarget = this.skilltarget;
            dsc.CondId = this.CondId;
            dsc.mParam = this.mParam;
            dsc.turn = this.turn;
            dsc.IsPassive = this.IsPassive;
            dsc.CondType = this.CondType;
            dsc.Condition = this.Condition;
            dsc.CheckTarget = this.CheckTarget;
            dsc.CheckTiming = this.CheckTiming;
            dsc.UseCondition = this.UseCondition;
            return;
        }

        public bool IsFailCondition()
        {
            ConditionEffectTypes types;
            types = this.CondType;
            if (types == 2)
            {
                goto Label_001C;
            }
            if (types == 3)
            {
                goto Label_001C;
            }
            if (types != 4)
            {
                goto Label_001E;
            }
        Label_001C:
            return 1;
        Label_001E:
            return 0;
        }

        public bool IsSame(CondAttachment dsc, bool is_ignore_timing)
        {
            if (dsc == null)
            {
                goto Label_00A3;
            }
            if (this.skill == null)
            {
                goto Label_00A3;
            }
            if (dsc.skill == null)
            {
                goto Label_00A3;
            }
            if ((this.skill.SkillID == dsc.skill.SkillID) == null)
            {
                goto Label_00A3;
            }
            if (this.IsPassive != dsc.IsPassive)
            {
                goto Label_00A3;
            }
            if (this.CondType != dsc.CondType)
            {
                goto Label_00A3;
            }
            if (this.Condition != dsc.Condition)
            {
                goto Label_00A3;
            }
            if (is_ignore_timing != null)
            {
                goto Label_0090;
            }
            if (this.CheckTiming != dsc.CheckTiming)
            {
                goto Label_00A3;
            }
        Label_0090:
            if (this.UseCondition != dsc.UseCondition)
            {
                goto Label_00A3;
            }
            return 1;
        Label_00A3:
            return 0;
        }

        public void SetupLinkageBuff()
        {
            string str;
            BuffEffectParam param;
            this.LinkageBuff = null;
            if (this.skill == null)
            {
                goto Label_001D;
            }
            if (this.mParam != null)
            {
                goto Label_001E;
            }
        Label_001D:
            return;
        Label_001E:
            str = this.mParam.GetLinkageBuffId(this.Condition);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_003C;
            }
            return;
        Label_003C:
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetBuffEffectParam(str);
            this.LinkageBuff = BuffEffect.CreateBuffEffect(param, this.skill.Rank, this.skill.GetRankCap());
            return;
        }

        public EffectCheckTimings CheckTiming
        {
            get
            {
                return this.mCheckTiming;
            }
            set
            {
                this.mCheckTiming = value;
                return;
            }
        }

        public ESkillCondition UseCondition
        {
            get
            {
                return this.mUseCondition;
            }
            set
            {
                this.mUseCondition = value;
                return;
            }
        }

        public CondEffectParam Param
        {
            get
            {
                return this.mParam;
            }
        }

        public ConditionEffectTypes CondType
        {
            get
            {
                return this.mCondType;
            }
            set
            {
                this.mCondType = value;
                return;
            }
        }

        public EUnitCondition Condition
        {
            get
            {
                return this.mCondition;
            }
            set
            {
                this.mCondition = value;
                return;
            }
        }

        public bool IsCurse
        {
            [CompilerGenerated]
            get
            {
                return this.<IsCurse>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsCurse>k__BackingField = value;
                return;
            }
        }
    }
}

