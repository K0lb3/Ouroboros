namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    [Pin(0, "ストーリーパート解放準備", 0, 0), Pin(100, "アイテムが選択された", 1, 100), Pin(0x65, "ストーリーパート表示", 1, 0x65)]
    public class QuestSectionList : MonoBehaviour, IFlowInterface
    {
        public ListItemEvents ItemTemplate;
        public GameObject ItemContainer;
        public string WorldMapControllerID;
        public bool RefreshOnStart;
        public ImageArray StoryPartIcon;
        private List<ListItemEvents> mItems;

        public QuestSectionList()
        {
            this.RefreshOnStart = 1;
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0014;
            }
            this.Refresh();
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
        Label_0014:
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            UIQuestSectionData data;
            WorldMapController controller;
            GameManager manager;
            int num;
            data = DataSource.FindDataOfClass<UIQuestSectionData>(go, null);
            GlobalVars.SelectedSection.Set(data.SectionID);
            controller = WorldMapController.FindInstance(this.WorldMapControllerID);
            if ((controller != null) == null)
            {
                goto Label_0084;
            }
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            goto Label_0076;
        Label_003D:
            if ((manager.Chapters[num].section == data.SectionID) == null)
            {
                goto Label_0072;
            }
            controller.GotoArea(manager.Chapters[num].world);
            goto Label_0084;
        Label_0072:
            num += 1;
        Label_0076:
            if (num < ((int) manager.Chapters.Length))
            {
                goto Label_003D;
            }
        Label_0084:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public void Refresh()
        {
            GameManager manager;
            PlayerData data;
            SectionParam[] paramArray;
            List<string> list;
            List<string> list2;
            long num;
            QuestParam[] paramArray2;
            int num2;
            int num3;
            ChapterParam param;
            int num4;
            SectionParam param2;
            ListItemEvents events;
            StringBuilder builder;
            ListItemEvents events2;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            if ((this.ItemTemplate == null) != null)
            {
                goto Label_002D;
            }
            if ((this.ItemContainer == null) == null)
            {
                goto Label_002E;
            }
        Label_002D:
            return;
        Label_002E:
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            paramArray = manager.Sections;
            list = new List<string>();
            list2 = new List<string>();
            num = Network.GetServerTime();
            paramArray2 = data.AvailableQuests;
            num2 = 0;
            goto Label_00C5;
        Label_0066:
            if (string.IsNullOrEmpty(paramArray2[num2].ChapterID) != null)
            {
                goto Label_00BF;
            }
            if (list.Contains(paramArray2[num2].ChapterID) != null)
            {
                goto Label_00BF;
            }
            if (paramArray2[num2].IsMulti != null)
            {
                goto Label_00BF;
            }
            if (paramArray2[num2].IsDateUnlock(num) == null)
            {
                goto Label_00BF;
            }
            list.Add(paramArray2[num2].ChapterID);
        Label_00BF:
            num2 += 1;
        Label_00C5:
            if (num2 < ((int) paramArray2.Length))
            {
                goto Label_0066;
            }
            num3 = 0;
            goto Label_0116;
        Label_00D8:
            param = manager.FindArea(list[num3]);
            if (param == null)
            {
                goto Label_0110;
            }
            if (list2.Contains(param.section) != null)
            {
                goto Label_0110;
            }
            list2.Add(param.section);
        Label_0110:
            num3 += 1;
        Label_0116:
            if (num3 < list.Count)
            {
                goto Label_00D8;
            }
            num4 = 0;
            goto Label_022D;
        Label_012B:
            param2 = paramArray[num4];
            if (paramArray[num4].IsDateUnlock() == null)
            {
                goto Label_0227;
            }
            if (list2.Contains(paramArray[num4].iname) == null)
            {
                goto Label_0227;
            }
            if (param2.storyPart != GlobalVars.SelectedStoryPart.Get())
            {
                goto Label_0227;
            }
            events = null;
            if (string.IsNullOrEmpty(param2.prefabPath) != null)
            {
                goto Label_01AF;
            }
            builder = GameUtility.GetStringBuilder();
            builder.Append("QuestSections/");
            builder.Append(param2.prefabPath);
            events = AssetManager.Load<ListItemEvents>(builder.ToString());
        Label_01AF:
            if ((events == null) == null)
            {
                goto Label_01C4;
            }
            events = this.ItemTemplate;
        Label_01C4:
            events2 = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<UIQuestSectionData>(events2.get_gameObject(), new UIQuestSectionData(paramArray[num4]));
            events2.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            events2.get_gameObject().SetActive(1);
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            this.mItems.Add(events2);
        Label_0227:
            num4 += 1;
        Label_022D:
            if (num4 < ((int) paramArray.Length))
            {
                goto Label_012B;
            }
            if ((this.StoryPartIcon != null) == null)
            {
                goto Label_025F;
            }
            this.StoryPartIcon.ImageIndex = GlobalVars.SelectedStoryPart.Get() - 1;
        Label_025F:
            return;
        }

        private void Start()
        {
            WorldMapController controller;
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
        Label_0022:
            if (this.RefreshOnStart == null)
            {
                goto Label_0033;
            }
            this.Refresh();
        Label_0033:
            controller = WorldMapController.FindInstance(this.WorldMapControllerID);
            if ((controller != null) == null)
            {
                goto Label_0052;
            }
            controller.SectionList = this;
        Label_0052:
            return;
        }
    }
}

