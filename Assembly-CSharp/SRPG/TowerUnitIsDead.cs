// Decompiled with JetBrains decompiler
// Type: SRPG.TowerUnitIsDead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  public class TowerUnitIsDead : MonoBehaviour, IGameParameter
  {
    public GameObject dead;
    public GameObject target;
    public CanvasGroup canvas_group;

    public TowerUnitIsDead()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TowerUnitIsDead.\u003CUpdateValue\u003Ec__AnonStorey382 valueCAnonStorey382 = new TowerUnitIsDead.\u003CUpdateValue\u003Ec__AnonStorey382();
      this.dead.SetActive(false);
      // ISSUE: reference to a compiler-generated field
      valueCAnonStorey382.data = DataSource.FindDataOfClass<UnitData>(this.target, (UnitData) null);
      // ISSUE: reference to a compiler-generated field
      if (valueCAnonStorey382.data == null)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (towerResuponse.pdeck == null)
        return;
      // ISSUE: reference to a compiler-generated method
      TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find(new Predicate<TowerResuponse.PlayerUnit>(valueCAnonStorey382.\u003C\u003Em__42D));
      this.dead.SetActive(playerUnit != null && playerUnit.isDied);
    }
  }
}
