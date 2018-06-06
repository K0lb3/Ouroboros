// Decompiled with JetBrains decompiler
// Type: SRPG.UnitManagementWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "初期化完了", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "初期化開始", FlowNode.PinTypes.Input, 1)]
  public class UnitManagementWindow : MonoBehaviour, IFlowInterface
  {
    public UnitListV2 Prefab_UnitListWindow;
    public UnitEquipmentWindow Prefab_EquipmentWindow;
    public UnitKakeraWindow Prefab_KakeraWindow;
    public UnitEnhanceV3 Prefab_UEWindow;
    private bool mInitialize;
    private UnitListV2 mUnitList;
    private UnitEnhanceV3 mUEMain;
    private UnitEquipmentWindow mEquipWindow;
    private UnitKakeraWindow mKakeraWindow;
    private LoadRequest mReqAbilityDetail;

    public UnitManagementWindow()
    {
      base.\u002Ector();
    }

    [DebuggerHidden]
    private IEnumerator Start()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitManagementWindow.\u003CStart\u003Ec__IteratorF1() { \u003C\u003Ef__this = this };
    }

    private void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.mUnitList, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mUnitList).get_gameObject());
        this.mUnitList = (UnitListV2) null;
      }
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

    private void OnUnitSelect(long uniqueID)
    {
      this.mUEMain.Refresh(uniqueID, 0L, false);
      this.mUEMain.OnUserClose = new UnitEnhanceV3.CloseEvent(this.OnUEWindowClosedByUser);
      ((WindowController) ((Component) this.mUnitList).GetComponent<WindowController>()).Close();
    }

    private void OnUEWindowClosedByUser()
    {
      this.mUEMain.OnUserClose = (UnitEnhanceV3.CloseEvent) null;
      if (this.mUEMain.GetDirtyUnits().Length > 0)
      {
        this.mUEMain.ClearDirtyUnits();
        this.mUnitList.RefreshData();
      }
      ((WindowController) ((Component) this.mUnitList).GetComponent<WindowController>()).Open();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.mInitialize = true;
    }
  }
}
