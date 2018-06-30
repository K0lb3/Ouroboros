namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class SkillDeriveList : MonoBehaviour
    {
        [SerializeField, HeaderBar("▼派生元スキルのバナーが属するリストの親")]
        private GameObject m_BaseSkillRoot;
        [SerializeField]
        private GameObject m_DisableBaseObject;
        [SerializeField, HeaderBar("▼派生先スキル関連のオブジェクトの親")]
        private GameObject m_DeriveObjectRoot;
        [SerializeField]
        private RectTransform m_DeriveSkillListRoot;
        [SerializeField]
        private GameObject m_DeriveSkillTemplate;
        [BitMask, SerializeField]
        private ShowFlags m_ShowFlags;
        private List<GameObject> m_DeriveSkills;

        public SkillDeriveList()
        {
            base..ctor();
            return;
        }

        private void CreateDeriveSkillItem(SkillDeriveParam skillDeriveParam)
        {
            GameObject obj2;
            if (this.m_DeriveSkills != null)
            {
                goto Label_0016;
            }
            this.m_DeriveSkills = new List<GameObject>();
        Label_0016:
            if ((this.m_DeriveSkillTemplate != null) == null)
            {
                goto Label_0077;
            }
            obj2 = Object.Instantiate<GameObject>(this.m_DeriveSkillTemplate);
            DataSource.Bind<SkillAbilityDeriveParam>(obj2, skillDeriveParam.m_SkillAbilityDeriveParam);
            DataSource.Bind<SkillParam>(obj2, skillDeriveParam.m_DeriveParam);
            DataSource.Bind<SkillDeriveParam>(obj2, skillDeriveParam);
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.m_DeriveSkillListRoot, 0);
            this.m_DeriveSkills.Add(obj2);
        Label_0077:
            return;
        }

        public unsafe void Setup(SkillParam baseSkill, List<SkillDeriveParam> skillDeriveParams)
        {
            SkillDeriveParam param;
            List<SkillDeriveParam>.Enumerator enumerator;
            if (skillDeriveParams == null)
            {
                goto Label_004F;
            }
            if (skillDeriveParams.Count <= 0)
            {
                goto Label_004F;
            }
            enumerator = skillDeriveParams.GetEnumerator();
        Label_0019:
            try
            {
                goto Label_002D;
            Label_001E:
                param = &enumerator.Current;
                this.CreateDeriveSkillItem(param);
            Label_002D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001E;
                }
                goto Label_004A;
            }
            finally
            {
            Label_003E:
                ((List<SkillDeriveParam>.Enumerator) enumerator).Dispose();
            }
        Label_004A:
            goto Label_0056;
        Label_004F:
            this.IsShowBaseSkill = 1;
        Label_0056:
            if ((this.m_BaseSkillRoot != null) == null)
            {
                goto Label_0073;
            }
            DataSource.Bind<SkillParam>(this.m_BaseSkillRoot, baseSkill);
        Label_0073:
            this.UpdateUIActive();
            return;
        }

        private bool ShowFlags_IsOff(ShowFlags flags)
        {
            return ((this.m_ShowFlags & flags) == 0);
        }

        private bool ShowFlags_IsOn(ShowFlags flags)
        {
            return (((this.m_ShowFlags & flags) == 0) == 0);
        }

        private void ShowFlags_Set(ShowFlags flags, bool value)
        {
            if (value == null)
            {
                goto Label_0012;
            }
            this.ShowFlags_SetOn(flags);
            goto Label_0019;
        Label_0012:
            this.ShowFlags_SetOff(flags);
        Label_0019:
            return;
        }

        private void ShowFlags_SetOff(ShowFlags flags)
        {
            this.m_ShowFlags &= ~flags;
            return;
        }

        private void ShowFlags_SetOn(ShowFlags flags)
        {
            this.m_ShowFlags |= flags;
            return;
        }

        private void Start()
        {
            GameUtility.SetGameObjectActive(this.m_DeriveSkillTemplate, 0);
            return;
        }

        public void SwitchBaseSkillEnable(bool enable)
        {
            this.IsShowBaseSkill = enable;
            this.UpdateUIActive();
            return;
        }

        private void UpdateUIActive()
        {
            int num;
            int num2;
            DeriveListItem item;
            int num3;
            DeriveListItem item2;
            if (this.IsShowBaseSkill == null)
            {
                goto Label_00B6;
            }
            GameUtility.SetGameObjectActive(this.m_BaseSkillRoot, 1);
            if (this.HasDerive == null)
            {
                goto Label_0089;
            }
            GameUtility.SetGameObjectActive(this.m_DeriveObjectRoot, 1);
            num = this.m_DeriveSkills.Count - 1;
            num2 = 0;
            goto Label_0073;
        Label_0043:
            item = this.m_DeriveSkills[num2].GetComponent<DeriveListItem>();
            if ((item != null) == null)
            {
                goto Label_006F;
            }
            item.SetLineActive(1, (num2 == num) == 0);
        Label_006F:
            num2 += 1;
        Label_0073:
            if (num2 < this.m_DeriveSkills.Count)
            {
                goto Label_0043;
            }
            goto Label_0095;
        Label_0089:
            GameUtility.SetGameObjectActive(this.m_DeriveObjectRoot, 0);
        Label_0095:
            if (this.IsDisableBaseSkillInteractable == null)
            {
                goto Label_013F;
            }
            GameUtility.SetGameObjectActive(this.m_DisableBaseObject, this.HasDerive);
            goto Label_013F;
        Label_00B6:
            if (this.HasDerive == null)
            {
                goto Label_0117;
            }
            GameUtility.SetGameObjectActive(this.m_BaseSkillRoot, 0);
            num3 = 0;
            goto Label_0101;
        Label_00D4:
            item2 = this.m_DeriveSkills[num3].GetComponent<DeriveListItem>();
            if ((item2 != null) == null)
            {
                goto Label_00FD;
            }
            item2.SetLineActive(0, 0);
        Label_00FD:
            num3 += 1;
        Label_0101:
            if (num3 < this.m_DeriveSkills.Count)
            {
                goto Label_00D4;
            }
            goto Label_0123;
        Label_0117:
            GameUtility.SetGameObjectActive(this.m_BaseSkillRoot, 1);
        Label_0123:
            if (this.IsDisableBaseSkillInteractable == null)
            {
                goto Label_013F;
            }
            GameUtility.SetGameObjectActive(this.m_DisableBaseObject, this.HasDerive);
        Label_013F:
            return;
        }

        public bool HasDerive
        {
            get
            {
                return ((this.m_DeriveSkills == null) ? 0 : (this.m_DeriveSkills.Count > 0));
            }
        }

        public bool IsShowBaseSkill
        {
            get
            {
                return this.ShowFlags_IsOn(1);
            }
            set
            {
                this.ShowFlags_Set(1, value);
                return;
            }
        }

        public bool IsDisableBaseSkillInteractable
        {
            get
            {
                return this.ShowFlags_IsOn(2);
            }
        }

        [Flags]
        private enum ShowFlags
        {
            ShowBaseSkill = 1,
            DisableBaseSkillInteractable = 2
        }
    }
}

