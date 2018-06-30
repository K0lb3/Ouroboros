namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardIconMultiSelect : ConceptCardIcon
    {
        [SerializeField]
        private GameObject mDisable;
        [SerializeField]
        private GameObject mSelect;

        public ConceptCardIconMultiSelect()
        {
            base..ctor();
            return;
        }

        public void RefreshEnableParam(bool enable)
        {
            Button button;
            if (base.ConceptCard != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.mDisable != null) == null)
            {
                goto Label_002C;
            }
            this.mDisable.SetActive(enable == 0);
        Label_002C:
            button = base.get_transform().get_gameObject().GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0050;
            }
            button.set_enabled(enable);
        Label_0050:
            return;
        }

        public void RefreshSelectParam(bool selected)
        {
            if (base.ConceptCard != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.mSelect != null) == null)
            {
                goto Label_0029;
            }
            this.mSelect.SetActive(selected);
        Label_0029:
            return;
        }
    }
}

