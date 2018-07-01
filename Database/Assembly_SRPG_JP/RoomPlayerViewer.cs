// Decompiled with JetBrains decompiler
// Type: SRPG.RoomPlayerViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class RoomPlayerViewer : MonoBehaviour
  {
    public GameObject[] PartyUnitSlots;

    public RoomPlayerViewer()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      JSON_MyPhotonPlayerParam multiPlayerParam = GlobalVars.SelectedMultiPlayerParam;
      if (multiPlayerParam == null || multiPlayerParam.units == null || this.PartyUnitSlots == null)
        return;
      for (int index1 = 0; index1 < this.PartyUnitSlots.Length; ++index1)
      {
        for (int index2 = 0; index2 < multiPlayerParam.units.Length; ++index2)
        {
          if (multiPlayerParam.units[index2].slotID == index1)
          {
            DataSource.Bind<UnitData>(this.PartyUnitSlots[index1], multiPlayerParam.units[index2].unit);
            break;
          }
        }
      }
      DataSource.Bind<JSON_MyPhotonPlayerParam>(((Component) this).get_gameObject(), multiPlayerParam);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }
  }
}
