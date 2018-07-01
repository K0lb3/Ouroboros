// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftSelectedUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class VersusDraftSelectedUnit : MonoBehaviour
  {
    [SerializeField]
    private UnitIcon mUnitIcon;
    [SerializeField]
    private GameObject mSelecting;
    [SerializeField]
    private GameObject mSecretIcon;
    private DataSource mDataSource;

    public VersusDraftSelectedUnit()
    {
      base.\u002Ector();
    }

    public void Initialize()
    {
      if (Object.op_Inequality((Object) this.mUnitIcon, (Object) null) && Object.op_Inequality((Object) this.mUnitIcon.Icon, (Object) null))
        ((Component) this.mUnitIcon.Icon).get_gameObject().SetActive(false);
      this.mDataSource = DataSource.Create(((Component) this).get_gameObject());
    }

    public void Selecting()
    {
      if (!Object.op_Inequality((Object) this.mSelecting, (Object) null))
        return;
      this.mSelecting.SetActive(true);
    }

    public void SetUnit(UnitData unit)
    {
      if (Object.op_Inequality((Object) this.mSecretIcon, (Object) null))
        this.mSecretIcon.SetActive(unit == null);
      if (Object.op_Inequality((Object) this.mUnitIcon, (Object) null) && Object.op_Inequality((Object) this.mUnitIcon.Icon, (Object) null))
        ((Component) this.mUnitIcon.Icon).get_gameObject().SetActive(unit != null);
      if (!Object.op_Inequality((Object) this.mUnitIcon, (Object) null))
        return;
      this.mDataSource.Clear();
      this.mDataSource.Add(typeof (UnitData), (object) unit);
      this.mUnitIcon.UpdateValue();
    }

    public void Select(UnitData unit)
    {
      if (Object.op_Inequality((Object) this.mSelecting, (Object) null))
        this.mSelecting.SetActive(false);
      this.SetUnit(unit);
    }
  }
}
