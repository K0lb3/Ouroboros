// Decompiled with JetBrains decompiler
// Type: SRPG.SupportElementListRootWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SupportElementListRootWindow : MonoBehaviour
  {
    [SerializeField]
    private SupportElementList m_UnitElementList;
    private long[] m_UnitUniequeIDs;

    public SupportElementListRootWindow()
    {
      base.\u002Ector();
    }

    private void DebugRefreshSupportUnitList()
    {
      long[] iids = new long[Enum.GetValues(typeof (EElement)).Length];
      iids[0] = (long) GlobalVars.SelectedSupportUnitUniqueID;
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      for (int index1 = 1; index1 < iids.Length; ++index1)
      {
        for (int index2 = 0; index2 < units.Count; ++index2)
        {
          if (units[index2].Element == (EElement) index1)
          {
            iids[index1] = units[index2].UniqueID;
            break;
          }
        }
      }
      this.SetSupportUnitData(iids);
    }

    public void Clear()
    {
      for (int index = 1; index < this.m_UnitUniequeIDs.Length; ++index)
        this.m_UnitUniequeIDs[index] = 0L;
      this.m_UnitElementList.Refresh(this.m_UnitUniequeIDs);
    }

    public void SetSupportUnitData(long[] iids)
    {
      UnitData[] units = new UnitData[Enum.GetValues(typeof (EElement)).Length];
      for (int index = 0; index < iids.Length; ++index)
        units[index] = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(iids[index]);
      this.SetSupportUnitData(units);
    }

    public void SetSupportUnitData(UnitData[] units)
    {
      if (units == null)
      {
        for (int index = 0; index < this.m_UnitUniequeIDs.Length; ++index)
          this.m_UnitUniequeIDs[index] = 0L;
      }
      else
      {
        for (int index = 0; index < units.Length; ++index)
          this.m_UnitUniequeIDs[index] = units[index] != null ? units[index].UniqueID : 0L;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitElementList, (UnityEngine.Object) null))
        return;
      this.m_UnitElementList.Refresh(units);
    }

    public void SetSupportUnitData(int element, long uniqId)
    {
      if (element < 0 || element >= this.m_UnitUniequeIDs.Length)
        return;
      this.m_UnitUniequeIDs[element] = uniqId;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_UnitElementList, (UnityEngine.Object) null))
        return;
      this.m_UnitElementList.Refresh(element, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(uniqId));
    }

    public FlowNode_ReqSupportSet.OwnSupportData[] GetSupportUnitData()
    {
      FlowNode_ReqSupportSet.OwnSupportData[] ownSupportDataArray = new FlowNode_ReqSupportSet.OwnSupportData[Enum.GetValues(typeof (EElement)).Length];
      for (int index = 0; index < this.m_UnitUniequeIDs.Length; ++index)
      {
        if (this.m_UnitUniequeIDs[index] == 0L)
        {
          ownSupportDataArray[index] = (FlowNode_ReqSupportSet.OwnSupportData) null;
        }
        else
        {
          ownSupportDataArray[index] = new FlowNode_ReqSupportSet.OwnSupportData();
          ownSupportDataArray[index].m_Element = (EElement) index;
          ownSupportDataArray[index].m_UniqueID = this.m_UnitUniequeIDs[index];
        }
      }
      return ownSupportDataArray;
    }

    public void OnEvent(string key, string value)
    {
      string key1 = key;
      if (key1 == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (SupportElementListRootWindow.\u003C\u003Ef__switch\u0024map14 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SupportElementListRootWindow.\u003C\u003Ef__switch\u0024map14 = new Dictionary<string, int>(3)
        {
          {
            "SET",
            0
          },
          {
            "REMOVE",
            1
          },
          {
            "REMOVEALL",
            2
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (!SupportElementListRootWindow.\u003C\u003Ef__switch\u0024map14.TryGetValue(key1, out num))
        return;
      switch (num)
      {
        case 0:
          if (!UnitListRootWindow.hasInstance)
            break;
          this.SetSupportUnitData(UnitListRootWindow.instance.GetData<int>("data_element"), GlobalVars.SelectedUnitUniqueID.Get());
          break;
        case 1:
          if (!UnitListRootWindow.hasInstance)
            break;
          this.SetSupportUnitData(UnitListRootWindow.instance.GetData<int>("data_element"), 0L);
          break;
        case 2:
          this.Clear();
          break;
      }
    }
  }
}
