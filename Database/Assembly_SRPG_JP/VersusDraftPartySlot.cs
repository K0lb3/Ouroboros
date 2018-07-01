// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftPartySlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusDraftPartySlot : MonoBehaviour
  {
    private static VersusDraftPartyEdit mVersusDraftPartyEdit;
    private static VersusDraftPartySlot mCurrentSelected;
    [SerializeField]
    private GameObject mLeaderIcon;
    [SerializeField]
    private GameObject mLocked;
    [SerializeField]
    private GameObject mSelect;
    [SerializeField]
    private Selectable mSelectable;
    private bool mIsLeader;
    private bool mIsLock;
    private DataSource mDataSource;
    private VersusDraftPartyUnit mPartyUnit;

    public VersusDraftPartySlot()
    {
      base.\u002Ector();
    }

    public static VersusDraftPartyEdit VersusDraftPartyEdit
    {
      get
      {
        return VersusDraftPartySlot.mVersusDraftPartyEdit;
      }
      set
      {
        VersusDraftPartySlot.mVersusDraftPartyEdit = value;
      }
    }

    public static VersusDraftPartySlot CurrentSelected
    {
      get
      {
        return VersusDraftPartySlot.mCurrentSelected;
      }
    }

    public bool IsLeader
    {
      get
      {
        return this.mIsLeader;
      }
    }

    public bool IsLock
    {
      get
      {
        return this.mIsLock;
      }
    }

    public void SetUp(bool is_leader, bool is_locked)
    {
      this.mIsLeader = is_leader;
      this.mIsLock = is_locked;
      if (is_leader)
      {
        this.mLeaderIcon.SetActive(true);
        this.SelectSlot(true);
      }
      if (is_locked)
      {
        this.mLocked.SetActive(true);
        this.mSelectable.set_interactable(false);
      }
      this.mDataSource = DataSource.Create(((Component) this).get_gameObject());
      ((Component) this).get_gameObject().SetActive(true);
    }

    public void OnClick(Button button)
    {
      if (Object.op_Equality((Object) VersusDraftPartySlot.mCurrentSelected, (Object) this))
        return;
      this.SelectSlot(true);
    }

    public void SelectSlot(bool selected)
    {
      if (selected)
      {
        if (Object.op_Inequality((Object) VersusDraftPartySlot.mCurrentSelected, (Object) null))
          VersusDraftPartySlot.mCurrentSelected.SelectSlot(false);
        this.mSelect.SetActive(true);
        VersusDraftPartySlot.mCurrentSelected = this;
      }
      else
        this.mSelect.SetActive(false);
    }

    public VersusDraftPartyUnit PartyUnit
    {
      get
      {
        return this.mPartyUnit;
      }
    }

    public void SetUnit(VersusDraftPartyUnit partyUnit)
    {
      if (Object.op_Inequality((Object) this.mDataSource, (Object) null))
        this.mDataSource.Clear();
      if (Object.op_Inequality((Object) partyUnit, (Object) null))
        this.mDataSource.Add(typeof (UnitData), (object) partyUnit.UnitData);
      this.mPartyUnit = partyUnit;
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      VersusDraftPartySlot.mVersusDraftPartyEdit.UpdateParty(this.mIsLeader);
    }
  }
}
