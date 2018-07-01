// Decompiled with JetBrains decompiler
// Type: SRPG.PartyUnitSlot2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class PartyUnitSlot2 : MonoBehaviour
  {
    public PartyUnitSlot2.SelectEvent OnSelect;
    public SRPG_Button SelectButton;
    public GameObject[] HideIfEmpty;
    public RectTransform Empty;
    public RectTransform NonEmpty;
    private UnitData mUnit;

    public PartyUnitSlot2()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.SelectButton, (Object) null))
        return;
      this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
    }

    public UnitData Unit
    {
      get
      {
        return this.mUnit;
      }
    }

    private void OnButtonClick(SRPG_Button button)
    {
      if (!((Selectable) button).IsInteractable() || this.OnSelect == null)
        return;
      this.OnSelect(this);
    }

    public void SetUnitData(UnitData unit)
    {
      this.mUnit = unit;
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unit);
      bool flag = unit != null;
      for (int index = 0; index < this.HideIfEmpty.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.HideIfEmpty[index], (Object) null))
          this.HideIfEmpty[index].SetActive(flag);
      }
      if (Object.op_Inequality((Object) this.Empty, (Object) null))
        ((Component) this.Empty).get_gameObject().SetActive(!flag);
      if (Object.op_Inequality((Object) this.NonEmpty, (Object) null))
        ((Component) this.NonEmpty).get_gameObject().SetActive(flag);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public delegate void SelectEvent(PartyUnitSlot2 slot);
  }
}
