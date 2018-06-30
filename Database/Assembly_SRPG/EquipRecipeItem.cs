namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class EquipRecipeItem : MonoBehaviour
    {
        public Color DefaultLineColor;
        public Color CommonEquipLineColor;
        public Color DefaultTextColor;
        public Color CommonEquipTextColor;
        public Image[] Lines;
        public Text EquipItemNum;
        public GameObject CommonText;
        public GameObject CommonIcon;

        public EquipRecipeItem()
        {
            base..ctor();
            return;
        }

        public void SetIsCommon(bool is_common)
        {
            if ((this.EquipItemNum == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.EquipItemNum.set_color((is_common == null) ? this.DefaultTextColor : this.CommonEquipTextColor);
            if ((this.CommonText != null) == null)
            {
                goto Label_0051;
            }
            this.CommonText.SetActive(is_common);
        Label_0051:
            if ((this.CommonIcon != null) == null)
            {
                goto Label_006E;
            }
            this.CommonIcon.SetActive(is_common);
        Label_006E:
            return;
        }

        public void SetIsCommonLine(bool is_common)
        {
            int num;
            if (this.Lines != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = 0;
            goto Label_003B;
        Label_0013:
            this.Lines[num].set_color((is_common == null) ? this.DefaultLineColor : this.CommonEquipLineColor);
            num += 1;
        Label_003B:
            if (num < ((int) this.Lines.Length))
            {
                goto Label_0013;
            }
            return;
        }

        private void Start()
        {
        }
    }
}

