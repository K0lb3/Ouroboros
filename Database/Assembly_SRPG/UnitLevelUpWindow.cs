namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "Close", 1, 0)]
    public class UnitLevelUpWindow : MonoBehaviour, IFlowInterface
    {
        [SerializeField]
        private RectTransform ListParent;
        [SerializeField]
        private GameObject ListItemTemplate;
        [SerializeField]
        private Text CurrentLevel;
        [SerializeField]
        private Text FinishedLevel;
        [SerializeField]
        private Text MaxLevel;
        [SerializeField]
        private Text NextExp;
        [SerializeField]
        private SliderAnimator LevelGauge;
        [SerializeField]
        private Text GetAllExp;
        [SerializeField]
        private Button DecideBtn;
        [SerializeField]
        private Button CancelBtn;
        [SerializeField]
        private Button MaxBtn;
        [SerializeField]
        private SliderAnimator AddLevelGauge;
        private MasterParam master;
        private PlayerData player;
        private UnitData mOriginUnit;
        private int mLv;
        private int mExp;
        private List<GameObject> mItems;
        private List<UnitLevelUpListItem> mUnitLevelupLists;
        private float mExpStart;
        private float mExpEnd;
        private float mExpAnimTime;
        private Dictionary<string, int> mSelectExpItems;
        public OnUnitLevelupEvent OnDecideEvent;
        private List<ItemData> mCacheExpItemList;
        public static readonly string CONFIRM_PATH;
        [CompilerGenerated]
        private static Comparison<ItemData> <>f__am$cache1A;
        [CompilerGenerated]
        private static Comparison<ItemData> <>f__am$cache1B;

        static UnitLevelUpWindow()
        {
            CONFIRM_PATH = "UI/UnitLevelUpConfirmWindow";
            return;
        }

        public UnitLevelUpWindow()
        {
            this.mItems = new List<GameObject>();
            this.mUnitLevelupLists = new List<UnitLevelUpListItem>();
            this.mSelectExpItems = new Dictionary<string, int>();
            this.mCacheExpItemList = new List<ItemData>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <Init>m__46C(ItemData a, ItemData b)
        {
            return (b.Param.value - a.Param.value);
        }

        [CompilerGenerated]
        private static int <RefreshUseMaxItems>m__470(ItemData a, ItemData b)
        {
            return (b.Param.value - a.Param.value);
        }

        public void Activated(int pinID)
        {
        }

        private unsafe void CalcCanUnitLevelUpMax()
        {
            long num;
            ItemData data;
            List<ItemData>.Enumerator enumerator;
            int num2;
            int num3;
            long num4;
            int num5;
            int num6;
            ItemData data2;
            ItemData data3;
            int num7;
            int num8;
            int num9;
            int num10;
            long num11;
            long num12;
            int num13;
            ItemData data4;
            int num14;
            int num15;
            GameObject obj2;
            ItemData data5;
            UnitLevelUpListItem item;
            Dictionary<string, int> dictionary;
            string str;
            int num16;
            Dictionary<string, int> dictionary2;
            if (this.mCacheExpItemList != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0L;
            enumerator = this.mCacheExpItemList.GetEnumerator();
        Label_001B:
            try
            {
                goto Label_0044;
            Label_0020:
                data = &enumerator.Current;
                num3 = data.Param.value * data.Num;
                num += (long) num3;
            Label_0044:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0020;
                }
                goto Label_0061;
            }
            finally
            {
            Label_0055:
                ((List<ItemData>.Enumerator) enumerator).Dispose();
            }
        Label_0061:
            num4 = (long) Mathf.Min((float) (this.mOriginUnit.GetGainExpCap() - this.mExp), (float) num);
            this.mSelectExpItems.Clear();
            num5 = 0;
            num6 = 0;
            goto Label_0250;
        Label_0094:
            if (num4 > 0L)
            {
                goto Label_00A6;
            }
            num4 = 0L;
            goto Label_0262;
        Label_00A6:
            data2 = this.mCacheExpItemList[num6];
            if (data2 != null)
            {
                goto Label_00CE;
            }
            if (data2.Num > 0)
            {
                goto Label_00CE;
            }
            goto Label_024A;
        Label_00CE:
            data3 = this.mCacheExpItemList[num5];
            if (num6 == num5)
            {
                goto Label_00FF;
            }
            if (data3 != null)
            {
                goto Label_00FF;
            }
            if (data3.Num > 0)
            {
                goto Label_00FF;
            }
            goto Label_024A;
        Label_00FF:
            if (((long) data2.Param.value) <= num4)
            {
                goto Label_011C;
            }
            num5 = num6;
            goto Label_024A;
        Label_011C:
            num7 = (int) (num4 / ((long) data2.Param.value));
            num8 = Mathf.Min(data2.Num, num7);
            num9 = data2.Param.value * num8;
            num10 = data3.Param.value;
            num11 = (long) Mathf.Abs((float) (num4 - ((long) num9)));
            num12 = (long) Mathf.Abs((float) (num4 - ((long) num10)));
            if (num11 <= num12)
            {
                goto Label_0225;
            }
            if (this.mSelectExpItems.ContainsKey(data3.Param.iname) == null)
            {
                goto Label_0204;
            }
            num13 = data3.Num - this.mSelectExpItems[data3.Param.iname];
            if (num13 <= 0)
            {
                goto Label_0225;
            }
            num16 = dictionary[str];
            (dictionary = this.mSelectExpItems)[str = data3.Param.iname] = num16 + 1;
            num4 = 0L;
            goto Label_0262;
            goto Label_0225;
        Label_0204:
            this.mSelectExpItems.Add(data3.Param.iname, 1);
            num4 = 0L;
            goto Label_0262;
        Label_0225:
            num4 -= (long) num9;
            this.mSelectExpItems.Add(data2.Param.iname, num8);
            num5 = num6;
        Label_024A:
            num6 += 1;
        Label_0250:
            if (num6 < this.mCacheExpItemList.Count)
            {
                goto Label_0094;
            }
        Label_0262:
            if (num4 <= 0L)
            {
                goto Label_031C;
            }
            data4 = this.mCacheExpItemList[num5];
            if (data4 == null)
            {
                goto Label_031C;
            }
            if (data4.Num <= 0)
            {
                goto Label_031C;
            }
            if (this.mSelectExpItems.ContainsKey(data4.Param.iname) == null)
            {
                goto Label_0304;
            }
            num14 = data4.Num - this.mSelectExpItems[data4.Param.iname];
            if (num14 <= 0)
            {
                goto Label_031C;
            }
            num16 = dictionary2[str];
            (dictionary2 = this.mSelectExpItems)[str = data4.Param.iname] = num16 + 1;
            goto Label_031C;
        Label_0304:
            this.mSelectExpItems.Add(data4.Param.iname, 1);
        Label_031C:
            if (this.mSelectExpItems.Count <= 0)
            {
                goto Label_03C2;
            }
            num15 = 0;
            goto Label_03B0;
        Label_0335:
            obj2 = this.mItems[num15];
            data5 = DataSource.FindDataOfClass<ItemData>(obj2, null);
            if (data5 != null)
            {
                goto Label_035A;
            }
            goto Label_03AA;
        Label_035A:
            if (this.mSelectExpItems.ContainsKey(data5.Param.iname) == null)
            {
                goto Label_03AA;
            }
            item = obj2.GetComponent<UnitLevelUpListItem>();
            if ((item != null) == null)
            {
                goto Label_03AA;
            }
            item.SetUseExpItemSliderValue(this.mSelectExpItems[data5.Param.iname]);
        Label_03AA:
            num15 += 1;
        Label_03B0:
            if (num15 < this.mItems.Count)
            {
                goto Label_0335;
            }
        Label_03C2:
            this.RefreshFinishedStatus();
            return;
        }

        private void Close()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0);
            return;
        }

        private unsafe void Init()
        {
            char[] chArray1;
            UnitData data;
            int num;
            int num2;
            string str;
            string[] strArray;
            List<string> list;
            List<ItemData> list2;
            UnitEnhanceV3 ev;
            int num3;
            ItemData data2;
            GameObject obj2;
            UnitLevelUpListItem item;
            int num4;
            int num5;
            int num6;
            if ((this.ListParent == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.ListItemTemplate == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            this.master = MonoSingleton<GameManager>.Instance.MasterParam;
            if (this.master != null)
            {
                goto Label_0040;
            }
            return;
        Label_0040:
            this.player = MonoSingleton<GameManager>.Instance.Player;
            if (this.player != null)
            {
                goto Label_005C;
            }
            return;
        Label_005C:
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_01A6;
            }
            num = data.GetExp();
            num2 = num + data.GetNextExp();
            if ((this.CurrentLevel != null) == null)
            {
                goto Label_00AA;
            }
            this.CurrentLevel.set_text(&data.Lv.ToString());
        Label_00AA:
            if ((this.FinishedLevel != null) == null)
            {
                goto Label_00D1;
            }
            this.FinishedLevel.set_text(this.CurrentLevel.get_text());
        Label_00D1:
            if ((this.MaxLevel != null) == null)
            {
                goto Label_0107;
            }
            this.MaxLevel.set_text("/" + &data.GetLevelCap(0).ToString());
        Label_0107:
            if ((this.NextExp != null) == null)
            {
                goto Label_0132;
            }
            this.NextExp.set_text(&data.GetNextExp().ToString());
        Label_0132:
            if (num2 > 0)
            {
                goto Label_013B;
            }
            num2 = 1;
        Label_013B:
            if ((this.LevelGauge != null) == null)
            {
                goto Label_0166;
            }
            this.LevelGauge.AnimateValue(Mathf.Clamp01(((float) num) / ((float) num2)), 0f);
        Label_0166:
            if ((this.GetAllExp != null) == null)
            {
                goto Label_0187;
            }
            this.GetAllExp.set_text("0");
        Label_0187:
            this.mOriginUnit = data;
            this.mExp = data.Exp;
            this.mLv = data.Lv;
        Label_01A6:
            chArray1 = new char[] { 0x7c };
            list = new List<string>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.UNIT_LEVELUP_EXPITEM_CHECKS, string.Empty).Split(chArray1));
            list2 = this.player.Items;
            ev = DataSource.FindDataOfClass<UnitEnhanceV3>(base.get_gameObject(), null);
            if ((ev != null) == null)
            {
                goto Label_0203;
            }
            list2 = ev.TmpExpItems;
        Label_0203:
            num3 = 0;
            goto Label_0347;
        Label_020B:
            data2 = list2[num3];
            if (((data2 == null) || (data2.Param == null)) || (data2.Param.type != 6))
            {
                goto Label_0341;
            }
            if (data2.Num > 0)
            {
                goto Label_024D;
            }
            goto Label_0341;
        Label_024D:
            obj2 = Object.Instantiate<GameObject>(this.ListItemTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_0341;
            }
            DataSource.Bind<ItemData>(obj2, data2);
            obj2.get_transform().SetParent(this.ListParent, 0);
            item = obj2.GetComponent<UnitLevelUpListItem>();
            if ((item != null) == null)
            {
                goto Label_0334;
            }
            item.OnSelect = new UnitLevelUpListItem.SelectExpItem(this.RefreshExpSelectItems);
            item.ChangeUseMax = new UnitLevelUpListItem.ChangeToggleEvent(this.RefreshUseMaxItems);
            item.OnCheck = new UnitLevelUpListItem.CheckSliderValue(this.OnCheck);
            this.mUnitLevelupLists.Add(item);
            if ((list == null) || (list.Count <= 0))
            {
                goto Label_0313;
            }
            item.SetUseMax((list.IndexOf(data2.Param.iname) == -1) == 0);
        Label_0313:
            if (item.IsUseMax() == null)
            {
                goto Label_032C;
            }
            this.mCacheExpItemList.Add(data2);
        Label_032C:
            obj2.SetActive(1);
        Label_0334:
            this.mItems.Add(obj2);
        Label_0341:
            num3 += 1;
        Label_0347:
            if (num3 < list2.Count)
            {
                goto Label_020B;
            }
            if ((this.mCacheExpItemList == null) || (this.mCacheExpItemList.Count <= 0))
            {
                goto Label_0399;
            }
            if (<>f__am$cache1A != null)
            {
                goto Label_038F;
            }
            <>f__am$cache1A = new Comparison<ItemData>(UnitLevelUpWindow.<Init>m__46C);
        Label_038F:
            this.mCacheExpItemList.Sort(<>f__am$cache1A);
        Label_0399:
            this.MaxBtn.set_interactable((this.mCacheExpItemList == null) ? 0 : (this.mCacheExpItemList.Count > 0));
            this.DecideBtn.set_interactable((this.mSelectExpItems == null) ? 0 : (this.mSelectExpItems.Count > 0));
            return;
        }

        private void OnCancel()
        {
            int num;
            UnitLevelUpListItem item;
            if (this.mSelectExpItems.Count <= 0)
            {
                goto Label_0062;
            }
            num = 0;
            goto Label_0040;
        Label_0018:
            item = this.mItems[num].GetComponent<UnitLevelUpListItem>();
            if ((item != null) == null)
            {
                goto Label_003C;
            }
            item.Reset();
        Label_003C:
            num += 1;
        Label_0040:
            if (num < this.mItems.Count)
            {
                goto Label_0018;
            }
            this.mSelectExpItems.Clear();
            this.RefreshFinishedStatus();
        Label_0062:
            return;
        }

        private unsafe int OnCheck(string iname, int num)
        {
            long num2;
            long num3;
            string str;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            ItemParam param;
            ItemParam param2;
            long num4;
            int num5;
            if (string.IsNullOrEmpty(iname) != null)
            {
                goto Label_0011;
            }
            if (num != null)
            {
                goto Label_0013;
            }
        Label_0011:
            return -1;
        Label_0013:
            if (this.mSelectExpItems.ContainsKey(iname) == null)
            {
                goto Label_0038;
            }
            if (this.mSelectExpItems[iname] <= num)
            {
                goto Label_0038;
            }
            return -1;
        Label_0038:
            num2 = (long) (this.mOriginUnit.GetGainExpCap() - this.mOriginUnit.Exp);
            num3 = 0L;
            enumerator = this.mSelectExpItems.Keys.GetEnumerator();
        Label_0065:
            try
            {
                goto Label_00B0;
            Label_006A:
                str = &enumerator.Current;
                if ((str == iname) == null)
                {
                    goto Label_0083;
                }
                goto Label_00B0;
            Label_0083:
                param = this.master.GetItemParam(str);
                if (param == null)
                {
                    goto Label_00B0;
                }
                num3 += (long) (param.value * this.mSelectExpItems[str]);
            Label_00B0:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_006A;
                }
                goto Label_00CD;
            }
            finally
            {
            Label_00C1:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_00CD:
            num2 -= num3;
            param2 = this.master.GetItemParam(iname);
            if (param2 != null)
            {
                goto Label_00E8;
            }
            return -1;
        Label_00E8:
            if (this.player.GetItemAmount(iname) != null)
            {
                goto Label_00FB;
            }
            return -1;
        Label_00FB:
            num4 = (long) (param2.value * num);
            if (num2 >= num4)
            {
                goto Label_0124;
            }
            return Mathf.CeilToInt(((float) num2) / ((float) param2.value));
        Label_0124:
            return num;
        }

        private void OnDecide()
        {
            if (this.OnDecideEvent == null)
            {
                goto Label_001C;
            }
            this.OnDecideEvent(this.mSelectExpItems);
        Label_001C:
            this.Close();
            return;
        }

        private void OnDecideConfirm()
        {
            GameObject obj2;
            GameObject obj3;
            UnitLevelUpConfirmWindow window;
            obj2 = AssetManager.Load<GameObject>(CONFIRM_PATH);
            if ((obj2 != null) == null)
            {
                goto Label_005B;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_005B;
            }
            window = obj3.GetComponentInChildren<UnitLevelUpConfirmWindow>();
            if ((window != null) == null)
            {
                goto Label_005B;
            }
            window.Refresh(this.mSelectExpItems);
            window.OnDecideEvent = new UnitLevelUpConfirmWindow.ConfirmDecideEvent(this.OnDecide);
        Label_005B:
            return;
        }

        private void OnMax()
        {
            int num;
            UnitLevelUpListItem item;
            if (this.mCacheExpItemList == null)
            {
                goto Label_001C;
            }
            if (this.mCacheExpItemList.Count >= 0)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            num = 0;
            goto Label_004C;
        Label_0024:
            item = this.mItems[num].GetComponent<UnitLevelUpListItem>();
            if ((item != null) == null)
            {
                goto Label_0048;
            }
            item.Reset();
        Label_0048:
            num += 1;
        Label_004C:
            if (num < this.mItems.Count)
            {
                goto Label_0024;
            }
            this.CalcCanUnitLevelUpMax();
            return;
        }

        private void RefreshExpSelectItems(string iname, int value)
        {
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.player.GetItemAmount(iname) != null)
            {
                goto Label_001E;
            }
            return;
        Label_001E:
            if (this.mSelectExpItems.ContainsKey(iname) != null)
            {
                goto Label_0041;
            }
            this.mSelectExpItems.Add(iname, value);
            goto Label_004E;
        Label_0041:
            this.mSelectExpItems[iname] = value;
        Label_004E:
            this.RefreshFinishedStatus();
            return;
        }

        private unsafe void RefreshFinishedStatus()
        {
            int num;
            string str;
            Dictionary<string, int>.KeyCollection.Enumerator enumerator;
            ItemData data;
            int num2;
            int num3;
            int num4;
            UnitLevelUpListItem item;
            List<UnitLevelUpListItem>.Enumerator enumerator2;
            float num5;
            float num6;
            float num7;
            int num8;
            int num9;
            int num10;
            int num11;
            if ((this.mSelectExpItems != null) && (this.mSelectExpItems.Count > 0))
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            num = 0;
            enumerator = this.mSelectExpItems.Keys.GetEnumerator();
        Label_0030:
            try
            {
                goto Label_008C;
            Label_0035:
                str = &enumerator.Current;
                data = this.player.FindItemDataByItemID(str);
                if (data == null)
                {
                    goto Label_008C;
                }
                num2 = this.mSelectExpItems[str];
                if (num2 == null)
                {
                    goto Label_008C;
                }
                if (num2 <= data.Num)
                {
                    goto Label_0077;
                }
                goto Label_008C;
            Label_0077:
                num3 = data.Param.value * num2;
                num += num3;
            Label_008C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0035;
                }
                goto Label_00A9;
            }
            finally
            {
            Label_009D:
                ((Dictionary<string, int>.KeyCollection.Enumerator) enumerator).Dispose();
            }
        Label_00A9:
            num4 = this.mOriginUnit.GetGainExpCap(this.player.Lv);
            this.mExp = Math.Min(this.mOriginUnit.Exp + num, num4);
            this.mLv = this.master.CalcUnitLevel(this.mExp, this.mOriginUnit.GetLevelCap(0));
            enumerator2 = this.mUnitLevelupLists.GetEnumerator();
        Label_010B:
            try
            {
                goto Label_0130;
            Label_0110:
                item = &enumerator2.Current;
                item.SetInputLock(((this.mExp < num4) == 0) == 0);
            Label_0130:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0110;
                }
                goto Label_014E;
            }
            finally
            {
            Label_0141:
                ((List<UnitLevelUpListItem>.Enumerator) enumerator2).Dispose();
            }
        Label_014E:
            if ((this.FinishedLevel != null) == null)
            {
                goto Label_01DC;
            }
            this.FinishedLevel.set_text(&this.mLv.ToString());
            if (this.mLv < this.mOriginUnit.GetLevelCap(0))
            {
                goto Label_01A1;
            }
            this.FinishedLevel.set_color(Color.get_red());
            goto Label_01DC;
        Label_01A1:
            if (this.mLv <= this.mOriginUnit.Lv)
            {
                goto Label_01CC;
            }
            this.FinishedLevel.set_color(Color.get_green());
            goto Label_01DC;
        Label_01CC:
            this.FinishedLevel.set_color(Color.get_white());
        Label_01DC:
            if ((this.AddLevelGauge != null) == null)
            {
                goto Label_0285;
            }
            if ((this.mExp != this.mOriginUnit.GetExp()) && (num != null))
            {
                goto Label_0223;
            }
            this.AddLevelGauge.AnimateValue(0f, 0f);
            goto Label_0285;
        Label_0223:
            num5 = (float) (this.mExp - this.master.GetUnitLevelExp(this.mOriginUnit.Lv));
            num6 = (float) this.master.GetUnitNextExp(this.mOriginUnit.Lv);
            num7 = Mathf.Min(1f, Mathf.Clamp01(((float) num5) / num6));
            this.AddLevelGauge.AnimateValue(num7, 0f);
        Label_0285:
            if ((this.NextExp != null) == null)
            {
                goto Label_0350;
            }
            num8 = 0;
            if (this.mExp >= this.mOriginUnit.GetGainExpCap(this.player.Lv))
            {
                goto Label_033E;
            }
            num9 = this.master.GetUnitLevelExp(this.mLv);
            num10 = this.master.GetUnitNextExp(this.mLv);
            if (this.mExp < num9)
            {
                goto Label_0313;
            }
            num10 = this.master.GetUnitNextExp(Math.Min(this.mOriginUnit.GetLevelCap(0), this.mLv + 1));
        Label_0313:
            num11 = this.mExp - num9;
            num8 = (num10 <= num11) ? 0 : (num10 - num11);
            num8 = Math.Max(0, num8);
        Label_033E:
            this.NextExp.set_text(&num8.ToString());
        Label_0350:
            if ((this.GetAllExp != null) == null)
            {
                goto Label_0373;
            }
            this.GetAllExp.set_text(&num.ToString());
        Label_0373:
            this.DecideBtn.set_interactable(num > 0);
            return;
        }

        private void RefreshUseMaxItems(string iname, bool is_on)
        {
            <RefreshUseMaxItems>c__AnonStorey3D2 storeyd;
            storeyd = new <RefreshUseMaxItems>c__AnonStorey3D2();
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            storeyd.item = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
            if (is_on != null)
            {
                goto Label_0072;
            }
            if (this.mCacheExpItemList.FindIndex(new Predicate<ItemData>(storeyd.<>m__46D)) == -1)
            {
                goto Label_009F;
            }
            this.mCacheExpItemList.RemoveAt(this.mCacheExpItemList.FindIndex(new Predicate<ItemData>(storeyd.<>m__46E)));
            goto Label_009F;
        Label_0072:
            if (this.mCacheExpItemList.Find(new Predicate<ItemData>(storeyd.<>m__46F)) != null)
            {
                goto Label_009F;
            }
            this.mCacheExpItemList.Add(storeyd.item);
        Label_009F:
            if (<>f__am$cache1B != null)
            {
                goto Label_00BD;
            }
            <>f__am$cache1B = new Comparison<ItemData>(UnitLevelUpWindow.<RefreshUseMaxItems>m__470);
        Label_00BD:
            this.mCacheExpItemList.Sort(<>f__am$cache1B);
            this.SaveSelectUseMax();
            this.MaxBtn.set_interactable((this.mCacheExpItemList == null) ? 0 : (this.mCacheExpItemList.Count > 0));
            return;
        }

        private void SaveSelectUseMax()
        {
            string[] strArray;
            int num;
            string str;
            strArray = new string[this.mCacheExpItemList.Count];
            num = 0;
            goto Label_0035;
        Label_0018:
            strArray[num] = this.mCacheExpItemList[num].Param.iname;
            num += 1;
        Label_0035:
            if (num < this.mCacheExpItemList.Count)
            {
                goto Label_0018;
            }
            str = (strArray == null) ? string.Empty : string.Join("|", strArray);
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.UNIT_LEVELUP_EXPITEM_CHECKS, str, 1);
            return;
        }

        private void Start()
        {
            if ((this.ListItemTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.ListItemTemplate.SetActive(0);
        Label_001D:
            if ((this.DecideBtn != null) == null)
            {
                goto Label_004A;
            }
            this.DecideBtn.get_onClick().AddListener(new UnityAction(this, this.OnDecideConfirm));
        Label_004A:
            if ((this.CancelBtn != null) == null)
            {
                goto Label_0077;
            }
            this.CancelBtn.get_onClick().AddListener(new UnityAction(this, this.OnCancel));
        Label_0077:
            if ((this.MaxBtn != null) == null)
            {
                goto Label_00A4;
            }
            this.MaxBtn.get_onClick().AddListener(new UnityAction(this, this.OnMax));
        Label_00A4:
            this.Init();
            return;
        }

        [CompilerGenerated]
        private sealed class <RefreshUseMaxItems>c__AnonStorey3D2
        {
            internal ItemData item;

            public <RefreshUseMaxItems>c__AnonStorey3D2()
            {
                base..ctor();
                return;
            }

            internal bool <>m__46D(ItemData p)
            {
                return (p.ItemID == this.item.ItemID);
            }

            internal bool <>m__46E(ItemData p)
            {
                return (p.ItemID == this.item.ItemID);
            }

            internal bool <>m__46F(ItemData p)
            {
                return (p.ItemID == this.item.ItemID);
            }
        }

        public delegate void OnUnitLevelupEvent(Dictionary<string, int> dict);
    }
}

