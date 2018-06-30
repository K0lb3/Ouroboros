namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "更新", 0, 0), Pin(10, "クエストが選択された", 1, 0x65)]
    public class ReplayQuestList : SRPG_ListBase, IFlowInterface
    {
        private const int PIN_ID_REFRESH = 0;
        private const int PIN_ID_SELECT = 10;
        public GameObject ItemTemplate;
        public GameObject ItemContainer;
        public Dictionary<string, GameObject> mListItemTemplates;
        public bool Descending;
        public bool RefreshOnStart;
        public bool ShowAllQuests;
        public UnityEngine.UI.ScrollRect ScrollRect;
        private List<QuestParam> mQuests;
        private string chapterName;
        [CompilerGenerated]
        private static Action<GameObject, bool> <>f__am$cache9;

        public ReplayQuestList()
        {
            this.mListItemTemplates = new Dictionary<string, GameObject>();
            this.Descending = 1;
            this.RefreshOnStart = 1;
            this.ShowAllQuests = 1;
            this.mQuests = new List<QuestParam>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__3F2(GameObject lie, bool value)
        {
            if ((lie != null) == null)
            {
                goto Label_001E;
            }
            if (lie.get_activeInHierarchy() == null)
            {
                goto Label_001E;
            }
            lie.SetActive(value);
        Label_001E:
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

        private bool CheckQuest(QuestParam quest)
        {
            if (quest.IsMulti == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (quest.IsReplayDateUnlock(-1L) != null)
            {
                goto Label_001C;
            }
            return 0;
        Label_001C:
            if (string.IsNullOrEmpty(quest.event_start) == null)
            {
                goto Label_003E;
            }
            if (string.IsNullOrEmpty(quest.event_clear) == null)
            {
                goto Label_003E;
            }
            return 0;
        Label_003E:
            if (quest.state == 1)
            {
                goto Label_0058;
            }
            if (quest.state == 2)
            {
                goto Label_0058;
            }
            return 0;
        Label_0058:
            if (quest.state != 1)
            {
                goto Label_0076;
            }
            if (string.IsNullOrEmpty(quest.event_start) == null)
            {
                goto Label_0076;
            }
            return 0;
        Label_0076:
            if (this.ShowAllQuests != null)
            {
                goto Label_009D;
            }
            if ((GlobalVars.ReplaySelectedChapter != quest.ChapterID) == null)
            {
                goto Label_009D;
            }
            return 0;
        Label_009D:
            return 1;
        }

        private GameObject LoadQuestListItem(QuestParam param)
        {
            GameObject obj2;
            if (string.IsNullOrEmpty(param.ItemLayout) == null)
            {
                goto Label_0012;
            }
            return null;
        Label_0012:
            if (this.mListItemTemplates.ContainsKey(param.ItemLayout) == null)
            {
                goto Label_003A;
            }
            return this.mListItemTemplates[param.ItemLayout];
        Label_003A:
            obj2 = AssetManager.Load<GameObject>("QuestListItems/" + param.ItemLayout);
            if ((obj2 != null) == null)
            {
                goto Label_006E;
            }
            this.mListItemTemplates.Add(param.ItemLayout, obj2);
        Label_006E:
            return obj2;
        }

        private void OnSelectItem(GameObject go)
        {
            QuestParam param;
            param = DataSource.FindDataOfClass<QuestParam>(go, null);
            if (param == null)
            {
                goto Label_0026;
            }
            GlobalVars.ReplaySelectedQuestID.Set(param.iname);
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_0026:
            return;
        }

        public void Refresh()
        {
            if (this.chapterName != null)
            {
                goto Label_0020;
            }
            this.chapterName = GlobalVars.ReplaySelectedChapter;
            goto Label_003B;
        Label_0020:
            if (this.chapterName.Equals(GlobalVars.ReplaySelectedChapter) == null)
            {
                goto Label_003B;
            }
            return;
        Label_003B:
            this.chapterName = GlobalVars.ReplaySelectedChapter;
            this.RefreshQuests();
            this.RefreshItems();
            if ((this.ScrollRect != null) == null)
            {
                goto Label_0078;
            }
            this.ScrollRect.set_normalizedPosition(Vector2.get_one());
        Label_0078:
            return;
        }

        private void RefreshItems()
        {
            QuestParam[] paramArray;
            int num;
            QuestParam param;
            GameObject obj2;
            GameObject obj3;
            ListItemEvents events;
            base.ClearItems();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            paramArray = this.mQuests.ToArray();
            if (this.Descending == null)
            {
                goto Label_0035;
            }
            Array.Reverse(paramArray);
        Label_0035:
            num = 0;
            goto Label_00FE;
        Label_003C:
            param = paramArray[num];
            obj2 = null;
            if (string.IsNullOrEmpty(param.ItemLayout) != null)
            {
                goto Label_005A;
            }
            obj2 = this.LoadQuestListItem(param);
        Label_005A:
            if ((obj2 == null) == null)
            {
                goto Label_006D;
            }
            obj2 = this.ItemTemplate;
        Label_006D:
            if ((obj2 == null) == null)
            {
                goto Label_007E;
            }
            goto Label_00FA;
        Label_007E:
            obj3 = Object.Instantiate<GameObject>(obj2);
            obj3.set_hideFlags(0x34);
            DataSource.Bind<QuestParam>(obj3, param);
            events = obj3.GetComponent<ListItemEvents>();
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelectItem);
            if (param.type != 5)
            {
                goto Label_00CD;
            }
            this.SetQuestTimerActive(obj3.get_transform(), 0);
        Label_00CD:
            obj3.get_transform().SetParent(this.ItemContainer.get_transform(), 0);
            obj3.get_gameObject().SetActive(1);
            base.AddItem(events);
        Label_00FA:
            num += 1;
        Label_00FE:
            if (num < ((int) paramArray.Length))
            {
                goto Label_003C;
            }
            return;
        }

        private void RefreshQuests()
        {
            QuestParam[] paramArray;
            QuestParam param;
            QuestParam[] paramArray2;
            int num;
            this.mQuests.Clear();
            paramArray2 = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            num = 0;
            goto Label_0044;
        Label_0024:
            param = paramArray2[num];
            if (this.CheckQuest(param) == null)
            {
                goto Label_0040;
            }
            this.mQuests.Add(param);
        Label_0040:
            num += 1;
        Label_0044:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0024;
            }
            return;
        }

        private void SetQuestTimerActive(Transform obj, bool value)
        {
            QuestTimeLimit limit;
            Transform transform;
            Transform transform2;
            if ((obj == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            limit = obj.GetComponent<QuestTimeLimit>();
            if ((limit != null) == null)
            {
                goto Label_0027;
            }
            limit.set_enabled(value);
        Label_0027:
            transform = obj.FindChild("bg");
            if ((transform != null) == null)
            {
                goto Label_0063;
            }
            transform2 = transform.FindChild("timer_base");
            if ((transform2 != null) == null)
            {
                goto Label_0063;
            }
            transform2.get_gameObject().SetActive(value);
        Label_0063:
            return;
        }

        protected override void Start()
        {
            Action<GameObject, bool> action;
            base.Start();
            if (<>f__am$cache9 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache9 = new Action<GameObject, bool>(ReplayQuestList.<Start>m__3F2);
        Label_001E:
            action = <>f__am$cache9;
            action(this.ItemTemplate, 0);
            this.Refresh();
            return;
        }
    }
}

