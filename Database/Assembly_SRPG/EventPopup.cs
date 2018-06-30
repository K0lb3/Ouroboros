namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [Pin(100, "Select", 0, 0), Pin(0x33, "ToEvent", 1, 2), Pin(0x34, "ToMulti", 1, 3), Pin(0x35, "ToShop", 1, 4), Pin(0x36, "ToGacha", 1, 5), Pin(0x37, "ToURL", 1, 6), Pin(0x3b, "ToBeginner", 1, 10), Pin(0x3a, "ToOrdeal", 1, 9), Pin(0x39, "ToPVP", 1, 8), Pin(0x38, "ToArena", 1, 7), Pin(50, "ToStory", 1, 1)]
    public class EventPopup : MonoBehaviour, IFlowInterface
    {
        public const int INPUT_BANNER_SELECT = 100;
        public const int OUTPUT_BANNER_TO_STORY = 50;
        public const int OUTPUT_BANNER_TO_EVENT = 0x33;
        public const int OUTPUT_BANNER_TO_MULTI = 0x34;
        public const int OUTPUT_BANNER_TO_SHOP = 0x35;
        public const int OUTPUT_BANNER_TO_GACHA = 0x36;
        public const int OUTPUT_BANNER_TO_URL = 0x37;
        public const int OUTPUT_BANNER_TO_ARENA = 0x38;
        public const int OUTPUT_BANNER_TO_PVP = 0x39;
        public const int OUTPUT_BANNER_TO_ORDEAL = 0x3a;
        public const int OUTPUT_BANNER_TO_BEGINNER = 0x3b;
        [SerializeField]
        private GameObject EventBannerTemplate;
        [SerializeField]
        private Transform ListRoot;
        private List<GameObject> m_EventBannerList;
        private List<BannerParam> m_BannerList;
        [CompilerGenerated]
        private static Comparison<BannerParam> <>f__am$cache4;

        public EventPopup()
        {
            this.m_EventBannerList = new List<GameObject>();
            this.m_BannerList = new List<BannerParam>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <MakeValidBannerParams>m__300(BannerParam a, BannerParam b)
        {
            return (a.priority - b.priority);
        }

        public void Activated(int pinID)
        {
            if (pinID != 100)
            {
                goto Label_000E;
            }
            this.OnSelect();
        Label_000E:
            return;
        }

        private void Awake()
        {
            if ((this.EventBannerTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.EventBannerTemplate.SetActive(0);
        Label_001D:
            return;
        }

        public static unsafe BannerParam[] MakeValidBannerParams(bool _is_home_banner)
        {
            List<BannerParam> list;
            GameManager manager;
            BannerParam[] paramArray;
            QuestParam param;
            QuestParam param2;
            QuestParam[] paramArray2;
            long num;
            DateTime time;
            bool flag;
            int num2;
            JSON_ShopListArray.Shops shops;
            <MakeValidBannerParams>c__AnonStorey32F storeyf;
            DateTime time2;
            DateTime time3;
            list = new List<BannerParam>();
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.MasterParam.Banners;
            if (paramArray != null)
            {
                goto Label_002A;
            }
            DebugUtility.LogError("バナーの設定がありません、有効なバナーを1つ以上設定してください");
            return null;
        Label_002A:
            param = manager.Player.FindLastStoryQuest();
            param2 = null;
            paramArray2 = manager.Player.AvailableQuests;
            num = Network.GetServerTime();
            time = TimeManager.FromUnixTime(num);
            flag = 1;
            num2 = 0;
            goto Label_035C;
        Label_0061:
            storeyf = new <MakeValidBannerParams>c__AnonStorey32F();
            storeyf.param = paramArray[num2];
            flag = 1;
            if (storeyf.param == null)
            {
                goto Label_0356;
            }
            if (string.IsNullOrEmpty(storeyf.param.banner) != null)
            {
                goto Label_0356;
            }
            if (list.FindIndex(new Predicate<BannerParam>(storeyf.<>m__2FC)) == -1)
            {
                goto Label_00B6;
            }
            goto Label_0356;
        Label_00B6:
            if (_is_home_banner == null)
            {
                goto Label_00D2;
            }
            if (storeyf.param.IsHomeBanner != null)
            {
                goto Label_00D2;
            }
            goto Label_0356;
        Label_00D2:
            if (storeyf.param.type != 4)
            {
                goto Label_0182;
            }
            if (manager.IsLimitedShopOpen != null)
            {
                goto Label_00F4;
            }
            goto Label_0356;
        Label_00F4:
            if (manager.LimitedShopList == null)
            {
                goto Label_032A;
            }
            if (string.IsNullOrEmpty(storeyf.param.sval) != null)
            {
                goto Label_032A;
            }
            shops = Array.Find<JSON_ShopListArray.Shops>(manager.LimitedShopList, new Predicate<JSON_ShopListArray.Shops>(storeyf.<>m__2FD));
            if (shops != null)
            {
                goto Label_013B;
            }
            goto Label_0356;
        Label_013B:
            storeyf.param.begin_at = &TimeManager.FromUnixTime(shops.start).ToString();
            storeyf.param.end_at = &TimeManager.FromUnixTime(shops.end).ToString();
            goto Label_032A;
        Label_0182:
            if (storeyf.param.type != null)
            {
                goto Label_021A;
            }
            flag = 0;
            if (param != null)
            {
                goto Label_01A1;
            }
            goto Label_0356;
        Label_01A1:
            if (string.IsNullOrEmpty(storeyf.param.sval) == null)
            {
                goto Label_01BF;
            }
            param2 = param;
            goto Label_0202;
        Label_01BF:
            param2 = Array.Find<QuestParam>(paramArray2, new Predicate<QuestParam>(storeyf.<>m__2FE));
            if (param2 == null)
            {
                goto Label_01FF;
            }
            if ((param2.iname != param.iname) == null)
            {
                goto Label_0202;
            }
            if (param2.state != null)
            {
                goto Label_0202;
            }
        Label_01FF:
            param2 = param;
        Label_0202:
            if (param2.IsDateUnlock(num) != null)
            {
                goto Label_032A;
            }
            goto Label_0356;
            goto Label_032A;
        Label_021A:
            if (storeyf.param.type == 1)
            {
                goto Label_023E;
            }
            if (storeyf.param.type != 2)
            {
                goto Label_0289;
            }
        Label_023E:
            if (string.IsNullOrEmpty(storeyf.param.sval) != null)
            {
                goto Label_032A;
            }
            param2 = Array.Find<QuestParam>(paramArray2, new Predicate<QuestParam>(storeyf.<>m__2FF));
            if (param2 == null)
            {
                goto Label_0356;
            }
            if (param2.IsDateUnlock(num) != null)
            {
                goto Label_032A;
            }
            goto Label_0356;
            goto Label_032A;
        Label_0289:
            if (storeyf.param.type != 6)
            {
                goto Label_02A0;
            }
            goto Label_032A;
        Label_02A0:
            if (storeyf.param.type != 3)
            {
                goto Label_02B7;
            }
            goto Label_032A;
        Label_02B7:
            if (storeyf.param.type != 5)
            {
                goto Label_02E9;
            }
            if (string.IsNullOrEmpty(storeyf.param.sval) == null)
            {
                goto Label_032A;
            }
            goto Label_0356;
            goto Label_032A;
        Label_02E9:
            if (storeyf.param.type != 7)
            {
                goto Label_0300;
            }
            goto Label_032A;
        Label_0300:
            if (storeyf.param.type != 8)
            {
                goto Label_0317;
            }
            goto Label_032A;
        Label_0317:
            if (storeyf.param.type != 9)
            {
                goto Label_032A;
            }
        Label_032A:
            if (flag == null)
            {
                goto Label_0349;
            }
            if (storeyf.param.IsAvailablePeriod(time) != null)
            {
                goto Label_0349;
            }
            goto Label_0356;
        Label_0349:
            list.Add(storeyf.param);
        Label_0356:
            num2 += 1;
        Label_035C:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0061;
            }
            if (<>f__am$cache4 != null)
            {
                goto Label_037F;
            }
            <>f__am$cache4 = new Comparison<BannerParam>(EventPopup.<MakeValidBannerParams>m__300);
        Label_037F:
            list.Sort(<>f__am$cache4);
            return list.ToArray();
        }

        private void OnSelect()
        {
            SerializeValueList list;
            int num;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            num = list.GetInt("select");
            this.Select(num);
            return;
        }

        private void Select(int index)
        {
            BannerParam[] paramArray;
            BannerParam param;
            int num;
            PlayerData data;
            BannerType type;
            if (MonoSingleton<GameManager>.Instance == null)
            {
                goto Label_021D;
            }
            paramArray = this.m_BannerList.ToArray();
            if (paramArray == null)
            {
                goto Label_021D;
            }
            if (((int) paramArray.Length) >= index)
            {
                goto Label_0035;
            }
            DebugUtility.LogError("選択されたイベントバナーが正しくありません");
            return;
        Label_0035:
            param = paramArray[index];
            num = -1;
            data = MonoSingleton<GameManager>.Instance.Player;
            switch (param.type)
            {
                case 0:
                    goto Label_0086;

                case 1:
                    goto Label_00A5;

                case 2:
                    goto Label_00C0;

                case 3:
                    goto Label_012E;

                case 4:
                    goto Label_0141;

                case 5:
                    goto Label_0149;

                case 6:
                    goto Label_00F4;

                case 7:
                    goto Label_016C;

                case 8:
                    goto Label_019A;

                case 9:
                    goto Label_01CE;

                case 10:
                    goto Label_0202;
            }
            goto Label_020A;
        Label_0086:
            if (SetupQuestVariables(param.sval, 1) == null)
            {
                goto Label_020F;
            }
            GlobalVars.SelectedQuestID = null;
            num = 50;
            goto Label_020F;
        Label_00A5:
            GlobalVars.ReqEventPageListType = 0;
            SetupQuestVariables(param.sval, 0);
            num = 0x33;
            goto Label_020F;
        Label_00C0:
            if (data.CheckUnlock(0x100) == null)
            {
                goto Label_00D8;
            }
            num = 0x34;
            goto Label_00EF;
        Label_00D8:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x100);
        Label_00EF:
            goto Label_020F;
        Label_00F4:
            if (data.CheckUnlock(0x400000) == null)
            {
                goto Label_0112;
            }
            GlobalVars.ReqEventPageListType = 2;
            num = 0x33;
            goto Label_0129;
        Label_0112:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x400000);
        Label_0129:
            goto Label_020F;
        Label_012E:
            GlobalVars.SelectedGachaTableId = param.sval;
            num = 0x36;
            goto Label_020F;
        Label_0141:
            num = 0x35;
            goto Label_020F;
        Label_0149:
            if (string.IsNullOrEmpty(param.sval) != null)
            {
                goto Label_0164;
            }
            Application.OpenURL(param.sval);
        Label_0164:
            num = 0x37;
            goto Label_020F;
        Label_016C:
            if (data.CheckUnlock(0x10) == null)
            {
                goto Label_0181;
            }
            num = 0x38;
            goto Label_0195;
        Label_0181:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10);
        Label_0195:
            goto Label_020F;
        Label_019A:
            if (data.CheckUnlock(0x10000) == null)
            {
                goto Label_01B2;
            }
            num = 0x39;
            goto Label_01C9;
        Label_01B2:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10000);
        Label_01C9:
            goto Label_020F;
        Label_01CE:
            if (data.CheckUnlock(0x20000) == null)
            {
                goto Label_01E6;
            }
            num = 0x3a;
            goto Label_01FD;
        Label_01E6:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x20000);
        Label_01FD:
            goto Label_020F;
        Label_0202:
            num = 0x3b;
            goto Label_020F;
        Label_020A:;
        Label_020F:
            if (num == -1)
            {
                goto Label_021D;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, num);
        Label_021D:
            return;
        }

        private void Setup(BannerParam[] _params)
        {
            int num;
            BannerParam param;
            int num2;
            GameObject obj2;
            EventPopupListItem item;
            ButtonEvent event2;
            ButtonEvent.Event event3;
            if (_params == null)
            {
                goto Label_000F;
            }
            if (((int) _params.Length) > 0)
            {
                goto Label_001A;
            }
        Label_000F:
            DebugUtility.LogError("イベントバナーデータが存在しません");
            return;
        Label_001A:
            if ((this.EventBannerTemplate == null) == null)
            {
                goto Label_0036;
            }
            DebugUtility.LogError("テンプレートオブジェクトが指定されていません");
            return;
        Label_0036:
            this.m_EventBannerList.Clear();
            num = 0;
            goto Label_00EE;
        Label_0048:
            param = _params[num];
            num2 = num;
            if (param == null)
            {
                goto Label_00EA;
            }
            obj2 = Object.Instantiate<GameObject>(this.EventBannerTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_00EA;
            }
            obj2.get_transform().SetParent(this.ListRoot, 0);
            item = obj2.GetComponent<EventPopupListItem>();
            if ((item != null) == null)
            {
                goto Label_009B;
            }
            item.SetupBannerParam(param);
        Label_009B:
            event2 = obj2.GetComponent<ButtonEvent>();
            if ((event2 != null) == null)
            {
                goto Label_00D7;
            }
            event3 = event2.GetEvent("EVENTPOPUP_BANNER_SELECT");
            if (event3 == null)
            {
                goto Label_00D7;
            }
            event3.valueList.SetField("select", num2);
        Label_00D7:
            obj2.SetActive(1);
            this.m_EventBannerList.Add(obj2);
        Label_00EA:
            num += 1;
        Label_00EE:
            if (num < ((int) _params.Length))
            {
                goto Label_0048;
            }
            return;
        }

        public static bool SetupQuestVariables(string _questID, bool _is_story)
        {
            GameManager manager;
            QuestParam[] paramArray;
            int num;
            QuestParam param;
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.Player.AvailableQuests;
            num = 0;
            goto Label_005B;
        Label_0019:
            if ((paramArray[num].iname == _questID) == null)
            {
                goto Label_0057;
            }
            GlobalVars.SelectedSection.Set(paramArray[num].Chapter.section);
            GlobalVars.SelectedChapter.Set(paramArray[num].ChapterID);
            return 1;
        Label_0057:
            num += 1;
        Label_005B:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0019;
            }
            if (_is_story == null)
            {
                goto Label_00B3;
            }
            param = manager.Player.FindLastStoryQuest();
            if (param == null)
            {
                goto Label_00B3;
            }
            if (param.IsDateUnlock(Network.GetServerTime()) == null)
            {
                goto Label_00B3;
            }
            GlobalVars.SelectedSection.Set(param.Chapter.section);
            GlobalVars.SelectedChapter.Set(param.ChapterID);
            return 1;
        Label_00B3:
            return 0;
        }

        private void Start()
        {
            if ((MonoSingleton<GameManager>.Instance != null) == null)
            {
                goto Label_003D;
            }
            this.m_BannerList.Clear();
            this.m_BannerList.AddRange(MakeValidBannerParams(0));
            this.Setup(this.m_BannerList.ToArray());
        Label_003D:
            return;
        }

        [CompilerGenerated]
        private sealed class <MakeValidBannerParams>c__AnonStorey32F
        {
            internal BannerParam param;

            public <MakeValidBannerParams>c__AnonStorey32F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2FC(BannerParam p)
            {
                return (p.iname == this.param.iname);
            }

            internal bool <>m__2FD(JSON_ShopListArray.Shops p)
            {
                return (p.gname == this.param.sval);
            }

            internal bool <>m__2FE(QuestParam p)
            {
                return (p.iname == this.param.sval);
            }

            internal bool <>m__2FF(QuestParam p)
            {
                return (p.iname == this.param.sval);
            }
        }
    }
}

