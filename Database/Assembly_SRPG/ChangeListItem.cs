namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ChangeListItem : MonoBehaviour
    {
        [FourCC]
        public int ID;
        public Text Label;
        public Text ValOld;
        public Text ValNew;
        public Text Diff;

        public ChangeListItem()
        {
            base..ctor();
            return;
        }
    }
}

