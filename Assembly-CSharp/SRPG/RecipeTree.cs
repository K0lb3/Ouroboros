// Decompiled with JetBrains decompiler
// Type: SRPG.RecipeTree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class RecipeTree
  {
    private List<RecipeTree> children = new List<RecipeTree>();
    private RecipeTree parent;
    private bool is_common;
    private ItemParam param;

    public RecipeTree(ItemParam param)
    {
      this.param = param;
    }

    public List<RecipeTree> Children
    {
      get
      {
        return this.children;
      }
    }

    public RecipeTree Parent
    {
      get
      {
        return this.parent;
      }
    }

    public bool IsCommon
    {
      get
      {
        return this.is_common;
      }
    }

    public string iname
    {
      get
      {
        if (this.param != null)
          return this.param.iname;
        return (string) null;
      }
    }

    public void SetChild(RecipeTree child)
    {
      child.parent = this;
      this.children.Add(child);
    }

    public void SetIsCommon()
    {
      this.is_common = true;
      if (this.parent == null)
        return;
      this.parent.SetIsCommon();
    }

    public void RemoveLastAt()
    {
      if (this.children == null || this.children.Count <= 0)
        return;
      this.children.RemoveAt(this.children.Count - 1);
    }
  }
}
