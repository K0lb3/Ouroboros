// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_NetworkReachability
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "Wifi", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("Network/NetworkReachability", 32741)]
  [FlowNode.Pin(1, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "Carrier", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(4, "Not Connected", FlowNode.PinTypes.Output, 102)]
  public class FlowNode_NetworkReachability : FlowNode
  {
    private const int PIN_ID_IN = 1;
    private const int PIN_ID_WIFI = 2;
    private const int PIN_ID_CARRIER = 3;
    private const int PIN_ID_NOT_CONNECTED = 4;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      switch ((int) Application.get_internetReachability())
      {
        case 0:
          this.ActivateOutputLinks(4);
          break;
        case 1:
          this.ActivateOutputLinks(3);
          break;
        case 2:
          this.ActivateOutputLinks(2);
          break;
      }
    }
  }
}
