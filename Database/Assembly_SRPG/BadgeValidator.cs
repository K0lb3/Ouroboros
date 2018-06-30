namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class BadgeValidator : MonoBehaviour
    {
        [BitMask]
        public GameManager.BadgeTypes BadgeType;

        public BadgeValidator()
        {
            base..ctor();
            return;
        }

        private void Update()
        {
            this.UpdateBadge();
            return;
        }

        private void UpdateBadge()
        {
            int num;
            GameManager manager;
            if (this.BadgeType != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            if ((manager == null) != null)
            {
                goto Label_0031;
            }
            if (manager.CheckBusyBadges(this.BadgeType) == null)
            {
                goto Label_0032;
            }
        Label_0031:
            return;
        Label_0032:
            base.get_gameObject().SetActive(MonoSingleton<GameManager>.GetInstanceDirect().CheckBadges(this.BadgeType));
            return;
        }
    }
}

