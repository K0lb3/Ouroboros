// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetPurchaseType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/SetPurchaseType", 32741)]
  [FlowNode.Pin(100, "Bundles", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "Gems", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
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
