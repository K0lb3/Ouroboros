namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class TobiraLearnSkill : MonoBehaviour
    {
        [SerializeField]
        private Text m_LearnSkillName;
        [SerializeField]
        private Text m_LearnSkillEffect;

        public TobiraLearnSkill()
        {
            base..ctor();
            return;
        }

        public void Setup(AbilityData newAbility)
        {
            this.m_LearnSkillName.set_text("アビリティ：" + newAbility.AbilityName);
            this.m_LearnSkillEffect.set_text(newAbility.Param.expr);
            return;
        }

        public void Setup(SkillData skill)
        {
            this.m_LearnSkillName.set_text("リーダースキル：" + skill.Name);
            this.m_LearnSkillEffect.set_text(skill.SkillParam.expr);
            return;
        }
    }
}

