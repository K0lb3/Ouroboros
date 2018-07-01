// Decompiled with JetBrains decompiler
// Type: SRPG.UnitMethods
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class UnitMethods : MonoBehaviour
  {
    public UnitMethods()
    {
      base.\u002Ector();
    }

    public void SetUnitToActivePartySlot()
    {
      PartyUnitSlot active = PartyUnitSlot.Active;
      UnitData dataOfClass1 = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (!Object.op_Inequality((Object) active, (Object) null))
        return;
      PartyData dataOfClass2 = DataSource.FindDataOfClass<PartyData>(((Component) active).get_gameObject(), (PartyData) null);
      if (dataOfClass1 != null)
      {
        for (int index = 0; index < dataOfClass2.MAX_UNIT; ++index)
        {
          if (dataOfClass2.GetUnitUniqueID(index) == dataOfClass1.UniqueID)
            dataOfClass2.SetUnitUniqueID(index, 0L);
        }
        dataOfClass2.SetUnitUniqueID(active.Index, dataOfClass1.UniqueID);
      }
      else
        dataOfClass2.SetUnitUniqueID(active.Index, 0L);
      FlowNodeEvent<FlowNode_OnPartyChange>.Invoke();
    }
  }
}
