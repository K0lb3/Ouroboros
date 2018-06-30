namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitTobiraConditionsItem : MonoBehaviour
    {
        [SerializeField]
        private ImageArray m_ClearIcon;
        [SerializeField]
        private Text m_ConditionsText;
        [SerializeField]
        private TextColor m_TextColor;

        public UnitTobiraConditionsItem()
        {
            base..ctor();
            return;
        }

        public void SetClearIcon(bool isClear)
        {
            if ((this.m_ClearIcon != null) == null)
            {
                goto Label_0060;
            }
            if (isClear == null)
            {
                goto Label_003E;
            }
            this.m_ClearIcon.ImageIndex = 1;
            this.m_ConditionsText.set_color(this.m_TextColor.m_Clear);
            goto Label_0060;
        Label_003E:
            this.m_ClearIcon.ImageIndex = 0;
            this.m_ConditionsText.set_color(this.m_TextColor.m_NotClear);
        Label_0060:
            return;
        }

        public void SetConditionsText(string text)
        {
            if ((this.m_ConditionsText != null) == null)
            {
                goto Label_001D;
            }
            this.m_ConditionsText.set_text(text);
        Label_001D:
            return;
        }

        public void Setup(ConditionsResult conds)
        {
            if (conds != null)
            {
                goto Label_0022;
            }
            this.SetConditionsText(LocalizedText.Get("sys.TOBIRA_CONDITIONS_NOTHING"));
            this.SetClearIcon(1);
            goto Label_003A;
        Label_0022:
            this.SetConditionsText(conds.text);
            this.SetClearIcon(conds.isClear);
        Label_003A:
            return;
        }

        [Serializable]
        private class TextColor
        {
            public Color m_Clear;
            public Color m_NotClear;

            public TextColor()
            {
                this.m_Clear = Color.get_black();
                this.m_NotClear = Color.get_black();
                base..ctor();
                return;
            }
        }
    }
}

