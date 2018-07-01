// Decompiled with JetBrains decompiler
// Type: SRPG.AbilitySlots
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Job Change(False)", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Job Change(True)", FlowNode.PinTypes.Input, 0)]
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
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0 && pinID != 1)
        return;
      this.Refresh(pinID == 0);
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Prefab_AbilityPicker, (UnityEngine.Object) null))
      {
        this.mAbilityPicker = (UnitAbilityPicker) UnityEngine.Object.Instantiate<UnitAbilityPicker>((M0) this.Prefab_AbilityPicker);
        this.mAbilityPicker.OnAbilitySelect = new UnitAbilityPicker.AbilityPickerEvent(this.OnSlotAbilitySelect);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.unitAbilityList, (UnityEngine.Object) null))
      {
        this.unitAbilityList.OnSlotSelect = new UnitAbilityList.AbilitySlotEvent(this.OnAbilitySlotSelect);
        this.mCurrentUnit = this.unitAbilityList.Unit;
      }
      this.SetButtonEnabled();
      this.SetAbilityExplanationText();
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mAbilityPicker, (UnityEngine.Object) null))
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.mAbilityPicker).get_gameObject());
      this.mAbilityPicker = (UnitAbilityPicker) null;
    }

    private void OnAbilitySlotSelect(int slotIndex)
    {
      if (this.mIsConnecting || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mAbilityPicker, (UnityEngine.Object) null) || this.mCurrentUnit == null)
        return;
      this.mAbilityPicker.UnitData = this.mCurrentUnit;
      this.mAbilityPicker.AbilitySlot = slotIndex;
      this.mAbilityPicker.Refresh();
      ((WindowController) ((Component) this.mAbilityPicker).GetComponent<WindowController>()).Open();
    }

    private void OnSlotAbilitySelect(AbilityData ability, GameObject itemGO)
    {
      int abilitySlot = this.mAbilityPicker.AbilitySlot;
      ((WindowController) ((Component) this.mAbilityPicker).GetComponent<WindowController>()).Close();
      this.mCurrentUnit.SetEquipAbility(this.mCurrentUnit.JobIndex, abilitySlot, ability == null ? 0L : ability.UniqueID);
      MonoSingleton<GameManager>.Instance.Player.OnChangeAbilitySet(this.mCurrentUnit.UnitID);
      this.unitAbilityList.DisplaySlots();
      this.mIsConnecting = true;
      Network.RequestAPI((WebAPI) new ReqJobAbility(this.mCurrentUnit.CurrentJob.UniqueID, this.mCurrentUnit.CurrentJob.AbilitySlots, new Network.ResponseCallback(this.Res_UpdateEquippedAbility)), false);
    }

    private void Res_UpdateEquippedAbility(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.NoAbilitySetAbility:
          case Network.EErrCode.NoJobSetAbility:
          case Network.EErrCode.UnsetAbility:
            FlowNode_Network.Failed();
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<Json_PlayerDataAll> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(www.text);
        try
        {
          MonoSingleton<GameManager>.Instance.Deserialize(jsonObject.body.units);
        }
        catch (Exception ex)
        {
          Debug.LogException(ex);
          FlowNode_Network.Failed();
          return;
        }
        Network.RemoveAPI();
        this.mIsConnecting = false;
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.refreshRoot, (UnityEngine.Object) null))
          return;
        GameParameter.UpdateAll(this.refreshRoot);
      }
    }

    public void Refresh(bool enable)
    {
      this.button_enable = enable;
      this.SetButtonEnabled();
      this.SetAbilityExplanationText();
    }

    public void SetButtonEnabled()
    {
      for (int index = 0; index < ((Component) this).get_transform().get_childCount(); ++index)
      {
        Button componentInChildren = (Button) ((Component) ((Component) this).get_transform().GetChild(index)).GetComponentInChildren<Button>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
          ((Behaviour) componentInChildren).set_enabled(this.button_enable);
      }
    }

    public void SetAbilityExplanationText()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.abilityExplanationText, (UnityEngine.Object) null))
        return;
      this.abilityExplanationText.SetActive(this.button_enable);
    }
  }
}
