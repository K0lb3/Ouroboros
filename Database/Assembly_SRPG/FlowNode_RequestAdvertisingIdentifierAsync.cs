// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_RequestAdvertisingIdentifierAsync
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "Request Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(11, "Request Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.NodeType("System/RequestAdvertisingIdentifierAsync", 32741)]
  [FlowNode.Pin(101, "Start", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_RequestAdvertisingIdentifierAsync : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      // ISSUE: method pointer
      if (Application.RequestAdvertisingIdentifierAsync(new Application.AdvertisingIdentifierCallback((object) this, __methodptr(AdCallback))))
        return;
      this.ActivateOutputLinks(11);
    }

    public void AdCallback(string advertisingId, bool trackingEnabled, string error)
    {
      GameManager.SetDeviceID(advertisingId);
      this.ActivateOutputLinks(10);
    }
  }
}
