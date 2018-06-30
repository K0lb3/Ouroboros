namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class BuffAttachment
    {
        public Unit user;
        public Unit CheckTarget;
        private OInt mCheckTiming;
        private OInt mUseCondition;
        public OInt turn;
        public OBool IsPassive;
        public SkillData skill;
        public SkillEffectTargets skilltarget;
        private BuffEffectParam mParam;
        public BaseStatus status;
        private OInt mBuffType;
        public bool IsNegativeValueIsBuff;
        private OInt mCalcType;
        public int DuplicateCount;
        public bool IsCalculated;
        public uint LinkageID;
        public OInt UpBuffCount;
        public List<Unit> AagTargetLists;

        public BuffAttachment()
        {
            this.status = new BaseStatus();
            this.mBuffType = 0;
            this.mCalcType = 0;
            this.UpBuffCount = 0;
            this.AagTargetLists = new List<Unit>();
            base..ctor();
            return;
        }

        public BuffAttachment(BuffEffectParam param)
        {
            this.status = new BaseStatus();
            this.mBuffType = 0;
            this.mCalcType = 0;
            this.UpBuffCount = 0;
            this.AagTargetLists = new List<Unit>();
            base..ctor();
            this.mParam = param;
            return;
        }

        public void CopyTo(BuffAttachment dsc)
        {
            dsc.user = this.user;
            dsc.turn = this.turn;
            dsc.IsPassive = this.IsPassive;
            dsc.skill = this.skill;
            dsc.skilltarget = this.skilltarget;
            dsc.mParam = this.mParam;
            dsc.BuffType = this.BuffType;
            dsc.IsNegativeValueIsBuff = this.IsNegativeValueIsBuff;
            dsc.CalcType = this.CalcType;
            dsc.CheckTarget = this.CheckTarget;
            dsc.CheckTiming = this.CheckTiming;
            dsc.UseCondition = this.UseCondition;
            dsc.DuplicateCount = this.DuplicateCount;
            dsc.LinkageID = this.LinkageID;
            dsc.UpBuffCount = this.UpBuffCount;
            this.status.CopyTo(dsc.status);
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

        public BuffEffectParam Param
        {
            get
            {
                return this.mParam;
            }
        }

        public BuffTypes BuffType
        {
            get
            {
                return this.mBuffType;
            }
            set
            {
                this.mBuffType = value;
                return;
            }
        }

        public SkillParamCalcTypes CalcType
        {
            get
            {
                return this.mCalcType;
            }
            set
            {
                this.mCalcType = value;
                return;
            }
        }

        public bool IsCalcLaterCondition
        {
            get
            {
                return ((this.UseCondition == 1) ? 1 : (this.UseCondition == 5));
            }
        }
    }
}

