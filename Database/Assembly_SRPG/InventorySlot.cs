namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class InventorySlot : MonoBehaviour
    {
        public static InventorySlot Active;
        public Animator StateAnimator;
        public string AnimatorBoolName;
        public GameObject Empty;
        public GameObject NonEmpty;
        public GameObject[] HideIfEmpty;
        public int Index;
        public SRPG_Button Button;

        public InventorySlot()
        {
            this.AnimatorBoolName = "active";
            this.HideIfEmpty = new GameObject[0];
            base..ctor();
            return;
        }

        public void Select()
        {
            Active = this;
            return;
        }

        public void SetItem(ItemData item)
        {
            bool flag;
            DataSource.Bind<ItemData>(base.get_gameObject(), item);
            flag = item == null;
            if ((this.Empty != null) == null)
            {
                goto Label_002E;
            }
            this.Empty.SetActive(flag);
        Label_002E:
            if ((this.NonEmpty != null) == null)
            {
                goto Label_004E;
            }
            this.NonEmpty.SetActive(flag == 0);
        Label_004E:
            GameParameter.UpdateAll(base.get_gameObject());
            return;
        }

        public void Update()
        {
            PlayerData data;
            bool flag;
            int num;
            if (((this.StateAnimator != null) == null) || (string.IsNullOrEmpty(this.AnimatorBoolName) != null))
            {
                goto Label_003D;
            }
            this.StateAnimator.SetBool(this.AnimatorBoolName, Active == this);
        Label_003D:
            data = MonoSingleton<GameManager>.Instance.Player;
            flag = 0;
            if ((0 > this.Index) || (this.Index >= ((int) data.Inventory.Length)))
            {
                goto Label_0097;
            }
            flag = (data.Inventory[this.Index] == null) ? 0 : ((data.Inventory[this.Index].Param == null) == 0);
        Label_0097:
            num = 0;
            goto Label_00C3;
        Label_009E:
            if ((this.HideIfEmpty[num] != null) == null)
            {
                goto Label_00BF;
            }
            this.HideIfEmpty[num].SetActive(flag);
        Label_00BF:
            num += 1;
        Label_00C3:
            if (num < ((int) this.HideIfEmpty.Length))
            {
                goto Label_009E;
            }
            return;
        }
    }
}

