// Decompiled with JetBrains decompiler
// Type: SRPG.FilterDispatcher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class FilterDispatcher : MonoBehaviour, ISortableList
  {
    public GameObject[] Targets;

    public FilterDispatcher()
    {
      base.\u002Ector();
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      if (this.Targets == null)
        return;
      foreach (GameObject target in this.Targets)
      {
        if (!Object.op_Equality((Object) target, (Object) null))
        {
          ISortableList component = (ISortableList) target.GetComponent<ISortableList>();
          if (component != null)
            component.SetSortMethod(method, ascending, filters);
        }
      }
    }
  }
}
