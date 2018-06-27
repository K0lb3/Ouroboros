// Decompiled with JetBrains decompiler
// Type: FlowNode_WaitUrlSchemeObserver
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[FlowNode.NodeType("Event/WaitUrlSchemeObserver", 58751)]
[AddComponentMenu("")]
[FlowNode.Pin(102, "Start", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(0, "Finished", FlowNode.PinTypes.Output, 0)]
public class FlowNode_WaitUrlSchemeObserver : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID != 102)
      return;
    DebugUtility.Log("WaitUrlSchemeObserver start");
    ((Behaviour) this).set_enabled(true);
  }

  private void Update()
  {
    if (FlowNode_OnUrlSchemeLaunch.IsExecuting)
      return;
    DebugUtility.Log("WaitUrlSchemeObserver done");
    ((Behaviour) this).set_enabled(false);
    this.ActivateOutputLinks(0);
  }
}
