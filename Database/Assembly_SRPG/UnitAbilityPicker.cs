namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "リスト更新", 0, 0), Pin(100, "アビリティが選択された", 1, 100)]
    public class UnitAbilityPicker : MonoBehaviour, IFlowInterface
    {
        public SRPG.UnitData UnitData;
        [NonSerialized]
        public int AbilitySlot;
        [StringIsGameObjectID]
        public string InventoryWindowID;
        public UnitAbilityList ListBody;
        public Button ClearButton;
        public bool AlwaysShowClearButton;
        public GameObject NoAbilityMessage;
        public Slider YajirushiSlider;
        public AbilityPickerEvent OnAbilitySelect;
        public AbilityPickerEvent OnAbilityRankUp;

        public UnitAbilityPicker()
        {
            this.AbilitySlot = -1;
            base..ctor();
            return;
        }

        private void _OnAbilityRankUp(AbilityData ability, GameObject itemGO)
        {
            if (this.OnAbilityRankUp == null)
            {
                goto Label_0019;
            }
            this.OnAbilityRankUp(ability, itemGO);
            return;
        Label_0019:
            return;
        }

        private void _OnAbilitySelect(AbilityData ability, GameObject itemGO)
        {
            int num;
            SRPG.UnitData data;
            SRPG.UnitData data2;
            if (this.OnAbilitySelect == null)
            {
                goto Label_0019;
            }
            this.OnAbilitySelect(ability, itemGO);
            return;
        Label_0019:
            Debug.Log("OnAbilitySelect");
            num = (this.AbilitySlot < 0) ? GlobalVars.SelectedAbilitySlot : this.AbilitySlot;
            data = DataSource.FindDataOfClass<SRPG.UnitData>(base.get_gameObject(), null);
            if ((data == null) || (ability == null))
            {
                goto Label_006B;
            }
            data.CurrentJob.SetAbilitySlot(num, ability);
        Label_006B:
            data2 = (this.UnitData == null) ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID) : this.UnitData;
            if (data2 != null)
            {
                goto Label_00A2;
            }
            return;
        Label_00A2:
            if (ability != null)
            {
                goto Label_00A9;
            }
            return;
        Label_00A9:
            data2.CurrentJob.SetAbilitySlot(num, ability);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_000C;
            }
            this.Refresh();
        Label_000C:
            return;
        }

        private void OnClearSlot()
        {
            SRPG.UnitData data;
            int num;
            if (this.OnAbilitySelect == null)
            {
                goto Label_0019;
            }
            this.OnAbilitySelect(null, null);
            return;
        Label_0019:
            Debug.Log("OnClearSlot");
            data = (this.UnitData == null) ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID) : this.UnitData;
            if (data != null)
            {
                goto Label_005A;
            }
            return;
        Label_005A:
            num = GlobalVars.SelectedAbilitySlot;
            data.CurrentJob.SetAbilitySlot(num, null);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        public void Refresh()
        {
            SRPG.UnitData data;
            int num;
            EAbilitySlot slot;
            bool flag;
            if ((this.ListBody == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            data = (this.UnitData == null) ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID) : this.UnitData;
            if (data != null)
            {
                goto Label_0049;
            }
            return;
        Label_0049:
            this.ListBody.Unit = data;
            this.ListBody.OnAbilitySelect = new UnitAbilityList.AbilityEvent(this._OnAbilitySelect);
            this.ListBody.OnAbilityRankUp = new UnitAbilityList.AbilityEvent(this._OnAbilityRankUp);
            num = (this.AbilitySlot < 0) ? GlobalVars.SelectedAbilitySlot : this.AbilitySlot;
            slot = JobData.ABILITY_SLOT_TYPES[num];
            this.ListBody.ShowFixedAbilities = num == 0;
            this.ListBody.DisplaySlotType(slot, 1, 1);
            if ((this.ClearButton != null) == null)
            {
                goto Label_0122;
            }
            if (this.AlwaysShowClearButton != null)
            {
                goto Label_0111;
            }
            flag = data.CurrentJob.AbilitySlots[num] == 0L;
            this.ClearButton.get_gameObject().SetActive(flag == 0);
            goto Label_0122;
        Label_0111:
            this.ClearButton.get_gameObject().SetActive(1);
        Label_0122:
            if ((this.NoAbilityMessage != null) == null)
            {
                goto Label_0149;
            }
            this.NoAbilityMessage.SetActive(this.ListBody.IsEmpty);
        Label_0149:
            if ((this.YajirushiSlider != null) == null)
            {
                goto Label_0185;
            }
            this.YajirushiSlider.set_value(((((float) num) / 5f) * this.YajirushiSlider.get_maxValue()) + this.YajirushiSlider.get_minValue());
        Label_0185:
            return;
        }

        private void Start()
        {
            if ((this.ClearButton != null) == null)
            {
                goto Label_002D;
            }
            this.ClearButton.get_onClick().AddListener(new UnityAction(this, this.OnClearSlot));
        Label_002D:
            this.Refresh();
            return;
        }

        public delegate void AbilityPickerEvent(AbilityData ability, GameObject itemGO);
    }
}

