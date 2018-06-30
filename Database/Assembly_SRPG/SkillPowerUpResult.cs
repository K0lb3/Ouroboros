namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class SkillPowerUpResult : MonoBehaviour
    {
        [SerializeField]
        private SkillPowerUpResultContent contentBase;
        [SerializeField]
        private Transform contanteParent;
        [SerializeField]
        private int onePageContentsHightMax;
        private List<DispParam> dispParamList;
        private List<SkillPowerUpResultContent> contentList;

        public SkillPowerUpResult()
        {
            this.dispParamList = new List<DispParam>();
            this.contentList = new List<SkillPowerUpResultContent>();
            base..ctor();
            return;
        }

        public unsafe void ApplyContent()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            DispParam param;
            DispParam param2;
            int num8;
            int num9;
            Array array;
            int num10;
            int num11;
            ParamTypes types;
            SkillPowerUpResultContent content;
            List<SkillPowerUpResultContent>.Enumerator enumerator;
            int num12;
            SkillPowerUpResultContent content2;
            int num13;
            num = 0;
            num2 = 0;
            num3 = this.dispParamList.Count;
            num4 = 0;
            goto Label_01F4;
        Label_0017:
            num += 1;
            if (num < this.onePageContentsHightMax)
            {
                goto Label_002C;
            }
            goto Label_01FB;
        Label_002C:
            num2 += 1;
            num5 = this.dispParamList[num4].dispTypeList.Count;
            num6 = this.dispParamList[num4].dispTypeListMul.Count;
            num += num5 + num6;
            if (num <= this.onePageContentsHightMax)
            {
                goto Label_01F0;
            }
            num7 = num - this.onePageContentsHightMax;
            if (num7 <= 0)
            {
                goto Label_01FB;
            }
            param = new DispParam();
            param2 = new DispParam();
            param.sourceParam = this.dispParamList[num4].sourceParam;
            param2.sourceParam = this.dispParamList[num4].sourceParam;
            param.dispTypeList = new List<ParamTypes>();
            param.dispTypeListMul = new List<ParamTypes>();
            num8 = (num5 + num6) - num7;
            num9 = 0;
            num10 = Enum.GetValues(typeof(ParamTypes)).Length;
            num11 = 0;
            goto Label_01B8;
        Label_010B:
            if (num11 == 2)
            {
                goto Label_01B2;
            }
            types = (short) num11;
            if (this.dispParamList[num4].dispTypeList.Contains(types) == null)
            {
                goto Label_0165;
            }
            if (num9 >= num8)
            {
                goto Label_0157;
            }
            param.dispTypeList.Add(types);
            num9 += 1;
            goto Label_0165;
        Label_0157:
            param2.dispTypeList.Add(types);
        Label_0165:
            if (this.dispParamList[num4].dispTypeListMul.Contains(types) == null)
            {
                goto Label_01B2;
            }
            if (num9 >= num8)
            {
                goto Label_01A4;
            }
            param.dispTypeListMul.Add(types);
            num9 += 1;
            goto Label_01B2;
        Label_01A4:
            param2.dispTypeListMul.Add(types);
        Label_01B2:
            num11 += 1;
        Label_01B8:
            if (num11 < num10)
            {
                goto Label_010B;
            }
            this.dispParamList[num4] = param;
            this.dispParamList.Insert(num4 + 1, param2);
            num3 = this.dispParamList.Count;
            goto Label_01FB;
        Label_01F0:
            num4 += 1;
        Label_01F4:
            if (num4 < num3)
            {
                goto Label_0017;
            }
        Label_01FB:
            enumerator = this.contentList.GetEnumerator();
        Label_0208:
            try
            {
                goto Label_0222;
            Label_020D:
                content = &enumerator.Current;
                Object.Destroy(content.get_gameObject());
            Label_0222:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_020D;
                }
                goto Label_0240;
            }
            finally
            {
            Label_0233:
                ((List<SkillPowerUpResultContent>.Enumerator) enumerator).Dispose();
            }
        Label_0240:
            this.contentList.Clear();
            num12 = 0;
            goto Label_0293;
        Label_0253:
            content2 = Object.Instantiate<SkillPowerUpResultContent>(this.contentBase);
            content2.get_transform().SetParent(this.contanteParent, 0);
            content2.get_gameObject().SetActive(1);
            this.contentList.Add(content2);
            num12 += 1;
        Label_0293:
            if (num12 < num2)
            {
                goto Label_0253;
            }
            num13 = 0;
            goto Label_02F1;
        Label_02A3:
            this.contentList[num13].SetData(this.dispParamList[num13].sourceParam, this.dispParamList[num13].dispTypeList, this.dispParamList[num13].dispTypeListMul);
            num13 += 1;
        Label_02F1:
            if (num13 < num2)
            {
                goto Label_02A3;
            }
            this.dispParamList.RemoveRange(0, num2);
            return;
        }

        public void SetData(ConceptCardData currentCardData, int prevAwakeCount, int prevLevel, bool includeMaxPowerUp)
        {
            List<ConceptCardEquipEffect> list;
            int num;
            int num2;
            int num3;
            List<string> list2;
            int num4;
            SkillData data;
            SkillPowerUpResultContent.Param param;
            DispParam param2;
            SkillPowerUpResultContent.DispType type;
            IEnumerator<SkillPowerUpResultContent.DispType> enumerator;
            list = currentCardData.GetCardSkills();
            num = list.Count;
            num2 = currentCardData.LvCap;
            num3 = currentCardData.AwakeCountCap;
            list2 = new List<string>();
            num4 = 0;
            goto Label_0117;
        Label_0030:
            data = list[num4].CardSkill;
            if (data == null)
            {
                goto Label_0111;
            }
            if (list2.Contains(data.Name) != null)
            {
                goto Label_0111;
            }
            list2.Add(data.Name);
            param = new SkillPowerUpResultContent.Param(data, currentCardData, num2, num3, prevLevel, prevAwakeCount, includeMaxPowerUp);
            if (param.IsBonusParamChanged() == null)
            {
                goto Label_0111;
            }
            param2 = new DispParam();
            param2.sourceParam = param;
            enumerator = param.GetAllBonusChangeType().GetEnumerator();
        Label_00A1:
            try
            {
                goto Label_00E6;
            Label_00A6:
                type = enumerator.Current;
                if (type.isScale != null)
                {
                    goto Label_00D3;
                }
                param2.dispTypeList.Add(type.type);
                goto Label_00E6;
            Label_00D3:
                param2.dispTypeListMul.Add(type.type);
            Label_00E6:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_00A6;
                }
                goto Label_0104;
            }
            finally
            {
            Label_00F7:
                if (enumerator != null)
                {
                    goto Label_00FC;
                }
            Label_00FC:
                enumerator.Dispose();
            }
        Label_0104:
            this.dispParamList.Add(param2);
        Label_0111:
            num4 += 1;
        Label_0117:
            if (num4 < num)
            {
                goto Label_0030;
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
                return ((this.dispParamList.Count != null) ? 0 : 1);
            }
        }

        private class DispParam
        {
            public SkillPowerUpResultContent.Param sourceParam;
            public List<ParamTypes> dispTypeList;
            public List<ParamTypes> dispTypeListMul;

            public DispParam()
            {
                this.dispTypeList = new List<ParamTypes>();
                this.dispTypeListMul = new List<ParamTypes>();
                base..ctor();
                return;
            }
        }
    }
}

