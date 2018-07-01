// Decompiled with JetBrains decompiler
// Type: SRPG.OrdealTeamPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class OrdealTeamPanel : MonoBehaviour
  {
    public GameObject UnitSlotContainer;
    public OrdealUnitSlot[] UnitSlots;
    public OrdealUnitSlot SupportSlot;
    public Text TotalAtack;
    public Text TeamName;
    public Button Button;
    private int mUnitCount;

    public OrdealTeamPanel()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    private void Reset()
    {
      if (this.UnitSlots != null)
      {
        foreach (OrdealUnitSlot unitSlot in this.UnitSlots)
          unitSlot.Unit.SetActive(false);
      }
      if (Object.op_Inequality((Object) this.SupportSlot, (Object) null))
        this.SupportSlot.Unit.SetActive(false);
      this.mUnitCount = 0;
    }

    public void Add(UnitData unitData)
    {
      if (this.mUnitCount < this.UnitSlots.Length)
      {
        OrdealUnitSlot unitSlot = this.UnitSlots[this.mUnitCount];
        unitSlot.Unit.SetActive(true);
        DataSource.Bind<UnitData>(unitSlot.Unit.get_gameObject(), unitData);
        GameParameter.UpdateAll(unitSlot.Unit.get_gameObject());
      }
      ++this.mUnitCount;
    }

    public void SetSupport(SupportData supportData)
    {
      DataSource.Bind<SupportData>(this.SupportSlot.Unit.get_gameObject(), supportData);
      this.SupportSlot.Unit.SetActive(true);
    }
  }
}
