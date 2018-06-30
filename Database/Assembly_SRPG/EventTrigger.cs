namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;

    public class EventTrigger
    {
        private EEventTrigger mTrigger;
        private EEventType mEventType;
        private EEventGimmick mGimmickType;
        private string mStrValue;
        private int mIntValue;
        private int mCount;
        private string mTag;

        public EventTrigger()
        {
            base..ctor();
            return;
        }

        public void CopyTo(EventTrigger dsc)
        {
            dsc.mTrigger = this.mTrigger;
            dsc.mEventType = this.mEventType;
            dsc.mGimmickType = this.mGimmickType;
            dsc.mStrValue = this.mStrValue;
            dsc.mIntValue = this.mIntValue;
            dsc.mCount = this.mCount;
            dsc.mTag = this.mTag;
            return;
        }

        public bool Deserialize(JSON_EventTrigger json)
        {
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.mTrigger = json.trg;
            this.mEventType = json.type;
            this.mGimmickType = json.detail;
            this.mStrValue = json.sval;
            this.mIntValue = json.ival;
            this.mCount = json.cnt;
            this.mTag = json.tag;
            return 1;
        }

        public unsafe void ExecuteGimmickEffect(Unit gimmick, Unit target, LogMapEvent log)
        {
            int num;
            int num2;
            BuffAttachment attachment;
            EEventGimmick gimmick2;
            switch ((this.GimmickType - 1))
            {
                case 0:
                    goto Label_003C;

                case 1:
                    goto Label_00B0;

                case 2:
                    goto Label_00B0;

                case 3:
                    goto Label_00B0;

                case 4:
                    goto Label_00B0;

                case 5:
                    goto Label_00B0;

                case 6:
                    goto Label_00B0;

                case 7:
                    goto Label_00B0;

                case 8:
                    goto Label_00B0;

                case 9:
                    goto Label_00B0;
            }
            goto Label_00E4;
        Label_003C:
            num = 0;
            if (target.IsUnitCondition(0x1000000L) != null)
            {
                goto Label_0097;
            }
            num2 = target.MaximumStatus.param.hp;
            num = (num2 * this.IntValue) / 100;
            num = Math.Min(target.CalcParamRecover(num), num2 - target.CurrentStatus.param.hp);
        Label_0097:
            target.Heal(num);
            if (log == null)
            {
                goto Label_00E4;
            }
            log.heal = num;
            goto Label_00E4;
        Label_00B0:
            attachment = this.MakeBuff(gimmick, target);
            target.SetBuffAttachment(attachment, 0);
            if (log == null)
            {
                goto Label_00E4;
            }
            BattleCore.SetBuffBits(attachment.status, &log.buff, &log.debuff);
        Label_00E4:
            return;
        }

        public BuffAttachment MakeBuff(Unit gimmick, Unit target)
        {
            BuffAttachment attachment;
            FixParam param;
            int num;
            int num2;
            SkillParamCalcTypes types;
            EEventGimmick gimmick2;
            attachment = new BuffAttachment();
            switch ((this.GimmickType - 2))
            {
                case 0:
                    goto Label_0040;

                case 1:
                    goto Label_0040;

                case 2:
                    goto Label_0040;

                case 3:
                    goto Label_0040;

                case 4:
                    goto Label_0040;

                case 5:
                    goto Label_0040;

                case 6:
                    goto Label_0040;

                case 7:
                    goto Label_0040;

                case 8:
                    goto Label_0040;
            }
            goto Label_01EA;
        Label_0040:
            param = MonoSingleton<GameManager>.Instance.MasterParam.FixParam;
            num = param.GemsBuffValue;
            num2 = param.GemsBuffTurn;
            types = 1;
            if (this.GimmickType != 2)
            {
                goto Label_008D;
            }
            attachment.status.param.atk = num;
        Label_008D:
            if (this.GimmickType != 3)
            {
                goto Label_00AF;
            }
            attachment.status.param.def = num;
        Label_00AF:
            if (this.GimmickType != 4)
            {
                goto Label_00D1;
            }
            attachment.status.param.mag = num;
        Label_00D1:
            if (this.GimmickType != 5)
            {
                goto Label_00F3;
            }
            attachment.status.param.mnd = num;
        Label_00F3:
            if (this.GimmickType != 6)
            {
                goto Label_0115;
            }
            attachment.status.param.rec = num;
        Label_0115:
            if (this.GimmickType != 7)
            {
                goto Label_0137;
            }
            attachment.status.param.spd = num;
        Label_0137:
            if (this.GimmickType != 8)
            {
                goto Label_0159;
            }
            attachment.status.param.cri = num;
        Label_0159:
            if (this.GimmickType != 9)
            {
                goto Label_017C;
            }
            attachment.status.param.luk = num;
        Label_017C:
            if (this.GimmickType != 10)
            {
                goto Label_01A2;
            }
            attachment.status.param.mov = 2;
            types = 0;
        Label_01A2:
            attachment.user = gimmick;
            attachment.BuffType = 0;
            attachment.CalcType = types;
            attachment.CheckTarget = target;
            attachment.CheckTiming = 0;
            attachment.UseCondition = 0;
            attachment.IsPassive = 0;
            attachment.turn = num2;
        Label_01EA:
            return attachment;
        }

        public void Setup(EventTrigger src)
        {
            this.mTrigger = src.Trigger;
            this.mEventType = src.EventType;
            this.mGimmickType = src.GimmickType;
            this.mStrValue = src.StrValue;
            this.mIntValue = src.IntValue;
            this.mCount = src.Count;
            this.mTag = src.mTag;
            return;
        }

        public EEventTrigger Trigger
        {
            get
            {
                return this.mTrigger;
            }
        }

        public EEventType EventType
        {
            get
            {
                return this.mEventType;
            }
        }

        public EEventGimmick GimmickType
        {
            get
            {
                return this.mGimmickType;
            }
        }

        public string StrValue
        {
            get
            {
                return this.mStrValue;
            }
        }

        public int IntValue
        {
            get
            {
                return this.mIntValue;
            }
        }

        public int Count
        {
            get
            {
                return this.mCount;
            }
            set
            {
                this.mCount = value;
                return;
            }
        }

        public string Tag
        {
            get
            {
                return this.mTag;
            }
        }

        public bool IsTriggerWithdraw
        {
            get
            {
                EEventTrigger trigger;
                switch ((this.mTrigger - 5))
                {
                    case 0:
                        goto Label_002C;

                    case 1:
                        goto Label_002C;

                    case 2:
                        goto Label_002C;

                    case 3:
                        goto Label_002C;

                    case 4:
                        goto Label_002C;

                    case 5:
                        goto Label_002C;
                }
                goto Label_002E;
            Label_002C:
                return 1;
            Label_002E:
                return 0;
            }
        }
    }
}

