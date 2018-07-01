// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayRoomCreateController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiPlayRoomCreateController : MonoBehaviour
  {
    public Toggle PassToggle;
    public Toggle LimitToggle;
    public Toggle ClearToggle;
    public GameObject Filter;
    public GameObject ClearEnableButton;
    public GameObject ClearDisableButton;
    public GameObject LimitEnableButton;
    public GameObject LimitDisableButton;
    public GameObject DetailFilter;
    public ScrollablePulldown UnitLvPulldown;
    public GraphicRaycaster UnitPulldownBG;
    private int mMaxLv;
    private int mNowSelIdx;

    public MultiPlayRoomCreateController()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      for (int index = 0; index < units.Count; ++index)
      {
        if (this.mMaxLv < units[index].Lv)
          this.mMaxLv = units[index].Lv;
      }
      if (Object.op_Inequality((Object) this.PassToggle, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.PassToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnPassToggle)));
      }
      if (Object.op_Inequality((Object) this.LimitToggle, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.LimitToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnLimitToggle)));
      }
      if (Object.op_Inequality((Object) this.ClearToggle, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.ClearToggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnClearToggle)));
      }
      this.Refresh();
    }

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      string empty = string.Empty;
      OInt[] multiPlayLimitUnitLv = instance.MasterParam.GetMultiPlayLimitUnitLv();
      if (Object.op_Inequality((Object) this.UnitLvPulldown, (Object) null) && multiPlayLimitUnitLv != null)
      {
        int num = 0;
        this.UnitLvPulldown.ClearItems();
        for (int index = 0; index < multiPlayLimitUnitLv.Length; ++index)
        {
          this.UnitLvPulldown.AddItem((int) multiPlayLimitUnitLv[index] != 0 ? multiPlayLimitUnitLv[index].ToString() + LocalizedText.Get("sys.MULTI_JOINLIMIT_OVER") : LocalizedText.Get("sys.MULTI_JOINLIMIT_NONE"), index);
          if ((int) multiPlayLimitUnitLv[index] == GlobalVars.MultiPlayJoinUnitLv)
            num = index;
        }
        this.mNowSelIdx = num;
        this.UnitLvPulldown.Selection = num;
        this.UnitLvPulldown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.OnUnitLvChange);
      }
      if (Object.op_Inequality((Object) this.ClearEnableButton, (Object) null))
        this.ClearEnableButton.SetActive(!GlobalVars.MultiPlayClearOnly);
      if (Object.op_Inequality((Object) this.ClearDisableButton, (Object) null))
        this.ClearDisableButton.SetActive(GlobalVars.MultiPlayClearOnly);
      if (Object.op_Inequality((Object) this.DetailFilter, (Object) null))
        this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
      if (Object.op_Inequality((Object) this.LimitEnableButton, (Object) null))
        this.LimitEnableButton.SetActive(!GlobalVars.SelectedMultiPlayLimit);
      if (Object.op_Inequality((Object) this.LimitDisableButton, (Object) null))
        this.LimitDisableButton.SetActive(GlobalVars.SelectedMultiPlayLimit);
      if (Object.op_Inequality((Object) this.UnitPulldownBG, (Object) null))
        ((Behaviour) this.UnitPulldownBG).set_enabled(true);
      if (Object.op_Inequality((Object) this.LimitToggle, (Object) null))
        this.LimitToggle.set_isOn(GlobalVars.SelectedMultiPlayLimit);
      if (!Object.op_Inequality((Object) this.ClearToggle, (Object) null))
        return;
      this.ClearToggle.set_isOn(GlobalVars.MultiPlayClearOnly);
    }

    public void OnEnablePass()
    {
      if (!Object.op_Inequality((Object) this.DetailFilter, (Object) null))
        return;
      this.DetailFilter.SetActive(true);
    }

    public void OnDisablePass()
    {
      if (!Object.op_Inequality((Object) this.DetailFilter, (Object) null))
        return;
      this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
    }

    public void OnEnableLimit()
    {
      GlobalVars.SelectedMultiPlayLimit = true;
      if (!Object.op_Inequality((Object) this.DetailFilter, (Object) null))
        return;
      this.DetailFilter.SetActive(false);
    }

    public void OnDisableLimit()
    {
      GlobalVars.SelectedMultiPlayLimit = false;
      if (!Object.op_Inequality((Object) this.DetailFilter, (Object) null))
        return;
      this.DetailFilter.SetActive(true);
    }

    public void OnEnableClear()
    {
      GlobalVars.MultiPlayClearOnly = true;
    }

    public void OnDisableClear()
    {
      GlobalVars.MultiPlayClearOnly = false;
    }

    private void OnUnitLvChange(int index)
    {
      OInt[] multiPlayLimitUnitLv = MonoSingleton<GameManager>.Instance.MasterParam.GetMultiPlayLimitUnitLv();
      if (multiPlayLimitUnitLv == null || index >= multiPlayLimitUnitLv.Length)
        return;
      if (this.mMaxLv < (int) multiPlayLimitUnitLv[index])
      {
        this.UnitLvPulldown.Selection = this.mNowSelIdx;
        this.UnitLvPulldown.PrevSelection = -1;
        UIUtility.SystemMessage(LocalizedText.Get("sys.MULTI_LIMIT_ERROR"), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
      else
      {
        this.mNowSelIdx = index;
        GlobalVars.MultiPlayJoinUnitLv = (int) multiPlayLimitUnitLv[index];
      }
    }

    public void OnPassToggle(bool isOn)
    {
      if (isOn)
      {
        if (Object.op_Inequality((Object) this.DetailFilter, (Object) null))
          this.DetailFilter.SetActive(true);
        FlowNode_Variable.Set("MultiPlayPasscode", "1");
      }
      else
      {
        if (Object.op_Inequality((Object) this.DetailFilter, (Object) null))
          this.DetailFilter.SetActive(!GlobalVars.SelectedMultiPlayLimit);
        FlowNode_Variable.Set("MultiPlayPasscode", "0");
      }
      if (!Object.op_Inequality((Object) this.Filter, (Object) null))
        return;
      this.Filter.SetActive(isOn);
    }

    public void OnLimitToggle(bool isOn)
    {
      GlobalVars.SelectedMultiPlayLimit = isOn;
      if (!Object.op_Inequality((Object) this.DetailFilter, (Object) null))
        return;
      this.DetailFilter.SetActive(!isOn);
    }

    public void OnClearToggle(bool isOn)
    {
      GlobalVars.MultiPlayClearOnly = isOn;
    }
  }
}
