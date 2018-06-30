namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnitLearnSkillWindow : MonoBehaviour, IFlowInterface
    {
        public List<SkillParam> Skills;
        public Transform SkillParent;
        public GameObject SkillTemplate;

        public UnitLearnSkillWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Awake()
        {
            if ((this.SkillTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.SkillTemplate.SetActive(0);
        Label_001D:
            return;
        }

        private void Refresh()
        {
            int num;
            SkillParam param;
            GameObject obj2;
            if (this.Skills != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_005B;
        Label_0013:
            param = this.Skills[num];
            if (param != null)
            {
                goto Label_002B;
            }
            goto Label_0057;
        Label_002B:
            obj2 = Object.Instantiate<GameObject>(this.SkillTemplate);
            DataSource.Bind<SkillParam>(obj2, param);
            obj2.get_transform().SetParent(this.SkillParent, 0);
            obj2.SetActive(1);
        Label_0057:
            num += 1;
        Label_005B:
            if (num < this.Skills.Count)
            {
                goto Label_0013;
            }
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }
    }
}

