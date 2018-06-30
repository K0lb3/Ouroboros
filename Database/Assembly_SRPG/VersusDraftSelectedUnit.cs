namespace SRPG
{
    using System;
    using UnityEngine;

    public class VersusDraftSelectedUnit : MonoBehaviour
    {
        [SerializeField]
        private UnitIcon mUnitIcon;
        [SerializeField]
        private GameObject mSelecting;
        [SerializeField]
        private GameObject mSecretIcon;
        private DataSource mDataSource;

        public VersusDraftSelectedUnit()
        {
            base..ctor();
            return;
        }

        public void Initialize()
        {
            if ((this.mUnitIcon != null) == null)
            {
                goto Label_003D;
            }
            if ((this.mUnitIcon.Icon != null) == null)
            {
                goto Label_003D;
            }
            this.mUnitIcon.Icon.get_gameObject().SetActive(0);
        Label_003D:
            this.mDataSource = DataSource.Create(base.get_gameObject());
            return;
        }

        public void Select(UnitData unit)
        {
            if ((this.mSelecting != null) == null)
            {
                goto Label_001D;
            }
            this.mSelecting.SetActive(0);
        Label_001D:
            this.SetUnit(unit);
            return;
        }

        public void Selecting()
        {
            if ((this.mSelecting != null) == null)
            {
                goto Label_001D;
            }
            this.mSelecting.SetActive(1);
        Label_001D:
            return;
        }

        public void SetUnit(UnitData unit)
        {
            if ((this.mSecretIcon != null) == null)
            {
                goto Label_0020;
            }
            this.mSecretIcon.SetActive(unit == null);
        Label_0020:
            if ((this.mUnitIcon != null) == null)
            {
                goto Label_0063;
            }
            if ((this.mUnitIcon.Icon != null) == null)
            {
                goto Label_0063;
            }
            this.mUnitIcon.Icon.get_gameObject().SetActive((unit == null) == 0);
        Label_0063:
            if ((this.mUnitIcon != null) == null)
            {
                goto Label_00A0;
            }
            this.mDataSource.Clear();
            this.mDataSource.Add(typeof(UnitData), unit);
            this.mUnitIcon.UpdateValue();
        Label_00A0:
            return;
        }
    }
}

