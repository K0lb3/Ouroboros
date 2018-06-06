// Decompiled with JetBrains decompiler
// Type: SRPG.UnitGauge
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitGauge : MonoBehaviour
  {
    public GradientGauge MainGauge;
    public string ModeInt;
    public int Mode;
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
      GameUtility.SetGameObjectActive(this.ElementIcon.get_gameObject(), true);
      if (!resetOverlay)
        return;
      this.ResetElementIconOverlay();
    }

    private void DeactivateElementIconInternal()
    {
      GameUtility.SetGameObjectActive(this.ElementIcon.get_gameObject(), false);
      this.ResetElementIconOverlay();
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
    }

    public void ActivateElementIcon(bool resetOverlay)
    {
      this.ActivateElementIconInternal(resetOverlay);
    }

    public void DeactivateElementIcon()
    {
      this.DeactivateElementIconInternal();
    }

    private int CalcElementRate(EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      return (skillElement == EElement.None ? 0 : skillElemValue - (int) this.mCurrentUnit.CurrentStatus.element_resist[skillElement]) + (attackerElement == EElement.None ? 0 : attackerElemValue - (int) this.mCurrentUnit.CurrentStatus.element_resist[attackerElement]);
    }

    public void OnAttack(EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      if (Object.op_Inequality((Object) this.ResistWeakPopup, (Object) null))
      {
        Object.Destroy((Object) this.ResistWeakPopup);
        this.ResistWeakPopup = (GameObject) null;
      }
      int num = this.CalcElementRate(skillElement, skillElemValue, attackerElement, attackerElemValue);
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

    public void Focus(EElement skillElement, int skillElemValue, EElement attackerElement, int attackerElemValue)
    {
      int num = this.CalcElementRate(skillElement, skillElemValue, attackerElement, attackerElemValue);
      if (num > 0)
        this.ToggleElementIconOverlay(true);
      else if (num < 0)
        this.ToggleElementIconOverlay(false);
      else
        this.ResetElementIconOverlay();
    }

    private void Update()
    {
      Animator component = (Animator) ((Component) this).GetComponent<Animator>();
      if (Object.op_Inequality((Object) component, (Object) null) && !string.IsNullOrEmpty(this.ModeInt))
        component.SetInteger(this.ModeInt, this.Mode);
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
    }
  }
}
