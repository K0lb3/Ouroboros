// Decompiled with JetBrains decompiler
// Type: SRPG.TowerUnitIsDead
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
      TowerUnitIsDead.\u003CUpdateValue\u003Ec__AnonStorey277 valueCAnonStorey277 = new TowerUnitIsDead.\u003CUpdateValue\u003Ec__AnonStorey277();
      this.dead.SetActive(false);
      // ISSUE: reference to a compiler-generated field
      valueCAnonStorey277.data = DataSource.FindDataOfClass<UnitData>(this.target, (UnitData) null);
      // ISSUE: reference to a compiler-generated field
      if (valueCAnonStorey277.data == null)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (towerResuponse.pdeck == null)
        return;
      // ISSUE: reference to a compiler-generated method
      TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find(new Predicate<TowerResuponse.PlayerUnit>(valueCAnonStorey277.\u003C\u003Em__2FD));
      this.dead.SetActive(playerUnit != null && playerUnit.isDied);
    }
  }
}
