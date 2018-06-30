namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(2, "Refresh", 0, 1), AddComponentMenu("UI/リスト/アイテム"), Pin(100, "詳細表示", 1, 100), Pin(1, "Start", 0, 1)]
    public class ItemListWindow : MonoBehaviour, IFlowInterface
    {
        public GameObject ItemTemplate;
        public Toggle ToggleShowUsed;
        public Toggle ToggleShowEquip;
        public Toggle ToggleShowMaterial;
        public Toggle ToggleShowEvolMaterial;
        public Toggle ToggleShowUnitPiece;
        public Toggle ToggleShowArtifactPiece;
        public Toggle ToggleShowTicket;
        public Toggle ToggleShowOther;
        private ItemData SelectItem;
        private ItemSource m_ItemSource;
        private ContentController m_ContenController;

        public ItemListWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        private void Awake()
        {
            if ((this.ToggleShowUsed != null) == null)
            {
                goto Label_002D;
            }
            this.ToggleShowUsed.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowUsed));
        Label_002D:
            if ((this.ToggleShowEquip != null) == null)
            {
                goto Label_005A;
            }
            this.ToggleShowEquip.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowEquip));
        Label_005A:
            if ((this.ToggleShowMaterial != null) == null)
            {
                goto Label_0087;
            }
            this.ToggleShowMaterial.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowMaterial));
        Label_0087:
            if ((this.ToggleShowEvolMaterial != null) == null)
            {
                goto Label_00B4;
            }
            this.ToggleShowEvolMaterial.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowEvolMaterial));
        Label_00B4:
            if ((this.ToggleShowUnitPiece != null) == null)
            {
                goto Label_00E1;
            }
            this.ToggleShowUnitPiece.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowUnitPiece));
        Label_00E1:
            if ((this.ToggleShowArtifactPiece != null) == null)
            {
                goto Label_010E;
            }
            this.ToggleShowArtifactPiece.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowArtifactPiece));
        Label_010E:
            if ((this.ToggleShowTicket != null) == null)
            {
                goto Label_013B;
            }
            this.ToggleShowTicket.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowTicket));
        Label_013B:
            if ((this.ToggleShowOther != null) == null)
            {
                goto Label_0168;
            }
            this.ToggleShowOther.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnShowOther));
        Label_0168:
            this.m_ContenController = base.GetComponent<ContentController>();
            this.m_ContenController.SetWork(this);
            return;
        }

        private void OnDestroy()
        {
            if ((this.m_ContenController != null) == null)
            {
                goto Label_001C;
            }
            this.m_ContenController.Release();
        Label_001C:
            this.m_ContenController = null;
            this.m_ItemSource = null;
            return;
        }

        private void OnSelect(GameObject go)
        {
            ItemData data;
            data = DataSource.FindDataOfClass<ItemData>(go, null);
            if (data != null)
            {
                goto Label_000F;
            }
            return;
        Label_000F:
            GlobalVars.SelectedItemID = data.Param.iname;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void OnShowArtifactPiece(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.m_ItemSource == null)
            {
                goto Label_001E;
            }
            this.m_ItemSource.SelectType(6);
        Label_001E:
            return;
        }

        private void OnShowEquip(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.m_ItemSource == null)
            {
                goto Label_001E;
            }
            this.m_ItemSource.SelectType(2);
        Label_001E:
            return;
        }

        private void OnShowEvolMaterial(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.m_ItemSource == null)
            {
                goto Label_001E;
            }
            this.m_ItemSource.SelectType(4);
        Label_001E:
            return;
        }

        private void OnShowMaterial(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.m_ItemSource == null)
            {
                goto Label_001E;
            }
            this.m_ItemSource.SelectType(3);
        Label_001E:
            return;
        }

        private void OnShowOther(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.m_ItemSource == null)
            {
                goto Label_001E;
            }
            this.m_ItemSource.SelectType(8);
        Label_001E:
            return;
        }

        private void OnShowTicket(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.m_ItemSource == null)
            {
                goto Label_001E;
            }
            this.m_ItemSource.SelectType(7);
        Label_001E:
            return;
        }

        private void OnShowUnitPiece(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.m_ItemSource == null)
            {
                goto Label_001E;
            }
            this.m_ItemSource.SelectType(5);
        Label_001E:
            return;
        }

        private void OnShowUsed(bool isActive)
        {
            if (isActive != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if (this.m_ItemSource == null)
            {
                goto Label_001E;
            }
            this.m_ItemSource.SelectType(1);
        Label_001E:
            return;
        }

        public void SetupNodeEvent(ContentNode node)
        {
            ListItemEvents events;
            if ((node != null) == null)
            {
                goto Label_0031;
            }
            events = node.GetComponent<ListItemEvents>();
            if ((events != null) == null)
            {
                goto Label_0031;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
        Label_0031:
            return;
        }

        private void Start()
        {
            List<ItemData> list;
            int num;
            ItemData data;
            if ((this.m_ContenController != null) == null)
            {
                goto Label_0097;
            }
            this.m_ItemSource = new ItemSource();
            list = MonoSingleton<GameManager>.Instance.Player.Items;
            num = 0;
            goto Label_0075;
        Label_0033:
            data = list[num];
            if (data.Num != null)
            {
                goto Label_004B;
            }
            goto Label_0071;
        Label_004B:
            if (data.Param.CheckCanShowInList() != null)
            {
                goto Label_0060;
            }
            goto Label_0071;
        Label_0060:
            this.m_ItemSource.Add(new ItemSource.ItemParam(data));
        Label_0071:
            num += 1;
        Label_0075:
            if (num < list.Count)
            {
                goto Label_0033;
            }
            this.m_ContenController.Initialize(this.m_ItemSource, Vector2.get_zero());
        Label_0097:
            return;
        }

        private void Update()
        {
        }

        private class ItemNode : ContentNode
        {
            private DataSource m_DataSource;
            private GameParameter[] m_GameParameters;

            public ItemNode()
            {
                base..ctor();
                return;
            }

            public void ForceUpdate()
            {
                int num;
                GameParameter parameter;
                if (this.m_GameParameters == null)
                {
                    goto Label_003F;
                }
                num = 0;
                goto Label_0031;
            Label_0012:
                parameter = this.m_GameParameters[num];
                if ((parameter != null) == null)
                {
                    goto Label_002D;
                }
                parameter.UpdateValue();
            Label_002D:
                num += 1;
            Label_0031:
                if (num < ((int) this.m_GameParameters.Length))
                {
                    goto Label_0012;
                }
            Label_003F:
                return;
            }

            public override void Initialize(ContentController controller)
            {
                ItemListWindow window;
                base.Initialize(controller);
                this.m_DataSource = DataSource.Create(base.get_gameObject());
                this.m_GameParameters = base.get_gameObject().GetComponentsInChildren<GameParameter>();
                window = controller.GetWork() as ItemListWindow;
                if ((window != null) == null)
                {
                    goto Label_0048;
                }
                window.SetupNodeEvent(this);
            Label_0048:
                return;
            }

            private void OnSelect(GameObject go)
            {
                ItemData data;
                data = DataSource.FindDataOfClass<ItemData>(go, null);
                if (data != null)
                {
                    goto Label_000F;
                }
                return;
            Label_000F:
                GlobalVars.SelectedItemID = data.Param.iname;
                FlowNode_GameObject.ActivateOutputLinks(this, 100);
                return;
            }

            public override void Release()
            {
                base.Release();
                return;
            }

            public DataSource dataSource
            {
                get
                {
                    return this.m_DataSource;
                }
            }
        }

        public class ItemSource : ContentSource
        {
            private EItemTabType m_ItemType;
            private List<ItemParam> m_Params;

            public ItemSource()
            {
                this.m_Params = new List<ItemParam>();
                base..ctor();
                return;
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
                this.SelectType(1);
                return;
            }

            public override ContentNode Instantiate(ContentNode res)
            {
                GameObject obj2;
                obj2 = Object.Instantiate<GameObject>(res.get_gameObject());
                if ((obj2 != null) == null)
                {
                    goto Label_001F;
                }
                return obj2.AddComponent<ItemListWindow.ItemNode>();
            Label_001F:
                return null;
            }

            public override void Release()
            {
                base.Release();
                this.m_ItemType = 0;
                return;
            }

            public unsafe void SelectType(EItemTabType itemType)
            {
                bool flag;
                Vector2 vector;
                Vector2 vector2;
                <SelectType>c__AnonStorey352 storey;
                storey = new <SelectType>c__AnonStorey352();
                storey.itemType = itemType;
                if (this.m_ItemType == storey.itemType)
                {
                    goto Label_00E0;
                }
                this.Clear();
                base.SetTable(Enumerable.ToArray<ItemParam>(Enumerable.Where<ItemParam>(this.m_Params, new Func<ItemParam, bool>(storey.<>m__34E))));
                base.contentController.Resize(0);
                flag = 0;
                vector = base.contentController.anchoredPosition;
                vector2 = base.contentController.GetLastPageAnchorePos();
                if (&vector.x >= &vector2.x)
                {
                    goto Label_008F;
                }
                flag = 1;
                &vector.x = &vector2.x;
            Label_008F:
                if (&vector.y >= &vector2.y)
                {
                    goto Label_00B2;
                }
                flag = 1;
                &vector.y = &vector2.y;
            Label_00B2:
                if (flag == null)
                {
                    goto Label_00C4;
                }
                base.contentController.anchoredPosition = vector;
            Label_00C4:
                base.contentController.scroller.StopMovement();
                this.m_ItemType = storey.itemType;
            Label_00E0:
                return;
            }

            [CompilerGenerated]
            private sealed class <SelectType>c__AnonStorey352
            {
                internal EItemTabType itemType;

                public <SelectType>c__AnonStorey352()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__34E(ItemListWindow.ItemSource.ItemParam param)
                {
                    return (param.data.Param.tabtype == this.itemType);
                }
            }

            public class ItemParam : ContentSource.Param
            {
                private ItemData m_Item;

                public ItemParam(ItemData item)
                {
                    base..ctor();
                    this.m_Item = item;
                    return;
                }

                public override bool IsValid()
                {
                    return ((this.m_Item == null) == 0);
                }

                public override void OnSetup(ContentNode node)
                {
                    ItemListWindow.ItemNode node2;
                    node2 = node as ItemListWindow.ItemNode;
                    if ((node2 != null) == null)
                    {
                        goto Label_003F;
                    }
                    node2.dataSource.Clear();
                    node2.dataSource.Add(typeof(ItemData), this.m_Item);
                    node2.ForceUpdate();
                Label_003F:
                    return;
                }

                public ItemData data
                {
                    get
                    {
                        return this.m_Item;
                    }
                }

                public EItemType itemType
                {
                    get
                    {
                        return this.m_Item.ItemType;
                    }
                }
            }
        }
    }
}

