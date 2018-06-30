namespace SRPG
{
    using System;
    using System.Text;
    using UnityEngine;

    public class CondEffectText : MonoBehaviour
    {
        public RichBitmapText Text;

        public CondEffectText()
        {
            base..ctor();
            return;
        }

        public void SetText(EUnitCondition condition)
        {
            string str;
            StringBuilder builder;
            string str2;
            if ((this.Text != null) == null)
            {
                goto Label_00A1;
            }
            str = ((EUnitCondition) condition).ToString();
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0029;
            }
            return;
        Label_0029:
            builder = GameUtility.GetStringBuilder();
            builder.Append("quest.COND_");
            builder.Append(str);
            str2 = LocalizedText.Get(builder.ToString());
            builder = GameUtility.GetStringBuilder();
            builder.Append(str2);
            builder.Append(0x20);
            this.Text.BottomColor = GameSettings.Instance.FailCondition_TextBottomColor;
            this.Text.TopColor = GameSettings.Instance.FailCondition_TextTopColor;
            this.Text.text = builder.ToString();
        Label_00A1:
            return;
        }
    }
}

