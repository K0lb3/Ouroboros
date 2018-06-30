namespace SRPG
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class GenericSlotFlags : MonoBehaviour
    {
        [BitMask]
        public VisibleFlags Flags;

        public GenericSlotFlags()
        {
            base..ctor();
            return;
        }

        [Flags]
        public enum VisibleFlags
        {
            Empty = 1,
            NonEmpty = 2,
            Locked = 4
        }
    }
}

