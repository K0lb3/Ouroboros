// Decompiled with JetBrains decompiler
// Type: SRPG.TowerUnitIsDead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
      this.dead.SetActive(false);
      UnitData data = DataSource.FindDataOfClass<UnitData>(this.target, (UnitData) null);
      if (data == null)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (towerResuponse.pdeck == null)
        return;
      TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>) (x => data.UnitParam.iname == x.unitname));
      this.dead.SetActive(playerUnit != null && playerUnit.isDied);
    }
  }
}
