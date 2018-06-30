namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class ConceptCardEquipEffect
    {
        private string mConditionsIname;
        private string mSkin;
        private ConceptCardEffectsParam mEffectParam;
        private SkillData mCardSkill;
        private SkillData mEquipSkill;
        private AbilityData mAbilityDefault;
        private AbilityData mAbilityLvMax;
        private BuffEffect mAddCardSkillBuffEffectAwake;
        private BuffEffect mAddCardSkillBuffEffectLvMax;
        private bool is_levelmax;

        public ConceptCardEquipEffect()
        {
            base..ctor();
            return;
        }

        public BuffEffect CreateAddCardSkillBuffEffectAwake(int awake, int awake_cap)
        {
            if (this.mEffectParam != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            return this.mEffectParam.CreateAddCardSkillBuffEffectAwake(awake, awake_cap);
        }

        public BuffEffect CreateAddCardSkillBuffEffectLvMax(int lv, int lv_cap, int awake)
        {
            if (this.mEffectParam != null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            return this.mEffectParam.CreateAddCardSkillBuffEffectLvMax(lv, lv_cap, awake);
        }

        public static ConceptCardEquipEffect CreateFromAbility(AbilityData abilityData)
        {
            ConceptCardEquipEffect effect;
            effect = new ConceptCardEquipEffect();
            effect.mAbilityDefault = abilityData;
            return effect;
        }

        public static ConceptCardEquipEffect CreateFromGroupSkill(SkillData skillData)
        {
            ConceptCardEquipEffect effect;
            effect = new ConceptCardEquipEffect();
            effect.mCardSkill = skillData;
            return effect;
        }

        public void GetAddCardSkillBuffStatusAwake(int awake, int awake_cap, ref BaseStatus total_add, ref BaseStatus total_scale)
        {
            if (this.mEffectParam != null)
            {
                goto Label_001B;
            }
            *(total_add).Clear();
            *(total_scale).Clear();
            return;
        Label_001B:
            this.mEffectParam.GetAddCardSkillBuffStatusAwake(awake, awake_cap, total_add, total_scale);
            return;
        }

        public void GetAddCardSkillBuffStatusLvMax(int lv, int lv_cap, int awake, ref BaseStatus total_add, ref BaseStatus total_scale)
        {
            if (this.mEffectParam != null)
            {
                goto Label_001C;
            }
            *(total_add).Clear();
            *(total_scale).Clear();
            return;
        Label_001C:
            this.mEffectParam.GetAddCardSkillBuffStatusLvMax(lv, lv_cap, awake, total_add, total_scale);
            return;
        }

        public unsafe string GetBufText(BuffEffect effect, BuffEffect.BuffTarget target)
        {
            object[] objArray4;
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            MasterParam param;
            StringBuilder builder;
            ConceptCardConditionsParam param2;
            UnitGroupParam param3;
            string str;
            bool flag;
            int num;
            string str2;
            SkillParamCalcTypes types;
            param = MonoSingleton<GameManager>.Instance.MasterParam;
            builder = new StringBuilder();
            param2 = param.GetConceptCardConditions(this.ConditionsIname);
            builder.Append(param2.GetConditionDescriptionEquip());
            param3 = param.GetUnitGroup(effect.param.un_group);
            if (param3 == null)
            {
                goto Label_0050;
            }
            builder.Append(param3.GetName());
        Label_0050:
            builder.Append(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_OF"));
            str = LocalizedText.Get("sys." + ((ParamTypes) target.paramType).ToString());
            builder.Append(str);
            flag = (0 > target.value) ? 0 : 1;
            num = Mathf.Abs(target.value);
            str2 = string.Empty;
            types = target.calcType;
            if (types == null)
            {
                goto Label_00DA;
            }
            if (types == 1)
            {
                goto Label_0123;
            }
            goto Label_016C;
        Label_00DA:
            if (flag == null)
            {
                goto Label_0102;
            }
            objArray1 = new object[] { &num.ToString() };
            str2 = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_CALC_ADD_PLUS", objArray1);
            goto Label_011E;
        Label_0102:
            objArray2 = new object[] { &num.ToString() };
            str2 = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_CALC_ADD_MINUS", objArray2);
        Label_011E:
            goto Label_016C;
        Label_0123:
            if (flag == null)
            {
                goto Label_014B;
            }
            objArray3 = new object[] { &num.ToString() };
            str2 = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_CALC_UP", objArray3);
            goto Label_0167;
        Label_014B:
            objArray4 = new object[] { &num.ToString() };
            str2 = LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_CALC_DOWN", objArray4);
        Label_0167:;
        Label_016C:
            builder.Append(str2);
            return builder.ToString();
        }

        public ConceptCardConditionsParam GetCondition()
        {
            return MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardConditions(this.ConditionsIname);
        }

        public JobParam[] GetConditionJob()
        {
            ConceptCardConditionsParam param;
            JobGroupParam param2;
            List<JobParam> list;
            int num;
            param = this.GetCondition();
            if (param != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            param2 = param.GetJobGroupParam();
            if (param2 == null)
            {
                goto Label_0027;
            }
            if (param2.jobs != null)
            {
                goto Label_0029;
            }
        Label_0027:
            return null;
        Label_0029:
            list = new List<JobParam>();
            num = 0;
            goto Label_0057;
        Label_0036:
            list.Add(MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(param2.jobs[num]));
            num += 1;
        Label_0057:
            if (num < ((int) param2.jobs.Length))
            {
                goto Label_0036;
            }
            return list.ToArray();
        }

        public UnitParam[] GetConditionUnit()
        {
            ConceptCardConditionsParam param;
            UnitGroupParam param2;
            List<UnitParam> list;
            int num;
            param = this.GetCondition();
            if (param != null)
            {
                goto Label_000F;
            }
            return null;
        Label_000F:
            param2 = param.GetUnitGroupParam();
            if (param2 == null)
            {
                goto Label_0027;
            }
            if (param2.units != null)
            {
                goto Label_0029;
            }
        Label_0027:
            return null;
        Label_0029:
            list = new List<UnitParam>();
            num = 0;
            goto Label_0057;
        Label_0036:
            list.Add(MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(param2.units[num]));
            num += 1;
        Label_0057:
            if (num < ((int) param2.units.Length))
            {
                goto Label_0036;
            }
            return list.ToArray();
        }

        public unsafe void GetStatus(ref BaseStatus fixed_status, ref BaseStatus scale_status)
        {
            BaseStatus status;
            int num;
            status = new BaseStatus();
            *(fixed_status).Clear();
            *(scale_status).Clear();
            SkillData.GetHomePassiveBuffStatus(this.mEquipSkill, 0, fixed_status, &status);
            *(scale_status).Add(status);
            if (this.Ability == null)
            {
                goto Label_008F;
            }
            if (this.Ability.Skills == null)
            {
                goto Label_008F;
            }
            num = 0;
            goto Label_0079;
        Label_004D:
            status.Clear();
            SkillData.GetHomePassiveBuffStatus(this.Ability.Skills[num], 0, fixed_status, &status);
            *(scale_status).Add(status);
            num += 1;
        Label_0079:
            if (num < this.Ability.Skills.Count)
            {
                goto Label_004D;
            }
        Label_008F:
            return;
        }

        public void Setup(ConceptCardEffectsParam param, int lv, int lvcap, int awake_count, int awake_count_cap)
        {
            BuffEffectParam param2;
            BuffEffectParam param3;
            AbilityParam param4;
            AbilityParam param5;
            this.mEffectParam = param;
            this.mConditionsIname = param.cnds_iname;
            this.is_levelmax = (lv < lvcap) == 0;
            if (string.IsNullOrEmpty(param.card_skill) != null)
            {
                goto Label_004F;
            }
            this.mCardSkill = new SkillData();
            this.mCardSkill.Setup(param.card_skill, lv, lvcap, null);
        Label_004F:
            if (string.IsNullOrEmpty(param.add_card_skill_buff_awake) != null)
            {
                goto Label_0093;
            }
            if (awake_count <= 0)
            {
                goto Label_0093;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(param.add_card_skill_buff_awake);
            if (param2 == null)
            {
                goto Label_0093;
            }
            this.mAddCardSkillBuffEffectAwake = BuffEffect.CreateBuffEffect(param2, awake_count, awake_count_cap);
        Label_0093:
            if (string.IsNullOrEmpty(param.add_card_skill_buff_lvmax) != null)
            {
                goto Label_00DC;
            }
            if (lv < lvcap)
            {
                goto Label_00DC;
            }
            if (awake_count <= 0)
            {
                goto Label_00DC;
            }
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetBuffEffectParam(param.add_card_skill_buff_lvmax);
            if (param3 == null)
            {
                goto Label_00DC;
            }
            this.mAddCardSkillBuffEffectLvMax = BuffEffect.CreateBuffEffect(param3, 1, 1);
        Label_00DC:
            if (string.IsNullOrEmpty(param.statusup_skill) != null)
            {
                goto Label_010B;
            }
            this.mEquipSkill = new SkillData();
            this.mEquipSkill.Setup(param.statusup_skill, lv, lvcap, null);
        Label_010B:
            if (string.IsNullOrEmpty(param.skin) != null)
            {
                goto Label_0127;
            }
            this.mSkin = param.skin;
        Label_0127:
            if (string.IsNullOrEmpty(param.abil_iname) != null)
            {
                goto Label_0189;
            }
            if (MonoSingleton<GameManager>.Instance.GetAbilityParam(param.abil_iname) == null)
            {
                goto Label_0189;
            }
            this.mAbilityDefault = new AbilityData();
            this.mAbilityDefault.Setup(null, 0L, param.abil_iname, lv - 1, lvcap);
            this.mAbilityDefault.IsNoneCategory = 1;
            this.mAbilityDefault.IsHideList = 0;
        Label_0189:
            if (string.IsNullOrEmpty(param.abil_iname) != null)
            {
                goto Label_01FB;
            }
            if (string.IsNullOrEmpty(param.abil_iname_lvmax) != null)
            {
                goto Label_01FB;
            }
            if (MonoSingleton<GameManager>.Instance.GetAbilityParam(param.abil_iname_lvmax) == null)
            {
                goto Label_01FB;
            }
            this.mAbilityLvMax = new AbilityData();
            this.mAbilityLvMax.Setup(null, 0L, param.abil_iname_lvmax, lv - 1, lvcap);
            this.mAbilityLvMax.IsNoneCategory = 1;
            this.mAbilityLvMax.IsHideList = 0;
        Label_01FB:
            return;
        }

        public string ConditionsIname
        {
            get
            {
                return this.mConditionsIname;
            }
        }

        public string Skin
        {
            get
            {
                return this.mSkin;
            }
        }

        public SkillData CardSkill
        {
            get
            {
                return this.mCardSkill;
            }
        }

        public SkillData EquipSkill
        {
            get
            {
                return this.mEquipSkill;
            }
        }

        public AbilityData Ability
        {
            get
            {
                return (((this.is_levelmax == null) || (this.mAbilityLvMax == null)) ? this.mAbilityDefault : this.mAbilityLvMax);
            }
        }

        public BuffEffect AddCardSkillBuffEffectAwake
        {
            get
            {
                return this.mAddCardSkillBuffEffectAwake;
            }
        }

        public BuffEffect AddCardSkillBuffEffectLvMax
        {
            get
            {
                return this.mAddCardSkillBuffEffectLvMax;
            }
        }

        public bool IsExistAbilityLvMax
        {
            get
            {
                return (((this.mEffectParam == null) || (string.IsNullOrEmpty(this.mEffectParam.abil_iname) != null)) ? 0 : (string.IsNullOrEmpty(this.mEffectParam.abil_iname_lvmax) == 0));
            }
        }
    }
}

