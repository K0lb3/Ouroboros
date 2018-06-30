namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(10, "データ設定完了", 1, 10)]
    public class ConceptCardDetailAbility : ConceptCardDetailBase, IFlowInterface
    {
        private const int PIN_ABILITY_DETAIL = 0;
        private const int PIN_ABILITY_DETAIL_END = 10;
        [SerializeField]
        private GameObject mCardAbilityObject;
        [SerializeField]
        private GameObject mCardSkillObject;
        [SerializeField]
        private GameObject mCardAbilityNameObject;
        [SerializeField]
        private GameObject mCardSkillNameObject;
        [SerializeField]
        private GameObject mCardUniqueUnitObject;
        [SerializeField]
        private GameObject mCardUniqueJobObject;
        [SerializeField]
        private GameObject mCardCommonUnitObject;
        [SerializeField]
        private GameObject mCardCommonJobObject;
        [SerializeField]
        private Text mCardUseUnitText;
        [SerializeField]
        private Text mCardUseJobText;
        [SerializeField]
        private Image_Transparent mCardUseUnitImage;
        [SerializeField]
        private RawImage_Transparent mCardUseJobImage;
        [SerializeField]
        private Text mCardAbilityDescription;
        [SerializeField]
        private StatusList mCardSkillStatusList;
        [SerializeField]
        private GameObject mLock;
        private ConceptCardSkillDatailData mShowData;
        private ClickDetail mClickDetail;

        public ConceptCardDetailAbility()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        public void OnClickDetail()
        {
            this.mClickDetail(this.mShowData);
            return;
        }

        public void SetAbilityDetailParent(ClickDetail detail)
        {
            this.mClickDetail = detail;
            return;
        }

        public unsafe void SetCardSkill(ConceptCardSkillDatailData data)
        {
            AbilityData data2;
            StringBuilder builder;
            BaseStatus status;
            BaseStatus status2;
            BaseStatus status3;
            BaseStatus status4;
            BaseStatus status5;
            BaseStatus status6;
            BaseStatus status7;
            BaseStatus status8;
            int num;
            int num2;
            int num3;
            int num4;
            BaseStatus status9;
            BaseStatus status10;
            BaseStatus status11;
            BaseStatus status12;
            BaseStatus status13;
            BaseStatus status14;
            BaseStatus status15;
            BaseStatus status16;
            base.SwitchObject(1, this.mCardSkillObject, this.mCardAbilityObject);
            base.SwitchObject(data.type == 1, this.mCardSkillNameObject, this.mCardAbilityNameObject);
            if (data.skill_data != null)
            {
                goto Label_004B;
            }
            base.SetText(this.mCardAbilityDescription, string.Empty);
            return;
        Label_004B:
            data2 = ((data.effect == null) || (data.effect.Ability == null)) ? null : data.effect.Ability;
            DataSource.Bind<AbilityData>(base.get_gameObject(), data2);
            DataSource.Bind<SkillData>(base.get_gameObject(), data.skill_data);
            builder = new StringBuilder();
            builder.Append(data.skill_data.SkillParam.expr);
            base.SetText(this.mCardAbilityDescription, builder.ToString());
            if ((this.mCardSkillStatusList != null) == null)
            {
                goto Label_00E6;
            }
            this.mCardSkillStatusList.get_gameObject().SetActive(0);
        Label_00E6:
            if (data.skill_data.Condition != 4)
            {
                goto Label_0307;
            }
            if ((this.mCardSkillStatusList != null) == null)
            {
                goto Label_0307;
            }
            if (data.skill_data == null)
            {
                goto Label_0307;
            }
            this.mCardSkillStatusList.get_gameObject().SetActive(1);
            status = new BaseStatus();
            status2 = new BaseStatus();
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            SkillData.GetPassiveBuffStatus(data.skill_data, null, 0, &status, &status2);
            if (data.effect == null)
            {
                goto Label_019E;
            }
            if (data.effect.AddCardSkillBuffEffectAwake == null)
            {
                goto Label_019E;
            }
            status5 = new BaseStatus();
            status6 = new BaseStatus();
            data.effect.AddCardSkillBuffEffectAwake.GetBaseStatus(&status5, &status6);
            status3.Add(status5);
            status4.Add(status6);
        Label_019E:
            if (data.effect == null)
            {
                goto Label_01ED;
            }
            if (data.effect.AddCardSkillBuffEffectLvMax == null)
            {
                goto Label_01ED;
            }
            status7 = new BaseStatus();
            status8 = new BaseStatus();
            data.effect.AddCardSkillBuffEffectLvMax.GetBaseStatus(&status7, &status8);
            status3.Add(status7);
            status4.Add(status8);
        Label_01ED:
            if (ConceptCardDescription.EnhanceInfo != null)
            {
                goto Label_0213;
            }
            this.mCardSkillStatusList.SetValues_TotalAndBonus(status, status2, status, status2, status3, status4, status3, status4);
            goto Label_0307;
        Label_0213:
            num = ConceptCardDescription.EnhanceInfo.Target.LvCap;
            num2 = ConceptCardDescription.EnhanceInfo.predictLv;
            num3 = ConceptCardDescription.EnhanceInfo.predictAwake;
            num4 = ConceptCardDescription.EnhanceInfo.Target.AwakeCountCap;
            status9 = new BaseStatus();
            status10 = new BaseStatus();
            ConceptCardParam.GetSkillAllStatus(data.skill_data.SkillID, num, num2, &status9, &status10);
            status11 = new BaseStatus();
            status12 = new BaseStatus();
            data.effect.GetAddCardSkillBuffStatusAwake(num3, num4, &status11, &status12);
            status13 = new BaseStatus();
            status14 = new BaseStatus();
            data.effect.GetAddCardSkillBuffStatusLvMax(num2, num, num3, &status13, &status14);
            status15 = new BaseStatus();
            status16 = new BaseStatus();
            status15.Add(status11);
            status15.Add(status13);
            status16.Add(status12);
            status16.Add(status14);
            this.mCardSkillStatusList.SetValues_TotalAndBonus(status, status2, status9, status10, status3, status4, status15, status16);
        Label_0307:
            if ((this.mLock != null) == null)
            {
                goto Label_032C;
            }
            this.mLock.SetActive(data.type == 3);
        Label_032C:
            this.mShowData = data;
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void SetUnitIcon(Image_Transparent image, UnitParam unit_param)
        {
            SpriteSheet sheet;
            ItemParam param;
            if ((image == null) != null)
            {
                goto Label_0012;
            }
            if (unit_param != null)
            {
                goto Label_0013;
            }
        Label_0012:
            return;
        Label_0013:
            sheet = AssetManager.Load<SpriteSheet>("ItemIcon/small");
            param = MonoSingleton<GameManager>.Instance.GetItemParam(unit_param.piece);
            image.set_sprite(sheet.GetSprite(param.icon));
            return;
        }

        public delegate void ClickDetail(ConceptCardSkillDatailData data);

        public enum ShowType
        {
            None,
            Skill,
            Ability,
            LockSkill
        }
    }
}

