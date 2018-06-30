namespace SRPG
{
    using System;

    public class CondEffectParam
    {
        public string iname;
        public string job;
        public string buki;
        public string birth;
        public ESex sex;
        public EElement elem;
        public ConditionEffectTypes type;
        public ESkillCondition cond;
        public OInt value_ini;
        public OInt value_max;
        public OInt rate_ini;
        public OInt rate_max;
        public OInt turn_ini;
        public OInt turn_max;
        public EffectCheckTargets chk_target;
        public EffectCheckTimings chk_timing;
        public EUnitCondition[] conditions;
        public string[] BuffIds;
        public OInt v_poison_rate;
        public OInt v_poison_fix;
        public OInt v_paralyse_rate;
        public OInt v_blink_hit;
        public OInt v_blink_avo;
        public OInt v_death_count;
        public OInt v_berserk_atk;
        public OInt v_berserk_def;
        public OInt v_fast;
        public OInt v_slow;
        public OInt v_donmov;
        public OInt v_auto_hp_heal;
        public OInt v_auto_hp_heal_fix;
        public OInt v_auto_mp_heal;
        public OInt v_auto_mp_heal_fix;
        public OInt curse;

        public CondEffectParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_CondEffectParam json)
        {
            int num;
            int num2;
            if (json != null)
            {
                goto Label_0008;
            }
            return 0;
        Label_0008:
            this.iname = json.iname;
            this.job = json.job;
            this.buki = json.buki;
            this.birth = json.birth;
            this.sex = json.sex;
            this.elem = json.elem;
            this.cond = json.cond;
            this.type = json.type;
            this.chk_target = json.chktgt;
            this.chk_timing = json.timing;
            this.value_ini = json.vini;
            this.value_max = json.vmax;
            this.rate_ini = json.rini;
            this.rate_max = json.rmax;
            this.turn_ini = json.tini;
            this.turn_max = json.tmax;
            this.curse = json.curse;
            this.conditions = null;
            if (json.conds == null)
            {
                goto Label_015E;
            }
            this.conditions = new EUnitCondition[(int) json.conds.Length];
            num = 0;
            goto Label_0150;
        Label_0123:
            if (json.conds[num] >= 0)
            {
                goto Label_0136;
            }
            goto Label_014C;
        Label_0136:
            this.conditions[num] = 1L << (json.conds[num] & 0x3f);
        Label_014C:
            num += 1;
        Label_0150:
            if (num < ((int) json.conds.Length))
            {
                goto Label_0123;
            }
        Label_015E:
            this.BuffIds = null;
            if (json.buffs == null)
            {
                goto Label_01AC;
            }
            this.BuffIds = new string[(int) json.buffs.Length];
            num2 = 0;
            goto Label_019E;
        Label_018A:
            this.BuffIds[num2] = json.buffs[num2];
            num2 += 1;
        Label_019E:
            if (num2 < ((int) json.buffs.Length))
            {
                goto Label_018A;
            }
        Label_01AC:
            this.v_poison_rate = json.v_poi;
            this.v_poison_fix = json.v_poifix;
            this.v_paralyse_rate = json.v_par;
            this.v_blink_hit = json.v_blihit;
            this.v_blink_avo = json.v_bliavo;
            this.v_death_count = json.v_dea;
            this.v_berserk_atk = json.v_beratk;
            this.v_berserk_def = json.v_berdef;
            this.v_fast = json.v_fast;
            this.v_slow = json.v_slow;
            this.v_donmov = json.v_don;
            this.v_auto_hp_heal = json.v_ahp;
            this.v_auto_mp_heal = json.v_amp;
            this.v_auto_hp_heal_fix = json.v_ahpfix;
            this.v_auto_mp_heal_fix = json.v_ampfix;
            return 1;
        }

        public string GetLinkageBuffId(EUnitCondition cond)
        {
            int num;
            if (this.conditions == null)
            {
                goto Label_0016;
            }
            if (this.BuffIds != null)
            {
                goto Label_0018;
            }
        Label_0016:
            return null;
        Label_0018:
            num = 0;
            goto Label_004A;
        Label_001F:
            if (this.conditions[num] != cond)
            {
                goto Label_0046;
            }
            if (num >= ((int) this.BuffIds.Length))
            {
                goto Label_0044;
            }
            return this.BuffIds[num];
        Label_0044:
            return null;
        Label_0046:
            num += 1;
        Label_004A:
            if (num < ((int) this.conditions.Length))
            {
                goto Label_001F;
            }
            return null;
        }
    }
}

