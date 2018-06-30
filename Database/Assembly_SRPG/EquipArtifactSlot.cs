namespace SRPG
{
    using System;

    public class EquipArtifactSlot : GenericSlot
    {
        public SRPG_Button Lock;

        public EquipArtifactSlot()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((base.SelectButton != null) == null)
            {
                goto Label_0028;
            }
            base.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
        Label_0028:
            if ((this.Lock != null) == null)
            {
                goto Label_0050;
            }
            this.Lock.AddListener(new SRPG_Button.ButtonClickEvent(this.OnLockClick));
        Label_0050:
            return;
        }

        private void OnButtonClick(SRPG_Button button)
        {
            if (base.OnSelect == null)
            {
                goto Label_0028;
            }
            if (button.get_interactable() == null)
            {
                goto Label_0028;
            }
            base.OnSelect(this, button.get_interactable());
        Label_0028:
            return;
        }

        private void OnLockClick(SRPG_Button button)
        {
            if (base.OnSelect == null)
            {
                goto Label_0018;
            }
            base.OnSelect(this, 0);
        Label_0018:
            return;
        }
    }
}

