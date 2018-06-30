namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "クエスト選択", 1, 100), Pin(1, "Refresh", 0, 1)]
    public class QuestBookmarkKakeraWindow : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private RectTransform QuestListParent;
        [SerializeField]
        private GameObject QuestListItemTemplate;
        private List<GameObject> mGainedQuests;

        public QuestBookmarkKakeraWindow()
        {
            this.mGainedQuests = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0012;
            }
            GameParameter.UpdateAll(base.get_gameObject());
        Label_0012:
            return;
        }

        private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
        {
            GameObject obj2;
            SRPG_Button button;
            Button button2;
            bool flag;
            bool flag2;
            <AddPanel>c__AnonStorey378 storey;
            storey = new <AddPanel>c__AnonStorey378();
            storey.questparam = questparam;
            if (storey.questparam != null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            if (storey.questparam.IsMulti == null)
            {
                goto Label_002E;
            }
            return;
        Label_002E:
            obj2 = Object.Instantiate<GameObject>(this.QuestListItemTemplate);
            button = obj2.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_005F;
            }
            button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
        Label_005F:
            this.mGainedQuests.Add(obj2);
            button2 = obj2.GetComponent<Button>();
            if ((button2 != null) == null)
            {
                goto Label_00B9;
            }
            flag = storey.questparam.IsDateUnlock(-1L);
            flag2 = (Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(storey.<>m__3B6)) == null) == 0;
            button2.set_interactable((flag == null) ? 0 : flag2);
        Label_00B9:
            DataSource.Bind<QuestParam>(obj2, storey.questparam);
            obj2.get_transform().SetParent(this.QuestListParent, 0);
            obj2.SetActive(1);
            return;
        }

        private void Awake()
        {
            if ((this.QuestListItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.QuestListItemTemplate.SetActive(0);
        Label_001D:
            return;
        }

        private void OnQuestSelect(SRPG_Button button)
        {
            int num;
            bool flag;
            QuestParam[] paramArray;
            bool flag2;
            <OnQuestSelect>c__AnonStorey379 storey;
            storey = new <OnQuestSelect>c__AnonStorey379();
            num = this.mGainedQuests.IndexOf(button.get_gameObject());
            storey.quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[num], null);
            if (storey.quest == null)
            {
                goto Label_00C8;
            }
            if (storey.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_0069;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_0069:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(storey.<>m__3B7)) == null) == 0) != null)
            {
                goto Label_00AF;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_00AF:
            GlobalVars.SelectedQuestID = storey.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_00C8:
            return;
        }

        public void Refresh(UnitParam unit, IEnumerable<QuestParam> quests)
        {
            if (unit == null)
            {
                goto Label_000C;
            }
            if (quests != null)
            {
                goto Label_000D;
            }
        Label_000C:
            return;
        Label_000D:
            DataSource.Bind<UnitParam>(base.get_gameObject(), unit);
            this.RefreshGainedQuests(unit, quests);
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private void RefreshGainedQuests(UnitParam unit, IEnumerable<QuestParam> quests)
        {
            QuestParam[] paramArray;
            QuestParam param;
            IEnumerator<QuestParam> enumerator;
            if ((this.QuestListItemTemplate == null) != null)
            {
                goto Label_0028;
            }
            if ((this.QuestListParent == null) != null)
            {
                goto Label_0028;
            }
            if (unit != null)
            {
                goto Label_0029;
            }
        Label_0028:
            return;
        Label_0029:
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_007F;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            enumerator = quests.GetEnumerator();
        Label_0050:
            try
            {
                goto Label_0064;
            Label_0055:
                param = enumerator.Current;
                this.AddPanel(param, paramArray);
            Label_0064:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0055;
                }
                goto Label_007F;
            }
            finally
            {
            Label_0074:
                if (enumerator != null)
                {
                    goto Label_0078;
                }
            Label_0078:
                enumerator.Dispose();
            }
        Label_007F:
            return;
        }

        [CompilerGenerated]
        private sealed class <AddPanel>c__AnonStorey378
        {
            internal QuestParam questparam;

            public <AddPanel>c__AnonStorey378()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3B6(QuestParam p)
            {
                return (p == this.questparam);
            }
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey379
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey379()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3B7(QuestParam p)
            {
                return (p == this.quest);
            }
        }
    }
}

