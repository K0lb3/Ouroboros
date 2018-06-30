namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShopText : MonoBehaviour
    {
        public string Normal;
        public string Tabi;
        public string Kimagure;
        public string Monozuki;
        public string Tour;
        public string Arena;
        public string Multi;
        public string AwakePiece;
        public string Artifact;
        public string Limited;
        private string mTextID;

        public ShopText()
        {
            base..ctor();
            return;
        }

        private void LateUpdate()
        {
            Text text;
            EShopType type;
            if (this.mTextID == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            text = base.GetComponent<Text>();
            if ((text == null) == null)
            {
                goto Label_0020;
            }
            return;
        Label_0020:
            type = GlobalVars.ShopType;
            if (type != null)
            {
                goto Label_003D;
            }
            this.mTextID = this.Normal;
            goto Label_0111;
        Label_003D:
            if (type != 1)
            {
                goto Label_0055;
            }
            this.mTextID = this.Tabi;
            goto Label_0111;
        Label_0055:
            if (type != 2)
            {
                goto Label_006D;
            }
            this.mTextID = this.Kimagure;
            goto Label_0111;
        Label_006D:
            if (type != 3)
            {
                goto Label_0085;
            }
            this.mTextID = this.Monozuki;
            goto Label_0111;
        Label_0085:
            if (type != 4)
            {
                goto Label_009D;
            }
            this.mTextID = this.Tour;
            goto Label_0111;
        Label_009D:
            if (type != 5)
            {
                goto Label_00B5;
            }
            this.mTextID = this.Arena;
            goto Label_0111;
        Label_00B5:
            if (type != 6)
            {
                goto Label_00CD;
            }
            this.mTextID = this.Multi;
            goto Label_0111;
        Label_00CD:
            if (type != 7)
            {
                goto Label_00E5;
            }
            this.mTextID = this.AwakePiece;
            goto Label_0111;
        Label_00E5:
            if (type != 8)
            {
                goto Label_00FD;
            }
            this.mTextID = this.Artifact;
            goto Label_0111;
        Label_00FD:
            if (type != 10)
            {
                goto Label_0111;
            }
            this.mTextID = this.Limited;
        Label_0111:
            if (string.IsNullOrEmpty(this.mTextID) == null)
            {
                goto Label_012D;
            }
            this.mTextID = string.Empty;
            return;
        Label_012D:
            text.set_text(LocalizedText.Get(this.mTextID));
            return;
        }
    }
}

