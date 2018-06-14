// Decompiled with JetBrains decompiler
// Type: AnimatedToggle
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (Animator))]
public class AnimatedToggle : Toggle
{
  public string BoolName;
  public string DisabledBool;
  private Animator mAnimator;
  public AnimatedToggle.ClickEvent OnClick;
  [CustomEnum(typeof (SystemSound.ECue), -1)]
  public int ClickSound;
  [BitMask]
  public CriticalSections CSMask;

  public AnimatedToggle()
  {
    base.\u002Ector();
  }

  protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
  {
  }

  protected virtual void Awake()
  {
    this.mAnimator = (Animator) ((Component) this).GetComponent<Animator>();
  }

  protected virtual void OnEnable()
  {
    base.OnEnable();
    this.Update();
  }

  private void Update()
  {
    this.mAnimator.SetBool(this.BoolName, this.get_isOn());
    if (string.IsNullOrEmpty(this.DisabledBool))
      return;
    this.mAnimator.SetBool(this.DisabledBool, !((Selectable) this).get_interactable());
  }

  public virtual void OnPointerClick(PointerEventData eventData)
  {
    if (this.IsCriticalSectionActive())
      return;
    base.OnPointerClick(eventData);
    if (this.OnClick != null)
      this.OnClick(this);
    this.PlaySound();
  }

  private void PlaySound()
  {
    if (!((Selectable) this).IsInteractable() || this.ClickSound < 0)
      return;
    SystemSound.Play((SystemSound.ECue) this.ClickSound);
  }

  private bool IsCriticalSectionActive()
  {
    return (this.CSMask & CriticalSection.GetActive()) != (CriticalSections) 0;
  }

  public delegate void ClickEvent(AnimatedToggle toggle);
}
