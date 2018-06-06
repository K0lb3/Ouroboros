// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetPurchaseType
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(101, "Gems", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "Bundles", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SetPurchaseType", 32741)]
  public class FlowNode_SetPurchaseType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          GlobalVars.SelectedPurchaseType = GlobalVars.PurchaseType.Bundles;
          break;
        case 101:
          GlobalVars.SelectedPurchaseType = GlobalVars.PurchaseType.Gems;
          break;
      }
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
  }
}
