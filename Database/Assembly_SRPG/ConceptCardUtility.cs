namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ConceptCardUtility
    {
        public ConceptCardUtility()
        {
            base..ctor();
            return;
        }

        public static List<ConceptCardSkillDatailData> CreateConceptCardSkillDatailData(AbilityData abilityData)
        {
            List<ConceptCardSkillDatailData> list;
            ConceptCardEquipEffect effect;
            int num;
            ConceptCardDetailAbility.ShowType type;
            SkillParam param;
            <CreateConceptCardSkillDatailData>c__AnonStorey206 storey;
            list = new List<ConceptCardSkillDatailData>();
            if (abilityData != null)
            {
                goto Label_000E;
            }
            return list;
        Label_000E:
            effect = ConceptCardEquipEffect.CreateFromAbility(abilityData);
            num = 0;
            goto Label_00E9;
        Label_001C:
            storey = new <CreateConceptCardSkillDatailData>c__AnonStorey206();
            storey.learning_skill = abilityData.LearningSkills[num];
            if (storey.learning_skill != null)
            {
                goto Label_0043;
            }
            goto Label_00E5;
        Label_0043:
            type = 2;
            storey.data = abilityData.Skills.Find(new Predicate<SkillData>(storey.<>m__EC));
            if (storey.data != null)
            {
                goto Label_00B1;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(storey.learning_skill.iname);
            storey.data = new SkillData();
            storey.data.Setup(param.iname, 1, 1, null);
            type = 3;
        Label_00B1:
            if (list.FindIndex(new Predicate<ConceptCardSkillDatailData>(storey.<>m__ED)) > -1)
            {
                goto Label_00E5;
            }
            list.Add(new ConceptCardSkillDatailData(effect, storey.data, type, storey.learning_skill));
        Label_00E5:
            num += 1;
        Label_00E9:
            if (num < ((int) abilityData.LearningSkills.Length))
            {
                goto Label_001C;
            }
            return list;
        }

        public static ConceptCardSkillDatailData CreateConceptCardSkillDatailData(SkillData groupSkill)
        {
            ConceptCardSkillDatailData data;
            ConceptCardEquipEffect effect;
            data = null;
            if (groupSkill != null)
            {
                goto Label_000A;
            }
            return data;
        Label_000A:
            data = new ConceptCardSkillDatailData(ConceptCardEquipEffect.CreateFromGroupSkill(groupSkill), groupSkill, 1, null);
            return data;
        }

        public static unsafe void GetExpParameter(int rarity, int exp, int current_lvcap, out int lv, out int nextExp, out int expTbl)
        {
            int num;
            if ((MonoSingleton<GameManager>.Instance == null) != null)
            {
                goto Label_001F;
            }
            if (MonoSingleton<GameManager>.Instance.MasterParam != null)
            {
                goto Label_002B;
            }
        Label_001F:
            *((int*) lv) = 1;
            *((int*) expTbl) = 1;
            *((int*) nextExp) = 0;
            return;
        Label_002B:
            *((int*) lv) = MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel(rarity, exp, current_lvcap);
            num = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp(rarity, *((int*) lv));
            if (*(((int*) lv)) >= current_lvcap)
            {
                goto Label_0080;
            }
            *((int*) expTbl) = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardNextExp(rarity, *(((int*) lv)) + 1);
            *((int*) nextExp) = *(((int*) expTbl)) - (exp - num);
            goto Label_0088;
        Label_0080:
            *((int*) expTbl) = 1;
            *((int*) nextExp) = 0;
        Label_0088:
            return;
        }

        public static bool IsEnableCardSkillForUnit(Unit target, SkillData card_skill)
        {
            BuffEffectParam param;
            BuffEffect effect;
            if (target == null)
            {
                goto Label_000C;
            }
            if (card_skill != null)
            {
                goto Label_000E;
            }
        Label_000C:
            return 0;
        Label_000E:
            if (card_skill.SkillParam.condition == 4)
            {
                goto Label_0021;
            }
            return 0;
        Label_0021:
            param = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetBuffEffectParam(card_skill.SkillParam.target_buff_iname);
            if (param != null)
            {
                goto Label_0044;
            }
            return 0;
        Label_0044:
            return BuffEffect.CreateBuffEffect(param, card_skill.Rank, card_skill.GetRankCap()).CheckEnableBuffTarget(target);
        }

        public static bool IsGetUnitConceptCard(string iname)
        {
            ConceptCardParam param;
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
            if (param != null)
            {
                goto Label_0026;
            }
            return 0;
        Label_0026:
            if (string.IsNullOrEmpty(param.first_get_unit) == null)
            {
                goto Label_0038;
            }
            return 0;
        Label_0038:
            return 1;
        }

        [CompilerGenerated]
        private sealed class <CreateConceptCardSkillDatailData>c__AnonStorey206
        {
            internal LearningSkill learning_skill;
            internal SkillData data;

            public <CreateConceptCardSkillDatailData>c__AnonStorey206()
            {
                base..ctor();
                return;
            }

            internal bool <>m__EC(SkillData x)
            {
                return (x.SkillParam.iname == this.learning_skill.iname);
            }

            internal bool <>m__ED(ConceptCardSkillDatailData abi)
            {
                return (abi.skill_data.SkillParam.iname == this.data.SkillParam.iname);
            }
        }
    }
}

