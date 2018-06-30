namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SkillAbilityDeriveListItem : MonoBehaviour
    {
        [HeaderBar("▼派生アビリティ関連"), SerializeField]
        private RectTransform m_AbilityDeriveListRoot;
        [SerializeField]
        private AbilityDeriveList m_AbilityDeriveListTemplate;
        [HeaderBar("▼派生スキル関連"), SerializeField]
        private RectTransform m_SkillDeriveListRoot;
        [SerializeField]
        private SkillDeriveList m_SkillDeriveListTemplate;
        private List<ViewContentSkillParam> m_ViewContentSkillParams;
        private List<ViewContentAbilityParam> m_ViewContentAbilityParams;

        public SkillAbilityDeriveListItem()
        {
            base..ctor();
            return;
        }

        private void CreateListItem(ViewContentAbilityParam viewContentAbilityParam)
        {
            GameObject obj2;
            AbilityDeriveList list;
            obj2 = Object.Instantiate<GameObject>(this.m_AbilityDeriveListTemplate.get_gameObject());
            obj2.get_transform().SetParent(this.m_AbilityDeriveListRoot, 0);
            obj2.SetActive(1);
            obj2.GetComponent<AbilityDeriveList>().SetupWithAbilityParam(viewContentAbilityParam.m_BaseAbilityParam, viewContentAbilityParam.m_AbilityDeriveParam);
            return;
        }

        private void CreateListItem(ViewContentSkillParam viewContentSkillParam)
        {
            GameObject obj2;
            SkillDeriveList list;
            obj2 = Object.Instantiate<GameObject>(this.m_SkillDeriveListTemplate.get_gameObject());
            obj2.get_transform().SetParent(this.m_SkillDeriveListRoot, 0);
            obj2.SetActive(1);
            obj2.GetComponent<SkillDeriveList>().Setup(viewContentSkillParam.m_BaseSkillParam, viewContentSkillParam.m_SkillDeriveParams);
            return;
        }

        private static unsafe List<ViewContentAbilityParam> CreateViewContentAbilityParams(List<AbilityDeriveParam> deriveAbilityParams)
        {
            List<ViewContentAbilityParam> list;
            List<AbilityDeriveParam>.Enumerator enumerator;
            ViewContentAbilityParam param;
            <CreateViewContentAbilityParams>c__AnonStorey3A5 storeya;
            list = new List<ViewContentAbilityParam>();
            storeya = new <CreateViewContentAbilityParams>c__AnonStorey3A5();
            enumerator = deriveAbilityParams.GetEnumerator();
        Label_0013:
            try
            {
                goto Label_007A;
            Label_0018:
                storeya.abilityDeriveParam = &enumerator.Current;
                param = null;
                param = list.Find(new Predicate<ViewContentAbilityParam>(storeya.<>m__411));
                if (param != null)
                {
                    goto Label_0058;
                }
                param = new ViewContentAbilityParam();
                param.m_AbilityDeriveParam = new List<AbilityDeriveParam>();
                list.Add(param);
            Label_0058:
                param.m_BaseAbilityParam = storeya.abilityDeriveParam.m_BaseParam;
                param.m_AbilityDeriveParam.Add(storeya.abilityDeriveParam);
            Label_007A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0018;
                }
                goto Label_0097;
            }
            finally
            {
            Label_008B:
                ((List<AbilityDeriveParam>.Enumerator) enumerator).Dispose();
            }
        Label_0097:
            return list;
        }

        private static unsafe List<ViewContentSkillParam> CreateViewContentSkillParams(List<SkillDeriveParam> deriveSkillParams)
        {
            List<ViewContentSkillParam> list;
            List<SkillDeriveParam>.Enumerator enumerator;
            ViewContentSkillParam param;
            <CreateViewContentSkillParams>c__AnonStorey3A6 storeya;
            list = new List<ViewContentSkillParam>();
            storeya = new <CreateViewContentSkillParams>c__AnonStorey3A6();
            enumerator = deriveSkillParams.GetEnumerator();
        Label_0013:
            try
            {
                goto Label_007A;
            Label_0018:
                storeya.skillDeriveParam = &enumerator.Current;
                param = null;
                param = list.Find(new Predicate<ViewContentSkillParam>(storeya.<>m__412));
                if (param != null)
                {
                    goto Label_0058;
                }
                param = new ViewContentSkillParam();
                param.m_SkillDeriveParams = new List<SkillDeriveParam>();
                list.Add(param);
            Label_0058:
                param.m_BaseSkillParam = storeya.skillDeriveParam.m_BaseParam;
                param.m_SkillDeriveParams.Add(storeya.skillDeriveParam);
            Label_007A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0018;
                }
                goto Label_0097;
            }
            finally
            {
            Label_008B:
                ((List<SkillDeriveParam>.Enumerator) enumerator).Dispose();
            }
        Label_0097:
            return list;
        }

        public unsafe void Setup(SkillAbilityDeriveData skillAbilityDeriveData)
        {
            List<SkillDeriveParam> list;
            List<AbilityDeriveParam> list2;
            ViewContentSkillParam param;
            List<ViewContentSkillParam>.Enumerator enumerator;
            ViewContentAbilityParam param2;
            List<ViewContentAbilityParam>.Enumerator enumerator2;
            list = skillAbilityDeriveData.GetAvailableSkillDeriveParams();
            list2 = skillAbilityDeriveData.GetAvailableAbilityDeriveParams();
            this.m_ViewContentSkillParams = CreateViewContentSkillParams(list);
            this.m_ViewContentAbilityParams = CreateViewContentAbilityParams(list2);
            enumerator = this.m_ViewContentSkillParams.GetEnumerator();
        Label_0032:
            try
            {
                goto Label_0046;
            Label_0037:
                param = &enumerator.Current;
                this.CreateListItem(param);
            Label_0046:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0037;
                }
                goto Label_0063;
            }
            finally
            {
            Label_0057:
                ((List<ViewContentSkillParam>.Enumerator) enumerator).Dispose();
            }
        Label_0063:
            enumerator2 = this.m_ViewContentAbilityParams.GetEnumerator();
        Label_0070:
            try
            {
                goto Label_0086;
            Label_0075:
                param2 = &enumerator2.Current;
                this.CreateListItem(param2);
            Label_0086:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0075;
                }
                goto Label_00A4;
            }
            finally
            {
            Label_0097:
                ((List<ViewContentAbilityParam>.Enumerator) enumerator2).Dispose();
            }
        Label_00A4:
            this.UpdateUIActive();
            return;
        }

        public unsafe void Setup(SkillAbilityDeriveParam skillAbilityDeriveParam)
        {
            ViewContentSkillParam param;
            List<ViewContentSkillParam>.Enumerator enumerator;
            ViewContentAbilityParam param2;
            List<ViewContentAbilityParam>.Enumerator enumerator2;
            this.m_ViewContentSkillParams = CreateViewContentSkillParams(skillAbilityDeriveParam.SkillDeriveParams);
            this.m_ViewContentAbilityParams = CreateViewContentAbilityParams(skillAbilityDeriveParam.AbilityDeriveParams);
            enumerator = this.m_ViewContentSkillParams.GetEnumerator();
        Label_002E:
            try
            {
                goto Label_0042;
            Label_0033:
                param = &enumerator.Current;
                this.CreateListItem(param);
            Label_0042:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0033;
                }
                goto Label_005F;
            }
            finally
            {
            Label_0053:
                ((List<ViewContentSkillParam>.Enumerator) enumerator).Dispose();
            }
        Label_005F:
            enumerator2 = this.m_ViewContentAbilityParams.GetEnumerator();
        Label_006B:
            try
            {
                goto Label_007F;
            Label_0070:
                param2 = &enumerator2.Current;
                this.CreateListItem(param2);
            Label_007F:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0070;
                }
                goto Label_009C;
            }
            finally
            {
            Label_0090:
                ((List<ViewContentAbilityParam>.Enumerator) enumerator2).Dispose();
            }
        Label_009C:
            this.UpdateUIActive();
            return;
        }

        private void Start()
        {
            GameUtility.SetGameObjectActive(this.m_SkillDeriveListTemplate, 0);
            GameUtility.SetGameObjectActive(this.m_AbilityDeriveListTemplate, 0);
            return;
        }

        private void UpdateUIActive()
        {
            if (this.m_ViewContentAbilityParams.Count >= 1)
            {
                goto Label_001D;
            }
            GameUtility.SetGameObjectActive(this.m_AbilityDeriveListRoot, 0);
        Label_001D:
            if (this.m_ViewContentSkillParams.Count >= 1)
            {
                goto Label_003A;
            }
            GameUtility.SetGameObjectActive(this.m_SkillDeriveListRoot, 0);
        Label_003A:
            return;
        }

        [CompilerGenerated]
        private sealed class <CreateViewContentAbilityParams>c__AnonStorey3A5
        {
            internal AbilityDeriveParam abilityDeriveParam;

            public <CreateViewContentAbilityParams>c__AnonStorey3A5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__411(SkillAbilityDeriveListItem.ViewContentAbilityParam content)
            {
                return (content.m_BaseAbilityParam == this.abilityDeriveParam.m_BaseParam);
            }
        }

        [CompilerGenerated]
        private sealed class <CreateViewContentSkillParams>c__AnonStorey3A6
        {
            internal SkillDeriveParam skillDeriveParam;

            public <CreateViewContentSkillParams>c__AnonStorey3A6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__412(SkillAbilityDeriveListItem.ViewContentSkillParam content)
            {
                return (content.m_BaseSkillParam == this.skillDeriveParam.m_BaseParam);
            }
        }

        private class ViewContentAbilityParam
        {
            public AbilityParam m_BaseAbilityParam;
            public List<AbilityDeriveParam> m_AbilityDeriveParam;

            public ViewContentAbilityParam()
            {
                base..ctor();
                return;
            }
        }

        private class ViewContentSkillParam
        {
            public SkillParam m_BaseSkillParam;
            public List<SkillDeriveParam> m_SkillDeriveParams;

            public ViewContentSkillParam()
            {
                base..ctor();
                return;
            }
        }
    }
}

