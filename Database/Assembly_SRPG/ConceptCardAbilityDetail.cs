namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardAbilityDetail : MonoBehaviour
    {
        [SerializeField]
        private Text mAbilityName;
        [SerializeField]
        private Text mDescriptionText;

        public ConceptCardAbilityDetail()
        {
            base..ctor();
            return;
        }

        public void SetAbilityData(ConceptCardEquipEffect effect)
        {
            AbilityData data;
            data = effect.Ability;
            if (data != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            this.SetText(this.mAbilityName, data.Param.name);
            this.SetText(this.mDescriptionText, data.Param.expr);
            return;
        }

        public void SetData(ConceptCardSkillDatailData data)
        {
            ConceptCardDetailAbility.ShowType type;
            switch ((data.type - 1))
            {
                case 0:
                    goto Label_0020;

                case 1:
                    goto Label_002C;

                case 2:
                    goto Label_0038;
            }
            goto Label_0044;
        Label_0020:
            this.SetGroup(data);
            goto Label_0044;
        Label_002C:
            this.SetGroup(data);
            goto Label_0044;
        Label_0038:
            this.SetGroup(data);
        Label_0044:
            return;
        }

        public void SetGroup(ConceptCardSkillDatailData data)
        {
            object[] objArray1;
            if (data.skill_data != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.SetText(this.mAbilityName, data.skill_data.Name);
            if (data.type != 3)
            {
                goto Label_006A;
            }
            if (data.learning_skill == null)
            {
                goto Label_0069;
            }
            objArray1 = new object[] { (int) data.learning_skill.locklv };
            this.SetText(this.mDescriptionText, LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_LV", objArray1));
        Label_0069:
            return;
        Label_006A:
            this.SetText(this.mDescriptionText, GameUtility.GetExternalLocalizedText("skill", data.skill_data.SkillParam.iname, "CONCEPT_TXT"));
            return;
        }

        public void SetSkillData(ConceptCardEquipEffect effect)
        {
            object[] objArray1;
            SkillData data;
            StringBuilder builder;
            List<BuffEffect.BuffTarget> list;
            int num;
            BuffEffect.BuffTarget target;
            UnitGroupParam param;
            data = effect.CardSkill;
            if (data != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            this.SetText(this.mAbilityName, data.Name);
            builder = new StringBuilder();
            list = data.mTargetBuffEffect.targets;
            num = 0;
            goto Label_005B;
        Label_0039:
            target = list[num];
            builder.Append(effect.GetBufText(data.mTargetBuffEffect, target));
            num += 1;
        Label_005B:
            if (num < list.Count)
            {
                goto Label_0039;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitGroup(data.mTargetBuffEffect.param.un_group);
            if (param == null)
            {
                goto Label_00DC;
            }
            if (string.IsNullOrEmpty(param.name) != null)
            {
                goto Label_00DC;
            }
            builder.Append(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_NEW_LINE"));
            objArray1 = new object[] { param.name, param.GetGroupUnitAllNameText() };
            builder.Append(LocalizedText.Get("sys.CONCEPT_CARD_SKILL_DESCRIPTION_GROUP", objArray1));
        Label_00DC:
            this.SetText(this.mDescriptionText, builder.ToString());
            return;
        }

        public void SetText(Text text, string str)
        {
            if ((text != null) == null)
            {
                goto Label_0013;
            }
            text.set_text(str);
        Label_0013:
            return;
        }

        private void Start()
        {
            ConceptCardSkillDatailData data;
            data = DataSource.FindDataOfClass<ConceptCardSkillDatailData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_001A;
            }
            this.SetData(data);
        Label_001A:
            return;
        }
    }
}

