// Decompiled with JetBrains decompiler
// Type: SRPG.VersusDraftUnit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusDraftUnit : MonoBehaviour
  {
    private static List<VersusDraftUnit> mCurrentSelected = new List<VersusDraftUnit>();
    private static int mCursorIndex = 0;
    private static VersusDraftList mVersusDraftList;
    [SerializeField]
    private UnitIcon mUnitIcon;
    [SerializeField]
    private GameObject mCursorPlayer;
    [SerializeField]
    private GameObject mCursorEnemy;
    [SerializeField]
    private GameObject mSecretIcon;
    [SerializeField]
    private GameObject mPickPlayer;
    [SerializeField]
    private GameObject mPickEnemy;
    private UnitData mUnitData;
    private bool mIsSelected;
    private bool mIsHidden;

    public VersusDraftUnit()
    {
      base.\u002Ector();
    }

    public static VersusDraftList VersusDraftList
    {
      get
      {
        return VersusDraftUnit.mVersusDraftList;
      }
      set
      {
        VersusDraftUnit.mVersusDraftList = value;
      }
    }

    public static List<VersusDraftUnit> CurrentSelectCursors
    {
      get
      {
        return VersusDraftUnit.mCurrentSelected;
      }
      set
      {
        VersusDraftUnit.mCurrentSelected = value;
      }
    }

    public UnitData UnitData
    {
      get
      {
        return this.mUnitData;
      }
    }

    public bool IsSelected
    {
      get
      {
        return this.mIsSelected;
      }
    }

    public void SetUp(UnitData unit, Transform parent, bool is_hidden)
    {
      this.mUnitData = unit;
      this.mIsHidden = is_hidden;
      if (!this.mIsHidden)
      {
        DataSource.Bind<UnitData>(((Component) this).get_gameObject(), this.mUnitData);
      }
      else
      {
        this.mSecretIcon.SetActive(true);
        this.mUnitIcon.Tooltip = false;
      }
      ((Component) this).get_transform().SetParent(parent, false);
      ((Component) this).get_gameObject().SetActive(true);
    }

    public void OnClick(Button button)
    {
      if (!VersusDraftList.VersusDraftTurnOwn || this.mIsSelected || VersusDraftUnit.mCurrentSelected.Contains(this))
        return;
      this.SelectUnit(true);
      if (!Object.op_Inequality((Object) VersusDraftUnit.mVersusDraftList, (Object) null))
        return;
      VersusDraftUnit.mVersusDraftList.SelectUnit();
    }

    public static void ResetSelectUnit()
    {
      for (int index = 0; index < VersusDraftUnit.mCurrentSelected.Count; ++index)
      {
        if (Object.op_Inequality((Object) VersusDraftUnit.mCurrentSelected[index], (Object) null))
        {
          VersusDraftUnit.mCurrentSelected[index].mCursorPlayer.SetActive(false);
          VersusDraftUnit.mCurrentSelected[index].mCursorEnemy.SetActive(false);
          VersusDraftUnit.mCurrentSelected[index] = (VersusDraftUnit) null;
        }
      }
      VersusDraftUnit.mCursorIndex = 0;
    }

    public void SelectUnit(bool isPlayer = true)
    {
      if (isPlayer)
      {
        if (Object.op_Inequality((Object) VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex], (Object) null))
          VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex].mCursorPlayer.SetActive(false);
        this.mCursorPlayer.SetActive(true);
        VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex] = this;
        VersusDraftUnit.VersusDraftList.SetUnit(!VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex].mIsHidden ? VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex].UnitData : (UnitData) null, VersusDraftUnit.mCursorIndex);
      }
      else
      {
        if (Object.op_Inequality((Object) VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex], (Object) null))
          VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex].mCursorEnemy.SetActive(false);
        this.mCursorEnemy.SetActive(true);
        VersusDraftUnit.mCurrentSelected[VersusDraftUnit.mCursorIndex] = this;
      }
      ++VersusDraftUnit.mCursorIndex;
      if (VersusDraftUnit.VersusDraftList.SelectableUnitCount > VersusDraftUnit.mCursorIndex)
        return;
      VersusDraftUnit.mCursorIndex = 0;
    }

    public void DecideUnit(bool isPlayer = true)
    {
      if (isPlayer)
      {
        if (Object.op_Inequality((Object) this.mPickPlayer, (Object) null))
        {
          this.mPickPlayer.SetActive(true);
          VersusDraftList.VersusDraftUnitDataListPlayer.Add(this.mUnitData);
        }
      }
      else if (Object.op_Inequality((Object) this.mPickEnemy, (Object) null))
      {
        VersusDraftList.VersusDraftUnitDataListEnemy.Add(this.mUnitData);
        this.mPickEnemy.SetActive(true);
      }
      this.mIsSelected = true;
    }
  }
}
