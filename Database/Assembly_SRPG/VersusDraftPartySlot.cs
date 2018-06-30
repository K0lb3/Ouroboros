namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusDraftPartySlot : MonoBehaviour
    {
        private static SRPG.VersusDraftPartyEdit mVersusDraftPartyEdit;
        private static VersusDraftPartySlot mCurrentSelected;
        [SerializeField]
        private GameObject mLeaderIcon;
        [SerializeField]
        private GameObject mLocked;
        [SerializeField]
        private GameObject mSelect;
        [SerializeField]
        private Selectable mSelectable;
        private bool mIsLeader;
        private bool mIsLock;
        private DataSource mDataSource;
        private VersusDraftPartyUnit mPartyUnit;

        public VersusDraftPartySlot()
        {
            base..ctor();
            return;
        }

        public void OnClick(Button button)
        {
            if ((mCurrentSelected == this) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            this.SelectSlot(1);
            return;
        }

        public void SelectSlot(bool selected)
        {
            if (selected == null)
            {
                goto Label_0038;
            }
            if ((mCurrentSelected != null) == null)
            {
                goto Label_0021;
            }
            mCurrentSelected.SelectSlot(0);
        Label_0021:
            this.mSelect.SetActive(1);
            mCurrentSelected = this;
            goto Label_0044;
        Label_0038:
            this.mSelect.SetActive(0);
        Label_0044:
            return;
        }

        public void SetUnit(VersusDraftPartyUnit partyUnit)
        {
            if ((this.mDataSource != null) == null)
            {
                goto Label_001C;
            }
            this.mDataSource.Clear();
        Label_001C:
            if ((partyUnit != null) == null)
            {
                goto Label_0043;
            }
            this.mDataSource.Add(typeof(UnitData), partyUnit.UnitData);
        Label_0043:
            this.mPartyUnit = partyUnit;
            GameParameter.UpdateAll(base.get_gameObject());
            mVersusDraftPartyEdit.UpdateParty(this.mIsLeader);
            return;
        }

        public void SetUp(bool is_leader, bool is_locked)
        {
            this.mIsLeader = is_leader;
            this.mIsLock = is_locked;
            if (is_leader == null)
            {
                goto Label_0027;
            }
            this.mLeaderIcon.SetActive(1);
            this.SelectSlot(1);
        Label_0027:
            if (is_locked == null)
            {
                goto Label_0045;
            }
            this.mLocked.SetActive(1);
            this.mSelectable.set_interactable(0);
        Label_0045:
            this.mDataSource = DataSource.Create(base.get_gameObject());
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

        public static VersusDraftPartySlot CurrentSelected
        {
            get
            {
                return mCurrentSelected;
            }
        }

        public bool IsLeader
        {
            get
            {
                return this.mIsLeader;
            }
        }

        public bool IsLock
        {
            get
            {
                return this.mIsLock;
            }
        }

        public VersusDraftPartyUnit PartyUnit
        {
            get
            {
                return this.mPartyUnit;
            }
        }
    }
}

