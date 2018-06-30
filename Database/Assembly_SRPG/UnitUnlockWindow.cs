namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "Refresh", 0, 1), Pin(100, "Unlock", 1, 100), Pin(0x65, "Selected Quest", 1, 0x65)]
    public class UnitUnlockWindow : MonoBehaviour, IFlowInterface
    {
        public GameObject QuestList;
        public RectTransform QuestListParent;
        public GameObject QuestListItemTemplate;
        public Text TxtTitle;
        public Text TxtComment;
        public Text TxtQuestNothing;
        public GameObject GOUnlockLimit;
        public Button BtnDecide;
        public Button BtnCancel;
        private UnitParam UnlockUnit;
        private List<GameObject> mGainedQuests;

        public UnitUnlockWindow()
        {
            this.mGainedQuests = new List<GameObject>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_000D;
            }
            this.Refresh();
        Label_000D:
            return;
        }

        private void AddPanel(QuestParam questparam, QuestParam[] availableQuests)
        {
            GameObject obj2;
            SRPG_Button button;
            Button button2;
            bool flag;
            bool flag2;
            <AddPanel>c__AnonStorey3E4 storeye;
            storeye = new <AddPanel>c__AnonStorey3E4();
            storeye.questparam = questparam;
            this.QuestList.SetActive(1);
            if (storeye.questparam != null)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            if (storeye.questparam.IsMulti == null)
            {
                goto Label_003A;
            }
            return;
        Label_003A:
            obj2 = Object.Instantiate<GameObject>(this.QuestListItemTemplate);
            button = obj2.GetComponent<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_006B;
            }
            button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
        Label_006B:
            this.mGainedQuests.Add(obj2);
            button2 = obj2.GetComponent<Button>();
            if ((button2 != null) == null)
            {
                goto Label_00C5;
            }
            flag = storeye.questparam.IsDateUnlock(-1L);
            flag2 = (Array.Find<QuestParam>(availableQuests, new Predicate<QuestParam>(storeye.<>m__48F)) == null) == 0;
            button2.set_interactable((flag == null) ? 0 : flag2);
        Label_00C5:
            DataSource.Bind<QuestParam>(obj2, storeye.questparam);
            obj2.get_transform().SetParent(this.QuestListParent, 0);
            obj2.SetActive(1);
            return;
        }

        public void ClearPanel()
        {
            int num;
            GameObject obj2;
            this.mGainedQuests.Clear();
            num = 0;
            goto Label_003F;
        Label_0012:
            obj2 = this.QuestListParent.GetChild(num).get_gameObject();
            if ((this.QuestListItemTemplate != obj2) == null)
            {
                goto Label_003B;
            }
            Object.Destroy(obj2);
        Label_003B:
            num += 1;
        Label_003F:
            if (num < this.QuestListParent.get_childCount())
            {
                goto Label_0012;
            }
            return;
        }

        private void OnQuestSelect(SRPG_Button button)
        {
            int num;
            bool flag;
            QuestParam[] paramArray;
            bool flag2;
            <OnQuestSelect>c__AnonStorey3E5 storeye;
            storeye = new <OnQuestSelect>c__AnonStorey3E5();
            num = this.mGainedQuests.IndexOf(button.get_gameObject());
            storeye.quest = DataSource.FindDataOfClass<QuestParam>(this.mGainedQuests[num], null);
            if (storeye.quest == null)
            {
                goto Label_00C8;
            }
            if (storeye.quest.IsDateUnlock(-1L) != null)
            {
                goto Label_0069;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), null, null, 0, -1);
            return;
        Label_0069:
            if (((Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Player.AvailableQuests, new Predicate<QuestParam>(storeye.<>m__490)) == null) == 0) != null)
            {
                goto Label_00AF;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), null, null, 0, -1);
            return;
        Label_00AF:
            GlobalVars.SelectedQuestID = storeye.quest.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_00C8:
            return;
        }

        private void Refresh()
        {
            string str;
            UnitData data;
            bool flag;
            int num;
            int num2;
            UnitUnlockTimeParam param;
            str = GlobalVars.UnlockUnitID;
            this.UnlockUnit = MonoSingleton<GameManager>.Instance.GetUnitParam(str);
            DataSource.Bind<UnitParam>(base.get_gameObject(), this.UnlockUnit);
            data = MonoSingleton<GameManager>.GetInstanceDirect().Player.FindUnitDataByUnitID(str);
            if (data == null)
            {
                goto Label_004B;
            }
            DataSource.Bind<UnitData>(base.get_gameObject(), data);
        Label_004B:
            flag = 0;
            if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(this.UnlockUnit) != null)
            {
                goto Label_0098;
            }
            num = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.UnlockUnit.piece);
            num2 = this.UnlockUnit.GetUnlockNeedPieces();
            flag = (num < num2) == 0;
        Label_0098:
            if ((this.QuestList != null) == null)
            {
                goto Label_00B8;
            }
            this.QuestList.SetActive(flag == 0);
        Label_00B8:
            if ((this.BtnDecide != null) == null)
            {
                goto Label_00DA;
            }
            this.BtnDecide.get_gameObject().SetActive(flag);
        Label_00DA:
            if ((this.BtnCancel != null) == null)
            {
                goto Label_00FC;
            }
            this.BtnCancel.get_gameObject().SetActive(flag);
        Label_00FC:
            if (flag == null)
            {
                goto Label_0164;
            }
            if ((this.TxtTitle != null) == null)
            {
                goto Label_0128;
            }
            this.TxtTitle.set_text(LocalizedText.Get("sys.UNIT_UNLOCK_TITLE"));
        Label_0128:
            if ((this.TxtComment != null) == null)
            {
                goto Label_021A;
            }
            this.TxtComment.set_text(LocalizedText.Get("sys.UNIT_UNLOCK_COMMENT"));
            this.TxtComment.get_gameObject().SetActive(1);
            goto Label_021A;
        Label_0164:
            if ((this.TxtTitle != null) == null)
            {
                goto Label_018A;
            }
            this.TxtTitle.set_text(LocalizedText.Get("sys.UNIT_GAINED_QUEST_TITLE"));
        Label_018A:
            if ((this.TxtComment != null) == null)
            {
                goto Label_01CE;
            }
            this.TxtComment.set_text(LocalizedText.Get("sys.UNIT_GAINED_COMMENT"));
            this.TxtComment.get_gameObject().SetActive(this.mGainedQuests.Count == 0);
        Label_01CE:
            if ((this.GOUnlockLimit != null) == null)
            {
                goto Label_020E;
            }
            if (MonoSingleton<GameManager>.Instance.MasterParam.GetUnitUnlockTimeParam(this.UnlockUnit.unlock_time) == null)
            {
                goto Label_020E;
            }
            this.GOUnlockLimit.SetActive(1);
        Label_020E:
            this.RefreshGainedQuests(this.UnlockUnit);
        Label_021A:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        private unsafe void RefreshGainedQuests(UnitParam unit)
        {
            ItemParam param;
            QuestParam[] paramArray;
            List<QuestParam> list;
            QuestParam param2;
            List<QuestParam>.Enumerator enumerator;
            this.ClearPanel();
            this.QuestList.SetActive(0);
            if ((this.QuestListItemTemplate == null) != null)
            {
                goto Label_0034;
            }
            if ((this.QuestListParent == null) == null)
            {
                goto Label_0035;
            }
        Label_0034:
            return;
        Label_0035:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(this.UnlockUnit.piece);
            DataSource.Bind<ItemParam>(this.QuestList, param);
            this.QuestList.SetActive(1);
            this.SetScrollTop();
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_0102;
            }
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            list = QuestDropParam.Instance.GetItemDropQuestList(param, GlobalVars.GetDropTableGeneratedDateTime());
            enumerator = list.GetEnumerator();
        Label_00A2:
            try
            {
                goto Label_00B7;
            Label_00A7:
                param2 = &enumerator.Current;
                this.AddPanel(param2, paramArray);
            Label_00B7:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00A7;
                }
                goto Label_00D5;
            }
            finally
            {
            Label_00C8:
                ((List<QuestParam>.Enumerator) enumerator).Dispose();
            }
        Label_00D5:
            if (list.Count != null)
            {
                goto Label_0102;
            }
            if ((this.TxtQuestNothing != null) == null)
            {
                goto Label_0102;
            }
            this.TxtQuestNothing.get_gameObject().SetActive(1);
        Label_0102:
            return;
        }

        private unsafe void SetScrollTop()
        {
            RectTransform transform;
            Vector2 vector;
            transform = this.QuestListParent.GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_0032;
            }
            vector = transform.get_anchoredPosition();
            &vector.y = 0f;
            transform.set_anchoredPosition(vector);
        Label_0032:
            return;
        }

        private void Start()
        {
            if ((this.QuestListItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.QuestListItemTemplate.SetActive(0);
        Label_001D:
            this.Refresh();
            return;
        }

        [CompilerGenerated]
        private sealed class <AddPanel>c__AnonStorey3E4
        {
            internal QuestParam questparam;

            public <AddPanel>c__AnonStorey3E4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__48F(QuestParam p)
            {
                return (p == this.questparam);
            }
        }

        [CompilerGenerated]
        private sealed class <OnQuestSelect>c__AnonStorey3E5
        {
            internal QuestParam quest;

            public <OnQuestSelect>c__AnonStorey3E5()
            {
                base..ctor();
                return;
            }

            internal bool <>m__490(QuestParam p)
            {
                return (p == this.quest);
            }
        }
    }
}

