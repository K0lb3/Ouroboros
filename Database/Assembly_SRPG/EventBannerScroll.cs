namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [Pin(0x34, "ToShop", 1, 9), Pin(1, "Refresh", 0, 0), Pin(0x30, "Refreshed", 1, 1), Pin(0x31, "ItemEmpty", 1, 2), Pin(10, "PageNext", 0, 3), Pin(11, "PagePrev", 0, 4), Pin(12, "Select", 0, 5), Pin(50, "ToStory", 1, 6), Pin(0x33, "ToEvent", 1, 7), Pin(0x37, "ToMulti", 1, 8), Pin(0x35, "ToGacha", 1, 10), Pin(0x36, "ToURL", 1, 11), Pin(0x38, "ToArena", 1, 12), Pin(0x39, "ToPVP", 1, 13), Pin(0x3a, "ToOrdeal", 1, 14), Pin(0x3b, "ToBeginner", 1, 15)]
    public class EventBannerScroll : SRPG_ScrollRect, IFlowInterface
    {
        private const int PIN_REFRESH = 1;
        private const int PIN_PAGE_NEXT = 10;
        private const int PIN_PAGE_PREV = 11;
        private const int PIN_SELECT = 12;
        private const int PIN_REFRESHED = 0x30;
        private const int PIN_EMPTY = 0x31;
        private const int PIN_TO_STORY = 50;
        private const int PIN_TO_EVENT = 0x33;
        private const int PIN_TO_SHOP = 0x34;
        private const int PIN_TO_GACHA = 0x35;
        private const int PIN_TO_URL = 0x36;
        private const int PIN_TO_MULTI = 0x37;
        private const int PIN_TO_ARENA = 0x38;
        private const int PIN_TO_PVP = 0x39;
        private const int PIN_TO_ORDEAL = 0x3a;
        private const int PIN_TO_BEGINNER = 0x3b;
        private const string BannerPathOfNormals = "Banners/";
        private const string BannerPathOfShop = "LimitedShopBanner/";
        public ToggleGroup PageToggleGroup;
        public GameObject TemplateBannerNormal;
        public GameObject TemplateBannerShop;
        public GameObject TemplatePageIcon;
        public float Interval;
        private bool mDragging;
        private int mPage;
        private float mElapsed;
        private IEnumerator mMove;

        public EventBannerScroll()
        {
            this.Interval = 4f;
            base..ctor();
            return;
        }

        private float decelerate(float value)
        {
            return (1f - (((1f - value) * (1f - value)) * (1f - value)));
        }

        private BannerParam getCurrentPageBannerParam()
        {
            Transform transform;
            DataSource source;
            if (this.mPage < base.get_content().get_childCount())
            {
                goto Label_0018;
            }
            return null;
        Label_0018:
            source = base.get_content().GetChild(this.mPage).GetComponent<DataSource>();
            if ((source == null) == null)
            {
                goto Label_003F;
            }
            return null;
        Label_003F:
            return source.FindDataOfClass<BannerParam>(null);
        }

        private unsafe float getPageOffset(int page)
        {
            RectTransform[] transformArray;
            int num;
            float num2;
            int num3;
            Rect rect;
            transformArray = new RectTransform[base.get_content().get_childCount()];
            num = 0;
            goto Label_0030;
        Label_0018:
            transformArray[num] = base.get_content().GetChild(num) as RectTransform;
            num += 1;
        Label_0030:
            if (num < base.get_content().get_childCount())
            {
                goto Label_0018;
            }
            num2 = 0f;
            num3 = 0;
            goto Label_006F;
        Label_004E:
            if (num3 != page)
            {
                goto Label_0057;
            }
            return num2;
        Label_0057:
            num2 += &transformArray[num3].get_rect().get_width();
            num3 += 1;
        Label_006F:
            if (num3 < ((int) transformArray.Length))
            {
                goto Label_004E;
            }
            return 0f;
        }

        void IFlowInterface.Activated(int pinID)
        {
            BannerParam param;
            PlayerData data;
            int num;
            BannerType type;
            num = pinID;
            switch ((num - 10))
            {
                case 0:
                    goto Label_0048;

                case 1:
                    goto Label_0072;

                case 2:
                    goto Label_009C;
            }
            if (num == 1)
            {
                goto Label_0023;
            }
            goto Label_02B4;
        Label_0023:
            if (this.Refresh() == null)
            {
                goto Label_003B;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x30);
            goto Label_0043;
        Label_003B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x31);
        Label_0043:
            goto Label_02B4;
        Label_0048:
            if (this.mMove != null)
            {
                goto Label_02B4;
            }
            this.mMove = this.movePage(this.mPage, this.mPage + 1);
            goto Label_02B4;
        Label_0072:
            if (this.mMove != null)
            {
                goto Label_02B4;
            }
            this.mMove = this.movePage(this.mPage, this.mPage - 1);
            goto Label_02B4;
        Label_009C:
            param = this.getCurrentPageBannerParam();
            data = MonoSingleton<GameManager>.Instance.Player;
            if (param == null)
            {
                goto Label_02B4;
            }
            switch (param.type)
            {
                case 0:
                    goto Label_00F2;

                case 1:
                    goto Label_0117;

                case 2:
                    goto Label_0138;

                case 3:
                    goto Label_0171;

                case 4:
                    goto Label_0189;

                case 5:
                    goto Label_0196;

                case 6:
                    goto Label_01BE;

                case 7:
                    goto Label_01FD;

                case 8:
                    goto Label_0230;

                case 9:
                    goto Label_0269;

                case 10:
                    goto Label_02A2;
            }
            goto Label_02AF;
        Label_00F2:
            if (this.setQuestVariables(param.sval, 1) == null)
            {
                goto Label_02B4;
            }
            GlobalVars.SelectedQuestID = null;
            FlowNode_GameObject.ActivateOutputLinks(this, 50);
            goto Label_02AF;
        Label_0117:
            GlobalVars.ReqEventPageListType = 0;
            this.setQuestVariables(param.sval, 0);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x33);
            goto Label_02AF;
        Label_0138:
            if (data.CheckUnlock(0x100) == null)
            {
                goto Label_0155;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x37);
            goto Label_016C;
        Label_0155:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x100);
        Label_016C:
            goto Label_02AF;
        Label_0171:
            GlobalVars.SelectedGachaTableId = param.sval;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x35);
            goto Label_02AF;
        Label_0189:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x34);
            goto Label_02AF;
        Label_0196:
            if (string.IsNullOrEmpty(param.sval) != null)
            {
                goto Label_01B1;
            }
            Application.OpenURL(param.sval);
        Label_01B1:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x36);
            goto Label_02AF;
        Label_01BE:
            if (data.CheckUnlock(0x400000) == null)
            {
                goto Label_01E1;
            }
            GlobalVars.ReqEventPageListType = 2;
            FlowNode_GameObject.ActivateOutputLinks(this, 0x33);
            goto Label_01F8;
        Label_01E1:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x400000);
        Label_01F8:
            goto Label_02AF;
        Label_01FD:
            if (data.CheckUnlock(0x10) == null)
            {
                goto Label_0217;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x38);
            goto Label_022B;
        Label_0217:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10);
        Label_022B:
            goto Label_02AF;
        Label_0230:
            if (data.CheckUnlock(0x10000) == null)
            {
                goto Label_024D;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x39);
            goto Label_0264;
        Label_024D:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x10000);
        Label_0264:
            goto Label_02AF;
        Label_0269:
            if (data.CheckUnlock(0x20000) == null)
            {
                goto Label_0286;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3a);
            goto Label_029D;
        Label_0286:
            LevelLock.ShowLockMessage(data.Lv, data.VipRank, 0x20000);
        Label_029D:
            goto Label_02AF;
        Label_02A2:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x3b);
        Label_02AF:;
        Label_02B4:
            return;
        }

        protected override unsafe void LateUpdate()
        {
            RectTransform[] transformArray;
            int num;
            float num2;
            Vector2 vector;
            int num3;
            float num4;
            Rect rect;
            if (this.PageCount <= 0)
            {
                goto Label_011F;
            }
            if (this.mMove == null)
            {
                goto Label_0033;
            }
            if (this.mMove.MoveNext() != null)
            {
                goto Label_011F;
            }
            this.mMove = null;
            goto Label_011F;
        Label_0033:
            if (this.mDragging != null)
            {
                goto Label_011F;
            }
            transformArray = new RectTransform[base.get_content().get_childCount()];
            num = 0;
            goto Label_006E;
        Label_0056:
            transformArray[num] = base.get_content().GetChild(num) as RectTransform;
            num += 1;
        Label_006E:
            if (num < base.get_content().get_childCount())
            {
                goto Label_0056;
            }
            num2 = 0f;
            vector = base.get_content().get_anchoredPosition();
            num3 = 0;
            goto Label_0115;
        Label_0099:
            num4 = &transformArray[num3].get_rect().get_width();
            if ((num2 + (num4 / 2f)) <= -&vector.x)
            {
                goto Label_010A;
            }
            &vector.x = Mathf.Lerp(&vector.x, -num2, 0.1f);
            this.SetContentAnchoredPosition(vector);
            num3 = num3 % this.PageCount;
            if (this.mPage == num3)
            {
                goto Label_011F;
            }
            this.onPageChanged(num3);
            goto Label_011F;
        Label_010A:
            num2 += num4;
            num3 += 1;
        Label_0115:
            if (num3 < ((int) transformArray.Length))
            {
                goto Label_0099;
            }
        Label_011F:
            base.LateUpdate();
            return;
        }

        private unsafe BannerParam[] makeValidBannerParams()
        {
            GameManager manager;
            BannerParam[] paramArray;
            List<BannerParam> list;
            GachaParam[] paramArray2;
            QuestParam[] paramArray3;
            QuestParam param;
            QuestParam param2;
            long num;
            DateTime time;
            int num2;
            JSON_ShopListArray.Shops shops;
            GachaParam param3;
            int num3;
            int num4;
            BannerParam param4;
            <makeValidBannerParams>c__AnonStorey32E storeye;
            DateTime time2;
            DateTime time3;
            DateTime time4;
            DateTime time5;
            manager = MonoSingleton<GameManager>.Instance;
            paramArray = manager.MasterParam.Banners;
            if (paramArray != null)
            {
                goto Label_001F;
            }
            return new BannerParam[0];
        Label_001F:
            list = new List<BannerParam>();
            paramArray2 = manager.Gachas;
            paramArray3 = manager.Player.AvailableQuests;
            param = null;
            param2 = manager.Player.FindLastStoryQuest();
            num = Network.GetServerTime();
            time = TimeManager.FromUnixTime(num);
            num2 = 0;
            goto Label_04FF;
        Label_0061:
            storeye = new <makeValidBannerParams>c__AnonStorey32E();
            storeye.banner = paramArray[num2];
            if (string.IsNullOrEmpty(storeye.banner.banner) != null)
            {
                goto Label_04F9;
            }
            if (list.FindIndex(new Predicate<BannerParam>(storeye.<>m__2F6)) == -1)
            {
                goto Label_00A7;
            }
            goto Label_04F9;
        Label_00A7:
            if (storeye.banner.IsHomeBanner != null)
            {
                goto Label_00BD;
            }
            goto Label_04F9;
        Label_00BD:
            if (storeye.banner.type != 4)
            {
                goto Label_01A2;
            }
            if (manager.IsLimitedShopOpen != null)
            {
                goto Label_00DF;
            }
            goto Label_04F9;
        Label_00DF:
            if (manager.LimitedShopList == null)
            {
                goto Label_0185;
            }
            if (string.IsNullOrEmpty(storeye.banner.sval) != null)
            {
                goto Label_0185;
            }
            shops = Array.Find<JSON_ShopListArray.Shops>(manager.LimitedShopList, new Predicate<JSON_ShopListArray.Shops>(storeye.<>m__2F7));
            if (shops != null)
            {
                goto Label_0126;
            }
            goto Label_04F9;
        Label_0126:
            storeye.banner.begin_at = &TimeManager.FromUnixTime(shops.start).ToString();
            storeye.banner.end_at = &TimeManager.FromUnixTime(shops.end).ToString();
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_019D;
        Label_0185:
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
        Label_019D:
            goto Label_04EC;
        Label_01A2:
            if (storeye.banner.type != null)
            {
                goto Label_0253;
            }
            if (param2 != null)
            {
                goto Label_01BF;
            }
            goto Label_04F9;
        Label_01BF:
            if (string.IsNullOrEmpty(storeye.banner.sval) == null)
            {
                goto Label_01F6;
            }
            param = param2;
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_024E;
        Label_01F6:
            param = Array.Find<QuestParam>(paramArray3, new Predicate<QuestParam>(storeye.<>m__2F8));
            if (param == null)
            {
                goto Label_0237;
            }
            if ((param.iname != param2.iname) == null)
            {
                goto Label_023B;
            }
            if (param.state != null)
            {
                goto Label_023B;
            }
        Label_0237:
            param = param2;
        Label_023B:
            if (param.IsDateUnlock(num) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
        Label_024E:
            goto Label_04EC;
        Label_0253:
            if (storeye.banner.type == 1)
            {
                goto Label_0277;
            }
            if (storeye.banner.type != 2)
            {
                goto Label_02DF;
            }
        Label_0277:
            if (string.IsNullOrEmpty(storeye.banner.sval) != null)
            {
                goto Label_02C2;
            }
            param = Array.Find<QuestParam>(paramArray3, new Predicate<QuestParam>(storeye.<>m__2F9));
            if (param == null)
            {
                goto Label_04F9;
            }
            if (param.IsDateUnlock(num) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_02DA;
        Label_02C2:
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
        Label_02DA:
            goto Label_04EC;
        Label_02DF:
            if (storeye.banner.type != 3)
            {
                goto Label_03A4;
            }
            if (string.IsNullOrEmpty(storeye.banner.sval) != null)
            {
                goto Label_0387;
            }
            param3 = Array.Find<GachaParam>(paramArray2, new Predicate<GachaParam>(storeye.<>m__2FA));
            if (param3 != null)
            {
                goto Label_0328;
            }
            goto Label_04F9;
        Label_0328:
            storeye.banner.begin_at = &TimeManager.FromUnixTime(param3.startat).ToString();
            storeye.banner.end_at = &TimeManager.FromUnixTime(param3.endat).ToString();
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_039F;
        Label_0387:
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
        Label_039F:
            goto Label_04EC;
        Label_03A4:
            if (storeye.banner.type != 5)
            {
                goto Label_03E9;
            }
            if (string.IsNullOrEmpty(storeye.banner.sval) != null)
            {
                goto Label_04F9;
            }
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_04EC;
        Label_03E9:
            if (storeye.banner.type != 6)
            {
                goto Label_0418;
            }
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_04EC;
        Label_0418:
            if (storeye.banner.type != 7)
            {
                goto Label_0447;
            }
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_04EC;
        Label_0447:
            if (storeye.banner.type != 8)
            {
                goto Label_0476;
            }
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_04EC;
        Label_0476:
            if (storeye.banner.type != 9)
            {
                goto Label_04EC;
            }
            if (string.IsNullOrEmpty(storeye.banner.sval) != null)
            {
                goto Label_04D4;
            }
            param = Array.Find<QuestParam>(paramArray3, new Predicate<QuestParam>(storeye.<>m__2FB));
            if (param == null)
            {
                goto Label_04F9;
            }
            if (param.IsDateUnlock(num) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
            goto Label_04EC;
        Label_04D4:
            if (storeye.banner.IsAvailablePeriod(time) != null)
            {
                goto Label_04EC;
            }
            goto Label_04F9;
        Label_04EC:
            list.Add(storeye.banner);
        Label_04F9:
            num2 += 1;
        Label_04FF:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_0061;
            }
            num3 = 0;
            goto Label_0578;
        Label_0511:
            num4 = num3 + 1;
            goto Label_0565;
        Label_051C:
            if (list[num3].priority <= list[num4].priority)
            {
                goto Label_055F;
            }
            param4 = list[num3];
            list[num3] = list[num4];
            list[num4] = param4;
        Label_055F:
            num4 += 1;
        Label_0565:
            if (num4 < list.Count)
            {
                goto Label_051C;
            }
            num3 += 1;
        Label_0578:
            if (num3 < (list.Count - 1))
            {
                goto Label_0511;
            }
            return list.ToArray();
        }

        [DebuggerHidden]
        private IEnumerator movePage(int from, int to)
        {
            <movePage>c__Iterator107 iterator;
            iterator = new <movePage>c__Iterator107();
            iterator.to = to;
            iterator.from = from;
            iterator.<$>to = to;
            iterator.<$>from = from;
            iterator.<>f__this = this;
            return iterator;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            this.mDragging = 1;
            this.mElapsed = 0f;
            return;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            this.mDragging = 0;
            base.OnEndDrag(eventData);
            return;
        }

        private void onPageChanged(int page)
        {
            int num;
            Toggle[] toggleArray;
            Toggle toggle;
            Toggle[] toggleArray2;
            int num2;
            this.mPage = page;
            num = 0;
            toggleArray2 = this.PageToggleGroup.GetComponentsInChildren<Toggle>();
            num2 = 0;
            goto Label_003D;
        Label_001F:
            toggle = toggleArray2[num2];
            toggle.set_isOn(num == this.mPage);
            num += 1;
            num2 += 1;
        Label_003D:
            if (num2 < ((int) toggleArray2.Length))
            {
                goto Label_001F;
            }
            return;
        }

        private unsafe void OnValueChanged(Vector2 value)
        {
            float num;
            float num2;
            Rect rect;
            Rect rect2;
            Vector2 vector;
            num = &base.get_content().get_rect().get_width() - &base.get_viewport().get_rect().get_width();
            num2 = -&base.get_content().get_anchoredPosition().x;
            if (num2 <= num)
            {
                goto Label_0060;
            }
            num2 = num2 % num;
            this.SetContentAnchoredPosition(new Vector2(-num2, 0f));
            goto Label_007F;
        Label_0060:
            if (num2 >= 0f)
            {
                goto Label_007F;
            }
            this.SetContentAnchoredPosition(new Vector2(-(num + num2), 0f));
        Label_007F:
            return;
        }

        private bool Refresh()
        {
            Transform transform;
            Transform transform2;
            BannerParam[] paramArray;
            int num;
            int num2;
            GameObject obj2;
            Vector3 vector;
            GameObject obj3;
            Toggle toggle;
            goto Label_0018;
        Label_0005:
            Object.Destroy(base.get_content().GetChild(0));
        Label_0018:
            if (base.get_content().get_childCount() != null)
            {
                goto Label_0005;
            }
            goto Label_0045;
        Label_002D:
            Object.Destroy(this.PageToggleGroup.get_transform().GetChild(0));
        Label_0045:
            if (this.PageToggleGroup.get_transform().get_childCount() != null)
            {
                goto Label_002D;
            }
            paramArray = this.makeValidBannerParams();
            if (((int) paramArray.Length) == null)
            {
                goto Label_0160;
            }
            num = 0;
            goto Label_0155;
        Label_0070:
            num2 = num % ((int) paramArray.Length);
            obj2 = null;
            if (paramArray[num2].type != 4)
            {
                goto Label_009B;
            }
            obj2 = Object.Instantiate<GameObject>(this.TemplateBannerShop);
            goto Label_00A8;
        Label_009B:
            obj2 = Object.Instantiate<GameObject>(this.TemplateBannerNormal);
        Label_00A8:
            vector = obj2.get_transform().get_localScale();
            obj2.get_transform().SetParent(base.get_content());
            obj2.get_transform().set_localScale(vector);
            obj2.SetActive(1);
            DataSource.Bind<BannerParam>(obj2, paramArray[num2]);
            if (num >= ((int) paramArray.Length))
            {
                goto Label_0151;
            }
            obj3 = Object.Instantiate<GameObject>(this.TemplatePageIcon);
            vector = obj2.get_transform().get_localScale();
            obj3.get_transform().SetParent(this.PageToggleGroup.get_transform());
            obj3.get_transform().set_localScale(vector);
            obj3.SetActive(1);
            if (num != null)
            {
                goto Label_0151;
            }
            obj3.GetComponent<Toggle>().set_isOn(1);
        Label_0151:
            num += 1;
        Label_0155:
            if (num < (((int) paramArray.Length) + 1))
            {
                goto Label_0070;
            }
        Label_0160:
            this.mPage = 0;
            this.mElapsed = 0f;
            this.PageToggleGroup.get_gameObject().SetActive(((int) paramArray.Length) > 1);
            return ((((int) paramArray.Length) == 0) == 0);
        }

        private bool setQuestVariables(string questId, bool story)
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
            if ((paramArray[num].iname == questId) == null)
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
            if (story == null)
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

        protected override void Start()
        {
            base.Start();
            base.set_inertia(0);
            base.set_movementType(0);
            base.set_horizontal(1);
            base.set_vertical(0);
            base.get_onValueChanged().AddListener(new UnityAction<Vector2>(this, this.OnValueChanged));
            return;
        }

        private void Update()
        {
            if (this.mDragging != null)
            {
                goto Label_0064;
            }
            if (this.mMove != null)
            {
                goto Label_0064;
            }
            if (base.get_content().get_childCount() <= 1)
            {
                goto Label_0064;
            }
            this.mElapsed += Time.get_deltaTime();
            if (this.mElapsed < this.Interval)
            {
                goto Label_0064;
            }
            this.mMove = this.movePage(this.mPage, this.mPage + 1);
        Label_0064:
            return;
        }

        private int PageCount
        {
            get
            {
                return (base.get_content().get_childCount() - 1);
            }
        }

        private int PageNow
        {
            get
            {
                return this.mPage;
            }
        }

        [CompilerGenerated]
        private sealed class <makeValidBannerParams>c__AnonStorey32E
        {
            internal BannerParam banner;

            public <makeValidBannerParams>c__AnonStorey32E()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2F6(BannerParam p)
            {
                return (p.iname == this.banner.iname);
            }

            internal bool <>m__2F7(JSON_ShopListArray.Shops p)
            {
                return (p.gname == this.banner.sval);
            }

            internal bool <>m__2F8(QuestParam p)
            {
                return (p.iname == this.banner.sval);
            }

            internal bool <>m__2F9(QuestParam p)
            {
                return (p.iname == this.banner.sval);
            }

            internal bool <>m__2FA(GachaParam p)
            {
                return (p.iname == this.banner.sval);
            }

            internal bool <>m__2FB(QuestParam p)
            {
                return (p.iname == this.banner.sval);
            }
        }

        [CompilerGenerated]
        private sealed class <movePage>c__Iterator107 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int to;
            internal int from;
            internal float <offsetFrom>__0;
            internal float <offsetTo>__1;
            internal float <tm>__2;
            internal float <t>__3;
            internal float <offset>__4;
            internal int $PC;
            internal object $current;
            internal int <$>to;
            internal int <$>from;
            internal EventBannerScroll <>f__this;

            public <movePage>c__Iterator107()
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
                        goto Label_0021;

                    case 1:
                        goto Label_016A;
                }
                goto Label_01C1;
            Label_0021:
                if (this.<>f__this.PageCount > 0)
                {
                    goto Label_0037;
                }
                goto Label_01C1;
            Label_0037:
                this.<>f__this.mElapsed = 0f;
                if (this.to <= this.<>f__this.PageCount)
                {
                    goto Label_0070;
                }
                this.to = 1;
                this.from = 0;
                goto Label_00AC;
            Label_0070:
                if (this.to >= 0)
                {
                    goto Label_00AC;
                }
                this.to = this.<>f__this.get_content().get_childCount() - 2;
                this.from = this.<>f__this.get_content().get_childCount() - 1;
            Label_00AC:
                this.<offsetFrom>__0 = this.<>f__this.getPageOffset(this.from);
                this.<offsetTo>__1 = this.<>f__this.getPageOffset(this.to);
                this.<tm>__2 = 0.4f;
                this.<t>__3 = 0f;
            Label_00F0:
                this.<offset>__4 = Mathf.Lerp(this.<offsetFrom>__0, this.<offsetTo>__1, this.<>f__this.decelerate(Mathf.Min(1f, this.<t>__3 / this.<tm>__2)));
                this.<>f__this.SetContentAnchoredPosition(new Vector2(-this.<offset>__4, 0f));
                this.<t>__3 += Time.get_deltaTime();
                this.$current = null;
                this.$PC = 1;
                goto Label_01C3;
            Label_016A:
                if (this.<t>__3 < this.<tm>__2)
                {
                    goto Label_00F0;
                }
                this.to = this.to % this.<>f__this.PageCount;
                if (this.<>f__this.mPage == this.to)
                {
                    goto Label_01BA;
                }
                this.<>f__this.onPageChanged(this.to);
            Label_01BA:
                this.$PC = -1;
            Label_01C1:
                return 0;
            Label_01C3:
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
    }
}

