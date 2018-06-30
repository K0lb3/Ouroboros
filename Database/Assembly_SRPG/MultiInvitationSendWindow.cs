namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class MultiInvitationSendWindow : FlowWindowBase
    {
        private const int CHECK_MAX = 5;
        private SerializeParam m_Param;
        private bool m_Destroy;
        private Content.ItemSource m_ContentSource;
        private ContentController m_ContentController;
        private SerializeValueBehaviour m_ValueList;
        private List<string> m_SendList;
        private static MultiInvitationSendWindow m_Instance;
        private static List<string> m_Invited;
        [CompilerGenerated]
        private static Comparison<FriendData> <>f__am$cache8;

        static MultiInvitationSendWindow()
        {
            m_Instance = null;
            m_Invited = new List<string>();
            return;
        }

        public MultiInvitationSendWindow()
        {
            this.m_SendList = new List<string>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static unsafe int <InitializeContentList>m__36C(FriendData p1, FriendData p2)
        {
            long num;
            long num2;
            num = (p1.IsFavorite == null) ? p1.LastLogin : 0x7fffffffffffffffL;
            num2 = (p2.IsFavorite == null) ? p2.LastLogin : 0x7fffffffffffffffL;
            return &num.CompareTo(num2);
        }

        public static void AddInvited(string uid)
        {
            m_Invited.Add(uid);
            return;
        }

        public static void ClearInvited()
        {
            m_Invited.Clear();
            return;
        }

        public string[] GetSendList()
        {
            List<string> list;
            int num;
            list = new List<string>();
            num = 0;
            goto Label_0034;
        Label_000D:
            if (this.m_SendList[num] == null)
            {
                goto Label_0030;
            }
            list.Add(this.m_SendList[num]);
        Label_0030:
            num += 1;
        Label_0034:
            if (num < this.m_SendList.Count)
            {
                goto Label_000D;
            }
            return list.ToArray();
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
                goto Label_0077;
            }
            this.m_ValueList.list.SetField("checkmax", 5);
        Label_0077:
            if ((this.m_Param.list != null) == null)
            {
                goto Label_00C0;
            }
            this.m_ContentController = this.m_Param.list.GetComponentInChildren<ContentController>();
            if ((this.m_ContentController != null) == null)
            {
                goto Label_00C0;
            }
            this.m_ContentController.SetWork(this);
        Label_00C0:
            base.Close(1);
            return;
        }

        public void InitializeContentList()
        {
            List<FriendData> list;
            MyPhoton photon;
            List<MyPhoton.MyPlayer> list2;
            int num;
            int num2;
            int num3;
            bool flag;
            long num4;
            int num5;
            FriendData data;
            Content.ItemSource.ItemParam param;
            <InitializeContentList>c__AnonStorey360 storey;
            <InitializeContentList>c__AnonStorey361 storey2;
            this.ReleaseContentList();
            if ((this.m_ContentController != null) == null)
            {
                goto Label_022A;
            }
            this.m_ContentSource = new Content.ItemSource();
            list = new List<FriendData>(MonoSingleton<GameManager>.Instance.Player.Friends);
            photon = PunMonoSingleton<MyPhoton>.Instance;
            if ((photon != null) == null)
            {
                goto Label_00D9;
            }
            list2 = photon.GetRoomPlayerList();
            num = 0;
            goto Label_00CD;
        Label_0057:
            if (list2[num] == null)
            {
                goto Label_00C9;
            }
            if (string.IsNullOrEmpty(list2[num].json) != null)
            {
                goto Label_00C9;
            }
            storey = new <InitializeContentList>c__AnonStorey360();
            storey.param = JSON_MyPhotonPlayerParam.Parse(list2[num].json);
            if (storey.param == null)
            {
                goto Label_00C9;
            }
            num2 = list.FindIndex(new Predicate<FriendData>(storey.<>m__36A));
            if (num2 == -1)
            {
                goto Label_00C9;
            }
            list.RemoveAt(num2);
        Label_00C9:
            num += 1;
        Label_00CD:
            if (num < list2.Count)
            {
                goto Label_0057;
            }
        Label_00D9:
            num3 = 0;
            goto Label_0190;
        Label_00E1:
            storey2 = new <InitializeContentList>c__AnonStorey361();
            storey2.data = list[num3];
            flag = 0;
            if (storey2.data == null)
            {
                goto Label_0172;
            }
            if (m_Invited.FindIndex(new Predicate<string>(storey2.<>m__36B)) == -1)
            {
                goto Label_012B;
            }
            flag = 1;
            goto Label_016D;
        Label_012B:
            if (storey2.data.MultiPush != null)
            {
                goto Label_0144;
            }
            flag = 1;
            goto Label_016D;
        Label_0144:
            if ((TimeManager.GetUnixSec(DateTime.Now) - storey2.data.LastLogin) <= 0x15180L)
            {
                goto Label_0175;
            }
            flag = 1;
        Label_016D:
            goto Label_0175;
        Label_0172:
            flag = 1;
        Label_0175:
            if (flag == null)
            {
                goto Label_018A;
            }
            list.RemoveAt(num3);
            num3 -= 1;
        Label_018A:
            num3 += 1;
        Label_0190:
            if (num3 < list.Count)
            {
                goto Label_00E1;
            }
            if (<>f__am$cache8 != null)
            {
                goto Label_01B6;
            }
            <>f__am$cache8 = new Comparison<FriendData>(MultiInvitationSendWindow.<InitializeContentList>m__36C);
        Label_01B6:
            SortUtility.StableSort<FriendData>(list, <>f__am$cache8);
            list.Reverse();
            num5 = 0;
            goto Label_0207;
        Label_01CE:
            data = list[num5];
            if (data == null)
            {
                goto Label_0201;
            }
            param = new Content.ItemSource.ItemParam(data);
            if (param.IsValid() == null)
            {
                goto Label_0201;
            }
            this.m_ContentSource.Add(param);
        Label_0201:
            num5 += 1;
        Label_0207:
            if (num5 < list.Count)
            {
                goto Label_01CE;
            }
            this.m_ContentController.Initialize(this.m_ContentSource, Vector2.get_zero());
        Label_022A:
            return;
        }

        public bool IsSendList(string uid)
        {
            <IsSendList>c__AnonStorey362 storey;
            storey = new <IsSendList>c__AnonStorey362();
            storey.uid = uid;
            return ((this.m_SendList.FindIndex(new Predicate<string>(storey.<>m__36D)) == -1) == 0);
        }

        public override int OnActivate(int pinId)
        {
            SerializeValueList list;
            Content.ItemAccessor accessor;
            string str;
            bool flag;
            if (pinId != 100)
            {
                goto Label_001F;
            }
            this.InitializeContentList();
            this.RefreshUI();
            base.Open();
            goto Label_00CF;
        Label_001F:
            if (pinId != 110)
            {
                goto Label_003A;
            }
            this.m_Destroy = 1;
            base.Close(0);
            goto Label_00CF;
        Label_003A:
            if (pinId != 120)
            {
                goto Label_00BE;
            }
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_00CF;
            }
            accessor = list.GetDataSource<Content.ItemAccessor>("_self");
            if (accessor == null)
            {
                goto Label_00CF;
            }
            if (accessor.isValid == null)
            {
                goto Label_00CF;
            }
            str = accessor.friend.UID;
            if ((this.IsSendList(str) == 0) == null)
            {
                goto Label_00A5;
            }
            accessor.isOn = 1;
            this.m_SendList.Add(str);
            goto Label_00B9;
        Label_00A5:
            accessor.isOn = 0;
            this.m_SendList.Remove(str);
        Label_00B9:
            goto Label_00CF;
        Label_00BE:
            if (pinId != 130)
            {
                goto Label_00CF;
            }
            return 190;
        Label_00CF:
            return -1;
        }

        private void RefreshUI()
        {
            int num;
            int num2;
            int num3;
            Content.ItemSource.ItemParam param;
            <RefreshUI>c__AnonStorey35F storeyf;
            if (this.m_ContentSource == null)
            {
                goto Label_0158;
            }
            num = this.m_ContentSource.GetCount();
            if (this.m_SendList.Count < 5)
            {
                goto Label_00C2;
            }
            num2 = 0;
            goto Label_00B6;
        Label_002F:
            storeyf = new <RefreshUI>c__AnonStorey35F();
            storeyf.param = this.m_ContentSource.GetParam(num2) as Content.ItemSource.ItemParam;
            if (storeyf.param == null)
            {
                goto Label_00B2;
            }
            if (storeyf.param.IsValid() == null)
            {
                goto Label_00B2;
            }
            if (this.m_SendList.FindIndex(new Predicate<string>(storeyf.<>m__369)) != -1)
            {
                goto Label_00A0;
            }
            storeyf.param.accessor.SetHatch(0);
            goto Label_00B2;
        Label_00A0:
            storeyf.param.accessor.SetHatch(1);
        Label_00B2:
            num2 += 1;
        Label_00B6:
            if (num2 < num)
            {
                goto Label_002F;
            }
            goto Label_0103;
        Label_00C2:
            num3 = 0;
            goto Label_00FC;
        Label_00C9:
            param = this.m_ContentSource.GetParam(num3) as Content.ItemSource.ItemParam;
            if (param == null)
            {
                goto Label_00F8;
            }
            if (param.IsValid() == null)
            {
                goto Label_00F8;
            }
            param.accessor.SetHatch(1);
        Label_00F8:
            num3 += 1;
        Label_00FC:
            if (num3 < num)
            {
                goto Label_00C9;
            }
        Label_0103:
            if ((this.m_ValueList != null) == null)
            {
                goto Label_0158;
            }
            this.m_ValueList.list.SetField("checknum", this.m_SendList.Count);
            this.m_ValueList.list.SetInteractable("btn_invication", this.m_SendList.Count > 0);
        Label_0158:
            return;
        }

        public override void Release()
        {
            this.ReleaseContentList();
            base.Release();
            m_Instance = null;
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
            this.m_SendList.Clear();
            return;
        }

        public override int Update()
        {
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
            return 0xbf;
        Label_002A:
            this.RefreshUI();
            return -1;
        }

        public override string name
        {
            get
            {
                return "FriendInvicationSendWindow";
            }
        }

        public static MultiInvitationSendWindow instance
        {
            get
            {
                return m_Instance;
            }
        }

        [CompilerGenerated]
        private sealed class <InitializeContentList>c__AnonStorey360
        {
            internal JSON_MyPhotonPlayerParam param;

            public <InitializeContentList>c__AnonStorey360()
            {
                base..ctor();
                return;
            }

            internal bool <>m__36A(FriendData prop)
            {
                return (prop.UID == this.param.UID);
            }
        }

        [CompilerGenerated]
        private sealed class <InitializeContentList>c__AnonStorey361
        {
            internal FriendData data;

            public <InitializeContentList>c__AnonStorey361()
            {
                base..ctor();
                return;
            }

            internal bool <>m__36B(string prop)
            {
                return (prop == this.data.UID);
            }
        }

        [CompilerGenerated]
        private sealed class <IsSendList>c__AnonStorey362
        {
            internal string uid;

            public <IsSendList>c__AnonStorey362()
            {
                base..ctor();
                return;
            }

            internal bool <>m__36D(string prop)
            {
                return (prop == this.uid);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshUI>c__AnonStorey35F
        {
            internal MultiInvitationSendWindow.Content.ItemSource.ItemParam param;

            public <RefreshUI>c__AnonStorey35F()
            {
                base..ctor();
                return;
            }

            internal bool <>m__369(string prop)
            {
                return (prop == this.param.friend.UID);
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
                private ContentNode m_Node;
                private FriendData m_Friend;
                private DataSource m_DataSource;
                private Toggle m_Toggle;
                private GameObject m_Hatch;

                public ItemAccessor()
                {
                    base..ctor();
                    return;
                }

                public void Bind(ContentNode node)
                {
                    SerializeValueBehaviour behaviour;
                    bool flag;
                    this.m_Node = node;
                    this.m_DataSource = DataSource.Create(node.get_gameObject());
                    this.m_DataSource.Add(typeof(FriendData), this.m_Friend);
                    this.m_DataSource.Add(typeof(UnitData), this.m_Friend.Unit);
                    this.m_DataSource.Add(typeof(MultiInvitationSendWindow.Content.ItemAccessor), this);
                    behaviour = this.m_Node.GetComponent<SerializeValueBehaviour>();
                    if ((behaviour != null) == null)
                    {
                        goto Label_0128;
                    }
                    flag = (MultiInvitationSendWindow.instance == null) ? 0 : MultiInvitationSendWindow.instance.IsSendList(this.m_Friend.UID);
                    this.m_Toggle = behaviour.list.GetUIToggle("toggle");
                    if ((this.m_Toggle != null) == null)
                    {
                        goto Label_00DA;
                    }
                    this.m_Toggle.set_isOn(flag);
                Label_00DA:
                    this.m_Hatch = behaviour.list.GetGameObject("hatch");
                    if ((this.m_Hatch != null) == null)
                    {
                        goto Label_010D;
                    }
                    this.m_Hatch.SetActive(0);
                Label_010D:
                    behaviour.list.SetField("comment", this.m_Friend.MultiComment);
                Label_0128:
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
                    this.m_Node = null;
                    this.m_Toggle = null;
                    this.m_Hatch = null;
                    return;
                }

                public void ForceUpdate()
                {
                }

                public void SetHatch(bool value)
                {
                    if ((this.m_Hatch != null) == null)
                    {
                        goto Label_0020;
                    }
                    this.m_Hatch.SetActive(value == 0);
                Label_0020:
                    return;
                }

                public void Setup(FriendData friend)
                {
                    this.m_Friend = friend;
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

                public Toggle tgl
                {
                    get
                    {
                        return this.m_Toggle;
                    }
                }

                public bool isOn
                {
                    get
                    {
                        return (((this.m_Toggle != null) == null) ? 0 : this.m_Toggle.get_isOn());
                    }
                    set
                    {
                        if ((this.m_Toggle != null) == null)
                        {
                            goto Label_001D;
                        }
                        this.m_Toggle.set_isOn(value);
                    Label_001D:
                        return;
                    }
                }

                public bool isValid
                {
                    get
                    {
                        return ((this.m_Friend == null) == 0);
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
                private static bool <Setup>m__36E(ItemParam prop)
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
                    <>f__am$cache1 = new Func<ItemParam, bool>(MultiInvitationSendWindow.Content.ItemSource.<Setup>m__36E);
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
                    base.contentController.scroller.StopMovement();
                    return;
                }

                public class ItemParam : ContentSource.Param
                {
                    private MultiInvitationSendWindow.Content.ItemAccessor m_Accessor;

                    public ItemParam(FriendData friend)
                    {
                        this.m_Accessor = new MultiInvitationSendWindow.Content.ItemAccessor();
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

                    public MultiInvitationSendWindow.Content.ItemAccessor accessor
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
                }
            }
        }

        [Serializable]
        public class SerializeParam : FlowWindowBase.SerializeParamBase
        {
            public GameObject list;

            public SerializeParam()
            {
                base..ctor();
                return;
            }

            public override Type type
            {
                get
                {
                    return typeof(MultiInvitationSendWindow);
                }
            }
        }
    }
}

