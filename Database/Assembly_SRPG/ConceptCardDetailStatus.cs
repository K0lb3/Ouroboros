namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ConceptCardDetailStatus : ConceptCardDetailBase
    {
        [SerializeField]
        private GameObject mParentObject;
        [SerializeField]
        private GameObject mStatusBaseItem;
        private List<GameObject> mInstantiateItems;
        private int mAddExp;
        private int mAddAwakeLv;
        private bool mIsEnhance;

        public ConceptCardDetailStatus()
        {
            this.mInstantiateItems = new List<GameObject>();
            base..ctor();
            return;
        }

        private GameObject CreateStatusItem(int index)
        {
            GameObject obj2;
            if (index >= this.mInstantiateItems.Count)
            {
                goto Label_0030;
            }
            this.mInstantiateItems[index].SetActive(1);
            return this.mInstantiateItems[index];
        Label_0030:
            obj2 = Object.Instantiate<GameObject>(this.mStatusBaseItem);
            obj2.get_transform().SetParent(this.mParentObject.get_transform());
            this.mInstantiateItems.Add(obj2);
            obj2.SetActive(1);
            obj2.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
            return obj2;
        }

        private ConceptCardEffectsParam GetBaseEffectParam()
        {
            ConceptCardEffectsParam[] paramArray;
            ConceptCardEffectsParam param;
            ConceptCardEffectsParam param2;
            ConceptCardEffectsParam[] paramArray2;
            int num;
            paramArray = base.mConceptCardData.Param.effects;
            if (paramArray == null)
            {
                goto Label_0020;
            }
            if (0 < ((int) paramArray.Length))
            {
                goto Label_0022;
            }
        Label_0020:
            return null;
        Label_0022:
            param = null;
            paramArray2 = paramArray;
            num = 0;
            goto Label_0071;
        Label_002E:
            param2 = paramArray2[num];
            if (param2 == null)
            {
                goto Label_006B;
            }
            if (string.IsNullOrEmpty(param2.cnds_iname) == null)
            {
                goto Label_006B;
            }
            if (string.IsNullOrEmpty(param2.statusup_skill) != null)
            {
                goto Label_006B;
            }
            if (param == null)
            {
                goto Label_0069;
            }
            DebugUtility.LogError("基準パラメータが２つ以上設定されています");
        Label_0069:
            param = param2;
        Label_006B:
            num += 1;
        Label_0071:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_002E;
            }
            return param;
        }

        private unsafe void InitStatusItems()
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            enumerator = this.mInstantiateItems.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0020;
            Label_0011:
                obj2 = &enumerator.Current;
                obj2.SetActive(0);
            Label_0020:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_003D;
            }
            finally
            {
            Label_0031:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_003D:
            return;
        }

        public override void Refresh()
        {
            int num;
            GameObject obj2;
            ConceptCardDetailStatusItems items;
            ConceptCardEffectsParam[] paramArray;
            ConceptCardEffectsParam param;
            ConceptCardEffectsParam[] paramArray2;
            int num2;
            GameObject obj3;
            ConceptCardDetailStatusItems items2;
            if (base.mConceptCardData == null)
            {
                goto Label_002D;
            }
            if ((this.mParentObject == null) != null)
            {
                goto Label_002D;
            }
            if ((this.mStatusBaseItem == null) == null)
            {
                goto Label_002E;
            }
        Label_002D:
            return;
        Label_002E:
            this.InitStatusItems();
            num = 0;
            items = this.CreateStatusItem(num++).GetComponentInChildren<ConceptCardDetailStatusItems>();
            if ((items != null) == null)
            {
                goto Label_0080;
            }
            items.SetParam(base.mConceptCardData, this.GetBaseEffectParam(), this.mAddExp, this.mAddAwakeLv, this.mIsEnhance, 1);
            items.Refresh();
        Label_0080:
            paramArray = base.mConceptCardData.Param.effects;
            if (paramArray == null)
            {
                goto Label_00A0;
            }
            if (0 < ((int) paramArray.Length))
            {
                goto Label_00A1;
            }
        Label_00A0:
            return;
        Label_00A1:
            paramArray2 = paramArray;
            num2 = 0;
            goto Label_012E;
        Label_00AC:
            param = paramArray2[num2];
            if (param == null)
            {
                goto Label_0128;
            }
            if (string.IsNullOrEmpty(param.cnds_iname) != null)
            {
                goto Label_0128;
            }
            if (string.IsNullOrEmpty(param.statusup_skill) != null)
            {
                goto Label_0128;
            }
            items2 = this.CreateStatusItem(num++).GetComponentInChildren<ConceptCardDetailStatusItems>();
            if ((items2 != null) == null)
            {
                goto Label_0128;
            }
            items2.SetParam(base.mConceptCardData, param, this.mAddExp, this.mAddAwakeLv, this.mIsEnhance, 0);
            items2.Refresh();
        Label_0128:
            num2 += 1;
        Label_012E:
            if (num2 < ((int) paramArray2.Length))
            {
                goto Label_00AC;
            }
            return;
        }

        public override void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
        {
            base.mConceptCardData = card_data;
            this.mAddExp = addExp;
            this.mAddAwakeLv = addAwakeLv;
            this.mIsEnhance = ConceptCardDescription.IsEnhance;
            if ((this.mStatusBaseItem != null) == null)
            {
                goto Label_003E;
            }
            this.mStatusBaseItem.SetActive(0);
        Label_003E:
            return;
        }
    }
}

