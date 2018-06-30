namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "ソート順の変更", 0, 0), Pin(1, "Save Setting", 0, 1), Pin(2, "ソート種別の変更", 0, 2), Pin(100, "Close", 1, 100)]
    public class GallerySortWindow : MonoBehaviour, IFlowInterface
    {
        private const int SORT_ORDER_CHANGE = 0;
        private const int SAVE_SETTING = 1;
        private const int SORT_TYPE_CHANGE = 2;
        private const int OUTPUT_CLOSE = 100;
        [SerializeField]
        private Toggle RarityButton;
        [SerializeField]
        private Toggle NameButton;
        [SerializeField]
        private Toggle AscButton;
        [SerializeField]
        private Toggle DescButton;
        private GalleryItemListWindow.Settings mSettings;
        private bool mIsRarityAscending;
        private bool mIsNameAscending;
        private GalleryItemListWindow.SortType mCurrentSortType;

        public GallerySortWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            SerializeValueList list;
            Toggle toggle;
            SerializeValueList list2;
            Toggle toggle2;
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_00A7;

                case 1:
                    goto Label_0131;

                case 2:
                    goto Label_001B;
            }
            goto Label_016D;
        Label_001B:
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list == null)
            {
                goto Label_00A6;
            }
            if ((list.GetUIToggle("toggle") == this.RarityButton) == null)
            {
                goto Label_007A;
            }
            this.mCurrentSortType = 0;
            this.AscButton.set_isOn(this.mIsRarityAscending);
            this.DescButton.set_isOn(this.mIsRarityAscending == 0);
            goto Label_00A6;
        Label_007A:
            this.mCurrentSortType = 1;
            this.AscButton.set_isOn(this.mIsNameAscending);
            this.DescButton.set_isOn(this.mIsNameAscending == 0);
        Label_00A6:
            return;
        Label_00A7:
            list2 = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list2 == null)
            {
                goto Label_0130;
            }
            toggle2 = list2.GetUIToggle("toggle");
            if ((toggle2 == this.AscButton) == null)
            {
                goto Label_0102;
            }
            if (this.mCurrentSortType != null)
            {
                goto Label_00F1;
            }
            this.mIsRarityAscending = toggle2.get_isOn();
            goto Label_00FD;
        Label_00F1:
            this.mIsNameAscending = toggle2.get_isOn();
        Label_00FD:
            goto Label_0130;
        Label_0102:
            if (this.mCurrentSortType != null)
            {
                goto Label_0121;
            }
            this.mIsRarityAscending = toggle2.get_isOn() == 0;
            goto Label_0130;
        Label_0121:
            this.mIsNameAscending = toggle2.get_isOn() == 0;
        Label_0130:
            return;
        Label_0131:
            this.mSettings.sortType = this.mCurrentSortType;
            this.mSettings.isRarityAscending = this.mIsRarityAscending;
            this.mSettings.isNameAscending = this.mIsNameAscending;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        Label_016D:
            return;
        }

        private void Awake()
        {
            SerializeValueList list;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mSettings = list.GetObject("settings") as GalleryItemListWindow.Settings;
            if (this.mSettings != null)
            {
                goto Label_0034;
            }
            return;
        Label_0034:
            this.AscButton.set_isOn(this.mSettings.isRarityAscending);
            this.DescButton.set_isOn(this.mSettings.isRarityAscending == 0);
            this.mCurrentSortType = this.mSettings.sortType;
            this.mIsRarityAscending = this.mSettings.isRarityAscending;
            this.mIsNameAscending = this.mSettings.isNameAscending;
            if (this.mCurrentSortType != null)
            {
                goto Label_00E3;
            }
            this.RarityButton.set_isOn(1);
            this.NameButton.set_isOn(0);
            this.AscButton.set_isOn(this.mIsRarityAscending);
            this.DescButton.set_isOn(this.mIsRarityAscending == 0);
            goto Label_0120;
        Label_00E3:
            this.RarityButton.set_isOn(0);
            this.NameButton.set_isOn(1);
            this.AscButton.set_isOn(this.mIsNameAscending);
            this.DescButton.set_isOn(this.mIsNameAscending == 0);
        Label_0120:
            return;
        }
    }
}

