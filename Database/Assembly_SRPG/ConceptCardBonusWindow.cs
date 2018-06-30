namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ConceptCardBonusWindow : MonoBehaviour
    {
        [SerializeField]
        private Transform mAwakeBonusParent;
        [SerializeField]
        private Transform mLvMaxBonusParent;
        [SerializeField]
        private ConceptCardBonusContentAwake mAwakeBonusTemplate;
        [SerializeField]
        private ConceptCardBonusContentLvMax mLvMaxBonusSkillTemplate;
        [SerializeField]
        private ConceptCardBonusContentLvMax mLvMaxBonusAbilityTemplate;

        public ConceptCardBonusWindow()
        {
            base..ctor();
            return;
        }

        public void Initailize(ConceptCardParam param, int current_awake_count, bool is_level_max)
        {
            List<ConceptCardBonusContentAwake> list;
            int num;
            bool flag;
            ConceptCardBonusContentAwake awake;
            int num2;
            int num3;
            bool flag2;
            bool flag3;
            bool flag4;
            ConceptCardBonusContentLvMax max;
            bool flag5;
            ConceptCardBonusContentLvMax max2;
            list = new List<ConceptCardBonusContentAwake>();
            if ((this.mAwakeBonusTemplate != null) == null)
            {
                goto Label_00ED;
            }
            if ((this.mAwakeBonusParent != null) == null)
            {
                goto Label_00ED;
            }
            num = 1;
            goto Label_0074;
        Label_002F:
            flag = (current_awake_count < num) == 0;
            awake = Object.Instantiate<ConceptCardBonusContentAwake>(this.mAwakeBonusTemplate);
            awake.get_transform().SetParent(this.mAwakeBonusParent, 0);
            awake.Setup(param.effects, num, param.AwakeCountCap, flag);
            list.Add(awake);
            num += 1;
        Label_0074:
            if (num <= param.AwakeCountCap)
            {
                goto Label_002F;
            }
            num2 = 0;
            goto Label_00CF;
        Label_0088:
            num3 = num2 + 1;
            flag2 = 0;
            flag3 = 1;
            if (list.Count <= num3)
            {
                goto Label_00B5;
            }
            flag2 = list[num3].IsEnable;
            goto Label_00B8;
        Label_00B5:
            flag3 = 0;
        Label_00B8:
            list[num2].SetProgressLineImage(flag2, flag3);
            num2 += 1;
        Label_00CF:
            if (num2 < list.Count)
            {
                goto Label_0088;
            }
            this.mAwakeBonusTemplate.get_gameObject().SetActive(0);
        Label_00ED:
            if ((this.mLvMaxBonusSkillTemplate != null) == null)
            {
                goto Label_0165;
            }
            if ((this.mLvMaxBonusParent != null) == null)
            {
                goto Label_0165;
            }
            flag4 = is_level_max;
            max = Object.Instantiate<ConceptCardBonusContentLvMax>(this.mLvMaxBonusSkillTemplate);
            max.get_transform().SetParent(this.mLvMaxBonusParent, 0);
            max.Setup(param.effects, param.lvcap, param.lvcap, param.AwakeCountCap, flag4, 0);
            this.mLvMaxBonusSkillTemplate.get_gameObject().SetActive(0);
        Label_0165:
            if ((this.mLvMaxBonusAbilityTemplate != null) == null)
            {
                goto Label_01DD;
            }
            if ((this.mLvMaxBonusParent != null) == null)
            {
                goto Label_01DD;
            }
            flag5 = is_level_max;
            max2 = Object.Instantiate<ConceptCardBonusContentLvMax>(this.mLvMaxBonusAbilityTemplate);
            max2.get_transform().SetParent(this.mLvMaxBonusParent, 0);
            max2.Setup(param.effects, param.lvcap, param.lvcap, param.AwakeCountCap, flag5, 1);
            this.mLvMaxBonusAbilityTemplate.get_gameObject().SetActive(0);
        Label_01DD:
            return;
        }

        public enum eViewType
        {
            CARD_SKILL,
            ABILITY
        }
    }
}

