namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;

    public class ConceptCardParam
    {
        public string iname;
        public string name;
        public string expr;
        public eCardType type;
        public string icon;
        public int rare;
        public int lvcap;
        public int sell;
        public int en_cost;
        public int en_exp;
        public int en_trust;
        public string trust_reward;
        public string first_get_unit;
        public bool is_override_lvcap;
        public ConceptCardEffectsParam[] effects;
        public bool not_sale;

        public ConceptCardParam()
        {
            base..ctor();
            return;
        }

        public bool Deserialize(JSON_ConceptCardParam json, MasterParam master)
        {
            RarityParam param;
            int num;
            ConceptCardEffectsParam param2;
            this.iname = json.iname;
            this.name = json.name;
            this.expr = json.expr;
            this.type = json.type;
            this.icon = json.icon;
            this.rare = json.rare;
            this.sell = json.sell;
            this.en_cost = json.en_cost;
            this.en_exp = json.en_exp;
            this.en_trust = json.en_trust;
            this.trust_reward = json.trust_reward;
            this.first_get_unit = json.first_get_unit;
            this.is_override_lvcap = 1;
            this.lvcap = json.lvcap;
            if (json.lvcap > 0)
            {
                goto Label_00E3;
            }
            this.is_override_lvcap = 0;
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.rare);
            if (param == null)
            {
                goto Label_00E3;
            }
            this.lvcap = param.ConceptCardLvCap;
        Label_00E3:
            if (json.effects == null)
            {
                goto Label_013E;
            }
            this.effects = new ConceptCardEffectsParam[(int) json.effects.Length];
            num = 0;
            goto Label_0130;
        Label_0108:
            param2 = new ConceptCardEffectsParam();
            if (param2.Deserialize(json.effects[num]) != null)
            {
                goto Label_0123;
            }
            return 0;
        Label_0123:
            this.effects[num] = param2;
            num += 1;
        Label_0130:
            if (num < ((int) json.effects.Length))
            {
                goto Label_0108;
            }
        Label_013E:
            this.not_sale = (json.not_sale != 1) ? 0 : 1;
            return 1;
        }

        public string GetLocalizedTextFlavor()
        {
            return GameUtility.GetExternalLocalizedText("ConceptCard", this.iname, "flavor");
        }

        public static void GetSkillAllStatus(string statusup_skill, int awakeLvCap, int lv, ref BaseStatus add, ref BaseStatus scale)
        {
            SkillData data;
            data = new SkillData();
            data.Setup(statusup_skill, lv, awakeLvCap, null);
            SkillData.GetPassiveBuffStatus(data, null, 0, add, scale);
            return;
        }

        public static void GetSkillStatus(string statusup_skill, int awakeLvCap, int lv, ref BaseStatus add, ref BaseStatus scale)
        {
            SkillData data;
            data = new SkillData();
            data.Setup(statusup_skill, lv, awakeLvCap, null);
            SkillData.GetHomePassiveBuffStatus(data, 0, add, scale);
            return;
        }

        public bool IsExistAddCardSkillBuffAwake()
        {
            int num;
            if (this.effects == null)
            {
                goto Label_005E;
            }
            num = 0;
            goto Label_0050;
        Label_0012:
            if (string.IsNullOrEmpty(this.effects[num].card_skill) == null)
            {
                goto Label_002E;
            }
            goto Label_004C;
        Label_002E:
            if (string.IsNullOrEmpty(this.effects[num].add_card_skill_buff_awake) == null)
            {
                goto Label_004A;
            }
            goto Label_004C;
        Label_004A:
            return 1;
        Label_004C:
            num += 1;
        Label_0050:
            if (num < ((int) this.effects.Length))
            {
                goto Label_0012;
            }
        Label_005E:
            return 0;
        }

        public bool IsExistAddCardSkillBuffLvMax()
        {
            int num;
            if (this.effects == null)
            {
                goto Label_0084;
            }
            num = 0;
            goto Label_0076;
        Label_0012:
            if (string.IsNullOrEmpty(this.effects[num].card_skill) != null)
            {
                goto Label_0042;
            }
            if (string.IsNullOrEmpty(this.effects[num].add_card_skill_buff_lvmax) != null)
            {
                goto Label_0042;
            }
            return 1;
        Label_0042:
            if (string.IsNullOrEmpty(this.effects[num].abil_iname) != null)
            {
                goto Label_0072;
            }
            if (string.IsNullOrEmpty(this.effects[num].abil_iname_lvmax) != null)
            {
                goto Label_0072;
            }
            return 1;
        Label_0072:
            num += 1;
        Label_0076:
            if (num < ((int) this.effects.Length))
            {
                goto Label_0012;
            }
        Label_0084:
            return 0;
        }

        public bool IsEnableAwake
        {
            get
            {
                return (this.is_override_lvcap == 0);
            }
        }

        public int AwakeCountCap
        {
            get
            {
                RarityParam param;
                if (this.IsEnableAwake == null)
                {
                    goto Label_0033;
                }
                param = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.rare);
                if (param == null)
                {
                    goto Label_0033;
                }
                return param.ConceptCardAwakeCountMax;
            Label_0033:
                return 0;
            }
        }

        public int AwakeLevelCap
        {
            get
            {
                RarityParam param;
                if (this.IsEnableAwake == null)
                {
                    goto Label_004D;
                }
                param = MonoSingleton<GameManager>.Instance.MasterParam.GetRarityParam(this.rare);
                if (param == null)
                {
                    goto Label_004D;
                }
                return (param.ConceptCardAwakeCountMax * MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap);
            Label_004D:
                return 0;
            }
        }
    }
}

