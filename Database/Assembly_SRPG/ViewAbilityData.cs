namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    public class ViewAbilityData
    {
        private AbilityParam base_ability;
        private AbilityDeriveParam ability_derive_param;
        private bool is_enable_base_ability;
        private bool is_locked_base_ability;
        private List<ViewDeriveAbilityData> derive_abilities;

        public ViewAbilityData()
        {
            base..ctor();
            return;
        }

        public void AddAbility(AbilityParam ability_param, bool is_enable, bool is_locked)
        {
            this.base_ability = ability_param;
            this.is_enable_base_ability = is_enable;
            this.is_locked_base_ability = is_locked;
            return;
        }

        public void AddAbility(string base_ability_iname, AbilityDeriveParam derive_param, string derive_ability_iname, bool is_enable)
        {
            ViewDeriveAbilityData data;
            if (this.derive_abilities != null)
            {
                goto Label_0016;
            }
            this.derive_abilities = new List<ViewDeriveAbilityData>();
        Label_0016:
            this.base_ability = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(base_ability_iname);
            if (is_enable == null)
            {
                goto Label_003A;
            }
            this.is_enable_base_ability = 0;
        Label_003A:
            data = new ViewDeriveAbilityData();
            data.ability = MonoSingleton<GameManager>.Instance.MasterParam.GetAbilityParam(derive_ability_iname);
            data.is_enable = is_enable;
            this.derive_abilities.Add(data);
            this.ability_derive_param = derive_param;
            return;
        }

        public AbilityParam baseAbility
        {
            get
            {
                return this.base_ability;
            }
        }

        public AbilityDeriveParam abilityDeriveParam
        {
            get
            {
                return this.ability_derive_param;
            }
        }

        public bool isEnableBaseAbility
        {
            get
            {
                return this.is_enable_base_ability;
            }
        }

        public bool isLockedBaseAbility
        {
            get
            {
                return this.is_locked_base_ability;
            }
        }

        public List<ViewDeriveAbilityData> deriveAbilities
        {
            get
            {
                return this.derive_abilities;
            }
        }
    }
}

