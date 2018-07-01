// Decompiled with JetBrains decompiler
// Type: SRPG.SortFilterMode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
