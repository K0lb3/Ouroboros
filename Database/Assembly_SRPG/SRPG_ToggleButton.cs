namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [AddComponentMenu("UI/Toggle Button (SRPG)")]
    public class SRPG_ToggleButton : SRPG_Button
    {
        private bool mIsOn;
        public bool AutoToggle;

        public SRPG_ToggleButton()
        {
            base..ctor();
            return;
        }

        protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
        {
            if (((state != null) && (state != 1)) && (state != 2))
            {
                goto Label_0028;
            }
            state = (this.mIsOn == null) ? 0 : 2;
        Label_0028:
            base.DoStateTransition(state, instant);
            return;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (this.IsInteractable() == null)
            {
                goto Label_0025;
            }
            if (this.AutoToggle == null)
            {
                goto Label_0025;
            }
            this.IsOn = this.IsOn == 0;
        Label_0025:
            base.OnPointerClick(eventData);
            return;
        }

        public bool IsOn
        {
            get
            {
                return this.mIsOn;
            }
            set
            {
                if (this.mIsOn == value)
                {
                    goto Label_002C;
                }
                this.mIsOn = value;
                this.DoStateTransition((this.mIsOn == null) ? 0 : 2, 0);
            Label_002C:
                return;
            }
        }
    }
}

