namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ArtifactDetailAbilityContent : MonoBehaviour
    {
        [SerializeField]
        private GameObject mBaseAbilityObject;
        [SerializeField]
        private GameObject mDeriveAbilityObject;
        [SerializeField]
        private ArtifactDetailAbilityItem mBaseAbilityItem;
        [SerializeField]
        private GameObject mDeriveParent;
        [SerializeField]
        private ArtifactDetailAbilityItem mDeriveTemplate;
        private List<ArtifactDetailAbilityItem> mAbilityItems;

        public ArtifactDetailAbilityContent()
        {
            this.mAbilityItems = new List<ArtifactDetailAbilityItem>();
            base..ctor();
            return;
        }

        private void BindData(GameObject target, AbilityParam ability_data, AbilityDeriveParam ability_derive_param)
        {
            int num;
            UnitParam param;
            int num2;
            JobParam param2;
            DataSource.Bind<AbilityParam>(target, ability_data);
            if (ability_data.condition_units == null)
            {
                goto Label_0067;
            }
            num = 0;
            goto Label_0059;
        Label_0019:
            if (string.IsNullOrEmpty(ability_data.condition_units[num]) == null)
            {
                goto Label_0030;
            }
            goto Label_0055;
        Label_0030:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(ability_data.condition_units[num]);
            if (param == null)
            {
                goto Label_0055;
            }
            DataSource.Bind<UnitParam>(target, param);
        Label_0055:
            num += 1;
        Label_0059:
            if (num < ((int) ability_data.condition_units.Length))
            {
                goto Label_0019;
            }
        Label_0067:
            if (ability_data.condition_jobs == null)
            {
                goto Label_00C7;
            }
            num2 = 0;
            goto Label_00B9;
        Label_0079:
            if (string.IsNullOrEmpty(ability_data.condition_jobs[num2]) == null)
            {
                goto Label_0090;
            }
            goto Label_00B5;
        Label_0090:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetJobParam(ability_data.condition_jobs[num2]);
            if (param2 == null)
            {
                goto Label_00B5;
            }
            DataSource.Bind<JobParam>(target, param2);
        Label_00B5:
            num2 += 1;
        Label_00B9:
            if (num2 < ((int) ability_data.condition_jobs.Length))
            {
                goto Label_0079;
            }
        Label_00C7:
            if (ability_derive_param == null)
            {
                goto Label_00D4;
            }
            DataSource.Bind<AbilityDeriveParam>(target, ability_derive_param);
        Label_00D4:
            return;
        }

        private void CreateBaseAbilityContent(ViewAbilityData view_ability_data, bool has_derive_ability)
        {
            if (view_ability_data.baseAbility != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.BindData(this.mBaseAbilityItem.get_gameObject(), view_ability_data.baseAbility, view_ability_data.abilityDeriveParam);
            this.mBaseAbilityItem.Setup(view_ability_data.baseAbility, 0, view_ability_data.isEnableBaseAbility, view_ability_data.isLockedBaseAbility, has_derive_ability);
            return;
        }

        private int CreateDeriveAbilityContents(ViewAbilityData view_data)
        {
            List<ViewDeriveAbilityData> list;
            int num;
            int num2;
            ArtifactDetailAbilityItem item;
            list = view_data.deriveAbilities;
            num = 0;
            if (list == null)
            {
                goto Label_00AE;
            }
            num2 = 0;
            goto Label_00A2;
        Label_0016:
            if (list[num2].is_enable != null)
            {
                goto Label_002C;
            }
            goto Label_009E;
        Label_002C:
            item = Object.Instantiate<ArtifactDetailAbilityItem>(this.mDeriveTemplate);
            item.get_transform().SetParent(this.mDeriveParent.get_transform(), 0);
            this.mAbilityItems.Add(item);
            this.BindData(item.get_gameObject(), list[num2].ability, view_data.abilityDeriveParam);
            item.Setup(list[num2].ability, 1, list[num2].is_enable, 0, 0);
            num += 1;
        Label_009E:
            num2 += 1;
        Label_00A2:
            if (num2 < list.Count)
            {
                goto Label_0016;
            }
        Label_00AE:
            if (num > 0)
            {
                goto Label_00C1;
            }
            this.mDeriveAbilityObject.SetActive(0);
        Label_00C1:
            return num;
        }

        public void Init(ViewAbilityData view_ability_data)
        {
            int num;
            bool flag;
            if (view_ability_data != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (view_ability_data.baseAbility != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            flag = this.CreateDeriveAbilityContents(view_ability_data) > 0;
            this.CreateBaseAbilityContent(view_ability_data, flag);
            if (this.mAbilityItems.Count <= 0)
            {
                goto Label_0056;
            }
            this.mAbilityItems[this.mAbilityItems.Count - 1].DestoryLastLine();
        Label_0056:
            this.mDeriveTemplate.get_gameObject().SetActive(0);
            return;
        }

        public bool IsExistEnableAbility
        {
            get
            {
                int num;
                if (this.mBaseAbilityItem.IsEnable == null)
                {
                    goto Label_0012;
                }
                return 1;
            Label_0012:
                num = 0;
                goto Label_0035;
            Label_0019:
                if (this.mAbilityItems[num].IsEnable == null)
                {
                    goto Label_0031;
                }
                return 1;
            Label_0031:
                num += 1;
            Label_0035:
                if (num < this.mAbilityItems.Count)
                {
                    goto Label_0019;
                }
                return 0;
            }
        }
    }
}

