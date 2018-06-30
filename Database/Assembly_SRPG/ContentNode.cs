namespace SRPG
{
    using System;
    using UnityEngine;

    public class ContentNode : MonoBehaviour
    {
        private RectTransform m_RectTransform;
        private ContentController m_ContentController;
        private int m_Index;
        private ContentSource.Param m_Param;
        private ContentGrid m_Grid;
        private Vector2 m_Pos;
        private bool m_ViewIn;

        public ContentNode()
        {
            this.m_Grid = ContentGrid.zero;
            this.m_Pos = Vector2.get_zero();
            base..ctor();
            return;
        }

        private void Awake()
        {
        }

        public virtual void Copy(ContentNode src)
        {
            this.m_ContentController = src.m_ContentController;
            this.m_Index = src.m_Index;
            this.m_Param = src.m_Param;
            this.m_Grid = src.m_Grid;
            this.m_Pos = src.m_Pos;
            return;
        }

        public ContentSource.Param GetParam()
        {
            return this.m_Param;
        }

        public T GetParam<T>() where T: ContentSource.Param
        {
            return (T) (this.m_Param as T);
        }

        public unsafe Vector2 GetPivotAnchoredPosition()
        {
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            vector = this.rectTransform.get_pivot();
            vector2 = this.rectTransform.get_sizeDelta();
            vector3 = this.rectTransform.get_anchoredPosition();
            &vector3.x += (0.5f - &vector.x) * &vector2.x;
            &vector3.y += (0.5f - &vector.y) * &vector2.y;
            return vector3;
        }

        public unsafe Vector2 GetPivotAnchoredPosition(Vector2 pos)
        {
            Vector2 vector;
            Vector2 vector2;
            vector = this.rectTransform.get_pivot();
            vector2 = this.rectTransform.get_sizeDelta();
            &pos.x += (0.5f - &vector.x) * &vector2.x;
            &pos.y += (0.5f - &vector.y) * &vector2.y;
            return pos;
        }

        public unsafe Vector2 GetWorldPos()
        {
            Vector2 vector;
            vector = this.m_ContentController.anchoredPosition;
            &vector.x += &this.m_Pos.x;
            &vector.y += &this.m_Pos.y;
            return vector;
        }

        public virtual void Initialize(ContentController controller)
        {
            RectTransform transform;
            this.m_ContentController = controller;
            transform = this.rectTransform;
            if ((transform != null) == null)
            {
                goto Label_0044;
            }
            transform.set_anchorMin(new Vector2(0f, 1f));
            transform.set_anchorMax(new Vector2(0f, 1f));
        Label_0044:
            this.m_ViewIn = 0;
            return;
        }

        public bool IsInvalid()
        {
            if (this.m_Param == null)
            {
                goto Label_001A;
            }
            return (this.m_Param.IsValid() == 0);
        Label_001A:
            return 1;
        }

        public bool IsLock()
        {
            if (this.m_Param == null)
            {
                goto Label_0017;
            }
            return this.m_Param.IsLock();
        Label_0017:
            return 1;
        }

        public bool IsReMake()
        {
            if (this.m_Param == null)
            {
                goto Label_0017;
            }
            return this.m_Param.IsReMake();
        Label_0017:
            return 1;
        }

        public bool IsValid()
        {
            if (this.m_Param == null)
            {
                goto Label_0017;
            }
            return this.m_Param.IsValid();
        Label_0017:
            return 0;
        }

        public bool IsViewIn()
        {
            return this.m_ViewIn;
        }

        public bool IsViewOut()
        {
            return (this.m_ViewIn == 0);
        }

        public virtual void LateUpdate()
        {
            if (this.m_Param == null)
            {
                goto Label_0016;
            }
            this.m_Param.LateUpdate();
        Label_0016:
            return;
        }

        public unsafe Vector2 LocalPosToPos(Vector2 pos)
        {
            Vector2 vector;
            Vector2 vector2;
            vector = this.rectTransform.get_pivot();
            vector2 = this.rectTransform.get_sizeDelta();
            &pos.x -= &vector.x * &vector2.x;
            &pos.y += (1f - &vector.y) * &vector2.y;
            return pos;
        }

        public virtual void OnClick()
        {
            if (this.m_Param == null)
            {
                goto Label_0017;
            }
            this.m_Param.OnClick(this);
        Label_0017:
            return;
        }

        public virtual void OnDisable()
        {
            if (this.m_Param == null)
            {
                goto Label_002E;
            }
            this.m_Param.OnDisable(this);
            if (this.m_Param.IsValid() != null)
            {
                goto Label_002E;
            }
            this.m_Param = null;
        Label_002E:
            return;
        }

        public virtual void OnEnable()
        {
            if (this.m_Param == null)
            {
                goto Label_003E;
            }
            if (this.m_Param.IsValid() != null)
            {
                goto Label_0027;
            }
            this.m_Param = null;
            goto Label_003E;
        Label_0027:
            this.m_Param.Wakeup();
            this.m_Param.OnEnable(this);
        Label_003E:
            return;
        }

        public virtual void OnSelectOff()
        {
            if (this.m_Param == null)
            {
                goto Label_0017;
            }
            this.m_Param.OnSelectOff(this);
        Label_0017:
            return;
        }

        public virtual void OnSelectOn()
        {
            if (this.m_Param == null)
            {
                goto Label_0017;
            }
            this.m_Param.OnSelectOn(this);
        Label_0017:
            return;
        }

        public virtual void OnViewIn(Vector2 pivotViewPosition)
        {
            this.m_ViewIn = 1;
            if (this.m_Param == null)
            {
                goto Label_001F;
            }
            this.m_Param.OnViewIn(this, pivotViewPosition);
        Label_001F:
            return;
        }

        public virtual void OnViewOut(Vector2 pivotViewPosition)
        {
            this.m_ViewIn = 0;
            if (this.m_Param == null)
            {
                goto Label_001F;
            }
            this.m_Param.OnViewOut(this, pivotViewPosition);
        Label_001F:
            return;
        }

        public unsafe Vector2 PosToLocalPos(Vector2 pos)
        {
            Vector2 vector;
            Vector2 vector2;
            vector = this.rectTransform.get_pivot();
            vector2 = this.rectTransform.get_sizeDelta();
            &pos.x += &vector.x * &vector2.x;
            &pos.y -= (1f - &vector.y) * &vector2.y;
            return pos;
        }

        public virtual void Release()
        {
            this.m_ContentController = null;
            return;
        }

        public void SetActive(bool value)
        {
            base.get_gameObject().SetActive(value);
            return;
        }

        public void SetGrid(ContentGrid grid)
        {
            this.m_Grid = grid;
            return;
        }

        public unsafe void SetGrid(int x, int y)
        {
            &this.m_Grid.x = x;
            &this.m_Grid.y = y;
            return;
        }

        public void SetParam(ContentSource.Param param)
        {
            this.m_Param = param;
            return;
        }

        public void SetPos(Vector2 pos)
        {
            this.m_Pos = pos;
            return;
        }

        public unsafe void SetPos(float x, float y)
        {
            &this.m_Pos.x = x;
            &this.m_Pos.y = y;
            return;
        }

        public virtual void Setup(int index, Vector2 pos, ContentSource.Param param)
        {
            this.m_Index = index;
            this.m_Param = param;
            this.m_Pos = pos;
            base.set_name("Node_" + ((int) this.m_Index));
            this.rectTransform.set_anchoredPosition(this.PosToLocalPos(this.m_Pos));
            this.m_ViewIn = 0;
            if (this.m_Param == null)
            {
                goto Label_0065;
            }
            this.m_Param.OnSetup(this);
        Label_0065:
            return;
        }

        public virtual void Setup(int index, int x, int y, ContentSource.Param param)
        {
            this.m_Grid = new ContentGrid(x, y);
            this.Setup(index, this.m_ContentController.GetNodePos(x, y), param);
            return;
        }

        public virtual void Update()
        {
            if (this.m_Param == null)
            {
                goto Label_0016;
            }
            this.m_Param.Update();
        Label_0016:
            return;
        }

        public void UpdateAnchoredPos(Vector2 pos)
        {
            this.m_Pos = pos;
            this.rectTransform.set_anchoredPosition(pos);
            return;
        }

        public void UpdateLocalPos(Vector2 pos)
        {
            this.m_Pos = pos;
            this.rectTransform.set_localPosition(pos);
            return;
        }

        public RectTransform rectTransform
        {
            get
            {
                if ((this.m_RectTransform == null) == null)
                {
                    goto Label_0022;
                }
                this.m_RectTransform = base.get_gameObject().GetComponent<RectTransform>();
            Label_0022:
                return this.m_RectTransform;
            }
        }

        public ContentController contentController
        {
            get
            {
                return this.m_ContentController;
            }
        }

        public int index
        {
            get
            {
                return this.m_Index;
            }
        }

        public float sizeX
        {
            get
            {
                Vector2 vector;
                return &this.rectTransform.get_sizeDelta().x;
            }
        }

        public float sizeY
        {
            get
            {
                Vector2 vector;
                return &this.rectTransform.get_sizeDelta().y;
            }
        }

        public float posX
        {
            get
            {
                return &this.m_Pos.x;
            }
        }

        public float posY
        {
            get
            {
                return &this.m_Pos.y;
            }
        }

        public int gridX
        {
            get
            {
                return &this.m_Grid.x;
            }
        }

        public int gridY
        {
            get
            {
                return &this.m_Grid.y;
            }
        }

        public enum EventType
        {
            SETUP,
            ENABLE,
            DISABLE
        }
    }
}

