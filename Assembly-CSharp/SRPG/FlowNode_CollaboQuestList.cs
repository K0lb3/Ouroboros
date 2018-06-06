// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CollaboQuestList
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/CollaboQuestList")]
  public class FlowNode_CollaboQuestList : FlowNode_GUI
  {
    protected override void OnInstanceCreate()
    {
      base.OnInstanceCreate();
      CollaboSkillQuestList componentInChildren = (CollaboSkillQuestList) this.Instance.GetComponentInChildren<CollaboSkillQuestList>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null))
        return;
      CollaboSkillParam.Pair collaboSkillPair = GlobalVars.SelectedCollaboSkillPair;
      if (collaboSkillPair == null)
      {
        DebugUtility.LogError("CollaboSkillParam.Pair が セットされていない");
      }
      else
      {
        componentInChildren.CurrentUnit1 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(collaboSkillPair.UnitParam1);
        componentInChildren.CurrentUnit2 = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueParam(collaboSkillPair.UnitParam2);
        if (componentInChildren.CurrentUnit1 == null)
          DebugUtility.LogError("window.CurrentUnit1 == null");
        else if (componentInChildren.CurrentUnit2 == null)
        {
          DebugUtility.LogError("window.CurrentUnit2 == null");
        }
        else
        {
          ((WindowController) ((Component) componentInChildren).GetComponent<WindowController>()).SetCollision(false);
          ((WindowController) ((Component) componentInChildren).GetComponent<WindowController>()).OnWindowStateChange = new WindowController.WindowStateChangeEvent(this.OnBack);
          WindowController.OpenIfAvailable((Component) componentInChildren);
        }
      }
    }

    private void OnBack(GameObject go, bool visible)
    {
      if (visible)
        return;
      CollaboSkillQuestList componentInChildren = (CollaboSkillQuestList) this.Instance.GetComponentInChildren<CollaboSkillQuestList>();
      if (Object.op_Equality((Object) componentInChildren, (Object) null) || visible)
        return;
      ((WindowController) ((Component) componentInChildren).GetComponent<WindowController>()).SetCollision(true);
      ((WindowController) ((Component) componentInChildren).GetComponent<WindowController>()).OnWindowStateChange = (WindowController.WindowStateChangeEvent) null;
      Object.Destroy((Object) ((Component) componentInChildren).get_gameObject());
    }
  }
}
