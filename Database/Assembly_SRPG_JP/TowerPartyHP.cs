// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPartyHP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TowerPartyHP : MonoBehaviour, IGameParameter
  {
    public Slider mSlider;

    public TowerPartyHP()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Refresh();
    }

    public void UpdateValue()
    {
      this.Refresh();
    }

    public void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mSlider, (UnityEngine.Object) null))
        return;
      UnitData UnitData = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (UnitData == null)
      {
        ((Component) this.mSlider).get_gameObject().SetActive(false);
      }
      else
      {
        TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
        if (towerResuponse == null)
          ((Component) this.mSlider).get_gameObject().SetActive(false);
        else if (towerResuponse.pdeck == null)
        {
          ((Component) this.mSlider).get_gameObject().SetActive(true);
          int hp = (int) UnitData.Status.param.hp;
          this.SetSliderValue(hp, hp);
        }
        else
        {
          TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>) (x => x.unitname == UnitData.UnitParam.iname));
          if (playerUnit == null)
          {
            ((Component) this.mSlider).get_gameObject().SetActive(true);
            int hp = (int) UnitData.Status.param.hp;
            this.SetSliderValue(hp, hp);
          }
          else if (playerUnit.isDied)
          {
            ((Component) this.mSlider).get_gameObject().SetActive(false);
          }
          else
          {
            ((Component) this.mSlider).get_gameObject().SetActive(true);
            this.SetSliderValue(Mathf.Max((int) UnitData.Status.param.hp - playerUnit.dmg, 1), (int) UnitData.Status.param.hp);
          }
        }
      }
    }

    private void SetSliderValue(int value, int maxValue)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mSlider, (UnityEngine.Object) null))
        return;
      this.mSlider.set_maxValue((float) maxValue);
      this.mSlider.set_minValue(0.0f);
      this.mSlider.set_value((float) value);
    }
  }
}
