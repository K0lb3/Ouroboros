namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class PartyUnitSlot2 : MonoBehaviour
    {
        public SelectEvent OnSelect;
        public SRPG_Button SelectButton;
        public GameObject[] HideIfEmpty;
        public RectTransform Empty;
        public RectTransform NonEmpty;
        private UnitData mUnit;

        public PartyUnitSlot2()
        {
            this.HideIfEmpty = new GameObject[0];
            base..ctor();
            return;
        }

        private void Awake()
        {
            if ((this.SelectButton != null) == null)
            {
                goto Label_0028;
            }
            this.SelectButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnButtonClick));
        Label_0028:
            return;
        }

        private void OnButtonClick(SRPG_Button button)
        {
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.OnSelect == null)
            {
                goto Label_0023;
            }
            this.OnSelect(this);
        Label_0023:
            return;
        }

        public void SetUnitData(UnitData unit)
        {
            bool flag;
            int num;
            this.mUnit = unit;
            DataSource.Bind<UnitData>(base.get_gameObject(), unit);
            flag = (unit == null) == 0;
            num = 0;
            goto Label_0047;
        Label_0022:
            if ((this.HideIfEmpty[num] != null) == null)
            {
                goto Label_0043;
            }
            this.HideIfEmpty[num].SetActive(flag);
        Label_0043:
            num += 1;
        Label_0047:
            if (num < ((int) this.HideIfEmpty.Length))
            {
                goto Label_0022;
            }
            if ((this.Empty != null) == null)
            {
                goto Label_007A;
            }
            this.Empty.get_gameObject().SetActive(flag == 0);
        Label_007A:
            if ((this.NonEmpty != null) == null)
            {
                goto Label_009C;
            }
            this.NonEmpty.get_gameObject().SetActive(flag);
        Label_009C:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public UnitData Unit
        {
            get
            {
                return this.mUnit;
            }
        }

        public delegate void SelectEvent(PartyUnitSlot2 slot);
    }
}

