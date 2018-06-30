namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class FlowWindowController
    {
        private bool m_Enabled;
        private FlowNode m_FlowNode;
        private List<FlowWindowBase> m_List;

        public FlowWindowController()
        {
            this.m_Enabled = 1;
            this.m_List = new List<FlowWindowBase>();
            base..ctor();
            return;
        }

        public void ActivateOutputLinks(int pinId)
        {
            if (this.enabled != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.m_FlowNode != null) == null)
            {
                goto Label_002A;
            }
            this.m_FlowNode.ActivateOutputLinks(pinId);
        Label_002A:
            return;
        }

        public void Add(FlowWindowBase.SerializeParamBase param)
        {
            FlowWindowBase base2;
            if ((param.window != null) == null)
            {
                goto Label_003B;
            }
            base2 = Activator.CreateInstance(param.type) as FlowWindowBase;
            if (base2 == null)
            {
                goto Label_003B;
            }
            base2.Initialize(param);
            this.m_List.Add(base2);
        Label_003B:
            return;
        }

        public T GetWindow<T>() where T: FlowWindowBase
        {
            int num;
            num = 0;
            goto Label_0038;
        Label_0007:
            if ((this.m_List[num] as T) == null)
            {
                goto Label_0034;
            }
            return (T) (this.m_List[num] as T);
        Label_0034:
            num += 1;
        Label_0038:
            if (num < this.m_List.Count)
            {
                goto Label_0007;
            }
            return (T) null;
        }

        public FlowWindowBase GetWindow(string name)
        {
            int num;
            num = 0;
            goto Label_0034;
        Label_0007:
            if ((this.m_List[num].name == name) == null)
            {
                goto Label_0030;
            }
            return this.m_List[num];
        Label_0030:
            num += 1;
        Label_0034:
            if (num < this.m_List.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public FlowWindowBase GetWindow(Type type)
        {
            int num;
            num = 0;
            goto Label_002F;
        Label_0007:
            if (this.m_List[num].GetType() != type)
            {
                goto Label_002B;
            }
            return this.m_List[num];
        Label_002B:
            num += 1;
        Label_002F:
            if (num < this.m_List.Count)
            {
                goto Label_0007;
            }
            return null;
        }

        public void Initialize(FlowNode node)
        {
            this.m_FlowNode = node;
            return;
        }

        public bool IsStartUp()
        {
            int num;
            num = 0;
            goto Label_0023;
        Label_0007:
            if (this.m_List[num].IsStartUp() != null)
            {
                goto Label_001F;
            }
            return 0;
        Label_001F:
            num += 1;
        Label_0023:
            if (num < this.m_List.Count)
            {
                goto Label_0007;
            }
            return 1;
        }

        public void LateUpdate()
        {
            int num;
            num = 0;
            goto Label_0022;
        Label_0007:
            this.m_List[num].LateUpdate(this.m_FlowNode);
            num += 1;
        Label_0022:
            if (num < this.m_List.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void OnActivate(int pinId)
        {
            int num;
            if (this.enabled != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_0029;
        Label_0013:
            this.m_List[num].RequestPin(pinId);
            num += 1;
        Label_0029:
            if (num < this.m_List.Count)
            {
                goto Label_0013;
            }
            return;
        }

        public void Release()
        {
            int num;
            num = 0;
            goto Label_001C;
        Label_0007:
            this.m_List[num].Release();
            num += 1;
        Label_001C:
            if (num < this.m_List.Count)
            {
                goto Label_0007;
            }
            this.m_List.Clear();
            return;
        }

        public void Remove(FlowWindowBase window)
        {
            window.Release();
            this.m_List.Remove(window);
            return;
        }

        public void Start()
        {
            int num;
            num = 0;
            goto Label_001C;
        Label_0007:
            this.m_List[num].Start();
            num += 1;
        Label_001C:
            if (num < this.m_List.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void Update()
        {
            int num;
            num = 0;
            goto Label_001D;
        Label_0007:
            this.m_List[num].Update(this);
            num += 1;
        Label_001D:
            if (num < this.m_List.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public bool enabled
        {
            get
            {
                return this.m_Enabled;
            }
            set
            {
                this.m_Enabled = value;
                return;
            }
        }
    }
}

