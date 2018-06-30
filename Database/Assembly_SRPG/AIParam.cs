namespace SRPG
{
    using System;

    public class AIParam
    {
        public string iname;
        public RoleTypes role;
        public ParamTypes param;
        public ParamPriorities param_prio;
        public OLong flags;
        public OInt escape_border;
        public OInt heal_border;
        public OInt gems_border;
        public OInt buff_border;
        public OInt cond_border;
        public OInt safe_border;
        public OInt gosa_border;
        public OInt DisableSupportActionHpBorder;
        public OInt DisableSupportActionMemberBorder;
        public SkillCategory[] SkillCategoryPriorities;
        public ParamTypes[] BuffPriorities;
        public EUnitCondition[] ConditionPriorities;

        public AIParam()
        {
            this.flags = 0L;
            this.escape_border = 0;
            this.heal_border = 0;
            this.gems_border = 0;
            this.buff_border = 0;
            this.cond_border = 0;
            this.safe_border = 0;
            this.gosa_border = 0;
            base..ctor();
            return;
        }

        public bool CheckFlag(AIFlags flag)
        {
            return (((this.flags & ((long) flag)) == 0L) == 0);
        }

        public bool Deserialize(JSON_AIParam json)
        {
            int num;
            int num2;
            int num3;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.role = json.role;
            this.param = (short) json.prm;
            this.param_prio = json.prmprio;
            if (json.best == null)
            {
                goto Label_005D;
            }
            this.flags |= 1L;
        Label_005D:
            if (json.sneak == null)
            {
                goto Label_0081;
            }
            this.flags |= 2L;
        Label_0081:
            if (json.notmov == null)
            {
                goto Label_00A5;
            }
            this.flags |= 4L;
        Label_00A5:
            if (json.notact == null)
            {
                goto Label_00C9;
            }
            this.flags |= 8L;
        Label_00C9:
            if (json.notskl == null)
            {
                goto Label_00EE;
            }
            this.flags |= 0x10L;
        Label_00EE:
            if (json.notavo == null)
            {
                goto Label_0113;
            }
            this.flags |= 0x20L;
        Label_0113:
            if (json.csff == null)
            {
                goto Label_0138;
            }
            this.flags |= 0x40L;
        Label_0138:
            if (json.notmpd == null)
            {
                goto Label_0160;
            }
            this.flags |= 0x80L;
        Label_0160:
            if (json.buff_self == null)
            {
                goto Label_0188;
            }
            this.flags |= 0x100L;
        Label_0188:
            if (json.notprio == null)
            {
                goto Label_01B0;
            }
            this.flags |= 0x200L;
        Label_01B0:
            if (json.use_old_sort == null)
            {
                goto Label_01D8;
            }
            this.flags |= 0x400L;
        Label_01D8:
            this.escape_border = json.sos;
            this.heal_border = json.heal;
            this.gems_border = json.gems;
            this.buff_border = json.buff_border;
            this.cond_border = json.cond_border;
            this.safe_border = json.safe_border;
            this.gosa_border = json.gosa_border;
            this.DisableSupportActionHpBorder = json.notsup_hp;
            this.DisableSupportActionMemberBorder = json.notsup_num;
            this.SkillCategoryPriorities = null;
            this.BuffPriorities = null;
            this.ConditionPriorities = null;
            if (json.skil_prio == null)
            {
                goto Label_02CD;
            }
            this.SkillCategoryPriorities = new SkillCategory[(int) json.skil_prio.Length];
            num = 0;
            goto Label_02BF;
        Label_02AB:
            this.SkillCategoryPriorities[num] = json.skil_prio[num];
            num += 1;
        Label_02BF:
            if (num < ((int) json.skil_prio.Length))
            {
                goto Label_02AB;
            }
        Label_02CD:
            if (json.buff_prio == null)
            {
                goto Label_0315;
            }
            this.BuffPriorities = new ParamTypes[(int) json.buff_prio.Length];
            num2 = 0;
            goto Label_0307;
        Label_02F2:
            this.BuffPriorities[num2] = (short) json.buff_prio[num2];
            num2 += 1;
        Label_0307:
            if (num2 < ((int) json.buff_prio.Length))
            {
                goto Label_02F2;
            }
        Label_0315:
            if (json.cond_prio == null)
            {
                goto Label_0362;
            }
            this.ConditionPriorities = new EUnitCondition[(int) json.cond_prio.Length];
            num3 = 0;
            goto Label_0354;
        Label_033A:
            this.ConditionPriorities[num3] = 1L << (json.cond_prio[num3] & 0x3f);
            num3 += 1;
        Label_0354:
            if (num3 < ((int) json.cond_prio.Length))
            {
                goto Label_033A;
            }
        Label_0362:
            return 1;
        }
    }
}

