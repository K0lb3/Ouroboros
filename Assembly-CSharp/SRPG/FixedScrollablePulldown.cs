// Decompiled with JetBrains decompiler
// Type: SRPG.FixedScrollablePulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class FixedScrollablePulldown : ScrollablePulldownBase
  {
    public void ResetAllItems()
    {
      using (List<PulldownItem>.Enumerator enumerator = this.Items.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          PulldownItem current = enumerator.Current;
          GameUtility.RemoveComponent<SRPG_Button>(((Component) current).get_gameObject());
          ((Component) current).get_gameObject().SetActive(false);
        }
      }
      this.ResetAllStatus();
    }

    public PulldownItem SetItem(string label, int index, int value)
    {
      if (index < 0 || index >= this.Items.Count)
        return (PulldownItem) null;
      PulldownItem pulldownItem = this.Items[index];
      if (Object.op_Inequality((Object) pulldownItem.Text, (Object) null))
        pulldownItem.Text.set_text(label);
      pulldownItem.Value = value;
      GameObject gameObject = ((Component) pulldownItem).get_gameObject();
      GameUtility.RequireComponent<SRPG_Button>(gameObject).AddListener((SRPG_Button.ButtonClickEvent) (g =>
      {
        this.Selection = value;
        this.ClosePulldown(false);
        this.TriggerItemChange();
      }));
      gameObject.get_transform().SetParent((Transform) this.ItemHolder, false);
      gameObject.SetActive(true);
      return pulldownItem;
    }
  }
}
