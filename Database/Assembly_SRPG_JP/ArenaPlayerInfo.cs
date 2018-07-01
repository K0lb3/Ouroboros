// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaPlayerInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class ArenaPlayerInfo : MonoBehaviour
  {
    [Space(10f)]
    public GameObject unit1;
    public GameObject unit2;
    public GameObject unit3;

    public ArenaPlayerInfo()
    {
      base.\u002Ector();
    }

    private void OnEnable()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(((Component) this).get_gameObject(), (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      DataSource.Bind<ArenaPlayer>(this.unit1, dataOfClass);
      DataSource.Bind<ArenaPlayer>(this.unit2, dataOfClass);
      DataSource.Bind<ArenaPlayer>(this.unit3, dataOfClass);
      ((UnitIcon) this.unit1.GetComponent<UnitIcon>()).UpdateValue();
      ((UnitIcon) this.unit2.GetComponent<UnitIcon>()).UpdateValue();
      ((UnitIcon) this.unit3.GetComponent<UnitIcon>()).UpdateValue();
    }
  }
}
