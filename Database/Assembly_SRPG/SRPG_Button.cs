namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [AddComponentMenu("UI/Button (SRPG)")]
    public class SRPG_Button : Button
    {
        private ButtonClickEvent mOnClick;
        [CustomEnum(typeof(SystemSound.ECue), -1)]
        public int ClickSound;
        [BitMask]
        public CriticalSections CSMask;
        [CompilerGenerated]
        private static ButtonClickEvent <>f__am$cache3;

        public SRPG_Button()
        {
            if (<>f__am$cache3 != null)
            {
                goto Label_0019;
            }
            <>f__am$cache3 = new ButtonClickEvent(SRPG_Button.<mOnClick>m__3F6);
        Label_0019:
            this.mOnClick = <>f__am$cache3;
            this.ClickSound = -1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <mOnClick>m__3F6(SRPG_Button b)
        {
        }

        public void AddListener(ButtonClickEvent listener)
        {
            this.mOnClick = (ButtonClickEvent) Delegate.Combine(this.mOnClick, listener);
            return;
        }

        private bool IsCriticalSectionActive()
        {
            CriticalSections sections;
            sections = CriticalSection.GetActive();
            if ((this.CSMask & sections) == null)
            {
                goto Label_0015;
            }
            return 1;
        Label_0015:
            return 0;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (this.IsCriticalSectionActive() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (SRPG_InputField.IsFocus == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (this.IsActive() == null)
            {
                goto Label_0039;
            }
            if (eventData.get_button() != null)
            {
                goto Label_0039;
            }
            this.mOnClick(this);
        Label_0039:
            this.PlaySound();
            base.OnPointerClick(eventData);
            return;
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            if (this.IsCriticalSectionActive() == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (SRPG_InputField.IsFocus == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (this.IsActive() == null)
            {
                goto Label_002E;
            }
            this.mOnClick(this);
        Label_002E:
            this.PlaySound();
            base.OnSubmit(eventData);
            return;
        }

        private void PlaySound()
        {
            if (this.IsInteractable() == null)
            {
                goto Label_0022;
            }
            if (this.ClickSound < 0)
            {
                goto Label_0022;
            }
            SystemSound.Play(this.ClickSound);
        Label_0022:
            return;
        }

        public void RemoveListener(ButtonClickEvent listener)
        {
            this.mOnClick = (ButtonClickEvent) Delegate.Remove(this.mOnClick, listener);
            return;
        }

        public virtual void UpdateButtonState()
        {
            Selectable.SelectionState state;
            state = base.get_currentSelectionState();
            if (this.IsActive() == null)
            {
                goto Label_001F;
            }
            if (this.IsInteractable() != null)
            {
                goto Label_001F;
            }
            state = 3;
        Label_001F:
            this.DoStateTransition(state, 1);
            return;
        }

        public delegate void ButtonClickEvent(SRPG_Button go);
    }
}

