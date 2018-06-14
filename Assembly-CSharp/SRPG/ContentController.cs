// Decompiled with JetBrains decompiler
// Type: SRPG.ContentController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
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
    public ContentController.Constraint m_Constraint;
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
      base.\u002Ector();
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
      set
      {
        this.m_RectTransform.set_anchoredPosition(value);
      }
      get
      {
        if (Object.op_Inequality((Object) this.m_RectTransform, (Object) null))
          return this.m_RectTransform.get_anchoredPosition();
        return Vector2.get_zero();
      }
    }

    protected virtual void Awake()
    {
      this.m_RectTransform = (RectTransform) ((Component) this).get_gameObject().GetComponent<RectTransform>();
      if (Object.op_Equality((Object) this.m_Scroller, (Object) null))
        this.m_Scroller = (ContentScroller) ((Component) this).get_gameObject().GetComponentInParent<ContentScroller>();
      if (Object.op_Inequality((Object) this.m_Node, (Object) null))
      {
        this.m_NodeStatic = false;
        ((Component) this.m_Node).get_gameObject().SetActive(false);
      }
      else
      {
        this.m_NodeStatic = true;
        this.Initialize((ContentSource) null, Vector2.get_zero());
      }
    }

    public virtual void Initialize(ContentSource source, Vector2 pos)
    {
      this.InitializeParam();
      if (!this.m_NodeStatic)
      {
        this.anchoredPosition = pos;
        if (source != null)
          this.SetCurrentSource(source);
        this.Resize(0);
        this.CreateNode();
      }
      else
      {
        List<ContentNode> nodeChilds = this.GetNodeChilds();
        this.anchoredPosition = pos;
        this.Resize(nodeChilds.Count);
        this.CreateStaticNode(nodeChilds);
      }
    }

    private void InitializeParam()
    {
      if (Object.op_Equality((Object) this.m_RectTransform, (Object) null))
        this.m_RectTransform = (RectTransform) ((Component) this).get_gameObject().GetComponent<RectTransform>();
      if (Object.op_Equality((Object) this.m_Scroller, (Object) null))
        this.m_Scroller = (ContentScroller) ((Component) this).get_gameObject().GetComponentInParent<ContentScroller>();
      if (!Object.op_Inequality((Object) this.m_RectTransform, (Object) null))
        return;
      this.m_RectTransform.set_anchorMin(new Vector2(0.0f, 1f));
      this.m_RectTransform.set_anchorMax(new Vector2(0.0f, 1f));
      this.m_RectTransform.set_pivot(new Vector2(0.0f, 1f));
      if (Object.op_Inequality((Object) this.m_Scroller, (Object) null))
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @this.m_PageSize;
        Rect rect1 = this.m_Scroller.get_viewport().get_rect();
        // ISSUE: explicit reference operation
        double width = (double) ((Rect) @rect1).get_width();
        // ISSUE: explicit reference operation
        (^local1).x = (__Null) width;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @this.m_PageSize;
        Rect rect2 = this.m_Scroller.get_viewport().get_rect();
        // ISSUE: explicit reference operation
        double height = (double) ((Rect) @rect2).get_height();
        // ISSUE: explicit reference operation
        (^local2).y = (__Null) height;
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @this.m_PageSize;
        Rect rect1 = this.m_RectTransform.get_rect();
        // ISSUE: explicit reference operation
        double width = (double) ((Rect) @rect1).get_width();
        // ISSUE: explicit reference operation
        (^local1).x = (__Null) width;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @this.m_PageSize;
        Rect rect2 = this.m_RectTransform.get_rect();
        // ISSUE: explicit reference operation
        double height = (double) ((Rect) @rect2).get_height();
        // ISSUE: explicit reference operation
        (^local2).y = (__Null) height;
      }
      Vector2 pageSize = this.m_PageSize;
      if (this.m_Constraint == ContentController.Constraint.Flexible)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @pageSize;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local1).x = (__Null) ((^local1).x - ((double) this.m_PaddingLeft + (double) this.m_PaddingRight));
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @pageSize;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local2).y = (__Null) ((^local2).y - ((double) this.m_PaddingTop + (double) this.m_PaddingBottom));
        this.m_NodeWidthNum = Mathf.CeilToInt((float) ((pageSize.x + this.m_Spacing.x) / (this.m_CellSize.x + this.m_Spacing.x)));
        this.m_NodeHeightNum = Mathf.CeilToInt((float) ((pageSize.y + this.m_Spacing.y) / (this.m_CellSize.y + this.m_Spacing.y)));
      }
      else if (this.m_Constraint == ContentController.Constraint.FixedColumnCount)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @pageSize;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y - ((double) this.m_PaddingTop + (double) this.m_PaddingBottom));
        this.m_NodeWidthNum = this.m_ConstraintCount;
        this.m_NodeHeightNum = Mathf.CeilToInt((float) ((pageSize.y + this.m_Spacing.y) / (this.m_CellSize.y + this.m_Spacing.y)));
      }
      else
      {
        if (this.m_Constraint != ContentController.Constraint.FixedRowCount)
          return;
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @pageSize;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).x = (__Null) ((^local).x - ((double) this.m_PaddingLeft + (double) this.m_PaddingRight));
        this.m_NodeWidthNum = Mathf.CeilToInt((float) ((pageSize.x + this.m_Spacing.x) / (this.m_CellSize.x + this.m_Spacing.x)));
        this.m_NodeHeightNum = this.m_ConstraintCount;
      }
    }

    public virtual void Release()
    {
      if (this.m_Source != null)
      {
        this.m_Source.Release();
        this.m_Source = (ContentSource) null;
      }
      this.DestroyNode();
    }

    protected virtual void Update()
    {
      this.UpdateNode();
      if (this.m_Source == null)
        return;
      this.m_Source.Update();
    }

    private void LateUpdate()
    {
      if (Input.get_touchCount() <= 0)
      {
        if (this.m_MoveRefresh)
          return;
        Vector2 velocity = this.m_Scroller.get_velocity();
        // ISSUE: explicit reference operation
        if ((double) ((Vector2) @velocity).get_magnitude() >= 0.00999999977648258)
          return;
        this.m_MoveRefreshTime += Time.get_deltaTime();
        if ((double) this.m_MoveRefreshTime <= 0.100000001490116)
          return;
        this.m_MoveRefresh = true;
        this.MoveRefresh();
        this.m_Scroller.StopMovement();
      }
      else
      {
        this.m_MoveRefresh = false;
        this.m_MoveRefreshTime = 0.0f;
      }
    }

    private void UpdateNode()
    {
      if (!this.m_NodeStatic)
      {
        this.CheckActiveNode();
      }
      else
      {
        List<ContentNode> nodeChilds = this.GetNodeChilds();
        if (nodeChilds.Count != this.m_NodeUsed.Count)
        {
          this.Resize(nodeChilds.Count);
          this.CreateStaticNode(nodeChilds);
        }
      }
      RectTransform viewport = this.m_Scroller.get_viewport();
      if (!Object.op_Inequality((Object) viewport, (Object) null))
        return;
      Rect rect = viewport.get_rect();
      IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.m_NodeUsed.GetEnumerator();
      while (enumerator.MoveNext())
      {
        ContentNode contentNode = (ContentNode) enumerator.Value;
        if (Object.op_Inequality((Object) contentNode, (Object) null))
        {
          Vector2 vector2 = Vector2.op_Implicit(((Transform) viewport).InverseTransformPoint(((Transform) contentNode.rectTransform).get_position()));
          vector2 = contentNode.GetPivotAnchoredPosition(vector2);
          float num1 = (float) (vector2.x - (double) contentNode.sizeX * 0.5 + 2.5);
          float num2 = (float) (vector2.x + (double) contentNode.sizeX * 0.5 - 2.5);
          float num3 = (float) (vector2.y + (double) contentNode.sizeY * 0.5 - 2.5);
          float num4 = (float) (vector2.y - (double) contentNode.sizeY * 0.5 + 2.5);
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if ((double) num2 > (double) ((Rect) @rect).get_x() && (double) num1 < (double) ((Rect) @rect).get_x() + (double) ((Rect) @rect).get_width() && ((double) num3 > (double) ((Rect) @rect).get_y() && (double) num4 < (double) ((Rect) @rect).get_y() + (double) ((Rect) @rect).get_height()))
            contentNode.OnViewIn(vector2);
          else
            contentNode.OnViewOut(vector2);
          if (contentNode.index == this.m_SelectNode)
            contentNode.OnSelectOn();
          else
            contentNode.OnSelectOff();
        }
      }
    }

    public bool MoveRefresh()
    {
      Vector2 anchoredPosition = this.anchoredPosition;
      bool flag = false;
      if (this.m_WidthLoop)
      {
        float num = (float) (this.m_RectTransform.get_sizeDelta().x - ((double) this.m_PaddingLeft + (double) this.m_PaddingRight));
        if (anchoredPosition.x > this.m_PageSize.x)
        {
          // ISSUE: variable of a reference type
          Vector2& local;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          for (; anchoredPosition.x > 0.0; (^local).x = (__Null) ((^local).x - (double) num))
          {
            // ISSUE: explicit reference operation
            local = @anchoredPosition;
          }
          flag = true;
        }
        else if (anchoredPosition.x < -this.m_RectTransform.get_sizeDelta().x)
        {
          // ISSUE: variable of a reference type
          Vector2& local;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          for (; anchoredPosition.x < -(double) num; (^local).x = (__Null) ((^local).x + (double) num))
          {
            // ISSUE: explicit reference operation
            local = @anchoredPosition;
          }
          flag = true;
        }
      }
      if (this.m_HeightLoop)
      {
        float num = (float) (this.m_RectTransform.get_sizeDelta().y - ((double) this.m_PaddingTop + (double) this.m_PaddingBottom));
        if (anchoredPosition.y > this.m_PageSize.y)
        {
          // ISSUE: variable of a reference type
          Vector2& local;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          for (; anchoredPosition.y > 0.0; (^local).y = (__Null) ((^local).y - (double) num))
          {
            // ISSUE: explicit reference operation
            local = @anchoredPosition;
          }
          flag = true;
        }
        else if (anchoredPosition.y < -this.m_RectTransform.get_sizeDelta().y)
        {
          // ISSUE: variable of a reference type
          Vector2& local;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          for (; anchoredPosition.y < -(double) num; (^local).y = (__Null) ((^local).y + (double) num))
          {
            // ISSUE: explicit reference operation
            local = @anchoredPosition;
          }
          flag = true;
        }
      }
      if (flag)
      {
        this.anchoredPosition = anchoredPosition;
        this.UpdateNode();
      }
      return flag;
    }

    public void CreateStaticNode(List<ContentNode> list)
    {
      this.DestroyNode();
      for (int index = 0; index < list.Count; ++index)
      {
        ContentNode contentNode = list[index];
        int x = index % this.m_ViewWidthNum;
        int y = index / this.m_ViewWidthNum;
        int paramIndex = this.GetParamIndex(x, y);
        contentNode.Initialize(this);
        contentNode.Setup(paramIndex, x, y, this.GetParam(paramIndex));
        this.m_NodeUsed.Add(this.GetNodeKey(x, y), contentNode);
      }
      this.UpdateNode();
      this.m_SelectNode = -1;
    }

    public void CreateNode()
    {
      if (Object.op_Equality((Object) this.m_Node, (Object) null))
      {
        Debug.LogError((object) ("ベースノードが設定されていません > " + ((Object) ((Component) this).get_gameObject()).get_name()));
      }
      else
      {
        this.DestroyNode();
        int nodeWidthNum = this.m_NodeWidthNum;
        int nodeHeightNum = this.m_NodeHeightNum;
        if (this.isScrollHorizontal)
          nodeWidthNum += 2;
        if (this.isScrollVertical)
          nodeHeightNum += 2;
        for (int index1 = 0; index1 < nodeHeightNum; ++index1)
        {
          for (int index2 = 0; index2 < nodeWidthNum; ++index2)
          {
            ContentNode contentNode = (ContentNode) null;
            if (this.m_Source != null)
            {
              contentNode = this.m_Source.Instantiate(this.m_Node);
            }
            else
            {
              GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) ((Component) this.m_Node).get_gameObject());
              if (Object.op_Inequality((Object) gameObject, (Object) null))
                contentNode = (ContentNode) gameObject.GetComponent<ContentNode>();
            }
            if (Object.op_Inequality((Object) contentNode, (Object) null))
            {
              contentNode.Initialize(this);
              ((Component) contentNode).get_gameObject().get_transform().SetParent((Transform) this.m_RectTransform, false);
              ((Component) contentNode).get_gameObject().SetActive(false);
              this.m_NodeEmpty.Add(contentNode);
            }
          }
        }
        this.UpdateNode();
        this.m_SelectNode = -1;
      }
    }

    public void DestroyNode()
    {
      for (int index = 0; index < this.m_NodeEmpty.Count; ++index)
      {
        ContentNode contentNode = this.m_NodeEmpty[index];
        if (Object.op_Inequality((Object) contentNode, (Object) null))
        {
          contentNode.Release();
          Object.Destroy((Object) ((Component) contentNode).get_gameObject());
        }
      }
      this.m_NodeEmpty.Clear();
      IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.m_NodeUsed.GetEnumerator();
      while (enumerator.MoveNext())
      {
        ContentNode contentNode = (ContentNode) enumerator.Value;
        if (Object.op_Inequality((Object) contentNode, (Object) null))
        {
          contentNode.Release();
          if (!this.m_NodeStatic)
            Object.Destroy((Object) ((Component) contentNode).get_gameObject());
        }
      }
      this.m_NodeUsed.Clear();
    }

    private void CheckActiveNode()
    {
      ContentGrid grid = this.GetGrid();
      List<ContentNode> contentNodeList = new List<ContentNode>();
      IDictionaryEnumerator enumerator = (IDictionaryEnumerator) this.m_NodeUsed.GetEnumerator();
      while (enumerator.MoveNext())
      {
        ContentNode contentNode = (ContentNode) enumerator.Value;
        if (Object.op_Inequality((Object) contentNode, (Object) null) && (contentNode.IsReMake() || !contentNode.IsValid() || (contentNode.gridX < grid.x - 1 || contentNode.gridX > grid.x + this.m_NodeWidthNum) || (contentNode.gridY < grid.y - 1 || contentNode.gridY > grid.y + this.m_NodeHeightNum)))
          contentNodeList.Add(contentNode);
      }
      for (int index = 0; index < contentNodeList.Count; ++index)
      {
        ContentNode contentNode = contentNodeList[index];
        if (Object.op_Inequality((Object) contentNode, (Object) null))
        {
          contentNode.OnSelectOff();
          contentNode.SetActive(false);
          this.m_NodeUsed.Remove(this.GetNodeKey(contentNode.gridX, contentNode.gridY));
          this.m_NodeEmpty.Add(contentNode);
        }
      }
      int x1 = grid.x;
      int y1 = grid.y;
      int num1 = grid.x + this.m_NodeWidthNum;
      int num2 = grid.y + this.m_NodeHeightNum;
      if (this.isScrollHorizontal)
      {
        --x1;
        ++num1;
      }
      if (this.isScrollVertical)
      {
        --y1;
        ++num2;
      }
      for (int y2 = y1; y2 < num2; ++y2)
      {
        for (int x2 = x1; x2 < num1; ++x2)
        {
          if ((this.m_WidthLoop || x2 >= 0 && x2 < this.m_ViewWidthNum) && (this.m_HeightLoop || y2 >= 0 && y2 < this.m_ViewHeightNum) && Object.op_Equality((Object) this.GetNodeUsed(x2, y2), (Object) null))
          {
            int paramIndex = this.GetParamIndex(x2, y2);
            ContentSource.Param obj = this.GetParam(paramIndex);
            if (obj != null)
            {
              ContentNode nodeEmpty = this.GetNodeEmpty();
              if (Object.op_Inequality((Object) nodeEmpty, (Object) null))
              {
                nodeEmpty.Setup(paramIndex, x2, y2, obj);
                nodeEmpty.SetActive(true);
                this.m_NodeUsed.Add(this.GetNodeKey(x2, y2), nodeEmpty);
              }
              else
                Debug.LogError((object) "ノードが不足しています");
            }
          }
        }
      }
    }

    private string GetNodeKey(int x, int y)
    {
      return x.ToString() + ":" + (object) y;
    }

    private ContentNode GetNodeUsed(int x, int y)
    {
      ContentNode contentNode = (ContentNode) null;
      this.m_NodeUsed.TryGetValue(this.GetNodeKey(x, y), out contentNode);
      return contentNode;
    }

    private ContentNode GetNodeEmpty()
    {
      if (this.m_NodeEmpty.Count <= 0)
        return (ContentNode) null;
      ContentNode contentNode = this.m_NodeEmpty[0];
      this.m_NodeEmpty.RemoveAt(0);
      return contentNode;
    }

    public int GetNodeCount()
    {
      return this.m_NodeEmpty.Count + this.m_NodeUsed.Count;
    }

    public List<ContentNode> GetNodeAll()
    {
      List<ContentNode> contentNodeList = new List<ContentNode>();
      contentNodeList.AddRange((IEnumerable<ContentNode>) this.m_NodeEmpty);
      contentNodeList.AddRange((IEnumerable<ContentNode>) this.m_NodeUsed.Values.ToArray<ContentNode>());
      return contentNodeList;
    }

    public List<ContentNode> GetNodeChilds()
    {
      List<ContentNode> contentNodeList = new List<ContentNode>();
      for (int index = 0; index < ((Component) this).get_transform().get_childCount(); ++index)
      {
        Transform child = ((Component) this).get_transform().GetChild(index);
        if (Object.op_Inequality((Object) child, (Object) null) && ((Component) child).get_gameObject().get_activeSelf())
        {
          ContentNode component = (ContentNode) ((Component) child).GetComponent<ContentNode>();
          if (Object.op_Inequality((Object) component, (Object) null))
            contentNodeList.Add(component);
        }
      }
      return contentNodeList;
    }

    public int GetParamIndex(int x, int y)
    {
      ContentGrid normalizeGrid = this.GetNormalizeGrid(x, y);
      if (this.m_Constraint == ContentController.Constraint.FixedRowCount)
        return normalizeGrid.x * this.m_ViewHeightNum + normalizeGrid.y;
      return normalizeGrid.y * this.m_ViewWidthNum + normalizeGrid.x;
    }

    public ContentSource.Param GetParam(int x, int y)
    {
      if (this.m_Source != null)
        return this.m_Source.GetParam(this.GetParamIndex(x, y));
      return (ContentSource.Param) null;
    }

    public ContentSource.Param GetParam(int index)
    {
      if (this.m_Source != null)
        return this.m_Source.GetParam(index);
      return (ContentSource.Param) null;
    }

    public ContentGrid GetLastPageGrid()
    {
      Vector2 lastPageAnchorePos = this.GetLastPageAnchorePos();
      if (lastPageAnchorePos.x < 0.0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @lastPageAnchorePos;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).x = (__Null) ((^local).x - this.m_CellSize.x * 0.5);
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @lastPageAnchorePos;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).x = (__Null) ((^local).x + this.m_CellSize.x * 0.5);
      }
      if (lastPageAnchorePos.y < 0.0)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @lastPageAnchorePos;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y - this.m_CellSize.y * 0.5);
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @lastPageAnchorePos;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y + this.m_CellSize.y * 0.5);
      }
      return new ContentGrid((float) (-lastPageAnchorePos.x / (this.m_CellSize.x + this.m_Spacing.x)), (float) (lastPageAnchorePos.y / (this.m_CellSize.y + this.m_Spacing.y)));
    }

    public ContentGrid GetGrid()
    {
      if (Object.op_Inequality((Object) this.m_RectTransform, (Object) null))
        return this.GetGrid(this.anchoredPosition);
      return ContentGrid.zero;
    }

    public ContentGrid GetGrid(Vector2 pos)
    {
      ContentGrid zero = ContentGrid.zero;
      if (Object.op_Inequality((Object) this.m_RectTransform, (Object) null))
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local1 = @pos;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local1).x = (__Null) ((^local1).x + ((double) this.m_PaddingLeft - this.m_Spacing.x));
        zero.fx = (float) (-pos.x / (this.m_CellSize.x + this.m_Spacing.x));
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local2 = @pos;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local2).y = (__Null) ((^local2).y - ((double) this.m_PaddingTop - this.m_Spacing.y));
        zero.fy = (float) (pos.y / (this.m_CellSize.y + this.m_Spacing.y));
        if (!this.m_WidthLoop && zero.x < 0)
          zero.x = 0;
        if (!this.m_HeightLoop && zero.y < 0)
          zero.y = 0;
      }
      return zero;
    }

    public ContentGrid GetGrid(int index)
    {
      ContentGrid zero = ContentGrid.zero;
      if (this.m_Constraint == ContentController.Constraint.Flexible || this.m_Constraint == ContentController.Constraint.FixedRowCount)
      {
        zero.x = index / this.m_ViewHeightNum;
        zero.y = index % this.m_ViewHeightNum;
      }
      else if (this.m_Constraint == ContentController.Constraint.FixedColumnCount)
      {
        zero.x = index % this.m_ViewWidthNum;
        zero.y = index / this.m_ViewWidthNum;
      }
      return zero;
    }

    public ContentGrid GetNormalizeGrid(int x, int y)
    {
      if (x < 0)
        x = (this.m_ViewWidthNum - x % this.m_ViewWidthNum) % this.m_ViewWidthNum;
      else
        x %= this.m_ViewWidthNum;
      if (y < 0)
        y = (this.m_ViewHeightNum - y % this.m_ViewHeightNum) % this.m_ViewHeightNum;
      else
        y %= this.m_ViewHeightNum;
      return new ContentGrid(x, y);
    }

    public Vector2 GetNodePos(int x, int y)
    {
      return new Vector2(this.m_PaddingLeft + (float) x * (float) (this.m_CellSize.x + this.m_Spacing.x), (float) -((double) this.m_PaddingTop + (double) y * (this.m_CellSize.y + this.m_Spacing.y)));
    }

    public Vector2 GetLastPageAnchorePos()
    {
      Vector2 sizeDelta = this.m_RectTransform.get_sizeDelta();
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local1 = @sizeDelta;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local1).x = (^local1).x - this.m_PageSize.x;
      if (sizeDelta.x < 0.0)
        sizeDelta.x = (__Null) 0.0;
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Vector2& local2 = @sizeDelta;
      // ISSUE: explicit reference operation
      // ISSUE: explicit reference operation
      (^local2).y = (^local2).y - this.m_PageSize.y;
      if (sizeDelta.y < 0.0)
        sizeDelta.y = (__Null) 0.0;
      return new Vector2((float) -sizeDelta.x, (float) sizeDelta.y);
    }

    public Vector2 GetAnchorePosFromGrid(int x, int y)
    {
      Vector2 zero = Vector2.get_zero();
      if (this.isScrollHorizontal && x >= 0)
        zero.x = (__Null) (-((double) x * (this.m_CellSize.x + this.m_Spacing.x)) - (double) this.m_PaddingLeft);
      if (this.isScrollVertical && y >= 0)
        zero.y = (__Null) ((double) y * (this.m_CellSize.y + this.m_Spacing.y) + (double) this.m_PaddingTop);
      return zero;
    }

    public ContentNode GetNode(Vector2 screenPos)
    {
      return (ContentNode) null;
    }

    public void Resize(int count = 0)
    {
      if (count == 0)
      {
        if (this.m_Source == null || Object.op_Equality((Object) this.m_RectTransform, (Object) null))
          return;
        count = this.m_Source.GetCount();
      }
      Vector2 pageSize = this.m_PageSize;
      if (this.m_Constraint == ContentController.Constraint.Flexible)
      {
        this.m_ViewWidthNum = Mathf.CeilToInt((float) count / (float) this.m_NodeHeightNum);
        this.m_ViewHeightNum = this.m_NodeHeightNum;
      }
      else if (this.m_Constraint == ContentController.Constraint.FixedColumnCount)
      {
        this.m_ViewWidthNum = this.m_ConstraintCount;
        this.m_ViewHeightNum = Mathf.CeilToInt((float) count / (float) this.m_ConstraintCount);
      }
      else if (this.m_Constraint == ContentController.Constraint.FixedRowCount)
      {
        this.m_ViewWidthNum = Mathf.CeilToInt((float) count / (float) this.m_ConstraintCount);
        this.m_ViewHeightNum = this.m_ConstraintCount;
      }
      pageSize.x = (__Null) ((double) this.m_ViewWidthNum * (this.m_CellSize.x + this.m_Spacing.x) + (double) this.m_PaddingLeft + (double) this.m_PaddingRight);
      pageSize.y = (__Null) ((double) this.m_ViewHeightNum * (this.m_CellSize.y + this.m_Spacing.y) + (double) this.m_PaddingTop + (double) this.m_PaddingBottom);
      this.m_RectTransform.set_sizeDelta(pageSize);
    }

    public void SetCurrentSource(ContentSource source)
    {
      if (this.m_Source != null)
        this.m_Source.Release();
      this.m_Source = source;
      if (this.m_Source == null)
        return;
      this.m_Source.Initialize(this);
    }

    public ContentSource GetCurrentSource()
    {
      return this.m_Source;
    }

    public void SetWork(object value)
    {
      this.m_Work = value;
    }

    public object GetWork()
    {
      return this.m_Work;
    }

    public void SetSelect(int index)
    {
      this.m_SelectNode = index;
    }

    public int GetSelect()
    {
      return this.m_SelectNode;
    }

    public void SetSpacing(Vector2 value)
    {
      this.m_Spacing = value;
    }

    public Vector2 GetSpacing()
    {
      return this.m_Spacing;
    }

    public Vector2 GetAnchorePos()
    {
      if (Object.op_Inequality((Object) this.m_RectTransform, (Object) null))
        return this.anchoredPosition;
      return Vector2.get_zero();
    }

    public enum Constraint
    {
      Flexible,
      FixedColumnCount,
      FixedRowCount,
    }
  }
}
