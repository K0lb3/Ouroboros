// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_Button
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/Button (SRPG)")]
  public class SRPG_Button : Button
  {
    private SRPG_Button.ButtonClickEvent mOnClick;
    [CustomEnum(typeof (SystemSound.ECue), -1)]
    public int ClickSound;
    [BitMask]
    public CriticalSections CSMask;

    public SRPG_Button()
    {
      base.\u002Ector();
    }

    public void AddListener(SRPG_Button.ButtonClickEvent listener)
    {
      this.mOnClick += listener;
    }

    public void RemoveListener(SRPG_Button.ButtonClickEvent listener)
    {
      this.mOnClick -= listener;
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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
      if (this.IsCriticalSectionActive())
        return;
      if (((UIBehaviour) this).IsActive() && eventData.get_button() == null)
        this.mOnClick(this);
      this.PlaySound();
      base.OnPointerClick(eventData);
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
      if (this.IsCriticalSectionActive())
        return;
      if (((UIBehaviour) this).IsActive())
        this.mOnClick(this);
      this.PlaySound();
      base.OnSubmit(eventData);
    }

    public virtual void UpdateButtonState()
    {
      Selectable.SelectionState selectionState = ((Selectable) this).get_currentSelectionState();
      if (((UIBehaviour) this).IsActive() && !((Selectable) this).IsInteractable())
        selectionState = (Selectable.SelectionState) 3;
      ((Selectable) this).DoStateTransition(selectionState, true);
    }

    public delegate void ButtonClickEvent(SRPG_Button go);
  }
}
