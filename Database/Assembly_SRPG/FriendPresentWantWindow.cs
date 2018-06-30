namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class FriendPresentWantWindow : FlowWindowBase
    {
        private Content.ItemSource m_ContentSource;
        private ContentController m_ContentController;

        public FriendPresentWantWindow()
        {
            base..ctor();
            return;
        }

        public override void Initialize(FlowWindowBase.SerializeParamBase param)
        {
            SerializeParam param2;
            base.Initialize(param);
            param2 = param as SerializeParam;
            if (param2 != null)
            {
                goto Label_002A;
            }
            throw new Exception(this.ToString() + " > Failed serializeParam null.");
        Label_002A:
            if ((param2.list != null) == null)
            {
                goto Label_0069;
            }
            this.m_ContentController = param2.list.GetComponentInChildren<ContentController>();
            if ((this.m_ContentController != null) == null)
            {
                goto Label_0069;
            }
            this.m_ContentController.SetWork(this);
        Label_0069:
            base.Close(1);
            return;
        }

        public void InitializeContentList()
        {
            FriendPresentItemParam[] paramArray;
            List<FriendPresentItemParam> list;
            int num;
            int num2;
            Content.ItemSource.ItemParam param;
            <InitializeContentList>c__AnonStorey33E storeye;
            this.ReleaseContentList();
            if ((this.m_ContentController != null) == null)
            {
                goto Label_00F3;
            }
            storeye = new <InitializeContentList>c__AnonStorey33E();
            this.m_ContentSource = new Content.ItemSource();
            paramArray = MonoSingleton<GameManager>.Instance.MasterParam.GetFriendPresentItemParams();
            list = new List<FriendPresentItemParam>();
            num = 0;
            goto Label_0077;
        Label_0046:
            if (paramArray[num].IsDefault() != null)
            {
                goto Label_0073;
            }
            if (paramArray[num].IsValid(Network.GetServerTime()) != null)
            {
                goto Label_006A;
            }
            goto Label_0073;
        Label_006A:
            list.Add(paramArray[num]);
        Label_0073:
            num += 1;
        Label_0077:
            if (num < ((int) paramArray.Length))
            {
                goto Label_0046;
            }
            storeye.serverTime = Network.GetServerTime();
            SortUtility.StableSort<FriendPresentItemParam>(list, new Comparison<FriendPresentItemParam>(storeye.<>m__321));
            num2 = 0;
            goto Label_00D1;
        Label_00A6:
            param = new Content.ItemSource.ItemParam(list[num2]);
            if (param.IsValid() == null)
            {
                goto Label_00CD;
            }
            this.m_ContentSource.Add(param);
        Label_00CD:
            num2 += 1;
        Label_00D1:
            if (num2 < list.Count)
            {
                goto Label_00A6;
            }
            this.m_ContentController.Initialize(this.m_ContentSource, Vector2.get_zero());
        Label_00F3:
            return;
        }

        public override int OnActivate(int pinId)
        {
            if (pinId != 200)
            {
                goto Label_001C;
            }
            this.InitializeContentList();
            base.Open();
            goto Label_002E;
        Label_001C:
            if (pinId != 210)
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
            return 290;
        Label_001F:
            return -1;
        }

        public override string name
        {
            get
            {
                return "FriendPresentWantWindow";
            }
        }

        [CompilerGenerated]
        private sealed class <InitializeContentList>c__AnonStorey33E
        {
            internal long serverTime;

            public <InitializeContentList>c__AnonStorey33E()
            {
                base..ctor();
                return;
            }

            internal unsafe int <>m__321(FriendPresentItemParam p1, FriendPresentItemParam p2)
            {
                long num;
                long num2;
                num = (p1.HasTimeLimit() == null) ? 0x7fffffffffffffffL : p1.GetRestTime(this.serverTime);
                num2 = (p2.HasTimeLimit() == null) ? 0x7fffffffffffffffL : p2.GetRestTime(this.serverTime);
                return &num.CompareTo(num2);
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
                        goto Label_003B;
                    }
                    this.m_Icon.Bind(this.present, 0);
                Label_003B:
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

                public string presentId
                {
                    get
                    {
                        return ((this.m_Present == null) ? "FP_DEFAULT" : this.m_Present.iname);
                    }
                }

                public bool isValid
                {
                    get
                    {
                        return ((this.m_Present == null) == 0);
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
                private static bool <Setup>m__322(ItemParam prop)
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
                    <>f__am$cache1 = new Func<ItemParam, bool>(FriendPresentWantWindow.Content.ItemSource.<Setup>m__322);
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
                    private FriendPresentWantWindow.Content.ItemAccessor m_Accessor;

                    public ItemParam(FriendPresentItemParam present)
                    {
                        this.m_Accessor = new FriendPresentWantWindow.Content.ItemAccessor();
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
                        FriendPresentWantWindow.Content.clickItem = this.m_Accessor;
                        ButtonEvent.Invoke("FRIENDPRESENT_WANTLIST_SELECT", node);
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

                    public FriendPresentWantWindow.Content.ItemAccessor accerror
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
                    return typeof(FriendPresentWantWindow);
                }
            }
        }
    }
}

