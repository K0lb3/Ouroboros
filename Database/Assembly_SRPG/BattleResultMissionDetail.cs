namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class BattleResultMissionDetail : MonoBehaviour
    {
        [SerializeField]
        private QuestMissionItem ItemTemplate;
        [SerializeField]
        private QuestMissionItem UnitTemplate;
        [SerializeField]
        private QuestMissionItem ArtifactTemplate;
        [SerializeField]
        private QuestMissionItem ConceptCardTemplate;
        [SerializeField]
        private QuestMissionItem NothingRewardTemplate;
        [SerializeField]
        private GameObject ContentsParent;
        [SerializeField]
        private UnityEngine.UI.ScrollRect ScrollRect;
        [SerializeField]
        private VerticalLayoutGroup VerticalLayout;
        private static readonly float WaitTime;
        private List<GameObject> allStarObjects;
        private Coroutine lastCoroutine;
        private float m_ItemHeight;

        static BattleResultMissionDetail()
        {
            WaitTime = 0.2f;
            return;
        }

        public BattleResultMissionDetail()
        {
            this.allStarObjects = new List<GameObject>();
            base..ctor();
            return;
        }

        private void Awake()
        {
            QuestParam param;
            param = DataSource.FindDataOfClass<QuestParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_003D;
            }
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_003D;
            }
            param = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID);
        Label_003D:
            if (param != null)
            {
                goto Label_0044;
            }
            return;
        Label_0044:
            this.RefreshQuestMissionReward(param);
            return;
        }

        public List<GameObject> GetObjectiveStars()
        {
            return this.allStarObjects;
        }

        [DebuggerHidden]
        private IEnumerator MoveScrollCoroutine(int index)
        {
            <MoveScrollCoroutine>c__IteratorE9 re;
            re = new <MoveScrollCoroutine>c__IteratorE9();
            re.index = index;
            re.<$>index = index;
            re.<>f__this = this;
            return re;
        }

        public float MoveTo(int index)
        {
            if ((this.ScrollRect == null) == null)
            {
                goto Label_0017;
            }
            return 0f;
        Label_0017:
            if (this.lastCoroutine == null)
            {
                goto Label_002E;
            }
            base.StopCoroutine(this.lastCoroutine);
        Label_002E:
            this.lastCoroutine = base.StartCoroutine(this.MoveScrollCoroutine(index));
            return WaitTime;
        }

        private unsafe void RefreshQuestMissionReward(QuestParam questParam)
        {
            int num;
            QuestMissionItem item;
            QuestBonusObjective objective;
            ConceptCardIcon icon;
            ConceptCardData data;
            ItemParam param;
            Rect rect;
            if (questParam.bonusObjective != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_0244;
        Label_0013:
            item = null;
            objective = questParam.bonusObjective[num];
            if (objective.itemType != 3)
            {
                goto Label_0045;
            }
            item = Object.Instantiate<GameObject>(this.ArtifactTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
            goto Label_011E;
        Label_0045:
            if (objective.itemType != 6)
            {
                goto Label_0099;
            }
            item = Object.Instantiate<GameObject>(this.ConceptCardTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
            icon = item.get_gameObject().GetComponent<ConceptCardIcon>();
            if ((icon != null) == null)
            {
                goto Label_011E;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(objective.item);
            icon.Setup(data);
            goto Label_011E;
        Label_0099:
            if (objective.itemType != 100)
            {
                goto Label_00C1;
            }
            item = Object.Instantiate<GameObject>(this.NothingRewardTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
            goto Label_011E;
        Label_00C1:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(objective.item);
            if (param != null)
            {
                goto Label_00DF;
            }
            goto Label_0240;
        Label_00DF:
            if (param.type != 0x10)
            {
                goto Label_0108;
            }
            item = Object.Instantiate<GameObject>(this.UnitTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
            goto Label_011E;
        Label_0108:
            item = Object.Instantiate<GameObject>(this.ItemTemplate.get_gameObject()).GetComponent<QuestMissionItem>();
        Label_011E:
            if ((item == null) == null)
            {
                goto Label_012F;
            }
            goto Label_0240;
        Label_012F:
            if ((item.Star != null) == null)
            {
                goto Label_014C;
            }
            item.Star.Index = num;
        Label_014C:
            if ((item.FrameParam != null) == null)
            {
                goto Label_0169;
            }
            item.FrameParam.Index = num;
        Label_0169:
            if ((item.IconParam != null) == null)
            {
                goto Label_0186;
            }
            item.IconParam.Index = num;
        Label_0186:
            if ((item.NameParam != null) == null)
            {
                goto Label_01A3;
            }
            item.NameParam.Index = num;
        Label_01A3:
            if ((item.AmountParam != null) == null)
            {
                goto Label_01C0;
            }
            item.AmountParam.Index = num;
        Label_01C0:
            if ((item.ObjectigveParam != null) == null)
            {
                goto Label_01DD;
            }
            item.ObjectigveParam.Index = num;
        Label_01DD:
            this.m_ItemHeight = &(item.get_transform() as RectTransform).get_rect().get_height();
            item.get_gameObject().SetActive(1);
            item.get_transform().SetParent(this.ContentsParent.get_transform(), 0);
            this.allStarObjects.Add(item.Star.get_gameObject());
            GameParameter.UpdateAll(item.get_gameObject());
        Label_0240:
            num += 1;
        Label_0244:
            if (num < ((int) questParam.bonusObjective.Length))
            {
                goto Label_0013;
            }
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0283;
            }
            this.ScrollRect.set_verticalNormalizedPosition(1f);
            this.ScrollRect.set_horizontalNormalizedPosition(1f);
        Label_0283:
            return;
        }

        [CompilerGenerated]
        private sealed class <MoveScrollCoroutine>c__IteratorE9 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <startPos>__0;
            internal float <contentHeight>__1;
            internal float <margin>__2;
            internal int index;
            internal float <endPos>__3;
            internal float <startTime>__4;
            internal float <diff>__5;
            internal int $PC;
            internal object $current;
            internal int <$>index;
            internal BattleResultMissionDetail <>f__this;

            public <MoveScrollCoroutine>c__IteratorE9()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                Rect rect;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0198;
                }
                goto Label_01BA;
            Label_0021:
                if ((this.<>f__this.ScrollRect == null) == null)
                {
                    goto Label_003C;
                }
                goto Label_01BA;
            Label_003C:
                this.<startPos>__0 = this.<>f__this.ScrollRect.get_verticalNormalizedPosition();
                this.<contentHeight>__1 = &(this.<>f__this.ScrollRect.get_content().get_transform() as RectTransform).get_rect().get_height();
                this.<margin>__2 = 0f;
                if ((this.<>f__this.VerticalLayout != null) == null)
                {
                    goto Label_00E3;
                }
                this.<margin>__2 = ((float) (this.<>f__this.VerticalLayout.get_padding().get_top() + this.<>f__this.VerticalLayout.get_padding().get_bottom())) + this.<>f__this.VerticalLayout.get_spacing();
            Label_00E3:
                this.<endPos>__3 = 1f - (((this.<>f__this.m_ItemHeight + this.<margin>__2) * ((float) this.index)) / this.<contentHeight>__1);
                this.<endPos>__3 = Mathf.Max(0f, this.<endPos>__3);
                this.<startTime>__4 = Time.get_timeSinceLevelLoad();
            Label_0131:
                this.<diff>__5 = Time.get_timeSinceLevelLoad() - this.<startTime>__4;
                if (this.<diff>__5 < BattleResultMissionDetail.WaitTime)
                {
                    goto Label_0158;
                }
                goto Label_019D;
            Label_0158:
                this.<>f__this.ScrollRect.set_verticalNormalizedPosition(Mathf.Lerp(this.<startPos>__0, this.<endPos>__3, this.<diff>__5 / BattleResultMissionDetail.WaitTime));
                this.$current = null;
                this.$PC = 1;
                goto Label_01BC;
            Label_0198:
                goto Label_0131;
            Label_019D:
                this.<>f__this.ScrollRect.set_verticalNormalizedPosition(this.<endPos>__3);
                this.$PC = -1;
            Label_01BA:
                return 0;
            Label_01BC:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }
    }
}

