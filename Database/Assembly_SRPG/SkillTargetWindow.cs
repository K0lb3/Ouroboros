namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SkillTargetWindow : MonoBehaviour
    {
        public TargetSelectEvent OnTargetSelect;
        public CancelEvent OnCancel;
        private WindowController mWC;

        public SkillTargetWindow()
        {
            base..ctor();
            return;
        }

        public void Cancel()
        {
            if (this.OnCancel == null)
            {
                goto Label_001C;
            }
            this.OnCancel();
            this.Hide();
        Label_001C:
            return;
        }

        public void ForceHide()
        {
            if ((this.mWC != null) == null)
            {
                goto Label_001C;
            }
            this.mWC.ForceClose();
        Label_001C:
            return;
        }

        public void GridSelected()
        {
            if (this.OnTargetSelect == null)
            {
                goto Label_001D;
            }
            this.OnTargetSelect(1);
            this.Hide();
        Label_001D:
            return;
        }

        public void Hide()
        {
            if ((this.mWC != null) == null)
            {
                goto Label_001C;
            }
            this.mWC.Close();
        Label_001C:
            return;
        }

        public void Show()
        {
            if ((this.mWC != null) == null)
            {
                goto Label_001C;
            }
            this.mWC.Open();
        Label_001C:
            return;
        }

        private void Start()
        {
            this.mWC = base.GetComponent<WindowController>();
            return;
        }

        public void UnitSelected()
        {
            if (this.OnTargetSelect == null)
            {
                goto Label_001D;
            }
            this.OnTargetSelect(0);
            this.Hide();
        Label_001D:
            return;
        }

        public delegate void CancelEvent();

        public delegate void TargetSelectEvent(bool grid);
    }
}

