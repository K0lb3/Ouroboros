namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [ExecuteInEditMode]
    public class ContentController : MonoBehaviour
    {
        public bool m_WidthLoop;
        public bool m_HeightLoop;
        public ContentScroller m_Scroller;
        public ContentNode m_Node;
        public float m_PaddingLeft;
        public float m_PaddingRight;
        public float m_PaddingTop;
        public float m_PaddingBottom;
        public Vector2 m_CellSize;
        public Vector2 m_Spacing;
        public Constraint m_Constraint;
        public int m_ConstraintCount;
        private RectTransform m_RectTransform;
        private ContentSource m_Source;
        private bool m_NodeStatic;
        private Dictionary<string, ContentNode> m_NodeUsed;
        private List<ContentNode> m_NodeEmpty;
        private Vector2 m_PageSize;
        private int m_NodeWidthNum;
        private int m_NodeHeightNum;
        private int m_ViewWidthNum;
        private int m_ViewHeightNum;
        private int m_SelectNode;
        private float m_MoveRefreshTime;
        private bool m_MoveRefresh;
        private object m_Work;
        public Vector2 _test;

        public ContentController()
        {
            this.m_CellSize = Vector2.get_zero();
            this.m_Spacing = Vector2.get_zero();
            this.m_NodeUsed = new Dictionary<string, ContentNode>();
            this.m_NodeEmpty = new List<ContentNode>();
            this.m_PageSize = Vector2.get_zero();
            this.m_NodeWidthNum = 1;
            this.m_NodeHeightNum = 1;
            this.m_ViewWidthNum = 1;
            this.m_ViewHeightNum = 1;
            this.m_SelectNode = -1;
            this._test = Vector2.get_zero();
            base..ctor();
            return;
        }

        protected virtual void Awake()
        {
            this.m_RectTransform = base.get_gameObject().GetComponent<RectTransform>();
            if ((this.m_Scroller == null) == null)
            {
                goto Label_0033;
            }
            this.m_Scroller = base.get_gameObject().GetComponentInParent<ContentScroller>();
        Label_0033:
            if ((this.m_Node != null) == null)
            {
                goto Label_0061;
            }
            this.m_NodeStatic = 0;
            this.m_Node.get_gameObject().SetActive(0);
            goto Label_0074;
        Label_0061:
            this.m_NodeStatic = 1;
            this.Initialize(null, Vector2.get_zero());
        Label_0074:
            return;
        }

        private unsafe void CheckActiveNode()
        {
            ContentGrid grid;
            List<ContentNode> list;
            IDictionaryEnumerator enumerator;
            ContentNode node;
            int num;
            ContentNode node2;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            ContentNode node3;
            int num8;
            ContentSource.Param param;
            grid = this.GetGrid();
            list = new List<ContentNode>();
            enumerator = (Dictionary<string, ContentNode>.Enumerator) this.m_NodeUsed.GetEnumerator();
            goto Label_00B2;
        Label_0023:
            node = (ContentNode) enumerator.Value;
            if ((node != null) == null)
            {
                goto Label_00B2;
            }
            if (node.IsReMake() != null)
            {
                goto Label_00AB;
            }
            if (node.IsValid() == null)
            {
                goto Label_00AB;
            }
            if (node.gridX < (&grid.x - 1))
            {
                goto Label_00AB;
            }
            if (node.gridX > (&grid.x + this.m_NodeWidthNum))
            {
                goto Label_00AB;
            }
            if (node.gridY < (&grid.y - 1))
            {
                goto Label_00AB;
            }
            if (node.gridY <= (&grid.y + this.m_NodeHeightNum))
            {
                goto Label_00B2;
            }
        Label_00AB:
            list.Add(node);
        Label_00B2:
            if (enumerator.MoveNext() != null)
            {
                goto Label_0023;
            }
            num = 0;
            goto Label_011E;
        Label_00C5:
            node2 = list[num];
            if ((node2 != null) == null)
            {
                goto Label_0118;
            }
            node2.OnSelectOff();
            node2.SetActive(0);
            this.m_NodeUsed.Remove(this.GetNodeKey(node2.gridX, node2.gridY));
            this.m_NodeEmpty.Add(node2);
        Label_0118:
            num += 1;
        Label_011E:
            if (num < list.Count)
            {
                goto Label_00C5;
            }
            num2 = &grid.x;
            num3 = &grid.y;
            num4 = &grid.x + this.m_NodeWidthNum;
            num5 = &grid.y + this.m_NodeHeightNum;
            if (this.isScrollHorizontal == null)
            {
                goto Label_0174;
            }
            num2 -= 1;
            num4 += 1;
        Label_0174:
            if (this.isScrollVertical == null)
            {
                goto Label_018B;
            }
            num3 -= 1;
            num5 += 1;
        Label_018B:
            num6 = num3;
            goto Label_027A;
        Label_0194:
            num7 = num2;
            goto Label_026B;
        Label_019D:
            if (this.m_WidthLoop != null)
            {
                goto Label_01BD;
            }
            if (num7 < 0)
            {
                goto Label_0265;
            }
            if (num7 >= this.m_ViewWidthNum)
            {
                goto Label_0265;
            }
        Label_01BD:
            if (this.m_HeightLoop != null)
            {
                goto Label_01DD;
            }
            if (num6 < 0)
            {
                goto Label_0265;
            }
            if (num6 >= this.m_ViewHeightNum)
            {
                goto Label_0265;
            }
        Label_01DD:
            if ((this.GetNodeUsed(num7, num6) == null) == null)
            {
                goto Label_0265;
            }
            num8 = this.GetParamIndex(num7, num6);
            param = this.GetParam(num8);
            if (param == null)
            {
                goto Label_0265;
            }
            node3 = this.GetNodeEmpty();
            if ((node3 != null) == null)
            {
                goto Label_025B;
            }
            node3.Setup(num8, num7, num6, param);
            node3.SetActive(1);
            this.m_NodeUsed.Add(this.GetNodeKey(num7, num6), node3);
            goto Label_0265;
        Label_025B:
            Debug.LogError("ノードが不足しています");
        Label_0265:
            num7 += 1;
        Label_026B:
            if (num7 < num4)
            {
                goto Label_019D;
            }
            num6 += 1;
        Label_027A:
            if (num6 < num5)
            {
                goto Label_0194;
            }
            return;
        }

        public void CreateNode()
        {
            int num;
            int num2;
            int num3;
            int num4;
            ContentNode node;
            GameObject obj2;
            if ((this.m_Node == null) == null)
            {
                goto Label_002C;
            }
            Debug.LogError("ベースノードが設定されていません > " + base.get_gameObject().get_name());
            return;
        Label_002C:
            this.DestroyNode();
            num = this.m_NodeWidthNum;
            num2 = this.m_NodeHeightNum;
            if (this.isScrollHorizontal == null)
            {
                goto Label_004F;
            }
            num += 2;
        Label_004F:
            if (this.isScrollVertical == null)
            {
                goto Label_005E;
            }
            num2 += 2;
        Label_005E:
            num3 = 0;
            goto Label_0110;
        Label_0065:
            num4 = 0;
            goto Label_0105;
        Label_006C:
            node = null;
            if (this.m_Source == null)
            {
                goto Label_0092;
            }
            node = this.m_Source.Instantiate(this.m_Node);
            goto Label_00BA;
        Label_0092:
            obj2 = Object.Instantiate<GameObject>(this.m_Node.get_gameObject());
            if ((obj2 != null) == null)
            {
                goto Label_00BA;
            }
            node = obj2.GetComponent<ContentNode>();
        Label_00BA:
            if ((node != null) == null)
            {
                goto Label_0101;
            }
            node.Initialize(this);
            node.get_gameObject().get_transform().SetParent(this.m_RectTransform, 0);
            node.get_gameObject().SetActive(0);
            this.m_NodeEmpty.Add(node);
        Label_0101:
            num4 += 1;
        Label_0105:
            if (num4 < num)
            {
                goto Label_006C;
            }
            num3 += 1;
        Label_0110:
            if (num3 < num2)
            {
                goto Label_0065;
            }
            this.UpdateNode();
            this.m_SelectNode = -1;
            return;
        }

        public void CreateStaticNode(List<ContentNode> list)
        {
            int num;
            ContentNode node;
            int num2;
            int num3;
            int num4;
            this.DestroyNode();
            num = 0;
            goto Label_0062;
        Label_000D:
            node = list[num];
            num2 = num % this.m_ViewWidthNum;
            num3 = num / this.m_ViewWidthNum;
            num4 = this.GetParamIndex(num2, num3);
            node.Initialize(this);
            node.Setup(num4, num2, num3, this.GetParam(num4));
            this.m_NodeUsed.Add(this.GetNodeKey(num2, num3), node);
            num += 1;
        Label_0062:
            if (num < list.Count)
            {
                goto Label_000D;
            }
            this.UpdateNode();
            this.m_SelectNode = -1;
            return;
        }

        public void DestroyNode()
        {
            int num;
            ContentNode node;
            IDictionaryEnumerator enumerator;
            ContentNode node2;
            num = 0;
            goto Label_0035;
        Label_0007:
            node = this.m_NodeEmpty[num];
            if ((node != null) == null)
            {
                goto Label_0031;
            }
            node.Release();
            Object.Destroy(node.get_gameObject());
        Label_0031:
            num += 1;
        Label_0035:
            if (num < this.m_NodeEmpty.Count)
            {
                goto Label_0007;
            }
            this.m_NodeEmpty.Clear();
            enumerator = (Dictionary<string, ContentNode>.Enumerator) this.m_NodeUsed.GetEnumerator();
            goto Label_009B;
        Label_0067:
            node2 = (ContentNode) enumerator.Value;
            if ((node2 != null) == null)
            {
                goto Label_009B;
            }
            node2.Release();
            if (this.m_NodeStatic != null)
            {
                goto Label_009B;
            }
            Object.Destroy(node2.get_gameObject());
        Label_009B:
            if (enumerator.MoveNext() != null)
            {
                goto Label_0067;
            }
            this.m_NodeUsed.Clear();
            return;
        }

        public Vector2 GetAnchorePos()
        {
            if ((this.m_RectTransform != null) == null)
            {
                goto Label_0018;
            }
            return this.anchoredPosition;
        Label_0018:
            return Vector2.get_zero();
        }

        public unsafe Vector2 GetAnchorePosFromGrid(int x, int y)
        {
            Vector2 vector;
            vector = Vector2.get_zero();
            if (this.isScrollHorizontal == null)
            {
                goto Label_0046;
            }
            if (x < 0)
            {
                goto Label_0046;
            }
            &vector.x = -(((float) x) * (&this.m_CellSize.x + &this.m_Spacing.x)) - this.m_PaddingLeft;
        Label_0046:
            if (this.isScrollVertical == null)
            {
                goto Label_0085;
            }
            if (y < 0)
            {
                goto Label_0085;
            }
            &vector.y = (((float) y) * (&this.m_CellSize.y + &this.m_Spacing.y)) + this.m_PaddingTop;
        Label_0085:
            return vector;
        }

        public ContentSource GetCurrentSource()
        {
            return this.m_Source;
        }

        public ContentGrid GetGrid()
        {
            if ((this.m_RectTransform != null) == null)
            {
                goto Label_001E;
            }
            return this.GetGrid(this.anchoredPosition);
        Label_001E:
            return ContentGrid.zero;
        }

        public unsafe ContentGrid GetGrid(int index)
        {
            ContentGrid grid;
            grid = ContentGrid.zero;
            if (this.m_Constraint == null)
            {
                goto Label_001D;
            }
            if (this.m_Constraint != 2)
            {
                goto Label_0040;
            }
        Label_001D:
            &grid.x = index / this.m_ViewHeightNum;
            &grid.y = index % this.m_ViewHeightNum;
            goto Label_006A;
        Label_0040:
            if (this.m_Constraint != 1)
            {
                goto Label_006A;
            }
            &grid.x = index % this.m_ViewWidthNum;
            &grid.y = index / this.m_ViewWidthNum;
        Label_006A:
            return grid;
        }

        public unsafe ContentGrid GetGrid(Vector2 pos)
        {
            ContentGrid grid;
            grid = ContentGrid.zero;
            if ((this.m_RectTransform != null) == null)
            {
                goto Label_00E4;
            }
            &pos.x += this.m_PaddingLeft - &this.m_Spacing.x;
            &grid.fx = -&pos.x / (&this.m_CellSize.x + &this.m_Spacing.x);
            &pos.y -= this.m_PaddingTop - &this.m_Spacing.y;
            &grid.fy = &pos.y / (&this.m_CellSize.y + &this.m_Spacing.y);
            if (this.m_WidthLoop != null)
            {
                goto Label_00C4;
            }
            if (&grid.x >= 0)
            {
                goto Label_00C4;
            }
            &grid.x = 0;
        Label_00C4:
            if (this.m_HeightLoop != null)
            {
                goto Label_00E4;
            }
            if (&grid.y >= 0)
            {
                goto Label_00E4;
            }
            &grid.y = 0;
        Label_00E4:
            return grid;
        }

        public unsafe Vector2 GetLastPageAnchorePos()
        {
            Vector2 vector;
            vector = this.m_RectTransform.get_sizeDelta();
            &vector.x -= &this.m_PageSize.x;
            if (&vector.x >= 0f)
            {
                goto Label_0042;
            }
            &vector.x = 0f;
        Label_0042:
            &vector.y -= &this.m_PageSize.y;
            if (&vector.y >= 0f)
            {
                goto Label_0078;
            }
            &vector.y = 0f;
        Label_0078:
            return new Vector2(-&vector.x, &vector.y);
        }

        public unsafe ContentGrid GetLastPageGrid()
        {
            Vector2 vector;
            float num;
            float num2;
            vector = this.GetLastPageAnchorePos();
            if (&vector.x >= 0f)
            {
                goto Label_003C;
            }
            &vector.x -= &this.m_CellSize.x * 0.5f;
            goto Label_005B;
        Label_003C:
            &vector.x += &this.m_CellSize.x * 0.5f;
        Label_005B:
            if (&vector.y >= 0f)
            {
                goto Label_0090;
            }
            &vector.y -= &this.m_CellSize.y * 0.5f;
            goto Label_00AF;
        Label_0090:
            &vector.y += &this.m_CellSize.y * 0.5f;
        Label_00AF:
            num = -&vector.x / (&this.m_CellSize.x + &this.m_Spacing.x);
            num2 = &vector.y / (&this.m_CellSize.y + &this.m_Spacing.y);
            return new ContentGrid(num, num2);
        }

        public ContentNode GetNode(Vector2 screenPos)
        {
            return null;
        }

        public List<ContentNode> GetNodeAll()
        {
            List<ContentNode> list;
            list = new List<ContentNode>();
            list.AddRange(this.m_NodeEmpty);
            list.AddRange(Enumerable.ToArray<ContentNode>(this.m_NodeUsed.Values));
            return list;
        }

        public List<ContentNode> GetNodeChilds()
        {
            List<ContentNode> list;
            int num;
            Transform transform;
            ContentNode node;
            list = new List<ContentNode>();
            num = 0;
            goto Label_0054;
        Label_000D:
            transform = base.get_transform().GetChild(num);
            if ((transform != null) == null)
            {
                goto Label_0050;
            }
            if (transform.get_gameObject().get_activeSelf() == null)
            {
                goto Label_0050;
            }
            node = transform.GetComponent<ContentNode>();
            if ((node != null) == null)
            {
                goto Label_0050;
            }
            list.Add(node);
        Label_0050:
            num += 1;
        Label_0054:
            if (num < base.get_transform().get_childCount())
            {
                goto Label_000D;
            }
            return list;
        }

        public int GetNodeCount()
        {
            return (this.m_NodeEmpty.Count + this.m_NodeUsed.Count);
        }

        private ContentNode GetNodeEmpty()
        {
            ContentNode node;
            if (this.m_NodeEmpty.Count <= 0)
            {
                goto Label_002C;
            }
            node = this.m_NodeEmpty[0];
            this.m_NodeEmpty.RemoveAt(0);
            return node;
        Label_002C:
            return null;
        }

        private string GetNodeKey(int x, int y)
        {
            return (((int) x) + ":" + ((int) y));
        }

        public unsafe Vector2 GetNodePos(int x, int y)
        {
            return new Vector2(this.m_PaddingLeft + (((float) x) * (&this.m_CellSize.x + &this.m_Spacing.x)), -(this.m_PaddingTop + (((float) y) * (&this.m_CellSize.y + &this.m_Spacing.y))));
        }

        private unsafe ContentNode GetNodeUsed(int x, int y)
        {
            ContentNode node;
            node = null;
            this.m_NodeUsed.TryGetValue(this.GetNodeKey(x, y), &node);
            return node;
        }

        public ContentGrid GetNormalizeGrid(int x, int y)
        {
            int num;
            int num2;
            int num3;
            int num4;
            if (x >= 0)
            {
                goto Label_0026;
            }
            num = (this.m_ViewWidthNum - (x % this.m_ViewWidthNum)) % this.m_ViewWidthNum;
            x = num;
            goto Label_0032;
        Label_0026:
            num2 = x % this.m_ViewWidthNum;
            x = num2;
        Label_0032:
            if (y >= 0)
            {
                goto Label_0058;
            }
            num3 = (this.m_ViewHeightNum - (y % this.m_ViewHeightNum)) % this.m_ViewHeightNum;
            y = num3;
            goto Label_0064;
        Label_0058:
            num4 = y % this.m_ViewHeightNum;
            y = num4;
        Label_0064:
            return new ContentGrid(x, y);
        }

        public ContentSource.Param GetParam(int index)
        {
            if (this.m_Source == null)
            {
                goto Label_0018;
            }
            return this.m_Source.GetParam(index);
        Label_0018:
            return null;
        }

        public ContentSource.Param GetParam(int x, int y)
        {
            if (this.m_Source == null)
            {
                goto Label_001F;
            }
            return this.m_Source.GetParam(this.GetParamIndex(x, y));
        Label_001F:
            return null;
        }

        public unsafe int GetParamIndex(int x, int y)
        {
            ContentGrid grid;
            grid = this.GetNormalizeGrid(x, y);
            if (this.m_Constraint != 2)
            {
                goto Label_002C;
            }
            return ((&grid.x * this.m_ViewHeightNum) + &grid.y);
        Label_002C:
            return ((&grid.y * this.m_ViewWidthNum) + &grid.x);
        }

        public int GetSelect()
        {
            return this.m_SelectNode;
        }

        public Vector2 GetSpacing()
        {
            return this.m_Spacing;
        }

        public object GetWork()
        {
            return this.m_Work;
        }

        public virtual void Initialize(ContentSource source, Vector2 pos)
        {
            List<ContentNode> list;
            this.InitializeParam();
            if (this.m_NodeStatic != null)
            {
                goto Label_0037;
            }
            this.anchoredPosition = pos;
            if (source == null)
            {
                goto Label_0025;
            }
            this.SetCurrentSource(source);
        Label_0025:
            this.Resize(0);
            this.CreateNode();
            goto Label_0058;
        Label_0037:
            list = this.GetNodeChilds();
            this.anchoredPosition = pos;
            this.Resize(list.Count);
            this.CreateStaticNode(list);
        Label_0058:
            return;
        }

        private unsafe void InitializeParam()
        {
            Vector2 vector;
            Rect rect;
            Rect rect2;
            Rect rect3;
            Rect rect4;
            if ((this.m_RectTransform == null) == null)
            {
                goto Label_0022;
            }
            this.m_RectTransform = base.get_gameObject().GetComponent<RectTransform>();
        Label_0022:
            if ((this.m_Scroller == null) == null)
            {
                goto Label_0044;
            }
            this.m_Scroller = base.get_gameObject().GetComponentInParent<ContentScroller>();
        Label_0044:
            if ((this.m_RectTransform != null) == null)
            {
                goto Label_02CC;
            }
            this.m_RectTransform.set_anchorMin(new Vector2(0f, 1f));
            this.m_RectTransform.set_anchorMax(new Vector2(0f, 1f));
            this.m_RectTransform.set_pivot(new Vector2(0f, 1f));
            if ((this.m_Scroller != null) == null)
            {
                goto Label_00FF;
            }
            &this.m_PageSize.x = &this.m_Scroller.get_viewport().get_rect().get_width();
            &this.m_PageSize.y = &this.m_Scroller.get_viewport().get_rect().get_height();
            goto Label_013C;
        Label_00FF:
            &this.m_PageSize.x = &this.m_RectTransform.get_rect().get_width();
            &this.m_PageSize.y = &this.m_RectTransform.get_rect().get_height();
        Label_013C:
            vector = this.m_PageSize;
            if (this.m_Constraint != null)
            {
                goto Label_01F5;
            }
            &vector.x -= this.m_PaddingLeft + this.m_PaddingRight;
            &vector.y -= this.m_PaddingTop + this.m_PaddingBottom;
            this.m_NodeWidthNum = Mathf.CeilToInt((&vector.x + &this.m_Spacing.x) / (&this.m_CellSize.x + &this.m_Spacing.x));
            this.m_NodeHeightNum = Mathf.CeilToInt((&vector.y + &this.m_Spacing.y) / (&this.m_CellSize.y + &this.m_Spacing.y));
            goto Label_02CC;
        Label_01F5:
            if (this.m_Constraint != 1)
            {
                goto Label_0263;
            }
            &vector.y -= this.m_PaddingTop + this.m_PaddingBottom;
            this.m_NodeWidthNum = this.m_ConstraintCount;
            this.m_NodeHeightNum = Mathf.CeilToInt((&vector.y + &this.m_Spacing.y) / (&this.m_CellSize.y + &this.m_Spacing.y));
            goto Label_02CC;
        Label_0263:
            if (this.m_Constraint != 2)
            {
                goto Label_02CC;
            }
            &vector.x -= this.m_PaddingLeft + this.m_PaddingRight;
            this.m_NodeWidthNum = Mathf.CeilToInt((&vector.x + &this.m_Spacing.x) / (&this.m_CellSize.x + &this.m_Spacing.x));
            this.m_NodeHeightNum = this.m_ConstraintCount;
        Label_02CC:
            return;
        }

        private unsafe void LateUpdate()
        {
            bool flag;
            Vector2 vector;
            if ((Input.get_touchCount() > 0) != null)
            {
                goto Label_0077;
            }
            if (this.m_MoveRefresh != null)
            {
                goto Label_0089;
            }
            if (&this.m_Scroller.get_velocity().get_magnitude() >= 0.01f)
            {
                goto Label_0089;
            }
            this.m_MoveRefreshTime += Time.get_deltaTime();
            if (this.m_MoveRefreshTime <= 0.1f)
            {
                goto Label_0089;
            }
            this.m_MoveRefresh = 1;
            this.MoveRefresh();
            this.m_Scroller.StopMovement();
            goto Label_0089;
        Label_0077:
            this.m_MoveRefresh = 0;
            this.m_MoveRefreshTime = 0f;
        Label_0089:
            return;
        }

        public unsafe bool MoveRefresh()
        {
            Vector2 vector;
            bool flag;
            float num;
            float num2;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            vector = this.anchoredPosition;
            flag = 0;
            if (this.m_WidthLoop == null)
            {
                goto Label_00BF;
            }
            num = &this.m_RectTransform.get_sizeDelta().x - (this.m_PaddingLeft + this.m_PaddingRight);
            if (&vector.x <= &this.m_PageSize.x)
            {
                goto Label_007A;
            }
            goto Label_0062;
        Label_0053:
            &vector.x -= num;
        Label_0062:
            if (&vector.x > 0f)
            {
                goto Label_0053;
            }
            flag = 1;
            goto Label_00BF;
        Label_007A:
            if (&vector.x >= -&this.m_RectTransform.get_sizeDelta().x)
            {
                goto Label_00BF;
            }
            goto Label_00AF;
        Label_00A0:
            &vector.x += num;
        Label_00AF:
            if (&vector.x < -num)
            {
                goto Label_00A0;
            }
            flag = 1;
        Label_00BF:
            if (this.m_HeightLoop == null)
            {
                goto Label_0175;
            }
            num2 = &this.m_RectTransform.get_sizeDelta().y - (this.m_PaddingTop + this.m_PaddingBottom);
            if (&vector.y <= &this.m_PageSize.y)
            {
                goto Label_0130;
            }
            goto Label_0118;
        Label_0109:
            &vector.y -= num2;
        Label_0118:
            if (&vector.y > 0f)
            {
                goto Label_0109;
            }
            flag = 1;
            goto Label_0175;
        Label_0130:
            if (&vector.y >= -&this.m_RectTransform.get_sizeDelta().y)
            {
                goto Label_0175;
            }
            goto Label_0165;
        Label_0156:
            &vector.y += num2;
        Label_0165:
            if (&vector.y < -num2)
            {
                goto Label_0156;
            }
            flag = 1;
        Label_0175:
            if (flag == null)
            {
                goto Label_0188;
            }
            this.anchoredPosition = vector;
            this.UpdateNode();
        Label_0188:
            return flag;
        }

        public virtual void Release()
        {
            if (this.m_Source == null)
            {
                goto Label_001D;
            }
            this.m_Source.Release();
            this.m_Source = null;
        Label_001D:
            this.DestroyNode();
            return;
        }

        public unsafe void Resize(int count)
        {
            Vector2 vector;
            if (count != null)
            {
                goto Label_0030;
            }
            if (this.m_Source == null)
            {
                goto Label_0022;
            }
            if ((this.m_RectTransform == null) == null)
            {
                goto Label_0023;
            }
        Label_0022:
            return;
        Label_0023:
            count = this.m_Source.GetCount();
        Label_0030:
            vector = this.m_PageSize;
            if (this.m_Constraint != null)
            {
                goto Label_0068;
            }
            this.m_ViewWidthNum = Mathf.CeilToInt(((float) count) / ((float) this.m_NodeHeightNum));
            this.m_ViewHeightNum = this.m_NodeHeightNum;
            goto Label_00C7;
        Label_0068:
            if (this.m_Constraint != 1)
            {
                goto Label_009A;
            }
            this.m_ViewWidthNum = this.m_ConstraintCount;
            this.m_ViewHeightNum = Mathf.CeilToInt(((float) count) / ((float) this.m_ConstraintCount));
            goto Label_00C7;
        Label_009A:
            if (this.m_Constraint != 2)
            {
                goto Label_00C7;
            }
            this.m_ViewWidthNum = Mathf.CeilToInt(((float) count) / ((float) this.m_ConstraintCount));
            this.m_ViewHeightNum = this.m_ConstraintCount;
        Label_00C7:
            &vector.x = ((((float) this.m_ViewWidthNum) * (&this.m_CellSize.x + &this.m_Spacing.x)) + this.m_PaddingLeft) + this.m_PaddingRight;
            &vector.y = ((((float) this.m_ViewHeightNum) * (&this.m_CellSize.y + &this.m_Spacing.y)) + this.m_PaddingTop) + this.m_PaddingBottom;
            this.m_RectTransform.set_sizeDelta(vector);
            return;
        }

        public void SetCurrentSource(ContentSource source)
        {
            if (this.m_Source == null)
            {
                goto Label_0016;
            }
            this.m_Source.Release();
        Label_0016:
            this.m_Source = source;
            if (this.m_Source == null)
            {
                goto Label_0034;
            }
            this.m_Source.Initialize(this);
        Label_0034:
            return;
        }

        public void SetSelect(int index)
        {
            this.m_SelectNode = index;
            return;
        }

        public void SetSpacing(Vector2 value)
        {
            this.m_Spacing = value;
            return;
        }

        public void SetWork(object value)
        {
            this.m_Work = value;
            return;
        }

        protected virtual void Update()
        {
            this.UpdateNode();
            if (this.m_Source == null)
            {
                goto Label_001C;
            }
            this.m_Source.Update();
        Label_001C:
            return;
        }

        private unsafe void UpdateNode()
        {
            List<ContentNode> list;
            RectTransform transform;
            Rect rect;
            IDictionaryEnumerator enumerator;
            ContentNode node;
            Vector2 vector;
            float num;
            float num2;
            float num3;
            float num4;
            if (this.m_NodeStatic != null)
            {
                goto Label_0016;
            }
            this.CheckActiveNode();
            goto Label_0046;
        Label_0016:
            list = this.GetNodeChilds();
            if (list.Count == this.m_NodeUsed.Count)
            {
                goto Label_0046;
            }
            this.Resize(list.Count);
            this.CreateStaticNode(list);
        Label_0046:
            transform = this.m_Scroller.get_viewport();
            if ((transform != null) == null)
            {
                goto Label_01BC;
            }
            rect = transform.get_rect();
            enumerator = (Dictionary<string, ContentNode>.Enumerator) this.m_NodeUsed.GetEnumerator();
            goto Label_01B1;
        Label_007B:
            node = (ContentNode) enumerator.Value;
            if ((node != null) == null)
            {
                goto Label_01B1;
            }
            vector = transform.InverseTransformPoint(node.rectTransform.get_position());
            vector = node.GetPivotAnchoredPosition(vector);
            num = (&vector.x - (node.sizeX * 0.5f)) + 2.5f;
            num2 = (&vector.x + (node.sizeX * 0.5f)) - 2.5f;
            num3 = (&vector.y + (node.sizeY * 0.5f)) - 2.5f;
            num4 = (&vector.y - (node.sizeY * 0.5f)) + 2.5f;
            if (num2 <= &rect.get_x())
            {
                goto Label_0183;
            }
            float introduced10 = &rect.get_x();
            if (num >= (introduced10 + &rect.get_width()))
            {
                goto Label_0183;
            }
            if (num3 <= &rect.get_y())
            {
                goto Label_0183;
            }
            float introduced11 = &rect.get_y();
            if (num4 >= (introduced11 + &rect.get_height()))
            {
                goto Label_0183;
            }
            node.OnViewIn(vector);
            goto Label_018C;
        Label_0183:
            node.OnViewOut(vector);
        Label_018C:
            if (node.index != this.m_SelectNode)
            {
                goto Label_01AA;
            }
            node.OnSelectOn();
            goto Label_01B1;
        Label_01AA:
            node.OnSelectOff();
        Label_01B1:
            if (enumerator.MoveNext() != null)
            {
                goto Label_007B;
            }
        Label_01BC:
            return;
        }

        public ContentScroller scroller
        {
            get
            {
                return this.m_Scroller;
            }
        }

        public bool isScrollHorizontal
        {
            get
            {
                return this.m_Scroller.get_horizontal();
            }
        }

        public bool isScrollVertical
        {
            get
            {
                return this.m_Scroller.get_vertical();
            }
        }

        public Vector2 anchoredPosition
        {
            get
            {
                if ((this.m_RectTransform != null) == null)
                {
                    goto Label_001D;
                }
                return this.m_RectTransform.get_anchoredPosition();
            Label_001D:
                return Vector2.get_zero();
            }
            set
            {
                this.m_RectTransform.set_anchoredPosition(value);
                return;
            }
        }

        public enum Constraint
        {
            Flexible,
            FixedColumnCount,
            FixedRowCount
        }
    }
}

