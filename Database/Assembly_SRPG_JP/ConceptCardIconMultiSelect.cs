// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardIconMultiSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardIconMultiSelect : ConceptCardIcon
  {
    [SerializeField]
    private GameObject mDisable;
    [SerializeField]
    private GameObject mSelect;

    public void RefreshEnableParam(bool enable)
    {
      if (this.ConceptCard == null)
        return;
      if (Object.op_Inequality((Object) this.mDisable, (Object) null))
        this.mDisable.SetActive(!enable);
      Button component = (Button) ((Component) ((Component) this).get_transform()).get_gameObject().GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      ((Behaviour) component).set_enabled(enable);
    }

    public void RefreshSelectParam(bool selected)
    {
      if (this.ConceptCard == null || !Object.op_Inequality((Object) this.mSelect, (Object) null))
        return;
      this.mSelect.SetActive(selected);
    }
  }
}
