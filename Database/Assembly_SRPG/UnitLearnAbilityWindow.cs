namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnitLearnAbilityWindow : MonoBehaviour, IFlowInterface
    {
        public List<AbilityData> AbilityList;
        public Transform LearnAbilityParent;
        public GameObject LearnAbilityTemplate;
        public GameObject LearnAbilitySkillTemplate;
        public GameObject[] LearningSkills;

        public UnitLearnAbilityWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            this.Refresh();
            return;
        }

        private void Awake()
        {
            if ((this.LearnAbilityTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.LearnAbilityTemplate.SetActive(0);
        Label_001D:
            if ((this.LearnAbilitySkillTemplate != null) == null)
            {
                goto Label_003A;
            }
            this.LearnAbilitySkillTemplate.SetActive(0);
        Label_003A:
            return;
        }

        private void Refresh()
        {
            int num;
            AbilityData data;
            GameObject obj2;
            UnitLearnAbilityElement element;
            if (this.AbilityList == null)
            {
                goto Label_00A5;
            }
            num = 0;
            goto Label_0094;
        Label_0012:
            data = this.AbilityList[num];
            obj2 = null;
            if (data.LearningSkills == null)
            {
                goto Label_003A;
            }
            if (((int) data.LearningSkills.Length) != 1)
            {
                goto Label_004B;
            }
        Label_003A:
            obj2 = Object.Instantiate<GameObject>(this.LearnAbilityTemplate);
            goto Label_0057;
        Label_004B:
            obj2 = Object.Instantiate<GameObject>(this.LearnAbilitySkillTemplate);
        Label_0057:
            DataSource.Bind<AbilityData>(obj2, data);
            element = obj2.GetComponent<UnitLearnAbilityElement>();
            if ((element != null) == null)
            {
                goto Label_0077;
            }
            element.Refresh();
        Label_0077:
            obj2.get_transform().SetParent(this.LearnAbilityParent, 0);
            obj2.SetActive(1);
            num += 1;
        Label_0094:
            if (num < this.AbilityList.Count)
            {
                goto Label_0012;
            }
        Label_00A5:
            base.set_enabled(1);
            return;
        }

        private void Start()
        {
            if (this.AbilityList != null)
            {
                goto Label_001C;
            }
            this.AbilityList = GlobalVars.LearningAbilities;
            GlobalVars.LearningAbilities = null;
        Label_001C:
            this.Refresh();
            return;
        }
    }
}

