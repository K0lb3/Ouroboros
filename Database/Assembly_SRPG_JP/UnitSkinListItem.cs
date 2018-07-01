// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSkinListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitSkinListItem : ListItemEvents
  {
    public ListItemEvents.ListItemEvent OnSelectAll;
    public ListItemEvents.ListItemEvent OnRemoveAll;
    public SRPG_Button Button;
    public GameObject Lock;

    public void SelectAll()
    {
      if (this.OnSelectAll == null)
        return;
      this.OnSelectAll(((Component) this).get_gameObject());
    }

    public void RemoveAll()
    {
      if (this.OnRemoveAll == null)
        return;
      this.OnRemoveAll(((Component) this).get_gameObject());
    }
  }
}
