namespace SRPG
{
    using System;
    using UnityEngine;

    public class UnitEnhancePanel : MonoBehaviour
    {
        public UnitEquipmentSlotEvents[] EquipmentSlots;
        public SRPG_Button JobRankUpButton;
        public SRPG_Button JobUnlockButton;
        public SRPG_Button AllEquipButton;
        public GameObject JobRankCapCaution;
        public SRPG_Button JobRankupAllIn;
        [Space(10f)]
        public GenericSlot ArtifactSlot;
        [Space(10f)]
        public GenericSlot ArtifactSlot2;
        [Space(10f)]
        public GenericSlot ArtifactSlot3;
        [Space(10f)]
        public RectTransform ExpItemList;
        public ListItemEvents ExpItemTemplate;
        public SRPG_Button UnitLevelupButton;
        [Space(10f)]
        public UnitAbilityList AbilityList;
        [Space(10f)]
        public UnitAbilityList AbilitySlots;
        [Space(10f)]
        public GenericSlot mConceptCardSlot;
        public ConceptCardIcon mEquipConceptCardIcon;

        public UnitEnhancePanel()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            Canvas canvas;
            canvas = base.GetComponent<Canvas>();
            if ((canvas != null) == null)
            {
                goto Label_001A;
            }
            canvas.set_enabled(0);
        Label_001A:
            return;
        }

        private void Start()
        {
            if ((this.ExpItemTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.ExpItemTemplate.get_gameObject().SetActive(0);
        Label_0022:
            return;
        }
    }
}

