// Decompiled with JetBrains decompiler
// Type: SRPG.UnitManagementWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "強化画面へ戻る", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(13, "進化ウィンドウへ戻る", FlowNode.PinTypes.Output, 130)]
  [FlowNode.Pin(12, "限界突破画面へ戻る", FlowNode.PinTypes.Output, 120)]
  [FlowNode.Pin(11, "キャラクタークエスト画面へ戻る", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(510, "ユニット選択へ戻る", FlowNode.PinTypes.Output, 510)]
  [FlowNode.Pin(500, "ユニット強化", FlowNode.PinTypes.Input, 500)]
  [FlowNode.Pin(2, "ユニット選択へ", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(1, "初期化開始", FlowNode.PinTypes.Input, 10)]
  public class UnitManagementWindow : MonoBehaviour, IFlowInterface
  {
    public UnitListWindow Prefab_UnitListWindow;
    public UnitEquipmentWindow Prefab_EquipmentWindow;
    public UnitKakeraWindow Prefab_KakeraWindow;
    public UnitEnhanceV3 Prefab_UEWindow;
    private bool mInitialize;
    private UnitListWindow mUnitListWindow;
    private UnitEnhanceV3 mUEMain;
    private UnitEquipmentWindow mEquipWindow;
    private UnitKakeraWindow mKakeraWindow;
    private LoadRequest mReqAbilityDetail;
    private UnitListRootWindow.Tab mCurrentTab;
    private Vector2 mCurrentTabAnchorePos;
    private long mCurrentUnit;

    public UnitManagementWindow()
    {
      base.\u002Ector();
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CStart\u003Ec__Iterator13A() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitKyouka()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitKyouka\u003Ec__Iterator13B() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitCharacterQuest()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitCharacterQuest\u003Ec__Iterator13C() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitKakera()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitKakera\u003Ec__Iterator13D() { \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    private IEnumerator InitializeUnitEvolution()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CInitializeUnitEvolution\u003Ec__Iterator13E() { \u003C\u003Ef__this = this };
    }

    private void Release()
    {
      this.DestroyUnitList();
      if (Object.op_Inequality((Object) this.mUEMain, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mUEMain).get_gameObject());
        this.mUEMain = (UnitEnhanceV3) null;
      }
      if (Object.op_Inequality((Object) this.mEquipWindow, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mEquipWindow).get_gameObject());
        this.mEquipWindow = (UnitEquipmentWindow) null;
      }
      if (!Object.op_Inequality((Object) this.mKakeraWindow, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mKakeraWindow).get_gameObject());
      this.mKakeraWindow = (UnitKakeraWindow) null;
    }

    public void CreateUnitList()
    {
      if (!Object.op_Inequality((Object) this.Prefab_UnitListWindow, (Object) null) || !Object.op_Equality((Object) this.mUnitListWindow, (Object) null))
        return;
      this.mUnitListWindow = (UnitListWindow) Object.Instantiate<UnitListWindow>((M0) this.Prefab_UnitListWindow);
      ((Component) this.mUnitListWindow).get_transform().SetParent(((Component) this).get_transform(), false);
    }

    public void DestroyUnitList()
    {
      if (!Object.op_Inequality((Object) this.mUnitListWindow, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.mUnitListWindow).get_gameObject());
      this.mUnitListWindow = (UnitListWindow) null;
    }

    private List<long> GetDefaultUnitList(long selectUniq)
    {
      List<long> longList = new List<long>();
      if (Object.op_Inequality((Object) this.mUEMain, (Object) null))
      {
        List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
        List<UnitListWindow.Data> list = new List<UnitListWindow.Data>();
        for (int index = 0; index < units.Count; ++index)
        {
          if (units[index] != null)
          {
            UnitListWindow.Data data = new UnitListWindow.Data(units[index]);
            data.Refresh();
            if (data.GetUniq() == selectUniq)
              data.filterMask = UnitListFilterWindow.SelectType.NONE;
            list.Add(data);
          }
        }
        if (Object.op_Inequality((Object) this.mUnitListWindow, (Object) null))
        {
          if (this.mUnitListWindow.filterWindow != null)
            this.mUnitListWindow.filterWindow.CalcUnit(list);
          if (this.mUnitListWindow.sortWindow != null)
            this.mUnitListWindow.sortWindow.CalcUnit(list);
        }
        for (int index = 0; index < list.Count; ++index)
          longList.Add(list[index].GetUniq());
      }
      return longList;
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        this.mInitialize = true;
      }
      else
      {
        if (pinID != 500)
          return;
        this.OnUnitSelect(GlobalVars.SelectedUnitUniqueID.Get());
      }
    }

    private void OnDestroy()
    {
      this.Release();
    }

    private void OnUnitSelect(long uniqueID)
    {
      if (!Object.op_Inequality((Object) this.mUnitListWindow, (Object) null) || !this.mUnitListWindow.IsEnabled())
        return;
      UnitListRootWindow rootWindow = this.mUnitListWindow.rootWindow;
      if (rootWindow == null || rootWindow.GetEditType() != UnitListWindow.EditType.DEFAULT)
        return;
      UnitListRootWindow.ListData listData = rootWindow.GetListData("unitlist");
      if (listData != null)
      {
        this.mCurrentTab = rootWindow.GetCurrentTab();
        this.mCurrentTabAnchorePos = rootWindow.GetCurrentTabAnchore();
        this.mCurrentUnit = uniqueID;
        this.mUEMain.UnitList = listData.GetUniqs();
      }
      else
      {
        List<long> longList = new List<long>();
        List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
        for (int index = 0; index < units.Count; ++index)
          longList.Add(units[index].UniqueID);
        this.mUEMain.UnitList = longList;
      }
      this.mUEMain.Refresh(uniqueID, 0L, false);
      this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.OnUEWindowClosedByUser);
      ButtonEvent.Invoke("UNITLIST_BTN_CLOSE", (object) null);
      this.mUnitListWindow.Enabled(false);
    }

    private void OnUEWindowClosedByUser()
    {
      this.mUEMain.OnUserClose = (UnitEnhanceV3.CloseEvent) null;
      if (this.mUEMain.GetDirtyUnits().Length > 0)
        this.mUEMain.ClearDirtyUnits();
      UnitListRootWindow.TabRegister tabRegister = new UnitListRootWindow.TabRegister();
      if (this.mCurrentTab != UnitListRootWindow.Tab.NONE)
      {
        tabRegister.tab = this.mCurrentTab;
        tabRegister.anchorePos = this.mCurrentTabAnchorePos;
        string s = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
        if (!string.IsNullOrEmpty(s))
        {
          long num = long.Parse(s);
          if (num > 0L && this.mCurrentUnit != num)
            tabRegister.forcus = num;
          FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
        }
      }
      else
      {
        tabRegister.tab = UnitListRootWindow.Tab.ALL;
        tabRegister.forcus = GlobalVars.SelectedUnitUniqueID.Get();
        string s = FlowNode_Variable.Get("LAST_SELECTED_UNITID");
        if (!string.IsNullOrEmpty(s))
        {
          long num = long.Parse(s);
          if (num > 0L)
            tabRegister.forcus = num;
          FlowNode_Variable.Set("LAST_SELECTED_UNITID", string.Empty);
        }
      }
      SerializeValueList serializeValueList = new SerializeValueList();
      serializeValueList.AddObject("data_register", (object) tabRegister);
      FlowNode_ButtonEvent.currentValue = (object) serializeValueList;
      this.mUnitListWindow.Enabled(true);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 510);
    }
  }
}
