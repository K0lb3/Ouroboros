// Decompiled with JetBrains decompiler
// Type: CellTreeNode
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CellTreeNode
{
  public int Id;
  public Vector3 Center;
  public Vector3 Size;
  public Vector3 TopLeft;
  public Vector3 BottomRight;
  public CellTreeNode.ENodeType NodeType;
  public CellTreeNode Parent;
  public List<CellTreeNode> Childs;
  private float maxDistance;

  public CellTreeNode()
  {
  }

  public CellTreeNode(int id, CellTreeNode.ENodeType nodeType, CellTreeNode parent)
  {
    this.Id = id;
    this.NodeType = nodeType;
    this.Parent = parent;
  }

  public void AddChild(CellTreeNode child)
  {
    if (this.Childs == null)
      this.Childs = new List<CellTreeNode>(1);
    this.Childs.Add(child);
  }

  public void Draw()
  {
  }

  public void GetAllLeafNodes(List<CellTreeNode> leafNodes)
  {
    if (this.Childs != null)
    {
      using (List<CellTreeNode>.Enumerator enumerator = this.Childs.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.GetAllLeafNodes(leafNodes);
      }
    }
    else
      leafNodes.Add(this);
  }

  public void GetInsideCells(List<int> insideCells, bool yIsUpAxis, Vector3 position)
  {
    if (!this.IsPointInsideCell(yIsUpAxis, position))
      return;
    insideCells.Add(this.Id);
    if (this.Childs == null)
      return;
    using (List<CellTreeNode>.Enumerator enumerator = this.Childs.GetEnumerator())
    {
      while (enumerator.MoveNext())
        enumerator.Current.GetInsideCells(insideCells, yIsUpAxis, position);
    }
  }

  public void GetNearbyCells(List<int> nearbyCells, bool yIsUpAxis, Vector3 position)
  {
    if (!this.IsPointNearCell(yIsUpAxis, position))
      return;
    if (this.NodeType != CellTreeNode.ENodeType.Leaf)
    {
      using (List<CellTreeNode>.Enumerator enumerator = this.Childs.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.GetNearbyCells(nearbyCells, yIsUpAxis, position);
      }
    }
    else
      nearbyCells.Add(this.Id);
  }

  public bool IsPointInsideCell(bool yIsUpAxis, Vector3 point)
  {
    if (point.x < this.TopLeft.x || point.x > this.BottomRight.x)
      return false;
    if (yIsUpAxis)
    {
      if (point.y >= this.TopLeft.y && point.y <= this.BottomRight.y)
        return true;
    }
    else if (point.z >= this.TopLeft.z && point.z <= this.BottomRight.z)
      return true;
    return false;
  }

  public bool IsPointNearCell(bool yIsUpAxis, Vector3 point)
  {
    if ((double) this.maxDistance == 0.0)
      this.maxDistance = (float) ((this.Size.x + this.Size.y + this.Size.z) / 2.0);
    Vector3 vector3 = Vector3.op_Subtraction(point, this.Center);
    // ISSUE: explicit reference operation
    return (double) ((Vector3) @vector3).get_sqrMagnitude() <= (double) this.maxDistance * (double) this.maxDistance;
  }

  public enum ENodeType
  {
    Root,
    Node,
    Leaf,
  }
}
