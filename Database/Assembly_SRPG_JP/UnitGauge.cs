// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitGauge : MonoBehaviour
  {
    public GradientGauge MainGauge;
    public UnitBuffDisplay BuffDisplay;
    public string ModeInt;
    public GradientGauge MaxGauge;
    private int mMode;
    public GameObject WeakTemplate;
    public GameObject ResistTemplate;
    public GameObject ElementIcon;
    public Image ElementIconImage;
    public GameObject ElementIconWeakOverlay;
    public GameObject ElementIconResistOverlay;
    private Unit mCurrentUnit;
    private GameObject ResistWeakPopup;

    public UnitGauge()
    {
      base.\u002Ector();
    }

    public int Mode
    {
      get
      {
        return this.mMode;
      }
      set
      {
        this.mMode = value;
        if (this.mMode == 0)
          this.activateHpGauge(false);
        else
          this.activateHpGauge(true);
      }
    }

    private void SetElementIconImage(EElement element)
    {
      if (!Object.op_Inequality((Object) this.ElementIcon, (Object) null) || !Object.op_Inequality((Object) this.ElementIconImage, (Object) null))
        return;
      this.ElementIconImage.set_sprite(GameSettings.Instance.Elements_IconSmall[(int) element]);
      this.ResetElementIconOverlay();
    }

    private void ResetElementIconOverlay()
    {
      GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, false);
      GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, false);
    }

    private void ToggleElementIconOverlay(bool weakActive)
    {
      GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, weakActive);
      GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, !weakActive);
    }

    private void ActivateElementIconInternal(bool resetOverlay)
    {
      bool active = true;
      if (this.mCurrentUnit != null && this.mCurrentUnit.IsBreakObj && this.mCurrentUnit.Element == EElement.None)
        active = false;
      GameUtility.SetGameObjectActive(this.ElementIcon.get_gameObject(), active);
      if (!resetOverlay)
        return;
      this.ResetElementIconOverlay();
    }

    private void DeactivateElementIconInternal()
    {
      GameUtility.SetGameObjectActive(this.ElementIcon.get_gameObject(), false);
      this.ResetElementIconOverlay();
    }

    private void activateHpGauge(bool is_active)
    {
      if (this.mCurrentUnit == null || !this.mCurrentUnit.IsBreakObj)
        return;
      if (Object.op_Implicit((Object) this.MainGauge))
        ((Component) this.MainGauge).get_gameObject().SetActive(is_active);
      if (Object.op_Implicit((Object) this.MaxGauge))
        ((Component) this.MaxGauge).get_gameObject().SetActive(is_active);
      Image component = (Image) ((Component) this).GetComponent<Image>();
      if (Object.op_Implicit((Object) component))
        ((Behaviour) component).set_enabled(is_active);
      if (!Object.op_Implicit((Object) this.ElementIcon) || this.mCurrentUnit.Element == EElement.None)
        return;
      Image[] componentsInChildren = (Image[]) this.ElementIcon.GetComponentsInChildren<Image>();
      if (componentsInChildren == null)
        return;
      foreach (Behaviour behaviour in componentsInChildren)
        behaviour.set_enabled(is_active);
    }

    protected void Start()
    {
      GameUtility.SetGameObjectActive(this.WeakTemplate, false);
      GameUtility.SetGameObjectActive(this.ResistTemplate, false);
      GameUtility.SetGameObjectActive(this.ElementIcon, false);
      GameUtility.SetGameObjectActive(this.ElementIconWeakOverlay, false);
      GameUtility.SetGameObjectActive(this.ElementIconResistOverlay, false);
    }

    public void SetOwner(Unit owner)
    {
      this.mCurrentUnit = owner;
      this.SetElementIconImage(this.mCurrentUnit.Element);
      this.activateHpGauge(false);
    }

    public Unit GetOwner()
    {
      return this.mCurrentUnit;
    }

    public void ActivateElementIcon(bool resetOverlay)
    {
      this.ActivateElementIconInternal(resetOverlay);
    }

    public void DeactivateElementIcon()
    {
      this.DeactivateElementIconInternal();
    }

    private int CalcElementRate(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      if (skill != null && skill.IsIgnoreElement())
      {
        skillElement = EElement.None;
        attackerElement = EElement.None;
      }
      EElement weakElement = UnitParam.GetWeakElement(this.mCurrentUnit.Element);
      EElement resistElement = UnitParam.GetResistElement(this.mCurrentUnit.Element);
      int num = 0;
      if (attackerElement != EElement.None)
        num += weakElement != attackerElement ? (resistElement != attackerElement ? 0 : -1) : 1;
      if (skillElement != EElement.None)
        num += weakElement != skillElement ? (resistElement != skillElement ? 0 : -1) : 1;
      return num;
    }

    public void OnAttack(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      if (Object.op_Inequality((Object) this.ResistWeakPopup, (Object) null))
      {
        Object.Destroy((Object) this.ResistWeakPopup);
        this.ResistWeakPopup = (GameObject) null;
      }
      if (this.mCurrentUnit != null && this.mCurrentUnit.IsBreakObj)
        return;
      int num = this.CalcElementRate(skill, skillElement, skillElemValue, attackerElement, attackerElemValue);
      if (num > 0)
      {
        if (!Object.op_Inequality((Object) this.WeakTemplate, (Object) null))
          return;
        this.ResistWeakPopup = (GameObject) Object.Instantiate<GameObject>((M0) this.WeakTemplate);
        this.ResistWeakPopup.get_transform().SetParent(((Component) this).get_transform(), false);
        this.ResistWeakPopup.SetActive(true);
      }
      else
      {
        if (num >= 0 || !Object.op_Inequality((Object) this.ResistTemplate, (Object) null))
          return;
        this.ResistWeakPopup = (GameObject) Object.Instantiate<GameObject>((M0) this.ResistTemplate);
        this.ResistWeakPopup.get_transform().SetParent(((Component) this).get_transform(), false);
        this.ResistWeakPopup.SetActive(true);
      }
    }

    public void Focus(SkillData skill, EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      if (this.mCurrentUnit == null || !this.mCurrentUnit.IsBreakObj)
      {
        int num = this.CalcElementRate(skill, skillElement, skillElemValue, attackerElement, attackerElemValue);
        if (num > 0)
        {
          this.ToggleElementIconOverlay(true);
          return;
        }
        if (num < 0)
        {
          this.ToggleElementIconOverlay(false);
          return;
        }
      }
      this.ResetElementIconOverlay();
    }

    private void Update()
    {
      Animator component = (Animator) ((Component) this).GetComponent<Animator>();
      if (Object.op_Inequality((Object) component, (Object) null) && !string.IsNullOrEmpty(this.ModeInt))
        component.SetInteger(this.ModeInt, this.mMode);
      this.ActivateElementIcon(false);
      if (this.MainGauge.IsAnimating || !Object.op_Inequality((Object) this.ResistWeakPopup, (Object) null))
        return;
      Object.Destroy((Object) this.ResistWeakPopup);
      this.ResistWeakPopup = (GameObject) null;
    }

    public enum GaugeMode
    {
      Normal,
      Attack,
      Target,
      Change,
    }
  }
}
