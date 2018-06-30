namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class ConceptCardBonusContentAwake : MonoBehaviour
    {
        [SerializeField]
        private GameObject mSkillParamTemplate;
        [SerializeField]
        private ImageArray mAwakeIconImageArray;
        [SerializeField]
        private ImageArray mAwakeIconBgArray;
        [SerializeField]
        private ImageArray mProgressLine;
        private int mCreatedCount;
        private bool mIsEnable;

        public ConceptCardBonusContentAwake()
        {
            base..ctor();
            return;
        }

        public void SetProgressLineImage(bool is_enable, bool is_active)
        {
            this.mProgressLine.ImageIndex = (is_enable == null) ? 1 : 0;
            this.mProgressLine.get_gameObject().SetActive(is_active);
            return;
        }

        public unsafe void Setup(ConceptCardEffectsParam[] effect_params, int awake_count, int awake_count_cap, bool is_enable)
        {
            Transform transform;
            List<string> list;
            int num;
            SkillParam param;
            BaseStatus status;
            BaseStatus status2;
            GameObject obj2;
            StatusList list2;
            if ((this.mSkillParamTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mIsEnable = is_enable;
            transform = this.mSkillParamTemplate.get_transform().get_parent();
            list = new List<string>();
            num = 0;
            goto Label_0144;
        Label_0038:
            if (string.IsNullOrEmpty(effect_params[num].card_skill) == null)
            {
                goto Label_004F;
            }
            goto Label_0140;
        Label_004F:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(effect_params[num].card_skill);
            if (param != null)
            {
                goto Label_0072;
            }
            goto Label_0140;
        Label_0072:
            if (string.IsNullOrEmpty(effect_params[num].add_card_skill_buff_awake) == null)
            {
                goto Label_0089;
            }
            goto Label_0140;
        Label_0089:
            if (list.Contains(param.iname) == null)
            {
                goto Label_009F;
            }
            goto Label_0140;
        Label_009F:
            status = new BaseStatus();
            status2 = new BaseStatus();
            effect_params[num].GetAddCardSkillBuffStatusAwake(awake_count, awake_count_cap, &status, &status2);
            obj2 = Object.Instantiate<GameObject>(this.mSkillParamTemplate);
            obj2.get_transform().SetParent(transform, 0);
            obj2.GetComponentInChildren<StatusList>().SetValues(status, status2, 0);
            if ((this.mAwakeIconImageArray != null) == null)
            {
                goto Label_010A;
            }
            this.mAwakeIconImageArray.ImageIndex = awake_count - 1;
        Label_010A:
            DataSource.Bind<SkillParam>(obj2, param);
            DataSource.Bind<bool>(base.get_gameObject(), is_enable);
            GameParameter.UpdateAll(obj2);
            list.Add(param.iname);
            this.mCreatedCount += 1;
        Label_0140:
            num += 1;
        Label_0144:
            if (num < ((int) effect_params.Length))
            {
                goto Label_0038;
            }
            if ((this.mAwakeIconBgArray != null) == null)
            {
                goto Label_0177;
            }
            this.mAwakeIconBgArray.ImageIndex = (is_enable == null) ? 1 : 0;
        Label_0177:
            this.mSkillParamTemplate.SetActive(0);
            base.get_gameObject().SetActive(this.mCreatedCount > 0);
            return;
        }

        public bool IsEnable
        {
            get
            {
                return this.mIsEnable;
            }
        }
    }
}

