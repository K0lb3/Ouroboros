namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitListRootWindow : FlowWindowBase
    {
        public const string UNITLIST_KEY = "unitlist";
        public const string PIECELIST_KEY = "piecelist";
        public const string SUPPORTLIST_KEY = "supportlist";
        public const int BTN_GROUP = 1;
        public const int TEXT_GROUP = 2;
        private const float SUPPORT_REFRESH_LOCK_TIME = 10f;
        private SerializeParam m_Param;
        private SerializeValueList m_ValueList;
        private UnitListWindow m_Root;
        private GameObject m_UnitList;
        private GameObject m_SupportList;
        private GameObject m_PieceList;
        private UnitListWindow.EditType m_EditType;
        private ContentType m_ContentType;
        private TabMaker m_Tab;
        private bool m_Destroy;
        private Content.ItemSource m_ContentSource;
        private ContentController m_ContentController;
        private Dictionary<string, ListData> m_Dict;
        private GameObject m_AccessoryRoot;
        private RectTransform[] m_MainSlotLabel;
        private RectTransform[] m_SubSlotLabel;
        private static UnitListRootWindow m_Instance;
        private ContentController m_PieceController;
        private ContentController m_SupportController;
        private SerializeValueList m_SupportValueList;
        private float m_SupportRefreshLock;
        private ContentController m_UnitController;
        [CompilerGenerated]
        private static Comparison<UnitListWindow.Data> <>f__am$cache16;
        [CompilerGenerated]
        private static Predicate<RectTransform> <>f__am$cache17;
        [CompilerGenerated]
        private static Comparison<UnitListWindow.Data> <>f__am$cache18;

        static UnitListRootWindow()
        {
        }

        public UnitListRootWindow()
        {
            this.m_Dict = new Dictionary<string, ListData>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static unsafe int <CreatePieceList>m__473(UnitListWindow.Data a, UnitListWindow.Data b)
        {
            int num;
            int num2;
            num = b.pieceAmount - a.pieceAmount;
            if (num == null)
            {
                goto Label_0016;
            }
            return num;
        Label_0016:
            num2 = b.param.rare - a.param.rare;
            if (num2 == null)
            {
                goto Label_0036;
            }
            return num2;
        Label_0036:
            return &a.sortPriority.CompareTo(b.sortPriority);
        }

        [CompilerGenerated]
        private static bool <InitializeUnitList>m__474(RectTransform mono)
        {
            return (mono.get_name() == "accessory");
        }

        [CompilerGenerated]
        private static unsafe int <SetupUnitList>m__476(UnitListWindow.Data p1, UnitListWindow.Data p2)
        {
            return &p1.subSortPriority.CompareTo(p2.subSortPriority);
        }

        public void AddData(string key, object value)
        {
            this.m_ValueList.AddObject(key, value);
            return;
        }

        public ListData AddListData(string key)
        {
            ListData data;
            data = new ListData();
            data.key = key;
            this.m_Dict.Add(key, data);
            return data;
        }

        public RectTransform AttachSlotLabel(UnitListWindow.Data data, ContentNode node)
        {
            RectTransform transform;
            transform = null;
            if (this.GetData<bool>("data_heroOnly", 0) != null)
            {
                goto Label_00CD;
            }
            if (data.partyMainSlot == -1)
            {
                goto Label_005C;
            }
            if (this.m_MainSlotLabel == null)
            {
                goto Label_00A0;
            }
            if (data.partyMainSlot < 0)
            {
                goto Label_00A0;
            }
            if (data.partyMainSlot >= ((int) this.m_MainSlotLabel.Length))
            {
                goto Label_00A0;
            }
            transform = this.m_MainSlotLabel[data.partyMainSlot];
            goto Label_00A0;
        Label_005C:
            if (data.partySubSlot == -1)
            {
                goto Label_00A0;
            }
            if (this.m_SubSlotLabel == null)
            {
                goto Label_00A0;
            }
            if (data.partySubSlot < 0)
            {
                goto Label_00A0;
            }
            if (data.partySubSlot >= ((int) this.m_SubSlotLabel.Length))
            {
                goto Label_00A0;
            }
            transform = this.m_SubSlotLabel[data.partySubSlot];
        Label_00A0:
            if ((transform != null) == null)
            {
                goto Label_00CD;
            }
            transform.set_anchoredPosition(new Vector2(0f, 0f));
            transform.get_gameObject().SetActive(1);
        Label_00CD:
            return transform;
        }

        public void CalcUnit(List<UnitListWindow.Data> list)
        {
            Tab tab;
            int num;
            UnitListWindow.Data data;
            tab = this.GetCurrentTab();
            num = list.Count - 1;
            goto Label_0035;
        Label_0015:
            data = list[num];
            if ((data.tabMask & tab) != null)
            {
                goto Label_0031;
            }
            list.RemoveAt(num);
        Label_0031:
            num -= 1;
        Label_0035:
            if (num >= 0)
            {
                goto Label_0015;
            }
            return;
        }

        public void ClearData()
        {
            List<SerializeValue> list;
            int num;
            SerializeValue value2;
            list = this.m_ValueList.list;
            num = 0;
            goto Label_0045;
        Label_0013:
            value2 = list[num];
            if (value2.key.IndexOf("data_") == -1)
            {
                goto Label_0041;
            }
            this.m_ValueList.RemoveFieldAt(num);
            num -= 1;
        Label_0041:
            num += 1;
        Label_0045:
            if (num < list.Count)
            {
                goto Label_0013;
            }
            return;
        }

        public ListData CreatePieceList()
        {
            ListData data;
            List<ItemParam> list;
            UnitParam[] paramArray;
            PlayerData data2;
            int num;
            UnitListWindow.Data data3;
            int num2;
            <CreatePieceList>c__AnonStorey3D3 storeyd;
            data = this.GetListData("piecelist");
            if (data != null)
            {
                goto Label_0023;
            }
            data = this.AddListData("piecelist");
            goto Label_0029;
        Label_0023:
            data.Delete();
        Label_0029:
            list = MonoSingleton<GameManager>.Instance.MasterParam.Items;
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnits();
            data2 = MonoSingleton<GameManager>.Instance.Player;
            num = 0;
            goto Label_0147;
        Label_005C:
            storeyd = new <CreatePieceList>c__AnonStorey3D3();
            storeyd.item = list[num];
            if (storeyd.item.type == 1)
            {
                goto Label_0089;
            }
            goto Label_0141;
        Label_0089:
            storeyd.focus = Array.Find<UnitParam>(paramArray, new Predicate<UnitParam>(storeyd.<>m__471));
            if (storeyd.focus == null)
            {
                goto Label_0141;
            }
            if (storeyd.focus.IsSummon() != null)
            {
                goto Label_00C5;
            }
            goto Label_0141;
        Label_00C5:
            if (data2.FindUnitDataByUnitID(storeyd.focus.iname) == null)
            {
                goto Label_00E1;
            }
            goto Label_0141;
        Label_00E1:
            if (storeyd.focus.CheckAvailable(TimeManager.ServerTime) != null)
            {
                goto Label_00FC;
            }
            goto Label_0141;
        Label_00FC:
            if (data.data.FindIndex(new Predicate<UnitListWindow.Data>(storeyd.<>m__472)) != -1)
            {
                goto Label_0141;
            }
            data3 = new UnitListWindow.Data(storeyd.focus);
            if (data3.unlockable == null)
            {
                goto Label_0141;
            }
            data.data.Add(data3);
        Label_0141:
            num += 1;
        Label_0147:
            if (num < list.Count)
            {
                goto Label_005C;
            }
            num2 = 0;
            goto Label_0177;
        Label_015C:
            data.data[num2].sortPriority = (long) num2;
            num2 += 1;
        Label_0177:
            if (num2 < data.data.Count)
            {
                goto Label_015C;
            }
            if (<>f__am$cache16 != null)
            {
                goto Label_01A7;
            }
            <>f__am$cache16 = new Comparison<UnitListWindow.Data>(UnitListRootWindow.<CreatePieceList>m__473);
        Label_01A7:
            data.data.Sort(<>f__am$cache16);
            data.calcData.AddRange(data.data);
            data.isValid = 1;
            return data;
        }

        public unsafe ListData CreateSupportList(EElement element)
        {
            ListData data;
            FlowNode_ReqSupportList.SupportList list;
            int num;
            SupportData[] dataArray;
            int num2;
            SupportData data2;
            UnitListWindow.Data data3;
            int num3;
            QuestParam param;
            int num4;
            UnitListWindow.Data data4;
            bool flag;
            int num5;
            bool flag2;
            data = this.GetSupportList(element);
            if (data != null)
            {
                goto Label_002E;
            }
            data = this.AddListData(this.GetSupportListKey(element));
            data.selectTab = this.GetTab(element);
            goto Label_0034;
        Label_002E:
            data.Delete();
        Label_0034:
            list = data.response as FlowNode_ReqSupportList.SupportList;
            if (list == null)
            {
                goto Label_009B;
            }
            if (list.m_SupportData == null)
            {
                goto Label_009B;
            }
            if (element != list.m_Element)
            {
                goto Label_009B;
            }
            num = 0;
            goto Label_008D;
        Label_0064:
            if (list.m_SupportData[num] == null)
            {
                goto Label_0089;
            }
            data.data.Add(new UnitListWindow.Data(list.m_SupportData[num]));
        Label_0089:
            num += 1;
        Label_008D:
            if (num < ((int) list.m_SupportData.Length))
            {
                goto Label_0064;
            }
        Label_009B:
            dataArray = this.GetData<SupportData[]>("data_support");
            num2 = this.GetData<int>("data_party_index", -1);
            if (dataArray == null)
            {
                goto Label_0135;
            }
            data2 = dataArray[num2];
            if (data2 == null)
            {
                goto Label_0135;
            }
            data3 = new UnitListWindow.Data("empty");
            data3.RefreshPartySlotPriority();
            data.data.Insert(0, data3);
            num3 = 0;
            goto Label_0123;
        Label_00F1:
            data.data[num3].partySelect = this.IsSameSupportUnit(data.data[num3].support, data2);
            num3 += 1;
        Label_0123:
            if (num3 < data.data.Count)
            {
                goto Label_00F1;
            }
        Label_0135:
            param = this.GetData<QuestParam>("data_quest", null);
            if (param == null)
            {
                goto Label_01E7;
            }
            num4 = 0;
            goto Label_01D5;
        Label_0152:
            data4 = data.data[num4];
            if (data4.unit == null)
            {
                goto Label_01CF;
            }
            flag = param.IsAvailableUnit(data4.unit);
            if (param.type != 15)
            {
                goto Label_01C6;
            }
            if (this.FindIncludingTeamIndexFromSupport(dataArray, data4.support, &num5) == null)
            {
                goto Label_01BE;
            }
            if (num5 == num2)
            {
                goto Label_01B0;
            }
            flag = 0;
        Label_01B0:
            data4.partyIndex = num5;
            goto Label_01C6;
        Label_01BE:
            data4.partyIndex = -1;
        Label_01C6:
            data4.interactable = flag;
        Label_01CF:
            num4 += 1;
        Label_01D5:
            if (num4 < data.data.Count)
            {
                goto Label_0152;
            }
        Label_01E7:
            data.calcData.AddRange(data.data);
            data.isValid = 1;
            return data;
        }

        public unsafe ListData CreateUnitList(UnitListWindow.EditType editType, UnitData[] units)
        {
            ListData data;
            int num;
            PartyEditData[] dataArray;
            int num2;
            int num3;
            QuestParam param;
            PartyEditData data2;
            int num4;
            bool flag;
            int num5;
            UnitListWindow.Data data3;
            UnitListWindow.Data data4;
            int num6;
            UnitListWindow.Data data5;
            bool flag2;
            string str;
            int num7;
            bool flag3;
            int num8;
            UnitListWindow.Data data6;
            UnitData data7;
            EElement element;
            UnitListWindow.Data data8;
            int num9;
            UnitListWindow.Data data9;
            <CreateUnitList>c__AnonStorey3D4 storeyd;
            data = this.GetListData("unitlist");
            if (data != null)
            {
                goto Label_0023;
            }
            data = this.AddListData("unitlist");
            goto Label_0029;
        Label_0023:
            data.Delete();
        Label_0029:
            if (units != null)
            {
                goto Label_0045;
            }
            units = MonoSingleton<GameManager>.Instance.Player.Units.ToArray();
        Label_0045:
            if (units == null)
            {
                goto Label_0430;
            }
            num = 0;
            goto Label_0071;
        Label_0052:
            if (units[num] == null)
            {
                goto Label_006D;
            }
            data.data.Add(new UnitListWindow.Data(units[num]));
        Label_006D:
            num += 1;
        Label_0071:
            if (num < ((int) units.Length))
            {
                goto Label_0052;
            }
            if ((editType != 1) && (editType != 2))
            {
                goto Label_0328;
            }
            dataArray = this.GetData<PartyEditData[]>("data_party", null);
            num2 = this.GetData<int>("data_party_index", -1);
            num3 = this.GetData<int>("data_slot", -1);
            param = this.GetData<QuestParam>("data_quest", null);
            data2 = (dataArray != null) ? dataArray[num2] : null;
            if (((data2 == null) || (num3 == -1)) || (num3 >= data2.PartyData.MAX_UNIT))
            {
                goto Label_0430;
            }
            num4 = data2.GetMainMemberCount();
            flag = data2.IsSubSlot(num3);
            num5 = 0;
            goto Label_01E6;
        Label_010D:
            storeyd = new <CreateUnitList>c__AnonStorey3D4();
            storeyd.unit = data2.Units[num5];
            if (storeyd.unit != null)
            {
                goto Label_0136;
            }
            goto Label_01E0;
        Label_0136:
            data3 = data.data.Find(new Predicate<UnitListWindow.Data>(storeyd.<>m__475));
            if (data3 == null)
            {
                goto Label_01E0;
            }
            if (((num5 != null) || (flag == null)) || ((num4 > 1) || (data2.Units[num3] != null)))
            {
                goto Label_018F;
            }
            data.data.Remove(data3);
            goto Label_01E0;
        Label_018F:
            if (data2.IsSubSlot(num5) == null)
            {
                goto Label_01B7;
            }
            data3.partySubSlot = data2.GetSubSlotNum(data3.GetUniq());
            goto Label_01CC;
        Label_01B7:
            data3.partyMainSlot = data2.GetMainSlotNum(data3.GetUniq());
        Label_01CC:
            data3.partySelect = num3 == num5;
            data3.RefreshPartySlotPriority();
        Label_01E0:
            num5 += 1;
        Label_01E6:
            if (num5 < data2.PartyData.MAX_UNIT)
            {
                goto Label_010D;
            }
            if ((data2.Units[num3] == null) || ((num3 == null) && (this.GetData<bool>("data_heroOnly", 0) == null)))
            {
                goto Label_0241;
            }
            data4 = new UnitListWindow.Data("empty");
            data4.RefreshPartySlotPriority();
            data.data.Insert(0, data4);
        Label_0241:
            if (param == null)
            {
                goto Label_0430;
            }
            num6 = 0;
            goto Label_0311;
        Label_0250:
            data5 = data.data[num6];
            if (data5.unit == null)
            {
                goto Label_030B;
            }
            flag2 = ((param.IsUnitAllowed(data5.unit) != null) || (data5.partyMainSlot != -1)) ? 1 : ((data5.partySubSlot == -1) == 0);
            str = null;
            flag2 &= param.IsEntryQuestCondition(data5.unit, &str);
            if (param.type != 15)
            {
                goto Label_0302;
            }
            if (this.FindIncludingTeamIndexFromParty(dataArray, data5.unit.UniqueID, &num7) == null)
            {
                goto Label_02FA;
            }
            if (num7 == num2)
            {
                goto Label_02EC;
            }
            flag2 = 0;
        Label_02EC:
            data5.partyIndex = num7;
            goto Label_0302;
        Label_02FA:
            data5.partyIndex = -1;
        Label_0302:
            data5.interactable = flag2;
        Label_030B:
            num6 += 1;
        Label_0311:
            if (num6 < data.data.Count)
            {
                goto Label_0250;
            }
            goto Label_0430;
        Label_0328:
            if (editType == 3)
            {
                goto Label_0336;
            }
            if (editType != 5)
            {
                goto Label_0389;
            }
        Label_0336:
            num8 = 0;
            goto Label_0372;
        Label_033E:
            data6 = data.data[num8];
            if (data6.unit == null)
            {
                goto Label_036C;
            }
            data6.interactable = data6.unit.CheckEnableEnhanceEquipment();
        Label_036C:
            num8 += 1;
        Label_0372:
            if (num8 < data.data.Count)
            {
                goto Label_033E;
            }
            goto Label_0430;
        Label_0389:
            if (editType != 4)
            {
                goto Label_0430;
            }
            data7 = this.GetData<UnitData>("data_unit", null);
            if (this.GetData<int>("data_element", 0) == null)
            {
                goto Label_03DB;
            }
            if (data7 == null)
            {
                goto Label_03DB;
            }
            data8 = new UnitListWindow.Data("empty");
            data8.RefreshPartySlotPriority();
            data.data.Insert(0, data8);
        Label_03DB:
            num9 = 0;
            goto Label_041E;
        Label_03E3:
            data9 = data.data[num9];
            if (data7 == null)
            {
                goto Label_0410;
            }
            data9.partySelect = data9.unit == data7;
            goto Label_0418;
        Label_0410:
            data9.partySelect = 0;
        Label_0418:
            num9 += 1;
        Label_041E:
            if (num9 < data.data.Count)
            {
                goto Label_03E3;
            }
        Label_0430:
            data.calcData.AddRange(data.data);
            data.isValid = 1;
            return data;
        }

        public void DettachSlotLabel(RectTransform tr)
        {
            if ((tr != null) == null)
            {
                goto Label_0023;
            }
            tr.set_anchoredPosition(Vector2.get_zero());
            tr.get_gameObject().SetActive(0);
        Label_0023:
            return;
        }

        private unsafe bool FindIncludingTeamIndexFromParty(PartyEditData[] partyEditDataAry, long uniqueID, out int index)
        {
            int num;
            PartyEditData data;
            UnitData data2;
            UnitData[] dataArray;
            int num2;
            *((int*) index) = 0;
            num = 0;
            goto Label_0052;
        Label_000A:
            data = partyEditDataAry[num];
            dataArray = data.Units;
            num2 = 0;
            goto Label_0044;
        Label_001D:
            data2 = dataArray[num2];
            if (data2 != null)
            {
                goto Label_002D;
            }
            goto Label_003E;
        Label_002D:
            if (data2.UniqueID != uniqueID)
            {
                goto Label_003E;
            }
            *((int*) index) = num;
            return 1;
        Label_003E:
            num2 += 1;
        Label_0044:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_001D;
            }
            num += 1;
        Label_0052:
            if (num < ((int) partyEditDataAry.Length))
            {
                goto Label_000A;
            }
            return 0;
        }

        private unsafe bool FindIncludingTeamIndexFromSupport(SupportData[] supports, SupportData target, out int index)
        {
            int num;
            SupportData data;
            *((int*) index) = 0;
            num = 0;
            goto Label_002F;
        Label_000A:
            data = supports[num];
            if (data != null)
            {
                goto Label_0019;
            }
            goto Label_002B;
        Label_0019:
            if (this.IsSameSupportUnit(data, target) == null)
            {
                goto Label_002B;
            }
            *((int*) index) = num;
            return 1;
        Label_002B:
            num += 1;
        Label_002F:
            if (num < ((int) supports.Length))
            {
                goto Label_000A;
            }
            return 0;
        }

        public ContentType GetContentType()
        {
            return this.m_ContentType;
        }

        public Tab GetCurrentTab()
        {
            TabMaker.Info info;
            if ((this.m_Tab != null) == null)
            {
                goto Label_002F;
            }
            info = this.m_Tab.GetOnIfno();
            if (info == null)
            {
                goto Label_002F;
            }
            return info.element.value;
        Label_002F:
            return 0xffff;
        }

        public Vector2 GetCurrentTabAnchore()
        {
            if ((this.m_ContentController != null) == null)
            {
                goto Label_001D;
            }
            return this.m_ContentController.anchoredPosition;
        Label_001D:
            return Vector2.get_zero();
        }

        public object GetData(string key)
        {
            return this.m_ValueList.GetObject(key);
        }

        public T GetData<T>(string key)
        {
            return this.m_ValueList.GetObject<T>(key);
        }

        public object GetData(string key, object defaultValue)
        {
            return this.m_ValueList.GetObject(key, defaultValue);
        }

        public T GetData<T>(string key, T defaultValue)
        {
            return this.m_ValueList.GetObject<T>(key, defaultValue);
        }

        public UnitListWindow.EditType GetEditType()
        {
            return this.m_EditType;
        }

        public EElement GetElement()
        {
            Tab tab;
            EElement element;
            tab = this.GetCurrentTab();
            element = 0;
            if (tab != 2)
            {
                goto Label_0017;
            }
            element = 1;
            goto Label_005B;
        Label_0017:
            if (tab != 4)
            {
                goto Label_0025;
            }
            element = 2;
            goto Label_005B;
        Label_0025:
            if (tab != 8)
            {
                goto Label_0033;
            }
            element = 4;
            goto Label_005B;
        Label_0033:
            if (tab != 0x10)
            {
                goto Label_0042;
            }
            element = 3;
            goto Label_005B;
        Label_0042:
            if (tab != 0x20)
            {
                goto Label_0051;
            }
            element = 5;
            goto Label_005B;
        Label_0051:
            if (tab != 0x40)
            {
                goto Label_005B;
            }
            element = 6;
        Label_005B:
            return element;
        }

        public EElement GetElement(Tab tab)
        {
            EElement element;
            element = 0;
            if (tab != 2)
            {
                goto Label_0010;
            }
            element = 1;
            goto Label_0054;
        Label_0010:
            if (tab != 4)
            {
                goto Label_001E;
            }
            element = 2;
            goto Label_0054;
        Label_001E:
            if (tab != 8)
            {
                goto Label_002C;
            }
            element = 4;
            goto Label_0054;
        Label_002C:
            if (tab != 0x10)
            {
                goto Label_003B;
            }
            element = 3;
            goto Label_0054;
        Label_003B:
            if (tab != 0x20)
            {
                goto Label_004A;
            }
            element = 5;
            goto Label_0054;
        Label_004A:
            if (tab != 0x40)
            {
                goto Label_0054;
            }
            element = 6;
        Label_0054:
            return element;
        }

        public unsafe ListData GetListData(string key)
        {
            ListData data;
            data = null;
            this.m_Dict.TryGetValue(key, &data);
            return data;
        }

        private int GetOutputSupportWebApiPin(EElement element, bool isForce)
        {
            this.m_SupportValueList.SetField("element", element);
            return ((isForce == null) ? 480 : 0x1e1);
        }

        private ListData GetSupportList(EElement element)
        {
            return this.GetListData(this.GetSupportListKey(element));
        }

        private ListData GetSupportList(FlowNode_ReqSupportList.SupportList support)
        {
            return this.GetSupportList((support == null) ? 0 : support.m_Element);
        }

        private string GetSupportListKey(EElement element)
        {
            return ("supportlist_" + ((EElement) element).ToString());
        }

        public Tab GetTab(EElement element)
        {
            Tab tab;
            tab = 0xffff;
            if (element != 1)
            {
                goto Label_0014;
            }
            tab = 2;
            goto Label_0069;
        Label_0014:
            if (element != 2)
            {
                goto Label_0022;
            }
            tab = 4;
            goto Label_0069;
        Label_0022:
            if (element != 4)
            {
                goto Label_0030;
            }
            tab = 8;
            goto Label_0069;
        Label_0030:
            if (element != 3)
            {
                goto Label_003F;
            }
            tab = 0x10;
            goto Label_0069;
        Label_003F:
            if (element != 5)
            {
                goto Label_004E;
            }
            tab = 0x20;
            goto Label_0069;
        Label_004E:
            if (element != 6)
            {
                goto Label_005D;
            }
            tab = 0x40;
            goto Label_0069;
        Label_005D:
            if (element != null)
            {
                goto Label_0069;
            }
            tab = 0x80;
        Label_0069:
            return tab;
        }

        private string[] GetTabKeys(Tab[] tabs)
        {
            List<string> list;
            int num;
            list = new List<string>();
            num = 0;
            goto Label_0024;
        Label_000D:
            list.Add(((Tab) tabs[num]).ToString());
            num += 1;
        Label_0024:
            if (num < ((int) tabs.Length))
            {
                goto Label_000D;
            }
            return list.ToArray();
        }

        public static Tab GetTabMask(UnitListWindow.Data data)
        {
            UnitParam param;
            Tab tab;
            UnitData data2;
            if (data.param != null)
            {
                goto Label_0011;
            }
            return 0xffff;
        Label_0011:
            param = data.param;
            tab = 0;
            if (data.unit == null)
            {
                goto Label_003B;
            }
            if (data.unit.IsFavorite == null)
            {
                goto Label_003B;
            }
            tab |= 1;
        Label_003B:
            if (param == null)
            {
                goto Label_00BD;
            }
            if (param.element != 1)
            {
                goto Label_0056;
            }
            tab |= 2;
            goto Label_00BD;
        Label_0056:
            if (param.element != 2)
            {
                goto Label_006B;
            }
            tab |= 4;
            goto Label_00BD;
        Label_006B:
            if (param.element != 4)
            {
                goto Label_0080;
            }
            tab |= 8;
            goto Label_00BD;
        Label_0080:
            if (param.element != 3)
            {
                goto Label_0096;
            }
            tab |= 0x10;
            goto Label_00BD;
        Label_0096:
            if (param.element != 5)
            {
                goto Label_00AC;
            }
            tab |= 0x20;
            goto Label_00BD;
        Label_00AC:
            if (param.element != 6)
            {
                goto Label_00BD;
            }
            tab |= 0x40;
        Label_00BD:
            return tab;
        }

        public override void Initialize(FlowWindowBase.SerializeParamBase param)
        {
            SerializeValueBehaviour behaviour;
            m_Instance = this;
            base.Initialize(param);
            this.m_Param = param as SerializeParam;
            if (this.m_Param != null)
            {
                goto Label_003A;
            }
            throw new Exception(this.ToString() + " > Failed serializeParam null.");
        Label_003A:
            behaviour = base.GetChildComponent<SerializeValueBehaviour>("root");
            if ((behaviour != null) == null)
            {
                goto Label_0063;
            }
            this.m_ValueList = behaviour.list;
            goto Label_006E;
        Label_0063:
            this.m_ValueList = new SerializeValueList();
        Label_006E:
            this.m_ValueList.SetActive(1, 0);
            this.m_ValueList.SetActive(2, 0);
            base.Close(1);
            return;
        }

        public void InitializeContentList(ContentType contentType)
        {
            ContentType type;
            this.m_Destroy = 0;
            this.m_ContentType = contentType;
            switch ((this.m_ContentType - 1))
            {
                case 0:
                    goto Label_002E;

                case 1:
                    goto Label_0040;

                case 2:
                    goto Label_0052;
            }
            goto Label_0064;
        Label_002E:
            this.m_ContentSource = this.SetupUnitList(null);
            goto Label_0064;
        Label_0040:
            this.m_ContentSource = this.SetupPieceList(null);
            goto Label_0064;
        Label_0052:
            this.m_ContentSource = this.SetupSupportList(null);
        Label_0064:
            if ((this.m_ContentController != null) == null)
            {
                goto Label_00B1;
            }
            if (this.m_ContentController.GetNodeCount() != null)
            {
                goto Label_00A0;
            }
            this.m_ContentController.Initialize(this.m_ContentSource, Vector2.get_zero());
            goto Label_00B1;
        Label_00A0:
            this.m_ContentController.SetCurrentSource(this.m_ContentSource);
        Label_00B1:
            return;
        }

        private void InitializePieceList()
        {
            if ((this.m_PieceList != null) == null)
            {
                goto Label_004B;
            }
            this.m_PieceController = this.m_PieceList.GetComponentInChildren<ContentController>();
            if ((this.m_PieceController != null) == null)
            {
                goto Label_003F;
            }
            this.m_PieceController.SetWork(this);
        Label_003F:
            this.m_PieceList.SetActive(0);
        Label_004B:
            return;
        }

        private void InitializeSupportList()
        {
            SerializeValueBehaviour behaviour;
            if ((this.m_SupportList != null) == null)
            {
                goto Label_007F;
            }
            this.m_SupportController = this.m_SupportList.GetComponentInChildren<ContentController>();
            if ((this.m_SupportController != null) == null)
            {
                goto Label_003F;
            }
            this.m_SupportController.SetWork(this);
        Label_003F:
            this.m_SupportList.SetActive(0);
            behaviour = this.m_SupportList.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_0074;
            }
            this.m_SupportValueList = behaviour.list;
            goto Label_007F;
        Label_0074:
            this.m_SupportValueList = new SerializeValueList();
        Label_007F:
            return;
        }

        private void InitializeUnitList()
        {
            Transform transform;
            int num;
            RectTransform[] transformArray;
            RectTransform transform2;
            List<RectTransform> list;
            if ((this.m_UnitList == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.m_UnitController = this.m_UnitList.GetComponentInChildren<ContentController>();
            if ((this.m_UnitController != null) == null)
            {
                goto Label_0075;
            }
            transform = this.m_UnitController.get_transform();
            num = 0;
            goto Label_005D;
        Label_0047:
            transform.GetChild(num).get_gameObject().SetActive(0);
            num += 1;
        Label_005D:
            if (num < transform.get_childCount())
            {
                goto Label_0047;
            }
            this.m_UnitController.SetWork(this);
        Label_0075:
            this.m_UnitList.SetActive(0);
            if (<>f__am$cache17 != null)
            {
                goto Label_00A6;
            }
            <>f__am$cache17 = new Predicate<RectTransform>(UnitListRootWindow.<InitializeUnitList>m__474);
        Label_00A6:
            transform2 = Array.Find<RectTransform>(this.m_UnitList.GetComponentsInChildren<RectTransform>(), <>f__am$cache17);
            if ((transform2 != null) == null)
            {
                goto Label_01A6;
            }
            this.m_AccessoryRoot = transform2.get_gameObject();
            list = new List<RectTransform>();
            base.SetActiveChild(this.m_AccessoryRoot, 0);
            list.Add(base.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_leader"));
            list.Add(base.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main2"));
            list.Add(base.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main3"));
            list.Add(base.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main4"));
            list.Add(base.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_main5"));
            this.m_MainSlotLabel = list.ToArray();
            list.Clear();
            list.Add(base.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_sub1"));
            list.Add(base.GetChildComponent<RectTransform>(this.m_AccessoryRoot, "party_sub2"));
            this.m_SubSlotLabel = list.ToArray();
        Label_01A6:
            return;
        }

        private bool IsSameSupportUnit(SupportData sup_1, SupportData sup_2)
        {
            if (sup_1 == null)
            {
                goto Label_005A;
            }
            if (sup_2.Unit == null)
            {
                goto Label_005A;
            }
            if ((sup_1.FUID == sup_2.FUID) == null)
            {
                goto Label_005A;
            }
            if ((sup_1.UnitID == sup_2.UnitID) == null)
            {
                goto Label_005A;
            }
            if (sup_1.Unit.SupportElement != sup_2.Unit.SupportElement)
            {
                goto Label_005A;
            }
            return 1;
        Label_005A:
            return 0;
        }

        public override int OnActivate(int pinId)
        {
            if (this.m_ContentType == 1)
            {
                goto Label_003C;
            }
            if (pinId == 100)
            {
                goto Label_003C;
            }
            if (pinId == 0x65)
            {
                goto Label_003C;
            }
            if (pinId == 0x66)
            {
                goto Label_003C;
            }
            if (pinId == 0x67)
            {
                goto Label_003C;
            }
            if (pinId == 0x69)
            {
                goto Label_003C;
            }
            if (pinId != 0x68)
            {
                goto Label_0044;
            }
        Label_003C:
            return this.OnActivate_Unit(pinId);
        Label_0044:
            if (this.m_ContentType == 2)
            {
                goto Label_0058;
            }
            if (pinId != 110)
            {
                goto Label_0060;
            }
        Label_0058:
            return this.OnActivate_Piece(pinId);
        Label_0060:
            if (this.m_ContentType == 3)
            {
                goto Label_0077;
            }
            if (pinId != 300)
            {
                goto Label_007F;
            }
        Label_0077:
            return this.OnActivate_Support(pinId);
        Label_007F:
            return -1;
        }

        private int OnActivate_Base(int pinId)
        {
            if (pinId != 490)
            {
                goto Label_0041;
            }
            this.m_ContentType = 0;
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            this.m_Destroy = 1;
            base.Close(0);
            ButtonEvent.ResetLock("unitlist");
            return 0x1eb;
        Label_0041:
            if (pinId != 400)
            {
                goto Label_007D;
            }
            this.RegistAnchorePos(Vector2.get_zero());
            this.RefreshContentList();
            if (base.isClosed == null)
            {
                goto Label_006E;
            }
            base.Open();
        Label_006E:
            ButtonEvent.ResetLock("unitlist");
            goto Label_0102;
        Label_007D:
            if (pinId != 410)
            {
                goto Label_00BF;
            }
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            this.RefreshContentList();
            if (base.isClosed == null)
            {
                goto Label_00B0;
            }
            base.Open();
        Label_00B0:
            ButtonEvent.ResetLock("unitlist");
            goto Label_0102;
        Label_00BF:
            if (pinId != 430)
            {
                goto Label_0102;
            }
            this.RemoveListDataAll();
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            this.RefreshContentList();
            if (base.isClosed == null)
            {
                goto Label_00F8;
            }
            base.Open();
        Label_00F8:
            ButtonEvent.ResetLock("unitlist");
        Label_0102:
            return -1;
        }

        private int OnActivate_Piece(int pinId)
        {
            SerializeValueList list;
            UnitParam param;
            if (pinId != 110)
            {
                goto Label_001A;
            }
            this.InitializeContentList(2);
            base.Open();
            goto Label_00B5;
        Label_001A:
            if (pinId != 490)
            {
                goto Label_0037;
            }
            this.InitializeContentList(1);
            base.Open();
            goto Label_00B5;
        Label_0037:
            if (pinId != 400)
            {
                goto Label_004A;
            }
            return this.OnActivate_Base(pinId);
        Label_004A:
            if (pinId != 430)
            {
                goto Label_0063;
            }
            this.RemoveListDataAll();
            return this.OnActivate_Base(pinId);
        Label_0063:
            if (pinId != 410)
            {
                goto Label_0076;
            }
            return this.OnActivate_Base(pinId);
        Label_0076:
            if (pinId != 420)
            {
                goto Label_00B5;
            }
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_00B5;
            }
            param = list.GetDataSource<UnitParam>("_self");
            if (param == null)
            {
                goto Label_00B5;
            }
            GlobalVars.UnlockUnitID = param.iname;
            return 0x1a7;
        Label_00B5:
            return -1;
        }

        private int OnActivate_Support(int pinId)
        {
            EElement element;
            ListData data;
            EElement element2;
            ListData data2;
            EElement element3;
            SerializeValueList list;
            SupportData data3;
            SerializeValueList list2;
            SupportData data4;
            if (pinId != 300)
            {
                goto Label_001E;
            }
            this.InitializeContentList(3);
            return this.OnActivate_Support(400);
        Label_001E:
            if (pinId != 490)
            {
                goto Label_0031;
            }
            return this.OnActivate_Base(pinId);
        Label_0031:
            if (pinId != 400)
            {
                goto Label_0091;
            }
            element = this.GetElement(this.GetCurrentTab());
            data = this.GetSupportList(element);
            if (data == null)
            {
                goto Label_005D;
            }
            data.Delete();
        Label_005D:
            if (data == null)
            {
                goto Label_006E;
            }
            if (data.response != null)
            {
                goto Label_0089;
            }
        Label_006E:
            this.m_ValueList.SetInteractable("btn_refresh", 0);
            return this.GetOutputSupportWebApiPin(element, 1);
        Label_0089:
            return this.OnActivate_Base(pinId);
        Label_0091:
            if (pinId != 430)
            {
                goto Label_0123;
            }
            element2 = this.GetElement(this.GetCurrentTab());
            data2 = this.GetSupportList(element2);
            if (data2 != null)
            {
                goto Label_00BF;
            }
            data2 = this.CreateSupportList(element2);
        Label_00BF:
            data2.Delete();
            data2.response = this.GetData<FlowNode_ReqSupportList.SupportList>("data_supportres");
            this.RemoveData("data_supportres");
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            this.RefreshContentList();
            this.m_SupportRefreshLock = Time.get_realtimeSinceStartup();
            if (base.isClosed == null)
            {
                goto Label_0114;
            }
            base.Open();
        Label_0114:
            ButtonEvent.ResetLock("unitlist");
            goto Label_0226;
        Label_0123:
            if (pinId != 410)
            {
                goto Label_0136;
            }
            return this.OnActivate_Base(pinId);
        Label_0136:
            if (pinId != 440)
            {
                goto Label_016B;
            }
            this.m_ValueList.SetInteractable("btn_refresh", 0);
            element3 = this.GetElement(this.GetCurrentTab());
            return this.GetOutputSupportWebApiPin(element3, 0);
        Label_016B:
            if (pinId != 420)
            {
                goto Label_01C0;
            }
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_0226;
            }
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            if (list.GetDataSource<SupportData>("_self") == null)
            {
                goto Label_01B5;
            }
            return 0x1aa;
        Label_01B5:
            return 0x1ab;
            goto Label_0226;
        Label_01C0:
            if (pinId != 0x1a3)
            {
                goto Label_0226;
            }
            if (string.IsNullOrEmpty(this.m_Param.tooltipPrefab) != null)
            {
                goto Label_0220;
            }
            list2 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list2 == null)
            {
                goto Label_0220;
            }
            data4 = list2.GetDataSource<SupportData>("_self");
            if (data4 == null)
            {
                goto Label_0220;
            }
            this.ShowToolTip(this.m_Param.tooltipPrefab, data4.Unit);
        Label_0220:
            return 0x1ad;
        Label_0226:
            return -1;
        }

        private int OnActivate_Unit(int pinId)
        {
            UnitListWindow.EditType type;
            SerializeValueList list;
            UnitData data;
            ListData data2;
            SerializeValueList list2;
            UnitData data3;
            if (pinId == 100)
            {
                goto Label_0030;
            }
            if (pinId == 0x65)
            {
                goto Label_0030;
            }
            if (pinId == 0x66)
            {
                goto Label_0030;
            }
            if (pinId == 0x67)
            {
                goto Label_0030;
            }
            if (pinId == 0x69)
            {
                goto Label_0030;
            }
            if (pinId != 0x68)
            {
                goto Label_00D3;
            }
        Label_0030:
            if (this.m_ValueList.HasField("data_edit") != null)
            {
                goto Label_00C1;
            }
            type = 0;
            if (pinId != 0x65)
            {
                goto Label_0056;
            }
            type = 1;
            goto Label_008D;
        Label_0056:
            if (pinId != 0x66)
            {
                goto Label_0065;
            }
            type = 2;
            goto Label_008D;
        Label_0065:
            if (pinId != 0x67)
            {
                goto Label_0074;
            }
            type = 3;
            goto Label_008D;
        Label_0074:
            if (pinId != 0x68)
            {
                goto Label_0083;
            }
            type = 4;
            goto Label_008D;
        Label_0083:
            if (pinId != 0x69)
            {
                goto Label_008D;
            }
            type = 5;
        Label_008D:
            this.AddData("data_edit", (UnitListWindow.EditType) type);
            if (pinId != 0x68)
            {
                goto Label_00C1;
            }
            this.AddData("data_unit", this.m_ValueList.GetDataSource<UnitData>("_self"));
        Label_00C1:
            this.InitializeContentList(1);
            base.Open();
            goto Label_026D;
        Label_00D3:
            if (pinId != 110)
            {
                goto Label_00ED;
            }
            this.InitializeContentList(2);
            base.Open();
            goto Label_026D;
        Label_00ED:
            if (pinId != 490)
            {
                goto Label_0100;
            }
            return this.OnActivate_Base(pinId);
        Label_0100:
            if (pinId != 400)
            {
                goto Label_0113;
            }
            return this.OnActivate_Base(pinId);
        Label_0113:
            if (pinId != 430)
            {
                goto Label_012C;
            }
            this.RemoveListDataAll();
            return this.OnActivate_Base(pinId);
        Label_012C:
            if (pinId != 410)
            {
                goto Label_013F;
            }
            return this.OnActivate_Base(pinId);
        Label_013F:
            if (pinId != 420)
            {
                goto Label_0201;
            }
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_026D;
            }
            this.RegistAnchorePos(this.m_ContentController.anchoredPosition);
            data = list.GetDataSource<UnitData>("_self");
            if (data == null)
            {
                goto Label_01F6;
            }
            data2 = this.GetListData("unitlist");
            if (data2 == null)
            {
                goto Label_026D;
            }
            data2.selectUniqueID = data.UniqueID;
            if (this.m_EditType == null)
            {
                goto Label_01CB;
            }
            if (this.m_EditType == 3)
            {
                goto Label_01CB;
            }
            if (this.m_EditType == 5)
            {
                goto Label_01CB;
            }
            if (this.m_EditType != 4)
            {
                goto Label_01EB;
            }
        Label_01CB:
            GlobalVars.SelectedUnitUniqueID.Set(data.UniqueID);
            GlobalVars.SelectedUnitJobIndex.Set(data.JobIndex);
        Label_01EB:
            return 0x1a5;
            goto Label_01FC;
        Label_01F6:
            return 0x1a6;
        Label_01FC:
            goto Label_026D;
        Label_0201:
            if (pinId != 0x1a3)
            {
                goto Label_026D;
            }
            if (this.m_EditType == null)
            {
                goto Label_0267;
            }
            if (string.IsNullOrEmpty(this.m_Param.tooltipPrefab) != null)
            {
                goto Label_0267;
            }
            list2 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list2 == null)
            {
                goto Label_0267;
            }
            data3 = list2.GetDataSource<UnitData>("_self");
            if (data3 == null)
            {
                goto Label_0267;
            }
            this.ShowToolTip(this.m_Param.tooltipPrefab, data3);
        Label_0267:
            return 0x1ad;
        Label_026D:
            return -1;
        }

        protected override int OnClosed()
        {
            if (this.m_Destroy == null)
            {
                goto Label_0011;
            }
            return 0x1ec;
        Label_0011:
            return -1;
        }

        protected override int OnOpened()
        {
            return 0xbf;
        }

        public void RefreshContentList()
        {
            ContentType type;
            if ((this.m_ContentController != null) == null)
            {
                goto Label_001D;
            }
            this.m_ContentController.SetCurrentSource(null);
        Label_001D:
            switch ((this.m_ContentType - 1))
            {
                case 0:
                    goto Label_003D;

                case 1:
                    goto Label_004F;

                case 2:
                    goto Label_0061;
            }
            goto Label_0073;
        Label_003D:
            this.SetupUnitList(this.m_ContentSource);
            goto Label_0073;
        Label_004F:
            this.SetupPieceList(this.m_ContentSource);
            goto Label_0073;
        Label_0061:
            this.SetupSupportList(this.m_ContentSource);
        Label_0073:
            if ((this.m_ContentController != null) == null)
            {
                goto Label_0095;
            }
            this.m_ContentController.SetCurrentSource(this.m_ContentSource);
        Label_0095:
            return;
        }

        public unsafe void RegistAnchorePos(Vector2 pos)
        {
            KeyValuePair<string, ListData> pair;
            Dictionary<string, ListData>.Enumerator enumerator;
            ListData data;
            enumerator = this.m_Dict.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_002E;
            Label_0011:
                pair = &enumerator.Current;
                data = &pair.Value;
                if (data == null)
                {
                    goto Label_002E;
                }
                data.anchorePos = pos;
            Label_002E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_004B;
            }
            finally
            {
            Label_003F:
                ((Dictionary<string, ListData>.Enumerator) enumerator).Dispose();
            }
        Label_004B:
            return;
        }

        public void RegistAnchorePos(string key, Vector2 pos)
        {
            ListData data;
            data = this.GetListData(key);
            if (data == null)
            {
                goto Label_0015;
            }
            data.anchorePos = pos;
        Label_0015:
            return;
        }

        public override void Release()
        {
            this.ReleaseContentList();
            base.Release();
            return;
        }

        public void ReleaseContentList()
        {
            if ((this.m_ContentController != null) == null)
            {
                goto Label_001C;
            }
            this.m_ContentController.Release();
        Label_001C:
            this.m_ContentSource = null;
            return;
        }

        public void RemoveData(string key)
        {
            this.m_ValueList.RemoveField(key);
            return;
        }

        public unsafe void RemoveListDataAll()
        {
            KeyValuePair<string, ListData> pair;
            Dictionary<string, ListData>.Enumerator enumerator;
            enumerator = this.m_Dict.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0025;
            Label_0011:
                pair = &enumerator.Current;
                &pair.Value.Delete();
            Label_0025:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_0042;
            }
            finally
            {
            Label_0036:
                ((Dictionary<string, ListData>.Enumerator) enumerator).Dispose();
            }
        Label_0042:
            return;
        }

        public void SetRoot(UnitListWindow root)
        {
            this.m_Root = root;
            return;
        }

        private Content.ItemSource SetupPieceList(Content.ItemSource source)
        {
            Tab[] tabArray1;
            bool flag;
            ListData data;
            ListData data2;
            Tab tab;
            ContentNode node;
            int num;
            UnitListWindow.Data data3;
            Content.ItemSource.ItemParam param;
            flag = 0;
            data = this.GetListData("unitlist");
            data2 = this.GetListData("piecelist");
            if (((data2 != null) && (data2.isValid != null)) && (source != null))
            {
                goto Label_003A;
            }
            data2 = this.CreatePieceList();
            flag = 1;
        Label_003A:
            if (source != null)
            {
                goto Label_015D;
            }
            source = new Content.ItemSource();
            tab = ((data == null) || (data.selectTab == null)) ? 0xffff : data.selectTab;
            if (tab != 1)
            {
                goto Label_0076;
            }
            tab = 0xffff;
        Label_0076:
            tabArray1 = new Tab[] { 0xffff, 2, 4, 8, 0x10, 0x20, 0x40 };
            this.SetupTab(tabArray1, tab);
            if ((this.Param_PieceList != null) == null)
            {
                goto Label_010C;
            }
            node = this.m_ValueList.GetComponent<ContentNode>("node_piece");
            if ((node != null) == null)
            {
                goto Label_0100;
            }
            this.m_ContentController = this.m_PieceController;
            this.m_ContentController.m_Node = node;
            this.m_ContentController.get_gameObject().SetActive(1);
        Label_0100:
            this.Param_PieceList.SetActive(1);
        Label_010C:
            this.m_ValueList.SetActive(1, 0);
            this.m_ValueList.SetActive(2, 0);
            this.m_ValueList.SetActive("btn_close", 1);
            this.m_ValueList.SetActive("desc_piece", 1);
            data.anchorePos = this.m_ContentController.anchoredPosition;
            flag = 1;
        Label_015D:
            if ((this.m_UnitList != null) == null)
            {
                goto Label_017A;
            }
            this.m_UnitList.SetActive(0);
        Label_017A:
            if ((this.m_SupportList != null) == null)
            {
                goto Label_0197;
            }
            this.m_SupportList.SetActive(0);
        Label_0197:
            if (data2 == null)
            {
                goto Label_024A;
            }
            if (flag == null)
            {
                goto Label_01A9;
            }
            data2.RefreshTabMask();
        Label_01A9:
            data.selectTab = this.GetCurrentTab();
            data2.calcData.Clear();
            data2.calcData.AddRange(data2.data);
            this.CalcUnit(data2.calcData);
            num = 0;
            goto Label_022C;
        Label_01E5:
            data3 = data2.calcData[num];
            if (data3 == null)
            {
                goto Label_0226;
            }
            param = new Content.ItemSource.ItemParam(this.m_Root, data3);
            if (param == null)
            {
                goto Label_0226;
            }
            if (param.IsValid() == null)
            {
                goto Label_0226;
            }
            source.Add(param);
        Label_0226:
            num += 1;
        Label_022C:
            if (num < data2.calcData.Count)
            {
                goto Label_01E5;
            }
            source.AnchorePos(data2.anchorePos);
        Label_024A:
            return source;
        }

        private Content.ItemSource SetupSupportList(Content.ItemSource source)
        {
            Tab[] tabArray1;
            bool flag;
            EElement element;
            ListData data;
            ListData data2;
            Tab tab;
            ContentNode node;
            int num;
            int num2;
            UnitListWindow.Data data3;
            Content.ItemSource.ItemParam param;
            flag = 0;
            element = this.GetElement(this.GetCurrentTab());
            data = this.GetSupportList(element);
            if (((data != null) && (data.isValid != null)) && (source != null))
            {
                goto Label_0038;
            }
            data = this.CreateSupportList(element);
            flag = 1;
        Label_0038:
            data2 = this.GetSupportList(0);
            if ((data2 != null) && (data2.isValid != null))
            {
                goto Label_0059;
            }
            data2 = this.CreateSupportList(0);
        Label_0059:
            this.RemoveData("data_supportres");
            if (source != null)
            {
                goto Label_0172;
            }
            source = new Content.ItemSource();
            tab = (data2 != null) ? data2.selectTab : 0x80;
            tabArray1 = new Tab[] { 0x80, 2, 4, 8, 0x10, 0x20, 0x40 };
            this.SetupTab(tabArray1, tab);
            if ((this.Param_SupportList != null) == null)
            {
                goto Label_0120;
            }
            node = this.m_ValueList.GetComponent<ContentNode>("node_support");
            if ((node != null) == null)
            {
                goto Label_0114;
            }
            this.m_ContentController = this.m_SupportController;
            this.m_ContentController.m_Node = node;
            this.m_ContentController.get_gameObject().SetActive(1);
        Label_0114:
            this.Param_SupportList.SetActive(1);
        Label_0120:
            this.m_ValueList.SetActive(1, 0);
            this.m_ValueList.SetActive(2, 0);
            this.m_ValueList.SetActive("btn_close", 1);
            this.m_ValueList.SetActive("btn_refresh", 1);
            this.m_ValueList.SetActive("btn_help_support", 1);
            flag = 1;
        Label_0172:
            if ((this.m_UnitList != null) == null)
            {
                goto Label_018F;
            }
            this.m_UnitList.SetActive(0);
        Label_018F:
            if ((this.m_PieceList != null) == null)
            {
                goto Label_01AC;
            }
            this.m_PieceList.SetActive(0);
        Label_01AC:
            if (data == null)
            {
                goto Label_02B5;
            }
            if (flag == null)
            {
                goto Label_01BE;
            }
            data.RefreshTabMask();
        Label_01BE:
            if (data2 == null)
            {
                goto Label_01D0;
            }
            data2.selectTab = this.GetCurrentTab();
        Label_01D0:
            data.selectTab = this.GetCurrentTab();
            data.calcData.Clear();
            data.calcData.AddRange(data.data);
            this.CalcUnit(data.calcData);
            num = 0;
            num2 = 0;
            goto Label_0257;
        Label_020F:
            data3 = data.calcData[num2];
            if (data3 == null)
            {
                goto Label_0251;
            }
            param = new Content.ItemSource.ItemParam(this.m_Root, data3);
            if ((param == null) || (param.IsValid() == null))
            {
                goto Label_0251;
            }
            num = source.Add(param);
        Label_0251:
            num2 += 1;
        Label_0257:
            if (num2 < data.calcData.Count)
            {
                goto Label_020F;
            }
            source.AnchorePos((data2 == null) ? Vector2.get_zero() : data2.anchorePos);
            if (num != null)
            {
                goto Label_02A3;
            }
            this.m_ValueList.SetActive("text_nosupport", 1);
            goto Label_02B5;
        Label_02A3:
            this.m_ValueList.SetActive("text_nosupport", 0);
        Label_02B5:
            return source;
        }

        private void SetupTab(Tab[] tabs, Tab start)
        {
            int num;
            TabMaker.Info[] infoArray;
            int num2;
            this.m_Tab = this.m_ValueList.GetComponent<TabMaker>("tab");
            if ((this.m_Tab != null) == null)
            {
                goto Label_00F8;
            }
            this.m_Tab.Create(this.GetTabKeys(tabs), new Action<GameObject, SerializeValueList>(this.SetupTabNode));
            if (this.m_EditType != 4)
            {
                goto Label_00E6;
            }
            num = this.GetData<int>("data_element", 0);
            if (num == null)
            {
                goto Label_00E6;
            }
            start = this.GetTab(num);
            infoArray = this.m_Tab.GetInfos();
            num2 = 0;
            goto Label_00DD;
        Label_0080:
            if ((infoArray[num2].ev != null) == null)
            {
                goto Label_00A1;
            }
            infoArray[num2].ev.ResetFlag(1);
        Label_00A1:
            if (infoArray[num2].element.value == start)
            {
                goto Label_00D0;
            }
            infoArray[num2].SetColor(new Color(0.5f, 0.5f, 0.5f));
        Label_00D0:
            infoArray[num2].interactable = 0;
            num2 += 1;
        Label_00DD:
            if (num2 < ((int) infoArray.Length))
            {
                goto Label_0080;
            }
        Label_00E6:
            this.m_Tab.SetOn((Tab) start, 1);
        Label_00F8:
            return;
        }

        private void SetupTabNode(GameObject gobj, SerializeValueList value)
        {
            TabMaker.Element element;
            object obj2;
            element = value.GetObject<TabMaker.Element>("element", null);
            if (element == null)
            {
                goto Label_003B;
            }
            obj2 = Enum.Parse(typeof(Tab), element.key);
            if (obj2 == null)
            {
                goto Label_003B;
            }
            element.value = (int) obj2;
        Label_003B:
            return;
        }

        private unsafe Content.ItemSource SetupUnitList(Content.ItemSource source)
        {
            Tab[] tabArray1;
            bool flag;
            TabRegister register;
            ListData data;
            Tab tab;
            ContentNode node;
            SerializeValueBehaviour behaviour;
            MyPhoton photon;
            QuestParam param;
            string str;
            int num;
            int num2;
            long num3;
            int num4;
            int num5;
            int num6;
            UnitListWindow.Data data2;
            Content.ItemSource.ItemParam param2;
            flag = 0;
            register = this.GetData<TabRegister>("data_register", null);
            this.m_EditType = this.GetData<UnitListWindow.EditType>("data_edit", 0);
            data = this.GetListData("unitlist");
            if (((data != null) && (data.isValid != null)) && (source != null))
            {
                goto Label_005E;
            }
            data = this.CreateUnitList(this.m_EditType, this.GetData<UnitData[]>("data_units"));
            flag = 1;
        Label_005E:
            if (source != null)
            {
                goto Label_0317;
            }
            source = new Content.ItemSource();
            tab = (data.selectTab != null) ? data.selectTab : 0xffff;
            if (register == null)
            {
                goto Label_0094;
            }
            tab = register.tab;
        Label_0094:
            tabArray1 = new Tab[] { 0xffff, 1, 2, 4, 8, 0x10, 0x20, 0x40 };
            this.SetupTab(tabArray1, tab);
            if ((this.Param_UnitList != null) == null)
            {
                goto Label_0193;
            }
            node = this.m_ValueList.GetComponent<ContentNode>((this.m_EditType != 2) ? "node_unit" : "node_tower");
            if ((node != null) == null)
            {
                goto Label_0187;
            }
            behaviour = node.GetComponent<SerializeValueBehaviour>();
            if ((behaviour != null) == null)
            {
                goto Label_015D;
            }
            behaviour.list.SetActive("empty", 0);
            behaviour.list.SetActive("body", 0);
            behaviour.list.SetActive("select", 0);
        Label_015D:
            this.m_ContentController = this.m_UnitController;
            this.m_ContentController.m_Node = node;
            this.m_ContentController.get_gameObject().SetActive(1);
        Label_0187:
            this.Param_UnitList.SetActive(1);
        Label_0193:
            if ((this.m_PieceList != null) == null)
            {
                goto Label_01B0;
            }
            this.m_PieceList.SetActive(0);
        Label_01B0:
            if ((this.m_SupportList != null) == null)
            {
                goto Label_01CD;
            }
            this.m_SupportList.SetActive(0);
        Label_01CD:
            this.m_ValueList.SetActive(1, 0);
            this.m_ValueList.SetActive(2, 0);
            this.m_ValueList.SetActive("btn_sort", 1);
            this.m_ValueList.SetActive("btn_filter", 1);
            if ((this.m_EditType != 1) && (this.m_EditType != 2))
            {
                goto Label_0276;
            }
            this.m_ValueList.SetActive("btn_close", 1);
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((((photon != null) != null) && (photon.IsConnectedInRoom() != null)) && (GlobalVars.SelectedMultiPlayRoomType == null))
            {
                goto Label_0315;
            }
            this.m_ValueList.SetActive("btn_attackable", 1);
            goto Label_0315;
        Label_0276:
            if (this.m_EditType != 3)
            {
                goto Label_0299;
            }
            this.m_ValueList.SetActive("btn_backhome", 1);
            goto Label_0315;
        Label_0299:
            if (this.m_EditType != 5)
            {
                goto Label_02BC;
            }
            this.m_ValueList.SetActive("btn_close", 1);
            goto Label_0315;
        Label_02BC:
            if (this.m_EditType != 4)
            {
                goto Label_02DF;
            }
            this.m_ValueList.SetActive("btn_close", 1);
            goto Label_0315;
        Label_02DF:
            this.m_ValueList.SetActive("btn_backhome", 1);
            this.m_ValueList.SetActive("btn_help", 1);
            this.m_ValueList.SetActive("btn_piece", 1);
        Label_0315:
            flag = 1;
        Label_0317:
            if (data == null)
            {
                goto Label_0602;
            }
            if (flag == null)
            {
                goto Label_0329;
            }
            data.Refresh();
        Label_0329:
            data.selectTab = this.GetCurrentTab();
            data.calcData.Clear();
            data.calcData.AddRange(data.data);
            this.CalcUnit(data.calcData);
            if (this.m_Root.filterWindow == null)
            {
                goto Label_0383;
            }
            this.m_Root.filterWindow.CalcUnit(data.calcData);
        Label_0383:
            if (this.m_Root.sortWindow == null)
            {
                goto Label_03A9;
            }
            this.m_Root.sortWindow.CalcUnit(data.calcData);
        Label_03A9:
            if (((this.m_EditType != 1) && (this.m_EditType != 2)) || (this.m_ValueList.GetUIOn("btn_attackable") == null))
            {
                goto Label_04C1;
            }
            param = this.GetData<QuestParam>("data_quest", null);
            if (param == null)
            {
                goto Label_04C1;
            }
            str = string.Empty;
            num = this.GetData<int>("data_party_index", -1);
            num2 = 0;
            goto Label_04AF;
        Label_0408:
            if (data.calcData[num2].unit == null)
            {
                goto Label_04A9;
            }
            if (param.IsEntryQuestCondition(data.calcData[num2].unit, &str) != null)
            {
                goto Label_0457;
            }
            data.calcData.RemoveAt(num2);
            num2 -= 1;
            goto Label_04A9;
        Label_0457:
            if (((param.type != 15) || (data.calcData[num2].partyIndex < 0)) || (data.calcData[num2].partyIndex == num))
            {
                goto Label_04A9;
            }
            data.calcData.RemoveAt(num2);
            num2 -= 1;
        Label_04A9:
            num2 += 1;
        Label_04AF:
            if (num2 < data.calcData.Count)
            {
                goto Label_0408;
            }
        Label_04C1:
            if (((this.m_EditType != 1) && (this.m_EditType != 2)) && (this.m_EditType != 4))
            {
                goto Label_050D;
            }
            if (<>f__am$cache18 != null)
            {
                goto Label_0503;
            }
            <>f__am$cache18 = new Comparison<UnitListWindow.Data>(UnitListRootWindow.<SetupUnitList>m__476);
        Label_0503:
            SortUtility.StableSort<UnitListWindow.Data>(data.calcData, <>f__am$cache18);
        Label_050D:
            num3 = (register == null) ? 0L : register.forcus;
            num4 = -1;
            num5 = 0;
            num6 = 0;
            goto Label_058A;
        Label_0530:
            data2 = data.calcData[num6];
            if (data2 == null)
            {
                goto Label_0584;
            }
            param2 = new Content.ItemSource.ItemParam(this.m_Root, data2);
            if (param2 == null)
            {
                goto Label_0584;
            }
            if (param2.IsValid() == null)
            {
                goto Label_0584;
            }
            if (num3 != data2.GetUniq())
            {
                goto Label_057A;
            }
            num4 = num5;
        Label_057A:
            num5 = source.Add(param2);
        Label_0584:
            num6 += 1;
        Label_058A:
            if (num6 < data.calcData.Count)
            {
                goto Label_0530;
            }
            if (register == null)
            {
                goto Label_05BB;
            }
            source.AnchorePos(register.anchorePos);
            source.ForcusIndex(num4);
            goto Label_05C7;
        Label_05BB:
            source.AnchorePos(data.anchorePos);
        Label_05C7:
            if (num5 != null)
            {
                goto Label_05E5;
            }
            this.m_ValueList.SetActive("text_nounit", 1);
            goto Label_05F7;
        Label_05E5:
            this.m_ValueList.SetActive("text_nounit", 0);
        Label_05F7:
            this.RemoveData("data_register");
        Label_0602:
            return source;
        }

        private void ShowToolTip(string path, UnitData unit)
        {
            GameObject obj2;
            GameObject obj3;
            UnitJobDropdown dropdown;
            Selectable selectable;
            Image image;
            ArtifactSlots slots;
            AbilitySlots slots2;
            ConceptCardSlots slots3;
            if (string.IsNullOrEmpty(path) != null)
            {
                goto Label_0011;
            }
            if (unit != null)
            {
                goto Label_0012;
            }
        Label_0011:
            return;
        Label_0012:
            obj3 = Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.m_Param.tooltipPrefab));
            DataSource.Bind<UnitData>(obj3, unit);
            dropdown = obj3.GetComponentInChildren<UnitJobDropdown>();
            if ((dropdown != null) == null)
            {
                goto Label_00A4;
            }
            dropdown.get_gameObject().SetActive(1);
            selectable = dropdown.get_gameObject().GetComponent<Selectable>();
            if ((selectable != null) == null)
            {
                goto Label_006F;
            }
            selectable.set_interactable(0);
        Label_006F:
            image = dropdown.get_gameObject().GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_00A4;
            }
            image.set_color(new Color(0.5f, 0.5f, 0.5f));
        Label_00A4:
            slots = obj3.GetComponentInChildren<ArtifactSlots>();
            slots2 = obj3.GetComponentInChildren<AbilitySlots>();
            if ((slots != null) == null)
            {
                goto Label_00DE;
            }
            if ((slots2 != null) == null)
            {
                goto Label_00DE;
            }
            slots.Refresh(0);
            slots2.Refresh(0);
        Label_00DE:
            slots3 = obj3.GetComponentInChildren<ConceptCardSlots>();
            if ((slots3 != null) == null)
            {
                goto Label_00FB;
            }
            slots3.Refresh(0);
        Label_00FB:
            GameParameter.UpdateAll(obj3);
            return;
        }

        public override int Update()
        {
            ContentType type;
            base.Update();
            if (base.isClosed == null)
            {
                goto Label_0019;
            }
            base.SetActiveChild(0);
        Label_0019:
            if (this.m_ContentType == 3)
            {
                goto Label_002C;
            }
            goto Label_0037;
        Label_002C:
            this.Update_Support();
        Label_0037:
            return -1;
        }

        private void Update_Support()
        {
            float num;
            if (this.m_SupportRefreshLock <= 0f)
            {
                goto Label_005C;
            }
            num = Time.get_realtimeSinceStartup() - this.m_SupportRefreshLock;
            if (num <= 10f)
            {
                goto Label_004A;
            }
            this.m_SupportRefreshLock = 0f;
            this.m_ValueList.SetInteractable("btn_refresh", 1);
            goto Label_005C;
        Label_004A:
            this.m_ValueList.SetInteractable("btn_refresh", 0);
        Label_005C:
            return;
        }

        public override string name
        {
            get
            {
                return "UnitListRootWindow";
            }
        }

        private GameObject Param_UnitList
        {
            get
            {
                GameObject obj2;
                ContentNode[] nodeArray;
                int num;
                bool flag;
                string str;
                if ((this.m_UnitList == null) == null)
                {
                    goto Label_00EC;
                }
                if (((this.m_Param != null) && (this.m_Param.unitList != null)) && ((this.m_Param.listParent == null) == null))
                {
                    goto Label_0044;
                }
                return null;
            Label_0044:
                obj2 = AssetManager.Load<GameObject>(this.m_Param.unitList);
                this.m_UnitList = Object.Instantiate<GameObject>(obj2);
                this.m_UnitList.get_transform().SetParent(this.m_Param.listParent.get_transform(), 0);
                this.InitializeUnitList();
                nodeArray = this.m_UnitList.GetComponentsInChildren<ContentNode>(1);
                num = 0;
                goto Label_00E3;
            Label_009C:
                str = ((nodeArray[num].get_name().IndexOf("tower", 5) > 0) == null) ? "node_unit" : "node_tower";
                this.m_ValueList.SetField(str, nodeArray[num].get_gameObject());
                num += 1;
            Label_00E3:
                if (num < ((int) nodeArray.Length))
                {
                    goto Label_009C;
                }
            Label_00EC:
                return this.m_UnitList;
            }
        }

        private GameObject Param_PieceList
        {
            get
            {
                GameObject obj2;
                ContentNode node;
                if ((this.m_PieceList == null) == null)
                {
                    goto Label_00AB;
                }
                if (this.m_Param == null)
                {
                    goto Label_0042;
                }
                if (this.m_Param.pieceList == null)
                {
                    goto Label_0042;
                }
                if ((this.m_Param.listParent == null) == null)
                {
                    goto Label_0044;
                }
            Label_0042:
                return null;
            Label_0044:
                obj2 = AssetManager.Load<GameObject>(this.m_Param.pieceList);
                this.m_PieceList = Object.Instantiate<GameObject>(obj2);
                this.m_PieceList.get_transform().SetParent(this.m_Param.listParent.get_transform(), 0);
                this.InitializePieceList();
                node = this.m_PieceList.GetComponentInChildren<ContentNode>(1);
                this.m_ValueList.SetField("node_piece", node.get_gameObject());
            Label_00AB:
                return this.m_PieceList;
            }
        }

        private GameObject Param_SupportList
        {
            get
            {
                GameObject obj2;
                ContentNode node;
                if ((this.m_SupportList == null) == null)
                {
                    goto Label_00AB;
                }
                if (this.m_Param == null)
                {
                    goto Label_0042;
                }
                if (this.m_Param.supportList == null)
                {
                    goto Label_0042;
                }
                if ((this.m_Param.listParent == null) == null)
                {
                    goto Label_0044;
                }
            Label_0042:
                return null;
            Label_0044:
                obj2 = AssetManager.Load<GameObject>(this.m_Param.supportList);
                this.m_SupportList = Object.Instantiate<GameObject>(obj2);
                this.m_SupportList.get_transform().SetParent(this.m_Param.listParent.get_transform(), 0);
                this.InitializeSupportList();
                node = this.m_SupportList.GetComponentInChildren<ContentNode>(1);
                this.m_ValueList.SetField("node_support", node.get_gameObject());
            Label_00AB:
                return this.m_SupportList;
            }
        }

        public static UnitListRootWindow instance
        {
            get
            {
                return m_Instance;
            }
        }

        public static bool hasInstance
        {
            get
            {
                if (m_Instance == null)
                {
                    goto Label_0021;
                }
                if (m_Instance.isValid == null)
                {
                    goto Label_001B;
                }
                return 1;
            Label_001B:
                m_Instance = null;
            Label_0021:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <CreatePieceList>c__AnonStorey3D3
        {
            internal ItemParam item;
            internal UnitParam focus;

            public <CreatePieceList>c__AnonStorey3D3()
            {
                base..ctor();
                return;
            }

            internal bool <>m__471(UnitParam p)
            {
                return (p.piece == this.item.iname);
            }

            internal bool <>m__472(UnitListWindow.Data prop)
            {
                return (prop.param == this.focus);
            }
        }

        [CompilerGenerated]
        private sealed class <CreateUnitList>c__AnonStorey3D4
        {
            internal UnitData unit;

            public <CreateUnitList>c__AnonStorey3D4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__475(UnitListWindow.Data prop)
            {
                return ((prop.unit == null) ? 0 : (prop.unit.UnitID == this.unit.UnitID));
            }
        }

        public static class Content
        {
            public static ItemAccessor clickItem;

            static Content()
            {
            }

            public class ItemAccessor
            {
                private UnitListRootWindow m_RootWindow;
                private UnitListSortWindow m_SortWindow;
                private UnitListRootWindow.ContentType m_ContentType;
                private ContentNode m_Node;
                private UnitListWindow.Data m_Param;
                private DataSource m_DataSource;
                private SerializeValueBehaviour m_Value;
                private SortBadge m_SortBadge;
                private RectTransform m_SlotLabel;

                public ItemAccessor()
                {
                    base..ctor();
                    return;
                }

                public void Bind(ContentNode node)
                {
                    UnitListWindow.EditType type;
                    CanvasGroup group;
                    ImageArray array;
                    int num;
                    Image image;
                    List<PartyData> list;
                    bool flag;
                    int num2;
                    ImageArray array2;
                    UnitListSortWindow.SelectType type2;
                    this.m_Node = node;
                    this.m_DataSource = DataSource.Create(node.get_gameObject());
                    this.m_DataSource.Add(typeof(UnitParam), this.m_Param.param);
                    if (this.m_Param.unit == null)
                    {
                        goto Label_0068;
                    }
                    this.m_DataSource.Add(typeof(UnitData), this.m_Param.unit);
                Label_0068:
                    if (this.m_Param.support == null)
                    {
                        goto Label_0098;
                    }
                    this.m_DataSource.Add(typeof(SupportData), this.m_Param.support);
                Label_0098:
                    this.m_Value = this.m_Node.GetComponent<SerializeValueBehaviour>();
                    if ((this.m_Value != null) == null)
                    {
                        goto Label_058A;
                    }
                    this.m_Value.list.SetActive(1, 0);
                    if (string.IsNullOrEmpty(this.m_Param.body) != null)
                    {
                        goto Label_0103;
                    }
                    this.m_Value.list.SetActive(this.m_Param.body, 1);
                    goto Label_011A;
                Label_0103:
                    this.m_Value.list.SetActive("body", 1);
                Label_011A:
                    this.m_Value.list.SetActive("select", this.m_Param.partySelect);
                    if ((this.m_ContentType != 1) && (this.m_ContentType != 1))
                    {
                        goto Label_0481;
                    }
                    if (this.m_RootWindow == null)
                    {
                        goto Label_056F;
                    }
                    type = this.m_RootWindow.GetEditType();
                    if ((type != 1) && (type != 2))
                    {
                        goto Label_0325;
                    }
                    this.m_SlotLabel = this.m_RootWindow.AttachSlotLabel(this.m_Param, this.m_Node);
                    this.m_Value.list.SetActive("use", 0);
                    this.m_Value.list.SetActive("badge", 0);
                    if ((type == 3) || (type == 5))
                    {
                        goto Label_056F;
                    }
                    group = this.m_Node.get_gameObject().GetComponent<CanvasGroup>();
                    if ((group != null) == null)
                    {
                        goto Label_02BB;
                    }
                    group.set_alpha((this.m_Param.interactable == null) ? 0.5f : 1f);
                    group.set_interactable(this.m_Param.interactable);
                    array = this.m_Value.list.GetComponent<ImageArray>("team");
                    if ((array != null) == null)
                    {
                        goto Label_02BB;
                    }
                    num = this.m_RootWindow.GetData<int>("data_party_index", -1);
                    array.get_gameObject().SetActive(0);
                    if (((this.m_Param.partyIndex < 0) || (this.m_Param.partyIndex >= ((int) array.Images.Length))) || (num == this.m_Param.partyIndex))
                    {
                        goto Label_02BB;
                    }
                    array.ImageIndex = this.m_Param.partyIndex;
                    array.get_gameObject().SetActive(1);
                Label_02BB:
                    if ((this.m_SlotLabel != null) == null)
                    {
                        goto Label_056F;
                    }
                    image = this.m_SlotLabel.GetComponentInChildren<Image>();
                    if ((image != null) == null)
                    {
                        goto Label_056F;
                    }
                    image.set_color(new Color(1f, 1f, 1f, (this.m_Param.interactable == null) ? 0.5f : 1f));
                    goto Label_047C;
                Label_0325:
                    if (type != 4)
                    {
                        goto Label_035F;
                    }
                    this.m_Value.list.SetActive("use", 0);
                    this.m_Value.list.SetActive("badge", 0);
                    goto Label_047C;
                Label_035F:
                    if (type == 3)
                    {
                        goto Label_0373;
                    }
                    if (type == 5)
                    {
                        goto Label_0373;
                    }
                    if (type != null)
                    {
                        goto Label_044E;
                    }
                Label_0373:
                    list = MonoSingleton<GameManager>.Instance.Player.Partys;
                    flag = 0;
                    num2 = 0;
                    goto Label_03BB;
                Label_038F:
                    if (list[num2].IsPartyUnit(this.m_Param.GetUniq()) == null)
                    {
                        goto Label_03B5;
                    }
                    flag = 1;
                    goto Label_03C9;
                Label_03B5:
                    num2 += 1;
                Label_03BB:
                    if (num2 < list.Count)
                    {
                        goto Label_038F;
                    }
                Label_03C9:
                    this.m_Value.list.SetActive("use", flag);
                    if (type == 3)
                    {
                        goto Label_03EF;
                    }
                    if (type != 5)
                    {
                        goto Label_0432;
                    }
                Label_03EF:
                    this.m_Value.list.SetActive("badge", 0);
                    if (this.m_Param.interactable != null)
                    {
                        goto Label_056F;
                    }
                    this.m_Value.list.SetActive("noequip", 1);
                    goto Label_0449;
                Label_0432:
                    this.m_Value.list.SetActive("badge", 1);
                Label_0449:
                    goto Label_047C;
                Label_044E:
                    this.m_Value.list.SetActive("use", 0);
                    this.m_Value.list.SetActive("badge", 0);
                Label_047C:
                    goto Label_056F;
                Label_0481:
                    if (this.m_ContentType != 3)
                    {
                        goto Label_056F;
                    }
                    this.m_Value.list.SetActive("use", this.m_Param.partySelect);
                    if (this.m_Param.support == null)
                    {
                        goto Label_04EA;
                    }
                    if (this.m_Param.support.IsFriend() == null)
                    {
                        goto Label_04EA;
                    }
                    this.m_Value.list.SetActive("friend", 1);
                Label_04EA:
                    this.m_Value.list.SetActive("locked", this.m_Param.interactable == 0);
                    array2 = this.m_Value.list.GetComponent<ImageArray>("team");
                    if ((array2 != null) == null)
                    {
                        goto Label_056F;
                    }
                    array2.get_gameObject().SetActive(0);
                    if (this.m_Param.partyIndex < 0)
                    {
                        goto Label_056F;
                    }
                    array2.ImageIndex = this.m_Param.partyIndex;
                    array2.get_gameObject().SetActive(1);
                Label_056F:
                    this.m_SortBadge = this.m_Value.list.GetComponent<SortBadge>("sort");
                Label_058A:
                    if ((this.m_SortBadge != null) == null)
                    {
                        goto Label_05D5;
                    }
                    if (this.m_SortWindow == null)
                    {
                        goto Label_05CD;
                    }
                    type2 = this.m_SortWindow.GetSection();
                    this.SetSortValue(type2, UnitListSortWindow.GetSortStatus(this.m_Param, type2));
                    goto Label_05D5;
                Label_05CD:
                    this.SetSortValue(1, 0);
                Label_05D5:
                    return;
                }

                public void Clear()
                {
                    if ((this.m_DataSource != null) == null)
                    {
                        goto Label_0023;
                    }
                    this.m_DataSource.Clear();
                    this.m_DataSource = null;
                Label_0023:
                    this.m_SortBadge = null;
                    if ((this.m_SlotLabel != null) == null)
                    {
                        goto Label_005E;
                    }
                    if (this.m_RootWindow == null)
                    {
                        goto Label_0057;
                    }
                    this.m_RootWindow.DettachSlotLabel(this.m_SlotLabel);
                Label_0057:
                    this.m_SlotLabel = null;
                Label_005E:
                    return;
                }

                public void ForceUpdate()
                {
                    if ((this.m_Node != null) == null)
                    {
                        goto Label_0021;
                    }
                    GameParameter.UpdateAll(this.m_Node.get_gameObject());
                Label_0021:
                    return;
                }

                public void LateUpdate()
                {
                    if ((this.m_SlotLabel != null) == null)
                    {
                        goto Label_0027;
                    }
                    this.m_SlotLabel.set_anchoredPosition(this.m_Node.GetWorldPos());
                Label_0027:
                    return;
                }

                public void Release()
                {
                    this.Clear();
                    this.m_Node = null;
                    this.m_Param = null;
                    return;
                }

                public unsafe void SetSortValue(UnitListSortWindow.SelectType section, int value)
                {
                    UnitListSortWindow.SelectType type;
                    if ((this.m_SortBadge != null) == null)
                    {
                        goto Label_00C9;
                    }
                    type = 7;
                    if ((section & type) != null)
                    {
                        goto Label_00A1;
                    }
                    if ((this.m_SortBadge.Value != null) == null)
                    {
                        goto Label_0048;
                    }
                    this.m_SortBadge.Value.set_text(&value.ToString());
                Label_0048:
                    if ((this.m_SortBadge.Icon != null) == null)
                    {
                        goto Label_0074;
                    }
                    this.m_SortBadge.Icon.set_sprite(UnitListSortWindow.GetIcon(section));
                Label_0074:
                    this.m_SortBadge.get_gameObject().SetActive(1);
                    this.m_Value.list.SetActive("lv", 0);
                    goto Label_00C9;
                Label_00A1:
                    this.m_SortBadge.get_gameObject().SetActive(0);
                    this.m_Value.list.SetActive("lv", 1);
                Label_00C9:
                    return;
                }

                public void Setup(UnitListWindow window, UnitListWindow.Data param)
                {
                    this.m_RootWindow = window.rootWindow;
                    this.m_SortWindow = window.sortWindow;
                    this.m_ContentType = this.m_RootWindow.GetContentType();
                    this.m_Param = param;
                    return;
                }

                public ContentNode node
                {
                    get
                    {
                        return this.m_Node;
                    }
                }

                public UnitListWindow.Data param
                {
                    get
                    {
                        return this.m_Param;
                    }
                }

                public bool isValid
                {
                    get
                    {
                        return ((this.m_Param == null) == 0);
                    }
                }
            }

            public class ItemSource : ContentSource
            {
                private List<ItemParam> m_Params;
                private Vector2 m_AnchorePos;
                private int m_Forcus;

                public ItemSource()
                {
                    this.m_Params = new List<ItemParam>();
                    this.m_AnchorePos = Vector2.get_zero();
                    this.m_Forcus = -1;
                    base..ctor();
                    return;
                }

                public int Add(ItemParam param)
                {
                    this.m_Params.Add(param);
                    return this.m_Params.Count;
                }

                public void AnchorePos(Vector2 pos)
                {
                    this.m_AnchorePos = pos;
                    return;
                }

                public void ForcusIndex(int index)
                {
                    this.m_Forcus = index;
                    return;
                }

                public override void Initialize(ContentController controller)
                {
                    base.Initialize(controller);
                    this.Setup();
                    return;
                }

                public override void Release()
                {
                    this.m_Params.Clear();
                    base.Release();
                    return;
                }

                public unsafe void Setup()
                {
                    ContentSource.Param param;
                    ContentGrid grid;
                    Vector2 vector;
                    bool flag;
                    Vector2 vector2;
                    Vector2 vector3;
                    Vector2 vector4;
                    Vector2 vector5;
                    this.Clear();
                    base.SetTable(this.m_Params.ToArray());
                    base.contentController.Resize(0);
                    if (this.m_Forcus == -1)
                    {
                        goto Label_00D8;
                    }
                    param = this.GetParam(this.m_Forcus);
                    if (param == null)
                    {
                        goto Label_00C3;
                    }
                    grid = base.contentController.GetGrid(param.id);
                    vector = base.contentController.GetAnchorePosFromGrid(&grid.x, &grid.y);
                    &vector.x += &base.contentController.GetSpacing().x;
                    &vector.y -= &base.contentController.GetSpacing().y;
                    base.contentController.anchoredPosition = vector;
                    goto Label_00D3;
                Label_00C3:
                    base.contentController.anchoredPosition = Vector2.get_zero();
                Label_00D3:
                    goto Label_00E9;
                Label_00D8:
                    base.contentController.anchoredPosition = this.m_AnchorePos;
                Label_00E9:
                    flag = 0;
                    vector2 = base.contentController.anchoredPosition;
                    vector3 = base.contentController.GetLastPageAnchorePos();
                    if (&vector2.x >= &vector3.x)
                    {
                        goto Label_0128;
                    }
                    flag = 1;
                    &vector2.x = &vector3.x;
                Label_0128:
                    if (&vector2.y >= &vector3.y)
                    {
                        goto Label_014B;
                    }
                    flag = 1;
                    &vector2.y = &vector3.y;
                Label_014B:
                    if (flag == null)
                    {
                        goto Label_015E;
                    }
                    base.contentController.anchoredPosition = vector2;
                Label_015E:
                    if ((base.contentController.scroller != null) == null)
                    {
                        goto Label_0184;
                    }
                    base.contentController.scroller.StopMovement();
                Label_0184:
                    this.m_AnchorePos = Vector2.get_zero();
                    this.m_Forcus = -1;
                    return;
                }

                public class ItemParam : ContentSource.Param
                {
                    private UnitListRootWindow.Content.ItemAccessor m_Accessor;

                    public ItemParam(UnitListWindow window, UnitListWindow.Data param)
                    {
                        this.m_Accessor = new UnitListRootWindow.Content.ItemAccessor();
                        base..ctor();
                        this.m_Accessor.Setup(window, param);
                        return;
                    }

                    public override bool IsValid()
                    {
                        return this.m_Accessor.isValid;
                    }

                    public override void LateUpdate()
                    {
                        this.m_Accessor.LateUpdate();
                        return;
                    }

                    public override void OnClick(ContentNode node)
                    {
                    }

                    public override void OnDisable(ContentNode node)
                    {
                        this.m_Accessor.Clear();
                        return;
                    }

                    public override void OnEnable(ContentNode node)
                    {
                        this.m_Accessor.Bind(node);
                        this.m_Accessor.ForceUpdate();
                        return;
                    }

                    public override void Release()
                    {
                        this.m_Accessor.Release();
                        return;
                    }

                    public UnitListRootWindow.Content.ItemAccessor accerror
                    {
                        get
                        {
                            return this.m_Accessor;
                        }
                    }
                }
            }
        }

        public enum ContentType
        {
            NONE,
            UNIT,
            PIECE,
            SUPPORT
        }

        protected class Json_ReqSupporterResponse
        {
            public Json_Support[] supports;

            public Json_ReqSupporterResponse()
            {
                base..ctor();
                return;
            }
        }

        public class ListData
        {
            public bool isValid;
            public string key;
            public object response;
            public List<UnitListWindow.Data> data;
            public List<UnitListWindow.Data> calcData;
            public Vector2 anchorePos;
            public UnitListRootWindow.Tab selectTab;
            public long selectUniqueID;

            public ListData()
            {
                this.key = string.Empty;
                this.data = new List<UnitListWindow.Data>();
                this.calcData = new List<UnitListWindow.Data>();
                this.anchorePos = Vector2.get_zero();
                base..ctor();
                return;
            }

            public void Delete()
            {
                this.isValid = 0;
                this.data.Clear();
                this.calcData.Clear();
                return;
            }

            public List<long> GetUniqs()
            {
                List<long> list;
                int num;
                long num2;
                list = new List<long>();
                num = 0;
                goto Label_0032;
            Label_000D:
                num2 = this.calcData[num].GetUniq();
                if (num2 <= 0L)
                {
                    goto Label_002E;
                }
                list.Add(num2);
            Label_002E:
                num += 1;
            Label_0032:
                if (num < this.calcData.Count)
                {
                    goto Label_000D;
                }
                return list;
            }

            public void Refresh()
            {
                int num;
                num = 0;
                goto Label_001C;
            Label_0007:
                this.data[num].Refresh();
                num += 1;
            Label_001C:
                if (num < this.data.Count)
                {
                    goto Label_0007;
                }
                return;
            }

            public void RefreshTabMask()
            {
                int num;
                num = 0;
                goto Label_001C;
            Label_0007:
                this.data[num].RefreshTabMask();
                num += 1;
            Label_001C:
                if (num < this.data.Count)
                {
                    goto Label_0007;
                }
                return;
            }
        }

        [Serializable]
        public class SerializeParam : FlowWindowBase.SerializeParamBase
        {
            public string unitList;
            public string pieceList;
            public string supportList;
            public GameObject listParent;
            public string tooltipPrefab;

            public SerializeParam()
            {
                base..ctor();
                return;
            }

            public override Type type
            {
                get
                {
                    return typeof(UnitListRootWindow);
                }
            }
        }

        public enum Tab
        {
            NONE = 0,
            ALL = 0xffff,
            FAVORITE = 1,
            FIRE = 2,
            WATER = 4,
            THUNDER = 8,
            WIND = 0x10,
            LIGHT = 0x20,
            DARK = 0x40,
            MAINSUPPORT = 0x80
        }

        public class TabRegister
        {
            public UnitListRootWindow.Tab tab;
            public Vector2 anchorePos;
            public long forcus;

            public TabRegister()
            {
                base..ctor();
                return;
            }
        }
    }
}

