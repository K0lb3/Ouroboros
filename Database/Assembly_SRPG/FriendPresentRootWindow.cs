namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class FriendPresentRootWindow : FlowWindowBase
    {
        private SerializeParam m_Param;
        private Tab m_Tab;
        private bool m_Destroy;
        private WantContent.ItemSource m_WantSource;
        private ContentController m_WantController;
        private ReceiveContent.ItemSource m_ReceiveSource;
        private ContentController m_ReceiveController;
        private SendContent.ItemSource m_SendSource;
        private ContentController m_SendController;
        private SerializeValueBehaviour m_ValueList;
        private Toggle m_ReceiveToggle;
        private Toggle m_SendToggle;
        private static FriendPresentRootWindow m_Instance;
        private static SendStatus m_SendStatus;

        static FriendPresentRootWindow()
        {
            m_SendStatus = 1;
            return;
        }

        public FriendPresentRootWindow()
        {
            base..ctor();
            return;
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
            this.m_ReceiveToggle = this.m_ValueList.list.GetUIToggle("tgl_receive");
            if ((this.m_ReceiveToggle != null) == null)
            {
                goto Label_0099;
            }
            this.m_ReceiveToggle.set_interactable(0);
        Label_0099:
            this.m_SendToggle = this.m_ValueList.list.GetUIToggle("tgl_send");
            if ((this.m_SendToggle != null) == null)
            {
                goto Label_00D1;
            }
            this.m_SendToggle.set_interactable(0);
        Label_00D1:
            if ((this.m_Param.wantList != null) == null)
            {
                goto Label_011A;
            }
            this.m_WantController = this.m_Param.wantList.GetComponentInChildren<ContentController>();
            if ((this.m_WantController != null) == null)
            {
                goto Label_011A;
            }
            this.m_WantController.SetWork(this);
        Label_011A:
            if ((this.m_Param.receiveList != null) == null)
            {
                goto Label_0163;
            }
            this.m_ReceiveController = this.m_Param.receiveList.GetComponentInChildren<ContentController>();
            if ((this.m_ReceiveController != null) == null)
            {
                goto Label_0163;
            }
            this.m_ReceiveController.SetWork(this);
        Label_0163:
            if ((this.m_Param.sendList != null) == null)
            {
                goto Label_01AC;
            }
            this.m_SendController = this.m_Param.sendList.GetComponentInChildren<ContentController>();
            if ((this.m_SendController != null) == null)
            {
                goto Label_01AC;
            }
            this.m_SendController.SetWork(this);
        Label_01AC:
            base.Close(1);
            return;
        }

        public void InitializeReceiveList()
        {
            List<FriendPresentReceiveList.Param> list;
            int num;
            ReceiveContent.ItemSource.ItemParam param;
            this.ReleaseReceiveList();
            if ((this.m_ReceiveController != null) == null)
            {
                goto Label_0088;
            }
            this.m_ReceiveSource = new ReceiveContent.ItemSource();
            list = MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.list;
            num = 0;
            goto Label_0066;
        Label_003E:
            param = new ReceiveContent.ItemSource.ItemParam(list[num]);
            if (param.IsValid() == null)
            {
                goto Label_0062;
            }
            this.m_ReceiveSource.Add(param);
        Label_0062:
            num += 1;
        Label_0066:
            if (num < list.Count)
            {
                goto Label_003E;
            }
            this.m_ReceiveController.Initialize(this.m_ReceiveSource, Vector2.get_zero());
        Label_0088:
            if ((this.m_ValueList != null) == null)
            {
                goto Label_00C6;
            }
            this.m_ValueList.list.SetInteractable("btn_receive", (this.m_ReceiveSource.GetCount() != null) ? 1 : 0);
        Label_00C6:
            return;
        }

        public void InitializeSendList()
        {
            List<FriendData> list;
            int num;
            FriendData data;
            SendContent.ItemSource.ItemParam param;
            bool flag;
            SendStatus status;
            this.ReleaseSendList();
            if ((this.m_SendController != null) == null)
            {
                goto Label_00A0;
            }
            this.m_SendSource = new SendContent.ItemSource();
            list = MonoSingleton<GameManager>.Instance.Player.Friends;
            num = 0;
            goto Label_007E;
        Label_0039:
            data = list[num];
            if ((data == null) || ((data.WishStatus != "1") == null))
            {
                goto Label_007A;
            }
            param = new SendContent.ItemSource.ItemParam(data);
            if (param.IsValid() == null)
            {
                goto Label_007A;
            }
            this.m_SendSource.Add(param);
        Label_007A:
            num += 1;
        Label_007E:
            if (num < list.Count)
            {
                goto Label_0039;
            }
            this.m_SendController.Initialize(this.m_SendSource, Vector2.get_zero());
        Label_00A0:
            if ((this.m_ValueList != null) == null)
            {
                goto Label_01B3;
            }
            flag = 0;
            switch (m_SendStatus)
            {
                case 0:
                    goto Label_00DB;

                case 1:
                    goto Label_00FD;

                case 2:
                    goto Label_011F;

                case 3:
                    goto Label_0163;

                case 4:
                    goto Label_0141;
            }
            goto Label_0185;
        Label_00DB:
            this.m_ValueList.list.SetField("status", string.Empty);
            flag = 0;
            goto Label_0185;
        Label_00FD:
            this.m_ValueList.list.SetField("status", "sys.FRIENDPRESENT_STATUS_UNSENT");
            flag = 1;
            goto Label_0185;
        Label_011F:
            this.m_ValueList.list.SetField("status", "sys.FRIENDPRESENT_STATUS_SENDING");
            flag = 0;
            goto Label_0185;
        Label_0141:
            this.m_ValueList.list.SetField("status", "sys.FRIENDPRESENT_STATUS_SENDED");
            flag = 1;
            goto Label_0185;
        Label_0163:
            this.m_ValueList.list.SetField("status", "sys.FRIENDPRESENT_STATUS_SENTFAILED");
            flag = 1;
        Label_0185:
            this.m_ValueList.list.SetInteractable("btn_send", (this.m_SendSource.GetCount() != null) ? flag : 0);
        Label_01B3:
            return;
        }

        public void InitializeWantList()
        {
            int num;
            FriendPresentItemParam param;
            this.ReleaseWantList();
            if ((this.m_WantController != null) == null)
            {
                goto Label_008D;
            }
            this.m_WantSource = new WantContent.ItemSource();
            num = 0;
            goto Label_0070;
        Label_0029:
            param = MonoSingleton<GameManager>.Instance.Player.FriendPresentWishList[num];
            if (param == null)
            {
                goto Label_005B;
            }
            this.m_WantSource.Add(new WantContent.ItemSource.ItemParam(param));
            goto Label_006C;
        Label_005B:
            this.m_WantSource.Add(new WantContent.ItemSource.ItemParam(null));
        Label_006C:
            num += 1;
        Label_0070:
            if (num < 3)
            {
                goto Label_0029;
            }
            this.m_WantController.Initialize(this.m_WantSource, Vector2.get_zero());
        Label_008D:
            return;
        }

        public override int OnActivate(int pinId)
        {
            if (pinId != 100)
            {
                goto Label_0032;
            }
            if (this.SetTab(1) == null)
            {
                goto Label_0026;
            }
            this.InitializeWantList();
            this.InitializeReceiveList();
            this.InitializeSendList();
        Label_0026:
            base.Open();
            return 0xbf;
        Label_0032:
            if (pinId != 110)
            {
                goto Label_004D;
            }
            this.m_Destroy = 1;
            base.Close(0);
            goto Label_011B;
        Label_004D:
            if (pinId != 120)
            {
                goto Label_0072;
            }
            if (this.SetTab(1) == null)
            {
                goto Label_011B;
            }
            this.InitializeWantList();
            this.InitializeReceiveList();
            goto Label_011B;
        Label_0072:
            if (pinId != 130)
            {
                goto Label_0094;
            }
            if (this.SetTab(2) == null)
            {
                goto Label_011B;
            }
            this.InitializeSendList();
            goto Label_011B;
        Label_0094:
            if (pinId != 140)
            {
                goto Label_00A4;
            }
            goto Label_011B;
        Label_00A4:
            if (pinId != 150)
            {
                goto Label_00B4;
            }
            goto Label_011B;
        Label_00B4:
            if (pinId != 160)
            {
                goto Label_00CA;
            }
            this.InitializeWantList();
            goto Label_011B;
        Label_00CA:
            if (pinId != 170)
            {
                goto Label_00E0;
            }
            this.InitializeReceiveList();
            goto Label_011B;
        Label_00E0:
            if (pinId != 0xab)
            {
                goto Label_010A;
            }
            MonoSingleton<GameManager>.Instance.Player.FriendPresentReceiveList.Clear();
            this.InitializeReceiveList();
            goto Label_011B;
        Label_010A:
            if (pinId != 180)
            {
                goto Label_011B;
            }
            this.InitializeSendList();
        Label_011B:
            return -1;
        }

        public override void Release()
        {
            this.ReleaseWantList();
            this.ReleaseReceiveList();
            this.ReleaseSendList();
            base.Release();
            m_Instance = null;
            return;
        }

        public void ReleaseReceiveList()
        {
            if ((this.m_ReceiveController != null) == null)
            {
                goto Label_001C;
            }
            this.m_ReceiveController.Release();
        Label_001C:
            this.m_ReceiveSource = null;
            return;
        }

        public void ReleaseSendList()
        {
            if ((this.m_SendController != null) == null)
            {
                goto Label_001C;
            }
            this.m_SendController.Release();
        Label_001C:
            this.m_SendSource = null;
            return;
        }

        public void ReleaseWantList()
        {
            if ((this.m_WantController != null) == null)
            {
                goto Label_001C;
            }
            this.m_WantController.Release();
        Label_001C:
            this.m_WantSource = null;
            return;
        }

        public static void SetSendStatus(SendStatus status)
        {
            m_SendStatus = status;
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
            if ((this.m_Param.tabReceive != null) == null)
            {
                goto Label_0050;
            }
            this.m_Param.tabReceive.SetActive(0);
        Label_0050:
            if ((this.m_Param.tabSend != null) == null)
            {
                goto Label_0122;
            }
            this.m_Param.tabSend.SetActive(0);
            goto Label_0122;
        Label_007C:
            if ((this.m_Param.tabReceive != null) == null)
            {
                goto Label_00A3;
            }
            this.m_Param.tabReceive.SetActive(1);
        Label_00A3:
            if ((this.m_Param.tabSend != null) == null)
            {
                goto Label_0122;
            }
            this.m_Param.tabSend.SetActive(0);
            goto Label_0122;
        Label_00CF:
            if ((this.m_Param.tabReceive != null) == null)
            {
                goto Label_00F6;
            }
            this.m_Param.tabReceive.SetActive(0);
        Label_00F6:
            if ((this.m_Param.tabSend != null) == null)
            {
                goto Label_0122;
            }
            this.m_Param.tabSend.SetActive(1);
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
            return 190;
        Label_002A:
            num = CriticalSection.GetActive();
            if ((this.m_ReceiveToggle != null) == null)
            {
                goto Label_0050;
            }
            this.m_ReceiveToggle.set_interactable(num == 0);
        Label_0050:
            if ((this.m_SendToggle != null) == null)
            {
                goto Label_0070;
            }
            this.m_SendToggle.set_interactable(num == 0);
        Label_0070:
            return -1;
        }

        public override string name
        {
            get
            {
                return "FriendPresentRootWindow";
            }
        }

        public static FriendPresentRootWindow instance
        {
            get
            {
                return m_Instance;
            }
        }

        public static class ReceiveContent
        {
            public static ItemAccessor clickItem;

            static ReceiveContent()
            {
            }

            public class ItemAccessor
            {
                private ContentNode m_Node;
                private FriendPresentReceiveList.Param m_Param;
                private FriendPresentItemIcon m_Icon;
                private SerializeValueBehaviour m_Value;

                public ItemAccessor()
                {
                    base..ctor();
                    return;
                }

                public void Bind(ContentNode node)
                {
                    this.m_Node = node;
                    this.m_Icon = this.m_Node.GetComponent<FriendPresentItemIcon>();
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_003B;
                    }
                    this.m_Icon.Bind(this.present, 0);
                Label_003B:
                    this.m_Value = this.m_Node.GetComponent<SerializeValueBehaviour>();
                    if ((this.m_Value != null) == null)
                    {
                        goto Label_007D;
                    }
                    this.m_Value.list.SetField("num", this.m_Param.num);
                Label_007D:
                    return;
                }

                public void Clear()
                {
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_0023;
                    }
                    this.m_Icon.Clear();
                    this.m_Icon = null;
                Label_0023:
                    this.m_Value = null;
                    this.m_Node = null;
                    return;
                }

                public void ForceUpdate()
                {
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_001C;
                    }
                    this.m_Icon.Refresh();
                Label_001C:
                    return;
                }

                public void Setup(FriendPresentReceiveList.Param param)
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

                public FriendPresentReceiveList.Param param
                {
                    get
                    {
                        return this.m_Param;
                    }
                }

                public FriendPresentItemParam present
                {
                    get
                    {
                        return this.m_Param.present;
                    }
                }

                public FriendPresentItemIcon icon
                {
                    get
                    {
                        return this.m_Icon;
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
                [CompilerGenerated]
                private static Func<ItemParam, bool> <>f__am$cache1;

                public ItemSource()
                {
                    this.m_Params = new List<ItemParam>();
                    base..ctor();
                    return;
                }

                [CompilerGenerated]
                private static bool <Setup>m__31F(ItemParam prop)
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
                    <>f__am$cache1 = new Func<ItemParam, bool>(FriendPresentRootWindow.ReceiveContent.ItemSource.<Setup>m__31F);
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
                    private FriendPresentRootWindow.ReceiveContent.ItemAccessor m_Accessor;

                    public ItemParam(FriendPresentReceiveList.Param param)
                    {
                        this.m_Accessor = new FriendPresentRootWindow.ReceiveContent.ItemAccessor();
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

                    public FriendPresentRootWindow.ReceiveContent.ItemAccessor accerror
                    {
                        get
                        {
                            return this.m_Accessor;
                        }
                    }

                    public FriendPresentReceiveList.Param param
                    {
                        get
                        {
                            return this.m_Accessor.param;
                        }
                    }

                    public FriendPresentItemParam present
                    {
                        get
                        {
                            return this.m_Accessor.present;
                        }
                    }
                }
            }
        }

        public static class SendContent
        {
            public static ItemAccessor clickItem;

            static SendContent()
            {
            }

            public class ItemAccessor
            {
                private ContentNode m_Node;
                private FriendData m_Friend;
                private FriendPresentItemParam m_Present;
                private FriendPresentItemIcon m_Icon;
                private DataSource m_DataSource;

                public ItemAccessor()
                {
                    base..ctor();
                    return;
                }

                public void Bind(ContentNode node)
                {
                    this.m_Node = node;
                    this.m_DataSource = DataSource.Create(node.get_gameObject());
                    this.m_DataSource.Add(typeof(FriendData), this.m_Friend);
                    this.m_DataSource.Add(typeof(UnitData), this.m_Friend.Unit);
                    this.m_Icon = this.m_Node.GetComponent<FriendPresentItemIcon>();
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_0087;
                    }
                    this.m_Icon.Bind(this.present, 0);
                Label_0087:
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
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_0046;
                    }
                    this.m_Icon.Clear();
                    this.m_Icon = null;
                Label_0046:
                    this.m_Node = null;
                    return;
                }

                public void ForceUpdate()
                {
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_001C;
                    }
                    this.m_Icon.Refresh();
                Label_001C:
                    return;
                }

                public void Setup(FriendData friend)
                {
                    this.m_Friend = friend;
                    if (string.IsNullOrEmpty(friend.Wish) == null)
                    {
                        goto Label_0027;
                    }
                    this.m_Present = FriendPresentItemParam.DefaultParam;
                    goto Label_0042;
                Label_0027:
                    this.m_Present = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParam(friend.Wish);
                Label_0042:
                    return;
                }

                public ContentNode node
                {
                    get
                    {
                        return this.m_Node;
                    }
                }

                public FriendData friend
                {
                    get
                    {
                        return this.m_Friend;
                    }
                }

                public FriendPresentItemParam present
                {
                    get
                    {
                        return this.m_Present;
                    }
                }

                public FriendPresentItemIcon icon
                {
                    get
                    {
                        return this.m_Icon;
                    }
                }

                public bool isValid
                {
                    get
                    {
                        if (this.m_Friend == null)
                        {
                            goto Label_0018;
                        }
                        if (this.m_Present == null)
                        {
                            goto Label_0018;
                        }
                        return 1;
                    Label_0018:
                        return 0;
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
                private static bool <Setup>m__320(ItemParam prop)
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
                    <>f__am$cache1 = new Func<ItemParam, bool>(FriendPresentRootWindow.SendContent.ItemSource.<Setup>m__320);
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
                    private FriendPresentRootWindow.SendContent.ItemAccessor m_Accessor;

                    public ItemParam(FriendData friend)
                    {
                        this.m_Accessor = new FriendPresentRootWindow.SendContent.ItemAccessor();
                        base..ctor();
                        this.m_Accessor.Setup(friend);
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

                    public FriendPresentRootWindow.SendContent.ItemAccessor accerror
                    {
                        get
                        {
                            return this.m_Accessor;
                        }
                    }

                    public FriendData friend
                    {
                        get
                        {
                            return this.m_Accessor.friend;
                        }
                    }

                    public FriendPresentItemParam present
                    {
                        get
                        {
                            return this.m_Accessor.present;
                        }
                    }
                }
            }
        }

        public enum SendStatus
        {
            NONE,
            UNSENT,
            SENDING,
            SENTFAILED,
            SENDED
        }

        [Serializable]
        public class SerializeParam : FlowWindowBase.SerializeParamBase
        {
            public GameObject tabReceive;
            public GameObject tabSend;
            public GameObject wantList;
            public GameObject receiveList;
            public GameObject sendList;

            public SerializeParam()
            {
                base..ctor();
                return;
            }

            public override Type type
            {
                get
                {
                    return typeof(FriendPresentRootWindow);
                }
            }
        }

        public enum Tab
        {
            NONE,
            RECEIVE,
            SEND
        }

        public static class WantContent
        {
            public static ItemAccessor clickItem;

            static WantContent()
            {
            }

            public class ItemAccessor
            {
                private ContentNode m_Node;
                private FriendPresentItemParam m_Present;
                private FriendPresentItemIcon m_Icon;

                public ItemAccessor()
                {
                    base..ctor();
                    return;
                }

                public void Bind(ContentNode node)
                {
                    this.m_Node = node;
                    this.m_Icon = this.m_Node.GetComponent<FriendPresentItemIcon>();
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_0061;
                    }
                    if (this.present == null)
                    {
                        goto Label_004B;
                    }
                    this.m_Icon.Bind(this.present, 1);
                    goto Label_0061;
                Label_004B:
                    this.m_Icon.Clear();
                    this.m_Icon.Refresh();
                Label_0061:
                    return;
                }

                public void Clear()
                {
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_0023;
                    }
                    this.m_Icon.Clear();
                    this.m_Icon = null;
                Label_0023:
                    this.m_Node = null;
                    return;
                }

                public void ForceUpdate()
                {
                    if ((this.m_Icon != null) == null)
                    {
                        goto Label_001C;
                    }
                    this.m_Icon.Refresh();
                Label_001C:
                    return;
                }

                public void Setup(FriendPresentItemParam present)
                {
                    this.m_Present = present;
                    return;
                }

                public ContentNode node
                {
                    get
                    {
                        return this.m_Node;
                    }
                }

                public FriendPresentItemParam present
                {
                    get
                    {
                        return this.m_Present;
                    }
                }

                public FriendPresentItemIcon icon
                {
                    get
                    {
                        return this.m_Icon;
                    }
                }

                public int priority
                {
                    get
                    {
                        return (((this.m_Node != null) == null) ? 0 : this.m_Node.index);
                    }
                }

                public bool isValid
                {
                    get
                    {
                        return 1;
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
                private static bool <Setup>m__31E(ItemParam prop)
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
                    <>f__am$cache1 = new Func<ItemParam, bool>(FriendPresentRootWindow.WantContent.ItemSource.<Setup>m__31E);
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
                    private FriendPresentRootWindow.WantContent.ItemAccessor m_Accessor;

                    public ItemParam(FriendPresentItemParam present)
                    {
                        this.m_Accessor = new FriendPresentRootWindow.WantContent.ItemAccessor();
                        base..ctor();
                        this.m_Accessor.Setup(present);
                        return;
                    }

                    public override bool IsValid()
                    {
                        return this.m_Accessor.isValid;
                    }

                    public override void OnClick(ContentNode node)
                    {
                        FriendPresentRootWindow.WantContent.clickItem = this.m_Accessor;
                        ButtonEvent.Invoke("FRIENDPRESENT_WANTLIST_OPEN", node);
                        return;
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

                    public FriendPresentRootWindow.WantContent.ItemAccessor accerror
                    {
                        get
                        {
                            return this.m_Accessor;
                        }
                    }

                    public FriendPresentItemParam present
                    {
                        get
                        {
                            return this.m_Accessor.present;
                        }
                    }
                }
            }
        }
    }
}

