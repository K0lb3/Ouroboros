namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class MultiInvitationReceiveWindow : FlowWindowBase
    {
        private SerializeParam m_Param;
        private Tab m_Tab;
        private bool m_Destroy;
        private ActiveContent.ItemSource m_ActiveSource;
        private ContentController m_ActiveController;
        private LogContent.ItemSource m_LogSource;
        private ContentController m_LogController;
        private SerializeValueBehaviour m_ValueList;
        private Toggle m_ActiveToggle;
        private Toggle m_LogToggle;
        private ActiveData m_ActiveData;
        private LogData m_LogData;
        private static MultiInvitationReceiveWindow m_Instance;

        static MultiInvitationReceiveWindow()
        {
        }

        public MultiInvitationReceiveWindow()
        {
            base..ctor();
            return;
        }

        public bool DeserializeActiveList(FlowNode_ReqMultiInvitation.Api_RoomInvitation.Json json)
        {
            this.m_ActiveData = new ActiveData();
            return this.m_ActiveData.Decerialize(json);
        }

        public bool DeserializeLogList(int page, FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.Json json)
        {
            this.m_LogData = new LogData();
            this.m_LogData.Decerialize(page, json);
            return 1;
        }

        private ActiveData.RoomData GetActiveRoomData(int roomId)
        {
            int num;
            ActiveData.RoomData data;
            if (this.m_ActiveData == null)
            {
                goto Label_005B;
            }
            if (this.m_ActiveData.rooms == null)
            {
                goto Label_005B;
            }
            num = 0;
            goto Label_0048;
        Label_0022:
            data = this.m_ActiveData.rooms[num];
            if (data == null)
            {
                goto Label_0044;
            }
            if (data.roomid != roomId)
            {
                goto Label_0044;
            }
            return data;
        Label_0044:
            num += 1;
        Label_0048:
            if (num < ((int) this.m_ActiveData.rooms.Length))
            {
                goto Label_0022;
            }
        Label_005B:
            return null;
        }

        public int GetLogPage()
        {
            if (this.m_LogData == null)
            {
                goto Label_0017;
            }
            return this.m_LogData.page;
        Label_0017:
            return -1;
        }

        public override void Initialize(FlowWindowBase.SerializeParamBase param)
        {
            m_Instance = this;
            base.Initialize(param);
            this.m_Param = param as SerializeParam;
            if (this.m_Param != null)
            {
                goto Label_003A;
            }
            throw new Exception(this.ToString() + " > Failed serializeParam null.");
        Label_003A:
            this.m_ValueList = this.m_Param.window.GetComponent<SerializeValueBehaviour>();
            if ((this.m_ValueList != null) == null)
            {
                goto Label_00D1;
            }
            this.m_ActiveToggle = this.m_ValueList.list.GetUIToggle("tgl_receive");
            if ((this.m_ActiveToggle != null) == null)
            {
                goto Label_0099;
            }
            this.m_ActiveToggle.set_interactable(0);
        Label_0099:
            this.m_LogToggle = this.m_ValueList.list.GetUIToggle("tgl_send");
            if ((this.m_LogToggle != null) == null)
            {
                goto Label_00D1;
            }
            this.m_LogToggle.set_interactable(0);
        Label_00D1:
            if ((this.m_Param.activeList != null) == null)
            {
                goto Label_011A;
            }
            this.m_ActiveController = this.m_Param.activeList.GetComponentInChildren<ContentController>();
            if ((this.m_ActiveController != null) == null)
            {
                goto Label_011A;
            }
            this.m_ActiveController.SetWork(this);
        Label_011A:
            if ((this.m_Param.logList != null) == null)
            {
                goto Label_0163;
            }
            this.m_LogController = this.m_Param.logList.GetComponentInChildren<ContentController>();
            if ((this.m_LogController != null) == null)
            {
                goto Label_0163;
            }
            this.m_LogController.SetWork(this);
        Label_0163:
            base.Close(1);
            return;
        }

        public void InitializeActiveList()
        {
            int num;
            ActiveData.RoomData data;
            ActiveContent.ItemSource.ItemParam param;
            this.ReleaseActiveList();
            if ((this.m_ActiveController != null) == null)
            {
                goto Label_00B4;
            }
            this.m_ActiveSource = new ActiveContent.ItemSource();
            if (this.m_ActiveData == null)
            {
                goto Label_009E;
            }
            if (this.m_ActiveData.rooms == null)
            {
                goto Label_009E;
            }
            num = 0;
            goto Label_008B;
        Label_0044:
            data = this.m_ActiveData.rooms[num];
            if (data == null)
            {
                goto Label_0087;
            }
            if (data.isValid == null)
            {
                goto Label_0087;
            }
            param = new ActiveContent.ItemSource.ItemParam(data);
            if (param == null)
            {
                goto Label_0087;
            }
            if (param.IsValid() == null)
            {
                goto Label_0087;
            }
            this.m_ActiveSource.Add(param);
        Label_0087:
            num += 1;
        Label_008B:
            if (num < ((int) this.m_ActiveData.rooms.Length))
            {
                goto Label_0044;
            }
        Label_009E:
            this.m_ActiveController.Initialize(this.m_ActiveSource, Vector2.get_zero());
        Label_00B4:
            return;
        }

        public void InitializeLogList()
        {
            int num;
            LogData.RoomData data;
            LogContent.ItemSource.ItemParam param;
            this.ReleaseLogList();
            if ((this.m_LogController != null) == null)
            {
                goto Label_00C5;
            }
            this.m_LogSource = new LogContent.ItemSource();
            if (this.m_LogData == null)
            {
                goto Label_00AF;
            }
            if (this.m_LogData.rooms == null)
            {
                goto Label_00AF;
            }
            num = 0;
            goto Label_009C;
        Label_0044:
            data = this.m_LogData.rooms[num];
            if (data == null)
            {
                goto Label_0098;
            }
            if (data.isValid == null)
            {
                goto Label_0098;
            }
            if (this.GetActiveRoomData(data.roomid) != null)
            {
                goto Label_0098;
            }
            param = new LogContent.ItemSource.ItemParam(data);
            if (param == null)
            {
                goto Label_0098;
            }
            if (param.IsValid() == null)
            {
                goto Label_0098;
            }
            this.m_LogSource.Add(param);
        Label_0098:
            num += 1;
        Label_009C:
            if (num < ((int) this.m_LogData.rooms.Length))
            {
                goto Label_0044;
            }
        Label_00AF:
            this.m_LogController.Initialize(this.m_LogSource, Vector2.get_zero());
        Label_00C5:
            return;
        }

        public override int OnActivate(int pinId)
        {
            SerializeValueList list;
            ActiveData.RoomData data;
            string str;
            if (pinId != 200)
            {
                goto Label_0034;
            }
            if (this.SetTab(1) == null)
            {
                goto Label_0023;
            }
            this.InitializeActiveList();
            this.InitializeLogList();
        Label_0023:
            MultiInvitationBadge.isValid = 0;
            base.Open();
            goto Label_017F;
        Label_0034:
            if (pinId != 210)
            {
                goto Label_0052;
            }
            this.m_Destroy = 1;
            base.Close(0);
            goto Label_017F;
        Label_0052:
            if (pinId != 220)
            {
                goto Label_0075;
            }
            if (this.SetTab(1) == null)
            {
                goto Label_006F;
            }
            this.InitializeActiveList();
        Label_006F:
            return 300;
        Label_0075:
            if (pinId != 230)
            {
                goto Label_0098;
            }
            if (this.SetTab(2) == null)
            {
                goto Label_0092;
            }
            this.InitializeLogList();
        Label_0092:
            return 300;
        Label_0098:
            if (pinId != 240)
            {
                goto Label_0158;
            }
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_017F;
            }
            data = list.GetDataSource<ActiveData.RoomData>("item");
            if (data == null)
            {
                goto Label_017F;
            }
            GlobalVars.SelectedMultiPlayRoomID = data.roomid;
            GlobalVars.MultiInvitation = data.multiType;
            GlobalVars.MultiInvitationRoomOwner = data.owner.fuid;
            GlobalVars.MultiInvitationRoomLocked = data.locked;
            str = "MENU_MULTI";
            if (data.multiType != 2)
            {
                goto Label_0129;
            }
            str = "MENU_MULTITOWER";
            GlobalVars.SelectedMultiTowerID = data.quest.param.iname;
            goto Label_013E;
        Label_0129:
            GlobalVars.SelectedQuestID = data.quest.param.iname;
        Label_013E:
            GlobalEvent.Invoke(str, null);
            this.m_Destroy = 1;
            base.Close(0);
            goto Label_017F;
        Label_0158:
            if (pinId != 250)
            {
                goto Label_016E;
            }
            this.InitializeActiveList();
            goto Label_017F;
        Label_016E:
            if (pinId != 260)
            {
                goto Label_017F;
            }
            this.InitializeLogList();
        Label_017F:
            return -1;
        }

        public override void Release()
        {
            this.ReleaseActiveList();
            this.ReleaseLogList();
            base.Release();
            m_Instance = null;
            return;
        }

        public void ReleaseActiveList()
        {
            if ((this.m_ActiveController != null) == null)
            {
                goto Label_001C;
            }
            this.m_ActiveController.Release();
        Label_001C:
            this.m_ActiveSource = null;
            return;
        }

        public void ReleaseLogList()
        {
            if ((this.m_LogController != null) == null)
            {
                goto Label_001C;
            }
            this.m_LogController.Release();
        Label_001C:
            this.m_LogSource = null;
            return;
        }

        public static void SetBadge(bool value)
        {
            if (m_Instance != null)
            {
                goto Label_002A;
            }
            if (value == null)
            {
                goto Label_002A;
            }
            if (MultiInvitationBadge.isValid != null)
            {
                goto Label_0030;
            }
            MultiInvitationBadge.isValid = value;
            NotifyList.PushMultiInvitation();
            goto Label_0030;
        Label_002A:
            MultiInvitationBadge.isValid = 0;
        Label_0030:
            return;
        }

        private bool SetTab(Tab tab)
        {
            bool flag;
            Tab tab2;
            flag = 0;
            if (this.m_Tab == tab)
            {
                goto Label_0129;
            }
            flag = 1;
            tab2 = tab;
            switch (tab2)
            {
                case 0:
                    goto Label_0029;

                case 1:
                    goto Label_007C;

                case 2:
                    goto Label_00CF;
            }
            goto Label_0122;
        Label_0029:
            if ((this.m_Param.tabActive != null) == null)
            {
                goto Label_0050;
            }
            this.m_Param.tabActive.SetActive(0);
        Label_0050:
            if ((this.m_Param.tabLog != null) == null)
            {
                goto Label_0122;
            }
            this.m_Param.tabLog.SetActive(0);
            goto Label_0122;
        Label_007C:
            if ((this.m_Param.tabActive != null) == null)
            {
                goto Label_00A3;
            }
            this.m_Param.tabActive.SetActive(1);
        Label_00A3:
            if ((this.m_Param.tabLog != null) == null)
            {
                goto Label_0122;
            }
            this.m_Param.tabLog.SetActive(0);
            goto Label_0122;
        Label_00CF:
            if ((this.m_Param.tabActive != null) == null)
            {
                goto Label_00F6;
            }
            this.m_Param.tabActive.SetActive(0);
        Label_00F6:
            if ((this.m_Param.tabLog != null) == null)
            {
                goto Label_0122;
            }
            this.m_Param.tabLog.SetActive(1);
        Label_0122:
            this.m_Tab = tab;
        Label_0129:
            return flag;
        }

        public override int Update()
        {
            int num;
            base.Update();
            if (this.m_Destroy == null)
            {
                goto Label_002A;
            }
            if (base.isClosed == null)
            {
                goto Label_002A;
            }
            base.SetActiveChild(0);
            return 290;
        Label_002A:
            num = CriticalSection.GetActive();
            if ((this.m_ActiveToggle != null) == null)
            {
                goto Label_0050;
            }
            this.m_ActiveToggle.set_interactable(num == 0);
        Label_0050:
            if ((this.m_LogToggle != null) == null)
            {
                goto Label_0070;
            }
            this.m_LogToggle.set_interactable(num == 0);
        Label_0070:
            return -1;
        }

        public override string name
        {
            get
            {
                return "MultiInvitationReceiveWindow";
            }
        }

        public static MultiInvitationReceiveWindow instance
        {
            get
            {
                return m_Instance;
            }
        }

        public static class ActiveContent
        {
            public static ItemAccessor clickItem;

            static ActiveContent()
            {
            }

            public class ItemAccessor
            {
                private ContentNode m_Node;
                private MultiInvitationReceiveWindow.ActiveData.RoomData m_Param;
                private DataSource m_DataSource;
                private SerializeValueBehaviour m_Value;

                public ItemAccessor()
                {
                    base..ctor();
                    return;
                }

                public void Bind(ContentNode node)
                {
                    this.m_Node = node;
                    this.m_DataSource = DataSource.Create(node.get_gameObject());
                    this.m_DataSource.Add(typeof(FriendData), this.m_Param.owner.friend);
                    if (this.m_Param.owner.unit == null)
                    {
                        goto Label_0077;
                    }
                    this.m_DataSource.Add(typeof(UnitData), this.m_Param.owner.unit);
                Label_0077:
                    this.m_DataSource.Add(typeof(MultiInvitationReceiveWindow.ActiveData.RoomData), this.m_Param);
                    this.m_Value = this.m_Node.GetComponent<SerializeValueBehaviour>();
                    if ((this.m_Value != null) == null)
                    {
                        goto Label_00FE;
                    }
                    this.m_Value.list.SetField("quest_name", this.m_Param.quest.param.name);
                    this.m_Value.list.SetField("comment", this.m_Param.comment);
                Label_00FE:
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
                    return;
                }

                public void ForceUpdate()
                {
                }

                public void Setup(MultiInvitationReceiveWindow.ActiveData.RoomData param)
                {
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

                public MultiInvitationReceiveWindow.ActiveData.RoomData param
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
                        return ((this.m_Param == null) ? 0 : this.m_Param.isValid);
                    }
                }
            }

            public class ItemSource : ContentSource
            {
                private List<ItemParam> m_Params;
                [CompilerGenerated]
                private static Func<ItemParam, bool> <>f__am$cache1;

                public ItemSource()
                {
                    this.m_Params = new List<ItemParam>();
                    base..ctor();
                    return;
                }

                [CompilerGenerated]
                private static bool <Setup>m__366(ItemParam prop)
                {
                    return 1;
                }

                public void Add(ItemParam param)
                {
                    if (param.IsValid() == null)
                    {
                        goto Label_0017;
                    }
                    this.m_Params.Add(param);
                Label_0017:
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
                    base.Release();
                    return;
                }

                public unsafe void Setup()
                {
                    Func<ItemParam, bool> func;
                    bool flag;
                    Vector2 vector;
                    Vector2 vector2;
                    if (<>f__am$cache1 != null)
                    {
                        goto Label_0018;
                    }
                    <>f__am$cache1 = new Func<ItemParam, bool>(MultiInvitationReceiveWindow.ActiveContent.ItemSource.<Setup>m__366);
                Label_0018:
                    func = <>f__am$cache1;
                    this.Clear();
                    if (func == null)
                    {
                        goto Label_0046;
                    }
                    base.SetTable(Enumerable.ToArray<ItemParam>(Enumerable.Where<ItemParam>(this.m_Params, func)));
                    goto Label_0057;
                Label_0046:
                    base.SetTable(this.m_Params.ToArray());
                Label_0057:
                    base.contentController.Resize(0);
                    flag = 0;
                    vector = base.contentController.anchoredPosition;
                    vector2 = base.contentController.GetLastPageAnchorePos();
                    if (&vector.x >= &vector2.x)
                    {
                        goto Label_00A0;
                    }
                    flag = 1;
                    &vector.x = &vector2.x;
                Label_00A0:
                    if (&vector.y >= &vector2.y)
                    {
                        goto Label_00C3;
                    }
                    flag = 1;
                    &vector.y = &vector2.y;
                Label_00C3:
                    if (flag == null)
                    {
                        goto Label_00D5;
                    }
                    base.contentController.anchoredPosition = vector;
                Label_00D5:
                    if ((base.contentController.scroller != null) == null)
                    {
                        goto Label_00FB;
                    }
                    base.contentController.scroller.StopMovement();
                Label_00FB:
                    return;
                }

                public class ItemParam : ContentSource.Param
                {
                    private MultiInvitationReceiveWindow.ActiveContent.ItemAccessor m_Accessor;

                    public ItemParam(MultiInvitationReceiveWindow.ActiveData.RoomData param)
                    {
                        this.m_Accessor = new MultiInvitationReceiveWindow.ActiveContent.ItemAccessor();
                        base..ctor();
                        this.m_Accessor.Setup(param);
                        return;
                    }

                    public override bool IsValid()
                    {
                        return this.m_Accessor.isValid;
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

                    public MultiInvitationReceiveWindow.ActiveContent.ItemAccessor accerror
                    {
                        get
                        {
                            return this.m_Accessor;
                        }
                    }

                    public MultiInvitationReceiveWindow.ActiveData.RoomData param
                    {
                        get
                        {
                            return this.m_Accessor.param;
                        }
                    }
                }
            }
        }

        public class ActiveData
        {
            public RoomData[] rooms;

            public ActiveData()
            {
                base..ctor();
                return;
            }

            public bool Decerialize(FlowNode_ReqMultiInvitation.Api_RoomInvitation.Json json)
            {
                int num;
                RoomData data;
                if ((json != null) && (json.rooms != null))
                {
                    goto Label_0013;
                }
                return 0;
            Label_0013:
                this.rooms = new RoomData[(int) json.rooms.Length];
                num = 0;
                goto Label_010F;
            Label_002D:
                data = new RoomData();
                data.roomid = json.rooms[num].roomid;
                data.comment = json.rooms[num].comment;
                data.num = json.rooms[num].num;
                data.multiType = ((json.rooms[num].btype == "multi") == null) ? 2 : 1;
                data.locked = json.rooms[num].pwd_hash == "1";
                data.owner = new OwnerData(json.rooms[num].owner);
                data.quest = new QuestData(json.rooms[num].quest);
                if (string.IsNullOrEmpty(data.comment) == null)
                {
                    goto Label_0102;
                }
                data.comment = LocalizedText.Get("sys.MULTI_INVTATION_COMMNET");
            Label_0102:
                this.rooms[num] = data;
                num += 1;
            Label_010F:
                if (num < ((int) json.rooms.Length))
                {
                    goto Label_002D;
                }
                return 1;
            }

            public class OwnerData
            {
                public FriendData friend;
                public UnitData unit;

                public OwnerData(FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomOwner json)
                {
                    <OwnerData>c__AnonStorey35E storeye;
                    storeye = new <OwnerData>c__AnonStorey35E();
                    storeye.json = json;
                    base..ctor();
                    if (storeye.json == null)
                    {
                        goto Label_008A;
                    }
                    this.friend = MonoSingleton<GameManager>.Instance.Player.Friends.Find(new Predicate<FriendData>(storeye.<>m__368));
                    if (storeye.json.units == null)
                    {
                        goto Label_008A;
                    }
                    if (((int) storeye.json.units.Length) <= 0)
                    {
                        goto Label_008A;
                    }
                    this.unit = new UnitData();
                    this.unit.Deserialize(storeye.json.units[0]);
                Label_008A:
                    return;
                }

                public bool isValid
                {
                    get
                    {
                        return ((this.friend == null) == 0);
                    }
                }

                public string fuid
                {
                    get
                    {
                        return this.friend.FUID;
                    }
                }

                [CompilerGenerated]
                private sealed class <OwnerData>c__AnonStorey35E
                {
                    internal FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomOwner json;

                    public <OwnerData>c__AnonStorey35E()
                    {
                        base..ctor();
                        return;
                    }

                    internal bool <>m__368(FriendData prop)
                    {
                        return (prop.FUID == this.json.fuid);
                    }
                }
            }

            public class QuestData
            {
                public QuestParam param;

                public QuestData(FlowNode_ReqMultiInvitation.Api_RoomInvitation.JsonRoomQuest json)
                {
                    base..ctor();
                    if (json == null)
                    {
                        goto Label_0022;
                    }
                    this.param = MonoSingleton<GameManager>.Instance.FindQuest(json.iname);
                Label_0022:
                    return;
                }

                public bool isValid
                {
                    get
                    {
                        return ((this.param == null) == 0);
                    }
                }
            }

            public class RoomData
            {
                public int roomid;
                public string comment;
                public int num;
                public MultiInvitationReceiveWindow.MultiType multiType;
                public MultiInvitationReceiveWindow.ActiveData.OwnerData owner;
                public MultiInvitationReceiveWindow.ActiveData.QuestData quest;
                public bool locked;

                public RoomData()
                {
                    base..ctor();
                    return;
                }

                public bool isValid
                {
                    get
                    {
                        if (((this.multiType != null) && (this.owner != null)) && (this.quest != null))
                        {
                            goto Label_0023;
                        }
                        return 0;
                    Label_0023:
                        return ((this.owner.isValid == null) ? 0 : this.quest.isValid);
                    }
                }
            }
        }

        public static class LogContent
        {
            public static ItemAccessor clickItem;

            static LogContent()
            {
            }

            public class ItemAccessor
            {
                private ContentNode m_Node;
                private MultiInvitationReceiveWindow.LogData.RoomData m_Param;
                private DataSource m_DataSource;
                private SerializeValueBehaviour m_Value;

                public ItemAccessor()
                {
                    base..ctor();
                    return;
                }

                public unsafe void Bind(ContentNode node)
                {
                    object[] objArray3;
                    object[] objArray2;
                    object[] objArray1;
                    DateTime time;
                    TimeSpan span;
                    int num;
                    int num2;
                    int num3;
                    this.m_Node = node;
                    this.m_DataSource = DataSource.Create(node.get_gameObject());
                    if (this.m_Param.owner.unit == null)
                    {
                        goto Label_0052;
                    }
                    this.m_DataSource.Add(typeof(UnitData), this.m_Param.owner.unit);
                Label_0052:
                    this.m_DataSource.Add(typeof(MultiInvitationReceiveWindow.ActiveData.RoomData), this.m_Param);
                    this.m_Value = this.m_Node.GetComponent<SerializeValueBehaviour>();
                    if ((this.m_Value != null) == null)
                    {
                        goto Label_01E4;
                    }
                    this.m_Value.list.SetField("quest_name", this.m_Param.quest.param.name);
                    this.m_Value.list.SetField("name", this.m_Param.owner.name);
                    this.m_Value.list.SetField("lv", this.m_Param.owner.lv);
                    time = GameUtility.UnixtimeToLocalTime(this.m_Param.created_at);
                    span = DateTime.Now - time;
                    num = &span.Days;
                    if (num <= 0)
                    {
                        goto Label_0163;
                    }
                    objArray1 = new object[] { &num.ToString() };
                    this.m_Value.list.SetField("time", LocalizedText.Get("sys.MULTIINVITATION_RECEIVE_TIMEDAY", objArray1));
                    goto Label_01E4;
                Label_0163:
                    if (&span.Hours <= 0)
                    {
                        goto Label_01AC;
                    }
                    objArray2 = new object[] { &&span.Hours.ToString() };
                    this.m_Value.list.SetField("time", LocalizedText.Get("sys.MULTIINVITATION_RECEIVE_TIMHOUR", objArray2));
                    goto Label_01E4;
                Label_01AC:
                    objArray3 = new object[] { &&span.Minutes.ToString() };
                    this.m_Value.list.SetField("time", LocalizedText.Get("sys.MULTIINVITATION_RECEIVE_TIMEMINUTE", objArray3));
                Label_01E4:
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
                    return;
                }

                public void ForceUpdate()
                {
                }

                public void Setup(MultiInvitationReceiveWindow.LogData.RoomData param)
                {
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

                public MultiInvitationReceiveWindow.LogData.RoomData param
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
                        return ((this.m_Param == null) ? 0 : this.m_Param.isValid);
                    }
                }
            }

            public class ItemSource : ContentSource
            {
                private List<ItemParam> m_Params;
                [CompilerGenerated]
                private static Func<ItemParam, bool> <>f__am$cache1;

                public ItemSource()
                {
                    this.m_Params = new List<ItemParam>();
                    base..ctor();
                    return;
                }

                [CompilerGenerated]
                private static bool <Setup>m__367(ItemParam prop)
                {
                    return 1;
                }

                public void Add(ItemParam param)
                {
                    if (param.IsValid() == null)
                    {
                        goto Label_0017;
                    }
                    this.m_Params.Add(param);
                Label_0017:
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
                    base.Release();
                    return;
                }

                public unsafe void Setup()
                {
                    Func<ItemParam, bool> func;
                    bool flag;
                    Vector2 vector;
                    Vector2 vector2;
                    if (<>f__am$cache1 != null)
                    {
                        goto Label_0018;
                    }
                    <>f__am$cache1 = new Func<ItemParam, bool>(MultiInvitationReceiveWindow.LogContent.ItemSource.<Setup>m__367);
                Label_0018:
                    func = <>f__am$cache1;
                    this.Clear();
                    if (func == null)
                    {
                        goto Label_0046;
                    }
                    base.SetTable(Enumerable.ToArray<ItemParam>(Enumerable.Where<ItemParam>(this.m_Params, func)));
                    goto Label_0057;
                Label_0046:
                    base.SetTable(this.m_Params.ToArray());
                Label_0057:
                    base.contentController.Resize(0);
                    flag = 0;
                    vector = base.contentController.anchoredPosition;
                    vector2 = base.contentController.GetLastPageAnchorePos();
                    if (&vector.x >= &vector2.x)
                    {
                        goto Label_00A0;
                    }
                    flag = 1;
                    &vector.x = &vector2.x;
                Label_00A0:
                    if (&vector.y >= &vector2.y)
                    {
                        goto Label_00C3;
                    }
                    flag = 1;
                    &vector.y = &vector2.y;
                Label_00C3:
                    if (flag == null)
                    {
                        goto Label_00D5;
                    }
                    base.contentController.anchoredPosition = vector;
                Label_00D5:
                    if ((base.contentController.scroller != null) == null)
                    {
                        goto Label_00FB;
                    }
                    base.contentController.scroller.StopMovement();
                Label_00FB:
                    return;
                }

                public class ItemParam : ContentSource.Param
                {
                    private MultiInvitationReceiveWindow.LogContent.ItemAccessor m_Accessor;

                    public ItemParam(MultiInvitationReceiveWindow.LogData.RoomData param)
                    {
                        this.m_Accessor = new MultiInvitationReceiveWindow.LogContent.ItemAccessor();
                        base..ctor();
                        this.m_Accessor.Setup(param);
                        return;
                    }

                    public override bool IsValid()
                    {
                        return this.m_Accessor.isValid;
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

                    public MultiInvitationReceiveWindow.LogContent.ItemAccessor accerror
                    {
                        get
                        {
                            return this.m_Accessor;
                        }
                    }

                    public MultiInvitationReceiveWindow.LogData.RoomData param
                    {
                        get
                        {
                            return this.m_Accessor.param;
                        }
                    }
                }
            }
        }

        public class LogData
        {
            public int page;
            public RoomData[] rooms;

            public LogData()
            {
                this.page = -1;
                base..ctor();
                return;
            }

            public bool Decerialize(int _page, FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.Json json)
            {
                int num;
                RoomData data;
                DateTime time;
                if ((json != null) && (json.list != null))
                {
                    goto Label_0013;
                }
                return 0;
            Label_0013:
                this.page = _page;
                this.rooms = new RoomData[(int) json.list.Length];
                num = 0;
                goto Label_00FC;
            Label_0034:
                data = new RoomData();
                data.id = json.list[num].id;
                data.roomid = json.list[num].roomid;
                data.multiType = ((json.list[num].btype == "multi") == null) ? 2 : 1;
                data.owner = new OwnerData(json.list[num].player);
                data.quest = new QuestData(json.list[num].iname);
                if (string.IsNullOrEmpty(json.list[num].created_at) != null)
                {
                    goto Label_00EF;
                }
                time = DateTime.Parse(json.list[num].created_at);
                data.created_at = TimeManager.GetUnixSec(time);
            Label_00EF:
                this.rooms[num] = data;
                num += 1;
            Label_00FC:
                if (num < ((int) json.list.Length))
                {
                    goto Label_0034;
                }
                return 1;
            }

            public class OwnerData
            {
                public string uid;
                public string fuid;
                public string name;
                public int lv;
                public UnitData unit;

                public OwnerData(FlowNode_ReqMultiInvitationHistory.Api_MultiInvitationHistory.JsonPlayer json)
                {
                    base..ctor();
                    if (json == null)
                    {
                        goto Label_0063;
                    }
                    this.uid = json.uid;
                    this.fuid = json.fuid;
                    this.name = json.name;
                    this.lv = json.lv;
                    if (json.unit == null)
                    {
                        goto Label_0063;
                    }
                    this.unit = new UnitData();
                    this.unit.Deserialize(json.unit);
                Label_0063:
                    return;
                }

                public bool isValid
                {
                    get
                    {
                        return ((string.IsNullOrEmpty(this.uid) != null) ? 0 : ((this.unit == null) == 0));
                    }
                }
            }

            public class QuestData
            {
                public QuestParam param;

                public QuestData(string iname)
                {
                    base..ctor();
                    this.param = MonoSingleton<GameManager>.Instance.FindQuest(iname);
                    return;
                }

                public bool isValid
                {
                    get
                    {
                        return ((this.param == null) == 0);
                    }
                }
            }

            public class RoomData
            {
                public int id;
                public int roomid;
                public MultiInvitationReceiveWindow.MultiType multiType;
                public long created_at;
                public MultiInvitationReceiveWindow.LogData.OwnerData owner;
                public MultiInvitationReceiveWindow.LogData.QuestData quest;

                public RoomData()
                {
                    base..ctor();
                    return;
                }

                public bool isValid
                {
                    get
                    {
                        if (((this.multiType != null) && (this.owner != null)) && (this.quest != null))
                        {
                            goto Label_0023;
                        }
                        return 0;
                    Label_0023:
                        return ((this.owner.isValid == null) ? 0 : this.quest.isValid);
                    }
                }
            }
        }

        public enum MultiType
        {
            NONE,
            NORMAL,
            TOWER
        }

        [Serializable]
        public class SerializeParam : FlowWindowBase.SerializeParamBase
        {
            public GameObject tabActive;
            public GameObject tabLog;
            public GameObject activeList;
            public GameObject logList;

            public SerializeParam()
            {
                base..ctor();
                return;
            }

            public override Type type
            {
                get
                {
                    return typeof(MultiInvitationReceiveWindow);
                }
            }
        }

        public enum Tab
        {
            NONE,
            ACTIVE,
            LOG
        }
    }
}

