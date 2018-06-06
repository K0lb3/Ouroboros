// Decompiled with JetBrains decompiler
// Type: SRPG.TargetPlate
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TargetPlate : MonoBehaviour
  {
    public GameObject NextTargetArrow;
    public GameObject PrevTargetArrow;
    public GameObject AttackInfoPlate;
    public GameObject HealValue;
    public GameObject HealOutPut;
    public GameObject DamageValue;
    public GameObject DamageOutPut;
    public GameObject CriticalRate;
    public GameObject CriticalRateOutPut;
    public GameObject HitRate;
    public GameObject HitRateOutPut;
    public GradientGauge HpGauge;
    public GradientGauge MpGauge;
    public StatusEffect StatusEffects;
    private Unit mSelectedUnit;
    private WindowController mWindowController;
    private TargetPlate.GaugeParam mHpGaugeParam;
    private TargetPlate.GaugeParam mMpGaugeParam;
    private TargetPlate.PlateUnitParam mUnitParam;
    public GameObject GimmickDescription;

    public TargetPlate()
    {
      base.\u002Ector();
    }

    public WindowController WindowController
    {
      get
      {
        if (Object.op_Equality((Object) this.mWindowController, (Object) null))
          this.mWindowController = (WindowController) ((Component) this).get_gameObject().GetComponent<WindowController>();
        return this.mWindowController;
      }
    }

    private void SetActive(GameObject targetObject, bool isActive)
    {
      if (!Object.op_Inequality((Object) targetObject, (Object) null))
        return;
      targetObject.SetActive(isActive);
    }

    private void SetTextValue(GameObject targetObject, string value)
    {
      if (Object.op_Equality((Object) targetObject, (Object) null))
        return;
      Text component = (Text) targetObject.GetComponent<Text>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.set_text(value);
    }

    public void SetNoAction(Unit unit)
    {
      this.SetActive(this.HealValue, false);
      this.SetActive(this.DamageValue, false);
      this.SetActive(this.CriticalRate, false);
      this.SetActive(this.HitRate, false);
      this.SetActive(this.AttackInfoPlate, false);
      this.mSelectedUnit = unit;
      DataSource.Bind<Unit>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void SetAttackAction(Unit unit, int damageValue, int criticalRate, int hitRate)
    {
      this.SetActive(this.HealValue, false);
      this.SetActive(this.DamageValue, true);
      this.SetActive(this.CriticalRate, true);
      this.SetActive(this.HitRate, true);
      this.SetActive(this.AttackInfoPlate, true);
      this.SetTextValue(this.DamageOutPut, damageValue.ToString());
      this.SetTextValue(this.CriticalRateOutPut, criticalRate.ToString());
      this.SetTextValue(this.HitRateOutPut, hitRate.ToString());
      this.mSelectedUnit = unit;
      DataSource.Bind<Unit>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void SetSkillAction(Unit unit, int damageValue, int criticalRate, int hitRate)
    {
      this.SetActive(this.HealValue, false);
      this.SetActive(this.DamageValue, true);
      this.SetActive(this.CriticalRate, false);
      this.SetActive(this.HitRate, true);
      this.SetActive(this.AttackInfoPlate, true);
      this.SetTextValue(this.DamageOutPut, damageValue.ToString());
      this.SetTextValue(this.HitRateOutPut, hitRate.ToString());
      this.mSelectedUnit = unit;
      DataSource.Bind<Unit>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void SetHealAction(Unit unit, int healValue, int criticalRate, int hitRate)
    {
      this.SetActive(this.HealValue, true);
      this.SetActive(this.DamageValue, false);
      this.SetActive(this.CriticalRate, false);
      this.SetActive(this.HitRate, false);
      this.SetActive(this.AttackInfoPlate, true);
      this.SetTextValue(this.HealOutPut, healValue.ToString());
      this.mSelectedUnit = unit;
      DataSource.Bind<Unit>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void toggleTargetArrow(ButtonExt.ButtonClickEvent listener, bool active, bool isNextArrow)
    {
      GameObject gameObject = !isNextArrow ? this.PrevTargetArrow : this.NextTargetArrow;
      if (listener == null || Object.op_Equality((Object) gameObject, (Object) null))
        return;
      Button component = (Button) gameObject.GetComponent<Button>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      gameObject.SetActive(active);
      if (active)
        component.AddClickListener(listener);
      else
        component.RemoveClickListener(listener);
    }

    public void ActivateNextTargetArrow(ButtonExt.ButtonClickEvent listener)
    {
      this.toggleTargetArrow(listener, true, true);
    }

    public void ActivatePrevTargetArrow(ButtonExt.ButtonClickEvent listener)
    {
      this.toggleTargetArrow(listener, true, false);
    }

    public void DeactivateNextTargetArrow(ButtonExt.ButtonClickEvent listener)
    {
      this.toggleTargetArrow(listener, false, true);
    }

    public void DeactivatePrevTargetArrow(ButtonExt.ButtonClickEvent listener)
    {
      this.toggleTargetArrow(listener, false, false);
    }

    public void SetNextTargetArrowActive(bool active)
    {
      this.NextTargetArrow.SetActive(active);
    }

    public void SetPrevTargetArrowActive(bool active)
    {
      this.PrevTargetArrow.SetActive(active);
    }

    public void SetUnitStatus(Unit TargetUnit)
    {
      if (TargetUnit == null)
        return;
      this.mUnitParam.SetUnitStatus(TargetUnit);
    }

    public void SetHpGaugeParam(EUnitSide Side, int CurrentHp, int MaxHp, int YosokuDamageHp = 0, int YosokuHealHp = 0)
    {
      if (Object.op_Equality((Object) this.HpGauge, (Object) null))
        return;
      int PlusValue = YosokuHealHp - YosokuDamageHp;
      if (Side == EUnitSide.Player)
        this.SetGaugeParamInternal(ref this.mHpGaugeParam, TargetPlate.EGaugeType.PLAYER_HP, CurrentHp, MaxHp, PlusValue);
      else
        this.SetGaugeParamInternal(ref this.mHpGaugeParam, TargetPlate.EGaugeType.ENEMY_HP, CurrentHp, MaxHp, PlusValue);
    }

    public void SetMpGaugeParam(EUnitSide Side, int CurrentMp, int MaxMp, int YosokuDamageMp = 0, int YosokuHealMp = 0)
    {
      if (Object.op_Equality((Object) this.MpGauge, (Object) null))
        return;
      int PlusValue = 0;
      if (0 < YosokuDamageMp)
        PlusValue = -YosokuDamageMp;
      else if (0 < YosokuHealMp)
        PlusValue = YosokuHealMp;
      if (Side == EUnitSide.Player)
        this.SetGaugeParamInternal(ref this.mMpGaugeParam, TargetPlate.EGaugeType.PLAYER_MP, CurrentMp, MaxMp, PlusValue);
      else
        this.SetGaugeParamInternal(ref this.mMpGaugeParam, TargetPlate.EGaugeType.PLAYER_MP, CurrentMp, MaxMp, PlusValue);
    }

    private void SetGaugeParamInternal(ref TargetPlate.GaugeParam Gauge, TargetPlate.EGaugeType Type, int CurrentValue, int MaxValue, int PlusValue)
    {
      GameSettings instance = GameSettings.Instance;
      Color32 color32_1 = (Color32) null;
      Color32 color32_2 = (Color32) null;
      Color32 color32_3 = (Color32) null;
      switch (Type)
      {
        case TargetPlate.EGaugeType.PLAYER_HP:
          color32_1 = instance.Gauge_PlayerHP_Base;
          color32_2 = instance.Gauge_PlayerHP_Damage;
          color32_3 = instance.Gauge_PlayerHP_Heal;
          break;
        case TargetPlate.EGaugeType.ENEMY_HP:
          color32_1 = instance.Gauge_EnemyHP_Base;
          color32_2 = instance.Gauge_EnemyHP_Damage;
          color32_3 = instance.Gauge_EnemyHP_Heal;
          break;
      }
      if (0 > PlusValue)
      {
        Color32[] color32Array = new Color32[2]{ color32_1, null };
        color32Array[0].a = (__Null) (int) (byte) ((double) Mathf.Clamp01((float) (CurrentValue + PlusValue) / (float) CurrentValue) * (double) byte.MaxValue);
        color32Array[1] = color32_2;
        color32Array[1].a = (__Null) (int) (byte) ((int) byte.MaxValue - color32Array[0].a);
        Gauge.Colors = color32Array;
        Gauge.Current = CurrentValue;
        Gauge.Max = MaxValue;
      }
      else if (0 < PlusValue)
      {
        Color32[] color32Array = new Color32[2]{ color32_1, null };
        float num = Mathf.Min((float) CurrentValue + (float) PlusValue, (float) MaxValue);
        color32Array[0].a = (__Null) (int) (byte) ((double) Mathf.Clamp01((float) CurrentValue / num) * (double) byte.MaxValue);
        color32Array[1] = color32_3;
        color32Array[1].a = (__Null) (int) (byte) ((int) byte.MaxValue - color32Array[0].a);
        Gauge.Colors = color32Array;
        Gauge.Current = (int) num;
        Gauge.Max = MaxValue;
      }
      else
      {
        Color32[] color32Array = new Color32[1]{ color32_1 };
        Gauge.Colors = color32Array;
        Gauge.Current = CurrentValue;
        Gauge.Max = MaxValue;
      }
    }

    private void UpdateGauge(TargetPlate.GaugeParam param, GradientGauge gauge)
    {
      if (param == null || param.Colors == null || Object.op_Equality((Object) gauge, (Object) null))
        return;
      gauge.UpdateColors(param.Colors);
      gauge.AnimateRangedValue(param.Current, param.Max, 0.0f);
    }

    public void UpdateHpGauge()
    {
      this.UpdateGauge(this.mHpGaugeParam, this.HpGauge);
    }

    public void ResetHpGauge(EUnitSide Side, int CurrentHp, int MaxHp)
    {
      this.SetHpGaugeParam(Side, CurrentHp, MaxHp, 0, 0);
      this.UpdateGauge(this.mHpGaugeParam, this.HpGauge);
    }

    private void ToggleGimmickDescription(bool Active)
    {
      if (this.mSelectedUnit == null || !Object.op_Inequality((Object) this.GimmickDescription, (Object) null))
        return;
      if (this.mSelectedUnit.UnitType == EUnitType.Gem)
        this.GimmickDescription.SetActive(Active);
      else
        this.GimmickDescription.SetActive(false);
    }

    public void OpenGimmickDescription()
    {
      this.ToggleGimmickDescription(true);
    }

    public void CloseGimmickDescription()
    {
      this.ToggleGimmickDescription(false);
    }

    public void Open()
    {
      if (Object.op_Inequality((Object) this.StatusEffects, (Object) null))
        this.StatusEffects.SetStatus(this.mSelectedUnit);
      this.OpenGimmickDescription();
      this.WindowController.Open();
    }

    public void Close()
    {
      this.CloseGimmickDescription();
      this.WindowController.Close();
    }

    public void ForceClose()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "TARGET_STATUS_BUTTON_HIDE");
      this.CloseGimmickDescription();
      this.WindowController.ForceClose();
    }

    private enum EGaugeType
    {
      PLAYER_HP = 1,
      PLAYER_MP = 2,
      ENEMY_HP = 10, // 0x0000000A
      ENEMY_MP = 11, // 0x0000000B
      NEUTRAL_HP = 20, // 0x00000014
      NEUTRAL_MP = 21, // 0x00000015
    }

    private class GaugeParam
    {
      public Color32[] Colors;
      public int Current;
      public int Max;
    }

    private class PlateUnitParam
    {
      public void SetUnitStatus(Unit TargetUnit)
      {
      }
    }
  }
}
