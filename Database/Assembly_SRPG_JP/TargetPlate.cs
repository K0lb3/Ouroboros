// Decompiled with JetBrains decompiler
// Type: SRPG.TargetPlate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TargetPlate : MonoBehaviour
  {
    private const float COND_HIT_CHANGE_TIME = 1f;
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
    public GradientGauge HpMaxGauge;
    public StatusEffect StatusEffects;
    public GameObject GoElement;
    public GameObject GoLvIcon;
    public GameObject GoLvText;
    public GameObject GoHpGuage;
    public GameObject GoMpGuage;
    public GameObject GoCTGuage;
    public GameObject GoCondHit;
    public Text TextCondName;
    public ImageArray ImageCondIcon;
    public Text TextCondPer;
    public CanvasGroup[] CgCanTouchs;
    private List<LogSkill.Target.CondHit> mCondHitLists;
    private int mCondHitIndex;
    private float mCondHitPassedTime;
    private Unit mSelectedUnit;
    private WindowController mWindowController;
    private CanvasGroup mTargetCg;
    private TargetPlate.GaugeParam mHpGaugeParam;
    public GameObject GimmickDescription;
    public GameObject FlipButton;

    public TargetPlate()
    {
      base.\u002Ector();
    }

    public Unit SelectedUnit
    {
      get
      {
        return this.mSelectedUnit;
      }
    }

    public WindowController WindowController
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mWindowController, (UnityEngine.Object) null))
          this.mWindowController = (WindowController) ((Component) this).get_gameObject().GetComponent<WindowController>();
        return this.mWindowController;
      }
    }

    private void SetActive(GameObject targetObject, bool isActive)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) targetObject, (UnityEngine.Object) null))
        return;
      targetObject.SetActive(isActive);
    }

    private void SetTextValue(GameObject targetObject, string value)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) targetObject, (UnityEngine.Object) null))
        return;
      Text component = (Text) targetObject.GetComponent<Text>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return;
      component.set_text(value);
    }

    private CanvasGroup TargetCg
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTargetCg, (UnityEngine.Object) null))
          this.mTargetCg = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
        return this.mTargetCg;
      }
    }

    private void Update()
    {
      if (this.CgCanTouchs != null)
      {
        CanvasGroup targetCg = this.TargetCg;
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) targetCg))
        {
          foreach (CanvasGroup cgCanTouch in this.CgCanTouchs)
            cgCanTouch.set_ignoreParentGroups((double) targetCg.get_alpha() >= 1.0);
        }
      }
      if (this.mCondHitLists.Count <= 1)
        return;
      this.mCondHitPassedTime += Time.get_deltaTime();
      if ((double) this.mCondHitPassedTime < 1.0)
        return;
      this.mCondHitPassedTime = 0.0f;
      ++this.mCondHitIndex;
      if (this.mCondHitIndex >= this.mCondHitLists.Count)
        this.mCondHitIndex = 0;
      this.DispCondHit(this.mCondHitIndex);
    }

    private void SetCondHit(List<LogSkill.Target.CondHit> cond_hit_lists = null)
    {
      if (cond_hit_lists == null)
        cond_hit_lists = new List<LogSkill.Target.CondHit>();
      this.mCondHitLists = cond_hit_lists;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoCondHit))
        this.GoCondHit.SetActive(this.mCondHitLists.Count != 0);
      if (this.mCondHitLists.Count == 0)
        return;
      this.mCondHitIndex = 0;
      this.mCondHitPassedTime = 0.0f;
      this.DispCondHit(this.mCondHitIndex);
    }

    private void DispCondHit(int ch_idx)
    {
      if (this.mCondHitLists.Count == 0)
        return;
      LogSkill.Target.CondHit mCondHitList = this.mCondHitLists[ch_idx];
      int index = new List<EUnitCondition>((IEnumerable<EUnitCondition>) Enum.GetValues(typeof (EUnitCondition))).IndexOf(mCondHitList.Cond);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextCondName) && index >= 0 && index < Unit.StrNameUnitConds.Length)
        this.TextCondName.set_text(Unit.StrNameUnitConds[index]);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ImageCondIcon) && index >= 0 && index < this.ImageCondIcon.Images.Length)
        this.ImageCondIcon.ImageIndex = index;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TextCondPer))
        return;
      this.TextCondPer.set_text(mCondHitList.Per.ToString());
    }

    private void reflectBreakObj(Unit unit)
    {
      bool isActive = unit == null || !unit.IsBreakObj;
      this.SetActive(this.GoElement, unit == null || !unit.IsBreakObj || unit.Element != EElement.None);
      this.SetActive(this.GoLvIcon, isActive);
      this.SetActive(this.GoLvText, isActive);
      this.SetActive(this.GoMpGuage, isActive);
      this.SetActive(this.GoCTGuage, isActive);
    }

    public void SetNoAction(Unit unit, List<LogSkill.Target.CondHit> cond_hit_lists = null)
    {
      this.SetActive(this.HealValue, false);
      this.SetActive(this.DamageValue, false);
      this.SetActive(this.CriticalRate, false);
      this.SetActive(this.HitRate, false);
      this.SetActive(this.AttackInfoPlate, false);
      this.reflectBreakObj(unit);
      this.SetCondHit(cond_hit_lists);
      if (cond_hit_lists != null && cond_hit_lists.Count != 0)
        this.SetActive(this.AttackInfoPlate, true);
      this.mSelectedUnit = unit;
      DataSource.Bind<Unit>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void SetAttackAction(Unit unit, int damageValue, int criticalRate, int hitRate, List<LogSkill.Target.CondHit> cond_hit_lists)
    {
      this.SetActive(this.HealValue, false);
      this.SetActive(this.DamageValue, true);
      this.SetActive(this.CriticalRate, true);
      this.SetActive(this.HitRate, true);
      this.SetActive(this.AttackInfoPlate, true);
      this.reflectBreakObj(unit);
      this.SetCondHit(cond_hit_lists);
      this.SetTextValue(this.DamageOutPut, damageValue.ToString());
      this.SetTextValue(this.CriticalRateOutPut, criticalRate.ToString());
      this.SetTextValue(this.HitRateOutPut, hitRate.ToString());
      this.mSelectedUnit = unit;
      DataSource.Bind<Unit>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void SetSkillAction(Unit unit, int damageValue, int criticalRate, int hitRate, List<LogSkill.Target.CondHit> cond_hit_lists)
    {
      this.SetActive(this.HealValue, false);
      this.SetActive(this.DamageValue, true);
      this.SetActive(this.CriticalRate, false);
      this.SetActive(this.HitRate, true);
      this.SetActive(this.AttackInfoPlate, true);
      this.reflectBreakObj(unit);
      this.SetCondHit(cond_hit_lists);
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
      this.reflectBreakObj(unit);
      this.SetCondHit((List<LogSkill.Target.CondHit>) null);
      this.SetTextValue(this.HealOutPut, healValue.ToString());
      this.mSelectedUnit = unit;
      DataSource.Bind<Unit>(((Component) this).get_gameObject(), unit);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public void SetTrick(TrickParam trick_param)
    {
      this.SetActive(this.HealValue, false);
      this.SetActive(this.DamageValue, false);
      this.SetActive(this.CriticalRate, false);
      this.SetActive(this.HitRate, false);
      this.SetActive(this.AttackInfoPlate, false);
      this.mSelectedUnit = (Unit) null;
      DataSource.Bind<TrickParam>(((Component) this).get_gameObject(), trick_param);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void toggleTargetArrow(ButtonExt.ButtonClickEvent listener, bool active, bool isNextArrow)
    {
      GameObject gameObject = !isNextArrow ? this.PrevTargetArrow : this.NextTargetArrow;
      if (listener == null || UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      Button component = (Button) gameObject.GetComponent<Button>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
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

    public void SetHpGaugeParam(EUnitSide Side, int CurrentHp, int MaxHp, int YosokuDamageHp = 0, int YosokuHealHp = 0, bool is_max_damage = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.HpGauge, (UnityEngine.Object) null))
        return;
      int PlusValue = YosokuHealHp - YosokuDamageHp;
      if (is_max_damage && PlusValue != 0)
      {
        int num = MaxHp;
        MaxHp = Math.Max(num + PlusValue, 0);
        this.mHpGaugeParam.MaxValue = num == 0 ? 1f : Mathf.Clamp01((float) MaxHp / (float) num);
        CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
        PlusValue = 0;
      }
      else
        this.mHpGaugeParam.MaxValue = 1f;
      if (Side == EUnitSide.Player)
        this.SetGaugeParamInternal(ref this.mHpGaugeParam, TargetPlate.EGaugeType.PLAYER_HP, CurrentHp, MaxHp, PlusValue);
      else
        this.SetGaugeParamInternal(ref this.mHpGaugeParam, TargetPlate.EGaugeType.ENEMY_HP, CurrentHp, MaxHp, PlusValue);
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
        Color32[] color32Array = new Color32[2]
        {
          color32_1,
          null
        };
        color32Array[0].a = (__Null) (int) (byte) ((double) Mathf.Clamp01((float) (CurrentValue + PlusValue) / (float) CurrentValue) * (double) byte.MaxValue);
        color32Array[1] = color32_2;
        color32Array[1].a = (__Null) (int) (byte) ((int) byte.MaxValue - color32Array[0].a);
        Gauge.Colors = color32Array;
        Gauge.Current = CurrentValue;
        Gauge.Max = MaxValue;
      }
      else if (0 < PlusValue)
      {
        Color32[] color32Array = new Color32[2]
        {
          color32_1,
          null
        };
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
        Color32[] color32Array = new Color32[1]
        {
          color32_1
        };
        Gauge.Colors = color32Array;
        Gauge.Current = CurrentValue;
        Gauge.Max = MaxValue;
      }
    }

    private void UpdateGauge(TargetPlate.GaugeParam param, GradientGauge gauge, GradientGauge max_gauge = null)
    {
      if (param == null || param.Colors == null || UnityEngine.Object.op_Equality((UnityEngine.Object) gauge, (UnityEngine.Object) null))
        return;
      gauge.UpdateColors(param.Colors);
      gauge.AnimateRangedValue(param.Current, param.Max, 0.0f);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) max_gauge))
        return;
      max_gauge.UpdateValue(param.MaxValue);
      gauge.UpdateValue(Mathf.Clamp01(gauge.Value * max_gauge.Value));
    }

    public void UpdateHpGauge()
    {
      this.UpdateGauge(this.mHpGaugeParam, this.HpGauge, this.HpMaxGauge);
    }

    public void ResetHpGauge(EUnitSide Side, int CurrentHp, int MaxHp)
    {
      this.SetHpGaugeParam(Side, CurrentHp, MaxHp, 0, 0, false);
      this.UpdateGauge(this.mHpGaugeParam, this.HpGauge, this.HpMaxGauge);
    }

    private void ToggleGimmickDescription(bool Active)
    {
      if (this.mSelectedUnit == null || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GimmickDescription, (UnityEngine.Object) null))
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
    }

    public void Open()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.StatusEffects, (UnityEngine.Object) null) && this.mSelectedUnit != null)
        this.StatusEffects.SetStatus(this.mSelectedUnit);
      this.OpenGimmickDescription();
      if (this.WindowController.IsOpening())
        return;
      if (this.WindowController.IsClosing())
        this.WindowController.ForceOpen();
      else
        this.WindowController.Open();
    }

    public void Close()
    {
      this.CloseGimmickDescription();
      if (this.WindowController.IsClosing())
        return;
      if (this.WindowController.IsOpening())
        this.WindowController.ForceClose();
      else
        this.WindowController.Close();
    }

    public void ForceClose(bool isHide = true)
    {
      if (isHide)
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "TARGET_STATUS_BUTTON_HIDE");
      this.CloseGimmickDescription();
      this.WindowController.ForceClose();
    }

    public void HideButton()
    {
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "TARGET_STATUS_BUTTON_HIDE");
    }

    public void SetEnableFlipButton(bool is_enable)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.FlipButton, (UnityEngine.Object) null))
        return;
      this.FlipButton.SetActive(is_enable);
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
      public float MaxValue = 1f;
      public Color32[] Colors;
      public int Current;
      public int Max;
    }
  }
}
