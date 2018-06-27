// Decompiled with JetBrains decompiler
// Type: CullArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class CullArea : MonoBehaviour
{
  private const int MAX_NUMBER_OF_ALLOWED_CELLS = 250;
  public const int MAX_NUMBER_OF_SUBDIVISIONS = 3;
  public readonly int FIRST_GROUP_ID;
  public readonly int[] SUBDIVISION_FIRST_LEVEL_ORDER;
  public readonly int[] SUBDIVISION_SECOND_LEVEL_ORDER;
  public readonly int[] SUBDIVISION_THIRD_LEVEL_ORDER;
  public Vector2 Center;
  public Vector2 Size;
  public Vector2[] Subdivisions;
  public int NumberOfSubdivisions;
  public bool YIsUpAxis;
  public bool RecreateCellHierarchy;
  private int idCounter;

  public CullArea()
  {
    base.\u002Ector();
  }

  public int CellCount { get; private set; }

  public CellTree CellTree { get; private set; }

  public Dictionary<int, GameObject> Map { get; private set; }

  private void Awake()
  {
    this.idCounter = this.FIRST_GROUP_ID;
    this.CreateCellHierarchy();
  }

  public void OnDrawGizmos()
  {
    this.idCounter = this.FIRST_GROUP_ID;
    if (this.RecreateCellHierarchy)
      this.CreateCellHierarchy();
    this.DrawCells();
  }

  private void CreateCellHierarchy()
  {
    if (!this.IsCellCountAllowed())
    {
      if (Debug.get_isDebugBuild())
      {
        Debug.LogError((object) ("There are too many cells created by your subdivision options. Maximum allowed number of cells is " + (object) (250 - this.FIRST_GROUP_ID) + ". Current number of cells is " + (object) this.CellCount + "."));
        return;
      }
      Application.Quit();
    }
    CellTreeNode cellTreeNode = new CellTreeNode(this.idCounter++, CellTreeNode.ENodeType.Root, (CellTreeNode) null);
    if (this.YIsUpAxis)
    {
      this.Center = new Vector2((float) ((Component) this).get_transform().get_position().x, (float) ((Component) this).get_transform().get_position().y);
      this.Size = new Vector2((float) ((Component) this).get_transform().get_localScale().x, (float) ((Component) this).get_transform().get_localScale().y);
      cellTreeNode.Center = new Vector3((float) this.Center.x, (float) this.Center.y, 0.0f);
      cellTreeNode.Size = new Vector3((float) this.Size.x, (float) this.Size.y, 0.0f);
      cellTreeNode.TopLeft = new Vector3((float) (this.Center.x - this.Size.x / 2.0), (float) (this.Center.y - this.Size.y / 2.0), 0.0f);
      cellTreeNode.BottomRight = new Vector3((float) (this.Center.x + this.Size.x / 2.0), (float) (this.Center.y + this.Size.y / 2.0), 0.0f);
    }
    else
    {
      this.Center = new Vector2((float) ((Component) this).get_transform().get_position().x, (float) ((Component) this).get_transform().get_position().z);
      this.Size = new Vector2((float) ((Component) this).get_transform().get_localScale().x, (float) ((Component) this).get_transform().get_localScale().z);
      cellTreeNode.Center = new Vector3((float) this.Center.x, 0.0f, (float) this.Center.y);
      cellTreeNode.Size = new Vector3((float) this.Size.x, 0.0f, (float) this.Size.y);
      cellTreeNode.TopLeft = new Vector3((float) (this.Center.x - this.Size.x / 2.0), 0.0f, (float) (this.Center.y - this.Size.y / 2.0));
      cellTreeNode.BottomRight = new Vector3((float) (this.Center.x + this.Size.x / 2.0), 0.0f, (float) (this.Center.y + this.Size.y / 2.0));
    }
    this.CreateChildCells(cellTreeNode, 1);
    this.CellTree = new CellTree(cellTreeNode);
    this.RecreateCellHierarchy = false;
  }

  private void CreateChildCells(CellTreeNode parent, int cellLevelInHierarchy)
  {
    if (cellLevelInHierarchy > this.NumberOfSubdivisions)
      return;
    int x = (int) this.Subdivisions[cellLevelInHierarchy - 1].x;
    int y = (int) this.Subdivisions[cellLevelInHierarchy - 1].y;
    float num1 = (float) (parent.Center.x - parent.Size.x / 2.0);
    float num2 = (float) parent.Size.x / (float) x;
    for (int index1 = 0; index1 < x; ++index1)
    {
      for (int index2 = 0; index2 < y; ++index2)
      {
        float num3 = (float) ((double) num1 + (double) index1 * (double) num2 + (double) num2 / 2.0);
        CellTreeNode cellTreeNode = new CellTreeNode(this.idCounter++, this.NumberOfSubdivisions != cellLevelInHierarchy ? CellTreeNode.ENodeType.Node : CellTreeNode.ENodeType.Leaf, parent);
        if (this.YIsUpAxis)
        {
          float num4 = (float) (parent.Center.y - parent.Size.y / 2.0);
          float num5 = (float) parent.Size.y / (float) y;
          float num6 = (float) ((double) num4 + (double) index2 * (double) num5 + (double) num5 / 2.0);
          cellTreeNode.Center = new Vector3(num3, num6, 0.0f);
          cellTreeNode.Size = new Vector3(num2, num5, 0.0f);
          cellTreeNode.TopLeft = new Vector3(num3 - num2 / 2f, num6 - num5 / 2f, 0.0f);
          cellTreeNode.BottomRight = new Vector3(num3 + num2 / 2f, num6 + num5 / 2f, 0.0f);
        }
        else
        {
          float num4 = (float) (parent.Center.z - parent.Size.z / 2.0);
          float num5 = (float) parent.Size.z / (float) y;
          float num6 = (float) ((double) num4 + (double) index2 * (double) num5 + (double) num5 / 2.0);
          cellTreeNode.Center = new Vector3(num3, 0.0f, num6);
          cellTreeNode.Size = new Vector3(num2, 0.0f, num5);
          cellTreeNode.TopLeft = new Vector3(num3 - num2 / 2f, 0.0f, num6 - num5 / 2f);
          cellTreeNode.BottomRight = new Vector3(num3 + num2 / 2f, 0.0f, num6 + num5 / 2f);
        }
        parent.AddChild(cellTreeNode);
        this.CreateChildCells(cellTreeNode, cellLevelInHierarchy + 1);
      }
    }
  }

  private void DrawCells()
  {
    if (this.CellTree != null && this.CellTree.RootNode != null)
      this.CellTree.RootNode.Draw();
    else
      this.RecreateCellHierarchy = true;
  }

  private bool IsCellCountAllowed()
  {
    int num1 = 1;
    int num2 = 1;
    foreach (Vector2 subdivision in this.Subdivisions)
    {
      num1 *= (int) subdivision.x;
      num2 *= (int) subdivision.y;
    }
    this.CellCount = num1 * num2;
    return this.CellCount <= 250 - this.FIRST_GROUP_ID;
  }

  public List<int> GetActiveCells(Vector3 position)
  {
    List<int> activeCells = new List<int>(0);
    this.CellTree.RootNode.GetActiveCells(activeCells, this.YIsUpAxis, position);
    return activeCells;
  }
}
