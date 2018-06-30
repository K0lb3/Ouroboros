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
    public class ArtifactLevelUpWindow : MonoBehaviour, IFlowInterface
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
        private ArtifactData mOriginArtifact;
        private List<GameObject> mItems;
        private List<ArtifactLevelUpListItem> mArtifactLevelupLists;
        private float mExpStart;
        private float mExpEnd;
        private float mExpAnimTime;
        private Dictionary<string, int> mSelectExpItems;
        public OnArtifactLevelupEvent OnDecideEvent;
        private List<ItemData> mCacheExpItemList;
        public static readonly string CONFIRM_PATH;
        [CompilerGenerated]
        private static Comparison<ItemData> <>f__am$cache18;
        [CompilerGenerated]
        private static Comparison<ItemData> <>f__am$cache19;

        static ArtifactLevelUpWindow()
        {
            CONFIRM_PATH = "UI/ArtifactLevelUpConfirmWindow";
            return;
        }

        public ArtifactLevelUpWindow()
        {
            this.mItems = new List<GameObject>();
            this.mArtifactLevelupLists = new List<ArtifactLevelUpListItem>();
            this.mSelectExpItems = new Dictionary<string, int>();
            this.mCacheExpItemList = new List<ItemData>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <Init>m__27D(ItemData a, ItemData b)
        {
            return (b.Param.value - a.Param.value);
        }

        [CompilerGenerated]
        private static int <RefreshUseMaxItems>m__281(ItemData a, ItemData b)
        {
            return (b.Param.value - a.Param.value);
        }

        public void Activated(int pinID)
        {
        }

        private unsafe void CalcCanArtifactLevelUpMax()
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
            ItemData data4;
            int num10;
            int num11;
            ItemData data5;
            bool flag;
            int num12;
            ItemData data6;
            int num13;
            GameObject obj2;
            ItemData data7;
            ArtifactLevelUpListItem item;
            Dictionary<string, int> dictionary;
            string str;
            int num14;
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
            num4 = (long) Mathf.Min((float) (this.mOriginArtifact.GetGainExpCap() - this.mOriginArtifact.Exp), (float) num);
            this.mSelectExpItems.Clear();
            num5 = 0;
            num6 = 0;
            goto Label_0180;
        Label_0099:
            if (num4 > 0L)
            {
                goto Label_00AB;
            }
            num4 = 0L;
            goto Label_0192;
        Label_00AB:
            data2 = this.mCacheExpItemList[num6];
            if ((data2 != null) || (data2.Num > 0))
            {
                goto Label_00D3;
            }
            goto Label_017A;
        Label_00D3:
            data3 = this.mCacheExpItemList[num5];
            if (((num6 == num5) || (data3 != null)) || (data3.Num > 0))
            {
                goto Label_0104;
            }
            goto Label_017A;
        Label_0104:
            if (((long) data2.Param.value) <= num4)
            {
                goto Label_0121;
            }
            num5 = num6;
            goto Label_017A;
        Label_0121:
            num7 = (int) (num4 / ((long) data2.Param.value));
            num8 = Mathf.Min(data2.Num, num7);
            num9 = data2.Param.value * num8;
            num4 -= (long) num9;
            this.mSelectExpItems.Add(data2.Param.iname, num8);
            num5 = num6;
        Label_017A:
            num6 += 1;
        Label_0180:
            if (num6 < this.mCacheExpItemList.Count)
            {
                goto Label_0099;
            }
        Label_0192:
            if (num4 <= 0L)
            {
                goto Label_0330;
            }
            data4 = this.mCacheExpItemList[num5];
            if ((data4 == null) || (data4.Num <= 0))
            {
                goto Label_0330;
            }
            if (this.mSelectExpItems.ContainsKey(data4.Param.iname) == null)
            {
                goto Label_0318;
            }
            num10 = data4.Num - this.mSelectExpItems[data4.Param.iname];
            if (num10 <= 0)
            {
                goto Label_0234;
            }
            num14 = dictionary[str];
            (dictionary = this.mSelectExpItems)[str = data4.Param.iname] = num14 + 1;
            goto Label_0313;
        Label_0234:
            num11 = this.mCacheExpItemList.Count - 2;
            goto Label_030B;
        Label_0248:
            data5 = this.mCacheExpItemList[num11];
            flag = this.mSelectExpItems.ContainsKey(data5.ItemID);
            num12 = (flag == null) ? 0 : this.mSelectExpItems[data5.ItemID];
            if ((data5.Num - (num12 + 1)) <= 0)
            {
                goto Label_0305;
            }
            if (flag == null)
            {
                goto Label_02C0;
            }
            this.mSelectExpItems[data5.ItemID] = num12 + 1;
            goto Label_02D3;
        Label_02C0:
            this.mSelectExpItems.Add(data5.ItemID, 1);
        Label_02D3:
            data6 = this.mCacheExpItemList[this.mCacheExpItemList.Count - 1];
            this.mSelectExpItems.Remove(data6.ItemID);
            goto Label_0313;
        Label_0305:
            num11 -= 1;
        Label_030B:
            if (num11 >= 0)
            {
                goto Label_0248;
            }
        Label_0313:
            goto Label_0330;
        Label_0318:
            this.mSelectExpItems.Add(data4.Param.iname, 1);
        Label_0330:
            if (this.mSelectExpItems.Count <= 0)
            {
                goto Label_03D6;
            }
            num13 = 0;
            goto Label_03C4;
        Label_0349:
            obj2 = this.mItems[num13];
            data7 = DataSource.FindDataOfClass<ItemData>(obj2, null);
            if (data7 != null)
            {
                goto Label_036E;
            }
            goto Label_03BE;
        Label_036E:
            if (this.mSelectExpItems.ContainsKey(data7.Param.iname) == null)
            {
                goto Label_03BE;
            }
            item = obj2.GetComponent<ArtifactLevelUpListItem>();
            if ((item != null) == null)
            {
                goto Label_03BE;
            }
            item.SetUseExpItemSliderValue(this.mSelectExpItems[data7.Param.iname]);
        Label_03BE:
            num13 += 1;
        Label_03C4:
            if (num13 < this.mItems.Count)
            {
                goto Label_0349;
            }
        Label_03D6:
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
            ArtifactData data;
            int num;
            int num2;
            float num3;
            float num4;
            string str;
            string[] strArray;
            List<string> list;
            List<ItemData> list2;
            ArtifactWindow window;
            int num5;
            ItemData data2;
            GameObject obj2;
            ArtifactLevelUpListItem item;
            OInt num6;
            int num7;
            int num8;
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
            data = DataSource.FindDataOfClass<ArtifactData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_01BA;
            }
            if ((this.CurrentLevel != null) == null)
            {
                goto Label_009A;
            }
            this.CurrentLevel.set_text(&data.Lv.ToString());
        Label_009A:
            if ((this.FinishedLevel != null) == null)
            {
                goto Label_00C1;
            }
            this.FinishedLevel.set_text(this.CurrentLevel.get_text());
        Label_00C1:
            if ((this.MaxLevel != null) == null)
            {
                goto Label_00F6;
            }
            this.MaxLevel.set_text("/" + &data.GetLevelCap().ToString());
        Label_00F6:
            if ((this.NextExp != null) == null)
            {
                goto Label_0121;
            }
            this.NextExp.set_text(&data.GetNextExp().ToString());
        Label_0121:
            if ((this.LevelGauge != null) == null)
            {
                goto Label_0192;
            }
            num = data.GetTotalExpFromLevel(data.Lv);
            num3 = (float) (data.GetTotalExpFromLevel(data.Lv + 1) - num);
            num4 = (float) (data.Exp - num);
            if (num3 > 0f)
            {
                goto Label_0179;
            }
            num3 = 1f;
        Label_0179:
            this.LevelGauge.AnimateValue(Mathf.Clamp01(num4 / num3), 0f);
        Label_0192:
            if ((this.GetAllExp != null) == null)
            {
                goto Label_01B3;
            }
            this.GetAllExp.set_text("0");
        Label_01B3:
            this.mOriginArtifact = data;
        Label_01BA:
            chArray1 = new char[] { 0x7c };
            list = new List<string>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.ARTIFACT_BULK_LEVELUP, string.Empty).Split(chArray1));
            list2 = this.player.Items;
            window = DataSource.FindDataOfClass<ArtifactWindow>(base.get_gameObject(), null);
            if ((window != null) == null)
            {
                goto Label_0219;
            }
            list2 = window.TmpItems;
        Label_0219:
            num5 = 0;
            goto Label_035E;
        Label_0221:
            data2 = list2[num5];
            if (((data2 == null) || (data2.Param == null)) || (data2.Param.type != 13))
            {
                goto Label_0358;
            }
            if (data2.Num > 0)
            {
                goto Label_0264;
            }
            goto Label_0358;
        Label_0264:
            obj2 = Object.Instantiate<GameObject>(this.ListItemTemplate);
            if ((obj2 != null) == null)
            {
                goto Label_0358;
            }
            DataSource.Bind<ItemData>(obj2, data2);
            obj2.get_transform().SetParent(this.ListParent, 0);
            item = obj2.GetComponent<ArtifactLevelUpListItem>();
            if ((item != null) == null)
            {
                goto Label_034B;
            }
            item.OnSelect = new ArtifactLevelUpListItem.SelectExpItem(this.RefreshExpSelectItems);
            item.ChangeUseMax = new ArtifactLevelUpListItem.ChangeToggleEvent(this.RefreshUseMaxItems);
            item.OnCheck = new ArtifactLevelUpListItem.CheckSliderValue(this.OnCheck);
            this.mArtifactLevelupLists.Add(item);
            if ((list == null) || (list.Count <= 0))
            {
                goto Label_032A;
            }
            item.SetUseMax((list.IndexOf(data2.Param.iname) == -1) == 0);
        Label_032A:
            if (item.IsUseMax() == null)
            {
                goto Label_0343;
            }
            this.mCacheExpItemList.Add(data2);
        Label_0343:
            obj2.SetActive(1);
        Label_034B:
            this.mItems.Add(obj2);
        Label_0358:
            num5 += 1;
        Label_035E:
            if (num5 < list2.Count)
            {
                goto Label_0221;
            }
            if ((this.mCacheExpItemList == null) || (this.mCacheExpItemList.Count <= 0))
            {
                goto Label_03B0;
            }
            if (<>f__am$cache18 != null)
            {
                goto Label_03A6;
            }
            <>f__am$cache18 = new Comparison<ItemData>(ArtifactLevelUpWindow.<Init>m__27D);
        Label_03A6:
            this.mCacheExpItemList.Sort(<>f__am$cache18);
        Label_03B0:
            this.MaxBtn.set_interactable((this.mCacheExpItemList == null) ? 0 : (this.mCacheExpItemList.Count > 0));
            this.DecideBtn.set_interactable((this.mSelectExpItems == null) ? 0 : (this.mSelectExpItems.Count > 0));
            return;
        }

        private void OnCancel()
        {
            int num;
            ArtifactLevelUpListItem item;
            if (this.mSelectExpItems.Count <= 0)
            {
                goto Label_0062;
            }
            num = 0;
            goto Label_0040;
        Label_0018:
            item = this.mItems[num].GetComponent<ArtifactLevelUpListItem>();
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
            num2 = (long) (this.mOriginArtifact.GetGainExpCap() - this.mOriginArtifact.Exp);
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
            ArtifactLevelUpConfirmWindow window;
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
            window = obj3.GetComponentInChildren<ArtifactLevelUpConfirmWindow>();
            if ((window != null) == null)
            {
                goto Label_005B;
            }
            window.Refresh(this.mSelectExpItems);
            window.OnDecideEvent = new ArtifactLevelUpConfirmWindow.ConfirmDecideEvent(this.OnDecide);
        Label_005B:
            return;
        }

        private void OnMax()
        {
            int num;
            ArtifactLevelUpListItem item;
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
            item = this.mItems[num].GetComponent<ArtifactLevelUpListItem>();
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
            this.CalcCanArtifactLevelUpMax();
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
            int num5;
            ArtifactData data2;
            ArtifactLevelUpListItem item;
            List<ArtifactLevelUpListItem>.Enumerator enumerator2;
            int num6;
            int num7;
            float num8;
            float num9;
            int num10;
            int num11;
            int num12;
            int num13;
            OInt num14;
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
            num4 = this.mOriginArtifact.GetGainExpCap();
            num5 = Math.Min(this.mOriginArtifact.Exp + num, num4);
            data2 = this.mOriginArtifact.Copy();
            data2.GainExp(num);
            enumerator2 = this.mArtifactLevelupLists.GetEnumerator();
        Label_00EE:
            try
            {
                goto Label_010F;
            Label_00F3:
                item = &enumerator2.Current;
                item.SetInputLock(((num5 < num4) == 0) == 0);
            Label_010F:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_00F3;
                }
                goto Label_012D;
            }
            finally
            {
            Label_0120:
                ((List<ArtifactLevelUpListItem>.Enumerator) enumerator2).Dispose();
            }
        Label_012D:
            if ((this.FinishedLevel != null) == null)
            {
                goto Label_01D0;
            }
            this.FinishedLevel.set_text(&data2.Lv.ToString());
            if (data2.Lv < this.mOriginArtifact.GetLevelCap())
            {
                goto Label_018A;
            }
            this.FinishedLevel.set_color(Color.get_red());
            goto Label_01D0;
        Label_018A:
            if (data2.Lv <= this.mOriginArtifact.Lv)
            {
                goto Label_01C0;
            }
            this.FinishedLevel.set_color(Color.get_green());
            goto Label_01D0;
        Label_01C0:
            this.FinishedLevel.set_color(Color.get_white());
        Label_01D0:
            if ((this.AddLevelGauge != null) == null)
            {
                goto Label_028C;
            }
            if ((num5 != this.mOriginArtifact.Exp) && (num != null))
            {
                goto Label_0213;
            }
            this.AddLevelGauge.AnimateValue(0f, 0f);
            goto Label_028C;
        Label_0213:
            num6 = this.mOriginArtifact.GetTotalExpFromLevel(this.mOriginArtifact.Lv);
            num8 = (float) (this.mOriginArtifact.GetTotalExpFromLevel(this.mOriginArtifact.Lv + 1) - num6);
            num9 = (float) (num5 - num6);
            if (num8 > 0f)
            {
                goto Label_0272;
            }
            num8 = 1f;
        Label_0272:
            this.AddLevelGauge.AnimateValue(Mathf.Clamp01(num9 / num8), 0f);
        Label_028C:
            if ((this.NextExp != null) == null)
            {
                goto Label_02FD;
            }
            num10 = 0;
            if (num5 >= this.mOriginArtifact.GetGainExpCap())
            {
                goto Label_02EB;
            }
            num11 = data2.Exp;
            num12 = data2.GetNextExp();
            num13 = num5 - num11;
            num10 = (num12 <= num13) ? 0 : (num12 - num13);
            num10 = Math.Max(0, num10);
        Label_02EB:
            this.NextExp.set_text(&num10.ToString());
        Label_02FD:
            if ((this.GetAllExp != null) == null)
            {
                goto Label_0320;
            }
            this.GetAllExp.set_text(&num.ToString());
        Label_0320:
            this.DecideBtn.set_interactable(num > 0);
            return;
        }

        private void RefreshUseMaxItems(string iname, bool is_on)
        {
            <RefreshUseMaxItems>c__AnonStorey2F6 storeyf;
            storeyf = new <RefreshUseMaxItems>c__AnonStorey2F6();
            if (string.IsNullOrEmpty(iname) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            storeyf.item = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(iname);
            if (is_on != null)
            {
                goto Label_0072;
            }
            if (this.mCacheExpItemList.FindIndex(new Predicate<ItemData>(storeyf.<>m__27E)) == -1)
            {
                goto Label_009F;
            }
            this.mCacheExpItemList.RemoveAt(this.mCacheExpItemList.FindIndex(new Predicate<ItemData>(storeyf.<>m__27F)));
            goto Label_009F;
        Label_0072:
            if (this.mCacheExpItemList.Find(new Predicate<ItemData>(storeyf.<>m__280)) != null)
            {
                goto Label_009F;
            }
            this.mCacheExpItemList.Add(storeyf.item);
        Label_009F:
            if (<>f__am$cache19 != null)
            {
                goto Label_00BD;
            }
            <>f__am$cache19 = new Comparison<ItemData>(ArtifactLevelUpWindow.<RefreshUseMaxItems>m__281);
        Label_00BD:
            this.mCacheExpItemList.Sort(<>f__am$cache19);
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
            PlayerPrefsUtility.SetString(PlayerPrefsUtility.ARTIFACT_BULK_LEVELUP, str, 1);
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
        private sealed class <RefreshUseMaxItems>c__AnonStorey2F6
        {
            internal ItemData item;

            public <RefreshUseMaxItems>c__AnonStorey2F6()
            {
                base..ctor();
                return;
            }

            internal bool <>m__27E(ItemData p)
            {
                return (p.ItemID == this.item.ItemID);
            }

            internal bool <>m__27F(ItemData p)
            {
                return (p.ItemID == this.item.ItemID);
            }

            internal bool <>m__280(ItemData p)
            {
                return (p.ItemID == this.item.ItemID);
            }
        }

        public delegate void OnArtifactLevelupEvent(Dictionary<string, int> dict);
    }
}

