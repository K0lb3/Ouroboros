namespace SRPG
{
    using System;
    using UnityEngine.UI;

    public class NoBreakSpaceText : Text
    {
        public NoBreakSpaceText()
        {
            base..ctor();
            return;
        }

        public unsafe void Refresh()
        {
            int num;
            char ch;
            num = Convert.ToInt32("00A0", 0x10);
            base.m_Text = base.m_Text.Replace(" ", &Convert.ToChar(num).ToString());
            return;
        }

        public override string text
        {
            get
            {
                return base.get_text();
            }
            set
            {
                base.set_text(value);
                this.Refresh();
                return;
            }
        }
    }
}

