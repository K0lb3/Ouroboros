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
    using UnityEngine.UI;

    public class LoginBonusCustomWindow : MonoBehaviour, IFlowInterface
    {
        public GameObject ItemList;
        public ListItemEvents Item_Normal;
        public ListItemEvents Item_Taken;
        public Json_LoginBonus[] DebugItems;
        public int DebugBonusCount;
        private int mLoginBonusCount;
        public RectTransform TodayBadge;
        public RectTransform TommorowBadge;
        public LoginBonusVIPBadge VIPBadge;
        public string CheckName;
        private List<ListItemEvents> mItems;
        private ItemData mCurrentItem;
        public GameObject ItemInfo;
        public GameObject ItemBg;
        public GameObject PieceInfo;
        public GameObject PieceBg;

        public LoginBonusCustomWindow()
        {
            this.CheckName = "CHECK";
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Awake()
        {
            if ((this.Item_Normal != null) == null)
            {
                goto Label_0022;
            }
            this.Item_Normal.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.Item_Taken != null) == null)
            {
                goto Label_0044;
            }
            this.Item_Taken.get_gameObject().SetActive(0);
        Label_0044:
            if ((this.VIPBadge != null) == null)
            {
                goto Label_0066;
            }
            this.VIPBadge.get_gameObject().SetActive(0);
        Label_0066:
            if ((this.TodayBadge != null) == null)
            {
                goto Label_0088;
            }
            this.TodayBadge.get_gameObject().SetActive(0);
        Label_0088:
            if ((this.TommorowBadge != null) == null)
            {
                goto Label_00AA;
            }
            this.TommorowBadge.get_gameObject().SetActive(0);
        Label_00AA:
            return;
        }

        private void FlipTodaysItem()
        {
            int num;
            ListItemEvents events;
            ItemData data;
            Button button;
            <FlipTodaysItem>c__AnonStorey359 storey;
            storey = new <FlipTodaysItem>c__AnonStorey359();
            storey.<>f__this = this;
            if (this.mLoginBonusCount < 0)
            {
                goto Label_0031;
            }
            if (this.mItems.Count >= this.mLoginBonusCount)
            {
                goto Label_0032;
            }
        Label_0031:
            return;
        Label_0032:
            num = this.mLoginBonusCount - 1;
            events = this.mItems[num];
            data = DataSource.FindDataOfClass<ItemData>(events.get_gameObject(), null);
            storey.newItem = Object.Instantiate<ListItemEvents>(this.Item_Taken);
            DataSource.Bind<ItemData>(storey.newItem.get_gameObject(), data);
            storey.newItem.get_transform().SetParent(events.get_transform().get_parent(), 0);
            storey.newItem.get_transform().SetSiblingIndex(events.get_transform().GetSiblingIndex());
            button = storey.newItem.get_transform().GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_00E8;
            }
            button.get_onClick().AddListener(new UnityAction(storey, this.<>m__35A));
        Label_00E8:
            if ((this.TodayBadge != null) == null)
            {
                goto Label_0111;
            }
            this.TodayBadge.SetParent(storey.newItem.get_transform(), 0);
        Label_0111:
            Object.Destroy(events.get_gameObject());
            storey.newItem.get_gameObject().SetActive(1);
            this.mItems[num] = storey.newItem;
            return;
        }

        private void ShowDetailWindow(ListItemEvents item)
        {
            ItemData data;
            string str;
            CanvasGroup group;
            CanvasGroup group2;
            data = DataSource.FindDataOfClass<ItemData>(item.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            str = string.Empty;
            if (string.IsNullOrEmpty(data.Param.Flavor) == null)
            {
                goto Label_0077;
            }
            DataSource.Bind<ItemData>(this.PieceInfo, data);
            GameParameter.UpdateAll(this.PieceInfo);
            str = "OPEN_PIECE_DETAIL";
            group = this.PieceBg.GetComponent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_00BA;
            }
            group.set_interactable(1);
            group.set_blocksRaycasts(1);
            goto Label_00BA;
        Label_0077:
            DataSource.Bind<ItemData>(this.ItemInfo, data);
            GameParameter.UpdateAll(this.ItemInfo);
            str = "OPEN_ITEM_DETAIL";
            group2 = this.ItemBg.GetComponent<CanvasGroup>();
            if ((group2 != null) == null)
            {
                goto Label_00BA;
            }
            group2.set_interactable(1);
            group2.set_blocksRaycasts(1);
        Label_00BA:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, str);
            return;
        }

        private unsafe void Start()
        {
            GameManager manager;
            Json_LoginBonus[] bonusArray;
            Transform transform;
            int num;
            string str;
            int num2;
            ItemParam param;
            LoginBonusData data;
            ListItemEvents events;
            LoginBonusVIPBadge badge;
            Transform transform2;
            Animator animator;
            Button button;
            <Start>c__AnonStorey358 storey;
            manager = MonoSingleton<GameManager>.Instance;
            bonusArray = manager.Player.LoginBonus;
            this.mLoginBonusCount = manager.Player.LoginBonusCount;
            if (this.DebugItems == null)
            {
                goto Label_004F;
            }
            if (((int) this.DebugItems.Length) <= 0)
            {
                goto Label_004F;
            }
            bonusArray = this.DebugItems;
            this.mLoginBonusCount = this.DebugBonusCount;
        Label_004F:
            if (bonusArray == null)
            {
                goto Label_03A9;
            }
            if ((this.Item_Normal != null) == null)
            {
                goto Label_03A9;
            }
            if ((this.ItemList != null) == null)
            {
                goto Label_03A9;
            }
            transform = this.ItemList.get_transform();
            num = 0;
            goto Label_03A0;
        Label_008A:
            storey = new <Start>c__AnonStorey358();
            storey.<>f__this = this;
            str = bonusArray[num].iname;
            num2 = bonusArray[num].num;
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_00D8;
            }
            if (bonusArray[num].coin <= 0)
            {
                goto Label_00D8;
            }
            str = "$COIN";
            num2 = bonusArray[num].coin;
        Label_00D8:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_00E9;
            }
            goto Label_039C;
        Label_00E9:
            param = manager.GetItemParam(str);
            if (param != null)
            {
                goto Label_00FF;
            }
            goto Label_039C;
        Label_00FF:
            data = new LoginBonusData();
            data.DayNum = num + 1;
            if (data.Setup(0L, param, num2) != null)
            {
                goto Label_0127;
            }
            goto Label_039C;
        Label_0127:
            if (num >= (this.mLoginBonusCount - 1))
            {
                goto Label_0142;
            }
            events = this.Item_Taken;
            goto Label_014A;
        Label_0142:
            events = this.Item_Normal;
        Label_014A:
            storey.item = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<ItemData>(storey.item.get_gameObject(), data);
            if ((events == this.Item_Normal) == null)
            {
                goto Label_0227;
            }
            if ((this.VIPBadge != null) == null)
            {
                goto Label_0227;
            }
            if (bonusArray[num].vip == null)
            {
                goto Label_0227;
            }
            if (bonusArray[num].vip.lv <= 0)
            {
                goto Label_0227;
            }
            badge = Object.Instantiate<LoginBonusVIPBadge>(this.VIPBadge);
            if ((badge.VIPRank != null) == null)
            {
                goto Label_01EB;
            }
            badge.VIPRank.set_text(&bonusArray[num].vip.lv.ToString());
        Label_01EB:
            badge.get_transform().SetParent(storey.item.get_transform(), 0);
            ((RectTransform) badge.get_transform()).set_anchoredPosition(Vector2.get_zero());
            badge.get_gameObject().SetActive(1);
        Label_0227:
            if ((this.TodayBadge != null) == null)
            {
                goto Label_0284;
            }
            if (num != (this.mLoginBonusCount - 1))
            {
                goto Label_0284;
            }
            this.TodayBadge.SetParent(storey.item.get_transform(), 0);
            this.TodayBadge.set_anchoredPosition(Vector2.get_zero());
            this.TodayBadge.get_gameObject().SetActive(1);
            goto Label_02DA;
        Label_0284:
            if ((this.TommorowBadge != null) == null)
            {
                goto Label_02DA;
            }
            if (num != this.mLoginBonusCount)
            {
                goto Label_02DA;
            }
            this.TommorowBadge.SetParent(storey.item.get_transform(), 0);
            this.TommorowBadge.set_anchoredPosition(Vector2.get_zero());
            this.TommorowBadge.get_gameObject().SetActive(1);
        Label_02DA:
            if (num >= (this.mLoginBonusCount - 1))
            {
                goto Label_032C;
            }
            transform2 = storey.item.get_transform().FindChild(this.CheckName);
            if ((transform2 != null) == null)
            {
                goto Label_032C;
            }
            animator = transform2.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_032C;
            }
            animator.set_enabled(0);
        Label_032C:
            button = storey.item.get_transform().GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0365;
            }
            button.get_onClick().AddListener(new UnityAction(storey, this.<>m__359));
        Label_0365:
            storey.item.get_transform().SetParent(transform, 0);
            storey.item.get_gameObject().SetActive(1);
            this.mItems.Add(storey.item);
        Label_039C:
            num += 1;
        Label_03A0:
            if (num < ((int) bonusArray.Length))
            {
                goto Label_008A;
            }
        Label_03A9:
            this.FlipTodaysItem();
            base.StartCoroutine(this.WaitLoadAsync());
            return;
        }

        [DebuggerHidden]
        private IEnumerator WaitLoadAsync()
        {
            <WaitLoadAsync>c__Iterator11F iteratorf;
            iteratorf = new <WaitLoadAsync>c__Iterator11F();
            return iteratorf;
        }

        [CompilerGenerated]
        private sealed class <FlipTodaysItem>c__AnonStorey359
        {
            internal ListItemEvents newItem;
            internal LoginBonusCustomWindow <>f__this;

            public <FlipTodaysItem>c__AnonStorey359()
            {
                base..ctor();
                return;
            }

            internal void <>m__35A()
            {
                this.<>f__this.ShowDetailWindow(this.newItem);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Start>c__AnonStorey358
        {
            internal ListItemEvents item;
            internal LoginBonusCustomWindow <>f__this;

            public <Start>c__AnonStorey358()
            {
                base..ctor();
                return;
            }

            internal void <>m__359()
            {
                this.<>f__this.ShowDetailWindow(this.item);
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <WaitLoadAsync>c__Iterator11F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;

            public <WaitLoadAsync>c__Iterator11F()
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
                        goto Label_0050;
                }
                goto Label_0061;
            Label_0025:
                this.$current = null;
                this.$PC = 1;
                goto Label_0063;
            Label_0038:
                goto Label_0050;
            Label_003D:
                this.$current = null;
                this.$PC = 2;
                goto Label_0063;
            Label_0050:
                if (AssetManager.IsLoading != null)
                {
                    goto Label_003D;
                }
                this.$PC = -1;
            Label_0061:
                return 0;
            Label_0063:
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

