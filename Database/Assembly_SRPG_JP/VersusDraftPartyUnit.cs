// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftPartyUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusDraftPartyUnit : MonoBehaviour
  {
    private static VersusDraftPartyEdit mVersusDraftPartyEdit;
    private UnitData mUnitData;
    private VersusDraftPartySlot mTargetSlot;
    [SerializeField]
    private GameObject mSelected;

    public VersusDraftPartyUnit()
    {
      base.\u002Ector();
    }

    public static VersusDraftPartyEdit VersusDraftPartyEdit
    {
      get
      {
        return VersusDraftPartyUnit.mVersusDraftPartyEdit;
      }
      set
      {
        VersusDraftPartyUnit.mVersusDraftPartyEdit = value;
      }
    }

    public UnitData UnitData
    {
      get
      {
        return this.mUnitData;
      }
    }

    public bool IsSetSlot
    {
      get
      {
        return Object.op_Inequality((Object) this.mTargetSlot, (Object) null);
      }
    }

    public void SetUp(UnitData unit)
    {
      this.mUnitData = unit;
      this.mSelected.SetActive(false);
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), this.mUnitData);
      ((Component) this).get_gameObject().SetActive(true);
    }

    public void OnClick(Button button)
    {
      if (Object.op_Equality((Object) VersusDraftPartySlot.CurrentSelected, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.mTargetSlot, (Object) null))
      {
        VersusDraftPartySlot mTargetSlot = this.mTargetSlot;
        this.mTargetSlot.PartyUnit.Reset();
        if (Object.op_Inequality((Object) VersusDraftPartySlot.CurrentSelected.PartyUnit, (Object) null))
          VersusDraftPartySlot.CurrentSelected.PartyUnit.Select(mTargetSlot);
      }
      else if (Object.op_Inequality((Object) VersusDraftPartySlot.CurrentSelected.PartyUnit, (Object) null))
        VersusDraftPartySlot.CurrentSelected.PartyUnit.Reset();
      this.Select(VersusDraftPartySlot.CurrentSelected);
      VersusDraftPartyUnit.mVersusDraftPartyEdit.SelectNextSlot();
    }

    public void Select(VersusDraftPartySlot slot)
    {
      this.mSelected.SetActive(true);
      this.mTargetSlot = slot;
      this.mTargetSlot.SetUnit(this);
    }

    public void Reset()
    {
      this.mSelected.SetActive(false);
      if (!Object.op_Inequality((Object) this.mTargetSlot, (Object) null))
        return;
      this.mTargetSlot.SetUnit((VersusDraftPartyUnit) null);
      this.mTargetSlot = (VersusDraftPartySlot) null;
    }
  }
}
