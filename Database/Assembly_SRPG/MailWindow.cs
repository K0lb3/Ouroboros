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

    [Pin(13, "次の頁", 0, 0x38), Pin(0x1f5, "念装選択アイテムで所持上限オーバー", 1, 0x1f5), Pin(500, "武具選択アイテムで所持上限オーバー", 1, 500), Pin(0x12f, "念装選択アイテム", 1, 0xcb), Pin(0x12e, "武具選択アイテム", 1, 0xca), Pin(0x12d, "アイテム選択アイテム", 1, 0xc9), Pin(300, "ユニット選択アイテム", 1, 200), Pin(60, "開封", 1, 0x6a), Pin(0x1f, "頁内開封", 0, 0x69), Pin(30, "一件開封", 0, 0x68), Pin(14, "再初期化", 0, 0x67), Pin(200, "ページ取得", 1, 0x66), Pin(11, "現在の頁更新", 0, 0x65), Pin(0x34, "メールリスト更新", 1, 100), Pin(12, "前の頁", 0, 0x37), Pin(4, "開封済み", 0, 0x36), Pin(3, "期限あり", 0, 0x35), Pin(2, "期限なし", 0, 0x34), Pin(0x33, "初期化終了(未読なし)", 1, 3), Pin(50, "初期化終了(未読あり)", 1, 2), Pin(1, "初期化", 0, 1)]
    public class MailWindow : MonoBehaviour, IFlowInterface
    {
        private const int PIN_ID_INITIALIZE = 1;
        private const int PIN_ID_TAB_NOT_PERIOD = 2;
        private const int PIN_ID_TAB_PERIOD = 3;
        private const int PIN_ID_TAB_READ = 4;
        private const int PIN_ID_CURRENT_PAGE = 11;
        private const int PIN_ID_PREV_PAGE = 12;
        private const int PIN_ID_NEXT_PAGE = 13;
        private const int PIN_ID_REFRESH = 14;
        private const int PIN_ID_READ = 30;
        private const int PIN_ID_READ_PAGE = 0x1f;
        private const int PIN_ID_INITIALIZED = 50;
        private const int PIN_ID_INITIALIZED_EMPTY = 0x33;
        private const int PIN_ID_SUCCESS = 0x34;
        private const int PIN_ID_REQUEST_READ = 60;
        private const int PIN_ID_REQUEST_PAGE = 200;
        private const int PIN_ID_UNIT_SELECT = 300;
        private const int PIN_ID_ITEM_SELECT = 0x12d;
        private const int PIN_ID_ARTIFACT_SELECT = 0x12e;
        private const int PIN_ID_CONCEPT_CARD_SELECT = 0x12f;
        private const int PIN_ID_ARTIFACT_OVER = 500;
        private const int PIN_ID_CONCEPT_CARD_OVER = 0x1f5;
        private TabData notPeriodTab;
        private TabData periodTab;
        private TabData readTab;
        private TabType currentTab;
        [SerializeField]
        private TabType startTab;
        [SerializeField]
        private BitmapText currentPageText;
        [SerializeField]
        private BitmapText maxPageText;
        [SerializeField]
        private GameObject readAllButton;
        [SerializeField]
        private ScrollRect scrollRect;
        [SerializeField]
        private Button prevPageButton;
        [SerializeField]
        private Button nextPageButton;
        [SerializeField]
        private GameObject flowParent;
        private int m_ConceptCardNum;
        [CompilerGenerated]
        private static Predicate<MailData> <>f__am$cacheD;
        [CompilerGenerated]
        private static Predicate<MailData> <>f__am$cacheE;
        [CompilerGenerated]
        private static Converter<MailData, long> <>f__am$cacheF;
        [CompilerGenerated]
        private static Predicate<MailData> <>f__am$cache10;
        [CompilerGenerated]
        private static Func<TabData, TabType, TabData> <>f__am$cache11;

        public MailWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Activated>m__35D(MailData md)
        {
            return (md.mid == GlobalVars.SelectedMailUniqueID);
        }

        [CompilerGenerated]
        private static bool <Activated>m__35E(MailData md)
        {
            return (md.Contains(0x2780L) == 0);
        }

        [CompilerGenerated]
        private static long <Activated>m__35F(MailData md)
        {
            return md.mid;
        }

        [CompilerGenerated]
        private static TabData <Refresh>m__361(TabData tabData, TabType tabType)
        {
            if (tabData != null)
            {
                goto Label_0014;
            }
            tabData = new TabData();
            tabData.tabType = tabType;
        Label_0014:
            return tabData;
        }

        [CompilerGenerated]
        private static bool <UpdateUI>m__360(MailData mailData)
        {
            return mailData.Contains(0x2780L);
        }

        public void Activated(int pinID)
        {
            long[] numArray1;
            bool flag;
            bool flag2;
            long[] numArray;
            MailData data;
            List<MailData> list;
            long[] numArray2;
            int num;
            TabType type;
            num = pinID;
            switch ((num - 1))
            {
                case 0:
                    goto Label_005B;

                case 1:
                    goto Label_01BD;

                case 2:
                    goto Label_01FD;

                case 3:
                    goto Label_023D;

                case 4:
                    goto Label_0044;

                case 5:
                    goto Label_0044;

                case 6:
                    goto Label_0044;

                case 7:
                    goto Label_0044;

                case 8:
                    goto Label_0044;

                case 9:
                    goto Label_0044;

                case 10:
                    goto Label_0115;

                case 11:
                    goto Label_027D;

                case 12:
                    goto Label_02C1;

                case 13:
                    goto Label_0166;
            }
        Label_0044:
            if (num == 30)
            {
                goto Label_0305;
            }
            if (num == 0x1f)
            {
                goto Label_0499;
            }
            goto Label_050D;
        Label_005B:
            this.currentTab = this.startTab;
            this.Refresh();
            flag = 0;
            flag2 = 0;
            switch (this.startTab)
            {
                case 0:
                    goto Label_0091;

                case 1:
                    goto Label_00B6;

                case 2:
                    goto Label_00DB;
            }
            goto Label_00E2;
        Label_0091:
            flag = MonoSingleton<GameManager>.Instance.Player.UnreadMail;
            flag2 = MonoSingleton<GameManager>.Instance.Player.UnreadMailPeriod;
            goto Label_00E2;
        Label_00B6:
            flag = MonoSingleton<GameManager>.Instance.Player.UnreadMailPeriod;
            flag2 = MonoSingleton<GameManager>.Instance.Player.UnreadMail;
            goto Label_00E2;
        Label_00DB:
            flag = 1;
        Label_00E2:
            if (flag == null)
            {
                goto Label_00F5;
            }
            this.LateActivateOutputLinks(50);
            goto Label_0110;
        Label_00F5:
            if (flag2 == null)
            {
                goto Label_0108;
            }
            this.LateActivateOutputLinks(0x33);
            goto Label_0110;
        Label_0108:
            this.LateActivateOutputLinks(50);
        Label_0110:
            goto Label_050D;
        Label_0115:
            if (this.currentTabData.SetPage(MonoSingleton<GameManager>.Instance.Player.MailPage) == null)
            {
                goto Label_0159;
            }
            this.UpdateUI();
            MonoSingleton<GameManager>.Instance.Player.CurrentMails = this.currentTabData.currentPageData.mails;
        Label_0159:
            this.ActivateOutputLinks(0x34);
            goto Label_050D;
        Label_0166:
            this.Refresh();
            if (this.currentTabData.SetPage(MonoSingleton<GameManager>.Instance.Player.MailPage) == null)
            {
                goto Label_01B0;
            }
            this.UpdateUI();
            MonoSingleton<GameManager>.Instance.Player.CurrentMails = this.currentTabData.currentPageData.mails;
        Label_01B0:
            this.ActivateOutputLinks(0x34);
            goto Label_050D;
        Label_01BD:
            this.TabChange(0);
            if (this.currentTabData.currentPageIsReady == null)
            {
                goto Label_01E7;
            }
            this.UpdateUI();
            this.ActivateOutputLinks(0x34);
            goto Label_01F8;
        Label_01E7:
            this.RequestCurrentMails();
            this.ActivateOutputLinks(200);
        Label_01F8:
            goto Label_050D;
        Label_01FD:
            this.TabChange(1);
            if (this.currentTabData.currentPageIsReady == null)
            {
                goto Label_0227;
            }
            this.UpdateUI();
            this.ActivateOutputLinks(0x34);
            goto Label_0238;
        Label_0227:
            this.RequestCurrentMails();
            this.ActivateOutputLinks(200);
        Label_0238:
            goto Label_050D;
        Label_023D:
            this.TabChange(2);
            if (this.currentTabData.currentPageIsReady == null)
            {
                goto Label_0267;
            }
            this.UpdateUI();
            this.ActivateOutputLinks(0x34);
            goto Label_0278;
        Label_0267:
            this.RequestCurrentMails();
            this.ActivateOutputLinks(200);
        Label_0278:
            goto Label_050D;
        Label_027D:
            if (this.currentTabData.HasPrev() == null)
            {
                goto Label_050D;
            }
            if (this.PrevPage() == null)
            {
                goto Label_02AB;
            }
            this.UpdateUI();
            this.ActivateOutputLinks(0x34);
            goto Label_02BC;
        Label_02AB:
            this.RequestPrevMails();
            this.ActivateOutputLinks(200);
        Label_02BC:
            goto Label_050D;
        Label_02C1:
            if (this.currentTabData.HasNext() == null)
            {
                goto Label_050D;
            }
            if (this.NexePage() == null)
            {
                goto Label_02EF;
            }
            this.UpdateUI();
            this.ActivateOutputLinks(0x34);
            goto Label_0300;
        Label_02EF:
            this.RequestNextMails();
            this.ActivateOutputLinks(200);
        Label_0300:
            goto Label_050D;
        Label_0305:
            numArray1 = new long[] { GlobalVars.SelectedMailUniqueID };
            numArray = numArray1;
            if (<>f__am$cacheD != null)
            {
                goto Label_0341;
            }
            <>f__am$cacheD = new Predicate<MailData>(MailWindow.<Activated>m__35D);
        Label_0341:
            data = this.currentTabData.currentPageData.mails.Find(<>f__am$cacheD);
            if (data == null)
            {
                goto Label_050D;
            }
            if (data.Contains(0x2700L) == null)
            {
                goto Label_0388;
            }
            GlobalVars.SelectedMailPeriod.Set(data.period);
            GlobalVars.SelectedMailPage.Set(this.currentTabData.currentPage);
        Label_0388:
            if (data.Contains(0x400L) == null)
            {
                goto Label_03E8;
            }
            if ((MonoSingleton<GameManager>.Instance.Player.ArtifactNum + 1) <= MonoSingleton<GameManager>.Instance.MasterParam.FixParam.ArtifactBoxCap)
            {
                goto Label_03D8;
            }
            this.ActivateOutputLinks(500);
            goto Label_03E3;
        Label_03D8:
            this.ActivateOutputLinks(0x12e);
        Label_03E3:
            goto Label_0494;
        Label_03E8:
            if (data.Contains(0x200L) == null)
            {
                goto Label_0409;
            }
            this.ActivateOutputLinks(0x12d);
            goto Label_0494;
        Label_0409:
            if (data.Contains(0x100L) == null)
            {
                goto Label_042A;
            }
            this.ActivateOutputLinks(300);
            goto Label_0494;
        Label_042A:
            if (data.Contains(0x2000L) == null)
            {
                goto Label_0485;
            }
            if ((GlobalVars.ConceptCardNum + 1) <= MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardMax)
            {
                goto Label_0475;
            }
            this.ActivateOutputLinks(0x1f5);
            goto Label_0480;
        Label_0475:
            this.ActivateOutputLinks(0x12f);
        Label_0480:
            goto Label_0494;
        Label_0485:
            this.RequestRead(numArray);
            this.ActivateOutputLinks(60);
        Label_0494:
            goto Label_050D;
        Label_0499:
            if (<>f__am$cacheE != null)
            {
                goto Label_04C1;
            }
            <>f__am$cacheE = new Predicate<MailData>(MailWindow.<Activated>m__35E);
        Label_04C1:
            if (<>f__am$cacheF != null)
            {
                goto Label_04E7;
            }
            <>f__am$cacheF = new Converter<MailData, long>(MailWindow.<Activated>m__35F);
        Label_04E7:
            numArray2 = this.currentTabData.currentPageData.mails.FindAll(<>f__am$cacheE).ConvertAll<long>(<>f__am$cacheF).ToArray();
            this.RequestRead(numArray2);
            this.ActivateOutputLinks(60);
        Label_050D:
            return;
        }

        private void ActivateOutputLinks(int pinID)
        {
            FlowNode_GameObject.ActivateOutputLinks(this, pinID);
            return;
        }

        private bool AddPage(int addValue)
        {
            TabData data;
            int num;
            MailPageData data2;
            data = this.currentTabData;
            if (data != null)
            {
                goto Label_000F;
            }
            return 0;
        Label_000F:
            num = data.currentPage + addValue;
            data2 = data.GetPageData(num);
            if (data2 != null)
            {
                goto Label_0028;
            }
            return 0;
        Label_0028:
            data.currentPage = num;
            MonoSingleton<GameManager>.Instance.Player.CurrentMails = data2.mails;
            return 1;
        }

        [DebuggerHidden]
        private IEnumerator CoroutineActivateOutputLinks(int pinID)
        {
            <CoroutineActivateOutputLinks>c__Iterator125 iterator;
            iterator = new <CoroutineActivateOutputLinks>c__Iterator125();
            iterator.pinID = pinID;
            iterator.<$>pinID = pinID;
            iterator.<>f__this = this;
            return iterator;
        }

        private TabData GetTab(TabType tabType)
        {
            if (tabType != null)
            {
                goto Label_000D;
            }
            return this.notPeriodTab;
        Label_000D:
            if (tabType != 1)
            {
                goto Label_001B;
            }
            return this.periodTab;
        Label_001B:
            if (tabType != 2)
            {
                goto Label_0029;
            }
            return this.readTab;
        Label_0029:
            return null;
        }

        private void LateActivateOutputLinks(int pinID)
        {
            base.StartCoroutine(this.CoroutineActivateOutputLinks(pinID));
            return;
        }

        private bool NexePage()
        {
            return this.AddPage(1);
        }

        private bool PrevPage()
        {
            return this.AddPage(-1);
        }

        private void Refresh()
        {
            Func<TabData, TabType, TabData> func;
            if (<>f__am$cache11 != null)
            {
                goto Label_0018;
            }
            <>f__am$cache11 = new Func<TabData, TabType, TabData>(MailWindow.<Refresh>m__361);
        Label_0018:
            func = <>f__am$cache11;
            this.notPeriodTab = func(this.notPeriodTab, 0);
            this.periodTab = func(this.periodTab, 1);
            this.readTab = func(this.readTab, 2);
            this.notPeriodTab.Clear();
            this.periodTab.Clear();
            this.readTab.Clear();
            return;
        }

        private void RequestCurrentMails()
        {
            TabData data;
            TabType type;
            data = this.currentTabData;
            if (data == null)
            {
                goto Label_0064;
            }
            switch (data.tabType)
            {
                case 0:
                    goto Label_002B;

                case 1:
                    goto Label_003E;

                case 2:
                    goto Label_0051;
            }
            goto Label_0064;
        Label_002B:
            this.SetMailRequestData(data.currentPage, 0, 0);
            goto Label_0064;
        Label_003E:
            this.SetMailRequestData(data.currentPage, 1, 0);
            goto Label_0064;
        Label_0051:
            this.SetMailRequestData(data.currentPage, 0, 1);
        Label_0064:
            return;
        }

        private void RequestNextMails()
        {
            TabData data;
            TabType type;
            data = this.currentTabData;
            if (data == null)
            {
                goto Label_0075;
            }
            if (data.HasNext() == null)
            {
                goto Label_0075;
            }
            switch (data.tabType)
            {
                case 0:
                    goto Label_0036;

                case 1:
                    goto Label_004B;

                case 2:
                    goto Label_0060;
            }
            goto Label_0075;
        Label_0036:
            this.SetMailRequestData(data.currentPage + 1, 0, 0);
            goto Label_0075;
        Label_004B:
            this.SetMailRequestData(data.currentPage + 1, 1, 0);
            goto Label_0075;
        Label_0060:
            this.SetMailRequestData(data.currentPage + 1, 0, 1);
        Label_0075:
            return;
        }

        private void RequestPrevMails()
        {
            TabData data;
            TabType type;
            data = this.currentTabData;
            if (data == null)
            {
                goto Label_0075;
            }
            if (data.HasPrev() == null)
            {
                goto Label_0075;
            }
            switch (data.tabType)
            {
                case 0:
                    goto Label_0036;

                case 1:
                    goto Label_004B;

                case 2:
                    goto Label_0060;
            }
            goto Label_0075;
        Label_0036:
            this.SetMailRequestData(data.currentPage - 1, 0, 0);
            goto Label_0075;
        Label_004B:
            this.SetMailRequestData(data.currentPage - 1, 1, 0);
            goto Label_0075;
        Label_0060:
            this.SetMailRequestData(data.currentPage - 1, 0, 1);
        Label_0075:
            return;
        }

        private void RequestRead(long[] mailIDs)
        {
            TabType type;
            type = this.currentTab;
            if (type == 0)
            {
                goto Label_001A;
            }
            if (type == 1)
            {
                goto Label_0032;
            }
            goto Label_004A;
        Label_001A:
            this.SetReadRequestData(this.currentTabData.currentPage, 0, mailIDs);
            goto Label_004B;
        Label_0032:
            this.SetReadRequestData(this.currentTabData.currentPage, 1, mailIDs);
            goto Label_004B;
        Label_004A:
            return;
        Label_004B:
            return;
        }

        private void SetMailRequestData(int page, bool isPeriod, bool isRead)
        {
            MailPageRequestData data;
            data = new MailPageRequestData();
            data.page = page;
            data.isRead = isRead;
            data.isPeriod = isPeriod;
            DataSource.Bind<MailPageRequestData>(this.flowParent, data);
            return;
        }

        private void SetReadRequestData(int page, bool isPeriod, long[] mailIDs)
        {
            MailReadRequestData data;
            data = new MailReadRequestData();
            data.page = page;
            data.mailIDs = mailIDs;
            data.isPeriod = isPeriod;
            DataSource.Bind<MailReadRequestData>(this.flowParent, data);
            return;
        }

        public void TabChange(TabType tabType)
        {
            TabType type;
            type = this.currentTab;
            this.currentTab = tabType;
            if (type == this.currentTab)
            {
                goto Label_002F;
            }
            MonoSingleton<GameManager>.Instance.Player.CurrentMails = new List<MailData>(1);
        Label_002F:
            this.AddPage(0);
            return;
        }

        private unsafe void UpdateUI()
        {
            int num;
            int num2;
            int num3;
            num = this.currentTabData.currentPageData.page;
            num2 = this.currentTabData.currentPageData.pageMax;
            if (num2 != null)
            {
                goto Label_002A;
            }
            num = 0;
        Label_002A:
            if ((this.scrollRect != null) == null)
            {
                goto Label_004C;
            }
            SRPG_Extensions.SetNormalizedPosition(this.scrollRect, Vector2.get_one(), 1);
        Label_004C:
            this.currentPageText.text = &num.ToString();
            this.maxPageText.text = &num2.ToString();
            this.nextPageButton.set_interactable(this.currentTabData.HasNext());
            this.prevPageButton.set_interactable(this.currentTabData.HasPrev());
            if (this.currentTabData.currentPageData.mails.Count < 1)
            {
                goto Label_00C3;
            }
            if (this.currentTab != 2)
            {
                goto Label_00D4;
            }
        Label_00C3:
            this.readAllButton.SetActive(0);
            goto Label_014B;
        Label_00D4:
            if (<>f__am$cache10 != null)
            {
                goto Label_00FC;
            }
            <>f__am$cache10 = new Predicate<MailData>(MailWindow.<UpdateUI>m__360);
        Label_00FC:
            num3 = this.currentTabData.currentPageData.mails.FindAll(<>f__am$cache10).Count;
            if (num3 <= 0)
            {
                goto Label_013F;
            }
            if (this.currentTabData.currentPageData.mails.Count != num3)
            {
                goto Label_013F;
            }
            this.readAllButton.SetActive(0);
            goto Label_014B;
        Label_013F:
            this.readAllButton.SetActive(1);
        Label_014B:
            return;
        }

        private TabData currentTabData
        {
            get
            {
                return this.GetTab(this.currentTab);
            }
        }

        [CompilerGenerated]
        private sealed class <CoroutineActivateOutputLinks>c__Iterator125 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int pinID;
            internal int $PC;
            internal object $current;
            internal int <$>pinID;
            internal MailWindow <>f__this;

            public <CoroutineActivateOutputLinks>c__Iterator125()
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

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_0038;

                    case 2:
                        goto Label_005C;
                }
                goto Label_0063;
            Label_0025:
                this.$current = null;
                this.$PC = 1;
                goto Label_0065;
            Label_0038:
                this.<>f__this.ActivateOutputLinks(this.pinID);
                this.$current = null;
                this.$PC = 2;
                goto Label_0065;
            Label_005C:
                this.$PC = -1;
            Label_0063:
                return 0;
            Label_0065:
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

        public class MailPageRequestData
        {
            public int page;
            public bool isRead;
            public bool isPeriod;

            public MailPageRequestData()
            {
                base..ctor();
                return;
            }
        }

        public class MailReadRequestData
        {
            public long[] mailIDs;
            public int page;
            public bool isPeriod;

            public MailReadRequestData()
            {
                base..ctor();
                return;
            }
        }

        internal class TabData
        {
            internal List<MailPageData> pageDataList;
            internal int currentPage;
            internal int pageMax;
            internal int mailCount;
            internal MailWindow.TabType tabType;

            public TabData()
            {
                this.currentPage = 1;
                base..ctor();
                return;
            }

            internal void Clear()
            {
                this.pageDataList = new List<MailPageData>();
                this.currentPage = 1;
                this.pageMax = 1;
                this.mailCount = 0;
                return;
            }

            internal MailPageData GetPageData(int page)
            {
                <GetPageData>c__AnonStorey35D storeyd;
                storeyd = new <GetPageData>c__AnonStorey35D();
                storeyd.page = page;
                return this.pageDataList.Find(new Predicate<MailPageData>(storeyd.<>m__363));
            }

            internal bool HasNext()
            {
                MailPageData data;
                data = this.GetPageData(this.currentPage);
                if (data == null)
                {
                    goto Label_001A;
                }
                return data.hasNext;
            Label_001A:
                return 0;
            }

            internal bool HasPrev()
            {
                MailPageData data;
                data = this.GetPageData(this.currentPage);
                if (data == null)
                {
                    goto Label_001A;
                }
                return data.hasPrev;
            Label_001A:
                return 0;
            }

            internal MailPageData NextPageData()
            {
                MailPageData data;
                data = this.GetPageData(this.currentPage + 1);
                if (data == null)
                {
                    goto Label_0023;
                }
                this.currentPage += 1;
            Label_0023:
                return data;
            }

            internal MailPageData PrevPageData()
            {
                MailPageData data;
                data = this.GetPageData(this.currentPage - 1);
                if (data == null)
                {
                    goto Label_0023;
                }
                this.currentPage -= 1;
            Label_0023:
                return data;
            }

            internal bool SetPage(MailPageData mailPageData)
            {
                MailPageData data;
                <SetPage>c__AnonStorey35C storeyc;
                storeyc = new <SetPage>c__AnonStorey35C();
                storeyc.mailPageData = mailPageData;
                if (storeyc.mailPageData != null)
                {
                    goto Label_001A;
                }
                return 0;
            Label_001A:
                if (this.pageDataList.Find(new Predicate<MailPageData>(storeyc.<>m__362)) != null)
                {
                    goto Label_007C;
                }
                this.pageDataList.Add(storeyc.mailPageData);
                this.currentPage = storeyc.mailPageData.page;
                this.pageMax = storeyc.mailPageData.pageMax;
                this.mailCount = storeyc.mailPageData.mailCount;
            Label_007C:
                return 1;
            }

            internal bool currentPageIsReady
            {
                get
                {
                    return ((this.currentPageData == null) == 0);
                }
            }

            internal MailPageData currentPageData
            {
                get
                {
                    return this.GetPageData(this.currentPage);
                }
            }

            [CompilerGenerated]
            private sealed class <GetPageData>c__AnonStorey35D
            {
                internal int page;

                public <GetPageData>c__AnonStorey35D()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__363(MailPageData pageData)
                {
                    return (pageData.page == this.page);
                }
            }

            [CompilerGenerated]
            private sealed class <SetPage>c__AnonStorey35C
            {
                internal MailPageData mailPageData;

                public <SetPage>c__AnonStorey35C()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__362(MailPageData pd)
                {
                    return (pd.page == this.mailPageData.page);
                }
            }
        }

        public enum TabType : byte
        {
            NotPeriod = 0,
            Period = 1,
            Read = 2
        }
    }
}

