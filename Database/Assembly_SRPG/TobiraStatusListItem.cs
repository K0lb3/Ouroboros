namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TobiraStatusListItem : MonoBehaviour
    {
        [SerializeField]
        private Text m_TextTobiraName;
        [SerializeField]
        private ImageArray m_IconTobira;
        [SerializeField]
        private GameObject m_ObjectDetail;
        [SerializeField]
        private GameObject m_ObjectLock;
        [SerializeField]
        private GameObject m_ObjectCommingSoon;
        [SerializeField]
        private TobiraLearnSkill m_TobiraLearnSkillTemplate;
        [SerializeField]
        private RectTransform m_TobiraLearnSkillParent;
        [SerializeField]
        private StatusList m_StatusList;
        [SerializeField]
        private GameObject m_TobiraLvMaxObject;
        private TobiraParam.Category m_Category;

        public TobiraStatusListItem()
        {
            base..ctor();
            return;
        }

        private TobiraLearnSkill CreateListItem()
        {
            GameObject obj2;
            TobiraLearnSkill skill;
            obj2 = Object.Instantiate<GameObject>(this.m_TobiraLearnSkillTemplate.get_gameObject());
            skill = obj2.GetComponent<TobiraLearnSkill>();
            obj2.get_transform().SetParent(this.m_TobiraLearnSkillParent, 0);
            obj2.SetActive(1);
            return skill;
        }

        public void SetTobiraLvIsMax(bool isMax)
        {
            GameUtility.SetGameObjectActive(this.m_TobiraLvMaxObject, isMax);
            return;
        }

        public unsafe void Setup(TobiraData tobiraData)
        {
            TobiraLearnSkill skill;
            SkillData data;
            List<AbilityData> list;
            List<AbilityData> list2;
            int num;
            TobiraLearnSkill skill2;
            BaseStatus status;
            BaseStatus status2;
            if (tobiraData != null)
            {
                goto Label_0011;
            }
            DebugUtility.LogError("tobiraDataがnullの時はSetup(TobiraParam param)を使用してください");
            return;
        Label_0011:
            this.m_Category = tobiraData.Param.TobiraCategory;
            this.m_TextTobiraName.set_text(TobiraParam.GetCategoryName(this.m_Category));
            this.m_IconTobira.ImageIndex = this.m_Category;
            if (tobiraData.IsLearnedLeaderSkill == null)
            {
                goto Label_0077;
            }
            skill = this.CreateListItem();
            data = new SkillData();
            data.Setup(tobiraData.LearnedLeaderSkillIname, 1, 1, null);
            skill.Setup(data);
        Label_0077:
            list = new List<AbilityData>();
            list2 = new List<AbilityData>();
            TobiraUtility.TryCraeteAbilityData(tobiraData.Param, tobiraData.Lv, list, list2, 0);
            num = 0;
            goto Label_00BC;
        Label_009F:
            this.CreateListItem().Setup(list[num]);
            num += 1;
        Label_00BC:
            if (num < list.Count)
            {
                goto Label_009F;
            }
            status = new BaseStatus();
            status2 = new BaseStatus();
            TobiraUtility.CalcTobiraParameter(tobiraData.Param.UnitIname, this.m_Category, tobiraData.Lv, &status, &status2);
            this.m_StatusList.SetValues(status, status2, 0);
            GameUtility.SetGameObjectActive(this.m_ObjectDetail, 1);
            GameUtility.SetGameObjectActive(this.m_ObjectLock, 0);
            GameUtility.SetGameObjectActive(this.m_ObjectCommingSoon, 0);
            return;
        }

        public unsafe void Setup(TobiraParam param)
        {
            BaseStatus status;
            BaseStatus status2;
            this.m_Category = param.TobiraCategory;
            this.m_TextTobiraName.set_text(TobiraParam.GetCategoryName(this.m_Category));
            this.m_IconTobira.ImageIndex = this.m_Category;
            if (param.Enable == null)
            {
                goto Label_006E;
            }
            status = new BaseStatus();
            status2 = new BaseStatus();
            TobiraUtility.CalcTobiraParameter(param.UnitIname, this.m_Category, 1, &status, &status2);
            this.m_StatusList.SetValues(status, status2, 1);
        Label_006E:
            GameUtility.SetGameObjectActive(this.m_ObjectDetail, param.Enable);
            GameUtility.SetGameObjectActive(this.m_ObjectLock, param.Enable);
            GameUtility.SetGameObjectActive(this.m_ObjectCommingSoon, param.Enable == 0);
            return;
        }

        private void Start()
        {
            GameUtility.SetGameObjectActive(this.m_TobiraLearnSkillTemplate, 0);
            return;
        }
    }
}

