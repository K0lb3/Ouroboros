namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitTobiraLearnAbilityPopup : MonoBehaviour
    {
        [SerializeField]
        private Text mTitleText;
        [SerializeField]
        private Text mNameText;
        [SerializeField]
        private Text mDescText;

        public UnitTobiraLearnAbilityPopup()
        {
            base..ctor();
            return;
        }

        public void Setup(UnitData unit, SkillParam skill)
        {
            if (unit == null)
            {
                goto Label_000C;
            }
            if (skill != null)
            {
                goto Label_000D;
            }
        Label_000C:
            return;
        Label_000D:
            this.mTitleText.set_text(LocalizedText.Get("sys.TOBIRA_LEARN_NEW_LEADER_SKILL_TEXT"));
            this.mNameText.set_text(skill.name);
            this.mDescText.set_text(skill.expr);
            DataSource.Bind<UnitData>(base.get_gameObject(), unit);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void Setup(UnitData unit, AbilityParam new_ability, AbilityParam old_ability)
        {
            if ((unit != null) && (new_ability != null))
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            this.mTitleText.set_text((old_ability == null) ? LocalizedText.Get("sys.TOBIRA_LEARN_NEW_ABILITY_TEXT") : LocalizedText.Get("sys.TOBIRA_LEARN_OVERRIDE_NEW_ABILITY_TEXT"));
            this.mNameText.set_text(new_ability.name);
            this.mDescText.set_text(new_ability.expr);
            DataSource.Bind<UnitData>(base.get_gameObject(), unit);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }
    }
}

