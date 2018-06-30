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

    [Pin(11, "Take Bonus", 0, 0), Pin(12, "Last Day", 1, 2), Pin(10, "Load Complete", 1, 1)]
    public class LoginBonusWindow : MonoBehaviour, IFlowInterface
    {
        public GameObject ItemList;
        public GameObject[] PositionList;
        [HeaderBar("▼アイコン表示用オブジェクト")]
        public ListItemEvents Item_Normal;
        public ListItemEvents Item_Taken;
        public Json_LoginBonus[] DebugItems;
        public int DebugBonusCount;
        private int mLoginBonusCount;
        public GameObject BonusParticleEffect;
        [HeaderBar("▼演出時のアイコン表示用オブジェクト")]
        public GameObject TodayItem;
        public GameObject TommorowItem;
        public Text Today;
        public Text Tommorow;
        public GameObject TommorowRow;
        public GameObject VIPBonusRow;
        public RectTransform TodayBadge;
        public RectTransform TommorowBadge;
        public LoginBonusVIPBadge VIPBadge;
        public string CheckName;
        public string[] DisabledFirstDayNames;
        public string TableID;
        public string TodayTextID;
        public string TommorowTextID1;
        public string TommorowTextID2;
        public string LastDayTextID;
        private List<ListItemEvents> mItems;

        public LoginBonusWindow()
        {
            this.CheckName = "CHECK";
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            if (num == 11)
            {
                goto Label_000F;
            }
            goto Label_001A;
        Label_000F:
            this.FlipTodaysItem();
        Label_001A:
            return;
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

        private void DisableFirstDayHiddenOject(GameObject parent)
        {
            int num;
            string str;
            Transform transform;
            if ((parent == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (this.DisabledFirstDayNames != null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            num = 0;
            goto Label_0062;
        Label_0020:
            str = this.DisabledFirstDayNames[num];
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0039;
            }
            goto Label_005E;
        Label_0039:
            transform = parent.get_transform().FindChild(str);
            if ((transform != null) == null)
            {
                goto Label_005E;
            }
            transform.get_gameObject().SetActive(0);
        Label_005E:
            num += 1;
        Label_0062:
            if (num < ((int) this.DisabledFirstDayNames.Length))
            {
                goto Label_0020;
            }
            return;
        }

        private void FlipTodaysItem()
        {
            int num;
            ListItemEvents events;
            GiftRecieveItemData data;
            ListItemEvents events2;
            GiftRecieveItem item;
            Transform transform;
            Animator animator;
            if (this.mLoginBonusCount < 0)
            {
                goto Label_0022;
            }
            if (this.mItems.Count >= this.mLoginBonusCount)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            num = this.mLoginBonusCount - 1;
            events = this.mItems[num];
            if ((this.BonusParticleEffect != null) == null)
            {
                goto Label_006F;
            }
            UIUtility.SpawnParticle(this.BonusParticleEffect, events.get_transform() as RectTransform, new Vector2(0.5f, 0.5f));
        Label_006F:
            data = DataSource.FindDataOfClass<GiftRecieveItemData>(events.get_gameObject(), null);
            events2 = Object.Instantiate<ListItemEvents>(this.Item_Taken);
            DataSource.Bind<GiftRecieveItemData>(events2.get_gameObject(), data);
            events2.get_transform().SetParent(events.get_transform().get_parent(), 0);
            events2.get_transform().SetSiblingIndex(events.get_transform().GetSiblingIndex());
            item = events2.GetComponentInChildren<GiftRecieveItem>();
            if ((item != null) == null)
            {
                goto Label_00DD;
            }
            item.UpdateValue();
        Label_00DD:
            if ((this.TodayBadge != null) == null)
            {
                goto Label_0100;
            }
            this.TodayBadge.SetParent(events2.get_transform(), 0);
        Label_0100:
            Object.Destroy(events.get_gameObject());
            events2.get_gameObject().SetActive(1);
            transform = events2.get_transform().FindChild(this.CheckName);
            if ((transform != null) == null)
            {
                goto Label_0155;
            }
            animator = transform.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0155;
            }
            animator.set_enabled(1);
        Label_0155:
            if (num != null)
            {
                goto Label_0167;
            }
            this.DisableFirstDayHiddenOject(events2.get_gameObject());
        Label_0167:
            this.mItems[num] = events2;
            return;
        }

        private string MakeLastText()
        {
            string str;
            if (string.IsNullOrEmpty(this.LastDayTextID) != null)
            {
                goto Label_001C;
            }
            str = this.LastDayTextID;
            goto Label_0052;
        Label_001C:
            if (string.IsNullOrEmpty(this.TableID) != null)
            {
                goto Label_004C;
            }
            str = "sys.LOGBO_" + this.TableID.ToUpper() + "_LAST";
            goto Label_0052;
        Label_004C:
            str = "sys.LOGBO_LAST";
        Label_0052:
            return LocalizedText.Get(str);
        }

        private string MakeTodayText(GiftRecieveItemData todaysBonusItem)
        {
            object[] objArray1;
            string str;
            if (string.IsNullOrEmpty(this.TodayTextID) != null)
            {
                goto Label_001C;
            }
            str = this.TodayTextID;
            goto Label_0052;
        Label_001C:
            if (string.IsNullOrEmpty(this.TableID) != null)
            {
                goto Label_004C;
            }
            str = "sys.LOGBO_" + this.TableID.ToUpper() + "_TODAY";
            goto Label_0052;
        Label_004C:
            str = "sys.LOGBO_TODAY";
        Label_0052:
            objArray1 = new object[] { todaysBonusItem.name, (int) todaysBonusItem.num, (int) this.mLoginBonusCount };
            return LocalizedText.Get(str, objArray1);
        }

        private string MakeTomorrowText(GiftRecieveItemData todaysBonusItem, GiftRecieveItemData tomorrowBonusItem)
        {
            object[] objArray1;
            bool flag;
            string str;
            if (((todaysBonusItem == null) ? 0 : (todaysBonusItem.iname == tomorrowBonusItem.iname)) == null)
            {
                goto Label_0078;
            }
            if (string.IsNullOrEmpty(this.TommorowTextID2) != null)
            {
                goto Label_003D;
            }
            str = this.TommorowTextID2;
            goto Label_0073;
        Label_003D:
            if (string.IsNullOrEmpty(this.TableID) != null)
            {
                goto Label_006D;
            }
            str = "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW2";
            goto Label_0073;
        Label_006D:
            str = "sys.LOGBO_TOMMOROW2";
        Label_0073:
            goto Label_00CA;
        Label_0078:
            if (string.IsNullOrEmpty(this.TommorowTextID1) != null)
            {
                goto Label_0094;
            }
            str = this.TommorowTextID1;
            goto Label_00CA;
        Label_0094:
            if (string.IsNullOrEmpty(this.TableID) != null)
            {
                goto Label_00C4;
            }
            str = "sys.LOGBO_" + this.TableID.ToUpper() + "_TOMMOROW";
            goto Label_00CA;
        Label_00C4:
            str = "sys.LOGBO_TOMMOROW";
        Label_00CA:
            objArray1 = new object[] { tomorrowBonusItem.name };
            return LocalizedText.Get(str, objArray1);
        }

        private unsafe void Start()
        {
            GameManager manager;
            Json_LoginBonus[] bonusArray;
            GiftRecieveItemData data;
            GiftRecieveItemData data2;
            List<GiftRecieveItemData> list;
            bool flag;
            int num;
            GiftRecieveItemData data3;
            string str;
            int num2;
            ItemParam param;
            ConceptCardParam param2;
            ListItemEvents events;
            ListItemEvents events2;
            LoginBonusVIPBadge badge;
            Transform transform;
            Animator animator;
            Transform transform2;
            GiftRecieveItem item;
            bool flag2;
            GiftRecieveItem item2;
            GiftRecieveItem item3;
            manager = MonoSingleton<GameManager>.Instance;
            bonusArray = manager.Player.FindLoginBonuses(this.TableID);
            this.mLoginBonusCount = manager.Player.LoginCountWithType(this.TableID);
            data = null;
            data2 = null;
            list = new List<GiftRecieveItemData>();
            flag = 0;
            if (this.DebugItems == null)
            {
                goto Label_0069;
            }
            if (((int) this.DebugItems.Length) <= 0)
            {
                goto Label_0069;
            }
            bonusArray = this.DebugItems;
            this.mLoginBonusCount = this.DebugBonusCount;
        Label_0069:
            if (bonusArray == null)
            {
                goto Label_0474;
            }
            num = 0;
            goto Label_046A;
        Label_0077:
            data3 = new GiftRecieveItemData();
            list.Add(data3);
            str = bonusArray[num].iname;
            num2 = bonusArray[num].num;
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_00CA;
            }
            if (bonusArray[num].coin <= 0)
            {
                goto Label_00CA;
            }
            str = "$COIN";
            num2 = bonusArray[num].coin;
        Label_00CA:
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_00DB;
            }
            goto Label_0464;
        Label_00DB:
            param = manager.MasterParam.GetItemParam(str, 0);
            if (param == null)
            {
                goto Label_0114;
            }
            data3.Set(str, 1L, param.rare, num2);
            data3.name = param.name;
        Label_0114:
            param2 = manager.MasterParam.GetConceptCardParam(str);
            if (param2 == null)
            {
                goto Label_0150;
            }
            data3.Set(str, 0x1000L, param2.rare, num2);
            data3.name = param2.name;
        Label_0150:
            if (param != null)
            {
                goto Label_016F;
            }
            if (param2 != null)
            {
                goto Label_016F;
            }
            DebugUtility.LogError(string.Format("不明な識別子が報酬として設定されています。itemID => {0}", str));
        Label_016F:
            if (num != (this.mLoginBonusCount - 1))
            {
                goto Label_01AB;
            }
            data = data3;
            if (bonusArray[num].vip == null)
            {
                goto Label_01BB;
            }
            if (bonusArray[num].vip.lv <= 0)
            {
                goto Label_01BB;
            }
            flag = 1;
            goto Label_01BB;
        Label_01AB:
            if (num != this.mLoginBonusCount)
            {
                goto Label_01BB;
            }
            data2 = data3;
        Label_01BB:
            if (num >= (this.mLoginBonusCount - 1))
            {
                goto Label_01D7;
            }
            events = this.Item_Taken;
            goto Label_01DF;
        Label_01D7:
            events = this.Item_Normal;
        Label_01DF:
            if ((events == null) != null)
            {
                goto Label_0464;
            }
            if ((this.ItemList == null) == null)
            {
                goto Label_0202;
            }
            goto Label_0464;
        Label_0202:
            events2 = Object.Instantiate<ListItemEvents>(events);
            DataSource.Bind<GiftRecieveItemData>(events2.get_gameObject(), data3);
            if ((events == this.Item_Normal) == null)
            {
                goto Label_02D3;
            }
            if ((this.VIPBadge != null) == null)
            {
                goto Label_02D3;
            }
            if (bonusArray[num].vip == null)
            {
                goto Label_02D3;
            }
            if (bonusArray[num].vip.lv <= 0)
            {
                goto Label_02D3;
            }
            badge = Object.Instantiate<LoginBonusVIPBadge>(this.VIPBadge);
            if ((badge.VIPRank != null) == null)
            {
                goto Label_029C;
            }
            badge.VIPRank.set_text(&bonusArray[num].vip.lv.ToString());
        Label_029C:
            badge.get_transform().SetParent(events2.get_transform(), 0);
            ((RectTransform) badge.get_transform()).set_anchoredPosition(Vector2.get_zero());
            badge.get_gameObject().SetActive(1);
        Label_02D3:
            if ((this.TodayBadge != null) == null)
            {
                goto Label_032C;
            }
            if (num != (this.mLoginBonusCount - 1))
            {
                goto Label_032C;
            }
            this.TodayBadge.SetParent(events2.get_transform(), 0);
            this.TodayBadge.set_anchoredPosition(Vector2.get_zero());
            this.TodayBadge.get_gameObject().SetActive(1);
            goto Label_037E;
        Label_032C:
            if ((this.TommorowBadge != null) == null)
            {
                goto Label_037E;
            }
            if (num != this.mLoginBonusCount)
            {
                goto Label_037E;
            }
            this.TommorowBadge.SetParent(events2.get_transform(), 0);
            this.TommorowBadge.set_anchoredPosition(Vector2.get_zero());
            this.TommorowBadge.get_gameObject().SetActive(1);
        Label_037E:
            if (num >= (this.mLoginBonusCount - 1))
            {
                goto Label_03CC;
            }
            transform = events2.get_transform().FindChild(this.CheckName);
            if ((transform != null) == null)
            {
                goto Label_03CC;
            }
            animator = transform.GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_03CC;
            }
            animator.set_enabled(0);
        Label_03CC:
            transform2 = this.ItemList.get_transform();
            if (this.PositionList == null)
            {
                goto Label_0417;
            }
            if (((int) this.PositionList.Length) <= num)
            {
                goto Label_0417;
            }
            if ((this.PositionList[num] != null) == null)
            {
                goto Label_0417;
            }
            transform2 = this.PositionList[num].get_transform();
        Label_0417:
            if (num != null)
            {
                goto Label_042B;
            }
            this.DisableFirstDayHiddenOject(events2.get_gameObject());
        Label_042B:
            events2.get_transform().SetParent(transform2, 0);
            events2.get_gameObject().SetActive(1);
            events2.GetComponentInChildren<GiftRecieveItem>().UpdateValue();
            this.mItems.Add(events2);
        Label_0464:
            num += 1;
        Label_046A:
            if (num < ((int) bonusArray.Length))
            {
                goto Label_0077;
            }
        Label_0474:
            flag2 = manager.Player.IsLastLoginBonus(this.TableID);
            if (bonusArray == null)
            {
                goto Label_049E;
            }
            if (this.mLoginBonusCount != ((int) bonusArray.Length))
            {
                goto Label_049E;
            }
            flag2 = 1;
        Label_049E:
            if ((this.Today != null) == null)
            {
                goto Label_04C7;
            }
            if (data == null)
            {
                goto Label_04C7;
            }
            this.Today.set_text(this.MakeTodayText(data));
        Label_04C7:
            if ((this.TodayItem != null) == null)
            {
                goto Label_0502;
            }
            DataSource.Bind<GiftRecieveItemData>(this.TodayItem.get_gameObject(), data);
            this.TodayItem.get_gameObject().GetComponentInChildren<GiftRecieveItem>().UpdateValue();
        Label_0502:
            if ((this.TommorowItem != null) == null)
            {
                goto Label_053D;
            }
            DataSource.Bind<GiftRecieveItemData>(this.TommorowItem.get_gameObject(), data2);
            this.TommorowItem.get_gameObject().GetComponentInChildren<GiftRecieveItem>().UpdateValue();
        Label_053D:
            if ((this.Tommorow != null) == null)
            {
                goto Label_0573;
            }
            if (flag2 != null)
            {
                goto Label_0573;
            }
            if (data2 == null)
            {
                goto Label_0573;
            }
            this.Tommorow.set_text(this.MakeTomorrowText(data, data2));
            goto Label_0595;
        Label_0573:
            if ((this.TommorowRow != null) == null)
            {
                goto Label_0595;
            }
            this.Tommorow.set_text(this.MakeLastText());
        Label_0595:
            if ((this.VIPBonusRow != null) == null)
            {
                goto Label_05B3;
            }
            this.VIPBonusRow.SetActive(flag);
        Label_05B3:
            if (flag2 == null)
            {
                goto Label_05C2;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
        Label_05C2:
            base.StartCoroutine(this.WaitLoadAsync());
            return;
        }

        [DebuggerHidden]
        private IEnumerator WaitLoadAsync()
        {
            <WaitLoadAsync>c__Iterator120 iterator;
            iterator = new <WaitLoadAsync>c__Iterator120();
            iterator.<>f__this = this;
            return iterator;
        }

        [CompilerGenerated]
        private sealed class <WaitLoadAsync>c__Iterator120 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal LoginBonusWindow <>f__this;

            public <WaitLoadAsync>c__Iterator120()
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
                goto Label_006E;
            Label_0025:
                this.$current = null;
                this.$PC = 1;
                goto Label_0070;
            Label_0038:
                goto Label_0050;
            Label_003D:
                this.$current = null;
                this.$PC = 2;
                goto Label_0070;
            Label_0050:
                if (AssetManager.IsLoading != null)
                {
                    goto Label_003D;
                }
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 10);
                this.$PC = -1;
            Label_006E:
                return 0;
            Label_0070:
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

