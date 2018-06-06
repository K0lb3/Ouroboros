// Decompiled with JetBrains decompiler
// Type: SRPG.BuyCoinWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1000, "Bundles", FlowNode.PinTypes.Output, 1000)]
  [FlowNode.Pin(1001, "Gems", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class BuyCoinWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_BUNDLES = 1000;
    public const int PIN_GEMS = 1001;

    public BuyCoinWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      if (GlobalVars.SelectedPurchaseType == GlobalVars.PurchaseType.Bundles)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1000);
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 1001);
    }
  }
}
