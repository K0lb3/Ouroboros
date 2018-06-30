namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ElementDropdown : Pulldown
    {
        [SerializeField]
        private Image ElementIcon;

        public ElementDropdown()
        {
            base..ctor();
            return;
        }

        public PulldownItem AddItem(string label, Sprite sprite, int value)
        {
            PulldownItem item;
            ElementDropdownItem item2;
            item = this.AddItem(label, value);
            item2 = item as ElementDropdownItem;
            if ((item2 != null) == null)
            {
                goto Label_0028;
            }
            item2.IconImage.set_sprite(sprite);
        Label_0028:
            return item;
        }

        protected override PulldownItem SetupPulldownItem(GameObject itemObject)
        {
            ElementDropdownItem item;
            item = itemObject.GetComponent<ElementDropdownItem>();
            if ((item == null) == null)
            {
                goto Label_001A;
            }
            item = itemObject.AddComponent<ElementDropdownItem>();
        Label_001A:
            return item;
        }

        protected override void UpdateSelection()
        {
            ElementDropdownItem item;
            if ((this.ElementIcon != null) == null)
            {
                goto Label_003F;
            }
            item = base.GetCurrentSelection() as ElementDropdownItem;
            if ((item != null) == null)
            {
                goto Label_003F;
            }
            this.ElementIcon.set_sprite(item.IconImage.get_sprite());
        Label_003F:
            return;
        }
    }
}

