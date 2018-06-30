namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class EquipTree : MonoBehaviour
    {
        public Image CursorImage;

        public EquipTree()
        {
            base..ctor();
            return;
        }

        public void SetIsLast(bool is_last)
        {
            this.CursorImage.set_enabled(is_last);
            return;
        }
    }
}

