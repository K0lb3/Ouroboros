namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusDraftUnit : MonoBehaviour
    {
        private static SRPG.VersusDraftList mVersusDraftList;
        private static List<VersusDraftUnit> mCurrentSelected;
        private static int mCursorIndex;
        [SerializeField]
        private UnitIcon mUnitIcon;
        [SerializeField]
        private GameObject mCursorPlayer;
        [SerializeField]
        private GameObject mCursorEnemy;
        [SerializeField]
        private GameObject mSecretIcon;
        [SerializeField]
        private GameObject mPickPlayer;
        [SerializeField]
        private GameObject mPickEnemy;
        private SRPG.UnitData mUnitData;
        private bool mIsSelected;
        private bool mIsHidden;

        static VersusDraftUnit()
        {
            mCurrentSelected = new List<VersusDraftUnit>();
            mCursorIndex = 0;
            return;
        }

        public VersusDraftUnit()
        {
            base..ctor();
            return;
        }

        public void DecideUnit(bool isPlayer)
        {
            if (isPlayer == null)
            {
                goto Label_0038;
            }
            if ((this.mPickPlayer != null) == null)
            {
                goto Label_0065;
            }
            this.mPickPlayer.SetActive(1);
            SRPG.VersusDraftList.VersusDraftUnitDataListPlayer.Add(this.mUnitData);
            goto Label_0065;
        Label_0038:
            if ((this.mPickEnemy != null) == null)
            {
                goto Label_0065;
            }
            SRPG.VersusDraftList.VersusDraftUnitDataListEnemy.Add(this.mUnitData);
            this.mPickEnemy.SetActive(1);
        Label_0065:
            this.mIsSelected = 1;
            return;
        }

        public void OnClick(Button button)
        {
            if (SRPG.VersusDraftList.VersusDraftTurnOwn != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            if (this.mIsSelected == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            if (mCurrentSelected.Contains(this) == null)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            this.SelectUnit(1);
            if ((mVersusDraftList != null) == null)
            {
                goto Label_0049;
            }
            mVersusDraftList.SelectUnit();
        Label_0049:
            return;
        }

        public static void ResetSelectUnit()
        {
            int num;
            num = 0;
            goto Label_0059;
        Label_0007:
            if ((mCurrentSelected[num] != null) == null)
            {
                goto Label_0055;
            }
            mCurrentSelected[num].mCursorPlayer.SetActive(0);
            mCurrentSelected[num].mCursorEnemy.SetActive(0);
            mCurrentSelected[num] = null;
        Label_0055:
            num += 1;
        Label_0059:
            if (num < mCurrentSelected.Count)
            {
                goto Label_0007;
            }
            mCursorIndex = 0;
            return;
        }

        public void SelectUnit(bool isPlayer)
        {
            SRPG.UnitData data;
            if (isPlayer == null)
            {
                goto Label_009F;
            }
            if ((mCurrentSelected[mCursorIndex] != null) == null)
            {
                goto Label_003A;
            }
            mCurrentSelected[mCursorIndex].mCursorPlayer.SetActive(0);
        Label_003A:
            this.mCursorPlayer.SetActive(1);
            mCurrentSelected[mCursorIndex] = this;
            data = (mCurrentSelected[mCursorIndex].mIsHidden == null) ? mCurrentSelected[mCursorIndex].UnitData : null;
            VersusDraftList.SetUnit(data, mCursorIndex);
            goto Label_00EF;
        Label_009F:
            if ((mCurrentSelected[mCursorIndex] != null) == null)
            {
                goto Label_00D3;
            }
            mCurrentSelected[mCursorIndex].mCursorEnemy.SetActive(0);
        Label_00D3:
            this.mCursorEnemy.SetActive(1);
            mCurrentSelected[mCursorIndex] = this;
        Label_00EF:
            mCursorIndex += 1;
            if (VersusDraftList.SelectableUnitCount > mCursorIndex)
            {
                goto Label_0115;
            }
            mCursorIndex = 0;
        Label_0115:
            return;
        }

        public void SetUp(SRPG.UnitData unit, Transform parent, bool is_hidden)
        {
            this.mUnitData = unit;
            this.mIsHidden = is_hidden;
            if (this.mIsHidden != null)
            {
                goto Label_002F;
            }
            DataSource.Bind<SRPG.UnitData>(base.get_gameObject(), this.mUnitData);
            goto Label_0047;
        Label_002F:
            this.mSecretIcon.SetActive(1);
            this.mUnitIcon.Tooltip = 0;
        Label_0047:
            base.get_transform().SetParent(parent, 0);
            base.get_gameObject().SetActive(1);
            return;
        }

        public static SRPG.VersusDraftList VersusDraftList
        {
            get
            {
                return mVersusDraftList;
            }
            set
            {
                mVersusDraftList = value;
                return;
            }
        }

        public static List<VersusDraftUnit> CurrentSelectCursors
        {
            get
            {
                return mCurrentSelected;
            }
            set
            {
                mCurrentSelected = value;
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

        public bool IsSelected
        {
            get
            {
                return this.mIsSelected;
            }
        }
    }
}

