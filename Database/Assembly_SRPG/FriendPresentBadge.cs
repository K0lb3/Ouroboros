namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class FriendPresentBadge : MonoBehaviour
    {
        public GameObject BadgeObject;
        [BitMask]
        public GameManager.BadgeTypes BadgeType;

        public FriendPresentBadge()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            if ((this.BadgeObject != null) == null)
            {
                goto Label_001D;
            }
            this.BadgeObject.SetActive(0);
        Label_001D:
            return;
        }

        private void Update()
        {
            GameManager manager;
            bool flag;
            if ((this.BadgeObject == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            flag = 0;
            if ((manager != null) == null)
            {
                goto Label_0058;
            }
            flag = manager.CheckBadges(this.BadgeType);
            if (manager.Player == null)
            {
                goto Label_004C;
            }
            flag |= manager.Player.ValidFriendPresent;
        Label_004C:
            this.BadgeObject.SetActive(flag);
        Label_0058:
            return;
        }
    }
}

