namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "Job Change(True)", 0, 0), Pin(1, "Job Change(False)", 0, 1)]
    public class AbilitySlots : MonoBehaviour, IFlowInterface
    {
        public UnitAbilityPicker Prefab_AbilityPicker;
        public UnitAbilityList unitAbilityList;
        public GameObject abilityExplanationText;
        public GameObject refreshRoot;
        private bool button_enable;
        private UnitAbilityPicker mAbilityPicker;
        private UnitData mCurrentUnit;
        private bool mIsConnecting;

        public AbilitySlots()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if ((pinID != null) && (pinID != 1))
            {
                goto Label_0020;
            }
            this.Refresh((pinID != null) ? 0 : 1);
        Label_0020:
            return;
        }

        private void OnAbilitySlotSelect(int slotIndex)
        {
            if (this.mIsConnecting != null)
            {
                goto Label_0027;
            }
            if ((this.mAbilityPicker == null) != null)
            {
                goto Label_0027;
            }
            if (this.mCurrentUnit != null)
            {
                goto Label_0028;
            }
        Label_0027:
            return;
        Label_0028:
            this.mAbilityPicker.UnitData = this.mCurrentUnit;
            this.mAbilityPicker.AbilitySlot = slotIndex;
            this.mAbilityPicker.Refresh();
            this.mAbilityPicker.GetComponent<WindowController>().Open();
            return;
        }

        private void OnDestroy()
        {
            if ((this.mAbilityPicker != null) == null)
            {
                goto Label_0021;
            }
            Object.Destroy(this.mAbilityPicker.get_gameObject());
        Label_0021:
            this.mAbilityPicker = null;
            return;
        }

        private void OnSlotAbilitySelect(AbilityData ability, GameObject itemGO)
        {
            int num;
            long num2;
            num = this.mAbilityPicker.AbilitySlot;
            this.mAbilityPicker.GetComponent<WindowController>().Close();
            this.mCurrentUnit.SetEquipAbility(this.mCurrentUnit.JobIndex, num, (ability == null) ? 0L : ability.UniqueID);
            MonoSingleton<GameManager>.Instance.Player.OnChangeAbilitySet(this.mCurrentUnit.UnitID);
            this.unitAbilityList.DisplaySlots();
            this.mIsConnecting = 1;
            Network.RequestAPI(new ReqJobAbility(this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.AbilitySlots, new Network.ResponseCallback(this.Res_UpdateEquippedAbility)), 0);
            return;
        }

        public void Refresh(bool enable)
        {
            this.button_enable = enable;
            this.SetButtonEnabled();
            this.SetAbilityExplanationText();
            return;
        }

        private unsafe void Res_UpdateEquippedAbility(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0039;
            }
            switch ((Network.ErrCode - 0x960))
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_002D;

                case 2:
                    goto Label_002D;
            }
            goto Label_0033;
        Label_002D:
            FlowNode_Network.Failed();
            return;
        Label_0033:
            FlowNode_Network.Retry();
            return;
        Label_0039:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0046:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.units);
                goto Label_0076;
            }
            catch (Exception exception1)
            {
            Label_0060:
                exception = exception1;
                Debug.LogException(exception);
                FlowNode_Network.Failed();
                goto Label_009E;
            }
        Label_0076:
            Network.RemoveAPI();
            this.mIsConnecting = 0;
            if ((this.refreshRoot != null) == null)
            {
                goto Label_009E;
            }
            GameParameter.UpdateAll(this.refreshRoot);
        Label_009E:
            return;
        }

        public void SetAbilityExplanationText()
        {
            if ((this.abilityExplanationText != null) == null)
            {
                goto Label_0022;
            }
            this.abilityExplanationText.SetActive(this.button_enable);
        Label_0022:
            return;
        }

        public void SetButtonEnabled()
        {
            int num;
            Transform transform;
            Button button;
            num = 0;
            goto Label_0037;
        Label_0007:
            button = base.get_transform().GetChild(num).GetComponentInChildren<Button>();
            if ((button != null) == null)
            {
                goto Label_0033;
            }
            button.set_enabled(this.button_enable);
        Label_0033:
            num += 1;
        Label_0037:
            if (num < base.get_transform().get_childCount())
            {
                goto Label_0007;
            }
            return;
        }

        private void Start()
        {
            if ((this.Prefab_AbilityPicker != null) == null)
            {
                goto Label_0039;
            }
            this.mAbilityPicker = Object.Instantiate<UnitAbilityPicker>(this.Prefab_AbilityPicker);
            this.mAbilityPicker.OnAbilitySelect = new UnitAbilityPicker.AbilityPickerEvent(this.OnSlotAbilitySelect);
        Label_0039:
            if ((this.unitAbilityList != null) == null)
            {
                goto Label_0072;
            }
            this.unitAbilityList.OnSlotSelect = new UnitAbilityList.AbilitySlotEvent(this.OnAbilitySlotSelect);
            this.mCurrentUnit = this.unitAbilityList.Unit;
        Label_0072:
            this.SetButtonEnabled();
            this.SetAbilityExplanationText();
            return;
        }
    }
}

