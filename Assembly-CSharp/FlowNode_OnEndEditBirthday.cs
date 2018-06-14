// Decompiled with JetBrains decompiler
// Type: FlowNode_OnEndEditBirthday
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[FlowNode.Pin(0, "Valid", FlowNode.PinTypes.Output, 0)]
[FlowNode.NodeType("Event/OnEndEditBirthday", 58751)]
[AddComponentMenu("")]
[FlowNode.Pin(1, "Invalid", FlowNode.PinTypes.Output, 0)]
public class FlowNode_OnEndEditBirthday : FlowNodePersistent
{
  [FlowNode.ShowInInfo]
  [FlowNode.DropTarget(typeof (InputField), true)]
  public InputField TargetYear;
  [FlowNode.DropTarget(typeof (InputField), true)]
  [FlowNode.ShowInInfo]
  public InputField TargetMonth;
  [FlowNode.DropTarget(typeof (InputField), true)]
  [FlowNode.ShowInInfo]
  public InputField TargetDay;
  [FlowNode.DropTarget(typeof (InputField), true)]
  public Button ok;

  private void Start()
  {
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetYear, (UnityEngine.Object) null))
    {
      // ISSUE: method pointer
      ((UnityEvent<string>) this.TargetYear.get_onEndEdit()).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CStart\u003Em__2AE)));
    }
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetMonth, (UnityEngine.Object) null))
    {
      // ISSUE: method pointer
      ((UnityEvent<string>) this.TargetMonth.get_onEndEdit()).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CStart\u003Em__2AF)));
    }
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetDay, (UnityEngine.Object) null))
    {
      // ISSUE: method pointer
      ((UnityEvent<string>) this.TargetDay.get_onEndEdit()).AddListener(new UnityAction<string>((object) this, __methodptr(\u003CStart\u003Em__2B0)));
    }
    ((Behaviour) this).set_enabled(true);
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    GUtility.SetImmersiveMove();
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetYear, (UnityEngine.Object) null) && this.TargetYear.get_onEndEdit() != null)
      ((UnityEventBase) this.TargetYear.get_onEndEdit()).RemoveAllListeners();
    if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetMonth, (UnityEngine.Object) null) && this.TargetMonth.get_onEndEdit() != null)
      ((UnityEventBase) this.TargetMonth.get_onEndEdit()).RemoveAllListeners();
    if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TargetDay, (UnityEngine.Object) null) || this.TargetDay.get_onEndEdit() == null)
      return;
    ((UnityEventBase) this.TargetDay.get_onEndEdit()).RemoveAllListeners();
  }

  private void OnEndEditYear(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.get_text().Length <= 0)
      return;
    DebugUtility.Log("OnEndEditYear:" + field.get_text());
    this.OutputResult();
  }

  private void OnEndEditMonth(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.get_text().Length <= 0)
      return;
    DebugUtility.Log("OnEndEditMonth:" + field.get_text());
    this.OutputResult();
  }

  private void OnEndEditDay(InputField field)
  {
    GUtility.SetImmersiveMove();
    if (field.get_text().Length <= 0)
      return;
    DebugUtility.Log("OnEndEditDay:" + field.get_text());
    this.OutputResult();
  }

  public override void OnActivate(int pinID)
  {
  }

  private void OutputResult()
  {
    int result1 = 0;
    int result2 = 0;
    int result3 = 0;
    DateTime now = DateTime.Now;
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TargetYear, (UnityEngine.Object) null) || string.IsNullOrEmpty(this.TargetYear.get_text()) || (!int.TryParse(this.TargetYear.get_text(), out result1) || now.Year < result1) || result1 < 1900)
      this.ActivateOutputLinks(1);
    else if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TargetMonth, (UnityEngine.Object) null) || string.IsNullOrEmpty(this.TargetMonth.get_text()) || (!int.TryParse(this.TargetMonth.get_text(), out result2) || result2 < 1) || (12 < result2 || now.Year == result1 && now.Month < result2))
    {
      this.ActivateOutputLinks(1);
    }
    else
    {
      int num;
      try
      {
        num = DateTime.DaysInMonth(result1, result2);
      }
      catch
      {
        this.ActivateOutputLinks(1);
        return;
      }
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.TargetDay, (UnityEngine.Object) null) || string.IsNullOrEmpty(this.TargetDay.get_text()) || (!int.TryParse(this.TargetDay.get_text(), out result3) || result3 < 1) || num < result3 || now.Year == result1 && now.Month == result2 && now.Day < result3)
      {
        this.ActivateOutputLinks(1);
      }
      else
      {
        GlobalVars.EditedYear = result1;
        GlobalVars.EditedMonth = result2;
        GlobalVars.EditedDay = result3;
        this.ActivateOutputLinks(0);
      }
    }
  }
}
