namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "Save", 0, 0), Pin(11, "キャンセル完了", 1, 11), Pin(3, "キャンセル", 0, 3), Pin(10, "Save完了", 1, 10), Pin(2, "全選択解除", 0, 2), Pin(1, "全選択", 0, 1)]
    public class ConceptCardListFilterWindow : MonoBehaviour, IFlowInterface
    {
        private const string SAVE_KEY = "CARD_FILTERT";
        private const string SAVE_CARDTYPE_KEY = "CARD_CARDTYPE_FILTERT";
        public const int PIN_SAVE = 0;
        public const int PIN_ALL_SELECT = 1;
        public const int PIN_ALL_CLEAR_SELECT = 2;
        public const int PIN_CANCEL = 3;
        public const int PIN_SAVE_END = 10;
        public const int PIN_CANCEL_END = 11;
        public const Type MASK_RARITY = 0x1f;
        public Item[] FilterToggles;
        public Type CurrentType;
        public Type FirstType;

        public ConceptCardListFilterWindow()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            ConceptCardManager manager;
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_001D;

                case 1:
                    goto Label_0054;

                case 2:
                    goto Label_0060;

                case 3:
                    goto Label_006C;
            }
            goto Label_007F;
        Label_001D:
            manager = ConceptCardManager.Instance;
            if ((manager != null) == null)
            {
                goto Label_0041;
            }
            if (manager.IsEnhanceListActive == null)
            {
                goto Label_0041;
            }
            manager.ToggleSameSelectCard = 0;
        Label_0041:
            this.Save();
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            goto Label_007F;
        Label_0054:
            this.OnSelectAll(1);
            goto Label_007F;
        Label_0060:
            this.OnSelectAll(0);
            goto Label_007F;
        Label_006C:
            this.ResetType();
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
        Label_007F:
            return;
        }

        public void Load()
        {
            this.CurrentType = LoadData();
            this.FirstType = this.CurrentType;
            this.OnSelectType();
            return;
        }

        public static unsafe Type LoadData()
        {
            string str;
            int num;
            if (PlayerPrefsUtility.HasKey("CARD_FILTERT") != null)
            {
                goto Label_0012;
            }
            return 0x1f;
        Label_0012:
            str = PlayerPrefsUtility.GetString("CARD_FILTERT", string.Empty);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0030;
            }
            return 0x1f;
        Label_0030:
            num = 0;
            if (int.TryParse(str, &num) != null)
            {
                goto Label_0042;
            }
            return 0x1f;
        Label_0042:
            return num;
        }

        public void OnSelect(bool is_on)
        {
            int num;
            Item item;
            this.CurrentType = 0;
            num = 0;
            goto Label_003E;
        Label_000E:
            item = this.FilterToggles[num];
            if (item.toggle.get_isOn() == null)
            {
                goto Label_003A;
            }
            this.CurrentType |= item.type;
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) this.FilterToggles.Length))
            {
                goto Label_000E;
            }
            this.SetType();
            return;
        }

        public void OnSelectAll(bool is_on)
        {
            int num;
            Item item;
            this.CurrentType = 0;
            num = 0;
            goto Label_0040;
        Label_000E:
            item = this.FilterToggles[num];
            GameUtility.SetToggle(item.toggle, is_on);
            if (is_on == null)
            {
                goto Label_003C;
            }
            this.CurrentType |= item.type;
        Label_003C:
            num += 1;
        Label_0040:
            if (num < ((int) this.FilterToggles.Length))
            {
                goto Label_000E;
            }
            this.SetType();
            return;
        }

        public void OnSelectType()
        {
            int num;
            Item item;
            num = 0;
            goto Label_002F;
        Label_0007:
            item = this.FilterToggles[num];
            GameUtility.SetToggle(item.toggle, (this.CurrentType & item.type) > 0);
            num += 1;
        Label_002F:
            if (num < ((int) this.FilterToggles.Length))
            {
                goto Label_0007;
            }
            this.SetType();
            return;
        }

        public void ResetType()
        {
            this.CurrentType = this.FirstType;
            this.SetType();
            return;
        }

        public unsafe void Save()
        {
            int num;
            PlayerPrefsUtility.SetString("CARD_FILTERT", &this.CurrentType.ToString(), 1);
            return;
        }

        public void SetType()
        {
            ConceptCardManager manager;
            manager = ConceptCardManager.Instance;
            if ((manager == null) == null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            manager.FilterType = this.CurrentType;
            return;
        }

        public void Start()
        {
            int num;
            Item item;
            num = 0;
            goto Label_0030;
        Label_0007:
            item = this.FilterToggles[num];
            item.toggle.onValueChanged.AddListener(new UnityAction<bool>(this, this.OnSelect));
            num += 1;
        Label_0030:
            if (num < ((int) this.FilterToggles.Length))
            {
                goto Label_0007;
            }
            this.Load();
            return;
        }

        [Serializable]
        public class FilterItem_CardType
        {
            public Toggle toggle;

            public FilterItem_CardType()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Item
        {
            public Toggle toggle;
            public ConceptCardListFilterWindow.Type type;

            public Item()
            {
                base..ctor();
                return;
            }
        }

        public enum Type
        {
            NONE = 0,
            RARITY_1 = 1,
            RARITY_2 = 2,
            RARITY_3 = 4,
            RARITY_4 = 8,
            RARITY_5 = 0x10
        }
    }
}

