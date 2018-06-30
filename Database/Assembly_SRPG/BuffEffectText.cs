namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    public class BuffEffectText : MonoBehaviour
    {
        public RichBitmapText Text;

        public BuffEffectText()
        {
            base..ctor();
            return;
        }

        public void SetText(ParamTypes type, bool down)
        {
            string str;
            StringBuilder builder;
            string str2;
            if ((this.Text != null) == null)
            {
                goto Label_00F8;
            }
            str = ((ParamTypes) type).ToString();
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            builder = GameUtility.GetStringBuilder();
            builder.Append("quest.BUFF_");
            builder.Append(str);
            str2 = LocalizedText.Get(builder.ToString());
            builder = GameUtility.GetStringBuilder();
            builder.Append(str2);
            builder.Append(0x20);
            if (down == null)
            {
                goto Label_00AC;
            }
            builder.Append(LocalizedText.Get("quest.EFF_DOWN"));
            this.Text.BottomColor = GameSettings.Instance.Debuff_TextBottomColor;
            this.Text.TopColor = GameSettings.Instance.Debuff_TextTopColor;
            goto Label_00E7;
        Label_00AC:
            builder.Append(LocalizedText.Get("quest.EFF_UP"));
            this.Text.BottomColor = GameSettings.Instance.Buff_TextBottomColor;
            this.Text.TopColor = GameSettings.Instance.Buff_TextTopColor;
        Label_00E7:
            this.Text.text = builder.ToString();
        Label_00F8:
            return;
        }
    }
}

