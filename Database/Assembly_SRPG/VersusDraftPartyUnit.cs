namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusDraftPartyUnit : MonoBehaviour
    {
        private static SRPG.VersusDraftPartyEdit mVersusDraftPartyEdit;
        private SRPG.UnitData mUnitData;
        private VersusDraftPartySlot mTargetSlot;
        [SerializeField]
        private GameObject mSelected;

        public VersusDraftPartyUnit()
        {
            base..ctor();
            return;
        }

        public void OnClick(Button button)
        {
            VersusDraftPartySlot slot;
            if ((VersusDraftPartySlot.CurrentSelected == null) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if ((this.mTargetSlot != null) == null)
            {
                goto Label_0063;
            }
            slot = this.mTargetSlot;
            this.mTargetSlot.PartyUnit.Reset();
            if ((VersusDraftPartySlot.CurrentSelected.PartyUnit != null) == null)
            {
                goto Label_0087;
            }
            VersusDraftPartySlot.CurrentSelected.PartyUnit.Select(slot);
            goto Label_0087;
        Label_0063:
            if ((VersusDraftPartySlot.CurrentSelected.PartyUnit != null) == null)
            {
                goto Label_0087;
            }
            VersusDraftPartySlot.CurrentSelected.PartyUnit.Reset();
        Label_0087:
            this.Select(VersusDraftPartySlot.CurrentSelected);
            mVersusDraftPartyEdit.SelectNextSlot();
            return;
        }

        public void Reset()
        {
            this.mSelected.SetActive(0);
            if ((this.mTargetSlot != null) == null)
            {
                goto Label_0030;
            }
            this.mTargetSlot.SetUnit(null);
            this.mTargetSlot = null;
        Label_0030:
            return;
        }

        public void Select(VersusDraftPartySlot slot)
        {
            this.mSelected.SetActive(1);
            this.mTargetSlot = slot;
            this.mTargetSlot.SetUnit(this);
            return;
        }

        public void SetUp(SRPG.UnitData unit)
        {
            this.mUnitData = unit;
            this.mSelected.SetActive(0);
            DataSource.Bind<SRPG.UnitData>(base.get_gameObject(), this.mUnitData);
            base.get_gameObject().SetActive(1);
            return;
        }

        public static SRPG.VersusDraftPartyEdit VersusDraftPartyEdit
        {
            get
            {
                return mVersusDraftPartyEdit;
            }
            set
            {
                mVersusDraftPartyEdit = value;
                return;
            }
        }

        public SRPG.UnitData UnitData
        {
            get
            {
                return this.mUnitData;
            }
        }

        public bool IsSetSlot
        {
            get
            {
                return (this.mTargetSlot != null);
            }
        }
    }
}

