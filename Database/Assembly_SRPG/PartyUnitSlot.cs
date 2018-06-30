namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class PartyUnitSlot : MonoBehaviour
    {
        public static PartyUnitSlot Active;
        [HelpBox("パーティ編成画面のユニットを割り当てるスロット。 選択状態あわせて StateAnimator に指定された Animator の bool 値を切り替えます。イベントにSelect()を登録してください。")]
        public Animator StateAnimator;
        public string AnimatorBoolName;
        public GameObject[] HideIfEmpty;
        public int Index;
        public bool ToggleButtonIfEmpty;
        private Button mButton;

        public PartyUnitSlot()
        {
            this.AnimatorBoolName = "active";
            this.HideIfEmpty = new GameObject[0];
            base..ctor();
            return;
        }

        private void Awake()
        {
            if (this.ToggleButtonIfEmpty == null)
            {
                goto Label_001C;
            }
            this.mButton = base.get_gameObject().GetComponent<Button>();
        Label_001C:
            this.ToggleButton();
            return;
        }

        public void Select()
        {
            Active = this;
            return;
        }

        private void ToggleButton()
        {
            UnitData data;
            if (this.ToggleButtonIfEmpty == null)
            {
                goto Label_001C;
            }
            if ((this.mButton == null) == null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            this.mButton.set_interactable((data == null) == 0);
            return;
        }

        public void Update()
        {
            UnitData data;
            bool flag;
            int num;
            if ((this.StateAnimator != null) == null)
            {
                goto Label_003D;
            }
            if (string.IsNullOrEmpty(this.AnimatorBoolName) != null)
            {
                goto Label_003D;
            }
            this.StateAnimator.SetBool(this.AnimatorBoolName, Active == this);
        Label_003D:
            flag = (DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null) == null) == 0;
            num = 0;
            goto Label_007E;
        Label_0059:
            if ((this.HideIfEmpty[num] != null) == null)
            {
                goto Label_007A;
            }
            this.HideIfEmpty[num].SetActive(flag);
        Label_007A:
            num += 1;
        Label_007E:
            if (num < ((int) this.HideIfEmpty.Length))
            {
                goto Label_0059;
            }
            this.ToggleButton();
            return;
        }
    }
}

