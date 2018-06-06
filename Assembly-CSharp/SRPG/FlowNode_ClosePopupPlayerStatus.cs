// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ClosePopupPlayerStatus
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/ClosePopup", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_ClosePopupPlayerStatus : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Object.op_Inequality((Object) FlowNode_BuyGold.ConfirmBoxObj, (Object) null))
      {
        Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) FlowNode_BuyGold.ConfirmBoxObj.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.BeginClose();
        FlowNode_BuyGold.ConfirmBoxObj = (GameObject) null;
      }
      if (Object.op_Inequality((Object) FlowNode_BuyStamina.ConfirmBoxObj, (Object) null))
      {
        Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) FlowNode_BuyStamina.ConfirmBoxObj.GetComponent<Win_Btn_DecideCancel_FL_C>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.BeginClose();
        FlowNode_BuyStamina.ConfirmBoxObj = (GameObject) null;
      }
      this.ActivateOutputLinks(1);
    }
  }
}
