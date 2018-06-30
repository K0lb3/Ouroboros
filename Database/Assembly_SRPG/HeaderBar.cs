namespace SRPG
{
    using System;
    using UnityEngine;

    public class HeaderBar : PropertyAttribute
    {
        public string Text;
        public Color BGColor;
        public Color FGColor;

        public HeaderBar(string text)
        {
            base..ctor();
            this.Text = text;
            this.BGColor = new Color(0f, 0.2f, 0.5f);
            this.FGColor = Color.get_white();
            return;
        }

        public HeaderBar(string text, Color bg)
        {
            base..ctor();
            this.Text = text;
            this.BGColor = bg;
            this.FGColor = Color.get_white();
            return;
        }

        public HeaderBar(string text, Color bg, Color fg)
        {
            base..ctor();
            this.Text = text;
            this.BGColor = bg;
            this.FGColor = fg;
            return;
        }
    }
}

