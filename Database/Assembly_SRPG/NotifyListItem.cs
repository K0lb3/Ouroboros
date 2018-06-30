namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class NotifyListItem : MonoBehaviour
    {
        public Text Message;
        [NonSerialized]
        public float Lifetime;
        [NonSerialized]
        public float Height;

        public NotifyListItem()
        {
            base..ctor();
            return;
        }
    }
}

