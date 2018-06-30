namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class LText : Text
    {
        private string mCurrentText;

        public LText()
        {
            base..ctor();
            return;
        }

        private void LateUpdate()
        {
            if (Application.get_isPlaying() != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            if (string.IsNullOrEmpty(this.mCurrentText) == null)
            {
                goto Label_0031;
            }
            if (string.IsNullOrEmpty(this.get_text()) == null)
            {
                goto Label_0058;
            }
            return;
            goto Label_0058;
        Label_0031:
            if (string.IsNullOrEmpty(this.get_text()) != null)
            {
                goto Label_0058;
            }
            if (this.mCurrentText.Equals(this.get_text()) == null)
            {
                goto Label_0058;
            }
            return;
        Label_0058:
            this.set_text(LocalizedText.Get(this.get_text()));
            this.mCurrentText = this.get_text();
            return;
        }
    }
}

