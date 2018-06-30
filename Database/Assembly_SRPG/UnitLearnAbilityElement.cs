namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnitLearnAbilityElement : MonoBehaviour, IFlowInterface
    {
        public Transform SkillParent;
        public GameObject SkillTemplate;
        private List<GameObject> mSkills;

        public UnitLearnAbilityElement()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        public void Refresh()
        {
            AbilityData data;
            int num;
            GameObject obj2;
            SkillParam param;
            data = DataSource.FindDataOfClass<AbilityData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_00BE;
            }
            this.mSkills = new List<GameObject>((int) data.LearningSkills.Length);
            num = 0;
            goto Label_00B0;
        Label_002D:
            if (data.LearningSkills[num] != null)
            {
                goto Label_003F;
            }
            goto Label_00AC;
        Label_003F:
            if (data.Rank >= data.LearningSkills[num].locklv)
            {
                goto Label_005C;
            }
            goto Label_00AC;
        Label_005C:
            obj2 = Object.Instantiate<GameObject>(this.SkillTemplate);
            param = MonoSingleton<GameManager>.Instance.GetSkillParam(data.LearningSkills[num].iname);
            DataSource.Bind<SkillParam>(obj2, param);
            obj2.get_transform().SetParent(this.SkillParent, 0);
            obj2.SetActive(1);
            this.mSkills.Add(obj2);
        Label_00AC:
            num += 1;
        Label_00B0:
            if (num < ((int) data.LearningSkills.Length))
            {
                goto Label_002D;
            }
        Label_00BE:
            base.get_gameObject().SetActive(1);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void Start()
        {
            if ((this.SkillTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.SkillTemplate.SetActive(0);
        Label_001D:
            return;
        }
    }
}

