namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(2, "キャンセル", 0, 1), Pin(0x65, "戻る", 1, 0x65), Pin(100, "アンロック", 1, 100), NodeType("System/EventQuestPopup", 0x7fe5), Pin(1, "決定", 0, 0)]
    public class EventQuestPopup : MonoBehaviour, IFlowInterface
    {
        public GameObject ItemLayout;
        public GameObject ItemTemplate;
        public Text Message;
        public SRPG_Button BtnDecide;
        public SRPG_Button BtnCancel;
        private ChapterParam Chapter;

        public EventQuestPopup()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Activated>m__305(GameObject go)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        [CompilerGenerated]
        private void <Activated>m__306(GameObject go)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        public void Activated(int pinID)
        {
            KeyItem item;
            ItemData data;
            if (pinID != 1)
            {
                goto Label_00DF;
            }
            if (this.Chapter == null)
            {
                goto Label_00D0;
            }
            if (this.Chapter.keys.Count <= 0)
            {
                goto Label_00D0;
            }
            item = this.Chapter.keys[0];
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(item.iname);
            if (data == null)
            {
                goto Label_0067;
            }
            if (data.Num >= item.num)
            {
                goto Label_0091;
            }
        Label_0067:
            UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.KEYQUEST_OUTOFKEY"), new UIUtility.DialogResultEvent(this.<Activated>m__305), null, 0, -1);
            return;
        Label_0091:
            if (this.Chapter.IsDateUnlock(Network.GetServerTime()) != null)
            {
                goto Label_00D0;
            }
            UIUtility.SystemMessage(LocalizedText.Get("sys.KEYQUEST_UNLOCK"), LocalizedText.Get("sys.QUEST_OUT_OF_DATE"), new UIUtility.DialogResultEvent(this.<Activated>m__306), null, 0, -1);
            return;
        Label_00D0:
            GlobalVars.KeyQuestTimeOver = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        Label_00DF:
            if (pinID != 2)
            {
                goto Label_00F5;
            }
            GlobalVars.KeyQuestTimeOver = 0;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        Label_00F5:
            return;
        }

        private void Awake()
        {
        }

        private void Refresh()
        {
            int num;
            KeyItem item;
            ItemParam param;
            ItemData data;
            GameObject obj2;
            KeyQuestBanner banner;
            if ((this.ItemTemplate == null) != null)
            {
                goto Label_0022;
            }
            if ((this.ItemLayout == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            num = 0;
            goto Label_00F7;
        Label_002A:
            item = this.Chapter.keys[num];
            if (item == null)
            {
                goto Label_00F3;
            }
            if (string.IsNullOrEmpty(item.iname) != null)
            {
                goto Label_00F3;
            }
            if (item.num != null)
            {
                goto Label_0062;
            }
            goto Label_00F3;
        Label_0062:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(param);
            obj2 = Object.Instantiate<GameObject>(this.ItemTemplate);
            DataSource.Bind<ChapterParam>(obj2, this.Chapter);
            DataSource.Bind<ItemParam>(obj2, param);
            DataSource.Bind<ItemData>(obj2, data);
            DataSource.Bind<KeyItem>(obj2, item);
            banner = obj2.GetComponent<KeyQuestBanner>();
            if ((banner != null) == null)
            {
                goto Label_00D3;
            }
            banner.UpdateValue();
        Label_00D3:
            obj2.get_transform().SetParent(this.ItemLayout.get_transform(), 0);
            obj2.SetActive(1);
        Label_00F3:
            num += 1;
        Label_00F7:
            if (num < this.Chapter.keys.Count)
            {
                goto Label_002A;
            }
            return;
        }

        private unsafe void Start()
        {
            object[] objArray4;
            object[] objArray3;
            object[] objArray2;
            object[] objArray1;
            string str;
            ItemParam param;
            int num;
            KeyItem item;
            KeyQuestTypes types;
            bool flag;
            DateTime time;
            DateTime time2;
            long num2;
            TimeSpan span;
            KeyQuestTypes types2;
            this.Chapter = MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedChapter);
            if (this.Chapter != null)
            {
                goto Label_002E;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        Label_002E:
            if (((this.ItemTemplate != null) == null) || (this.ItemTemplate.get_activeInHierarchy() == null))
            {
                goto Label_005B;
            }
            this.ItemTemplate.SetActive(0);
        Label_005B:
            if ((this.Message != null) == null)
            {
                goto Label_02BE;
            }
            str = null;
            param = null;
            num = 0;
            if (this.Chapter.keys.Count <= 0)
            {
                goto Label_00B2;
            }
            item = this.Chapter.keys[0];
            param = MonoSingleton<GameManager>.Instance.GetItemParam(item.iname);
            num = item.num;
        Label_00B2:
            types = this.Chapter.GetKeyQuestType();
            flag = GlobalVars.KeyQuestTimeOver;
            types2 = types;
            if (types2 == 1)
            {
                goto Label_00DF;
            }
            if (types2 == 2)
            {
                goto Label_0279;
            }
            goto Label_02B2;
        Label_00DF:
            if ((this.Chapter.keytime == null) || (param == null))
            {
                goto Label_02B2;
            }
            time = TimeManager.FromUnixTime(0L);
            if (this.Chapter.end != null)
            {
                goto Label_0125;
            }
            time2 = TimeManager.FromUnixTime(this.Chapter.keytime);
            goto Label_0166;
        Label_0125:
            num2 = Math.Min(this.Chapter.end - TimeManager.FromDateTime(TimeManager.ServerTime), this.Chapter.keytime);
            time2 = TimeManager.FromUnixTime((num2 >= 0L) ? num2 : 0L);
        Label_0166:
            span = time2 - time;
            if (&span.TotalDays < 1.0)
            {
                goto Label_01CE;
            }
            objArray1 = new object[] { param.name, (int) num, (int) &span.Days };
            str = LocalizedText.Get((flag == null) ? "sys.KEYQUEST_UNLCOK_TIMER_D" : "sys.KEYQUEST_TIMEOVER_D", objArray1);
            goto Label_0274;
        Label_01CE:
            if (&span.TotalHours < 1.0)
            {
                goto Label_022B;
            }
            objArray2 = new object[] { param.name, (int) num, (int) &span.Hours };
            str = LocalizedText.Get((flag == null) ? "sys.KEYQUEST_UNLCOK_TIMER_H" : "sys.KEYQUEST_TIMEOVER_H", objArray2);
            goto Label_0274;
        Label_022B:
            objArray3 = new object[] { param.name, (int) num, (int) Mathf.Max(&span.Minutes, 0) };
            str = LocalizedText.Get((flag == null) ? "sys.KEYQUEST_UNLCOK_TIMER_M" : "sys.KEYQUEST_TIMEOVER_M", objArray3);
        Label_0274:
            goto Label_02B2;
        Label_0279:
            objArray4 = new object[] { param.name, (int) num };
            str = LocalizedText.Get((flag == null) ? "sys.KEYQUEST_UNLCOK_COUNT" : "sys.KEYQUEST_TIMEOVER_COUNT", objArray4);
        Label_02B2:
            this.Message.set_text(str);
        Label_02BE:
            this.Refresh();
            return;
        }
    }
}

