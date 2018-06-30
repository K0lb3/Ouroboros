namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class BadgeValidatorEx : BadgeValidator
    {
        [BitMask]
        public GameManager.BadgeTypes PriorityBadgeType;

        public BadgeValidatorEx()
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
            int num2;
            bool flag;
            if (base.BadgeType != null)
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
            if (manager.CheckBusyBadges(base.BadgeType) == null)
            {
                goto Label_0032;
            }
        Label_0031:
            return;
        Label_0032:
            num2 = this.PriorityBadgeType;
            flag = manager.CheckBadges(base.BadgeType);
            if (num2 == null)
            {
                goto Label_005F;
            }
            if (manager.CheckBadges(this.PriorityBadgeType) == null)
            {
                goto Label_005F;
            }
            flag = 0;
        Label_005F:
            base.get_gameObject().SetActive(flag);
            return;
        }
    }
}

