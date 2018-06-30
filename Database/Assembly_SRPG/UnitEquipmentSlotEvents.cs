namespace SRPG
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class UnitEquipmentSlotEvents : ListItemEvents
    {
        [HelpBox("スロットの状態にあわせてこの数値を切り替えます。スロットが空=0、装備は持ってる=1、レベル足りない=2、装備してる=3")]
        public string StateIntName;

        public UnitEquipmentSlotEvents()
        {
            this.StateIntName = "state";
            base..ctor();
            return;
        }

        public SlotStateTypes StateType
        {
            set
            {
                Animator animator;
                animator = base.GetComponent<Animator>();
                if ((animator != null) == null)
                {
                    goto Label_002B;
                }
                animator.SetInteger(this.StateIntName, value);
                animator.Update(1f);
            Label_002B:
                return;
            }
        }

        public enum SlotStateTypes
        {
            Empty,
            HasEquipment,
            NeedMoreLevel,
            Equipped,
            EnableCraft,
            EnableCraftNeedMoreLevel,
            None,
            EnableCommon,
            EnableCommonSoul,
            EnableCommonSoulNeedMoreLevel
        }
    }
}

