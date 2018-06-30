namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class AbilityPowerUpResult : MonoBehaviour
    {
        [SerializeField]
        private AbilityPowerUpResultContent contentBase;
        [SerializeField]
        private Transform contanteParent;
        [SerializeField]
        private int onePageContentsMax;
        private List<AbilityPowerUpResultContent.Param> paramList;
        private List<AbilityPowerUpResultContent> contentList;

        public AbilityPowerUpResult()
        {
            this.paramList = new List<AbilityPowerUpResultContent.Param>();
            this.contentList = new List<AbilityPowerUpResultContent>();
            base..ctor();
            return;
        }

        public void ApplyContent()
        {
            int num;
            int num2;
            int num3;
            AbilityPowerUpResultContent content;
            int num4;
            int num5;
            int num6;
            num = this.paramList.Count;
            num2 = (num >= this.onePageContentsMax) ? this.onePageContentsMax : num;
            if (this.contentList.Count != null)
            {
                goto Label_0076;
            }
            num3 = 0;
            goto Label_006A;
        Label_003C:
            content = Object.Instantiate<AbilityPowerUpResultContent>(this.contentBase);
            content.get_transform().SetParent(this.contanteParent, 0);
            this.contentList.Add(content);
            num3 += 1;
        Label_006A:
            if (num3 < num2)
            {
                goto Label_003C;
            }
            goto Label_00AD;
        Label_0076:
            if (num2 <= num)
            {
                goto Label_00AD;
            }
            num4 = num - 1;
            goto Label_00A5;
        Label_0087:
            this.contentList[num4].get_gameObject().SetActive(0);
            num4 += 1;
        Label_00A5:
            if (num4 < num2)
            {
                goto Label_0087;
            }
        Label_00AD:
            num5 = 0;
            goto Label_00DA;
        Label_00B5:
            this.contentList[num5].SetData(this.paramList[num5]);
            num5 += 1;
        Label_00DA:
            if (num5 < num2)
            {
                goto Label_00B5;
            }
            num6 = 0;
            goto Label_00FC;
        Label_00EA:
            this.paramList.RemoveAt(0);
            num6 += 1;
        Label_00FC:
            if (num6 < num2)
            {
                goto Label_00EA;
            }
            return;
        }

        public void SetData(ConceptCardData currentCardData, int prevAwakeCount)
        {
            List<ConceptCardEquipEffect> list;
            int num;
            int num2;
            AbilityData data;
            AbilityPowerUpResultContent.Param param;
            list = currentCardData.GetAbilities();
            num = list.Count;
            num2 = 0;
            goto Label_0047;
        Label_0015:
            data = list[num2].Ability;
            param = new AbilityPowerUpResultContent.Param();
            param.data = data.Param;
            this.paramList.Add(param);
            num2 += 1;
        Label_0047:
            if (num2 < num)
            {
                goto Label_0015;
            }
            return;
        }

        private void Start()
        {
            if ((this.contentBase != null) == null)
            {
                goto Label_0037;
            }
            if (this.contentBase.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0037;
            }
            this.contentBase.get_gameObject().SetActive(0);
        Label_0037:
            return;
        }

        public bool IsEnd
        {
            get
            {
                return ((this.paramList.Count != null) ? 0 : 1);
            }
        }
    }
}

