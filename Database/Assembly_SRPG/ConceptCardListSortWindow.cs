namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(1, "キャンセル", 0, 1), Pin(20, "キャンセル完了", 1, 20), Pin(10, "セーブ完了", 1, 10), Pin(0, "セーブ", 0, 0)]
    public class ConceptCardListSortWindow : MonoBehaviour, IFlowInterface
    {
        public const string SAVE_KEY = "CARD_FILTER";
        public const Type CLEAR_TYPE = 0xf000000;
        public const Type CLEAR_ORDER_TYPE = 0xffffff;
        public const Type MASK_TYPE = 0xffffff;
        public const Type MASK_ORDER_TYPE = 0xf000000;
        public const int PIN_SAVE = 0;
        public const int PIN_CANCEL = 1;
        public const int PIN_SAVE_END = 10;
        public const int PIN_CANCEL_END = 20;
        public const Type DefaultType = 0x1000200;
        [SerializeField]
        private ParentType parent_type;
        [SerializeField]
        private Item[] SortToggles;
        [SerializeField]
        private Item[] SortOrderToggles;
        [SerializeField]
        private ToggleGroup Group;
        [SerializeField]
        private ToggleGroup OrderGroup;
        private Type CurrentType;
        private Type FirstType;
        [CompilerGenerated]
        private static Comparison<SortData> <>f__am$cache7;

        public ConceptCardListSortWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static unsafe int <Sort>m__2DA(SortData x, SortData y)
        {
            int num;
            int num2;
            long num3;
            if (x.sort_val == y.sort_val)
            {
                goto Label_0023;
            }
            return &x.sort_val.CompareTo(y.sort_val);
        Label_0023:
            if (x.data.Param.type == y.data.Param.type)
            {
                goto Label_0078;
            }
            return ((eCardType) y.data.Param.type).CompareTo((eCardType) x.data.Param.type);
        Label_0078:
            if ((x.data.Param.iname != y.data.Param.iname) == null)
            {
                goto Label_00C8;
            }
            return x.data.Param.iname.CompareTo(y.data.Param.iname);
        Label_00C8:
            if (x.data.Lv == y.data.Lv)
            {
                goto Label_0116;
            }
            return &x.data.Lv.CompareTo(y.data.Lv);
        Label_0116:
            if (x.data.Exp == y.data.Exp)
            {
                goto Label_0164;
            }
            return &x.data.Exp.CompareTo(y.data.Exp);
        Label_0164:
            if (x.data.UniqueID == y.data.UniqueID)
            {
                goto Label_01B2;
            }
            return &x.data.UniqueID.CompareTo(y.data.UniqueID);
        Label_01B2:
            return 0;
        }

        public void Activated(int pinID)
        {
            ConceptCardManager manager;
            int num;
            num = pinID;
            if (num == null)
            {
                goto Label_0014;
            }
            if (num == 1)
            {
                goto Label_004B;
            }
            goto Label_005E;
        Label_0014:
            manager = ConceptCardManager.Instance;
            if ((manager != null) == null)
            {
                goto Label_0038;
            }
            if (manager.IsEnhanceListActive == null)
            {
                goto Label_0038;
            }
            manager.ToggleSameSelectCard = 0;
        Label_0038:
            this.Save();
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_005E;
        Label_004B:
            this.ResetType();
            FlowNode_GameObject.ActivateOutputLinks(this, 20);
        Label_005E:
            return;
        }

        public void ChangeOrderType(Type type)
        {
            if ((type & 0xffffff) <= 0)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            this.CurrentType &= 0xffffff;
            this.CurrentType |= type;
            return;
        }

        public void ChangeType(Type type)
        {
            if ((type & 0xf000000) <= 0)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            this.CurrentType &= 0xf000000;
            this.CurrentType |= type;
            return;
        }

        public static string GetTypeString(Type type)
        {
            Type type2;
            type2 = type;
            switch ((type2 - 1))
            {
                case 0:
                    goto Label_007E;

                case 1:
                    goto Label_0084;

                case 2:
                    goto Label_002A;

                case 3:
                    goto Label_008A;

                case 4:
                    goto Label_002A;

                case 5:
                    goto Label_002A;

                case 6:
                    goto Label_002A;

                case 7:
                    goto Label_0090;
            }
        Label_002A:
            if (type2 == 0x10)
            {
                goto Label_0096;
            }
            if (type2 == 0x20)
            {
                goto Label_009C;
            }
            if (type2 == 0x40)
            {
                goto Label_00A2;
            }
            if (type2 == 0x80)
            {
                goto Label_00A8;
            }
            if (type2 == 0x100)
            {
                goto Label_00AE;
            }
            if (type2 == 0x200)
            {
                goto Label_00B4;
            }
            if (type2 == 0x400)
            {
                goto Label_00BA;
            }
            if (type2 == 0x800)
            {
                goto Label_00C0;
            }
            goto Label_00C6;
        Label_007E:
            return "sys.SORT_LEVEL";
        Label_0084:
            return "sys.SORT_RARITY";
        Label_008A:
            return "sys.SORT_ATK";
        Label_0090:
            return "sys.SORT_DEF";
        Label_0096:
            return "sys.SORT_MAG";
        Label_009C:
            return "sys.SORT_MND";
        Label_00A2:
            return "sys.SORT_SPD";
        Label_00A8:
            return "sys.SORT_LUCK";
        Label_00AE:
            return "sys.SORT_HP";
        Label_00B4:
            return "sys.SORT_TIME";
        Label_00BA:
            return "sys.SORT_TRUST";
        Label_00C0:
            return "sys.SORT_AWAKE";
        Label_00C6:
            return string.Empty;
        }

        public void Load()
        {
            this.CurrentType = LoadData();
            return;
        }

        private static unsafe Type LoadData()
        {
            string str;
            int num;
            if (PlayerPrefsUtility.HasKey("CARD_FILTER") != null)
            {
                goto Label_0015;
            }
            return 0x1000200;
        Label_0015:
            str = PlayerPrefsUtility.GetString("CARD_FILTER", string.Empty);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0036;
            }
            return 0x1000200;
        Label_0036:
            num = 0;
            if (int.TryParse(str, &num) != null)
            {
                goto Label_004B;
            }
            return 0x1000200;
        Label_004B:
            return num;
        }

        public static Type LoadDataOrderType()
        {
            return (LoadData() & 0xf000000);
        }

        public static Type LoadDataType()
        {
            return (LoadData() & 0xffffff);
        }

        public void OnSelect(bool is_on)
        {
            IEnumerable<Toggle> enumerable;
            List<Toggle> list;
            Toggle toggle;
            int num;
            Item item;
            list = Enumerable.ToList<Toggle>(this.Group.ActiveToggles());
            if (list.Count == 1)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            toggle = list[0];
            num = 0;
            goto Label_005C;
        Label_002F:
            item = this.SortToggles[num];
            if ((toggle == item.toggle) == null)
            {
                goto Label_0058;
            }
            this.ChangeType(item.type);
        Label_0058:
            num += 1;
        Label_005C:
            if (num < ((int) this.SortToggles.Length))
            {
                goto Label_002F;
            }
            this.SetType();
            return;
        }

        public void OnSelectOrder(bool is_on)
        {
            IEnumerable<Toggle> enumerable;
            List<Toggle> list;
            Toggle toggle;
            int num;
            Item item;
            list = Enumerable.ToList<Toggle>(this.OrderGroup.ActiveToggles());
            if (list.Count == 1)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            toggle = list[0];
            num = 0;
            goto Label_005C;
        Label_002F:
            item = this.SortOrderToggles[num];
            if ((toggle == item.toggle) == null)
            {
                goto Label_0058;
            }
            this.ChangeOrderType(item.type);
        Label_0058:
            num += 1;
        Label_005C:
            if (num < ((int) this.SortOrderToggles.Length))
            {
                goto Label_002F;
            }
            this.SetType();
            return;
        }

        public void ResetType()
        {
            this.CurrentType = this.FirstType;
            this.SetType();
            return;
        }

        public unsafe void Save()
        {
            int num;
            if (string.IsNullOrEmpty("CARD_FILTER") == null)
            {
                goto Label_0010;
            }
            return;
        Label_0010:
            PlayerPrefsUtility.SetString("CARD_FILTER", &this.CurrentType.ToString(), 1);
            return;
        }

        public void SetType()
        {
            ConceptCardManager manager;
            ConceptCardEquipWindow window;
            ParentType type;
            type = this.parent_type;
            if (type == null)
            {
                goto Label_0019;
            }
            if (type == 1)
            {
                goto Label_0055;
            }
            goto Label_0091;
        Label_0019:
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_002C;
            }
            return;
        Label_002C:
            manager.SortType = this.CurrentType & 0xffffff;
            manager.SortOrderType = this.CurrentType & 0xf000000;
            goto Label_0091;
        Label_0055:
            window = ConceptCardEquipWindow.Instance;
            if ((window == null) == null)
            {
                goto Label_0068;
            }
            return;
        Label_0068:
            window.SortType = this.CurrentType & 0xffffff;
            window.SortOrderType = this.CurrentType & 0xf000000;
        Label_0091:
            return;
        }

        public static void Sort(Type sort_type, Type sort_order_type, List<ConceptCardData> card_list)
        {
            List<SortData> list;
            int num;
            ConceptCardData data;
            int num2;
            SortData data2;
            list = new List<SortData>();
            num = 0;
            goto Label_0037;
        Label_000D:
            data = card_list[num];
            if (data != null)
            {
                goto Label_0020;
            }
            goto Label_0033;
        Label_0020:
            list.Add(new SortData(data, data.GetSortData(sort_type)));
        Label_0033:
            num += 1;
        Label_0037:
            if (num < card_list.Count)
            {
                goto Label_000D;
            }
            if (<>f__am$cache7 != null)
            {
                goto Label_005C;
            }
            <>f__am$cache7 = new Comparison<SortData>(ConceptCardListSortWindow.<Sort>m__2DA);
        Label_005C:
            list.Sort(<>f__am$cache7);
            card_list.Clear();
            num2 = 0;
            goto Label_008D;
        Label_0073:
            data2 = list[num2];
            card_list.Add(data2.data);
            num2 += 1;
        Label_008D:
            if (num2 < list.Count)
            {
                goto Label_0073;
            }
            if (sort_order_type != 0x2000000)
            {
                goto Label_00AA;
            }
            card_list.Reverse();
        Label_00AA:
            return;
        }

        public void Start()
        {
            int num;
            Item item;
            int num2;
            Item item2;
            int num3;
            int num4;
            if (this.SortToggles != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_003C;
        Label_0013:
            item = this.SortToggles[num];
            item.toggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnSelect));
            num += 1;
        Label_003C:
            if (num < ((int) this.SortToggles.Length))
            {
                goto Label_0013;
            }
            num2 = 0;
            goto Label_007A;
        Label_0051:
            item2 = this.SortOrderToggles[num2];
            item2.toggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnSelectOrder));
            num2 += 1;
        Label_007A:
            if (num2 < ((int) this.SortOrderToggles.Length))
            {
                goto Label_0051;
            }
            this.CurrentType = LoadData();
            this.FirstType = this.CurrentType;
            num3 = 0;
            goto Label_00DC;
        Label_00A7:
            GameUtility.SetToggle(this.SortToggles[num3].toggle, this.SortToggles[num3].type == (this.CurrentType & 0xffffff));
            num3 += 1;
        Label_00DC:
            if (num3 < ((int) this.SortToggles.Length))
            {
                goto Label_00A7;
            }
            num4 = 0;
            goto Label_0128;
        Label_00F3:
            GameUtility.SetToggle(this.SortOrderToggles[num4].toggle, this.SortOrderToggles[num4].type == (this.CurrentType & 0xf000000));
            num4 += 1;
        Label_0128:
            if (num4 < ((int) this.SortOrderToggles.Length))
            {
                goto Label_00F3;
            }
            return;
        }

        [Serializable]
        public class Item
        {
            public Toggle toggle;
            public ConceptCardListSortWindow.Type type;

            public Item()
            {
                base..ctor();
                return;
            }
        }

        public enum ParentType
        {
            Manager,
            Equip
        }

        public class SortData
        {
            public ConceptCardData data;
            public long sort_val;

            public SortData(ConceptCardData _data, long _sort_val)
            {
                base..ctor();
                this.data = _data;
                this.sort_val = _sort_val;
                return;
            }
        }

        public enum Type
        {
            NONE = 0,
            LEVEL = 1,
            RARITY = 2,
            ATK = 4,
            DEF = 8,
            MAG = 0x10,
            MND = 0x20,
            SPD = 0x40,
            LUCK = 0x80,
            HP = 0x100,
            TIME = 0x200,
            TRUST = 0x400,
            AWAKE = 0x800,
            ASCENDING = 0x1000000,
            DESCENDING = 0x2000000
        }
    }
}

