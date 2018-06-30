namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0x65, "選択された", 1, 0x65), Pin(0, "表示", 0, 0), Pin(0x33, "鍵あり", 0, 0x33), AddComponentMenu("Multi/クエスト一覧"), Pin(50, "鍵なし", 0, 50)]
    public class QuestListV2_MultiPlay : MonoBehaviour, IFlowInterface
    {
        [Description("リストアイテムとして使用するゲームオブジェクト")]
        public GameObject ItemTemplate;
        [Description("詳細画面として使用するゲームオブジェクト")]
        public GameObject DetailTemplate;
        private GameObject mDetailInfo;
        public UnityEngine.UI.ScrollRect ScrollRect;

        public QuestListV2_MultiPlay()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0011;
            }
            this.Refresh();
            goto Label_004A;
        Label_0011:
            if (pinID != 50)
            {
                goto Label_002E;
            }
            GlobalVars.EditMultiPlayRoomPassCode = null;
            GameParameter.UpdateValuesOfType(0xe0);
            goto Label_004A;
        Label_002E:
            if (pinID != 0x33)
            {
                goto Label_004A;
            }
            GlobalVars.EditMultiPlayRoomPassCode = "1";
            GameParameter.UpdateValuesOfType(0xe0);
        Label_004A:
            return;
        }

        private unsafe void Awake()
        {
            string str;
            int num;
            GlobalVars.EditMultiPlayRoomPassCode = "0";
            str = FlowNode_Variable.Get("MultiPlayPasscode");
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_003D;
            }
            num = 0;
            if (int.TryParse(str, &num) == null)
            {
                goto Label_003D;
            }
            if (num == null)
            {
                goto Label_003D;
            }
            this.Activated(0x33);
        Label_003D:
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_006A;
            }
            if (this.ItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_006A;
            }
            this.ItemTemplate.SetActive(0);
        Label_006A:
            if ((this.DetailTemplate != null) == null)
            {
                goto Label_0097;
            }
            if (this.DetailTemplate.get_activeInHierarchy() == null)
            {
                goto Label_0097;
            }
            this.DetailTemplate.SetActive(0);
        Label_0097:
            return;
        }

        private void OnCloseItemDetail(GameObject go)
        {
            if ((this.mDetailInfo != null) == null)
            {
                goto Label_0028;
            }
            Object.DestroyImmediate(this.mDetailInfo.get_gameObject());
            this.mDetailInfo = null;
        Label_0028:
            return;
        }

        private void OnOpenItemDetail(GameObject go)
        {
            QuestParam param;
            CanvasGroup group;
            param = DataSource.FindDataOfClass<QuestParam>(go, null);
            group = base.get_gameObject().GetComponentInParent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_002C;
            }
            if (group.get_interactable() != null)
            {
                goto Label_002C;
            }
            return;
        Label_002C:
            if ((this.mDetailInfo == null) == null)
            {
                goto Label_006C;
            }
            if (param == null)
            {
                goto Label_006C;
            }
            this.mDetailInfo = Object.Instantiate<GameObject>(this.DetailTemplate);
            DataSource.Bind<QuestParam>(this.mDetailInfo, param);
            this.mDetailInfo.SetActive(1);
        Label_006C:
            return;
        }

        private void OnSelectItem(GameObject go)
        {
            QuestParam param;
            bool flag;
            <OnSelectItem>c__AnonStorey385 storey;
            storey = new <OnSelectItem>c__AnonStorey385();
            storey.go = go;
            param = DataSource.FindDataOfClass<QuestParam>(storey.go, null);
            if (param == null)
            {
                goto Label_009E;
            }
            if ((QuestDropParam.Instance != null) == null)
            {
                goto Label_009E;
            }
            flag = QuestDropParam.Instance.IsChangedQuestDrops(param);
            GlobalVars.SetDropTableGeneratedTime();
            if (flag == null)
            {
                goto Label_0077;
            }
            if (QuestDropParam.Instance.IsWarningPopupDisable != null)
            {
                goto Label_0077;
            }
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.PARTYEDITOR_DROP_TABLE"), new UIUtility.DialogResultEvent(storey.<>m__3CF), null, 0, -1);
            return;
        Label_0077:
            GlobalVars.SelectedQuestID = param.iname;
            DebugUtility.Log("Select Quest:" + GlobalVars.SelectedQuestID);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_009E:
            return;
        }

        public void Refresh()
        {
            ListExtras extras;
            this.RefreshItems();
            if ((this.ScrollRect != null) == null)
            {
                goto Label_004F;
            }
            extras = this.ScrollRect.GetComponent<ListExtras>();
            if ((extras != null) == null)
            {
                goto Label_003F;
            }
            extras.SetScrollPos(1f);
            goto Label_004F;
        Label_003F:
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        Label_004F:
            return;
        }

        private void RefreshItems()
        {
            Transform transform;
            int num;
            Transform transform2;
            int num2;
            QuestParam param;
            GameObject obj2;
            ListItemEvents events;
            transform = base.get_transform();
            num = transform.get_childCount() - 1;
            goto Label_004D;
        Label_0015:
            transform2 = transform.GetChild(num);
            if ((transform2 == null) == null)
            {
                goto Label_002E;
            }
            goto Label_0049;
        Label_002E:
            if (transform2.get_gameObject().get_activeSelf() == null)
            {
                goto Label_0049;
            }
            Object.DestroyImmediate(transform2.get_gameObject());
        Label_0049:
            num -= 1;
        Label_004D:
            if (num >= 0)
            {
                goto Label_0015;
            }
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0066;
            }
            return;
        Label_0066:
            num2 = 0;
            goto Label_0182;
        Label_006D:
            param = MonoSingleton<GameManager>.Instance.Quests[num2];
            if (param == null)
            {
                goto Label_017E;
            }
            if (param.type == 1)
            {
                goto Label_00A0;
            }
            if (param.IsMultiAreaQuest != null)
            {
                goto Label_00A0;
            }
            goto Label_017E;
        Label_00A0:
            if (param.IsMultiEvent == GlobalVars.SelectedMultiPlayQuestIsEvent)
            {
                goto Label_00B6;
            }
            goto Label_017E;
        Label_00B6:
            if (string.IsNullOrEmpty(param.ChapterID) != null)
            {
                goto Label_017E;
            }
            if (param.ChapterID.Equals(GlobalVars.SelectedMultiPlayArea) != null)
            {
                goto Label_00E2;
            }
            goto Label_017E;
        Label_00E2:
            if (param.IsDateUnlock(-1L) != null)
            {
                goto Label_00F5;
            }
            goto Label_017E;
        Label_00F5:
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            obj2.set_hideFlags(0x34);
            DataSource.Bind<QuestParam>(obj2, param);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0163;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenItemDetail);
            events.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseItemDetail);
        Label_0163:
            obj2.get_transform().SetParent(transform, 0);
            obj2.get_gameObject().SetActive(1);
        Label_017E:
            num2 += 1;
        Label_0182:
            if (num2 < ((int) MonoSingleton<GameManager>.Instance.Quests.Length))
            {
                goto Label_006D;
            }
            return;
        }

        private void Start()
        {
            this.RefreshItems();
            return;
        }

        [CompilerGenerated]
        private sealed class <OnSelectItem>c__AnonStorey385
        {
            internal GameObject go;

            public <OnSelectItem>c__AnonStorey385()
            {
                base..ctor();
                return;
            }

            internal void <>m__3CF(GameObject obj)
            {
                ListItemEvents events;
                events = this.go.GetComponent<ListItemEvents>();
                if ((events != null) == null)
                {
                    goto Label_001E;
                }
                events.OpenDetail();
            Label_001E:
                return;
            }
        }
    }
}

