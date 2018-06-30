namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(12, "アイテムが選択された", 1, 0x66), Pin(10, "閲覧可能なストーリーがある", 1, 100), Pin(0, "更新", 0, 0), Pin(11, "閲覧可能なストーリーがない", 1, 0x65)]
    public class ReplayQuestSectionList : MonoBehaviour, IFlowInterface
    {
        private const int PIN_ID_REFRESH = 0;
        private const int PIN_ID_EXIST = 10;
        private const int PIN_ID_NOT_EXIST = 11;
        private const int PIN_ID_SELECT = 12;
        public ListItemEvents ItemTemplate;
        public GameObject ItemContainer;
        private bool isRefreshed;
        public UnityEngine.UI.ScrollRect ScrollRect;
        private List<ListItemEvents> mItems;

        public ReplayQuestSectionList()
        {
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            UIQuestSectionData data;
            data = DataSource.FindDataOfClass<UIQuestSectionData>(go, null);
            if (data == null)
            {
                goto Label_0026;
            }
            GlobalVars.ReplaySelectedSection.Set(data.SectionID);
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
        Label_0026:
            return;
        }

        private void Refresh()
        {
            GameManager manager;
            SectionParam[] paramArray;
            List<string> list;
            List<string> list2;
            long num;
            QuestParam[] paramArray2;
            QuestParam param;
            QuestParam[] paramArray3;
            int num2;
            int num3;
            ChapterParam param2;
            int num4;
            SectionParam param3;
            ListItemEvents events;
            StringBuilder builder;
            ListItemEvents events2;
            if (this.isRefreshed == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.isRefreshed = 1;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            this.mItems.Clear();
            if ((this.ItemTemplate == null) != null)
            {
                goto Label_004B;
            }
            if ((this.ItemContainer == null) == null)
            {
                goto Label_004C;
            }
        Label_004B:
            return;
        Label_004C:
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Sections;
            list = new List<string>();
            list2 = new List<string>();
            num = Network.GetServerTime();
            paramArray3 = manager.Player.AvailableQuests;
            num2 = 0;
            goto Label_0136;
        Label_0085:
            param = paramArray3[num2];
            if (string.IsNullOrEmpty(param.ChapterID) == null)
            {
                goto Label_00A2;
            }
            goto Label_0130;
        Label_00A2:
            if (param.IsMulti == null)
            {
                goto Label_00B3;
            }
            goto Label_0130;
        Label_00B3:
            if (param.IsReplayDateUnlock(num) != null)
            {
                goto Label_00C6;
            }
            goto Label_0130;
        Label_00C6:
            if (list.Contains(param.ChapterID) == null)
            {
                goto Label_00DD;
            }
            goto Label_0130;
        Label_00DD:
            if (string.IsNullOrEmpty(param.event_start) == null)
            {
                goto Label_0104;
            }
            if (string.IsNullOrEmpty(param.event_clear) == null)
            {
                goto Label_0104;
            }
            goto Label_0130;
        Label_0104:
            if (param.state == 1)
            {
                goto Label_0123;
            }
            if (param.state == 2)
            {
                goto Label_0123;
            }
            goto Label_0130;
        Label_0123:
            list.Add(param.ChapterID);
        Label_0130:
            num2 += 1;
        Label_0136:
            if (num2 < ((int) paramArray3.Length))
            {
                goto Label_0085;
            }
            num3 = 0;
            goto Label_01B1;
        Label_0149:
            param2 = manager.FindArea(list[num3]);
            if (param2 == null)
            {
                goto Label_01AB;
            }
            if (list2.Contains(param2.section) != null)
            {
                goto Label_01AB;
            }
            if ((param2.section != "WD_DAILY") == null)
            {
                goto Label_01AB;
            }
            if ((param2.section != "WD_CHARA") == null)
            {
                goto Label_01AB;
            }
            list2.Add(param2.section);
        Label_01AB:
            num3 += 1;
        Label_01B1:
            if (num3 < list.Count)
            {
                goto Label_0149;
            }
            num4 = 0;
            goto Label_02A3;
        Label_01C6:
            param3 = paramArray[num4];
            if (list2.Contains(paramArray[num4].iname) == null)
            {
                goto Label_029D;
            }
            events = null;
            if (string.IsNullOrEmpty(param3.prefabPath) != null)
            {
                goto Label_0225;
            }
            builder = GameUtility.GetStringBuilder();
            builder.Append("QuestSections/");
            builder.Append(param3.prefabPath);
            events = AssetManager.Load<ListItemEvents>(builder.ToString());
        Label_0225:
            if ((events == null) == null)
            {
                goto Label_023A;
            }
            events = this.ItemTemplate;
        Label_023A:
            events2 = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<UIQuestSectionData>(events2.get_gameObject(), new UIQuestSectionData(paramArray[num4]));
            events2.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            events2.get_gameObject().SetActive(1);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            this.mItems.Add(events2);
        Label_029D:
            num4 += 1;
        Label_02A3:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_01C6;
            }
            if ((this.ScrollRect != null) == null)
            {
                goto Label_02CE;
            }
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        Label_02CE:
            if (this.mItems.Count <= 0)
            {
                goto Label_02EC;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_02F4;
        Label_02EC:
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
        Label_02F4:
            return;
        }

        private void Start()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
        Label_0022:
            this.Refresh();
            return;
        }
    }
}

