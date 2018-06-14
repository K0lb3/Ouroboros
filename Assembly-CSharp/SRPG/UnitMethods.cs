// Decompiled with JetBrains decompiler
// Type: SRPG.UnitMethods
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
