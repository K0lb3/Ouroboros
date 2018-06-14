// Decompiled with JetBrains decompiler
// Type: SRPG.TowerPartyHP
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TowerPartyHP.\u003CRefresh\u003Ec__AnonStorey273 refreshCAnonStorey273 = new TowerPartyHP.\u003CRefresh\u003Ec__AnonStorey273();
      if (Object.op_Equality((Object) this.mSlider, (Object) null))
        return;
      // ISSUE: reference to a compiler-generated field
      refreshCAnonStorey273.UnitData = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      // ISSUE: reference to a compiler-generated field
      if (refreshCAnonStorey273.UnitData == null)
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
          // ISSUE: reference to a compiler-generated field
          int hp = (int) refreshCAnonStorey273.UnitData.Status.param.hp;
          this.SetSliderValue(hp, hp);
        }
        else
        {
          // ISSUE: reference to a compiler-generated method
          TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find(new Predicate<TowerResuponse.PlayerUnit>(refreshCAnonStorey273.\u003C\u003Em__2EF));
          if (playerUnit == null)
          {
            ((Component) this.mSlider).get_gameObject().SetActive(true);
            // ISSUE: reference to a compiler-generated field
            int hp = (int) refreshCAnonStorey273.UnitData.Status.param.hp;
            this.SetSliderValue(hp, hp);
          }
          else if (playerUnit.isDied)
          {
            ((Component) this.mSlider).get_gameObject().SetActive(false);
          }
          else
          {
            ((Component) this.mSlider).get_gameObject().SetActive(true);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            this.SetSliderValue(Mathf.Max((int) refreshCAnonStorey273.UnitData.Status.param.hp - playerUnit.dmg, 1), (int) refreshCAnonStorey273.UnitData.Status.param.hp);
          }
        }
      }
    }

    private void SetSliderValue(int value, int maxValue)
    {
      if (!Object.op_Inequality((Object) this.mSlider, (Object) null))
        return;
      this.mSlider.set_maxValue((float) maxValue);
      this.mSlider.set_minValue(0.0f);
      this.mSlider.set_value((float) value);
    }
  }
}
