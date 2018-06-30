namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [Pin(200, "Refresh", 0, 200), Pin(20, "Clear Selection", 0, 20), Pin(100, "Selected", 1, 100), Pin(0x65, "Detail Selected", 1, 0x65), Pin(0xc9, "Update Selection", 0, 0xc9)]
    public class ArtifactList : UIBehaviour, IFlowInterface, ISortableList
    {
        private const int PIN_ID_CLEAR_SELECTION = 20;
        private const int PIN_ID_SELECTED = 100;
        private const int PIN_ID_DETAIL_SELECTED = 0x65;
        private const int PIN_ID_REFRESH = 200;
        private const int PIN_ID_UPDATE_SELECTION = 0xc9;
        public ListSource Source;
        [BitMask]
        public KakeraHideFlags KakeraFlags;
        public GameObject ListItem;
        private int mPage;
        private int mMaxPages;
        private int mPageSize;
        private bool mStarted;
        private bool mShouldRefresh;
        private bool mInvokeSelChange;
        public Scrollbar PageScrollBar;
        public Text PageIndex;
        public Text PageIndexMax;
        public Button ForwardButton;
        public Button BackButton;
        public Button ApplyButton;
        public bool UseGridLayout;
        public float CellWidth;
        public float CellHeight;
        public float PaddingX;
        public float PaddingY;
        public float SpacingX;
        public float SpacingY;
        public bool TruncateX;
        public bool TruncateY;
        public string SelectionState;
        public int Item_Normal;
        public int Item_Selected;
        public int Item_Disabled;
        public Text NumSelection;
        public int MaxSelection;
        public bool ShowSelection;
        public bool OnlyEquippable;
        public UnitData TestOwner;
        public GameObject[] ExtraItems;
        public Text TotalDecomposeCost;
        public GameObject ArtifactDetail;
        public GameObject ArtifactDetailRef;
        public GameObject EmptyMessage;
        public Text TotalSellCost;
        public bool ExcludeEquiped;
        private List<GameObject> mItems;
        private Type mDataType;
        private bool mFocusSelection;
        private object[] mData;
        private BaseStatus mTmpVal0;
        private BaseStatus mTmpVal1;
        private int[] mTmpSortVal;
        private string mSortMethod;
        private bool mDescending;
        private bool mEmptyMessageChanged;
        private bool mAutoSelected;
        private List<object> mSelection;
        public Scrollbar AbilityScrollBar;
        public SelectionChangeEvent OnSelectionChange;
        public int MaxCellCount;
        private string[] mFiltersPriority;
        private string[] mFilters;
        [CompilerGenerated]
        private static SelectionChangeEvent <>f__am$cache39;
        [CompilerGenerated]
        private static Predicate<ItemData> <>f__am$cache3A;
        [CompilerGenerated]
        private static Predicate<ItemData> <>f__am$cache3B;
        [CompilerGenerated]
        private static Converter<ArtifactData, object> <>f__am$cache3C;
        [CompilerGenerated]
        private static Comparison<ArtifactData> <>f__am$cache3D;
        [CompilerGenerated]
        private static Comparison<ArtifactParam> <>f__am$cache3E;
        [CompilerGenerated]
        private static Comparison<ArtifactData> <>f__am$cache3F;
        [CompilerGenerated]
        private static Comparison<ArtifactParam> <>f__am$cache40;

        public ArtifactList()
        {
            this.UseGridLayout = 1;
            this.CellWidth = 100f;
            this.CellHeight = 100f;
            this.TruncateY = 1;
            this.Item_Selected = 1;
            this.Item_Disabled = 2;
            this.ExtraItems = new GameObject[0];
            this.mItems = new List<GameObject>(0x20);
            this.mSelection = new List<object>(4);
            if (<>f__am$cache39 != null)
            {
                goto Label_0070;
            }
            <>f__am$cache39 = new SelectionChangeEvent(ArtifactList.<OnSelectionChange>m__282);
        Label_0070:
            this.OnSelectionChange = <>f__am$cache39;
            this.MaxCellCount = 0x40;
            base..ctor();
            return;
        }

        private unsafe void _Refresh()
        {
            object[] objArray1;
            Transform transform;
            GameObject obj2;
            Transform transform2;
            ListItemEvents events;
            GameManager manager;
            object[] objArray;
            List<ArtifactData> list;
            int num;
            int num2;
            UnitData data;
            JobData data2;
            List<ArtifactParam> list2;
            ArtifactParam[] paramArray;
            int num3;
            int num4;
            int num5;
            string str;
            string str2;
            MethodInfo info;
            Exception exception;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            if (this.Source != null)
            {
                goto Label_0020;
            }
            this.mDataType = typeof(ArtifactData);
            goto Label_003C;
        Label_0020:
            if (this.Source != 1)
            {
                goto Label_003C;
            }
            this.mDataType = typeof(ArtifactParam);
        Label_003C:
            if ((this.ListItem == null) == null)
            {
                goto Label_004E;
            }
            return;
        Label_004E:
            this.mPageSize = this.CellCount;
            transform = base.get_transform();
            goto Label_00C4;
        Label_0066:
            obj2 = Object.Instantiate<GameObject>(this.ListItem);
            obj2.get_transform().SetParent(transform, 0);
            this.mItems.Add(obj2);
            events = obj2.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_00C4;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        Label_00C4:
            if (this.mItems.Count < this.mPageSize)
            {
                goto Label_0066;
            }
            if (this.mItems.Count != null)
            {
                goto Label_00EB;
            }
            return;
        Label_00EB:
            manager = MonoSingleton<GameManager>.Instance;
            objArray = null;
            if (this.Source != null)
            {
                goto Label_01F0;
            }
            list = new List<ArtifactData>(manager.Player.Artifacts);
            if ((this.OnlyEquippable == null) || (this.TestOwner == null))
            {
                goto Label_016D;
            }
            num = 0;
            goto Label_015F;
        Label_0131:
            if (list[num].CheckEnableEquip(this.TestOwner, -1) != null)
            {
                goto Label_0159;
            }
            list.RemoveAt(num--);
        Label_0159:
            num += 1;
        Label_015F:
            if (num < list.Count)
            {
                goto Label_0131;
            }
        Label_016D:
            if ((this.ExcludeEquiped == null) || (this.TestOwner == null))
            {
                goto Label_01E2;
            }
            num2 = 0;
            goto Label_01D4;
        Label_018B:
            if ((manager.Player.FindOwner(list[num2], &data, &data2) == null) || (data.UniqueID == this.TestOwner.UniqueID))
            {
                goto Label_01CE;
            }
            list.RemoveAt(num2--);
        Label_01CE:
            num2 += 1;
        Label_01D4:
            if (num2 < list.Count)
            {
                goto Label_018B;
            }
        Label_01E2:
            objArray = list.ToArray();
            goto Label_02B4;
        Label_01F0:
            if (this.Source != 1)
            {
                goto Label_02B4;
            }
            list2 = new List<ArtifactParam>();
            if (manager.MasterParam.Artifacts == null)
            {
                goto Label_025B;
            }
            paramArray = manager.MasterParam.Artifacts.ToArray();
            num3 = 0;
            goto Label_0250;
        Label_022F:
            if (paramArray[num3].is_create == null)
            {
                goto Label_024A;
            }
            list2.Add(paramArray[num3]);
        Label_024A:
            num3 += 1;
        Label_0250:
            if (num3 < ((int) paramArray.Length))
            {
                goto Label_022F;
            }
        Label_025B:
            if (this.KakeraFlags == null)
            {
                goto Label_02AB;
            }
            num4 = 0;
            goto Label_029D;
        Label_026E:
            if (ShouldShowKakera(list2[num4], manager, this.KakeraFlags) != null)
            {
                goto Label_0297;
            }
            list2.RemoveAt(num4--);
        Label_0297:
            num4 += 1;
        Label_029D:
            if (num4 < list2.Count)
            {
                goto Label_026E;
            }
        Label_02AB:
            objArray = list2.ToArray();
        Label_02B4:
            if ((this.mFiltersPriority == null) || (((int) this.mFiltersPriority.Length) <= 0))
            {
                goto Label_02E1;
            }
            objArray = FilterArtifacts(objArray, this.mFiltersPriority);
            goto Label_02F0;
        Label_02E1:
            objArray = FilterArtifacts(objArray, this.mFilters);
        Label_02F0:
            if ((this.EmptyMessage != null) == null)
            {
                goto Label_0324;
            }
            this.mEmptyMessageChanged = 1;
            this.EmptyMessage.SetActive((objArray == null) ? 1 : (((int) objArray.Length) == 0));
        Label_0324:
            if (objArray != null)
            {
                goto Label_032C;
            }
            return;
        Label_032C:
            if ((string.IsNullOrEmpty(this.mSortMethod) != null) || ((num5 = this.mSortMethod.IndexOf(0x3a)) <= 0))
            {
                goto Label_03CD;
            }
            str = this.mSortMethod.Substring(0, num5);
            str2 = this.mSortMethod.Substring(num5 + 1);
            info = base.GetType().GetMethod(str, 0x34);
            if (info == null)
            {
                goto Label_03BC;
            }
        Label_038B:
            try
            {
                objArray1 = new object[] { objArray, str2 };
                info.Invoke(this, objArray1);
                goto Label_03B7;
            }
            catch (Exception exception1)
            {
            Label_03A9:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_03B7;
            }
        Label_03B7:
            goto Label_03CD;
        Label_03BC:
            DebugUtility.LogWarning("SortMethod Not Found: " + str);
        Label_03CD:
            if (this.mDescending == null)
            {
                goto Label_03DF;
            }
            Array.Reverse(objArray);
        Label_03DF:
            this.mData = new object[(int) objArray.Length];
            Array.Copy(objArray, this.mData, (int) objArray.Length);
            if (this.mPageSize <= 0)
            {
                goto Label_044D;
            }
            this.mMaxPages = (((((int) objArray.Length) + ((int) this.ExtraItems.Length)) + this.mPageSize) - 1) / this.mPageSize;
            this.mPage = Mathf.Max(0, Mathf.Min(this.mPage, this.mMaxPages - 1));
        Label_044D:
            if (this.mFocusSelection == null)
            {
                goto Label_04B4;
            }
            this.mFocusSelection = 0;
            if ((this.mSelection == null) || (this.mSelection.Count <= 0))
            {
                goto Label_04B4;
            }
            num6 = Array.IndexOf<object>(this.mData, this.mSelection[0]) + ((int) this.ExtraItems.Length);
            if (num6 < 0)
            {
                goto Label_04B4;
            }
            this.mPage = num6 / this.mPageSize;
        Label_04B4:
            num7 = 0;
            goto Label_055B;
        Label_04BC:
            num8 = ((this.mPage * this.mPageSize) + num7) - ((int) this.ExtraItems.Length);
            if ((0 > num8) || (num8 >= ((int) this.mData.Length)))
            {
                goto Label_0542;
            }
            DataSource.Bind(this.mItems[num7], this.mDataType, this.mData[num8]);
            this.mItems[num7].SetActive(num7 < this.mPageSize);
            GameParameter.UpdateAll(this.mItems[num7]);
            goto Label_0555;
        Label_0542:
            this.mItems[num7].SetActive(0);
        Label_0555:
            num7 += 1;
        Label_055B:
            if (num7 < this.mItems.Count)
            {
                goto Label_04BC;
            }
            num9 = 0;
            goto Label_05C6;
        Label_0575:
            num10 = (this.mPage * this.mPageSize) + num9;
            if ((this.ExtraItems[num9] != null) == null)
            {
                goto Label_05C0;
            }
            this.ExtraItems[num9].SetActive((0 > num10) ? 0 : (num10 < ((int) this.ExtraItems.Length)));
        Label_05C0:
            num9 += 1;
        Label_05C6:
            if (num9 < ((int) this.ExtraItems.Length))
            {
                goto Label_0575;
            }
            this.UpdateSelection();
            this.UpdatePage();
            if (this.mInvokeSelChange == null)
            {
                goto Label_05F9;
            }
            this.mInvokeSelChange = 0;
            this.TriggerSelectionChange();
        Label_05F9:
            return;
        }

        [CompilerGenerated]
        private static bool <AddCreatableInfo>m__285(ItemData i)
        {
            return (i.ItemType == 14);
        }

        [CompilerGenerated]
        private static object <FilterArtifacts>m__287(ArtifactData o)
        {
            return o;
        }

        [CompilerGenerated]
        private static bool <IsCreatableArtifact>m__283(ItemData i)
        {
            return (i.ItemType == 14);
        }

        [CompilerGenerated]
        private static void <OnSelectionChange>m__282(ArtifactList list)
        {
        }

        [CompilerGenerated]
        private static int <SortById>m__288(ArtifactData x, ArtifactData y)
        {
            return string.Compare(x.ArtifactParam.iname, y.ArtifactParam.iname);
        }

        [CompilerGenerated]
        private static int <SortById>m__289(ArtifactParam x, ArtifactParam y)
        {
            return string.Compare(x.iname, y.iname);
        }

        [CompilerGenerated]
        private static int <SortByType>m__28A(ArtifactData x, ArtifactData y)
        {
            return ArtifactCompareExtention.CompareByTypeAndID(x, y);
        }

        [CompilerGenerated]
        private static int <SortByType>m__28B(ArtifactParam x, ArtifactParam y)
        {
            return ArtifactCompareExtention.CompareByTypeAndID(x, y);
        }

        public void Activated(int pinID)
        {
            if (pinID != 20)
            {
                goto Label_0013;
            }
            this.ClearSelection();
            goto Label_003A;
        Label_0013:
            if (pinID != 200)
            {
                goto Label_0029;
            }
            this.Refresh();
            goto Label_003A;
        Label_0029:
            if (pinID != 0xc9)
            {
                goto Label_003A;
            }
            this.UpdateSelection();
        Label_003A:
            return;
        }

        private unsafe List<GenericBadge<ArtifactData>> AddCreatableInfo(List<ArtifactData> artifactDataList)
        {
            List<GenericBadge<ArtifactData>> list;
            GameManager manager;
            List<ItemData> list2;
            ArtifactData data;
            List<ArtifactData>.Enumerator enumerator;
            ItemData data2;
            RarityParam param;
            bool flag;
            <AddCreatableInfo>c__AnonStorey2F8 storeyf;
            list = new List<GenericBadge<ArtifactData>>();
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_001A;
            }
            return list;
        Label_001A:
            if (manager.Player != null)
            {
                goto Label_0027;
            }
            return list;
        Label_0027:
            if (<>f__am$cache3B != null)
            {
                goto Label_004A;
            }
            <>f__am$cache3B = new Predicate<ItemData>(ArtifactList.<AddCreatableInfo>m__285);
        Label_004A:
            list2 = manager.Player.Items.FindAll(<>f__am$cache3B);
            enumerator = artifactDataList.GetEnumerator();
        Label_005D:
            try
            {
                goto Label_00EC;
            Label_0062:
                data = &enumerator.Current;
                storeyf = new <AddCreatableInfo>c__AnonStorey2F8();
                storeyf.artifactParam = data.ArtifactParam;
                data2 = list2.Find(new Predicate<ItemData>(storeyf.<>m__286));
                if (data2 != null)
                {
                    goto Label_00AC;
                }
                list.Add(new GenericBadge<ArtifactData>(data, 0));
                goto Label_00EC;
            Label_00AC:
                param = MonoSingleton<GameManager>.Instance.GetRarityParam(storeyf.artifactParam.rareini);
                flag = (data2.Num < param.ArtifactCreatePieceNum) == 0;
                list.Add(new GenericBadge<ArtifactData>(data, flag));
            Label_00EC:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0062;
                }
                goto Label_010A;
            }
            finally
            {
            Label_00FD:
                ((List<ArtifactData>.Enumerator) enumerator).Dispose();
            }
        Label_010A:
            return list;
        }

        public bool CheckEndOfIndex(ArtifactData artifact)
        {
            int num;
            if (artifact != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            if (this.mDataType == typeof(ArtifactData))
            {
                goto Label_001F;
            }
            return 1;
        Label_001F:
            return (this.GetIndexOf(artifact) == (((int) this.mData.Length) - 1));
        }

        public bool CheckStartOfIndex(ArtifactData artifact)
        {
            int num;
            if (artifact != null)
            {
                goto Label_0008;
            }
            return 1;
        Label_0008:
            if (this.mDataType == typeof(ArtifactData))
            {
                goto Label_001F;
            }
            return 1;
        Label_001F:
            return (this.GetIndexOf(artifact) == 0);
        }

        public void ClearSelection()
        {
            this.mSelection.Clear();
            this.UpdateSelection();
            this.TriggerSelectionChange();
            return;
        }

        public static unsafe object[] FilterArtifacts(object[] artifacts, string[] filter)
        {
            int num;
            int num2;
            bool flag;
            List<string> list;
            string str;
            int num3;
            int num4;
            ArtifactTypes types;
            List<ArtifactData> list2;
            int num5;
            bool flag2;
            int num6;
            ArtifactData data;
            int num7;
            if (filter != null)
            {
                goto Label_0008;
            }
            return artifacts;
        Label_0008:
            num = 0;
            num2 = 0;
            flag = 0;
            list = new List<string>();
            str = null;
            num3 = 0;
            goto Label_00F9;
        Label_001F:
            if (GetValue(filter[num3], "RARE:", &str) == null)
            {
                goto Label_0054;
            }
            if (int.TryParse(str, &num4) == null)
            {
                goto Label_00F3;
            }
            num |= 1 << ((num4 & 0x1f) & 0x1f);
            goto Label_00F3;
        Label_0054:
            if (GetValue(filter[num3], "TYPE:", &str) == null)
            {
                goto Label_00BA;
            }
        Label_0069:
            try
            {
                types = (int) Enum.Parse(typeof(ArtifactTypes), str, 1);
                num2 |= 1 << ((types & 0x1f) & 0x1f);
                goto Label_00B5;
            }
            catch
            {
            Label_0094:
                if (GameUtility.IsDebugBuild == null)
                {
                    goto Label_00B0;
                }
                Debug.LogError("Unknown element type: " + str);
            Label_00B0:
                goto Label_00B5;
            }
        Label_00B5:
            goto Label_00F3;
        Label_00BA:
            if (GetValue(filter[num3], "FAV:", &str) == null)
            {
                goto Label_00D6;
            }
            flag = 1;
            goto Label_00F3;
        Label_00D6:
            if (GetValue(filter[num3], "SAME:", &str) == null)
            {
                goto Label_00F3;
            }
            list.Add(str);
        Label_00F3:
            num3 += 1;
        Label_00F9:
            if (num3 < ((int) filter.Length))
            {
                goto Label_001F;
            }
            list2 = new List<ArtifactData>();
            num5 = 0;
            goto Label_0128;
        Label_0112:
            list2.Add(artifacts[num5] as ArtifactData);
            num5 += 1;
        Label_0128:
            if (num5 < ((int) artifacts.Length))
            {
                goto Label_0112;
            }
            flag2 = 0;
            num6 = list2.Count - 1;
            goto Label_01FA;
        Label_0145:
            data = list2[num6];
            if (flag == null)
            {
                goto Label_0170;
            }
            if (data.IsFavorite != null)
            {
                goto Label_0170;
            }
            list2.RemoveAt(num6);
            goto Label_01F4;
        Label_0170:
            flag2 = 0;
            num7 = 0;
            goto Label_01A7;
        Label_017B:
            if ((data.ArtifactParam.iname == list[num7]) == null)
            {
                goto Label_01A1;
            }
            flag2 = 1;
            goto Label_01B4;
        Label_01A1:
            num7 += 1;
        Label_01A7:
            if (num7 < list.Count)
            {
                goto Label_017B;
            }
        Label_01B4:
            if (flag2 != null)
            {
                goto Label_01EB;
            }
            if (((1 << (data.Rarity & 0x1f)) & num) == null)
            {
                goto Label_01EB;
            }
            if (((1 << (data.ArtifactParam.type & 0x1f)) & num2) != null)
            {
                goto Label_01F4;
            }
        Label_01EB:
            list2.RemoveAt(num6);
        Label_01F4:
            num6 -= 1;
        Label_01FA:
            if (num6 >= 0)
            {
                goto Label_0145;
            }
            if (<>f__am$cache3C != null)
            {
                goto Label_021C;
            }
            <>f__am$cache3C = new Converter<ArtifactData, object>(ArtifactList.<FilterArtifacts>m__287);
        Label_021C:
            return list2.ConvertAll<object>(<>f__am$cache3C).ToArray();
        }

        public bool GetAutoSelected(bool autoReset)
        {
            if (this.mAutoSelected == null)
            {
                goto Label_001A;
            }
            if (autoReset == null)
            {
                goto Label_0018;
            }
            this.mAutoSelected = 0;
        Label_0018:
            return 1;
        Label_001A:
            return 0;
        }

        private int GetIndexOf(ArtifactData artifact)
        {
            <GetIndexOf>c__AnonStorey2FA storeyfa;
            storeyfa = new <GetIndexOf>c__AnonStorey2FA();
            storeyfa.artifact = artifact;
            if (storeyfa.artifact != null)
            {
                goto Label_001A;
            }
            return -1;
        Label_001A:
            if (this.mDataType == typeof(ArtifactData))
            {
                goto Label_0031;
            }
            return -1;
        Label_0031:
            return Array.FindIndex<object>(this.mData, new Predicate<object>(storeyfa.<>m__28D));
        }

        private static bool GetValue(string str, string key, ref string value)
        {
            if (str.StartsWith(key) == null)
            {
                goto Label_001C;
            }
            *(value) = str.Substring(key.Length);
            return 1;
        Label_001C:
            return 0;
        }

        public void GotoNextPage()
        {
            if (this.mPage >= (this.mMaxPages - 1))
            {
                goto Label_0027;
            }
            this.mPage += 1;
            this._Refresh();
        Label_0027:
            return;
        }

        public void GotoPreviousPage()
        {
            if (this.mPage <= 0)
            {
                goto Label_0020;
            }
            this.mPage -= 1;
            this._Refresh();
        Label_0020:
            return;
        }

        private unsafe bool IsCreatableArtifact(List<ArtifactData> artifactDataList)
        {
            GameManager manager;
            List<ItemData> list;
            ArtifactData data;
            List<ArtifactData>.Enumerator enumerator;
            ItemData data2;
            RarityParam param;
            <IsCreatableArtifact>c__AnonStorey2F7 storeyf;
            bool flag;
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) == null)
            {
                goto Label_0014;
            }
            return 0;
        Label_0014:
            if (manager.Player != null)
            {
                goto Label_0021;
            }
            return 0;
        Label_0021:
            if (<>f__am$cache3A != null)
            {
                goto Label_0044;
            }
            <>f__am$cache3A = new Predicate<ItemData>(ArtifactList.<IsCreatableArtifact>m__283);
        Label_0044:
            list = manager.Player.Items.FindAll(<>f__am$cache3A);
            enumerator = artifactDataList.GetEnumerator();
        Label_0056:
            try
            {
                goto Label_00CB;
            Label_005B:
                data = &enumerator.Current;
                storeyf = new <IsCreatableArtifact>c__AnonStorey2F7();
                storeyf.artifactParam = data.ArtifactParam;
                data2 = list.Find(new Predicate<ItemData>(storeyf.<>m__284));
                if (data2 == null)
                {
                    goto Label_00CB;
                }
                param = MonoSingleton<GameManager>.Instance.GetRarityParam(storeyf.artifactParam.rareini);
                if (data2.Num < param.ArtifactCreatePieceNum)
                {
                    goto Label_00CB;
                }
                flag = 1;
                goto Label_00EA;
            Label_00CB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_005B;
                }
                goto Label_00E8;
            }
            finally
            {
            Label_00DC:
                ((List<ArtifactData>.Enumerator) enumerator).Dispose();
            }
        Label_00E8:
            return 0;
        Label_00EA:
            return flag;
        }

        private void LateUpdate()
        {
            if (this.mShouldRefresh == null)
            {
                goto Label_0018;
            }
            this.mShouldRefresh = 0;
            this._Refresh();
        Label_0018:
            return;
        }

        private unsafe void OnItemDetail(GameObject go)
        {
            object obj2;
            object obj3;
            GameObject obj4;
            ArtifactData data;
            UnitData data2;
            JobData data3;
            if ((this.ArtifactDetail == null) == null)
            {
                goto Label_005C;
            }
            if ((this.ArtifactDetailRef != null) == null)
            {
                goto Label_005B;
            }
            obj2 = DataSource.FindDataOfClass(go, this.mDataType, null);
            if (obj2 != null)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            this.mSelection.Clear();
            this.mSelection.Add(obj2);
            this.UpdateSelection();
            this.TriggerDetailSelectionChange();
            return;
        Label_005B:
            return;
        Label_005C:
            obj3 = DataSource.FindDataOfClass(go, this.mDataType, null);
            if (obj3 != null)
            {
                goto Label_0071;
            }
            return;
        Label_0071:
            obj4 = Object.Instantiate<GameObject>(this.ArtifactDetail);
            DataSource.Bind(obj4, this.mDataType, obj3);
            if ((obj3 as ArtifactData) == null)
            {
                goto Label_00C5;
            }
            data = obj3 as ArtifactData;
            if (MonoSingleton<GameManager>.GetInstanceDirect().Player.FindOwner(data, &data2, &data3) == null)
            {
                goto Label_00C5;
            }
            DataSource.Bind<UnitData>(obj4, data2);
            DataSource.Bind<JobData>(obj4, data3);
        Label_00C5:
            obj4.get_gameObject().SetActive(1);
            return;
        }

        private void OnItemSelect(GameObject go)
        {
            object obj2;
            if (this.mShouldRefresh == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            obj2 = DataSource.FindDataOfClass(go, this.mDataType, null);
            if (obj2 != null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            this.UpdateSelection(obj2);
            return;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            this.mShouldRefresh = 1;
            return;
        }

        public void Refresh()
        {
            this.mShouldRefresh = 1;
            return;
        }

        public bool SelectBack(ArtifactData artifactData)
        {
            object[] objArray1;
            int num;
            int num2;
            if (this.mDataType == typeof(ArtifactData))
            {
                goto Label_0017;
            }
            return 0;
        Label_0017:
            this.mAutoSelected = 1;
            num = this.GetIndexOf(artifactData);
            num2 = num - 1;
            if (num == -1)
            {
                goto Label_0051;
            }
            if (num2 <= -1)
            {
                goto Label_0051;
            }
            objArray1 = new object[] { this.mData[num2] };
            this.SetSelection(objArray1, 1, 1);
        Label_0051:
            return (num > 0);
        }

        public bool SelectFoward(ArtifactData artifactData)
        {
            object[] objArray1;
            int num;
            int num2;
            if (this.mDataType == typeof(ArtifactData))
            {
                goto Label_0017;
            }
            return 0;
        Label_0017:
            this.mAutoSelected = 1;
            num = this.GetIndexOf(artifactData);
            num2 = num + 1;
            if (num == -1)
            {
                goto Label_0058;
            }
            if (num2 >= ((int) this.mData.Length))
            {
                goto Label_0058;
            }
            objArray1 = new object[] { this.mData[num2] };
            this.SetSelection(objArray1, 1, 1);
        Label_0058:
            return (num < ((int) this.mData.Length));
        }

        public void SetSelection(object[] sel, bool invoke, bool focus)
        {
            int num;
            this.mFocusSelection = focus;
            this.Refresh();
            this.mSelection.Clear();
            num = 0;
            goto Label_004C;
        Label_001F:
            if ((sel[num] == null) || (this.mSelection.Contains(sel[num]) != null))
            {
                goto Label_0048;
            }
            this.mSelection.Add(sel[num]);
        Label_0048:
            num += 1;
        Label_004C:
            if (num < ((int) sel.Length))
            {
                goto Label_001F;
            }
            if (this.mStarted != null)
            {
                goto Label_007A;
            }
            this.mInvokeSelChange = (this.mInvokeSelChange != null) ? 1 : invoke;
            goto Label_008C;
        Label_007A:
            this.UpdateSelection();
            if (invoke == null)
            {
                goto Label_008C;
            }
            this.TriggerSelectionChange();
        Label_008C:
            return;
        }

        public void SetSortMethod(string method, bool ascending, string[] filters)
        {
            this.mSortMethod = method;
            this.mDescending = ascending == 0;
            this.mFilters = filters;
            this.Refresh();
            return;
        }

        private static bool ShouldShowKakera(ArtifactParam artifact, GameManager gm, KakeraHideFlags flags)
        {
            RarityParam param;
            int num;
            param = null;
            if (artifact != null)
            {
                goto Label_000A;
            }
            return 0;
        Label_000A:
            if ((flags & 6) == null)
            {
                goto Label_0024;
            }
            param = gm.MasterParam.GetRarityParam(artifact.rareini);
        Label_0024:
            if ((flags & 3) == null)
            {
                goto Label_006A;
            }
            num = gm.Player.GetItemAmount(artifact.kakera);
            if ((flags & 1) == null)
            {
                goto Label_004F;
            }
            if (num >= 1)
            {
                goto Label_004F;
            }
            return 0;
        Label_004F:
            if ((flags & 2) == null)
            {
                goto Label_006A;
            }
            if (num >= param.ArtifactCreatePieceNum)
            {
                goto Label_006A;
            }
            return 0;
        Label_006A:
            if ((flags & 4) == null)
            {
                goto Label_008F;
            }
            if (gm.Player.Gold >= param.ArtifactCreateCost)
            {
                goto Label_008F;
            }
            return 0;
        Label_008F:
            return 1;
        }

        private void SortById(object[] artifacts)
        {
            ArtifactData[] dataArray;
            ArtifactParam[] paramArray;
            if ((artifacts as ArtifactData[]) == null)
            {
                goto Label_003A;
            }
            dataArray = (ArtifactData[]) artifacts;
            if (<>f__am$cache3D != null)
            {
                goto Label_002B;
            }
            <>f__am$cache3D = new Comparison<ArtifactData>(ArtifactList.<SortById>m__288);
        Label_002B:
            Array.Sort<ArtifactData>(dataArray, <>f__am$cache3D);
            goto Label_006F;
        Label_003A:
            if ((artifacts as ArtifactParam[]) == null)
            {
                goto Label_006F;
            }
            paramArray = (ArtifactParam[]) artifacts;
            if (<>f__am$cache3E != null)
            {
                goto Label_0065;
            }
            <>f__am$cache3E = new Comparison<ArtifactParam>(ArtifactList.<SortById>m__289);
        Label_0065:
            Array.Sort<ArtifactParam>(paramArray, <>f__am$cache3E);
        Label_006F:
            return;
        }

        private void SortByMember(object[] artifacts, string key)
        {
            PropertyInfo info;
            int num;
            object obj2;
            string str;
            info = typeof(ArtifactData).GetProperty(key, 20);
            if (info != null)
            {
                goto Label_002A;
            }
            DebugUtility.LogWarning("Property Not Found: " + key);
            return;
        Label_002A:
            this.SortById(artifacts);
            if (this.mTmpSortVal == null)
            {
                goto Label_004C;
            }
            if (((int) this.mTmpSortVal.Length) >= ((int) artifacts.Length))
            {
                goto Label_005A;
            }
        Label_004C:
            this.mTmpSortVal = new int[(int) artifacts.Length];
        Label_005A:
            num = 0;
            goto Label_0099;
        Label_0061:
            try
            {
                str = info.GetValue(artifacts[num], null).ToString();
                this.mTmpSortVal[num] = int.Parse(str);
                goto Label_0095;
            }
            catch (Exception)
            {
            Label_0086:
                this.mTmpSortVal[num] = 0;
                goto Label_0095;
            }
        Label_0095:
            num += 1;
        Label_0099:
            if (num < ((int) artifacts.Length))
            {
                goto Label_0061;
            }
            this.SortItems(artifacts, this.mTmpSortVal);
            return;
        }

        private unsafe void SortByStatus(object[] artifacts, string key)
        {
            ParamTypes types;
            Exception exception;
            int num;
            if (string.IsNullOrEmpty(key) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.SortById(artifacts);
        Label_0013:
            try
            {
                types = (short) Enum.Parse(typeof(ParamTypes), key);
                goto Label_0044;
            }
            catch (Exception exception1)
            {
            Label_002E:
                exception = exception1;
                DebugUtility.LogWarning(exception.Message);
                goto Label_010F;
            }
        Label_0044:
            if (this.mTmpVal0 != null)
            {
                goto Label_005F;
            }
            this.mTmpVal0 = new BaseStatus();
            goto Label_006A;
        Label_005F:
            this.mTmpVal0.Clear();
        Label_006A:
            if (this.mTmpVal1 != null)
            {
                goto Label_0085;
            }
            this.mTmpVal1 = new BaseStatus();
            goto Label_0090;
        Label_0085:
            this.mTmpVal1.Clear();
        Label_0090:
            if (this.mTmpSortVal == null)
            {
                goto Label_00AB;
            }
            if (((int) this.mTmpSortVal.Length) >= ((int) artifacts.Length))
            {
                goto Label_00B9;
            }
        Label_00AB:
            this.mTmpSortVal = new int[(int) artifacts.Length];
        Label_00B9:
            num = 0;
            goto Label_00F9;
        Label_00C0:
            ((ArtifactData) artifacts[num]).GetHomePassiveBuffStatus(&this.mTmpVal0, &this.mTmpVal1, null, 0, 1);
            this.mTmpSortVal[num] = this.mTmpVal0[types];
            num += 1;
        Label_00F9:
            if (num < ((int) artifacts.Length))
            {
                goto Label_00C0;
            }
            this.SortItems(artifacts, this.mTmpSortVal);
        Label_010F:
            return;
        }

        private void SortByType(object[] artifacts, string key)
        {
            ArtifactData[] dataArray;
            ArtifactParam[] paramArray;
            int num;
            if ((artifacts as ArtifactData[]) == null)
            {
                goto Label_003A;
            }
            dataArray = (ArtifactData[]) artifacts;
            if (<>f__am$cache3F != null)
            {
                goto Label_002B;
            }
            <>f__am$cache3F = new Comparison<ArtifactData>(ArtifactList.<SortByType>m__28A);
        Label_002B:
            Array.Sort<ArtifactData>(dataArray, <>f__am$cache3F);
            goto Label_006F;
        Label_003A:
            if ((artifacts as ArtifactParam[]) == null)
            {
                goto Label_006F;
            }
            paramArray = (ArtifactParam[]) artifacts;
            if (<>f__am$cache40 != null)
            {
                goto Label_0065;
            }
            <>f__am$cache40 = new Comparison<ArtifactParam>(ArtifactList.<SortByType>m__28B);
        Label_0065:
            Array.Sort<ArtifactParam>(paramArray, <>f__am$cache40);
        Label_006F:
            if (this.mTmpSortVal == null)
            {
                goto Label_008A;
            }
            if (((int) this.mTmpSortVal.Length) >= ((int) artifacts.Length))
            {
                goto Label_0098;
            }
        Label_008A:
            this.mTmpSortVal = new int[(int) artifacts.Length];
        Label_0098:
            num = 0;
            goto Label_00BD;
        Label_009F:
            this.mTmpSortVal[num] = ((ArtifactData) artifacts[num]).ArtifactParam.type;
            num += 1;
        Label_00BD:
            if (num < ((int) artifacts.Length))
            {
                goto Label_009F;
            }
            this.SortItems(artifacts, this.mTmpSortVal);
            return;
        }

        private void SortItems(object[] items, int[] values)
        {
            SortData[] dataArray;
            int num;
            int num2;
            <SortItems>c__AnonStorey2F9 storeyf;
            storeyf = new <SortItems>c__AnonStorey2F9();
            dataArray = new SortData[(int) items.Length];
            num = 0;
            goto Label_0028;
        Label_0016:
            dataArray[num] = new SortData(items[num], values[num]);
            num += 1;
        Label_0028:
            if (num < ((int) items.Length))
            {
                goto Label_0016;
            }
            storeyf.result = 0;
            Array.Sort<SortData>(dataArray, new Comparison<SortData>(storeyf.<>m__28C));
            num2 = 0;
            goto Label_006B;
        Label_0051:
            items[num2] = dataArray[num2].mArtifact;
            values[num2] = dataArray[num2].mStatusValue;
            num2 += 1;
        Label_006B:
            if (num2 < ((int) items.Length))
            {
                goto Label_0051;
            }
            return;
        }

        protected override void Start()
        {
            base.Start();
            if ((this.ListItem != null) == null)
            {
                goto Label_0033;
            }
            if (this.ListItem.get_activeInHierarchy() == null)
            {
                goto Label_0033;
            }
            this.ListItem.SetActive(0);
        Label_0033:
            if ((this.EmptyMessage != null) == null)
            {
                goto Label_0062;
            }
            if (this.mEmptyMessageChanged != null)
            {
                goto Label_0062;
            }
            this.mEmptyMessageChanged = 1;
            this.EmptyMessage.SetActive(0);
        Label_0062:
            this.mStarted = 1;
            return;
        }

        private void TriggerDetailSelectionChange()
        {
            this.OnSelectionChange(this);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            return;
        }

        private void TriggerSelectionChange()
        {
            this.OnSelectionChange(this);
            if ((this.AbilityScrollBar != null) == null)
            {
                goto Label_002D;
            }
            this.AbilityScrollBar.set_value(1f);
        Label_002D:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public unsafe void UpdatePage()
        {
            int num;
            if ((this.PageScrollBar != null) == null)
            {
                goto Label_007A;
            }
            if (this.mMaxPages < 2)
            {
                goto Label_005A;
            }
            this.PageScrollBar.set_size(1f / ((float) this.mMaxPages));
            this.PageScrollBar.set_value(((float) this.mPage) / (((float) this.mMaxPages) - 1f));
            goto Label_007A;
        Label_005A:
            this.PageScrollBar.set_size(1f);
            this.PageScrollBar.set_value(0f);
        Label_007A:
            if ((this.PageIndex != null) == null)
            {
                goto Label_00B1;
            }
            this.PageIndex.set_text(&Mathf.Min(this.mPage + 1, this.mMaxPages).ToString());
        Label_00B1:
            if ((this.PageIndexMax != null) == null)
            {
                goto Label_00D8;
            }
            this.PageIndexMax.set_text(&this.mMaxPages.ToString());
        Label_00D8:
            if ((this.ForwardButton != null) == null)
            {
                goto Label_0104;
            }
            this.ForwardButton.set_interactable(this.mPage < (this.mMaxPages - 1));
        Label_0104:
            if ((this.BackButton != null) == null)
            {
                goto Label_0129;
            }
            this.BackButton.set_interactable(this.mPage > 0);
        Label_0129:
            return;
        }

        public unsafe void UpdateSelection()
        {
            int num;
            object obj2;
            ArtifactData data;
            List<CheckBadge> list;
            int num2;
            int num3;
            int num4;
            Animator animator;
            ArtifactIcon icon;
            bool flag;
            int num5;
            long num6;
            int num7;
            ArtifactData data2;
            long num8;
            int num9;
            int num10;
            if (this.mData == null)
            {
                goto Label_0016;
            }
            if (this.ShowSelection != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            num = this.mSelection.Count - 1;
            goto Label_00A1;
        Label_002A:
            obj2 = this.mSelection[num];
            if (Array.IndexOf<object>(this.mData, obj2) >= 0)
            {
                goto Label_0061;
            }
            this.mSelection.RemoveAt(num);
            this.mInvokeSelChange = 1;
            goto Label_009D;
        Label_0061:
            if ((obj2 as ArtifactData) == null)
            {
                goto Label_009D;
            }
            if (this.MaxSelection <= 0)
            {
                goto Label_009D;
            }
            data = obj2 as ArtifactData;
            if (data.IsFavorite == null)
            {
                goto Label_009D;
            }
            this.mSelection.RemoveAt(num);
            this.mInvokeSelChange = 1;
        Label_009D:
            num -= 1;
        Label_00A1:
            if (num >= 0)
            {
                goto Label_002A;
            }
            if ((this.NumSelection != null) == null)
            {
                goto Label_00D8;
            }
            this.NumSelection.set_text(&this.mSelection.Count.ToString());
        Label_00D8:
            list = new List<CheckBadge>();
            num2 = 0;
            goto Label_0275;
        Label_00E6:
            num3 = ((this.mPage * this.mPageSize) + num2) - ((int) this.ExtraItems.Length);
            if (0 > num3)
            {
                goto Label_025C;
            }
            if (num3 >= ((int) this.mData.Length))
            {
                goto Label_025C;
            }
            num4 = this.mSelection.IndexOf(this.mData[num3]);
            animator = this.mItems[num2].GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0192;
            }
            if (string.IsNullOrEmpty(this.SelectionState) != null)
            {
                goto Label_0192;
            }
            if (num4 < 0)
            {
                goto Label_017F;
            }
            animator.SetInteger(this.SelectionState, this.Item_Selected);
            goto Label_0192;
        Label_017F:
            animator.SetInteger(this.SelectionState, this.Item_Normal);
        Label_0192:
            icon = this.mItems[num2].GetComponentInChildren<ArtifactIcon>(1);
            if (((this.mSelection.Count < this.MaxSelection) == 0) == null)
            {
                goto Label_01DB;
            }
            if (num4 >= 0)
            {
                goto Label_01DB;
            }
            icon.ForceMask = 1;
            goto Label_01E3;
        Label_01DB:
            icon.ForceMask = 0;
        Label_01E3:
            GameParameter.UpdateAll(this.mItems[num2]);
            this.mItems[num2].GetComponentsInChildren<CheckBadge>(1, list);
            num5 = 0;
            goto Label_024A;
        Label_0211:
            if (num4 < 0)
            {
                goto Label_0231;
            }
            list[num5].get_gameObject().SetActive(1);
            goto Label_0244;
        Label_0231:
            list[num5].get_gameObject().SetActive(0);
        Label_0244:
            num5 += 1;
        Label_024A:
            if (num5 < list.Count)
            {
                goto Label_0211;
            }
            goto Label_026F;
        Label_025C:
            this.mItems[num2].SetActive(0);
        Label_026F:
            num2 += 1;
        Label_0275:
            if (num2 < this.mItems.Count)
            {
                goto Label_00E6;
            }
            if (this.Source != null)
            {
                goto Label_03BF;
            }
            if ((this.TotalDecomposeCost != null) == null)
            {
                goto Label_030B;
            }
            num6 = 0L;
            num7 = 0;
            goto Label_02E7;
        Label_02AF:
            data2 = this.mSelection[num7] as ArtifactData;
            if (data2 == null)
            {
                goto Label_02E1;
            }
            num6 += (long) data2.RarityParam.ArtifactChangeCost;
        Label_02E1:
            num7 += 1;
        Label_02E7:
            if (num7 < this.mSelection.Count)
            {
                goto Label_02AF;
            }
            this.TotalDecomposeCost.set_text(&num6.ToString());
        Label_030B:
            if ((this.TotalSellCost != null) == null)
            {
                goto Label_03BF;
            }
            num8 = 0L;
            num9 = 0;
            goto Label_039B;
        Label_0328:
            if ((this.mSelection[num9] as ArtifactData) == null)
            {
                goto Label_0361;
            }
            num8 += (long) ((ArtifactData) this.mSelection[num9]).GetSellPrice();
            goto Label_0395;
        Label_0361:
            if ((this.mSelection[num9] as ArtifactParam) == null)
            {
                goto Label_0395;
            }
            num8 += (long) ((ArtifactParam) this.mSelection[num9]).sell;
        Label_0395:
            num9 += 1;
        Label_039B:
            if (num9 < this.mSelection.Count)
            {
                goto Label_0328;
            }
            this.TotalSellCost.set_text(&num8.ToString());
        Label_03BF:
            if ((this.ApplyButton != null) == null)
            {
                goto Label_03E9;
            }
            this.ApplyButton.set_interactable(this.mSelection.Count > 0);
        Label_03E9:
            return;
        }

        public void UpdateSelection(object selection)
        {
            ArtifactData data;
            if (this.MaxSelection <= 0)
            {
                goto Label_008C;
            }
            if ((selection as ArtifactData) == null)
            {
                goto Label_0036;
            }
            data = selection as ArtifactData;
            if (data.IsFavorite == null)
            {
                goto Label_002A;
            }
            return;
        Label_002A:
            if (data.CheckEquiped() == null)
            {
                goto Label_0036;
            }
            return;
        Label_0036:
            if (this.mSelection.Contains(selection) == null)
            {
                goto Label_0059;
            }
            this.mSelection.Remove(selection);
            goto Label_007B;
        Label_0059:
            if (this.mSelection.Count >= this.MaxSelection)
            {
                goto Label_007B;
            }
            this.mSelection.Add(selection);
        Label_007B:
            this.UpdateSelection();
            this.TriggerSelectionChange();
            goto Label_00AF;
        Label_008C:
            this.mSelection.Clear();
            this.mSelection.Add(selection);
            this.UpdateSelection();
            this.TriggerSelectionChange();
        Label_00AF:
            return;
        }

        public object[] Selection
        {
            get
            {
                return this.mSelection.ToArray();
            }
        }

        public bool HasSelection
        {
            get
            {
                return (this.mSelection.Count > 0);
            }
        }

        public int CellCount
        {
            get
            {
                float num;
                float num2;
                float num3;
                float num4;
                float num5;
                float num6;
                GridLayoutGroup group;
                RectTransform transform;
                Rect rect;
                float num7;
                float num8;
                int num9;
                int num10;
                Vector2 vector;
                Vector2 vector2;
                Vector2 vector3;
                Vector2 vector4;
                if (this.UseGridLayout == null)
                {
                    goto Label_0086;
                }
                if (((group = base.GetComponent<GridLayoutGroup>()) != null) == null)
                {
                    goto Label_0086;
                }
                num = &group.get_cellSize().x;
                num2 = &group.get_cellSize().y;
                num3 = &group.get_spacing().x;
                num4 = &group.get_spacing().y;
                num5 = (float) group.get_padding().get_horizontal();
                num6 = (float) group.get_padding().get_vertical();
                goto Label_00B2;
            Label_0086:
                num = this.CellWidth;
                num2 = this.CellHeight;
                num3 = this.SpacingX;
                num4 = this.SpacingY;
                num5 = this.PaddingX;
                num6 = this.PaddingY;
            Label_00B2:
                transform = (RectTransform) base.get_transform();
                rect = transform.get_rect();
                num7 = (&rect.get_width() - num5) + num3;
                if (this.TruncateX != null)
                {
                    goto Label_00EF;
                }
                num7 += (num + num4) - 1f;
            Label_00EF:
                num8 = (&rect.get_height() - num6) + num4;
                if (this.TruncateY != null)
                {
                    goto Label_0116;
                }
                num8 += (num2 + num3) - 1f;
            Label_0116:
                num9 = Mathf.FloorToInt(num7 / (num + num3));
                num10 = Mathf.FloorToInt(num8 / (num2 + num4));
                return Mathf.Clamp(num9 * num10, 0, this.MaxCellCount);
            }
        }

        public string[] FiltersPriority
        {
            get
            {
                return this.mFiltersPriority;
            }
            set
            {
                this.mFiltersPriority = value;
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <AddCreatableInfo>c__AnonStorey2F8
        {
            internal ArtifactParam artifactParam;

            public <AddCreatableInfo>c__AnonStorey2F8()
            {
                base..ctor();
                return;
            }

            internal bool <>m__286(ItemData piece)
            {
                return (piece.Param.iname == this.artifactParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <GetIndexOf>c__AnonStorey2FA
        {
            internal ArtifactData artifact;

            public <GetIndexOf>c__AnonStorey2FA()
            {
                base..ctor();
                return;
            }

            internal bool <>m__28D(object a)
            {
                return (((ArtifactData) a).UniqueID == this.artifact.UniqueID);
            }
        }

        [CompilerGenerated]
        private sealed class <IsCreatableArtifact>c__AnonStorey2F7
        {
            internal ArtifactParam artifactParam;

            public <IsCreatableArtifact>c__AnonStorey2F7()
            {
                base..ctor();
                return;
            }

            internal bool <>m__284(ItemData piece)
            {
                return (piece.Param.iname == this.artifactParam.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <SortItems>c__AnonStorey2F9
        {
            internal int result;

            public <SortItems>c__AnonStorey2F9()
            {
                base..ctor();
                return;
            }

            internal int <>m__28C(ArtifactList.SortData x, ArtifactList.SortData y)
            {
                this.result = x.mStatusValue - y.mStatusValue;
                if (this.result == null)
                {
                    goto Label_0025;
                }
                return this.result;
            Label_0025:
                this.result = ArtifactCompareExtention.CompareByID(x.artifactParam, y.artifactParam);
                if (this.result == null)
                {
                    goto Label_004E;
                }
                return this.result;
            Label_004E:
                if (x.isArtifactData == null)
                {
                    goto Label_00B5;
                }
                if (y.isArtifactData == null)
                {
                    goto Label_00B5;
                }
                this.result = x.artifactData.Lv - y.artifactData.Lv;
                if (this.result == null)
                {
                    goto Label_009D;
                }
                return this.result;
            Label_009D:
                return (x.artifactData.Exp - y.artifactData.Exp);
            Label_00B5:
                return this.result;
            }
        }

        [Flags]
        public enum KakeraHideFlags
        {
            LeastKakera = 1,
            EnoughKakera = 2,
            EnoughGold = 4
        }

        public enum ListSource
        {
            Normal,
            Kakera
        }

        public delegate void SelectionChangeEvent(ArtifactList list);

        private class SortData
        {
            public object mArtifact;
            public int mStatusValue;
            [CompilerGenerated]
            private bool <isArtifactData>k__BackingField;

            public SortData(object artifact, int statusValue)
            {
                base..ctor();
                this.mArtifact = artifact;
                this.mStatusValue = statusValue;
                this.isArtifactData = (this.mArtifact as ArtifactData) > null;
                return;
            }

            public ArtifactParam artifactParam
            {
                get
                {
                    if (this.isArtifactData == null)
                    {
                        goto Label_001C;
                    }
                    return ((ArtifactData) this.mArtifact).ArtifactParam;
                Label_001C:
                    return (ArtifactParam) this.mArtifact;
                }
            }

            public ArtifactData artifactData
            {
                get
                {
                    if (this.isArtifactData == null)
                    {
                        goto Label_0017;
                    }
                    return (ArtifactData) this.mArtifact;
                Label_0017:
                    return null;
                }
            }

            public bool isArtifactData
            {
                [CompilerGenerated]
                get
                {
                    return this.<isArtifactData>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<isArtifactData>k__BackingField = value;
                    return;
                }
            }
        }
    }
}

