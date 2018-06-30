namespace SRPG
{
    using GR;
    using System;

    public class ConceptCardEffectsParam
    {
        public string cnds_iname;
        public string card_skill;
        public string add_card_skill_buff_awake;
        public string add_card_skill_buff_lvmax;
        public string abil_iname;
        public string abil_iname_lvmax;
        public string statusup_skill;
        public string skin;

        public ConceptCardEffectsParam()
        {
            base..ctor();
            return;
        }

        public BuffEffect CreateAddCardSkillBuffEffectAwake(int awake, int awake_cap)
        {
            BuffEffectParam param;
            if (string.IsNullOrEmpty(this.add_card_skill_buff_awake) != null)
            {
                goto Label_0017;
            }
            if (awake > 0)
            {
                goto Label_0019;
            }
        Label_0017:
            return null;
        Label_0019:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(this.add_card_skill_buff_awake);
            if (param != null)
            {
                goto Label_0037;
            }
            return null;
        Label_0037:
            return BuffEffect.CreateBuffEffect(param, awake, awake_cap);
        }

        public BuffEffect CreateAddCardSkillBuffEffectLvMax(int lv, int lv_cap, int awake)
        {
            BuffEffectParam param;
            if (lv >= lv_cap)
            {
                goto Label_0009;
            }
            return null;
        Label_0009:
            if (string.IsNullOrEmpty(this.add_card_skill_buff_lvmax) != null)
            {
                goto Label_0020;
            }
            if (awake > 0)
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(this.add_card_skill_buff_lvmax);
            if (param != null)
            {
                goto Label_0040;
            }
            return null;
        Label_0040:
            return BuffEffect.CreateBuffEffect(param, 1, 1);
        }

        public bool Deserialize(JSON_ConceptCardEquipParam json)
        {
            this.cnds_iname = json.cnds_iname;
            this.card_skill = json.card_skill;
            this.add_card_skill_buff_awake = json.add_card_skill_buff_awake;
            this.add_card_skill_buff_lvmax = json.add_card_skill_buff_lvmax;
            this.abil_iname = json.abil_iname;
            this.abil_iname_lvmax = json.abil_iname_lvmax;
            this.statusup_skill = json.statusup_skill;
            this.skin = json.skin;
            return 1;
        }

        public void GetAddCardSkillBuffStatusAwake(int awake, int awake_cap, ref BaseStatus total_add, ref BaseStatus total_scale)
        {
            BuffEffect effect;
            *(total_add).Clear();
            *(total_scale).Clear();
            effect = this.CreateAddCardSkillBuffEffectAwake(awake, awake_cap);
            if (effect != null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            effect.GetBaseStatus(total_add, total_scale);
            return;
        }

        public void GetAddCardSkillBuffStatusLvMax(int lv, int lv_cap, int awake, ref BaseStatus total_add, ref BaseStatus total_scale)
        {
            BuffEffect effect;
            *(total_add).Clear();
            *(total_scale).Clear();
            effect = this.CreateAddCardSkillBuffEffectLvMax(lv, lv_cap, awake);
            if (effect != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            effect.GetBaseStatus(total_add, total_scale);
            return;
        }
    }
}

