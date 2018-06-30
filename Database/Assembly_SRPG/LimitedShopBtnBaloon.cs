namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class LimitedShopBtnBaloon : MonoBehaviour
    {
        [SerializeField]
        private Image BaloonChar;
        [SerializeField]
        private Image BaloonTextLeft;
        [SerializeField]
        private Image BaloonTextRight;
        [SerializeField]
        private string ReverseObjectID;
        [HideInInspector]
        public Sprite CurrentTextLeftSprite;
        [HideInInspector]
        public Sprite CurrentTextRightSprite;
        [HideInInspector]
        public Sprite CurrentCharSprite;
        private GameObject mBaloonChar;
        private GameObject mBaloonTextLeft;
        private GameObject mBaloonTextRight;

        public LimitedShopBtnBaloon()
        {
            base..ctor();
            return;
        }

        private void RefreshBaloonImage()
        {
            if ((this.BaloonChar != null) == null)
            {
                goto Label_0054;
            }
            this.BaloonChar.set_sprite(((this.CurrentCharSprite != null) == null) ? this.BaloonChar.get_sprite() : this.CurrentCharSprite);
            this.BaloonChar.get_gameObject().SetActive(1);
        Label_0054:
            if ((this.BaloonTextLeft != null) == null)
            {
                goto Label_00A8;
            }
            this.BaloonTextLeft.set_sprite(((this.CurrentTextLeftSprite != null) == null) ? this.BaloonTextLeft.get_sprite() : this.CurrentTextLeftSprite);
            this.BaloonTextLeft.get_gameObject().SetActive(1);
        Label_00A8:
            if ((this.BaloonTextRight != null) == null)
            {
                goto Label_00FC;
            }
            this.BaloonTextRight.set_sprite(((this.CurrentTextRightSprite != null) == null) ? this.BaloonTextRight.get_sprite() : this.CurrentTextRightSprite);
            this.BaloonTextRight.get_gameObject().SetActive(1);
        Label_00FC:
            return;
        }

        private void Start()
        {
            if ((this.BaloonChar != null) == null)
            {
                goto Label_0022;
            }
            this.BaloonChar.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.BaloonTextLeft != null) == null)
            {
                goto Label_0044;
            }
            this.BaloonTextLeft.get_gameObject().SetActive(0);
        Label_0044:
            if ((this.BaloonTextRight != null) == null)
            {
                goto Label_0066;
            }
            this.BaloonTextRight.get_gameObject().SetActive(0);
        Label_0066:
            this.RefreshBaloonImage();
            this.UpdatePosition();
            return;
        }

        private void UpdatePosition()
        {
            GameObject obj2;
            UIProjector projector;
            if (string.IsNullOrEmpty(this.ReverseObjectID) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            obj2 = GameObjectID.FindGameObject(this.ReverseObjectID);
            if ((obj2 != null) == null)
            {
                goto Label_0042;
            }
            projector = obj2.GetComponent<UIProjector>();
            if ((projector != null) == null)
            {
                goto Label_0042;
            }
            projector.ReStart();
        Label_0042:
            return;
        }
    }
}

