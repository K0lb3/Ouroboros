namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class AbilityDeriveList : MonoBehaviour
    {
        [HeaderBar("▼派生元アビリティのバナーが属するリストの親"), SerializeField]
        private GameObject m_BaseAbilityRoot;
        [SerializeField]
        private Button m_DisableBaseAbility;
        [HeaderBar("▼派生先アビリティ関連のオブジェクトの親"), SerializeField]
        private GameObject m_DeriveObjectRoot;
        [SerializeField]
        private RectTransform m_DeriveAbilityListRoot;
        [SerializeField]
        private GameObject m_DeriveAbilityTemplate;
        [BitMask, SerializeField]
        private ShowFlags m_ShowFlags;
        [HeaderBar("▼アビリティ詳細を開く為のコールバック")]
        public OnListItemEvent OnDetailOpen;
        [HeaderBar("▼アビリティが選択された時のコールバック")]
        public OnListItemEvent OnSelectEvent;
        [HeaderBar("▼アビリティのランクアップボタンのコールバック")]
        public OnListItemEvent OnRankUpEvent;
        public OnListItemEvent OnRankUpBtnPressEvent;
        public OnListItemEvent OnRankUpBtnUpEvent;
        private List<GameObject> m_DeriveAbilitys;

        public AbilityDeriveList()
        {
            base..ctor();
            return;
        }

        public void AddDetailOpenEventListener(UnityAction<GameObject> callback)
        {
            if (this.OnDetailOpen != null)
            {
                goto Label_0016;
            }
            this.OnDetailOpen = new OnListItemEvent();
        Label_0016:
            this.OnDetailOpen.RemoveListener(callback);
            this.OnDetailOpen.AddListener(callback);
            return;
        }

        public void AddRankUpBtnPressEventListener(UnityAction<GameObject> callback)
        {
            if (this.OnRankUpBtnPressEvent != null)
            {
                goto Label_0016;
            }
            this.OnRankUpBtnPressEvent = new OnListItemEvent();
        Label_0016:
            this.OnRankUpBtnPressEvent.RemoveListener(callback);
            this.OnRankUpBtnPressEvent.AddListener(callback);
            return;
        }

        public void AddRankUpBtnUpEventListener(UnityAction<GameObject> callback)
        {
            if (this.OnRankUpBtnUpEvent != null)
            {
                goto Label_0016;
            }
            this.OnRankUpBtnUpEvent = new OnListItemEvent();
        Label_0016:
            this.OnRankUpBtnUpEvent.RemoveListener(callback);
            this.OnRankUpBtnUpEvent.AddListener(callback);
            return;
        }

        public void AddRankUpEventListener(UnityAction<GameObject> callback)
        {
            if (this.OnRankUpEvent != null)
            {
                goto Label_0016;
            }
            this.OnRankUpEvent = new OnListItemEvent();
        Label_0016:
            this.OnRankUpEvent.RemoveListener(callback);
            this.OnRankUpEvent.AddListener(callback);
            return;
        }

        public void AddSelectEventListener(UnityAction<GameObject> callback)
        {
            if (this.OnSelectEvent != null)
            {
                goto Label_0016;
            }
            this.OnSelectEvent = new OnListItemEvent();
        Label_0016:
            this.OnSelectEvent.RemoveListener(callback);
            this.OnSelectEvent.AddListener(callback);
            return;
        }

        private GameObject CreateDeriveAbilityItem(AbilityData derivedAbility)
        {
            GameObject obj2;
            obj2 = null;
            if (this.m_DeriveAbilitys != null)
            {
                goto Label_0018;
            }
            this.m_DeriveAbilitys = new List<GameObject>();
        Label_0018:
            if ((this.m_DeriveAbilityTemplate != null) == null)
            {
                goto Label_0090;
            }
            obj2 = Object.Instantiate<GameObject>(this.m_DeriveAbilityTemplate);
            if (derivedAbility.DeriveParam == null)
            {
                goto Label_0051;
            }
            DataSource.Bind<SkillAbilityDeriveParam>(obj2, derivedAbility.DeriveParam.m_SkillAbilityDeriveParam);
        Label_0051:
            DataSource.Bind<AbilityData>(obj2, derivedAbility);
            DataSource.Bind<AbilityDeriveParam>(obj2, derivedAbility.DeriveParam);
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.m_DeriveAbilityListRoot, 0);
            this.m_DeriveAbilitys.Add(obj2);
            this.RegisterHoldEvent(obj2);
        Label_0090:
            return obj2;
        }

        private GameObject CreateDeriveAbilityItem(AbilityDeriveParam abilityDeriveParam)
        {
            GameObject obj2;
            obj2 = null;
            if (this.m_DeriveAbilitys != null)
            {
                goto Label_0018;
            }
            this.m_DeriveAbilitys = new List<GameObject>();
        Label_0018:
            if ((this.m_DeriveAbilityTemplate != null) == null)
            {
                goto Label_0080;
            }
            obj2 = Object.Instantiate<GameObject>(this.m_DeriveAbilityTemplate);
            DataSource.Bind<SkillAbilityDeriveParam>(obj2, abilityDeriveParam.m_SkillAbilityDeriveParam);
            DataSource.Bind<AbilityParam>(obj2, abilityDeriveParam.m_DeriveParam);
            DataSource.Bind<AbilityDeriveParam>(obj2, abilityDeriveParam);
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.m_DeriveAbilityListRoot, 0);
            this.m_DeriveAbilitys.Add(obj2);
            this.RegisterHoldEvent(obj2);
        Label_0080:
            return obj2;
        }

        private GameObject CreateDeriveConceptCardSkillItem(SkillData derivedSkill)
        {
            GameObject obj2;
            obj2 = null;
            if (this.m_DeriveAbilitys != null)
            {
                goto Label_0018;
            }
            this.m_DeriveAbilitys = new List<GameObject>();
        Label_0018:
            if ((this.m_DeriveAbilityTemplate != null) == null)
            {
                goto Label_0089;
            }
            obj2 = Object.Instantiate<GameObject>(this.m_DeriveAbilityTemplate);
            if (derivedSkill.DeriveParam == null)
            {
                goto Label_0051;
            }
            DataSource.Bind<SkillAbilityDeriveParam>(obj2, derivedSkill.DeriveParam.m_SkillAbilityDeriveParam);
        Label_0051:
            DataSource.Bind<SkillData>(obj2, derivedSkill);
            DataSource.Bind<SkillDeriveParam>(obj2, derivedSkill.DeriveParam);
            obj2.SetActive(1);
            obj2.get_transform().SetParent(this.m_DeriveAbilityListRoot, 0);
            this.m_DeriveAbilitys.Add(obj2);
        Label_0089:
            return obj2;
        }

        public void OnRankUp(GameObject obj)
        {
            if (this.OnRankUpEvent == null)
            {
                goto Label_0017;
            }
            this.OnRankUpEvent.Invoke(obj);
        Label_0017:
            return;
        }

        public void OnRankUpBtnPress(GameObject obj)
        {
            if (this.OnRankUpBtnPressEvent == null)
            {
                goto Label_0017;
            }
            this.OnRankUpBtnPressEvent.Invoke(obj);
        Label_0017:
            return;
        }

        public void OnRankUpBtnUp(GameObject obj)
        {
            if (this.OnRankUpBtnUpEvent == null)
            {
                goto Label_0017;
            }
            this.OnRankUpBtnUpEvent.Invoke(obj);
        Label_0017:
            return;
        }

        public void OnSelect(GameObject obj)
        {
            if (this.OnSelectEvent == null)
            {
                goto Label_0017;
            }
            this.OnSelectEvent.Invoke(obj);
        Label_0017:
            return;
        }

        public void OpenAbilityDetail(GameObject obj)
        {
            AbilityParam param;
            param = DataSource.FindDataOfClass<AbilityParam>(obj, null);
            if (param == null)
            {
                goto Label_0014;
            }
            AbilityDetailWindow.SetBindAbility(param);
        Label_0014:
            if (this.OnDetailOpen == null)
            {
                goto Label_002B;
            }
            this.OnDetailOpen.Invoke(obj);
        Label_002B:
            return;
        }

        private void RegisterHoldEvent(GameObject obj)
        {
            UnitAbilityListItemEvents events;
            events = obj.GetComponentInChildren<UnitAbilityListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0167;
            }
            events.OnOpenDetail = (ListItemEvents.ListItemEvent) Delegate.Remove(events.OnOpenDetail, new ListItemEvents.ListItemEvent(this.OpenAbilityDetail));
            events.OnOpenDetail = (ListItemEvents.ListItemEvent) Delegate.Combine(events.OnOpenDetail, new ListItemEvents.ListItemEvent(this.OpenAbilityDetail));
            events.OnSelect = (ListItemEvents.ListItemEvent) Delegate.Remove(events.OnSelect, new ListItemEvents.ListItemEvent(this.OnSelect));
            events.OnSelect = (ListItemEvents.ListItemEvent) Delegate.Combine(events.OnSelect, new ListItemEvents.ListItemEvent(this.OnSelect));
            events.OnRankUp = (ListItemEvents.ListItemEvent) Delegate.Remove(events.OnRankUp, new ListItemEvents.ListItemEvent(this.OnRankUp));
            events.OnRankUp = (ListItemEvents.ListItemEvent) Delegate.Combine(events.OnRankUp, new ListItemEvents.ListItemEvent(this.OnRankUp));
            events.OnRankUpBtnPress = (ListItemEvents.ListItemEvent) Delegate.Remove(events.OnRankUpBtnPress, new ListItemEvents.ListItemEvent(this.OnRankUpBtnPress));
            events.OnRankUpBtnPress = (ListItemEvents.ListItemEvent) Delegate.Combine(events.OnRankUpBtnPress, new ListItemEvents.ListItemEvent(this.OnRankUpBtnPress));
            events.OnRankUpBtnUp = (ListItemEvents.ListItemEvent) Delegate.Remove(events.OnRankUpBtnUp, new ListItemEvents.ListItemEvent(this.OnRankUpBtnUp));
            events.OnRankUpBtnUp = (ListItemEvents.ListItemEvent) Delegate.Combine(events.OnRankUpBtnUp, new ListItemEvents.ListItemEvent(this.OnRankUpBtnUp));
        Label_0167:
            return;
        }

        public void RemoveOnDetailOpen(UnityAction<GameObject> callback)
        {
            if (this.OnDetailOpen == null)
            {
                goto Label_0017;
            }
            this.OnDetailOpen.RemoveListener(callback);
        Label_0017:
            return;
        }

        private static void SetupConceptCard(GameObject obj, ConceptCardSkillDatailData conceptCardSkillDatailData, ConceptCardDetailAbility.ClickDetail onClickDetail)
        {
            ConceptCardDetailAbility ability;
            ability = obj.GetComponent<ConceptCardDetailAbility>();
            if ((ability != null) == null)
            {
                goto Label_0021;
            }
            ability.SetCardSkill(conceptCardSkillDatailData);
            ability.SetAbilityDetailParent(onClickDetail);
        Label_0021:
            return;
        }

        public unsafe void SetupWithAbilityData(AbilityData baseAbility, List<AbilityData> derivedAbilitys)
        {
            AbilityData data;
            List<AbilityData>.Enumerator enumerator;
            if (derivedAbilitys == null)
            {
                goto Label_0050;
            }
            if (derivedAbilitys.Count <= 0)
            {
                goto Label_0050;
            }
            enumerator = derivedAbilitys.GetEnumerator();
        Label_0019:
            try
            {
                goto Label_002E;
            Label_001E:
                data = &enumerator.Current;
                this.CreateDeriveAbilityItem(data);
            Label_002E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001E;
                }
                goto Label_004B;
            }
            finally
            {
            Label_003F:
                ((List<AbilityData>.Enumerator) enumerator).Dispose();
            }
        Label_004B:
            goto Label_0057;
        Label_0050:
            this.IsShowBaseAbility = 1;
        Label_0057:
            if ((this.m_BaseAbilityRoot != null) == null)
            {
                goto Label_0080;
            }
            this.RegisterHoldEvent(this.m_BaseAbilityRoot);
            DataSource.Bind<AbilityData>(this.m_BaseAbilityRoot, baseAbility);
        Label_0080:
            this.UpdateUIActive();
            return;
        }

        public unsafe void SetupWithAbilityParam(AbilityParam baseAbility, List<AbilityDeriveParam> abilityDeriveParams)
        {
            AbilityDeriveParam param;
            List<AbilityDeriveParam>.Enumerator enumerator;
            if (abilityDeriveParams == null)
            {
                goto Label_0050;
            }
            if (abilityDeriveParams.Count <= 0)
            {
                goto Label_0050;
            }
            enumerator = abilityDeriveParams.GetEnumerator();
        Label_0019:
            try
            {
                goto Label_002E;
            Label_001E:
                param = &enumerator.Current;
                this.CreateDeriveAbilityItem(param);
            Label_002E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001E;
                }
                goto Label_004B;
            }
            finally
            {
            Label_003F:
                ((List<AbilityDeriveParam>.Enumerator) enumerator).Dispose();
            }
        Label_004B:
            goto Label_0057;
        Label_0050:
            this.IsShowBaseAbility = 1;
        Label_0057:
            if ((this.m_BaseAbilityRoot != null) == null)
            {
                goto Label_0080;
            }
            this.RegisterHoldEvent(this.m_BaseAbilityRoot);
            DataSource.Bind<AbilityParam>(this.m_BaseAbilityRoot, baseAbility);
        Label_0080:
            this.UpdateUIActive();
            return;
        }

        public unsafe void SetupWithConceptCard(ConceptCardSkillDatailData conceptCardSkillDatailData, UnitData unitData, ConceptCardDetailAbility.ClickDetail onClickDetail)
        {
            bool flag;
            List<AbilityData> list;
            AbilityData data;
            AbilityData data2;
            List<AbilityData>.Enumerator enumerator;
            List<ConceptCardSkillDatailData> list2;
            ConceptCardSkillDatailData data3;
            List<ConceptCardSkillDatailData>.Enumerator enumerator2;
            GameObject obj2;
            AbilityData data4;
            SkillData data5;
            flag = (conceptCardSkillDatailData.effect == null) ? 0 : ((conceptCardSkillDatailData.effect.Ability == null) == 0);
            if (unitData == null)
            {
                goto Label_00ED;
            }
            if (conceptCardSkillDatailData.type == 3)
            {
                goto Label_00ED;
            }
            if (flag == null)
            {
                goto Label_00ED;
            }
            list = new List<AbilityData>();
            data = conceptCardSkillDatailData.effect.Ability;
            enumerator = unitData.SearchDerivedAbilitys(data).GetEnumerator();
        Label_005A:
            try
            {
                goto Label_00CF;
            Label_005F:
                data2 = &enumerator.Current;
                enumerator2 = ConceptCardUtility.CreateConceptCardSkillDatailData(data2).GetEnumerator();
            Label_0078:
                try
                {
                    goto Label_00B1;
                Label_007D:
                    data3 = &enumerator2.Current;
                    if (data3.type != 2)
                    {
                        goto Label_00B1;
                    }
                    SetupConceptCard(this.CreateDeriveAbilityItem(data3.effect.Ability), data3, onClickDetail);
                Label_00B1:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_007D;
                    }
                    goto Label_00CF;
                }
                finally
                {
                Label_00C2:
                    ((List<ConceptCardSkillDatailData>.Enumerator) enumerator2).Dispose();
                }
            Label_00CF:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_005F;
                }
                goto Label_00ED;
            }
            finally
            {
            Label_00E0:
                ((List<AbilityData>.Enumerator) enumerator).Dispose();
            }
        Label_00ED:
            if (this.HasDerive != null)
            {
                goto Label_0100;
            }
            this.ShowFlags_IsOn(1);
        Label_0100:
            if ((this.m_BaseAbilityRoot != null) == null)
            {
                goto Label_0165;
            }
            if (flag == null)
            {
                goto Label_0143;
            }
            data4 = conceptCardSkillDatailData.effect.Ability;
            SetupConceptCard(this.m_BaseAbilityRoot, conceptCardSkillDatailData, onClickDetail);
            DataSource.Bind<AbilityData>(this.m_BaseAbilityRoot, data4);
            goto Label_0165;
        Label_0143:
            data5 = conceptCardSkillDatailData.skill_data;
            SetupConceptCard(this.m_BaseAbilityRoot, conceptCardSkillDatailData, onClickDetail);
            DataSource.Bind<SkillData>(this.m_BaseAbilityRoot, data5);
        Label_0165:
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
            GameUtility.SetGameObjectActive(this.m_DeriveAbilityTemplate, 0);
            return;
        }

        public void SwitchBaseAbilityEnable(bool enable)
        {
            this.IsShowBaseAbility = enable;
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
            if (this.IsShowBaseAbility == null)
            {
                goto Label_00CA;
            }
            GameUtility.SetGameObjectActive(this.m_BaseAbilityRoot, 1);
            if (this.HasDerive == null)
            {
                goto Label_0089;
            }
            GameUtility.SetGameObjectActive(this.m_DeriveObjectRoot, 1);
            num = this.m_DeriveAbilitys.Count - 1;
            num2 = 0;
            goto Label_0073;
        Label_0043:
            item = this.m_DeriveAbilitys[num2].GetComponent<DeriveListItem>();
            if ((item != null) == null)
            {
                goto Label_006F;
            }
            item.SetLineActive(1, (num2 == num) == 0);
        Label_006F:
            num2 += 1;
        Label_0073:
            if (num2 < this.m_DeriveAbilitys.Count)
            {
                goto Label_0043;
            }
            goto Label_0095;
        Label_0089:
            GameUtility.SetGameObjectActive(this.m_DeriveObjectRoot, 0);
        Label_0095:
            if (this.IsDisableBaseAbilityInteractable == null)
            {
                goto Label_0167;
            }
            if ((this.m_DisableBaseAbility != null) == null)
            {
                goto Label_0167;
            }
            this.m_DisableBaseAbility.set_interactable(this.HasDerive == 0);
            goto Label_0167;
        Label_00CA:
            if (this.HasDerive == null)
            {
                goto Label_012B;
            }
            GameUtility.SetGameObjectActive(this.m_BaseAbilityRoot, 0);
            num3 = 0;
            goto Label_0115;
        Label_00E8:
            item2 = this.m_DeriveAbilitys[num3].GetComponent<DeriveListItem>();
            if ((item2 != null) == null)
            {
                goto Label_0111;
            }
            item2.SetLineActive(0, 0);
        Label_0111:
            num3 += 1;
        Label_0115:
            if (num3 < this.m_DeriveAbilitys.Count)
            {
                goto Label_00E8;
            }
            goto Label_0137;
        Label_012B:
            GameUtility.SetGameObjectActive(this.m_BaseAbilityRoot, 1);
        Label_0137:
            if (this.IsDisableBaseAbilityInteractable == null)
            {
                goto Label_0167;
            }
            if ((this.m_DisableBaseAbility != null) == null)
            {
                goto Label_0167;
            }
            this.m_DisableBaseAbility.set_interactable(this.HasDerive == 0);
        Label_0167:
            return;
        }

        public bool HasDerive
        {
            get
            {
                return ((this.m_DeriveAbilitys == null) ? 0 : (this.m_DeriveAbilitys.Count > 0));
            }
        }

        public bool IsShowBaseAbility
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

        public bool IsDisableBaseAbilityInteractable
        {
            get
            {
                return this.ShowFlags_IsOn(2);
            }
        }

        [Serializable]
        public class OnListItemEvent : UnityEvent<GameObject>
        {
            public OnListItemEvent()
            {
                base..ctor();
                return;
            }
        }

        [Flags]
        private enum ShowFlags
        {
            ShowBaseAbility = 1,
            DisableBaseAbilityInteractable = 2
        }
    }
}

