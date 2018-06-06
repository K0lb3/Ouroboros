// Decompiled with JetBrains decompiler
// Type: SRPG.GetUnitWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Unit Unlocked", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(100, "Refresh", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(10, "Unit Detail", FlowNode.PinTypes.Output, 10)]
  public class GetUnitWindow : SRPG_FixedList, IFlowInterface, ISortableList
  {
    public GetUnitWindow.UnitSelectEvent OnUnitSelect;
    public Transform ItemLayoutParent;
    public GameObject ItemTemplate;
    public GameObject PieceTemplate;
    public Pulldown SortFilter;
    public GameObject UnitSortFilterButton;
    public GameObject AscendingIcon;
    public GameObject DescendingIcon;

    protected virtual void Awake()
    {
      base.Awake();
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null) && this.ItemTemplate.get_activeInHierarchy())
        this.ItemTemplate.SetActive(false);
      if (!Object.op_Inequality((Object) this.PieceTemplate, (Object) null) || !this.PieceTemplate.get_activeInHierarchy())
        return;
      this.PieceTemplate.SetActive(false);
    }

    public override RectTransform ListParent
    {
      get
      {
        if (Object.op_Inequality((Object) this.ItemLayoutParent, (Object) null))
          return (RectTransform) ((Component) this.ItemLayoutParent).GetComponent<RectTransform>();
        return (RectTransform) null;
      }
    }

    protected override void Start()
    {
      base.Start();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.SortFilter, (Object) null))
      {
        GameSettings instance = GameSettings.Instance;
        for (int index = 0; index < instance.UnitSort_Modes.Length; ++index)
          this.SortFilter.AddItem(LocalizedText.Get("sys.SORT_" + instance.UnitSort_Modes[index].Mode.ToString().ToUpper()), index);
        this.SortFilter.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnSortModeChange);
      }
      this.RefreshData();
    }

    protected override void Update()
    {
      base.Update();
    }

    public void Activated(int pinID)
    {
      if (pinID != 100 || !this.HasStarted)
        return;
      this.RefreshData();
    }

    private void OnSortModeChange(int index)
    {
      this.RefreshData();
    }

    public void RefreshData()
    {
      this.ClearItems();
    }

    protected override GameObject CreateItem()
    {
      return (GameObject) Object.Instantiate<GameObject>((M0) this.PieceTemplate);
    }

    private static bool GetValue(string str, string key, ref string value)
    {
      if (!str.StartsWith(key))
        return false;
      value = str.Substring(key.Length);
      return true;
    }

    public static void FilterUnits(List<UnitData> units, List<int> sortValues, string[] filter)
    {
      if (filter == null)
        return;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      List<string> stringList = new List<string>();
      string s = (string) null;
      for (int index = 0; index < filter.Length; ++index)
      {
        if (GetUnitWindow.GetValue(filter[index], "RARE:", ref s))
        {
          int result;
          if (int.TryParse(s, out result))
            num1 |= 1 << result;
        }
        else if (GetUnitWindow.GetValue(filter[index], "ELEM:", ref s))
        {
          try
          {
            EElement eelement = (EElement) Enum.Parse(typeof (EElement), s, true);
            num2 |= 1 << (int) (eelement & (EElement) 31 & (EElement) 31);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown element type: " + s));
          }
        }
        else if (GetUnitWindow.GetValue(filter[index], "WEAPON:", ref s))
        {
          try
          {
            AttackDetailTypes attackDetailTypes = (AttackDetailTypes) Enum.Parse(typeof (AttackDetailTypes), s, true);
            num3 |= 1 << (int) (attackDetailTypes & (AttackDetailTypes) 31 & (AttackDetailTypes) 31);
          }
          catch
          {
            if (GameUtility.IsDebugBuild)
              Debug.LogError((object) ("Unknown weapon type: " + s));
          }
        }
        else if (GetUnitWindow.GetValue(filter[index], "BIRTH:", ref s))
          stringList.Add(s);
      }
      for (int index = units.Count - 1; index >= 0; --index)
      {
        UnitData unit = units[index];
        if ((1 << unit.Rarity & num1) == 0 || (1 << (int) (unit.Element & (EElement) 31) & num2) == 0 || (unit.CurrentJob.GetAttackSkill() == null || (1 << (int) (unit.CurrentJob.GetAttackSkill().AttackDetailType & (AttackDetailTypes) 31) & num3) == 0) || (string.IsNullOrEmpty((string) unit.UnitParam.birth) || !stringList.Contains(unit.UnitParam.birth.ToString())))
        {
          units.RemoveAt(index);
          if (sortValues != null)
            sortValues.RemoveAt(index);
        }
      }
    }

    public void RefreshPieceUnit(bool clear, UnitSelectListData UnitSelectListData)
    {
      if (Object.op_Equality((Object) this.PieceTemplate, (Object) null))
        return;
      UnitParam[] allUnits = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnits();
      List<UnitParam> unitParamList = new List<UnitParam>(this.DataCount);
      for (int index = 0; index < UnitSelectListData.items.Count; ++index)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: reference to a compiler-generated method
        UnitParam unitParam = Array.Find<UnitParam>(allUnits, new Predicate<UnitParam>(new GetUnitWindow.\u003CRefreshPieceUnit\u003Ec__AnonStorey24F() { item = UnitSelectListData.items[index] }.\u003C\u003Em__297));
        if (unitParam != null)
          unitParamList.Add(unitParam);
      }
      this.SetData((object[]) unitParamList.ToArray(), typeof (UnitParam));
    }

    protected override void OnItemSelect(GameObject go)
    {
      this.OnSelectPieceUnit(go);
    }

    private void OnSelectUnit(GameObject go)
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(go, (UnitData) null);
      if (dataOfClass == null)
        return;
      if (this.OnUnitSelect != null)
      {
        this.OnUnitSelect(dataOfClass.UniqueID);
      }
      else
      {
        GlobalVars.SelectedUnitUniqueID.Set(dataOfClass.UniqueID);
        GlobalVars.SelectedUnitJobIndex.Set(dataOfClass.JobIndex);
        FlowNode_DownloadAssets component = (FlowNode_DownloadAssets) ((Component) this).GetComponent<FlowNode_DownloadAssets>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.DownloadUnits = new string[1]
          {
            GlobalVars.UnlockUnitID
          };
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
      }
    }

    private void OnSelectPieceUnit(GameObject go)
    {
      UnitParam dataOfClass = DataSource.FindDataOfClass<UnitParam>(go, (UnitParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.UnlockUnitID = dataOfClass.iname;
      FlowNode_DownloadAssets component = (FlowNode_DownloadAssets) ((Component) this).GetComponent<FlowNode_DownloadAssets>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.DownloadUnits = new string[1]
        {
          GlobalVars.UnlockUnitID
        };
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
    }

    public void SetSortMethod(string method, bool ascending, string[] filters)
    {
      GameUtility.UnitSortModes unitSortModes = GameUtility.UnitSortModes.Time;
      try
      {
        if (!string.IsNullOrEmpty(method))
          unitSortModes = (GameUtility.UnitSortModes) Enum.Parse(typeof (GameUtility.UnitSortModes), method, true);
      }
      catch (Exception ex)
      {
        if (GameUtility.IsDebugBuild)
          DebugUtility.LogError("Unknown sort mode: " + method);
      }
      if (Object.op_Inequality((Object) this.AscendingIcon, (Object) null))
        this.AscendingIcon.SetActive(ascending);
      if (Object.op_Inequality((Object) this.DescendingIcon, (Object) null))
        this.DescendingIcon.SetActive(!ascending);
      if (unitSortModes == GameUtility.UnitSortModes.Time)
        ascending = !ascending;
      this.RefreshData();
    }

    public void OnHoldIcon(GameObject go)
    {
      UnitParam dataOfClass = DataSource.FindDataOfClass<UnitParam>(go, (UnitParam) null);
      if (dataOfClass == null)
        return;
      GlobalVars.UnlockUnitID = dataOfClass.iname;
      FlowNode_DownloadAssets component = (FlowNode_DownloadAssets) ((Component) this).GetComponent<FlowNode_DownloadAssets>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.DownloadUnits = new string[1]
        {
          GlobalVars.UnlockUnitID
        };
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    public delegate void UnitSelectEvent(long uniqueID);
  }
}
