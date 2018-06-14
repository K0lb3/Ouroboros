// Decompiled with JetBrains decompiler
// Type: SRPG.UnitSkinListItem
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
