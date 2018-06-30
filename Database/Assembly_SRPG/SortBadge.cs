namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class SortBadge : MonoBehaviour
    {
        [FourCC]
        public int ID;
        public Image Icon;
        public Text Value;

        public SortBadge()
        {
            base..ctor();
            return;
        }

        public unsafe void SetValue(int value)
        {
            if ((this.Value != null) == null)
            {
                goto Label_0023;
            }
            this.Value.set_text(&value.ToString());
        Label_0023:
            return;
        }

        public void SetValue(string value)
        {
            if ((this.Value != null) == null)
            {
                goto Label_001D;
            }
            this.Value.set_text(value);
        Label_001D:
            return;
        }
    }
}

