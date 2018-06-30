namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ConceptCardBonusContentLvMax : MonoBehaviour
    {
        [SerializeField]
        private GameObject mParamTemplate;
        [SerializeField]
        private ImageArray mAwakeIconImageArray;
        [SerializeField]
        private ImageArray mAwakeIconBgArray;
        [SerializeField]
        private StatusList mParamStatusList;
        [SerializeField]
        private StatusList mBonusStatusList;
        private int mCreatedCount;

        public ConceptCardBonusContentLvMax()
        {
            base..ctor();
            return;
        }

        private void CreateAbilityBonus(ConceptCardEffectsParam[] effect_params, int lv, int lv_cap, int awake_count_cap, bool is_enable)
        {
            Transform transform;
            List<string> list;
            int num;
            AbilityParam param;
            int num2;
            SkillParam param2;
            GameObject obj2;
            if ((this.mParamTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            transform = this.mParamTemplate.get_transform().get_parent();
            list = new List<string>();
            num = 0;
            goto Label_0150;
        Label_0030:
            if (string.IsNullOrEmpty(effect_params[num].abil_iname) != null)
            {
                goto Label_014C;
            }
            if (string.IsNullOrEmpty(effect_params[num].abil_iname_lvmax) == null)
            {
                goto Label_0059;
            }
            goto Label_014C;
        Label_0059:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(effect_params[num].abil_iname_lvmax);
            if (list.Contains(param.iname) == null)
            {
                goto Label_0087;
            }
            goto Label_014C;
        Label_0087:
            num2 = 0;
            goto Label_0131;
        Label_008F:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(param.skills[num2].iname);
            if (param2 != null)
            {
                goto Label_00BA;
            }
            goto Label_012B;
        Label_00BA:
            obj2 = Object.Instantiate<GameObject>(this.mParamTemplate);
            obj2.get_transform().SetParent(transform, 0);
            if ((this.mAwakeIconImageArray != null) == null)
            {
                goto Label_0100;
            }
            this.mAwakeIconImageArray.ImageIndex = ((int) this.mAwakeIconImageArray.Images.Length) - 1;
        Label_0100:
            DataSource.Bind<SkillParam>(obj2, param2);
            DataSource.Bind<bool>(base.get_gameObject(), is_enable);
            GameParameter.UpdateAll(obj2);
            this.mCreatedCount += 1;
        Label_012B:
            num2 += 1;
        Label_0131:
            if (num2 < ((int) param.skills.Length))
            {
                goto Label_008F;
            }
            list.Add(param.iname);
        Label_014C:
            num += 1;
        Label_0150:
            if (num < ((int) effect_params.Length))
            {
                goto Label_0030;
            }
            if ((this.mAwakeIconBgArray != null) == null)
            {
                goto Label_0183;
            }
            this.mAwakeIconBgArray.ImageIndex = (is_enable == null) ? 1 : 0;
        Label_0183:
            this.mParamTemplate.SetActive(0);
            return;
        }

        private unsafe void CreateCardSkillBonus(ConceptCardEffectsParam[] effect_params, int lv, int lv_cap, int awake_count_cap, bool is_enable)
        {
            Transform transform;
            List<string> list;
            int num;
            SkillParam param;
            BaseStatus status;
            BaseStatus status2;
            SkillData data;
            BaseStatus status3;
            BaseStatus status4;
            string str;
            string str2;
            GameObject obj2;
            StatusList[] listArray;
            int num2;
            if ((this.mParamTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            transform = this.mParamTemplate.get_transform().get_parent();
            list = new List<string>();
            num = 0;
            goto Label_0235;
        Label_0030:
            if (string.IsNullOrEmpty(effect_params[num].card_skill) == null)
            {
                goto Label_0047;
            }
            goto Label_0231;
        Label_0047:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(effect_params[num].card_skill);
            if (param != null)
            {
                goto Label_006A;
            }
            goto Label_0231;
        Label_006A:
            if (string.IsNullOrEmpty(effect_params[num].add_card_skill_buff_lvmax) == null)
            {
                goto Label_0081;
            }
            goto Label_0231;
        Label_0081:
            if (list.Contains(param.iname) == null)
            {
                goto Label_0097;
            }
            goto Label_0231;
        Label_0097:
            status = new BaseStatus();
            status2 = new BaseStatus();
            data = new SkillData();
            data.Setup(param.iname, lv, lv_cap, null);
            SkillData.GetPassiveBuffStatus(data, null, 0, &status, &status2);
            status3 = new BaseStatus();
            status4 = new BaseStatus();
            effect_params[num].GetAddCardSkillBuffStatusLvMax(lv, lv_cap, awake_count_cap, &status3, &status4);
            str = ((this.mParamStatusList != null) == null) ? string.Empty : this.mParamStatusList.get_name();
            str2 = ((this.mBonusStatusList != null) == null) ? string.Empty : this.mBonusStatusList.get_name();
            obj2 = Object.Instantiate<GameObject>(this.mParamTemplate);
            obj2.get_transform().SetParent(transform, 0);
            listArray = obj2.GetComponentsInChildren<StatusList>();
            num2 = 0;
            goto Label_01C5;
        Label_0163:
            if (listArray[num2].get_name().StartsWith(str) == null)
            {
                goto Label_0191;
            }
            listArray[num2].SetValues_Restrict(status, status2, status3, status4, 0);
            goto Label_01BF;
        Label_0191:
            if (listArray[num2].get_name().StartsWith(str2) == null)
            {
                goto Label_01BF;
            }
            listArray[num2].SetValues_Restrict(status, status2, status3, status4, 1);
        Label_01BF:
            num2 += 1;
        Label_01C5:
            if (num2 < ((int) listArray.Length))
            {
                goto Label_0163;
            }
            if ((this.mAwakeIconImageArray != null) == null)
            {
                goto Label_01FB;
            }
            this.mAwakeIconImageArray.ImageIndex = ((int) this.mAwakeIconImageArray.Images.Length) - 1;
        Label_01FB:
            DataSource.Bind<SkillParam>(obj2, param);
            DataSource.Bind<bool>(base.get_gameObject(), is_enable);
            GameParameter.UpdateAll(obj2);
            list.Add(param.iname);
            this.mCreatedCount += 1;
        Label_0231:
            num += 1;
        Label_0235:
            if (num < ((int) effect_params.Length))
            {
                goto Label_0030;
            }
            if ((this.mAwakeIconBgArray != null) == null)
            {
                goto Label_0268;
            }
            this.mAwakeIconBgArray.ImageIndex = (is_enable == null) ? 1 : 0;
        Label_0268:
            this.mParamTemplate.SetActive(0);
            return;
        }

        public void Setup(ConceptCardEffectsParam[] effect_params, int lv, int lv_cap, int awake_count_cap, bool is_enable, ConceptCardBonusWindow.eViewType view_type)
        {
            ConceptCardBonusWindow.eViewType type;
            type = view_type;
            if (type == null)
            {
                goto Label_0015;
            }
            if (type == 1)
            {
                goto Label_0027;
            }
            goto Label_0039;
        Label_0015:
            this.CreateCardSkillBonus(effect_params, lv, lv_cap, awake_count_cap, is_enable);
            goto Label_0039;
        Label_0027:
            this.CreateAbilityBonus(effect_params, lv, lv_cap, awake_count_cap, is_enable);
        Label_0039:
            base.get_gameObject().SetActive(this.mCreatedCount > 0);
            return;
        }
    }
}

