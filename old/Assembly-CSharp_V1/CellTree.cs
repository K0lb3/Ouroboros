// Decompiled with JetBrains decompiler
// Type: CellTree
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

public class CellTree
{
  public CellTree()
  {
  }

  public CellTree(CellTreeNode root)
  {
    this.RootNode = root;
  }

  public CellTreeNode RootNode { get; private set; }
}
