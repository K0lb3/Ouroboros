namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(4, "Open Tab Practice", 0, 4), Pin(1, "Start", 0, 1), Pin(2, "Refresh", 0, 2), Pin(3, "Open Tab Basic", 0, 3), Pin(5, "Open Tab Banner", 0, 5), Pin(10, "Select Tips", 0, 10), Pin(11, "Select Practice", 0, 11), Pin(12, "Select Banner", 0, 12), Pin(100, "Reset Status", 0, 100), Pin(0x2710, "Tips Detail", 1, 0x2710), Pin(0x2711, "Go To Quest", 1, 0x2711)]
    public class BeginnerTop : MonoBehaviour, IFlowInterface
    {
        private const int START = 1;
        private const int REFRESH = 2;
        private const int TAB_BASIC = 3;
        private const int TAB_PRACTICE = 4;
        private const int TAB_BANNER = 5;
        private const int ON_SELECT_TIPS = 10;
        private const int ON_SELECT_PRACTICE = 11;
        private const int ON_SELECT_BANNER = 12;
        private const int RESET_STATUS = 100;
        private const int OUTPUT_SHOW_TIPS_DETAIL = 0x2710;
        private const int OUTPUT_GOTO_QUEST = 0x2711;
        [SerializeField]
        private Toggle ToggleTips;
        [SerializeField]
        private Toggle TogglePractice;
        [SerializeField]
        private Toggle ToggleBanners;
        [SerializeField, Space(8f)]
        private GameObject BadgeTips;
        [SerializeField]
        private GameObject BadgePractice;
        [SerializeField, Space(8f)]
        private GameObject BasicPanel;
        [SerializeField]
        private GameObject PracticePanel;
        [SerializeField]
        private GameObject BannerPanel;
        [SerializeField, Space(8f)]
        private Transform BasicHolder;
        [SerializeField]
        private Transform PracticeHolder;
        [SerializeField]
        private GameObject BasicTemplate;
        [SerializeField]
        private GameObject PracticeTemplate;
        private TabType mCurrentTabType;
        private List<GameObject> mTipsItems;
        private List<GameObject> mQuestItems;
        [CompilerGenerated]
        private static Func<TipsParam, int> <>f__am$cacheF;

        public BeginnerTop()
        {
            this.mTipsItems = new List<GameObject>();
            this.mQuestItems = new List<GameObject>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <CreateBasicTabContent>m__29C(TipsParam t)
        {
            return t.order;
        }

        public void Activated(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0030;
            }
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.BEGINNER_TOP_HAS_VISITED, 1, 0);
            Network.RequestAPI(new ReqGetTipsAlreadyRead(new SRPG.Network.ResponseCallback(this.ResponseCallback)), 0);
            goto Label_00C5;
        Label_0030:
            if (pinID != 2)
            {
                goto Label_0042;
            }
            this.Refresh();
            goto Label_00C5;
        Label_0042:
            if (pinID != 3)
            {
                goto Label_0056;
            }
            this.ChangeTab(0, 0);
            goto Label_00C5;
        Label_0056:
            if (pinID != 4)
            {
                goto Label_006A;
            }
            this.ChangeTab(1, 0);
            goto Label_00C5;
        Label_006A:
            if (pinID != 5)
            {
                goto Label_007E;
            }
            this.ChangeTab(2, 0);
            goto Label_00C5;
        Label_007E:
            if (pinID != 10)
            {
                goto Label_0091;
            }
            this.OnSelectBasic();
            goto Label_00C5;
        Label_0091:
            if (pinID != 11)
            {
                goto Label_00A4;
            }
            this.OnSelectPractice();
            goto Label_00C5;
        Label_00A4:
            if (pinID != 12)
            {
                goto Label_00B7;
            }
            this.OnSelectBanner();
            goto Label_00C5;
        Label_00B7:
            if (pinID != 100)
            {
                goto Label_00C5;
            }
            GlobalVars.RestoreBeginnerQuest = 0;
        Label_00C5:
            return;
        }

        private void Awake()
        {
            if ((this.BasicTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.BasicTemplate.SetActive(0);
        Label_001D:
            if ((this.PracticeTemplate != null) == null)
            {
                goto Label_003A;
            }
            this.PracticeTemplate.SetActive(0);
        Label_003A:
            GlobalVars.LastReadTips = null;
            return;
        }

        private void ChangeTab(TabType tabType, bool forceRefresh)
        {
            TabType type;
            if (forceRefresh != null)
            {
                goto Label_0013;
            }
            if (tabType != this.mCurrentTabType)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            type = tabType;
            switch (type)
            {
                case 0:
                    goto Label_002C;

                case 1:
                    goto Label_004A;

                case 2:
                    goto Label_0068;
            }
            goto Label_0086;
        Label_002C:
            this.ToggleTips.set_isOn(1);
            this.mCurrentTabType = 0;
            this.RefreshBasicTabPage();
            goto Label_0086;
        Label_004A:
            this.TogglePractice.set_isOn(1);
            this.mCurrentTabType = 1;
            this.RefreshPracticeTabPage();
            goto Label_0086;
        Label_0068:
            this.ToggleBanners.set_isOn(1);
            this.mCurrentTabType = 2;
            this.RefreshBannerTabPage();
        Label_0086:
            return;
        }

        private void CreateAllTabs()
        {
            bool flag;
            bool flag2;
            flag = this.CreateBasicTabContent();
            if ((this.BadgeTips != null) == null)
            {
                goto Label_0027;
            }
            this.BadgeTips.SetActive(flag == 0);
        Label_0027:
            flag2 = this.CreatePracticeTabContent();
            if ((this.BadgePractice != null) == null)
            {
                goto Label_004E;
            }
            this.BadgePractice.SetActive(flag2 == 0);
        Label_004E:
            return;
        }

        private bool CreateBasicTabContent()
        {
            bool flag;
            IOrderedEnumerable<TipsParam> enumerable;
            TipsParam param;
            IEnumerator<TipsParam> enumerator;
            GameObject obj2;
            TipsItem item;
            bool flag2;
            bool flag3;
            flag = 1;
            if (<>f__am$cacheF != null)
            {
                goto Label_0029;
            }
            <>f__am$cacheF = new Func<TipsParam, int>(BeginnerTop.<CreateBasicTabContent>m__29C);
        Label_0029:
            enumerator = Enumerable.OrderBy<TipsParam, int>(MonoSingleton<GameManager>.Instance.MasterParam.Tips, <>f__am$cacheF).GetEnumerator();
        Label_003B:
            try
            {
                goto Label_0108;
            Label_0040:
                param = enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(this.BasicTemplate);
                item = obj2.GetComponent<TipsItem>();
                flag2 = param.hide;
                flag3 = MonoSingleton<GameManager>.Instance.Tips.Contains(param.iname);
                if ((flag2 != null) || (flag3 != null))
                {
                    goto Label_008C;
                }
                flag = 0;
            Label_008C:
                item.IsCompleted = flag3;
                item.IsHidden = (flag2 == null) ? 0 : (flag3 == 0);
                item.Title = ((flag2 == null) || (flag3 != null)) ? param.title : param.cond_text;
                obj2.get_transform().SetParent(this.BasicHolder, 0);
                obj2.SetActive(1);
                DataSource.Bind<TipsParam>(obj2, param);
                this.mTipsItems.Add(obj2);
                item.UpdateContent();
            Label_0108:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0040;
                }
                goto Label_0123;
            }
            finally
            {
            Label_0118:
                if (enumerator != null)
                {
                    goto Label_011C;
                }
            Label_011C:
                enumerator.Dispose();
            }
        Label_0123:
            return flag;
        }

        private unsafe bool CreatePracticeTabContent()
        {
            QuestParam[] paramArray;
            List<QuestParam> list;
            QuestParam param;
            QuestParam[] paramArray2;
            int num;
            bool flag;
            QuestParam param2;
            List<QuestParam>.Enumerator enumerator;
            GameObject obj2;
            TipsQuestItem item;
            bool flag2;
            paramArray = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
            list = new List<QuestParam>();
            paramArray2 = paramArray;
            num = 0;
            goto Label_0056;
        Label_0020:
            param = paramArray2[num];
            if (param.type == 13)
            {
                goto Label_0037;
            }
            goto Label_0050;
        Label_0037:
            if (param.IsDateUnlock(-1L) != null)
            {
                goto Label_0049;
            }
            goto Label_0050;
        Label_0049:
            list.Add(param);
        Label_0050:
            num += 1;
        Label_0056:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0020;
            }
            flag = 1;
            enumerator = list.GetEnumerator();
        Label_006B:
            try
            {
                goto Label_0102;
            Label_0070:
                param2 = &enumerator.Current;
                obj2 = Object.Instantiate<GameObject>(this.PracticeTemplate);
                item = obj2.GetComponent<TipsQuestItem>();
                item.Title = param2.title;
                item.Name = param2.name;
                flag2 = param2.state == 2;
                if (flag2 != null)
                {
                    goto Label_00C1;
                }
                flag = 0;
            Label_00C1:
                item.IsCompleted = flag2;
                obj2.get_transform().SetParent(this.PracticeHolder, 0);
                obj2.SetActive(1);
                DataSource.Bind<QuestParam>(obj2, param2);
                this.mQuestItems.Add(obj2);
                item.UpdateContent();
            Label_0102:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0070;
                }
                goto Label_0120;
            }
            finally
            {
            Label_0113:
                ((List<QuestParam>.Enumerator) enumerator).Dispose();
            }
        Label_0120:
            return flag;
        }

        private unsafe void DeleteAllObj()
        {
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            GameObject obj3;
            List<GameObject>.Enumerator enumerator2;
            enumerator = this.mTipsItems.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0024;
            Label_0011:
                obj2 = &enumerator.Current;
                Object.Destroy(obj2.get_gameObject());
            Label_0024:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_0041;
            }
            finally
            {
            Label_0035:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_0041:
            this.mTipsItems.Clear();
            enumerator2 = this.mQuestItems.GetEnumerator();
        Label_0058:
            try
            {
                goto Label_0070;
            Label_005D:
                obj3 = &enumerator2.Current;
                Object.Destroy(obj3.get_gameObject());
            Label_0070:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_005D;
                }
                goto Label_008D;
            }
            finally
            {
            Label_0081:
                ((List<GameObject>.Enumerator) enumerator2).Dispose();
            }
        Label_008D:
            this.mQuestItems.Clear();
            return;
        }

        private void OnDestroy()
        {
            this.DeleteAllObj();
            GlobalVars.LastReadTips = null;
            return;
        }

        private void OnSelectBanner()
        {
            SerializeValueList list;
            string str;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            str = list.GetString("url");
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_002F;
            }
            Application.OpenURL(str);
        Label_002F:
            return;
        }

        private void OnSelectBasic()
        {
            SerializeValueList list;
            GameObject obj2;
            TipsParam param;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            GlobalVars.LastReadTips = DataSource.FindDataOfClass<TipsParam>(list.GetGameObject("item"), null).iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x2710);
            return;
        }

        private void OnSelectPractice()
        {
            SerializeValueList list;
            GameObject obj2;
            QuestParam param;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            GlobalVars.SelectedQuestID = DataSource.FindDataOfClass<QuestParam>(list.GetGameObject("item"), null).iname;
            GlobalVars.RestoreBeginnerQuest = 1;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x2711);
            return;
        }

        private unsafe void Refresh()
        {
            bool flag;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            TipsItem item;
            TipsParam param;
            bool flag2;
            bool flag3;
            if (this.mCurrentTabType != null)
            {
                goto Label_00F3;
            }
            flag = 1;
            enumerator = this.mTipsItems.GetEnumerator();
        Label_0019:
            try
            {
                goto Label_00B6;
            Label_001E:
                obj2 = &enumerator.Current;
                item = obj2.GetComponent<TipsItem>();
                param = DataSource.FindDataOfClass<TipsParam>(item.get_gameObject(), null);
                flag2 = param.hide;
                flag3 = MonoSingleton<GameManager>.Instance.Tips.Contains(param.iname);
                if ((flag2 != null) || (flag3 != null))
                {
                    goto Label_006C;
                }
                flag = 0;
            Label_006C:
                item.IsCompleted = flag3;
                item.IsHidden = (flag2 == null) ? 0 : (flag3 == 0);
                item.Title = ((flag2 == null) || (flag3 != null)) ? param.title : param.cond_text;
                item.UpdateContent();
            Label_00B6:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001E;
                }
                goto Label_00D3;
            }
            finally
            {
            Label_00C7:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_00D3:
            if ((this.BadgeTips != null) == null)
            {
                goto Label_00F3;
            }
            this.BadgeTips.SetActive(flag == 0);
        Label_00F3:
            return;
        }

        private void RefreshBannerTabPage()
        {
            this.BasicPanel.SetActive(0);
            this.PracticePanel.SetActive(0);
            this.BannerPanel.SetActive(1);
            return;
        }

        private void RefreshBasicTabPage()
        {
            this.BasicPanel.SetActive(1);
            this.PracticePanel.SetActive(0);
            this.BannerPanel.SetActive(0);
            return;
        }

        private void RefreshPracticeTabPage()
        {
            this.BasicPanel.SetActive(0);
            this.PracticePanel.SetActive(1);
            this.BannerPanel.SetActive(0);
            return;
        }

        private unsafe void ResponseCallback(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_Tips> response;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_001C;
            }
            FlowNode_Network.Retry();
            return;
        Label_001C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_Tips>>(&www.text);
            Network.RemoveAPI();
            if (response.body.tips == null)
            {
                goto Label_0058;
            }
            MonoSingleton<GameManager>.Instance.Tips = Enumerable.ToList<string>(response.body.tips);
        Label_0058:
            this.CreateAllTabs();
            if (GlobalVars.RestoreBeginnerQuest == null)
            {
                goto Label_007B;
            }
            GlobalVars.RestoreBeginnerQuest = 0;
            this.ChangeTab(1, 1);
            goto Label_0083;
        Label_007B:
            this.ChangeTab(0, 1);
        Label_0083:
            return;
        }

        private enum TabType
        {
            Basic,
            Practice,
            Banners
        }
    }
}

