// Decompiled with JetBrains decompiler
// Type: SRPG.SupportElementList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  public class SupportElementList : MonoBehaviour
  {
    [SerializeField]
    private GameObject[] m_SupportUnits;

    public SupportElementList()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
    }

    public void Clear()
    {
      if (this.m_SupportUnits == null)
      {
        DebugUtility.LogError("m_SupportUnitsがnullです。");
      }
      else
      {
        for (int element = 0; element < this.m_SupportUnits.Length; ++element)
          this.Refresh(element, (UnitData) null);
      }
    }

    public void Refresh(int element, UnitData unit)
    {
      if (this.m_SupportUnits == null)
        DebugUtility.LogError("m_SupportUnitsがnullです。");
      else if (this.m_SupportUnits.Length < Enum.GetValues(typeof (EElement)).Length)
        DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
      else if (element >= this.m_SupportUnits.Length)
      {
        DebugUtility.LogError("unitsの数が足りません。");
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_SupportUnits[element], (UnityEngine.Object) null))
          return;
        GameObject gameObject = this.m_SupportUnits[element].get_gameObject();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          return;
        SerializeValueBehaviour component = (SerializeValueBehaviour) gameObject.GetComponent<SerializeValueBehaviour>();
        DataSource dataSource = DataSource.Create(gameObject);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) dataSource, (UnityEngine.Object) null))
          return;
        dataSource.Clear();
        if (unit != null)
        {
          dataSource.Add(typeof (UnitData), (object) unit);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            component.list.SetInteractable("btn", true);
            component.list.SetActive(1, true);
          }
        }
        else if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        {
          component.list.SetInteractable("btn", false);
          component.list.SetActive(1, false);
        }
        GameParameter.UpdateAll(gameObject);
      }
    }

    public void Refresh(long[] uniqs)
    {
      if (this.m_SupportUnits == null)
        DebugUtility.LogError("m_SupportUnitsがnullです。");
      else if (uniqs == null)
        DebugUtility.LogError("unitsがnullです。");
      else if (this.m_SupportUnits.Length < Enum.GetValues(typeof (EElement)).Length)
        DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
      else if (uniqs.Length < this.m_SupportUnits.Length)
      {
        DebugUtility.LogError("unitsの数が足りません。");
      }
      else
      {
        for (int element = 0; element < uniqs.Length; ++element)
          this.Refresh(element, MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(uniqs[element]));
        GameParameter.UpdateAll(((Component) this).get_gameObject());
      }
    }

    public void Refresh(UnitData[] units)
    {
      if (this.m_SupportUnits == null)
        DebugUtility.LogError("m_SupportUnitsがnullです。");
      else if (units == null)
        DebugUtility.LogError("unitsがnullです。");
      else if (this.m_SupportUnits.Length < Enum.GetValues(typeof (EElement)).Length)
        DebugUtility.LogError("m_SupportUnitsの数が足りません。Inspectorからの設定を確認してください。");
      else if (units.Length < this.m_SupportUnits.Length)
      {
        DebugUtility.LogError("unitsの数が足りません。");
      }
      else
      {
        for (int element = 0; element < units.Length; ++element)
          this.Refresh(element, units[element]);
        GameParameter.UpdateAll(((Component) this).get_gameObject());
      }
    }
  }
}
