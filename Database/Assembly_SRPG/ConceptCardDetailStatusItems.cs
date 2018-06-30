namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardDetailStatusItems : MonoBehaviour
    {
        [SerializeField]
        private Text mGroupNameField;
        [SerializeField]
        private StatusList mStatusList;
        private ConceptCardData mConceptCardData;
        private ConceptCardEffectsParam mConceptCardEffectsParam;
        private int mAddExp;
        private int mAddAwakeLv;
        private bool mIsEnhance;
        private bool mIsBaseParam;

        public ConceptCardDetailStatusItems()
        {
            base..ctor();
            return;
        }

        public bool IsValueEmpty(BaseStatus paramAdd, BaseStatus paramMul)
        {
            Array array;
            int num;
            int num2;
            array = Enum.GetValues(typeof(ParamTypes));
            num = 0;
            goto Label_0069;
        Label_0017:
            if (paramAdd[(short) array.GetValue(num)] == null)
            {
                goto Label_003E;
            }
            if (num == 2)
            {
                goto Label_003E;
            }
            return 0;
        Label_003E:
            if (paramMul[(short) array.GetValue(num)] == null)
            {
                goto Label_0065;
            }
            if (num == 2)
            {
                goto Label_0065;
            }
            return 0;
        Label_0065:
            num += 1;
        Label_0069:
            if (num < array.Length)
            {
                goto Label_0017;
            }
            return 1;
        }

        public void Refresh()
        {
            bool flag;
            if (this.mConceptCardData != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.RefreshEquipTarget(this.mConceptCardEffectsParam);
            if (this.RefreshEquipParam(this.mConceptCardData, this.mConceptCardEffectsParam) == null)
            {
                goto Label_0048;
            }
            if (this.mIsBaseParam != null)
            {
                goto Label_0048;
            }
            base.get_gameObject().SetActive(0);
        Label_0048:
            return;
        }

        private unsafe bool RefreshEquipParam(ConceptCardData conceptCardData, ConceptCardEffectsParam equipParam)
        {
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            int num;
            int num2;
            int num3;
            if ((this.mStatusList == null) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            if (equipParam != null)
            {
                goto Label_0031;
            }
            this.mStatusList.SetValues(new BaseStatus(), new BaseStatus(), 0);
            return 0;
        Label_0031:
            status = new BaseStatus();
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            ConceptCardParam.GetSkillStatus(equipParam.statusup_skill, conceptCardData.LvCap, conceptCardData.Lv, &status, &status3);
            if (this.mIsEnhance == null)
            {
                goto Label_0103;
            }
            num = Mathf.Min(conceptCardData.LvCap, conceptCardData.CurrentLvCap + this.mAddAwakeLv);
            num2 = conceptCardData.Exp + this.mAddExp;
            num3 = MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel(conceptCardData.Rarity, num2, num);
            ConceptCardParam.GetSkillStatus(equipParam.statusup_skill, conceptCardData.LvCap, num3, &status2, &status4);
            this.mStatusList.SetValuesAfterOnly(status, status3, status2, status4, 0, 1);
            goto Label_0111;
        Label_0103:
            this.mStatusList.SetValues(status, status3, 0);
        Label_0111:
            return this.IsValueEmpty(status, status3);
        }

        private void RefreshEquipTarget(ConceptCardEffectsParam equipParam)
        {
            SkillParam param;
            string str;
            string str2;
            if ((this.mGroupNameField == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mGroupNameField.set_text(string.Empty);
            if (this.mIsBaseParam == null)
            {
                goto Label_0047;
            }
            this.mGroupNameField.set_text(LocalizedText.Get("sys.CONCEPT_CARD_STATUS_DEFAULT_TITLE"));
            goto Label_009D;
        Label_0047:
            if (equipParam == null)
            {
                goto Label_009D;
            }
            if (string.IsNullOrEmpty(equipParam.statusup_skill) != null)
            {
                goto Label_009D;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(equipParam.statusup_skill);
            if (param == null)
            {
                goto Label_009D;
            }
            str = param.expr;
            str2 = LocalizedText.Get("sys.CONCEPT_CARD_STATUS_ADDPARAM_TITLE");
            this.mGroupNameField.set_text(str + str2);
        Label_009D:
            return;
        }

        public void SetParam(ConceptCardData conceptCardData, ConceptCardEffectsParam conceptCardEffectsParam, int addExp, int addAwakeLv, bool isEnhance, bool isBaseParam)
        {
            this.mConceptCardData = conceptCardData;
            this.mConceptCardEffectsParam = conceptCardEffectsParam;
            this.mAddExp = addExp;
            this.mAddAwakeLv = addAwakeLv;
            this.mIsEnhance = isEnhance;
            this.mIsBaseParam = isBaseParam;
            return;
        }
    }
}

