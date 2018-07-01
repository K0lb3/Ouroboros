// Decompiled with JetBrains decompiler
// Type: SRPG.SortFilterMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SortFilterMode : MonoBehaviour, ISortableList
  {
    public GameObject Ascending;
    public GameObject Descending;
    public GameObject FilterOn;
    public GameObject FilterOff;

    public SortFilterMode()
    {
      base.\u002Ector();
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      GameUtility.SetGameObjectActive(this.Ascending, ascending);
      GameUtility.SetGameObjectActive(this.Descending, !ascending);
      GameUtility.SetGameObjectActive(this.FilterOn, filters != null);
      GameUtility.SetGameObjectActive(this.FilterOff, filters == null);
    }
  }
}
