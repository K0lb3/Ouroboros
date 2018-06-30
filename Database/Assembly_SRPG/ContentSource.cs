namespace SRPG
{
    using System;
    using UnityEngine;

    public class ContentSource
    {
        private Param[] m_Table;
        private ContentController m_ContentController;

        public ContentSource()
        {
            base..ctor();
            return;
        }

        public virtual void Clear()
        {
            int num;
            int num2;
            Param param;
            num = 0;
            num2 = this.GetCount();
            goto Label_0026;
        Label_000E:
            param = this.GetParam(num);
            if (param == null)
            {
                goto Label_0022;
            }
            param.Release();
        Label_0022:
            num += 1;
        Label_0026:
            if (num < num2)
            {
                goto Label_000E;
            }
            this.m_Table = null;
            return;
        }

        public ContentController GetContentController()
        {
            return this.m_ContentController;
        }

        public virtual int GetCount()
        {
            if (this.m_Table == null)
            {
                goto Label_0014;
            }
            return (int) this.m_Table.Length;
        Label_0014:
            return 0;
        }

        public virtual Param GetParam(int index)
        {
            if (this.m_Table == null)
            {
                goto Label_0029;
            }
            if (index < 0)
            {
                goto Label_0029;
            }
            if (index >= ((int) this.m_Table.Length))
            {
                goto Label_0029;
            }
            return this.m_Table[index];
        Label_0029:
            return null;
        }

        public virtual Param GetParam(string value)
        {
            int num;
            Param param;
            if (this.m_Table == null)
            {
                goto Label_0041;
            }
            num = 0;
            goto Label_0033;
        Label_0012:
            param = this.m_Table[num];
            if (param == null)
            {
                goto Label_002F;
            }
            if (param.Equal(value) == null)
            {
                goto Label_002F;
            }
            return param;
        Label_002F:
            num += 1;
        Label_0033:
            if (num < ((int) this.m_Table.Length))
            {
                goto Label_0012;
            }
        Label_0041:
            return null;
        }

        public virtual void Initialize(ContentController controller)
        {
            int num;
            int num2;
            Param param;
            this.m_ContentController = controller;
            num = 0;
            num2 = this.GetCount();
            goto Label_002E;
        Label_0015:
            param = this.GetParam(num);
            if (param == null)
            {
                goto Label_002A;
            }
            param.Initialize(this);
        Label_002A:
            num += 1;
        Label_002E:
            if (num < num2)
            {
                goto Label_0015;
            }
            return;
        }

        public virtual ContentNode Instantiate(ContentNode res)
        {
            GameObject obj2;
            obj2 = Object.Instantiate<GameObject>(res.get_gameObject());
            if ((obj2 != null) == null)
            {
                goto Label_001F;
            }
            return obj2.GetComponent<ContentNode>();
        Label_001F:
            return null;
        }

        public virtual void Release()
        {
            this.Clear();
            return;
        }

        public void SetTable(Param[] values)
        {
            int num;
            if (values == null)
            {
                goto Label_002F;
            }
            num = 0;
            goto Label_001A;
        Label_000D:
            values[num].id = num;
            num += 1;
        Label_001A:
            if (num < ((int) values.Length))
            {
                goto Label_000D;
            }
            this.m_Table = values;
            goto Label_0036;
        Label_002F:
            this.m_Table = null;
        Label_0036:
            return;
        }

        public virtual void Update()
        {
        }

        protected ContentController contentController
        {
            get
            {
                return this.m_ContentController;
            }
        }

        public class Param
        {
            private int _id;
            private int _idprev;

            public Param()
            {
                this._id = -1;
                this._idprev = -2147483648;
                base..ctor();
                return;
            }

            public virtual bool Equal(string value)
            {
                return (this.ToString() == value);
            }

            public virtual void Initialize(ContentSource source)
            {
            }

            public virtual bool IsLock()
            {
                return 0;
            }

            public virtual bool IsReMake()
            {
                return ((this.id == this._idprev) == 0);
            }

            public virtual bool IsValid()
            {
                return 1;
            }

            public virtual void LateUpdate()
            {
            }

            public virtual void OnClick(ContentNode node)
            {
            }

            public virtual void OnDisable(ContentNode node)
            {
            }

            public virtual void OnEnable(ContentNode node)
            {
            }

            public virtual void OnPageFit(ContentNode node)
            {
            }

            public virtual void OnSelectOff(ContentNode node)
            {
            }

            public virtual void OnSelectOn(ContentNode node)
            {
            }

            public virtual void OnSetup(ContentNode node)
            {
            }

            public virtual void OnViewIn(ContentNode node, Vector2 pivotViewPosition)
            {
            }

            public virtual void OnViewOut(ContentNode node, Vector2 pivotViewPosition)
            {
            }

            public virtual void Release()
            {
                this._idprev = -2147483648;
                return;
            }

            public virtual void Update()
            {
            }

            public void Wakeup()
            {
                this._idprev = this.id;
                return;
            }

            public int id
            {
                get
                {
                    return this._id;
                }
                set
                {
                    this._id = value;
                    return;
                }
            }
        }
    }
}

