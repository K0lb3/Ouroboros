namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(0x2b3, "フィルタウィンドキャンセルした", 1, 0x2b3), NodeType("UI/UnitListWindow", 0x7fe5), Pin(100, "所持ユニットウィンド開く", 0, 100), Pin(0x65, "編成ユニットウィンド開く", 0, 0x65), Pin(0x66, "タワー編成ユニットウィンド開く", 0, 0x66), Pin(0x67, "装備強化ユニットウィンド開く", 0, 0x67), Pin(0x68, "サポート傭兵設定ユニットウィンド開く", 0, 0x68), Pin(0x69, "ショップ装備強化ユニットウィンド開く", 0, 0x69), Pin(110, "ユニット欠片ウィンド開く", 0, 110), Pin(300, "傭兵雇用ウィンド開く", 0, 300), Pin(190, "ウィンド開く", 1, 0x18e), Pin(0xbf, "ウィンド開いた", 1, 0x18f), Pin(400, "リストタブ切り替え", 0, 400), Pin(410, "リスト更新", 0, 410), Pin(430, "リスト再生成", 0, 0x19b), Pin(420, "アイテム選択", 0, 0x1a3), Pin(0x1a3, "アイテムホールド", 0, 420), Pin(0x3f2, "ウィンドロック解除", 0, 0x3f2), Pin(0x1a5, "ユニット選択", 1, 0x1a6), Pin(0x1a6, "ユニット解除", 1, 0x1a7), Pin(0x1a7, "欠片選択", 1, 0x1a7), Pin(0x1aa, "傭兵雇用選択", 1, 0x1aa), Pin(0x1ab, "傭兵雇用解除", 1, 0x1ab), Pin(0x1ad, "アイテムホールド", 1, 0x1ad), Pin(440, "サポートリスト更新", 0, 440), Pin(480, "サポートリスト取得", 1, 480), Pin(0x1e1, "サポートリスト強制取得", 1, 0x1e1), Pin(490, "ウィンド閉じる", 0, 490), Pin(0x1eb, "ウィンド閉じる", 1, 0x1eb), Pin(0x1ec, "ウィンド閉じた", 1, 0x1ec), Pin(500, "ソートウィンド開く", 0, 500), Pin(510, "ソートウィンド閉じる", 0, 510), Pin(520, "ソートウィンド確定", 0, 520), Pin(0x3e8, "ウィンドロック", 0, 0x3e8), Pin(530, "ソートウィンドトグル切り替え", 0, 530), Pin(590, "ソートウィンド確定した", 1, 590), Pin(0x24f, "ソートウィンドキャンセルした", 1, 0x24f), Pin(600, "フィルタウィンド開く", 0, 600), Pin(610, "フィルタウィンド閉じる", 0, 610), Pin(620, "フィルタウィンド確定", 0, 620), Pin(630, "フィルタウィンドトグル切り替え", 0, 630), Pin(640, "フィルタウィンドトグル全て選択", 0, 640), Pin(650, "フィルタウィンドトグル全てクリア", 0, 650), Pin(690, "フィルタウィンド確定した", 1, 690)]
    public class UnitListWindow : FlowNodePersistent
    {
        public const int INPUT_UNITWINDOW_OPEN = 100;
        public const int INPUT_UNITWINDOW_EDIT_OPEN = 0x65;
        public const int INPUT_UNITWINDOW_TWEDIT_OPEN = 0x66;
        public const int INPUT_UNITWINDOW_EQUIP_OPEN = 0x67;
        public const int INPUT_UNITWINDOW_SUPPORT_OPEN = 0x68;
        public const int INPUT_UNITWINDOW_SHOPEQUIP_OPEN = 0x69;
        public const int INPUT_PIECEWINDOW_OPEN = 110;
        public const int INPUT_SUPPORTWINDOW_OPEN = 300;
        public const int OUTPUT_WINDOW_OPEN = 190;
        public const int OUTPUT_WINDOW_OPENED = 0xbf;
        public const int INPUT_LIST_TABCHANGE = 400;
        public const int INPUT_LIST_REFRESH = 410;
        public const int INPUT_LIST_HOLD = 0x1a3;
        public const int INPUT_LIST_SELECT = 420;
        public const int INPUT_LIST_REMAKE = 430;
        public const int INPUT_SUPPORTLIST_REFRESH = 440;
        public const int OUTPUT_SELECT_UNIT = 0x1a5;
        public const int OUTPUT_REMOVE_UNIT = 0x1a6;
        public const int OUTPUT_SELECT_PIECE = 0x1a7;
        public const int OUTPUT_SELECT_SUPPORT = 0x1aa;
        public const int OUTPUT_REMOVE_SUPPORT = 0x1ab;
        public const int OUTPUT_HOLD_ITEM = 0x1ad;
        public const int OUTPUT_WEBAPI_SUPPORT_UPDATE = 480;
        public const int OUTPUT_WEBAPI_SUPPORT_UPDATEFORCE = 0x1e1;
        public const int INPUT_WINDOW_CLOSE = 490;
        public const int OUTPUT_WINDOW_CLOSE = 0x1eb;
        public const int OUTPUT_WINDOW_CLOSED = 0x1ec;
        public const int INPUT_SORTWINDOW_OPEN = 500;
        public const int INPUT_SORTWINDOW_CLOSE = 510;
        public const int INPUT_SORTWINDOW_OK = 520;
        public const int INPUT_SORTWINDOW_TGLCHANGE = 530;
        public const int OUTPUT_SORTWINDOW_CONFIRMED = 590;
        public const int OUTPUT_SORTWINDOW_CANCELED = 0x24f;
        public const int OUTPUT_SORTWINDOW_CLOSED = 0x257;
        public const int INPUT_FILTERWINDOW_OPEN = 600;
        public const int INPUT_FILTERWINDOW_CLOSE = 610;
        public const int INPUT_FILTERWWINDOW_OK = 620;
        public const int INPUT_FILTERWWINDOW_TGLCHANGE = 630;
        public const int INPUT_FILTERWWINDOW_ALL = 640;
        public const int INPUT_FILTERWWINDOW_CLEAR = 650;
        public const int OUTPUT_FILTERWINDOW_CONFIRMED = 690;
        public const int OUTPUT_FILTERWINDOW_CANCELED = 0x2b3;
        public const int OUTPUT_FILTERWINDOW_CLOSED = 0x2bb;
        public const int INPUT_LOCK = 0x3e8;
        public const int INPUT_UNLOCK = 0x3f2;
        public const string DATA_REGISTER = "data_register";
        public const string DATA_EDIT = "data_edit";
        public const string DATA_UNIT = "data_unit";
        public const string DATA_UNITS = "data_units";
        public const string DATA_PARTY = "data_party";
        public const string DATA_PARTY_INDEX = "data_party_index";
        public const string DATA_QUEST = "data_quest";
        public const string DATA_ELEMENT = "data_element";
        public const string DATA_SELECTSLOT = "data_slot";
        public const string DATA_HEROONLY = "data_heroOnly";
        public const string DATA_SUPPORT = "data_support";
        public const string DATA_SUPPORT_RESPONSE = "data_supportres";
        public UnitListRootWindow.SerializeParam m_RootWindowParam;
        public UnitListSortWindow.SerializeParam m_SortWindowParam;
        public UnitListFilterWindow.SerializeParam m_FilterWindowParam;
        private FlowWindowController m_WindowController;
        private UnitListRootWindow m_RootWindow;
        private UnitListSortWindow m_SortWindow;
        private UnitListFilterWindow m_FilterWindow;

        public UnitListWindow()
        {
            this.m_WindowController = new FlowWindowController();
            base..ctor();
            return;
        }

        public void AddData(string key, object value)
        {
            if (this.m_RootWindow == null)
            {
                goto Label_0018;
            }
            this.m_RootWindow.AddData(key, value);
        Label_0018:
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            this.m_WindowController.Initialize(this);
            this.m_WindowController.Add(this.m_RootWindowParam);
            this.m_WindowController.Add(this.m_SortWindowParam);
            this.m_WindowController.Add(this.m_FilterWindowParam);
            this.m_RootWindow = this.m_WindowController.GetWindow<UnitListRootWindow>();
            this.m_SortWindow = this.m_WindowController.GetWindow<UnitListSortWindow>();
            this.m_FilterWindow = this.m_WindowController.GetWindow<UnitListFilterWindow>();
            if (this.m_RootWindow == null)
            {
                goto Label_008F;
            }
            this.m_RootWindow.SetRoot(this);
        Label_008F:
            if (this.m_SortWindow == null)
            {
                goto Label_00A6;
            }
            this.m_SortWindow.SetRoot(this);
        Label_00A6:
            if (this.m_FilterWindow == null)
            {
                goto Label_00BD;
            }
            this.m_FilterWindow.SetRoot(this);
        Label_00BD:
            return;
        }

        public void ClearData()
        {
            if (this.m_RootWindow == null)
            {
                goto Label_0016;
            }
            this.m_RootWindow.ClearData();
        Label_0016:
            return;
        }

        public void Enabled(bool value)
        {
            if (this.m_WindowController == null)
            {
                goto Label_0017;
            }
            this.m_WindowController.enabled = value;
        Label_0017:
            return;
        }

        public object GetData(string key)
        {
            if (this.m_RootWindow == null)
            {
                goto Label_0018;
            }
            return this.m_RootWindow.GetData(key);
        Label_0018:
            return null;
        }

        public T GetData<T>(string key)
        {
            T local;
            if (this.m_RootWindow == null)
            {
                goto Label_0018;
            }
            return this.m_RootWindow.GetData<T>(key);
        Label_0018:
            return default(T);
        }

        public T GetData<T>(string key, T defaultValue)
        {
            T local;
            if (this.m_RootWindow == null)
            {
                goto Label_0019;
            }
            return this.m_RootWindow.GetData<T>(key, defaultValue);
        Label_0019:
            return default(T);
        }

        public bool IsEnabled()
        {
            if (this.m_WindowController == null)
            {
                goto Label_0017;
            }
            return this.m_WindowController.enabled;
        Label_0017:
            return 0;
        }

        public unsafe bool IsState(string stateName)
        {
            Animator animator;
            AnimatorStateInfo info;
            animator = base.GetComponentInChildren<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0024;
            }
            return &animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        Label_0024:
            return 0;
        }

        public override void OnActivate(int pinID)
        {
            if (this.m_WindowController == null)
            {
                goto Label_0045;
            }
            if (pinID != 0x3e8)
            {
                goto Label_0022;
            }
            this.Enabled(0);
            goto Label_0045;
        Label_0022:
            if (pinID != 0x3f2)
            {
                goto Label_0039;
            }
            this.Enabled(1);
            goto Label_0045;
        Label_0039:
            this.m_WindowController.OnActivate(pinID);
        Label_0045:
            return;
        }

        protected override void OnDestroy()
        {
            this.m_RootWindow = null;
            this.m_WindowController.Release();
            base.OnDestroy();
            return;
        }

        private void Start()
        {
        }

        private void Update()
        {
            this.m_WindowController.Update();
            return;
        }

        public UnitListRootWindow rootWindow
        {
            get
            {
                return this.m_RootWindow;
            }
        }

        public UnitListSortWindow sortWindow
        {
            get
            {
                return this.m_SortWindow;
            }
        }

        public UnitListFilterWindow filterWindow
        {
            get
            {
                return this.m_FilterWindow;
            }
        }

        public class Data
        {
            public string body;
            public UnitParam param;
            public UnitData unit;
            public SupportData support;
            public bool interactable;
            public bool partySelect;
            public int partyMainSlot;
            public int partySubSlot;
            public int partyIndex;
            public bool unlockable;
            public int pieceAmount;
            public UnitListRootWindow.Tab tabMask;
            public UnitListFilterWindow.SelectType filterMask;
            public long sortPriority;
            public int subSortPriority;

            public Data()
            {
                base..ctor();
                this.body = null;
                this.param = null;
                this.unit = null;
                this.interactable = 1;
                this.partySelect = 0;
                this.partyMainSlot = -1;
                this.partySubSlot = -1;
                this.partyIndex = -1;
                this.tabMask = 0;
                this.filterMask = 0;
                this.sortPriority = 0L;
                this.subSortPriority = 0;
                return;
            }

            public Data(SupportData _support)
            {
                this..ctor(_support.Unit);
                this.support = _support;
                return;
            }

            public Data(UnitData _unit)
            {
                this..ctor(_unit.UnitParam);
                this.unit = _unit;
                return;
            }

            public Data(UnitParam _param)
            {
                this..ctor();
                this.param = _param;
                this.pieceAmount = MonoSingleton<GameManager>.Instance.Player.GetItemAmount(this.param.piece);
                this.unlockable = (this.param.GetUnlockNeedPieces() <= this.pieceAmount) ? 1 : MonoSingleton<GameManager>.Instance.MasterParam.IsUnlockableUnit(this.param.unlock_time, TimeManager.ServerTime);
                return;
            }

            public Data(string _body)
            {
                this..ctor();
                this.body = _body;
                return;
            }

            public long GetUniq()
            {
                if (this.unit == null)
                {
                    goto Label_0017;
                }
                return this.unit.UniqueID;
            Label_0017:
                return 0L;
            }

            public void Refresh()
            {
                this.RefreshTabMask();
                this.RefreshFilterMask();
                this.sortPriority = 0L;
                return;
            }

            public void RefreshFilterMask()
            {
                if (this.param == null)
                {
                    goto Label_0023;
                }
                if (this.partyMainSlot != -1)
                {
                    goto Label_0023;
                }
                if (this.partySubSlot == -1)
                {
                    goto Label_002F;
                }
            Label_0023:
                this.filterMask = 0;
                goto Label_003B;
            Label_002F:
                this.filterMask = UnitListFilterWindow.GetFilterMask(this);
            Label_003B:
                return;
            }

            public void RefreshPartySlotPriority()
            {
                this.subSortPriority = 0;
                if ((this.body == "empty") == null)
                {
                    goto Label_002C;
                }
                this.subSortPriority = -2147483648;
                goto Label_0069;
            Label_002C:
                if (this.partyMainSlot == -1)
                {
                    goto Label_004D;
                }
                this.subSortPriority = -(100 - this.partyMainSlot);
                goto Label_0069;
            Label_004D:
                if (this.partySubSlot == -1)
                {
                    goto Label_0069;
                }
                this.subSortPriority = -(50 - this.partySubSlot);
            Label_0069:
                return;
            }

            public void RefreshSortPriority(UnitListSortWindow.SelectType sortType)
            {
                this.sortPriority = UnitListSortWindow.GetSortPriority(this, sortType);
                return;
            }

            public void RefreshTabMask()
            {
                if (this.support != null)
                {
                    goto Label_002E;
                }
                if (this.param == null)
                {
                    goto Label_002E;
                }
                if (this.partyMainSlot != -1)
                {
                    goto Label_002E;
                }
                if (this.partySubSlot == -1)
                {
                    goto Label_003E;
                }
            Label_002E:
                this.tabMask = 0xffff;
                goto Label_004A;
            Label_003E:
                this.tabMask = UnitListRootWindow.GetTabMask(this);
            Label_004A:
                return;
            }
        }

        public enum EditType
        {
            DEFAULT,
            PARTY,
            PARTY_TOWER,
            EQUIP,
            SUPPORT,
            SHOP_EQUIP
        }
    }
}

