namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;

    public class ConceptCardSlots : MonoBehaviour
    {
        private static readonly string CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH;
        [SerializeField]
        private GenericSlot mConceptCardSlot;
        [SerializeField]
        private ConceptCardIcon mConceptCardIcon;
        [SerializeField]
        private GameObject mToolTipRoot;
        private UnitData mUnit;
        private bool mIsButtonEnable;

        static ConceptCardSlots()
        {
            CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH = "UI/ConceptCardSelect";
            return;
        }

        public ConceptCardSlots()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnEquipCardSlot>m__2DF(GameObject go)
        {
            this.OnCloseEquipConceptCardWindow();
            return;
        }

        private void OnCloseEquipConceptCardWindow()
        {
            UnitData data;
            if ((this.mToolTipRoot == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mUnit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(this.mUnit.UniqueID);
            data = DataSource.FindDataOfClass<UnitData>(this.mToolTipRoot, null);
            data.ConceptCard = this.mUnit.ConceptCard;
            data.CalcStatus();
            this.Refresh(this.mIsButtonEnable);
            GameParameter.UpdateAll(this.mToolTipRoot);
            return;
        }

        private void OnEquipCardSlot()
        {
            DestroyEventListener local1;
            GameObject obj2;
            GameObject obj3;
            ConceptCardEquipWindow window;
            DestroyEventListener listener;
            if (this.mUnit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            obj2 = AssetManager.Load<GameObject>(CONCEPT_CARD_EQUIP_WINDOW_PREFAB_PATH);
            if ((obj2 == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            obj3 = Object.Instantiate<GameObject>(obj2);
            window = obj3.GetComponent<ConceptCardEquipWindow>();
            if ((window != null) == null)
            {
                goto Label_0073;
            }
            window.Init(this.mUnit);
            local1 = obj3.AddComponent<DestroyEventListener>();
            local1.Listeners = (DestroyEventListener.DestroyEvent) Delegate.Combine(local1.Listeners, new DestroyEventListener.DestroyEvent(this.<OnEquipCardSlot>m__2DF));
        Label_0073:
            return;
        }

        public void Refresh(bool enable)
        {
            UnitData data;
            bool flag;
            data = DataSource.FindDataOfClass<UnitData>(base.get_transform().get_parent().get_gameObject(), null);
            if (data == null)
            {
                goto Label_0024;
            }
            this.mUnit = data;
        Label_0024:
            if (this.mUnit != null)
            {
                goto Label_0030;
            }
            return;
        Label_0030:
            flag = 0;
            this.mIsButtonEnable = enable;
            this.RefreshSlots(this.mConceptCardSlot, this.mConceptCardIcon, this.mUnit.ConceptCard, flag, this.mIsButtonEnable);
            return;
        }

        private void RefreshSlots(GenericSlot slot, ConceptCardIcon icon, ConceptCardData card, bool is_locked, bool enable)
        {
            SRPG_Button button;
            if ((slot == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if ((icon == null) == null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            slot.SetLocked(is_locked);
            slot.SetSlotData<ConceptCardData>(card);
            icon.Setup(card);
            button = slot.get_gameObject().GetComponentInChildren<SRPG_Button>();
            if ((button != null) == null)
            {
                goto Label_0072;
            }
            button.set_enabled(enable);
            button.get_onClick().RemoveAllListeners();
            button.get_onClick().AddListener(new UnityAction(this, this.OnEquipCardSlot));
        Label_0072:
            return;
        }
    }
}

