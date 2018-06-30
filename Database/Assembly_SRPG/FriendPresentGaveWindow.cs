namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class FriendPresentGaveWindow : FlowWindowBase
    {
        private Content.ItemSource m_ContentSource;
        private ContentController m_ContentController;
        private List<string> m_FriendUidList;
        private static FriendPresentGaveWindow m_Instance;

        public FriendPresentGaveWindow()
        {
            this.m_FriendUidList = new List<string>();
            base..ctor();
            return;
        }

        public void AddUid(string uid)
        {
            this.m_FriendUidList.Add(uid);
            return;
        }

        public void ClearFuids()
        {
            this.m_FriendUidList.Clear();
            return;
        }

        public override void Initialize(FlowWindowBase.SerializeParamBase param)
        {
            SerializeParam param2;
            m_Instance = this;
            base.Initialize(param);
            param2 = param as SerializeParam;
            if (param2 != null)
            {
                goto Label_0030;
            }
            throw new Exception(this.ToString() + " > Failed serializeParam null.");
        Label_0030:
            if ((param2.list != null) == null)
            {
                goto Label_006F;
            }
            this.m_ContentController = param2.list.GetComponentInChildren<ContentController>();
            if ((this.m_ContentController != null) == null)
            {
                goto Label_006F;
            }
            this.m_ContentController.SetWork(this);
        Label_006F:
            base.Close(1);
            return;
        }

        public void InitializeContentList()
        {
            int num;
            Content.ItemSource.ItemParam param;
            this.ReleaseContentList();
            if ((this.m_ContentController != null) == null)
            {
                goto Label_007D;
            }
            this.m_ContentSource = new Content.ItemSource();
            num = 0;
            goto Label_0056;
        Label_0029:
            param = new Content.ItemSource.ItemParam(this.m_FriendUidList[num]);
            if (param.IsValid() == null)
            {
                goto Label_0052;
            }
            this.m_ContentSource.Add(param);
        Label_0052:
            num += 1;
        Label_0056:
            if (num < this.m_FriendUidList.Count)
            {
                goto Label_0029;
            }
            this.m_ContentController.Initialize(this.m_ContentSource, Vector2.get_zero());
        Label_007D:
            return;
        }

        public override int OnActivate(int pinId)
        {
            if (pinId != 300)
            {
                goto Label_001C;
            }
            this.InitializeContentList();
            base.Open();
            goto Label_002E;
        Label_001C:
            if (pinId != 310)
            {
                goto Label_002E;
            }
            base.Close(0);
        Label_002E:
            return -1;
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
            return;
        }

        public override int Update()
        {
            base.Update();
            if (base.isClosed == null)
            {
                goto Label_001F;
            }
            base.SetActiveChild(0);
            return 390;
        Label_001F:
            return -1;
        }

        public override string name
        {
            get
            {
                return "FriendPresentGaveWindow";
            }
        }

        public static FriendPresentGaveWindow instance
        {
            get
            {
                return m_Instance;
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
                private FriendData m_FriendData;

                public ItemAccessor()
                {
                    base..ctor();
                    return;
                }

                public void Bind(ContentNode node)
                {
                    SerializeValueBehaviour behaviour;
                    this.m_Node = node;
                    behaviour = this.m_Node.GetComponent<SerializeValueBehaviour>();
                    if ((behaviour != null) == null)
                    {
                        goto Label_003A;
                    }
                    behaviour.list.SetField("name", this.m_FriendData.PlayerName);
                Label_003A:
                    return;
                }

                public void Clear()
                {
                    this.m_Node = null;
                    return;
                }

                public void ForceUpdate()
                {
                }

                public void Setup(string uid)
                {
                    PlayerData data;
                    <Setup>c__AnonStorey33C storeyc;
                    storeyc = new <Setup>c__AnonStorey33C();
                    storeyc.uid = uid;
                    data = MonoSingleton<GameManager>.Instance.Player;
                    this.m_FriendData = data.Friends.Find(new Predicate<FriendData>(storeyc.<>m__31B));
                    return;
                }

                public ContentNode node
                {
                    get
                    {
                        return this.m_Node;
                    }
                }

                public FriendData friendData
                {
                    get
                    {
                        return this.m_FriendData;
                    }
                }

                public bool isValid
                {
                    get
                    {
                        return ((this.m_FriendData == null) == 0);
                    }
                }

                [CompilerGenerated]
                private sealed class <Setup>c__AnonStorey33C
                {
                    internal string uid;

                    public <Setup>c__AnonStorey33C()
                    {
                        base..ctor();
                        return;
                    }

                    internal bool <>m__31B(FriendData prop)
                    {
                        return (prop.UID == this.uid);
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
                private static bool <Setup>m__31C(ItemParam prop)
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
                    <>f__am$cache1 = new Func<ItemParam, bool>(FriendPresentGaveWindow.Content.ItemSource.<Setup>m__31C);
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
                    private FriendPresentGaveWindow.Content.ItemAccessor m_Accessor;

                    public ItemParam(string uid)
                    {
                        this.m_Accessor = new FriendPresentGaveWindow.Content.ItemAccessor();
                        base..ctor();
                        this.m_Accessor.Setup(uid);
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

                    public FriendPresentGaveWindow.Content.ItemAccessor accerror
                    {
                        get
                        {
                            return this.m_Accessor;
                        }
                    }

                    public FriendData friendData
                    {
                        get
                        {
                            return this.m_Accessor.friendData;
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
                    return typeof(FriendPresentGaveWindow);
                }
            }
        }
    }
}

